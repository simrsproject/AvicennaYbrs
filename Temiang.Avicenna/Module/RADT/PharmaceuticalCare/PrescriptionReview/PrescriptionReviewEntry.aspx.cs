using System;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Temiang.Avicenna.Module.RADT.PharmaceuticalCare
{
    public partial class PrescriptionReviewEntry : BasePageDialogEntry
    {
        public override string RegistrationNo => Request.QueryString["regno"];
        public override string PatientID => Request.QueryString["patid"];
        protected string PrescriptionNo => Request.QueryString["pn"];

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PrecriptionReview;

            // Program Fiture
            IsMedicalRecordEntry = true; //Activate deadline edit & add
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;

            ToolBar.EditVisible = true;
            ToolBar.AddVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                this.Title = "Prescription Review";
            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {

        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            // Apply readonly entry
            grdEntry.Rebind();
        }
        protected override void OnMenuNewClick()
        {
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save(args);
        }

        private bool Save(ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                var pr = new PrescriptionReviewQuery("pr");
                pr.Where(pr.PrescriptionNo == PrescriptionNo);

                var coll = new PrescriptionReviewCollection();
                coll.Load(pr);

                coll.MarkAllAsDeleted();

                foreach (GridDataItem item in grdEntry.MasterTableView.Items)
                {
                    var inf = ((RadTextBox)item.FindControl("txtInformation")).Text;
                    var rightSelected = ((RadRadioButtonList)item.FindControl("optRight")).SelectedValue;

                    if (!string.IsNullOrEmpty(rightSelected) || !string.IsNullOrEmpty(rightSelected))
                    {
                        var itemID = item.GetDataKeyValue("ItemID").ToString();
                        var ent = coll.AddNew();

                        ent.PrescriptionNo = PrescriptionNo;
                        ent.SRPrescReview = itemID;
                        ent.Information = inf;

                        if (string.IsNullOrEmpty(rightSelected))
                            ent.str.IsRight = string.Empty;
                        else
                            ent.IsRight = rightSelected.Equals("1");
                    }
                }

                coll.Save();

                var presc = new TransPrescription();
                presc.LoadByPrimaryKey(PrescriptionNo);
                presc.IsReviewed = true;
                presc.ReviewedDateTime = DateTime.Now;
                presc.ReviewedByUserID = AppSession.UserLogin.UserID;
                presc.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            grdEntry.Rebind();
            return true;
        }



        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Save(args);
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

        #region grdPrescriptionItem
        protected void grdPrescriptionItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new TransPrescriptionItemQuery("a");
            var qItem = new ItemQuery("b");
            var qItemI = new ItemQuery("c");

            var emb = new EmbalaceQuery("x");
            var cons = new ConsumeMethodQuery("y");

            query.Select
                (
                    query,
                    qItem.ItemName.As("ItemName"),
                    qItemI.ItemName.As("ItemInterventionName"),
                    (query.ResultQty * (query.Price - query.DiscountAmount)).As("Total"),
                    "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                    emb.EmbalaceLabel.As("EmbalaceLabel"),
                    cons.SRConsumeMethodName.As("SRConsumeMethodName")
                );
            query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
            query.LeftJoin(qItemI).On(query.ItemInterventionID == qItemI.ItemID);
            query.LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID);
            query.LeftJoin(cons).On(query.SRConsumeMethod == cons.SRConsumeMethod);
            query.Where(query.PrescriptionNo == PrescriptionNo);
            query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

            grdPrescriptionItem.DataSource = query.LoadDataTable();
        }

        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            else
                return "&nbsp;&nbsp;&nbsp;" + itemName.ToString();
        }

        #endregion

        protected void grdEntry_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = ((RadGrid)source);
            grd.DataSource = PrescriptionReviewDataTable();

        }
        private DataTable PrescriptionReviewDataTable()
        {
            var cacheName = string.Format("prv_{0}_{1}", PrescriptionNo, AppSession.UserLogin.UserID);
            if (!IsPostBack || Cache[cacheName] == null)
            {
                var que = new AppStandardReferenceItemQuery("sri");
                var qrRev = new PrescriptionReviewQuery("rev");

                que.LeftJoin(qrRev).On(que.ItemID == qrRev.SRPrescReview & qrRev.PrescriptionNo == PrescriptionNo);

                que.Where(que.StandardReferenceID == "PrescReview");
                que.OrderBy(que.LineNumber.Ascending);
                que.Select(que.ItemID, que.ItemName, qrRev.Information,
                    "<CASE WHEN  rev.IsRight=1 THEN '1' WHEN rev.IsRight=0 THEN '0' ELSE '' END as RightStatus>");
                var dtb = que.LoadDataTable();
                Cache.Insert(cacheName, dtb, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 30, 0));
            }
            return (DataTable)Cache[cacheName];
        }


        protected void grdEntry_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;

                var right = dataItem["RightStatus"].Text;
                if (right == "&nbsp;")
                    right = string.Empty;

                var optRight = ((RadRadioButtonList)dataItem.FindControl("optRight"));
                optRight.Attributes.Add("RowIndex", dataItem.RowIndex.ToString());
                optRight.SelectedValue = right;

            }
        }
        protected string DisplayMenuRightStatusAll()
        {
            return DataModeCurrent == AppEnum.DataMode.Read ? "none" : "display";
        }

        protected void grdEntry_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var commandName = e.CommandName.ToLower();
            if (commandName == "unselect" || commandName == "yes" || commandName == "no")
            {
                var status = commandName == "yes" ? "1" : commandName == "no" ? "0" : string.Empty;
                var dtb = PrescriptionReviewDataTable();
                foreach (DataRow row in dtb.Rows)
                {
                    row["RightStatus"] = status;
                }
                grdEntry.Rebind();
            }
        }
        protected string RightStatusInfo(GridItem container)
        {
            if ( DataModeCurrent != AppEnum.DataMode.Read)
                return string.Empty;

            var rightStatus = DataBinder.Eval(container.DataItem, "RightStatus").ToString();

            switch (rightStatus)
            {
                case "":
                    return "Not Review";
                case "1":
                    return "Yes";
                case "0":
                    return "No";

            }
            return string.Empty;
        }
    }
}
