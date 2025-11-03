using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Configuration;
using Temiang.Avicenna.Module.RADT;
using DevExpress.XtraRichEdit.Layout.Export;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class FinalizeBillingVerification : BasePage
    {
        private AppAutoNumberLast _autoNumberTrans, _autoNumberIntermBill, _autoNumberPayment;
        private int _lengthOfStay;
        private string _healthcareInitial;
        private bool _isUsingIntermBill, _isUsingHumanResourcesModul, _isOperatingRoomResetPrice, _isOperatingRoomResetPriceLastClass, _isOperatingRoomResetPriceHighestClass, _isRADTLinkToHumanResourcesModul;
        private string _guarantorTypeEmployee;
        private string _businessMethodFlavon;// _businessMethodBpjs, _guarantorAskesID;
        private string _guarantorTypeSelf, _selfGuarantor, _defaultTariffClass, _defaultTariffType;
        private string _guarantorRuleTypeDiscount, _guarantorRuleTypeMargin;
        private string _registrationType;
        private bool _isPowerUser;

        #region Custom Unique Session
        private string UniqueID
        {
            get
            {
                return Request.QueryString["regNo"];
            }
        }

        private void SetSession(string StartWith, object value)
        {
            if (value == null)
            {
                RemoveSimilarSessions(StartWith);
            }
            Session[StartWith + UniqueID] = value;
        }

        private void RemoveSimilarSessions(string StartsWith)
        {
            for (int i = 0; i < Session.Count; i++)
            {
                var crntSession = Session.Keys[i];
                if (crntSession.StartsWith(StartsWith) && !crntSession.Equals(StartsWith + UniqueID))
                {
                    // freeing the resources for optimization
                    Session.Remove(crntSession);
                }
            }
        }

        protected string PatientID
        {
            get { return hdnPatientID.Value; }
            set { hdnPatientID.Value = value; }
        }
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Transactions = null;
                TransPrescriptionItems = null;
                TransChargesItems = null;
                IntermBills = null;
                CostCalculations = null;
                Buffers = null;
                RegistrationItemRules = null;
                GuarantorReceipts = null;

                ResetSessionPlafond();
            }

            // Url Search & List
            ProgramID = AppConstant.Program.VerificationFinalizeBilling;

            _healthcareInitial = AppSession.Parameter.HealthcareInitialAppsVersion;
            _isUsingIntermBill = AppSession.Parameter.IsUsingIntermBill;
            _isUsingHumanResourcesModul = AppSession.Parameter.IsUsingHumanResourcesModul;
            _isOperatingRoomResetPrice = AppSession.Parameter.IsOperatingRoomResetPrice;
            _isOperatingRoomResetPriceLastClass = AppSession.Parameter.IsOperatingRoomResetPriceLastClass;
            _isOperatingRoomResetPriceHighestClass = AppSession.Parameter.IsOperatingRoomResetPriceHighestClass;
            _guarantorTypeEmployee = AppSession.Parameter.GuarantorTypeEmployee;
            _isRADTLinkToHumanResourcesModul = AppSession.Parameter.IsRADTLinkToHumanResourcesModul;
            _businessMethodFlavon = AppSession.Parameter.BusinessMethodFlavon;
            _guarantorTypeSelf = AppSession.Parameter.GuarantorTypeSelf;
            _selfGuarantor = AppSession.Parameter.SelfGuarantor;
            _defaultTariffClass = AppSession.Parameter.DefaultTariffClass;
            _defaultTariffType = AppSession.Parameter.DefaultTariffType;
            _guarantorRuleTypeDiscount = AppSession.Parameter.GuarantorRuleTypeDiscount;
            _guarantorRuleTypeMargin = AppSession.Parameter.GuarantorRuleTypeMargin;

            _isPowerUser = this.IsPowerUser || !(AppSession.Parameter.IsHideOpenCloseOnVerificationForUser);

            if (!IsPostBack)
            {
                if (!_isUsingIntermBill)
                {
                    RadTabStrip1.Tabs[1].Visible = false;//tab Interm Bill
                    btnIntermBill.Visible = false;
                    cboFilterByIntermBillStatus.Enabled = false;
                }


                tdLblFilterByCheckedStatusYes.Visible = true;
                tdLblFilterByCheckedStatusNo.Visible = false;
                tdCboFilterByCheckedStatusYes.Visible = true;
                tdCboFilterByCheckedStatusNo.Visible = false;
                tdLblFilterByItemGroupIdYes.Visible = true;
                tdLblFilterByItemGroupIdNo.Visible = false;
                tdCboFilterByItemGroupIdYes.Visible = true;
                tdCboFilterByItemGroupIdNo.Visible = false;

                InitializeCboFilterByItemGroupID();

                trGuarantorHeader.Visible = false;

                pnlEmployeeInfo.Visible = _isUsingHumanResourcesModul;

                pnlProcedureClass.Visible = _isOperatingRoomResetPrice && !_isOperatingRoomResetPriceLastClass && !_isOperatingRoomResetPriceHighestClass;
                cboProcedureClassID.Enabled = AppSession.Parameter.IsAllowEditProcedureChargeClass;

                CollapsePanel1.IsCollapsed = AppSession.Parameter.IsCollapsedPatientInformationOnBilling ? "true" : "false";
                //lbtnFilterTransactionOpen.Visible = AppSession.Parameter.IsCollapsedTransactionFilterOnBilling;
                //lbtnFilterTransactionClose.Visible = !AppSession.Parameter.IsCollapsedTransactionFilterOnBilling;
                pnlTransactionFilter.Visible = !AppSession.Parameter.IsCollapsedTransactionFilterOnBilling;
                btnPatient.Visible = AppSession.Parameter.IsAllowEditPatientFromVerificationBilling;

                RegistrationItemRules = null;

                StandardReference.InitializeIncludeSpace(cboSRBusinessMethod, AppEnum.StandardReference.BusinessMethod);
                StandardReference.InitializeIncludeSpace(cboGuarSRRelationship, AppEnum.StandardReference.Relationship);
                StandardReference.InitializeIncludeSpace(cboSRDiscountReason, AppEnum.StandardReference.DiscountReason);
                StandardReference.InitializeIncludeSpace(cboSRPatientInType, AppEnum.StandardReference.PatientInType);

                InitializeCboFilterByServiceUnitID();

                cboFilterByPaymentStatus.Items.Add(new RadComboBoxItem("--All--", ""));
                cboFilterByPaymentStatus.Items.Add(new RadComboBoxItem("Paid", "1"));
                cboFilterByPaymentStatus.Items.Add(new RadComboBoxItem("Outstanding", "0"));

                cboFilterByIntermBillStatus.Items.Add(new RadComboBoxItem("--All--", ""));
                cboFilterByIntermBillStatus.Items.Add(new RadComboBoxItem("IntermBill", "1"));
                cboFilterByIntermBillStatus.Items.Add(new RadComboBoxItem("Outstanding", "0"));

                cboFilterByItemType.Items.Add(new RadComboBoxItem("--All--", ""));
                cboFilterByItemType.Items.Add(new RadComboBoxItem("All Service Items", "1"));
                cboFilterByItemType.Items.Add(new RadComboBoxItem("All Product Items", "0"));
                cboFilterByItemType.Items.Add(new RadComboBoxItem("Item Service", "01"));
                cboFilterByItemType.Items.Add(new RadComboBoxItem("Item Laboratory", "31"));
                cboFilterByItemType.Items.Add(new RadComboBoxItem("Item Radiology", "41"));
                cboFilterByItemType.Items.Add(new RadComboBoxItem("Item Package", "61"));
                cboFilterByItemType.Items.Add(new RadComboBoxItem("Item Medical", "11"));
                cboFilterByItemType.Items.Add(new RadComboBoxItem("Item Non Medical", "21"));

                cboFilterByCheckedStatus.Items.Add(new RadComboBoxItem("--All--", ""));
                cboFilterByCheckedStatus.Items.Add(new RadComboBoxItem("Already Checked", "1"));
                cboFilterByCheckedStatus.Items.Add(new RadComboBoxItem("Not Checked", "0"));

                PopulateEntryControl();

                var r = new Registration();
                r.LoadByPrimaryKey(Page.Request.QueryString["regNo"]);

                _registrationType = r.SRRegistrationType;

                if (_registrationType == AppConstant.RegistrationType.InPatient && AppSession.Parameter.IsAutoClosedRegOnDischargePermit)
                    _isPowerUser = false;

                var dout = r.DischargeDate != null ? r.DischargeDate.Value.Date : (new DateTime()).NowAtSqlServer().Date;
                var din = r.RegistrationDate.Value.Date;
                _lengthOfStay = Convert.ToInt32((dout - din).TotalDays) + 1;

                if (_lengthOfStay > AppSession.Parameter.MaxLosToDisplayTransactionList)
                {
                    cboFilterByPaymentStatus.SelectedValue = "0";
                    cboFilterByIntermBillStatus.SelectedValue = "0";
                }
                else if (_healthcareInitial == "RSCH" || _healthcareInitial == "RSSA")
                    cboFilterByPaymentStatus.SelectedValue = "0";

                lblAnamnesa.Text = r.Anamnesis;

                if (_healthcareInitial == "RSUI" || _healthcareInitial == "RSPM")
                {
                    var srPC = new AppStandardReferenceItem();
                    if (srPC.LoadByPrimaryKey("PatientCategory", r.SRPatientCategory))
                    {
                        lblPatientCategory.Text = srPC.ItemName;
                    }
                }
                else
                {
                    lblPatientCategory.Visible = false;
                    lblPatientCategoryLbl.Visible = false;
                }

                grdGuarantorReceipt.MasterTableView.Columns[2].Visible = (_healthcareInitial == "RSCH");
                grdGuarantorReceipt.MasterTableView.Columns[3].Visible = (_healthcareInitial == "RSBHP");

                grdGuarantorReceipt.MasterTableView.Columns[4].Visible = (_healthcareInitial != "RSSMCB");
                grdGuarantorReceipt.MasterTableView.Columns[5].Visible = (_healthcareInitial == "RSSMCB") || (_healthcareInitial == "GRHA");
                grdGuarantorReceipt.MasterTableView.Columns[6].Visible = (_healthcareInitial == "RSSMCB") || (_healthcareInitial == "YBRSGKP") || (_healthcareInitial == "RSPP") || (_healthcareInitial == "RSYS");

                btnSurgeryCostPreview.Visible = _healthcareInitial == "YBRSGKP";

                if (_healthcareInitial != "RSSMCB")
                {
                    btnPaymentProcessRSSM.Visible = false;
                }

                if (r.IsNonPatient == true)
                {
                    lbtnNewTransaction.Visible = false;
                    lbtnNewAncillary.Visible = false;
                    lbtnCorrection.Visible = false;

                    lbtnAlertNewTransaction.Visible = true;
                    lbtnAlertNewAncillary.Visible = true;
                    lbtnAlertCorrection.Visible = true;
                }
                else if (r.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                {
                    if (string.IsNullOrEmpty(r.SRDischargeMethod))
                    {
                        var bed = new Bed();
                        if (bed.LoadByPrimaryKey(r.BedID) && bed.IsNeedConfirmation == true && bed.SRBedStatus == AppSession.Parameter.BedStatusPending)
                        {
                            lbtnNewTransaction.Visible = false;
                            lbtnNewAncillary.Visible = false;
                            lbtnCorrection.Visible = false;

                            lbtnAlertNewTransactionCheckinConfirmed.Visible = true;
                            lbtnAlertNewAncillaryCheckinConfirmed.Visible = true;
                            lbtnAlertCorrectionCheckinConfirmed.Visible = true;
                        }
                    }
                }

                if (AppSession.Parameter.IsVerificationBillingAuthorizationActivated)
                {
                    var programId = ProgramID;
                    var userAccess = new UserAccess(AppSession.UserLogin.UserID, programId);
                    if (lbtnNewTransaction.Visible)
                    {
                        programId = AppConstant.Program.ServiceUnitTransactionVerification;
                        userAccess = new UserAccess(AppSession.UserLogin.UserID, programId);
                        if (!userAccess.IsAddAble)
                        {
                            lbtnNewTransaction.Visible = false;
                            lbtnAlertNewTransactionUserAuthorization.Visible = true;
                        }
                    }
                    if (lbtnNewAncillary.Visible)
                    {
                        programId = AppConstant.Program.DiagnosticSupportTransactionVerification;
                        userAccess = new UserAccess(AppSession.UserLogin.UserID, programId);
                        if (!userAccess.IsAddAble)
                        {
                            lbtnNewAncillary.Visible = false;
                            lbtnAlertNewAncillaryUserAuthorization.Visible = true;
                        }
                    }
                    if (lbtnCorrection.Visible)
                    {
                        programId = AppConstant.Program.ServiceUnitTransactionCorrectionVerification;
                        userAccess = new UserAccess(AppSession.UserLogin.UserID, programId);
                        if (!userAccess.IsAddAble)
                        {
                            lbtnCorrection.Visible = false;
                            lbtnAlertCorrectionUserAuthorization.Visible = true;
                        }
                    }

                    programId = AppConstant.Program.PaymentReceive;
                    userAccess = new UserAccess(AppSession.UserLogin.UserID, programId);
                    if (!userAccess.IsAddAble)
                    {
                        btnPaymentProcess.Enabled = false;
                    }

                    if (!this.IsUserAddAble)
                    {
                        btnIntermBill.Enabled = false;
                        btnPaymentReceive.Enabled = false;
                        btnPersonalAr.Enabled = false;

                        lbtnAdmDisc.Visible = false;

                        grdRegistrationItemRule.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                        grdGuarantorReceipt.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                    }

                    if (!this.IsUserEditAble)
                    {
                        btnSaveGuarantor.Enabled = false;
                        btnSaveGuarantor.ImageUrl = "../../../../Images/Toolbar/save16_d.png";

                        btnCheckGrouper.Enabled = false;
                        btnCheckGrouper.ImageUrl = "~/Images/Toolbar/refresh16_d.png";

                        btnCalculateAdmin.Enabled = false;
                        btnCalculateAdmin.ImageUrl = "~/Images/Toolbar/process16_d.png";

                        btnSRDiscountReason.Enabled = false;
                        btnSRDiscountReason.ImageUrl = "../../../../Images/Toolbar/save16_d.png";

                        btnRecalculated.Enabled = false;

                        //verified transaction
                        tdProcessChecked.Visible = false;
                        tdSaveVerified.Visible = false;
                        tdSaveBuffer.Visible = false;
                        tdProcessPatientToGuarantor.Visible = false;
                        tdProcessGuarantorToPatient.Visible = false;

                        grdRegistrationItemRule.Columns[0].Visible = false;

                        tblSaveTariffCompDiscountRule.Visible = false;
                        tblSavePlafondRule.Visible = false;
                    }

                    if (!this.IsUserVoidAble)
                    {
                        grdRegistrationItemRule.Columns[grdRegistrationItemRule.Columns.Count - 1].Visible = false;
                    }
                }
            }
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);

            if (_isUsingHumanResourcesModul)
            {
                RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(cboGuarantorID, cboEmployeeID);
                RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(cboGuarantorID, cboGuarSRRelationship);
                RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(cboGuarantorGroupID, cboEmployeeID);
                RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(cboGuarantorGroupID, cboGuarSRRelationship);
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            RadToolBar tb = (RadToolBar)Common.Helper.FindControlRecursive(Page, "RadToolBar2");

            if (tb != null)
            {
                if (string.IsNullOrEmpty(_registrationType))
                {
                    var r = new Registration();
                    r.LoadByPrimaryKey(Page.Request.QueryString["regNo"]);
                    _registrationType = r.SRRegistrationType;
                    if (_registrationType == AppConstant.RegistrationType.InPatient && AppSession.Parameter.IsAutoClosedRegOnDischargePermit)
                        _isPowerUser = false;
                }

                tb.Items[16].Visible = _isPowerUser;// sembunyikan tombol close open untuk user biasa
                tb.Items[17].Visible = (AppSession.Parameter.IsBillingAdjustEnabled && this.IsUserEditAble);// billing adjust
                tb.Items[18].Visible = (AppSession.Parameter.IsNeedAllowCheckoutConfirmedForDischarge);// allow checkout
                tb.Items[19].Visible = (_registrationType == AppConstant.RegistrationType.InPatient);// discharge permit
                tb.Items[20].Visible = (AppSession.Parameter.IsVisiblePrintBillingPaymentPermit);// payment permit
                tb.Items[21].Visible = (_healthcareInitial == "YBRSGKP");// billing surgical

                switch (_healthcareInitial)
                {
                    case "RSUI":
                    case "RSPM":
                        tb.Items[6].Visible = false; // sementara matiin tombol print detail rawat jalan supaya tidak dipakai untuk rawat inap
                        tb.Items[7].Visible = false;
                        //lbtnPrintPreviewVerified.Visible = false;
                        break;
                    case "YBRSGKP":
                        tb.Items[6].Visible = false; // sementara matiin tombol print detail rawat jalan supaya tidak dipakai untuk rawat inap
                        tb.Items[7].Visible = false;
                        ((RadToolBarDropDown)tb.Items[8]).Buttons[3].Visible = false;
                        ((RadToolBarDropDown)tb.Items[9]).Buttons[2].Visible = false;
                        break;
                    //case "RSTJ":
                    //    tb.Items[6].Visible = false; // sementara matiin tombol print detail rawat jalan supaya tidak dipakai untuk rawat inap
                    //    tb.Items[7].Visible = false;
                    //    ((RadToolBarDropDown)tb.Items[8]).Buttons[3].Visible = false;
                    //    ((RadToolBarDropDown)tb.Items[9]).Buttons[2].Visible = false;
                    //    break;
                    case "RSMM":
                        tb.Items[6].Visible = false; // sementara matiin tombol print detail rawat jalan supaya tidak dipakai untuk rawat inap
                        tb.Items[7].Visible = false;
                        ((RadToolBarDropDown)tb.Items[8]).Buttons[2].Visible = false;
                        ((RadToolBarDropDown)tb.Items[9]).Buttons[2].Visible = false;
                        break;
                    case "RSCH":
                    case "RSBHP":
                        tb.Items[7].Visible = false;
                        break;
                    case "RSSMCB":
                        tb.Items[7].Visible = false;
                        //tb.Items[8].Visible = false;
                        var tb8 = (RadToolBarDropDown)tb.Items[8];
                        tb8.Text = "Print (Detail 2)";
                        tb8.Buttons[0].Text = "Print With Patient Payment";
                        tb8.Buttons[1].Text = "Print Without Patient Payment";
                        tb8.Buttons[2].Visible = false;

                        tb.Items[9].Visible = false;
                        break;
                    default:
                        tb.Items[6].Visible = false; // sementara matiin tombol print detail rawat jalan supaya tidak dipakai untuk rawat inap
                        tb.Items[7].Visible = false;
                        ((RadToolBarDropDown)tb.Items[8]).Buttons[3].Visible = false;
                        ((RadToolBarDropDown)tb.Items[9]).Buttons[2].Visible = false;
                        break;
                }

                ((RadToolBarDropDown)tb.Items[8]).Buttons[4].Visible = ((RadToolBarDropDown)tb.Items[8]).Buttons[0].Visible && AppSession.Parameter.IsUsingBillingSlipInEnglish;
                ((RadToolBarDropDown)tb.Items[8]).Buttons[5].Visible = ((RadToolBarDropDown)tb.Items[8]).Buttons[1].Visible && AppSession.Parameter.IsUsingBillingSlipInEnglish;
                ((RadToolBarDropDown)tb.Items[8]).Buttons[6].Visible = ((RadToolBarDropDown)tb.Items[8]).Buttons[2].Visible && AppSession.Parameter.IsUsingBillingSlipInEnglish;
                ((RadToolBarDropDown)tb.Items[8]).Buttons[7].Visible = _healthcareInitial == "RSTJ";

                ((RadToolBarDropDown)tb.Items[9]).Buttons[3].Visible = ((RadToolBarDropDown)tb.Items[9]).Buttons[0].Visible && AppSession.Parameter.IsUsingBillingSlipInEnglish;
                ((RadToolBarDropDown)tb.Items[9]).Buttons[4].Visible = ((RadToolBarDropDown)tb.Items[9]).Buttons[1].Visible && AppSession.Parameter.IsUsingBillingSlipInEnglish;

                if (AppSession.Parameter.IsVerificationBillingAuthorizationActivated)
                {
                    var programId = ProgramID;
                    switch (_registrationType)
                    {
                        case AppConstant.RegistrationType.InPatient:
                            programId = AppConstant.Program.InPatientCloseOpenRegistration;
                            break;
                        case AppConstant.RegistrationType.OutPatient:
                            programId = AppConstant.Program.OutPatientCloseOpenRegistration;
                            break;
                        case AppConstant.RegistrationType.EmergencyPatient:
                            programId = AppConstant.Program.EmergencyPatientCloseOpenRegistration;
                            break;
                        case AppConstant.RegistrationType.MedicalCheckUp:
                            programId = AppConstant.Program.HealthScreeningCloseOpenRegistration;
                            break;
                    }
                    var userAccess = new UserAccess(AppSession.UserLogin.UserID, programId);
                    if (!userAccess.IsEditAble)
                    {
                        tb.Items[2].Enabled = false; // lock-unlock
                        tb.Items[16].Enabled = false; // close-open
                    }
                }
            }
        }

        private void PopulateEntryControl()
        {
            var regNo = Page.Request.QueryString["regNo"];
            if (string.IsNullOrEmpty(regNo))
                return;

            var registration = new Registration();
            registration.LoadByPrimaryKey(regNo);

            //trPlavonChargeValue.Visible = AppSession.Parameter.GuarantorAskesID.Contains(registration.GuarantorID);
            btnCheckGrouper.Visible = AppSession.Parameter.GuarantorAskesID.Contains(registration.GuarantorID);
            pgEklaim.Visible = AppSession.Parameter.GuarantorAskesID.Contains(registration.GuarantorID);

            txtRegistrationNo.Text = registration.RegistrationNo;

            var patient = new Patient();
            patient.LoadByPrimaryKey(registration.PatientID);

            txtMedicalNo.Text = patient.MedicalNo;
            var std = new AppStandardReferenceItem();
            txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
            txtPatientName.Text = patient.PatientName;
            txtGender.Text = patient.Sex;
            txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));
            txtParamedicID.Text = registration.ParamedicID;
            hdnPatientID.Value = patient.PatientID;
            PopulateParamedicName(false);

            optSexMale.Checked = (patient.Sex == "M");
            optSexMale.Enabled = (patient.Sex == "M");
            optSexFemale.Checked = (patient.Sex == "F");
            optSexFemale.Enabled = (patient.Sex == "F");

            txtAgeYear.Value = (double)registration.AgeInYear;
            txtAgeMonth.Value = (double)registration.AgeInMonth;
            txtAgeDay.Value = (double)registration.AgeInDay;

            txtDepartmentID.Text = registration.DepartmentID;
            PopulateDepartmentName(false);

            txtServiceUnitID.Text = registration.ServiceUnitID;
            PopulateServiceUnitName(false);
            hdnRegType.Value = registration.SRRegistrationType;

            var sr = new ServiceRoom();
            sr.LoadByPrimaryKey(registration.RoomID);
            txtRoomID.Text = sr.RoomName;
            txtBedID.Text = registration.BedID;

            if (!string.IsNullOrEmpty(txtBedID.Text))
            {
                var bed = new Bed();
                if (bed.LoadByPrimaryKey(txtBedID.Text))
                {
                    var c = new Class();
                    if (c.LoadByPrimaryKey(bed.DefaultChargeClassID))
                        txtBedID.Text = txtBedID.Text + " (" + c.ClassName + ")";
                }
            }

            txtChargeClassID.Text = registration.ChargeClassID;

            var cls = new Class();
            cls.LoadByPrimaryKey(registration.ChargeClassID);
            lblChargeClassName.Text = cls.ClassName;

            var query = new GuarantorQuery();
            query.Where(query.GuarantorID == registration.GuarantorID);

            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
            cboGuarantorID.SelectedValue = registration.GuarantorID;

            if (GuarantorBPJS.Contains(registration.GuarantorID))
                hdnBpjsLabel.Value = "bpjs";
            else
                hdnBpjsLabel.Value = "";

            var g = new Guarantor();
            g.LoadByPrimaryKey(registration.GuarantorID);

            query = new GuarantorQuery();
            query.Where(query.GuarantorID == g.GuarantorHeaderID);

            cboGuarantorGroupID.DataSource = query.LoadDataTable();
            cboGuarantorGroupID.DataBind();
            cboGuarantorGroupID.SelectedValue = g.GuarantorHeaderID;

            if (registration.PersonID != null)
            {
                var empq = new PersonalInfoQuery();
                empq.Where(empq.PersonID == registration.PersonID);
                cboEmployeeID.DataSource = empq.LoadDataTable();
                cboEmployeeID.DataBind();
                cboEmployeeID.SelectedValue = registration.PersonID.ToString();
            }

            if (g.SRGuarantorType == _guarantorTypeEmployee)
            {
                var emp = new PersonalInfo();
                if (emp.LoadByPrimaryKey(Convert.ToInt32(registration.PersonID)))
                {
                    cboEmployeeID.Enabled = !_isRADTLinkToHumanResourcesModul;
                    cboGuarSRRelationship.Enabled = !_isRADTLinkToHumanResourcesModul;
                }
                else
                {
                    string searchTextContain = string.Format("%{0}%", cboGuarantorID.SelectedValue);
                    var pars = new AppParameterCollection();
                    pars.Query.Where(pars.Query.ParameterID == "DependentsOfEmployeesGuarantorID",
                                     pars.Query.ParameterValue.Like(searchTextContain));
                    pars.LoadAll();
                    if (pars.Count > 0)
                    {
                        cboEmployeeID.Enabled = true;
                        cboGuarSRRelationship.Enabled = true;
                    }
                    else
                    {
                        cboEmployeeID.Enabled = !_isRADTLinkToHumanResourcesModul;
                        cboGuarSRRelationship.Enabled = !_isRADTLinkToHumanResourcesModul;
                    }
                }
            }

            cboGuarSRRelationship.SelectedValue = registration.SREmployeeRelationship;
            cboSRBusinessMethod.SelectedValue = registration.SRBussinesMethod;
            btnPersonalAr.Enabled = true;
            txtDischargePlanDate.SelectedDate = registration.DischargePlanDate;

            txtPlafonValue.ReadOnly = cboSRBusinessMethod.SelectedValue != _businessMethodFlavon;
            txtPlafonValue.Value = Convert.ToDouble(registration.PlavonAmount);
            CheckPlafondDetail(cboSRBusinessMethod.SelectedValue == _businessMethodFlavon);

            chkIsGlobalFlavon.Enabled = cboSRBusinessMethod.SelectedValue == _businessMethodFlavon;
            chkIsGlobalFlavon.Checked = registration.IsGlobalPlafond ?? false;

            txtAdminValue.Value = _isUsingIntermBill
                                      ? Convert.ToDouble(registration.PatientAdm ?? 0) +
                                        Convert.ToDouble(registration.GuarantorAdm ?? 0)
                                      : (double)(registration.AdministrationAmount ?? 0);

            var cl = new ClassCollection();
            cl.Query.Where(cl.Query.IsActive == true, cl.Query.IsTariffClass == true);
            cl.LoadAll();

            cboProcedureClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var c in cl)
            {
                cboCoverageClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                cboProcedureClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
            }
            cboCoverageClassID.SelectedValue = registration.CoverageClassID;
            cboProcedureClassID.SelectedValue = registration.ProcedureChargeClassID;

            var cl2 = new ClassCollection();
            cl2.Query.Where(cl2.Query.IsActive == true, cl2.Query.IsInPatientClass == true);
            cl2.LoadAll();

            foreach (var c in cl2)
            {
                cboBillToClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
            }
            cboBillToClassID.SelectedValue = registration.CoverageClassID;

            cboCoverageClassID.Enabled = (registration.SRRegistrationType == AppConstant.RegistrationType.InPatient);
            cboBillToClassID.Enabled = (registration.SRRegistrationType == AppConstant.RegistrationType.InPatient);

            var merge = new MergeBilling();
            if (merge.LoadByPrimaryKey(txtRegistrationNo.Text) && !string.IsNullOrEmpty(merge.FromRegistrationNo))
            {
                var rm = new Registration();
                rm.LoadByPrimaryKey(merge.FromRegistrationNo);

                var um = new ServiceUnit();
                um.LoadByPrimaryKey(rm.ServiceUnitID);

                lblHasMergeTo.Text = "This registration has been merged into : " + merge.FromRegistrationNo + " (" + um.ServiceUnitName + ")";
            }
            else
                lblHasMergeTo.Text = string.Empty;

            var mbq = new MergeBillingQuery("a");
            var rmbq = new RegistrationQuery("b");
            mbq.InnerJoin(rmbq).On(rmbq.RegistrationNo == mbq.RegistrationNo);
            mbq.Where(mbq.FromRegistrationNo == txtRegistrationNo.Text, rmbq.GuarantorID != registration.GuarantorID);
            DataTable mbdt = mbq.LoadDataTable();
            if (mbdt.Rows.Count > 0)
            {
                lblNeedRecalculation.Text = "There is a difference between the guarantor from the previous registration, <span style=\"color:Red\" class=\"blinking\"><b>please Recalculate</b></span>";
            }
            else
                lblNeedRecalculation.Text = string.Empty;

            if (AppSession.Parameter.IsPrescriptionSplitBillActived)
            {
                trSplitBillInfo.Visible = true;
                var tpipm = new TransPrescriptionItemQuery("a");
                var tppm = new TransPrescriptionQuery("b");
                var regpm = new RegistrationQuery("c");
                var tpiopm = new TransPaymentItemOrderQuery("d");

                tpipm.InnerJoin(tppm).On(tppm.PrescriptionNo == tpipm.PrescriptionNo && tppm.IsApproval == true && tppm.IsVoid == false && tppm.IsCash == true);
                tpipm.InnerJoin(regpm).On(regpm.RegistrationNo == tppm.RegistrationNo);
                tpipm.LeftJoin(tpiopm).On(tpiopm.TransactionNo == tpipm.PrescriptionNo && tpiopm.SequenceNo == tpipm.SequenceNo &&
                    tpiopm.IsPaymentProceed == true && tpiopm.IsPaymentReturned == false);
                tpipm.Where(regpm.FromRegistrationNo == registration.RegistrationNo, regpm.IsVoid == false, regpm.IsFromDispensary == true,
                    tpiopm.PaymentNo.IsNull());

                DataTable dtbpm = tpipm.LoadDataTable();

                if (dtbpm.Rows.Count > 0)
                {
                    trSplitBillInfo.Visible = true;
                    lblSplitBillInfo.Text = "This patient has outstanding separate prescription bill, <span style=\"color:Red\" class=\"blinking\"><b>click here for more information...</b></span>";
                }
                else
                {
                    tpipm = new TransPrescriptionItemQuery("a");
                    tppm = new TransPrescriptionQuery("b");
                    regpm = new RegistrationQuery("c");

                    tpipm.InnerJoin(tppm).On(tppm.PrescriptionNo == tpipm.PrescriptionNo && tppm.IsApproval == true && tppm.IsVoid == false && tppm.IsSplitBill == true);
                    tpipm.InnerJoin(regpm).On(regpm.RegistrationNo == tppm.RegistrationNo);
                    tpipm.Where(regpm.FromRegistrationNo == registration.RegistrationNo, regpm.IsVoid == false, regpm.IsFromDispensary == true);

                    trSplitBillInfo.Visible = false;

                    dtbpm = tpipm.LoadDataTable();
                    if (dtbpm.Rows.Count > 0)
                    {
                        trSplitBillInfo.Visible = true;
                        lblSplitBillInfo.Text = "This patient has split prescription bill, <span style=\"color:Red\" class=\"blinking\"><b>click here for more information...</b></span>";
                    }
                    else
                        trSplitBillInfo.Visible = false;
                }
            }
            else
                trSplitBillInfo.Visible = false;

            RefreshButtonLock(registration.IsHoldTransactionEntry);
            RefreshButtonClose(registration.IsClosed);
            RefreshButtonCheckout(registration.IsAllowPatientCheckOut);

            pnlInfo.Visible = GetStatusOutstandingTransaction();
            pnlInfo2.Visible = false;
            pnlInfo3.Visible = false;

            CollapsePanel1.Title = txtPatientName.Text +
                                        " [" + txtMedicalNo.Text + "] [" + txtRegistrationNo.Text + "] " +
                                        (optSexMale.Checked ? "M [" : "F [") +
                                        lblParamedicName.Text + "] " +
                                        lblServiceUnitName.Text + ", " +
                                        txtRoomID.Text + ", " +
                                        txtBedID.Text + ", " +
                                        lblChargeClassName.Text;

            //Discount Rule
            var dr = new RegistrationDiscountRule();
            if (dr.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                //Tariff Comp Discount Rule
                txtTariffCompDiscountResep.Value = Convert.ToDouble(dr.ResepPercentage);
                txtTariffCompDiscountGlobalAmount.Value = Convert.ToDouble(dr.DiscountGlobalAmount);
                chkIsTariffCompDiscountGlobalInPercent.Checked = dr.IsDiscountGlobalInPercent ?? false;
                txtTariffCompDiscountItemMedical.Value = Convert.ToDouble(dr.ItemMedicalPercentage);
                txtTariffCompDiscountItemNonMedical.Value = Convert.ToDouble(dr.ItemNonMedicalPercentage);
            }

            //Plafond Rule
            var pr = new RegistrationPlafondRule();
            if (pr.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                txtPlafondRuleAmount.Value = Convert.ToDouble(pr.PlafondAmount);
                chkIsPlafondRuleInPercent.Checked = pr.IsPlafondInPercent ?? false;
                rblIsPlafondRuleToGuarantor.SelectedIndex = pr.IsToGuarantor ?? false ? 0 : 1;
            }
            txtRegistrationDate.SelectedDate = registration.RegistrationDate;
            txtRegistrationTime.Text = registration.RegistrationTime;
            cboSRPatientInType.SelectedValue = registration.SRPatientInType;
            pnlForInpatient.Visible = (registration.SRRegistrationType == AppConstant.RegistrationType.InPatient &
                                            _healthcareInitial != "RSSA");
            if (pnlForInpatient.Visible)
            {
                var x = registration.DischargeDate != null ? registration.DischargeDate.Value.Date : (new DateTime()).NowAtSqlServer().Date;
                var y = registration.RegistrationDate.Value.Date;
                txtLengthOfStay.Value = (x - y).TotalDays == 0 ? 1 : (x - y).TotalDays + 1;
            }
            rblToGuarantor.SelectedIndex = g.SRGuarantorType == _guarantorTypeSelf ? 1 : 0;

            lblRegistrationInfo2.Text = RegistrationInfoSumary.GetDocumentCheckListCountRemains(txtRegistrationNo.Text);
        }

        private void PopulateParamedicName(bool isResetIdIfNotExist)
        {
            if (txtParamedicID.Text == string.Empty)
            {
                lblParamedicName.Text = string.Empty;
                return;
            }
            var entity = new Paramedic();
            if (entity.LoadByPrimaryKey(txtParamedicID.Text))
                lblParamedicName.Text = entity.ParamedicName;
            else
            {
                lblParamedicName.Text = string.Empty;
                if (isResetIdIfNotExist)
                    txtParamedicID.Text = string.Empty;
            }
        }

        private void PopulateDepartmentName(bool isResetIdIfNotExist)
        {
            if (txtDepartmentID.Text == string.Empty)
            {
                lblDepartmentName.Text = string.Empty;
                return;
            }
            var entity = new Department();
            if (entity.LoadByPrimaryKey(txtDepartmentID.Text))
                lblDepartmentName.Text = entity.DepartmentName;
            else
            {
                lblDepartmentName.Text = string.Empty;
                if (isResetIdIfNotExist)
                    txtDepartmentID.Text = string.Empty;
            }
        }

        private void PopulateServiceUnitName(bool isResetIdIfNotExist)
        {
            if (txtServiceUnitID.Text == string.Empty)
            {
                lblServiceUnitName.Text = string.Empty;
                return;
            }
            var entity = new ServiceUnit();
            if (entity.LoadByPrimaryKey(txtServiceUnitID.Text))
                lblServiceUnitName.Text = entity.ServiceUnitName;
            else
            {
                lblServiceUnitName.Text = string.Empty;
                if (isResetIdIfNotExist)
                    txtServiceUnitID.Text = string.Empty;
            }
        }

        private void Refresh()
        {
            Transactions = null;
            TransChargesItems = null;
            TransPrescriptionItems = null;
            grdTransChargesItem.Rebind();

            IntermBills = null;
            grdIntermBill.Rebind();

            CostCalculations = null;
            grdCostCalculation.Rebind();

            Buffers = null;
            grdBuffer.Rebind();

            pnlInfo.Visible = GetStatusOutstandingTransaction();

            GuarantorReceipts = null;
            grdGuarantorReceipt.Rebind();

            pnlInfo2.Visible = false;
            pnlInfo3.Visible = false;

            //db:29-09-2023, plafond 
            PlanfondCalculatation("bpjs");
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(source is RadGrid))
                return;

            pnlInfo2.Visible = false;
            lblInfo2.Text = string.Empty;
            pnlInfo3.Visible = false;
            lblInfo3.Text = string.Empty;

            switch (eventArgument)
            {
                case "rebind":
                    Refresh();

                    break;

                case "lock":
                    Validate();
                    if (!IsValid)
                        return;
                    Lock();
                    if (AppSession.Parameter.IsRefreshBeforeLockVerification)
                    {
                        var r = new Registration();
                        r.LoadByPrimaryKey(txtRegistrationNo.Text);
                        if (r.IsHoldTransactionEntry ?? false)
                        {
                            Refresh();
                        }
                    }
                    break;
                case "closed":
                    Validate();
                    if (!IsValid)
                        return;

                    pnlInfo2.Visible = false;
                    lblInfo2.Text = string.Empty;
                    pnlInfo3.Visible = false;
                    lblInfo3.Text = string.Empty;

                    var discharge = new Registration();
                    discharge.LoadByPrimaryKey(txtRegistrationNo.Text);
                    if (discharge.SRRegistrationType == AppConstant.RegistrationType.InPatient && !discharge.DischargeDate.HasValue)
                    {
                        pnlInfo2.Visible = true;
                        lblInfo2.Text = "Patient have not discharge yet. Registration can't be closed.";
                        pnlInfo3.Visible = false;
                        lblInfo3.Text = string.Empty;
                        return;
                    }

                    Closed(false);
                    break;
                case "process":
                    Validate();
                    if (!IsValid)
                        return;

                    if (string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    {
                        pnlInfo2.Visible = false;
                        lblInfo2.Text = "Guarantor required.";
                        pnlInfo3.Visible = false;
                        lblInfo3.Text = string.Empty;
                        return;
                    }

                    Save();
                    Process();

                    var reg = new Registration();
                    reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                    txtAdminValue.Value = _isUsingIntermBill
                                      ? Convert.ToDouble(reg.PatientAdm ?? 0) +
                                        Convert.ToDouble(reg.GuarantorAdm ?? 0)
                                      : (double)(reg.AdministrationAmount ?? 0);

                    Transactions = null;
                    TransChargesItems = null;
                    TransPrescriptionItems = null;
                    grdTransChargesItem.Rebind();

                    CostCalculations = null;
                    grdCostCalculation.Rebind();

                    break;
                case "save":
                    Validate();
                    if (!IsValid)
                        return;

                    if (string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    {
                        pnlInfo2.Visible = false;
                        lblInfo2.Text = "Guarantor required.";
                        pnlInfo3.Visible = false;
                        lblInfo3.Text = string.Empty;
                        return;
                    }

                    Save();

                    Transactions = null;
                    TransChargesItems = null;
                    TransPrescriptionItems = null;
                    grdTransChargesItem.Rebind();

                    CostCalculations = null;
                    grdCostCalculation.Rebind();

                    break;
                case "printd":
                    Print(AppConstant.Report.BillingIntermStatementPatientDetail, AppSession.Parameter.IsForceUseNoIntermbill);
                    break;
                case "printd2_g":
                    Print(AppConstant.Report.BillingStatementDetail2);
                    break;
                case "printd2_g_en":
                    Print(AppConstant.Report.BillingStatementDetail2EN);
                    break;
                case "printd2_d":
                    Print(AppConstant.Report.BillingStatementDetailByDate);
                    break;
                case "printd2_d_en":
                    Print(AppConstant.Report.BillingStatementDetailByDateEN);
                    break;
                case "printre_g":
                    switch (_healthcareInitial)
                    {
                        case "RSSMCB":
                            Print(AppConstant.Report.BillingStatementDetail2, false, false);
                            break;
                        default:
                            Print(AppConstant.Report.BillingStatementRekap);
                            break;
                    }
                    break;
                case "printre_g_en":
                    switch (_healthcareInitial)
                    {
                        case "RSSMCB":
                            Print(AppConstant.Report.BillingStatementDetail2EN, false, false);
                            break;
                        default:
                            Print(AppConstant.Report.BillingStatementRekapEN);
                            break;
                    }
                    break;
                case "printdLabFarLog_g":
                    Print(AppConstant.Report.BillingStatementLabFarLog);
                    break;
                case "printd2_p":
                    //db:20231028 - billing u/ pasien tetap bisa diprint meskipun tanpa IB
                    //Print(AppConstant.Report.BillingStatementDetail2Pribadi);
                    Print(AppConstant.Report.BillingStatementDetail2Pribadi, AppSession.Parameter.IsForceUseNoIntermbill);
                    break;
                case "printd2_p_en":
                    //Print(AppConstant.Report.BillingStatementDetail2PribadiEN);
                    Print(AppConstant.Report.BillingStatementDetail2PribadiEN, AppSession.Parameter.IsForceUseNoIntermbill);
                    break;
                case "printre_p":
                    //Print(AppConstant.Report.BillingStatementRekapPribadi);
                    Print(AppConstant.Report.BillingStatementRekapPribadi, AppSession.Parameter.IsForceUseNoIntermbill);
                    break;
                case "printre_p_en":
                    //Print(AppConstant.Report.BillingStatementRekapPribadiEN);
                    Print(AppConstant.Report.BillingStatementRekapPribadiEN, AppSession.Parameter.IsForceUseNoIntermbill);
                    break;
                case "printre_p_rkp":
                    switch (_healthcareInitial)
                    {
                        case "RSTJ":
                            Print(AppConstant.Report.BillingStatementRekap2, false, false);
                            break;
                        default:
                            Print(AppConstant.Report.BillingStatementRekap);
                            break;
                    }
                    break;
                case "printBpjs":
                    Print(AppConstant.Report.BillingStatementBpjs, AppSession.Parameter.IsForceUseNoIntermbill);
                    break;
                case "printBpjs2":
                    Print(AppConstant.Report.BillingStatementBpjsWithPrice, AppSession.Parameter.IsForceUseNoIntermbill);
                    break;
                case "printdLabFarLog_p":
                    //Print(AppConstant.Report.BillingStatementLabFarLogPribadi);
                    Print(AppConstant.Report.BillingStatementLabFarLogPribadi, AppSession.Parameter.IsForceUseNoIntermbill);
                    break;
                case "printR":
                    PrintR(AppConstant.Report.RssaBillingPrescription);
                    break;
                case "printOt":
                    Print(AppSession.Parameter.ProgramIdPrintSurgeryBilling);
                    break;
                case "printg":
                    PrintBillingInformation(AppConstant.Report.BillingInformation);
                    break;
                case "payment":
                    IntermBills = null;
                    grdIntermBill.Rebind();

                    Buffers = null;
                    grdBuffer.Rebind();

                    GuarantorReceipts = null;
                    grdGuarantorReceipt.Rebind();

                    break;
                case "paymentPersonalAr":
                    IntermBills = null;
                    grdIntermBill.Rebind();

                    GuarantorReceipts = null;
                    grdGuarantorReceipt.Rebind();

                    break;
                case "voidpayment":
                    GuarantorReceipts = null;
                    grdGuarantorReceipt.Rebind();

                    IntermBills = null;
                    grdIntermBill.Rebind();

                    Buffers = null;
                    grdBuffer.Rebind();

                    break;
                case "checkout":
                    Validate();
                    if (!IsValid)
                        return;

                    pnlInfo2.Visible = false;
                    lblInfo2.Text = string.Empty;
                    pnlInfo3.Visible = false;
                    lblInfo3.Text = string.Empty;

                    var co = new Registration();
                    co.LoadByPrimaryKey(txtRegistrationNo.Text);
                    co.IsAllowPatientCheckOut = !(co.IsAllowPatientCheckOut ?? false);
                    if (co.IsAllowPatientCheckOut ?? false)
                    {
                        co.AllowPatientCheckOutByUserID = AppSession.UserLogin.UserID;
                        co.AllowPatientCheckOutDateTime = (new DateTime()).NowAtSqlServer();
                    }
                    else
                    {
                        co.AllowPatientCheckOutByUserID = null;
                        co.AllowPatientCheckOutDateTime = null;
                    }
                    co.Save();

                    RefreshButtonCheckout(co.IsAllowPatientCheckOut);

                    break;
                case "printpatpermit":
                    if (AppSession.Parameter.IsAutoClosedRegOnDischargePermit)
                        Closed(true);

                    PrintP(AppConstant.Report.PatientDischargePermit);
                    break;
                case "printpaymentpermit":
                    PrintP(AppConstant.Report.BillingPaymentPermit);
                    break;
                case "closeopenfiltertxlist":
                    bool isVisible = pnlTransactionFilter.Visible;
                    pnlTransactionFilter.Visible = !isVisible;


                    break;
            }

            if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');
                bool isValid = true;

                switch (param[0])
                {
                    case "voidib":
                        pnlInfo2.Visible = false;
                        pnlInfo3.Visible = false;

                        var c = new TransPaymentItemIntermBillQuery("a");
                        var tp = new TransPaymentQuery("b");
                        c.InnerJoin(tp).On(c.PaymentNo == tp.PaymentNo & tp.IsVoid == false);
                        c.Where(c.IntermBillNo == param[1], c.IsPaymentReturned == false);
                        DataTable dtb = c.LoadDataTable();
                        if (dtb.Rows.Count == 0)
                        {
                            var c2 = new TransPaymentItemIntermBillGuarantorQuery("a");
                            tp = new TransPaymentQuery("b");
                            c2.InnerJoin(tp).On(c2.PaymentNo == tp.PaymentNo & tp.IsVoid == false);
                            c2.Where(c2.IntermBillNo == param[1], c2.IsPaymentReturned == false);
                            dtb = c2.LoadDataTable();
                            if (dtb.Rows.Count > 0)
                                isValid = false;
                        }
                        else
                            isValid = false;

                        if (isValid)
                        {
                            double admin = 0;
                            var eib = new IntermBill();
                            if (eib.LoadByPrimaryKey(param[1]))
                            {
                                using (var trans = new esTransactionScope())
                                {
                                    eib.IsVoid = true;
                                    eib.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    eib.LastUpdateByUserID = AppSession.UserLogin.UserID;

                                    var coll = new CostCalculationCollection();
                                    coll.Query.Where(coll.Query.IntermBillNo == param[1]);
                                    coll.LoadAll();

                                    string paymentNo = string.Empty;
                                    var ret = new TransPaymentItemIntermBillCollection();
                                    ret.Query.Where(ret.Query.IntermBillNo == param[1], ret.Query.IsPaymentReturned == true);
                                    ret.LoadAll();
                                    if (ret.Count > 0)
                                    {
                                        foreach (var item in ret)
                                        {
                                            paymentNo = item.PaymentNo;
                                        }
                                    }

                                    var colltemp = new CostCalculationIntermBillTempCollection();
                                    var tcicolltemp = new TransChargesItemTempPaymentReturnCollection();
                                    var tciccolltemp = new TransChargesItemCompTempPaymentReturnCollection();
                                    var tpicolltemp = new TransPrescriptionItemTempPaymentReturnCollection();

                                    foreach (var item in coll)
                                    {
                                        item.IntermBillNo = null;

                                        if (!string.IsNullOrEmpty(paymentNo))
                                        {
                                            var temp = colltemp.AddNew();
                                            temp.RegistrationNo = item.RegistrationNo;
                                            temp.TransactionNo = item.TransactionNo;
                                            temp.SequenceNo = item.SequenceNo;
                                            temp.IntermBillNo = param[1];
                                            temp.PaymentNo = paymentNo;

                                            var tci = new TransChargesItem();
                                            if (tci.LoadByPrimaryKey(item.TransactionNo, item.SequenceNo))
                                            {
                                                var tx = tcicolltemp.AddNew();
                                                tx.TransactionNo = item.TransactionNo;
                                                tx.SequenceNo = item.SequenceNo;
                                                tx.IntermBillNo = param[1];
                                                tx.PaymentNo = paymentNo;
                                                tx.Price = tci.Price;
                                                tx.Discount = tci.DiscountAmount;

                                                var tcicColl = new TransChargesItemCompCollection();
                                                tcicColl.Query.Where(
                                                    tcicColl.Query.TransactionNo == item.TransactionNo,
                                                    tcicColl.Query.SequenceNo == item.SequenceNo);
                                                tcicColl.LoadAll();
                                                foreach (var d in tcicColl)
                                                {
                                                    var txc = tciccolltemp.AddNew();
                                                    txc.TransactionNo = item.TransactionNo;
                                                    txc.SequenceNo = item.SequenceNo;
                                                    txc.TariffComponentID = d.TariffComponentID;
                                                    txc.IntermBillNo = param[1];
                                                    txc.PaymentNo = paymentNo;
                                                    txc.Price = d.Price;
                                                    txc.Discount = d.DiscountAmount;
                                                }
                                            }
                                            else
                                            {
                                                var tpi = new TransPrescriptionItem();
                                                if (tpi.LoadByPrimaryKey(item.TransactionNo, item.SequenceNo))
                                                {
                                                    var tx = tpicolltemp.AddNew();
                                                    tx.Prescription = item.TransactionNo;
                                                    tx.SequenceNo = item.SequenceNo;
                                                    tx.IntermBillNo = param[1];
                                                    tx.PaymentNo = paymentNo;
                                                    tx.Price = tpi.Price;
                                                    tx.Discount = tpi.DiscountAmount;
                                                    tx.LineAmount = tpi.LineAmount;
                                                }
                                            }
                                        }
                                    }

                                    eib.Save();
                                    coll.Save();
                                    if (colltemp.Count > 0)
                                        colltemp.Save();
                                    if (tcicolltemp.Count > 0)
                                        tcicolltemp.Save();
                                    if (tciccolltemp.Count > 0)
                                        tciccolltemp.Save();
                                    if (tpicolltemp.Count > 0)
                                        tpicolltemp.Save();

                                    //--- hitung ulang biaya admin
                                    double patAdm = 0;
                                    double guarAdm = 0;
                                    var ibcoll = new IntermBillCollection();
                                    ibcoll.Query.Where(ibcoll.Query.RegistrationNo == txtRegistrationNo.Text,
                                                       ibcoll.Query.IsVoid == false, ibcoll.Query.IntermBillNo != param[1]);
                                    ibcoll.LoadAll();
                                    if (ibcoll.Count > 0)
                                    {
                                        foreach (var ib in ibcoll)
                                        {
                                            var cc = new CostCalculationCollection();
                                            cc.Query.Where(cc.Query.IntermBillNo == ib.IntermBillNo);
                                            cc.LoadAll();
                                            if (cc.Count > 0)
                                            {
                                                patAdm += Convert.ToDouble(ib.AdministrationAmount);
                                                guarAdm += Convert.ToDouble(ib.GuarantorAdministrationAmount);
                                            }
                                        }
                                    }

                                    var reg = new Registration();
                                    reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                                    reg.AdministrationAmount = (Convert.ToDecimal(patAdm) + Convert.ToDecimal(guarAdm));
                                    reg.PatientAdm = Convert.ToDecimal(patAdm);
                                    reg.GuarantorAdm = Convert.ToDecimal(guarAdm);

                                    admin = patAdm + guarAdm;

                                    reg.Save();
                                    trans.Complete();
                                }
                            }

                            IntermBills = null;
                            grdIntermBill.Rebind();

                            CostCalculations = null;
                            grdCostCalculation.Rebind();

                            Transactions = null;
                            TransChargesItems = null;
                            TransPrescriptionItems = null;
                            grdTransChargesItem.Rebind();

                            txtAdminValue.Value = admin;
                        }
                        else
                        {
                            IntermBills = null;
                            grdIntermBill.Rebind();

                            pnlInfo2.Visible = true;
                            lblInfo2.Text = "Interm Bill Patient: " + param[1] + " has been proceed to Payment Process. Data can't be void.";
                            pnlInfo3.Visible = false;
                            lblInfo3.Text = string.Empty;
                        }

                        break;
                    case "voidpy":
                        pnlInfo2.Visible = false;
                        pnlInfo3.Visible = false;

                        var ii = new InvoicesItemQuery("a");
                        var i = new InvoicesQuery("b");
                        ii.InnerJoin(i).On(ii.InvoiceNo == i.InvoiceNo & i.IsVoid == false);
                        ii.Where(ii.PaymentNo == param[1]);
                        DataTable dtbi = ii.LoadDataTable();
                        if (dtbi.Rows.Count > 0)
                            isValid = false;

                        if (isValid)
                        {
                            var etp = new TransPayment();
                            if (etp.LoadByPrimaryKey(param[1]))
                            {
                                using (var trans = new esTransactionScope())
                                {
                                    etp.IsApproved = false;
                                    etp.IsVoid = true;

                                    var collib = new TransPaymentItemIntermBillGuarantorCollection();
                                    collib.Query.Where(collib.Query.PaymentNo == etp.PaymentNo);
                                    collib.LoadAll();

                                    collib.MarkAllAsDeleted();

                                    var collbuffer = new CostCalculationBufferCollection();
                                    collbuffer.Query.Where(collbuffer.Query.PaymentNo == etp.PaymentNo);
                                    collbuffer.LoadAll();

                                    foreach (var item in collbuffer)
                                    {
                                        item.PaymentNo = null;
                                    }

                                    var collac = new AskesCovered2Collection();
                                    collac.Query.Where(collac.Query.PaymentNo == etp.PaymentNo);
                                    collac.LoadAll();

                                    foreach (var item in collac)
                                    {
                                        item.PaymentNo = null;
                                        item.IsPaid = false;
                                    }

                                    var colltpi = new TransPaymentItemCollection();
                                    colltpi.Query.Where(colltpi.Query.PaymentNo == etp.PaymentNo);
                                    colltpi.LoadAll();

                                    var total = colltpi.Sum(detail => detail.Amount ?? 0);

                                    var reg = new Registration();
                                    reg.LoadByPrimaryKey(etp.RegistrationNo);
                                    reg.RemainingAmount += total;

                                    etp.Save();
                                    reg.Save();
                                    collib.Save();
                                    collbuffer.Save();
                                    collac.Save();

                                    var closingperiod = (new DateTime()).NowAtSqlServer();;
                                    var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod);
                                    if (isClosingPeriod)
                                    {
                                        IntermBills = null;
                                        grdIntermBill.Rebind();

                                        pnlInfo2.Visible = true;
                                        lblInfo2.Text = "Financial statements for period: " +
                                                        string.Format("{0:MMMM-yyyy}", closingperiod) +
                                                        " have been closed. Please contact the authorities.";
                                        pnlInfo3.Visible = false;
                                        lblInfo3.Text = string.Empty;
                                    }
                                    else
                                    {
                                        int? journalId = JournalTransactions.AddNewPaymentCorrectionJournal(BusinessObject.JournalType.Payment, etp, reg, colltpi, "TP", AppSession.UserLogin.UserID, 0);

                                        trans.Complete();

                                        GuarantorReceipts = null;
                                        grdGuarantorReceipt.Rebind();

                                        IntermBills = null;
                                        grdIntermBill.Rebind();

                                        Buffers = null;
                                        grdBuffer.Rebind();

                                        Transactions = null;
                                        TransChargesItems = null;
                                        TransPrescriptionItems = null;
                                        grdTransChargesItem.Rebind();
                                    }

                                }
                            }
                        }
                        else
                        {
                            IntermBills = null;
                            grdIntermBill.Rebind();

                            pnlInfo2.Visible = true;
                            lblInfo2.Text = "A/R Receive: " + param[1] + " has been proceed to Invoicing process. Data can't be void.";
                            pnlInfo3.Visible = false;
                            lblInfo3.Text = string.Empty;
                        }

                        break;
                    case "deletebuff":
                        using (var trans = new esTransactionScope())
                        {
                            var collBuff = new CostCalculationBufferCollection();
                            collBuff.Query.Where(collBuff.Query.GuarantorID == param[1] &&
                                                 collBuff.Query.RegistrationNo == param[2] &&
                                                 collBuff.Query.PaymentNo.IsNull());
                            collBuff.LoadAll();
                            collBuff.MarkAllAsDeleted();
                            collBuff.Save();
                            trans.Complete();
                        }

                        Buffers = null;
                        grdBuffer.Rebind();
                        break;
                }
            }
        }

        public string GetStatus(object isOrder, object IsOrderRealization, object IsApprove)
        {
            if (IsApprove.Equals(false))
                return "<img src=\"../../../../Images/Toolbar/post16_d.png\" border=\"0\" />";
            else
            {
                if (isOrder.Equals(false))
                    return "<img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" />";
                else
                {
                    if (IsOrderRealization.Equals(false))
                        return "<img src=\"../../../../Images/Toolbar/post16_d.png\" border=\"0\" />";
                    else
                        return "<img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" />";
                }
            }
        }

        public bool GetStatusCheck(object isOrder, object IsOrderRealization, object IsApprove)
        {
            if (IsApprove.Equals(true))
            {
                if (isOrder.Equals(true))
                    return (bool)IsOrderRealization;
                return true;
            }
            return false;
        }

        public bool GetStatusOutstandingTransaction()
        {
            var query = new TransChargesItemQuery("a");
            var header = new TransChargesQuery("c");
            var pay = new TransPaymentItemOrderQuery("g");

            query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo && header.IsOrder == true &&
                                       query.IsOrderRealization == false && query.IsBillProceed == false);
            query.InnerJoin(pay).On(
                query.TransactionNo == pay.TransactionNo &&
                query.SequenceNo == pay.SequenceNo &&
                pay.IsPaymentProceed == true &&
                pay.IsPaymentReturned == false
                );
            query.Where(
                    header.RegistrationNo.In(MergeRegistrationList()),
                    header.IsVoid == false,
                    query.IsVoid == false,
                    query.Or(query.ParentNo == string.Empty, query.ParentNo.IsNull())
                );

            DataTable tbl = query.LoadDataTable();

            return tbl.Rows.Count > 0;
        }

        private string GetNewTransactionNo()
        {
            _autoNumberTrans = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.TransactionNo);
            return _autoNumberTrans.LastCompleteNumber;
        }

        private string[] MergeRegistrationList()
        {
            if (ViewState["BillingVerification:MergeRegistration" + Request.UserHostName] == null)
                ViewState["BillingVerification:MergeRegistration" + Request.UserHostName] = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);

            return (string[])ViewState["BillingVerification:MergeRegistration" + Request.UserHostName];
        }

        private string[] MergeFullRegistrationList()
        {
            if (ViewState["BillingVerification:MergeFullRegistration" + Request.UserHostName] == null)
                ViewState["BillingVerification:MergeFullRegistration" + Request.UserHostName] = Helper.MergeBilling.GetFullMergeRegistration(txtRegistrationNo.Text);

            return (string[])ViewState["BillingVerification:MergeFullRegistration" + Request.UserHostName];
        }

        private string[] IntermBillList()
        {
            int i = grdIntermBill.MasterTableView.Items.Cast<GridDataItem>().Count(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked);
            var arr = new string[i];

            var idx = 0;
            foreach (GridDataItem dataItem in grdIntermBill.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked))
            {
                arr.SetValue(dataItem.GetDataKeyValue("IntermBillNo"), idx);
                idx++;
            }

            return arr;
        }

        protected void btnCheckGrouper_Click(object sender, EventArgs e)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

            var patient = new Patient();
            patient.LoadByPrimaryKey(reg.PatientID);

            if (string.IsNullOrEmpty(reg.GuarantorCardNo))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid", "alert('Guarantor Card No is required.');openWinGuarantorDetail();", true);
                return;
            }
            if (string.IsNullOrEmpty(reg.BpjsSepNo))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid", "alert('SEP No is required.');openWinGuarantorDetail();", true);
                return;
            }
            //var svc = new Common.Inacbg.v50.Service();
            //var grouper = svc.GetGrouper(new Common.Inacbg.v50.Grouper.Data { nomor_sep = reg.BpjsSepNo });
            //if (!grouper.Metadata.IsValid)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "invalid", string.Format("alert('{0} : {1}');", grouper.Metadata.Code, grouper.Metadata.Message), true);
            //    return;
            //}
            //var response = grouper.Response;
            //var cbg = response.Cbg;

            //txtPlafonValue.Value = Convert.ToDouble(cbg.Tariff);

            //reg.PlavonAmount = Convert.ToDecimal(cbg.Tariff);

            //var bpjs = new BpjsSEP();
            //if (bpjs.LoadByPrimaryKey(reg.BpjsSepNo))
            //{
            //    bpjs.KodeCBG = cbg.Code;
            //    bpjs.TariffCBG = Convert.ToDecimal(cbg.Tariff);
            //    bpjs.DeskripsiCBG = cbg.Description;
            //}
            //else
            //{
            //    bpjs = new BpjsSEP()
            //    {
            //        NoSEP = reg.BpjsSepNo,
            //        NomorKartu = reg.GuarantorCardNo,
            //        TanggalSEP = reg.RegistrationDate,
            //        PPKPelayanan = ConfigurationManager.AppSettings["BPJSHospitalID"],
            //        JenisPelayanan = string.Empty,
            //        PoliTujuan = string.Empty,
            //        KelasRawat = string.Empty,
            //        LakaLantas = string.Empty,
            //        User = AppSession.UserLogin.UserID,
            //        NamaPasien = patient.PatientName,
            //        Nik = patient.Ssn,
            //        JenisKelamin = string.Empty,
            //        TanggalLahir = patient.DateOfBirth,
            //        KodeCBG = cbg.Code,
            //        TariffCBG = Convert.ToDecimal(cbg.Tariff),
            //        DeskripsiCBG = cbg.Description
            //    };
            //}

            //var coll = new BpjsCMGCollection();
            //coll.Query.Where(coll.Query.NoSEP == reg.BpjsSepNo);
            //if (!coll.Query.Load()) coll = new BpjsCMGCollection();

            //var cmg = response.SpecialCmg;
            //if (cmg != null)
            //{
            //    foreach (var entity in cmg)
            //    {
            //        var spc = coll.FindByPrimaryKey(reg.BpjsSepNo, entity.Code);
            //        if (spc == null)
            //        {
            //            spc = coll.AddNew();
            //            spc.NoSEP = reg.BpjsSepNo;
            //            spc.KodeCMG = entity.Code;
            //        }
            //        spc.TariffCMG = Convert.ToDecimal(entity.Tariff);
            //        spc.DeskripsiCMG = entity.Description;
            //        spc.TipeCMG = entity.Type;
            //        spc.IsOptionCMG = false;
            //    }
            //}

            //var opt = grouper.SpecialCmgOption;
            //if (opt != null)
            //{
            //    foreach (var entity in opt)
            //    {
            //        var spc = coll.FindByPrimaryKey(reg.BpjsSepNo, entity.Code);
            //        if (spc == null)
            //        {
            //            spc = coll.AddNew();
            //            spc.NoSEP = reg.BpjsSepNo;
            //            spc.KodeCMG = entity.Code;
            //        }
            //        spc.TariffCMG = 0;
            //        spc.DeskripsiCMG = entity.Description;
            //        spc.TipeCMG = entity.Type;
            //        spc.IsOptionCMG = true;
            //    }
            //}

            var hist = new RegistrationPlafondHistory()
            {
                RegistrationNo = reg.RegistrationNo,
                GuarantorID = reg.GuarantorID,
                PlafondAmount = reg.PlavonAmount,
                LastUpdateDateTime = (new DateTime()).NowAtSqlServer(),
                LastUpdateByUserID = AppSession.UserLogin.UserID

            };

            using (var trans = new esTransactionScope())
            {
                reg.Save();
                //bpjs.Save();
                //if (coll.Any()) coll.Save();
                hist.Save();

                trans.Complete();
            }
        }

        #region RadToolBar


        private void Print(string reportName)
        {
            Print(reportName, false, true);
        }
        private void Print(string reportName, bool forceUseNoIntermbill)
        {
            Print(reportName, forceUseNoIntermbill, true);
        }

        private void Print(string reportName, bool forceUseNoIntermbill, bool ShowPatientPaid)
        {
            var jobParameters = new PrintJobParameterCollection();

            var jobParameter = jobParameters.AddNew();
            jobParameter.Name = "IntermBillNoList";
            jobParameter.ValueString = string.Empty;

            if (_healthcareInitial == "RSMM" || _healthcareInitial == "YBRSGKP")
            {
                string[] intermBillNoList = IntermBillList();
                foreach (var str in intermBillNoList)
                {
                    jobParameter.ValueString += str + ",";
                }

                if (jobParameter.ValueString != string.Empty)
                    jobParameter.ValueString = jobParameter.ValueString.Substring(0, jobParameter.ValueString.Length - 1);
            }
            else
            {
                if (_isUsingIntermBill && !forceUseNoIntermbill)
                {
                    string[] intermBillNoList = IntermBillList();
                    foreach (var str in intermBillNoList)
                    {
                        jobParameter.ValueString += str + ",";
                    }

                    if (jobParameter.ValueString == string.Empty)
                        return;

                    jobParameter.ValueString = jobParameter.ValueString.Substring(0, jobParameter.ValueString.Length - 1);
                }
                else
                { 
                    //db:20231028 - ditambah kondisi seperti RSI
                    string[] intermBillNoList = IntermBillList();
                    foreach (var str in intermBillNoList)
                    {
                        jobParameter.ValueString += str + ",";
                    }

                    if (jobParameter.ValueString != string.Empty)
                        jobParameter.ValueString = jobParameter.ValueString.Substring(0, jobParameter.ValueString.Length - 1);
                }
            }

            string[] registrationNoList = MergeRegistrationList();
            var jobParameter2 = jobParameters.AddNew();
            jobParameter2.Name = "RegistrationNoList";
            jobParameter2.ValueString = string.Empty;

            foreach (var str in registrationNoList)
            {
                jobParameter2.ValueString += str + ",";
            }

            jobParameter2.ValueString = jobParameter2.ValueString.Substring(0, jobParameter2.ValueString.Length - 1);

            var parRegNo = jobParameters.AddNew();
            parRegNo.Name = "RegNo";
            parRegNo.ValueString = txtRegistrationNo.Text;

            var parUserID = jobParameters.AddNew();
            parUserID.Name = "UserID";
            parUserID.ValueString = AppSession.UserLogin.UserID;

            var parUser = jobParameters.AddNew();
            parUser.Name = "UserName";
            parUser.ValueString = AppSession.UserLogin.UserName;

            var parplafond = jobParameters.AddNew();
            parplafond.Name = "plafond";
            parplafond.ValueString = txtPlafonValue.Text;

            var parDate1 = jobParameters.AddNew();
            parDate1.Name = "StartDate";
            parDate1.ValueDateTime = txtTransDate1.SelectedDate ?? Convert.ToDateTime("1900-01-01 00:00:00");

            var parDate2 = jobParameters.AddNew();
            parDate2.Name = "EndDate";
            parDate2.ValueDateTime = txtTransDate2.SelectedDate ?? (new DateTime()).NowAtSqlServer().AddDays(10);

            var parSelfGuarantor = jobParameters.AddNew();
            parSelfGuarantor.Name = "SelfGuarantor";
            parSelfGuarantor.ValueString = _selfGuarantor;

            var parAksesGuarantor = jobParameters.AddNew();
            parAksesGuarantor.Name = "AskesGuarantor";
            parAksesGuarantor.ValueString = string.Empty;// _guarantorAskesID;

            if (_healthcareInitial == "RSSMCB")
            {
                var parShowPatientPaid = jobParameters.AddNew();
                parShowPatientPaid.Name = "ShowPatientPaid";
                parShowPatientPaid.ValueNumeric = ShowPatientPaid ? 1 : 0;// _guarantorAskesID;
            }

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = reportName;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                            "oWnd.Show();" +
                            "oWnd.Maximize();";
            RadAjaxPanel1.ResponseScripts.Add(script);

            if (AppSession.Parameter.IsUsedPrintSlipLogForBillingStatement)
                PrintSlipLog.InsertUpdate(reportName, "IntermBillNoList", jobParameter.ValueString, AppSession.UserLogin.UserID);
        }

        private void PrintP(string reportName)
        {
            var jobParameters = new PrintJobParameterCollection();

            var parRegistrationNo = jobParameters.AddNew();
            parRegistrationNo.Name = "p_RegistrationNo";
            parRegistrationNo.ValueString = txtRegistrationNo.Text;

            var parUser = jobParameters.AddNew();
            parUser.Name = "UserName";
            parUser.ValueString = AppSession.UserLogin.UserName;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = reportName;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                            "oWnd.Show();" +
                            "oWnd.Maximize();";
            RadAjaxPanel1.ResponseScripts.Add(script);

            if (AppSession.Parameter.IsUsedPrintSlipLogForBillingStatement)
                PrintSlipLog.InsertUpdate(reportName, "p_RegistrationNo", txtRegistrationNo.Text, AppSession.UserLogin.UserID);
        }

        private void PrintR(string reportName)
        {
            string[] intermBillNoList = IntermBillList();
            if (intermBillNoList.Count() == 0)
            {
                RadAjaxPanel1.ResponseScripts.Add("alert('Please create Interm Bill first');");
                return;
            }

            var jobParameters = new PrintJobParameterCollection();

            var jobParameter = jobParameters.AddNew();
            jobParameter.Name = "IntermBillNoList";
            jobParameter.ValueString = string.Empty;

            foreach (var str in intermBillNoList)
            {
                jobParameter.ValueString += str + ",";
            }

            jobParameter.ValueString = jobParameter.ValueString.Substring(0, jobParameter.ValueString.Length - 1);

            string[] registrationNoList = MergeRegistrationList();
            var jobParameter2 = jobParameters.AddNew();
            jobParameter2.Name = "RegistrationNoList";
            jobParameter2.ValueString = string.Empty;

            foreach (var str in registrationNoList)
            {
                jobParameter2.ValueString += str + ",";
            }

            jobParameter2.ValueString = jobParameter2.ValueString.Substring(0, jobParameter2.ValueString.Length - 1);

            var parRegNo = jobParameters.AddNew();
            parRegNo.Name = "RegNo";
            parRegNo.ValueString = txtRegistrationNo.Text;

            var parUserID = jobParameters.AddNew();
            parUserID.Name = "UserID";
            parUserID.ValueString = AppSession.UserLogin.UserID;

            var parUser = jobParameters.AddNew();
            parUser.Name = "UserName";
            parUser.ValueString = AppSession.UserLogin.UserName;

            var parplafond = jobParameters.AddNew();
            var oreg = new Registration();
            parplafond.Name = "plafond";
            oreg.LoadByPrimaryKey(txtRegistrationNo.Text);
            parplafond.ValueString = Convert.ToString(oreg.PlavonAmount);

            var parDate1 = jobParameters.AddNew();
            parDate1.Name = "StartDate";
            parDate1.ValueDateTime = Convert.ToDateTime("1900-01-01 00:00:00");

            var parDate2 = jobParameters.AddNew();
            parDate2.Name = "EndDate";
            parDate2.ValueDateTime = (new DateTime()).NowAtSqlServer().AddDays(10);

            var parSelfGuarantor = jobParameters.AddNew();
            parSelfGuarantor.Name = "SelfGuarantor";
            parSelfGuarantor.ValueString = AppSession.Parameter.SelfGuarantor;

            var parAksesGuarantor = jobParameters.AddNew();
            parAksesGuarantor.Name = "AskesGuarantor";
            parAksesGuarantor.ValueString = string.Empty;// AppSession.Parameter.GuarantorAskesID;
            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = reportName;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                            "oWnd.Show();" +
                            "oWnd.Maximize();";
            RadAjaxPanel1.ResponseScripts.Add(script);

            if (AppSession.Parameter.IsUsedPrintSlipLogForBillingStatement)
                PrintSlipLog.InsertUpdate(reportName, "IntermBillNoList", jobParameter.ValueString, AppSession.UserLogin.UserID);
        }

        private void PrintBillingInformation(string reportName)
        {
            string[] registrationNoList = MergeRegistrationList();

            var jobParameters = new PrintJobParameterCollection();

            var parRegNo = jobParameters.AddNew();
            parRegNo.Name = "RegNo";
            parRegNo.ValueString = txtRegistrationNo.Text;

            var jobParameter = jobParameters.AddNew();
            jobParameter.Name = "RegistrationNoList";
            jobParameter.ValueString = "";
            foreach (var str in registrationNoList)
            {
                jobParameter.ValueString += str + ",";
            }
            jobParameter.ValueString = jobParameter.ValueString.Substring(0, jobParameter.ValueString.Length - 1);

            jobParameter = jobParameters.AddNew();
            jobParameter.Name = "DownPayment";
            jobParameter.ValueNumeric = Helper.Payment.GetTotalDownPayment(registrationNoList);

            var paymentType = new string[3];
            paymentType.SetValue(AppSession.Parameter.PaymentTypePayment, 0);
            paymentType.SetValue(AppSession.Parameter.PaymentTypeCorporateAR, 1);
            paymentType.SetValue(AppSession.Parameter.PaymentTypePersonalAR, 2);
            jobParameter = jobParameters.AddNew();
            jobParameter.Name = "PaymentAmount";
            jobParameter.ValueNumeric = Helper.Payment.GetTotalPayment(registrationNoList, paymentType);

            var jobParameterUser = jobParameters.AddNew();
            jobParameterUser.Name = "UserName";
            jobParameterUser.ValueString = AppSession.UserLogin.UserName;

            var jobParameterUserId = jobParameters.AddNew();
            jobParameterUserId.Name = "UserID";
            jobParameterUserId.ValueString = AppSession.UserLogin.UserID;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = reportName;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
            "oWnd.Show();" +
            "oWnd.Maximize();";
            RadAjaxPanel1.ResponseScripts.Add(script);

            if (AppSession.Parameter.IsUsedPrintSlipLogForBillingStatement)
                PrintSlipLog.InsertUpdate(reportName, "RegNo", txtRegistrationNo.Text, AppSession.UserLogin.UserID);
        }

        private void Process()
        {
            Transactions = null;
            TransChargesItems = null;
            TransPrescriptionItems = null;
            grdTransChargesItem.Rebind();

            CostCalculations = null;
            grdCostCalculation.Rebind();

            //validating data
            bool select = false;
            int index = 0;
            foreach (GridDataItem dataItem in grdTransChargesItem.MasterTableView.Items)
            {
                if (!(dataItem.FindControl("detailChkbox") as CheckBox).Checked)
                {
                    CostCalculation entity = CostCalculations.FindByPrimaryKey(dataItem["RegistrationNo"].Text,
                        dataItem["TransactionNo"].Text, dataItem["SequenceNo"].Text);
                    if (entity != null)
                        entity.MarkAsDeleted();
                }
                else
                {
                    index++;
                    select = true;
                }
            }

            if ((!select) && (index == 0))
                return;

            using (var trans = new esTransactionScope())
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                string coverageClassId = reg.CoverageClassID;

                #region history
                var recalHistory = new RecalculationProcessHistory();
                var costHistory = new CostCalculationHistoryCollection();
                var chargesItemHistory = new TransChargesItemHistoryCollection();
                var prescriptions = new TransPrescriptionCollection();
                var charges = new TransChargesCollection();
                var chargesItemCompHistory = new TransChargesItemCompHistoryCollection();


                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsAutoJournalFinalizeBilling) == "Yes")
                {
                    if (AppParameter.IsNo(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                    {
                        //save as history
                        var autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.RecalculationProcess);

                        //process header
                        recalHistory = new RecalculationProcessHistory
                        {
                            RecalculationProcessNo = autoNumber.LastCompleteNumber,
                            RecalculationProcessDate = (new DateTime()).NowAtSqlServer(),
                            RegistrationNo = reg.RegistrationNo,
                            FromGuarantorID = reg.GuarantorID,
                            ToGuarantorID = cboGuarantorID.SelectedValue,
                            LastUpdateDateTime = (new DateTime()).NowAtSqlServer(),
                            LastUpdateByUserID = AppSession.UserLogin.UserID
                        };

                        //cost calculation

                        foreach (var cc in CostCalculations)
                        {
                            var cost = costHistory.AddNew();
                            cost.RecalculationProcessNo = recalHistory.RecalculationProcessNo;
                            cost.RegistrationNo = cc.RegistrationNo;
                            cost.TransactionNo = cc.TransactionNo;
                            cost.SequenceNo = cc.SequenceNo;
                            cost.ItemID = cc.ItemID;
                            cost.PatientAmount = cc.PatientAmount;
                            cost.GuarantorAmount = cc.GuarantorAmount;
                            cost.DiscountAmount = cc.DiscountAmount;
                            cost.ParamedicAmount = cc.ParamedicAmount;
                            cost.LastUpdateDateTime = cc.LastUpdateDateTime;
                            cost.LastUpdateByUserID = cc.LastUpdateByUserID;
                            cost.ParamedicFeeAmount = cc.ParamedicFeeAmount;
                            cost.ParamedicFeePaymentNo = cc.ParamedicFeePaymentNo;
                            cost.IsPackage = cc.IsPackage;
                            cost.ParentNo = cc.ParentNo;
                        }

                        //transcharges
                        var chargesHistory = new TransChargesHistoryCollection();

                        //var charges = new TransChargesCollection();
                        charges.Query.Where(charges.Query.TransactionNo.In(costHistory.Select(c => c.TransactionNo)));
                        charges.LoadAll();

                        foreach (var ch in charges)
                        {
                            var tch = chargesHistory.AddNew();
                            tch.RecalculationProcessNo = recalHistory.RecalculationProcessNo;
                            tch.TransactionNo = ch.TransactionNo;
                            tch.RegistrationNo = ch.RegistrationNo;
                            tch.TransactionDate = ch.TransactionDate;
                            tch.ExecutionDate = ch.ExecutionDate;
                            tch.ReferenceNo = ch.ReferenceNo;
                            tch.FromServiceUnitID = ch.FromServiceUnitID;
                            tch.ToServiceUnitID = ch.ToServiceUnitID;
                            tch.ClassID = ch.ClassID;
                            tch.RoomID = ch.RoomID;
                            tch.BedID = ch.BedID;
                            tch.DueDate = ch.DueDate;
                            tch.SRShift = ch.SRShift;
                            tch.SRItemType = ch.SRItemType;
                            tch.IsProceed = ch.IsProceed;
                            tch.IsApproved = ch.IsApproved;
                            tch.IsVoid = ch.IsVoid;
                            tch.IsOrder = ch.IsOrder;
                            tch.IsCorrection = ch.IsCorrection;
                            tch.IsClusterAssign = ch.IsClusterAssign;
                            tch.IsAutoBillTransaction = ch.IsAutoBillTransaction;
                            tch.IsBillProceed = ch.IsBillProceed;
                            tch.Notes = ch.Notes;
                            tch.LastUpdateDateTime = ch.LastUpdateDateTime;
                            tch.LastUpdateByUserID = ch.LastUpdateByUserID;
                            tch.SRTypeResult = ch.SRTypeResult;
                            tch.ResponUnitID = ch.ResponUnitID;
                        }

                        string transSeq = costHistory.Select(c => c.TransactionNo + c.SequenceNo).Aggregate(string.Empty, (current, transNo) => current + (transNo + "','"));

                        //transchargesitem
                        // var chargesItemHistory = new TransChargesItemHistoryCollection();

                        var chargesItems = new TransChargesItemCollection();
                        chargesItems.Query.Where(string.Format("<TransactionNo + SequenceNo IN ('{0}')>", transSeq));
                        chargesItems.LoadAll();

                        foreach (var ci in chargesItems)
                        {
                            var tci = chargesItemHistory.AddNew();
                            tci.RecalculationProcessNo = recalHistory.RecalculationProcessNo;
                            tci.TransactionNo = ci.TransactionNo;
                            tci.SequenceNo = ci.SequenceNo;
                            tci.ReferenceNo = ci.ReferenceNo;
                            tci.ReferenceSequenceNo = ci.ReferenceSequenceNo;
                            tci.ItemID = ci.ItemID;
                            tci.ChargeClassID = ci.ChargeClassID;
                            tci.ParamedicID = ci.ParamedicID;
                            tci.SecondParamedicID = ci.SecondParamedicID;
                            tci.IsAdminCalculation = ci.IsAdminCalculation;
                            tci.IsVariable = ci.IsVariable;
                            tci.IsCito = ci.IsCito;
                            tci.ChargeQuantity = ci.ChargeQuantity;
                            tci.StockQuantity = ci.StockQuantity;
                            tci.SRItemUnit = ci.SRItemUnit;
                            tci.CostPrice = ci.CostPrice;
                            tci.Price = ci.Price;
                            tci.DiscountAmount = ci.DiscountAmount;
                            tci.CitoAmount = ci.CitoAmount;
                            tci.RoundingAmount = ci.RoundingAmount;
                            tci.SRDiscountReason = ci.SRDiscountReason;
                            tci.IsAssetUtilization = ci.IsAssetUtilization;
                            tci.AssetID = ci.AssetID;
                            tci.IsBillProceed = ci.IsBillProceed;
                            tci.IsOrderRealization = ci.IsOrderRealization;
                            tci.IsPackage = ci.IsPackage;
                            tci.IsApprove = ci.IsApprove;
                            tci.IsVoid = ci.IsVoid;
                            tci.Notes = ci.Notes;
                            tci.FilmNo = ci.FilmNo;
                            tci.LastUpdateDateTime = ci.LastUpdateDateTime;
                            tci.LastUpdateByUserID = ci.LastUpdateByUserID;
                            tci.ParentNo = ci.ParentNo;
                            tci.SRCenterID = ci.SRCenterID;
                            tci.AutoProcessCalculation = ci.AutoProcessCalculation;
                        }

                        //transchargesitemcomp

                        var chargesItemComps = new TransChargesItemCompCollection();
                        chargesItemComps.Query.Where(string.Format("<TransactionNo + SequenceNo IN ('{0}')>", transSeq));
                        chargesItemComps.LoadAll();

                        foreach (var cic in chargesItemComps)
                        {
                            var tcic = chargesItemCompHistory.AddNew();
                            tcic.RecalculationProcessNo = recalHistory.RecalculationProcessNo;
                            tcic.TransactionNo = cic.TransactionNo;
                            tcic.SequenceNo = cic.SequenceNo;
                            tcic.TariffComponentID = cic.TariffComponentID;
                            tcic.Price = cic.Price;
                            tcic.DiscountAmount = cic.DiscountAmount;
                            tcic.ParamedicID = cic.ParamedicID;
                            tcic.LastUpdateDateTime = cic.LastUpdateDateTime;
                            tcic.LastUpdateByUserID = cic.LastUpdateByUserID;
                            tcic.IsPackage = cic.IsPackage;
                            tcic.AutoProcessCalculation = cic.AutoProcessCalculation;
                        }

                        //transprescription
                        var prescriptionHistory = new TransPrescriptionHistoryCollection();

                        //var prescriptions = new TransPrescriptionCollection();
                        prescriptions.Query.Where(prescriptions.Query.PrescriptionNo.In(costHistory.Select(c => c.TransactionNo)));
                        prescriptions.LoadAll();

                        foreach (var p in prescriptions)
                        {
                            var tp = prescriptionHistory.AddNew();
                            tp.RecalculationProcessNo = recalHistory.RecalculationProcessNo;
                            tp.PrescriptionNo = p.PrescriptionNo;
                            tp.PrescriptionDate = p.PrescriptionDate;
                            tp.RegistrationNo = p.RegistrationNo;
                            tp.ServiceUnitID = p.ServiceUnitID;
                            tp.ClassID = p.ClassID;
                            tp.ParamedicID = p.ParamedicID;
                            tp.IsApproval = p.IsApproval;
                            tp.IsVoid = p.IsVoid;
                            tp.Note = p.Note;
                            tp.LastUpdateDateTime = p.LastUpdateDateTime;
                            tp.LastUpdateByUserID = p.LastUpdateByUserID;
                            tp.IsPrescriptionReturn = p.IsPrescriptionReturn;
                            tp.ReferenceNo = p.ReferenceNo;
                            tp.IsFromSOAP = p.IsFromSOAP;
                            tp.IsBillProceed = p.IsBillProceed;
                            tp.IsUnitDosePrescription = p.IsUnitDosePrescription;
                            tp.IsCito = p.IsCito;
                            tp.IsClosed = p.IsClosed;
                            tp.ApprovalDateTime = p.ApprovalDateTime;
                            tp.DeliverDateTime = p.DeliverDateTime;
                        }

                        //transprescriptionitem
                        var prescriptionItemHistory = new TransPrescriptionItemHistoryCollection();

                        var prescriptionItems = new TransPrescriptionItemCollection();
                        prescriptionItems.Query.Where(string.Format("<PrescriptionNo + SequenceNo IN ('{0}')>", transSeq));
                        prescriptionItems.LoadAll();

                        foreach (var pi in prescriptionItems)
                        {
                            var tpi = prescriptionItemHistory.AddNew();
                            tpi.RecalculationProcessNo = recalHistory.RecalculationProcessNo;
                            tpi.PrescriptionNo = pi.PrescriptionNo;
                            tpi.SequenceNo = pi.SequenceNo;
                            tpi.ParentNo = pi.ParentNo;
                            tpi.IsRFlag = pi.IsRFlag;
                            tpi.IsCompound = pi.IsCompound;
                            tpi.ItemID = pi.ItemID;
                            tpi.ItemInterventionID = pi.ItemInterventionID;
                            tpi.SRItemUnit = pi.SRItemUnit;
                            tpi.ItemQtyInString = pi.ItemQtyInString;
                            tpi.IsUsingDosageUnit = pi.IsUsingDosageUnit;
                            tpi.SRDosageUnit = pi.SRDosageUnit;
                            tpi.FrequencyOfDosing = pi.FrequencyOfDosing;
                            tpi.DosingPeriod = pi.DosingPeriod;
                            tpi.NumberOfDosage = pi.NumberOfDosage;
                            tpi.DurationOfDosing = pi.DurationOfDosing;
                            tpi.Acpcdc = pi.Acpcdc;
                            tpi.SRMedicationRoute = pi.SRMedicationRoute;
                            tpi.ConsumeMethod = pi.ConsumeMethod;
                            tpi.PrescriptionQty = pi.PrescriptionQty;
                            tpi.TakenQty = pi.TakenQty;
                            tpi.ResultQty = pi.ResultQty;
                            tpi.CostPrice = pi.CostPrice;
                            tpi.InitialPrice = pi.InitialPrice;
                            tpi.Price = pi.Price;
                            tpi.DiscountAmount = pi.DiscountAmount;
                            tpi.EmbalaceID = pi.EmbalaceID;
                            tpi.EmbalaceAmount = pi.EmbalaceAmount;
                            tpi.IsUseSweetener = pi.IsUseSweetener;
                            tpi.SweetenerAmount = pi.SweetenerAmount;
                            tpi.LineAmount = pi.LineAmount;
                            tpi.Notes = pi.Notes;
                            tpi.LastUpdateDateTime = pi.LastUpdateDateTime;
                            tpi.LastUpdateByUserID = pi.LastUpdateByUserID;
                            tpi.SRDiscountReason = pi.SRDiscountReason;
                            tpi.IsApprove = pi.IsApprove;
                            tpi.IsVoid = pi.IsVoid;
                            tpi.IsBillProceed = pi.IsBillProceed;
                            tpi.DurationRelease = pi.DurationRelease;
                            tpi.AutoProcessCalculation = pi.AutoProcessCalculation;
                            //  tpi.ConsumeMethodText = pi.ConsumeMethodText;
                        }


                        autoNumber.Save();
                        recalHistory.Save();
                        chargesHistory.Save();
                        chargesItemHistory.Save();
                        chargesItemCompHistory.Save();
                        prescriptionHistory.Save();
                        prescriptionItemHistory.Save();
                        costHistory.Save();
                    }
                }

                #endregion

                #region rollback  //rollback
                var grrID = reg.GuarantorID;
                if (grrID == _selfGuarantor)
                {
                    var pat = new Patient();
                    pat.LoadByPrimaryKey(reg.PatientID);
                    if (!string.IsNullOrEmpty(pat.MemberID))
                        grrID = pat.MemberID;
                }

                var grr = new Guarantor();
                grr.LoadByPrimaryKey(grrID);

                var operatingRooms = new ServiceRoomCollection();
                operatingRooms.Query.Select(operatingRooms.Query.ServiceUnitID);
                operatingRooms.Query.Where(
                    operatingRooms.Query.IsOperatingRoom == true,
                    operatingRooms.Query.IsResetPrice == true,
                    operatingRooms.Query.IsActive == true
                    );
                operatingRooms.LoadAll();

                var itemTariffRooms = new AppStandardReferenceItemCollection();
                itemTariffRooms.Query.Select(itemTariffRooms.Query.ItemID);
                itemTariffRooms.Query.Where(
                    itemTariffRooms.Query.StandardReferenceID == AppEnum.StandardReference.ItemTariffRoom,
                    itemTariffRooms.Query.IsActive == true
                    );
                itemTariffRooms.LoadAll();

                var tariffCompParamedics = new TariffComponentCollection();
                tariffCompParamedics.Query.Select(tariffCompParamedics.Query.TariffComponentID);
                tariffCompParamedics.Query.Where(tariffCompParamedics.Query.IsTariffParamedic == true);
                tariffCompParamedics.LoadAll();

                var isUsingParentRegTypeDefault = AppSession.Parameter.IsUpdatePriceUsingParentRuleWhenRecal;

                pnlInfo3.Visible = false;
                lblInfo3.Text = string.Empty;

                foreach (DataRow row in Transactions.AsEnumerable().Where(v => v.Field<bool>("IsBillProceed") &&
                                                                               !v.Field<bool>("IsVoid") &&
                                                                               !v.Field<bool>("IsPaymentProceed") &&
                                                                               !v.Field<bool>("IsPaymentProceedReff") &&
                                                                               !v.Field<bool>("IsIntermBillProceed")))
                {
                    switch (row["TYPE"].ToString())
                    {
                        case "1":
                            // debug
                            var ddd = row["ItemID"].ToString();
                            if (ddd.Trim() == "AL01.052")
                            {
                                ddd = ddd;
                            }
                            // end of debug

                            // cek jika ada komponen tarif variable maka skip rollback (Fajri - 2024/01/15)
                            var header = new TransCharges();
                            header.LoadByPrimaryKey(row["TransactionNo"].ToString());

                            var regHeader = new Registration();
                            regHeader.LoadByPrimaryKey(header.RegistrationNo);

                            DateTime transDate = header.TransactionDate.Value.Date;

                            if (header.IsCorrection ?? false)
                            {
                                var headerReff = new TransCharges();
                                if (headerReff.LoadByPrimaryKey(header.ReferenceNo))
                                {
                                    transDate = headerReff.TransactionDate.Value.Date;

                                    regHeader = new Registration();
                                    regHeader.LoadByPrimaryKey(headerReff.RegistrationNo);
                                }
                            }

                            var it = new Item();
                            it.LoadByPrimaryKey(ddd);

                            DateTime tariffDate;
                            if (it.SRItemType == ItemType.Medical || it.SRItemType == ItemType.NonMedical ||
                                it.SRItemType == ItemType.Kitchen)
                            {
                                tariffDate = transDate;
                            }
                            else
                                tariffDate = grr.TariffCalculationMethod == 1 ? reg.RegistrationDate.Value.Date : transDate;

                            var operatingRoom = (operatingRooms.Where(i => i.ServiceUnitID == header.ToServiceUnitID)
                                            .Select(i => i.ServiceUnitID)).Distinct().SingleOrDefault();

                            var isOperatingRoomUsingParentRegType = false;
                            if (operatingRoom != null)
                            {
                                if (_isOperatingRoomResetPrice)
                                {
                                    if (_isOperatingRoomResetPriceLastClass)
                                    {
                                        isOperatingRoomUsingParentRegType = true;
                                    }
                                    else
                                    {
                                        if (_isOperatingRoomResetPriceHighestClass)
                                        {
                                            isOperatingRoomUsingParentRegType = true;
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(cboProcedureClassID.SelectedValue))
                                            {
                                                isOperatingRoomUsingParentRegType = true;
                                            }
                                        }
                                    }
                                }
                            }

                            var isUsingParentRegType = isUsingParentRegTypeDefault || isOperatingRoomUsingParentRegType;

                            ItemTariff tariff = (Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, header.ClassID, header.ClassID, ddd, cboGuarantorID.SelectedValue, false, (isUsingParentRegType ? reg.SRRegistrationType : regHeader.SRRegistrationType)) ??
                                                 Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, _defaultTariffClass, header.ClassID, ddd, cboGuarantorID.SelectedValue, false, (isUsingParentRegType ? reg.SRRegistrationType : regHeader.SRRegistrationType))) ??
                                                (Helper.Tariff.GetItemTariff(tariffDate, _defaultTariffType, header.ClassID, header.ClassID, ddd, cboGuarantorID.SelectedValue, false, (isUsingParentRegType ? reg.SRRegistrationType : regHeader.SRRegistrationType)) ??
                                                 Helper.Tariff.GetItemTariff(tariffDate, _defaultTariffType, _defaultTariffClass, header.ClassID, ddd, cboGuarantorID.SelectedValue, false, (isUsingParentRegType ? reg.SRRegistrationType : regHeader.SRRegistrationType)));

                            if (tariff != null && tariff.IsComponentsHaveAllowVariable()) continue;
                            // end of cek jika ada komponen tarif variable maka skip rollback

                            if (tariff == null)
                            {
                                pnlInfo3.Visible = true;
                                lblInfo3.Text = "Tariff Item [" + ddd + "] Not Found";
                            }

                            var detail = new TransChargesItem();
                            if (detail.LoadByPrimaryKey(row["TransactionNo"].ToString(), row["SequenceNo"].ToString()))
                            {
                                if (detail.AutoProcessCalculation < 0)
                                {
                                    detail.DiscountAmount += (detail.AutoProcessCalculation * Math.Abs(detail.ChargeQuantity ?? 0));
                                    if (detail.DiscountAmount > detail.Price * Math.Abs(detail.ChargeQuantity ?? 0))
                                        detail.DiscountAmount = detail.Price * Math.Abs(detail.ChargeQuantity ?? 0);
                                }
                                else
                                    detail.Price -= detail.AutoProcessCalculation;
                                detail.AutoProcessCalculation = 0;
                                detail.Save();
                            }

                            var comps = new TransChargesItemCompCollection();
                            comps.Query.Where(
                                comps.Query.TransactionNo == row["TransactionNo"].ToString(),
                                comps.Query.SequenceNo == row["SequenceNo"].ToString()
                                );
                            comps.LoadAll();

                            foreach (var comp in comps)
                            {
                                if (comp.AutoProcessCalculation == null)
                                    comp.AutoProcessCalculation = 0;

                                if (comp.AutoProcessCalculation < 0)
                                {
                                    comp.DiscountAmount += comp.AutoProcessCalculation;
                                    if (comp.DiscountAmount > comp.Price)
                                        comp.DiscountAmount = comp.Price;
                                }
                                else
                                    comp.Price -= comp.AutoProcessCalculation;
                                comp.AutoProcessCalculation = 0;
                            }
                            comps.Save();

                            break;
                        case "2":
                            var presc = new TransPrescriptionItem();
                            if (presc.LoadByPrimaryKey(row["TransactionNo"].ToString(), row["SequenceNo"].ToString()))
                            {
                                if (presc.AutoProcessCalculation < 0)
                                {
                                    presc.DiscountAmount += (presc.AutoProcessCalculation * Math.Abs(presc.ResultQty ?? 0));
                                    if (presc.DiscountAmount > presc.Price * Math.Abs(presc.ResultQty ?? 0))
                                        presc.DiscountAmount = presc.Price * Math.Abs(presc.ResultQty ?? 0);
                                }
                                else
                                    presc.Price -= presc.AutoProcessCalculation;
                                presc.AutoProcessCalculation = 0;
                                presc.Save();
                            }
                            break;
                    }
                }

                //db:20231218 - di remark antispasi u/ tarif variabel yg ikut terhapus
                //CostCalculations.MarkAllAsDeleted();
                //CostCalculations.Save();

                #endregion

                #region recalculation
                foreach (DataRow row in Transactions.AsEnumerable().Where(v => v.Field<bool>("IsBillProceed") &&
                                                                               !v.Field<bool>("IsVoid") &&
                                                                               !v.Field<bool>("IsPaymentProceed") &&
                                                                               //!v.Field<bool>("IsPaymentProceedReff") &&
                                                                               !v.Field<bool>("IsIntermBillProceed")))
                {
                    switch (row["TYPE"].ToString())
                    {
                        case "1":
                            var detail = new TransChargesItem();
                            if (detail.LoadByPrimaryKey(row["TransactionNo"].ToString(), row["SequenceNo"].ToString()))
                            {
                                var header = new TransCharges();
                                header.LoadByPrimaryKey(row["TransactionNo"].ToString());

                                if (header.IsOrder == true && detail.IsBillProceed == false)
                                    continue;

                                var regHeader = new Registration();
                                regHeader.LoadByPrimaryKey(header.RegistrationNo);
                                coverageClassId = regHeader.CoverageClassID;

                                DateTime transDate = header.TransactionDate.Value.Date;

                                if (header.IsCorrection ?? false)
                                {
                                    var headerReff = new TransCharges();
                                    if (headerReff.LoadByPrimaryKey(header.ReferenceNo))
                                    {
                                        transDate = headerReff.TransactionDate.Value.Date;

                                        regHeader = new Registration();
                                        regHeader.LoadByPrimaryKey(headerReff.RegistrationNo);
                                    }
                                }

                                var itm = new Item();
                                itm.LoadByPrimaryKey(detail.ItemID);

                                DateTime tariffDate;
                                if (itm.SRItemType == ItemType.Medical || itm.SRItemType == ItemType.NonMedical ||
                                    itm.SRItemType == ItemType.Kitchen)
                                {
                                    tariffDate = transDate;
                                }
                                else
                                    tariffDate = grr.TariffCalculationMethod == 1 ? reg.RegistrationDate.Value.Date : transDate;

                                detail.TariffDate = tariffDate;
                                detail.IsBillProceed = true;


                                var operatingRoom = (operatingRooms.Where(i => i.ServiceUnitID == header.ToServiceUnitID)
                                                 .Select(i => i.ServiceUnitID)).Distinct().SingleOrDefault();

                                var isOperatingRoomUsingParentRegType = false;
                                if (operatingRoom != null)
                                {
                                    if (_isOperatingRoomResetPrice)
                                    {
                                        if (_isOperatingRoomResetPriceLastClass)
                                        {
                                            header.ClassID = txtChargeClassID.Text;
                                            header.Save();

                                            detail.ChargeClassID = txtChargeClassID.Text;
                                            isOperatingRoomUsingParentRegType = true;
                                        }
                                        else
                                        {
                                            if (_isOperatingRoomResetPriceHighestClass)
                                            {
                                                var cHist = new PatientTransferHistoryQuery("a");
                                                var c = new ClassQuery("b");
                                                cHist.InnerJoin(c).On(cHist.ChargeClassID == c.ClassID);
                                                cHist.Where(cHist.RegistrationNo == header.RegistrationNo);
                                                cHist.Select(cHist.ChargeClassID);
                                                cHist.OrderBy(c.ClassSeq.Ascending, cHist.ChargeClassID.Ascending);
                                                cHist.es.Top = 1;

                                                DataTable dtcHist = cHist.LoadDataTable();
                                                if (dtcHist.Rows.Count > 0)
                                                {
                                                    header.ClassID = dtcHist.Rows[0]["ChargeClassID"].ToString();
                                                    header.Save();

                                                    detail.ChargeClassID = header.ClassID;
                                                }
                                                else //patient blm ada data transfer, ambil reg terakhir
                                                {
                                                    header.ClassID = txtChargeClassID.Text;
                                                    header.Save();

                                                    detail.ChargeClassID = header.ClassID;
                                                }
                                                isOperatingRoomUsingParentRegType = true;
                                            }
                                            else
                                            {
                                                //diganti sesuai dg class operasi
                                                if (!string.IsNullOrEmpty(cboProcedureClassID.SelectedValue))
                                                {
                                                    header.ClassID = cboProcedureClassID.SelectedValue;
                                                    header.Save();

                                                    detail.ChargeClassID = cboProcedureClassID.SelectedValue;
                                                    isOperatingRoomUsingParentRegType = true;
                                                }
                                            }
                                        }
                                    }
                                }

                                var isUsingParentRegType = isUsingParentRegTypeDefault || isOperatingRoomUsingParentRegType;

                                //start recalculation
                                ItemTariff tariff = (Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, header.ClassID, header.ClassID, detail.ItemID, cboGuarantorID.SelectedValue, false, (isUsingParentRegType ? reg.SRRegistrationType : regHeader.SRRegistrationType)) ??
                                                     Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, _defaultTariffClass, header.ClassID, detail.ItemID, cboGuarantorID.SelectedValue, false, (isUsingParentRegType ? reg.SRRegistrationType : regHeader.SRRegistrationType))) ??
                                                    (Helper.Tariff.GetItemTariff(tariffDate, _defaultTariffType, header.ClassID, header.ClassID, detail.ItemID, cboGuarantorID.SelectedValue, false, (isUsingParentRegType ? reg.SRRegistrationType : regHeader.SRRegistrationType)) ??
                                                     Helper.Tariff.GetItemTariff(tariffDate, _defaultTariffType, _defaultTariffClass, header.ClassID, detail.ItemID, cboGuarantorID.SelectedValue, false, (isUsingParentRegType ? reg.SRRegistrationType : regHeader.SRRegistrationType)));

                                
                                if (tariff != null && tariff.IsComponentsHaveAllowVariable())
                                {
                                    //db:20231226 - add cc kalo udah terlanjur pernah hilang
                                    try
                                    {
                                        var cost = (CostCalculations.Where(b => b.RegistrationNo == row["RegistrationNo"].ToString() && b.TransactionNo == detail.TransactionNo && b.SequenceNo == detail.SequenceNo)).Take(1).Single();
                                    }
                                    catch (Exception)
                                    {
                                        DataTable tblCoveredXX;
                                        tblCoveredXX = Helper.GetCoveredItems(regHeader.RegistrationNo, grrID, coverageClassId /*reg.CoverageClassID*/, detail.ItemID, tariffDate, false);

                                        //post
                                        decimal? totalXX = ((Math.Abs(detail.ChargeQuantity ?? 0) * detail.Price) - detail.DiscountAmount) + detail.CitoAmount;

                                        var calcXX = new Helper.CostCalculation(grrID, detail.ItemID, totalXX ?? 0, tblCoveredXX,
                                                                              Math.Abs(detail.ChargeQuantity ?? 0),
                                                                              detail.IsCito ?? false,
                                                                              detail.IsCitoInPercent ?? false,
                                                                              detail.BasicCitoAmount ?? 0, detail.Price ?? 0,
                                                                              header.IsRoomIn ?? false, detail.IsItemRoom ?? false,
                                                                              header.TariffDiscountForRoomIn ?? 0, detail.DiscountAmount ?? 0,
                                                                              reg.IsGlobalPlafond ?? false,
                                                                              detail.ItemConditionRuleID, tariffDate, detail.IsVariable ?? false);

                                        if (operatingRoom != null && !string.IsNullOrEmpty(header.SurgicalPackageID))
                                        {
                                            var package = new GuarantorSurgicalPackageCoveredItem();
                                            if (package.LoadByPrimaryKey(grrID, header.SurgicalPackageID, detail.ItemID))
                                            {
                                                if (calcXX.PatientAmount + calcXX.GuarantorAmount <= package.CoveredAmount)
                                                {
                                                    calcXX.GuarantorAmount = calcXX.PatientAmount + calcXX.GuarantorAmount;
                                                    calcXX.PatientAmount = 0;
                                                }
                                                else
                                                {
                                                    calcXX.PatientAmount = calcXX.PatientAmount + calcXX.GuarantorAmount - package.CoveredAmount ?? 0;
                                                    calcXX.GuarantorAmount = package.CoveredAmount ?? 0;
                                                }
                                            }
                                        }

                                        var cost = CostCalculations.AddNew();
                                        cost.RegistrationNo = row["RegistrationNo"].ToString();
                                        cost.TransactionNo = detail.TransactionNo;
                                        cost.SequenceNo = detail.SequenceNo;
                                        cost.ItemID = detail.ItemID;
                                        //start here
                                        decimal? totaltrans = calcXX.GuarantorAmount + calcXX.PatientAmount + (detail.DiscountAmount ?? 0);
                                        decimal? totaldisc = detail.DiscountAmount ?? 0;

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
                                            if (calcXX.GuarantorAmount > 0)
                                            {
                                                cost.DiscountAmount = totaldisc > calcXX.GuarantorAmount + detail.DiscountAmount
                                                                           ? calcXX.GuarantorAmount + detail.DiscountAmount
                                                                           : totaldisc;

                                                cost.GuarantorAmount = totaldisc > calcXX.GuarantorAmount + detail.DiscountAmount
                                                                           ? 0
                                                                           : calcXX.GuarantorAmount + detail.DiscountAmount - totaldisc;
                                                cost.PatientAmount = calcXX.PatientAmount;

                                            }
                                            else
                                            {
                                                cost.DiscountAmount = totaldisc > calcXX.PatientAmount + detail.DiscountAmount
                                                                          ? calcXX.PatientAmount + detail.DiscountAmount
                                                                          : totaldisc;

                                                cost.PatientAmount = totaldisc > calcXX.PatientAmount + detail.DiscountAmount
                                                                         ? 0
                                                                         : calcXX.PatientAmount + detail.DiscountAmount - totaldisc;
                                                cost.GuarantorAmount = calcXX.GuarantorAmount;
                                            }
                                        }
                                        //end

                                        var compsXX = new TransChargesItemCompCollection();
                                        compsXX.Query.Where(
                                            compsXX.Query.TransactionNo == detail.TransactionNo,
                                            compsXX.Query.SequenceNo == detail.SequenceNo
                                            );
                                        compsXX.LoadAll();

                                        cost.PatientAmount = (detail.ChargeQuantity < 0) ? 0 - cost.PatientAmount : cost.PatientAmount;
                                        cost.GuarantorAmount = (detail.ChargeQuantity < 0) ? 0 - cost.GuarantorAmount : cost.GuarantorAmount;
                                        cost.DiscountAmount = (detail.ChargeQuantity < 0) ? 0 - cost.DiscountAmount : cost.DiscountAmount;
                                        cost.IsPackage = detail.IsPackage;
                                        cost.ParentNo = detail.ParentNo;
                                        cost.ParamedicAmount = detail.ChargeQuantity * compsXX.Where(comp => comp.TransactionNo == detail.TransactionNo &&
                                                                                                           comp.SequenceNo == detail.SequenceNo &&
                                                                                                           !string.IsNullOrEmpty(comp.ParamedicID))
                                                                                            .Sum(comp => comp.Price - comp.DiscountAmount);
                                        cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                        cost.LastUpdateByUserID = AppSession.UserLogin.UserID;

                                        cost.TransactionDate = transDate;
                                        cost.ReferenceNo = header.ReferenceNo;
                                        var su = new ServiceUnit();
                                        if (su.LoadByPrimaryKey(header.ToServiceUnitID))
                                            cost.ServiceUnitName = su.ServiceUnitName;
                                        var item = new Item();
                                        if (item.LoadByPrimaryKey(cost.ItemID))
                                            cost.ItemName = string.IsNullOrEmpty(detail.ParamedicCollectionName)
                                                                ? item.ItemName
                                                                : item.ItemName + " (" + detail.ParamedicCollectionName + ")";
                                        var cls = new Class();
                                        if (cls.LoadByPrimaryKey(header.ClassID))
                                            cost.ClassName = cls.ClassName;
                                    }

                                    continue;// gak usah hitung ulang karena ada yang variable
                                }

                                //--recal detail paket
                                #region recalculate detail paket
                                if (header.IsPackage ?? false)
                                {
                                    var pacs = new TransChargesItemCollection();
                                    var pacDtQ = new TransChargesItemQuery("dt");
                                    var pacHdQ = new TransChargesQuery("hd");
                                    pacDtQ.InnerJoin(pacHdQ).On(pacDtQ.TransactionNo == pacHdQ.TransactionNo);
                                    pacDtQ.Where(pacHdQ.PackageReferenceNo == detail.TransactionNo,
                                                 pacDtQ.SequenceNo.Substring(1, 3) == detail.SequenceNo);

                                    pacs.Load(pacDtQ);

                                    bool isFromItemPackComp = false;
                                    var collItemPackageComp = new ItemPackageTariffComponentCollection();
                                    collItemPackageComp.Query.Where(collItemPackageComp.Query.ItemID == detail.ItemID);
                                    collItemPackageComp.LoadAll();
                                    if (collItemPackageComp.Count > 0)
                                        isFromItemPackComp = true;

                                    foreach (var pac in pacs)
                                    {
                                        decimal pricePackage = 0;
                                        decimal discPackage = 0;

                                        var pHeader = new TransCharges();
                                        pHeader.LoadByPrimaryKey(pac.TransactionNo);

                                        decimal paramedicAmount = 0;
                                        var itmpacs = new Item();
                                        itmpacs.LoadByPrimaryKey(pac.ItemID);

                                        switch (itmpacs.SRItemType)
                                        {
                                            case ItemType.Medical:
                                            case ItemType.NonMedical:
                                            case ItemType.Kitchen:
                                                if (isFromItemPackComp == true)
                                                {
                                                    var tariffCompPack = new ItemPackageTariffComponentCollection();
                                                    tariffCompPack.Query.Where(tariffCompPack.Query.ItemID == detail.ItemID, tariffCompPack.Query.DetailItemID == pac.ItemID);
                                                    tariffCompPack.LoadAll();
                                                    if (tariffCompPack.Count > 0)
                                                    {
                                                        var comp = tariffCompPack.First();
                                                        //pricePackage = comp.Price ?? 0;
                                                        //discPackage = comp.Discount ?? 0;
                                                        pricePackage = Helper.Tariff.GetItemConditionRuleTariff(comp.Price ?? 0, detail.ItemConditionRuleID, transDate);
                                                        discPackage = Helper.Tariff.GetItemConditionRuleTariff(comp.Discount ?? 0, detail.ItemConditionRuleID, transDate);
                                                    }
                                                }
                                                else
                                                {
                                                    var tariffpacs = (Helper.Tariff.GetItemTariff(transDate, grr.SRTariffType, detail.ChargeClassID, detail.ChargeClassID, pac.ItemID, reg.GuarantorID, false, (isUsingParentRegType ? reg.SRRegistrationType : regHeader.SRRegistrationType)) ??
                                                      Helper.Tariff.GetItemTariff(transDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, detail.ChargeClassID, pac.ItemID, reg.GuarantorID, false, (isUsingParentRegType ? reg.SRRegistrationType : regHeader.SRRegistrationType))) ??
                                                     (Helper.Tariff.GetItemTariff(transDate, AppSession.Parameter.DefaultTariffType, detail.ChargeClassID, detail.ChargeClassID, pac.ItemID, reg.GuarantorID, false, (isUsingParentRegType ? reg.SRRegistrationType : regHeader.SRRegistrationType)) ??
                                                      Helper.Tariff.GetItemTariff(transDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, detail.ChargeClassID, pac.ItemID, reg.GuarantorID, false, (isUsingParentRegType ? reg.SRRegistrationType : regHeader.SRRegistrationType)));

                                                    pricePackage = Helper.Tariff.GetItemConditionRuleTariff(tariffpacs.Price ?? 0, detail.ItemConditionRuleID, transDate);
                                                }
                                                break;
                                            case ItemType.Diagnostic:
                                            case ItemType.Laboratory:
                                            case ItemType.Package:
                                            case ItemType.Radiology:
                                            case ItemType.Service:
                                                //kl ItemPackageTariffComponent ada isinya maka semua ambil dari situ karena komponen yg ada di itemtariff tidak selalu sama
                                                // dengan yg sudah disetting di ItemPackageTariffComponent sehingga cara lama gak bisa dipakai karena detail di transchargeitemcomp jadi salah
                                                if (isFromItemPackComp == true)
                                                {
                                                    var tariffCompPack = new ItemPackageTariffComponentCollection();
                                                    tariffCompPack.Query.Where(tariffCompPack.Query.ItemID == detail.ItemID, tariffCompPack.Query.DetailItemID == pac.ItemID);
                                                    tariffCompPack.LoadAll();

                                                    if (tariffCompPack.Count > 0)
                                                    {
                                                        foreach (var comp in tariffCompPack)
                                                        {
                                                            var tcomp = new TransChargesItemComp();
                                                            if (tcomp.LoadByPrimaryKey(pac.TransactionNo, pac.SequenceNo, comp.TariffComponentID))
                                                            {
                                                                //tcomp.Price = comp.Price ?? 0;
                                                                //tcomp.DiscountAmount = comp.Discount ?? 0;
                                                                tcomp.Price = Helper.Tariff.GetItemConditionRuleTariff(comp.Price ?? 0, detail.ItemConditionRuleID, tariffDate);
                                                                tcomp.DiscountAmount = Helper.Tariff.GetItemConditionRuleTariff(comp.Discount ?? 0, detail.ItemConditionRuleID, tariffDate);

                                                                tcomp.Save();

                                                                pricePackage += tcomp.Price ?? 0;
                                                                discPackage += tcomp.DiscountAmount ?? 0;

                                                                var tcompColl = new TransChargesItemCompCollection();
                                                                tcompColl.AttachEntity(tcomp);
                                                                if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                                                                {
                                                                    // extract fee
                                                                    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                                                                    feeColl.SetFeeByTCIC(tcompColl, AppSession.UserLogin.UserID);
                                                                    feeColl.Save();
                                                                    //feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                                                                    //feeColl.Save();
                                                                }

                                                                if (!string.IsNullOrEmpty(tcomp.ParamedicID))
                                                                    paramedicAmount += (tcomp.Price ?? 0 - tcomp.DiscountAmount ?? 0);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    var packageComps = Helper.Tariff.GetItemTariffComponentCollection(transDate, grr.SRTariffType, detail.ChargeClassID, pac.ItemID);
                                                    if (!packageComps.Any())
                                                        packageComps = Helper.Tariff.GetItemTariffComponentCollection(transDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, pac.ItemID);
                                                    if (!packageComps.Any())
                                                        packageComps = Helper.Tariff.GetItemTariffComponentCollection(transDate, AppSession.Parameter.DefaultTariffType, detail.ChargeClassID, pac.ItemID);
                                                    if (!packageComps.Any())
                                                        packageComps = Helper.Tariff.GetItemTariffComponentCollection(transDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, pac.ItemID);

                                                    decimal packageDiscValue = 0;
                                                    var itemPackage = new ItemPackage();
                                                    if (itemPackage.LoadByPrimaryKey(detail.ItemID, pac.ItemID, pHeader.ToServiceUnitID))
                                                        packageDiscValue = itemPackage.DiscountValue ?? 0;

                                                    foreach (var comp in packageComps)
                                                    {
                                                        var tcomp = new TransChargesItemComp();
                                                        if (tcomp.LoadByPrimaryKey(pac.TransactionNo, pac.SequenceNo, comp.TariffComponentID))
                                                        {
                                                            //tcomp.Price = comp.Price ?? 0;
                                                            tcomp.Price = Helper.Tariff.GetItemConditionRuleTariff(comp.Price ?? 0, detail.ItemConditionRuleID, tariffDate);
                                                            tcomp.DiscountAmount = tcomp.Price * packageDiscValue / 100;
                                                            tcomp.Save();

                                                            pricePackage += tcomp.Price ?? 0;
                                                            discPackage += tcomp.DiscountAmount ?? 0;

                                                            var tcompColl = new TransChargesItemCompCollection();
                                                            tcompColl.AttachEntity(tcomp);
                                                            if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                                                            {
                                                                // extract fee
                                                                var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                                                                feeColl.SetFeeByTCIC(tcompColl, AppSession.UserLogin.UserID);
                                                                feeColl.Save();
                                                                //feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                                                                //feeColl.Save();
                                                            }

                                                            if (!string.IsNullOrEmpty(tcomp.ParamedicID))
                                                                paramedicAmount += (tcomp.Price ?? 0 - tcomp.DiscountAmount ?? 0);
                                                        }
                                                    }
                                                }

                                                break;
                                        }

                                        pac.Price = pricePackage;
                                        pac.DiscountAmount = discPackage * pac.ChargeQuantity;

                                        //post
                                        decimal? pTotal = ((Math.Abs(pac.ChargeQuantity ?? 0) * pac.Price) - pac.DiscountAmount) + pac.CitoAmount;
                                        DataTable pTblCovered = Helper.GetCoveredItems(txtRegistrationNo.Text, grrID,
                                                                              coverageClassId /*reg.CoverageClassID*/, detail.ItemID,
                                                                              tariffDate, false);

                                        var pCalc = new Helper.CostCalculation(grrID, detail.ItemID, pTotal ?? 0, pTblCovered,
                                                                              Math.Abs(pac.ChargeQuantity ?? 0),
                                                                              pac.IsCito ?? false,
                                                                              pac.IsCitoInPercent ?? false,
                                                                              pac.BasicCitoAmount ?? 0, pac.Price ?? 0,
                                                                              header.IsRoomIn ?? false, pac.IsItemRoom ?? false,
                                                                              header.TariffDiscountForRoomIn ?? 0, pac.DiscountAmount ?? 0,
                                                                              reg.IsGlobalPlafond ?? false,
                                                                              detail.ItemConditionRuleID, tariffDate, detail.IsVariable ?? false);

                                        //CostCalculations
                                        var pCost = new CostCalculation();
                                        if (pCost.LoadByPrimaryKey(pHeader.RegistrationNo, pac.TransactionNo, pac.SequenceNo))
                                        {
                                            pCost.PatientAmount = (pac.ChargeQuantity < 0) ? 0 - pCalc.PatientAmount : pCalc.PatientAmount;
                                            pCost.GuarantorAmount = (pac.ChargeQuantity < 0) ? 0 - pCalc.GuarantorAmount : pCalc.GuarantorAmount;
                                            pCost.DiscountAmount = (pac.ChargeQuantity < 0) ? 0 - pac.DiscountAmount : pac.DiscountAmount;
                                            pCost.DiscountAmount2 = 0;
                                            pCost.Save();
                                        }
                                        else
                                        {
                                            pCost.AddNew();
                                            pCost.RegistrationNo = pHeader.RegistrationNo;
                                            pCost.TransactionNo = pac.TransactionNo;
                                            pCost.SequenceNo = pac.SequenceNo;
                                            pCost.ItemID = pac.ItemID;
                                            pCost.PatientAmount = (pac.ChargeQuantity < 0) ? 0 - pCalc.PatientAmount : pCalc.PatientAmount;
                                            pCost.GuarantorAmount = (pac.ChargeQuantity < 0) ? 0 - pCalc.GuarantorAmount : pCalc.GuarantorAmount;
                                            pCost.DiscountAmount = (pac.ChargeQuantity < 0) ? 0 - pac.DiscountAmount : pac.DiscountAmount;
                                            pCost.DiscountAmount2 = 0;

                                            pCost.IsPackage = false;
                                            pCost.ParentNo = string.Empty;
                                            pCost.ParamedicAmount = paramedicAmount;
                                            pCost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                            pCost.LastUpdateByUserID = AppSession.UserLogin.UserID;

                                            pCost.Save();
                                        }
                                    }

                                    pacs.Save();
                                }
                                #endregion

                                //--cek comp kosong apa gak, kalo kosong reload lagi dari master
                                #region Cek Comp
                                var comps = new TransChargesItemCompCollection();
                                comps.Query.Where(
                                    comps.Query.TransactionNo == row["TransactionNo"].ToString(),
                                    comps.Query.SequenceNo == row["SequenceNo"].ToString()
                                    );
                                comps.Query.OrderBy(comps.Query.TariffComponentID.Ascending);
                                comps.LoadAll();

                                //db:20250429 - bandingkan existing component dg tariff component
                                var existingComp = string.Empty;
                                foreach (var c in comps)
                                {
                                    existingComp += (c.TariffComponentID + "|");
                                }
                                var tariffComp = string.Empty;

                                if (itm.SRItemType != ItemType.Medical && itm.SRItemType != ItemType.NonMedical && itm.SRItemType != ItemType.Kitchen)
                                {
                                    var compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, grr.SRTariffType, header.ClassID, detail.ItemID);
                                    if (!compColl.Any())
                                        compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, grr.SRTariffType, _defaultTariffClass, detail.ItemID);
                                    if (!compColl.Any())
                                        compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, _defaultTariffType, header.ClassID, detail.ItemID);
                                    if (!compColl.Any())
                                        compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, _defaultTariffType, _defaultTariffClass, detail.ItemID);


                                    //db:20250429
                                    foreach (var comp in compColl)
                                    {
                                        tariffComp += (comp.TariffComponentID + "|");
                                    }

                                    //db:20250429 - bandingkan existing component dg tariff component 
                                    if (existingComp != tariffComp)
                                    {
                                        //--> jika berbeda maka delete existing, insert baru sesuai master
                                        //comps.MarkAllAsDeleted();

                                        foreach (var comp in compColl)
                                        {

                                            TransChargesItemComp compCharges;
                                            try
                                            {
                                                compCharges = (comps.Where(b => b.TariffComponentID == comp.TariffComponentID)).Take(1).Single();
                                            }
                                            catch (Exception)
                                            {
                                                //--> jika belum ada, insert baru sesuai master

                                                compCharges = comps.AddNew();
                                                compCharges.TransactionNo = header.TransactionNo;
                                                compCharges.SequenceNo = detail.SequenceNo;
                                                compCharges.TariffComponentID = comp.TariffComponentID;
                                                compCharges.Price = 0;
                                                compCharges.DiscountAmount = (decimal)0D;
                                                compCharges.CitoAmount = 0;
                                                
                                                var tcomp = new TariffComponent();
                                                tcomp.LoadByPrimaryKey(comp.TariffComponentID);
                                                if (tcomp.IsTariffParamedic ?? false)
                                                {
                                                    if (!string.IsNullOrEmpty(detail.ParamedicCollectionName))
                                                    {
                                                        var p = new ParamedicQuery();
                                                        p.Where(p.ParamedicName == detail.ParamedicCollectionName);
                                                        p.Select(p.ParamedicID);
                                                        p.es.Top = 1;

                                                        DataTable dtp = p.LoadDataTable();
                                                        if (dtp.Rows.Count > 0)
                                                            compCharges.ParamedicID = dtp.Rows[0]["ParamedicID"].ToString();
                                                        else
                                                            compCharges.ParamedicID = string.Empty;
                                                    }
                                                    else
                                                        compCharges.ParamedicID = string.Empty;
                                                }
                                                else
                                                    compCharges.ParamedicID = string.Empty;

                                                compCharges.FeeDiscountPercentage = 0;
                                                compCharges.IsPackage = itm.SRItemType == ItemType.Package;
                                                compCharges.LastUpdateByUserID = detail.LastUpdateByUserID;
                                                compCharges.LastUpdateDateTime = detail.LastUpdateDateTime;

                                            }
                                        }
                                    }

                                    #region db:20250429 - remark
                                    //var i = 0;
                                    //foreach (var comp in compColl)
                                    //{

                                    //    //cek comp-ny sdh ada TransChargesItemComp belum. kalo belum, diinsert
                                    //    var comps2 = new TransChargesItemCompCollection();
                                    //    comps2.Query.Where(
                                    //        comps2.Query.TransactionNo == row["TransactionNo"].ToString(),
                                    //        comps2.Query.SequenceNo == row["SequenceNo"].ToString(),
                                    //        comps2.Query.TariffComponentID == comp.TariffComponentID
                                    //        );
                                    //    comps2.LoadAll();
                                    //    if (comps2.Count == 0)
                                    //    {
                                    //        var compCharges = comps.AddNew();
                                    //        compCharges.TransactionNo = header.TransactionNo;
                                    //        compCharges.SequenceNo = detail.SequenceNo;
                                    //        compCharges.TariffComponentID = comp.TariffComponentID;

                                    //        //if (comp.IsAllowVariable ?? false)
                                    //        //{
                                    //        //    if (i == 0)
                                    //        //        compCharges.Price = (detail.Price / detail.ChargeQuantity);
                                    //        //    else
                                    //        //        compCharges.Price = 0;
                                    //        //}
                                    //        //else
                                    //        //{
                                    //        var compPrice = Helper.Tariff.GetItemConditionRuleTariff(comp.Price ?? 0, detail.ItemConditionRuleID, tariffDate);
                                    //        if (header.IsRoomIn == true && detail.IsItemRoom == true)
                                    //            compCharges.Price = compPrice - (compPrice * header.TariffDiscountForRoomIn / 100);
                                    //        else
                                    //            compCharges.Price = compPrice;
                                    //        //}

                                    //        compCharges.DiscountAmount = (decimal)0D;
                                    //        if (!(detail.IsCito ?? false)) compCharges.CitoAmount = 0;
                                    //        else compCharges.CitoAmount = (!detail.IsCitoInPercent ?? false) ? (detail.BasicCitoAmount ?? 0) : (((detail.BasicCitoAmount ?? 0) / 100) * compCharges.Price);

                                    //        var tcomp = new TariffComponent();
                                    //        tcomp.LoadByPrimaryKey(comp.TariffComponentID);
                                    //        if (tcomp.IsTariffParamedic ?? false)
                                    //        {
                                    //            if (!string.IsNullOrEmpty(detail.ParamedicCollectionName))
                                    //            {
                                    //                var p = new ParamedicQuery();
                                    //                p.Where(p.ParamedicName == detail.ParamedicCollectionName);
                                    //                p.Select(p.ParamedicID);
                                    //                p.es.Top = 1;

                                    //                DataTable dtp = p.LoadDataTable();
                                    //                if (dtp.Rows.Count > 0)
                                    //                    compCharges.ParamedicID = dtp.Rows[0]["ParamedicID"].ToString();
                                    //                else
                                    //                    compCharges.ParamedicID = string.Empty;
                                    //            }
                                    //            else
                                    //                compCharges.ParamedicID = string.Empty;
                                    //        }
                                    //        else
                                    //            compCharges.ParamedicID = string.Empty;

                                    //        compCharges.FeeDiscountPercentage = 0;

                                    //        var fee = compCharges.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                    //            reg.RegistrationNo, detail.ItemID, compCharges.DiscountAmount, AppSession.UserLogin.UserID,
                                    //            header.ClassID, header.ToServiceUnitID);

                                    //        compCharges.IsPackage = itm.SRItemType == ItemType.Package;
                                    //        compCharges.LastUpdateByUserID = detail.LastUpdateByUserID;
                                    //        compCharges.LastUpdateDateTime = detail.LastUpdateDateTime;
                                    //        i += 1;
                                    //    }
                                    //}
                                    #endregion
                                }

                                #endregion

                                if (tariff != null)
                                {
                                    //decimal tariffPrice = tariff.Price ?? 0;
                                    decimal tariffPrice = Helper.Tariff.GetItemConditionRuleTariff(tariff.Price ?? 0, detail.ItemConditionRuleID, tariffDate);
                                    decimal tarifDiscForRoomIn = header.TariffDiscountForRoomIn ?? 0;

                                    if ((header.IsRoomIn ?? false) && (detail.IsItemRoom ?? false))
                                        tariffPrice = tariffPrice - (tariffPrice * tarifDiscForRoomIn / 100);

                                    //if (!(tariff.IsAllowVariable ?? false))
                                    // variabel atau tidak itu hanya bisa dilihat di ItemTariffComponent
                                    // jika salah satu komponen ada yang variabel maka transaksi tidak bisa
                                    // di-Recalculate!!!

                                    if (!(tariff.IsComponentsHaveAllowVariable()))
                                    {
                                        //detail.CostPrice = tariffPrice;
                                        detail.Price = tariffPrice;
                                        detail.CitoAmount = (tariffPrice * (detail.BasicCitoAmount ?? 0) / 100) *
                                                            Math.Abs(detail.ChargeQuantity ?? 0);
                                        detail.DiscountAmount = 0;

                                        foreach (var entity in comps)
                                        {
                                            if (tariff.SRTariffType == null || tariff.ItemID == null || tariff.ClassID == null)
                                                break;

                                            var comp = Helper.Tariff.GetItemTariffComponent(tariff.SRTariffType,
                                                                                                     tariff.ItemID,
                                                                                                     tariff.ClassID,
                                                                                                     tariff.StartingDate ?? (new DateTime()).NowAtSqlServer(),
                                                                                                     entity.TariffComponentID);

                                            if (!comp.AsEnumerable().Any())
                                            {
                                                entity.Price = 0;
                                                entity.CitoAmount = 0;
                                                entity.DiscountAmount = 0;
                                                entity.ParamedicID = string.Empty;
                                            }
                                            else
                                            {
                                                decimal tariffCompPrice = comp.AsEnumerable().Select(c => c.Field<decimal>("Price")).Single();
                                                tariffCompPrice = Helper.Tariff.GetItemConditionRuleTariff(tariffCompPrice, detail.ItemConditionRuleID, tariffDate);

                                                if ((header.IsRoomIn ?? false) && (detail.IsItemRoom ?? false))
                                                    tariffCompPrice = tariffCompPrice - (tariffCompPrice * tarifDiscForRoomIn / 100);

                                                //if (!(comp.AsEnumerable().Select(c => c.Field<bool>("IsAllowVariable")).Single()))
                                                //{
                                                entity.Price = tariffCompPrice;
                                                entity.CitoAmount = tariffCompPrice * (detail.BasicCitoAmount ?? 0) / 100;

                                                var discountRule = 0;
                                                var fee = entity.CalculateParamedicPercentDiscount(
                                                    AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                                    header.RegistrationNo, detail.ItemID, discountRule,
                                                    AppSession.UserLogin.UserID, header.ClassID, header.ToServiceUnitID);

                                                //entity.DiscountAmount = 0;
                                                entity.AutoProcessCalculation = 0 - entity.DiscountAmount;
                                                //}
                                            }
                                        }
                                        detail.IsVariable = false;
                                    }
                                    else
                                    {
                                        detail.IsVariable = true;
                                    }
                                }
                                //end recalculation

                                DataTable tblCovered;
                                tblCovered = Helper.GetCoveredItems(regHeader.RegistrationNo, grrID, coverageClassId /*reg.CoverageClassID*/, detail.ItemID, tariffDate, false);


                                var rowCovered = tblCovered.AsEnumerable().SingleOrDefault(t => t.Field<string>("ItemID") == detail.ItemID &&
                                                                                                t.Field<bool>("IsInclude"));

                                //TransChargesItemComps
                                if (rowCovered != null)
                                {
                                    decimal? discount = 0;
                                    bool isDiscount = false, isMargin = false;
                                    foreach (var comp in comps.Where(t => t.TransactionNo == detail.TransactionNo &&
                                                                          t.SequenceNo == detail.SequenceNo)
                                                              .OrderBy(t => t.TariffComponentID))
                                    {
                                        var amountValue = (decimal?)rowCovered["AmountValue"];
                                        var basicPrice = (decimal?)rowCovered["BasicPrice"];
                                        var coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                        basicPrice = Helper.Tariff.GetItemConditionRuleTariff(basicPrice ?? 0, detail.ItemConditionRuleID, tariffDate);
                                        coveragePrice = Helper.Tariff.GetItemConditionRuleTariff(coveragePrice ?? 0, detail.ItemConditionRuleID, tariffDate);

                                        if (rowCovered["SRGuarantorRuleType"].ToString().Equals(_guarantorRuleTypeDiscount))
                                        {
                                            if ((comp.Price + comp.CitoAmount - comp.DiscountAmount) <= 0)
                                                continue;

                                            var compPrice = comp.Price ?? 0;
                                            var compCitoAmt = compPrice * (detail.BasicCitoAmount ?? 0) / 100;
                                            if (basicPrice > coveragePrice)
                                            {
                                                var tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, grr.SRTariffType, coverageClassId /*reg.CoverageClassID*/, comp.TariffComponentID, detail.ItemID);
                                                if (!tcomp.AsEnumerable().Any())
                                                    tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, grr.SRTariffType, _defaultTariffClass, comp.TariffComponentID, detail.ItemID);
                                                if (!tcomp.AsEnumerable().Any())
                                                    tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, _defaultTariffType, coverageClassId /*reg.CoverageClassID*/, comp.TariffComponentID, detail.ItemID);
                                                if (!tcomp.AsEnumerable().Any())
                                                    tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, _defaultTariffType, _defaultTariffClass, comp.TariffComponentID, detail.ItemID);

                                                if (!tcomp.AsEnumerable().Any())
                                                    continue;

                                                compPrice = tcomp.AsEnumerable().Select(c => c.Field<decimal>("Price")).Single();
                                                compPrice = Helper.Tariff.GetItemConditionRuleTariff(compPrice, detail.ItemConditionRuleID, tariffDate);
                                                compCitoAmt = compPrice * (detail.BasicCitoAmount ?? 0) / 100;
                                            }

                                            if ((bool)rowCovered["IsValueInPercent"])
                                            {
                                                var discountRule = (amountValue / 100) * (compPrice + compCitoAmt);
                                                var fee = comp.CalculateParamedicPercentDiscount(
                                                    AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                                    header.RegistrationNo, detail.ItemID, discountRule,
                                                    AppSession.UserLogin.UserID, header.ClassID, header.ToServiceUnitID);
                                                comp.AutoProcessCalculation = 0 - comp.DiscountAmount;

                                                //comp.DiscountAmount = (amountValue / 100) * compPrice;
                                                //comp.AutoProcessCalculation = 0 - (amountValue / 100) * compPrice;
                                            }
                                            else
                                            {
                                                //if (!isDiscount)
                                                //{
                                                //    if (discount == 0)
                                                //    {
                                                if (detail.Price > compPrice)
                                                    amountValue = ((compPrice + compCitoAmt) / (detail.Price + (detail.CitoAmount / Math.Abs(detail.ChargeQuantity ?? 0)))) * amountValue;

                                                if (compPrice + compCitoAmt >= amountValue)
                                                {
                                                    var discountRule = amountValue;
                                                    var fee = comp.CalculateParamedicPercentDiscount(
                                                        AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                                        header.RegistrationNo, detail.ItemID, discountRule,
                                                        AppSession.UserLogin.UserID, header.ClassID, header.ToServiceUnitID);
                                                    comp.AutoProcessCalculation = 0 - comp.DiscountAmount;

                                                    //comp.DiscountAmount = amountValue;
                                                    //comp.AutoProcessCalculation = 0 - amountValue;
                                                    //isDiscount = true;
                                                }
                                                else
                                                {
                                                    var discountRule = compPrice + compCitoAmt;
                                                    var fee = comp.CalculateParamedicPercentDiscount(
                                                        AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                                        header.RegistrationNo, detail.ItemID, discountRule,
                                                        AppSession.UserLogin.UserID, header.ClassID, header.ToServiceUnitID);
                                                    comp.AutoProcessCalculation = 0 - comp.DiscountAmount;

                                                    //comp.DiscountAmount = compPrice;
                                                    //comp.AutoProcessCalculation = 0 - compPrice;
                                                    //discount = amountValue - compPrice;
                                                }
                                                //    }
                                                //    else
                                                //    {
                                                //        if (compPrice >= discount)
                                                //        {
                                                //            comp.DiscountAmount = discount;
                                                //            comp.AutoProcessCalculation = 0 - discount;
                                                //            isDiscount = true;
                                                //        }
                                                //        else
                                                //        {
                                                //            comp.DiscountAmount = compPrice;
                                                //            comp.AutoProcessCalculation = 0 - compPrice;
                                                //            discount -= compPrice;
                                                //        }
                                                //    }
                                                //}
                                            }
                                        }
                                        else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(_guarantorRuleTypeMargin))
                                        {
                                            if ((bool)rowCovered["IsValueInPercent"])
                                            {
                                                comp.AutoProcessCalculation = (amountValue / 100) * (comp.Price + comp.CitoAmount);
                                                comp.Price += (amountValue / 100) * comp.Price;
                                                comp.CitoAmount += (amountValue / 100) * comp.CitoAmount;

                                                var discountRule = 0;
                                                var fee = comp.CalculateParamedicPercentDiscount(
                                                    AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                                    header.RegistrationNo, detail.ItemID, discountRule,
                                                    AppSession.UserLogin.UserID, header.ClassID, header.ToServiceUnitID);
                                                comp.AutoProcessCalculation = comp.AutoProcessCalculation - comp.DiscountAmount;
                                            }
                                            else
                                            {
                                                if (!isMargin)
                                                {
                                                    comp.Price += amountValue;
                                                    comp.CitoAmount = comp.Price * (detail.BasicCitoAmount ?? 0) / 100;
                                                    comp.AutoProcessCalculation = amountValue + comp.CitoAmount;
                                                    isMargin = true;

                                                    var discountRule = 0;
                                                    var fee = comp.CalculateParamedicPercentDiscount(
                                                        AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                                        header.RegistrationNo, detail.ItemID, discountRule,
                                                        AppSession.UserLogin.UserID, header.ClassID, header.ToServiceUnitID);
                                                    comp.AutoProcessCalculation = amountValue - comp.DiscountAmount;
                                                }
                                            }
                                        }
                                    }

                                    comps.Save();
                                }

                                //TransChargesItems
                                if (comps.Count > 0)
                                {
                                    detail.AutoProcessCalculation = comps.Where(comp => comp.TransactionNo == detail.TransactionNo &&
                                                                                        comp.SequenceNo == detail.SequenceNo)
                                                                         .Sum(t => t.AutoProcessCalculation);
                                    if (detail.AutoProcessCalculation < 0)
                                    {
                                        detail.DiscountAmount += Math.Abs(detail.ChargeQuantity ?? 0) * Math.Abs(detail.AutoProcessCalculation ?? 0);

                                        if (detail.DiscountAmount > (detail.Price * Math.Abs(detail.ChargeQuantity ?? 0)) + detail.CitoAmount)
                                        {
                                            detail.DiscountAmount = (detail.Price * Math.Abs(detail.ChargeQuantity ?? 0)) + detail.CitoAmount;
                                            detail.AutoProcessCalculation = 0 - (detail.Price + (detail.CitoAmount / Math.Abs(detail.ChargeQuantity ?? 0)));
                                        }
                                    }
                                    else if (detail.AutoProcessCalculation > 0)
                                        detail.Price += detail.AutoProcessCalculation;
                                }
                                else
                                {
                                    if (rowCovered != null)
                                    {
                                        if (rowCovered["SRGuarantorRuleType"].ToString().Equals(_guarantorRuleTypeDiscount))
                                        {
                                            var basicPrice = (decimal?)rowCovered["BasicPrice"];
                                            var coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                            basicPrice = Helper.Tariff.GetItemConditionRuleTariff(basicPrice ?? 0, detail.ItemConditionRuleID, tariffDate);
                                            coveragePrice = Helper.Tariff.GetItemConditionRuleTariff(coveragePrice ?? 0, detail.ItemConditionRuleID, tariffDate);

                                            var detailPrice = detail.Price ?? 0;
                                            if (basicPrice > coveragePrice)
                                            {
                                                ItemTariff itariff = (Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, coverageClassId /*reg.CoverageClassID*/, coverageClassId /*reg.CoverageClassID*/, detail.ItemID, reg.GuarantorID, false, (isUsingParentRegType ? reg.SRRegistrationType : regHeader.SRRegistrationType)) ??
                                                     Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, _defaultTariffClass, coverageClassId /*reg.CoverageClassID*/, detail.ItemID, reg.GuarantorID, false, (isUsingParentRegType ? reg.SRRegistrationType : regHeader.SRRegistrationType))) ??
                                                     (Helper.Tariff.GetItemTariff(tariffDate, _defaultTariffType, coverageClassId /*reg.CoverageClassID*/, coverageClassId /*reg.CoverageClassID*/, detail.ItemID, reg.GuarantorID, false, (isUsingParentRegType ? reg.SRRegistrationType : regHeader.SRRegistrationType)) ??
                                                     Helper.Tariff.GetItemTariff(tariffDate, _defaultTariffType, _defaultTariffClass, coverageClassId /*reg.CoverageClassID*/, detail.ItemID, reg.GuarantorID, false, (isUsingParentRegType ? reg.SRRegistrationType : regHeader.SRRegistrationType)));
                                                if (itariff != null)
                                                {
                                                    //detailPrice = itariff.Price ?? 0;
                                                    detailPrice = Helper.Tariff.GetItemConditionRuleTariff(itariff.Price ?? 0, detail.ItemConditionRuleID, tariffDate);
                                                }
                                            }

                                            if ((bool)rowCovered["IsValueInPercent"])
                                            {
                                                detail.DiscountAmount = Math.Abs(detail.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * detailPrice);
                                                detail.AutoProcessCalculation = 0 - (((decimal)rowCovered["AmountValue"] / 100) * detailPrice);
                                            }
                                            else
                                            {
                                                detail.DiscountAmount = (detail.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];
                                                detail.AutoProcessCalculation = 0 - (decimal)rowCovered["AmountValue"];
                                            }

                                            if (detail.DiscountAmount > (detailPrice * Math.Abs(detail.ChargeQuantity ?? 0)))
                                                detail.DiscountAmount = (detailPrice * Math.Abs(detail.ChargeQuantity ?? 0));
                                        }
                                        else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(_guarantorRuleTypeMargin))
                                        {
                                            if ((bool)rowCovered["IsValueInPercent"])
                                            {
                                                detail.AutoProcessCalculation = ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;
                                                detail.Price += ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;
                                            }
                                            else
                                            {
                                                detail.Price += (decimal)rowCovered["AmountValue"];
                                                detail.AutoProcessCalculation = (decimal)rowCovered["AmountValue"];
                                            }
                                            detail.DiscountAmount = 0;
                                        }
                                        else
                                            detail.DiscountAmount = 0;
                                    }
                                }

                                detail.Save();

                                //post
                                decimal? total = ((Math.Abs(detail.ChargeQuantity ?? 0) * detail.Price) - detail.DiscountAmount) + detail.CitoAmount;

                                var calc = new Helper.CostCalculation(grrID, detail.ItemID, total ?? 0, tblCovered,
                                                                      Math.Abs(detail.ChargeQuantity ?? 0),
                                                                      detail.IsCito ?? false,
                                                                      detail.IsCitoInPercent ?? false,
                                                                      detail.BasicCitoAmount ?? 0, detail.Price ?? 0,
                                                                      header.IsRoomIn ?? false, detail.IsItemRoom ?? false,
                                                                      header.TariffDiscountForRoomIn ?? 0, detail.DiscountAmount ?? 0,
                                                                      reg.IsGlobalPlafond ?? false,
                                                                      detail.ItemConditionRuleID, tariffDate, detail.IsVariable ?? false);

                                if (operatingRoom != null && !string.IsNullOrEmpty(header.SurgicalPackageID))
                                {
                                    var package = new GuarantorSurgicalPackageCoveredItem();
                                    if (package.LoadByPrimaryKey(grrID, header.SurgicalPackageID, detail.ItemID))
                                    {
                                        if (calc.PatientAmount + calc.GuarantorAmount <= package.CoveredAmount)
                                        {
                                            calc.GuarantorAmount = calc.PatientAmount + calc.GuarantorAmount;
                                            calc.PatientAmount = 0;
                                        }
                                        else
                                        {
                                            calc.PatientAmount = calc.PatientAmount + calc.GuarantorAmount - package.CoveredAmount ?? 0;
                                            calc.GuarantorAmount = package.CoveredAmount ?? 0;
                                        }
                                    }
                                }

                                //CostCalculations
                                //db: 20231218 - cc gak langsung add, tp update dari yg udah ada, kalo gak ketemu baru di add
                                try
                                {
                                    var cost = (CostCalculations.Where(b => b.RegistrationNo == row["RegistrationNo"].ToString() && b.TransactionNo == detail.TransactionNo && b.SequenceNo == detail.SequenceNo)).Take(1).Single();
                                    //cost.RegistrationNo = row["RegistrationNo"].ToString();
                                    //cost.TransactionNo = detail.TransactionNo;
                                    //cost.SequenceNo = detail.SequenceNo;
                                    cost.ItemID = detail.ItemID;
                                    //start here
                                    decimal? totaltrans = calc.GuarantorAmount + calc.PatientAmount + (detail.DiscountAmount ?? 0);
                                    decimal? totaldisc = detail.DiscountAmount ?? 0;

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
                                            cost.DiscountAmount = totaldisc > calc.GuarantorAmount + detail.DiscountAmount
                                                                       ? calc.GuarantorAmount + detail.DiscountAmount
                                                                       : totaldisc;

                                            cost.GuarantorAmount = totaldisc > calc.GuarantorAmount + detail.DiscountAmount
                                                                       ? 0
                                                                       : calc.GuarantorAmount + detail.DiscountAmount - totaldisc;
                                            cost.PatientAmount = calc.PatientAmount;

                                        }
                                        else
                                        {
                                            cost.DiscountAmount = totaldisc > calc.PatientAmount + detail.DiscountAmount
                                                                      ? calc.PatientAmount + detail.DiscountAmount
                                                                      : totaldisc;

                                            cost.PatientAmount = totaldisc > calc.PatientAmount + detail.DiscountAmount
                                                                     ? 0
                                                                     : calc.PatientAmount + detail.DiscountAmount - totaldisc;
                                            cost.GuarantorAmount = calc.GuarantorAmount;
                                        }

                                        if (totaldisc > cost.DiscountAmount)
                                        {
                                            //hitung ulang diskon di TransChargesItem & TransChargesItemComp
                                            var compColl = comps.Where(
                                                    t =>
                                                    t.TransactionNo == detail.TransactionNo &&
                                                    t.SequenceNo == detail.SequenceNo)
                                                    .OrderBy(t => t.TariffComponentID);
                                            var i = compColl.Count();

                                            foreach (var compEntity in compColl)
                                            {
                                                compEntity.DiscountAmount = i == 1
                                                                           ? (cost.DiscountAmount / Math.Abs(detail.ChargeQuantity ?? 0))
                                                                           : (compEntity.Price + compEntity.CitoAmount) * (cost.DiscountAmount / detail.DiscountAmount);

                                                var fee = compEntity.CalculateParamedicPercentDiscount(
                                                    AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                                    cost.RegistrationNo, detail.ItemID, (compEntity.DiscountAmount ?? 0),
                                                    AppSession.UserLogin.UserID, header.ClassID, header.ToServiceUnitID);

                                            }

                                            comps.Save();

                                            detail.DiscountAmount = cost.DiscountAmount;
                                            detail.Save();
                                        }
                                    }
                                    //end

                                    cost.PatientAmount = (detail.ChargeQuantity < 0) ? 0 - cost.PatientAmount : cost.PatientAmount;
                                    cost.GuarantorAmount = (detail.ChargeQuantity < 0) ? 0 - cost.GuarantorAmount : cost.GuarantorAmount;
                                    cost.DiscountAmount = (detail.ChargeQuantity < 0) ? 0 - cost.DiscountAmount : cost.DiscountAmount;
                                    cost.DiscountAmount2 = 0;
                                    cost.IsPackage = detail.IsPackage;
                                    cost.ParentNo = detail.ParentNo;
                                    cost.ParamedicAmount = detail.ChargeQuantity * comps.Where(comp => comp.TransactionNo == detail.TransactionNo &&
                                                                                                       comp.SequenceNo == detail.SequenceNo &&
                                                                                                       !string.IsNullOrEmpty(comp.ParamedicID))
                                                                                        .Sum(comp => comp.Price - comp.DiscountAmount);
                                    cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    cost.LastUpdateByUserID = AppSession.UserLogin.UserID;

                                    cost.TransactionDate = transDate;
                                    cost.ReferenceNo = header.ReferenceNo;
                                    var su = new ServiceUnit();
                                    if (su.LoadByPrimaryKey(header.ToServiceUnitID))
                                        cost.ServiceUnitName = su.ServiceUnitName;
                                    var item = new Item();
                                    if (item.LoadByPrimaryKey(cost.ItemID))
                                        cost.ItemName = string.IsNullOrEmpty(detail.ParamedicCollectionName)
                                                            ? item.ItemName
                                                            : item.ItemName + " (" + detail.ParamedicCollectionName + ")";
                                    var cls = new Class();
                                    if (cls.LoadByPrimaryKey(header.ClassID))
                                        cost.ClassName = cls.ClassName;
                                }
                                catch (Exception)
                                {
                                    var cost = CostCalculations.AddNew();
                                    cost.RegistrationNo = row["RegistrationNo"].ToString();
                                    cost.TransactionNo = detail.TransactionNo;
                                    cost.SequenceNo = detail.SequenceNo;
                                    cost.ItemID = detail.ItemID;
                                    //start here
                                    decimal? totaltrans = calc.GuarantorAmount + calc.PatientAmount + (detail.DiscountAmount ?? 0);
                                    decimal? totaldisc = detail.DiscountAmount ?? 0;

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
                                            cost.DiscountAmount = totaldisc > calc.GuarantorAmount + detail.DiscountAmount
                                                                       ? calc.GuarantorAmount + detail.DiscountAmount
                                                                       : totaldisc;

                                            cost.GuarantorAmount = totaldisc > calc.GuarantorAmount + detail.DiscountAmount
                                                                       ? 0
                                                                       : calc.GuarantorAmount + detail.DiscountAmount - totaldisc;
                                            cost.PatientAmount = calc.PatientAmount;

                                        }
                                        else
                                        {
                                            cost.DiscountAmount = totaldisc > calc.PatientAmount + detail.DiscountAmount
                                                                      ? calc.PatientAmount + detail.DiscountAmount
                                                                      : totaldisc;

                                            cost.PatientAmount = totaldisc > calc.PatientAmount + detail.DiscountAmount
                                                                     ? 0
                                                                     : calc.PatientAmount + detail.DiscountAmount - totaldisc;
                                            cost.GuarantorAmount = calc.GuarantorAmount;
                                        }

                                        if (totaldisc > cost.DiscountAmount)
                                        {
                                            //hitung ulang diskon di TransChargesItem & TransChargesItemComp
                                            var compColl = comps.Where(
                                                    t =>
                                                    t.TransactionNo == detail.TransactionNo &&
                                                    t.SequenceNo == detail.SequenceNo)
                                                    .OrderBy(t => t.TariffComponentID);
                                            var i = compColl.Count();

                                            foreach (var compEntity in compColl)
                                            {
                                                compEntity.DiscountAmount = i == 1
                                                                           ? (cost.DiscountAmount / Math.Abs(detail.ChargeQuantity ?? 0))
                                                                           : (compEntity.Price + compEntity.CitoAmount) * (cost.DiscountAmount / detail.DiscountAmount);

                                                var fee = compEntity.CalculateParamedicPercentDiscount(
                                                    AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                                    cost.RegistrationNo, detail.ItemID, (compEntity.DiscountAmount ?? 0),
                                                    AppSession.UserLogin.UserID, header.ClassID, header.ToServiceUnitID);

                                            }

                                            comps.Save();

                                            detail.DiscountAmount = cost.DiscountAmount;
                                            detail.Save();
                                        }
                                    }
                                    //end

                                    cost.PatientAmount = (detail.ChargeQuantity < 0) ? 0 - cost.PatientAmount : cost.PatientAmount;
                                    cost.GuarantorAmount = (detail.ChargeQuantity < 0) ? 0 - cost.GuarantorAmount : cost.GuarantorAmount;
                                    cost.DiscountAmount = (detail.ChargeQuantity < 0) ? 0 - cost.DiscountAmount : cost.DiscountAmount;
                                    cost.DiscountAmount2 = 0;
                                    cost.IsPackage = detail.IsPackage;
                                    cost.ParentNo = detail.ParentNo;
                                    cost.ParamedicAmount = detail.ChargeQuantity * comps.Where(comp => comp.TransactionNo == detail.TransactionNo &&
                                                                                                       comp.SequenceNo == detail.SequenceNo &&
                                                                                                       !string.IsNullOrEmpty(comp.ParamedicID))
                                                                                        .Sum(comp => comp.Price - comp.DiscountAmount);
                                    cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    cost.LastUpdateByUserID = AppSession.UserLogin.UserID;

                                    cost.TransactionDate = transDate;
                                    cost.ReferenceNo = header.ReferenceNo;
                                    var su = new ServiceUnit();
                                    if (su.LoadByPrimaryKey(header.ToServiceUnitID))
                                        cost.ServiceUnitName = su.ServiceUnitName;
                                    var item = new Item();
                                    if (item.LoadByPrimaryKey(cost.ItemID))
                                        cost.ItemName = string.IsNullOrEmpty(detail.ParamedicCollectionName)
                                                            ? item.ItemName
                                                            : item.ItemName + " (" + detail.ParamedicCollectionName + ")";
                                    var cls = new Class();
                                    if (cls.LoadByPrimaryKey(header.ClassID))
                                        cost.ClassName = cls.ClassName;
                                }

                                if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                                {
                                    // extract fee
                                    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                                    feeColl.SetFeeByTCIC(comps, AppSession.UserLogin.UserID);
                                    feeColl.Save();
                                    //feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                                    //feeColl.Save();
                                }
                            }
                            break;
                        case "2":
                            var hd = new TransPrescription();
                            hd.LoadByPrimaryKey(row["TransactionNo"].ToString());

                            var regHd = new Registration();
                            regHd.LoadByPrimaryKey(hd.RegistrationNo);
                            coverageClassId = regHd.CoverageClassID;

                            DateTime prescDate = hd.PrescriptionDate.Value.Date;

                            if (hd.IsPrescriptionReturn ?? false)
                            {
                                var hdRef = new TransPrescription();
                                if (hdRef.LoadByPrimaryKey(hd.ReferenceNo))
                                {
                                    prescDate = hdRef.PrescriptionDate.Value.Date;

                                    regHd = new Registration();
                                    regHd.LoadByPrimaryKey(hdRef.RegistrationNo);
                                }
                            }

                            var presc = new TransPrescriptionItem();
                            if (presc.LoadByPrimaryKey(row["TransactionNo"].ToString(), row["SequenceNo"].ToString()))
                            {
                                //start recalculation
                                //presc.IsBillProceed = true;

                                //cek apakah rekalkulasi hitung ulang price
                                if (AppSession.Parameter.IsUpdatePrescriptionPriceWhenRecal)
                                {
                                    //kalo parameter RecipeMarginValueCompound > 0, untuk margin tidak ditambah margin per item tapi lgsg ditambah margin parameter value tsb
                                    if (presc.IsCompound == true && AppSession.Parameter.RecipeMarginValueCompound != 0)
                                    {
                                        presc.Price = Helper.Tariff.GetItemTariffNonMargin(grr.SRTariffType, prescDate,
                                            hd.ClassID, string.IsNullOrEmpty(presc.ItemInterventionID) ? presc.ItemID : presc.ItemInterventionID,
                                            presc.IsCompound ?? false, presc.SRItemUnit);
                                        presc.Price += Convert.ToDecimal(AppSession.Parameter.RecipeMarginValueCompound / 100) * presc.Price;
                                    }
                                    else
                                    {
                                        presc.Price = Helper.Tariff.GetItemTariff(grr.SRTariffType, prescDate,
                                            hd.ClassID, string.IsNullOrEmpty(presc.ItemInterventionID) ? presc.ItemID : presc.ItemInterventionID,
                                            presc.IsCompound ?? false, presc.SRItemUnit, reg.GuarantorID, (isUsingParentRegTypeDefault ? reg.SRRegistrationType : regHd.SRRegistrationType));
                                    }
                                }

                                var recipe = new TransPrescription();
                                presc.RecipeAmount = (decimal)recipe.RecipeAmount(null, reg.GuarantorID, presc.ItemID, presc.ResultQty ?? 0, presc.ParentNo, presc.ItemQtyInString, presc.IsCompound ?? false);
                                decimal resultQty = presc.ResultQty ?? 0;
                                decimal recipeAmount = (presc.RecipeAmount ?? 0) + (presc.EmbalaceAmount ?? 0) + (presc.SweetenerAmount ?? 0);

                                if (hd.IsPrescriptionReturn ?? false)
                                {
                                    var prescI = new TransPrescriptionItem();
                                    if (prescI.LoadByPrimaryKey(hd.ReferenceNo, presc.SequenceNo))
                                    {
                                        var recipeAmountI = (prescI.RecipeAmount ?? 0) + (prescI.EmbalaceAmount ?? 0) + (prescI.SweetenerAmount ?? 0);
                                        recipeAmount = (recipeAmountI / (prescI.ResultQty ?? 0)) * Math.Abs(presc.ResultQty ?? 0);
                                        presc.RecipeAmount = recipeAmount;
                                    }
                                }

                                decimal rPrice = 0;
                                if (resultQty != 0)
                                {
                                    rPrice = recipeAmount;
                                }
                                bool IsIncR = false;

                                DataTable tblCovered2 = Helper.GetCoveredItems(txtRegistrationNo.Text, grrID, coverageClassId /*reg.CoverageClassID*/,
                                                                            string.IsNullOrEmpty(presc.ItemInterventionID) ? presc.ItemID : presc.ItemInterventionID,
                                                                            prescDate, true);

                                var rowCovered = tblCovered2.AsEnumerable().SingleOrDefault(t => t.Field<string>("ItemID") == (string.IsNullOrEmpty(presc.ItemInterventionID) ? presc.ItemID : presc.ItemInterventionID) &&
                                                                                                 t.Field<bool>("IsInclude"));
                                if (rowCovered != null)
                                {
                                    IsIncR = (AppSession.Parameter.IsPrescriptionDiscountIncludeR);

                                    if (rowCovered["SRGuarantorRuleType"].ToString().Equals(_guarantorRuleTypeDiscount))
                                    {
                                        if ((bool)rowCovered["IsValueInPercent"])
                                        {
                                            decimal _lineAmt = ((Math.Abs(resultQty) * presc.Price ?? 0) + (IsIncR ? rPrice : 0));
                                            if (AppSession.Parameter.IsPrescriptionDiscountAfterRounding)
                                                _lineAmt = Helper.Rounding(_lineAmt, AppEnum.RoundingType.Prescription);

                                            presc.DiscountAmount = _lineAmt * ((decimal)rowCovered["AmountValue"] / 100);
                                            //presc.DiscountAmount = ((Math.Abs(resultQty) * presc.Price) + (IsIncR ? rPrice : 0)) * ((decimal)rowCovered["AmountValue"] / 100);
                                            presc.AutoProcessCalculation = 0 - (((decimal)rowCovered["AmountValue"] / 100) * (presc.Price + (IsIncR ? (rPrice / Math.Abs(resultQty == 0 ? 1 : resultQty)) : 0)));
                                        }
                                        else
                                        {
                                            presc.DiscountAmount = Math.Abs(resultQty) * (decimal)rowCovered["AmountValue"];
                                            presc.AutoProcessCalculation = 0 - (decimal)rowCovered["AmountValue"];
                                        }

                                        if (presc.DiscountAmount > (Math.Abs(resultQty) * presc.Price) + (IsIncR ? rPrice : 0))
                                            presc.DiscountAmount = (Math.Abs(resultQty) * presc.Price) + (IsIncR ? rPrice : 0);
                                    }
                                    else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(_guarantorRuleTypeMargin))
                                    {
                                        if ((bool)rowCovered["IsValueInPercent"])
                                        {
                                            presc.AutoProcessCalculation = ((decimal)rowCovered["AmountValue"] / 100) * presc.Price;
                                            presc.Price += ((decimal)rowCovered["AmountValue"] / 100) * presc.Price;

                                        }
                                        else
                                        {
                                            presc.Price += (decimal)rowCovered["AmountValue"];
                                            presc.AutoProcessCalculation = (decimal)rowCovered["AmountValue"];
                                        }
                                        presc.DiscountAmount = 0;
                                    }
                                    else
                                        presc.DiscountAmount = 0;
                                }

                                decimal? lineAmt = 0;
                                //lineAmt = ((Math.Abs(resultQty) * presc.Price) - presc.DiscountAmount + recipeAmount);
                                //if (presc.IsUsingAdminReturn ?? false)
                                //{
                                //    lineAmt = lineAmt - (lineAmt * (AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) / 100);
                                //}

                                //lineAmt = Helper.Rounding(Convert.ToDecimal(lineAmt), AppEnum.RoundingType.Prescription);

                                if (presc.IsUsingAdminReturn ?? false)
                                {
                                    if (AppSession.Parameter.IsUpdatePrescriptionPriceWhenRecal)
                                        presc.Price = presc.Price - (((AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) / 100) * presc.Price);
                                    presc.DiscountAmount = presc.DiscountAmount - (presc.DiscountAmount * (AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) / 100);
                                }

                                lineAmt = ((Math.Abs(resultQty) * presc.Price) + recipeAmount);

                                if (AppSession.Parameter.IsPrescriptionDiscountAfterRounding)
                                    lineAmt = Helper.Rounding(lineAmt ?? 0, AppEnum.RoundingType.Prescription) - presc.DiscountAmount;
                                else
                                    lineAmt = Helper.Rounding((lineAmt ?? 0) - (presc.DiscountAmount ?? 0), AppEnum.RoundingType.Prescription);

                                presc.LineAmount = resultQty < 0 ? 0 - lineAmt : lineAmt;
                                presc.Save();

                                var calc = new Helper.CostCalculation(grrID, reg.IsGlobalPlafond ?? false,
                                    string.IsNullOrEmpty(presc.ItemInterventionID) ? presc.ItemID : presc.ItemInterventionID, Math.Abs(presc.LineAmount ?? 0),
                                    tblCovered2, Math.Abs(resultQty), presc.Price ?? 0, recipeAmount, presc.DiscountAmount ?? 0);

                                //db:20231218 - cc gak langsung add, tp update dari yg udah ada, kalo gak ketemu baru di add
                                try
                                {
                                    var cost = (CostCalculations.Where(b => b.RegistrationNo == row["RegistrationNo"].ToString() && b.TransactionNo == presc.PrescriptionNo && b.SequenceNo == presc.SequenceNo)).Take(1).Single();
                                    //cost.RegistrationNo = row["RegistrationNo"].ToString();
                                    //cost.TransactionNo = presc.PrescriptionNo;
                                    //cost.SequenceNo = presc.SequenceNo;
                                    cost.ItemID = string.IsNullOrEmpty(presc.ItemInterventionID) ? presc.ItemID : presc.ItemInterventionID;
                                    cost.PatientAmount = resultQty < 0 ? 0 - calc.PatientAmount : calc.PatientAmount;
                                    cost.GuarantorAmount = resultQty < 0 ? 0 - calc.GuarantorAmount : calc.GuarantorAmount;
                                    if (reg.SRBussinesMethod == AppSession.Parameter.BusinessMethodFlavon)
                                    {
                                        cost.GuarantorAmount = cost.GuarantorAmount + cost.PatientAmount;
                                        cost.PatientAmount = 0;
                                    }
                                    cost.DiscountAmount = resultQty < 0 ? 0 - presc.DiscountAmount : presc.DiscountAmount;
                                    cost.DiscountAmount2 = 0;
                                    cost.ParamedicAmount = 0;
                                    cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    cost.LastUpdateByUserID = AppSession.UserLogin.UserID;

                                    cost.TransactionDate = prescDate;
                                    cost.ReferenceNo = hd.ReferenceNo;
                                    var su = new ServiceUnit();
                                    if (su.LoadByPrimaryKey(hd.ServiceUnitID))
                                        cost.ServiceUnitName = su.ServiceUnitName;
                                    var item = new Item();
                                    if (item.LoadByPrimaryKey(cost.ItemID))
                                        cost.ItemName = item.ItemName;
                                    var cls = new Class();
                                    if (cls.LoadByPrimaryKey(hd.ClassID))
                                        cost.ClassName = cls.ClassName;

                                }
                                catch (Exception)
                                {
                                    var cost = CostCalculations.AddNew();
                                    cost.RegistrationNo = row["RegistrationNo"].ToString();
                                    cost.TransactionNo = presc.PrescriptionNo;
                                    cost.SequenceNo = presc.SequenceNo;
                                    cost.ItemID = string.IsNullOrEmpty(presc.ItemInterventionID) ? presc.ItemID : presc.ItemInterventionID;
                                    cost.PatientAmount = resultQty < 0 ? 0 - calc.PatientAmount : calc.PatientAmount;
                                    cost.GuarantorAmount = resultQty < 0 ? 0 - calc.GuarantorAmount : calc.GuarantorAmount;
                                    if (reg.SRBussinesMethod == AppSession.Parameter.BusinessMethodFlavon)
                                    {
                                        cost.GuarantorAmount = cost.GuarantorAmount + cost.PatientAmount;
                                        cost.PatientAmount = 0;
                                    }
                                    cost.DiscountAmount = resultQty < 0 ? 0 - presc.DiscountAmount : presc.DiscountAmount;
                                    cost.DiscountAmount2 = 0;
                                    cost.ParamedicAmount = 0;
                                    cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    cost.LastUpdateByUserID = AppSession.UserLogin.UserID;

                                    cost.TransactionDate = prescDate;
                                    cost.ReferenceNo = hd.ReferenceNo;
                                    var su = new ServiceUnit();
                                    if (su.LoadByPrimaryKey(hd.ServiceUnitID))
                                        cost.ServiceUnitName = su.ServiceUnitName;
                                    var item = new Item();
                                    if (item.LoadByPrimaryKey(cost.ItemID))
                                        cost.ItemName = item.ItemName;
                                    var cls = new Class();
                                    if (cls.LoadByPrimaryKey(hd.ClassID))
                                        cost.ClassName = cls.ClassName;
                                }
                            }
                            break;
                    }
                }

                CostCalculations.Save();
                #endregion

                #region jurnal
                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsAutoJournalFinalizeBilling) == "Yes" && AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                {
                    if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                    {
                        JournalTransactions.AddNewPatientBillingRecalculation(BusinessObject.JournalType.Income, txtRegistrationNo.Text, (new DateTime()).NowAtSqlServer(), AppSession.UserLogin.UserID, 0);
                    }
                    else
                    {
                        foreach (var item in CostCalculations.Select(c => c.TransactionNo).Distinct())
                        {
                            var coll = CostCalculations.Where(c => c.TransactionNo == item);
                            //link ke cc history 
                            //foreach (var cc in coll)
                            //{
                            var ccHDesc = costHistory.Where(c => c.TransactionNo == item).OrderByDescending(c => c.RecalculationProcessNo).Take(1).SingleOrDefault();
                            var ccH = costHistory.Where(c => c.RecalculationProcessNo == ccHDesc.RecalculationProcessNo && c.TransactionNo == ccHDesc.TransactionNo);
                            if (ccH != null)
                            {
                                var CompH = chargesItemCompHistory.Where(c => c.RecalculationProcessNo == ccHDesc.RecalculationProcessNo && c.TransactionNo == ccHDesc.TransactionNo);
                                if (coll.Select(c => c.PatientAmount).Sum() != ccH.Select(c => c.PatientAmount).Sum() ||
                                    coll.Select(c => c.GuarantorAmount).Sum() != ccH.Select(c => c.GuarantorAmount).Sum() ||
                                    coll.Select(c => c.DiscountAmount).Sum() != ccH.Select(c => c.DiscountAmount).Sum())
                                {

                                    //exec jurnal(coll)
                                    var entity = charges.Where(c => c.TransactionNo == item).SingleOrDefault();

                                    var presc = prescriptions.Where(c => c.PrescriptionNo == item).SingleOrDefault();

                                    if (entity != null)
                                    {
                                        var closingperiod = entity.TransactionDate;
                                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod.Value);
                                        if (isClosingPeriod)
                                        {
                                            throw new Exception("Financial statements for period: " +
                                                                string.Format("{0:MMMM-yyyy}", closingperiod) +
                                                                " have been closed. Please contact the authorities.");
                                            return;
                                        }

                                        // hrs deklarasi baru karena kemungkinan ada transferan dr RJ/RD sehingga no registrasi lbh dari 1
                                        var regs = new Registration();
                                        regs.LoadByPrimaryKey(entity.RegistrationNo);

                                        var unit = new ServiceUnit();
                                        unit.LoadByPrimaryKey(entity.ToServiceUnitID);

                                        var compsNow = new TransChargesItemCompCollection();
                                        compsNow.Query.Where(compsNow.Query.TransactionNo == item);
                                        compsNow.LoadAll();

                                        if (entity.IsCorrection == true)
                                        {

                                            int? journalId = JournalTransactions.AddNewIncomeCorrectionVerificationJournal(entity, compsNow, CompH, regs, unit, coll, ccH, "SC",
                                                AppSession.UserLogin.UserID, false);
                                        }
                                        else
                                        {
                                            if (entity.IsOrder == true)
                                            {
                                                int? journalId = JournalTransactions.AddNewIncomeVerificationJournal(entity, compsNow, CompH, regs, unit, coll, ccH, "JO",
                                                AppSession.UserLogin.UserID);
                                            }
                                            else
                                            {

                                                int? journalId = JournalTransactions.AddNewIncomeVerificationJournal(entity, compsNow, CompH, regs, unit, coll, ccH, "SU",
                                                 AppSession.UserLogin.UserID);
                                            }
                                        }
                                    }
                                    if (presc != null)
                                    {
                                        var closingperiod = presc.PrescriptionDate;
                                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod.Value);
                                        if (isClosingPeriod)
                                        {
                                            throw new Exception("Financial statements for period: " +
                                                                string.Format("{0:MMMM-yyyy}", closingperiod) +
                                                                " have been closed. Please contact the authorities.");
                                            return;
                                        }

                                        var regs = new Registration();
                                        regs.LoadByPrimaryKey(presc.RegistrationNo);

                                        var unit = new ServiceUnit();
                                        unit.LoadByPrimaryKey(presc.ServiceUnitID);


                                        if (presc.IsPrescriptionReturn == false)
                                        {
                                            int? journalId = JournalTransactions.AddNewPrescriptionVerificationJournal(presc, regs, unit, coll, ccH, "RS",
                                                 AppSession.UserLogin.UserID);
                                        }
                                        else
                                        {
                                            int? journalId = JournalTransactions.AddNewPrescriptionReturnVerificationJournal(presc, regs, unit, coll, ccH, "RS",
                                                AppSession.UserLogin.UserID);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                trans.Complete();
            }
        }

        private bool Save()
        {
            //registration
            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            var guarantorIdBefore = reg.GuarantorID;

            bool updateGuarantor = reg.GuarantorID != cboGuarantorID.SelectedValue;
            bool updatePlafond = reg.PlavonAmount != Convert.ToDecimal(txtPlafonValue.Value);

            var hist = new RegistrationGuarantorHistory();
            hist.AddNew();
            hist.RegistrationNo = reg.RegistrationNo;
            hist.FromGuarantorID = reg.GuarantorID;
            hist.ToGuarantorID = cboGuarantorID.SelectedValue;
            hist.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            hist.LastUpdateByUserID = AppSession.UserLogin.UserID;

            var plafondHist = new RegistrationPlafondHistory();
            plafondHist.AddNew();
            plafondHist.RegistrationNo = reg.RegistrationNo;
            plafondHist.GuarantorID = cboGuarantorID.SelectedValue;
            plafondHist.PlafondAmount = Convert.ToDecimal(txtPlafonValue.Value);
            plafondHist.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            plafondHist.LastUpdateByUserID = AppSession.UserLogin.UserID;

            //guarantor
            var grr = new Guarantor();
            grr.LoadByPrimaryKey(cboGuarantorID.SelectedValue);

            reg.GuarantorID = cboGuarantorID.SelectedValue;
            reg.CoverageClassID = cboCoverageClassID.SelectedValue;
            reg.TransactionAmount = CostCalculations.Sum(c => c.PatientAmount + c.GuarantorAmount);
            reg.SRBussinesMethod = cboSRBusinessMethod.SelectedValue;
            reg.PlavonAmount = Convert.ToDecimal(txtPlafonValue.Value);
            //reg.PlavonAmount2 = Convert.ToDecimal(txtPlavonChargeValue.Value);
            reg.IsGlobalPlafond = chkIsGlobalFlavon.Checked;

            if (!string.IsNullOrEmpty(cboEmployeeID.SelectedValue))
            {
                reg.PersonID = Convert.ToInt32(cboEmployeeID.SelectedValue);
                var pInfo = new PersonalInfo();
                pInfo.LoadByPrimaryKey(Convert.ToInt32(cboEmployeeID.SelectedValue));
                reg.EmployeeNumber = pInfo.EmployeeNumber;
            }
            else
            {
                reg.PersonID = null;
                reg.EmployeeNumber = null;
            }
            reg.SREmployeeRelationship = cboGuarSRRelationship.SelectedValue;

            if (!_isUsingIntermBill)
            {
                if (grr.IsAdminFromTotal ?? false)
                    reg.AdministrationAmount = Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations.Sum(c => c.PatientAmount + c.GuarantorAmount) ?? 0, reg.SRRegistrationType);
                else
                    reg.AdministrationAmount = Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations, reg.SRRegistrationType);
            }
            else
            {
                var ibq = new IntermBillQuery("a");
                ibq.Where(ibq.RegistrationNo == txtRegistrationNo.Text, ibq.IsVoid == false);
                ibq.Select(ibq.RegistrationNo, @"<SUM(ISNULL(a.AdministrationAmount, 0)) AS AdministrationAmount>");
                ibq.GroupBy(ibq.RegistrationNo);

                DataTable dtb = ibq.LoadDataTable();
                reg.AdministrationAmount = dtb.Rows.Count > 0 ? dtb.AsEnumerable().Sum(t => t.Field<decimal>("AdministrationAmount")) : 0;
            }

            reg.RemainingAmount = (reg.TransactionAmount ?? 0) - Helper.Payment.GetTotalPayment(MergeRegistrationList());

            if (pnlProcedureClass.Visible && cboProcedureClassID.Enabled)
                reg.ProcedureChargeClassID = cboProcedureClassID.SelectedValue;

            //reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
            //reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (var trans = new esTransactionScope())
            {
                reg.Save();
                if (updateGuarantor)
                {
                    hist.Save();
                    var pat = new Patient();
                    pat.LoadByPrimaryKey(reg.PatientID);
                    pat.GuarantorID = reg.GuarantorID;
                    pat.Save();
                }

                if (updateGuarantor && !string.IsNullOrEmpty(reg.MembershipNo))
                {
                    var x = BusinessObject.MembershipDetail.EmployeeRefferalRewardPoints(reg.MembershipNo, reg.RegistrationNo, reg.RegistrationDate ?? (new DateTime()).NowAtSqlServer(),
                            guarantorIdBefore, AppSession.Parameter.GuarantorTypeSelf, AppSession.Parameter.RewardPointsForPatientGeneral, AppSession.Parameter.RewardPointsForPatientGuarantee,
                            AppSession.UserLogin.UserID, false, reg.GuarantorID, string.Empty);
                }

                if (updatePlafond)
                    plafondHist.Save();

                string[] merge = MergeRegistrationList();
                foreach (var mrg in from s in merge let mrg = new Registration() where mrg.LoadByPrimaryKey(s) select mrg)
                {
                    mrg.GuarantorID = cboGuarantorID.SelectedValue;
                    mrg.SRBussinesMethod = cboSRBusinessMethod.SelectedValue;
                    mrg.PersonID = reg.PersonID;
                    mrg.EmployeeNumber = reg.EmployeeNumber;
                    mrg.Save();
                }

                trans.Complete();
            }

            return updateGuarantor;
        }

        private void Lock()
        {
            var r = new Registration();
            r.LoadByPrimaryKey(txtRegistrationNo.Text);
            bool statusNow = (r.IsHoldTransactionEntry ?? false);

            using (var trans = new esTransactionScope())
            {
                var regColl = new RegistrationCollection();
                regColl.Query.Where(regColl.Query.RegistrationNo.In(MergeRegistrationList()));
                regColl.LoadAll();

                var historys = new RegistrationCloseOpenHistoryCollection();

                foreach (var reg in regColl)
                {
                    reg.IsHoldTransactionEntry = !statusNow;
                    reg.IsHoldTransactionEntryByUserID = AppSession.UserLogin.UserID;

                    var hist = historys.AddNew();
                    hist.RegistrationNo = reg.RegistrationNo;
                    hist.StatusId = "H";
                    hist.IsTrue = !statusNow;
                    hist.Notes = "Verification & Finalize Billing";
                    hist.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();;
                    hist.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }

                regColl.Save();
                historys.Save();

                trans.Complete();
            }

            RefreshButtonLock(!statusNow);
        }

        private void Closed(bool fromDischargePermit)
        {
            var r = new Registration();
            r.LoadByPrimaryKey(txtRegistrationNo.Text);
            bool statusNow = fromDischargePermit ? false : (r.IsClosed ?? false);

            var historys = new RegistrationCloseOpenHistoryCollection();

            using (var trans = new esTransactionScope())
            {
                var regColl = new RegistrationCollection();
                regColl.Query.Where(regColl.Query.RegistrationNo.In(MergeRegistrationList()));
                regColl.LoadAll();

                foreach (var reg in regColl)
                {
                    reg.IsClosed = !statusNow;

                    var hist = historys.AddNew();
                    hist.RegistrationNo = reg.RegistrationNo;
                    hist.StatusId = "C";
                    hist.IsTrue = !statusNow;
                    hist.Notes = "Verification & Finalize Billing" + (fromDischargePermit ? " >> Discharge Patient Permit" : "");
                    hist.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();;
                    hist.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }

                regColl.Save();
                historys.Save();
                trans.Complete();
            }

            if (!fromDischargePermit)
                RefreshButtonClose(!statusNow);
        }

        private void RefreshButtonClose()
        {
            var r = new Registration();
            r.LoadByPrimaryKey(txtRegistrationNo.Text);
            RefreshButtonClose(r.IsClosed);
        }

        private void RefreshButtonClose(bool? isClosed)
        {
            var btn = RadToolBar2.Items[16];
            btn.Text = isClosed ?? false ? "Open" : "Closed";
            btn.ImageUrl = isClosed ?? false ? "~/Images/Toolbar/lock16.png" : "~/Images/Toolbar/unlock16.png";
            btn.HoveredImageUrl = isClosed ?? false
                ? "~/Images/Toolbar/lock16_h.png"
                : "~/Images/Toolbar/unlock16_h.png";
            btn.DisabledImageUrl = isClosed ?? false
                ? "~/Images/Toolbar/lock16_d.png"
                : "~/Images/Toolbar/unlock16_d.png";
        }

        private void RefreshButtonLock()
        {
            var r = new Registration();
            r.LoadByPrimaryKey(txtRegistrationNo.Text);
            RefreshButtonLock(r.IsHoldTransactionEntry);
        }

        private void RefreshButtonLock(bool? isHoldTransactionEntry)
        {
            var btn = RadToolBar2.Items[2];
            btn.Text = isHoldTransactionEntry ?? false ? "Unlock" : "Lock";
            btn.ImageUrl = isHoldTransactionEntry ?? false ? "~/Images/Toolbar/lock16.png" : "~/Images/Toolbar/unlock16.png";
            btn.HoveredImageUrl = isHoldTransactionEntry ?? false ? "~/Images/Toolbar/lock16_h.png" : "~/Images/Toolbar/unlock16_h.png";
            btn.DisabledImageUrl = isHoldTransactionEntry ?? false ? "~/Images/Toolbar/lock16_d.png" : "~/Images/Toolbar/unlock16_d.png";
        }

        private void RefreshButtonCheckout(bool? isAllowCheckout)
        {
            var btn = RadToolBar2.Items[18];
            //btn.Text = isAllowCheckout ?? false ? "Checkout" : "Checkout";
            btn.ImageUrl = isAllowCheckout ?? false ? "~/Images/RpN.png" : "~/Images/RpY.png";
            btn.HoveredImageUrl = isAllowCheckout ?? false
                ? "~/Images/RpN.png"
                : "~/Images/RpY.png";
            btn.DisabledImageUrl = isAllowCheckout ?? false
                ? "~/Images/RpN.png"
                : "~/Images/RpY.png";
        }

        #endregion

        #region Header
        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void cboGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            GuarantorQuery query = new GuarantorQuery();
            query.es.Top = 30;
            query.Where
                (
                    query.GuarantorName.Like(searchTextContain),
                    query.SRGuarantorType != AppSession.Parameter.GuarantorTypeMemberID,
                    query.IsActive == true
                );
            if (_healthcareInitial == "RSCH")
                query.Where(query.GuarantorHeaderID == cboGuarantorGroupID.SelectedValue);
            query.OrderBy(query.GuarantorName.Ascending);

            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        protected void cboGuarantorID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var grr = new Guarantor();
            grr.LoadByPrimaryKey(e.Value);
            cboSRBusinessMethod.SelectedValue = grr.SRBusinessMethod;

            txtPlafonValue.ReadOnly = !(grr.SRBusinessMethod == _businessMethodFlavon);
            txtPlafonValue.Value = 0;

            if (txtPlafonValue.ReadOnly == false && hdnRegType.Value != AppConstant.RegistrationType.InPatient)
            {
                var plafondAmt = GuarantorServiceUnitPlafond.GetPlafondAmount(cboGuarantorID.SelectedValue, txtServiceUnitID.Text, GuarantorBPJS.Contains(cboGuarantorID.SelectedValue));
                txtPlafonValue.Value = Convert.ToDouble(plafondAmt);
            }

            chkIsGlobalFlavon.Enabled = (grr.SRBusinessMethod == _businessMethodFlavon);
            chkIsGlobalFlavon.Checked = grr.IsGlobalPlafond ?? false;
            //btnBpjsPackage.Visible = (grr.SRBusinessMethod == _businessMethodBpjs);

            cboEmployeeID.Items.Clear();
            cboEmployeeID.Text = string.Empty;
            cboGuarSRRelationship.SelectedValue = string.Empty;
            cboGuarSRRelationship.Text = string.Empty;

            if (grr.SRGuarantorType == _guarantorTypeEmployee)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                if (pat.PersonID != null)
                {
                    var empq = new PersonalInfoQuery();
                    empq.Where(empq.PersonID == pat.PersonID);
                    cboEmployeeID.DataSource = empq.LoadDataTable();
                    cboEmployeeID.DataBind();
                    cboEmployeeID.SelectedValue = pat.PersonID.ToString();
                }

                cboGuarSRRelationship.SelectedValue = pat.SREmployeeRelationship;

                cboEmployeeID.Enabled = !_isRADTLinkToHumanResourcesModul;
                cboGuarSRRelationship.Enabled = !_isRADTLinkToHumanResourcesModul;
            }
            else
            {
                string searchTextContain = string.Format("%{0}%", cboGuarantorID.SelectedValue);
                var pars = new AppParameterCollection();
                pars.Query.Where(pars.Query.ParameterID == "DependentsOfEmployeesGuarantorID",
                                 pars.Query.ParameterValue.Like(searchTextContain));
                pars.LoadAll();
                if (pars.Count > 0)
                {
                    cboEmployeeID.Enabled = true;
                    cboGuarSRRelationship.Enabled = true;
                }
                else
                {
                    cboEmployeeID.Enabled = !_isRADTLinkToHumanResourcesModul;
                    cboGuarSRRelationship.Enabled = !_isRADTLinkToHumanResourcesModul;
                }
            }
            if (GuarantorBPJS.Contains(e.Value))
                hdnBpjsLabel.Value = "bpjs";
            else hdnBpjsLabel.Value = "";
        }

        protected void cboGuarantorGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery("a");
            var querydt = new GuarantorQuery("b");
            query.InnerJoin(querydt).On(query.GuarantorHeaderID == querydt.GuarantorID);
            query.Select(query.GuarantorHeaderID.As("GuarantorID"), querydt.GuarantorName);
            query.es.Top = 30;
            query.es.Distinct = true;
            query.Where
                (
                    querydt.GuarantorName.Like(searchTextContain),
                    query.IsActive == true,
                    query.SRGuarantorType != AppSession.Parameter.GuarantorTypeMemberID
                );
            query.OrderBy(querydt.GuarantorName.Ascending);

            cboGuarantorGroupID.DataSource = query.LoadDataTable();
            cboGuarantorGroupID.DataBind();
        }

        protected void cboGuarantorGroupID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboGuarantorID.Items.Clear();
            cboGuarantorID.Text = string.Empty;
            cboEmployeeID.Items.Clear();
            cboEmployeeID.Text = string.Empty;
            cboGuarSRRelationship.SelectedValue = string.Empty;
            cboGuarSRRelationship.Text = string.Empty;
            cboSRBusinessMethod.SelectedValue = string.Empty;
            cboSRBusinessMethod.Text = string.Empty;
            txtPlafonValue.Value = 0;
            chkIsGlobalFlavon.Checked = false;
            //btnBpjsPackage.Visible = false;
        }

        protected void cboEmployeeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " +
                          ((DataRowView)e.Item.DataItem)["FirstName"].ToString() + " " +
                          ((DataRowView)e.Item.DataItem)["MiddleName"].ToString() + " " +
                          ((DataRowView)e.Item.DataItem)["LastName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboEmployeeID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new PersonalInfoQuery();
            query.es.Top = 15;
            query.Select(query.PersonID, query.EmployeeNumber, query.FirstName, query.MiddleName, query.LastName);
            query.Where
                (
                    query.Or(query.EmployeeNumber == e.Text,
                    query.FirstName.Like(searchTextContain))
                );
            query.OrderBy(query.EmployeeNumber.Ascending);

            cboEmployeeID.DataSource = query.LoadDataTable();
            cboEmployeeID.DataBind();
        }

        protected void btnCalculateAdmin_Click(object sender, ImageClickEventArgs e)
        {
            using (var trans = new esTransactionScope())
            {
                double patAdm = 0, guarAdm = 0;
                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);

                var grr = new Guarantor();
                grr.LoadByPrimaryKey(cboGuarantorID.SelectedValue);

                if (!_isUsingIntermBill)
                {
                    if (grr.IsIncludeAdminValue ?? false)
                    {
                        if (grr.IsAdminFromTotal ?? false)
                        {
                            if (grr.IsAdminCalcBeforeDiscount ?? false)
                            {
                                if (grr.SRGuarantorType == _guarantorTypeSelf)
                                {
                                    patAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations.Sum(c => c.PatientAmount + c.DiscountAmount) ?? 0, reg.SRRegistrationType));
                                    guarAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations.Sum(c => c.GuarantorAmount) ?? 0, reg.SRRegistrationType));
                                }
                                else
                                {
                                    patAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations.Sum(c => c.PatientAmount + (c.DiscountAmount2 ?? 0)) ?? 0, reg.SRRegistrationType));
                                    guarAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations.Sum(c => c.GuarantorAmount + c.DiscountAmount) ?? 0, reg.SRRegistrationType));
                                }
                            }
                            else
                            {
                                patAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations.Sum(c => c.PatientAmount) ?? 0, reg.SRRegistrationType));
                                guarAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations.Sum(c => c.GuarantorAmount) ?? 0, reg.SRRegistrationType));
                            }
                        }
                        else
                        {
                            patAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations, reg.SRRegistrationType, true));
                            guarAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations, reg.SRRegistrationType, false));
                        }
                        if (grr.IsCoverAllAdminCosts ?? false)
                        {
                            guarAdm += patAdm;
                            patAdm = 0;
                        }
                    }
                    else
                    {
                        if (grr.IsAdminFromTotal ?? false)
                        {
                            if (grr.IsAdminCalcBeforeDiscount ?? false)
                                patAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations.Sum(c => c.PatientAmount + c.GuarantorAmount + c.DiscountAmount + (c.DiscountAmount2 ?? 0)) ?? 0, reg.SRRegistrationType));
                            else
                                patAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations.Sum(c => c.PatientAmount + c.GuarantorAmount) ?? 0, reg.SRRegistrationType));
                        }
                        else
                            patAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations, reg.SRRegistrationType));
                    }
                }
                else
                {
                    if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                    {
                        var ibcoll = new IntermBillCollection();
                        ibcoll.Query.Where(ibcoll.Query.RegistrationNo == txtRegistrationNo.Text,
                                           ibcoll.Query.IsVoid == false);
                        ibcoll.LoadAll();
                        if (ibcoll.Count > 0)
                        {
                            foreach (var ib in ibcoll)
                            {
                                var c = new CostCalculationCollection();
                                c.Query.Where(c.Query.IntermBillNo == ib.IntermBillNo);
                                c.LoadAll();
                                if (c.Count > 0)
                                {
                                    patAdm += Convert.ToDouble(ib.AdministrationAmount);
                                    guarAdm += Convert.ToDouble(ib.GuarantorAdministrationAmount);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (grr.IsIncludeAdminValue ?? false)
                        {
                            if (grr.IsAdminFromTotal ?? false)
                            {
                                if (grr.IsAdminCalcBeforeDiscount ?? false)
                                {
                                    if (grr.SRGuarantorType == _guarantorTypeSelf)
                                    {
                                        patAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations.Sum(c => c.PatientAmount + c.DiscountAmount) ?? 0, reg.SRRegistrationType));
                                        guarAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations.Sum(c => c.GuarantorAmount) ?? 0, reg.SRRegistrationType));
                                    }
                                    else
                                    {
                                        patAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations.Sum(c => c.PatientAmount + (c.DiscountAmount2 ?? 0)) ?? 0, reg.SRRegistrationType));
                                        guarAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations.Sum(c => c.GuarantorAmount + c.DiscountAmount) ?? 0, reg.SRRegistrationType));
                                    }
                                }
                                else
                                {
                                    patAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations.Sum(c => c.PatientAmount) ?? 0, reg.SRRegistrationType));
                                    guarAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations.Sum(c => c.GuarantorAmount) ?? 0, reg.SRRegistrationType));
                                }
                            }
                            else
                            {
                                patAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations, reg.SRRegistrationType, true));
                                guarAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations, reg.SRRegistrationType, false));
                            }
                            if (grr.IsCoverAllAdminCosts ?? false)
                            {
                                guarAdm += patAdm;
                                patAdm = 0;
                            }
                        }
                        else
                        {
                            if (grr.IsAdminFromTotal ?? false)
                            {
                                if (grr.IsAdminCalcBeforeDiscount ?? false)
                                    patAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations.Sum(c => c.PatientAmount + c.GuarantorAmount + c.DiscountAmount + (c.DiscountAmount2 ?? 0)) ?? 0, reg.SRRegistrationType));
                                else
                                    patAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations.Sum(c => c.PatientAmount + c.GuarantorAmount) ?? 0, reg.SRRegistrationType));
                            }
                            else
                                patAdm = Convert.ToDouble(Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, CostCalculations, reg.SRRegistrationType));
                        }
                    }
                }

                txtAdminValue.Value = patAdm + guarAdm;

                reg.AdministrationAmount = Convert.ToDecimal(txtAdminValue.Value);
                reg.PatientAdm = Convert.ToDecimal(patAdm);
                reg.GuarantorAdm = Convert.ToDecimal(guarAdm);

                reg.Save();

                trans.Complete();
            }
        }

        protected void btnSaveAdmin_Click(object sender, ImageClickEventArgs e)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);
            reg.AdministrationAmount = Convert.ToDecimal(txtAdminValue.Value);
            //reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
            //reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            reg.Save();

            pnlInfo2.Visible = true;
            lblInfo2.Text = "Save Administration Amount completed.";
            pnlInfo3.Visible = false;
            lblInfo3.Text = string.Empty;
        }

        protected void btnSaveGuarantor_Click(object sender, ImageClickEventArgs e)
        {
            bool isValid = true;
            string msg = string.Empty;

            if (string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
            {
                isValid = false;
                msg = "Guarantor required.";
            }
            else
            {
                var grr = new Guarantor();
                if (!grr.LoadByPrimaryKey(cboGuarantorID.SelectedValue))
                {
                    isValid = false;
                    msg = "Guarantor is not found.";
                }
                else
                {
                    if (_healthcareInitial == "RSCH")
                    {
                        var guarhd = new Guarantor();
                        guarhd.LoadByPrimaryKey(grr.GuarantorHeaderID);

                        if (guarhd.IsActive == false)
                        {
                            isValid = false;
                            msg = "Guarantor Group is not active. Please select another Guarantor Group.";
                        }
                        if (isValid && txtRegistrationDate.SelectedDate > guarhd.ContractEnd)
                        {
                            isValid = false;
                            msg =
                                "The contract period for selected Guarantor Group is over. Please select another Guarantor Group.";
                        }
                        if (isValid && grr.IsActive == false)
                        {
                            isValid = false;
                            msg = "Guarantor is not active. Please select another Guarantor.";
                        }
                        if (isValid && txtRegistrationDate.SelectedDate > grr.ContractEnd)
                        {
                            isValid = false;
                            msg = "The contract period for selected Guarantor is over. Please select another Guarantor.";
                        }
                    }
                    else
                    {
                        var reg = new Registration();
                        reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                        if (AppSession.Parameter.ValidateGuarantorContractPeriode == "Yes")
                        {
                            if (reg.RegistrationDate > grr.ContractEnd)
                            {
                                isValid = false;
                                msg = "The contract period for selected Guarantor is over. Please select another Guarantor.";
                            }
                        }

                        if (isValid && grr.IsActive == false)
                        {
                            isValid = false;
                            msg = "Guarantor is not active. Please select another Guarantor.";
                        }
                    }

                    if (isValid && grr.SRGuarantorType == _guarantorTypeSelf && grr.SRBusinessMethod != AppSession.Parameter.BusinessMethodFlavon)
                    {
                        if (txtPlafonValue.Value > 0)// || txtPlavonChargeValue.Value >0)
                        {
                            isValid = false;
                            msg = "Plafond Value for SELF Guarantor must be zero.";
                        }
                    }

                    if (isValid && _isUsingHumanResourcesModul)
                    {
                        string isEmployeeIdRequeredType;
                        var guar = new Guarantor();
                        guar.LoadByPrimaryKey(cboGuarantorID.SelectedValue);
                        if (guar.SRGuarantorType == _guarantorTypeEmployee)
                            isEmployeeIdRequeredType = "1";
                        else
                        {
                            string searchTextContain = string.Format("%{0}%", cboGuarantorID.SelectedValue);
                            var apps = new AppParameterCollection();
                            apps.Query.Where(apps.Query.ParameterID == "DependentsOfEmployeesGuarantorID",
                                            apps.Query.ParameterValue.Like(searchTextContain));
                            apps.LoadAll();
                            isEmployeeIdRequeredType = apps.Count > 0 ? "2" : "0";
                        }

                        if (isEmployeeIdRequeredType != "0")
                        {
                            if (string.IsNullOrEmpty(cboEmployeeID.SelectedValue))
                            {
                                switch (isEmployeeIdRequeredType)
                                {
                                    case "1":
                                        isValid = false;
                                        msg = "Employee ID required. Please contact HRD to define that required.";
                                        break;
                                    case "2":
                                        isValid = false;
                                        msg = "Employee ID required.";
                                        break;
                                }
                            }
                            else
                            {
                                var emp = new PersonalInfo();
                                if (!emp.LoadByPrimaryKey(Convert.ToInt32(cboEmployeeID.SelectedValue)))
                                {
                                    isValid = false;
                                    msg = "Employee ID is not registered.";
                                }
                            }
                        }
                    }
                    if (isValid)
                    {
                        var reg = new Registration();
                        reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                        if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                        {
                            if (grr.IsCoverInpatient == false)
                            {
                                isValid = false;
                                msg = "Guarantor is not cover Inpatient or Emergency. Please select another Guarantor.";
                            }
                        }
                        else
                        {
                            if (grr.IsCoverOutpatient == true)
                            {
                                var notCovereds = new GuarantorServiceUnitRuleCollection();
                                notCovereds.Query.Where(notCovereds.Query.GuarantorID == cboGuarantorID.SelectedValue,
                                                         notCovereds.Query.ServiceUnitID == reg.ServiceUnitID,
                                                         notCovereds.Query.IsCovered == false);
                                notCovereds.LoadAll();
                                if (notCovereds.Count > 0)
                                {
                                    var unit = new ServiceUnit();
                                    unit.LoadByPrimaryKey(reg.ServiceUnitID);

                                    isValid = false;
                                    msg = "Guarantor is not cover Outpatient - Unit: " + unit.ServiceUnitName +
                                                          ". Please select another Guarantor.";
                                }
                            }
                            else
                            {
                                var covereds = new GuarantorServiceUnitRuleCollection();
                                covereds.Query.Where(covereds.Query.GuarantorID == cboGuarantorID.SelectedValue,
                                                         covereds.Query.ServiceUnitID == reg.ServiceUnitID,
                                                         covereds.Query.IsCovered == true);
                                covereds.LoadAll();
                                if (covereds.Count == 0)
                                {
                                    var unit = new ServiceUnit();
                                    unit.LoadByPrimaryKey(reg.ServiceUnitID);

                                    isValid = false;
                                    msg = "Guarantor is not cover Outpatient - Unit: " + unit.ServiceUnitName +
                                                          ". Please select another Guarantor.";
                                }
                            }
                        }
                    }
                }
            }

            if (isValid)
            {
                bool IsSaveGuarantor = Save();
                pnlInfo2.Visible = true;
                lblInfo2.Text = IsSaveGuarantor ? "Changes to the guarantor have been made, <span style=\"color:Red\" class=\"blinking\"><b>please Recalculate</b></span>" :
                    "Save Guarantor completed.";
                pnlInfo3.Visible = false;
                lblInfo3.Text = string.Empty;

                btnPersonalAr.Enabled = true; // cboSRBusinessMethod.SelectedValue != _businessMethodBpjs;
                //btnBpjsPackage.Visible = cboSRBusinessMethod.SelectedValue == _businessMethodBpjs;

                if (GuarantorBPJS.Contains(cboGuarantorID.SelectedValue))
                    hdnBpjsLabel.Value = "bpjs";
                else hdnBpjsLabel.Value = "";

                lblNeedRecalculation.Text = string.Empty;
            }
            else
            {
                pnlInfo2.Visible = true;
                lblInfo2.Text = msg;
                pnlInfo3.Visible = false;
                lblInfo3.Text = string.Empty;
            }
        }

        protected void btnRegistrationRule_Click(object sender, ImageClickEventArgs e)
        {
            using (var trans = new esTransactionScope())
            {
                RegistrationItemRules.MarkAllAsDeleted();
                RegistrationItemRules.Save();

                var item = new ItemQuery("a");
                var transHd = new TransChargesQuery("b");
                var transDt = new TransChargesItemQuery("c");

                item.es.Distinct = true;
                item.Select
                    (
                        item.ItemID,
                        item.ItemName
                    );
                item.InnerJoin(transDt).On
                    (
                        item.ItemID == transDt.ItemID &&
                        transDt.IsApprove == true
                    );
                item.InnerJoin(transHd).On
                    (
                        transDt.TransactionNo == transHd.TransactionNo &&
                        transHd.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regNo"])) &&
                        transHd.IsApproved == true
                    );

                DataTable dtbt = item.LoadDataTable();

                var itemp = new ItemQuery("a");
                var presHd = new TransPrescriptionQuery("b");
                var presDt = new TransPrescriptionItemQuery("c");

                itemp.es.Distinct = true;
                itemp.Select
                    (
                        itemp.ItemID,
                        itemp.ItemName
                    );
                itemp.InnerJoin(presDt).On
                    (
                        itemp.ItemID == presDt.ItemID &&
                        presDt.IsApprove == true
                    );
                itemp.InnerJoin(presHd).On
                    (
                        presDt.PrescriptionNo == presHd.PrescriptionNo &&
                        presHd.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regNo"])) &&
                        presHd.IsApproval == true
                    );
                DataTable dtbp = itemp.LoadDataTable();

                DataTable dtb = dtbt;
                dtb.Merge(dtbp);

                foreach (DataRow row in dtb.Rows)
                {
                    var itemRule = RegistrationItemRules.AddNew();

                    itemRule.RegistrationNo = txtRegistrationNo.Text;
                    itemRule.ItemID = row["ItemID"].ToString();
                    itemRule.ItemName = row["ItemName"].ToString();
                    itemRule.SRGuarantorRuleType = _guarantorRuleTypeDiscount;
                    var std = new AppStandardReferenceItem();
                    std.LoadByPrimaryKey("GuarantorRuleType", itemRule.SRGuarantorRuleType);
                    itemRule.GuarantorRuleTypeName = std.ItemName;
                    itemRule.AmountValue = Convert.ToDecimal(txtDiscountAmount.Value);
                    itemRule.IsValueInPercent = true;
                    itemRule.IsInclude = true;
                    itemRule.IsToGuarantor = false;
                    itemRule.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    itemRule.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }

                RegistrationItemRules.Save();
                trans.Complete();
            }

            RegistrationItemRules = null;
            grdRegistrationItemRule.Rebind();
        }

        protected void btnSRDiscountReason_Click(object sender, ImageClickEventArgs e)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);
            reg.SRDiscountReason = cboSRDiscountReason.SelectedValue;
            //reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
            //reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            reg.Save();

            pnlInfo2.Visible = true;
            lblInfo2.Text = "Save Discount Category completed.";
            pnlInfo3.Visible = false;
            lblInfo3.Text = string.Empty;
        }

        protected void cboSRBusinessMethod_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtPlafonValue.ReadOnly = e.Value != _businessMethodFlavon;
            txtPlafonValue.Value = 0;

            if (txtPlafonValue.ReadOnly == false && hdnRegType.Value != AppConstant.RegistrationType.InPatient)
            {
                var plafondAmt = GuarantorServiceUnitPlafond.GetPlafondAmount(cboGuarantorID.SelectedValue, txtServiceUnitID.Text, GuarantorBPJS.Contains(cboGuarantorID.SelectedValue));

                if (plafondAmt > 0)
                    txtPlafonValue.Value = Convert.ToDouble(plafondAmt);
            }

            chkIsGlobalFlavon.Enabled = e.Value == _businessMethodFlavon;
            chkIsGlobalFlavon.Checked = true; //e.Value == _businessMethodBpjs;
            //btnBpjsPackage.Visible = e.Value == _businessMethodBpjs;
        }

        protected void cboItemMateraiID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var item = new ItemQuery("a");

            item.es.Top = 10;
            item.Select
                (
                    item.ItemID,
                    item.ItemName
                );
            item.Where
                (
                    item.Or
                        (
                            item.ItemID.Like(searchTextContain),
                            item.ItemName.Like(searchTextContain)
                        ),
                    item.IsActive == true,
                    item.ItemGroupID == AppSession.Parameter.ItemGroupMaterai
                );
            item.OrderBy(item.ItemID.Ascending);

            cboItemMateraiID.DataSource = item.LoadDataTable();
            cboItemMateraiID.DataBind();
        }

        protected void cboItemMateraiID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void btnAddStamp_Click(object sender, ImageClickEventArgs e)
        {
            if (SaveMaterai())
            {
                Transactions = null;
                TransChargesItems = null;
                TransPrescriptionItems = null;
                grdTransChargesItem.Rebind();

                CostCalculations = null;
                grdCostCalculation.Rebind();

                pnlInfo2.Visible = true;
                lblInfo2.Text = "Add Stamp completed.";
                pnlInfo3.Visible = false;
                lblInfo3.Text = string.Empty;
            }
        }

        private bool SaveMaterai()
        {
            if (!string.IsNullOrEmpty(cboItemMateraiID.SelectedValue))
            {
                //registration
                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);

                //guarantor
                var grr = new Guarantor();
                grr.LoadByPrimaryKey(cboGuarantorID.SelectedValue);

                using (var trans = new esTransactionScope())
                {
                    #region Materai
                    var transCharges = new TransCharges();
                    var transChargesItem = new TransChargesItem();
                    var transChargesItemColl = new TransChargesItemCollection();
                    var transChargesItemCompColl = new TransChargesItemCompCollection();
                    var costCalculations = new CostCalculationCollection();

                    if (!string.IsNullOrEmpty(cboItemMateraiID.SelectedValue))
                    {
                        transCharges.TransactionNo = GetNewTransactionNo();
                        _autoNumberTrans.LastCompleteNumber = transCharges.TransactionNo;
                        _autoNumberTrans.Save();

                        transCharges.RegistrationNo = reg.RegistrationNo;
                        transCharges.TransactionDate = (new DateTime()).NowAtSqlServer();
                        transCharges.ReferenceNo = string.Empty;
                        transCharges.FromServiceUnitID = reg.ServiceUnitID;
                        transCharges.ToServiceUnitID = reg.ServiceUnitID;
                        transCharges.ClassID = reg.ChargeClassID;
                        transCharges.RoomID = reg.RoomID;
                        transCharges.BedID = reg.BedID;
                        transCharges.DueDate = (new DateTime()).NowAtSqlServer().Date;
                        transCharges.SRShift = Registration.GetShiftID();
                        transCharges.SRItemType = string.Empty;
                        transCharges.IsProceed = false;
                        transCharges.IsBillProceed = true;
                        transCharges.IsApproved = true;
                        transCharges.IsVoid = false;
                        transCharges.IsOrder = false;
                        transCharges.IsCorrection = false;
                        transCharges.IsClusterAssign = false;
                        transCharges.IsAutoBillTransaction = true;
                        transCharges.Notes = string.Empty;
                        transCharges.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        transCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        transChargesItem.TransactionNo = transCharges.TransactionNo;
                        transChargesItem.SequenceNo = "001";
                        transChargesItem.ReferenceNo = string.Empty;
                        transChargesItem.ReferenceSequenceNo = string.Empty;
                        transChargesItem.ItemID = cboItemMateraiID.SelectedValue;
                        transChargesItem.ChargeClassID = reg.ChargeClassID;
                        transChargesItem.ParamedicID = string.Empty;

                        ItemTariff tariff = (Helper.Tariff.GetItemTariff(transCharges.TransactionDate.Value, grr.SRTariffType, reg.ChargeClassID, reg.ChargeClassID, cboItemMateraiID.SelectedValue, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                             Helper.Tariff.GetItemTariff(transCharges.TransactionDate.Value, grr.SRTariffType, _defaultTariffClass, reg.ChargeClassID, cboItemMateraiID.SelectedValue, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                            (Helper.Tariff.GetItemTariff(transCharges.TransactionDate.Value, _defaultTariffType, reg.ChargeClassID, reg.ChargeClassID, cboItemMateraiID.SelectedValue, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                             Helper.Tariff.GetItemTariff(transCharges.TransactionDate.Value, _defaultTariffType, _defaultTariffClass, reg.ChargeClassID, cboItemMateraiID.SelectedValue, reg.GuarantorID, false, reg.SRRegistrationType));


                        transChargesItem.IsAdminCalculation = tariff.IsAdminCalculation ?? false;

                        var itemService = new ItemService();
                        itemService.LoadByPrimaryKey(cboItemMateraiID.SelectedValue);
                        transChargesItem.SRItemUnit = itemService.SRItemUnit;

                        transChargesItem.CostPrice = 0;
                        transChargesItem.IsVariable = false;
                        transChargesItem.IsCito = false;
                        transChargesItem.ChargeQuantity = (decimal)1D;
                        transChargesItem.StockQuantity = (decimal)0D;
                        transChargesItem.Price = tariff.Price ?? 0;
                        transChargesItem.DiscountAmount = (decimal)0D;
                        transChargesItem.CitoAmount = (decimal)0D;
                        transChargesItem.RoundingAmount = Helper.RoundingDiff;
                        transChargesItem.SRDiscountReason = string.Empty;
                        transChargesItem.IsAssetUtilization = false;
                        transChargesItem.AssetID = string.Empty;
                        transChargesItem.IsBillProceed = true;
                        transChargesItem.IsOrderRealization = false;
                        transChargesItem.IsPackage = false;
                        transChargesItem.IsApprove = true;
                        transChargesItem.IsVoid = false;
                        transChargesItem.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        transChargesItem.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        transChargesItem.ParentNo = string.Empty;
                        transChargesItem.SRCenterID = string.Empty;

                        #region item component

                        var compQuery = new ItemTariffComponentQuery();
                        compQuery.es.Top = 1;
                        compQuery.Where
                            (
                                compQuery.SRTariffType == grr.SRTariffType,
                                compQuery.ItemID == cboItemMateraiID.SelectedValue,
                                compQuery.ClassID == reg.ChargeClassID,
                                compQuery.StartingDate <= (new DateTime()).NowAtSqlServer().Date
                            );

                        var compColl = Helper.Tariff.GetItemTariffComponentCollection(transCharges.TransactionDate.Value, grr.SRTariffType, reg.ChargeClassID, cboItemMateraiID.SelectedValue);
                        if (!compColl.Any())
                            compColl = Helper.Tariff.GetItemTariffComponentCollection(transCharges.TransactionDate.Value, grr.SRTariffType,
                                _defaultTariffClass, cboItemMateraiID.SelectedValue);
                        if (!compColl.Any())
                            compColl = Helper.Tariff.GetItemTariffComponentCollection(transCharges.TransactionDate.Value, _defaultTariffType,
                                reg.ChargeClassID, cboItemMateraiID.SelectedValue);
                        if (!compColl.Any())
                            compColl = Helper.Tariff.GetItemTariffComponentCollection(transCharges.TransactionDate.Value, _defaultTariffType,
                                _defaultTariffClass, cboItemMateraiID.SelectedValue);

                        foreach (var comp in compColl)
                        {
                            var transChargesItemComp = transChargesItemCompColl.AddNew();
                            transChargesItemComp.TransactionNo = transCharges.TransactionNo;
                            transChargesItemComp.SequenceNo = "001";
                            transChargesItemComp.TariffComponentID = comp.TariffComponentID;
                            transChargesItemComp.Price = comp.Price;
                            transChargesItemComp.DiscountAmount = (decimal)0D;
                            transChargesItemComp.ParamedicID = string.Empty;
                            transChargesItemComp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            transChargesItemComp.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        }
                        #endregion

                        #region Cost Calculation
                        var tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, grr.GuarantorID, cboItemMateraiID.SelectedValue, (new DateTime()).NowAtSqlServer(), false);

                        var grrID = reg.GuarantorID;

                        var pat = new Patient();
                        pat.LoadByPrimaryKey(reg.PatientID);

                        if (grrID == _selfGuarantor)
                        {
                            if (!string.IsNullOrEmpty(pat.MemberID))
                                grrID = pat.MemberID;
                        }

                        var rowCovered = tblCovered.AsEnumerable().SingleOrDefault(t => t.Field<string>("ItemID") == transChargesItem.ItemID &&
                                                                                        t.Field<bool>("IsInclude"));

                        //TransChargesItemComps
                        if (rowCovered != null)
                        {
                            decimal? discount = 0;
                            bool isDiscount = false, isMargin = false;
                            foreach (var comp in transChargesItemCompColl.Where(t => t.TransactionNo == transChargesItem.TransactionNo &&
                                                                                     t.SequenceNo == transChargesItem.SequenceNo)
                                                                         .OrderBy(t => t.TariffComponentID))
                            {
                                var amountValue = (decimal?)rowCovered["AmountValue"];
                                if (rowCovered["SRGuarantorRuleType"].ToString().Equals(_guarantorRuleTypeDiscount))
                                {
                                    if ((comp.Price - comp.DiscountAmount) <= 0)
                                        continue;

                                    if ((bool)rowCovered["IsValueInPercent"])
                                    {
                                        //comp.DiscountAmount += (amountValue / 100) * comp.Price;
                                        comp.DiscountAmount = (amountValue / 100) * comp.Price;
                                        comp.AutoProcessCalculation = 0 - (amountValue / 100) * comp.Price;
                                    }
                                    else
                                    {
                                        if (!isDiscount)
                                        {
                                            if (discount == 0)
                                            {
                                                if (comp.Price >= amountValue)
                                                {
                                                    //comp.DiscountAmount += amountValue;
                                                    comp.DiscountAmount = amountValue;
                                                    comp.AutoProcessCalculation = 0 - amountValue;
                                                    isDiscount = true;
                                                }
                                                else
                                                {
                                                    //comp.DiscountAmount += comp.Price;
                                                    comp.DiscountAmount = comp.Price;
                                                    comp.AutoProcessCalculation = 0 - comp.Price;
                                                    discount = amountValue - comp.Price;
                                                }
                                            }
                                            else
                                            {
                                                if (comp.Price >= discount)
                                                {
                                                    //comp.DiscountAmount += discount;
                                                    comp.DiscountAmount = discount;
                                                    comp.AutoProcessCalculation = 0 - discount;
                                                    isDiscount = true;
                                                }
                                                else
                                                {
                                                    //comp.DiscountAmount += comp.Price;
                                                    comp.DiscountAmount = comp.Price;
                                                    comp.AutoProcessCalculation = 0 - comp.Price;
                                                    discount -= comp.Price;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(_guarantorRuleTypeMargin))
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
                        if (transChargesItemCompColl.Count > 0)
                        {
                            transChargesItem.AutoProcessCalculation = transChargesItemCompColl.Where(t => t.TransactionNo == transChargesItem.TransactionNo &&
                                                                                                          t.SequenceNo == transChargesItem.SequenceNo)
                                                                                              .Sum(t => t.AutoProcessCalculation);
                            if (transChargesItem.AutoProcessCalculation < 0)
                            {
                                transChargesItem.DiscountAmount += transChargesItem.ChargeQuantity * Math.Abs(transChargesItem.AutoProcessCalculation ?? 0);

                                if (transChargesItem.DiscountAmount > transChargesItem.Price)
                                {
                                    transChargesItem.DiscountAmount = transChargesItem.Price;
                                    transChargesItem.AutoProcessCalculation = 0 - transChargesItem.Price;
                                }
                            }
                            else if (transChargesItem.AutoProcessCalculation > 0)
                                transChargesItem.Price += transChargesItem.AutoProcessCalculation;
                        }
                        else
                        {
                            if (rowCovered != null)
                            {
                                if (rowCovered["SRGuarantorRuleType"].ToString().Equals(_guarantorRuleTypeDiscount))
                                {
                                    if ((bool)rowCovered["IsValueInPercent"])
                                        transChargesItem.DiscountAmount += (transChargesItem.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * transChargesItem.Price);
                                    else
                                        transChargesItem.DiscountAmount += (transChargesItem.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];

                                    if (transChargesItem.DiscountAmount > (transChargesItem.Price * transChargesItem.ChargeQuantity ?? 0))
                                        transChargesItem.DiscountAmount = transChargesItem.Price * (transChargesItem.ChargeQuantity ?? 0);

                                    //transChargesItem.AutoProcessCalculation = 0 - transChargesItem.DiscountAmount;
                                    transChargesItem.AutoProcessCalculation = 0 - transChargesItem.DiscountAmount / (transChargesItem.ChargeQuantity ?? 0);
                                }
                                else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(_guarantorRuleTypeMargin))
                                {
                                    if ((bool)rowCovered["IsValueInPercent"])
                                        transChargesItem.Price += ((decimal)rowCovered["AmountValue"] / 100) * transChargesItem.Price;
                                    else
                                        transChargesItem.Price += (decimal)rowCovered["AmountValue"];

                                    transChargesItem.AutoProcessCalculation = transChargesItem.Price;
                                }
                            }
                        }

                        //post
                        decimal? sum = ((transChargesItem.ChargeQuantity * transChargesItem.Price) -
                                        transChargesItem.DiscountAmount) + transChargesItem.CitoAmount;
                        var calc = new Helper.CostCalculation(grrID, transChargesItem.ItemID, sum ?? 0, tblCovered,
                                                              transChargesItem.ChargeQuantity ?? 0, transChargesItem.DiscountAmount ?? 0);

                        //CostCalculation
                        var cost = costCalculations.AddNew();
                        cost.RegistrationNo = reg.RegistrationNo;
                        cost.TransactionNo = transChargesItem.TransactionNo;
                        cost.SequenceNo = transChargesItem.SequenceNo;
                        cost.ItemID = transChargesItem.ItemID;
                        cost.PatientAmount = calc.PatientAmount;
                        cost.GuarantorAmount = calc.GuarantorAmount;
                        cost.DiscountAmount = transChargesItem.DiscountAmount;
                        cost.IsPackage = transChargesItem.IsPackage;
                        cost.ParentNo = transChargesItem.ParentNo;
                        cost.ParamedicAmount = transChargesItem.ChargeQuantity * transChargesItemCompColl.Where(comp => comp.TransactionNo == transChargesItem.TransactionNo &&
                                                                                                                        comp.SequenceNo == transChargesItem.SequenceNo &&
                                                                                                                        !string.IsNullOrEmpty(comp.ParamedicID))
                                                                                                         .Sum(comp => comp.Price - comp.DiscountAmount);
                        cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        #endregion
                    }
                    #endregion

                    if (!string.IsNullOrEmpty(cboItemMateraiID.SelectedValue))
                    {
                        transCharges.Save();
                        transChargesItem.Save();
                        transChargesItemColl.Save();
                        transChargesItemCompColl.Save();
                        costCalculations.Save();
                    }

                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                    {
                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(transCharges.TransactionDate.Value);
                        if (isClosingPeriod)
                        {
                            throw new Exception("Financial statements for period: " +
                                                string.Format("{0:MMMM-yyyy}", transCharges.TransactionDate.Value) +
                                                " have been closed. Please contact the authorities.");
                            return false;
                        }

                        var unit = new ServiceUnit();
                        unit.LoadByPrimaryKey(reg.ServiceUnitID);

                        int? journalId = JournalTransactions.AddNewIncomeJournal(transCharges, transChargesItemCompColl, reg,
                                                                                 unit, costCalculations, "SU",
                                                                                 AppSession.UserLogin.UserID, 0);

                    }

                    trans.Complete();
                }
                return true;
            }

            return false;
        }

        #endregion

        #region Filter Transaction List
        private void InitializeCboFilterByServiceUnitID()
        {
            string[] regList = Helper.MergeBilling.GetMergeRegistration(Page.Request.QueryString["regNo"]);

            var suQuery = new ServiceUnitQuery("a");
            var trQuery = new TransChargesQuery("b");
            suQuery.InnerJoin(trQuery).On(suQuery.ServiceUnitID == trQuery.ToServiceUnitID &&
                                            trQuery.RegistrationNo.In(regList));
            suQuery.es.Distinct = true;
            suQuery.Select(suQuery.ServiceUnitID, suQuery.ServiceUnitName);
            suQuery.Where
                (
                    suQuery.SRRegistrationType.In(
                        AppConstant.RegistrationType.ClusterPatient,
                        AppConstant.RegistrationType.EmergencyPatient,
                        AppConstant.RegistrationType.InPatient,
                        AppConstant.RegistrationType.OutPatient
                    ),
                    suQuery.IsActive == true
                );
            DataTable dtb1 = suQuery.LoadDataTable();

            var suQuery2 = new ServiceUnitQuery("a");
            var trQuery2 = new TransPrescriptionQuery("b");
            suQuery2.InnerJoin(trQuery2).On(suQuery2.ServiceUnitID == trQuery2.ServiceUnitID &&
                                          trQuery2.RegistrationNo.In(regList));
            suQuery2.es.Distinct = true;
            suQuery2.Select(suQuery2.ServiceUnitID, suQuery2.ServiceUnitName);
            suQuery2.Where(suQuery2.IsActive == true);
            DataTable dtb2 = suQuery2.LoadDataTable();

            DataTable tbl = dtb1;
            tbl.Merge(dtb2);

            cboFilterByServiceUnitID.Items.Add(new RadComboBoxItem("--All--", string.Empty));
            foreach (DataRow row in tbl.Rows)
            {
                cboFilterByServiceUnitID.Items.Add(new RadComboBoxItem(row["ServiceUnitName"].ToString(), row["ServiceUnitID"].ToString()));
            }
        }

        private void InitializeCboFilterByItemGroupID()
        {
            string[] regList = Helper.MergeBilling.GetMergeRegistration(Page.Request.QueryString["regNo"]);

            var igQuery = new ItemGroupQuery("a");
            //var iQuery = new ItemQuery("b");
            //var trQuery = new VwTransactionItemMergeQuery("c");
            //igQuery.InnerJoin(iQuery).On(igQuery.ItemGroupID == iQuery.ItemGroupID);
            //igQuery.InnerJoin(trQuery).On(iQuery.ItemID == trQuery.ItemID &&
            //                                trQuery.RegistrationNo.In(regList));

            //cek pasien REG/IP/140817-0018

            igQuery.es.Distinct = true;
            igQuery.Select(igQuery.ItemGroupID, igQuery.ItemGroupName);

            DataTable tbl = igQuery.LoadDataTable();

            cboFilterByItemGroupID.Items.Add(new RadComboBoxItem("--All--", string.Empty));
            foreach (DataRow row in tbl.Rows)
            {
                cboFilterByItemGroupID.Items.Add(new RadComboBoxItem(row["ItemGroupName"].ToString(), row["ItemGroupID"].ToString()));
            }
        }

        protected void cboFilterByServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Transactions = null;
            TransChargesItems = null;
            TransPrescriptionItems = null;
            grdTransChargesItem.Rebind();

            CostCalculations = null;
            grdCostCalculation.Rebind();
        }

        protected void cboFilterByPaymentStatus_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Transactions = null;
            TransChargesItems = null;
            TransPrescriptionItems = null;
            grdTransChargesItem.Rebind();
        }

        protected void cboFilterByIntermBillStatus_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Transactions = null;
            TransChargesItems = null;
            TransPrescriptionItems = null;
            grdTransChargesItem.Rebind();
        }

        protected void cboFilterByItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Transactions = null;
            TransChargesItems = null;
            TransPrescriptionItems = null;
            grdTransChargesItem.Rebind();

            CostCalculations = null;
            grdCostCalculation.Rebind();
        }

        protected void cboFilterByCheckedStatus_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Transactions = null;
            TransChargesItems = null;
            TransPrescriptionItems = null;
            grdTransChargesItem.Rebind();

            CostCalculations = null;
            grdCostCalculation.Rebind();
        }

        protected void chkIsIncludePrescription_CheckedChanged(object sender, EventArgs e)
        {
            CostCalculations = null;
            grdCostCalculation.Rebind();

            Transactions = null;
            TransChargesItems = null;
            TransPrescriptionItems = null;
            grdTransChargesItem.Rebind();
        }

        protected void cboFilterByItemGroupID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Transactions = null;
            TransChargesItems = null;
            TransPrescriptionItems = null;
            grdTransChargesItem.Rebind();

            CostCalculations = null;
            grdCostCalculation.Rebind();
        }
        #endregion

        #region Process - Recalculated, Interm Bill, Corporate A/R, Personal A/R
        protected void btnRecalculated_Click(object sender, EventArgs e)
        {
            Validate();
            if (!IsValid)
                return;

            bool isValid = true;
            string msg = string.Empty;
            //if (_isUsingHumanResourcesModul == "Yes")
            //{
            //    string isEmployeeIdRequeredType;
            //    var guar = new Guarantor();
            //    guar.LoadByPrimaryKey(cboGuarantorID.SelectedValue);
            //    if (guar.SRGuarantorType == _guarantorTypeEmployee)
            //        isEmployeeIdRequeredType = "1";
            //    else
            //    {
            //        var apps = new AppParameterCollection();
            //        apps.Query.Where(apps.Query.ParameterID == "DependentsOfEmployeesGuarantorID",
            //                        apps.Query.ParameterValue.Like(string.Format(".%{0}%", cboGuarantorID.SelectedValue)));
            //        apps.LoadAll();
            //        isEmployeeIdRequeredType = apps.Count > 0 ? "2" : "0";
            //    }

            //    if (isEmployeeIdRequeredType != "0")
            //    {
            //        if (string.IsNullOrEmpty(cboEmployeeID.SelectedValue))
            //        {
            //            switch (isEmployeeIdRequeredType)
            //            {
            //                case "1":
            //                    isValid = false;
            //                    msg = "Employee ID required. Please contact HRD to define that required.";
            //                    break;
            //                case "2":
            //                    isValid = false;
            //                    msg = "Employee ID required.";
            //                    break;
            //            }
            //        }
            //        else
            //        {
            //            var emp = new PersonalInfo();
            //            if (!emp.LoadByPrimaryKey(Convert.ToInt32(cboEmployeeID.SelectedValue)))
            //            {
            //                isValid = false;
            //                msg = "Employee ID is not registered.";
            //            }
            //        }

            //    }
            //}

            if (string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
            {
                pnlInfo2.Visible = true;
                lblInfo2.Text = "Guarantor required.";
                pnlInfo3.Visible = false;
                lblInfo3.Text = string.Empty;
                return;
            }

            if (isValid)
            {
                Save();
                Process();

                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                txtAdminValue.Value = _isUsingIntermBill
                                      ? Convert.ToDouble(reg.PatientAdm ?? 0) +
                                        Convert.ToDouble(reg.GuarantorAdm ?? 0)
                                      : (double)(reg.AdministrationAmount ?? 0);

                Transactions = null;
                TransChargesItems = null;
                TransPrescriptionItems = null;
                grdTransChargesItem.DataSource = null;
                grdTransChargesItem.Rebind();

                CostCalculations = null;
                grdCostCalculation.DataSource = null;
                grdCostCalculation.Rebind();

                if (AppSession.Parameter.IsRunTheCostCalculationCleanUpProcess)
                {
                    int x = CostCalculation.CostCalculationCleanUpProcess(txtRegistrationNo.Text);
                }

                pnlInfo2.Visible = true;
                lblInfo2.Text = "Recalculated procees completed.";
                lblNeedRecalculation.Text = string.Empty;

                btnPersonalAr.Enabled = true; // cboSRBusinessMethod.SelectedValue != _businessMethodBpjs;
                //btnBpjsPackage.Visible = reg.SRBussinesMethod == _businessMethodBpjs;
            }
            else
            {
                pnlInfo2.Visible = true;
                lblInfo2.Text = msg;
            }
        }

        protected void btnIntermBill_Click(object sender, EventArgs e)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            if (reg.IsVoid ?? false)
            {
                pnlInfo2.Visible = true;
                lblInfo2.Text = "Registration has been void.";
                pnlInfo3.Visible = false;
                lblInfo3.Text = string.Empty;
                return;
            }

            bool isValid = CostCalculations.Count > 0;
            bool isValid2 = true;

            var g = new Guarantor();
            if (g.LoadByPrimaryKey(cboGuarantorID.SelectedValue))
            {
                if (g.SRGuarantorType == _guarantorTypeSelf)
                {
                    var list = CostCalculations.GroupBy(c => c.ItemID).Select(q => new
                    {
                        GuarAmt = q.Sum(p => (p.GuarantorAmount))
                    });

                    foreach (var item in list)
                    {
                        if (item.GuarAmt > 0)
                            isValid2 = false;
                    }
                }
            }

            if (isValid && isValid2)
            {
                _autoNumberIntermBill = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.IntermBill);
                var ibNo = _autoNumberIntermBill.LastCompleteNumber;
                _autoNumberIntermBill.Save();

                using (var trans = new esTransactionScope())
                {
                    DateTime startDate = (new DateTime()).NowAtSqlServer();
                    DateTime endDate = (new DateTime()).NowAtSqlServer().Date.AddDays(-100);
                    decimal patAmount = 0;
                    decimal guarAmount = 0;

                    //Update Detil
                    foreach (CostCalculation item in CostCalculations)
                    {
                        item.IntermBillNo = ibNo;

                        if (item.TransactionDate < startDate)
                            startDate = item.TransactionDate ?? (new DateTime()).NowAtSqlServer();
                        if (item.TransactionDate > endDate)
                            endDate = item.TransactionDate ?? (new DateTime()).NowAtSqlServer();
                        patAmount += item.PatientAmount ?? 0;
                        guarAmount += item.GuarantorAmount ?? 0;
                    }

                    var entity = new IntermBill();
                    entity.AddNew();
                    entity.IntermBillNo = ibNo;
                    entity.IntermBillDate = (new DateTime()).NowAtSqlServer();
                    entity.RegistrationNo = txtRegistrationNo.Text;
                    entity.StartDate = startDate;
                    entity.EndDate = endDate;
                    entity.PatientAmount = Convert.ToDecimal(string.Format("{0:n2}", (patAmount)));
                    entity.GuarantorAmount = Convert.ToDecimal(string.Format("{0:n2}", (guarAmount)));
                    entity.IsVoid = false;
                    entity.IsApproved = false;
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    entity.AskesCoveredSeqNo = string.Empty;
                    entity.AdministrationAmount = 0;
                    entity.GuarantorAdministrationAmount = 0;
                    entity.DiscAdmPatient = 0;
                    entity.DiscAdmGuarantor = 0;
                    entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                    entity.CreatedByUserID = AppSession.UserLogin.UserID;

                    CostCalculations.Save();
                    entity.Save();

                    trans.Complete();
                }

                //-- update nilai intermbill
                var ccq = new CostCalculationQuery("a");
                ccq.Where(ccq.IntermBillNo == ibNo);
                ccq.Select(ccq.IntermBillNo, ccq.PatientAmount.Sum().As("PatientAmount"),
                           ccq.GuarantorAmount.Sum().As("GuarantorAmount"),
                           ccq.DiscountAmount.Sum().As("DiscountAmount"),
                           @"<SUM(ISNULL(a.DiscountAmount2, 0)) AS DiscountAmount2>");
                ccq.GroupBy(ccq.IntermBillNo);

                DataTable dtb = ccq.LoadDataTable();
                if (dtb.Rows.Count > 0)
                {
                    decimal tpatient = dtb.AsEnumerable().Sum(t => t.Field<decimal>("PatientAmount"));
                    decimal tguarantor = dtb.AsEnumerable().Sum(t => t.Field<decimal>("GuarantorAmount"));
                    decimal tdiscount = dtb.AsEnumerable().Sum(t => t.Field<decimal>("DiscountAmount"));
                    decimal tdiscount2 = dtb.AsEnumerable().Sum(t => t.Field<decimal>("DiscountAmount2"));

                    var intermBill = new IntermBill();
                    if (intermBill.LoadByPrimaryKey(ibNo))
                    {
                        intermBill.PatientAmount = Convert.ToDecimal(string.Format("{0:n2}", (tpatient)));
                        intermBill.GuarantorAmount = Convert.ToDecimal(string.Format("{0:n2}", (tguarantor)));
                        intermBill.Save();
                    }
                    //-----------------------------------------
                    //--- update nilai biaya admin
                   

                    var grr = new Guarantor();
                    grr.LoadByPrimaryKey(cboGuarantorID.SelectedValue);

                    decimal admin = 0, admingr = 0;
                    if (grr.IsIncludeAdminValue ?? false) //Cover Administration
                    {
                        if (grr.IsAdminFromTotal ?? false)
                        {
                            if (grr.IsAdminCalcBeforeDiscount ?? false)
                            {
                                if (grr.SRGuarantorType == _guarantorTypeSelf)
                                {
                                    admin =
                                        Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue,
                                                                                              tpatient + tdiscount,
                                                                                              reg.SRRegistrationType);
                                    admingr =
                                        Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, tguarantor, reg.SRRegistrationType);
                                }
                                else
                                {
                                    admin =
                                        Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue,
                                                                                              tpatient + tdiscount2,
                                                                                              reg.SRRegistrationType);
                                    admingr =
                                        Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, tguarantor + tdiscount, reg.SRRegistrationType);
                                }
                            }
                            else
                            {
                                admin =
                                Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, tpatient, reg.SRRegistrationType);
                                admingr =
                                    Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, tguarantor, reg.SRRegistrationType);
                            }
                        }
                        else
                        {
                            var ccs = new CostCalculationCollection();
                            ccs.Query.Where(ccs.Query.IntermBillNo == ibNo);
                            ccs.LoadAll();

                            admin = Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, ccs, reg.SRRegistrationType, true);
                            admingr = Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, ccs, reg.SRRegistrationType, false);
                        }
                        if (grr.IsCoverAllAdminCosts ?? false)
                        {
                            admingr += admin;
                            admin = 0;

                            if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                            {
                                if (grr.AdminValueMinimum > 0 && admingr < (grr.AdminValueMinimum ?? 0))
                                    admingr = grr.AdminValueMinimum ?? 0;
                                else if (grr.AdminAmountLimit > 0 && admingr > grr.AdminAmountLimit)
                                    admingr = grr.AdminAmountLimit ?? 0;
                            }
                            else
                            {
                                if (grr.AdminValueMinimumOp > 0 && admingr < (grr.AdminValueMinimumOp ?? 0))
                                    admingr = grr.AdminValueMinimumOp ?? 0;
                                else if (grr.AdminAmountLimitOp > 0 && admingr > grr.AdminAmountLimitOp)
                                    admingr = grr.AdminAmountLimitOp ?? 0;
                            }
                        }
                    }
                    else
                    {
                        if (grr.IsAdminFromTotal ?? false)
                        {
                            if (grr.IsAdminCalcBeforeDiscount ?? false)
                                admin =
                                Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue,
                                                                                      tpatient + tguarantor + tdiscount + tdiscount2,
                                                                                      reg.SRRegistrationType);
                            else
                                admin =
                                    Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue,
                                                                                          tpatient + tguarantor,
                                                                                          reg.SRRegistrationType);
                        }

                        else
                        {
                            var ccs = new CostCalculationCollection();
                            ccs.Query.Where(ccs.Query.IntermBillNo == ibNo);
                            ccs.LoadAll();

                            admin = Helper.CostCalculation.GetAdminValue(cboGuarantorID.SelectedValue, ccs, reg.SRRegistrationType);
                        }

                        if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                        {
                            if (grr.AdminValueMinimum > 0 && admin < (grr.AdminValueMinimum ?? 0))
                                admin = grr.AdminValueMinimum ?? 0;
                            else if (grr.AdminAmountLimit > 0 && admin > grr.AdminAmountLimit)
                                admin = grr.AdminAmountLimit ?? 0;
                        }
                        else
                        {
                            if (grr.AdminValueMinimumOp > 0 && admin < (grr.AdminValueMinimumOp ?? 0))
                                admin = grr.AdminValueMinimumOp ?? 0;
                            else if (grr.AdminAmountLimitOp > 0 && admin > grr.AdminAmountLimitOp)
                                admin = grr.AdminAmountLimitOp ?? 0;
                        }
                    }

                    admin = Convert.ToDecimal(string.Format("{0:n2}", (admin)));
                    admingr = Convert.ToDecimal(string.Format("{0:n2}", (admingr)));

                    if (admin + admingr != 0)
                    {
                        intermBill = new IntermBill();
                        if (intermBill.LoadByPrimaryKey(ibNo))
                        {
                            var tAdmMax = Helper.CostCalculation.GetAdminValueMax(cboGuarantorID.SelectedValue, reg.SRRegistrationType);

                            var tpat = Helper.Rounding(Convert.ToDecimal(admin) + (intermBill.PatientAmount ?? 0), AppEnum.RoundingType.GlobalTransaction);
                            var tguar = Helper.Rounding(Convert.ToDecimal(admingr) + (intermBill.GuarantorAmount ?? 0), AppEnum.RoundingType.GlobalTransaction);

                            // to make sure admin is not bigger than adminmax
                            if (tAdmMax > 0)
                            {
                                if (admin < tAdmMax)
                                    admin += tpat - (admin + intermBill.PatientAmount ?? 0);
                                if (admingr < tAdmMax)
                                    admingr += tguar - (admingr + intermBill.GuarantorAmount ?? 0);
                            }
                            else
                            {
                                admin += tpat - (admin + intermBill.PatientAmount ?? 0);
                                admingr += tguar - (admingr + intermBill.GuarantorAmount ?? 0);
                            }

                            intermBill.AdministrationAmount = admin;
                            intermBill.GuarantorAdministrationAmount = admingr;
                            intermBill.Save();
                        }
                    }

                    reg.AdministrationAmount += (admin + admingr);
                    reg.PatientAdm += admin;
                    reg.GuarantorAdm += admingr;

                    reg.Save();

                    admin = reg.AdministrationAmount ?? 0;
                    //------------------------------------------

                    Transactions = null;
                    TransChargesItems = null;
                    TransPrescriptionItems = null;
                    grdTransChargesItem.DataSource = null;
                    grdTransChargesItem.Rebind();

                    IntermBills = null;
                    grdIntermBill.DataSource = null;
                    grdIntermBill.Rebind();

                    CostCalculations = null;
                    grdCostCalculation.DataSource = null;
                    grdCostCalculation.Rebind();

                    pnlInfo2.Visible = true;
                    lblInfo2.Text = "Interm Bill process completed.";
                    pnlInfo3.Visible = false;
                    lblInfo3.Text = string.Empty;

                    txtAdminValue.Value = Convert.ToDouble(admin);
                }
                else
                {
                    var ib = new IntermBill();
                    if (ib.LoadByPrimaryKey(ibNo))
                    {
                        ib.MarkAsDeleted();
                        ib.Save();
                    }

                    pnlInfo2.Visible = true;
                    lblInfo2.Text = "Interm Bill process failed. Please try again.";
                    pnlInfo3.Visible = false;
                    lblInfo3.Text = string.Empty;
                }
            }
            else
            {
                pnlInfo2.Visible = true;
                lblInfo2.Text = isValid == false
                                    ? "Interm Bill process failed. There are no verified transaction that can be proceed."
                                    : "Interm Bill process failed. There are SELF Guarantor, but have guarantor amount. <span style=\"color:Red\" class=\"blinking\"><b>Please Recalculate</b></span>";
                pnlInfo3.Visible = false;
                lblInfo3.Text = string.Empty;
            }
        }
        #endregion

        #region Tab: Transaction List
        protected void grdTransChargesItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransChargesItem.DataSource = Transactions;
        }

        private DataTable Transactions
        {
            get
            {
                if (Session["VerificationBilling:Transactions" + UniqueID] != null)
                    return (DataTable)Session["VerificationBilling:Transactions" + UniqueID];

                DataTable tbl = TransChargesItems;
                if (chkIsIncludePrescription.Checked)
                    tbl.Merge(TransPrescriptionItems);

                var dv = tbl.DefaultView;
                dv.Sort = "ServiceUnitName ASC, TransactionNo ASC, SequenceNo ASC";

                SetSession("VerificationBilling:Transactions", dv.ToTable());

                return (DataTable)Session["VerificationBilling:Transactions" + UniqueID];
            }
            set { SetSession("VerificationBilling:Transactions", value); }
        }

        private DataTable TransPrescriptionItems
        {
            get
            {
                if (Session["VerificationBilling:TransPrescriptionItems" + UniqueID] != null)
                    return (DataTable)Session["VerificationBilling:TransPrescriptionItems" + UniqueID];

                var query = new TransPrescriptionItemQuery("a");
                var item = new ItemQuery("b");
                var header = new TransPrescriptionQuery("c");
                var unit = new ServiceUnitQuery("d");
                var cost = new CostCalculationQuery("e");
                var reg = new RegistrationQuery("f");
                //var pay = new TransPaymentItemOrderQuery("g");
                //var payib = new TransPaymentItemIntermBillQuery("h");

                //var payReff = new TransPaymentItemOrderQuery("i");
                //var payibReff = new TransPaymentItemIntermBillQuery("j");
                var queryReff = new TransPrescriptionItemQuery("k");
                var costReff = new CostCalculationQuery("l");

                //var view = new VwTransactionQuery("x");
                var cls = new ClassQuery("cls");
                var tpReff = new TransPrescriptionQuery("tpReff");

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = header.PrescriptionDate.Cast(esCastType.String);

                var std = new AppStandardReferenceItemQuery("z");

                query.Select
                    (
                        header.RegistrationNo,
                        query.PrescriptionNo.As("TransactionNo"),
                        query.SequenceNo,
                        "<CASE WHEN a.IsApprove = 1 AND c.IsApproval = 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsApprove>",
                        query.IsVoid,
                        
                        //query.IsBillProceed,
                        "<CASE WHEN a.IsBillProceed = 1 AND c.IsBillProceed = 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsBillProceed>", 
                        //"<CASE WHEN a.IsBillProceed = 1 OR c.IsBillProceed = 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsBillProceed>", //db:20250429
                        
                        "<CAST(0 AS BIT) AS IsOrderRealization>",
                        "<CASE WHEN a.ItemInterventionID = '' THEN a.ItemID ELSE a.ItemInterventionID END AS ItemID>",
                        query.ResultQty.As("ChargeQuantity"),
                        query.SRItemUnit,
                        query.Price,
                        query.DiscountAmount,
                        query.LastUpdateByUserID,
                        "<CAST(0 AS NUMERIC(18, 2)) AS CitoAmount>",
                        @"<CASE WHEN e.LastUpdateDateTime = '01/01/1900 00:00:00.000' THEN '' 
                            ELSE e.LastUpdateDateTime END AS 'LastUpdateDateTime'>",

                        //@"<CASE WHEN a.IsBillProceed = 1 AND c.IsBillProceed = 1 THEN a.LineAmount
                        //    ELSE 0 END AS Total>",
                        @"<CASE WHEN (a.IsBillProceed = 1 OR c.IsBillProceed = 1) THEN a.LineAmount
                            ELSE 0 END AS Total>", //db:20250429


                        @"<CASE WHEN a.ItemInterventionID = '' THEN b.ItemName 
                            ELSE (SELECT ItemName FROM Item WHERE ItemID = a.ItemInterventionID) END AS ItemName>",
                        header.PrescriptionDate.As("TransactionDate"),
                        header.ServiceUnitID,
                        header.ClassID,
                        unit.ServiceUnitName,
                        "<CAST(0 AS BIT) AS IsOrder>",
                        group.As("Group"),
                        "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                        "<'2' AS TYPE>",
                        reg.IsHoldTransactionEntry,
                        //@"<CASE WHEN g.PaymentNo IS NOT NULL OR h.PaymentNo IS NOT NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsPaymentProceed>",
                        @"<CAST(0 AS BIT) AS IsPaymentProceed>",
                        //@"<CASE WHEN g.PaymentNo IS NOT NULL THEN '[' + g.PaymentNo + ']' ELSE CASE WHEN h.PaymentNo IS NOT NULL THEN '[' + h.PaymentNo + ']' ELSE '' END END AS PaymentNo>",
                        @"<'' AS PaymentNo>",
                        @"<CASE WHEN e.IntermBillNo IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsIntermBillProceed>",
                        @"<CASE WHEN e.IntermBillNo IS NULL THEN '' ELSE ' - ' + e.IntermBillNo END AS IntermBillNo>",
                        //@"<CASE WHEN i.PaymentNo IS NOT NULL OR j.PaymentNo IS NOT NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsPaymentProceedReff>",
                        @"<CAST(0 AS BIT) AS IsPaymentProceedReff>",
                        "<'' AS PatientName>",
                        //view.IsCorrection,
                        //view.OrderDate,
                        //view.OrderTransNo,
                        header.IsPrescriptionReturn.As("IsCorrection"),
                        @"<ISNULL(tpReff.PrescriptionDate, c.PrescriptionDate) AS OrderDate>",
                        @"<ISNULL(tpReff.PrescriptionNo, c.PrescriptionNo) AS OrderTransNo>",
                        header.ExecutionDate.As("ExecutionDate"),
                        cls.ClassName,
                        query.IsReturned.As("IsCorrected"),
                        //"<CAST(0 AS BIT) AS IsCorrected>"
                        std.ItemName.Coalesce("''").As("DiscountReason"),
                        @"<ISNULL(e.IntermBillNo, '') AS CcIntermBillNo>",
                        @"<ISNULL(l.IntermBillNo, '') AS CcRefIntermBillNo>",
                        header.ReferenceNo, 
                        query.Notes
                    );

                query.LeftJoin(std).On(query.SRDiscountReason == std.ItemID && std.StandardReferenceID == AppEnum.StandardReference.DiscountReason.ToString());

                query.InnerJoin(header).On(query.PrescriptionNo == header.PrescriptionNo);
                query.InnerJoin(reg).On(header.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.LeftJoin(cls).On(header.ClassID == cls.ClassID);
                if (!string.IsNullOrEmpty(cboFilterByItemType.SelectedValue))
                {
                    if (cboFilterByItemType.SelectedValue == "1")
                        query.Where(item.SRItemType.In(ItemType.Service, ItemType.Laboratory, ItemType.Radiology, ItemType.Package));
                    else if (cboFilterByItemType.SelectedValue == "0")
                        query.Where(item.SRItemType.In(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen));
                    else if (cboFilterByItemType.SelectedValue == "21")
                        query.Where(item.SRItemType.In(ItemType.NonMedical, ItemType.Kitchen));
                    else
                        query.Where(item.SRItemType == cboFilterByItemType.SelectedValue);
                }
                //if (!string.IsNullOrEmpty(cboFilterByItemType.SelectedValue))
                //{
                //    if (cboFilterByItemType.SelectedValue == "1")
                //        query.Where(item.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen), item.ItemGroupID != "0199");
                //    else
                //        query.Where(query.Or(item.SRItemType.In(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen), item.ItemGroupID == "0199"));
                //}

                query.InnerJoin(unit).On(header.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(cost).On(
                        query.PrescriptionNo == cost.TransactionNo &&
                        query.SequenceNo == cost.SequenceNo
                    );
                //query.LeftJoin(pay).On(
                //    query.PrescriptionNo == pay.TransactionNo &&
                //    query.SequenceNo == pay.SequenceNo &&
                //    pay.IsPaymentProceed == true &&
                //    pay.IsPaymentReturned == false
                //    );
                //query.LeftJoin(payib).On(
                //    cost.IntermBillNo == payib.IntermBillNo &&
                //    payib.IsPaymentProceed == true &&
                //    payib.IsPaymentReturned == false
                //    );

                //------------------------
                query.LeftJoin(queryReff).On(header.ReferenceNo == queryReff.PrescriptionNo &&
                                             query.SequenceNo == queryReff.SequenceNo);
                query.LeftJoin(costReff).On(header.RegistrationNo == costReff.RegistrationNo &&
                                            queryReff.PrescriptionNo == costReff.TransactionNo &&
                                            query.SequenceNo == costReff.SequenceNo);
                //query.LeftJoin(payReff).On(
                //    header.ReferenceNo == payReff.TransactionNo &&
                //    query.SequenceNo == payReff.SequenceNo &&
                //    payReff.IsPaymentProceed == true &&
                //    payReff.IsPaymentReturned == false
                //    );
                //query.LeftJoin(payibReff).On(
                //    costReff.IntermBillNo == payibReff.IntermBillNo &&
                //    payibReff.IsPaymentProceed == true &&
                //    payibReff.IsPaymentReturned == false
                //    );
                //------------------------------
                //query.InnerJoin(view).On(query.PrescriptionNo == view.TransactionNo);
                query.LeftJoin(tpReff).On(header.ReferenceNo == tpReff.PrescriptionNo);


                query.Where(
                        header.RegistrationNo.In(MergeRegistrationList()),
                        header.IsVoid == false,
                        query.IsVoid == false
                    );

                if (!(string.IsNullOrEmpty(cboFilterByServiceUnitID.SelectedValue)))
                    query.Where(header.ServiceUnitID == cboFilterByServiceUnitID.SelectedValue);
                //if (!(string.IsNullOrEmpty(cboFilterByPaymentStatus.SelectedValue)))
                //{
                //    if (cboFilterByPaymentStatus.SelectedValue == "1")
                //        query.Where(query.Or(pay.PaymentNo.IsNotNull(), payib.PaymentNo.IsNotNull()));
                //    else
                //        query.Where(pay.PaymentNo.IsNull(), payib.PaymentNo.IsNull());
                //}
                if (!(string.IsNullOrEmpty(cboFilterByIntermBillStatus.SelectedValue)))
                    query.Where(cboFilterByIntermBillStatus.SelectedValue == "1" ? cost.IntermBillNo.IsNotNull() : cost.IntermBillNo.IsNull());
                if (!string.IsNullOrEmpty(cboFilterByCheckedStatus.SelectedValue))
                {
                    if (cboFilterByCheckedStatus.SelectedValue == "1")
                        query.Where(cost.IsChecked == true);
                    else
                        query.Where(query.Or(cost.IsChecked.IsNull(), cost.IsChecked == false));
                }
                if (!(string.IsNullOrEmpty(cboFilterByItemGroupID.SelectedValue)))
                    query.Where(item.ItemGroupID == cboFilterByItemGroupID.SelectedValue);
                if (!txtTransDate1.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtTransDate2.SelectedDate.ToString().Trim().Equals(string.Empty))
                    query.Where(header.PrescriptionDate >= txtTransDate1.SelectedDate, header.PrescriptionDate < txtTransDate2.SelectedDate.Value.AddDays(1));

                query.OrderBy(
                    header.PrescriptionDate.Ascending,
                    query.PrescriptionNo.Ascending,
                    //view.OrderTransNo.Ascending,
                    query.SequenceNo.Ascending
                    );

                query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

                DataTable tbl = query.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    var presc = new TransPrescriptionItemCollection();
                    presc.Query.Where(presc.Query.PrescriptionNo == row["TransactionNo"], presc.Query.IsApprove == true,
                                      presc.Query.IsBillProceed == true);
                    presc.LoadAll();
                    decimal subTotal = presc.Sum(x => (x.LineAmount ?? 0));

                    //--cek payment
                    var listPaymentNo = string.Empty;

                    var tpioColl = new TransPaymentItemOrderCollection();
                    tpioColl.Query.Where(tpioColl.Query.TransactionNo == row["TransactionNo"].ToString(),
                                         tpioColl.Query.SequenceNo == row["SequenceNo"].ToString(),
                                         tpioColl.Query.IsPaymentProceed == true,
                                         tpioColl.Query.IsPaymentReturned == false);
                    tpioColl.LoadAll();
                    if (tpioColl.Count > 0)
                    {
                        foreach (var tpio in tpioColl)
                        {
                            if (string.IsNullOrEmpty(listPaymentNo))
                                listPaymentNo = tpio.PaymentNo;
                            else listPaymentNo = listPaymentNo + ", " + tpio.PaymentNo;
                        }
                    }
                    else
                    {
                        var ibColl = new TransPaymentItemIntermBillCollection();
                        ibColl.Query.Where(ibColl.Query.IntermBillNo == row["CcIntermBillNo"].ToString(), ibColl.Query.IsPaymentProceed == true,
                                 ibColl.Query.IsPaymentReturned == false);
                        ibColl.LoadAll();
                        if (ibColl.Count > 0)
                        {
                            foreach (var ib in ibColl)
                            {
                                if (string.IsNullOrEmpty(listPaymentNo))
                                    listPaymentNo = ib.PaymentNo;
                                else listPaymentNo = listPaymentNo + ", " + ib.PaymentNo;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(listPaymentNo))
                    {
                        row["IsPaymentProceed"] = true;
                        row["PaymentNo"] = " [" + listPaymentNo + "]";
                    }

                    //--cek payment reference
                    var tpioRefColl = new TransPaymentItemOrderCollection();
                    tpioRefColl.Query.Where(tpioRefColl.Query.TransactionNo == row["ReferenceNo"].ToString(),
                                         tpioRefColl.Query.SequenceNo == row["SequenceNo"].ToString(),
                                         tpioRefColl.Query.IsPaymentProceed == true,
                                         tpioRefColl.Query.IsPaymentReturned == false);
                    tpioRefColl.LoadAll();

                    if (tpioRefColl.Count > 0)
                    {
                        row["IsPaymentProceedReff"] = true;
                    }
                    else
                    {
                        var ibRefColl = new TransPaymentItemIntermBillCollection();
                        ibRefColl.Query.Where(ibRefColl.Query.IntermBillNo == row["CcRefIntermBillNo"].ToString(),
                                              ibRefColl.Query.IsPaymentProceed == true,
                                              ibRefColl.Query.IsPaymentReturned == false);
                        ibRefColl.LoadAll();
                        if (ibRefColl.Count > 0)
                        {
                            row["IsPaymentProceedReff"] = true;
                        }
                    }

                    row["Group"] = row["ServiceUnitName"] + " - " +
                                   Convert.ToDateTime(row["Group"]).ToString(AppConstant.DisplayFormat.Date) + " - " +
                                   row["TransactionNo"] + " - " + row["ClassName"] + " (Rp. " + string.Format("{0:n2}", (subTotal)) + ")" + row["IntermBillNo"] + row["PaymentNo"];


                    if (!(string.IsNullOrEmpty(cboFilterByPaymentStatus.SelectedValue)))
                    {
                        if (cboFilterByPaymentStatus.SelectedValue == "1")
                        {
                            if (string.IsNullOrEmpty(listPaymentNo))
                                row.Delete();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(listPaymentNo))
                                row.Delete();
                        }
                    }
                }

                tbl.AcceptChanges();

                SetSession("VerificationBilling:TransPrescriptionItems", tbl);
                return tbl;
            }
            set
            { SetSession("VerificationBilling:TransPrescriptionItems", value); }
        }

        private DataTable TransChargesItems
        {
            get
            {
                if (Session["VerificationBilling:TransChargesItems" + UniqueID] != null)
                    return (DataTable)Session["VerificationBilling:TransChargesItems" + UniqueID];

                var query = new TransChargesItemQuery("a");
                var item = new ItemQuery("b");
                var header = new TransChargesQuery("c");
                var unit = new ServiceUnitQuery("d");
                var cost = new CostCalculationQuery("e");
                var reg = new RegistrationQuery("f");
                //var pay = new TransPaymentItemOrderQuery("g");
                //var payib = new TransPaymentItemIntermBillQuery("h");
                //var view = new VwTransactionQuery("x");
                var pat = new PatientQuery("i");
                var cls = new ClassQuery("cls");
                var tcReff = new TransChargesQuery("tcReff");


                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = header.TransactionDate.Cast(esCastType.String);

                var itemCond = new ItemConditionRuleQuery("ic");


                var std = new AppStandardReferenceItemQuery("z");

                query.Select
                    (
                        header.RegistrationNo,
                        query.TransactionNo,
                        query.SequenceNo,
                        //query.IsApprove,
                        @"<CASE WHEN a.IsApprove = 1 AND c.IsApproved = 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsApprove>",
                        query.IsVoid,
                        //query.IsBillProceed,
                        //@"<CASE WHEN a.IsBillProceed = 1 AND c.IsBillProceed = 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsBillProceed>",
                        @"<CASE WHEN a.IsBillProceed = 1 OR c.IsBillProceed = 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsBillProceed>", //db:20250429
                        query.IsOrderRealization,
                        query.ItemID,
                        query.ChargeQuantity,
                        query.SRItemUnit,
                        query.Price,
                        query.DiscountAmount,
                        query.CitoAmount,
                        header.LastUpdateByUserID,
                        @"<CASE WHEN e.LastUpdateDateTime = '01/01/1900 00:00:00.000' THEN '' 
                                ELSE e.LastUpdateDateTime END AS 'LastUpdateDateTime'>",
                        //                        @"<CASE WHEN a.IsApprove = 1 AND a.IsBillProceed = 1 THEN 
                        //                                CASE WHEN c.IsCorrection = 1 
                        //                                    THEN 0 - (((ABS(a.ChargeQuantity) * a.Price) - a.DiscountAmount) + a.CitoAmount)
                        //                                    ELSE ((a.ChargeQuantity * a.Price) - a.DiscountAmount) + a.CitoAmount
                        //                                END
                        //                            ELSE 0 END AS Total>",



                        //@"<CASE WHEN a.IsBillProceed = 1 AND c.IsBillProceed = 1 THEN 
                        //        CASE WHEN c.IsCorrection = 1 
                        //            THEN 0 - (((ABS(a.ChargeQuantity) * a.Price) - a.DiscountAmount) + a.CitoAmount)
                        //            ELSE ((a.ChargeQuantity * a.Price) - a.DiscountAmount) + a.CitoAmount
                        //        END
                        //    ELSE 0 END AS Total>",


                        @"<CASE WHEN (a.IsBillProceed = 1 OR c.IsBillProceed = 1) THEN 
                                CASE WHEN c.IsCorrection = 1 
                                    THEN 0 - (((ABS(a.ChargeQuantity) * a.Price) - a.DiscountAmount) + a.CitoAmount)
                                    ELSE ((a.ChargeQuantity * a.Price) - a.DiscountAmount) + a.CitoAmount
                                END
                            ELSE 0 END AS Total>", //db:20250429



                        //@"<b.[ItemName] + CASE WHEN (
                        //             SELECT TOP 1 p.ParamedicName
                        //             FROM   TransChargesItemComp tcic
                        //                    INNER JOIN TariffComponent tc
                        //                         ON  tc.TariffComponentID = tcic.TariffComponentID
                        //                         AND tc.IsTariffParamedic = 1
                        //                    LEFT JOIN Paramedic p
                        //                         ON  p.ParamedicID = tcic.ParamedicID
                        //             WHERE  tcic.TransactionNo = a.TransactionNo
                        //                    AND tcic.SequenceNo = a.SequenceNo
                        //             ORDER BY
                        //                    tc.TariffComponentID DESC
                        //         ) IS NULL THEN ''
                        //    ELSE ' (' + (
                        //             SELECT TOP 1 p.ParamedicName
                        //             FROM   TransChargesItemComp tcic
                        //                    INNER JOIN TariffComponent tc
                        //                         ON  tc.TariffComponentID = tcic.TariffComponentID
                        //                         AND tc.IsTariffParamedic = 1
                        //                    LEFT JOIN Paramedic p
                        //                         ON  p.ParamedicID = tcic.ParamedicID
                        //             WHERE  tcic.TransactionNo = a.TransactionNo
                        //                    AND tcic.SequenceNo = a.SequenceNo
                        //             ORDER BY
                        //                    tc.TariffComponentID DESC
                        //    ) + ')'
                        //END AS ItemName>",
                        "<b.[ItemName] + case ISNULL(a.[ParamedicCollectionName],'') when '' then '' else (' (' + a.[ParamedicCollectionName] + ')')  end as ItemName>",
                        @"<CASE WHEN ISNULL(ic.ItemConditionRuleName, '') = '' THEN '' ELSE '~ ' + ic.ItemConditionRuleName END AS 'ItemConditionRuleName'>",
                        header.TransactionDate,
                        header.ToServiceUnitID.As("ServiceUnitID"),
                        header.ClassID,
                        unit.ServiceUnitName,
                        header.IsOrder,
                        group.As("Group"),
                        "<'' AS ORDERKEY>",
                        "<'1' AS TYPE>",
                        reg.IsHoldTransactionEntry,
                        //@"<CASE WHEN g.PaymentNo IS NOT NULL OR h.PaymentNo IS NOT NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsPaymentProceed>",
                        @"<CAST(0 AS BIT) AS IsPaymentProceed>",
                        //@"<CASE WHEN g.PaymentNo IS NOT NULL THEN '[' + g.PaymentNo + ']' ELSE CASE WHEN h.PaymentNo IS NOT NULL THEN '[' + h.PaymentNo + ']' ELSE '' END END AS PaymentNo>",
                        @"<'' AS PaymentNo>",
                        @"<CASE WHEN e.IntermBillNo IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsIntermBillProceed>",
                        @"<CASE WHEN e.IntermBillNo IS NULL THEN '' ELSE ' - ' + e.IntermBillNo END AS IntermBillNo>",
                        @"<CAST(0 AS BIT) AS IsPaymentProceedReff>",
                        pat.PatientName,
                        //view.IsCorrection,
                        //view.OrderDate,
                        //view.OrderTransNo,
                        header.IsCorrection,
                        @"<ISNULL(tcReff.TransactionDate, c.TransactionDate) AS OrderDate>",
                        @"<ISNULL(tcReff.TransactionNo, c.TransactionNo) AS OrderTransNo>",
                        header.ExecutionDate,
                        cls.ClassName,
                        query.IsCorrection.As("IsCorrected"),
                        //@"<CAST(0 AS BIT) AS IsCorrected>",
                        std.ItemName.Coalesce("''").As("DiscountReason"),
                        @"<ISNULL(e.IntermBillNo, '') AS CcIntermBillNo>", 
                        query.Notes
                    );

                query.LeftJoin(std).On(query.SRDiscountReason == std.ItemID && std.StandardReferenceID == AppEnum.StandardReference.DiscountReason.ToString());

                query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo);
                query.InnerJoin(reg).On(header.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.LeftJoin(cls).On(header.ClassID == cls.ClassID);
                query.LeftJoin(itemCond).On(itemCond.ItemConditionRuleID == query.ItemConditionRuleID);
                if (!string.IsNullOrEmpty(cboFilterByItemType.SelectedValue))
                {
                    if (cboFilterByItemType.SelectedValue == "1")
                        query.Where(item.SRItemType.In(ItemType.Service, ItemType.Laboratory, ItemType.Radiology, ItemType.Package));
                    else if (cboFilterByItemType.SelectedValue == "0")
                        query.Where(item.SRItemType.In(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen));
                    else if (cboFilterByItemType.SelectedValue == "21")
                        query.Where(item.SRItemType.In(ItemType.NonMedical, ItemType.Kitchen));
                    else
                        query.Where(item.SRItemType == cboFilterByItemType.SelectedValue);
                }
                //if (!string.IsNullOrEmpty(cboFilterByItemType.SelectedValue))
                //{
                //    if (cboFilterByItemType.SelectedValue == "1")
                //        query.Where(item.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen), item.ItemGroupID != "0199");
                //    else
                //        query.Where(query.Or(item.SRItemType.In(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen), item.ItemGroupID == "0199"));
                //}

                query.InnerJoin(unit).On(header.ToServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(cost).On(
                        query.TransactionNo == cost.TransactionNo &&
                        query.SequenceNo == cost.SequenceNo
                    );
                //query.LeftJoin(pay).On(
                //    query.TransactionNo == pay.TransactionNo &&
                //    query.SequenceNo == pay.SequenceNo &&
                //    pay.IsPaymentProceed == true &&
                //    pay.IsPaymentReturned == false
                //    );
                //query.LeftJoin(payib).On(
                //    cost.IntermBillNo == payib.IntermBillNo &&
                //    payib.IsPaymentProceed == true &&
                //    payib.IsPaymentReturned == false
                //    );
                //query.InnerJoin(view).On(query.TransactionNo == view.TransactionNo);
                query.LeftJoin(tcReff).On(header.ReferenceNo == tcReff.TransactionNo);
                
                query.Where(
                        header.RegistrationNo.In(MergeRegistrationList()),
                         query.Or(
                            header.PackageReferenceNo == string.Empty,
                            header.PackageReferenceNo.IsNull()
                            ),
                        header.IsVoid == false,
                        query.IsVoid == false,
                        query.Or(
                        query.ParentNo == string.Empty,
                        query.ParentNo.IsNull()
                        )
                    );

                if (!(string.IsNullOrEmpty(cboFilterByServiceUnitID.SelectedValue)))
                    query.Where(header.ToServiceUnitID == cboFilterByServiceUnitID.SelectedValue);
                //if (!(string.IsNullOrEmpty(cboFilterByPaymentStatus.SelectedValue)))
                //{
                //    if (cboFilterByPaymentStatus.SelectedValue == "1")
                //        query.Where(query.Or(pay.PaymentNo.IsNotNull(), payib.PaymentNo.IsNotNull()));
                //    else
                //        query.Where(pay.PaymentNo.IsNull(), payib.PaymentNo.IsNull());
                //}
                if (!(string.IsNullOrEmpty(cboFilterByIntermBillStatus.SelectedValue)))
                {
                    if (cboFilterByIntermBillStatus.SelectedValue == "1")
                        query.Where(cost.IntermBillNo.IsNotNull());
                    else
                        query.Where(cost.IntermBillNo.IsNull());
                }
                if (!string.IsNullOrEmpty(cboFilterByCheckedStatus.SelectedValue))
                {
                    if (cboFilterByCheckedStatus.SelectedValue == "1")
                        query.Where(cost.IsChecked == true);
                    else
                        query.Where(query.Or(cost.IsChecked.IsNull(), cost.IsChecked == false));
                }
                if (!(string.IsNullOrEmpty(cboFilterByItemGroupID.SelectedValue)))
                    query.Where(item.ItemGroupID == cboFilterByItemGroupID.SelectedValue);
                //if (!txtTransDate1.IsEmpty && !txtTransDate2.IsEmpty)
                //    query.Where(view.FilterDate.Date().Between(txtTransDate1.SelectedDate.Value.Date, txtTransDate2.SelectedDate.Value.Date));

                if (!txtTransDate1.IsEmpty && !txtTransDate2.IsEmpty)
                    query.Where(@"<ISNULL(tcReff.ExecutionDate, c.ExecutionDate) >= '" + txtTransDate1.SelectedDate.Value.Date + "' AND ISNULL(tcReff.ExecutionDate, c.ExecutionDate) < '" + txtTransDate2.SelectedDate.Value.AddDays(1) + "'>");

                query.OrderBy
                    (
                        @"<ISNULL(tcReff.ExecutionDate, c.ExecutionDate) ASC>",
                        query.TransactionNo.Ascending,
                        //view.OrderTransNo.Ascending,
                        query.SequenceNo.Ascending
                    );

                DataTable tbl = query.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    var charges = new TransChargesItemCollection();
                    //charges.Query.Where(charges.Query.TransactionNo == row["TransactionNo"], charges.Query.IsVoid == false);
                    charges.Query.Where(charges.Query.TransactionNo == row["TransactionNo"], charges.Query.IsBillProceed == true, charges.Query.IsVoid == false);
                    charges.LoadAll();
                    decimal subTotal = 0;
                    foreach (var x in charges)
                    {
                        //if ((bool)row["IsApprove"] & (bool)row["IsBillProceed"])
                        {
                            if ((bool)row["IsCorrection"])
                                subTotal += (0 -
                                             ((Math.Abs(x.ChargeQuantity ?? 0) * (x.Price ?? 0)) - (x.DiscountAmount ?? 0) +
                                              (x.CitoAmount ?? 0)));
                            else
                                subTotal += (((x.ChargeQuantity ?? 0) * (x.Price ?? 0)) - (x.DiscountAmount ?? 0) + (x.CitoAmount ?? 0));
                        }
                    }

                    //--cek payment
                    var listPaymentNo = string.Empty;

                    var tpioColl = new TransPaymentItemOrderCollection();
                    tpioColl.Query.Where(tpioColl.Query.TransactionNo == row["TransactionNo"].ToString(),
                                         tpioColl.Query.SequenceNo == row["SequenceNo"].ToString(),
                                         tpioColl.Query.IsPaymentProceed == true,
                                         tpioColl.Query.IsPaymentReturned == false);
                    tpioColl.LoadAll();
                    if (tpioColl.Count > 0)
                    {
                        foreach (var tpio in tpioColl)
                        {
                            if (string.IsNullOrEmpty(listPaymentNo))
                                listPaymentNo = tpio.PaymentNo;
                            else listPaymentNo = listPaymentNo + ", " + tpio.PaymentNo;
                        }
                    }
                    else
                    {
                        var ibColl = new TransPaymentItemIntermBillCollection();
                        ibColl.Query.Where(ibColl.Query.IntermBillNo == row["CcIntermBillNo"].ToString(), ibColl.Query.IsPaymentProceed == true,
                                 ibColl.Query.IsPaymentReturned == false);
                        ibColl.LoadAll();
                        if (ibColl.Count > 0)
                        {
                            foreach (var ib in ibColl)
                            {
                                if (string.IsNullOrEmpty(listPaymentNo))
                                    listPaymentNo = ib.PaymentNo;
                                else listPaymentNo = listPaymentNo + ", " + ib.PaymentNo;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(listPaymentNo))
                    {
                        row["IsPaymentProceed"] = true;
                        row["PaymentNo"] = " [" + listPaymentNo + "]";
                    }

                    row["Group"] = row["ServiceUnitName"] + " - " +
                                  Convert.ToDateTime(row["Group"]).ToString(AppConstant.DisplayFormat.Date) + " - " +
                                  row["TransactionNo"] + " - " + row["ClassName"] + " (Rp. " + string.Format("{0:n2}", (subTotal)) + ")" + row["IntermBillNo"] + row["PaymentNo"];
                    if ((bool)row["IsOrder"] && !(bool)row["IsOrderRealization"])
                        row["Total"] = 0D;

                    if (!(string.IsNullOrEmpty(cboFilterByPaymentStatus.SelectedValue)))
                    {
                        if (cboFilterByPaymentStatus.SelectedValue == "1")
                        {
                            if (string.IsNullOrEmpty(listPaymentNo))
                                row.Delete();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(listPaymentNo))
                                row.Delete();
                        }
                    }
                }

                tbl.AcceptChanges();

                var chargeBedItems = new ServiceRoomCollection();
                chargeBedItems.Query.Where(chargeBedItems.Query.ItemID != string.Empty);
                chargeBedItems.LoadAll();

                var chargeBeds = tbl.AsEnumerable().Where(t => chargeBedItems.Select(c => c.ItemID).Contains(t.Field<string>("ItemID")));
                if (chargeBedItems != null)
                {
                    foreach (DataRow chargeBed in chargeBeds)
                    {
                        chargeBed["ItemName"] = chargeBed["ItemName"].ToString() + " (" + chargeBed["PatientName"].ToString() + ")";
                    }
                }

                SetSession("VerificationBilling:TransChargesItems", tbl);

                return tbl;
            }
            set
            { SetSession("VerificationBilling:TransChargesItems", value); }
        }

        #endregion

        #region Tab: Interm Bill List

        protected void btnPrintBillToClass_Click(object sender, EventArgs e)
        {
            var coll = new TransChargesItemTempCoverageCollection();
            coll.Query.Where(coll.Query.RegistrationNo == txtRegistrationNo.Text &&
                             coll.Query.ChargeClassID == cboBillToClassID.SelectedValue);
            coll.LoadAll();
            if (coll.Count == 0)
                return;

            var jobParameters = new PrintJobParameterCollection();

            var parRegNo = jobParameters.AddNew();
            parRegNo.Name = "RegNo";
            parRegNo.ValueString = txtRegistrationNo.Text;

            var parUser = jobParameters.AddNew();
            parUser.Name = "UserName";
            parUser.ValueString = AppSession.UserLogin.UserName;

            var parUserId = jobParameters.AddNew();
            parUserId.Name = "UserID";
            parUserId.ValueString = AppSession.UserLogin.UserID;

            var parPaymentNo = jobParameters.AddNew();
            parPaymentNo.Name = "PaymentNo";
            parPaymentNo.ValueString = string.Empty;

            var parInitialRpt = jobParameters.AddNew();
            parInitialRpt.Name = "InitialRpt";
            parInitialRpt.ValueString = "2";

            var parClassID = jobParameters.AddNew();
            parClassID.Name = "ClassID";
            parClassID.ValueString = cboBillToClassID.SelectedValue;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = AppConstant.Report.RssaBillingToSelectedClassStatementDetail;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
            "oWnd.Show();" +
            "oWnd.Maximize();";
            RadAjaxPanel1.ResponseScripts.Add(script);
        }

        protected void btnProcessBillToClass_Click(object sender, EventArgs e)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);
            if (reg.SRRegistrationType != AppConstant.RegistrationType.InPatient)
            {
                pnlInfo2.Visible = true;
                lblInfo2.Text = "Billing To Selected Class can't be proceed. Selected patient not inpatient.";
                pnlInfo3.Visible = false;
                lblInfo3.Text = string.Empty;
                return;
            }
            //var g = new Guarantor();
            //g.LoadByPrimaryKey(reg.GuarantorID);
            //if (g.SRGuarantorType != _guarantorTypeSelf)
            //{
            //    pnlInfo2.Visible = true;
            //    lblInfo2.Text = "Billing To Selected Class can't be proceed. Patient guarantor is not allow to process this billing.";
            //    return;
            //}

            //if (Convert.ToInt16(reg.ChargeClassID) >= Convert.ToInt16(cboBillToClassID.SelectedValue))
            //{
            //    pnlInfo2.Visible = true;
            //    lblInfo2.Text = "Billing To Selected Class can't be proceed. Selected class must be under charge class.";
            //    return;
            //}

            string[] intermBillNoList = IntermBillList();
            var ib = intermBillNoList.Aggregate(string.Empty, (current, str) => current + (str + ","));

            if (ib == string.Empty)
            {
                pnlInfo2.Visible = true;
                lblInfo2.Text = "Process Billing To Selected Class failed. There are no selected Interm Bill.";
                pnlInfo3.Visible = false;
                lblInfo3.Text = string.Empty;
            }
            else
            {
                var costs = new CostCalculationCollection();
                costs.Query.Where(costs.Query.IntermBillNo.In(intermBillNoList));
                costs.LoadAll();
                if (costs.Count == 0)
                {
                    pnlInfo2.Visible = true;
                    lblInfo2.Text = "Process Billing To Selected Class failed. There are no item can't be proceed.";
                    pnlInfo3.Visible = false;
                    lblInfo3.Text = string.Empty;
                }
                else
                {
                    using (var trans = new esTransactionScope())
                    {
                        var chargesTemp = new TransChargesItemTempCoverageCollection();
                        chargesTemp.Query.Where(chargesTemp.Query.RegistrationNo == txtRegistrationNo.Text,
                                                chargesTemp.Query.ChargeClassID == cboBillToClassID.SelectedValue);
                        chargesTemp.LoadAll();
                        chargesTemp.MarkAllAsDeleted();
                        chargesTemp.Save();

                        var prescTemp = new TransPrescriptionItemTempCoverageCollection();
                        prescTemp.Query.Where(prescTemp.Query.RegistrationNo == txtRegistrationNo.Text,
                                              prescTemp.Query.ChargeClassID == cboBillToClassID.SelectedValue);
                        prescTemp.LoadAll();
                        prescTemp.MarkAllAsDeleted();
                        prescTemp.Save();

                        foreach (var item in costs)
                        {
                            var tci = new TransChargesItem();
                            if (tci.LoadByPrimaryKey(item.TransactionNo, item.SequenceNo))
                            {
                                var ct = new TransChargesItemTempCoverage();
                                ct.AddNew();
                                ct.RegistrationNo = txtRegistrationNo.Text;
                                ct.TransactionNo = tci.TransactionNo;
                                ct.SequenceNo = tci.SequenceNo;
                                ct.ReferenceNo = tci.ReferenceNo;
                                ct.ReferenceSequenceNo = tci.ReferenceSequenceNo;
                                ct.ItemID = tci.ItemID;
                                ct.ChargeClassID = cboBillToClassID.SelectedValue;
                                ct.ChargeQuantity = tci.ChargeQuantity;

                                var tc = new TransCharges();
                                tc.LoadByPrimaryKey(item.TransactionNo);

                                var regTc = new Registration();
                                regTc.LoadByPrimaryKey(tc.RegistrationNo);

                                var grr = new Guarantor();
                                grr.LoadByPrimaryKey(regTc.GuarantorID);

                                ItemTariff tariff = (Helper.Tariff.GetItemTariff(tc.TransactionDate.Value.Date, grr.SRTariffType, cboBillToClassID.SelectedValue, cboBillToClassID.SelectedValue, item.ItemID, cboGuarantorID.SelectedValue, false, regTc.SRRegistrationType) ??
                                                     Helper.Tariff.GetItemTariff(tc.TransactionDate.Value.Date, grr.SRTariffType, _defaultTariffClass, cboBillToClassID.SelectedValue, item.ItemID, cboGuarantorID.SelectedValue, false, regTc.SRRegistrationType)) ??
                                                    (Helper.Tariff.GetItemTariff(tc.TransactionDate.Value.Date, _defaultTariffType, cboBillToClassID.SelectedValue, cboBillToClassID.SelectedValue, item.ItemID, cboGuarantorID.SelectedValue, false, regTc.SRRegistrationType) ??
                                                     Helper.Tariff.GetItemTariff(tc.TransactionDate.Value.Date, _defaultTariffType, _defaultTariffClass, cboBillToClassID.SelectedValue, item.ItemID, cboGuarantorID.SelectedValue, false, regTc.SRRegistrationType));

                                if (tariff != null)
                                {
                                    if (!(tariff.IsAllowVariable ?? false))
                                        ct.Price = tariff.Price ?? 0;
                                    else
                                        ct.Price = tci.Price;
                                }
                                else
                                    ct.Price = 0;

                                ct.ParamedicCollectionName = tci.ParamedicCollectionName;
                                ct.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                ct.LastUpdateByUserID = AppSession.UserLogin.UserID;

                                ct.Save();
                            }
                            else
                            {
                                var tpi = new TransPrescriptionItem();
                                if (tpi.LoadByPrimaryKey(item.TransactionNo, item.SequenceNo))
                                {
                                    var pt = new TransPrescriptionItemTempCoverage();
                                    pt.AddNew();
                                    pt.RegistrationNo = txtRegistrationNo.Text;
                                    pt.PrescriptionNo = tpi.PrescriptionNo;
                                    pt.SequenceNo = tpi.SequenceNo;
                                    pt.ItemID = tpi.ItemID;
                                    pt.ChargeClassID = cboBillToClassID.SelectedValue;
                                    pt.ResultQty = tpi.ResultQty;
                                    pt.Price = tpi.Price;
                                    pt.LineAmount = tpi.LineAmount;
                                    pt.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    pt.LastUpdateByUserID = AppSession.UserLogin.UserID;

                                    pt.Save();
                                }
                            }
                        }

                        trans.Complete();
                    }

                    pnlInfo2.Visible = true;
                    lblInfo2.Text = "Process Billing To Selected Class completed.";
                    pnlInfo3.Visible = false;
                    lblInfo3.Text = string.Empty;
                }
            }
        }

        protected void lbtnSaveAdmDisc_Click(object sender, EventArgs e)
        {
            SaveAdmDisc();

            IntermBills = null;
            grdIntermBill.Rebind();
        }

        private void SaveAdmDisc()
        {
            double discpat = 0;
            double discguar = 0;
            foreach (GridDataItem dataItem in grdIntermBill.MasterTableView.Items)
            {
                var txtDiscAdmPatient = (dataItem.FindControl("txtDiscAdmPatient") as RadNumericTextBox);
                var txtDiscAdmGuarantor = (dataItem.FindControl("txtDiscAdmGuarantor") as RadNumericTextBox);
                var patAdm = string.IsNullOrEmpty(txtDiscAdmPatient.Text) ? 0 : txtDiscAdmPatient.Value;
                var guarAdm = string.IsNullOrEmpty(txtDiscAdmGuarantor.Text) ? 0 : txtDiscAdmGuarantor.Value;
                var ibNo = dataItem.GetDataKeyValue("IntermBillNo").ToString();

                discpat += (patAdm ?? 0);
                discguar += (guarAdm ?? 0);

                var ib = new IntermBill();
                ib.LoadByPrimaryKey(ibNo);
                ib.DiscAdmPatient = Convert.ToDecimal(patAdm);
                ib.DiscAdmGuarantor = Convert.ToDecimal(guarAdm);
                ib.Save();
            }
            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);
            reg.DiscAdmPatient = Convert.ToDecimal(discpat);
            reg.DiscAdmGuarantor = Convert.ToDecimal(discguar);
            reg.Save();
        }

        protected void grdIntermBill_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdIntermBill.DataSource = IntermBills;
        }

        private DataTable IntermBills
        {
            get
            {
                if (Session["VerificationBilling:IntermBills" + UniqueID] != null)
                    return (DataTable)Session["VerificationBilling:IntermBills" + UniqueID];

                var query = new IntermBillQuery("a");
                query.Select
                    (
                        query.IntermBillNo,
                        query.IntermBillDate,
                        query.StartDate,
                        query.EndDate,
                        query.PatientAmount,
                        query.GuarantorAmount,
                        query.AdministrationAmount,
                        query.GuarantorAdministrationAmount,
                        "<ISNULL(a.DiscAdmPatient, 0) AS DiscAdmPatient>",
                        "<ISNULL(a.DiscAdmGuarantor, 0) AS DiscAdmGuarantor>",
                        //"<CASE WHEN b.PaymentNo IS NOT NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsPaid>",
                        //"<CASE WHEN b.PaymentNo IS NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsUnPaid>",
                        "<CAST(0 AS BIT) AS IsPaid>",
                        "<CAST(1 AS BIT) AS IsUnPaid>",
                        //"<CAST(1 AS BIT) AS IsUnPaid>",
                        //"<CASE WHEN c.PaymentNo IS NOT NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsPaid2>",
                        "<CAST(0 AS BIT) AS IsPaid2>",
                        //"<CASE WHEN x.PaymentNo IS NOT NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsPatientPaid>",
                        "<CAST(0 AS BIT) AS IsPatientPaid>",
                        "<ISNULL(a.AskesCoveredSeqNo, '') AS AskesCoveredSeqNo>",
                        query.LastUpdateByUserID,
                        @"<'' AS PaymentNo>",
                        "<CAST(0 AS BIT) AS IsNotAllowEdit>",
                        query.CreatedDateTime,
                        query.CreatedByUserID
                    );

                query.Where(
                        query.RegistrationNo.In(MergeRegistrationList()),
                        query.IsVoid == false
                    );
                query.OrderBy(
                        query.IntermBillNo.Ascending
                    );

                DataTable tbl = query.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    var cc = new CostCalculationCollection();
                    cc.Query.Where(cc.Query.IntermBillNo == row["IntermBillNo"].ToString(),
                                   cc.Query.RegistrationNo.In(MergeRegistrationList()));
                    cc.LoadAll();
                    if (cc.Count == 0)
                        row.Delete();
                    else
                    {
                        var ListPaymentNo = string.Empty;
                        if (rblToGuarantor.SelectedIndex == 1)
                        {
                            var py = new TransPaymentItemIntermBillCollection();
                            py.Query.Where(py.Query.IntermBillNo == row["IntermBillNo"].ToString(),
                                           py.Query.IsPaymentProceed == true, py.Query.IsPaymentReturned == false);
                            py.LoadAll();
                            if (py.Count > 0)
                            {
                                row["IsPaid"] = true;
                                row["IsUnPaid"] = false;
                                row["IsPatientPaid"] = true;

                                foreach (var p in py)
                                {
                                    if (string.IsNullOrEmpty(ListPaymentNo))
                                        ListPaymentNo = p.PaymentNo + "(P)";
                                    else ListPaymentNo = ListPaymentNo + ", " + p.PaymentNo + "(P)";
                                }
                            }


                            var py2 = new TransPaymentItemIntermBillGuarantorCollection();
                            py2.Query.Where(py2.Query.IntermBillNo == row["IntermBillNo"].ToString(),
                                            py2.Query.IsPaymentProceed == true, py2.Query.IsPaymentReturned == false);

                            py2.LoadAll();
                            if (py2.Count > 0)
                            {
                                row["IsPaid2"] = true;

                                foreach (var p in py2)
                                {
                                    if (string.IsNullOrEmpty(ListPaymentNo))
                                        ListPaymentNo = p.PaymentNo + "(C)";
                                    else ListPaymentNo = ListPaymentNo + ", " + p.PaymentNo + "(C)";
                                }
                            }
                        }
                        else
                        {
                            var py2 = new TransPaymentItemIntermBillCollection();
                            py2.Query.Where(py2.Query.IntermBillNo == row["IntermBillNo"].ToString(),
                                           py2.Query.IsPaymentProceed == true, py2.Query.IsPaymentReturned == false);
                            py2.LoadAll();
                            if (py2.Count > 0)
                            {
                                row["IsPaid2"] = true;
                                row["IsPatientPaid"] = true;

                                foreach (var p in py2)
                                {
                                    if (string.IsNullOrEmpty(ListPaymentNo))
                                        ListPaymentNo = p.PaymentNo + "(P)";
                                    else ListPaymentNo = ListPaymentNo + ", " + p.PaymentNo + "(P)";
                                }
                            }

                            var py = new TransPaymentItemIntermBillGuarantorCollection();
                            py.Query.Where(py.Query.IntermBillNo == row["IntermBillNo"].ToString(),
                                            py.Query.IsPaymentProceed == true, py.Query.IsPaymentReturned == false);
                            py.LoadAll();
                            if (py.Count > 0)
                            {
                                row["IsPaid"] = true;
                                row["IsUnPaid"] = false;

                                foreach (var p in py)
                                {
                                    if (string.IsNullOrEmpty(ListPaymentNo))
                                        ListPaymentNo = p.PaymentNo + "(C)";
                                    else ListPaymentNo = ListPaymentNo + ", " + p.PaymentNo + "(C)";
                                }
                            }
                        }

                        row["PaymentNo"] = ListPaymentNo;

                        if ((bool)row["IsPaid"] || (bool)row["IsPaid2"])
                        {
                            row["IsNotAllowEdit"] = true;
                        }
                    }
                }
                tbl.AcceptChanges();

                SetSession("VerificationBilling:IntermBills", tbl);

                return tbl;
            }
            set { SetSession("VerificationBilling:IntermBills", value); }
        }

        protected void grdIntermBill_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            #region old
            //var query = new CostCalculationQuery("a");
            //var transQ = new VwTransactionQuery("b");
            //var itemQ = new ItemQuery("c");
            //var suQ = new ServiceUnitQuery("d");

            //query.InnerJoin(transQ).On(query.TransactionNo == transQ.TransactionNo, query.RegistrationNo == transQ.RegistrationNo);
            //query.InnerJoin(itemQ).On(query.ItemID == itemQ.ItemID);
            //query.InnerJoin(suQ).On(transQ.ServiceUnitID == suQ.ServiceUnitID);

            //query.Select
            //    (
            //        query,
            //        itemQ.ItemName,
            //        transQ.TransactionDate,
            //        suQ.ServiceUnitName
            //    );
            //query.OrderBy(suQ.ServiceUnitName.Ascending, transQ.TransactionDate.Ascending);
            //query.Where
            //    (
            //        query.IntermBillNo == e.DetailTableView.ParentItem.GetDataKeyValue("IntermBillNo").ToString()
            //    );

            //e.DetailTableView.DataSource = query.LoadDataTable();
            #endregion

            var query = new CostCalculationQuery("a");
            var transQ = new TransChargesQuery("b");
            var itemQ = new ItemQuery("c");
            var suQ = new ServiceUnitQuery("d");

            query.InnerJoin(transQ).On(query.TransactionNo == transQ.TransactionNo && query.RegistrationNo == transQ.RegistrationNo);
            query.InnerJoin(itemQ).On(query.ItemID == itemQ.ItemID);
            query.InnerJoin(suQ).On(transQ.ToServiceUnitID == suQ.ServiceUnitID);

            query.Select
                (
                    query,
                    itemQ.ItemName,
                    transQ.TransactionDate,
                    suQ.ServiceUnitName
                );
            query.OrderBy(suQ.ServiceUnitName.Ascending, transQ.TransactionDate.Ascending);
            query.Where
                (
                    query.IntermBillNo == e.DetailTableView.ParentItem.GetDataKeyValue("IntermBillNo").ToString()
                );

            DataTable dtb = query.LoadDataTable();

            query = new CostCalculationQuery("a");
            var prescQ = new TransPrescriptionQuery("b");
            itemQ = new ItemQuery("c");
            suQ = new ServiceUnitQuery("d");

            query.InnerJoin(prescQ).On(query.TransactionNo == prescQ.PrescriptionNo && query.RegistrationNo == prescQ.RegistrationNo);
            query.InnerJoin(itemQ).On(query.ItemID == itemQ.ItemID);
            query.InnerJoin(suQ).On(prescQ.ServiceUnitID == suQ.ServiceUnitID);

            query.Select
                (
                    query,
                    itemQ.ItemName,
                    prescQ.PrescriptionDate.As("TransactionDate"),
                    suQ.ServiceUnitName
                );
            query.OrderBy(suQ.ServiceUnitName.Ascending, prescQ.PrescriptionDate.Ascending);
            query.Where
                (
                    query.IntermBillNo == e.DetailTableView.ParentItem.GetDataKeyValue("IntermBillNo").ToString()
                );

            DataTable dtb2 = query.LoadDataTable();

            dtb.Merge(dtb2);

            e.DetailTableView.DataSource = dtb;
        }

        protected void rblToGuarantor_OnTextChanged(object sender, EventArgs e)
        {
            IntermBills = null;
            grdIntermBill.Rebind();
        }

        #endregion

        #region Tab: Verified Transaction

        protected void lbtnProcessChecked_Click(object sender, EventArgs e)
        {
            ProcessChecked();

            Transactions = null;
            TransChargesItems = null;
            TransPrescriptionItems = null;
            grdTransChargesItem.Rebind();

            CostCalculations = null;
            grdCostCalculation.Rebind();
        }

        protected void lbtnSaveVerified_Click(object sender, EventArgs e)
        {
            SaveManualReCalculate();
            CostCalculations = null;
            grdCostCalculation.Rebind();

            lblInfo2.Text = "Save Verified Transaction completed.";
            pnlInfo2.Visible = true;
            pnlInfo3.Visible = false;
            lblInfo3.Text = string.Empty;
        }

        private void SaveManualReCalculate()
        {
            //foreach (GridDataItem dataItem in grdCostCalculation.MasterTableView.Items)
            //{
            //    var txtGuarantorAmount = (dataItem.FindControl("txtGuarantorAmount") as RadNumericTextBox);
            //    var guarantor = string.IsNullOrEmpty(txtGuarantorAmount.Text) ? 0 : txtGuarantorAmount.Value;

            //    var txtDiscountAmount2 = (dataItem.FindControl("txtDiscountAmount2") as RadNumericTextBox);
            //    var disc2 = string.IsNullOrEmpty(txtDiscountAmount2.Text) ? 0 : txtDiscountAmount2.Value;

            //    using (var trans = new esTransactionScope())
            //    {
            //        var cost = CostCalculations.FindByPrimaryKey(dataItem["RegistrationNo"].Text, dataItem["TransactionNo"].Text, dataItem["SequenceNo"].Text);
            //        if (cost != null)
            //        {
            //            var total = cost.GuarantorAmount + cost.PatientAmount;
            //            cost.GuarantorAmount = Convert.ToDecimal(guarantor);
            //            cost.DiscountAmount2 = Convert.ToDecimal(disc2);
            //            cost.PatientAmount = total - Convert.ToDecimal(guarantor) - Convert.ToDecimal(disc2);
            //        }

            //        CostCalculations.Save();
            //        trans.Complete();
            //    }
            //}

            using (var trans = new esTransactionScope())
            {
                foreach (GridDataItem dataItem in grdCostCalculation.MasterTableView.Items)
                {
                    var cc = new CostCalculation();
                    if (cc.LoadByPrimaryKey(dataItem["RegistrationNo"].Text, dataItem["TransactionNo"].Text, dataItem["SequenceNo"].Text))
                    {
                        var IsCorrection =
                            !dataItem["ReferenceNo"].Text.Replace("&nbsp;", string.Empty).Equals(string.Empty) ||
                            (cc.GuarantorAmount ?? 0) + (cc.PatientAmount ?? 0) < 0;
                        decimal Corr = IsCorrection ? -1 : 1;

                        decimal total = (cc.GuarantorAmount ?? 0) + (cc.PatientAmount ?? 0) + (cc.DiscountAmount ?? 0) + (cc.DiscountAmount2 ?? 0);

                        var txtGuarantorAmount = (dataItem.FindControl("txtGuarantorAmount") as RadNumericTextBox);
                        var guarantor = string.IsNullOrEmpty(txtGuarantorAmount.Text) ? 0 : txtGuarantorAmount.Value;

                        var txtDiscountAmount2 = (dataItem.FindControl("txtDiscountAmount2") as RadNumericTextBox);
                        var disc2 = string.IsNullOrEmpty(txtDiscountAmount2.Text) ? 0 :
                            (txtDiscountAmount2.Value * Convert.ToDouble(Corr)) < 0 ? 0 : txtDiscountAmount2.Value;

                        cc.GuarantorAmount = Convert.ToDecimal(guarantor);
                        cc.PatientAmount = total - cc.GuarantorAmount - (cc.DiscountAmount ?? 0);
                        if ((Convert.ToDecimal(disc2) * Corr) > ((cc.PatientAmount ?? 0) * Corr))
                            disc2 = Convert.ToDouble(cc.PatientAmount);
                        cc.DiscountAmount2 = Convert.ToDecimal(disc2);
                        cc.PatientAmount = cc.PatientAmount - cc.DiscountAmount2;

                        decimal tdisc = (cc.DiscountAmount ?? 0) + (cc.DiscountAmount2 ?? 0);

                        if (tdisc > 0)
                        {
                            var tci = new TransChargesItem();
                            if (tci.LoadByPrimaryKey(dataItem["TransactionNo"].Text, dataItem["SequenceNo"].Text))
                            {
                                decimal price = tci.Price ?? 0;
                                decimal qty = tci.ChargeQuantity ?? 0;
                                tci.DiscountAmount = tdisc;
                                tci.Save();

                                var tcic = new TransChargesItemCompCollection();
                                tcic.Query.Where(tcic.Query.TransactionNo == dataItem["TransactionNo"].Text,
                                                 tcic.Query.SequenceNo == dataItem["SequenceNo"].Text);
                                tcic.LoadAll();

                                if (tcic.Count > 0)
                                {
                                    foreach (var i in tcic)
                                    {
                                        //i.DiscountAmount = i.Price * (tdisc / (price * qty));
                                        i.DiscountAmount = Math.Round(i.Price * (tdisc / (price * qty)) ?? 0, 2);
                                    }
                                    tcic.Save();
                                }
                            }
                            else
                            {
                                var tpi = new TransPrescriptionItem();
                                if (tpi.LoadByPrimaryKey(dataItem["TransactionNo"].Text, dataItem["SequenceNo"].Text))
                                {
                                    tpi.DiscountAmount = tdisc;
                                    tpi.LineAmount = cc.PatientAmount + cc.GuarantorAmount;
                                    tpi.Save();
                                }
                            }
                        }

                        cc.Save();
                    }
                }
                trans.Complete();
            }
        }

        protected void lbtnProcessVerifiedPatientToGuarantor_Click(object sender, EventArgs e)
        {
            ProcessManualReCalculate(true);
            CostCalculations = null;
            grdCostCalculation.Rebind();

            lblInfo2.Text = "Process Verified Transaction (Patient to Guarantor) completed.";
            pnlInfo2.Visible = true;
            pnlInfo3.Visible = false;
            lblInfo3.Text = string.Empty;
        }

        protected void lbtnProcessVerifiedGuarantorToPatient_Click(object sender, EventArgs e)
        {
            ProcessManualReCalculate(false);
            CostCalculations = null;
            grdCostCalculation.Rebind();

            lblInfo2.Text = "Process Verified Transaction (Guarantor to Patient) completed.";
            pnlInfo2.Visible = true;
            pnlInfo3.Visible = false;
            lblInfo3.Text = string.Empty;
        }

        private void ProcessManualReCalculate(bool patientToGuarantor)
        {
            foreach (GridDataItem dataItem in grdCostCalculation.MasterTableView.Items)
            {
                if (!((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                    continue;

                using (var trans = new esTransactionScope())
                {
                    var cost = CostCalculations.FindByPrimaryKey(dataItem["RegistrationNo"].Text, dataItem["TransactionNo"].Text, dataItem["SequenceNo"].Text);
                    if (cost != null)
                    {
                        var total = cost.GuarantorAmount + cost.PatientAmount;
                        cost.GuarantorAmount = patientToGuarantor ? total : 0;
                        cost.PatientAmount = patientToGuarantor ? 0 : total;
                    }

                    CostCalculations.Save();
                    trans.Complete();
                }
            }

            var hist = new BillTransferHistory();
            hist.AddNew();
            hist.RegistrationNo = txtRegistrationNo.Text;
            hist.ProcessDateTime = (new DateTime()).NowAtSqlServer();
            hist.ProcessByUserID = AppSession.UserLogin.UserID;
            hist.IsPatientToGuarantor = patientToGuarantor;

            hist.Save();
        }

        private void ProcessChecked()
        {
            foreach (GridDataItem dataItem in grdCostCalculation.MasterTableView.Items)
            {
                if (!((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                    continue;

                using (var trans = new esTransactionScope())
                {
                    var cost = CostCalculations.FindByPrimaryKey(dataItem["RegistrationNo"].Text, dataItem["TransactionNo"].Text, dataItem["SequenceNo"].Text);
                    if (cost != null)
                    {
                        cost.IsChecked = true;
                    }

                    CostCalculations.Save();
                    trans.Complete();
                }
            }
        }

        protected void lbtnPrintPreviewVerified_Click(object sender, EventArgs e)
        {
            var jobParameters = new PrintJobParameterCollection();
            string[] registrationNoList = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);

            switch (_healthcareInitial)
            {
                case "RSGPI":
                case "ARSANI":

                    {
                        var ibParameter = jobParameters.AddNew();
                        ibParameter.Name = "IntermBillNoList";
                        ibParameter.ValueString = string.Empty;

                        var parUser = jobParameters.AddNew();
                        parUser.Name = "UserName";
                        parUser.ValueString = AppSession.UserLogin.UserName;

                        var parplafond = jobParameters.AddNew();
                        parplafond.Name = "plafond";

                        var reg = new Registration();
                        reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                        parplafond.ValueNumeric = reg.PlavonAmount ?? 0;

                        var parSelfGuarantor = jobParameters.AddNew();
                        parSelfGuarantor.Name = "SelfGuarantor";
                        parSelfGuarantor.ValueString = _selfGuarantor;

                        AppSession.PrintJobReportID = AppConstant.Report.BillingIntermStatementPatientDetail;
                        break;
                    }
                case "RSSA":
                    {
                        var dpParameter = jobParameters.AddNew();
                        dpParameter.Name = "DownPayment";
                        dpParameter.ValueNumeric = Helper.Payment.GetTotalDownPayment(registrationNoList) - Helper.Payment.GetTotalDownPaymentReturn(registrationNoList);

                        var payParameter = jobParameters.AddNew();
                        payParameter.Name = "PaymentAmount";
                        payParameter.ValueNumeric = Helper.Payment.GetTotalPayment(registrationNoList);

                        var par = jobParameters.AddNew();
                        par.Name = "ParamedicTariffComponentID";
                        par.ValueString = AppSession.Parameter.ParamedicTariffComponentID;

                        var parServiceUnitID = jobParameters.AddNew();
                        parServiceUnitID.Name = "ServiceUnitID";
                        parServiceUnitID.ValueString = cboFilterByServiceUnitID.SelectedValue;

                        var parIncludePrescription = jobParameters.AddNew();
                        parIncludePrescription.Name = "parIncludePrescription";
                        parIncludePrescription.ValueString = chkIsIncludePrescription.Checked ? "true" : "false";

                        AppSession.PrintJobReportID = AppConstant.Report.RssaBillingTemporaryStatement;
                        break;
                    }
                default:
                    {
                        //case "RSUI":
                        //case "RSPM":
                        //case "RSCH":
                        //case "RSBHP":
                        //case "RSSMCB":
                        //case "YBRSGKP":
                        //case "RSMM":

                        var ibParameter = jobParameters.AddNew();
                        ibParameter.Name = "IntermBillNoList";
                        ibParameter.ValueString = string.Empty;

                        var parUser = jobParameters.AddNew();
                        parUser.Name = "UserName";
                        parUser.ValueString = AppSession.UserLogin.UserName;

                        var parplafond = jobParameters.AddNew();
                        parplafond.Name = "plafond";

                        var reg = new Registration();
                        reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                        parplafond.ValueNumeric = reg.PlavonAmount ?? 0;

                        var parSelfGuarantor = jobParameters.AddNew();
                        parSelfGuarantor.Name = "SelfGuarantor";
                        parSelfGuarantor.ValueString = _selfGuarantor;

                        //---------------------------
                        var parServiceUnitID = jobParameters.AddNew();
                        parServiceUnitID.Name = "ServiceUnitID";
                        parServiceUnitID.ValueString = cboFilterByServiceUnitID.SelectedValue;

                        var parItemType = jobParameters.AddNew();
                        parItemType.Name = "ItemType";
                        parItemType.ValueString = cboFilterByItemType.SelectedValue;

                        var parItemGroupID = jobParameters.AddNew();
                        parItemGroupID.Name = "ItemGroupID";
                        parItemGroupID.ValueString = cboFilterByItemGroupID.SelectedValue;

                        var parIncludePrescription = jobParameters.AddNew();
                        parIncludePrescription.Name = "IncludePrescription";
                        parIncludePrescription.ValueString = chkIsIncludePrescription.Checked ? "true" : "false";
                        //---------------------------

                        AppSession.PrintJobReportID = AppConstant.Report.BillingStatementDetailWithComponentTariffDraft;
                        break;
                    }
            }

            var jobParameter2 = jobParameters.AddNew();
            jobParameter2.Name = "RegistrationNoList";
            jobParameter2.ValueString = string.Empty;
            foreach (var str in registrationNoList)
            {
                jobParameter2.ValueString += str + ",";
            }
            jobParameter2.ValueString = jobParameter2.ValueString.Substring(0, jobParameter2.ValueString.Length - 1);

            var parDate1 = jobParameters.AddNew();
            parDate1.Name = "StartDate";
            parDate1.ValueDateTime = txtTransDate1.SelectedDate ?? Convert.ToDateTime("1900-01-01 00:00:00");

            var parDate2 = jobParameters.AddNew();
            parDate2.Name = "EndDate";
            parDate2.ValueDateTime = txtTransDate2.SelectedDate ?? (new DateTime()).NowAtSqlServer().AddDays(10);

            var parRegNo = jobParameters.AddNew();
            parRegNo.Name = "RegNo";
            parRegNo.ValueString = txtRegistrationNo.Text;

            var parUserID = jobParameters.AddNew();
            parUserID.Name = "UserID";
            parUserID.ValueString = AppSession.UserLogin.UserID;

            var parAksesGuarantor = jobParameters.AddNew();
            parAksesGuarantor.Name = "AskesGuarantor";
            parAksesGuarantor.ValueString = string.Empty;// _guarantorAskesID;

            AppSession.PrintJobParameters = jobParameters;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
            "oWnd.Show();" +
            "oWnd.Maximize();";
            RadAjaxPanel1.ResponseScripts.Add(script);
        }

        protected void btnFilterTransDate_Click(object sender, ImageClickEventArgs e)
        {
            CostCalculations = null;
            grdCostCalculation.Rebind();

            Transactions = null;
            TransChargesItems = null;
            TransPrescriptionItems = null;
            grdTransChargesItem.Rebind();
        }

        protected void lbtnSaveToBuffer_Click(object sender, EventArgs e)
        {
            bool isValid = CostCalculations.Count > 0;

            if (isValid)
            {
                var collBuffer = new CostCalculationBufferCollection();
                collBuffer.Query.Where(collBuffer.Query.RegistrationNo == txtRegistrationNo.Text &&
                                       collBuffer.Query.GuarantorID == cboGuarantorID.SelectedValue &&
                                       collBuffer.Query.PaymentNo.IsNotNull());
                collBuffer.LoadAll();

                if (collBuffer.Count > 0)
                {
                    pnlInfo2.Visible = true;
                    lblInfo2.Text = "Save To Buffer failed. There are other buffer for this guarantor that has been paid.";
                    pnlInfo3.Visible = false;
                    lblInfo3.Text = string.Empty;
                }
                else
                {
                    using (var trans = new esTransactionScope())
                    {
                        var buffer = new CostCalculationBufferCollection();
                        buffer.Query.Where(collBuffer.Query.RegistrationNo == txtRegistrationNo.Text &&
                                               collBuffer.Query.GuarantorID == cboGuarantorID.SelectedValue);
                        buffer.LoadAll();

                        buffer.MarkAllAsDeleted();
                        buffer.Save();

                        foreach (CostCalculation item in CostCalculations)
                        {
                            var ccb = new CostCalculationBuffer();
                            ccb.AddNew();
                            ccb.RegistrationNo = txtRegistrationNo.Text;
                            ccb.GuarantorID = cboGuarantorID.SelectedValue;
                            ccb.TransactionNo = item.TransactionNo;
                            ccb.SequenceNo = item.SequenceNo;
                            ccb.ItemID = item.ItemID;
                            ccb.PatientAmount = item.PatientAmount;
                            ccb.GuarantorAmount = item.GuarantorAmount;
                            ccb.DiscountAmount = item.DiscountAmount;
                            ccb.ParamedicAmount = item.DiscountAmount;
                            ccb.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            ccb.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            ccb.Save();
                        }

                        trans.Complete();
                    }

                    pnlInfo2.Visible = true;
                    lblInfo2.Text = "Save To Buffer completed.";
                    pnlInfo3.Visible = false;
                    lblInfo3.Text = string.Empty;

                    Buffers = null;
                    grdBuffer.Rebind();
                }

            }
        }

        private CostCalculationCollection CostCalculations
        {
            get
            {
                var obj = Session["VerificationBilling:CostCalculations" + UniqueID];
                if (obj != null)
                    return ((CostCalculationCollection)(obj));

                var registrationNoList = MergeRegistrationList();

                var collection = new CostCalculationCollection();

                var query = new CostCalculationQuery("a");
                var item = new ItemQuery("b");
                var unit = new ServiceUnitQuery("c");
                var view = new VwTransactionQuery("d");
                var pay = new TransPaymentItemOrderQuery("e");
                var viewItem = new VwTransactionItemQuery("y");
                var cls = new ClassQuery("cls");

                query.Select(
                        query,
                        view.TransactionDate.As("refToTransaction_TransactionDate"),
                        unit.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"),
                        @"<CASE WHEN y.ParamedicCollectionName = '' THEN b.ItemName ELSE b.ItemName + ' (' + y.ParamedicCollectionName + ')' END AS 'refToItem_ItemName'>",
                        view.ReferenceNo.As("refToTransaction_ReferenceNo"),
                        cls.ClassName.As("refToClass_ClassName")
                    );

                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                if (!string.IsNullOrEmpty(cboFilterByItemType.SelectedValue))
                {
                    if (cboFilterByItemType.SelectedValue == "1")
                        query.Where(item.SRItemType.In(ItemType.Service, ItemType.Laboratory, ItemType.Radiology, ItemType.Package));
                    else if (cboFilterByItemType.SelectedValue == "0")
                        query.Where(item.SRItemType.In(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen));
                    else if (cboFilterByItemType.SelectedValue == "21")
                        query.Where(item.SRItemType.In(ItemType.NonMedical, ItemType.Kitchen));
                    else
                        query.Where(item.SRItemType == cboFilterByItemType.SelectedValue);
                }
                //if (!string.IsNullOrEmpty(cboFilterByItemType.SelectedValue))
                //{
                //    if (cboFilterByItemType.SelectedValue == "1")
                //        query.Where(item.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen), item.ItemGroupID != "0199");
                //    else
                //        query.Where(query.Or(item.SRItemType.In(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen), item.ItemGroupID == "0199"));
                //}

                query.InnerJoin(view).On(query.TransactionNo == view.TransactionNo &&
                                         query.RegistrationNo == view.RegistrationNo);
                query.InnerJoin(viewItem).On(query.TransactionNo == viewItem.TransactionNo &&
                                             query.SequenceNo == viewItem.SequenceNo);

                query.LeftJoin(cls).On(view.ClassID == cls.ClassID);
                query.InnerJoin(unit).On(view.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(pay).On(
                    query.TransactionNo == pay.TransactionNo &&
                    query.SequenceNo == pay.SequenceNo &&
                    pay.IsPaymentProceed == true &&
                    pay.IsPaymentReturned == false
                    );

                if (!chkIsIncludePrescription.Checked)
                    query.Where(viewItem.TxType == 1);

                if (!string.IsNullOrEmpty(cboFilterByServiceUnitID.SelectedValue))
                    query.Where(view.ServiceUnitID == cboFilterByServiceUnitID.SelectedValue);

                if (!string.IsNullOrEmpty(cboFilterByCheckedStatus.SelectedValue))
                {
                    if (cboFilterByCheckedStatus.SelectedValue == "1")
                        query.Where(query.IsChecked == true);
                    else
                        query.Where(query.Or(query.IsChecked.IsNull(), query.IsChecked == false));
                }
                if (!(string.IsNullOrEmpty(cboFilterByItemGroupID.SelectedValue)))
                    query.Where(item.ItemGroupID == cboFilterByItemGroupID.SelectedValue);
                if (!txtTransDate1.IsEmpty && !txtTransDate2.IsEmpty)
                    query.Where(view.FilterDate >= txtTransDate1.SelectedDate, view.FilterDate < txtTransDate2.SelectedDate.Value.AddDays(1));
                
                query.Where(
                    query.IntermBillNo.IsNull(),
                    query.RegistrationNo.In(registrationNoList),
                    pay.PaymentNo.IsNull(),
                    view.PackageReferenceNo == string.Empty
                    );

                query.OrderBy(
                    unit.ServiceUnitName.Ascending,
                    view.OrderDate.Ascending,
                    view.OrderTransNo.Ascending,
                    query.SequenceNo.Ascending,
                    query.TransactionNo.Ascending
                    );

                collection.Load(query);

                SetSession("VerificationBilling:CostCalculations", collection);

                return collection;
            }
            set { SetSession("VerificationBilling:CostCalculations", value); }
        }

        protected void grdCostCalculation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdCostCalculation.DataSource = CostCalculations;
        }

        protected void grdBuffer_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdBuffer.DataSource = Buffers;
        }

        private DataTable Buffers
        {
            get
            {
                if (Session["VerificationBilling:CostCalculationBuffers" + UniqueID] != null)
                    return (DataTable)Session["VerificationBilling:CostCalculationBuffers" + UniqueID];

                var query = new CostCalculationBufferQuery("a");
                var guar = new GuarantorQuery("b");

                query.InnerJoin(guar).On(query.GuarantorID == guar.GuarantorID);
                query.Select
                    (
                        query.RegistrationNo,
                        query.GuarantorID,
                        guar.GuarantorName,
                        query.PatientAmount.Sum().As("PatientAmount"),
                        query.GuarantorAmount.Sum().As("GuarantorAmount"),
                        query.DiscountAmount.Sum().As("DiscountAmount"),
                        "<CASE WHEN a.PaymentNo IS NOT NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsPaid>"
                    );
                query.GroupBy(query.RegistrationNo, query.GuarantorID, guar.GuarantorName, query.PaymentNo);

                query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                query.OrderBy(query.GuarantorID.Ascending);

                DataTable tbl = query.LoadDataTable();

                SetSession("VerificationBilling:CostCalculationBuffers", tbl);

                return tbl;
            }
            set { SetSession("VerificationBilling:CostCalculationBuffers", value); }
        }

        protected void grdBuffer_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new CostCalculationBufferQuery("a");
            var transQ = new VwTransactionQuery("b");
            var itemQ = new ItemQuery("c");
            var suQ = new ServiceUnitQuery("d");

            query.InnerJoin(transQ).On(query.TransactionNo == transQ.TransactionNo);
            query.InnerJoin(itemQ).On(query.ItemID == itemQ.ItemID);
            query.InnerJoin(suQ).On(transQ.ServiceUnitID == suQ.ServiceUnitID);

            query.Select
                (
                    query,
                    itemQ.ItemName,
                    transQ.TransactionDate,
                    suQ.ServiceUnitName
                );
            query.OrderBy(suQ.ServiceUnitName.Ascending, transQ.TransactionDate.Ascending);
            query.Where
                (
                    query.RegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString(),
                    query.GuarantorID == e.DetailTableView.ParentItem.GetDataKeyValue("GuarantorID").ToString()
                );
            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdCostCalculation.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }
        #endregion

        #region Tab: Registration Item Rule
        private RegistrationItemRuleCollection RegistrationItemRules
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["BillingVerification:collRegistrationItemRule" + UniqueID];
                    if (obj != null)
                        return ((RegistrationItemRuleCollection)(obj));
                }

                var coll = new RegistrationItemRuleCollection();
                var query = new RegistrationItemRuleQuery("a");
                var iq = new ItemQuery("b");
                var qSr = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName"),
                        qSr.ItemName.As("refToSRItem_ItemName")
                    );

                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                query.LeftJoin(qSr).On
                    (
                        query.SRGuarantorRuleType == qSr.ItemID &
                        qSr.StandardReferenceID == "GuarantorRuleType"
                    );

                query.Where(query.RegistrationNo == txtRegistrationNo.Text);

                query.OrderBy(query.ItemID.Ascending);

                coll.Load(query);

                SetSession("BillingVerification:collRegistrationItemRule", coll);
                return coll;
            }
            set { SetSession("BillingVerification:collRegistrationItemRule", value); }
        }

        protected void grdRegistrationItemRule_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRegistrationItemRule.DataSource = RegistrationItemRules;
        }

        protected void grdRegistrationItemRule_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            String itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][RegistrationItemRuleMetadata.ColumnNames.ItemID]);
            BusinessObject.RegistrationItemRule entity = FindItemGrid(itemID);
            if (entity != null)
            {
                SetEntityValue(entity, e);
                RegistrationItemRules.Save();
            }
        }

        protected void grdRegistrationItemRule_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null)
                return;

            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][RegistrationItemRuleMetadata.ColumnNames.ItemID]);
            BusinessObject.RegistrationItemRule entity = FindItemGrid(itemID);
            if (entity != null)
            {
                entity.MarkAsDeleted();
                RegistrationItemRules.Save();
            }
        }

        protected void grdRegistrationItemRule_InsertCommand(object source, GridCommandEventArgs e)
        {
            BusinessObject.RegistrationItemRule entity = RegistrationItemRules.AddNew();
            SetEntityValue(entity, e);
            RegistrationItemRules.Save();

            e.Canceled = true;
            grdRegistrationItemRule.Rebind();
        }

        private void SetEntityValue(BusinessObject.RegistrationItemRule entity, GridCommandEventArgs e)
        {
            var userControl = (RegistrationItemRuleDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RegistrationNo = txtRegistrationNo.Text;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.SRGuarantorRuleType = userControl.SRGuarantorRuleType;
                entity.GuarantorRuleTypeName = userControl.GuarantorRuleTypeName;
                entity.AmountValue = userControl.AmountValue;
                entity.OutpatientAmountValue = userControl.OutpatientAmountValue;
                entity.EmergencyAmountValue = userControl.EmergencyAmountValue;
                entity.IsValueInPercent = userControl.IsValueInPercent;
                entity.IsInclude = userControl.IsInclude;
                entity.IsToGuarantor = userControl.IsToGuarantor;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private BusinessObject.RegistrationItemRule FindItemGrid(string itemID)
        {
            RegistrationItemRuleCollection coll = RegistrationItemRules;
            return coll.FirstOrDefault(rec => rec.ItemID.Equals(itemID));
        }
        #endregion

        #region Tab: Registration Tariff Component Discount Rule

        protected void lbtnSaveTariffCompDiscountRule_Click(object sender, EventArgs e)
        {
            var entity = new RegistrationDiscountRule();
            if (!entity.LoadByPrimaryKey(txtRegistrationNo.Text))
                entity.AddNew();

            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.RoomPercentage = 0;
            entity.RsPercentage = 0;
            entity.DrPercentage = 0;
            entity.BhpPercentage = 0;
            entity.ResepPercentage = Convert.ToDecimal(txtTariffCompDiscountResep.Value);
            entity.DiscountGlobalAmount = Convert.ToDecimal(txtTariffCompDiscountGlobalAmount.Value);
            entity.IsDiscountGlobalInPercent = chkIsTariffCompDiscountGlobalInPercent.Checked;
            entity.IsDiscountGlobal = entity.DiscountGlobalAmount > 0;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.ItemMedicalPercentage = Convert.ToDecimal(txtTariffCompDiscountItemMedical.Value);
            entity.ItemNonMedicalPercentage = Convert.ToDecimal(txtTariffCompDiscountItemNonMedical.Value);

            var tariffCompDiscRules = new RegistrationTariffComponentDiscountRuleCollection();
            tariffCompDiscRules.Query.Where(tariffCompDiscRules.Query.RegistrationNo == txtRegistrationNo.Text);
            tariffCompDiscRules.LoadAll();

            foreach (GridDataItem dataItem in grdTariffCompDiscountRule.MasterTableView.Items)
            {
                string tariffCompId = dataItem.GetDataKeyValue("TariffComponentID").ToString();
                bool isDiscountInPercentage = ((CheckBox)dataItem.FindControl("chkIsDiscountInPercentage")).Checked;
                double amount = ((RadNumericTextBox)dataItem.FindControl("txtDiscount")).Value ?? 0;
                bool isExist = false;
                foreach (RegistrationTariffComponentDiscountRule row in tariffCompDiscRules)
                {
                    if (row.TariffComponentID.Equals(tariffCompId))
                    {
                        isExist = true;
                        row.IsDiscountInPercentage = isDiscountInPercentage;
                        row.Amount = Convert.ToDecimal(amount);
                        row.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        row.LastUpdateByUserID = AppSession.UserLogin.UserID;

                        if (amount <= 0)
                            row.MarkAsDeleted();

                        break;
                    }
                }
                if (!isExist && amount > 0)
                {
                    RegistrationTariffComponentDiscountRule row = tariffCompDiscRules.AddNew();
                    row.RegistrationNo = txtRegistrationNo.Text;
                    row.TariffComponentID = tariffCompId;
                    row.IsDiscountInPercentage = isDiscountInPercentage;
                    row.Amount = Convert.ToDecimal(amount);
                    row.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    row.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                tariffCompDiscRules.Save();
                trans.Complete();
            }

            ProcessWithTariffComponentDiscRules();
            CostCalculations = null;
            grdCostCalculation.Rebind();

            Transactions = null;
            TransChargesItems = null;
            TransPrescriptionItems = null;
            grdTransChargesItem.Rebind();

            pnlInfo2.Visible = true;
            lblInfo2.Text = "Save & Process Tariff Component Discount Rule completed.";
            pnlInfo3.Visible = false;
            lblInfo3.Text = string.Empty;
        }

        protected void grdTariffCompDiscountRule_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTariffCompDiscountRule.DataSource = GetTariffCompDiscountRules();
        }

        private DataTable GetTariffCompDiscountRules()
        {
            var query = new RegistrationTariffComponentDiscountRuleQuery("a");
            var qrRef = new TariffComponentQuery("b");
            query.RightJoin(qrRef).On(query.TariffComponentID == qrRef.TariffComponentID & query.RegistrationNo == txtRegistrationNo.Text);
            query.OrderBy(qrRef.TariffComponentID.Ascending);
            query.Select
                (
                    qrRef.TariffComponentID.As("TariffComponentID"),
                    qrRef.TariffComponentName.As("TariffComponentName"),
                    "<ISNULL(a.IsDiscountInPercentage, 1) as IsDiscountInPercentage>",
                    "<ISNULL(a.Amount, 0) as Amount>"
                );
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        private void ProcessWithTariffComponentDiscRules()
        {
            var discRules = new RegistrationDiscountRule();
            if (discRules.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                //validating data
                bool select = false;
                int index = 0;
                foreach (GridDataItem dataItem in grdTransChargesItem.MasterTableView.Items)
                {
                    if (!(dataItem.FindControl("detailChkbox") as CheckBox).Checked)
                    {
                        CostCalculation entity = CostCalculations.FindByPrimaryKey(dataItem["RegistrationNo"].Text,
                            dataItem["TransactionNo"].Text, dataItem["SequenceNo"].Text);
                        if (entity != null)
                            entity.MarkAsDeleted();
                    }
                    else
                    {
                        index++;
                        select = true;
                    }
                }

                if ((!select) && (index == 0))
                    return;

                using (var trans = new esTransactionScope())
                {
                    var list = CostCalculations.GroupBy(c => c.RegistrationNo).Select(q => new
                    {
                        TxAmt = q.Sum(p => (p.GuarantorAmount + p.PatientAmount))
                    });
                    decimal totalTx = 0;
                    foreach (var item in list)
                    {
                        totalTx += item.TxAmt ?? 0;
                    }

                    //re-calculation
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

                    var grrID = cboGuarantorID.SelectedValue;
                    if (grrID == _selfGuarantor)
                    {
                        var pat = new Patient();
                        pat.LoadByPrimaryKey(reg.PatientID);
                        if (!string.IsNullOrEmpty(pat.MemberID))
                            grrID = pat.MemberID;
                    }

                    var grr = new Guarantor();
                    grr.LoadByPrimaryKey(grrID);

                    if (discRules.IsDiscountGlobal == false)
                    {
                        foreach (DataRow row in Transactions.AsEnumerable().Where(v => v.Field<bool>("IsBillProceed") &&
                                                                                       !v.Field<bool>("IsVoid") &&
                                                                                       !v.Field<bool>("IsPaymentProceed") &&
                                                                                       !v.Field<bool>("IsPaymentProceedReff") &&
                                                                                       !v.Field<bool>("IsIntermBillProceed")))
                        {
                            switch (row["TYPE"].ToString())
                            {
                                case "1":
                                    var detail = new TransChargesItem();
                                    if (detail.LoadByPrimaryKey(row["TransactionNo"].ToString(), row["SequenceNo"].ToString()))
                                    {
                                        var header = new TransCharges();
                                        header.LoadByPrimaryKey(row["TransactionNo"].ToString());

                                        var comps = new TransChargesItemCompCollection();
                                        comps.Query.Where(
                                            comps.Query.TransactionNo == row["TransactionNo"].ToString(),
                                            comps.Query.SequenceNo == row["SequenceNo"].ToString()
                                            );
                                        comps.LoadAll();

                                        decimal amtDisc = 0;
                                        decimal amtAutoCalc = 0;

                                        if (comps.Count > 0)
                                        {
                                            foreach (var entity in comps)
                                            {
                                                var tariffCompDiscount = new RegistrationTariffComponentDiscountRule();
                                                if (tariffCompDiscount.LoadByPrimaryKey(txtRegistrationNo.Text, entity.TariffComponentID))
                                                {
                                                    if (tariffCompDiscount.IsDiscountInPercentage == true)
                                                        entity.DiscountAmount = entity.Price * tariffCompDiscount.Amount / 100;
                                                    else
                                                        entity.DiscountAmount = tariffCompDiscount.Amount > entity.Price ? entity.Price : tariffCompDiscount.Amount;
                                                }
                                                else
                                                    entity.DiscountAmount = 0;

                                                entity.AutoProcessCalculation = 0 - entity.DiscountAmount;
                                                //entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                                //entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                                                amtDisc += entity.DiscountAmount ?? 0;
                                                amtAutoCalc += entity.AutoProcessCalculation ?? 0;
                                            }
                                        }
                                        else
                                        {
                                            var i = new Item();
                                            i.LoadByPrimaryKey(detail.ItemID);
                                            if (i.SRItemType == ItemType.Medical)
                                                amtDisc = (detail.Price ?? 0) * ((discRules.ItemMedicalPercentage ?? 0) / 100);
                                            else
                                                amtDisc = (detail.Price ?? 0) * ((discRules.ItemNonMedicalPercentage ?? 0) / 100);
                                            amtAutoCalc = 0 - amtDisc;
                                        }

                                        detail.AutoProcessCalculation = amtAutoCalc;

                                        if (detail.AutoProcessCalculation < 0)
                                        {
                                            detail.DiscountAmount = Math.Abs(detail.ChargeQuantity ?? 0) * Math.Abs(amtAutoCalc);

                                            if (detail.DiscountAmount > (detail.Price * Math.Abs(detail.ChargeQuantity ?? 0)) + detail.CitoAmount)
                                            {
                                                detail.DiscountAmount = (detail.Price * Math.Abs(detail.ChargeQuantity ?? 0)) + detail.CitoAmount;
                                                detail.AutoProcessCalculation = 0 - (detail.DiscountAmount / Math.Abs(detail.ChargeQuantity ?? 0));
                                            }
                                        }
                                        else if (detail.AutoProcessCalculation > 0)
                                            detail.Price += detail.AutoProcessCalculation;

                                        //detail.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                        //detail.LastUpdateByUserID = AppSession.UserLogin.UserID;

                                        detail.Save();
                                        comps.Save();

                                        if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                                        {
                                            // extract fee
                                            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                                            feeColl.SetFeeByTCIC(comps, AppSession.UserLogin.UserID);
                                            feeColl.Save();
                                            //feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                                            //feeColl.Save();
                                        }

                                        //post
                                        decimal? totaldisc = Math.Abs(detail.DiscountAmount ?? 0);

                                        //CostCalculations
                                        var cost = new CostCalculation();
                                        if (cost.LoadByPrimaryKey(header.RegistrationNo, row["TransactionNo"].ToString(), row["SequenceNo"].ToString()))
                                        {
                                            DataTable tblCovered = Helper.GetCoveredItems(txtRegistrationNo.Text, grrID,
                                                                              reg.CoverageClassID, detail.ItemID,
                                                                              header.TransactionDate.Value.Date, false);

                                            decimal? total = ((Math.Abs(detail.ChargeQuantity ?? 0) * detail.Price) - detail.DiscountAmount) + detail.CitoAmount;

                                            var calc = new Helper.CostCalculation(grrID, detail.ItemID, total ?? 0, tblCovered,
                                                                  Math.Abs(detail.ChargeQuantity ?? 0),
                                                                  detail.IsCito ?? false,
                                                                  detail.IsCitoInPercent ?? false,
                                                                  detail.BasicCitoAmount ?? 0, detail.Price ?? 0,
                                                                  header.IsRoomIn ?? false, detail.IsItemRoom ?? false,
                                                                  header.TariffDiscountForRoomIn ?? 0, totaldisc ?? 0,
                                                                  reg.IsGlobalPlafond ?? false,
                                                                  detail.ItemConditionRuleID, header.TransactionDate.Value, detail.IsVariable ?? false);

                                            cost.PatientAmount = (detail.ChargeQuantity < 0) ? 0 - calc.PatientAmount : calc.PatientAmount;
                                            cost.GuarantorAmount = (detail.ChargeQuantity < 0) ? 0 - calc.GuarantorAmount : calc.GuarantorAmount;
                                            cost.DiscountAmount = (detail.ChargeQuantity < 0) ? 0 - totaldisc : totaldisc;

                                            cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                            cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            cost.Save();
                                        }
                                    }

                                    break;

                                case "2":
                                    if (discRules.ResepPercentage > 0)
                                    {
                                        var presc = new TransPrescriptionItem();
                                        if (presc.LoadByPrimaryKey(row["TransactionNo"].ToString(), row["SequenceNo"].ToString()))
                                        {
                                            var hpresc = new TransPrescription();
                                            hpresc.LoadByPrimaryKey(row["TransactionNo"].ToString());

                                            //Pembulatan qty
                                            var itemMedic = new ItemProductMedic();
                                            decimal resultQty = presc.ResultQty ?? 0;
                                            decimal recipeAmount = presc.RecipeAmount ?? 0 + presc.EmbalaceAmount ?? 0 + presc.SweetenerAmount ?? 0;

                                            var IsIncR = (AppSession.Parameter.IsPrescriptionDiscountIncludeR);
                                            decimal rPrice = 0;
                                            if (resultQty != 0)
                                            {
                                                rPrice = ((presc.EmbalaceAmount ?? 0) + (presc.SweetenerAmount ?? 0) + (presc.RecipeAmount ?? 0));
                                            }

                                            decimal _lineAmt = Convert.ToDecimal((Math.Abs(resultQty) * presc.Price) + (IsIncR ? rPrice : 0));
                                            if (AppSession.Parameter.IsPrescriptionDiscountAfterRounding)
                                                _lineAmt = Helper.Rounding(_lineAmt, AppEnum.RoundingType.Prescription);

                                            presc.DiscountAmount = Convert.ToDecimal(_lineAmt) * (discRules.ResepPercentage / 100);
                                            if (presc.IsUsingAdminReturn ?? false)
                                                presc.DiscountAmount = presc.DiscountAmount - (presc.DiscountAmount * (AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) / 100);
                                            //presc.DiscountAmount = ((Math.Abs(resultQty) * presc.Price) + (IsIncR ? rPrice : 0)) * (discRules.ResepPercentage / 100);
                                            presc.AutoProcessCalculation = 0 - (((IsIncR ? presc.Price + (rPrice / (Math.Abs(resultQty) == 0 ? 1 : Math.Abs(resultQty))) : presc.Price)) * (discRules.ResepPercentage / 100));

                                            decimal lineAmt = 0;
                                            //lineAmt = ((Math.Abs(resultQty) * presc.Price) - presc.DiscountAmount + rPrice);

                                            //if (presc.IsUsingAdminReturn ?? false)
                                            //{
                                            //    lineAmt = lineAmt - (lineAmt * (AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) / 100);
                                            //}

                                            //lineAmt = Helper.Rounding(Convert.ToDecimal(lineAmt), AppEnum.RoundingType.Prescription);

                                            lineAmt = ((Math.Abs(resultQty) * presc.Price ?? 0) + recipeAmount);

                                            if (AppSession.Parameter.IsPrescriptionDiscountAfterRounding)
                                                lineAmt = Helper.Rounding(lineAmt, AppEnum.RoundingType.Prescription) - (presc.DiscountAmount ?? 0);
                                            else
                                                lineAmt = Helper.Rounding(lineAmt - (presc.DiscountAmount ?? 0), AppEnum.RoundingType.Prescription);

                                            presc.LineAmount = resultQty < 0 ? 0 - lineAmt : lineAmt;
                                            presc.Save();

                                            decimal? totaldiscpresc = Math.Abs(presc.DiscountAmount ?? 0);

                                            var costpresc = new CostCalculation();
                                            if (costpresc.LoadByPrimaryKey(hpresc.RegistrationNo, row["TransactionNo"].ToString(), row["SequenceNo"].ToString()))
                                            {
                                                DataTable tblCovered2 = Helper.GetCoveredItems(txtRegistrationNo.Text, grrID,
                                                                             reg.CoverageClassID, string.IsNullOrEmpty(presc.ItemInterventionID) ? presc.ItemID : presc.ItemInterventionID,
                                                                             hpresc.PrescriptionDate.Value.Date, true);

                                                var calc = new Helper.CostCalculation(grrID, reg.IsGlobalPlafond ?? false,
                                                    string.IsNullOrEmpty(presc.ItemInterventionID) ? presc.ItemID : presc.ItemInterventionID, Math.Abs(presc.LineAmount ?? 0),
                                                    tblCovered2, Math.Abs(resultQty), presc.Price ?? 0, recipeAmount, presc.DiscountAmount ?? 0);

                                                costpresc.PatientAmount = resultQty < 0 ? 0 - calc.PatientAmount : calc.PatientAmount;
                                                costpresc.GuarantorAmount = resultQty < 0 ? 0 - calc.GuarantorAmount : calc.GuarantorAmount;
                                                costpresc.DiscountAmount = resultQty < 0 ? 0 - presc.DiscountAmount : presc.DiscountAmount;

                                                costpresc.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                                costpresc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                                costpresc.Save();
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        decimal discp = discRules.DiscountGlobalAmount ?? 0;

                        if (!(discRules.IsDiscountGlobalInPercent ?? false)) discp = (discp / totalTx) * 100;

                        discp = Convert.ToDecimal(string.Format("{0:n2}", discp));

                        foreach (DataRow row in Transactions.AsEnumerable().Where(v => v.Field<bool>("IsBillProceed") &&
                                                                                       !v.Field<bool>("IsVoid") &&
                                                                                       !v.Field<bool>("IsPaymentProceed") &&
                                                                                       !v.Field<bool>("IsPaymentProceedReff") &&
                                                                                       !v.Field<bool>("IsIntermBillProceed")))
                        {
                            switch (row["TYPE"].ToString())
                            {
                                case "1":
                                    var detail = new TransChargesItem();
                                    if (detail.LoadByPrimaryKey(row["TransactionNo"].ToString(), row["SequenceNo"].ToString()))
                                    {
                                        var header = new TransCharges();
                                        header.LoadByPrimaryKey(row["TransactionNo"].ToString());

                                        var comps = new TransChargesItemCompCollection();
                                        comps.Query.Where(
                                            comps.Query.TransactionNo == row["TransactionNo"].ToString(),
                                            comps.Query.SequenceNo == row["SequenceNo"].ToString()
                                            );
                                        comps.LoadAll();

                                        detail.DiscountAmount = Math.Abs(detail.ChargeQuantity ?? 0) *
                                                                (detail.Price * discp / 100);
                                        detail.AutoProcessCalculation = 0 - (detail.Price * discp / 100);

                                        foreach (var entity in comps)
                                        {
                                            entity.DiscountAmount = entity.Price * discp / 100;
                                            entity.AutoProcessCalculation = 0 - entity.DiscountAmount;
                                        }
                                        detail.Save();
                                        comps.Save();

                                        if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                                        {
                                            // extract fee
                                            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                                            feeColl.SetFeeByTCIC(comps, AppSession.UserLogin.UserID);
                                            feeColl.Save();
                                            //feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                                            //feeColl.Save();
                                        }

                                        //post
                                        decimal? totaldisc = Math.Abs(detail.DiscountAmount ?? 0);

                                        //CostCalculations
                                        var cost = new CostCalculation();
                                        if (cost.LoadByPrimaryKey(header.RegistrationNo, row["TransactionNo"].ToString(), row["SequenceNo"].ToString()))
                                        {
                                            DataTable tblCovered = Helper.GetCoveredItems(txtRegistrationNo.Text, grrID,
                                                                              reg.CoverageClassID, detail.ItemID,
                                                                              header.TransactionDate.Value.Date, false);

                                            decimal? total = ((Math.Abs(detail.ChargeQuantity ?? 0) * detail.Price) - detail.DiscountAmount) + detail.CitoAmount;

                                            var calc = new Helper.CostCalculation(grrID, detail.ItemID, total ?? 0, tblCovered,
                                                                  Math.Abs(detail.ChargeQuantity ?? 0),
                                                                  detail.IsCito ?? false,
                                                                  detail.IsCitoInPercent ?? false,
                                                                  detail.BasicCitoAmount ?? 0, detail.Price ?? 0,
                                                                  header.IsRoomIn ?? false, detail.IsItemRoom ?? false,
                                                                  header.TariffDiscountForRoomIn ?? 0, totaldisc ?? 0,
                                                                  reg.IsGlobalPlafond ?? false,
                                                                  detail.ItemConditionRuleID, header.TransactionDate.Value, detail.IsVariable ?? false);

                                            cost.PatientAmount = (detail.ChargeQuantity < 0) ? 0 - calc.PatientAmount : calc.PatientAmount;
                                            cost.GuarantorAmount = (detail.ChargeQuantity < 0) ? 0 - calc.GuarantorAmount : calc.GuarantorAmount;
                                            cost.DiscountAmount = (detail.ChargeQuantity < 0) ? 0 - totaldisc : totaldisc;

                                            cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                            cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            cost.Save();
                                        }
                                    }
                                    break;

                                case "2":
                                    var presc = new TransPrescriptionItem();
                                    if (presc.LoadByPrimaryKey(row["TransactionNo"].ToString(), row["SequenceNo"].ToString()))
                                    {
                                        var hpresc = new TransPrescription();
                                        hpresc.LoadByPrimaryKey(row["TransactionNo"].ToString());

                                        decimal resultQty = presc.ResultQty ?? 0;
                                        decimal recipeAmount = presc.RecipeAmount ?? 0 + presc.EmbalaceAmount ?? 0 + presc.SweetenerAmount ?? 0;

                                        var IsIncR = (AppSession.Parameter.IsPrescriptionDiscountIncludeR);
                                        decimal rPrice = 0;
                                        if (resultQty != 0)
                                        {
                                            rPrice = ((presc.EmbalaceAmount ?? 0) + (presc.SweetenerAmount ?? 0) + (presc.RecipeAmount ?? 0));
                                        }

                                        decimal _lineAmt = Convert.ToDecimal((Math.Abs(resultQty) * presc.Price) + (IsIncR ? rPrice : 0));
                                        if (AppSession.Parameter.IsPrescriptionDiscountAfterRounding)
                                            _lineAmt = Helper.Rounding(_lineAmt, AppEnum.RoundingType.Prescription);

                                        presc.DiscountAmount = Convert.ToDecimal(_lineAmt) * (discp / 100);
                                        if (presc.IsUsingAdminReturn ?? false)
                                            presc.DiscountAmount = presc.DiscountAmount - (presc.DiscountAmount * (AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) / 100);

                                        //presc.DiscountAmount = ((Math.Abs(resultQty) * presc.Price) + (IsIncR ? rPrice : 0)) * (discp / 100);
                                        presc.AutoProcessCalculation = 0 - (((IsIncR ? presc.Price + (rPrice / (Math.Abs(resultQty) == 0 ? 1 : Math.Abs(resultQty))) : presc.Price)) * (discp / 100));

                                        decimal lineAmt = 0;
                                        //lineAmt = ((Math.Abs(resultQty) * presc.Price) - presc.DiscountAmount + rPrice);
                                        //if (presc.IsUsingAdminReturn ?? false)
                                        //{
                                        //    lineAmt = lineAmt - (lineAmt * (AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) / 100);
                                        //}
                                        //lineAmt = Helper.Rounding(Convert.ToDecimal(lineAmt), AppEnum.RoundingType.Prescription);

                                        lineAmt = ((Math.Abs(resultQty) * presc.Price ?? 0) + recipeAmount);

                                        if (AppSession.Parameter.IsPrescriptionDiscountAfterRounding)
                                            lineAmt = Helper.Rounding(lineAmt, AppEnum.RoundingType.Prescription) - presc.DiscountAmount ?? 0;
                                        else
                                            lineAmt = Helper.Rounding(lineAmt - (presc.DiscountAmount ?? 0), AppEnum.RoundingType.Prescription);

                                        presc.LineAmount = resultQty < 0 ? 0 - lineAmt : lineAmt;
                                        presc.Save();

                                        decimal? totaldiscpresc = Math.Abs(presc.DiscountAmount ?? 0);

                                        var costpresc = new CostCalculation();
                                        if (costpresc.LoadByPrimaryKey(hpresc.RegistrationNo, row["TransactionNo"].ToString(), row["SequenceNo"].ToString()))
                                        {
                                            DataTable tblCovered2 = Helper.GetCoveredItems(txtRegistrationNo.Text, grrID,
                                                                             reg.CoverageClassID, string.IsNullOrEmpty(presc.ItemInterventionID) ? presc.ItemID : presc.ItemInterventionID,
                                                                             hpresc.PrescriptionDate.Value.Date, true);

                                            var calc = new Helper.CostCalculation(grrID, reg.IsGlobalPlafond ?? false,
                                                string.IsNullOrEmpty(presc.ItemInterventionID) ? presc.ItemID : presc.ItemInterventionID, Math.Abs(presc.LineAmount ?? 0),
                                                tblCovered2, Math.Abs(resultQty), presc.Price ?? 0, recipeAmount, presc.DiscountAmount ?? 0);

                                            costpresc.PatientAmount = resultQty < 0 ? 0 - calc.PatientAmount : calc.PatientAmount;
                                            costpresc.GuarantorAmount = resultQty < 0 ? 0 - calc.GuarantorAmount : calc.GuarantorAmount;
                                            costpresc.DiscountAmount = resultQty < 0 ? 0 - presc.DiscountAmount : presc.DiscountAmount;

                                            costpresc.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                            costpresc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            costpresc.Save();
                                        }
                                    }
                                    break;
                            }
                        }
                    }

                    trans.Complete();
                }
            }
        }

        #endregion

        #region Tab: Registration Plafond Rule

        protected void lbtnSavePlafondRule_Click(object sender, EventArgs e)
        {
            var entity = new RegistrationPlafondRule();
            if (!entity.LoadByPrimaryKey(txtRegistrationNo.Text))
                entity.AddNew();

            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.PlafondAmount = Convert.ToDecimal(txtPlafondRuleAmount.Value);
            entity.IsPlafondInPercent = chkIsPlafondRuleInPercent.Checked;
            entity.IsToGuarantor = rblIsPlafondRuleToGuarantor.SelectedIndex == 0;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                trans.Complete();
            }

            ProcessWithPlafondRules();
            CostCalculations = null;
            grdCostCalculation.Rebind();

            pnlInfo2.Visible = true;
            lblInfo2.Text = "Save & Process Plafond Rule completed.";
            pnlInfo3.Visible = false;
            lblInfo3.Text = string.Empty;
        }

        private void ProcessWithPlafondRules()
        {
            var plafondRule = new RegistrationPlafondRule();
            if (plafondRule.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                //validating data
                bool select = false;
                int index = 0;
                foreach (GridDataItem dataItem in grdTransChargesItem.MasterTableView.Items)
                {
                    if (!(dataItem.FindControl("detailChkbox") as CheckBox).Checked)
                    {
                        CostCalculation entity = CostCalculations.FindByPrimaryKey(dataItem["RegistrationNo"].Text,
                            dataItem["TransactionNo"].Text, dataItem["SequenceNo"].Text);
                        if (entity != null)
                            entity.MarkAsDeleted();
                    }
                    else
                    {
                        index++;
                        select = true;
                    }
                }

                if ((!select) && (index == 0))
                    return;

                using (var trans = new esTransactionScope())
                {
                    //re-calculation
                    foreach (DataRow row in Transactions.AsEnumerable().Where(v => v.Field<bool>("IsBillProceed") &&
                                                                                       !v.Field<bool>("IsVoid") &&
                                                                                       !v.Field<bool>("IsPaymentProceed") &&
                                                                                       !v.Field<bool>("IsPaymentProceedReff") &&
                                                                                       !v.Field<bool>("IsIntermBillProceed")))
                    {
                        switch (row["TYPE"].ToString())
                        {
                            case "1":
                                var detail = new TransChargesItem();
                                if (detail.LoadByPrimaryKey(row["TransactionNo"].ToString(), row["SequenceNo"].ToString()))
                                {
                                    var header = new TransCharges();
                                    header.LoadByPrimaryKey(row["TransactionNo"].ToString());

                                    //CostCalculations
                                    var cost = new CostCalculation();
                                    if (cost.LoadByPrimaryKey(header.RegistrationNo, row["TransactionNo"].ToString(), row["SequenceNo"].ToString()))
                                    {
                                        decimal tcost = (cost.PatientAmount ?? 0) + (cost.GuarantorAmount ?? 0);
                                        decimal tplafond;
                                        if (plafondRule.IsPlafondInPercent ?? false)
                                            tplafond = tcost * (plafondRule.PlafondAmount ?? 0) / 100;
                                        else
                                        {
                                            tplafond = (plafondRule.PlafondAmount ?? 0) > Math.Abs(tcost)
                                                           ? Math.Abs(tcost)
                                                           : (plafondRule.PlafondAmount ?? 0);

                                            tplafond = (detail.ChargeQuantity < 0) ? 0 - tplafond : tplafond;
                                        }
                                        cost.PatientAmount = plafondRule.IsToGuarantor ?? false
                                                                     ? tcost - tplafond
                                                                     : tplafond;
                                        cost.GuarantorAmount = plafondRule.IsToGuarantor ?? false
                                                                   ? tplafond
                                                                   : tcost - tplafond;

                                        cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                        cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                        cost.Save();
                                    }
                                }

                                break;

                            case "2":
                                var presc = new TransPrescriptionItem();
                                if (presc.LoadByPrimaryKey(row["TransactionNo"].ToString(), row["SequenceNo"].ToString()))
                                {
                                    var hpresc = new TransPrescription();
                                    hpresc.LoadByPrimaryKey(row["TransactionNo"].ToString());

                                    decimal resultQty = presc.ResultQty ?? 0;

                                    var costpresc = new CostCalculation();
                                    if (costpresc.LoadByPrimaryKey(hpresc.RegistrationNo, row["TransactionNo"].ToString(), row["SequenceNo"].ToString()))
                                    {
                                        decimal tcost = (costpresc.PatientAmount ?? 0) + (costpresc.GuarantorAmount ?? 0);
                                        decimal tplafond;
                                        if (plafondRule.IsPlafondInPercent ?? false)
                                            tplafond = tcost * (plafondRule.PlafondAmount ?? 0) / 100;
                                        else
                                        {
                                            tplafond = (plafondRule.PlafondAmount ?? 0) > Math.Abs(tcost)
                                                           ? Math.Abs(tcost)
                                                           : (plafondRule.PlafondAmount ?? 0);

                                            tplafond = (resultQty < 0) ? 0 - tplafond : tplafond;
                                        }
                                        costpresc.PatientAmount = plafondRule.IsToGuarantor ?? false
                                                                     ? tcost - tplafond
                                                                     : tplafond;
                                        costpresc.GuarantorAmount = plafondRule.IsToGuarantor ?? false
                                                                   ? tplafond
                                                                   : tcost - tplafond;

                                        costpresc.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                        costpresc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                        costpresc.Save();
                                    }
                                }
                                break;
                        }
                    }
                    trans.Complete();
                }
            }
        }

        #endregion

        #region Tab: A/R Receipt
        private DataTable GuarantorReceipts
        {
            get
            {
                if (Session["VerificationBilling:GuarantorReceipts" + UniqueID] != null)
                    return (DataTable)Session["VerificationBilling:GuarantorReceipts" + UniqueID];

                var query = new TransPaymentQuery("a");
                var detail = new TransPaymentItemQuery("b");
                var guar = new GuarantorQuery("c");

                query.InnerJoin(detail).On(
                    query.PaymentNo == detail.PaymentNo && query.IsApproved == true && query.IsVoid == false &&
                    query.TransactionCode == TransactionCode.Payment);
                query.InnerJoin(guar).On(query.GuarantorID == guar.GuarantorID);

                query.Select
                    (
                        query.PaymentNo,
                        query.PaymentDate,
                        query.PaymentTime,
                        query.GuarantorID,
                        guar.GuarantorName,
                        detail.Amount.Sum().As("PaymentAmount"),
                        @"<ISNULL((SELECT SUM(x.Amount) FROM TransPaymentItem x 
                            WHERE x.PaymentNo = a.PaymentNo 
                                AND x.SRPaymentType = 'PaymentType-005'), 0) AS DiscountAmount>",
                        query.IsApproved,
                        query.IsVoid,
                        query.Notes,
                        query.LastUpdateByUserID,
                        "<'' AS InvoiceNo>",
                        "<CAST(0 AS BIT) AS IsProceed>"
                    );
                query.GroupBy(query.PaymentNo, query.PaymentDate, query.PaymentTime, query.GuarantorID,
                              guar.GuarantorName, query.IsApproved, query.IsVoid, query.Notes,
                              query.LastUpdateByUserID);

                query.Where(query.RegistrationNo.In(MergeRegistrationList()),
                            query.Or(detail.SRPaymentType == AppSession.Parameter.PaymentTypePersonalAR,
                                     detail.SRPaymentType == AppSession.Parameter.PaymentTypeCorporateAR,
                                     detail.SRPaymentType == AppSession.Parameter.PaymentTypeSaldoAR));
                query.OrderBy(query.PaymentNo.Ascending);

                DataTable tbl = query.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    var c = new InvoicesItemCollection();
                    var qd = new InvoicesItemQuery("a");
                    var qh = new InvoicesQuery("b");
                    qd.InnerJoin(qh).On(qd.InvoiceNo == qh.InvoiceNo && qh.IsVoid == false &&
                                        qh.IsInvoicePayment == false);
                    qd.Where(qd.PaymentNo == row["PaymentNo"].ToString());
                    c.Load(qd);
                    if (c.Count > 0)
                        row["IsProceed"] = true;
                }

                tbl.AcceptChanges();

                SetSession("VerificationBilling:GuarantorReceipts", tbl);

                return tbl;
            }
            set { SetSession("VerificationBilling:GuarantorReceipts", value); }
        }

        protected void grdGuarantorReceipt_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdGuarantorReceipt.DataSource = GuarantorReceipts;
            grdGuarantorReceipt.Columns[5].Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB";
            grdGuarantorReceipt.Columns[8].Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB" && AppSession.Parameter.IsUsingBillingSlipInEnglish;
            grdGuarantorReceipt.Columns[7].Visible = AppSession.Parameter.IsUsingBillingSlipInEnglish;
        }

        protected void grdGuarantorReceipt_ItemCommand(object source, GridCommandEventArgs e)
        {
            var isPrint = false;
            string parameterName = string.Empty;
            string parameterValue = string.Empty;

            if (e.CommandName == "PrintGuarantorReceipt")
            {
                isPrint = false;
                parameterName = "PaymentNo";
                parameterValue = e.CommandArgument.ToString();

                var tp = new TransPayment();
                if (tp.LoadByPrimaryKey(e.CommandArgument.ToString()))
                {
                    tp.PrintNumber++;
                    if (!tp.IsPrinted ?? false)
                        tp.IsPrinted = true;
                    tp.LastPrintedDateTime = (new DateTime()).NowAtSqlServer();
                    tp.LastPrintedByUserID = AppSession.UserLogin.UserID;
                    tp.Save();
                }

                var jobParameters = new PrintJobParameterCollection();

                var parPaymentNo = jobParameters.AddNew();
                parPaymentNo.Name = "PaymentNo";
                parPaymentNo.ValueString = e.CommandArgument.ToString();

                var parUserName = jobParameters.AddNew();
                parUserName.Name = "UserName";
                parUserName.ValueString = AppSession.UserLogin.UserName;

                var parRegistrationNo = jobParameters.AddNew();
                parRegistrationNo.Name = "RegistrationNo";
                parRegistrationNo.ValueString = txtRegistrationNo.Text;

                var parGuarantorAskesID = jobParameters.AddNew();
                parGuarantorAskesID.Name = "GuarantorAskesID";
                parGuarantorAskesID.ValueString = string.Empty;// _guarantorAskesID;

                AppSession.PrintJobParameters = jobParameters;
                var pay = new TransPayment();
                pay.LoadByPrimaryKey(e.CommandArgument.ToString());
                if (pay.IsToGuarantor ?? false)
                    AppSession.PrintJobReportID = AppConstant.Report.RSSA_PaymentRRtInPatientG;
                else
                    AppSession.PrintJobReportID = AppConstant.Report.RSSA_PaymentRRtInPatientP;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);

                if (AppSession.Parameter.IsUsedPrintSlipLogForPaymentReceipt)
                    PrintSlipLog.InsertUpdate(AppSession.PrintJobReportID, parameterName, parameterValue, AppSession.UserLogin.UserID);

            }
            else if (e.CommandName == "PrintGuarantorBillingStatement")
            {
                isPrint = true;
                parameterName = "PaymentNo";
                parameterValue = e.CommandArgument.ToString();

                var jobParameters = new PrintJobParameterCollection();

                AppSession.PrintJobParameters = jobParameters;

                string[] registrationNoList = MergeRegistrationList();
                var jobParameter2 = jobParameters.AddNew();
                jobParameter2.Name = "RegistrationNoList";
                jobParameter2.ValueString = string.Empty;

                foreach (var str in registrationNoList)
                {
                    jobParameter2.ValueString += str + ",";
                }

                jobParameter2.ValueString = jobParameter2.ValueString.Substring(0, jobParameter2.ValueString.Length - 1);

                var parPaymentNo = jobParameters.AddNew();
                parPaymentNo.Name = "PaymentNo";
                parPaymentNo.ValueString = e.CommandArgument.ToString();

                var parRegNo = jobParameters.AddNew();
                parRegNo.Name = "RegNo";
                parRegNo.ValueString = txtRegistrationNo.Text;

                var parUserID = jobParameters.AddNew();
                parUserID.Name = "UserID";
                parUserID.ValueString = AppSession.UserLogin.UserID;

                var parUser = jobParameters.AddNew();
                parUser.Name = "UserName";
                parUser.ValueString = AppSession.UserLogin.UserName;

                var parInitialRpt = jobParameters.AddNew();
                parInitialRpt.Name = "InitialRpt";
                parInitialRpt.ValueString = "1";

                var parClassID = jobParameters.AddNew();
                parClassID.Name = "ClassID";
                parClassID.ValueString = cboBillToClassID.SelectedValue;

                AppSession.PrintJobParameters = jobParameters;
                var pay = new TransPayment();
                pay.LoadByPrimaryKey(e.CommandArgument.ToString());

                switch (_healthcareInitial)
                {
                    case "RSUI":
                    case "RSPM":
                    case "RSCH":
                    case "RSBHP":
                    case "YBRSGKP":
                    case "RSMM":
                        AppSession.PrintJobReportID = AppConstant.Report.BillingGuarantorStatementDetail;
                        break;
                    default:
                        if (pay.IsToGuarantor ?? false)
                            AppSession.PrintJobReportID = AppConstant.Report.BillingGuarantorStatementDetail;
                        else
                            AppSession.PrintJobReportID = AppConstant.Report.BillingPatientStatementDetail;

                        break;
                }

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "PrintGuarantorBillingStatement_EN")
            {
                isPrint = true;
                parameterName = "PaymentNo";
                parameterValue = e.CommandArgument.ToString();

                var jobParameters = new PrintJobParameterCollection();

                AppSession.PrintJobParameters = jobParameters;

                string[] registrationNoList = MergeRegistrationList();
                var jobParameter2 = jobParameters.AddNew();
                jobParameter2.Name = "RegistrationNoList";
                jobParameter2.ValueString = string.Empty;

                foreach (var str in registrationNoList)
                {
                    jobParameter2.ValueString += str + ",";
                }

                jobParameter2.ValueString = jobParameter2.ValueString.Substring(0, jobParameter2.ValueString.Length - 1);

                var parPaymentNo = jobParameters.AddNew();
                parPaymentNo.Name = "PaymentNo";
                parPaymentNo.ValueString = e.CommandArgument.ToString();

                var parRegNo = jobParameters.AddNew();
                parRegNo.Name = "RegNo";
                parRegNo.ValueString = txtRegistrationNo.Text;

                var parUserID = jobParameters.AddNew();
                parUserID.Name = "UserID";
                parUserID.ValueString = AppSession.UserLogin.UserID;

                var parUser = jobParameters.AddNew();
                parUser.Name = "UserName";
                parUser.ValueString = AppSession.UserLogin.UserName;

                var parInitialRpt = jobParameters.AddNew();
                parInitialRpt.Name = "InitialRpt";
                parInitialRpt.ValueString = "1";

                var parClassID = jobParameters.AddNew();
                parClassID.Name = "ClassID";
                parClassID.ValueString = cboBillToClassID.SelectedValue;

                AppSession.PrintJobParameters = jobParameters;
                var pay = new TransPayment();
                pay.LoadByPrimaryKey(e.CommandArgument.ToString());

                AppSession.PrintJobReportID = AppConstant.Report.BillingGuarantorStatementDetailEN;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "PrintPackageDetailStatement")
            {
                isPrint = true;
                parameterName = "PaymentNo";
                parameterValue = e.CommandArgument.ToString();

                var jobParameters = new PrintJobParameterCollection();

                var parPaymentNo = jobParameters.AddNew();
                parPaymentNo.Name = "PaymentNo";
                parPaymentNo.ValueString = e.CommandArgument.ToString();

                var parUser = jobParameters.AddNew();
                parUser.Name = "UserName";
                parUser.ValueString = AppSession.UserLogin.UserName;

                AppSession.PrintJobReportID = AppConstant.Report.PackagePatientStatementDetail;
                AppSession.PrintJobParameters = jobParameters;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "PrintDetailOpStatement")
            {
                isPrint = true;
                parameterName = "PaymentNo";
                parameterValue = e.CommandArgument.ToString();

                var jobParameters = new PrintJobParameterCollection();

                var parPaymentNo = jobParameters.AddNew();
                parPaymentNo.Name = "PaymentNo";
                parPaymentNo.ValueString = e.CommandArgument.ToString();

                var parUser = jobParameters.AddNew();
                parUser.Name = "UserName";
                parUser.ValueString = AppSession.UserLogin.UserName;

                AppSession.PrintJobReportID = AppConstant.Report.PaymentReceiptSlip;
                AppSession.PrintJobParameters = jobParameters;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "PrintDetailBpjsStatement")
            {
                isPrint = true;
                parameterName = "PaymentNo";
                parameterValue = e.CommandArgument.ToString();

                var jobParameters = new PrintJobParameterCollection();

                AppSession.PrintJobParameters = jobParameters;

                string[] registrationNoList = MergeRegistrationList();
                var jobParameter2 = jobParameters.AddNew();
                jobParameter2.Name = "RegistrationNoList";
                jobParameter2.ValueString = string.Empty;

                foreach (var str in registrationNoList)
                {
                    jobParameter2.ValueString += str + ",";
                }

                jobParameter2.ValueString = jobParameter2.ValueString.Substring(0, jobParameter2.ValueString.Length - 1);

                var parPaymentNo = jobParameters.AddNew();
                parPaymentNo.Name = "PaymentNo";
                parPaymentNo.ValueString = e.CommandArgument.ToString();

                var parRegNo = jobParameters.AddNew();
                parRegNo.Name = "RegNo";
                parRegNo.ValueString = txtRegistrationNo.Text;

                var parUserID = jobParameters.AddNew();
                parUserID.Name = "UserID";
                parUserID.ValueString = AppSession.UserLogin.UserID;

                var parUser = jobParameters.AddNew();
                parUser.Name = "UserName";
                parUser.ValueString = AppSession.UserLogin.UserName;

                var parInitialRpt = jobParameters.AddNew();
                parInitialRpt.Name = "InitialRpt";
                parInitialRpt.ValueString = "1";

                var parClassID = jobParameters.AddNew();
                parClassID.Name = "ClassID";
                parClassID.ValueString = cboBillToClassID.SelectedValue;

                AppSession.PrintJobParameters = jobParameters;
                var pay = new TransPayment();
                pay.LoadByPrimaryKey(e.CommandArgument.ToString());

                AppSession.PrintJobReportID = AppConstant.Report.BillingStatementBpjsByPaymentReceipt;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "PrintGuarantorOnlyBillingStatementNoDiscNoDP")
            {
                isPrint = true;
                parameterName = "PaymentNo";
                parameterValue = e.CommandArgument.ToString();

                var tp = new TransPayment();
                if (tp.LoadByPrimaryKey(e.CommandArgument.ToString()))
                {
                    tp.PrintNumber++;
                    if (!tp.IsPrinted ?? false)
                        tp.IsPrinted = true;
                    tp.LastPrintedDateTime = (new DateTime()).NowAtSqlServer();
                    tp.LastPrintedByUserID = AppSession.UserLogin.UserID;
                    tp.Save();
                }

                var jobParameters = new PrintJobParameterCollection();

                var parPaymentNo = jobParameters.AddNew();
                parPaymentNo.Name = "PaymentNo";
                parPaymentNo.ValueString = e.CommandArgument.ToString();

                var parUserName = jobParameters.AddNew();
                parUserName.Name = "UserName";
                parUserName.ValueString = AppSession.UserLogin.UserName;

                var parShowPatientPaid = jobParameters.AddNew();
                parShowPatientPaid.Name = "ShowPatientPaid";
                parShowPatientPaid.ValueNumeric = 4;

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.BillingStatementPaymentReceiptGuarantorOnly;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "PrintGuarantorOnlyBillingStatementNoDiscNoDP_EN")
            {
                isPrint = true;
                parameterName = "PaymentNo";
                parameterValue = e.CommandArgument.ToString();

                var tp = new TransPayment();
                if (tp.LoadByPrimaryKey(e.CommandArgument.ToString()))
                {
                    tp.PrintNumber++;
                    if (!tp.IsPrinted ?? false)
                        tp.IsPrinted = true;
                    tp.LastPrintedDateTime = (new DateTime()).NowAtSqlServer();
                    tp.LastPrintedByUserID = AppSession.UserLogin.UserID;
                    tp.Save();
                }

                var jobParameters = new PrintJobParameterCollection();

                var parPaymentNo = jobParameters.AddNew();
                parPaymentNo.Name = "PaymentNo";
                parPaymentNo.ValueString = e.CommandArgument.ToString();

                var parUserName = jobParameters.AddNew();
                parUserName.Name = "UserName";
                parUserName.ValueString = AppSession.UserLogin.UserName;

                var parShowPatientPaid = jobParameters.AddNew();
                parShowPatientPaid.Name = "ShowPatientPaid";
                parShowPatientPaid.ValueNumeric = 4;

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.BillingStatementPaymentReceiptGuarantorOnlyEN;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }

            else if (e.CommandName == "PrintGuarantorOnlyBillingStatement")
            {
                isPrint = true;
                parameterName = "PaymentNo";
                parameterValue = e.CommandArgument.ToString();

                var tp = new TransPayment();
                if (tp.LoadByPrimaryKey(e.CommandArgument.ToString()))
                {
                    tp.PrintNumber++;
                    if (!tp.IsPrinted ?? false)
                        tp.IsPrinted = true;
                    tp.LastPrintedDateTime = (new DateTime()).NowAtSqlServer();
                    tp.LastPrintedByUserID = AppSession.UserLogin.UserID;
                    tp.Save();
                }

                var jobParameters = new PrintJobParameterCollection();

                var parPaymentNo = jobParameters.AddNew();
                parPaymentNo.Name = "PaymentNo";
                parPaymentNo.ValueString = e.CommandArgument.ToString();

                var parUserName = jobParameters.AddNew();
                parUserName.Name = "UserName";
                parUserName.ValueString = AppSession.UserLogin.UserName;

                var parShowPatientPaid = jobParameters.AddNew();
                parShowPatientPaid.Name = "ShowPatientPaid";
                parShowPatientPaid.ValueNumeric = 2;

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.BillingStatementPaymentReceiptGuarantorOnly;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            if (isPrint && AppSession.Parameter.IsUsedPrintSlipLogForBillingStatement)
                PrintSlipLog.InsertUpdate(AppSession.PrintJobReportID, parameterName, parameterValue, AppSession.UserLogin.UserID);
        }

        protected void grdGuarantorReceipt_InsertCommand(object source, GridCommandEventArgs e)
        {
            SetEntityValue(e);

            GuarantorReceipts = null;
            grdGuarantorReceipt.Rebind();
        }

        private void SetEntityValue(GridCommandEventArgs e)
        {
            var userControl = (GuarantorReceiptDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

            if (userControl != null)
            {
                var closingperiod = userControl.PaymentDate;
                var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod.Value);
                if (isClosingPeriod)
                {
                    throw new Exception("Financial statements for period: " +
                                        string.Format("{0:MMMM-yyyy}", closingperiod) +
                                        " have been closed. Please contact the authorities.");
                    return;
                }

                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);

                var guarantor = new Guarantor();
                guarantor.LoadByPrimaryKey(userControl.GuarantorId);
                var srPaymentType = string.IsNullOrEmpty(guarantor.SRPaymentType) ? AppSession.Parameter.PaymentTypeCorporateAR : guarantor.SRPaymentType;

                var entity = new TransPayment();
                entity.AddNew();
                entity.TransactionCode = TransactionCode.Payment;
                entity.PaymentNo = GetNewPaymentNo();
                entity.RegistrationNo = txtRegistrationNo.Text;
                entity.PaymentDate = userControl.PaymentDate;
                entity.PaymentTime = userControl.PaymentTime;
                entity.PrintReceiptAsName = userControl.GuarantorName;
                entity.TotalPaymentAmount = userControl.PaymentAmount;
                entity.RemainingAmount = 0;
                entity.PrintNumber = 0;
                entity.PaymentReceiptNo = string.Empty;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entity.IsVoid = false;
                entity.IsApproved = true;
                entity.Notes = string.Empty;

                entity.GuarantorID = userControl.GuarantorId;
                entity.IsToGuarantor = true;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.ApproveByUserID = AppSession.UserLogin.UserID;
                entity.ApproveDate = (new DateTime()).NowAtSqlServer();

                var rg = new RegistrationGuarantor();
                if (!rg.LoadByPrimaryKey(txtRegistrationNo.Text, userControl.GuarantorId))
                {
                    rg.AddNew();
                    rg.RegistrationNo = txtRegistrationNo.Text;
                    rg.GuarantorID = userControl.GuarantorId;
                }
                rg.PlafondAmount = userControl.PaymentAmount;
                rg.Notes = userControl.Notes;
                rg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                rg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                //---TransPaymentItem
                var collPaymentItem = new TransPaymentItemCollection();
                var pi = collPaymentItem.AddNew();
                pi.PaymentNo = entity.PaymentNo;
                pi.SequenceNo = "001";
                pi.SRPaymentType = srPaymentType;

                var type = new PaymentType();
                pi.PaymentTypeName = type.LoadByPrimaryKey(pi.SRPaymentType) ? type.PaymentTypeName : string.Empty;

                pi.SRPaymentMethod = string.Empty;
                pi.PaymentMethodName = string.Empty;
                pi.Amount = userControl.PaymentAmount;
                pi.RoundingAmount = 0;
                pi.Balance = 0;
                pi.IsFromDownPayment = false;
                pi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                pi.LastUpdateByUserID = AppSession.UserLogin.UserID;

                reg.RemainingAmount -= userControl.PaymentAmount;

                #region Close Registration
                if ((reg.SRRegistrationType == AppConstant.RegistrationType.InPatient && AppSession.Parameter.IsAutoClosedRegIpOnPayment) ||
                    (reg.SRRegistrationType != AppConstant.RegistrationType.InPatient && AppSession.Parameter.IsAutoClosedRegOpOnPayment))
                {
                    string[] regno = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);

                    var isClosed = Helper.RegistrationOpenClose.GetStatusClosed(reg, regno, 0, userControl.PaymentAmount);
                    if (isClosed)
                    {
                        var isAutoClosedRegOnPaymentWithHoldTx = AppSession.Parameter.IsAutoClosedRegOnPaymentWithHoldTx;

                        var coll = new MergeBillingCollection();
                        coll.Query.Where(coll.Query.FromRegistrationNo == entity.RegistrationNo);
                        coll.LoadAll();

                        var regs = new string[coll.Count + 1];
                        var idx = 1;

                        regs.SetValue(entity.RegistrationNo, 0);

                        foreach (var bill in coll)
                        {
                            regs.SetValue(bill.RegistrationNo, idx);
                            idx++;
                        }

                        var regis = new RegistrationCollection();
                        regis.Query.Where(regis.Query.RegistrationNo.In(regs));
                        regis.LoadAll();

                        var historys = new RegistrationCloseOpenHistoryCollection();

                        foreach (var re in regis)
                        {
                            var hist = historys.AddNew();
                            hist.RegistrationNo = re.RegistrationNo;
                            if (!isAutoClosedRegOnPaymentWithHoldTx)
                            {
                                re.IsClosed = true;

                                hist.StatusId = "C";
                            }
                            else
                            {
                                re.IsHoldTransactionEntry = true;
                                re.IsHoldTransactionEntryByUserID = AppSession.UserLogin.UserID;

                                hist.StatusId = "H";
                            }
                            re.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            re.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                            hist.IsTrue = true;
                            hist.Notes = "Verification & Finalize Billing";
                            hist.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            hist.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        }

                        var ques = new ServiceUnitQueCollection();
                        ques.Query.Where(ques.Query.RegistrationNo.In(regs));
                        ques.LoadAll();

                        foreach (var que in ques)
                        {
                            if (!isAutoClosedRegOnPaymentWithHoldTx)
                                que.IsClosed = true;

                            que.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            que.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        }

                        using (var trans = new esTransactionScope())
                        {
                            regis.Save();
                            historys.Save();

                            if (ques.Count > 0)
                                ques.Save();

                            trans.Complete();
                        }

                        var ques2 = new ServiceUnitQueCollection();
                        ques2.Query.Where(ques2.Query.RegistrationNo == entity.RegistrationNo);
                        ques2.LoadAll();

                        foreach (var que in ques2)
                        {
                            if (!isAutoClosedRegOnPaymentWithHoldTx)
                                que.IsClosed = true;
                            que.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            que.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        }

                        using (var trans = new esTransactionScope())
                        {
                            if (ques2.Count > 0)
                                ques2.Save();

                            trans.Complete();
                        }

                        if (!isAutoClosedRegOnPaymentWithHoldTx)
                            reg.IsClosed = true;
                        else
                        {
                            reg.IsHoldTransactionEntry = true;
                            reg.IsHoldTransactionEntryByUserID = AppSession.UserLogin.UserID;
                        }
                    }
                }
                #endregion

                using (var trans = new esTransactionScope())
                {
                    entity.Save();
                    collPaymentItem.Save();
                    rg.Save();

                    _autoNumberPayment.Save();

                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "Yes")
                    {
                        int? journalId = JournalTransactions.AddNewPaymentCashBasedJournal(BusinessObject.JournalType.ARCreating,
                            entity, reg, collPaymentItem,
                            "TP", entity.PaymentNo, AppSession.UserLogin.UserID, 0);
                    }
                    else
                    {
                        var rtype = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                        if (rtype.Contains(reg.SRRegistrationType))
                        {

                            int? journalId = JournalTransactions.AddNewPaymentCashBasedJournalTemporary(BusinessObject.JournalType.ARCreating,
                                entity, reg, collPaymentItem,
                                "TP", entity.PaymentNo, AppSession.UserLogin.UserID, 0);
                        }
                        else
                        {
                            int? journalId = JournalTransactions.AddNewPaymentCashBasedJournal(BusinessObject.JournalType.ARCreating,
                                entity, reg, collPaymentItem,
                                "TP", entity.PaymentNo, AppSession.UserLogin.UserID, 0);
                        }
                    }

                    #region Guarantor Deposit Balance

                    if (srPaymentType == AppSession.Parameter.PaymentTypeSaldoAR)
                    {
                        var balance = new GuarantorDepositBalance();
                        var movement = new GuarantorDepositMovement();
                        GuarantorDepositBalance.PrepareGuarantorDepositBalances(entity.PaymentNo, entity.GuarantorID,
                                                                                "A/R Process", AppSession.UserLogin.UserID,
                                                                                0,
                                                                                userControl.PaymentAmount,
                                                                                ref balance, ref movement);
                        balance.Save();
                        movement.Save();
                    }

                    #endregion

                    // update informasi payment jasmed
                    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    feeColl.RecalculateForFeeByPlafonGuarantor(entity, collPaymentItem, AppSession.UserLogin.UserID);
                    feeColl.SetPayment(entity, collPaymentItem, 3, AppSession.UserLogin.UserID);
                    feeColl.Save();

                    // rekal untuk prorata ???
                    var ba = new BillingAdjustment(entity.RegistrationNo, true);
                    var msg = ba.CalculateAndSaveProrata_NoTransactionScope(AppSession.UserLogin.UserID);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        throw new Exception(msg);
                        return;
                    }

                    trans.Complete();
                }
            }
        }

        private string GetNewPaymentNo()
        {
            _autoNumberPayment = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer(), AppEnum.AutoNumber.PaymentNo);
            return _autoNumberPayment.LastCompleteNumber;
        }
        #endregion

        #region Plafond Balance

        private void PlanfondCalculatation(string type)
        {
            decimal plafond = 0;
            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            switch (type)
            {
                case "bpjs":
                    plafond = reg.PlavonAmount ?? 0;
                    CheckPlafondDetail(true);
                    break;
            }
            txtPlafonValue.Value = Convert.ToDouble(plafond);
        }

        private void CheckPlafondDetail(bool isPlafond)
        {
            if (isPlafond)
            {
                var pla = new RegistrationCoverageDetailCollection();
                pla.Query.Where(pla.Query.RegistrationNo == txtRegistrationNo.Text);
                txtPlafonValue.ReadOnly = (pla.Query.Load() && pla.Count > 0);
            }
        }

        private void ResetSessionPlafond()
        {
            Session["reg"] = null;
            Session["regnos"] = null;
            Session["cobPlafond"] = null;
            Session["patientamt"] = null;
            Session["guarantoramt"] = null;
            Session["guarantortot"] = null;
            Session["patienttot"] = null;
        }

        private decimal PlafondValueUsedInPercent(decimal totalGuarantorAndRemainingPatientAmount, decimal totalPlafond)
        {
            if (totalPlafond == 0)
                totalPlafond = 1;

            var plafonUsedPercent = (totalGuarantorAndRemainingPatientAmount / totalPlafond) * (decimal)100;
            return plafonUsedPercent;
        }

        private Registration Registration
        {
            get
            {
                var reg = new Registration();
                if (Session["reg"] != null)
                {
                    reg = (Registration)Session["reg"];
                    if (reg.RegistrationNo == txtRegistrationNo.Text)
                        return reg;
                }

                reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                Session["reg"] = reg;
                return reg;
            }
        }
        private string[] MergeRegistrationNos
        {
            get
            {
                var regnos = new string[0];
                if (Session["regnos"] != null)
                {
                    regnos = (string[])Session["regnos"];
                    if (Registration.RegistrationNo == txtRegistrationNo.Text)
                        return regnos;
                }
                regnos = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);
                Session["regnos"] = regnos;
                return regnos;
            }
        }
        private decimal TotalPlafond(Registration reg)
        {
            if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
            {
                var plafondAmt = (decimal)txtPlafonValue.Value;

                if (GuarantorBPJS.Contains(reg.GuarantorID))// && plafondAmt == 0)
                    plafondAmt = (decimal)(reg.ApproximatePlafondAmount == null ? 0 : reg.ApproximatePlafondAmount);

                return plafondAmt + AdditionalPlafond;
            }
            else
            {
                var plafondAmt = GuarantorServiceUnitPlafond.GetPlafondAmount(reg.GuarantorID, reg.ServiceUnitID, GuarantorBPJS.Contains(reg.GuarantorID));

                if (plafondAmt > 0) 
                    return plafondAmt;

                return reg.ApproximatePlafondAmount ?? 0;

                //if (AppParameter.GetParameterValue(AppParameter.ParameterItem.NonInPatientBpjsPlafond).ToDecimal() > 0) return AppParameter.GetParameterValue(AppParameter.ParameterItem.NonInPatientBpjsPlafond).ToDecimal();
                //else return reg.ApproximatePlafondAmount ?? 0;
            }
        }
        private decimal AdditionalPlafond
        {
            get
            {
                decimal cobPlafond = 0;
                if (Session["cobPlafond"] != null)
                {
                    if (Registration.RegistrationNo == txtRegistrationNo.Text)
                    {
                        cobPlafond = (decimal)Session["cobPlafond"];
                        return cobPlafond;
                    }
                }
                var cob = new RegistrationGuarantorCollection();
                cob.Query.Where(cob.Query.RegistrationNo == txtRegistrationNo.Text);
                cob.LoadAll();
                cobPlafond = cob.Sum(c => (c.PlafondAmount ?? 0));
                Session["cobPlafond"] = cobPlafond;
                return cobPlafond;
            }
        }
        private void PopulateBilling()
        {
            decimal tpatient;
            decimal tguarantor;
            var reg = Registration;
            var regnos = MergeRegistrationNos;
            decimal cobPlafond = AdditionalPlafond;

            var guarantor = new Guarantor();
            guarantor.LoadByPrimaryKey(reg.GuarantorID);

            Helper.CostCalculation.GetBillingTotal2(regnos, reg.SRBussinesMethod, (reg.PlavonAmount ?? 0) + cobPlafond, out tpatient, out tguarantor,
                                                   guarantor, reg.IsGlobalPlafond ?? false);

            Session["patientamt"] = tpatient;
            Session["guarantoramt"] = tguarantor;
        }

        private decimal TotalGuarantorAndRemainingPatientAmount(Registration reg)
        {
            var guarantor = new Guarantor();
            guarantor.LoadByPrimaryKey(reg.GuarantorID);

            decimal tpatient;
            decimal tguarantor;

            Helper.CostCalculation.GetBillingTotal2(MergeRegistrationNos, reg.SRBussinesMethod, 0, out tpatient, out tguarantor,
                                                   guarantor, reg.IsGlobalPlafond ?? false);

            var totalRemain = tguarantor + tpatient;

            return totalRemain;
        }

        private decimal TotalGuarantorAmount()
        {
            decimal guarantortot = 0;
            if (Session["guarantortot"] != null)
            {
                if (Registration.RegistrationNo == txtRegistrationNo.Text)
                {
                    guarantortot = (decimal)Session["guarantortot"];
                    return guarantortot;
                }
            }
            PopulateBilling(); // Hitung AmountGuarantor
                               //var discGuarantor = (decimal)Helper.Payment.GetPaymentDiscount(MergeRegistrationNos, true);
            var discGuarantor = 0;
            var totalAmountGuarantor = (decimal)Session["guarantoramt"] - discGuarantor;
            Session["guarantortot"] = totalAmountGuarantor;
            return totalAmountGuarantor;
        }

        private decimal RemainingPatientAmount
        {
            get
            {
                decimal patienttot = 0;
                if (Session["patienttot"] != null)
                {
                    if (Registration.RegistrationNo == txtRegistrationNo.Text)
                    {
                        patienttot = (decimal)Session["patienttot"];
                        return patienttot;
                    }
                }
                PopulateBilling(); // Hitung Amount Guarantor & Patient

                //var reg = Registration;
                //var mergeRegistrationNos = MergeRegistrationNos;
                //var discPatient = (decimal)Helper.Payment.GetPaymentDiscount(mergeRegistrationNos, false);

                //string[] patientParam = new string[2];
                //patientParam.SetValue(AppSession.Parameter.PaymentTypePayment, 0);
                //patientParam.SetValue(AppSession.Parameter.PaymentTypePersonalAR, 1);

                //decimal tpayment = Helper.Payment.GetTotalPayment(mergeRegistrationNos, true, patientParam);
                //decimal treturn = Helper.Payment.GetTotalPayment(mergeRegistrationNos, false);
                //var totalPaymentAmountPatient = (decimal)tpayment + (decimal)treturn;

                var tpatient = (decimal)Session["patientamt"];
                //var remainingAmountPatient = tpatient - totalPaymentAmountPatient - discPatient;
                var remainingAmountPatient = tpatient;

                //decimal selisih = 0;
                ////selisih kelas untuk bpjs
                //if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                //{
                //    if (AppSession.Parameter.GuarantorAskesID.Contains(reg.GuarantorID))
                //    {
                //        if (reg.CoverageClassID != reg.ChargeClassID)
                //        {
                //            //if ((reg.PlavonAmount2 ?? 0) > 0)
                //            //{
                //            //    var class1 = new Class();
                //            //    class1.LoadByPrimaryKey(reg.CoverageClassID);

                //            //    var asri1 = new AppStandardReferenceItem();
                //            //    asri1.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class1.SRClassRL);

                //            //    var class2 = new Class();
                //            //    class2.LoadByPrimaryKey(reg.ChargeClassID);

                //            //    var asri2 = new AppStandardReferenceItem();
                //            //    asri2.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class2.SRClassRL);

                //            //    if (asri2.Note.ToInt() < asri1.Note.ToInt())
                //            //        selisih = (reg.PlavonAmount2 ?? 0) - (reg.PlavonAmount ?? 0);
                //            //}

                //            var cov = new RegistrationCoverageDetail();
                //            cov.Query.Select(cov.Query.CalculatedAmount.Sum());
                //            cov.Query.Where(cov.Query.RegistrationNo == reg.RegistrationNo);
                //            if (cov.Query.Load()) selisih = cov.CalculatedAmount ?? 0;
                //        }
                //    }
                //}

                //if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                //{
                //    if (AppSession.Parameter.GuarantorAskesID.Contains(reg.GuarantorID))
                //        if (reg.CoverageClassID != reg.ChargeClassID)
                //            //if ((reg.PlavonAmount2 ?? 0) > 0)
                //            //    remainingAmountPatient = (((reg.PlavonAmount2 ?? 0) == 0) ? (decimal)tpatient : (decimal)selisih) - totalPaymentAmountPatient - discPatient;
                //            remainingAmountPatient = ((decimal)selisih > (decimal)tpatient ? ((decimal)tpatient == 0 ? (decimal)selisih : (decimal)tpatient) : (decimal)selisih) - totalPaymentAmountPatient - discPatient;
                //}
                Session["patienttot"] = remainingAmountPatient;
                return remainingAmountPatient;
            }
        }

        private string[] GuarantorBPJS
        {
            get
            {
                if (ViewState["bpjs"] != null) return (string[])ViewState["bpjs"];
                var grr = new GuarantorBridgingCollection();
                grr.Query.es.Distinct = true;
                grr.Query.Where(grr.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(),
                                                            AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(),
                                                            AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
                if (grr.Query.Load()) ViewState["bpjs"] = grr.Select(g => g.GuarantorID).ToArray();
                else ViewState["bpjs"] = new string[] { string.Empty };

                return (string[])ViewState["bpjs"];
            }
        }

        //protected double TotalPatientAmount
        //{
        //    get {
        //        double patienttot = 0;
        //        if (Session["patienttot"] != null)
        //        {
        //            if (Registration.RegistrationNo == txtRegistrationNo.Text)
        //            {
        //                patienttot = (double)Session["patienttot"];
        //                return patienttot;
        //            }
        //        }
        //        PopulateBilling(); // Hitung AmountGuarantor

        //    var reg = Registration;
        //    var mergeRegistrationNos = MergeRegistrationNos;
        //    var discPatient = (double)Helper.Payment.GetPaymentDiscount(mergeRegistrationNos, false);
        //    var plafonAmount = (double)((reg.PlavonAmount ?? 0) + AdditionalPlafond);
        //    var adminCal = (double)(reg.AdministrationAmount ?? 0);
        //    var patientAdm = (double)(reg.PatientAdm ?? 0);
        //    var guarantorAdm = (double)(reg.GuarantorAdm ?? 0);
        //    var downPaymentAmount = (double)(Helper.Payment.GetTotalDownPayment(mergeRegistrationNos) - Helper.Payment.GetTotalDownPaymentReturn(mergeRegistrationNos));
        //    var discountAmount = (double)Helper.Payment.GetTotalPaymentDiscount(mergeRegistrationNos);

        //    string[] patientParam = new string[2];
        //    patientParam.SetValue(AppSession.Parameter.PaymentTypePayment, 0);
        //    patientParam.SetValue(AppSession.Parameter.PaymentTypePersonalAR, 1);

        //    decimal tpayment = Helper.Payment.GetTotalPayment(mergeRegistrationNos, true, patientParam);
        //    decimal treturn = Helper.Payment.GetTotalPayment(mergeRegistrationNos, false);
        //    var totalPaymentAmountPatient = (double)tpayment + (double)treturn;

        //    var tpatient = (double)Session["patientamt"];
        //    var remainingAmountPatient = tpatient - totalPaymentAmountPatient - discPatient;

        //    decimal selisih = 0;
        //    //selisih kelas untuk bpjs
        //    if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
        //    {
        //        if (AppSession.Parameter.GuarantorAskesID.Contains(reg.GuarantorID))
        //        {
        //            if (reg.CoverageClassID != reg.ChargeClassID)
        //            {
        //                if ((reg.PlavonAmount2 ?? 0) > 0)
        //                {
        //                    var class1 = new Class();
        //                    class1.LoadByPrimaryKey(reg.CoverageClassID);

        //                    var asri1 = new AppStandardReferenceItem();
        //                    asri1.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class1.SRClassRL);

        //                    var class2 = new Class();
        //                    class2.LoadByPrimaryKey(reg.ChargeClassID);

        //                    var asri2 = new AppStandardReferenceItem();
        //                    asri2.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class2.SRClassRL);

        //                    if (asri2.Note.ToInt() < asri1.Note.ToInt())
        //                        selisih = (reg.PlavonAmount2 ?? 0) - (reg.PlavonAmount ?? 0);
        //                }
        //            }
        //        }
        //    }

        //    if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
        //    {
        //        if (AppSession.Parameter.GuarantorAskesID.Contains(reg.GuarantorID))
        //            if (reg.CoverageClassID != reg.ChargeClassID)
        //                if ((reg.PlavonAmount2 ?? 0) > 0)
        //                    remainingAmountPatient = (((reg.PlavonAmount2 ?? 0) == 0) ? (double)tpatient : (double)selisih) - totalPaymentAmountPatient - discPatient;
        //    }
        //    return totalPaymentAmountPatient;
        //    }
        //}

        //        private decimal TransChargesAmount()
        //        {
        //            // Service Unit Transaction
        //            var query = new TransChargesItemQuery("a");
        //            var header = new TransChargesQuery("c");
        //            query.Select
        //                (
        //                    @"<SUM(CASE WHEN a.IsApprove=1 AND a.IsBillProceed = 1 THEN 
        //                                CASE WHEN c.IsCorrection=1 
        //                                    THEN 0-(((ABS(a.ChargeQuantity)*a.Price) - a.DiscountAmount)+a.CitoAmount)
        //                                    ELSE ((a.ChargeQuantity*a.Price)-a.DiscountAmount)+a.CitoAmount
        //                                END
        //                            ELSE 0 END) as Total>"
        //                );

        //            query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo);
        //            query.Where(
        //                    header.RegistrationNo.In(MergeRegistrationList()),
        //                     query.Or(
        //                        header.PackageReferenceNo == string.Empty,
        //                        header.PackageReferenceNo.IsNull()
        //                        ),
        //                    header.IsVoid == false,
        //                    query.IsVoid == false,
        //                    query.Or(
        //                    query.ParentNo == string.Empty,
        //                    query.ParentNo.IsNull()
        //                    )
        //                );


        //            var tbl = query.LoadDataTable();
        //            if (tbl.Rows != null && tbl.Rows.Count > 0)
        //            {
        //                return Convert.ToDecimal(tbl.Rows[0][0]);
        //            }

        //            return 0;
        //        }

        //        private decimal TransPrescriptionAmount()
        //        {
        //            var query = new TransPrescriptionItemQuery("a");
        //            var header = new TransPrescriptionQuery("c");
        //            var cost = new CostCalculationQuery("e");
        //            query.Select
        //                (
        //                    @"<SUM(CASE WHEN a.IsApprove = 1 AND a.IsBillProceed = 1 THEN a.LineAmount
        //                            ELSE 0 END) as Total>"

        //                );

        //            query.InnerJoin(header).On(query.PrescriptionNo == header.PrescriptionNo);
        //            query.LeftJoin(cost).On(
        //                    query.PrescriptionNo == cost.TransactionNo &&
        //                    query.SequenceNo == cost.SequenceNo
        //                );

        //            query.Where(
        //                    header.RegistrationNo.In(MergeRegistrationList()),
        //                    header.IsVoid == false,
        //                    query.IsVoid == false
        //                );

        //            var tbl = query.LoadDataTable();
        //            if (tbl.Rows != null && tbl.Rows.Count > 0)
        //            {
        //                return Convert.ToDecimal(tbl.Rows[0][0]);
        //            }
        //            return 0;
        //        }
        #endregion

        protected string PlafondStatus()
        {
            if (hdnBpjsLabel.Value != "bpjs")
                return string.Empty;

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            var totalPlafond = TotalPlafond(reg);
            var totalGuarantorAndRemainingPatientAmount = TotalGuarantorAndRemainingPatientAmount(reg);
            var plafondValueUsedInPercent = PlafondValueUsedInPercent(totalGuarantorAndRemainingPatientAmount, totalPlafond);

            var grouper = string.Empty;
            if (GuarantorBPJS.Contains(reg.GuarantorID))
            {
                var ncc = new NccInacbg();
                if (ncc.LoadByPrimaryKey(reg.RegistrationNo)) grouper = $"({ncc.CbgID}) {ncc.CbgName}";
            }

            var plafondStatus = string.Format(@"
<table cellpadding='1' cellspacing='1' style='width:100%;border:1px; color:white;'>
    <tr align='center' style='background:gray;'>
        <td style='width:150px;'>Plafond</td>
        <td>Plafond Status</td>
        <td style='width:154px;'>Guarantor + Remaining Patient</td>
        <td style='width:204px;{8}'>E-Klaim Grouper</td>
    </tr>
    <tr style='font-weight:bold;'>
        <td style='background:gray;' align='center'>{0:n2}</td>
        <td>
            <table cellpadding='0' cellspacing='0' style='width:100%'>
                <tr align='center'>
                    <td id='usedplafond' style='background:{4};width:{2}%;'>{3:n2}%{6}</td>
                    <td id='remaining' style='background:black;'/>
                </tr>
            </table>                
            <table cellpadding='0' cellspacing='0' style='width:100%'>                
                <tr align='center'>
                    <td style='background:gray;width:{5}%;'>100% ({0:n2})</td>
                    <td style='background:black;'></td>
                </tr>                                                        
            </table>                                  
        </td>
        <td style='background:gray;' align='center'>{1:n2}</td>
        <td style='background:gray;{8}' align='center'>{7}</td>
    </tr>
</table>",
            totalPlafond, totalGuarantorAndRemainingPatientAmount,
             plafondValueUsedInPercent > 100 ? 100 : plafondValueUsedInPercent,
             plafondValueUsedInPercent,
             plafondValueUsedInPercent > 100 ? "red" : plafondValueUsedInPercent > 75 ? "yellow" : "green",
             plafondValueUsedInPercent < 100 ? 100 : 100 / (plafondValueUsedInPercent / (decimal)100),
             plafondValueUsedInPercent > 100 ? string.Format(" ({0:n2})", totalGuarantorAndRemainingPatientAmount - totalPlafond) : string.Empty,
             grouper,
             string.IsNullOrWhiteSpace(grouper) ? "display:none;" : string.Empty    
         );
            return plafondStatus;
        }

        protected string GetItemTemplatePatientID(GridItem container)
        {
            return string.Format("<a href=\"#\" onclick=\"openWinPatient('edit', '{0}', 'patient','0'); return false;\"><b>{0}</b></a>",
                    DataBinder.Eval(container.DataItem, "PatientID"));
        }
    }
}
