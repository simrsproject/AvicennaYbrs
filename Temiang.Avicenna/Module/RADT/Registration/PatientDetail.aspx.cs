/// ------------------------------------------------------------------------------------------------------ ///
/// Purpose   : Entry Master Patient
/// Busines Rules : (IMPORTANT !! MUST UNDERSTAND BEFORE MODIFIED & ADDED NEW CONDITION HERE)
///                 
/// ------------------------------------------------------------------------------------------------------ ///

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Appointment = Temiang.Avicenna.BusinessObject.Appointment;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web.UI;
using System.Linq;
using Temiang.Avicenna.Bridging.PCare;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientDetail : BasePageDialog
    {
        protected bool IsNewRecord
        {
            get { return (bool)ViewState["mode"]; }
            set
            {
                ViewState["mode"] = value;
            }
        }

        private AppAutoNumberLast _autoNumberLastPID;
        private AppAutoNumberLast _autoNumberLastMRN;

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            //return "oWnd.argument.id = '" + txtPatientID.Text.Trim() + "'";
            string script;

            // balikin valuenya pake md2 karena md akan diupdate dari new ke edit saat save tanpa close
            if (Request.QueryString["unit"] == null)
            {
                if (Request.QueryString["apptNo"] != null)
                    script = "oWnd.argument.mode = '" + Request.QueryString["md2"] + "|" + Request.QueryString["apptNo"] + "|" + txtPatientID.Text.Trim() + "'";
                else if (Request.QueryString["bpjsNo"] != null)
                    script = "oWnd.argument.mode = '" + "value!pasien!" + txtPatientID.Text + "'";
                else if (!string.IsNullOrWhiteSpace(Request.QueryString["gfid"]))
                    script = "oWnd.argument.mode = '" + Request.QueryString["md2"] + "|" + Request.QueryString["gfid"] + "|" + txtPatientID.Text.Trim() + "'";
                else
                    script = "oWnd.argument.mode = '" + Request.QueryString["md2"] + "|" + txtPatientID.Text.Trim() + "'";
            }
            else
            {
                if (Request.QueryString["sep"] == null)
                    script = "oWnd.argument.mode = 'rebind';oWnd.argument.PatientID = '" + txtPatientID.Text + "'";
                else
                    script = "oWnd.argument.mode = '" + Request.QueryString["md2"] + "|" + txtPatientID.Text.Trim() + "|" + Request.QueryString["sep"] + "'";
            }

            return script;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            IsNewRecord = Page.Request.QueryString["md"] == "new";

            // Add fitur Import Bpjs Member menggunakan pencarian berdasarkan No KTP atau no bpjs (Handono 231113 KLMM)
            var pCareValidation = ConfigurationManager.AppSettings["PCareValidation"];
            if (IsNewRecord && !string.IsNullOrEmpty(pCareValidation) && pCareValidation.ToUpper().Equals("YES"))
                btnImpBpjsMember.Visible = true;
            else
                btnImpBpjsMember.Visible = false;

            Button btnOK = ((Button)Helper.FindControlRecursive(Page, "btnOk"));
            btnOK.ValidationGroup = "entry";

            if (!string.IsNullOrEmpty(Page.Request.QueryString["emr"]))
            {
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;
                btnOK.Visible = false;
                this.Page.Title = "Patient Information";
            }
            else
            {
                string regType = Page.Request.QueryString["rt"];
                switch (regType)
                {
                    case AppConstant.RegistrationType.InPatient:
                        ProgramID = AppConstant.Program.Admitting;
                        btnOK.Visible = this.IsUserAddAble || this.IsUserEditAble;
                        break;
                    case AppConstant.RegistrationType.OutPatient:
                        ProgramID = AppConstant.Program.OutPatientRegistration;
                        btnOK.Visible = this.IsUserAddAble || this.IsUserEditAble;
                        break;
                    case AppConstant.RegistrationType.ClusterPatient:
                        ProgramID = AppConstant.Program.ClusterPatientRegistration;
                        btnOK.Visible = this.IsUserAddAble || this.IsUserEditAble;
                        break;
                    case AppConstant.RegistrationType.EmergencyPatient:
                        ProgramID = AppConstant.Program.EmergencyPatientRegistration;
                        btnOK.Visible = this.IsUserAddAble || this.IsUserEditAble;
                        break;
                    case AppConstant.RegistrationType.MedicalCheckUp:
                        ProgramID = AppConstant.Program.HealthScreeningRegistration;
                        btnOK.Visible = this.IsUserAddAble || this.IsUserEditAble;
                        break;
                    case AppConstant.RegistrationType.Ancillary:
                        ProgramID = AppConstant.Program.AncillaryRegistration;
                        btnOK.Visible = this.IsUserAddAble || this.IsUserEditAble;
                        break;
                    default:
                        break;
                }

                if (AppSession.Parameter.IsUsingUserAccessForEditPatient)
                {
                    if (!IsNewRecord)
                    {
                        btnOK.Visible = this.IsUserEditAble;
                    }
                }
            }

            if (btnOK.Visible)
            {
                // create tombol save tapi gak langsung tutup dialog
                var btn = new Button();
                btn.Text = "Save";
                btn.Click += btnSaveOnly_Click;

                var cell = btnOK.Parent as System.Web.UI.HtmlControls.HtmlTableCell;

                cell.Controls.AddAt(0, btn);

                btnOK.Width = new Unit(100, UnitType.Pixel);
                btnOK.Text = "Save and Close";
            }
        }

        protected void btnSaveOnly_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (this.OnButtonOkClicked())
            {
                if (Page.Request.QueryString["md"] == "new")
                {
                    var url = Helper.ReplaceUriQueryString(Helper.CurrentURL, "md", "edit");
                    url = Helper.ReplaceUriQueryString(url, "pid", txtPatientID.Text);
                    Response.Redirect(url);
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            IsNewRecord = Page.Request.QueryString["md"] == "new";

            //StandardReference Initialize
            if (!IsPostBack)
            {
                //hapus session patient id
                //Session.Remove("PatientID");

                // Remove Patient Picture Capture status
                CaptureImageFile = string.Empty;

                PopulateItemComboBox();

                switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                {
                    case "RSCH":
                        trRelation.Visible = false;
                        pnlParentInformationFather.Visible = false;
                        pnlFatherOccupation.Visible = false;

                        lblParentSpouseName.Text = "Father / Husband Name";
                        lblParentSpouseMedicalNo.Text = "Father / Husband MRN";

                        lblSRParentSpouseOccupation.Text = "Father / Husband Occupation";
                        lblParentSpouseOccupationDesc.Text = "Father / Husband Occupation Description";

                        lblMotherName.Text = "Mother / Wife Name";
                        lblMotherMedicalNo.Text = "Mother / Wife MRN";

                        lblMotherOccupation.Text = "Mother / Wife Occupation";
                        lblMotherOccupationDesc.Text = "Mother / Wife Occupation Description";

                        chkIsNotPaidOff.Visible = true;
                        break;
                    default:
                        trRelation.Visible = false;

                        break;
                }

                if (IsNewRecord)
                    InitializeNewPatient();
                else
                {
                    if (string.IsNullOrEmpty(Page.Request.QueryString["noka"])) PopulateEntryControl(Page.Request.QueryString["pid"]);
                    else
                    {
                        var patient = new Patient();
                        patient.Query.Where(patient.Query.GuarantorCardNo == Page.Request.QueryString["noka"]);
                        patient.Query.Load();
                        PopulateEntryControl(patient.PatientID);
                    }
                }

                //allergy data
                PatientAllergyCollection allergyCollection = new PatientAllergyCollection();
                allergyCollection.Query.Where(allergyCollection.Query.PatientID == txtPatientID.Text);
                allergyCollection.Query.OrderBy(allergyCollection.Query.AllergyGroup.Ascending);
                allergyCollection.LoadAll();

                AppStandardReferenceItemQuery query = new AppStandardReferenceItemQuery("a");

                query.Select
                    (
                        query.StandardReferenceID,
                        query.ItemID,
                        query.ItemName
                    );
                query.Where(query.ReferenceID == AppEnum.StandardReference.PatientHealthRecord);

                DataTable tbl = AllergyTable(query.LoadDataTable());

                foreach (DataRow row in tbl.Rows)
                {
                    foreach (BusinessObject.PatientAllergy all in allergyCollection)
                    {
                        if (((string)row[1] == all.AllergyGroup) && ((string)row[2] == all.Allergen))
                        {
                            row[4] = all.DescAndReaction;
                            break;
                        }
                    }
                }

                tbl.AcceptChanges();

                ViewState["collPatientAllergy" + Request.UserHostName] = tbl;


                //db:03-10-2023
                //kalo nambahin validasi, di appparameter u/ parametername jangan lupa ditambahin juga ya.. biar tau yg sudah bisa disetting apa aja
                var app = AppSession.Parameter.TablePatientFieldValidation;
                if (!string.IsNullOrEmpty(app))
                {
                    if (app.Contains("SRSalutation"))
                    {
                        rfvSRSalutation.Visible = true;
                    }
                    if (app.Contains("MiddleName"))
                    {
                        rfvMiddleName.Visible = true;
                    }
                    if (app.Contains("LastName"))
                    {
                        rfvLastName.Visible = true;
                    }
                    if (app.Contains("ParentSpouseName"))
                    {
                        rfvParentSpouseName.Visible = true;
                    }
                    if (app.Contains("MotherName"))
                    {
                        rfvMotherName.Visible = true;
                    }
                    if (app.Contains("SSN"))
                    {
                        rfvSSN.Visible = true;
                    }
                    if (app.Contains("SRPatienRelation"))
                    {
                        rfvSRPatienRelation.Visible = true;
                    }
                    if (app.Contains("SRReligion"))
                    {
                        rfvSRReligion.Visible = true;
                    }
                    if (app.Contains("SRMaritalStatus"))
                    {
                        rfvSRMaritalStatus.Visible = true;
                    }
                    if (app.Contains("StreetName"))
                    {
                        ctlAddress.RFVStreetName.Visible = true;
                    }
                    if (!Helper.IsDukcapilIntegration)
                    {
                        if (app.Contains("ZipCode"))
                        {
                            ctlAddress.RFVZipCode.Visible = true;
                        }
                    }
                    if (app.Contains("PhoneNo"))
                    {
                        ctlAddress.RFVPhoneNo.Visible = true;
                    }
                    //if (app.Contains("MobilePhoneNo"))
                    //{
                    //    ctlAddress.RFVMobilePhoneNo.Visible = true;
                    //}
                    if (app.Contains("SREthnic"))
                    {
                        rfvSREthnic.Visible = true;
                    }
                    if (app.Contains("SRPatientLanguage"))
                    {
                        rfvSRPatientLanguage.Visible = true;
                    }
                    if (app.Contains("SRNationality"))
                    {
                        rfvSRNationality.Visible = true;
                    }
                }

                if (AppSession.Parameter.IsNeedValidateMobilePhoneNo)
                {
                    ctlAddress.RFVMobilePhoneNo.Visible = true;
                }

                txtMedicalNo.ReadOnly = AppSession.Parameter.IsReadonlyMedicalNoOnPatientEntry || (!IsNewRecord && AppSession.Parameter.IsReadonlyMedicalNoOnEditPatientEntry);
                txtFirstName.ReadOnly = !IsNewRecord && AppSession.Parameter.IsReadonlyPatientNameOnEditPatientEntry;
                txtMiddleName.ReadOnly = !IsNewRecord && AppSession.Parameter.IsReadonlyPatientNameOnEditPatientEntry;
                txtLastName.ReadOnly = !IsNewRecord && AppSession.Parameter.IsReadonlyPatientNameOnEditPatientEntry;

                btnInfoDukcapil.Visible = Helper.IsDukcapilIntegration;
                trSyncDukcapil.Visible = Helper.IsDukcapilIntegration;
                lblSSN.Text = Helper.IsDukcapilIntegration ? "NIK/KTP (DUKCAPIL)" : "SSN";
                rfvSSN.ErrorMessage = Helper.IsDukcapilIntegration ? "NIK/KTP (DUKCAPIL) required." : "SSN required.";
            }
        }

        private void PopulateItemComboBox()
        {
            StandardReference.InitializeIncludeSpace(cboSRBloodType, AppEnum.StandardReference.BloodType);
            StandardReference.InitializeIncludeSpace(cboSREthnic, AppEnum.StandardReference.Ethnic);
            StandardReference.InitializeIncludeSpace(cboSREducation, AppEnum.StandardReference.Education);
            StandardReference.InitializeIncludeSpace(cboSRMaritalStatus, AppEnum.StandardReference.MaritalStatus);
            StandardReference.InitializeIncludeSpace(cboSRNationality, AppEnum.StandardReference.Nationality);
            StandardReference.InitializeIncludeSpace(cboSROccupation, AppEnum.StandardReference.Occupation);
            StandardReference.InitializeIncludeSpace(cboSRParentSpouseOccupation, AppEnum.StandardReference.Occupation);
            StandardReference.InitializeIncludeSpace(cboSRMotherOccupation, AppEnum.StandardReference.Occupation);
            StandardReference.InitializeIncludeSpace(cboSRFatherOccupation, AppEnum.StandardReference.Occupation);
            StandardReference.InitializeIncludeSpace(cboSRSalutation, AppEnum.StandardReference.Salutation);
            StandardReference.InitializeIncludeSpace(cboSRGenderType, AppEnum.StandardReference.GenderType);
            StandardReference.InitializeIncludeSpace(cboSRPatientCategory, AppEnum.StandardReference.PatientCategory);
            StandardReference.InitializeIncludeSpace(cboSRReligion, AppEnum.StandardReference.Religion);
            StandardReference.InitializeIncludeSpace(cboSRMedicalFileBin, AppEnum.StandardReference.MedicalFileBin);
            StandardReference.InitializeIncludeSpace(cboSRMedicalFileStatus, AppEnum.StandardReference.MedicalFileStatus);
            StandardReference.InitializeIncludeSpace(cboSRPatienRelation, AppEnum.StandardReference.Relationship);
            StandardReference.InitializeIncludeSpace(cboSRRelation, AppEnum.StandardReference.Relationship);
            StandardReference.InitializeIncludeSpace(cboInformation, AppEnum.StandardReference.InformationFrom);
            StandardReference.InitializeIncludeSpace(cboSRPatientLanguage, AppEnum.StandardReference.SRPatientLanguage);

            //GuarantorCollection guarantorColl = new GuarantorCollection();
            //guarantorColl.Query.Where(
            //    guarantorColl.Query.SRGuarantorType != AppSession.Parameter.GuarantorTypeMemberID,
            //    guarantorColl.Query.IsActive == true
            //    );
            //guarantorColl.LoadAll();
            //cboGuarantorID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            //foreach (Guarantor guarantor in guarantorColl)
            //{
            //    cboGuarantorID.Items.Add(new RadComboBoxItem(guarantor.GuarantorName, guarantor.GuarantorID));
            //}

            var guarantorColl = new GuarantorCollection();
            guarantorColl.Query.Where(
                guarantorColl.Query.SRGuarantorType == AppSession.Parameter.GuarantorTypeMemberID,
                guarantorColl.Query.IsActive == true
                );
            guarantorColl.LoadAll();

            cboMemberID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var entity in guarantorColl)
            {
                cboMemberID.Items.Add(new RadComboBoxItem(entity.GuarantorName, entity.GuarantorID));
            }
        }

        private void InitializeNewPatient()
        {
            ViewState["ResponTime" + Request.UserHostName] = (new DateTime()).NowAtSqlServer();

            //TODO:Un remark bila medical no dibuat otomatis
            if (!string.IsNullOrEmpty(Request.QueryString["unit"]))
            {
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(Request.QueryString["unit"]);
                if (unit.IsGenerateMedicalNo ?? false)
                {
                    _autoNumberLastMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);
                    txtMedicalNo.Text = _autoNumberLastMRN.LastCompleteNumber;
                }
            }

            _autoNumberLastPID = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PatientID);
            txtPatientID.Text = _autoNumberLastPID.LastCompleteNumber;

            chkIsActive.Checked = true;
            chkIsAlive.Checked = false;

            //Check if call from grid appointment, copy data appointment
            string apptNo = Page.Request.QueryString["apptNo"];
            if (!string.IsNullOrEmpty(apptNo))
            {
                InitializedNewPatientFromAppointment(apptNo);
            }
            else if (!string.IsNullOrEmpty(Page.Request.QueryString["gfid"]) && Session["gs"] != null)
            {
                var gfid = Page.Request.QueryString["gfid"];
                InitializedNewPatientFromGoogleForm(gfid);
            }

            string sep = Request.QueryString["sep"];
            if (!string.IsNullOrEmpty(sep))
            {
                var bpjs = new BpjsSEP();
                bpjs.LoadByPrimaryKey(sep);

                txtFirstName.Text = bpjs.NamaPasien;
                txtDateOfBirth.SelectedDate = bpjs.TanggalLahir;
                txtAgeInYear.Text = Helper.GetAgeInYear(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
                txtAgeInMonth.Text = Helper.GetAgeInMonth(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
                txtAgeInDay.Text = Helper.GetAgeInDay(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
                //rbtSex.SelectedValue = bpjs.JenisKelamin == "P" ? "F" : "M";
                cboSRGenderType.SelectedValue = bpjs.JenisKelamin == "P" ? "F" : "M";
                txtSSN.Text = bpjs.Nik;
                //cboGuarantorID.PopulateItemWithValue(AppSession.Parameter.GuarantorAskesID);
                txtGuarantorCardNo.Text = bpjs.NomorKartu;
            }

            string bpjsNo = Request.QueryString["bpjsNo"];
            if (!string.IsNullOrEmpty(bpjsNo))
            {
                _autoNumberLastMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);
                txtMedicalNo.Text = _autoNumberLastMRN.LastCompleteNumber;

                if (Request.QueryString["type"] == "bpjs")
                {
                    var service = new Temiang.Avicenna.Common.BPJS.VClaim.v11.Service();
                    var resp = service.GetPeserta(Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta, bpjsNo, DateTime.Now.Date);
                    if (resp.MetaData.IsValid)
                    {
                        var peserta = resp.Response.Peserta;
                        txtFirstName.Text = peserta.Nama;
                        txtDateOfBirth.SelectedDate = Convert.ToDateTime(peserta.TglLahir);
                        txtAgeInYear.Text = Helper.GetAgeInYear(txtDateOfBirth.SelectedDate.Value.Date).ToString();
                        txtAgeInMonth.Text = Helper.GetAgeInMonth(txtDateOfBirth.SelectedDate.Value.Date).ToString();
                        txtAgeInDay.Text = Helper.GetAgeInDay(txtDateOfBirth.SelectedDate.Value.Date).ToString();
                        //rbtSex.SelectedValue = peserta.Sex == "P" ? "F" : "M";
                        cboSRGenderType.SelectedValue = peserta.Sex == "P" ? "F" : "M";
                        txtSSN.Text = peserta.Nik;
                    }
                    //cboGuarantorID.PopulateItemWithValue(AppSession.Parameter.GuarantorAskesID);
                }
                else if (Request.QueryString["type"] == "inhealth")
                {
                    var service = new WebService.WSDL.Inhealth.InHealthWebService();
                    var response = service.EligibilitasPeserta(ConfigurationManager.AppSettings["InhealthHospitalToken"], Request.QueryString["bpjsNo"],
                        (new DateTime()).NowAtSqlServer().Date, ConfigurationManager.AppSettings["InhealthHospitalID"],
                        Request.QueryString["jp"], Request.QueryString["poli"]);

                    if (response.ERRORCODE == "00")
                    {
                        txtFirstName.Text = response.NMPST;
                        txtDateOfBirth.SelectedDate = Convert.ToDateTime(response.TGLLAHIR);
                        txtAgeInYear.Text = Helper.GetAgeInYear(txtDateOfBirth.SelectedDate.Value.Date).ToString();
                        txtAgeInMonth.Text = Helper.GetAgeInMonth(txtDateOfBirth.SelectedDate.Value.Date).ToString();
                        txtAgeInDay.Text = Helper.GetAgeInDay(txtDateOfBirth.SelectedDate.Value.Date).ToString();
                    }
                }

                txtGuarantorCardNo.Text = bpjsNo;
            }
        }

        private void InitializedNewPatientFromAppointment(string apptNo)
        {
            Appointment appt = new Appointment();
            appt.LoadByPrimaryKey(apptNo);
            txtFirstName.Text = appt.FirstName;
            txtMiddleName.Text = appt.MiddleName;
            txtLastName.Text = appt.LastName;
            txtNotes.Text = appt.Notes;
            txtDateOfBirth.SelectedDate = appt.DateOfBirth;
            if (appt.DateOfBirth != null)
            {
                txtAgeInYear.Text = Helper.GetAgeInYear(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
                txtAgeInMonth.Text = Helper.GetAgeInMonth(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
                txtAgeInDay.Text = Helper.GetAgeInDay(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
            }

            //cboGuarantorID.SelectedValue = appt.GuarantorID;
            cboGuarantorID.PopulateItemWithValue(appt.GuarantorID);
            ctlAddress.StreetName = appt.StreetName;
            ctlAddress.District = appt.District;
            ctlAddress.City = appt.City;
            ctlAddress.County = appt.County;
            ctlAddress.State = appt.State;

            if (!string.IsNullOrEmpty(appt.str.ZipCode))
            {
                ZipCodeQuery zip = new ZipCodeQuery();
                zip.es.Top = 1;
                zip.Where(zip.Or(zip.ZipCode.Like("%" + appt.str.ZipCode + "%"), zip.ZipPostalCode.Like("%" + appt.str.ZipCode + "%")));
                zip.OrderBy(zip.ZipPostalCode.Ascending, zip.ZipCode.Ascending);
                var dtb = zip.LoadDataTable();
                ctlAddress.ZipCodeCombo.DataSource = dtb;
                ctlAddress.ZipCodeCombo.DataBind();
                if (dtb.Rows.Count > 0) ctlAddress.ZipCodeCombo.SelectedValue = appt.str.ZipCode;
            }

            ctlAddress.PhoneNo = appt.PhoneNo;
            ctlAddress.MobilePhoneNo = appt.MobilePhoneNo;
            ctlAddress.FaxNo = appt.FaxNo;
            ctlAddress.Email = appt.Email;

            //rbtSex.SelectedValue = appt.Sex;
            cboSRGenderType.SelectedValue = appt.Sex;
            txtCityOfBirth.Text = appt.BirthPlace;
            txtEmployeeNo.Text = appt.EmployeeNo;
            txtEmployeeJobDepartementName.Text = appt.EmployeeJobDepartementName;
            txtEmployeeJobTitleName.Text = appt.EmployeeJobTitleName;
            txtSSN.Text = appt.Ssn;
            cboSRSalutation.SelectedValue = appt.SRSalutation;
            cboSRNationality.SelectedValue = appt.SRNationality;
            cboSROccupation.SelectedValue = appt.SROccupation;
            cboSRMaritalStatus.SelectedValue = appt.SRMaritalStatus;

            txtSSN.Text = appt.Ssn;
            txtGuarantorCardNo.Text = appt.GuarantorCardNo;
        }

        private void InitializedNewPatientFromGoogleForm(string gfid)
        {
            // Cek jika sudah terdaftar 
            var dtbGs = (DataTable)Session["gs"];
            var row = dtbGs.Rows.Find(Convert.ToDateTime(gfid));
            var patReg = LoadPatient(row["Name"].ToString(), Convert.ToDateTime(row["DateOfBirth"]), row["SSN"].ToString());

            if (patReg != null)
            {
                // switch mode ke edit
                IsNewRecord = false;
                PopulateEntryControl(patReg.PatientID);
            }

            // Timpa dgn data dari Google form
            txtFirstName.Text = row["Name"].ToString();
            //txtMiddleName.Text = appt.MiddleName;
            //txtLastName.Text = appt.LastName;
            txtDateOfBirth.SelectedDate = Convert.ToDateTime(row["DateOfBirth"]);
            txtCityOfBirth.Text = row["CityOfBirth"].ToString();
            if (!txtDateOfBirth.IsEmpty)
            {
                txtAgeInYear.Text = Helper.GetAgeInYear(txtDateOfBirth.SelectedDate ?? DateTime.Now.Date).ToString();
                txtAgeInMonth.Text = Helper.GetAgeInMonth(txtDateOfBirth.SelectedDate ?? DateTime.Now.Date).ToString();
                txtAgeInDay.Text = Helper.GetAgeInDay(txtDateOfBirth.SelectedDate ?? DateTime.Now.Date).ToString();
            }

            //rbtSex.SelectedValue = row["Gender"].ToString().ToLower().Equals("pria") ? "M" : "F";
            cboSRGenderType.SelectedValue = row["Gender"].ToString().ToLower().Equals("pria") ? "M" : "F";

            ComboBox.PopulateWithOneRow(cboGuarantorID, AppParameter.GetParameterValue(AppParameter.ParameterItem.GuarantorTypeSelf),
                BusinessObject.Common.Enums.EntityClassName.Guarantor, GuarantorMetadata.ColumnNames.GuarantorID, GuarantorMetadata.ColumnNames.GuarantorName);

            ctlAddress.MobilePhoneNo = row["HpNumber"].ToString();
            ctlAddress.Email = row["Email"].ToString();
            ctlAddress.StreetName = row["AddressStreet"].ToString();
            ctlAddress.City = row["AddressCity"].ToString();
            txtSSN.Text = row["SSN"].ToString();
        }

        internal static Patient LoadPatient(string firstName, DateTime dateOfBirth, string ssn)
        {
            Patient entity = new Patient();
            // Cek dengan SSN
            if (!string.IsNullOrWhiteSpace(ssn))
            {
                entity = new Patient();
                entity.Query.es.Top = 1;
                entity.Query.Where(entity.Query.Ssn == ssn,
                                    entity.Query.IsActive == true);
                if (entity.Query.Load())
                    return entity;
            }

            // Cek dengan nama dll
            entity = new Patient();
            entity.Query.es.Top = 1;
            entity.Query.Where(entity.Query.FirstName == firstName,
                                entity.Query.DateOfBirth.Date() == dateOfBirth,
                                entity.Query.IsActive == true);
            if (entity.Query.Load())
                return entity;


            return null;
        }

        private void SetEntityValue(Patient entity, PatientEmergencyContact contact, PatientDialysis dialysis)
        {
            //Generate Ulang untuk mencegah no sudah keburu dipakai user lain
            if (IsNewRecord)
            {
                //TODO:Un remark bila medical no dibuat otomatis
                if (!string.IsNullOrEmpty(Request.QueryString["unit"]))
                {
                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(Request.QueryString["unit"]);
                    if (unit.IsGenerateMedicalNo ?? false)
                    {
                        _autoNumberLastMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);
                        txtMedicalNo.Text = _autoNumberLastMRN.LastCompleteNumber;
                    }
                }

                string bpjsNo = Request.QueryString["bpjsNo"];
                if (!string.IsNullOrEmpty(bpjsNo))
                {
                    _autoNumberLastMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);
                    txtMedicalNo.Text = _autoNumberLastMRN.LastCompleteNumber;
                }

                _autoNumberLastPID = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PatientID);
                txtPatientID.Text = _autoNumberLastPID.LastCompleteNumber;
            }

            #region Patient

            entity.EmployeeNo = txtEmployeeNo.Text;
            entity.EmployeeJobDepartementName = txtEmployeeJobDepartementName.Text;
            entity.EmployeeJobTitleName = txtEmployeeJobTitleName.Text;

            entity.PatientID = txtPatientID.Text;
            if (IsNewRecord || txtMedicalNo.ReadOnly == false)
            {
                entity.MedicalNo = txtMedicalNo.Text.Trim();
            }
            entity.Ssn = txtSSN.Text;
            entity.PassportNo = txtPassportNo.Text;
            entity.SRSalutation = cboSRSalutation.SelectedValue;
            entity.SRPatienRelation = cboSRPatienRelation.SelectedValue;

            if (AppSession.Parameter.IsUppercasePatientID)
            {
                if (IsNewRecord || !AppSession.Parameter.IsReadonlyPatientNameOnEditPatientEntry)
                {
                    entity.FirstName = txtFirstName.Text.ToUpper();
                    entity.MiddleName = txtMiddleName.Text.ToUpper();
                    entity.LastName = txtLastName.Text.ToUpper();
                }

                entity.CityOfBirth = txtCityOfBirth.Text.ToUpper();
                entity.ParentSpouseName = txtParentSpouseName.Text.ToUpper();
                entity.MotherName = txtMotherName.Text.ToUpper();
                entity.FatherName = txtFatherName.Text.ToUpper();
            }
            else
            {
                if (IsNewRecord || !AppSession.Parameter.IsReadonlyPatientNameOnEditPatientEntry)
                {
                    entity.FirstName = txtFirstName.Text;
                    entity.MiddleName = txtMiddleName.Text;
                    entity.LastName = txtLastName.Text;
                }

                entity.CityOfBirth = txtCityOfBirth.Text;
                entity.ParentSpouseName = txtParentSpouseName.Text;
                entity.MotherName = txtMotherName.Text;
                entity.FatherName = txtFatherName.Text;
            }

            entity.DateOfBirth = txtDateOfBirth.SelectedDate;
            //entity.Sex = rbtSex.SelectedValue;
            entity.Sex = cboSRGenderType.SelectedValue;
            entity.SRBloodType = cboSRBloodType.SelectedValue;
            entity.BloodRhesus = rblBloodRhesus.SelectedItem == null ? "+" : rblBloodRhesus.SelectedItem.Text;
            entity.IsDisability = rblIsDisability.SelectedItem == null ? false : rblIsDisability.SelectedItem.Value == "1";
            entity.SREthnic = cboSREthnic.SelectedValue;
            entity.SRPatientLanguage = cboSRPatientLanguage.SelectedValue;
            entity.SREducation = cboSREducation.SelectedValue;
            entity.SRMaritalStatus = cboSRMaritalStatus.SelectedValue;
            entity.SRNationality = cboSRNationality.SelectedValue;
            entity.SROccupation = cboSROccupation.SelectedValue;
            entity.SRParentSpouseOccupation = cboSRParentSpouseOccupation.SelectedValue;
            entity.ParentSpouseOccupationDesc = txtParentSpouseOccupationDesc.Text;
            entity.ParentSpouseAge = Convert.ToInt16(txtParentSpouseAge.Value);
            entity.MotherAge = Convert.ToInt16(txtMotherAge.Value);
            entity.SRMotherOccupation = cboSRMotherOccupation.SelectedValue;
            entity.MotherOccupationDesc = txtMotherOccupationDesc.Text;
            entity.FatherAge = Convert.ToInt16(txtFatherAge.Value);
            entity.SRFatherOccupation = cboSRFatherOccupation.SelectedValue;
            entity.FatherOccupationDesc = txtFatherOccupationDesc.Text;

            entity.SRTitle = string.Empty;

            entity.SRPatientCategory = cboSRPatientCategory.SelectedValue;
            entity.SRReligion = cboSRReligion.SelectedValue;
            entity.ValuesOfTrust = txtValuesOfTrust.Text;
            entity.SRMedicalFileBin = cboSRMedicalFileBin.SelectedValue;
            entity.SRMedicalFileStatus = cboSRMedicalFileStatus.SelectedValue;
            entity.GuarantorID = cboGuarantorID.SelectedValue;
            entity.GuarantorCardNo = txtGuarantorCardNo.Text;
            entity.Company = txtCompany.Text;
            entity.CompanyAddress = txtCompanyAddress.Text;

            //Address
            if (AppSession.Parameter.IsUppercasePatientID)
            {
                entity.StreetName = ctlAddress.StreetName.ToUpper();
                entity.District = ctlAddress.District.ToUpper();
                entity.City = ctlAddress.City.ToUpper();
                entity.County = ctlAddress.County.ToUpper();
                entity.State = ctlAddress.State.ToUpper();
            }
            else
            {
                entity.StreetName = ctlAddress.StreetName;
                entity.District = ctlAddress.District;
                entity.City = ctlAddress.City;
                entity.County = ctlAddress.County;
                entity.State = ctlAddress.State;
            }
            entity.str.ZipCode = ctlAddress.ZipCode ?? string.Empty;
            entity.PhoneNo = ctlAddress.PhoneNo;
            entity.MobilePhoneNo = ctlAddress.MobilePhoneNo;

            entity.FaxNo = ctlAddress.FaxNo;
            entity.Email = ctlAddress.Email;

            //Temporary Address
            if (AppSession.Parameter.IsUppercasePatientID)
            {
                entity.str.TempAddressStreetName = txtTempAddressStreetName.Text.ToUpper();
                entity.str.TempAddressDistrict = txtTempAddressDistrict.Text.ToUpper();
                entity.str.TempAddressCity = txtTempAddressCity.Text.ToUpper();
                entity.str.TempAddressCounty = txtTempAddressCounty.Text.ToUpper();
                entity.str.TempAddressState = txtTempAddressState.Text.ToUpper();
            }
            else
            {
                entity.str.TempAddressStreetName = txtTempAddressStreetName.Text;
                entity.str.TempAddressDistrict = txtTempAddressDistrict.Text;
                entity.str.TempAddressCity = txtTempAddressCity.Text;
                entity.str.TempAddressCounty = txtTempAddressCounty.Text;
                entity.str.TempAddressState = txtTempAddressState.Text;
            }
            entity.str.TempAddressZipCode = cboZipCode.SelectedValue == null ? string.Empty : cboZipCode.SelectedValue;

            entity.OldMedicalNo = txtOldMedicalNo.Text;
            entity.AccountNo = txtAccountNo.Text;
            entity.PictureFileName = string.Empty;
            entity.IsDonor = chkIsDonor.Checked;
            entity.NumberOfDonor = Convert.ToDecimal(txtNumberOfDonor.Value);
            entity.LastDonorDate = txtLastDonorDate.SelectedDate;
            entity.IsBlackList = chkIsBlackList.Checked;
            entity.IsNotPaidOff = chkIsNotPaidOff.Checked;
            entity.IsAlive = !chkIsAlive.Checked;
            entity.IsActive = chkIsActive.Checked;
            entity.Notes = txtNotes.Text;
            entity.MemberID = cboMemberID.SelectedValue;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.ParentSpouseMedicalNo = txtParentSpouseMedicalNo.Text;
            entity.MotherMedicalNo = txtMotherMedicalNo.Text;

            if (!string.IsNullOrEmpty(Request.QueryString["tp"]))
            {
                if (Page.Request.QueryString["tp"] == "inp")
                    entity.IsNonPatient = true;
            }

            if (ViewState["ResponTime" + Request.UserHostName] != null && IsNewRecord)
                entity.ResponTime = (new DateTime()).NowAtSqlServer() - ((DateTime)ViewState["ResponTime" + Request.UserHostName]);

            if (entity.es.IsAdded)
                entity.HealthcareID = AppSession.Parameter.HealthcareID;
            else
            {
                if (string.IsNullOrEmpty(entity.HealthcareID))
                    entity.HealthcareID = AppSession.Parameter.HealthcareID;
            }

            entity.SRInformationFrom = cboInformation.SelectedValue;
            if (!txtDeceasedDate.IsEmpty)
            {
                var dt = new DateTime(txtDeceasedDate.SelectedDate.Value.Year, txtDeceasedDate.SelectedDate.Value.Month, txtDeceasedDate.SelectedDate.Value.Day, txtDeceasedTime.SelectedTime.Value.Hours, txtDeceasedTime.SelectedTime.Value.Minutes, 0);
                entity.DeceasedDateTime = dt;
            }
            else entity.str.DeceasedDateTime = string.Empty;
            entity.FamilyRegisterNo = txtFamilyRegisterNo.Text;
            if (Helper.IsDukcapilIntegration) entity.IsSyncWithDukcapil = chkSyncDukcapil.Checked;
            #endregion Patient

            #region Emergency Contact
            contact.PatientID = txtPatientID.Text;
            if (AppSession.Parameter.IsUppercasePatientID)
            {
                contact.ContactName = txtContactName.Text.ToUpper();
                contact.StreetName = txtEmrContactStreetName.Text.ToUpper();
                contact.District = txtEmrContactDistrict.Text.ToUpper();
                contact.City = txtEmrContactCity.Text.ToUpper();
                contact.County = txtEmrContactCounty.Text.ToUpper();
                contact.State = txtEmrContactState.Text.ToUpper();
            }
            else
            {
                contact.ContactName = txtContactName.Text;
                contact.StreetName = txtEmrContactStreetName.Text;
                contact.District = txtEmrContactDistrict.Text;
                contact.City = txtEmrContactCity.Text;
                contact.County = txtEmrContactCounty.Text;
                contact.State = txtEmrContactState.Text;
            }
            contact.SRRelationship = cboSRRelation.SelectedValue;
            contact.Ssn = txtContactSsn.Text;
            contact.ZipCode = cboEmrContactZipCode.SelectedValue;
            contact.PhoneNo = txtEmrContactPhoneNo.Text;
            contact.FaxNo = txtEmrContactFaxNo.Text;
            contact.MobilePhoneNo = txtEmrContactMobilePhoneNo.Text;
            contact.Email = txtEmrContactEmail.Text;
            contact.Notes = string.Empty;
            contact.LastUpdateByUserID = AppSession.UserLogin.UserID;
            contact.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            #endregion

            #region PatientDialysis
            dialysis.PatientID = txtPatientID.Text;
            dialysis.InitialDiagnosis = txtInitialDiagnosis.Text;
            dialysis.RefferingHospital = txtRefferingHospital.Text;
            dialysis.RefferingPhysician = txtRefferingPhysician.Text;
            dialysis.Hb = txtHb.Text;
            dialysis.HbDate = txtHbDate.SelectedDate;
            dialysis.Urea = txtUrea.Text;
            dialysis.UreaDate = txtUreaDate.SelectedDate;
            dialysis.Creatinine = txtCreatinine.Text;
            dialysis.CreatinineDate = txtCreatinineDate.SelectedDate;
            dialysis.HBsAg = txtHBsAg.Text;
            dialysis.HBsAgDate = txtHBsAgDate.SelectedDate;
            dialysis.AntiHCV = txtAntiHCV.Text;
            dialysis.AntiHCVDate = txtAntiHCVDate.SelectedDate;
            dialysis.AntiHIV = txtAntiHIV.Text;
            dialysis.AntiHIVDate = txtAntiHIVDate.SelectedDate;
            dialysis.KidneyUltrasound = txtKidneyUltrasound.Text;
            dialysis.KidneyUltrasoundDate = txtKidneyUltrasoundDate.SelectedDate;
            dialysis.ECHO = txtECHO.Text;
            dialysis.ECHODate = txtECHODate.SelectedDate;
            dialysis.FirstHDDate = txtFirstHemodialysisDate.SelectedDate;
            dialysis.TransferHDDate = txtTransferHDDate.SelectedDate;
            dialysis.FirstPDDate = txtFirstPeritonealDialysisDate.SelectedDate;
            dialysis.TransferPDDate = txtTransferPDDate.SelectedDate;
            dialysis.KidneyTransplantDate = txtKidneytransplantDate.SelectedDate;

            if (dialysis.es.IsAdded)
            {
                dialysis.CreatedByUserID = AppSession.UserLogin.UserID;
                dialysis.CreatedDateTime = DateTime.Now;
                dialysis.LastUpdateByUserID = AppSession.UserLogin.UserID;
                dialysis.LastUpdateDateTime = DateTime.Now;
            }
            else if (dialysis.es.IsModified)
            {
                dialysis.LastUpdateByUserID = AppSession.UserLogin.UserID;
                dialysis.LastUpdateDateTime = DateTime.Now;
            }
            #endregion
        }

        private void PopulateEntryControl(string patientID)
        {
            //Load record
            var patient = new Patient();
            if (!patient.LoadByPrimaryKey(patientID)) return;

            //Populate Control
            txtPatientID.Text = patient.PatientID;
            txtMedicalNo.Text = patient.MedicalNo;
            txtSSN.Text = patient.Ssn;
            txtPassportNo.Text = patient.PassportNo;
            cboSRSalutation.SelectedValue = patient.SRSalutation;
            txtFirstName.Text = patient.FirstName;
            txtMiddleName.Text = patient.MiddleName;
            txtLastName.Text = patient.LastName;
            txtCityOfBirth.Text = patient.CityOfBirth;
            txtDateOfBirth.SelectedDate = patient.DateOfBirth;
            txtAgeInYear.Text = Helper.GetAgeInYear(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
            txtAgeInMonth.Text = Helper.GetAgeInMonth(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
            txtAgeInDay.Text = Helper.GetAgeInDay(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
            //rbtSex.SelectedValue = patient.Sex;
            cboSRGenderType.SelectedValue = patient.Sex;
            txtParentSpouseName.Text = patient.ParentSpouseName;
            txtMotherName.Text = patient.MotherName;
            cboSRPatienRelation.SelectedValue = patient.SRPatienRelation;
            cboSRBloodType.SelectedValue = patient.SRBloodType;
            rblBloodRhesus.SelectedValue = (patient.BloodRhesus == "+" ? "0" : "1");
            rblIsDisability.SelectedValue = ((patient.IsDisability ?? false == true) ? "1" : "0");
            ComboBox.PopulateWithOneStandardReference(cboSREthnic, "Ethnic", patient.SREthnic);
            ComboBox.PopulateWithOneStandardReference(cboSRNationality, "Nationality", patient.SRNationality);
            ComboBox.PopulateWithOneStandardReference(cboSREducation, "Education", patient.SREducation);
            cboSRMaritalStatus.SelectedValue = patient.SRMaritalStatus;
            cboSROccupation.SelectedValue = patient.SROccupation;
            cboSRPatientCategory.SelectedValue = patient.SRPatientCategory;
            cboSRReligion.SelectedValue = patient.SRReligion;
            cboSRPatientLanguage.SelectedValue = patient.SRPatientLanguage;
            txtValuesOfTrust.Text = patient.ValuesOfTrust;
            cboSRMedicalFileBin.SelectedValue = patient.SRMedicalFileBin;
            cboSRMedicalFileStatus.SelectedValue = patient.SRMedicalFileStatus;
            cboSRParentSpouseOccupation.SelectedValue = patient.SRParentSpouseOccupation;
            txtParentSpouseOccupationDesc.Text = patient.ParentSpouseOccupationDesc;
            txtParentSpouseAge.Value = Convert.ToDouble(patient.ParentSpouseAge);
            txtMotherAge.Value = Convert.ToDouble(patient.MotherAge);
            cboSRMotherOccupation.SelectedValue = patient.SRMotherOccupation;
            txtMotherOccupationDesc.Text = patient.MotherOccupationDesc;
            txtFatherName.Text = patient.FatherName;
            txtFatherAge.Value = Convert.ToDouble(patient.FatherAge);
            cboSRFatherOccupation.SelectedValue = patient.SRFatherOccupation;
            txtFatherOccupationDesc.Text = patient.FatherOccupationDesc;

            cboGuarantorID.PopulateItemWithValue(patient.GuarantorID);

            txtGuarantorCardNo.Text = patient.GuarantorCardNo;
            txtCompany.Text = patient.Company;
            txtCompanyAddress.Text = patient.CompanyAddress;

            txtParentSpouseMedicalNo.Text = patient.str.ParentSpouseMedicalNo;
            txtMotherMedicalNo.Text = patient.str.MotherMedicalNo;

            txtEmployeeNo.Text = patient.EmployeeNo;
            txtEmployeeJobDepartementName.Text = patient.EmployeeJobDepartementName;
            txtEmployeeJobTitleName.Text = patient.EmployeeJobTitleName;

            //Address
            ctlAddress.StreetName = patient.StreetName;
            ctlAddress.District = patient.District;
            ctlAddress.City = patient.City;
            ctlAddress.County = patient.County;
            ctlAddress.State = patient.State;

            var zip = new ZipCodeQuery();
            zip.Where(zip.ZipCode == patient.str.ZipCode);

            ctlAddress.ZipCodeCombo.DataSource = zip.LoadDataTable();
            ctlAddress.ZipCodeCombo.DataBind();

            bool exist = false;
            foreach (RadComboBoxItem item in ctlAddress.ZipCodeCombo.Items)
            {
                if (item.Value == patient.str.ZipCode)
                {
                    exist = true;
                    break;
                }
            }

            if (exist)
            {
                ctlAddress.ZipCodeCombo.SelectedValue = patient.str.ZipCode;

                ctlAddress.TxtDistrict.ReadOnly = true;
                ctlAddress.TxtCity.ReadOnly = true;
                ctlAddress.TxtCounty.ReadOnly = true;
                ctlAddress.TxtState.ReadOnly = true;
            }
            else
            {
                ctlAddress.ZipCodeCombo.Text = patient.str.ZipCode;

                ctlAddress.TxtDistrict.ReadOnly = false;
                ctlAddress.TxtCity.ReadOnly = false;
                ctlAddress.TxtCounty.ReadOnly = false;
                ctlAddress.TxtState.ReadOnly = false;
            }

            ctlAddress.PhoneNo = patient.PhoneNo;
            ctlAddress.MobilePhoneNo = patient.MobilePhoneNo;

            ctlAddress.FaxNo = patient.FaxNo;
            ctlAddress.Email = patient.Email;

            //Temporary Address
            txtTempAddressStreetName.Text = patient.TempAddressStreetName;
            txtTempAddressDistrict.Text = patient.TempAddressDistrict;
            txtTempAddressCity.Text = patient.TempAddressCity;
            txtTempAddressCounty.Text = patient.TempAddressCounty;
            txtTempAddressState.Text = patient.TempAddressState;

            zip = new ZipCodeQuery();
            zip.Where(zip.ZipCode == patient.str.TempAddressZipCode);

            cboZipCode.DataSource = zip.LoadDataTable();
            cboZipCode.DataBind();

            exist = false;
            foreach (RadComboBoxItem item in cboZipCode.Items)
            {
                if (item.Value == patient.str.TempAddressZipCode)
                {
                    exist = true;
                    break;
                }
            }

            if (exist)
                cboZipCode.SelectedValue = patient.str.TempAddressZipCode;
            else
                cboZipCode.Text = patient.str.TempAddressZipCode;

            var health = new HealthcareQuery();
            health.Where(health.HealthcareID == patient.HealthcareID);
            txtHealthcareName.Text = health.HealthcareName;

            txtLastVisitDate.SelectedDate = patient.LastVisitDate;
            txtNumberOfVisit.Value = Convert.ToDouble(patient.NumberOfVisit);
            txtOldMedicalNo.Text = patient.OldMedicalNo;
            txtAccountNo.Text = patient.AccountNo;
            chkIsDonor.Checked = patient.IsDonor ?? false;
            txtNumberOfDonor.Value = Convert.ToDouble(patient.NumberOfDonor);
            txtLastDonorDate.SelectedDate = patient.LastDonorDate;
            chkIsBlackList.Checked = patient.IsBlackList ?? false;
            chkIsNotPaidOff.Checked = patient.IsNotPaidOff ?? false;
            chkIsAlive.Checked = !(patient.IsAlive ?? false);
            chkIsActive.Checked = patient.IsActive ?? false;
            txtDiagnosticNo.Text = patient.DiagnosticNo;
            txtNotes.Text = patient.Notes;
            cboMemberID.SelectedValue = patient.MemberID;
            cboInformation.SelectedValue = patient.SRInformationFrom;

            //emergency contact
            var contact = new PatientEmergencyContact();
            if (contact.LoadByPrimaryKey(patientID))
            {
                txtContactName.Text = contact.ContactName;
                cboSRRelation.SelectedValue = contact.SRRelationship;
                txtContactSsn.Text = contact.Ssn;

                txtEmrContactStreetName.Text = contact.StreetName;
                txtEmrContactDistrict.Text = contact.District;
                txtEmrContactCity.Text = contact.City;
                txtEmrContactCounty.Text = contact.County;
                txtEmrContactState.Text = contact.State;

                var contactzip = new ZipCodeQuery();
                contactzip.Where(contactzip.ZipCode == contact.str.ZipCode);

                cboEmrContactZipCode.DataSource = contactzip.LoadDataTable();
                cboEmrContactZipCode.DataBind();

                cboEmrContactZipCode.SelectedValue = contact.str.ZipCode;

                txtEmrContactPhoneNo.Text = contact.PhoneNo;
                txtEmrContactFaxNo.Text = contact.FaxNo;
                txtEmrContactMobilePhoneNo.Text = contact.MobilePhoneNo;
                txtEmrContactEmail.Text = contact.Email;
            }

            var patRelated = new PatientRelatedCollection();
            patRelated.Query.Where(patRelated.Query.RelatedPatientID == patientID);
            patRelated.LoadAll();
            chkIsActive.Enabled = patRelated.Count <= 0;

            txtDeceasedDate.SelectedDate = patient.DeceasedDateTime;
            txtDeceasedTime.SelectedDate = patient.DeceasedDateTime;
            txtFamilyRegisterNo.Text = patient.FamilyRegisterNo;
            chkSyncDukcapil.Checked = patient.IsSyncWithDukcapil ?? false;

            // patient dialysis
            var dialysis = new PatientDialysis();
            if (dialysis.LoadByPrimaryKey(patientID))
            {
                txtInitialDiagnosis.Text = dialysis.InitialDiagnosis;
                txtRefferingHospital.Text = dialysis.RefferingHospital;
                txtRefferingPhysician.Text = dialysis.RefferingPhysician;
                txtHb.Text = dialysis.Hb;
                txtHbDate.SelectedDate = dialysis.HbDate;
                txtUrea.Text = dialysis.Urea;
                txtUreaDate.SelectedDate = dialysis.UreaDate;
                txtCreatinine.Text = dialysis.Creatinine;
                txtCreatinineDate.SelectedDate = dialysis.CreatinineDate;
                txtHBsAg.Text = dialysis.HBsAg;
                txtHBsAgDate.SelectedDate = dialysis.HBsAgDate;
                txtAntiHCV.Text = dialysis.AntiHCV;
                txtAntiHCVDate.SelectedDate = dialysis.AntiHCVDate;
                txtAntiHIV.Text = dialysis.AntiHIV;
                txtAntiHIVDate.SelectedDate = dialysis.AntiHIVDate;
                txtKidneyUltrasound.Text = dialysis.KidneyUltrasound;
                txtKidneyUltrasoundDate.SelectedDate = dialysis.KidneyUltrasoundDate;
                txtECHO.Text = dialysis.ECHO;
                txtECHODate.SelectedDate = dialysis.ECHODate;
                txtFirstHemodialysisDate.SelectedDate = dialysis.FirstHDDate;
                txtTransferHDDate.SelectedDate = dialysis.TransferHDDate;
                txtFirstPeritonealDialysisDate.SelectedDate = dialysis.FirstPDDate;
                txtTransferPDDate.SelectedDate = dialysis.TransferPDDate;
                txtKidneytransplantDate.SelectedDate = dialysis.KidneyTransplantDate;
            }

            PopulatePatientImage(patientID);

        }

        private bool IsDuplicateNewMedicalNo()
        {
            PatientQuery patientQuery = new PatientQuery();
            patientQuery.es.Top = 1;
            patientQuery.Select(patientQuery.PatientName, patientQuery.PatientID);
            patientQuery.Where(patientQuery.MedicalNo == txtMedicalNo.Text);
            DataTable dtb = patientQuery.LoadDataTable();
            if (dtb.Rows.Count > 0 && !dtb.Rows[0]["PatientID"].Equals(txtPatientID.Text))
            {
                this.ShowInformationHeader(
                    string.Format("MRN: {0} has been used by another patient, please change to other No.",
                                  txtMedicalNo.Text));
                return true;
            }
            return false;
        }

        private bool SaveNew()
        {
            Patient entity = new Patient();
            PatientEmergencyContact contact = new PatientEmergencyContact();
            PatientDialysis dialysis = new PatientDialysis();

            entity.AddNew();
            contact.AddNew();
            dialysis.AddNew();

            SetEntityValue(entity, contact, dialysis);

            var coll = new RegistrationCollection();

            SaveEntity(entity, contact, dialysis, coll);

            return true;
        }

        #region PatientImage
        private void PopulatePatientImage(string patientID)
        {
            // Patient Photo
            imgPatientPhoto.ImageUrl = string.Empty;
            if (!string.IsNullOrEmpty(CaptureImageFile))
            {
                // Load form webcam capture
                var capturedImageFileArgs = CaptureImageFile.Split('|');
                var capturedImageFile = capturedImageFileArgs[0];
                if (Convert.ToBoolean(capturedImageFileArgs[2]) == true)
                {
                    var imgByteArr = (new ImageHelper()).LoadImageToArray(capturedImageFile);
                    if (imgByteArr != null)
                    {
                        imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                            Convert.ToBase64String(imgByteArr));
                        return;
                    }
                }
            }

            // Load from database
            var patientImg = new PatientImage();
            if (patientImg.LoadByPrimaryKey(patientID))
            {
                if (patientImg.Photo != null)
                {
                    imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(patientImg.Photo));
                }
                else
                {
                    //imgPatientPhoto.ImageUrl = rbtSex.SelectedValue == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                    imgPatientPhoto.ImageUrl = cboSRGenderType.SelectedValue == "M" ? "~/Images/Asset/Patient/ManVector.png" : (cboSRGenderType.SelectedValue == "F" ? "~/Images/Asset/Patient/WomanVector.png" : "~/Images/Asset/Patient/HumanVector.png");
                }
            }
            else
                //imgPatientPhoto.ImageUrl = rbtSex.SelectedValue == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                imgPatientPhoto.ImageUrl = cboSRGenderType.SelectedValue == "M" ? "~/Images/Asset/Patient/ManVector.png" : (cboSRGenderType.SelectedValue == "F" ? "~/Images/Asset/Patient/WomanVector.png" : "~/Images/Asset/Patient/HumanVector.png");

        }
        private string CaptureImageFile
        {
            get
            {
                var obj = Session["capturedImageFile"];
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                    return obj.ToString();
                return string.Empty;
            }
            set
            {
                Session["capturedImageFile"] = string.Empty;
            }
        }
        //private void SavePatientImage(string patientID)
        //{
        //    if (!string.IsNullOrEmpty(CaptureImageFile))
        //    {
        //        var capturedImageFileArgs = CaptureImageFile.Split('|');
        //        if (Convert.ToBoolean(capturedImageFileArgs[2]) == true) // Save hanya jika statusnya sudah di crop
        //        {
        //            var patientImg = new PatientImage();
        //            if (!patientImg.LoadByPrimaryKey(patientID))
        //            {
        //                patientImg.PatientID = patientID;
        //            }

        //            var imgByteArr = ImageHelper.LoadImageToArray(capturedImageFileArgs[0]);
        //            if (imgByteArr != null)
        //            {
        //                patientImg.Photo = imgByteArr;
        //                patientImg.Save();
        //            }
        //        }
        //    }
        //}

        private void SavePatientImage(string patientID)
        {

            if (!string.IsNullOrWhiteSpace(hdnImgData.Value))
            {
                // Contoh data 
                //  - dari JCrop  -> data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD...
                //  - dari CropIt -> data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAA...
                var imgHelper = new ImageHelper();
                var dataImage = imgHelper.ConvertBase64StringToImage(hdnImgData.Value.Split(',')[1]);
                var patientImg = new PatientImage();
                if (!patientImg.LoadByPrimaryKey(patientID))
                {
                    patientImg.PatientID = patientID;
                }

                var resizedImg = imgHelper.ResizeImage(dataImage, new System.Drawing.Size(240, 240), true, System.Drawing.Drawing2D.InterpolationMode.Default);
                var compressedImg = imgHelper.CompressImageToArray(resizedImg, 100); // 115KB from 14KB 
                if (compressedImg != null)
                {
                    patientImg.Photo = compressedImg;
                    patientImg.Save();
                }
            }
        }

        #endregion

        #region Imunization History
        private void SaveImmunization(string PatientID)
        {
            // Hapus yg hanya dari entry manual
            var coll = new PatientImmunizationCollection();
            coll.Query.Where(coll.Query.PatientID == PatientID);
            coll.LoadAll();
            coll.MarkAllAsDeleted();
            coll.Save();

            // Add yg entry manual
            coll = new PatientImmunizationCollection();
            foreach (GridDataItem item in grdImunizationHist.MasterTableView.Items)
            {
                for (int i = 1; i < 11; i++)
                {
                    AddPatientManualImmunization(coll, item, i);
                }
            }

            coll.Save();

        }

        private void AddPatientManualImmunization(PatientImmunizationCollection coll, GridDataItem item, int seqNo)
        {
            if (!string.IsNullOrEmpty(item["ReferenceNo"].Text) && !item["ReferenceNo"].Text.Equals("&nbsp;")) return;
            if (item["MaxCount"].Text.ToInt() < seqNo) return;

            var date = ((RadMonthYearPicker)item.FindControl(string.Format("txtMonthYear_{0:00}", seqNo))).SelectedDate;
            var isChecked = ((CheckBox)item.FindControl(string.Format("chkDate_{0:00}", seqNo))).Checked;
            if (date != null || isChecked)
            {
                var ent = coll.AddNew();
                ent.PatientID = txtPatientID.Text;
                ent.ImmunizationID = item["ImmunizationID"].Text;
                ent.ImmunizationNo = seqNo;
                ent.IsDateInMonthYear = true;

                if (date != null)
                    ent.ImmunizationDate = date;
            }
        }
        protected void grdImunizationHist_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)sender).DataSource = PatientImunization();
        }


        private DataTable PatientImunization()
        {
            var query = new ImmunizationQuery("a");
            query.Select(query.ImmunizationID, query.ImmunizationName, query.MaxCount);
            query.OrderBy(query.IndexNo.Ascending);
            var dtb = query.LoadDataTable();
            for (int i = 1; i < 11; i++)
            {
                dtb.Columns.Add(string.Format("Date_0{0}", i), typeof(DateTime));
                dtb.Columns.Add(string.Format("IsChecked_0{0}", i), typeof(bool));
            }

            dtb.PrimaryKey = new[] { dtb.Columns["ImmunizationID"] };

            if (dtb.Rows == null || dtb.Rows.Count == 0) return dtb;

            // Populate Imunization Date
            var pid = Page.Request.QueryString["pid"];
            if (!string.IsNullOrEmpty(pid))
            {
                var qrImun = new PatientImmunizationQuery("b");
                qrImun.Where(qrImun.PatientID == Page.Request.QueryString["pid"]);
                qrImun.Select(qrImun.ImmunizationID, qrImun.ImmunizationNo, qrImun.ImmunizationDate);
                qrImun.OrderBy(qrImun.ImmunizationID.Ascending, qrImun.ImmunizationNo.Ascending);

                var dtbPatientImun = qrImun.LoadDataTable();
                var rowHd = dtb.Rows[0];
                foreach (DataRow row in dtbPatientImun.Rows)
                {
                    if (rowHd["ImmunizationID"].ToString() != row["ImmunizationID"].ToString())
                        rowHd = dtb.Rows.Find(row["ImmunizationID"].ToString());

                    if (rowHd == null) continue;

                    for (int i = 1; i < 11; i++)
                    {
                        if (i.Equals(row["ImmunizationNo"]))
                        {
                            rowHd[string.Format("Date_0{0}", i)] = row["ImmunizationDate"];
                            rowHd[string.Format("IsChecked_0{0}", i)] = true;
                            break;
                        }
                    }
                }
            }
            


            return dtb;
        }

        protected void grdImunizationHist_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                for (var i = 1; i < 11; i++)
                {
                    PopulateCell(item, i);
                }
            }
        }

        private void PopulateCell(GridDataItem item, int seqNo)
        {
            var isExistRef = (!string.IsNullOrEmpty(item["ReferenceNo"].Text) &&
                !item["ReferenceNo"].Text.Equals("&nbsp;"));
            var txtDate = (RadMonthYearPicker)item.FindControl(string.Format("txtMonthYear_{0:00}", seqNo));
            var chkDate = (CheckBox)item.FindControl(string.Format("chkDate_{0:00}", seqNo));
            txtDate.Visible = item["MaxCount"].Text.ToInt() >= seqNo;
            chkDate.Visible = txtDate.Visible;

            // Date
            var date = item[string.Format("Date_{0:00}", seqNo)].Text;
            if (!string.IsNullOrEmpty(date) && !date.ToLower().Equals("&nbsp;"))
                txtDate.SelectedDate = Convert.ToDateTime(date);
            else
                txtDate.SelectedDate = null;
            //txtDate.Enabled = IsEdited && !isExistRef;


            // Check
            var isChecked = item[string.Format("IsChecked_{0:00}", seqNo)].Text;
            if (!string.IsNullOrEmpty(isChecked) && !isChecked.ToLower().Equals("&nbsp;"))
                chkDate.Checked = Convert.ToBoolean(isChecked);
            //chkDate.Enabled = IsEdited && !isExistRef;

            if (chkDate.Checked)
                item[string.Format("InputDate_{0:00}", seqNo)].BackColor = System.Drawing.Color.DodgerBlue;

            //if (!txtDate.Visible)
            //    item[string.Format("InputDate_{0:00}", seqNo)].BackColor = System.Drawing.Color.LightGray;

        }
        #endregion


        private void SaveEntity(Patient entity, PatientEmergencyContact contact, PatientDialysis dialysis, RegistrationCollection coll)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                // Patient Photo
                SavePatientImage(entity.PatientID);

                SaveImmunization(entity.PatientID);

                // Jika dari Appointment maka update ID
                if (IsNewRecord)
                {
                    string apptNo = Page.Request.QueryString["apptNo"];
                    if (!string.IsNullOrEmpty(apptNo))
                    {
                        Appointment appt = new Appointment();
                        appt.LoadByPrimaryKey(apptNo);
                        appt.PatientID = entity.PatientID;
                        appt.Save();
                    }
                }

                //EmergencyContact
                if (contact != null)
                    contact.Save();

                //Diaysis
                if (dialysis != null)
                    dialysis.Save();

                if (IsNewRecord)
                {
                    if (_autoNumberLastPID != null)
                        _autoNumberLastPID.Save();

                    //TODO:Un remark bila medical no dibuat otomatis
                    if (_autoNumberLastMRN != null && txtMedicalNo.Text.Length > 0 && txtMedicalNo.Text == _autoNumberLastMRN.LastCompleteNumber)
                        _autoNumberLastMRN.Save();
                }

                PatientAllergyCollection all = new PatientAllergyCollection();
                all.Query.Where(all.Query.PatientID == entity.PatientID);
                all.LoadAll();
                all.MarkAllAsDeleted();
                all.Save();

                all = new PatientAllergyCollection();

                foreach (GridDataItem item in grdPatientAllergy.MasterTableView.Items)
                {
                    string desc = ((RadTextBox)item.FindControl("txtAllergenDesc")).Text.Trim();
                    if (desc.Length > 0)
                    {
                        BusinessObject.PatientAllergy allergy = all.AddNew();
                        allergy.AllergyGroup = item["StandardReferenceID"].Text;
                        allergy.Allergen = item["ItemID"].Text;
                        allergy.AllergenName = item["ItemName"].Text;
                        allergy.SRAnaphylaxis = item["StandardReferenceID"].Text;
                        allergy.Anaphylaxis = item["StandardReferenceID"].Text;
                        allergy.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        allergy.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        allergy.PatientID = entity.PatientID;
                        allergy.DescAndReaction = desc;
                    }
                }

                all.Save();

                if (!IsNewRecord)
                {
                    if (coll.Count > 0)
                        coll.Save();
                }

                if (IsNewRecord)
                {
                    if (Request.QueryString["sep"] != null)
                    {
                        var bpjs = new BpjsSEP();
                        bpjs.LoadByPrimaryKey(Request.QueryString["sep"]);
                        bpjs.PatientID = entity.PatientID;
                        bpjs.Save();
                    }
                }

                //Commit if success, Rollback if failed
                trans.Complete();

            }
        }

        private bool SaveEdit()
        {
            Patient entity = new Patient();
            PatientEmergencyContact contact = new PatientEmergencyContact();
            PatientDialysis dialysis = new PatientDialysis();

            if (entity.LoadByPrimaryKey(txtPatientID.Text))
            {
                DateTime? dob = entity.DateOfBirth;

                if (!contact.LoadByPrimaryKey(txtPatientID.Text))
                    contact = new PatientEmergencyContact();

                if (!dialysis.LoadByPrimaryKey(txtPatientID.Text))
                    dialysis = new PatientDialysis();

                SetEntityValue(entity, contact, dialysis);

                var coll = new RegistrationCollection();

                if (dob != entity.DateOfBirth)
                {
                    coll.Query.Where
                        (
                            coll.Query.PatientID == txtPatientID.Text,
                            coll.Query.IsVoid == false
                        );
                    coll.LoadAll();

                    foreach (Registration reg in coll)
                    {
                        reg.AgeInYear = (byte)Helper.GetAgeInYear(entity.DateOfBirth.Value);
                        reg.AgeInMonth = (byte)Helper.GetAgeInMonth(entity.DateOfBirth.Value);
                        reg.AgeInDay = (byte)Helper.GetAgeInDay(entity.DateOfBirth.Value);
                    }
                }

                SaveEntity(entity, contact, dialysis, coll);
            }

            return true;
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            if (IsNewRecord)
            {
                if (AppSession.Parameter.IsUsingNewDuplicatePatientDataChecking)
                {
                    DataTable patDtb =
                   (new PatientCollection()).DuplicatePatient(txtSSN.Text, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, txtDateOfBirth.SelectedDate, cboSRGenderType.SelectedValue /* rbtSex.SelectedValue*/, ctlAddress.StreetName);
                    if (patDtb.Rows.Count > 0)
                    {
                        var mrno = string.Empty;
                        foreach (DataRow row in patDtb.Rows)
                        {
                            if (mrno == string.Empty)
                                mrno = row["MedicalNo"].ToString();
                            else
                                mrno = mrno + ", " + row["MedicalNo"].ToString();
                        }
                        this.ShowInformationHeader(string.Format("Patient data already exists with MRN: " + mrno + "."));
                        return false;
                    }
                }
                else
                {
                    var patColl = new PatientCollection();
                    patColl.Query.Where(patColl.Query.FirstName == txtFirstName.Text,
                                        patColl.Query.MiddleName == txtMiddleName.Text,
                                        patColl.Query.LastName == txtLastName.Text,
                                        patColl.Query.DateOfBirth.Date() == txtDateOfBirth.SelectedDate.Value.Date,
                                        //patColl.Query.Sex == rbtSex.SelectedValue,
                                        patColl.Query.Sex == cboSRGenderType.SelectedValue,
                                        patColl.Query.MotherName == txtMotherName.Text,
                                        patColl.Query.IsActive == true);
                    patColl.LoadAll();
                    if (patColl.Count > 0)
                    {
                        var mrno = string.Empty;
                        foreach (var pat in patColl)
                        {
                            if (mrno == string.Empty)
                                mrno = pat.MedicalNo;
                            else
                                mrno = mrno + ", " + pat.MedicalNo;
                        }
                        this.ShowInformationHeader(string.Format("Patient data already exists with MRN: " + mrno + "."));
                        return false;
                    }
                }
            }

            var guar = new Guarantor();
            if (guar.LoadByPrimaryKey(cboGuarantorID.SelectedValue))
            {
                if (guar.IsActive == false)
                {
                    ShowInformationHeader("Guarantor is not active. Please select another Guarantor.");
                    return false;
                }

                if (guar.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS &&
                        AppSession.Parameter.IsPatientBpjsNoMandatory &&
                        string.IsNullOrEmpty(txtGuarantorCardNo.Text))
                {
                    ShowInformationHeader("BPJS No required.");
                    return false;
                }
            }

            if (txtMedicalNo.Text != string.Empty)
            {
                if (IsDuplicateNewMedicalNo())
                    return false;
            }

            bool retval = IsNewRecord ? SaveNew() : SaveEdit();

            return retval;
        }

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
                    query.IsActive == true
                );
            query.OrderBy(query.GuarantorName.Ascending);

            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        protected void cboSREthnic_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }

        protected void cboSREthnic_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested((RadComboBox)o, "Ethnic", e.Text);
        }

        protected void cboSREducation_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }

        protected void cboSREducation_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested((RadComboBox)o, "Education", e.Text);
        }

        protected void cboSRNationality_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }

        protected void cboSRNationality_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested((RadComboBox)o, "Nationality", e.Text);
        }

        private DataTable AllergyTable(DataTable table)
        {
            DataTable tbl = new DataTable();

            tbl.Columns.Add("Group", typeof(string));
            tbl.Columns.Add("StandardReferenceID", typeof(string));
            tbl.Columns.Add("ItemID", typeof(string));
            tbl.Columns.Add("ItemName", typeof(string));
            tbl.Columns.Add("DescAndReaction", typeof(string));

            foreach (DataRow row in table.Rows)
            {
                tbl.Rows.Add(WordProcessing((string)row[0]), row[0], row[1], row[2], string.Empty);
            }

            return tbl;
        }

        protected void grdPatientAllergy_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPatientAllergy.DataSource = (DataTable)ViewState["collPatientAllergy" + Request.UserHostName];
            grdPatientAllergy.MasterTableView.GroupsDefaultExpanded = false;
        }

        private string WordProcessing(string value)
        {
            string capital = string.Empty;
            int index = 0;
            foreach (char c in value)
            {
                if (Char.IsUpper(c) && index > 0)
                {
                    capital = c.ToString();
                    break;
                }

                index++;
            }

            if (!capital.Equals(string.Empty))
                return value.Insert(index, " ");
            else
                return value;
        }

        protected void cboZipCode_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var zip = new ZipCode();
            if (zip.LoadByPrimaryKey(e.Value))
            {
                txtTempAddressDistrict.Text = zip.District;
                txtTempAddressCounty.Text = zip.County;
                txtTempAddressCity.Text = zip.City;

                var item = new AppStandardReferenceItem();
                if (item.LoadByPrimaryKey("Province", zip.SRProvince))
                {
                    txtTempAddressState.Text = item.ItemName;
                    txtTempAddressState.ReadOnly = true;
                }
                else
                {
                    txtTempAddressState.Text = string.Empty;
                    txtTempAddressState.ReadOnly = false;
                }
                txtTempAddressDistrict.ReadOnly = true;
                txtTempAddressCounty.ReadOnly = true;
                txtTempAddressCity.ReadOnly = true;
            }
            else
            {
                txtTempAddressDistrict.ReadOnly = false;
                txtTempAddressCounty.ReadOnly = false;
                txtTempAddressCity.ReadOnly = false;
                txtTempAddressState.ReadOnly = false;
            }
        }

        protected void cboZipCode_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new BusinessObject.ZipCodeQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.ZipCode,
                    query.ZipPostalCode,
                    query.District,
                    query.County,
                    query.City
                );
            query.Where
                (
                    query.Or
                        (
                            query.ZipCode.Like(searchTextContain),
                            query.ZipPostalCode.Like(searchTextContain),
                            query.District.Like(searchTextContain),
                            query.County.Like(searchTextContain),
                            query.City.Like(searchTextContain)
                        )
                );

            cboZipCode.DataSource = query.LoadDataTable();
            cboZipCode.DataBind();
        }

        protected void cboZipCode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ZipPostalCode"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ZipCode"].ToString();
        }

        protected void cboEmrContactZipCode_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var zip = new ZipCode();
            if (zip.LoadByPrimaryKey(e.Value))
            {
                txtEmrContactDistrict.Text = zip.District;
                txtEmrContactCounty.Text = zip.County;
                txtEmrContactCity.Text = zip.City;

                var item = new AppStandardReferenceItem();
                txtEmrContactState.Text = item.LoadByPrimaryKey("Province", zip.SRProvince) ? item.ItemName : string.Empty;
            }
        }

        protected void cboEmrContactZipCode_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new BusinessObject.ZipCodeQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.ZipCode,
                    query.ZipPostalCode,
                    query.District,
                    query.County,
                    query.City
                );
            query.Where
                (
                    query.Or
                        (
                            query.ZipCode.Like(searchTextContain),
                            query.ZipPostalCode.Like(searchTextContain),
                            query.District.Like(searchTextContain),
                            query.County.Like(searchTextContain),
                            query.City.Like(searchTextContain)
                        )
                );

            cboEmrContactZipCode.DataSource = query.LoadDataTable();
            cboEmrContactZipCode.DataBind();
        }

        protected void cboEmrContactZipCode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ZipPostalCode"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ZipCode"].ToString();
        }

        protected void cvBirthDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtDateOfBirth.SelectedDate > (new DateTime()).NowAtSqlServer().Date)
                args.IsValid = false;
        }

        protected void grdVisite_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new TransPaymentItemVisiteQuery("a");
            var item = new ItemQuery("b");
            query.Select(
                query,
                item.ItemName
                );
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.Where(
                query.PatientID == txtPatientID.Text,
                query.IsClosed == false
                );
            grdVisite.DataSource = query.LoadDataTable();
        }

        protected void cboSRSalutation_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var appStandardReferenceItem = new AppStandardReferenceItem();
            appStandardReferenceItem.LoadByPrimaryKey(AppEnum.StandardReference.Salutation.ToString(), cboSRSalutation.SelectedValue);
            //rbtSex.SelectedValue = appStandardReferenceItem.ReferenceID;
            cboSRGenderType.SelectedValue = appStandardReferenceItem.ReferenceID;
        }

        protected void cboSRGenderType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var appStandardReferenceItem = new AppStandardReferenceItem();
            appStandardReferenceItem.LoadByPrimaryKey(AppEnum.StandardReference.GenderType.ToString(), cboSRGenderType.SelectedValue);
        }

        protected void txtDateOfBirth_SelectedDateChanged(object sender, EventArgs e)
        {
            txtAgeInYear.Text = Helper.GetAgeInYear(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
            txtAgeInMonth.Text = Helper.GetAgeInMonth(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
            txtAgeInDay.Text = Helper.GetAgeInDay(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
        }

        protected void txtAgeInYear_TextChanged(object sender, EventArgs e)
        {
            txtDateOfBirth.SelectedDate = Helper.GetDateOfBirth(int.Parse(txtAgeInYear.Text), 0);
            txtAgeInMonth.Text = 0.ToString();
            txtAgeInDay.Text = 0.ToString();
        }

        protected void txtAgeInMonth_TextChanged(object sender, EventArgs e)
        {
            txtDateOfBirth.SelectedDate = Helper.GetDateOfBirth(int.Parse(txtAgeInMonth.Text), 1);
            txtAgeInYear.Text = 0.ToString();
            txtAgeInDay.Text = 0.ToString();
        }

        protected void txtAgeInDay_TextChanged(object sender, EventArgs e)
        {
            txtDateOfBirth.SelectedDate = Helper.GetDateOfBirth(int.Parse(txtAgeInDay.Text), 2);
            txtAgeInYear.Text = 0.ToString();
            txtAgeInMonth.Text = 0.ToString();
        }

        protected void txtEmrContactStreetName_TextChanged(object sender, EventArgs e)
        {
            if (txtEmrContactStreetName.Text == "*")
            {
                txtEmrContactStreetName.Text = ctlAddress.StreetName;
                txtEmrContactDistrict.Text = ctlAddress.District;
                txtEmrContactCity.Text = ctlAddress.City;
                txtEmrContactCounty.Text = ctlAddress.County;
                txtEmrContactState.Text = ctlAddress.State;
                if (!string.IsNullOrEmpty(ctlAddress.ZipCode))
                {
                    var contactzip = new ZipCodeQuery();
                    contactzip.Where(contactzip.ZipCode == ctlAddress.ZipCode);
                    cboEmrContactZipCode.DataSource = contactzip.LoadDataTable();
                    cboEmrContactZipCode.DataBind();
                    cboEmrContactZipCode.SelectedValue = ctlAddress.ZipCode;
                }
                txtEmrContactPhoneNo.Text = ctlAddress.PhoneNo;
                txtEmrContactFaxNo.Text = ctlAddress.FaxNo;
                txtEmrContactMobilePhoneNo.Text = ctlAddress.MobilePhoneNo;
                txtEmrContactEmail.Text = ctlAddress.Email;
            }
        }

        protected void grdVisite_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var regs = new RegistrationCollection();
            regs.Query.Where(
                regs.Query.VisiteRegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("PaymentNo").ToString() &&
                regs.Query.IsVoid == false
                );
            regs.Query.Load();

            e.DetailTableView.DataSource = regs;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (sourceControl is RadTextBox && eventArgument == "rebind")
            {
                string NOKK = string.Empty, NIK = string.Empty, NAMALGKP = string.Empty, KABNAME = string.Empty, AGAMA = string.Empty, NAMALGKPAYAH = string.Empty, KECNAME = string.Empty,
                    JENISPKRJN = string.Empty, ALAMAT = string.Empty, NORT = string.Empty, NORW = string.Empty, TMPTLHR = string.Empty, PDDKAKH = string.Empty, STATUSKAWIN = string.Empty,
                    NAMALGKPIBU = string.Empty, PROPNAME = string.Empty, KELNAME = string.Empty, JENISKLMIN = string.Empty, TGLLHR = string.Empty;

                var svc = new Common.Disdukcapil.Service();
                if (AppSession.Parameter.HealthcareInitial == "RSHM")
                {
                    var response = svc.GetPatientByNik(txtSSN.Text);
                    if (response.Content != null && response.Content.Any())
                    {
                        var data = response.Content[0];
                        NOKK = data.NOKK;
                        NIK = data.NIK;
                        NAMALGKP = data.NAMALGKP;
                        KABNAME = data.KABNAME;
                        AGAMA = data.AGAMA;
                        NAMALGKPAYAH = data.NAMALGKPAYAH;
                        KECNAME = data.KECNAME;
                        JENISPKRJN = data.JENISPKRJN;
                        ALAMAT = data.ALAMAT;
                        NORT = data.NORT;
                        NORW = data.NORW;
                        TMPTLHR = data.TMPTLHR;
                        PDDKAKH = data.PDDKAKH;
                        STATUSKAWIN = data.STATUSKAWIN;
                        NAMALGKPIBU = data.NAMALGKPIBU;
                        PROPNAME = data.PROPNAME;
                        KELNAME = data.KELNAME;
                        JENISKLMIN = data.JENISKLMIN;
                        TGLLHR = data.TGLLHR;
                    }
                }
                else if (AppSession.Parameter.HealthcareInitial == "RSTJ")
                {
                    //var response = svc.GetPatientByNikTarakan(txtSSN.Text);
                    var data = svc.GetPatientByNikTarakan(txtSSN.Text);
                    if (string.IsNullOrEmpty(data.STATUS))
                    {
                        NOKK = data.NOKK;
                        NIK = data.NIK;
                        NAMALGKP = data.NAMALGKP;
                        KABNAME = data.NMKAB;
                        //AGAMA = data.AGAMA;
                        //NAMALGKPAYAH = data.NAMALGKPAYAH;
                        KECNAME = data.NMKEC;
                        JENISPKRJN = data.DSCJENISPKRJN;
                        ALAMAT = data.ALAMAT;
                        NORT = data.NORT;
                        NORW = data.NORW;
                        TMPTLHR = data.TMPTLHR;
                        //PDDKAKH = data.PDDKAKH;
                        STATUSKAWIN = data.DSCSTATKWN;
                        //NAMALGKPIBU = data.NAMALGKPIBU;
                        PROPNAME = data.NMPROP;
                        KELNAME = data.NMKEL;
                        JENISKLMIN = data.JENISKLMIN;
                        TGLLHR = data.TGLLHR.Replace("\\", string.Empty).Substring(0, 10);
                    }
                }

                //if (response.Content != null && response.Content.Any())
                {
                    //var data = response.Content[0];
                    txtFamilyRegisterNo.Text = NOKK;
                    txtSSN.Text = NIK;
                    txtFirstName.Text = NAMALGKP;
                    ctlAddress.City = KABNAME;

                    var std = new AppStandardReferenceItem();
                    std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.Religion.ToString(), std.Query.ItemName.ToLower() == AGAMA.ToLower());
                    if (std.Query.Load()) cboSRReligion.SelectedValue = std.ItemID;
                    else
                    {
                        // jika data item id numerik
                        std = new AppStandardReferenceItem();
                        std.Query.Select(std.Query.ItemID.Cast(Dal.DynamicQuery.esCastType.Int32).Max());
                        std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.Religion.ToString());
                        var itemID = std.Query.Load() ? (std.ItemID.ToInt() + 1).ToString() : "0";
                        std = new AppStandardReferenceItem
                        {
                            StandardReferenceID = AppEnum.StandardReference.Religion.ToString(),
                            ItemID = itemID,
                            ItemName = AGAMA,
                            Note = string.Empty,
                            IsUsedBySystem = false,
                            IsActive = true,
                            LastUpdateByUserID = AppSession.UserLogin.UserID,
                            LastUpdateDateTime = DateTime.Now
                        };
                        std.Save();
                        StandardReference.InitializeIncludeSpace(cboSRReligion, AppEnum.StandardReference.Religion);
                        cboSRReligion.SelectedValue = itemID;
                    }

                    txtFatherName.Text = NAMALGKPAYAH;
                    ctlAddress.County = KECNAME;

                    std = new AppStandardReferenceItem();
                    std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.Occupation.ToString(), std.Query.ItemName.ToLower() == JENISPKRJN.ToLower());
                    if (std.Query.Load()) cboSROccupation.SelectedValue = std.ItemID;
                    else
                    {
                        // jika data item id numerik
                        std = new AppStandardReferenceItem();
                        std.Query.Select(std.Query.ItemID.Cast(Dal.DynamicQuery.esCastType.Int32).Max());
                        std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.Occupation.ToString());
                        var itemID = std.Query.Load() ? (std.ItemID.ToInt() + 1).ToString() : "0";
                        std = new AppStandardReferenceItem
                        {
                            StandardReferenceID = AppEnum.StandardReference.Occupation.ToString(),
                            ItemID = itemID,
                            ItemName = JENISPKRJN,
                            Note = string.Empty,
                            IsUsedBySystem = false,
                            IsActive = true,
                            LastUpdateByUserID = AppSession.UserLogin.UserID,
                            LastUpdateDateTime = DateTime.Now
                        };
                        std.Save();
                        StandardReference.InitializeIncludeSpace(cboSROccupation, AppEnum.StandardReference.Occupation);
                        cboSROccupation.SelectedValue = itemID;
                    }

                    ctlAddress.StreetName = string.Format("{0} RT: {1} RW: {2}", ALAMAT, NORT, NORW);
                    txtCityOfBirth.Text = TMPTLHR;

                    std = new AppStandardReferenceItem();
                    std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.Education.ToString(), std.Query.ItemName.ToLower() == PDDKAKH.ToLower());
                    if (std.Query.Load()) cboSREducation.SelectedValue = std.ItemID;
                    else
                    {
                        // jika data item id numerik
                        std = new AppStandardReferenceItem();
                        std.Query.Select(std.Query.ItemID.Cast(Dal.DynamicQuery.esCastType.Int32).Max());
                        std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.Education.ToString());
                        var itemID = std.Query.Load() ? (std.ItemID.ToInt() + 1).ToString() : "0";
                        std = new AppStandardReferenceItem
                        {
                            StandardReferenceID = AppEnum.StandardReference.Education.ToString(),
                            ItemID = itemID,
                            ItemName = PDDKAKH,
                            Note = string.Empty,
                            IsUsedBySystem = false,
                            IsActive = true,
                            LastUpdateByUserID = AppSession.UserLogin.UserID,
                            LastUpdateDateTime = DateTime.Now
                        };
                        std.Save();
                        StandardReference.InitializeIncludeSpace(cboSREducation, AppEnum.StandardReference.Education);
                        cboSREducation.SelectedValue = itemID;
                    }

                    std = new AppStandardReferenceItem();
                    std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.MaritalStatus.ToString(), std.Query.ItemName.ToLower() == STATUSKAWIN.ToLower());
                    if (std.Query.Load()) cboSRMaritalStatus.SelectedValue = std.ItemID;
                    else
                    {
                        // jika data item id numerik
                        std = new AppStandardReferenceItem();
                        std.Query.Select(std.Query.ItemID.Cast(Dal.DynamicQuery.esCastType.Int32).Max());
                        std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.MaritalStatus.ToString());
                        var itemID = std.Query.Load() ? (std.ItemID.ToInt() + 1).ToString() : "0";
                        std = new AppStandardReferenceItem
                        {
                            StandardReferenceID = AppEnum.StandardReference.MaritalStatus.ToString(),
                            ItemID = itemID,
                            ItemName = STATUSKAWIN,
                            Note = string.Empty,
                            IsUsedBySystem = false,
                            IsActive = true,
                            LastUpdateByUserID = AppSession.UserLogin.UserID,
                            LastUpdateDateTime = DateTime.Now
                        };
                        std.Save();
                        StandardReference.InitializeIncludeSpace(cboSRMaritalStatus, AppEnum.StandardReference.MaritalStatus);
                        cboSRMaritalStatus.SelectedValue = itemID;
                    }

                    txtMotherName.Text = NAMALGKPIBU;
                    ctlAddress.State = PROPNAME;
                    ctlAddress.District = KELNAME;
                    //rbtSex.SelectedValue = JENISKLMIN == "Laki-Laki" ? "M" : "F";
                    cboSRGenderType.SelectedValue = JENISKLMIN == "Laki-Laki" ? "M" : "F";

                    var format = string.Empty;
                    //if (AppSession.Parameter.HealthcareInitial == "RSHM") format = "yyyy-MM-dd";
                    //else if (AppSession.Parameter.HealthcareInitial == "RSTJ") format = "dd/MM/yyyy";
                    //DateTime.TryParseExact(TGLLHR.Replace("\\", string.Empty), format, null, System.Globalization.DateTimeStyles.None, out DateTime parsed);
                    DateTime parsed = new DateTime();
                    if (AppSession.Parameter.HealthcareInitial == "RSHM")
                    {
                        format = "yyyy-MM-dd";
                        if (!DateTime.TryParseExact(TGLLHR.Replace("\\", string.Empty), format, null,
                            System.Globalization.DateTimeStyles.None, out parsed))
                        {
                            parsed = new DateTime();
                        }
                    }
                    else if (AppSession.Parameter.HealthcareInitial == "RSTJ")
                    {
                        //format = "dd/MM/yyyy";
                        //if (!DateTime.TryParseExact(TGLLHR.Replace("\\", string.Empty), format, null,
                        //    System.Globalization.DateTimeStyles.None, out parsed))
                        //{
                        format = "yyyy-MM-dd";
                        if (!DateTime.TryParseExact(TGLLHR.Replace("\\", string.Empty).Substring(0, 10), format, null,
                            System.Globalization.DateTimeStyles.None, out parsed))
                        {
                            parsed = new DateTime();
                        }
                        //}
                    }
                    txtDateOfBirth.SelectedDate = parsed;
                    txtAgeInYear.Text = Helper.GetAgeInYear(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
                    txtAgeInMonth.Text = Helper.GetAgeInMonth(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
                    txtAgeInDay.Text = Helper.GetAgeInDay(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();

                    var zipQuery = new ZipCodeQuery("a");
                    var stdQuery = new AppStandardReferenceItemQuery("b");
                    zipQuery.InnerJoin(stdQuery).On(zipQuery.SRProvince == stdQuery.ItemID && stdQuery.StandardReferenceID == AppEnum.StandardReference.Province.ToString());
                    zipQuery.Where(zipQuery.District == ctlAddress.District, zipQuery.County == ctlAddress.County, stdQuery.ItemName == ctlAddress.State);
                    zipQuery.es.Top = 1;

                    var zip = zipQuery.LoadDataTable();
                    if (zip.AsEnumerable().Any())
                    {
                        ctlAddress.ZipCodeCombo.DataSource = zip;
                        ctlAddress.ZipCodeCombo.DataBind();
                        ctlAddress.ZipCodeCombo.SelectedValue = zip.Rows[0][0].ToString();
                    }
                    else
                    {
                        ctlAddress.ZipCodeCombo.SelectedValue = string.Empty;
                        ctlAddress.ZipCodeCombo.Text = string.Empty;
                    }

                    chkSyncDukcapil.Checked = true;
                }
            }
        }

    
    }
}