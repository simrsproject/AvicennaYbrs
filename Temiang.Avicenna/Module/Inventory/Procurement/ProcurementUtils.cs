using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory
{
    public class ProcurementUtils
    {
        private static decimal BalanceTotal(string itemID)
        {
            // Sub Query BalanceTotal
            var itemBalTot = new ItemBalanceQuery("ibt");
            itemBalTot.Select((itemBalTot.Balance.Sum().As("BalanceTotal")));
            itemBalTot.Where(itemBalTot.ItemID == itemID);
            var reader = itemBalTot.ExecuteReader();
            decimal total = 0;
            while (reader.Read())
            {
                total = reader.IsDBNull(0) ? 0 : reader.GetDecimal(0);
            }

            // Always call Close when done reading.
            reader.Close();

            return total;
        }

        public static void PopulateBalanceInfoByBlankValue(ItemTransactionItem itemTransactionItem)
        {
            itemTransactionItem.Balance = 0;
            itemTransactionItem.BalanceSG = 0;
            itemTransactionItem.Minimum = 0;
            itemTransactionItem.Maximum = 0;
            itemTransactionItem.BalanceTotal = 0;
            itemTransactionItem.SRMasterBaseUnit = string.Empty;
        }

        public static void PopulateBalanceInfoByStockGroup(ItemTransactionItem itemTransactionItem, string stockGroup, string locationID)
        {
            var ibsg = new ItemBalanceByStockGroup();
            ibsg.LoadByPrimaryKey(stockGroup, itemTransactionItem.ItemID);
            itemTransactionItem.Maximum = ibsg.Maximum ?? 0;
            itemTransactionItem.Minimum = ibsg.Minimum ?? 0;
            itemTransactionItem.BalanceSG = ibsg.Balance ?? 0;
            itemTransactionItem.BalanceTotal = BalanceTotal(itemTransactionItem.ItemID);

            var ib = new ItemBalance();
            if (ib.LoadByPrimaryKey(locationID, itemTransactionItem.ItemID))
            {
                itemTransactionItem.Balance = ib.Balance;
            }
        }

        public static string LocationIdByItemType(string itemType)
        {
            var locId = string.Empty;
            switch (itemType)
            {
                case ItemType.Medical:
                    locId = AppSession.Parameter.LocationPharmacyCentralWHID;
                    break;
                case ItemType.NonMedical:
                    locId = AppSession.Parameter.LocationLogisticCentralWHID;
                    break;
                case ItemType.Kitchen:
                    locId = AppSession.Parameter.LocationLogisticCentralWHID;
                    break;
            }
            return locId;
        }
        public static void PopulateBalanceInfoByItemType(ItemTransactionItem itemTransactionItem, string itemType)
        {
            var locID = LocationIdByItemType(itemType);
            PopulateBalanceInfoByLocation(itemTransactionItem, itemType, locID);
        }
        public static void PopulateBalanceInfoByLocation(ItemTransactionItem itemTransactionItem, string itemType, string locId)
        {
            var srMasterBaseUnit = string.Empty;
            switch (itemType)
            {
                case ItemType.Medical:
                    var ipm = new ItemProductMedic();
                    ipm.LoadByPrimaryKey(itemTransactionItem.ItemID);
                    srMasterBaseUnit = ipm.SRItemUnit;
                    break;
                case ItemType.NonMedical:
                    var ipnm = new ItemProductNonMedic();
                    ipnm.LoadByPrimaryKey(itemTransactionItem.ItemID);
                    srMasterBaseUnit = ipnm.SRItemUnit;
                    break;
                case ItemType.Kitchen:
                    var ik = new ItemKitchen();
                    ik.LoadByPrimaryKey(itemTransactionItem.ItemID);
                    srMasterBaseUnit = ik.SRItemUnit;
                    break;
            }
            var ib = new ItemBalance();
            if (ib.LoadByPrimaryKey(locId, itemTransactionItem.ItemID))
            {
                itemTransactionItem.Balance = ib.Balance;
                itemTransactionItem.BalanceSG = 0;
                itemTransactionItem.Minimum = ib.Minimum;
                itemTransactionItem.Maximum = ib.Minimum;
                itemTransactionItem.BalanceTotal = BalanceTotal(ib.ItemID);
            }
            else
            {
                itemTransactionItem.Balance = 0;
                itemTransactionItem.BalanceSG = 0;
                itemTransactionItem.Minimum = 0;
                itemTransactionItem.Maximum = 0;
                itemTransactionItem.BalanceTotal = 0;
            }
            itemTransactionItem.SRMasterBaseUnit = srMasterBaseUnit;
        }

        public static void PopulateWithHistPrice(ItemTransactionItem item, string itemType, string supplierID)
        {
            string purchaseUnit = string.Empty;
            string baseUnit = string.Empty;
            decimal convertionPurchaseUnitToBaseUnit = 1;
            PopulateWithHistPrice(item, itemType, supplierID, ref  purchaseUnit, ref  baseUnit, ref  convertionPurchaseUnitToBaseUnit);
        }

        public static void PopulateWithHistPrice(ItemTransactionItem item, string itemType, string supplierID, ref string purchaseUnit, ref string baseUnit, ref decimal convertionBaseUnitToPurchaseUnit)
        {
            if (itemType == ItemType.Medical)
            {
                var medic = new ItemProductMedic();
                medic.LoadByPrimaryKey(item.ItemID);

                if (string.IsNullOrEmpty(item.SRItemUnit))
                {
                    item.SRItemUnit = medic.SRPurchaseUnit;
                    item.ConversionFactor = medic.ConversionFactor;
                }
                item.Discount1Percentage = medic.PurchaseDiscount1 ?? 0;
                item.Discount2Percentage = medic.PurchaseDiscount2 ?? 0;

                if (item.SRItemUnit == medic.SRItemUnit)
                    item.Price = medic.PriceInBaseUnit ?? 0;
                else
                    item.Price = (medic.PriceInBaseUnit ?? 0) * (item.ConversionFactor ?? 0); //(medic.PriceInBaseUnit ?? 0) * (medic.ConversionFactor ?? 0);

                purchaseUnit = medic.SRPurchaseUnit;
                baseUnit = medic.SRItemUnit;
                convertionBaseUnitToPurchaseUnit = medic.ConversionFactor ?? 1;
            }
            else if (itemType == ItemType.NonMedical)
            {
                var nmedic = new ItemProductNonMedic();
                nmedic.LoadByPrimaryKey(item.ItemID);

                if (string.IsNullOrEmpty(item.SRItemUnit))
                {
                    item.SRItemUnit = nmedic.SRPurchaseUnit;
                    item.ConversionFactor = nmedic.ConversionFactor;
                }

                item.Discount1Percentage = nmedic.PurchaseDiscount1 ?? 0;
                item.Discount2Percentage = nmedic.PurchaseDiscount2 ?? 0;

                if (item.SRItemUnit == nmedic.SRItemUnit)
                    item.Price = nmedic.PriceInBaseUnit ?? 0;
                else
                    item.Price = (nmedic.PriceInBaseUnit ?? 0) * (item.ConversionFactor ?? 0); //(nmedic.PriceInBaseUnit ?? 0) * (nmedic.ConversionFactor ?? 0);

                purchaseUnit = nmedic.SRPurchaseUnit;
                baseUnit = nmedic.SRItemUnit;
                convertionBaseUnitToPurchaseUnit = nmedic.ConversionFactor ?? 1;
            }
            else if (itemType == ItemType.Kitchen)
            {
                var k = new ItemKitchen();
                k.LoadByPrimaryKey(item.ItemID);

                if (string.IsNullOrEmpty(item.SRItemUnit))
                {
                    item.SRItemUnit = k.SRPurchaseUnit;
                    item.ConversionFactor = k.ConversionFactor;
                }

                item.Discount1Percentage = k.PurchaseDiscount1 ?? 0;
                item.Discount2Percentage = k.PurchaseDiscount2 ?? 0;

                if (item.SRItemUnit == k.SRItemUnit)
                    item.Price = k.PriceInBaseUnit ?? 0;
                else
                    item.Price = (k.PriceInBaseUnit ?? 0) * (item.ConversionFactor ?? 0); //(k.PriceInBaseUnit ?? 0) * (k.ConversionFactor ?? 0);

                purchaseUnit = k.SRPurchaseUnit;
                baseUnit = k.SRItemUnit;
                convertionBaseUnitToPurchaseUnit = k.ConversionFactor ?? 1;
            }


            // Override if exist in SupplierItem
            var suppItem = new SupplierItem();
            if (suppItem.LoadByPrimaryKey(supplierID, item.ItemID))
            {
                item.Discount1Percentage = suppItem.PurchaseDiscount1 ?? 0;
                item.Discount2Percentage = suppItem.PurchaseDiscount2 ?? 0;

                item.Price = (suppItem.PriceInPurchaseUnit ?? 0) / suppItem.ConversionFactor * item.ConversionFactor;
            }

            item.Discount = (item.Price * item.Discount1Percentage / 100) +
                            ((item.Price - (item.Price * item.Discount1Percentage / 100)) *
                             item.Discount2Percentage / 100);
        }

        public static void PopulateWithHistPrice(ItemTransactionItem item, string itemType, string supplierID, string priceInItemUnit)
        {
            decimal convertionBaseUnitToPurchaseUnit = 1;
            item.SRItemUnit = priceInItemUnit;
            if (itemType == ItemType.Medical)
            {
                var medic = new ItemProductMedic();
                medic.LoadByPrimaryKey(item.ItemID);

                item.ConversionFactor = medic.SRItemUnit == priceInItemUnit ? 1 : medic.ConversionFactor;

                item.Discount1Percentage = medic.PurchaseDiscount1 ?? 0;
                item.Discount2Percentage = medic.PurchaseDiscount2 ?? 0;

                if (priceInItemUnit == medic.SRItemUnit)
                    item.Price = medic.PriceInBaseUnit ?? 0;
                else
                    item.Price = (medic.PriceInBaseUnit ?? 0) * (medic.ConversionFactor ?? 0);

                convertionBaseUnitToPurchaseUnit = medic.ConversionFactor ?? 1;
            }
            else if (itemType == ItemType.NonMedical)
            {
                var nmedic = new ItemProductNonMedic();
                nmedic.LoadByPrimaryKey(item.ItemID);
                item.ConversionFactor = nmedic.SRItemUnit == priceInItemUnit ? 1 : nmedic.ConversionFactor;

                item.Discount1Percentage = nmedic.PurchaseDiscount1 ?? 0;
                item.Discount2Percentage = nmedic.PurchaseDiscount2 ?? 0;

                if (priceInItemUnit == nmedic.SRItemUnit)
                    item.Price = nmedic.PriceInBaseUnit ?? 0;
                else
                    item.Price = (nmedic.PriceInBaseUnit ?? 0) * (nmedic.ConversionFactor ?? 0);

                convertionBaseUnitToPurchaseUnit = nmedic.ConversionFactor ?? 1;
            }
            else if (itemType == ItemType.Kitchen)
            {
                var k = new ItemKitchen();
                k.LoadByPrimaryKey(item.ItemID);
                item.ConversionFactor = k.SRItemUnit == priceInItemUnit ? 1 : k.ConversionFactor;

                item.Discount1Percentage = k.PurchaseDiscount1 ?? 0;
                item.Discount2Percentage = k.PurchaseDiscount2 ?? 0;

                if (priceInItemUnit == k.SRItemUnit)
                    item.Price = k.PriceInBaseUnit ?? 0;
                else
                    item.Price = (k.PriceInBaseUnit ?? 0) * (k.ConversionFactor ?? 0);

                convertionBaseUnitToPurchaseUnit = k.ConversionFactor ?? 1;
            }


            // Override if exist in SupplierItem
            var suppItem = new SupplierItem();
            if (suppItem.LoadByPrimaryKey(supplierID, item.ItemID))
            {
                item.Discount1Percentage = suppItem.PurchaseDiscount1 ?? 0;
                item.Discount2Percentage = suppItem.PurchaseDiscount2 ?? 0;

                item.Price = (suppItem.PriceInPurchaseUnit ?? 0) / suppItem.ConversionFactor * item.ConversionFactor;
            }

            item.Discount = (item.Price * item.Discount1Percentage / 100) +
                            ((item.Price - (item.Price * item.Discount1Percentage / 100)) *
                             item.Discount2Percentage / 100);
        }

        public static void HideColumnStockAndPriceInfo(GridTableView masterTableView)
        {
            var isShowPriceInPurchaseRequest =
                AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsShowPriceInPurchaseRequest);

            if (!isShowPriceInPurchaseRequest)
            {
                foreach (object col in masterTableView.Columns)
                {
                    if (col is GridBoundColumn || col is GridCalculatedColumn)
                    {
                        var boundCol = (GridColumn)col;
                        var uniqueName = boundCol.UniqueName;
                        if (uniqueName == "Price" || uniqueName == "Discount1Percentage" ||
                            uniqueName == "Discount2Percentage" || uniqueName == "Discount" || uniqueName == "Total")
                            boundCol.Visible = false;
                    }
                }
            }

            HideColumnStockInfo(masterTableView);
        }

        public static void HideColumnStockInfo(GridTableView masterTableView)
        {
            var stockInfoVisible = AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPOWithStockInfo);
            var balanceSGVisible = AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPOWithStockInfo) && AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorByStockGroup);
            var stockInfoTotalVisible = AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPOWithStockInfo) && AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPOWithStockInfoTotal);

            foreach (object col in masterTableView.Columns)
            {
                if (col is GridBoundColumn)
                {
                    var boundCol = (GridBoundColumn)col;
                    var dataField = boundCol.DataField;
                    if (dataField == "Minimum" || dataField == "Maximum" || dataField == "Balance" || dataField == "SRMasterBaseUnit")
                    {
                        boundCol.Visible = stockInfoVisible;
                    }
                    else if (dataField == "BalanceTotal")
                    {
                        boundCol.Visible = stockInfoTotalVisible;
                    }
                    else if (dataField == "BalanceSG")
                    {
                        boundCol.Visible = balanceSGVisible;
                    }
                }
            }
        }

        public static void HideColumnPriceInfo(GridTableView masterTableView)
        {
            var isShowPriceInPurchaseRequest =
                AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsShowPriceInPurchaseRequest);

            if (!isShowPriceInPurchaseRequest)
            {
                foreach (object col in masterTableView.Columns)
                {
                    if (col is GridBoundColumn || col is GridCalculatedColumn)
                    {
                        var boundCol = (GridColumn)col;
                        var uniqueName = boundCol.UniqueName;
                        if (uniqueName == "Price" || uniqueName == "Discount1Percentage" ||
                            uniqueName == "Discount2Percentage" || uniqueName == "Discount" || uniqueName == "Total")
                            boundCol.Visible = false;
                    }
                }
            }
        }
    }
}
