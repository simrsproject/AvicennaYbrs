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
	public partial class CashTransactionDetailCollection : esCashTransactionDetailCollection
	{
        public DataTable GetCashTransactionDetailRefByPage(string BankID, int iRowStart, int iRowFinish,
            DateTime? dateFrom, DateTime? dateTo, string description,
            out int iRowCount, out decimal Balance, out decimal ReconciledBalance)
        {
            var str = "sp_GetCashTransactionDetailRef";
            var pars = new esParameters();
            var pBankID = new esParameter("BankID", BankID, esParameterDirection.Input, DbType.String, 10);
            var piRowStart = new esParameter("iRowStart", iRowStart, esParameterDirection.Input);
            var piRowFinish = new esParameter("iRowFinish", iRowFinish, esParameterDirection.Input);
            pars.Add(pBankID); pars.Add(piRowStart); pars.Add(piRowFinish);

            if (dateFrom.HasValue) {
                var pDateFrom = new esParameter("DateFrom", dateFrom.Value, esParameterDirection.Input);
                pars.Add(pDateFrom);
            }
            if (dateTo.HasValue)
            {
                var pDateTo = new esParameter("DateTo", dateTo.Value, esParameterDirection.Input);
                pars.Add(pDateTo);
            }
            if (!description.Equals(string.Empty)) {
                var pDescription = new esParameter("Description", description, esParameterDirection.Input, DbType.String, 255);
                pars.Add(pDescription);
            }

            var ret = FillDataTable(Temiang.Dal.DynamicQuery.esQueryType.StoredProcedure, str, pars);
            iRowCount = 0;
            Balance = 0;
            ReconciledBalance = 0;
            if (ret.Rows.Count > 0)
            {
                iRowCount = System.Convert.ToInt32(ret.Rows[0]["iRowCount"]);
            }

            return ret;
        }

        public DataTable GetCashTransactionRealizationDetail(int ParentDetailId)
        {
            var str = "sp_GetCashTransactionRealizationDetail";
            var pars = new esParameters();
            var pDetailId = new esParameter("DetailId", ParentDetailId, esParameterDirection.Input);
            pars.Add(pDetailId);

            var ret = FillDataTable(Temiang.Dal.DynamicQuery.esQueryType.StoredProcedure, str, pars);
            return ret;
        }

        public bool LoadByTransactionId(int TransactionId){
            this.QueryReset();
            this.Query.Where(this.Query.TransactionId == TransactionId);
            return this.LoadAll();
        }
	}
}
