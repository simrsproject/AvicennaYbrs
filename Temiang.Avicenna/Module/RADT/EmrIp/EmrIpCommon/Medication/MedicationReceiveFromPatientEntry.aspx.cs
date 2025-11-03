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
    public partial class MedicationReceiveFromPatientEntry : BasePageDialogHistEntry
    {
        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
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

        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (AppConstant.Program.PharmaceuticalCare.Equals(Request.QueryString["prgid"]))
                ProgramID = AppConstant.Program.PharmaceuticalCare;
            else
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            //IsSingleRecordMode = true; //Save then close
            //ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            IsSingleRecordMode = false;

            //ToolBar.EditVisible = false;
            //ToolBar.AddVisible = false;
            // -------------------

            Splitter.Orientation = Orientation.Vertical;
            Splitter.Items[0].Width = Unit.Pixel(450);

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Medication Receive of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
                StandardReference.InitializeIncludeSpace(cboSRMedicationConsume, AppEnum.StandardReference.MedicationConsume);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void PopulateEntryControl()
        {
            var ent = new MedicationReceive();
            ent.LoadByPrimaryKey(MedicationReceiveNo);
            txtReceiveDateTime.SelectedDate = ent.ReceiveDateTime;
            if (!string.IsNullOrEmpty(ent.ItemID))
                ComboBox.PopulateWithOneItem(cboItemID, ent.ItemID);
            txtItemDescription.Text = ent.ItemDescription;
            txtReceiveQty.Value = Convert.ToDouble(ent.ReceiveQty);
            txtConsumeQty.Value = Convert.ToDouble(ent.ConsumeQty);

            ComboBox.PopulateWithOneConsumeUnit(cboSRConsumeUnit, ent.SRConsumeUnit);

            ComboBox.PopulateWithOneConsumeMethod(cboConsumeMethod, ent.SRConsumeMethod);

            ComboBox.SelectedValue(cboSRMedicationConsume, ent.SRMedicationConsume);

            var mdp = new MedicationReceiveFromPatient();
            mdp.LoadByPrimaryKey(MedicationReceiveNo);
            txtCondition.Text = mdp.Condition;
            txtTemp.Text = mdp.Temp;
            txtBeyondUseDate.SelectedDate = mdp.BeyondUseDate;
            txtExpireDate.SelectedDate = mdp.ExpireDate;
            optIsAppropriate.SelectedValue = (mdp.IsAppropriate ?? false) ? "1" : "0";

            txtLastConsumeDateTime.SelectedDate = mdp.LastConsumeDateTime;
            txtDuration.Text = mdp.Duration;
            txtReason.Text = mdp.Reason;
            chkIsManagedByPatient.Checked = mdp.IsManagedByPatient ?? false;
        }

        #region override method

        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            PopulateEntryControl();
            grdMedicationStatus.Rebind();

        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            grdMedicationStatus.Columns[0].Visible = newVal == AppEnum.DataMode.Read;
        }
        protected override void OnMenuNewClick()
        {
            var timeNow = (new DateTime()).NowAtSqlServer();
            txtReceiveDateTime.SelectedDate = timeNow.Date;
            txtStartDateTime.SelectedDate = timeNow.Date;
            txtReceiveQty.Value = 0;
            txtConsumeQty.Value = 0;

            cboItemID.SelectedIndex = -1;
            cboItemID.Text = string.Empty;
            txtItemDescription.Text = string.Empty;
            cboSRConsumeUnit.SelectedIndex = -1;
            cboSRConsumeUnit.Text = string.Empty;
            cboConsumeMethod.SelectedIndex = -1;
            cboConsumeMethod.Text = string.Empty;
            txtLastConsumeDateTime.Clear();
            txtExpireDate.Clear();
            txtCondition.Text = string.Empty;

            MedicationReceiveNo = 0;
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            args.IsCancel = !Save(args, true);
            if (!args.IsCancel)
            {
                grdMedicationStatus.Rebind();
            }
        }

        private bool Save(ValidateArgs args, bool isNewRecord = false)
        {
            var newRecQty = Convert.ToDecimal(txtReceiveQty.Value);
            var ent = new MedicationReceive();
            if (isNewRecord || !ent.LoadByPrimaryKey(MedicationReceiveNo))
            {
                ent.RegistrationNo = RegistrationNo;
                ent.MedicationReceiveNo = NewMedicationReceiveNo();
                ent.BalanceQty = newRecQty;
                ent.BalanceRealQty = newRecQty;
            }
            else
            {
                ent.BalanceQty = ent.BalanceQty + (newRecQty - ent.ReceiveQty);
                ent.BalanceRealQty = ent.BalanceRealQty + (newRecQty - ent.ReceiveQty);
            }

            ent.ReceiveQty = newRecQty;

            ent.ReceiveDateTime = txtReceiveDateTime.SelectedDate;
            ent.ItemID = cboItemID.SelectedValue;
            ent.ItemDescription = txtItemDescription.Text;

            ent.ConsumeQty = Convert.ToDecimal(txtConsumeQty.Value);
            ent.ConsumeQtyInString = txtConsumeQty.Value.ToString();
            ent.SRConsumeUnit = cboSRConsumeUnit.SelectedValue;
            ent.SRConsumeMethod = cboConsumeMethod.SelectedValue;
            ent.IsVoid = false;
            ent.IsContinue = false; // Obat yg dibawa oleh pasien hasus direcon dan diapprove dulu baru bisa lanjut
            ent.SRMedicationConsume = cboSRMedicationConsume.SelectedValue;
            ent.StartDateTime = txtStartDateTime.SelectedDate;
            ent.Save();

            var mdp = new MedicationReceiveFromPatient();
            if (!mdp.LoadByPrimaryKey(MedicationReceiveNo))
            {
                mdp.MedicationReceiveNo = ent.MedicationReceiveNo;

            }
            mdp.Condition = txtCondition.Text;
            mdp.Temp = txtTemp.Text;

            if (txtBeyondUseDate.IsEmpty)
                mdp.str.BeyondUseDate = String.Empty;
            else
                mdp.BeyondUseDate = txtBeyondUseDate.SelectedDate;

            if (txtExpireDate.IsEmpty)
                mdp.str.ExpireDate = String.Empty;
            else
                mdp.ExpireDate = txtExpireDate.SelectedDate;

            mdp.IsAppropriate = "1".Equals(optIsAppropriate.SelectedValue);

            mdp.LastConsumeDateTime = txtLastConsumeDateTime.SelectedDate;

            mdp.Duration = txtDuration.Text;
            mdp.Reason = txtReason.Text;
            mdp.IsManagedByPatient = chkIsManagedByPatient.Checked;
            mdp.Save();

            MedicationReceiveNo = ent.MedicationReceiveNo.ToInt();
            return true;
        }


        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            args.IsCancel = !Save(args, false);
            if (!args.IsCancel)
            {
                grdMedicationStatus.Rebind();
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


        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var nmd = new MedicationReceive();
            if (nmd.LoadByPrimaryKey(MedicationReceiveNo))
            {
                nmd.IsVoid = true;
                nmd.Save();
            }

            grdMedicationStatus.Rebind();
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
            return MedicationReceiveNo > 0;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return MedicationReceiveNo > 0;
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

            var imed = new ItemProductMedic();
            if (imed.LoadByPrimaryKey(cboItemID.SelectedValue))
            {
                ComboBox.PopulateWithOneConsumeUnit(cboSRConsumeUnit, imed.SRDosageUnit);
            }
        }

        public override void OnServerValidate(ValidateArgs args)
        {
            if (txtConsumeQty.Value == 0)
            {
                args.IsCancel = true;
                args.MessageText = @"Qty Every Consume must >0";
                txtConsumeQty.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cboConsumeMethod.SelectedValue))
            {
                args.IsCancel = true;
                args.MessageText = @"Consume Method is not selected properly";
                cboConsumeMethod.Focus();
                return;
            }

            if (string.IsNullOrEmpty(cboSRConsumeUnit.SelectedValue))
            {
                args.IsCancel = true;
                args.MessageText = @"Consume Unit is not selected properly";
                cboSRConsumeUnit.Focus();
                return;
            }
        }


        #region List
        protected void grdMedicationStatus_ItemCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            if (e.CommandName == "View")
            {
                MedicationReceiveNo = item.OwnerTableView.DataKeyValues[item.ItemIndex]["MedicationReceiveNo"].ToInt();

                PopulateEntryControl();
                RefreshMenuStatus();
            }
        }
        protected void grdMedicationStatus_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                if (true.Equals(item.GetDataKeyValue("IsVoid")))
                {
                    item.Style.Add(HtmlTextWriterStyle.TextDecoration, "line-through");
                    item.Style.Add(HtmlTextWriterStyle.Color, "gray");
                }
            }
        }

        protected void grdMedicationStatus_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdMedicationStatus.DataSource = MedicationReceiveDataTable();
        }

        private DataTable MedicationReceiveDataTable()
        {
            var query = new MedicationReceiveQuery("a");

            var cm = new ConsumeMethodQuery("cm");
            query.LeftJoin(cm).On(query.SRConsumeMethod == cm.SRConsumeMethod);

            var fp = new MedicationReceiveFromPatientQuery("fp");
            query.InnerJoin(fp).On(query.MedicationReceiveNo == fp.MedicationReceiveNo);

            var mc = new AppStandardReferenceItemQuery("mc");
            query.LeftJoin(mc).On(query.SRMedicationConsume == mc.ItemID &&
                mc.StandardReferenceID == "MedicationConsume");

            query.Select
            (
                query,
                cm.SRConsumeMethodName,
                mc.ItemName.As("SRMedicationConsumeName"),
                fp.Duration, fp.Condition, fp.Reason, fp.IsManagedByPatient

            );

            query.Where(query.RegistrationNo == RegistrationNo);


            query.OrderBy(query.ItemDescription.Ascending);
            var dtb = query.LoadDataTable();

            return dtb;
        }



        protected void grdMedicationStatus_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string medicationReceiveNo = dataItem.GetDataKeyValue("MedicationReceiveNo").ToString();

            if (e.DetailTableView.Name.Equals("grdMedicationStatusUsed"))
            {
                var query = new MedicationReceiveUsedQuery("a");

                var setup = new AppUserQuery("u1");
                query.LeftJoin(setup).On(query.SetupByUserID == setup.UserID);
                var verif = new AppUserQuery("u2");
                query.LeftJoin(verif).On(query.VerificationByUserID == verif.UserID);
                var realize = new AppUserQuery("u3");
                query.LeftJoin(realize).On(query.RealizedByUserID == realize.UserID);

                query.Select(query.SelectAll(), setup.UserName.As("SetupByUserName"), verif.UserName.As("VerificationByUserName"), realize.UserName.As("RealizedByUserName"));
                query.Where(query.MedicationReceiveNo == medicationReceiveNo);
                query.OrderBy(query.SequenceNo.Descending);

                e.DetailTableView.DataSource = query.LoadDataTable();
            }
        }
        #endregion
    }
}
