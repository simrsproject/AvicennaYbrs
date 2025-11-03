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
    public partial class RlTxReport : esRlTxReport
    {
        public static int ProcessRlTxReport4B(string reportNo, int fromMonth, int toMonth, int year, string userId)
        {
            esParameters prms = new esParameters();

            prms.Add("p_RlTxReportNo", reportNo, esParameterDirection.Input, DbType.String, 20);
            prms.Add("p_FromMonth", fromMonth, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_ToMonth", toMonth, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_Year", year, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_UserID", userId, esParameterDirection.Input, DbType.String, 20);

            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            RlTxReport entity = new RlTxReport();
            entity.es.Connection.CommandTimeout = 60 * 10; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_RlTxReport4B", prms);
            return (int)prms["Return_Value"].Value;
        }

        public static int ProcessRlTxReport4BSebab(string reportNo, int fromMonth, int toMonth, int year, string userId)
        {
            esParameters prms = new esParameters();

            prms.Add("p_RlTxReportNo", reportNo, esParameterDirection.Input, DbType.String, 20);
            prms.Add("p_FromMonth", fromMonth, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_ToMonth", toMonth, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_Year", year, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_UserID", userId, esParameterDirection.Input, DbType.String, 20);

            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            RlTxReport entity = new RlTxReport();
            entity.es.Connection.CommandTimeout = 60 * 10; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_RlTxReport4BSebab", prms);
            return (int)prms["Return_Value"].Value;
        }
    }
}
