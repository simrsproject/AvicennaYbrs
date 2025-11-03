using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using System.Data;

namespace Temiang.Avicenna.Module.Charges.Dispensary
{
    public partial class PrescriptionReturnDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;
        
        private bool IsFromVerif
        {
            get
            {
                return (Request.QueryString["ver"] == "1");
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PrescriptionReturnSearch.aspx";
            if (IsFromVerif)
                UrlPageList = "../PrescriptionVerification/PrescriptionVerificationList.aspx";
            else
                UrlPageList = "PrescriptionReturnList.aspx";

            ProgramID = AppConstant.Program.PrescriptionReturn;

            if (!IsPostBack)
            {
                TransPrescriptionItems = null;

                //Registration
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regno"]);

                //Service Unit
                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, BusinessObject.Reference.TransactionCode.Prescription, true);
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"] && !(bool)ViewState["IsVoid"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private void PopulateRegistrationInformation(string registrationNo)
        {
            if (string.IsNullOrEmpty(registrationNo))
                return;

            var registration = new Registration();
            if (registration.LoadByPrimaryKey(registrationNo))
            {
                var patient = new Patient();
                patient.LoadByPrimaryKey(registration.PatientID);

                txtMedicalNo.Text = patient.MedicalNo;
                txtPatientName.Text = patient.PatientName;

                optSexFemale.Checked = patient.Sex.Equals("F");
                optSexMale.Checked = patient.Sex.Equals("M");
                if (patient.Sex.Equals("F"))
                    optSexMale.Enabled = false;
                else
                    optSexFemale.Enabled = false;

                txtAgeDay.Value = Helper.GetAgeInDay(patient.DateOfBirth.Value);
                txtAgeMonth.Value = Helper.GetAgeInMonth(patient.DateOfBirth.Value);
                txtAgeYear.Value = Helper.GetAgeInYear(patient.DateOfBirth.Value);

                txtServiceUnitID.Text = registration.ServiceUnitID;
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(txtServiceUnitID.Text);
                lblServiceUnitName.Text = unit.ServiceUnitName;

                txtRoomID.Text = registration.RoomID;
                var room = new ServiceRoom();
                room.LoadByPrimaryKey(txtRoomID.Text);
                lblRoomName.Text = room.RoomName;

                txtBedID.Text = registration.BedID;

                PopulatePatientImage(patient.PatientID);
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdTransPrescriptionItem, grdTransPrescriptionItem);

            ajax.AddAjaxSetting(cboServiceUnitID, cboServiceUnitID);
            ajax.AddAjaxSetting(cboServiceUnitID, txtPrescriptionNo);
            ajax.AddAjaxSetting(cboServiceUnitID, cboLocationID);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            switch (programID)
            {
                case AppConstant.Report.PrescriptionReturnReceiptSlip:
                    printJobParameters.AddNew("p_PrescriptionNo", txtPrescriptionNo.Text);
                    printJobParameters.AddNew("p_Label", "");
                    printJobParameters.AddNew("temp_TITLE", "RETUR RESEP FARMASI");
                    break;
                //Untuk RSSA
                case AppConstant.Report.RSSA_PrescriptionReturnReceiptSlip:
                    printJobParameters.AddNew("p_PrescriptionNo", txtPrescriptionNo.Text);
                    printJobParameters.AddNew("p_Label", "");
                    printJobParameters.AddNew("temp_TITLE", "RETUR RESEP FARMASI");
                    break;
            }
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new TransPrescription());

            if (cboServiceUnitID.Items.Count == 2)
                cboServiceUnitID.SelectedIndex = 1;

            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, cboServiceUnitID.SelectedValue);
                cboLocationID.SelectedIndex = 1;
            }
            else
            {
                cboLocationID.Items.Clear();
                cboLocationID.Text = string.Empty;
            }

            txtRegistrationNo.Text = Request.QueryString["regno"];
            txtPrescriptionDate.SelectedDate = DateTime.Today;
            txtPrescriptionNo.Text = GetNewPrescriptionNo();

            PopulateRegistrationInformation(txtRegistrationNo.Text);

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            TransPrescription entity = new TransPrescription();
            if (entity.LoadByPrimaryKey(txtPrescriptionNo.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (TransPrescriptionItems.Count == 0)
            {
                args.MessageText = "Detail transaction is not defined.";
                args.IsCancel = true;
                return;
            }

            TransPrescription entity = new TransPrescription();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (TransPrescriptionItems.Count == 0)
            {
                args.MessageText = "Detail transaction is not defined.";
                args.IsCancel = true;
                return;
            }

            var r = new Registration();
            r.LoadByPrimaryKey(txtRegistrationNo.Text);
            if (r.IsHoldTransactionEntry ?? false)
            {
                args.MessageText = "Registration locked.";
                args.IsCancel = true;
                return;
            }

            if (r.IsClosed ?? false)
            {
                args.MessageText = "Registration closed.";
                args.IsCancel = true;
                return;
            }

            TransPrescription entity = new TransPrescription();
            if (entity.LoadByPrimaryKey(txtPrescriptionNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
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
            auditLogFilter.PrimaryKeyData = string.Format("PrescriptionNo='{0}'", txtPrescriptionNo.Text.Trim());
            auditLogFilter.TableName = "TransPrescription";
        }

        #endregion

        #region ToolBar Menu Support

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
            RefreshCommandItemTransPrescriptionItem(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new TransPrescription();
            if (parameters.Length > 0)
            {
                var prescriptionNo = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(prescriptionNo);
            }
            else
                entity.LoadByPrimaryKey(txtPrescriptionNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var transPrescription = (TransPrescription)entity;

            txtPrescriptionNo.Text = transPrescription.PrescriptionNo;

            txtRegistrationNo.Text = transPrescription.RegistrationNo;
            cboServiceUnitID.SelectedValue = transPrescription.ServiceUnitID;
            if (!string.IsNullOrEmpty(transPrescription.ServiceUnitID))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, transPrescription.ServiceUnitID);
                if (!string.IsNullOrEmpty(transPrescription.LocationID))
                    cboLocationID.SelectedValue = transPrescription.LocationID;
                else cboLocationID.SelectedIndex = 1;
            }

            txtReferenceNo.Text = transPrescription.ReferenceNo;

            txtParamedicID.Text = transPrescription.ParamedicID;
            var medic = new Paramedic();
            medic.LoadByPrimaryKey(txtParamedicID.Text);
            lblParamedicName.Text = medic.ParamedicName;

            txtPrescriptionDate.SelectedDate = transPrescription.PrescriptionDate;

            PopulateRegistrationInformation(txtRegistrationNo.Text);

            ViewState["IsApproved"] = transPrescription.IsApproval ?? false;
            ViewState["IsVoid"] = transPrescription.IsVoid ?? false;

            //Display Data Detail
            PopulateTransPrescriptionItemGrid();
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new TransPrescription();
            if (!entity.LoadByPrimaryKey(txtPrescriptionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsApproval ?? false)
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

            var r = new Registration();
            r.LoadByPrimaryKey(entity.RegistrationNo);
            if (r.IsHoldTransactionEntry ?? false)
            {
                args.MessageText = "Registration locked.";
                args.IsCancel = true;
                return;
            }

            if (r.IsClosed ?? false)
            {
                args.MessageText = "Registration closed.";
                args.IsCancel = true;
                return;
            }

            var loc = new Location();
            if (loc.LoadByPrimaryKey(entity.LocationID) && loc.IsHoldForTransaction == true)
            {
                args.MessageText = "Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.";
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, true, args);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new TransPrescription();
            if (!entity.LoadByPrimaryKey(txtPrescriptionNo.Text))
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
            if (entity.IsApproval == false)
            {
                args.MessageText = AppConstant.Message.RecordHasNotApproved;
                args.IsCancel = true;
                return;
            }

            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH" && entity.ApprovalDateTime.Value.AddMinutes(1) > (new DateTime()).NowAtSqlServer())
            {
                args.MessageText = "This data can not be unapproval before passing 1 minute.";
                args.IsCancel = true;
                return;
            }

            if (entity.ApprovalDateTime.Value.Date != (new DateTime()).NowAtSqlServer().Date && !this.IsPowerUser)
            {
                args.MessageText = "Transaction is expired. Please contact your supervision.";
                args.IsCancel = true;
                return;
            }

            var r = new Registration();
            r.LoadByPrimaryKey(entity.RegistrationNo);
            if (r.IsHoldTransactionEntry ?? false)
            {
                args.MessageText = "Registration locked.";
                args.IsCancel = true;
                return;
            }

            if (r.IsClosed ?? false)
            {
                args.MessageText = "Registration closed.";
                args.IsCancel = true;
                return;
            }

            var pycoll = new TransPaymentItemOrderCollection();
            pycoll.Query.Where(
                pycoll.Query.TransactionNo == entity.PrescriptionNo,
                pycoll.Query.IsPaymentProceed == true,
                pycoll.Query.IsPaymentReturned == false
                );
            pycoll.LoadAll();

            if (pycoll.Count > 0)
            {
                args.MessageText = "Prescreption already paid. Un-Approval is not allowed.";
                args.IsCancel = true;
                return;
            }

            var ibcoll = new CostCalculationCollection();
            ibcoll.Query.Where(
                ibcoll.Query.TransactionNo == entity.PrescriptionNo,
                ibcoll.Query.IntermBillNo.IsNotNull()
                );
            ibcoll.LoadAll();

            if (ibcoll.Count > 0)
            {
                args.MessageText = "Prescreption already on interm bill. Un-Approval is not allowed.";
                args.IsCancel = true;
                return;
            }

            var loc = new Location();
            if (loc.LoadByPrimaryKey(entity.LocationID) && loc.IsHoldForTransaction == true)
            {
                args.MessageText = "Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.";
                args.IsCancel = true;
                return;
            }

            SetUnApproval(entity, args);
        }

        private void SetApproval(TransPrescription entity, bool isApproval, ValidateArgs args)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(entity.RegistrationNo);
            if (reg.IsHoldTransactionEntry ?? false)
            {
                args.MessageText = "Transaction is locked.";
                args.IsCancel = true;
                return;
            }

            var prescRef = new TransPrescription();
            prescRef.LoadByPrimaryKey(entity.ReferenceNo);

            using (var trans = new esTransactionScope())
            {
                //header
                entity.IsApproval = isApproval;

                if (isApproval)
                {
                    entity.ApprovalDateTime = (new DateTime()).NowAtSqlServer();
                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                }
                else
                {
                    entity.str.ApprovalDateTime = string.Empty;
                    entity.ApprovedByUserID = null;
                }

                entity.IsBillProceed = isApproval;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                if (isApproval)
                {
                    var itemId = TransPrescriptionItems.Select(t => new { ItemID = string.IsNullOrEmpty(t.ItemInterventionID) ? t.ItemID : t.ItemInterventionID });
                    var tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, reg.GuarantorID, itemId.Select(t => t.ItemID).ToArray(), prescRef.PrescriptionDate.Value.Date, true);

                    foreach (var detail in TransPrescriptionItems)
                    {
                        if (!detail.IsVoid ?? false)
                        {
                            //detail.RecipeAmount = 0;
                            //detail.Price = detail.Price - (((AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) / 100) * detail.Price);
                            //detail.IsUsingAdminReturn = (AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) > 0;
                            detail.IsApprove = isApproval;
                            detail.IsBillProceed = isApproval;
                            detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            detail.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                            var presc = new TransPrescriptionItem();
                            presc.LoadByPrimaryKey(entity.ReferenceNo, detail.SequenceNo);

                            var calc = new Helper.CostCalculation(reg.GuarantorID, reg.IsGlobalPlafond ?? false,
                                       string.IsNullOrEmpty(detail.ItemInterventionID) ? detail.ItemID : detail.ItemInterventionID, Math.Abs(detail.LineAmount ?? 0),
                                       tblCovered, Math.Abs(detail.ResultQty ?? 0), detail.Price ?? 0, detail.RecipeAmount ?? 0, detail.DiscountAmount ?? 0);

                            //cost calculations
                            var cost = CostCalculations.AddNew();
                            cost.RegistrationNo = entity.RegistrationNo;
                            cost.TransactionNo = detail.PrescriptionNo;
                            cost.SequenceNo = detail.SequenceNo;
                            cost.ItemID = string.IsNullOrEmpty(detail.ItemInterventionID) ? detail.ItemID : detail.ItemInterventionID;
                            cost.PatientAmount = 0 - calc.PatientAmount;
                            cost.GuarantorAmount = 0 - calc.GuarantorAmount;
                            cost.DiscountAmount = 0 - detail.DiscountAmount;
                            cost.ParamedicAmount = 0;
                            cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            cost.LastUpdateByUserID = AppSession.UserLogin.UserID;

                            //update status IsReturned
                            presc.IsReturned = true;
                            presc.Save();
                        }
                    }
                }
                else
                    CostCalculations.MarkAllAsDeleted();

                entity.Save();

                //stock calculation
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(entity.ServiceUnitID);

                // stock calculation
                var chargesBalances = new ItemBalanceCollection();
                var chargesDetailBalances = new ItemBalanceDetailCollection();
                var chargesDetailBalanceEds = new ItemBalanceDetailEdCollection();
                var chargesMovements = new ItemMovementCollection();

                ItemBalance.PrepareItemBalancesForReturn(txtPrescriptionNo.Text, TransPrescriptionItems, BusinessObject.Reference.TransactionCode.PrescriptionReturn, entity.ServiceUnitID,
                    entity.LocationID, AppSession.UserLogin.UserID, isApproval, ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, ref chargesDetailBalanceEds, 
                    AppSession.Parameter.IsEnabledStockWithEdControl);

                TransPrescriptionItems.Save();
                CostCalculations.Save();

                if (chargesBalances != null)
                    chargesBalances.Save();
                if (chargesDetailBalances != null)
                    chargesDetailBalances.Save();
                if (chargesDetailBalanceEds != null)
                    chargesDetailBalanceEds.Save();
                if (chargesMovements != null)
                    chargesMovements.Save();

                /* Automatic Journal Testing Start */
                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                {
                    if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                    {
                        JournalTransactions.AddNewPatientIncomeAccrual(BusinessObject.JournalType.Income, entity.PrescriptionNo, AppSession.UserLogin.UserID, 0);
                    }
                    else {
                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.PrescriptionDate.Value.Date);
                        if (isClosingPeriod)
                        {
                            args.MessageText = "Financial statements for period: " +
                                               string.Format("{0:MMMM-yyyy}", entity.PrescriptionDate.Value.Date) +
                                               " have been closed. Please contact the authorities.";
                            args.IsCancel = true;
                            return;
                        }

                        //if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsPemisahanCOAUangRacikan) == "1")
                        //{
                        //    int? journalId = JournalTransactions.AddNewPrescriptionReturnJournalWithSeparationPersonalizedRecipeMoney(entity, reg, unit, CostCalculations, "RS", AppSession.UserLogin.UserID, 0);
                        //}
                        //else
                        //{
                        //    int? journalId = JournalTransactions.AddNewPrescriptionReturnJournal(entity, reg, unit, CostCalculations, "RS", AppSession.UserLogin.UserID, 0);
                        //}

                        int? journalId = JournalTransactions.AddNewPrescriptionReturnJournalTemporaryNetto(entity, reg, unit,
                                        CostCalculations, "RS", AppSession.UserLogin.UserID, 0);
                    }
                }
                //else if (AppSession.Parameter.IsUsingIntermBill != "Yes")
                //{
                //    var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.PrescriptionDate.Value.Date);
                //    if (isClosingPeriod)
                //    {
                //        args.MessageText = "Financial statements for period: " +
                //                           string.Format("{0:MMMM-yyyy}", entity.PrescriptionDate.Value.Date) +
                //                           " have been closed. Please contact the authorities.";
                //        args.IsCancel = true;
                //        return;
                //    }

                //    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsPemisahanCOAUangRacikan) == "1")
                //    {
                //        int? journalId = JournalTransactions.AddNewPrescriptionReturnJournalWithSeparationPersonalizedRecipeMoney(entity, reg, unit, CostCalculations, "RS", AppSession.UserLogin.UserID, 0);
                //    }
                //    else
                //    {
                //        int? journalId = JournalTransactions.AddNewPrescriptionReturnJournal(entity, reg, unit, CostCalculations, "RS", AppSession.UserLogin.UserID, 0);
                //    }
                //}
                /* Automatic Journal Testing End */

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void SetUnApproval(TransPrescription header, ValidateArgs args)
        {
            //var reg = new Registration();
            //reg.LoadByPrimaryKey(header.RegistrationNo);
            //if (reg.IsHoldTransactionEntry ?? false)
            //{
            //    args.MessageText = "Transaction is locked.";
            //    args.IsCancel = true;
            //    return;
            //}

            //var grUsr = new AppUserUserGroupQuery("a");
            //var gr = new AppUserGroupQuery("b");
            //grUsr.InnerJoin(gr).On(grUsr.UserGroupID == gr.UserGroupID && grUsr.UserID == AppSession.UserLogin.UserID &&
            //                       gr.IsEditAble == true);
            //if (grUsr.LoadDataTable().Rows.Count == 0)
            //{
            //    if (header.PrescriptionDate.Value.Date != (new DateTime()).NowAtSqlServer().Date)
            //    {
            //        args.MessageText = "Transaction is expired.";
            //        args.IsCancel = true;
            //        return;
            //    }
            //}

            using (var trans = new esTransactionScope())
            {
                header.IsApproval = false;
                header.IsBillProceed = false;
                header.LastUpdateByUserID = AppSession.UserLogin.UserID;
                header.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                foreach (var detail in TransPrescriptionItems)
                {
                    detail.IsApprove = false;
                    detail.IsBillProceed = false;
                    detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    detail.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }

                CostCalculations.MarkAllAsDeleted();
                CostCalculations.Save();

                TransPrescriptionItems.Save();
                header.Save();

                // stock calculation
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(header.ServiceUnitID);

                var chargesBalances = new ItemBalanceCollection();
                var chargesDetailBalances = new ItemBalanceDetailCollection();
                var chargesDetailBalanceEds = new ItemBalanceDetailEdCollection();
                var chargesMovements = new ItemMovementCollection();

                ItemBalance.PrepareItemBalancesForReturn(TransPrescriptionItems, BusinessObject.Reference.TransactionCode.PrescriptionReturn, unit.ServiceUnitID,
                    header.LocationID, AppSession.UserLogin.UserID, ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, ref chargesDetailBalanceEds, 
                    AppSession.Parameter.IsEnabledStockWithEdControl);

                if (chargesBalances != null)
                    chargesBalances.Save();
                if (chargesDetailBalances != null)
                    chargesDetailBalances.Save();
                if (chargesDetailBalanceEds != null)
                    chargesDetailBalanceEds.Save();
                if (chargesMovements != null)
                    chargesMovements.Save();

                //hapus jurnal karena dianggap batal transaksi di hari yg sama
                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                {
                    var jhd = new JournalTransactions();
                    jhd.Query.Where(jhd.Query.RefferenceNumber == header.PrescriptionNo);
                    if (jhd.Query.Load())
                    {
                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(jhd.TransactionDate.Value.Date);
                        if (isClosingPeriod)
                        {
                            args.MessageText = "Financial statements for period: " +
                                                  string.Format("{0:MMMM-yyyy}", jhd.TransactionDate.Value.Date) +
                                                  " have been closed. Please contact the authorities.";
                            args.IsCancel = true;
                            return;
                        }

                        var jdts = new JournalTransactionDetailsCollection();
                        jdts.Query.Where(jdts.Query.JournalId == jhd.JournalId);
                        if (jdts.Query.Load())
                        {
                            jdts.MarkAllAsDeleted();
                            jdts.Save();
                        }

                        jhd.MarkAsDeleted();
                        jhd.Save();
                    }
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new TransPrescription();
            if (!entity.LoadByPrimaryKey(txtPrescriptionNo.Text))
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

            var r = new Registration();
            r.LoadByPrimaryKey(entity.RegistrationNo);
            if (r.IsHoldTransactionEntry ?? false)
            {
                args.MessageText = "Registration locked.";
                args.IsCancel = true;
                return;
            }

            if (r.IsClosed ?? false)
            {
                args.MessageText = "Registration closed.";
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            var entity = new TransPrescription();
            if (!entity.LoadByPrimaryKey(txtPrescriptionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            var r = new Registration();
            r.LoadByPrimaryKey(entity.RegistrationNo);
            if (r.IsHoldTransactionEntry ?? false)
            {
                args.MessageText = "Registration locked.";
                args.IsCancel = true;
                return;
            }

            if (r.IsClosed ?? false)
            {
                args.MessageText = "Registration closed.";
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(TransPrescription entity, bool isVoid)
        {
            //header
            entity.IsVoid = isVoid;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            //detail
            foreach (TransPrescriptionItem item in TransPrescriptionItems)
            {
                item.IsVoid = isVoid;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                TransPrescriptionItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(TransPrescription entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtPrescriptionNo.Text = GetNewPrescriptionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            entity.PrescriptionNo = txtPrescriptionNo.Text;
            entity.PrescriptionDate = txtPrescriptionDate.SelectedDate;

            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.LocationID = cboLocationID.SelectedValue;

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);
            entity.ClassID = reg.ChargeClassID;

            entity.ParamedicID = txtParamedicID.Text;
            entity.IsPrescriptionReturn = true;
            entity.ReferenceNo = txtReferenceNo.Text;
            entity.IsFromSOAP = false;
            entity.IsApproval = false;
            entity.IsVoid = false;
            entity.OrderNo = string.Empty;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                if (entity.es.IsAdded)
                {
                    entity.CreatedByUserID = AppSession.UserLogin.UserID;
                    entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            foreach (var item in TransPrescriptionItems)
            {
                item.PrescriptionNo = entity.PrescriptionNo;
                item.IsReturned = true;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                if (item.es.IsAdded)
                {
                    item.CreatedByUserID = AppSession.UserLogin.UserID;
                    item.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
        }

        private void SaveEntity(TransPrescription entity)
        {
            using (var trans = new esTransactionScope())
            {
                //autonumber has been saved on SetEntity
                //if (entity.es.IsAdded)
                //    _autoNumber.Save();

                entity.Save();

                TransPrescriptionItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new TransPrescriptionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.PrescriptionNo > txtPrescriptionNo.Text,
                        que.IsPrescriptionReturn == true
                    );
                que.OrderBy(que.PrescriptionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.PrescriptionNo < txtPrescriptionNo.Text,
                        que.IsPrescriptionReturn == true
                    );
                que.OrderBy(que.PrescriptionNo.Descending);
            }

            var entity = new TransPrescription();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        private string GetNewPrescriptionNo()
        {
            if (AppSession.Parameter.IsPrescriptionReturnNoFormatBasedOnRegType)
            {
                _autoNumber = Helper.GetNewAutoNumber(txtPrescriptionDate.SelectedDate.Value.Date,
                                                  cboServiceUnitID.SelectedValue ==
                                                  AppSession.Parameter.ServiceUnitPharmacyID
                                                      ? AppEnum.AutoNumber.PrescRetIpNo
                                                      : AppEnum.AutoNumber.PrescRetOpNo);
            }
            else
                _autoNumber = Helper.GetNewAutoNumber(txtPrescriptionDate.SelectedDate.Value.Date, AppEnum.AutoNumber.PrescriptionNo);
            
            return _autoNumber.LastCompleteNumber;
        }

        #region Record Detail Method Function TransPrescriptionItem

        private void RefreshCommandItemTransPrescriptionItem(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdTransPrescriptionItem.Columns[0].Visible = isVisible;
            grdTransPrescriptionItem.Columns[grdTransPrescriptionItem.Columns.Count - 1].Visible = isVisible;

            grdTransPrescriptionItem.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;

            if (oldVal != AppEnum.DataMode.Read)
                TransPrescriptionItems = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdTransPrescriptionItem.Rebind();
        }

        private TransPrescriptionItemCollection TransPrescriptionItems
        {
            get
            {
                if (IsPostBack)
                {
                    if (Session["collTransPrescriptionItem" + Request.UserHostName] != null)
                        return (TransPrescriptionItemCollection)Session["collTransPrescriptionItem" + Request.UserHostName];
                }

                var query = new TransPrescriptionItemQuery("a");
                var item = new ItemQuery("b");
                var inter = new ItemQuery("c");

                query.Select
                    (
                        query,
                        "<CASE WHEN c.ItemName IS NOT NULL THEN c.ItemName ELSE b.ItemName END AS refToItem_ItemName>",
                        "<(a.ResultQty * a.Price) - a.DiscountAmount AS refToTransPrescriptionItem_Total>",
                        "<(a.ParentNo + a.SequenceNo) as ORDERKEY>"
                    );
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.LeftJoin(inter).On(query.ItemInterventionID == inter.ItemID);
                query.Where(query.PrescriptionNo == txtPrescriptionNo.Text);
                query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

                var coll = new TransPrescriptionItemCollection();
                coll.Load(query);

                Session["collTransPrescriptionItem" + Request.UserHostName] = coll;

                return coll;
            }
            set { Session["collTransPrescriptionItem" + Request.UserHostName] = value; }
        }

        private void PopulateTransPrescriptionItemGrid()
        {
            //Display Data Detail
            TransPrescriptionItems = null; //Reset Record Detail
            grdTransPrescriptionItem.DataSource = TransPrescriptionItems; //Requery
            grdTransPrescriptionItem.MasterTableView.IsItemInserted = false;
            grdTransPrescriptionItem.MasterTableView.ClearEditItems();
            grdTransPrescriptionItem.DataBind();

            CostCalculations = null;
            CostCalculations.LoadAll();
        }

        protected void grdTransPrescriptionItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            foreach (var item in TransPrescriptionItems.Where(t => string.IsNullOrEmpty(t.ItemName)))
            {
                var i = new Item();
                i.LoadByPrimaryKey(item.ItemID);
                item.ItemName = i.ItemName;
            }

            grdTransPrescriptionItem.DataSource = TransPrescriptionItems;
        }

        protected void grdTransPrescriptionItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            var sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][TransPrescriptionItemMetadata.ColumnNames.SequenceNo]);
            var entity = FindTransPrescriptionItem(sequenceNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdTransPrescriptionItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            var sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransPrescriptionItemMetadata.ColumnNames.SequenceNo]);
            var entity = FindTransPrescriptionItem(sequenceNo);
            if (entity != null)
            {
                if (DataModeCurrent == AppEnum.DataMode.Edit)
                    entity.IsVoid = true;
                else if (DataModeCurrent == AppEnum.DataMode.New)
                    entity.MarkAsDeleted();
            }
        }

        protected void grdTransPrescriptionItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = TransPrescriptionItems.AddNew();
            SetEntityValue(entity, e);
        }

        private TransPrescriptionItem FindTransPrescriptionItem(String sequenceNo)
        {
            var coll = TransPrescriptionItems;
            return coll.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        private void SetEntityValue(TransPrescriptionItem entity, GridCommandEventArgs e)
        {
            var userControl = (PrescriptionReturnItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SequenceNo = userControl.SequenceNo;
                entity.TakenQty = userControl.ReturnQty;
                entity.ResultQty = userControl.ReturnQty;
                entity.Price = userControl.Price;
                entity.DiscountAmount = userControl.Discount;
                entity.Total = ((entity.ResultQty ?? 0) * (entity.Price ?? 0)) - (entity.DiscountAmount ?? 0);
                entity.SRDiscountReason = userControl.DiscountReason;
            }
        }

        #endregion

        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            return "&nbsp;&nbsp;&nbsp;" + itemName;
        }

        protected override void OnMenuEditClick()
        {
            if (txtPrescriptionNo.Text == string.Empty)
            {
                OnPopulateEntryControl(new TransPrescription());

                //if (cboServiceUnitID.Items.Count == 2)
                //    cboServiceUnitID.SelectedIndex = 1;

                txtRegistrationNo.Text = (string)ViewState["regno"];
                txtPrescriptionDate.SelectedDate = DateTime.Today;
                txtPrescriptionNo.Text = GetNewPrescriptionNo();

                if (Request.QueryString["md"] == "new")
                    PopulateRegistrationInformation(txtRegistrationNo.Text);

                ViewState["IsApproved"] = false;
                ViewState["IsVoid"] = false;
            }
        }

        protected void grdTransPrescriptionItem_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = TransPrescriptionItems[e.Item.DataSetIndex];
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

        protected void grdTransPrescriptionItem_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit" || e.CommandName == "Delete")
            {
                var item = TransPrescriptionItems[e.Item.DataSetIndex];
                if (item != null)
                {
                    if (item.IsBillProceed ?? false)
                        e.Canceled = true;
                }
            }
        }

        private CostCalculationCollection CostCalculations
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = ViewState["collCostCalculation"];
                    if (obj != null)
                        return ((CostCalculationCollection)(obj));
                }

                var coll = new CostCalculationCollection();
                coll.Query.Where
                    (
                        coll.Query.TransactionNo == txtPrescriptionNo.Text,
                        coll.Query.RegistrationNo == txtRegistrationNo.Text
                    );
                coll.LoadAll();

                ViewState["collCostCalculation"] = coll;
                return coll;
            }
            set { ViewState["collCostCalculation"] = value; }
        }

        protected void txtReferenceNo_TextChanged(object sender, EventArgs e)
        {
            if (txtReferenceNo.Text.Equals(string.Empty))
            {
                PopulateTransPrescriptionItemGrid();

                txtParamedicID.Text = string.Empty;
                lblParamedicName.Text = string.Empty;
            }
            else
            {
                var header = new TransPrescription();
                header.LoadByPrimaryKey(txtReferenceNo.Text);

                txtParamedicID.Text = header.ParamedicID;
                var medic = new Paramedic();
                medic.LoadByPrimaryKey(txtParamedicID.Text);
                lblParamedicName.Text = medic.ParamedicName;

                grdTransPrescriptionItem.Rebind();
            }
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtPrescriptionNo.Text = GetNewPrescriptionNo();
            ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, e.Value);
            cboLocationID.SelectedIndex = 1;
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
                    imgPatientPhoto.ImageUrl = optSexMale.Checked ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                }
            }
            else
                imgPatientPhoto.ImageUrl = optSexMale.Checked ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";

        }
        #endregion
    }
}
