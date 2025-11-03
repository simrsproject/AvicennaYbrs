using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Transaction
{
    public partial class OvertimeList : BasePageList
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 300;

            UrlPageSearch = "OvertimeSearch.aspx?type=" + FormType;
            UrlPageDetail = "OvertimeDetail.aspx?type=" + FormType;

            switch (FormType)
            {
                case "":
                    ProgramID = AppConstant.Program.EmployeeOvertime;
                    break;
                case "appr":
                    ProgramID = AppConstant.Program.EmployeeOvertimeApproval;
                    break;
                case "verif":
                    ProgramID = AppConstant.Program.EmployeeOvertimeVerified;
                    break;
            }

            if (!IsPostBack)
            {
            }
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", FormType);
            return script;
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
            string id = dataItem.GetDataKeyValue(EmployeeOvertimeMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("OvertimeDetail.aspx?md={0}&id={1}&type={2}", mode, id, FormType);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeeOvertimes;
        }

        private DataTable EmployeeOvertimes
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                EmployeeOvertimeQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmployeeOvertimeQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new EmployeeOvertimeQuery("a");
                    var personal = new VwEmployeeTableQuery("b");
                    var payrollperiod = new PayrollPeriodQuery("c");
                    var unit = new OrganizationUnitQuery("d");
                    var usr = new AppUserQuery("e");
                    var usrVal = new AppUserQuery("usrVal");

                    query.InnerJoin(personal).On(query.SupervisorID == personal.PersonID);
                    query.InnerJoin(payrollperiod).On(query.PayrollPeriodID == payrollperiod.PayrollPeriodID);
                    query.LeftJoin(unit).On(personal.ServiceUnitID == unit.OrganizationUnitID);
                    query.LeftJoin(usr).On(query.VerifiedByUserID == usr.UserID);
                    query.LeftJoin(usrVal).On(query.ValidatedByUserID == usrVal.UserID);

                    if (FormType == "")
                        query.Where(query.CreatedByUserID == AppSession.UserLogin.UserID);

                    query.OrderBy
                        (
                            query.TransactionNo.Descending
                        );

                    query.Select(
                        query.TransactionNo,
                        query.TransactionDate,
                        payrollperiod.PayrollPeriodName,
                        personal.EmployeeNumber,
                        personal.EmployeeName.As("SupervisorName"),
                        unit.OrganizationUnitName.As("ServiceUnitName"),
                        query.IsApproved,
                        query.IsVoid,
                        query.IsVerified,
                        query.VerifiedDateTime,
                        usr.UserName.As("VerifiedBy"),
                        query.IsValidated,
                        query.ValidatedDateTime,
                        usrVal.UserName.As("ValidatedBy")
                        );
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
