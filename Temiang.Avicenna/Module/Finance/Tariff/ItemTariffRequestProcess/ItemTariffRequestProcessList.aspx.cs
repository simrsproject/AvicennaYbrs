using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Finance.Tariff
{
    public partial class ItemTariffRequestProcessList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ItemTariffRequestProcessSearch.aspx";
            UrlPageDetail = "ItemTariffRequestProcessDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.ItemTariffRequestProcess;
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
            string id = dataItem.GetDataKeyValue(ItemTariffRequestProcessMetadata.ColumnNames.TariffRequestNo).ToString();
            string url = string.Format("ItemTariffRequestProcessDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        private DataTable ItemTariffRequestProcess
        {
            get
            {
                object obj = Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                ItemTariffRequestProcessQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (ItemTariffRequestProcessQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new ItemTariffRequestProcessQuery("a");
                    var tariffType = new AppStandardReferenceItemQuery("b");
                    var tariffType2 = new AppStandardReferenceItemQuery("c");
                    var itemType = new AppStandardReferenceItemQuery("d");
                    var igroup = new ItemGroupQuery("e");
                    query.InnerJoin(tariffType).On(query.FromSRTariffType == tariffType.ItemID &
                                                   tariffType.StandardReferenceID ==
                                                   AppEnum.StandardReference.TariffType.ToString());
                    query.InnerJoin(tariffType2).On(query.ToSRTariffType == tariffType2.ItemID &
                                                   tariffType2.StandardReferenceID ==
                                                   AppEnum.StandardReference.TariffType.ToString());
                    query.InnerJoin(itemType).On(query.SRItemType == itemType.ItemID &
                                                 itemType.StandardReferenceID ==
                                                 AppEnum.StandardReference.ItemType.ToString());
                    query.LeftJoin(igroup).On(igroup.ItemGroupID == query.ItemGroupID);

                    query.Select(query.SelectAllExcept(), itemType.ItemName.As("ItemTypeName"),
                                 tariffType.ItemName.As("FromTariffTypeName"), tariffType2.ItemName.As("ToTariffTypeName"), igroup.ItemGroupName);
                    query.Where(query.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen));
                    query.OrderBy(query.TariffRequestNo.Descending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
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
                grdList.DataSource = ItemTariffRequestProcess;
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
                    var query = new ItemTariffRequestProcessItemCompQuery("a");
                    var tcQuery = new TariffComponentQuery("b");
                    query.InnerJoin(tcQuery).On(query.TariffComponentID == tcQuery.TariffComponentID);
                    query.Select(query.SelectAllExcept(), tcQuery.TariffComponentName);
                    query.Where(query.TariffRequestNo == tariffRequestNo);
                    DataTable dtb = query.LoadDataTable();

                    //Apply
                    e.DetailTableView.DataSource = dtb;
                    break;
            }
        }
    }
}