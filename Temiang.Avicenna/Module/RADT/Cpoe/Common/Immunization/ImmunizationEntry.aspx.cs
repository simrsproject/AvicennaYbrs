using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class ImmunizationEntry : BasePageDialogEntry
    {
        private AppAutoNumberLast _autoNumber, _amplopFilmAutoNumber;
        private string _healthcareInitial, _paramedicIdDokterLuar;

        private string EntryStatus
        {
            get { return Request.QueryString["mod"]; }
        }        
        private string TransactionNo
        {
            get { return Request.QueryString["id"]; }
        }        
        private string FromServiceUnitID
        {
            get { return Request.QueryString["cid"]; }
        }        

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ServiceUnitTransaction;
            _healthcareInitial = AppSession.Parameter.HealthcareInitial;
            _paramedicIdDokterLuar = AppSession.Parameter.ParamedicIdDokterLuar;

            if (!IsPostBack)
            { var reg = new Registration();
                if (string.IsNullOrEmpty(RegistrationNo))
                {
                    var charges = new TransCharges();
                    charges.LoadByPrimaryKey(TransactionNo);
                    reg.LoadByPrimaryKey(charges.RegistrationNo);
                }
                else
                {
                    reg.LoadByPrimaryKey(RegistrationNo);
                }
               
                pnlLengthOfStay.Visible = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient;
                if (pnlLengthOfStay.Visible)
                {
                    var x = reg.DischargeDate != null ? reg.DischargeDate.Value.Date : DateTime.Now.Date;
                    var y = reg.RegistrationDate.Value.Date;
                    txtLengthOfStay.Value = (x - y).TotalDays + 1;
                }

                pnlResponUnit.Visible = false;

                if (pnlResponUnit.Visible)
                    ComboBox.PopulateWithServiceUnitForTransaction(cboResponUnit, true);
                    pnlJobOrder.Visible = false;
                    pnlJobOrder2.Visible = false;
                    pnlJobOrder3.Visible = false;
                    pnlSurgeryPackage.Visible = false;

                    switch (_healthcareInitial)
                    {
                        case "RSSA":
                            var rooms = new ServiceRoomCollection();
                            rooms.Query.es.Distinct = true;
                            rooms.Query.Select(rooms.Query.ServiceUnitID);
                            rooms.Query.Where(
                                rooms.Query.IsOperatingRoom == true,
                                rooms.Query.IsActive == true,
                                rooms.Query.ServiceUnitID == FromServiceUnitID
                                );
                            rooms.LoadAll();

                            pnlSurgeryPackage.Visible = rooms.Count() > 0;
                            break;
                        case "RSCH":
                            var rooms2 = new ServiceRoomCollection();
                            rooms2.Query.es.Distinct = true;
                            rooms2.Query.Select(rooms2.Query.ServiceUnitID);
                            rooms2.Query.Where(
                                rooms2.Query.IsOperatingRoom == true,
                                rooms2.Query.IsActive == true,
                                rooms2.Query.ServiceUnitID == FromServiceUnitID
                                );
                            rooms2.LoadAll();

                            pnlServiceUnitBookingNo.Visible = rooms2.Count() > 0;

                            pnlKiaCaseType.Visible = (FromServiceUnitID ==
                                                      AppSession.Parameter.ServiceUnitKiaId ||
                                                      FromServiceUnitID ==
                                                      AppSession.Parameter.ServiceUnitImmunizationId);
                            pnlObstetricType.Visible = FromServiceUnitID ==
                                                     AppSession.Parameter.ServiceUnitObstetricsId;
                            break;
                    }

                

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(FromServiceUnitID);
                cboFromServiceUnitID.Items.Add(new RadComboBoxItem(unit.ServiceUnitName, unit.ServiceUnitID));
                if (cboFromServiceUnitID.Items.Count > 1)
                    cboFromServiceUnitID.Items.Remove(cboFromServiceUnitID.Items.Single(c => string.IsNullOrEmpty(c.Value)));

                TransChargesItems = null;
                TransChargesItemComps = null;
                TransChargesItemConsumptions = null;


                    cboBedID.Enabled = false;


                cboFromServiceUnitID.Enabled = false;

                if (!pnlResponUnit.Visible && EntryStatus == "new")
                {
                    var cstext1 = new StringBuilder();
                    cstext1.Append("<script type=text/javascript> __doPostBack('ctl00$ContentPlaceHolder1$grdTransChargesItem$ctl00$ctl02$ctl00$lbInsert','') </script>");

                    Page.ClientScript.RegisterStartupScript(GetType(), "OpenAddNewRecordGrid", cstext1.ToString());
                }
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {

            ajax.AddAjaxSetting(grdTransChargesItem, cboBedID);
            ajax.AddAjaxSetting(cboBedID, grdTransChargesItem);
            ajax.AddAjaxSetting(cboBedID, cboFromServiceUnitID);
            ajax.AddAjaxSetting(cboBedID, txtClassID);
            ajax.AddAjaxSetting(cboBedID, lblClassName);
            ajax.AddAjaxSetting(cboBedID, txtRoomID);
            ajax.AddAjaxSetting(cboBedID, lblRoomName);
            ajax.AddAjaxSetting(cboBedID, txtTariffDiscForRoomIn);
            ajax.AddAjaxSetting(cboBedID, chkIsRoomIn);
            ajax.AddAjaxSetting(cboBedID, cboBedID);

            ajax.AddAjaxSetting(grdTransChargesItem, grdTransChargesItem);
            ajax.AddAjaxSetting(txtBarcodeEntry, txtBarcodeEntry);
            ajax.AddAjaxSetting(txtBarcodeEntry, grdTransChargesItem);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new TransCharges());

            var timeNow = (new DateTime()).NowAtSqlServer();
            txtTransactionDate.SelectedDate = timeNow;
            txtTransactionTime.Text = timeNow.ToString("HH:mm");

            _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, AppEnum.AutoNumber.TransactionNo);
            txtTransactionNo.Text = _autoNumber.LastCompleteNumber;

            txtExecutionDate.SelectedDate = timeNow;
            txtExecutionTime.Text = timeNow.ToString("HH:mm");

            txtRegistrationNo.Text = Request.QueryString["regno"];

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            cboFromServiceUnitID.SelectedValue = FromServiceUnitID;

            if (pnlKiaCaseType.Visible && !string.IsNullOrEmpty(reg.SRKiaCaseType))
            {
                var kiaCase = new AppStandardReferenceItemQuery();
                kiaCase.Where(kiaCase.ItemID == reg.SRKiaCaseType,
                              kiaCase.StandardReferenceID == AppEnum.StandardReference.KiaCaseType);

                cboSRKiaCaseType.DataSource = kiaCase.LoadDataTable();
                cboSRKiaCaseType.DataBind();
                cboSRKiaCaseType.SelectedValue = reg.SRKiaCaseType;
            }

            if (pnlObstetricType.Visible && !string.IsNullOrEmpty(reg.SRObstetricType))
            {
                var obstetricType = new AppStandardReferenceItemQuery();
                obstetricType.Where(obstetricType.ItemID == reg.SRObstetricType,
                              obstetricType.StandardReferenceID == AppEnum.StandardReference.ObstetricType);

                cboSRObstetricType.DataSource = obstetricType.LoadDataTable();
                cboSRObstetricType.DataBind();
                cboSRObstetricType.SelectedValue = reg.SRObstetricType;
            }

            var par = new ParamedicQuery();
            par.Where(par.ParamedicID == ParamedicID);

            cboParamedicID.DataSource = par.LoadDataTable();
            cboParamedicID.DataBind();
            cboParamedicID.SelectedValue = ParamedicID;

            var patient = new Patient();
            patient.LoadByPrimaryKey(reg.PatientID);
            txtMedicalNo.Text = patient.MedicalNo;
            txtPatientName.Text = patient.PatientName;
            txtGender.Text = patient.Sex;
            txtAgeInYear.Text = Convert.ToString(reg.AgeInYear);
            txtAgeInMonth.Text = Convert.ToString(reg.AgeInMonth);
            txtAgeInDay.Text = Convert.ToString(reg.AgeInDay);

            var rooms = new ServiceRoomCollection();
            rooms.Query.Where(rooms.Query.IsOperatingRoom == true &&
                              rooms.Query.ServiceUnitID == cboFromServiceUnitID.SelectedValue);
            rooms.LoadAll();

            txtRoomID.Text = reg.RoomID;
            var room = new ServiceRoom();
            room.LoadByPrimaryKey(txtRoomID.Text);
            lblRoomName.Text = room.RoomName;
            txtTariffDiscForRoomIn.Value = Convert.ToDouble(room.TariffDiscountForRoomIn);

            if (rooms.Count > 0)
            {
                txtClassID.Text = string.IsNullOrEmpty(reg.ProcedureChargeClassID)
                                      ? reg.ChargeClassID
                                      : reg.ProcedureChargeClassID;
            }
            else
                txtClassID.Text = reg.ChargeClassID;
            var c = new Class();
            c.LoadByPrimaryKey(txtClassID.Text);
            lblClassName.Text = c.ClassName;

            PopulateBedCollection(reg);
            cboBedID.SelectedValue = reg.ServiceUnitID + ", " + reg.RoomID + ", " + reg.ChargeClassID + ", " + reg.BedID;
            chkIsRoomIn.Checked = reg.IsRoomIn ?? false;

            var guar = new GuarantorQuery();
            guar.Where(guar.GuarantorID == (string.IsNullOrEmpty(patient.str.MemberID) ? reg.GuarantorID : patient.MemberID));
            cboGuarantorID.DataSource = guar.LoadDataTable();
            cboGuarantorID.DataBind();
            cboGuarantorID.SelectedValue = string.IsNullOrEmpty(patient.str.MemberID) ? reg.GuarantorID : patient.MemberID;

            var parId = !string.IsNullOrEmpty(reg.ParamedicID)
                                   ? reg.ParamedicID
                                   : ParamedicID;
            if (parId == _paramedicIdDokterLuar)
                txtPhysicianSenders.Text = reg.PhysicianSenders;
            else
            {
                bool isDefaultFromReg = false;
                if (_healthcareInitial == "RSCH")
                {
                    var query = new ServiceUnitTransactionCodeQuery("a");
                    var qrServ = new ServiceUnitQuery("c");

                    query.es.Distinct = true;
                    query.Select(qrServ.ServiceUnitID, qrServ.ServiceUnitName);
                    query.InnerJoin(qrServ).On(query.ServiceUnitID == qrServ.ServiceUnitID);
                    query.Where
                        (
                            query.SRTransactionCode == TransactionCode.JobOrder,
                            qrServ.IsActive == true,
                            qrServ.IsUsingJobOrder == true,
                            qrServ.ServiceUnitID == cboFromServiceUnitID.SelectedValue
                        );

                    DataTable dtb = query.LoadDataTable();
                    if (dtb.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(reg.ReferralID))
                        {
                            var r = new Referral();
                            if (r.LoadByPrimaryKey(reg.ReferralID))
                                txtPhysicianSenders.Text = r.ReferralName;
                            else
                                isDefaultFromReg = true;
                        }
                        else
                            isDefaultFromReg = true;
                    }
                    else
                        isDefaultFromReg = true;
                }
                else
                    isDefaultFromReg = true;

                if (isDefaultFromReg)
                {
                    var p = new Paramedic();
                    p.LoadByPrimaryKey(parId);
                    txtPhysicianSenders.Text = p.ParamedicName;
                }
            }

            chkIsProceed.Checked = false;

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuEditClick()
        {
            cboBedID.Enabled = false;
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

            if (txtExecutionDate.SelectedDate.Value.Date > txtTransactionDate.SelectedDate.Value.Date)
            {
                args.MessageText = string.Format("Execution Date should not be greater than Transaction Date.");
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

            if (txtExecutionDate.SelectedDate.Value.Date > txtTransactionDate.SelectedDate.Value.Date)
            {
                args.MessageText = string.Format("Execution Date should not be greater than Transaction Date.");
                args.IsCancel = true;
                return;
            }

            var entity = new TransCharges();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
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

            var reg = new Registration();
            reg.LoadByPrimaryKey(entity.RegistrationNo);

            if (reg.IsClosed ?? false)
            {
                args.MessageText = string.Format("Registration has been closed.");
                args.IsCancel = true;
                return;
            }

            if (reg.IsHoldTransactionEntry ?? false)
            {
                args.MessageText = string.Format("Transaction is locked.");
                args.IsCancel = true;
                return;
            }


            SetApproval(entity, true, args);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            var detail = new TransChargesItemQuery();
            detail.Where
                (
                    detail.TransactionNo == txtTransactionNo.Text,
                    detail.IsOrderRealization == true,
                    detail.IsVoid == false
                );

            if (detail.LoadDataTable().Rows.Count > 0)
            {
                args.MessageText = "This transaction has been proceed. Data can't be canceled";
                args.IsCancel = true;
                return;
            }

            var pay = new TransPaymentItemOrderCollection();
            pay.Query.Where(pay.Query.TransactionNo == txtTransactionNo.Text, pay.Query.IsPaymentReturned == false);
            pay.LoadAll();
            if (pay.Count > 0)
            {
                args.MessageText = "This transaction has been paid. Data can't be canceled";
                args.IsCancel = true;
                return;
            }

            var cc = new CostCalculationCollection();
            cc.Query.Where(cc.Query.TransactionNo == txtTransactionNo.Text, cc.Query.IntermBillNo.IsNotNull());
            cc.LoadAll();
            if (cc.Count > 0)
            {
                args.MessageText = "Transaction is already on interm bill. Data can't be canceled";
                args.IsCancel = true;
                return;
            }

            using (var trans = new esTransactionScope())
            {
                //entity.IsApproved = false;
                //entity.IsBillProceed = false;
                //entity.IsProceed = false;
                //entity.IsVoid = true;
                //entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                //entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer(); 

                //foreach (var item in TransChargesItems)
                //{
                //    item.IsApprove = false;
                //    item.IsBillProceed = false;
                //    item.IsVoid = true;
                //    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                //    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer(); 
                //}

                //if (entity.IsPackage ?? false)
                //{
                var headers = new TransChargesCollection();
                headers.Query.Where(
                    headers.Query.Or(
                        headers.Query.TransactionNo == txtTransactionNo.Text,
                        headers.Query.PackageReferenceNo == txtTransactionNo.Text
                        )
                    );
                headers.LoadAll();

                var items = new TransChargesItemCollection();
                items.Query.Where(items.Query.TransactionNo.In(headers.Select(h => h.TransactionNo)));
                items.LoadAll();

                foreach (var item in items)
                {
                    item.IsApprove = false;
                    item.IsBillProceed = false;
                    item.IsVoid = true;
                }

                items.Save();

                ItemBalance.PrepareItemBalancesForMCUCorrection(headers, AppSession.UserLogin.UserID, AppSession.Parameter.IsEnabledStockWithEdControl);

                foreach (var header in headers)
                {
                    var comps = new TransChargesItemCompCollection();
                    var cost = new CostCalculationCollection();

                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(header.ToServiceUnitID);

                    if ((!(header.IsOrder ?? false)) && (header.IsApproved ?? false) && (!(header.IsPackage ?? false)))
                    {
                        comps.Query.Where(comps.Query.TransactionNo == header.TransactionNo);
                        comps.LoadAll();

                        cost.Query.Where(cost.Query.TransactionNo == header.TransactionNo);
                        cost.LoadAll();

                        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                        {
                            if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                            {
                                JournalTransactions.AddNewPatientIncomeAccrualUnapproval(header.TransactionNo, DateTime.Now.Date, AppSession.UserLogin.UserID, 0);
                            }
                            else {
                                var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                                if (type.Contains(reg.SRRegistrationType))
                                {
                                    int? journalId = JournalTransactions.AddNewIncomeCorrectionJournalTemporary(header, comps, reg, unit, cost, "SC", AppSession.UserLogin.UserID, 0);
                                }
                            }
                        }
                    }
                    else if ((header.IsOrder ?? false) && (header.IsBillProceed ?? false))
                    {
                        var cuery = new TransChargesItemCompQuery("a");
                        var tuery = new TransChargesItemQuery("b");

                        cuery.InnerJoin(tuery).On(
                            cuery.TransactionNo == tuery.TransactionNo &&
                            cuery.SequenceNo == tuery.SequenceNo &&
                            tuery.IsOrderRealization == true
                            );
                        cuery.Where(cuery.TransactionNo == header.TransactionNo);

                        comps.Load(cuery);

                        var cst = new CostCalculationQuery("a");
                        tuery = new TransChargesItemQuery("b");

                        cst.InnerJoin(tuery).On(
                            cst.TransactionNo == tuery.TransactionNo &&
                            cst.SequenceNo == tuery.SequenceNo &&
                            tuery.IsOrderRealization == true
                            );
                        cst.Where(cst.TransactionNo == header.TransactionNo);

                        cost.Load(cst);

                        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                        {
                            if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                            {
                                JournalTransactions.AddNewPatientIncomeAccrualUnapproval(header.TransactionNo, DateTime.Now.Date, AppSession.UserLogin.UserID, 0);
                            }
                            else {
                                var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                                if (type.Contains(reg.SRRegistrationType))
                                {
                                    int? journalId = JournalTransactions.AddNewIncomeCorrectionJournalTemporary(header, comps, reg, unit, cost, "SC", AppSession.UserLogin.UserID, 0);
                                }
                            }
                        }
                    }

                    cost.MarkAllAsDeleted();
                    cost.Save();

                    header.IsApproved = false;
                    header.IsBillProceed = false;
                    header.IsProceed = false;
                    header.IsVoid = true;
                    header.VoidDateTime = DateTime.Now;
                }

                headers.Save();

                //if (AppSession.Parameter.IsPhysicianFeeBasedOnPayment == "Yes")
                //{
                //    int? x = ParamedicFeeTransChargesItemCompSettled.AddReturnSettled(headers, AppSession.UserLogin.UserID);
                //}

                //}

                trans.Complete();
            }
        }

        private static bool IsServiceUnitOrder(string serviceUnitID)
        {
            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(serviceUnitID);
            return unit.IsUsingJobOrder ?? false;
        }

        private void SetApproval(TransCharges entity, bool isApproval, ValidateArgs args)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(entity.RegistrationNo);

            using (var trans = new esTransactionScope())
            {
                //package manipulation
                if (entity.IsPackage ?? false)
                {
                    var headers = new TransChargesCollection();
                    var details = new TransChargesItemCollection();
                    var components = new TransChargesItemCompCollection();
                    var consumptions = new TransChargesItemConsumptionCollection();

                    var pacs = (TransChargesItems.Where(i => !string.IsNullOrEmpty(i.ParentNo))
                                                 .GroupBy(i => new
                                                 {
                                                     i.ParentNo,
                                                     i.ToServiceUnitID
                                                 })
                                                 .Select(g => new
                                                 {
                                                     g.Key.ParentNo,
                                                     g.Key.ToServiceUnitID,
                                                     IsOrder = IsServiceUnitOrder(g.Key.ToServiceUnitID)
                                                 })).Distinct();

                    foreach (var pac in pacs)
                    {
                        var autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date,
                            pac.IsOrder ? AppEnum.AutoNumber.JobOrderNo : AppEnum.AutoNumber.TransactionNo);
                        var transactionNo = autoNumber.LastCompleteNumber;
                        autoNumber.Save();

                        //header
                        #region header
                        var header = headers.AddNew();
                        header.TransactionNo = transactionNo;
                        header.RegistrationNo = entity.RegistrationNo;
                        header.TransactionDate = entity.TransactionDate;
                        header.ExecutionDate = entity.ExecutionDate;
                        header.ReferenceNo = string.Empty;
                        header.ResponUnitID = String.Empty;
                        header.FromServiceUnitID = (pac.IsOrder) ? entity.FromServiceUnitID : pac.ToServiceUnitID;
                        header.IsBillProceed = false;
                        header.IsApproved = pac.IsOrder;
                        header.ToServiceUnitID = pac.ToServiceUnitID;
                        header.ClassID = entity.ClassID;
                        header.RoomID = entity.RoomID;
                        header.BedID = entity.BedID;
                        header.DueDate = entity.DueDate;
                        header.SRShift = entity.SRShift;
                        header.SRItemType = string.Empty;
                        header.IsProceed = false;
                        header.IsVoid = false;
                        header.IsAutoBillTransaction = false;
                        header.IsOrder = pac.IsOrder;
                        header.IsCorrection = false;
                        header.Notes = string.Empty;
                        header.IsPackage = false;
                        header.PackageReferenceNo = entity.TransactionNo;
                        header.SurgicalPackageID = String.Empty;
                        #endregion

                        var tcis = TransChargesItems.Where(t => t.ParentNo == pac.ParentNo &&
                                                                t.ToServiceUnitID == pac.ToServiceUnitID)
                                                    .OrderBy(t => t.SequenceNo);

                        foreach (var tci in tcis)
                        {
                            //detail
                            #region detail
                            var detail = details.AddNew();
                            detail.TransactionNo = header.TransactionNo;
                            detail.SequenceNo = tci.SequenceNo;
                            detail.ReferenceNo = tci.ReferenceNo;
                            detail.ReferenceSequenceNo = tci.ReferenceSequenceNo;
                            detail.ItemID = tci.ItemID;
                            detail.ChargeClassID = tci.ChargeClassID;
                            detail.ParamedicID = tci.ParamedicID;
                            detail.SecondParamedicID = tci.SecondParamedicID;
                            detail.IsAdminCalculation = tci.IsAdminCalculation;
                            detail.IsVariable = tci.IsVariable;
                            detail.IsCito = tci.IsCito;
                            detail.ChargeQuantity = tci.ChargeQuantity;
                            detail.StockQuantity = tci.StockQuantity;
                            detail.SRItemUnit = tci.SRItemUnit;
                            detail.CostPrice = tci.CostPrice;
                            detail.Price = tci.Price;
                            detail.DiscountAmount = tci.DiscountAmount;
                            detail.CitoAmount = tci.CitoAmount;
                            detail.RoundingAmount = tci.RoundingAmount;
                            detail.SRDiscountReason = tci.SRDiscountReason;
                            detail.IsAssetUtilization = tci.IsAssetUtilization;
                            detail.AssetID = tci.AssetID;
                            detail.IsBillProceed = false;
                            detail.IsOrderRealization = tci.IsOrderRealization;
                            detail.IsPackage = tci.IsPackage;
                            detail.IsApprove = (tci.IsVoid ?? false) ? false : pac.IsOrder;
                            detail.IsVoid = tci.IsVoid;
                            detail.Notes = tci.Notes;
                            detail.FilmNo = tci.FilmNo;
                            detail.LastUpdateDateTime = tci.LastUpdateDateTime;
                            detail.LastUpdateByUserID = tci.LastUpdateByUserID;
                            detail.ParentNo = string.Empty;
                            detail.SRCenterID = tci.SRCenterID;
                            detail.AutoProcessCalculation = tci.AutoProcessCalculation;
                            detail.ParamedicCollectionName = tci.ParamedicCollectionName;
                            detail.ToServiceUnitID = tci.ToServiceUnitID;
                            detail.IsCitoInPercent = tci.IsCitoInPercent;
                            detail.BasicCitoAmount = tci.BasicCitoAmount;
                            detail.IsItemRoom = tci.IsItemRoom;
                            detail.IsItemRoom = false;

                            if (tci.IsExtraItem ?? false)
                            {
                                detail.IsExtraItem = tci.IsExtraItem;
                                detail.IsSelectedExtraItem = tci.IsSelectedExtraItem;
                            }

                            #endregion

                            var tcics = TransChargesItemComps.Where(t => t.SequenceNo == tci.SequenceNo)
                                                             .OrderBy(t => t.TariffComponentID);
                            //component
                            #region component
                            foreach (var tcic in tcics)
                            {
                                var component = TransChargesItemComps.AddNew();
                                component.TransactionNo = detail.TransactionNo;
                                component.SequenceNo = detail.SequenceNo;
                                component.TariffComponentID = tcic.TariffComponentID;
                                component.Price = tcic.Price;
                                component.DiscountAmount = tcic.DiscountAmount;
                                component.ParamedicID = tcic.ParamedicID;
                                component.LastUpdateDateTime = tcic.LastUpdateDateTime;
                                component.LastUpdateByUserID = tcic.LastUpdateByUserID;
                                component.IsPackage = tcic.IsPackage;
                                component.AutoProcessCalculation = tcic.AutoProcessCalculation;
                                component.CitoAmount = tcic.CitoAmount;

                                tcic.MarkAsDeleted();
                            }
                            #endregion

                            var cons = TransChargesItemConsumptions.Where(t => t.SequenceNo == tci.SequenceNo)
                                                                   .OrderBy(t => t.DetailItemID);
                            //consumption
                            #region consumption
                            foreach (var con in cons)
                            {
                                var consumption = consumptions.AddNew();
                                consumption.TransactionNo = detail.TransactionNo;
                                consumption.SequenceNo = detail.SequenceNo;
                                consumption.DetailItemID = con.DetailItemID;
                                consumption.Qty = con.Qty;
                                consumption.QtyRealization = con.QtyRealization;
                                consumption.SRItemUnit = con.SRItemUnit;
                                consumption.Price = con.Price;
                                consumption.AveragePrice = con.AveragePrice;
                                consumption.FifoPrice = con.FifoPrice;
                                consumption.LastUpdateDateTime = con.LastUpdateDateTime;
                                consumption.LastUpdateByUserID = con.LastUpdateByUserID;
                                consumption.IsPackage = con.IsPackage;

                                con.MarkAsDeleted();
                            }
                            #endregion

                            tci.MarkAsDeleted();
                        }
                    }

                    headers.Save();
                    details.Save();
                    components.Save();
                    consumptions.Save();

                    TransChargesItems.Save();
                    TransChargesItemComps.Save();
                    TransChargesItemConsumptions.Save();
                }

                //header
                entity.IsApproved = isApproval;

                var grrID = reg.GuarantorID;

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                if (grrID == AppSession.Parameter.SelfGuarantor)
                {
                    if (!string.IsNullOrEmpty(pat.MemberID))
                        grrID = pat.MemberID;
                }

                var grr = new Guarantor();
                grr.LoadByPrimaryKey(reg.GuarantorID);

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(entity.ToServiceUnitID);

                if (!isApproval) CostCalculations.MarkAllAsDeleted();

                entity.Save();

                //Commit if success, Rollback if failed
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
            if (entity.IsBillProceed ?? false)
            {
                args.MessageText = "This data has been proceed to billing.";
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

        private void SetVoid(TransCharges entity, bool isVoid)
        {
            //header
            entity.IsVoid = isVoid;
            if (isVoid)
                entity.IsApproved = false;

            //detail
            foreach (TransChargesItem item in TransChargesItems)
            {
                item.IsVoid = isVoid;
                if (isVoid)
                    item.IsApprove = false;
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

        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var entity = new TransCharges();
            if (string.IsNullOrWhiteSpace(TransactionNo))
            {
                    entity.LoadByPrimaryKey(TransactionNo);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
            OnPopulateEntryControl(entity);
        }

        private void OnPopulateEntryControl(esEntity entity)
        {
            var transCharges = (TransCharges)entity;
            txtTransactionNo.Text = transCharges.TransactionNo;
            ViewState["IsApproved"] = transCharges.IsApproved ?? false;
            ViewState["IsVoid"] = transCharges.IsVoid ?? false;

            txtRegistrationNo.Text = transCharges.RegistrationNo;
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.str.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;
                txtGender.Text = pat.Sex;
                txtAgeInYear.Text = Convert.ToString(reg.AgeInYear);
                txtAgeInMonth.Text = Convert.ToString(reg.AgeInMonth);
                txtAgeInDay.Text = Convert.ToString(reg.AgeInDay);

                var par = new ParamedicQuery();
                par.Where(par.ParamedicID == reg.ParamedicID);
                cboParamedicID.DataSource = par.LoadDataTable();
                cboParamedicID.DataBind();
                cboParamedicID.SelectedValue = string.IsNullOrEmpty(reg.str.ParamedicID) ? ParamedicID : reg.ParamedicID;

                var guar = new GuarantorQuery();
                guar.Where(guar.GuarantorID == (string.IsNullOrEmpty(pat.str.MemberID) ? reg.GuarantorID : pat.MemberID));
                cboGuarantorID.DataSource = guar.LoadDataTable();
                cboGuarantorID.DataBind();
                cboGuarantorID.SelectedValue = string.IsNullOrEmpty(pat.str.MemberID) ? reg.GuarantorID : pat.MemberID;

                PopulateBedCollection(reg);
                if (pnlKiaCaseType.Visible && !string.IsNullOrEmpty(reg.SRKiaCaseType))
                {
                    var kiaCase = new AppStandardReferenceItemQuery();
                    kiaCase.Where(kiaCase.ItemID == reg.SRKiaCaseType,
                                  kiaCase.StandardReferenceID == AppEnum.StandardReference.KiaCaseType);

                    cboSRKiaCaseType.DataSource = kiaCase.LoadDataTable();
                    cboSRKiaCaseType.DataBind();
                    cboSRKiaCaseType.SelectedValue = reg.SRKiaCaseType;
                }
                if (pnlObstetricType.Visible && !string.IsNullOrEmpty(reg.SRObstetricType))
                {
                    var obstetricType = new AppStandardReferenceItemQuery();
                    obstetricType.Where(obstetricType.ItemID == reg.SRObstetricType,
                                  obstetricType.StandardReferenceID == AppEnum.StandardReference.ObstetricType);

                    cboSRObstetricType.DataSource = obstetricType.LoadDataTable();
                    cboSRObstetricType.DataBind();
                    cboSRObstetricType.SelectedValue = reg.SRObstetricType;
                }
            }

            if (transCharges.TransactionDate.HasValue)
            {
                txtTransactionDate.SelectedDate = transCharges.TransactionDate.Value.Date;
                txtTransactionTime.Text = transCharges.TransactionDate.Value.ToString("HH:mm");
            }

            if (transCharges.ExecutionDate.HasValue)
            {
                txtExecutionDate.SelectedDate = transCharges.ExecutionDate.Value.Date;
                txtExecutionTime.Text = transCharges.ExecutionDate.Value.ToString("HH:mm");
            }

            if (pnlResponUnit.Visible)
                cboResponUnit.SelectedValue = transCharges.ToServiceUnitID;

            cboFromServiceUnitID.SelectedValue = transCharges.FromServiceUnitID;


            txtNotes.Text = transCharges.Notes;
            chkIsProceed.Checked = transCharges.IsProceed ?? false;

            if (pnlResponUnit.Visible)
                cboResponUnit.SelectedValue = transCharges.str.ResponUnitID;

            txtRoomID.Text = transCharges.RoomID;
            var room = new ServiceRoom();
            if (room.LoadByPrimaryKey(txtRoomID.Text))
            {
                lblRoomName.Text = room.RoomName;
                txtTariffDiscForRoomIn.Value = Convert.ToDouble(room.TariffDiscountForRoomIn);
            }
            else
                txtTariffDiscForRoomIn.Value = 0;

            txtClassID.Text = transCharges.ClassID;
            var c = new Class();
            if (c.LoadByPrimaryKey(txtClassID.Text))
                lblClassName.Text = c.ClassName;

            if (!string.IsNullOrEmpty(transCharges.BedID))
            {
                cboBedID.SelectedValue = transCharges.ToServiceUnitID + ", " + transCharges.RoomID + ", " + transCharges.ClassID + ", " + transCharges.BedID;
                chkIsRoomIn.Checked = transCharges.IsRoomIn ?? false;
            }
            else
                chkIsRoomIn.Checked = false;

            if (pnlSurgeryPackage.Visible && !string.IsNullOrEmpty(transCharges.SurgicalPackageID))
            {
                var query = new SurgicalPackageQuery();
                query.Select(query.PackageID, query.PackageName);
                query.Where(query.PackageID == transCharges.SurgicalPackageID);
                DataTable dtb = query.LoadDataTable();
                cboSurgeryPackageID.DataSource = dtb;
                cboSurgeryPackageID.DataBind();
                cboSurgeryPackageID.SelectedValue = transCharges.SurgicalPackageID;
            }
            if (pnlServiceUnitBookingNo.Visible && !string.IsNullOrEmpty(transCharges.ServiceUnitBookingNo))
            {
                var query = new ServiceUnitBookingQuery("a");
                var par = new ParamedicQuery("b");
                query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID);
                query.Select(query.BookingNo, query.BookingDateTimeFrom, par.ParamedicName);
                query.Where(query.BookingNo == transCharges.ServiceUnitBookingNo);
                DataTable dtb = query.LoadDataTable();
                cboServiceUnitBookingNo.DataSource = dtb;
                cboServiceUnitBookingNo.DataBind();
                cboServiceUnitBookingNo.SelectedValue = transCharges.ServiceUnitBookingNo;
            }

            //Display Data Detail
            PopulateTransChargesItemGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(TransCharges entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, AppEnum.AutoNumber.TransactionNo);
                txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            entity.TransactionNo = txtTransactionNo.Text;
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.TransactionDate = DateTime.Parse(txtTransactionDate.SelectedDate.Value.ToShortDateString() + " " + txtTransactionTime.TextWithLiterals);
            entity.ExecutionDate = DateTime.Parse(txtExecutionDate.SelectedDate.Value.ToShortDateString() + " " + txtExecutionTime.TextWithLiterals);
            entity.ReferenceNo = string.Empty;
            entity.ResponUnitID = pnlResponUnit.Visible ? cboResponUnit.SelectedValue : String.Empty;
            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            entity.IsBillProceed = false;
            entity.IsApproved = false;

                entity.ToServiceUnitID = pnlResponUnit.Visible ? cboResponUnit.SelectedValue : cboFromServiceUnitID.SelectedValue;

            entity.ClassID = txtClassID.Text;
            entity.RoomID = txtRoomID.Text;
            entity.BedID = cboBedID.SelectedValue == string.Empty ? string.Empty : cboBedID.SelectedValue.Split(',')[3].Trim();
            entity.IsRoomIn = chkIsRoomIn.Checked;
            entity.TariffDiscountForRoomIn = Convert.ToDecimal(txtTariffDiscForRoomIn.Value);
            entity.DueDate = txtTransactionDate.SelectedDate;
            entity.SRShift = Registration.GetShiftID();
            entity.SRItemType = string.Empty;
            entity.IsProceed = chkIsProceed.Checked;
            entity.IsVoid = false;
            entity.IsAutoBillTransaction = false;
            entity.IsOrder = false;
            entity.IsCorrection = false;
            entity.Notes = txtNotes.Text;

            entity.SurgicalPackageID = cboSurgeryPackageID.SelectedValue;
            entity.ServiceUnitBookingNo = cboServiceUnitBookingNo.SelectedValue;

            entity.IsPackage = false;

            entity.PhysicianSenders = string.Empty;

            //Last Update Status Detail
            foreach (var item in TransChargesItems)
            {
                item.TransactionNo = entity.TransactionNo;
                item.IsBillProceed = false;
                item.IsApprove = false;
            }

            if (TransChargesItemComps.Count > 0)
            {
                foreach (var comp in TransChargesItemComps)
                {
                    comp.TransactionNo = entity.TransactionNo;
                }
            }

            if (TransChargesItemConsumptions.Count > 0)
            {
                foreach (var cons in TransChargesItemConsumptions)
                {
                    cons.TransactionNo = entity.TransactionNo;
                }
            }
        }

        private void SaveEntity(TransCharges entity)
        {
            using (var trans = new esTransactionScope())
            {
                //autonumber has been saved on SetEntity
                //if (DataModeCurrent == AppEnum.DataMode.New)
                //    _autoNumber.Save();

                entity.Save();

                TransChargesItems.Save();
                TransChargesItemComps.Save();
                TransChargesItemConsumptions.Save();

                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                if (pnlKiaCaseType.Visible || pnlObstetricType.Visible)
                {
                    if (pnlKiaCaseType.Visible)
                        reg.SRKiaCaseType = cboSRKiaCaseType.SelectedValue;
                    if (pnlObstetricType.Visible)
                        reg.SRObstetricType = cboSRObstetricType.SelectedValue;
                    reg.Save();
                }

                var patient = new Patient();
                patient.LoadByPrimaryKey(reg.PatientID);

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }


        #endregion

        #region Record Detail Method Function TransChargesItem

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
                var tci = new TransChargesItemQuery("d");
                var tounit = new ServiceUnitQuery("e");

                tci.Select(tci.TransactionNo, tci.SequenceNo);
                tci.Where(tci.TransactionNo == query.TransactionNo, tci.SequenceNo == query.SequenceNo,
                          tci.IsExtraItem == true,
                          tci.IsSelectedExtraItem == false);


                var total = new esQueryItem(query, "Total", esSystemType.Decimal);
                total = ((query.ChargeQuantity * query.Price) - query.DiscountAmount) + query.CitoAmount;

                query.Select
                    (
                        query,
                        total.As("refToTransChargesItem_Total"),
                        item.ItemName.As("refToItem_ItemName"),
                        param.ParamedicName.As("refToParamedic_ParamedicName"),
                        tounit.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"),
                        "<CAST((CASE WHEN b.SRItemType IN ('" + ItemType.Medical + "', '" + ItemType.NonMedical + "') THEN 0 ELSE 1 END) AS BIT) AS refTo_IsItemTypeService>"
                    );

                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.LeftJoin(param).On(query.ParamedicID == param.ParamedicID);
                query.LeftJoin(tounit).On(query.ToServiceUnitID == tounit.ServiceUnitID);

                query.Where(query.TransactionNo == txtTransactionNo.Text, query.NotExists(tci));

                query.OrderBy(query.SequenceNo.Ascending);
                DataTable dtb = query.LoadDataTable();
                coll.Load(query);

                Session["collTransChargesItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collTransChargesItem" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemTransChargesItem(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdTransChargesItem.Columns[0].Visible = isVisible;
            grdTransChargesItem.Columns[1].Visible = isVisible;
            grdTransChargesItem.Columns[2].Visible = isVisible;

            grdTransChargesItem.Columns[grdTransChargesItem.Columns.Count - 1].Visible = isVisible;

            grdTransChargesItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

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
                SetEntityValue(entity, e, false);

            if (pnlResponUnit.Visible)
                cboResponUnit.Enabled = (TransChargesItems.Count == 0);
            cboBedID.Enabled = false;
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
                if (EntryStatus == "new")
                {
                    if (!(entity.IsPackage ?? false))
                    {
                        entity.MarkAsDeleted();

                        foreach (var comps in TransChargesItemComps.Where(comps => comps.SequenceNo == sequenceNo))
                        {
                            comps.MarkAsDeleted();
                        }

                        foreach (var consm in TransChargesItemConsumptions.Where(consm => consm.SequenceNo == sequenceNo))
                        {
                            consm.MarkAsDeleted();
                        }
                    }
                    else
                    {
                        foreach (TransChargesItem pac in TransChargesItems.Where(pac => pac.ParentNo == sequenceNo || pac.SequenceNo == sequenceNo))
                        {
                            foreach (var comp in TransChargesItemComps.Where(comp => comp.SequenceNo == pac.SequenceNo))
                            {
                                comp.MarkAsDeleted();
                            }

                            foreach (var cons in TransChargesItemConsumptions.Where(cons => cons.SequenceNo == pac.SequenceNo))
                            {
                                cons.MarkAsDeleted();
                            }

                            pac.MarkAsDeleted();
                        }

                    }
                    entity.MarkAsDeleted();
                }
                else
                {
                    var transactionNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransChargesItemMetadata.ColumnNames.TransactionNo]);
                    var hd = new TransCharges();
                    if (hd.LoadByPrimaryKey(transactionNo))
                    {
                        if (string.IsNullOrEmpty(hd.PackageReferenceNo))
                            entity.MarkAsDeleted();
                        else
                            entity.IsVoid = true;
                    }
                }
            }

            if (pnlResponUnit.Visible)
                cboResponUnit.Enabled = (TransChargesItems.Count == 0);
            cboBedID.Enabled = false;
        }

        protected void grdTransChargesItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = TransChargesItems.AddNew();
            SetEntityValue(entity, e, true);

            e.Canceled = true;
            grdTransChargesItem.Rebind();

            if (pnlResponUnit.Visible)
                cboResponUnit.Enabled = (TransChargesItems.Count == 0);
            cboBedID.Enabled = false;
        }

        private TransChargesItem FindTransChargesItem(String sequenceNo)
        {
            return TransChargesItems.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        private void SetEntityValue(TransChargesItem entity, GridCommandEventArgs e, bool isInsertCommand)
        {
            var userControl = (ImmunizationDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                SetEntityDetail(entity, userControl.SequenceNo, userControl.ItemID, userControl.ItemName, userControl.ParamedicID,
                    userControl.ParamedicName, userControl.IsAdminCalculation, userControl.IsVariable, userControl.IsCito,
                    userControl.ChargeQuantity, userControl.StockQuantity, userControl.SRItemUnit, 0,
                    userControl.Price, userControl.DiscountAmount, userControl.SRDiscountReason, userControl.IsAssetUtilization,
                    userControl.AssetID, userControl.IsPackage, userControl.IsVoid, userControl.Notes, userControl.CenterID,
                    userControl.TariffComponent, userControl.IsNewRecord, userControl.IsItemRoom, isInsertCommand);
            }
        }

        private void SetEntityDetail(TransChargesItem entity, string sequenceNo, string itemID, string itemName, string paramedicID,
             string paramedicName, bool? isAdminCalculation, bool? isVariable, bool? isCito, decimal? chargeQuantity,
             decimal? stockQuantity, string srItemUnit, decimal? costPrice, decimal? price, decimal? discountAmount,
             string srDiscountReason, bool? isAssetUtilization, string assetID, bool? isPackage, bool? isVoid, string notes, string centerID,
             IEnumerable<TransChargesItemComp> tariffComponents, bool isNewRecord, bool? isItemRoom, bool isInsertCommand)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.SequenceNo = sequenceNo;
            entity.ParentNo = string.Empty;
            entity.ReferenceNo = string.Empty;
            entity.ReferenceSequenceNo = string.Empty;
            entity.ItemID = itemID;
            entity.ItemName = itemName;

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            entity.ChargeClassID = txtClassID.Text;
            entity.ParamedicID = paramedicID;
            entity.ParamedicName = paramedicName;
            entity.IsAdminCalculation = isAdminCalculation;
            entity.IsVariable = isVariable;
            entity.IsCito = isCito;
            entity.ChargeQuantity = chargeQuantity;
            entity.StockQuantity = stockQuantity;
            entity.SRItemUnit = srItemUnit;
            entity.CostPrice = costPrice;
            entity.Price = price;

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            if (!(entity.IsCito ?? false))
            {
                entity.CitoAmount = 0;
                entity.IsCitoInPercent = false;
                entity.BasicCitoAmount = 0;
            }
            else
            {
                var tariff = new ItemTariff();
                if (!tariff.Load(GetItemTariffQuery(grr.SRTariffType, entity.ChargeClassID, entity.ItemID)))
                    tariff.Load(GetItemTariffQuery(grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, entity.ItemID));

                entity.CitoAmount = (!tariff.IsCitoInPercent ?? false)
                                        ? (chargeQuantity * tariff.CitoValue)
                                        : (chargeQuantity * ((tariff.CitoValue / 100) * entity.Price));
                entity.IsCitoInPercent = tariff.IsCitoInPercent ?? false;
                entity.BasicCitoAmount = tariff.CitoValue;
            }

            entity.RoundingAmount = Helper.RoundingDiff;
            entity.SRDiscountReason = srDiscountReason;
            entity.IsAssetUtilization = isAssetUtilization;
            entity.AssetID = assetID;
            entity.IsBillProceed = false;
            entity.IsOrderRealization = false;
            entity.IsPackage = isPackage;
            entity.IsVoid = isVoid;
            entity.Notes = notes;
            entity.IsItemTypeService = false;
            entity.SRCenterID = centerID;
            entity.IsApprove = false;
            entity.IsItemRoom = isItemRoom;

            string p = string.Empty;

            //Item Tariff Component
            if (tariffComponents != null)
            {
                if (tariffComponents.Any())
                {
                    foreach (var comp in tariffComponents)
                    {
                        TransChargesItemComp item = FindTransChargesItemComp(comp.SequenceNo, comp.TariffComponentID);

                        if (item == null)
                        {
                            item = TransChargesItemComps.AddNew();
                            item.TransactionNo = txtTransactionNo.Text;
                            item.SequenceNo = comp.SequenceNo;
                        }

                        item.TariffComponentID = comp.TariffComponentID;
                        item.Price = comp.Price ?? 0;
                        item.DiscountAmount = comp.DiscountAmount;
                        if (!(entity.IsCito ?? false))
                            item.CitoAmount = 0;
                        else
                            item.CitoAmount = (!entity.IsCitoInPercent ?? false)
                                                  ? entity.BasicCitoAmount
                                                  : ((entity.BasicCitoAmount / 100) * comp.Price);
                        item.ParamedicID = comp.ParamedicID;
                        item.IsPackage = false;

                        if (!string.IsNullOrEmpty(item.ParamedicID))
                        {
                            var par = new Paramedic();
                            par.LoadByPrimaryKey(item.ParamedicID);
                            if (p.Length == 0)
                                p = par.ParamedicName;
                            else if (!p.Contains(par.ParamedicName))
                                p = p + "; " + par.ParamedicName;
                        }
                    }
                }
                else
                {
                    var comps = Helper.Tariff.GetItemTariffComponentCollection(txtTransactionDate.SelectedDate.Value, grr.SRTariffType,
                                        entity.ChargeClassID, itemID);
                    if (!comps.Any())
                        comps = Helper.Tariff.GetItemTariffComponentCollection(txtTransactionDate.SelectedDate.Value,
                            grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, itemID);
                    if (!comps.Any())
                        comps = Helper.Tariff.GetItemTariffComponentCollection(txtTransactionDate.SelectedDate.Value,
                            AppSession.Parameter.DefaultTariffType, entity.ChargeClassID, itemID);
                    if (!comps.Any())
                        comps = Helper.Tariff.GetItemTariffComponentCollection(txtTransactionDate.SelectedDate.Value,
                            AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, itemID);

                    foreach (var comp in comps)
                    {
                        var tcomp = TransChargesItemComps.AddNew();
                        tcomp.TransactionNo = entity.TransactionNo;
                        tcomp.SequenceNo = entity.SequenceNo;
                        tcomp.TariffComponentID = comp.TariffComponentID;

                        var tc = new TariffComponent();
                        tc.LoadByPrimaryKey(tcomp.TariffComponentID);
                        tcomp.TariffComponentName = tc.TariffComponentName;
                        tcomp.Price = comp.Price ?? 0;
                        tcomp.DiscountAmount = 0;
                        tcomp.CitoAmount = 0;
                        tcomp.ParamedicID = string.Empty;
                        tcomp.IsPackage = false;
                    }
                }
            }

            entity.ParamedicCollectionName = p;
            entity.DiscountAmount = discountAmount;
            decimal tot = (entity.ChargeQuantity.Value * entity.Price.Value) - entity.DiscountAmount.Value +
                          entity.CitoAmount.Value;
            entity.Total = Helper.Rounding(tot, AppEnum.RoundingType.Transaction);

            if (isNewRecord)
            {
                TransChargesItemConsumption consItem;

                if (entity.IsPackage ?? false)
                {
                    var pacs = new ItemPackageCollection();
                    pacs.Query.Where(
                        pacs.Query.ItemID == entity.ItemID &&
                        pacs.Query.IsExtraItem == false &&
                        pacs.Query.IsActive == true
                        );
                    pacs.LoadAll();

                    bool isFromItemPackComp = false;
                    var collItemPackageComp = new ItemPackageTariffComponentCollection();
                    collItemPackageComp.Query.Where(collItemPackageComp.Query.ItemID == entity.ItemID);
                    collItemPackageComp.LoadAll();
                    if (collItemPackageComp.Count > 0)
                        isFromItemPackComp = true;

                    foreach (var pac in pacs)
                    {
                        var ent = TransChargesItems.AddNew();
                        ent.TransactionNo = entity.TransactionNo;

                        var seq = (TransChargesItems.Where(c => c.ParentNo == sequenceNo)
                                                    .OrderByDescending(c => c.SequenceNo)
                                                    .Select(c => c.SequenceNo.Substring(3, 3))).Take(1).SingleOrDefault();

                        ent.SequenceNo = sequenceNo + string.Format("{0:000}", int.Parse((seq ?? "0")) + 1);
                        ent.ParentNo = entity.SequenceNo;
                        ent.ReferenceNo = string.Empty;
                        ent.ReferenceSequenceNo = string.Empty;
                        ent.ItemID = pac.DetailItemID;

                        var i = new Item();
                        i.LoadByPrimaryKey(ent.ItemID);
                        ent.ItemName = i.ItemName;
                        ent.ChargeClassID = entity.ChargeClassID;
                        ent.ParamedicID = string.Empty;
                        ent.IsAdminCalculation = false;
                        ent.IsVariable = false;
                        ent.IsCito = false;
                        ent.ChargeQuantity = chargeQuantity * pac.Quantity;

                        switch (i.SRItemType)
                        {
                            case ItemType.Medical:
                                ent.StockQuantity = ent.ChargeQuantity;

                                var ipm = new ItemProductMedic();
                                ipm.LoadByPrimaryKey(itemID);

                                ent.CostPrice = ipm.CostPrice ?? 0;
                                break;
                            case ItemType.NonMedical:
                                ent.StockQuantity = ent.ChargeQuantity;

                                var ipn = new ItemProductNonMedic();
                                ipn.LoadByPrimaryKey(itemID);

                                ent.CostPrice = ipn.CostPrice ?? 0;
                                break;
                            case ItemType.Kitchen:
                                ent.StockQuantity = ent.ChargeQuantity;

                                var ik = new ItemKitchen();
                                ik.LoadByPrimaryKey(itemID);

                                ent.CostPrice = ik.CostPrice ?? 0;
                                break;
                            default:
                                ent.StockQuantity = 0;
                                ent.CostPrice = 0;
                                break;
                        }

                        ent.SRItemUnit = pac.SRItemUnit;
                        ent.DiscountAmount = 0;
                        ent.CitoAmount = 0;
                        ent.RoundingAmount = 0;
                        ent.SRDiscountReason = string.Empty;
                        ent.IsAssetUtilization = false;
                        ent.AssetID = string.Empty;
                        ent.IsBillProceed = false;
                        ent.IsOrderRealization = false;
                        ent.IsPackage = false;
                        ent.IsVoid = false;
                        ent.Notes = string.Empty;
                        ent.IsItemTypeService = i.SRItemType != ItemType.Medical && i.SRItemType != ItemType.NonMedical;
                        ent.ToServiceUnitID = pac.ServiceUnitID;

                        var unit = new ServiceUnit();
                        if (unit.LoadByPrimaryKey(ent.ToServiceUnitID))
                            ent.ServiceUnitName = unit.ServiceUnitName;

                        ent.IsCitoInPercent = false;
                        ent.BasicCitoAmount = 0;
                        ent.IsItemRoom = false;

                        decimal pricePackage = 0;

                        switch (i.SRItemType)
                        {
                            case ItemType.Diagnostic:
                            case ItemType.Laboratory:
                            case ItemType.Package:
                            case ItemType.Radiology:
                            case ItemType.Service:


                                //var comps = Helper.Tariff.GetItemTariffComponentCollection(txtTransactionDate.SelectedDate.Value, grr.SRTariffType,
                                //    entity.ChargeClassID, ent.ItemID);
                                //if (!comps.Any())
                                //    comps = Helper.Tariff.GetItemTariffComponentCollection(txtTransactionDate.SelectedDate.Value,
                                //        AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, ent.ItemID);

                                //foreach (var comp in comps)
                                //{
                                //    var tcomp = TransChargesItemComps.AddNew();
                                //    tcomp.TransactionNo = entity.TransactionNo;
                                //    tcomp.SequenceNo = ent.SequenceNo;
                                //    tcomp.TariffComponentID = comp.TariffComponentID;

                                //    var tc = new TariffComponent();
                                //    tc.LoadByPrimaryKey(tcomp.TariffComponentID);
                                //    tcomp.TariffComponentName = tc.TariffComponentName;

                                //    var tcp = new ItemPackageTariffComponent();
                                //    if (tcp.LoadByPrimaryKey(entity.ItemID, ent.ItemID, tcomp.TariffComponentID))
                                //        tcomp.Price = tcp.Price ?? 0;
                                //    else
                                //        tcomp.Price = comp.Price ?? 0;

                                //    tcomp.DiscountAmount = 0;
                                //    tcomp.ParamedicID = string.Empty;
                                //    tcomp.IsPackage = true;

                                //    pricePackage += tcomp.Price ?? 0;
                                //}

                                //kl ItemPackageTariffComponent ada isinya maka semua ambil dari situ karena komponen yg ada di itemtariff tidak selalu sama
                                // dengan yg sudah disetting di ItemPackageTariffComponent sehingga cara lama gak bisa dipakai karena detail di transchargeitemcomp jadi salah
                                if (isFromItemPackComp == true)
                                {
                                    var tariffCompPack = new ItemPackageTariffComponentCollection();
                                    tariffCompPack.Query.Where(tariffCompPack.Query.ItemID == pac.ItemID, tariffCompPack.Query.DetailItemID == pac.DetailItemID);
                                    tariffCompPack.LoadAll();

                                    if (tariffCompPack.Count > 0)
                                    {
                                        foreach (var comp in tariffCompPack)
                                        {
                                            var tcomp = TransChargesItemComps.AddNew();
                                            tcomp.TransactionNo = entity.TransactionNo;
                                            tcomp.SequenceNo = ent.SequenceNo;
                                            tcomp.TariffComponentID = comp.TariffComponentID;

                                            var tc = new TariffComponent();
                                            tc.LoadByPrimaryKey(tcomp.TariffComponentID);
                                            tcomp.TariffComponentName = tc.TariffComponentName;
                                            tcomp.Price = comp.Price ?? 0;
                                            tcomp.DiscountAmount = 0;
                                            tcomp.CitoAmount = 0;
                                            tcomp.ParamedicID = string.Empty;
                                            tcomp.IsPackage = true;

                                            pricePackage += tcomp.Price ?? 0;
                                        }
                                    }
                                }
                                else
                                {
                                    var comps = Helper.Tariff.GetItemTariffComponentCollection(txtTransactionDate.SelectedDate.Value, grr.SRTariffType,
                                        entity.ChargeClassID, ent.ItemID);
                                    if (!comps.Any())
                                        comps = Helper.Tariff.GetItemTariffComponentCollection(txtTransactionDate.SelectedDate.Value,
                                            AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, ent.ItemID);

                                    foreach (var comp in comps)
                                    {
                                        var tcomp = TransChargesItemComps.AddNew();
                                        tcomp.TransactionNo = entity.TransactionNo;
                                        tcomp.SequenceNo = ent.SequenceNo;
                                        tcomp.TariffComponentID = comp.TariffComponentID;

                                        var tc = new TariffComponent();
                                        tc.LoadByPrimaryKey(tcomp.TariffComponentID);
                                        tcomp.TariffComponentName = tc.TariffComponentName;
                                        tcomp.Price = comp.Price ?? 0;
                                        tcomp.DiscountAmount = 0;
                                        tcomp.CitoAmount = 0;
                                        tcomp.ParamedicID = string.Empty;
                                        tcomp.IsPackage = true;

                                        pricePackage += tcomp.Price ?? 0;
                                    }
                                }

                                // consumption
                                var cons = new ItemConsumptionCollection();
                                cons.Query.Where(cons.Query.ItemID == pac.DetailItemID);
                                cons.LoadAll();

                                foreach (var consEntity in cons)
                                {
                                    consItem = TransChargesItemConsumptions.AddNew();
                                    consItem.TransactionNo = entity.TransactionNo;
                                    consItem.SequenceNo = ent.SequenceNo;
                                    consItem.DetailItemID = consEntity.DetailItemID;

                                    i = new Item();
                                    i.LoadByPrimaryKey(consItem.DetailItemID);
                                    consItem.ItemName = i.ItemName;

                                    consItem.Qty = chargeQuantity * consEntity.Qty;
                                    consItem.QtyRealization = consItem.Qty;
                                    consItem.SRItemUnit = consEntity.SRItemUnit;

                                    var tariff = new ItemTariff();
                                    if (!tariff.Load(GetItemTariffQuery(grr.SRTariffType, entity.ChargeClassID, consItem.DetailItemID)))
                                        tariff.Load(GetItemTariffQuery(AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, consItem.DetailItemID));
                                    consItem.Price = tariff.Price ?? 0;

                                    consItem.IsPackage = true;
                                }
                                break;
                            default:
                                if (pac.IsStockControl ?? false)
                                {
                                    consItem = TransChargesItemConsumptions.AddNew();
                                    consItem.TransactionNo = entity.TransactionNo;
                                    consItem.SequenceNo = ent.SequenceNo;
                                    consItem.DetailItemID = pac.DetailItemID;

                                    i = new Item();
                                    i.LoadByPrimaryKey(consItem.DetailItemID);
                                    consItem.ItemName = i.ItemName;

                                    consItem.Qty = chargeQuantity * pac.Quantity;
                                    consItem.QtyRealization = consItem.Qty;
                                    consItem.SRItemUnit = pac.SRItemUnit;

                                    var tariff = new ItemTariff();
                                    if (!tariff.Load(GetItemTariffQuery(grr.SRTariffType, entity.ChargeClassID, consItem.DetailItemID)))
                                        tariff.Load(GetItemTariffQuery(AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, consItem.DetailItemID));
                                    consItem.Price = tariff.Price ?? 0;

                                    consItem.IsPackage = true;
                                }
                                break;
                        }

                        ent.Price = pricePackage;
                        ent.IsExtraItem = false;
                        ent.IsSelectedExtraItem = false;
                    }
                }
                else
                {
                    var consColl = new ItemConsumptionCollection();
                    consColl.Query.Where(consColl.Query.ItemID == itemID);
                    consColl.LoadAll();

                    foreach (var consEntity in consColl)
                    {
                        consItem = TransChargesItemConsumptions.AddNew();
                        consItem.TransactionNo = entity.TransactionNo;
                        consItem.SequenceNo = sequenceNo;
                        consItem.DetailItemID = consEntity.DetailItemID;
                        consItem.Qty = chargeQuantity * consEntity.Qty;
                        consItem.QtyRealization = consItem.Qty;
                        consItem.SRItemUnit = consEntity.SRItemUnit;

                        var tariff = new ItemTariff();
                        if (!tariff.Load(GetItemTariffQuery(grr.SRTariffType, entity.ChargeClassID, consItem.DetailItemID)))
                            tariff.Load(GetItemTariffQuery(grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, consItem.DetailItemID));
                        consItem.Price = tariff.Price ?? 0;

                        consItem.IsPackage = false;
                    }
                }
            }
        }

        #endregion

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

                var query = new TransChargesItemCompQuery("a");
                var comp = new TariffComponentQuery("b");
                var tci = new TransChargesItemQuery("d");

                tci.Select(tci.TransactionNo, tci.SequenceNo);
                tci.Where(tci.TransactionNo == query.TransactionNo, tci.SequenceNo == query.SequenceNo,
                          tci.IsExtraItem == true,
                          tci.IsSelectedExtraItem == false);

                query.Select(
                    query,
                    comp.TariffComponentName.As("refToTariffComponent_TariffComponentName"),
                    comp.IsTariffParamedic
                    );
                query.InnerJoin(comp).On(query.TariffComponentID == comp.TariffComponentID);

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

                var query = new TransChargesItemConsumptionQuery("a");
                var item = new ItemQuery("b");
                var tci = new TransChargesItemQuery("d");

                tci.Select(tci.TransactionNo, tci.SequenceNo);
                tci.Where(tci.TransactionNo == query.TransactionNo, tci.SequenceNo == query.SequenceNo,
                          tci.IsExtraItem == true,
                          tci.IsSelectedExtraItem == false);

                query.Select(
                    query,
                    item.ItemName.As("refToItem_ItemName")
                    );
                query.InnerJoin(item).On(query.DetailItemID == item.ItemID);

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

                coll.Query.Where
                    (
                        coll.Query.TransactionNo == txtTransactionNo.Text,
                        coll.Query.RegistrationNo == txtRegistrationNo.Text
                    );
                coll.LoadAll();

                ViewState["collCostCalculation" + Request.UserHostName] = coll;
                return coll;
            }
            set { ViewState["collCostCalculation" + Request.UserHostName] = value; }
        }

        protected void grdTransChargesItem_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                if (TransChargesItems.Count < e.Item.DataSetIndex)
                {
                    var item = TransChargesItems[e.Item.DataSetIndex];
                    if (item != null)
                    {
                        if (item.IsVoid ?? false)
                        {
                            for (var i = 0; i < e.Item.Cells.Count; i++)
                            {
                                if (i > 0 && i < e.Item.Cells.Count)
                                    e.Item.Cells[i].Font.Strikeout = true;

                            }
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

        private TransChargesItemComp FindTransChargesItemComp(String sequenceNo, String tariffComponentID)
        {
            var coll = TransChargesItemComps;
            return coll.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo) && rec.TariffComponentID.Equals(tariffComponentID));
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid))
                return;

            var grd = (RadGrid)source;
            if (grd.ID == "grdTransChargesItem")
            {
                if (pnlResponUnit.Visible)
                    cboResponUnit.Enabled = (TransChargesItems.Count == 0);
                grd.Rebind();
            }
        }

        protected virtual void txtBarcodeEntry_OnTextChanged(object sender, System.EventArgs e)
        {
            if (txtBarcodeEntry.Text == string.Empty)
                return;

            if (QuickUpdateDetail(txtBarcodeEntry.Text))
            {
                grdTransChargesItem.Rebind();
                cboBedID.Enabled = false;
            }

            txtBarcodeEntry.Text = string.Empty;
            txtBarcodeEntry.Focus();
        }

        private bool QuickUpdateDetail(string itemID)
        {
            //Check hanya untuk type item 11 Medical & 21 Non Medical
            var item = new Item();
            if (!item.LoadByPrimaryKey(itemID))
                return false;

            if (item.SRItemType != ItemType.Medical && item.SRItemType != ItemType.NonMedical)
                return false;

            bool isItemInventory = false;
            if (item.SRItemType == ItemType.Medical)
            {
                var ipm = new ItemProductMedic();
                ipm.LoadByPrimaryKey(itemID);
                isItemInventory = ipm.IsInventoryItem ?? false;
            }
            else if (item.SRItemType == ItemType.NonMedical)
            {
                var ipnm = new ItemProductNonMedic();
                ipnm.LoadByPrimaryKey(itemID);
                isItemInventory = ipnm.IsInventoryItem ?? false;
            }
            else if (item.SRItemType == ItemType.Kitchen)
            {
                var ik = new ItemKitchen();
                ik.LoadByPrimaryKey(itemID);
                isItemInventory = ik.IsInventoryItem ?? false;
            }

            var unit = new ServiceUnit();
            if (!unit.LoadByPrimaryKey(pnlResponUnit.Visible ? cboResponUnit.SelectedValue : cboFromServiceUnitID.SelectedValue))
                    return false;

            var bal = new ItemBalance();
            if (!bal.LoadByPrimaryKey(unit.GetMainLocationId(), itemID))
                return false;

            if (bal.Balance < 0 && isItemInventory)
                return false;

            string sequenceNo, itemName, paramedicID, paramedicName, srItemUnit = string.Empty, srDiscountReason, assetID, notes;
            bool? isAdminCalculation, isVariable, isCito, isAssetUtilization, isPackage, isVoid, isItemRoom;
            decimal? chargeQuantity, stockQuantity, costPrice = 0, price, discountAmount;
            bool isNewRecord;

            //Check bila sudah ada maka tambah di qty nya saja
            var entity = TransChargesItems.FirstOrDefault(rec => rec.ItemID.Equals(itemID));

            if (entity != null)
            {
                sequenceNo = entity.SequenceNo;
                itemName = entity.ItemName;
                paramedicID = entity.ParamedicID;
                paramedicName = entity.ParamedicName;
                isAdminCalculation = entity.IsAdminCalculation;
                isVariable = entity.IsVariable;
                isCito = entity.IsCito;
                chargeQuantity = entity.ChargeQuantity + 1;
                stockQuantity = entity.StockQuantity + 1;
                srItemUnit = entity.SRItemUnit;
                costPrice = entity.CostPrice;
                price = entity.Price;
                discountAmount = entity.DiscountAmount;
                srDiscountReason = entity.SRDiscountReason;
                isAssetUtilization = entity.IsAssetUtilization;
                assetID = entity.AssetID;
                isPackage = entity.IsPackage;
                isVoid = entity.IsVoid;
                notes = entity.Notes;
                isNewRecord = false;
                isItemRoom = entity.IsItemRoom;
            }
            else
            {
                sequenceNo = TransChargesItems.Count > 0 ? string.Format("{0:000}", int.Parse(TransChargesItems[TransChargesItems.Count - 1].SequenceNo) + 1) : "001";

                itemName = item.ItemName;
                paramedicID = string.Empty;
                paramedicName = string.Empty;

                if (item.SRItemType == ItemType.Medical)
                {
                    var itemMedic = new ItemProductMedic();
                    itemMedic.LoadByPrimaryKey(itemID);
                    srItemUnit = itemMedic.SRItemUnit;
                    costPrice = itemMedic.CostPrice;
                }
                else if (item.SRItemType == ItemType.NonMedical)
                {
                    var itemNonMedic = new ItemProductNonMedic();
                    itemNonMedic.LoadByPrimaryKey(itemID);
                    srItemUnit = itemNonMedic.SRItemUnit;
                    costPrice = itemNonMedic.CostPrice;
                }
                else if (item.SRItemType == ItemType.Kitchen)
                {
                    var itemKitchen = new ItemKitchen();
                    itemKitchen.LoadByPrimaryKey(itemID);
                    srItemUnit = itemKitchen.SRItemUnit;
                    costPrice = itemKitchen.CostPrice;
                }

                stockQuantity = 1;
                chargeQuantity = 1;

                var reg = new Registration();
                reg.LoadByPrimaryKey(((RadTextBox)Helper.FindControlRecursive(Page, "txtRegistrationNo")).Text);

                var grr = new Guarantor();
                grr.LoadByPrimaryKey(reg.GuarantorID);

                ItemTariff tariff = (Helper.Tariff.GetItemTariff(txtTransactionDate.SelectedDate.Value, grr.SRTariffType, reg.ChargeClassID, reg.ChargeClassID, itemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                     Helper.Tariff.GetItemTariff(txtTransactionDate.SelectedDate.Value, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, itemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                    (Helper.Tariff.GetItemTariff(txtTransactionDate.SelectedDate.Value, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, reg.ChargeClassID, itemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                    Helper.Tariff.GetItemTariff(txtTransactionDate.SelectedDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, itemID, reg.GuarantorID, false, reg.SRRegistrationType));

                price = tariff.Price ?? 0;
                isVariable = tariff.IsAllowVariable ?? false;
                isCito = tariff.IsAllowCito ?? false;
                isAdminCalculation = tariff.IsAdminCalculation ?? false;
                discountAmount = 0;
                srDiscountReason = string.Empty;
                isAssetUtilization = false;
                assetID = string.Empty;
                isPackage = false;
                isVoid = false;
                notes = string.Empty;
                isNewRecord = true;

                var itemRooms = new AppStandardReferenceItemCollection();
                itemRooms.Query.Where(itemRooms.Query.StandardReferenceID == "ItemTariffRoom",
                                      itemRooms.Query.ItemID == itemID, itemRooms.Query.IsActive == true);
                itemRooms.LoadAll();
                isItemRoom = itemRooms.Count > 0;

                entity = TransChargesItems.AddNew();
            }
            SetEntityDetail(entity, sequenceNo, itemID, itemName, paramedicID, paramedicName, isAdminCalculation, isVariable, isCito, chargeQuantity,
                  stockQuantity, srItemUnit, costPrice, price, discountAmount, srDiscountReason, isAssetUtilization, assetID, isPackage, isVoid,
                  notes, string.Empty, null, isNewRecord, isItemRoom, true);
            return true;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"] && !(bool)ViewState["IsVoid"];
        }

        private static ItemTariffQuery GetItemTariffQuery(string tariffType, string classID, string itemID)
        {
            var query = new ItemTariffQuery();
            query.es.Top = 1;
            query.Where
                (
                    query.SRTariffType == tariffType,
                    query.ClassID == classID,
                    query.ItemID == itemID,
                    query.StartingDate <= DateTime.Now
                );
            query.OrderBy(query.StartingDate.Descending);

            return query;
        }

        protected void cboToServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grdTransChargesItem.DataSource = TransChargesItems;
            grdTransChargesItem.MasterTableView.IsItemInserted = false;
            grdTransChargesItem.MasterTableView.ClearEditItems();
            grdTransChargesItem.DataBind();
        }

        protected void cboResponUnit_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bool isVisible = (DataModeCurrent != AppEnum.DataMode.Read);
            grdTransChargesItem.MasterTableView.CommandItemDisplay = (isVisible && !string.IsNullOrEmpty(cboResponUnit.SelectedValue))
                                                                         ? GridCommandItemDisplay.Top
                                                                         : GridCommandItemDisplay.None;
            grdTransChargesItem.Rebind();
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery("a");
            var unit = new ServiceUnitParamedicQuery("b");

            query.es.Top = 10;
            query.Select(
                query.ParamedicID,
                query.ParamedicName
                );
            query.InnerJoin(unit).On(query.ParamedicID == unit.ParamedicID);
            query.Where
                (
                    query.ParamedicName.Like(searchTextContain),
                    query.IsActive == true,
                    query.IsAvailable == true,
                    unit.ServiceUnitID == cboFromServiceUnitID.SelectedValue
                );
            query.OrderBy(query.ParamedicName.Ascending);

            cboParamedicID.DataSource = query.LoadDataTable();
            cboParamedicID.DataBind();
        }

        protected void cboBedID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboFromServiceUnitID.Items.Clear();
            var uq = new ServiceUnitQuery();
            uq.Where(uq.ServiceUnitID == e.Value.Split(',')[0].Trim());
            cboFromServiceUnitID.DataSource = uq.LoadDataTable();
            cboFromServiceUnitID.DataBind();

            cboFromServiceUnitID.SelectedValue = e.Value.Split(',')[0].Trim();
            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue);

            txtRoomID.Text = e.Value.Split(',')[1].Trim();
            var room = new ServiceRoom();
            room.LoadByPrimaryKey(txtRoomID.Text);
            lblRoomName.Text = room.RoomName;
            txtTariffDiscForRoomIn.Value = Convert.ToDouble(room.TariffDiscountForRoomIn);

            if (room.IsOperatingRoom == true)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                txtClassID.Text = string.IsNullOrEmpty(reg.ProcedureChargeClassID)
                                          ? e.Value.Split(',')[2].Trim()
                                          : reg.ProcedureChargeClassID;
            }
            else
                txtClassID.Text = e.Value.Split(',')[2].Trim();

            var c = new Class();
            c.LoadByPrimaryKey(txtClassID.Text);
            lblClassName.Text = c.ClassName;

            var birColl = new BedRoomInCollection();
            birColl.Query.Where(birColl.Query.BedID == e.Value.Split(',')[3].Trim(),
                                birColl.Query.RegistrationNo == txtRegistrationNo.Text, birColl.Query.IsVoid == false);
            birColl.LoadAll();
            chkIsRoomIn.Checked = birColl.Count > 0;
        }

        private void PopulateBedCollection(Registration reg)
        {
            cboBedID.Items.Clear();
            var refers = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text).Where(m => !m.Equals(txtRegistrationNo.Text));
            if (refers.Any())
            {
                var regs = new RegistrationCollection();
                regs.Query.Where(regs.Query.RegistrationNo.In(refers));
                regs.LoadAll();

                foreach (var entity in regs)
                {
                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(entity.ServiceUnitID);

                    var room = new ServiceRoom();
                    room.LoadByPrimaryKey(entity.RoomID);

                    var c = new Class();
                    c.LoadByPrimaryKey(entity.ChargeClassID);

                    cboBedID.Items.Add(
                        new RadComboBoxItem(
                            unit.ServiceUnitName + ", " + room.RoomName + ", " + c.ClassName + ", " + entity.BedID,
                            entity.ServiceUnitID + ", " + entity.RoomID + ", " + entity.ChargeClassID + ", " + entity.BedID)
                            );
                }
            }

            var transfers = new PatientTransferCollection();
            transfers.Query.Where(
                transfers.Query.RegistrationNo == txtRegistrationNo.Text,
                transfers.Query.IsApprove == true
                );
            transfers.LoadAll();

            if (transfers.HasData)
            {
                var array = new string[transfers.Count * 2];
                var i = 0;
                foreach (var transfer in transfers)
                {
                    array.SetValue(transfer.FromServiceUnitID + ", " + transfer.FromRoomID + ", " + transfer.FromChargeClassID + ", " + transfer.FromBedID, i);
                    i++;

                    array.SetValue(transfer.ToServiceUnitID + ", " + transfer.ToRoomID + ", " + transfer.ToChargeClassID + ", " + transfer.ToBedID, i);
                    i++;
                }

                foreach (var str in array.Distinct())
                {
                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(str.Split(',')[0].Trim());

                    var room = new ServiceRoom();
                    room.LoadByPrimaryKey(str.Split(',')[1].Trim());

                    var c = new Class();
                    c.LoadByPrimaryKey(str.Split(',')[2].Trim());

                    cboBedID.Items.Add(new RadComboBoxItem(unit.ServiceUnitName + ", " + room.RoomName + ", " + c.ClassName + ", " + str.Split(',')[3].Trim(), str));
                }
            }
            else
            {
                if (string.IsNullOrEmpty(reg.BedID))
                    return;

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(reg.ServiceUnitID);

                var room = new ServiceRoom();
                room.LoadByPrimaryKey(reg.RoomID);

                var c = new Class();
                c.LoadByPrimaryKey(reg.ChargeClassID);

                cboBedID.Items.Add(new RadComboBoxItem(unit.ServiceUnitName + ", " + room.RoomName + ", " + c.ClassName + ", " + reg.BedID,
                    reg.ServiceUnitID + ", " + reg.RoomID + ", " + reg.ChargeClassID + ", " + reg.BedID));
            }

            var bookings = new ServiceUnitBookingCollection();
            bookings.Query.Where(
                bookings.Query.RegistrationNo == txtRegistrationNo.Text,
                bookings.Query.IsApproved == true
                );
            bookings.LoadAll();
            if (bookings.HasData)
            {
                foreach (var entity in bookings)
                {
                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(entity.ServiceUnitID);

                    var room = new ServiceRoom();
                    room.LoadByPrimaryKey(entity.RoomID);

                    var c = new Class();
                    c.LoadByPrimaryKey(reg.ChargeClassID);

                    cboBedID.Items.Add(
                        new RadComboBoxItem(
                            unit.ServiceUnitName + ", " + room.RoomName + ", " + c.ClassName + ", " + reg.BedID,
                            entity.ServiceUnitID + ", " + entity.RoomID + ", " + reg.ChargeClassID + ", " + reg.BedID)
                            );
                }
            }
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void cboTypeResult_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }

        protected void cboTypeResult_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested((RadComboBox)o, "TypeResult", e.Text);
        }

        protected void cboSurgeryPackageID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PackageName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PackageID"].ToString();
        }

        protected void cboSurgeryPackageID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SurgicalPackageQuery();
            query.es.Top = 20;
            query.Select(query.PackageID, query.PackageName);
            query.Where
                (
                    query.PackageName.Like(searchTextContain),
                    query.IsActive == true
                );
            query.OrderBy(query.PackageID.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboSurgeryPackageID.DataSource = dtb;
            cboSurgeryPackageID.DataBind();
        }

        protected void cboFromServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboServiceUnitBookingNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitBookingQuery("a");
            var par = new ParamedicQuery("b");
            query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID);
            query.es.Top = 20;
            query.Select(query.BookingNo, query.BookingDateTimeFrom, par.ParamedicName);
            query.Where(
                query.Or(par.ParamedicName.Like(searchTextContain),
                         query.BookingNo.Like(searchTextContain)),
                query.IsApproved == true,
                query.RegistrationNo == txtRegistrationNo.Text);
            query.OrderBy(query.BookingNo.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboServiceUnitBookingNo.DataSource = dtb;
            cboServiceUnitBookingNo.DataBind();
        }

        protected void cboServiceUnitBookingNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["BookingNo"] + " [" +
                          (string.Format("{0:dd-MMM-yyyy}",
                                         Convert.ToDateTime(((DataRowView)e.Item.DataItem)["BookingDateTimeFrom"]))) +
                          "] " + ((DataRowView)e.Item.DataItem)["ParamedicName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["BookingNo"].ToString();
        }

        protected void cboSRKiaCaseType_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery();
            query.es.Top = 20;
            query.Select(query.ItemID, query.ItemName);
            query.Where(
                query.Or(query.ItemID.Like(searchTextContain),
                         query.ItemName.Like(searchTextContain)),
                query.IsActive == true, query.StandardReferenceID == AppEnum.StandardReference.KiaCaseType);
            query.OrderBy(query.ItemID.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboSRKiaCaseType.DataSource = dtb;
            cboSRKiaCaseType.DataBind();
        }

        protected void cboSRKiaCaseType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRObstetricType_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery();
            query.es.Top = 20;
            query.Select(query.ItemID, query.ItemName);
            query.Where(
                query.Or(query.ItemID.Like(searchTextContain),
                         query.ItemName.Like(searchTextContain)),
                query.IsActive == true, query.StandardReferenceID == AppEnum.StandardReference.ObstetricType);
            query.OrderBy(query.ItemID.Ascending);
            DataTable dtb = query.LoadDataTable();
            cboSRObstetricType.DataSource = dtb;
            cboSRObstetricType.DataBind();
        }

        protected void cboSRObstetricType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboPhysicianSendersID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }
    }
}
