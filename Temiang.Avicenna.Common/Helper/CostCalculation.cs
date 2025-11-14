using System;
using System.Data;
using System.Linq;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Common
{
    public partial class Helper
    {
        public class CostCalculation
        {
            public CostCalculation(string guarantorID, string itemID, decimal total,
                DataTable coveredItems, decimal qty, decimal discount)
            {
                var grr = new Guarantor();
                grr.LoadByPrimaryKey(guarantorID);

                decimal tPatient = 0, tGuarantor = 0;

                var row = coveredItems.AsEnumerable().SingleOrDefault(c => c.Field<string>("ItemID") == itemID);
                if (row != null)
                {
                    GetCalculatedValue(guarantorID, (row["SRGuarantorRuleType"] == DBNull.Value ? string.Empty : (string)row["SRGuarantorRuleType"]),
                                       (grr.IsGlobalPlafond ?? false), (bool)row["IsGuarantor"], (bool)row["IsValueInPercent"],
                                       (decimal)row["AmountValue"], total, out tPatient, out tGuarantor, (decimal)row["BasicPrice"],
                                       (decimal)row["CoveragePrice"], (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeMemberID),
                                       (bool)row["IsInclude"], qty, discount);
                }

                PatientAmount = tPatient;
                GuarantorAmount = tGuarantor;
            }

            //resep + recipeAmt
            public CostCalculation(string guarantorID, string itemID, decimal total, DataTable coveredItems, decimal qty, decimal recipeAmt,
                                   decimal discount)
            {
                var grr = new Guarantor();
                grr.LoadByPrimaryKey(guarantorID);

                decimal tPatient = 0, tGuarantor = 0;

                var row = coveredItems.AsEnumerable().SingleOrDefault(c => c.Field<string>("ItemID") == itemID);
                if (row != null)
                {
                    GetCalculatedValue(guarantorID, (row["SRGuarantorRuleType"] == DBNull.Value ? string.Empty : (string)row["SRGuarantorRuleType"]),
                                       (grr.IsGlobalPlafond ?? false), (bool)row["IsGuarantor"], (bool)row["IsValueInPercent"],
                                       (decimal)row["AmountValue"], total, out tPatient, out tGuarantor, (decimal)row["BasicPrice"],
                                       (decimal)row["CoveragePrice"], (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeMemberID),
                                       (bool)row["IsInclude"], qty, true, recipeAmt, discount);
                }

                PatientAmount = tPatient;
                GuarantorAmount = tGuarantor;
            }

            public CostCalculation(string guarantorID, string itemID, decimal total, DataTable coveredItems, decimal qty, bool isCito,
                                   bool isCitoInPercent, decimal basicCito, decimal price, bool isRoomIn, bool isItemRoom,
                                   decimal tariffDiscForRoomIn, decimal discount, bool isGlobalPlafond,
                                   string srItemConditionRule, DateTime transactionDate, bool isVariable)
            {
                var grr = new Guarantor();
                grr.LoadByPrimaryKey(guarantorID);

                decimal tPatient = 0, tGuarantor = 0;

                var row = coveredItems.AsEnumerable().SingleOrDefault(c => c.Field<string>("ItemID") == itemID);
                if (row != null)
                {
                    //var basicPrice = (decimal) row["BasicPrice"];
                    //var coveragePrice = (decimal) row["CoveragePrice"];

                    var basicPrice = Tariff.GetItemConditionRuleTariff((decimal)row["BasicPrice"], srItemConditionRule, transactionDate);
                    var coveragePrice = Tariff.GetItemConditionRuleTariff((decimal)row["CoveragePrice"], srItemConditionRule, transactionDate);

                    GetCalculatedValue(guarantorID, (row["SRGuarantorRuleType"] == DBNull.Value ? string.Empty : (string)row["SRGuarantorRuleType"]),
                                       isGlobalPlafond, (bool)row["IsGuarantor"], (bool)row["IsValueInPercent"],
                                       (decimal)row["AmountValue"], total, out tPatient, out tGuarantor, basicPrice,
                                       coveragePrice, (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeMemberID),
                                       (bool)row["IsInclude"], qty, isCito, isCitoInPercent, basicCito, price, isRoomIn, isItemRoom,
                                       tariffDiscForRoomIn, discount, isVariable);
                }

                PatientAmount = tPatient;
                GuarantorAmount = tGuarantor;
            }

            public CostCalculation(string guarantorID, bool isGlobalPlafond, string itemID, decimal total, DataTable coveredItems, decimal qty,
                                   decimal price, decimal discount)
            {
                var grr = new Guarantor();
                grr.LoadByPrimaryKey(guarantorID);

                decimal tPatient = 0, tGuarantor = 0;

                var row = coveredItems.AsEnumerable().SingleOrDefault(c => c.Field<string>("ItemID") == itemID);
                if (row != null)
                {
                    GetCalculatedValue(guarantorID, (row["SRGuarantorRuleType"] == DBNull.Value ? string.Empty : (string)row["SRGuarantorRuleType"]),
                                       isGlobalPlafond, (bool)row["IsGuarantor"], (bool)row["IsValueInPercent"],
                                       (decimal)row["AmountValue"], total, out tPatient, out tGuarantor, (decimal)row["BasicPrice"],
                                       (decimal)row["CoveragePrice"], (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeMemberID),
                                       (bool)row["IsInclude"], qty, price, discount);
                }

                PatientAmount = tPatient;
                GuarantorAmount = tGuarantor;
            }

            public CostCalculation(string guarantorID, bool isGlobalPlafond, string itemID, decimal total, DataTable coveredItems, decimal qty,
                                   decimal price, decimal recipeAmt, decimal discount)
            {
                var grr = new Guarantor();
                grr.LoadByPrimaryKey(guarantorID);

                decimal tPatient = 0, tGuarantor = 0;

                var row = coveredItems.AsEnumerable().SingleOrDefault(c => c.Field<string>("ItemID") == itemID);
                if (row != null)
                {
                    GetCalculatedValue(guarantorID, (row["SRGuarantorRuleType"] == DBNull.Value ? string.Empty : (string)row["SRGuarantorRuleType"]),
                                       isGlobalPlafond, (bool)row["IsGuarantor"], (bool)row["IsValueInPercent"],
                                       (decimal)row["AmountValue"], total, out tPatient, out tGuarantor, (decimal)row["BasicPrice"],
                                       (decimal)row["CoveragePrice"], (grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeMemberID),
                                       (bool)row["IsInclude"], qty, price, recipeAmt, discount);
                }

                PatientAmount = tPatient;
                GuarantorAmount = tGuarantor;
            }

            public decimal PatientAmount { get; set; }

            public decimal GuarantorAmount { get; set; }

            public decimal DiscountAmount { get; set; }

            private void GetCalculatedValue(string guarantorID, string guarantorRuleType, bool isGlobalPlafond, bool isGuarantor,
                                            bool isValueInPercent, decimal amountValue, decimal total, out decimal patientAmount,
                                            out decimal guarantorAmount, decimal basicPrice, decimal coveragePrice, bool isMember,
                                            bool isInclude, decimal qty, decimal discount)
            {
                decimal tPatient, tGuarantor, totalCoverage;

                basicPrice = (basicPrice * qty) > total ? total / qty : basicPrice;

                if (basicPrice < coveragePrice)
                    coveragePrice = basicPrice;

                if (basicPrice > 0 && basicPrice <= total / qty)
                    totalCoverage = (coveragePrice == 0) ? total : (coveragePrice * qty) - discount;
                else
                    totalCoverage = total;

                if (guarantorRuleType.Equals(AppSession.Parameter.GuarantorRuleTypePlavon))
                {
                    if (guarantorID.Equals(AppSession.Parameter.SelfGuarantor))
                    {
                        tPatient = total;
                        tGuarantor = 0;
                    }
                    else
                    {
                        if (isGuarantor)
                        {
                            if (isInclude)
                            {
                                var grr = new GuarantorBridgingCollection();
                                grr.Query.Where(grr.Query.GuarantorID == guarantorID, grr.Query.SRBridgingType == AppEnum.BridgingType.INACBG.ToString());
                                if (grr.Query.Load()) amountValue = total;

                                decimal tPlafon = isValueInPercent ? (amountValue / 100) * total : (isGlobalPlafond ? amountValue * qty : amountValue);

                                tGuarantor = total >= tPlafon ? tPlafon : total;
                                if (total - tGuarantor <= 0)
                                    tPatient = 0;
                                else
                                    tPatient = total - tGuarantor;
                            }
                            else
                            {
                                tPatient = total;
                                tGuarantor = 0;
                            }
                        }
                        else
                        {
                            tPatient = total;
                            tGuarantor = 0;
                        }
                    }
                }
                else
                {
                    if (!isGuarantor)
                    {
                        tPatient = total;
                        tGuarantor = 0;
                    }
                    else
                    {
                        tPatient = (total - totalCoverage >= 0) ? total - totalCoverage : 0;
                        tGuarantor = totalCoverage;
                    }
                }

                patientAmount = isMember ? tGuarantor : tPatient;
                guarantorAmount = isMember ? tPatient : tGuarantor;
            }

            private void GetCalculatedValue(string guarantorID, string guarantorRuleType, bool isGlobalPlafond, bool isGuarantor,
                                            bool isValueInPercent, decimal amountValue, decimal total, out decimal patientAmount,
                                            out decimal guarantorAmount, decimal basicPrice, decimal coveragePrice, bool isMember,
                                            bool isInclude, decimal qty, bool isPresc, decimal recipeAmt, decimal discount)
            {
                decimal tPatient, tGuarantor, totalCoverage;

                basicPrice = basicPrice * qty > total ? total / qty : basicPrice;

                if (basicPrice < coveragePrice)
                    coveragePrice = basicPrice;

                if (basicPrice > 0 && basicPrice <= total / qty)
                    totalCoverage = (coveragePrice == 0) ? total : ((coveragePrice * qty) - discount) + recipeAmt;
                else
                    totalCoverage = total;

                if (guarantorRuleType.Equals(AppSession.Parameter.GuarantorRuleTypePlavon))
                {
                    if (guarantorID.Equals(AppSession.Parameter.SelfGuarantor))
                    {
                        tPatient = total;
                        tGuarantor = 0;
                    }
                    else
                    {
                        if (isGuarantor)
                        {
                            if (isInclude)
                            {
                                var grr = new GuarantorBridgingCollection();
                                grr.Query.Where(grr.Query.GuarantorID == guarantorID, grr.Query.SRBridgingType == AppEnum.BridgingType.INACBG.ToString());
                                if (grr.Query.Load()) amountValue = total;

                                decimal tPlafon = isValueInPercent ? (amountValue / 100) * total : (isGlobalPlafond ? amountValue * qty : amountValue);

                                tGuarantor = total >= tPlafon ? tPlafon : total;
                                if (total - tGuarantor <= 0)
                                    tPatient = 0;
                                else
                                    tPatient = total - tGuarantor;
                            }
                            else
                            {
                                tPatient = total;
                                tGuarantor = 0;
                            }
                        }
                        else
                        {
                            tPatient = total;
                            tGuarantor = 0;
                        }
                    }
                }
                else
                {
                    if (!isGuarantor)
                    {
                        tPatient = total;
                        tGuarantor = 0;
                    }
                    else
                    {
                        tPatient = (total - totalCoverage >= 0) ? total - totalCoverage : 0;
                        tGuarantor = totalCoverage;
                    }
                }

                patientAmount = isMember ? tGuarantor : tPatient;
                guarantorAmount = isMember ? tPatient : tGuarantor;
            }

            private void GetCalculatedValue(string guarantorID, string guarantorRuleType, bool isGlobalPlafond, bool isGuarantor,
                                            bool isValueInPercent, decimal amountValue, decimal total, out decimal patientAmount,
                                            out decimal guarantorAmount, decimal basicPrice, decimal coveragePrice, bool isMember,
                                            bool isInclude, decimal qty, bool isCito, bool isCitoInPercent, decimal basicCito,
                                            decimal price, bool isRoomIn, bool isItemRoom, decimal tariffDiscForRoomIn, decimal discount, bool isVariable)
            {
                decimal tPatient, tGuarantor, totalCoverage;

                basicPrice = price;
                //if (isVariable && coveragePrice == 0) coveragePrice = price; ==> validasi pindah ke bawah

                if (isRoomIn && isItemRoom)
                    coveragePrice = coveragePrice - (coveragePrice * tariffDiscForRoomIn / 100);

                if (basicPrice < coveragePrice)
                    coveragePrice = basicPrice;

                if (basicPrice != coveragePrice)
                {
                    if (isCito)
                    {
                        //if (isCitoInPercent)
                        //    totalCoverage = (coveragePrice == 0) ? (basicPrice + (basicPrice * basicCito / 100)) * qty
                        //                                         : (coveragePrice + (coveragePrice * basicCito / 100)) * qty;
                        //else
                        //    totalCoverage = (coveragePrice == 0) ? (basicPrice + basicCito) * qty : (coveragePrice + basicCito) * qty;

                        if (isCitoInPercent)
                            totalCoverage = (coveragePrice == 0) ? 0
                                                                 : (coveragePrice + (coveragePrice * basicCito / 100)) * qty;
                        else
                            totalCoverage = (coveragePrice == 0) ? 0 : (coveragePrice + basicCito) * qty;

                        totalCoverage -= discount;
                    }
                    else
                        //totalCoverage = (coveragePrice == 0) ? total : (coveragePrice * qty) - discount;
                        totalCoverage = (coveragePrice == 0) ? 0 : (coveragePrice * qty) - discount;
                }
                else
                    totalCoverage = total;

                if (guarantorRuleType.Equals(AppSession.Parameter.GuarantorRuleTypePlavon))
                {
                    if (guarantorID.Equals(AppSession.Parameter.SelfGuarantor))
                    {
                        tPatient = total;
                        tGuarantor = 0;
                    }
                    else
                    {
                        if (isGuarantor)
                        {
                            if (isInclude)
                            {
                                var grr = new GuarantorBridgingCollection();
                                grr.Query.Where(grr.Query.GuarantorID == guarantorID, grr.Query.SRBridgingType == AppEnum.BridgingType.INACBG.ToString());
                                if (grr.Query.Load()) amountValue = total;

                                decimal tPlafon = isValueInPercent ? (amountValue / 100) * total : (isGlobalPlafond ? amountValue * qty : amountValue);

                                tGuarantor = total >= tPlafon ? tPlafon : total;
                                if (total - tGuarantor <= 0)
                                    tPatient = 0;
                                else
                                    tPatient = total - tGuarantor;
                            }
                            else
                            {
                                tPatient = total;
                                tGuarantor = 0;
                            }
                        }
                        else
                        {
                            tPatient = total;
                            tGuarantor = 0;
                        }
                    }
                }
                else
                {
                    if (!isGuarantor)
                    {
                        tPatient = total;
                        tGuarantor = 0;
                    }
                    else
                    {
                        /*--- untuk item variable, jika di rule-nya ditanggung maka akan masuk ke guarantor semua ---*/
                        if (isVariable)
                        {
                            tPatient = 0;
                            tGuarantor = total;
                        }
                        else
                        {
                            tPatient = (total - totalCoverage >= 0) ? total - totalCoverage : 0;
                            tGuarantor = (total - totalCoverage >= 0) ? totalCoverage : total;

                            if (guarantorRuleType == AppSession.Parameter.GuarantorRuleTypeMargin)
                            {
                                decimal tMargin = isValueInPercent ? (amountValue / 100) * totalCoverage : amountValue;
                                if (total == totalCoverage + tMargin)
                                {
                                    tPatient = 0;
                                    tGuarantor = total;
                                }
                            }
                        }
                    }
                }

                patientAmount = isMember ? tGuarantor : tPatient;
                guarantorAmount = isMember ? tPatient : tGuarantor;
            }

            private void GetCalculatedValue(string guarantorID, string guarantorRuleType, bool isGlobalPlafond, bool isGuarantor,
                                            bool isValueInPercent, decimal amountValue, decimal total, out decimal patientAmount,
                                            out decimal guarantorAmount, decimal basicPrice, decimal coveragePrice, bool isMember,
                                            bool isInclude, decimal qty, decimal price, decimal discount)
            {
                decimal tPatient, tGuarantor, totalCoverage;

                if (guarantorID != AppSession.Parameter.SelfGuarantor)
                {
                    basicPrice = price;

                    if (basicPrice < coveragePrice)
                        coveragePrice = basicPrice;

                    if (basicPrice > 0 && basicPrice <= total / qty)
                        totalCoverage = (coveragePrice == 0) ? total : (coveragePrice * qty) - discount;
                    else
                        totalCoverage = total;
                }
                else
                    totalCoverage = total;

                if (guarantorRuleType.Equals(AppSession.Parameter.GuarantorRuleTypePlavon))
                {
                    if (guarantorID.Equals(AppSession.Parameter.SelfGuarantor))
                    {
                        tPatient = total;
                        tGuarantor = 0;
                    }
                    else
                    {
                        if (isGuarantor)
                        {
                            if (isInclude)
                            {
                                var grr = new GuarantorBridgingCollection();
                                grr.Query.Where(grr.Query.GuarantorID == guarantorID, grr.Query.SRBridgingType == AppEnum.BridgingType.INACBG.ToString());
                                if (grr.Query.Load()) amountValue = total;

                                decimal tPlafon = isValueInPercent ? (amountValue / 100) * total : (isGlobalPlafond ? amountValue * qty : amountValue);

                                tGuarantor = total >= tPlafon ? tPlafon : total;
                                if (total - tGuarantor <= 0)
                                    tPatient = 0;
                                else
                                    tPatient = total - tGuarantor;
                            }
                            else
                            {
                                tPatient = total;
                                tGuarantor = 0;
                            }
                        }
                        else
                        {
                            tPatient = total;
                            tGuarantor = 0;
                        }
                    }
                }
                else
                {
                    if (!isGuarantor)
                    {
                        tPatient = total;
                        tGuarantor = 0;
                    }
                    else
                    {
                        tPatient = (total - totalCoverage >= 0) ? total - totalCoverage : 0;
                        tGuarantor = totalCoverage;
                    }
                }

                patientAmount = isMember ? tGuarantor : tPatient;
                guarantorAmount = isMember ? tPatient : tGuarantor;
            }

            private void GetCalculatedValue(string guarantorID, string guarantorRuleType, bool isGlobalPlafond, bool isGuarantor,
                                            bool isValueInPercent, decimal amountValue, decimal total, out decimal patientAmount,
                                            out decimal guarantorAmount, decimal basicPrice, decimal coveragePrice, bool isMember,
                                            bool isInclude, decimal qty, decimal price, decimal recipeAmt, decimal discount)
            {
                decimal tPatient, tGuarantor, totalCoverage;

                if (guarantorID != AppSession.Parameter.SelfGuarantor)
                {
                    if (basicPrice == coveragePrice)
                        totalCoverage = total;
                    else
                    {
                        basicPrice = price;
                        if (basicPrice < coveragePrice)
                            totalCoverage = total;
                        else if (basicPrice > 0 && basicPrice <= (qty == 0 ? 0 : total / qty))
                            totalCoverage = (coveragePrice == 0) ? total : ((coveragePrice * qty) - discount) + recipeAmt;
                        else
                            totalCoverage = total;
                    }
                }
                else
                    totalCoverage = total;

                if (guarantorRuleType.Equals(AppSession.Parameter.GuarantorRuleTypePlavon))
                {
                    if (guarantorID.Equals(AppSession.Parameter.SelfGuarantor))
                    {
                        tPatient = total;
                        tGuarantor = 0;
                    }
                    else
                    {
                        if (isGuarantor)
                        {
                            if (isInclude)
                            {
                                var grr = new GuarantorBridgingCollection();
                                grr.Query.Where(grr.Query.GuarantorID == guarantorID, grr.Query.SRBridgingType == AppEnum.BridgingType.INACBG.ToString());
                                if (grr.Query.Load()) amountValue = total;

                                decimal tPlafon = isValueInPercent ? (amountValue / 100) * total : (isGlobalPlafond ? amountValue * qty : amountValue);

                                tGuarantor = total >= tPlafon ? tPlafon : total;
                                if (total - tGuarantor <= 0)
                                    tPatient = 0;
                                else
                                    tPatient = total - tGuarantor;
                            }
                            else
                            {
                                tPatient = total;
                                tGuarantor = 0;
                            }
                        }
                        else
                        {
                            tPatient = total;
                            tGuarantor = 0;
                        }
                    }
                }
                else
                {
                    if (!isGuarantor)
                    {
                        tPatient = total;
                        tGuarantor = 0;
                    }
                    else
                    {
                        tPatient = (total - totalCoverage >= 0) ? total - totalCoverage : 0;
                        tGuarantor = totalCoverage;
                    }
                }

                patientAmount = isMember ? tGuarantor : tPatient;
                guarantorAmount = isMember ? tPatient : tGuarantor;
            }

            public static void GetBillingTotal(string[] registrationNo, string srBusinessMethod, decimal plafonAmount, out decimal patientAmount, out decimal guarantorAmount,
                                               Guarantor guarantor, bool isGlobalPlafond)
            {
                var collection = new CostCalculationCollection();

                var query = new CostCalculationQuery("a");
                var item = new ItemQuery("b");
                //var unit = new ServiceUnitQuery("c");
                var view = new VwTransactionQuery("d");
                
                //db:29-09-2023 --> diremark, filter langsung dari view
                //var charges = new TransChargesQuery("i"); // utk MCU cek biar paket dan detil ga terhitung 2x
                //var chargesitem = new TransChargesItemQuery("j");

                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                //query.InnerJoin(chargesitem).On(query.TransactionNo == chargesitem.TransactionNo && query.SequenceNo == chargesitem.SequenceNo);
                query.InnerJoin(view).On(query.TransactionNo == view.TransactionNo && query.RegistrationNo == view.RegistrationNo); //&& query.SequenceNo == chargesitem.SequenceNo);
                //query.InnerJoin(unit).On(view.ServiceUnitID == unit.ServiceUnitID);

                ////filter transaksi MCU
                //query.LeftJoin(charges).On(query.TransactionNo == charges.TransactionNo && query.RegistrationNo == charges.RegistrationNo);

                query.Where(
                    query.RegistrationNo.In(registrationNo),
                    //charges.PackageReferenceNo.IsNull() //db:29-09-2023 --> diremark, filter langsung dari view
                    view.PackageReferenceNo == string.Empty
                    );
                query.OrderBy(
                    view.TransactionDate.Ascending,
                    query.TransactionNo.Ascending,
                    query.SequenceNo.Ascending
                    );

                collection.Load(query);

                decimal? patient = collection.Sum(c => c.PatientAmount), grr = collection.Sum(c => c.GuarantorAmount);

                var regs = new RegistrationCollection();
                regs.Query.Where(regs.Query.RegistrationNo.In(registrationNo));
                regs.LoadAll();

                decimal admin = regs.Sum(r => (r.PatientAdm ?? 0) + (r.GuarantorAdm ?? 0) - (r.DiscAdmGuarantor ?? 0) - (r.DiscAdmPatient ?? 0));

                if (guarantor.GuarantorID == AppSession.Parameter.SelfGuarantor)
                    patient += admin;
                else
                {
                    if (guarantor.IsIncludeAdminValue ?? false)
                        grr += admin;
                    else
                        patient += admin;
                }

                if (isGlobalPlafond && (plafonAmount > 0))
                {
                    bool isBridging = false;
                    //if (AppSession.Parameter.IsBridgingBillingBpjs)
                    var isBridgingBillingBpjs = AppParameter.IsYes(AppParameter.ParameterItem.IsBridgingBillingBpjs);
                    if (isBridgingBillingBpjs)
                    {
                        var bridging = new GuarantorBridging();
                        bridging.Query.Where(bridging.Query.GuarantorID == guarantor.GuarantorID,
                                             bridging.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(),
                                                                              AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(),
                                                                              AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
                        if (bridging.Query.Load())
                        {
                            isBridging = true;
                        }
                    }

                    if (isBridging)
                    {
                        grr = plafonAmount;
                        patient = 0;
                    }
                    else
                    {
                        if (patient + grr >= plafonAmount)
                        {
                            patient = (patient + grr) - plafonAmount;
                            grr = plafonAmount;
                        }
                        else
                        {
                            grr = patient + grr;
                            patient = 0;
                        }
                    }

                }

                patientAmount = patient ?? 0;
                guarantorAmount = grr ?? 0;
            }

            public static void GetBillingTotal2(string[] registrationNo, string srBusinessMethod, decimal plafonAmount, out decimal patientAmount, out decimal guarantorAmount,
                                              Guarantor guarantor, bool isGlobalPlafond)
            {
                var collection = new CostCalculationCollection();

                var query = new CostCalculationQuery("a");
                var item = new ItemQuery("b");
                //var unit = new ServiceUnitQuery("c"); //db:29-09-2023
                var view = new VwTransactionQuery("d");

                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(view).On(query.TransactionNo == view.TransactionNo && query.RegistrationNo == view.RegistrationNo);
                //query.InnerJoin(unit).On(view.ServiceUnitID == unit.ServiceUnitID); //db:29-09-2023

                //db:29-09-2023 --> diremark, filter langsung dari view
                //filter transaksi MCU
                //var charges = new TransChargesQuery("i"); // utk MCU cek biar paket dan detil ga terhitung 2x 
                //query.LeftJoin(charges).On(query.TransactionNo == charges.TransactionNo && query.RegistrationNo == charges.RegistrationNo);

                //query.Select(query);

                query.Where(
                    query.RegistrationNo.In(registrationNo),
                    //charges.PackageReferenceNo.IsNull() //db:29-09-2023 --> diremark, filter langsung dari view
                    view.PackageReferenceNo == string.Empty
                    );
                query.OrderBy(
                    view.TransactionDate.Ascending,
                    query.TransactionNo.Ascending,
                    query.SequenceNo.Ascending
                    );

                collection.Load(query);

                decimal? patient = collection.Sum(c => c.PatientAmount), grr = collection.Sum(c => c.GuarantorAmount);

                var regs = new RegistrationCollection();
                regs.Query.Where(regs.Query.RegistrationNo.In(registrationNo));
                regs.LoadAll();

                decimal admin = regs.Sum(r => (r.PatientAdm ?? 0) + (r.GuarantorAdm ?? 0) - (r.DiscAdmGuarantor ?? 0) - (r.DiscAdmPatient ?? 0));

                if (guarantor.GuarantorID == AppSession.Parameter.SelfGuarantor)
                    patient += admin;
                else
                {
                    if (guarantor.IsIncludeAdminValue ?? false)
                        grr += admin;
                    else
                        patient += admin;
                }

                if (isGlobalPlafond && (plafonAmount > 0))
                {
                    bool isBridging = false;

                    //if (AppSession.Parameter.IsBridgingBillingBpjs)
                    var isBridgingBillingBpjs = AppParameter.IsYes(AppParameter.ParameterItem.IsBridgingBillingBpjs);
                    if (isBridgingBillingBpjs)
                    {
                        var bridging = new GuarantorBridging();
                        bridging.Query.Where(bridging.Query.GuarantorID == guarantor.GuarantorID,
                                             bridging.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(),
                                                                              AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(),
                                                                              AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
                        if (bridging.Query.Load())
                        {
                            isBridging = true;
                        }
                    }

                    if (isBridging)
                    {
                        grr = plafonAmount;
                        patient = 0;
                    }
                    else
                    {
                        if (patient + grr >= plafonAmount)
                        {
                            patient = (patient + grr) - plafonAmount;
                            grr = plafonAmount;
                        }
                        else
                        {
                            grr = patient + grr;
                            patient = 0;
                        }
                    }
                }

                patientAmount = patient ?? 0;
                guarantorAmount = grr ?? 0;
            }

            /// <summary>
            /// Tune Up GetBillingTotal2 untuk keperluan display total (Handono 230328)
            /// </summary>
            /// <param name="registrationNo"></param>
            /// <param name="srBusinessMethod"></param>
            /// <param name="plafonAmount"></param>
            /// <param name="patientAmount"></param>
            /// <param name="guarantorAmount"></param>
            /// <param name="guarantor"></param>
            /// <param name="isGlobalPlafond"></param>
            public static void GetBillingTotalStatus(string[] registrationNos, decimal plafonAmount, out decimal patientAmount, out decimal guarantorAmount,
                                  Guarantor guarantor, bool isGlobalPlafond)
            {
                // Kata-kata mutiara dari ci Deb (230330):
                // yg view, item, unit bisa di remark sepertinya gak perlu
                // tp kalo yg  packagereferenceno kayakny msh perlu, krn memang ada yg masuk ke cc, khusus tarif paket
                // dulu di join ke view krn ada di cc yg transaksi statusnya void jg masuk ke cc
                // bugs yg belum ketemu bocornya darimana
                // trs ada double cc, no transaksi sama tp beda noreg

                var query = new CostCalculationQuery("a");
                //var item = new ItemQuery("b");
                //var unit = new ServiceUnitQuery("c"); kenapa dijoin ke ServiceUnitQuery ? Tidak perlu (DB)
                var view = new VwTransactionQuery("d"); // di join ke view krn ada di cc yg transaksi statusnya void jg masuk ke cc (DB)

                //query.InnerJoin(item).On(query.ItemID == item.ItemID); kenapa dijoin ke Item ? Tidak perlu (DB)
                query.InnerJoin(view).On(query.TransactionNo == view.TransactionNo && query.RegistrationNo == view.RegistrationNo);
                //query.InnerJoin(unit).On(view.ServiceUnitID == unit.ServiceUnitID);

                //db:29-09-2023 --> diremark, filter langsung dari view
                //filter transaksi MCU
                //var charges = new TransChargesQuery("i"); // utk MCU cek biar paket dan detil ga terhitung 2x
                //query.LeftJoin(charges).On(query.TransactionNo == charges.TransactionNo && query.RegistrationNo == charges.RegistrationNo);

                // Apakah yg PackageReferenceNo nya null masuk juga ke CostCalculation ?
                // packagereferenceno kayakny msh perlu, krn memang ada yg masuk ke cc, khusus tarif paket (DB)
                query.Where(
                query.RegistrationNo.In(registrationNos),
                //charges.PackageReferenceNo.IsNull(), //db:29-09-2023 --> diremark, filter langsung dari view
                view.PackageReferenceNo == string.Empty
                );

                query.Select(query.PatientAmount.Sum().As("PatientAmount"), query.GuarantorAmount.Sum().As("GuarantorAmount"));

                var dtb = query.LoadDataTable();

                decimal? patient = 0, grr = 0;
                if (dtb.Rows.Count > 0)
                {
                    patient = (dtb.Rows[0]["PatientAmount"]).ToDecimal();
                    grr = (dtb.Rows[0]["GuarantorAmount"]).ToDecimal();
                }

                var qrReg = new RegistrationQuery("reg");
                qrReg.Where(qrReg.RegistrationNo.In(registrationNos));
                qrReg.Select(qrReg.PatientAdm.Sum().As("PatientAdm"),
                    qrReg.GuarantorAdm.Sum().As("GuarantorAdm"),
                    qrReg.DiscAdmGuarantor.Sum().As("DiscAdmGuarantor"),
                    qrReg.DiscAdmPatient.Sum().As("DiscAdmPatient")
                    );
                var dtbReg = qrReg.LoadDataTable();

                decimal admin = 0;
                if (dtbReg.Rows.Count > 0)
                {
                    admin = (dtbReg.Rows[0]["PatientAdm"]).ToDecimal() +
                       (dtbReg.Rows[0]["GuarantorAdm"]).ToDecimal() +
                       (dtbReg.Rows[0]["DiscAdmGuarantor"]).ToDecimal() +
                       (dtbReg.Rows[0]["DiscAdmPatient"]).ToDecimal();
                }

                if (guarantor.GuarantorID == AppSession.Parameter.SelfGuarantor)
                    patient += admin;
                else
                {
                    if (guarantor.IsIncludeAdminValue ?? false)
                        grr += admin;
                    else
                        patient += admin;
                }

                if (isGlobalPlafond && (plafonAmount > 0))
                {
                    bool isBridging = false;

                    //if (AppSession.Parameter.IsBridgingBillingBpjs)
                    var isBridgingBillingBpjs = AppParameter.IsYes(AppParameter.ParameterItem.IsBridgingBillingBpjs);
                    if (isBridgingBillingBpjs)
                    {
                        var bridging = new GuarantorBridging();
                        bridging.Query.Where(bridging.Query.GuarantorID == guarantor.GuarantorID,
                                             bridging.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(),
                                                                              AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(),
                                                                              AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
                        if (bridging.Query.Load())
                        {
                            isBridging = true;
                        }
                    }

                    if (isBridging)
                    {
                        grr = plafonAmount;
                        patient = 0;
                    }
                    else
                    {
                        if (patient + grr >= plafonAmount)
                        {
                            patient = (patient + grr) - plafonAmount;
                            grr = plafonAmount;
                        }
                        else
                        {
                            grr = patient + grr;
                            patient = 0;
                        }
                    }
                }

                patientAmount = patient ?? 0;
                guarantorAmount = grr ?? 0;
            }
            public static void GetBillingTotal(string[] registrationNo, string srBusinessMethod, decimal plafonAmount, out decimal patientAmount, out decimal guarantorAmount,
                                               Guarantor guarantor, DateTime toDate, bool isGlobalPlafond)
            {
                toDate = toDate.AddDays(1);

                var collection = new CostCalculationCollection();

                var cc = new CostCalculationQuery("a");
                var py = new TransPaymentItemOrderQuery("b");
                var tr = new VwTransactionQuery("c");
                var pyib = new TransPaymentItemIntermBillQuery("d");

                var presc = new TransPrescriptionQuery("g");
                var queryReff = new CostCalculationQuery("h");
                var payReff = new TransPaymentItemOrderQuery("j");
                var payibReff = new TransPaymentItemIntermBillQuery("k");

                //db:29-09-2023 --> diremark, filter langsung dari view
                //var tc = new TransChargesQuery("l");


                cc.LeftJoin(py).On(
                    cc.TransactionNo == py.TransactionNo &&
                    cc.SequenceNo == py.SequenceNo &&
                    py.IsPaymentProceed == true &&
                    py.IsPaymentReturned == false
                    );
                cc.InnerJoin(tr).On(cc.TransactionNo == tr.TransactionNo && tr.TransactionDate < toDate &&
                                    cc.RegistrationNo.In(registrationNo));
                cc.LeftJoin(pyib).On(
                    cc.IntermBillNo == pyib.IntermBillNo &&
                    pyib.IsPaymentProceed == true &&
                    pyib.IsPaymentReturned == false
                    );

                cc.LeftJoin(presc).On(cc.TransactionNo == presc.PrescriptionNo);
                cc.LeftJoin(queryReff).On(
                    presc.ReferenceNo == queryReff.TransactionNo && cc.SequenceNo == queryReff.SequenceNo);
                cc.LeftJoin(payReff).On(
                                    queryReff.TransactionNo == payReff.TransactionNo &&
                                    queryReff.SequenceNo == payReff.SequenceNo &&
                                    payReff.IsPaymentProceed == true &&
                                    payReff.IsPaymentReturned == false
                                    );
                cc.LeftJoin(payibReff).On(
                    queryReff.IntermBillNo == payibReff.IntermBillNo &&
                    payibReff.IsPaymentProceed == true &&
                    payibReff.IsPaymentReturned == false
                    );
                //cc.LeftJoin(tc).On(
                //    tc.TransactionNo == cc.TransactionNo &&
                //    tc.RegistrationNo == cc.RegistrationNo
                //   );

                cc.Where(
                    cc.Or(
                        cc.ParentNo == string.Empty,
                        cc.ParentNo.IsNull()
                        ),
                    py.PaymentNo.IsNull(),
                    pyib.PaymentNo.IsNull(),
                    payReff.PaymentNo.IsNull(),
                    payibReff.PaymentNo.IsNull(),
                    //tc.PackageReferenceNo.IsNull() //db:29-09-2023 --> diremark, filter langsung dari view
                    tr.PackageReferenceNo == string.Empty
                    );
                collection.Load(cc);

                decimal? patient = collection.Sum(c => c.PatientAmount), grr = collection.Sum(c => c.GuarantorAmount);

                var regs = new RegistrationCollection();
                regs.Query.Where(regs.Query.RegistrationNo.In(registrationNo));
                regs.LoadAll();

                decimal admin = regs.Sum(r => (r.AdministrationAmount ?? 0));

                if (guarantor.GuarantorID == AppSession.Parameter.SelfGuarantor)
                    patient += admin;
                else
                {
                    if (guarantor.IsIncludeAdminValue ?? false)
                        grr += admin;
                    else
                        patient += admin;
                }

                if (isGlobalPlafond && (plafonAmount > 0))
                {
                    bool isBridging = false;

                    //if (AppSession.Parameter.IsBridgingBillingBpjs)
                    var isBridgingBillingBpjs = AppParameter.IsYes(AppParameter.ParameterItem.IsBridgingBillingBpjs);
                    if (isBridgingBillingBpjs)
                    {
                        var bridging = new GuarantorBridging();
                        bridging.Query.Where(bridging.Query.GuarantorID == guarantor.GuarantorID,
                                             bridging.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(),
                                                                              AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(),
                                                                              AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
                        if (bridging.Query.Load())
                        {
                            isBridging = true;
                        }
                    }

                    if (isBridging)
                    {
                        grr = plafonAmount;
                        patient = 0;
                    }
                    else
                    {
                        if (plafonAmount >= grr + patient)
                        {
                            grr = patient + grr;
                            patient = 0;
                        }
                        else
                        {
                            patient = grr + patient - plafonAmount;
                            grr = plafonAmount;
                        }
                    }
                }

                patientAmount = patient ?? 0;
                guarantorAmount = grr ?? 0;
            }

            public static void GetBillingTotalServiceUnit(string[] registrationNo, string srBusinessMethod, decimal plafonAmount, out decimal patientAmount,
                                                          out decimal guarantorAmount, Guarantor guarantor)
            {
                var collection = new CostCalculationCollection();
                var cc = new CostCalculationQuery("a");
                var py = new TransPaymentItemOrderQuery("b");
                var tr = new TransChargesQuery("c");
                var pyib = new TransPaymentItemIntermBillQuery("d");

                cc.LeftJoin(py).On(
                    cc.TransactionNo == py.TransactionNo &&
                    cc.SequenceNo == py.SequenceNo &&
                    py.IsPaymentProceed == true &&
                    py.IsPaymentReturned == false
                    );
                cc.InnerJoin(tr).On(cc.TransactionNo == tr.TransactionNo && cc.RegistrationNo.In(registrationNo));
                cc.LeftJoin(pyib).On(
                   cc.IntermBillNo == pyib.IntermBillNo &&
                   pyib.IsPaymentProceed == true &&
                   pyib.IsPaymentReturned == false
                   );
                cc.Where(
                    cc.Or(
                        cc.ParentNo == string.Empty,
                        cc.ParentNo.IsNull()
                        ),
                    py.PaymentNo.IsNull(), pyib.PaymentNo.IsNull(), tr.PackageReferenceNo.IsNull()
                    );
                collection.Load(cc);

                decimal? patient = collection.Sum(c => c.PatientAmount), grr = collection.Sum(c => c.GuarantorAmount);

                if (plafonAmount > 0)
                {
                    bool isBridging = false;

                    //if (AppSession.Parameter.IsBridgingBillingBpjs)
                    var isBridgingBillingBpjs = AppParameter.IsYes(AppParameter.ParameterItem.IsBridgingBillingBpjs);
                    if (isBridgingBillingBpjs)
                    {
                        var bridging = new GuarantorBridging();
                        bridging.Query.Where(bridging.Query.GuarantorID == guarantor.GuarantorID,
                                             bridging.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(),
                                                                              AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(),
                                                                              AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
                        if (bridging.Query.Load())
                        {
                            isBridging = true;
                        }
                    }

                    if (isBridging)
                    {
                        grr = plafonAmount;
                        patient = 0;
                    }
                    else
                    {
                        if (plafonAmount >= grr + patient)
                        {
                            grr = patient + grr;
                            patient = 0;
                        }
                        else
                        {
                            patient = grr + patient - plafonAmount;
                            grr = plafonAmount;
                        }
                    }
                }

                var regs = new RegistrationCollection();
                regs.Query.Where(regs.Query.RegistrationNo.In(registrationNo));
                regs.LoadAll();

                decimal admin = regs.Sum(r => (r.AdministrationAmount ?? 0) + (r.GuarantorAdm ?? 0));

                if (guarantor.GuarantorID == AppSession.Parameter.SelfGuarantor)
                    patient += admin;
                else
                {
                    if (guarantor.IsIncludeAdminValue ?? false)
                        grr += admin;
                    else
                        patient += admin;
                }

                patientAmount = patient ?? 0;
                guarantorAmount = grr ?? 0;
            }

            public static void GetBillingTotalPrescription(string[] registrationNo, string srBusinessMethod, decimal plafonAmount, out decimal patientAmount,
                                                           out decimal guarantorAmount, Guarantor guarantor, bool isGlobalPlafond)
            {
                var collection = new CostCalculationCollection();
                var cc = new CostCalculationQuery("a");
                var py = new TransPaymentItemOrderQuery("b");
                var tr = new TransPrescriptionQuery("c");

                var pyib = new TransPaymentItemIntermBillQuery("d");
                var queryReff = new CostCalculationQuery("h");
                var payReff = new TransPaymentItemOrderQuery("j");
                var payibReff = new TransPaymentItemIntermBillQuery("k");

                cc.LeftJoin(py).On(
                    cc.TransactionNo == py.TransactionNo &&
                    cc.SequenceNo == py.SequenceNo &&
                    py.IsPaymentProceed == true &&
                    py.IsPaymentReturned == false
                    );
                cc.InnerJoin(tr).On(cc.TransactionNo == tr.PrescriptionNo && cc.RegistrationNo.In(registrationNo));

                cc.LeftJoin(pyib).On(
                    cc.IntermBillNo == pyib.IntermBillNo &&
                    pyib.IsPaymentProceed == true &&
                    pyib.IsPaymentReturned == false
                    );

                cc.LeftJoin(queryReff).On(
                    tr.ReferenceNo == queryReff.TransactionNo && cc.SequenceNo == queryReff.SequenceNo);
                cc.LeftJoin(payReff).On(
                                    queryReff.TransactionNo == payReff.TransactionNo &&
                                    queryReff.SequenceNo == payReff.SequenceNo &&
                                    payReff.IsPaymentProceed == true &&
                                    payReff.IsPaymentReturned == false
                                    );
                cc.LeftJoin(payibReff).On(
                    queryReff.IntermBillNo == payibReff.IntermBillNo &&
                    payibReff.IsPaymentProceed == true &&
                    payibReff.IsPaymentReturned == false
                    );
                cc.Where(
                    cc.Or(
                        cc.ParentNo == string.Empty,
                        cc.ParentNo.IsNull()
                        ),
                    py.PaymentNo.IsNull(), pyib.PaymentNo.IsNull()
                    );
                collection.Load(cc);

                decimal? patient = collection.Sum(c => c.PatientAmount), grr = collection.Sum(c => c.GuarantorAmount);

                var regs = new RegistrationCollection();
                regs.Query.Where(regs.Query.RegistrationNo.In(registrationNo));
                regs.LoadAll();

                decimal admin = regs.Sum(r => (r.AdministrationAmount ?? 0) + (r.GuarantorAdm ?? 0));

                if (guarantor.GuarantorID == AppSession.Parameter.SelfGuarantor)
                    patient += admin;
                else
                {
                    if (guarantor.IsIncludeAdminValue ?? false)
                        grr += admin;
                    else
                        patient += admin;
                }

                if (isGlobalPlafond && (plafonAmount > 0))
                {
                    bool isBridging = false;

                    //if (AppSession.Parameter.IsBridgingBillingBpjs)
                    var isBridgingBillingBpjs = AppParameter.IsYes(AppParameter.ParameterItem.IsBridgingBillingBpjs);
                    if (isBridgingBillingBpjs)
                    {
                        var bridging = new GuarantorBridging();
                        bridging.Query.Where(bridging.Query.GuarantorID == guarantor.GuarantorID,
                                             bridging.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(),
                                                                              AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(),
                                                                              AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
                        if (bridging.Query.Load())
                        {
                            isBridging = true;
                        }
                    }

                    if (isBridging)
                    {
                        grr = plafonAmount;
                        patient = 0;
                    }
                    else
                    {
                        if (plafonAmount >= grr + patient)
                        {
                            grr = patient + grr;
                            patient = 0;
                        }
                        else
                        {
                            patient = grr + patient - plafonAmount;
                            grr = plafonAmount;
                        }
                    }
                }

                patientAmount = patient ?? 0;
                guarantorAmount = grr ?? 0;
            }

            public static decimal GetBillingTotalAllTransaction(string[] registrationNo)
            {

                return (GetBillingTotalAllTransactionInclAdm(registrationNo, false));
            }

            public static decimal GetBillingTotalAllTransactionInclAdm(string[] registrationNo, bool inclAdm)
            {
                return Temiang.Avicenna.BusinessObject.CostCalculationCollection.GetBillingTotalAllTransactionInclAdm(registrationNo, inclAdm);
            }

            public static decimal GetBillingTotalAllTransactionIntermbillInclAdm(string[] intermbillNo, bool inclAdm)
            {
                return Temiang.Avicenna.BusinessObject.CostCalculationCollection.GetBillingTotalAllTransactionIntermbillInclAdm(intermbillNo, inclAdm);
            }

            public static decimal GetAdminValueMax(string guarantorID, string srRegistrationType)
            {
                var guarantor = new Guarantor();
                guarantor.LoadByPrimaryKey(guarantorID);

                if (srRegistrationType == AppConstant.RegistrationType.InPatient)
                    return guarantor.AdminAmountLimit ?? 0;

                return guarantor.AdminAmountLimitOp ?? 0;
            }

            public static decimal GetAdminValue(string guarantorID, decimal total, string srRegistrationType)
            {
                if (total == 0)
                    return 0;

                var guarantor = new Guarantor();
                guarantor.LoadByPrimaryKey(guarantorID);

                decimal admin = 0;
                if (srRegistrationType == AppConstant.RegistrationType.InPatient)
                {
                    admin = ((guarantor.AdminPercentage ?? 0) / 100) * total;

                    if (guarantor.AdminValueMinimum > 0 && admin < (guarantor.AdminValueMinimum ?? 0))
                        admin = guarantor.AdminValueMinimum ?? 0;
                    else if (guarantor.AdminAmountLimit > 0 && admin > guarantor.AdminAmountLimit)
                        admin = guarantor.AdminAmountLimit ?? 0;
                }
                else
                {
                    admin = ((guarantor.AdminPercentageOp ?? 0) / 100) * total;

                    if (guarantor.AdminValueMinimumOp > 0 && admin < (guarantor.AdminValueMinimumOp ?? 0))
                        admin = guarantor.AdminValueMinimumOp ?? 0;
                    else if (guarantor.AdminAmountLimitOp > 0 && admin > guarantor.AdminAmountLimitOp)
                        admin = guarantor.AdminAmountLimitOp ?? 0;
                }

                return admin;
            }

            public static decimal GetAdminValue(string guarantorID, CostCalculationCollection cost, string srRegistrationType)
            {
                if (cost.Count > 0)
                {
                    var g = new Guarantor();
                    g.LoadByPrimaryKey(guarantorID);

                    if (!AppSession.Parameter.IsAdminCalcIncludeItemProduct)
                    {
                        var items = new VwItemWithAdminCalculationCollection();
                        items.Query.Where(items.Query.ItemID.In(cost.Select(c => c.ItemID)));
                        items.Query.Load();

                        if (items.Any())
                        {
                            var cc = cost.Where(c => items.Select(i => i.ItemID)
                                                          .Contains(c.ItemID));
                            if (cc.Any())
                            {
                                if (g.IsAdminCalcBeforeDiscount ?? false)
                                    return GetAdminValue(guarantorID, cc.Sum(c => c.GuarantorAmount + c.PatientAmount + c.DiscountAmount + (c.DiscountAmount2 ?? 0)) ?? 0, srRegistrationType);
                                return GetAdminValue(guarantorID, cc.Sum(c => c.GuarantorAmount + c.PatientAmount) ?? 0, srRegistrationType);
                            }
                        }
                    }
                    else
                    {
                        var items = new VwItemServiceAndProductWithAdminCalculationCollection();
                        items.Query.Where(items.Query.ItemID.In(cost.Select(c => c.ItemID)));
                        items.Query.Load();

                        if (items.Any())
                        {
                            var cc = cost.Where(c => items.Select(i => i.ItemID)
                                                          .Contains(c.ItemID));
                            if (cc.Any())
                            {
                                if (g.IsAdminCalcBeforeDiscount ?? false)
                                    return GetAdminValue(guarantorID, cc.Sum(c => c.GuarantorAmount + c.PatientAmount + c.DiscountAmount + (c.DiscountAmount2 ?? 0)) ?? 0, srRegistrationType);
                                return GetAdminValue(guarantorID, cc.Sum(c => c.GuarantorAmount + c.PatientAmount) ?? 0, srRegistrationType);
                            }
                        }
                    }
                }

                return 0;
            }

            public static decimal GetAdminValue(string guarantorID, CostCalculationCollection cost, string srRegistrationType, bool isToPatient)
            {
                var g = new Guarantor();
                g.LoadByPrimaryKey(guarantorID);
                string guarantorType = g.SRGuarantorType;

                if (cost.Count > 0)
                {
                    if (!AppSession.Parameter.IsAdminCalcIncludeItemProduct)
                    {
                        var items = new VwItemWithAdminCalculationCollection();
                        items.Query.Where(items.Query.ItemID.In(cost.Select(c => c.ItemID)));
                        items.Query.Load();

                        if (items.Any())
                        {
                            var cc = cost.Where(c => items.Select(i => i.ItemID)
                                                          .Contains(c.ItemID));
                            if (cc.Any())
                            {
                                if (g.IsAdminCalcBeforeDiscount ?? false)
                                {
                                    if (guarantorType == AppSession.Parameter.GuarantorTypeSelf)
                                    {
                                        if (isToPatient)
                                            return GetAdminValue(guarantorID, cc.Sum(c => c.PatientAmount + c.DiscountAmount) ?? 0, srRegistrationType);
                                        return GetAdminValue(guarantorID, cc.Sum(c => c.GuarantorAmount) ?? 0, srRegistrationType);
                                    }

                                    //-- guarantorType != AppSession.Parameter.GuarantorTypeSelf
                                    if (isToPatient)
                                        return GetAdminValue(guarantorID, cc.Sum(c => c.PatientAmount + (c.DiscountAmount2 ?? 0)) ?? 0, srRegistrationType);
                                    return GetAdminValue(guarantorID, cc.Sum(c => c.GuarantorAmount + c.DiscountAmount) ?? 0, srRegistrationType);
                                }

                                //-- AppSession.Parameter.IsAdminCalcBeforeDiscount == "No"
                                if (isToPatient)
                                    return GetAdminValue(guarantorID, cc.Sum(c => c.PatientAmount) ?? 0, srRegistrationType);
                                return GetAdminValue(guarantorID, cc.Sum(c => c.GuarantorAmount) ?? 0, srRegistrationType);
                            }
                        }
                    }
                    else
                    {
                        var items = new VwItemServiceAndProductWithAdminCalculationCollection();
                        items.Query.Where(items.Query.ItemID.In(cost.Select(c => c.ItemID)));
                        items.Query.Load();

                        if (items.Any())
                        {
                            var cc = cost.Where(c => items.Select(i => i.ItemID)
                                                          .Contains(c.ItemID));
                            if (cc.Any())
                            {
                                if (g.IsAdminCalcBeforeDiscount ?? false)
                                {
                                    if (guarantorType == AppSession.Parameter.GuarantorTypeSelf)
                                    {
                                        if (isToPatient)
                                            return GetAdminValue(guarantorID, cc.Sum(c => c.PatientAmount + c.DiscountAmount) ?? 0, srRegistrationType);
                                        return GetAdminValue(guarantorID, cc.Sum(c => c.GuarantorAmount) ?? 0, srRegistrationType);
                                    }

                                    //-- guarantorType != AppSession.Parameter.GuarantorTypeSelf
                                    if (isToPatient)
                                        return GetAdminValue(guarantorID, cc.Sum(c => c.PatientAmount + (c.DiscountAmount2 ?? 0)) ?? 0, srRegistrationType);
                                    return GetAdminValue(guarantorID, cc.Sum(c => c.GuarantorAmount + c.DiscountAmount) ?? 0, srRegistrationType);
                                }

                                //-- AppSession.Parameter.IsAdminCalcBeforeDiscount == "No"
                                if (isToPatient)
                                    return GetAdminValue(guarantorID, cc.Sum(c => c.PatientAmount) ?? 0, srRegistrationType);
                                return GetAdminValue(guarantorID, cc.Sum(c => c.GuarantorAmount) ?? 0, srRegistrationType);
                            }
                        }
                    }
                }

                return 0;
            }

            public static void GetTotalTemporaryBill(string[] registrationNo, out decimal billingAmount)
            {
                billingAmount = 0;

                var tcq = new TransChargesQuery("tc");
                var tciq = new TransChargesItemQuery("tci");
                tcq.InnerJoin(tciq).On(tciq.TransactionNo == tcq.TransactionNo);
                tcq.Where(tcq.RegistrationNo.In(registrationNo), tcq.IsVoid == false, tciq.IsVoid == false);
                tcq.Select(@"<ISNULL(SUM((tci.ChargeQuantity * tci.Price) - tci.DiscountAmount + tci.CitoAmount), 0) AS 'Total'>");
                DataTable tcdtb = tcq.LoadDataTable();
                if (tcdtb.Rows.Count > 0)
                    billingAmount += Convert.ToDecimal(tcdtb.Rows[0]["Total"]);

                var tpq = new TransPrescriptionQuery("tp");
                var tpiq = new TransPrescriptionItemQuery("tpi");
                tpq.InnerJoin(tpiq).On(tpiq.PrescriptionNo == tpq.PrescriptionNo);
                tpq.Where(tpq.RegistrationNo.In(registrationNo), tpq.IsVoid == false, tpiq.IsVoid == false);
                tpq.Select(@"<ISNULL(SUM(tpi.LineAmount), 0) AS 'Total'>");
                DataTable tpdtb = tpq.LoadDataTable();
                if (tpdtb.Rows.Count > 0)
                    billingAmount += Convert.ToDecimal(tpdtb.Rows[0]["Total"]);

            }

            //public static decimal InsertToCostCalculation(string registrationNo)
            //{
            //    var reg = new Registration();
            //    reg.LoadByPrimaryKey(registrationNo);

            //    var costCalculations = new CostCalculationCollection();

            //    //transcharges - induk
            //    var tciQ = new TransChargesItemQuery("tci");
            //    var tcQ = new TransChargesQuery("tc");
            //    var ccQ = new CostCalculationQuery("cc");

            //    tciQ.InnerJoin(tcQ).On(tciQ.TransactionNo == tcQ.TransactionNo && tcQ.RegistrationNo == registrationNo)
            //        .LeftJoin(ccQ).On(tciQ.TransactionNo == ccQ.TransactionNo && tciQ.SequenceNo == ccQ.SequenceNo);
            //    tciQ.Where(tciQ.IsBillProceed == true, tciQ.IsVoid == false, ccQ.PatientAmount.IsNull());

            //    tciQ.Select(tcQ.RegistrationNo, tcQ.TransactionDate, tciQ.TransactionNo, tciQ.SequenceNo, tciQ.ItemID, tciQ.ChargeQuantity,
            //                tciQ.Price, tciQ.DiscountAmount, tciQ.CitoAmount, tciQ.IsCito, tciQ.IsCitoInPercent,
            //                tciQ.BasicCitoAmount, tcQ.IsRoomIn, tciQ.IsItemRoom, tcQ.TariffDiscountForRoomIn, tciQ.IsPackage, tciQ.ParentNo, tciQ.IsVariable.Coalesce("0"));

            //    DataTable dtbtci = tciQ.LoadDataTable();

            //    //transcharges - merge
            //    tciQ = new TransChargesItemQuery("tci");
            //    tcQ = new TransChargesQuery("tc");
            //    ccQ = new CostCalculationQuery("cc");
            //    var mbQ = new MergeBillingQuery("mb");

            //    tciQ.InnerJoin(tcQ).On(tciQ.TransactionNo == tcQ.TransactionNo)
            //        .InnerJoin(mbQ).On(tcQ.RegistrationNo == mbQ.FromRegistrationNo && mbQ.FromRegistrationNo == registrationNo)
            //        .LeftJoin(ccQ).On(tciQ.TransactionNo == ccQ.TransactionNo && tciQ.SequenceNo == ccQ.SequenceNo);
            //    tciQ.Where(tciQ.IsBillProceed == true, tciQ.IsVoid == false, ccQ.PatientAmount.IsNull());

            //    tciQ.Select(tcQ.RegistrationNo, tcQ.TransactionDate, tciQ.TransactionNo, tciQ.SequenceNo, tciQ.ItemID, tciQ.ChargeQuantity,
            //                tciQ.Price, tciQ.DiscountAmount, tciQ.CitoAmount, tciQ.IsCito, tciQ.IsCitoInPercent,
            //                tciQ.BasicCitoAmount, tcQ.IsRoomIn, tciQ.IsItemRoom, tcQ.TariffDiscountForRoomIn, tciQ.IsPackage, tciQ.ParentNo, tciQ.IsVariable.Coalesce("0"), tciQ.ItemConditionRuleID);

            //    DataTable dtbtci2 = tciQ.LoadDataTable();

            //    dtbtci.Merge(dtbtci2);

            //    foreach (DataRow row in dtbtci.Rows)
            //    {
            //        DataTable tblCovered = GetCoveredItems(registrationNo, reg.GuarantorID, reg.CoverageClassID,
            //                                               row["ItemID"].ToString(),
            //                                               Convert.ToDateTime(row["TransactionDate"]), false);

            //        var transChargesItemsDtComp = new TransChargesItemCompCollection();
            //        transChargesItemsDtComp.Query.Where(
            //            transChargesItemsDtComp.Query.TransactionNo == row["TransactionNo"].ToString(),
            //            transChargesItemsDtComp.Query.SequenceNo == row["SequenceNo"].ToString());
            //        transChargesItemsDtComp.LoadAll();

            //        decimal? total = ((Convert.ToDecimal(row["ChargeQuantity"]) * Convert.ToDecimal(row["Price"])) - Convert.ToDecimal(row["DiscountAmount"])) + Convert.ToDecimal(row["CitoAmount"]);
            //        decimal? qty = Convert.ToDecimal(row["ChargeQuantity"]);

            //        var calc = new CostCalculation(reg.GuarantorID, row["ItemID"].ToString(), total ?? 0, tblCovered, qty ?? 0,
            //                                                          Convert.ToBoolean(row["IsCito"]),
            //                                                          Convert.ToBoolean(row["IsCitoInPercent"]),
            //                                                          Convert.ToDecimal(row["BasicCitoAmount"]), 
            //                                                          Convert.ToDecimal(row["Price"]),
            //                                                          Convert.ToBoolean(row["IsRoomIn"]), 
            //                                                          Convert.ToBoolean(row["IsItemRoom"]),
            //                                                          Convert.ToDecimal(row["TariffDiscountForRoomIn"]), 
            //                                                          Convert.ToDecimal(row["DiscountAmount"]), false,
            //                                                          row["SRItemConditionRule"].ToString(), Convert.ToDateTime(row["TransactionDate"]), Convert.ToBoolean(row["IsVariable"]));

            //        var cost = costCalculations.AddNew();
            //        cost.RegistrationNo = row["RegistrationNo"].ToString();
            //        cost.TransactionNo = row["TransactionNo"].ToString();
            //        cost.SequenceNo = row["SequenceNo"].ToString();
            //        cost.ItemID = row["ItemID"].ToString();
            //        cost.PatientAmount = calc.PatientAmount;
            //        cost.GuarantorAmount = calc.GuarantorAmount;
            //        cost.DiscountAmount = Convert.ToDecimal(row["DiscountAmount"]);
            //        cost.IsPackage = Convert.ToBoolean(row["IsPackage"]);
            //        cost.ParentNo = row["ParentNo"].ToString();
            //        cost.ParamedicAmount = qty * transChargesItemsDtComp.Where(comp => comp.TransactionNo == row["TransactionNo"].ToString() &&
            //                                                                                                    comp.SequenceNo == row["SequenceNo"].ToString() &&
            //                                                                                                    !string.IsNullOrEmpty(comp.ParamedicID))
            //                                                                                     .Sum(comp => comp.Price - comp.DiscountAmount);
            //        cost.LastUpdateDateTime = DateTime.Now;
            //        cost.LastUpdateByUserID = "SYSTEM";
            //    }

            //    //prescription - induk
            //    var tpiQ = new TransPrescriptionItemQuery("tpi");
            //    var tpQ = new TransPrescriptionQuery("tp");
            //    ccQ = new CostCalculationQuery("cc");

            //    tpiQ.InnerJoin(tpQ).On(tpiQ.PrescriptionNo == tpQ.PrescriptionNo && tpQ.RegistrationNo == registrationNo)
            //        .LeftJoin(ccQ).On(tpiQ.PrescriptionNo == ccQ.TransactionNo && tpiQ.SequenceNo == ccQ.SequenceNo);
            //    tpiQ.Where(tpiQ.IsBillProceed == true, tpiQ.IsVoid == false, ccQ.PatientAmount.IsNull());

            //    tpiQ.Select(tpQ.RegistrationNo, tpQ.PrescriptionDate, tpiQ.PrescriptionNo, tpiQ.SequenceNo, tpiQ.ItemID,
            //                tpiQ.ItemInterventionID, tpiQ.ResultQty, tpiQ.Price, tpiQ.DiscountAmount, tpiQ.RecipeAmount,
            //                tpiQ.EmbalaceAmount, tpiQ.SweetenerAmount, tpiQ.LineAmount);

            //    DataTable dtbtpi = tpiQ.LoadDataTable();

            //    //prescription - merge
            //    tpiQ = new TransPrescriptionItemQuery("tpi");
            //    tpQ = new TransPrescriptionQuery("tp");
            //    ccQ = new CostCalculationQuery("cc");
            //    mbQ = new MergeBillingQuery("mb");

            //    tpiQ.InnerJoin(tpQ).On(tpiQ.PrescriptionNo == tpQ.PrescriptionNo && tpQ.RegistrationNo == registrationNo)
            //        .InnerJoin(mbQ).On(tpQ.RegistrationNo == mbQ.FromRegistrationNo && mbQ.FromRegistrationNo == registrationNo)
            //        .LeftJoin(ccQ).On(tpiQ.PrescriptionNo == ccQ.TransactionNo && tpiQ.SequenceNo == ccQ.SequenceNo);
            //    tpiQ.Where(tpiQ.IsBillProceed == true, tpiQ.IsVoid == false, ccQ.PatientAmount.IsNull());

            //    tpiQ.Select(tpQ.RegistrationNo, tpQ.PrescriptionDate, tpiQ.PrescriptionNo, tpiQ.SequenceNo, tpiQ.ItemID,
            //                tpiQ.ItemInterventionID, tpiQ.ResultQty, tpiQ.Price, tpiQ.DiscountAmount, tpiQ.RecipeAmount,
            //                tpiQ.EmbalaceAmount, tpiQ.SweetenerAmount, tpiQ.LineAmount);

            //    DataTable dtbtpi2 = tpiQ.LoadDataTable();
            //    dtbtpi.Merge(dtbtpi2);

            //    foreach (DataRow row in dtbtpi.Rows)
            //    {
            //        DataTable tblCovered = GetCoveredItems(registrationNo, reg.GuarantorID,
            //                                                                  reg.CoverageClassID, string.IsNullOrEmpty(row["ItemInterventionID"].ToString()) ? row["ItemID"].ToString() : row["ItemInterventionID"].ToString(),
            //                                                                  Convert.ToDateTime(row["PrescriptionDate"]), true);
            //        decimal resultQty = Convert.ToDecimal(row["ResultQty"]);
            //        decimal recipeAmount = Convert.ToDecimal(row["RecipeAmount"]) + Convert.ToDecimal(row["EmbalaceAmount"]) + Convert.ToDecimal(row["SweetenerAmount"]);
            //        var lineAmt = (((Math.Abs(resultQty) * Convert.ToDecimal(row["Price"])) - Convert.ToDecimal(row["DiscountAmount"])) + recipeAmount);
            //        lineAmt = Rounding(Convert.ToDecimal(lineAmt), AppEnum.RoundingType.Prescription);


            //        var calc = new CostCalculation(reg.GuarantorID, reg.IsGlobalPlafond ?? false,
            //            string.IsNullOrEmpty(row["ItemInterventionID"].ToString()) ? row["ItemID"].ToString() : row["ItemInterventionID"].ToString(), Math.Abs(lineAmt),
            //            tblCovered, Math.Abs(resultQty), Convert.ToDecimal(row["Price"]), recipeAmount, Convert.ToDecimal(row["DiscountAmount"]));

            //        var cost = costCalculations.AddNew();
            //        cost.RegistrationNo = row["RegistrationNo"].ToString();
            //        cost.TransactionNo = row["PrescriptionNo"].ToString();
            //        cost.SequenceNo = row["SequenceNo"].ToString();
            //        cost.ItemID = string.IsNullOrEmpty(row["ItemInterventionID"].ToString()) ? row["ItemID"].ToString() : row["ItemInterventionID"].ToString();
            //        cost.PatientAmount = resultQty < 0 ? 0 - calc.PatientAmount : calc.PatientAmount;
            //        cost.GuarantorAmount = resultQty < 0 ? 0 - calc.GuarantorAmount : calc.GuarantorAmount;
            //        cost.DiscountAmount = resultQty < 0 ? 0 - Convert.ToDecimal(row["DiscountAmount"]) : Convert.ToDecimal(row["DiscountAmount"]);
            //        cost.ParamedicAmount = 0;
            //        cost.LastUpdateDateTime = DateTime.Now;
            //        cost.LastUpdateByUserID = "SYSTEM";
            //    }

            //    costCalculations.Save();

            //    return 0;
            //}
        }
    }
}
