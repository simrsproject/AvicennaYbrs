using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class TherapyList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "TherapySearch.aspx";
            UrlPageDetail = "TherapyDetail.aspx";

            ProgramID = AppConstant.Program.Therapy;

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
            string id = dataItem.GetDataKeyValue(TherapyMetadata.ColumnNames.TherapyID).ToString();
            Page.Response.Redirect("TherapyDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Therapys;
        }

        private DataTable Therapys
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                TherapyQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (TherapyQuery)Session[SessionNameForQuery];
                else
                {
                    query = new TherapyQuery("a");
                    AppStandardReferenceItemQuery srQuery = new AppStandardReferenceItemQuery("b");
                    query.Select
                    (
                        query,
                        srQuery.ItemName.As("TherapyGroupName")
                    );
                    query.LeftJoin(srQuery).On(query.SRTherapyGroup == srQuery.ItemID & srQuery.StandardReferenceID == AppEnum.StandardReference.TherapyGroup.ToString());
                    query.OrderBy(query.SRTherapyGroup.Ascending, query.TherapyName.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query);
                }
                
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
