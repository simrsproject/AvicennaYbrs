using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Dal.Interfaces;
using System.Data;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport31ItemV2025
    {
        public static int ProcessInsert(string month, string endmonth, string year, string userId)
        {
            esParameters prms = new esParameters();

            prms.Add("p_FromMonth", month, esParameterDirection.Input, DbType.String, 2);
            prms.Add("p_ToMonth", endmonth, esParameterDirection.Input, DbType.String, 2);
            prms.Add("p_Year", year, esParameterDirection.Input, DbType.String, 4);
            prms.Add("p_UserID", userId, esParameterDirection.Input, DbType.String, 15);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            var entity = new RlTxReport31ItemV2025();
            entity.es.Connection.CommandTimeout = 60 * 5; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_IndikatorPelayananV2025", prms);
            return (int)prms["Return_Value"].Value;
        }
    }
}
