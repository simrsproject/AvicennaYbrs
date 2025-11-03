using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.DynamicQuery;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class NonPatientCustomerChargesDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber, _amplopFilmAutoNumber;
        private AppAutoNumberLast _autoNumberReg;

        #region Page Event & Initialize

        public bool IsReturn {
            get {
                return Request.QueryString["source"] == "return";
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "NonPatientCustomerChargesSearch.aspx";
            if (IsReturn)
            {
                UrlPageList = "NPCItemReturnList.aspx";
                ProgramID = AppConstant.Program.NonPatientCustomerChargesItemReturn;
            }
            else
            {
                UrlPageList = "NonPatientCustomerChargesList.aspx";
                ProgramID = AppConstant.Program.NonPatientCustomerCharges;
            }

            if (!IsPostBack)
            {
                TransChargesItems = null;
                TransChargesItemComps = null;
                TransChargesItemConsumptions = null;
                ExtramuralItems = null;

                txtPatientName.Visible = false;
                btnNewPatient.Visible = true;


                var unit = new ServiceUnit();
                if (unit.LoadByPrimaryKey(AppSession.Parameter.ServiceUnitPharmacyID))
                {
                    //  txtServiceUnitID.Text = unit.ServiceUnitID;
                    //  lblServiceUnitName.Text = unit.ServiceUnitName;
                }


                //Service Unit
                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, BusinessObject.Reference.TransactionCode.NonPatientCustomerharges, true);
                trExtramural.Visible = AppSession.Parameter.IsUsingExtramuralItem;
                StandardReference.InitializeIncludeSpace(cboSRGenderType, AppEnum.StandardReference.GenderType);

                //var cstext1 = new StringBuilder();
                //cstext1.Append("<script type=text/javascript> __doPostBack('ctl00$ContentPlaceHolder1$grdTransPrescriptionItem$ctl00$ctl02$ctl00$AddNewRecordButton','') </script>");

                //Page.ClientScript.RegisterStartupScript(GetType(), "OpenAddNewRecordGrid", cstext1.ToString());
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string eventTarget = Request.Params.Get("__EVENTTARGET");
            if (eventTarget == "ShowNewPatient")
            {
                string passedArgument = Request.Params.Get("__EVENTARGUMENT");
                PopulatePatientInformation(passedArgument);
                PopulateGuarantorFromPatientInformation(passedArgument);
            }


        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuSearch.Visible = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"] && !IsReturn;
        }

        private void PopulateRegistrationInformation(string registrationNo)
        {
            if (string.IsNullOrEmpty(registrationNo))
                return;

            var registration = new Registration();
            if (registration.LoadByPrimaryKey(registrationNo))
            {
                var patient = new Patient();
                if (patient.LoadByPrimaryKey(registration.PatientID))
                {
                    txtMedicalNo.Text = patient.MedicalNo;
                    txtPatientName.Text = patient.PatientName;
                    cboSRGenderType.SelectedValue = patient.Sex;
                    txtAgeDay.Value = Helper.GetAgeInDay(patient.DateOfBirth.Value);
                    txtAgeMonth.Value = Helper.GetAgeInMonth(patient.DateOfBirth.Value);
                    txtAgeYear.Value = Helper.GetAgeInYear(patient.DateOfBirth.Value);
                }
                else
                {
                    cboSRGenderType.Text = string.Empty;
                    cboSRGenderType.SelectedValue = string.Empty;
                }

                var patQ = new PatientQuery();
                patQ.Select(patQ.PatientID, patQ.PatientName, patQ.DateOfBirth, patQ.MedicalNo, patQ.Address);
                patQ.Where(patQ.PatientID == registration.PatientID);
                DataTable patTbl = patQ.LoadDataTable();
                cboPatientID.DataSource = patTbl;
                cboPatientID.DataBind();
                cboPatientID.SelectedValue = registration.PatientID;
                cboPatientID.Text = patTbl.Rows[0]["PatientName"].ToString();

                cboServiceUnitID.SelectedValue = registration.ServiceUnitID;
                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                {
                    ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, cboServiceUnitID.SelectedValue);
                    cboLocationID.SelectedIndex = 1;
                }

                var grr = new GuarantorQuery();
                grr.Where(grr.GuarantorID == registration.GuarantorID);
                cboGuarantorID.DataSource = grr.LoadDataTable();
                cboGuarantorID.DataBind();
                cboGuarantorID.SelectedValue = registration.GuarantorID;
            }
            
        }

        private void PopulatePatientInformation(string patientID)
        {
            if (string.IsNullOrEmpty(patientID))
                return;

            var patient = new Patient();
            if (patient.LoadByPrimaryKey(patientID))
            {
                cboPatientID.SelectedValue = patient.PatientID;
                cboPatientID.Text = patient.PatientName;
                txtMedicalNo.Text = patient.MedicalNo;
                txtPatientName.Text = patient.PatientName;
                cboSRGenderType.SelectedValue = patient.Sex;
                txtAgeDay.Value = Helper.GetAgeInDay(patient.DateOfBirth.Value);
                txtAgeMonth.Value = Helper.GetAgeInMonth(patient.DateOfBirth.Value);
                txtAgeYear.Value = Helper.GetAgeInYear(patient.DateOfBirth.Value);
            }
        }

        private void PopulateGuarantorFromPatientInformation(string patientId)
        {
            if (string.IsNullOrEmpty(patientId))
                return;
            cboGuarantorID.Items.Clear();
            cboGuarantorID.Text = string.Empty;
            var patient = new Patient();
            if (patient.LoadByPrimaryKey(patientId))
            {
                var grr = new GuarantorQuery();
                grr.Where(grr.GuarantorID == patient.GuarantorID);
                cboGuarantorID.DataSource = grr.LoadDataTable();
                cboGuarantorID.DataBind();
                cboGuarantorID.SelectedValue = patient.GuarantorID;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdTransChargesItem, grdTransChargesItem);
            ajax.AddAjaxSetting(grdTransChargesItem, cboServiceUnitID);
            ajax.AddAjaxSetting(grdTransChargesItem, cboLocationID);
            ajax.AddAjaxSetting(grdTransChargesItem, cboGuarantorID);
            ajax.AddAjaxSetting(txtRegistrationNo, cboGuarantorID);
            ajax.AddAjaxSetting(cboGuarantorID, cboGuarantorID);
            ajax.AddAjaxSetting(cboServiceUnitID, txtRegistrationNo);
            ajax.AddAjaxSetting(cboServiceUnitID, cboLocationID);
            ajax.AddAjaxSetting(cboServiceUnitID, cboServiceUnitID);
            
            if (AppSession.Parameter.IsUsingExtramuralItem)
                ajax.AddAjaxSetting(grdExtramural, grdExtramural);
            //var toolbar = (RadToolBar)Helper.FindControlRecursive(this, "fw_tbarData");
            //ajax.AddAjaxSetting(toolbar, grdTransChargesItem);
            //ajax.AddAjaxSetting(toolbar, grdExtramural);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new TransCharges());

            ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, BusinessObject.Reference.TransactionCode.NonPatientCustomerharges, true);

            txtTransactionDate.SelectedDate = DateTime.Today;
            txtTransactionNo.Text = GetNewTransactionNo();

            cboServiceUnitID.AutoPostBack = true;
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                txtRegistrationNo.Text = GetNewRegistrationNo();
                ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, cboServiceUnitID.SelectedValue);
                cboLocationID.SelectedIndex = 1;
            }
            else
            {
                cboLocationID.Items.Clear();
                cboLocationID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
                PopulateRegistrationInformation(txtRegistrationNo.Text);
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                cboSRGenderType.Text = string.Empty;
                cboSRGenderType.SelectedValue = string.Empty;
                txtAgeDay.Value = 0;
                txtAgeMonth.Value = 0;
                txtAgeYear.Value = 0;

                cboPatientID.Items.Clear();
                cboPatientID.Text = string.Empty;

                cboGuarantorID.Items.Clear();
                cboGuarantorID.Text = string.Empty;   
            }

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
            //}
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

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            TransCharges entity = new TransCharges();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

        }

        private bool IsApprovedOrVoid(TransCharges entity, ValidateArgs args)
        {
            if (entity.IsApproved != null && entity.IsApproved.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid != null && entity.IsVoid.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }
            return true;
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

        #endregion

        #region ToolBar Menu Support

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            switch (programID)
            {
                default:
                    printJobParameters.AddNew("TransactionNo", txtTransactionNo.Text);
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
            RefreshCommandItemTransChargesItem(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new TransCharges();
            if (parameters.Length > 0)
            {
                var TransactionNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(TransactionNo);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var transCharges = (TransCharges)entity;

            txtTransactionNo.Text = transCharges.TransactionNo;
            txtTransactionDate.SelectedDate = transCharges.TransactionDate;

            txtRegistrationNo.Text = transCharges.RegistrationNo;
            cboServiceUnitID.SelectedValue = transCharges.FromServiceUnitID;
            if (!string.IsNullOrEmpty(transCharges.FromServiceUnitID))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, transCharges.FromServiceUnitID);
                if (!string.IsNullOrEmpty(transCharges.LocationID))
                    cboLocationID.SelectedValue = transCharges.LocationID;
                else
                    cboLocationID.SelectedIndex = 1;
            }
            if (DataModeCurrent != AppEnum.DataMode.New)
            {
                PopulateRegistrationInformation(txtRegistrationNo.Text);
                btnNewPatient.Visible = false;
                txtPatientName.Visible = true;
                rfvTxtPatientName.Visible = true;
                cboPatientID.Visible = false;
                rfvCboPatientID.Visible = false;
            }
            else
            {
                btnNewPatient.Visible = true;
                txtPatientName.Visible = false;
                rfvTxtPatientName.Visible = false;
                cboPatientID.Visible = true;
                rfvCboPatientID.Visible = true;
            }

            ViewState["IsApproved"] = (transCharges.IsApproved ?? false) && !IsReturn;
            ViewState["IsVoid"] = transCharges.IsVoid ?? false;

            //Display Data Detail
            PopulateTransChargesItemGrid();

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
            //if (entity.IsApproved ?? false)
            //{
            //    args.MessageText = AppConstant.Message.RecordHasApproved;
            //    args.IsCancel = true;
            //    return;
            //}
            //if (entity.IsVoid ?? false)
            //{
            //    args.MessageText = AppConstant.Message.RecordHasVoided;
            //    args.IsCancel = true;
            //    return;
            //}

            //var reg = new Registration();
            //reg.LoadByPrimaryKey(entity.RegistrationNo);

            //if (reg.IsClosed ?? false)
            //{
            //    args.MessageText = string.Format("Registration has been closed.");
            //    args.IsCancel = true;
            //    return;
            //}

            //SetApproval(entity, true, args);

            try
            {
                string retMsg = ServiceUnitTransactionDetail.SetApproval(entity, true,
                    TransChargesItems, TransChargesItemComps, TransChargesItemConsumptions,
                    string.Empty, CostCalculations,
                    "npc", string.Empty, string.Empty,
                    txtTransactionDate.SelectedDate.Value, _amplopFilmAutoNumber, args);
                if (!retMsg.Equals(string.Empty))
                {
                    args.MessageText = retMsg;
                    args.IsCancel = true;
                }
            }
            catch (Exception e)
            {
                // lagi cari penyebab approve header tapi detailnya gak approve
                LogError(e);
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new TransCharges();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetUnApproval(entity, false, args);
        }

        //private void SetApproval(TransCharges entity, bool isApproval, ValidateArgs args)
        //{
        //    var reg = new Registration();
        //    reg.LoadByPrimaryKey(entity.RegistrationNo);

        //    if (reg.IsHoldTransactionEntry ?? false)
        //    {
        //        args.MessageText = "Transaction is locked.";
        //        args.IsCancel = true;
        //        return;
        //    }

        //    using (var trans = new esTransactionScope())
        //    {
        //        //header
        //        entity.IsApproved = isApproval;
        //        entity.IsBillProceed = isApproval;

        //        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //        entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

        //        var grrID = reg.GuarantorID;

        //        var pat = new Patient();
        //        pat.LoadByPrimaryKey(reg.PatientID);

        //        if (grrID == AppSession.Parameter.SelfGuarantor)
        //        {
        //            if (!string.IsNullOrEmpty(pat.MemberID))
        //                grrID = pat.MemberID;
        //        }

        //        var grr = new Guarantor();
        //        grr.LoadByPrimaryKey(reg.GuarantorID);

        //        var unit = new ServiceUnit();
        //        unit.LoadByPrimaryKey(entity.ToServiceUnitID);

        //        var tblCovered = new DataTable();

        //        if (isApproval)
        //        {
        //            tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, grrID, entity.ClassID, reg.CoverageClassID,
        //                                                (TransChargesItems.Select(t => t.ItemID)).ToArray(),
        //                                                entity.TransactionDate.Value, false);

        //            foreach (TransChargesItem detail in TransChargesItems.Where(t => string.IsNullOrEmpty(t.ParentNo)))
        //            {
        //                if (detail.IsVoid ?? false)
        //                    continue;

        //                var rowCovered = tblCovered.AsEnumerable().SingleOrDefault(t => t.Field<string>("ItemID") == detail.ItemID &&
        //                                                                                t.Field<bool>("IsInclude"));

        //                //TransChargesItemComps
        //                if (rowCovered != null)
        //                {
        //                    decimal? discount = 0;
        //                    bool isDiscount = false, isMargin = false;
        //                    foreach (var comp in TransChargesItemComps.Where(t => t.TransactionNo == detail.TransactionNo &&
        //                                                                          t.SequenceNo == detail.SequenceNo)
        //                                                              .OrderBy(t => t.TariffComponentID))
        //                    {
        //                        var amountValue = (decimal?)rowCovered["AmountValue"];
        //                        var basicPrice = (decimal?)rowCovered["BasicPrice"];
        //                        var coveragePrice = (decimal?)rowCovered["CoveragePrice"];

        //                        if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
        //                        {
        //                            if ((comp.Price - comp.DiscountAmount) <= 0)
        //                                continue;

        //                            var compPrice = comp.Price ?? 0;
        //                            if (basicPrice > coveragePrice)
        //                            {
        //                                var tcomp = Helper.Tariff.GetItemTariffComponent(entity.TransactionDate.Value, grr.SRTariffType,
        //                                        reg.CoverageClassID, comp.TariffComponentID, detail.ItemID);
        //                                if (!tcomp.AsEnumerable().Any())
        //                                    tcomp = Helper.Tariff.GetItemTariffComponent(entity.TransactionDate.Value, grr.SRTariffType,
        //                                        AppSession.Parameter.DefaultTariffClass, comp.TariffComponentID, detail.ItemID);
        //                                if (!tcomp.AsEnumerable().Any())
        //                                    tcomp = Helper.Tariff.GetItemTariffComponent(entity.TransactionDate.Value, AppSession.Parameter.DefaultTariffType,
        //                                        reg.CoverageClassID, comp.TariffComponentID, detail.ItemID);
        //                                if (!tcomp.AsEnumerable().Any())
        //                                    tcomp = Helper.Tariff.GetItemTariffComponent(entity.TransactionDate.Value, AppSession.Parameter.DefaultTariffType,
        //                                        AppSession.Parameter.DefaultTariffClass, comp.TariffComponentID, detail.ItemID);

        //                                if (!tcomp.AsEnumerable().Any())
        //                                    continue;

        //                                compPrice = tcomp.AsEnumerable().Select(c => c.Field<decimal>("Price")).Single();
        //                            }

        //                            if ((bool)rowCovered["IsValueInPercent"])
        //                            {
        //                                comp.DiscountAmount += (amountValue / 100) * compPrice;
        //                                comp.AutoProcessCalculation = 0 - (amountValue / 100) * compPrice;
        //                            }
        //                            else
        //                            {
        //                                if (!isDiscount)
        //                                {
        //                                    if (discount == 0)
        //                                    {
        //                                        if (compPrice >= amountValue)
        //                                        {
        //                                            comp.DiscountAmount += amountValue;
        //                                            comp.AutoProcessCalculation = 0 - amountValue;
        //                                            isDiscount = true;
        //                                        }
        //                                        else
        //                                        {
        //                                            comp.DiscountAmount += compPrice;
        //                                            comp.AutoProcessCalculation = 0 - compPrice;
        //                                            discount = amountValue - compPrice;
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        if (compPrice >= discount)
        //                                        {
        //                                            comp.DiscountAmount += discount;
        //                                            comp.AutoProcessCalculation = 0 - discount;
        //                                            isDiscount = true;
        //                                        }
        //                                        else
        //                                        {
        //                                            comp.DiscountAmount += compPrice;
        //                                            comp.AutoProcessCalculation = 0 - compPrice;
        //                                            discount -= compPrice;
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
        //                        {
        //                            if ((bool)rowCovered["IsValueInPercent"])
        //                            {
        //                                comp.AutoProcessCalculation = (amountValue / 100) * comp.Price;
        //                                comp.Price += (amountValue / 100) * comp.Price;

        //                            }
        //                            else
        //                            {
        //                                if (!isMargin)
        //                                {
        //                                    comp.Price += amountValue;
        //                                    comp.AutoProcessCalculation = amountValue;
        //                                    isMargin = true;
        //                                }
        //                            }
        //                        }
        //                        comp.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //                        comp.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //                    }
        //                }

        //                //TransChargesItems
        //                detail.IsApprove = isApproval;
        //                detail.IsBillProceed = isApproval;
        //                if (Request.QueryString["type"] == "ds")
        //                {
        //                    detail.IsOrderRealization = true;
        //                    detail.RealizationDateTime = (new DateTime()).NowAtSqlServer();
        //                    detail.RealizationUserID = AppSession.UserLogin.UserID;
        //                }

        //                if (TransChargesItemComps.Count > 0)
        //                {
        //                    detail.AutoProcessCalculation = TransChargesItemComps.Where(t => t.TransactionNo == detail.TransactionNo &&
        //                                                                                     t.SequenceNo == detail.SequenceNo)
        //                                                                         .Sum(t => t.AutoProcessCalculation);
        //                    if (detail.AutoProcessCalculation < 0)
        //                    {
        //                        detail.DiscountAmount += detail.ChargeQuantity * Math.Abs(detail.AutoProcessCalculation ?? 0);

        //                        if (detail.DiscountAmount > (detail.Price * Math.Abs(detail.ChargeQuantity ?? 0)))
        //                        {
        //                            detail.DiscountAmount = detail.Price * Math.Abs(detail.ChargeQuantity ?? 0);
        //                            detail.AutoProcessCalculation = 0 - detail.Price;
        //                        }
        //                    }
        //                    else if (detail.AutoProcessCalculation > 0)
        //                        detail.Price += detail.AutoProcessCalculation;
        //                }
        //                else
        //                {
        //                    if (rowCovered != null)
        //                    {
        //                        if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
        //                        {
        //                            var basicPrice = (decimal?)rowCovered["BasicPrice"];
        //                            var coveragePrice = (decimal?)rowCovered["CoveragePrice"];
        //                            var detailPrice = detail.Price ?? 0;
        //                            if (basicPrice > coveragePrice)
        //                            {
        //                                ItemTariff tariff = (Helper.Tariff.GetItemTariff(entity.TransactionDate.Value, grr.SRTariffType, reg.CoverageClassID, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
        //                                             Helper.Tariff.GetItemTariff(entity.TransactionDate.Value, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
        //                                            (Helper.Tariff.GetItemTariff(entity.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, reg.CoverageClassID, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
        //                                             Helper.Tariff.GetItemTariff(entity.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));
        //                                if (tariff != null)
        //                                    detailPrice = tariff.Price ?? 0;
        //                            }

        //                            if ((bool)rowCovered["IsValueInPercent"])
        //                            {
        //                                detail.DiscountAmount += (detail.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * detailPrice);
        //                                detail.AutoProcessCalculation = 0 - (((decimal)rowCovered["AmountValue"] / 100) * detailPrice);
        //                            }
        //                            else
        //                            {
        //                                detail.DiscountAmount += (detail.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];
        //                                detail.AutoProcessCalculation = 0 - (decimal)rowCovered["AmountValue"];
        //                            }

        //                            if (detail.DiscountAmount > (detailPrice * detail.ChargeQuantity))
        //                                detail.DiscountAmount = detailPrice * detail.ChargeQuantity;
        //                        }
        //                        else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
        //                        {
        //                            if ((bool)rowCovered["IsValueInPercent"])
        //                            {
        //                                detail.AutoProcessCalculation = ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;
        //                                detail.Price += ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;
        //                            }
        //                            else
        //                            {
        //                                detail.Price += (decimal)rowCovered["AmountValue"];
        //                                detail.AutoProcessCalculation = (decimal)rowCovered["AmountValue"];
        //                            }
        //                        }
        //                    }
        //                }

        //                detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //                detail.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

        //                //post
        //                decimal? total = ((detail.ChargeQuantity * detail.Price) - detail.DiscountAmount) + detail.CitoAmount;
        //                decimal? qty = detail.ChargeQuantity;

        //                var calc = new Helper.CostCalculation(grrID, detail.ItemID, total ?? 0, tblCovered, qty ?? 0,
        //                                                      detail.IsCito ?? false,
        //                                                      detail.IsCitoInPercent ?? false,
        //                                                      detail.BasicCitoAmount ?? 0, detail.Price ?? 0,
        //                                                      entity.IsRoomIn ?? false, detail.IsItemRoom ?? false,
        //                                                      entity.TariffDiscountForRoomIn ?? 0, detail.DiscountAmount ?? 0, false);

        //                //CostCalculation
        //                CostCalculation cost = CostCalculations.AddNew();
        //                cost.RegistrationNo = entity.RegistrationNo;
        //                cost.TransactionNo = detail.TransactionNo;
        //                cost.SequenceNo = detail.SequenceNo;
        //                cost.ItemID = detail.ItemID;
        //                cost.PatientAmount = calc.PatientAmount;
        //                cost.GuarantorAmount = calc.GuarantorAmount;
        //                cost.DiscountAmount = detail.DiscountAmount;
        //                cost.IsPackage = detail.IsPackage;
        //                cost.ParentNo = detail.ParentNo;
        //                cost.ParamedicAmount = detail.ChargeQuantity * TransChargesItemComps.Where(comp => comp.TransactionNo == detail.TransactionNo &&
        //                                                                                                   comp.SequenceNo == detail.SequenceNo &&
        //                                                                                                   !string.IsNullOrEmpty(comp.ParamedicID))
        //                                                                                    .Sum(comp => comp.Price - comp.DiscountAmount + comp.CitoAmount);
        //                cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        //                cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //            }
        //        }
        //        else
        //        {
        //            CostCalculations.MarkAllAsDeleted();
        //            CostCalculations.Save();
        //        }

        //        entity.Save();

        //        if (isApproval)
        //        {
        //            // stock calculation
        //            // charges
        //            var chargesBalances = new ItemBalanceCollection();
        //            var chargesDetailBalances = new ItemBalanceDetailCollection();
        //            var chargesMovements = new ItemMovementCollection();

        //            string itemNoStock;
        //            var transChargesItems = TransChargesItems;

        //            ItemBalance.PrepareItemBalances(transChargesItems, unit.ServiceUnitID, unit.LocationID,
        //                AppSession.UserLogin.UserID, isApproval, ref chargesBalances, ref chargesDetailBalances, ref chargesMovements,
        //                out itemNoStock);

        //            if (!string.IsNullOrEmpty(itemNoStock))
        //            {
        //                if (itemNoStock.Length >= 5 && itemNoStock.Substring(0, 5) == "Zero|")
        //                    args.MessageText = "Zero cost price of item : " + itemNoStock.Replace("Zero|", "");
        //                else
        //                    args.MessageText = "Insufficient balance of item : " + itemNoStock;

        //                args.IsCancel = true;
        //                return;
        //            }

        //            transChargesItems.Save();
        //            TransChargesItemComps.Save();
        //            CostCalculations.Save();

        //            if (chargesBalances != null)
        //                chargesBalances.Save();
        //            if (chargesDetailBalances != null)
        //                chargesDetailBalances.Save();
        //            if (chargesMovements != null)
        //                chargesMovements.Save();

        //            // consumption
        //            var consumptionBalances = new ItemBalanceCollection();
        //            var consumptionDetailBalances = new ItemBalanceDetailCollection();
        //            var consumptionMovements = new ItemMovementCollection();

        //            var transChargesItemConsumptions = TransChargesItemConsumptions;

        //            ItemBalance.PrepareItemBalances(transChargesItemConsumptions, unit.ServiceUnitID, unit.LocationID,
        //                AppSession.UserLogin.UserID, ref consumptionBalances, ref consumptionDetailBalances,
        //                ref consumptionMovements, out itemNoStock);

        //            if (!string.IsNullOrEmpty(itemNoStock))
        //            {
        //                if (itemNoStock.Length >= 5 && itemNoStock.Substring(0, 5) == "Zero|")
        //                    args.MessageText = "Zero cost price of item : " + itemNoStock.Replace("Zero|", "");
        //                else
        //                    args.MessageText = "Insufficient balance of item : " + itemNoStock;

        //                args.IsCancel = true;
        //                return;
        //            }

        //            transChargesItemConsumptions.Save();

        //            if (consumptionBalances != null)
        //                consumptionBalances.Save();
        //            if (consumptionDetailBalances != null)
        //                consumptionDetailBalances.Save();
        //            if (consumptionMovements != null)
        //                consumptionMovements.Save();

        //            /* Automatic Journal Testing Start */
        //            if (AppSession.Parameter.IsUsingIntermBill != "Yes")
        //            {
        //                int? journalId = JournalTransactions.AddNewIncomeJournal(entity, TransChargesItemComps,
        //                                                                         reg, unit, CostCalculations,
        //                                                                         "NPC",
        //                                                                         AppSession.UserLogin.UserID, 0);
        //            }

        //            /* Automatic Journal Testing End */

        //            //if (!string.IsNullOrEmpty(entity.PackageReferenceNo))
        //            //{
        //            //    int? x = ParamedicFeeTransChargesItemCompSettled.AddNewSettled(entity, TransChargesItemComps, AppSession.UserLogin.UserID, false);
        //            //}
        //        }
        //        else
        //        {
        //            foreach (TransChargesItem detail in TransChargesItems.Where(t => string.IsNullOrEmpty(t.ParentNo)))
        //            {
        //                var initIb = InitIntermBill(detail.TransactionNo, detail.SequenceNo);
        //                if (!string.IsNullOrEmpty(initIb))
        //                {
        //                    var i = new Item();
        //                    i.LoadByPrimaryKey(detail.ItemID);

        //                    args.MessageText = "Item : " + i.ItemName + " can not be corrected. This item is already proceed to Interm Bill with transaction no: " + initIb;
        //                    args.IsCancel = true;
        //                    return;
        //                }
        //            }
        //        }

        //        //Commit if success, Rollback if failed
        //        trans.Complete();
        //    }
        //}

        private void SetUnApproval(TransCharges entity, bool isApproval, ValidateArgs args)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(entity.RegistrationNo);

            if (reg.IsHoldTransactionEntry ?? false)
            {
                args.MessageText = "Transaction is locked.";
                args.IsCancel = true;
                return;
            }

            using (var trans = new esTransactionScope())
            {
                //header
                entity.IsApproved = isApproval;
                entity.IsBillProceed = isApproval;

                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                CostCalculations.MarkAllAsDeleted();
                CostCalculations.Save();

                entity.Save();

                foreach (TransChargesItem detail in TransChargesItems.Where(t => string.IsNullOrEmpty(t.ParentNo)))
                {
                    var initIb = InitIntermBill(detail.TransactionNo, detail.SequenceNo);
                    if (!string.IsNullOrEmpty(initIb))
                    {
                        var i = new Item();
                        i.LoadByPrimaryKey(detail.ItemID);

                        args.MessageText = "Item : " + i.ItemName + " can not be corrected. This item is already proceed to Interm Bill with transaction no: " + initIb;
                        args.IsCancel = true;
                        return;
                    }
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
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

        private static bool IsServiceUnitOrder(string serviceUnitID)
        {
            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(serviceUnitID);
            return unit.IsUsingJobOrder ?? false;
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

        private void SetVoid(TransCharges entity, bool isVoid)
        {
            //header
            entity.IsVoid = isVoid;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            //detail
            foreach (TransChargesItem item in TransChargesItems)
            {
                item.IsVoid = isVoid;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                TransChargesItems.Save();

                if (!string.IsNullOrEmpty(Request.QueryString["mode"]))
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(entity.RegistrationNo);

                    reg.IsVoid = isVoid;
                    if (reg.IsVoid ?? false)
                    {
                        reg.VoidByUserID = AppSession.UserLogin.UserID;
                        reg.VoidDate = (new DateTime()).NowAtSqlServer();
                        reg.VoidNotes = "TRANSACTION CANCEL";
                    }
                    else
                    {
                        reg.VoidByUserID = null;
                        reg.str.VoidDate = string.Empty;
                        reg.VoidNotes = null;
                    }
                    reg.Save();
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(TransCharges entity) {
            txtTransactionNo.Text = ServiceUnitTransactionDetail.SetEntityValue(entity, (DataModeCurrent == AppEnum.DataMode.New), txtTransactionNo.Text,
                txtRegistrationNo.Text,
                "npc", _autoNumber,
                txtTransactionDate.SelectedDate.Value, "00:00",
                txtTransactionDate.SelectedDate.Value, "00:00",
                string.Empty, false,
                cboServiceUnitID.SelectedValue, cboServiceUnitID.SelectedValue, cboLocationID.SelectedValue,
                string.Empty, AppSession.Parameter.OutPatientClassID, string.Empty, string.Empty,
                false, 0, false, false, string.Empty, string.Empty, string.Empty,
                TransChargesItems, TransChargesItemComps, TransChargesItemConsumptions,
                string.Empty, string.Empty, cboGuarantorID.SelectedValue, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        private void SaveEntity(TransCharges entity)
        {
            using (var trans = new esTransactionScope())
            {
                if (entity.es.IsAdded)
                {
                    //autonumber has been saved on SetEntity
                    //_autoNumber.Save();

                    var reg = new Registration();
                    txtRegistrationNo.Text = GetNewRegistrationNo();
                    reg.RegistrationNo = txtRegistrationNo.Text;
                    reg.SRRegistrationType = AppConstant.RegistrationType.OutPatient;
                    reg.PatientID = cboPatientID.SelectedValue;
                    reg.ClassID = AppSession.Parameter.OutPatientClassID;
                    reg.RegistrationDate = txtTransactionDate.SelectedDate;
                    reg.RegistrationTime = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
                    reg.AgeInYear = Convert.ToByte(txtAgeYear.Value);
                    reg.AgeInMonth = Convert.ToByte(txtAgeMonth.Value);
                    reg.AgeInDay = Convert.ToByte(txtAgeDay.Value);
                    reg.SRShift = Registration.GetShiftID();
                    var dep = new ServiceUnit();
                    dep.LoadByPrimaryKey(cboServiceUnitID.SelectedValue);
                    reg.DepartmentID = dep.DepartmentID;
                    reg.ServiceUnitID = cboServiceUnitID.SelectedValue;
                    reg.ChargeClassID = AppSession.Parameter.OutPatientClassID;
                    reg.CoverageClassID = AppSession.Parameter.OutPatientClassID;
                    reg.GuarantorID = cboGuarantorID.SelectedValue;
                    reg.IsFromDispensary = false;
                    reg.LastCreateDateTime = (new DateTime()).NowAtSqlServer();
                    reg.LastCreateUserID = AppSession.UserLogin.UserID;
                    reg.str.ParamedicID = null;
                    reg.IsNonPatient = true;

                    //Last Update Status
                    reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    reg.Save();

                    var mrg = new MergeBilling();
                    mrg.RegistrationNo = reg.RegistrationNo;
                    mrg.FromRegistrationNo = string.Empty;

                    //Last Update Status
                    mrg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    mrg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    mrg.Save();

                    entity.RegistrationNo = reg.RegistrationNo;

                    _autoNumberReg.Save();

                    entity.RegistrationNo = reg.RegistrationNo;
                }
                else
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(txtRegistrationNo.Text);

                    reg.GuarantorID = cboGuarantorID.SelectedValue;
                    reg.Save();
                }

                entity.Save();

                TransChargesItems.Save();
                TransChargesItemComps.Save();
                TransChargesItemConsumptions.Save();

                foreach (var ext in ExtramuralItems) {
                    if (ext.es.IsAdded) {
                        ext.TransactionNo = entity.TransactionNo;
                    }
                }
                ExtramuralItems.Save();

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
                que.Where
                    (
                        que.TransactionNo > txtTransactionNo.Text,
                        que.IsCorrection == false,
                          que.IsNonPatient == true

                    );
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.TransactionNo < txtTransactionNo.Text,
                        que.IsCorrection == false,
                        que.IsNonPatient == true
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new TransCharges();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, AppEnum.AutoNumber.NonPatientCharges);
            return _autoNumber.LastCompleteNumber;
        }

        private string GetNewRegistrationNo()
        {
            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(cboServiceUnitID.SelectedValue);
            _autoNumberReg = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, BusinessObject.Reference.TransactionCode.Registration,
                unit.DepartmentID);
            return _autoNumberReg.LastCompleteNumber;
        }

        #region Record Detail Method Function TransChargesItem

        private void RefreshCommandItemTransChargesItem(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read) && !IsReturn;
            grdTransChargesItem.Columns[0].Visible = isVisible;
            grdTransChargesItem.Columns[grdTransChargesItem.Columns.Count - 1].Visible = isVisible;


            grdTransChargesItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top :
                GridCommandItemDisplay.None;

            grdExtramural.Columns[0].Visible = isVisible;
            grdExtramural.Columns[grdExtramural.Columns.Count - 1].Visible = isVisible;
            grdExtramural.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top :
                GridCommandItemDisplay.None;

            if (oldVal != AppEnum.DataMode.Read)
            {
                TransChargesItems = null;
                TransChargesItemComps = null;
                TransChargesItemConsumptions = null;
                CostCalculations = null;
                ExtramuralItems = null;
            }

            //Perbaharui tampilan dan data
            if (IsPostBack)
            {
                grdTransChargesItem.Rebind();
                grdExtramural.Rebind();
            }
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

                if (Request.QueryString["type"] == "mcu")
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                else
                    query.Where(query.TransactionNo == txtTransactionNo.Text, query.NotExists(tci));

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

            //nyontek yang diatas
            ExtramuralItems = null; //Reset Record Detail
            grdExtramural.DataSource = ExtramuralItems; //Requery
            grdExtramural.MasterTableView.IsItemInserted = false;
            grdExtramural.MasterTableView.ClearEditItems();
            grdExtramural.DataBind();
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

            var sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                [TransChargesItemMetadata.ColumnNames.SequenceNo]);
            var entity = FindTransChargesItem(sequenceNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdTransChargesItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            var sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]
                [TransChargesItemMetadata.ColumnNames.SequenceNo]);
            var entity = FindTransChargesItem(sequenceNo);
            if (entity != null)
            {
                entity.MarkAsDeleted();
                // Delete juga componennya
                var comps = TransChargesItemComps.Where(x => x.SequenceNo == sequenceNo);
                foreach (var comp in comps) comp.MarkAsDeleted();
            }

            cboGuarantorID.Enabled = !(TransChargesItems.Count > 0);
            cboServiceUnitID.Enabled = !(TransChargesItems.Count > 0);
            cboLocationID.Enabled = !(TransChargesItems.Count > 0);
        }

        protected void grdTransChargesItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = TransChargesItems.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdTransChargesItem.Rebind();
            cboGuarantorID.Enabled = !(TransChargesItems.Count > 0);
            cboServiceUnitID.Enabled = !(TransChargesItems.Count > 0);
            cboLocationID.Enabled = !(TransChargesItems.Count > 0);
        }

        protected void grdTransChargesItem_ItemCreated(object sender, GridItemEventArgs e)
        {
            ServiceUnitTransactionDetail.grdTransChargesItem_OnItemCreated(TransChargesItems, sender, e);
            //if (e.Item is GridDataItem)
            //{
            //    if (TransChargesItems.Count < e.Item.DataSetIndex)
            //    {
            //        var item = TransChargesItems[e.Item.DataSetIndex];
            //        if (item != null)
            //        {
            //            if (item.IsVoid ?? false)
            //            {
            //                for (var i = 0; i < e.Item.Cells.Count; i++)
            //                {
            //                    if (i > 0 && i < e.Item.Cells.Count)
            //                        e.Item.Cells[i].Font.Strikeout = true;

            //                }
            //            }
            //        }
            //    }
            //}
        }

        protected void grdTransChargesItem_ItemCommand(object source, GridCommandEventArgs e)
        {
            ServiceUnitTransactionDetail.grdTransChargesItem_OnItemCommand(TransChargesItems, source, e);
            //if (e.CommandName == "Edit" || e.CommandName == "Delete")
            //{
            //    var item = TransChargesItems[e.Item.DataSetIndex];
            //    if (item != null)
            //    {
            //        if (item.IsBillProceed.Value)
            //            e.Canceled = true;
            //    }
            //}
        }
        private TransChargesItem FindTransChargesItem(String sequenceNo)
        {
            return TransChargesItems.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        private TransChargesItemComp FindTransChargesItemComp(String sequenceNo, String tariffComponentID)
        {
            var coll = TransChargesItemComps;
            return coll.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo) && rec.TariffComponentID.Equals(tariffComponentID));
        }

        private void SetEntityValue(TransChargesItem entity, GridCommandEventArgs e)
        {
            var userControl = (NonPatientCustomerChargesItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                ServiceUnitTransactionDetail.SetEntityDetail(entity, userControl.SequenceNo, userControl.ItemID, userControl.ItemName, userControl.ParamedicID,
                userControl.ParamedicName, userControl.IsAdminCalculation, userControl.IsVariable, userControl.IsCito,
                userControl.ChargeQuantity, userControl.StockQuantity, userControl.SRItemUnit, 0,
                userControl.Price, userControl.DiscountAmount, userControl.SRDiscountReason, userControl.IsAssetUtilization,
                userControl.AssetID, userControl.IsPackage, userControl.IsVoid, userControl.Notes, userControl.CenterID,
                userControl.TariffComponent, userControl.IsNewRecord, userControl.IsItemRoom, string.Empty,
                txtTransactionNo.Text, txtRegistrationNo.Text, AppSession.Parameter.OutPatientClassID, cboServiceUnitID.SelectedValue,
                TransChargesItemComps, txtTransactionDate.SelectedDate.Value,
                TransChargesItems, TransChargesItemConsumptions, cboGuarantorID.SelectedValue, userControl.SRCitoPercentage, string.Empty, string.Empty, userControl.IsItemTypeService, string.Empty, string.Empty);
                
                //SetEntityDetail(entity, userControl.SequenceNo, userControl.ItemID, userControl.ItemName, userControl.ParamedicID,
                //    userControl.ParamedicName, userControl.IsAdminCalculation, userControl.IsVariable, userControl.IsCito,
                //    userControl.ChargeQuantity, userControl.StockQuantity, userControl.SRItemUnit, 0,
                //    userControl.Price, userControl.DiscountAmount, userControl.SRDiscountReason, userControl.IsAssetUtilization,
                //    userControl.AssetID, userControl.IsPackage, userControl.IsVoid, userControl.Notes, userControl.CenterID,
                //    userControl.TariffComponent, userControl.IsNewRecord, userControl.IsItemRoom);
            }
        }

        private void SetEntityDetail(TransChargesItem entity, string sequenceNo, string itemID, string itemName, string paramedicID,
          string paramedicName, bool? isAdminCalculation, bool? isVariable, bool? isCito, decimal? chargeQuantity,
          decimal? stockQuantity, string srItemUnit, decimal? costPrice, decimal? price, decimal? discountAmount,
          string srDiscountReason, bool? isAssetUtilization, string assetID, bool? isPackage, bool? isVoid, string notes, string centerID,
          IEnumerable<TransChargesItemComp> tariffComponents, bool isNewRecord, bool? isItemRoom)
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
            entity.ChargeClassID = AppSession.Parameter.DefaultTariffClass;

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
            grr.LoadByPrimaryKey(cboGuarantorID.SelectedValue);

            if (!(entity.IsCito ?? false))
            {
                entity.CitoAmount = 0;
                entity.IsCitoInPercent = false;
                entity.BasicCitoAmount = 0;
            }
            else
            {
                var tariff = new ItemTariff();
                if (!tariff.Load(GetItemTariffQuery(grr.SRTariffType, AppSession.Parameter.OutPatientClassID, entity.ItemID)))
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
                foreach (var comp in tariffComponents)
                {
                    TransChargesItemComp item;
                    if (isNewRecord)
                    {
                        item = TransChargesItemComps.AddNew();
                        item.TransactionNo = txtTransactionNo.Text;
                        item.SequenceNo = comp.SequenceNo;
                    }
                    else
                        item = FindTransChargesItemComp(comp.SequenceNo, comp.TariffComponentID);
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
                        var tComp = new TariffComponent();
                        if (tComp.LoadByPrimaryKey(item.TariffComponentID))
                        {
                            if (tComp.IsPrintParamedicInSlip ?? false)
                            {
                                var par = new Paramedic();
                                par.LoadByPrimaryKey(item.ParamedicID);
                                if (par.IsPrintInSlip ?? true)
                                {
                                    if (p.Length == 0)
                                        p = par.ParamedicName;
                                    else if (!p.Contains(par.ParamedicName))
                                        p = p + "; " + par.ParamedicName;
                                }
                            }
                        }
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
                        ent.ChargeClassID = reg.ChargeClassID;
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
                                // component
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

                                    var tcp = new ItemPackageTariffComponent();
                                    if (tcp.LoadByPrimaryKey(entity.ItemID, ent.ItemID, tcomp.TariffComponentID))
                                        tcomp.Price = tcp.Price ?? 0;
                                    else
                                        tcomp.Price = comp.Price ?? 0;

                                    tcomp.DiscountAmount = 0;
                                    tcomp.ParamedicID = string.Empty;
                                    tcomp.IsPackage = true;

                                    pricePackage += tcomp.Price ?? 0;
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

                                    //var tariff = new ItemTariff();
                                    //if (!tariff.Load(GetItemTariffQuery(grr.SRTariffType, entity.ChargeClassID, consItem.DetailItemID)))
                                    //{
                                    //    if (!tariff.Load(GetItemTariffQuery(grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, consItem.DetailItemID)))
                                    //    {
                                    //        if (!tariff.Load(GetItemTariffQuery(AppSession.Parameter.DefaultTariffType, entity.ChargeClassID, consItem.DetailItemID)))
                                    //            tariff.Load(GetItemTariffQuery(AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, consItem.DetailItemID));
                                    //    }
                                    //}

                                    var tariff =
                                        (Helper.Tariff.GetItemTariff(DateTime.Now.Date, grr.SRTariffType,
                                                                     reg.ChargeClassID, reg.ChargeClassID,
                                                                     consItem.DetailItemID, reg.GuarantorID, false,
                                                                     reg.SRRegistrationType) ??
                                         Helper.Tariff.GetItemTariff(DateTime.Now.Date, grr.SRTariffType,
                                                                     AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID,
                                                                     consItem.DetailItemID,
                                                                     reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                        (Helper.Tariff.GetItemTariff(DateTime.Now.Date,
                                                                     AppSession.Parameter.DefaultTariffType,
                                                                     reg.ChargeClassID, reg.ChargeClassID,
                                                                     consItem.DetailItemID, reg.GuarantorID, false,
                                                                     reg.SRRegistrationType) ??
                                         Helper.Tariff.GetItemTariff(DateTime.Now.Date,
                                                                     AppSession.Parameter.DefaultTariffType,
                                                                     AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID,
                                                                     consItem.DetailItemID,
                                                                     reg.GuarantorID, false, reg.SRRegistrationType));

                                    
                                    consItem.Price = tariff.Price ?? 0;

                                    switch (i.SRItemType)
                                    {
                                        case ItemType.Medical:
                                            var im = new ItemProductMedic();
                                            im.LoadByPrimaryKey(i.ItemID);
                                            consItem.AveragePrice = im.CostPrice;
                                            consItem.FifoPrice = im.PriceInBaseUnit;
                                            break;
                                        case ItemType.NonMedical:
                                            var inm = new ItemProductNonMedic();
                                            inm.LoadByPrimaryKey(i.ItemID);
                                            consItem.AveragePrice = inm.CostPrice;
                                            consItem.FifoPrice = inm.PriceInBaseUnit;
                                            break;
                                        case ItemType.Kitchen:
                                            var ik = new ItemKitchen();
                                            ik.LoadByPrimaryKey(i.ItemID);
                                            consItem.AveragePrice = ik.CostPrice;
                                            consItem.FifoPrice = ik.PriceInBaseUnit;
                                            break;
                                        default:
                                            consItem.AveragePrice = consItem.Price;
                                            consItem.FifoPrice = consItem.Price;
                                            break;
                                    }

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

                                    //var tariff = new ItemTariff();
                                    //if (!tariff.Load(GetItemTariffQuery(grr.SRTariffType, entity.ChargeClassID, consItem.DetailItemID)))
                                    //{
                                    //    if (!tariff.Load(GetItemTariffQuery(grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, consItem.DetailItemID)))
                                    //    {
                                    //        if (!tariff.Load(GetItemTariffQuery(AppSession.Parameter.DefaultTariffType, entity.ChargeClassID, consItem.DetailItemID)))
                                    //            tariff.Load(GetItemTariffQuery(AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, consItem.DetailItemID));
                                    //    }
                                    //}

                                    var tariff =
                                        (Helper.Tariff.GetItemTariff(DateTime.Now.Date, grr.SRTariffType,
                                                                     reg.ChargeClassID, reg.ChargeClassID,
                                                                     consItem.DetailItemID, reg.GuarantorID, false,
                                                                     reg.SRRegistrationType) ??
                                         Helper.Tariff.GetItemTariff(DateTime.Now.Date, grr.SRTariffType,
                                                                     AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID,
                                                                     consItem.DetailItemID,
                                                                     reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                        (Helper.Tariff.GetItemTariff(DateTime.Now.Date,
                                                                     AppSession.Parameter.DefaultTariffType,
                                                                     reg.ChargeClassID, reg.ChargeClassID,
                                                                     consItem.DetailItemID, reg.GuarantorID, false,
                                                                     reg.SRRegistrationType) ??
                                         Helper.Tariff.GetItemTariff(DateTime.Now.Date,
                                                                     AppSession.Parameter.DefaultTariffType,
                                                                     AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID,
                                                                     consItem.DetailItemID,
                                                                     reg.GuarantorID, false, reg.SRRegistrationType));

                                    consItem.Price = tariff.Price ?? 0;

                                    switch (i.SRItemType)
                                    {
                                        case ItemType.Medical:
                                            var im = new ItemProductMedic();
                                            im.LoadByPrimaryKey(i.ItemID);
                                            consItem.AveragePrice = im.CostPrice;
                                            consItem.FifoPrice = im.PriceInBaseUnit;
                                            break;
                                        case ItemType.NonMedical:
                                            var inm = new ItemProductNonMedic();
                                            inm.LoadByPrimaryKey(i.ItemID);
                                            consItem.AveragePrice = inm.CostPrice;
                                            consItem.FifoPrice = inm.PriceInBaseUnit;
                                            break;
                                        case ItemType.Kitchen:
                                            var ik = new ItemKitchen();
                                            ik.LoadByPrimaryKey(i.ItemID);
                                            consItem.AveragePrice = ik.CostPrice;
                                            consItem.FifoPrice = ik.PriceInBaseUnit;
                                            break;
                                        default:
                                            consItem.AveragePrice = consItem.Price;
                                            consItem.FifoPrice = consItem.Price;
                                            break;
                                    }

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
                        
                        var i = new Item();
                        i.LoadByPrimaryKey(consItem.DetailItemID);
                        consItem.ItemName = i.ItemName;

                        consItem.Qty = chargeQuantity * consEntity.Qty;
                        consItem.QtyRealization = consItem.Qty;
                        consItem.SRItemUnit = consEntity.SRItemUnit;

                        var tariff =
                            (Helper.Tariff.GetItemTariff(DateTime.Now.Date, grr.SRTariffType,
                                                         reg.ChargeClassID, reg.ChargeClassID,
                                                         consItem.DetailItemID, reg.GuarantorID, false,
                                                         reg.SRRegistrationType) ??
                             Helper.Tariff.GetItemTariff(DateTime.Now.Date, grr.SRTariffType,
                                                         AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID,
                                                         consItem.DetailItemID,
                                                         reg.GuarantorID, false, reg.SRRegistrationType)) ??
                            (Helper.Tariff.GetItemTariff(DateTime.Now.Date,
                                                         AppSession.Parameter.DefaultTariffType,
                                                         reg.ChargeClassID, reg.ChargeClassID,
                                                         consItem.DetailItemID, reg.GuarantorID, false,
                                                         reg.SRRegistrationType) ??
                             Helper.Tariff.GetItemTariff(DateTime.Now.Date,
                                                         AppSession.Parameter.DefaultTariffType,
                                                         AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID,
                                                         consItem.DetailItemID,
                                                         reg.GuarantorID, false, reg.SRRegistrationType));

                        consItem.Price = tariff.Price ?? 0;
                        
                        switch (i.SRItemType)
                        {
                            case ItemType.Medical:
                                var im = new ItemProductMedic();
                                im.LoadByPrimaryKey(i.ItemID);
                                consItem.AveragePrice = im.CostPrice;
                                consItem.FifoPrice = im.PriceInBaseUnit;
                                break;
                            case ItemType.NonMedical:
                                var inm = new ItemProductNonMedic();
                                inm.LoadByPrimaryKey(i.ItemID);
                                consItem.AveragePrice = inm.CostPrice;
                                consItem.FifoPrice = inm.PriceInBaseUnit;
                                break;
                            case ItemType.Kitchen:
                                var ik = new ItemKitchen();
                                ik.LoadByPrimaryKey(i.ItemID);
                                consItem.AveragePrice = ik.CostPrice;
                                consItem.FifoPrice = ik.PriceInBaseUnit;
                                break;
                            default:
                                consItem.AveragePrice = consItem.Price;
                                consItem.FifoPrice = consItem.Price;
                                break;
                        }

                        consItem.IsPackage = false;
                    }
                }
            }
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
                    query.StartingDate <= (new DateTime()).NowAtSqlServer()
                );
            query.OrderBy(query.StartingDate.Descending);

            return query;
        }
        #endregion

        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            else
                return "&nbsp;&nbsp;&nbsp;" + itemName.ToString();
        }

        protected override void OnMenuEditClick()
        {
            if (txtTransactionNo.Text == string.Empty)
            {
                OnPopulateEntryControl(new TransCharges());


                txtRegistrationNo.Text = (string)ViewState["regno" + Request.UserHostName];
                txtTransactionDate.SelectedDate = DateTime.Today;
                txtTransactionNo.Text = GetNewTransactionNo();

                if (Request.QueryString["md"] == "new")
                    PopulateRegistrationInformation(txtRegistrationNo.Text);

                ViewState["IsApproved"] = false;
                ViewState["IsVoid"] = false;


                cboServiceUnitID.Enabled = TransChargesItems.Count == 0;
                cboGuarantorID.Enabled = !(TransChargesItems.Count > 0);
                cboLocationID.Enabled = TransChargesItems.Count == 0;
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

                //query.Where(query.TransactionNo == txtTransactionNo.Text);

                if (Request.QueryString["type"] == "mcu")
                    query.Where(query.TransactionNo == txtTransactionNo.Text, query.NotExists(tci));
                else
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

                if (Request.QueryString["type"] == "mcu")
                    query.Where(query.TransactionNo == txtTransactionNo.Text, query.NotExists(tci));
                else
                    query.Where(query.TransactionNo == txtTransactionNo.Text);

                //query.Where(query.TransactionNo == txtTransactionNo.Text);

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
                    object obj = ViewState["collCostCalculation" + Request.UserHostName];
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

        protected void cboPatientID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulatePatientInformation(e.Value);
            PopulateGuarantorFromPatientInformation(e.Value);
        }

        protected void cboPatientID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var dtbPatient = (new PatientCollection()).NonPatientRegisterAble(e.Text, string.Empty, string.Empty, string.Empty, 10);
            cboPatientID.DataSource = dtbPatient;
            cboPatientID.DataBind();
        }

        protected void cboPatientID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PatientName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, e.Value);
                cboLocationID.SelectedIndex = 1;
                txtRegistrationNo.Text = GetNewRegistrationNo();
            }
            else
                txtRegistrationNo.Text = string.Empty;
        }

        protected void cboGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var grr = new GuarantorQuery();
            grr.Where(
                grr.Or(
                    grr.GuarantorID.Like(searchTextContain),
                    grr.GuarantorName.Like(searchTextContain)
                    )
                ).OrderBy(grr.GuarantorName.Ascending);
            cboGuarantorID.DataSource = grr.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void grdExtramural_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdExtramural.DataSource = ExtramuralItems;
        }

        protected void grdExtramural_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ExtramuralItems.AddNew();
            SetEntityValueExtramural(entity, e);

            e.Canceled = true;
            grdExtramural.Rebind();
        }
        protected void grdExtramural_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            var entity = ExtramuralItems[editedItem.ItemIndex];
            if (entity != null)
            {
                SetEntityValueExtramural(entity, e);
                grdExtramural.Rebind();
            }
        }

        protected void grdExtramural_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            var entity = ExtramuralItems[item.ItemIndex];
            if (entity != null)
            {
                entity.MarkAsDeleted();
                grdExtramural.Rebind();
            }
        }

        private void SetEntityValueExtramural(TransChargesExtramuralItems tex, GridCommandEventArgs e)
        {
            var uc = (NonPatientCustomerChargesExtramuralItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (uc != null)
            {
                tex.SRExtramuralItem = uc.SRExtramuralItem;
                tex.SRExtramuralItemName = uc.SRExtramuralItemName;
                tex.Qty = uc.Qty;
                tex.LeasingPeriodInDays = uc.LeasingPeriodInDays;
                tex.IsReturned = false;

                if (tex.es.IsAdded) {
                    tex.CreateDateTime = (new DateTime()).NowAtSqlServer();
                    tex.CreateByUserID = AppSession.UserLogin.UserID;
                }
                if (tex.es.IsModified) {
                    tex.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    tex.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }
                tex.GuarantyAmount = uc.Guaranty;
            }
        }
        private TransChargesExtramuralItemsCollection ExtramuralItems
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTransChargesExtramural" + Request.UserHostName];
                    if (obj != null)
                        return ((TransChargesExtramuralItemsCollection)(obj));
                }

                var coll = new TransChargesExtramuralItemsCollection();
                var ext = new TransChargesExtramuralItemsQuery("ext");
                var stdRef = new AppStandardReferenceItemQuery("stdRef");

                ext.InnerJoin(stdRef).On(stdRef.StandardReferenceID == "ExtramuralItem" &&
                    ext.SRExtramuralItem == stdRef.ItemID)
                    .Select(ext, stdRef.ItemName.As("refToStdRef_ItemName"), stdRef.NumericValue.As("refToStdRef_Guaranty"))
                    .Where(ext.TransactionNo == txtTransactionNo.Text);
                coll.Load(ext);

                Session["collTransChargesExtramural" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collTransChargesExtramural" + Request.UserHostName] = value; }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');

                var entity = ExtramuralItems.FindByPrimaryKey(System.Convert.ToInt64(param[1]));

                switch (param[0])
                {
                    case "return":
                        entity.IsReturned = true;
                        entity.ReturnDate = (new DateTime()).NowAtSqlServer();
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        ExtramuralItems.Save();

                        grdExtramural.Rebind();
                        break;
                }
            }
        }
    }
}
