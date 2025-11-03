using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Master
{
    public partial class CompanyLaborProfileList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "CompanyLaborProfileSearch.aspx";
            UrlPageDetail = "CompanyLaborProfileDetail.aspx";
			
			ProgramID = AppConstant.Program.Profile ; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(CompanyLaborProfileMetadata.ColumnNames.CompanyLaborProfileID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = CompanyLaborProfiles;
        }

        private DataTable CompanyLaborProfiles
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				CompanyLaborProfileQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (CompanyLaborProfileQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new CompanyLaborProfileQuery();
                }
				query.es.Top = AppSession.Parameter.MaxResultRecord;
				query.Select(
                				query.CompanyLaborProfileID,
                				query.CompanyLaborProfileCode,
                				query.CompanyLaborProfileName,
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

