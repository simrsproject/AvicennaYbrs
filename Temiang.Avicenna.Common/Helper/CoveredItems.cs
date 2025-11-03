using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Common
{
    public partial class Helper
    {
        public static DataTable GetCoveredItems(string registrationNo, string guarantorID, string itemID, DateTime transactionDate, bool isPrescription)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            var regType = reg.SRRegistrationType;

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(guarantorID);

            //sudah ditentukan di depan, jd gak perlu didefinisikan lg
            //if (grr.TariffCalculationMethod == 1) transactionDate = reg.RegistrationDate.Value.Date;

            if (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf)
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                if (!string.IsNullOrEmpty(pat.str.MemberID)) guarantorID = pat.str.MemberID;
            }

            if (grr.IsItemRuleUsingDefaultAmountValue ?? true) regType = "IPR";
            else
            {
                if (regType != "EMR")
                {
                    var merge = new BusinessObject.MergeBilling();
                    if (merge.LoadByPrimaryKey(reg.RegistrationNo) && !string.IsNullOrEmpty(merge.FromRegistrationNo))
                    {
                        var r = new Registration();
                        r.LoadByPrimaryKey(merge.FromRegistrationNo);
                        regType = r.SRRegistrationType;
                    }
                }
                if (regType == "MCU") regType = "OPR";
            }

            decimal amountValue;
            if (regType == "IPR") amountValue = grr.AmountValue ?? 0;
            else if (regType == "EMR") amountValue = grr.EmergencyAmountValue ?? 0;
            else amountValue = grr.OutpatientAmountValue ?? 0;

            var i = new ItemQuery();

            if (reg.SRBussinesMethod == AppSession.Parameter.BusinessMethodFlavon && (!reg.IsGlobalPlafond ?? false))
                i.Select(string.Format("<CAST('{0}' AS NUMERIC(18, 2)) AS [AmountValue]>", reg.PlavonAmount == 0 ? amountValue : reg.PlavonAmount));
            else i.Select(string.Format("<CAST('{0}' AS NUMERIC(18, 2)) AS [AmountValue]>", amountValue));

            i.Select(
                    i.ItemID,
                    string.Format("<'{0}' AS [SRGuarantorRuleType]>", grr.SRGuarantorRuleType),
                    string.Format("<CAST('{0}' AS BIT) AS [IsValueInPercent]>", (grr.IsValueInPercent ?? false ? 1 : 0)),
                    "<CAST('1' AS BIT) AS [IsInclude]>",
                    string.Format("<CAST('{0}' AS BIT) AS [IsGuarantor]>", (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf ? 0 : 1)),
                    "<CAST('0' AS NUMERIC(18, 2)) AS [BasicPrice]>",
                    "<CAST('0' AS NUMERIC(18, 2)) AS [CoveragePrice]>",
                    "<CAST('0' AS BIT) AS [IsByTariffComponent]>",
                    "<'' AS [TariffComponentValue]>"
                );
            i.Where(i.ItemID == itemID);
            i.OrderBy(i.ItemID.Ascending);

            var tbl = i.LoadDataTable();

            foreach (DataRow row in tbl.Rows)
            {
                var basic = (Tariff.GetItemTariff(transactionDate, grr.SRTariffType, reg.ChargeClassID, reg.ChargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType) ??
                             Tariff.GetItemTariff(transactionDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType)) ??
                            (Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, reg.ChargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType) ??
                             Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType));

                row["BasicPrice"] = basic == null ? 0 : basic.Price;

                var coveredClassId = grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf ? reg.ChargeClassID : reg.CoverageClassID;

                ItemTariff cover = (Tariff.GetItemTariff(transactionDate, grr.SRTariffType, coveredClassId, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType) ??
                                    Tariff.GetItemTariff(transactionDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType)) ??
                                   (Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, coveredClassId, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType) ??
                                    Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType));

                row["CoveragePrice"] = cover == null ? 0 : cover.Price;
            }

            tbl.AcceptChanges();

            var types = new GuarantorItemTypeRuleCollection();
            types.Query.Where(types.Query.GuarantorID == guarantorID);
            types.LoadAll();

            foreach (DataRow row in tbl.Rows)
            {
                var item = new Item();
                item.LoadByPrimaryKey(row["ItemID"].ToString());
                if (item.SRItemType == ItemType.Medical)
                {
                    row["IsInclude"] = grr.IsIncludeItemMedical ?? false;
                    row["IsGuarantor"] = grr.IsIncludeItemMedicalToGuarantor ?? false;
                }
                else if (item.SRItemType == ItemType.NonMedical || item.SRItemType == ItemType.Kitchen)
                {
                    row["IsInclude"] = grr.IsIncludeItemNonMedical ?? false;
                    row["IsGuarantor"] = grr.IsIncludeItemNonMedicalToGuarantor ?? false;
                }
                else
                {
                    row["IsInclude"] = true;
                    if (types.AsEnumerable().Any()) row["IsGuarantor"] = types.SingleOrDefault(t => t.SRItemType == item.SRItemType).IsToGuarantor;
                    else row["IsGuarantor"] = true;
                }
            }

            var grrItemColl = new GuarantorItemRuleCollection();
            grrItemColl.Query.Where(grrItemColl.Query.GuarantorID == guarantorID);
            grrItemColl.LoadAll();

            foreach (GuarantorItemRule grrItem in grrItemColl)
            {
                foreach (DataRow row in tbl.Rows.Cast<DataRow>().Where(row => row["ItemID"].ToString().Equals(grrItem.ItemID)))
                {
                    row["SRGuarantorRuleType"] = grrItem.SRGuarantorRuleType;
                    row["IsByTariffComponent"] = grrItem.IsByTariffComponent ?? false;

                    if (grrItem.IsByTariffComponent ?? false)
                    {
                        var comps = Helper.Tariff.GetItemTariffComponentCollection(transactionDate, grr.SRTariffType, reg.ChargeClassID, grrItem.ItemID);
                        if (comps == null || comps.Count() == 0)
                        {
                            comps = Helper.Tariff.GetItemTariffComponentCollection(transactionDate, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, grrItem.ItemID);
                            if (comps == null || comps.Count() == 0)
                                comps = Helper.Tariff.GetItemTariffComponentCollection(transactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, grrItem.ItemID);
                        }
                        var girtc = new GuarantorItemRuleTariffComponentCollection();
                        girtc.Query.Where(girtc.Query.GuarantorID == guarantorID, girtc.Query.ItemID == grrItem.ItemID, girtc.Query.TariffComponentID.In(comps.Select(c => c.TariffComponentID)));
                        if (girtc.Query.Load())
                        {
                            //format : 
                            //BasicPrice1/CoveragePrice1/TariffComponentID1/AmountValue1;
                            //BasicPrice.../CoveragePrice.../TariffComponentID.../AmountValue...;
                            //...
                            var str = string.Empty;
                            foreach (var gir in girtc)
                            {
                                var tblBasic = Helper.Tariff.GetItemTariffComponent(transactionDate, grr.SRTariffType, reg.ChargeClassID, gir.TariffComponentID, grrItem.ItemID);
                                decimal tcBasic = 0;
                                if (tblBasic == null || tblBasic.Rows.Count == 0) tcBasic = 0;
                                else tcBasic = Convert.ToDecimal(tblBasic.Rows[0]["Price"]);

                                decimal tcCoverage = 0;
                                var tblCoverage = Helper.Tariff.GetItemTariffComponent(transactionDate, grr.SRTariffType, grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf ? reg.ChargeClassID : reg.CoverageClassID, gir.TariffComponentID, grrItem.ItemID);
                                if (tblCoverage == null || tblCoverage.Rows.Count == 0) tcBasic = 0;
                                else tcCoverage = Convert.ToDecimal(tblBasic.Rows[0]["Price"]);

                                str += string.Format("{0}/{1}/{2}/", tcBasic.ToString(), tcCoverage.ToString(), gir.TariffComponentID);
                                switch (regType)
                                {
                                    case "IPR":
                                        str += (gir.AmountValue ?? 0).ToString() + ";";
                                        break;
                                    case "OPR":
                                        str += (gir.OutpatientAmountValue ?? 0).ToString() + ";";
                                        break;
                                    case "EMR":
                                        str += (gir.EmergencyAmountValue ?? 0).ToString() + ";";
                                        break;
                                }
                            }
                            str = str.Remove(str.Length - 1, 1);
                            row["TariffComponentValue"] = str;
                        }
                        else
                        {
                            switch (regType)
                            {
                                case "IPR":
                                    row["AmountValue"] = grrItem.AmountValue ?? 0;
                                    break;
                                case "OPR":
                                    row["AmountValue"] = grrItem.OutpatientAmountValue ?? 0;
                                    break;
                                case "EMR":
                                    row["AmountValue"] = grrItem.EmergencyAmountValue ?? 0;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (regType)
                        {
                            case "IPR":
                                row["AmountValue"] = grrItem.AmountValue ?? 0;
                                break;
                            case "OPR":
                                row["AmountValue"] = grrItem.OutpatientAmountValue ?? 0;
                                break;
                            case "EMR":
                                row["AmountValue"] = grrItem.EmergencyAmountValue ?? 0;
                                break;
                        }
                    }

                    row["IsValueInPercent"] = grrItem.IsValueInPercent ?? false;
                    row["IsInclude"] = grrItem.IsInclude ?? false;
                    row["IsGuarantor"] = grrItem.IsToGuarantor ?? false;
                    break;
                }
            }

            if (isPrescription)
            {
                var grrItemPrescColl = new GuarantorItemPrescriptionRuleCollection();
                grrItemPrescColl.Query.Where(grrItemPrescColl.Query.GuarantorID == guarantorID);
                grrItemPrescColl.LoadAll();

                foreach (GuarantorItemPrescriptionRule grrItemPresc in grrItemPrescColl)
                {
                    foreach (DataRow row in tbl.Rows.Cast<DataRow>().Where(row => row["ItemID"].ToString().Equals(grrItemPresc.ItemID)))
                    {
                        row["SRGuarantorRuleType"] = grrItemPresc.SRGuarantorRuleType;

                        switch (regType)
                        {
                            case "IPR":
                                row["AmountValue"] = grrItemPresc.AmountValue ?? 0;
                                break;
                            case "OPR":
                                row["AmountValue"] = grrItemPresc.OutpatientAmountValue ?? 0;
                                break;
                            case "EMR":
                                row["AmountValue"] = grrItemPresc.EmergencyAmountValue ?? 0;
                                break;

                        }

                        row["IsValueInPercent"] = grrItemPresc.IsValueInPercent ?? false;
                        row["IsInclude"] = grrItemPresc.IsInclude ?? false;
                        row["IsGuarantor"] = grrItemPresc.IsToGuarantor ?? false;
                        break;
                    }
                }
            }

            var regItemColl = new RegistrationItemRuleCollection();
            regItemColl.Query.Where(regItemColl.Query.RegistrationNo == registrationNo);
            regItemColl.LoadAll();

            foreach (RegistrationItemRule regItem in regItemColl)
            {
                foreach (DataRow row in tbl.Rows.Cast<DataRow>().Where(row => row["ItemID"].ToString().Equals(regItem.ItemID)))
                {
                    row["SRGuarantorRuleType"] = regItem.SRGuarantorRuleType;

                    switch (regType)
                    {
                        case "IPR":
                            row["AmountValue"] = regItem.AmountValue ?? 0;
                            break;
                        case "OPR":
                            row["AmountValue"] = regItem.OutpatientAmountValue ?? 0;
                            break;
                        case "EMR":
                            row["AmountValue"] = regItem.EmergencyAmountValue ?? 0;
                            break;

                    }

                    row["IsValueInPercent"] = regItem.IsValueInPercent ?? false;
                    row["IsInclude"] = regItem.IsInclude ?? false;
                    row["IsGuarantor"] = regItem.IsToGuarantor ?? false;
                    break;
                }
            }

            return tbl;
        }

        public static DataTable GetCoveredItems(string registrationNo, string guarantorID, string[] itemID, DateTime transactionDate, bool isPrescription)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            var regType = reg.SRRegistrationType;

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(guarantorID);

            //if (grr.TariffCalculationMethod == 1) transactionDate = reg.RegistrationDate.Value.Date;

            if (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf)
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                if (!string.IsNullOrEmpty(pat.str.MemberID)) guarantorID = pat.str.MemberID;
            }

            if (grr.IsItemRuleUsingDefaultAmountValue ?? true) regType = "IPR";
            else
            {
                if (regType != "EMR")
                {
                    var merge = new BusinessObject.MergeBilling();
                    if (merge.LoadByPrimaryKey(reg.RegistrationNo) && !string.IsNullOrEmpty(merge.FromRegistrationNo))
                    {
                        var r = new Registration();
                        r.LoadByPrimaryKey(merge.FromRegistrationNo);
                        regType = r.SRRegistrationType;
                    }
                }
                if (regType == "MCU") regType = "OPR";
            }

            decimal amountValue;
            if (regType == "IPR") amountValue = grr.AmountValue ?? 0;
            else if (regType == "EMR") amountValue = grr.EmergencyAmountValue ?? 0;
            else amountValue = grr.OutpatientAmountValue ?? 0;

            var i = new ItemQuery("a");
            var ipm = new ItemProductMedicQuery("b");

            if (reg.SRBussinesMethod == AppSession.Parameter.BusinessMethodFlavon && (!reg.IsGlobalPlafond ?? false))
                i.Select(string.Format("<CAST('{0}' AS NUMERIC(18, 2)) AS [AmountValue]>", reg.PlavonAmount == 0 ? amountValue : reg.PlavonAmount));
            else i.Select(string.Format("<CAST('{0}' AS NUMERIC(18, 2)) AS [AmountValue]>", amountValue));

            i.Select
                (
                    i.ItemID,
                    string.Format("<'{0}' AS [SRGuarantorRuleType]>", grr.SRGuarantorRuleType),
                    string.Format("<CAST('{0}' AS BIT) AS [IsValueInPercent]>", (grr.IsValueInPercent ?? false ? 1 : 0)),
                    "<CAST('1' AS BIT) AS [IsInclude]>",
                    string.Format("<CAST('{0}' AS BIT) AS [IsGuarantor]>", (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf ? 0 : 1)),
                    "<CAST('0' AS NUMERIC(18, 2)) AS [BasicPrice]>",
                    "<CAST('0' AS NUMERIC(18, 2)) AS [CoveragePrice]>",
                    "<CAST('0' AS BIT) AS [IsByTariffComponent]>",
                    "<'' AS [TariffComponentValue]>",
                    "<ISNULL(b.SRTherapyGroup, '') AS [SRTherapyGroup]>"
                );
            i.LeftJoin(ipm).On(ipm.ItemID == i.ItemID);
            i.Where(i.ItemID.In(itemID));
            i.OrderBy(i.ItemID.Ascending);

            DataTable tbl = i.LoadDataTable();

            foreach (DataRow row in tbl.Rows)
            {
                ItemTariff basic = (Tariff.GetItemTariff(transactionDate, grr.SRTariffType, reg.ChargeClassID, reg.ChargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType) ??
                                    Tariff.GetItemTariff(transactionDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType)) ??
                                   (Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, reg.ChargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType) ??
                                    Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType));

                row["BasicPrice"] = basic == null ? 0 : basic.Price;

                var coveredClassId = grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf ? reg.ChargeClassID : reg.CoverageClassID;

                ItemTariff cover = (Tariff.GetItemTariff(transactionDate, grr.SRTariffType, coveredClassId, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType) ??
                                    Tariff.GetItemTariff(transactionDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType)) ??
                                   (Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, coveredClassId, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType) ??
                                    Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType));

                row["CoveragePrice"] = cover == null ? 0 : cover.Price;
            }

            tbl.AcceptChanges();

            var types = new GuarantorItemTypeRuleCollection();
            types.Query.Where(types.Query.GuarantorID == guarantorID);
            types.LoadAll();

            foreach (DataRow row in tbl.Rows)
            {
                var item = new Item();
                item.LoadByPrimaryKey(row["ItemID"].ToString());
                if (item.SRItemType == ItemType.Medical)
                {
                    row["IsInclude"] = grr.IsIncludeItemMedical ?? false;
                    row["IsGuarantor"] = grr.IsIncludeItemMedicalToGuarantor ?? false;
                }
                else if (item.SRItemType == ItemType.NonMedical || item.SRItemType == ItemType.Kitchen)
                {
                    row["IsInclude"] = grr.IsIncludeItemNonMedical ?? false;
                    row["IsGuarantor"] = grr.IsIncludeItemNonMedicalToGuarantor ?? false;
                }
                else
                {
                    row["IsInclude"] = true;
                    row["IsGuarantor"] = types.AsEnumerable().Any() ? types.SingleOrDefault(t => t.SRItemType == item.SRItemType).IsToGuarantor :
                                                                      true;
                }
            }

            var grrItemColl = new GuarantorItemRuleCollection();
            grrItemColl.Query.Where(grrItemColl.Query.GuarantorID == guarantorID);
            grrItemColl.LoadAll();

            foreach (var grrItem in grrItemColl)
            {
                foreach (var row in tbl.Rows.Cast<DataRow>().Where(row => row["ItemID"].ToString().Equals(grrItem.ItemID)))
                {
                    row["SRGuarantorRuleType"] = grrItem.SRGuarantorRuleType;
                    row["IsByTariffComponent"] = grrItem.IsByTariffComponent ?? false;

                    if (grrItem.IsByTariffComponent ?? false)
                    {
                        var comps = Helper.Tariff.GetItemTariffComponentCollection(transactionDate, grr.SRTariffType, reg.ChargeClassID, grrItem.ItemID);
                        if (comps == null || comps.Count() == 0)
                        {
                            comps = Helper.Tariff.GetItemTariffComponentCollection(transactionDate, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, grrItem.ItemID);
                            if (comps == null || comps.Count() == 0)
                                comps = Helper.Tariff.GetItemTariffComponentCollection(transactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, grrItem.ItemID);
                        }
                        var girtc = new GuarantorItemRuleTariffComponentCollection();
                        girtc.Query.Where(girtc.Query.GuarantorID == guarantorID, girtc.Query.ItemID == grrItem.ItemID, girtc.Query.TariffComponentID.In(comps.Select(c => c.TariffComponentID)));
                        if (girtc.Query.Load())
                        {
                            //format : 
                            //BasicPrice1/CoveragePrice1/TariffComponentID1/AmountValue1;
                            //BasicPrice.../CoveragePrice.../TariffComponentID.../AmountValue...;
                            //...
                            var str = string.Empty;
                            foreach (var gir in girtc)
                            {
                                var tblBasic = Helper.Tariff.GetItemTariffComponent(transactionDate, grr.SRTariffType, reg.ChargeClassID, gir.TariffComponentID, grrItem.ItemID);
                                decimal tcBasic = 0;
                                if (tblBasic == null || tblBasic.Rows.Count == 0) tcBasic = 0;
                                else tcBasic = Convert.ToDecimal(tblBasic.Rows[0]["Price"]);

                                decimal tcCoverage = 0;
                                var tblCoverage = Helper.Tariff.GetItemTariffComponent(transactionDate, grr.SRTariffType, grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf ? reg.ChargeClassID : reg.CoverageClassID, gir.TariffComponentID, grrItem.ItemID);
                                if (tblCoverage == null || tblCoverage.Rows.Count == 0) tcBasic = 0;
                                else tcCoverage = Convert.ToDecimal(tblBasic.Rows[0]["Price"]);

                                str += string.Format("{0}/{1}/{2}/", tcBasic.ToString(), tcCoverage.ToString(), gir.TariffComponentID);
                                switch (regType)
                                {
                                    case "IPR":
                                        str += (gir.AmountValue ?? 0).ToString() + ";";
                                        break;
                                    case "OPR":
                                        str += (gir.OutpatientAmountValue ?? 0).ToString() + ";";
                                        break;
                                    case "EMR":
                                        str += (gir.EmergencyAmountValue ?? 0).ToString() + ";";
                                        break;
                                }
                            }
                            str = str.Remove(str.Length - 1, 1);
                            row["TariffComponentValue"] = str;
                        }
                        else
                        {
                            switch (regType)
                            {
                                case "IPR":
                                    row["AmountValue"] = grrItem.AmountValue ?? 0;
                                    break;
                                case "OPR":
                                    row["AmountValue"] = grrItem.OutpatientAmountValue ?? 0;
                                    break;
                                case "EMR":
                                    row["AmountValue"] = grrItem.EmergencyAmountValue ?? 0;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (regType)
                        {
                            case "IPR":
                                row["AmountValue"] = grrItem.AmountValue ?? 0;
                                break;
                            case "OPR":
                                row["AmountValue"] = grrItem.OutpatientAmountValue ?? 0;
                                break;
                            case "EMR":
                                row["AmountValue"] = grrItem.EmergencyAmountValue ?? 0;
                                break;
                        }
                    }

                    row["IsValueInPercent"] = grrItem.IsValueInPercent ?? false;
                    row["IsInclude"] = grrItem.IsInclude ?? false;
                    row["IsGuarantor"] = grrItem.IsToGuarantor ?? false;
                    break;
                }
            }

            if (isPrescription)
            {
                var x = new GuarantorItemPrescriptionByTherapyRuleQuery("x");
                var y = new ItemProductMedicQuery("y");
                x.InnerJoin(y).On(y.SRTherapyGroup == x.SRTherapyGroup);
                x.Where(x.GuarantorID == guarantorID);
                x.Select(y.ItemID, x.SRGuarantorRuleType, x.AmountValue, x.OutpatientAmountValue, x.EmergencyAmountValue, x.IsValueInPercent, x.IsInclude, x.IsToGuarantor);
                DataTable xdtb = x.LoadDataTable();
                if (xdtb.Rows.Count > 0)
                {
                    foreach (DataRow xrow in xdtb.Rows)
                    {
                        foreach (var row in tbl.Rows.Cast<DataRow>().Where(row => row["ItemID"].ToString().Equals(xrow["ItemID"].ToString())))
                        {
                            row["SRGuarantorRuleType"] = xrow["SRGuarantorRuleType"].ToString();

                            switch (regType)
                            {
                                case "IPR":
                                    row["AmountValue"] = Convert.ToDecimal(xrow["AmountValue"]);
                                    break;
                                case "OPR":
                                    row["AmountValue"] = Convert.ToDecimal(xrow["OutpatientAmountValue"]);
                                    break;
                                case "EMR":
                                    row["AmountValue"] = Convert.ToDecimal(xrow["EmergencyAmountValue"]);
                                    break;
                            }

                            row["IsValueInPercent"] = Convert.ToBoolean(xrow["IsValueInPercent"]);
                            row["IsInclude"] = Convert.ToBoolean(xrow["IsInclude"]);
                            row["IsGuarantor"] = Convert.ToBoolean(xrow["IsToGuarantor"]);
                            break;
                        }
                    }
                }

                //var grrItemPrescByTherapyColl = new GuarantorItemPrescriptionByTherapyRuleCollection();
                //grrItemPrescByTherapyColl.Query.Where(grrItemPrescByTherapyColl.Query.GuarantorID == guarantorID);
                //grrItemPrescByTherapyColl.LoadAll();

                //if (grrItemPrescByTherapyColl.Count > 0)
                //{
                //    foreach (var row in tbl.Rows.Cast<DataRow>())
                //    {
                //        foreach (var x in grrItemPrescByTherapyColl)
                //        {
                //            if (x.SRTherapyGroup == row["SRTherapyGroup"].ToString())
                //            {
                //                row["SRGuarantorRuleType"] = x.SRGuarantorRuleType;

                //                switch (regType)
                //                {
                //                    case "IPR":
                //                        row["AmountValue"] = x.AmountValue ?? 0;
                //                        break;
                //                    case "OPR":
                //                        row["AmountValue"] = x.OutpatientAmountValue ?? 0;
                //                        break;
                //                    case "EMR":
                //                        row["AmountValue"] = x.EmergencyAmountValue ?? 0;
                //                        break;
                //                }

                //                row["IsValueInPercent"] = x.IsValueInPercent ?? false;
                //                row["IsInclude"] = x.IsInclude ?? false;
                //                row["IsGuarantor"] = x.IsToGuarantor ?? false;
                //                break;
                //            }
                //        }
                //    }
                //}

                var grrItemPrescColl = new GuarantorItemPrescriptionRuleCollection();
                grrItemPrescColl.Query.Where(grrItemPrescColl.Query.GuarantorID == guarantorID);
                grrItemPrescColl.LoadAll();

                foreach (var grrItemPresc in grrItemPrescColl)
                {
                    foreach (var row in tbl.Rows.Cast<DataRow>().Where(row => row["ItemID"].ToString().Equals(grrItemPresc.ItemID)))
                    {
                        row["SRGuarantorRuleType"] = grrItemPresc.SRGuarantorRuleType;

                        switch (regType)
                        {
                            case "IPR":
                                row["AmountValue"] = grrItemPresc.AmountValue ?? 0;
                                break;
                            case "OPR":
                                row["AmountValue"] = grrItemPresc.OutpatientAmountValue ?? 0;
                                break;
                            case "EMR":
                                row["AmountValue"] = grrItemPresc.EmergencyAmountValue ?? 0;
                                break;
                        }

                        row["IsValueInPercent"] = grrItemPresc.IsValueInPercent ?? false;
                        row["IsInclude"] = grrItemPresc.IsInclude ?? false;
                        row["IsGuarantor"] = grrItemPresc.IsToGuarantor ?? false;
                        break;
                    }
                }
            }

            var regItemColl = new RegistrationItemRuleCollection();
            regItemColl.Query.Where(regItemColl.Query.RegistrationNo == registrationNo);
            regItemColl.LoadAll();

            foreach (var regItem in regItemColl)
            {
                foreach (var row in tbl.Rows.Cast<DataRow>().Where(row => row["ItemID"].ToString().Equals(regItem.ItemID)))
                {
                    row["SRGuarantorRuleType"] = regItem.SRGuarantorRuleType;

                    switch (regType)
                    {
                        case "IPR":
                            row["AmountValue"] = regItem.AmountValue ?? 0;
                            break;
                        case "OPR":
                            row["AmountValue"] = regItem.OutpatientAmountValue ?? 0;
                            break;
                        case "EMR":
                            row["AmountValue"] = regItem.EmergencyAmountValue ?? 0;
                            break;
                    }

                    row["IsValueInPercent"] = regItem.IsValueInPercent ?? false;
                    row["IsInclude"] = regItem.IsInclude ?? false;
                    row["IsGuarantor"] = regItem.IsToGuarantor ?? false;
                    break;
                }
            }

            return tbl;
        }

        //public static DataTable GetCoveredItems(string registrationNo, string guarantorID, string coverageClassID, string[] itemID, DateTime transactionDate, bool isPrescription)
        //{
        //    var reg = new Registration();
        //    reg.LoadByPrimaryKey(registrationNo);

        //    var regType = reg.SRRegistrationType;

        //    var grr = new Guarantor();
        //    grr.LoadByPrimaryKey(guarantorID);

        //    //if (grr.TariffCalculationMethod == 1) transactionDate = reg.RegistrationDate.Value.Date;

        //    if (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf)
        //    {
        //        var pat = new Patient();
        //        pat.LoadByPrimaryKey(reg.PatientID);
        //        if (!string.IsNullOrEmpty(pat.str.MemberID)) guarantorID = pat.str.MemberID;
        //    }

        //    if (grr.IsItemRuleUsingDefaultAmountValue ?? true) regType = "IPR";
        //    else
        //    {
        //        if (regType != "EMR")
        //        {
        //            var merge = new BusinessObject.MergeBilling();
        //            if (merge.LoadByPrimaryKey(reg.RegistrationNo) && !string.IsNullOrEmpty(merge.FromRegistrationNo))
        //            {
        //                var r = new Registration();
        //                r.LoadByPrimaryKey(merge.FromRegistrationNo);
        //                regType = r.SRRegistrationType;
        //            }
        //        }
        //        if (regType == "MCU") regType = "OPR";
        //    }

        //    decimal amountValue;
        //    if (regType == "IPR") amountValue = grr.AmountValue ?? 0;
        //    else if (regType == "EMR") amountValue = grr.EmergencyAmountValue ?? 0;
        //    else amountValue = grr.OutpatientAmountValue ?? 0;

        //    var i = new ItemQuery();

        //    if (reg.SRBussinesMethod == AppSession.Parameter.BusinessMethodFlavon && (!reg.IsGlobalPlafond ?? false))
        //        i.Select(string.Format("<CAST('{0}' AS NUMERIC(18, 2)) AS [AmountValue]>", reg.PlavonAmount == 0 ? amountValue : reg.PlavonAmount));
        //    else i.Select(string.Format("<CAST('{0}' AS NUMERIC(18, 2)) AS [AmountValue]>", amountValue));

        //    i.Select
        //        (
        //            i.ItemID,
        //            string.Format("<'{0}' AS [SRGuarantorRuleType]>", grr.SRGuarantorRuleType),
        //            string.Format("<CAST('{0}' AS BIT) AS [IsValueInPercent]>", (grr.IsValueInPercent ?? false ? 1 : 0)),
        //            "<CAST('1' AS BIT) AS [IsInclude]>",
        //            string.Format("<CAST('{0}' AS BIT) AS [IsGuarantor]>", (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf ? 0 : 1)),
        //            "<CAST('0' AS NUMERIC(18, 2)) AS [BasicPrice]>",
        //            "<CAST('0' AS NUMERIC(18, 2)) AS [CoveragePrice]>",
        //            "<CAST('0' AS BIT) AS [IsByTariffComponent]>",
        //            "<'' AS [TariffComponentValue]>"
        //        );
        //    if (itemID.Length == 0) i.Where(i.ItemID.In(""));
        //    else i.Where(i.ItemID.In(itemID));
        //    i.OrderBy(i.ItemID.Ascending);

        //    var tbl = i.LoadDataTable();

        //    foreach (DataRow row in tbl.Rows)
        //    {
        //        var basic = (Tariff.GetItemTariff(transactionDate, grr.SRTariffType, reg.ChargeClassID, reg.ChargeClassID, row["ItemID"].ToString(), AppSession.Parameter.SelfGuarantor, isPrescription, reg.SRRegistrationType) ??
        //                     Tariff.GetItemTariff(transactionDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, row["ItemID"].ToString(), AppSession.Parameter.SelfGuarantor, isPrescription, reg.SRRegistrationType) ??
        //                    (Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, reg.ChargeClassID, row["ItemID"].ToString(), AppSession.Parameter.SelfGuarantor, isPrescription, reg.SRRegistrationType) ??
        //                     Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, row["ItemID"].ToString(), AppSession.Parameter.SelfGuarantor, isPrescription, reg.SRRegistrationType)));

        //        row["BasicPrice"] = basic == null ? 0 : basic.Price;

        //        var coveredClassId = grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf ? reg.ChargeClassID : coverageClassID;

        //        var cover =
        //            (Tariff.GetItemTariff(transactionDate, grr.SRTariffType, coveredClassId, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType) ??
        //             Tariff.GetItemTariff(transactionDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType)) ??
        //            (Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, coveredClassId, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType) ??
        //             Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType));

        //        row["CoveragePrice"] = cover == null ? 0 : cover.Price;
        //    }

        //    tbl.AcceptChanges();

        //    var types = new GuarantorItemTypeRuleCollection();
        //    types.Query.Where(types.Query.GuarantorID == guarantorID);
        //    types.LoadAll();

        //    foreach (DataRow row in tbl.Rows)
        //    {
        //        var item = new Item();
        //        item.LoadByPrimaryKey(row["ItemID"].ToString());
        //        if (item.SRItemType == ItemType.Medical)
        //        {
        //            row["IsInclude"] = grr.IsIncludeItemMedical ?? false;
        //            row["IsGuarantor"] = grr.IsIncludeItemMedicalToGuarantor ?? false;
        //        }
        //        else if (item.SRItemType == ItemType.NonMedical || item.SRItemType == ItemType.Kitchen)
        //        {
        //            row["IsInclude"] = grr.IsIncludeItemNonMedical ?? false;
        //            row["IsGuarantor"] = grr.IsIncludeItemNonMedicalToGuarantor ?? false;
        //        }
        //        else
        //        {
        //            row["IsInclude"] = true;
        //            row["IsGuarantor"] = types.AsEnumerable().Any() ? types.SingleOrDefault(t => t.SRItemType == item.SRItemType).IsToGuarantor : true;
        //        }
        //    }

        //    var grrItemColl = new GuarantorItemRuleCollection();
        //    grrItemColl.Query.Where(grrItemColl.Query.GuarantorID == guarantorID);
        //    grrItemColl.LoadAll();

        //    foreach (var grrItem in grrItemColl)
        //    {
        //        foreach (var row in tbl.Rows.Cast<DataRow>().Where(row => row["ItemID"].ToString().Equals(grrItem.ItemID)))
        //        {
        //            row["SRGuarantorRuleType"] = grrItem.SRGuarantorRuleType;
        //            row["IsByTariffComponent"] = grrItem.IsByTariffComponent ?? false;

        //            if (grrItem.IsByTariffComponent ?? false)
        //            {
        //                var comps = Helper.Tariff.GetItemTariffComponentCollection(transactionDate, grr.SRTariffType, reg.ChargeClassID, grrItem.ItemID);
        //                if (comps == null || comps.Count() == 0)
        //                {
        //                    comps = Helper.Tariff.GetItemTariffComponentCollection(transactionDate, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, grrItem.ItemID);
        //                    if (comps == null || comps.Count() == 0)
        //                        comps = Helper.Tariff.GetItemTariffComponentCollection(transactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, grrItem.ItemID);
        //                }
        //                var girtc = new GuarantorItemRuleTariffComponentCollection();
        //                girtc.Query.Where(girtc.Query.GuarantorID == guarantorID, girtc.Query.ItemID == grrItem.ItemID, girtc.Query.TariffComponentID.In(comps.Select(c => c.TariffComponentID)));
        //                if (girtc.Query.Load())
        //                {
        //                    //format : 
        //                    //BasicPrice1/CoveragePrice1/TariffComponentID1/AmountValue1;
        //                    //BasicPrice.../CoveragePrice.../TariffComponentID.../AmountValue...;
        //                    //...
        //                    var str = string.Empty;
        //                    foreach (var gir in girtc)
        //                    {
        //                        var tblBasic = Helper.Tariff.GetItemTariffComponent(transactionDate, grr.SRTariffType, reg.ChargeClassID, gir.TariffComponentID, grrItem.ItemID);
        //                        decimal tcBasic = 0;
        //                        if (tblBasic == null || tblBasic.Rows.Count == 0) tcBasic = 0;
        //                        else tcBasic = Convert.ToDecimal(tblBasic.Rows[0]["Price"]);

        //                        decimal tcCoverage = 0;
        //                        var tblCoverage = Helper.Tariff.GetItemTariffComponent(transactionDate, grr.SRTariffType, grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf ? reg.ChargeClassID : reg.CoverageClassID, gir.TariffComponentID, grrItem.ItemID);
        //                        if (tblCoverage == null || tblCoverage.Rows.Count == 0) tcBasic = 0;
        //                        else tcCoverage = Convert.ToDecimal(tblBasic.Rows[0]["Price"]);

        //                        str += string.Format("{0}/{1}/{2}/", tcBasic.ToString(), tcCoverage.ToString(), gir.TariffComponentID);
        //                        switch (regType)
        //                        {
        //                            case "IPR":
        //                                str += (gir.AmountValue ?? 0).ToString() + ";";
        //                                break;
        //                            case "OPR":
        //                                str += (gir.OutpatientAmountValue ?? 0).ToString() + ";";
        //                                break;
        //                            case "EMR":
        //                                str += (gir.EmergencyAmountValue ?? 0).ToString() + ";";
        //                                break;
        //                        }
        //                    }
        //                    str = str.Remove(str.Length - 1, 1);
        //                    row["TariffComponentValue"] = str;
        //                }
        //                else
        //                {
        //                    switch (regType)
        //                    {
        //                        case "IPR":
        //                            row["AmountValue"] = grrItem.AmountValue ?? 0;
        //                            break;
        //                        case "OPR":
        //                            row["AmountValue"] = grrItem.OutpatientAmountValue ?? 0;
        //                            break;
        //                        case "EMR":
        //                            row["AmountValue"] = grrItem.EmergencyAmountValue ?? 0;
        //                            break;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                switch (regType)
        //                {
        //                    case "IPR":
        //                        row["AmountValue"] = grrItem.AmountValue ?? 0;
        //                        break;
        //                    case "OPR":
        //                        row["AmountValue"] = grrItem.OutpatientAmountValue ?? 0;
        //                        break;
        //                    case "EMR":
        //                        row["AmountValue"] = grrItem.EmergencyAmountValue ?? 0;
        //                        break;
        //                }
        //            }

        //            row["IsValueInPercent"] = grrItem.IsValueInPercent ?? false;
        //            row["IsInclude"] = grrItem.IsInclude ?? false;
        //            row["IsGuarantor"] = grrItem.IsToGuarantor ?? false;
        //            break;
        //        }
        //    }

        //    if (isPrescription)
        //    {
        //        var grrItemPrescColl = new GuarantorItemPrescriptionRuleCollection();
        //        grrItemPrescColl.Query.Where(grrItemPrescColl.Query.GuarantorID == guarantorID);
        //        grrItemPrescColl.LoadAll();

        //        foreach (var grrItemPresc in grrItemPrescColl)
        //        {
        //            foreach (var row in tbl.Rows.Cast<DataRow>().Where(row => row["ItemID"].ToString().Equals(grrItemPresc.ItemID)))
        //            {
        //                row["SRGuarantorRuleType"] = grrItemPresc.SRGuarantorRuleType;

        //                switch (regType)
        //                {
        //                    case "IPR":
        //                        row["AmountValue"] = grrItemPresc.AmountValue ?? 0;
        //                        break;
        //                    case "OPR":
        //                        row["AmountValue"] = grrItemPresc.OutpatientAmountValue ?? 0;
        //                        break;
        //                    case "EMR":
        //                        row["AmountValue"] = grrItemPresc.EmergencyAmountValue ?? 0;
        //                        break;
        //                }

        //                row["IsValueInPercent"] = grrItemPresc.IsValueInPercent ?? false;
        //                row["IsInclude"] = grrItemPresc.IsInclude ?? false;
        //                row["IsGuarantor"] = grrItemPresc.IsToGuarantor ?? false;
        //                break;
        //            }
        //        }
        //    }

        //    var regItemColl = new RegistrationItemRuleCollection();
        //    regItemColl.Query.Where(regItemColl.Query.RegistrationNo == registrationNo);
        //    regItemColl.LoadAll();

        //    foreach (var regItem in regItemColl)
        //    {
        //        foreach (var row in tbl.Rows.Cast<DataRow>().Where(row => row["ItemID"].ToString().Equals(regItem.ItemID)))
        //        {
        //            row["SRGuarantorRuleType"] = regItem.SRGuarantorRuleType;

        //            switch (regType)
        //            {
        //                case "IPR":
        //                    row["AmountValue"] = regItem.AmountValue ?? 0;
        //                    break;
        //                case "OPR":
        //                    row["AmountValue"] = regItem.OutpatientAmountValue ?? 0;
        //                    break;
        //                case "EMR":
        //                    row["AmountValue"] = regItem.EmergencyAmountValue ?? 0;
        //                    break;
        //            }

        //            row["IsValueInPercent"] = regItem.IsValueInPercent ?? false;
        //            row["IsInclude"] = regItem.IsInclude ?? false;
        //            row["IsGuarantor"] = regItem.IsToGuarantor ?? false;
        //            break;
        //        }
        //    }

        //    return tbl;
        //}

        public static DataTable GetCoveredItems(string registrationNo, string guarantorID, string chargeClassID, string coverageClassID, string[] itemID, DateTime transactionDate, bool isPrescription)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            var regType = reg.SRRegistrationType;

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(guarantorID);

            //if (grr.TariffCalculationMethod == 1) transactionDate = reg.RegistrationDate.Value.Date;

            if (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf)
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                if (!string.IsNullOrEmpty(pat.str.MemberID)) guarantorID = pat.str.MemberID;
            }

            if (grr.IsItemRuleUsingDefaultAmountValue ?? true) regType = "IPR";
            else
            {
                if (regType != "EMR")
                {
                    var merge = new BusinessObject.MergeBilling();
                    if (merge.LoadByPrimaryKey(reg.RegistrationNo) && !string.IsNullOrEmpty(merge.FromRegistrationNo))
                    {
                        var r = new Registration();
                        r.LoadByPrimaryKey(merge.FromRegistrationNo);
                        regType = r.SRRegistrationType;
                    }
                }
                if (regType == "MCU") regType = "OPR";
            }

            decimal amountValue;
            if (regType == "IPR") amountValue = grr.AmountValue ?? 0;
            else if (regType == "EMR") amountValue = grr.EmergencyAmountValue ?? 0;
            else amountValue = grr.OutpatientAmountValue ?? 0;

            var i = new ItemQuery();

            if (reg.SRBussinesMethod == AppSession.Parameter.BusinessMethodFlavon && (!reg.IsGlobalPlafond ?? false))
                i.Select(string.Format("<CAST('{0}' AS NUMERIC(18, 2)) AS [AmountValue]>", reg.PlavonAmount == 0 ? amountValue : reg.PlavonAmount));
            else i.Select(string.Format("<CAST('{0}' AS NUMERIC(18, 2)) AS [AmountValue]>", amountValue));

            i.Select
                (
                    i.ItemID,
                    string.Format("<'{0}' AS [SRGuarantorRuleType]>", grr.SRGuarantorRuleType),
                    string.Format("<CAST('{0}' AS BIT) AS [IsValueInPercent]>", (grr.IsValueInPercent ?? false ? 1 : 0)),
                    "<CAST('1' AS BIT) AS [IsInclude]>",
                    string.Format("<CAST('{0}' AS BIT) AS [IsGuarantor]>", (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf ? 0 : 1)),
                    "<CAST('0' AS NUMERIC(18, 2)) AS [BasicPrice]>",
                    "<CAST('0' AS NUMERIC(18, 2)) AS [CoveragePrice]>",
                    "<CAST('0' AS BIT) AS [IsByTariffComponent]>",
                    "<'' AS [TariffComponentValue]>"
                );
            i.Where(i.ItemID.In(itemID.Length == 0 ? new string[1] { string.Empty } : itemID));
            i.OrderBy(i.ItemID.Ascending);

            var tbl = i.LoadDataTable();

            foreach (DataRow row in tbl.Rows)
            {
                var basic = (Tariff.GetItemTariff(transactionDate, grr.SRTariffType, chargeClassID, chargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType) ??
                             Tariff.GetItemTariff(transactionDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, chargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType)) ??
                            (Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, chargeClassID, chargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType) ??
                             Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, chargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType));

                row["BasicPrice"] = basic == null ? 0 : basic.Price;

                var coveredClassId = grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf ? chargeClassID : coverageClassID;

                var cover = (Tariff.GetItemTariff(transactionDate, grr.SRTariffType, coveredClassId, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType) ??
                             Tariff.GetItemTariff(transactionDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType)) ??
                            (Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, coveredClassId, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType) ??
                             Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType));

                row["CoveragePrice"] = cover == null ? 0 : cover.Price;
            }

            tbl.AcceptChanges();

            var types = new GuarantorItemTypeRuleCollection();
            types.Query.Where(types.Query.GuarantorID == guarantorID);
            types.LoadAll();

            foreach (DataRow row in tbl.Rows)
            {
                var item = new Item();
                item.LoadByPrimaryKey(row["ItemID"].ToString());
                if (item.SRItemType == ItemType.Medical)
                {
                    row["IsInclude"] = grr.IsIncludeItemMedical ?? false;
                    row["IsGuarantor"] = grr.IsIncludeItemMedicalToGuarantor ?? false;
                }
                else if (item.SRItemType == ItemType.NonMedical || item.SRItemType == ItemType.Kitchen)
                {
                    row["IsInclude"] = grr.IsIncludeItemNonMedical ?? false;
                    row["IsGuarantor"] = grr.IsIncludeItemNonMedicalToGuarantor ?? false;
                }
                else
                {
                    row["IsInclude"] = true;
                    row["IsGuarantor"] = types.AsEnumerable().Any() ? types.SingleOrDefault(t => t.SRItemType == item.SRItemType).IsToGuarantor :
                                                                      true;
                }
            }

            var grrItemColl = new GuarantorItemRuleCollection();
            grrItemColl.Query.Where(grrItemColl.Query.GuarantorID == guarantorID);
            grrItemColl.LoadAll();

            foreach (var grrItem in grrItemColl)
            {
                foreach (var row in tbl.Rows.Cast<DataRow>().Where(row => row["ItemID"].ToString().Equals(grrItem.ItemID)))
                {
                    row["SRGuarantorRuleType"] = grrItem.SRGuarantorRuleType;
                    row["IsByTariffComponent"] = grrItem.IsByTariffComponent ?? false;

                    if (grrItem.IsByTariffComponent ?? false)
                    {
                        var comps = Helper.Tariff.GetItemTariffComponentCollection(transactionDate, grr.SRTariffType, reg.ChargeClassID, grrItem.ItemID);
                        if (comps == null || comps.Count() == 0)
                        {
                            comps = Helper.Tariff.GetItemTariffComponentCollection(transactionDate, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, grrItem.ItemID);
                            if (comps == null || comps.Count() == 0)
                                comps = Helper.Tariff.GetItemTariffComponentCollection(transactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, grrItem.ItemID);
                        }
                        var girtc = new GuarantorItemRuleTariffComponentCollection();
                        girtc.Query.Where(girtc.Query.GuarantorID == guarantorID, girtc.Query.ItemID == grrItem.ItemID, girtc.Query.TariffComponentID.In(comps.Select(c => c.TariffComponentID)));
                        if (girtc.Query.Load())
                        {
                            //format : 
                            //BasicPrice1/CoveragePrice1/TariffComponentID1/AmountValue1;
                            //BasicPrice.../CoveragePrice.../TariffComponentID.../AmountValue...;
                            //...
                            var str = string.Empty;
                            foreach (var gir in girtc)
                            {
                                var tblBasic = Helper.Tariff.GetItemTariffComponent(transactionDate, grr.SRTariffType, chargeClassID, gir.TariffComponentID, grrItem.ItemID);
                                decimal tcBasic = 0;
                                if (tblBasic == null || tblBasic.Rows.Count == 0) tcBasic = 0;
                                else tcBasic = Convert.ToDecimal(tblBasic.Rows[0]["Price"]);

                                decimal tcCoverage = 0;
                                var tblCoverage = Helper.Tariff.GetItemTariffComponent(transactionDate, grr.SRTariffType, grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf ? reg.ChargeClassID : reg.CoverageClassID, gir.TariffComponentID, grrItem.ItemID);
                                if (tblCoverage == null || tblCoverage.Rows.Count == 0) tcBasic = 0;
                                else tcCoverage = Convert.ToDecimal(tblBasic.Rows[0]["Price"]);

                                str += string.Format("{0}/{1}/{2}/", tcBasic.ToString(), tcCoverage.ToString(), gir.TariffComponentID);
                                switch (regType)
                                {
                                    case "IPR":
                                        str += (gir.AmountValue ?? 0).ToString() + ";";
                                        break;
                                    case "OPR":
                                        str += (gir.OutpatientAmountValue ?? 0).ToString() + ";";
                                        break;
                                    case "EMR":
                                        str += (gir.EmergencyAmountValue ?? 0).ToString() + ";";
                                        break;
                                }
                            }
                            str = str.Remove(str.Length - 1, 1);
                            row["TariffComponentValue"] = str;
                        }
                        else
                        {
                            switch (regType)
                            {
                                case "IPR":
                                    row["AmountValue"] = grrItem.AmountValue ?? 0;
                                    break;
                                case "OPR":
                                    row["AmountValue"] = grrItem.OutpatientAmountValue ?? 0;
                                    break;
                                case "EMR":
                                    row["AmountValue"] = grrItem.EmergencyAmountValue ?? 0;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (regType)
                        {
                            case "IPR":
                                row["AmountValue"] = grrItem.AmountValue ?? 0;
                                break;
                            case "OPR":
                                row["AmountValue"] = grrItem.OutpatientAmountValue ?? 0;
                                break;
                            case "EMR":
                                row["AmountValue"] = grrItem.EmergencyAmountValue ?? 0;
                                break;
                        }
                    }

                    row["IsValueInPercent"] = grrItem.IsValueInPercent ?? false;
                    row["IsInclude"] = grrItem.IsInclude ?? false;
                    row["IsGuarantor"] = grrItem.IsToGuarantor ?? false;
                    break;
                }
            }

            if (isPrescription)
            {
                var grrItemPrescColl = new GuarantorItemPrescriptionRuleCollection();
                grrItemPrescColl.Query.Where(grrItemPrescColl.Query.GuarantorID == guarantorID);
                grrItemPrescColl.LoadAll();

                foreach (var grrPrescItem in grrItemPrescColl)
                {
                    foreach (var row in tbl.Rows.Cast<DataRow>().Where(row => row["ItemID"].ToString().Equals(grrPrescItem.ItemID)))
                    {
                        row["SRGuarantorRuleType"] = grrPrescItem.SRGuarantorRuleType;

                        switch (regType)
                        {
                            case "IPR":
                                row["AmountValue"] = grrPrescItem.AmountValue ?? 0;
                                break;
                            case "OPR":
                                row["AmountValue"] = grrPrescItem.OutpatientAmountValue ?? 0;
                                break;
                            case "EMR":
                                row["AmountValue"] = grrPrescItem.EmergencyAmountValue ?? 0;
                                break;

                        }

                        row["IsValueInPercent"] = grrPrescItem.IsValueInPercent ?? false;
                        row["IsInclude"] = grrPrescItem.IsInclude ?? false;
                        row["IsGuarantor"] = grrPrescItem.IsToGuarantor ?? false;
                        break;
                    }
                }
            }

            var regItemColl = new RegistrationItemRuleCollection();
            regItemColl.Query.Where(regItemColl.Query.RegistrationNo == registrationNo);
            regItemColl.LoadAll();

            foreach (var regItem in regItemColl)
            {
                foreach (var row in tbl.Rows.Cast<DataRow>().Where(row => row["ItemID"].ToString().Equals(regItem.ItemID)))
                {
                    row["SRGuarantorRuleType"] = regItem.SRGuarantorRuleType;

                    switch (regType)
                    {
                        case "IPR":
                            row["AmountValue"] = regItem.AmountValue ?? 0;
                            break;
                        case "OPR":
                            row["AmountValue"] = regItem.OutpatientAmountValue ?? 0;
                            break;
                        case "EMR":
                            row["AmountValue"] = regItem.EmergencyAmountValue ?? 0;
                            break;

                    }

                    row["IsValueInPercent"] = regItem.IsValueInPercent ?? false;
                    row["IsInclude"] = regItem.IsInclude ?? false;
                    row["IsGuarantor"] = regItem.IsToGuarantor ?? false;
                    break;
                }
            }

            return tbl;
        }

        public static DataTable GetCoveredItems(string registrationNo, string guarantorID, string coverageClassID, string itemID, DateTime transactionDate, bool isPrescription, decimal? amountVariable = 0)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            var regType = reg.SRRegistrationType;

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(guarantorID);

            //if (grr.TariffCalculationMethod == 1) transactionDate = reg.RegistrationDate.Value.Date;

            if (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf)
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                if (!string.IsNullOrEmpty(pat.str.MemberID)) guarantorID = pat.str.MemberID;
            }

            if (grr.IsItemRuleUsingDefaultAmountValue ?? true) regType = "IPR";
            else
            {
                if (regType != "EMR")
                {
                    var merge = new BusinessObject.MergeBilling();
                    if (merge.LoadByPrimaryKey(reg.RegistrationNo) && !string.IsNullOrEmpty(merge.FromRegistrationNo))
                    {
                        var r = new Registration();
                        r.LoadByPrimaryKey(merge.FromRegistrationNo);
                        regType = r.SRRegistrationType;
                    }
                }
                if (regType == "MCU") regType = "OPR";
            }

            decimal amountValue;
            if (regType == "IPR") amountValue = grr.AmountValue ?? 0;
            else if (regType == "EMR") amountValue = grr.EmergencyAmountValue ?? 0;
            else amountValue = grr.OutpatientAmountValue ?? 0;

            var i = new ItemQuery("a");
            var ipm = new ItemProductMedicQuery("b");

            if (reg.SRBussinesMethod == AppSession.Parameter.BusinessMethodFlavon && !(reg.IsGlobalPlafond ?? false))
                i.Select(string.Format("<CAST('{0}' AS NUMERIC(18, 2)) AS [AmountValue]>", reg.PlavonAmount == 0 ? amountValue : reg.PlavonAmount));
            else i.Select(string.Format("<CAST('{0}' AS NUMERIC(18, 2)) AS [AmountValue]>", amountValue));

            i.Select
                (
                    i.ItemID,
                    string.Format("<'{0}' AS [SRGuarantorRuleType]>", grr.SRGuarantorRuleType),
                    string.Format("<CAST('{0}' AS BIT) AS [IsValueInPercent]>", (grr.IsValueInPercent ?? false ? 1 : 0)),
                    "<CAST('1' AS BIT) AS [IsInclude]>",
                    string.Format("<CAST('{0}' AS BIT) AS [IsGuarantor]>", (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf ? 0 : 1)),
                    "<CAST('0' AS NUMERIC(18, 2)) AS [BasicPrice]>",
                    "<CAST('0' AS NUMERIC(18, 2)) AS [CoveragePrice]>",
                    "<CAST('0' AS BIT) AS [IsByTariffComponent]>",
                    "<'' AS [TariffComponentValue]>",
                    "<ISNULL(b.SRTherapyGroup, '') AS [SRTherapyGroup]>"
                );
            i.LeftJoin(ipm).On(ipm.ItemID == i.ItemID);
            i.Where(i.ItemID == itemID);
            i.OrderBy(i.ItemID.Ascending);

            var tbl = i.LoadDataTable();

            foreach (DataRow row in tbl.Rows)
            {
                if (amountVariable == 0)
                {
                    var basic = (Tariff.GetItemTariff(transactionDate, grr.SRTariffType, reg.ChargeClassID, reg.ChargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType) ??
                            Tariff.GetItemTariff(transactionDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType)) ??
                           (Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, reg.ChargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType) ??
                            Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType));

                    row["BasicPrice"] = basic == null ? 0 : basic.Price;
                }
                else
                    row["BasicPrice"] = amountVariable;

                var coveredClassId = grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf ? reg.ChargeClassID : reg.CoverageClassID;

                ItemTariff cover = (Tariff.GetItemTariff(transactionDate, grr.SRTariffType, coveredClassId, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType) ??
                                    Tariff.GetItemTariff(transactionDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType)) ??
                                   (Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, coveredClassId, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType) ??
                                    Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType));

                row["CoveragePrice"] = cover == null ? 0 : cover.Price;
            }

            tbl.AcceptChanges();

            var types = new GuarantorItemTypeRuleCollection();
            types.Query.Where(types.Query.GuarantorID == guarantorID);
            types.LoadAll();

            foreach (DataRow row in tbl.Rows)
            {
                var item = new Item();
                item.LoadByPrimaryKey(row["ItemID"].ToString());
                if (item.SRItemType == ItemType.Medical)
                {
                    row["IsInclude"] = grr.IsIncludeItemMedical ?? false;
                    row["IsGuarantor"] = grr.IsIncludeItemMedicalToGuarantor ?? false;
                }
                else if (item.SRItemType == ItemType.NonMedical || item.SRItemType == ItemType.Kitchen)
                {
                    row["IsInclude"] = grr.IsIncludeItemNonMedical ?? false;
                    row["IsGuarantor"] = grr.IsIncludeItemNonMedicalToGuarantor ?? false;
                }
                else
                {
                    row["IsInclude"] = true;
                    //row["IsGuarantor"] = types.SingleOrDefault(t => t.SRItemType == item.SRItemType).IsToGuarantor ?? true;
                    row["IsGuarantor"] = types.Where(t => t.SRItemType == item.SRItemType).Any() ? (types.SingleOrDefault(t => t.SRItemType == item.SRItemType).IsToGuarantor ?? true) : true;
                }
            }

            var grrItemColl = new GuarantorItemRuleCollection();
            grrItemColl.Query.Where(grrItemColl.Query.GuarantorID == guarantorID);
            grrItemColl.LoadAll();

            foreach (var grrItem in grrItemColl)
            {
                foreach (var row in tbl.Rows.Cast<DataRow>().Where(row => row["ItemID"].ToString().Equals(grrItem.ItemID)))
                {
                    row["SRGuarantorRuleType"] = grrItem.SRGuarantorRuleType;
                    row["IsByTariffComponent"] = grrItem.IsByTariffComponent ?? false;

                    if (grrItem.IsByTariffComponent ?? false)
                    {
                        var comps = Helper.Tariff.GetItemTariffComponentCollection(transactionDate, grr.SRTariffType, reg.ChargeClassID, grrItem.ItemID);
                        if (comps == null || comps.Count() == 0)
                        {
                            comps = Helper.Tariff.GetItemTariffComponentCollection(transactionDate, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, grrItem.ItemID);
                            if (comps == null || comps.Count() == 0)
                                comps = Helper.Tariff.GetItemTariffComponentCollection(transactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, grrItem.ItemID);
                        }
                        var girtc = new GuarantorItemRuleTariffComponentCollection();
                        girtc.Query.Where(girtc.Query.GuarantorID == guarantorID, girtc.Query.ItemID == grrItem.ItemID, girtc.Query.TariffComponentID.In(comps.Select(c => c.TariffComponentID)));
                        if (girtc.Query.Load())
                        {
                            //format : 
                            //BasicPrice1/CoveragePrice1/TariffComponentID1/AmountValue1;
                            //BasicPrice.../CoveragePrice.../TariffComponentID.../AmountValue...;
                            //...
                            var str = string.Empty;
                            foreach (var gir in girtc)
                            {
                                var tblBasic = Helper.Tariff.GetItemTariffComponent(transactionDate, grr.SRTariffType, reg.ChargeClassID, gir.TariffComponentID, grrItem.ItemID);
                                decimal tcBasic = 0;
                                if (tblBasic == null || tblBasic.Rows.Count == 0) tcBasic = 0;
                                else tcBasic = Convert.ToDecimal(tblBasic.Rows[0]["Price"]);

                                decimal tcCoverage = 0;
                                var tblCoverage = Helper.Tariff.GetItemTariffComponent(transactionDate, grr.SRTariffType, grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf ? reg.ChargeClassID : reg.CoverageClassID, gir.TariffComponentID, grrItem.ItemID);
                                if (tblCoverage == null || tblCoverage.Rows.Count == 0) tcBasic = 0;
                                else tcCoverage = Convert.ToDecimal(tblBasic.Rows[0]["Price"]);

                                str += string.Format("{0}/{1}/{2}/", tcBasic.ToString(), tcCoverage.ToString(), gir.TariffComponentID);
                                switch (regType)
                                {
                                    case "IPR":
                                        str += (gir.AmountValue ?? 0).ToString() + ";";
                                        break;
                                    case "OPR":
                                        str += (gir.OutpatientAmountValue ?? 0).ToString() + ";";
                                        break;
                                    case "EMR":
                                        str += (gir.EmergencyAmountValue ?? 0).ToString() + ";";
                                        break;
                                }
                            }
                            str = str.Remove(str.Length - 1, 1);
                            row["TariffComponentValue"] = str;
                        }
                        else
                        {
                            switch (regType)
                            {
                                case "IPR":
                                    row["AmountValue"] = grrItem.AmountValue ?? 0;
                                    break;
                                case "OPR":
                                    row["AmountValue"] = grrItem.OutpatientAmountValue ?? 0;
                                    break;
                                case "EMR":
                                    row["AmountValue"] = grrItem.EmergencyAmountValue ?? 0;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (regType)
                        {
                            case "IPR":
                                row["AmountValue"] = grrItem.AmountValue ?? 0;
                                break;
                            case "OPR":
                                row["AmountValue"] = grrItem.OutpatientAmountValue ?? 0;
                                break;
                            case "EMR":
                                row["AmountValue"] = grrItem.EmergencyAmountValue ?? 0;
                                break;
                        }
                    }

                    row["IsValueInPercent"] = grrItem.IsValueInPercent ?? false;
                    row["IsInclude"] = grrItem.IsInclude ?? false;
                    row["IsGuarantor"] = grrItem.IsToGuarantor ?? false;
                    break;
                }
            }

            if (isPrescription)
            {
                var x = new GuarantorItemPrescriptionByTherapyRuleQuery("x");
                var y = new ItemProductMedicQuery("y");
                x.InnerJoin(y).On(y.SRTherapyGroup == x.SRTherapyGroup);
                x.Where(x.GuarantorID == guarantorID);
                x.Select(y.ItemID, x.SRGuarantorRuleType, x.AmountValue, x.OutpatientAmountValue, x.EmergencyAmountValue, x.IsValueInPercent, x.IsInclude, x.IsToGuarantor);
                DataTable xdtb = x.LoadDataTable();
                if (xdtb.Rows.Count > 0)
                {
                    foreach (DataRow xrow in xdtb.Rows)
                    {
                        foreach (var row in tbl.Rows.Cast<DataRow>().Where(row => row["ItemID"].ToString().Equals(xrow["ItemID"].ToString())))
                        {
                            row["SRGuarantorRuleType"] = xrow["SRGuarantorRuleType"].ToString();

                            switch (regType)
                            {
                                case "IPR":
                                    row["AmountValue"] = Convert.ToDecimal(xrow["AmountValue"]);
                                    break;
                                case "OPR":
                                    row["AmountValue"] = Convert.ToDecimal(xrow["OutpatientAmountValue"]);
                                    break;
                                case "EMR":
                                    row["AmountValue"] = Convert.ToDecimal(xrow["EmergencyAmountValue"]);
                                    break;
                            }

                            row["IsValueInPercent"] = Convert.ToBoolean(xrow["IsValueInPercent"]);
                            row["IsInclude"] = Convert.ToBoolean(xrow["IsInclude"]);
                            row["IsGuarantor"] = Convert.ToBoolean(xrow["IsToGuarantor"]);
                            break;
                        }
                    }
                }


                //var grrItemPrescByTherapyColl = new GuarantorItemPrescriptionByTherapyRuleCollection();
                //grrItemPrescByTherapyColl.Query.Where(grrItemPrescByTherapyColl.Query.GuarantorID == guarantorID);
                //grrItemPrescByTherapyColl.LoadAll();

                //foreach (var x in grrItemPrescByTherapyColl)
                //{
                //    foreach (var row in tbl.Rows.Cast<DataRow>().Where(row => row["SRTherapyGroup"].ToString().Equals(x.SRTherapyGroup)))
                //    {
                //        row["SRGuarantorRuleType"] = x.SRGuarantorRuleType;

                //        switch (regType)
                //        {
                //            case "IPR":
                //                row["AmountValue"] = x.AmountValue ?? 0;
                //                break;
                //            case "OPR":
                //                row["AmountValue"] = x.OutpatientAmountValue ?? 0;
                //                break;
                //            case "EMR":
                //                row["AmountValue"] = x.EmergencyAmountValue ?? 0;
                //                break;
                //        }

                //        row["IsValueInPercent"] = x.IsValueInPercent ?? false;
                //        row["IsInclude"] = x.IsInclude ?? false;
                //        row["IsGuarantor"] = x.IsToGuarantor ?? false;
                //        break;
                //    }
                //}

                var grrItemPrescColl = new GuarantorItemPrescriptionRuleCollection();
                grrItemPrescColl.Query.Where(grrItemPrescColl.Query.GuarantorID == guarantorID);
                grrItemPrescColl.LoadAll();

                foreach (GuarantorItemPrescriptionRule grrItemPresc in grrItemPrescColl)
                {
                    foreach (DataRow row in tbl.Rows.Cast<DataRow>().Where(row => row["ItemID"].ToString().Equals(grrItemPresc.ItemID)))
                    {
                        row["SRGuarantorRuleType"] = grrItemPresc.SRGuarantorRuleType;

                        switch (regType)
                        {
                            case "IPR":
                                row["AmountValue"] = grrItemPresc.AmountValue ?? 0;
                                break;
                            case "OPR":
                                row["AmountValue"] = grrItemPresc.OutpatientAmountValue ?? 0;
                                break;
                            case "EMR":
                                row["AmountValue"] = grrItemPresc.EmergencyAmountValue ?? 0;
                                break;
                        }

                        row["IsValueInPercent"] = grrItemPresc.IsValueInPercent ?? false;
                        row["IsInclude"] = grrItemPresc.IsInclude ?? false;
                        row["IsGuarantor"] = grrItemPresc.IsToGuarantor ?? false;
                        break;
                    }
                }
            }

            var regItemColl = new RegistrationItemRuleCollection();
            regItemColl.Query.Where(regItemColl.Query.RegistrationNo == registrationNo);
            regItemColl.LoadAll();

            foreach (var regItem in regItemColl)
            {
                foreach (var row in tbl.Rows.Cast<DataRow>().Where(row => row["ItemID"].ToString().Equals(regItem.ItemID)))
                {
                    row["SRGuarantorRuleType"] = regItem.SRGuarantorRuleType;

                    switch (regType)
                    {
                        case "IPR":
                            row["AmountValue"] = regItem.AmountValue ?? 0;
                            break;
                        case "OPR":
                            row["AmountValue"] = regItem.OutpatientAmountValue ?? 0;
                            break;
                        case "EMR":
                            row["AmountValue"] = regItem.EmergencyAmountValue ?? 0;
                            break;
                    }

                    row["IsValueInPercent"] = regItem.IsValueInPercent ?? false;
                    row["IsInclude"] = regItem.IsInclude ?? false;
                    row["IsGuarantor"] = regItem.IsToGuarantor ?? false;
                    break;
                }
            }

            return tbl;
        }

        public static DataTable GetCoveredItems(string registrationNo, string bussinesMethod, decimal plafondAmount, bool isGlobalPlafond, string chargeClassID,
            string coverageClassID, string guarantorID, string[] itemID, DateTime transactionDate, RegistrationItemRuleCollection itemRules, bool isPrescription)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            return GetCoveredItems(reg, bussinesMethod, plafondAmount, isGlobalPlafond, chargeClassID,
            coverageClassID, guarantorID, itemID, transactionDate, itemRules, isPrescription);
        }

        public static DataTable GetCoveredItems(Registration reg, string bussinesMethod, decimal plafondAmount, bool isGlobalPlafond, string chargeClassID,
            string coverageClassID, string guarantorID, string[] itemID, DateTime transactionDate, RegistrationItemRuleCollection itemRules, bool isPrescription)
        {
            var regType = reg.SRRegistrationType;

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(guarantorID);

            //if (grr.TariffCalculationMethod == 1) transactionDate = reg.RegistrationDate.Value.Date;

            if (grr.IsItemRuleUsingDefaultAmountValue ?? true) regType = "IPR";
            else
            {
                if (regType != "EMR")
                {
                    var merge = new BusinessObject.MergeBilling();
                    if (merge.LoadByPrimaryKey(reg.RegistrationNo) && !string.IsNullOrEmpty(merge.FromRegistrationNo))
                    {
                        var r = new Registration();
                        r.LoadByPrimaryKey(merge.FromRegistrationNo);
                        regType = r.SRRegistrationType;
                    }
                }
                if (regType == "MCU") regType = "OPR";
            }

            decimal amountValue;
            if (regType == "IPR") amountValue = grr.AmountValue ?? 0;
            else if (regType == "EMR") amountValue = grr.EmergencyAmountValue ?? 0;
            else amountValue = grr.OutpatientAmountValue ?? 0;

            var i = new ItemQuery();

            if (bussinesMethod == AppSession.Parameter.BusinessMethodFlavon && !isGlobalPlafond)
                i.Select(string.Format("<CAST('{0}' AS NUMERIC(18, 2)) AS [AmountValue]>", plafondAmount == 0 ? amountValue : plafondAmount));
            else i.Select(string.Format("<CAST('{0}' AS NUMERIC(18, 2)) AS [AmountValue]>", amountValue));

            i.Select
                (
                    i.ItemID,
                    string.Format("<'{0}' AS [SRGuarantorRuleType]>", grr.SRGuarantorRuleType),
                    string.Format("<CAST('{0}' AS BIT) AS [IsValueInPercent]>", (grr.IsValueInPercent ?? false ? 1 : 0)),
                    "<CAST('1' AS BIT) AS [IsInclude]>",
                    string.Format("<CAST('{0}' AS BIT) AS [IsGuarantor]>", (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf ? 0 : 1)),
                    "<CAST('0' AS NUMERIC(18, 2)) AS [BasicPrice]>",
                    "<CAST('0' AS NUMERIC(18, 2)) AS [CoveragePrice]>",
                    "<CAST('0' AS BIT) AS [IsByTariffComponent]>",
                    "<'' AS [TariffComponentValue]>"
                );
            i.Where(i.ItemID.In(itemID));
            i.OrderBy(i.ItemID.Ascending);

            var tbl = i.LoadDataTable();

            foreach (DataRow row in tbl.Rows)
            {
                var basic = (Tariff.GetItemTariff(transactionDate, grr.SRTariffType, chargeClassID, chargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType) ??
                             Tariff.GetItemTariff(transactionDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, chargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType)) ??
                            (Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, chargeClassID, chargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType) ??
                             Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, chargeClassID, row["ItemID"].ToString(), guarantorID /*AppSession.Parameter.SelfGuarantor*/, isPrescription, reg.SRRegistrationType));

                row["BasicPrice"] = basic == null ? 0 : basic.Price;

                var coveredClassId = grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf ? reg.ChargeClassID : reg.CoverageClassID;

                ItemTariff cover = (Tariff.GetItemTariff(transactionDate, grr.SRTariffType, coveredClassId, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType) ??
                                    Tariff.GetItemTariff(transactionDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType)) ??
                                   (Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, coveredClassId, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType) ??
                                    Tariff.GetItemTariff(transactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, coveredClassId, row["ItemID"].ToString(), guarantorID, isPrescription, reg.SRRegistrationType));

                row["CoveragePrice"] = cover == null ? 0 : cover.Price;
            }

            tbl.AcceptChanges();

            var types = new GuarantorItemTypeRuleCollection();
            types.Query.Where(types.Query.GuarantorID == guarantorID);
            types.LoadAll();

            foreach (DataRow row in tbl.Rows)
            {
                var item = new Item();
                item.LoadByPrimaryKey(row["ItemID"].ToString());
                if (item.SRItemType == ItemType.Medical)
                {
                    row["IsInclude"] = grr.IsIncludeItemMedical ?? false;
                    row["IsGuarantor"] = grr.IsIncludeItemMedicalToGuarantor ?? false;
                }
                else if (item.SRItemType == ItemType.NonMedical || item.SRItemType == ItemType.Kitchen)
                {
                    row["IsInclude"] = grr.IsIncludeItemNonMedical ?? false;
                    row["IsGuarantor"] = grr.IsIncludeItemNonMedicalToGuarantor ?? false;
                }
                else
                {
                    row["IsInclude"] = true;
                    row["IsGuarantor"] = types.AsEnumerable().Any() ? types.SingleOrDefault(t => t.SRItemType == item.SRItemType).IsToGuarantor :
                                                                      true;
                }
            }

            var grrItemColl = new GuarantorItemRuleCollection();
            grrItemColl.Query.Where(grrItemColl.Query.GuarantorID == guarantorID);
            grrItemColl.LoadAll();

            foreach (var grrItem in grrItemColl)
            {
                foreach (var row in tbl.Rows.Cast<DataRow>().Where(row => row["ItemID"].ToString().Equals(grrItem.ItemID)))
                {
                    row["SRGuarantorRuleType"] = grrItem.SRGuarantorRuleType;
                    row["IsByTariffComponent"] = grrItem.IsByTariffComponent ?? false;

                    if (grrItem.IsByTariffComponent ?? false)
                    {
                        var comps = Helper.Tariff.GetItemTariffComponentCollection(transactionDate, grr.SRTariffType, reg.ChargeClassID, grrItem.ItemID);
                        if (comps == null || comps.Count() == 0)
                        {
                            comps = Helper.Tariff.GetItemTariffComponentCollection(transactionDate, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, grrItem.ItemID);
                            if (comps == null || comps.Count() == 0)
                                comps = Helper.Tariff.GetItemTariffComponentCollection(transactionDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, grrItem.ItemID);
                        }
                        var girtc = new GuarantorItemRuleTariffComponentCollection();
                        girtc.Query.Where(girtc.Query.GuarantorID == guarantorID, girtc.Query.ItemID == grrItem.ItemID, girtc.Query.TariffComponentID.In(comps.Select(c => c.TariffComponentID)));
                        if (girtc.Query.Load())
                        {
                            //format : 
                            //BasicPrice1/CoveragePrice1/TariffComponentID1/AmountValue1;
                            //BasicPrice.../CoveragePrice.../TariffComponentID.../AmountValue...;
                            //...
                            var str = string.Empty;
                            foreach (var gir in girtc)
                            {
                                var tblBasic = Helper.Tariff.GetItemTariffComponent(transactionDate, grr.SRTariffType, reg.ChargeClassID, gir.TariffComponentID, grrItem.ItemID);
                                decimal tcBasic = 0;
                                if (tblBasic == null || tblBasic.Rows.Count == 0) tcBasic = 0;
                                else tcBasic = Convert.ToDecimal(tblBasic.Rows[0]["Price"]);

                                decimal tcCoverage = 0;
                                var tblCoverage = Helper.Tariff.GetItemTariffComponent(transactionDate, grr.SRTariffType, grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeSelf ? reg.ChargeClassID : reg.CoverageClassID, gir.TariffComponentID, grrItem.ItemID);
                                if (tblCoverage == null || tblCoverage.Rows.Count == 0) tcBasic = 0;
                                else tcCoverage = Convert.ToDecimal(tblBasic.Rows[0]["Price"]);

                                str += string.Format("{0}/{1}/{2}/", tcBasic.ToString(), tcCoverage.ToString(), gir.TariffComponentID);
                                switch (regType)
                                {
                                    case "IPR":
                                        str += (gir.AmountValue ?? 0).ToString() + ";";
                                        break;
                                    case "OPR":
                                        str += (gir.OutpatientAmountValue ?? 0).ToString() + ";";
                                        break;
                                    case "EMR":
                                        str += (gir.EmergencyAmountValue ?? 0).ToString() + ";";
                                        break;
                                }
                            }
                            str = str.Remove(str.Length - 1, 1);
                            row["TariffComponentValue"] = str;
                        }
                        else
                        {
                            switch (regType)
                            {
                                case "IPR":
                                    row["AmountValue"] = grrItem.AmountValue ?? 0;
                                    break;
                                case "OPR":
                                    row["AmountValue"] = grrItem.OutpatientAmountValue ?? 0;
                                    break;
                                case "EMR":
                                    row["AmountValue"] = grrItem.EmergencyAmountValue ?? 0;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (regType)
                        {
                            case "IPR":
                                row["AmountValue"] = grrItem.AmountValue ?? 0;
                                break;
                            case "OPR":
                                row["AmountValue"] = grrItem.OutpatientAmountValue ?? 0;
                                break;
                            case "EMR":
                                row["AmountValue"] = grrItem.EmergencyAmountValue ?? 0;
                                break;
                        }
                    }

                    row["IsValueInPercent"] = grrItem.IsValueInPercent ?? false;
                    row["IsInclude"] = grrItem.IsInclude ?? false;
                    row["IsGuarantor"] = grrItem.IsToGuarantor ?? false;
                    break;
                }
            }

            if (isPrescription)
            {
                var grrItemPrescColl = new GuarantorItemPrescriptionRuleCollection();
                grrItemPrescColl.Query.Where(grrItemPrescColl.Query.GuarantorID == guarantorID);
                grrItemPrescColl.LoadAll();

                foreach (var grrItemPresc in grrItemPrescColl)
                {
                    foreach (var row in tbl.Rows.Cast<DataRow>().Where(row => row["ItemID"].ToString().Equals(grrItemPresc.ItemID)))
                    {
                        row["SRGuarantorRuleType"] = grrItemPresc.SRGuarantorRuleType;

                        switch (regType)
                        {
                            case "IPR":
                                row["AmountValue"] = grrItemPresc.AmountValue ?? 0;
                                break;
                            case "OPR":
                                row["AmountValue"] = grrItemPresc.OutpatientAmountValue ?? 0;
                                break;
                            case "EMR":
                                row["AmountValue"] = grrItemPresc.EmergencyAmountValue ?? 0;
                                break;

                        }

                        row["IsValueInPercent"] = grrItemPresc.IsValueInPercent ?? false;
                        row["IsInclude"] = grrItemPresc.IsInclude ?? false;
                        row["IsGuarantor"] = grrItemPresc.IsToGuarantor ?? false;
                        break;
                    }
                }
            }

            foreach (var regItem in itemRules)
            {
                foreach (var row in tbl.Rows.Cast<DataRow>().Where(row => (string)row["ItemID"] == regItem.ItemID))
                {
                    row["SRGuarantorRuleType"] = regItem.SRGuarantorRuleType;

                    switch (regType)
                    {
                        case "IPR":
                            row["AmountValue"] = regItem.AmountValue ?? 0;
                            break;
                        case "OPR":
                            row["AmountValue"] = regItem.OutpatientAmountValue ?? 0;
                            break;
                        case "EMR":
                            row["AmountValue"] = regItem.EmergencyAmountValue ?? 0;
                            break;
                    }

                    row["IsValueInPercent"] = regItem.IsValueInPercent ?? false;
                    row["IsInclude"] = regItem.IsInclude ?? false;
                    row["IsGuarantor"] = regItem.IsToGuarantor ?? false;
                    break;
                }
            }

            return tbl;
        }
    }
}
