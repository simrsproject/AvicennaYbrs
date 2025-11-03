using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class DietList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "DietSearch.aspx";
            UrlPageDetail = "DietDetail.aspx";

            ProgramID = AppConstant.Program.Diet;

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
            string id = dataItem.GetDataKeyValue(DietMetadata.ColumnNames.DietID).ToString();
            Page.Response.Redirect("DietDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Diets;
        }

        private DataTable Diets
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                DietQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (DietQuery)Session[SessionNameForQuery];
                else
                {
                    query = new DietQuery("a");
                    var dc = new AppStandardReferenceItemQuery("b");
                    query.InnerJoin(dc).On(query.SRDietType == dc.ItemID &&
                                           dc.StandardReferenceID == AppEnum.StandardReference.DietType.ToString());
                    query.Select
                        (
                            query.DietID,
                            query.DietName,
                            dc.ItemName.As("DietCategory"),
                            query.IsActive
                        );
                    query.OrderBy(query.DietID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "DietName", "DietID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
