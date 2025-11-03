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

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaunderedProcessDetailItemCentralizationPicklist : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeData();
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["LaundryItemCentralizations" + Request.UserHostName] != null)
                grdList.DataSource = ViewState["LaundryItemCentralizations" + Request.UserHostName];
        }

        protected void grdList_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSource();
        }

        private void UpdateDataSource()
        {
            DataTable dtb = (DataTable)ViewState["LaundryItemCentralizations" + Request.UserHostName];
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (row["ItemID"].Equals(dataItem.GetDataKeyValue("ItemID").ToString()))
                    {
                        row["QtyProcessed"] = ((RadNumericTextBox)dataItem.FindControl("txtQtyProcessed")).Value ?? 0;

                        break;
                    }
                }

                ViewState["LaundryItemCentralizations" + Request.UserHostName] = dtb;
            }
        }

        private void InitializeData()
        {
            var query = new LaundryItemBalanceQuery("a");
            var itemq = new ItemQuery("b");
            var itemproductq = new ItemProductNonMedicQuery("c");
            var unitq = new AppStandardReferenceItemQuery("d");

            query.InnerJoin(itemq).On(itemq.ItemID == query.ItemID);
            query.InnerJoin(itemproductq).On(itemproductq.ItemID == query.ItemID);
            query.InnerJoin(unitq).On(unitq.StandardReferenceID == "ItemUnit" && unitq.ItemID == itemproductq.SRItemUnit);

            query.Where(query.ServiceUnitID == AppSession.Parameter.ServiceUnitLaundryID, query.IsCleanLaundry == false);

            query.Select(
                query.ItemID,
                itemq.ItemName,
                query.Balance.As("Qty"),
                query.Balance.As("QtyProcessed"),
                unitq.ItemName.As("ItemUnit"));

            query.OrderBy(itemq.ItemName.Ascending);

            DataTable dtb = query.LoadDataTable();

            ViewState["LaundryItemCentralizations" + Request.UserHostName] = dtb;

            grdList.DataSource = dtb;
            grdList.DataBind();
        }

        private LaunderedProcessItemCentralization FindItem(string itemId)
        {
            var coll = (LaunderedProcessItemCentralizationCollection)Session["collLaunderedProcessItemCentralization" + Request.UserHostName];
            foreach (LaunderedProcessItemCentralization entity in coll)
            {
                if (entity.ItemID == itemId)
                    return entity;
            }
            return null;
        }

        public override bool OnButtonOkClicked()
        {
            var coll = (LaunderedProcessItemCentralizationCollection)Session["collLaunderedProcessItemCentralization" + Request.UserHostName];
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>().Where(g => ((CheckBox)g.FindControl("detailChkbox")).Checked))
            {
                decimal qty = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyProcessed")).Value);
                string itemName = dataItem["ItemName"].Text;

                if (qty <= 0) continue;

                LaunderedProcessItemCentralization entity = FindItem(dataItem["ItemID"].Text);
                if (entity == null)
                {
                    entity = coll.AddNew();
                }
                entity.ItemID = dataItem["ItemID"].Text;
                entity.ItemName = dataItem["ItemName"].Text;
                entity.Qty = qty;
                entity.ItemUnit = dataItem["ItemUnit"].Text;
            }

            ViewState["LaundryItemCentralizations" + Request.UserHostName] = null;
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind1'";
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }
    }
}