using System;
using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class OperationCostEstimation
    {
        public static int ProcessCalculationCost(string diagId, string procId, string srCategory, string classId, string userId)
        {
            var prms = new esParameters();


            prms.Add("p_DiagnoseID", diagId, esParameterDirection.Input, DbType.String, 20);
            prms.Add("p_ProcedureID", procId, esParameterDirection.Input, DbType.String, 10);
            prms.Add("p_SRProcedureCategory", srCategory, esParameterDirection.Input, DbType.String, 20);
            prms.Add("p_ClassID", classId, esParameterDirection.Input, DbType.String, 10);
            prms.Add("p_UserID", userId, esParameterDirection.Input, DbType.String, 15);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            var entity = new OperationCostEstimation();
            entity.es.Connection.CommandTimeout = 60 * 5; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_OperationCostEstimation", prms);
            return (int)prms["Return_Value"].Value;
        }
    }
}
