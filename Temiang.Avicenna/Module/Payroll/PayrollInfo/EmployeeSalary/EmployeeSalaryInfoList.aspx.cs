using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.PayrollInfo
{
    public partial class EmployeeSalaryInfoList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "EmployeeSalaryInfoSearch.aspx";
            UrlPageDetail = "EmployeeSalaryInfoDetail.aspx";
            UrlPageDetailImport = "openWinImport('" + AppConstant.Program.EmployeeSalaryInfo + "');";

            ProgramID = AppConstant.Program.EmployeeSalaryInfo; //TODO: Isi ProgramID
            IsProgramUseSignature = true; // Optional isi passcode ketika akan akses menu ini

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
            string id = dataItem.GetDataKeyValue(EmployeeSalaryInfoMetadata.ColumnNames.PersonID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeeSalaryInfos;
        }

        private DataTable EmployeeSalaryInfos
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                VwEmployeeTableQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (VwEmployeeTableQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new VwEmployeeTableQuery("a");
                    var status = new AppStandardReferenceItemQuery("f");
                    var bank = new AppStandardReferenceItemQuery("e");
                    var taxStatus = new AppStandardReferenceItemQuery("d");
                    var paymentFrequency = new AppStandardReferenceItemQuery("c");
                    var salary = new EmployeeSalaryInfoQuery("b");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;

                    query.LeftJoin(salary).On
                        (
                            query.PersonID == salary.PersonID
                        );
                    query.LeftJoin(paymentFrequency).On
                           (
                               query.SRPaymentFrequency == paymentFrequency.ItemID &
                               paymentFrequency.StandardReferenceID == "PaySequent"
                           );
                    query.LeftJoin(taxStatus).On
                           (
                               query.SRTaxStatus == taxStatus.ItemID &
                               taxStatus.StandardReferenceID == "taxStatus"
                           );
                    query.LeftJoin(bank).On
                        (
                            salary.BankID == bank.ItemID && bank.StandardReferenceID == "BankHRD"
                         );
                    query.LeftJoin(status).On
                            (
                                query.SREmployeeStatus == status.ItemID &
                                status.StandardReferenceID == AppEnum.StandardReference.EmployeeStatus
                            );

                    query.Select(
                                    query.PersonID,
                                    query.EmployeeNumber,
                                    query.EmployeeName,
                                    query.Npwp,
                                    query.SRPaymentFrequency,
                                    paymentFrequency.ItemName.As("PaymentFrequencyName"),
                                    salary.SRTaxStatus,
                                    taxStatus.ItemName.As("TaxStatusName"),
                                    salary.BankID,
                                    bank.ItemName.As("BankName"),
                                    salary.BankAccountNo,
                                    salary.LastUpdateDateTime,
                                    salary.LastUpdateByUserID
                                );
                    query.Where(query.SREmploymentType != "0");

                    query.OrderBy(query.PersonID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "EmployeeName", "EmployeeNumber");
                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

