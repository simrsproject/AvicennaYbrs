using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class SalaryComponentRoundingList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "SalaryComponentRoundingSearch.aspx";
            UrlPageDetail = "SalaryComponentRoundingDetail.aspx";

            ProgramID = AppConstant.Program.SalaryComponentRounding; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(SalaryComponentRoundingMetadata.ColumnNames.SalaryComponentRoundingID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = SalaryComponentRoundings;
        }

        private DataTable SalaryComponentRoundings
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				SalaryComponentRoundingQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (SalaryComponentRoundingQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new SalaryComponentRoundingQuery();
                }
				query.es.Top = AppSession.Parameter.MaxResultRecord;
				query.Select(
                				query.SalaryComponentRoundingID,
                				query.SalaryComponentRoundingName,
                				query.NominalValue,
                				query.NearestValue,
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

