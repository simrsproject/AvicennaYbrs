using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeeCardTransactionList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "EmployeeCardTransactionSearch.aspx";
            UrlPageDetail = "EmployeeCardTransactionDetail.aspx";

            WindowSearch.Height = 170;

            ProgramID = AppConstant.Program.EmployeeCardTransaction;
        }

        public override void OnMenuEditClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(EmployeeCardTransactionMetadata.ColumnNames.EmployeeCardTransactionID).ToString();
            Page.Response.Redirect("EmployeeCardTransactionDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeeCardTransactions;
        }

        private DataTable EmployeeCardTransactions
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null) return ((DataTable)(obj));

                EmployeeCardTransactionQuery query;
                if (Session[SessionNameForQuery] != null) query = (EmployeeCardTransactionQuery)Session[SessionNameForQuery];
                else
                {
                    query = new EmployeeCardTransactionQuery("a");
                    var emp = new VwEmployeeTableQuery("b");

                    query.Select(
                        query,
                        emp.EmployeeName
                        );
                    query.InnerJoin(emp).On(query.PersonID == emp.PersonID);
                    query.OrderBy(query.Datetime.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}