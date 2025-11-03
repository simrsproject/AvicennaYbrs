using System;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class FluidBalanceDetailEntry : BasePageDialogEntry
    {
        public int SequenceNo
        {
            get
            {
                return Request.QueryString["seqno"].ToInt();
            }
        }
        public int DetailSequenceNo
        {
            get
            {
                return Request.QueryString["dseqno"].ToInt();
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

                ComboBox.StandardReferenceItem(cboSRFluidInOutMethod, "FluidInOutMethod");

                // Schema Infus
                var query = new PatientFluidBalanceSchemaInfusQuery("a");
                query.Where(query.RegistrationNo == RegistrationNo, query.SequenceNo == SequenceNo);
                query.Select(query.SchemaInfusNo, query.SchemaInfusName);
                var dtb = query.LoadDataTable();

                cboSchemaInfus.Items.Add(new RadComboBoxItem(String.Empty, String.Empty));
                foreach (System.Data.DataRow row in dtb.Rows)
                {
                    cboSchemaInfus.Items.Add(new RadComboBoxItem(row["SchemaInfusName"].ToString(), row["SchemaInfusNo"].ToString()));
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

            var fbd = new PatientFluidBalanceDetail();
            if (fbd.LoadByPrimaryKey(RegistrationNo, SequenceNo, DetailSequenceNo))
            {
                txtInOutDateTime.SelectedDate = fbd.InOutDateTime;
                ComboBox.SelectedValue(cboSRFluidInOutMethod, fbd.SRFluidInOutMethod);
                ComboBox.SelectedValue(cboSchemaInfus, Convert.ToString(fbd.SchemaInfusNo));
                txtFluidName.Text = fbd.FluidName;
                txtFluidQty.Value = fbd.FluidQty.ToDouble();
                txtInOutQty.Value = fbd.InOutQty.ToDouble();
            }
        }


        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {
            var timeNow = (new DateTime()).NowAtSqlServer();
            txtInOutDateTime.SelectedDate = timeNow;
            ComboBox.SelectedValue(cboSRFluidInOutMethod, "INF");
            txtFluidName.Text = string.Empty;
            txtFluidQty.Value = 0;
            txtInOutQty.Value = 0;
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save();
        }
        private int NewDetailSequenceNo()
        {
            var qr = new PatientFluidBalanceDetailQuery("a");
            var fbd = new PatientFluidBalanceDetail();
            qr.Where(qr.RegistrationNo == RegistrationNo, qr.SequenceNo == SequenceNo);
            qr.es.Top = 1;
            qr.OrderBy(qr.DetailSequenceNo.Descending);

            if (fbd.Load(qr))
            {
                return fbd.DetailSequenceNo.ToInt() + 1;
            }
            return 1;
        }
        private void Save()
        {
            var fbd = new PatientFluidBalanceDetail();
            if (!fbd.LoadByPrimaryKey(RegistrationNo, SequenceNo, DetailSequenceNo))
            {
                fbd.RegistrationNo = RegistrationNo;
                fbd.SequenceNo = SequenceNo;
                fbd.DetailSequenceNo = NewDetailSequenceNo();
            }
            fbd.InOutDateTime = txtInOutDateTime.SelectedDate;
            fbd.SRFluidInOutMethod = cboSRFluidInOutMethod.SelectedValue;
            if (string.IsNullOrEmpty(cboSchemaInfus.SelectedValue))
                fbd.str.SchemaInfusNo = string.Empty;
            else
                fbd.SchemaInfusNo = cboSchemaInfus.SelectedValue.ToInt();

            fbd.FluidName = txtFluidName.Text;
            fbd.FluidQty = txtFluidQty.Value.ToDecimal();
            fbd.InOutQty = txtInOutQty.Value.ToDecimal();
            fbd.Save();
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

        protected void cboSRFluidInOutMethod_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (((RadComboBox)sender).SelectedValue == "INF")
            {
                // Hitung perkiraan pemakaian cairan infus
                txtInOutQty.Value = 100;
            }
        }
    }
}
