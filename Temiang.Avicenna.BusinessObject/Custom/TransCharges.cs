using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransCharges
    {
        public override void Save()
        {
            if(!this.es.IsDeleted && string.IsNullOrEmpty(this.ToServiceUnitID)) throw new Exception("Empty To Service Unit ID, Operation Aborted!");
            base.Save();
        }

        private static ItemBalanceCollection PrepareItemBalances(TransPrescription transPrescription, string locationID, string userID, bool isApproval)
        {
            string tCode;
            if (transPrescription.IsPrescriptionReturn ?? false)
                tCode = "094";
            else
                tCode = "091";

            TransPrescriptionItemQuery transPrescriptionQ = new TransPrescriptionItemQuery("a");
            transPrescriptionQ.Select
                (
                    transPrescriptionQ.ItemID,
                    (transPrescriptionQ.ResultQty).Sum().As("Quantity")
                );
            transPrescriptionQ.GroupBy(transPrescriptionQ.ItemID);
            transPrescriptionQ.Where(transPrescriptionQ.PrescriptionNo == transPrescription.PrescriptionNo);

            DataTable dtbItemTrans = transPrescriptionQ.LoadDataTable();

            ItemBalanceQuery balQ = new ItemBalanceQuery("b");
            balQ.es.Distinct = true;
            balQ.Select
                (
                    balQ.LocationID,
                    balQ.ItemID,
                    balQ.Balance,
                    balQ.ReorderType,
                    balQ.Maximum,
                    balQ.Minimum,
                    balQ.LastUpdateByUserID,
                    balQ.LastUpdateDateTime
                );
            balQ.InnerJoin(transPrescriptionQ).On(balQ.ItemID == transPrescriptionQ.ItemID);
            balQ.Where
                (
                    transPrescriptionQ.PrescriptionNo == transPrescription.PrescriptionNo,
                    balQ.LocationID == locationID
                );


            ItemBalanceCollection itemBalances = new ItemBalanceCollection();
            itemBalances.Load(balQ);

            foreach (DataRow row in dtbItemTrans.Rows)
            {
                ItemBalance balance = null;
                bool isFound = false;
                foreach (ItemBalance findBalance in itemBalances)
                {
                    //Jika ItemID tidak ditemukan, maka tambah row
                    if (findBalance.ItemID.Equals(row["ItemID"]))
                    {
                        isFound = true;
                        balance = findBalance;
                        break;
                    }
                }

                if (!isFound)
                {
                    balance = itemBalances.AddNew();
                    balance.ItemID = row["ItemID"].ToString();
                    balance.LocationID = locationID;
                    if (tCode == "094")
                        if (isApproval)
                            balance.Balance = Convert.ToDecimal(row["Quantity"]);
                        else
                            balance.Balance = 0 - Convert.ToDecimal(row["Quantity"]);
                    else
                        if (isApproval)
                            balance.Balance = 0 - Convert.ToDecimal(row["Quantity"]);
                        else
                            balance.Balance = Convert.ToDecimal(row["Quantity"]);

                    balance.Minimum = 0;
                    balance.Maximum = 0;
                    balance.ReorderType = string.Empty;
                }
                else
                {
                    if (tCode == "094")
                        if (isApproval)
                            balance.Balance += Convert.ToDecimal(balance.GetColumn("TransQty"));
                        else
                            balance.Balance -= Convert.ToDecimal(balance.GetColumn("TransQty"));
                    else
                        if (isApproval)
                            balance.Balance -= Convert.ToDecimal(balance.GetColumn("TransQty"));
                        else
                            balance.Balance += Convert.ToDecimal(balance.GetColumn("TransQty"));
                }

                if (balance.es.IsModified || balance.es.IsAdded)
                {
                    balance.LastUpdateByUserID = userID;
                    balance.LastUpdateDateTime = DateTime.Now;
                }
            }

            return itemBalances;
        }

        #region Additional Field

        public string ToServiceUnitName
        {
            get { return GetColumn("refToServiceUnit_ToServiceUnitName").ToString(); }
            set { SetColumn("refToServiceUnit_ToServiceUnitName", value); }
        }

        public string FromServiceUnitName
        {
            get { return GetColumn("refToServiceUnit_FromServiceUnitName").ToString(); }
            set { SetColumn("refToServiceUnit_FromServiceUnitName", value); }
        }

        public string RegistrationTypeName
        {
            get { return GetColumn("refToAppStandardReferenceItem_RegistrationType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_RegistrationType", value); }
        }

        # endregion

        //#region Approve
        //private static string ValidateApproval(Registration reg, string QueryString_type,
        //    TransChargesItemCollection TransChargesItems, TransChargesItemCompCollection TransChargesItemComps,
        //    ref bool detailApproved)
        //{
        //    detailApproved = false;

        //    if (this.IsApproved ?? false)
        //    {
        //        // requery lagi memastikan transchargesdetail belum diapprove oleh thread yang lain
        //        // atau user yang lain
        //        var tcds = new TransChargesItemCollection();
        //        tcds.Query.Where(
        //            tcds.Query.TransactionNo.Equal(entity.TransactionNo),
        //            tcds.Query.IsVoid.Equal(false),
        //            tcds.Query.ParentNo.Equal(string.Empty));
        //        if (tcds.LoadAll())
        //        {
        //            foreach (var tcd in tcds)
        //            {
        //                detailApproved = detailApproved || (tcd.IsApprove ?? false);
        //            }
        //        }

        //        if (detailApproved)
        //        {
        //            return AppConstant.Message.RecordHasApproved;
        //        }
        //    }
        //    if (entity.IsVoid ?? false)
        //    {
        //        return AppConstant.Message.RecordHasVoided;
        //    }

        //    bool isClosed = true, isLocked = true;
        //    var mergebilling = new MergeBilling();
        //    if (mergebilling.LoadByPrimaryKey(entity.RegistrationNo) && !string.IsNullOrEmpty(mergebilling.FromRegistrationNo))
        //    {
        //        var regmb = new Registration();
        //        if (regmb.LoadByPrimaryKey(mergebilling.FromRegistrationNo))
        //        {
        //            isClosed = regmb.IsClosed ?? false;
        //            isLocked = regmb.IsHoldTransactionEntry ?? false;
        //        }
        //    }

        //    reg.LoadByPrimaryKey(entity.RegistrationNo);
        //    if (isClosed && (reg.IsClosed ?? false))
        //    {
        //        return string.Format("Registration has been closed.");
        //    }

        //    if (isLocked && (reg.IsHoldTransactionEntry ?? false))
        //    {
        //        return string.Format("Transaction is locked.");
        //    }

        //    if (QueryString_type.Equals(string.Empty)) QueryString_type = "tr";

        //    if (QueryString_type != "jo")
        //    {
        //        foreach (var comp in TransChargesItemComps.Where(c => TransChargesItems.Where(t => string.IsNullOrEmpty(t.ParentNo)).Select(t => t.SequenceNo).Contains(c.SequenceNo)))
        //        {
        //            var tc = new TariffComponent();
        //            tc.LoadByPrimaryKey(comp.TariffComponentID);
        //            if ((tc.IsTariffParamedic ?? false) && string.IsNullOrEmpty(comp.ParamedicID))
        //            {
        //                var item = TransChargesItems.FindByPrimaryKey(comp.TransactionNo, comp.SequenceNo);

        //                return string.Format("Physician ID for {0} is not defined.", item.GetColumn("refToItem_ItemName"));
        //            }
        //        }

        //        if (AppSession.Parameter.IsAutoApprovePackage)
        //        {
        //            // validate approve header paket, harus cek detail paket ada yang autoapprove atau tidak, trus yang auto approve harus sudah ada dokternya
        //            foreach (var comp in TransChargesItemComps.Where(c => TransChargesItems.Where(t => !string.IsNullOrEmpty(t.ParentNo)).Select(t => t.SequenceNo).Contains(c.SequenceNo)))
        //            {
        //                var tc = new TariffComponent();
        //                tc.LoadByPrimaryKey(comp.TariffComponentID);
        //                if ((tc.IsTariffParamedic ?? false) && string.IsNullOrEmpty(comp.ParamedicID))
        //                {
        //                    var item = TransChargesItems.FindByPrimaryKey(comp.TransactionNo, comp.SequenceNo);
        //                    // jika auto approve

        //                    var itemPkg = TransChargesItems.Where(x => x.TransactionNo == item.TransactionNo && x.IsPackage == true).FirstOrDefault();
        //                    if (itemPkg != null)
        //                    {
        //                        var ipColl = new ItemPackageCollection();
        //                        ipColl.Query.Where(ipColl.Query.ItemID == itemPkg.ItemID,
        //                            ipColl.Query.DetailItemID == item.ItemID,
        //                            ipColl.Query.ServiceUnitID == item.ToServiceUnitID,
        //                            ipColl.Query.IsAutoApprove == true);
        //                        if (ipColl.LoadAll())
        //                        {
        //                            return string.Format("Physician ID for {0} in detail package for auto approve is not defined.", item.GetColumn("refToItem_ItemName"));
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return string.Empty;
        //}
        //public string Approve() {
        //    bool detailApproved = false;
        //    var reg = new Registration();
        //    var unit = new ServiceUnit();

        //    string valMsg = ValidateApproval(entity, reg, QueryString_type,
        //        TransChargesItems, TransChargesItemComps, ref detailApproved);
        //    if (!valMsg.Equals(string.Empty)) return valMsg;

        //    if (ClassID.Equals(string.Empty)) ClassID = entity.ClassID;
        //    if (FromServiceUnitID.Equals(string.Empty)) FromServiceUnitID = entity.FromServiceUnitID;
        //    if (ToServiceUnitID.Equals(string.Empty)) ToServiceUnitID = entity.ToServiceUnitID;


        //    using (var trans = new esTransactionScope())
        //    {
        //        if (string.IsNullOrEmpty(entity.LocationID))
        //            entity.LocationID = unit.GetMainLocationId(entity.ToServiceUnitID);

        //        if (!(entity.IsApproved ?? false) && detailApproved)
        //        {
        //            entity.IsApproved = true;

        //            entity.Save();
        //        }
        //        else if ((entity.IsApproved ?? false) && !detailApproved)
        //        {
        //            foreach (var tci in TransChargesItems)
        //            {
        //                if (!(tci.IsVoid ?? false))
        //                    tci.IsApprove = true;
        //            }
        //            TransChargesItems.Save();
        //        }
        //        else
        //        {
        //            //package manipulation
        //            //hanya paket dari mcu yang di pecah per unit tujuan
        //            //paket non mcu tidak dipecah
        //            //if ((QueryString_type == "mcu") && (entity.IsPackage ?? false))
        //            if (entity.IsPackage ?? false)
        //            {
        //                var headers = new TransChargesCollection();
        //                var details = new TransChargesItemCollection();
        //                var components = new TransChargesItemCompCollection();
        //                var consumptions = new TransChargesItemConsumptionCollection();

        //                var pacs = (TransChargesItems.Where(i => !string.IsNullOrEmpty(i.ParentNo) && (i.ParentNo.Length == 3))
        //                                             .GroupBy(i => new
        //                                             {
        //                                                 i.ParentNo,
        //                                                 i.ToServiceUnitID
        //                                             })
        //                                             .Select(g => new
        //                                             {
        //                                                 g.Key.ParentNo,
        //                                                 g.Key.ToServiceUnitID,
        //                                                 IsOrder = IsServiceUnitOrder(g.Key.ToServiceUnitID) &&
        //                                                    g.Key.ToServiceUnitID !=
        //                                                    ((QueryString_type == "tr" || QueryString_type == "npc") ? FromServiceUnitID : ToServiceUnitID)
        //                                             })).Distinct();

        //                foreach (var pac in pacs)
        //                {
        //                    var autoNumber = Helper.GetNewAutoNumber(TransactionDate.Date,
        //                        pac.IsOrder ? AppEnum.AutoNumber.JobOrderNo : AppEnum.AutoNumber.TransactionNo);
        //                    var transactionNo = autoNumber.LastCompleteNumber;
        //                    autoNumber.Save();

        //                    //header
        //                    #region header
        //                    var header = headers.AddNew();
        //                    header.TransactionNo = transactionNo;
        //                    header.RegistrationNo = entity.RegistrationNo;
        //                    header.TransactionDate = entity.TransactionDate;
        //                    header.ExecutionDate = entity.ExecutionDate;
        //                    header.ReferenceNo = string.Empty;
        //                    header.ResponUnitID = String.Empty;
        //                    header.FromServiceUnitID = (pac.IsOrder) ? entity.FromServiceUnitID : pac.ToServiceUnitID;
        //                    header.IsBillProceed = false;
        //                    header.IsApproved = pac.IsOrder;
        //                    header.ToServiceUnitID = pac.ToServiceUnitID;
        //                    header.ClassID = entity.ClassID;
        //                    header.RoomID = entity.RoomID;
        //                    header.BedID = entity.BedID;
        //                    header.DueDate = entity.DueDate;
        //                    header.SRShift = entity.SRShift;
        //                    header.SRItemType = string.Empty;
        //                    header.IsProceed = false;
        //                    header.IsVoid = false;
        //                    header.IsAutoBillTransaction = false;
        //                    header.IsOrder = pac.IsOrder;
        //                    header.IsCorrection = false;
        //                    header.Notes = string.Empty;
        //                    header.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //                    header.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //                    header.IsPackage = false;
        //                    header.PackageReferenceNo = entity.TransactionNo;
        //                    header.SurgicalPackageID = String.Empty;
        //                    header.LocationID = unit.GetMainLocationId(pac.ToServiceUnitID);
        //                    #endregion

        //                    var tcis = TransChargesItems.Where(t => t.ParentNo == pac.ParentNo &&
        //                                                            t.ToServiceUnitID == pac.ToServiceUnitID)
        //                                                .OrderBy(t => t.SequenceNo);

        //                    foreach (var tci in tcis)
        //                    {
        //                        //detail
        //                        #region detail
        //                        var detail = details.AddNew();
        //                        detail.TransactionNo = header.TransactionNo;
        //                        detail.SequenceNo = tci.SequenceNo;
        //                        detail.ReferenceNo = tci.ReferenceNo;
        //                        detail.ReferenceSequenceNo = tci.ReferenceSequenceNo;
        //                        detail.ItemID = tci.ItemID;
        //                        detail.ChargeClassID = tci.ChargeClassID;
        //                        detail.ParamedicID = tci.ParamedicID;
        //                        detail.SecondParamedicID = tci.SecondParamedicID;
        //                        detail.IsAdminCalculation = tci.IsAdminCalculation;
        //                        detail.IsVariable = tci.IsVariable;
        //                        detail.IsCito = tci.IsCito;
        //                        detail.ChargeQuantity = tci.ChargeQuantity;
        //                        detail.StockQuantity = tci.StockQuantity;
        //                        detail.SRItemUnit = tci.SRItemUnit;
        //                        detail.CostPrice = tci.CostPrice;
        //                        detail.Price = tci.Price;
        //                        detail.DiscountAmount = tci.DiscountAmount;
        //                        detail.CitoAmount = tci.CitoAmount;
        //                        detail.RoundingAmount = tci.RoundingAmount;
        //                        detail.SRDiscountReason = tci.SRDiscountReason;
        //                        detail.IsAssetUtilization = tci.IsAssetUtilization;
        //                        detail.AssetID = tci.AssetID;
        //                        detail.IsBillProceed = false;// (tci.IsVoid ?? false) ? false : pac.IsOrder;
        //                        //detail.IsBillProceed = AppParameter.GetParameterValue(AppParameter.ParameterItem.IsJobOrderRealizationNeedConfirm).ToLower() == "yes" ? false : pac.IsOrder;
        //                        detail.IsOrderRealization = tci.IsOrderRealization;
        //                        detail.IsPaymentConfirmed = tci.IsPaymentConfirmed;
        //                        detail.IsPackage = tci.IsPackage;
        //                        detail.IsApprove = (tci.IsVoid ?? false) ? false : pac.IsOrder;
        //                        detail.IsVoid = tci.IsVoid;
        //                        detail.Notes = tci.Notes;
        //                        detail.FilmNo = tci.FilmNo;

        //                        var item = new Item();
        //                        item.LoadByPrimaryKey(detail.ItemID);
        //                        if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH" && string.IsNullOrEmpty(detail.FilmNo))
        //                        {
        //                            if (item.Notes.Length > 0 && item.SRItemType != ItemType.Medical && item.SRItemType != ItemType.NonMedical && item.SRItemType != ItemType.Kitchen)
        //                            {
        //                                amplopFilmAutoNumber =
        //                                    Helper.GetNewAutoNumber(TransactionDate.Date,
        //                                                            AppEnum.AutoNumber.AmplopFilmNo,
        //                                                            item.Notes.Length >= 3
        //                                                                ? item.Notes.Substring(0, 3).ToUpper()
        //                                                                : item.Notes.ToUpper(),
        //                                                            AppSession.UserLogin.UserID);

        //                                var filmNo = amplopFilmAutoNumber.LastCompleteNumber;
        //                                amplopFilmAutoNumber.Save();

        //                                detail.FilmNo = filmNo;
        //                            }
        //                        }

        //                        detail.LastUpdateDateTime = tci.LastUpdateDateTime;
        //                        detail.LastUpdateByUserID = tci.LastUpdateByUserID;
        //                        detail.ParentNo = string.Empty;
        //                        detail.SRCenterID = tci.SRCenterID;
        //                        detail.AutoProcessCalculation = tci.AutoProcessCalculation;
        //                        detail.ParamedicCollectionName = tci.ParamedicCollectionName;
        //                        detail.ToServiceUnitID = tci.ToServiceUnitID;
        //                        detail.IsCitoInPercent = tci.IsCitoInPercent;
        //                        detail.BasicCitoAmount = tci.BasicCitoAmount;
        //                        detail.IsItemRoom = tci.IsItemRoom;
        //                        detail.IsItemRoom = false;

        //                        detail.SRCitoPercentage = tci.SRCitoPercentage;
        //                        detail.ItemConditionRuleID = tci.ItemConditionRuleID;

        //                        if (pac.IsOrder)
        //                            detail.IsOrderConfirmed = true;

        //                        if (tci.IsExtraItem ?? false)
        //                        {
        //                            detail.IsExtraItem = tci.IsExtraItem;
        //                            detail.IsSelectedExtraItem = tci.IsSelectedExtraItem;
        //                        }

        //                        // cek mapping serviceunit item service, jika belum ada mapping
        //                        // maka harus dimapping, mapping dibutuhkan untuk edit (untuk isi dokter per detail paket)
        //                        // dan jurnal
        //                        var suisColl = new ServiceUnitItemServiceCollection();
        //                        suisColl.Query.Where(suisColl.Query.ItemID.Equal(detail.ItemID),
        //                            suisColl.Query.ServiceUnitID.Equal(header.ToServiceUnitID));
        //                        if (!suisColl.LoadAll())
        //                        {
        //                            var nSuis = suisColl.AddNew();
        //                            nSuis.ServiceUnitID = header.ToServiceUnitID;
        //                            nSuis.ItemID = detail.ItemID;
        //                            nSuis.ChartOfAccountId = 0;
        //                            nSuis.SubledgerId = 0;
        //                            nSuis.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //                            nSuis.LastUpdateByUserID = "system";
        //                            nSuis.IsAllowEditByUserVerificated = true;
        //                            nSuis.IsVisible = true;
        //                            suisColl.Save();
        //                        }

        //                        #endregion

        //                        var tcis2 = TransChargesItems.Where(t => t.ParentNo == detail.SequenceNo)
        //                                                     .OrderBy(t => t.SequenceNo);
        //                        foreach (var tci2 in tcis2)
        //                        {
        //                            var detail2 = details.AddNew();
        //                            detail2.TransactionNo = header.TransactionNo;
        //                            detail2.SequenceNo = tci2.SequenceNo;
        //                            detail2.ReferenceNo = tci2.ReferenceNo;
        //                            detail2.ReferenceSequenceNo = tci2.ReferenceSequenceNo;
        //                            detail2.ItemID = tci2.ItemID;
        //                            detail2.ChargeClassID = tci2.ChargeClassID;
        //                            detail2.ParamedicID = tci2.ParamedicID;
        //                            detail2.SecondParamedicID = tci2.SecondParamedicID;
        //                            detail2.IsAdminCalculation = tci2.IsAdminCalculation;
        //                            detail2.IsVariable = tci2.IsVariable;
        //                            detail2.IsCito = tci2.IsCito;
        //                            detail2.ChargeQuantity = tci2.ChargeQuantity;
        //                            detail2.StockQuantity = tci2.StockQuantity;
        //                            detail2.SRItemUnit = tci2.SRItemUnit;
        //                            detail2.CostPrice = tci2.CostPrice;
        //                            detail2.Price = tci2.Price;
        //                            detail2.DiscountAmount = tci2.DiscountAmount;
        //                            detail2.CitoAmount = tci2.CitoAmount;
        //                            detail2.RoundingAmount = tci2.RoundingAmount;
        //                            detail2.SRDiscountReason = tci2.SRDiscountReason;
        //                            detail2.IsAssetUtilization = tci2.IsAssetUtilization;
        //                            detail2.AssetID = tci2.AssetID;
        //                            detail2.IsBillProceed = tci2.IsBillProceed;
        //                            detail2.IsOrderRealization = tci2.IsOrderRealization;
        //                            detail2.IsPaymentConfirmed = tci2.IsPaymentConfirmed;
        //                            detail2.IsPackage = tci2.IsPackage;
        //                            detail2.IsApprove = tci2.IsApprove;
        //                            detail2.IsVoid = tci2.IsVoid;
        //                            detail2.Notes = tci2.Notes;
        //                            detail2.FilmNo = tci2.FilmNo;

        //                            detail2.LastUpdateDateTime = tci2.LastUpdateDateTime;
        //                            detail2.LastUpdateByUserID = tci2.LastUpdateByUserID;
        //                            detail2.ParentNo = tci2.ParentNo;
        //                            detail2.SRCenterID = tci2.SRCenterID;
        //                            detail2.AutoProcessCalculation = tci2.AutoProcessCalculation;
        //                            detail2.ParamedicCollectionName = tci2.ParamedicCollectionName;
        //                            detail2.ToServiceUnitID = tci2.ToServiceUnitID;
        //                            detail2.IsCitoInPercent = tci2.IsCitoInPercent;
        //                            detail2.BasicCitoAmount = tci2.BasicCitoAmount;
        //                            detail2.IsItemRoom = tci2.IsItemRoom;
        //                            detail2.IsItemRoom = tci2.IsItemRoom;

        //                            detail2.SRCitoPercentage = tci2.SRCitoPercentage;
        //                            detail2.ItemConditionRuleID = tci2.ItemConditionRuleID;

        //                            if (tci2.IsExtraItem ?? false)
        //                            {
        //                                detail2.IsExtraItem = tci2.IsExtraItem;
        //                                detail2.IsSelectedExtraItem = tci2.IsSelectedExtraItem;
        //                            }

        //                            var tcis3 = TransChargesItems.Where(t => t.ParentNo == tci2.SequenceNo)
        //                                                         .OrderBy(t => t.SequenceNo);
        //                            foreach (var tci3 in tcis3)
        //                            {
        //                                var detail3 = details.AddNew();
        //                                detail3.TransactionNo = header.TransactionNo;
        //                                detail3.SequenceNo = tci3.SequenceNo;
        //                                detail3.ReferenceNo = tci3.ReferenceNo;
        //                                detail3.ReferenceSequenceNo = tci3.ReferenceSequenceNo;
        //                                detail3.ItemID = tci3.ItemID;
        //                                detail3.ChargeClassID = tci3.ChargeClassID;
        //                                detail3.ParamedicID = tci3.ParamedicID;
        //                                detail3.SecondParamedicID = tci3.SecondParamedicID;
        //                                detail3.IsAdminCalculation = tci3.IsAdminCalculation;
        //                                detail3.IsVariable = tci3.IsVariable;
        //                                detail3.IsCito = tci3.IsCito;
        //                                detail3.ChargeQuantity = tci3.ChargeQuantity;
        //                                detail3.StockQuantity = tci3.StockQuantity;
        //                                detail3.SRItemUnit = tci3.SRItemUnit;
        //                                detail3.CostPrice = tci3.CostPrice;
        //                                detail3.Price = tci3.Price;
        //                                detail3.DiscountAmount = tci3.DiscountAmount;
        //                                detail3.CitoAmount = tci3.CitoAmount;
        //                                detail3.RoundingAmount = tci3.RoundingAmount;
        //                                detail3.SRDiscountReason = tci3.SRDiscountReason;
        //                                detail3.IsAssetUtilization = tci3.IsAssetUtilization;
        //                                detail3.AssetID = tci3.AssetID;
        //                                detail3.IsBillProceed = tci3.IsBillProceed;
        //                                detail3.IsOrderRealization = tci3.IsOrderRealization;
        //                                detail3.IsPaymentConfirmed = tci3.IsPaymentConfirmed;
        //                                detail3.IsPackage = tci3.IsPackage;
        //                                detail3.IsApprove = tci3.IsApprove;
        //                                detail3.IsVoid = tci3.IsVoid;
        //                                detail3.Notes = tci3.Notes;
        //                                detail3.FilmNo = tci3.FilmNo;

        //                                detail3.LastUpdateDateTime = tci3.LastUpdateDateTime;
        //                                detail3.LastUpdateByUserID = tci3.LastUpdateByUserID;
        //                                detail3.ParentNo = tci3.ParentNo;
        //                                detail3.SRCenterID = tci3.SRCenterID;
        //                                detail3.AutoProcessCalculation = tci3.AutoProcessCalculation;
        //                                detail3.ParamedicCollectionName = tci3.ParamedicCollectionName;
        //                                detail3.ToServiceUnitID = tci3.ToServiceUnitID;
        //                                detail3.IsCitoInPercent = tci3.IsCitoInPercent;
        //                                detail3.BasicCitoAmount = tci3.BasicCitoAmount;
        //                                detail3.IsItemRoom = tci3.IsItemRoom;
        //                                detail3.IsItemRoom = tci3.IsItemRoom;

        //                                detail3.SRCitoPercentage = tci3.SRCitoPercentage;
        //                                detail3.ItemConditionRuleID = tci3.ItemConditionRuleID;

        //                                if (tci3.IsExtraItem ?? false)
        //                                {
        //                                    detail3.IsExtraItem = tci3.IsExtraItem;
        //                                    detail3.IsSelectedExtraItem = tci3.IsSelectedExtraItem;
        //                                }
        //                            }
        //                        }

        //                        var tcics = TransChargesItemComps.Where(t => t.SequenceNo == tci.SequenceNo)
        //                                                         .OrderBy(t => t.TariffComponentID);
        //                        //component
        //                        #region component
        //                        foreach (var tcic in tcics)
        //                        {
        //                            var component = TransChargesItemComps.AddNew();
        //                            component.TransactionNo = detail.TransactionNo;
        //                            component.SequenceNo = detail.SequenceNo;
        //                            component.TariffComponentID = tcic.TariffComponentID;
        //                            component.Price = tcic.Price;
        //                            component.DiscountAmount = tcic.DiscountAmount;
        //                            component.ParamedicID = tcic.ParamedicID;
        //                            component.LastUpdateDateTime = tcic.LastUpdateDateTime;
        //                            component.LastUpdateByUserID = tcic.LastUpdateByUserID;
        //                            component.IsPackage = tcic.IsPackage;
        //                            component.AutoProcessCalculation = tcic.AutoProcessCalculation;
        //                            component.CitoAmount = tcic.CitoAmount;

        //                            tcic.MarkAsDeleted();
        //                        }
        //                        #endregion

        //                        var cons = TransChargesItemConsumptions.Where(t => t.SequenceNo == tci.SequenceNo)
        //                                                               .OrderBy(t => t.DetailItemID);
        //                        //consumption
        //                        #region consumption
        //                        foreach (var con in cons)
        //                        {
        //                            var consumption = consumptions.AddNew();
        //                            consumption.TransactionNo = detail.TransactionNo;
        //                            consumption.SequenceNo = detail.SequenceNo;
        //                            consumption.DetailItemID = con.DetailItemID;
        //                            consumption.Qty = con.Qty;
        //                            consumption.QtyRealization = con.QtyRealization;
        //                            consumption.SRItemUnit = con.SRItemUnit;
        //                            consumption.Price = con.Price;
        //                            consumption.AveragePrice = con.AveragePrice;
        //                            consumption.FifoPrice = con.FifoPrice;
        //                            consumption.LastUpdateDateTime = con.LastUpdateDateTime;
        //                            consumption.LastUpdateByUserID = con.LastUpdateByUserID;
        //                            consumption.IsPackage = con.IsPackage;

        //                            con.MarkAsDeleted();
        //                        }
        //                        #endregion

        //                        tci.MarkAsDeleted();
        //                    }
        //                }

        //                headers.Save();
        //                details.Save();
        //                components.Save();
        //                consumptions.Save();

        //                TransChargesItems.Save();
        //                TransChargesItemComps.Save();
        //                TransChargesItemConsumptions.Save();
        //            }

        //            //header
        //            entity.IsApproved = isApproval;

        //            if (QueryString_type != "jo") entity.IsBillProceed = isApproval;

        //            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

        //            var grrID = reg.GuarantorID;

        //            var pat = new Patient();
        //            pat.LoadByPrimaryKey(reg.PatientID);

        //            if (grrID == AppSession.Parameter.SelfGuarantor)
        //            {
        //                if (!string.IsNullOrEmpty(pat.MemberID)) grrID = pat.MemberID;
        //            }

        //            var grr = new Guarantor();
        //            grr.LoadByPrimaryKey(reg.GuarantorID);

        //            //var unit = new ServiceUnit();
        //            //unit.LoadByPrimaryKey(entity.ToServiceUnitID);

        //            var tblCovered = new DataTable();
        //            if ((QueryString_type != "jo") && isApproval)
        //            {
        //                tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, grrID, ClassID, reg.CoverageClassID, (TransChargesItems.Where(t => !(t.IsVoid ?? false)).Select(t => t.ItemID)).ToArray(),
        //                    entity.TransactionDate.Value, false);
        //            }

        //            //cost calculation
        //            if (QueryString_type != "jo")
        //            {
        //                if (isApproval)
        //                {
        //                    foreach (TransChargesItem detail in TransChargesItems.Where(t => string.IsNullOrEmpty(t.ParentNo) && !(t.IsVoid ?? false)))
        //                    {
        //                        //--- untuk item detail paket, covered diambil dari header item paket
        //                        string itemId = detail.ItemID;
        //                        if (!string.IsNullOrEmpty(entity.PackageReferenceNo))
        //                        {
        //                            var tciPackageRef = new TransChargesItem();
        //                            if (tciPackageRef.LoadByPrimaryKey(entity.PackageReferenceNo, detail.SequenceNo.Substring(0, 3))) itemId = tciPackageRef.ItemID;
        //                        }

        //                        //var rowCovered = tblCovered.AsEnumerable().SingleOrDefault(t => t.Field<string>("ItemID") == detail.ItemID &&
        //                        //                                                                t.Field<bool>("IsInclude"));

        //                        var rowCovered = tblCovered.AsEnumerable().SingleOrDefault(t => t.Field<string>("ItemID") == itemId && t.Field<bool>("IsInclude"));
        //                        bool isTransChargesItemComps = false;
        //                        //TransChargesItemComps
        //                        if (rowCovered != null)
        //                        {
        //                            decimal? discount = 0;
        //                            bool isDiscount = false, isMargin = false;

        //                            foreach (var comp in TransChargesItemComps.Where(t => t.TransactionNo == detail.TransactionNo && t.SequenceNo == detail.SequenceNo)
        //                                                                      .OrderBy(t => t.TariffComponentID))
        //                            {
        //                                decimal? amountValue = 0;
        //                                decimal? basicPrice = 0;
        //                                decimal? coveragePrice = 0;

        //                                if (Convert.ToBoolean(rowCovered["IsByTariffComponent"]))
        //                                {
        //                                    var array = rowCovered["TariffComponentValue"].ToString().Split(';').Where(l => l.Split('/')[2] == comp.TariffComponentID).SingleOrDefault();
        //                                    if (array == null)
        //                                    {
        //                                        amountValue = (decimal?)rowCovered["AmountValue"];
        //                                        basicPrice = (decimal?)rowCovered["BasicPrice"];
        //                                        coveragePrice = (decimal?)rowCovered["CoveragePrice"];
        //                                    }
        //                                    else
        //                                    {
        //                                        var list = array.Split('/');
        //                                        if (list == null || list.Count() == 0)
        //                                        {
        //                                            amountValue = (decimal?)rowCovered["AmountValue"];
        //                                            basicPrice = (decimal?)rowCovered["BasicPrice"];
        //                                            coveragePrice = (decimal?)rowCovered["CoveragePrice"];
        //                                        }
        //                                        else
        //                                        {
        //                                            amountValue = Convert.ToDecimal(list[3]);
        //                                            basicPrice = Convert.ToDecimal(list[0]);
        //                                            coveragePrice = Convert.ToDecimal(list[1]);
        //                                        }
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    amountValue = (decimal?)rowCovered["AmountValue"];
        //                                    basicPrice = (decimal?)rowCovered["BasicPrice"];
        //                                    coveragePrice = (decimal?)rowCovered["CoveragePrice"];
        //                                }

        //                                isTransChargesItemComps = true;

        //                                basicPrice = Helper.Tariff.GetItemConditionRuleTariff(basicPrice ?? 0, detail.ItemConditionRuleID, entity.TransactionDate.Value);
        //                                coveragePrice = Helper.Tariff.GetItemConditionRuleTariff(coveragePrice ?? 0, detail.ItemConditionRuleID, entity.TransactionDate.Value);

        //                                if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
        //                                {
        //                                    if ((comp.Price - comp.DiscountAmount) <= 0) continue;

        //                                    var compPrice = comp.Price;
        //                                    if (basicPrice > coveragePrice)
        //                                    {
        //                                        var tcomp = Helper.Tariff.GetItemTariffComponent(entity.TransactionDate.Value, grr.SRTariffType, reg.CoverageClassID, comp.TariffComponentID, detail.ItemID);
        //                                        if (!tcomp.AsEnumerable().Any())
        //                                            tcomp = Helper.Tariff.GetItemTariffComponent(entity.TransactionDate.Value, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, comp.TariffComponentID,
        //                                                detail.ItemID);
        //                                        if (!tcomp.AsEnumerable().Any())
        //                                            tcomp = Helper.Tariff.GetItemTariffComponent(entity.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, reg.CoverageClassID, comp.TariffComponentID,
        //                                                detail.ItemID);
        //                                        if (!tcomp.AsEnumerable().Any())
        //                                            tcomp = Helper.Tariff.GetItemTariffComponent(entity.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass,
        //                                                comp.TariffComponentID, detail.ItemID);

        //                                        if (!tcomp.AsEnumerable().Any()) continue;

        //                                        compPrice = tcomp.AsEnumerable().Select(c => c.Field<decimal>("Price")).Single();
        //                                        if (!string.IsNullOrEmpty(detail.ItemConditionRuleID)) compPrice = Helper.Tariff.GetItemConditionRuleTariff(compPrice ?? 0, detail.ItemConditionRuleID,
        //                                            entity.TransactionDate.Value);
        //                                    }

        //                                    if ((bool)rowCovered["IsValueInPercent"])
        //                                    {
        //                                        var discountRule = (amountValue / 100) * compPrice;
        //                                        var fee = comp.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare == "Yes", entity.RegistrationNo, detail.ItemID, discountRule,
        //                                            AppSession.UserLogin.UserID);
        //                                        comp.AutoProcessCalculation = 0 - comp.DiscountAmount;

        //                                        //comp.DiscountAmount = (amountValue / 100) * compPrice;
        //                                        //comp.AutoProcessCalculation = 0 - (amountValue / 100) * compPrice;
        //                                    }
        //                                    else
        //                                    {
        //                                        //if (!isDiscount)
        //                                        //{
        //                                        //    if (discount == 0)
        //                                        //    {
        //                                        if (detail.Price > compPrice) amountValue = (compPrice / detail.Price) * amountValue;

        //                                        if (compPrice >= amountValue)
        //                                        {
        //                                            var discountRule = amountValue;
        //                                            var fee = comp.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare == "Yes", entity.RegistrationNo, detail.ItemID,
        //                                                discountRule, AppSession.UserLogin.UserID);
        //                                            comp.AutoProcessCalculation = 0 - comp.DiscountAmount;

        //                                            //comp.DiscountAmount = amountValue;
        //                                            //comp.AutoProcessCalculation = 0 - amountValue;
        //                                            //isDiscount = true;
        //                                        }
        //                                        else
        //                                        {
        //                                            var discountRule = compPrice;
        //                                            var fee = comp.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare == "Yes", entity.RegistrationNo, detail.ItemID,
        //                                                discountRule, AppSession.UserLogin.UserID);
        //                                            comp.AutoProcessCalculation = 0 - comp.DiscountAmount;

        //                                            //comp.DiscountAmount = compPrice;
        //                                            //comp.AutoProcessCalculation = 0 - compPrice;
        //                                            //discount = amountValue - compPrice;
        //                                        }
        //                                        //    }
        //                                        //    else
        //                                        //    {
        //                                        //        if (compPrice >= discount)
        //                                        //        {
        //                                        //            comp.DiscountAmount = discount;
        //                                        //            comp.AutoProcessCalculation = 0 - discount;
        //                                        //            isDiscount = true;
        //                                        //        }
        //                                        //        else
        //                                        //        {
        //                                        //            comp.DiscountAmount = compPrice;
        //                                        //            comp.AutoProcessCalculation = 0 - compPrice;
        //                                        //            discount -= compPrice;
        //                                        //        }
        //                                        //    }
        //                                        //}
        //                                    }
        //                                }
        //                                else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
        //                                {
        //                                    if ((bool)rowCovered["IsValueInPercent"])
        //                                    {
        //                                        comp.AutoProcessCalculation = (amountValue / 100) * comp.Price;
        //                                        comp.Price += (amountValue / 100) * comp.Price;

        //                                        var discountRule = 0;
        //                                        var fee = comp.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare == "Yes", entity.RegistrationNo, detail.ItemID,
        //                                            discountRule, AppSession.UserLogin.UserID);
        //                                        comp.AutoProcessCalculation = comp.AutoProcessCalculation - comp.DiscountAmount;
        //                                    }
        //                                    else
        //                                    {
        //                                        if (!isMargin)
        //                                        {
        //                                            comp.Price += amountValue;
        //                                            comp.AutoProcessCalculation = amountValue;
        //                                            isMargin = true;

        //                                            var discountRule = 0;
        //                                            var fee = comp.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare == "Yes", entity.RegistrationNo, detail.ItemID,
        //                                                discountRule, AppSession.UserLogin.UserID);
        //                                            comp.AutoProcessCalculation = amountValue - comp.DiscountAmount;
        //                                        }
        //                                    }
        //                                }
        //                                comp.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //                                comp.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //                            }
        //                        }

        //                        //TransChargesItems
        //                        detail.IsApprove = isApproval;
        //                        detail.IsBillProceed = isApproval;
        //                        //jangan di remark, kalo mo remark, tanya deby dulu
        //                        if (QueryString_type == "ds")
        //                        {
        //                            detail.IsOrderRealization = true;
        //                            detail.IsPaymentConfirmed = false;
        //                            detail.RealizationDateTime = (new DateTime()).NowAtSqlServer();
        //                            detail.RealizationUserID = AppSession.UserLogin.UserID;
        //                            detail.IsSendToLIS = entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID;
        //                        }
        //                        //end jangan di remark
        //                        //if (TransChargesItemComps.Count > 0)
        //                        if (isTransChargesItemComps)
        //                        {
        //                            detail.AutoProcessCalculation = TransChargesItemComps.Where(t => t.TransactionNo == detail.TransactionNo && t.SequenceNo == detail.SequenceNo)
        //                                                                                 .Sum(t => t.AutoProcessCalculation);
        //                            if (detail.AutoProcessCalculation < 0)
        //                            {
        //                                //detail.DiscountAmount += detail.ChargeQuantity * Math.Abs(detail.AutoProcessCalculation ?? 0);
        //                                detail.DiscountAmount = detail.ChargeQuantity * Math.Abs(detail.AutoProcessCalculation ?? 0);

        //                                if (detail.DiscountAmount > (detail.Price * Math.Abs(detail.ChargeQuantity ?? 0)))
        //                                {
        //                                    detail.DiscountAmount = detail.Price * Math.Abs(detail.ChargeQuantity ?? 0);
        //                                    detail.AutoProcessCalculation = 0 - detail.Price;
        //                                }
        //                            }
        //                            else if (detail.AutoProcessCalculation > 0) detail.Price += detail.AutoProcessCalculation;
        //                        }
        //                        else
        //                        {
        //                            if (rowCovered != null)
        //                            {
        //                                if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
        //                                {
        //                                    var basicPrice = (decimal?)rowCovered["BasicPrice"];
        //                                    var coveragePrice = (decimal?)rowCovered["CoveragePrice"];
        //                                    if (!string.IsNullOrEmpty(detail.ItemConditionRuleID))
        //                                    {
        //                                        basicPrice = Helper.Tariff.GetItemConditionRuleTariff(basicPrice ?? 0, detail.ItemConditionRuleID, entity.TransactionDate.Value);
        //                                        coveragePrice = Helper.Tariff.GetItemConditionRuleTariff(coveragePrice ?? 0, detail.ItemConditionRuleID, entity.TransactionDate.Value);
        //                                    }

        //                                    var detailPrice = detail.Price ?? 0;
        //                                    if (basicPrice > coveragePrice)
        //                                    {
        //                                        ItemTariff tariff = (Helper.Tariff.GetItemTariff(entity.TransactionDate.Value, grr.SRTariffType, reg.CoverageClassID, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
        //                                                 Helper.Tariff.GetItemTariff(entity.TransactionDate.Value, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
        //                                                (Helper.Tariff.GetItemTariff(entity.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, reg.CoverageClassID, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
        //                                                 Helper.Tariff.GetItemTariff(entity.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));
        //                                        if (tariff != null)
        //                                        {
        //                                            //detailPrice = tariff.Price ?? 0;
        //                                            detailPrice = Helper.Tariff.GetItemConditionRuleTariff(tariff.Price ?? 0, detail.ItemConditionRuleID, entity.TransactionDate.Value);
        //                                        }
        //                                    }

        //                                    if ((bool)rowCovered["IsValueInPercent"])
        //                                    {
        //                                        detail.DiscountAmount = (detail.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * detailPrice);
        //                                        detail.AutoProcessCalculation = 0 - (((decimal)rowCovered["AmountValue"] / 100) * detailPrice);
        //                                    }
        //                                    else
        //                                    {
        //                                        detail.DiscountAmount = (detail.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];
        //                                        detail.AutoProcessCalculation = 0 - (decimal)rowCovered["AmountValue"];
        //                                    }

        //                                    if (detail.DiscountAmount > (detailPrice * detail.ChargeQuantity))
        //                                        detail.DiscountAmount = detailPrice * detail.ChargeQuantity;
        //                                }
        //                                else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
        //                                {
        //                                    if ((bool)rowCovered["IsValueInPercent"])
        //                                    {
        //                                        detail.AutoProcessCalculation = ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;
        //                                        detail.Price += ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;
        //                                    }
        //                                    else
        //                                    {
        //                                        detail.Price += (decimal)rowCovered["AmountValue"];
        //                                        detail.AutoProcessCalculation = (decimal)rowCovered["AmountValue"];
        //                                    }
        //                                }
        //                            }
        //                        }

        //                        if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH" && string.IsNullOrEmpty(detail.FilmNo))
        //                        {
        //                            var item = new Item();
        //                            item.LoadByPrimaryKey(detail.ItemID);
        //                            if (item.Notes.Length > 0 && item.SRItemType != ItemType.Medical && item.SRItemType != ItemType.NonMedical && item.SRItemType != ItemType.Kitchen)
        //                            {
        //                                amplopFilmAutoNumber = Helper.GetNewAutoNumber(TransactionDate.Date, AppEnum.AutoNumber.AmplopFilmNo,
        //                                    item.Notes.Length >= 3 ? item.Notes.Substring(0, 3).ToUpper() : item.Notes.ToUpper(), AppSession.UserLogin.UserID);

        //                                var filmNo = amplopFilmAutoNumber.LastCompleteNumber;
        //                                amplopFilmAutoNumber.Save();

        //                                detail.FilmNo = filmNo;
        //                            }
        //                        }

        //                        detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //                        detail.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

        //                        //post
        //                        decimal? total = ((detail.ChargeQuantity * detail.Price) - detail.DiscountAmount) + detail.CitoAmount;
        //                        decimal? qty = detail.ChargeQuantity;

        //                        var calc = new Helper.CostCalculation(grrID, detail.ItemID, total ?? 0, tblCovered, qty ?? 0,
        //                                                              detail.IsCito ?? false,
        //                                                              detail.IsCitoInPercent ?? false,
        //                                                              detail.BasicCitoAmount ?? 0, detail.Price ?? 0,
        //                                                              entity.IsRoomIn ?? false, detail.IsItemRoom ?? false,
        //                                                              entity.TariffDiscountForRoomIn ?? 0, detail.DiscountAmount ?? 0, false,
        //                                                              detail.ItemConditionRuleID, entity.TransactionDate.Value);

        //                        var package = new GuarantorSurgicalPackageCoveredItem();
        //                        if (package.LoadByPrimaryKey(grrID, entity.SurgicalPackageID, detail.ItemID))
        //                        {
        //                            if (calc.PatientAmount + calc.GuarantorAmount <= package.CoveredAmount)
        //                            {
        //                                calc.GuarantorAmount = calc.PatientAmount + calc.GuarantorAmount;
        //                                calc.PatientAmount = 0;
        //                            }
        //                            else
        //                            {
        //                                calc.PatientAmount = calc.PatientAmount + calc.GuarantorAmount - package.CoveredAmount ?? 0;
        //                                calc.GuarantorAmount = package.CoveredAmount ?? 0;
        //                            }
        //                        }

        //                        //CostCalculation

        //                        // 20160912 terjadi duplikasi CostCalculation padahal transaksinya (cth RSMP: SU161205-0192) belum approve
        //                        var costs = CostCalculations.Where(cc => cc.TransactionNo == detail.TransactionNo && cc.SequenceNo == detail.SequenceNo);
        //                        CostCalculation cost;
        //                        if (costs.Count() == 1)
        //                        {
        //                            cost = costs.First();
        //                            new BasePage().LogError(new Exception(string.Format("Dev Warning: duplication of cost calculation on TransactionNo {0}, SequenceNo {1}, ItemID {2}. Potential of duplicate stock taking!!",
        //                                cost.TransactionNo, cost.SequenceNo, cost.ItemID)));
        //                        }
        //                        else cost = CostCalculations.AddNew();

        //                        cost.RegistrationNo = entity.RegistrationNo;
        //                        cost.TransactionNo = detail.TransactionNo;
        //                        cost.SequenceNo = detail.SequenceNo;
        //                        cost.ItemID = detail.ItemID;
        //                        cost.PatientAmount = calc.PatientAmount;
        //                        cost.GuarantorAmount = calc.GuarantorAmount;
        //                        cost.DiscountAmount = detail.DiscountAmount;
        //                        cost.IsPackage = detail.IsPackage;
        //                        cost.ParentNo = detail.ParentNo;
        //                        cost.ParamedicAmount = detail.ChargeQuantity * TransChargesItemComps.Where(comp => comp.TransactionNo == detail.TransactionNo && comp.SequenceNo == detail.SequenceNo &&
        //                                                                                                           !string.IsNullOrEmpty(comp.ParamedicID))
        //                                                                                            .Sum(comp => comp.Price - comp.DiscountAmount + comp.CitoAmount);
        //                        cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //                        cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //                    }

        //                    if (!string.IsNullOrEmpty(entity.PackageReferenceNo))
        //                    {
        //                        foreach (TransChargesItem detail in TransChargesItems.Where(t => string.IsNullOrEmpty(t.ParentNo) && (t.IsVoid ?? false)))
        //                        {
        //                            foreach (var cons in TransChargesItemConsumptions.Where(cons => cons.SequenceNo == detail.SequenceNo))
        //                            {
        //                                cons.MarkAsDeleted();
        //                            }

        //                            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
        //                            {
        //                                var parent = new TransChargesItem();
        //                                if (parent.LoadByPrimaryKey(entity.PackageReferenceNo, detail.SequenceNo.Substring(0, 3)))
        //                                {
        //                                    var package = new ItemPackage();
        //                                    package.Query.Where(package.Query.ItemID == parent.ItemID && package.Query.DetailItemID == detail.ItemID);
        //                                    if (package.Query.Load())
        //                                    {
        //                                        var comp = new ItemPackageTariffComponent();
        //                                        comp.Query.Select(comp.Query.Price.Sum());
        //                                        comp.Query.Where(comp.Query.ItemID == parent.ItemID && comp.Query.DetailItemID == detail.ItemID);
        //                                        if (comp.Query.Load())
        //                                        {
        //                                            parent.DiscountAmount += (comp.Price - ((package.IsDiscountInPercent ?? false) ? (((package.DiscountValue ?? 0) / 100) * comp.Price) : (package.DiscountValue ?? 0)));
        //                                            parent.AutoProcessCalculation += 0 - (comp.Price - ((package.IsDiscountInPercent ?? false) ? (((package.DiscountValue ?? 0) / 100) * comp.Price) : (package.DiscountValue ?? 0)));
        //                                            parent.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //                                            parent.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //                                            parent.Save();
        //                                        }
        //                                    }
        //                                }

        //                                var cmp = new TransChargesItemComp();
        //                                if (cmp.LoadByPrimaryKey(entity.PackageReferenceNo, detail.SequenceNo.Substring(0, 3), AppSession.Parameter.TariffComponentJasaSaranaID))
        //                                {
        //                                    cmp.DiscountAmount += parent.DiscountAmount;
        //                                    cmp.AutoProcessCalculation += 0 - parent.DiscountAmount;
        //                                    cmp.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //                                    cmp.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //                                    cmp.Save();
        //                                }

        //                                var cc = new CostCalculation();
        //                                if (cc.LoadByPrimaryKey(entity.RegistrationNo, entity.PackageReferenceNo, detail.SequenceNo.Substring(0, 3)))
        //                                {
        //                                    cc.PatientAmount = cc.PatientAmount == 0 ? 0 : (parent.ChargeQuantity * parent.Price) - parent.DiscountAmount;
        //                                    cc.GuarantorAmount = cc.GuarantorAmount == 0 ? 0 : (parent.ChargeQuantity * parent.Price) - parent.DiscountAmount;
        //                                    cc.DiscountAmount += parent.DiscountAmount;
        //                                    cc.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //                                    cc.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //                                    cc.Save();
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                    CostCalculations.MarkAllAsDeleted();
        //            }
        //            else
        //            {
        //                if (!isApproval) CostCalculations.MarkAllAsDeleted();
        //            }

        //            if (QueryString_type == "jo" || QueryString_type == "ds")
        //            {
        //                if (string.IsNullOrEmpty(pat.DiagnosticNo) && isApproval && AppSession.Parameter.RadiologyNoAutoCreate == "Yes")
        //                {
        //                    pat.DiagnosticNo = (new DateTime()).NowAtSqlServer().ToString(AppSession.Parameter.RadiologyNoFormat);
        //                    pat.Save();
        //                }
        //            }

        //            entity.Save();

        //            if (isApproval)
        //            {
        //                if (QueryString_type != "jo")
        //                {
        //                    // stock calculation
        //                    // charges
        //                    var chargesBalances = new ItemBalanceCollection();
        //                    var chargesDetailBalances = new ItemBalanceDetailCollection();
        //                    var chargesMovements = new ItemMovementCollection();

        //                    string itemNoStock;
        //                    var transChargesItems = TransChargesItems;

        //                    ItemBalance.PrepareItemBalances(transChargesItems, entity.ToServiceUnitID, entity.LocationID, AppSession.UserLogin.UserID, isApproval, ref chargesBalances, ref chargesDetailBalances,
        //                        ref chargesMovements, out itemNoStock);

        //                    if (!string.IsNullOrEmpty(itemNoStock))
        //                    {
        //                        if (itemNoStock.Length >= 5 && itemNoStock.Substring(0, 5) == "Zero|") return "Zero cost price of item : " + itemNoStock.Replace("Zero|", "");
        //                        return "Insufficient balance of item : " + itemNoStock;
        //                    }

        //                    transChargesItems.Save();
        //                    TransChargesItemComps.Save();
        //                    CostCalculations.Save();

        //                    if (AppSession.Parameter.IsFeeCalculatedOnTransaction.ToLower().Equals("yes"))
        //                    {
        //                        // extract fee
        //                        var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
        //                        feeColl.SetFeeByTCIC(TransChargesItemComps, AppSession.UserLogin.UserID);
        //                        feeColl.Save();
        //                        feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
        //                        feeColl.Save();
        //                    }

        //                    if (chargesBalances != null) chargesBalances.Save();
        //                    if (chargesDetailBalances != null) chargesDetailBalances.Save();
        //                    if (chargesMovements != null) chargesMovements.Save();

        //                    // consumption
        //                    var consumptionBalances = new ItemBalanceCollection();
        //                    var consumptionDetailBalances = new ItemBalanceDetailCollection();
        //                    var consumptionMovements = new ItemMovementCollection();

        //                    var transChargesItemConsumptions = TransChargesItemConsumptions;

        //                    ItemBalance.PrepareItemBalances(transChargesItemConsumptions, entity.ToServiceUnitID, entity.LocationID, AppSession.UserLogin.UserID, ref consumptionBalances, ref consumptionDetailBalances,
        //                        ref consumptionMovements, out itemNoStock);

        //                    if (!string.IsNullOrEmpty(itemNoStock))
        //                    {
        //                        if (itemNoStock.Length >= 5 && itemNoStock.Substring(0, 5) == "Zero|") return "Zero cost price of item : " + itemNoStock.Replace("Zero|", "");
        //                        return "Insufficient balance of item : " + itemNoStock;
        //                    }

        //                    if (chargesBalances != null || consumptionBalances != null)
        //                    {
        //                        var loc = new Location();
        //                        if (loc.LoadByPrimaryKey(entity.LocationID) && loc.IsHoldForTransaction == true)
        //                        {
        //                            return "Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.";
        //                        }
        //                    }

        //                    transChargesItemConsumptions.Save();

        //                    if (consumptionBalances != null) consumptionBalances.Save();
        //                    if (consumptionDetailBalances != null) consumptionDetailBalances.Save();
        //                    if (consumptionMovements != null) consumptionMovements.Save();

        //                    unit = new ServiceUnit();
        //                    unit.LoadByPrimaryKey(entity.ToServiceUnitID);
        //                    if (QueryString_type == "jo")
        //                    {
        //                        /* Automatic Journal Testing Start */
        //                        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
        //                        {
        //                            var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');

        //                            var mb = new MergeBilling();
        //                            mb.LoadByPrimaryKey(reg.RegistrationNo);
        //                            if (string.IsNullOrEmpty(mb.FromRegistrationNo))
        //                            {
        //                                if (type.Contains(reg.SRRegistrationType))
        //                                {
        //                                    var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.TransactionDate.Value.Date);
        //                                    if (isClosingPeriod)
        //                                    {
        //                                        return "Financial statements for period: " +
        //                                               string.Format("{0:MMMM-yyyy}", entity.TransactionDate.Value.Date) +
        //                                               " have been closed. Please contact the authorities.";
        //                                    }

        //                                    int? journalId = JournalTransactions.AddNewIncomeJournalTemporary(entity, TransChargesItemComps, reg, unit, CostCalculations, "JO", AppSession.UserLogin.UserID, 0);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                var freg = new Registration();
        //                                freg.LoadByPrimaryKey(mb.FromRegistrationNo);
        //                                if (type.Contains(reg.SRRegistrationType))
        //                                {
        //                                    var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.TransactionDate.Value.Date);
        //                                    if (isClosingPeriod)
        //                                    {
        //                                        return "Financial statements for period: " +
        //                                               string.Format("{0:MMMM-yyyy}", entity.TransactionDate.Value.Date) +
        //                                               " have been closed. Please contact the authorities.";
        //                                    }

        //                                    int? journalId = JournalTransactions.AddNewIncomeJournalTemporary(entity, TransChargesItemComps, reg, unit, CostCalculations, "JO", AppSession.UserLogin.UserID, 0);
        //                                }
        //                            }
        //                        }

        //                        /* Automatic Journal Testing End */
        //                    }
        //                    else if (QueryString_type != "mcu")
        //                    {
        //                        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
        //                        {
        //                            var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');

        //                            var mb = new MergeBilling();
        //                            mb.LoadByPrimaryKey(reg.RegistrationNo);
        //                            if (string.IsNullOrEmpty(mb.FromRegistrationNo))
        //                            {
        //                                if (type.Contains(reg.SRRegistrationType))
        //                                {
        //                                    var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.TransactionDate.Value.Date);
        //                                    if (isClosingPeriod)
        //                                    {
        //                                        return "Financial statements for period: " +
        //                                               string.Format("{0:MMMM-yyyy}", entity.TransactionDate.Value.Date) +
        //                                               " have been closed. Please contact the authorities.";
        //                                    }

        //                                    int? journalId = JournalTransactions.AddNewIncomeJournalTemporary(entity, TransChargesItemComps, reg, unit, CostCalculations, "SU", AppSession.UserLogin.UserID, 0);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                var freg = new Registration();
        //                                freg.LoadByPrimaryKey(mb.FromRegistrationNo);
        //                                if (type.Contains(freg.SRRegistrationType))
        //                                {
        //                                    var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.TransactionDate.Value.Date);
        //                                    if (isClosingPeriod)
        //                                    {
        //                                        return "Financial statements for period: " +
        //                                               string.Format("{0:MMMM-yyyy}", entity.TransactionDate.Value.Date) +
        //                                               " have been closed. Please contact the authorities.";
        //                                    }

        //                                    int? journalId = JournalTransactions.AddNewIncomeJournalTemporary(entity, TransChargesItemComps, reg, unit, CostCalculations, "SU", AppSession.UserLogin.UserID, 0);
        //                                }
        //                            }
        //                        }
        //                        if (QueryString_type == "ds")
        //                        {
        //                            //var charges = new TransCharges();
        //                            //charges.LoadByPrimaryKey(txtTransactionNo.Text);

        //                            #region Interop
        //                            if (AppSession.Parameter.IsUsingHisInterop == "Yes")
        //                            {
        //                                var patient = new Patient();
        //                                patient.LoadByPrimaryKey(reg.PatientID);

        //                                switch (AppSession.Parameter.HisInteropConfigName)
        //                                {
        //                                    #region PAC_HIS_INTEROP_CONNECTION_NAME
        //                                    case "PAC_HIS_INTEROP_CONNECTION_NAME":
        //                                        if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
        //                                        {
        //                                            var lto = new BusinessObject.Interop.PAC.LabTestOrder
        //                                            {
        //                                                TransactionNo = entity.TransactionNo,
        //                                                TransactionDate = entity.TransactionDate,
        //                                                RegistrationNo = entity.RegistrationNo
        //                                            };

        //                                            lto.MedicalNo = patient.MedicalNo;
        //                                            lto.FirstName = patient.FirstName;
        //                                            lto.MiddleName = patient.MiddleName;
        //                                            lto.LastName = patient.LastName;
        //                                            lto.Sex = patient.Sex;
        //                                            lto.FromServiceUnitID = reg.ServiceUnitID;
        //                                            lto.FromServiceUnitName = unit.ServiceUnitName;
        //                                            lto.ClassID = entity.ClassID;

        //                                            var cls = new Class();
        //                                            cls.LoadByPrimaryKey(entity.ClassID);
        //                                            lto.ClassName = cls.ClassName;

        //                                            lto.CityOfBirth = patient.CityOfBirth;
        //                                            lto.DateOfBirth = patient.DateOfBirth;
        //                                            lto.ParamedicID = reg.ParamedicID;

        //                                            var param = new Paramedic();
        //                                            param.LoadByPrimaryKey(reg.ParamedicID);
        //                                            lto.ParamedicName = param.ParamedicName;

        //                                            lto.StreetName = patient.StreetName;
        //                                            lto.District = patient.District;
        //                                            lto.City = patient.City;
        //                                            lto.County = patient.County;
        //                                            lto.State = patient.State;
        //                                            lto.ZipCode = patient.ZipCode;
        //                                            lto.PhoneNo = patient.PhoneNo;
        //                                            lto.FaxNo = patient.FaxNo;
        //                                            lto.Email = patient.Email;
        //                                            lto.MobilePhoneNo = patient.MobilePhoneNo;
        //                                            lto.Company = patient.Company;

        //                                            lto.GuarantorName = grr.GuarantorName;

        //                                            foreach (var entity2 in transChargesItems)
        //                                            {
        //                                                var item = new Item();
        //                                                item.LoadByPrimaryKey(entity2.ItemID);

        //                                                lto.TestOrderID += item.ItemIDExternal + "^";
        //                                                lto.TestOrderName += item.ItemName + "^";
        //                                            }

        //                                            lto.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //                                            lto.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //                                            lto.IsConfirm = false;

        //                                            lto.es.Connection.Name = AppConstant.HIS_INTEROP.PAC_HIS_INTEROP_CONNECTION_NAME;
        //                                            lto.Save();
        //                                        }
        //                                        break;
        //                                    #endregion
        //                                    case "RSSA_HIS_INTEROP_CONNECTION_NAME":
        //                                        break;
        //                                    #region RSCH_LIS_INTEROP_CONNECTION_NAME
        //                                    case AppConstant.HIS_INTEROP.RSCH_LIS_INTEROP_CONNECTION_NAME:
        //                                        if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
        //                                        {
        //                                            var olh = new BusinessObject.Interop.RSCH.OrderLabHeader();
        //                                            olh.es.Connection.Name = AppConstant.HIS_INTEROP.RSCH_LIS_INTEROP_CONNECTION_NAME;
        //                                            olh.OrderLabNo = entity.TransactionNo;
        //                                            olh.OrderLabTglOrder = (new DateTime()).NowAtSqlServer().Date;
        //                                            olh.OrderLabNoMR = patient.MedicalNo;
        //                                            olh.OrderLabNama = ((patient.FirstName + " " + patient.MiddleName).Trim() + " " + patient.LastName).Trim();

        //                                            if (!string.IsNullOrEmpty(reg.PhysicianSenders)) olh.OrderLabNamaPengirim = reg.PhysicianSenders;
        //                                            else
        //                                            {
        //                                                var medic = new Paramedic();
        //                                                medic.LoadByPrimaryKey(reg.ParamedicID);
        //                                                olh.OrderLabNamaPengirim = medic.ParamedicName;
        //                                            }

        //                                            olh.OrderLabKdPoli = entity.FromServiceUnitID;
        //                                            olh.OrderLabBirthdate = patient.DateOfBirth;
        //                                            olh.OrderLabAgeYear = reg.AgeInYear;
        //                                            olh.OrderLabAgeMonth = reg.AgeInMonth;
        //                                            olh.OrderLabAgeDay = reg.AgeInDay;
        //                                            olh.OrderLabSex = patient.Sex;
        //                                            olh.OrderLabKdPengirim = string.Empty;

        //                                            unit = new ServiceUnit();
        //                                            unit.LoadByPrimaryKey(entity.FromServiceUnitID);
        //                                            olh.OrderlabNamaPoli = unit.ServiceUnitName;

        //                                            olh.OrderLabJamOrder = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
        //                                            olh.OrderLabStatus = string.Empty;
        //                                            olh.OrderLabNoBed = reg.BedID;

        //                                            var guar = new Guarantor();
        //                                            if (guar.LoadByPrimaryKey(reg.GuarantorID))
        //                                                olh.GuarantorName = guar.GuarantorName;

        //                                            if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
        //                                            {
        //                                                var soap = new RegistrationInfoMedic();
        //                                                soap.Query.es.Top = 1;
        //                                                soap.Query.Where(soap.Query.RegistrationNo == reg.RegistrationNo, soap.Query.SRMedicalNotesInputType == "SOAP");
        //                                                soap.Query.OrderBy(soap.Query.RegistrationInfoMedicID.Descending);
        //                                                if (soap.Query.Load()) olh.DiagnoseText = soap.Info3;
        //                                                else olh.DiagnoseText = string.Empty;
        //                                            }
        //                                            else if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
        //                                            {
        //                                                var nhd = new NursingTransHD();
        //                                                nhd.Query.es.Top = 1;
        //                                                nhd.Query.Where(nhd.Query.RegistrationNo == reg.RegistrationNo);
        //                                                if (nhd.Query.Load())
        //                                                {
        //                                                    var ndt = new NursingDiagnosaTransDT();
        //                                                    ndt.Query.es.Top = 1;
        //                                                    ndt.Query.Where(ndt.Query.TransactionNo == nhd.TransactionNo);
        //                                                    ndt.Query.OrderBy(ndt.Query.Priority.Ascending);
        //                                                    if (ndt.Query.Load()) olh.DiagnoseText = ndt.NursingDiagnosaName;
        //                                                    else olh.DiagnoseText = string.Empty;
        //                                                }
        //                                                else olh.DiagnoseText = string.Empty;
        //                                            }

        //                                            olh.Save();

        //                                            var details = new BusinessObject.Interop.RSCH.OrderLabDetailCollection();
        //                                            details.es.Connection.Name = AppConstant.HIS_INTEROP.RSCH_LIS_INTEROP_CONNECTION_NAME;

        //                                            foreach (var entity2 in transChargesItems)
        //                                            {
        //                                                var old = details.AddNew();

        //                                                var item = new Item();
        //                                                item.LoadByPrimaryKey(entity2.ItemID);

        //                                                old.OrderLabNo = entity.TransactionNo;
        //                                                old.OrderLabTglOrder = olh.OrderLabTglOrder;
        //                                                old.CheckupResultTestCode = item.ItemIDExternal;
        //                                                old.OrderLabJamOrder = olh.OrderLabJamOrder;
        //                                                old.OrderLabStatus = string.Empty;
        //                                                old.OrderLabCito = (entity2.IsCito ?? false) ? "C" : string.Empty;
        //                                            }

        //                                            if (details.Any()) details.Save();
        //                                        }
        //                                        break;
        //                                    #endregion
        //                                    #region SYSMEX_LIS_INTEROP_CONNECTION_NAME
        //                                    case AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME:
        //                                    case AppConstant.HIS_INTEROP.SYSMEX_LIS_INTEROP_CONNECTION_NAME:
        //                                        if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
        //                                        {
        //                                            var lo = new BusinessObject.Interop.SYSMEX.LisOrder();
        //                                            if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME) lo.es.Connection.Name = AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME;
        //                                            else lo.es.Connection.Name = AppConstant.HIS_INTEROP.SYSMEX_LIS_INTEROP_CONNECTION_NAME;

        //                                            lo.MessageDt = (entity.LastUpdateDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
        //                                            lo.OrderControl = "NW";
        //                                            lo.Pid = patient.MedicalNo;
        //                                            lo.Pname = patient.PatientName;
        //                                            lo.Address1 = patient.StreetName.Trim();

        //                                            switch (AppSession.Parameter.HealthcareInitialAppsVersion)
        //                                            {
        //                                                case "RSUTAMA":
        //                                                    lo.Address2 = patient.District;
        //                                                    lo.Address3 = patient.County + " " + patient.State;
        //                                                    lo.Address4 = patient.MobilePhoneNo;
        //                                                    break;
        //                                                case "RSMP":
        //                                                case "GRHA":
        //                                                    lo.Address2 = patient.District;
        //                                                    lo.Address3 = patient.County + " " + patient.State;
        //                                                    lo.Address4 = grr.GuarantorName;
        //                                                    break;
        //                                                case "RSSMCB":
        //                                                    lo.Address2 = grr.GuarantorName;
        //                                                    lo.Address3 = patient.District.Trim() + " " + patient.County.Trim();
        //                                                    if (AppSession.Parameter.HealthcareInitial == "RSSMHB")
        //                                                    {
        //                                                        lo.Address3 += " " + patient.State.Trim();

        //                                                        var mb = new MergeBilling();
        //                                                        if (mb.LoadByPrimaryKey(reg.RegistrationNo))
        //                                                        {
        //                                                            if (!string.IsNullOrEmpty(mb.FromRegistrationNo))
        //                                                            {
        //                                                                var freg = new Registration();
        //                                                                freg.LoadByPrimaryKey(mb.FromRegistrationNo);
        //                                                                var funit = new ServiceUnit();
        //                                                                funit.LoadByPrimaryKey(freg.ServiceUnitID);
        //                                                                lo.Address4 = funit.ServiceUnitID + "^" + funit.ServiceUnitName;
        //                                                            }
        //                                                            else lo.Address4 = unit.ServiceUnitID + "^" + unit.ServiceUnitName; ;
        //                                                        }
        //                                                    }
        //                                                    else lo.Address4 = patient.State;
        //                                                    break;
        //                                                default:
        //                                                    lo.Address2 = patient.District;
        //                                                    lo.Address3 = patient.County;
        //                                                    lo.Address4 = patient.State;
        //                                                    break;
        //                                            }

        //                                            lo.Ptype = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? "IN" : "OP";
        //                                            lo.BirthDt = (patient.DateOfBirth ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
        //                                            lo.Sex = patient.Sex == "M" ? "1" : "0";
        //                                            lo.Ono = entity.TransactionNo;
        //                                            lo.RequestDt = (entity.LastUpdateDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");

        //                                            unit = new ServiceUnit();
        //                                            unit.LoadByPrimaryKey(entity.FromServiceUnitID);

        //                                            lo.Source = unit.ServiceUnitID + "^" + unit.ServiceUnitName;

        //                                            var param = new Paramedic();

        //                                            switch (AppSession.Parameter.HealthcareInitialAppsVersion)
        //                                            {
        //                                                case "RSUTAMA":
        //                                                    if (!string.IsNullOrEmpty(reg.ReferralID))
        //                                                    {
        //                                                        var refer = new Referral();
        //                                                        refer.LoadByPrimaryKey(reg.ReferralID);

        //                                                        lo.Clinician = reg.ReferralID + "^" + refer.ReferralName;
        //                                                    }
        //                                                    else if (!string.IsNullOrEmpty(reg.PhysicianSenders))
        //                                                    {
        //                                                        lo.Clinician = reg.ParamedicID + "^" + reg.PhysicianSenders;
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        param.LoadByPrimaryKey(reg.ParamedicID);

        //                                                        lo.Clinician = reg.ParamedicID + "^" + param.ParamedicName;
        //                                                    }
        //                                                    break;
        //                                                default:
        //                                                    param.LoadByPrimaryKey(reg.ParamedicID);

        //                                                    lo.Clinician = reg.ParamedicID + "^" + param.ParamedicName;
        //                                                    break;
        //                                            }

        //                                            lo.RoomNo = reg.RoomID;
        //                                            lo.Priority = transChargesItems.Any(t => (t.IsCito ?? false)) ? "U" : "R";

        //                                            if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME) lo.Cmt = grr.GuarantorName;
        //                                            else lo.Cmt = entity.Notes; //string.Empty;

        //                                            lo.Visitno = entity.RegistrationNo;

        //                                            var items = new ItemCollection();
        //                                            items.Query.Where(items.Query.ItemID.In(transChargesItems.Where(t => string.IsNullOrEmpty(t.ParentNo)).Select(t => t.ItemID)));
        //                                            items.Query.Load();

        //                                            foreach (var item in items)
        //                                            {
        //                                                if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME) lo.OrderTestid += item.ItemID + "~";
        //                                                else lo.OrderTestid += item.ItemIDExternal + "~";

        //                                                //lo.OrderTestid += item.ItemIDExternal + "~";
        //                                            }

        //                                            lo.Save();
        //                                        }
        //                                        break;
        //                                    #endregion
        //                                    #region PRODIA_LIS_INTEROP_CONNECTION_NAME
        //                                    case AppConstant.HIS_INTEROP.PRODIA_LIS_INTEROP_CONNECTION_NAME:
        //                                        break;
        //                                    #endregion
        //                                }
        //                            }
        //                            #endregion
        //                        }
        //                    }

        //                    //if (!string.IsNullOrEmpty(entity.PackageReferenceNo))
        //                    //{
        //                    //    int? x = ParamedicFeeTransChargesItemCompSettled.AddNewSettled(entity, TransChargesItemComps, AppSession.UserLogin.UserID, false);
        //                    //}
        //                }
        //            }
        //        }

        //        //Commit if success, Rollback if failed
        //        trans.Complete();

        //        return string.Empty;
        //    }
        //}
        //#endregion

    }
}
