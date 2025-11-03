
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Temiang.Dal.Interfaces; using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class Budgeting
    {
        public static void CekBudget(string ServiceUnitID, string[] ItemIDs, int YearOfBudget){
            var i = new ItemQuery("i");
            var supa = new ServiceUnitProductAccountMappingV2Query("supa");
            var jd = new JournalTransactionDetailsQuery("jd");
            var j = new JournalTransactionsQuery("j");

            i.InnerJoin(supa).On(i.ProductAccountID == supa.ProductAccountId && supa.ServiceUnitId == ServiceUnitID)
                .InnerJoin(jd).On(supa.ChartOfAccountIdExpense == jd.ChartOfAccountId)
                .InnerJoin(j).On(jd.JournalId == j.JournalId)
                .Where(i.ItemID.In(ItemIDs), j.IsVoid == 0, j.TransactionDate.DatePart("YYYY") == YearOfBudget)
                .Select(
                    i.ItemID, supa.ChartOfAccountIdExpense,
                    jd.Debit.Sum().As("SumDebit"), jd.Credit.Sum().As("SumCredit"))
                .GroupBy(i.ItemID, supa.ChartOfAccountIdExpense);
        }
    }

    public partial class BudgetingCollection : esBudgetingCollection
	{
        public DataTable LoadByPage(string BudgetingNo, string Periode, string ServiceUnitID, string SRBudgetingGroup, bool IsByItem,
            int iRowStart, int iRowFinish, out int iRowCount)
        {
            var str = "sp_GetBudgeting";
            var pars = new esParameters();
            var pBudgetingNo = new esParameter("pBudgetingNo", BudgetingNo, esParameterDirection.Input, DbType.String, 20);
            var pPeriode = new esParameter("pPeriode", Periode, esParameterDirection.Input, DbType.String, 4);
            var pServiceUnitID = new esParameter("pServiceUnitID", ServiceUnitID, esParameterDirection.Input, DbType.String, 10);
            var pSRGudgetingGroup = new esParameter("pSRGudgetingGroup", SRBudgetingGroup, esParameterDirection.Input, DbType.String, 10);
            var pIsByItem = new esParameter("pIsByItem", IsByItem, esParameterDirection.Input, DbType.Boolean, 1);
            pars.Add(pBudgetingNo); pars.Add(pPeriode); pars.Add(pServiceUnitID); pars.Add(pSRGudgetingGroup); pars.Add(pIsByItem);
            var piRowStart = new esParameter("iRowStart", iRowStart, esParameterDirection.Input);
            var piRowFinish = new esParameter("iRowFinish", iRowFinish, esParameterDirection.Input);
            pars.Add(piRowStart); pars.Add(piRowFinish);

            var ret = FillDataTable(Temiang.Dal.DynamicQuery.esQueryType.StoredProcedure, str, pars);
            iRowCount = 0;

            if (ret.Rows.Count > 0)
            {
                iRowCount = System.Convert.ToInt32(ret.Rows[0]["iRowCount"]);
            }
            return ret;
        }
        public DataTable GetBudgetRealizationForChart(int year)
        {
            var str = "sp_BudgetRealizationChart";
            var pars = new esParameters();
            var pPeriode = new esParameter("Periode", year, esParameterDirection.Input);
            pars.Add(pPeriode);
            return FillDataTable(Temiang.Dal.DynamicQuery.esQueryType.StoredProcedure, str, pars);
        }
    }
}
