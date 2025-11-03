using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using System.Drawing;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ServiceUnitRegistrationList : BasePage
    {
        private void MessageShow(string msg)
        {
            fw_PanelInfo.Visible = true;
            fw_LabelInfo.Text = msg;
        }

        private void MessageHide()
        {
            fw_PanelInfo.Visible = false;
            fw_LabelInfo.Text = "";
        }

        private string _healthcareInitial;
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            switch (Request.QueryString["type"])
            {
                case "tr":
                    ProgramID = Request.QueryString["disch"] == "0" ? AppConstant.Program.ServiceUnitTransaction : AppConstant.Program.ServiceUnitTransactionVerification;
                    break;
                case "jo":
                    ProgramID = Request.QueryString["disch"] == "0" ? AppConstant.Program.JobOrderTransaction : AppConstant.Program.JobOrderTransactionForCashier;
                    break;
                case "ds":
                    ProgramID = Request.QueryString["disch"] == "0" ? AppConstant.Program.DiagnosticSupportTransaction : AppConstant.Program.DiagnosticSupportTransactionVerification;
                    break;
                case "mcu":
                    ProgramID = AppConstant.Program.HealthScreeningTransaction;
                    break;
            }

            if (!IsPostBack)
            {
                _healthcareInitial = AppSession.Parameter.HealthcareInitialAppsVersion;

                //paramedic
                var param = new ParamedicCollection();
                param.Query.Where
                    (
                        param.Query.IsActive == true,
                        param.Query.IsAvailable == true
                    );
                param.Query.OrderBy(param.Query.ParamedicName.Ascending);
                param.LoadAll();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic entity in param)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                }

                var grr = new GuarantorCollection();
                grr.Query.Where(grr.Query.IsActive == true);
                grr.Query.Load();
                cboGuarantorID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var entity in grr)
                {
                    cboGuarantorID.Items.Add(new RadComboBoxItem(entity.GuarantorName, entity.GuarantorID));
                }

                grdList.Columns.FindByUniqueName("openWinTransfer").Visible = Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu"; // consul
                grdList.Columns.FindByUniqueName("onVoidRegistration").Visible = Request.QueryString["type"] == "tr"; // void consul
                grdList.Columns.FindByUniqueName("openWinPrescOrder").Visible = Request.QueryString["type"] == "tr" & false;// AppSession.Parameter.IsUsingPrescriptionOrder; // presc order
                grdList.Columns.FindByUniqueName("openWinPhysicinTeamOp").Visible = Request.QueryString["type"] == "tr" & AppSession.Parameter.IsAllowSubstituteDoctorOnRegistrationOpr; // physician team (ipr) + substitute (opr)
                grdList.Columns.FindByUniqueName("openWinPhysicinTeam").Visible = Request.QueryString["type"] == "tr" & !AppSession.Parameter.IsAllowSubstituteDoctorOnRegistrationOpr; // just physician team (ipr)
                grdList.Columns.FindByUniqueName("PrintMotherChildWristband").Visible = Request.QueryString["type"] == "tr" && _healthcareInitial == "RSSA"; // Mother Child Wristband
                grdList.Columns.FindByUniqueName("PrintPatientLabelRSUI").Visible = Request.QueryString["type"] == "tr" && (_healthcareInitial == "RSUI" || _healthcareInitial == "RSPM"); // Patient Label Diet
                grdList.Columns.FindByUniqueName("PrintPatientStickerRssmcb").Visible = Request.QueryString["type"] == "tr" && AppSession.Parameter.IsShowPrintLabelOnTransEntry; // Patient Sticker
                grdList.Columns.FindByUniqueName("PrintLabelMCURssmcb").Visible = Request.QueryString["type"] == "mcu"; // Sticker MCU
                grdList.Columns.FindByUniqueName("Triage").Visible = Request.QueryString["type"] == "jo" && _healthcareInitial == "RSCH"; // Triage
                grdList.Columns.FindByUniqueName("LinkPhr").Visible = (_healthcareInitial == "RSUI" || _healthcareInitial == "RSPM"); //Request by RSUI
                grdList.Columns.FindByUniqueName("RadNo").Visible = (Request.QueryString["type"] == "ds" && !AppSession.Parameter.IsRadiologyNoAutoCreate);
                grdList.Columns.FindByUniqueName("viewDetailTx").Visible = Request.QueryString["type"] != "mcu";

                trFilterDate.Visible = AppSession.Parameter.TransEnty_ShowFilterDateReg == "Yes";
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_healthcareInitial != "RSSA")
                if (!IsPostBack) RestoreValueFromCookie();

            MessageHide();

            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

                if (Request.QueryString["type"] != "ds" && Request.QueryString["resp"] == "0" && Request.QueryString["disch"] == "0")
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }

                if (Request.QueryString["type"] == "tr" && _healthcareInitial == "RSSA")
                    query.Where(query.IsUsingJobOrder == false);

                if (Request.QueryString["type"] == "mcu")
                    query.Where(
                        query.SRRegistrationType.In(AppConstant.RegistrationType.MedicalCheckUp),
                        query.IsActive == true
                        );
                else
                    query.Where(
                        query.SRRegistrationType.In(
                            AppConstant.RegistrationType.ClusterPatient,
                            AppConstant.RegistrationType.EmergencyPatient,
                            AppConstant.RegistrationType.InPatient,
                            AppConstant.RegistrationType.OutPatient,
                            AppConstant.RegistrationType.MedicalCheckUp
                            ),
                        query.IsActive == true
                        );

                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }
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

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit) {
                grd.DataSource = new String[] { };
                return;
            } 
            
            var dataSource = TransChargess;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable TransChargess
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && txtRegistrationDate.IsEmpty && string.IsNullOrEmpty(cboParamedicID.SelectedValue) &&
                    string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue) && string.IsNullOrEmpty(txtBarcodeEntry.Text);
                if (!ValidateSearch(isEmptyFilter, "Patient")) return null;

                DataTable dtb;
                if (Request.QueryString["type"] == "mcu")
                    dtb = TransChargesMCU;
                else
                {
                    dtb = TransChargesInPatient;

                    dtb.Merge(TransChargesEmergencyPatient);
                    dtb.Merge(TransChargesOutPatient);
                    dtb.Merge(TransChargesMCU);

                    if (Request.QueryString["type"] == "tr")// && Request.QueryString["resp"] == "0" && Request.QueryString["disch"] == "0")
                    {
                        dtb.Merge(TransChargesOperatingTheater);
                        dtb.Merge(TransChargesOperatingTheaterOp);
                        dtb.Merge(TransChargesMCUChild);
                    }
                    else if (Request.QueryString["type"] == "jo")// && Request.QueryString["resp"] == "0" && Request.QueryString["disch"] == "0")
                    {
                        dtb.Merge(TransChargesOperatingTheater);
                        dtb.Merge(TransChargesOperatingTheaterOp);
                    }
                }

                return dtb;
            }
        }

        private DataTable TransChargesEmergencyPatient
        {
            get
            {
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var query = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var sumInfo = new RegistrationInfoSumaryQuery("h");
                var sal = new AppStandardReferenceItemQuery("sal");
                var gdc = new GuarantorDocumentChecklistQuery("gdc");
                var dc = new AppStandardReferenceItemQuery("dc");

                var fileq = new MedicalRecordFileStatusMovementQuery("z");
                query.LeftJoin(fileq).On(
                    query.RegistrationNo == fileq.RegistrationNo &&
                    query.ServiceUnitID == fileq.LastPositionServiceUnitID
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.RoomID,
                        room.RoomName,
                        query.RegistrationDate,
                        @"<0 AS QueNo>",
                        unit.ServiceUnitID,
                        query.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.PatientID,
                        query.IsConsul,
                        @"<'' AS BedID>",
                        query.SRRegistrationType,
                        @"<CASE WHEN h.NoteCount <= 0 THEN NULL ELSE h.NoteCount END AS NoteCount>",
                        @"<CASE WHEN dc.LineNumber IS NULL OR (dc.LineNumber - h.DocumentCheckListCount) <= 0 THEN NULL ELSE (dc.LineNumber - h.DocumentCheckListCount) END AS DocumentCheckListCountRemains>",
                        @"<CAST(0 AS BIT) AS IsInpatient>",
                        //query.SRTriage
                        @"<'' AS SRTriage>",
                        @"<CASE WHEN ISNULL(e.ParamedicID, '')  = '' OR e.ParamedicID = '" + AppSession.Parameter.EmptyDoctorId + @"' OR e.ParamedicID = '" + AppSession.Parameter.DoctorOnDutyId + @"' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                        //@"<CASE WHEN e.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                        "<ISNULL(e.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                        unit.IsNeedConfirmationOfAttendance,
                        @"<'' AS ChargeClassID>",
                        @"<'' AS CoverageClassID>",
                        @"<'' AS ClassID>",
                        @"<'' AS DefaultClassID>",
                        @"<CAST(0 AS BIT) AS BedIsReady>",
                        @"<CASE WHEN ISNULL(c.IsOperatingRoom, 0) = 1 AND ISNULL(c.IsShowOnBookingOT, 0) = 0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsSurgeryRoom >",
                        sal.ItemName.As("SalutationName"),
                        patient.DiagnosticNo,
                        fileq.LastPositionUserID.Coalesce("''"),
                        query.IsHoldTransactionEntry,
                        @"<CAST(1 AS BIT) AS 'IsValidAssessment'>",
                        @"<'' AS FromRegistrationNo>",
                        @"<CAST(1 AS BIT) AS 'IsValidMedicalFileReceived'>",
                        @"<'0' AS ClassSeq1>",
                        @"<'0' AS ClassSeq2>"
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                query.LeftJoin(gdc).On(query.GuarantorID == gdc.GuarantorID & query.SRRegistrationType == gdc.SRRegistrationType);
                query.LeftJoin(dc).On(dc.StandardReferenceID == "DocumentChecklist" & gdc.SRDocumentChecklist == dc.ItemID);

                if (Request.QueryString["type"] != "ds" && Request.QueryString["resp"] == "0" && Request.QueryString["disch"] == "0")
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            query.RegistrationNo == searchReg,
                            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );


                    //query.Where(
                    //    query.Or(
                    //        query.RegistrationNo == searchReg,
                    //        patient.MedicalNo == searchReg,
                    //        patient.OldMedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(f.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //        string.Format("< OR REPLACE(f.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    //Helper.AddFilterMedNoOrRegNoOrPatName(query, patient, searchReg, "registration");
                }

                if (!string.IsNullOrEmpty(txtBarcodeEntry.Text))
                {
                    string searchReg = Helper.EscapeQuery(txtBarcodeEntry.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );



                    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                    //    query.Where(
                    //        query.Or(
                    //            string.Format("< REPLACE(f.MedicalNo, '-', '') LIKE '{0}'>", searchReg),
                    //            string.Format("< OR REPLACE(f.OldMedicalNo, '-', '') LIKE '{0}'>", searchReg)
                    //            )
                    //        );
                    //else
                    //    query.Where(
                    //        query.Or(
                    //            string.Format("< f.MedicalNo LIKE '{0}'>", searchReg),
                    //            string.Format("< OR f.OldMedicalNo LIKE '{0}'>", searchReg)
                    //            )
                    //        );
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                            patient.FullName.Like(searchPatient)
                            //string.Format("<RTRIM(f.FirstName+' '+f.MiddleName)+' '+f.LastName LIKE '{0}'>", searchPatient)
                        );
                }
                if (!txtRegistrationDate.IsEmpty)
                    query.Where(query.RegistrationDate >= txtRegistrationDate.SelectedDate, query.RegistrationDate < txtRegistrationDate.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    query.Where(query.GuarantorID == cboGuarantorID.SelectedValue);

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));
                query.Where
                    (
                        query.IsClosed == false,
                        query.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient,
                        //query.IsHoldTransactionEntry == false,
                        query.IsVoid == false,
                        query.IsFromDispensary == false,
                        query.IsNonPatient == false
                    );

                query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationNo.Ascending
                    );

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var rim = new RegistrationInfoMedicCollection();
                    rim.Query.Where(rim.Query.RegistrationNo == row["RegistrationNo"].ToString(),
                                    rim.Query.Or(rim.Query.IsDeleted.IsNull(), rim.Query.IsDeleted == false));
                    rim.LoadAll();
                    if (rim.Count > 0) row["SRTriage"] = "99";

                    if (Request.QueryString["type"] == "jo" & AppParameter.IsYes(AppParameter.ParameterItem.IsExamOrderEmrMustAssessmentFirst))
                    {
                        row["IsValidAssessment"] = rim.Count > 0;
                    }

                    if (AppSession.Parameter.IsValidatedMedicalFileReceived && Request.QueryString["type"] != "ds" && Convert.ToBoolean(row["IsNewVisible"]))
                    {
                        var file = new MedicalRecordFileStatusMovement();
                        if (!file.LoadByPrimaryKey(row["ServiceUnitID"].ToString(), row["RegistrationNo"].ToString()))
                            row["IsValidMedicalFileReceived"] = false;
                    }
                }
                dtb.AcceptChanges();

                return dtb;
            }
        }

        private DataTable TransChargesOutPatient
        {
            get
            {
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var query = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var sumInfo = new RegistrationInfoSumaryQuery("h");
                var sal = new AppStandardReferenceItemQuery("sal");
                var gdc = new GuarantorDocumentChecklistQuery("gdc");
                var dc = new AppStandardReferenceItemQuery("dc");

                var fileq = new MedicalRecordFileStatusMovementQuery("z");
                query.LeftJoin(fileq).On(
                    query.RegistrationNo == fileq.RegistrationNo &&
                    query.ServiceUnitID == fileq.LastPositionServiceUnitID
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (AppParameter.IsYes(AppParameter.ParameterItem.IsUsingKioskQueNoFormat)) //Untuk RSI Tegal , menyesuaikan format QueNo seperti di Menu Registrasi
                {
                    var appt = new AppointmentQuery("appt");
                    query.LeftJoin(appt).On(appt.AppointmentNo == query.AppointmentNo);
                    var appttype = new AppStandardReferenceItemQuery("appttype");
                    query.LeftJoin(appttype).On(appttype.StandardReferenceID == "AppoinmentType" && appttype.ItemID == appt.SRAppoinmentType);
                    var apptQue = new AppointmentQueueingQuery("apptQue");
                    query.LeftJoin(apptQue).On(appt.AppointmentNo == apptQue.AppointmentNo && apptQue.SRQueueingGroup == "01");
                    query.Select(
                        @"<apptQue.FormattedNo>", 
                        @"<e.RegistrationQue AS QueNo>"
                    );
                }
                else
                {
                    query.Select(@"<'' AS FormattedNo>");
                    query.Select(@"<e.RegistrationQue AS QueNo>");
                }

                query.Select
                    (
                        query.RoomID,
                        room.RoomName,
                        query.RegistrationDate,
                        //@"<e.RegistrationQue AS QueNo>",
                        unit.ServiceUnitID,
                        query.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.PatientID,
                        query.IsConsul,
                        @"<'' AS BedID>",
                        query.SRRegistrationType,
                        @"<CASE WHEN h.NoteCount <= 0 THEN NULL ELSE h.NoteCount END AS NoteCount>",
                        @"<CASE WHEN dc.LineNumber IS NULL OR (dc.LineNumber - h.DocumentCheckListCount) <= 0 THEN NULL ELSE (dc.LineNumber - h.DocumentCheckListCount) END AS DocumentCheckListCountRemains>",
                        @"<CASE WHEN e.ServiceUnitID = '" + AppSession.Parameter.ServiceUnitVkId + @"' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsInpatient>",
                        @"<'' AS SRTriage>",
                        //@"<CASE WHEN ISNULL(e.ParamedicID, '')  = '' OR e.ParamedicID = '" + AppSession.Parameter.EmptyDoctorId + @"' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                        //@"<CASE WHEN e.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                        @"<CAST(1 AS BIT) AS IsNewVisible>",
                        @"<ISNULL(e.ParamedicID, '') AS RegParamedicID>",
                        @"<ISNULL(e.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                        unit.IsNeedConfirmationOfAttendance,
                        @"<'' AS ChargeClassID>",
                        @"<'' AS CoverageClassID>",
                        @"<'' AS ClassID>",
                        @"<'' AS DefaultClassID>",
                        @"<CAST(0 AS BIT) AS BedIsReady>",
                        @"<CASE WHEN ISNULL(c.IsOperatingRoom, 0) = 1 AND ISNULL(c.IsShowOnBookingOT, 0) = 0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsSurgeryRoom >",
                        sal.ItemName.As("SalutationName"),
                        patient.DiagnosticNo,
                        fileq.LastPositionUserID.Coalesce("''"),
                        query.IsHoldTransactionEntry,
                        @"<CAST(1 AS BIT) AS 'IsValidAssessment'>",
                        unit.IsUsingJobOrder,
                        @"<CASE WHEN e.IsConsul = 1 THEN '**' + e.FromRegistrationNo ELSE '' END AS FromRegistrationNo>",
                        query.FromRegistrationNo.As("fRegno"),
                        @"<CAST(1 AS BIT) AS 'IsValidMedicalFileReceived'>",
                        @"<'0' AS ClassSeq1>",
                        @"<'0' AS ClassSeq2>"
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                query.LeftJoin(gdc).On(query.GuarantorID == gdc.GuarantorID & query.SRRegistrationType == gdc.SRRegistrationType);
                query.LeftJoin(dc).On(dc.StandardReferenceID == "DocumentChecklist" & gdc.SRDocumentChecklist == dc.ItemID);

                if (Request.QueryString["type"] != "ds" && Request.QueryString["resp"] == "0" && Request.QueryString["disch"] == "0")
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            query.RegistrationNo == searchReg,
                            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );


                    //query.Where(
                    //    query.Or(
                    //        query.RegistrationNo == searchReg,
                    //        patient.MedicalNo == searchReg,
                    //        patient.OldMedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(f.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //        string.Format("< OR REPLACE(f.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    //Helper.AddFilterMedNoOrRegNoOrPatName(query, patient, searchReg, "registration");
                }

                if (!string.IsNullOrEmpty(txtBarcodeEntry.Text))
                {
                    string searchReg = Helper.EscapeQuery(txtBarcodeEntry.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );

                    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                    //    query.Where(
                    //        query.Or(
                    //            string.Format("< REPLACE(f.MedicalNo, '-', '') LIKE '{0}'>", searchReg),
                    //            string.Format("< OR REPLACE(f.OldMedicalNo, '-', '') LIKE '{0}'>", searchReg)
                    //            )
                    //        );
                    //else
                    //    query.Where(
                    //        query.Or(
                    //            string.Format("< f.MedicalNo LIKE '{0}'>", searchReg),
                    //            string.Format("< OR f.OldMedicalNo LIKE '{0}'>", searchReg)
                    //            )
                    //        );
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                            patient.FullName.Like(searchPatient)
                            //string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (!txtRegistrationDate.IsEmpty)
                    query.Where(query.RegistrationDate >= txtRegistrationDate.SelectedDate, query.RegistrationDate < txtRegistrationDate.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    query.Where(query.GuarantorID == cboGuarantorID.SelectedValue);

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));
                query.Where
                    (
                        query.IsClosed == false,
                        query.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
                        //query.IsHoldTransactionEntry == false,
                        query.IsVoid == false,
                        query.IsFromDispensary == false,
                        query.IsNonPatient == false
                    );

                query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationNo.Ascending
                    );

                DataTable dtb = query.LoadDataTable();

                var aps = new string[4];
                aps.SetValue(AppSession.Parameter.ServiceUnitLaboratoryID, 0);
                aps.SetValue(AppSession.Parameter.ServiceUnitRadiologyID, 1);
                aps.SetValue(AppSession.Parameter.ServiceUnitRadiologyID2, 2);
                aps.SetValue(AppSession.Parameter.ServiceUnitMedicalRehabId, 3);

                var aps2 = AppSession.Parameter.ServiceUnitLaboratoryIdArray;

                foreach (DataRow row in dtb.Rows)
                {
                    if (row["RegParamedicID"].ToString() == string.Empty || AppSession.Parameter.EmptyDoctorId.Contains(row["RegParamedicID"].ToString()) || row["RegParamedicID"].ToString() == AppSession.Parameter.DoctorOnDutyId)
                    {
                        row["IsNewVisible"] = false;
                    }

                    var rim = new RegistrationInfoMedicCollection();
                    rim.Query.Where(rim.Query.Or(rim.Query.RegistrationNo == row["RegistrationNo"].ToString(), rim.Query.RegistrationNo == row["fRegno"].ToString()),
                                    rim.Query.Or(rim.Query.IsDeleted.IsNull(), rim.Query.IsDeleted == false));
                    rim.LoadAll();
                    if (rim.Count > 0)
                        row["SRTriage"] = "99";

                    if (Request.QueryString["type"] == "jo" & !Convert.ToBoolean(row["IsUsingJobOrder"]) & AppParameter.IsYes(AppParameter.ParameterItem.IsExamOrderOprMustAssessmentFirst))
                    {
                        if (!aps.Contains(row["ServiceUnitID"].ToString()) && !aps2.Contains(row["ServiceUnitID"].ToString())) 
                            row["IsValidAssessment"] = rim.Count > 0;
                    }

                    if ((AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH") && Request.QueryString["type"] != "tr" && Convert.ToBoolean(row["IsNeedConfirmationOfAttendance"]) && Convert.ToBoolean(row["IsConfirmedAttendance"]) == false)
                        row.Delete();
                    else if (AppSession.Parameter.IsValidatedMedicalFileReceived && Request.QueryString["type"] != "ds" && Convert.ToBoolean(row["IsNewVisible"]))
                    {
                        var file = new MedicalRecordFileStatusMovement();
                        if (!file.LoadByPrimaryKey(row["ServiceUnitID"].ToString(), row["RegistrationNo"].ToString()))
                            row["IsValidMedicalFileReceived"] = false;
                    }
                }
                dtb.AcceptChanges();

                return dtb;
            }
        }

        private DataTable TransChargesInPatient
        {
            get
            {
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var query = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var sumInfo = new RegistrationInfoSumaryQuery("h");
                var sal = new AppStandardReferenceItemQuery("sal");
                var gdc = new GuarantorDocumentChecklistQuery("gdc");
                var dc = new AppStandardReferenceItemQuery("dc");
                var bd = new BedQuery("bd");
                var cl = new ClassQuery("cl");
                var cl2 = new ClassQuery("cl2");

                var fileq = new MedicalRecordFileStatusMovementQuery("z");
                query.LeftJoin(fileq).On(
                    query.RegistrationNo == fileq.RegistrationNo &&
                    query.ServiceUnitID == fileq.LastPositionServiceUnitID
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.RoomID,
                        room.RoomName,
                        query.RegistrationDate,
                        @"<0 AS QueNo>",
                        unit.ServiceUnitID,
                        query.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.PatientID,
                        query.IsConsul,
                        query.BedID,
                        query.SRRegistrationType,
                        @"<CASE WHEN h.NoteCount <= 0 THEN NULL ELSE h.NoteCount END AS NoteCount>",
                        @"<CASE WHEN dc.LineNumber IS NULL OR (dc.LineNumber - h.DocumentCheckListCount) <= 0 THEN NULL ELSE (dc.LineNumber - h.DocumentCheckListCount) END AS DocumentCheckListCountRemains>",
                        @"<CAST(1 AS BIT) AS IsInpatient>",
                        @"<'' AS SRTriage>",
                        @"<CAST(1 AS BIT) AS 'IsNewVisible'>",
                        //@"<CASE WHEN e.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                        @"<ISNULL(e.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                        unit.IsNeedConfirmationOfAttendance,
                        query.IsRoomIn,
                        query.ChargeClassID,
                        query.CoverageClassID,
                        query.ClassID,
                        @"<ISNULL(bd.DefaultChargeClassID, e.ChargeClassID) AS DefaultClassID>",
                        @"<CAST(0 AS BIT) AS BedIsReady>",
                        @"<CASE WHEN ISNULL(c.IsOperatingRoom, 0) = 1 AND ISNULL(c.IsShowOnBookingOT, 0) = 0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsSurgeryRoom >",
                        sal.ItemName.As("SalutationName"), patient.DiagnosticNo,
                        @"<CASE WHEN e.DischargeDate IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsDischarge>",
                        fileq.LastPositionUserID.Coalesce("''"),
                        query.IsHoldTransactionEntry,
                        @"<CAST(1 AS BIT) AS 'IsValidAssessment'>",
                        @"<'' AS FromRegistrationNo>",
                        @"<CAST(1 AS BIT) AS 'IsValidMedicalFileReceived'>",
                        @"<CAST(cl.ClassSeq AS VARCHAR) AS ClassSeq1>",
                        @"<CAST(cl2.ClassSeq AS VARCHAR) AS ClassSeq2>"
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                query.LeftJoin(gdc).On(query.GuarantorID == gdc.GuarantorID & query.SRRegistrationType == gdc.SRRegistrationType);
                query.LeftJoin(dc).On(dc.StandardReferenceID == "DocumentChecklist" & gdc.SRDocumentChecklist == dc.ItemID);
                query.LeftJoin(bd).On(bd.BedID == query.BedID);
                query.LeftJoin(cl).On(cl.ClassID == query.ChargeClassID);
                query.LeftJoin(cl2).On(cl2.ClassID == query.CoverageClassID);

                if (Request.QueryString["type"] != "ds" && Request.QueryString["resp"] == "0" && Request.QueryString["disch"] == "0")
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            query.RegistrationNo == searchReg,
                            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );

                    //query.Where(
                    //    query.Or(query.RegistrationNo == searchReg,
                    //             patient.MedicalNo == searchReg,
                    //             patient.OldMedicalNo == searchReg,
                    //             string.Format("< OR REPLACE(f.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //             string.Format("< OR REPLACE(f.OldMedicalNo, '-', '') LIKE '%{0}%'>",
                    //                           searchReg)));
                    //Helper.AddFilterMedNoOrRegNoOrPatName(query, patient, searchReg, "registration");
                }

                if (!string.IsNullOrEmpty(txtBarcodeEntry.Text))
                {
                    string searchReg = Helper.EscapeQuery(txtBarcodeEntry.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );

                    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                    //    query.Where(
                    //        query.Or(
                    //            string.Format("< REPLACE(f.MedicalNo, '-', '') LIKE '{0}'>", searchReg),
                    //            string.Format("< OR REPLACE(f.OldMedicalNo, '-', '') LIKE '{0}'>", searchReg)
                    //            )
                    //        );
                    //else
                    //    query.Where(
                    //        query.Or(
                    //            string.Format("< f.MedicalNo LIKE '{0}'>", searchReg),
                    //            string.Format("< OR f.OldMedicalNo LIKE '{0}'>", searchReg)
                    //            )
                    //        );
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                            patient.FullName.Like(searchPatient)
                            //string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (!txtRegistrationDate.IsEmpty)
                    query.Where(query.RegistrationDate >= txtRegistrationDate.SelectedDate, query.RegistrationDate < txtRegistrationDate.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    query.Where(query.GuarantorID == cboGuarantorID.SelectedValue);

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));
                query.Where
                    (
                        query.IsClosed == false,
                        query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        //query.IsHoldTransactionEntry == false,
                        query.IsVoid == false,
                        query.IsFromDispensary == false,
                        query.IsNonPatient == false
                    );

                if (Request.QueryString["disch"] == "0")
                    query.Where(query.DischargeDate.IsNull());

                query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationNo.Ascending
                    );

                DataTable dtbl = query.LoadDataTable();

                if (Request.QueryString["disch"] == "0")
                {
                    foreach (DataRow row in dtbl.Rows)
                    {
                        bool isRowDelete = false;

                        if (Convert.ToBoolean(row["IsRoomIn"]) == false)
                        {
                            var bed = new Bed();
                            if (bed.LoadByPrimaryKey(row["BedID"].ToString()))
                            {
                                if (bed.IsNeedConfirmation == true && bed.SRBedStatus == AppSession.Parameter.BedStatusPending)
                                    isRowDelete = true;
                            }
                        }
                        else
                        {
                            var bedRoomingInColl = new BedRoomInCollection();
                            var bedRoomingInQ = new BedRoomInQuery("a");
                            var bedQ = new BedQuery("b");
                            bedRoomingInQ.InnerJoin(bedQ).On(bedRoomingInQ.BedID == bedQ.BedID && bedQ.IsNeedConfirmation == true);
                            bedRoomingInQ.Where(bedRoomingInQ.RegistrationNo == row["RegistrationNo"].ToString() &&
                                                     bedRoomingInQ.BedID == row["BedID"].ToString() &&
                                                     bedRoomingInQ.SRBedStatus == AppSession.Parameter.BedStatusPending);
                            bedRoomingInColl.Load(bedRoomingInQ);
                            if (bedRoomingInColl.Count > 0)
                                isRowDelete = true;
                        }

                        if (isRowDelete)
                            row.Delete();
                        else
                        {
                            if (row["ChargeClassID"].ToString() != row["DefaultClassID"].ToString())
                            {
                                var c = new Class();
                                c.LoadByPrimaryKey(row["DefaultClassID"].ToString());
                                if (c.IsTariffClass ?? false)
                                {
                                    var bedReadys = new BedCollection();
                                    bedReadys.Query.Where(bedReadys.Query.DefaultChargeClassID == row["ChargeClassID"].ToString(),
                                                          bedReadys.Query.SRBedStatus ==
                                                          AppSession.Parameter.BedStatusUnoccupied,
                                                          bedReadys.Query.IsActive == true);
                                    bedReadys.LoadAll();
                                    if (bedReadys.Count > 0)
                                        row["BedIsReady"] = true;
                                }
                            }

                            if (Request.QueryString["type"] == "jo" & AppParameter.IsYes(AppParameter.ParameterItem.IsExamOrderIprMustAssessmentFirst))
                            {
                                var rim = new RegistrationInfoMedicCollection();
                                rim.Query.Where(rim.Query.RegistrationNo == row["RegistrationNo"].ToString(),
                                                rim.Query.Or(rim.Query.IsDeleted.IsNull(), rim.Query.IsDeleted == false));
                                rim.LoadAll();

                                row["IsValidAssessment"] = rim.Count > 0;
                            }

                            if (AppSession.Parameter.IsValidatedMedicalFileReceived && Request.QueryString["type"] != "ds")
                            {
                                var file = new MedicalRecordFileStatusMovement();
                                if (!file.LoadByPrimaryKey(row["ServiceUnitID"].ToString(), row["RegistrationNo"].ToString()))
                                    row["IsValidMedicalFileReceived"] = false;
                            }
                        }
                    }
                    dtbl.AcceptChanges();
                }
                else
                {
                    foreach (DataRow row in dtbl.Rows)
                    {
                        bool isDischarge = Convert.ToBoolean(row["IsDischarge"]);
                        if (isDischarge == false)
                        {
                            bool isRowDelete = false;

                            if (Convert.ToBoolean(row["IsRoomIn"]) == false)
                            {
                                var bed = new Bed();
                                if (bed.LoadByPrimaryKey(row["BedID"].ToString()))
                                {
                                    if (bed.IsNeedConfirmation == true && bed.SRBedStatus == AppSession.Parameter.BedStatusPending)
                                        isRowDelete = true;
                                }
                            }
                            else
                            {
                                var bedRoomingInColl = new BedRoomInCollection();
                                var bedRoomingInQ = new BedRoomInQuery("a");
                                var bedQ = new BedQuery("b");
                                bedRoomingInQ.InnerJoin(bedQ).On(bedRoomingInQ.BedID == bedQ.BedID && bedQ.IsNeedConfirmation == true);
                                bedRoomingInQ.Where(bedRoomingInQ.RegistrationNo == row["RegistrationNo"].ToString() &&
                                                         bedRoomingInQ.BedID == row["BedID"].ToString() &&
                                                         bedRoomingInQ.SRBedStatus == AppSession.Parameter.BedStatusPending);
                                bedRoomingInColl.Load(bedRoomingInQ);
                                if (bedRoomingInColl.Count > 0)
                                    isRowDelete = true;
                            }

                            if (isRowDelete)
                                row.Delete();
                            else
                            {
                                if (row["ChargeClassID"].ToString() != row["DefaultClassID"].ToString())
                                {
                                    var c = new Class();
                                    c.LoadByPrimaryKey(row["DefaultClassID"].ToString());
                                    if (c.IsTariffClass ?? false)
                                    {
                                        var bedReadys = new BedCollection();
                                        bedReadys.Query.Where(bedReadys.Query.DefaultChargeClassID == row["ChargeClassID"].ToString(),
                                                              bedReadys.Query.SRBedStatus ==
                                                              AppSession.Parameter.BedStatusUnoccupied,
                                                              bedReadys.Query.IsActive == true);
                                        bedReadys.LoadAll();
                                        if (bedReadys.Count > 0)
                                            row["BedIsReady"] = true;
                                    }
                                }

                                if (Request.QueryString["type"] == "jo" & AppParameter.IsYes(AppParameter.ParameterItem.IsExamOrderIprMustAssessmentFirst))
                                {
                                    var rim = new RegistrationInfoMedicCollection();
                                    rim.Query.Where(rim.Query.RegistrationNo == row["RegistrationNo"].ToString(),
                                                    rim.Query.Or(rim.Query.IsDeleted.IsNull(), rim.Query.IsDeleted == false));
                                    rim.LoadAll();

                                    row["IsValidAssessment"] = rim.Count > 0;
                                }

                                if (AppSession.Parameter.IsValidatedMedicalFileReceived && Request.QueryString["type"] != "ds")
                                {
                                    var file = new MedicalRecordFileStatusMovement();
                                    if (!file.LoadByPrimaryKey(row["ServiceUnitID"].ToString(), row["RegistrationNo"].ToString()))
                                        row["IsValidMedicalFileReceived"] = false;
                                }
                            }
                        }
                    }
                    dtbl.AcceptChanges();
                }

                return dtbl;
            }
        }

        private DataTable TransChargesOperatingTheater
        {
            get
            {
                var query = new ServiceUnitBookingQuery("a");
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var reg = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var sumInfo = new RegistrationInfoSumaryQuery("h");
                var sal = new AppStandardReferenceItemQuery("sal");
                var gdc = new GuarantorDocumentChecklistQuery("gdc");
                var dc = new AppStandardReferenceItemQuery("dc");
                var cl = new ClassQuery("cl");
                var cl2 = new ClassQuery("cl2");

                var fileq = new MedicalRecordFileStatusMovementQuery("z");
                query.LeftJoin(fileq).On(
                    query.RegistrationNo == fileq.RegistrationNo &&
                    query.ServiceUnitID == fileq.LastPositionServiceUnitID
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.es.Distinct = true;

                query.Select
                    (
                        query.RoomID,
                        room.RoomName,
                        reg.RegistrationDate,
                        @"<1 AS QueNo>",
                        unit.ServiceUnitID,
                        query.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        patient.PatientID,
                        reg.IsConsul,
                        @"<'' AS BedID>",
                        reg.SRRegistrationType,
                        @"<CASE WHEN h.NoteCount <= 0 THEN NULL ELSE h.NoteCount END AS NoteCount>",
                        @"<CASE WHEN dc.LineNumber IS NULL OR (dc.LineNumber - h.DocumentCheckListCount) <= 0 THEN NULL ELSE (dc.LineNumber - h.DocumentCheckListCount) END AS DocumentCheckListCountRemains>",
                        @"<CAST(1 AS BIT) AS IsInpatient>",
                        @"<'' AS SRTriage>",
                        @"<CAST(1 AS BIT) AS 'IsNewVisible'>",
                        //@"<CASE WHEN e.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                        @"<ISNULL(e.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                        unit.IsNeedConfirmationOfAttendance,
                        reg.ChargeClassID,
                        reg.CoverageClassID,
                        reg.ClassID,
                        reg.ChargeClassID.As("DefaultClassID"),
                        @"<CAST(0 AS BIT) AS BedIsReady>",
                        @"<CAST(0 AS BIT) AS IsSurgeryRoom >",
                        sal.ItemName.As("SalutationName"),
                        patient.DiagnosticNo,
                        fileq.LastPositionUserID.Coalesce("''"),
                        //@"<CASE WHEN e.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                        reg.IsHoldTransactionEntry,
                        @"<CAST(1 AS BIT) AS 'IsValidAssessment'>",
                        @"<'' AS FromRegistrationNo>",
                        @"<CAST(1 AS BIT) AS 'IsValidMedicalFileReceived'>",
                        @"<CAST(cl.ClassSeq AS VARCHAR) AS ClassSeq1>",
                        @"<CAST(cl2.ClassSeq AS VARCHAR) AS ClassSeq2>"
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                query.LeftJoin(gdc).On(reg.GuarantorID == gdc.GuarantorID & reg.SRRegistrationType == gdc.SRRegistrationType);
                query.LeftJoin(dc).On(dc.StandardReferenceID == "DocumentChecklist" & gdc.SRDocumentChecklist == dc.ItemID);
                query.LeftJoin(cl).On(cl.ClassID == reg.ChargeClassID);
                query.LeftJoin(cl2).On(cl2.ClassID == reg.CoverageClassID);

                if (Request.QueryString["type"] != "ds" && Request.QueryString["resp"] == "0" && Request.QueryString["disch"] == "0")
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            query.RegistrationNo == searchReg,
                            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );

                    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                    //    query.Where(
                    //        query.Or(
                    //            query.RegistrationNo == searchReg,
                    //            patient.MedicalNo == searchReg,
                    //            patient.OldMedicalNo == searchReg,
                    //            string.Format("< OR REPLACE(f.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //            string.Format("< OR REPLACE(f.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                    //else
                    //    query.Where(
                    //        query.Or(
                    //            query.RegistrationNo == searchReg,
                    //            patient.MedicalNo == searchReg,
                    //            patient.OldMedicalNo == searchReg,
                    //            string.Format("< OR f.MedicalNo LIKE '%{0}%'>", searchReg),
                    //            string.Format("< OR f.OldMedicalNo LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                }

                if (!string.IsNullOrEmpty(txtBarcodeEntry.Text))
                {
                    string searchReg = Helper.EscapeQuery(txtBarcodeEntry.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );


                    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                    //    query.Where(
                    //        query.Or(
                    //            string.Format("< REPLACE(f.MedicalNo, '-', '') LIKE '{0}'>", searchReg),
                    //            string.Format("< OR REPLACE(f.OldMedicalNo, '-', '') LIKE '{0}'>", searchReg)
                    //            )
                    //        );
                    //else
                    //    query.Where(
                    //        query.Or(
                    //            string.Format("< f.MedicalNo LIKE '{0}'>", searchReg),
                    //            string.Format("< OR f.OldMedicalNo LIKE '{0}'>", searchReg)
                    //            )
                    //        );
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                            patient.FullName.Like(searchPatient)
                            //string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (!txtRegistrationDate.IsEmpty)
                    query.Where(reg.RegistrationDate >= txtRegistrationDate.SelectedDate, reg.RegistrationDate < txtRegistrationDate.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    query.Where(reg.GuarantorID == cboGuarantorID.SelectedValue);

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));
                query.Where
                    (
                        reg.IsClosed == false,
                        reg.IsVoid == false,
                        //reg.IsHoldTransactionEntry == false,
                        query.IsApproved == true,
                        reg.IsFromDispensary == false,
                        reg.IsNonPatient == false,
                        reg.SRRegistrationType == AppConstant.RegistrationType.InPatient
                    );
                query.Where(
                    string.Format(
                        "<ISNULL(c.IsOperatingRoom, 0) = 1 AND ISNULL(c.IsShowOnBookingOT, 0) = 1>"));
                if (Request.QueryString["disch"] == "0")
                    query.Where(reg.DischargeDate.IsNull());
                query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationNo.Ascending
                    );

                return query.LoadDataTable();
            }
        }

        private DataTable TransChargesOperatingTheaterOp
        {
            get
            {
                var query = new ServiceUnitBookingQuery("a");
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var reg = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var sumInfo = new RegistrationInfoSumaryQuery("h");
                var sal = new AppStandardReferenceItemQuery("sal");
                var gdc = new GuarantorDocumentChecklistQuery("gdc");
                var dc = new AppStandardReferenceItemQuery("dc");

                var fileq = new MedicalRecordFileStatusMovementQuery("z");
                query.LeftJoin(fileq).On(
                    query.RegistrationNo == fileq.RegistrationNo &&
                    query.ServiceUnitID == fileq.LastPositionServiceUnitID
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.es.Distinct = true;

                query.Select
                    (
                        query.RoomID,
                        room.RoomName,
                        reg.RegistrationDate,
                        "<1 AS QueNo>",
                        unit.ServiceUnitID,
                        query.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        patient.PatientID,
                        reg.IsConsul,
                        "<'' AS BedID>",
                        reg.SRRegistrationType,
                        @"<CASE WHEN h.NoteCount <= 0 THEN NULL ELSE h.NoteCount END AS NoteCount>",
                        @"<CASE WHEN dc.LineNumber IS NULL OR (dc.LineNumber - h.DocumentCheckListCount) <= 0 THEN NULL ELSE (dc.LineNumber - h.DocumentCheckListCount) END AS DocumentCheckListCountRemains>",
                        @"<CASE WHEN e.ServiceUnitID = '" + AppSession.Parameter.ServiceUnitVkId + @"' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsInpatient>",
                        "<'' AS SRTriage>",
                        @"<CASE WHEN ISNULL(e.ParamedicID, '')  = '' OR e.ParamedicID = '" + AppSession.Parameter.EmptyDoctorId + @"' OR e.ParamedicID = '" + AppSession.Parameter.DoctorOnDutyId + @"' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                        //@"<CASE WHEN e.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                        "<ISNULL(e.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                        unit.IsNeedConfirmationOfAttendance,
                        reg.ChargeClassID,
                        reg.CoverageClassID,
                        reg.ClassID,
                        reg.ChargeClassID.As("DefaultClassID"),
                        "<CAST(0 AS BIT) AS BedIsReady>",
                        @"<CAST(0 AS BIT) AS IsSurgeryRoom >",
                        sal.ItemName.As("SalutationName"),
                        patient.DiagnosticNo,
                        fileq.LastPositionUserID.Coalesce("''"),
                        reg.IsHoldTransactionEntry,
                        @"<CAST(1 AS BIT) AS 'IsValidAssessment'>",
                        @"<'' AS FromRegistrationNo>",
                        @"<CAST(1 AS BIT) AS 'IsValidMedicalFileReceived'>",
                        @"<'0' AS ClassSeq1>",
                        @"<'0' AS ClassSeq2>"
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                query.LeftJoin(gdc).On(reg.GuarantorID == gdc.GuarantorID & reg.SRRegistrationType == gdc.SRRegistrationType);
                query.LeftJoin(dc).On(dc.StandardReferenceID == "DocumentChecklist" & gdc.SRDocumentChecklist == dc.ItemID);

                if (Request.QueryString["type"] != "ds" && Request.QueryString["resp"] == "0" && Request.QueryString["disch"] == "0")
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            query.RegistrationNo == searchReg,
                            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );

                    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                    //    query.Where(
                    //        query.Or(
                    //            query.RegistrationNo == searchReg,
                    //            patient.MedicalNo == searchReg,
                    //            patient.OldMedicalNo == searchReg,
                    //            string.Format("< OR REPLACE(f.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //            string.Format("< OR REPLACE(f.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                    //else
                    //    query.Where(
                    //        query.Or(
                    //            query.RegistrationNo == searchReg,
                    //            patient.MedicalNo == searchReg,
                    //            patient.OldMedicalNo == searchReg,
                    //            string.Format("< OR f.MedicalNo LIKE '%{0}%'>", searchReg),
                    //            string.Format("< OR f.OldMedicalNo LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                }

                if (!string.IsNullOrEmpty(txtBarcodeEntry.Text))
                {
                    string searchReg = Helper.EscapeQuery(txtBarcodeEntry.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );

                    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                    //    query.Where(
                    //        query.Or(
                    //            string.Format("< REPLACE(f.MedicalNo, '-', '') LIKE '{0}'>", searchReg),
                    //            string.Format("< OR REPLACE(f.OldMedicalNo, '-', '') LIKE '{0}'>", searchReg)
                    //            )
                    //        );
                    //else
                    //    query.Where(
                    //        query.Or(
                    //            string.Format("< f.MedicalNo LIKE '{0}'>", searchReg),
                    //            string.Format("< OR f.OldMedicalNo LIKE '{0}'>", searchReg)
                    //            )
                    //        );
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                            patient.FullName.Like(searchPatient)
                            //string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (!txtRegistrationDate.IsEmpty)
                    query.Where(reg.RegistrationDate >= txtRegistrationDate.SelectedDate, reg.RegistrationDate < txtRegistrationDate.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    query.Where(reg.GuarantorID == cboGuarantorID.SelectedValue);

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));
                query.Where
                    (
                        reg.IsClosed == false,
                        reg.IsVoid == false,
                        //reg.IsHoldTransactionEntry == false,
                        query.IsApproved == true,
                        reg.IsNonPatient == false,
                        reg.IsFromDispensary == false,
                        reg.SRRegistrationType != AppConstant.RegistrationType.InPatient
                    );
                query.Where(
                    string.Format(
                        "<ISNULL(c.IsOperatingRoom, 0) = 1 AND ISNULL(c.IsShowOnBookingOT, 0) = 1>"));
                query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationNo.Ascending
                    );

                return query.LoadDataTable();
            }
        }

        private DataTable TransChargesMCU
        {
            get
            {
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var query = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var sumInfo = new RegistrationInfoSumaryQuery("h");
                var sal = new AppStandardReferenceItemQuery("sal");
                var gdc = new GuarantorDocumentChecklistQuery("gdc");
                var dc = new AppStandardReferenceItemQuery("dc");

                var fileq = new MedicalRecordFileStatusMovementQuery("z");
                query.LeftJoin(fileq).On(
                    query.RegistrationNo == fileq.RegistrationNo &&
                    query.ServiceUnitID == fileq.LastPositionServiceUnitID
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (AppParameter.IsYes(AppParameter.ParameterItem.IsUsingKioskQueNoFormat)) //Untuk RSI Tegal , menyesuaikan format QueNo seperti di Menu Registrasi
                {
                    var appt = new AppointmentQuery("appt");
                    query.LeftJoin(appt).On(appt.AppointmentNo == query.AppointmentNo);
                    var appttype = new AppStandardReferenceItemQuery("appttype");
                    query.LeftJoin(appttype).On(appttype.StandardReferenceID == "AppoinmentType" && appttype.ItemID == appt.SRAppoinmentType);
                    var apptQue = new AppointmentQueueingQuery("apptQue");
                    query.LeftJoin(apptQue).On(appt.AppointmentNo == apptQue.AppointmentNo && apptQue.SRQueueingGroup == "01");
                    query.Select(
                        @"<apptQue.FormattedNo>",
                        @"<e.RegistrationQue AS QueNo>"
                    );
                }
                else
                {
                    query.Select(@"<'' AS FormattedNo>");
                    query.Select(@"<e.RegistrationQue AS QueNo>");
                }

                query.Select
                    (
                        query.RoomID,
                        room.RoomName,
                        query.RegistrationDate,
                        //"<e.RegistrationQue AS QueNo>",
                        unit.ServiceUnitID,
                        query.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.PatientID,
                        query.IsConsul,
                        "<'' AS BedID>",
                        query.SRRegistrationType,
                        @"<CASE WHEN h.NoteCount <= 0 THEN NULL ELSE h.NoteCount END AS NoteCount>",
                        @"<CASE WHEN dc.LineNumber IS NULL OR (dc.LineNumber - h.DocumentCheckListCount) <= 0 THEN NULL ELSE (dc.LineNumber - h.DocumentCheckListCount) END AS DocumentCheckListCountRemains>",
                        "<CAST(0 AS BIT) AS IsInpatient>",
                        "<'' AS SRTriage>",
                        @"<CASE WHEN ISNULL(e.ParamedicID, '')  = '' OR e.ParamedicID = '" + AppSession.Parameter.EmptyDoctorId + @"' OR e.ParamedicID = '" + AppSession.Parameter.DoctorOnDutyId + @"' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                        //@"<CASE WHEN e.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                        "<ISNULL(e.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                        unit.IsNeedConfirmationOfAttendance,
                        @"<'' AS ChargeClassID>",
                        @"<'' AS CoverageClassID>",
                        @"<'' AS ClassID>",
                        @"<'' AS DefaultClassID>",
                        "<CAST(0 AS BIT) AS BedIsReady>",
                        @"<CAST(0 AS BIT) AS IsSurgeryRoom >",
                        sal.ItemName.As("SalutationName"),
                        patient.DiagnosticNo,
                        fileq.LastPositionUserID.Coalesce("''"),
                        query.IsHoldTransactionEntry,
                        @"<CAST(1 AS BIT) AS 'IsValidAssessment'>",
                        @"<'' AS FromRegistrationNo>",
                        @"<CAST(1 AS BIT) AS 'IsValidMedicalFileReceived'>",
                        @"<'0' AS ClassSeq1>",
                        @"<'0' AS ClassSeq2>"
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                query.LeftJoin(gdc).On(query.GuarantorID == gdc.GuarantorID & query.SRRegistrationType == gdc.SRRegistrationType);
                query.LeftJoin(dc).On(dc.StandardReferenceID == "DocumentChecklist" & gdc.SRDocumentChecklist == dc.ItemID);

                if (Request.QueryString["type"] != "ds" && Request.QueryString["resp"] == "0" && Request.QueryString["disch"] == "0")
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            query.RegistrationNo == searchReg,
                            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );


                    //query.Where(
                    //    query.Or(
                    //        query.RegistrationNo == searchReg,
                    //        patient.MedicalNo == searchReg,
                    //        patient.OldMedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(f.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //        string.Format("< OR REPLACE(f.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    //Helper.AddFilterMedNoOrRegNoOrPatName(query, patient, searchReg, "registration");
                }

                if (!string.IsNullOrEmpty(txtBarcodeEntry.Text))
                {
                    string searchReg = Helper.EscapeQuery(txtBarcodeEntry.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );

                    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                    //    query.Where(
                    //        query.Or(
                    //            string.Format("< REPLACE(f.MedicalNo, '-', '') LIKE '{0}'>", searchReg),
                    //            string.Format("< OR REPLACE(f.OldMedicalNo, '-', '') LIKE '{0}'>", searchReg)
                    //            )
                    //        );
                    //else
                    //    query.Where(
                    //        query.Or(
                    //            string.Format("< f.MedicalNo LIKE '{0}'>", searchReg),
                    //            string.Format("< OR f.OldMedicalNo LIKE '{0}'>", searchReg)
                    //            )
                    //        );
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                            patient.FullName.Like(searchPatient)
                            //string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (!txtRegistrationDate.IsEmpty)
                    query.Where(query.RegistrationDate >= txtRegistrationDate.SelectedDate, query.RegistrationDate < txtRegistrationDate.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    query.Where(query.GuarantorID == cboGuarantorID.SelectedValue);

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));
                query.Where
                    (
                        query.IsClosed == false,
                        query.SRRegistrationType == AppConstant.RegistrationType.MedicalCheckUp,
                        //query.IsHoldTransactionEntry == false,
                        query.IsVoid == false,
                        query.IsFromDispensary == false,
                        query.IsNonPatient == false
                    );

                query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationNo.Ascending
                    );

                return query.LoadDataTable();
            }
        }

        private DataTable TransChargesMCUChild
        {
            get
            {
                var chargesQuery = new TransChargesQuery("a");
                var unitQuery = new ServiceUnitQuery("b");
                var regQuery = new RegistrationQuery("c");
                var patientQuery = new PatientQuery("d");
                var medicQuery = new ParamedicQuery("e");
                var grrQuery = new GuarantorQuery("f");
                var roomQuery = new ServiceRoomQuery("g");
                var sumInfo = new RegistrationInfoSumaryQuery("h");
                var sal = new AppStandardReferenceItemQuery("sal");
                var gdc = new GuarantorDocumentChecklistQuery("gdc");
                var dc = new AppStandardReferenceItemQuery("dc");

                var fileq = new MedicalRecordFileStatusMovementQuery("z");
                chargesQuery.LeftJoin(fileq).On(
                    chargesQuery.RegistrationNo == fileq.RegistrationNo &&
                    chargesQuery.FromServiceUnitID == fileq.LastPositionServiceUnitID
                    );

                chargesQuery.es.Top = AppSession.Parameter.MaxResultRecord;

                chargesQuery.Select(
                    chargesQuery.RegistrationNo,
                    chargesQuery.FromServiceUnitID,
                    unitQuery.ServiceUnitName,
                    patientQuery.MedicalNo,
                    patientQuery.PatientName,
                    regQuery.RegistrationDate,
                    patientQuery.Sex,
                    regQuery.IsConsul,
                    regQuery.ParamedicID,
                    medicQuery.ParamedicName,
                    grrQuery.GuarantorName,
                    regQuery.RoomID,
                    roomQuery.RoomName,
                    regQuery.PatientID,
                    regQuery.SRRegistrationType,
                    @"<CASE WHEN h.NoteCount <= 0 THEN NULL ELSE h.NoteCount END AS NoteCount>",
                        @"<CASE WHEN dc.LineNumber IS NULL OR (dc.LineNumber - h.DocumentCheckListCount) <= 0 THEN NULL ELSE (dc.LineNumber - h.DocumentCheckListCount) END AS DocumentCheckListCountRemains>",
                    "<CAST(0 AS BIT) AS IsInpatient>",
                    "<'' AS SRTriage>",
                    @"<CASE WHEN ISNULL(c.ParamedicID, '')  = '' OR e.ParamedicID = '" + AppSession.Parameter.EmptyDoctorId + @"' OR e.ParamedicID = '" + AppSession.Parameter.DoctorOnDutyId + @"' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                    //@"<CASE WHEN c.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                    "<ISNULL(c.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                    unitQuery.IsNeedConfirmationOfAttendance,
                    @"<'' AS ChargeClassID>",
                    @"<'' AS CoverageClassID>",
                    @"<'' AS ClassID>",
                    @"<'' AS DefaultClassID>",
                    "<CAST(0 AS BIT) AS BedIsReady>",
                    @"<CAST(0 AS BIT) AS IsSurgeryRoom >",
                    sal.ItemName.As("SalutationName"),
                    patientQuery.DiagnosticNo,
                    fileq.LastPositionUserID.Coalesce("''"),
                    regQuery.IsHoldTransactionEntry,
                    @"<CAST(1 AS BIT) AS 'IsValidAssessment'>",
                    @"<'' AS FromRegistrationNo>",
                    @"<CAST(1 AS BIT) AS 'IsValidMedicalFileReceived'>",
                    @"<'0' AS ClassSeq1>",
                    @"<'0' AS ClassSeq2>"
                    );
                chargesQuery.InnerJoin(unitQuery).On(chargesQuery.FromServiceUnitID == unitQuery.ServiceUnitID);
                chargesQuery.InnerJoin(regQuery).On(chargesQuery.RegistrationNo == regQuery.RegistrationNo);
                chargesQuery.InnerJoin(patientQuery).On(regQuery.PatientID == patientQuery.PatientID);
                chargesQuery.InnerJoin(medicQuery).On(regQuery.ParamedicID == medicQuery.ParamedicID);
                chargesQuery.InnerJoin(grrQuery).On(regQuery.GuarantorID == grrQuery.GuarantorID);
                chargesQuery.InnerJoin(roomQuery).On(regQuery.RoomID == roomQuery.RoomID);
                chargesQuery.LeftJoin(sumInfo).On(chargesQuery.RegistrationNo == sumInfo.RegistrationNo);
                chargesQuery.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patientQuery.SRSalutation == sal.ItemID);
                chargesQuery.LeftJoin(gdc).On(regQuery.GuarantorID == gdc.GuarantorID & regQuery.SRRegistrationType == gdc.SRRegistrationType);
                chargesQuery.LeftJoin(dc).On(dc.StandardReferenceID == "DocumentChecklist" & gdc.SRDocumentChecklist == dc.ItemID);

                chargesQuery.Where(
                    regQuery.IsClosed == false,
                    regQuery.SRRegistrationType == AppConstant.RegistrationType.MedicalCheckUp,
                    //regQuery.IsHoldTransactionEntry == false,
                    regQuery.IsVoid == false,
                    regQuery.IsFromDispensary == false,
                    chargesQuery.IsOrder == false,
                    chargesQuery.IsVoid == false,
                    regQuery.IsNonPatient == false,
                    chargesQuery.Or(
                        chargesQuery.PackageReferenceNo.IsNotNull(),
                        chargesQuery.PackageReferenceNo != string.Empty
                        )
                    );

                if (Request.QueryString["type"] != "ds" && Request.QueryString["resp"] == "0" && Request.QueryString["disch"] == "0")
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    chargesQuery.InnerJoin(qusr).On(chargesQuery.FromServiceUnitID == qusr.ServiceUnitID);
                    chargesQuery.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    chargesQuery.Where(chargesQuery.FromServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    chargesQuery.Where(regQuery.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    chargesQuery.Where(
                        chargesQuery.Or(
                            chargesQuery.RegistrationNo == searchReg,
                            patientQuery.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patientQuery.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );


                    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                    //    chargesQuery.Where(
                    //        chargesQuery.Or(
                    //            chargesQuery.RegistrationNo == searchReg,
                    //            patientQuery.MedicalNo == searchReg,
                    //            patientQuery.OldMedicalNo == searchReg,
                    //            string.Format("< OR REPLACE(d.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //            string.Format("< OR REPLACE(d.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                    //else
                    //    chargesQuery.Where(
                    //        chargesQuery.Or(
                    //            chargesQuery.RegistrationNo == searchReg,
                    //            patientQuery.MedicalNo == searchReg,
                    //            patientQuery.OldMedicalNo == searchReg,
                    //            string.Format("< OR d.MedicalNo LIKE '%{0}%'>", searchReg),
                    //            string.Format("< OR d.OldMedicalNo LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                }

                if (!string.IsNullOrEmpty(txtBarcodeEntry.Text))
                {
                    string searchReg = Helper.EscapeQuery(txtBarcodeEntry.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    chargesQuery.Where(
                        chargesQuery.Or(
                            patientQuery.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patientQuery.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );

                    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                    //    chargesQuery.Where(
                    //        chargesQuery.Or(
                    //            string.Format("< REPLACE(d.MedicalNo, '-', '') LIKE '{0}'>", searchReg),
                    //            string.Format("< OR REPLACE(d.OldMedicalNo, '-', '') LIKE '{0}'>", searchReg)
                    //            )
                    //        );
                    //else
                    //    chargesQuery.Where(
                    //        chargesQuery.Or(
                    //            string.Format("< d.MedicalNo LIKE '{0}'>", searchReg),
                    //            string.Format("< OR d.OldMedicalNo LIKE '{0}'>", searchReg)
                    //            )
                    //        );
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    chargesQuery.Where
                        (
                            patientQuery.FullName.Like(searchPatient)
                            //string.Format("<LTRIM(RTRIM(LTRIM(d.FirstName + ' ' + d.MiddleName)) + ' ' + d.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (!txtRegistrationDate.IsEmpty)
                    chargesQuery.Where(regQuery.RegistrationDate >= txtRegistrationDate.SelectedDate, regQuery.RegistrationDate < txtRegistrationDate.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    chargesQuery.Where(regQuery.GuarantorID == cboGuarantorID.SelectedValue);

                //if (TransChargesMCU.Rows.Count > 0)
                //    chargesQuery.Where(regQuery.RegistrationNo.NotIn(TransChargesMCU.AsEnumerable().Select(t => t.Field<string>("RegistrationNo"))));

                if (TransChargesMCU.Rows.Count > 0)
                    chargesQuery.Where(chargesQuery.ToServiceUnitID.NotIn(TransChargesMCU.AsEnumerable().Select(t => t.Field<string>("ServiceUnitID"))));


                var dtb = chargesQuery.LoadDataTable();

                var table = TransChargesMCU.Copy();
                table.Clear();

                if (dtb.AsEnumerable().Any())
                {

                    foreach (DataRow tab in dtb.Rows)
                    {
                        var row = table.NewRow();

                        row["RoomID"] = tab["RoomID"];
                        row["RoomName"] = tab["RoomName"];
                        row["RegistrationDate"] = tab["RegistrationDate"];
                        row["QueNo"] = 1;
                        row["ServiceUnitID"] = tab["FromServiceUnitID"].ToString();
                        row["Group"] = tab["ServiceUnitName"].ToString();
                        row["ParamedicID"] = tab["ParamedicID"];
                        row["ParamedicName"] = tab["ParamedicName"];
                        row["RegistrationNo"] = tab["RegistrationNo"];
                        row["MedicalNo"] = tab["MedicalNo"];
                        row["PatientName"] = tab["PatientName"];
                        row["Sex"] = tab["Sex"];
                        row["GuarantorName"] = tab["GuarantorName"];
                        row["PatientID"] = tab["PatientID"];
                        row["IsConsul"] = tab["IsConsul"];
                        row["BedID"] = "";
                        row["NoteCount"] = tab["NoteCount"];
                        row["DocumentCheckListCountRemains"] = tab["DocumentCheckListCountRemains"];
                        row["IsInpatient"] = false;
                        row["SRTriage"] = tab["SRTriage"];
                        row["IsNewVisible"] = tab["IsNewVisible"];
                        row["IsConfirmedAttendance"] = tab["IsConfirmedAttendance"];
                        row["IsNeedConfirmationOfAttendance"] = tab["IsNeedConfirmationOfAttendance"];

                        row["ChargeClassID"] = tab["ChargeClassID"];
                        row["CoverageClassID"] = tab["CoverageClassID"];
                        row["ClassID"] = tab["ClassID"];
                        row["DefaultClassID"] = tab["DefaultClassID"];
                        row["BedIsReady"] = tab["BedIsReady"];
                        row["IsSurgeryRoom"] = tab["IsSurgeryRoom"];
                        row["SalutationName"] = tab["SalutationName"];
                        row["DiagnosticNo"] = tab["DiagnosticNo"];
                        row["LastPositionUserID"] = tab["LastPositionUserID"];
                        row["IsHoldTransactionEntry"] = tab["IsHoldTransactionEntry"];
                        row["IsValidAssessment"] = tab["IsValidAssessment"];
                        row["FromRegistrationNo"] = tab["FromRegistrationNo"];
                        row["IsValidMedicalFileReceived"] = tab["IsValidMedicalFileReceived"];
                        row["ClassSeq1"] = tab["ClassSeq1"];
                        row["ClassSeq2"] = tab["ClassSeq2"];

                        table.Rows.Add(row);
                    }
                }

                return table;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (_healthcareInitial != "RSSA")
                SaveValueToCookie();

            grdList.Rebind();
        }

        protected void grdList_PreRender(object sender, System.EventArgs e)
        {
            var grid = (RadGrid)sender;
            // grid target
            GridItem[] nestedViewItems = grid.MasterTableView.GetItems(GridItemType.NestedView);
            foreach (GridNestedViewItem nestedViewItem in nestedViewItems)
            {
                foreach (GridTableView nestedView in nestedViewItem.NestedTableViews)
                {
                    foreach (GridDataItem x in nestedView.Items)
                    {
                        System.Web.UI.WebControls.CheckBox chkPkg = x["IsPackage"].Controls[0] as System.Web.UI.WebControls.CheckBox;
                        if (!chkPkg.Checked)
                        {
                            //x.Cells[1].Text = "00";
                            x.Cells[1].Text = "&nbsp;";
                            //x.Cells[2].Text = "02";
                            //x.Cells[3].Text = "03";
                            //x.Cells[4].Text = "04";
                        }
                        else
                        {
                            /*
                             this will slow down the load a bit, if you have a better idea please fix it
                             */
                            System.Web.UI.WebControls.CheckBox chkApprove = x["IsApproved"].Controls[0] as System.Web.UI.WebControls.CheckBox;
                            System.Web.UI.WebControls.CheckBox chkVoid = x["IsVoid"].Controls[0] as System.Web.UI.WebControls.CheckBox;
                            if (chkApprove.Checked && !chkVoid.Checked)
                            {
                                // jika paket, cek detail paketnya, jika ada yang belum approve maka kasih warna merah
                                // sebagai penanda bahwa paket ini masih ada transaksi gantung
                                var tcColl = new TransChargesCollection();
                                tcColl.Query.Where(tcColl.Query.PackageReferenceNo == x.GetDataKeyValue("TransactionNo"));
                                if (tcColl.LoadAll())
                                {
                                    if (tcColl.Where(c => c.IsApproved == false && c.IsVoid == false).Count() > 0)
                                    {
                                        x.ForeColor = System.Drawing.Color.Red;
                                        x.ToolTip = "Package has unapproved detail transaction. Please expand then approve!";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //protected void grdList_ItemDataBound(object source, GridItemEventArgs e)
        //{
        //    switch (e.Item.ItemType)
        //    {
        //        case (GridItemType.AlternatingItem):
        //        case (GridItemType.Item):
        //            {
        //                var lname = e.Item.OwnerTableView.Name;
        //                if (lname == "detail")
        //                {
        //                    var IsPackage = (bool)DataBinder.Eval(e.Item.DataItem, "IsPackage");
        //                    if (!IsPackage)
        //                    {
        //                        //e.Item.Cells[1].Text = "0";
        //                        e.Item.Cells[1].Text = "&nbsp;";
        //                        //e.Item.Cells[2].Text = "2";
        //                        //e.Item.Cells[3].Text = "3";
        //                        //e.Item.Cells[4].Text = "4";
        //                        //e.Item.Cells[1].Controls[0].Visible = false;
        //                    }
        //                }
        //                break;
        //            }
        //    }
        //}

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            switch (e.DetailTableView.Name)
            {
                case "detail":
                    {
                        gridListLoadDetail(e);
                        break;
                    }
                case "detailPackage":
                    {
                        gridListLoadDetailPackage(e);
                        break;
                    }
            }
        }

        private void gridListLoadDetailPackage(GridDetailTableDataBindEventArgs e)
        {
            var query = new TransChargesQuery("a");
            var reg = new RegistrationQuery("b");
            var unit = new ServiceUnitQuery("d");
            var tcItem = new TransChargesItemQuery("e");
            var item = new ItemQuery("f");
            var qusr = new AppUserServiceUnitQuery("u");
            var condRule = new ItemConditionRuleQuery("condRule");

            var group = new esQueryItem(query, "Group", esSystemType.String);
            group = query.TransactionNo.Cast(esCastType.String);

            query.Select
            (
                query.TransactionNo,
                query.ReferenceNo,
                query.TransactionDate,
                unit.ServiceUnitName,
                query.RegistrationNo,
                query.IsAutoBillTransaction,
                tcItem.IsApprove.As("IsApproved"),
                tcItem.IsVoid,
                query.IsCorrection,
                tcItem.IsBillProceed,
                @"<(CASE WHEN ISNULL(e.FilmNo, '') = '' THEN f.ItemName ELSE f.ItemName + ' [' + e.FilmNo + ']' END) + ' ' + ISNULL(condRule.ItemConditionRuleName,'') AS ItemName>",
                //item.ItemName,
                query.LastUpdateByUserID,
                tcItem.ChargeQuantity,
                tcItem.Price,
                tcItem.ParamedicCollectionName,
                tcItem.CitoAmount,
                tcItem.DiscountAmount,
                query.FromServiceUnitID,
                reg.ParamedicID,
                tcItem.SequenceNo,
                query.IsOrder,
                query.IsPackage,
                "<CAST((case when u.ServiceUnitID is null then 0 else 1 end) as bit) HasAccess>",
                group.As("Group"),
                @"<'' AS ChargeClassID>",
                @"<'' AS CoverageClassID>",
                @"<'' AS ClassID>",
                @"<'0' AS ClassSeq1>",
                @"<'0' AS ClassSeq2>"
            );

            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(tcItem).On(query.TransactionNo == tcItem.TransactionNo);
            query.InnerJoin(item).On(tcItem.ItemID == item.ItemID);
            query.InnerJoin(unit).On(tcItem.ToServiceUnitID == unit.ServiceUnitID);
            query.LeftJoin(qusr).On(unit.ServiceUnitID == qusr.ServiceUnitID &&
                qusr.UserID == AppSession.UserLogin.UserID);
            query.LeftJoin(condRule).On(tcItem.ItemConditionRuleID == condRule.ItemConditionRuleID);

            query.Where
                (
                    query.PackageReferenceNo == e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString(),
                    query.Or(tcItem.ParentNo.IsNull(), tcItem.ParentNo == string.Empty)
                );

            query.OrderBy(query.TransactionDate.Ascending, query.TransactionNo.Ascending, tcItem.SequenceNo.Ascending);

            DataTable dtb = query.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
                var charges = new TransChargesItemCollection();
                charges.Query.Where(charges.Query.TransactionNo == row["TransactionNo"], charges.Query.IsVoid == false);
                charges.LoadAll();
                decimal subTotal = charges.Sum(x => (((x.ChargeQuantity ?? 0) * (x.Price ?? 0)) - (x.DiscountAmount ?? 0) + (x.CitoAmount ?? 0)));

                row["Group"] = row["ServiceUnitName"] + " - " +
                               row["TransactionNo"] + " (Total: " + string.Format("{0:n2}", (subTotal)) + ") ";
            }
            dtb.AcceptChanges();

            e.DetailTableView.DataSource = dtb;
        }

        private void gridListLoadDetail(GridDetailTableDataBindEventArgs e)
        {
            var query = new TransChargesQuery("a");
            var reg = new RegistrationQuery("b");
            var patient = new PatientQuery("c");
            var unit = new ServiceUnitQuery("d");
            var tcItem = new TransChargesItemQuery("e");
            var item = new ItemQuery("f");
            var cc = new CostCalculationQuery("g");
            var tci = new TransChargesItemQuery("h");
            var condRule = new ItemConditionRuleQuery("condRule");

            tci.Select(tci.TransactionNo, tci.SequenceNo);
            tci.Where(tci.TransactionNo == tcItem.TransactionNo, tci.SequenceNo == tcItem.SequenceNo, tci.IsExtraItem == true,
                          tci.IsSelectedExtraItem == false);

            query.Select
                (
                    query.TransactionNo,
                    query.ReferenceNo,
                    query.TransactionDate,
                    unit.ServiceUnitName,
                    query.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    query.IsAutoBillTransaction,
                    Request.QueryString["type"] != "jo" ? tcItem.IsApprove.As("IsApproved") : query.IsApproved,
                    tcItem.IsVoid,
                    query.IsCorrection,
                    tcItem.IsBillProceed,
                    @"<(CASE WHEN ISNULL(e.FilmNo, '') = '' THEN f.ItemName ELSE f.ItemName + ' [' + e.FilmNo + ']' END) + ' ' + CASE WHEN ISNULL(condRule.ItemConditionRuleName,'') = '' THEN '' ELSE '~ ' + condRule.ItemConditionRuleName END AS ItemName>",
                    query.LastUpdateByUserID,
                    tcItem.ChargeQuantity,
                    tcItem.Price,
                    tcItem.ParamedicCollectionName,
                    tcItem.CitoAmount,
                    tcItem.DiscountAmount,
                    query.FromServiceUnitID,
                    reg.ParamedicID,
                    cc.IntermBillNo,
                    tcItem.SequenceNo,
                    query.IsOrder,
                    query.IsPackage,
                    @"<'' AS ChargeClassID>",
                    @"<'' AS CoverageClassID>",
                    @"<'' AS ClassID>",
                    tcItem.ItemID,
                    tcItem.Notes,
                    item.SRItemType,
                    query.RoomID,
                    @"<ISNULL((SELECT SUM(tci.ChargeQuantity) AS Qty 
FROM TransChargesItem AS tci
INNER JOIN TransCharges AS tc ON tc.TransactionNo = tci.TransactionNo
WHERE tci.IsApprove = 1 AND tci.IsBillProceed = 1 AND tc.IsCorrection = 1
AND tci.ReferenceNo = e.TransactionNo AND tci.ReferenceSequenceNo = e.SequenceNo), 0) AS CorrectionQty>",
                    @"<'0' AS ClassSeq1>",
                    @"<'0' AS ClassSeq2>"
                );

            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.InnerJoin(tcItem).On(query.TransactionNo == tcItem.TransactionNo);
            query.InnerJoin(item).On(tcItem.ItemID == item.ItemID);
            query.LeftJoin(cc).On(tcItem.TransactionNo == cc.TransactionNo && tcItem.SequenceNo == cc.SequenceNo);
            query.LeftJoin(condRule).On(tcItem.ItemConditionRuleID == condRule.ItemConditionRuleID);

            if (Request.QueryString["type"] == "tr")
                query.InnerJoin(unit).On(query.FromServiceUnitID == unit.ServiceUnitID);
            else
                query.InnerJoin(unit).On(query.ToServiceUnitID == unit.ServiceUnitID);

            query.Where
                (
                    query.RegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString(),
                    query.IsOrder == (Request.QueryString["type"] == "jo" || Request.QueryString["type"] == "ds"),
                    query.IsCorrection == false,
                    reg.IsClosed == false,
                    query.Or(
                        tcItem.ParentNo.IsNull(),
                        tcItem.ParentNo == string.Empty
                    ),
                    query.NotExists(tci)
                );
            if (Request.QueryString["type"] == "jo" || Request.QueryString["type"] == "ds")
            {
                if (Request.QueryString["type"] == "jo")
                    query.Where(query.TransactionNo.Substring(1, 2) == "JO");
                else
                    query.Where(query.TransactionNo.Substring(1, 2) == "DS");
            }

            query.Where(query.Or(query.PackageReferenceNo.IsNull(), query.PackageReferenceNo == string.Empty));
            if (Request.QueryString["type"] == "jo")
                query.Where(query.IsProceed == true);
            //if (Request.QueryString["type"] == "ds")
            //    query.Where(query.IsProceed == false);
            if ((Request.QueryString["type"] == "mcu"))
                query.Where(query.IsPackage == true);

            if (Request.QueryString["disch"] == "0")
            {
                if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "ds")
                {
                    var tcPkg = new TransChargesQuery("tcp");
                    var tciPkg = new TransChargesItemQuery("tcip");
                    var usuP = new AppUserServiceUnitQuery("usuP");
                    query.LeftJoin(tcPkg).On(query.TransactionNo == tcPkg.PackageReferenceNo)
                        .LeftJoin(tciPkg).On(tcPkg.TransactionNo == tciPkg.TransactionNo);

                    if (Request.QueryString["type"] == "tr")
                    {
                        query.LeftJoin(usuP).On(tcPkg.FromServiceUnitID == usuP.ServiceUnitID);
                        query.Where(query.Or(
                          query.ToServiceUnitID == e.DetailTableView.ParentItem.GetDataKeyValue("ServiceUnitID").ToString(),
                          tciPkg.ToServiceUnitID == e.DetailTableView.ParentItem.GetDataKeyValue("ServiceUnitID").ToString()
                          ));
                    }
                    else
                    {
                        query.LeftJoin(usuP).On(tcPkg.ToServiceUnitID == usuP.ServiceUnitID);
                    }

                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(unit.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(query.Or(
                        qusr.UserID == AppSession.UserLogin.UserID,
                        usuP.UserID == AppSession.UserLogin.UserID));
                }
                else
                {
                    if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    {
                        if (Request.QueryString["type"] == "jo")
                            query.Where(query.FromServiceUnitID == cboServiceUnitID.SelectedValue);
                        else query.Where(query.ToServiceUnitID == cboServiceUnitID.SelectedValue);
                    }

                    if (Request.QueryString["type"] == "jo")
                    {
                        var qusr = new AppUserServiceUnitQuery("u");
                        query.InnerJoin(qusr).On(query.FromServiceUnitID == qusr.ServiceUnitID);
                        query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                    }
                    else
                    {
                        var qusr = new AppUserServiceUnitQuery("u");
                        query.InnerJoin(qusr).On(query.ToServiceUnitID == qusr.ServiceUnitID);
                        query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                    }
                }
            }
            query.OrderBy(query.TransactionDate.Ascending, query.TransactionNo.Ascending, tcItem.SequenceNo.Ascending);

            query.es.Distinct = true;

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "MotherChildWristband")
            {
                var jobParameters = new PrintJobParameterCollection();

                var jobParameter = jobParameters.AddNew();
                jobParameter.Name = "p_RegistrationNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.MotherChildWristband;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "PatientLabel")
            {
                var jobParameters = new PrintJobParameterCollection();

                var jobParameter = jobParameters.AddNew();
                jobParameter.Name = "p_RegistrationNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.RSUI_LABEL_GIZI_PASIEN;

                string script = @"openRpt();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "PatientStickerRssmcb")
            {
                var jobParameters = new PrintJobParameterCollection();

                var jobParameter = jobParameters.AddNew();
                jobParameter.Name = "p_RegistrationNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.RegistrationLabel;
                var SuPrintLabelPatientID = AppSession.Parameter.AppProgramServiceUnitPatientLabel;
                if (!string.IsNullOrEmpty(SuPrintLabelPatientID)) AppSession.PrintJobReportID = SuPrintLabelPatientID;

                string script = @"openRpt();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "PrintLabelMCU")
            {
                var jobParameters = new PrintJobParameterCollection();

                var jobParameter = jobParameters.AddNew();
                jobParameter.Name = "p_RegistrationNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppSession.Parameter.AppProgramPrintLabelMCU;

                string script = @"openRpt();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var tooltip = string.Empty;
                var dataItem = e.Item as GridDataItem;
                if (dataItem.OwnerTableView.Name == "master")
                {
                    if (dataItem["ChargeClassID"].Text != dataItem["CoverageClassID"].Text)
                    {
                        // Beri warna merah jika CoverageClassID berbeda dg ChargeClassID Up, 
                        // Beri warna biru jika CoverageClassID berbeda dg ChargeClassID Down, 
                        var classSeq1 = dataItem["ClassSeq1"].Text.ToInt();
                        var classSeq2 = dataItem["ClassSeq2"].Text.ToInt();

                        dataItem.ForeColor = classSeq1 < classSeq2 ? Color.Red : Color.Blue;
                        dataItem.Font.Bold = true;
                        tooltip = "Charge class is different from coverage class.";
                    }
                    if (dataItem["ChargeClassID"].Text != dataItem["DefaultClassID"].Text)
                    {
                        var c = new Class();
                        c.LoadByPrimaryKey(dataItem["DefaultClassID"].Text);
                        if (c.IsTariffClass ?? false)
                        {
                            dataItem.Font.Bold = true;
                            dataItem.Font.Italic = true;
                            tooltip = tooltip == string.Empty ? "Charge class is different from bed class." : "Charge class is different from coverage and bed class.";
                        }
                    }
                    dataItem.ToolTip = tooltip;
                }
                if (dataItem.OwnerTableView.Name == "detail")
                {
                    dataItem.ToolTip = ((DataRowView)dataItem.DataItem).Row["Notes"].ToString();
                }
            }
        }

        public System.Drawing.Color GetColorOfTriase(object SRTriage)
        {
            System.Drawing.Color color = System.Drawing.Color.White;
            switch (SRTriage.ToString())
            {
                case "01":
                    {
                        color = System.Drawing.Color.Red;
                        break;
                    }
                case "02":
                    {
                        color = System.Drawing.Color.Orange;
                        break;
                    }
                case "03":
                    {
                        color = System.Drawing.Color.Yellow;
                        break;
                    }
                case "04":
                    {
                        color = System.Drawing.Color.Green;
                        break;
                    }
                case "05":
                    {
                        color = System.Drawing.Color.Black;
                        break;
                    }
                case "99": // pasien rawat jalan yg sudah dilakukan PHYEXAM
                    {
                        color = System.Drawing.Color.Blue;
                        break;
                    }
            }

            return color;
        }

        protected virtual void txtBarcodeEntry_OnTextChanged(object sender, System.EventArgs e)
        {
            if (txtBarcodeEntry.Text == "") return;
            grdList.Rebind();
            if (grdList.MasterTableView.Items.Count == 0)
            {
                // record not found
                MessageShow("Data not found");
            }
            else if (grdList.MasterTableView.Items.Count == 1)
            {
                MedicalRecordFileMoveTo(
                    grdList.MasterTableView.Items[0].GetDataKeyValue("ServiceUnitID").ToString(),
                    grdList.MasterTableView.Items[0].GetDataKeyValue("RegistrationNo").ToString());
                grdList.DataSource = null;
                grdList.Rebind();
            }
            else
            {
                // multiple registration
                MessageShow("Multiple registrations have been found, please mark the medical file receive manually");
            }

            txtBarcodeEntry.Text = "";
            txtBarcodeEntry.Focus();
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (eventArgument == null) return;

            if (eventArgument.Contains("!"))
            {
                var usr = new AppUserServiceUnitCollection();
                usr.Query.Where(usr.Query.UserID == AppSession.UserLogin.UserID,
                                usr.Query.ServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID);
                usr.LoadAll();
                if (usr.Count > 0)
                {
                    var param = eventArgument.Split('!');
                    var regno = param[1];
                    var diagno = param[2];
                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(regno))
                    {
                        var pat = new Patient();
                        if (pat.LoadByPrimaryKey(reg.PatientID))
                        {
                            pat.DiagnosticNo = diagno;
                            pat.Save();
                            grdList.Rebind();
                        }
                    }
                }
            }
            else
            {
                // split args
                var args = eventArgument.Split(new char[] { '|' });

                if (sourceControl is RadGrid)
                {
                    switch (args[0])
                    {
                        case "rebind":
                            {
                                grdList.Rebind();
                                break;
                            }
                        case "printlbl":
                            {
                                //Print(AppConstant.Report.BillingStatementDetail2);

                                var jobParameters = new PrintJobParameterCollection();
                                var jParam1 = jobParameters.AddNew();
                                jParam1.Name = "P_TransactionNo";
                                jParam1.ValueString = args[1];
                                var jParam2 = jobParameters.AddNew();
                                jParam2.Name = "P_SequenceNo";
                                jParam2.ValueString = args[2];

                                AppSession.PrintJobParameters = jobParameters;
                                AppSession.PrintJobReportID = AppConstant.Report.JobOrderReceipt;

                                string script = @"var oWnd = $find('" + winPrintOpt.ClientID + "');" +
                                                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                                                "oWnd.Show();" +
                                                "oWnd.Maximize();";
                                RadAjaxPanel1.ResponseScripts.Add(script);
                                break;
                            }
                        case "receive":
                            {
                                var reg = new Registration();
                                if (reg.LoadByPrimaryKey(eventArgument.Split('|')[1]))
                                {
                                    string unitId = eventArgument.Split('|')[2];

                                    MedicalRecordFileMoveTo(unitId, reg.RegistrationNo);
                                    grdList.Rebind();
                                }

                                break;
                            }
                    }
                }
            }
        }

        private void MedicalRecordFileMoveTo(string ServiceUnitID, string RegistrationNo)
        {
            var file = new MedicalRecordFileStatusMovement();
            file.MoveTo(ServiceUnitID, RegistrationNo, AppSession.UserLogin.UserID);
            file.Save();

            if (AppSession.Parameter.HealthcareInitial == "RSTJ")
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(RegistrationNo);
                if (!string.IsNullOrEmpty(reg.AppointmentNo))
                {
                    var task = new BusinessObject.Interop.TARAKAN.AppointmentOnlineTask();
                    if (!task.LoadByPrimaryKey(reg.AppointmentNo, "4"))
                    {
                        task.AppointmentNo = reg.AppointmentNo;
                        task.TaskId = "4";
                        task.Timestamp = Temiang.Avicenna.Common.BPJS.Helper.GetUnixTimeStamp();
                        task.Save();
                    }
                }
            }

            //if (Helper.IsBpjsAntrolIntegration)
            //{
            //    var reg = new Registration();
            //    reg.LoadByPrimaryKey(RegistrationNo);
            //    if (!string.IsNullOrEmpty(reg.AppointmentNo) && reg.SRRegistrationType != AppConstant.RegistrationType.InPatient)
            //    {
            //        var svc = new Common.BPJS.Antrian.Service();
            //        var response = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
            //        {
            //            Kodebooking = reg.AppointmentNo,
            //            Taskid = 4,
            //            Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
            //        });
            //    }
            //}
        }
    }
}
