using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.ControlPanel.PrinterManagement
{
    public partial class UserHostPrinterList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "UserHostPrinterSearch.aspx";
            UrlPageDetail = "UserHostPrinterDetail.aspx";

            ProgramID = AppConstant.Program.UserHostPrinter;

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
            string id = dataItem.GetDataKeyValue(UserHostPrinterMetadata.ColumnNames.UserHostName).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = UserHostPrinters;
        }

        private DataTable UserHostPrinters
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				UserHostPrinterQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (UserHostPrinterQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new UserHostPrinterQuery("a");
                    PrinterQuery qb = new PrinterQuery("b");
                    query.LeftJoin(qb).On(query.PrinterID == qb.PrinterID);
                    query.es.Top = AppSession.Parameter.MaxResultRecord;
				    query.Select(
                				query.UserHostName,
                				query.Description,
                				query.PrinterID,
                                qb.PrinterName
							);

                    //Quick Search
                    ApplyQuickSearch(query, "UserHostName", "Description");
                }

				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}