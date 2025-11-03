using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Dal.Interfaces;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicFeeByNumberOfPatients
    {
        public static int PhysicianFeeCalculationProcess(DateTime d1, DateTime d2, string paramedicId, string userId)
        {
            esParameters prms = new esParameters();

            prms.Add("p_FromDate", d1, esParameterDirection.Input, DbType.DateTime, 8);
            prms.Add("p_ToDate", d2, esParameterDirection.Input, DbType.DateTime, 8);
            prms.Add("p_ParamedicID", paramedicId, esParameterDirection.Input, DbType.String, 10);
            prms.Add("p_UserID", userId, esParameterDirection.Input, DbType.String, 15);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            var entity = new ParamedicFeeByNumberOfPatients();
            entity.es.Connection.CommandTimeout = 60 * 10; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_PhysicianFeeByNumberOfPatientsCalculationProcess_Main", prms);
            return (int)prms["Return_Value"].Value;
        }

        public static int PhysicianFeeReCalculationProcess(DateTime d1, DateTime d2, string paramedicId, string userId)
        {
            esParameters prms = new esParameters();

            prms.Add("p_FromDate", d1, esParameterDirection.Input, DbType.DateTime, 8);
            prms.Add("p_ToDate", d2, esParameterDirection.Input, DbType.DateTime, 8);
            prms.Add("p_ParamedicID", paramedicId, esParameterDirection.Input, DbType.String, 10);
            prms.Add("p_UserID", userId, esParameterDirection.Input, DbType.String, 15);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            var entity = new ParamedicFeeByNumberOfPatients();
            entity.es.Connection.CommandTimeout = 60 * 10; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_PhysicianFeeByNumberOfPatientsReCalculationProcess", prms);
            return (int)prms["Return_Value"].Value;
        }
    }
}
