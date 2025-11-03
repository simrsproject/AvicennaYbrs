/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 7/27/2011 11:29:31 PM
===============================================================================
*/

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Temiang.Dal.Interfaces;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.BusinessObject
{
	public partial class CashTransactionBalanceCollection : esCashTransactionBalanceCollection
	{
        public DataTable GetCashTransactionByPage(string BankID, DateTime DateEnd, bool UnReconciledOnly,
            int iRowStart, int iRowFinish, 
            out int iRowCount, out decimal Balance, out decimal ReconciledBalance)
        {
            var str = "sp_GetCashTransactionForRecon";
            var pars = new esParameters();
            var pBankID = new esParameter("BankID", BankID, esParameterDirection.Input, DbType.String, 10);
            var pDateEnd = new esParameter("DateEnd", DateEnd, esParameterDirection.Input);
            var pUnReconciledOnly = new esParameter("UnReconciledOnly", UnReconciledOnly, esParameterDirection.Input);
            var piRowStart = new esParameter("iRowStart", iRowStart, esParameterDirection.Input);
            var piRowFinish = new esParameter("iRowFinish", iRowFinish, esParameterDirection.Input);
            pars.Add(pBankID); pars.Add(pDateEnd); pars.Add(pUnReconciledOnly); pars.Add(piRowStart); pars.Add(piRowFinish);

            var ret = FillDataTable(Temiang.Dal.DynamicQuery.esQueryType.StoredProcedure, str, pars);
            iRowCount = 0;
            Balance = 0;
            ReconciledBalance = 0;
            if (ret.Rows.Count > 0)
            {
                iRowCount = System.Convert.ToInt32(ret.Rows[0]["iRowCount"]);
                Balance = System.Convert.ToDecimal(ret.Rows[0]["Balance"]);
                ReconciledBalance = System.Convert.ToDecimal(ret.Rows[0]["ReconciledBalance"]);
            }
            else {
                var ctb = new CashTransactionBalanceQuery("ctb");
                var b = new BankQuery("b");
                var ct = new CashTransactionQuery("ct");
                ctb.InnerJoin(b).On(ctb.ChartOfAccountId.Equal(b.ChartOfAccountId))
                    .InnerJoin(ct).On(ctb.TransactionId.Equal(ct.TransactionId))
                    .Where(b.BankID.Equal(BankID), ct.TransactionDate.Date() <= DateEnd)
                    .Select("<ISNULL(SUM(ctb.DebitAmount - ctb.CreditAmount), 0) Balance>",
                    "<ISNULL(SUM(CASE WHEN ct.IsCleared = 1 THEN (ctb.DebitAmount - ctb.CreditAmount) ELSE 0 END), 0) ReconciledBalance>");
                var dttbl = ctb.LoadDataTable();
                if (dttbl.Rows.Count > 0) {
                    Balance = System.Convert.ToDecimal(dttbl.Rows[0]["Balance"]);
                    ReconciledBalance = System.Convert.ToDecimal(dttbl.Rows[0]["ReconciledBalance"]);
                }
            }

            return ret;
        }

        public DataTable GetCashTransactionExportToExcel(string BankID, DateTime DateEnd, bool UnReconciledOnly,
            out decimal Balance, out decimal ReconciledBalance)
        {
            var str = "sp_GetCashTransactionForReconNew";
            var pars = new esParameters();
            var pBankID = new esParameter("BankID", BankID, esParameterDirection.Input, DbType.String, 10);
            var pDateEnd = new esParameter("DateEnd", DateEnd, esParameterDirection.Input);
            var pUnReconciledOnly = new esParameter("UnReconciledOnly", UnReconciledOnly, esParameterDirection.Input);
            pars.Add(pBankID); pars.Add(pDateEnd); pars.Add(pUnReconciledOnly);

            var ret = FillDataTable(Temiang.Dal.DynamicQuery.esQueryType.StoredProcedure, str, pars);
            Balance = 0;
            ReconciledBalance = 0;
            if (ret.Rows.Count > 0)
            {
                Balance = System.Convert.ToDecimal(ret.Rows[0]["Balance"]);
                ReconciledBalance = System.Convert.ToDecimal(ret.Rows[0]["ReconciledBalance"]);
            }
            else
            {
                var ctb = new CashTransactionBalanceQuery("ctb");
                var b = new BankQuery("b");
                var ct = new CashTransactionQuery("ct");
                ctb.InnerJoin(b).On(ctb.ChartOfAccountId.Equal(b.ChartOfAccountId))
                    .InnerJoin(ct).On(ctb.TransactionId.Equal(ct.TransactionId))
                    .Where(b.BankID.Equal(BankID), ct.TransactionDate.Date() <= DateEnd)
                    .Select("<ISNULL(SUM(ctb.DebitAmount - ctb.CreditAmount), 0) Balance>",
                    "<ISNULL(SUM(CASE WHEN ct.IsCleared = 1 THEN (ctb.DebitAmount - ctb.CreditAmount) ELSE 0 END), 0) ReconciledBalance>");
                var dttbl = ctb.LoadDataTable();
                if (dttbl.Rows.Count > 0)
                {
                    Balance = System.Convert.ToDecimal(dttbl.Rows[0]["Balance"]);
                    ReconciledBalance = System.Convert.ToDecimal(dttbl.Rows[0]["ReconciledBalance"]);
                }
            }

            return ret;
        }

        public DataTable GetCashTransactionByBalanceByPage(string BankID, string Description, DateTime DateEnd, bool UnReconciledOnly,
            int iRowStart, int iRowFinish,
            out int iRowCount, out decimal Balance, out decimal ReconciledBalance)
        {
            var str = "sp_GetCashTransactionForReconByBalance";
            var pars = new esParameters();
            var pBankID = new esParameter("BankID", BankID, esParameterDirection.Input, DbType.String, 10);
            var pDescr = new esParameter("Description", Description, esParameterDirection.Input, DbType.String, 255);
            var pDateEnd = new esParameter("DateEnd", DateEnd, esParameterDirection.Input);
            var pUnReconciledOnly = new esParameter("UnReconciledOnly", UnReconciledOnly, esParameterDirection.Input);
            var piRowStart = new esParameter("iRowStart", iRowStart, esParameterDirection.Input);
            var piRowFinish = new esParameter("iRowFinish", iRowFinish, esParameterDirection.Input);
            pars.Add(pBankID); pars.Add(pDescr); pars.Add(pDateEnd); pars.Add(pUnReconciledOnly); pars.Add(piRowStart); pars.Add(piRowFinish);

            var ret = FillDataTable(Temiang.Dal.DynamicQuery.esQueryType.StoredProcedure, str, pars);
            iRowCount = 0;
            Balance = 0;
            ReconciledBalance = 0;
            if (ret.Rows.Count > 0)
            {
                iRowCount = System.Convert.ToInt32(ret.Rows[0]["iRowCount"]);
                Balance = System.Convert.ToDecimal(ret.Rows[0]["Balance"]);
                ReconciledBalance = System.Convert.ToDecimal(ret.Rows[0]["ReconciledBalance"]);
            }
            else
            {
                var ctb = new CashTransactionBalanceQuery("ctb");
                var b = new BankQuery("b");
                var ct = new CashTransactionQuery("ct");
                var ctd = new CashTransactionDetailQuery("ctd");
                ctb.InnerJoin(b).On(ctb.ChartOfAccountId.Equal(b.ChartOfAccountId))
                    .InnerJoin(ct).On(ctb.TransactionId.Equal(ct.TransactionId))
                    .InnerJoin(ctd).On(ctb.TransactionId.Equal(ctd.TransactionId))
                    .Where(b.BankID.Equal(BankID), ct.TransactionDate.Date() <= DateEnd)
                    .Select("<ISNULL(SUM(ctb.DebitAmount - ctb.CreditAmount), 0) Balance>",
                    "<ISNULL(SUM(CASE WHEN ReconcileID IS NOT NULL THEN (ctb.DebitAmount - ctb.CreditAmount) ELSE 0 END), 0) ReconciledBalance>");
                var dttbl = ctb.LoadDataTable();
                if (dttbl.Rows.Count > 0)
                {
                    //iRowCount = System.Convert.ToInt32(ret.Rows[0]["iRowCount"]);
                    Balance = System.Convert.ToDecimal(dttbl.Rows[0]["Balance"]);
                    ReconciledBalance = System.Convert.ToDecimal(dttbl.Rows[0]["ReconciledBalance"]);
                }
            }

            return ret;
        }
    }
}
