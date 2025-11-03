using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class DirectPrescriptionReturnDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;
        private AppAutoNumberLast _autoNumberReg;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "#";
            UrlPageList = "DirectPrescriptionReturnList.aspx";
            ProgramID = AppConstant.Program.DirectPrescriptionReturn;

            if (!IsPostBack)
            {
                TransPrescriptionItems = null;

                //Service Unit
                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, BusinessObject.Reference.TransactionCode.Prescription, true);
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuSearch.Visible = false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdTransPrescriptionItem, grdTransPrescriptionItem);
            ajax.AddAjaxSetting(cboServiceUnitID, txtPrescriptionNo);
            ajax.AddAjaxSetting(cboServiceUnitID, cboServiceUnitID);
            ajax.AddAjaxSetting(cboServiceUnitID, cboLocationID);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new TransPrescription());

            txtPrescriptionDate.SelectedDate = DateTime.Today;
            txtPrescriptionNo.Text = GetNewPrescriptionNo();
            txtRegistrationNo.Text = GetNewRegistrationNo();

            cboLocationID.Items.Clear();
            cboLocationID.Text = string.Empty;

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new TransPrescription();
            if (entity.LoadByPrimaryKey(txtPrescriptionNo.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (TransPrescriptionItems.Count == 0)
            {
                args.MessageText = "Detail transaction is not defined.";
                args.IsCancel = true;
                return;
            }

            var entity = new TransPrescription();
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

            var entity = new TransPrescription();
            if (entity.LoadByPrimaryKey(txtPrescriptionNo.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("PrescriptionNo='{0}'", txtPrescriptionNo.Text.Trim());
            auditLogFilter.TableName = "TransPrescription";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            switch (programID)
            {
                case AppConstant.Report.PrescriptionSlip:
                    printJobParameters.AddNew("p_PrescriptionNo", txtPrescriptionNo.Text);
                    printJobParameters.AddNew("p_Label", "");
                    printJobParameters.AddNew("temp_TITLE", "RESEP FARMASI");
                    break;
                case AppConstant.Report.PrescriptionEtiket:
                    printJobParameters.AddNew("p_PrescriptionNo", txtPrescriptionNo.Text);
                    printJobParameters.AddNew("p_Label", "1");
                    break;
                case AppConstant.Report.PrescriptionEtiketLr:
                    printJobParameters.AddNew("p_PrescriptionNo", txtPrescriptionNo.Text);
                    printJobParameters.AddNew("p_Label", "2");
                    break;
                case AppConstant.Report.PrescriptionReturnReceiptSlip:
                    printJobParameters.AddNew("p_PrescriptionNo", txtPrescriptionNo.Text);
                    printJobParameters.AddNew("p_Label", "");
                    printJobParameters.AddNew("temp_TITLE", "RETUR RESEP FARMASI");
                    break;
                //Untuk RSSA
                case AppConstant.Report.RSSA_PrescriptionSlip:
                    printJobParameters.AddNew("p_PrescriptionNo", txtPrescriptionNo.Text);
                    printJobParameters.AddNew("p_Label", "");
                    printJobParameters.AddNew("temp_TITLE", "RESEP FARMASI");
                    break;
                case AppConstant.Report.RSSA_PrescriptionReturnReceiptSlip:
                    printJobParameters.AddNew("p_PrescriptionNo", txtPrescriptionNo.Text);
                    printJobParameters.AddNew("p_Label", "");
                    printJobParameters.AddNew("temp_TITLE", "RETUR RESEP FARMASI");
                    break;
            }
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
            RefreshCommandItemTransPrescriptionItem(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new TransPrescription();
            if (parameters.Length > 0)
            {
                var prescriptionNo = (String)parameters[0];

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
            txtPrescriptionDate.SelectedDate = transPrescription.PrescriptionDate;
            txtRegistrationNo.Text = transPrescription.RegistrationNo;
            txtReferenceNo.Text = transPrescription.ReferenceNo;
            cboServiceUnitID.SelectedValue = transPrescription.ServiceUnitID;
            if (!string.IsNullOrEmpty(transPrescription.ServiceUnitID))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, transPrescription.ServiceUnitID);
                if (!string.IsNullOrEmpty(transPrescription.LocationID))
                    cboLocationID.SelectedValue = transPrescription.LocationID;
                else
                    cboLocationID.SelectedIndex = 1;
            }

            PopulatePatientInfo(txtReferenceNo.Text);

            if (!string.IsNullOrEmpty(transPrescription.FromServiceUnitID))
            {
                txtServiceUnitID.Text = transPrescription.FromServiceUnitID;
                var unit = new ServiceUnit();
                if (unit.LoadByPrimaryKey(txtServiceUnitID.Text))
                    lblServiceUnitName.Text = unit.ServiceUnitName;
            }
            if (!string.IsNullOrEmpty(transPrescription.FromRoomID))
            {
                txtRoomID.Text = transPrescription.FromRoomID;
                var room = new ServiceRoom();
                if (room.LoadByPrimaryKey(txtRoomID.Text))
                    lblRoomName.Text = room.RoomName;
            }
            if (!string.IsNullOrEmpty(transPrescription.FromBedID))
            {
                txtBedID.Text = transPrescription.FromBedID;
            }
            if (!string.IsNullOrEmpty(transPrescription.ClassID))
            {
                txtClassID.Text = transPrescription.ClassID;
                var cls = new Class();
                if (cls.LoadByPrimaryKey(txtClassID.Text))
                    lblClassName.Text = cls.ClassName;
            }

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

            SetApproval(entity, false, args);
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

            var loc = new Location();
            if (loc.LoadByPrimaryKey(entity.LocationID) && loc.IsHoldForTransaction == true)
            {
                args.MessageText = "Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.";
                args.IsCancel = true;
                return;
            }


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

            using (var trans = new esTransactionScope())
            {
                if (isApproval)
                {
                    foreach (var detail in TransPrescriptionItems)
                    {
                        //Pembulatan qty
                        //detail.RecipeAmount = 0;
                        //detail.Price = detail.Price - (((AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) / 100) * detail.Price);
                        //detail.IsUsingAdminReturn = (AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) > 0;
                        detail.IsApprove = isApproval;
                        detail.IsBillProceed = isApproval;
                        detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        detail.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        var presc = new TransPrescription();
                        presc.LoadByPrimaryKey(entity.ReferenceNo);

                        var prescItem = new TransPrescriptionItem();
                        prescItem.LoadByPrimaryKey(entity.ReferenceNo, detail.SequenceNo);

                        //var calcQUery = new CostCalculationQuery();
                        //calcQUery.Where(calcQUery.TransactionNo == entity.ReferenceNo,
                        //                calcQUery.SequenceNo == detail.SequenceNo);

                        //var calc = new CostCalculation();
                        //calc.Load(calcQUery);

                        CostCalculation cost = CostCalculations.AddNew();
                        cost.RegistrationNo = entity.RegistrationNo;
                        cost.TransactionNo = detail.PrescriptionNo;
                        cost.SequenceNo = detail.SequenceNo;
                        cost.ItemID = string.IsNullOrEmpty(detail.ItemInterventionID) ? detail.ItemID : detail.ItemInterventionID;

                        var tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, reg.GuarantorID, string.IsNullOrEmpty(detail.ItemInterventionID) ? detail.ItemID : detail.ItemInterventionID, presc.PrescriptionDate.Value.Date, true);
                        var calc = new Helper.CostCalculation(reg.GuarantorID, reg.IsGlobalPlafond ?? false,
                                       string.IsNullOrEmpty(detail.ItemInterventionID) ? detail.ItemID : detail.ItemInterventionID, Math.Abs(detail.LineAmount ?? 0),
                                       tblCovered, Math.Abs(detail.ResultQty ?? 0), detail.Price ?? 0, detail.RecipeAmount ?? 0, detail.DiscountAmount ?? 0);

                        cost.PatientAmount = 0 - calc.PatientAmount;
                        cost.DiscountAmount = 0 - calc.DiscountAmount;
                        cost.GuarantorAmount = 0 - calc.GuarantorAmount;
                        cost.IntermBillNo = null;
                        cost.IsChecked = null;

                        //cost.PatientAmount = 0 - (Math.Abs(detail.ResultQty ?? 0) / presc.ResultQty) * calc.PatientAmount;
                        //cost.PatientAmount = (calc.PatientAmount + (((AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) / 100) * Math.Abs(calc.PatientAmount ?? 0)));
                        //cost.GuarantorAmount = 0 - (Math.Abs(detail.ResultQty ?? 0) / presc.ResultQty) * calc.GuarantorAmount;
                        //cost.GuarantorAmount = (calc.GuarantorAmount + (((AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) / 100) * Math.Abs(calc.GuarantorAmount ?? 0)));
                        //cost.DiscountAmount = 0 - (Math.Abs(detail.ResultQty ?? 0) / presc.ResultQty) * calc.DiscountAmount;
                        cost.ParamedicAmount = 0;
                        cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        cost.LastUpdateByUserID = AppSession.UserLogin.UserID;

                        //update IsReturned
                        prescItem.IsReturned = true;
                        prescItem.Save();
                    }
                }
                else
                    CostCalculations.MarkAllAsDeleted();

                entity.Save();

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(entity.ServiceUnitID);

                //if (isApproval)
                //{
                //    string itemZeroCostPrice;
                //    ItemBalance.UpdateCostPrice(TransPrescriptionItems, out itemZeroCostPrice);
                //    if (!string.IsNullOrEmpty(itemZeroCostPrice))
                //    {
                //        args.MessageText = "Zero cost price of item : " + itemZeroCostPrice;
                //        args.IsCancel = true;
                //        return;
                //    }
                //}

                // stock calculation
                var chargesBalances = new ItemBalanceCollection();
                var chargesDetailBalances = new ItemBalanceDetailCollection();
                var chargesDetailBalanceEds = new ItemBalanceDetailEdCollection();
                var chargesMovements = new ItemMovementCollection();

                if (isApproval)
                    ItemBalance.PrepareItemBalancesForReturn(txtPrescriptionNo.Text, TransPrescriptionItems,
                                                             BusinessObject.Reference.TransactionCode.PrescriptionReturn,
                                                             entity.ServiceUnitID,
                                                             entity.LocationID, AppSession.UserLogin.UserID, isApproval,
                                                             ref chargesBalances, ref chargesDetailBalances,
                                                             ref chargesMovements, ref chargesDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl);
                else
                    ItemBalance.PrepareItemBalancesForReturn(TransPrescriptionItems,
                                                             BusinessObject.Reference.TransactionCode.PrescriptionReturn,
                                                             unit.ServiceUnitID,
                                                             entity.LocationID, AppSession.UserLogin.UserID,
                                                             ref chargesBalances, ref chargesDetailBalances,
                                                             ref chargesMovements, ref chargesDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl);

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
                if (isApproval)
                {
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

                            int? journalId = JournalTransactions.AddNewDirectPrescriptionReturnJournal(entity, reg, unit, CostCalculations, "RS", AppSession.UserLogin.UserID);
                        }
                    }
                }
                else
                {
                    //hapus jurnal karena dianggap batal transaksi di hari yg sama
                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                    {
                        var jhd = new JournalTransactions();
                        jhd.Query.Where(jhd.Query.RefferenceNumber == entity.PrescriptionNo);
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
                }
                /* Automatic Journal Testing End */

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

            using (esTransactionScope trans = new esTransactionScope())
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

            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtRegistrationNo.Text = GetNewRegistrationNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumberReg.Save();
            }

            entity.RegistrationNo = txtRegistrationNo.Text;

            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.LocationID = cboLocationID.SelectedValue;
            //entity.ClassID = AppSession.Parameter.DefaultTariffClass;
            entity.ClassID = txtClassID.Text;
            entity.ParamedicID = txtParamedicID.Text;
            entity.Note = string.Empty;
            entity.IsPrescriptionReturn = true;
            entity.ReferenceNo = txtReferenceNo.Text;
            entity.IsFromSOAP = false;
            entity.FromServiceUnitID = txtServiceUnitID.Text;
            entity.FromRoomID = txtRoomID.Text;
            entity.FromBedID = txtBedID.Text;

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

            foreach (TransPrescriptionItem item in TransPrescriptionItems)
            {
                item.PrescriptionNo = entity.PrescriptionNo;
                item.IsReturned = true;
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    if (item.es.IsAdded)
                    {
                        item.CreatedByUserID = AppSession.UserLogin.UserID;
                        item.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                    }
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

                if (entity.es.IsAdded)
                {
                    var reg = new Registration();
                    reg.RegistrationNo = txtRegistrationNo.Text;
                    reg.SRRegistrationType = AppConstant.RegistrationType.OutPatient;
                    reg.PatientID = txtPatientID.Text;
                    reg.ClassID = AppSession.Parameter.OutPatientClassID;
                    reg.RegistrationDate = txtPrescriptionDate.SelectedDate;
                    reg.RegistrationTime = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
                    reg.AgeInYear = Convert.ToByte(txtAgeYear.Value);
                    reg.AgeInMonth = Convert.ToByte(txtAgeMonth.Value);
                    reg.AgeInDay = Convert.ToByte(txtAgeDay.Value);
                    reg.SRShift = Registration.GetShiftID();
                    reg.DepartmentID = AppParameter.GetParameterValue(AppParameter.ParameterItem.PharmacyDepartmentID);
                    reg.ServiceUnitID = cboServiceUnitID.SelectedValue;
                    reg.ChargeClassID = AppSession.Parameter.OutPatientClassID;
                    reg.CoverageClassID = AppSession.Parameter.OutPatientClassID;
                    reg.GuarantorID = AppSession.Parameter.SelfGuarantor;
                    reg.str.ParamedicID = txtParamedicID.Text;

                    //Last Update Status
                    reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    reg.IsFromDispensary = true;
                    reg.IsDirectPrescriptionReturn = true;
                    reg.Save();

                    var mrg = new MergeBilling();
                    mrg.RegistrationNo = reg.RegistrationNo;
                    mrg.FromRegistrationNo = string.Empty;

                    //Last Update Status
                    mrg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    mrg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    mrg.Save();

                    entity.RegistrationNo = reg.RegistrationNo;

                    //autonumber has been saved on SetEntity
                    //_autoNumberReg.Save();
                }

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
            _autoNumber = Helper.GetNewAutoNumber(txtPrescriptionDate.SelectedDate.Value.Date,
                                                  AppSession.Parameter.IsPrescriptionReturnNoFormatBasedOnRegType
                                                      ? AppEnum.AutoNumber.PrescRetOpNo
                                                      : AppEnum.AutoNumber.PrescriptionNo);

            return _autoNumber.LastCompleteNumber;
        }

        private string GetNewRegistrationNo()
        {
            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(AppSession.Parameter.ServiceUnitPharmacyID);
            _autoNumberReg = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, BusinessObject.Reference.TransactionCode.Registration,
                unit.DepartmentID);
            return _autoNumberReg.LastCompleteNumber;
        }

        #region Record Detail Method Function TransPrescriptionItem

        private void RefreshCommandItemTransPrescriptionItem(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdTransPrescriptionItem.Columns[0].Visible = isVisible;
            grdTransPrescriptionItem.Columns[grdTransPrescriptionItem.Columns.Count - 1].Visible = isVisible;

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
                    object obj = Session["collTransPrescriptionItem" + Request.UserHostName];
                    if (obj != null)
                        return ((TransPrescriptionItemCollection)(obj));
                }

                var coll = new TransPrescriptionItemCollection();
                //coll.CreateColumnsForBinding("Total2");

                var query = new TransPrescriptionItemQuery("a");
                var qItem = new ItemQuery("b");
                var qItemI = new ItemQuery("c");

                var total = new esQueryItem(query, "Total", esSystemType.Decimal);
                total = (query.ResultQty * query.Price) - query.DiscountAmount;

                query.Select
                    (
                        query,
                        qItem.ItemName.As("refToItem_ItemName"),
                        qItemI.ItemName.As("refToItem_ItemInterventionName"),
                        total.As("refToTransPrescriptionItem_Total"),
                        "<(a.ParentNo + a.SequenceNo) as ORDERKEY>"
                    );
                query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
                query.LeftJoin(qItemI).On(query.ItemInterventionID == qItemI.ItemID);
                query.Where(query.PrescriptionNo == txtPrescriptionNo.Text);
                query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);
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
            if (editedItem == null)
                return;

            var sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                [TransPrescriptionItemMetadata.ColumnNames.SequenceNo]);
            var entity = FindTransPrescriptionItem(sequenceNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdTransPrescriptionItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            var sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]
                [TransPrescriptionItemMetadata.ColumnNames.SequenceNo]);
            var entity = FindTransPrescriptionItem(sequenceNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdTransPrescriptionItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = TransPrescriptionItems.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdTransPrescriptionItem.Rebind();
        }

        private TransPrescriptionItem FindTransPrescriptionItem(String sequenceNo)
        {
            var coll = TransPrescriptionItems;
            TransPrescriptionItem retEntity = null;
            foreach (var rec in coll)
            {
                if (rec.SequenceNo.Equals(sequenceNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(TransPrescriptionItem entity, GridCommandEventArgs e)
        {
            var userControl = (PrescriptionSalesItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.PrescriptionNo = txtPrescriptionNo.Text;
                entity.SequenceNo = userControl.SequenceNo;
                entity.ParentNo = userControl.ParentNo;

                if (!string.IsNullOrEmpty(userControl.ParentNo))
                    entity.IsRFlag = false;
                else
                    entity.IsRFlag = true;

                entity.IsCompound = userControl.IsCompound;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.ItemInterventionID = userControl.ItemInterventionID;
                entity.ItemInterventionName = userControl.ItemInterventionName;
                entity.SRItemUnit = userControl.ItemUnit;
                entity.ItemQtyInString = userControl.ItemQtyInString;
                //entity.IsUsingDosageUnit = userControl.IsUsingDosageUnit;
                entity.SRDosageUnit = userControl.SRDosageUnit;
                //entity.FrequencyOfDosing = userControl.FrequencyOfDosing;
                //entity.DosingPeriod = userControl.DosingPeriod;
                //entity.NumberOfDosage = userControl.NumberOfDosage;
                //entity.DurationOfDosing = userControl.DurationOfDosing;
                //entity.Acpcdc = userControl.Acpcdc;
                //entity.SRMedicationRoute = userControl.SRMedicationRoute;
                //entity.ConsumeMethod = userControl.ConsumeMethod;
                entity.PrescriptionQty = userControl.PrescriptionQty;
                entity.TakenQty = userControl.TakenQty;
                entity.ResultQty = userControl.ResultQty;
                entity.CostPrice = userControl.CostPrice;
                entity.InitialPrice = userControl.InitialPrice;
                entity.Price = userControl.Price;
                entity.DiscountAmount = userControl.DiscountAmount;
                entity.Total = (entity.ResultQty ?? 0) * ((entity.Price ?? 0) - (entity.DiscountAmount ?? 0));
                entity.EmbalaceID = userControl.EmbalaceID;
                entity.EmbalaceAmount = 0;
                //entity.IsUseSweetener = userControl.IsUseSweetener;
                entity.SweetenerAmount = 0;
                entity.LineAmount = userControl.LineAmount;
                entity.SRDiscountReason = userControl.SRDiscountReason;
                entity.Notes = userControl.Notes;
            }
        }

        #endregion

        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            return "&nbsp;&nbsp;&nbsp;" + itemName.ToString();
        }

        private CostCalculationCollection CostCalculations
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = ViewState["collCostCalculation" + Request.UserHostName];
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

                ViewState["collCostCalculation" + Request.UserHostName] = coll;
                return coll;
            }
            set { ViewState["collCostCalculation" + Request.UserHostName] = value; }
        }

        protected void txtReferenceNo_TextChanged(object sender, EventArgs e)
        {
            if (txtReferenceNo.Text != string.Empty)
                PopulatePatientInfo(txtReferenceNo.Text);
            else
                ResetPatientInfo();

            grdTransPrescriptionItem.Rebind();
        }

        private void PopulatePatientInfo(string prescriptionNo)
        {
            var presc = new TransPrescription();
            if (!presc.LoadByPrimaryKey(prescriptionNo))
            {
                ResetPatientInfo();
                return;
            }

            var reg = new Registration();
            if (!reg.LoadByPrimaryKey(presc.RegistrationNo))
            {
                ResetPatientInfo();
                return;
            }

            var pat = new Patient();
            if (!pat.LoadByPrimaryKey(reg.PatientID))
            {
                ResetPatientInfo();
                return;
            }

            txtPatientID.Text = pat.PatientID;
            txtMedicalNo.Text = pat.MedicalNo;
            var std = new AppStandardReferenceItem();
            txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
            txtPatientName.Text = pat.PatientName;

            optSexFemale.Checked = (pat.Sex == "F");
            optSexFemale.Enabled = (pat.Sex == "F");
            optSexMale.Checked = (pat.Sex == "M");
            optSexMale.Enabled = (pat.Sex == "M");

            txtAgeYear.Text = reg.AgeInYear.ToString();
            txtAgeMonth.Text = reg.AgeInMonth.ToString();
            txtAgeDay.Text = reg.AgeInDay.ToString();

            txtParamedicID.Text = reg.str.ParamedicID;
            var medic = new Paramedic();
            lblParamedicName.Text = medic.LoadByPrimaryKey(txtParamedicID.Text) ? medic.ParamedicName : string.Empty;

            txtServiceUnitID.Text = reg.str.ServiceUnitID;
            var unit = new ServiceUnit();
            lblServiceUnitName.Text = unit.LoadByPrimaryKey(txtServiceUnitID.Text) ? unit.ServiceUnitName : string.Empty;

            txtRoomID.Text = reg.str.RoomID;
            var room = new ServiceRoom();
            lblRoomName.Text = room.LoadByPrimaryKey(txtRoomID.Text) ? room.RoomName : string.Empty;

            txtBedID.Text = reg.str.BedID;

            txtClassID.Text = reg.str.ChargeClassID;
            var cls = new Class();
            lblClassName.Text = cls.LoadByPrimaryKey(txtClassID.Text) ? cls.ClassName : string.Empty;
        }

        private void ResetPatientInfo()
        {
            txtPatientID.Text = string.Empty;
            txtMedicalNo.Text = string.Empty;
            txtPatientName.Text = string.Empty;
            optSexFemale.Checked = false;
            optSexFemale.Enabled = true;
            optSexMale.Checked = false;
            optSexMale.Enabled = true;
            txtAgeYear.Text = string.Empty;
            txtAgeMonth.Text = string.Empty;
            txtAgeDay.Text = string.Empty;
            txtParamedicID.Text = string.Empty;
            lblParamedicName.Text = string.Empty;
            txtServiceUnitID.Text = string.Empty;
            lblServiceUnitName.Text = string.Empty;
            txtRoomID.Text = string.Empty;
            lblRoomName.Text = string.Empty;
            txtBedID.Text = string.Empty;
            txtClassID.Text = string.Empty;
            lblClassName.Text = string.Empty;
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtPrescriptionNo.Text = GetNewPrescriptionNo();
            ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, e.Value);
            cboLocationID.SelectedIndex = 1;
        }
    }
}
