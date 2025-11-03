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
    public partial class MonthlyAttendanceDetail
    {
        public static int ProcessInsertWorkSchedule(int payrollPeriodID, string userID)
        {
            esParameters prms = new esParameters();

            prms.Add("p_PayrollPeriodID", payrollPeriodID, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_UserID", userID, esParameterDirection.Input, DbType.String, 40);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            var entity = new MonthlyAttendanceDetail();
            entity.es.Connection.CommandTimeout = 60 * 5; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_MonthlyAttendance_InsertWorkSchedule", prms);
            return (int)prms["Return_Value"].Value;
        }

        public static int ProcessCalculation(int payrollPeriodID, string userID, int personID)
        {
            esParameters prms = new esParameters();

            prms.Add("p_PayrollPeriodID", payrollPeriodID, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_UserID", userID, esParameterDirection.Input, DbType.String, 40);
            prms.Add("p_PersonID", personID, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            var entity = new MonthlyAttendanceDetail();
            entity.es.Connection.CommandTimeout = 60 * 5; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_MonthlyAttendance_Calculation", prms);
            return (int)prms["Return_Value"].Value;
        }

        public static int CreateHeader(int payrollPeriodID, string userID)
        {
            esParameters prms = new esParameters();

            prms.Add("p_PayrollPeriodID", payrollPeriodID, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_UserID", userID, esParameterDirection.Input, DbType.String, 40);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            var entity = new MonthlyAttendanceDetail();
            entity.es.Connection.CommandTimeout = 60 * 5; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_MonthlyAttendance_CreateHeader", prms);
            return (int)prms["Return_Value"].Value;
        }

        public static int CreateHeaderRsys(int payrollPeriodID, string userID)
        {
            esParameters prms = new esParameters();

            prms.Add("p_PayrollPeriodID", payrollPeriodID, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_UserID", userID, esParameterDirection.Input, DbType.String, 40);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            var entity = new MonthlyAttendanceDetail();
            entity.es.Connection.CommandTimeout = 60 * 5; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_MonthlyAttendance_CreateHeader_Rsys", prms);
            return (int)prms["Return_Value"].Value;
        }
    }
}
