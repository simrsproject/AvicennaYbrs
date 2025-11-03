using System;
using System.Linq;
using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemTransactionItem
    {
        #region Get Function
        public decimal GetCountBudgetPlan(string ServiceUnitID, string toServiceUnitID, string ItemID, int year, string transactionNoException)
        {
            var qtiColl = new ItemTransactionItemCollection();
            var qti = new ItemTransactionItemQuery("a");
            var qt = new ItemTransactionQuery("b");

            qti.InnerJoin(qt).On(qti.TransactionNo == qt.TransactionNo);
            qti.Where(qt.TransactionCode == BusinessObject.Reference.TransactionCode.BudgetPlanApproval,
                qt.IsVoid == false,
                qti.ItemID == ItemID, qt.TransactionNo != transactionNoException);
            qti.Where(string.Format("<YEAR(b.TransactionDate) = {0}>", year));
            qti.Where(qt.IsApproved == true);

            qti.Where(qt.FromServiceUnitID == ServiceUnitID, qt.ToServiceUnitID == toServiceUnitID);
            qti.Select(qti.Quantity, qti.ConversionFactor);

            qtiColl.Load(qti);

            return qtiColl.Sum(x => (x.Quantity * x.ConversionFactor)) ?? 0;
        }

        public decimal GetCountBudgetPlanRealization(string FromServiceUnitID, string ToServiceUnitID, string ItemID, int year, string transactionNoException, bool IsApprovedOnly)
        {
            var qtiColl = new ItemTransactionItemCollection();
            var qti = new ItemTransactionItemQuery("a");
            var qt = new ItemTransactionQuery("b");

            qti.InnerJoin(qt).On(qti.TransactionNo == qt.TransactionNo);
            qti.Where(qt.TransactionCode == BusinessObject.Reference.TransactionCode.Distribution,
                qt.IsVoid == false,
                qti.ItemID == ItemID, qt.TransactionNo != transactionNoException);
            qti.Where(string.Format("<YEAR(b.TransactionDate) = {0}>", year));
            if (true) //(IsApprovedOnly)
            {
                qti.Where(qt.IsApproved == true);
            }
            qti.Where(qt.FromServiceUnitID == FromServiceUnitID, qt.ToServiceUnitID == ToServiceUnitID);
            qti.Select(qti.Quantity, qti.ConversionFactor);

            qtiColl.Load(qti);

            var qty = qtiColl.Sum(x => (x.Quantity * x.ConversionFactor)) ?? 0;

            // dikurangi yang balik
            var qtiColl2 = new ItemTransactionItemCollection();
            var qti2 = new ItemTransactionItemQuery("a");
            var qt2 = new ItemTransactionQuery("b");

            qti2.InnerJoin(qt2).On(qti2.TransactionNo == qt2.TransactionNo);
            qti2.Where(qt2.TransactionCode == BusinessObject.Reference.TransactionCode.Distribution,
                qt2.IsVoid == false,
                qti2.ItemID == ItemID, qt2.TransactionNo != transactionNoException);
            qti2.Where(string.Format("<YEAR(b.TransactionDate) = {0}>", year));
            if (true) //(IsApprovedOnly)
            {
                qti2.Where(qt.IsApproved == true);
            }
            qti2.Where(qt2.FromServiceUnitID == ToServiceUnitID, qt2.ToServiceUnitID == FromServiceUnitID);
            qti2.Select(qti2.Quantity, qti2.ConversionFactor);

            qtiColl2.Load(qti2);

            qty = qty - qtiColl2.Sum(x => (x.Quantity * x.ConversionFactor)) ?? 0;

            //// gabung dengan penerimaan untuk item non inventory
            //var qtiColl3 = new ItemTransactionItemCollection();
            //var qti3 = new ItemTransactionItemQuery("a");
            //var qt3 = new ItemTransactionQuery("b");
            //var itmnm = new ItemProductNonMedicQuery("c");

            //qti3.InnerJoin(qt3).On(qti3.TransactionNo == qt3.TransactionNo);
            //qti3.InnerJoin(itmnm).On(qti3.ItemID == itmnm.ItemID);
            //qti3.Where(qt3.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseOrderReceive,
            //    qt3.IsVoid == false,
            //    qti3.ItemID == ItemID,
            //    qt3.TransactionNo != transactionNoException,
            //    itmnm.IsInventoryItem == false);
            //qti3.Where(string.Format("<YEAR(b.TransactionDate) = {0}>", year));
            //if (true) //(IsApprovedOnly)
            //{
            //    qti3.Where(qt.IsApproved == true);
            //}
            //qti3.Where(qt3.FromServiceUnitID == ToServiceUnitID, qt3.ToServiceUnitID == FromServiceUnitID);
            //qti3.Select(qti3.Quantity, qti3.ConversionFactor);

            //qtiColl3.Load(qti3);
            //qty = qty + qtiColl3.Sum(x => (x.Quantity * x.ConversionFactor)) ?? 0;

            // gabung dengan permintaan pembelian untuk item non inventory
            // cara ini belum bisa dipakai karena ada PR yang pas PO beda peruntukannya

            //var qtiColl3 = new ItemTransactionItemCollection();
            var qti3 = new ItemTransactionItemQuery("a");
            var qt3 = new ItemTransactionQuery("b");
            var itmnm = new ItemProductNonMedicQuery("c");

            qti3.InnerJoin(qt3).On(qti3.TransactionNo == qt3.TransactionNo);
            qti3.InnerJoin(itmnm).On(qti3.ItemID == itmnm.ItemID);
            qti3.Where(qt3.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseRequest,
                qt3.IsVoid == false,
                qti3.ItemID == ItemID,
                qt3.TransactionNo != transactionNoException,
                itmnm.IsInventoryItem == false);
            qti3.Where(string.Format("<YEAR(b.TransactionDate) = {0}>", year));
            qti3.Select(
                (qti3.Quantity * qti3.ConversionFactor).Sum().As("Quantity"),
                qti3.TransactionNo,
                qti3.ItemID,
                qti3.IsClosed
            );
            qti3.GroupBy(qti3.TransactionNo,
                qti3.ItemID,
                qti3.IsClosed);
            if (true) //(IsApprovedOnly)
            {
                qti3.Where(qt.IsApproved == true);
            }
            //qti3.Where(qt3.ServiceUnitCostID == ToServiceUnitID, qt3.ToServiceUnitID == FromServiceUnitID);
            qti3.Where(qt3.FromServiceUnitID == ToServiceUnitID, qt3.ToServiceUnitID == FromServiceUnitID);

            //qtiColl3.Load(qti3);
            var dtPR = qti3.LoadDataTable();

            // jika PO sudah close maka cek jumlah penerimaannya
            // jika PO blm close maka pakai jumlah yg dibikin PO

            decimal qtyNonInv = (from p in dtPR.AsEnumerable() select p.Field<decimal>("Quantity")).Sum();

            decimal qtyCanceled = 0;
            foreach (System.Data.DataRow i in dtPR.Rows)
            {
                decimal prApproved = System.Convert.ToDecimal(i["Quantity"]);

                var qtiPO = new ItemTransactionItemQuery("a");
                var qtPO = new ItemTransactionQuery("b");
                qtiPO.InnerJoin(qtPO).On(qtiPO.TransactionNo == qtPO.TransactionNo)
                    .Where(
                        qt3.ReferenceNo == i["TransactionNo"].ToString(),
                        qt3.IsVoid == false,
                        qti3.ItemID == i["ItemID"].ToString()
                    ).Select(
                        qtiPO.TransactionNo,
                        (qtiPO.Quantity * qtiPO.ConversionFactor).Sum().As("Quantity"),
                        qtPO.IsClosed
                    );
                qtiPO.GroupBy(qtiPO.TransactionNo, qtPO.IsClosed);
                var dtPO = qtiPO.LoadDataTable();

                decimal prCanceled = 0;
                decimal tPoApproved = 0;

                if (((bool)(i["IsClosed"] ?? false)) && (dtPO.Rows.Count == 0))
                {
                    // jika PR close tapi tidak ada PO maka canceled
                    prCanceled = prApproved;
                }
                else
                {
                    foreach (System.Data.DataRow rowPO in dtPO.Rows)
                    {
                        // jika sudah sampai PO maka pakai jumlah yang ada di PO
                        var poApproved = System.Convert.ToDecimal(rowPO["Quantity"]);

                        if ((bool)rowPO["IsClosed"])
                        {
                            // cari penerimaannya 
                            var qtiRC = new ItemTransactionItemQuery("a");
                            var qtRC = new ItemTransactionQuery("b");
                            qtiRC.InnerJoin(qtRC).On(qtiRC.TransactionNo == qtRC.TransactionNo)
                                .Where(
                                    qtRC.ReferenceNo == rowPO["TransactionNo"].ToString(),
                                    qtRC.IsVoid == false,
                                    qtiRC.ItemID == i["ItemID"].ToString()
                                ).Select(
                                    (qtiRC.Quantity * qtiRC.ConversionFactor).Sum().As("Quantity"),
                                    qtRC.IsClosed
                                );
                            qtiRC.GroupBy(qtRC.IsClosed);
                            var dtRC = qtiRC.LoadDataTable();

                            decimal rcApproved = 0;
                            foreach (System.Data.DataRow rowRC in dtRC.Rows)
                            {
                                rcApproved = rcApproved + System.Convert.ToDecimal(rowRC["Quantity"]);
                            }

                            poApproved = rcApproved;
                        }

                        tPoApproved = tPoApproved + poApproved;
                        prCanceled = prApproved - tPoApproved;
                    }
                }

                qtyCanceled = qtyCanceled + prCanceled;
            }

            qty = qty + qtyNonInv - qtyCanceled;

            // ditambah item non inventory yang langsung dari po tanpa PR
            var qti4 = new ItemTransactionItemQuery("a");
            var qt4 = new ItemTransactionQuery("b");
            var itmnm4 = new ItemProductNonMedicQuery("c");
            var qtiPO4 = new ItemTransactionItemQuery("d");
            var qtPO4 = new ItemTransactionQuery("e");

            qti4.InnerJoin(qt4).On(qti4.TransactionNo == qt4.TransactionNo);
            qti4.InnerJoin(qtiPO4).On(qti4.ReferenceNo == qtiPO4.TransactionNo &&
                qti4.ReferenceSequenceNo == qtiPO4.SequenceNo && qtiPO4.ReferenceNo == string.Empty);
            qti4.InnerJoin(qtPO4).On(qtiPO4.TransactionNo == qtPO4.TransactionNo);
            qti4.InnerJoin(itmnm).On(qti4.ItemID == itmnm.ItemID);
            qti4.Where(qt4.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseOrderReceive,
                qt4.IsVoid == false,
                qti4.ItemID == ItemID,
                qt4.TransactionNo != transactionNoException,
                itmnm.IsInventoryItem == false);
            qti4.Where(string.Format("<YEAR(e.TransactionDate) = {0}>", year));
            qti4.Select(
                (qti4.Quantity * qti4.ConversionFactor).Sum().As("Quantity"),
                qti4.TransactionNo,
                qti4.ItemID,
                qti4.IsClosed
            );
            qti4.GroupBy(qti4.TransactionNo,
                qti4.ItemID,
                qti4.IsClosed);
            if (true) //(IsApprovedOnly)
            {
                qti4.Where(qt.IsApproved == true);
            }
            //qti4.Where(qt4.ServiceUnitCostID == ToServiceUnitID, qt4.ToServiceUnitID == FromServiceUnitID);
            qti4.Where(qt4.FromServiceUnitID == ToServiceUnitID, qt4.ToServiceUnitID == FromServiceUnitID);

            //qtiColl3.Load(qti3);
            var dt4 = qti4.LoadDataTable();

            decimal qtyNonInv4 = (from p in dt4.AsEnumerable() select p.Field<decimal>("Quantity")).Sum();
            // end of item non inventory yang langsung dari po tanpa PR

            qty += qtyNonInv4;

            // ditambah item produksi dari work order
            var par = new AppParameter();
            par.LoadByPrimaryKey("WorkTypeProject");

            var awo = new AssetWorkOrderQuery("a");
            itmnm = new ItemProductNonMedicQuery("b");
            awo.InnerJoin(itmnm).On(awo.ItemID == itmnm.ItemID);
            awo.Where(string.Format("<YEAR(a.LastRealizationDateTime) = {0}>", year));
            awo.Where(awo.IsProceed == true, awo.ItemID == ItemID, awo.FromServiceUnitID == ToServiceUnitID, awo.SRWorkType == par.ParameterValue);
            awo.Select(awo.ItemID, awo.Qty.Sum().As("Quantity"));
            awo.GroupBy(awo.ItemID);
            var dtawo = awo.LoadDataTable();

            decimal qtyawo = dtawo.Rows.Cast<DataRow>().Sum(row => Convert.ToDecimal(row["Quantity"]));
            qty += qtyawo;

            // dikurang realisasi material used non inventory dari item service work order
            var awoi = new AssetWorkOrderItemQuery("a");
            awo = new AssetWorkOrderQuery("b");
            itmnm = new ItemProductNonMedicQuery("c");
            awoi.InnerJoin(awo).On(awo.OrderNo == awoi.OrderNo);
            awoi.InnerJoin(itmnm).On(itmnm.ItemID == awoi.ItemID);
            awoi.Where(string.Format("<YEAR(b.LastRealizationDateTime) = {0}>", year));
            awoi.Where(awo.IsProceed == true, awoi.ItemID == ItemID, awo.ToServiceUnitID == ToServiceUnitID,
                      awo.SRWorkType != par.ParameterValue, itmnm.IsInventoryItem == 0);
            awoi.Select(awoi.ItemID, @"<SUM(a.Quantity - a.QuantityRealization) AS Quantity>");
            awoi.GroupBy(awoi.ItemID);
            DataTable dtawoi = awoi.LoadDataTable();
            decimal qtyawoi = dtawoi.Rows.Cast<DataRow>().Sum(row => Convert.ToDecimal(row["Quantity"]));

            qty = qty - qtyawoi;

            return qty;
        }

        public bool SetQtyPricePO(string TransactionNoPR, string ItemID, 
            decimal newConversionFactor, string SupplierID, bool IsUpdateQty) {

            if (string.IsNullOrEmpty(TransactionNoPR)) return false;    
            if (string.IsNullOrEmpty(ItemID)) return false;
                this.ItemID = ItemID;

            var prColl = new ItemTransactionItemCollection();
            prColl.Query.Where(
                prColl.Query.TransactionNo == TransactionNoPR,
                prColl.Query.ItemID == ItemID);
            if (prColl.Query.Load())
            {
                var pr = prColl.First();
                var prQty = pr.Quantity - (pr.QuantityFinishInBaseUnit / pr.ConversionFactor);
                var prSrItem = pr.SRItemUnit;
                var prCf = pr.ConversionFactor;

                if (IsUpdateQty)
                    this.Quantity = prQty * prCf / newConversionFactor;

                var item = new Item();
                item.LoadByPrimaryKey(this.ItemID);
                if (item.SRItemType == "11") {
                    var ipm = new ItemProductMedic();
                    ipm.LoadByPrimaryKey(this.ItemID);
                    ipm.SetSupplierPriceInBaseUnit(SupplierID);
                    this.Price = ipm.PriceInBaseUnit * newConversionFactor;
                    this.Discount1Percentage = ipm.PurchaseDiscount1;
                    this.Discount2Percentage = ipm.PurchaseDiscount2;
                }
                else if (item.SRItemType == "21") {
                    var ipm = new ItemProductNonMedic();
                    ipm.LoadByPrimaryKey(this.ItemID);
                    ipm.SetSupplierPriceInBaseUnit(SupplierID);
                    this.Price = ipm.PriceInBaseUnit * newConversionFactor;
                    this.Discount1Percentage = ipm.PurchaseDiscount1;
                    this.Discount2Percentage = ipm.PurchaseDiscount2;
                } else if (item.SRItemType == "81")
                {
                    var ipm = new ItemKitchen();
                    ipm.LoadByPrimaryKey(this.ItemID);
                    ipm.SetSupplierPriceInBaseUnit(SupplierID);
                    this.Price = ipm.PriceInBaseUnit * newConversionFactor;
                    this.Discount1Percentage = ipm.PurchaseDiscount1;
                    this.Discount2Percentage = ipm.PurchaseDiscount2;
                }
                return true;
            }else{
                return false;
            }
        }

        #endregion


        public string ItemName
        {
            // Penamaaan Column jangan sama dgn nama propertiesnya itu akan mengakibatkan error 'System.OutOfMemoryException'
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }

        public string ProductAccountId
        {
            get { return GetColumn("refToItem_ProductAccountId").ToString(); }
            set { SetColumn("refToItem_ProductAccountId", value); }
        }

        public decimal? Balance
        {
            get
            {
                try
                {
                    return (decimal?)GetColumn("refToItemBalance_Balance");
                }
                catch
                {
                    return (decimal?)0;
                }
            }
            set { SetColumn("refToItemBalance_Balance", value); }
        }
        public decimal? Booking
        {
            get
            {
                try
                {
                    return (decimal?)GetColumn("refToItemBalance_Booking");
                }
                catch
                {
                    return (decimal?)0;
                }
            }
            set { SetColumn("refToItemBalance_Booking", value); }
        }
        public decimal? BalanceSG
        {
            get
            {
                try
                {
                    return (decimal?)GetColumn("refToItemBalance_BalanceSG");
                }
                catch
                {
                    return (decimal?)0;
                }
            }
            set { SetColumn("refToItemBalance_BalanceSG", value); }
        }
        public decimal? BalanceTotal
        {
            get
            {
                try
                {
                    return (decimal?)GetColumn("refToItemBalance_BalanceTotal");
                }
                catch
                {
                    return (decimal?)0;
                }
            }
            set { SetColumn("refToItemBalance_BalanceTotal", value); }
        }

        public decimal? Balance2
        {
            get
            {
                try
                {
                    return (decimal?)GetColumn("refToItemBalance_Balance2");
                }
                catch
                {
                    return (decimal?)0;
                }
            }
            set { SetColumn("refToItemBalance_Balance2", value); }
        }

        public decimal? Minimum
        {
            get
            {
                try
                {
                    return (decimal?)GetColumn("refToItemBalance_Minimum");
                }
                catch
                {
                    return (decimal?)0;
                }
            }
            set { SetColumn("refToItemBalance_Minimum", value); }
        }

        public decimal? Maximum
        {
            get {
                try
                {
                    return (decimal?)GetColumn("refToItemBalance_Maximum");
                }
                catch
                {
                    return (decimal?)0;
                }
            }
            set { SetColumn("refToItemBalance_Maximum", value); }
        }

        public string SRItemBinName
        {
            get { return GetColumn("refToSRI_ItemBin").ToString(); }
            set { SetColumn("refToSRI_ItemBin", value); }
        }

        public string ItemIDExternal
        {
            get { return GetColumn("refToItemIDExternal").ToString(); }
            set { SetColumn("refToItemIDExternal", value); }
        }

        public decimal? Total
        {
            get { return (decimal?)GetColumn("refTo_Total"); }
            set { SetColumn("refTo_Total", value); }
        }

        public bool IsControlExpired
        {
            get { return (bool)GetColumn("refToItemProduct_IsControlExpired"); }
            set { SetColumn("refToItemProduct_IsControlExpired", value); }
        }

        public string DrugDistributionLicenseNo
        {
            get { return GetColumn("refToItemProductMedic_DrugDistributionLicenseNo").ToString(); }
            set { SetColumn("refToItemProductMedic_DrugDistributionLicenseNo", value); }
        }
        public string SRMasterBaseUnit
        {
            // Penamaaan Column jangan sama dgn nama propertiesnya itu akan mengakibatkan error 'System.OutOfMemoryException'
            get { return GetColumn("refToItemProduct_SRItemUnit").ToString(); }
            set { SetColumn("refToItemProduct_SRItemUnit", value); }
        }
        public string SRMasterPurchaseUnit
        {
            get { return GetColumn("refToItemProduct_PurchaseUnit").ToString(); }
            set { SetColumn("refToItemProduct_PurchaseUnit", value); }
        }
        public string Barcode
        {
            get { return GetColumn("refToItem_Barcode").ToString(); }
            set { SetColumn("refToItem_Barcode", value); }
        }

        public string AdditionalInfo
        {
            get { return GetColumn("refToAdditionalInfo").ToString(); }
            set { SetColumn("refToAdditionalInfo", value); }
        }

        public bool IsNotCompleteED
        {
            get { return (bool)GetColumn("refToItemProduct_IsNotCompleteED"); }
            set { SetColumn("refToItemProduct_IsNotCompleteED", value); }
        }

        public string FabricName
        {
            get { return GetColumn("refToFabric_FabricName").ToString(); }
            set { SetColumn("refToFabric_FabricName", value); }
        }

        public bool IsAsset
        {
            get { return (bool)GetColumn("refToItem_IsAsset"); }
            set { SetColumn("refToItem_IsAsset", value); }
        }

        public int? EconomicLifeInYear
        {
            get
            {
                try
                {
                    return (int?)GetColumn("refToItem_EconomicLifeInYear");
                }
                catch
                {
                    return (int?)0;
                }
            }
            set { SetColumn("refToItem_EconomicLifeInYear", value); }
        }
    }
}
