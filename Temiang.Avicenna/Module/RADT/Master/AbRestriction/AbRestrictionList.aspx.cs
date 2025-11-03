using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Master
{
    public partial class AbRestrictionList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "AbRestrictionSearch.aspx";
            UrlPageDetail = "AbRestrictionDetail.aspx";

            ProgramID = AppConstant.Program.AbRestriction;

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
            string id = dataItem.GetDataKeyValue(AbRestrictionMetadata.ColumnNames.AbRestrictionID).ToString();
            Page.Response.Redirect("AbRestrictionDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = AbRestrictions;

            
        }

        private DataTable AbRestrictions
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                AbRestrictionQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (AbRestrictionQuery)Session[SessionNameForQuery];
                else
                {
                    query = new AbRestrictionQuery("a");
                    var parent = new AbRestrictionQuery("p");
                    query.LeftJoin(parent).On(query.ParentID == parent.AbRestrictionID);

                    var stdi = new AppStandardReferenceItemQuery("stdi");
                    query.LeftJoin(stdi).On(query.SRAbRestrictionType == stdi.ItemID & stdi.StandardReferenceID=="AbRestrictionType");
                    query.Select(query.AbRestrictionID, query.ParentID, query.AbRestrictionName, parent.AbRestrictionName.As("ParentName"), stdi.ItemName.As("AbRestrictionTypeName"));
                    query.OrderBy(query.AbRestrictionID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}

