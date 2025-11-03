using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Process
{
    public partial class UpdateBasicSalaryByPositionGradeList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "UpdateBasicSalaryByPositionGradeSearch.aspx";
            UrlPageDetail = "UpdateBasicSalaryByPositionGrade.aspx";

            ProgramID = AppConstant.Program.ProcessUpdateBasicSalaryByPositionGrade;

            WindowSearch.Height = 300;
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
            string id = dataItem.GetDataKeyValue(EmployeePeriodicSalaryMetadata.ColumnNames.EmployeePeriodicSalaryID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", "UpdateBasicSalaryByPositionGradeDetail.aspx", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeePeriodicSalarys;
        }

        private DataTable EmployeePeriodicSalarys
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                EmployeePeriodicSalaryQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmployeePeriodicSalaryQuery)Session[SessionNameForQuery];
                }
                else
                {
                    SalaryComponentQuery salary = new SalaryComponentQuery("e");
                    AppStandardReferenceItemQuery process = new AppStandardReferenceItemQuery("d");
                    PayrollPeriodQuery period = new PayrollPeriodQuery("c");
                    PersonalInfoQuery personal = new PersonalInfoQuery("b");
                    query = new EmployeePeriodicSalaryQuery("a");
                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                                    query.EmployeePeriodicSalaryID,
                                    query.PayrollPeriodID,
                                    query.PersonID,
                                    personal.EmployeeNumber,
                                    personal.EmployeeName,
                                    query.SalaryComponentID,
                                    salary.SalaryComponentName,
                                    period.PayrollPeriodName,
                                    query.SRProcessType,
                                    process.ItemName.As("ProcessTypeName"),
                                    query.Amount,
                                    query.LastUpdateDateTime,
                                    query.LastUpdateByUserID
                                );
                    query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                    query.InnerJoin(period).On(query.PayrollPeriodID == period.PayrollPeriodID);
                    query.InnerJoin(process).On
                           (
                               query.SRProcessType == process.ItemID &&
                               process.StandardReferenceID == AppEnum.StandardReference.ProcessType
                           );
                    query.InnerJoin(salary).On(query.SalaryComponentID == salary.SalaryComponentID);
                    query.Where(query.SRProcessType == AppSession.Parameter.ProcessTypePositionGrade);
                    query.OrderBy(period.PayrollPeriodCode.Descending, query.SalaryComponentID.Ascending, query.PersonID.Ascending);
                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
