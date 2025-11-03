using System;
using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicGlobalSchedule
    {
        public string DayOfWeekName
        {
            get { return GetColumn("refToParamedicGlobalSchedule_DayOfWeekName").ToString(); }
            set { SetColumn("refToParamedicGlobalSchedule_DayOfWeekName", value); }
        }

        public string OperationalTimeName
        {
            get { return GetColumn("refToOperationalTime_OperationalTimeName").ToString(); }
            set { SetColumn("refToOperationalTime_OperationalTimeName", value); }
        }

        public string OperationalTimeBackcolor
        {
            get { return GetColumn("refToOperationalTime_OperationalTimeBackcolor").ToString(); }
            set { SetColumn("refToOperationalTime_OperationalTimeBackcolor", value); }
        }

        public static int InsertFromGlobalSchedule(string unitId, string parId, string year, string month, string userId)
        {
            esParameters prms = new esParameters();

            prms.Add("p_ServiceUnitID", unitId, esParameterDirection.Input, DbType.String, 10);
            prms.Add("p_ParamedicID", parId, esParameterDirection.Input, DbType.String, 10);
            prms.Add("p_Year", year, esParameterDirection.Input, DbType.String, 4);
            prms.Add("p_Month", month, esParameterDirection.Input, DbType.String, 2);
            prms.Add("p_UserID", userId, esParameterDirection.Input, DbType.String, 15);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            var entity = new ParamedicGlobalSchedule();
            entity.es.Connection.CommandTimeout = 60 * 10; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_ParamedicScheduleDate_InsertFromGlobalSchedule", prms);

            return (int)prms["Return_Value"].Value;
        }
    }
}
