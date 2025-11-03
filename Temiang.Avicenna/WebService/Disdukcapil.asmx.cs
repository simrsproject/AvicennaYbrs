using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for Disdukcapil
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Disdukcapil : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Insert(string nik, string namaLengkap, string jenisKelamin, string tempatLahir, string tanggalLahir, string agama, string statusKawin, string jenisPekerjaan, string namaProvinsi,
            string namaKabupaten, string namaKecamatan, string namaKelurahan, string alamat, string nomorRt, string nomorRw, string desa, string kodePos, string golonganDarah, string kewarganegaraan,
            string foto)
        {
            var json = new JsonRetWS();

            if (string.IsNullOrWhiteSpace(nik)) Context.Response.Write(json.JSonRetFormatted("Nik is empty", false, string.Empty));

            var _autoNumberLastPID = new AppAutoNumberLast();

            var patient = new Patient();
            patient.Query.Where(patient.Query.Ssn == nik);
            if (!patient.Query.Load())
            {
                patient = new Patient();
                _autoNumberLastPID = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PatientID);
                patient.PatientID = _autoNumberLastPID.LastCompleteNumber;
                patient.Ssn = nik;
                patient.GuarantorID = AppParameter.GetParameterValue(AppParameter.ParameterItem.SelfGuarantorID);
            }

            patient.SRSalutation = string.Empty;
            patient.FirstName = namaLengkap;
            patient.MiddleName = string.Empty;
            patient.LastName = string.Empty;
            patient.Sex = jenisKelamin == "LAKI-LAKI" ? "M" : "F";
            patient.CityOfBirth = tempatLahir;

            string format = "d-M-yyyy";
            if (!DateTime.TryParseExact(tanggalLahir, format, null, System.Globalization.DateTimeStyles.None, out var parsed)) Context.Response.Write(json.JSonRetFormatted($"Invalid tanggalLahir format : {tanggalLahir}", false, string.Empty));
            patient.DateOfBirth = parsed;

            var asri = new AppStandardReferenceItem();
            asri.Query.Where(asri.Query.StandardReferenceID == AppEnum.StandardReference.BloodType, asri.Query.ItemName == golonganDarah);
            patient.SRBloodType = asri.Query.Load() ? asri.ItemID : string.Empty;

            patient.BloodRhesus = string.Empty;
            patient.SREthnic = string.Empty;
            patient.SREducation = string.Empty;

            asri = new AppStandardReferenceItem();
            asri.Query.Where(asri.Query.StandardReferenceID == AppEnum.StandardReference.MaritalStatus, asri.Query.ItemName == statusKawin);
            patient.SRMaritalStatus = asri.Query.Load() ? asri.ItemID : string.Empty;

            patient.SRNationality = kewarganegaraan == "WNI" ? "01" : string.Empty;

            asri = new AppStandardReferenceItem();
            asri.Query.Where(asri.Query.StandardReferenceID == AppEnum.StandardReference.Occupation, asri.Query.ItemName == jenisPekerjaan);
            patient.SROccupation = asri.Query.Load() ? asri.ItemID : string.Empty;

            asri = new AppStandardReferenceItem();
            asri.Query.Where(asri.Query.StandardReferenceID == AppEnum.StandardReference.Religion, asri.Query.ItemName == agama);
            patient.SRReligion = asri.Query.Load() ? asri.ItemID : string.Empty;

            patient.SRTitle = string.Empty;
            patient.SRPatientCategory = string.Empty;
            patient.SRMedicalFileBin = string.Empty;
            patient.SRMedicalFileStatus = string.Empty;
            patient.Company = string.Empty;

            patient.StreetName = $"{alamat} RT : {nomorRt} RW : {nomorRw} {desa}";
            patient.District = namaKelurahan;
            patient.City = namaKabupaten;
            patient.County = namaKecamatan;
            patient.State = namaProvinsi;
            patient.ZipCode = kodePos;

            patient.PhoneNo = string.Empty;
            patient.FaxNo = string.Empty;
            patient.Email = string.Empty;
            patient.MobilePhoneNo = string.Empty;
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

            patient.LastUpdateDateTime = DateTime.Now;
            patient.LastUpdateByUserID = "WEBSERVICE";

            var image = new PatientImage();
            if (!image.LoadByPrimaryKey(patient.PatientID))
            {
                image = new PatientImage();
                image.PatientID = patient.PatientID;
            }
            image.Photo = Convert.FromBase64String(foto);
            image.LastUpdateDateTime = DateTime.Now;
            image.LastUpdateByUserID = "WEBSERVICE";

            using (var trans = new esTransactionScope())
            {
                _autoNumberLastPID.Save();
                patient.Save();
                image.Save();

                trans.Complete();
            }

            Context.Response.Write(json.JSonRetFormatted("Success", true, string.Empty));
        }
    }
}
