using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Remuneration
{
    public partial class IncentiveProcessList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "IncentiveProcessSearch.aspx";
            UrlPageDetail = "IncentiveProcessDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.EmployeeIncentiveProcess;
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
            string id = dataItem.GetDataKeyValue(EmployeeIncentiveProcessMetadata.ColumnNames.EmployeeIncentiveProcessID).ToString();
            string url = string.Format("IncentiveProcessDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeeIncentiveProcesss;
        }

        private DataTable EmployeeIncentiveProcesss
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                EmployeeIncentiveProcessQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmployeeIncentiveProcessQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new EmployeeIncentiveProcessQuery("a");
                    var pp = new PayrollPeriodQuery("b");
                    query.InnerJoin(pp).On(pp.PayrollPeriodID == query.PayrollPeriodID);
                    query.Select(query, pp.PayrollPeriodName);
                    query.OrderBy(pp.PayrollPeriodCode.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}