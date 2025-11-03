using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Cssd.Master
{
    public partial class ItemList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.CssdItem;

            UrlPageSearch = "ItemSearch.aspx";
            UrlPageDetail = "ItemDetail.aspx";

            this.WindowSearch.Height = 300;
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
            string id = dataItem.GetDataKeyValue(ItemMetadata.ColumnNames.ItemID).ToString();
            Page.Response.Redirect("ItemDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ItemNeedToBeSterilizeds;
        }

        private DataTable ItemNeedToBeSterilizeds
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ItemQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ItemQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ItemQuery("a");
                    var qs = new VwItemProductMedicNonMedicQuery("b");
                    var qgroup = new ItemGroupQuery("c");
                    var qstd = new AppStandardReferenceItemQuery("d");
                    
                    query.InnerJoin(qs).On(query.ItemID == qs.ItemID);
                    query.LeftJoin(qgroup).On(query.ItemGroupID == qgroup.ItemGroupID);
                    query.LeftJoin(qstd).On(qstd.StandardReferenceID == AppEnum.StandardReference.CssdItemGroup &&
                                            query.SRCssdItemGroup == qstd.ItemID);
                    query.Where(query.IsNeedToBeSterilized == true);
                    
                    query.Select
                        (
                            query.ItemID,
                            qgroup.ItemGroupName,
                            query.ItemName,
                            qs.SRItemUnit,
                            qstd.ItemName.As("CssdItemGroup"),
                            query.CssdPackagingCostAmount.Coalesce("0"),
                            query.IsItemProduction,
                            query.IsActive
                        );
                    query.OrderBy(query.ItemID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "ItemName", "ItemID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
