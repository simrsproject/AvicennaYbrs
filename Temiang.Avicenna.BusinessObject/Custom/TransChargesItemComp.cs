using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransChargesItemComp
    {
        public string TariffComponentName
        {
            get { return GetColumn("refToTariffComponent_TariffComponentName").ToString(); }
            set { SetColumn("refToTariffComponent_TariffComponentName", value); }
        }

        public decimal CalculateParamedicPercentDiscount(bool IsTarifCompPhysicianDiscountMaxByShare,
            string RegistrationNo, string ItemID, decimal? DiscountRule, string UserID, 
            string ClassID, string ToServiceUnitID)
        {
            //if (DiscountRule > this.Price /*+ this.CitoAmount --> tambah cito kah?*/) DiscountRule = this.Price /*+ this.CitoAmount*/;
            if (DiscountRule > this.Price + this.CitoAmount) DiscountRule = this.Price + this.CitoAmount;
            DiscountRule = Math.Round(DiscountRule ?? 0, 2);
            this.DiscountAmount = (DiscountRule ?? 0);
            //this.DiscountAmount = DiscountRule;
            this.FeeDiscountPercentage = this.FeeDiscountPercentage ?? 0;
            if (!IsTarifCompPhysicianDiscountMaxByShare) return 0;

            if (string.IsNullOrEmpty(this.ParamedicID)) return 0;
            if (string.IsNullOrEmpty(RegistrationNo)) return 0; /**/

            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            feeColl.LoadEmpty();

            var fee = feeColl.AddNew();

            fee.RegistrationNo = RegistrationNo;
            fee.ItemID = ItemID;
            fee.TariffComponentID = this.TariffComponentID;
            fee.ParamedicID = this.ParamedicID;
            fee.Qty = 1; // System.Convert.ToDecimal(chargeQty);
            fee.Price = (this.Price ?? 0) + (this.CitoAmount ?? 0) - (DiscountRule ?? 0);
            //fee.CalculateFee_disabled(this.FeeDiscountPercentage, string.Empty);
            fee.RegistrationNoMergeTo = fee.RegistrationNo;

            var reg = new Registration();
            reg.LoadByPrimaryKey(fee.RegistrationNo);
            var guar = new Guarantor();
            guar.LoadByPrimaryKey(reg.GuarantorID);

            fee.GuarantorID = reg.GuarantorID;
            fee.SRPhysicianFeeCategory = guar.SRPhysicianFeeCategory;
            fee.SRRegistrationType = reg.SRRegistrationType;
            fee.IsProrata = guar.IsProrateParamedicFee ?? false;
            fee.ClassID = ClassID;
            fee.ServiceUnitIDTo = ToServiceUnitID;
            fee.TCIC = this;
            feeColl.CalculateGrossFee(UserID);
            feeColl.CalculateDeductionBeforeTax(fee.TransactionNo, fee.SequenceNo, fee.TariffComponentID, UserID);

            var dFee = fee.FeeAmount ?? 0;

            this.FeeCalculated = fee.CalculatedAmount;
            // diskonnya adalah diskon dari rule ditambah diskon kalkulasi jasmed
            this.DiscountAmount = (DiscountRule ?? 0) + (fee.DiscountExtra ?? 0);
            this.FeeDiscount = (fee.DiscountExtra ?? 0);

            return fee.FeeAmount ?? 0;
        }

        public decimal GetFinalValue()
        {
            return (Price ?? 0) - (DiscountAmount ?? 0) + (CitoAmount ?? 0);
        }
    }
}
