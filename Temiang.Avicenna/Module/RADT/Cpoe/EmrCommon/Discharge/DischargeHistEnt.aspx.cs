using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Text;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class DischargeHistEnt : BasePageDialogHistEntry
    {
        protected bool IsModeViewHist => (DataModeCurrent == AppEnum.DataMode.Read);

        #region Grid Discharge History

        protected void grdDischargeHist_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDischargeHist.DataSource = RegistrationDischarge();
        }

        private DataTable RegistrationDischarge()
        {
            var query = new RegistrationQuery("a");
            var suQ = new ServiceUnitQuery("b");
            var parQ = new ParamedicQuery("c");
            var dmQ = new AppStandardReferenceItemQuery("d");
            var dcQ = new AppStandardReferenceItemQuery("e");

            query.InnerJoin(suQ).On(query.ServiceUnitID == suQ.ServiceUnitID);
            query.InnerJoin(parQ).On(query.ParamedicID == parQ.ParamedicID);
            query.LeftJoin(dmQ).On(query.SRDischargeMethod == dmQ.ItemID && dmQ.StandardReferenceID == "DischargeMethod");
            query.LeftJoin(dcQ).On(query.SRDischargeCondition == dcQ.ItemID &&
                                   dcQ.StandardReferenceID == "DischargeCondition");

            query.Select
            (
                query.PatientID,
                query.RegistrationNo,
                @"<CAST(CONVERT(VARCHAR(10), a.RegistrationDate, 112) + ' ' + a.RegistrationTime AS DATETIME) AS RegistrationDateTime>",
                parQ.ParamedicName,
                suQ.ServiceUnitName,
                query.BedID,
                @"<CAST(CONVERT(VARCHAR(10), a.DischargeDate, 112) + ' ' + a.DischargeTime AS DATETIME) AS DischargeDate>",
                dmQ.ItemName.As("DischargeMethod"),
                dcQ.ItemName.As("DischargeCondition"),
                query.DischargeNotes,
                query.DischargeMedicalNotes
            );

            query.Where(query.PatientID == PatientID, query.IsVoid == false, query.DischargeDate.IsNotNull());
            query.OrderBy(query.RegistrationDate.Descending, query.RegistrationTime.Descending);

            var dtb = query.LoadDataTable();
            dtb.Columns.Add(new DataColumn("Diagnosis", typeof(string)));
            dtb.Columns.Add(new DataColumn("ICD10", typeof(string)));
            dtb.Columns.Add(new DataColumn("Therapy", typeof(string)));

            foreach (DataRow row in dtb.Rows)
            {
                var regNo = row["RegistrationNo"].ToString();

                // Diagnose entri oleh dokter
                var rimQr = new RegistrationInfoMedicQuery();
                rimQr.Where(rimQr.RegistrationNo == regNo);
                rimQr.es.Top = 1;
                rimQr.Select(rimQr.Info3, rimQr.Info4);
                var rim = new RegistrationInfoMedic();
                if (rim.Load(rimQr))
                {
                    row["Diagnosis"] = rim.Info3;
                    row["Therapy"] = rim.Info4;
                }
                else
                {
                    // Cari didata lama
                    var soapQr = new EpisodeSOAPEQuery();
                    soapQr.Where(soapQr.RegistrationNo == regNo);
                    soapQr.es.Top = 1;
                    soapQr.Select(soapQr.Assesment, soapQr.Planning);
                    var soap = new EpisodeSOAPE();
                    if (soap.Load(soapQr))
                    {
                        row["Diagnosis"] = soap.Assesment;
                        row["Therapy"] = soap.Planning;
                    }
                }

                row["ICD10"] = EpisodeDiagnose.DiagnoseSummaryHtml(regNo);
            }
            return dtb;
        }

        #endregion

        #region Query String Propeties

        private string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        private string ParamedicID
        {
            get
            {
                return Request.QueryString["parid"];
            }
        }

        private string _patientID;
        private string PatientID
        {
            get
            {
                // Jangan ambil dari QueryString krn bisa jadi utk PatientID yg berbeda tetapi masih pasien yg sama (PatientRelated)
                //return Request.QueryString["patid"];
                if (!string.IsNullOrEmpty(RegistrationNo) && string.IsNullOrEmpty(_patientID))
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(RegistrationNo);
                    _patientID = reg.PatientID;
                }
                else
                    _patientID = Request.QueryString["patid"];

                return _patientID;
            }
        }
        private string RegistrationType
        {
            get
            {
                return Request.QueryString["rt"];
            }
        }
        private bool IsParamedicInTeam
        {
            get
            {
                return Request.QueryString["pit"] == "1";
            }
        }
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Ovveride enable menu
            ToolBar.ApprovalEnabled = false;
            ToolBar.VoidEnabled = true;
            ToolBar.EditEnabled = false;
            ToolBar.UnVoidEnabled = false;
            ToolBar.DeleteEnabled = false;

            ToolBar.AddEnabled = IsParamedicInTeam;

            Splitter.Orientation = Orientation.Horizontal;
            IsCustomReportList = true;

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Disharge of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRUnitIntended, AppEnum.StandardReference.UnitIntended);
                ComboBox.PopulateWithParamedic(cboTreatingPhysician);

                // Tampilkan untuk registrasi dipilih
                //OnPopulateEntryControl();
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            // Override Void Menu Access
            ToolBarMenuVoid.Enabled = IsParamedicInTeam && !string.IsNullOrEmpty(cboSRDischargeMethod.SelectedValue);
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
        }

        private void PopulateEntryControl(Registration reg)
        {
            // Hardcode Print button
            // Dirawat
            //O02	RAWAT INAP VIA REGISTRASI
            //O03	RAWAT INAP VIA IGD
            //E10 KEMBALI KE RAWAT INAP
            //E02 DIRAWAT (MRS)
            ToolBarMenuPrint.Buttons.Clear();

            //if ("O02_O03_E10_E02".Contains(reg.SRDischargeMethod))
            if (AppSession.Parameter.DischargeMethodInCare.Contains(reg.SRDischargeMethod))
            {
                ToolBarMenuPrint.Enabled = true;
                // Print
                var btn = new RadToolBarButton("Inpatient Admission Letter")
                {
                    Value = string.Format("rpt_{0}", AppConstant.Report.InpatientAdmissionLetter)
                };
                ToolBarMenuPrint.Buttons.Add(btn);
                ToolBarMenuPrint.Visible = true;

            }
            else
                ToolBarMenuPrint.Visible = false;


            StandardReference.InitializeWithOneRow(cboSRDischargeMethod, AppEnum.StandardReference.DischargeMethod, reg.SRDischargeMethod);
            StandardReference.InitializeWithOneRow(cboSRDischargeCondition, AppEnum.StandardReference.DischargeCondition, reg.SRDischargeCondition);

            txtDischargeDate.SelectedDate = reg.DischargeDate;
            txtDischargeTime.SelectedDate = Convert.ToDateTime(reg.DischargeTime);
            txtDischargeMedicalNotes.Text = reg.DischargeMedicalNotes;
            txtDischargeNotes.Text = reg.DischargeNotes;


            var discharge = new RegistrationDischargeDetail();
            if (discharge.LoadByPrimaryKey(reg.RegistrationNo))
            {
                cboTreatingPhysician.SelectedValue = discharge.ParamedicID;
                txtTreatingPhysicianName.Text = discharge.ParamedicName;
                cboSRUnitIntended.SelectedValue = discharge.SRUnitIntended;
            }

            // Sign
            imgParSign.DataValue = null;
            hdnParSign.Value = string.Empty;
            lblSignDate.Text = DateTime.Now.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute);
            if (discharge.SignID != null)
            {
                var psign = new SignPool();
                if (psign.LoadByPrimaryKey(discharge.SignID ?? 0))
                {
                    imgParSign.DataValue = psign.SignImg;
                    if (psign.SignDateTime != null)
                        lblSignDate.Text =
                            psign.SignDateTime.Value.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute);
                }
            }
        }


        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            // Hanay untuk edit current reg
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(RegistrationNo) && !string.IsNullOrEmpty(reg.SRDischargeMethod))
            {
                PopulateEntryControl(reg);
            }
            else
                ClearEntryValue();
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            grdDischargeHist.Columns[0].Display = newVal == AppEnum.DataMode.Read;
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(RegistrationNo) && !string.IsNullOrEmpty(reg.SRDischargeMethod))
            {
                PopulateEntryControl(reg);
                ScriptManager.RegisterStartupScript(this, GetType(), "discharge", "alert('Patients has been discharged');", true);
                args.IsCancel = true;
                args.MessageText = "Patients has been discharged";
            }

        }
        protected override void OnMenuNewClick()
        {
            ClearEntryValue();

            // Discharge for current Reg
            StandardReference.InitializeIncludeSpace(cboSRDischargeMethod, AppEnum.StandardReference.DischargeMethod, RegistrationType);
            StandardReference.InitializeIncludeSpace(cboSRDischargeCondition, AppEnum.StandardReference.DischargeCondition, RegistrationType);

            var timeNow = (new DateTime()).NowAtSqlServer();
            txtDischargeDate.SelectedDate = timeNow.Date;
            txtDischargeTime.SelectedDate = timeNow;

            if (string.IsNullOrEmpty(cboTreatingPhysician.Text))
            {
                ComboBox.SelectedValue(cboTreatingPhysician, ParamedicID);
            }

            lblSignDate.Text = DateTime.Now.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute);
        }


        private void ClearEntryValue()
        {
            cboSRDischargeMethod.Text = string.Empty;
            cboSRDischargeMethod.SelectedValue = string.Empty;
            cboSRDischargeCondition.Text = string.Empty;
            cboSRDischargeCondition.SelectedValue = string.Empty;
            txtDischargeMedicalNotes.Text = string.Empty;
            txtDischargeNotes.Text = string.Empty;

            cboTreatingPhysician.Text = string.Empty;
            cboTreatingPhysician.SelectedValue = string.Empty;
            txtTreatingPhysicianName.Text = string.Empty;
            cboSRUnitIntended.Text = string.Empty;
            cboSRUnitIntended.SelectedValue = string.Empty;
            imgParSign.DataValue = null;
            hdnParSign.Value = string.Empty;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            SaveDischarge();
        }


        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            var dd = new RegistrationDischargeDetail();
            if (dd.LoadByPrimaryKey(RegistrationNo))
            {
                PrintJobParameter jobParameter;

                jobParameter = printJobParameters.AddNew();
                jobParameter.Name = "RegistrationNo";
                jobParameter.ValueString = RegistrationNo;
            }
            else
            {
                args.IsCancel = true; //skip print in framework
                args.MessageText = "Patients has not been discharged";
                ScriptManager.RegisterStartupScript(this, GetType(), "discharge", "alert('Patients has not been discharged');", true);
            }
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {

        }

        protected override void OnMenuEditClick()
        {

        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {

        }

        private void CancelDischarge()
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);
            if (string.IsNullOrEmpty(reg.SRDischargeMethod))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "discharge", "alert('Patient is not discharged');", true);
                return;
            }

            var bed = new Bed();
            var isUsingBed = false;

            if (!string.IsNullOrEmpty(reg.BedID) && bed.LoadByPrimaryKey(reg.BedID))
            {
                isUsingBed = true;

                if (bed.RegistrationNo != string.Empty && bed.SRBedStatus != AppSession.Parameter.BedStatusUnoccupied)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "discharge", "alert('Bed already fill with another patient');", true);
                    return;
                }
            }

            using (var trans = new esTransactionScope())
            {
                var history = new PatientDischargeHistory();
                history.AddNew();
                history.RegistrationNo = reg.RegistrationNo;
                history.BedID = reg.BedID;
                history.DischargeDate = reg.DischargeDate;
                history.DischargeTime = reg.DischargeTime;
                history.SRDischargeMethod = reg.SRDischargeMethod;
                history.SRDischargeCondition = reg.SRDischargeCondition;
                history.DischargeOperatorID = AppSession.UserLogin.UserID;
                history.IsCancel = true;
                history.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                var patient = new Patient();
                patient.LoadByPrimaryKey(reg.PatientID);
                if (reg.SRDischargeCondition == AppSession.Parameter.DischargeConditionDieLessThen48 || reg.SRDischargeCondition == AppSession.Parameter.DischargeConditionDieMoreThen48)
                {
                    patient.IsAlive = true;
                    patient.Save();
                }

                reg.DischargeDate = null;
                reg.DischargeTime = null;
                reg.DischargeMedicalNotes = string.Empty;
                reg.DischargeNotes = string.Empty;
                reg.SRDischargeCondition = string.Empty;
                reg.SRDischargeMethod = string.Empty;
                reg.DischargeOperatorID = string.Empty;

                if (!(reg.IsRoomIn ?? false))
                {
                    if (isUsingBed)
                    {
                        bed.RegistrationNo = reg.RegistrationNo;
                        bed.SRBedStatus = AppSession.Parameter.BedStatusOccupied;

                        var birColl = new BedRoomInCollection();
                        birColl.Query.Where(
                            birColl.Query.BedID == bed.BedID,
                            birColl.Query.IsVoid == false,
                            birColl.Query.DateOfExit.IsNull()
                            );
                        birColl.LoadAll();

                        bed.IsRoomIn = birColl.Count > 0;
                        bed.Save();
                    }

                    var thuColl = new PatientTransferHistoryCollection();
                    var thuQuery = new PatientTransferHistoryQuery();
                    thuQuery.Where(thuQuery.RegistrationNo == reg.RegistrationNo);
                    thuQuery.es.Top = 1;
                    thuQuery.OrderBy(thuQuery.TransferNo.Descending);
                    thuColl.Load(thuQuery);

                    foreach (var item in thuColl)
                    {
                        item.DateOfExit = null;
                        item.TimeOfExit = null;
                    }

                    reg.Save();
                    thuColl.Save();
                    history.Save();
                }
                else
                {
                    if (isUsingBed)
                    {
                        bed.IsRoomIn = true;

                        var bir = new BedRoomIn();
                        bir.LoadByPrimaryKey(bed.BedID, reg.RegistrationNo, reg.RegistrationDate ?? DateTime.Now,
                            reg.RegistrationTime);
                        bir.DateOfExit = null;
                        bir.TimeOfExit = null;

                        bed.Save();
                        bir.Save();
                    }

                    var thuColl = new PatientTransferHistoryCollection();
                    var thuQuery = new PatientTransferHistoryQuery();
                    thuQuery.Where(thuQuery.RegistrationNo == reg.RegistrationNo);
                    thuQuery.es.Top = 1;
                    thuQuery.OrderBy(thuQuery.TransferNo.Descending);
                    thuColl.Load(thuQuery);

                    foreach (var item in thuColl)
                    {
                        item.DateOfExit = null;
                        item.TimeOfExit = null;
                    }
                    thuColl.Save();

                    reg.Save();
                    history.Save();
                }

                var discharge = new RegistrationDischargeDetail();
                if (discharge.LoadByPrimaryKey(RegistrationNo))
                {
                    var sp = new SignPool();
                    if (discharge.SignID != null && sp.LoadByPrimaryKey(discharge.SignID ?? 0))
                    {
                        sp.MarkAsDeleted();
                        sp.Save();
                    }
                    discharge.MarkAsDeleted();
                    discharge.Save();
                }

                trans.Complete();
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "discharge", "alert('Patient discharge cancelled');", true);
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
            CancelDischarge();
            grdDischargeHist.DataSource = null;
            grdDischargeHist.Rebind();
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
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);
            if (!string.IsNullOrEmpty(reg.SRDischargeMethod))
            {
                return false;
            }
            return true;
        }

        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public override bool OnGetStatusMenuDelete()
        {
            // Dokter lain atau petugas bisa isi discharge
            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            // Disable diset menggunakan ToolBar.ApprovalEnabled = false 
            // Jika fungsi ini return false malah akan memunculkan image Stamp
            return true;
        }

        public override bool OnGetStatusMenuVoid()
        {
            // Salah satu syarat Menu Void enable
            return true;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        private void SaveDischarge()
        {
            var isUsingBed = false;
            var bedID = string.Empty;
            // Save for registration selected in main EMR
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);

            if (!string.IsNullOrEmpty(reg.SRDischargeMethod))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "discharge", "alert('Patient already discharged');", true);
                return;
            }


            var bed = new Bed();
            if (!string.IsNullOrEmpty(reg.BedID) && bed.LoadByPrimaryKey(reg.BedID))
            {
                isUsingBed = true;
                bedID = reg.BedID;
            }

            using (var trans = new esTransactionScope())
            {
                //update registration
                reg.DischargeDate = txtDischargeDate.SelectedDate;
                reg.DischargeTime = txtDischargeTime.SelectedDate.Value.ToString("HH:mm");
                reg.DischargeMedicalNotes = txtDischargeMedicalNotes.Text;
                reg.DischargeNotes = txtDischargeNotes.Text;
                reg.SRDischargeCondition = cboSRDischargeCondition.SelectedValue;
                reg.SRDischargeMethod = cboSRDischargeMethod.SelectedValue;
                reg.DischargeOperatorID = AppSession.UserLogin.UserID;


                //update bed
                if (isUsingBed)
                {
                    if (reg.IsRoomIn == false)
                    {
                        bed.RegistrationNo = string.Empty;
                        bed.SRBedStatus = AppSession.Parameter.BedStatusUnoccupied;
                    }

                    var bric = new BedRoomInCollection();
                    bric.Query.Where(
                        bric.Query.BedID == bedID,
                        bric.Query.RegistrationNo != reg.RegistrationNo,
                        bric.Query.IsVoid == false,
                        bric.Query.DateOfExit.IsNull()
                        );
                    bric.LoadAll();
                    bed.IsRoomIn = bric.Count > 0;

                    bed.Save();

                    //update room in
                    var briColl = new BedRoomInCollection();
                    if (reg.IsRoomIn ?? false)
                    {
                        briColl.Query.Where(
                            briColl.Query.BedID == bedID,
                            briColl.Query.RegistrationNo == reg.RegistrationNo,
                            briColl.Query.IsVoid == false,
                            briColl.Query.DateOfExit.IsNull()
                            );
                        briColl.LoadAll();
                        foreach (var item in briColl)
                        {
                            item.DateOfExit = reg.DischargeDate;
                            item.TimeOfExit = reg.DischargeTime;
                        }

                        briColl.Save();
                    }
                }

                //update PatientTransferHistory
                var transferHistQr = new PatientTransferHistoryQuery();
                transferHistQr.Where(transferHistQr.RegistrationNo == reg.RegistrationNo);
                transferHistQr.es.Top = 1;
                transferHistQr.OrderBy(transferHistQr.TransferNo.Descending);

                var transferHistColl = new PatientTransferHistoryCollection();
                transferHistColl.Load(transferHistQr);

                foreach (var item in transferHistColl)
                {
                    item.DateOfExit = reg.DischargeDate;
                    item.TimeOfExit = reg.DischargeTime;
                }

                var dischargeHistory = new PatientDischargeHistory();
                dischargeHistory.AddNew();
                dischargeHistory.RegistrationNo = reg.RegistrationNo;
                dischargeHistory.BedID = bedID;
                dischargeHistory.DischargeDate = reg.DischargeDate;
                dischargeHistory.DischargeTime = reg.DischargeTime;
                dischargeHistory.SRDischargeMethod = reg.SRDischargeMethod;
                dischargeHistory.SRDischargeCondition = reg.SRDischargeCondition;
                dischargeHistory.DischargeOperatorID = reg.DischargeOperatorID;
                dischargeHistory.IsCancel = false;

                //update patient
                var patient = new Patient();
                patient.LoadByPrimaryKey(reg.PatientID);
                if (reg.SRDischargeCondition == AppSession.Parameter.DischargeConditionDieLessThen48 || reg.SRDischargeCondition == AppSession.Parameter.DischargeConditionDieMoreThen48)
                {
                    patient.IsAlive = false;
                    patient.Save();
                }

                //discharge detail
                var discharge = new RegistrationDischargeDetail();
                if (!discharge.LoadByPrimaryKey(RegistrationNo))
                    discharge.AddNew();
                discharge.RegistrationNo = RegistrationNo;
                discharge.ParamedicID = cboTreatingPhysician.SelectedValue;
                discharge.ParamedicName = txtTreatingPhysicianName.Text;
                discharge.SRUnitIntended = cboSRUnitIntended.SelectedValue;

                //Physician Sign
                if (!string.IsNullOrWhiteSpace(hdnParSign.Value))
                {
                    var isSignNew = true;
                    var sp = new SignPool();
                    if (discharge.SignID != null)
                    {
                        isSignNew = !sp.LoadByPrimaryKey(discharge.SignID ?? 0);
                        if (isSignNew)
                            sp = new SignPool();
                    }

                    sp.SignByID = AppSession.UserLogin.ParamedicID;
                    sp.SignDateTime = DateTime.Now;

                    var imgHelper = new ImageHelper();
                    var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnParSign.Value), new Size(332, 185));
                    sp.SignImg = imgHelper.ToByteArray(resized, ImageFormat.Png);
                    sp.Save();

                    if (isSignNew)
                        discharge.SignID = sp.SignID; //Otomatis terisi setelah save, SignID IDENTITY -> true
                }

                //save            
                reg.Save();
                transferHistColl.Save();
                dischargeHistory.Save();
                discharge.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            grdDischargeHist.DataSource = null;
            grdDischargeHist.Rebind();

            ScriptManager.RegisterStartupScript(this, GetType(), "discharge", "alert('Discharge has saved');", true);
        }
    }
}
