using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ZipCodeList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ZipCodeSearch.aspx";
            UrlPageDetail = "ZipCodeDetail.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.ZipCode;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
        }	
        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
			RedirectToPageDetail(dataItems[0],"edit");
        }
        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
			RedirectToPageDetail(dataItems[0],"view");
        }		
        private void RedirectToPageDetail(GridDataItem dataItem,string mode)
        {
			//TODO: Betulkan parameter nya
            string id = dataItem.GetDataKeyValue(ZipCodeMetadata.ColumnNames.ZipCode).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
			Page.Response.Redirect(url, true);
        }	
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ZipCodes;
        }

        private DataTable ZipCodes
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				ZipCodeQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (ZipCodeQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new ZipCodeQuery();
                    query.OrderBy(query.ZipCode.Ascending);

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


