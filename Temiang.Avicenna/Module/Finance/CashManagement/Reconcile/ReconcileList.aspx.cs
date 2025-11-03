using System;
using System.Linq;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Finance.CashManagement.Reconcile
{
    public partial class ReconcileList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.RECONCILE;

            if (!IsPostBack)
            {
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit) return;

            grdList.DataSource = CashTransactions;
        }

        private DataTable CashTransactions
        {
            get
            {
                DataTable dtb;

                var b = new BankQuery("b");
                var coa = new ChartOfAccountsQuery("coa");
                var cbal = new CashTransactionBalanceQuery("cbal");
                var ct = new CashTransactionQuery("ct");
                b.InnerJoin(coa).On(b.ChartOfAccountId.Equal(coa.ChartOfAccountId) && b.IsToBeCleared.Equal(true))
                    .InnerJoin(cbal).On(coa.ChartOfAccountId.Equal(cbal.ChartOfAccountId))
                    .InnerJoin(ct).On(cbal.TransactionId.Equal(ct.TransactionId))
                    .Select(
                        b.BankID, b.BankName, coa.ChartOfAccountCode, coa.ChartOfAccountName,
                        cbal.DebitAmount.Sum().As("DebitAmount"), cbal.CreditAmount.Sum().As("CreditAmount"),
                        "<SUM(CASE WHEN ct.IsCleared = 1 THEN cbal.DebitAmount - cbal.CreditAmount ELSE 0 END) BalanceCleared>",
                        "<SUM(cbal.DebitAmount - cbal.CreditAmount) Balance>"
                    ).GroupBy(b.BankID, b.BankName, coa.ChartOfAccountCode, coa.ChartOfAccountName)
                    .OrderBy(b.BankName.Ascending);
                dtb = b.LoadDataTable();
                return dtb;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            switch (eventArgument)
            {
                case "export":
                    Export();
                    break;
            }
        }

        private void Export()
        {
            var tbl = CashTransactions;

            Common.CreateExcelFile.CreateExcelDocument(tbl, "BankReconcileSummary.xls", this.Response);
        }

    }
}