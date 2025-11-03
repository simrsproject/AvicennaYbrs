using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;


namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class ProductionOfGoodsList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ProductionOfGoodsSearch.aspx";
            UrlPageDetail = "ProductionOfGoodsDetail.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.ProductionOfGoods;
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
            string id = dataItem.GetDataKeyValue(ProductionOfGoodsMetadata.ColumnNames.ProductionNo).ToString();
            string url = string.Format("ProductionOfGoodsDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = ProductionOfGoodss;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string prodNo = dataItem.GetDataKeyValue("ProductionNo").ToString();

            //Load record
            var query = new ProductionOfGoodsItemQuery("a");
            var itemQ = new ItemQuery("b");
            query.InnerJoin(itemQ).On(query.ItemID == itemQ.ItemID);
            query.Where(query.ProductionNo == prodNo);
            query.OrderBy(query.ItemID.Ascending);

            query.Select
                (
                    query.ItemID,
                    query.SRItemUnit,
                    query.Qty,
                    itemQ.ItemName.As("ItemName")
                );
            
            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        private DataTable ProductionOfGoodss
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ProductionOfGoodsQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ProductionOfGoodsQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ProductionOfGoodsQuery("a");
                    var suQ = new ServiceUnitQuery("b");
                    var pfQ = new ProductionFormulaQuery("c");
                    var itQ = new ItemQuery("d");
                    var usrQ = new AppUserServiceUnitQuery("e");

                    query.InnerJoin(suQ).On(query.ServiceUnitID == suQ.ServiceUnitID);
                    query.InnerJoin(pfQ).On(query.FormulaID == pfQ.FormulaID);
                    query.InnerJoin(itQ).On(pfQ.ItemID == itQ.ItemID);
                    query.InnerJoin(usrQ).On(query.ServiceUnitID == usrQ.ServiceUnitID &&
                                             usrQ.UserID == AppSession.UserLogin.UserID);
                    query.OrderBy(query.ProductionNo.Descending);

                    query.Select
                        (
                           query.ProductionNo,
                           query.ProductionDate,
                           suQ.ServiceUnitName,
                           query.IsApproved,
                           query.IsVoid,
                           query.Notes,
                           pfQ.FormulaName,
                           itQ.ItemName,
                           (query.Qty * pfQ.Qty).As("Qty")
                       );
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();

                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
