using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransChargesItem
    {
        public string SRRegistrationType
        {
            get { return GetColumn("refToRegistration_SRRegistrationType").ToString(); }
            set { SetColumn("refToRegistration_SRRegistrationType", value); }
        }

        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }

        public string ParamedicName
        {
            get { return GetColumn("refToParamedic_ParamedicName").ToString(); }
            set { SetColumn("refToParamedic_ParamedicName", value); }
        }

        public string CombinedNotes
        {
            get { return GetColumn("refTo_CombinedNotes").ToString(); }
            set { SetColumn("refTo_CombinedNotes", value); }
        }

        public decimal? Total
        {
            get { return (decimal?)GetColumn("refToTransChargesItem_Total"); }
            set { SetColumn("refToTransChargesItem_Total", value); }
        }

        public string ServiceUnitName
        {
            get { return GetColumn("refToServiceUnit_ServiceUnitName").ToString(); }
            set { SetColumn("refToServiceUnit_ServiceUnitName", value); }
        }
        public string ToServiceUnitIDHeader
        {
            get { return GetColumn("refToTransCharges_ToServiceUnitID").ToString(); }
            set { SetColumn("refToTransCharges_ToServiceUnitID", value); }
        }

        public string LocationID
        {
            get { return GetColumn("refToServiceUnit_LocationID").ToString(); }
            set { SetColumn("refToServiceUnit_LocationID", value); }
        }

        public string SRItemType
        {
            get { return GetColumn("refToItem_SRItemType").ToString(); }
            set { SetColumn("refToItem_SRItemType", value); }
        }

        public bool? IsItemTypeService
        {
            get { return (bool?)GetColumn("refTo_IsItemTypeService"); }
            set { SetColumn("refTo_IsItemTypeService", value); }
        }

        public string IntermBillNo
        {
            get { return GetColumn("refToCostCalculation_IntermBillNo").ToString(); }
            set { SetColumn("refToCostCalculation_IntermBillNo", value); }
        }

        public string TransactionCorrectionNo
        {
            get { return GetColumn("refTo_TransactionCorrectionNo").ToString(); }
            set { SetColumn("refTo_TransactionCorrectionNo", value); }
        }

        public bool? IsOrderConfirmed
        {
            get { return (bool?)GetColumn("refTo_IsPaymentConfirmed"); }
            set { SetColumn("refTo_IsPaymentConfirmed", value); }
        }

        public bool? IsCasemixApprovedFlag
        {
            get { return (bool?)GetColumn("refto_IsCasemixApprovedFlag"); }
            set { SetColumn("refto_IsCasemixApprovedFlag", value); }
        }

        public decimal GetFinalValue()
        {
            return ((ChargeQuantity ?? 0) * (Price ?? 0) - (DiscountAmount ?? 0)) + (CitoAmount ?? 0);
        }

        private string _ParentNoByTransactionNo;
        //public string ParentNoByTransactionNo{
        //    get { return _ParentNoByTransactionNo; }
        //    set { _ParentNoByTransactionNo = value; }
        //}

        public string ParentNoByTransactionNo
        {
            get { return GetColumn("refTo_ParentNoByTransactionNo").ToString(); }
            set { SetColumn("refTo_ParentNoByTransactionNo", value); }
        }

        public string ItemGroupName
        {
            get { return GetColumn("refToItemGroup_ItemGroupName").ToString(); }
            set { SetColumn("refToItemGroup_ItemGroupName", value); }
        }
        public string PrevOrder
        {
            get { return GetColumn("refTo_PrevOrder").ToString(); }
            set { SetColumn("refTo_PrevOrder", value); }
        }

        public string SpecimenTypeName
        {
            get { return GetColumn("refTo_SpecimenTypeName").ToString(); }
            set { SetColumn("refTo_SpecimenTypeName", value); }
        }

        public string SpecimenStatus
        {
            get { return GetColumn("refTo_SpecimenStatus").ToString(); }
            set { SetColumn("refTo_SpecimenStatus", value); }
        }

        public string ItemConditionRuleName
        {
            get { return GetColumn("refToItemConditionRule_ItemConditionRuleName").ToString(); }
            set { SetColumn("refToItemConditionRule_ItemConditionRuleName", value); }
        }

        public void SetPrevOrder(string RegistrationNo, int iHourInterval)
        {
            if (iHourInterval == 0) return;

            var tciCollPrev = new TransChargesItemCollection();
            var tciPrev = new TransChargesItemQuery("tci");
            var tcPrev = new TransChargesQuery("tc");
            tciPrev.InnerJoin(tcPrev).On(tciPrev.TransactionNo == tcPrev.TransactionNo)
                .Where(tcPrev.RegistrationNo == RegistrationNo,
                    /*tcPrev.IsOrder == true, */tcPrev.IsVoid == false,
                    tciPrev.IsVoid == false,
                    tcPrev.TransactionNo != this.TransactionNo
                );
            if (tciCollPrev.Load(tciPrev))
            {
                SetPrevOrder(tciCollPrev, iHourInterval);
            }
        }

        public void SetPrevOrder(TransChargesItemCollection tciCollPrev, int iHourInterval)
        {
            if (tciCollPrev.Count == 0) return;
            if (iHourInterval == 0) return;

            var item = new Item();
            if (item.LoadByPrimaryKey(this.ItemID))
            {
                if ((new string[] { ItemType.Laboratory, ItemType.Radiology, ItemType.Service }).Contains(item.SRItemType))
                {
                    var tciLastPrev = tciCollPrev.Where(t => t.ItemID == this.ItemID && t.TransactionNo != this.TransactionNo && t.CreatedDateTime < this.CreatedDateTime)
                        .OrderByDescending(t => t.CreatedDateTime)
                        .FirstOrDefault();
                    if (tciLastPrev != null)
                    {
                        //var hr = (this.CreatedDateTime - tciLastPrev.CreatedDateTime).Value.Hours;
                        TimeSpan hr = (this.CreatedDateTime.Value - tciLastPrev.CreatedDateTime.Value);
                        if (item.IntervalOrderWarning > 0)
                            iHourInterval = item.IntervalOrderWarning.ToInt();

                        if (hr.TotalHours < iHourInterval)
                        {
                            this.PrevOrder = string.Format("Warning: Duplicate order within {0} hours. Please continue if necessary.", iHourInterval.ToString());
                        }
                    }
                    if (item.SRItemType == ItemType.Laboratory)
                    {
                        var itemlab = new ItemLaboratory();
                        if (itemlab.LoadByPrimaryKey(this.ItemID) & !string.IsNullOrEmpty(itemlab.SRSpecimenType))
                        {
                            var specimentype = new AppStandardReferenceItem();
                            if (specimentype.LoadByPrimaryKey("SpecimenType", itemlab.SRSpecimenType))
                            {
                                this.SpecimenTypeName = "Specimen Type: " + specimentype.ItemName;
                            }
                        }
                    }
                }
            }
        }
    }
}
