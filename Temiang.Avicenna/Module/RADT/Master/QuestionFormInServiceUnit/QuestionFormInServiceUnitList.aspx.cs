using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class QuestionFormInServiceUnitList : BasePageList
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //ToolBarMenuQuickSearch.Enabled = true;
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageDetail = "QuestionFormInServiceUnitDetail2.aspx";
            UrlPageSearch = "QuestionFormInServiceUnitSearch.aspx";
            ProgramID = AppConstant.Program.QuestionFormInServiceUnit;
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
            string id = dataItem.GetDataKeyValue(ServiceUnitMetadata.ColumnNames.ServiceUnitID).ToString();
            Page.Response.Redirect(string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id), true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ServiceUnitDataTable;
        }
        private DataTable ServiceUnitDataTable
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ServiceUnitQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (ServiceUnitQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new ServiceUnitQuery("a");
                    query.Select
                        (
                            query.ServiceUnitID,
                            query.ServiceUnitName,
                            query.IsActive
                        );
                    query.Where(query.SRRegistrationType != string.Empty, query.IsActive == true);

                    query.es.Top = AppSession.Parameter.MaxResultRecord;

                    //var quickSearch = ToolBarMenuQuickSearch.Text;
                    //if (quickSearch.Trim() != string.Empty)
                    //{
                    //    var searchs = quickSearch.Trim().Split(' ');
                    //    foreach (var search in searchs)
                    //    {
                    //        var searchLike = string.Format("%{0}%", search);
                    //        query.Where(query.ServiceUnitName.Like(searchLike) || query.ServiceUnitID.Like(searchLike));
                    //    }
                    //}

                    //Quick Search
                    ApplyQuickSearch(query);

                }
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
            set { this.Session[SessionNameForList] = value; }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ServiceUnitDataTable = null;
            grdList.Rebind();
        }
    }
}
