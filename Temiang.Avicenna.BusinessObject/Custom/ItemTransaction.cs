using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemTransaction
    {
        public override void Save()
        {
            // Insert ke approval level
            var isReInsert = false;
            if ((this.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseOrder && AppParameter.IsYes(AppParameter.ParameterItem.IsUseApprovalLevel))
                || (this.TransactionCode == BusinessObject.Reference.TransactionCode.Distribution && AppParameter.IsYes(AppParameter.ParameterItem.IsDistributionUseApprovalLevel)))
            {
                if (
                    (TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseOrder && this.es.IsModified && this.ChargesAmount != Convert.ToDecimal(this.GetOriginalColumnValue("ChargesAmount")))
                    || (TransactionCode == BusinessObject.Reference.TransactionCode.Distribution && this.es.IsModified && this.ItemGroupID != Convert.ToString(this.GetOriginalColumnValue("ItemGroupID")))
                    || this.es.IsDeleted)
                {
                    var atc = new ApprovalTransactionCollection();
                    atc.Query.Where(atc.Query.TransactionNo == this.TransactionNo);
                    atc.LoadAll();
                    foreach (ApprovalTransaction at in atc)
                    {
                        if (at.IsApproved == true)
                        {
                            throw new esException("Approval level in progress, can't edit or delete");
                        }
                    }

                    atc.MarkAllAsDeleted();
                    atc.Save();
                    isReInsert = true;
                }
                if ((this.es.IsModified && isReInsert) || this.es.IsAdded)
                {
                    PopulateApprovalTransaction(TransactionCode, SRItemType, ItemGroupID, this.ChargesAmount ?? 0);
                }
            }

            base.Save();
        }

        private void PopulateApprovalTransaction(string transactionCode, string itemType, string itemGroupID, decimal chargesAmount)
        {
            if (string.IsNullOrEmpty(itemGroupID))
                itemGroupID = string.Empty;

            var arq = new ApprovalRangeQuery();
            arq.Where(arq.TransactionCode == transactionCode, arq.SRItemType == itemType, arq.ItemGroupID == itemGroupID, arq.AmountFrom <= chargesAmount);
            arq.OrderBy(arq.AmountFrom.Descending);
            arq.es.Top = 1;

            var ar = new ApprovalRange();
            if (!ar.Load(arq))
            {
                if (transactionCode == BusinessObject.Reference.TransactionCode.Distribution)
                {
                    throw new esException("Please define first approval user for this Distribution or contact IT Department");
                }

                throw new esException("Please define first approval user for this amount or contact IT Department");
            }

            PopulateApprovalTransaction(ar.TransactionCode, ar.ApprovalRangeID, ar.ApprovalLevelFinal ?? 1);
        }

        private void PopulateApprovalTransaction(string transactionCode, string approvalRangeID, int approvalLevelFinal)
        {
            var aruc = new ApprovalRangeUserCollection();
            var aruq = new ApprovalRangeUserQuery();
            aruq.Where(aruq.ApprovalRangeID == approvalRangeID);
            aruq.OrderBy(aruq.ApprovalLevel.Ascending);
            aruc.Load(aruq);

            if (aruc == null || aruc.Count < 1)
                throw new esException("Please define first approval user for this approval range ID [" + approvalRangeID + "] or contact IT Department");

            foreach (ApprovalRangeUser user in aruc)
            {
                if (user.ApprovalLevel > approvalLevelFinal)
                    break;

                var at = new ApprovalTransaction
                {
                    TransactionNo = TransactionNo,
                    ApprovalRangeID = user.ApprovalRangeID,
                    ApprovalLevel = user.ApprovalLevel,
                    UserID = user.UserID,
                    IsApprovalLevelFinal = (user.ApprovalLevel == approvalLevelFinal),
                    IsApproved = false,
                    TransactionCode = transactionCode
                };
                at.Save();
            }
        }

        #region Void/Unvoid

        private static void VoidProcess(ItemTransaction entity, string userID, bool isVoid, string inventoryIssueUsingRequest = null)
        {
            if (entity.IsVoid == true && isVoid) return;
            if (entity.IsVoid == false && !isVoid) return;

            entity.IsVoid = isVoid;
            entity.VoidDate = Utils.NowAtSqlServer(); //DateTime.Now;
            entity.VoidByUserID = userID;

            using (var trans = new esTransactionScope())
            {
                entity.Save();

                if (isVoid)
                {
                    switch (entity.TransactionCode)
                    {
                        case BusinessObject.Reference.TransactionCode.InventoryIssueOut:
                            //if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIssueUsingRequest).ToLower() == "yes")
                            //db:20231111 - Obsolete, baca dari parameter list yg dikirim di form InventoryIssueDetail.aspx.cs
                            if (!string.IsNullOrWhiteSpace(inventoryIssueUsingRequest) && inventoryIssueUsingRequest == "y")
                            {
                                var e2 = new ItemTransaction();
                                e2.Query.Where(e2.Query.ReferenceNo == entity.TransactionNo);
                                if (e2.Load(e2.Query))
                                {
                                    //e2.str.ReferenceDate = string.Empty;
                                    e2.ReferenceNo = string.Empty;
                                    e2.Save();
                                }
                            }
                            break;

                        case BusinessObject.Reference.TransactionCode.PurchaseOrder:
                            if (entity.IsBySystem == true)
                            {
                                var itiColl = new ItemTransactionItemCollection();
                                itiColl.Query.Where(itiColl.Query.TransactionNo == entity.TransactionNo);
                                itiColl.LoadAll();
                                foreach (var iti in itiColl)
                                {
                                    var x = new ItemTransactionItem();
                                    if (x.LoadByPrimaryKey(iti.ReferenceNo, iti.ReferenceSequenceNo))
                                    {
                                        x.QuantityFinishInBaseUnit -= (iti.Quantity * iti.ConversionFactor);
                                        x.IsClosed = false;
                                        x.Save();
                                    }
                                }
                            }
                            break;

                        case BusinessObject.Reference.TransactionCode.PurchaseOrderReceive:
                            var porDt = new ItemTransactionItemCollection();
                            porDt.Query.Where(porDt.Query.TransactionNo == entity.TransactionNo);
                            porDt.LoadAll();
                            foreach (var iti in porDt)
                            {
                                var x = new ItemTransactionItem();
                                if (x.LoadByPrimaryKey(iti.ReferenceNo, iti.ReferenceSequenceNo))
                                {
                                    x.QuantityFinishInBaseUnit -= (iti.Quantity * iti.ConversionFactor);
                                    if (x.QuantityFinishInBaseUnit < 0)
                                        x.QuantityFinishInBaseUnit = 0;
                                    x.IsClosed = false;
                                    x.Save();
                                }
                            }

                            break;
                    }

                    //if (entity.TransactionCode == BusinessObject.Reference.TransactionCode.InventoryIssueOut && AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIssueUsingRequest).ToLower() == "yes")
                    //{
                    //    var e2 = new ItemTransaction();
                    //    e2.Query.Where(e2.Query.ReferenceNo == entity.TransactionNo);
                    //    if (e2.Load(e2.Query))
                    //    {
                    //        //e2.str.ReferenceDate = string.Empty;
                    //        e2.ReferenceNo = string.Empty;
                    //        e2.Save();
                    //    }
                    //}
                    //else if (entity.TransactionCode == Reference.TransactionCode.PurchaseOrder && entity.IsBySystem == true)
                    //{
                    //    var itiColl = new ItemTransactionItemCollection();
                    //    itiColl.Query.Where(itiColl.Query.TransactionNo == entity.TransactionNo);
                    //    itiColl.LoadAll();
                    //    foreach (var iti in itiColl)
                    //    {
                    //        var x = new ItemTransactionItem();
                    //        if (x.LoadByPrimaryKey(iti.ReferenceNo, iti.ReferenceSequenceNo))
                    //        {
                    //            x.QuantityFinishInBaseUnit -= (iti.Quantity * iti.ConversionFactor);
                    //            x.IsClosed = false;
                    //            x.Save();
                    //        }
                    //    }
                    //}
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        public void Void(string transactionNo, string userID, string isInventoryIssueUsingRequest = null)
        {
            var entity = new ItemTransaction();
            entity.LoadByPrimaryKey(transactionNo);
            VoidProcess(entity, userID, true, isInventoryIssueUsingRequest);
        }

        public void UnVoid(string transactionNo, string userID)
        {
            var entity = new ItemTransaction();
            entity.LoadByPrimaryKey(transactionNo);
            VoidProcess(entity, userID, false);
        }

        #endregion

        #region Approve/UnApprove

        public string Approve(string transactionNo, ItemTransactionItemCollection coll, string userID, decimal rounding)
        {
            var entity = new ItemTransaction();
            entity.LoadByPrimaryKey(transactionNo);
            return ApproveProcess(entity, coll, userID, true, rounding);
        }

        public string Approve(string transactionNo, ItemTransactionItemCollection coll, string userID)
        {
            var entity = new ItemTransaction();
            entity.LoadByPrimaryKey(transactionNo);
            return ApproveProcess(entity, coll, userID, true, 0);
        }

        public string UnApprove(string transactionNo, ItemTransactionItemCollection coll, string userID)
        {
            var entity = new ItemTransaction();
            entity.LoadByPrimaryKey(transactionNo);
            return ApproveProcess(entity, coll, userID, false, 0);
        }

        //public static string CekBudgetPlan(ItemTransaction entity, ItemTransactionItemCollection coll)
        //{
        //    var str = string.Empty;
        //    if (AppParameter.IsYes(AppParameter.ParameterItem.IsDistReqOrPurcReqUsingBudgetPlan))
        //    {
        //        if (IsNeedCheckingBudgetPlan(entity.TransactionCode))
        //        {
        //            if (entity.SRItemType == BusinessObject.Reference.ItemType.Medical) {
        //                if (!AppParameter.IsYes(AppParameter.ParameterItem.IsBudgetingMedical)) {
        //                    return string.Empty;
        //                }

        //                // cek budget
        //            }
        //            else if (entity.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
        //            {
        //                if (!AppParameter.IsYes(AppParameter.ParameterItem.IsBudgetingNonMedical))
        //                {
        //                    return string.Empty;
        //                }

        //                // cek budget
        //            }
        //            else if (entity.SRItemType == BusinessObject.Reference.ItemType.Kitchen)
        //            {
        //                if (!AppParameter.IsYes(AppParameter.ParameterItem.IsBudgetingKitchen))
        //                {
        //                    return string.Empty;
        //                }

        //                // cek budget
        //            }
        //            else {
        //                return string.Empty;
        //            }

        //            if (entity.TransactionCode == Reference.TransactionCode.DistributionRequest) { 

        //            }

        //            //khusus RSCH, u/ permintaan distribusi dari bengkel smp akhir tahun 2015, belum liat budget plan 
        //            //bengkel2 mana yg bablas, disetting di ServiceUnitTransactionCode --> IsItemProductNonMedic = 1 
        //            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSCH")
        //            {
        //                if (entity.TransactionCode == Reference.TransactionCode.DistributionRequest)
        //                {
        //                    var tcode = new ServiceUnitTransactionCodeCollection();
        //                    tcode.Query.Where(tcode.Query.ServiceUnitID == entity.FromServiceUnitID,
        //                                      tcode.Query.SRTransactionCode ==
        //                                      Reference.TransactionCode.AssetWorkOrderRealization,
        //                                      tcode.Query.IsItemProductNonMedic == true);
        //                    tcode.LoadAll();
        //                    if (tcode.Count == 1)
        //                    {
        //                        return str;
        //                    }
        //                }
        //                else if (entity.TransactionCode == Reference.TransactionCode.Distribution)
        //                {
        //                    var tcode = new ServiceUnitTransactionCodeCollection();
        //                    tcode.Query.Where(tcode.Query.ServiceUnitID == entity.ToServiceUnitID,
        //                                      tcode.Query.SRTransactionCode ==
        //                                      Reference.TransactionCode.AssetWorkOrderRealization,
        //                                      tcode.Query.IsItemProductNonMedic == true);
        //                    tcode.LoadAll();
        //                    if (tcode.Count == 1)
        //                    {
        //                        return str;
        //                    }
        //                }
        //            }
        //            //END OF --> khusus RSCH, u/ permintaan distribusi dari bengkel smp akhir tahun 2015, belum liat budget plan 

        //            // hanya item nonmedical yang perlu cek budget
        //            if (entity.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
        //            {
        //                var svcUnitBpFrom = string.Empty;
        //                var svcUnitBpTo = string.Empty;
        //                var svcUnit = new ServiceUnit();
        //                if (entity.TransactionCode == Reference.TransactionCode.Distribution)
        //                {
        //                    svcUnit.LoadByPrimaryKey(entity.FromServiceUnitID);
        //                    svcUnitBpFrom = entity.ToServiceUnitID;
        //                    svcUnitBpTo = entity.FromServiceUnitID;
        //                }
        //                else
        //                {
        //                    svcUnit.LoadByPrimaryKey(entity.ToServiceUnitID);
        //                    svcUnitBpFrom = entity.FromServiceUnitID;
        //                    svcUnitBpTo = entity.ToServiceUnitID;
        //                }

        //                appparam = new AppParameter();
        //                if (entity.TransactionCode == Reference.TransactionCode.PurchaseRequest)
        //                {
        //                    if (!appparam.LoadByPrimaryKey("MainPurchasingUnitIDForNonMedical")) return string.Empty;
        //                    if (svcUnit.ServiceUnitID != appparam.ParameterValue) return string.Empty;
        //                }
        //                else
        //                {
        //                    //if (!appparam.LoadByPrimaryKey("MainDistributionLocationIDForNonMedical")) return string.Empty;
        //                    //if (svcUnit.LocationID != appparam.ParameterValue) return string.Empty;

        //                    var tcsu = new ServiceUnitTransactionCodeQuery("a");
        //                    var su = new ServiceUnitQuery("b");
        //                    tcsu.InnerJoin(su).On(su.ServiceUnitID == tcsu.ServiceUnitID &&
        //                                          tcsu.SRTransactionCode == Reference.TransactionCode.BudgetPlanApproval);
        //                    DataTable dtb = tcsu.LoadDataTable();

        //                    if (dtb.Rows.Count == 0) return string.Empty;
        //                }

        //                foreach (var item in coll)
        //                {
        //                    var qtyBp = item.GetCountBudgetPlan(svcUnitBpFrom, svcUnitBpTo, item.ItemID,
        //                        entity.TransactionDate.Value.Year, entity.TransactionNo);

        //                    var qtyBpRe = item.GetCountBudgetPlanRealization(svcUnitBpFrom, svcUnitBpTo, item.ItemID,
        //                        entity.TransactionDate.Value.Year, entity.TransactionNo, true);

        //                    var balance = qtyBp - qtyBpRe;
        //                    if (item.Quantity * item.ConversionFactor > balance)
        //                    {
        //                        // lebih dari budget
        //                        str = (str == string.Empty)
        //                                  ? "Insufficient budget plan balance for item " + item.ItemID + "[bal: " +
        //                                    balance.ToString() + "]"
        //                                  : str + ", " + item.ItemID + "[bal: " + balance.ToString() + "]";
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return str;
        //}

        public static string CekBudgetPlan(ItemTransaction entity, ItemTransactionItemCollection coll)
        {
            var str = string.Empty;
            var appparam = new AppParameter();
            appparam.LoadByPrimaryKey("IsDistReqOrPurcReqUsingBudgetPlan");
            if (appparam.ParameterValue == "Yes")
            {
                if (IsNeedCheckingBudgetPlan(entity.TransactionCode))
                {
                    //khusus RSCH, u/ permintaan distribusi dari bengkel smp akhir tahun 2015, belum liat budget plan 
                    //bengkel2 mana yg bablas, disetting di ServiceUnitTransactionCode --> IsItemProductNonMedic = 1 
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSCH")
                    {
                        if (entity.TransactionCode == Reference.TransactionCode.DistributionRequest)
                        {
                            var tcode = new ServiceUnitTransactionCodeCollection();
                            tcode.Query.Where(tcode.Query.ServiceUnitID == entity.FromServiceUnitID,
                                              tcode.Query.SRTransactionCode ==
                                              Reference.TransactionCode.AssetWorkOrderRealization,
                                              tcode.Query.IsItemProductNonMedic == true);
                            tcode.LoadAll();
                            if (tcode.Count == 1)
                            {
                                return str;
                            }
                        }
                        else if (entity.TransactionCode == Reference.TransactionCode.Distribution)
                        {
                            var tcode = new ServiceUnitTransactionCodeCollection();
                            tcode.Query.Where(tcode.Query.ServiceUnitID == entity.ToServiceUnitID,
                                              tcode.Query.SRTransactionCode ==
                                              Reference.TransactionCode.AssetWorkOrderRealization,
                                              tcode.Query.IsItemProductNonMedic == true);
                            tcode.LoadAll();
                            if (tcode.Count == 1)
                            {
                                return str;
                            }
                        }
                    }
                    //END OF --> khusus RSCH, u/ permintaan distribusi dari bengkel smp akhir tahun 2015, belum liat budget plan 

                    // hanya item nonmedical yang perlu cek budget
                    if (entity.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
                    {
                        var svcUnitBpFrom = string.Empty;
                        var svcUnitBpTo = string.Empty;
                        var svcUnit = new ServiceUnit();
                        if (entity.TransactionCode == Reference.TransactionCode.Distribution)
                        {
                            svcUnit.LoadByPrimaryKey(entity.FromServiceUnitID);
                            svcUnitBpFrom = entity.ToServiceUnitID;
                            svcUnitBpTo = entity.FromServiceUnitID;
                        }
                        else
                        {
                            svcUnit.LoadByPrimaryKey(entity.ToServiceUnitID);
                            svcUnitBpFrom = entity.FromServiceUnitID;
                            svcUnitBpTo = entity.ToServiceUnitID;
                        }

                        appparam = new AppParameter();
                        if (entity.TransactionCode == Reference.TransactionCode.PurchaseRequest)
                        {
                            if (!appparam.LoadByPrimaryKey("MainPurchasingUnitIDForNonMedical")) return string.Empty;
                            if (svcUnit.ServiceUnitID != appparam.ParameterValue) return string.Empty;
                        }
                        else
                        {
                            //if (!appparam.LoadByPrimaryKey("MainDistributionLocationIDForNonMedical")) return string.Empty;
                            //if (svcUnit.LocationID != appparam.ParameterValue) return string.Empty;

                            var tcsu = new ServiceUnitTransactionCodeQuery("a");
                            var su = new ServiceUnitQuery("b");
                            tcsu.InnerJoin(su).On(su.ServiceUnitID == tcsu.ServiceUnitID &&
                                                  tcsu.SRTransactionCode == Reference.TransactionCode.BudgetPlanApproval);
                            DataTable dtb = tcsu.LoadDataTable();

                            if (dtb.Rows.Count == 0) return string.Empty;
                        }

                        foreach (var item in coll)
                        {
                            var qtyBp = item.GetCountBudgetPlan(svcUnitBpFrom, svcUnitBpTo, item.ItemID,
                                entity.TransactionDate.Value.Year, entity.TransactionNo);

                            var qtyBpRe = item.GetCountBudgetPlanRealization(svcUnitBpFrom, svcUnitBpTo, item.ItemID,
                                entity.TransactionDate.Value.Year, entity.TransactionNo, true);

                            var balance = qtyBp - qtyBpRe;
                            if (item.Quantity * item.ConversionFactor > balance)
                            {
                                // lebih dari budget
                                str = (str == string.Empty)
                                          ? "Insufficient budget plan balance for item " + item.ItemID + "[bal: " +
                                            balance.ToString() + "]"
                                          : str + ", " + item.ItemID + "[bal: " + balance.ToString() + "]";
                            }
                        }

                        //// populate budget plan
                        //var qtiColl = new ItemTransactionItemCollection();
                        //var qti = new ItemTransactionItemQuery("a");
                        //var qt = new ItemTransactionQuery("b");

                        //qti.InnerJoin(qt).On(qti.TransactionNo == qt.TransactionNo);
                        //qti.Where(qt.TransactionCode == BusinessObject.Reference.TransactionCode.BudgetPlanApproval,
                        //    qt.IsVoid == false,
                        //    qt.TransactionNo != entity.TransactionNo);
                        //qti.Where(string.Format("<YEAR(b.TransactionDate) = {0}>", entity.TransactionDate.Value.Year));
                        //qti.Where(qt.IsApproved == true);

                        //qti.Where(qt.FromServiceUnitID == svcUnitBpFrom, qt.ToServiceUnitID == svcUnitBpTo);
                        //qti.Select(qti);

                        //qtiColl.Load(qti);
                        //// end of populate budget plan

                        //// populate budget plan realization
                        //var qtiColl1 = new ItemTransactionItemCollection();
                        //var qti1 = new ItemTransactionItemQuery("a");
                        //var qt1 = new ItemTransactionQuery("b");

                        //qti1.InnerJoin(qt1).On(qti1.TransactionNo == qt1.TransactionNo);
                        //qti1.Where(qt1.TransactionCode == BusinessObject.Reference.TransactionCode.Distribution,
                        //    qt1.IsVoid == false,
                        //    qt1.TransactionNo != entity.TransactionNo);
                        //qti1.Where(string.Format("<YEAR(b.TransactionDate) = {0}>", entity.TransactionDate.Value.Year));
                        //if (true)//(IsApprovedOnly)
                        //{
                        //    qti1.Where(qt1.IsApproved == true);
                        //}
                        //qti1.Where(qt1.FromServiceUnitID == svcUnitBpTo,
                        //    qt1.ToServiceUnitID == svcUnitBpFrom);
                        //qti1.Select(qti1);

                        //qtiColl1.Load(qti1);

                        ////var qty = qtiColl1.Sum(x => (x.Quantity * x.ConversionFactor)) ?? 0;

                        //// dikurangi yang balik
                        //var qtiColl2 = new ItemTransactionItemCollection();
                        //var qti2 = new ItemTransactionItemQuery("a");
                        //var qt2 = new ItemTransactionQuery("b");

                        //qti2.InnerJoin(qt2).On(qti2.TransactionNo == qt2.TransactionNo);
                        //qti2.Where(qt2.TransactionCode == BusinessObject.Reference.TransactionCode.Distribution,
                        //    qt2.IsVoid == false,
                        //    qt2.TransactionNo != entity.TransactionNo);
                        //qti2.Where(string.Format("<YEAR(b.TransactionDate) = {0}>", entity.TransactionDate.Value.Year));
                        //if (true)//(IsApprovedOnly)
                        //{
                        //    qti2.Where(qt.IsApproved == true);
                        //}
                        //qti2.Where(qt2.FromServiceUnitID == svcUnitBpFrom, qt2.ToServiceUnitID == svcUnitBpTo);
                        //qti2.Select(qti2);

                        //qtiColl2.Load(qti2);

                        ////qty = qty - qtiColl2.Sum(x => (x.Quantity * x.ConversionFactor)) ?? 0;

                        //// gabung dengan penerimaan untuk item non inventory
                        //var qtiColl3 = new ItemTransactionItemCollection();
                        //var qti3 = new ItemTransactionItemQuery("a");
                        //var qt3 = new ItemTransactionQuery("b");
                        //var itmnm = new ItemProductNonMedicQuery("c");

                        //qti3.InnerJoin(qt3).On(qti3.TransactionNo == qt3.TransactionNo);
                        //qti3.InnerJoin(itmnm).On(qti3.ItemID == itmnm.ItemID);
                        //qti3.Where(qt3.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseOrderReceive,
                        //    qt3.IsVoid == false,
                        //    qt3.TransactionNo != entity.TransactionNo,
                        //    itmnm.IsInventoryItem == false);
                        //qti3.Where(string.Format("<YEAR(b.TransactionDate) = {0}>", entity.TransactionDate.Value.Year));
                        //if (true) //(IsApprovedOnly)
                        //{
                        //    qti3.Where(qt.IsApproved == true);
                        //}
                        //qti3.Where(qt3.FromServiceUnitID == svcUnitBpFrom, qt3.ToServiceUnitID == svcUnitBpTo);
                        //qti3.Select(qti3);

                        //qtiColl3.Load(qti3);

                        ////qty = qty + qtiColl3.Sum(x => (x.Quantity * x.ConversionFactor)) ?? 0;


                        ////return qty;
                        //// end of populate budget plan realization

                        //foreach (var c in coll)
                        //{
                        //    var quota = qtiColl.Where(x => x.ItemID == c.ItemID).Sum(x => (x.Quantity * x.ConversionFactor)) ?? 0;
                        //    var dist = (qtiColl1.Where(x => x.ItemID == c.ItemID).Sum(x => (x.Quantity * x.ConversionFactor)) ?? 0);
                        //    var ret = (qtiColl2.Where(x => x.ItemID == c.ItemID).Sum(x => (x.Quantity * x.ConversionFactor)) ?? 0);
                        //    var pr = (qtiColl3.Where(x => x.ItemID == c.ItemID).Sum(x => (x.Quantity * x.ConversionFactor)) ?? 0);
                        //    var balance = quota - dist + ret - pr;
                        //    if (c.Quantity * c.ConversionFactor > balance)
                        //    {
                        //        // lebih dari budget
                        //        str = (str == string.Empty)
                        //                  ? "Insufficient budget plan balance for item " + c.ItemID + "[bal: " +
                        //                    balance.ToString() + "]"
                        //                  : str + ", " + c.ItemID + "[bal: " + balance.ToString() + "]";
                        //    }
                        //}
                    }
                }
            }

            return str;
        }

        private static string ApproveProcess(ItemTransaction entity, ItemTransactionItemCollection coll, string userID, bool isApproval, decimal rounding)
        {
            if (isApproval)
            {
                if (entity.IsApproved ?? false)
                    return "Approved";
                if (entity.IsVoid ?? false)
                    return "Void";

                var str = CekBudgetPlan(entity, coll);
                if (str != string.Empty) return str;
            }
            else
            {
                if (IsNeedUpdateBalance(entity.TransactionCode))
                {
                    //Transaksi yg bila di approve akan mengupdate balance, tidak bisa di UnApprove
                    return "CantUnApprove";
                }
                if (!entity.IsApproved ?? false)
                    return "UnApproved";
            }

            var locationId = GetLocationID(entity);
            if (IsNeedUpdateBalance(entity.TransactionCode) && !string.IsNullOrEmpty(locationId))
            {
                var loc = new Location();
                if (loc.LoadByPrimaryKey(locationId) && loc.IsHoldForTransaction == true)
                    return "Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.";
            }
            bool isEnabledStockWithEdControl = (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsEnabledStockWithEdControl).ToLower() == "yes");

            entity.IsApproved = isApproval;
            entity.ApprovedDate = Utils.NowAtSqlServer(); //DateTime.Now;
            entity.ApprovedByUserID = userID;

            var collEd = new ItemTransactionItemEdCollection();
            collEd.Query.Where(collEd.Query.TransactionNo == entity.TransactionNo);
            collEd.LoadAll();

            ItemTransaction entityRef;
            ItemTransactionItemCollection collRef;
            ItemTransactionItemEdCollection collEdRef;
            PrepareUpdateReferenceItem(entity.TransactionCode, entity.ReferenceNo, entity.TransactionNo, isApproval, userID, out entityRef,
                out collRef, out collEdRef);

            ItemBalanceCollection itemBalances = null;
            //ItemBalanceByPeriodCollection itemBalanceByPeriods = null;
            //ItemBalanceExpireCollection itemBalanceExpires = null;
            ItemBalanceDetailCollection itemBalanceDetails = null;
            ItemBalanceDetailEdCollection itemBalanceDetailEds = null;
            ItemMovementCollection itemMovements = null;
            ItemProductMedicCollection itemProductMedics = null;
            ItemProductNonMedicCollection itemProductNonMedics = null;
            ItemKitchenCollection itemKitchens = null;
            ItemTariffCollection itemTariffs = null;
            //ItemTariffComponentCollection itemTariffComponents = null;
            AveragePriceHistoryCollection averageMedics = null;
            AveragePriceHistoryCollection averageNonMedics = null;
            AveragePriceHistoryCollection averageKitchens = null;
            SupplierItemCollection supplierItems = null;
            SupplierContract supplierContract;
            PrepareUpdateSupplierContract(entity, isApproval, userID, out supplierContract);

            ItemTransactionItemBakCollection baks = null;

            ItemBalanceCollection itemBalanceConsignments = null;
            ItemBalanceDetailCollection itemBalanceDetailConsignments = null;
            ItemBalanceDetailEdCollection itemBalanceDetailEdConsignments = null;
            ItemMovementCollection itemMovementConsignments = null;

            var app = new AppParameter();
            var isJournalAdjustmentReverse = false;

            if (isApproval)
            {
                if (entity.RevisionNumber == null)
                    entity.RevisionNumber = 0;
                else
                    entity.RevisionNumber += 1;

                if (!(entity.TransactionCode == Reference.TransactionCode.PurchaseOrderReceive && entity.IsConsignmentAlreadyReceived != null && entity.IsConsignmentAlreadyReceived == true))
                {
                    itemBalances = PrepareItemBalances(entity, coll, locationId, userID);
                    //itemBalanceByPeriods = PrepareItemBalanceByPeriods(entity, coll, locationId, userID);

                    //if (app.LoadByPrimaryKey("IsTxUsingEdDetail") && app.ParameterValue == "Yes")
                    //{
                    //    switch (entity.TransactionCode)
                    //    {
                    //        case Reference.TransactionCode.PurchaseOrderReturn:
                    //            itemBalanceExpires = PrepareItemBalanceExpires(entity, coll, locationId, userID);
                    //            break;
                    //        default:
                    //            itemBalanceExpires = PrepareItemBalanceExpires(entity, collEd, locationId, userID, "");
                    //            break;
                    //    }
                    //}
                    //else
                    //    itemBalanceExpires = PrepareItemBalanceExpires(entity, coll, locationId, userID);
                }

                //-- checking utk function item balance detail & price
                switch (entity.TransactionCode)
                {
                    case Reference.TransactionCode.GrantsReceive:
                        // Update Balance Detail (FIFO)
                        if (!isEnabledStockWithEdControl)
                            itemBalanceDetails = PrepareItemBalanceDetail(entity, coll, locationId, userID);

                        // Update Master
                        PrepareItemProductMedicAndAverage(entity, coll, userID, out itemProductMedics, out averageMedics, true);
                        PrepareItemProductNonMedicAndAverage(entity, coll, userID, out itemProductNonMedics, out averageNonMedics, true);
                        PrepareItemKitchenAndAverage(entity, coll, userID, out itemKitchens, out averageKitchens, true);
                        break;
                    case Reference.TransactionCode.PurchaseOrderReceive:
                        if (entity.IsConsignmentAlreadyReceived == null || entity.IsConsignmentAlreadyReceived == false)
                        {
                            // Update Balance Detail (FIFO)
                            if (!isEnabledStockWithEdControl)
                                itemBalanceDetails = PrepareItemBalanceDetail(entity, coll, locationId, userID);

                            // Update Master ItemProduct
                            if (entity.SRPurchaseOrderType == "CS")
                            {
                                PrepareItemProductMedicAndAverage(entity, coll, userID, out itemProductMedics, out averageMedics,
                                    (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCashPurchaseOrderUpdatePrice) == "Yes"));
                            }
                            else
                                PrepareItemProductMedicAndAverage(entity, coll, userID, out itemProductMedics, out averageMedics, true);

                            PrepareItemProductNonMedicAndAverage(entity, coll, userID, out itemProductNonMedics, out averageNonMedics, true);
                            PrepareItemKitchenAndAverage(entity, coll, userID, out itemKitchens, out averageKitchens, true);

                            if (entity.SRPurchaseOrderType == "CS")
                            {
                                if (entity.SRItemType != ItemType.Medical || AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCashPurchaseOrderUpdatePrice) == "Yes")
                                    itemTariffs = PrepareItemTariffs(entity, userID, rounding, false);
                            }
                            else
                            {
                                itemTariffs = PrepareItemTariffs(entity, userID, rounding, false);
                            }
                        }

                        break;

                    case Reference.TransactionCode.DirectPurchase:
                        // Update Balance Detail (FIFO)
                        if (!isEnabledStockWithEdControl)
                            itemBalanceDetails = PrepareItemBalanceDetail(entity, coll, locationId, userID);

                        // Update Master
                        //PrepareItemProductMedicAndAverage(entity, coll, userID, out itemProductMedics, out averageMedics, true);
                        //-db: direct purchase = cash (SRPurchaseOrderType = "CS")
                        PrepareItemProductMedicAndAverage(entity, coll, userID, out itemProductMedics, out averageMedics,
                                    (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCashPurchaseOrderUpdatePrice) == "Yes"));
                        PrepareItemProductNonMedicAndAverage(entity, coll, userID, out itemProductNonMedics, out averageNonMedics, true);
                        PrepareItemKitchenAndAverage(entity, coll, userID, out itemKitchens, out averageKitchens, true);

                        itemTariffs = PrepareItemTariffs(entity, userID, rounding, false);
                        //itemTariffComponents = PrepareItemTariffComponents(entity, userID, rounding, false);
                        break;

                    case Reference.TransactionCode.ConsignmentReceive:
                    case Reference.TransactionCode.SalesToBranchReturn:
                    case Reference.TransactionCode.ReceiptOfSubstitute:
                    case Reference.TransactionCode.SalesReturn:
                        if (!isEnabledStockWithEdControl)
                            itemBalanceDetails = PrepareItemBalanceDetail(entity, coll, locationId, userID);
                        break;

                    case Reference.TransactionCode.DistributionConfirm:
                        if (!isEnabledStockWithEdControl)
                            itemBalanceDetails = PrepareItemBalanceDetailForDistribution(entity, coll, locationId, userID);
                        break;
                    case Reference.TransactionCode.PurchaseOrderReturn:
                        if (!isEnabledStockWithEdControl)
                            itemBalanceDetails = PrepareItemBalanceDetailForPurchaseOrderReturn(entity, coll, locationId, userID);

                        //PrepareItemProductMedicAndAverage(entity, coll, userID, out itemProductMedics, out averageMedics, true);
                        //PrepareItemProductNonMedicAndAverage(entity, coll, userID, out itemProductNonMedics, out averageNonMedics, true);
                        //PrepareItemKitchenAndAverage(entity, coll, userID, out itemKitchens, out averageKitchens, true);

                        //itemTariffs = PrepareItemTariffs(entity, userID, rounding, false);
                        //itemTariffComponents = PrepareItemTariffComponents(entity, userID, rounding, false);
                        break;
                    case Reference.TransactionCode.ConsignmentReturn:
                        if (!isEnabledStockWithEdControl)
                            itemBalanceDetails = PrepareItemBalanceDetailForPurchaseOrderReturn(entity, coll, locationId, userID);
                        break;
                }

                //-- checking utk function item movement-nya
                switch (entity.TransactionCode)
                {
                    case Reference.TransactionCode.GrantsReceive:
                        if (!isEnabledStockWithEdControl)
                            itemMovements = PrepareItemMovementsForPurchaseOrderReceive(entity, coll, itemBalances, locationId, userID, itemProductMedics, itemProductNonMedics, itemKitchens);
                        else
                            PrepareItemMovementsAndItemBalanceDetailEdForPurchaseOrderReceive(entity, coll, itemBalances, locationId, userID, itemProductMedics, itemProductNonMedics, itemKitchens, out itemMovements, out itemBalanceDetailEds);
                        break;
                    case Reference.TransactionCode.PurchaseOrderReceive:
                        if (entity.IsConsignmentAlreadyReceived == null || entity.IsConsignmentAlreadyReceived == false)
                        {
                            if (!isEnabledStockWithEdControl)
                                itemMovements = PrepareItemMovementsForPurchaseOrderReceive(entity, coll, itemBalances, locationId, userID, itemProductMedics, itemProductNonMedics, itemKitchens);
                            else    
                                PrepareItemMovementsAndItemBalanceDetailEdForPurchaseOrderReceive(entity, coll, itemBalances, locationId, userID, itemProductMedics, itemProductNonMedics, itemKitchens, out itemMovements, out itemBalanceDetailEds);
                        }
                            
                        supplierItems = PrepareSupplierItems(coll, userID, entity.BusinessPartnerID, entity.SRItemType);
                        break;

                    case Reference.TransactionCode.DirectPurchase:
                        if (!isEnabledStockWithEdControl)
                            itemMovements = PrepareItemMovementsForPurchaseOrderReceive(entity, coll, itemBalances, locationId, userID, itemProductMedics, itemProductNonMedics, itemKitchens);
                        else    
                            PrepareItemMovementsAndItemBalanceDetailEdForPurchaseOrderReceive(entity, coll, itemBalances, locationId, userID, itemProductMedics, itemProductNonMedics, itemKitchens, out itemMovements, out itemBalanceDetailEds);
                        supplierItems = PrepareSupplierItems(coll, userID, entity.BusinessPartnerID, entity.SRItemType);
                        break;

                    case Reference.TransactionCode.SalesToBranchReturn:
                    case Reference.TransactionCode.ReceiptOfSubstitute:
                        itemProductMedics = new ItemProductMedicCollection();
                        itemProductMedics.Query.Where(itemProductMedics.Query.ItemID.In((coll.Select(c => c.ItemID)).Distinct()));
                        itemProductMedics.LoadAll();

                        itemProductNonMedics = new ItemProductNonMedicCollection();
                        itemProductNonMedics.Query.Where(itemProductNonMedics.Query.ItemID.In((coll.Select(c => c.ItemID)).Distinct()));
                        itemProductNonMedics.LoadAll();

                        itemKitchens = new ItemKitchenCollection();
                        itemKitchens.Query.Where(itemKitchens.Query.ItemID.In((coll.Select(c => c.ItemID)).Distinct()));
                        itemKitchens.LoadAll();

                        if (!isEnabledStockWithEdControl)
                            itemMovements = PrepareItemMovementsForPurchaseOrderReceive(entity, coll, itemBalances, locationId, userID, itemProductMedics, itemProductNonMedics, itemKitchens);
                        else
                            PrepareItemMovementsAndItemBalanceDetailEdForPurchaseOrderReceive(entity, coll, itemBalances, locationId, userID, itemProductMedics, itemProductNonMedics, itemKitchens, out itemMovements, out itemBalanceDetailEds);

                        break;

                    case Reference.TransactionCode.SalesReturn:
                        itemProductMedics = new ItemProductMedicCollection();
                        itemProductMedics.Query.Where(itemProductMedics.Query.ItemID.In((coll.Select(c => c.ItemID)).Distinct()));
                        itemProductMedics.LoadAll();

                        itemProductNonMedics = new ItemProductNonMedicCollection();
                        itemProductNonMedics.Query.Where(itemProductNonMedics.Query.ItemID.In((coll.Select(c => c.ItemID)).Distinct()));
                        itemProductNonMedics.LoadAll();

                        itemKitchens = new ItemKitchenCollection();
                        itemKitchens.Query.Where(itemKitchens.Query.ItemID.In((coll.Select(c => c.ItemID)).Distinct()));
                        itemKitchens.LoadAll();

                        if (!isEnabledStockWithEdControl)
                            itemMovements = PrepareItemMovementsForPurchaseOrderReceive(entity, coll, itemBalances, locationId, userID, itemProductMedics, itemProductNonMedics, itemKitchens);
                        else
                            PrepareItemMovementsAndItemBalanceDetailEdForSalesReturn(entity, coll, itemBalances, locationId, userID, itemProductMedics, itemProductNonMedics, itemKitchens, out itemMovements, out itemBalanceDetailEds);

                        break;

                    case Reference.TransactionCode.PurchaseOrderReturn:
                    case Reference.TransactionCode.ConsignmentReturn:
                        itemProductMedics = new ItemProductMedicCollection();
                        itemProductMedics.Query.Where(itemProductMedics.Query.ItemID.In((coll.Select(c => c.ItemID)).Distinct()));
                        itemProductMedics.LoadAll();

                        itemProductNonMedics = new ItemProductNonMedicCollection();
                        itemProductNonMedics.Query.Where(itemProductNonMedics.Query.ItemID.In((coll.Select(c => c.ItemID)).Distinct()));
                        itemProductNonMedics.LoadAll();

                        itemKitchens = new ItemKitchenCollection();
                        itemKitchens.Query.Where(itemKitchens.Query.ItemID.In((coll.Select(c => c.ItemID)).Distinct()));
                        itemKitchens.LoadAll();

                        if (!isEnabledStockWithEdControl)
                            itemMovements = PrepareItemMovementsForPurchaseOrderReturn(entity, coll, itemBalances, locationId, userID, itemProductMedics, itemProductNonMedics, itemKitchens);
                        else    
                            PrepareItemMovementsAndItemBalanceDetailEdForPurchaseOrderReturn(entity, coll, itemBalances, locationId, userID, itemProductMedics, itemProductNonMedics, itemKitchens, out itemMovements, out itemBalanceDetailEds);
                        break;

                    default:
                        if (!isEnabledStockWithEdControl)
                            itemMovements = PrepareItemMovements(entity, coll, itemBalances, locationId, userID);
                        else 
                            PrepareItemMovementsAndItemBalanceDetailEd(entity, coll, itemBalances, out itemMovements, out itemBalanceDetailEds, locationId, userID);
                        break;
                }

                // khusus POR konsinyasi, mengurangi stok di lokasi konsinyasi
                if (entity.TransactionCode == Reference.TransactionCode.PurchaseOrderReceive && entity.IsConsignment == true)
                {
                    if (entity.IsConsignmentAlreadyReceived == null || entity.IsConsignmentAlreadyReceived == false)
                    {
                        locationId = entity.FromLocationID;

                        string itemNoStock;

                        itemBalanceConsignments = new ItemBalanceCollection();
                        itemBalanceConsignments.Query.Where(itemBalanceConsignments.Query.LocationID == locationId,
                                                            itemBalanceConsignments.Query.ItemID.In(
                                                                (coll.Select(c => c.ItemID)).Distinct()));
                        itemBalanceConsignments.LoadAll();

                        itemBalanceDetailConsignments = new ItemBalanceDetailCollection();
                        itemBalanceDetailConsignments.Query.Where(itemBalanceDetailConsignments.Query.LocationID == locationId,
                                                            itemBalanceDetailConsignments.Query.ItemID.In(
                                                                (coll.Select(c => c.ItemID)).Distinct()));
                        itemBalanceDetailConsignments.LoadAll();

                        itemBalanceDetailEdConsignments = new ItemBalanceDetailEdCollection();
                        itemBalanceDetailEdConsignments.Query.Where(itemBalanceDetailEdConsignments.Query.LocationID == locationId,
                                                            itemBalanceDetailEdConsignments.Query.ItemID.In(
                                                                (coll.Select(c => c.ItemID)).Distinct()), 
                                                            itemBalanceDetailEdConsignments.Query.IsActive == true);
                        itemBalanceDetailEdConsignments.LoadAll();

                        itemMovementConsignments = new ItemMovementCollection();
                        itemMovementConsignments.Query.Where(itemMovementConsignments.Query.LocationID == locationId,
                                                            itemMovementConsignments.Query.ItemID.In(
                                                                (coll.Select(c => c.ItemID)).Distinct()));
                        itemMovementConsignments.LoadAll();

                        ItemBalance.PrepareItemBalancesForPurchaseOrderReceivedConsignment(coll, entity.TransactionCode,
                                                                                           entity.ToServiceUnitID,
                                                                                           locationId,
                                                                                           userID,
                                                                                           ref itemBalanceConsignments,
                                                                                           ref itemBalanceDetailConsignments,
                                                                                           ref itemMovementConsignments,
                                                                                           ref itemBalanceDetailEdConsignments,
                                                                                           isEnabledStockWithEdControl,
                                                                                           out itemNoStock);
                    }
                    else
                        isJournalAdjustmentReverse = true;
                }
                else if (entity.TransactionCode == Reference.TransactionCode.PurchaseOrderReturn && entity.IsConsignment == true)
                {
                    // khusus PO Return konsinyasi, menambah stok di lokasi konsinyasi
                    var supp = new Supplier();
                    supp.LoadByPrimaryKey(entity.BusinessPartnerID);
                    locationId = entity.ToLocationID;

                    itemBalanceConsignments = new ItemBalanceCollection();
                    itemBalanceConsignments.Query.Where(itemBalanceConsignments.Query.LocationID == locationId,
                                                        itemBalanceConsignments.Query.ItemID.In(
                                                            (coll.Select(c => c.ItemID)).Distinct()));
                    itemBalanceConsignments.LoadAll();

                    itemBalanceDetailConsignments = new ItemBalanceDetailCollection();
                    itemBalanceDetailConsignments.Query.Where(itemBalanceDetailConsignments.Query.LocationID == locationId,
                                                        itemBalanceDetailConsignments.Query.ItemID.In(
                                                            (coll.Select(c => c.ItemID)).Distinct()));
                    itemBalanceDetailConsignments.LoadAll();

                    itemBalanceDetailEdConsignments = new ItemBalanceDetailEdCollection();
                    itemBalanceDetailEdConsignments.Query.Where(itemBalanceDetailEdConsignments.Query.LocationID == locationId,
                                                        itemBalanceDetailEdConsignments.Query.ItemID.In(
                                                            (coll.Select(c => c.ItemID)).Distinct()));
                    itemBalanceDetailEdConsignments.LoadAll();

                    itemMovementConsignments = new ItemMovementCollection();
                    itemMovementConsignments.Query.Where(itemMovementConsignments.Query.LocationID == locationId,
                                                        itemMovementConsignments.Query.ItemID.In(
                                                            (coll.Select(c => c.ItemID)).Distinct()));
                    itemMovementConsignments.LoadAll();

                    ItemBalance.PrepareItemBalancesForPurchaseOrderReturnConsignment(coll, entity,
                                                                                       locationId,
                                                                                       userID,
                                                                                       ref itemBalanceConsignments,
                                                                                       ref itemBalanceDetailConsignments,
                                                                                       ref itemMovementConsignments, ref itemBalanceDetailEdConsignments, isEnabledStockWithEdControl);
                }

                // khusus POR, insert to table backup. jaga2 takut masih ada data detil yg suka hilang. nanti kalo gak ada kejadian lagi,
                // boleh di remove
                if (entity.TransactionCode == Reference.TransactionCode.PurchaseOrderReceive)
                    baks = PrepareReceivedItemDetailForBackup(coll, entity.TransactionNo);
            }

            //return "debug";

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                coll.Save();

                if (entityRef != null)
                    entityRef.Save();
                if (collRef != null)
                    collRef.Save();
                if (collEdRef != null)
                    collEdRef.Save();
                if (itemBalances != null)
                    itemBalances.Save();
                //if (itemBalanceByPeriods != null)
                //    itemBalanceByPeriods.Save();
                //if (itemBalanceExpires != null)
                //    itemBalanceExpires.Save();
                if (itemBalanceDetails != null)
                    itemBalanceDetails.Save();
                if (itemBalanceDetailEds != null)
                    itemBalanceDetailEds.Save();
                if (itemMovements != null)
                    itemMovements.Save();
                if (itemProductMedics != null)
                    itemProductMedics.Save();
                if (itemProductNonMedics != null)
                    itemProductNonMedics.Save();
                if (itemKitchens != null)
                    itemKitchens.Save();
                if (itemTariffs != null)
                    itemTariffs.Save();
                //if (itemTariffComponents != null)
                //    itemTariffComponents.Save();
                if (averageMedics != null)
                    averageMedics.Save();
                if (averageNonMedics != null)
                    averageNonMedics.Save();
                if (averageKitchens != null)
                    averageKitchens.Save();
                if (supplierItems != null)
                    supplierItems.Save();
                if (supplierContract != null)
                    supplierContract.Save();
                if (baks != null)
                    baks.Save();

                if (itemBalanceConsignments != null)
                    itemBalanceConsignments.Save();
                if (itemBalanceDetailConsignments != null)
                    itemBalanceDetailConsignments.Save();
                if (itemBalanceDetailEdConsignments != null)
                    itemBalanceDetailEdConsignments.Save();
                if (itemMovementConsignments != null)
                    itemMovementConsignments.Save();

                /* Automatic Journal Testing Start */
                app = new AppParameter();
                switch (entity.TransactionCode)
                {
                    case Reference.TransactionCode.PurchaseOrderReceive:
                        app.LoadByPrimaryKey("acc_IsAutoJournalPOReceived");
                        if (app.ParameterValue == "Yes")
                        {
                            int? journalId;

                            //if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSIAMTP")
                            //{
                            //    var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.TransactionDate.Value);
                            //    if (isClosingPeriod)
                            //        return "Financial statements for period: " +
                            //               string.Format("{0:MMMM-yyyy}", entity.TransactionDate.Value) +
                            //               " have been closed. Please contact the authorities.";

                            //    journalId = JournalTransactions.AddNewPurchaseOrderReceivedJournalNonVat(entity, userID, 0);
                            //}
                            //else
                            //{
                                DateTime jDate = (new DateTime()).NowAtSqlServer();
                                jDate = AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_JournalPORDate).Equals("0") ?
                                    entity.TransactionDate.Value.Date : entity.ApprovedDate.Value.Date;

                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(jDate);
                                if (isClosingPeriod)
                                    return "Financial statements for period: " +
                                           string.Format("{0:MMMM-yyyy}", jDate) +
                                           " have been closed. Please contact the authorities.";

                                journalId = JournalTransactions.AddNewPurchaseOrderReceivedJournal(entity, userID, 0);
                            //}

                        }

                        if (isJournalAdjustmentReverse)
                        {
                            app.LoadByPrimaryKey("acc_IsAutoJournalStockAdjustment");
                            if (app.ParameterValue == "Yes")
                            {
                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.TransactionDate.Value);
                                if (isClosingPeriod)
                                    return "Financial statements for period: " +
                                           string.Format("{0:MMMM-yyyy}", entity.TransactionDate.Value) +
                                           " have been closed. Please contact the authorities.";

                                /* Automatic Journal Testing Start */

                                int? journalId = JournalTransactions.AddNewInventoryStockAdjustmentReverseJournal(entity, userID, 0, "SA", 0);

                                /* Automatic Journal Testing End */
                            }
                        }


                        break;

                    case Reference.TransactionCode.PurchaseOrderReturn:
                        app.LoadByPrimaryKey("acc_IsAutoJournalPOReturn");
                        if (app.ParameterValue == "Yes")
                        {
                            DateTime jDate = DateTime.Now;
                            var appprmdate = new AppParameter();
                            if (appprmdate.LoadByPrimaryKey("acc_JournalPORetDate"))
                                jDate = appprmdate.ParameterValue.ToString().Equals("0") ?
                                    entity.TransactionDate.Value.Date : entity.ApprovedDate.Value.Date;

                            var isClosingPeriod = PostingStatus.IsPeriodeClosed(jDate);
                            if (isClosingPeriod)
                                return "Financial statements for period: " +
                                       string.Format("{0:MMMM-yyyy}", jDate) +
                                       " have been closed. Please contact the authorities.";

                            if (entity.SRPurchaseReturnType == "CR")
                            {
                                int? journalId = JournalTransactions.AddNewPurchaseOrderReturnedJournal(entity, userID, 0);
                            }
                            else
                            {
                                //jurnal pengurangan persediaan
                                int? journalId = JournalTransactions.AddNewPurchaseOrderReturnedJournal(entity, userID, 0);
                            }
                        }
                        break;

                    case Reference.TransactionCode.ReceiptOfSubstitute:
                        //jurnal penambah persediaan
                        break;
                    case Reference.TransactionCode.GrantsReceive:
                        app.LoadByPrimaryKey("acc_IsAutoJournalPOReceived");
                        if (app.ParameterValue == "Yes")
                        {
                            int? journalId;

                            DateTime jDate = (new DateTime()).NowAtSqlServer();
                            jDate = AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_JournalPORDate).Equals("0") ?
                                entity.TransactionDate.Value.Date : entity.ApprovedDate.Value.Date;

                            var isClosingPeriod = PostingStatus.IsPeriodeClosed(jDate);
                            if (isClosingPeriod)
                                return "Financial statements for period: " +
                                       string.Format("{0:MMMM-yyyy}", jDate) +
                                       " have been closed. Please contact the authorities.";

                            journalId = JournalTransactions.AddNewGrantReceivedJournal(entity, userID, 0);
                        }

                        break;
                    case Reference.TransactionCode.DirectPurchase:
                        app.LoadByPrimaryKey("acc_IsAutoJournalPOReceived");
                        if (app.ParameterValue == "Yes")
                        {
                            int? journalId;

                            DateTime jDate = (new DateTime()).NowAtSqlServer();
                            jDate = AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_JournalPORDate).Equals("0") ?
                                entity.TransactionDate.Value.Date : entity.ApprovedDate.Value.Date;

                            var isClosingPeriod = PostingStatus.IsPeriodeClosed(jDate);
                            if (isClosingPeriod)
                                return "Financial statements for period: " +
                                       string.Format("{0:MMMM-yyyy}", jDate) +
                                       " have been closed. Please contact the authorities.";

                            journalId = JournalTransactions.AddNewPurchaseOrderReceivedJournal(entity, userID, 0);
                        }
                        break;
                    case Reference.TransactionCode.SalesReturn:
                        app.LoadByPrimaryKey("acc_IsAutoJournalSales");
                        if (app.ParameterValue == "Yes")
                        {
                            int? journalId;

                            DateTime jDate = entity.TransactionDate.Value.Date;

                            var isClosingPeriod = PostingStatus.IsPeriodeClosed(jDate);
                            if (isClosingPeriod)
                                return "Financial statements for period: " +
                                       string.Format("{0:MMMM-yyyy}", jDate) +
                                       " have been closed. Please contact the authorities.";

                            journalId = JournalTransactions.AddNewSalesReturnedJournal(entity, userID, 0);
                        }
                        break;
                }

                trans.Complete();


            }
            return string.Empty;
        }

        private static string GetLocationID(esItemTransaction itemTransaction)
        {
            string retVal;
            if (itemTransaction.TransactionCode == Reference.TransactionCode.PurchaseOrderReceive ||
                itemTransaction.TransactionCode == Reference.TransactionCode.ConsignmentReceive ||
                itemTransaction.TransactionCode == Reference.TransactionCode.DirectPurchase ||
                itemTransaction.TransactionCode == Reference.TransactionCode.SalesToBranchReturn ||
                itemTransaction.TransactionCode == Reference.TransactionCode.ReceiptOfSubstitute ||
                itemTransaction.TransactionCode == Reference.TransactionCode.ConsignmentTransfer ||
                itemTransaction.TransactionCode == Reference.TransactionCode.GrantsReceive ||
                itemTransaction.TransactionCode == Reference.TransactionCode.SalesReturn)

                retVal = itemTransaction.ToLocationID;
            else
                retVal = itemTransaction.FromLocationID;

            return retVal;
        }

        private static string GetServiceUnitID(esItemTransaction itemTransaction)
        {
            if (itemTransaction.TransactionCode == Reference.TransactionCode.PurchaseOrderReceive ||
                itemTransaction.TransactionCode == Reference.TransactionCode.ConsignmentReceive ||
                itemTransaction.TransactionCode == Reference.TransactionCode.DirectPurchase ||
                itemTransaction.TransactionCode == Reference.TransactionCode.SalesToBranchReturn ||
                itemTransaction.TransactionCode == Reference.TransactionCode.ReceiptOfSubstitute ||
                itemTransaction.TransactionCode == Reference.TransactionCode.ConsignmentTransfer ||
                itemTransaction.TransactionCode == Reference.TransactionCode.GrantsReceive)
                return itemTransaction.ToServiceUnitID;
            return itemTransaction.FromServiceUnitID;
        }

        private static ItemBalanceDetailCollection PrepareItemBalanceDetail(esItemTransaction entity, IEnumerable<ItemTransactionItem> coll,
            string locationID, string userID)
        {
            if (!IsNeedUpdateBalance(entity.TransactionCode))
                return null;

            var list = coll.GroupBy(c => new
            {
                c.ItemID
                //,
                //c.Price
            }).Select(q => new
            {
                q.Key.ItemID,
                Quantity = q.Sum(p => (p.ConversionFactor * p.Quantity)),
                Price = q.Sum(p => ((p.PriceInCurrency - p.DiscountInCurrency) / p.ConversionFactor))
            });

            var balances = new ItemBalanceDetailCollection();

            foreach (var item in list)
            {
                var isInventoryItem = true;
                switch (entity.SRItemType)
                {
                    case ItemType.Medical:
                        var ipm = new ItemProductMedic();
                        ipm.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = ipm.IsInventoryItem ?? false;
                        break;
                    case ItemType.NonMedical:
                        var ipnm = new ItemProductNonMedic();
                        ipnm.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = ipnm.IsInventoryItem ?? false;
                        break;
                    case ItemType.Kitchen:
                        var ik = new ItemKitchen();
                        ik.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = ik.IsInventoryItem ?? false;
                        break;
                }

                if (isInventoryItem)
                {
                    var balance = balances.AddNew();
                    balance.LocationID = locationID;
                    balance.ItemID = item.ItemID;
                    balance.ReferenceNo = entity.TransactionNo;
                    balance.TransactionCode = entity.TransactionCode;
                    balance.BalanceDate = entity.TransactionDate;
                    balance.Balance = item.Quantity;
                    balance.Booking = 0;
                    balance.Price = item.Price + (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (item.Price * (entity.TaxPercentage / 100)) : 0);
                    balance.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                    balance.LastUpdateByUserID = userID;
                }
            }

            return balances;
        }

        private static ItemBalanceDetailCollection PrepareItemBalanceDetailForPurchaseOrderReturn(esItemTransaction entity, IEnumerable<ItemTransactionItem> coll,
            string locationID, string userID)
        {
            if (!IsNeedUpdateBalance(entity.TransactionCode))
                return null;

            var list = coll.GroupBy(c => new
            {
                c.ItemID
                //,
                //c.Price
            }).Select(q => new
            {
                q.Key.ItemID,
                Quantity = q.Sum(p => (p.ConversionFactor * p.Quantity)),
                Price = q.Sum(p => ((p.PriceInCurrency - p.Discount) / p.ConversionFactor))
            });

            var balances = new ItemBalanceDetailCollection();
            balances.Query.Where(
                balances.Query.LocationID == locationID,
                balances.Query.ItemID.In(list.Select(l => l.ItemID).ToArray()),
                balances.Query.ReferenceNo == entity.ReferenceNo
                );
            balances.LoadAll();

            foreach (var item in list)
            {
                var balance = balances.Where(b => b.ItemID == item.ItemID).SingleOrDefault();
                if (balance == null)
                {
                    balance = balances.AddNew();
                    balance.LocationID = locationID;
                    balance.ItemID = item.ItemID;
                    balance.ReferenceNo = entity.TransactionNo;
                    balance.TransactionCode = entity.TransactionCode;
                    balance.BalanceDate = entity.TransactionDate;
                    balance.Balance = 0 - item.Quantity;
                    balance.Booking = 0;
                    balance.Price = item.Price + (item.Price *
                        (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (entity.TaxPercentage / 100) : 0));
                    balance.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                    balance.LastUpdateByUserID = userID;
                }
                else
                {
                    balance.Balance -= item.Quantity;
                    balance.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                    balance.LastUpdateByUserID = userID;
                }
            }

            return balances;
        }

        private static ItemBalanceDetailCollection PrepareItemBalanceDetailForDistribution(ItemTransaction entity, ItemTransactionItemCollection coll,
          string locationID, string userID)
        {
            if (!IsNeedUpdateBalance(entity.TransactionCode))
                return null;

            var list = coll.GroupBy(c => new
            {
                c.ItemID,
                c.Price
            }).Select(q => new
            {
                q.Key.ItemID,
                Quantity = q.Sum(p => (p.ConversionFactor * p.Quantity)),
                Price = q.Sum(p => p.CostPrice)
            });

            var balances = new ItemBalanceDetailCollection();

            foreach (var item in list)
            {
                var balance = balances.AddNew();
                balance.LocationID = locationID;
                balance.ItemID = item.ItemID;
                balance.ReferenceNo = entity.TransactionNo;
                balance.TransactionCode = entity.TransactionCode;
                balance.BalanceDate = entity.TransactionDate;
                balance.Balance = item.Quantity;
                balance.Booking = 0;
                balance.Price = item.Price;
                balance.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                balance.LastUpdateByUserID = userID;
            }

            return balances;
        }

        private static ItemMovementCollection PrepareItemMovements(ItemTransaction entity, ItemTransactionItemCollection coll,
            ItemBalanceCollection balances, string locationID, string userID)
        {
            if (!IsNeedUpdateBalance(entity.TransactionCode))
                return null;

            var allBalances = new ItemBalanceCollection();
            allBalances.Query.Select(
                allBalances.Query.ItemID,
                allBalances.Query.Balance.Sum()
                );
            allBalances.Query.Where(allBalances.Query.ItemID.In((coll.Select(c => c.ItemID)).Distinct()));
            allBalances.Query.GroupBy(allBalances.Query.ItemID);
            allBalances.LoadAll();

            var movements = new ItemMovementCollection();

            var balanceType = GetBalanceType(entity.TransactionCode);

            foreach (var item in coll)
            {
                var itemMovement = movements.AddNew();
                itemMovement.MovementDate = entity.ApprovedDate;
                itemMovement.ServiceUnitID = GetServiceUnitID(entity);
                itemMovement.LocationID = locationID;
                itemMovement.TransactionCode = entity.TransactionCode;
                itemMovement.TransactionNo = entity.TransactionNo;
                itemMovement.SequenceNo = item.SequenceNo;
                itemMovement.ItemID = item.ItemID;
                if (item.ExpiredDate != null)
                    itemMovement.ExpiredDate = item.ExpiredDate;
                else
                    itemMovement.str.ExpiredDate = string.Empty;

                var balance = balances.FindByPrimaryKey(itemMovement.LocationID, itemMovement.ItemID);
                itemMovement.InitialStock = balance.Balance - (item.Quantity * item.ConversionFactor);

                if (balanceType == BalanceType.QtyIn)
                {
                    itemMovement.QuantityIn = item.Quantity * item.ConversionFactor;
                    itemMovement.QuantityOut = 0;
                }
                else
                {
                    itemMovement.QuantityIn = 0;
                    itemMovement.QuantityOut = item.Quantity * item.ConversionFactor;
                }

                string baseUnit;
                decimal? costPrice;
                string parCostType = AppParameter.GetParameterValue(AppParameter.ParameterItem.ItemProductCostPriceType);

                if (entity.TransactionCode == Reference.TransactionCode.PurchaseOrderReceive ||
                    entity.TransactionCode == Reference.TransactionCode.DirectPurchase)
                {
                    var discount1Amount = (item.PriceInCurrency * item.Discount1Percentage) / 100;
                    var discount2Amount = ((item.PriceInCurrency - discount1Amount) * item.Discount2Percentage) / 100;
                    var priceWvat = (item.PriceInCurrency - discount1Amount - discount2Amount) * (1 + entity.TaxPercentage / 100);
                    costPrice = priceWvat / (item.ConversionFactor == 0 ? 1 : item.ConversionFactor);
                    baseUnit = item.SRItemUnit;
                }
                else
                {
                    if (entity.SRItemType == ItemType.Medical)
                    {
                        var medic = new ItemProductMedic();
                        medic.LoadByPrimaryKey(itemMovement.ItemID);
                        baseUnit = item.SRItemUnit;
                        if (parCostType == "AVG")
                            costPrice = item.CostPrice * (entity.CurrencyRate ?? 1); //ga yakin ini ngaruh kemana
                        else
                        {
                            costPrice = ((item.PriceInCurrency - item.Discount) / item.ConversionFactor) * (1 + (entity.TaxPercentage / 100));
                        }
                    }
                    else if (entity.SRItemType == ItemType.NonMedical)
                    {
                        var nonMedic = new ItemProductNonMedic();
                        nonMedic.LoadByPrimaryKey(itemMovement.ItemID);
                        baseUnit = item.SRItemUnit;
                        if (parCostType == "AVG")
                            costPrice = item.CostPrice * (entity.CurrencyRate ?? 1);
                        else
                        {
                            costPrice = ((item.PriceInCurrency - item.Discount) / item.ConversionFactor) * (1 + (entity.TaxPercentage / 100));
                        }
                    }
                    else
                    {
                        var kitchen = new ItemKitchen();
                        kitchen.LoadByPrimaryKey(itemMovement.ItemID);
                        baseUnit = item.SRItemUnit;
                        if (parCostType == "AVG")
                            costPrice = item.CostPrice * (entity.CurrencyRate ?? 1);
                        else
                        {
                            costPrice = ((item.PriceInCurrency - item.Discount) / item.ConversionFactor) * (1 + (entity.TaxPercentage / 100));
                        }
                    }
                }

                itemMovement.SRItemUnit = baseUnit;
                itemMovement.CostPrice = costPrice;
                itemMovement.SalesPrice = 0; //TODO: set Sales Price
                itemMovement.PurchasePrice = item.PriceInCurrency / (item.ConversionFactor == 0 ? 1 : item.ConversionFactor);
                itemMovement.LastPriceInBaseUnit = (((item.PriceInCurrency ?? 0) - (item.DiscountInCurrency ?? 0)) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0))) *
                        (1 + (((entity.IsTaxable ?? 0) == 1) && (item.IsTaxable ?? false) ? (entity.TaxPercentage ?? 0) : 0) / 100);
                itemMovement.LastUpdateByUserID = userID;
                itemMovement.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
            }
            return movements;
        }

        private static void PrepareItemMovementsAndItemBalanceDetailEd(ItemTransaction entity, ItemTransactionItemCollection coll,
            ItemBalanceCollection balances, out ItemMovementCollection itemMovements, out ItemBalanceDetailEdCollection itemBalanceDetailEds, string locationID, string userID)
        {
            if (!IsNeedUpdateBalance(entity.TransactionCode))
            {
                itemMovements = null;
                itemBalanceDetailEds = null;
                return;
            }

            var allBalances = new ItemBalanceCollection();
            allBalances.Query.Select(
                allBalances.Query.ItemID,
                allBalances.Query.Balance.Sum()
                );
            allBalances.Query.Where(allBalances.Query.ItemID.In((coll.Select(c => c.ItemID)).Distinct()));
            allBalances.Query.GroupBy(allBalances.Query.ItemID);
            allBalances.LoadAll();

            itemMovements = new ItemMovementCollection();

            itemBalanceDetailEds = new ItemBalanceDetailEdCollection();
            itemBalanceDetailEds.Query.Where(
                itemBalanceDetailEds.Query.ItemID.In(coll.Select(l => l.ItemID)),
                itemBalanceDetailEds.Query.LocationID == locationID
                );
            itemBalanceDetailEds.LoadAll();

            var balanceType = GetBalanceType(entity.TransactionCode);

            foreach (var item in coll)
            {
                var itiEds = new ItemTransactionItemEdCollection();
                itiEds.Query.Where(itiEds.Query.TransactionNo == entity.TransactionNo, itiEds.Query.SequenceNo == item.SequenceNo);
                itiEds.Query.OrderBy(itiEds.Query.ExpiredDate.Ascending, itiEds.Query.LastUpdateDateTime.Ascending);
                itiEds.LoadAll();

                if (itiEds.Count > 0)
                {
                    int x = 0;
                    decimal initialStock = 0;
                    decimal qtyInOut = 0;

                    foreach (var itiEd in itiEds)
                    {
                        #region ItemMovement

                        var itemMovement = itemMovements.AddNew();
                        itemMovement.MovementDate = entity.ApprovedDate.Value.AddMilliseconds(x * 100);
                        itemMovement.ServiceUnitID = GetServiceUnitID(entity);
                        itemMovement.LocationID = locationID;
                        itemMovement.TransactionCode = entity.TransactionCode;
                        itemMovement.TransactionNo = entity.TransactionNo;
                        itemMovement.SequenceNo = item.SequenceNo;
                        itemMovement.ItemID = item.ItemID;
                        itemMovement.ExpiredDate = itiEd.ExpiredDate;
                        itemMovement.BatchNumber = itiEd.BatchNumber;

                        if (balanceType == BalanceType.QtyIn)
                        {
                            if (x == 0)
                            {
                                var balance = balances.FindByPrimaryKey(itemMovement.LocationID, itemMovement.ItemID);
                                itemMovement.InitialStock = balance.Balance - item.Quantity;

                                initialStock = itemMovement.InitialStock ?? 0;
                            }
                            else
                            {
                                itemMovement.InitialStock = initialStock + qtyInOut;
                            }

                            itemMovement.QuantityIn = itiEd.Quantity * itiEd.ConversionFactor;
                            itemMovement.QuantityOut = 0;
                        }
                        else
                        {
                            if (x == 0)
                            {
                                var balance = balances.FindByPrimaryKey(itemMovement.LocationID, itemMovement.ItemID);
                                itemMovement.InitialStock = balance.Balance + item.Quantity;

                                initialStock = itemMovement.InitialStock ?? 0;
                            }
                            else
                            {
                                itemMovement.InitialStock = initialStock - qtyInOut;
                            }

                            itemMovement.QuantityIn = 0;
                            itemMovement.QuantityOut = itiEd.Quantity * itiEd.ConversionFactor;
                        }

                        qtyInOut += ((itemMovement.QuantityIn ?? 0) + (itemMovement.QuantityOut ?? 0));

                        string baseUnit;
                        decimal? costPrice;
                        string parCostType = AppParameter.GetParameterValue(AppParameter.ParameterItem.ItemProductCostPriceType);

                        if (entity.TransactionCode == Reference.TransactionCode.PurchaseOrderReceive ||
                            entity.TransactionCode == Reference.TransactionCode.DirectPurchase)
                        {
                            var discount1Amount = (item.PriceInCurrency * item.Discount1Percentage) / 100;
                            var discount2Amount = ((item.PriceInCurrency - discount1Amount) * item.Discount2Percentage) / 100;
                            var priceWvat = (item.PriceInCurrency - discount1Amount - discount2Amount) * (1 + entity.TaxPercentage / 100);
                            costPrice = priceWvat / (item.ConversionFactor == 0 ? 1 : item.ConversionFactor);
                            baseUnit = item.SRItemUnit;
                        }
                        else
                        {
                            if (entity.SRItemType == ItemType.Medical)
                            {
                                var medic = new ItemProductMedic();
                                medic.LoadByPrimaryKey(itemMovement.ItemID);
                                baseUnit = item.SRItemUnit;
                                if (parCostType == "AVG")
                                    costPrice = item.CostPrice * (entity.CurrencyRate ?? 1); //ga yakin ini ngaruh kemana
                                else
                                {
                                    costPrice = ((item.PriceInCurrency - item.Discount) / item.ConversionFactor) * (1 + (entity.TaxPercentage / 100));
                                }
                            }
                            else if (entity.SRItemType == ItemType.NonMedical)
                            {
                                var nonMedic = new ItemProductNonMedic();
                                nonMedic.LoadByPrimaryKey(itemMovement.ItemID);
                                baseUnit = item.SRItemUnit;
                                if (parCostType == "AVG")
                                    costPrice = item.CostPrice * (entity.CurrencyRate ?? 1);
                                else
                                {
                                    costPrice = ((item.PriceInCurrency - item.Discount) / item.ConversionFactor) * (1 + (entity.TaxPercentage / 100));
                                }
                            }
                            else
                            {
                                var kitchen = new ItemKitchen();
                                kitchen.LoadByPrimaryKey(itemMovement.ItemID);
                                baseUnit = item.SRItemUnit;
                                if (parCostType == "AVG")
                                    costPrice = item.CostPrice * (entity.CurrencyRate ?? 1);
                                else
                                {
                                    costPrice = ((item.PriceInCurrency - item.Discount) / item.ConversionFactor) * (1 + (entity.TaxPercentage / 100));
                                }
                            }
                        }

                        itemMovement.SRItemUnit = baseUnit;
                        itemMovement.CostPrice = costPrice;
                        itemMovement.SalesPrice = 0; //TODO: set Sales Price
                        itemMovement.PurchasePrice = item.PriceInCurrency / (item.ConversionFactor == 0 ? 1 : item.ConversionFactor);
                        itemMovement.LastPriceInBaseUnit = (((item.PriceInCurrency ?? 0) - (item.DiscountInCurrency ?? 0)) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0))) *
                                (1 + (((entity.IsTaxable ?? 0) == 1) && (item.IsTaxable ?? false) ? (entity.TaxPercentage ?? 0) : 0) / 100);
                        itemMovement.LastUpdateByUserID = userID;
                        itemMovement.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                        #endregion

                        #region ItemBalanceDetailEd

                        ItemBalanceDetailEd itemBalanceDetailEd = null;
                        var isAvailable = false;
                        foreach (var findBalance in itemBalanceDetailEds.Where(findBalance => findBalance.ItemID.Equals(item.ItemID) &&
                                                                                findBalance.ExpiredDate.Equals(itiEd.ExpiredDate) &&
                                                                                  findBalance.BatchNumber.ToLower().Equals(itiEd.BatchNumber.ToLower())))
                        {
                            isAvailable = true;
                            itemBalanceDetailEd = findBalance;
                            break;
                        }
                        if (!isAvailable)
                        {
                            itemBalanceDetailEd = itemBalanceDetailEds.AddNew();
                            itemBalanceDetailEd.LocationID = locationID;
                            itemBalanceDetailEd.ItemID = item.ItemID;
                            itemBalanceDetailEd.ExpiredDate = itiEd.ExpiredDate;
                            itemBalanceDetailEd.BatchNumber = itiEd.BatchNumber;

                            if (balanceType == BalanceType.QtyIn)
                            {
                                itemBalanceDetailEd.Balance = itiEd.Quantity * itiEd.ConversionFactor;
                            }
                            else
                            {
                                itemBalanceDetailEd.Balance = 0 - (itiEd.Quantity * itiEd.ConversionFactor);
                            }

                            itemBalanceDetailEd.IsActive = true;
                            itemBalanceDetailEd.CreatedDateTime = Utils.NowAtSqlServer();
                            itemBalanceDetailEd.CreatedByUserID = userID;
                        }
                        else
                        {
                            if (balanceType == BalanceType.QtyIn)
                                itemBalanceDetailEd.Balance += Convert.ToDecimal(itiEd.Quantity * itiEd.ConversionFactor);
                            else
                                itemBalanceDetailEd.Balance -= Convert.ToDecimal(itiEd.Quantity * itiEd.ConversionFactor);
                        }

                        itemBalanceDetailEd.LastUpdateDateTime = Utils.NowAtSqlServer();
                        itemBalanceDetailEd.LastUpdateByUserID = userID;

                        #endregion

                        x += 1;

                    }
                }
                else
                {
                    var itemMovement = itemMovements.AddNew();
                    itemMovement.MovementDate = entity.ApprovedDate;
                    itemMovement.ServiceUnitID = GetServiceUnitID(entity);
                    itemMovement.LocationID = locationID;
                    itemMovement.TransactionCode = entity.TransactionCode;
                    itemMovement.TransactionNo = entity.TransactionNo;
                    itemMovement.SequenceNo = item.SequenceNo;
                    itemMovement.ItemID = item.ItemID;
                    if (item.ExpiredDate != null)
                        itemMovement.ExpiredDate = item.ExpiredDate;
                    else
                        itemMovement.ExpiredDate = Convert.ToDateTime("1/1/2999");
                    itemMovement.BatchNumber = string.IsNullOrEmpty(item.BatchNumber) ? string.Empty : item.BatchNumber;

                    var balance = balances.FindByPrimaryKey(itemMovement.LocationID, itemMovement.ItemID);

                    if (balanceType == BalanceType.QtyIn)
                    {
                        itemMovement.InitialStock = balance.Balance - (item.Quantity * item.ConversionFactor);

                        itemMovement.QuantityIn = item.Quantity * item.ConversionFactor;
                        itemMovement.QuantityOut = 0;
                    }
                    else
                    {
                        itemMovement.InitialStock = balance.Balance + (item.Quantity * item.ConversionFactor);
                        itemMovement.QuantityIn = 0;
                        itemMovement.QuantityOut = item.Quantity * item.ConversionFactor;
                    }

                    string baseUnit;
                    decimal? costPrice;
                    string parCostType = AppParameter.GetParameterValue(AppParameter.ParameterItem.ItemProductCostPriceType);

                    if (entity.TransactionCode == Reference.TransactionCode.PurchaseOrderReceive ||
                        entity.TransactionCode == Reference.TransactionCode.DirectPurchase)
                    {
                        var discount1Amount = (item.PriceInCurrency * item.Discount1Percentage) / 100;
                        var discount2Amount = ((item.PriceInCurrency - discount1Amount) * item.Discount2Percentage) / 100;
                        var priceWvat = (item.PriceInCurrency - discount1Amount - discount2Amount) * (1 + entity.TaxPercentage / 100);
                        costPrice = priceWvat / (item.ConversionFactor == 0 ? 1 : item.ConversionFactor);
                        baseUnit = item.SRItemUnit;
                    }
                    else
                    {
                        if (entity.SRItemType == ItemType.Medical)
                        {
                            var medic = new ItemProductMedic();
                            medic.LoadByPrimaryKey(itemMovement.ItemID);
                            baseUnit = item.SRItemUnit;
                            if (parCostType == "AVG")
                                costPrice = item.CostPrice * (entity.CurrencyRate ?? 1); //ga yakin ini ngaruh kemana
                            else
                            {
                                costPrice = ((item.PriceInCurrency - item.Discount) / item.ConversionFactor) * (1 + (entity.TaxPercentage / 100));
                            }
                        }
                        else if (entity.SRItemType == ItemType.NonMedical)
                        {
                            var nonMedic = new ItemProductNonMedic();
                            nonMedic.LoadByPrimaryKey(itemMovement.ItemID);
                            baseUnit = item.SRItemUnit;
                            if (parCostType == "AVG")
                                costPrice = item.CostPrice * (entity.CurrencyRate ?? 1);
                            else
                            {
                                costPrice = ((item.PriceInCurrency - item.Discount) / item.ConversionFactor) * (1 + (entity.TaxPercentage / 100));
                            }
                        }
                        else
                        {
                            var kitchen = new ItemKitchen();
                            kitchen.LoadByPrimaryKey(itemMovement.ItemID);
                            baseUnit = item.SRItemUnit;
                            if (parCostType == "AVG")
                                costPrice = item.CostPrice * (entity.CurrencyRate ?? 1);
                            else
                            {
                                costPrice = ((item.PriceInCurrency - item.Discount) / item.ConversionFactor) * (1 + (entity.TaxPercentage / 100));
                            }
                        }
                    }

                    itemMovement.SRItemUnit = baseUnit;
                    itemMovement.CostPrice = costPrice;
                    itemMovement.SalesPrice = 0; //TODO: set Sales Price
                    itemMovement.PurchasePrice = item.PriceInCurrency / (item.ConversionFactor == 0 ? 1 : item.ConversionFactor);
                    itemMovement.LastPriceInBaseUnit = (((item.PriceInCurrency ?? 0) - (item.DiscountInCurrency ?? 0)) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0))) *
                            (1 + (((entity.IsTaxable ?? 0) == 1) && (item.IsTaxable ?? false) ? (entity.TaxPercentage ?? 0) : 0) / 100);
                    itemMovement.LastUpdateByUserID = userID;
                    itemMovement.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;


                    #region ItemBalanceDetailEd

                    ItemBalanceDetailEd itemBalanceDetailEd = null;
                    var isAvailable = false;
                    foreach (var findBalance in itemBalanceDetailEds.Where(findBalance => findBalance.ItemID.Equals(item.ItemID) &&
                                                                            findBalance.ExpiredDate.Equals(itemMovement.ExpiredDate) &&
                                                                              findBalance.BatchNumber.ToLower().Equals(itemMovement.BatchNumber.ToLower())))
                    {
                        isAvailable = true;
                        itemBalanceDetailEd = findBalance;
                        break;
                    }
                    if (!isAvailable)
                    {
                        itemBalanceDetailEd = itemBalanceDetailEds.AddNew();
                        itemBalanceDetailEd.LocationID = locationID;
                        itemBalanceDetailEd.ItemID = item.ItemID;
                        itemBalanceDetailEd.ExpiredDate = itemMovement.ExpiredDate;
                        itemBalanceDetailEd.BatchNumber = itemMovement.BatchNumber;
                        if (balanceType == BalanceType.QtyIn)
                        {
                            itemBalanceDetailEd.Balance = item.Quantity * item.ConversionFactor;
                        }
                        else
                        {
                            itemBalanceDetailEd.Balance = 0 - (item.Quantity * item.ConversionFactor);
                        }

                        itemBalanceDetailEd.IsActive = true;
                        itemBalanceDetailEd.CreatedDateTime = Utils.NowAtSqlServer();
                        itemBalanceDetailEd.CreatedByUserID = userID;
                    }
                    else
                    {
                        if (balanceType == BalanceType.QtyIn)
                            itemBalanceDetailEd.Balance += Convert.ToDecimal(item.Quantity * item.ConversionFactor);
                        else
                            itemBalanceDetailEd.Balance -= Convert.ToDecimal(item.Quantity * item.ConversionFactor);
                    }
                    itemBalanceDetailEd.LastUpdateDateTime = Utils.NowAtSqlServer();
                    itemBalanceDetailEd.LastUpdateByUserID = userID;

                    #endregion
                }
            }
        }

        private static ItemMovementCollection PrepareItemMovementsForPurchaseOrderReturn(ItemTransaction entity, ItemTransactionItemCollection coll,
            ItemBalanceCollection balances, string locationID, string userID, ItemProductMedicCollection collMedic, ItemProductNonMedicCollection collNonMedic, ItemKitchenCollection collKitchen)
        {
            if (!IsNeedUpdateBalance(entity.TransactionCode))
                return null;

            var allBalances = new ItemBalanceCollection();
            allBalances.Query.Select(
                allBalances.Query.ItemID,
                allBalances.Query.Balance.Sum()
                );
            allBalances.Query.Where(allBalances.Query.ItemID.In((coll.Select(c => c.ItemID)).Distinct()));
            allBalances.Query.GroupBy(allBalances.Query.ItemID);
            allBalances.LoadAll();

            var movements = new ItemMovementCollection();

            var balanceType = GetBalanceType(entity.TransactionCode);

            var collItem = coll.OrderBy(i => i.ItemID).OrderBy(j => j.SequenceNo);

            foreach (var item in collItem)
            {
                var isInventoryItem = true;
                var baseUnitItem = string.Empty;
                switch (entity.SRItemType)
                {
                    case ItemType.Medical:
                        var ipm = new ItemProductMedic();
                        ipm.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = ipm.IsInventoryItem ?? false;
                        baseUnitItem = ipm.SRItemUnit;
                        break;
                    case ItemType.NonMedical:
                        var ipnm = new ItemProductNonMedic();
                        ipnm.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = ipnm.IsInventoryItem ?? false;
                        baseUnitItem = ipnm.SRItemUnit;
                        break;
                    case ItemType.Kitchen:
                        var ik = new ItemKitchen();
                        ik.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = ik.IsInventoryItem ?? false;
                        baseUnitItem = ik.SRItemUnit;
                        break;
                }

                if (isInventoryItem)
                {
                    var itemMovement = movements.AddNew();
                    itemMovement.MovementDate = entity.ApprovedDate.Value;
                    itemMovement.ServiceUnitID = GetServiceUnitID(entity);
                    itemMovement.LocationID = locationID;
                    itemMovement.TransactionCode = entity.TransactionCode;
                    itemMovement.TransactionNo = entity.TransactionNo;
                    itemMovement.SequenceNo = item.SequenceNo;
                    itemMovement.ItemID = item.ItemID;
                    if (item.ExpiredDate != null)
                        itemMovement.ExpiredDate = item.ExpiredDate;
                    else
                        itemMovement.str.ExpiredDate = string.Empty;

                    var balance = balances.FindByPrimaryKey(itemMovement.LocationID, itemMovement.ItemID);

                    itemMovement.InitialStock = balance.Balance + (item.Quantity * item.ConversionFactor);

                    if (balanceType == BalanceType.QtyIn)
                    {
                        itemMovement.QuantityIn = item.Quantity * item.ConversionFactor;
                        itemMovement.QuantityOut = 0;
                    }
                    else
                    {
                        itemMovement.QuantityIn = 0;
                        itemMovement.QuantityOut = item.Quantity * item.ConversionFactor;
                    }

                    string baseUnit;
                    decimal? costPrice;
                    string parCostType = AppParameter.GetParameterValue(AppParameter.ParameterItem.ItemProductCostPriceType);

                    //
                    if (entity.SRItemType == ItemType.Medical)
                    {
                        var costlist = (collMedic.Where(b => b.ItemID == item.ItemID)).Take(1).Single();
                        if (parCostType == "AVG")
                            costPrice = costlist.CostPrice;
                        else
                        {
                            costPrice = ((item.PriceInCurrency - item.DiscountInCurrency) / item.ConversionFactor) *
                                (1 + (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (entity.TaxPercentage / 100) : 0));
                        }
                    }
                    else if (entity.SRItemType == ItemType.NonMedical)
                    {
                        var costlist = (collNonMedic.Where(b => b.ItemID == item.ItemID)).Take(1).Single();
                        if (parCostType == "AVG")
                            costPrice = costlist.CostPrice;
                        else
                        {
                            costPrice = ((item.PriceInCurrency - item.DiscountInCurrency) / item.ConversionFactor) *
                                (1 + (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (entity.TaxPercentage / 100) : 0));
                        }
                    }
                    else
                    {
                        var costlist = (collKitchen.Where(b => b.ItemID == item.ItemID)).Take(1).Single();
                        if (parCostType == "AVG")
                            costPrice = costlist.CostPrice;
                        else
                        {
                            costPrice = ((item.PriceInCurrency - item.DiscountInCurrency) / item.ConversionFactor) *
                                (1 + (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (entity.TaxPercentage / 100) : 0));
                        }
                    }
                    //

                    baseUnit = item.SRItemUnit;

                    itemMovement.SRItemUnit = baseUnit;
                    itemMovement.CostPrice = costPrice;
                    itemMovement.SalesPrice = 0; //TODO: set Sales Price
                    //itemMovement.PurchasePrice = item.PriceInCurrency / (item.ConversionFactor == 0 ? 1 : item.ConversionFactor);
                    itemMovement.PurchasePrice = ((item.PriceInCurrency ?? 0) - (item.DiscountInCurrency ?? 0)) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0));
                    itemMovement.LastPriceInBaseUnit = (((item.PriceInCurrency ?? 0) - (item.DiscountInCurrency ?? 0)) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0))) *
                        (1 + (((entity.IsTaxable ?? 0) == 1) && (item.IsTaxable ?? false) ? (entity.TaxPercentage ?? 0) : 0) / 100);
                    itemMovement.LastUpdateByUserID = userID;
                    itemMovement.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                }
            }
            return movements;
        }

        private static void PrepareItemMovementsAndItemBalanceDetailEdForPurchaseOrderReturn(ItemTransaction entity, ItemTransactionItemCollection coll,
            ItemBalanceCollection balances, string locationID, string userID, ItemProductMedicCollection collMedic, ItemProductNonMedicCollection collNonMedic, ItemKitchenCollection collKitchen,
            out ItemMovementCollection itemMovements, out ItemBalanceDetailEdCollection itemBalanceDetailEds)
        {
            if (!(entity.IsInventoryItem ?? false))
            {
                itemMovements = null;
                itemBalanceDetailEds = null;
                return;
            }

            itemMovements = new ItemMovementCollection();
            itemBalanceDetailEds = new ItemBalanceDetailEdCollection();
            itemBalanceDetailEds.Query.Where(
                itemBalanceDetailEds.Query.ItemID.In(coll.Select(l => l.ItemID)),
                itemBalanceDetailEds.Query.LocationID == locationID
                );
            itemBalanceDetailEds.LoadAll();

            var collItem = coll.OrderBy(i => i.ItemID).OrderBy(j => j.IsBonusItem).OrderBy(k => k.SequenceNo);

            //di-grouping per item
            var collGroupByItem = coll.GroupBy(c => c.ItemID).Select(q => new
            {
                ItemID = q.Key,
                Quantity = q.Sum(p => (p.ConversionFactor * p.Quantity))
            });

            foreach (var item in collGroupByItem)
            {
                var isInventoryItem = true;
                var baseUnit = string.Empty;
                switch (entity.SRItemType)
                {
                    case ItemType.Medical:
                        var md = new ItemProductMedic();
                        md.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = md.IsInventoryItem ?? false;
                        baseUnit = md.SRItemUnit;
                        break;
                    case ItemType.NonMedical:
                        var nm = new ItemProductNonMedic();
                        nm.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = nm.IsInventoryItem ?? false;
                        baseUnit = nm.SRItemUnit;
                        break;
                    case ItemType.Kitchen:
                        var ik = new ItemKitchen();
                        ik.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = ik.IsInventoryItem ?? false;
                        baseUnit = ik.SRItemUnit;
                        break;
                }

                if (isInventoryItem)
                {
                    int x = 0;
                    decimal initialStock = 0;
                    decimal qtyOut = 0;

                    foreach (var findItem in collItem.Where(findItem => findItem.ItemID.Equals(item.ItemID)))
                    {
                        var seqNo = findItem.SequenceNo;

                        var itiEds = new ItemTransactionItemEdCollection();
                        itiEds.Query.Where(itiEds.Query.TransactionNo == findItem.TransactionNo, itiEds.Query.SequenceNo == findItem.SequenceNo);
                        itiEds.Query.OrderBy(itiEds.Query.ExpiredDate.Ascending, itiEds.Query.LastUpdateDateTime.Ascending);
                        itiEds.LoadAll();

                        foreach (var itiEd in itiEds)
                        {
                            #region ItemMovement

                            var itemMovement = itemMovements.AddNew();
                            itemMovement.MovementDate = entity.ApprovedDate.Value.AddMilliseconds(x * 100);
                            itemMovement.ServiceUnitID = GetServiceUnitID(entity);
                            itemMovement.LocationID = locationID;
                            itemMovement.TransactionCode = entity.TransactionCode;
                            itemMovement.TransactionNo = entity.TransactionNo;
                            itemMovement.SequenceNo = findItem.SequenceNo;
                            itemMovement.ItemID = findItem.ItemID;
                            itemMovement.ExpiredDate = itiEd.ExpiredDate;
                            itemMovement.BatchNumber = itiEd.BatchNumber;

                            if (x == 0)
                            {
                                var balance = balances.FindByPrimaryKey(itemMovement.LocationID, itemMovement.ItemID);
                                itemMovement.InitialStock = balance.Balance + item.Quantity;

                                initialStock = itemMovement.InitialStock ?? 0;
                            }
                            else
                            {
                                itemMovement.InitialStock = initialStock - qtyOut;
                            }

                            itemMovement.QuantityIn = 0;
                            itemMovement.QuantityOut = itiEd.Quantity * itiEd.ConversionFactor;

                            qtyOut += (itemMovement.QuantityOut ?? 0);

                            decimal? costPrice;
                            string parCostType = AppParameter.GetParameterValue(AppParameter.ParameterItem.ItemProductCostPriceType);

                            if (entity.SRItemType == ItemType.Medical)
                            {
                                var costlist = (collMedic.Where(b => b.ItemID == item.ItemID)).Take(1).Single();
                                costPrice = costlist.CostPrice;
                                if (parCostType == "AVG")
                                    costPrice = costlist.CostPrice;
                                else
                                {
                                    costPrice = ((findItem.PriceInCurrency - findItem.DiscountInCurrency) / findItem.ConversionFactor) *
                                        (1 + (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (entity.TaxPercentage / 100) : 0));
                                }
                            }
                            else if (entity.SRItemType == ItemType.NonMedical)
                            {
                                var costlist = (collNonMedic.Where(b => b.ItemID == item.ItemID)).Take(1).Single();
                                costPrice = costlist.CostPrice;
                                if (parCostType == "AVG")
                                    costPrice = costlist.CostPrice;
                                else
                                {
                                    costPrice = ((findItem.PriceInCurrency - findItem.DiscountInCurrency) / findItem.ConversionFactor) *
                                        (1 + (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (entity.TaxPercentage / 100) : 0));
                                }
                            }
                            else
                            {
                                var costlist = (collKitchen.Where(b => b.ItemID == item.ItemID)).Take(1).Single();
                                costPrice = costlist.CostPrice;
                                if (parCostType == "AVG")
                                    costPrice = costlist.CostPrice;
                                else
                                {
                                    costPrice = ((findItem.PriceInCurrency - findItem.DiscountInCurrency) / findItem.ConversionFactor) *
                                        (1 + (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (entity.TaxPercentage / 100) : 0));
                                }
                            }

                            itemMovement.SRItemUnit = baseUnit;
                            itemMovement.CostPrice = costPrice;
                            itemMovement.SalesPrice = 0;
                            itemMovement.PurchasePrice = ((findItem.PriceInCurrency ?? 0) - (findItem.DiscountInCurrency ?? 0)) / ((findItem.ConversionFactor ?? 0) == 0 ? 1 : (findItem.ConversionFactor ?? 0));
                            itemMovement.LastPriceInBaseUnit = (((findItem.PriceInCurrency ?? 0) - (findItem.DiscountInCurrency ?? 0)) / ((findItem.ConversionFactor ?? 0) == 0 ? 1 : (findItem.ConversionFactor ?? 0))) *
                                (1 + (((entity.IsTaxable ?? 0) == 1) && (findItem.IsTaxable ?? false) ? (entity.TaxPercentage ?? 0) : 0) / 100);
                            itemMovement.LastUpdateByUserID = userID;
                            itemMovement.LastUpdateDateTime = Utils.NowAtSqlServer();

                            #endregion

                            #region ItemBalanceDetailEd

                            ItemBalanceDetailEd itemBalanceDetailEd = null;
                            var isAvailable = false;
                            foreach (var findBalance in itemBalanceDetailEds.Where(findBalance => findBalance.ItemID.Equals(item.ItemID) &&
                                                                                    findBalance.ExpiredDate.Equals(itiEd.ExpiredDate) &&
                                                                                      findBalance.BatchNumber.ToLower().Equals(itiEd.BatchNumber.ToLower())))
                            {
                                isAvailable = true;
                                itemBalanceDetailEd = findBalance;
                                break;
                            }
                            if (!isAvailable)
                            {
                                itemBalanceDetailEd = itemBalanceDetailEds.AddNew();
                                itemBalanceDetailEd.LocationID = locationID;
                                itemBalanceDetailEd.ItemID = item.ItemID;
                                itemBalanceDetailEd.ExpiredDate = itiEd.ExpiredDate;
                                itemBalanceDetailEd.BatchNumber = itiEd.BatchNumber;
                                itemBalanceDetailEd.Balance = 0 - (itiEd.Quantity * itiEd.ConversionFactor);
                                itemBalanceDetailEd.IsActive = true;
                                itemBalanceDetailEd.CreatedDateTime = Utils.NowAtSqlServer();
                                itemBalanceDetailEd.CreatedByUserID = userID;
                            }
                            else
                            {
                                itemBalanceDetailEd.Balance -= Convert.ToDecimal(itiEd.Quantity * itiEd.ConversionFactor);
                            }
                            itemBalanceDetailEd.LastUpdateDateTime = Utils.NowAtSqlServer();
                            itemBalanceDetailEd.LastUpdateByUserID = userID;

                            #endregion

                            x += 1;

                        }
                    }
                }
            }
        }

        private static ItemMovementCollection PrepareItemMovementsForPurchaseOrderReceive(ItemTransaction entity, ItemTransactionItemCollection coll,
           ItemBalanceCollection balances, string locationID, string userID, ItemProductMedicCollection collMedic, ItemProductNonMedicCollection collNonMedic, ItemKitchenCollection collKitchen)
        {
            if (!IsNeedUpdateBalance(entity.TransactionCode))
                return null;

            var allBalances = new ItemBalanceCollection();
            allBalances.Query.Select(
                allBalances.Query.ItemID,
                allBalances.Query.Balance.Sum()
                );
            allBalances.Query.Where(allBalances.Query.ItemID.In((coll.Select(c => c.ItemID)).Distinct()));
            allBalances.Query.GroupBy(allBalances.Query.ItemID);
            allBalances.LoadAll();

            var movements = new ItemMovementCollection();

            var balanceType = GetBalanceType(entity.TransactionCode);

            var collItem = coll.OrderBy(i => i.ItemID).OrderBy(j => j.SequenceNo);

            foreach (var item in collItem)
            {
                var isInventoryItem = true;
                switch (entity.SRItemType)
                {
                    case ItemType.Medical:
                        var ipm = new ItemProductMedic();
                        ipm.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = ipm.IsInventoryItem ?? false;
                        break;
                    case ItemType.NonMedical:
                        var ipnm = new ItemProductNonMedic();
                        ipnm.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = ipnm.IsInventoryItem ?? false;
                        break;
                    case ItemType.Kitchen:
                        var ik = new ItemKitchen();
                        ik.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = ik.IsInventoryItem ?? false;
                        break;
                }

                if (isInventoryItem)
                {
                    var seqNo = item.SequenceNo;

                    var itemMovement = movements.AddNew();
                    itemMovement.MovementDate = entity.ApprovedDate;
                    itemMovement.ServiceUnitID = GetServiceUnitID(entity);
                    itemMovement.LocationID = locationID;
                    itemMovement.TransactionCode = entity.TransactionCode;
                    itemMovement.TransactionNo = entity.TransactionNo;
                    itemMovement.SequenceNo = item.SequenceNo;
                    itemMovement.ItemID = item.ItemID;
                    if (item.ExpiredDate != null)
                        itemMovement.ExpiredDate = item.ExpiredDate;
                    else
                        itemMovement.str.ExpiredDate = string.Empty;

                    var list = coll.Where(a => a.ItemID == item.ItemID);

                    decimal? qty = 0;
                    if (list.Count() > 1)
                    {
                        var list2 = coll.Where(a => a.ItemID == item.ItemID && Convert.ToInt16(a.SequenceNo) > Convert.ToInt16(seqNo));
                        qty = list2.Aggregate(qty, (current, i) => current + (i.Quantity * i.ConversionFactor));
                    }

                    var balance = balances.FindByPrimaryKey(itemMovement.LocationID, itemMovement.ItemID);
                    itemMovement.InitialStock = balance.Balance - qty - (item.Quantity * item.ConversionFactor);

                    if (balanceType == BalanceType.QtyIn)
                    {
                        itemMovement.QuantityIn = item.Quantity * item.ConversionFactor;
                        itemMovement.QuantityOut = 0;
                    }
                    else
                    {
                        itemMovement.QuantityIn = 0;
                        itemMovement.QuantityOut = item.Quantity * item.ConversionFactor;
                    }

                    string baseUnit;
                    decimal? costPrice;
                    string parCostType = AppParameter.GetParameterValue(AppParameter.ParameterItem.ItemProductCostPriceType);

                    if (entity.SRItemType == ItemType.Medical)
                    {
                        var costlist = (collMedic.Where(b => b.ItemID == item.ItemID)).Take(1).Single();
                        costPrice = costlist.CostPrice;
                        if (parCostType == "AVG")
                            costPrice = costlist.CostPrice;
                        else
                        {
                            costPrice = ((item.PriceInCurrency - item.DiscountInCurrency) / item.ConversionFactor) *
                                (1 + (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (entity.TaxPercentage / 100) : 0));
                        }
                    }
                    else if (entity.SRItemType == ItemType.NonMedical)
                    {
                        var costlist = (collNonMedic.Where(b => b.ItemID == item.ItemID)).Take(1).Single();
                        costPrice = costlist.CostPrice;
                        if (parCostType == "AVG")
                            costPrice = costlist.CostPrice;
                        else
                        {
                            costPrice = ((item.PriceInCurrency - item.DiscountInCurrency) / item.ConversionFactor) *
                                (1 + (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (entity.TaxPercentage / 100) : 0));
                        }
                    }
                    else
                    {
                        var costlist = (collKitchen.Where(b => b.ItemID == item.ItemID)).Take(1).Single();
                        costPrice = costlist.CostPrice;
                        if (parCostType == "AVG")
                            costPrice = costlist.CostPrice;
                        else
                        {
                            costPrice = ((item.PriceInCurrency - item.DiscountInCurrency) / item.ConversionFactor) *
                                (1 + (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (entity.TaxPercentage / 100) : 0));
                        }
                    }

                    baseUnit = item.SRItemUnit;

                    itemMovement.SRItemUnit = baseUnit;
                    itemMovement.CostPrice = costPrice;
                    itemMovement.SalesPrice = 0; //TODO: set Sales Price
                    //itemMovement.PurchasePrice = item.PriceInCurrency / (item.ConversionFactor == 0 ? 1 : item.ConversionFactor);
                    itemMovement.PurchasePrice = ((item.PriceInCurrency ?? 0) - (item.DiscountInCurrency ?? 0)) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0));
                    itemMovement.LastPriceInBaseUnit = (((item.PriceInCurrency ?? 0) - (item.DiscountInCurrency ?? 0)) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0))) *
                        (1 + (((entity.IsTaxable ?? 0) == 1) && (item.IsTaxable ?? false) ? (entity.TaxPercentage ?? 0) : 0) / 100);
                    itemMovement.LastUpdateByUserID = userID;
                    itemMovement.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                }
            }
            return movements;
        }

        private static void PrepareItemMovementsAndItemBalanceDetailEdForPurchaseOrderReceive(ItemTransaction entity, ItemTransactionItemCollection coll,
           ItemBalanceCollection balances, string locationID, string userID, ItemProductMedicCollection collMedic, ItemProductNonMedicCollection collNonMedic, ItemKitchenCollection collKitchen,
           out ItemMovementCollection itemMovements, out ItemBalanceDetailEdCollection itemBalanceDetailEds)
        {
            if (!(entity.IsInventoryItem ?? false))
            {
                itemMovements = null;
                itemBalanceDetailEds = null;
                return;
            }

            itemMovements = new ItemMovementCollection();
            itemBalanceDetailEds = new ItemBalanceDetailEdCollection();
            itemBalanceDetailEds.Query.Where(
                itemBalanceDetailEds.Query.LocationID == locationID,
                itemBalanceDetailEds.Query.ItemID.In(coll.Select(l => l.ItemID))
                );
            itemBalanceDetailEds.LoadAll();

            var collItem = coll.OrderBy(i => i.ItemID).OrderBy(j => j.IsBonusItem).OrderBy(k => k.SequenceNo);

            //di-grouping per item, in case ada item bonus dg item yg sama
            var collGroupByItem = coll.GroupBy(c => c.ItemID).Select(q => new
            {
                ItemID = q.Key,
                Quantity = q.Sum(p => (p.ConversionFactor * p.Quantity))
            });

            foreach (var item in collGroupByItem)
            {
                var isInventoryItem = true;
                var baseUnit = string.Empty;
                switch (entity.SRItemType)
                {
                    case ItemType.Medical:
                        var md = new ItemProductMedic();
                        md.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = md.IsInventoryItem ?? false;
                        baseUnit = md.SRItemUnit;
                        break;
                    case ItemType.NonMedical:
                        var nm = new ItemProductNonMedic();
                        nm.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = nm.IsInventoryItem ?? false;
                        baseUnit = nm.SRItemUnit;
                        break;
                    case ItemType.Kitchen:
                        var ik = new ItemKitchen();
                        ik.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = ik.IsInventoryItem ?? false;
                        baseUnit = ik.SRItemUnit;
                        break;
                }

                if (isInventoryItem)
                {
                    int x = 0;
                    decimal initialStock = 0;
                    decimal qtyIn = 0;

                    foreach (var findItem in collItem.Where(findItem => findItem.ItemID.Equals(item.ItemID)))
                    {
                        var seqNo = findItem.SequenceNo;

                        var itiEds = new ItemTransactionItemEdCollection();
                        itiEds.Query.Where(itiEds.Query.TransactionNo == findItem.TransactionNo, itiEds.Query.SequenceNo == findItem.SequenceNo);
                        itiEds.Query.OrderBy(itiEds.Query.ExpiredDate.Ascending, itiEds.Query.LastUpdateDateTime.Ascending);
                        itiEds.LoadAll();

                        foreach (var itiEd in itiEds)
                        {
                            #region ItemMovement

                            var itemMovement = itemMovements.AddNew();
                            itemMovement.MovementDate = entity.ApprovedDate.Value.AddMilliseconds(x*100);
                            itemMovement.ServiceUnitID = GetServiceUnitID(entity);
                            itemMovement.LocationID = locationID;
                            itemMovement.TransactionCode = entity.TransactionCode;
                            itemMovement.TransactionNo = entity.TransactionNo;
                            itemMovement.SequenceNo = findItem.SequenceNo;
                            itemMovement.ItemID = findItem.ItemID;
                            itemMovement.ExpiredDate = itiEd.ExpiredDate;
                            itemMovement.BatchNumber = itiEd.BatchNumber;

                            if (x == 0)
                            {
                                var balance = balances.FindByPrimaryKey(itemMovement.LocationID, itemMovement.ItemID);
                                itemMovement.InitialStock = balance.Balance - item.Quantity;

                                initialStock = itemMovement.InitialStock ?? 0;
                            }
                            else
                            {
                                itemMovement.InitialStock = initialStock + qtyIn;
                            }

                            itemMovement.QuantityIn = itiEd.Quantity * itiEd.ConversionFactor;
                            itemMovement.QuantityOut = 0;

                            qtyIn += (itemMovement.QuantityIn ?? 0);

                            decimal? costPrice;
                            string parCostType = AppParameter.GetParameterValue(AppParameter.ParameterItem.ItemProductCostPriceType);

                            if (entity.SRItemType == ItemType.Medical)
                            {
                                var costlist = (collMedic.Where(b => b.ItemID == item.ItemID)).Take(1).Single();
                                costPrice = costlist.CostPrice;
                                if (parCostType == "AVG")
                                    costPrice = costlist.CostPrice;
                                else
                                {
                                    costPrice = ((findItem.PriceInCurrency - findItem.DiscountInCurrency) / findItem.ConversionFactor) *
                                        (1 + (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (entity.TaxPercentage / 100) : 0));
                                }
                            }
                            else if (entity.SRItemType == ItemType.NonMedical)
                            {
                                var costlist = (collNonMedic.Where(b => b.ItemID == item.ItemID)).Take(1).Single();
                                costPrice = costlist.CostPrice;
                                if (parCostType == "AVG")
                                    costPrice = costlist.CostPrice;
                                else
                                {
                                    costPrice = ((findItem.PriceInCurrency - findItem.DiscountInCurrency) / findItem.ConversionFactor) *
                                        (1 + (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (entity.TaxPercentage / 100) : 0));
                                }
                            }
                            else
                            {
                                var costlist = (collKitchen.Where(b => b.ItemID == item.ItemID)).Take(1).Single();
                                costPrice = costlist.CostPrice;
                                if (parCostType == "AVG")
                                    costPrice = costlist.CostPrice;
                                else
                                {
                                    costPrice = ((findItem.PriceInCurrency - findItem.DiscountInCurrency) / findItem.ConversionFactor) *
                                        (1 + (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (entity.TaxPercentage / 100) : 0));
                                }
                            }

                            itemMovement.SRItemUnit = baseUnit;
                            itemMovement.CostPrice = costPrice;
                            itemMovement.SalesPrice = 0;
                            itemMovement.PurchasePrice = ((findItem.PriceInCurrency ?? 0) - (findItem.DiscountInCurrency ?? 0)) / ((findItem.ConversionFactor ?? 0) == 0 ? 1 : (findItem.ConversionFactor ?? 0));
                            itemMovement.LastPriceInBaseUnit = (((findItem.PriceInCurrency ?? 0) - (findItem.DiscountInCurrency ?? 0)) / ((findItem.ConversionFactor ?? 0) == 0 ? 1 : (findItem.ConversionFactor ?? 0))) *
                                (1 + (((entity.IsTaxable ?? 0) == 1) && (findItem.IsTaxable ?? false) ? (entity.TaxPercentage ?? 0) : 0) / 100);
                            itemMovement.LastUpdateByUserID = userID;
                            itemMovement.LastUpdateDateTime = Utils.NowAtSqlServer();

                            #endregion

                            #region ItemBalanceDetailEd

                            ItemBalanceDetailEd itemBalanceDetailEd = null;
                            var isAvailable = false;
                            foreach (var findBalance in itemBalanceDetailEds.Where(findBalance => findBalance.ItemID.Equals(item.ItemID) &&
                                                                                    findBalance.ExpiredDate.Equals(itiEd.ExpiredDate) &&
                                                                                      findBalance.BatchNumber.ToLower().Equals(itiEd.BatchNumber.ToLower())))
                            {
                                isAvailable = true;
                                itemBalanceDetailEd = findBalance;
                                break;
                            }
                            if (!isAvailable)
                            {
                                itemBalanceDetailEd = itemBalanceDetailEds.AddNew();
                                itemBalanceDetailEd.LocationID = locationID;
                                itemBalanceDetailEd.ItemID = item.ItemID;
                                itemBalanceDetailEd.ExpiredDate = itiEd.ExpiredDate;
                                itemBalanceDetailEd.BatchNumber = itiEd.BatchNumber;
                                itemBalanceDetailEd.Balance = itiEd.Quantity * itiEd.ConversionFactor;
                                itemBalanceDetailEd.IsActive = true;
                                itemBalanceDetailEd.CreatedDateTime = Utils.NowAtSqlServer();
                                itemBalanceDetailEd.CreatedByUserID = userID;
                            }
                            else
                            {
                                itemBalanceDetailEd.Balance += Convert.ToDecimal(itiEd.Quantity * itiEd.ConversionFactor);
                                itemBalanceDetailEd.IsActive = true;
                            }
                            itemBalanceDetailEd.LastUpdateDateTime = Utils.NowAtSqlServer();
                            itemBalanceDetailEd.LastUpdateByUserID = userID;

                            //var details = (itemBalanceDetailEds.Where(ed => ed.LocationID == locationID && ed.ItemID == findItem.ItemID && ed.ExpiredDate == itiEd.ExpiredDate && ed.BatchNumber == itiEd.BatchNumber)
                            //                .OrderByDescending(ed => ed.CreatedDateTime)).Take(1).SingleOrDefault();
                            //if (details == null)
                            //{
                            //    details = itemBalanceDetailEds.AddNew();
                            //    details.LocationID = locationID;
                            //    details.ItemID = findItem.ItemID;
                            //    details.ExpiredDate = itiEd.ExpiredDate;
                            //    details.BatchNumber = itiEd.BatchNumber;
                            //    details.Balance = itiEd.Quantity * itiEd.ConversionFactor;
                            //    details.IsActive = true;
                            //    details.CreatedDateTime = Utils.NowAtSqlServer();
                            //    details.CreatedByUserID = userID;
                            //}
                            //else
                            //{
                            //    if (details.Balance < 0)
                            //        details.Balance = (itiEd.Quantity * itiEd.ConversionFactor);
                            //    else
                            //        details.Balance += (itiEd.Quantity * itiEd.ConversionFactor);
                            //    details.IsActive = true;
                            //}

                            //details.LastUpdateDateTime = Utils.NowAtSqlServer();
                            //details.LastUpdateByUserID = userID;

                            #endregion

                            x += 1;

                        }
                    }
                }
            }
        }

        private static void PrepareItemMovementsAndItemBalanceDetailEdForSalesReturn(ItemTransaction entity, ItemTransactionItemCollection coll,
           ItemBalanceCollection balances, string locationID, string userID, ItemProductMedicCollection collMedic, ItemProductNonMedicCollection collNonMedic, ItemKitchenCollection collKitchen,
           out ItemMovementCollection itemMovements, out ItemBalanceDetailEdCollection itemBalanceDetailEds)
        {
            if (!(entity.IsInventoryItem ?? false))
            {
                itemMovements = null;
                itemBalanceDetailEds = null;
                return;
            }

            itemMovements = new ItemMovementCollection();
            itemBalanceDetailEds = new ItemBalanceDetailEdCollection();
            itemBalanceDetailEds.Query.Where(
                itemBalanceDetailEds.Query.LocationID == locationID,
                itemBalanceDetailEds.Query.ItemID.In(coll.Select(l => l.ItemID))
                );
            itemBalanceDetailEds.LoadAll();

            var collItem = coll.OrderBy(i => i.ItemID).OrderBy(j => j.IsBonusItem).OrderBy(k => k.SequenceNo);

            //di-grouping per item, in case ada item bonus dg item yg sama
            var collGroupByItem = coll.GroupBy(c => c.ItemID).Select(q => new
            {
                ItemID = q.Key,
                Quantity = q.Sum(p => (p.ConversionFactor * p.Quantity))
            });

            var serviceUnitID = entity.ToServiceUnitID;

            foreach (var item in collGroupByItem)
            {
                var isInventoryItem = true;
                var baseUnit = string.Empty;
                decimal costPrice = 0;
                decimal lastPrice = 0;
                switch (entity.SRItemType)
                {
                    case ItemType.Medical:
                        var md = new ItemProductMedic();
                        md.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = md.IsInventoryItem ?? false;
                        baseUnit = md.SRItemUnit;
                        costPrice = md.CostPrice ?? 0;
                        lastPrice = md.LastPriceInBaseUnit ?? 0;
                        break;
                    case ItemType.NonMedical:
                        var nm = new ItemProductNonMedic();
                        nm.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = nm.IsInventoryItem ?? false;
                        baseUnit = nm.SRItemUnit;
                        costPrice = nm.CostPrice ?? 0;
                        lastPrice = nm.LastPriceInBaseUnit ?? 0;
                        break;
                    case ItemType.Kitchen:
                        var ik = new ItemKitchen();
                        ik.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = ik.IsInventoryItem ?? false;
                        baseUnit = ik.SRItemUnit;
                        costPrice = ik.CostPrice ?? 0;
                        lastPrice = ik.LastPriceInBaseUnit ?? 0;
                        break;
                }

                if (isInventoryItem)
                {
                    foreach (var findItem in collItem.Where(findItem => findItem.ItemID.Equals(item.ItemID)))
                    {
                        var query = new ItemMovementQuery();
                        query.Where(query.TransactionNo == findItem.ReferenceNo, query.ItemID == findItem.ItemID);
                        query.OrderBy(query.MovementDate.Descending);

                        DataTable tbl = query.LoadDataTable();

                        var balance = balances.FindByPrimaryKey(locationID, findItem.ItemID);

                        decimal qty = Math.Abs(item.Quantity ?? 0);
                        decimal blc = (balance.Balance ?? 0) - qty;
                       
                        decimal ctr = 0;

                        int idx = 0;
                        foreach (DataRow row in tbl.Rows)
                        {
                            ctr += Math.Abs((decimal)row["QuantityOut"]);

                            if (qty > 0)
                            {
                                if (qty > Math.Abs((decimal)row["QuantityOut"]))
                                {
                                    #region movement
                                    var movement = itemMovements.AddNew();
                                    movement.MovementDate = Utils.NowAtSqlServer().AddMilliseconds(10 * idx);
                                    movement.ServiceUnitID = serviceUnitID;
                                    movement.LocationID = locationID;
                                    movement.TransactionCode = entity.TransactionCode;
                                    movement.TransactionNo = entity.TransactionNo;
                                    movement.SequenceNo = findItem.SequenceNo;
                                    movement.ItemID = findItem.ItemID;
                                    movement.ExpiredDate = Convert.ToDateTime(row["ExpiredDate"]);
                                    movement.BatchNumber = Convert.ToString(row["BatchNumber"]);

                                    movement.InitialStock = blc;
                                    movement.QuantityOut = 0;
                                    movement.QuantityIn = Math.Abs((decimal)row["QuantityOut"]);
                                    blc += (movement.QuantityIn ?? 0);
                                    qty -= (decimal)movement.QuantityIn;

                                    movement.SRItemUnit = baseUnit;
                                    movement.CostPrice = costPrice;

                                    movement.SalesPrice = (decimal)row["SalesPrice"];
                                    movement.PurchasePrice = (decimal)row["PurchasePrice"];
                                    try
                                    {
                                        movement.LastPriceInBaseUnit = (decimal)row["LastPriceInBaseUnit"];
                                    }
                                    catch (Exception)
                                    {
                                        movement.LastPriceInBaseUnit = lastPrice;
                                    }
                                    movement.LastUpdateDateTime = Utils.NowAtSqlServer();
                                    movement.LastUpdateByUserID = userID;
                                    #endregion

                                    #region balance detail ed (fefo)
                                    var details = (itemBalanceDetailEds.Where(im => im.LocationID == locationID && im.ItemID == findItem.ItemID && im.ExpiredDate == movement.ExpiredDate && im.BatchNumber.ToLower() == movement.BatchNumber.ToLower())
                                            .OrderByDescending(im => im.CreatedDateTime)).Take(1).SingleOrDefault();
                                    if (details == null)
                                    {
                                        details = itemBalanceDetailEds.AddNew();
                                        details.LocationID = locationID;
                                        details.ItemID = findItem.ItemID;
                                        details.ExpiredDate = movement.ExpiredDate;
                                        details.BatchNumber = movement.BatchNumber;
                                        details.Balance = movement.QuantityIn;
                                        details.IsActive = true;
                                        details.CreatedDateTime = Utils.NowAtSqlServer();
                                        details.CreatedByUserID = userID;
                                    }
                                    else
                                    {
                                        if (details.Balance < 0)
                                            details.Balance = movement.QuantityIn;
                                        else
                                            details.Balance += movement.QuantityIn;
                                        details.IsActive = true;
                                    }

                                    details.LastUpdateDateTime = Utils.NowAtSqlServer();
                                    details.LastUpdateByUserID = userID;
                                    #endregion

                                }
                                else
                                {
                                    #region movement
                                    var movement = itemMovements.AddNew();
                                    movement.MovementDate = Utils.NowAtSqlServer().AddMilliseconds(10 * idx);
                                    movement.ServiceUnitID = serviceUnitID;
                                    movement.LocationID = locationID;
                                    movement.TransactionCode = entity.TransactionCode;
                                    movement.TransactionNo = entity.TransactionNo;
                                    movement.SequenceNo = findItem.SequenceNo;
                                    movement.ItemID = findItem.ItemID;
                                    movement.ExpiredDate = Convert.ToDateTime(row["ExpiredDate"]);
                                    movement.BatchNumber = Convert.ToString(row["BatchNumber"]);

                                    movement.InitialStock = blc;
                                    movement.QuantityOut = 0;
                                    movement.QuantityIn = Math.Abs(qty);
                                    blc += (movement.QuantityIn ?? 0);
                                    qty = 0;

                                    movement.SRItemUnit = baseUnit;
                                    movement.CostPrice = costPrice;
                                    
                                    movement.SalesPrice = (decimal)row["SalesPrice"];
                                    movement.PurchasePrice = (decimal)row["PurchasePrice"];
                                    try
                                    {
                                        movement.LastPriceInBaseUnit = (decimal)row["LastPriceInBaseUnit"];
                                    }
                                    catch (Exception)
                                    {
                                        movement.LastPriceInBaseUnit = lastPrice;
                                    }
                                    movement.LastUpdateDateTime = Utils.NowAtSqlServer();
                                    movement.LastUpdateByUserID = userID;
                                    #endregion

                                    #region balance detail ed (fefo)
                                    var details = (itemBalanceDetailEds.Where(im => im.LocationID == locationID && im.ItemID == findItem.ItemID && im.ExpiredDate == movement.ExpiredDate && im.BatchNumber.ToLower() == movement.BatchNumber.ToLower())
                                            .OrderByDescending(im => im.CreatedDateTime)).Take(1).SingleOrDefault();
                                    if (details == null)
                                    {
                                        details = itemBalanceDetailEds.AddNew();
                                        details.LocationID = locationID;
                                        details.ItemID = findItem.ItemID;
                                        details.ExpiredDate = movement.ExpiredDate;
                                        details.BatchNumber = movement.BatchNumber;
                                        details.Balance = movement.QuantityIn;
                                        details.IsActive = true;
                                        details.CreatedDateTime = Utils.NowAtSqlServer();
                                        details.CreatedByUserID = userID;
                                    }
                                    else
                                    {
                                        if (details.Balance < 0)
                                            details.Balance = movement.QuantityIn;
                                        else
                                            details.Balance += movement.QuantityIn;
                                        details.IsActive = true;
                                    }

                                    details.LastUpdateDateTime = Utils.NowAtSqlServer();
                                    details.LastUpdateByUserID = userID;
                                    #endregion
                                }
                            }
                            idx++;
                        }
                    }
                }
            }
        }

        //private static ItemBalanceExpireCollection PrepareItemBalanceExpires(esItemTransaction entity, IEnumerable<ItemTransactionItem> coll,
        //    string locationID, string userID)
        //{
        //    if (!IsNeedUpdateBalance(entity.TransactionCode))
        //        return null;

        //    var list = coll.Where(c => c.ExpiredDate != null).GroupBy(c => new
        //    {
        //        c.ItemID,
        //        c.ExpiredDate
        //    }).Select(q => new
        //    {
        //        q.Key.ItemID,
        //        q.Key.ExpiredDate,
        //        Quantity = q.Sum(p => (p.ConversionFactor * p.Quantity))
        //    });

        //    if (!list.Any())
        //        return null;

        //    var balances = new ItemBalanceExpireCollection();
        //    balances.Query.Where(
        //        balances.Query.ItemID.In(list.Select(l => l.ItemID)),
        //        balances.Query.LocationID == locationID
        //        );
        //    balances.LoadAll();

        //    var balanceType = GetBalanceType(entity.TransactionCode);

        //    foreach (var item in list)
        //    {
        //        var isInventoryItem = true;
        //        switch (entity.SRItemType)
        //        {
        //            case ItemType.Medical:
        //                var ipm = new ItemProductMedic();
        //                ipm.LoadByPrimaryKey(item.ItemID);
        //                isInventoryItem = ipm.IsInventoryItem ?? false;
        //                break;
        //            case ItemType.NonMedical:
        //                var ipnm = new ItemProductNonMedic();
        //                ipnm.LoadByPrimaryKey(item.ItemID);
        //                isInventoryItem = ipnm.IsInventoryItem ?? false;
        //                break;
        //            case ItemType.Kitchen:
        //                var ik = new ItemKitchen();
        //                ik.LoadByPrimaryKey(item.ItemID);
        //                isInventoryItem = ik.IsInventoryItem ?? false;
        //                break;
        //        }

        //        if (isInventoryItem)
        //        {
        //            ItemBalanceExpire balance = null;
        //            var isAvailable = false;
        //            foreach (var findBalance in balances.Where(findBalance => findBalance.ItemID.Equals(item.ItemID) &&
        //                                                                      findBalance.ExpiredDate.Equals(item.ExpiredDate)))
        //            {
        //                isAvailable = true;
        //                balance = findBalance;
        //                break;
        //            }
        //            if (!isAvailable)
        //            {
        //                balance = balances.AddNew();
        //                balance.LocationID = locationID;
        //                if (item.ExpiredDate != null)
        //                    balance.ExpiredDate = item.ExpiredDate;
        //                else
        //                    balance.str.ExpiredDate = string.Empty;
        //                balance.ItemID = item.ItemID;
        //                balance.IsActive = true;
        //                if (balanceType == BalanceType.QtyIn)
        //                {
        //                    balance.QuantityIn = Convert.ToDecimal(item.Quantity);
        //                    balance.QuantityOut = 0;
        //                }
        //                else
        //                {
        //                    balance.QuantityIn = 0;
        //                    balance.QuantityOut = Convert.ToDecimal(item.Quantity);
        //                }
        //            }
        //            else
        //            {
        //                if (balanceType == BalanceType.QtyIn)
        //                    balance.QuantityIn += Convert.ToDecimal(item.Quantity);
        //                else
        //                    balance.QuantityOut += Convert.ToDecimal(item.Quantity);
        //            }
        //            balance.LastUpdateByUserID = userID;
        //            balance.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
        //        }
        //    }
        //    return balances;
        //}

        //public static ItemBalanceExpireCollection PrepareItemBalanceExpires(esItemTransaction entity, IEnumerable<ItemTransactionItemEd> coll,
        //    string locationId, string userId, string balType)
        //{
        //    if (!IsNeedUpdateBalance(entity.TransactionCode))
        //        return null;

        //    var list = coll.Where(c => c.ExpiredDate != null).GroupBy(c => new
        //    {
        //        c.ItemID,
        //        c.ExpiredDate
        //    }).Select(q => new
        //    {
        //        q.Key.ItemID,
        //        q.Key.ExpiredDate,
        //        Quantity = q.Sum(p => (p.ConversionFactor * p.Quantity))
        //    });

        //    if (!list.Any())
        //        return null;

        //    var balances = new ItemBalanceExpireCollection();
        //    balances.Query.Where(
        //        balances.Query.ItemID.In(list.Select(l => l.ItemID)),
        //        balances.Query.LocationID == locationId
        //        );
        //    balances.LoadAll();

        //    var balanceType = GetBalanceType(entity.TransactionCode);
        //    switch (balType)
        //    {
        //        case "in":
        //            balanceType = BalanceType.QtyIn;
        //            break;
        //        case "out":
        //            balanceType = BalanceType.QtyOut;
        //            break;
        //    }

        //    if (entity.TransactionCode == Reference.TransactionCode.InventoryIssueOut && entity.IsVoid == true)
        //        balanceType = BalanceType.QtyIn;

        //    foreach (var item in list)
        //    {
        //        var isInventoryItem = true;
        //        switch (entity.SRItemType)
        //        {
        //            case ItemType.Medical:
        //                var ipm = new ItemProductMedic();
        //                ipm.LoadByPrimaryKey(item.ItemID);
        //                isInventoryItem = ipm.IsInventoryItem ?? false;
        //                break;
        //            case ItemType.NonMedical:
        //                var ipnm = new ItemProductNonMedic();
        //                ipnm.LoadByPrimaryKey(item.ItemID);
        //                isInventoryItem = ipnm.IsInventoryItem ?? false;
        //                break;
        //            case ItemType.Kitchen:
        //                var ik = new ItemKitchen();
        //                ik.LoadByPrimaryKey(item.ItemID);
        //                isInventoryItem = ik.IsInventoryItem ?? false;
        //                break;
        //        }

        //        if (isInventoryItem)
        //        {
        //            ItemBalanceExpire balance = null;
        //            var isAvailable = false;
        //            foreach (var findBalance in balances.Where(findBalance => findBalance.ItemID.Equals(item.ItemID) &&
        //                                                                      findBalance.ExpiredDate.Equals(item.ExpiredDate)))
        //            {
        //                isAvailable = true;
        //                balance = findBalance;
        //                break;
        //            }
        //            if (!isAvailable)
        //            {
        //                balance = balances.AddNew();
        //                balance.LocationID = locationId;
        //                if (item.ExpiredDate != null)
        //                    balance.ExpiredDate = item.ExpiredDate;
        //                else
        //                    balance.str.ExpiredDate = string.Empty;
        //                balance.ItemID = item.ItemID;
        //                balance.IsActive = true;
        //                if (balanceType == BalanceType.QtyIn)
        //                {
        //                    balance.QuantityIn = Convert.ToDecimal(item.Quantity);
        //                    balance.QuantityOut = 0;
        //                }
        //                else
        //                {
        //                    balance.QuantityIn = 0;
        //                    balance.QuantityOut = Convert.ToDecimal(item.Quantity);
        //                }
        //            }
        //            else
        //            {
        //                if (balanceType == BalanceType.QtyIn)
        //                    balance.QuantityIn += Convert.ToDecimal(item.Quantity);
        //                else
        //                    balance.QuantityOut += Convert.ToDecimal(item.Quantity);
        //            }
        //            balance.LastUpdateByUserID = userId;
        //            balance.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
        //        }
        //    }
        //    return balances;
        //}

        private static ItemTariffCollection PrepareItemTariffs(ItemTransaction itemTransaction, string userID, decimal rounding, bool isValidateHET)
        {
            var transQuery = new ItemTransactionItemQuery("a");

            if (itemTransaction.SRItemType == ItemType.Medical)
            {
                var itemQuery = new ItemProductMedicQuery("b");
                if (isValidateHET)
                    transQuery.InnerJoin(itemQuery).On(
                        transQuery.ItemID == itemQuery.ItemID &&
                        itemQuery.HighestPriceInBasedUnit.Coalesce("0") == 0
                        );
                else
                    transQuery.InnerJoin(itemQuery).On(transQuery.ItemID == itemQuery.ItemID);
                transQuery.Select(
                    transQuery.ItemID,
                    @"<1 AS ConversionFactor>",
                    @"<(a.Quantity * a.ConversionFactor) AS Quantity>",
                    @"<(a.PriceInCurrency / a.ConversionFactor) AS PriceInCurrency>",
                    itemQuery.MarginID,
                    itemQuery.MarginPercentage,
                    itemQuery.HighestPriceInBasedUnit,
                    itemQuery.SalesDiscount,
                    transQuery.Discount1Percentage,
                    itemQuery.IsSharePurchaseDiscToPatient
                    );
            }
            else if (itemTransaction.SRItemType == ItemType.NonMedical)
            {
                var itemQuery = new ItemProductNonMedicQuery("b");
                transQuery.InnerJoin(itemQuery).On(transQuery.ItemID == itemQuery.ItemID);
                transQuery.Select(
                    transQuery.ItemID,
                    @"<1 AS ConversionFactor>",
                    @"<(a.Quantity * a.ConversionFactor) AS Quantity>",
                    @"<(a.PriceInCurrency / a.ConversionFactor) AS PriceInCurrency>",
                    itemQuery.MarginID,
                    itemQuery.MarginPercentage,
                    itemQuery.HighestPriceInBasedUnit,
                    itemQuery.SalesDiscount,
                    transQuery.Discount1Percentage,
                    itemQuery.IsSharePurchaseDiscToPatient
                    );
            }
            else
            {
                var itemQuery = new ItemKitchenQuery("b");
                transQuery.InnerJoin(itemQuery).On(transQuery.ItemID == itemQuery.ItemID);
                transQuery.Select(
                    transQuery.ItemID,
                    @"<1 AS ConversionFactor>",
                    @"<(a.Quantity * a.ConversionFactor) AS Quantity>",
                    @"<(a.PriceInCurrency / a.ConversionFactor) AS PriceInCurrency>",
                    itemQuery.MarginID,
                    itemQuery.MarginPercentage,
                    itemQuery.HighestPriceInBasedUnit,
                    @"<CAST(0 AS NUMERIC(5,2)) AS SalesDiscount>",
                    transQuery.Discount1Percentage,
                    @"<CAST(0 as bit) AS IsSharePurchaseDiscToPatient>"
                    );
            }
            transQuery.Where(transQuery.TransactionNo == itemTransaction.TransactionNo, transQuery.IsBonusItem == false);
            transQuery.OrderBy(transQuery.ItemID.Ascending, 
                (transQuery.PriceInCurrency / transQuery.ConversionFactor).Descending);

            var dtbItemTrans = transQuery.LoadDataTable();

            var itemTariffs = new ItemTariffCollection();

            var itemIdNow = string.Empty;

            foreach (DataRow row in dtbItemTrans.Rows)
            {
                if (itemIdNow == row["ItemID"].ToString())
                    continue;

                itemIdNow = row["ItemID"].ToString();

                decimal priceInBaseUnit = (decimal)row["PriceInCurrency"] / (Convert.ToDecimal(row["ConversionFactor"]) == 0 ? 1 : Convert.ToDecimal(row["ConversionFactor"]));
                decimal priceInBaseUnitWVat = priceInBaseUnit * (1 + (Convert.ToDecimal((AppParameter.GetParameterValue(AppParameter.ParameterItem.Ppn))) / 100));
                
                //if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsAllItemProductSalesPriceIncludeTax) == "No")
                //{
                //    if (itemTransaction.IsTaxable != 1)
                //        priceInBaseUnitWVat = priceInBaseUnit;
                //}

                decimal? salesDisc = 0;
                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatient).ToLower() == "yes" ||
                    (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatient).ToLower() == "no" &&
                    (bool)row["IsSharePurchaseDiscToPatient"]))
                {
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatientFull).ToLower() == "yes")
                        salesDisc = (decimal)row["Discount1Percentage"];
                    else
                        salesDisc = (decimal)row["Discount1Percentage"] < (decimal)row["SalesDiscount"]
                                         ? (decimal)row["Discount1Percentage"]
                                         : (decimal)row["SalesDiscount"];
                }

                bool isPricesRiseOrEqual; //- db 20230512: yg dicek cuma kalo ada kenaikan harga saja, yg sama tidak dicek

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.TheSellingPriceBasedOnTheHighestPrice).ToLower() == "yes")
                {
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.TheSellingPriceBasedOnTheHighestPriceByPeriod).ToLower() == "yes")
                    {
                        var top = AppParameter.GetParameterValue(AppParameter.ParameterItem.TheSellingPriceBasedOnTheHighestPricePurchPeriod).ToInt() - 1;
                        if (top <= 1)
                            top = 1;

                        var hd = new ItemTransactionQuery("hd");
                        var dt = new ItemTransactionItemQuery("dt");
                        hd.InnerJoin(dt).On(dt.TransactionNo == hd.TransactionNo);
                        hd.Where(hd.TransactionCode == "040", hd.IsApproved == true, dt.ItemID == itemIdNow, dt.IsBonusItem == false);
                        hd.OrderBy(hd.TransactionDate.Descending);
                        hd.es.Top = top;
                        hd.Select(hd.TransactionNo);

                        var lastBuy = new ItemTransactionItemQuery("a");
                        lastBuy.Select(lastBuy.ItemID, lastBuy.PriceInCurrency, lastBuy.ConversionFactor);
                        lastBuy.Where(lastBuy.TransactionNo.In(hd), lastBuy.ItemID == itemIdNow, lastBuy.IsBonusItem == false);
                        lastBuy.OrderBy((lastBuy.PriceInCurrency / lastBuy.ConversionFactor).Descending);

                        var dtb = lastBuy.LoadDataTable();
                        if (dtb.Rows.Count > 0)
                        {
                            decimal lastPriceInBaseUnit = (decimal)dtb.Rows[0]["PriceInCurrency"] / (Convert.ToDecimal(dtb.Rows[0]["ConversionFactor"]) == 0 ? 1 : Convert.ToDecimal(dtb.Rows[0]["ConversionFactor"]));
                            if (lastPriceInBaseUnit > priceInBaseUnit)
                            {
                                priceInBaseUnitWVat = lastPriceInBaseUnit * (1 + (Convert.ToDecimal((AppParameter.GetParameterValue(AppParameter.ParameterItem.Ppn))) / 100));
                            }
                        }
                        isPricesRiseOrEqual = true;
                    }
                    else
                        isPricesRiseOrEqual = (priceInBaseUnit * (1 - (salesDisc / 100))) > Convert.ToDecimal(row["HighestPriceInBasedUnit"]);
                }
                else
                    isPricesRiseOrEqual = true;

                if (isPricesRiseOrEqual)
                {
                    // harga naik atau sama
                    var tariffs = new ItemTariffCollection();
                    tariffs.Query.Where(
                        tariffs.Query.SRTariffType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffType),
                        tariffs.Query.ItemID == row["ItemID"].ToString(),
                        tariffs.Query.ClassID == AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffClass),
                        tariffs.Query.StartingDate == itemTransaction.ApprovedDate.Value.Date
                        );
                    tariffs.LoadAll();
                    tariffs.MarkAllAsDeleted();
                    tariffs.Save();

                    var tariff = itemTariffs.AddNew();
                    tariff.SRTariffType = AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffType);
                    tariff.ItemID = row["ItemID"].ToString();
                    tariff.ClassID = AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffClass);
                    tariff.StartingDate = itemTransaction.ApprovedDate.Value.Date;

                    // Price yg disimpan adalah price pembeliannya, tetapi waktu ambil untuk jual
                    // harus di markup dulu, proses markupnya di pindah ke saat query untuk penjualan
                    tariff.Price = priceInBaseUnitWVat;
                    tariff.DiscPercentage = salesDisc;
                    tariff.Ppn = Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.Ppn));
                    tariff.ReferenceNo = itemTransaction.TransactionNo;
                    tariff.ReferenceTransactionCode = Reference.TransactionCode.PurchaseOrderReceive;
                    tariff.LastUpdateByUserID = userID;
                    tariff.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;

                    #region - db 20230512 - sudah gak dipake lagi
                    //- db 20230512 - sudah gak dipake lagi
                    //var irQ = new ItemServiceQuery();
                    //irQ.Select(irQ.ItemID, irQ.QtyDivider);
                    //irQ.Where(irQ.ItemRelatedID == tariff.ItemID);
                    //var dtir = irQ.LoadDataTable();
                    //if (dtir.Rows.Count > 0)
                    //{
                    //    var irId = dtir.Rows[0]["ItemID"].ToString();
                    //    var irQty = Convert.ToDecimal(dtir.Rows[0]["QtyDivider"]);
                    //    var basePrice = priceInBaseUnitWVat;
                    //    basePrice = basePrice - (basePrice * (salesDisc ?? 0) / 100);

                    //    var marginID = string.Empty;
                    //    decimal marginPercent = 0, fixedPrice = 0, hetPrice = 0;

                    //    var ip = new Item();
                    //    ip.LoadByPrimaryKey(tariff.ItemID);

                    //    if (ip.SRItemType == ItemType.Medical)
                    //    {
                    //        var medic = new ItemProductMedic();
                    //        if (medic.LoadByPrimaryKey(tariff.ItemID))
                    //        {
                    //            marginID = medic.str.MarginID;
                    //            marginPercent = medic.MarginPercentage ?? 0;
                    //            fixedPrice = medic.SalesFixedPrice ?? 0;
                    //            hetPrice = medic.HET ?? 0;
                    //        }
                    //    }
                    //    else if (ip.SRItemType == ItemType.NonMedical)
                    //    {
                    //        var nonMedic = new ItemProductNonMedic();
                    //        if (nonMedic.LoadByPrimaryKey(tariff.ItemID))
                    //        {
                    //            marginID = nonMedic.str.MarginID;
                    //            marginPercent = nonMedic.MarginPercentage ?? 0;
                    //            fixedPrice = nonMedic.SalesFixedPrice ?? 0;
                    //        }
                    //    }
                    //    else if (ip.SRItemType == ItemType.Kitchen)
                    //    {
                    //        var kitchen = new ItemKitchen();
                    //        if (kitchen.LoadByPrimaryKey(tariff.ItemID))
                    //        {
                    //            marginID = kitchen.str.MarginID;
                    //            marginPercent = kitchen.MarginPercentage ?? 0;
                    //            fixedPrice = kitchen.SalesFixedPrice ?? 0;
                    //        }
                    //    }

                    //    if (fixedPrice > 0)
                    //        basePrice = fixedPrice;
                    //    else
                    //    {
                    //        if (marginPercent > 0)
                    //            basePrice += ((marginPercent / 100) * basePrice);
                    //        else
                    //        {
                    //            var margin = new ItemProductMargin();
                    //            if (margin.LoadByPrimaryKey(marginID))
                    //            {
                    //                var value = new ItemProductMarginValueCollection();
                    //                value.Query.Where(value.Query.MarginID == marginID);
                    //                value.Query.OrderBy(value.Query.SequenceNo.Ascending);
                    //                value.LoadAll();

                    //                foreach (var entity in value.Where(entity => basePrice >= entity.StartingValue &&
                    //                                                             basePrice <= entity.EndingValue))
                    //                {
                    //                    marginPercent = Convert.ToDecimal(entity.MarginPercentage);
                    //                    break;
                    //                }
                    //            }
                    //            basePrice += ((marginPercent / 100) * basePrice);
                    //        }
                    //    }

                    //    if ((hetPrice > 0) && (basePrice > hetPrice))
                    //        basePrice = hetPrice;

                    //    decimal? newPrice = Rounding(basePrice / irQty, rounding);

                    //    var tariffRels = new ItemTariffCollection();
                    //    tariffRels.Query.Where(
                    //        tariffRels.Query.SRTariffType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffType),
                    //        tariffRels.Query.ItemID == irId,
                    //        tariffRels.Query.ClassID == AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffClass),
                    //        tariffRels.Query.StartingDate == itemTransaction.ApprovedDate.Value.Date
                    //        );
                    //    tariffRels.LoadAll();

                    //    ItemTariff tariffRel = null;
                    //    var isRelAvailable = false;
                    //    foreach (var findTariff in tariffRels.Where(findTariff => findTariff.ItemID.Equals(irId)))
                    //    {
                    //        isRelAvailable = true;
                    //        tariffRel = findTariff;
                    //        break;
                    //    }
                    //    if (!isRelAvailable)
                    //    {
                    //        tariffRel = itemTariffs.AddNew();
                    //        tariffRel.SRTariffType = AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffType);
                    //        tariffRel.ItemID = irId;
                    //        tariffRel.ClassID = AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffClass);
                    //        tariffRel.StartingDate = itemTransaction.ApprovedDate.Value.Date;
                    //    }

                    //    tariffRel.Price = newPrice;
                    //    tariffRel.ReferenceNo = itemTransaction.TransactionNo;
                    //    tariffRel.ReferenceTransactionCode = Reference.TransactionCode.PurchaseOrderReceive;
                    //    tariffRel.LastUpdateByUserID = userID;
                    //    tariffRel.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                    //}
#endregion

                }

                #region - db 20230512: sudah gak diperlukan lagi karena HighestPriceInBasedUnit sudah nett (price-discount)
                //- db 20230512: sudah gak diperlukan lagi karena HighestPriceInBasedUnit sudah nett (price-discount)
                //else
                //{
                //    // harga turun
                //    // cek apakah diskon pembelian akan dibagi ke pasien (ikut perhitungan harga jual)
                //    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatient) == "Yes" ||
                //        (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatient) == "No" &&
                //        (bool)row["IsSharePurchaseDiscToPatient"]))
                //    {
                //        //cek harga - diskon, jika hasilny lebih kecil dari harga - diskon skr, insert
                //        decimal? priceInBaseUnitNew = priceInBaseUnit - (priceInBaseUnit * (decimal)row["Discount1Percentage"] / 100);
                //        decimal? priceInBaseUnitOld = (decimal)row["HighestPriceInBasedUnit"] -
                //                                      ((decimal)row["HighestPriceInBasedUnit"] *
                //                                       (decimal)row["SalesDiscount"] / 100);

                //        if (priceInBaseUnitNew > priceInBaseUnitOld)
                //        {
                //            salesDisc = (decimal)row["Discount1Percentage"];

                //            var tariffs = new ItemTariffCollection();
                //            tariffs.Query.Where(
                //                tariffs.Query.SRTariffType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffType),
                //                tariffs.Query.ItemID == row["ItemID"].ToString(),
                //                tariffs.Query.ClassID == AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffClass),
                //                tariffs.Query.StartingDate == itemTransaction.ApprovedDate.Value.Date
                //                );
                //            tariffs.LoadAll();

                //            ItemTariff tariff = null;
                //            var isAvailable = false;
                //            foreach (var findTariff in tariffs.Where(findTariff => findTariff.ItemID.Equals(row["ItemID"].ToString())))
                //            {
                //                isAvailable = true;
                //                tariff = findTariff;
                //                break;
                //            }
                //            if (!isAvailable)
                //            {
                //                tariff = itemTariffs.AddNew();
                //                tariff.SRTariffType = AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffType);
                //                tariff.ItemID = row["ItemID"].ToString();
                //                tariff.ClassID = AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffClass);
                //                tariff.StartingDate = itemTransaction.ApprovedDate.Value.Date;
                //            }
                //            // Price yg disimpan adalah price pembeliannya, tetapi waktu ambil untuk jual
                //            // harus di markup dulu, proses markupnya di pindah ke saat query untuk penjualan
                //            tariff.Price = priceInBaseUnitWVat;
                //            tariff.DiscPercentage = salesDisc;
                //            tariff.Ppn = Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.Ppn));
                //            tariff.ReferenceNo = itemTransaction.TransactionNo;
                //            tariff.ReferenceTransactionCode = Reference.TransactionCode.PurchaseOrderReceive;
                //            tariff.LastUpdateByUserID = userID;
                //            tariff.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;

                //            var irQ = new ItemServiceQuery();
                //            irQ.Select(irQ.ItemID, irQ.QtyDivider);
                //            irQ.Where(irQ.ItemRelatedID == tariff.ItemID);
                //            var dtir = irQ.LoadDataTable();
                //            if (dtir.Rows.Count > 0)
                //            {
                //                var irId = dtir.Rows[0]["ItemID"].ToString();
                //                var irQty = Convert.ToDecimal(dtir.Rows[0]["QtyDivider"]);
                //                var basePrice = priceInBaseUnitWVat;
                //                basePrice = basePrice - (basePrice * (salesDisc ?? 0) / 100);

                //                var marginID = string.Empty;
                //                decimal marginPercent = 0, fixedPrice = 0, hetPrice = 0;

                //                var ip = new Item();
                //                ip.LoadByPrimaryKey(tariff.ItemID);

                //                if (ip.SRItemType == ItemType.Medical)
                //                {
                //                    var medic = new ItemProductMedic();
                //                    if (medic.LoadByPrimaryKey(tariff.ItemID))
                //                    {
                //                        marginID = medic.str.MarginID;
                //                        marginPercent = medic.MarginPercentage ?? 0;
                //                        fixedPrice = medic.SalesFixedPrice ?? 0;
                //                        hetPrice = medic.HET ?? 0;
                //                    }
                //                }
                //                else if (ip.SRItemType == ItemType.NonMedical)
                //                {
                //                    var nonMedic = new ItemProductNonMedic();
                //                    if (nonMedic.LoadByPrimaryKey(tariff.ItemID))
                //                    {
                //                        marginID = nonMedic.str.MarginID;
                //                        marginPercent = nonMedic.MarginPercentage ?? 0;
                //                        fixedPrice = nonMedic.SalesFixedPrice ?? 0;
                //                    }
                //                }
                //                else if (ip.SRItemType == ItemType.Kitchen)
                //                {
                //                    var kitchen = new ItemKitchen();
                //                    if (kitchen.LoadByPrimaryKey(tariff.ItemID))
                //                    {
                //                        marginID = kitchen.str.MarginID;
                //                        marginPercent = kitchen.MarginPercentage ?? 0;
                //                        fixedPrice = kitchen.SalesFixedPrice ?? 0;
                //                    }
                //                }

                //                if (fixedPrice > 0)
                //                    basePrice = fixedPrice;
                //                else
                //                {
                //                    if (marginPercent > 0)
                //                        basePrice += ((marginPercent / 100) * basePrice);
                //                    else
                //                    {
                //                        var margin = new ItemProductMargin();
                //                        if (margin.LoadByPrimaryKey(marginID))
                //                        {
                //                            var value = new ItemProductMarginValueCollection();
                //                            value.Query.Where(value.Query.MarginID == marginID);
                //                            value.Query.OrderBy(value.Query.SequenceNo.Ascending);
                //                            value.LoadAll();

                //                            foreach (var entity in value.Where(entity => basePrice >= entity.StartingValue &&
                //                                                                         basePrice <= entity.EndingValue))
                //                            {
                //                                marginPercent = Convert.ToDecimal(entity.MarginPercentage);
                //                                break;
                //                            }
                //                        }
                //                        basePrice += ((marginPercent / 100) * basePrice);
                //                    }
                //                }

                //                if ((hetPrice > 0) && (basePrice > hetPrice))
                //                    basePrice = hetPrice;

                //                decimal? newPrice = Rounding(basePrice / irQty, rounding);

                //                var tariffRels = new ItemTariffCollection();
                //                tariffRels.Query.Where(
                //                    tariffRels.Query.SRTariffType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffType),
                //                    tariffRels.Query.ItemID == irId,
                //                    tariffRels.Query.ClassID == AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffClass),
                //                    tariffRels.Query.StartingDate == itemTransaction.ApprovedDate.Value.Date
                //                    );
                //                tariffRels.LoadAll();

                //                ItemTariff tariffRel = null;
                //                var isRelAvailable = false;
                //                foreach (var findTariff in tariffRels.Where(findTariff => findTariff.ItemID.Equals(irId)))
                //                {
                //                    isRelAvailable = true;
                //                    tariffRel = findTariff;
                //                    break;
                //                }
                //                if (!isRelAvailable)
                //                {
                //                    tariffRel = itemTariffs.AddNew();
                //                    tariffRel.SRTariffType = AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffType);
                //                    tariffRel.ItemID = irId;
                //                    tariffRel.ClassID = AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffClass);
                //                    tariffRel.StartingDate = itemTransaction.ApprovedDate.Value.Date;
                //                }

                //                tariffRel.Price = newPrice;
                //                tariffRel.ReferenceNo = itemTransaction.TransactionNo;
                //                tariffRel.ReferenceTransactionCode = Reference.TransactionCode.PurchaseOrderReceive;
                //                tariffRel.LastUpdateByUserID = userID;
                //                tariffRel.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                //            }
                //        }
                //    }
                //}
                #endregion
            }

            return itemTariffs;
        }

        private static ItemTariffComponentCollection PrepareItemTariffComponents(ItemTransaction itemTransaction, string userID, decimal rounding, bool isValidateHET)
        {
            var transQuery = new ItemTransactionItemQuery("a");

            if (itemTransaction.SRItemType == ItemType.Medical)
            {
                var itemQuery = new ItemProductMedicQuery("b");
                if (isValidateHET)
                    transQuery.InnerJoin(itemQuery).On(
                        transQuery.ItemID == itemQuery.ItemID &&
                        itemQuery.HighestPriceInBasedUnit.Coalesce("0") == 0
                        );
                else
                    transQuery.InnerJoin(itemQuery).On(transQuery.ItemID == itemQuery.ItemID);
                transQuery.Select(
                    transQuery.ItemID,
                    //transQuery.ExpiredDate,
                    //transQuery.SequenceNo,
                    @"<1 AS ConversionFactor>",
                    @"<SUM(a.Quantity * a.ConversionFactor) AS Quantity>",
                    @"<SUM(a.PriceInCurrency / a.ConversionFactor) AS PriceInCurrency>",
                    //transQuery.ConversionFactor,
                    //transQuery.Quantity,
                    //transQuery.PriceInCurrency,
                    itemQuery.MarginID,
                    itemQuery.MarginPercentage,
                    itemQuery.HighestPriceInBasedUnit,
                    itemQuery.SalesDiscount,
                    transQuery.Discount1Percentage,
                    itemQuery.IsSharePurchaseDiscToPatient
                    );
                transQuery.GroupBy(
                    @"<(a.ItemID)>",
                    @"<(a.PriceInCurrency / a.ConversionFactor)>",
                    itemQuery.MarginID,
                    itemQuery.MarginPercentage,
                    itemQuery.HighestPriceInBasedUnit,
                    itemQuery.SalesDiscount,
                    transQuery.Discount1Percentage,
                    itemQuery.IsSharePurchaseDiscToPatient);
            }
            else if (itemTransaction.SRItemType == ItemType.NonMedical)
            {
                var itemQuery = new ItemProductNonMedicQuery("b");
                transQuery.InnerJoin(itemQuery).On(transQuery.ItemID == itemQuery.ItemID);
                transQuery.Select(
                    transQuery.ItemID,
                    //transQuery.ExpiredDate,
                    //transQuery.SequenceNo,
                    @"<1 AS ConversionFactor>",
                    @"<SUM(a.Quantity * a.ConversionFactor) AS Quantity>",
                    @"<SUM(a.PriceInCurrency / a.ConversionFactor) AS PriceInCurrency>",
                    //transQuery.ConversionFactor,
                    //transQuery.Quantity,
                    //transQuery.PriceInCurrency,
                    itemQuery.MarginID,
                    itemQuery.MarginPercentage,
                    itemQuery.HighestPriceInBasedUnit,
                    itemQuery.SalesDiscount,
                    transQuery.Discount1Percentage,
                    itemQuery.IsSharePurchaseDiscToPatient
                    );
                transQuery.GroupBy(
                    @"<(a.ItemID)>",
                    @"<(a.PriceInCurrency / a.ConversionFactor)>",
                    itemQuery.MarginID,
                    itemQuery.MarginPercentage,
                    itemQuery.HighestPriceInBasedUnit,
                    itemQuery.SalesDiscount,
                    transQuery.Discount1Percentage,
                    itemQuery.IsSharePurchaseDiscToPatient);
            }
            else
            {
                var itemQuery = new ItemKitchenQuery("b");
                transQuery.InnerJoin(itemQuery).On(transQuery.ItemID == itemQuery.ItemID);
                transQuery.Select(
                    transQuery.ItemID,
                    //transQuery.ExpiredDate,
                    //transQuery.SequenceNo,
                    @"<1 AS ConversionFactor>",
                    @"<SUM(a.Quantity * a.ConversionFactor) AS Quantity>",
                    @"<SUM(a.PriceInCurrency / a.ConversionFactor) AS PriceInCurrency>",
                    //transQuery.ConversionFactor,
                    //transQuery.Quantity,
                    //transQuery.PriceInCurrency,
                    itemQuery.MarginID,
                    itemQuery.MarginPercentage,
                    itemQuery.HighestPriceInBasedUnit,
                    @"<CAST(0 AS NUMERIC(5,2)) AS SalesDiscount>",
                    transQuery.Discount1Percentage,
                    @"<CAST(0 as bit) AS IsSharePurchaseDiscToPatient>"
                    );
                transQuery.GroupBy(
                    @"<(a.ItemID)>",
                    @"<(a.PriceInCurrency / a.ConversionFactor)>",
                    itemQuery.MarginID,
                    itemQuery.MarginPercentage,
                    itemQuery.HighestPriceInBasedUnit,
                    transQuery.Discount1Percentage);
            }
            transQuery.Where(transQuery.TransactionNo == itemTransaction.TransactionNo, transQuery.IsBonusItem == false);
            //transQuery.OrderBy(transQuery.ItemID.Ascending, transQuery.Price.Descending);
            var dtbItemTrans = transQuery.LoadDataTable();

            var itemTariffComponents = new ItemTariffComponentCollection();

            var itemIdNow = string.Empty;

            foreach (DataRow row in dtbItemTrans.Rows)
            {
                if (itemIdNow == row["ItemID"].ToString())
                    continue;

                itemIdNow = row["ItemID"].ToString();

                decimal priceInBaseUnit = (decimal)row["PriceInCurrency"] / (Convert.ToDecimal(row["ConversionFactor"]) == 0 ? 1 : Convert.ToDecimal(row["ConversionFactor"]));
                decimal priceInBaseUnitWVat = priceInBaseUnit * (1 + (Convert.ToDecimal((AppParameter.GetParameterValue(AppParameter.ParameterItem.Ppn))) / 100));

                //if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsAllItemProductSalesPriceIncludeTax) == "No")
                //{
                //    if (itemTransaction.IsTaxable != 1)
                //        priceInBaseUnitWVat = priceInBaseUnit;
                //}

                decimal? salesDisc = 0;
                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatient) == "Yes" ||
                    (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatient) == "No" &&
                    (bool)row["IsSharePurchaseDiscToPatient"]))
                {
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatientFull) == "Yes")
                        salesDisc = (decimal)row["Discount1Percentage"];
                    else
                        salesDisc = (decimal)row["Discount1Percentage"] < (decimal)row["SalesDiscount"]
                                         ? (decimal)row["Discount1Percentage"]
                                         : (decimal)row["SalesDiscount"];
                }

                bool isPricesRiseOrEqual;

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.TheSellingPriceBasedOnTheHighestPrice) == "Yes")
                    isPricesRiseOrEqual = (priceInBaseUnit * (1 - (salesDisc / 100))) > Convert.ToDecimal(row["HighestPriceInBasedUnit"]);
                else
                    isPricesRiseOrEqual = true;

                if (isPricesRiseOrEqual)
                {
                    //harga naik atau sama
                    var irQ = new ItemServiceQuery();
                    irQ.Select(irQ.ItemID, irQ.QtyDivider);
                    irQ.Where(irQ.ItemRelatedID == row["ItemID"].ToString());
                    var dtir = irQ.LoadDataTable();
                    if (dtir.Rows.Count > 0)
                    {
                        var irId = dtir.Rows[0]["ItemID"].ToString();
                        var irQty = Convert.ToDecimal(dtir.Rows[0]["QtyDivider"]);
                        var basePrice = priceInBaseUnitWVat;
                        basePrice = basePrice - (basePrice * (salesDisc ?? 0) / 100);

                        var marginID = string.Empty;
                        decimal marginPercent = 0, fixedPrice = 0, hetPrice = 0;

                        var ip = new Item();
                        ip.LoadByPrimaryKey(row["ItemID"].ToString());

                        if (ip.SRItemType == ItemType.Medical)
                        {
                            var medic = new ItemProductMedic();
                            if (medic.LoadByPrimaryKey(row["ItemID"].ToString()))
                            {
                                marginID = medic.str.MarginID;
                                marginPercent = medic.MarginPercentage ?? 0;
                                fixedPrice = medic.SalesFixedPrice ?? 0;
                                hetPrice = medic.HET ?? 0;
                            }
                        }
                        else if (ip.SRItemType == ItemType.NonMedical)
                        {
                            var nonMedic = new ItemProductNonMedic();
                            if (nonMedic.LoadByPrimaryKey(row["ItemID"].ToString()))
                            {
                                marginID = nonMedic.str.MarginID;
                                marginPercent = nonMedic.MarginPercentage ?? 0;
                                fixedPrice = nonMedic.SalesFixedPrice ?? 0;
                            }
                        }
                        else if (ip.SRItemType == ItemType.Kitchen)
                        {
                            var kitchen = new ItemKitchen();
                            if (kitchen.LoadByPrimaryKey(row["ItemID"].ToString()))
                            {
                                marginID = kitchen.str.MarginID;
                                marginPercent = kitchen.MarginPercentage ?? 0;
                                fixedPrice = kitchen.SalesFixedPrice ?? 0;
                            }
                        }

                        if (fixedPrice > 0)
                            basePrice = fixedPrice;
                        else
                        {
                            if (marginPercent > 0)
                                basePrice += ((marginPercent / 100) * basePrice);
                            else
                            {
                                var margin = new ItemProductMargin();
                                if (margin.LoadByPrimaryKey(marginID))
                                {
                                    var value = new ItemProductMarginValueCollection();
                                    value.Query.Where(value.Query.MarginID == marginID);
                                    value.Query.OrderBy(value.Query.SequenceNo.Ascending);
                                    value.LoadAll();

                                    foreach (var entity in value.Where(entity => basePrice >= entity.StartingValue &&
                                                                                 basePrice <= entity.EndingValue))
                                    {
                                        marginPercent = Convert.ToDecimal(entity.MarginPercentage);
                                        break;
                                    }
                                }
                                basePrice += ((marginPercent / 100) * basePrice);
                            }
                        }

                        if ((hetPrice > 0) && (basePrice > hetPrice))
                            basePrice = hetPrice;

                        decimal? newPrice = Rounding(basePrice / irQty, rounding);

                        var tariffComps = new ItemTariffComponentCollection();
                        tariffComps.Query.Where(
                            tariffComps.Query.SRTariffType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffType),
                            tariffComps.Query.ItemID == irId,
                            tariffComps.Query.ClassID == AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffClass),
                            tariffComps.Query.StartingDate == itemTransaction.ApprovedDate.Value.Date,
                            tariffComps.Query.TariffComponentID == AppParameter.GetParameterValue(AppParameter.ParameterItem.TariffComponentBhp)
                            );
                        tariffComps.LoadAll();

                        ItemTariffComponent tariffCompRel = null;
                        var isAvailable = false;
                        foreach (var findTariff in tariffComps.Where(findTariff => findTariff.ItemID.Equals(irId)))
                        {
                            isAvailable = true;
                            tariffCompRel = findTariff;
                            break;
                        }
                        if (!isAvailable)
                        {
                            tariffCompRel = itemTariffComponents.AddNew();
                            tariffCompRel.SRTariffType = AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffType);
                            tariffCompRel.ItemID = irId;
                            tariffCompRel.ClassID = AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffClass);
                            tariffCompRel.StartingDate = itemTransaction.ApprovedDate.Value.Date;
                            tariffCompRel.TariffComponentID = AppParameter.GetParameterValue(AppParameter.ParameterItem.TariffComponentBhp);
                        }

                        tariffCompRel.Price = newPrice;
                        tariffCompRel.IsAllowDiscount = false;
                        tariffCompRel.IsAllowVariable = false;
                        tariffCompRel.LastUpdateByUserID = userID;
                        tariffCompRel.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                    }
                }
                //else
                //{
                //    // harga turun
                //    // cek apakah diskon pembelian akan dibagi ke pasien (ikut perhitungan harga jual)
                //    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatient) == "Yes" ||
                //        (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatient) == "No" &&
                //        (bool)row["IsSharePurchaseDiscToPatient"]))
                //    {
                //        //cek harga - diskon, jika hasilny lebih kecil dari harga - diskon skr, insert
                //        decimal? priceInBaseUnitNew = priceInBaseUnit -
                //                                      (priceInBaseUnit * (decimal)row["Discount1Percentage"] / 100);
                //        decimal? priceInBaseUnitOld = (decimal)row["HighestPriceInBasedUnit"] -
                //                                      ((decimal)row["HighestPriceInBasedUnit"] *
                //                                       (decimal)row["SalesDiscount"] / 100);
                //        if (priceInBaseUnitNew > priceInBaseUnitOld)
                //        {
                //            salesDisc = (decimal)row["Discount1Percentage"];

                //            var irQ = new ItemServiceQuery();
                //            irQ.Select(irQ.ItemID, irQ.QtyDivider);
                //            irQ.Where(irQ.ItemRelatedID == row["ItemID"].ToString());
                //            var dtir = irQ.LoadDataTable();
                //            if (dtir.Rows.Count > 0)
                //            {
                //                var irId = dtir.Rows[0]["ItemID"].ToString();
                //                var irQty = Convert.ToDecimal(dtir.Rows[0]["QtyDivider"]);
                //                var basePrice = priceInBaseUnitWVat;
                //                basePrice = basePrice - (basePrice * (salesDisc ?? 0) / 100);

                //                var marginID = string.Empty;
                //                decimal marginPercent = 0, fixedPrice = 0, hetPrice = 0;

                //                var ip = new Item();
                //                ip.LoadByPrimaryKey(row["ItemID"].ToString());

                //                if (ip.SRItemType == ItemType.Medical)
                //                {
                //                    var medic = new ItemProductMedic();
                //                    if (medic.LoadByPrimaryKey(row["ItemID"].ToString()))
                //                    {
                //                        marginID = medic.str.MarginID;
                //                        marginPercent = medic.MarginPercentage ?? 0;
                //                        fixedPrice = medic.SalesFixedPrice ?? 0;
                //                        hetPrice = medic.HET ?? 0;
                //                    }
                //                }
                //                else if (ip.SRItemType == ItemType.NonMedical)
                //                {
                //                    var nonMedic = new ItemProductNonMedic();
                //                    if (nonMedic.LoadByPrimaryKey(row["ItemID"].ToString()))
                //                    {
                //                        marginID = nonMedic.str.MarginID;
                //                        marginPercent = nonMedic.MarginPercentage ?? 0;
                //                        fixedPrice = nonMedic.SalesFixedPrice ?? 0;
                //                    }
                //                }
                //                else if (ip.SRItemType == ItemType.Kitchen)
                //                {
                //                    var kitchen = new ItemKitchen();
                //                    if (kitchen.LoadByPrimaryKey(row["ItemID"].ToString()))
                //                    {
                //                        marginID = kitchen.str.MarginID;
                //                        marginPercent = kitchen.MarginPercentage ?? 0;
                //                        fixedPrice = kitchen.SalesFixedPrice ?? 0;
                //                    }
                //                }

                //                if (fixedPrice > 0)
                //                    basePrice = fixedPrice;
                //                else
                //                {
                //                    if (marginPercent > 0)
                //                        basePrice += ((marginPercent / 100) * basePrice);
                //                    else
                //                    {
                //                        var margin = new ItemProductMargin();
                //                        if (margin.LoadByPrimaryKey(marginID))
                //                        {
                //                            var value = new ItemProductMarginValueCollection();
                //                            value.Query.Where(value.Query.MarginID == marginID);
                //                            value.Query.OrderBy(value.Query.SequenceNo.Ascending);
                //                            value.LoadAll();

                //                            foreach (var entity in value.Where(entity => basePrice >= entity.StartingValue &&
                //                                                                         basePrice <= entity.EndingValue))
                //                            {
                //                                marginPercent = Convert.ToDecimal(entity.MarginPercentage);
                //                                break;
                //                            }
                //                        }
                //                        basePrice += ((marginPercent / 100) * basePrice);
                //                    }
                //                }

                //                if ((hetPrice > 0) && (basePrice > hetPrice))
                //                    basePrice = hetPrice;

                //                decimal? newPrice = Rounding(basePrice / irQty, rounding);

                //                var tariffComps = new ItemTariffComponentCollection();
                //                tariffComps.Query.Where(
                //                    tariffComps.Query.SRTariffType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffType),
                //                    tariffComps.Query.ItemID == irId,
                //                    tariffComps.Query.ClassID == AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffClass),
                //                    tariffComps.Query.StartingDate == itemTransaction.ApprovedDate.Value.Date,
                //                    tariffComps.Query.TariffComponentID == AppParameter.GetParameterValue(AppParameter.ParameterItem.TariffComponentBhp)
                //                    );
                //                tariffComps.LoadAll();

                //                ItemTariffComponent tariffCompRel = null;
                //                var isAvailable = false;
                //                foreach (var findTariff in tariffComps.Where(findTariff => findTariff.ItemID.Equals(irId)))
                //                {
                //                    isAvailable = true;
                //                    tariffCompRel = findTariff;
                //                    break;
                //                }
                //                if (!isAvailable)
                //                {
                //                    tariffCompRel = itemTariffComponents.AddNew();
                //                    tariffCompRel.SRTariffType = AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffType);
                //                    tariffCompRel.ItemID = irId;
                //                    tariffCompRel.ClassID = AppParameter.GetParameterValue(AppParameter.ParameterItem.DefaultTariffClass);
                //                    tariffCompRel.StartingDate = itemTransaction.ApprovedDate.Value.Date;
                //                    tariffCompRel.TariffComponentID = AppParameter.GetParameterValue(AppParameter.ParameterItem.TariffComponentBhp);
                //                }

                //                tariffCompRel.Price = newPrice;
                //                tariffCompRel.IsAllowDiscount = false;
                //                tariffCompRel.IsAllowVariable = false;
                //                tariffCompRel.LastUpdateByUserID = userID;
                //                tariffCompRel.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                //            }

                //        }
                //    }
                //}
            }
            return itemTariffComponents;
        }

        private static ItemBalanceByPeriodCollection PrepareItemBalanceByPeriods(esItemTransaction entity, IEnumerable<ItemTransactionItem> coll,
            string locationID, string userID)
        {
            if (!IsNeedUpdateBalance(entity.TransactionCode))
                return null;

            var list = coll.GroupBy(c => c.ItemID).Select(q => new
            {
                ItemID = q.Key,
                Quantity = q.Sum(p => (p.ConversionFactor * p.Quantity))
            });

            var balances = new ItemBalanceByPeriodCollection();
            balances.Query.Where(
                balances.Query.ItemID.In(list.Select(l => l.ItemID)),
                balances.Query.LocationID == locationID,
                balances.Query.PeriodYear == entity.ApprovedDate.Value.Year,
                balances.Query.PeriodMonth == entity.ApprovedDate.Value.Month
                );
            balances.LoadAll();

            var balanceType = GetBalanceType(entity.TransactionCode);

            foreach (var item in list)
            {
                var isInventoryItem = true;
                switch (entity.SRItemType)
                {
                    case ItemType.Medical:
                        var ipm = new ItemProductMedic();
                        ipm.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = ipm.IsInventoryItem ?? false;
                        break;
                    case ItemType.NonMedical:
                        var ipnm = new ItemProductNonMedic();
                        ipnm.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = ipnm.IsInventoryItem ?? false;
                        break;
                    case ItemType.Kitchen:
                        var ik = new ItemKitchen();
                        ik.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = ik.IsInventoryItem ?? false;
                        break;
                }

                if (isInventoryItem)
                {
                    ItemBalanceByPeriod balance = null;
                    var isAvailable = false;
                    foreach (var findBalance in balances.Where(findBalance => findBalance.ItemID.Equals(item.ItemID)))
                    {
                        isAvailable = true;
                        balance = findBalance;
                        break;
                    }
                    if (!isAvailable)
                    {
                        balance = balances.AddNew();
                        balance.PeriodYear = entity.ApprovedDate.Value.Year;
                        balance.PeriodMonth = entity.ApprovedDate.Value.Month;
                        balance.ItemID = item.ItemID;
                        balance.LocationID = locationID;
                        balance.BeginningBalance = 0;
                        balance.AdjustmentIn = 0;
                        balance.AdjustmentOut = 0;
                        if (balanceType == BalanceType.QtyIn)
                        {
                            balance.QuantityIn = Convert.ToDecimal(item.Quantity);
                            balance.QuantityOut = 0;
                        }
                        else
                        {
                            balance.QuantityIn = 0;
                            balance.QuantityOut = Convert.ToDecimal(item.Quantity);
                        }
                    }
                    else
                    {
                        if (balanceType == BalanceType.QtyIn)
                            balance.QuantityIn += Convert.ToDecimal(item.Quantity);
                        else
                            balance.QuantityOut += Convert.ToDecimal(item.Quantity);
                    }
                    balance.LastUpdateByUserID = userID;
                    balance.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                }
            }

            return balances;
        }

        private static ItemBalanceCollection PrepareItemBalances(esItemTransaction entity, IEnumerable<ItemTransactionItem> coll, string locationID,
            string userID)
        {
            if (!IsNeedUpdateBalance(entity.TransactionCode))
                return null;

            var list = coll.GroupBy(c => c.ItemID).Select(q => new
            {
                ItemID = q.Key,
                Quantity = q.Sum(p => (p.ConversionFactor * p.Quantity))
            });

            var balances = new ItemBalanceCollection();
            balances.Query.Where(
                balances.Query.LocationID == locationID,
                balances.Query.ItemID.In(list.Select(l => l.ItemID))
                );
            balances.LoadAll();

            var balanceType = GetBalanceType(entity.TransactionCode);

            foreach (var item in list)
            {
                var isInventoryItem = true;
                switch (entity.SRItemType)
                {
                    case ItemType.Medical:
                        var ipm = new ItemProductMedic();
                        ipm.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = ipm.IsInventoryItem ?? false;
                        break;
                    case ItemType.NonMedical:
                        var ipnm = new ItemProductNonMedic();
                        ipnm.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = ipnm.IsInventoryItem ?? false;
                        break;
                    case ItemType.Kitchen:
                        var ik = new ItemKitchen();
                        ik.LoadByPrimaryKey(item.ItemID);
                        isInventoryItem = ik.IsInventoryItem ?? false;
                        break;
                }

                if (isInventoryItem)
                {
                    ItemBalance balance = null;
                    var isAvailable = false;
                    foreach (var findBalance in balances.Where(findBalance => findBalance.ItemID.Equals(item.ItemID)))
                    {
                        isAvailable = true;
                        balance = findBalance;
                        break;
                    }
                    if (!isAvailable)
                    {
                        balance = balances.AddNew();
                        balance.ItemID = item.ItemID;
                        balance.LocationID = locationID;
                        if (balanceType == BalanceType.QtyIn)
                            balance.Balance = item.Quantity;
                        else
                            balance.Balance = 0 - item.Quantity;
                        balance.Minimum = 0;
                        balance.Maximum = 0;
                        balance.ReorderType = string.Empty;
                    }
                    else
                    {
                        if (balanceType == BalanceType.QtyIn)
                            balance.Balance += item.Quantity;
                        else
                            balance.Balance -= item.Quantity;
                    }
                    balance.LastUpdateByUserID = userID;
                    balance.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                }

            }

            return balances;
        }

        private static bool IsNeedUpdateBalance(string transactionCode)
        {
            switch (transactionCode)
            {
                case Reference.TransactionCode.PurchaseOrderReceive:
                case Reference.TransactionCode.PurchaseOrderReturn:
                case Reference.TransactionCode.InventoryIssueOut:
                case Reference.TransactionCode.DistributionConfirm:
                case Reference.TransactionCode.Distribution:
                case Reference.TransactionCode.ConsignmentReceive:
                case Reference.TransactionCode.DirectPurchase:
                case Reference.TransactionCode.ConsignmentReturn:
                case Reference.TransactionCode.SalesToBranch:
                case Reference.TransactionCode.SalesToBranchReturn:
                case Reference.TransactionCode.ReceiptOfSubstitute:
                case Reference.TransactionCode.ConsignmentTransfer:
                case Reference.TransactionCode.GrantsReceive:
                case Reference.TransactionCode.Sales:
                case Reference.TransactionCode.SalesReturn:
                    return true;
            }
            return false;
        }

        private static bool IsNeedCheckingBudgetPlan(string transactionCode)
        {
            switch (transactionCode)
            {
                case Reference.TransactionCode.PurchaseRequest:
                case Reference.TransactionCode.DistributionRequest:
                case Reference.TransactionCode.Distribution:
                case Reference.TransactionCode.InventoryIssueRequestOut:
                    return true;
            }
            return false;
        }

        public static void PrepareUpdateReferenceItem(string transactionCode, string referenceNo, string transactionNo, bool isApproval,
            string userID, out ItemTransaction entityRef, out ItemTransactionItemCollection entityRefItems, out ItemTransactionItemEdCollection entityRefItemEds)
        {
            // Approval akan mengupdate reference berlaku untuk transaksi:
            // "037" Purchase Order --> Increment QuantiySystem Purchase Request
            // "050" Distribution --> Increment QuantiySystem Distribution Request
            // "055" Distribution Confirm --> Increment QuantiySystem Distribution
            // "040" PurchaseOrderReceive --> Increment QuantiySystem Purchase Order
            // "043" PurchaseOrderReturn --> Increment QuantiySystem Purchase Order Receive
            // "044" ReceiptOfSubstituteItem--> Increment QuantiySystem Purchase Order Return
            // "082" InventoryIssued--> Increment QuantiySystem Inventory Issued Request

            if (referenceNo == string.Empty && transactionCode != Reference.TransactionCode.PurchaseOrder)
            {
                entityRefItems = null;
                entityRef = null;
                entityRefItemEds = null;
                return;
            }
            if (transactionCode != Reference.TransactionCode.PurchaseOrder &&
                transactionCode != Reference.TransactionCode.Distribution &&
                transactionCode != Reference.TransactionCode.DistributionConfirm &&
                transactionCode != Reference.TransactionCode.PurchaseOrderReceive &&
                transactionCode != Reference.TransactionCode.PurchaseOrderReturn &&
                transactionCode != Reference.TransactionCode.InventoryIssueOut &&
                transactionCode != Reference.TransactionCode.ConsignmentReturn &&
                transactionCode != Reference.TransactionCode.SalesToBranchReturn &&
                transactionCode != Reference.TransactionCode.ReceiptOfSubstitute &&
                transactionCode != Reference.TransactionCode.SalesReturn)
            {
                entityRefItems = null;
                entityRef = null;
                entityRefItemEds = null;
                return;
            }

            if (referenceNo == string.Empty && transactionCode == Reference.TransactionCode.PurchaseOrder)
            {
                entityRef = null;
                entityRefItems = new ItemTransactionItemCollection();

                var refQ = new ItemTransactionItemQuery("a");
                var currentQ = new ItemTransactionItemQuery("b");
                var currenthQ = new ItemTransactionQuery("c");

                refQ.LeftJoin(currentQ).On(
                    refQ.TransactionNo == currentQ.ReferenceNo &&
                    refQ.SequenceNo == currentQ.ReferenceSequenceNo
                    );
                refQ.LeftJoin(currenthQ).On(currenthQ.TransactionNo == currentQ.TransactionNo);
                refQ.Select(
                    refQ.TransactionNo,
                    refQ.SequenceNo,
                    currentQ.TransactionNo.As("UpdatedNo"),
                    (refQ.ConversionFactor * refQ.Quantity).As("QtyInBaseUnit"),
                    refQ.IsClosed, refQ.LastUpdateByUserID,
                    refQ.LastUpdateDateTime,
                    refQ.QuantityFinishInBaseUnit,
                    (currentQ.ConversionFactor * currentQ.Quantity).As("QtyUsed")
                    );
                refQ.Where(currentQ.TransactionNo == transactionNo, currenthQ.IsVoid == false);

                entityRefItems.Load(refQ);

                foreach (var item in entityRefItems)
                {
                    if (isApproval)
                    {
                        item.QuantityFinishInBaseUnit += Convert.ToDecimal(item.GetColumn("QtyUsed"));
                        if (item.QuantityFinishInBaseUnit >= Convert.ToDecimal(item.GetColumn("QtyInBaseUnit")))
                            item.IsClosed = true;
                    }
                    else
                    {
                        item.QuantityFinishInBaseUnit -= Convert.ToDecimal(item.GetColumn("QtyUsed"));
                        item.IsClosed = false;
                    }
                    if (item.es.IsModified)
                    {
                        item.LastUpdateByUserID = userID;
                        item.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                    }
                }
                entityRefItemEds = null;
            }
            else
            {
                entityRef = new ItemTransaction();
                entityRefItems = new ItemTransactionItemCollection();

                var refQ = new ItemTransactionItemQuery("a");
                var currentQ = new ItemTransactionItemQuery("b");
                var currenthQ = new ItemTransactionQuery("c");
                var nowQ = new ItemTransactionItemQuery("d");

                refQ.LeftJoin(currentQ).On(
                    refQ.TransactionNo == currentQ.ReferenceNo &&
                    refQ.SequenceNo == currentQ.ReferenceSequenceNo
                    );
                refQ.LeftJoin(currenthQ).On(currenthQ.TransactionNo == currentQ.TransactionNo);
                refQ.LeftJoin(nowQ).On(
                    refQ.TransactionNo == nowQ.ReferenceNo &&
                    refQ.SequenceNo == nowQ.ReferenceSequenceNo && 
                    nowQ.TransactionNo == transactionNo
                    );
                refQ.Select(
                    refQ.TransactionNo,
                    refQ.SequenceNo,
                    //currentQ.TransactionNo.As("UpdatedNo"),
                    (refQ.ConversionFactor * refQ.Quantity).As("QtyInBaseUnit"),
                    refQ.IsClosed, refQ.LastUpdateByUserID,
                    refQ.LastUpdateDateTime,
                    refQ.QuantityFinishInBaseUnit,
                    (currentQ.ConversionFactor * currentQ.Quantity).Sum().As("QtyUsed"),
                    (nowQ.ConversionFactor * nowQ.Quantity).As("QtyNow")
                    );
                refQ.Where(refQ.TransactionNo == referenceNo, currenthQ.IsVoid == false);
                refQ.GroupBy(refQ.TransactionNo,
                             refQ.SequenceNo,
                             //currentQ.TransactionNo,
                             refQ.ConversionFactor, refQ.Quantity,
                             refQ.IsClosed, refQ.LastUpdateByUserID,
                             refQ.LastUpdateDateTime,
                             refQ.QuantityFinishInBaseUnit,
                             nowQ.ConversionFactor, 
                             nowQ.Quantity);

                DataTable dtb = refQ.LoadDataTable();


                entityRefItems.Load(refQ);

                #region -old-
                //var isAllItemClosed = true;
                //foreach (var item in entityRefItems)
                //{
                //    if (item.GetColumn("QtyUsed") == DBNull.Value)
                //    {
                //        if (item.IsClosed.Equals(false) && (item.QuantityFinishInBaseUnit < Convert.ToDecimal(item.GetColumn("QtyInBaseUnit"))))
                //            isAllItemClosed = false;
                //        continue;
                //    }
                //    if (!item.GetColumn("UpdatedNo").Equals(transactionNo))
                //    {
                //        //if (item.IsClosed.Equals(false) && (Convert.ToDecimal(item.GetColumn("QtyUsed")) < Convert.ToDecimal(item.GetColumn("QtyInBaseUnit"))))
                //        //    isAllItemClosed = false;
                //        continue;
                //    }

                //    if (isApproval)
                //    {
                //        item.QuantityFinishInBaseUnit += Convert.ToDecimal(item.GetColumn("QtyUsed"));
                //        if (item.QuantityFinishInBaseUnit >= Convert.ToDecimal(item.GetColumn("QtyInBaseUnit")))
                //            item.IsClosed = true;
                //    }
                //    else
                //    {
                //        item.QuantityFinishInBaseUnit -= Convert.ToDecimal(item.GetColumn("QtyUsed"));
                //        item.IsClosed = false;

                //    }
                //    if (item.es.IsModified)
                //    {
                //        item.LastUpdateByUserID = userID;
                //        item.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                //    }
                //    //if (item.IsClosed.Equals(false) && item.QuantityFinishInBaseUnit < Convert.ToDecimal(item.GetColumn("QtyInBaseUnit")))
                //    //    isAllItemClosed = false;
                //    if (item.IsClosed.Equals(false) || item.QuantityFinishInBaseUnit < Convert.ToDecimal(item.GetColumn("QtyInBaseUnit")))
                //        isAllItemClosed = false;
                //}
#endregion

                var isAllItemClosed = true;
                foreach (var item in entityRefItems)
                {
                    if (item.GetColumn("QtyUsed") == DBNull.Value)
                    {
                        isAllItemClosed = false;
                        continue;
                    }
                    //if (!item.GetColumn("UpdatedNo").Equals(transactionNo))
                    //{

                    //    continue;
                    //}

                    if (isApproval)
                    {
                        //item.QuantityFinishInBaseUnit += Convert.ToDecimal(item.GetColumn("QtyUsed"));
                        item.QuantityFinishInBaseUnit = Convert.ToDecimal(item.GetColumn("QtyUsed"));
                        if (item.QuantityFinishInBaseUnit >= Convert.ToDecimal(item.GetColumn("QtyInBaseUnit")))
                            item.IsClosed = true;
                        else
                            isAllItemClosed = false;
                    }
                    else
                    {
                        if (item.GetColumn("QtyNow") == DBNull.Value)
                        {
                            isAllItemClosed = false;
                            continue;
                        }

                        //item.QuantityFinishInBaseUnit -= Convert.ToDecimal(item.GetColumn("QtyUsed"));
                        item.QuantityFinishInBaseUnit -= Convert.ToDecimal(item.GetColumn("QtyNow"));
                        item.IsClosed = false;
                        isAllItemClosed = false;
                    }
                    if (item.es.IsModified)
                    {
                        item.LastUpdateByUserID = userID;
                        item.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                    }
                }

                entityRef.LoadByPrimaryKey(referenceNo);
                entityRef.IsClosed = isAllItemClosed;
                entityRef.LastUpdateByUserID = userID;
                entityRef.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;

                if (transactionCode == Reference.TransactionCode.PurchaseOrderReturn)
                {
                    entityRefItemEds = new ItemTransactionItemEdCollection();

                    var refEdQ = new ItemTransactionItemEdQuery("a");
                    var currentEdQ = new ItemTransactionItemQuery("b");

                    refEdQ.LeftJoin(currentQ).On(
                        refEdQ.TransactionNo == currentQ.ReferenceNo &&
                        refEdQ.SequenceNo == currentQ.ReferenceSequenceNo &&
                        refEdQ.ExpiredDate.Date() == currentEdQ.ExpiredDate.Date()
                        );
                    refEdQ.Select(
                        refEdQ.TransactionNo,
                        refEdQ.SequenceNo,
                        refEdQ.BatchNumber,
                        refEdQ.ExpiredDate,
                        currentEdQ.TransactionNo.As("UpdatedNo"),
                        (refEdQ.ConversionFactor * refEdQ.Quantity).As("QtyInBaseUnit"),
                        refEdQ.LastUpdateByUserID,
                        refEdQ.LastUpdateDateTime,
                        refEdQ.QuantityFinishInBaseUnit,
                        (currentEdQ.ConversionFactor * currentEdQ.Quantity).As("QtyUsed")
                        );
                    refEdQ.Where(refEdQ.TransactionNo == referenceNo);

                    entityRefItemEds.Load(refEdQ);

                    foreach (var item in entityRefItemEds)
                    {
                        if (item.GetColumn("QtyUsed") == DBNull.Value)
                            continue;

                        if (!item.GetColumn("UpdatedNo").Equals(transactionNo))
                            continue;

                        if (isApproval)
                            item.QuantityFinishInBaseUnit += Convert.ToDecimal(item.GetColumn("QtyUsed"));
                        else
                            item.QuantityFinishInBaseUnit -= Convert.ToDecimal(item.GetColumn("QtyUsed"));

                        if (item.es.IsModified)
                        {
                            item.LastUpdateByUserID = userID;
                            item.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                        }
                    }
                }
                else
                {
                    entityRefItemEds = null;
                }
            }
        }

        private static void PrepareItemProductMedicAndAverage(ItemTransaction entity, ItemTransactionItemCollection coll,
            string userID, out ItemProductMedicCollection itemProductMedics, out AveragePriceHistoryCollection averagePriceHistories, bool isUpdatePrice)
        {
            if (entity.SRItemType != ItemType.Medical)
            {
                itemProductMedics = null;
                averagePriceHistories = null;
                return;
            }

            itemProductMedics = new ItemProductMedicCollection();
            itemProductMedics.Query.Where(itemProductMedics.Query.ItemID.In((coll.Select(c => c.ItemID)).Distinct()));
            itemProductMedics.LoadAll();

            averagePriceHistories = new AveragePriceHistoryCollection();

            var collItemX = new ItemTransactionItemCollection();
            var itemX = new ItemTransactionItemQuery("a");
            itemX.Where(itemX.TransactionNo == entity.TransactionNo, itemX.IsBonusItem == false); // barang bonus tetap hitung ulang costprice sbg penambahan QTY saja
            itemX.Select(
                itemX.IsBonusItem,
                itemX.IsTaxable,
                itemX.TransactionNo,
                itemX.ItemID,
                itemX.Discount1Percentage,
                itemX.Discount2Percentage,
                @"<SUM((a.Quantity * a.ConversionFactor)) AS Quantity>",
                @"<1 AS ConversionFactor>",
                @"<(a.PriceInCurrency / a.ConversionFactor) AS PriceInCurrency>",
                @"<(a.DiscountInCurrency / a.ConversionFactor) AS DiscountInCurrency>");
            itemX.GroupBy(
                itemX.IsBonusItem,
                itemX.IsTaxable,
                itemX.TransactionNo,
                itemX.ItemID,
                itemX.Discount1Percentage,
                itemX.Discount2Percentage,
                @"<(a.PriceInCurrency / a.ConversionFactor)>",
                @"<(a.DiscountInCurrency / a.ConversionFactor)>");
            collItemX.Load(itemX);

            //var collItem = coll.OrderBy(i => i.ItemID);
            var collItem = collItemX.OrderBy(i => i.ItemID);

            foreach (var item in collItem)
            {
                //if (item.IsBonusItem == false)
                //{
                var medic = itemProductMedics.FindByPrimaryKey(item.ItemID);
                if (medic == null)
                    continue;

                //if (!isUpdatePrice)
                //    continue;

                decimal priceWvat = 0; //diambil harga satuan kecil karena penerimaan bisa dilakukan pake satuan kecil juga
                decimal priceInBaseUnitNow = 0;
                decimal priceInBasedUnitWVat = 0;
                decimal lastPriceInBaseUnit = 0;

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeDiscount) == "Yes")
                {
                    priceWvat = (((item.PriceInCurrency ?? 0) - (item.DiscountInCurrency ?? 0)) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0))) *
                    (1 + (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (entity.TaxPercentage ?? 0) : 0) / 100);
                }
                else
                {
                    priceWvat = ((item.PriceInCurrency ?? 0) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0))) *
                    (1 + (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (entity.TaxPercentage ?? 0) : 0) / 100);
                }
                priceInBaseUnitNow = ((item.PriceInCurrency ?? 0) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0)));
                priceInBasedUnitWVat = ((item.PriceInCurrency ?? 0) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0))) *
                    (1 + (((entity.IsTaxable ?? 0) == 1) && (item.IsTaxable ?? false) ? (entity.TaxPercentage ?? 0) : 0) / 100);
                lastPriceInBaseUnit = (((item.PriceInCurrency ?? 0) - (item.DiscountInCurrency ?? 0)) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0))) *
                    (1 + (((entity.IsTaxable ?? 0) == 1) && (item.IsTaxable ?? false) ? (entity.TaxPercentage ?? 0) : 0) / 100);

                decimal oldPriceInBaseUnit = medic.PriceInBaseUnit ?? 0;
                decimal oldPriceInPurchaseUnit = medic.PriceInPurchaseUnit ?? 0;
                decimal oldPurchaseDiscount1 = medic.PurchaseDiscount1 ?? 0;
                decimal oldPurchaseDiscount2 = medic.PurchaseDiscount2 ?? 0;

                if (entity.TransactionCode != Reference.TransactionCode.PurchaseOrderReturn && item.IsBonusItem == false && isUpdatePrice)
                {
                    //if (priceInBaseUnitNow > nonMedic.HighestPriceInBasedUnit)
                    //    medic.HighestPriceInBasedUnit = priceInBaseUnitNow;

                    //if (item.Discount1Percentage < medic.SalesDiscount &&
                    //    (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatient) == "Yes" ||
                    //    (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatient) == "No" &&
                    //    (medic.IsSharePurchaseDiscToPatient ?? false))))
                    //    medic.SalesDiscount = item.Discount1Percentage;

                    decimal? salesDiscount = 0;
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatient) == "Yes" || 
                        (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatient) == "No" &&
                        (medic.IsSharePurchaseDiscToPatient ?? false)))
                    {
                        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatientFull) == "Yes")
                        {
                            salesDiscount = item.Discount1Percentage;
                        }
                        else if (item.Discount1Percentage < medic.SalesDiscount)
                            salesDiscount = item.Discount1Percentage;
                    }
                    medic.SalesDiscount = salesDiscount;

                    if ((priceInBaseUnitNow * (1 - medic.SalesDiscount / 100)) > medic.HighestPriceInBasedUnit)
                    {
                        medic.HighestPriceInBasedUnit = priceInBaseUnitNow * (1 - (medic.SalesDiscount / 100));
                    }

                    medic.PriceInBaseUnit = priceInBaseUnitNow;
                    medic.PriceInBasedUnitWVat = priceInBasedUnitWVat;
                    medic.PriceInPurchaseUnit = priceInBaseUnitNow * (medic.ConversionFactor == 0 ? 1 : medic.ConversionFactor);
                    medic.PurchaseDiscount1 = item.Discount1Percentage;
                    medic.PurchaseDiscount2 = item.Discount2Percentage;
                    medic.LastPriceInBaseUnit = lastPriceInBaseUnit;
                    medic.PriceWVat = priceWvat;
                }

                var balances = new ItemBalanceCollection();
                var ibQ = new ItemBalanceQuery("ib");
                var lQ = new LocationQuery("l");

                ibQ.Select(
                    ibQ.ItemID,
                    ibQ.Balance.Sum(),
                    ibQ.Booking.Sum()
                    )
                    .InnerJoin(lQ).On(ibQ.LocationID == lQ.LocationID)
                    .Where(ibQ.ItemID == item.ItemID, lQ.IsConsignment.Coalesce("0") == false);

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSCH")
                    ibQ.Where(balances.Query.LocationID == entity.ToLocationID);

                ibQ.GroupBy(balances.Query.ItemID);
                balances.Load(ibQ);

                //throw new Exception("coba - coba saja");

                var initialQty = 0;
                if (balances.Count() != 0)
                    initialQty = Convert.ToInt32(balances[0].Balance) + Convert.ToInt32(balances[0].Booking);

                var list = (coll.GroupBy(c => new
                {
                    c.ItemID
                }).Select(q => new
                {
                    q.Key.ItemID,
                    Quantity = q.Sum(p => (p.ConversionFactor * p.Quantity))
                }).Where(a => a.ItemID == item.ItemID)).Take(1).Single();

                //var qtyReceiveInBaseUnit = item.Quantity * (item.ConversionFactor == 0 ? 1 : item.ConversionFactor); //Qty Beli
                //priceWvat = (priceWvat * (qtyReceiveInBaseUnit ?? 0)) / (list.Quantity ?? 0);
                var qtyReceiveInBaseUnit = list.Quantity; //Qty Beli di-sum ulang (include item bonus)

                if (entity.TransactionCode == Reference.TransactionCode.PurchaseOrderReturn)
                {
                    qtyReceiveInBaseUnit = -qtyReceiveInBaseUnit;
                }

                var qtyTotal = qtyReceiveInBaseUnit + initialQty;
                var newCostPrice = (((medic.CostPrice ?? 0) * initialQty) + (priceWvat * qtyReceiveInBaseUnit)) / (qtyTotal == 0 ? 1 : qtyTotal);

                string parCostType = AppParameter.GetParameterValue(AppParameter.ParameterItem.ItemProductCostPriceType);
                if (parCostType != "AVG")
                    newCostPrice = priceWvat; /*20170320:deby: khusus u/ RSUD (non AVG), costprice diambil dari harga terakhir - diskon + ppn*/

                if (newCostPrice < 0)
                    newCostPrice = 0;

                //throw new Exception("coba - coba saja");

                if (medic.IsInventoryItem ?? false)
                {
                    var averagePriceHis = averagePriceHistories.SingleOrDefault(ib => ib.TransactionNo == entity.TransactionNo && ib.ItemID == item.ItemID);
                    if (averagePriceHis == null)
                    {
                        averagePriceHis = averagePriceHistories.AddNew();
                        averagePriceHis.TransactionCode = entity.TransactionCode;
                        averagePriceHis.TransactionNo = entity.TransactionNo;
                        averagePriceHis.ItemUnit = medic.SRItemUnit;
                        averagePriceHis.ItemID = item.ItemID;
                        averagePriceHis.ChangedDate = entity.ApprovedDate;
                        averagePriceHis.OldAveragePrice = medic.CostPrice;
                        averagePriceHis.NewAveragePrice = newCostPrice;
                        averagePriceHis.LastUpdateByUserID = userID;
                        averagePriceHis.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;

                        averagePriceHis.OldPriceInBaseUnit = oldPriceInBaseUnit;
                        averagePriceHis.OldPriceInPurchaseUnit = oldPriceInPurchaseUnit;
                        averagePriceHis.OldPurchaseDiscount1 = oldPurchaseDiscount1;
                        averagePriceHis.OldPurchaseDiscount2 = oldPurchaseDiscount2;
                        averagePriceHis.NewPriceInBaseUnit = medic.PriceInBaseUnit;
                        averagePriceHis.NewPriceInPurchaseUnit = medic.PriceInPurchaseUnit;
                        averagePriceHis.NewPurchaseDiscount1 = medic.PurchaseDiscount1;
                        averagePriceHis.NewPurchaseDiscount2 = medic.PurchaseDiscount2;
                    }
                }
                //ditaruh disini agar averagePriceHis.OldAveragePrice terisi dahulu dgn nilai lama
                medic.CostPrice = newCostPrice;

                //jika costprice nol, maka reset harga jd nol
                if (newCostPrice == 0)
                {
                    medic.PriceInBaseUnit = 0;
                    medic.PriceInBasedUnitWVat = 0;
                    medic.PriceInPurchaseUnit = 0;
                    medic.PurchaseDiscount1 = 0;
                    medic.PurchaseDiscount2 = 0;
                    medic.LastPriceInBaseUnit = 0;
                    medic.PriceWVat = 0;
                }

                //cost price item transaction diupdate dg yg terbaru
                //item.CostPrice = newCostPrice;
                //item.LastUpdateByUserID = userID;
                //item.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                //}
            }
        }

        private static void PrepareItemProductNonMedicAndAverage(ItemTransaction entity, ItemTransactionItemCollection coll,
            string userID, out ItemProductNonMedicCollection itemProductNonMedics, out AveragePriceHistoryCollection averagePriceHistories, bool isUpdatePrice)
        {
            if (entity.SRItemType != ItemType.NonMedical)
            {
                itemProductNonMedics = null;
                averagePriceHistories = null;
                return;
            }

            itemProductNonMedics = new ItemProductNonMedicCollection();
            itemProductNonMedics.Query.Where(itemProductNonMedics.Query.ItemID.In((coll.Select(c => c.ItemID)).Distinct()));
            itemProductNonMedics.LoadAll();

            averagePriceHistories = new AveragePriceHistoryCollection();

            var collItemX = new ItemTransactionItemCollection();
            var itemX = new ItemTransactionItemQuery("a");
            itemX.Where(itemX.TransactionNo == entity.TransactionNo, itemX.IsBonusItem == false); // barang bonus tetap hitung ulang costprice
            itemX.Select(
                itemX.IsBonusItem,
                itemX.IsTaxable,
                itemX.TransactionNo,
                itemX.ItemID,
                itemX.Discount1Percentage,
                itemX.Discount2Percentage,
                @"<SUM((a.Quantity * a.ConversionFactor)) AS Quantity>",
                @"<1 AS ConversionFactor>",
                @"<(a.PriceInCurrency / a.ConversionFactor) AS PriceInCurrency>",
                @"<(a.DiscountInCurrency / a.ConversionFactor) AS DiscountInCurrency>");
            itemX.GroupBy(
                itemX.IsBonusItem,
                itemX.IsTaxable,
                itemX.TransactionNo,
                itemX.ItemID,
                itemX.Discount1Percentage,
                itemX.Discount2Percentage,
                @"<(a.PriceInCurrency / a.ConversionFactor)>",
                @"<(a.DiscountInCurrency / a.ConversionFactor)>");
            collItemX.Load(itemX);

            //var collItem = coll.OrderBy(i => i.ItemID);
            var collItem = collItemX.OrderBy(i => i.ItemID);

            foreach (var item in collItem)
            {
                //if (item.IsBonusItem == false)
                //{
                var nonMedic = itemProductNonMedics.FindByPrimaryKey(item.ItemID);
                if (nonMedic == null)
                    continue;

                //if (!isUpdatePrice)
                //    continue;

                decimal priceWvat = 0; //diambil harga satuan kecil karena penerimaan bisa dilakukan pake satuan kecil juga
                decimal priceInBaseUnitNow = 0;
                decimal priceInBasedUnitWVat = 0;
                decimal lastPriceInBaseUnit = 0;

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeDiscount) == "Yes")
                {
                    priceWvat = (((item.PriceInCurrency ?? 0) - (item.DiscountInCurrency ?? 0)) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0))) *
                    (1 + (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (entity.TaxPercentage ?? 0) : 0) / 100);
                }
                else
                {
                    priceWvat = ((item.PriceInCurrency ?? 0) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0))) *
                    (1 + (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (entity.TaxPercentage ?? 0) : 0) / 100);
                }

                priceInBaseUnitNow = ((item.PriceInCurrency ?? 0) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0)));
                priceInBasedUnitWVat = ((item.PriceInCurrency ?? 0) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0))) *
                    (1 + (((entity.IsTaxable ?? 0) == 1) && (item.IsTaxable ?? false) ? (entity.TaxPercentage ?? 0) : 0) / 100);
                lastPriceInBaseUnit = (((item.PriceInCurrency ?? 0) - (item.DiscountInCurrency ?? 0)) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0))) *
                    (1 + (((entity.IsTaxable ?? 0) == 1) && (item.IsTaxable ?? false) ? (entity.TaxPercentage ?? 0) : 0) / 100);

                decimal oldPriceInBaseUnit = nonMedic.PriceInBaseUnit ?? 0;
                decimal oldPriceInPurchaseUnit = nonMedic.PriceInPurchaseUnit ?? 0;
                decimal oldPurchaseDiscount1 = nonMedic.PurchaseDiscount1 ?? 0;
                decimal oldPurchaseDiscount2 = nonMedic.PurchaseDiscount2 ?? 0;

                if (entity.TransactionCode != Reference.TransactionCode.PurchaseOrderReturn && item.IsBonusItem == false && isUpdatePrice)
                {
                    //if (priceInBaseUnitNow > nonMedic.HighestPriceInBasedUnit)
                    //    nonMedic.HighestPriceInBasedUnit = priceInBaseUnitNow;

                    //if (item.Discount1Percentage < nonMedic.SalesDiscount &&
                    //    (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatient) == "Yes" ||
                    //    (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatient) == "No" &&
                    //    (nonMedic.IsSharePurchaseDiscToPatient ?? false))))
                    //    nonMedic.SalesDiscount = item.Discount1Percentage;

                    decimal? salesDiscount = 0;
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatient) == "Yes" ||
                        (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatient) == "No" &&
                        (nonMedic.IsSharePurchaseDiscToPatient ?? false)))
                    {
                        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSharePurchaseDiscToPatientFull) == "Yes")
                        {
                            salesDiscount = item.Discount1Percentage;
                        }
                        else if (item.Discount1Percentage < nonMedic.SalesDiscount)
                            salesDiscount = item.Discount1Percentage;
                    }
                    nonMedic.SalesDiscount = salesDiscount;

                    if ((priceInBaseUnitNow * (1 - salesDiscount / 100)) > nonMedic.HighestPriceInBasedUnit)
                    {
                        nonMedic.HighestPriceInBasedUnit = priceInBaseUnitNow * (1 - (nonMedic.SalesDiscount / 100));
                    }

                    nonMedic.PriceInBaseUnit = priceInBaseUnitNow;
                    nonMedic.PriceInBasedUnitWVat = priceInBasedUnitWVat;
                    nonMedic.PriceInPurchaseUnit = priceInBaseUnitNow * nonMedic.ConversionFactor;
                    nonMedic.PurchaseDiscount1 = item.Discount1Percentage;
                    nonMedic.PurchaseDiscount2 = item.Discount2Percentage;
                    nonMedic.LastPriceInBaseUnit = lastPriceInBaseUnit;
                    nonMedic.PriceWVat = priceWvat;
                }

                var balances = new ItemBalanceCollection();
                var ibQ = new ItemBalanceQuery("ib");
                var lQ = new LocationQuery("l");

                ibQ.Select(
                    ibQ.ItemID,
                    ibQ.Balance.Sum(),
                    ibQ.Booking.Sum()
                    )
                    .InnerJoin(lQ).On(ibQ.LocationID == lQ.LocationID)
                    .Where(ibQ.ItemID == item.ItemID, lQ.IsConsignment.Coalesce("0") == false);

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSCH")
                    ibQ.Where(balances.Query.LocationID == entity.ToLocationID);

                ibQ.GroupBy(balances.Query.ItemID);
                balances.Load(ibQ);

                var initialQty = 0;
                if (balances.Count() != 0) initialQty = Convert.ToInt32(balances[0].Balance) + Convert.ToInt32(balances[0].Booking);

                var list = (coll.GroupBy(c => new
                {
                    c.ItemID
                }).Select(q => new
                {
                    q.Key.ItemID,
                    Quantity = q.Sum(p => (p.ConversionFactor * p.Quantity))
                }).Where(a => a.ItemID == item.ItemID)).Take(1).Single();

                //var qtyReceiveInBaseUnit = item.Quantity * (item.ConversionFactor == 0 ? 1 : item.ConversionFactor); //Qty Beli
                //priceWvat = (priceWvat * (qtyReceiveInBaseUnit ?? 0)) / (list.Quantity ?? 0);
                var qtyReceiveInBaseUnit = list.Quantity; //Qty beli di-sum ulang (include item bonus)

                if (entity.TransactionCode == Reference.TransactionCode.PurchaseOrderReturn)
                {
                    qtyReceiveInBaseUnit = -qtyReceiveInBaseUnit;
                }

                var qtyTotal = qtyReceiveInBaseUnit + initialQty;
                var newCostPrice = (((nonMedic.CostPrice ?? 0) * initialQty) + (priceWvat * qtyReceiveInBaseUnit)) / (qtyTotal == 0 ? 1 : qtyTotal);

                string parCostType = AppParameter.GetParameterValue(AppParameter.ParameterItem.ItemProductCostPriceType);
                if (parCostType != "AVG")
                    newCostPrice = priceWvat; /*20170320:deby: khusus u/ RSUD (non AVG), costprice diambil dari harga terakhir - diskon + ppn*/

                if (newCostPrice < 0)
                    newCostPrice = 0;

                if (nonMedic.IsInventoryItem ?? false)
                {
                    var averagePriceHis = averagePriceHistories.SingleOrDefault(ib => ib.TransactionNo == entity.TransactionNo && ib.ItemID == item.ItemID);
                    if (averagePriceHis == null)
                    {
                        averagePriceHis = averagePriceHistories.AddNew();
                        averagePriceHis.TransactionCode = entity.TransactionCode;
                        averagePriceHis.TransactionNo = entity.TransactionNo;
                        averagePriceHis.ItemUnit = nonMedic.SRItemUnit;
                        averagePriceHis.ItemID = item.ItemID;
                        averagePriceHis.ChangedDate = entity.ApprovedDate;
                        averagePriceHis.OldAveragePrice = nonMedic.CostPrice;
                        averagePriceHis.NewAveragePrice = newCostPrice;
                        averagePriceHis.LastUpdateByUserID = userID;
                        averagePriceHis.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;

                        averagePriceHis.OldPriceInBaseUnit = oldPriceInBaseUnit;
                        averagePriceHis.OldPriceInPurchaseUnit = oldPriceInPurchaseUnit;
                        averagePriceHis.OldPurchaseDiscount1 = oldPurchaseDiscount1;
                        averagePriceHis.OldPurchaseDiscount2 = oldPurchaseDiscount2;
                        averagePriceHis.NewPriceInBaseUnit = nonMedic.PriceInBaseUnit;
                        averagePriceHis.NewPriceInPurchaseUnit = nonMedic.PriceInPurchaseUnit;
                        averagePriceHis.NewPurchaseDiscount1 = nonMedic.PurchaseDiscount1;
                        averagePriceHis.NewPurchaseDiscount2 = nonMedic.PurchaseDiscount2;
                    }
                }

                nonMedic.CostPrice = newCostPrice;

                //jika costprice nol, maka reset harga jd nol
                if (newCostPrice == 0)
                {
                    nonMedic.PriceInBaseUnit = 0;
                    nonMedic.PriceInBasedUnitWVat = 0;
                    nonMedic.PriceInPurchaseUnit = 0;
                    nonMedic.PurchaseDiscount1 = 0;
                    nonMedic.PurchaseDiscount2 = 0;
                    nonMedic.LastPriceInBaseUnit = 0;
                    nonMedic.PriceWVat = 0;
                }

                //item.CostPrice = newCostPrice;
                //item.LastUpdateByUserID = userID;
                //item.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                //}
            }
        }

        private static void PrepareItemKitchenAndAverage(ItemTransaction entity, ItemTransactionItemCollection coll,
            string userID, out ItemKitchenCollection itemKitchens, out AveragePriceHistoryCollection averagePriceHistories, bool isUpdatePrice)
        {
            if (entity.SRItemType != ItemType.Kitchen)
            {
                itemKitchens = null;
                averagePriceHistories = null;
                return;
            }

            itemKitchens = new ItemKitchenCollection();
            itemKitchens.Query.Where(itemKitchens.Query.ItemID.In((coll.Select(c => c.ItemID)).Distinct()));
            itemKitchens.LoadAll();

            averagePriceHistories = new AveragePriceHistoryCollection();

            var collItemX = new ItemTransactionItemCollection();
            var itemX = new ItemTransactionItemQuery("a");
            itemX.Where(itemX.TransactionNo == entity.TransactionNo, itemX.IsBonusItem == false); // barang bonus tetap hitung ulang costprice
            itemX.Select(
                itemX.IsBonusItem,
                itemX.IsTaxable,
                itemX.TransactionNo,
                itemX.ItemID,
                itemX.Discount1Percentage,
                itemX.Discount2Percentage,
                @"<SUM((a.Quantity * a.ConversionFactor)) AS Quantity>",
                @"<1 AS ConversionFactor>",
                @"<(a.PriceInCurrency / a.ConversionFactor) AS PriceInCurrency>",
                @"<(a.DiscountInCurrency / a.ConversionFactor) AS DiscountInCurrency>");
            itemX.GroupBy(
                itemX.IsBonusItem,
                itemX.IsTaxable,
                itemX.TransactionNo,
                itemX.ItemID,
                itemX.Discount1Percentage,
                itemX.Discount2Percentage,
                @"<(a.PriceInCurrency / a.ConversionFactor)>",
                @"<(a.DiscountInCurrency / a.ConversionFactor)>");
            collItemX.Load(itemX);

            //var collItem = coll.OrderBy(i => i.ItemID);
            var collItem = collItemX.OrderBy(i => i.ItemID);

            foreach (var item in collItem)
            {
                //if (item.IsBonusItem == false)
                //{
                var kitchen = itemKitchens.FindByPrimaryKey(item.ItemID);
                if (kitchen == null)
                    continue;

                //if (!isUpdatePrice)
                //    continue;

                decimal priceWvat = 0; //diambil harga satuan kecil karena penerimaan bisa dilakukan pake satuan kecil juga
                decimal priceInBaseUnitNow = 0;
                decimal priceInBasedUnitWVat = 0;
                decimal lastPriceInBaseUnit = 0;

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeDiscount) == "Yes")
                {
                    priceWvat = (((item.PriceInCurrency ?? 0) - (item.DiscountInCurrency ?? 0)) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0))) *
                    (1 + (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (entity.TaxPercentage ?? 0) : 0) / 100);
                }
                else
                {
                    priceWvat = ((item.PriceInCurrency ?? 0) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0))) *
                    (1 + (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIncludeTax) == "Yes" ? (entity.TaxPercentage ?? 0) : 0) / 100);
                }

                priceInBaseUnitNow = ((item.PriceInCurrency ?? 0) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0)));
                priceInBasedUnitWVat = ((item.PriceInCurrency ?? 0) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0))) *
                                       (1 + (((entity.IsTaxable ?? 0) == 1) ? (entity.TaxPercentage ?? 0) : 0) / 100);
                lastPriceInBaseUnit = (((item.PriceInCurrency ?? 0) - (item.DiscountInCurrency ?? 0)) / ((item.ConversionFactor ?? 0) == 0 ? 1 : (item.ConversionFactor ?? 0))) *
                    (1 + (((entity.IsTaxable ?? 0) == 1) && (item.IsTaxable ?? false) ? (entity.TaxPercentage ?? 0) : 0) / 100);

                decimal oldPriceInBaseUnit = kitchen.PriceInBaseUnit ?? 0;
                decimal oldPriceInPurchaseUnit = kitchen.PriceInPurchaseUnit ?? 0;
                decimal oldPurchaseDiscount1 = kitchen.PurchaseDiscount1 ?? 0;
                decimal oldPurchaseDiscount2 = kitchen.PurchaseDiscount2 ?? 0;

                if (entity.TransactionCode != Reference.TransactionCode.PurchaseOrderReturn && item.IsBonusItem == false && isUpdatePrice)
                {
                    if (priceInBaseUnitNow > kitchen.HighestPriceInBasedUnit)
                        kitchen.HighestPriceInBasedUnit = priceInBaseUnitNow;

                    kitchen.PriceInBaseUnit = priceInBaseUnitNow;
                    kitchen.PriceInBasedUnitWVat = priceInBasedUnitWVat;
                    kitchen.PriceInPurchaseUnit = priceInBaseUnitNow * kitchen.ConversionFactor;
                    kitchen.PurchaseDiscount1 = item.Discount1Percentage;
                    kitchen.PurchaseDiscount2 = item.Discount2Percentage;
                    kitchen.LastPriceInBaseUnit = lastPriceInBaseUnit;
                    kitchen.PriceWVat = priceWvat;
                }

                var balances = new ItemBalanceCollection();
                var ibQ = new ItemBalanceQuery("ib");
                var lQ = new LocationQuery("l");

                ibQ.Select(
                    ibQ.ItemID,
                    ibQ.Balance.Sum(),
                    ibQ.Booking.Sum()
                    )
                    .InnerJoin(lQ).On(ibQ.LocationID == lQ.LocationID)
                    .Where(ibQ.ItemID == item.ItemID, lQ.IsConsignment.Coalesce("0") == false);

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSCH")
                    ibQ.Where(balances.Query.LocationID == entity.ToLocationID);

                ibQ.GroupBy(balances.Query.ItemID);
                balances.Load(ibQ);

                var initialQty = 0;
                if (balances.Count() != 0) initialQty = Convert.ToInt32(balances[0].Balance) + Convert.ToInt32(balances[0].Booking);

                var list = (coll.GroupBy(c => new
                {
                    c.ItemID
                }).Select(q => new
                {
                    q.Key.ItemID,
                    Quantity = q.Sum(p => (p.ConversionFactor * p.Quantity))
                }).Where(a => a.ItemID == item.ItemID)).Take(1).Single();

                //var qtyReceiveInBaseUnit = item.Quantity * (item.ConversionFactor == 0 ? 1 : item.ConversionFactor); //Qty Beli
                //priceWvat = (priceWvat * (qtyReceiveInBaseUnit ?? 0)) / (list.Quantity ?? 0);
                var qtyReceiveInBaseUnit = list.Quantity; //Qty beli di-sum ulang (include item bonus)

                if (entity.TransactionCode == Reference.TransactionCode.PurchaseOrderReturn)
                {
                    qtyReceiveInBaseUnit = -qtyReceiveInBaseUnit;
                }

                var qtyTotal = qtyReceiveInBaseUnit + initialQty;
                var newCostPrice = (((kitchen.CostPrice ?? 0) * initialQty) + (priceWvat * qtyReceiveInBaseUnit)) / (qtyTotal == 0 ? 1 : qtyTotal);

                string parCostType = AppParameter.GetParameterValue(AppParameter.ParameterItem.ItemProductCostPriceType);
                if (parCostType != "AVG")
                    newCostPrice = priceWvat; /*20170320:deby: khusus u/ RSUD (non AVG), costprice diambil dari harga terakhir - diskon + ppn*/

                if (newCostPrice < 0)
                    newCostPrice = 0;

                if (kitchen.IsInventoryItem ?? false)
                {
                    var averagePriceHis = averagePriceHistories.SingleOrDefault(ib => ib.TransactionNo == entity.TransactionNo && ib.ItemID == item.ItemID);
                    if (averagePriceHis == null)
                    {
                        averagePriceHis = averagePriceHistories.AddNew();
                        averagePriceHis.TransactionCode = entity.TransactionCode;
                        averagePriceHis.TransactionNo = entity.TransactionNo;
                        averagePriceHis.ItemUnit = kitchen.SRItemUnit;
                        averagePriceHis.ItemID = item.ItemID;
                        averagePriceHis.ChangedDate = entity.ApprovedDate;
                        averagePriceHis.OldAveragePrice = kitchen.CostPrice;
                        averagePriceHis.NewAveragePrice = newCostPrice;
                        averagePriceHis.LastUpdateByUserID = userID;
                        averagePriceHis.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;

                        averagePriceHis.OldPriceInBaseUnit = oldPriceInBaseUnit;
                        averagePriceHis.OldPriceInPurchaseUnit = oldPriceInPurchaseUnit;
                        averagePriceHis.OldPurchaseDiscount1 = oldPurchaseDiscount1;
                        averagePriceHis.OldPurchaseDiscount2 = oldPurchaseDiscount2;
                        averagePriceHis.NewPriceInBaseUnit = kitchen.PriceInBaseUnit;
                        averagePriceHis.NewPriceInPurchaseUnit = kitchen.PriceInPurchaseUnit;
                        averagePriceHis.NewPurchaseDiscount1 = kitchen.PurchaseDiscount1;
                        averagePriceHis.NewPurchaseDiscount2 = kitchen.PurchaseDiscount2;
                    }
                }

                kitchen.CostPrice = newCostPrice;

                //jika costprice nol, maka reset harga jd nol
                if (newCostPrice == 0)
                {
                    kitchen.PriceInBaseUnit = 0;
                    kitchen.PriceInBasedUnitWVat = 0;
                    kitchen.PriceInPurchaseUnit = 0;
                    kitchen.PurchaseDiscount1 = 0;
                    kitchen.PurchaseDiscount2 = 0;
                    kitchen.LastPriceInBaseUnit = 0;
                    kitchen.PriceWVat = 0;
                }

                //item.CostPrice = newCostPrice;
                //item.LastUpdateByUserID = userID;
                //item.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
                //}
            }
        }

        private static SupplierItemCollection PrepareSupplierItems(ItemTransactionItemCollection coll, string userID, string supplierID, string srItemType)
        {
            var supplierItems = new SupplierItemCollection();
            supplierItems.Query.Where(
                supplierItems.Query.SupplierID == supplierID,
                supplierItems.Query.ItemID.In((coll.Select(c => c.ItemID)).Distinct())
                );
            supplierItems.LoadAll();

            foreach (var item in coll)
            {
                SupplierItem suppItem = null;

                var isAvailable = false;
                foreach (var findItem in supplierItems.Where(findItem => findItem.ItemID.Equals(item.ItemID)))
                {
                    isAvailable = true;
                    suppItem = findItem;
                    break;
                }

                if (!isAvailable)
                {
                    suppItem = supplierItems.AddNew();
                    suppItem.SupplierID = supplierID;
                    suppItem.ItemID = item.ItemID;
                    suppItem.IsActive = true;
                }
                suppItem.PurchaseDiscount1 = item.Discount1Percentage;
                suppItem.PurchaseDiscount2 = item.Discount2Percentage;
                suppItem.SRPurchaseUnit = item.SRItemUnit;
                suppItem.ConversionFactor = item.ConversionFactor;

                //decimal conversionFactor = 1;
                //switch (srItemType)
                //{
                //    case ItemType.Medical:
                //        var ipm = new ItemProductMedic();
                //        if (ipm.LoadByPrimaryKey(item.ItemID))
                //            conversionFactor = ipm.ConversionFactor ?? 1;
                //        break;
                //    case ItemType.NonMedical:
                //        var ipnm = new ItemProductNonMedic();
                //        if (ipnm.LoadByPrimaryKey(item.ItemID))
                //            conversionFactor = ipnm.ConversionFactor ?? 1;
                //        break;
                //    case ItemType.Kitchen:
                //        var ik = new ItemKitchen();
                //        if (ik.LoadByPrimaryKey(item.ItemID))
                //            conversionFactor = ik.ConversionFactor ?? 1;
                //        break;
                //}
                suppItem.PriceInPurchaseUnit = (item.PriceInCurrency / item.ConversionFactor) * item.ConversionFactor;
                suppItem.LastUpdateByUserID = userID;
                suppItem.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
            }

            return supplierItems;
        }

        private static void PrepareUpdateSupplierContract(ItemTransaction entity, bool isApproval, string userID,
            out SupplierContract entitySc)
        {
            if (entity.TransactionCode != Reference.TransactionCode.PurchaseOrder)
            {
                entitySc = null;
                return;
            }
            if (string.IsNullOrEmpty(entity.ContractNo))
            {
                entitySc = null;
                return;
            }

            entitySc = new SupplierContract();
            entitySc.LoadByPrimaryKey(entity.ContractNo);
            if (isApproval)
            {
                entitySc.PurchaseAmount += (entity.ChargesAmount + entity.DiscountAmount);
                entitySc.DiscountAmount += entity.DiscountAmount;
            }
            else
            {
                entitySc.PurchaseAmount -= (entity.ChargesAmount + entity.DiscountAmount);
                entitySc.DiscountAmount -= entity.DiscountAmount;
            }

            entitySc.LastUpdateByUserID = userID;
            entitySc.LastUpdateDateTime = Utils.NowAtSqlServer(); //DateTime.Now;
        }

        private static ItemTransactionItemBakCollection PrepareReceivedItemDetailForBackup(IEnumerable<ItemTransactionItem> coll, string transactionNo)
        {
            var baks = new ItemTransactionItemBakCollection();
            baks.Query.Where(baks.Query.TransactionNo == transactionNo);
            baks.LoadAll();
            baks.MarkAllAsDeleted();

            foreach (var item in coll)
            {
                var entity = baks.AddNew();
                entity.TransactionNo = item.TransactionNo;
                entity.SequenceNo = item.SequenceNo;
                entity.ItemID = item.ItemID;
                entity.ReferenceNo = item.ReferenceNo;
                entity.ReferenceSequenceNo = item.ReferenceSequenceNo;
                entity.Quantity = item.Quantity;
                entity.SRItemUnit = item.SRItemUnit;
                entity.ConversionFactor = item.ConversionFactor;
                entity.QuantityFinishInBaseUnit = item.QuantityFinishInBaseUnit;
                entity.PageNo = item.PageNo;
                entity.CostPrice = item.CostPrice;
                entity.Price = item.Price;
                entity.PriceInCurrency = item.PriceInCurrency;
                entity.Discount1Percentage = item.Discount1Percentage;
                entity.Discount2Percentage = item.Discount2Percentage;
                entity.BatchNumber = item.BatchNumber;
                entity.ExpiredDate = item.ExpiredDate;
                entity.IsPackage = item.IsPackage;
                entity.IsBonusItem = item.IsBonusItem;
                entity.IsClosed = item.IsClosed;
                entity.Description = item.Description;
                entity.LastUpdateDateTime = item.LastUpdateDateTime;
                entity.LastUpdateByUserID = item.LastUpdateByUserID;
                entity.RequestQty = item.RequestQty;
                entity.Discount = item.Discount;
                entity.DiscountInCurrency = item.DiscountInCurrency;
                entity.IsDiscountInPercent = item.IsDiscountInPercent;
                entity.IsInvoiceUpdate = item.IsInvoiceUpdate;
                entity.PriorPrice = item.PriorPrice;
                entity.PriorPriceInCurrency = item.PriorPriceInCurrency;
                entity.PriorDiscount1Percentage = item.PriorDiscount1Percentage;
                entity.PriorDiscount2Percentage = item.PriorDiscount2Percentage;
                entity.PriorDiscount = item.PriorDiscount;
                entity.PriorDiscountInCurrency = item.PriorDiscountInCurrency;
                entity.LastInvoiceUpdateDateTime = item.LastInvoiceUpdateDateTime;
                entity.LastInvoiceUpdateByUserID = item.LastInvoiceUpdateByUserID;
                entity.HistoryPrice = item.HistoryPrice;
                entity.HistoryPriceInCurrency = item.HistoryPriceInCurrency;
                entity.HistoryDiscount1Percentage = item.HistoryDiscount1Percentage;
                entity.HistoryDiscount2Percentage = item.HistoryDiscount2Percentage;
                entity.HistoryDiscount = item.HistoryDiscount;
                entity.HistoryDiscountInCurrency = item.HistoryDiscountInCurrency;
                entity.Specification = item.Specification;
            }

            return baks;
        }

        private static BalanceType GetBalanceType(string transactionCode)
        {
            switch (transactionCode)
            {
                case Reference.TransactionCode.PrescriptionReturn:
                case Reference.TransactionCode.PurchaseOrderReceive:
                case Reference.TransactionCode.DistributionConfirm:
                case Reference.TransactionCode.ConsignmentReceive:
                case Reference.TransactionCode.DirectPurchase:
                case Reference.TransactionCode.SalesToBranchReturn:
                case Reference.TransactionCode.ReceiptOfSubstitute:
                case Reference.TransactionCode.ConsignmentTransfer:
                case Reference.TransactionCode.GrantsReceive:
                case Reference.TransactionCode.SalesReturn:
                    return BalanceType.QtyIn;
            }
            return BalanceType.QtyOut;
        }

        private enum BalanceType
        {
            QtyIn,
            QtyOut
        }

        public static string IsItemMinusProcess(string transactionNo, ItemTransactionItemCollection coll)
        {
            var entity = new ItemTransaction();
            entity.LoadByPrimaryKey(transactionNo);

            var itemId = GetItemBalanceMinus(entity, coll, GetLocationID(entity));
            if (itemId != string.Empty)
                return "Transaction can't be Approved because Item ID : " + itemId + " will be minus.";

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsEnabledStockWithEdControl).ToLower() == "yes")
            {
                var collEd = new ItemTransactionItemEdCollection();
                collEd.Query.Where(collEd.Query.TransactionNo == transactionNo);
                collEd.LoadAll();

                itemId = GetItemBalanceDetailEdMinus(entity, collEd, GetLocationID(entity));
                if (itemId != string.Empty)
                    return "Transaction can't be Approved because Item ID : " + itemId + " will be minus.";
            }

            return string.Empty;
        }

        private static string GetItemBalanceMinus(esItemTransaction entity, IEnumerable<ItemTransactionItem> coll, string locationID)
        {
            if (!IsNeedUpdateBalance(entity.TransactionCode))
                return string.Empty;

            var balanceType = GetBalanceType(entity.TransactionCode);

            var retval = string.Empty;

            foreach (var item in coll)
            {
                if (!(entity.IsInventoryItem ?? false)) continue;

                if (balanceType != BalanceType.QtyOut)
                    continue;

                var itemBalance = new ItemBalance();
                if (itemBalance.LoadByPrimaryKey(locationID, item.ItemID))
                {
                    if ((itemBalance.Balance - (item.Quantity * item.ConversionFactor)) < 0)
                    {
                        if (retval == string.Empty)
                            retval = item.ItemID;
                        else
                            retval += ", " + item.ItemID;
                    }
                }
                else
                {
                    if (retval == string.Empty)
                        retval = item.ItemID;
                    else
                        retval += ", " + item.ItemID;
                }

            }
            return retval;
        }

        private static string GetItemBalanceDetailEdMinus(esItemTransaction entity, IEnumerable<ItemTransactionItemEd> coll, string locationID)
        {
            var retval = string.Empty;

            foreach (var item in coll)
            {
                if (!(entity.IsInventoryItem ?? false)) continue;

                var balance = new ItemBalanceDetailEd();
                if (balance.LoadByPrimaryKey(locationID, item.ItemID, item.ExpiredDate ?? DateTime.Now, item.BatchNumber))
                {
                    if ((balance.Balance - (item.Quantity * item.ConversionFactor)) < 0)
                    {
                        if (retval == string.Empty)
                            retval = item.ItemID + "(BN: " + item.BatchNumber + " | ED: "+ item.ExpiredDate.Value.ToString("dd-MMM-yyyy") + ")";
                        else
                            retval += ", " + item.ItemID + "(BN: " + item.BatchNumber + " | ED: " + item.ExpiredDate.Value.ToString("dd-MMM-yyyy") + ")";
                    }
                }
                else
                {
                    if (retval == string.Empty)
                        retval = item.ItemID + "(BN: " + item.BatchNumber + " | ED: " + item.ExpiredDate.Value.ToString("dd-MMM-yyyy") + ")";
                    else
                        retval += ", " + item.ItemID + "(BN: " + item.BatchNumber + " | ED: " + item.ExpiredDate.Value.ToString("dd-MMM-yyyy") + ")";
                }
            }
            return retval;
        }

        public static string IsItemMinusProcessForPurchaseOrderReceivedConsignment(string transactionNo, ItemTransactionItemCollection coll, string locationId)
        {
            var entity = new ItemTransaction();
            entity.LoadByPrimaryKey(transactionNo);

            var itemId = GetItemBalanceMinusForPurchaseOrderReceivedConsignment(entity, coll, locationId);
            if (itemId != string.Empty)
                return "Transaction can't be Approved because Item ID : " + itemId + " on Location Consignment will be minus.";
            return string.Empty;
        }

        private static string GetItemBalanceMinusForPurchaseOrderReceivedConsignment(esItemTransaction entity, IEnumerable<ItemTransactionItem> coll, string locationID)
        {
            if (!IsNeedUpdateBalance(entity.TransactionCode))
                return string.Empty;

            var retval = string.Empty;

            foreach (var item in coll)
            {
                var itemBalance = new ItemBalance();
                if (itemBalance.LoadByPrimaryKey(locationID, item.ItemID))
                {
                    if ((itemBalance.Balance - (item.Quantity * item.ConversionFactor)) < 0)
                    {
                        if (retval == string.Empty)
                            retval = item.ItemID;
                        else
                            retval += ", " + item.ItemID;
                    }
                }
                else
                {
                    if (retval == string.Empty)
                        retval = item.ItemID;
                    else
                        retval += ", " + item.ItemID;
                }
            }
            return retval;
        }

        public static string IsExceedOrderQuantityForPurchaseOrderReceived(ItemTransactionItemCollection coll)
        {
            var itemId = GetItemExceedOrderQuantityForPurchaseOrderReceived(coll);
            if (itemId != string.Empty)
                return "This transaction can't be saved because Item ID : " + itemId + " will exceed the order quantity. Please check back your transaction.";//"This transaction can't be approved because Item ID : " + itemId + " will exceed the order quantity. Please check back your transaction.";
            return string.Empty;
        }

        private static string GetItemExceedOrderQuantityForPurchaseOrderReceived(IEnumerable<ItemTransactionItem> coll)
        {
            var retval = string.Empty;

            foreach (var item in coll)
            {
                if (item.IsBonusItem == false)
                {
                    var poiQ = new ItemTransactionItemQuery("a");
                    var poQ = new ItemTransactionQuery("b");
                    poiQ.InnerJoin(poQ).On(poQ.TransactionNo == poiQ.TransactionNo && poQ.IsVoid == false);
                    poiQ.Where(poiQ.TransactionNo != item.TransactionNo, poiQ.ReferenceNo == item.ReferenceNo, poiQ.ReferenceSequenceNo == item.ReferenceSequenceNo);
                    poiQ.Select(poiQ.ReferenceNo, poiQ.ReferenceSequenceNo, @"<SUM(a.Quantity * a.ConversionFactor) AS QtyFinished>");
                    poiQ.GroupBy(poiQ.ReferenceNo, poiQ.ReferenceSequenceNo);
                    DataTable poiDtb = poiQ.LoadDataTable();

                    decimal qtyProceed = 0;
                    if (poiDtb.Rows.Count > 0)
                        qtyProceed = Convert.ToDecimal(poiDtb.Rows[0]["QtyFinished"]);

                    var poItem = new ItemTransactionItem();
                    if (poItem.LoadByPrimaryKey(item.ReferenceNo, item.ReferenceSequenceNo))
                    {
                        var qtyFinisehd = qtyProceed;//poItem.QuantityFinishInBaseUnit;
                        var qtyPo = poItem.Quantity * poItem.ConversionFactor;
                        var qtyRemains = qtyPo - qtyFinisehd;
                        if (qtyRemains < 0)
                            qtyRemains = 0;

                        if (item.Quantity * item.ConversionFactor > qtyRemains)
                        {
                            if (retval == string.Empty)
                                retval = item.ItemID + " (Qty: " + string.Format("{0:N2}", qtyFinisehd) + ")";
                            else
                                retval += ", " + item.ItemID + " (Qty: " + string.Format("{0:N2}", qtyFinisehd) + ")";
                        }
                    }
                    else
                    {
                        if (retval == string.Empty)
                            retval = item.ItemID;
                        else
                            retval += ", " + item.ItemID;
                    }
                }
                
            }
            return retval;
        }

        public static string IsItemAlreadyProcess(string transactionNo, ItemTransactionItemCollection coll)
        {
            var entity = new ItemTransaction();
            entity.LoadByPrimaryKey(transactionNo);

            var itemId = GetItemAlreadyProcess(entity, coll);
            if (itemId != string.Empty)
                return "Transaction can't be Approved because Item ID : " + itemId + " will be reached the limit of the process.";
            return string.Empty;
        }

        private static string GetItemAlreadyProcess(esItemTransaction entity, IEnumerable<ItemTransactionItem> coll)
        {
            var retval = string.Empty;

            foreach (var item in coll)
            {
                if (string.IsNullOrEmpty(item.ReferenceNo))
                    continue;

                decimal qty = (item.Quantity ?? 0) * (item.ConversionFactor ?? 0);
                decimal qtyRequest = 0;
                decimal qtyFinished = 0;

                var reff = new ItemTransactionItem();
                if (reff.LoadByPrimaryKey(item.ReferenceNo, item.ReferenceSequenceNo))
                {
                    qtyRequest = (reff.Quantity ?? 0) * (reff.ConversionFactor ?? 0);
                    //qtyFinished = (reff.QuantityFinishInBaseUnit ?? 0);
                }

                var reffDt = new ItemTransactionItemQuery("dt");
                var reffHd = new ItemTransactionQuery("hd");
                reffDt.InnerJoin(reffHd).On(reffHd.TransactionNo == reffDt.TransactionNo && reffHd.IsApproved == true);
                reffDt.Where(reffDt.TransactionNo != entity.TransactionNo, reffDt.ReferenceNo == item.ReferenceNo, reffDt.ReferenceSequenceNo == item.ReferenceSequenceNo);
                reffDt.Select(@"<ISNULL(SUM(dt.Quantity * dt.ConversionFactor), 0) AS QtyFinished>");
                DataTable reffDtb = reffDt.LoadDataTable();
                if (reffDtb.Rows.Count > 0)
                    qtyFinished = Convert.ToDecimal(reffDtb.Rows[0]["QtyFinished"]);

                if (qtyRequest < qty + qtyFinished)
                {
                    if (retval == string.Empty)
                        retval = item.ItemID;
                    else
                        retval += ", " + item.ItemID;
                }
            }
            return retval;
        }

        #endregion

        #region Approve/UnApprove Non Master

        public string ApproveNonMaster(string transactionNo, ItemTransactionItemCollection coll, string userID)
        {
            var entity = new ItemTransaction();
            entity.LoadByPrimaryKey(transactionNo);
            return ApproveProcessNonMaster(entity, coll, userID, true);
        }

        public string UnApproveNonMaster(string transactionNo, ItemTransactionItemCollection coll, string userID)
        {
            var entity = new ItemTransaction();
            entity.LoadByPrimaryKey(transactionNo);
            return ApproveProcessNonMaster(entity, coll, userID, false);
        }

        private static string ApproveProcessNonMaster(ItemTransaction entity, ItemTransactionItemCollection coll, string userID, bool isApproval)
        {
            if (isApproval)
            {
                if (entity.IsApproved ?? false)
                    return "Approved";
                if (entity.IsVoid ?? false)
                    return "Void";
            }
            else
            {
                if (entity.IsInventoryItem ?? false && !IsNeedUpdateBalance(entity.TransactionCode))
                {
                    //Transaksi yg bila di approve akan mengupdate balance, tidak bisa di UnApprove
                    return "CantUnApprove";
                }
                if (!entity.IsApproved ?? false)
                    return "UnApproved";
            }

            entity.IsApproved = isApproval;
            entity.ApprovedDate = Utils.NowAtSqlServer(); //DateTime.Now;
            entity.ApprovedByUserID = userID;

            ItemTransaction entityRef;
            ItemTransactionItemCollection collRef;
            ItemTransactionItemEdCollection collEdRef;
            PrepareUpdateReferenceItem(entity.TransactionCode, entity.ReferenceNo, entity.TransactionNo, isApproval, userID, out entityRef,
                out collRef, out collEdRef);

            ItemTransactionItemBakCollection baks = null;
            if (entity.TransactionCode == Reference.TransactionCode.PurchaseOrderReceive)
                baks = PrepareReceivedItemDetailForBackup(coll, entity.TransactionNo);

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                coll.Save();

                if (entityRef != null)
                    entityRef.Save();
                if (collRef != null)
                    collRef.Save();
                if (collEdRef != null)
                    collEdRef.Save();
                if (baks != null)
                    baks.Save();

                var app = new AppParameter();

                if (entity.TransactionCode == Reference.TransactionCode.PurchaseOrderReceive)
                {
                    app.LoadByPrimaryKey("acc_IsAutoJournalPOReceived");
                    if (app.ParameterValue == "Yes")
                    {
                        DateTime jDate = (new DateTime()).NowAtSqlServer();
                        jDate = AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_JournalPORDate).Equals("0") ?
                            entity.TransactionDate.Value.Date : entity.ApprovedDate.Value.Date;

                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(jDate);
                        if (isClosingPeriod)
                            return "Financial statements for period: " +
                                   string.Format("{0:MMMM-yyyy}", jDate) +
                                   " have been closed. Please contact the authorities.";

                        //if (entity.SRPurchaseOrderType == "CR")
                        //{
                        /* Automatic Journal Testing Start */

                        int? journalId;

                        //if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSIAMTP")
                        //    journalId = JournalTransactions.AddNewPurchaseOrderReceivedJournalNonVat(entity, userID, 0);
                        //else if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSUI" ||
                        //    AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion) == "RSUTAMA")
                        //    journalId = JournalTransactions.AddNewPurchaseOrderReceivedJournalVer2(entity, userID, 0);
                        //else
                        journalId = JournalTransactions.AddNewPurchaseOrderReceivedJournal(entity, userID, 0);

                        /* Automatic Journal Testing End */
                        //}
                    }

                }

                trans.Complete();
            }
            return string.Empty;
        }

        #endregion

        #region Rounding
        public static decimal RoundingDiff { get; set; }

        public static decimal Rounding(decimal xnValue, decimal xnSetup)
        {
            decimal hsl;
            if (xnSetup == 0)
                hsl = xnValue;
            else
            {
                var signVal = Math.Sign(xnValue);
                xnValue = Math.Abs(xnValue);
                var sisaBulat = (xnValue % xnSetup);

                if (xnSetup <= 0)
                    xnSetup = 1;

                hsl = sisaBulat > 0 ? (xnValue - (xnValue % xnSetup) + xnSetup) * signVal : (xnValue - (xnValue % xnSetup)) * signVal;
            }
            RoundingDiff = hsl - xnValue;
            return hsl;
        }
        #endregion

        #region Update Cost Price
        public static void UpdateCostPriceForPOR(ItemTransaction hd, ItemTransactionItemCollection coll, out string itemZeroCostPrice)
        {
            itemZeroCostPrice = string.Empty;

            string parCostType = AppParameter.GetParameterValue(AppParameter.ParameterItem.ItemProductCostPriceType);
            if (parCostType == "AVG")
            {
                decimal taxPercentage = hd.TaxPercentage ?? 0;

                foreach (var entity in coll)
                {
                    var item = new Item();
                    item.LoadByPrimaryKey(entity.ItemID);
                    decimal price = 0;
                    switch (item.SRItemType)
                    {
                        case Reference.ItemType.Medical:
                            var med = new ItemProductMedic();
                            med.LoadByPrimaryKey(item.ItemID);

                            if (med.PriceInBaseUnit > 0)
                                entity.CostPrice = med.CostPrice;
                            else
                                entity.CostPrice = ((entity.PriceInCurrency - entity.Discount) / entity.ConversionFactor) * (1 + (taxPercentage / 100));
                            price = med.PriceInBaseUnit ?? 0;
                            if (!med.IsInventoryItem ?? false)
                                continue;

                            break;
                        case Reference.ItemType.NonMedical:
                            var nmed = new ItemProductNonMedic();
                            nmed.LoadByPrimaryKey(item.ItemID);

                            if (nmed.PriceInBaseUnit > 0)
                                entity.CostPrice = nmed.CostPrice;
                            else
                                entity.CostPrice = ((entity.PriceInCurrency - entity.Discount) / entity.ConversionFactor) * (1 + (taxPercentage / 100));
                            price = nmed.PriceInBaseUnit ?? 0;
                            if (!nmed.IsInventoryItem ?? false)
                                continue;

                            break;
                        case Reference.ItemType.Kitchen:
                            var kitc = new ItemKitchen();
                            kitc.LoadByPrimaryKey(item.ItemID);

                            if (kitc.PriceInBaseUnit > 0)
                                entity.CostPrice = kitc.CostPrice;
                            else
                                entity.CostPrice = ((entity.PriceInCurrency - entity.Discount) / entity.ConversionFactor) * (1 + (taxPercentage / 100));
                            price = kitc.PriceInBaseUnit ?? 0;
                            if (!kitc.IsInventoryItem ?? false)
                                continue;

                            break;
                        default:
                            continue;
                    }

                    if ((entity.CostPrice ?? 0) == 0 && price > 0 && entity.IsBonusItem == false)
                    {
                        itemZeroCostPrice = item.ItemName;
                        return;
                    }
                }
            }
        }

        public static void UpdateCostPrice(ItemTransactionItemCollection coll, out string itemZeroCostPrice)
        {
            itemZeroCostPrice = string.Empty;
            foreach (var entity in coll)
            {
                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);
                decimal price = 0;
                decimal pctgDisc = 0;
                switch (item.SRItemType)
                {
                    case Reference.ItemType.Medical:
                        var med = new ItemProductMedic();
                        med.LoadByPrimaryKey(item.ItemID);
                        entity.CostPrice = med.CostPrice;
                        price = med.PriceInBaseUnit ?? 0;
                        pctgDisc = med.PurchaseDiscount1 ?? 0;
                        if (!med.IsInventoryItem ?? false)
                            continue;

                        break;
                    case Reference.ItemType.NonMedical:
                        var nmed = new ItemProductNonMedic();
                        nmed.LoadByPrimaryKey(item.ItemID);
                        entity.CostPrice = nmed.CostPrice;
                        price = nmed.PriceInBaseUnit ?? 0;
                        pctgDisc = nmed.PurchaseDiscount1 ?? 0;
                        if (!nmed.IsInventoryItem ?? false)
                            continue;

                        break;
                    case Reference.ItemType.Kitchen:
                        var kitc = new ItemKitchen();
                        kitc.LoadByPrimaryKey(item.ItemID);
                        entity.CostPrice = kitc.CostPrice;
                        price = kitc.PriceInBaseUnit ?? 0;
                        pctgDisc = kitc.PurchaseDiscount1 ?? 0;
                        if (!kitc.IsInventoryItem ?? false)
                            continue;

                        break;
                    default:
                        continue;
                }

                if ((entity.CostPrice ?? 0) == 0 && price > 0 && pctgDisc < 100)
                {
                    itemZeroCostPrice = item.ItemName;
                    return;
                }
            }
        }
        #endregion

        public void Void(ItemTransaction entity, ItemTransactionItemCollection coll, string userID)
        {
            using (var trans = new esTransactionScope())
            {
                entity.IsVoid = true;
                entity.VoidDate = Utils.NowAtSqlServer(); //DateTime.Now;
                entity.VoidByUserID = userID;
                entity.IsApproved = false;
                entity.Save();

                var entityRef = new ItemTransaction();
                entityRef.LoadByPrimaryKey(entity.ReferenceNo);
                entityRef.IsClosed = false;
                entityRef.Save();

                if (coll.Count > 0)
                {
                    var collRef = new ItemTransactionItemCollection();
                    collRef.Query.Where(
                        collRef.Query.TransactionNo == entity.ReferenceNo &&
                        collRef.Query.SequenceNo.In((coll.Select(c => c.ReferenceSequenceNo)).Distinct())
                        );
                    foreach (var c in collRef.Where(c => (c.IsClosed ?? false)))
                    {
                        c.IsClosed = false;
                    }
                    collRef.Save();
                }

                //rollback balance
                foreach (var c in coll)
                {
                    var ib = new ItemBalance();
                    if (ib.LoadByPrimaryKey(entity.ToLocationID, c.ItemID))
                    {
                        ib.Balance -= (c.Quantity * c.ConversionFactor);
                        ib.Save();
                    }

                    var ibd = new ItemBalanceDetail();
                    ibd.Query.Where(
                        ibd.Query.LocationID == entity.ToLocationID &&
                        ibd.Query.ItemID == c.ItemID &&
                        ibd.Query.ReferenceNo == entity.TransactionNo
                        );
                    if (ibd.Query.Load())
                    {
                        ibd.MarkAsDeleted();
                        ibd.Save();
                    }

                    var im = new ItemMovement();
                    im.Query.Where(
                        im.Query.TransactionNo == entity.TransactionNo &&
                        im.Query.SequenceNo == c.SequenceNo
                        );
                    if (im.Query.Load())
                    {
                        im.MarkAsDeleted();
                        im.Save();
                    }
                }

                // rollback journal
                var jr = new JournalTransactions();
                jr.Query.Where(
                    jr.Query.RefferenceNumber == entity.TransactionNo &&
                    jr.Query.IsVoid == false
                    );
                if (jr.Query.Load())
                {
                    var jrd = new JournalTransactionDetailsCollection();
                    jrd.Query.Where(jrd.Query.JournalId == jr.JournalId);
                    if (jrd.Query.Load())
                    {
                        jrd.MarkAllAsDeleted();
                        jrd.Save();
                    }

                    jr.MarkAsDeleted();
                    jr.Save();
                }

                var bak = new ItemTransactionItemBakCollection();
                bak.Query.Where(bak.Query.TransactionNo == entity.TransactionNo);
                bak.LoadAll();
                bak.MarkAllAsDeleted();
                bak.Save();

                trans.Complete();
            }
        }

        public void UnApproved(ItemTransaction entity, ItemTransactionItemCollection coll, string userID)
        {
            using (var trans = new esTransactionScope())
            {
                entity.IsApproved = false;
                entity.ApprovedByUserID = userID;
                entity.ApprovedDate = Utils.NowAtSqlServer(); //DateTime.Now;
                entity.Save();

                ItemTransaction entityRef;
                ItemTransactionItemCollection collRef;
                ItemTransactionItemEdCollection collEdRef;
                PrepareUpdateReferenceItem(entity.TransactionCode, entity.ReferenceNo, entity.TransactionNo, false, userID, out entityRef,
                    out collRef, out collEdRef);

                entityRef.Save();
                collRef.Save();
                if (collEdRef != null)
                    collEdRef.Save();

                var avgPriceHistColl = new AveragePriceHistoryCollection();
                avgPriceHistColl.Query.Where(avgPriceHistColl.Query.TransactionNo == entity.TransactionNo);
                if (avgPriceHistColl.LoadAll())
                {
                    avgPriceHistColl.MarkAllAsDeleted();
                    avgPriceHistColl.Save();
                }

                // TODO: Bikin jurnal balik, jangan dihapus jurnalnya

                // rollback journal
                var jr = new JournalTransactions();
                jr.Query.Where(
                    jr.Query.RefferenceNumber == entity.TransactionNo &&
                    jr.Query.IsVoid == false
                    );
                if (jr.Query.Load())
                {
                    var jrd = new JournalTransactionDetailsCollection();
                    jrd.Query.Where(jrd.Query.JournalId == jr.JournalId);
                    if (jrd.Query.Load())
                    {
                        jrd.MarkAllAsDeleted();
                        jrd.Save();
                    }

                    jr.MarkAsDeleted();
                    jr.Save();
                }

                var bak = new ItemTransactionItemBakCollection();
                bak.Query.Where(bak.Query.TransactionNo == entity.TransactionNo);
                bak.LoadAll();
                bak.MarkAllAsDeleted();
                bak.Save();

                trans.Complete();
            }
        }

        public static bool IsUnApproveAble(string transactionNo)
        {
            var app = new AppParameter();
            app.LoadByPrimaryKey("IsTransWAutoJournalCanUnApproveIfPerClosed");

            if (!app.ParameterValue.ToLower().Equals("yes")) return true;

            var entity = new ItemTransaction();
            entity.LoadByPrimaryKey(transactionNo);

            if (entity.IsVoid ?? false)
                return false;

            var isCheckCloseStatus = false;
            switch (entity.TransactionCode)
            {
                case Reference.TransactionCode.PurchaseOrderReceive:
                case Reference.TransactionCode.PurchaseOrderReturn:
                case Reference.TransactionCode.ReceiptOfSubstitute:
                    isCheckCloseStatus = true;
                    break;
            }
            if (isCheckCloseStatus)
                return PostingStatus.IsPeriodeClosed(Convert.ToDateTime(entity.ApprovedDate));
            else
                return true;
        }

    }
}