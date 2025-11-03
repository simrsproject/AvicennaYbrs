using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class DukcapilDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            switch (Request.QueryString["type"])
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
            }

            if (!IsPostBack)
            {
                cboNoRekamMedis.Enabled = Request.QueryString["info"] == "0";
                ButtonOk.Text = Request.QueryString["info"] == "1" ? "Load" : ButtonOk.Text;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var svc = new Common.Disdukcapil.Service();
                if (AppSession.Parameter.HealthcareInitial == "RSHM")
                {
                    var response = svc.GetPatientByNik(Request.QueryString["nik"]);
                    if (response.Content != null && response.Content.Any())
                    {
                        var content = response.Content[0];
                        lblNIK.Text = content.NIK;
                        lblNoKK.Text = content.NOKK;
                        lblNamaLengkap.Text = content.NAMALGKP;
                        lblJenisKelamin.Text = content.JENISKLMIN;
                        lblTempatLahir.Text = string.Format("{0}, {1}", content.TMPTLHR, content.TGLLHR);
                        lblAlamat.Text = string.Format("{0} RT: {1} RW: {2} KEL: {3} KEC: {4} KAB: {5} PROP: {6}", content.ALAMAT, content.NORT, content.NORW, content.KELNAME, content.KECNAME, content.KABNAME, content.PROPNAME); ;
                        lblNamaAyah.Text = content.NAMALGKPAYAH;
                        lblNamaIbu.Text = content.NAMALGKPIBU;
                        lblAgama.Text = content.AGAMA;
                        lblStatusKawin.Text = content.STATUSKAWIN;
                        lblPendidikanAkhir.Text = content.PDDKAKH;
                        lblPekerjaan.Text = content.JENISPKRJN;

                        ViewState["content"] = content;
                    }
                    else ViewState["content"] = null;
                }
                else if (AppSession.Parameter.HealthcareInitial == "RSTJ")
                {
                    var response = svc.GetPatientByNikTarakan(Request.QueryString["nik"]);
                    var content = response;
                    if (string.IsNullOrEmpty(content.STATUS))
                    {
                        lblNIK.Text = content.NIK;
                        lblNoKK.Text = content.NOKK;
                        lblNamaLengkap.Text = content.NAMALGKP;
                        lblJenisKelamin.Text = content.JENISKLMIN == "1" ? "Laki-Laki" : "Perempuan";
                        lblTempatLahir.Text = string.Format("{0}, {1}", content.TMPTLHR, content.TGLLHR.Replace("\\", string.Empty).Substring(0, 10));
                        lblAlamat.Text = string.Format("{0} RT: {1} RW: {2} KEL: {3} KEC: {4} KAB: {5} PROP: {6}", content.ALAMAT, content.NORT, content.NORW, content.NMKEL, content.NMKEC, content.NMKAB, content.NMPROP); ;
                        //lblNamaAyah.Text = content.NAMALGKPAYAH;
                        //lblNamaIbu.Text = content.NAMALGKPIBU;
                        //lblAgama.Text = content.AGAMA;
                        lblStatusKawin.Text = content.DSCSTATKWN;
                        //lblPendidikanAkhir.Text = content.PDDKAKH;
                        lblPekerjaan.Text = content.DSCJENISPKRJN;

                        ViewState["content"] = content;

                        var patients = new PatientCollection();
                        patients.Query.Where(patients.Query.Ssn == lblNIK.Text);
                        patients.Query.Load();

                        foreach (var data in patients)
                        {
                            cboNoRekamMedis.Items.Add(new RadComboBoxItem()
                            {
                                Text = data.MedicalNo + " - " + data.PatientName,
                                Value = data.MedicalNo + "|" + data.PatientID
                            });
                        }
                        if (patients.Count == 1) cboNoRekamMedis.SelectedIndex = 0;
                    }
                    else ViewState["content"] = null;
                }
                else ViewState["content"] = null;

                if (string.IsNullOrWhiteSpace(lblNIK.Text)) ButtonOk.Enabled = false;
            }
        }

        protected void cboNoMRSep_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((Patient)e.Item.DataItem).MedicalNo + " - " + ((Patient)e.Item.DataItem).PatientName;
            e.Item.Value = ((Patient)e.Item.DataItem).MedicalNo;
        }

        public override bool OnButtonOkClicked()
        {
            if (Helper.IsDukcapilIntegration)
            {
                if (Request.QueryString["info"] == "0")
                {
                    using (var trans = new esTransactionScope())
                    {
                        string NOKK = string.Empty, NIK = string.Empty, NAMALGKP = string.Empty, KABNAME = string.Empty, AGAMA = string.Empty, NAMALGKPAYAH = string.Empty, KECNAME = string.Empty,
                            JENISPKRJN = string.Empty, ALAMAT = string.Empty, NORT = string.Empty, NORW = string.Empty, TMPTLHR = string.Empty, PDDKAKH = string.Empty, STATUSKAWIN = string.Empty,
                            NAMALGKPIBU = string.Empty, PROPNAME = string.Empty, KELNAME = string.Empty, JENISKLMIN = string.Empty, TGLLHR = string.Empty;
                        if (AppSession.Parameter.HealthcareInitial == "RSHM")
                        {
                            var data = ViewState["content"] as Common.Disdukcapil.Response.Content;
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
                        else if (AppSession.Parameter.HealthcareInitial == "RSTJ")
                        {
                            var data = ViewState["content"] as Common.Disdukcapil.ResponseTarakan.Root;
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
                            JENISKLMIN = data.JENISKLMIN == "1" ? "Laki-Laki" : "Perempuan";
                            TGLLHR = data.TGLLHR.Replace("\\", string.Empty);
                        }

                        //var data = ViewState["content"] as Common.Disdukcapil.Response.Content;
                        var patient = new Patient();
                        if (!patient.LoadByMedicalNo(string.IsNullOrWhiteSpace(cboNoRekamMedis.SelectedValue) ? string.Empty : cboNoRekamMedis.SelectedValue.Split('|')[0]))
                        {
                            patient = new Patient();

                            var autoNumberLastPID = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PatientID);
                            patient.PatientID = autoNumberLastPID.LastCompleteNumber;
                            autoNumberLastPID.Save();
                        }

                        patient.FamilyRegisterNo = NOKK;
                        patient.Ssn = NIK;
                        patient.FirstName = NAMALGKP;
                        patient.City = KABNAME;

                        var std = new AppStandardReferenceItem();
                        std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.Religion.ToString(), std.Query.ItemName.ToLower() == AGAMA.ToLower());
                        if (std.Query.Load()) patient.SRReligion = std.ItemID;
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
                            patient.SRReligion = itemID;
                        }

                        patient.FatherName = NAMALGKPAYAH;
                        patient.County = KECNAME;

                        std = new AppStandardReferenceItem();
                        std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.Occupation.ToString(), std.Query.ItemName.ToLower() == JENISPKRJN.ToLower());
                        if (std.Query.Load()) patient.SROccupation = std.ItemID;
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
                            patient.SROccupation = itemID;
                        }

                        patient.StreetName = string.Format("{0} RT: {1} RW: {2}", ALAMAT, NORT, NORW);
                        patient.CityOfBirth = TMPTLHR;

                        std = new AppStandardReferenceItem();
                        std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.Education.ToString(), std.Query.ItemName.ToLower() == PDDKAKH.ToLower());
                        if (std.Query.Load()) patient.SREducation = std.ItemID;
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
                            patient.SREducation = itemID;
                        }

                        std = new AppStandardReferenceItem();
                        std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.MaritalStatus.ToString(), std.Query.ItemName.ToLower() == STATUSKAWIN.ToLower());
                        if (std.Query.Load()) patient.SRMaritalStatus = std.ItemID;
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
                            patient.SRMaritalStatus = itemID;
                        }

                        patient.MotherName = NAMALGKPIBU;
                        patient.State = PROPNAME;
                        patient.District = KELNAME;
                        patient.Sex = JENISKLMIN == "Laki-Laki" ? "M" : "F";

                        var format = string.Empty;
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
                        patient.DateOfBirth = parsed;
                        patient.LastUpdateDateTime = DateTime.Now;
                        patient.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        patient.IsSyncWithDukcapil = true;

                        patient.MedicalNo = cboNoRekamMedis.SelectedValue.Split('|')[0];

                        var zipQuery = new ZipCodeQuery("a");
                        var stdQuery = new AppStandardReferenceItemQuery("b");
                        zipQuery.es.Top = 1;
                        zipQuery.InnerJoin(stdQuery).On(zipQuery.SRProvince == stdQuery.ItemID && stdQuery.StandardReferenceID == AppEnum.StandardReference.Province.ToString());
                        zipQuery.Where(zipQuery.District == patient.District, zipQuery.County == patient.County, stdQuery.ItemName == patient.State);

                        var zip = new ZipCode();
                        if (zip.Load(zipQuery)) patient.ZipCode = zip.ZipCode;

                        patient.SRSalutation = string.Empty;
                        patient.MiddleName = string.Empty;
                        patient.LastName = string.Empty;
                        patient.ParentSpouseName = string.Empty;
                        patient.SRBloodType = string.Empty;
                        patient.BloodRhesus = string.Empty;
                        patient.SREthnic = string.Empty;
                        patient.SRNationality = string.Empty;
                        patient.SRTitle = string.Empty;
                        patient.SRPatientCategory = string.Empty;
                        patient.SRMedicalFileBin = string.Empty;
                        patient.SRMedicalFileStatus = string.Empty;
                        patient.GuarantorID = string.Empty;
                        patient.Company = string.Empty;
                        patient.PhoneNo = string.Empty;
                        patient.FaxNo = string.Empty;
                        patient.Email = string.Empty;
                        patient.MobilePhoneNo = string.Empty;
                        patient.TempAddressStreetName = string.Empty;
                        patient.TempAddressDistrict = string.Empty;
                        patient.TempAddressCity = string.Empty;
                        patient.TempAddressCounty = string.Empty;
                        patient.TempAddressState = string.Empty;
                        patient.TempAddressZipCode = string.Empty;
                        patient.TempAddressPhoneNo = string.Empty;
                        patient.NumberOfVisit = 0;
                        patient.OldMedicalNo = string.Empty;
                        patient.AccountNo = string.Empty;
                        patient.PictureFileName = string.Empty;
                        patient.IsDonor = false;
                        patient.NumberOfDonor = 0;
                        patient.IsBlackList = false;
                        patient.IsAlive = true;
                        patient.IsActive = true;
                        patient.Notes = string.Empty;
                        patient.DiagnosticNo = string.Empty;
                        patient.MemberID = string.Empty;
                        patient.PackageBalance = 0;
                        patient.HealthcareID = AppSession.Parameter.HealthcareID;
                        patient.SRInformationFrom = string.Empty;
                        patient.SRPatienRelation = string.Empty;
                        patient.EmployeeNumber = string.Empty;
                        patient.SREmployeeRelationship = string.Empty;
                        patient.GuarantorCardNo = string.Empty;
                        patient.IsNonPatient = false;
                        patient.ParentSpouseAge = 0;
                        patient.SRParentSpouseOccupation = string.Empty;
                        patient.ParentSpouseOccupationDesc = string.Empty;
                        patient.SRMotherOccupation = string.Empty;
                        patient.MotherOccupationDesc = string.Empty;
                        patient.MotherName = string.Empty;
                        patient.MotherAge = 0;
                        patient.IsNotPaidOff = false;
                        patient.ParentSpouseMedicalNo = string.Empty;
                        patient.MotherMedicalNo = string.Empty;
                        patient.CompanyAddress = string.Empty;
                        patient.CreatedByUserID = AppSession.UserLogin.UserID;
                        patient.CreatedDateTime = DateTime.Now;
                        patient.SRRelationshipQuality = string.Empty;
                        patient.SRResidentialHome = string.Empty;
                        patient.IsStoredToLokadok = false;
                        patient.FatherName = string.Empty;
                        patient.FatherAge = 0;
                        patient.FatherMedicalNo = string.Empty;
                        patient.SRFatherOccupation = string.Empty;
                        patient.FatherOccupationDesc = string.Empty;
                        patient.DeathCertificateNo = string.Empty;
                        patient.EmployeeNo = string.Empty;
                        patient.EmployeeJobTitleName = string.Empty;
                        patient.EmployeeJobDepartementName = string.Empty;
                        patient.ValuesOfTrust = string.Empty;

                        var contact = new PatientEmergencyContact
                        {
                            PatientID = patient.PatientID,
                            ContactName = string.Empty,
                            SRRelationship = string.Empty,
                            StreetName = string.Empty,
                            District = string.Empty,
                            City = string.Empty,
                            County = string.Empty,
                            State = string.Empty,
                            FaxNo = string.Empty,
                            Email = string.Empty,
                            PhoneNo = string.Empty,
                            MobilePhoneNo = string.Empty,
                            Notes = string.Empty,
                            LastUpdateDateTime = DateTime.Now,
                            LastUpdateByUserID = AppSession.UserLogin.UserID,
                            SROccupation = string.Empty,
                            Ssn = string.Empty
                        };

                        patient.Save();
                        contact.Save();

                        ViewState["patientID"] = patient.PatientID;

                        trans.Complete();
                    }
                }
            }
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            if (Request.QueryString["info"] == "0")
            {
                if (ViewState["patientID"] == null) return string.Empty;
                return string.Format("oWnd.argument.mode = 'new|{0}'", ViewState["patientID"].ToString());
            }
            else return "oWnd.argument.mode = 'rebind'";
        }
    }
}