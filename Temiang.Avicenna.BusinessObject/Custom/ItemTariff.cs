using System;
using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemTariff
    {
        public DataTable GetHistory(string itemID)
        {
            var tariffQuery = new ItemTariffQuery("a");
            var std = new AppStandardReferenceItemQuery("b");
            var classQuery = new ClassQuery("c");
            var treq1 = new ItemTariffRequestQuery("d");
            var treq2 = new ItemTariffRequest2Query("e");

            tariffQuery.InnerJoin(std).On(tariffQuery.SRTariffType == std.ItemID & std.StandardReferenceID == "TariffType");
            tariffQuery.InnerJoin(classQuery).On(tariffQuery.ClassID == classQuery.ClassID);
            tariffQuery.LeftJoin(treq1).On(tariffQuery.ReferenceNo == treq1.TariffRequestNo);
            tariffQuery.LeftJoin(treq2).On(tariffQuery.ReferenceNo == treq2.TariffRequestNo);

            tariffQuery.OrderBy(tariffQuery.StartingDate.Descending, tariffQuery.SRTariffType.Ascending, tariffQuery.ClassID.Ascending);

            tariffQuery.Select(tariffQuery, classQuery.ClassName, std.ItemName.As("TariffTypeName"),
                tariffQuery.ReferenceNo, tariffQuery.DiscPercentage,
                @"<ISNULL(d.Notes, e.Notes) AS 'Notes'>",
                @"<ISNULL(d.LastUpdateByUserID, e.LastUpdateByUserID) AS 'UpdateBy'>");
            tariffQuery.Where(tariffQuery.ItemID == itemID);
            DataTable dtb = tariffQuery.LoadDataTable();
            return dtb;
        }
        public DataTable GetItemTariffComponent(string tariffType, string itemID, string classID, DateTime startingDate)
        {
            ItemTariffComponentQuery itemCompQuery = new ItemTariffComponentQuery("a");
            TariffComponentQuery compQuery = new TariffComponentQuery("b");
            itemCompQuery.InnerJoin(compQuery).On(itemCompQuery.TariffComponentID == compQuery.TariffComponentID);
            itemCompQuery.Select(itemCompQuery.TariffComponentID, itemCompQuery.Price, itemCompQuery.IsAllowDiscount, itemCompQuery.IsAllowVariable, compQuery.TariffComponentName);
            itemCompQuery.Where(
                itemCompQuery.SRTariffType == tariffType,
                itemCompQuery.ItemID == itemID,
                itemCompQuery.ClassID == classID,
                itemCompQuery.StartingDate.Date() == startingDate
                );
            itemCompQuery.OrderBy(compQuery.TariffComponentID.Ascending);
            DataTable dtb = itemCompQuery.LoadDataTable();
            return dtb;
        }
        public static int InsertFromImport(string refNo, string userId)
        {
            esParameters prms = new esParameters();

            prms.Add("p_ReferenceNo", refNo, esParameterDirection.Input, DbType.String, 20);
            prms.Add("p_UserID", userId, esParameterDirection.Input, DbType.String, 15);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            var entity = new ItemTariff();
            entity.es.Connection.CommandTimeout = 60 * 10; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_ItemTariffRequestItemToImportProcess_InsertToItemTariff", prms);

            return (int)prms["Return_Value"].Value;
        }
        public static int InsertFromImportNew(string refNo, string userId)
        {
            esParameters prms = new esParameters();

            prms.Add("p_ReferenceNo", refNo, esParameterDirection.Input, DbType.String, 20);
            prms.Add("p_UserID", userId, esParameterDirection.Input, DbType.String, 15);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            var entity = new ItemTariff();
            entity.es.Connection.CommandTimeout = 60 * 10; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_ItemTariffRequestItemToImportProcess_InsertToItemTariff_New", prms);

            return (int)prms["Return_Value"].Value;
        }
        public static int InsertFromProcess(string refNo, string userId)
        {
            esParameters prms = new esParameters();

            prms.Add("p_ReferenceNo", refNo, esParameterDirection.Input, DbType.String, 20);
            prms.Add("p_UserID", userId, esParameterDirection.Input, DbType.String, 15);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            var entity = new ItemTariff();
            entity.es.Connection.CommandTimeout = 60 * 10; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_ItemTariffRequestProcess_InsertToItemTariff", prms);

            return (int)prms["Return_Value"].Value;
        }

        public bool IsComponentsHaveAllowVariable()
        {
            var ret = false;

            if (this.ItemID != null)
            {
                var itcc = new ItemTariffComponentCollection();
                var query = new ItemTariffComponentQuery("a");
                query.Where(
                    query.SRTariffType == this.SRTariffType,
                    query.ItemID == this.ItemID,
                    query.ClassID == this.ClassID,
                    query.StartingDate.Date() == this.StartingDate
                    );

                itcc.Load(query);
                foreach (var itc in itcc)
                {
                    ret = itc.IsAllowVariable ?? false;
                    if (ret) break;
                }
            }
            return ret;
        }

        public void UpdateCitoFromStdRef(string SRCitoPercentage)
        {
            if (this.IsCitoFromStandardReference ?? false)
            {
                var appstdi = new AppStandardReferenceItem();
                appstdi.Query.Where(appstdi.Query.StandardReferenceID == "CitoPercentage",
                    appstdi.Query.ItemID == SRCitoPercentage);
                if (appstdi.Load(appstdi.Query))
                {
                    this.IsCitoInPercent = true;
                    this.CitoValue = appstdi.NumericValue ?? 0;
                }
            }
        }
    }
}