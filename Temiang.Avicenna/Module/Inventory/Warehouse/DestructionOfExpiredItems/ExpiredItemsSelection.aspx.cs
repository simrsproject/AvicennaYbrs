using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;
using System.Collections;
using System.Linq;

namespace Temiang.Avicenna.Module.Inventory.Warehouse.DestructionOfExpiredItems
{
    public partial class ExpiredItemsSelection : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                InitializeData();
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["DetailExpiredItem" + Request.UserHostName] != null)
                grdDetail.DataSource = ViewState["DetailExpiredItem" + Request.UserHostName];
        }

        protected void grdDetail_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSource();
        }

        private void UpdateDataSource()
        {
            DataTable dtb = (DataTable)ViewState["DetailExpiredItem" + Request.UserHostName];
            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (row["ItemID"].Equals(dataItem.GetDataKeyValue("ItemID").ToString()))
                    {
                        row["QtyProceed"] = ((RadNumericTextBox)dataItem.FindControl("QtyProceed")).Value ?? 0;
                        break;
                    }
                }

                ViewState["DetailExpiredItem" + Request.UserHostName] = dtb;
            }
        }

        private void InitializeData()
        {
            string tloc = Request.QueryString["fu"];

            var query = new ItemBalanceQuery("a");

            if (Request.QueryString["it"] == BusinessObject.Reference.ItemType.Medical)
            {
                var qrItemMed = new ItemProductMedicQuery("b");
                query.Select
                    (
                        qrItemMed.SRItemUnit
                    );
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID);
            }
            else if (Request.QueryString["it"] == BusinessObject.Reference.ItemType.NonMedical)
            {
                var qrItemMed = new ItemProductNonMedicQuery("b");
                query.Select
                    (
                        qrItemMed.SRItemUnit
                    );
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID);
            }
            else
            {
                var qrItemMed = new ItemKitchenQuery("b");
                query.Select
                    (
                        qrItemMed.SRItemUnit
                    );
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID);
            }

            var qrItem = new ItemQuery("d");
            query.InnerJoin(qrItem).On(query.ItemID == qrItem.ItemID);

            query.Where
                (
                    query.LocationID == tloc,
                    query.Balance > 0,
                    qrItem.IsActive == true
                );

            query.Select
                (
                    query.ItemID,
                    qrItem.ItemName.As("ItemName"),
                    query.Balance,
                    @"<b.SRItemUnit AS Unit>",
                    query.Balance.As("QtyProceed")
                );

            query.OrderBy(qrItem.ItemName.Ascending);
            DataTable dtb = query.LoadDataTable();

            ViewState["DetailExpiredItem" + Request.UserHostName] = dtb;

            grdDetail.DataSource = dtb;
            grdDetail.DataBind();
        }

        private ItemTransactionItem FindItemTransactionItem(string itemID)
        {
            var coll = (ItemTransactionItemCollection)Session["collDestructionOfExpiredItems" + Request.UserHostName];
            foreach (ItemTransactionItem entity in coll)
            {
                if (entity.ItemID == itemID)
                    return entity;
            }
            return null;
        }

        public override bool OnButtonOkClicked()
        {
            var coll = (ItemTransactionItemCollection)Session["collDestructionOfExpiredItems" + Request.UserHostName];
            string seqNo = coll.HasData ? coll[coll.Count - 1].SequenceNo : "000";

            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items.Cast<GridDataItem>().Where(g => ((CheckBox)g.FindControl("detailChkbox")).Checked))
            {
                decimal qty = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyProceed")).Value);
                if (qty <= 0) continue;

                ItemTransactionItem entity = FindItemTransactionItem(dataItem["ItemID"].Text);
                if (entity == null)
                {
                    entity = coll.AddNew();

                    entity.SequenceNo = string.Format("{0:000}", int.Parse(seqNo) + 1);
                    seqNo = entity.SequenceNo;
                }
                entity.ItemID = dataItem["ItemID"].Text;
                entity.ItemName = dataItem["ItemName"].Text;
                entity.ReferenceNo = string.Empty;
                entity.ReferenceSequenceNo = string.Empty;
                entity.Quantity = qty;
                entity.SRItemUnit = dataItem["SRItemUnit"].Text;
                entity.ConversionFactor = 1;
                entity.QuantityFinishInBaseUnit = 0;
                entity.PageNo = 0;

                if (Request.QueryString["it"] == BusinessObject.Reference.ItemType.Medical)
                {
                    var med = new ItemProductMedic();
                    med.LoadByPrimaryKey(entity.ItemID);
                    entity.CostPrice = med.CostPrice;
                    entity.Price = med.PriceInBasedUnitWVat;
                }
                else if (Request.QueryString["it"] == BusinessObject.Reference.ItemType.NonMedical)
                {
                    var nonMed = new ItemProductNonMedic();
                    nonMed.LoadByPrimaryKey(entity.ItemID);
                    entity.CostPrice = nonMed.CostPrice;
                    entity.Price = nonMed.PriceInBasedUnitWVat;
                }
                else
                {
                    var kitchen = new ItemKitchen();
                    kitchen.LoadByPrimaryKey(entity.ItemID);
                    entity.CostPrice = kitchen.CostPrice;
                    entity.Price = kitchen.PriceInBasedUnitWVat;
                }

                entity.Discount1Percentage = 0;
                entity.Discount2Percentage = 0;
                entity.BatchNumber = string.Empty;
                entity.str.ExpiredDate = string.Empty;
                entity.IsPackage = false;
                entity.IsBonusItem = false;
                entity.IsClosed = false;
                entity.Description = dataItem["ItemName"].Text;
                
            }

            ViewState["DetailExpiredItem" + Request.UserHostName] = null;
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind'";
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }
    }
}
