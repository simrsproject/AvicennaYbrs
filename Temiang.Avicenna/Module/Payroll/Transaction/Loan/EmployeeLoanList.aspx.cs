using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Transaction
{
    public partial class EmployeeLoanList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "EmployeeLoanSearch.aspx";
            UrlPageDetail = "EmployeeLoanDetail.aspx";

            ProgramID = AppConstant.Program.EmployeeLoan; //TODO: Isi ProgramID
            IsProgramUseSignature = true; // Optional isi passcode ketika akan akses menu ini
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
            string id = dataItem.GetDataKeyValue(EmployeeLoanMetadata.ColumnNames.EmployeeLoanID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeeLoans;
        }

        private DataTable EmployeeLoans
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				EmployeeLoanQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmployeeLoanQuery)Session[SessionNameForQuery];
                }
                else
                {
                    AppStandardReferenceItemQuery loan = new AppStandardReferenceItemQuery("d"); 
                    PayrollPeriodQuery period = new PayrollPeriodQuery("c");
                    PersonalInfoQuery personal = new PersonalInfoQuery("b");
                    query = new EmployeeLoanQuery("a");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                                    query.EmployeeLoanID,
                                    query.PersonID,
                                    personal.EmployeeNumber,
                                    personal.EmployeeName,
                                    query.LoanDate,
                                    query.Amount,
                                    query.SRPurposeOfLoan,
                                    loan.ItemName.As("PurposeOfLoanName"),
                                    query.NumberOfInstallment,
                                    query.AmountOfInstallment,
                                    period.PayrollPeriodName.As("StartPaymentPeriode"),
                                    query.LastUpdateDateTime,
                                    query.LastUpdateByUserID,
                                    query.CoverageAmount, 
                                    query.IsApproved
                                );
                    query.InnerJoin(period).On(query.StartPaymentPeriode == period.PayrollPeriodID);
                    query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                    query.InnerJoin(loan).On
                           (
                               query.SRPurposeOfLoan == loan.ItemID &
                               loan.StandardReferenceID == "PurposeOfLoan"
                           );
                    query.OrderBy(query.LoanDate.Descending, query.PersonID.Ascending);

                }
				
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

