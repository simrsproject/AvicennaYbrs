using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Charting;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class RegistrationInfoMedicEntry : BasePageDialogEntry
    {
        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var ent = new RegistrationInfoMedic();
            ent.LoadByPrimaryKey(LastRegistrationInfoMedicID);

            ComboBox.SelectedValue(cboInputType, ent.SRMedicalNotesInputType);
            FormatInterface();

            txtInfo1.Text = ent.Info1;
            txtInfo2.Text = ent.Info2;
            txtInfo3.Text = ent.Info3;
            txtInfo4.Text = ent.Info4;
            dateInfo.SelectedDate = ent.DateTimeInfo;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {
            txtInfo1.Text = string.Empty;
            txtInfo2.Text = string.Empty;
            txtInfo3.Text = string.Empty;
            txtInfo4.Text = string.Empty;
            cboInputType.SelectedIndex = 0;
            FormatInterface();
            dateInfo.SelectedDate = (new DateTime()).NowAtSqlServer(); 
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            SaveRecord();
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            SaveRecord();
        }

        private void SaveRecord()
        {
            using (var trans = new esTransactionScope())
            {
                var ent = new RegistrationInfoMedic();
                if (DataModeCurrent == AppEnum.DataMode.New)
                {
                    var autoNumber = new AppAutoNumberLast();
                    autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.RegInfoMedicNo);
                    ent.RegistrationInfoMedicID = autoNumber.LastCompleteNumber;
                    autoNumber.Save();

                    ent.RegistrationNo = RegistrationNo;
                }
                else
                {
                    ent.LoadByPrimaryKey(LastRegistrationInfoMedicID);
                }

                ent.Info1 = txtInfo1.Text;
                if ("notes".Equals(cboInputType.SelectedValue.ToLower()))
                {
                    ent.Info2 = string.Empty;
                    ent.Info3 = string.Empty;
                    ent.Info4 = string.Empty;
                }
                else
                {
                    ent.Info2 = txtInfo2.Text;
                    ent.Info3 = txtInfo3.Text;
                    ent.Info4 = txtInfo4.Text;
                }
                ent.DateTimeInfo = dateInfo.SelectedDate;
                ent.SRMedicalNotesInputType = cboInputType.SelectedValue;

                var reg = new Registration();
                reg.LoadByPrimaryKey(RegistrationNo);

                ent.ServiceUnitID = reg.ServiceUnitID;

                ent.Save();

                // Vital Sign
                var vitalsigns = new RegistrationInfoMedicVitalSignCollection();
                if (DataModeCurrent == AppEnum.DataMode.Edit)
                {
                    vitalsigns.Query.Where(vitalsigns.Query.RegistrationInfoMedicID == ent.RegistrationInfoMedicID);
                    vitalsigns.LoadAll();
                    vitalsigns.MarkAllAsDeleted();
                    vitalsigns.Save();
                }

                vitalsigns = new RegistrationInfoMedicVitalSignCollection();

                foreach (GridDataItem item in grdVitalSign.MasterTableView.Items)
                {
                    var txtNum = ((RadNumericTextBox)item.FindControl("txtValueType_NUM"));
                    if (txtNum.Value != null)
                    {
                        var vitalsign = vitalsigns.AddNew();
                        vitalsign.RegistrationInfoMedicID = ent.RegistrationInfoMedicID;
                        vitalsign.VitalSignID = item["VitalSignID"].Text;
                        vitalsign.VitalSignUnit = item["VitalSignUnit"].Text;
                        vitalsign.VitalSignValueText = txtNum.Text;
                        vitalsign.VitalSignValueNum = Convert.ToDecimal(txtNum.Value);

                    }
                }

                vitalsigns.Save();

                //Commit if success, Rollback if failed
                trans.Complete();

                // Simpan utk keperluan OnPopulateEntryControl
                LastRegistrationInfoMedicID = ent.RegistrationInfoMedicID;
            }
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
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
            throw new Exception("The method or operation is not implemented.");
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
        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return true;
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

        private CpoeTypeEnum CpoeType
        {
            get
            {
                switch (this.Request.QueryString["rt"])
                {
                    case "IPR":
                        return CpoeTypeEnum.InPatient;
                    //case "EMR":
                    //    return CpoeTypeEnum.Emergency;
                    default:
                        return CpoeTypeEnum.Outpatient;
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            switch (CpoeType)
            {
                case CpoeTypeEnum.InPatient:
                    ProgramID = AppConstant.Program.CpoeInPatient;
                    break;
                case CpoeTypeEnum.Emergency:
                    ProgramID = AppConstant.Program.CpoeEmergency;
                    break;
                case CpoeTypeEnum.Outpatient:
                    ProgramID = AppConstant.Program.CpoeOutPatient;
                    break;
            }
            // Program Fiture
            IsSingleRecordMode = true; //Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Integrated Notes for : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboInputType.Items.Add(new RadComboBoxItem("Notes", "Notes"));
                cboInputType.Items.Add(new RadComboBoxItem("SBAR", "SBAR"));
                if (!string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID))
                {
                    cboInputType.Items.Add(new RadComboBoxItem("SOAP", "SOAP"));
                }
            }
        }

        private string LastRegistrationInfoMedicID
        {
            get
            {
                if (ViewState["recid"] != null && !string.IsNullOrEmpty(ViewState["recid"].ToString()))
                    return ViewState["recid"].ToString();

                return Request.QueryString["recid"];
            }
            set { ViewState["recid"] = value; }
        }
        private string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        protected void cboInputType_OnSelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            FormatInterface();
        }
        private void FormatInterface()
        {
            row2.Visible = true;
            row3.Visible = true;
            row4.Visible = true;

            switch (cboInputType.SelectedValue)//(MedicalNotesInputType)
            {
                case "SBAR":
                    {
                        lblInfo1.Text = "S";
                        lblInfo2.Text = "B";
                        lblInfo3.Text = "A";
                        lblInfo4.Text = "R";
                        break;
                    }
                case "SOAP":
                    {
                        lblInfo1.Text = "S";
                        lblInfo2.Text = "O";
                        lblInfo3.Text = "A";
                        lblInfo4.Text = "P";
                        break;
                    }
                default:
                    {
                        lblInfo1.Text = "Notes";
                        row2.Visible = false;
                        row3.Visible = false;
                        row4.Visible = false;
                        break;
                    }
            }
        }


        protected void grdVitalSign_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)sender).DataSource = VitalSign();
        }

        private DataTable VitalSign()
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);

            var qr = new VitalSignQuery("vs");
            var qrValue = new RegistrationInfoMedicVitalSignQuery("v");
            var svt = new ServiceUnitVitalSignQuery("a");

            qr.InnerJoin(svt).On(qr.VitalSignID == svt.VitalSignID && svt.ServiceUnitID == reg.ServiceUnitID);
            qr.LeftJoin(qrValue).On(qrValue.RegistrationInfoMedicID == LastRegistrationInfoMedicID && qr.VitalSignID == qrValue.VitalSignID);
            //qr.Where(qr.IsMonitoring == true);
            qr.Select(
                qr.VitalSignID, qr.VitalSignName, qr.VitalSignInitial,
                qr.VitalSignUnit, qr.NumDecimalDigits, qr.NumMaxLength,
                qr.NumMaxValue, qr.NumMinValue, qr.NumType, qr.ValueType,
                qr.EntryMask, qrValue.VitalSignValueText, qrValue.VitalSignValueNum);
            var dtb = qr.LoadDataTable();
            return dtb;
        }

        protected void grdVitalSign_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;

                var ctlNum = (RadNumericTextBox)item.FindControl("txtValueType_NUM");
                var ctlTxt = (RadMaskedTextBox)item.FindControl("txtValueType_TXT");

                ctlNum.Visible = false;
                ctlTxt.Visible = false;
                switch (item["ValueType"].Text)
                {
                    case "NUM":
                        ctlNum.Visible = true;
                        var decDigit = item["NumDecimalDigits"].Text;
                        if (!string.IsNullOrEmpty(decDigit) && !decDigit.ToLower().Equals("&nbsp;"))
                            ctlNum.NumberFormat.DecimalDigits = Convert.ToInt32(decDigit);

                        var minValue = item["NumMinValue"].Text;
                        if (!string.IsNullOrEmpty(minValue) && !minValue.ToLower().Equals("&nbsp;"))
                            ctlNum.MinValue = Convert.ToInt32(minValue);

                        var maxValue = item["NumMaxValue"].Text;
                        if (!string.IsNullOrEmpty(maxValue) && !maxValue.ToLower().Equals("&nbsp;"))
                            ctlNum.MaxValue = Convert.ToInt32(maxValue);

                        var maxLength = item["NumMaxLength"].Text;
                        if (!string.IsNullOrEmpty(maxLength) && !maxLength.ToLower().Equals("&nbsp;"))
                            ctlNum.MaxLength = Convert.ToInt32(maxLength);

                        if (!IsPostBack)
                        {
                            var value = item["VitalSignValueNum"].Text;
                            if (!string.IsNullOrEmpty(value) && !value.ToLower().Equals("&nbsp;"))
                                ctlNum.Value = Convert.ToDouble(value);
                            else
                                ctlNum.Value = null;
                        }
                        break;
                    case "TXT":
                        ctlTxt.Visible = true;
                        if (!IsPostBack)
                        {
                            var value = item["VitalSignValueText"].Text;
                            if (!string.IsNullOrEmpty(value) && !value.ToLower().Equals("&nbsp;"))
                                ctlTxt.Text = value;
                            else
                                ctlTxt.Text = string.Empty;
                        }
                        break;
                }
            }
        }
    }
}
