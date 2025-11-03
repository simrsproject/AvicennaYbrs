using System;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.Module.ControlPanel.Process
{
    public partial class AutomaticChargeBedDialog : BasePage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ProgramID = AppConstant.Program.AutomaticChargeBed;

            if (!IsPostBack)
                txtDate.SelectedDate = (new DateTime()).NowAtSqlServer();
        }

        private void ShowInformation(string information)
        {
            lblInformation.Text = information;
            pnlInformation.Visible = true;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            /// ------------------------------------------------------------------- ///
            /// !! Jika mengubah di sini, ubah juga di  WebService/ChargeBed.asmx
            /// ------------------------------------------------------------------- ///

            var regQ = new RegistrationQuery("b");
            var bedQ = new BedQuery("a");
            var itQ = new ItemQuery("c");
            var patQ = new PatientQuery("d");
            var guarQ = new GuarantorQuery("e");
            var roomQ = new ServiceRoomQuery("f");
            var unitQ = new ServiceUnitQuery("g");

            regQ.Select
                (
                   regQ.RegistrationNo,
                   //"<CASE WHEN d.MemberID = '' THEN b.GuarantorID ELSE d.MemberID END AS GuarantorID>",
                   regQ.ParamedicID,
                   regQ.DepartmentID,
                   regQ.ServiceUnitID,
                   regQ.RoomID,
                   regQ.BedID,
                   regQ.ChargeClassID,
                   regQ.CoverageClassID,
                   guarQ.SRTariffType,
                   roomQ.ItemID,
                   itQ.SRItemType,
                   regQ.GuarantorID,
                   bedQ.SRBedStatus,
                   roomQ.ItemBookedID,
                   regQ.IsRoomIn,
                   roomQ.TariffDiscountForRoomIn,
                   bedQ.IsNeedConfirmation
                );
            regQ.InnerJoin(bedQ).On(bedQ.BedID == regQ.BedID);
            regQ.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
            regQ.InnerJoin(roomQ).On(regQ.RoomID == roomQ.RoomID);
            regQ.InnerJoin(unitQ).On(regQ.ServiceUnitID == unitQ.ServiceUnitID);
            regQ.InnerJoin(itQ).On(roomQ.ItemID == itQ.ItemID);
            regQ.InnerJoin(patQ).On(regQ.PatientID == patQ.PatientID);
            regQ.Where
                (
                    regQ.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    regQ.RegistrationDate.Date() <= txtDate.SelectedDate.Value.Date,
                    regQ.IsVoid == false,
                    regQ.IsRoomIn == false,
                    bedQ.IsActive == true
                );
            if (AppSession.Parameter.IsAutoChargeBedBasedOnDischargeDate)
                regQ.Where(regQ.Or(regQ.DischargeDate.IsNull(), regQ.DischargeDate.Date() > txtDate.SelectedDate.Value.Date));
            else
                regQ.Where(regQ.Or(regQ.SRDischargeMethod == string.Empty, regQ.DischargeDate.Date() > txtDate.SelectedDate.Value.Date));

            DataTable tbl = regQ.LoadDataTable();

            regQ = new RegistrationQuery("b");
            var bedRoomInQ = new BedRoomInQuery("a");
            itQ = new ItemQuery("c");
            patQ = new PatientQuery("d");
            guarQ = new GuarantorQuery("e");
            roomQ = new ServiceRoomQuery("f");
            unitQ = new ServiceUnitQuery("g");
            bedQ = new BedQuery("h");

            regQ.Select
                (
                   bedRoomInQ.RegistrationNo,
                   //"<CASE WHEN d.MemberID = '' THEN b.GuarantorID ELSE d.MemberID END AS GuarantorID>",
                   regQ.ParamedicID,
                   regQ.DepartmentID,
                   regQ.ServiceUnitID,
                   bedQ.RoomID,
                   bedRoomInQ.BedID,
                   regQ.ChargeClassID,
                   regQ.CoverageClassID,
                   guarQ.SRTariffType,
                   roomQ.ItemID,
                   itQ.SRItemType,
                   regQ.GuarantorID,
                   bedQ.SRBedStatus,
                   roomQ.ItemBookedID,
                   regQ.IsRoomIn,
                   roomQ.TariffDiscountForRoomIn,
                   bedQ.IsNeedConfirmation
                );
            regQ.InnerJoin(bedRoomInQ).On(bedRoomInQ.RegistrationNo == regQ.RegistrationNo);
            regQ.InnerJoin(bedQ).On(bedRoomInQ.BedID == bedQ.BedID);
            regQ.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
            regQ.InnerJoin(roomQ).On(bedQ.RoomID == roomQ.RoomID);
            regQ.InnerJoin(unitQ).On(roomQ.ServiceUnitID == unitQ.ServiceUnitID);
            regQ.InnerJoin(itQ).On(roomQ.ItemID == itQ.ItemID);
            regQ.InnerJoin(patQ).On(regQ.PatientID == patQ.PatientID);
            regQ.Where
                (
                    regQ.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    regQ.RegistrationDate.Date() <= txtDate.SelectedDate.Value.Date,
                    regQ.IsVoid == false,
                    regQ.IsRoomIn == true,
                    bedQ.IsActive == true,
                    bedRoomInQ.DateOfExit.IsNull()
                );
            if (AppSession.Parameter.IsAutoChargeBedBasedOnDischargeDate)
                regQ.Where(regQ.Or(regQ.DischargeDate.IsNull(), regQ.DischargeDate.Date() > txtDate.SelectedDate.Value.Date));
            else
                regQ.Where(regQ.Or(regQ.SRDischargeMethod == string.Empty, regQ.DischargeDate.Date() > txtDate.SelectedDate.Value.Date));

            DataTable tbl2 = regQ.LoadDataTable();
            tbl.Merge(tbl2);

            if (AppSession.Parameter.IsBookingBedCharged)
            {
                regQ = new RegistrationQuery("b");
                bedQ = new BedQuery("a");
                itQ = new ItemQuery("c");
                patQ = new PatientQuery("d");
                guarQ = new GuarantorQuery("e");
                roomQ = new ServiceRoomQuery("f");
                unitQ = new ServiceUnitQuery("g");

                regQ.Select
                    (
                       regQ.RegistrationNo,
                       //"<CASE WHEN d.MemberID = '' THEN b.GuarantorID ELSE d.MemberID END AS GuarantorID>",
                       regQ.ParamedicID,
                       regQ.DepartmentID,
                       roomQ.ServiceUnitID,
                       bedQ.RoomID,
                       bedQ.BedID,
                       bedQ.DefaultChargeClassID.As("ChargeClassID"),
                       //regQ.ChargeClassID,
                       regQ.CoverageClassID,
                       guarQ.SRTariffType,
                       roomQ.ItemID,
                       itQ.SRItemType,
                       regQ.GuarantorID,
                       bedQ.SRBedStatus,
                       roomQ.ItemBookedID,
                       regQ.IsRoomIn,
                       roomQ.TariffDiscountForRoomIn,
                       bedQ.IsNeedConfirmation
                    );
                regQ.InnerJoin(bedQ).On(bedQ.RegistrationNo == regQ.RegistrationNo && bedQ.SRBedStatus == AppSession.Parameter.BedStatusBooked);
                regQ.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
                regQ.InnerJoin(roomQ).On(roomQ.RoomID == bedQ.RoomID);
                regQ.InnerJoin(unitQ).On(unitQ.ServiceUnitID == roomQ.ServiceUnitID);
                regQ.InnerJoin(itQ).On(roomQ.ItemBookedID == itQ.ItemID);
                regQ.InnerJoin(patQ).On(regQ.PatientID == patQ.PatientID);
                regQ.Where
                    (
                        regQ.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        regQ.RegistrationDate.Date() <= txtDate.SelectedDate.Value.Date,
                        regQ.IsVoid == false,
                        regQ.Or(regQ.IsRoomIn == false, regQ.IsRoomIn.IsNull()),
                        bedQ.IsActive == true
                    );
                if (AppSession.Parameter.IsAutoChargeBedBasedOnDischargeDate)
                    regQ.Where(regQ.Or(regQ.DischargeDate.IsNull(), regQ.DischargeDate.Date() > txtDate.SelectedDate.Value.Date));
                else
                    regQ.Where(regQ.Or(regQ.SRDischargeMethod == string.Empty, regQ.DischargeDate.Date() > txtDate.SelectedDate.Value.Date));

                DataTable tbl3 = regQ.LoadDataTable();
                tbl.Merge(tbl3);
            }

            foreach (DataRow row in tbl.Rows)
            {
                string bedStatus = row["SRBedStatus"].ToString();
                string itemId, serviceUnitId, roomId, bedId, chargeClassId;

                if (bedStatus == AppSession.Parameter.BedStatusBooked)
                {
                    itemId = row["ItemBookedID"].ToString();
                    serviceUnitId = row["ServiceUnitID"].ToString();
                    roomId = row["RoomID"].ToString();
                    bedId = row["BedID"].ToString();
                    chargeClassId = row["ChargeClassID"].ToString();
                }
                else
                {
                    var pthq = new PatientTransferHistoryQuery();
                    pthq.Where(pthq.RegistrationNo == row["RegistrationNo"].ToString(),
                               pthq.DateOfEntry.Date() <= txtDate.SelectedDate.Value.Date,
                               pthq.Or(pthq.DateOfExit.Date() >= txtDate.SelectedDate.Value.Date,
                                       pthq.DateOfExit.IsNull()));
                    pthq.OrderBy(pthq.DateOfEntry.Descending, pthq.TimeOfEntry.Descending);
                    pthq.es.Top = 1;

                    DataTable pthdt = pthq.LoadDataTable();
                    if (pthdt.Rows.Count > 0)
                    {
                        var pth = new PatientTransferHistory();
                        pth.Load(pthq);
                        serviceUnitId = pth.ServiceUnitID;
                        roomId = pth.RoomID;
                        bedId = pth.BedID;
                        chargeClassId = pth.ChargeClassID;

                        var room = new ServiceRoom();
                        itemId = room.LoadByPrimaryKey(roomId) ? room.ItemID : string.Empty;
                    }
                    else
                    {
                        itemId = row["ItemID"].ToString();
                        serviceUnitId = row["ServiceUnitID"].ToString();
                        roomId = row["RoomID"].ToString();
                        bedId = row["BedID"].ToString();
                        chargeClassId = row["ChargeClassID"].ToString();
                    }
                }

                if (!string.IsNullOrEmpty(itemId) && !(Convert.ToBoolean(row["IsNeedConfirmation"]) && bedStatus == AppSession.Parameter.BedStatusPending))
                {
                    var hd = new TransChargesQuery("a");
                    var dt = new TransChargesItemQuery("b");

                    dt.es.Top = 1;

                    var chargesDt = new TransChargesItem();

                    dt.InnerJoin(hd).On(
                        dt.TransactionNo == hd.TransactionNo &&
                        hd.RegistrationNo == row["RegistrationNo"].ToString() &&
                        hd.ExecutionDate.Date() == txtDate.SelectedDate.Value.Date &&
                        hd.ToServiceUnitID == serviceUnitId &&
                        hd.ClassID == chargeClassId
                        );
                    dt.Where(dt.ItemID == itemId);

                    if (chargesDt.Load(dt)) continue;

                    var cAutoBillItem = new ServiceRoomAutoBillItemCollection();
                    var iroom = cAutoBillItem.AddNew();
                    iroom.RoomID = roomId;
                    iroom.ItemID = itemId;
                    iroom.Quantity = 1;
                    iroom.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    iroom.LastUpdateByUserID = AppSession.UserLogin.UserID;

                    if (AppSession.Parameter.IsAutomaticChargeBedReprocessIncludeAutoBillItem && bedStatus != AppSession.Parameter.BedStatusBooked)
                    {
                        var abiColl = new ServiceRoomAutoBillItemCollection();
                        abiColl.Query.Where(abiColl.Query.RoomID == roomId, abiColl.Query.ItemID != itemId);
                        abiColl.LoadAll();
                        if (abiColl.Count > 0)
                        {
                            foreach (var i in abiColl)
                            {
                                var abi = cAutoBillItem.AddNew();
                                abi.RoomID = i.RoomID;
                                abi.ItemID = i.ItemID;
                                abi.Quantity = i.Quantity;
                                abi.LastUpdateDateTime = i.LastUpdateDateTime;
                                abi.LastUpdateByUserID = i.LastUpdateByUserID;
                            }
                        }
                    }

                    var reg = new Registration();
                    reg.LoadByPrimaryKey(row["RegistrationNo"].ToString());

                    var grr = new Guarantor();
                    grr.LoadByPrimaryKey(row["GuarantorID"].ToString());

                    #region header
                    var chargesHd = new TransCharges();

                    var number = Helper.GetNewAutoNumber(txtDate.SelectedDate.Value.Date, AppEnum.AutoNumber.AutoChargeBedNo, string.Empty, "WEBSERVICE");
                    chargesHd.TransactionNo = number.LastCompleteNumber;

                    // number
                    number.LastCompleteNumber = chargesHd.TransactionNo;
                    number.Save();

                    chargesHd.RegistrationNo = row["RegistrationNo"].ToString();
                    chargesHd.TransactionDate = txtDate.SelectedDate.Value.Date;
                    chargesHd.ExecutionDate = txtDate.SelectedDate.Value.Date;
                    chargesHd.ReferenceNo = string.Empty;
                    chargesHd.FromServiceUnitID = serviceUnitId;
                    chargesHd.ToServiceUnitID = serviceUnitId;
                    chargesHd.ClassID = chargeClassId;
                    chargesHd.RoomID = roomId;
                    chargesHd.BedID = bedId;
                    chargesHd.DueDate = (new DateTime()).NowAtSqlServer().Date;
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

                    chargesHd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    chargesHd.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    chargesHd.CreatedByUserID = AppSession.UserLogin.UserID;
                    chargesHd.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                    #endregion

                    var tariffDate = chargesHd.TransactionDate.Value.Date;
                    if (grr.TariffCalculationMethod == 1) tariffDate = reg.RegistrationDate.Value.Date;

                    var transChargesItemsDt = new TransChargesItemCollection();
                    var transChargesItemsDtComp = new TransChargesItemCompCollection();
                    var transChargesItemsDtConsumption = new TransChargesItemConsumptionCollection();
                    var costCalculations = new CostCalculationCollection();

                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(serviceUnitId);
                    var locationId = unit.GetMainLocationId(serviceUnitId);

                    var grrId = row["GuarantorID"].ToString();
                    if (grrId == AppSession.Parameter.SelfGuarantor)
                    {
                        var pat = new Patient();
                        pat.LoadByPrimaryKey(reg.PatientID);
                        if (!string.IsNullOrEmpty(pat.MemberID))
                            grrId = pat.MemberID;
                    }

                    var seqNo = 1;
                    foreach (var x in cAutoBillItem)
                    {
                        #region detail
                        //chargesDt = new TransChargesItem();
                        chargesDt = transChargesItemsDt.AddNew();

                        chargesDt.TransactionNo = chargesHd.TransactionNo;
                        chargesDt.SequenceNo = string.Format("{0:000}", seqNo);
                        chargesDt.ReferenceNo = string.Empty;
                        chargesDt.ReferenceSequenceNo = string.Empty;
                        chargesDt.ItemID = x.ItemID;
                        chargesDt.ChargeClassID = chargeClassId;
                        chargesDt.ParamedicID = row["ParamedicID"].ToString();
                        chargesDt.IsItemRoom = true;
                        chargesDt.TariffDate = tariffDate;

                        ItemTariff tariff = (Helper.Tariff.GetItemTariff(tariffDate,
                                                                         row["SRTariffType"].ToString(),
                                                                         chargesHd.ClassID, chargesHd.ClassID, chargesDt.ItemID,
                                                                         row["GuarantorID"].ToString(), false, AppConstant.RegistrationType.InPatient) ??
                                             Helper.Tariff.GetItemTariff(tariffDate,
                                                                         row["SRTariffType"].ToString(),
                                                                         AppSession.Parameter.DefaultTariffClass, chargesHd.ClassID,
                                                                         chargesDt.ItemID, row["GuarantorID"].ToString(), false, AppConstant.RegistrationType.InPatient)) ??
                                            (Helper.Tariff.GetItemTariff(tariffDate,
                                                                        AppSession.Parameter.DefaultTariffType,
                                                                        chargesHd.ClassID, chargesHd.ClassID,
                                                                        chargesDt.ItemID,
                                                                        row["GuarantorID"].ToString(), false, AppConstant.RegistrationType.InPatient) ??
                                            Helper.Tariff.GetItemTariff(tariffDate,
                                                                        AppSession.Parameter.DefaultTariffType,
                                                                        AppSession.Parameter.DefaultTariffClass, chargesHd.ClassID,
                                                                        chargesDt.ItemID, row["GuarantorID"].ToString(), false, AppConstant.RegistrationType.InPatient));

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
                        chargesDt.ChargeQuantity = x.Quantity;

                        if (row["SRItemType"].ToString() == BusinessObject.Reference.ItemType.Medical ||
                            row["SRItemType"].ToString() == BusinessObject.Reference.ItemType.NonMedical ||
                            row["SRItemType"].ToString() == BusinessObject.Reference.ItemType.Kitchen)
                            chargesDt.StockQuantity = chargesDt.ChargeQuantity;
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
                        chargesDt.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        chargesDt.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        chargesDt.CreatedByUserID = AppSession.UserLogin.UserID;
                        chargesDt.CreatedDateTime = (new DateTime()).NowAtSqlServer();

                        if (Helper.GuarantorBpjsCasemix.Contains(reg.GuarantorID) && AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                            chargesDt.IsCasemixApproved = Helper.IsCasemixApproved(chargesDt.ItemID, chargesDt.ChargeQuantity ?? 0, reg.RegistrationNo, chargesDt.TransactionNo, reg.GuarantorID, false);
                        else
                            chargesDt.IsCasemixApproved = true;

                        chargesDt.IsBillProceed = chargesDt.IsCasemixApproved;
                        chargesDt.IsApprove = chargesDt.IsCasemixApproved;

                        if ((chargesHd.IsBillProceed ?? false) && !(chargesDt.IsBillProceed ?? false))
                        {
                            chargesHd.IsBillProceed = false;
                            chargesHd.IsApproved = false;
                        }

                        #endregion

                        #region item component
                        var compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate,
                            row["SRTariffType"].ToString(), chargesHd.ClassID, chargesDt.ItemID);
                        if (!compColl.Any())
                            compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate,
                                row["SRTariffType"].ToString(), AppSession.Parameter.DefaultTariffClass, chargesDt.ItemID);
                        if (!compColl.Any())
                            compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate,
                               AppSession.Parameter.DefaultTariffType, chargesHd.ClassID, chargesDt.ItemID);
                        if (!compColl.Any())
                            compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate,
                                AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, chargesDt.ItemID);

                        foreach (var comp in compColl)
                        {
                            var compCharges = transChargesItemsDtComp.AddNew();
                            compCharges.TransactionNo = chargesDt.TransactionNo;
                            compCharges.SequenceNo = chargesDt.SequenceNo;
                            compCharges.TariffComponentID = comp.TariffComponentID;
                            compCharges.Price = comp.Price ?? 0;
                            if (chargesHd.IsRoomIn == true)
                                compCharges.Price = compCharges.Price -
                                                    (compCharges.Price * chargesHd.TariffDiscountForRoomIn / 100);

                            compCharges.DiscountAmount = (decimal)0D;
                            compCharges.CitoAmount = (decimal)0D;

                            var tcomp = new TariffComponent();
                            tcomp.LoadByPrimaryKey(comp.TariffComponentID);
                            if (tcomp.IsTariffParamedic ?? false)
                                compCharges.ParamedicID = row["ParamedicID"].ToString();
                            else
                                compCharges.ParamedicID = string.Empty;

                            compCharges.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            compCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        }
                        #endregion

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
                            consCharges.Qty = cons.Qty * chargesDt.ChargeQuantity;
                            consCharges.QtyRealization = cons.Qty * chargesDt.ChargeQuantity;
                            consCharges.SRItemUnit = cons.SRItemUnit;
                            consCharges.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            consCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        }
                        #endregion

                        #region auto calculation
                        var tblCovered = Helper.GetCoveredItems(row["RegistrationNo"].ToString(), grrId, chargesDt.ItemID, tariffDate, false);
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
                                var amountValue = (decimal?)rowCovered["AmountValue"];
                                var basicPrice = (decimal?)rowCovered["BasicPrice"];
                                var coveragePrice = (decimal?)rowCovered["CoveragePrice"];

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
                                                if (comp.Price >= discount)
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
                            chargesDt.AutoProcessCalculation = transChargesItemsDtComp.Where(t => t.TransactionNo == chargesDt.TransactionNo &&
                                                                                                  t.SequenceNo == chargesDt.SequenceNo)
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
                            else if (chargesDt.AutoProcessCalculation > 0)
                                chargesDt.Price += chargesDt.AutoProcessCalculation;
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
                                        ItemTariff ttariff = (Helper.Tariff.GetItemTariff(tariffDate, row["SRTariffType"].ToString(), row["CoverageClassID"].ToString(), row["CoverageClassID"].ToString(), chargesDt.ItemID, row["GuarantorID"].ToString(), false, AppConstant.RegistrationType.InPatient) ??
                                                 Helper.Tariff.GetItemTariff(tariffDate, row["SRTariffType"].ToString(), AppSession.Parameter.DefaultTariffClass, row["CoverageClassID"].ToString(), chargesDt.ItemID, row["GuarantorID"].ToString(), false, AppConstant.RegistrationType.InPatient)) ??
                                                (Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, row["CoverageClassID"].ToString(), row["CoverageClassID"].ToString(), chargesDt.ItemID, row["GuarantorID"].ToString(), false, AppConstant.RegistrationType.InPatient) ??
                                                 Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, row["CoverageClassID"].ToString(), chargesDt.ItemID, row["GuarantorID"].ToString(), false, AppConstant.RegistrationType.InPatient));
                                        if (ttariff != null)
                                            chargesDtPrice = ttariff.Price ?? 0;
                                    }

                                    if ((bool)rowCovered["IsValueInPercent"])
                                        chargesDt.DiscountAmount += (chargesDt.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * chargesDtPrice);
                                    else
                                        chargesDt.DiscountAmount += (chargesDt.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];

                                    if (chargesDt.DiscountAmount > chargesDtPrice)
                                        chargesDt.DiscountAmount = chargesDtPrice;

                                    chargesDt.AutoProcessCalculation = 0 - chargesDt.DiscountAmount;
                                }
                                else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                                {
                                    if ((bool)rowCovered["IsValueInPercent"])
                                        chargesDt.Price += ((decimal)rowCovered["AmountValue"] / 100) * chargesDt.Price;
                                    else
                                        chargesDt.Price += (decimal)rowCovered["AmountValue"];

                                    chargesDt.AutoProcessCalculation = chargesDt.Price;
                                }
                            }
                        }

                        //post
                        if (chargesHd.IsBillProceed ?? false)
                        {
                            decimal? total = ((chargesDt.ChargeQuantity * chargesDt.Price) - chargesDt.DiscountAmount) + chargesDt.CitoAmount;
                            decimal? qty = chargesDt.ChargeQuantity;

                            var calc = new Helper.CostCalculation(grrId, chargesDt.ItemID, total ?? 0, tblCovered, qty ?? 0,
                                                                              chargesDt.IsCito ?? false,
                                                                              chargesDt.IsCitoInPercent ?? false,
                                                                              chargesDt.BasicCitoAmount ?? 0, chargesDt.Price ?? 0,
                                                                              chargesHd.IsRoomIn ?? false, chargesDt.IsItemRoom ?? false,
                                                                              chargesHd.TariffDiscountForRoomIn ?? 0, chargesDt.DiscountAmount ?? 0, false,
                                                                              chargesDt.ItemConditionRuleID, tariffDate, chargesDt.IsVariable ?? false);

                            CostCalculation cost = costCalculations.AddNew();
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
                                    var itemCompColl = transChargesItemsDtComp.Where(
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

                            cost.IsPackage = chargesDt.IsPackage;
                            cost.ParentNo = chargesDt.ParentNo;
                            cost.ParamedicAmount = chargesDt.ChargeQuantity * transChargesItemsDtComp.Where(comp => comp.TransactionNo == chargesDt.TransactionNo &&
                                                                                                                    comp.SequenceNo == chargesDt.SequenceNo &&
                                                                                                                    !string.IsNullOrEmpty(comp.ParamedicID))
                                                                                                     .Sum(comp => comp.Price - comp.DiscountAmount);
                            cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        }

                        #endregion

                        seqNo += 1;
                    }

                    using (var trans = new esTransactionScope())
                    {
                        chargesHd.Save();

                        transChargesItemsDt.Save();
                        transChargesItemsDtComp.Save();
                        transChargesItemsDtConsumption.Save();

                        if (chargesHd.IsBillProceed ?? false)
                        {
                            // stock calculation
                            // charges
                            //var chargesBalance = new ItemBalance();
                            var chargesBalances = new ItemBalanceCollection();
                            var chargesDetailBalances = new ItemBalanceDetailCollection();
                            var chargesDetailBalanceEds = new ItemBalanceDetailEdCollection();
                            var chargesMovements = new ItemMovementCollection();

                            string itemNoStock;

                            //ItemBalance.PrepareItemBalances(chargesDt, serviceUnitId, locationId,
                            //    AppSession.UserLogin.UserID, ref chargesBalance, ref chargesDetailBalances, ref chargesMovements);

                            ItemBalance.PrepareItemBalances(transChargesItemsDt, serviceUnitId, locationId, AppSession.UserLogin.UserID, true,
                                    ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, ref chargesDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl, out itemNoStock);

                            //chargesDt.Save();

                            costCalculations.Save();

                            //if (chargesBalance != null)
                            //    chargesBalance.Save();
                            if (chargesBalances != null)
                                chargesBalances.Save();
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

                            ItemBalance.PrepareItemBalances(transChargesItemsDtConsumption, serviceUnitId, locationId,
                                AppSession.UserLogin.UserID, ref consumptionBalances, ref consumptionDetailBalances, ref consumptionMovements, ref consumptionDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl, out itemNoStock);

                            if (consumptionBalances != null)
                                consumptionBalances.Save();
                            if (consumptionDetailBalances != null)
                                consumptionDetailBalances.Save();
                            if (consumptionDetailBalanceEds != null)
                                consumptionDetailBalanceEds.Save();
                            if (consumptionMovements != null)
                                consumptionMovements.Save();

                            //if (AppSession.Parameter.IsUsingIntermBill != "Yes")
                            //{
                            //    int? journalId = JournalTransactions.AddNewIncomeJournal(chargesHd, transChargesItemsDtComp, reg,
                            //                                                             unit, costCalculations, "SU",
                            //                                                             AppSession.UserLogin.UserID, 0);
                            //}

                            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                            {
                                if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                                {
                                    JournalTransactions.AddNewPatientIncomeAccrual(BusinessObject.JournalType.Income, chargesHd.TransactionNo, AppSession.UserLogin.UserID, 0);
                                }
                                else{
                                    var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                                    if (type.Contains(reg.SRRegistrationType))
                                    {
                                        int? journalId = JournalTransactions.AddNewIncomeJournalTemporary(chargesHd, transChargesItemsDtComp, reg, unit, costCalculations, "SU", AppSession.UserLogin.UserID, 0);
                                    }
                                }
                            }
                        }

                        trans.Complete();
                    }
                }
            }

            ShowInformation("Automatic Charge Bed is complete.");
        }
    }
}
