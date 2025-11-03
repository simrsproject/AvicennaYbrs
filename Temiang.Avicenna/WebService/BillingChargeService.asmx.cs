using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for BillingChargeService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class BillingChargeService : System.Web.Services.WebService
    {

        #region Plafond Balance
        [WebMethod]
        public string PlafondProgress(string regNo)
        {
            var usedInPercent = PlafondValueUsedInPercent(regNo);
            if (usedInPercent == 0) return string.Empty;
            return string.Format(@"<div style='background:black;width: 100%; padding: 1px'>
                            <div style='background:{0};color:Black;width: {1}%'>{2:n2}%</div>
                        </div>", usedInPercent > 100 ? "red" : usedInPercent > 75 ? "yellow" : "green", usedInPercent > 100 ? 100 : usedInPercent, usedInPercent);

        }

        [WebMethod]
        public string RemainingAmountConfirm(string patientId, string beforeRegNo)
        {
            // Get last registration
            var regColl = new RegistrationCollection();
            var qr = new RegistrationQuery("a");
            var mb = new MergeBillingQuery("b");
            qr.LeftJoin(mb).On(mb.RegistrationNo == qr.RegistrationNo);
            qr.Where(qr.PatientID == patientId, qr.IsVoid == false, qr.Or(mb.FromRegistrationNo == string.Empty, mb.FromRegistrationNo.IsNull()));

            if (!string.IsNullOrEmpty(beforeRegNo))
            {
                var regNos = Helper.MergeBilling.GetMergeRegistration(beforeRegNo);
                qr.Where(qr.RegistrationNo.NotIn(regNos));
            }

            qr.OrderBy(qr.RegistrationDate.Descending, qr.RegistrationTime.Descending);
            qr.es.Top = 10;

            if (!regColl.Load(qr) || regColl.Count == 0) return string.Empty;

            var strb = new StringBuilder();
            var i = 0;
            foreach (Registration reg in regColl)
            {
                var isReferer = false;
                foreach (Registration search in regColl)
                {
                    if (reg.RegistrationNo == search.FromRegistrationNo || (!string.IsNullOrEmpty(beforeRegNo) && reg.FromRegistrationNo == beforeRegNo))
                    {
                        isReferer = true;
                        break;
                    }
                }

                if (!isReferer)
                {
                    
                    var amount = RemainingAmount(reg);
                    if (amount > 0)
                    {
                        strb.AppendLine(string.Empty);
                        strb.AppendFormat("Reg No: {0}, Date: {1}, Amount: {2:n2}", reg.RegistrationNo,
                            Convert.ToDateTime(reg.RegistrationDate).ToString(AppConstant.DisplayFormat.DateLong),
                            amount);
                        i++;
                    }
                }

                if (i > 3) break;
            }

            return strb.ToString();
        }

        private double RemainingAmount(Registration reg)
        {
            var regNo = reg.RegistrationNo;
            double remainingAmountPatient = 0;
            double totalPaymentAmountPatient = 0;
            double remainingAmountGuarantor = 0;
            double totalPaymentAmountGuarantor = 0;

            var guarantor = new Guarantor();
            guarantor.LoadByPrimaryKey(reg.GuarantorID);


            var gQ = new GuarantorQuery();
            gQ.Select(gQ.GuarantorID, gQ.GuarantorName);
            gQ.Where(gQ.GuarantorID == reg.GuarantorID);


            string[] patientParam = new string[2], registrationNos = Helper.MergeBilling.GetMergeRegistration(regNo);
            patientParam.SetValue(AppSession.Parameter.PaymentTypePayment, 0);
            patientParam.SetValue(AppSession.Parameter.PaymentTypePersonalAR, 1);

            var discPatient = (double)Helper.Payment.GetPaymentDiscount(registrationNos, false);
            var discGuarantor = (double)Helper.Payment.GetPaymentDiscount(registrationNos, true);

            decimal tpayment = Helper.Payment.GetTotalPayment(registrationNos, true, patientParam);
            decimal treturn = Helper.Payment.GetTotalPayment(registrationNos, false);
            totalPaymentAmountPatient = (double)tpayment + (double)treturn;
            totalPaymentAmountGuarantor =
                (double)Helper.Payment.GetTotalPayment(registrationNos, true, AppSession.Parameter.PaymentTypeCorporateAR) +
                (double)Helper.Payment.GetTotalPayment(registrationNos, true, AppSession.Parameter.PaymentTypeSaldoAR);

            decimal tpatient, tguarantor;
            decimal selisih = 0;
            bool isBridging = false;

            var bridging = new GuarantorBridging();
            bridging.Query.Where(bridging.Query.GuarantorID == reg.GuarantorID,
                                 bridging.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(),
                                                                  AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(),
                                                                  AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
            if (bridging.Query.Load())
            {
                isBridging = true;
                if (reg.CoverageClassID != reg.ChargeClassID || reg.GuarantorID == AppSession.Parameter.SelfGuarantor)
                {
                    var cov = new RegistrationCoverageDetail();
                    cov.Query.Select(cov.Query.CalculatedAmount.Sum());
                    cov.Query.Where(cov.Query.RegistrationNo == reg.RegistrationNo);
                    if (cov.Query.Load()) selisih = cov.CalculatedAmount ?? 0;
                    else
                    {
                        if ((reg.PlavonAmount2 ?? 0) > 0)
                        {
                            var class1 = new Class();
                            class1.LoadByPrimaryKey(reg.CoverageClassID);

                            var asri1 = new AppStandardReferenceItem();
                            asri1.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class1.SRClassRL);

                            var class2 = new Class();
                            class2.LoadByPrimaryKey(reg.ChargeClassID);

                            var asri2 = new AppStandardReferenceItem();
                            asri2.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class2.SRClassRL);

                            if (asri2.Note.ToInt() < asri1.Note.ToInt()) selisih = (reg.PlavonAmount2 ?? 0) - (reg.PlavonAmount ?? 0);
                        }
                    }
                }
            }

            

            var cob = new RegistrationGuarantorCollection();
            cob.Query.Where(cob.Query.RegistrationNo == regNo);
            cob.LoadAll();
            decimal cobPlafond = cob.Sum(c => (c.PlafondAmount ?? 0));

            Helper.CostCalculation.GetBillingTotal(registrationNos, reg.SRBussinesMethod, (reg.PlavonAmount ?? 0) + cobPlafond, out tpatient, out tguarantor,
                                                   guarantor, reg.IsGlobalPlafond ?? false);

            remainingAmountGuarantor = (double)tguarantor - totalPaymentAmountGuarantor - discGuarantor;

            //txtPlafonAmount.Value = (double)((reg.PlavonAmount ?? 0) + cobPlafond);
            //txtAdminCal.Value = (double)(reg.AdministrationAmount ?? 0);
            //txtPatientAdm.Value = (double)(reg.PatientAdm ?? 0);
            //txtGuarantorAdm.Value = (double)(reg.GuarantorAdm ?? 0);
            //txtDownPaymentAmount.Value = (double)(Helper.Payment.GetTotalDownPayment(registrationNos) - Helper.Payment.GetTotalDownPaymentReturn(registrationNos));
            //txtDiscountAmount.Value = (double)Helper.Payment.GetTotalPaymentDiscount(registrationNos);

            remainingAmountPatient = (double)tpatient - totalPaymentAmountPatient - discPatient;
 

            if (isBridging)
            {
                if (reg.CoverageClassID != reg.ChargeClassID || reg.GuarantorID == AppSession.Parameter.SelfGuarantor)
                {
                    if ((reg.PlavonAmount2 ?? 0) > 0)
                        remainingAmountPatient = (((reg.PlavonAmount2 ?? 0) == 0) ? (double)tpatient : (double)selisih) - totalPaymentAmountPatient - discPatient;
                    else
                    {
                        remainingAmountPatient = (selisih > 0 ? (double)selisih : (double)tpatient) - totalPaymentAmountPatient - discPatient;
                    }
                }
            }
            return remainingAmountPatient + remainingAmountGuarantor;
        }


        private decimal PlafondValueUsedInPercent(string regNo)
        {
            var reg = Registration(regNo);
            var additionalPlafond = AdditionalPlafond(regNo);
            //decimal additionalPlafond = 0;

            var plafondAmt = (decimal)(reg.PlavonAmount == null ? 0 : reg.PlavonAmount);
            var guarantorAskesID = AppParameter.GetParameterValue(AppParameter.ParameterItem.GuarantorAskesID);
            if (guarantorAskesID.Contains(reg.GuarantorID) && plafondAmt == 0)
                plafondAmt = (decimal)(reg.ApproximatePlafondAmount == null ? 0 : reg.ApproximatePlafondAmount);

            var totalPlafond = plafondAmt + additionalPlafond;
            if (totalPlafond == 0) return 0;

            var mergeRegistrationNos = Helper.MergeBilling.GetMergeRegistration(regNo);

            var plafonUsedPercent = (TotalGuarantorAndRemainingPatientAmount(reg, mergeRegistrationNos, additionalPlafond) / totalPlafond) * (decimal)100;
            return plafonUsedPercent;
        }

        private Registration Registration(string regNo)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(regNo);
            return reg;
        }


        private decimal AdditionalPlafond(string regno)
        {
            decimal cobPlafond = 0;
            var cob = new RegistrationGuarantorCollection();
            cob.Query.Where(cob.Query.RegistrationNo == regno);
            cob.LoadAll();
            cobPlafond = cob.Sum(c => (c.PlafondAmount ?? 0));
            return cobPlafond;
        }
        private decimal TotalGuarantorAndRemainingPatientAmount(Registration reg, string[] mergeRegistrationNos, decimal additionalPlafond)
        {
            decimal tpatient;
            decimal tguarantor;
            var guarantor = new Guarantor();
            guarantor.LoadByPrimaryKey(reg.GuarantorID);

            decimal plafondAmt = reg.PlavonAmount ?? 0;
            var guarantorAskesID = AppParameter.GetParameterValue(AppParameter.ParameterItem.GuarantorAskesID);
            if (guarantorAskesID.Contains(reg.GuarantorID) && plafondAmt == 0)
                plafondAmt = reg.ApproximatePlafondAmount ?? 0;


            Helper.CostCalculation.GetBillingTotal2(mergeRegistrationNos, reg.SRBussinesMethod, plafondAmt + additionalPlafond, out tpatient, out tguarantor,
                                                   guarantor, reg.IsGlobalPlafond ?? false);
            return tpatient + tguarantor;
        }



        #endregion

        #region BillingProcess

        public static void BillingProcess(string regNo, string itemId, string seqNo, decimal chargeQty, string tType, bool isCasemixApproved)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(regNo);

            var guarantorId = reg.GuarantorID;
            if (guarantorId == AppSession.Parameter.SelfGuarantor)
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                if (!string.IsNullOrEmpty(pat.MemberID))
                    guarantorId = pat.MemberID;
            }

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(guarantorId);
            var tariffDate = grr.TariffCalculationMethod == 1
                ? reg.RegistrationDate.Value.Date
                : (new DateTime()).NowAtSqlServer().Date;

            #region header
            var chargesHd = new TransCharges();

            var number = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date,
                tType == "ds" ? AppEnum.AutoNumber.AncillaryServiceNo : AppEnum.AutoNumber.TransactionNo, string.Empty,
                AppSession.UserLogin.UserID);
            chargesHd.TransactionNo = number.LastCompleteNumber;

            // number
            number.LastCompleteNumber = chargesHd.TransactionNo;
            number.Save();

            chargesHd.RegistrationNo = regNo;
            chargesHd.TransactionDate = (new DateTime()).NowAtSqlServer().Date;
            chargesHd.ReferenceNo = string.Empty;
            chargesHd.FromServiceUnitID = reg.ServiceUnitID;
            chargesHd.ToServiceUnitID = tType == "ds" ? AppSession.Parameter.ServiceUnitBloodBankID : reg.ServiceUnitID;
            chargesHd.ClassID = reg.ChargeClassID;
            chargesHd.RoomID = reg.RoomID;
            chargesHd.BedID = reg.BedID;
            chargesHd.DueDate = (new DateTime()).NowAtSqlServer().Date;
            chargesHd.SRShift = BusinessObject.Registration.GetShiftID();
            chargesHd.SRItemType = string.Empty;
            chargesHd.IsProceed = tType == "ds";
            chargesHd.IsBillProceed = true;
            chargesHd.IsApproved = true;
            chargesHd.IsVoid = false;
            chargesHd.IsOrder = tType == "ds";
            chargesHd.IsCorrection = false;
            chargesHd.IsClusterAssign = false;
            chargesHd.IsAutoBillTransaction = true;
            chargesHd.Notes = string.Empty;
            chargesHd.IsRoomIn = reg.IsRoomIn;
            var room = new ServiceRoom();
            room.LoadByPrimaryKey(chargesHd.RoomID);
            chargesHd.TariffDiscountForRoomIn = room.TariffDiscountForRoomIn ?? 0;
            chargesHd.SurgicalPackageID = string.Empty;

            chargesHd.LastUpdateByUserID = AppSession.UserLogin.UserID;
            chargesHd.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            chargesHd.CreatedByUserID = AppSession.UserLogin.UserID;
            chargesHd.CreatedDateTime = (new DateTime()).NowAtSqlServer();
            #endregion

            #region detail
            var chargesDt = new TransChargesItem();

            chargesDt.TransactionNo = chargesHd.TransactionNo;
            chargesDt.SequenceNo = seqNo;
            chargesDt.ReferenceNo = string.Empty;
            chargesDt.ReferenceSequenceNo = string.Empty;
            chargesDt.ItemID = itemId;
            chargesDt.ChargeClassID = reg.ChargeClassID;
            chargesDt.ParamedicID = reg.ParamedicID;
            chargesDt.IsItemRoom = true;
            chargesDt.TariffDate = tariffDate;

            ItemTariff tariff = (Helper.Tariff.GetItemTariff(tariffDate,
                                                             grr.SRTariffType,
                                                             chargesHd.ClassID, chargesHd.ClassID, chargesDt.ItemID,
                                                             guarantorId, false, reg.SRRegistrationType) ??
                                 Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType,
                                                             AppSession.Parameter.DefaultTariffClass, chargesHd.ClassID,
                                                             chargesDt.ItemID, guarantorId, false, reg.SRRegistrationType)) ??
                                (Helper.Tariff.GetItemTariff(tariffDate,
                                                            AppSession.Parameter.DefaultTariffType,
                                                            chargesHd.ClassID, chargesHd.ClassID,
                                                            chargesDt.ItemID,
                                                            guarantorId, false, reg.SRRegistrationType) ??
                                Helper.Tariff.GetItemTariff(tariffDate,
                                                            AppSession.Parameter.DefaultTariffType,
                                                            AppSession.Parameter.DefaultTariffClass, chargesHd.ClassID,
                                                            chargesDt.ItemID, guarantorId, false, reg.SRRegistrationType));

            if (tariff != null)
            {
                chargesDt.IsAdminCalculation = tariff.IsAdminCalculation ?? false;
                chargesDt.Price = tariff.Price ?? 0;
                if (chargesHd.IsRoomIn == true)
                    chargesDt.Price = chargesDt.Price - (chargesDt.Price * chargesHd.TariffDiscountForRoomIn / 100);
            }
            else
            {
                chargesDt.IsAdminCalculation = false;
                chargesDt.Price = 0;
            }

            var service = new ItemService();
            if (service.LoadByPrimaryKey(chargesDt.ItemID))
                chargesDt.SRItemUnit = service.SRItemUnit;
            else chargesDt.SRItemUnit = "X";

            chargesDt.IsVariable = false;
            chargesDt.IsCito = false;
            chargesDt.IsCitoInPercent = false;
            chargesDt.BasicCitoAmount = (decimal)0D;
            chargesDt.ChargeQuantity = chargeQty;
            chargesDt.StockQuantity = (decimal)0D;
            chargesDt.DiscountAmount = (decimal)0D;
            chargesDt.CitoAmount = (decimal)0D;
            chargesDt.RoundingAmount = Helper.RoundingDiff;
            chargesDt.SRDiscountReason = string.Empty;
            chargesDt.IsAssetUtilization = false;
            chargesDt.AssetID = string.Empty;
            chargesDt.IsBillProceed = true;
            chargesDt.IsPackage = false;
            chargesDt.IsApprove = true;
            chargesDt.IsVoid = false;
            chargesDt.ParentNo = string.Empty;
            chargesDt.ItemConditionRuleID = string.Empty;

            if (tType == "ds")
            {
                chargesDt.IsOrderRealization = true;
                chargesDt.RealizationUserID = AppSession.UserLogin.UserID;
                chargesDt.RealizationDateTime = (new DateTime()).NowAtSqlServer();
            }
            else
                chargesDt.IsOrderRealization = false;
            chargesDt.IsCasemixApproved = isCasemixApproved;

            chargesDt.LastUpdateByUserID = AppSession.UserLogin.UserID;
            chargesDt.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            chargesDt.CreatedByUserID = AppSession.UserLogin.UserID;
            chargesDt.CreatedDateTime = (new DateTime()).NowAtSqlServer();
            #endregion

            var transChargesItemsDtComp = new TransChargesItemCompCollection();
            #region item component
            var compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, grr.SRTariffType, chargesHd.ClassID, chargesDt.ItemID);
            if (!compColl.Any())
                compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, chargesDt.ItemID);
            if (!compColl.Any())
                compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, AppSession.Parameter.DefaultTariffType, chargesHd.ClassID, chargesDt.ItemID);
            if (!compColl.Any())
                compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, chargesDt.ItemID);

            foreach (var comp in compColl)
            {
                var compCharges = transChargesItemsDtComp.AddNew();
                compCharges.TransactionNo = chargesDt.TransactionNo;
                compCharges.SequenceNo = chargesDt.SequenceNo;
                compCharges.TariffComponentID = comp.TariffComponentID;
                compCharges.Price = comp.Price ?? 0;
                if (chargesHd.IsRoomIn == true) compCharges.Price = compCharges.Price - (compCharges.Price * chargesHd.TariffDiscountForRoomIn / 100);

                compCharges.DiscountAmount = (decimal)0D;
                compCharges.CitoAmount = (decimal)0D;

                var tcomp = new TariffComponent();
                tcomp.LoadByPrimaryKey(comp.TariffComponentID);
                if (tcomp.IsTariffParamedic ?? false) compCharges.ParamedicID = reg.ParamedicID;
                else compCharges.ParamedicID = string.Empty;

                compCharges.LastUpdateByUserID = AppSession.UserLogin.UserID;
                compCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
            #endregion

            var transChargesItemsDtConsumption = new TransChargesItemConsumptionCollection();

            #region Item Consumption
            var consColl = new ItemConsumptionCollection();
            consColl.Query.Where(consColl.Query.ItemID == chargesDt.ItemID);
            consColl.LoadAll();

            foreach (var cons in consColl)
            {
                TransChargesItemConsumption consCharges = transChargesItemsDtConsumption.AddNew();
                consCharges.TransactionNo = chargesDt.TransactionNo;
                consCharges.SequenceNo = chargesDt.SequenceNo;
                consCharges.DetailItemID = cons.ItemID;
                consCharges.Qty = cons.Qty;
                consCharges.SRItemUnit = cons.SRItemUnit;
                consCharges.LastUpdateByUserID = AppSession.UserLogin.UserID;
                consCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
            #endregion

            var costCalculations = new CostCalculationCollection();
            #region auto calculation

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(reg.ServiceUnitID);
            var locationId = unit.GetMainLocationId(unit.ServiceUnitID);

            var tblCovered = Helper.GetCoveredItems(regNo, guarantorId, chargesDt.ItemID, tariffDate, false);
            var rowCovered = tblCovered.AsEnumerable().SingleOrDefault(t => t.Field<string>("ItemID") == chargesDt.ItemID &&
                                                                            t.Field<bool>("IsInclude"));

            //TransChargesItemComps
            if (rowCovered != null)
            {
                decimal? discount = 0;
                bool isDiscount = false, isMargin = false;
                foreach (var comp in transChargesItemsDtComp.Where(t => t.TransactionNo == chargesDt.TransactionNo &&
                                                                        t.SequenceNo == chargesDt.SequenceNo)
                                                            .OrderBy(t => t.TariffComponentID))
                {
                    decimal? amountValue = 0;
                    decimal? basicPrice = 0;
                    decimal? coveragePrice = 0;

                    if (Convert.ToBoolean(rowCovered["IsByTariffComponent"]))
                    {
                        var array = rowCovered["TariffComponentValue"].ToString().Split(';').Where(l => l.Split('/')[2] == comp.TariffComponentID).SingleOrDefault();
                        if (array == null)
                        {
                            amountValue = (decimal?)rowCovered["AmountValue"];
                            basicPrice = (decimal?)rowCovered["BasicPrice"];
                            coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                        }
                        else
                        {
                            var list = array.Split('/');
                            if (list == null || list.Count() == 0)
                            {
                                amountValue = (decimal?)rowCovered["AmountValue"];
                                basicPrice = (decimal?)rowCovered["BasicPrice"];
                                coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                            }
                            else
                            {
                                amountValue = Convert.ToDecimal(list[3]);
                                basicPrice = Convert.ToDecimal(list[0]);
                                coveragePrice = Convert.ToDecimal(list[1]);
                            }
                        }
                    }
                    else
                    {
                        amountValue = (decimal?)rowCovered["AmountValue"];
                        basicPrice = (decimal?)rowCovered["BasicPrice"];
                        coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                    }

                    basicPrice = Helper.Tariff.GetItemConditionRuleTariff(basicPrice ?? 0, chargesDt.ItemConditionRuleID, tariffDate);
                    coveragePrice = Helper.Tariff.GetItemConditionRuleTariff(coveragePrice ?? 0, chargesDt.ItemConditionRuleID, tariffDate);

                    if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                    {
                        if ((comp.Price - comp.DiscountAmount) <= 0)
                            continue;

                        var compPrice = comp.Price;
                        if (basicPrice > coveragePrice)
                        {
                            var tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, grr.SRTariffType,
                               reg.CoverageClassID, comp.TariffComponentID, chargesDt.ItemID);
                            if (!tcomp.AsEnumerable().Any())
                                tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, grr.SRTariffType,
                                    AppSession.Parameter.DefaultTariffClass, comp.TariffComponentID, chargesDt.ItemID);
                            if (!tcomp.AsEnumerable().Any())
                                tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, AppSession.Parameter.DefaultTariffType,
                                    reg.CoverageClassID, comp.TariffComponentID, chargesDt.ItemID);
                            if (!tcomp.AsEnumerable().Any())
                                tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, AppSession.Parameter.DefaultTariffType,
                                    AppSession.Parameter.DefaultTariffClass, comp.TariffComponentID, chargesDt.ItemID);

                            if (!tcomp.AsEnumerable().Any())
                                continue;

                            compPrice = tcomp.AsEnumerable().Select(c => c.Field<decimal>("Price")).Single();
                        }

                        if ((bool)rowCovered["IsValueInPercent"])
                        {
                            comp.DiscountAmount += (amountValue / 100) * compPrice;
                            comp.AutoProcessCalculation = 0 - (amountValue / 100) * compPrice;
                        }
                        else
                        {
                            if (!isDiscount)
                            {
                                if (discount == 0)
                                {
                                    if (comp.Price >= amountValue)
                                    {
                                        comp.DiscountAmount += amountValue;
                                        comp.AutoProcessCalculation = 0 - amountValue;
                                        isDiscount = true;
                                    }
                                    else
                                    {
                                        comp.DiscountAmount += compPrice;
                                        comp.AutoProcessCalculation = 0 - compPrice;
                                        discount = amountValue - compPrice;
                                    }
                                }
                                else
                                {
                                    if (compPrice >= discount)
                                    {
                                        comp.DiscountAmount += discount;
                                        comp.AutoProcessCalculation = 0 - discount;
                                        isDiscount = true;
                                    }
                                    else
                                    {
                                        comp.DiscountAmount += compPrice;
                                        comp.AutoProcessCalculation = 0 - compPrice;
                                        discount -= compPrice;
                                    }
                                }
                            }
                        }
                    }
                    else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                    {
                        if ((bool)rowCovered["IsValueInPercent"])
                        {
                            comp.Price += (amountValue / 100) * comp.Price;
                            comp.AutoProcessCalculation = (amountValue / 100) * comp.Price;
                        }
                        else
                        {
                            if (!isMargin)
                            {
                                comp.Price += amountValue;
                                comp.AutoProcessCalculation = amountValue;
                                isMargin = true;
                            }
                        }
                    }
                }
            }

            //TransChargesItems
            if (transChargesItemsDtComp.Count > 0)
            {
                chargesDt.AutoProcessCalculation = transChargesItemsDtComp.Where(t => t.TransactionNo == chargesDt.TransactionNo && t.SequenceNo == chargesDt.SequenceNo)
                                                                          .Sum(t => t.AutoProcessCalculation);
                if (chargesDt.AutoProcessCalculation < 0)
                {
                    chargesDt.DiscountAmount += chargesDt.ChargeQuantity * Math.Abs(chargesDt.AutoProcessCalculation ?? 0);

                    if (chargesDt.DiscountAmount > chargesDt.Price)
                    {
                        chargesDt.DiscountAmount = chargesDt.Price;
                        chargesDt.AutoProcessCalculation = 0 - chargesDt.Price;
                    }
                }
                else if (chargesDt.AutoProcessCalculation > 0) chargesDt.Price += chargesDt.AutoProcessCalculation;
            }
            else
            {
                if (rowCovered != null)
                {
                    if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                    {
                        var basicPrice = (decimal?)rowCovered["BasicPrice"];
                        var coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                        var chargesDtPrice = chargesDt.Price ?? 0;
                        if (basicPrice > coveragePrice)
                        {
                            ItemTariff ttariff = (Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, reg.CoverageClassID, reg.CoverageClassID, chargesDt.ItemID, guarantorId, false, reg.SRRegistrationType) ??
                                     Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.CoverageClassID, chargesDt.ItemID, guarantorId, false, reg.SRRegistrationType)) ??
                                    (Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, reg.CoverageClassID, reg.CoverageClassID, chargesDt.ItemID, guarantorId, false, reg.SRRegistrationType) ??
                                     Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.CoverageClassID, chargesDt.ItemID, guarantorId, false, reg.SRRegistrationType));
                            if (ttariff != null)
                                chargesDtPrice = ttariff.Price ?? 0;
                        }

                        if ((bool)rowCovered["IsValueInPercent"]) chargesDt.DiscountAmount += (chargesDt.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * chargesDtPrice);
                        else chargesDt.DiscountAmount += (chargesDt.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];

                        if (chargesDt.DiscountAmount > chargesDtPrice) chargesDt.DiscountAmount = chargesDtPrice;

                        chargesDt.AutoProcessCalculation = 0 - chargesDt.DiscountAmount;
                    }
                    else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                    {
                        if ((bool)rowCovered["IsValueInPercent"]) chargesDt.Price += ((decimal)rowCovered["AmountValue"] / 100) * chargesDt.Price;
                        else chargesDt.Price += (decimal)rowCovered["AmountValue"];

                        chargesDt.AutoProcessCalculation = chargesDt.Price;
                    }
                }
            }

            //post
            decimal? total = ((chargesDt.ChargeQuantity * chargesDt.Price) - chargesDt.DiscountAmount) + chargesDt.CitoAmount;
            decimal? qty = chargesDt.ChargeQuantity;

            var calc = new Helper.CostCalculation(guarantorId, chargesDt.ItemID, total ?? 0, tblCovered, qty ?? 0, chargesDt.IsCito ?? false,
                chargesDt.IsCitoInPercent ?? false, chargesDt.BasicCitoAmount ?? 0, chargesDt.Price ?? 0, chargesHd.IsRoomIn ?? false, chargesDt.IsItemRoom ?? false,
                chargesHd.TariffDiscountForRoomIn ?? 0, chargesDt.DiscountAmount ?? 0, false, chargesDt.ItemConditionRuleID, tariffDate, chargesDt.IsVariable ?? false);

            CostCalculation cost = costCalculations.AddNew();
            cost.RegistrationNo = regNo;
            cost.TransactionNo = chargesDt.TransactionNo;
            cost.SequenceNo = chargesDt.SequenceNo;
            cost.ItemID = chargesDt.ItemID;
            cost.PatientAmount = calc.PatientAmount;
            cost.GuarantorAmount = calc.GuarantorAmount;
            cost.DiscountAmount = chargesDt.DiscountAmount;
            cost.IsPackage = chargesDt.IsPackage;
            cost.ParentNo = chargesDt.ParentNo;
            cost.ParamedicAmount = chargesDt.ChargeQuantity * transChargesItemsDtComp.Where(comp => comp.TransactionNo == chargesDt.TransactionNo &&
                                                                                                    comp.SequenceNo == chargesDt.SequenceNo &&
                                                                                                    !string.IsNullOrEmpty(comp.ParamedicID))
                                                                                     .Sum(comp => comp.Price - comp.DiscountAmount);
            cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
            #endregion

            // charges
            chargesHd.Save();

            // stock calculation
            var chargesBalance = new ItemBalance();
            var chargesDetailBalances = new ItemBalanceDetailCollection();
            var chargesDetailBalanceEds = new ItemBalanceDetailEdCollection();
            var chargesMovements = new ItemMovementCollection();

            string itemNoStock;

            ItemBalance.PrepareItemBalances(chargesDt, reg.ServiceUnitID, locationId,
                AppSession.UserLogin.UserID, ref chargesBalance, ref chargesDetailBalances, ref chargesMovements, 
                ref chargesDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl);

            chargesDt.Save();
            transChargesItemsDtComp.Save();
            costCalculations.Save();

            if (chargesBalance != null)
                chargesBalance.Save();
            if (chargesDetailBalances != null)
                chargesDetailBalances.Save();
            if (chargesDetailBalanceEds != null)
                chargesDetailBalanceEds.Save();
            if (chargesMovements != null)
                chargesMovements.Save();

            // consumption
            var consumptionBalances = new ItemBalanceCollection();
            var consumptionDetailBalances = new ItemBalanceDetailCollection();
            var consumptionDetailBalanceEds = new ItemBalanceDetailEdCollection();
            var consumptionMovements = new ItemMovementCollection();

            ItemBalance.PrepareItemBalances(transChargesItemsDtConsumption, reg.ServiceUnitID, locationId,
                AppSession.UserLogin.UserID, ref consumptionBalances, ref consumptionDetailBalances, ref consumptionMovements, 
                ref consumptionDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl, out itemNoStock);

            transChargesItemsDtConsumption.Save();

            if (consumptionBalances != null)
                consumptionBalances.Save();
            if (consumptionDetailBalances != null)
                consumptionDetailBalances.Save();
            if (consumptionDetailBalanceEds != null)
                consumptionDetailBalanceEds.Save();
            if (consumptionMovements != null)
                consumptionMovements.Save();

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
            {
                if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                {
                    JournalTransactions.AddNewPatientIncomeAccrual(BusinessObject.JournalType.Income, chargesHd.TransactionNo, AppSession.UserLogin.UserID, 0);
                }
                else {
                    var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');

                    var mb = new MergeBilling();
                    mb.LoadByPrimaryKey(reg.RegistrationNo);
                    if (string.IsNullOrEmpty(mb.FromRegistrationNo))
                    {
                        if (type.Contains(reg.SRRegistrationType))
                        {
                            int? journalId = JournalTransactions.AddNewIncomeJournalTemporary(chargesHd, transChargesItemsDtComp, reg, unit, costCalculations, "SU", AppSession.UserLogin.UserID, 0);
                        }
                    }
                    else
                    {
                        var freg = new Registration();
                        freg.LoadByPrimaryKey(mb.FromRegistrationNo);
                        if (type.Contains(freg.SRRegistrationType))
                        {
                            int? journalId = JournalTransactions.AddNewIncomeJournalTemporary(chargesHd, transChargesItemsDtComp, reg, unit, costCalculations, "SU", AppSession.UserLogin.UserID, 0);
                        }
                    }
                }
            }
        }


        #endregion
    }
}
