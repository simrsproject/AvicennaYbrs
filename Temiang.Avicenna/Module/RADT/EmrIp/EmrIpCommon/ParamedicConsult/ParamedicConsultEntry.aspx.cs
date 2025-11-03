using System;
using System.Web.UI;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    /// <summary>
    /// Consult dan Refer
    ///
    /// Create : Handono 2019 Nov (Timika)
    /// Note:
    /// - Tanggal read only dan nilainya ambil dari system utk yg baru
    /// Consult :
    ///  - Untuk Pasien tidak pindah ruangan (dokter datang ke ruangan untuk mengecek kondisi pasien)
    ///  - Dokter biasanya sudah tahu ke siapa dia akan mengkonsul dan disini dianggap sudah tahu
    ///  - Otomatis men-set dokter tujuan sbg dokter team suapaya bisa isi asesmen beserta jawabannya
    ///  - Untuk konsul ke fisioterapi memiliki tambahan isian dan hasil tambahannya dientry di PHR dgn QuestionFormType = FISIO
    ///
    /// Refer :
    ///  - Untuk Pasien disuruh ke Service Unit lain dan pendapatannya diakui di service unit yg dituju
    ///  - Membuat registrasi baru
    ///  
    /// Modif:
    ///  - Maret 2021 (HN): Paramedic tujuan bisa diedit jika jawaban belum diisi dan tipenya adalah konsul
    /// </summary>
    public partial class ParamedicConsultEntry : BasePageDialogEntry
    {
        private AppAutoNumberLast _autoNumber, _autoNumberTrans;

        #region QueryString Properties


        private bool IsUserAddAble
        {
            get
            {
                return Request.QueryString["addable"] == "True";
            }
        }
        #endregion
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;
            //ToolBar.AddEnabled = IsUserAddAble;
            IsMedicalRecordEntry = true; //Activate deadline edit & add

            // Hanya dokter utama dan dokter bersama (sharing team) yg bisa add
            ToolBar.AddEnabled = ParamedicTeam.IsParamedicTeamStatusDpjpOrSharing(RegistrationNo, AppSession.UserLogin.ParamedicID) || AppSession.Parameter.IsReferToSpecialistCanEntryByUserNonPhsycian;

            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            IsCustomReportList = true;

            #region Refer
            //TransChargesItemsDTConsumption = null;
            //TransChargesItemsDTComp = null;
            //TransChargesItemsDT = null;
            //RegistrationItemRules = null;
            #endregion

            if (!IsPostBack)
            {
                // Jangan dibuat properties utk mencegah kesalahan ambil krn form ini bukan single entry
                // shg ConsultReferNo yg dikirim dari page history hanya diambil saat bukan postback saja
                hdnConsultReferNo.Value = Request.QueryString["crno"];
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Consult / Refer Patient : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ", RegNo: " + RegistrationNo + ")";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitComplete(EventArgs e)
        {
            if (!IsPostBack)
            {
                // Toolbar Print
                var btn = new RadToolBarButton("Refer Notes")
                {
                    Value = string.Format("rpt_{0}", AppConstant.Report.ReferNotes)
                };
                ToolBarMenuPrint.Buttons.Add(btn);

                var btn2 = new RadToolBarButton("Refer Notes RM 12")
                {
                    Value = string.Format("rpt_{0}", AppConstant.Report.RM12_ReferNotes)
                };
                ToolBarMenuPrint.Buttons.Add(btn2);
            }
            base.OnInitComplete(e);

            // Hardcode Print button
            ToolBarMenuPrint.Visible = true;

        }

        private ParamedicConsultRefer _paramedicConsultRefer;
        private ParamedicConsultRefer CurrentConsultRefer
        {
            get
            {
                if (_paramedicConsultRefer == null)
                {
                    _paramedicConsultRefer = new ParamedicConsultRefer();
                    _paramedicConsultRefer.LoadByPrimaryKey(hdnConsultReferNo.Value);
                }
                return _paramedicConsultRefer;
            }
        }
        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            PopulateEntryControl(CurrentConsultRefer);
        }

        private void PopulateEntryControl(ParamedicConsultRefer consult)
        {
            ApplyConsultReferType(consult.ConsultReferType);
            ApplyServiceUnitID(consult.ToServiceUnitID);

            optConsultReferType.SelectedValue = consult.ConsultReferType;
            txtConsultDate.SelectedDate = consult.ConsultDateTime;
            txtConsultTime.SelectedDate = consult.ConsultDateTime;
            ComboBox.PopulateWithOneServiceUnit(cboServiceUnitID, consult.ToServiceUnitID);

            if (consult.ConsultReferType == "C")
                ComboBox.PopulateWithOneParamedic(cboConsultToParamedicID, consult.ToParamedicID);
            else
                ComboBox.PopulateWithOneParamedic(cboReferToParamedicID, consult.ToParamedicID);

            StandardReference.InitializeWithOneRow(cboSRParamedicConsultType, AppEnum.StandardReference.ParamedicConsultType,
                consult.SRParamedicConsultType);
            txtNotes.Text = consult.Notes;

            txtToRegistrationNo.Text = consult.ToRegistrationNo;
            ComboBox.PopulateWithOneRoom(cboRoomID, consult.ToRoomID);

            txtChiefComplaint.Text = consult.ChiefComplaint;
            txtPastMedicalHistory.Text = consult.PastMedicalHistory;
            txtHpi.Text = consult.Hpi;
            txtActionExamTreatment.Text = consult.ActionExamTreatment;

            txtActiveMotion.Text = consult.ActiveMotion;
            txtPassiveMotion.Text = consult.PassiveMotion;

            hdnConsultReferNo.Value = consult.ConsultReferNo == null ? string.Empty : consult.ConsultReferNo;

            //SIGN
            var imgHelper = new ImageHelper();
            if (consult.PhysicianSign != null)
            {
                var val = (byte[])consult.PhysicianSign;
                fmImage.DataValue = val;
                var mstream = new MemoryStream(val);
                Telerik.Web.UI.ImageEditor.EditableImage img = new Telerik.Web.UI.ImageEditor.EditableImage(mstream);
                hdnPhysicianSignImage.Value = imgHelper.ToBase64String(img.Image, ImageFormat.Png);
            }
            else
            {
                fmImage.DataValue = null;
                hdnPhysicianSignImage.Value = String.Empty;
            }

        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            // Sudah masuk ke Paramedic Team, tidak boleh diedit
            cboServiceUnitID.Enabled = newVal == AppEnum.DataMode.New;
            //cboConsultToParamedicID.Enabled = newVal == AppEnum.DataMode.New;

            if (optConsultReferType.SelectedValue == "C")
                cboConsultToParamedicID.Enabled = (newVal == AppEnum.DataMode.New) || (newVal == AppEnum.DataMode.Edit && string.IsNullOrWhiteSpace(CurrentConsultRefer.Answer));

            if (optConsultReferType.SelectedValue == "R")
                cboReferToParamedicID.Enabled = newVal == AppEnum.DataMode.New;

            optConsultReferType.Enabled = newVal == AppEnum.DataMode.New;
            cboRoomID.Enabled = newVal == AppEnum.DataMode.New;
            cboQue.Enabled = newVal == AppEnum.DataMode.New;

            lbtnResetPastMedicalHistory.Visible = newVal != AppEnum.DataMode.Read;
            lbtnResetHistoryOfPresentIllness.Visible = newVal != AppEnum.DataMode.Read;
            lbtnResetPhysicalExamination.Visible = newVal != AppEnum.DataMode.Read;

            //SIGN
            var isVisible = newVal != AppEnum.DataMode.Read;
            btnPhysicianSign.Enabled = isVisible;

        }
        protected override void OnMenuNewClick()
        {
            // Untuk mengembalikan display jika cancel
            hdnConsultReferNoPrev.Value = hdnConsultReferNo.Value;

            // Reset
            var consult = new ParamedicConsultRefer();
            PopulateEntryControl(consult);

            StandardReference.InitializeIncludeSpace(cboSRParamedicConsultType, AppEnum.StandardReference.ParamedicConsultType);

            // Default Value
            var date = (new DateTime()).NowAtSqlServer();
            txtConsultDate.SelectedDate = date;
            txtConsultTime.SelectedDate = date;

            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);
            txtChiefComplaint.Text = reg.Complaint;

            txtActionExamTreatment.Text = Patient.Last.PhysicalExamination(RegistrationNo, FromRegistrationNo).ReplaceHTMLTags();
            txtHpi.Text = Patient.Last.PatientAssessment(RegistrationNo, FromRegistrationNo).Hpi;
            txtPastMedicalHistory.Text = Patient.PastMedicalHistory(PatientID).ReplaceHTMLTags();

        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            args.IsCancel = !Save(args, true);

            if (!args.IsCancel)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeAndApply", "CloseAndApply();", true);
        }

        protected override void OnMenuCancelNewClick(ValidateArgs args)
        {
            hdnConsultReferNo.Value = hdnConsultReferNoPrev.Value;
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Save(args, false);
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            if (string.IsNullOrEmpty(hdnConsultReferNo.Value))
            {
                Helper.RegisterStartupScript(this, "invalid", "alert('Please select refer record first');");
                args.IsCancel = true;
                return;
            }

            if (programID == AppConstant.Report.RM12_ReferNotes)
            {
                var jobParameter = printJobParameters.AddNew();
                jobParameter.Name = "RegistrationNo";
                jobParameter.ValueString = RegistrationNo;
            }

            var jobParameter2 = printJobParameters.AddNew();
            jobParameter2.Name = "ConsultReferNo";
            jobParameter2.ValueString = hdnConsultReferNo.Value;
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
            StandardReference.InitializeIncludeSpace(cboSRParamedicConsultType, AppEnum.StandardReference.ParamedicConsultType);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var consul = new ParamedicConsultRefer();
            if (consul.LoadByPrimaryKey(hdnConsultReferNo.Value))
            {
                if (consul.ConsultReferType == "R")
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(consul.ToRegistrationNo);
                    if (reg.IsVoid == null || reg.IsVoid == false)
                    {
                        args.MessageText = "Refer type can't delete before void at registration";
                        args.IsCancel = true;
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(consul.Answer))
                {
                    args.MessageText = "Consult has responded, can not void refer";
                    args.IsCancel = true;
                    return;
                }

                // Delete ParamedicTeam jika tidak ada consul lain dgn paramedicID yg sama
                var coll = new ParamedicConsultReferCollection();
                coll.Query.Where(coll.Query.RegistrationNo == consul.RegistrationNo,
                    coll.Query.ToParamedicID == consul.ToParamedicID);
                coll.LoadAll();
                if (coll.Count == 1)
                {
                    var pt = new ParamedicTeam();
                    if (pt.LoadByPrimaryKey(consul.RegistrationNo, consul.ToParamedicID,
                            consul.ConsultDateTime.Value.Date) && pt.SourceType == "C")
                    {
                        pt.MarkAsDeleted();
                        pt.Save();
                    }
                }


                // Mark RegistrationInfoMedic
                var consultRefer = consul.ConsultReferType == "C" ? "CON" : "REF";
                var ent = new RegistrationInfoMedic();
                var qr = new RegistrationInfoMedicQuery();
                qr.Where(qr.RegistrationNo == RegistrationNo, qr.ReferenceNo == consul.ConsultReferNo, qr.SRMedicalNotesInputType == consultRefer);
                qr.es.Top = 1;


                if (ent.Load(qr) && !string.IsNullOrEmpty(ent.RegistrationInfoMedicID))
                {
                    ent.IsDeleted = true;
                    ent.IsApproved = false;
                    ent.Save();
                }

                consul.MarkAsDeleted();
                consul.Save();

            }

            // Close
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeAndApply", "CloseAndApply();", true);
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {
        }
        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        protected override void OnMenuRejournalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }
        public override string OnGetScriptToolBarSaveClicking()
        {
            return string.Empty;
        }

        public override bool OnGetStatusMenuAdd()
        {
            return ParamedicTeam.IsParamedicTeamStatusDpjpOrSharing(RegistrationNo, AppSession.UserLogin.ParamedicID);
        }
        public override bool OnGetStatusMenuEdit()
        {
            return OnGetStatusMenuDelete();
        }

        public override bool OnGetStatusMenuDelete()
        {
            var refer = new ParamedicConsultRefer();
            if (refer.LoadByPrimaryKey(hdnConsultReferNo.Value))
            {
                // Hanya yg buat yg bisa menghapus
                return string.IsNullOrEmpty(refer.Answer) && refer.ParamedicID.Equals(AppSession.UserLogin.ParamedicID);
            }
            return false;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return null;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        private bool IsEntryValid(ValidateArgs args, bool isNewRecord)
        {
            if (string.IsNullOrWhiteSpace(optConsultReferType.SelectedValue))
            {
                args.MessageText = "Consult / Refer Type required.";
                args.IsCancel = true;
                return false;
            }

            var regEnt = new Registration();
            if (regEnt.LoadByPrimaryKey(txtToRegistrationNo.Text))
            {
                if (txtConsultDate.SelectedDate.Value.Date < regEnt.RegistrationDate)
                {
                    args.MessageText = "Consult / Refer date can not less than registration date";
                    args.IsCancel = true;
                    return false;
                }
            }

            if (optConsultReferType.SelectedValue == "R")
            {
                #region Refer Validation

                if (string.IsNullOrWhiteSpace(cboServiceUnitID.SelectedValue))
                {
                    args.MessageText = "Service Unit required.";
                    args.IsCancel = true;
                    return false;
                }

                var su = new ServiceUnit();
                if (!su.LoadByPrimaryKey(cboServiceUnitID.SelectedValue))
                {
                    args.MessageText = "Service Unit required.";
                    args.IsCancel = true;
                    return false;
                }

                if (string.IsNullOrWhiteSpace(cboReferToParamedicID.SelectedValue))
                {
                    args.MessageText = "Physican required.";
                    args.IsCancel = true;
                    return false;
                }

                var par = new Paramedic();
                if (!par.LoadByPrimaryKey(cboReferToParamedicID.SelectedValue))
                {
                    args.MessageText = "Physican required.";
                    args.IsCancel = true;
                    return false;
                }

                if (string.IsNullOrEmpty(cboRoomID.SelectedValue))
                {
                    args.MessageText = "Room required.";
                    args.IsCancel = true;
                    return false;
                }

                var sp = new ServiceUnitParamedic();
                sp.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboReferToParamedicID.SelectedValue);
                if ((sp.IsUsingQue ?? false))
                {
                    if (cboQue.SelectedValue == "0" || string.IsNullOrEmpty(cboQue.SelectedValue))
                    {
                        args.MessageText = "Que Slot Number is required.";
                        args.IsCancel = true;
                        return false;
                    }
                }

                string time;
                if (!string.IsNullOrEmpty(cboQue.Text))
                {
                    string value = cboQue.Text.Split('-')[1].Substring(1);
                    DateTime dt;
                    DateTime.TryParse(value, out dt);
                    time = dt.ToString("HH:mm");
                }
                else
                    time = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(cboServiceUnitID.SelectedValue);

                string physicianOnleave = Registration.GetPhysicianOnLeave(
                    txtConsultDate.SelectedDate ?? (new DateTime()).NowAtSqlServer(), time,
                    unit.SRRegistrationType,
                    cboReferToParamedicID.SelectedValue,
                    cboServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(physicianOnleave))
                {
                    args.MessageText = physicianOnleave;
                    args.IsCancel = true;
                    return false;
                }

                if (isNewRecord)
                {
                    var reg = new RegistrationQuery("a");
                    var qu = new ServiceUnitQueQuery("b");
                    reg.Select(reg.RegistrationNo, reg.ParamedicID, reg.RegistrationDate, qu.ServiceUnitID);
                    reg.InnerJoin(qu).On(qu.RegistrationNo == reg.RegistrationNo);
                    reg.Where(reg.ParamedicID == cboReferToParamedicID.SelectedValue, reg.PatientID == PatientID,
                        qu.ServiceUnitID == cboServiceUnitID.SelectedValue, reg.RegistrationDate == DateTime.Today,
                        reg.IsClosed == false, qu.IsFromReferProcess == true);


                    var dtbs = reg.LoadDataTable();
                    if (dtbs.Rows.Count > 0)
                    {
                        args.MessageText =
                            "Registration to the same unit and phycisian for this patient already exist in today's registration list.";
                        args.IsCancel = true;
                        return false;
                    }
                }

                #endregion
            }
            else
            {
                if (string.IsNullOrWhiteSpace(cboConsultToParamedicID.SelectedValue))
                {
                    args.MessageText = "Physican required.";
                    args.IsCancel = true;
                    return false;

                }

                var par = new Paramedic();
                if (!par.LoadByPrimaryKey(cboConsultToParamedicID.SelectedValue))
                {
                    args.MessageText = "Physican required.";
                    args.IsCancel = true;
                    return false;
                }
            }


            return true;
        }

        private bool Save(ValidateArgs args, bool isNewRecord)
        {

            // Validate
            if (!IsEntryValid(args, isNewRecord)) return false;

            using (var trans = new esTransactionScope())
            {
                // Save Refer
                var consult = new ParamedicConsultRefer();
                if (optConsultReferType.SelectedValue == "C")
                {
                    // Hanya tambah di paramedic team supaya dokter nya bisa mengakses EMR
                    SaveNewParamedicTeam(isNewRecord);
                }

                //db:20231031 - dipindah ke bawah, krn pada saat edit valuenya ke refresh
                //var imgHelper = new ImageHelper();
                ////SIGN
                //if (!string.IsNullOrWhiteSpace(hdnPhysicianSignImage.Value))
                //{

                //    var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnPhysicianSignImage.Value), new Size(332, 185));
                //    consult.PhysicianSign = imgHelper.ToByteArray(resized, ImageFormat.Png);
                //}
                //else
                //    consult.PhysicianSign = null;

                if (isNewRecord)
                {
                    consult.ConsultReferType = optConsultReferType.SelectedValue;

                    if (optConsultReferType.SelectedValue == "R")
                    {
                        // Create new Registration for Refer krn beda klinik
                        // Tidak perlu tambah di paramedic team krn dokternya sudah menjadi DPJP yg berarti bisa mengakses EMR
                        var newRreg = SaveNewRegistration(args);
                        if (args.IsCancel == true) return false;
                        consult.ToRegistrationNo = newRreg.RegistrationNo;
                        consult.ToRegistrationQue = newRreg.RegistrationQue;
                    }
                    else
                    {
                        // Hanya tambah di paramedic team supaya dokter nya bisa mengakses EMR
                        //SaveNewParamedicTeam();
                    }

                    // Sequence No
                    var lastConsult = new ParamedicConsultRefer();
                    lastConsult.Query.Where(lastConsult.Query.RegistrationNo == RegistrationNo);
                    lastConsult.Query.es.Top = 1;
                    lastConsult.Query.OrderBy(lastConsult.Query.ConsultReferNo.Descending);
                    lastConsult.Query.Select(lastConsult.Query.ConsultReferNo);
                    if (lastConsult.Query.Load())
                    {
                        var lastNo = lastConsult.ConsultReferNo.Trim();
                        var lastSeqNo = lastNo.Substring(lastNo.Length - 4).ToInt();
                        consult.ConsultReferNo = string.Format("{0}-{1:0000}", RegistrationNo, lastSeqNo + 1);

                    }
                    else
                        consult.ConsultReferNo = string.Format("{0}-0001", RegistrationNo);

                    consult.RegistrationNo = RegistrationNo;
                    consult.ParamedicID = ParamedicID;
                    consult.IsPhysiotherapy = pnlBasicFunction.Visible;
                }
                else
                {
                    consult.LoadByPrimaryKey(hdnConsultReferNo.Value);
                }

                consult.ConsultDateTime = txtConsultTime.SelectedDate;
                if (string.IsNullOrWhiteSpace(cboSRParamedicConsultType.SelectedValue))
                    consult.str.SRParamedicConsultType = string.Empty;
                else
                    consult.SRParamedicConsultType = cboSRParamedicConsultType.SelectedValue;

                consult.Notes = txtNotes.Text;
                consult.ChiefComplaint = txtChiefComplaint.Text;
                consult.PastMedicalHistory = txtPastMedicalHistory.Text;
                consult.Hpi = txtHpi.Text;
                consult.ActionExamTreatment = txtActionExamTreatment.Text;

                consult.ActiveMotion = txtActiveMotion.Text;
                consult.PassiveMotion = txtPassiveMotion.Text;

                var imgHelper = new ImageHelper();
                //SIGN
                if (!string.IsNullOrWhiteSpace(hdnPhysicianSignImage.Value))
                {
                    var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnPhysicianSignImage.Value), new Size(332, 185));
                    consult.PhysicianSign = imgHelper.ToByteArray(resized, ImageFormat.Png);
                }
                else
                    consult.PhysicianSign = null;

                var info = string.Empty;
                if (optConsultReferType.SelectedValue == "R")
                {
                    info = string.Format("{0} [{1}]", cboReferToParamedicID.Text, cboServiceUnitID.Text);
                    consult.ToParamedicID = cboReferToParamedicID.SelectedValue;
                    consult.ToServiceUnitID = cboServiceUnitID.SelectedValue;
                    consult.ToRoomID = cboRoomID.SelectedValue;
                }
                else
                {
                    info = cboConsultToParamedicID.Text;
                    consult.ToParamedicID = cboConsultToParamedicID.SelectedValue;
                }
                consult.Save();

                SaveRegistrationInfoMedic(consult.ConsultReferNo, consult.Notes, consult.ActionExamTreatment,
                    info, consult.ConsultDateTime, consult.ConsultReferType);

                trans.Complete();
                hdnConsultReferNo.Value = consult.ConsultReferNo;
            }

            #region Cetakan Registrasi hasil refer
            // Jangan disimpan di scope transaction diatas krn bentrok transaction scopenya
            if (optConsultReferType.SelectedValue == "R" && AppSession.Parameter.IsRegistrationPrintAutomatic)
            {
                var dt = new TransChargesItemQuery("a");
                var hd = new TransChargesQuery("b");
                dt.InnerJoin(hd).On(dt.TransactionNo == hd.TransactionNo &&
                                    hd.RegistrationNo == txtToRegistrationNo.Text && hd.IsAutoBillTransaction == true &&
                                    hd.IsBillProceed == true);
                dt.Select(dt.TransactionNo);
                DataTable dtb = dt.LoadDataTable();

                if (dtb.Rows.Count > 0)
                {
                    if (AppSession.Parameter.IsRegistrationPrintReceipt)
                    {
                        var parametersReceipt = new PrintJobParameterCollection();
                        parametersReceipt.AddNew("p_RegistrationNo", txtToRegistrationNo.Text, null, null);
                        PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationReceiptRpt, parametersReceipt);
                    }
                }
                else
                {
                    if (AppSession.Parameter.IsRegistrationPrintTicket)
                    {
                        var parametersTicket = new PrintJobParameterCollection();
                        parametersTicket.AddNew("p_RegistrationNo", txtToRegistrationNo.Text, null, null);
                        PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationTicketRpt, parametersTicket);
                    }
                }
            }
            #endregion
            return true;
        }

        private void SaveNewParamedicTeam(bool isNewRecord)
        {
            // Add to ParamedicTeam
            var teams = new ParamedicTeamCollection();
            teams.Query.es.Top = 1;
            teams.Query.Where(teams.Query.RegistrationNo == RegistrationNo,
                teams.Query.ParamedicID == cboConsultToParamedicID.SelectedValue);
            teams.Query.OrderBy(teams.Query.StartDate.Descending);
            teams.LoadAll();
            if (teams.Count == 0 || (teams.Count == 1 &&
                                     (teams[0].StartDate > Convert.ToDateTime(txtConsultDate.SelectedDate)
                                          .Date
                                      || (teams[0].EndDate != null && teams[0].EndDate <
                                          Convert.ToDateTime(txtConsultDate.SelectedDate).Date))))
            {
                var team = new ParamedicTeam();
                team.RegistrationNo = RegistrationNo;
                team.ParamedicID = cboConsultToParamedicID.SelectedValue;
                team.SRParamedicTeamStatus = "02"; //Set sbg Dokter rawat bersama dulu TODO: selanjutnya diupdate saat dokter menjawab konsul
                team.StartDate = Convert.ToDateTime(txtConsultDate.SelectedDate);
                team.SourceType = optConsultReferType.SelectedValue;
                team.Save();
            }
        }

        private void SaveRegistrationInfoMedic(string refNo, string notes, string action, string info, DateTime? referDate, string consultRefer)
        {
            consultRefer = consultRefer == "C" ? "CON" : "REF";
            var ent = new RegistrationInfoMedic();
            var qr = new RegistrationInfoMedicQuery();
            qr.Where(qr.RegistrationNo == RegistrationNo, qr.ReferenceNo == refNo, qr.SRMedicalNotesInputType == consultRefer);
            qr.es.Top = 1;

            ent.Load(qr);

            if (string.IsNullOrEmpty(ent.RegistrationInfoMedicID))
            {
                ent = new RegistrationInfoMedic();
                var autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.RegInfoMedicNo);
                ent.RegistrationInfoMedicID = autoNumber.LastCompleteNumber;
                autoNumber.Save();

                ent.RegistrationNo = RegistrationNo;

                ent.SRMedicalNotesInputType = consultRefer;
                ent.ServiceUnitID = string.Empty;
                ent.ParamedicID = ParamedicID;

                ent.Info4 = string.Empty;
            }

            ent.Info1 = info;
            ent.Info2 = notes;
            ent.Info3 = action;
            ent.IsPRMRJ = false;
            ent.DateTimeInfo = referDate;
            ent.ReferenceNo = refNo;
            ent.Save();
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ApplyServiceUnitID(e.Value);
        }

        private void ApplyServiceUnitID(string serviceUnitID)
        {
            pnlBasicFunction.Visible = false; // Hanya untuk Fisioterapi
            if (!string.IsNullOrEmpty(serviceUnitID))
            {
                var unit = new ServiceUnit();
                if (unit.LoadByPrimaryKey(serviceUnitID))
                {
                    pnlBasicFunction.Visible = AppParameter
                        .GetParameterValue(AppParameter.ParameterItem.PhysiotherapyServiceUnitIDs).Contains(serviceUnitID);

                    ComboBox.PopulateWithParamedic(cboReferToParamedicID, serviceUnitID);
                    ComboBox.PopulateWithRoom(cboRoomID, serviceUnitID);

                    // Refer hanya untuk Rawat Jalan
                    switch (unit.SRRegistrationType)
                    {
                        case AppConstant.RegistrationType.OutPatient:
                            txtToRegistrationNo.Text = GetNewRegistrationNo(AppSession.Parameter.OutPatientDepartmentID);
                            break;
                    }
                    return;
                }
            }
            cboQue.Items.Clear();
            cboQue.Text = string.Empty;

            cboReferToParamedicID.Items.Clear();
            cboReferToParamedicID.Text = string.Empty;

            cboRoomID.Items.Clear();
            cboRoomID.Text = string.Empty;
        }
        protected void cboReferToParamedicID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ApplyParamedicID(e.Value);
        }

        private string GetNewRegistrationNo(string departmentID)
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, BusinessObject.Reference.TransactionCode.Registration, departmentID);
            return _autoNumber.LastCompleteNumber;
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }
        protected void cboServiceUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            // Service Unit untuk Refer hanya yg tipenya Rawat Jalan
            var query = new ServiceUnitQuery("a");
            query.Select
            (
                query.ServiceUnitID,
                query.ServiceUnitName
            );
            query.OrderBy(query.ServiceUnitID.Ascending);
            query.Where
            (
                query.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
                query.ServiceUnitName.Like(searchTextContain),
                query.IsActive == true
            );

            cboServiceUnitID.DataSource = query.LoadDataTable();
            cboServiceUnitID.DataBind();
        }


        #region Refer create new Registration

        private TransChargesItemCollection _transChargesItemsDT = null;
        private TransChargesItemCollection TransChargesItemsDT
        {
            get
            {
                if (IsPostBack)
                {
                    if (_transChargesItemsDT != null)
                        return (_transChargesItemsDT);
                }

                //var coll = new TransChargesItemCollection();
                //var header = new TransChargesQuery("a");
                //var detail = new TransChargesItemQuery("b");

                //detail.InnerJoin(header).On(detail.TransactionNo == header.TransactionNo);
                //detail.Where
                //    (
                //        header.RegistrationNo == txtToRegistrationNo.Text,
                //        header.ToServiceUnitID == cboServiceUnitID.SelectedValue,
                //        header.IsAutoBillTransaction == true,
                //        header.IsVoid == false,
                //        detail.IsVoid == false
                //    );

                //coll.Load(detail);

                //_transChargesItemsDT = coll;
                //return coll;
                _transChargesItemsDT = new TransChargesItemCollection();
                return _transChargesItemsDT;
            }
        }

        private TransChargesItemCompCollection _transChargesItemsDTComp = null;
        private TransChargesItemCompCollection TransChargesItemsDTComp
        {
            get
            {
                if (IsPostBack)
                {
                    if (_transChargesItemsDTComp != null)
                        return _transChargesItemsDTComp;
                }

                //var coll = new TransChargesItemCompCollection();

                //var header = new TransChargesQuery("a");
                //var detail = new TransChargesItemQuery("b");
                //var comp = new TransChargesItemCompQuery("c");

                //comp.InnerJoin(detail).On
                //    (
                //        comp.TransactionNo == detail.TransactionNo &
                //        comp.ConsultReferNo == detail.ConsultReferNo
                //    );
                //comp.InnerJoin(header).On(detail.TransactionNo == header.TransactionNo);
                //comp.Where
                //    (
                //        header.RegistrationNo == txtToRegistrationNo.Text,
                //        header.ToServiceUnitID == cboServiceUnitID.SelectedValue,
                //        header.IsAutoBillTransaction == true,
                //        header.IsVoid == false,
                //        detail.IsVoid == false
                //    );

                //coll.Load(comp);

                //ViewState["collTransChargesItemComp" + Request.UserHostName] = coll;
                //return coll;
                _transChargesItemsDTComp = new TransChargesItemCompCollection();
                return _transChargesItemsDTComp;
            }
            set { _transChargesItemsDTComp = value; }
        }

        private TransChargesItemConsumptionCollection _transChargesItemsDTConsumption = null;
        private TransChargesItemConsumptionCollection TransChargesItemsDTConsumption
        {
            get
            {
                if (IsPostBack)
                {
                    if (_transChargesItemsDTConsumption != null)
                        return _transChargesItemsDTConsumption;
                }

                //var coll = new TransChargesItemConsumptionCollection();

                //var header = new TransChargesQuery("a");
                //var detail = new TransChargesItemQuery("b");
                //var cons = new TransChargesItemConsumptionQuery("c");

                //cons.InnerJoin(detail).On
                //    (
                //        cons.TransactionNo == detail.TransactionNo &
                //        cons.ConsultReferNo == detail.ConsultReferNo
                //    );
                //cons.InnerJoin(header).On(detail.TransactionNo == header.TransactionNo);
                //cons.Where
                //    (
                //        header.RegistrationNo == txtToRegistrationNo.Text,
                //        header.ToServiceUnitID == cboServiceUnitID.SelectedValue,
                //        header.IsAutoBillTransaction == true,
                //        header.IsVoid == false,
                //        detail.IsVoid == false
                //    );

                //coll.Load(cons);

                //ViewState["collTransChargesItemConsumption" + Request.UserHostName] = coll;
                //return coll;
                _transChargesItemsDTConsumption = new TransChargesItemConsumptionCollection();
                return _transChargesItemsDTConsumption;
            }
            set
            {
                //ViewState["collTransChargesItemConsumption" + Request.UserHostName] = value;
                _transChargesItemsDTConsumption = value;
            }
        }

        private CostCalculationCollection _costCalculations = null;
        private CostCalculationCollection CostCalculations
        {
            get
            {
                if (IsPostBack)
                {
                    if (_costCalculations != null)
                        return _costCalculations;
                }

                //var coll = new CostCalculationCollection();

                //var header = new TransChargesQuery("a");
                //var detail = new TransChargesItemQuery("b");
                //var cost = new CostCalculationQuery("c");

                //cost.InnerJoin(detail).On
                //    (
                //        cost.TransactionNo == detail.TransactionNo &
                //        cost.ConsultReferNo == detail.ConsultReferNo
                //    );
                //cost.InnerJoin(header).On(detail.TransactionNo == header.TransactionNo);
                //cost.Where
                //    (
                //        header.RegistrationNo == txtToRegistrationNo.Text,
                //        header.ToServiceUnitID == cboServiceUnitID.SelectedValue,
                //        header.IsAutoBillTransaction == true,
                //        header.IsVoid == false,
                //        detail.IsVoid == false
                //    );

                //coll.Load(cost);

                //ViewState["collCostCalculation" + Request.UserHostName] = coll;
                //return coll;
                _costCalculations = new CostCalculationCollection();
                return _costCalculations;
            }
            set
            {
                //ViewState["collCostCalculation" + Request.UserHostName] = value;
                _costCalculations = value;
            }
        }

        private RegistrationItemRuleCollection _registrationItemRules = null;
        private RegistrationItemRuleCollection RegistrationItemRules
        {
            get
            {
                if (IsPostBack)
                {
                    if (_registrationItemRules != null)
                        return _registrationItemRules;
                }

                //var coll = new RegistrationItemRuleCollection();
                //var query = new RegistrationItemRuleQuery("a");
                //var iq = new ItemQuery("b");
                //var qSr = new AppStandardReferenceItemQuery("c");

                //query.Select
                //    (
                //        query,
                //        iq.ItemName.As("refToItem_ItemName"),
                //        qSr.ItemName.As("refToSRItem_ItemName")
                //    );

                //query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                //query.LeftJoin(qSr).On
                //    (
                //        query.SRGuarantorRuleType == qSr.ItemID &
                //        qSr.StandardReferenceID == "GuarantorRuleType"
                //    );

                //query.Where(query.RegistrationNo == txtToRegistrationNo.Text);

                //query.OrderBy(query.ItemID.Ascending);

                //coll.Load(query);

                //Session["collRegistrationItemRule" + Request.UserHostName] = coll;
                //return coll;
                _registrationItemRules = new RegistrationItemRuleCollection();
                return _registrationItemRules;
            }
            set
            {
                //Session["collRegistrationItemRule" + Request.UserHostName] = value;
                _registrationItemRules = value;
            }
        }

        private string GetNewTransactionNo()
        {
            _autoNumberTrans = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.TransactionNo);
            return _autoNumberTrans.LastCompleteNumber;
        }

        private void SaveNewRegistration(Registration entity, ServiceUnitQue que, MergeBilling billing, TransCharges chargesHD)
        {
            var serverDate = (new DateTime()).NowAtSqlServer();
            // reg
            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(cboServiceUnitID.SelectedValue);

            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);

            switch (unit.SRRegistrationType)
            {
                case AppConstant.RegistrationType.MedicalCheckUp:
                case AppConstant.RegistrationType.OutPatient:
                    txtToRegistrationNo.Text = GetNewRegistrationNo(AppSession.Parameter.OutPatientDepartmentID);
                    if (AppSession.Parameter.IsReferPatientUsingClassBefore)
                    {
                        entity.ClassID = reg.ClassID;
                        entity.ChargeClassID = reg.ChargeClassID;
                        entity.CoverageClassID = reg.CoverageClassID;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(unit.DefaultChargeClassID))
                        {
                            entity.ClassID = AppSession.Parameter.OutPatientClassID;
                            entity.ChargeClassID = AppSession.Parameter.OutPatientClassID;
                            entity.CoverageClassID = AppSession.Parameter.OutPatientClassID;
                        }
                        else
                        {
                            entity.ClassID = unit.DefaultChargeClassID;
                            entity.ChargeClassID = unit.DefaultChargeClassID;
                            entity.str.CoverageClassID = unit.DefaultChargeClassID;
                        }
                    }
                    entity.IsClusterAssessment = true;
                    break;
                case AppConstant.RegistrationType.ClusterPatient:
                    txtToRegistrationNo.Text = GetNewRegistrationNo(AppSession.Parameter.ClusterPatientDepartmentID);
                    if (string.IsNullOrEmpty(unit.DefaultChargeClassID))
                    {
                        entity.ClassID = AppSession.Parameter.ClusterPatientClassID;
                        entity.ChargeClassID = AppSession.Parameter.ClusterPatientClassID;
                        entity.CoverageClassID = AppSession.Parameter.ClusterPatientClassID;
                    }
                    else
                    {
                        entity.ClassID = unit.DefaultChargeClassID;
                        entity.ChargeClassID = unit.DefaultChargeClassID;
                        entity.str.CoverageClassID = unit.DefaultChargeClassID;
                    }
                    if (!(entity.IsClusterAssessment ?? false))
                        entity.IsClusterAssessment = (cboReferToParamedicID.SelectedValue != string.Empty) && (cboRoomID.SelectedValue != string.Empty);
                    break;
                case AppConstant.RegistrationType.EmergencyPatient:
                    txtToRegistrationNo.Text = GetNewRegistrationNo(AppSession.Parameter.EmergencyDepartmentID);
                    if (string.IsNullOrEmpty(unit.DefaultChargeClassID))
                    {
                        entity.ClassID = AppSession.Parameter.EmergencyPatientClassID;
                        entity.ChargeClassID = AppSession.Parameter.EmergencyPatientClassID;
                        entity.CoverageClassID = AppSession.Parameter.EmergencyPatientClassID;
                    }
                    else
                    {
                        entity.ClassID = unit.DefaultChargeClassID;
                        entity.ChargeClassID = unit.DefaultChargeClassID;
                        entity.str.CoverageClassID = unit.DefaultChargeClassID;
                    }
                    entity.IsClusterAssessment = true;
                    break;
            }

            entity.RegistrationNo = txtToRegistrationNo.Text;
            entity.SRRegistrationType = unit.SRRegistrationType;

            entity.GuarantorID = reg.GuarantorID;
            entity.PatientID = reg.PatientID;
            entity.RegistrationDate = txtConsultDate.SelectedDate.Value.Date;
            if (!string.IsNullOrEmpty(cboQue.Text))
            {
                string value = cboQue.Text.Split('-')[1].Substring(1);
                DateTime dt;
                DateTime.TryParse(value, out dt);
                entity.RegistrationTime = dt.ToString("HH:mm");
            }
            else
                entity.RegistrationTime = serverDate.ToString("HH:mm");

            entity.GuarantorCardNo = reg.GuarantorCardNo;
            entity.BpjsSepNo = reg.BpjsSepNo;

            entity.AppointmentNo = string.Empty;
            entity.AgeInYear = reg.AgeInYear;
            entity.AgeInMonth = reg.AgeInMonth;
            entity.AgeInDay = reg.AgeInDay;
            entity.SRShift = Registration.GetShiftID();
            entity.DepartmentID = unit.DepartmentID;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.VisitTypeID = reg.VisitTypeID;
            entity.SRPatientCategory = reg.SRPatientCategory;
            entity.SRBussinesMethod = reg.SRBussinesMethod;
            entity.ParamedicID = cboReferToParamedicID.SelectedValue;
            entity.RoomID = cboRoomID.SelectedValue;
            entity.Anamnesis = string.Empty;
            entity.Complaint = string.Empty;
            entity.IsConsul = true;
            entity.IsNewPatient = false;
            entity.IsFromDispensary = false;
            entity.IsRoomIn = reg.IsRoomIn;
            entity.PersonID = reg.PersonID;
            entity.EmployeeNumber = reg.EmployeeNumber;
            entity.SREmployeeRelationship = reg.SREmployeeRelationship;

            var query = new RegistrationQuery();
            query.es.Top = 1;
            query.Where
                (
                    query.PatientID == entity.PatientID,
                    query.ServiceUnitID == entity.ServiceUnitID
                );

            //reg = new Registration();
            //entity.IsNewVisit = !reg.Load(query);

            var sch = new ParamedicScheduleDate();
            if (sch.LoadByPrimaryKey(entity.ServiceUnitID, entity.ParamedicID,
                                     entity.RegistrationDate.Value.Year.ToString(), entity.RegistrationDate.Value.Date))
            {
                var sp = new ServiceUnitParamedic();
                if (sp.LoadByPrimaryKey(entity.ServiceUnitID, entity.ParamedicID))
                {
                    if (sp.IsUsingQue ?? false)
                    {
                        entity.RegistrationQue = !string.IsNullOrEmpty(cboQue.SelectedValue) ? int.Parse(cboQue.Text.Split('-')[0].Trim()) :
                                        ServiceUnitQue.GetNewQueNo(entity.ServiceUnitID, entity.ParamedicID, entity.RegistrationDate ?? serverDate.Date);
                    }
                    else
                        entity.RegistrationQue = ServiceUnitQue.GetNewQueNo(cboServiceUnitID.SelectedValue, cboReferToParamedicID.SelectedValue, entity.RegistrationDate.Value.Date);
                }
                else
                    entity.RegistrationQue = ServiceUnitQue.GetNewQueNo(cboServiceUnitID.SelectedValue, cboReferToParamedicID.SelectedValue, entity.RegistrationDate.Value.Date);
            }
            else
                entity.RegistrationQue = ServiceUnitQue.GetNewQueNo(cboServiceUnitID.SelectedValue, cboReferToParamedicID.SelectedValue, entity.RegistrationDate.Value.Date);

            entity.FromRegistrationNo = RegistrationNo;

            //Last Update Status
            entity.LastCreateUserID = AppSession.UserLogin.UserID;
            entity.LastCreateDateTime = serverDate;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = serverDate;

            que.QueNo = entity.RegistrationQue;
            que.QueDate = entity.RegistrationDate + TimeSpan.Parse(entity.RegistrationTime);
            que.ServiceUnitID = cboServiceUnitID.SelectedValue;
            que.ServiceRoomID = cboRoomID.SelectedValue;
            que.RegistrationNo = txtToRegistrationNo.Text;
            que.ParamedicID = cboReferToParamedicID.SelectedValue;
            que.Notes = txtNotes.Text;
            que.IsFromReferProcess = true;
            que.IsClosed = false;
            que.ParentNo = RegistrationNo;
            que.StartTime = que.QueDate;
            que.IsStopped = true;
            que.LastUpdateByUserID = AppSession.UserLogin.UserID;
            que.LastUpdateDateTime = serverDate;

            //merge billing
            var mb = new MergeBillingQuery();
            mb.Select(mb.FromRegistrationNo);
            mb.Where(mb.RegistrationNo == RegistrationNo);
            mb.es.Top = 1;
            DataTable dtmb = mb.LoadDataTable();
            billing.FromRegistrationNo = (dtmb.Rows.Count > 0 && dtmb.Rows[0]["FromRegistrationNo"].ToString() != "") ? dtmb.Rows[0]["FromRegistrationNo"].ToString() : RegistrationNo;
            billing.RegistrationNo = txtToRegistrationNo.Text;
            billing.LastUpdateByUserID = AppSession.UserLogin.UserID;
            billing.LastUpdateDateTime = serverDate;

            #region auto bill & visite item (outpatient)

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(entity.GuarantorID);

            var billColl = new ServiceUnitAutoBillItemCollection();
            billColl.Query.Where
                    (
                        billColl.Query.ServiceUnitID == entity.ServiceUnitID,
                        billColl.Query.IsActive == true,
                        billColl.Query.IsGenerateOnReferral == true
                    );

            billColl.LoadAll();

            var parColl = new ParamedicAutoBillItemCollection();
            parColl.Query.Where
                (
                    parColl.Query.ParamedicID == entity.ParamedicID,
                    parColl.Query.ServiceUnitID == entity.ServiceUnitID,
                    parColl.Query.IsActive == true,
                    parColl.Query.IsGenerateOnReferral == true
                );
            parColl.LoadAll();

            foreach (var par in parColl)
            {
                var suabi = billColl.AddNew();
                suabi.ServiceUnitID = string.Empty;
                suabi.ItemID = par.ItemID;
                suabi.Quantity = par.Quantity;

                var item = new ItemService();
                suabi.SRItemUnit = item.LoadByPrimaryKey(suabi.ItemID) ? item.SRItemUnit : "X";

                suabi.IsAutoPayment = false;
                suabi.IsActive = true;
                suabi.IsGenerateOnRegistration = par.IsGenerateOnRegistration;
                suabi.IsGenerateOnNewRegistration = par.IsGenerateOnRegistration;
                suabi.IsGenerateOnReferral = par.IsGenerateOnReferral;
                suabi.LastUpdateDateTime = serverDate;
                suabi.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }

            if (billColl.Count > 0)
            {
                chargesHD.TransactionNo = GetNewTransactionNo();
                _autoNumberTrans.LastCompleteNumber = chargesHD.TransactionNo;
                _autoNumberTrans.Save();

                chargesHD.RegistrationNo = entity.RegistrationNo;
                chargesHD.TransactionDate = entity.RegistrationDate;
                chargesHD.ReferenceNo = string.Empty;
                chargesHD.FromServiceUnitID = entity.ServiceUnitID;
                chargesHD.ToServiceUnitID = entity.ServiceUnitID;
                chargesHD.ClassID = entity.ChargeClassID;
                chargesHD.RoomID = entity.RoomID;
                chargesHD.BedID = string.Empty;
                chargesHD.DueDate = serverDate.Date;
                chargesHD.SRShift = entity.SRShift;
                chargesHD.SRItemType = string.Empty;
                chargesHD.IsProceed = false;
                chargesHD.IsBillProceed = true;
                chargesHD.IsApproved = true;
                chargesHD.IsVoid = false;
                chargesHD.IsOrder = false;
                chargesHD.IsCorrection = false;
                chargesHD.IsClusterAssign = false;
                chargesHD.IsAutoBillTransaction = true;
                chargesHD.Notes = string.Empty;
                chargesHD.LastUpdateByUserID = AppSession.UserLogin.UserID;
                chargesHD.LastUpdateDateTime = serverDate;
                chargesHD.CreatedByUserID = AppSession.UserLogin.UserID;
                chargesHD.CreatedDateTime = serverDate;
                chargesHD.IsPackage = false;
                chargesHD.IsRoomIn = entity.IsRoomIn;
                var room = new ServiceRoom();
                if (room.LoadByPrimaryKey(entity.RoomID))
                    chargesHD.TariffDiscountForRoomIn = room.TariffDiscountForRoomIn;
                else
                    chargesHD.TariffDiscountForRoomIn = 0;
            }

            string seqNo = string.Empty;
            foreach (ServiceUnitAutoBillItem billItem in billColl)
            {
                var item = new Item();
                item.LoadByPrimaryKey(billItem.ItemID);
                string itemTypeHD = item.SRItemType;

                seqNo = TransChargesItemsDT.Count == 0 ? "001" : string.Format("{0:000}", int.Parse(TransChargesItemsDT[TransChargesItemsDT.Count - 1].SequenceNo) + 1);

                ItemTariff tariff =
                    (Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, grr.SRTariffType, chargesHD.ClassID, chargesHD.ClassID,
                                                 billItem.ItemID, reg.GuarantorID, false, entity.SRRegistrationType) ??
                     Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, grr.SRTariffType,
                                                 AppSession.Parameter.DefaultTariffClass, chargesHD.ClassID, billItem.ItemID,
                                                 reg.GuarantorID, false, entity.SRRegistrationType)) ??
                    (Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, AppSession.Parameter.DefaultTariffType,
                                                 chargesHD.ClassID, chargesHD.ClassID, billItem.ItemID, reg.GuarantorID, false, entity.SRRegistrationType) ??
                     Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, AppSession.Parameter.DefaultTariffType,
                                                 AppSession.Parameter.DefaultTariffClass, chargesHD.ClassID, billItem.ItemID,
                                                 reg.GuarantorID, false, entity.SRRegistrationType));

                if (tariff == null) continue; /*tidak ada tarifnya*/

                var chargesDT = TransChargesItemsDT.AddNew();
                chargesDT.TransactionNo = chargesHD.TransactionNo;
                chargesDT.SequenceNo = seqNo;
                chargesDT.ReferenceNo = string.Empty;
                chargesDT.ReferenceSequenceNo = string.Empty;
                chargesDT.ItemID = billItem.ItemID;
                chargesDT.ChargeClassID = entity.ChargeClassID;
                chargesDT.ParamedicID = string.Empty;

                chargesDT.IsAdminCalculation = tariff.IsAdminCalculation ?? false;

                switch (itemTypeHD)
                {
                    case BusinessObject.Reference.ItemType.Service:
                        var service = new ItemService();
                        service.LoadByPrimaryKey(billItem.ItemID);
                        chargesDT.SRItemUnit = service.SRItemUnit;

                        chargesDT.CostPrice = tariff.Price ?? 0;
                        break;
                    case BusinessObject.Reference.ItemType.Medical:
                        var ipm = new ItemProductMedic();
                        ipm.LoadByPrimaryKey(billItem.ItemID);
                        chargesDT.SRItemUnit = ipm.SRItemUnit;

                        chargesDT.CostPrice = ipm.CostPrice ?? 0;
                        break;
                    case BusinessObject.Reference.ItemType.NonMedical:
                        var ipn = new ItemProductNonMedic();
                        ipn.LoadByPrimaryKey(billItem.ItemID);
                        chargesDT.SRItemUnit = ipn.SRItemUnit;

                        chargesDT.CostPrice = ipn.CostPrice ?? 0;
                        break;
                    case BusinessObject.Reference.ItemType.Laboratory:
                    case BusinessObject.Reference.ItemType.Diagnostic:
                    case BusinessObject.Reference.ItemType.Radiology:
                        chargesDT.SRItemUnit = string.Empty;
                        chargesDT.CostPrice = tariff.Price ?? 0;
                        break;
                }

                chargesDT.IsVariable = false;
                chargesDT.IsCito = false;
                chargesDT.IsCitoInPercent = false;
                chargesDT.BasicCitoAmount = (decimal)0D;
                chargesDT.ChargeQuantity = billItem.Quantity;

                if (itemTypeHD == BusinessObject.Reference.ItemType.Medical || itemTypeHD == BusinessObject.Reference.ItemType.NonMedical)
                    chargesDT.StockQuantity = billItem.Quantity;
                else
                    chargesDT.StockQuantity = (decimal)0D;
                var itemRooms = new AppStandardReferenceItemCollection();
                itemRooms.Query.Where(itemRooms.Query.StandardReferenceID == "ItemTariffRoom",
                                      itemRooms.Query.ItemID == billItem.ItemID, itemRooms.Query.IsActive == true);
                itemRooms.LoadAll();
                chargesDT.IsItemRoom = itemRooms.Count > 0;

                chargesDT.Price = tariff.Price ?? 0;
                if (chargesDT.IsItemRoom == true && chargesHD.IsRoomIn == true)
                    chargesDT.Price = chargesDT.Price - (chargesDT.Price * chargesHD.TariffDiscountForRoomIn / 100);

                chargesDT.DiscountAmount = (decimal)0D;
                chargesDT.CitoAmount = (decimal)0D;
                chargesDT.RoundingAmount = Helper.RoundingDiff;
                chargesDT.SRDiscountReason = string.Empty;
                chargesDT.IsAssetUtilization = false;
                chargesDT.AssetID = string.Empty;
                chargesDT.IsBillProceed = true;
                chargesDT.IsOrderRealization = false;
                chargesDT.IsPackage = false;
                chargesDT.IsApprove = true;
                chargesDT.IsVoid = false;
                chargesDT.LastUpdateByUserID = AppSession.UserLogin.UserID;
                chargesDT.LastUpdateDateTime = serverDate;
                chargesDT.CreatedByUserID = AppSession.UserLogin.UserID;
                chargesDT.CreatedDateTime = serverDate;
                chargesDT.ParentNo = string.Empty;
                chargesDT.SRCenterID = string.Empty;
                chargesDT.ItemConditionRuleID = string.Empty;

                if (Helper.GuarantorBpjsCasemix.Contains(entity.GuarantorID) && AppSession.Parameter.CasemixValidationRegistrationType.Contains(entity.SRRegistrationType))
                    chargesDT.IsCasemixApproved = Helper.IsCasemixApproved(chargesDT.ItemID, chargesDT.ChargeQuantity ?? 0, entity.RegistrationNo, chargesDT.TransactionNo, entity.GuarantorID, false);
                else
                    chargesDT.IsCasemixApproved = true;

                chargesDT.IsBillProceed = chargesDT.IsCasemixApproved;
                chargesDT.IsApprove = chargesDT.IsCasemixApproved;

                if ((chargesHD.IsBillProceed ?? false) && !(chargesDT.IsBillProceed ?? false))
                {
                    chargesHD.IsBillProceed = false;
                    chargesHD.IsApproved = false;
                }

                #region item component

                var compQuery = new ItemTariffComponentQuery();
                compQuery.es.Top = 1;
                compQuery.Where
                    (
                        compQuery.SRTariffType == grr.SRTariffType,
                        compQuery.ItemID == billItem.ItemID,
                        compQuery.ClassID == entity.ChargeClassID,
                        compQuery.StartingDate <= serverDate.Date
                    );

                var compColl = Helper.Tariff.GetItemTariffComponentCollection(chargesHD.TransactionDate.Value,
                                                                              grr.SRTariffType, chargesHD.ClassID,
                                                                              chargesDT.ItemID);
                if (!compColl.Any())
                    compColl = Helper.Tariff.GetItemTariffComponentCollection(chargesHD.TransactionDate.Value,
                                                                              grr.SRTariffType,
                                                                              AppSession.Parameter.DefaultTariffClass,
                                                                              chargesDT.ItemID);
                if (!compColl.Any())
                    compColl = Helper.Tariff.GetItemTariffComponentCollection(chargesHD.TransactionDate.Value,
                                                                              AppSession.Parameter.DefaultTariffType,
                                                                              chargesHD.ClassID, chargesDT.ItemID);
                if (!compColl.Any())
                    compColl = Helper.Tariff.GetItemTariffComponentCollection(chargesHD.TransactionDate.Value,
                                                                              AppSession.Parameter.DefaultTariffType,
                                                                              AppSession.Parameter.DefaultTariffClass,
                                                                              chargesDT.ItemID);

                var p = string.Empty;
                foreach (var comp in compColl)
                {
                    var compCharges = TransChargesItemsDTComp.AddNew();
                    compCharges.TransactionNo = chargesHD.TransactionNo;
                    compCharges.SequenceNo = seqNo;
                    compCharges.TariffComponentID = comp.TariffComponentID;
                    if (chargesHD.IsRoomIn == true && chargesDT.IsItemRoom == true)
                        compCharges.Price = comp.Price - (comp.Price * chargesHD.TariffDiscountForRoomIn / 100);
                    else
                        compCharges.Price = comp.Price;
                    compCharges.DiscountAmount = (decimal)0D;
                    compCharges.CitoAmount = (decimal)0D;

                    var tComp = new TariffComponent();
                    tComp.LoadByPrimaryKey(comp.TariffComponentID);
                    if (tComp.IsTariffParamedic ?? false)
                        compCharges.ParamedicID = entity.ParamedicID;
                    else
                        compCharges.ParamedicID = string.Empty;

                    compCharges.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    compCharges.LastUpdateDateTime = serverDate;

                    if (!string.IsNullOrEmpty(compCharges.ParamedicID))
                    {
                        if (tComp.IsPrintParamedicInSlip ?? false)
                        {
                            var par = new Paramedic();
                            par.LoadByPrimaryKey(compCharges.ParamedicID);
                            if (par.IsPrintInSlip ?? true)
                            {
                                p = p.Length == 0 ? par.ParamedicName : p + "; " + par.ParamedicName;
                            }
                        }
                    }
                }
                chargesDT.ParamedicCollectionName = p;

                #endregion

                #region Item Consumption

                var consQuery = new ItemConsumptionQuery();
                consQuery.Where(consQuery.ItemID == billItem.ItemID);

                var consColl = new ItemConsumptionCollection();
                consColl.Load(consQuery);

                foreach (var cons in consColl)
                {
                    var consCharges = TransChargesItemsDTConsumption.AddNew();
                    consCharges.TransactionNo = chargesHD.TransactionNo;
                    consCharges.SequenceNo = seqNo;
                    consCharges.DetailItemID = cons.ItemID;
                    consCharges.Qty = cons.Qty;
                    consCharges.SRItemUnit = cons.SRItemUnit;
                    consCharges.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    consCharges.LastUpdateDateTime = serverDate;
                }

                #endregion
            }

            chargesHD.IsApproved = chargesHD.IsBillProceed;

            #region auto calculation

            if (TransChargesItemsDT.Count > 0)
            {
                var grrID = entity.GuarantorID;
                if (grrID == AppSession.Parameter.SelfGuarantor)
                {
                    var pat = new Patient();
                    pat.LoadByPrimaryKey(entity.PatientID);
                    if (!string.IsNullOrEmpty(pat.MemberID))
                        grrID = pat.MemberID;
                }

                DataTable tblCovered = Helper.GetCoveredItems(entity, entity.SRBussinesMethod, 0, false, entity.ChargeClassID, entity.CoverageClassID,
                    grrID, TransChargesItemsDT.Select(t => t.ItemID).ToArray(), entity.RegistrationDate.Value, RegistrationItemRules, false);

                foreach (TransChargesItem detail in TransChargesItemsDT)
                {
                    var rowCovered = tblCovered.AsEnumerable().Where(t => t.Field<string>("ItemID") == detail.ItemID &&
                                                                          t.Field<bool>("IsInclude")).SingleOrDefault();

                    //TransChargesItemComps
                    if (rowCovered != null)
                    {
                        decimal? discount = 0;
                        bool isDiscount = false, isMargin = false;
                        foreach (var comp in TransChargesItemsDTComp.Where(t => t.TransactionNo == detail.TransactionNo &&
                                                                                t.SequenceNo == detail.SequenceNo)
                                                                    .OrderBy(t => t.TariffComponentID))
                        {
                            var amountValue = (decimal?)rowCovered["AmountValue"];
                            var basicPrice = (decimal?)rowCovered["BasicPrice"];
                            var coveragePrice = (decimal?)rowCovered["CoveragePrice"];

                            if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                            {
                                if ((comp.Price - comp.DiscountAmount) <= 0)
                                    continue;

                                var compPrice = comp.Price;
                                if (basicPrice > coveragePrice)
                                {
                                    var tcomp = Helper.Tariff.GetItemTariffComponent(chargesHD.TransactionDate.Value, grr.SRTariffType,
                                        entity.CoverageClassID, comp.TariffComponentID, detail.ItemID);
                                    if (!tcomp.AsEnumerable().Any())
                                        tcomp = Helper.Tariff.GetItemTariffComponent(chargesHD.TransactionDate.Value, grr.SRTariffType,
                                            AppSession.Parameter.DefaultTariffClass, comp.TariffComponentID, detail.ItemID);
                                    if (!tcomp.AsEnumerable().Any())
                                        tcomp = Helper.Tariff.GetItemTariffComponent(chargesHD.TransactionDate.Value, AppSession.Parameter.DefaultTariffType,
                                            entity.CoverageClassID, comp.TariffComponentID, detail.ItemID);
                                    if (!tcomp.AsEnumerable().Any())
                                        tcomp = Helper.Tariff.GetItemTariffComponent(chargesHD.TransactionDate.Value, AppSession.Parameter.DefaultTariffType,
                                            AppSession.Parameter.DefaultTariffClass, comp.TariffComponentID, detail.ItemID);

                                    if (!tcomp.AsEnumerable().Any())
                                        continue;

                                    compPrice = tcomp.AsEnumerable().Select(c => c.Field<decimal>("Price")).Single();
                                }

                                if ((bool)rowCovered["IsValueInPercent"])
                                {
                                    comp.DiscountAmount += (amountValue / 100) * compPrice;
                                    comp.AutoProcessCalculation = 0 - (amountValue / 100) * compPrice;
                                }
                                else
                                {
                                    if (!isDiscount)
                                    {
                                        if (discount == 0)
                                        {
                                            if (comp.Price >= amountValue)
                                            {
                                                comp.DiscountAmount += amountValue;
                                                comp.AutoProcessCalculation = 0 - amountValue;
                                                isDiscount = true;
                                            }
                                            else
                                            {
                                                comp.DiscountAmount += compPrice;
                                                comp.AutoProcessCalculation = 0 - compPrice;
                                                discount = amountValue - compPrice;
                                            }
                                        }
                                        else
                                        {
                                            if (comp.Price >= discount)
                                            {
                                                comp.DiscountAmount += discount;
                                                comp.AutoProcessCalculation = 0 - discount;
                                                isDiscount = true;
                                            }
                                            else
                                            {
                                                comp.DiscountAmount += compPrice;
                                                comp.AutoProcessCalculation = 0 - compPrice;
                                                discount -= comp.Price;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                            {
                                if ((bool)rowCovered["IsValueInPercent"])
                                {
                                    comp.Price += (amountValue / 100) * comp.Price;
                                    comp.AutoProcessCalculation = (amountValue / 100) * comp.Price;
                                }
                                else
                                {
                                    if (!isMargin)
                                    {
                                        comp.Price += amountValue;
                                        comp.AutoProcessCalculation = amountValue;
                                        isMargin = true;
                                    }
                                }
                            }
                        }
                    }

                    //TransChargesItems
                    if (TransChargesItemsDTComp.Count > 0)
                    {
                        detail.AutoProcessCalculation = TransChargesItemsDTComp.Where(t => t.TransactionNo == detail.TransactionNo &&
                                                                                           t.SequenceNo == detail.SequenceNo)
                                                                               .Sum(t => t.AutoProcessCalculation);
                        if (detail.AutoProcessCalculation < 0)
                        {
                            detail.DiscountAmount += detail.ChargeQuantity * Math.Abs(detail.AutoProcessCalculation ?? 0);

                            if (detail.DiscountAmount > detail.Price)
                            {
                                detail.DiscountAmount = detail.Price;
                                detail.AutoProcessCalculation = 0 - detail.Price;
                            }
                        }
                        else if (detail.AutoProcessCalculation > 0)
                            detail.Price += detail.AutoProcessCalculation;
                    }
                    else
                    {
                        if (rowCovered != null)
                        {
                            if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                            {
                                var basicPrice = (decimal?)rowCovered["BasicPrice"];
                                var coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                var detailPrice = detail.Price ?? 0;
                                if (basicPrice > coveragePrice)
                                {
                                    ItemTariff tariff = (Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, grr.SRTariffType, entity.CoverageClassID, entity.CoverageClassID, detail.ItemID, entity.GuarantorID, false, entity.SRRegistrationType) ??
                                             Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, entity.CoverageClassID, detail.ItemID, entity.GuarantorID, false, entity.SRRegistrationType)) ??
                                            (Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, entity.CoverageClassID, entity.CoverageClassID, detail.ItemID, entity.GuarantorID, false, entity.SRRegistrationType) ??
                                             Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, entity.CoverageClassID, detail.ItemID, entity.GuarantorID, false, entity.SRRegistrationType));
                                    if (tariff != null)
                                        detailPrice = tariff.Price ?? 0;
                                }

                                if ((bool)rowCovered["IsValueInPercent"])
                                    detail.DiscountAmount += (detail.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * detailPrice);
                                else
                                    detail.DiscountAmount += (detail.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];

                                if (detail.DiscountAmount > detailPrice)
                                    detail.DiscountAmount = detailPrice;

                                detail.AutoProcessCalculation = 0 - detail.DiscountAmount;
                            }
                            else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                            {
                                if ((bool)rowCovered["IsValueInPercent"])
                                    detail.Price += ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;
                                else
                                    detail.Price += (decimal)rowCovered["AmountValue"];

                                detail.AutoProcessCalculation = detail.Price;
                            }
                        }
                    }

                    //cost calculation hanya dihitung jika sudah melewati proses dari casemix
                    //dimana ditandai dg chargesHD.IsBillProceed = true
                    if (chargesHD.IsBillProceed ?? false)
                    {
                        //post
                        decimal? total = ((detail.ChargeQuantity * detail.Price) - detail.DiscountAmount) + detail.CitoAmount;
                        var calc = new Helper.CostCalculation(grrID, detail.ItemID, total ?? 0, tblCovered, detail.ChargeQuantity ?? 0,
                                                                      detail.IsCito ?? false,
                                                                      detail.IsCitoInPercent ?? false,
                                                                      detail.BasicCitoAmount ?? 0, detail.Price ?? 0,
                                                                      chargesHD.IsRoomIn ?? false, detail.IsItemRoom ?? false,
                                                                      chargesHD.TariffDiscountForRoomIn ?? 0, detail.DiscountAmount ?? 0, false,
                                                                      detail.ItemConditionRuleID, chargesHD.TransactionDate.Value, detail.IsVariable ?? false);

                        CostCalculation cost = CostCalculations.AddNew();
                        cost.RegistrationNo = entity.RegistrationNo;
                        cost.TransactionNo = detail.TransactionNo;
                        cost.SequenceNo = detail.SequenceNo;
                        cost.ItemID = detail.ItemID;
                        cost.PatientAmount = calc.PatientAmount;
                        cost.GuarantorAmount = calc.GuarantorAmount;
                        cost.DiscountAmount = detail.DiscountAmount;
                        cost.IsPackage = detail.IsPackage;
                        cost.ParentNo = detail.ParentNo;
                        cost.ParamedicAmount = detail.ChargeQuantity * TransChargesItemsDTComp.Where(comp => comp.TransactionNo == detail.TransactionNo &&
                                                                                                             comp.SequenceNo == detail.SequenceNo &&
                                                                                                             !string.IsNullOrEmpty(comp.ParamedicID))
                                                                                              .Sum(comp => comp.Price - comp.DiscountAmount);
                        cost.LastUpdateDateTime = serverDate;
                        cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                }
            }

            #endregion

            entity.RemainingAmount = CostCalculations.Sum(c => (c.PatientAmount + c.GuarantorAmount));
            reg = new Registration();
            entity.IsNewVisit = !reg.Load(query);

            #endregion
        }

        //private void SaveNewAppointment()
        //{
        //    var appt = new BusinessObject.Appointment();

        //    var autoNumber = GetNewAppointmentNo(txtReferDate.SelectedDate.Value.Date);
        //    appt.AppointmentNo = autoNumber.LastCompleteNumber;
        //    autoNumber.Save();

        //    var sch = new ParamedicScheduleDate();
        //    if (sch.LoadByPrimaryKey(cboReferServiceUnitID.SelectedValue, cboReferParamedicID.SelectedValue,
        //                             txtReferDate.SelectedDate.Value.Year.ToString(), txtReferDate.SelectedDate.Value.Date))
        //    {
        //        if (isUsingQue)
        //            appt.AppointmentQue = int.Parse(cboReferQue.Text.Split('-')[0].Trim());
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "required", "alert('Que Slot Number is required.');", true);
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "required", "alert('Physician schedule is not available.');", true);
        //        return;
        //    }

        //    appt.ServiceUnitID = cboReferServiceUnitID.SelectedValue;
        //    appt.ParamedicID = cboReferParamedicID.SelectedValue;
        //    appt.PatientID = PatientID;
        //    appt.AppointmentDate = txtReferDate.SelectedDate.Value.Date;

        //    string value = cboReferQue.Text.Split('-')[1].Substring(1);
        //    DateTime dt;
        //    DateTime.TryParse(value, out dt);
        //    appt.AppointmentTime = dt.ToString("HH:mm");

        //    appt.VisitTypeID = string.Empty;

        //    var sc = new ParamedicSchedule();
        //    sc.LoadByPrimaryKey(cboReferServiceUnitID.SelectedValue, cboReferParamedicID.SelectedValue, txtReferDate.SelectedDate.Value.Year.ToString());
        //    appt.VisitDuration = Convert.ToByte(sc.ExamDuration ?? 0);

        //    appt.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusOpen;

        //    var pat = new Patient();
        //    pat.LoadByPrimaryKey(appt.PatientID);

        //    appt.FirstName = pat.FirstName;
        //    appt.MiddleName = pat.MiddleName;
        //    appt.LastName = pat.LastName;
        //    appt.DateOfBirth = pat.DateOfBirth;
        //    appt.GuarantorID = pat.GuarantorID;
        //    appt.Notes = txtReferNotes.Text;
        //    appt.StreetName = pat.StreetName;
        //    appt.District = pat.District;
        //    appt.City = pat.City;
        //    appt.County = pat.County;
        //    appt.State = pat.State;
        //    appt.str.ZipCode = pat.ZipCode ?? string.Empty;
        //    appt.PhoneNo = pat.PhoneNo;
        //    appt.MobilePhoneNo = pat.MobilePhoneNo;
        //    appt.Email = pat.Email;
        //    appt.FaxNo = pat.FaxNo;
        //    appt.FromRegistrationNo = RegistrationNo;
        //}

        private void SaveNewRegistration(Registration entity, ServiceUnitQue que, MergeBilling billing, TransCharges chargesHD, TransChargesItemCollection chargesDT, TransChargesItemCompCollection compDT,
            TransChargesItemConsumptionCollection consDT, CostCalculationCollection cost, out string itemNoStock)
        {
            itemNoStock = string.Empty;

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(entity.ServiceUnitID);

            //auto number
            _autoNumber.Save();

            entity.Save();

            //service unit que
            que.Save();

            //billing
            billing.Save();

            if (chargesDT.Count > 0 && Request.QueryString["type"] != "mcu")
            {
                chargesHD.Save();

                // stock calculation
                // charges
                var chargesBalances = new ItemBalanceCollection();
                var chargesDetailBalances = new ItemBalanceDetailCollection();
                var chargesDetailBalanceEds = new ItemBalanceDetailEdCollection();
                var chargesMovements = new ItemMovementCollection();

                ItemBalance.PrepareItemBalances(chargesDT, unit.ServiceUnitID, unit.GetMainLocationId(), AppSession.UserLogin.UserID,
                    true, ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, ref chargesDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl, out itemNoStock);

                chargesDT.Save();
                compDT.Save();
                cost.Save();

                if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                {
                    // extract fee
                    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    feeColl.SetFeeByTCIC(compDT, AppSession.UserLogin.UserID);
                    feeColl.Save();
                    //feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                    //feeColl.Save();
                }

                if (chargesBalances != null)
                    chargesBalances.Save();
                if (chargesDetailBalances != null)
                    chargesDetailBalances.Save();
                if (chargesDetailBalanceEds != null)
                    chargesDetailBalanceEds.Save();
                if (chargesMovements != null)
                    chargesMovements.Save();

                // consumption
                var consumptionBalances = new ItemBalanceCollection();
                var consumptionDetailBalances = new ItemBalanceDetailCollection();
                var consumptionDetailBalanceEds = new ItemBalanceDetailEdCollection();
                var consumptionMovements = new ItemMovementCollection();

                ItemBalance.PrepareItemBalances(consDT, unit.ServiceUnitID, unit.GetMainLocationId(), AppSession.UserLogin.UserID,
                    ref consumptionBalances, ref consumptionDetailBalances, ref consumptionMovements, ref consumptionDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl, out itemNoStock);

                if (!string.IsNullOrEmpty(itemNoStock))
                    return;

                consDT.Save();

                if (consumptionBalances != null)
                    consumptionBalances.Save();
                if (consumptionDetailBalances != null)
                    consumptionDetailBalances.Save();
                if (consumptionDetailBalanceEds != null)
                    consumptionDetailBalanceEds.Save();
                if (consumptionMovements != null)
                    consumptionMovements.Save();

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                {
                    if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                    {
                        JournalTransactions.AddNewPatientIncomeAccrual(BusinessObject.JournalType.Income, chargesHD.TransactionNo, AppSession.UserLogin.UserID, 0);
                    }
                    else
                    {
                        int? journalId = JournalTransactions.AddNewIncomeJournal(chargesHD, compDT, entity, unit, cost,
                                                                                 "SU", AppSession.UserLogin.UserID, 0);
                    }
                }
            }

        }

        public override void OnServerValidate(ValidateArgs args)
        {

        }

        private Registration SaveNewRegistration(ValidateArgs args)
        {
            var newReg = new Registration();
            var que = new ServiceUnitQue();
            var billing = new MergeBilling();
            var chargesHD = new TransCharges();

            SaveNewRegistration(newReg, que, billing, chargesHD);
            string itemNoStock;
            SaveNewRegistration(newReg, que, billing, chargesHD, TransChargesItemsDT, TransChargesItemsDTComp,
                       TransChargesItemsDTConsumption, CostCalculations, out itemNoStock);


            if (!string.IsNullOrEmpty(itemNoStock))
            {
                // Hanya konfirmasi
                args.MessageText = "Insufficient balance of item : " + itemNoStock;
                args.IsCancel = false;
            }

            return newReg;
        }
        #endregion

        protected void optConsultReferType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyConsultReferType(optConsultReferType.SelectedValue);
        }

        private void ApplyConsultReferType(string consultReferType)
        {
            var isRefer = consultReferType == "R";
            trReferQueNo.Visible = isRefer;
            trReferRoom.Visible = isRefer;
            trReferServiceUnit.Visible = isRefer;
            trReferToRegistrationNo.Visible = isRefer;
            cboReferToParamedicID.Visible = isRefer;
            cboConsultToParamedicID.Visible = !isRefer;
            lblConsultType.Text = isRefer ? "Refer Type" : "Consult Type";

            cboConsultToParamedicID.Items.Clear();
            cboConsultToParamedicID.Text = string.Empty;

            cboReferToParamedicID.Items.Clear();
            cboReferToParamedicID.Text = string.Empty;

        }

        #region Que No

        private void ApplyParamedicID(string paramedicID)
        {
            if (paramedicID == string.Empty)
            {
                cboQue.DataSource = null;
                cboQue.DataTextField = "Subject";
                cboQue.DataValueField = "Subject";
                cboQue.DataBind();
                return;
            }

            var rooms = new ServiceRoomCollection();
            rooms.Query.Where(
                rooms.Query.RoomID.In(cboRoomID.Items.Select(c => c.Value)),
                rooms.Query.ParamedicID1 == paramedicID
                );
            rooms.LoadAll();

            if (rooms.Count == 1) cboRoomID.SelectedValue = rooms[0].RoomID;

            if (rooms.Count != 1)
            {
                // cari default room untuk Service Unit dan Dokter yang bersangkutan
                var supC = new ServiceUnitParamedicCollection();
                supC.Query.Where(supC.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                    supC.Query.ParamedicID == paramedicID);
                supC.LoadAll();
                cboRoomID.SelectedValue = supC.Count > 0 ? supC[0].DefaultRoomID : string.Empty;
            }

            var sp = new ServiceUnitParamedic();
            if (sp.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboReferToParamedicID.SelectedValue))
            {
                if (sp.IsUsingQue ?? false)
                {
                    cboQue.DataSource = AppointmentSlotTime(cboServiceUnitID.SelectedValue, cboReferToParamedicID.SelectedValue,
                                                            txtConsultDate.SelectedDate.Value.Date);
                    cboQue.DataTextField = "Subject";
                    cboQue.DataValueField = "Subject";
                    cboQue.DataBind();
                }
                else
                {
                    cboQue.DataSource = null;
                    cboQue.DataTextField = "Subject";
                    cboQue.DataValueField = "Subject";
                    cboQue.DataBind();
                }
            }
            else
            {
                cboQue.DataSource = null;
                cboQue.DataTextField = "Subject";
                cboQue.DataValueField = "Subject";
                cboQue.DataBind();
            }

        }
        private DataTable AppointmentSlotTime(string serviceUnitID, string paramedicID, DateTime date)
        {
            var serverDate = (new DateTime()).NowAtSqlServer();

            var dtb = new DataTable("AppointmentSlotTime");

            //column
            var dc = new DataColumn("SlotNo", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("Start", Type.GetType("System.DateTime"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("End", Type.GetType("System.DateTime"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("Subject", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("Description", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("OperationalStart", Type.GetType("System.DateTime"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("OperationalEnd", Type.GetType("System.DateTime"));
            dtb.Columns.Add(dc);

            if (!string.IsNullOrEmpty(serviceUnitID) && !string.IsNullOrEmpty(paramedicID))
            {
                DataRow r = dtb.NewRow();
                r[0] = 0;
                r[1] = serverDate;
                r[2] = serverDate;
                r[3] = string.Empty;
                r[4] = string.Empty;
                r[5] = serverDate;
                r[6] = serverDate;
                dtb.Rows.Add(r);

                var sch = new ParamedicScheduleDateQuery("a");
                var ot = new OperationalTimeQuery("b");
                var par = new ParamedicScheduleQuery("c");

                sch.Select(
                    sch.ScheduleDate,
                    ot.StartTime1,
                    ot.EndTime1,
                    ot.StartTime2,
                    ot.EndTime2,
                    ot.StartTime3,
                    ot.EndTime3,
                    ot.StartTime4,
                    ot.EndTime4,
                    ot.StartTime5,
                    ot.EndTime5,
                    par.ExamDuration
                    );
                sch.InnerJoin(ot).On(sch.OperationalTimeID == ot.OperationalTimeID);
                sch.InnerJoin(par).On(
                    sch.ServiceUnitID == par.ServiceUnitID &&
                    sch.ParamedicID == par.ParamedicID &&
                    sch.PeriodYear == par.PeriodYear
                    );
                sch.Where(
                    sch.ServiceUnitID == serviceUnitID,
                    sch.ParamedicID == paramedicID,
                    sch.PeriodYear == date.Year,
                    sch.ScheduleDate == date
                    );
                var list = sch.LoadDataTable();

                double duration = 0;
                if (list.Rows.Count > 0)
                    duration = Convert.ToDouble(list.Rows[0][11]);

                foreach (DataRow row in list.Rows)
                {
                    //time 1
                    if (row[1].ToString().Trim() != string.Empty && row[2].ToString().Trim() != string.Empty)
                    {
                        var i = 1;
                        var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[1].ToString().Trim());
                        var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[2].ToString().Trim());
                        while (dt1 < dt2)
                        {
                            DataRow dr = dtb.NewRow();
                            dr[0] = i;
                            dr[1] = dt1;
                            dr[2] = dt1.AddMinutes(duration);
                            dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                            dr[4] = string.Empty;
                            dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[1].ToString().Trim());
                            dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[2].ToString().Trim());
                            dtb.Rows.Add(dr);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                    }
                    //time 2
                    if (row[3].ToString().Trim() != string.Empty && row[4].ToString().Trim() != string.Empty)
                    {
                        var i = 1;
                        var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[3].ToString().Trim());
                        var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[4].ToString().Trim());
                        while (dt1 < dt2)
                        {
                            DataRow dr = dtb.NewRow();
                            dr[0] = i;
                            dr[1] = dt1;
                            dr[2] = dt1.AddMinutes(duration);
                            dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                            dr[4] = string.Empty;
                            dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[3].ToString().Trim());
                            dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[4].ToString().Trim());
                            dtb.Rows.Add(dr);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                    }
                    //time 3
                    if (row[5].ToString().Trim() != string.Empty && row[6].ToString().Trim() != string.Empty)
                    {
                        var i = 1;
                        var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[5].ToString().Trim());
                        var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[6].ToString().Trim());
                        while (dt1 < dt2)
                        {
                            DataRow dr = dtb.NewRow();
                            dr[0] = i;
                            dr[1] = dt1;
                            dr[2] = dt1.AddMinutes(duration);
                            dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                            dr[4] = string.Empty;
                            dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[5].ToString().Trim());
                            dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[6].ToString().Trim());
                            dtb.Rows.Add(dr);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                    }
                    //time 4
                    if (row[7].ToString().Trim() != string.Empty && row[8].ToString().Trim() != string.Empty)
                    {
                        var i = 1;
                        var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[7].ToString().Trim());
                        var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[8].ToString().Trim());
                        while (dt1 < dt2)
                        {
                            DataRow dr = dtb.NewRow();
                            dr[0] = i;
                            dr[1] = dt1;
                            dr[2] = dt1.AddMinutes(duration);
                            dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                            dr[4] = string.Empty;
                            dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[7].ToString().Trim());
                            dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[8].ToString().Trim());
                            dtb.Rows.Add(dr);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                    }
                    //time 5
                    if (row[9].ToString().Trim() != string.Empty && row[10].ToString().Trim() != string.Empty)
                    {
                        var i = 1;
                        var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[9].ToString().Trim());
                        var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[10].ToString().Trim());
                        while (dt1 < dt2)
                        {
                            DataRow dr = dtb.NewRow();
                            dr[0] = i;
                            dr[1] = dt1;
                            dr[2] = dt1.AddMinutes(duration);
                            dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                            dr[4] = string.Empty;
                            dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[9].ToString().Trim());
                            dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[10].ToString().Trim());
                            dtb.Rows.Add(dr);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                    }
                }

                var appt = AppointmentList(serviceUnitID, paramedicID);

                foreach (DataRow slot in dtb.Rows)
                {
                    foreach (var entity in from entity in appt let dateTime = entity.AppointmentDate.Value.Date + TimeSpan.Parse(entity.AppointmentTime) where Convert.ToDateTime(slot[1]) == dateTime select entity)
                    {
                        slot[0] = entity.AppointmentNo;
                        slot[3] = entity.AppointmentQue + " - " + entity.AppointmentTime + " - " + entity.GetColumn("PatientName").ToString() + " [A]";
                        break;
                    }
                }

                dtb.AcceptChanges();

                var regs = new RegistrationCollection();

                var query = new RegistrationQuery("a");
                var pq = new PatientQuery("b");

                query.Select(
                    query,
                    pq.PatientName
                    );
                query.InnerJoin(pq).On(query.PatientID == pq.PatientID);
                query.Where(
                    query.RegistrationDate == date,
                    query.ServiceUnitID == serviceUnitID,
                    query.ParamedicID == paramedicID,
                    query.IsVoid == false
                    );
                regs.Load(query);

                foreach (var reg in regs)
                {
                    DateTime dateTime = reg.RegistrationDate.Value.Date + TimeSpan.Parse(reg.RegistrationTime);

                    var slot = dtb.AsEnumerable().SingleOrDefault(d => d.Field<string>("SlotNo") == reg.RegistrationQue.ToString() &&
                                                                       d.Field<DateTime>("Start") == dateTime);

                    if (slot != null)
                    {
                        slot[0] = reg.RegistrationNo;
                        slot[3] = slot[3].ToString().Split('-')[0] + "- " + reg.RegistrationTime + " - " + reg.GetColumn("PatientName");
                    }
                }

                dtb.AcceptChanges();
            }
            return dtb;
        }
        private BusinessObject.AppointmentCollection AppointmentList(string serviceUnitID, string paramedicID)
        {
            var query = new AppointmentQuery("a");
            var unit = new ServiceUnitQuery("b");
            var medic = new ParamedicQuery("c");
            var patient = new PatientQuery("e");

            query.Select(
                query.AppointmentNo,
                query.AppointmentQue,
                query.AppointmentDate,
                query.AppointmentTime,
                query.PatientName,
                (medic.ParamedicName + "<br />" + unit.ServiceUnitName + "<br />" + query.Notes).As("Description"),
                query.SRAppointmentStatus
            );
            query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
            query.InnerJoin(medic).On(query.ParamedicID == medic.ParamedicID);
            query.LeftJoin(patient).On(query.PatientID == patient.PatientID);

            if (!string.IsNullOrEmpty(serviceUnitID))
                query.Where(query.ServiceUnitID == serviceUnitID);

            if (!string.IsNullOrEmpty(paramedicID))
                query.Where(query.ParamedicID == paramedicID);

            query.Where(
                query.AppointmentDate == txtConsultDate.SelectedDate.Value,
                query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
            );

            var coll = new BusinessObject.AppointmentCollection();
            coll.Load(query);

            return coll;
        }
        #endregion
    }
}
