using System;
using Temiang.Dal.Interfaces;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemTariffRequestItemToImport
    {
        public string ClassName
        {
            get { return GetColumn("refToClass_ItemName").ToString(); }
            set { SetColumn("refToClass_ItemName", value); }
        }

        public string TariffComponentName
        {
            get { return GetColumn("refToTariffComponent_TariffComponentName").ToString(); }
            set { SetColumn("refToTariffComponent_TariffComponentName", value); }
        }

        public static int InsertToTableProcess(string refNo, DateTime startingDate, string srItemType, string itemGroup, string srTariffType, DateTime importFromDate, string userId)
        {
            esParameters prms = new esParameters();

            prms.Add("p_ReferenceNo", refNo, esParameterDirection.Input, DbType.String, 20);
            prms.Add("p_StartingDate", startingDate, esParameterDirection.Input, DbType.DateTime, 8);
            prms.Add("p_SRItemType", srItemType, esParameterDirection.Input, DbType.String, 20);
            prms.Add("p_ItemGroup", itemGroup, esParameterDirection.Input, DbType.String, 10);
            prms.Add("p_SRTariffType", srTariffType, esParameterDirection.Input, DbType.String, 20);
            prms.Add("p_ImportFromDate", importFromDate, esParameterDirection.Input, DbType.DateTime, 8);
            prms.Add("p_UserID", userId, esParameterDirection.Input, DbType.String, 15);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            var entity = new ItemTariffRequestItemToImport();
            entity.es.Connection.CommandTimeout = 60 * 10; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_ItemTariffRequestItemToImportProcess_Main", prms);
            
            return (int)prms["Return_Value"].Value;
        }

        public static int InsertToTableProcessNew(string refNo, DateTime startingDate, string itemGroup, string srTariffType, string userId)
        {
            esParameters prms = new esParameters();

            prms.Add("p_ReferenceNo", refNo, esParameterDirection.Input, DbType.String, 20);
            prms.Add("p_StartingDate", startingDate, esParameterDirection.Input, DbType.DateTime, 8);
            prms.Add("p_ItemGroup", itemGroup, esParameterDirection.Input, DbType.String, 10);
            prms.Add("p_SRTariffType", srTariffType, esParameterDirection.Input, DbType.String, 20);
            prms.Add("p_UserID", userId, esParameterDirection.Input, DbType.String, 15);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            var entity = new ItemTariffRequestItemToImport();
            entity.es.Connection.CommandTimeout = 60 * 10; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_ItemTariffRequestItemToImportProcess_Main_New", prms);

            return (int)prms["Return_Value"].Value;
        }

        public static void DeletePrevRecord(string refNo)
        {
            var util = new Dal.Core.esUtility();

            string cmdText = string.Format(@"DELETE ItemTariffRequestItemToImport WHERE ReferenceNo = '{0}'", refNo);
            util.ExecuteNonQuery(esQueryType.Text, cmdText);
        }
    }
}
