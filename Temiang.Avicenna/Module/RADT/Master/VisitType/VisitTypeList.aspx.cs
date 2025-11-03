using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class VisitTypeList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "VisitTypeSearch.aspx";
            UrlPageDetail = "VisitTypeDetail.aspx";

            ProgramID = AppConstant.Program.VisitType;

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
            string id = dataItem.GetDataKeyValue(VisitTypeMetadata.ColumnNames.VisitTypeID).ToString();
            Page.Response.Redirect("VisitTypeDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = VisitTypes;
        }

        private DataTable VisitTypes
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                VisitTypeQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (VisitTypeQuery)Session[SessionNameForQuery];
                else
                {
                    query = new VisitTypeQuery();
                    query.OrderBy(query.VisitTypeID.Ascending);
                    //Quick Search
                    ApplyQuickSearch(query, "VisitTypeName", "VisitTypeID");
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}

