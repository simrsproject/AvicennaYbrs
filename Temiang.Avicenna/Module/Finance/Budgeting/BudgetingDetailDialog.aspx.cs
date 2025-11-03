using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Budgeting
{
    public partial class BudgetingDetailDialog : BasePageDialog
    {
        private string Mode
        {
            get
            {
                return Request.QueryString["md"];
            }
        }
        private string BudgetingNo
        {
            get
            {
                return Request.QueryString["bno"];
            }
        }
        private int Revision
        {
            get
            {
                return System.Convert.ToInt32(Request.QueryString["rev"]);
            }
        }
        private int ChartOfAccountID {
            get {
                return System.Convert.ToInt32(Request.QueryString["coaid"]);
            }
        }

        private BudgetingDetailItemCollection BudgetingDetailItems
        {
            get
            {
                return Session["BudgetingDetailItems"] as BudgetingDetailItemCollection;
            }
            set
            {
                Session["BudgetingDetailItems"] = value;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdList.MasterTableView.CommandItemDisplay = Mode.ToLower() == "edit" ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
                grdList.Columns[0].Visible = Mode.ToLower() == "edit";
                grdList.Columns[grdList.Columns.Count - 1].Visible = Mode.ToLower() == "edit";
                grdList.Rebind();
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var ds = BudgetingDetailItems.Where(x => ((x.BudgetingNo == BudgetingNo && x.Revision == Revision ) || x.es.IsAdded) && 
                x.ChartOfAccountID == ChartOfAccountID);
            grdList.DataSource = ds;
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem) {
                BudgetingDetailItem bdt = e.Item.DataItem as BudgetingDetailItem;
                if (bdt.IsAsset ?? false)
                {
                    if (AppSession.Parameter.BudgetOfAssetNeedExtraApprovalLimit > 0)
                    {
                        if ((bdt.Price ?? 0) * (bdt.Qty ?? 0) > AppSession.Parameter.BudgetOfAssetNeedExtraApprovalLimit)
                        {
                            if (bdt.IsAssetApproved ?? false)
                            {
                                e.Item.ForeColor = System.Drawing.Color.DarkGreen;
                                e.Item.ToolTip = "Approved";
                            }
                            else if (bdt.IsAssetRejected ?? false) {
                                e.Item.ForeColor = System.Drawing.Color.DarkRed;
                                e.Item.ToolTip = "Rejected";
                            }
                            else
                            {
                                e.Item.ForeColor = System.Drawing.Color.DarkBlue;
                                e.Item.ToolTip = "Need extra approval";
                            }
                        }
                    }
                }
            }
        }

        private void SetEntityValue(BudgetingDetailItem entity, GridCommandEventArgs e)
        {
            var userControl = (BudgetingDetailDialogEditor)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;

                entity.QtyMonth01 = userControl.QtyMonth01;
                entity.QtyMonth02 = userControl.QtyMonth02;
                entity.QtyMonth03 = userControl.QtyMonth03;
                entity.QtyMonth04 = userControl.QtyMonth04;
                entity.QtyMonth05 = userControl.QtyMonth05;
                entity.QtyMonth06 = userControl.QtyMonth06;
                entity.QtyMonth07 = userControl.QtyMonth07;
                entity.QtyMonth08 = userControl.QtyMonth08;
                entity.QtyMonth09 = userControl.QtyMonth09;
                entity.QtyMonth10 = userControl.QtyMonth10;
                entity.QtyMonth11 = userControl.QtyMonth11;
                entity.QtyMonth12 = userControl.QtyMonth12;

                entity.Qty = userControl.Qty;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ConversionFactor = userControl.ConversionFactor;

                entity.Price = userControl.Price;

                entity.ChartOfAccountID = ChartOfAccountID;
                entity.Revision = Revision;
                entity.BudgetingNo = BudgetingNo;

                entity.IsAsset = userControl.IsAsset;
                entity.IsAssetApproved = false;
                entity.IsAssetRejected = false;
            }
        }
        protected void grdList_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var entity = BudgetingDetailItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdList.Rebind();
        }
        protected void grdList_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][BudgetingDetailItemMetadata.ColumnNames.ItemID]);
            var entity = FindItemGrid(itemID);
            if (entity != null)
                SetEntityValue(entity, e);
        }
        protected void grdList_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][BudgetingDetailItemMetadata.ColumnNames.ItemID]);
            var entity = FindItemGrid(itemID);
            if (entity != null)
            {
                entity.MarkAsDeleted();
            }
        }
        private BudgetingDetailItem FindItemGrid(string ItemID) {
            return BudgetingDetailItems.Where(x => x.BudgetingNo == BudgetingNo && x.Revision == Revision &&
            x.ChartOfAccountID == ChartOfAccountID && x.ItemID == ItemID).FirstOrDefault();
        }
    }
}
