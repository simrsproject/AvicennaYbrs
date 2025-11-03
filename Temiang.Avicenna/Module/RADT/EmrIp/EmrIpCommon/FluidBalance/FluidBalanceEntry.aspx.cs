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
    public partial class FluidBalanceEntry : BasePageDialogEntry
    {
        public int SequenceNo
        {
            get
            {
                return Request.QueryString["seqno"].ToInt();
            }
        }


        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
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
                    this.Title = "Fluid Balance of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            // Hanya tuk single entry
            var fb = new PatientFluidBalance();
            fb.LoadByPrimaryKey(RegistrationNo, SequenceNo);
            txtInOutDate.SelectedDate = fb.InOutDate;
            txtSequenceNo.Value = fb.SequenceNo;
            txtSchemaInfus.Text = fb.SchemaInfus;
            trSchemaInfusOldVersion.Visible = !string.IsNullOrEmpty(txtSchemaInfus.Text);
            txtLastTemp.Value = fb.LastTemp.ToDouble();
            txtIwlForHour.Value = fb.IwlForHour.ToDouble();
            txtBodyWeight.Value = fb.BodyWeight.ToDouble();
            txtNormalTemp.Value = fb.NormalTemp.ToDouble();
            txtIwlConstant.Value = fb.IwlConstant.ToDouble();

            PopulateSchemaInfusGrid();
        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemSchemaInfus(newVal);
        }
        protected override void OnMenuNewClick()
        {
            trSchemaInfusOldVersion.Visible = false; //Untuk data baru dihide
            var timeNow = (new DateTime()).NowAtSqlServer();
            txtInOutDate.SelectedDate = timeNow.Date;
            txtSchemaInfus.Text = string.Empty;
            txtSequenceNo.Value = NewSequenceNo();
            txtIwlForHour.Value = 24;

            var lastTemp = VitalSign.LastVitalSignValue(RegistrationNo, FromRegistrationNo, VitalSign.VitalSignEnum.Temperature, timeNow);
            txtLastTemp.Value = lastTemp == 0 ? AppParameter.GetParameterValue(AppParameter.ParameterItem.NormalTemperature).ToDouble() : lastTemp;

            var val = VitalSign.LastVitalSignValue(RegistrationNo, FromRegistrationNo, VitalSign.VitalSignEnum.BodyWeight, timeNow);
            txtBodyWeight.Value = val;

            txtNormalTemp.Value =
                AppParameter.GetParameterValue(AppParameter.ParameterItem.NormalTemperature).ToDouble();

            txtIwlConstant.Value = 0;

        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save();
        }

        private void Save()
        {
            var fb = new PatientFluidBalance();
            if (!fb.LoadByPrimaryKey(RegistrationNo, SequenceNo))
            {
                fb.RegistrationNo = RegistrationNo;
                fb.SequenceNo = NewSequenceNo();
                fb.InOutDate = txtInOutDate.SelectedDate.Value.Date;
            }
            fb.SchemaInfus = txtSchemaInfus.Text;
            fb.LastTemp = txtLastTemp.Value.ToDecimal();
            fb.IwlForHour = txtIwlForHour.Value.ToInt();
            fb.BodyWeight = txtBodyWeight.Value.ToDecimal();
            fb.NormalTemp = txtNormalTemp.Value.ToDecimal();
            fb.IwlConstant = txtIwlConstant.Value.ToInt();
            fb.Save();

            //SchemaInfus
            foreach (var schemaInfus in SchemaInfusColl)
            {
                schemaInfus.RegistrationNo = RegistrationNo;
                schemaInfus.SequenceNo = fb.SequenceNo;
            }
            SchemaInfusColl.Save();

            // Info for resfresh parent page
            hdnEditRegistrationNo.Value = fb.RegistrationNo;
            hdnEditSequenceNo.Value = fb.SequenceNo.ToString();
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            var script = string.Format(@"oArg.editRegNo = document.getElementById('{0}').value;
            oArg.editSeqNo = document.getElementById('{1}').value;", hdnEditRegistrationNo.ClientID,
                hdnEditSequenceNo.ClientID);
            return script;
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
            MedicalRecordEditableValidate(args, RegistrationCurrent);
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
            MedicalRecordAddableValidate(args, RegistrationCurrent);
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

        private int NewSequenceNo()
        {
            var qr = new PatientFluidBalanceQuery("a");
            var fb = new PatientFluidBalance();
            qr.Where(qr.RegistrationNo == RegistrationNo);
            qr.es.Top = 1;
            qr.OrderBy(qr.SequenceNo.Descending);

            if (fb.Load(qr))
            {
                return fb.SequenceNo.ToInt() + 1;
            }
            return 1;
        }

        protected void lbtnResetLastTemp_OnClick(object sender, EventArgs e)
        {
            var val = VitalSign.LastVitalSignValue(RegistrationNo, FromRegistrationNo, VitalSign.VitalSignEnum.Temperature, txtInOutDate.SelectedDate.Value);
            txtLastTemp.Value = val;
        }

        protected void lbtnResetBodyWeight_OnClick(object sender, EventArgs e)
        {
            var val = VitalSign.LastVitalSignValue(RegistrationNo, FromRegistrationNo, VitalSign.VitalSignEnum.BodyWeight, txtInOutDate.SelectedDate.Value);
            txtBodyWeight.Value = val;
        }

        protected void lbtnResetNormalTemp_OnClick(object sender, EventArgs e)
        {
            txtNormalTemp.Value =
                AppParameter.GetParameterValue(AppParameter.ParameterItem.NormalTemperature).ToDouble();
        }

        #region Record Detail Method Function PatientFluidBalanceSchemaInfus

        private PatientFluidBalanceSchemaInfusCollection SchemaInfusColl
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collSchemaInfus"];
                    if (obj != null)
                    {
                        return ((PatientFluidBalanceSchemaInfusCollection)(obj));
                    }
                }

                var coll = new PatientFluidBalanceSchemaInfusCollection();
                var query = new PatientFluidBalanceSchemaInfusQuery("a");

                query.Where(query.RegistrationNo == RegistrationNo, query.SequenceNo == SequenceNo);
                coll.Load(query);

                Session["collSchemaInfus"] = coll;
                return coll;
            }
            set { Session["collSchemaInfus"] = value; }
        }

        private void RefreshCommandItemSchemaInfus(AppEnum.DataMode newVal)
        {

            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdSchemaInfus.Columns[0].Visible = isVisible; //Edit
            grdSchemaInfus.Columns[grdSchemaInfus.Columns.Count - 2].Visible = isVisible; //delete
            grdSchemaInfus.MasterTableView.CommandItemDisplay = isVisible
                                                                               ? GridCommandItemDisplay.Top
                                                                               : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdSchemaInfus.Rebind();

        }

        private void PopulateSchemaInfusGrid()
        {
            //Display Data Detail
            SchemaInfusColl = null; //Reset Record Detail
            grdSchemaInfus.DataSource = SchemaInfusColl; //Requery
            grdSchemaInfus.MasterTableView.IsItemInserted = false;
            grdSchemaInfus.MasterTableView.ClearEditItems();
            grdSchemaInfus.DataBind();
        }

        protected void grdSchemaInfus_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSchemaInfus.DataSource = SchemaInfusColl;
        }

        protected void grdSchemaInfus_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            int schemaInfusNo =
                Convert.ToInt32(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        PatientFluidBalanceSchemaInfusMetadata.ColumnNames.SchemaInfusNo]);
            PatientFluidBalanceSchemaInfus entity = FindPatientFluidBalanceSchemaInfus(schemaInfusNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdSchemaInfus_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            int schemaInfusNo =
                Convert.ToInt32(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][PatientFluidBalanceSchemaInfusMetadata.ColumnNames.SchemaInfusNo]);
            PatientFluidBalanceSchemaInfus entity = FindPatientFluidBalanceSchemaInfus(schemaInfusNo);

            if (entity != null)
            {
                entity.MarkAsDeleted();
            }

        }

        protected void grdSchemaInfus_InsertCommand(object source, GridCommandEventArgs e)
        {
            PatientFluidBalanceSchemaInfus entity = SchemaInfusColl.AddNew();
            SetEntityValue(entity, e);
            //Stay in insert mode

            e.Canceled = true;
            grdSchemaInfus.Rebind();
        }

        private PatientFluidBalanceSchemaInfus FindPatientFluidBalanceSchemaInfus(int schemaInfusNo)
        {
            PatientFluidBalanceSchemaInfusCollection coll = SchemaInfusColl;
            return coll.FirstOrDefault(rec => rec.SchemaInfusNo.Equals(schemaInfusNo));
        }


        private void SetEntityValue(PatientFluidBalanceSchemaInfus entity, GridCommandEventArgs e)
        {
            var userControl = (FluidBalanceSchemaInfusDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RegistrationNo = RegistrationNo;
                entity.SequenceNo = SequenceNo;
                entity.SchemaInfusNo = userControl.SchemaInfusNo;
                entity.SchemaInfusName = userControl.SchemaInfusName;
                entity.QtyVolume = userControl.QtyVolume;
                entity.QtyPerHour = userControl.QtyPerHour;

            }
        }

        #endregion

    }
}
