using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ServiceUnitCorrectionDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;
        private string _serviceUnitRadiologyID, _serviceUnitRadiologyID2;

        protected void btnReset_Click(object sender, EventArgs e)
        {
            //Reset PR
            if (!string.IsNullOrEmpty(txtReferenceNo.Text))
            {

                txtReferenceNo.Text = string.Empty;
                if (TransChargesItems.Any())
                    TransChargesItems.MarkAllAsDeleted();
                grdTransChargesItem.DataSource = TransChargesItems;
                grdTransChargesItem.DataBind();

                btnGet.Enabled = true;
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["verif"] == "0")
                UrlPageList = "ServiceUnitCorrectionList.aspx?resp=" + Request.QueryString["resp"] + "&disch=" + Request.QueryString["disch"];
            else
                UrlPageList = "../../Billing/FinalizeBilling/FinalizeBillingVerification.aspx?regNo=" + Request.QueryString["regno"] + "&regType=" + Request.QueryString["type"] + "&md=new&from=1";

            if (Request.QueryString["disch"] == "0")
                ProgramID = AppConstant.Program.ServiceUnitTransactionCorrection;
            else if (Request.QueryString["disch"] == "1")
                ProgramID = AppConstant.Program.ServiceUnitTransactionCorrectionVerification;
            else
                ProgramID = AppConstant.Program.ServiceUnitTransactionCorrectionVerificationAncillary;

            _serviceUnitRadiologyID = AppSession.Parameter.ServiceUnitRadiologyID;
            _serviceUnitRadiologyID2 = AppSession.Parameter.ServiceUnitRadiologyID2;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                TransChargesItems = null;
                TransChargesItemComps = null;
                TransChargesItemConsumptions = null;
                if (AppSession.Parameter.IsTariffPriceVisibleOnlyForAdm)
                {
                    if (string.IsNullOrEmpty(Request.QueryString["verif"]) || (Request.QueryString["verif"] == "0"))
                    {
                        grdTransChargesItem.Columns.FindByUniqueName("Price").Visible = false;
                        grdTransChargesItem.Columns.FindByUniqueName("DiscountAmount").Visible = false;
                        grdTransChargesItem.Columns.FindByUniqueName("CitoAmount").Visible = false;
                        grdTransChargesItem.Columns.FindByUniqueName("Total").Visible = false;
                    }
                }
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdTransChargesItem, grdTransChargesItem);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new TransCharges());

            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtTransactionNo.Text = GetNewTransactionNo();

            txtShiftID.Text = Registration.GetShiftID();
            var std = new AppStandardReferenceItem();
            std.LoadByPrimaryKey("Shift", txtShiftID.Text);
            lblShiftName.Text = std.ItemName;

            txtRegistrationNo.Text = Request.QueryString["regno"];

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            txtFromServiceUnitID.Text = Request.QueryString["cid"];
            PopulateFromServiceUnitName(false);

            var patient = new Patient();
            if (patient.LoadByPrimaryKey(reg.PatientID))
            {
                txtMedicalNo.Text = patient.MedicalNo;
                std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName.Text = patient.PatientName;
                txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));
                txtGender.Text = patient.Sex;
                PopulatePatientImage(patient.PatientID);
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtSalutation.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtPlaceDOB.Text = string.Empty;
                txtGender.Text = string.Empty;
            }

            btnGet.Enabled = true;

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new TransCharges();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (TransChargesItems.Count == 0)
            {
                args.MessageText = "Detail transaction is not defined.";
                args.IsCancel = true;
                return;
            }

            var entity = new TransCharges();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (TransChargesItems.Count == 0)
            {
                args.MessageText = "Detail transaction is not defined.";
                args.IsCancel = true;
                return;
            }

            var entity = new TransCharges();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "TransCharges";
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new TransCharges();

            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            bool skipRegValidation = false;

            var mbRegNo = Helper.MergeBilling.GetMergeBillingFrom(entity.RegistrationNo).Trim();
            if (!string.IsNullOrEmpty(mbRegNo))
            {
                var regMB = new Registration();
                if (regMB.LoadByPrimaryKey(mbRegNo))
                {

                    if (regMB.IsClosed ?? false)
                    {
                        args.MessageText = string.Format("Registration {0} has been closed.", regMB.RegistrationNo);
                        args.IsCancel = true;
                        return;
                    }

                    if (regMB.IsHoldTransactionEntry ?? false)
                    {
                        args.MessageText = string.Format("Registration {0} has been locked.", regMB.RegistrationNo);
                        args.IsCancel = true;
                        return;
                    }
                    skipRegValidation = true;
                }
            }

            if (!skipRegValidation)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(entity.RegistrationNo);

                if (reg.IsClosed ?? false)
                {
                    args.MessageText = string.Format("Registration {0} has been closed.", reg.RegistrationNo);
                    args.IsCancel = true;
                    return;
                }

                if (reg.IsHoldTransactionEntry ?? false)
                {
                    args.MessageText = string.Format("Registration {0} has been locked.", reg.RegistrationNo);
                    args.IsCancel = true;
                    return;
                }
            }

            //cek apakah sudah ada perubahan classid dari menu verifikasi billing
            var tc = new TransCharges();
            if (tc.LoadByPrimaryKey(entity.ReferenceNo) && tc.ClassID != entity.ClassID)
            {
                args.MessageText = "Charge class has changed, please void this transaction number and create a new transaction correction.";
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, true, args);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            //var entity = new TransCharges();

            //if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            //{
            //    args.MessageText = AppConstant.Message.RecordNotExist;
            //    args.IsCancel = true;
            //    return;
            //}
            //if (entity.IsVoid ?? false)
            //{
            //    args.MessageText = AppConstant.Message.RecordHasVoided;
            //    args.IsCancel = true;
            //    return;
            //}

            //SetApproval(entity, false, args);
        }

        private void SetApproval(TransCharges entity, bool isApproval, ValidateArgs args)
        {
            //header
            entity.IsApproved = isApproval;
            if (isApproval)
            {
                entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;
            }
            else
            {
                entity.ApprovedDateTime = null;
                entity.ApprovedByUserID = null;
            }
            entity.IsBillProceed = isApproval;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(entity.ToServiceUnitID);
            if (string.IsNullOrEmpty(entity.LocationID))
                entity.LocationID = unit.GetMainLocationId(entity.ToServiceUnitID);

            var reg = new Registration();
            reg.LoadByPrimaryKey(entity.RegistrationNo);

            if (reg.IsHoldTransactionEntry ?? false)
            {
                args.MessageText = "Transaction is locked.";
                args.IsCancel = true;
                return;
            }

            //detail
            foreach (var item in TransChargesItems)
            {
                var initStr = InitTransChargesItem(item.ReferenceNo, item.ReferenceSequenceNo);
                if (!string.IsNullOrEmpty(initStr))
                {
                    args.MessageText = "No item correction available for : " + initStr;
                    args.IsCancel = true;
                    return;
                }

                if (AppSession.Parameter.IsUsingIntermBill && !(AppSession.Parameter.IsAllowCorrectionForIntermBillTx))
                {
                    var initIb = InitIntermBill(item.ReferenceNo, item.ReferenceSequenceNo);
                    if (!string.IsNullOrEmpty(initIb))
                    {
                        var i = new Item();
                        i.LoadByPrimaryKey(item.ItemID);

                        args.MessageText = "Item : " + i.ItemName + " can not be corrected. This item is already proceed to Interm Bill with transaction no: " + initIb;
                        args.IsCancel = true;
                        return;
                    }
                }

                item.IsApprove = isApproval;
                item.IsBillProceed = isApproval;
                if (item.IsOrderRealization == true)
                {
                    item.RealizationDateTime = (new DateTime()).NowAtSqlServer();
                    item.RealizationUserID = AppSession.UserLogin.UserID;
                }

                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                var detail = new TransChargesItem();
                detail.LoadByPrimaryKey(item.ReferenceNo, item.ReferenceSequenceNo);

                var header = new TransCharges();
                header.LoadByPrimaryKey(detail.TransactionNo);

                var calc = new CostCalculation();
                calc.Query.Where(
                    calc.Query.RegistrationNo == header.RegistrationNo,
                    calc.Query.TransactionNo == item.ReferenceNo,
                    calc.Query.SequenceNo == item.ReferenceSequenceNo
                    );
                calc.Query.Load();

                //CostCalculations
                var cost = CostCalculations.AddNew();
                cost.RegistrationNo = entity.RegistrationNo;
                cost.TransactionNo = item.TransactionNo;
                cost.SequenceNo = item.SequenceNo;
                cost.ItemID = item.ItemID;
                cost.PatientAmount = 0 - ((Math.Abs(item.ChargeQuantity ?? 0) * calc.PatientAmount) / detail.ChargeQuantity);
                cost.GuarantorAmount = 0 - ((Math.Abs(item.ChargeQuantity ?? 0) * calc.GuarantorAmount) / detail.ChargeQuantity);
                cost.DiscountAmount = 0 - ((Math.Abs(item.ChargeQuantity ?? 0) * calc.DiscountAmount) / detail.ChargeQuantity);
                cost.IsPackage = item.IsPackage;
                cost.ParentNo = item.ParentNo;
                cost.ParamedicAmount = 0 - ((Math.Abs(item.ChargeQuantity ?? 0) * calc.ParamedicAmount) / detail.ChargeQuantity);
                cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }

            using (var trans = new esTransactionScope())
            {
                entity.Save();

                // stock calculation
                // charges
                var chargesBalances = new ItemBalanceCollection();
                var chargesDetailBalances = new ItemBalanceDetailCollection();
                var chargesDetailBalanceEds = new ItemBalanceDetailEdCollection();
                var chargesMovements = new ItemMovementCollection();

                var transChargesItems = TransChargesItems;
                string itemZeroCostPrice;

                ItemBalance.PrepareItemBalancesForCorrection(transChargesItems, entity.ToServiceUnitID, entity.LocationID, AppSession.UserLogin.UserID,
                    ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, ref chargesDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl, out itemZeroCostPrice);

                if (!string.IsNullOrEmpty(itemZeroCostPrice))
                {
                    args.MessageText = "Zero cost price of item : " + itemZeroCostPrice;
                    args.IsCancel = true;
                    return;
                }

                transChargesItems.Save();
                CostCalculations.Save();

                if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                {
                    // extract fee
                    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    feeColl.SetFeeByTCIC(TransChargesItemComps, AppSession.UserLogin.UserID);
                    feeColl.Save();
                    feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                    feeColl.Save();
                }

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

                var transChargesItemConsumptions = TransChargesItemConsumptions;

                ItemBalance.PrepareItemBalancesForCorrection(transChargesItemConsumptions, entity.ToServiceUnitID, entity.LocationID,
                    AppSession.UserLogin.UserID, ref consumptionBalances, ref consumptionDetailBalances, ref consumptionMovements, ref consumptionDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl);

                transChargesItemConsumptions.Save();

                if (chargesBalances != null || consumptionBalances != null)
                {
                    var loc = new Location();
                    if (loc.LoadByPrimaryKey(entity.LocationID) && loc.IsHoldForTransaction == true)
                    {
                        args.MessageText = "Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.";
                        args.IsCancel = true;
                        return;
                    }
                }

                if (consumptionBalances != null)
                    consumptionBalances.Save();
                if (consumptionDetailBalances != null)
                    consumptionDetailBalances.Save();
                if (consumptionDetailBalanceEds != null)
                    consumptionDetailBalanceEds.Save();
                if (consumptionMovements != null)
                    consumptionMovements.Save();

                foreach (var t in from item in transChargesItems let t = new TransChargesItem() where t.LoadByPrimaryKey(item.ReferenceNo, item.ReferenceSequenceNo) select t)
                {
                    t.IsCorrection = true;
                    t.Save();
                }

                /* Automatic Journal Testing Start */
                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                {
                    if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                    {
                        JournalTransactions.AddNewPatientIncomeAccrual(BusinessObject.JournalType.Income, entity.TransactionNo, AppSession.UserLogin.UserID, 0);
                    }
                    else {
                        var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                        //if (type.Contains(reg.SRRegistrationType))
                        //{
                        //    var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.TransactionDate.Value.Date);
                        //    if (isClosingPeriod)
                        //    {
                        //        args.MessageText = "Financial statements for period: " +
                        //                           string.Format("{0:MMMM-yyyy}", entity.TransactionDate.Value.Date) +
                        //                           " have been closed. Please contact the authorities.";
                        //        args.IsCancel = true;
                        //        return;
                        //    }

                        //    int? journalId = JournalTransactions.AddNewIncomeCorrectionJournalTemporary(entity, TransChargesItemComps, reg, unit, CostCalculations, "SC", AppSession.UserLogin.UserID, 0);
                        //}
                        var mb = new MergeBilling();
                        mb.LoadByPrimaryKey(reg.RegistrationNo);
                        if (string.IsNullOrEmpty(mb.FromRegistrationNo))
                        {
                            if (type.Contains(reg.SRRegistrationType))
                            {
                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.TransactionDate.Value.Date);
                                if (isClosingPeriod)
                                {
                                    args.MessageText = "Financial statements for period: " +
                                           string.Format("{0:MMMM-yyyy}", entity.TransactionDate.Value.Date) +
                                           " have been closed. Please contact the authorities.";
                                    args.IsCancel = true;
                                    return;
                                }

                                int? journalId = JournalTransactions.AddNewIncomeCorrectionJournalTemporary(entity, TransChargesItemComps, reg, unit, CostCalculations, "SC", AppSession.UserLogin.UserID, 0);
                            }
                        }
                        else
                        {
                            var freg = new Registration();
                            freg.LoadByPrimaryKey(mb.FromRegistrationNo);
                            if (type.Contains(freg.SRRegistrationType))
                            {
                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.TransactionDate.Value.Date);
                                if (isClosingPeriod)
                                {
                                    args.MessageText = "Financial statements for period: " +
                                           string.Format("{0:MMMM-yyyy}", entity.TransactionDate.Value.Date) +
                                           " have been closed. Please contact the authorities.";
                                    args.IsCancel = true;
                                    return;
                                }

                                int? journalId = JournalTransactions.AddNewIncomeCorrectionJournalTemporary(entity, TransChargesItemComps, reg, unit, CostCalculations, "SC", AppSession.UserLogin.UserID, 0);
                            }
                        }
                    }
                }
                /* Automatic Journal Testing End */

                #region Interop
                if (AppSession.Parameter.IsUsingHisInterop && AppSession.Parameter.IsUsingHisInteropCorrection)
                {
                    var patient = new Patient();
                    patient.LoadByPrimaryKey(reg.PatientID);

                    var salutation = string.Empty;
                    var apstd = new AppStandardReferenceItem();
                    if (apstd.LoadByPrimaryKey("Salutation", patient.SRSalutation))
                    { salutation = apstd.ItemName; }

                    var grr = new Guarantor();
                    grr.LoadByPrimaryKey(reg.GuarantorID);

                    if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                    {
                        switch (AppSession.Parameter.HisInteropConfigName)
                        {
                            #region SYSMEX_LIS_INTEROP_CONNECTION_NAME
                            case AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME:
                            case AppConstant.HIS_INTEROP.SYSMEX_LIS_INTEROP_CONNECTION_NAME:
                                if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                {
                                    var lo = new BusinessObject.Interop.SYSMEX.LisOrder();
                                    if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME) lo.es.Connection.Name = AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME;
                                    else lo.es.Connection.Name = AppConstant.HIS_INTEROP.SYSMEX_LIS_INTEROP_CONNECTION_NAME;

                                    lo.MessageDt = (entity.LastUpdateDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
                                    lo.OrderControl = "CA";
                                    if (AppSession.Parameter.HealthcareInitialAppsVersion == "GRHA") lo.Pid = string.IsNullOrEmpty(patient.MedicalNo) ? patient.PatientID : patient.MedicalNo;
                                    else lo.Pid = patient.MedicalNo;
                                    lo.Pname = patient.PatientName;
                                    lo.Address1 = patient.StreetName.Trim();

                                    switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                                    {
                                        case "RSTJ":
                                            lo.Pname = (patient.PatientName + " " + salutation).Trim();
                                            lo.Address2 = patient.District;
                                            lo.Address3 = patient.County;
                                            lo.Address4 = patient.State;
                                            break;

                                        case "RSUTAMA":
                                        case "KLUTAMA":
                                            lo.Address2 = patient.District;
                                            lo.Address3 = patient.County + " " + patient.State;
                                            lo.Address4 = patient.MobilePhoneNo;
                                            break;

                                        case "RSMP":
                                        case "GRHA":
                                            var refral = new Referral();
                                            refral.LoadByPrimaryKey(reg.ReferralID);

                                            lo.Address2 = patient.District;
                                            lo.Address3 = string.IsNullOrEmpty(reg.ReferralID) ? reg.ReferralName : refral.ReferralName;
                                            lo.Address4 = grr.GuarantorName;
                                            break;
                                        case "RSSMCB":
                                            lo.Address2 = grr.GuarantorName;
                                            lo.Address3 = patient.District.Trim() + " " + patient.County.Trim();
                                            if (AppSession.Parameter.IsUsingHisInterop)
                                            {
                                                lo.Address4 = patient.MobilePhoneNo;
                                            }
                                            else
                                            {
                                                if (AppSession.Parameter.HealthcareInitial == "RSSMHB")
                                                {
                                                    lo.Address3 += " " + patient.State.Trim();

                                                    var mb = new MergeBilling();
                                                    if (mb.LoadByPrimaryKey(reg.RegistrationNo))
                                                    {
                                                        if (!string.IsNullOrEmpty(mb.FromRegistrationNo))
                                                        {
                                                            var freg = new Registration();
                                                            freg.LoadByPrimaryKey(mb.FromRegistrationNo);
                                                            var funit = new ServiceUnit();
                                                            funit.LoadByPrimaryKey(freg.ServiceUnitID);
                                                            lo.Address4 = funit.ServiceUnitID + "^" + funit.ServiceUnitName;
                                                        }
                                                        else lo.Address4 = unit.ServiceUnitID + "^" + unit.ServiceUnitName; ;
                                                    }
                                                }
                                                else
                                                {
                                                    lo.Address4 = patient.State;
                                                }
                                            }

                                            break;
                                        case "YBRSGKP":
                                            lo.Address2 = "";
                                            switch (grr.SRGuarantorType)
                                            {
                                                case "09":
                                                    lo.Address2 = "BPJS";
                                                    break;
                                                case "00":
                                                    lo.Address2 = "PRIBADI";
                                                    break;
                                                default:
                                                    lo.Address2 = "MITRA";
                                                    break;

                                            }

                                            lo.Address3 = grr.GuarantorName;
                                            lo.Address4 = entity.ClassID;

                                            var cls = new Class();
                                            cls.LoadByPrimaryKey(entity.ClassID);
                                            lo.Address4 = reg.SRRegistrationType == "IPR" ? cls.ClassName : string.Empty;
                                            break;
                                        default:
                                            lo.Pname = patient.PatientName;
                                            lo.Address2 = patient.District;
                                            lo.Address3 = patient.County;
                                            lo.Address4 = patient.State;
                                            break;
                                    }

                                    lo.Ptype = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? "IN" : "OP";
                                    lo.BirthDt = (patient.DateOfBirth ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
                                    lo.Sex = patient.Sex == "M" ? "1" : "0";
                                    lo.Ono = entity.TransactionNo;
                                    lo.RequestDt = (entity.LastUpdateDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");

                                    unit = new ServiceUnit();
                                    unit.LoadByPrimaryKey(entity.FromServiceUnitID);

                                    lo.Source = unit.ServiceUnitID + "^" + unit.ServiceUnitName;

                                    var param = new Paramedic();

                                    switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                                    {
                                        case "RSUTAMA":
                                            if (!string.IsNullOrEmpty(reg.ReferralID))
                                            {
                                                var refer = new Referral();
                                                refer.LoadByPrimaryKey(reg.ReferralID);

                                                lo.Clinician = reg.ReferralID + "^" + refer.ReferralName;
                                            }
                                            else if (!string.IsNullOrEmpty(reg.PhysicianSenders))
                                            {
                                                lo.Clinician = reg.ParamedicID + "^" + reg.PhysicianSenders;
                                            }
                                            else
                                            {
                                                param.LoadByPrimaryKey(reg.ParamedicID);

                                                lo.Clinician = reg.ParamedicID + "^" + param.ParamedicName;
                                            }
                                            break;
                                        default:
                                            param.LoadByPrimaryKey(reg.ParamedicID);

                                            lo.Clinician = reg.ParamedicID + "^" + param.ParamedicName;
                                            break;
                                    }

                                    lo.RoomNo = reg.RoomID;
                                    lo.Priority = transChargesItems.Any(t => (t.IsCito ?? false)) ? "U" : "R";

                                    if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME) lo.Cmt = grr.GuarantorName;
                                    else
                                    {
                                        if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB" && AppSession.Parameter.IsUsingHisInteropToHcLab)
                                            lo.Cmt = patient.Ssn;
                                        else
                                            lo.Cmt = entity.Notes; //string.Empty;
                                    }

                                    lo.Visitno = entity.RegistrationNo;

                                    var items = new ItemCollection();
                                    items.Query.Where(items.Query.ItemID.In(transChargesItems.Where(t => string.IsNullOrEmpty(t.ParentNo)).Select(t => t.ItemID)));
                                    items.Query.Load();

                                    foreach (var item in items)
                                    {
                                        if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME) lo.OrderTestid += item.ItemID + "~";
                                        else lo.OrderTestid += item.ItemIDExternal + "~";
                                    }

                                    lo.Save();
                                }
                                break;
                            #endregion
                        }
                    }
                }
                #endregion

                //if (AppSession.Parameter.HealthcareID == "RSUI")
                //{
                //    #region Interop
                //    if (AppSession.Parameter.IsUsingHisInterop)
                //    {
                //        var patient = new Patient();
                //        patient.LoadByPrimaryKey(reg.PatientID);

                //        switch (AppSession.Parameter.HisInteropConfigName)
                //        {
                //            #region RSUI_LIS_INTEROP_CONNECTION_NAME
                //            case AppConstant.HIS_INTEROP.SYSMEX_LIS_INTEROP_CONNECTION_NAME:
                //                if (entity.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                //                {
                //                    var lo = new BusinessObject.Interop.SYSMEX.LisOrder();
                //                    lo.es.Connection.Name = AppConstant.HIS_INTEROP.SYSMEX_LIS_INTEROP_CONNECTION_NAME;

                //                    lo.MessageDt = (entity.LastUpdateDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
                //                    lo.OrderControl = "CA";
                //                    lo.Pid = patient.MedicalNo;
                //                    lo.Pname = patient.PatientName;
                //                    lo.Address1 = patient.StreetName;
                //                    lo.Address2 = patient.District;
                //                    lo.Address3 = patient.County;
                //                    lo.Address4 = patient.State;
                //                    lo.Ptype = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? "IN" : "OP";
                //                    lo.BirthDt = (patient.DateOfBirth ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
                //                    lo.Sex = patient.Sex == "M" ? "1" : "0";
                //                    lo.Ono = entity.TransactionNo;
                //                    lo.RequestDt = (entity.LastUpdateDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");

                //                    unit = new ServiceUnit();
                //                    unit.LoadByPrimaryKey(entity.FromServiceUnitID);

                //                    lo.Source = unit.ServiceUnitID + "^" + unit.ServiceUnitName;

                //                    var param = new Paramedic();
                //                    param.LoadByPrimaryKey(reg.ParamedicID);

                //                    lo.Clinician = reg.ParamedicID + "^" + param.ParamedicName;

                //                    lo.RoomNo = reg.RoomID;
                //                    lo.Priority = transChargesItems.Any(t => (t.IsOrderRealization ?? false) && (t.IsCito ?? false)) ? "U" : "R";
                //                    lo.Cmt = string.Empty;
                //                    lo.Visitno = entity.RegistrationNo;

                //                    var items = new ItemCollection();
                //                    items.Query.Where(items.Query.ItemID.In(transChargesItems.Where(t => t.IsOrderRealization ?? false).Select(t => t.ItemID)));
                //                    items.Query.Load();

                //                    foreach (var item in items)
                //                    {
                //                        lo.OrderTestid += "~" + item.ItemIDExternal;
                //                    }

                //                    lo.Save();
                //                }
                //                break;
                //            #endregion
                //        }
                //    }
                //    #endregion
                //}

                //Commit if success, Rollback if failed

                #region Interop RIS/PACS
                if (AppSession.Parameter.IsUsingRisPacsInterop && AppSession.Parameter.HealthcareInitial == "RSBK")
                {
                    if (entity.ToServiceUnitID == _serviceUnitRadiologyID || entity.ToServiceUnitID == _serviceUnitRadiologyID2)
                    {
                        var patient = new Patient();
                        patient.LoadByPrimaryKey(reg.PatientID);

                        var pref = new Paramedic();
                        pref.LoadByPrimaryKey(reg.ParamedicID);

                        var uref = new ServiceUnit();
                        uref.LoadByPrimaryKey(entity.FromServiceUnitID);

                        var epsdiag = new EpisodeDiagnose();
                        epsdiag.Query.es.Top = 1;
                        epsdiag.Query.Where(epsdiag.Query.RegistrationNo == reg.RegistrationNo, epsdiag.Query.SRDiagnoseType.In("DiagnoseType-001", "DiagnoseType-006"), epsdiag.Query.IsVoid == false);
                        epsdiag.Query.OrderBy(epsdiag.Query.CreateDateTime.Descending);
                        var isEpsDiag = epsdiag.Query.Load();

                        string diagId = string.Empty;
                        string diagnoseName = string.Empty;
                        string patasdiagnose = string.Empty;

                        var patas = new PatientAssessment();
                        patas.Query.es.Top = 1;
                        patas.Query.Where(patas.Query.RegistrationNo == reg.RegistrationNo);
                        patas.Query.OrderBy(patas.Query.CreatedDateTime.Descending);
                        var patasdiag = patas.Query.Load();

                        diagId = string.IsNullOrWhiteSpace(epsdiag.DiagnoseID) ? string.Empty : $"({epsdiag.DiagnoseID}) ";
                        diagnoseName = epsdiag.DiagnosisText ?? string.Empty;
                        patasdiagnose = patas.Diagnose ?? string.Empty;

                        if (transChargesItems.Any(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)))
                        {
                            var list = transChargesItems.Where(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)).Select(t =>
                            {
                                var it = new Item();
                                it.LoadByPrimaryKey(t.ItemID);

                                if (it.IsHasTestResults == false)
                                {
                                    return null;
                                }

                                var itg = new ItemGroup();
                                itg.LoadByPrimaryKey(it.ItemGroupID);

                                var refdoc = entity.PhysicianSenders ?? string.Empty;

                                var tcic = new TransChargesItemComp();
                                tcic.LoadByPrimaryKey(t.TransactionNo, t.SequenceNo, "05");

                                var opername = string.Empty;
                                opername = tcic.ParamedicID ?? string.Empty;

                                var sero = new ServiceRoom();
                                sero.LoadByPrimaryKey(reg.RoomID);

                                var seru = new ServiceUnit();
                                seru.LoadByPrimaryKey(entity.FromServiceUnitID);

                                var sal = new AppStandardReferenceItem();
                                sal.LoadByPrimaryKey("Salutation", patient.SRSalutation);

                                return new Common.Worklist.RSBK.DataExamOrder()
                                {
                                    patient_id = patient.MedicalNo,
                                    patient_name = $"{sal.ItemName} {patient.FirstName} {patient.MiddleName} {patient.LastName}",
                                    patient_sex = patient.Sex == "M" ? "M" : (patient.Sex == "F" ? "F" : (patient.Sex == "O" ? "O" : "U")),
                                    patient_birthday = patient.DateOfBirth.Value.ToString("yyyy-MM-dd"),
                                    patient_weight = string.Empty,
                                    patient_class = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? "I" : reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient ? "O" : "E",
                                    ward = sero.RoomName,
                                    attending_doctor = t.ParamedicCollectionName,
                                    referring_doctor = refdoc,
                                    order_control = "CA",
                                    order_department = reg.DepartmentID,
                                    accession_number = $"{t.ReferenceNo}" + $"{t.ReferenceSequenceNo.Substring(t.ReferenceSequenceNo.Length - 2)}", //co:JO240424-00003 + 001 > JO240424-00003 + 01 > JO240424-0000301
                                    study_code = t.ItemID,
                                    study_name = GetItemName(t.ItemID),
                                    order_datetime = entity.TransactionDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                                    scheduled_datetime = entity.ExecutionDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                                    clinic_comments = $"{diagId}, {diagnoseName} ~~ {entity.Notes} ~~ {patasdiagnose}",
                                    sickness_name = t.Notes,
                                    reason_for_study = string.Empty,
                                    body_part = string.Empty,
                                    ordering_doctor = t.ParamedicCollectionName,
                                    exam_room = seru.ServiceUnitName,
                                    modality = itg.Initial.Substring(itg.Initial.Length - 2),
                                    operator_name = Paramedic.GetParamedicName(opername),
                                    exam_urgent = t.IsCito.HasValue ? (t.IsCito.Value ? "1" : "0") : "0",
                                    issuer = "H",
                                    if_flag = 0,
                                    result = 0,
                                    urllink = string.Empty
                                };
                            })
                            .Where(order => order != null)
                            .ToList();

                            var ris = new Common.Worklist.RSBK.Service();
                            if (!ris.CancelExamOrder(list))
                            {
                                //ShowInformationHeader($"Send order failed, please try again.");
                                //return false;
                            }
                            foreach (var tci in transChargesItems.Where(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)))
                            {
                                tci.IsSendToLIS = true;
                            }
                        }
                    }
                }
                #endregion

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new TransCharges();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            var entity = new TransCharges();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(esTransCharges entity, bool isVoid)
        {
            //header
            entity.IsVoid = isVoid;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            //detail
            foreach (var item in TransChargesItems)
            {
                item.IsVoid = isVoid;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                TransChargesItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtTransactionNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemTransChargesItem(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new TransCharges();
            if (parameters.Length > 0)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                    entity.LoadByPrimaryKey(Request.QueryString["id"]);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var transCharges = (TransCharges)entity;
            txtTransactionNo.Text = transCharges.TransactionNo;
            ViewState["IsApproved"] = transCharges.IsApproved ?? false;
            ViewState["IsVoid"] = transCharges.IsVoid ?? false;

            txtRegistrationNo.Text = transCharges.RegistrationNo;
            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            var pat = new Patient();
            if (pat.LoadByPrimaryKey(reg.str.PatientID))
            {
                txtMedicalNo.Text = pat.MedicalNo;
                var std1 = new AppStandardReferenceItem();
                txtSalutation.Text = std1.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std1.ItemName : string.Empty;
                txtPatientName.Text = pat.PatientName;
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));
                txtGender.Text = pat.Sex;

                PopulatePatientImage(pat.PatientID);
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtSalutation.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtPlaceDOB.Text = string.Empty;
                txtGender.Text = string.Empty;
            }

            txtTransactionDate.SelectedDate = transCharges.TransactionDate;
            txtReferenceNo.Text = transCharges.ReferenceNo;

            txtFromServiceUnitID.Text = transCharges.FromServiceUnitID;
            PopulateFromServiceUnitName(false);

            txtShiftID.Text = transCharges.SRShift;
            var std = new AppStandardReferenceItem();
            std.LoadByPrimaryKey("Shift", txtShiftID.Text);
            lblShiftName.Text = std.ItemName;

            txtNotes.Text = transCharges.Notes;
            chkIsProceed.Checked = transCharges.IsBillProceed ?? false;

            //Display Data Detail
            PopulateTransChargesItemGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(esTransCharges entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewTransactionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            entity.TransactionNo = txtTransactionNo.Text;
            //entity.RegistrationNo = txtRegistrationNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.ReferenceNo = txtReferenceNo.Text;
            //entity.FromServiceUnitID = txtFromServiceUnitID.Text;

            var tr = new TransCharges();
            tr.LoadByPrimaryKey(entity.ReferenceNo);
            entity.RegistrationNo = tr.RegistrationNo; // <-- mengikuti registrasi asal transaksi
            entity.ToServiceUnitID = tr.ToServiceUnitID;
            entity.LocationID = tr.LocationID;// <-- mengikuti lokasi asal transaksi
            entity.FromServiceUnitID = tr.FromServiceUnitID; // <-- mengikuti unit asal transaksi

            entity.ClassID = tr.ClassID; // <-- mengikuti kelas asal transaksi
            entity.RoomID = tr.RoomID; // <-- mengikuti room asal transaksi
            entity.BedID = tr.BedID; // <-- mengikuti bed asal transaksi

            entity.DueDate = txtTransactionDate.SelectedDate;
            entity.SRShift = txtShiftID.Text;
            entity.SRItemType = string.Empty;
            entity.IsProceed = false;
            entity.IsApproved = false;
            entity.IsVoid = false;
            entity.IsAutoBillTransaction = tr.IsAutoBillTransaction ?? false;
            entity.IsBillProceed = false;
            entity.IsOrder = false;
            entity.IsCorrection = true;
            entity.Notes = txtNotes.Text;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.CreatedByUserID = AppSession.UserLogin.UserID;
            entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();

            //Last Update Status Detail
            foreach (var item in TransChargesItems)
            {
                item.TransactionNo = entity.TransactionNo;
                item.IsCorrection = true;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                item.CreatedByUserID = AppSession.UserLogin.UserID;
                item.CreatedDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var comp in TransChargesItemComps)
            {
                comp.TransactionNo = entity.TransactionNo;
                comp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                comp.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var cons in TransChargesItemConsumptions)
            {
                cons.TransactionNo = entity.TransactionNo;
                cons.LastUpdateByUserID = AppSession.UserLogin.UserID;
                cons.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(TransCharges entity)
        {
            using (var trans = new esTransactionScope())
            {
                //autonumber has been saved on SetEntity
                //if (DataModeCurrent == DataMode.New)
                //    _autoNumber.Save();



                entity.Save();
                TransChargesItems.Save();
                TransChargesItemComps.Save();
                TransChargesItemConsumptions.Save();



                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new TransChargesQuery();

            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(
                        que.TransactionNo > txtTransactionNo.Text,
                        que.IsCorrection == true
                    );
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(
                        que.TransactionNo < txtTransactionNo.Text,
                        que.IsCorrection == true
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new TransCharges();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, AppEnum.AutoNumber.TransCorrectionNo);
            return _autoNumber.LastCompleteNumber;
        }

        #endregion

        #region Method & Event TextChanged

        private void PopulateFromServiceUnitName(bool isResetIdIfNotExist)
        {
            var entity = new ServiceUnit();
            lblFromServiceUnitName.Text = entity.LoadByPrimaryKey(txtFromServiceUnitID.Text) ? entity.ServiceUnitName : string.Empty;
        }

        protected void txtReferenceNo_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReferenceNo.Text.Trim()))
                PopulateTransChargesItemGrid();
            else
                btnGet.Enabled = false;

            grdTransChargesItem.Rebind();
        }

        #endregion

        #region Record Detail Method Function TransChargesItem

        private void RefreshCommandItemTransChargesItem(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            var isVisible = (newVal != AppEnum.DataMode.Read);
            grdTransChargesItem.Columns[0].Visible = isVisible;
            grdTransChargesItem.Columns[grdTransChargesItem.Columns.Count - 1].Visible = isVisible;

            grdTransChargesItem.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;

            if (oldVal != AppEnum.DataMode.Read)
            {
                TransChargesItems = null;
                TransChargesItemComps = null;
                TransChargesItemConsumptions = null;
                CostCalculations = null;
            }

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdTransChargesItem.Rebind();
        }

        private TransChargesItemCollection TransChargesItems
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTransChargesItem" + Request.UserHostName];
                    if (obj != null)
                        return ((TransChargesItemCollection)(obj));
                }

                var coll = new TransChargesItemCollection();
                var query = new TransChargesItemQuery("a");
                var item = new ItemQuery("b");
                var param = new ParamedicQuery("c");

                query.Select(
                        query,
                        "<0 - (((ABS(a.ChargeQuantity) * a.Price) - a.DiscountAmount) + a.CitoAmount) AS refToTransChargesItem_Total>",
                        item.ItemName.As("refToItem_ItemName"),
                        param.ParamedicName.As("refToParamedic_ParamedicName")
                    );

                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.LeftJoin(param).On(query.ParamedicID == param.ParamedicID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.SequenceNo.Ascending);

                coll.Load(query);

                Session["collTransChargesItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collTransChargesItem" + Request.UserHostName] = value; }
        }

        private void PopulateTransChargesItemGrid()
        {
            //Display Data Detail
            TransChargesItems = null; //Reset Record Detail
            grdTransChargesItem.DataSource = TransChargesItems; //Requery
            grdTransChargesItem.MasterTableView.IsItemInserted = false;
            grdTransChargesItem.MasterTableView.ClearEditItems();
            grdTransChargesItem.DataBind();

            TransChargesItemComps = null;
            TransChargesItemComps.LoadAll();

            TransChargesItemConsumptions = null;
            TransChargesItemConsumptions.LoadAll();

            CostCalculations = null;
            CostCalculations.LoadAll();
        }

        protected void grdTransChargesItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            foreach (var item in TransChargesItems.Where(t => string.IsNullOrEmpty(t.ItemName)))
            {
                var i = new Item();
                i.LoadByPrimaryKey(item.ItemID);
                item.ItemName = i.ItemName;
            }

            grdTransChargesItem.DataSource = TransChargesItems;
        }

        protected void grdTransChargesItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            var sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][TransChargesItemMetadata.ColumnNames.SequenceNo]);
            var entity = FindTransChargesItem(sequenceNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdTransChargesItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            var sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransChargesItemMetadata.ColumnNames.SequenceNo]);
            var entity = FindTransChargesItem(sequenceNo);
            if (entity != null)
            {
                if (Request.QueryString["md"] == "new")
                    entity.MarkAsDeleted();
                else
                    entity.IsVoid = true;
            }
        }

        private TransChargesItem FindTransChargesItem(String sequenceNo)
        {
            TransChargesItemCollection coll = TransChargesItems;
            return coll.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        private void SetEntityValue(TransChargesItem entity, GridCommandEventArgs e)
        {
            var userControl = (ServiceUnitCorrectionEntry)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ChargeQuantity = decimal.Parse("-" + userControl.ChargeQuantity.ToString());
                entity.StockQuantity = userControl.ChargeQuantity;

                entity.Total = Helper.Rounding((entity.ChargeQuantity.Value * ((entity.Price.Value - entity.DiscountAmount.Value) +
                    entity.CitoAmount.Value)), AppEnum.RoundingType.Transaction);

                var consColl = new ItemConsumptionCollection();
                consColl.Query.Where(consColl.Query.ItemID == entity.ItemID);
                consColl.LoadAll();

                foreach (var consEntity in consColl)
                {
                    var consItem = TransChargesItemConsumptions.FindByPrimaryKey(entity.TransactionNo, entity.SequenceNo, consEntity.DetailItemID) ??
                                   TransChargesItemConsumptions.AddNew();
                    consItem.TransactionNo = txtTransactionNo.Text;
                    consItem.SequenceNo = entity.SequenceNo;
                    consItem.DetailItemID = consEntity.DetailItemID;
                    consItem.Qty = entity.ChargeQuantity * consEntity.Qty;
                    consItem.SRItemUnit = consEntity.SRItemUnit;
                }
            }
        }

        #endregion

        protected void grdTransChargesItem_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = TransChargesItems[e.Item.DataSetIndex];
                if (item != null)
                {
                    if (item.IsVoid.Value)
                    {
                        for (int i = 0; i < e.Item.Cells.Count; i++)
                        {
                            if (i > 0 && i < e.Item.Cells.Count)
                                e.Item.Cells[i].Font.Strikeout = true;
                        }
                    }
                }
            }
        }

        protected void grdTransChargesItem_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit" || e.CommandName == "Delete")
            {
                var item = TransChargesItems[e.Item.DataSetIndex];
                if (item != null)
                {
                    if (item.IsBillProceed.Value)
                        e.Canceled = true;
                }
            }
        }

        private TransChargesItemCompCollection TransChargesItemComps
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTransChargesItemComp" + Request.UserHostName];
                    if (obj != null)
                        return ((TransChargesItemCompCollection)(obj));
                }

                var coll = new TransChargesItemCompCollection();
                var query = new TransChargesItemCompQuery();

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(
                        query.SequenceNo.Ascending,
                        query.TariffComponentID.Ascending
                    );

                coll.Load(query);

                Session["collTransChargesItemComp" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collTransChargesItemComp" + Request.UserHostName] = value; }
        }

        private TransChargesItemConsumptionCollection TransChargesItemConsumptions
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTransChargesItemConsumption" + Request.UserHostName];
                    if (obj != null)
                        return ((TransChargesItemConsumptionCollection)(obj));
                }

                var coll = new TransChargesItemConsumptionCollection();
                var query = new TransChargesItemConsumptionQuery();

                query.Where(query.TransactionNo == txtTransactionNo.Text);

                coll.Load(query);

                Session["collTransChargesItemConsumption" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collTransChargesItemConsumption" + Request.UserHostName] = value; }
        }

        private CostCalculationCollection CostCalculations
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = ViewState["collCostCalculation" + Request.UserHostName];
                    if (obj != null)
                        return ((CostCalculationCollection)(obj));
                }

                var coll = new CostCalculationCollection();
                coll.Query.Where(
                        coll.Query.TransactionNo == txtTransactionNo.Text,
                        coll.Query.RegistrationNo == txtRegistrationNo.Text
                    );
                coll.LoadAll();

                ViewState["collCostCalculation" + Request.UserHostName] = coll;
                return coll;
            }
            set { ViewState["collCostCalculation" + Request.UserHostName] = value; }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"];
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind")
            {
                grdTransChargesItem.Rebind();
            }
        }

        private static string InitTransChargesItem(string transactionNo, string sequenceNo)
        {
            var hd = new TransCharges();
            hd.LoadByPrimaryKey(transactionNo);

            var query = new TransChargesItemQuery("a");
            var item = new ItemQuery("b");
            var param = new ParamedicQuery("c");

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    item.ItemName,
                    param.ParamedicName,
                    @"<a.ChargeQuantity + ISNULL((SELECT SUM(ChargeQuantity)
                                                FROM TransChargesItem 
                                                WHERE ReferenceNo = a.TransactionNo
                                                    AND ReferenceSequenceNo = a.SequenceNo
                                                    AND IsApprove = 1 AND IsVoid = 0), 0) AS 'ChargeQuantity'>",
                    query.SRItemUnit,
                    query.Price,
                    query.DiscountAmount,
                    query.CitoAmount,
                    @"<(a.ChargeQuantity + ISNULL((SELECT SUM(ChargeQuantity)
                                                FROM TransChargesItem 
                                                WHERE ReferenceNo = a.TransactionNo
                                                    AND ReferenceSequenceNo = a.SequenceNo
                                                    AND IsApprove = 1 AND IsVoid = 0), 0)) * ((a.Price - a.DiscountAmount) + a.CitoAmount) AS 'Total'>",
                    query.IsOrderRealization
                );
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.LeftJoin(param).On(query.ParamedicID == param.ParamedicID);
            query.Where
                (
                    query.TransactionNo == transactionNo,
                    query.SequenceNo == sequenceNo,
                    query.IsApprove == true,
                    query.IsVoid == false,
                    query.Or(
                        query.ParentNo == string.Empty,
                        query.ParentNo.IsNull()
                        )
                );


            query.OrderBy(query.SequenceNo.Ascending);

            var tbl = query.LoadDataTable();

            foreach (var row in tbl.Rows.Cast<DataRow>().Where(row => (hd.IsOrder ?? false) &&
                                                                      !Convert.ToBoolean(row["IsOrderRealization"])))
            {
                row.Delete();
            }

            tbl.AcceptChanges();

            return tbl.AsEnumerable().Any() ? (Convert.ToDouble(tbl.Rows[0]["ChargeQuantity"]) == 0 ? tbl.Rows[0]["ItemName"].ToString() : string.Empty) : string.Empty;
        }

        private static string InitIntermBill(string transactionNo, string sequenceNo)
        {
            var query = new CostCalculationQuery("a");
            var ib = new IntermBillQuery("b");

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.IntermBillNo
                );
            query.InnerJoin(ib).On(
                query.IntermBillNo == ib.IntermBillNo &&
                ib.IsVoid == false
                );
            query.Where
                (
                    query.TransactionNo == transactionNo,
                    query.SequenceNo == sequenceNo,
                    query.Or(
                        query.ParentNo == string.Empty,
                        query.ParentNo.IsNull()
                        ),
                    query.IntermBillNo.IsNotNull()
                );


            query.OrderBy(query.SequenceNo.Ascending);

            var tbl = query.LoadDataTable();

            return tbl.AsEnumerable().Any() ? tbl.Rows[0]["IntermBillNo"].ToString() : string.Empty;
        }

        #region PatientImage
        private void PopulatePatientImage(string patientID)
        {
            // Load from database
            var patientImg = new PatientImage();
            if (patientImg.LoadByPrimaryKey(patientID))
            {
                // Show Image
                if (patientImg.Photo != null)
                {
                    imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(patientImg.Photo));
                }
                else
                {
                    imgPatientPhoto.ImageUrl = txtGender.Text == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                }
            }
            else
                imgPatientPhoto.ImageUrl = txtGender.Text == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";

        }

        #endregion

        private string GetItemName(string itemId)
        {
            var item = new Item();
            return item.LoadByPrimaryKey(itemId) ? item.ItemName : string.Empty;
        }
    }
}
