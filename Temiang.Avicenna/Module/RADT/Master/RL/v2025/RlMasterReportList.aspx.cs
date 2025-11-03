using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master.v2025
{
    public partial class RlMasterReportList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "RlMasterReportSearch.aspx";
            UrlPageDetail = "RlMasterReportDetail.aspx";

            ProgramID = AppConstant.Program.RlMasterReportV2; //TODO: Isi ProgramID

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
            //TODO: Betulkan parameter nya
            string id = dataItem.GetDataKeyValue(RlMasterReportV2025Metadata.ColumnNames.RlMasterReportID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = RlMasterReports;
        }

        private DataTable RlMasterReports
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                RlMasterReportV2025Query query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (RlMasterReportV2025Query)Session[SessionNameForQuery];
                }
                else
                {
                    query = new RlMasterReportV2025Query();

                    query.Select(
                                query.RlMasterReportID,
                                query.RlMasterReportNo,
                                query.RlMasterReportName,
                                query.IsActive,
                                query.LastUpdateDateTime,
                                query.LastUpdateByUserID
                            );

                    //Quick Search
                    ApplyQuickSearch(query, "RlMasterReportName", "RlMasterReportNo");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

