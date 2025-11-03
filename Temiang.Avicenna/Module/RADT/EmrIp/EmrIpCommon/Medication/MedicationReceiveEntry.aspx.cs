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
using System.Data.SqlTypes;
using System.Text;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class MedicationReceiveEntry : BasePageDialogEntry
    {
        public int MedicationReceiveNo
        {
            get
            {
                if (!IsPostBack)
                    ViewState["medrecno"] = Request.QueryString["medrecno"];

                return ViewState["medrecno"].ToInt();
            }
            set { ViewState["medrecno"] = value; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            //IsSingleRecordMode = true; //Save then close
            //ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;

            //ToolBar.EditVisible = false;
            //ToolBar.AddVisible = false;
            // -------------------

            ToolBarMenuSave.ValidationGroup = "Entry";
            
            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Medication Receive of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //var consumes = new ConsumeMethodCollection();
                //consumes.Query.OrderBy("<LEFT(SRConsumeMethodName, 5)>");
                //consumes.LoadAll();

                //foreach (var consume in consumes)
                //{
                //    cboConsumeMethod.Items.Add(
                //        new RadComboBoxItem(consume.SRConsumeMethodName, consume.SRConsumeMethod));
                //}
            }
        }


        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            // Hanya tuk single entry
            var ent = new MedicationReceive();
            ent.LoadByPrimaryKey(MedicationReceiveNo);
            txtReceiveDateTime.SelectedDate = ent.ReceiveDateTime;
            if (!string.IsNullOrEmpty(ent.ItemID))
                ComboBox.PopulateWithOneItem(cboItemID, ent.ItemID);
            txtItemDescription.Text = ent.ItemDescription;
            txtReceiveQty.Value = Convert.ToDouble(ent.ReceiveQty);
            txtConsumeQty.Text = ent.ConsumeQtyInString;
            ComboBox.PopulateWithOneStandardReference(cboSRItemUnit, AppEnum.StandardReference.ItemUnit.ToString(), ent.SRConsumeUnit);
            ComboBox.PopulateWithOneConsumeMethod(cboConsumeMethod, ent.SRConsumeMethod);


            var mdp = new MedicationReceiveFromPatient();
            mdp.LoadByPrimaryKey(MedicationReceiveNo);
            txtCondition.Text = mdp.Condition;
            txtLastConsumeDateTime.SelectedDate = mdp.LastConsumeDateTime;
            txtExpireDate.SelectedDate = mdp.ExpireDate;
        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {
            var timeNow = (new DateTime()).NowAtSqlServer();
            txtReceiveDateTime.SelectedDate = timeNow.Date;
            txtReceiveQty.Value = 0;
            txtConsumeQty.Text = "1";

            cboItemID.SelectedIndex = -1;
            cboItemID.Text = string.Empty;
            txtItemDescription.Text = string.Empty;
            cboSRItemUnit.SelectedIndex = -1;
            cboSRItemUnit.Text = string.Empty;
            cboConsumeMethod.SelectedIndex = -1;
            cboConsumeMethod.Text = string.Empty;
            txtLastConsumeDateTime.Clear();
            txtExpireDate.Clear();
            txtCondition.Text = string.Empty;


            MedicationReceiveNo = 0;
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save(true);
        }

        private void Save(bool isNewRecord=false)
        {
            var ent = new MedicationReceive();
            if (isNewRecord || !ent.LoadByPrimaryKey(MedicationReceiveNo))
            {
                ent.RegistrationNo = RegistrationNo;
                ent.MedicationReceiveNo = NewMedicationReceiveNo();
                ent.BalanceQty = Convert.ToDecimal(txtReceiveQty.Value);
            }
            ent.ReceiveDateTime = txtReceiveDateTime.SelectedDate;
            ent.StartDateTime = txtReceiveDateTime.SelectedDate;
            ent.ItemID = cboItemID.SelectedValue;
            ent.ItemDescription = txtItemDescription.Text;
            ent.ReceiveQty = Convert.ToDecimal(txtReceiveQty.Value);
            ent.ConsumeQty = Convert.ToDecimal(new BusinessObject.Common.Fraction(txtConsumeQty.Text));
            ent.ConsumeQtyInString  = txtConsumeQty.Text;
            ent.SRConsumeUnit = cboSRItemUnit.SelectedValue;
            ent.SRConsumeMethod = cboConsumeMethod.SelectedValue;
            ent.IsVoid = false;
            ent.IsContinue = false;
            ent.Save();

            var mdp = new MedicationReceiveFromPatient();
            if (!mdp.LoadByPrimaryKey(MedicationReceiveNo))
            {
                mdp.MedicationReceiveNo = ent.MedicationReceiveNo;

            }
            mdp.Condition = txtCondition.Text;
            mdp.LastConsumeDateTime = txtLastConsumeDateTime.SelectedDate;
            mdp.ExpireDate = txtExpireDate.SelectedDate;
            mdp.Save();

            MedicationReceiveNo = ent.MedicationReceiveNo.ToInt();
        }


        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Save();
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

        private int NewMedicationReceiveNo()
        {
            var qr = new MedicationReceiveQuery("a");
            var fb = new MedicationReceive();
            qr.es.Top = 1;
            qr.OrderBy(qr.MedicationReceiveNo.Descending);

            if (fb.Load(qr))
            {
                return fb.MedicationReceiveNo.ToInt() + 1;
            }
            return 1;
        }

        protected void cboItemID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtItemDescription.Text = cboItemID.Text;
        }


        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtReceiveQty.Value==0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = @"Receive Qty must >0";
            }
            if (string.IsNullOrWhiteSpace(txtConsumeQty.Text) || txtConsumeQty.Text == "0")
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = @"Consume Qty must >0";
            }
            if (string.IsNullOrEmpty(cboConsumeMethod.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = @"Consume Method is not selected properly";
            }

            if (string.IsNullOrEmpty(cboSRItemUnit.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = @"Unit is not selected properly";
            }
        }
    }
}
