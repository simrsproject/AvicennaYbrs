using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using System.Globalization;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class RegistrationList : BasePage
    {
        private AppAutoNumberLast _autoNumberReg, _autoNumberTrans;
        private bool _isHideEmptySearchMessage = false;
        private string _patientId = string.Empty;
        private string _registrationNo = string.Empty;
        private string _serviceUnitId = string.Empty;

        protected string RegistrationType
        {
            get { return (string)ViewState["_regType" + Request.UserHostName]; }
            set { ViewState["_regType" + Request.UserHostName] = value; }
        }

        protected string BuildingID
        {
            get { return (string)ViewState["_parBuildingId" + Request.UserHostName]; }
            set { ViewState["_parBuildingId" + Request.UserHostName] = value; }
        }

        protected string GetItemTemplatePatientID(GridItem container)
        {
            return string.Format("<a href=\"#\" onclick=\"openWinPatient('edit', '{0}', 'patient','0'); return false;\"><b>{0}</b></a>",
                    DataBinder.Eval(container.DataItem, "PatientID"));
        }

        protected string GetItemTemplateReservationNo(GridItem container)
        {
            return string.Format("<a href=\"#\" onclick=\"openReservationAppt('edit', '{0}', 'patient','0'); return false;\"><b>{0}</b></a>",
                    DataBinder.Eval(container.DataItem, "ReservationNo"));
        }

        protected void SendToLokadok(string PatientID)
        {
            if (Helper.IsLokadokIntegration)
            {
                var entity = new Patient();
                if (entity.LoadByPrimaryKey(PatientID))
                {
                    // send new patient to lokadok
                    if (string.IsNullOrEmpty(entity.MedicalNo)) return;

                    var saveToLokadok = Common.Lokadok.Helper.AddPatient(
                        entity.MedicalNo, entity.PatientName, entity.MobilePhoneNo,
                        entity.DateOfBirth.Value, entity.Sex);
                    entity.IsStoredToLokadok = saveToLokadok;
                    entity.Save();
                }
            }
        }

        protected bool IsStoredToLokadok(GridItem container)
        {
            if (!Helper.IsLokadokIntegration) return true;
            if (DataBinder.Eval(container.DataItem, "IsStoredToLokadok") is DBNull) return true; // hide in grid
            return DataBinder.Eval(container.DataItem, "IsStoredToLokadok").Equals(true);
        }

        protected string GetUrlForNewRegistration(GridItem container)
        {
            //return (
            //    DataBinder.Eval(container.DataItem, "IsRegisterAble").Equals(false) ? string.Empty :
            //        string.Format(
            //            "<a href=\"#\" onclick=\"openWinReg('new', '{0}','0'); return false;\"><img src=\"../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New Registration\" title=\"New Registration\" /></a>",
            //            DataBinder.Eval(container.DataItem, "PatientID")
            //            )
            //    );

            //return (
            //    DataBinder.Eval(container.DataItem, "IsRegisterAble").Equals(false) ? string.Empty :
            //        (DataBinder.Eval(container.DataItem, "IsNotPaidOff").Equals(true) && AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH" && RegistrationType.Equals(AppConstant.RegistrationType.OutPatient) ? 
            //        string.Format("<a href=\"#\" return false;\"><img src=\"../../../Images/Attention.png\" border=\"0\" title=\"Have Outstanding Transaction\" /></a>") :
            //        DataBinder.Eval(container.DataItem, "IsValidateByZipCode").Equals(true) && DataBinder.Eval(container.DataItem, "ZipCode").Equals(string.Empty) ?
            //            string.Format("<a href=\"#\" onclick=\"alert('Patient zipcode is required.');openWinPatient('edit', '{0}', 'patient','0');return false;\"><img src=\"../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New Registration\" title=\"New Registration\" /></a>", DataBinder.Eval(container.DataItem, "PatientID")) :
            //            string.Format("<a href=\"#\" onclick=\"openWinReg('new', '{0}','0'); return false;\"><img src=\"../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New Registration\" title=\"New Registration\" /></a>", DataBinder.Eval(container.DataItem, "PatientID")))
            //    );


            return (
                DataBinder.Eval(container.DataItem, "IsRegisterAble").Equals(false) ? "<img src=\"../../../Images/Toolbar/new16_d.png\" border=\"0\" title=\"Pending Registration\" />" :
                    (DataBinder.Eval(container.DataItem, "IsNotPaidOff").Equals(true) && AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH" && RegistrationType.Equals(AppConstant.RegistrationType.OutPatient) ? string.Format("<a href=\"#\" return false;\"><img src=\"../../../Images/Attention.png\" border=\"0\" title=\"Have Outstanding Transaction\" /></a>") :
                    string.Format(
                        "<a href=\"#\" onclick=\"openWinReg('new', '{0}','0'); return false;\"><img src=\"../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New Registration\" title=\"New Registration\" /></a>",
                        DataBinder.Eval(container.DataItem, "PatientID")
                        ))
                );

            //return GetUrlNewRegistration(
            //        (bool)DataBinder.Eval(container.DataItem, "IsRegisterAble"),
            //        (bool)DataBinder.Eval(container.DataItem, "IsNotPaidOff"),
            //        (string)DataBinder.Eval(container.DataItem, "PatientID")
            //    );
        }

        protected string GetUrlForNewRegistrationFromLokadok(GridItem container)
        {
            return ((DataBinder.Eval(container.DataItem, "RegistrationNoRef") ?? string.Empty).Equals(string.Empty) && !(bool)DataBinder.Eval(container.DataItem, "PatientNotFound") ?
                string.Format("<a href=\"#\" onclick=\"openWinRegByNoRMFromLokadok('new','0','{0}','{1}','8'); return false;\"><img src=\"../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New Registration\" title=\"New Registration\" /></a>",
                    DataBinder.Eval(container.DataItem, "pid"), DataBinder.Eval(container.DataItem, "apptid")) :
                string.Empty
            );
        }

        //private string GetUrlNewRegistration(bool IsRegisterable, bool IsNotPaidOff, string PatientID)
        //{
        //    return (!IsRegisterable ? string.Empty :
        //            (IsNotPaidOff && AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH" && RegistrationType.Equals(AppConstant.RegistrationType.OutPatient) ? string.Format("<a href=\"#\" return false;\"><img src=\"../../../Images/Attention.png\" border=\"0\" title=\"Have Outstanding Transaction\" /></a>") :
        //            string.Format("<a href=\"#\" onclick=\"openWinReg('new', '{0}','0'); return false;\"><img src=\"../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New Registration\" title=\"New Registration\" /></a>", PatientID))
        //        );
        //}

        protected string GetUrlForRegistration(GridItem container)
        {
            //return (DataBinder.Eval(container.DataItem, "PatientID").ToString().Equals(string.Empty) ?
            //          string.Format("<a href=\"#\" onclick=\"openWinPatientFromAppt('{0}'); return false;\">New Patient</a>", DataBinder.Eval(container.DataItem, "AppointmentNo"))
            //            : string.Format("<a href=\"#\" onclick=\"openWinRegFromAppt('{0}'); return false;\">New Reg.</a>", DataBinder.Eval(container.DataItem, "AppointmentNo")));
            return (DataBinder.Eval(container.DataItem, "PatientID").ToString().Equals(string.Empty) ?
                      string.Format("<a href=\"#\" onclick=\"openWinPatientFromAppt('{0}'); return false;\"><img src=\"../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New Patient\" title=\"New Patient\" /></a>", DataBinder.Eval(container.DataItem, "AppointmentNo"))
                        : string.Format("<a href=\"#\" onclick=\"openWinRegFromAppt('{0}'); return false;\"><img src=\"../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New Registration\" title=\"New Registration\" /></a>", DataBinder.Eval(container.DataItem, "AppointmentNo")));
        }

        protected string GetUrlForChangePatient(GridItem container)
        {
            return string.Format("<a href=\"#\" onclick=\"openWinChangePatient('{0}')\"><img src=\"../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"Change Patient\" title=\"Change Patient\" /></a>", DataBinder.Eval(container.DataItem, "AppointmentNo").ToString());
        }

        protected string GetUrlForEditPatient(GridItem container)
        {
            return (DataBinder.Eval(container.DataItem, "PatientID").ToString().Equals(string.Empty) ? DataBinder.Eval(container.DataItem, "PatientName").ToString()
                    : string.Format("<a href=\"#\" onclick=\"openWinPatient('edit', '{0}', 'patient','reg');return false;\">{2} {1}</a>", DataBinder.Eval(container.DataItem, "PatientID"), DataBinder.Eval(container.DataItem, "PatientName"), DataBinder.Eval(container.DataItem, "SalutationName")));
        }

        protected string GetUrlForEditPatientAppt(GridItem container)
        {
            return (DataBinder.Eval(container.DataItem, "PatientID").ToString().Equals(string.Empty) ? DataBinder.Eval(container.DataItem, "PatientName").ToString()
                    : string.Format("<a href=\"#\" onclick=\"openWinPatient('edit', '{0}', 'patient','appt');return false;\">{2} {1}</a>", DataBinder.Eval(container.DataItem, "PatientID"), DataBinder.Eval(container.DataItem, "PatientName"), DataBinder.Eval(container.DataItem, "SalutationName")));
        }


        protected string GetUrlForNewRegistrationFromReservation(GridItem container)
        {
            return (string.Format("<a href=\"#\" onclick=\"openWinRegFromReservation('{0}'); return false;\"><img src=\"../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New Registration\" /></a>",
                DataBinder.Eval(container.DataItem, "ReservationNo")));
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            var regType = Request.QueryString["rt"];
            if (string.IsNullOrEmpty(regType))
                regType = AppConstant.RegistrationType.ClusterPatient;

            switch (regType)
            {
                case AppConstant.RegistrationType.InPatient:
                    ProgramID = AppConstant.Program.Admitting;
                    break;
                case AppConstant.RegistrationType.OutPatient:
                    ProgramID = AppConstant.Program.OutPatientRegistration;
                    break;
                case AppConstant.RegistrationType.ClusterPatient:
                    ProgramID = AppConstant.Program.ClusterPatientRegistration;
                    break;
                case AppConstant.RegistrationType.EmergencyPatient:
                    ProgramID = AppConstant.Program.EmergencyPatientRegistration;
                    break;
                case AppConstant.RegistrationType.MedicalCheckUp:
                    ProgramID = AppConstant.Program.HealthScreeningRegistration;
                    break;
                case AppConstant.RegistrationType.Ancillary:
                    ProgramID = AppConstant.Program.AncillaryRegistration;
                    break;
            }

            if (!IsPostBack)
            {
                //grdPatient.Columns[grdPatient.Columns.Count - 1].Visible = Helper.IsLokadokIntegration;
                grdPatient.Columns.FindByUniqueName("TemplateColumnToLokadok").Visible = Helper.IsLokadokIntegration;
                grdPatient.Columns.FindByUniqueName("PrescriptionHistory").Visible = AppSession.Parameter.IsShowPrescriptionHistoryOnRegistration;

                trDisdukcapil.Visible = Helper.IsDukcapilIntegration;
                trSsn.Visible = !trDisdukcapil.Visible;

                switch (regType)
                {
                    case AppConstant.RegistrationType.InPatient:
                        tabStrip.Tabs[3].Visible = true; //pgReservation

                        grdRegisteredList.Columns[10].Visible = true; //class
                        grdRegisteredList.Columns[11].Visible = true; //bed

                        grdRegisteredList.Columns[17].Visible = (AppSession.Parameter.IsGuarantorInRegEditable);
                        grdRegisteredList.Columns[20].Visible = false; // edit physician
                        grdRegisteredList.Columns[21].Visible = false; // Substitute Physician

                        grdRegisteredList.Columns[23].Visible = false; // transfer to inpatient
                        grdRegisteredList.Columns[24].Visible = false; // transfer to emr

                        pnlIncludeCheckedOut.Visible = true;

                        break;
                    case AppConstant.RegistrationType.ClusterPatient:
                    case AppConstant.RegistrationType.OutPatient:
                        tabStrip.Tabs[2].Visible = true; //pgAppointment
                        tabStrip.Tabs[6].Visible = (Helper.IsLokadokIntegration); //pgAppointmentLokadok

                        grdRegisteredList.Columns[3].Visible = true; // que
                        grdRegisteredList.Columns[14].Visible = true; // consul
                        grdRegisteredList.Columns[15].Visible = true; // confirm attandent
                        grdRegisteredList.Columns[17].Visible = (AppSession.Parameter.IsGuarantorInRegEditable);
                        grdRegisteredList.Columns[18].Visible = false;
                        grdRegisteredList.Columns[19].Visible = false;
                        grdRegisteredList.Columns[20].Visible = (AppSession.Parameter.IsPhycisianInRegEditable);
                        grdRegisteredList.Columns[25].Visible = true; // refer to other unit

                        pnlConfirmedAttendance.Visible = true;

                        break;
                    case AppConstant.RegistrationType.Ancillary:
                        tabStrip.Tabs[2].Visible = true; //pgAppointment
                        tabStrip.Tabs[6].Visible = false; //pgAppointmentLokadok

                        grdRegisteredList.Columns[3].Visible = true; // que
                        grdRegisteredList.Columns[17].Visible = (AppSession.Parameter.IsGuarantorInRegEditable);
                        grdRegisteredList.Columns[18].Visible = false;
                        grdRegisteredList.Columns[19].Visible = false;
                        grdRegisteredList.Columns[20].Visible = (AppSession.Parameter.IsPhycisianInRegEditable);
                        grdRegisteredList.Columns[21].Visible = false; // Substitute Physician
                        grdRegisteredList.Columns[23].Visible = false;// transfer to ipr
                        grdRegisteredList.Columns[24].Visible = false;// transfer to emr

                        break;
                    case AppConstant.RegistrationType.EmergencyPatient:
                        grdRegisteredList.Columns[17].Visible = (AppSession.Parameter.IsGuarantorInRegEditable);
                        grdRegisteredList.Columns[18].Visible = false;
                        grdRegisteredList.Columns[19].Visible = false;
                        grdRegisteredList.Columns[20].Visible = (AppSession.Parameter.IsPhycisianInRegEditable);
                        grdRegisteredList.Columns[21].Visible = false; // Substitute Physician
                        grdRegisteredList.Columns[24].Visible = false;// transfer to emr
                        grdRegisteredList.Columns[25].Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSMM"; // refer to other unit

                        break;
                    case AppConstant.RegistrationType.MedicalCheckUp:
                        tabStrip.Tabs[2].Visible = true; //pgAppointment

                        grdRegisteredList.Columns[3].Visible = false;

                        grdRegisteredList.Columns[17].Visible = (AppSession.Parameter.IsGuarantorInRegEditable);
                        grdRegisteredList.Columns[18].Visible = false;
                        grdRegisteredList.Columns[19].Visible = false;
                        grdRegisteredList.Columns[20].Visible = (AppSession.Parameter.IsPhycisianInRegEditable);
                        grdRegisteredList.Columns[21].Visible = false; // Substitute Physician
                        grdRegisteredList.Columns[23].Visible = false; // transfer to inpatient
                        grdRegisteredList.Columns[24].Visible = false; // transfer to emr
                        grdRegisteredList.Columns[grdRegisteredList.Columns.Count - 1].Visible = false;

                        break;
                }
                // delete
                grdRegisteredList.Columns[22].Visible = this.IsUserVoidAble;
                grdRegisteredList.Columns[grdRegisteredList.Columns.Count - 1].Visible = AppSession.Parameter.IsRegistrationLinkToPatientDocument; // patient document

                if (AppSession.Parameter.IsRegistrationListWithCreatedDateTime)
                    grdRegisteredList.Columns.FindByUniqueName("RegistrationDate").HeaderText = "Created Reg. Date";


                ComboBox.PopulateWithServiceUnit(cboServiceUnitID, AppConstant.RegistrationType.OutPatient, false, BuildingID);
                //ComboBox.PopulateWithServiceUnit(cboFilterServiceUnitID, regType, false);
                ComboBox.PopulateWithServiceUnit(cboFilterServiceUnitID, regType, regType == "ANC", BuildingID);

                // request custom rsui
                pnlRSUI.Visible = (AppSession.Parameter.HealthcareInitialAppsVersion == "RSUI" || AppSession.Parameter.HealthcareInitialAppsVersion == "RSPM");

                //var unit = new ServiceUnitCollection();
                //unit.Query.Where(unit.Query.IsActive == true, unit.Query.ServiceUnitIDBPJS != string.Empty);
                //unit.Query.Load();
                //cboServiceUnitBPJS.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                //foreach (var entity in unit)
                //{
                //    cboServiceUnitBPJS.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitIDBPJS));
                //}

                txtSEPDate.SelectedDate = DateTime.Now.Date;

                tabStrip.Tabs[4].Visible = Helper.IsBpjsIntegration; //pgBPJS

                txtDateApptLokadok.SelectedDate = DateTime.Now.Date;

                cboConfirmedAttendanceStatus.Items.Add(new RadComboBoxItem("-All-", ""));
                cboConfirmedAttendanceStatus.Items.Add(new RadComboBoxItem("Confirmed", "1"));
                cboConfirmedAttendanceStatus.Items.Add(new RadComboBoxItem("Not Confirm", "0"));

                if (this.IsUserAddAble == false || this.IsUserEditAble == false)
                {
                    btnNewPatient.Visible = this.IsUserAddAble;
                }

                tabStrip.Tabs[1].Visible = AppSession.Parameter.IsUsingGoogleForm; //Google Form
            }

            RegistrationType = regType;
            BuildingID = string.IsNullOrEmpty(Request.QueryString["gd"]) ? string.Empty : Request.QueryString["gd"];
        }

        protected void grdTransaction_Init(object sender, EventArgs e)
        {
            InitializeCultureGrid((RadGrid)sender);
        }

        protected void grdPatientDocument_Init(object sender, EventArgs e)
        {
            InitializeCultureGrid((RadGrid)sender);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtAppointmentDate.SelectedDate = DateTime.Today;
                txtReservationDate.SelectedDate = DateTime.Today;
                txtGFDate.SelectedDate = DateTime.Today;

                //set filter date to now date
                if (pnlFilterDate.Visible && RegistrationType != AppConstant.RegistrationType.InPatient)
                {
                    txtFromDate.SelectedDate = DateTime.Now;
                    txtToDate.SelectedDate = DateTime.Now;
                }

                StandardReference.InitializeIncludeSpace(cboAppointmentStatus, AppEnum.StandardReference.AppointmentStatus);
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            lblInfo.Text = string.Empty;
            pnlInfo.Visible = false;

            if (!(source is RadGrid)) return;
            var grd = (RadGrid)source;
            switch (grd.ID.ToLower())
            {
                case "grdpatient":
                    {
                        var args = eventArgument.Split(':');
                        switch (args[0])
                        {
                            case "rebind":
                                {
                                    break;
                                }
                            case "resend":
                                {
                                    SendToLokadok(args[1]);
                                    break;
                                }
                            case "confirmed":
                                {
                                    var patientID = args[1];
                                    var pat = new Patient();
                                    if (pat.LoadByPrimaryKey(patientID))
                                    {
                                        pat.IsKYC = true;
                                        pat.Save();
                                        grdPatient.Rebind();
                                    }
                                    break;
                                }
                        }
                        grdPatient.Rebind();
                        break;
                    }
                case "grdappointment":
                    {
                        switch (eventArgument)
                        {
                            case "rebind":
                                grdAppointment.Rebind();
                                grdRegisteredList.Rebind();
                                break;
                            case "generate":
                                GenerateRegistration();
                                grdAppointment.Rebind();
                                grdRegisteredList.Rebind();
                                break;
                        }

                        break;
                    }
                case "grdregisteredlist":
                    {
                        var command = eventArgument.Split(':');
                        switch (command[0])
                        {
                            case "rebind":
                                {
                                    if (command.Length > 1)
                                    {
                                        if (!string.IsNullOrEmpty(command[1]))
                                        {
                                            try
                                            {
                                                var pars = command[1].Split('|');
                                                // update que
                                                int newQueNo = int.Parse(pars[1]);
                                                (new RegistrationCollection()).
                                                    RegistrationUpdateQue(pars[0], newQueNo);
                                            }
                                            catch (Exception e)
                                            {
                                                // data not found
                                                lblInfo.Text = e.Message;
                                                pnlInfo.Visible = true;
                                            }
                                        }
                                    }
                                    grdRegisteredList.Rebind();
                                    break;
                                }
                            case "void":
                                {
                                    // Proses untuk Void Saja, Unvoid gak boleh
                                    var pars = eventArgument.Split('|');
                                    var registrationNo = pars[0].Split(':')[1];
                                    VoidRegistration(registrationNo);
                                    grdRegisteredList.Rebind();
                                    break;
                                }
                            case "resend":
                                {
                                    var reg = new Registration();
                                    if (reg.LoadByPrimaryKey(eventArgument.Split(':')[1]))
                                    {
                                        reg.IsGenerateHL7 = false;
                                        reg.Save();
                                    }
                                    grdRegisteredList.Rebind();
                                    break;
                                }
                        }

                        break;
                    }
                case "gridapptlokadok":
                    {
                        gridApptLokadok.Rebind();
                        break;
                    }
            }
        }

        private CostCalculationCollection GetCostCalculations(string transNo, string regNo, string serviceUnitId)
        {
            var coll = new CostCalculationCollection();

            var header = new TransChargesQuery("a");
            var detail = new TransChargesItemQuery("b");
            var cost = new CostCalculationQuery("c");

            cost.InnerJoin(detail).On
                (
                    cost.TransactionNo == detail.TransactionNo &
                    cost.SequenceNo == detail.SequenceNo
                );
            cost.InnerJoin(header).On(detail.TransactionNo == header.TransactionNo);
            cost.Where
                (
                    header.TransactionNo == transNo,
                    header.RegistrationNo == regNo,
                    header.ToServiceUnitID == serviceUnitId,
                    header.IsAutoBillTransaction == true,
                    header.IsVoid == false,
                    detail.IsVoid == false
                );

            coll.Load(cost);

            return coll;

        }

        private void VoidRegistration(string registrationNo)
        {
            var allowVoid = true;

            //charges
            var charges = new TransChargesCollection();
            charges.Query.Where
                (
                    charges.Query.RegistrationNo == registrationNo,
                    charges.Query.IsApproved == true
                );
            charges.LoadAll();

            if (charges.Count > 0)
            {
                var autobill = charges;
                autobill.Query.Where(autobill.Query.IsAutoBillTransaction == true);
                autobill.LoadAll();

                allowVoid = (charges.Count == autobill.Count);
            }

            if (allowVoid)
            {
                //prescription
                var prescription = new TransPrescriptionCollection();
                prescription.Query.Where
                    (
                        prescription.Query.RegistrationNo == registrationNo,
                        prescription.Query.IsApproval == true
                    );
                prescription.LoadAll();

                allowVoid = prescription.Count <= 0;
            }

            if (allowVoid)
            {
                //payment
                var payment = new TransPaymentCollection();
                payment.Query.Where
                    (
                        payment.Query.RegistrationNo == registrationNo,
                        payment.Query.IsApproved == true
                    );
                payment.LoadAll();

                allowVoid = payment.Count <= 0;
            }

            if (allowVoid)
            {
                using (var trans = new esTransactionScope())
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(registrationNo);

                    Helper.RegistrationOpenClose.SetVoid(reg, "Registration");

                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(reg.ServiceUnitID);

                    // kl udh sampai sini pasti transactionnya autobill
                    var tcic = new TransChargesItemCompCollection();
                    tcic.Query.Where(tcic.Query.TransactionNo == charges.Query.TransactionNo);
                    tcic.LoadAll();

                    foreach (var tc in charges)
                    {
                        /* Automatic Journal Testing Start */

                        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                        {
                            if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                            {
                                var serverDate = (new DateTime()).NowAtSqlServer();
                                JournalTransactions.AddNewPatientIncomeAccrualUnapproval(tc.TransactionNo, serverDate.Date, AppSession.UserLogin.UserID, 0);
                            }
                            else {
                                var cost = GetCostCalculations(tc.TransactionNo, reg.RegistrationNo, tc.ToServiceUnitID);

                                var journalId = JournalTransactions.AddNewIncomeCorrectionJournal(tc, tcic, reg, unit, cost, "SU", AppSession.UserLogin.UserID, true, 0);
                            }
                        }
                        /* Automatic Journal Testing End */
                    }

                    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    feeColl.Query.Where(feeColl.Query.RegistrationNo == reg.RegistrationNo);
                    if (feeColl.LoadAll())
                    {
                        feeColl.MarkAllAsDeleted();

                        feeColl.Save();
                    }

                    trans.Complete();
                }
            }
            else
            {
                lblInfo.Text = "The registration is unavailable to void";
                pnlInfo.Visible = true;
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        #region Patient Detail

        private DataTable Patients
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtPatientSearch.Text) && txtDateOfBirth.IsEmpty && string.IsNullOrEmpty(txtPhoneNo.Text) && string.IsNullOrEmpty(txtAddress.Text) && string.IsNullOrEmpty(txtSsn.Text) && string.IsNullOrEmpty(txtGuarantorCardNo.Text);
                if (Helper.IsDukcapilIntegration) isEmptyFilter = string.IsNullOrEmpty(txtPatientSearch.Text) && txtDateOfBirth.IsEmpty && string.IsNullOrEmpty(txtPhoneNo.Text) && string.IsNullOrEmpty(txtAddress.Text) && string.IsNullOrWhiteSpace(txtDisdukcapil.Text) && string.IsNullOrEmpty(txtGuarantorCardNo.Text);

                if (!ValidateSearch(isEmptyFilter, "Patient")) return null;

                var search = txtPatientSearch.Text.Trim();
                //comment jgn dihapus...sedang dikerjakan by lili
                var dtbPatient = (new PatientCollection()).PatientRegisterAble(Helper.EscapeQuery(search),
                    txtDateOfBirth.IsEmpty ? string.Empty : txtDateOfBirth.SelectedDate.Value.ToShortDateString(),
                    Helper.EscapeQuery(txtPhoneNo.Text), Helper.EscapeQuery(txtAddress.Text), IsValidateByZipCode, RegistrationType, 
                    Helper.IsDukcapilIntegration ? txtDisdukcapil.Text : string.Empty, Helper.IsDukcapilIntegration ? string.Empty: Helper.EscapeQuery(txtSsn.Text), Helper.EscapeQuery(txtGuarantorCardNo.Text));

                return dtbPatient;
            }
        }

        protected void btnSearchPatient_Click(object sender, ImageClickEventArgs e)
        {
            grdPatient.CurrentPageIndex = 0;
            grdPatient.Rebind();
        }

        protected void grdPatient_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = Patients;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }

        }

        protected void grdPatient_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string patientId = dataItem.GetDataKeyValue("PatientID").ToString();

            if (e.DetailTableView.Name.Equals("grdDetail"))
            {
                var query = new PatientQuery("a");
                query.Select
                    (
                        query.PatientID,
                        query.Notes
                    );

                query.Where(query.PatientID == patientId);

                e.DetailTableView.DataSource = query.LoadDataTable();
            }
            else
            {
                var query = new PatientBlackListHistoryQuery("a");
                var usr = new AppUserQuery("b");
                query.InnerJoin(usr).On(query.LastUpdateByUserID == usr.UserID);
                query.Select
                    (
                        query.PatientID,
                        query.IsBlackList,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID,
                        usr.UserName,
                        query.Notes
                    );

                query.Where(query.PatientID == patientId);
                query.OrderBy(query.LastUpdateDateTime.Descending);

                e.DetailTableView.DataSource = query.LoadDataTable();
            }
        }
        #endregion

        #region Registration

        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboParamedicID.SelectedValue)
                                    && txtFromDate.IsEmpty
                                    && txtToDate.IsEmpty
                                    && string.IsNullOrEmpty(cboFilterServiceUnitID.SelectedValue)
                                    && string.IsNullOrEmpty(txtSearchRegPatient.Text)
                                    &&
                                    (string.IsNullOrEmpty(txtFromRegistrationTime.Text) ||
                                     txtFromRegistrationTime.Text == "00:00")
                                    &&
                                    (string.IsNullOrEmpty(txtToRegistrationTime.Text) ||
                                     txtToRegistrationTime.Text == "00:00");
                if (!ValidateSearch(isEmptyFilter, "Registration"))
                    return null;


                var qr = new RegistrationQuery("r");

                var qp = new PatientQuery("p");
                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);

                var qm = new ParamedicQuery("m");
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);

                var unit = new ServiceUnitQuery("s");
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);

                var room = new ServiceRoomQuery("d");
                qr.LeftJoin(room).On(qr.RoomID == room.RoomID);

                var cq = new ClassQuery("e");
                qr.LeftJoin(cq).On(qr.ClassID == cq.ClassID);

                var gr = new GuarantorQuery("f");
                qr.InnerJoin(gr).On(qr.GuarantorID == gr.GuarantorID);

                var iq = new RegistrationInfoSumaryQuery("i");
                qr.LeftJoin(iq).On(qr.RegistrationNo == iq.RegistrationNo);

                var pi = new PatientInfoSumaryQuery("pi");
                qr.LeftJoin(pi).On(qr.PatientID == pi.PatientID);

                var gdc = new GuarantorDocumentChecklistQuery("gdc");
                qr.LeftJoin(gdc).On(qr.GuarantorID == gdc.GuarantorID & qr.SRRegistrationType == gdc.SRRegistrationType);

                var dc = new AppStandardReferenceItemQuery("dc");
                qr.LeftJoin(dc).On(dc.StandardReferenceID == "DocumentChecklist" & gdc.SRDocumentChecklist == dc.ItemID);

                qr.Where
                    (
                    qr.SRRegistrationType == (RegistrationType == "ANC" ? AppConstant.RegistrationType.OutPatient : RegistrationType),
                        qr.IsFromDispensary == false,
                        qr.IsVoid == false,
                        qr.IsClosed == false,
                        qr.IsNonPatient == false
                    );

                if (RegistrationType == "ANC")
                {
                    var usr = new AppUserServiceUnitQuery("usr");
                    qr.InnerJoin(usr).On(qr.ServiceUnitID == usr.ServiceUnitID &&
                                         usr.UserID == AppSession.UserLogin.UserID);
                }

                if (txtSearchRegPatient.Text != string.Empty)
                {
                    var searchPatient = Helper.EscapeQuery(txtSearchRegPatient.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchPatient.Replace("-", "").Reverse());

                    searchPatient = string.Format("%{0}%", searchPatient);

                    qr.Where(
                        qr.Or(
                            qp.ReverseMedicalNo.Like(reverseMedNoSearch),
                            qp.ReverseOldMedicalNo.Like(reverseMedNoSearch),
                            qp.FullName.Like(searchPatient)
                            )
                        );

                    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                    //    qr.Where
                    //        (
                    //            string.Format(@"<p.MedicalNo LIKE '%{0}%'
                    //                            OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%' 
                    //                            OR p.OldMedicalNo LIKE '%{0}%' 
                    //                            OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%' 
                    //                            or p.EmployeeNo LIKE '%{0}%'
                    //                            OR LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '%{0}%'>", searchPatient)
                    //        );
                    //else
                    //    qr.Where
                    //        (
                    //            string.Format(@"<p.MedicalNo LIKE '%{0}%'
                    //                            OR p.OldMedicalNo LIKE '%{0}%' 
                    //                            OR p.EmployeeNo LIKE '%{0}%'
                    //                            OR LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '%{0}%'>", searchPatient)
                    //        );
                }

                if (txtRegistrationNo.Text != string.Empty)
                    qr.Where(qr.RegistrationNo == txtRegistrationNo.Text);

                if (cboParamedicID.SelectedValue != string.Empty)
                    qr.Where(qr.ParamedicID == cboParamedicID.SelectedValue);

                if (cboFilterServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboFilterServiceUnitID.SelectedValue);

                if (cboRegGuarantor.SelectedValue != string.Empty)
                    qr.Where(qr.GuarantorID == cboRegGuarantor.SelectedValue);

                switch (RegistrationType)
                {
                    case AppConstant.RegistrationType.InPatient:
                        if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
                        {
                            qr.Where(qr.RegistrationDate >= txtFromDate.SelectedDate, qr.RegistrationDate  < txtToDate.SelectedDate.Value.AddDays(1));

                            if (txtFromDate.SelectedDate == txtToDate.SelectedDate && (txtFromRegistrationTime.Text != "0000" || txtToRegistrationTime.Text != "0000"))
                                qr.Where(
                                    qr.RegistrationTime.Between(
                                        txtFromRegistrationTime.Text.Substring(0, 2) + ":" +
                                        txtFromRegistrationTime.Text.Substring(2, 2),
                                        txtToRegistrationTime.Text.Substring(0, 2) + ":" +
                                        txtToRegistrationTime.Text.Substring(2, 2)));
                        }

                        if (!chkIncludeCheckedOut.Checked)
                            qr.Where(qr.DischargeDate.IsNull());

                        break;
                    case AppConstant.RegistrationType.OutPatient:
                    case AppConstant.RegistrationType.ClusterPatient:
                    case AppConstant.RegistrationType.EmergencyPatient:
                    case AppConstant.RegistrationType.MedicalCheckUp:
                    case AppConstant.RegistrationType.Ancillary:
                        if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
                        {
                            qr.Where(qr.RegistrationDate >= txtFromDate.SelectedDate, qr.RegistrationDate < txtToDate.SelectedDate.Value.AddDays(1));

                            if (txtFromDate.SelectedDate == txtToDate.SelectedDate && (txtFromRegistrationTime.Text != "0000" || txtToRegistrationTime.Text != "0000"))
                                qr.Where(
                                    qr.RegistrationTime.Between(
                                        txtFromRegistrationTime.Text.Substring(0, 2) + ":" +
                                        txtFromRegistrationTime.Text.Substring(2, 2),
                                        txtToRegistrationTime.Text.Substring(0, 2) + ":" +
                                        txtToRegistrationTime.Text.Substring(2, 2)));
                        }
                        if (RegistrationType == AppConstant.RegistrationType.OutPatient)
                        {
                            if (!string.IsNullOrEmpty(cboConfirmedAttendanceStatus.SelectedValue))
                            {
                                if (cboConfirmedAttendanceStatus.SelectedValue == "1")
                                    qr.Where(qr.IsConfirmedAttendance.IsNotNull(), qr.IsConfirmedAttendance == true);
                                else if (cboConfirmedAttendanceStatus.SelectedValue == "0")
                                    qr.Where(qr.Or(qr.IsConfirmedAttendance.IsNull(), qr.IsConfirmedAttendance == false));
                            }
                            if (!string.IsNullOrEmpty(BuildingID))
                            {
                                qr.Where(qr.Or(unit.SRBuilding.IsNull(), unit.SRBuilding == string.Empty, unit.SRBuilding == BuildingID));
                            }
                        }

                        break;
                }

                var apsr = new AppStandardReferenceItemQuery("apsr");
                qr.LeftJoin(apsr).On(apsr.StandardReferenceID == "Salutation" && apsr.ItemID == qp.SRSalutation);

                if (AppSession.Parameter.IsRegistrationListWithCreatedDateTime)
                {
                    qr.Select(
                        "<CASE CAST(r.LastCreateDateTime as DATE) WHEN CAST(r.RegistrationDate as DATE) THEN CAST(r.LastCreateDateTime as DATE) ELSE CAST(r.RegistrationDate as DATE) END RegistrationDate>",
                        "<CASE CAST(r.LastCreateDateTime as DATE) WHEN CAST(r.RegistrationDate as DATE) THEN CONVERT(VARCHAR(5), cast(r.LastCreateDateTime as time)) ELSE r.RegistrationTime END RegistrationTime>"
                        );
                }
                else
                {
                    qr.Select(qr.RegistrationDate, qr.RegistrationTime);
                }

                if (RegistrationType == "OPR")
                {
                    var appt = new AppointmentQuery("appt");
                    qr.LeftJoin(appt).On(appt.AppointmentNo == qr.AppointmentNo);
                    var appttype = new AppStandardReferenceItemQuery("appttype");
                    qr.LeftJoin(appttype).On(appttype.StandardReferenceID == "AppoinmentType" && appttype.ItemID == appt.SRAppoinmentType);
                    qr.Select(@"<ISNULL(appttype.ItemName, '') AS AppoinmentTypeName>");

                    var apptQue = new AppointmentQueueingQuery("apptQue");
                    qr.LeftJoin(apptQue).On(appt.AppointmentNo == apptQue.AppointmentNo && apptQue.SRQueueingGroup == "01");
                    qr.Select(apptQue.FormattedNo);
                }
                else
                {
                    qr.Select(@"<'' AS AppoinmentTypeName>");
                    qr.Select(@"<'' AS FormattedNo>");
                }

                qr.Select
                    (
                        qr.PatientID,

                        qr.RegistrationNo,
                        qr.IsClosed,
                        qr.IsVoid,
                        qr.IsFromDispensary,
                        qp.MedicalNo,
                        qp.PatientName,
                        apsr.ItemName.As("SalutationName"),
                        qp.Sex,
                        qm.ParamedicName,
                        qr.ServiceUnitID,
                        unit.ServiceUnitName,
                        room.RoomName,
                        cq.ClassName,
                        qr.BedID,
                        gr.GuarantorName,
                        qr.IsConsul,
                        qr.SRRegistrationType,
                        qr.IsNewPatient,
                        qr.LastCreateUserID,
                        @"<COALESCE(i.NoteCount,0) +  COALESCE(pi.NoteCount,0) as NoteCount>",
                        @"<CASE WHEN dc.LineNumber IS NULL OR (dc.LineNumber - i.DocumentCheckListCount) <= 0 THEN NULL ELSE (dc.LineNumber - i.DocumentCheckListCount) END AS DocumentCheckListCountRemains>",
                        qr.RegistrationQue,
                        "<CASE WHEN r.DischargeDate IS NOT NULL AND r.SRRegistrationType = 'IPR' THEN 1 ELSE 0 END IsDischarged>",
                        "<ISNULL(r.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                        "<CASE WHEN r.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>"
                    );
                if (AppSession.Parameter.IsCrmMembershipActive)
                    qr.Select(@"<CASE WHEN ISNULL(r.MembershipNo, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsVipMember'>");
                else
                    qr.Select(@"<CAST(0 AS BIT) AS 'IsVipMember'>");

                qr.OrderBy(qm.ParamedicName.Ascending, qr.RegistrationDate.Ascending,
                        qr.RegistrationQue.Ascending, qr.RegistrationTime.Ascending, qr.RegistrationNo.Ascending);

                var dtb = qr.LoadDataTable();

                return dtb;
            }
        }

        protected void grdRegisteredList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = Registrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        protected void grdRegisteredList_ItemCreated(object sender, GridItemEventArgs e)
        {
            //var gridNestedViewItem = e.Item as GridNestedViewItem;
            //if (gridNestedViewItem != null)
            //{
            //    e.Item.FindControl("InnerContainer").Visible = (gridNestedViewItem).ParentItem.Expanded;
            //}

            var gridNestedViewItem = e.Item as GridNestedViewItem;
            if (gridNestedViewItem != null)
            {
                e.Item.FindControl("pnlPhr").Visible = (gridNestedViewItem).ParentItem.Expanded;
            }
        }

        protected void grdRegisteredList_PreRender(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            //    grdList.MasterTableView.Items[0].Expanded = true;
            //    grdList.MasterTableView.Items[0].ChildItem.FindControl("InnerContainer").Visible = true;
            //}
        }

        protected void grdRegisteredList_ItemCommand(object source, GridCommandEventArgs e)
        {
            //if (e.CommandName == RadGrid.ExpandCollapseCommandName && e.Item is GridDataItem)
            //{
            //    ((GridDataItem)e.Item).ChildItem.FindControl("InnerContainer").Visible = !e.Item.Expanded;
            //}

            var isVisible = false;
            if (e.CommandName == RadGrid.ExpandCollapseCommandName && e.Item is GridDataItem)
            {
                isVisible = !e.Item.Expanded;
                ((GridDataItem)e.Item).ChildItem.FindControl("pnlPhr").Visible = isVisible;

                _serviceUnitId = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ServiceUnitID"]);
                _patientId = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PatientID"]);
                _registrationNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RegistrationNo"]);
            }

            if (isVisible)
            {
                var childItem = ((GridDataItem)e.Item).ChildItem;

                var grd = (RadGrid)childItem.FindControl("grdPhr");
                var tbarPhr = (RadToolBar)childItem.FindControl("tbarPhr");

                PopulatePhr(grd, tbarPhr, _serviceUnitId, _patientId, _registrationNo, grd.ClientID);

                _serviceUnitId = string.Empty;
                _patientId = string.Empty;
                _registrationNo = string.Empty;
            }
        }

        #region Grid Phr
        protected void grdPhr_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "RebindGrid")
            {
                var grd = (RadGrid)sender;
                var tbarPhr = (RadToolBar)grd.Parent.FindControl("tbarPhr");

                var serviceUnitID = hdnServiceUnitID.Value;
                var patientID = hdnPatientID.Value;
                var registrationNo = hdnRegistrationNo.Value;


                PopulatePhr(grd, tbarPhr, serviceUnitID, patientID, registrationNo, grd.ClientID);
            }
        }

        private void PopulatePhr(RadGrid grd, RadToolBar tbarPhr, string serviceUnitID, string patientID, string registrationNo,
           string grdPhrClientId)
        {
            var isRecordAddAble = true;
            //var deadlineAddable = AppParameter.GetParameterValue(AppParameter.ParameterItem.DeadlineMedicalRecordAddableAfterDischarge).ToInt();
            var isRecordEditble = true;
            //var deadlineEditable = AppParameter.GetParameterValue(AppParameter.ParameterItem.DeadlineMedicalRecordEditableAfterDischarge).ToInt();

            //if (deadlineAddable > 0 || deadlineEditable > 0)
            //{
            //    var reg = new Registration();
            //    reg.LoadByPrimaryKey(registrationNo);

            //    if (!IsMedicalRecordOpen(deadlineAddable, reg))
            //    {
            //        var par = AppParameter.GetParameter(AppParameter.ParameterItem.DeadlineMedicalRecordAddableAfterDischarge);
            //        var msg = string.Format(par.Message, par.ParameterValue);

            //        var litPhrMessage = (Literal)grd.Parent.FindControl("litPhrMessage");
            //        litPhrMessage.Visible = true;
            //        litPhrMessage.Text = string.Format("<div style=\"color:yellow;width:100%;padding:4px 4px 4px 4px;\"><img src=\"../../../Images/boundleft.gif\"/>&nbsp;{0}</div>", msg);
            //        isRecordAddAble = false;
            //    }

            //    if (!IsMedicalRecordOpen(deadlineEditable, reg))
            //    {
            //        var par = AppParameter.GetParameter(AppParameter.ParameterItem.DeadlineMedicalRecordEditableAfterDischarge);
            //        var msg = string.Format(par.Message, par.ParameterValue);

            //        var litPhrMessage = (Literal)grd.Parent.FindControl("litPhrMessage");
            //        litPhrMessage.Visible = true;

            //        if (!string.IsNullOrWhiteSpace(litPhrMessage.Text))
            //        {
            //            var editMsg = string.Format("<div style=\"color:yellow;width:100%;padding:0px 4px 4px 4px;\"><img src=\"../../../Images/boundleft.gif\"/>&nbsp;{0}</div>", msg);
            //            litPhrMessage.Text = string.Concat(litPhrMessage.Text, editMsg);
            //        }
            //        else
            //            litPhrMessage.Text = string.Format("<div style=\"color:yellow;width:100%;padding:4px 4px 4px 4px;\"><img src=\"../../../Images/boundleft.gif\"/>&nbsp;{0}</div>", msg);

            //        isRecordEditble = false;
            //    }
            //}

            grd.InitializeCultureGrid();

            PopulatePhrMenuAdd(tbarPhr, serviceUnitID, patientID, registrationNo, grdPhrClientId, isRecordAddAble);

            var tbarItemRefresh = (RadToolBarButton)tbarPhr.Items[1];
            tbarItemRefresh.Value = string.Concat("refresh_", grdPhrClientId);

            grd.DataSource = PatientHeathRecordDataTable(registrationNo, grdPhrClientId, isRecordEditble);
            grd.Rebind();
        }

        private DataTable QuestionFormDatatable(string serviceUnitID, string patientID, string registrationNo)
        {
            var query = new QuestionFormQuery("a");
            var suQr = new QuestionFormInServiceUnitQuery("s");

            query.InnerJoin(suQr).On(query.QuestionFormID == suQr.QuestionFormID && suQr.ServiceUnitID == serviceUnitID);
            query.Where(query.IsActive == true);

            // Berdasarkan Form Type
            //query.Where(query.SRQuestionFormType == QuestionForm.QuestionFormType.PatientLetter);

            // Berdasarkan tipe user
            query.Where(query.RestrictionUserType.Like("%REG%"));

            query.Select(string.Format("<'{0}' as registrationNo>", registrationNo),
                query.QuestionFormID,
                query.QuestionFormName,
                query.IsSingleEntry.Coalesce("0").As("IsSingleEntry"), @"<CAST(1 AS BIT) AS IsNewEnable>");

            query.OrderBy(query.QuestionFormName.Ascending);

            var dtb = query.LoadDataTable();
            foreach (DataRow row in dtb.Rows)
            {
                if (Convert.ToBoolean(row["IsSingleEntry"]))
                {
                    var phr = new PatientHealthRecordCollection();
                    phr.Query.Where(phr.Query.RegistrationNo == registrationNo,
                        phr.Query.QuestionFormID == row["QuestionFormID"].ToString());
                    phr.LoadAll();
                    row["IsNewEnable"] = phr.Count == 0;
                }
            }
            dtb.AcceptChanges();

            return dtb;
        }

        private void PopulatePhrMenuAdd(RadToolBar tbarPhr, string serviceUnitID, string patientID, string registrationNo, string grdPhrClientId, bool isRecordAddAble = true)
        {
            var tbarItemAdd = (RadToolBarDropDown)tbarPhr.Items[0];
            tbarItemAdd.Buttons.Clear();

            if (!IsUserAddAble || !isRecordAddAble)
            {
                tbarItemAdd.Enabled = false;
                return;
            }

            var dtbQuestionForm = QuestionFormDatatable(serviceUnitID, patientID, registrationNo);
            if (dtbQuestionForm.Rows.Count > 0)
            {
                tbarItemAdd.Enabled = true;
                foreach (DataRow row in dtbQuestionForm.Rows)
                {
                    var btn = new RadToolBarButton(row["QuestionFormName"].ToString())
                    {
                        Value = string.Format("addphr^{0}^{1}^{2}^{3}^{4}", row["QuestionFormID"], serviceUnitID, patientID, registrationNo, grdPhrClientId)
                    };
                    btn.Enabled = true.Equals(row["IsNewEnable"]);
                    tbarItemAdd.Buttons.Add(btn);
                }
            }
            else
                tbarItemAdd.Enabled = false;
        }

        private string PhrEditLink(DataRow row, string grdPhrClientId, bool isRecordEditble)
        {
            // entryPhr(md, id, fid,suId, patId, regNo,grdPhrClientId)
            var isEditAble = isRecordEditble && this.IsUserEditAble && (row["IsApproved"] == DBNull.Value || false.Equals(row["IsApproved"])) && (row["IsSharingEdit"] != DBNull.Value && true.Equals(row["IsSharingEdit"]) || AppSession.UserLogin.UserID.Equals(row["CreateByUserID"]));
            return string.Format(
                "<a href=\"#\" onclick=\"entryPhr('{6}', '{0}','{1}','{2}','{3}','{4}', '{5}'); return false;\"><img src=\"{8}/Images/Toolbar/{7}16.png\" border=\"0\" /></a>",
                row["TransactionNo"], row["QuestionFormID"], row["ServiceUnitID"], row["PatientID"], row["RegistrationNo"], grdPhrClientId,
                isEditAble ? "edit" : "view",
                isEditAble ? "edit" : "views", Helper.UrlRoot());
        }

        protected string PhrViewLink(DataRow row, string grdPhrClientId)
        {
            // entryPhr(md, id, fid,suId, patId, regNo,grdPhrClientId)
            var retval =
                string.Format(
                    "<a href=\"#\" onclick=\"entryPhr('view', '{0}','{1}','{2}','{3}','{4}','{5}'); return false;\">{0}</a>",
                    row["TransactionNo"], row["QuestionFormID"], row["ServiceUnitID"], row["PatientID"], row["RegistrationNo"], grdPhrClientId);
            return retval;
        }

        private DataTable PatientHeathRecordDataTable(string registrationNo, string grdPhrClientId, bool isRecordEditble)
        {
            // Display semua PHR krn untuk keperluan list history
            var query = new PatientHealthRecordQuery("phr");
            var form = new QuestionFormQuery("f");
            var userQr = new AppUserQuery("usr");
            var reg = new RegistrationQuery("x");
            var par = new ParamedicQuery("y");
            var unit = new ServiceUnitQuery("z");

            query.InnerJoin(form).On(query.QuestionFormID == form.QuestionFormID);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.LeftJoin(par).On(reg.ParamedicID == par.ParamedicID);
            query.LeftJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
            query.LeftJoin(userQr).On(query.CreateByUserID == userQr.UserID);

            query.Where(query.RegistrationNo == registrationNo);

            // Filter RestrictionUserType hanya u/ REG
            query.Where(form.RestrictionUserType.Like("%REG%"));

            //// hanya Form PatientLetter yg dimunculkan
            //query.Where(form.SRQuestionFormType == QuestionForm.QuestionFormType.PatientTransfer);

            query.Select(
                query.TransactionNo,
                reg.RegistrationNo,
                reg.PatientID,
                par.ParamedicName,
                unit.ServiceUnitName,
                unit.ServiceUnitID, // Untuk keperluan hak akses
                query.QuestionFormID,
                form.QuestionFormName,
                @"<CAST(CONVERT(VARCHAR(10), phr.RecordDate, 112) + ' ' + phr.RecordTime AS DATETIME) AS RecordDateTime>",
                userQr.UserName.As("CreatedByUserName"),
                query.IsComplete,
                query.ReferenceNo,
                form.RmNO,
                query.CreateByUserID,
                query.IsApproved,
                form.IsSharingEdit,
                form.RestrictionUserType,
                form.IsSingleEntry
                );

            var dtb = query.LoadDataTable();
            dtb.Columns.Add(new DataColumn("UrlEdit", typeof(string)));
            dtb.Columns.Add(new DataColumn("UrlView", typeof(string)));
            foreach (DataRow row in dtb.Rows)
            {
                row["UrlEdit"] = PhrEditLink(row, grdPhrClientId, isRecordEditble);
                row["UrlView"] = PhrViewLink(row, grdPhrClientId);
            }
            dtb.AcceptChanges();

            return dtb;
        }

        #endregion

       
        protected void btnSearchRegistration_Click(object sender, ImageClickEventArgs e)
        {
            grdRegisteredList.CurrentPageIndex = 0;
            grdRegisteredList.Rebind();

            lblInfo.Text = string.Empty;
            pnlInfo.Visible = false;
        }

        #endregion

        #region Appointment

        protected void btnSearchAppointment_Click(object sender, ImageClickEventArgs e)
        {
            grdAppointment.CurrentPageIndex = 0;
            grdAppointment.Rebind();
        }

        private DataTable AppointmentPendings
        {
            get
            {
                var isEmptyFilter = txtAppointmentDate.IsEmpty
                                    && string.IsNullOrEmpty(txtAppointmentSearch.Text)
                                    && string.IsNullOrEmpty(txtAppointmentNote.Text)
                                    && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue)
                                    && string.IsNullOrEmpty(cboAppointmentParamedicID.SelectedValue);

                if (!ValidateSearch(isEmptyFilter, "Pending Appointment"))
                    return null;


                var qr = new AppointmentQuery("r");
                var qm = new ParamedicQuery("m");
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                
                var qv = new VisitTypeQuery("v");
                qr.LeftJoin(qv).On(qr.VisitTypeID == qv.VisitTypeID);

                qr.Where
                    (
                        qr.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusClosed,
                        qr.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                    );

                if (!txtAppointmentDate.IsEmpty)
                    qr.Where(qr.AppointmentDate == txtAppointmentDate.SelectedDate.Value);
                if (!txtAppointmentSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchPatient = Helper.EscapeQuery(txtAppointmentSearch.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        qr.Where
                            (
                                string.Format(@"<p.MedicalNo = '{0}'
                                                OR REPLACE(p.MedicalNo, '-', '') = '{0}' 
                                                OR p.OldMedicalNo = '{0}' 
                                                OR REPLACE(p.OldMedicalNo, '-', '') = '{0}' 
                                                OR REPLACE(r.EmployeeNo, '-', '') = '{0}' 
                                                OR LTRIM(RTRIM(LTRIM(r.FirstName + ' ' + r.MiddleName)) + ' ' + r.LastName) LIKE '%{0}%'>", searchPatient)
                            );
                    else
                        qr.Where
                            (
                                string.Format(@"<p.MedicalNo = '{0}'
                                                OR p.OldMedicalNo = '{0}' 
                                                OR REPLACE(r.EmployeeNo, '-', '') = '{0}' 
                                                OR LTRIM(RTRIM(LTRIM(r.FirstName + ' ' + r.MiddleName)) + ' ' + r.LastName) LIKE '%{0}%'>", searchPatient)
                            );
                }

                if (!txtAppointmentNote.Text.Trim().Equals(string.Empty))
                {
                    string searchPatient = Helper.EscapeQuery(txtAppointmentNote.Text);
                    qr.Where
                        (
                            string.Format(@"<r.Notes LIKE '%{0}%'>", searchPatient)
                        );
                }

                var unit = new ServiceUnitQuery("a");
                qr.InnerJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);

                var std = new AppStandardReferenceItemQuery("x");
                qr.LeftJoin(std).On(qr.SRAppointmentStatus == std.ItemID && std.StandardReferenceID == AppEnum.StandardReference.AppointmentStatus.ToString());

                var appttype = new AppStandardReferenceItemQuery("y");
                qr.LeftJoin(appttype).On(qr.SRAppoinmentType == appttype.ItemID && appttype.StandardReferenceID == AppEnum.StandardReference.AppoinmentType.ToString());

                qr.Select
                    (
                        qr.PatientID,
                        qr.PatientName,
                        qr.AppointmentNo,
                        qm.ParamedicName,
                        qr.AppointmentTime,
                        qv.VisitTypeName,
                        qr.AppointmentDate,
                        qr.LastUpdateDateTime,
                        unit.ServiceUnitName,
                        qr.Notes,
                        @"<CASE WHEN p.StreetName <>'' THEN p.StreetName ELSE r.StreetName END AS Address>",
                        qr.LastCreateByUserID,
                        std.ItemName.As("Status"),
                        appttype.ItemName.As("AppoinmentTypeName")
                    );

                var qp = new PatientQuery("p");
                qr.LeftJoin(qp).On(qr.PatientID == qp.PatientID);
                qr.Select(qp.MedicalNo.Coalesce("''").As("MedicalNo"), 
                    @"<ISNULL(p.IsAlive, 1) AS IsAlive>",
                    @"<ISNULL(p.IsBlackList, 0) AS IsBlackList>",
                    @"<ISNULL(p.IsActive, 1) AS IsActive>");

                var qsal = new AppStandardReferenceItemQuery("sal");
                qr.LeftJoin(qsal).On(qsal.StandardReferenceID == "Salutation" && qsal.ItemID == qp.SRSalutation);
                qr.Select(qsal.ItemName.As("SalutationName"));

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboAppointmentParamedicID.SelectedValue))
                    qr.Where(qr.ParamedicID == cboAppointmentParamedicID.SelectedValue);
                if (cboAppointmentGuarantor.SelectedValue != string.Empty)
                    qr.Where(qr.GuarantorID == cboAppointmentGuarantor.SelectedValue);
                if (!string.IsNullOrEmpty(BuildingID))
                {
                    qr.Where(qr.Or(unit.SRBuilding.IsNull(), unit.SRBuilding == string.Empty, unit.SRBuilding == BuildingID));
                }
                if (!string.IsNullOrEmpty(cboAppointmentStatus.SelectedValue))
                {
                    qr.Where(qr.SRAppointmentStatus == cboAppointmentStatus.SelectedValue);
                }
                if (!string.IsNullOrEmpty(cboCallerGroup.SelectedValue))
                {
                    qr.Where(qr.LastCreateByUserID == cboCallerGroup.SelectedValue);
                }

                qr.OrderBy(qr.AppointmentDate.Ascending, qr.AppointmentTime.Ascending);

                var apptQue = new AppointmentQueueingQuery("apptQue");
                qr.LeftJoin(apptQue).On(qr.AppointmentNo == apptQue.AppointmentNo && apptQue.SRQueueingGroup == "01");
                qr.Select(apptQue.FormattedNo);

                return qr.LoadDataTable();
            }
        }

        protected void grdAppointment_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (tabStrip.Tabs[2].Visible) //pgAppointment
            {
                var grd = (RadGrid)source;

                if (!IsPostBack && !IsListLoadRecordOnInit)
                {
                    grd.DataSource = new String[] { };
                    return;
                }

                var dataSource = AppointmentPendings;
                if (dataSource == null)
                    grd.DataSource = new String[] { }; // Clear rows
                else
                    grd.DataSource = dataSource;
            }
        }

        #endregion

        #region Reservation Detail

        protected void btnSearchReservation_Click(object sender, ImageClickEventArgs e)
        {
            grdReservation.CurrentPageIndex = 0;
            grdReservation.Rebind();
        }

        private DataTable Reservations
        {
            get
            {
                var isEmptyFilter = txtReservationDate.IsEmpty
                    && string.IsNullOrEmpty(txtReservationSearch.Text)
                    && string.IsNullOrEmpty(cboReservationServiceUnitSearch.SelectedValue);

                if (!ValidateSearch(isEmptyFilter, "Reservation"))
                    return null;

                var qa = new ReservationQuery("a");
                var qc = new ServiceUnitQuery("c");
                var qd = new ServiceRoomQuery("d");
                var qe = new ClassQuery("e");
                var qf = new RegistrationQuery("f");

                qa.InnerJoin(qc).On(qa.ServiceUnitID == qc.ServiceUnitID);
                qa.LeftJoin(qd).On(qa.RoomID == qd.RoomID);
                qa.LeftJoin(qe).On(qa.ClassID == qe.ClassID);
                qa.LeftJoin(qf).On(qa.ReservationNo == qf.AppointmentNo & qf.IsVoid == false);

                qa.Where
                    (
                        qa.SRReservationStatus != AppSession.Parameter.AppointmentStatusClosed,
                        qa.SRReservationStatus != AppSession.Parameter.AppointmentStatusCancel,
                        qf.RegistrationNo.IsNull()
                    );

                if (!txtReservationDate.IsEmpty)
                    qa.Where(qa.ReservationDate.Date() == txtReservationDate.SelectedDate.Value.Date);
                if (!txtReservationSearch.Text.Trim().Equals(string.Empty))
                {
                    var searchPatient = txtReservationSearch.Text;
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        qa.Where
                            (
                                string.Format(@"<b.MedicalNo = '{0}'
                                                OR REPLACE(b.MedicalNo, '-', '') = '{0}' 
                                                OR b.OldMedicalNo = '{0}' 
                                                OR REPLACE(b.OldMedicalNo, '-', '') = '{0}' 
                                                OR LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) LIKE '%{0}%'>", searchPatient)
                            );
                    else
                        qa.Where
                        (
                            string.Format(@"<b.MedicalNo = '{0}'
                                            OR b.OldMedicalNo = '{0}' 
                                            OR LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) LIKE '%{0}%'>", searchPatient)
                        );
                }

                qa.Select
                    (
                        qa.ReservationNo,
                        qa.ReservationDate,
                        qc.ServiceUnitName,
                        qd.RoomName,
                        qe.ClassName,
                        qa.BedID,
                        qa.Notes
                    );

                var qb = new PatientQuery("b");
                qa.LeftJoin(qb).On(qa.PatientID == qb.PatientID);
                qa.Select(qb.MedicalNo,
                          ((((qa.FirstName + " " + qa.MiddleName).LTrim()).RTrim() + " " + qa.LastName).LTrim()).As("PatientName"),
                          qb.StreetName.As("Address"));

                var qsal = new AppStandardReferenceItemQuery("sal");
                qa.LeftJoin(qsal).On(qsal.StandardReferenceID == "Salutation" & qsal.ItemID == qb.SRSalutation);
                qa.Select(qsal.ItemName.As("SalutationName"));

                if (cboReservationServiceUnitSearch.SelectedValue != string.Empty)
                    qa.Where(qa.ServiceUnitID == cboReservationServiceUnitSearch.SelectedValue);

                return qa.LoadDataTable();
            }
        }

        protected void grdReservation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (tabStrip.Tabs[3].Visible) //pgReservation
            {
                var grd = (RadGrid)source;

                if (!IsPostBack && !IsListLoadRecordOnInit)
                {
                    grd.DataSource = new String[] { };
                    return;
                }

                var dataSource = Reservations;
                if (dataSource == null)
                    grd.DataSource = new String[] { }; // Clear rows
                else
                    grd.DataSource = dataSource;
            }
        }

        #endregion Reservation Detail

        #region Appointment Lokadok

        protected void btnSearchApptLokadok_Click(object sender, ImageClickEventArgs e)
        {
            gridApptLokadok.Rebind();
        }

        protected void gridApptLokadok_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (tabStrip.Tabs[6].Visible) //pgAppointmentLokadok
            {
                if (!IsPostBack && !IsListLoadRecordOnInit) return;

                if (!txtDateApptLokadok.SelectedDate.HasValue) return;

                var apptColl = new AppointmentLokadokCollection();
                apptColl.LoadGridDataSource(txtDateApptLokadok.SelectedDate.Value, cboPhyApptLokadok.SelectedValue);
                gridApptLokadok.DataSource = apptColl;
            }
        }

        protected void gridApptLokadok_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = ((GridDataItem)e.Item);
                var chk = item["PatientNotFound"].Controls[0] as System.Web.UI.WebControls.CheckBox;
                if (chk.Checked)
                {
                    item.ForeColor = System.Drawing.Color.Red;
                }
                item.ToolTip = "Medical number not found!";
            }
        }

        protected void btnUpdateApptLokadok_Click(object sender, ImageClickEventArgs e)
        {
            var apptColl = new AppointmentLokadokCollection();

            if (!txtDateApptLokadok.SelectedDate.HasValue)
            {
                pnlInfoApptLoka.Visible = true;
                lblInfoApptLoka.Text = "Please select a valid date.";
                lblLokaDownloadInfo.Text = "";
                return;
            }

            int[] iCount;
            try
            {
                iCount = Common.Lokadok.Helper.UpdateFromLokadokTest1(apptColl, txtDateApptLokadok.SelectedDate.Value);
                pnlInfoApptLoka.Visible = false;

                //gridApptLokadok.DataSource = apptColl;
                //gridApptLokadok.DataBind();
                gridApptLokadok.Rebind();
                lblLokaDownloadInfo.Text = (iCount[0] == 0 ? "Data is up to date" :
                    string.Format("{0} appointment(s) have successfully been downloaded", iCount[0]));
            }
            catch (Exception ex)
            {
                LogError(ex);
                pnlInfoApptLoka.Visible = true;
                lblInfoApptLoka.Text = "Update failed.";
                lblLokaDownloadInfo.Text = "";
            }
        }

        #endregion

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var medic = new ParamedicQuery("a");
            medic.es.Top = 10;
            medic.Where(
                medic.ParamedicName.Like(searchTextContain),
                medic.IsAvailable == true,
                medic.IsActive == true
                );

            cboParamedicID.DataSource = medic.LoadDataTable();
            cboParamedicID.DataBind();
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboAppointmentParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var cbo = (RadComboBox)o;
            var medic = new ParamedicQuery("a");
            medic.es.Top = 10;
            medic.Where(
                medic.ParamedicName.Like(searchTextContain),
                medic.IsAvailable == true,
                medic.IsActive == true
                );

            cbo.DataSource = medic.LoadDataTable();
            cbo.DataBind();
        }

        protected void cboGuarantor_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void cboAppointmentGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var gua = (RadComboBox)o;
            var grr = new GuarantorQuery("a");
            grr.es.Top = 10;
            grr.Where(
                grr.GuarantorName.Like(searchTextContain),
                grr.IsActive == true
                );

            gua.DataSource = grr.LoadDataTable();
            gua.DataBind();
        }

        protected void grdPHR_ItemCommand(object source, GridCommandEventArgs e)
        {
            var pars = e.CommandArgument.ToString().Split('_');
            var jobParameters = new PrintJobParameterCollection();
            jobParameters.AddNew("p_PatientID", pars[0]);
            jobParameters.AddNew("p_QuestionFormID", pars[1]);

            AppSession.PrintJobParameters = jobParameters;

            switch (e.CommandName)
            {
                case "1":
                    AppSession.PrintJobReportID = AppConstant.Report.MedicalHistory;
                    break;
                case "2":
                    AppSession.PrintJobReportID = AppConstant.Report.PhysicalExam;
                    break;
                case "3":
                    AppSession.PrintJobReportID = AppConstant.Report.ExaminationSummary;
                    break;
                default:
                    AppSession.PrintJobReportID = AppConstant.Report.SystemicExam;
                    break;
            }


            string script = @"var oWnd = $find('" + winPrint.ClientID + @"');oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + @"');
                                oWnd.Show(); oWnd.Maximize();";

            radAjaxPanel.ResponseScripts.Add(script);

        }

        #region Generate Registration

        private void GenerateRegistration()
        {
            using (var trans = new esTransactionScope())
            {
                foreach (GridDataItem dataItem in grdAppointment.MasterTableView.Items)
                {
                    string appointmentNo = dataItem.GetDataKeyValue("AppointmentNo").ToString();
                    var app = new BusinessObject.Appointment();
                    if (app.LoadByPrimaryKey(appointmentNo))
                    {
                        if (string.IsNullOrEmpty(app.PatientID)) continue; /*jangan generate pasien baru*/

                        DateTime appointmentDateTime =
                            DateTime.Parse(app.AppointmentDate.Value.ToShortDateString() + " " + app.AppointmentTime);

                        if (appointmentDateTime < DateTime.Now) continue; /*jangan generate appointment yg sdh expired*/

                        var patient = new Patient();

                        #region Patient
                        if (!patient.LoadByPrimaryKey(app.PatientID)) continue;
                        if (string.IsNullOrEmpty(patient.MedicalNo)) continue;
                        if (patient.IsBlackList ?? false) continue;
                        if (!(patient.IsAlive ?? true)) continue;
                        if (!(patient.IsActive ?? true)) continue;

                        patient.NumberOfVisit++;
                        patient.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        patient.LastUpdateDateTime = DateTime.Now;
                        patient.Save();
                        #endregion

                        var reg = new Registration();
                        var que = new ServiceUnitQue();
                        var chargesHd = new TransCharges();
                        var billing = new MergeBilling();
                        var mrFileStatus = new MedicalRecordFileStatus();
                        var responsible = new RegistrationResponsiblePerson();
                        var emrContact = new EmergencyContact();
                        var patEmrContact = new PatientEmergencyContact();
                        var registrationInfoSumary = new RegistrationInfoSumary();
                        var PatientInfoSumary = new PatientInfoSumary();

                        #region Appointment
                        app.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusClosed;
                        app.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        app.LastUpdateDateTime = DateTime.Now;
                        app.Save();
                        #endregion

                        #region Registration
                        reg.AddNew();
                        reg.RegistrationNo = GetNewRegistrationNo();

                        _autoNumberReg.LastCompleteNumber = reg.RegistrationNo;
                        _autoNumberReg.Save();

                        reg.str.ParamedicID = app.ParamedicID;

                        var roomId = string.Empty;
                        var sup = new ServiceUnitParamedic();
                        if (sup.LoadByPrimaryKey(app.ServiceUnitID, app.ParamedicID))
                        {
                            if (!string.IsNullOrEmpty(sup.DefaultRoomID))
                                roomId = sup.DefaultRoomID;
                        }
                        if (string.IsNullOrEmpty(roomId))
                        {
                            var srQ = new ServiceRoomQuery();
                            srQ.Where(srQ.ServiceUnitID == app.ServiceUnitID, srQ.IsActive == true);
                            srQ.OrderBy(srQ.RoomID.Ascending);
                            srQ.es.Top = 1;
                            var sr = new ServiceRoom();
                            sr.Load(srQ);
                            if (sr != null)
                                roomId = sr.RoomID;
                        }

                        reg.str.RoomID = roomId;
                        reg.PhysicianSenders = string.Empty;
                        reg.GuarantorID = AppSession.Parameter.SelfGuarantor;
                        reg.PatientID = app.PatientID;
                        reg.ClassID = AppSession.Parameter.OutPatientClassID;
                        reg.ChargeClassID = AppSession.Parameter.OutPatientClassID;
                        reg.str.CoverageClassID = AppSession.Parameter.OutPatientClassID;
                        reg.VisiteRegistrationNo = string.Empty;
                        reg.SRRegistrationType = AppConstant.RegistrationType.OutPatient;
                        reg.RegistrationDate = app.AppointmentDate;
                        reg.RegistrationTime = app.AppointmentTime;
                        reg.AppointmentNo = app.AppointmentNo;

                        var ageInYear = Helper.GetAgeInYear(patient.DateOfBirth ?? reg.RegistrationDate.Value.Date).ToString();
                        var ageInMonth = Helper.GetAgeInMonth(patient.DateOfBirth ?? reg.RegistrationDate.Value.Date).ToString();
                        var ageInDay = Helper.GetAgeInDay(patient.DateOfBirth ?? reg.RegistrationDate.Value.Date).ToString();

                        reg.AgeInYear = byte.Parse(ageInYear);
                        reg.AgeInMonth = byte.Parse(ageInMonth);
                        reg.AgeInDay = byte.Parse(ageInDay);

                        var srShift = Helper.GetShiftID(reg.RegistrationTime.Replace(":", ""));
                        reg.SRShift = srShift;
                        reg.AccountNo = string.Empty;
                        reg.InsuranceID = string.Empty;

                        var paramedic = new Paramedic();
                        paramedic.LoadByPrimaryKey(app.ParamedicID);
                        reg.SmfID = paramedic.SRParamedicRL1;
                        reg.SRPatientInType = string.Empty;
                        reg.SRPatientCategory = "PatientCategory-001";
                        reg.SRERCaseType = string.Empty;
                        reg.SRVisitReason = string.Empty;
                        reg.ReasonsForTreatmentID = string.Empty;
                        reg.ReasonsForTreatmentDescID = string.Empty;

                        var guarantor = new Guarantor();
                        guarantor.LoadByPrimaryKey(reg.GuarantorID);
                        reg.SRBussinesMethod = guarantor.SRBusinessMethod;
                        reg.PlavonAmount = 0;
                        reg.PatientAdm = 0;
                        reg.GuarantorAdm = 0;
                        reg.str.ServiceUnitID = app.ServiceUnitID;

                        var serviceUnit = new ServiceUnit();
                        serviceUnit.LoadByPrimaryKey(app.ServiceUnitID);
                        reg.str.DepartmentID = serviceUnit.DepartmentID;
                        reg.str.VisitTypeID = string.IsNullOrEmpty(app.VisitTypeID) ? "VT001" : app.VisitTypeID;
                        reg.str.SRReferralGroup = AppSession.Parameter.ReferralGroupDatangSendiri;
                        reg.str.ReferralID = string.Empty;
                        reg.ReferralName = string.Empty;
                        reg.Anamnesis = string.Empty;
                        reg.Complaint = string.Empty;
                        reg.InitialDiagnose = string.Empty;
                        reg.MedicationPlanning = string.Empty;
                        reg.SRTriage = string.Empty;
                        reg.IsPrintingPatientCard = false;
                        reg.IsTransferedToInpatient = false;
                        reg.IsNewBornInfant = false;
                        reg.IsParturition = false;
                        reg.IsRoomIn = false;
                        reg.IsHoldTransactionEntry = false;
                        reg.IsHasCorrection = false;
                        reg.IsEMRValid = false;
                        reg.IsBackDate = false;
                        reg.IsVoid = false;
                        reg.IsClosed = false;
                        reg.PlavonAmount = (decimal)0D;
                        reg.Notes = string.Empty;
                        reg.IsFromDispensary = false;
                        reg.IsClusterAssessment = false;
                        reg.IsNewVisit = false;
                        reg.LastCreateUserID = AppSession.UserLogin.UserID;
                        reg.LastCreateDateTime = DateTime.Now;
                        reg.SREmployeeRelationship = string.Empty;
                        reg.PersonID = null;
                        reg.EmployeeNumber = null;
                        reg.GuarantorCardNo = string.Empty;
                        reg.IsGlobalPlafond = guarantor.IsGlobalPlafond;
                        reg.FromRegistrationNo = !string.IsNullOrEmpty(app.FromRegistrationNo)
                                                     ? app.FromRegistrationNo
                                                     : string.Empty;
                        reg.IsNewPatient = (patient.LastVisitDate == null);
                        reg.RegistrationQue = app.AppointmentQue;
                        reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        reg.LastUpdateDateTime = DateTime.Now;
                        #endregion

                        #region ServiceUnitQue
                        que.AddNew();
                        que.QueDate = reg.RegistrationDate.Value + TimeSpan.Parse(reg.RegistrationTime);
                        que.RegistrationNo = reg.RegistrationNo;
                        que.ParamedicID = reg.ParamedicID;
                        que.ServiceUnitID = reg.ServiceUnitID;
                        que.QueNo = app.AppointmentQue;
                        que.ServiceRoomID = reg.RoomID;
                        que.IsFromReferProcess = false;
                        que.StartTime = que.QueDate;
                        que.IsStopped = false;
                        que.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        que.LastUpdateDateTime = DateTime.Now;

                        #endregion

                        #region Auto bill & visite item

                        var transChargesItemsDt = new TransChargesItemCollection();
                        var tciQ = new TransChargesItemQuery("a");
                        var tcQ = new TransChargesQuery("b");
                        tciQ.InnerJoin(tcQ).On(tciQ.TransactionNo == tcQ.TransactionNo);
                        tciQ.Where(tcQ.RegistrationNo == reg.RegistrationNo);
                        transChargesItemsDt.Load(tciQ);

                        var transChargesItemsDtComp = new TransChargesItemCompCollection();
                        var tcicQ = new TransChargesItemCompQuery("a");
                        tciQ = new TransChargesItemQuery("b");
                        tcQ = new TransChargesQuery("c");
                        tcicQ.InnerJoin(tciQ).On(tcicQ.TransactionNo == tciQ.TransactionNo &&
                                                 tcicQ.SequenceNo == tciQ.SequenceNo);
                        tcicQ.InnerJoin(tcQ).On(tciQ.TransactionNo == tcQ.TransactionNo);
                        tcicQ.Where(tcQ.RegistrationNo == reg.RegistrationNo);
                        transChargesItemsDtComp.Load(tcicQ);

                        var transChargesItemsDtConsumption = new TransChargesItemConsumptionCollection();
                        var tciccQ = new TransChargesItemConsumptionQuery("a");
                        tciQ = new TransChargesItemQuery("b");
                        tcQ = new TransChargesQuery("c");
                        tciccQ.InnerJoin(tciQ).On(tciccQ.TransactionNo == tciQ.TransactionNo &&
                                                 tciccQ.SequenceNo == tciQ.SequenceNo);
                        tciccQ.InnerJoin(tcQ).On(tciQ.TransactionNo == tcQ.TransactionNo);
                        tciccQ.Where(tcQ.RegistrationNo == reg.RegistrationNo);
                        transChargesItemsDtConsumption.Load(tciccQ);

                        var costCalculation = new CostCalculationCollection();
                        costCalculation.Query.Where(costCalculation.Query.RegistrationNo == reg.RegistrationNo);
                        costCalculation.LoadAll();

                        var registrationItemRule = new RegistrationItemRuleCollection();
                        registrationItemRule.Query.Where(registrationItemRule.Query.RegistrationNo == reg.RegistrationNo);
                        registrationItemRule.LoadAll();

                        var patientCardItemId = AppSession.Parameter.PatientCardItemID;

                        var billColl = new ServiceUnitAutoBillItemCollection();
                        if (string.IsNullOrEmpty(reg.VisiteRegistrationNo))
                        {
                            billColl.Query.Where
                                (
                                    billColl.Query.ServiceUnitID == reg.ServiceUnitID,
                                    billColl.Query.IsActive == true,
                                    billColl.Query.ItemID != patientCardItemId
                                );

                            if (reg.IsNewPatient == true)
                                billColl.Query.Where(billColl.Query.IsGenerateOnNewRegistration == true);
                            else
                                billColl.Query.Where(billColl.Query.IsGenerateOnRegistration == true);

                            billColl.LoadAll();

                            var parColl = new ParamedicAutoBillItemCollection();
                            parColl.Query.Where
                                (
                                    parColl.Query.ParamedicID == reg.ParamedicID,
                                    parColl.Query.ServiceUnitID == reg.ServiceUnitID,
                                    parColl.Query.IsActive == true
                                );
                            parColl.Query.Where(parColl.Query.IsGenerateOnRegistration == true);
                            parColl.LoadAll();

                            foreach (var par in parColl)
                            {
                                var suabi = billColl.AddNew();
                                suabi.ServiceUnitID = string.Empty;
                                suabi.ItemID = par.ItemID;
                                suabi.Quantity = par.Quantity;

                                var item = new ItemService();
                                suabi.SRItemUnit = item.LoadByPrimaryKey(suabi.ItemID) ? item.SRItemUnit : "X";

                                suabi.IsAutoPayment = false;
                                suabi.IsActive = true;
                                suabi.IsGenerateOnRegistration = par.IsGenerateOnRegistration;
                                suabi.IsGenerateOnNewRegistration = par.IsGenerateOnRegistration;
                                suabi.IsGenerateOnReferral = par.IsGenerateOnReferral;
                                suabi.LastUpdateDateTime = DateTime.Now;
                                suabi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            }

                            if (AppSession.Parameter.IsVisibleGuarantorAutoBillItem)
                            {
                                //--guarantor auto bill
                                var guarAutoBills = new GuarantorAutoBillItemCollection();
                                guarAutoBills.Query.Where(guarAutoBills.Query.GuarantorID == reg.GuarantorID, guarAutoBills.Query.ServiceUnitID == reg.ServiceUnitID);
                                if (reg.IsNewPatient == true)
                                    guarAutoBills.Query.Where(guarAutoBills.Query.IsGenerateOnNewRegistration == true);
                                else
                                    guarAutoBills.Query.Where(guarAutoBills.Query.IsGenerateOnRegistration == true);

                                guarAutoBills.LoadAll();
                                foreach (var a in guarAutoBills)
                                {
                                    var suabi = billColl.AddNew();
                                    suabi.ServiceUnitID = string.Empty;
                                    suabi.ItemID = a.ItemID;
                                    suabi.Quantity = a.Quantity;

                                    var item = new ItemService();
                                    suabi.SRItemUnit = item.LoadByPrimaryKey(suabi.ItemID) ? item.SRItemUnit : "X";

                                    suabi.IsAutoPayment = false;
                                    suabi.IsActive = true;
                                    suabi.IsGenerateOnRegistration = a.IsGenerateOnRegistration;
                                    suabi.IsGenerateOnNewRegistration = a.IsGenerateOnNewRegistration;
                                    suabi.IsGenerateOnReferral = a.IsGenerateOnReferral;
                                    suabi.IsGenerateOnFirstRegistration = a.IsGenerateOnFirstRegistration;
                                    suabi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    suabi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                }
                            }

                            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                            {
                                if (serviceUnit.DepartmentID == AppSession.Parameter.OutPatientDepartmentID)
                                {
                                    var itemIdAdmRjGuar = AppSession.Parameter.ItemIdAdmRjGuar;

                                    if (!string.IsNullOrEmpty(itemIdAdmRjGuar))
                                    {
                                        if (guarantor.SRGuarantorType != AppSession.Parameter.GuarantorTypeSelf)
                                        {
                                            if (!string.IsNullOrEmpty(itemIdAdmRjGuar))
                                            {
                                                var suabi = billColl.AddNew();
                                                suabi.ServiceUnitID = string.Empty;
                                                suabi.ItemID = itemIdAdmRjGuar;
                                                suabi.Quantity = 1;

                                                var item = new ItemService();
                                                suabi.SRItemUnit = item.LoadByPrimaryKey(suabi.ItemID) ? item.SRItemUnit : "X";

                                                suabi.IsAutoPayment = false;
                                                suabi.IsActive = true;
                                                suabi.IsGenerateOnRegistration = true;
                                                suabi.IsGenerateOnNewRegistration = true;
                                                suabi.IsGenerateOnReferral = false;
                                                suabi.LastUpdateDateTime = DateTime.Now;
                                                suabi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            var visites = new TransPaymentItemVisiteCollection();
                            visites.Query.Where(visites.Query.PaymentNo == reg.VisiteRegistrationNo);
                            visites.LoadAll();

                            foreach (var visite in visites)
                            {
                                var bill = billColl.AddNew();
                                bill.ServiceUnitID = reg.ServiceUnitID;
                                bill.ItemID = visite.ItemID;
                                bill.Quantity = 1;
                                bill.SRItemUnit = "X";
                                bill.IsAutoPayment = true;
                                bill.IsActive = true;
                                bill.IsGenerateOnRegistration = true;
                                bill.IsGenerateOnNewRegistration = false;
                                bill.IsGenerateOnReferral = false;
                                bill.LastUpdateDateTime = DateTime.Now;
                                bill.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            }
                        }

                        if (billColl.Count > 0)
                        {
                            chargesHd.AddNew();
                            chargesHd.TransactionNo = GetNewTransactionNo();
                            _autoNumberTrans.LastCompleteNumber = chargesHd.TransactionNo;
                            _autoNumberTrans.Save();

                            chargesHd.RegistrationNo = reg.RegistrationNo;
                            chargesHd.TransactionDate = DateTime.Parse(reg.RegistrationDate.Value.ToShortDateString() + " " + reg.RegistrationTime);
                            chargesHd.ExecutionDate = DateTime.Parse(reg.RegistrationDate.Value.ToShortDateString() + " " + reg.RegistrationTime);
                            chargesHd.ReferenceNo = string.Empty;
                            chargesHd.FromServiceUnitID = reg.ServiceUnitID;
                            chargesHd.ToServiceUnitID = reg.ServiceUnitID;
                            chargesHd.ClassID = reg.ChargeClassID;
                            chargesHd.RoomID = reg.RoomID;
                            chargesHd.BedID = reg.BedID;
                            chargesHd.DueDate = DateTime.Now.Date;
                            chargesHd.SRShift = reg.SRShift;
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
                            chargesHd.SurgicalPackageID = string.Empty;
                            chargesHd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            chargesHd.LastUpdateDateTime = DateTime.Now;
                            chargesHd.CreatedByUserID = AppSession.UserLogin.UserID;
                            chargesHd.CreatedDateTime = DateTime.Now;
                            chargesHd.IsPackage = false;
                            chargesHd.IsRoomIn = reg.IsRoomIn;

                            var room = new ServiceRoom();
                            room.LoadByPrimaryKey(reg.RoomID);
                            chargesHd.TariffDiscountForRoomIn = room.TariffDiscountForRoomIn;
                        }

                        var seqNo = string.Empty;
                        foreach (ServiceUnitAutoBillItem billItem in billColl)
                        {
                            var item = new Item();
                            item.LoadByPrimaryKey(billItem.ItemID);
                            string itemTypeHd = item.SRItemType;

                            seqNo = transChargesItemsDt.Count == 0 ? "001" : string.Format("{0:000}", int.Parse(transChargesItemsDt[transChargesItemsDt.Count - 1].SequenceNo) + 1);

                            var chargesDt = transChargesItemsDt.AddNew();
                            chargesDt.TransactionNo = chargesHd.TransactionNo;
                            chargesDt.SequenceNo = seqNo;
                            chargesDt.ReferenceNo = string.Empty;
                            chargesDt.ReferenceSequenceNo = string.Empty;
                            chargesDt.ItemID = billItem.ItemID;
                            chargesDt.ChargeClassID = reg.ChargeClassID;
                            chargesDt.ParamedicID = string.Empty;

                            var transDate = chargesHd.TransactionDate.Value.Date;
                            if (guarantor.TariffCalculationMethod == 1) transDate = reg.RegistrationDate.Value.Date;

                            ItemTariff tariff =
                                (Helper.Tariff.GetItemTariff(transDate, guarantor.SRTariffType,
                                                             chargesHd.ClassID, chargesHd.ClassID, chargesDt.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                 Helper.Tariff.GetItemTariff(transDate, guarantor.SRTariffType,
                                                             AppSession.Parameter.DefaultTariffClass, chargesHd.ClassID, chargesDt.ItemID,
                                                             reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                (Helper.Tariff.GetItemTariff(transDate,
                                                             AppSession.Parameter.DefaultTariffType, chargesHd.ClassID, chargesHd.ClassID,
                                                             chargesDt.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                 Helper.Tariff.GetItemTariff(transDate,
                                                             AppSession.Parameter.DefaultTariffType,
                                                             AppSession.Parameter.DefaultTariffClass, chargesHd.ClassID, chargesDt.ItemID,
                                                             reg.GuarantorID, false, reg.SRRegistrationType));

                            chargesDt.IsAdminCalculation = tariff.IsAdminCalculation ?? false;

                            switch (itemTypeHd)
                            {
                                case BusinessObject.Reference.ItemType.Service:
                                    var service = new ItemService();
                                    service.LoadByPrimaryKey(billItem.ItemID);
                                    chargesDt.SRItemUnit = service.SRItemUnit;

                                    chargesDt.CostPrice = tariff.Price ?? 0;
                                    break;
                                case BusinessObject.Reference.ItemType.Medical:
                                    var ipm = new ItemProductMedic();
                                    ipm.LoadByPrimaryKey(billItem.ItemID);
                                    chargesDt.SRItemUnit = ipm.SRItemUnit;

                                    chargesDt.CostPrice = ipm.CostPrice ?? 0;
                                    break;
                                case BusinessObject.Reference.ItemType.NonMedical:
                                    var ipn = new ItemProductNonMedic();
                                    ipn.LoadByPrimaryKey(billItem.ItemID);
                                    chargesDt.SRItemUnit = ipn.SRItemUnit;

                                    chargesDt.CostPrice = ipn.CostPrice ?? 0;
                                    break;
                                case BusinessObject.Reference.ItemType.Laboratory:
                                case BusinessObject.Reference.ItemType.Diagnostic:
                                case BusinessObject.Reference.ItemType.Radiology:
                                    chargesDt.SRItemUnit = string.Empty;
                                    chargesDt.CostPrice = tariff.Price ?? 0;
                                    break;
                            }

                            chargesDt.IsVariable = false;
                            chargesDt.IsCito = false;
                            chargesDt.IsCitoInPercent = false;
                            chargesDt.BasicCitoAmount = (decimal)0D;
                            chargesDt.ChargeQuantity = billItem.Quantity;

                            if (itemTypeHd == BusinessObject.Reference.ItemType.Medical || itemTypeHd == BusinessObject.Reference.ItemType.NonMedical)
                                chargesDt.StockQuantity = billItem.Quantity;
                            else
                                chargesDt.StockQuantity = (decimal)0D;

                            var itemRooms = new AppStandardReferenceItemCollection();
                            itemRooms.Query.Where(itemRooms.Query.StandardReferenceID == "ItemTariffRoom",
                                                  itemRooms.Query.ItemID == billItem.ItemID, itemRooms.Query.IsActive == true);
                            itemRooms.LoadAll();
                            chargesDt.IsItemRoom = itemRooms.Count > 0;

                            chargesDt.Price = tariff.Price ?? 0;
                            if (chargesDt.IsItemRoom == true && chargesHd.IsRoomIn == true)
                                chargesDt.Price = chargesDt.Price - (chargesDt.Price * chargesHd.TariffDiscountForRoomIn / 100);

                            if (string.IsNullOrEmpty(reg.VisiteRegistrationNo))
                                chargesDt.DiscountAmount = (decimal)0D;
                            else
                            {
                                var visites = new TransPaymentItemVisiteQuery();
                                visites.SelectAll().Where(visites.PaymentNo == reg.VisiteRegistrationNo);
                                var visit = new TransPaymentItemVisite();
                                visit.Load(visites);
                                chargesDt.DiscountAmount = visit.Price * (visit.Discount / 100);
                            }
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
                            chargesDt.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            chargesDt.LastUpdateDateTime = DateTime.Now;
                            chargesDt.ParentNo = string.Empty;
                            chargesDt.SRCenterID = string.Empty;
                            chargesDt.ItemConditionRuleID = string.Empty;
                            chargesDt.CreatedByUserID = AppSession.UserLogin.UserID;
                            chargesDt.CreatedDateTime = DateTime.Now;

                            #region item component

                            var compQuery = new ItemTariffComponentQuery();
                            compQuery.es.Top = 1;
                            compQuery.Where
                                (
                                    compQuery.SRTariffType == guarantor.SRTariffType,
                                    compQuery.ItemID == billItem.ItemID,
                                    compQuery.ClassID == reg.ChargeClassID,
                                    compQuery.StartingDate <= DateTime.Now.Date
                                );


                            var compColl = Helper.Tariff.GetItemTariffComponentCollection(transDate,
                                                                                          guarantor.SRTariffType, chargesHd.ClassID,
                                                                                          chargesDt.ItemID);
                            if (!compColl.Any())
                                compColl = Helper.Tariff.GetItemTariffComponentCollection(transDate,
                                                                                          guarantor.SRTariffType,
                                                                                          AppSession.Parameter.DefaultTariffClass,
                                                                                          chargesDt.ItemID);
                            if (!compColl.Any())
                                compColl = Helper.Tariff.GetItemTariffComponentCollection(transDate,
                                                                                          AppSession.Parameter.DefaultTariffType,
                                                                                          chargesHd.ClassID, chargesDt.ItemID);
                            if (!compColl.Any())
                                compColl = Helper.Tariff.GetItemTariffComponentCollection(transDate,
                                                                                          AppSession.Parameter.DefaultTariffType,
                                                                                          AppSession.Parameter.DefaultTariffClass,
                                                                                          chargesDt.ItemID);

                            var p = string.Empty;
                            foreach (var comp in compColl)
                            {
                                var compCharges = transChargesItemsDtComp.AddNew();
                                compCharges.TransactionNo = chargesHd.TransactionNo;
                                compCharges.SequenceNo = seqNo;
                                compCharges.TariffComponentID = comp.TariffComponentID;
                                if (chargesHd.IsRoomIn == true && chargesDt.IsItemRoom == true)
                                    compCharges.Price = comp.Price - (comp.Price * chargesHd.TariffDiscountForRoomIn / 100);
                                else
                                    compCharges.Price = comp.Price;
                                if (string.IsNullOrEmpty(reg.VisiteRegistrationNo))
                                    compCharges.DiscountAmount = (decimal)0D;
                                else
                                {
                                    var visites = new TransPaymentItemVisiteQuery();
                                    visites.SelectAll().Where(visites.PaymentNo == reg.VisiteRegistrationNo);
                                    var visit = new TransPaymentItemVisite();
                                    visit.Load(visites);
                                    compCharges.DiscountAmount = visit.Price * (visit.Discount / 100);
                                }
                                compCharges.CitoAmount = (decimal)0D;

                                var tcomp = new TariffComponent();
                                tcomp.LoadByPrimaryKey(comp.TariffComponentID);

                                if (tcomp.IsTariffParamedic ?? false)
                                    compCharges.ParamedicID = reg.ParamedicID;
                                else
                                    compCharges.ParamedicID = string.Empty;

                                compCharges.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                compCharges.LastUpdateDateTime = DateTime.Now;

                                if (!string.IsNullOrEmpty(compCharges.ParamedicID))
                                {
                                    if (tcomp.IsPrintParamedicInSlip ?? false)
                                    {
                                        var par = new Paramedic();
                                        par.LoadByPrimaryKey(compCharges.ParamedicID);
                                        if (par.IsPrintInSlip ?? true)
                                            p = p.Length == 0 ? par.ParamedicName : p + "; " + par.ParamedicName;
                                    }
                                }
                            }
                            chargesDt.ParamedicCollectionName = p;

                            #endregion

                            #region Item Consumption

                            var consQuery = new ItemConsumptionQuery();
                            consQuery.Where(consQuery.ItemID == billItem.ItemID);

                            var consColl = new ItemConsumptionCollection();
                            consColl.Load(consQuery);

                            foreach (var cons in consColl)
                            {
                                var consCharges = transChargesItemsDtConsumption.AddNew();
                                consCharges.TransactionNo = chargesHd.TransactionNo;
                                consCharges.SequenceNo = seqNo;
                                consCharges.DetailItemID = cons.ItemID;
                                consCharges.Qty = cons.Qty;
                                consCharges.SRItemUnit = cons.SRItemUnit;
                                consCharges.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                consCharges.LastUpdateDateTime = DateTime.Now;
                            }

                            #endregion
                        }

                        #region auto calculation

                        if (transChargesItemsDt.Count > 0)
                        {
                            var grrID = reg.GuarantorID;
                            if (grrID == AppSession.Parameter.SelfGuarantor)
                            {
                                var pat = new Patient();
                                pat.LoadByPrimaryKey(reg.PatientID);
                                if (!string.IsNullOrEmpty(pat.MemberID))
                                    grrID = pat.MemberID;
                            }

                            DataTable tblCovered = Helper.GetCoveredItems(reg, reg.SRBussinesMethod, reg.PlavonAmount ?? 0, reg.IsGlobalPlafond ?? false,
                                reg.ChargeClassID, reg.CoverageClassID, grrID, transChargesItemsDt.Select(t => t.ItemID).ToArray(),
                                reg.RegistrationDate.Value.Date, registrationItemRule, false);

                            foreach (TransChargesItem detail in transChargesItemsDt)
                            {
                                var rowCovered = tblCovered.AsEnumerable().Where(t => t.Field<string>("ItemID") == detail.ItemID &&
                                                                                      t.Field<bool>("IsInclude")).SingleOrDefault();

                                //TransChargesItemComps
                                if (rowCovered != null)
                                {
                                    decimal? discount = 0;
                                    bool isDiscount = false, isMargin = false;
                                    foreach (var comp in transChargesItemsDtComp.Where(t => t.TransactionNo == detail.TransactionNo &&
                                                                                            t.SequenceNo == detail.SequenceNo)
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
                                                var transDate = chargesHd.TransactionDate.Value.Date;
                                                if (guarantor.TariffCalculationMethod == 1) transDate = reg.RegistrationDate.Value.Date;

                                                var tcomp = Helper.Tariff.GetItemTariffComponent(transDate, guarantor.SRTariffType,
                                                    reg.CoverageClassID, comp.TariffComponentID, detail.ItemID);
                                                if (!tcomp.AsEnumerable().Any())
                                                    tcomp = Helper.Tariff.GetItemTariffComponent(transDate, guarantor.SRTariffType,
                                                        AppSession.Parameter.DefaultTariffClass, comp.TariffComponentID, detail.ItemID);
                                                if (!tcomp.AsEnumerable().Any())
                                                    tcomp = Helper.Tariff.GetItemTariffComponent(transDate, AppSession.Parameter.DefaultTariffType,
                                                        reg.CoverageClassID, comp.TariffComponentID, detail.ItemID);
                                                if (!tcomp.AsEnumerable().Any())
                                                    tcomp = Helper.Tariff.GetItemTariffComponent(transDate, AppSession.Parameter.DefaultTariffType,
                                                        AppSession.Parameter.DefaultTariffClass, comp.TariffComponentID, detail.ItemID);

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
                                                        if (compPrice >= amountValue)
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
                                                comp.AutoProcessCalculation = (amountValue / 100) * comp.Price;
                                                comp.Price += (amountValue / 100) * comp.Price;

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
                                    detail.AutoProcessCalculation = transChargesItemsDtComp.Where(t => t.TransactionNo == detail.TransactionNo &&
                                                                                                       t.SequenceNo == detail.SequenceNo)
                                                                                           .Sum(t => t.AutoProcessCalculation);
                                    if (detail.AutoProcessCalculation < 0)
                                    {
                                        detail.DiscountAmount += detail.ChargeQuantity * Math.Abs(detail.AutoProcessCalculation ?? 0);

                                        if (detail.DiscountAmount > detail.Price)
                                        {
                                            detail.DiscountAmount = detail.Price;
                                            detail.AutoProcessCalculation = 0 - detail.Price;
                                        }
                                    }
                                    else if (detail.AutoProcessCalculation > 0)
                                        detail.Price += detail.AutoProcessCalculation;
                                }
                                else
                                {
                                    if (rowCovered != null)
                                    {
                                        if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                                        {
                                            var basicPrice = (decimal?)rowCovered["BasicPrice"];
                                            var coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                            var detailPrice = detail.Price ?? 0;
                                            if (basicPrice > coveragePrice)
                                            {
                                                var transDate = chargesHd.TransactionDate.Value.Date;
                                                if (guarantor.TariffCalculationMethod == 1) transDate = reg.RegistrationDate.Value.Date;

                                                ItemTariff tariff = (Helper.Tariff.GetItemTariff(transDate, guarantor.SRTariffType, reg.CoverageClassID, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                                         Helper.Tariff.GetItemTariff(transDate, guarantor.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                                        (Helper.Tariff.GetItemTariff(transDate, AppSession.Parameter.DefaultTariffType, reg.CoverageClassID, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                                         Helper.Tariff.GetItemTariff(transDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));
                                                if (tariff != null)
                                                    detailPrice = tariff.Price ?? 0;
                                            }

                                            if ((bool)rowCovered["IsValueInPercent"])
                                            {
                                                detail.DiscountAmount += (detail.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * detailPrice);
                                                detail.AutoProcessCalculation = 0 - (detail.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * detailPrice);
                                            }
                                            else
                                            {
                                                detail.DiscountAmount += (detail.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];
                                                detail.AutoProcessCalculation = 0 - (detail.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];
                                            }

                                            if (detail.DiscountAmount > detailPrice)
                                                detail.DiscountAmount = detailPrice;
                                        }
                                        else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
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
                                        }
                                    }
                                }

                                //post
                                decimal? total = ((detail.ChargeQuantity * detail.Price) - detail.DiscountAmount) + detail.CitoAmount;
                                var calc = new Helper.CostCalculation(grrID, detail.ItemID, total ?? 0, tblCovered, detail.ChargeQuantity ?? 0,
                                                                          detail.IsCito ?? false,
                                                                          detail.IsCitoInPercent ?? false,
                                                                          detail.BasicCitoAmount ?? 0, detail.Price ?? 0,
                                                                          chargesHd.IsRoomIn ?? false, detail.IsItemRoom ?? false,
                                                                          chargesHd.TariffDiscountForRoomIn ?? 0, detail.DiscountAmount ?? 0, false,
                                                                          detail.ItemConditionRuleID, chargesHd.TransactionDate.Value, false);

                                CostCalculation cost = costCalculation.AddNew();
                                cost.RegistrationNo = reg.RegistrationNo;
                                cost.TransactionNo = detail.TransactionNo;
                                cost.SequenceNo = detail.SequenceNo;
                                cost.ItemID = detail.ItemID;
                                cost.PatientAmount = calc.PatientAmount;
                                cost.GuarantorAmount = calc.GuarantorAmount;
                                cost.DiscountAmount = detail.DiscountAmount;
                                cost.IsPackage = detail.IsPackage;
                                cost.ParentNo = detail.ParentNo;
                                cost.ParamedicAmount = detail.ChargeQuantity * transChargesItemsDtComp.Where(comp => comp.TransactionNo == detail.TransactionNo &&
                                                                                                                     comp.SequenceNo == detail.SequenceNo &&
                                                                                                                     !string.IsNullOrEmpty(comp.ParamedicID))
                                                                                                      .Sum(comp => comp.Price - comp.DiscountAmount);
                                cost.LastUpdateDateTime = DateTime.Now;
                                cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            }
                        }

                        #endregion

                        reg.RemainingAmount = costCalculation.Sum(c => (c.PatientAmount + c.GuarantorAmount));
                        reg.Save();
                        que.Save();

                        chargesHd.Save();
                        transChargesItemsDt.Save();
                        transChargesItemsDtComp.Save();
                        transChargesItemsDtConsumption.Save();
                        costCalculation.Save();

                        if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                        {
                            // extract fee
                            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                            feeColl.SetFeeByTCIC(transChargesItemsDtComp, AppSession.UserLogin.UserID);
                            feeColl.Save();
                            //feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                            //feeColl.Save();
                        }

                        #endregion

                        #region Merge Billing
                        billing.AddNew();
                        billing.RegistrationNo = reg.RegistrationNo;
                        billing.FromRegistrationNo = string.Empty;
                        billing.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        billing.LastUpdateDateTime = DateTime.Now;
                        billing.Save();
                        #endregion

                        #region Insert Medical Record File Status
                        mrFileStatus.AddNew();
                        mrFileStatus.RegistrationNo = reg.RegistrationNo;
                        mrFileStatus.FileOutDate = reg.RegistrationDate;
                        mrFileStatus.SRMedicalFileCategory = AppSession.Parameter.MedicalFileCategoryOut;
                        mrFileStatus.SRMedicalFileStatus = AppSession.Parameter.MedicalFileStatusRequest;
                        mrFileStatus.Notes = string.Empty;
                        mrFileStatus.RequestByUserID = AppSession.UserLogin.UserID;
                        mrFileStatus.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        mrFileStatus.LastUpdateDateTime = DateTime.Now;
                        mrFileStatus.Save();
                        #endregion Insert Medical File Status

                        #region Responsible Person
                        responsible.AddNew();
                        responsible.RegistrationNo = reg.RegistrationNo;
                        responsible.NameOfTheResponsible = string.Empty;
                        responsible.SRRelationship = string.Empty;
                        responsible.SROccupation = string.Empty;
                        responsible.JobDescription = string.Empty;
                        responsible.HomeAddress = string.Empty;
                        responsible.PhoneNo = string.Empty;
                        responsible.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        responsible.LastUpdateDateTime = DateTime.Now;
                        responsible.Save();
                        #endregion

                        #region Emergency Contact
                        if (patEmrContact.LoadByPrimaryKey(reg.PatientID))
                        {
                            emrContact.AddNew();
                            emrContact.RegistrationNo = reg.RegistrationNo;
                            emrContact.ContactName = patEmrContact.ContactName;
                            emrContact.SRRelationship = patEmrContact.SRRelationship;
                            emrContact.SROccupation = patEmrContact.SROccupation;
                            emrContact.StreetName = patEmrContact.StreetName;
                            emrContact.District = patEmrContact.District;
                            emrContact.City = patEmrContact.City;
                            emrContact.County = patEmrContact.County;
                            emrContact.State = patEmrContact.State;
                            emrContact.str.ZipCode = patEmrContact.ZipCode;
                            emrContact.PhoneNo = patEmrContact.PhoneNo;
                            emrContact.FaxNo = patEmrContact.FaxNo;
                            emrContact.MobilePhoneNo = patEmrContact.MobilePhoneNo;
                            emrContact.Email = patEmrContact.Email;
                            emrContact.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            emrContact.LastUpdateDateTime = DateTime.Now;
                            emrContact.Save();
                        }
                        #endregion

                        #region Registration Info Sumary
                        if (!registrationInfoSumary.LoadByPrimaryKey(reg.RegistrationNo))
                        {
                            registrationInfoSumary.AddNew();
                            registrationInfoSumary.RegistrationNo = reg.RegistrationNo;
                            registrationInfoSumary.NoteCount = 0;
                            registrationInfoSumary.NoteMedicalCount = 0;
                            registrationInfoSumary.DocumentCheckListCount = 0;
                            registrationInfoSumary.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            registrationInfoSumary.LastUpdateDateTime = DateTime.Now;
                            registrationInfoSumary.Save();
                        }
                        #endregion

                        #region Patient Info Sumary
                        if (!PatientInfoSumary.LoadByPrimaryKey(reg.RegistrationNo))
                        {
                            PatientInfoSumary.AddNew();
                            PatientInfoSumary.PatientID = reg.PatientID;
                            PatientInfoSumary.NoteCount = 0;
                            PatientInfoSumary.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            PatientInfoSumary.LastUpdateDateTime = DateTime.Now;
                            PatientInfoSumary.Save();
                        }
                        #endregion
                    }
                }

                trans.Complete();
            }
        }

        private string GetNewRegistrationNo()
        {
            _autoNumberReg = Helper.GetNewAutoNumber(DateTime.Now.Date,
                                                     BusinessObject.Reference.TransactionCode.Registration,
                                                     AppSession.Parameter.OutPatientDepartmentID);

            return _autoNumberReg.LastCompleteNumber;
        }

        private string GetNewTransactionNo()
        {
            _autoNumberTrans = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.TransactionNo);
            return _autoNumberTrans.LastCompleteNumber;
        }

        #endregion

        protected void btnSearchBPJS_Click(object sender, ImageClickEventArgs e)
        {
            //if (Common.Helper.IsBpjsIntegration)
            //{
            //    var bpjs = new BusinessObject.BpjsSEP();
            //    var i = bpjs.ImportSEP(txtNoSEP.Text, txtSEPDate.SelectedDate.Value.Date, txtBPJSNo.Text, txtPatientNameBPJS.Text,
            //        RegistrationType == AppConstant.RegistrationType.InPatient ? "1" : "2");
            //}

            grdBPJS.Rebind();
        }

        protected void grdBPJS_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var bpjs = new BpjsSEPQuery("a");
            var unit = new ServiceUnitBridgingQuery("b");
            var patient = new PatientQuery("c");
            var reg = new RegistrationQuery("d");

            bpjs.Select(
                bpjs.NoSEP,
                bpjs.TanggalSEP,
                bpjs.NomorKartu,
                bpjs.NamaPasien,
                "<CASE WHEN a.JenisKelamin = 'P' THEN 'F' ELSE 'M' END AS Sex>",
                bpjs.TanggalLahir,
                bpjs.LakaLantas,
                "<ISNULL(c.MedicalNo, a.NoMR) AS NoMR>",
                patient.PatientID.Coalesce("''"),
                bpjs.PoliTujuan
                );

            if (RegistrationType == AppConstant.RegistrationType.EmergencyPatient || RegistrationType == AppConstant.RegistrationType.OutPatient)
            {
                bpjs.Select(
                    unit.BridgingID.Coalesce("''"),
                    unit.BridgingName.Coalesce("''")
                );
                bpjs.LeftJoin(unit).On(bpjs.PoliTujuan == unit.BridgingID && unit.SRBridgingType == AppEnum.BridgingType.BPJS.ToString());
            }
            else
            {
                bpjs.Select(
                    "<'' AS BridgingID>",
                    "<'' AS BridgingName>"
                );
            }
            bpjs.LeftJoin(patient).On(bpjs.PatientID == patient.PatientID);
            bpjs.LeftJoin(reg).On(bpjs.PatientID == reg.PatientID && bpjs.NoSEP == reg.BpjsSepNo && reg.IsVoid == false);

            if (!string.IsNullOrEmpty(txtBPJSNo.Text)) bpjs.Where(bpjs.NomorKartu == txtBPJSNo.Text);
            if (!txtSEPDate.IsEmpty) bpjs.Where(bpjs.TanggalSEP == txtSEPDate.SelectedDate);
            if (!string.IsNullOrEmpty(txtPatientNameBPJS.Text))
            {
                string searchTextContain = string.Format("%{0}%", txtPatientNameBPJS.Text);
                bpjs.Where(bpjs.NamaPasien.Like(searchTextContain));
            }
            if (!string.IsNullOrEmpty(txtNoSEP.Text)) bpjs.Where(bpjs.NoSEP == txtNoSEP.Text);

            bpjs.Where(string.Format("<a.JenisPelayanan = {0}>", RegistrationType == AppConstant.RegistrationType.InPatient ? "1" : "2"));
            bpjs.Where(reg.RegistrationNo.IsNull());

            if (RegistrationType == AppConstant.RegistrationType.EmergencyPatient) bpjs.Where(bpjs.PoliTujuan.In("IGD", "UGD"));
            else if (RegistrationType == AppConstant.RegistrationType.OutPatient) bpjs.Where(bpjs.PoliTujuan.NotIn("IGD", "UGD"));

            if (!string.IsNullOrEmpty(BuildingID))
            {
                var su = new ServiceUnitQuery("e");
                bpjs.InnerJoin(su).On(su.ServiceUnitID == unit.ServiceUnitID);
                bpjs.Where(bpjs.Or(su.SRBuilding.IsNull(), su.SRBuilding == string.Empty, su.SRBuilding == BuildingID));
            }

            var table = bpjs.LoadDataTable();
            grdBPJS.DataSource = table;
        }

        protected string GetUrlForNewRegistrationFromBPJS(GridItem container)
        {
            return (DataBinder.Eval(container.DataItem, "NoMR").ToString().Equals(string.Empty) || DataBinder.Eval(container.DataItem, "NoMR").ToString().Equals("-") ?
                      string.Format("<a href=\"#\" onclick=\"openWinPatientBPJS('new', '{0}', '{1}', '7'); return false;\"><img src=\"../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New Patient\" title=\"New Patient\" /></a>", DataBinder.Eval(container.DataItem, "NoSEP"), DataBinder.Eval(container.DataItem, "ServiceUnitID"))
                        : string.Format("<a href=\"#\" onclick=\"openWinRegBPJS('new', '{0}', '{1}', '0'); return false;\"><img src=\"../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New Registration\" title=\"New Registration\" /></a>", DataBinder.Eval(container.DataItem, "PatientID"), DataBinder.Eval(container.DataItem, "NoSEP")));
        }

        protected string GetItemTemplatePatientName(GridItem container)
        {
            if (string.IsNullOrEmpty(DataBinder.Eval(container.DataItem, "PatientID").ToString()))
                return DataBinder.Eval(container.DataItem, "NamaPasien").ToString();
            else
                return string.Format("<a href=\"#\" onclick=\"openWinPatient('edit', '{0}', 'patient','0'); return false;\"><b>{1}</b></a>",
                        DataBinder.Eval(container.DataItem, "PatientID"), DataBinder.Eval(container.DataItem, "NamaPasien"));
        }

        private byte IsValidateByZipCode
        {
            get
            {
                var app = AppSession.Parameter.TablePatientFieldValidation;
                if (string.IsNullOrEmpty(app)) return 0;
                if (app.Contains("ZipCode")) return 1;
                return 0;
            }
        }

                #region Google Form
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
        private string[] _driveThruGoogleScopes = { SheetsService.Scope.SpreadsheetsReadonly };
        private string _driveThruGoogleUser = AppParameter.GetParameterValue(AppParameter.ParameterItem.DriveThruGoogleUser);
        private string _driveThruGoogleAppName = AppParameter.GetParameterValue(AppParameter.ParameterItem.DriveThruGoogleAppName);
        private string _driveThruGoogleSpreadsheetId = AppParameter.GetParameterValue(AppParameter.ParameterItem.DriveThruGoogleSpreadsheetId); // "1iCnDmB6EQBagwbMS6t9McbT-YxpsophXtE0fKmQTtl0";
        protected void grdGoogleForm_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)sender;
            if (!IsPostBack)
            {
                grd.DataSource = string.Empty;
                return;
            }

            grd.DataSource = DownloadGoogleSheetRow();
        }

        // OAuth client created
        // ID: 653330822816-gnsio42v7qdfghsl2jv0fnn823aqjrfa.apps.googleusercontent.com
        // Secret: I-weoBuMwmvXehQljVtxe8to
        private DataTable DownloadGoogleSheetRow()
        {
            if (Session["gs"] != null) return (DataTable)Session["gs"];

            var service = GoogleSheetService();

            // Define request parameters.
            var range = "Form Responses 1!A2:M";

            var request =
                    service.Spreadsheets.Values.Get(_driveThruGoogleSpreadsheetId, range);

            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;

            var dtb = new DataTable();
            dtb.Columns.Add("Timestamp", typeof(DateTime));
            dtb.Columns.Add("Name", typeof(string));
            dtb.Columns.Add("HpNumber", typeof(string));
            dtb.Columns.Add("IdType", typeof(string));
            dtb.Columns.Add("SSN", typeof(string));
            dtb.Columns.Add("DateOfBirth", typeof(DateTime));
            dtb.Columns.Add("Gender", typeof(string));
            dtb.Columns.Add("Email", typeof(string));
            dtb.Columns.Add("TestType", typeof(string));
            dtb.Columns.Add("IdPhotoUrl", typeof(string));
            dtb.Columns.Add("CityOfBirth", typeof(string));
            dtb.Columns.Add("AddressStreet", typeof(string));
            dtb.Columns.Add("AddressCity", typeof(string));

            if (values != null && values.Count > 0)
            {
                foreach (var val in values)
                {
                    var ts = val[0].ToString();
                    var timestamp = DateTime.ParseExact(ts, ts.Length == 18 ? "dd/MM/yyyy H:mm:ss" : "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    if (!txtGFDate.IsEmpty)
                        if (timestamp.Date != txtGFDate.SelectedDate)
                            continue;

                    if (!string.IsNullOrEmpty(txtGFName.Text))
                        if (!val[1].ToString().ToLower().Contains(txtGFName.Text.ToLower()))
                            continue;

                    // Registered check
                    var rgf = new RegistrationGoogleForm();
                    if (rgf.LoadByPrimaryKey(timestamp))
                        continue;

                    var acount = val.Count;
                    var newRow = dtb.NewRow();
                    newRow["Timestamp"] = timestamp;
                    if (acount > 1)
                        newRow["HpNumber"] = val[1];

                    if (acount > 2)
                        newRow["Email"] = val[2];

                    if (acount > 3)
                        newRow["Name"] = val[3];

                    if (acount > 4)
                    {
                        newRow["SSN"] = val[4];
                    }

                    if (acount > 5)
                        newRow["DateOfBirth"] = DateTime.ParseExact(val[5].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (acount > 6)
                        newRow["Gender"] = val[6];


                    if (acount > 7)
                        newRow["TestType"] = val[7];

                    if (acount > 8)
                        newRow["AddressStreet"] = val[8];

                    if (acount > 9)
                        newRow["CityOfBirth"] = val[9];

                    //if (acount > 10)
                    //    newRow["AddressCity"] = val[10];

                    //if (acount > 11)
                    //    newRow[""] = val[11];

                    //if (acount > 12)
                    //    newRow["IdPhotoUrl"] = val[12];


                    dtb.Rows.Add(newRow);
                }

            }
            else
            {
                //Console.WriteLine("No data found.");
            }

            //Ad key
            dtb.PrimaryKey = new DataColumn[] { dtb.Columns["Timestamp"] };

            Session["gs"] = dtb;
            return dtb;
        }

        private SheetsService GoogleSheetService()
        {
            UserCredential credential;
            using (var stream =
                new FileStream(Server.MapPath("~/AvicennaCareGoogleCredentials.json"), FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = Server.MapPath("~/Token");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    _driveThruGoogleScopes,
                    _driveThruGoogleUser,
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                //Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _driveThruGoogleAppName,
            });
            return service;
        }
        private string GoogleSheetRange(SheetsService service, string sheetId)
        {
            // Define request parameters.  
            String spreadsheetId = sheetId;
            String range = "A:A";

            SpreadsheetsResource.ValuesResource.GetRequest getRequest =
                       service.Spreadsheets.Values.Get(spreadsheetId, range);
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            ValueRange getResponse = getRequest.Execute();
            IList<IList<Object>> getValues = getResponse.Values;
            if (getValues == null)
            {
                // spreadsheet is empty return Row A Column A  
                return "A:A";
            }

            int currentCount = getValues.Count() + 1;
            String newRange = "A" + currentCount + ":A";
            return newRange;
        }
        protected void btnSearchGoogleForm_Click(object sender, ImageClickEventArgs e)
        {
            Session["gs"] = null;
            grdGoogleForm.Rebind();
        }
        protected string NewRegFromGoogleForm(GridItem container)
        {
            return string.Format("<a href=\"#\" onclick=\"openWinPatientFromGoogleForm('{0}'); return false;\"><img src=\"../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New Registration\" title=\"New Registration\" /></a>", DataBinder.Eval(container.DataItem, "Timestamp"));
        }
        #endregion

    }
}