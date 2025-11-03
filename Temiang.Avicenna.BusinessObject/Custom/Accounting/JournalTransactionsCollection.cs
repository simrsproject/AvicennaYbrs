/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 3/3/2010 4:56:15 PM
===============================================================================
*/

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject 
{
	public partial class JournalTransactionsCollection : esJournalTransactionsCollection
	{
        public DataTable GetUnJournal(string JournalCode, DateTime dFrom, DateTime dTo)
        {
            string cmd = string.Empty;
            cmd = "sp_UnJournalTransaction";

            var pars = new esParameters();
            var pJournalCode = new esParameter("JournalCode", JournalCode, esParameterDirection.Input, DbType.String, 10);
            pars.Add(pJournalCode);

            var pDateFrom = new esParameter("dStart", dFrom);
            pars.Add(pDateFrom);
            var pDateTo = new esParameter("dEnd ", dTo);
            pars.Add(pDateTo);

            es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }


        public DataTable GetUnCashEntriedDownPayment()
        {
            string cmd = string.Empty;
            cmd = "sp_UnCashEntriedDownPayment";

            var pars = new esParameters();

            es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }
	}
}
