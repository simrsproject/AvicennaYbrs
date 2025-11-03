using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for AutoBill
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AutoBillOnSchedule : System.Web.Services.WebService
    {
        [System.Web.Services.WebMethod]
        public string Execute()
        {
            return ExecuteChargeBed(string.Empty);
        }

        public string ExecuteByRegistrationNo(string RegistrationNo)
        {
            return ExecuteChargeBed(RegistrationNo);
        }

        private string ExecuteChargeBed(string RegistrationNo)
        {
            var billingCreated = string.Empty;
            #region Reg Patient Not Rooming In (tidak satu bed dgn bayinya)
            var regQ = new RegistrationQuery("b");
            var bedQ = new BedQuery("a");
            regQ.InnerJoin(bedQ).On(bedQ.BedID == regQ.BedID);

            var patQ = new PatientQuery("d");
            regQ.InnerJoin(patQ).On(regQ.PatientID == patQ.PatientID);

            var guarQ = new GuarantorQuery("e");
            regQ.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);

            var roomQ = new ServiceRoomQuery("f");
            regQ.InnerJoin(roomQ).On(regQ.RoomID == roomQ.RoomID);

            var unitQ = new ServiceUnitQuery("g");
            regQ.InnerJoin(unitQ).On(regQ.ServiceUnitID == unitQ.ServiceUnitID);

            var aubillQ = new ServiceUnitAutoBillItemQuery("ab");
            regQ.InnerJoin(aubillQ).On(regQ.ServiceUnitID == aubillQ.ServiceUnitID & aubillQ.IsGenerateOnSchedule == true);

            var itQ = new ItemQuery("c");
            regQ.InnerJoin(itQ).On(aubillQ.ItemID == itQ.ItemID);

            regQ.Select
                (
                   regQ.RegistrationNo,
                   regQ.ParamedicID,
                   regQ.DepartmentID,
                   regQ.ServiceUnitID,
                   regQ.RoomID,
                   regQ.BedID,
                   bedQ.ClassID, // Utk status digenerate atau tidak
                   regQ.ChargeClassID,
                   regQ.CoverageClassID,
                   guarQ.SRTariffType,
                   aubillQ.ItemID,
                   itQ.SRItemType,
                   regQ.GuarantorID,
                   bedQ.SRBedStatus,
                   regQ.IsRoomIn,
                   roomQ.TariffDiscountForRoomIn,
                   bedQ.IsNeedConfirmation,
                   aubillQ.GenerateOnClassIDs,
                   aubillQ.GenerateOnDayStart,
                   aubillQ.GenerateOnDayEnd,
                   regQ.RegistrationDate
                );

            regQ.Where
                (
                    regQ.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    regQ.IsVoid == false,
                    regQ.IsRoomIn == false, //IsRoomingIn untuk kasus Ibu melahirkan yg ingin bayinya bareng satu bed dan ada kebijakan dari sisi biayanya
                    bedQ.IsActive == true
                );
            
            if (AppSession.Parameter.IsAutoChargeBedBasedOnDischargeDate)
                regQ.Where(regQ.DischargeDate.IsNull());
            else
                regQ.Where(regQ.SRDischargeMethod == string.Empty);

            if (!string.IsNullOrEmpty(RegistrationNo))
            {
                regQ.Where(regQ.RegistrationNo == RegistrationNo);
            }

            if (AppSession.Parameter.IsAutoChargeBedFilterLock) {
                regQ.Where(regQ.IsHoldTransactionEntry == false, regQ.IsClosed == false);
            }

            var dtb = regQ.LoadDataTable();
            #endregion

            #region Reg Patient Rooming In (Satu bed dgn bayinya)
            regQ = new RegistrationQuery("b");
            var bedRoomInQ = new BedRoomInQuery("a");
            regQ.InnerJoin(bedRoomInQ).On(bedRoomInQ.RegistrationNo == regQ.RegistrationNo);

            bedQ = new BedQuery("h");
            regQ.InnerJoin(bedQ).On(bedRoomInQ.BedID == bedQ.BedID);

            patQ = new PatientQuery("d");
            regQ.InnerJoin(patQ).On(regQ.PatientID == patQ.PatientID);

            guarQ = new GuarantorQuery("e");
            regQ.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);

            roomQ = new ServiceRoomQuery("f");
            regQ.InnerJoin(roomQ).On(bedQ.RoomID == roomQ.RoomID);

            unitQ = new ServiceUnitQuery("g");
            regQ.InnerJoin(unitQ).On(roomQ.ServiceUnitID == unitQ.ServiceUnitID);

            aubillQ = new ServiceUnitAutoBillItemQuery("ab");
            regQ.InnerJoin(aubillQ).On(regQ.ServiceUnitID == aubillQ.ServiceUnitID & aubillQ.IsGenerateOnSchedule == true);

            itQ = new ItemQuery("c");
            regQ.InnerJoin(itQ).On(aubillQ.ItemID == itQ.ItemID);

            regQ.Select
                (
                   bedRoomInQ.RegistrationNo,
                   //"<CASE WHEN d.MemberID = '' THEN b.GuarantorID ELSE d.MemberID END AS GuarantorID>",
                   regQ.ParamedicID,
                   regQ.DepartmentID,
                   regQ.ServiceUnitID,
                   bedQ.RoomID,
                   bedRoomInQ.BedID,
                   bedQ.ClassID, // Utk status digenerate atau tidak
                   regQ.ChargeClassID,
                   regQ.CoverageClassID,
                   guarQ.SRTariffType,
                   aubillQ.ItemID,
                   itQ.SRItemType,
                   regQ.GuarantorID,
                   bedQ.SRBedStatus,
                   regQ.IsRoomIn,
                   roomQ.TariffDiscountForRoomIn,
                   bedQ.IsNeedConfirmation,
                   aubillQ.GenerateOnClassIDs,
                   aubillQ.GenerateOnDayStart,
                   aubillQ.GenerateOnDayEnd,
                   regQ.RegistrationDate
                );
            regQ.Where
                (
                    regQ.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    regQ.IsVoid == false,
                    regQ.IsRoomIn == true,  //IsRoomingIn untuk kasus Ibu melahirkan yg ingin bayinya bareng satu bed dan ada kebijakan dari sisi biayanya
                    bedQ.IsActive == true,
                    bedRoomInQ.DateOfExit.IsNull()
                );

            if (AppSession.Parameter.IsAutoChargeBedBasedOnDischargeDate)
                regQ.Where(regQ.DischargeDate.IsNull());
            else
                regQ.Where(regQ.SRDischargeMethod == string.Empty);

            if (!string.IsNullOrEmpty(RegistrationNo))
            {
                regQ.Where(regQ.RegistrationNo == RegistrationNo);
            }

            if (AppSession.Parameter.IsAutoChargeBedFilterLock)
            {
                regQ.Where(regQ.IsHoldTransactionEntry == false, regQ.IsClosed == false);
            }

            var dtbRoomingIn = regQ.LoadDataTable();
            dtb.Merge(dtbRoomingIn);

            #endregion

            #region Pasien rawat inap dgn status baru booking
            // Diremark dulu krn sudah ada di ChargesBed 
            //if (AppSession.Parameter.IsBookingBedCharged)
            //{
            //    regQ = new RegistrationQuery("b");
            //    bedQ = new BedQuery("a");
            //    regQ.InnerJoin(bedQ).On(bedQ.RegistrationNo == regQ.RegistrationNo && bedQ.SRBedStatus == AppSession.Parameter.BedStatusBooked);

            //    guarQ = new GuarantorQuery("e");
            //    regQ.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);

            //    roomQ = new ServiceRoomQuery("f");
            //    regQ.InnerJoin(roomQ).On(roomQ.RoomID == bedQ.RoomID);

            //    unitQ = new ServiceUnitQuery("g");
            //    regQ.InnerJoin(unitQ).On(unitQ.ServiceUnitID == roomQ.ServiceUnitID);

            //    patQ = new PatientQuery("d");
            //    regQ.InnerJoin(patQ).On(regQ.PatientID == patQ.PatientID);

            //    aubillQ = new ServiceUnitAutoBillItemQuery("ab");
            //    regQ.InnerJoin(aubillQ).On(regQ.ServiceUnitID == aubillQ.ServiceUnitID & aubillQ.IsGenerateOnSchedule == true);

            //    itQ = new ItemQuery("c");
            //    regQ.InnerJoin(itQ).On(aubillQ.ItemID == itQ.ItemID);

            //    regQ.Select
            //        (
            //           bedQ.RegistrationNo,
            //           //"<CASE WHEN d.MemberID = '' THEN b.GuarantorID ELSE d.MemberID END AS GuarantorID>",
            //           regQ.ParamedicID,
            //           regQ.DepartmentID,
            //           roomQ.ServiceUnitID,
            //           bedQ.RoomID,
            //           bedQ.BedID,
            //           bedQ.ClassID, // Utk status digenerate atau tidak
            //           regQ.ChargeClassID,
            //           regQ.CoverageClassID,
            //           guarQ.SRTariffType,
            //           aubillQ.ItemID,
            //           itQ.SRItemType,
            //           regQ.GuarantorID,
            //           bedQ.SRBedStatus,
            //           regQ.IsRoomIn,
            //           roomQ.TariffDiscountForRoomIn,
            //           bedQ.IsNeedConfirmation,
            //       aubillQ.GenerateOnClassIDs,
            //       aubillQ.GenerateOnDayStart,
            //       aubillQ.GenerateOnDayEnd,
            //       regQ.RegistrationDate
            //        );
            //    regQ.Where
            //        (
            //            regQ.SRRegistrationType == AppConstant.RegistrationType.InPatient,
            //            regQ.IsVoid == false,
            //            regQ.IsRoomIn == false,
            //            bedQ.IsActive == true
            //        );
            //    if (AppSession.Parameter.IsAutoChargeBedBasedOnDischargeDate)
            //        regQ.Where(regQ.DischargeDate.IsNull());
            //    else
            //        regQ.Where(regQ.SRDischargeMethod == string.Empty);

            //    DataTable tbl3 = regQ.LoadDataTable();
            //    dtb.Merge(tbl3);
            //}
            #endregion

            var currentTime = (new DateTime()).NowAtSqlServer();
            var currentDate = currentTime.Date;
            var regNo = string.Empty;
            var seqNo = 0;
            var locationID = string.Empty;
            var serviceUnitID = string.Empty;
            Registration reg = null;
            Guarantor grr = null;
            TransCharges chargesHd = null;
            TransChargesItemCollection chargesItemColl = null;
            TransChargesItemCompCollection chargesItemCompColl = null;
            TransChargesItemConsumptionCollection chargesItemConsumptionColl = null;
            CostCalculationCollection costCalculationColl = null;

            //Sort by RegistrationNo
            var tblOrdered = dtb.AsEnumerable()
                    .OrderBy(row => row.Field<string>("RegistrationNo"))
                    .CopyToDataTable();

            foreach (DataRow row in tblOrdered.Rows)
            {
                string itemId = row["ItemID"].ToString();
                if (string.IsNullOrEmpty(itemId)) continue;

                // Check billing sudah digenerate dari current itemID
                if (IsExist(row, itemId, currentDate)) continue;

                // Check batasan class
                var generateOnClassIDs = row["GenerateOnClassIDs"] == DBNull.Value || string.IsNullOrEmpty(row["GenerateOnClassIDs"].ToString()) ? string.Empty : string.Format("{0};", row["GenerateOnClassIDs"]);
                if (!string.IsNullOrEmpty(generateOnClassIDs) && !generateOnClassIDs.Contains(string.Format("{0};", row["ClassID"]))) continue;

                // Check batasan Start End day
                var dayStart = row["GenerateOnDayStart"].ToInt();
                var dayEnd = row["GenerateOnDayEnd"].ToInt();
                var daysIP = (currentDate - Convert.ToDateTime(row["RegistrationDate"]).Date).TotalDays + 1;

                if (daysIP < dayStart || (dayEnd > 0 && daysIP > dayEnd)) continue;

                string bedStatus = row["SRBedStatus"].ToString();
                if (!(Convert.ToBoolean(row["IsNeedConfirmation"]) && bedStatus == AppSession.Parameter.BedStatusPending))
                {
                    if (!regNo.Equals(row["RegistrationNo"].ToString()))
                    {
                        // Save
                        if (chargesHd != null)
                        {
                            billingCreated = string.Concat(billingCreated,", ", Save(chargesHd, reg, chargesItemColl, chargesItemCompColl, chargesItemConsumptionColl, costCalculationColl, serviceUnitID, locationID));

                            // Get Time
                            currentTime = (new DateTime()).NowAtSqlServer();
                            currentDate = currentTime.Date;
                        }


                        // Prepare 
                        seqNo = 0;
                        chargesHd = new TransCharges();
                        chargesItemColl = new TransChargesItemCollection();
                        chargesItemCompColl = new TransChargesItemCompCollection();
                        chargesItemConsumptionColl = new TransChargesItemConsumptionCollection();
                        costCalculationColl = new CostCalculationCollection();

                        regNo = row["RegistrationNo"].ToString();
                        serviceUnitID = row["ServiceUnitID"].ToString();
                        locationID = ServiceUnit.MainLocationID(serviceUnitID);

                        chargesHd = InitializeHeader(row, currentTime);

                        reg = new Registration();
                        reg.LoadByPrimaryKey(regNo);

                        grr = new Guarantor();
                        grr.LoadByPrimaryKey(row["GuarantorID"].ToString());
                    }
                    seqNo ++;
                    InitializeDetail(seqNo, row, itemId, chargesHd, reg, grr, chargesItemColl, chargesItemCompColl, chargesItemConsumptionColl, costCalculationColl);
                }
            }

            // Save Last Charges
            if (chargesHd != null)
                billingCreated = string.Concat(billingCreated,", ", Save(chargesHd, reg, chargesItemColl, chargesItemCompColl, chargesItemConsumptionColl, costCalculationColl, serviceUnitID, locationID));

            return billingCreated;
        }

        private static string Save(TransCharges chargesHd, Registration reg,
            TransChargesItemCollection chargesItemColl,
            TransChargesItemCompCollection chargesItemCompColl,
            TransChargesItemConsumptionCollection chargesItemConsumptionColl,
            CostCalculationCollection costCalculationColl, string serviceUnitID, string locationID)
        {
            using (var trans = new esTransactionScope())
            {
                chargesHd.Save();

                // stock calculation
                // charges
                var itemBalanceColl = new ItemBalanceCollection();
                var itemBalanceDetailColl = new ItemBalanceDetailCollection();
                var itemBalanceDetailEdColl = new ItemBalanceDetailEdCollection();
                var itemMovementColl = new ItemMovementCollection();

                string itemNoStock = null;

                ItemBalance.PrepareItemBalances(chargesItemConsumptionColl, serviceUnitID, locationID,
                    "WEBSERVICE", ref itemBalanceColl, ref itemBalanceDetailColl, ref itemMovementColl, ref itemBalanceDetailEdColl, AppSession.Parameter.IsEnabledStockWithEdControl, 
                    out itemNoStock);

                chargesItemColl.Save();
                chargesItemCompColl.Save();
                costCalculationColl.Save();

                if (itemBalanceColl != null)
                    itemBalanceColl.Save();
                if (itemBalanceDetailColl != null)
                    itemBalanceDetailColl.Save();
                if (itemBalanceDetailEdColl != null)
                    itemBalanceDetailEdColl.Save();
                if (itemMovementColl != null)
                    itemMovementColl.Save();

                // consumption
                var consumptionBalances = new ItemBalanceCollection();
                var consumptionDetailBalances = new ItemBalanceDetailCollection();
                var consumptionDetailBalanceEds = new ItemBalanceDetailEdCollection();
                var consumptionMovements = new ItemMovementCollection();

                ItemBalance.PrepareItemBalances(chargesItemConsumptionColl, serviceUnitID, locationID,
                    "WEBSERVICE", ref consumptionBalances, ref consumptionDetailBalances, ref consumptionMovements, ref consumptionDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl, 
                    out itemNoStock);

                chargesItemConsumptionColl.Save();

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
                        JournalTransactions.AddNewPatientIncomeAccrual(BusinessObject.JournalType.Income, chargesHd.TransactionNo, "WEBSERVICE", 0);
                    }
                    else {
                        var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                        if (type.Contains(reg.SRRegistrationType))
                        {
                            var unit = new ServiceUnit();
                            unit.LoadByPrimaryKey(serviceUnitID);
                            int? journalId = JournalTransactions.AddNewIncomeJournalTemporary(chargesHd, chargesItemCompColl, reg, unit, costCalculationColl, "SU", "WEBSERVICE", 0);
                        }
                    }
                }

                trans.Complete();
            }
            return chargesHd.TransactionNo;
        }
        private static bool IsExist(DataRow row, string itemId, DateTime date)
        {
            // Check AutoBill has created
            var hd = new TransChargesQuery("a");
            var dt = new TransChargesItemQuery("b");
            dt.es.Top = 1;
            var chargesDt = new TransChargesItem();
            dt.InnerJoin(hd).On(dt.TransactionNo == hd.TransactionNo);
            dt.Where(hd.RegistrationNo == row["RegistrationNo"].ToString(),
                hd.ExecutionDate >= date.ToString(AppConstant.DisplayFormat.DateSql), hd.ExecutionDate < date.AddDays(1).ToString(AppConstant.DisplayFormat.DateSql),
                hd.ToServiceUnitID == row["ServiceUnitID"].ToString(),
                hd.ClassID == row["ChargeClassID"].ToString(), dt.ItemID == itemId);

            return (chargesDt.Load(dt));
        }
        private static TransCharges InitializeHeader(DataRow row, DateTime currentTime)
        {
            #region header
            var chargesHd = new TransCharges();

            var number = Helper.GetNewAutoNumber(currentTime.Date, AppEnum.AutoNumber.AutoChargeBedNo, string.Empty, "WEBSERVICE");
            chargesHd.TransactionNo = number.LastCompleteNumber;

            // number
            number.LastCompleteNumber = chargesHd.TransactionNo;
            number.Save();

            chargesHd.RegistrationNo = row["RegistrationNo"].ToString();
            chargesHd.TransactionDate = currentTime.Date;
            chargesHd.ReferenceNo = string.Empty;
            chargesHd.FromServiceUnitID = row["ServiceUnitID"].ToString();
            chargesHd.ToServiceUnitID = row["ServiceUnitID"].ToString();
            chargesHd.ClassID = row["ChargeClassID"].ToString();
            chargesHd.RoomID = row["RoomID"].ToString();
            chargesHd.BedID = row["BedID"].ToString();
            chargesHd.DueDate = currentTime.Date;
            chargesHd.SRShift = Registration.GetShiftID();
            chargesHd.SRItemType = string.Empty;
            chargesHd.IsProceed = false;
            chargesHd.IsBillProceed = true;
            chargesHd.IsApproved = true;
            chargesHd.IsVoid = false;
            chargesHd.IsOrder = false;
            chargesHd.IsCorrection = false;
            chargesHd.IsClusterAssign = false;
            chargesHd.IsAutoBillTransaction = true;
            chargesHd.Notes = string.Empty;
            chargesHd.IsRoomIn = (bool)row["IsRoomIn"];
            var room = new ServiceRoom();
            room.LoadByPrimaryKey(chargesHd.RoomID);
            chargesHd.TariffDiscountForRoomIn = room.TariffDiscountForRoomIn ?? 0;
            chargesHd.SurgicalPackageID = string.Empty;

            chargesHd.LastUpdateByUserID = "WEBSERVICE";
            chargesHd.LastUpdateDateTime = currentTime;
            chargesHd.CreatedByUserID = "WEBSERVICE";
            chargesHd.CreatedDateTime = currentTime;
            #endregion

            return chargesHd;
        }
        private static void InitializeDetail(int seqNo, DataRow row, string itemId, TransCharges chargesHd, Registration reg, Guarantor grr,
            TransChargesItemCollection chargesItemColl,
            TransChargesItemCompCollection chargesItemCompColl,
            TransChargesItemConsumptionCollection chargesItemConsumptionColl,
            CostCalculationCollection costCalculationColl)
        {
            var tariffDate = chargesHd.TransactionDate.Value.Date;
            if (grr.TariffCalculationMethod == 1) tariffDate = reg.RegistrationDate.Value.Date;

            #region detail
            var chargesDt = chargesItemColl.AddNew();
            chargesDt.TransactionNo = chargesHd.TransactionNo;
            chargesDt.SequenceNo = string.Format("{0:000}", seqNo);
            chargesDt.ReferenceNo = string.Empty;
            chargesDt.ReferenceSequenceNo = string.Empty;
            chargesDt.ItemID = itemId;
            chargesDt.ChargeClassID = row["ChargeClassID"].ToString();
            chargesDt.ParamedicID = row["ParamedicID"].ToString();
            chargesDt.IsItemRoom = true;
            chargesDt.TariffDate = tariffDate;

            ItemTariff tariff = (Helper.Tariff.GetItemTariff(tariffDate,
                                                             row["SRTariffType"].ToString(),
                                                             chargesHd.ClassID, chargesHd.ClassID, chargesDt.ItemID,
                                                             row["GuarantorID"].ToString(), false, reg.SRRegistrationType) ??
                                 Helper.Tariff.GetItemTariff(tariffDate,
                                                             row["SRTariffType"].ToString(),
                                                             AppSession.Parameter.DefaultTariffClass, chargesHd.ClassID,
                                                             chargesDt.ItemID, row["GuarantorID"].ToString(), false, reg.SRRegistrationType)) ??
                                (Helper.Tariff.GetItemTariff(tariffDate,
                                                            AppSession.Parameter.DefaultTariffType,
                                                            chargesHd.ClassID, chargesHd.ClassID,
                                                            chargesDt.ItemID,
                                                            row["GuarantorID"].ToString(), false, reg.SRRegistrationType) ??
                                Helper.Tariff.GetItemTariff(tariffDate,
                                                            AppSession.Parameter.DefaultTariffType,
                                                            AppSession.Parameter.DefaultTariffClass, chargesHd.ClassID,
                                                            chargesDt.ItemID, row["GuarantorID"].ToString(), false, reg.SRRegistrationType));

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

            if (row["SRItemType"].ToString() == BusinessObject.Reference.ItemType.Medical)
            {
                var ipm = new ItemProductMedic();
                ipm.LoadByPrimaryKey(chargesDt.ItemID);
                chargesDt.SRItemUnit = ipm.SRItemUnit;
            }
            else if (row["SRItemType"].ToString() == BusinessObject.Reference.ItemType.NonMedical)
            {
                var ipn = new ItemProductNonMedic();
                ipn.LoadByPrimaryKey(chargesDt.ItemID);
                chargesDt.SRItemUnit = ipn.SRItemUnit;
            }
            else
            {
                var service = new ItemService();
                service.LoadByPrimaryKey(chargesDt.ItemID);
                chargesDt.SRItemUnit = service.SRItemUnit;
            }

            chargesDt.IsVariable = false;
            chargesDt.IsCito = false;
            chargesDt.IsCitoInPercent = false;
            chargesDt.BasicCitoAmount = (decimal)0D;
            chargesDt.ChargeQuantity = 1;

            if (row["SRItemType"].ToString() == BusinessObject.Reference.ItemType.Medical ||
                row["SRItemType"].ToString() == BusinessObject.Reference.ItemType.NonMedical)
                chargesDt.StockQuantity = 1;
            else
                chargesDt.StockQuantity = (decimal)0D;

            chargesDt.DiscountAmount = (decimal)0D;
            chargesDt.CitoAmount = (decimal)0D;
            chargesDt.RoundingAmount = Helper.RoundingDiff;
            chargesDt.SRDiscountReason = string.Empty;
            chargesDt.IsAssetUtilization = false;
            chargesDt.AssetID = string.Empty;
            chargesDt.IsBillProceed = true;
            chargesDt.IsOrderRealization = false;
            chargesDt.IsPackage = false;
            chargesDt.IsApprove = true;
            chargesDt.IsVoid = false;
            chargesDt.ParentNo = string.Empty;
            chargesDt.ItemConditionRuleID = string.Empty;

            var currentTime = (new DateTime()).NowAtSqlServer();
            chargesDt.LastUpdateByUserID = "WEBSERVICE";
            chargesDt.LastUpdateDateTime = currentTime;
            chargesDt.CreatedByUserID = "WEBSERVICE";
            chargesDt.CreatedDateTime = currentTime;
            #endregion

            #region item component
            var compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, row["SRTariffType"].ToString(), chargesHd.ClassID, chargesDt.ItemID);
            if (!compColl.Any())
                compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, row["SRTariffType"].ToString(), AppSession.Parameter.DefaultTariffClass, chargesDt.ItemID);
            if (!compColl.Any())
                compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, AppSession.Parameter.DefaultTariffType, chargesHd.ClassID, chargesDt.ItemID);
            if (!compColl.Any())
                compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, chargesDt.ItemID);

            foreach (var comp in compColl)
            {
                var compCharges = chargesItemCompColl.AddNew();
                compCharges.TransactionNo = chargesDt.TransactionNo;
                compCharges.SequenceNo = chargesDt.SequenceNo;
                compCharges.TariffComponentID = comp.TariffComponentID;
                compCharges.Price = comp.Price ?? 0;
                if (chargesHd.IsRoomIn == true) compCharges.Price = compCharges.Price - (compCharges.Price * chargesHd.TariffDiscountForRoomIn / 100);

                compCharges.DiscountAmount = (decimal)0D;
                compCharges.CitoAmount = (decimal)0D;

                var tcomp = new TariffComponent();
                tcomp.LoadByPrimaryKey(comp.TariffComponentID);
                if (tcomp.IsTariffParamedic ?? false) compCharges.ParamedicID = row["ParamedicID"].ToString();
                else compCharges.ParamedicID = string.Empty;

                compCharges.LastUpdateByUserID = "WEBSERVICE";
                compCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
            #endregion

            #region Item Consumption
            var consColl = new ItemConsumptionCollection();
            consColl.Query.Where(consColl.Query.ItemID == chargesDt.ItemID);
            consColl.LoadAll();

            foreach (var cons in consColl)
            {
                TransChargesItemConsumption consCharges = chargesItemConsumptionColl.AddNew();
                consCharges.TransactionNo = chargesDt.TransactionNo;
                consCharges.SequenceNo = chargesDt.SequenceNo;
                consCharges.DetailItemID = cons.ItemID;
                consCharges.Qty = cons.Qty;
                consCharges.SRItemUnit = cons.SRItemUnit;
                consCharges.LastUpdateByUserID = "WEBSERVICE";
                consCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
            #endregion

            #region auto calculation

            var grrId = row["GuarantorID"].ToString();
            if (grrId == AppSession.Parameter.SelfGuarantor)
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                if (!string.IsNullOrEmpty(pat.MemberID))
                    grrId = pat.MemberID;
            }

            var tblCovered = Helper.GetCoveredItems(row["RegistrationNo"].ToString(), grrId, chargesDt.ItemID, tariffDate, false);
            var rowCovered = tblCovered.AsEnumerable().SingleOrDefault(t => t.Field<string>("ItemID") == chargesDt.ItemID &&
                                                                            t.Field<bool>("IsInclude"));

            //TransChargesItemComps
            if (rowCovered != null)
            {
                decimal? discount = 0;
                bool isDiscount = false, isMargin = false;
                foreach (var comp in chargesItemCompColl.Where(t => t.TransactionNo == chargesDt.TransactionNo &&
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
                            var tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, row["SRTariffType"].ToString(),
                               row["CoverageClassID"].ToString(), comp.TariffComponentID, chargesDt.ItemID);
                            if (!tcomp.AsEnumerable().Any())
                                tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, row["SRTariffType"].ToString(),
                                    AppSession.Parameter.DefaultTariffClass, comp.TariffComponentID, chargesDt.ItemID);
                            if (!tcomp.AsEnumerable().Any())
                                tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, AppSession.Parameter.DefaultTariffType,
                                    row["CoverageClassID"].ToString(), comp.TariffComponentID, chargesDt.ItemID);
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
            if (chargesItemCompColl.Count > 0)
            {
                chargesDt.AutoProcessCalculation = chargesItemCompColl.Where(t => t.TransactionNo == chargesDt.TransactionNo && t.SequenceNo == chargesDt.SequenceNo)
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
                            ItemTariff ttariff = (Helper.Tariff.GetItemTariff(tariffDate, row["SRTariffType"].ToString(), row["CoverageClassID"].ToString(), row["CoverageClassID"].ToString(), chargesDt.ItemID, row["GuarantorID"].ToString(), false, reg.SRRegistrationType) ??
                                     Helper.Tariff.GetItemTariff(tariffDate, row["SRTariffType"].ToString(), AppSession.Parameter.DefaultTariffClass, row["CoverageClassID"].ToString(), chargesDt.ItemID, row["GuarantorID"].ToString(), false, reg.SRRegistrationType)) ??
                                    (Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, row["CoverageClassID"].ToString(), row["CoverageClassID"].ToString(), chargesDt.ItemID, row["GuarantorID"].ToString(), false, reg.SRRegistrationType) ??
                                     Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, row["CoverageClassID"].ToString(), chargesDt.ItemID, row["GuarantorID"].ToString(), false, reg.SRRegistrationType));
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

            var calc = new Helper.CostCalculation(grrId, chargesDt.ItemID, total ?? 0, tblCovered, qty ?? 0, chargesDt.IsCito ?? false,
                chargesDt.IsCitoInPercent ?? false, chargesDt.BasicCitoAmount ?? 0, chargesDt.Price ?? 0, chargesHd.IsRoomIn ?? false, chargesDt.IsItemRoom ?? false,
                chargesHd.TariffDiscountForRoomIn ?? 0, chargesDt.DiscountAmount ?? 0, false, chargesDt.ItemConditionRuleID, tariffDate, chargesDt.IsVariable ?? false);

            CostCalculation cost = costCalculationColl.AddNew();
            cost.RegistrationNo = row["RegistrationNo"].ToString();
            cost.TransactionNo = chargesDt.TransactionNo;
            cost.SequenceNo = chargesDt.SequenceNo;
            cost.ItemID = chargesDt.ItemID;

            //start here
            decimal? totaltrans = calc.GuarantorAmount + calc.PatientAmount + (chargesDt.DiscountAmount ?? 0);
            decimal? totaldisc = chargesDt.DiscountAmount ?? 0;

            if (reg.SRBussinesMethod == AppSession.Parameter.BusinessMethodFlavon)
            {
                if (totaldisc >= totaltrans)
                {
                    cost.GuarantorAmount = 0;
                    cost.PatientAmount = 0;
                }
                else
                {
                    cost.GuarantorAmount = totaltrans - totaldisc;
                    cost.PatientAmount = 0;
                }
                cost.DiscountAmount = totaldisc;
            }
            else
            {
                if (calc.GuarantorAmount > 0)
                {
                    cost.DiscountAmount = totaldisc > (calc.GuarantorAmount + chargesDt.DiscountAmount)
                                               ? (calc.GuarantorAmount + chargesDt.DiscountAmount)
                                               : totaldisc;

                    cost.GuarantorAmount = totaldisc > (calc.GuarantorAmount + chargesDt.DiscountAmount)
                                               ? 0
                                               : (calc.GuarantorAmount + chargesDt.DiscountAmount) - totaldisc;
                    cost.PatientAmount = calc.PatientAmount;

                }
                else
                {
                    cost.DiscountAmount = totaldisc > calc.PatientAmount + chargesDt.DiscountAmount
                                              ? calc.PatientAmount + chargesDt.DiscountAmount
                                              : totaldisc;

                    cost.PatientAmount = totaldisc > calc.PatientAmount + chargesDt.DiscountAmount
                                             ? 0
                                             : calc.PatientAmount + chargesDt.DiscountAmount - totaldisc;
                    cost.GuarantorAmount = calc.GuarantorAmount;
                }

                if (totaldisc > cost.DiscountAmount)
                {
                    //hitung ulang diskon di TransChargesItem & TransChargesItemComp
                    var itemCompColl = chargesItemCompColl.Where(
                            t =>
                            t.TransactionNo == chargesDt.TransactionNo &&
                            t.SequenceNo == chargesDt.SequenceNo)
                            .OrderBy(t => t.TariffComponentID);
                    var i = itemCompColl.Count();

                    foreach (var compEntity in itemCompColl)
                    {
                        compEntity.DiscountAmount = i == 1
                                                   ? (cost.DiscountAmount / Math.Abs(chargesDt.ChargeQuantity ?? 0))
                                                   : (compEntity.Price + compEntity.CitoAmount) * (cost.DiscountAmount / chargesDt.DiscountAmount);

                        var fee = compEntity.CalculateParamedicPercentDiscount(
                            AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                            cost.RegistrationNo, chargesDt.ItemID, (compEntity.DiscountAmount ?? 0),
                            AppSession.UserLogin.UserID, chargesHd.ClassID, chargesHd.ToServiceUnitID);

                    }

                    chargesDt.DiscountAmount = cost.DiscountAmount;
                    chargesDt.Save();
                }
            }
            //end

            //cost.PatientAmount = calc.PatientAmount;
            //cost.GuarantorAmount = calc.GuarantorAmount;
            //cost.DiscountAmount = chargesDt.DiscountAmount;
            cost.IsPackage = chargesDt.IsPackage;
            cost.ParentNo = chargesDt.ParentNo;
            cost.ParamedicAmount = chargesDt.ChargeQuantity * chargesItemCompColl.Where(comp => comp.TransactionNo == chargesDt.TransactionNo &&
                                                                                                    comp.SequenceNo == chargesDt.SequenceNo &&
                                                                                                    !string.IsNullOrEmpty(comp.ParamedicID))
                                                                                     .Sum(comp => comp.Price - comp.DiscountAmount);
            cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            cost.LastUpdateByUserID = "WEBSERVICE";
            #endregion

        }
    }
}
