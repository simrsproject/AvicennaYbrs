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
    public partial class ClosingWageTransaction : esClosingWageTransaction
    {
        public static ClosingWageTransaction Get(int payrollPeriodID)
        {
            ClosingWageTransaction entity = new ClosingWageTransaction();
            entity.Query.Where(entity.Query.PayrollPeriodID == payrollPeriodID);
            if (entity.Query.Load())
                return entity;
            else
                return null;
        }

        public static int ProcessWageTransaction(int payrollPeriodID, Int64 wageProcessTypeID, DateTime transactionDate, string userID, int personID)
        {

            esParameters prms = new esParameters();

            prms.Add("p_PayrollPeriodID", payrollPeriodID, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_WageProcessTypeID", wageProcessTypeID, esParameterDirection.Input, DbType.Int64, 0);
            prms.Add("p_TransactionDate", transactionDate, esParameterDirection.Input, DbType.DateTime, 8);
            prms.Add("p_UserID", userID, esParameterDirection.Input, DbType.String, 40);
            prms.Add("p_PersonID", personID, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            ClosingWageTransaction entity = new ClosingWageTransaction();
            entity.es.Connection.CommandTimeout = 60 * 10; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_Wage_Process_Main", prms);
            return (int)prms["Return_Value"].Value;
        }

        public static int ProcessWageTax(int payrollPeriodID, int wageProcessTypeID, string userID, int personID)
        {
            esParameters prms = new esParameters();

            prms.Add("p_PayrollPeriodID", payrollPeriodID, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_WageProcessTypeID", wageProcessTypeID, esParameterDirection.Input, DbType.Int64, 0);
            prms.Add("p_UserID", userID, esParameterDirection.Input, DbType.String, 40);
            prms.Add("p_PersonID", personID, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            ClosingWageTransaction entity = new ClosingWageTransaction();
            entity.es.Connection.CommandTimeout = 60 * 5; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_Wage_Process_Tax", prms);
            return (int)prms["Return_Value"].Value;
        }
    }
}