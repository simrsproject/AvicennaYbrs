using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.ControlPanel.Setting
{
    public partial class AppAutoNumberList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "AutoNumberSearch.aspx";
            UrlPageDetail = "AutoNumberDetail.aspx";

            ProgramID = AppConstant.Program.AutoNumbering;

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
            string id = dataItem.GetDataKeyValue(AppAutoNumberMetadata.ColumnNames.SRAutoNumber).ToString();
            string efDate = dataItem.GetDataKeyValue(AppAutoNumberMetadata.ColumnNames.EffectiveDate).ToString();

            string url = string.Format("AutoNumberDetail.aspx?md={0}&id={1}&id2={2}", mode, id, efDate);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = AppAutoNumbers;
        }

        private DataTable AppAutoNumbers
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                AppAutoNumberQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (AppAutoNumberQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new AppAutoNumberQuery();
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

