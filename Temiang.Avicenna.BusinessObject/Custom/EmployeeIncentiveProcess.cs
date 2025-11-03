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
    public partial class EmployeeIncentiveProcess : esEmployeeIncentiveProcess
    {
        public static int ProcessItem(int id, int payrollPeriodId, string userId)
        {
            esParameters prms = new esParameters();

            prms.Add("p_EmployeeIncentiveProcessID", id, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_PayrollPeriodID", payrollPeriodId, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_UserID", userId, esParameterDirection.Input, DbType.String, 40);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            var entity = new EmployeeIncentiveProcess();
            entity.es.Connection.CommandTimeout = 60 * 10; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_EmployeeIncentiveProcess", prms);
            return (int)prms["Return_Value"].Value;
        }

        public static int Approved(int id, int payrollPeriodId, string userId)
        {
            esParameters prms = new esParameters();

            prms.Add("p_EmployeeIncentiveProcessID", id, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_PayrollPeriodID", payrollPeriodId, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_UserID", userId, esParameterDirection.Input, DbType.String, 40);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            var entity = new EmployeeIncentiveProcess();
            entity.es.Connection.CommandTimeout = 60 * 10; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_EmployeeIncentiveProcess_Approved", prms);
            return (int)prms["Return_Value"].Value;
        }

        public static int UnApproved(int id, int payrollPeriodId, string userId)
        {
            esParameters prms = new esParameters();

            prms.Add("p_EmployeeIncentiveProcessID", id, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_PayrollPeriodID", payrollPeriodId, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_UserID", userId, esParameterDirection.Input, DbType.String, 40);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            var entity = new EmployeeIncentiveProcess();
            entity.es.Connection.CommandTimeout = 60 * 10; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_EmployeeIncentiveProcess_UnApproved", prms);
            return (int)prms["Return_Value"].Value;
        }
    }
}
