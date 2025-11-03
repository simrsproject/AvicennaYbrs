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
    public partial class PrescriptionProgress : BasePageDialogEntry
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
                    this.Title = "Prescription Progress of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
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
            grdPrescriptionProgress.Columns[0].Display = isEdited; // Selected
            grdPrescriptionProgress.Columns[1].Display = !isEdited; // IsSelected
        }
        protected override void OnMenuNewClick()
        {
            grdPrescriptionProgress.Rebind();
        }


        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            args.IsCancel = !Save(args);
        }

        private bool Save(ValidateArgs args)
        {
            SavePrescriptionProgress();

            // Return Value
            var progress = string.Empty;
            var stdi = new AppStandardReferenceItemQuery("stdi");
            var prgs = new TransPrescriptionProgressQuery("a");
            stdi.InnerJoin(prgs)
                .On(stdi.ItemID == prgs.SRPrescriptionProgress && stdi.StandardReferenceID == "PrescriptionProgress");
            stdi.Select(stdi.ItemName,
                "<CONVERT(BIT,CASE WHEN a.SRPrescriptionProgress IS NULL THEN 0 ELSE 1 END) as IsExist>");
            stdi.OrderBy(stdi.LineNumber.Ascending);
            stdi.Where(prgs.PrescriptionNo == PrescriptionNo);

            var dtbProgress = stdi.LoadDataTable();
            foreach (DataRow row in dtbProgress.Rows)
            {
                if (true.Equals(row["IsExist"]))
                    progress = string.Concat(progress, ", ", row["ItemName"]);
            }
            if (!string.IsNullOrEmpty(progress))
            {
                progress = progress.Substring(2);
            }
            hdfReturnValue.Value = progress;
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Format("oArg.value = document.getElementById('{0}').value;", hdfReturnValue.ClientID);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            args.IsCancel = !Save(args);
            grdPrescriptionProgress.Rebind();
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
            grdPrescriptionProgress.Rebind();
        }
        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var line = new TransPrescriptionProgressCollection();
            line.Query.Where(line.Query.PrescriptionNo == PrescriptionNo);
            line.LoadAll();
            line.MarkAllAsDeleted();
            line.Save();

            // Update Prescription
            var presc = new TransPrescription();
            presc.LoadByPrimaryKey(PrescriptionNo);
            presc.str.ReviewByUserID = string.Empty;
            presc.Save();

            grdPrescriptionProgress.Rebind();
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


        #region Entry
        private void SavePrescriptionProgress()
        {
            using (var trans = new esTransactionScope())
            {
                foreach (GridDataItem item in grdPrescriptionProgress.MasterTableView.Items)
                {
                    var progress = new TransPrescriptionProgress();
                    var itemID = item.GetDataKeyValue("ItemID").ToString();
                    var lineNumber = item.GetDataKeyValue("LineNumber").ToString();
                    var chkIsSelected = ((CheckBox)item.FindControl("chkIsSelected"));
                    if (chkIsSelected.Checked)
                    {
                        // Add if not exist
                        if (!progress.LoadByPrimaryKey(PrescriptionNo, itemID))
                        {
                            progress = new TransPrescriptionProgress();
                            progress.PrescriptionNo = PrescriptionNo;
                            progress.SRPrescriptionProgress = itemID;
                            progress.ProgressNo = MethodExts.ToInt(lineNumber);
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
        }



        private DataTable PrescriptionProgressDataTable(string prescriptionNo)
        {
            var que = new AppStandardReferenceItemQuery("sri");
            var qrProgress = new TransPrescriptionProgressQuery("a");
            que.LeftJoin(qrProgress).On(que.ItemID == qrProgress.SRPrescriptionProgress && qrProgress.PrescriptionNo == prescriptionNo);

            var qrUser = new AppUserQuery("u");
            que.LeftJoin(qrUser).On(qrProgress.LastUpdateByUserID == qrUser.UserID);

            que.Where(que.StandardReferenceID == "PrescriptionProgress");
            que.OrderBy(que.LineNumber.Ascending);
            que.Select(que.ItemID, que.ItemName,que.LineNumber, qrProgress.LastUpdateByUserID, qrUser.UserName.As("ProgressByUserName"), "<CONVERT(BIT,CASE WHEN a.SRPrescriptionProgress IS NULL THEN 0 ELSE 1 END) as IsSelected>");
            return que.LoadDataTable();
        }

        #endregion
        protected void grdPrescriptionProgress_ItemDataBound(object sender, GridItemEventArgs e)
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
        protected void grdPrescriptionProgress_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPrescriptionProgress.DataSource = PrescriptionProgressDataTable(PrescriptionNo);

        }
    }
}
