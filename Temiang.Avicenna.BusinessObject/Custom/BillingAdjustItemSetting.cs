using System;
using System.Linq;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class BillingAdjustItemSetting
    {
        public string ParamedicName
        {
            get { return GetColumn("refToParamedic_Name").ToString(); }
            set { SetColumn("refToParamedic_Name", value); }
        }
        public string SRSpecialtyName
        {
            get { return GetColumn("refToSRSpecialty_Name").ToString(); }
            set { SetColumn("refToSRSpecialty_Name", value); }
        }
        public string SRRegistrationTypeName
        {
            get { return GetColumn("refToSRRegistrationType_Name").ToString(); }
            set { SetColumn("refToSRRegistrationType_Name", value); }
        }
        public string SRTariffTypeName
        {
            get { return GetColumn("refToSRTariffType_Name").ToString(); }
            set { SetColumn("refToSRTariffType_Name", value); }
        }
        public string GuarantorName
        {
            get { return GetColumn("refToGuarantor_Name").ToString(); }
            set { SetColumn("refToGuarantor_Name", value); }
        }
        public string ServiceUnitName
        {
            get { return GetColumn("refToServiceUnit_Name").ToString(); }
            set { SetColumn("refToServiceUnit_Name", value); }
        }
        public string ItemGroupReplacementName
        {
            get { return GetColumn("refToItemGroup_ReplacementName").ToString(); }
            set { SetColumn("refToItemGroup_ReplacementName", value); }
        }
        public string ItemName
        {
            get { return GetColumn("refToItem_Name").ToString(); }
            set { SetColumn("refToItem_Name", value); }
        }
        public string TariffComponentName
        {
            get { return GetColumn("refToTariffComponent_Name").ToString(); }
            set { SetColumn("refToTariffComponent_Name", value); }
        }
        public string ClassName
        {
            get { return GetColumn("refToClass_Name").ToString(); }
            set { SetColumn("refToClass_Name", value); }
        }

        public bool HasReplacement(CostCalculationCollection CostCalculations)
        {
            if (this.ItemGroupIDsReplacement == string.Empty) return false;
            return (CostCalculations.Where(x => this.GetItemGroupReplacementIDs.Contains(x.ItemGroupID)).Any());

        }

        public string[] GetItemGroupReplacementIDs
        {
            get
            {
                return this.ItemGroupIDsReplacement.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public void AddItemGroupReplacementID(string id)
        {
            if (Array.IndexOf(GetItemGroupReplacementIDs, id) >= 0)
            {
                ItemGroupIDsReplacement = ItemGroupIDsReplacement + "|" + id;
            }
        }

        public void FillReferrence(bool FillEmptyOnly)
        {
            // query related reference
            if (!string.IsNullOrEmpty(this.ParamedicID))
            {
                if (FillEmptyOnly && this.ParamedicName != string.Empty)
                {

                }
                else
                {
                    var par = new Paramedic();
                    if (par.LoadByPrimaryKey(this.ParamedicID))
                    {
                        this.ParamedicName = par.ParamedicName;
                    }
                }
            }
            else
            {
                this.ParamedicName = string.Empty;
            }
            if (!string.IsNullOrEmpty(this.SRSpecialty))
            {
                if (FillEmptyOnly && this.SRSpecialtyName != string.Empty)
                {

                }
                else
                {
                    var spc = new AppStandardReferenceItem();
                    spc.Query.Where(spc.Query.StandardReferenceID == "Specialty",
                        spc.Query.ItemID == this.SRSpecialty);
                    if (spc.Load(spc.Query))
                    {
                        this.SRSpecialtyName = spc.ItemName;
                    }
                }
            }
            else
            {
                this.SRSpecialtyName = string.Empty;
            }
            if (!string.IsNullOrEmpty(this.SRRegistrationType))
            {
                if (FillEmptyOnly && this.SRRegistrationTypeName != string.Empty)
                {

                }
                else
                {
                    var spc = new AppStandardReferenceItem();
                    spc.Query.Where(spc.Query.StandardReferenceID == "RegistrationType",
                        spc.Query.ItemID == this.SRRegistrationType);
                    if (spc.Load(spc.Query))
                    {
                        this.SRRegistrationTypeName = spc.ItemName;
                    }
                }
            }
            else
            {
                this.SRRegistrationTypeName = string.Empty;
            }
            if (!string.IsNullOrEmpty(this.SRTariffType))
            {
                if (FillEmptyOnly && this.SRTariffTypeName != string.Empty)
                {

                }
                else
                {
                    var spc = new AppStandardReferenceItem();
                    spc.Query.Where(spc.Query.StandardReferenceID == "TariffType",
                        spc.Query.ItemID == this.SRTariffType);
                    if (spc.Load(spc.Query))
                    {
                        this.SRTariffTypeName = spc.ItemName;
                    }
                }
            }
            else
            {
                this.SRTariffTypeName = string.Empty;
            }
            if (!string.IsNullOrEmpty(this.GuarantorID))
            {
                if (FillEmptyOnly && this.GuarantorName != string.Empty)
                {

                }
                else
                {
                    var guar = new Guarantor();
                    if (guar.LoadByPrimaryKey(this.GuarantorID))
                    {
                        this.GuarantorName = guar.GuarantorName;
                    }
                }
            }
            else
            {
                this.GuarantorName = string.Empty;
            }
            if (!string.IsNullOrEmpty(this.ServiceUnitID))
            {
                if (FillEmptyOnly && this.ServiceUnitName != string.Empty)
                {

                }
                else
                {
                    var su = new ServiceUnit();
                    if (su.LoadByPrimaryKey(this.ServiceUnitID))
                    {
                        this.ServiceUnitName = su.ServiceUnitName;
                    }
                }
            }
            else
            {
                this.ServiceUnitName = string.Empty;
            }

            this.ItemGroupReplacementName = string.Empty;
            if (!string.IsNullOrEmpty(this.ItemGroupIDsReplacement))
            {
                if (FillEmptyOnly && this.ItemGroupReplacementName != string.Empty)
                {

                }
                else
                {
                    if (this.GetItemGroupReplacementIDs.Length > 0)
                    {
                        var igColl = new ItemGroupCollection();
                        igColl.Query.Where(igColl.Query.ItemGroupID.In(this.GetItemGroupReplacementIDs));
                        if (igColl.LoadAll())
                        {
                            foreach (var ig in igColl)
                            {
                                this.ItemGroupReplacementName += (this.ItemGroupReplacementName == string.Empty ? "" : ", ") + ig.ItemGroupName;
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(this.ItemID))
            {
                if (FillEmptyOnly && this.ItemName != string.Empty)
                {

                }
                else
                {
                    var i = new Item();
                    if (i.LoadByPrimaryKey(this.ItemID))
                    {
                        this.ItemName = i.ItemName;
                    }
                }
            }
            else
            {
                this.ItemName = string.Empty;
            }
            if (!string.IsNullOrEmpty(this.ClassID))
            {
                if (FillEmptyOnly && this.ClassName != string.Empty)
                {

                }
                else
                {
                    var cl = new Class();
                    if (cl.LoadByPrimaryKey(this.ClassID))
                    {
                        this.ClassName = cl.ClassName;
                    }
                }
            }
            else
            {
                this.ClassName = string.Empty;
            }
            if (!string.IsNullOrEmpty(this.TariffComponentID))
            {
                if (FillEmptyOnly && this.TariffComponentName != string.Empty)
                {

                }
                else
                {
                    var tf = new TariffComponent();
                    if (tf.LoadByPrimaryKey(this.TariffComponentID))
                    {
                        this.TariffComponentName = tf.TariffComponentName;
                    }
                }
            }
            else
            {
                this.TariffComponentName = string.Empty;
            }
        }
    }
}
