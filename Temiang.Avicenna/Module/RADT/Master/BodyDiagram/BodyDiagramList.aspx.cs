using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class BodyDiagramList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageSearch = "BodyDiagramSearch.aspx";
            UrlPageDetail = "BodyDiagramDetail.aspx";

            ProgramID = AppConstant.Program.BodyDiagram;

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
            string id = dataItem.GetDataKeyValue(BodyDiagramMetadata.ColumnNames.BodyID).ToString();
            Page.Response.Redirect("BodyDiagramDetail.aspx?md=" + mode + "&id=" + id, true);
        }
        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = BodyDiagrams;
        }

        private DataTable BodyDiagrams
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                BodyDiagramQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (BodyDiagramQuery)Session[SessionNameForQuery];
                else
                {
                    query = new BodyDiagramQuery();
                    query.OrderBy(query.BodyID.Ascending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

