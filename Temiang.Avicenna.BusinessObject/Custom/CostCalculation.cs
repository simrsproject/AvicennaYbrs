using System;
using System.Linq;
using System.Collections.Generic;
using Temiang.Dal.Interfaces;
using System.Data;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CostCalculation
    {
        public DateTime? DischargeDate
        {
            get { return (DateTime?)GetColumn("refTo_DischargeDate"); }
            set { SetColumn("refTo_DischargeDate", value); }
        }

        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }

        public string ServiceUnitName
        {
            get { return GetColumn("refToServiceUnit_ServiceUnitName").ToString(); }
            set { SetColumn("refToServiceUnit_ServiceUnitName", value); }
        }

        public DateTime? TransactionDate
        {
            get { return (DateTime?)GetColumn("refToTransaction_TransactionDate"); }
            set { SetColumn("refToTransaction_TransactionDate", value); }
        }

        public string ReferenceNo
        {
            get { return GetColumn("refToTransaction_ReferenceNo").ToString(); }
            set { SetColumn("refToTransaction_ReferenceNo", value); }
        }

        public string ClassName
        {
            get { return GetColumn("refToClass_ClassName").ToString(); }
            set { SetColumn("refToClass_ClassName", value); }
        }

        public string PaymentNo
        {
            get { return GetColumn("refToPayment_PaymentNo").ToString(); }
            set { SetColumn("refToPayment_PaymentNo", value); }
        }

        public string ItemGroupID
        {
            get { return GetColumn("refToItemGroup_ItemGroupID").ToString(); }
            set { SetColumn("refToItemGroup_ItemGroupID", value); }
        }

        public string ItemTypeID
        {
            get { return GetColumn("refToAppStdRef_ItemTypeID").ToString(); }
            set { SetColumn("refToAppStdRef_ItemTypeID", value); }
        }

        public decimal AmountTotal
        {
            get { return (decimal)GetColumn("refToCostcalculation_AmountTotal"); }
            set { SetColumn("refToCostcalculation_AmountTotal", value); }
        }

        public decimal AmountTransaction {
            get {
                return (PatientAmount ?? 0) + (GuarantorAmount ?? 0) - (DiscountAmount ?? 0) - (DiscountAmount2 ?? 0);
            }
        }

        private bool _isAdjusted;
        public bool IsAdjusted {
            get {
                return _isAdjusted;
            }
            set {
                _isAdjusted = value;
            }
        }

        //AdjustedDisc _adjustedDisc = new AdjustedDisc();
        //public AdjustedDisc AdjustedDisc
        //{
        //    get { return _adjustedDisc; }
        //    set { _adjustedDisc = value; }
        //}

        public void ResetAdjustment() {
            AmountAdjusted = null;
            AdjustedDiscAmount = null;
            AdjustedDiscSelection = null;
        }

        public void SetAdjustmentDisc(AdjustedDisc AdjustedDisc) {
            AdjustedDiscAmount = AdjustedDisc.AdjustedDiscAmount;
            AdjustedDiscSelection = AdjustedDisc.AdjustedDiscSelection;
        }

        public void CopyAdjustmentDefault(TransChargesItemCollection tciColl, TransChargesItemCompCollection tcicColl) {
            // adjust costcalculation
            AmountAdjusted = AmountTransaction;
            // adjust tci
            var tci = GetTCI(tciColl);
            tci.PriceAdjusted = tci.GetFinalValue();
            // adjust tcic
            var tcics = GetTCICs(tcicColl);
            foreach (var tcic in tcics) {
                tcic.PriceAdjusted = tcic.GetFinalValue();
            }

            IsAdjusted = false;
        }

        public void SetAdjustmentPercent(TransChargesItemCollection tciColl, TransChargesItemCompCollection tcicColl, AdjustedDisc Disc)
        {
            if (Disc.AdjustedDiscSelection != 0) throw new Exception("Function only accept AdjustedDiscSelection = 0 (Discount by tarif)");
            AdjustedDiscAmount = Disc.AdjustedDiscAmount;
            AdjustedDiscSelection = Disc.AdjustedDiscSelection;

            AmountAdjusted = AmountTransaction - (AmountTransaction * (AdjustedDiscAmount ?? 0) / 100);

            // tci
            var tci = GetTCI(tciColl);
            tci.PriceAdjusted = AmountAdjusted;

            var tcics = GetTCICs(tcicColl);
            decimal oldVal = tcics.Sum(x => x.GetFinalValue());
            foreach (var tcic in tcics) {
                tcic.PriceAdjusted = AmountAdjusted / oldVal * tcic.GetFinalValue();
            }
        }

        public void SetAdjustmentValue(TransChargesItemCollection tciColl, TransChargesItemCompCollection tcicColl, decimal NewValue)
        {
            AmountAdjusted = NewValue;
            //AdjustedDiscAmount = 0;
            //AdjustedDiscSelection = 0;

            // tci
            var tci = GetTCI(tciColl);
            tci.PriceAdjusted = AmountAdjusted;

            var tcics = GetTCICs(tcicColl);
            decimal oldVal = tcics.Sum(x => x.GetFinalValue());
            foreach (var tcic in tcics) {
                if (oldVal == 0) tcic.PriceAdjusted = 0;
                else tcic.PriceAdjusted = NewValue / oldVal * tcic.GetFinalValue();
            }

            IsAdjusted = true;
        }

        public void SetAdjustmentValueWithoutTCIC(TransChargesItemCollection tciColl, TransChargesItemCompCollection tcicColl, decimal NewValue)
        {
            // validation
            var tcics = GetTCICs(tcicColl);
            decimal AdjVal = tcics.Sum(x => x.PriceAdjusted ?? 0);

            if (NewValue != AdjVal) throw new Exception("Invalid value");

            AmountAdjusted = NewValue;
            //AdjustedDiscAmount = 0;
            //AdjustedDiscSelection = 0;

            // tci
            var tci = GetTCI(tciColl);
            tci.PriceAdjusted = AmountAdjusted;

            IsAdjusted = true;
        }

        public TransChargesItem GetTCI(TransChargesItemCollection tciColl)
        {
            var tci = tciColl.Where(x => x.TransactionNo == TransactionNo && x.SequenceNo == SequenceNo).FirstOrDefault();
            if (tci == null)
            {
                tci = new TransChargesItem();
                tci.Query.Where(tci.Query.TransactionNo == TransactionNo, tci.Query.SequenceNo == SequenceNo);
                tci.Load(tci.Query);
                tciColl.AttachEntity(tci);
            }
            return tci;
        }
        public TransChargesItem GetTCI()
        {
            TransChargesItemCollection tciColl = new TransChargesItemCollection();
            return GetTCI(tciColl);
        }

        public IEnumerable<TransChargesItemComp> GetTCICs(TransChargesItemCompCollection tcicColl)
        {
            var tcics = tcicColl.Where(x => x.TransactionNo == TransactionNo && x.SequenceNo == SequenceNo);
            if (!tcics.Any())
            {
                var tcicColl2 = new TransChargesItemCompCollection();
                tcicColl2.Query.Where(tcicColl2.Query.TransactionNo == TransactionNo, tcicColl2.Query.SequenceNo == SequenceNo);
                tcicColl2.Load(tcicColl2.Query);
                foreach (var tcic2 in tcicColl2) {
                    tcicColl2.DetachEntity(tcic2);
                    tcicColl.AttachEntity(tcic2);
                }
                tcics = tcicColl.Where(x => x.TransactionNo == TransactionNo && x.SequenceNo == SequenceNo);
            }
            return tcics;
        }
        public IEnumerable<TransChargesItemComp> GetTCICs()
        {
            TransChargesItemCompCollection tcicColl = new TransChargesItemCompCollection();
            return GetTCICs(tcicColl);
        }

        public static int CostCalculationCleanUpProcess(string registrationNo)
        {
            var prms = new esParameters();
            prms.Add("p_RegistrationNo", registrationNo, esParameterDirection.Input, DbType.String, 20);

            var entity = new CostCalculation();
            entity.es.Connection.CommandTimeout = 60 * 5; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_CostCalculationCleanUpProcess", prms);
            return 1;
        }
    }

    public partial class CostCalculationCollection
    {
        public bool LoadByTransactionNo(string TransactionNo) {
            this.QueryReset();
            this.Query.Where(this.Query.TransactionNo == TransactionNo);
            return this.LoadAll();
        }
        public void ResetAdjustment(TransChargesItemCollection TransChargesItems, TransChargesItemCompCollection TransChargesItemComps) {
            foreach (var cc in this) {
                cc.ResetAdjustment();

                var tci = cc.GetTCI(TransChargesItems);
                tci.PriceAdjusted = null;
                // adjust tcic
                var tcics = cc.GetTCICs(TransChargesItemComps);
                foreach (var tcic in tcics)
                {
                    tcic.PriceAdjusted = null;
                }
            }
        }

        public void GetCostCalculationsByRegWithMergeBilling(string RegistrationNo)
        {
            var registrationNoList = MergeBilling.GetMergeRegistration(RegistrationNo);
            GetCostCalculationsByRegWithMergeBilling(registrationNoList);
        }
        public void GetCostCalculationsByRegWithMergeBilling(string RegistrationNo, bool IsPatientOnly)
        {
            var registrationNoList = MergeBilling.GetMergeRegistration(RegistrationNo);
            GetCostCalculationsByRegWithMergeBilling(registrationNoList, IsPatientOnly, false);
        }
        public void GetCostCalculationsByRegWithMergeBilling(string[] RegistrationNos)
        {
            GetCostCalculationsByRegWithMergeBilling(RegistrationNos, false, false);
        }
        public void GetCostCalculationsByRegWithMergeBilling(string[] RegistrationNos, bool IsPatientOnly, bool IntermbilledOnly) {
            var query = new CostCalculationQuery("a");
            var item = new ItemQuery("b");
            var unit = new ServiceUnitQuery("c");
            var view = new VwTransactionQuery("d");
            var pay = new TransPaymentItemOrderQuery("e");
            var viewItem = new VwTransactionItemQuery("y");
            var cls = new ClassQuery("cls");

            query.Select(
                    query,
                    view.TransactionDate.As("refToTransaction_TransactionDate"),
                    unit.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"),
                    @"<CASE WHEN y.ParamedicCollectionName = '' THEN b.ItemName ELSE b.ItemName + ' (' + y.ParamedicCollectionName + ')' END AS 'refToItem_ItemName'>",
                    view.ReferenceNo.As("refToTransaction_ReferenceNo"),
                    cls.ClassName.As("refToClass_ClassName"),
                    item.ItemGroupID.As("refToItemGroup_ItemGroupID"),
                    item.SRItemType.As("refToAppStdRef_ItemTypeID"),
                    "<CAST(0 as decimal(18,2)) as refToCostcalculation_AmountTotal>"
            );

            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.InnerJoin(view).On(query.TransactionNo == view.TransactionNo &&
                                     query.RegistrationNo == view.RegistrationNo);
            query.InnerJoin(viewItem).On(query.TransactionNo == viewItem.TransactionNo &&
                                         query.SequenceNo == viewItem.SequenceNo);

            query.LeftJoin(cls).On(view.ClassID == cls.ClassID);
            query.InnerJoin(unit).On(view.ServiceUnitID == unit.ServiceUnitID);
            query.LeftJoin(pay).On(
                query.TransactionNo == pay.TransactionNo &&
                query.SequenceNo == pay.SequenceNo &&
                pay.IsPaymentProceed == true &&
                pay.IsPaymentReturned == false
                );

            query.Where(
                query.RegistrationNo.In(RegistrationNos),
                view.PackageReferenceNo == string.Empty
                );
            if (IsPatientOnly) {
                query.Where(query.GuarantorAmount == 0, query.PatientAmount != 0);
            }
            if (IntermbilledOnly) {
                query.Where(query.IntermBillNo.IsNotNull());
            }

            query.OrderBy(
                unit.ServiceUnitName.Ascending,
                view.OrderDate.Ascending,
                view.OrderTransNo.Ascending,
                query.SequenceNo.Ascending,
                query.TransactionNo.Ascending
                );

            this.Load(query);

            foreach (var cc in this)
            {
                cc.AmountTotal = cc.AmountTransaction;
            }
        }

        public static decimal GetBillingTotalLab(string[] registrationNo)
        {
            var collection = new CostCalculationCollection();
            var query = new CostCalculationQuery("a");
            var i = new ItemQuery("i");
            query.InnerJoin(i).On(query.ItemID == i.ItemID)
                .Where(
                    query.RegistrationNo.In(registrationNo),
                    i.SRItemType == "31",
                    query.Or(query.ParentNo == string.Empty, query.ParentNo.IsNull()))
                .Select(query);

            collection.Load(query);

            decimal? patient = collection.Sum(c => c.PatientAmount), grr = collection.Sum(c => c.GuarantorAmount);
            var totalAmount = (patient ?? 0) + (grr ?? 0);

            return totalAmount;
        }

        public static decimal GetBillingTotalBhp(string[] registrationNo)
        {
            var collection = new CostCalculationCollection();
            var query = new CostCalculationQuery("a");
            var i = new ItemQuery("i");
            query.InnerJoin(i).On(query.ItemID == i.ItemID)
                .Where(
                    query.RegistrationNo.In(registrationNo),
                    i.SRItemType.In("11","21","81"),
                    query.Or(query.ParentNo == string.Empty, query.ParentNo.IsNull()))
                .Select(query);

            collection.Load(query);

            decimal? patient = collection.Sum(c => c.PatientAmount), grr = collection.Sum(c => c.GuarantorAmount);
            var totalAmount = (patient ?? 0) + (grr ?? 0);

            return totalAmount;
        }

        public static decimal GetBillingTotalAllTransactionInclAdm(string[] registrationNo, bool inclAdm)
        {
            var collection = new CostCalculationCollection();
            var query = new CostCalculationQuery("a");
            query.Where(query.RegistrationNo.In(registrationNo),
                        query.Or(query.ParentNo == string.Empty, query.ParentNo.IsNull()));

            collection.Load(query);

            decimal? patient = collection.Sum(c => c.PatientAmount), grr = collection.Sum(c => c.GuarantorAmount);
            var totalAmount = (patient ?? 0) + (grr ?? 0);

            if (inclAdm)
            {
                var ibs = collection.Where(x => (x.IntermBillNo ?? string.Empty) != string.Empty)
                    .Select(x => x.IntermBillNo).Distinct().ToArray();

                if (ibs.Count() > 0)
                {
                    var ibColl = new IntermBillCollection();
                    ibColl.Query.Where(ibColl.Query.IntermBillNo.In(ibs));
                    if (ibColl.LoadAll())
                    {
                        foreach (var ib in ibColl)
                        {
                            totalAmount += (
                                ib.AdministrationAmount.Value +
                                ib.GuarantorAdministrationAmount.Value -
                                ib.DiscAdmPatient.Value - ib.DiscAdmGuarantor.Value
                            );
                        }
                    }
                }
            }

            return totalAmount;
        }

        public static decimal GetBillingTotalAllTransactionIntermbillInclAdm(string[] intermbillNo, bool inclAdm)
        {
            decimal totalAmount = 0;
            var ibColl = new IntermBillCollection();
            ibColl.Query.Where(ibColl.Query.IntermBillNo.In(intermbillNo));
            if (ibColl.LoadAll())
            {
                foreach (var ib in ibColl)
                {
                    totalAmount += (ib.PatientAmount ?? 0 + ib.GuarantorAmount ?? 0);
                    if (inclAdm)
                    {
                        totalAmount += (
                        ib.AdministrationAmount.Value +
                        ib.GuarantorAdministrationAmount.Value -
                        ib.DiscAdmPatient.Value - ib.DiscAdmGuarantor.Value);
                    }
                }
            }

            return totalAmount;
        }
    }
}
