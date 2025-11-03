using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ProductionFormulaList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "ProductionFormulaSearch.aspx";
            UrlPageDetail = "ProductionFormulaDetail.aspx";

            ProgramID = AppConstant.Program.ProductionFormula;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(ProductionFormulaMetadata.ColumnNames.FormulaID).ToString();
            Page.Response.Redirect("ProductionFormulaDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ProductionFormulas;
        }

        private DataTable ProductionFormulas
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ProductionFormulaQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ProductionFormulaQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ProductionFormulaQuery("a");
                    var iq = new ItemQuery("b");
                    var viq = new VwItemProductMedicNonMedicQuery("c");
                    query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                    query.InnerJoin(viq).On(query.ItemID == viq.ItemID);
                    query.Select(
                        query.FormulaID, 
                        query.FormulaName, 
                        @"<b.ItemName + ' [' + a.ItemID + ']' AS ItemName>",
                        query.Qty,
                        viq.SRItemUnit,
                        query.Notes, 
                        query.IsActive);
                    query.OrderBy(query.FormulaID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "FormulaName", "FormulaID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;

            //Load record
            var query = new ProductionFormulaItemQuery("a");
            var iq = new ItemQuery("b");
            query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
            query.Select(
                query.FormulaID,
                query.ItemID,
                iq.ItemName,
                query.Qty,
                query.SRItemUnit,
                query.IsConsumables
            );
            query.Where(query.FormulaID == dataItem.GetDataKeyValue("FormulaID").ToString());
            query.OrderBy(query.ItemID.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }
    }
}
