using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Tariff
{
    public partial class ItemProductTariffRequestList : BasePageList
    {
        private DataTable ItemTariffRequests
        {
            get
            {
                object obj = Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                ItemTariffRequestQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (ItemTariffRequestQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new ItemTariffRequestQuery("a");
                    var tariffType = new AppStandardReferenceItemQuery("b");
                    var itemType = new AppStandardReferenceItemQuery("d");
                    var classQuery = new ClassQuery("c");
                    query.InnerJoin(tariffType).On(
                        query.SRTariffType == tariffType.ItemID &
                        tariffType.StandardReferenceID == AppEnum.StandardReference.TariffType.ToString());
                    query.InnerJoin(itemType).On(
                        query.SRItemType == itemType.ItemID &
                        itemType.StandardReferenceID == AppEnum.StandardReference.ItemType.ToString());

                    query.InnerJoin(classQuery).On(query.ClassID == classQuery.ClassID);
                    query.Select(query.SelectAllExcept(), itemType.ItemName.As("ItemTypeName"),
                                 tariffType.ItemName.As("TariffTypeName"), classQuery.ClassName);
                    query.Where
                        (
                            query.Or
                            (
                                query.SRItemType == BusinessObject.Reference.ItemType.Medical.ToString(),
                                query.SRItemType == BusinessObject.Reference.ItemType.NonMedical.ToString()
                            )
                        );
                    query.OrderBy(query.TariffRequestDate.Descending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ItemProductTariffRequestSearch.aspx";
            UrlPageDetail = "ItemProductTariffRequestDetail.aspx";

            ProgramID = AppConstant.Program.ITEM_PRODUCT_TARIFF_REQUEST;
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
            string id = dataItem.GetDataKeyValue(ItemTariffRequestMetadata.ColumnNames.TariffRequestNo).ToString();
            string url = string.Format("ItemProductTariffRequestDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            if (!e.IsFromDetailTable)
            {
                grdList.DataSource = ItemTariffRequests;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string tariffRequestNo = dataItem.GetDataKeyValue("TariffRequestNo").ToString();

            switch (e.DetailTableView.Name)
            {
                case "grdItem":
                    //Load record
                    ItemTariffRequestItemQuery query = new ItemTariffRequestItemQuery("a");
                    ItemQuery itemQuery = new ItemQuery("b");
                    query.InnerJoin(itemQuery).On(query.ItemID == itemQuery.ItemID);
                    query.Select(query.SelectAllExcept(), itemQuery.ItemName);
                    query.Where(query.TariffRequestNo == tariffRequestNo);
                    DataTable dtb = query.LoadDataTable();

                    //Apply
                    e.DetailTableView.DataSource = dtb;
                    break;
                case "grdItemTariffRequestItemComp":
                    string itemID = dataItem.GetDataKeyValue("ItemID").ToString();
                    //Load record
                    ItemTariffRequestItemCompQuery itemCompQuery = new ItemTariffRequestItemCompQuery("a");
                    TariffComponentQuery compQuery = new TariffComponentQuery("b");
                    itemCompQuery.InnerJoin(compQuery).On(itemCompQuery.TariffComponentID == compQuery.TariffComponentID);
                    itemCompQuery.Select(itemCompQuery.SelectAllExcept(), compQuery.TariffComponentName);
                    itemCompQuery.Where(itemCompQuery.TariffRequestNo == tariffRequestNo, itemCompQuery.ItemID == itemID);
                    DataTable dtb2 = itemCompQuery.LoadDataTable();

                    //Apply
                    e.DetailTableView.DataSource = dtb2;
                    break;
            }
        }
    }
}