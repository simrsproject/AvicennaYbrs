using System;
using System.ComponentModel;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Module.RADT.Emr.MainContent;
using Temiang.Dal.Interfaces;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    /// <summary>
    /// EMR Patient List
    /// </summary>
    /// Create By: Handono
    /// Modif Hist:
    /// ===========================================
    /// [2023 March 24 Handono]
    /// Penerapan asynchronous update pada status:
    /// - Lab Test
    /// - Radiologi
    /// - EWS
    /// - Plafond
    /// - SOAP
    /// - Prescription
    /// - Pathway
    /// ============================================
    public partial class EmrList : BasePage
    {
        protected string PlafondProgress(string regNo)
        {
            return string.Empty;
        }

        protected void cboRegistrationType_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            PopulateServiceUnit(cboRegistrationType.SelectedValue);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsEmrListShowPlafondProgress))
            {
                grdList.MasterTableView.GetColumnSafe("PlafondProgress").Visible = false;
            }

            if (!IsPostBack)
            {
                //GuarantorBpjs = null; // Reset list Guarantor Bpjs
                hdnIsClinicalPathwayActive.Value = (AppSession.Parameter.ClinicalPathwayRegistrationType.Contains(AppConstant.RegistrationType.InPatient) ||
                    AppSession.Parameter.ClinicalPathwayRegistrationType.Contains(AppConstant.RegistrationType.OutPatient) ||
                    AppSession.Parameter.ClinicalPathwayRegistrationType.Contains(AppConstant.RegistrationType.EmergencyPatient)) ? "y" : "n";

                //chkIsRegToOtherPhy.Visible = !string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID) && AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor; // Hanya untuk user dokter
                cboParamedicTeam.Enabled = !string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID) && AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor; // Hanya untuk user dokter

                var isDebug = HttpContext.Current.IsDebuggingEnabled;
                trSmfFilter.Visible = isDebug;
                grdList.Columns.FindByUniqueName("RowSource").Visible = isDebug;
                grdList.Columns.FindByUniqueName("RegistrationQue").Visible = !AppSession.Parameter.IsEmrListUsingExternalQueNo;
                grdList.Columns.FindByUniqueName("ExternalQueNo").Visible = AppSession.Parameter.IsEmrListUsingExternalQueNo;
                grdList.Columns.FindByUniqueName("ClinicalPathway").Visible = hdnIsClinicalPathwayActive.Value == "y";
                grdList.Columns.FindByUniqueName("IsVipMember").Visible = AppSession.Parameter.IsCrmMembershipActive;

                if (trSmfFilter.Visible)
                {
                    var smf = new SmfCollection();
                    smf.Query.OrderBy(smf.Query.SmfName.Ascending);
                    smf.LoadAll();

                    cboSmf.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (var entity in smf)
                    {
                        cboSmf.Items.Add(new RadComboBoxItem(entity.SmfName, entity.SmfID));
                    }
                }

                StandardReference.InitializeIncludeSpace(cboRegistrationType, AppEnum.StandardReference.RegistrationType);

                //service unit
                PopulateServiceUnit(cboRegistrationType.SelectedValue);

                if (!string.IsNullOrEmpty(Request.QueryString["medno"]))
                {
                    // TODO: switch nya masih rancu krn kriteria kalau dokter jadi bisa pilih dokter lain, nanti ganti caranya
                    // Jika mode switch Registration
                    txtRegistrationNo.Text = Request.QueryString["medno"];
                    chkIsIncludeClosed.Checked = true;

                }
                else
                {
                    txtRegistrationDate.SelectedDate = DateTime.Now.Date;

                    cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    if (!string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID) && AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor)
                    {
                        ComboBox.PopulateWithOneParamedic(cboParamedicID, AppSession.UserLogin.ParamedicID);
                        cboParamedicID.Enabled = false;
                    }

                    txtExamOrderDateFrom.SelectedDate = DateTime.Now.Date.AddDays(-1);
                    txtExamOrderDateTo.SelectedDate = DateTime.Now.Date;
                }

                // Filter utk out patient
                cboConfirmedAttendanceStatus.Items.Add(new RadComboBoxItem("-All-", ""));
                cboConfirmedAttendanceStatus.Items.Add(new RadComboBoxItem("Confirmed", "1"));
                cboConfirmedAttendanceStatus.Items.Add(new RadComboBoxItem("Not Confirm", "0"));

                // Physician Team Type
                cboParamedicTeam.Items.Add(new RadComboBoxItem("-All-", "ALL"));
                cboParamedicTeam.Items.Add(new RadComboBoxItem("Patient registrated to me", "REGTOME"));
                var pt = new AppStandardReferenceItemQuery("s");
                pt.Where(pt.StandardReferenceID == "ParamedicTeamStatus");
                pt.OrderBy(pt.LineNumber.Ascending, pt.ItemID.Ascending);
                pt.Select(pt.ItemID, pt.ItemName);
                var dtbPt = pt.LoadDataTable();
                foreach (DataRow row in dtbPt.Rows)
                {
                    cboParamedicTeam.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
                }
                if (cboParamedicTeam.Enabled)
                    cboParamedicTeam.SelectedValue = AppSession.Parameter.DefaultParamedicTeamOnEmrList;

                if (string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID))
                {
                    btnCloseClinic.ImageUrl = "~/Images/doctor_with_closed_sign_d.png";
                    btnCloseClinic.Enabled = false;
                    btnCloseClinic.Style[HtmlTextWriterStyle.Cursor] = "default";
                }
            }
        }

        private void PopulateServiceUnit(string registrationType)
        {
            var units = new ServiceUnitCollection();
            var query = new ServiceUnitQuery("a");

            if (string.IsNullOrEmpty(registrationType))
            {
                // ServiceUnit OK SubQuery
                var srOK = new ServiceRoomQuery("sr");
                srOK.Select(srOK.ServiceUnitID);
                srOK.Where(srOK.IsOperatingRoom == true, srOK.IsShowOnBookingOT == true,
                    srOK.ServiceUnitID == query.ServiceUnitID);

                query.Where(query.Or(
                        query.SRRegistrationType.In(
                            AppConstant.RegistrationType.InPatient,
                            AppConstant.RegistrationType.EmergencyPatient,
                            AppConstant.RegistrationType.OutPatient,
                            AppConstant.RegistrationType.MedicalCheckUp
                        ), query.ServiceUnitID.In(srOK)),
                    query.IsActive == true
                );
            }
            else
                query.Where(
                    query.SRRegistrationType == registrationType,
                    query.IsActive == true
                );

            // nurse & physiotherapy itu ikutin mapping, karena yg fisio biasany dia di refer ke sana. 
            // tp kalo nutrition sama farmasi (kalo ada) itu bisa liat semua pasien (DB) serta yg lainnya (HN)
            // user physiotherapy ada 2 yaitu tanpa paramedicid dan memiliki paramedicid
            // yg tidak memiliki paramedic id dianggap spt nurse dan yg memiliki paramedicid dainggap spt dokter menggunakan paramedicteam tuk hak akses pasiennya
            if (AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse || (AppSession.UserLogin.SRUserType == AppUser.UserType.Physiotherapy && string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID)))
            {
                var ausu = new AppUserServiceUnitQuery("ausu");
                query.InnerJoin(ausu).On(query.ServiceUnitID == ausu.ServiceUnitID &&
                                         ausu.UserID == AppSession.UserLogin.UserID);
            }
            query.OrderBy(units.Query.ServiceUnitName.Ascending);
            units.Load(query);

            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Text = string.Empty;
            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ServiceUnit entity in units)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
            }

            if (!IsPostBack && !string.IsNullOrEmpty(AppSession.UserLogin.ServiceUnitID))
            {
                ComboBox.SelectedValue(cboServiceUnitID, AppSession.UserLogin.ServiceUnitID);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                if (string.IsNullOrWhiteSpace(cboParamedicTeam.SelectedValue))
                    cboParamedicTeam.SelectedValue = "ALL";

                if (string.IsNullOrEmpty(Request.QueryString["medno"]))
                    RestoreValueFromCookie("EmrList");
            }

            // Update pnlFilterInPatient
            pnlFilterInPatient.Visible = false;
            if (IsRegistrationTypeInPatientActive)
            {
                pnlFilterInPatient.Visible = true;
                AjaxManager.AjaxSettings.AddAjaxSetting(btnFilterIncludeNotInBed, grdList, ajaxLoadingPanel);
            }
        }

        private bool? _isRegistrationTypeInPatientActive = null;
        private bool IsRegistrationTypeInPatientActive
        {
            get
            {
                if (_isRegistrationTypeInPatientActive == null)
                {
                    _isRegistrationTypeInPatientActive = false;
                    foreach (DropDownListItem item in cboRegistrationType.Items)
                    {
                        if (item.Value == AppConstant.RegistrationType.InPatient)
                        {
                            _isRegistrationTypeInPatientActive = true;
                            break;
                        }
                    }

                }
                return _isRegistrationTypeInPatientActive ?? false;
            }
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            // Jangan load jika bukan dokter supaya cepat
            if (!IsPostBack && AppSession.UserLogin.SRUserType != AppUser.UserType.Doctor &&
                string.IsNullOrWhiteSpace(cboServiceUnitID.SelectedValue) &&
                string.IsNullOrWhiteSpace(txtRegistrationNo.Text))
            {
                grdList.DataSource = String.Empty;
                return;
            }

            var dtb = Registrations();

            if (dtb == null)
                grdList.DataSource = string.Empty;
            else
                grdList.DataSource = dtb;

            var recCount = dtb == null ? 0 : dtb.Rows.Count;
            lblRegistrationCount.Text = string.Format("{0}{1}", recCount, AppSession.Parameter.MaxResultRecordEmrList == recCount ? "+" : "");
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print1")
            {
                PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();
                PrintJobParameter jobParameter = jobParameters.AddNew();
                jobParameter.Name = "MedicalNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.ResumeRawatJalan;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                  "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                  "oWnd.Show();" +
                  "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "Print2")
            {
                PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();
                PrintJobParameter jobParameter = jobParameters.AddNew();
                jobParameter.Name = "MedicalNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.RingkasanPenyakitPasien;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                  "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                  "oWnd.Show();" +
                  "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "Print3")
            {
                PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();
                PrintJobParameter jobParameter = jobParameters.AddNew();
                jobParameter.Name = "RegistrationNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                var parUser = jobParameters.AddNew();
                parUser.Name = "UserName";
                parUser.ValueString = AppSession.UserLogin.UserName;

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.PhysicianStatement;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                  "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                  "oWnd.Show();" +
                  "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "Print4")
            {
                PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();
                PrintJobParameter jobParameter = jobParameters.AddNew();
                jobParameter.Name = "RegistrationNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                var parUser = jobParameters.AddNew();
                parUser.Name = "UserName";
                parUser.ValueString = AppSession.UserLogin.UserName;

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.ResumeMedisRawatInap;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                  "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                  "oWnd.Show();" +
                  "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
        }

        #region Patient Registrations
        private string[] _patientIdSearchs = null;

        private DataTable Registrations()
        {
            DataTable dtb = null;
            using (var tr = new esTransactionScope())
            {
                var regType = string.Empty;
                var isShowJobOrder = true;
                var isShowOperatingRoom = true;

                // Check record in Registration and Patient
                if (!string.IsNullOrWhiteSpace(txtRegistrationNo.Text))
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    if (txtRegistrationNo.Text.ToLower().Contains("reg"))
                    {
                        var reg = new Registration();
                        if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
                        {
                            regType = reg.SRRegistrationType;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        // Check patient Exist
                        var patQr = new PatientQuery("p");
                        patQr.Select(patQr.PatientID);
                        patQr.es.Top = 50; // Batasi hanya 50 berdasrkan LastVisitDate (Handono 20241121)
                        patQr.OrderBy(patQr.LastVisitDate.Descending);

                        var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());
                        patQr.Where(
                            patQr.Or(
                                patQr.ReverseMedicalNo.Like(reverseMedNoSearch),
                                patQr.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                                )
                            );

                        var dtbPids = patQr.LoadDataTable();
                        if (dtbPids.Rows.Count>0)
                            // Isi _patientIdSearchs untuk pencarian dan ini efektif jika patientid nya "sedikit"
                            _patientIdSearchs = dtbPids.AsEnumerable().Select(r => r.Field<string>("PatientID")).ToArray();
                        else
                            // Isi kosong spy tidak menggunakan pencarian berdasarkan PatientID
                            _patientIdSearchs = new string[0] { };

                        if (dtbPids.Rows.Count == 0) return null;
                    }
                } else if (!string.IsNullOrWhiteSpace(txtPatientName.Text))
                {
                    // Check patient Exist
                    var searchPatient = txtPatientName.Text.Trim() + "%"; //Sudah konfirmasi ke IT RSI dan bu Rimma kalau user biasanya cari dengan nama depan dulu (Handono 202411)
                    var patQr = new PatientQuery("p");
                    patQr.Select(patQr.PatientID);
                    patQr.es.Top = 50; // Batasi hanya 50 berdasrkan LastVisitDate (Handono 20241121)
                    patQr.OrderBy(patQr.LastVisitDate.Descending);
                    patQr.Where(patQr.FullName.Like(searchPatient));
                    var dtbPids = patQr.LoadDataTable();
                    if (dtbPids.Rows.Count > 0)
                        // Isi _patientIdSearchs untuk pencarian dan ini efektif jika patientid nya "sedikit"
                        _patientIdSearchs = dtbPids.AsEnumerable().Select(r => r.Field<string>("PatientID")).ToArray();
                    else
                        // Isi kosong spy tidak menggunakan pencarian berdasarkan PatientID
                        _patientIdSearchs = new string[0] { };

                    if (dtbPids.Rows.Count == 0) return null;

                }

                // Check RegistrationType
                if (!string.IsNullOrEmpty(cboRegistrationType.SelectedValue))
                {
                    // Jika regType sudah terisi (dari regType = reg.SRRegistrationType) 
                    // dan berbeda dg cboRegistrationType maka dianggap tidak akan ada data yg ditemukan
                    if (!string.IsNullOrEmpty(regType) && regType != cboRegistrationType.SelectedValue)
                        return null;

                    regType = cboRegistrationType.SelectedValue;
                }

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                {
                    // Jika regType sudah terisi (dari regType = reg.SRRegistrationType) 
                    // dan berbeda dg cboRegistrationType maka dianggap tidak akan ada data yg ditemukan

                    var su = new ServiceUnit();
                    su.LoadByPrimaryKey(cboServiceUnitID.SelectedValue);
                    if (!string.IsNullOrEmpty(regType) && regType != su.SRRegistrationType)
                        return null;

                    regType = su.SRRegistrationType;
                    isShowJobOrder = su.IsUsingJobOrder ?? false;

                    // isShowOperatingRoom
                    var sr = new ServiceRoom();
                    sr.Query.Where(sr.Query.ServiceUnitID == su.ServiceUnitID, sr.Query.IsOperatingRoom == true,
                        sr.Query.IsShowOnBookingOT == true);
                    sr.Query.es.Top = 1;
                    isShowOperatingRoom = sr.Query.Load();
                }


                var maxResultRecord = AppSession.Parameter.MaxResultRecordEmrList;
                switch (regType)
                {
                    case AppConstant.RegistrationType.InPatient:
                        {
                            dtb = PatientInPatient(maxResultRecord);
                            maxResultRecord = maxResultRecord - dtb.Rows.Count;
                            break;
                        }
                    case AppConstant.RegistrationType.MedicalCheckUp:
                        {
                            isShowJobOrder = false;
                            dtb = RegistrationNonIp(maxResultRecord, new string[] { AppConstant.RegistrationType.MedicalCheckUp });
                            maxResultRecord = maxResultRecord - dtb.Rows.Count;
                            if (maxResultRecord > 0)
                            {
                                var dtb2 = RegistrationListFromServiceUnitTransaction(maxResultRecord, new string[] { AppConstant.RegistrationType.MedicalCheckUp });
                                dtb.Merge(dtb2);
                                maxResultRecord = maxResultRecord - dtb2.Rows.Count;
                            }
                            if (maxResultRecord > 0)
                            {
                                var dtb3 = RegistrationListFromJobOrderTransaction(maxResultRecord, new string[] { AppConstant.RegistrationType.MedicalCheckUp });
                                dtb.Merge(dtb3);
                                maxResultRecord = maxResultRecord - dtb3.Rows.Count;
                            }
                            break;
                        }
                    case AppConstant.RegistrationType.EmergencyPatient:
                        {
                            dtb = RegistrationNonIp(maxResultRecord, new string[] { AppConstant.RegistrationType.EmergencyPatient });
                            maxResultRecord = maxResultRecord - dtb.Rows.Count;
                            break;
                        }
                    case AppConstant.RegistrationType.OutPatient:
                        {
                            dtb = RegistrationNonIp(maxResultRecord, new string[] { AppConstant.RegistrationType.OutPatient });
                            maxResultRecord = maxResultRecord - dtb.Rows.Count;
                            break;
                        }
                    default:
                        {
                            dtb = RegistrationNonIp(maxResultRecord, new string[] { AppConstant.RegistrationType.OutPatient, AppConstant.RegistrationType.EmergencyPatient, AppConstant.RegistrationType.MedicalCheckUp, AppConstant.RegistrationType.ClusterPatient });
                            maxResultRecord = maxResultRecord - dtb.Rows.Count;
                            if (maxResultRecord > 0 && IsRegistrationTypeInPatientActive)
                            {
                                var dtb2 = PatientInPatient(maxResultRecord);
                                dtb.Merge(dtb2);
                                maxResultRecord = maxResultRecord - dtb2.Rows.Count;
                            }

                            if (maxResultRecord > 0)
                            {
                                var dtb2 = RegistrationListFromServiceUnitTransaction(maxResultRecord, new string[] { AppConstant.RegistrationType.MedicalCheckUp });
                                dtb.Merge(dtb2);
                                maxResultRecord = maxResultRecord - dtb2.Rows.Count;
                            }
                            if (maxResultRecord > 0)
                            {
                                var dtb3 = RegistrationListFromJobOrderTransaction(maxResultRecord, new string[] { AppConstant.RegistrationType.MedicalCheckUp });
                                dtb.Merge(dtb3);
                                maxResultRecord = maxResultRecord - dtb3.Rows.Count;
                            }
                            break;
                        }
                }

                // Add pasien dari Job Order 
                // Filter Service Unit nya dari Service Unit yg dituju (ToServiceUnitID) 
                if (isShowJobOrder && maxResultRecord > 0)
                {
                    var dtbJO = PatientExamOrder(regType, maxResultRecord);
                    dtb.Merge(dtbJO);
                    maxResultRecord = maxResultRecord - dtbJO.Rows.Count;
                }

                // Add Pasien di ruang OK
                if (isShowOperatingRoom && maxResultRecord > 0)
                {
                    // Jika user tipe dokter maka cari di semua seting dokternya
                    // Selainnya cukup cari di Main Pysiciannya saja krn tujuannya hanya menampilkan pasiennya saja
                    var dtbOk = PatientServiceUnitBookingPhyMain(maxResultRecord);
                    dtb.Merge(dtbOk);
                    maxResultRecord = maxResultRecord - dtbOk.Rows.Count;

                    if (maxResultRecord > 0 && AppSession.UserLogin.SRUserType == UserLogin.UserType.Doctor)
                    {
                        if (cboParamedicID.SelectedValue != string.Empty)
                        {
                            var dtb2 = PatientServiceUnitBookingPhy2(maxResultRecord);
                            dtb.Merge(dtb2);
                            maxResultRecord = maxResultRecord - dtb2.Rows.Count;
                            if (maxResultRecord > 0)
                            {
                                var dtb3 = PatientServiceUnitBookingPhy3(maxResultRecord);
                                dtb.Merge(dtb3);
                                maxResultRecord = maxResultRecord - dtb3.Rows.Count;
                            }

                            if (maxResultRecord > 0)
                            {
                                var dtb3 = PatientServiceUnitBookingPhy4(maxResultRecord);
                                dtb.Merge(dtb3);
                                maxResultRecord = maxResultRecord - dtb3.Rows.Count;
                            }

                            if (maxResultRecord > 0)
                            {
                                var dtb3 = PatientServiceUnitBookingAnes(maxResultRecord);
                                dtb.Merge(dtb3);
                            }
                        }
                    }
                }

                // Add pasien dari dokter lain untuk dokter jaga yaitu dokter yg diset di service unit yg AllowAccessPatientWithServiceUnitParamedic
                //if (AppSession.UserLogin.SRUserType == UserLogin.UserType.Doctor && chkIsRegToOtherPhy.Checked)
                if (AppSession.UserLogin.SRUserType == UserLogin.UserType.Doctor && cboParamedicTeam.SelectedValue == "ALL")
                {
                    var dtbDrJg =
                        PatientInAllowAccessPatientWithServiceUnitParamedic(maxResultRecord, string.IsNullOrWhiteSpace(regType) ? new string[] { } : new string[] { regType }, dtb);
                    if (dtbDrJg != null)
                        dtb.Merge(dtbDrJg);
                }


                return dtb;
            }
        }

        #region Emergency Patient
        /// <summary>
        /// Patient Emergency adalah Patient yg bisa ditangani oleh semua dokter yg di maping ke unit nya
        /// </summary>
        /// <param name="maxResultRecord"></param>
        /// <returns></returns>
        //private DataTable RegistrationEmergencyPatient(int maxResultRecord)
        //{
        //    var unit = new ServiceUnitQuery("su");
        //    var room = new ServiceRoomQuery("c");
        //    var medic = new ParamedicQuery("d");
        //    var query = new RegistrationQuery("reg");
        //    var patient = new PatientQuery("f");
        //    var grr = new GuarantorQuery("g");
        //    var sumInfo = new RegistrationInfoSumaryQuery("h");
        //    var sal = new AppStandardReferenceItemQuery("sal");

        //    query.es.Top = maxResultRecord;

        //    query.Select
        //        (
        //            room.RoomName,
        //            query.RegistrationDate,
        //            unit.ServiceUnitID,
        //            unit.SRAssessmentType,
        //            // Ambil alih ParamedicID nya krn Emergency adalah patient keroyokan 
        //            // ParamedicID dipakai sbg tanda dientrian detilnya bahwa patient tsb sedang dihandle oleh Paramedic tsb
        //            string.Format("<{0} as ParamedicID>", AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor && !string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID) ? string.Format("'{0}'", AppSession.UserLogin.ParamedicID) : "reg.ParamedicID"),
        //            medic.ParamedicName, // Tetap display dokter saat registrasi
        //            query.RegistrationNo,
        //            patient.MedicalNo,
        //            patient.PatientName,
        //            patient.Sex,
        //            grr.GuarantorName,
        //            query.PatientID,
        //            query.IsConsul,
        //            query.SRRegistrationType,
        //            query.RoomID,
        //            "<'' AS BedID>",
        //            "<'' AS ReferFrom>",
        //            "<'' AS ReferFromRegistrationType>",
        //            "<'' AS ReferTo>",
        //            query.RegistrationTime,
        //            //query.RegistrationQue,
        //            query.IsConfirmedAttendance,
        //            query.IsNewPatient,
        //            sumInfo.NoteCount,
        //            query.SRTriage,
        //            "<CASE WHEN reg.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsParamedicNotNull>",
        //            "<ISNULL(reg.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
        //            unit.IsNeedConfirmationOfAttendance,
        //            query.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
        //            patient.DateOfBirth,
        //            sal.ItemName.As("SalutationName"),
        //            "<'EMG' AS RowSource>"
        //        );

        //    query.LeftJoin(room).On(query.RoomID == room.RoomID);
        //    query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
        //    query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
        //    query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
        //    query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo & sumInfo.NoteCount > 0);

        //    // Reg EmergencyPatient
        //    query.Where(query.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient, query.IsVoid == false);
        //    query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
        //    query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);

        //    if (cboServiceUnitID.SelectedValue == string.Empty)
        //    {
        //        if (AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse)
        //        {
        //            // Yg lainnya bisa lintas serviceUnit
        //            var qusr = new AppUserServiceUnitQuery("u");
        //            query.InnerJoin(qusr).On(
        //                query.ServiceUnitID == qusr.ServiceUnitID &
        //                qusr.UserID == AppSession.UserLogin.UserID
        //            );
        //        }
        //    }
        //    else
        //        query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);

        //    // Untuk dokter emergency (yg di mapping ke unit emergency) bisa melihat semua pasien yg ada di emergency
        //    if (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor)
        //    {
        //        if (chkIsRegToOtherPhy.Checked)
        //        {
        //            // All Patient in my ServiceUnit AllowAccessPatientWithServiceUnitParamedic
        //            var qsup = new ServiceUnitParamedicQuery("u");
        //            query.InnerJoin(qsup).On(
        //                query.ServiceUnitID == qsup.ServiceUnitID &
        //                qsup.ParamedicID == AppSession.UserLogin.ParamedicID & unit.IsAllowAccessPatientWithServiceUnitParamedic == true
        //            );
        //        }
        //        else
        //            // Just reg to me
        //            query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

        //    }
        //    else

        //    if (AppSession.UserLogin.SRUserType == AppUser.UserType.Physiotherapy) // u/ user fisio bisa lihat yg dikonsul 
        //    {
        //        var parteam = new ParamedicTeamQuery("pt");
        //        query.InnerJoin(parteam).On(query.RegistrationNo == parteam.RegistrationNo);
        //    }
        //    else if (cboParamedicID.SelectedValue != string.Empty)
        //        query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

        //    AddFilterRegistrationAndPatient(query, patient, AppConstant.RegistrationType.EmergencyPatient);

        //    var group = new esQueryItem(query, "Group", esSystemType.String);
        //    group = unit.ServiceUnitName;

        //    query.Select(group.As("Group"));

        //    //query.OrderBy
        //    //    (
        //    //        medic.ParamedicName.Ascending,
        //    //        query.RegistrationDate.Descending,
        //    //        query.RegistrationTime.Ascending,
        //    //        query.RegistrationNo.Descending
        //    //    );
        //    if (AppSession.Parameter.IsShowExternalQueue)
        //    {
        //        query.Select(query.ExternalQueNo.As("RegistrationQue"));
        //        query.OrderBy(query.RegistrationDate.Descending, query.ExternalQueNo.Ascending, query.RegistrationTime.Ascending);
        //    }
        //    else
        //    {
        //        query.Select(query.RegistrationQue);
        //        query.OrderBy(query.RegistrationDate.Descending, query.RegistrationQue.Ascending, query.RegistrationTime.Ascending);
        //    }
        //    return query.LoadDataTable();
        //}


        /// <summary>
        /// Patient yg di consul atau refer ke dokter lain
        /// Untuk Paramedic Team yg bisa dari entrian konsul atau di set di Paramedic Team
        /// Diquery hanya jika yg login dokter
        /// </summary>
        /// <param name="maxResultRecord"></param>
        /// <returns></returns>
        //private DataTable RegistrationEmergencyPatientSubstitutePhysician(int maxResultRecord, string paramedicID)
        //{
        //    var unit = new ServiceUnitQuery("b");
        //    var room = new ServiceRoomQuery("c");
        //    var medic = new ParamedicQuery("d");
        //    var reg = new RegistrationQuery("reg");
        //    var patient = new PatientQuery("f");
        //    var grr = new GuarantorQuery("g");
        //    var parteam = new ParamedicTeamQuery("pt");
        //    var sumInfo = new RegistrationInfoSumaryQuery("h");
        //    var sal = new AppStandardReferenceItemQuery("sal");

        //    reg.es.Top = maxResultRecord;

        //    reg.Select
        //        (
        //            room.RoomName,
        //            reg.RegistrationDate,
        //            unit.ServiceUnitID,
        //            unit.SRAssessmentType,
        //            medic.ParamedicID,
        //            medic.ParamedicName,
        //            reg.RegistrationNo,
        //            patient.MedicalNo,
        //            patient.PatientName,
        //            patient.Sex,
        //            grr.GuarantorName,
        //            reg.PatientID,
        //            reg.IsConsul,
        //            reg.SRRegistrationType,
        //            reg.RoomID,
        //            "<'' AS BedID>",
        //            "<'' AS ReferFrom>",
        //            "<'' AS ReferFromRegistrationType>",
        //            "<'' AS ReferTo>",
        //            reg.RegistrationTime,
        //            reg.IsConfirmedAttendance,
        //            reg.IsNewPatient,
        //            sumInfo.NoteCount,
        //            reg.SRTriage,
        //            "<CASE WHEN reg.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsParamedicNotNull>",
        //            "<ISNULL(reg.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
        //            unit.IsNeedConfirmationOfAttendance,
        //        reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
        //        patient.DateOfBirth,
        //            sal.ItemName.As("SalutationName"),
        //            "<'EMGT' AS RowSource>"
        //        );

        //    reg.LeftJoin(room).On(reg.RoomID == room.RoomID);
        //    reg.InnerJoin(parteam).On(reg.RegistrationNo == parteam.RegistrationNo & parteam.ParamedicID == paramedicID);
        //    reg.InnerJoin(medic).On(parteam.ParamedicID == medic.ParamedicID);
        //    reg.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
        //    reg.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
        //    reg.LeftJoin(sumInfo).On(reg.RegistrationNo == sumInfo.RegistrationNo & sumInfo.NoteCount > 0);
        //    reg.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

        //    reg.Where(reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient, reg.IsVoid == false);

        //    if (cboServiceUnitID.SelectedValue != string.Empty)
        //    {
        //        reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
        //        reg.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);
        //    }
        //    else
        //        reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);


        //    AddFilterRegistrationAndPatient(reg, patient, AppConstant.RegistrationType.EmergencyPatient);

        //    var group = new esQueryItem(reg, "Group", esSystemType.String);
        //    group = unit.ServiceUnitName;

        //    reg.Select(group.As("Group"));

        //    //reg.OrderBy
        //    //    (
        //    //        reg.RegistrationDate.Descending,
        //    //        reg.RegistrationTime.Ascending,
        //    //        reg.RegistrationNo.Descending
        //    //    );
        //    if (AppSession.Parameter.IsShowExternalQueue)
        //    {
        //        reg.Select(reg.ExternalQueNo.As("RegistrationQue"));
        //        reg.OrderBy(reg.RegistrationDate.Descending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);
        //    }
        //    else
        //    {
        //        reg.Select(reg.RegistrationQue);
        //        reg.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.RegistrationTime.Ascending);
        //    }
        //    return reg.LoadDataTable();
        //}

        #endregion

        private DataTable RegistrationNonIp(int maxResultRecord, string[] regTypes)
        {
            var reg = new RegistrationQuery("reg");
            var room = new ServiceRoomQuery("c");
            reg.LeftJoin(room).On(reg.RoomID == room.RoomID);

            var medic = new ParamedicQuery("m");
            reg.InnerJoin(medic).On(reg.ParamedicID == medic.ParamedicID);

            var patient = new PatientQuery("f");
            reg.InnerJoin(patient).On(reg.PatientID == patient.PatientID);

            var unit = new ServiceUnitQuery("b");
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);

            var grr = new GuarantorQuery("g");
            reg.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);

            var sal = new AppStandardReferenceItemQuery("sal");
            reg.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

            reg.es.Top = maxResultRecord;

            reg.Select
                (
                    room.RoomName,
                    reg.RegistrationDate,
                    unit.ServiceUnitID,
                    // ParamedicID dipakai sbg parameter dientrian detilnya bahwa patient tsb sedang dihandle oleh Paramedic tsb
                    string.Format("<{0} as ParamedicID>", (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor || AppSession.UserLogin.SRUserType == AppUser.UserType.Physiotherapy)
                                                         && !string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID)
                        ? string.Format("'{0}'", AppSession.UserLogin.ParamedicID)
                        : "reg.ParamedicID"),
                    medic.ParamedicName, // Tetap display dokter saat registrasi
                    reg.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    patient.Sex,
                    grr.GuarantorName,
                    reg.PatientID,
                    reg.IsConsul,
                    reg.SRRegistrationType,
                    reg.RoomID,
                    "<'' AS BedID>",
                    "<'' AS SRBedStatus>",
                    "<'' AS ReferFrom>",
                    "<'' AS ReferFromRegistrationType>",
                    "<'' AS ReferTo>",
                    reg.RegistrationTime,
                    reg.IsConfirmedAttendance,
                    reg.IsNewPatient,
                    "<'' AS SRTriage>",
                    "<CASE WHEN reg.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsParamedicNotNull>",
                    "<ISNULL(reg.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                    unit.IsNeedConfirmationOfAttendance,
                    reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                    patient.DateOfBirth,
                    sal.ItemName.As("SalutationName"),
                    "<'NIP-'+reg.SRRegistrationType AS RowSource>",
                    reg.SRCovidStatus,
                    reg.SRPatientRiskStatus,
                    reg.SRDischargeCondition,
                    @"<CASE WHEN reg.SRRegistrationType = 'EMR' THEN ((CASE WHEN reg.ParamedicID = '" + AppParameter.GetParameterValue(AppParameter.ParameterItem.DoctorOnDutyId) + @"' THEN 'true' ELSE 'false' END)) ELSE 'false' END AS IsDoctorOnDuty>",
                    "<ISNULL(reg.IsFinishedAttendance, 0) AS IsFinishedAttendance>",
                    reg.SRPatientRiskColor,
                    patient.IsAlive,
                    patient.DeceasedDateTime
                );

            // IsDoctorOnDuty untuk kondisi popup konfirmasi mengambil alih pasien (takeover)

            if (AppSession.Parameter.IsCrmMembershipActive)
                reg.Select(@"<CASE WHEN ISNULL(reg.MembershipNo, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsVipMember'>");
            else
                reg.Select(@"<CAST(0 AS BIT) AS 'IsVipMember'>");

            if (regTypes.Length == 1)
                reg.Where(reg.SRRegistrationType == regTypes[0]);
            else if (regTypes.Length > 1)
                reg.Where(reg.SRRegistrationType.In(regTypes));

            AddFilterServiceUnitAndParamedic(reg, false);
            AddFilterRegistrationAndPatient(reg, patient, regTypes);

            var group = new esQueryItem(reg, "Group", esSystemType.String);
            group = unit.ServiceUnitName;
            reg.Select(group.As("Group"));

            //reg.OrderBy
            //    (
            //reg.ParamedicID.Ascending,
            //reg.RegistrationDate.Descending
            //reg.RegistrationTime.Ascending,
            //reg.RegistrationNo.Descending,
            //reg.RegistrationQue.Ascending
            //);

            //if (AppSession.Parameter.IsShowExternalQueue)
            //{
            //    reg.Select(reg.ExternalQueNo.As("RegistrationQue"));
            //    reg.OrderBy(reg.RegistrationDate.Descending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);
            //}
            //else
            //{
            //    reg.Select(reg.RegistrationQue);
            //    reg.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.RegistrationTime.Ascending);
            //}

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsUsingKioskQueNoFormat))  //Untuk RSI Tegal , menyesuaikan format QueNo seperti di Menu Registrasi
            {
                var appt = new AppointmentQuery("appt");
                reg.LeftJoin(appt).On(appt.AppointmentNo == reg.AppointmentNo);
                var appttype = new AppStandardReferenceItemQuery("appttype");
                reg.LeftJoin(appttype).On(appttype.StandardReferenceID == "AppoinmentType" && appttype.ItemID == appt.SRAppoinmentType);
                var apptQue = new AppointmentQueueingQuery("apptQue");
                reg.LeftJoin(apptQue).On(appt.AppointmentNo == apptQue.AppointmentNo && apptQue.SRQueueingGroup == "01");

                reg.Select(@"<apptQue.FormattedNo>", reg.RegistrationQue, reg.ExternalQueNo);
                reg.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);
            }
            else
            {
                reg.Select(@"<'' AS FormattedNo>", reg.RegistrationQue, reg.ExternalQueNo);
                reg.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);
            }

            var dtb = reg.LoadDataTable();

            // Update aditional
            SetAdditionalFieldOutPatient(dtb);

            return dtb;
        }

        private void AddFilterServiceUnitAndParamedic(RegistrationQuery reg, bool isInPatient)
        {
            if (cboServiceUnitID.SelectedValue == string.Empty)
            {
                if (AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse || AppSession.UserLogin.SRUserType == AppUser.UserType.Physiotherapy) // Hanya yg diset di Usernya
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    if (AppSession.UserLogin.SRUserType == AppUser.UserType.Physiotherapy && !string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID))
                    {
                        reg.LeftJoin(qusr).On(reg.ServiceUnitID == qusr.ServiceUnitID & qusr.UserID == AppSession.UserLogin.UserID);
                        // Jika Physiotherapy merupakan Paramedic maka ambil juga di ParamedicTeam yg merupakan hasil proses consult 
                        var parteam = new ParamedicTeamQuery("pt");
                        reg.LeftJoin(parteam).On(reg.RegistrationNo == parteam.RegistrationNo);

                        reg.Where(reg.Or(parteam.ParamedicID == AppSession.UserLogin.ParamedicID, qusr.ServiceUnitID.IsNotNull()));
                    }
                    else
                        reg.InnerJoin(qusr).On(reg.ServiceUnitID == qusr.ServiceUnitID & qusr.UserID == AppSession.UserLogin.UserID);
                }
            }
            else
                reg.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);

            //// Filter dgn dokter diregistrasi
            //if (AppSession.UserLogin.SRUserType == UserLogin.UserType.Doctor && !string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID))
            //{
            //    // Jika loginnya Doctor 
            //    // cboParamedicID.SelectedValue akan terisi oleh AppSession.UserLogin.ParamedicID
            //    if (chkIsRegToOtherPhy.Checked && isInPatient)
            //    {
            //        // My Patient in my team 
            //        // InPatient dokter saat registrasi selalu ada di ParamedicTeam
            //        var parteam = new ParamedicTeamQuery("pt");
            //        reg.InnerJoin(parteam).On(reg.RegistrationNo == parteam.RegistrationNo);
            //        reg.Where(parteam.ParamedicID == AppSession.UserLogin.ParamedicID);
            //    }
            //    else if (chkIsRegToOtherPhy.Checked && !isInPatient)
            //    {
            //        // My Patient in my team and in my reg
            //        // Non InPatient dokter saat registrasi tidak ada di ParamedicTeam, shg harus diselect juga dari reg
            //        var parteam = new ParamedicTeamQuery("pt");
            //        reg.LeftJoin(parteam).On(reg.RegistrationNo == parteam.RegistrationNo);
            //        reg.Where(reg.Or(reg.ParamedicID == AppSession.UserLogin.ParamedicID, parteam.ParamedicID == AppSession.UserLogin.ParamedicID));
            //    }
            //    else
            //        // Just reg to me
            //        // cboParamedicID.SelectedValue == AppSession.UserLogin.ParamedicID jika loginnya dokter (lihat di init)
            //        reg.Where(reg.ParamedicID == AppSession.UserLogin.ParamedicID);
            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
            //    {
            //        // Login selain Doctor hanya memunculkan pasien dg dokter saat registrasi saja
            //        reg.Where(reg.ParamedicID == cboParamedicID.SelectedValue);
            //    }
            //}

            // Filter dgn dokter 
            if (AppSession.UserLogin.SRUserType == UserLogin.UserType.Doctor && !string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID))
            {
                // Jika loginnya Doctor 
                if (cboParamedicTeam.SelectedValue == "REGTOME")
                {
                    // Just reg to me
                    // cboParamedicID.SelectedValue == AppSession.UserLogin.ParamedicID jika loginnya dokter (lihat di init)
                    reg.Where(reg.ParamedicID == AppSession.UserLogin.ParamedicID);
                }
                else if (isInPatient)
                {
                    // My Patient in my team 
                    // InPatient dokter saat registrasi selalu ada di ParamedicTeam
                    var parteam = new ParamedicTeamQuery("pt");
                    reg.InnerJoin(parteam).On(reg.RegistrationNo == parteam.RegistrationNo);
                    reg.Where(parteam.ParamedicID == AppSession.UserLogin.ParamedicID);
                    if (!string.IsNullOrWhiteSpace(cboParamedicTeam.SelectedValue) && cboParamedicTeam.SelectedValue != "ALL")
                    {
                        reg.Where(parteam.SRParamedicTeamStatus == cboParamedicTeam.SelectedValue);
                    }
                }
                else
                {
                    // My Patient in my team and in my reg
                    // Non InPatient dokter saat registrasi tidak ada di ParamedicTeam, shg harus diselect juga dari reg
                    var parteam = new ParamedicTeamQuery("pt");
                    reg.LeftJoin(parteam).On(reg.RegistrationNo == parteam.RegistrationNo);
                    reg.Where(reg.Or(reg.ParamedicID == AppSession.UserLogin.ParamedicID, parteam.ParamedicID == AppSession.UserLogin.ParamedicID));
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                {
                    // Login selain Doctor hanya memunculkan pasien dg dokter saat registrasi saja
                    reg.Where(reg.ParamedicID == cboParamedicID.SelectedValue);
                }
            }
        }

        private static void SetAdditionalFieldOutPatient(DataTable dtb)
        {
            // Triage status
            foreach (DataRow row in dtb.Rows)
            {
                // Refer From
                if (row["FromRegistrationNo"] != null &&
                    !string.IsNullOrWhiteSpace(row["FromRegistrationNo"].ToString()))
                {
                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(row["FromRegistrationNo"].ToString()))
                    {
                        row["ReferFrom"] = string.Format("{0} ({1})", Paramedic.GetParamedicName(reg.ParamedicID), ServiceUnit.GetServiceUnitName(reg.ServiceUnitID));
                        row["ReferFromRegistrationType"] = reg.SRRegistrationType;
                    }
                }

                // TODO: Refer To di List EMR cek lagi
                // Refer To
                // Ambil dari MergeBilling 
                // Tidak diambil dari paramedicConsul krn data lama belum ada di ParamedicConsul 
                var referTo = string.Empty;
                var mbcoll = new MergeBillingCollection();
                mbcoll.Query.Where(mbcoll.Query.FromRegistrationNo == row["RegistrationNo"].ToString());
                mbcoll.LoadAll();
                foreach (var c in mbcoll)
                {
                    var r = new Registration();
                    r.LoadByPrimaryKey(c.RegistrationNo);
                    if (r.IsVoid == false)
                    {
                        if (!string.IsNullOrEmpty(r.ParamedicID))
                        {
                            var p = new Paramedic();
                            p.LoadByPrimaryKey(r.ParamedicID);
                            referTo += p.ParamedicName + ";";
                        }
                    }
                }

                if (referTo != string.Empty)
                    referTo = referTo.Remove(referTo.Length - 1);

                row["ReferTo"] = referTo;


                // Triage Rawat Jalan diisi status Pasien rawat jalan yg sudah dilakukan pemeriksaan Vital Sign
                if (VitalSign.LastVitalSignDate(row["RegistrationNo"].ToString(), row["FromRegistrationNo"].ToString()) !=
                    new DateTime(1900, 1, 1))
                {
                    row["SRTriage"] =
                        "99"; // pasien rawat jalan yg sudah dilakukan pemeriksaan Vital Sign / Physical Examamination
                }
            }

            dtb.AcceptChanges();
        }

        // In Paramedic Team
        //private DataTable RegistrationOutPatientSubstitutePhysician(int maxResultRecord, string paramedicID)
        //{
        //    var unit = new ServiceUnitQuery("b");
        //    var room = new ServiceRoomQuery("c");
        //    var medic = new ParamedicQuery("d");
        //    var reg = new RegistrationQuery("e");
        //    var patient = new PatientQuery("f");
        //    var grr = new GuarantorQuery("g");
        //    var parteam = new ParamedicTeamQuery("pt");
        //    var mb = new MergeBillingQuery("mb");
        //    var rmb = new RegistrationQuery("rmb");
        //    var pmb = new ParamedicQuery("pmb");
        //    var sumInfo = new RegistrationInfoSumaryQuery("h");
        //    var sal = new AppStandardReferenceItemQuery("sal");

        //    reg.es.Top = maxResultRecord;

        //    reg.Select
        //        (
        //            room.RoomName,
        //            reg.RegistrationDate,
        //            //reg.RegistrationQue,
        //            unit.ServiceUnitID,
        //            unit.SRAssessmentType,
        //            medic.ParamedicID,
        //            medic.ParamedicName,
        //            reg.RegistrationNo,
        //            patient.MedicalNo,
        //            patient.PatientName,
        //            patient.Sex,
        //            grr.GuarantorName,
        //            reg.PatientID,
        //            reg.IsConsul,
        //            reg.SRRegistrationType,
        //            reg.RoomID,
        //            "<'' AS BedID>",
        //            pmb.ParamedicName.As("ReferFrom"),
        //            rmb.SRRegistrationType.As("ReferFromRegistrationType"),
        //            "<'' AS ReferTo>",
        //            reg.RegistrationTime,
        //            //reg.RegistrationQue,
        //            reg.IsConfirmedAttendance,
        //            reg.IsNewPatient,
        //            sumInfo.NoteCount,
        //            "<'' AS SRTriage>",
        //            @"<CASE WHEN e.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsParamedicNotNull>",
        //            "<ISNULL(e.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
        //            unit.IsNeedConfirmationOfAttendance,
        //        reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
        //        patient.DateOfBirth,
        //            sal.ItemName.As("SalutationName"),
        //            "<'OPRT' AS RowSource>"
        //        );
        //    if (AppSession.Parameter.IsShowExternalQueue)
        //    {
        //        reg.Select(reg.ExternalQueNo.As("RegistrationQue"));
        //    }
        //    else
        //    {
        //        reg.Select(reg.RegistrationQue);
        //    }

        //    reg.LeftJoin(room).On(reg.RoomID == room.RoomID);
        //    reg.InnerJoin(parteam).On(reg.RegistrationNo == parteam.RegistrationNo & parteam.ParamedicID == paramedicID);
        //    reg.InnerJoin(medic).On(parteam.ParamedicID == medic.ParamedicID);
        //    reg.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
        //    reg.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
        //    reg.InnerJoin(mb).On(reg.RegistrationNo == mb.RegistrationNo);
        //    reg.LeftJoin(rmb).On(mb.FromRegistrationNo == rmb.RegistrationNo);
        //    reg.LeftJoin(pmb).On(rmb.ParamedicID == pmb.ParamedicID);
        //    reg.LeftJoin(sumInfo).On(reg.RegistrationNo == sumInfo.RegistrationNo & sumInfo.NoteCount > 0);
        //    reg.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
        //    reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
        //    reg.Where(reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient, reg.IsVoid == false, reg.IsFromDispensary == false);

        //    reg.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);

        //    AddFilterRegistrationAndPatient(reg, patient, AppConstant.RegistrationType.OutPatient);

        //    var group = new esQueryItem(reg, "Group", esSystemType.String);
        //    group = unit.ServiceUnitName;

        //    reg.Select(group.As("Group"));

        //    reg.OrderBy
        //        (
        //            reg.RegistrationDate.Descending,
        //            reg.RegistrationTime.Ascending,
        //            reg.RegistrationNo.Descending,
        //            reg.RegistrationQue.Ascending
        //        );

        //    var dtb = reg.LoadDataTable();

        //    // Update aditional
        //    SetAdditionalFieldOutPatient(dtb);

        //    return dtb;
        //}

        private DataTable PatientInPatient(int maxResultRecord)
        {
            var reg = new RegistrationQuery("reg");
            var unit = new ServiceUnitQuery("b");
            var room = new ServiceRoomQuery("c");
            var patient = new PatientQuery("p");
            var grr = new GuarantorQuery("g");
            var sal = new AppStandardReferenceItemQuery("sal");

            var medic = new ParamedicQuery("medic");
            reg.InnerJoin(medic).On(reg.ParamedicID == medic.ParamedicID);

            reg.es.Top = maxResultRecord;


            // BED 
            // IsNeedConfirmation == true -> tampilkan hanya pasien yg SRBedStatus != AppSession.Parameter.BedStatusPending & SRBedStatus != AppSession.Parameter.BedStatusBooked
            // IsNeedConfirmation == false -> tidak usah cek SRBedStatus
            var bed = new BedQuery("bd");
            if (!chkIsIncludeNotInBed.Checked)
            {
                if (AppParameter.IsYes(AppParameter.ParameterItem.IsUsingRoomingIn))
                {
                    reg.InnerJoin(bed).On(reg.BedID == bed.BedID);
                    reg.Where(reg.DischargeDate.IsNull(), reg.Or(reg.IsRoomIn == 1, bed.RegistrationNo == reg.RegistrationNo), reg.Or(bed.IsNeedConfirmation != true, reg.And(bed.IsNeedConfirmation == true, bed.SRBedStatus != AppSession.Parameter.BedStatusPending, bed.SRBedStatus != AppSession.Parameter.BedStatusBooked)));
                }
                else
                {
                    // Just patient in room
                    reg.InnerJoin(bed).On(reg.RegistrationNo == bed.RegistrationNo);
                    reg.Where(reg.Or(bed.IsNeedConfirmation != true, reg.And(bed.IsNeedConfirmation == true, bed.SRBedStatus != AppSession.Parameter.BedStatusPending, bed.SRBedStatus != AppSession.Parameter.BedStatusBooked)));
                }
            }
            else
            {
                reg.LeftJoin(bed).On(reg.RegistrationNo == bed.RegistrationNo);

                //if (row["RegistrationNo"].Equals(row["BedRegistrationNo"]) && row["IsNeedConfirmation"].Equals(true) &&  row["SRBedStatus"].Equals(AppSession.Parameter.BedStatusPending))
                //reg.Where(reg.Or(bed.IsNeedConfirmation == false, bed.IsNeedConfirmation.IsNull(), reg.And(bed.IsNeedConfirmation == true, reg.RegistrationNo == bed.RegistrationNo, bed.SRBedStatus != AppSession.Parameter.BedStatusPending, bed.SRBedStatus != AppSession.Parameter.BedStatusBooked)));


                // Untuk inpateint yg baru masuk RS(masih open), ambil yg statusnya bukan pending jika IsNeedConfirmation = true untuk mencegah transaksi, krn kalau yg sudah close sudah tidak bisa
                // Jika pasiennya masih ada di RS dgn tanda No Reg nya masih terdaftar di Bed maka cek status bed nya (IsNeedConfirmation, !BedStatusPending, !BedStatusBooked)
                reg.Where(
                    reg.Or(bed.RegistrationNo.IsNull(), reg.RegistrationNo != bed.RegistrationNo, // Sudah tidak ada di Bed
                        reg.And(bed.IsNeedConfirmation != true, reg.RegistrationNo == bed.RegistrationNo),
                        reg.And(bed.IsNeedConfirmation == true, reg.RegistrationNo == bed.RegistrationNo, bed.SRBedStatus != AppSession.Parameter.BedStatusPending, bed.SRBedStatus != AppSession.Parameter.BedStatusBooked)
                        ));
            }

            reg.Select
                (
                    room.RoomName,
                    reg.RegistrationDate,
                    reg.ServiceUnitID,

                    // ParamedicID dipakai sbg parameter dientrian detilnya bahwa patient tsb sedang dihandle oleh Paramedic tsb
                    string.Format("<{0} as ParamedicID>", (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor || AppSession.UserLogin.SRUserType == AppUser.UserType.Physiotherapy)
                                                         && !string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID)
                        ? string.Format("'{0}'", AppSession.UserLogin.ParamedicID)
                        : "reg.ParamedicID"),
                    medic.ParamedicName, // Tetap display dokter saat registrasi

                    reg.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    patient.Sex,
                    grr.GuarantorName,
                    reg.PatientID,
                    reg.IsConsul,
                    reg.SRRegistrationType,
                    reg.RoomID,
                    reg.BedID,
                    bed.SRBedStatus,
                    "<'' AS ReferFrom>",
                    "<'' AS ReferFromRegistrationType>",
                    "<'' AS ReferTo>",
                    reg.RegistrationTime,
                    reg.IsConfirmedAttendance,
                    reg.IsNewPatient,
                    "<'' AS SRTriage>",
                    "<CASE WHEN reg.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsParamedicNotNull>",
                    "<ISNULL(reg.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                    unit.IsNeedConfirmationOfAttendance,
                    reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                    patient.DateOfBirth,
                    sal.ItemName.As("SalutationName"),
                    "<'IPR' AS RowSource>",
                    reg.SRCovidStatus,
                    reg.SRPatientRiskStatus,
                    reg.SRDischargeCondition,
                    @"<'false' AS IsDoctorOnDuty>",
                    "<ISNULL(reg.IsFinishedAttendance, 0) AS IsFinishedAttendance>",
                    reg.SRPatientRiskColor,
                    patient.IsAlive,
                    patient.DeceasedDateTime
                );
            if (AppSession.Parameter.IsCrmMembershipActive)
                reg.Select(@"<CASE WHEN ISNULL(reg.MembershipNo, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsVipMember'>");
            else
                reg.Select(@"<CAST(0 AS BIT) AS 'IsVipMember'>");

            reg.InnerJoin(room).On(reg.RoomID == room.RoomID);
            reg.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            reg.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
            reg.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            reg.Where(reg.SRRegistrationType == AppConstant.RegistrationType.InPatient);

            AddFilterServiceUnitAndParamedic(reg, true);

            AddFilterRegistrationAndPatient(reg, patient, new string[] { AppConstant.RegistrationType.InPatient });

            // dokter ybs sudah entry soap

            if (AppSession.UserLogin.SRUserType == UserLogin.UserType.Doctor)
            {
                if (!chkIprIsSoapInputted.Checked)
                {
                    var rim = new RegistrationInfoMedicQuery("rim");
                    reg.LeftJoin(rim).On(reg.RegistrationNo == rim.RegistrationNo && rim.DateTimeInfo.Date() == DateTime.Now.Date && rim.ParamedicID == AppSession.UserLogin.ParamedicID)
                        .Where(rim.RegistrationInfoMedicID.IsNull());
                    reg.es.Distinct = true;
                }
            }

            if (trSmfFilter.Visible && cboSmf.SelectedValue != string.Empty)
            {
                reg.Where(reg.SmfID == cboSmf.SelectedValue);
            }

            reg.Select(unit.ServiceUnitName.As("Group"));

            reg.Select(@"<'' AS FormattedNo>", reg.RegistrationQue, reg.ExternalQueNo);
            reg.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);

            var dtb = reg.LoadDataTable();

            return dtb; // DeleteInPatientBedStatusPendingAndNotInBed(dtb);
        }

        private DataTable DeleteInPatientBedStatusPendingAndNotInBed(DataTable dtb)
        {
            // Bersihkan row pasien rawat inap
            // Hapus yg belum diakui sebagai patient rawat inap krn tidak boleh melakukan transaksi 
            foreach (DataRow row in dtb.Rows)
            {
                if (!row["SrRegistrationType"].Equals(AppConstant.RegistrationType.InPatient)) continue;

                // Hapus yg belum diconfirm jika harus diconfirm dulu
                if (row["RegistrationNo"].Equals(row["BedRegistrationNo"]) && row["IsNeedConfirmation"].Equals(true) && row["SRBedStatus"].Equals(AppSession.Parameter.BedStatusPending))
                {
                    row.Delete();
                    continue;
                }

                // Jika hanya yg masih ada dibed maka hapus yg sudah tidak ada
                if (!chkIsIncludeNotInBed.Checked && !row["RegistrationNo"].Equals(row["BedRegistrationNo"]))
                {
                    row.Delete();
                }
            }
            dtb.AcceptChanges();
            return dtb;
        }

        #region Pasien SrviceUnitBooking / Kamar Operasi / OK
        private DataTable PatientServiceUnitBookingPhyMain(int maxResultRecord)
        {
            var query = new ServiceUnitBookingQuery("a");
            var medic = new ParamedicQuery("d");

            PatientServiceUnitBookingQuery(query, medic, maxResultRecord, "PM");
            query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);

            if (cboParamedicID.SelectedValue != string.Empty)
                query.Where(query.ParamedicID == cboParamedicID.SelectedValue && query.IsVoid == false);

            return query.LoadDataTable();
        }
        private DataTable PatientServiceUnitBookingPhy2(int maxResultRecord)
        {
            var query = new ServiceUnitBookingQuery("a");
            var medic = new ParamedicQuery("d");

            PatientServiceUnitBookingQuery(query, medic, maxResultRecord, "P2");
            query.LeftJoin(medic).On(query.ParamedicID2 == medic.ParamedicID);

            if (cboParamedicID.SelectedValue != string.Empty)
                query.Where(query.ParamedicID2 == cboParamedicID.SelectedValue && query.IsVoid == false);

            return query.LoadDataTable();
        }
        private DataTable PatientServiceUnitBookingPhy3(int maxResultRecord)
        {
            var query = new ServiceUnitBookingQuery("a");
            var medic = new ParamedicQuery("d");

            PatientServiceUnitBookingQuery(query, medic, maxResultRecord, "P3");
            query.LeftJoin(medic).On(query.ParamedicID3 == medic.ParamedicID);

            if (cboParamedicID.SelectedValue != string.Empty)
                query.Where(query.ParamedicID3 == cboParamedicID.SelectedValue && query.IsVoid == false);

            return query.LoadDataTable();
        }
        private DataTable PatientServiceUnitBookingPhy4(int maxResultRecord)
        {

            var query = new ServiceUnitBookingQuery("a");
            var medic = new ParamedicQuery("d");

            PatientServiceUnitBookingQuery(query, medic, maxResultRecord, "P4");
            query.LeftJoin(medic).On(query.ParamedicID4 == medic.ParamedicID);

            if (cboParamedicID.SelectedValue != string.Empty)
                query.Where(query.ParamedicID4 == cboParamedicID.SelectedValue && query.IsVoid == false);

            return query.LoadDataTable();

        }
        private DataTable PatientServiceUnitBookingAnes(int maxResultRecord)
        {

            var query = new ServiceUnitBookingQuery("a");
            var medic = new ParamedicQuery("m");

            PatientServiceUnitBookingQuery(query, medic, maxResultRecord, "PA");
            query.LeftJoin(medic).On(query.ParamedicIDAnestesi == medic.ParamedicID);

            if (cboParamedicID.SelectedValue != string.Empty)
                query.Where(query.ParamedicIDAnestesi == cboParamedicID.SelectedValue && query.IsVoid == false);

            return query.LoadDataTable();
        }
        private void PatientServiceUnitBookingQuery(ServiceUnitBookingQuery query, ParamedicQuery medic, int maxResultRecord, string paramedicType)
        {
            var parTypeName = string.Empty;
            switch (paramedicType)
            {
                case "PM":
                    parTypeName = "OK Main Paramedic";
                    break;
                case "P2":
                    parTypeName = "OK Paramedic 2";
                    break;
                case "P3":
                    parTypeName = "OK Paramedic 3";
                    break;
                case "P4":
                    parTypeName = "OK Paramedic 4";
                    break;
                case "PA":
                    parTypeName = "Anestesi";
                    break;
                default:
                    break;
            }

            var unit = new ServiceUnitQuery("b");
            var room = new ServiceRoomQuery("c");
            var reg = new RegistrationQuery("reg");
            var patient = new PatientQuery("f");
            var grr = new GuarantorQuery("g");
            var sal = new AppStandardReferenceItemQuery("sal");

            //var file = new MedicalRecordFileStatusMovementQuery("z");
            query.es.Top = maxResultRecord;
            query.es.Distinct = true;


            query.Select
            (
                room.RoomName,
                reg.RegistrationDate,
                query.ServiceUnitID,
                medic.ParamedicID,
                (medic.ParamedicName + " (" + parTypeName + ")").As("ParamedicName"),
                query.RegistrationNo,
                patient.MedicalNo,
                patient.PatientName,
                patient.Sex,
                grr.GuarantorName,
                reg.PatientID,
                reg.IsConsul,
                reg.SRRegistrationType,
                query.RoomID,
                "<'' AS BedID>",
                "<'' AS SRBedStatus>",
                "<'' AS ReferFrom>",
                "<'' AS ReferFromRegistrationType>",
                "<'' AS ReferTo>",
                reg.RegistrationTime,
                //reg.RegistrationQue,
                reg.IsConfirmedAttendance,
                reg.IsNewPatient,
                "<'' AS SRTriage>",
                "<CASE WHEN reg.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsParamedicNotNull>",
                "<ISNULL(reg.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                unit.IsNeedConfirmationOfAttendance,
                reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                patient.DateOfBirth,
                sal.ItemName.As("SalutationName"),
                "<'OK_" + paramedicType + "' AS RowSource>",
                reg.SRCovidStatus,
                reg.SRPatientRiskStatus,
                reg.SRDischargeCondition,
                @"<'false' AS IsDoctorOnDuty>",
                "<ISNULL(reg.IsFinishedAttendance, 0) AS IsFinishedAttendance>",
                reg.SRPatientRiskColor,
                patient.IsAlive,
                patient.DeceasedDateTime
            );
            if (AppSession.Parameter.IsCrmMembershipActive)
                query.Select(@"<CASE WHEN ISNULL(reg.MembershipNo, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsVipMember'>");
            else
                query.Select(@"<CAST(0 AS BIT) AS 'IsVipMember'>");

            query.LeftJoin(room).On(query.RoomID == room.RoomID);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);

            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
            query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);

            query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

            if (!string.IsNullOrWhiteSpace(cboRegistrationType.SelectedValue))
            {
                query.Where(reg.SRRegistrationType == cboRegistrationType.SelectedValue);
            }

            // Reg To Me
            if (cboParamedicTeam.SelectedValue == "REGTOME" && !string.IsNullOrWhiteSpace(cboParamedicID.SelectedValue))
                query.Where(reg.ParamedicID == cboParamedicID.SelectedValue);

            if (cboServiceUnitID.SelectedValue == string.Empty)
            {
                if (AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse)
                {
                    // Yg lainnya bisa lintas serviceUnit
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(
                        query.ServiceUnitID == qusr.ServiceUnitID &
                        qusr.UserID == AppSession.UserLogin.UserID
                    );
                }
            }
            else
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);


            if (txtRegistrationNo.Text != string.Empty)
            {
                string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                if (searchReg.ToLower().Contains("reg"))
                    query.Where(query.RegistrationNo == searchReg);
                //else
                //{
                //    if (AppSession.Parameter.IsMedicalNoContainStrip)
                //        query.Where(
                //            query.Or(
                //                query.RegistrationNo == searchReg,
                //                patient.MedicalNo == searchReg,
                //                patient.OldMedicalNo == searchReg,
                //                string.Format("< OR REPLACE(f.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                //                string.Format("< OR REPLACE(f.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                //            )
                //        );
                //    else
                //        query.Where(
                //        query.Or(
                //            query.RegistrationNo == searchReg,
                //            patient.MedicalNo == searchReg,
                //            patient.OldMedicalNo == searchReg
                //        )
                //    );
                //}

            }

            //if (txtPatientName.Text != string.Empty)
            //{
            //    //string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
            //    //query.Where(string.Format(
            //    //    "<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>",
            //    //    searchPatient));
            //}

            // Filter PatientID jika didepan ada patient nya berdasarkan pencarin nama atau MedicalNo
            if (_patientIdSearchs !=null &&_patientIdSearchs.Length > 0)
                query.Where(query.PatientID.In(_patientIdSearchs), reg.PatientID.In(_patientIdSearchs));

            if (!txtRegistrationDate.IsEmpty)
                query.Where(string.Format("<CAST(RealizationDateTimeFrom as DATE) = CAST('{0}' AS DATE)>",
                    txtRegistrationDate.SelectedDate.Value.Date.ToString(AppConstant.DisplayFormat.DateSql)));


            var group = new esQueryItem(query, "Group", esSystemType.String);
            group = unit.ServiceUnitName;

            query.Select(group.As("Group"));
            query.Where
            (
                //query.IsApproved == true, // Abaikan krn sudah ada no reg nya berarti sudah bisa dibuat laporannya
                reg.IsVoid == false && query.IsVoid == false
            //,query.IsExtendedSurgery == false //bikin repot, dokter komplen koq gak ada pasiennya
            );

            //query.OrderBy
            //(
            //    medic.ParamedicID.Ascending,
            //    reg.RegistrationDate.Descending,
            //    query.RegistrationNo.Ascending
            //);
            //if (AppSession.Parameter.IsShowExternalQueue)
            //{
            //    query.Select(reg.ExternalQueNo.As("RegistrationQue"));
            //    query.OrderBy(reg.RegistrationDate.Descending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);
            //}
            //else
            //{
            //    query.Select(reg.RegistrationQue);
            //    query.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.RegistrationTime.Ascending);
            //}
            query.Select(reg.RegistrationQue, reg.ExternalQueNo);
            query.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);
        }
        #endregion

        private DataTable PatientExamOrder(string regType, int maxResultRecord)
        {
            //Filter Service Unit nya dari Service Unit yg dituju (ToServiceUnitID) 
            var dtbResult = new DataTable();
            if (string.IsNullOrEmpty(regType))
            {
                // Load non in patient
                var dtbNonIpr = PatientExamOrder(string.Empty, maxResultRecord, dtbResult);
                dtbResult.Merge(dtbNonIpr);
                maxResultRecord = maxResultRecord - dtbNonIpr.Rows.Count;

                // Load in patient
                if (maxResultRecord > 0)
                {
                    var dtbIpr = PatientExamOrder(AppConstant.RegistrationType.InPatient, maxResultRecord, dtbResult);
                    dtbResult.Merge(dtbIpr);
                }
            }
            else
            {
                var dtbNonIpr = PatientExamOrder(regType, maxResultRecord, dtbResult);
                dtbResult.Merge(dtbNonIpr);
            }

            return dtbResult;
        }

        /// <summary>
        /// Patient list dgn filter Service Unit nya dari Service Unit JOb Order / Exam Order yg dituju (ToServiceUnitID) 
        /// </summary>
        /// <param name="regType"></param>
        /// <param name="maxResultRecord"></param>
        /// <param name="dtbPrevQuery"></param>
        /// <returns></returns>
        private DataTable PatientExamOrder(string regType, int maxResultRecord, DataTable dtbPrevQuery)
        {
            var tc = new TransChargesQuery("tc");
            var reg = new RegistrationQuery("reg");
            var patient = new PatientQuery("c");
            var unit = new ServiceUnitQuery("d");
            var guar = new GuarantorQuery("h");
            var sal = new AppStandardReferenceItemQuery("sal");
            var medic = new ParamedicQuery("med");
            tc.es.Top = maxResultRecord;
            tc.es.Distinct = true;

            tc.Select(
                "<'' AS RoomName>",
                reg.RegistrationDate,
                unit.ServiceUnitID,

                // ParamedicID dipakai sbg parameter dientrian detilnya bahwa patient tsb sedang dihandle oleh Paramedic tsb
                string.Format("<{0} as ParamedicID>", (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor || AppSession.UserLogin.SRUserType == AppUser.UserType.Physiotherapy)
                                                        && !string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID)
                    ? string.Format("'{0}'", AppSession.UserLogin.ParamedicID)
                    : "reg.ParamedicID"),

                medic.ParamedicName, //Paramedic Name saat registrasi utk grouping reg by
                tc.RegistrationNo,
                patient.MedicalNo,
                patient.PatientName,
                patient.Sex,
                guar.GuarantorName,
                reg.PatientID,
                reg.IsConsul,
                reg.SRRegistrationType,
                reg.RoomID,
                reg.BedID,
                "<'' AS SRBedStatus>",
                "<'' AS ReferFrom>",
                "<'' AS ReferFromRegistrationType>",
                "<'' AS ReferTo>",
                reg.RegistrationTime,
                reg.IsConfirmedAttendance,
                reg.IsNewPatient,
                "<'' AS SRTriage>",
                "<CASE WHEN reg.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsParamedicNotNull>",
                "<ISNULL(reg.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                unit.IsNeedConfirmationOfAttendance,
                reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                patient.DateOfBirth,
                sal.ItemName.As("SalutationName"),
                "<'JO' AS RowSource>",
                reg.SRCovidStatus,
                reg.SRPatientRiskStatus,
                reg.SRDischargeCondition,
                @"<'false' AS IsDoctorOnDuty>",
                 "<ISNULL(reg.IsFinishedAttendance, 0) AS IsFinishedAttendance>",
                reg.SRPatientRiskColor,
                patient.IsAlive,
                patient.DeceasedDateTime
                );
            if (AppSession.Parameter.IsCrmMembershipActive)
                tc.Select(@"<CASE WHEN ISNULL(reg.MembershipNo, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsVipMember'>");
            else
                tc.Select(@"<CAST(0 AS BIT) AS 'IsVipMember'>");

            tc.InnerJoin(reg).On(tc.RegistrationNo == reg.RegistrationNo);
            tc.InnerJoin(medic).On(reg.ParamedicID == medic.ParamedicID); // Yg dimunculkan nama dokter waktu reg
            tc.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            tc.InnerJoin(unit).On(tc.ToServiceUnitID == unit.ServiceUnitID);
            tc.InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID);
            tc.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

            if (AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse)
            {
                // Yg lainnya bisa lintas serviceUnit (selain dokter)
                var qusu = new AppUserServiceUnitQuery("qusu");
                var sutc = new ServiceUnitTransactionCodeQuery("sutc");
                qusu.InnerJoin(sutc).On(qusu.ServiceUnitID == sutc.ServiceUnitID);
                qusu.Where(sutc.SRTransactionCode == "005", qusu.UserID == AppSession.UserLogin.UserID, qusu.ServiceUnitID == tc.ToServiceUnitID); // 005 -> Job Order
                qusu.Select(qusu.ServiceUnitID);

                tc.Where(tc.ToServiceUnitID.In(qusu));
            }
            else if (AppSession.UserLogin.SRUserType == UserLogin.UserType.Doctor)
            {

                // Joborder bisa diakses oleh semua dokter yg diset ke serviceunit tipe
                // Cari praktek di service Unit Joborder apa saja
                // Tambah filter IsAllowAccessPatientWithServiceUnitParamedic == true atau sebagai dokter jaga di unit yg ditentukan (Han Maret 2022)
                var sup = new ServiceUnitParamedicQuery("sup");
                var sutc = new ServiceUnitTransactionCodeQuery("sutc");
                sup.InnerJoin(sutc).On(sup.ServiceUnitID == sutc.ServiceUnitID);
                var su = new ServiceUnitQuery("su");
                sup.InnerJoin(su).On(sup.ServiceUnitID == su.ServiceUnitID);
                sup.Where(su.IsAllowAccessPatientWithServiceUnitParamedic == true, sutc.SRTransactionCode == "005", sup.ParamedicID == AppSession.UserLogin.ParamedicID, sup.ServiceUnitID == tc.ToServiceUnitID); // 005 -> Job Order
                sup.Select(sup.ServiceUnitID);

                tc.Where(tc.ToServiceUnitID.In(sup));

                //if (!chkIsRegToOtherPhy.Checked)
                if (cboParamedicTeam.SelectedValue == "REGTOME")
                {
                    tc.Where(reg.ParamedicID == AppSession.UserLogin.ParamedicID);
                }
            }

            if (AppSession.UserLogin.SRUserType != UserLogin.UserType.Doctor && !string.IsNullOrWhiteSpace(cboParamedicID.SelectedValue))
            {
                tc.Where(reg.ParamedicID == cboParamedicID.SelectedValue);
            }

            // Tambah filter ServiceUnit
            if (cboServiceUnitID.SelectedValue != string.Empty)
                tc.Where(tc.ToServiceUnitID == cboServiceUnitID.SelectedValue);

            if (regType == AppConstant.RegistrationType.InPatient)
            {
                var bed = new BedQuery("bed");
                if (!chkIsIncludeNotInBed.Checked)
                    tc.InnerJoin(bed).On(reg.BedID == bed.BedID & reg.RegistrationNo == bed.RegistrationNo); // Hanya yg masih ada di bed
                else
                    tc.InnerJoin(bed).On(reg.BedID == bed.BedID);

                tc.Where(tc.Or(bed.IsNeedConfirmation == false, bed.RegistrationNo != tc.RegistrationNo,
                    tc.And(bed.IsNeedConfirmation == true, bed.SRBedStatus != AppSession.Parameter.BedStatusPending))); // Jika perlu confirm maka yg pending jangan diambil

                tc.Where(reg.SRRegistrationType == AppConstant.RegistrationType.InPatient);
            }
            else
            {
                // Utk semua pasien method ini dijalankan 2x dg filter regType== "" lalu regType=="IPR"
                if (string.IsNullOrEmpty(regType))
                {
                    // Exclude MCU krn sudah diambil di method khusus utk MCU Reg
                    tc.Where(reg.SRRegistrationType != AppConstant.RegistrationType.InPatient, reg.SRRegistrationType != AppConstant.RegistrationType.MedicalCheckUp);
                }
                else
                {
                    tc.Where(reg.SRRegistrationType == regType);
                }

                if (!txtRegistrationDate.IsEmpty)
                {
                    tc.Where(reg.RegistrationDate == txtRegistrationDate.SelectedDate.Value.Date);

                    if (txtFromRegistrationTime.Text != "0000" || txtToRegistrationTime.Text != "0000")
                        tc.Where(
                            reg.RegistrationTime.Between(
                                txtFromRegistrationTime.Text.Substring(0, 2) + ":" +
                                txtFromRegistrationTime.Text.Substring(2, 2),
                                txtToRegistrationTime.Text.Substring(0, 2) + ":" +
                                txtToRegistrationTime.Text.Substring(2, 2)));
                }
            }


            if (!txtExamOrderDateFrom.IsEmpty)
            {
                if (txtExamOrderDateTo.IsEmpty)
                    tc.Where(tc.TransactionDate >= txtExamOrderDateFrom.SelectedDate,
                        tc.TransactionDate < txtExamOrderDateFrom.SelectedDate.Value.AddDays(1));
                else
                    tc.Where(tc.TransactionDate >= txtExamOrderDateFrom.SelectedDate,
                        tc.TransactionDate < txtExamOrderDateTo.SelectedDate.Value.AddDays(1));
            }

            // Filter PatientID jika didepan ada patient nya berdasarkan pencarin nama atau MedicalNo
            if (_patientIdSearchs !=null &&_patientIdSearchs.Length > 0)
                tc.Where(patient.PatientID.In(_patientIdSearchs), reg.PatientID.In(_patientIdSearchs));

            if (txtRegistrationNo.Text != string.Empty)
            {
                if (txtRegistrationNo.Text.Contains("REG"))
                    tc.Where(reg.RegistrationNo == txtRegistrationNo.Text);
                //else
                //{
                //    //var searchText = "%" + txtRegistrationNo.Text + "%";
                //    //tc.Where(patient.MedicalNo.Like(searchText));

                //    // Search use Reverse Value (Handono 241112)
                //    // ReverseMedicalNo & ReverseOldMedicalNo is computed field
                //    // Deklarasi dgn tipe string supaya ke sql jadi VARCHAR
                //    string reverseMedNoSearch = string.Format("{0}%", txtRegistrationNo.Text.Trim().Replace("-", "").Reverse());
                //    tc.Where(
                //        tc.Or(
                //            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                //            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                //            )
                //        );
                //}
            }


            //if (txtPatientName.Text != string.Empty)
            //{
            //    //string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
            //    //tc.Where
            //    //    (
            //    //      string.Format("<LTRIM(RTRIM(LTRIM(c.FirstName + ' ' + c.MiddleName)) + ' ' + c.LastName) LIKE '{0}'>", searchPatient)
            //    //    );
            //}

            if (trSmfFilter.Visible && cboSmf.SelectedValue != string.Empty)
            {
                tc.Where(reg.SmfID == cboSmf.SelectedValue);
            }

            tc.Where
                (
                    tc.IsOrder == true,
                    tc.IsApproved == true
                );

            if (!chkIsIncludeClosed.Checked)
                tc.Where(reg.IsClosed == false);


            var group = new esQueryItem(tc, "Group", esSystemType.String);
            group = unit.ServiceUnitName;

            tc.Select(group.As("Group"));
            //tc.OrderBy
            //(
            //    tci.ParamedicID.Ascending,
            //    reg.RegistrationDate.Descending,
            //    tc.RegistrationNo.Ascending
            //);

            //if (AppSession.Parameter.IsShowExternalQueue)
            //{
            //    tc.Select(reg.ExternalQueNo.As("RegistrationQue"));
            //    tc.OrderBy(reg.RegistrationDate.Descending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);
            //}
            //else
            //{
            //    tc.Select(reg.RegistrationQue);
            //    tc.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.RegistrationTime.Ascending);
            //}
            tc.Select(reg.RegistrationQue, reg.ExternalQueNo);
            tc.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);

            var dtb = tc.LoadDataTable();

            // Hapus yg sudah ada
            foreach (DataRow row in dtb.Rows)
            {
                var regNo = row["RegistrationNo"];
                var suID = row["ServiceUnitID"];
                foreach (DataRow rowSearch in dtbPrevQuery.Rows)
                {
                    if (rowSearch["RegistrationNo"].Equals(regNo) && rowSearch["ServiceUnitID"].Equals(suID))
                    {
                        row.Delete();
                        continue;
                    }
                }
            }
            dtb.AcceptChanges();
            return dtb;
        }


        private DataTable RegistrationListFromServiceUnitTransaction(int maxResultRecord, string[] regTypes)
        {
            // Ambil list pasien di transaksi service unit ..filter FromServiceUnit and IsOrder = false
            var tc = new TransChargesQuery("a");
            var chargesComp = new TransChargesItemCompQuery("tcc");
            var joFromUnit = new ServiceUnitQuery("jfu");
            var regUnit = new ServiceUnitQuery("ru");
            var reg = new RegistrationQuery("reg");
            var patient = new PatientQuery("f");
            var medic = new ParamedicQuery("m");
            var grr = new GuarantorQuery("g");
            var room = new ServiceRoomQuery("s");
            var sal = new AppStandardReferenceItemQuery("sal");

            tc.es.Top = maxResultRecord;
            tc.es.Distinct = true;


            tc.Select(
                chargesComp.ParamedicID.As("f1"),
                reg.RegistrationDate.As("f2"),
                reg.RegistrationTime.As("f3"),
                reg.RegistrationNo.As("f4"),

                reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                room.RoomName,
                reg.RegistrationDate,
                joFromUnit.ServiceUnitID,
                chargesComp.ParamedicID,
                medic.ParamedicName, // Tetap display dokter saat registrasi
                reg.RegistrationNo,
                patient.MedicalNo,
                patient.PatientName,
                patient.Sex,
                grr.GuarantorName,
                reg.PatientID,
                reg.IsConsul,
                reg.SRRegistrationType,
                reg.RoomID,
                "<'' AS BedID>",
                "<'' AS SRBedStatus>",
                "<'' AS ReferFrom>",
                "<'' AS ReferFromRegistrationType>",
                "<'' AS ReferTo>",
                reg.RegistrationTime,
                reg.IsConfirmedAttendance,
                reg.IsNewPatient,
                "<'' AS SRTriage>",
                "<CASE WHEN reg.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsParamedicNotNull>",
                "<ISNULL(reg.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                regUnit.IsNeedConfirmationOfAttendance,
                reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                patient.DateOfBirth,
                sal.ItemName.As("SalutationName"),
                "<'MCUSU' AS RowSource>",
                reg.SRCovidStatus,
                reg.SRPatientRiskStatus,
                reg.SRDischargeCondition,
                @"<'false' AS IsDoctorOnDuty>",
                "<ISNULL(reg.IsFinishedAttendance, 0) AS IsFinishedAttendance>",
                reg.SRPatientRiskColor,
                patient.IsAlive,
                patient.DeceasedDateTime
                );
            if (AppSession.Parameter.IsCrmMembershipActive)
                tc.Select(@"<CASE WHEN ISNULL(reg.MembershipNo, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsVipMember'>");
            else
                tc.Select(@"<CAST(0 AS BIT) AS 'IsVipMember'>");

            tc.InnerJoin(chargesComp).On(tc.TransactionNo == chargesComp.TransactionNo);
            tc.InnerJoin(joFromUnit).On(tc.FromServiceUnitID == joFromUnit.ServiceUnitID);
            tc.InnerJoin(reg).On(tc.RegistrationNo == reg.RegistrationNo);
            tc.InnerJoin(regUnit).On(reg.ServiceUnitID == regUnit.ServiceUnitID);

            tc.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            tc.InnerJoin(medic).On(reg.ParamedicID == medic.ParamedicID);
            tc.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
            tc.InnerJoin(room).On(reg.RoomID == room.RoomID);
            tc.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

            tc.Where(
                reg.SRRegistrationType == AppConstant.RegistrationType.MedicalCheckUp,
                reg.IsVoid == false,
                reg.IsFromDispensary == false,
                chargesComp.ParamedicID != reg.ParamedicID, // Yg selain paramedic di reg
                tc.IsOrder == false,
                tc.IsVoid == false,
                reg.IsNonPatient == false
                //tc.Or(
                //    tc.PackageReferenceNo.IsNotNull(),
                //    tc.PackageReferenceNo != string.Empty
                //    )
                );


            if (cboServiceUnitID.SelectedValue == string.Empty)
            {
                if (AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse)
                {
                    // Yg lainnya bisa lintas serviceUnit
                    var qusr = new AppUserServiceUnitQuery("u");
                    tc.InnerJoin(qusr).On(
                        tc.FromServiceUnitID == qusr.ServiceUnitID &
                        qusr.UserID == AppSession.UserLogin.UserID
                    );
                }
            }
            else
                tc.Where(tc.FromServiceUnitID == cboServiceUnitID.SelectedValue);


            if (cboParamedicID.SelectedValue != string.Empty)
                tc.Where(chargesComp.ParamedicID == cboParamedicID.SelectedValue);

            // Filter PatientID jika didepan ada patient nya berdasarkan pencarin nama atau MedicalNo
            if (_patientIdSearchs !=null &&_patientIdSearchs.Length > 0)
                tc.Where(patient.PatientID.In(_patientIdSearchs), reg.PatientID.In(_patientIdSearchs));

            if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
            {
                if (txtRegistrationNo.Text.Contains("REG"))
                    tc.Where(tc.RegistrationNo == txtRegistrationNo.Text);
                //else
                //{
                //    //var searchText = "%" + txtRegistrationNo.Text + "%";
                //    //tc.Where(patient.MedicalNo.Like(searchText));

                //    // Search use Reverse Value (Handono 241112)
                //    // ReverseMedicalNo & ReverseOldMedicalNo is computed field
                //    // Deklarasi dgn tipe string supaya ke sql jadi VARCHAR
                //    string reverseMedNoSearch = string.Format("{0}%", txtRegistrationNo.Text.Trim().Replace("-", "").Reverse());
                //    tc.Where(
                //        tc.Or(
                //            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                //            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                //            )
                //        );
                //}
            }

            //if (txtPatientName.Text != string.Empty)
            //{
            //    //string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
            //    //tc.Where
            //    //    (
            //    //      string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient)
            //    //    );
            //}
            if (!chkIsIncludeClosed.Checked)
                tc.Where(reg.IsClosed == false);

            if (!string.IsNullOrEmpty(cboConfirmedAttendanceStatus.SelectedValue))
            {
                if (cboConfirmedAttendanceStatus.SelectedValue == "1")
                    tc.Where(reg.IsConfirmedAttendance.IsNotNull(), reg.IsConfirmedAttendance == true);
                else if (cboConfirmedAttendanceStatus.SelectedValue == "0")
                    tc.Where(tc.Or(reg.IsConfirmedAttendance.IsNull(), reg.IsConfirmedAttendance == false));
            }

            if (!txtRegistrationDate.IsEmpty)
                tc.Where(reg.RegistrationDate.Date() == txtRegistrationDate.SelectedDate.Value.Date);


            var group = new esQueryItem(tc, "Group", esSystemType.String);
            group = joFromUnit.ServiceUnitName;
            tc.Select(group.As("Group"));


            //if (AppSession.Parameter.IsShowExternalQueue)
            //{
            //    tc.Select(reg.ExternalQueNo.As("RegistrationQue"));
            //    tc.OrderBy(reg.RegistrationDate.Descending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);
            //}
            //else
            //{
            //    tc.Select(reg.RegistrationQue);
            //    tc.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.RegistrationTime.Ascending);
            //}
            tc.Select(reg.RegistrationQue, reg.ExternalQueNo);
            tc.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);

            var dtb = tc.LoadDataTable();

            // Hapus 4 kolom pertama yg dipakai utk distinct
            dtb.Columns.RemoveAt(0);
            dtb.Columns.RemoveAt(0);
            dtb.Columns.RemoveAt(0);
            dtb.Columns.RemoveAt(0);
            return dtb;
        }

        private DataTable RegistrationListFromJobOrderTransaction(int maxResultRecord, string[] regTypes)
        {
            // Ambil list pasien di transaksi Job Order ...filter ToServiceUnitID and IsJobOrder=true
            var tc = new TransChargesQuery("a");
            var chargesComp = new TransChargesItemCompQuery("tcc");
            var joToUnit = new ServiceUnitQuery("jtu");
            var regUnit = new ServiceUnitQuery("ru");
            var reg = new RegistrationQuery("reg");
            var patient = new PatientQuery("f");
            var medic = new ParamedicQuery("m");
            var grr = new GuarantorQuery("g");
            var room = new ServiceRoomQuery("s");
            var sal = new AppStandardReferenceItemQuery("sal");

            tc.es.Top = maxResultRecord;
            tc.es.Distinct = true;


            tc.Select(
                chargesComp.ParamedicID.As("f1"),
                reg.RegistrationDate.As("f2"),
                reg.RegistrationTime.As("f3"),
                reg.RegistrationNo.As("f4"),

                reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                room.RoomName,
                reg.RegistrationDate,
                joToUnit.ServiceUnitID,
                chargesComp.ParamedicID,
                    medic.ParamedicName, // Tetap display dokter saat registrasi
                reg.RegistrationNo,
                patient.MedicalNo,
                patient.PatientName,
                patient.Sex,
                grr.GuarantorName,
                reg.PatientID,
                reg.IsConsul,
                reg.SRRegistrationType,
                reg.RoomID,
                "<'' AS BedID>",
                "<'' AS SRBedStatus>",
                "<'' AS ReferFrom>",
                "<'' AS ReferFromRegistrationType>",
                "<'' AS ReferTo>",
                reg.RegistrationTime,
                reg.IsConfirmedAttendance,
                reg.IsNewPatient,
                "<'' AS SRTriage>",
                "<CASE WHEN reg.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsParamedicNotNull>",
                "<ISNULL(reg.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                regUnit.IsNeedConfirmationOfAttendance,
                reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                patient.DateOfBirth,
                sal.ItemName.As("SalutationName"),
                "<'MCUJO' AS RowSource>",
                reg.SRCovidStatus,
                reg.SRPatientRiskStatus,
                reg.SRDischargeCondition,
                @"<'false' AS IsDoctorOnDuty>",
                "<ISNULL(reg.IsFinishedAttendance, 0) AS IsFinishedAttendance>",
                reg.SRPatientRiskColor,
                patient.IsAlive,
                patient.DeceasedDateTime
                );
            if (AppSession.Parameter.IsCrmMembershipActive)
                tc.Select(@"<CASE WHEN ISNULL(reg.MembershipNo, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsVipMember'>");
            else
                tc.Select(@"<CAST(0 AS BIT) AS 'IsVipMember'>");

            tc.InnerJoin(chargesComp).On(tc.TransactionNo == chargesComp.TransactionNo);
            tc.InnerJoin(joToUnit).On(tc.ToServiceUnitID == joToUnit.ServiceUnitID);
            tc.InnerJoin(reg).On(tc.RegistrationNo == reg.RegistrationNo);
            tc.InnerJoin(regUnit).On(reg.ServiceUnitID == regUnit.ServiceUnitID);

            tc.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            tc.InnerJoin(medic).On(reg.ParamedicID == medic.ParamedicID);
            tc.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
            tc.InnerJoin(room).On(reg.RoomID == room.RoomID);
            tc.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

            tc.Where(
                chargesComp.ParamedicID != reg.ParamedicID, // Yg selain paramedic di reg
                reg.IsVoid == false,
                reg.IsFromDispensary == false,
                tc.IsOrder == true,
                tc.IsVoid == false,
                reg.IsNonPatient == false
                //tc.Or(
                //    tc.PackageReferenceNo.IsNotNull(),
                //    tc.PackageReferenceNo != string.Empty
                //    )
                );

            if (regTypes.Length == 1)
                tc.Where(reg.SRRegistrationType == regTypes[0]);
            else if (regTypes.Length > 1)
                tc.Where(reg.SRRegistrationType.In(regTypes));


            if (cboServiceUnitID.SelectedValue == string.Empty)
            {
                if (AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse)
                {
                    // Yg lainnya bisa lintas serviceUnit
                    var qusr = new AppUserServiceUnitQuery("u");
                    tc.InnerJoin(qusr).On(
                        tc.FromServiceUnitID == qusr.ServiceUnitID &
                        qusr.UserID == AppSession.UserLogin.UserID
                    );
                }
            }
            else
                tc.Where(tc.FromServiceUnitID == cboServiceUnitID.SelectedValue);


            if (cboParamedicID.SelectedValue != string.Empty)
                tc.Where(chargesComp.ParamedicID == cboParamedicID.SelectedValue);

            // Filter PatientID jika didepan ada patient nya berdasarkan pencarin nama atau MedicalNo
            if (_patientIdSearchs !=null &&_patientIdSearchs.Length > 0)
                tc.Where(patient.PatientID.In(_patientIdSearchs), reg.PatientID.In(_patientIdSearchs));

            if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
            {
                if (txtRegistrationNo.Text.Contains("REG"))
                    tc.Where(tc.RegistrationNo == txtRegistrationNo.Text);
                //else
                //{
                //    //var searchText = "%" + txtRegistrationNo.Text + "%";
                //    //tc.Where(patient.MedicalNo.Like(searchText));

                //    // Search use Reverse Value (Handono 241112)
                //    // ReverseMedicalNo & ReverseOldMedicalNo is computed field
                //    // Deklarasi dgn tipe string supaya ke sql jadi VARCHAR
                //    string reverseMedNoSearch = string.Format("{0}%", txtRegistrationNo.Text.Trim().Replace("-", "").Reverse());
                //    tc.Where(
                //        tc.Or(
                //            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                //            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                //            )
                //        );
                //}
            }

            //if (txtPatientName.Text != string.Empty)
            //{
            //    //string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
            //    //tc.Where
            //    //    (
            //    //      string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient)
            //    //    );
            //}
            if (!chkIsIncludeClosed.Checked)
                tc.Where(reg.IsClosed == false);

            if (!string.IsNullOrEmpty(cboConfirmedAttendanceStatus.SelectedValue))
            {
                if (cboConfirmedAttendanceStatus.SelectedValue == "1")
                    tc.Where(reg.IsConfirmedAttendance.IsNotNull(), reg.IsConfirmedAttendance == true);
                else if (cboConfirmedAttendanceStatus.SelectedValue == "0")
                    tc.Where(tc.Or(reg.IsConfirmedAttendance.IsNull(), reg.IsConfirmedAttendance == false));
            }

            if (!txtRegistrationDate.IsEmpty)
                tc.Where(reg.RegistrationDate.Date() == txtRegistrationDate.SelectedDate.Value.Date);


            var group = new esQueryItem(tc, "Group", esSystemType.String);
            group = joToUnit.ServiceUnitName;
            tc.Select(group.As("Group"));


            //if (AppSession.Parameter.IsShowExternalQueue)
            //{
            //    tc.Select(reg.ExternalQueNo.As("RegistrationQue"));
            //    tc.OrderBy(reg.RegistrationDate.Descending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);
            //}
            //else
            //{
            //    tc.Select(reg.RegistrationQue);
            //    tc.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.RegistrationTime.Ascending);
            //}
            tc.Select(reg.RegistrationQue, reg.ExternalQueNo);
            tc.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);

            var dtb = tc.LoadDataTable();

            // Hapus 4 kolom pertama yg dipakai utk distinct
            dtb.Columns.RemoveAt(0);
            dtb.Columns.RemoveAt(0);
            dtb.Columns.RemoveAt(0);
            dtb.Columns.RemoveAt(0);
            return dtb;
        }

        #endregion

        private void AddFilterRegistrationAndPatient(RegistrationQuery qr, PatientQuery qp, string[] regTypes)
        {
            qr.Where(qr.IsVoid == false);
            qr.Where(qr.IsFromDispensary == false); // Bukan dari registrasi hasil penjualan obat bebas / langsung 
            qr.Where(qr.IsNonPatient == false); // Bukan dari transaksi Non Patient Customer Charges / Transaksi dari pasien luar yg memanfaatkan fasilitas RS

            // Filter PatientID jika didepan ada patient nya berdasarkan pencarin nama atau MedicalNo
            if (_patientIdSearchs !=null &&_patientIdSearchs.Length > 0)
                qr.Where(qr.PatientID.In(_patientIdSearchs));

            if (!string.IsNullOrWhiteSpace(txtRegistrationNo.Text))
            {
                string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                if (searchReg.ToLower().Contains("reg"))
                    qr.Where(qr.RegistrationNo == searchReg);
            }

            var isIncludeInPatient = !string.IsNullOrWhiteSpace(Array.Find(regTypes, element => element.StartsWith(AppConstant.RegistrationType.InPatient, StringComparison.Ordinal)));

            // Filter tgl kecuali utk InPatient tgl diabaikan               
            if (!txtRegistrationDate.IsEmpty && !(regTypes.Length == 1 && isIncludeInPatient))
            {
                if (regTypes.Length == 0 || (regTypes.Length > 1 && isIncludeInPatient))
                {
                    // Filter tgl kecuali utk InPatient tgl diabaikan
                    qr.Where(qr.Or(qr.SRRegistrationType == AppConstant.RegistrationType.InPatient, qr.RegistrationDate == txtRegistrationDate.SelectedDate.Value.Date));
                }
                else
                {
                    qr.Where(qr.RegistrationDate == txtRegistrationDate.SelectedDate.Value.Date);
                }

                if (txtFromRegistrationTime.Text != "0000" || txtToRegistrationTime.Text != "0000")
                    qr.Where(
                        qr.Or(qr.SRRegistrationType == AppConstant.RegistrationType.InPatient, qr.RegistrationTime.Between(
                            txtFromRegistrationTime.Text.Substring(0, 2) + ":" +
                            txtFromRegistrationTime.Text.Substring(2, 2),
                            txtToRegistrationTime.Text.Substring(0, 2) + ":" +
                            txtToRegistrationTime.Text.Substring(2, 2))));
            }


            if (!chkIsIncludeClosed.Checked)
                qr.Where(qr.IsClosed == false);

            // Abaikan untuk Rawat Inap dan Emergency
            if (!chkIsAllSoap.Checked)
            {

                if (regTypes.Length == 1 && (regTypes[0] == AppConstant.RegistrationType.InPatient || regTypes[0] == AppConstant.RegistrationType.EmergencyPatient))
                {
                    // Not filter
                }
                else
                {
                    var isIncludeEmergencyPatient = !string.IsNullOrWhiteSpace(Array.Find(regTypes, element => element.StartsWith(AppConstant.RegistrationType.EmergencyPatient, StringComparison.Ordinal)));

                    // Soap Sub Query for non InPatient and non EmergencyPatient
                    var notInSoapQr = new RegistrationInfoMedicQuery("nisoap");
                    notInSoapQr.Select(notInSoapQr.RegistrationNo);
                    notInSoapQr.Where(notInSoapQr.RegistrationNo == qr.RegistrationNo);

                    if (regTypes.Length == 0 || (regTypes.Length > 1 && isIncludeEmergencyPatient && isIncludeInPatient))
                    {
                        qr.Where(qr.Or(qr.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                            qr.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient,
                            qr.RegistrationNo.NotIn(notInSoapQr)));
                    }
                    else if (regTypes.Length > 1 && isIncludeEmergencyPatient)
                    {
                        qr.Where(qr.Or(qr.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient,
                            qr.RegistrationNo.NotIn(notInSoapQr)));
                    }
                    else if (regTypes.Length > 1 && isIncludeInPatient)
                    {
                        qr.Where(qr.Or(qr.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient,
                            qr.RegistrationNo.NotIn(notInSoapQr)));
                    }
                    else if (!isIncludeInPatient && !isIncludeEmergencyPatient)
                    {
                        qr.Where(qr.RegistrationNo.NotIn(notInSoapQr));
                    }

                }
            }

            if (!string.IsNullOrEmpty(cboConfirmedAttendanceStatus.SelectedValue))
            {
                if (regTypes.Length == 0 || (regTypes.Length > 1 && isIncludeInPatient))
                {
                    if (cboConfirmedAttendanceStatus.SelectedValue == "1")
                        qr.Where(qr.Or(qr.SRRegistrationType == AppConstant.RegistrationType.InPatient, qr.IsConfirmedAttendance.IsNotNull(), qr.IsConfirmedAttendance == true));
                    else if (cboConfirmedAttendanceStatus.SelectedValue == "0")
                        qr.Where(qr.Or(qr.SRRegistrationType == AppConstant.RegistrationType.InPatient, qr.Or(qr.IsConfirmedAttendance.IsNull(), qr.IsConfirmedAttendance == false)));
                }
                else if (!isIncludeInPatient)
                {
                    if (cboConfirmedAttendanceStatus.SelectedValue == "1")
                        qr.Where(qr.IsConfirmedAttendance.IsNotNull(), qr.IsConfirmedAttendance == true);
                    else if (cboConfirmedAttendanceStatus.SelectedValue == "0")
                        qr.Where(qr.Or(qr.IsConfirmedAttendance.IsNull(), qr.IsConfirmedAttendance == false));
                }
            }
        }

        #region Pasien bersama yg diseting berdasarkan status di master ServiceUnit
        /// <summary>
        /// Pasien untuk Dokter Jaga
        /// Hannya dipanggil jika login sbg dokter
        /// </summary>
        /// <param name="maxResultRecord"></param>
        /// <returns></returns>
        private DataTable PatientInAllowAccessPatientWithServiceUnitParamedic(int maxResultRecord, string[] regTypes, DataTable dtbPrevQuery)
        {
            //if (AppSession.UserLogin.SRUserType != UserLogin.UserType.Doctor || !chkIsRegToOtherPhy.Checked) return null;

            if (AppSession.UserLogin.SRUserType != UserLogin.UserType.Doctor || cboParamedicTeam.SelectedValue == "REGTOME") return null;

            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                var su = new ServiceUnit();
                if (su.LoadByPrimaryKey(cboServiceUnitID.SelectedValue))
                {
                    if (su.IsAllowAccessPatientWithServiceUnitParamedic != true)
                        return null;
                }

                var supar = new ServiceUnitParamedic();
                if (!supar.LoadByPrimaryKey(su.ServiceUnitID, AppSession.UserLogin.ParamedicID))
                    return null;
            }

            var reg = new RegistrationQuery("reg");
            var unit = new ServiceUnitQuery("su");
            var room = new ServiceRoomQuery("c");
            var medic = new ParamedicQuery("d");
            var patient = new PatientQuery("f");
            var grr = new GuarantorQuery("g");
            var sal = new AppStandardReferenceItemQuery("sal");

            reg.es.Top = maxResultRecord;

            reg.Select
                (
                    room.RoomName,
                    reg.RegistrationDate,
                    unit.ServiceUnitID,
                    // ParamedicID dipakai sbg tanda dientrian detilnya bahwa patient tsb sedang dihandle oleh Paramedic tsb
                    string.Format("<'{0}' as ParamedicID>", AppSession.UserLogin.ParamedicID),
                    medic.ParamedicName, // Tetap display dokter saat registrasi
                    reg.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    patient.Sex,
                    grr.GuarantorName,
                    reg.PatientID,
                    reg.IsConsul,
                    reg.SRRegistrationType,
                    reg.RoomID,
                    reg.BedID,
                    "<'' AS SRBedStatus>",
                    "<'' AS ReferFrom>",
                    "<'' AS ReferFromRegistrationType>",
                    "<'' AS ReferTo>",
                    reg.RegistrationTime,
                    reg.IsConfirmedAttendance,
                    reg.IsNewPatient,
                    reg.SRTriage,
                    "<CASE WHEN reg.ParamedicID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsParamedicNotNull>",
                    "<ISNULL(reg.IsConfirmedAttendance, 0) AS IsConfirmedAttendance>",
                    unit.IsNeedConfirmationOfAttendance,
                    reg.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                    patient.DateOfBirth,
                    sal.ItemName.As("SalutationName"),
                    "<'DRJG' AS RowSource>", // Untuk info programmer saja yg dishow pd waktu mode debug
                    reg.SRCovidStatus,
                    reg.SRPatientRiskStatus,
                    reg.SRDischargeCondition,
                    @"<CASE WHEN reg.SRRegistrationType = 'EMR' THEN ((CASE WHEN reg.ParamedicID = '" + AppParameter.GetParameterValue(AppParameter.ParameterItem.DoctorOnDutyId) + @"' THEN 'true' ELSE 'false' END)) ELSE 'false' END AS IsDoctorOnDuty>",
                    "<ISNULL(reg.IsFinishedAttendance, 0) AS IsFinishedAttendance>",
                    reg.SRPatientRiskColor,
                    patient.IsAlive,
                    patient.DeceasedDateTime
                );
            if (AppSession.Parameter.IsCrmMembershipActive)
                reg.Select(@"<CASE WHEN ISNULL(reg.MembershipNo, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsVipMember'>");
            else
                reg.Select(@"<CAST(0 AS BIT) AS 'IsVipMember'>");

            reg.LeftJoin(room).On(reg.RoomID == room.RoomID);
            reg.LeftJoin(medic).On(reg.ParamedicID == medic.ParamedicID);
            reg.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            reg.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);

            reg.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);

            // Untuk Inpatient yg baru masuk RS (masih open), ambil yg statusnya bukan pending jika IsNeedConfirmation=true untuk mencegah transaksi, krn kalau yg sudah close sudah tidak bisa
            var bed = new BedQuery("bd");
            reg.LeftJoin(bed).On(reg.RegistrationNo == bed.RegistrationNo);
            reg.Where(reg.Or(bed.IsNeedConfirmation == false, bed.IsNeedConfirmation.IsNull(), reg.And(bed.IsNeedConfirmation == true, reg.RegistrationNo == bed.RegistrationNo, bed.SRBedStatus != AppSession.Parameter.BedStatusPending, bed.SRBedStatus != AppSession.Parameter.BedStatusBooked)));

            if (cboServiceUnitID.SelectedValue == string.Empty)
            {
                // Filter dg Service Unit Paramedic
                var supar = new ServiceUnitParamedicQuery("u");
                reg.InnerJoin(supar).On(unit.ServiceUnitID == supar.ServiceUnitID & unit.IsAllowAccessPatientWithServiceUnitParamedic == true &
                    supar.ParamedicID == AppSession.UserLogin.ParamedicID
                );
            }
            else
            {
                // Pengecekan IsAllowAccessPatientWithServiceUnitParamedic == true harus dilakukan saat pemanggilan fungsi ini
                reg.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);
            }

            reg.Where(reg.ParamedicID != AppSession.UserLogin.ParamedicID); // Hanya ambil yg belum terambil oleh query2 sebelumnya yg difilter dg ParamedicID nya 

            if (regTypes.Length == 1)
                reg.Where(reg.SRRegistrationType == regTypes[0]);
            else if (regTypes.Length > 1)
                reg.Where(reg.SRRegistrationType.In(regTypes));

            AddFilterRegistrationAndPatient(reg, patient, regTypes);

            var group = new esQueryItem(reg, "Group", esSystemType.String);
            group = unit.ServiceUnitName;

            reg.Select(group.As("Group"));

            //if (AppSession.Parameter.IsShowExternalQueue)
            //{
            //    reg.Select(reg.ExternalQueNo.As("RegistrationQue"));
            //    reg.OrderBy(reg.RegistrationDate.Descending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);
            //}
            //else
            //{
            //    reg.Select(reg.RegistrationQue);
            //    reg.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.RegistrationTime.Ascending);
            //}
            reg.Select(reg.RegistrationQue, reg.ExternalQueNo);
            reg.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationQue.Ascending, reg.ExternalQueNo.Ascending, reg.RegistrationTime.Ascending);

            var dtb = reg.LoadDataTable();

            // Hapus yg sudah ada
            foreach (DataRow row in dtb.Rows)
            {
                bool isCheckDouble = true;
                //add by deby 20221108
                if (!chkIsIncludeNotInBed.Checked && row["SRRegistrationType"].ToString() == "IPR" && row["SRDischargeCondition"].ToString() != string.Empty)
                {
                    row.Delete();
                    isCheckDouble = false;
                }
                else if (row["SRRegistrationType"].ToString() != "IPR" && cboConfirmedAttendanceStatus.SelectedValue != string.Empty)
                {
                    if ((cboConfirmedAttendanceStatus.SelectedValue == "0" && Convert.ToBoolean(row["IsConfirmedAttendance"]) == true) || (cboConfirmedAttendanceStatus.SelectedValue == "1" && Convert.ToBoolean(row["IsConfirmedAttendance"]) == false))
                    {
                        row.Delete();
                        isCheckDouble = false;
                    }
                }

                if (isCheckDouble)
                {
                    var regNo = row["RegistrationNo"];
                    var suID = row["ServiceUnitID"];
                    foreach (DataRow rowSearch in dtbPrevQuery.Rows)
                    {
                        if (rowSearch["RegistrationNo"].Equals(regNo) && rowSearch["ServiceUnitID"].Equals(suID))
                        {
                            row.Delete();
                            continue;
                        }
                    }
                }
            }
            dtb.AcceptChanges();

            return dtb; // DeleteInPatientBedStatusPendingAndNotInBed(dtb);
        }
        #endregion

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            //SaveValueToCookie(txtPatientName, txtRegistrationNo);
            SaveValueToCookie("EmrList");
            grdList.CurrentPageIndex = 0;
            grdList.Rebind();
        }

        protected void tmrAutoRefreshList_Tick(object sender, EventArgs e)
        {
            grdList.Rebind();
        }

        protected string ColorOfTriase(object SRTriage)
        {
            var color = "Transparant";
            switch (SRTriage.ToString())
            {
                case "01":
                    {
                        color = "Red";
                        break;
                    }
                case "02":
                    {
                        color = "Orange";
                        break;
                    }
                case "03":
                    {
                        color = "Yellow";
                        break;
                    }
                case "04":
                    {
                        color = "Green";
                        break;
                    }
                case "05":
                    {
                        color = "Black";
                        break;
                    }
                case "99": // pasien rawat jalan yg sudah dilakukan PHYEXAM
                    {
                        color = "Blue";
                        break;
                    }
            }

            return color;
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

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind")
            {
                grdList.Rebind();
            }
            else if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');
                if (param[0] == "confirmed")
                {
                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(param[1]))
                    {
                        var time = (new DateTime()).NowAtSqlServer();
                        reg.IsConfirmedAttendance = true;
                        reg.ConfirmedAttendanceByUserID = AppSession.UserLogin.UserID;
                        reg.ConfirmedAttendanceDateTime = time;
                        reg.Save();

                        if (Helper.IsBpjsAntrolIntegration)
                        {
                            try
                            {
                                if (!string.IsNullOrWhiteSpace(reg.AppointmentNo) && reg.SRRegistrationType != AppConstant.RegistrationType.InPatient)
                                {
                                    var log = new WebServiceAPILog();
                                    log.DateRequest = DateTime.Now;
                                    log.IPAddress = string.Empty;
                                    log.UrlAddress = "EmrList";
                                    log.Params = JsonConvert.SerializeObject(new Temiang.Avicenna.Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                    {
                                        Kodebooking = reg.AppointmentNo,
                                        Taskid = 4,
                                        Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                                    });

                                    var svc = new Temiang.Avicenna.Common.BPJS.Antrian.Service();
                                    var response = svc.UpdateWaktuAntrian(new Temiang.Avicenna.Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                    {
                                        Kodebooking = reg.AppointmentNo,
                                        Taskid = 4,
                                        Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                                    });

                                    log.Response = JsonConvert.SerializeObject(response);
                                    log.Save();
                                }
                            }
                            catch (Exception e)
                            {

                            }
                        }

                        if (AppSession.Parameter.HealthcareInitial == "RSTJ")
                        {
                            if (!string.IsNullOrEmpty(reg.AppointmentNo))
                            {
                                var task = new BusinessObject.Interop.TARAKAN.AppointmentOnlineTask();
                                if (!task.LoadByPrimaryKey(reg.AppointmentNo, "5"))
                                {
                                    task.AppointmentNo = reg.AppointmentNo;
                                    task.TaskId = "5";
                                    task.Timestamp = Temiang.Avicenna.Common.BPJS.Helper.GetUnixTimeStamp();
                                    task.Save();
                                }
                            }
                        }

                        grdList.Rebind();
                    }
                }
                else if (param[0] == "finished")
                {
                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(param[1]))
                    {
                        var time = (new DateTime()).NowAtSqlServer();
                        reg.IsFinishedAttendance = true;
                        reg.FinishedAttendanceByUserID = AppSession.UserLogin.UserID;
                        reg.FinishedAttendanceDateTime = time;
                        reg.Save();

                        if (Helper.IsBpjsAntrolIntegration)
                        {
                            try
                            {
                                if (!string.IsNullOrWhiteSpace(reg.AppointmentNo))
                                {
                                    var log = new WebServiceAPILog();
                                    log.DateRequest = DateTime.Now;
                                    log.IPAddress = string.Empty;
                                    log.UrlAddress = "EmrList";
                                    log.Params = JsonConvert.SerializeObject(new Temiang.Avicenna.Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                    {
                                        Kodebooking = reg.AppointmentNo,
                                        Taskid = 5,
                                        Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                                    });

                                    var svc = new Temiang.Avicenna.Common.BPJS.Antrian.Service();
                                    var response = svc.UpdateWaktuAntrian(new Temiang.Avicenna.Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                    {
                                        Kodebooking = reg.AppointmentNo,
                                        Taskid = 5,
                                        Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                                    });

                                    log.Response = JsonConvert.SerializeObject(response);
                                    log.Save();
                                }
                            }
                            catch (Exception e)
                            {

                            }

                        }

                        grdList.Rebind();
                    }
                }
                // Tuneup: Dipindah ke webservice untuk menghemat loading dan langsung redirect ke layar emr detail jika berhasil (Handono 2302)
                //else if (param[0] == "takeover")
                //{
                //    var reg = new Registration();
                //    if (reg.LoadByPrimaryKey(param[1]))
                //    {
                //        Helper.RegistrationOpenClose.EditPhysician(reg, AppSession.UserLogin.ParamedicID, AppSession.UserLogin.ParamedicName, "", "", "", "");
                //    }
                //    grdList.Rebind();
                //}

            }
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            var dataItem = e.Item as GridDataItem;
            if (dataItem != null && dataItem["ReferFromRegistrationType"].Text == AppConstant.RegistrationType.MedicalCheckUp)
                e.Item.ForeColor = Color.Blue;
        }

        protected string RegistrationNoteCount(GridItem container)
        {
            var regNo = DataBinder.Eval(container.DataItem, "RegistrationNo").ToString();
            int? noteCount = 0;
            var si = new RegistrationInfoSumary();
            if (si.LoadByPrimaryKey(regNo))
                noteCount = si.NoteCount;
            return (string.Format("<a href=\"#\" title=\"Note\" class=\"noti_Container\" onclick=\"openWinRegistrationInfo('{0}'); return false;\"><span id=\"noti_{0}\" class=\"noti_bubble\">{1}</span></a>",
                                                                    regNo, noteCount > 0 ? noteCount.ToString() : string.Empty));
        }

        //Dipindah ke EmrWebService (Handono 230327)
        //protected string EwsScoreLevelHtml(GridItem container)
        //{
        //    if (!DataBinder.Eval(container.DataItem, "SRRegistrationType")
        //        .Equals(AppConstant.RegistrationType.InPatient))
        //        return string.Empty;

        //    // EWS for InPatient
        //    string ewsTopLevelVitalSignValue = string.Empty;
        //    string ewsTopLevelColor = string.Empty;
        //    DateTime vitalSignRecordDate = DateTime.Today;
        //    var topEwsLevel = VitalSign.EwsLevelTopLevel(DataBinder.Eval(container.DataItem, "RegistrationNo").ToString(), DataBinder.Eval(container.DataItem, "FromRegistrationNo").ToString(), Convert.ToDateTime(DataBinder.Eval(container.DataItem, "DateOfBirth")),
        //        ref ewsTopLevelVitalSignValue, ref ewsTopLevelColor, ref vitalSignRecordDate, DateTime.Now);

        //    if (topEwsLevel > 0)
        //    {
        //        return string.Format(
        //            "<div style='background-color: {0};width:100%;padding-left: 2px'><a href=\"#\" onclick=\"javascript:openVitalSignChartEws('{2}','{3}','{4}','{5}'); return false;\">{1}</a></div>",
        //            ewsTopLevelColor,
        //            ewsTopLevelVitalSignValue,
        //            DataBinder.Eval(container.DataItem, "PatientID"),
        //            DataBinder.Eval(container.DataItem, "RegistrationNo"),
        //            DataBinder.Eval(container.DataItem, "FromRegistrationNo"),
        //            vitalSignRecordDate);
        //    }

        //    return string.Empty;
        //}


        //protected string SoapEntryStatuslHtml(GridItem container)
        //{
        //    var regno = DataBinder.Eval(container.DataItem, "RegistrationNo").ToString();
        //    var parid = DataBinder.Eval(container.DataItem, "ParamedicID").ToString();
        //    var regtype = DataBinder.Eval(container.DataItem, "SRRegistrationType").ToString();

        //    // Cek di Integrated Note
        //    var rimQr = new RegistrationInfoMedicQuery();
        //    rimQr.es.Top = 1;
        //    rimQr.es.WithNoLock = true;

        //    rimQr.Where(rimQr.RegistrationNo == regno,
        //        rimQr.Or(rimQr.IsDeleted.IsNull(), rimQr.IsDeleted == false), rimQr.SRMedicalNotesInputType == "SOAP", rimQr.Info1 != string.Empty);

        //    if (regtype == AppConstant.RegistrationType.InPatient)
        //    {
        //        // Untuk in patient cek hari ini apakah sudah diisi soapnya
        //        rimQr.Where(rimQr.ParamedicID == parid, rimQr.DateTimeInfo > DateTime.Today);
        //    }

        //    var rim = new RegistrationInfoMedic();
        //    if (rim.Load(rimQr))
        //    {
        //        return string.Format("<img src='{0}/Images/Toolbar/post_green_16.png'/>", Helper.UrlRoot());
        //    }
        //    return string.Empty;
        //}

        //protected string RegistrationPathwayStatuslHtml(GridItem container)
        //{
        //    if (hdnIsClinicalPathwayActive.Value == "n")
        //        return string.Empty;

        //    var regno = DataBinder.Eval(container.DataItem, "RegistrationNo").ToString();

        //    var rpQr = new RegistrationPathwayQuery("a");
        //    rpQr.es.Top = 1;
        //    rpQr.es.WithNoLock = true;

        //    rpQr.Where(rpQr.RegistrationNo == regno, rpQr.PathwayID != string.Empty);

        //    var rp = new RegistrationPathway();
        //    if (rp.Load(rpQr) && !string.IsNullOrEmpty(rp.PathwayStatus))
        //    {
        //        return (rp.PathwayStatus == "A" ? string.Format("<img src='{0}/Images/Toolbar/post_green_16.png'/>", Helper.UrlRoot()) : (rp.PathwayStatus == "F" ? string.Format("<img src='{0}/Images/Toolbar/cancel16.png'/>", Helper.UrlRoot()) : string.Format("<img src='{0}/Images/Toolbar/row_delete16_d.png'/>", Helper.UrlRoot())));
        //    }

        //    return string.Empty;
        //}

        //protected string PrescriptionProgress(string regType, string regNo)
        //{
        //    if (regType != AppConstant.RegistrationType.InPatient) return string.Empty;

        //    var pQr = new TransPrescriptionQuery("tp");

        //    // Return Prescription
        //    var subQr = new TransPrescriptionQuery("tp");
        //    subQr.Where(subQr.RegistrationNo == regNo, subQr.ReferenceNo == pQr.PrescriptionNo, subQr.IsPrescriptionReturn == true, subQr.IsApproval == true);
        //    subQr.Select(subQr.ReferenceNo);

        //    // Hitung jumlah resep
        //    pQr.Where(pQr.RegistrationNo == regNo, pQr.IsPrescriptionReturn == false, pQr.Or(pQr.IsVoid.IsNull(), pQr.IsVoid == false));
        //    pQr.Where(pQr.PrescriptionNo.NotIn(subQr));
        //    pQr.Select(pQr.PrescriptionNo.Count());
        //    pQr.es.WithNoLock = true;
        //    var dtb = pQr.LoadDataTable();
        //    if (dtb.Rows[0][0].ToInt() == 0) return string.Empty;

        //    // Hitung jumlah yg sudah komplit dan belum dideliver
        //    pQr = new TransPrescriptionQuery("tp");
        //    pQr.Where(pQr.RegistrationNo == regNo, pQr.CompleteDateTime.IsNotNull(), pQr.IsPrescriptionReturn == false, pQr.DeliverDateTime.IsNull(), pQr.Or(pQr.IsVoid.IsNull(), pQr.IsVoid == false));
        //    pQr.Where(pQr.PrescriptionNo.NotIn(subQr));
        //    pQr.Select(pQr.PrescriptionNo.Count());
        //    pQr.es.WithNoLock = true;
        //    dtb = pQr.LoadDataTable();
        //    var completeCount = dtb.Rows[0][0].ToInt();


        //    // Hitung jumlah resp yg belum diproses
        //    pQr = new TransPrescriptionQuery("tp");
        //    pQr.Where(pQr.RegistrationNo == regNo, pQr.CompleteDateTime.IsNull(), pQr.IsPrescriptionReturn == false, pQr.Or(pQr.IsVoid.IsNull(), pQr.IsVoid == false));
        //    pQr.Select(pQr.PrescriptionNo.Count());
        //    pQr.Where(pQr.PrescriptionNo.NotIn(subQr));
        //    pQr.es.WithNoLock = true;
        //    dtb = pQr.LoadDataTable();
        //    var notCompleteCount = dtb.Rows[0][0].ToInt();

        //    var retVal = string.Empty;

        //    if (notCompleteCount > 0 && completeCount == 0)
        //        retVal = string.Format(@"<div style='font-weight: bold;color: white;text-align: center;background:gray;width: 100%; padding: 1px'>{0}</div>", notCompleteCount);

        //    else if (notCompleteCount > 0 && completeCount > 0)
        //        retVal = string.Format(@"<div style='font-weight: bold;color: white;text-align: center;background:orange;width: 100%; padding: 1px'>{0}</div>", completeCount);

        //    else if (notCompleteCount == 0 && completeCount > 0)
        //        retVal = string.Format(@"<div style='font-weight: bold;color: white;text-align: center;background:green;width: 100%; padding: 1px'>{0}</div>", completeCount);

        //    else
        //        retVal = "<div style='font-weight: bold;color: green;text-align: center;background:green;width: 100%; padding: 1px'>&nbsp;</div>";

        //    return string.Format("<a href=\"#\" onclick=\"openPendingPrescription('{0}','{1}'); return false;\">{2}</a>", regNo, regType, retVal);
        //}

        //protected string ExamOrderLabProgress(string regType, string regNo)
        //{
        //    var tc = new TransChargesQuery("b");
        //    tc.Where(tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID, tc.RegistrationNo == regNo);
        //    tc.Select(tc.TransactionNo);
        //    tc.es.WithNoLock = true;
        //    var dtbLab = tc.LoadDataTable();

        //    if (dtbLab.Rows.Count == 0) return string.Empty;

        //    var fractionCount = 0;
        //    var resultCount = 0;

        //    foreach (DataRow testLab in dtbLab.Rows)
        //    {
        //        var labResult = LaboratoryResult(testLab[0].ToString(), regNo);
        //        foreach (DataRow row in labResult.Rows)
        //        {
        //            if (row["IsFraction"].ToBoolean() == true)
        //            {
        //                fractionCount++;
        //                var result = row["Result"].ToString();
        //                if (!string.IsNullOrWhiteSpace(result) && result.ToLower() != "menyusul")
        //                    resultCount++;
        //            }
        //        }
        //    }


        //    if (fractionCount == 0) return "<div style='background:gray;width: 100%; padding: 1px'>&nbsp;</div>";

        //    return string.Format(@"<div style='background:{0};color: white;text-align: center;width: 100%; padding: 1px'>{1}/{2}</div>", fractionCount == resultCount ? "green" : (resultCount == 0 ? "gray" : "orange"), resultCount, fractionCount);
        //}
        //protected string ExamOrderRadProgress(string regType, string regNo)
        //{
        //    var tc = new TransChargesQuery("b");
        //    tc.Where(tc.Or(tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID, tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID2), tc.RegistrationNo == regNo);

        //    tc.Select(tc.TransactionNo);
        //    tc.es.Top = 1;
        //    tc.OrderBy(tc.TransactionDate.Descending, tc.TransactionNo.Descending);
        //    tc.es.WithNoLock = true;
        //    var dtbLab = tc.LoadDataTable();

        //    if (dtbLab.Rows.Count == 0) return string.Empty;

        //    var result = new TestResultQuery("r");
        //    var tci = new TransChargesItemQuery("tci");
        //    result.InnerJoin(tci).On(result.ItemID == tci.ItemID & tci.TransactionNo == dtbLab.Rows[0][0].ToString());

        //    result.Where(result.TransactionNo == dtbLab.Rows[0][0].ToString());
        //    result.Select(result.TransactionNo);
        //    result.es.WithNoLock = true;
        //    var radResult = result.LoadDataTable();
        //    return string.Format(@"<div style='background:{0};width: 100%; padding: 1px'>&nbsp;</div>", radResult.Rows.Count > 0 ? "green" : "gray");
        //}

        //private DataTable LaboratoryResult(string transactionNo, string registrationNo)
        //{
        //    if (AppSession.Parameter.IsUsingHisInterop)
        //    {
        //        DataTable dtbResult;
        //        switch (AppSession.Parameter.LisInterop)
        //        {
        //            case "SYSMEX":
        //                dtbResult = ExamOrderHistCtl.LabHistOrderResultFromSysmex(transactionNo);
        //                return dtbResult;
        //            case "RSCH":
        //                dtbResult = ExamOrderHistCtl.LabHistOrderResultFromRSCH(transactionNo);
        //                return dtbResult;
        //            case "WYNAKOM":
        //                dtbResult = ExamOrderHistCtl.LabHistOrderResultFromWynakom(transactionNo);
        //                return dtbResult;
        //            case "LINK_LIS":
        //                //sementara masih development
        //                dtbResult = new DataTable();
        //                break;
        //            case "VANSLITE":
        //                dtbResult = ExamOrderHistCtl.LabHistOrderResultFromVanslite(transactionNo);
        //                return dtbResult;
        //            case "ELIMS":
        //                dtbResult = ExamOrderHistCtl.LabHistOrderResultFromElims(transactionNo);
        //                return dtbResult;
        //            default:
        //                dtbResult = ExamOrderHistCtl.LabHistOrderResultFromVanslab(transactionNo);
        //                return dtbResult;
        //        }
        //    }
        //    return ExamOrderHistCtl.LabHistOrderResultFromManualEntry(transactionNo, registrationNo);

        //}

        //#region Plafond Progress
        //public static string PlafondProgress(string regNo, bool isModeText = false)
        //{
        //    if (!AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsEmrListShowPlafondProgress))
        //        return String.Empty;

        //    decimal tpatient = 0;
        //    decimal tguarantor = 0;
        //    decimal totalPlafond = 0;

        //    var usedInPercent = PlafondValueUsedInPercent(regNo, ref tguarantor, ref tpatient, ref totalPlafond);
        //    if (usedInPercent == 0) return string.Empty;

        //    if (isModeText)
        //        return string.Format(@"<table style='font-weight:bold;'>
        //                    <tr>
        //                        <td style='width:70px;'>Plafond</td><td>&nbsp;:&nbsp;</td>
        //                        <td style='width:70px;text-align:right;'>{0:n}</td>
        //                    </tr>
        //                    <tr>
        //                        <td>Billing</td><td>&nbsp;:&nbsp;</td>
        //                        <td style='width:70px;text-align:right;'>{1:n}</td>
        //                    </tr>
        //                </table>", totalPlafond, tguarantor + tpatient);
        //    else
        //        return string.Format(@"<div title='G: [{3:N2}] P: [{4:N2}] F: [{5:N2}]' style='background:black;width: 100%; padding: 1px'>
        //                    <div style='background:{0};color:Black;width: {1}%'>{2}</div>
        //                </div>",
        //                    usedInPercent > 100 ? "red" : usedInPercent > 75 ? "yellow" : "green",
        //                    usedInPercent > 100 ? 100 : usedInPercent,
        //                    usedInPercent > 300 ? ">300%" : string.Format("{0:n2}%",usedInPercent),
        //                    tguarantor,
        //                    tpatient,
        //                    totalPlafond);

        //}

        //private static decimal PlafondValueUsedInPercent(string regno, ref decimal tguarantor, ref decimal tpatient, ref decimal totalPlafond)
        //{
        //    var reg = new Registration();
        //    var rqr = new RegistrationQuery();
        //    rqr.Select(rqr.SRRegistrationType, rqr.PlavonAmount, rqr.GuarantorID, rqr.ApproximatePlafondAmount, rqr.SRBussinesMethod, rqr.IsGlobalPlafond);
        //    rqr.Where(rqr.RegistrationNo == regno);
        //    reg.Load(rqr);

        //    if (!GuarantorBpjs.Contains(reg.GuarantorID))
        //        return 0;

        //    decimal cobPlafond = AdditionalPlafond(regno);
        //    totalPlafond = TotalPlafond(reg.SRRegistrationType, reg.PlavonAmount, reg.GuarantorID, reg.ApproximatePlafondAmount, cobPlafond);
        //    if (totalPlafond == 0)
        //        totalPlafond = 1;

        //    var regnos = Helper.MergeBilling.GetMergeRegistration(regno);

        //    var guarantor = new Guarantor();
        //    guarantor.LoadByPrimaryKey(reg.GuarantorID);

        //    //Helper.CostCalculation.GetBillingTotal2(regnos, reg.SRBussinesMethod, 0, out tpatient, out tguarantor,
        //    //                                       guarantor, reg.IsGlobalPlafond ?? false);
        //    Helper.CostCalculation.GetBillingTotalStatus(regnos, reg.SRBussinesMethod, 0, out tpatient, out tguarantor,
        //                                          guarantor, reg.IsGlobalPlafond ?? false);

        //    var totalRemain = tguarantor + tpatient;

        //    var plafonUsedPercent = (totalRemain / totalPlafond) * (decimal)100;
        //    return plafonUsedPercent;
        //}

        //private static decimal AdditionalPlafond(string regno)
        //{
        //    decimal cobPlafond = 0;
        //    var cob = new RegistrationGuarantorCollection();
        //    cob.Query.Where(cob.Query.RegistrationNo == regno);
        //    cob.LoadAll();
        //    cobPlafond = cob.Sum(c => (c.PlafondAmount ?? 0));
        //    return cobPlafond;
        //}
        //private static decimal TotalPlafond(string srRegistrationType, decimal? plavonAmount, string guarantorID, decimal? approximatePlafondAmount, decimal additionalPlafond)
        //{
        //    // approximatePlafondAmount adalah plafond yg diupdate dari inacbgs

        //    if (srRegistrationType == AppConstant.RegistrationType.InPatient)
        //    {
        //        decimal plafondAmt = plavonAmount ?? 0;
        //        if (GuarantorBpjs.Contains(guarantorID) && plafondAmt == 0)
        //            plafondAmt = (decimal)(approximatePlafondAmount == null ? 0 : approximatePlafondAmount);

        //        return plafondAmt + additionalPlafond;
        //    }
        //    else
        //    {
        //        //TODO: [230324 Handono] Cari info apa plavond dari Inacbg juga berlaku untuk pasien selain rawat inap
        //        // Ambil dari parameter
        //        var parNonInPatientBpjsPlafond = AppParameter.GetParameterValue(AppParameter.ParameterItem.NonInPatientBpjsPlafond).ToDecimal();
        //        return parNonInPatientBpjsPlafond > 0 ? parNonInPatientBpjsPlafond : (approximatePlafondAmount ?? 0);
        //    }
        //}
        //private static string[] _guarantorBpjs = null;
        //internal static string[] GuarantorBpjs
        //{
        //    get
        //    {
        //        if (_guarantorBpjs != null) return _guarantorBpjs;
        //        var grr = new GuarantorBridgingCollection();
        //        grr.Query.es.Distinct = true;
        //        grr.Query.Where(grr.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(),
        //                                                    AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(),
        //                                                    AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
        //        if (grr.Query.Load()) _guarantorBpjs = grr.Select(g => g.GuarantorID).ToArray();
        //        else _guarantorBpjs = new string[] { string.Empty };

        //        return _guarantorBpjs;
        //    }
        //    set
        //    {
        //        _guarantorBpjs = value;
        //    }
        //}
        //#endregion


        /// <summary>
        /// High Risk Color
        /// </summary>
        /// Create By: Fajri
        /// Create Date: 2023-March-27
        /// Client Req: RSYS
        /// <param name="srRiskColor"></param>
        /// <returns></returns>
        public string GetRiskColor(object srRiskColor)
        {
            var color = string.Empty;

            var prColor = new AppStandardReferenceItem();
            prColor.LoadByPrimaryKey("PatientRiskColor", srRiskColor.ToString());
            if (string.IsNullOrEmpty(srRiskColor.ToString()))
                color = "Gray";
            else
                color = prColor.ReferenceID;

            return color;
        }

        private StringBuilder StrbResponseScripts { get; } = new StringBuilder();

        protected string UpdateStatScript(string statType, object regNo, object fregNo, object regType, object patId, object dob, object parId)
        {
            if (statType == "ews" && !regType.Equals(AppConstant.RegistrationType.InPatient))
                return string.Empty;

            if (statType == "plafond" && !AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsEmrListShowPlafondProgress))
                return String.Empty;

            if (statType == "pathway" && hdnIsClinicalPathwayActive.Value == "n")
                return string.Empty;

            var script = string.Format("UpdateStateEmrList(\"{6}\",\"{6}{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{7}\");", regNo.ToString().Replace("/", "_"),
                regNo, fregNo, regType, patId, dob, statType, parId);

            // Tampung script untuk diregistrasi pada AjaxManager.ResponseScripts karena AJAX defaultnya tidak akan menjalankan script yg ada di page
            StrbResponseScripts.AppendLine(script);
            return script;
        }


        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            if (IsPostBack)
                AjaxManager.ResponseScripts.Add(StrbResponseScripts.ToString());
        }
    }
}
