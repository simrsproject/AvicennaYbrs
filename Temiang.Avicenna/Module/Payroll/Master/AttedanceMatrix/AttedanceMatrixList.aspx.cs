using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class AttedanceMatrixList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "AttedanceMatrixSearch.aspx";
            UrlPageDetail = "AttedanceMatrixDetail.aspx";

            ProgramID = AppConstant.Program.AttedanceMatrix; //TODO: Isi ProgramID

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
            string id = dataItem.GetDataKeyValue(AttedanceMatrixMetadata.ColumnNames.AttedanceMatrixID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = AttedanceMatrixs;
        }

        private DataTable AttedanceMatrixs
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				AttedanceMatrixQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (AttedanceMatrixQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new AttedanceMatrixQuery();

                    //Quick Search
                    ApplyQuickSearch(query);
                }
				query.es.Top = AppSession.Parameter.MaxResultRecord;
				query.Select(
                				query.AttedanceMatrixID,
                				query.AttedanceMatrixName,
                				query.AttedanceMatrixFieldt,
                				query.LastUpdateDateTime,
                				query.LastUpdateByUserID
							);
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

