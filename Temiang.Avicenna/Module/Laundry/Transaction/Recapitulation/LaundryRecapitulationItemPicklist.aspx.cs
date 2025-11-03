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
    public partial class LaundryRecapitulationItemPicklist : BasePageDialog
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
            if (ViewState["LaundryItems" + Request.UserHostName] != null)
                grdList.DataSource = ViewState["LaundryItems" + Request.UserHostName];
        }

        protected void grdList_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSource();
        }

        private void UpdateDataSource()
        {
            DataTable dtb = (DataTable)ViewState["LaundryItems" + Request.UserHostName];
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (row["ItemID"].Equals(dataItem.GetDataKeyValue("ItemID").ToString()))
                    {
                        row["Qty"] = ((RadNumericTextBox)dataItem.FindControl("txtQty")).Value ?? 0;
                        row["QtyRewashing"] = ((RadNumericTextBox)dataItem.FindControl("txtQtyRewashing")).Value ?? 0;

                        break;
                    }
                }

                ViewState["LaundryItems" + Request.UserHostName] = dtb;
            }
        }

        private void InitializeData()
        {
            var query = new ItemQuery("a");
            var itemproduct = new ItemProductNonMedicQuery("b");
            var unit = new AppStandardReferenceItemQuery("c");

            query.InnerJoin(itemproduct).On(itemproduct.ItemID == query.ItemID);
            query.InnerJoin(unit).On(unit.StandardReferenceID == "ItemUnit" && unit.ItemID == itemproduct.SRItemUnit);

            query.Where(query.IsActive == true, itemproduct.IsNeedToBeLaundered == true);

            query.Select(
                query.ItemID,
                query.ItemName,
                @"<0 AS Qty>",
                @"<0 AS QtyRewashing>",
                itemproduct.SRItemUnit,
                unit.ItemName.As("ItemUnit"));

            query.OrderBy(query.ItemName.Ascending);

            DataTable dtb = query.LoadDataTable();

            //var dtb = (new LaundryRecapitulationProcessItemCollection()).GetLaundryRecapitulationItem(Convert.ToDateTime(Request.QueryString["dt"]));

            ViewState["LaundryItems" + Request.UserHostName] = dtb;

            grdList.DataSource = dtb;
            grdList.DataBind();
        }

        private LaundryRecapitulationProcessItem FindItem(string itemId)
        {
            var coll = (LaundryRecapitulationProcessItemCollection)Session["collLaundryRecapitulationProcessItem" + Request.UserHostName];
            foreach (LaundryRecapitulationProcessItem entity in coll)
            {
                if (entity.ItemID == itemId)
                    return entity;
            }
            return null;
        }

        public override bool OnButtonOkClicked()
        {
            var coll = (LaundryRecapitulationProcessItemCollection)Session["collLaundryRecapitulationProcessItem" + Request.UserHostName];
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>().Where(g => ((CheckBox)g.FindControl("detailChkbox")).Checked))
            {
                decimal qty = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQty")).Value);
                decimal qtyRewashing = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyRewashing")).Value);
                string itemName = dataItem["ItemName"].Text;

                if (qty <= 0 && qtyRewashing <= 0) continue;

                LaundryRecapitulationProcessItem entity = FindItem(dataItem["ItemID"].Text);
                if (entity == null)
                {
                    entity = coll.AddNew();
                }
                entity.TransactionNo = Request.QueryString["tno"].ToString();
                entity.ItemID = dataItem["ItemID"].Text;
                entity.ItemName = dataItem["ItemName"].Text;
                entity.Qty = qty;
                entity.QtyRewashing = qtyRewashing;
                entity.SRItemUnit = dataItem["SRItemUnit"].Text;
                entity.ItemUnit = dataItem["ItemUnit"].Text;
            }

            ViewState["LaundryItems" + Request.UserHostName] = null;
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind'";
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