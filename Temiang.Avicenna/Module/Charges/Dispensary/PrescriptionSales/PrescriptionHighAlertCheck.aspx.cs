using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;
//using Temiang.Avicenna.Module.Payroll.Transaction;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges.Dispensary.PrescriptionSales
{
    public partial class PrescriptionHighAlertCheck : BasePageDialogEntry
    {
        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }
        public string PrescriptionNo
        {
            get
            {
                return Request.QueryString["prescno"];
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["prgid"]))
                ProgramID = Request.QueryString["prgid"];
            else
                ProgramID = AppConstant.Program.PrescriptionSales;

            // Program Fiture
            IsSingleRecordMode = true; //Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = true;

            //ToolBar.EditVisible = false;
            //ToolBar.AddVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                var patientID = PatientID;
                if (string.IsNullOrEmpty(patientID))
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(RegistrationNo);
                    patientID = reg.PatientID;
                }
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(patientID))
                {
                    this.Title = "High Alert Drug of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }


        #region override method
        public override void OnServerValidate(ValidateArgs args)
        {
        }
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            var isEdited = newVal != AppEnum.DataMode.Read;
            grdPrescriptionHighAlert.Columns[0].Display = isEdited; // Selected
            grdPrescriptionHighAlert.Columns[1].Display = !isEdited; // IsSelected
        }
        protected override void OnMenuNewClick()
        {
            grdPrescriptionHighAlert.Rebind();
        }


        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            args.IsCancel = !Save(args);
        }

        private bool Save(ValidateArgs args)
        {
            SavePrescriptionHighAlert();
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Format("oArg.value = document.getElementById('{0}').value;", hdfReturnValue.ClientID);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            args.IsCancel = !Save(args);
            grdPrescriptionHighAlert.Rebind();
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
            grdPrescriptionHighAlert.Rebind();
        }
        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var line = new TransPrescriptionHighAlertCollection();
            line.Query.Where(line.Query.PrescriptionNo == PrescriptionNo);
            line.LoadAll();
            line.MarkAllAsDeleted();
            line.Save();

            // Update Prescription
            var presc = new TransPrescription();
            presc.LoadByPrimaryKey(PrescriptionNo);
            presc.str.ReviewByUserID = string.Empty;
            presc.Save();

            grdPrescriptionHighAlert.Rebind();
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


        private void SavePrescriptionHighAlert()
        {
            using (var trans = new esTransactionScope())
            {
                foreach (GridDataItem item in grdPrescriptionHighAlert.MasterTableView.Items)
                {
                    var progress = new TransPrescriptionHighAlert();
                    var itemID = item.GetDataKeyValue("ItemID").ToString();
                    var chkIsSelected = ((CheckBox)item.FindControl("chkIsSelected"));
                    if (chkIsSelected.Checked)
                    {
                        // Add if not exist
                        if (!progress.LoadByPrimaryKey(PrescriptionNo, itemID))
                        {
                            progress = new TransPrescriptionHighAlert();
                            progress.PrescriptionNo = PrescriptionNo;
                            progress.SRPrescriptionHAlert = itemID;
                            progress.Save();
                        }
                    }
                    else
                    {
                        // Delete
                        if (progress.LoadByPrimaryKey(PrescriptionNo, itemID))
                        {
                            progress.MarkAsDeleted();
                            progress.Save();
                        }
                    }
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            hdfReturnValue.Value = string.Format("{0} @{1}", AppSession.UserLogin.UserName, DateTime.Now.ToString(AppConstant.DisplayFormat.DateTimeSecond));
        }



        private DataTable PrescriptionHighAlertDataTable(string prescriptionNo)
        {
            var que = new AppStandardReferenceItemQuery("sri");
            var qrPresc = new TransPrescriptionHighAlertQuery("a");
            que.LeftJoin(qrPresc).On(que.ItemID == qrPresc.SRPrescriptionHAlert && qrPresc.PrescriptionNo == prescriptionNo);

            var qrUser = new AppUserQuery("u");
            que.LeftJoin(qrUser).On(qrPresc.LastUpdateByUserID == qrUser.UserID);

            que.Where(que.StandardReferenceID == "PrescriptionHAlert");
            que.OrderBy(que.LineNumber.Ascending);
            que.Select(que.ItemID, que.ItemName, que.LineNumber, qrPresc.LastUpdateByUserID, qrUser.UserName, "<CONVERT(BIT,CASE WHEN a.SRPrescriptionHAlert IS NULL THEN 0 ELSE 1 END) as IsSelected>");
            return que.LoadDataTable();
        }

        protected void grdPrescriptionHighAlert_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (DataModeCurrent == AppEnum.DataMode.Read)
                return;

            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                var chkIsSelected = ((CheckBox)dataItem.FindControl("chkIsSelected"));
                chkIsSelected.Checked = ((CheckBox)(dataItem["IsSelected"].Controls[0])).Checked;
            }
        }
        protected void grdPrescriptionHighAlert_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPrescriptionHighAlert.DataSource = PrescriptionHighAlertDataTable(PrescriptionNo);

        }
    }
}
