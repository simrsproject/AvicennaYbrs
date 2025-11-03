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
    public partial class EmployeeLeave : esEmployeeLeave
    {
        public static int AnnualLeaveProcess(string yearPeriod, DateTime sDate, DateTime eDate, string userId)
        {
            esParameters prms = new esParameters();

            prms.Add("p_YearPeriod", yearPeriod, esParameterDirection.Input, DbType.String, 4);
            prms.Add("p_StartDate", sDate, esParameterDirection.Input, DbType.DateTime, 8);
            prms.Add("p_EndDate", eDate, esParameterDirection.Input, DbType.DateTime, 8);
            prms.Add("p_UserID", userId, esParameterDirection.Input, DbType.String, 40);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            EmployeeLeave entity = new EmployeeLeave();
            entity.es.Connection.CommandTimeout = 60 * 10; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_EmployeeLeave_AnnualLeaveProcess", prms);
            return (int)prms["Return_Value"].Value;
        }
    }
}
