using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemRadiologyList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageSearch = "ItemRadiologySearch.aspx";
            UrlPageDetail = "ItemRadiologyDetail.aspx";

            ProgramID = AppConstant.Program.RadiologyItem;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
        }

        public override void OnMenuEditClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(ItemMetadata.ColumnNames.ItemID).ToString();
            Page.Response.Redirect("ItemRadiologyDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ItemRadiologys;
        }

        private DataTable ItemRadiologys
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
                    ItemRadiologyQuery diagnosticQuery = new ItemRadiologyQuery("b");
                    ItemGroupQuery grp = new ItemGroupQuery("c");
                    query = new ItemQuery("a");
                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Where(query.SRItemType == BusinessObject.Reference.ItemType.Radiology);
                    query.LeftJoin(diagnosticQuery).On(query.ItemID == diagnosticQuery.ItemID);
                    query.LeftJoin(grp).On(query.ItemGroupID == grp.ItemGroupID);
                    query.Select(
                                query.ItemID,
                                query.ItemGroupID,
                                grp.ItemGroupName,
                                query.ItemName,
                                query.IsActive,
                				diagnosticQuery.ReportRLID,
                				diagnosticQuery.IsAdminCalculation,
                				diagnosticQuery.IsAllowVariable,
                				diagnosticQuery.IsAllowCito,
                				diagnosticQuery.IsAllowDiscount,
                				diagnosticQuery.IsPrintWithDoctorName,
                				diagnosticQuery.IsAssetUtilization,
                                query.Notes
							);
                    query.OrderBy(query.ItemGroupID.Ascending, query.ItemID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "ItemName", "ItemID");
                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}