using System;
using System.Collections.Generic;
using System.Linq;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Telerik.Web.UI.Calendar;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl;
using Temiang.Dal.DynamicQuery;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class IntegratedNoteEntry : BasePageDialogEntry
    {
        public string ServiceUnitID
        {
            get
            {
                return Request.QueryString["unit"];
            }
        }
        public string RegistrationInfoMedicID
        {
            get
            {
                if (!IsPostBack)
                {
                    hdnRegistrationInfoMedicID.Value = Request.QueryString["rimid"];
                }
                return hdnRegistrationInfoMedicID.Value;
            }
            set
            {
                hdnRegistrationInfoMedicID.Value = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!IsPostBack)
            {
                // Tampilkan consul / refer terakhir
                // Consul / refer sebelumnya diisi di page Consul / refer
                // Jawab consul harus oleh dokter bersangkutan jadi amannya pakai AppSession.UserLogin.ParamedicID
                var consult =
                    ParamedicConsultRefer.LastConsultReferTo(MergeRegistrations, AppSession.UserLogin.ParamedicID);
                if (consult != null)
                {
                    AddReferAnswerCtl();
                }
            }
            else
            {
                List<string> keys = Request.Form.AllKeys.Where(key => key.Contains("referAnswerCtl")).ToList();
                foreach (string key in keys)
                {
                    AddReferAnswerCtl();
                    break; // hanya buat 1 ctl
                }
            }
        }

        private void AddReferAnswerCtl()
        {
            var referAnswerCtl =
                (ConsultReferAnswerCtl)
                    Page.LoadControl("~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/ConsultReferAnswerCtl.ascx");
            referAnswerCtl.Width = "100%";
            referAnswerCtl.ID = "referAnswerCtl";
            pnlReferAnswer.Controls.Add(referAnswerCtl);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            IsMedicalRecordEntry = true; //Activate deadline edit & add
            ToolBar.AutoSaveVisible = true;
            ToolBar.SaveAndEditVisible = true;
            IsSingleRecordMode = true; //Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;

            ToolBar.EditVisible = false;
            ToolBar.AddVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Progress Notes of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
                hdnRegistrationInfoMedicID.Value = Request.QueryString["rimid"];
            }

            //ICD 9CM Entry
            trIcd9cm.Visible = AppParameter.IsYes(AppParameter.ParameterItem.IsShowIcd9cmInProgressNoteEntry);
            curbPanel.Visible = AppParameter.GetParameterValue(AppParameter.ParameterItem.IsShowCurb65ScoreInAssesmentAndMDS).Equals("Yes", StringComparison.OrdinalIgnoreCase);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboInputType.Items.Add(new RadComboBoxItem("SOAP", "SOAP"));
                cboInputType.Items.Add(new RadComboBoxItem("SBAR", "SBAR"));
                cboInputType.Items.Add(new RadComboBoxItem("ADIME", "ADIME"));
                cboInputType.Items.Add(new RadComboBoxItem("Notes", "Notes"));
            }
        }

        #region override method
        public override void OnServerValidate(ValidateArgs args)
        {
            //if (string.IsNullOrWhiteSpace(txtInfo1.Text) || txtInfo1.Text.Trim().Length<26)
            //{
            //    args.IsCancel = true;
            //    args.MessageText = string.Format("{0} must be at least 25 characters ", lblInfo1.Text);
            //    return;
            //}
            //if (string.IsNullOrWhiteSpace(txtInfo2.Text) || txtInfo2.Text.Trim().Length < 26)
            //{
            //    args.IsCancel = true;
            //    args.MessageText = string.Format("{0} must be at least 25 characters ", lblInfo2.Text);
            //    return;
            //}
            //if (string.IsNullOrWhiteSpace(txtInfo3.Text) || txtInfo3.Text.Trim().Length < 26)
            //{
            //    args.IsCancel = true;
            //    args.MessageText = string.Format("{0} must be at least 25 characters ", lblInfo3.Text);
            //    return;
            //}
            //if (string.IsNullOrWhiteSpace(txtInfo4.Text) || txtInfo4.Text.Trim().Length < 26)
            //{
            //    args.IsCancel = true;
            //    args.MessageText = string.Format("{0} must be at least 25 characters ", lblInfo4.Text);
            //    return;
            //}
        }
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            PopulateRefer();
            // Hanya tuk single entry
            var infMedic = new RegistrationInfoMedic();
            if (infMedic.LoadByPrimaryKey(RegistrationInfoMedicID))
            {
                hdnServiceUnitID.Value = infMedic.ServiceUnitID;

                ComboBox.PopulateWithOneParamedic(cboParamedicID, infMedic.ParamedicID);
                txtDateSOAP.SelectedDate = infMedic.DateTimeInfo;
                txtTimeSOAP.SelectedDate = Convert.ToDateTime(infMedic.DateTimeInfo);
                //txtInfo1.Text = string.IsNullOrWhiteSpace(infMedic.Info1Entry) ? infMedic.Info1 : infMedic.Info1Entry;
                txtInfo1.Text = infMedic.Info1Entry;
                txtInfo2.Text = infMedic.Info2;
                //txtInfo3.Text = string.IsNullOrWhiteSpace(infMedic.Info3Entry) ? infMedic.Info3 : infMedic.Info3Entry;
                txtInfo3.Text = infMedic.Info3Entry;
                txtInfo4.Text = infMedic.Info4;
                txtInfo5.Text = infMedic.Info5;
                txtPpaInstruction.Text = infMedic.PpaInstruction;
                txtReceiveBy.Text = infMedic.ReceiveBy;

                if (infMedic.DateTimeInfo != null) PopulatePrescriptionCurrentDay(infMedic.DateTimeInfo.Value);
                ComboBox.SelectedValue(cboInputType, infMedic.SRMedicalNotesInputType);
                FormatInterface();

            }
            lvLocalistStatus.Rebind();

            if (trIcd10.Visible)
                workDiagCtl.Rebind(RegistrationInfoMedicID, DataModeCurrent != AppEnum.DataMode.Read);

            if (trIcd9cm.Visible)
                epProcedureCtl.Rebind(RegistrationInfoMedicID, DataModeCurrent != AppEnum.DataMode.Read);
        }

        private void PopulateRefer()
        {
            if (pnlReferAnswer.Controls.Count > 0)
            {
                foreach (var ctl in pnlReferAnswer.Controls)
                {
                    if (ctl is ConsultReferAnswerCtl)
                    {
                        var referAnswerCtl = (ConsultReferAnswerCtl)ctl;
                        referAnswerCtl.PopulateEntryControl(null, null);
                    }
                }
            }
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {

        }
        protected override void OnMenuNewClick()
        {
            PopulateRefer();
            hdnRegistrationInfoMedicID.Value = string.Empty;
            hdnServiceUnitID.Value = ServiceUnitID;

            var timeNow = (new DateTime()).NowAtSqlServer();
            txtDateSOAP.SelectedDate = timeNow;
            txtTimeSOAP.SelectedDate = timeNow;
            txtInfo1.Text = string.Empty;
            txtInfo2.Text = string.Empty;
            txtInfo3.Text = string.Empty;
            txtInfo4.Text = string.Empty;
            txtInfo5.Text = string.Empty;
            txtReceiveBy.Text = string.Empty;

            cboInputType.SelectedIndex = 0;
            FormatInterface();

            if (trPpaInstruction.Visible)
            {
                // PpaInstruction akan diisi intruksi2 yg harus dikerjakan oleh perawat atau PPA lainnya
                PopulatePrescriptionCurrentDay(timeNow);
            }

            ComboBox.PopulateWithParamedicTeam(cboParamedicID, RegistrationNo, RegistrationType, DateTime.Today, ParamedicID);
            lvLocalistStatus.Rebind();

            if (trIcd10.Visible)
                workDiagCtl.Rebind(RegistrationInfoMedicID, DataModeCurrent != AppEnum.DataMode.Read);

            if (trIcd9cm.Visible)
                epProcedureCtl.Rebind(RegistrationInfoMedicID, DataModeCurrent != AppEnum.DataMode.Read);

        }

        private void PopulatePrescriptionCurrentDay(DateTime dateTimeNow)
        {
            // Show histori resep dari dokter bersangkutan
            var updateDate = dateTimeNow.Date;
            var prescHist = string.Empty;
            if (string.IsNullOrEmpty(FromRegistrationNo))
                prescHist = TransPrescription.PrescriptionHist(ParamedicID, RegistrationNo, updateDate);
            else
                prescHist = string.Format("{0}{1}{1}{2}",
                    TransPrescription.PrescriptionHist(ParamedicID, FromRegistrationNo, updateDate), Environment.NewLine,
                    TransPrescription.PrescriptionHist(ParamedicID, RegistrationNo, updateDate));

            if (!string.IsNullOrEmpty(prescHist.Replace("\r\n", ""))) //Jangan diisi jika hanya berisi baris kosong (Handono 231209)
                txtPrescriptionCurrentDay.Text = prescHist;
            else
                txtPrescriptionCurrentDay.Text = string.Empty;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            SaveSoapAndBodyImage(true, args);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            SaveSoapAndBodyImage(false, args);
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
            ComboBox.PopulateWithParamedicTeam(cboParamedicID, RegistrationNo, RegistrationType, DateTime.Today, cboParamedicID.SelectedValue);
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {
        }
        public override string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }
        public override string OnGetScriptToolBarSaveClicking()
        {
            return string.Empty;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            var fw_btnAutoSave = (Button)Helper.FindControlRecursive(Master, "fw_btnAutoSave");
            ajax.AddAjaxSetting(fw_btnAutoSave, hdnRegistrationInfoMedicID);
        }
        #endregion


        private void SaveSoapAndBodyImage(bool isNewRecord, ValidateArgs args)
        {
            // Load 
            var rim = isNewRecord ? LoadNewSoap() : LoadEditSoap();
            RegistrationInfoMedicID = rim.RegistrationInfoMedicID;
            rim.Info1 = txtInfo1.Text;
            rim.Info1Entry = txtInfo1.Text;
            if ("notes".Equals(cboInputType.SelectedValue.ToLower()))
            {
                rim.Info2 = string.Empty;
                rim.Info3 = string.Empty;
                rim.Info4 = string.Empty;
                rim.Info5 = string.Empty;
            }
            else
            {
                rim.Info2 = txtInfo2.Text;
                //Assessmen / Diagnose
                rim.Info3Entry = txtInfo3.Text;
                rim.Info3 = txtInfo3.Text;
                rim.Info4 = txtInfo4.Text;
                rim.Info5 = txtInfo5.Text;
                rim.PpaInstruction = txtPpaInstruction.Text;
                rim.ReceiveBy = txtReceiveBy.Text;
            }
            rim.SRMedicalNotesInputType = cboInputType.SelectedValue;
            rim.ParamedicID = cboParamedicID.SelectedValue;

            var date = Convert.ToDateTime(txtDateSOAP.SelectedDate);
            var time = Convert.ToDateTime(txtTimeSOAP.SelectedDate);
            rim.DateTimeInfo = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);

            rim.AttendingNotes = string.Empty;
            rim.IsInformConcern = false;

            // SOAP Type
            if ("soap".Equals(cboInputType.SelectedValue.ToLower()))
            {
                string diagSummary;
                if (trIcd10.Visible)
                {
                    // Save work diagnose
                    workDiagCtl.Save(args, rim.RegistrationNo, rim.RegistrationInfoMedicID, rim.ParamedicID,
                        rim.DateTimeInfo.Value);

                    if (args.IsCancel)
                    {
                        // Abaikan Mandatory List ICD X untuk entrian Progress Note karena kalau kosong artinya Work Diagnose ICD X nya tidak berubah
                        args.IsCancel = false;
                        args.MessageText = String.Empty;
                    }

                    diagSummary = RegistrationInfoMedicDiagnose.DiagnoseSummaryCurrentSoap(rim.RegistrationInfoMedicID);
                }
                else
                    diagSummary = EpisodeDiagnose.DiagnoseSummary(rim.RegistrationNo);

                // Tambahkan ICD 10
                if (!string.IsNullOrWhiteSpace(txtInfo3.Text) && !string.IsNullOrWhiteSpace(diagSummary))
                    rim.Info3 = string.Concat(txtInfo3.Text, Environment.NewLine, diagSummary);
                else if (!string.IsNullOrWhiteSpace(diagSummary))
                    rim.Info3 = diagSummary;

                rim.PopulatePrescriptionCurrentDay(rim.ParamedicID, RegistrationNo, FromRegistrationNo);
            }

            // ICD 9
            if (trIcd9cm.Visible)
                epProcedureCtl.Save(rim.RegistrationInfoMedicID);

            // Save
            rim.Save();

            // Save Localist / Body Image 
            if (Session["rimBodyImage_" + PageID] != null)
            {
                var dtbSession = (DataTable)Session["rimBodyImage_" + PageID];
                foreach (DataRow row in dtbSession.Rows)
                {
                    if (true.Equals(row["IsModified"]))
                    {
                        SaveLocalistStatus(RegistrationInfoMedicID, row["BodyID"].ToString(),
                            (byte[])row["BodyImage"]);
                    }
                }
            }

            Session.Remove("rimBodyImage_" + PageID);
            Session.Remove("rimBodyImage_id_" + PageID);

            // Save ConsultReferAnswer
            if (pnlReferAnswer.Controls.Count > 0)
            {
                foreach (var ctl in pnlReferAnswer.Controls)
                {
                    if (ctl is ConsultReferAnswerCtl)
                    {
                        var referAnswerCtl = (ConsultReferAnswerCtl)ctl;
                        referAnswerCtl.SetEntityValue(null, null, null);
                    }
                }
            }
        }

        private void SaveLocalistStatus(string regInfoMedicID, string bodyId, byte[] bodyImage)
        {
            var es = new RegistrationInfoMedicBodyDiagram();
            if (!es.LoadByPrimaryKey(regInfoMedicID, bodyId))
            {
                es = new RegistrationInfoMedicBodyDiagram
                {
                    RegistrationInfoMedicID = regInfoMedicID,
                    IsDeleted = false,
                    ServiceUnitID = Request.QueryString["unit"],
                    ParamedicID = ParamedicID,
                    BodyID = bodyId,
                    CreatedDateTime = DateTime.Now,
                    CreatedByUserID = AppSession.UserLogin.UserID
                };
            }

            es.BodyImage = bodyImage;
            es.Save();
        }
        private RegistrationInfoMedic LoadNewSoap()
        {
            var ent = new RegistrationInfoMedic();

            var autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.RegInfoMedicNo);
            ent.RegistrationInfoMedicID = autoNumber.LastCompleteNumber;
            autoNumber.Save();

            ent.RegistrationNo = RegistrationNo;
            ent.SRMedicalNotesInputType = "SOAP"; //Hardcode
            ent.ServiceUnitID = ServiceUnitID;
            ent.ParamedicID = ParamedicID;
            return ent;
        }

        private RegistrationInfoMedic LoadEditSoap()
        {
            var ent = new RegistrationInfoMedic();
            ent.LoadByPrimaryKey(RegistrationInfoMedicID);
            return ent;
        }



        protected void lvLocalistStatus_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            if (!IsPostBack || !RegistrationInfoMedicID.Equals(Session["rimBodyImage_id_" + PageID]))
            {
                PopulateBodyImageSession();
            }

            var dtbSession = (DataTable)Session["rimBodyImage_" + PageID];
            lvLocalistStatus.DataSource = dtbSession;
        }

        private void PopulateBodyImageSession()
        {
            var qr = new RegistrationInfoMedicBodyDiagramQuery("rim");
            var qrSubd = new BodyDiagramServiceUnitQuery("bsu");

            if (string.IsNullOrEmpty(RegistrationInfoMedicID))
            {
                qr.RightJoin(qrSubd).On(qr.BodyID == qrSubd.BodyID && qr.RegistrationInfoMedicID == "_");

            }
            else
            {
                qr.RightJoin(qrSubd).On(qr.BodyID == qrSubd.BodyID && qr.RegistrationInfoMedicID == RegistrationInfoMedicID);
            }
            var qrBody = new BodyDiagramQuery("bd");
            qr.InnerJoin(qrBody).On(qrSubd.BodyID == qrBody.BodyID);

            qr.Select(qr.RegistrationInfoMedicID,
                "<CASE WHEN rim.RegistrationInfoMedicID IS NULL THEN bd.BodyImage ELSE rim.BodyImage END as BodyImage>",
                qr.LastUpdateByUserID, qr.CreatedDateTime, qr.LastUpdateDateTime,
                "<CASE WHEN rim.RegistrationInfoMedicID IS NULL THEN 'new' ELSE 'edit' END as EntryMode>",
                qrBody.BodyID, qrBody.BodyName, "<CONVERT(BIT,0) IsModified>");

            qr.Where(qrSubd.ServiceUnitID == ServiceUnitID);

            var dtb = qr.LoadDataTable();

            // Jangan rubah session kecuali dirubah juga pada page /Module/RADT/Cpoe/Common/LocalistStatus/LocalistStatusEntry.aspx
            Session["rimBodyImage_id_" + PageID] = RegistrationInfoMedicID;
            Session["rimBodyImage_" + PageID] = dtb;
        }


        #region Vital Sign
        protected void grdVitalSign_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var dtm = new DateTime();
            if (txtDateSOAP.SelectedDate.HasValue)
            {
                var dt = txtDateSOAP.SelectedDate.Value;
                var tm = txtTimeSOAP.SelectedDate.Value;

                dtm = new DateTime(dt.Year, dt.Month, dt.Day, tm.Hour, tm.Minute, 59);
            }
            else
                dtm = (new DateTime()).NowAtSqlServer();


            grdVitalSign.DataSource = VitalSign.VitalSignLastValue(RegistrationNo, MergeRegistrations, true, dtm);
        }
        #endregion

        #region curb
        protected void grdCurb_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var dtm = new DateTime();
            if (txtDateSOAP.SelectedDate.HasValue)
            {
                var dt = txtDateSOAP.SelectedDate.Value;
                var tm = txtTimeSOAP.SelectedDate.Value;

                dtm = new DateTime(dt.Year, dt.Month, dt.Day, tm.Hour, tm.Minute, 59);
            }
            else
                dtm = (new DateTime()).NowAtSqlServer();


            grdCurb.DataSource = VitalSign.VitalSignLastValue(RegistrationNo, MergeRegistrations, true, dtm, true);
        }
        #endregion


        protected void cboInputType_OnSelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            FormatInterface();
        }
        private void FormatInterface()
        {
            row2.Visible = true;
            row3.Visible = true;
            row4.Visible = true;
            row5.Visible = false;
            trPpaInstruction.Visible = false;
            //trIcd10.Visible = false;
            trReceiveBy.Visible = false;

            switch (cboInputType.SelectedValue)//(MedicalNotesInputType)
            {
                case "SBAR":
                    {
                        lblInfo1.Text = "Situation (S)";
                        lblInfo2.Text = "Background (B)";
                        lblInfo3.Text = "Assessment (A)";
                        lblInfo4.Text = "Recommendation (R)";
                        lblPpaInstruction.Text = "Instruction / Advice (I)";
                        trPpaInstruction.Visible = true;
                        trIcd10.Visible = false;

                        lblInfo5.Text = "Write, Read and Reconfirm Instructions (TBAK)";
                        row5.Visible = true;

                        trReceiveBy.Visible = true;
                        break;
                    }
                case "SOAP":
                    {
                        lblInfo1.Text = "Subjective (S)";
                        lblInfo2.Text = "Objective (O)";
                        lblInfo3.Text = "Assessment (A)";
                        lblInfo4.Text = "Planning (P)";
                        trPpaInstruction.Visible = true;
                        if (RegistrationCurrent.SRRegistrationType == AppConstant.RegistrationType.InPatient && RegistrationCurrent.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient)
                        {
                            trIcd10.Visible = true;
                        }
                        break;
                    }
                case "ADIME":
                    {
                        lblInfo1.Text = "Assessment (A)";
                        lblInfo2.Text = "Diagnosis (D)";
                        lblInfo3.Text = "Intervention (I)";
                        lblInfo4.Text = "Monitoring (M)";
                        lblInfo5.Text = "Evaluation (E)";

                        row5.Visible = true;
                        trIcd10.Visible = false;
                        break;
                    }
                default:
                    {
                        lblInfo1.Text = "Notes";
                        row2.Visible = false;
                        row3.Visible = false;
                        row4.Visible = false;
                        trIcd10.Visible = false;
                        break;
                    }
            }
        }

        protected void txtDateSOAP_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            if (txtDateSOAP.SelectedDate != null)
                PopulatePrescriptionCurrentDay(txtDateSOAP.SelectedDate.Value);
            else
                txtPrescriptionCurrentDay.Text = string.Empty;
        }

        protected string LookUpSoLink(string soro)
        {
            if (cboInputType.SelectedValue == "SOAP")
            {
                if (soro == "S")
                    return
                        "<a style=\"cursor: pointer\" onclick=\"javascript:openLookUpS();return false;\"><img src=\"../../../../../Images/Toolbar/search16.png\" alt=\"\" /></a>";
                return
                    "<a style=\"cursor: pointer\" onclick=\"javascript:openLookUpO();return false;\"><img src=\"../../../../../Images/Toolbar/search16.png\" alt=\"\" /></a>";
            }
            return String.Empty;
        }

        #region Antibiotic for Raspraja
        protected string AntibioticItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            return "&nbsp;&nbsp;&nbsp;" + itemName;
        }
        protected void grdAntibioticItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var lastrr = new RegistrationRaspro();
            lastrr.Query.Where(lastrr.Query.RegistrationNo == RegistrationNo, lastrr.Query.SRRaspro.NotIn(AppConstant.RasproType.Raspraja, AppConstant.RasproType.Prophylaxis));
            lastrr.Query.es.Top = 1;
            lastrr.Query.OrderBy(lastrr.Query.SeqNo.Descending);
            if (lastrr.Query.Load())
            {
                var query = new RegistrationRasproItemQuery("a");
                var qItem = new ItemQuery("b");
                query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);

                var qza = new ZatActiveQuery("za");
                query.InnerJoin(qza).On(query.ZatActiveID == qza.ZatActiveID);

                var cm = new ConsumeMethodQuery("cm");
                query.LeftJoin(cm).On(query.SRConsumeMethod == cm.SRConsumeMethod);

                query.Select
                    (
                          query.RasproSeqNo, query.ItemID, query.SRConsumeMethod, cm.SRConsumeMethodName, cm.SygnaText, query.ConsumeQty, query.SRConsumeUnit, query.ZatActiveID, qItem.ItemName, qza.ZatActiveName, query.StartDateTime
                    );

                query.Where(query.RegistrationNo == RegistrationNo, query.RasproSeqNo == lastrr.SeqNo, query.StopDateTime.IsNull());

                query.OrderBy(qza.ZatActiveName.Ascending);
                var dtb = query.LoadDataTable();
                dtb.Columns.Add("ConsumeDayNo", typeof(int));

                lnkRaspraja.Enabled = false;
                var antibioticMaxConsumeDay = AppParameter.GetParameterValue(AppParameter.ParameterItem.AntibioticMaxConsumeDay).ToInt();
                foreach (DataRow row in dtb.Rows)
                {
                    // ConsumeDayNo diambil dari Realisasi Medication
                    row["ConsumeDayNo"] = MedicationReceiveUsed.ConsumedDay(RegistrationNo, row["ItemID"].ToString(),
                     row["SRConsumeMethod"].ToString(), row["ConsumeQty"].ToString(), row["SRConsumeUnit"].ToString());

                    if (row["ConsumeDayNo"].ToInt() >= antibioticMaxConsumeDay)
                    {
                        lnkRaspraja.Enabled = true;
                    }
                }

                grdAntibioticItem.DataSource = dtb;
            }
            else
                grdAntibioticItem.DataSource = string.Empty;
        }

        int _antibioticMaxConsumeDay = AppParameter.GetParameterValue(AppParameter.ParameterItem.AntibioticMaxConsumeDay).ToInt();
        protected void grdAntibioticItem_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                // Red utk AntibioticMaxConsumeDay
                if (item["ConsumeDayNo"].Text.ToInt() >= _antibioticMaxConsumeDay)
                {
                    item.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        #endregion



        protected void lbtnResetHistoryOfPresentIllness_OnClick(object sender, EventArgs e)
        {
            txtInfo1.Text = RegistrationInfoMedic.Last.RegistrationInfoMedicInfo2(RegistrationNo, FromRegistrationNo).Info1;
        }

        protected void lbtnResetObjective_OnClick(object sender, EventArgs e)
        {

            txtInfo2.Text = RegistrationInfoMedic.Last.RegistrationInfoMedicInfo2(RegistrationNo, FromRegistrationNo).Info2;
        }
        protected void lbtnResetPlanning_OnClick(object sender, EventArgs e)
        {
            txtInfo4.Text = RegistrationInfoMedic.Last.RegistrationInfoMedicInfo2(RegistrationNo, FromRegistrationNo).Info4;
        }

    }
}
