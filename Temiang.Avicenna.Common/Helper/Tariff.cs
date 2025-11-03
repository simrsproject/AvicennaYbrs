using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Common
{
    public partial class Helper
    {
        public class Tariff
        {
            //            public static ItemQuery GetItemQuery(string serviceUnitID, DateTime transactionDate, string tariffType, string classID, string itemType,
            //                string itemName, string guarantorId, bool IsVisibleOnly)
            //            {
            //                var unit = new ServiceUnit();
            //                unit.LoadByPrimaryKey(serviceUnitID);

            //                var query = new ItemQuery("a");
            //                var tariff = new ItemTariffQuery("b");
            //                var itemUnit = new ServiceUnitItemServiceQuery("c");
            //                var balance = new ItemBalanceQuery("d");

            //                query.Select
            //                    (
            //                        query.ItemID,
            //                        query.ItemName,
            //                        tariff.Price.Coalesce("0").As("Price"),
            //                        tariff.IsAllowCito,
            //                        tariff.IsAllowVariable,
            //                        tariff.IsAdminCalculation,
            //                        balance.Balance.Coalesce("0"),
            //                        query.SRItemType,
            //                        "<ISNULL(IsCitoFromStandardReference, 0) IsCitoFromStandardReference>"
            //                    );
            //                query.InnerJoin(tariff).On
            //                    (
            //                        query.ItemID == tariff.ItemID &
            //                        tariff.SRTariffType == tariffType &
            //                        tariff.ClassID == classID &
            //                        tariff.StartingDate <= transactionDate
            //                    );

            //                query.LeftJoin(balance).On
            //                    (
            //                        query.ItemID == balance.ItemID &
            //                        balance.LocationID == unit.GetMainLocationId(unit.ServiceUnitID)
            //                    );

            //                if (itemType == ItemType.Service || itemType == string.Empty)
            //                {
            //                    query.InnerJoin(itemUnit).On
            //                        (
            //                            query.ItemID == itemUnit.ItemID &
            //                            itemUnit.ServiceUnitID == serviceUnitID
            //                        );

            //                    if (IsVisibleOnly) query.Where(itemUnit.IsVisible == IsVisibleOnly);

            //                    var svc = new ItemServiceQuery("e");
            //                    query.LeftJoin(svc).On(query.ItemID == svc.ItemID);

            //                    query.Select(svc.SRItemUnit.Coalesce("''"));
            //                }
            //                else
            //                {
            //                    query.Select(@"<CASE WHEN a.SRItemType = '11' THEN ISNULL((SELECT SRItemUnit FROM ItemProductMedic WHERE ItemID = a.ItemID), '') 
            //                                    WHEN a.SRItemType = '21' THEN ISNULL((SELECT SRItemUnit FROM ItemProductNonMedic WHERE ItemID = a.ItemID), '') 
            //                                    WHEN a.SRItemType = '81' THEN ISNULL((SELECT SRItemUnit FROM ItemKitchen WHERE ItemID = a.ItemID), '') 
            //                                    ELSE ISNULL((SELECT SRItemUnit FROM ItemOptic WHERE ItemID = a.ItemID), '') 
            //                                    END AS 'SRItemUnit'>");
            //                    query.Where(
            //                        query.SRItemType.In(
            //                                ItemType.Medical,
            //                                ItemType.NonMedical,
            //                                ItemType.Kitchen
            //                            )
            //                        );
            //                }

            //                if (!string.IsNullOrEmpty(itemName))
            //                    query.Where(query.ItemName.Like(string.Format("%{0}%", itemName)));

            //                query.Where(
            //                    query.IsActive == true,
            //                    query.Or(
            //                            query.GuarantorID.IsNull(),
            //                            query.GuarantorID == string.Empty,
            //                            query.GuarantorID == guarantorId)
            //                            );
            //                query.OrderBy(
            //                    query.ItemName.Ascending,
            //                    tariff.StartingDate.Descending
            //                    );

            //                return query;
            //            }

            public static ItemQuery GetItemQuery(string serviceUnitID, string locationId, string itemType, string itemName, string guarantorId, bool IsVisibleOnly)
            {
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(serviceUnitID);

                var query = new ItemQuery("a");
                var itemUnit = new ServiceUnitItemServiceQuery("c");


                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        @"<CAST(0 AS NUMERIC(18,2)) AS Price>",
                        @"<CAST(0 AS BIT) AS IsAllowCito>",
                        @"<CAST(0 AS BIT) AS IsAllowVariable>",
                        @"<CAST(0 AS BIT) AS IsAdminCalculation>",
                        query.SRItemType,
                        @"<CAST(0 AS BIT) AS IsCitoFromStandardReference>"
                    );

                //query.LeftJoin(balance).On
                //    (
                //        query.ItemID == balance.ItemID &
                //        balance.LocationID == unit.LocationID
                //    );

                if (itemType == ItemType.Service || itemType == ItemType.Laboratory || itemType == ItemType.Radiology || itemType == string.Empty)
                {
                    query.InnerJoin(itemUnit).On
                        (
                            query.ItemID == itemUnit.ItemID &
                            itemUnit.ServiceUnitID == serviceUnitID
                        );

                    if (IsVisibleOnly) query.Where(itemUnit.IsVisible == IsVisibleOnly);

                    if (itemType == ItemType.Service)
                    {
                        var svc = new ItemServiceQuery("e");
                        query.LeftJoin(svc).On(query.ItemID == svc.ItemID);
                        query.Select(@"<0 AS Balance, ISNULL(e.SRItemUnit, 'X') AS SRItemUnit>");
                        //query.Select(svc.SRItemUnit.Coalesce("''"));
                    }
                    else query.Select(@"<0 AS Balance, 'X' AS SRItemUnit>");
                }
                else
                {
                    var balance = new ItemBalanceQuery("d");
                    var view = new VwItemProductMedicNonMedicQuery("e");
                    query.InnerJoin(balance).On
                        (
                            query.ItemID == balance.ItemID &
                            balance.LocationID == locationId
                        );
                    query.InnerJoin(view).On(query.ItemID == view.ItemID);

                    query.Select(@"<CASE WHEN a.SRItemType = '11' THEN ISNULL((SELECT SRItemUnit FROM ItemProductMedic WHERE ItemID = a.ItemID), '') 
                                    WHEN a.SRItemType = '21' THEN ISNULL((SELECT SRItemUnit FROM ItemProductNonMedic WHERE ItemID = a.ItemID), '') 
                                    WHEN a.SRItemType = '81' THEN ISNULL((SELECT SRItemUnit FROM ItemKitchen WHERE ItemID = a.ItemID), '') 
                                    ELSE ISNULL((SELECT SRItemUnit FROM ItemOptic WHERE ItemID = a.ItemID), '') 
                                    END AS 'SRItemUnit'>");
                    query.Select
                    (
                        balance.Balance.Coalesce("0")
                    );
                    query.Where(
                        query.SRItemType.In(
                                ItemType.Medical,
                                ItemType.NonMedical,
                                ItemType.Kitchen
                            ), balance.Balance >= 1, view.IsSalesAvailable == true
                        );
                }

                if (!string.IsNullOrEmpty(itemName))
                    query.Where(query.ItemName.Like(string.Format("%{0}%", itemName)));

                query.Where(
                    query.IsActive == true,
                    query.Or(
                            query.GuarantorID.IsNull(),
                            query.GuarantorID == string.Empty,
                            query.GuarantorID == guarantorId)
                            );
                query.OrderBy(
                    query.ItemName.Ascending
                    );

                return query;
            }

            public static ItemTariff GetItemTariffDetailPackage(IEnumerable<ItemTariffComponent> itcColl)
            {
                var it = new ItemTariff();
                it.SRTariffType = itcColl.First().SRTariffType;
                it.ItemID = itcColl.First().ItemID;
                it.ClassID = itcColl.First().ClassID;
                it.StartingDate = itcColl.First().StartingDate;
                it.Price = itcColl.Sum(x => x.Price);
                it.IsAdminCalculation = false;
                it.IsAllowDiscount = false;
                it.IsAllowVariable = false;
                it.IsAllowCito = false;
                it.IsCitoInPercent = false;
                it.CitoValue = 0;
                it.ReferenceNo = "";
                it.ReferenceTransactionCode = "";
                it.LastUpdateDateTime = DateTime.Now;
                it.LastUpdateByUserID = "";
                it.DiscPercentage = 0;
                it.IsCitoFromStandardReference = false;
                return it;
            }

            public static ItemTariff GetItemTariff(DateTime transactionDate, string tariffType,
                string chargeClassID, string marginClassID, string itemID, string guarantorID,
                bool isPrescription, string registrationType)
            {
                var item = new Item();
                item.LoadByPrimaryKey(itemID);

                decimal marginPercent = 0, hetPrice = 0, fixedPrice = 0;
                var marginId = string.Empty;
                if (item.SRItemType == ItemType.Medical || item.SRItemType == ItemType.NonMedical || item.SRItemType == ItemType.Kitchen)
                {
                    if (item.SRItemType == ItemType.Medical)
                    {
                        var medic = new ItemProductMedic();
                        if (medic.LoadByPrimaryKey(itemID))
                        {
                            marginId = medic.str.MarginID;
                            marginPercent = medic.MarginPercentage ?? 0;
                            fixedPrice = medic.SalesFixedPrice ?? 0;
                            hetPrice = medic.HET ?? 0;
                        }
                    }
                    else if (item.SRItemType == ItemType.NonMedical)
                    {
                        var nonMedic = new ItemProductNonMedic();
                        if (nonMedic.LoadByPrimaryKey(itemID))
                        {
                            marginId = nonMedic.str.MarginID;
                            marginPercent = nonMedic.MarginPercentage ?? 0;
                            fixedPrice = nonMedic.SalesFixedPrice ?? 0;
                        }
                    }
                    else
                    {
                        var kitchen = new ItemKitchen();
                        if (kitchen.LoadByPrimaryKey(itemID))
                        {
                            marginId = kitchen.str.MarginID;
                            marginPercent = kitchen.MarginPercentage ?? 0;
                            fixedPrice = kitchen.SalesFixedPrice ?? 0;
                        }
                    }
                }

                // 1. fixed price - master item
                // 2. margin guarantor
                // 3. margin class
                // 4. margin - master item

                var itemTariff = new ItemTariff();
                if (fixedPrice > 0)
                {
                    //--> 1
                    itemTariff.Price = fixedPrice;
                }
                else
                {
                    var query = new ItemTariffQuery();
                    query.es.Top = 1;
                    query.Where(
                        query.StartingDate.Date() <= transactionDate,
                        query.SRTariffType == tariffType,
                        query.ItemID == itemID,
                        query.ClassID == chargeClassID
                        );
                    query.OrderBy(query.StartingDate, esOrderByDirection.Descending);

                    if (itemTariff.Load(query))
                    {
                        itemTariff.Price = itemTariff.Price - (itemTariff.Price * (itemTariff.DiscPercentage ?? 0) / 100);

                        if (item.SRItemType == ItemType.Medical || item.SRItemType == ItemType.NonMedical || item.SRItemType == ItemType.Kitchen)
                        {
                            //hitung hna
                            decimal ppn = itemTariff.Ppn ?? -1;
                            decimal x = 100;
                            if (ppn == -1)
                                itemTariff.Price = itemTariff.Price / (decimal)1.1;
                            else
                                itemTariff.Price = itemTariff.Price / ((x + ppn) / x);

                            //hitung hna + ppn
                            ppn = (decimal)(AppSession.Parameter.Ppn);
                            itemTariff.Price = itemTariff.Price + (itemTariff.Price * ppn / x);

                            //--> 2
                            decimal marginGuarrPercent;
                            string marginGuarrId;
                            var g = new Guarantor();
                            g.LoadByPrimaryKey(guarantorID);
                            if (item.SRItemType == ItemType.Medical)
                            {
                                marginGuarrPercent = Convert.ToDecimal(g.ItemMedicMarginPercentage);
                                marginGuarrId = g.ItemMedicMarginID;
                            }
                            else
                            {
                                marginGuarrPercent = Convert.ToDecimal(g.ItemNonMedicMarginPercentage);
                                marginGuarrId = g.ItemNonMedicMarginID;
                            }

                            var gMarginItemGroup = new GuarantorItemGroupProductMargin();
                            if (gMarginItemGroup.LoadByPrimaryKey(guarantorID, item.ItemGroupID))
                            {
                                marginGuarrPercent = Convert.ToDecimal(gMarginItemGroup.MarginPercentage);
                                marginGuarrId = gMarginItemGroup.MarginID;
                            }

                            if (marginGuarrPercent > 0)
                                itemTariff.Price += ((marginGuarrPercent / 100) * itemTariff.Price);
                            else
                            {
                                if (!string.IsNullOrEmpty(marginGuarrId))
                                {
                                    marginGuarrPercent = GetMargin(Convert.ToDecimal(itemTariff.Price), marginGuarrId, registrationType, marginClassID);
                                    var priceVAT = GetPriceVAT(Convert.ToDecimal(itemTariff.Price), marginGuarrId, registrationType);

                                    //itemTariff.Price += ((marginGuarrPercent / 100) * itemTariff.Price);
                                    itemTariff.Price = priceVAT + ((marginGuarrPercent / 100) * priceVAT);
                                }
                                else
                                {
                                    //--> 3
                                    var c = new Class();
                                    c.LoadByPrimaryKey(chargeClassID);

                                    var c2 = new ItemProductMedicMarginDetail();

                                    var marginClassPercent = Convert.ToDecimal(item.SRItemType == ItemType.Medical ?
                                        (!c2.LoadByPrimaryKey(itemID, marginClassID) ? c.MarginPercentage : c2.AmountPercentage) : c.Margin2Percentage);

                                    if (marginClassPercent > 0)
                                        itemTariff.Price += ((marginClassPercent / 100) * itemTariff.Price);
                                    else
                                    {
                                        if (!AppSession.Parameter.IsUsingHetAsMaxSalesPrice && hetPrice > 0)
                                            itemTariff.Price = hetPrice;
                                        else
                                        {
                                            //--> 4
                                            if (marginPercent > 0)
                                                itemTariff.Price += ((marginPercent / 100) * itemTariff.Price);
                                            else
                                            {
                                                marginPercent = GetMargin(Convert.ToDecimal(itemTariff.Price), marginId, registrationType, marginClassID);
                                                var priceVAT = GetPriceVAT(Convert.ToDecimal(itemTariff.Price), marginId, registrationType);

                                                itemTariff.Price += ((marginPercent / 100) * itemTariff.Price);
                                                itemTariff.Price = priceVAT + ((marginPercent / 100) * priceVAT);
                                            }
                                        }
                                    }
                                }
                            }
                            // tambahkan ppn out
                            if (registrationType != "IPR")
                            {
                                decimal ppnOutRJ;
                                if (registrationType == "EMR")
                                    ppnOutRJ = Convert.ToDecimal(AppSession.Parameter.PpnOutRD);
                                else
                                    ppnOutRJ = Convert.ToDecimal(AppSession.Parameter.PpnOutRJ);
                                itemTariff.Price += ((ppnOutRJ / 100) * itemTariff.Price);
                            }

                            if ((hetPrice > 0) && (itemTariff.Price > hetPrice) && AppSession.Parameter.IsUsingHetAsMaxSalesPrice)
                                itemTariff.Price = hetPrice;

                            if (!(isPrescription && AppSession.Parameter.RoundingPrescription > 0))
                                itemTariff.Price = Helper.Rounding(itemTariff.Price ?? 0, AppEnum.RoundingType.Transaction);

                        }

                        itemTariff.AcceptChanges();
                    }
                    else
                    {
                        return null;
                    }
                }
                return itemTariff;
            }

            private static decimal GetItemTariffNonMargin(DateTime transactionDate, string tariffType, string classID, string itemID)
            {
                var query = new ItemTariffQuery();
                query.es.Top = 1;
                query.Where(
                    query.StartingDate.Date() <= transactionDate,
                    query.SRTariffType == tariffType,
                    query.ItemID == itemID,
                    query.ClassID == classID
                    );
                query.OrderBy(query.StartingDate, esOrderByDirection.Descending);

                decimal price = 0;
                var itemTariff = new ItemTariff();
                if (itemTariff.Load(query))
                {
                    price = (itemTariff.Price ?? 0) - ((itemTariff.Price ?? 0) * (itemTariff.DiscPercentage ?? 0) / 100);

                    //hitung hna
                    decimal ppn = itemTariff.Ppn ?? -1;
                    decimal x = 100;
                    if (ppn == -1)
                        price = price / (decimal)1.1;
                    else
                        price = price / ((x + ppn) / x);

                    //hitung hna + ppn
                    ppn = (decimal)(AppSession.Parameter.Ppn);
                    price = price + (price * ppn / x);
                }

                return price;
            }

            public static IEnumerable<ItemTariffComponent> GetItemTariffComponentCollection(string itemID)
            {
                return GetItemTariffComponentCollection(DateTime.Now.Date, string.Empty, string.Empty, itemID);

                // kebawah harusnya sudah tidak dipakai lagi

                //var coll = new ItemTariffComponentCollection();
                //coll.Query.Where(
                //    coll.Query.ItemID == itemID,
                //    coll.Query.StartingDate.Date() <= DateTime.Now.Date
                //    );
                //coll.Query.Where(
                //    coll.Query.Or(coll.Query.Price > 0,
                //    coll.Query.IsAllowVariable == true)
                //    );
                //coll.Query.OrderBy(
                //    coll.Query.TariffComponentID.Ascending,
                //    coll.Query.StartingDate.Descending
                //    );
                //coll.LoadAll();

                //var date = (coll.OrderByDescending(c => c.StartingDate).Select(c => c.StartingDate)).Distinct().Take(1).SingleOrDefault();
                //return coll.Where(c => c.StartingDate == date);
            }

            /// <summary>
            /// Function Get Item Tariff Component
            /// </summary>
            /// <param name="transactionDate">Transaction Date</param>
            /// <param name="tariffType">SRTariffType / string.Empty for all SRTariffType</param>
            /// <param name="classID">ClassID / string.Empty for all Class</param>
            /// <param name="itemID">Item ID</param>
            /// <returns></returns>
            public static IEnumerable<ItemTariffComponent> GetItemTariffComponentDetailPackageCollection(
                string itemPkgID, string detailItemID)
            {
                var itcColl = new ItemTariffComponentCollection();
                var ctColl = new TariffComponentCollection();
                ctColl.LoadAll();

                var pkgtcColl = new ItemPackageTariffComponentCollection();
                pkgtcColl.Query.Where(pkgtcColl.Query.ItemID == itemPkgID,
                    pkgtcColl.Query.DetailItemID == detailItemID);
                pkgtcColl.LoadAll();

                foreach (var ct in ctColl)
                {
                    //tambahan x.TariffComponentID="" supaya bisa edit item obat
                    var pkgtc = pkgtcColl.Where(x => x.TariffComponentID == ct.TariffComponentID || x.TariffComponentID == "").FirstOrDefault();
                    if (pkgtc == null) continue;

                    var itc = itcColl.AddNew();
                    itc.ItemID = detailItemID;
                    itc.ClassID = "";
                    itc.StartingDate = DateTime.Now;
                    itc.TariffComponentID = ct.TariffComponentID;
                    itc.Price = pkgtc.Price;
                    itc.IsAllowDiscount = false;
                    itc.IsAllowVariable = false;
                    itc.LastUpdateDateTime = DateTime.Now;
                    itc.LastUpdateByUserID = "";
                }

                return itcColl;
            }
            public static IEnumerable<ItemTariffComponent> GetItemTariffComponentCollection(DateTime transactionDate, string tariffType, string classID,
                string itemID)
            {
                var coll = new ItemTariffComponentCollection();
                var itcq = new ItemTariffComponentQuery("a");
                var itq = new ItemTariffQuery("b");
                itcq.InnerJoin(itq).On(
                    itcq.SRTariffType.Equal(itq.SRTariffType) &&
                    itcq.ItemID.Equal(itq.ItemID) &&
                    itcq.ClassID.Equal(itq.ClassID) &&
                    itcq.StartingDate.Equal(itq.StartingDate));
                itcq.Where(
                    itcq.ItemID == itemID,
                    itcq.StartingDate.Date() <= transactionDate
                    );
                if (!classID.Equals(string.Empty)) itcq.Where(itcq.ClassID == classID);
                if (!tariffType.Equals(string.Empty)) itcq.Where(itcq.SRTariffType == tariffType);

                itcq.Where(itcq.Or(itcq.Price > 0, itcq.IsAllowVariable == true));
                itcq.OrderBy(
                    itcq.TariffComponentID.Ascending,
                    itcq.StartingDate.Descending
                    );
                coll.Load(itcq);

                var date = (coll.OrderByDescending(c => c.StartingDate).Select(c => c.StartingDate)).Distinct().Take(1).SingleOrDefault();
                return coll.Where(c => c.StartingDate == date);
            }

            public static DataTable GetItemTariffComponent(DateTime transactionDate, string tariffType, string classID, string tariffComponentID,
                string itemID)
            {
                var query = new ItemTariffComponentQuery("a");
                var tariff = new TariffComponentQuery("b");
                var itq = new ItemTariffQuery("c");

                query.es.Top = 1;
                query.Select(
                    query.TariffComponentID,
                    query.Price,
                    query.IsAllowDiscount,
                    query.IsAllowVariable,
                    tariff.TariffComponentName,
                    tariff.IsTariffParamedic
                    );
                query.InnerJoin(tariff).On(query.TariffComponentID == tariff.TariffComponentID);
                query.InnerJoin(itq).On(
                    query.SRTariffType == itq.SRTariffType &&
                    query.ItemID == itq.ItemID &&
                    query.ClassID == itq.ClassID &&
                    query.StartingDate.Date() <= itq.StartingDate.Date()
                    );
                query.Where(
                    query.SRTariffType == tariffType,
                    query.ItemID == itemID,
                    query.ClassID == classID,
                    query.TariffComponentID == tariffComponentID,
                    query.StartingDate.Date() <= transactionDate
                    );
                query.OrderBy(
                    query.TariffComponentID.Ascending,
                    query.StartingDate.Descending
                    );

                return query.LoadDataTable();
            }

            public static DataTable GetItemTariffComponent(string tariffType, string itemId, string classId, DateTime startingDate, string tariffComponentId)
            {
                var query = new ItemTariffComponentQuery("a");
                var tariff = new TariffComponentQuery("b");

                query.es.Top = 1;
                query.Select(
                    query.TariffComponentID,
                    query.Price,
                    query.IsAllowDiscount,
                    query.IsAllowVariable,
                    tariff.TariffComponentName,
                    tariff.IsTariffParamedic
                    );
                query.InnerJoin(tariff).On(query.TariffComponentID == tariff.TariffComponentID);
                query.Where(
                    query.SRTariffType == tariffType,
                    query.ItemID == itemId,
                    query.ClassID == classId,
                    query.StartingDate.Date() == startingDate.Date,
                    query.TariffComponentID == tariffComponentId
                    );

                return query.LoadDataTable();
            }

            public static decimal GetItemTariff(string tariffType, DateTime prescriptionDate, string chargeClassID, string itemID, bool isCompound,
                string itemUnitOrEmbalaceID, string guarantorID, string registrationType)
            {
                var tariff = (GetItemTariff(prescriptionDate, tariffType, chargeClassID, chargeClassID, itemID, guarantorID, true, registrationType) ??
                              GetItemTariff(prescriptionDate, tariffType, AppSession.Parameter.DefaultTariffClass, chargeClassID, itemID, guarantorID, true, registrationType)) ??
                             (GetItemTariff(prescriptionDate, AppSession.Parameter.DefaultTariffType, chargeClassID, chargeClassID, itemID, guarantorID, true, registrationType) ??
                              GetItemTariff(prescriptionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, chargeClassID, itemID, guarantorID, true, registrationType));
                if (tariff != null)
                {
                    if (isCompound) return ((tariff.Price ?? 0) * Helper.GetConvertionFactor(itemID, itemUnitOrEmbalaceID));
                    return (tariff.Price ?? 0);
                }
                return 0;
            }

            public static decimal GetItemTariffNonMargin(string tariffType, DateTime prescriptionDate, string chargeClassID, string itemID,
                bool isCompound, string itemUnitOrEmbalaceID)
            {
                var tariff = GetItemTariffNonMargin(prescriptionDate, tariffType, chargeClassID, itemID);
                if (tariff == 0)
                    tariff = GetItemTariffNonMargin(prescriptionDate, tariffType, AppSession.Parameter.DefaultTariffClass, itemID);
                if (tariff == 0)
                    tariff = GetItemTariffNonMargin(prescriptionDate, AppSession.Parameter.DefaultTariffType, chargeClassID, itemID);
                if (tariff == 0)
                    tariff = GetItemTariffNonMargin(prescriptionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, itemID);

                if (tariff != 0 && isCompound)
                    return (tariff * Helper.GetConvertionFactor(itemID, itemUnitOrEmbalaceID));
                return tariff;
            }

            //private static decimal GetMargin(Decimal price, string marginID)
            //{
            //    decimal marginPercent = 0;
            //    var margin = new ItemProductMargin();
            //    if (margin.LoadByPrimaryKey(marginID))
            //    {
            //        var query = new ItemProductMarginValueQuery("a");
            //        query.Where(
            //            query.MarginID == marginID,
            //            string.Format("<{0} BETWEEN a.StartingValue AND a.EndingValue>", price)
            //            );

            //        var entity = new ItemProductMarginValue();
            //        if (entity.Load(query))
            //            marginPercent = entity.MarginPercentage ?? 0;
            //    }
            //    return marginPercent;
            //}

            private static decimal GetMargin(Decimal price, string marginID, string registrationType, string chargeClassID)
            {
                decimal marginPercent = 0;
                var margin = new ItemProductMargin();
                if (margin.LoadByPrimaryKey(marginID))
                {
                    var query = new ItemProductMarginValueQuery("a");
                    query.Where(
                        query.MarginID == marginID,
                        string.Format("<{0} BETWEEN a.StartingValue AND a.EndingValue>", price)
                        );

                    var entity = new ItemProductMarginValue();
                    if (entity.Load(query))
                    {
                        if ((entity.MarginPercentage ?? 0) > 0)
                            marginPercent = entity.MarginPercentage ?? 0;
                        else
                        {
                            switch (registrationType)
                            {
                                case AppConstant.RegistrationType.InPatient:
                                    // cek setting global dl, kl > 0 ambil setting global, kl = 0 ambil setting kelas
                                    marginPercent = entity.InpatientMarginPercentage ?? 0;
                                    if (marginPercent == 0)
                                    {
                                        var cls = new ItemProductMarginClassValue();
                                        cls.Query.Where(cls.Query.MarginID == marginID, cls.Query.SequenceNo == entity.SequenceNo, cls.Query.ClassID == chargeClassID);
                                        if (cls.Query.Load()) marginPercent = cls.MarginValuePercentage ?? 0;
                                    }
                                    //// dahulukan setting per kelas lalu setting globalnya
                                    //var cls = new ItemProductMarginClassValue();
                                    //cls.Query.Where(cls.Query.MarginID == marginID, cls.Query.SequenceNo == entity.SequenceNo, cls.Query.ClassID == chargeClassID);
                                    //if (cls.Query.Load()) marginPercent = cls.MarginValuePercentage ?? 0;
                                    //else marginPercent = entity.InpatientMarginPercentage ?? 0;
                                    break;
                                case AppConstant.RegistrationType.MedicalCheckUp:
                                case AppConstant.RegistrationType.OutPatient:
                                    marginPercent = entity.OutpatientMarginPercentage ?? 0;
                                    break;
                                case AppConstant.RegistrationType.EmergencyPatient:
                                    marginPercent = entity.EmergencyMarginPercentage ?? 0;
                                    break;
                                default: //OTC
                                    marginPercent = entity.OTCMarginPercentage ?? 0;
                                    break;
                            }
                        }
                    }
                }
                return marginPercent;
            }

            private static decimal GetPriceVAT(Decimal price, string marginID, string registrationType)
            {
                decimal priceVAT = 0;

                var margin = new ItemProductMargin();
                if (margin.LoadByPrimaryKey(marginID))
                {
                    var query = new ItemProductMarginValueQuery("a");
                    query.Where(
                        query.MarginID == marginID,
                        string.Format("<{0} BETWEEN a.StartingValue AND a.EndingValue>", price)
                        );

                    var entity = new ItemProductMarginValue();
                    if (entity.Load(query))
                    {
                        double tax = 1 + (AppSession.Parameter.Ppn / 100.00);

                        if ((entity.MarginPercentage ?? 0) > 0)
                            priceVAT = (entity.IsGlobalWithoutVAT ?? false) ? (price / Convert.ToDecimal(tax)) : price;
                        else
                        {
                            switch (registrationType)
                            {
                                case AppConstant.RegistrationType.InPatient:
                                    priceVAT = (entity.IsIpWithoutVAT ?? false) ? (price / Convert.ToDecimal(tax)) : price;
                                    break;
                                case AppConstant.RegistrationType.MedicalCheckUp:
                                case AppConstant.RegistrationType.OutPatient:
                                    priceVAT = (entity.IsOpWithoutVAT ?? false) ? (price / Convert.ToDecimal(tax)) : price;
                                    break;
                                case AppConstant.RegistrationType.EmergencyPatient:
                                    priceVAT = (entity.IsEmWithoutVAT ?? false) ? (price / Convert.ToDecimal(tax)) : price;
                                    break;
                                default: //OTC
                                    priceVAT = (entity.IsOtcWithoutVAT ?? false) ? (price / Convert.ToDecimal(tax)) : price;
                                    break;
                            }
                        }
                    }
                    else
                        priceVAT = price;
                }
                else
                    priceVAT = price;

                return priceVAT;
            }

            public static decimal GetItemConditionRuleTariff(Decimal price, string itemConditionRuleId, DateTime transactionDate)
            {
                if (string.IsNullOrEmpty(itemConditionRuleId)) return price;

                var query = new ItemConditionRuleQuery();
                query.es.Top = 1;
                query.Where(
                    query.ItemConditionRuleID == itemConditionRuleId,
                    query.StartingDate.Date() <= transactionDate,
                    query.EndingDate.Date() >= transactionDate
                     );
                query.OrderBy(query.StartingDate, esOrderByDirection.Descending);

                var rule = new ItemConditionRule();
                if (rule.Load(query))
                {
                    bool isValueInPercent = rule.IsValueInPercent ?? false;
                    decimal amountValue = rule.AmountValue ?? 0;

                    if (isValueInPercent)
                        amountValue = price * amountValue / 100;

                    if (rule.SRItemConditionRuleType == "MRG")
                    {
                        price += amountValue;
                    }
                    else if (rule.SRItemConditionRuleType == "DISC")
                    {
                        if (price >= amountValue)
                            price -= amountValue;
                        else
                            price = 0;
                    }
                }

                return price;
            }
        }
    }
}
