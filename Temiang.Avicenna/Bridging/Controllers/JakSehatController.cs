using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using Newtonsoft.Json;
using Temiang.Dal.Interfaces;
using System.Text;
using Temiang.Avicenna.Bridging.Antrean.ParameterClass;

namespace Temiang.Avicenna.Bridging.Controllers
{
    public class JakSehatController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("jaksehat/test")]
        public HttpResponseMessage Test()
        {
            return Request.CreateResponse(HttpStatusCode.Accepted, new
            {
                metadata = new
                {
                    code = (int)HttpStatusCode.Accepted,
                    message = "OK"
                }
            });
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("jaksehat/auth")]
        public HttpResponseMessage GetToken(TokenParam parVal)
        {
            string tokenSource;
            if (AuthenticationMiddleware.ValidateUser(parVal.Username, parVal.Password))
            {
                tokenSource = string.Format("{0}:{1}:{2}:{3}:{4}", AuthenticationMiddleware.GetUnixTimeStamp(0), parVal.Username, parVal.Password, AuthenticationMiddleware.GetUnixTimeStamp(10), AuthenticationMiddleware.GetXSignature(parVal.Username, parVal.Password));
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    metadata = new
                    {
                        code = HttpStatusCode.OK,
                        message = "Sukses"
                    },
                    response = new
                    {
                        token = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(tokenSource))
                    }
                });
            }

            return Request.CreateResponse(HttpStatusCode.Unauthorized, new
            {
                metadata = new
                {
                    code = (int)HttpStatusCode.Unauthorized,
                    message = "Gagal"
                }
            });
        }

        [JakSehatCustomAuthorization]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("jaksehat/patient/find/{type}/{number}")]
        public HttpResponseMessage FindPatient(string type, string number)
        {
            var patient = new Patient();
            if (type == "nik") patient.Query.Where(patient.Query.Ssn == number);
            else if (type == "nomr") patient.Query.Where(patient.Query.MedicalNo == number);
            else patient.Query.Where(patient.Query.GuarantorCardNo == number);
            if (!patient.Query.Load())
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metaData = new
                    {
                        code = (int)HttpStatusCode.NotFound,
                        message = "data tidak ditemukan"
                    }
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                metaData = new
                {
                    code = HttpStatusCode.OK,
                    message = "OK"
                },
                data = new
                {
                    noMR = patient.MedicalNo,
                    name = patient.PatientName,
                    gender = patient.Sex == "M" ? "L" : "P",
                    birthDate = patient.DateOfBirth.Value.ToString("yyyy-MM-dd"),
                    nik = patient.Ssn,
                    noJKN = patient.GuarantorCardNo,
                    phone = patient.MobilePhoneNo
                }
            });
        }

        [JakSehatCustomAuthorization]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("jaksehat/patient/create")]
        public HttpResponseMessage CreatePatient()
        {
            var form = HttpContext.Current.Request.Form;

            string format = "yyyy-MM-dd";
            var parsed = new DateTime();

            var validationList = new List<string>()
            {
                "nik",
                //"kkNumber",
                "name",
                "gender",
                //"placeOfBirth",
                "dateOfBirth",
                //"religion",
                "address",
                "provinceCode",
                //"provinceName",
                "regencyCode",
                //"regencyName",
                "districtCode",
                //"districtName",
                "villageCode",
                //"villageName",
                "rw",
                "rt",
                "insurrance",
                "insurranceNumber",
                "email",
                "occupation",
                "education",
                "tribe",
                "motherName",
                "maritalStatus",
                "patientCompanion[relationship]",
                "patientCompanion[name]",
                "patientCompanion[gender]",
                "patientCompanion[dateOfBirth]",
                "patientCompanion[address]",
                "disabilities"
            };

            //return Request.CreateResponse(HttpStatusCode.Created, new
            //{
            //    metaData = new
            //    {
            //        code = HttpStatusCode.OK,
            //        message = "OK"
            //    }
            //});

            foreach (var validationItem in validationList)
            {
                switch (validationItem)
                {
                    case "dateOfBirth":
                    case "patientCompanion[dateOfBirth]":
                        if (string.IsNullOrWhiteSpace(form[validationItem].Trim()) || !DateTime.TryParseExact(form[validationItem].Trim(), format, null, System.Globalization.DateTimeStyles.None, out _))
                        {
                            return Request.CreateResponse((HttpStatusCode)422, new
                            {
                                metaData = new
                                {
                                    code = 422,
                                    message = $"{validationItem} tidak boleh kosong atau salah format"
                                }
                            });
                        }
                        break;
                    case "insurranceNumber":
                        if (form["insurrance"].Trim() == "0001" && string.IsNullOrWhiteSpace(form[validationItem].Trim()))
                        {
                            return Request.CreateResponse((HttpStatusCode)422, new
                            {
                                metaData = new
                                {
                                    code = 422,
                                    message = $"{validationItem} tidak boleh kosong"
                                }
                            });
                        }
                        break;
                    default:
                        if (string.IsNullOrWhiteSpace(form[validationItem].Trim()))
                        {
                            return Request.CreateResponse((HttpStatusCode)422, new
                            {
                                metaData = new
                                {
                                    code = 422,
                                    message = $"{validationItem} tidak boleh kosong"
                                }
                            });
                        }
                        break;
                }

            }

            var pasien = new Patient();
            pasien.Query.Where(pasien.Query.Ssn == form["nik"].Trim());
            if (pasien.Query.Load())
            {
                return Request.CreateResponse((HttpStatusCode)422, new
                {
                    metaData = new
                    {
                        code = 422,
                        message = $"nik sudah terdaftar"
                    }
                });
            }

            pasien = new Patient();
            var contact = new PatientEmergencyContact();

            var autoNumberLastPID = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PatientID);
            pasien.PatientID = autoNumberLastPID.LastCompleteNumber;
            var autoNumberLastMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);
            pasien.MedicalNo = autoNumberLastMRN.LastCompleteNumber;
            pasien.Ssn = form["nik"].Trim();
            pasien.SRSalutation = string.Empty;
            pasien.FirstName = form["name"].Trim();
            pasien.MiddleName = string.Empty;
            pasien.LastName = string.Empty;
            pasien.ParentSpouseName = form["patientCompanion[name]"].Trim();
            pasien.CityOfBirth = string.Empty;

            pasien.DateOfBirth = DateTime.ParseExact(form["dateOfBirth"].Trim(), format, null, System.Globalization.DateTimeStyles.None);

            pasien.Sex = form["gender"].Trim() == "L" ? "M" : "F";
            pasien.SRBloodType = string.Empty;
            pasien.BloodRhesus = string.Empty;

            var std = new AppStandardReferenceItem();
            std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.Ethnic.ToString(), std.Query.ItemName.Trim().ToLower() == form["tribe"].Trim().ToLower(), std.Query.IsActive == true);
            if (!std.Query.Load())
            {
                std = new AppStandardReferenceItem();
                std.StandardReferenceID = AppEnum.StandardReference.Ethnic.ToString();
                std.ItemID = form["tribe"].Trim().Replace(" ", string.Empty);
                std.Note = string.Empty;
                std.IsUsedBySystem = false;
                std.IsActive = true;
                std.LastUpdateDateTime = DateTime.Now;
                std.LastUpdateByUserID = "JAKSEHAT";
                std.Save();
            }
            pasien.SREthnic = std.ItemID;

            std = new AppStandardReferenceItem();
            std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.Education.ToString(), std.Query.ItemName.Trim().ToLower() == form["education"].Trim().ToLower(), std.Query.IsActive == true);
            if (!std.Query.Load())
            {
                std = new AppStandardReferenceItem();
                std.StandardReferenceID = AppEnum.StandardReference.Education.ToString();
                std.ItemID = form["education"].Trim().Replace(" ", string.Empty);
                std.Note = string.Empty;
                std.IsUsedBySystem = false;
                std.IsActive = true;
                std.LastUpdateDateTime = DateTime.Now;
                std.LastUpdateByUserID = "JAKSEHAT";
                std.Save();
            }
            pasien.SREducation = std.ItemID;

            std = new AppStandardReferenceItem();
            std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.MaritalStatus.ToString(), std.Query.ItemName.Trim().ToLower() == form["maritalStatus"].Trim().ToLower(), std.Query.IsActive == true);
            if (!std.Query.Load())
            {
                std = new AppStandardReferenceItem();
                std.StandardReferenceID = AppEnum.StandardReference.MaritalStatus.ToString();
                std.ItemID = form["maritalStatus"].Trim().Replace(" ", string.Empty);
                std.Note = string.Empty;
                std.IsUsedBySystem = false;
                std.IsActive = true;
                std.LastUpdateDateTime = DateTime.Now;
                std.LastUpdateByUserID = "JAKSEHAT";
                std.Save();
            }
            pasien.SRMaritalStatus = std.ItemID;

            pasien.SRNationality = string.Empty;

            std = new AppStandardReferenceItem();
            std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.Occupation.ToString(), std.Query.ItemName.Trim().ToLower() == form["occupation"].Trim().ToLower(), std.Query.IsActive == true);
            if (!std.Query.Load())
            {
                std = new AppStandardReferenceItem();
                std.StandardReferenceID = AppEnum.StandardReference.Occupation.ToString();
                std.ItemID = form["occupation"].Trim().Replace(" ", string.Empty);
                std.Note = string.Empty;
                std.IsUsedBySystem = false;
                std.IsActive = true;
                std.LastUpdateDateTime = DateTime.Now;
                std.LastUpdateByUserID = "JAKSEHAT";
                std.Save();
            }
            pasien.SROccupation = std.ItemID;

            pasien.SRTitle = string.Empty;
            pasien.SRPatientCategory = string.Empty;

            std = new AppStandardReferenceItem();
            std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.Religion.ToString(), std.Query.ItemName.Trim().ToLower() == form["religion"].Trim().ToLower(), std.Query.IsActive == true);
            if (!std.Query.Load())
            {
                std = new AppStandardReferenceItem();
                std.StandardReferenceID = AppEnum.StandardReference.Religion.ToString();
                std.ItemID = form["religion"].Trim().Replace(" ", string.Empty);
                std.Note = string.Empty;
                std.IsUsedBySystem = false;
                std.IsActive = true;
                std.LastUpdateDateTime = DateTime.Now;
                std.LastUpdateByUserID = "JAKSEHAT";
                std.Save();
            }
            pasien.SRReligion = std.ItemID;

            pasien.SRMedicalFileBin = string.Empty;
            pasien.SRMedicalFileStatus = string.Empty;
            pasien.GuarantorID = form["insurrance"].Trim() == "0000" ? AppSession.Parameter.SelfGuarantor : AppSession.Parameter.GuarantorAskesID[0];
            pasien.Company = string.Empty;
            pasien.StreetName = $"{form["address"].Trim()} RT : {form["rt"].Trim()} RW : {form["rw"].Trim()}";

            std = new AppStandardReferenceItem();
            std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.District.ToString(), std.Query.ItemID.ToLower() == form["districtCode"].Trim().ToLower());
            pasien.District = std.Query.Load() ? std.ItemName : string.Empty;

            std = new AppStandardReferenceItem();
            std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.City.ToString(), std.Query.ItemID.ToLower() == form["regencyCode"].Trim().ToLower());
            pasien.City = std.Query.Load() ? std.ItemName : string.Empty;

            std = new AppStandardReferenceItem();
            std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.County.ToString(), std.Query.ItemID.ToLower() == form["villageCode"].Trim().ToLower());
            pasien.County = std.Query.Load() ? std.ItemName : string.Empty;

            std = new AppStandardReferenceItem();
            std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.Province.ToString(), std.Query.ItemID.ToLower() == form["provinceCode"].Trim().ToLower());
            pasien.State = std.Query.Load() ? std.ItemName : string.Empty;

            pasien.str.ZipCode = form["postalCode"].ToString();
            pasien.PhoneNo = string.Empty;
            pasien.FaxNo = string.Empty;
            pasien.Email = string.Empty;
            pasien.MobilePhoneNo = form["phoneNumber"].ToString();
            pasien.TempAddressStreetName = string.Empty;
            pasien.TempAddressDistrict = string.Empty;
            pasien.TempAddressCity = string.Empty;
            pasien.TempAddressCounty = string.Empty;
            pasien.TempAddressState = string.Empty;
            pasien.str.TempAddressZipCode = string.Empty;
            pasien.TempAddressPhoneNo = string.Empty;
            pasien.str.LastVisitDate = string.Empty;
            pasien.NumberOfVisit = 0;
            pasien.OldMedicalNo = string.Empty;
            pasien.AccountNo = string.Empty;
            pasien.PictureFileName = string.Empty;
            pasien.IsDonor = false;
            pasien.NumberOfDonor = 0;
            pasien.str.LastDonorDate = string.Empty;
            pasien.IsBlackList = false;
            pasien.IsAlive = true;
            pasien.IsActive = true;
            pasien.Notes = string.Empty;
            pasien.DiagnosticNo = string.Empty;
            pasien.MemberID = string.Empty;
            pasien.LastUpdateDateTime = DateTime.Now;
            pasien.LastUpdateByUserID = "JAKSEHAT";
            pasien.PackageBalance = 0;
            pasien.HealthcareID = AppSession.Parameter.HealthcareID;
            pasien.str.ResponTime = string.Empty;
            pasien.SRInformationFrom = string.Empty;
            pasien.SRPatienRelation = string.Empty;
            pasien.PersonID = 0;
            pasien.EmployeeNumber = string.Empty;
            pasien.SREmployeeRelationship = string.Empty;
            pasien.GuarantorCardNo = form["insurranceNumber"].Trim();
            pasien.IsNonPatient = false;
            pasien.ParentSpouseAge = 0;
            pasien.SRParentSpouseOccupation = string.Empty;
            pasien.ParentSpouseOccupationDesc = string.Empty;
            pasien.SRMotherOccupation = string.Empty;
            pasien.MotherOccupationDesc = string.Empty;
            pasien.MotherName = form["motherName"].Trim();
            pasien.MotherAge = 0;
            pasien.IsNotPaidOff = false;
            pasien.ParentSpouseMedicalNo = string.Empty;
            pasien.MotherMedicalNo = string.Empty;
            pasien.CompanyAddress = string.Empty;
            pasien.CreatedByUserID = "JAKSEHAT";
            pasien.CreatedDateTime = DateTime.Now;
            pasien.SRRelationshipQuality = string.Empty;
            pasien.SRResidentialHome = string.Empty;
            pasien.IsStoredToLokadok = false;
            pasien.FatherName = string.Empty;
            pasien.FatherAge = 0;
            pasien.FatherMedicalNo = string.Empty;
            pasien.SRFatherOccupation = string.Empty;
            pasien.FatherOccupationDesc = string.Empty;
            pasien.DeathCertificateNo = string.Empty;
            pasien.EmployeeNo = string.Empty;
            pasien.EmployeeJobTitleName = string.Empty;
            pasien.EmployeeJobDepartementName = string.Empty;
            pasien.ValuesOfTrust = string.Empty;
            pasien.str.DeceasedDateTime = string.Empty;
            pasien.FamilyRegisterNo = form["kkNumber"].Trim();
            pasien.IsSyncWithDukcapil = false;

            contact.PatientID = pasien.PatientID;
            contact.ContactName = form["patientCompanion[name]"];

            std = new AppStandardReferenceItem();
            std.Query.Where(std.Query.StandardReferenceID == AppEnum.StandardReference.Relationship.ToString(), std.Query.ItemName.Trim().ToLower() == form["patientCompanion[relationship]"].Trim().ToLower(), std.Query.IsActive == true);
            if (!std.Query.Load())
            {
                std = new AppStandardReferenceItem();
                std.StandardReferenceID = AppEnum.StandardReference.Relationship.ToString();
                std.ItemID = form["patientCompanion[relationship]"].Trim().Replace(" ", string.Empty);
                std.Note = string.Empty;
                std.IsUsedBySystem = false;
                std.IsActive = true;
                std.LastUpdateDateTime = DateTime.Now;
                std.LastUpdateByUserID = "JAKSEHAT";
                std.Save();
            }
            contact.SRRelationship = std.ItemID;

            contact.StreetName = form["patientCompanion[address]"];
            contact.District = string.Empty;
            contact.City = string.Empty;
            contact.County = string.Empty;
            contact.State = string.Empty;
            contact.str.ZipCode = string.Empty;
            contact.FaxNo = string.Empty;
            contact.Email = string.Empty;
            contact.PhoneNo = string.Empty;
            contact.MobilePhoneNo = form["patientCompanion[phoneNumber]"];
            contact.Notes = string.Empty;
            contact.LastUpdateDateTime = DateTime.Now;
            contact.LastUpdateByUserID = "JAKSEHAT";
            contact.SROccupation = string.Empty;
            contact.Ssn = string.Empty;

            using (var trans = new esTransactionScope())
            {
                autoNumberLastPID.Save();
                autoNumberLastMRN.Save();
                pasien.Save();
                contact.Save();

                trans.Complete();
            }

            return Request.CreateResponse(HttpStatusCode.Created, new
            {
                metaData = new
                {
                    code = HttpStatusCode.OK,
                    message = "OK"
                },
                data = new
                {
                    nik = pasien.Ssn,
                    kkNumber = pasien.FamilyRegisterNo,
                    name = pasien.PatientName,
                    gender = pasien.Sex == "M" ? "L" : "P",
                    dateOfBirth = pasien.DateOfBirth.Value.ToString("yyyy-MM-dd"),
                    address = pasien.StreetName,
                    provinceCode = form["provinceCode"].Trim(),
                    provinceNmae = pasien.State,
                    regencyCode = form["regencyCode"].Trim(),
                    regencyName = pasien.City,
                    districtCode = form["districtCode"].Trim(),
                    districtName = pasien.District,
                    villageCode = form["villageCode"].Trim(),
                    villageName = pasien.County,
                    rw = form["rw"].Trim(),
                    rt = form["rt"].Trim(),
                    insurrance = form["insurrance"].Trim(),
                    insurranceNumber = pasien.GuarantorCardNo
                }
            });
        }

        [JakSehatCustomAuthorization]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("jaksehat/find/referral/bpjs_number/{number}")]
        public HttpResponseMessage FindReferral(string number)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            var listRujukan = new List<Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2>();
            var rujukan = svc.GetRujukan(number, Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);
            if (rujukan != null)
            {
                if (rujukan.MetaData.IsValid && rujukan.Response != null && rujukan.Response.Rujukan != null) listRujukan.AddRange(rujukan.Response.Rujukan);
            }
            svc = new Common.BPJS.VClaim.v11.Service();
            rujukan = svc.GetRujukan(number, Common.BPJS.VClaim.Enum.JenisFaskes.RS);
            if (rujukan != null)
            {
                if (rujukan.MetaData.IsValid && rujukan.Response != null && rujukan.Response.Rujukan != null) listRujukan.AddRange(rujukan.Response.Rujukan);
            }

            var listResponse = new List<ListRujukanResponse>();
            if (listRujukan.Count > 0)
            {
                foreach (var itemRujukan in listRujukan)
                {
                    listResponse.Add(new ListRujukanResponse()
                    {
                        no_rujukan = itemRujukan.NoKunjungan,
                        tgl_rujukan = itemRujukan.TglKunjungan,
                        kode_ppk = itemRujukan.Pelayanan.Kode,
                        nama_rs_ppk = itemRujukan.Pelayanan.Nama,
                        kode_poli = itemRujukan.PoliRujukan.Kode,
                        nama_poli = itemRujukan.PoliRujukan.Nama
                    });
                }

                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    metaData = new
                    {
                        code = HttpStatusCode.OK,
                        message = "OK"
                    },
                    data = listResponse
                });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    metaData = new
                    {
                        code = (int)HttpStatusCode.Unauthorized,
                        message = "data tidak ditemukan"
                    }
                });
            }
        }

        private class ListRujukanResponse
        {
            public string no_rujukan { get; set; }
            public string tgl_rujukan { get; set; }
            public string kode_ppk { get; set; }
            public string nama_rs_ppk { get; set; }
            public string kode_poli { get; set; }
            public string nama_poli { get; set; }
        }

        [JakSehatCustomAuthorization]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("jaksehat/find/doctor/bypolyclinicid/{kodePoli}/date/{tanggal}")]
        public HttpResponseMessage FindSchedule(string kodePoli, string tanggal)
        {
            string format = "yyyy-MM-dd";

            if (!DateTime.TryParseExact(tanggal, format, null, System.Globalization.DateTimeStyles.None, out var parsed))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    metaData = new
                    {
                        code = (int)HttpStatusCode.Unauthorized,
                        message = "data tidak ditemukan"
                    }
                });
            }

            var sub = new ServiceUnit();
            if (!sub.LoadByPrimaryKey(kodePoli))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    metaData = new
                    {
                        code = (int)HttpStatusCode.Unauthorized,
                        message = "data tidak ditemukan"
                    }
                });
            }

            var pss = new ParamedicScheduleCollection();
            pss.Query.Where(pss.Query.ServiceUnitID == sub.ServiceUnitID, pss.Query.PeriodYear == parsed.Year.ToString());
            if (!pss.Query.Load() || !pss.Any() || pss.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    metaData = new
                    {
                        code = (int)HttpStatusCode.Unauthorized,
                        message = "data tidak ditemukan"
                    }
                });
            }

            var psds = new ParamedicScheduleDateCollection();
            psds.Query.Where(psds.Query.ServiceUnitID == sub.ServiceUnitID, psds.Query.ParamedicID.In(pss.Select(p => p.ParamedicID)), psds.Query.ScheduleDate.Date() == parsed.Date,
                psds.Query.PeriodYear == parsed.Year.ToString());
            if (!psds.Query.Load() || !psds.Any() || psds.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    metaData = new
                    {
                        code = (int)HttpStatusCode.Unauthorized,
                        message = "data tidak ditemukan"
                    }
                });
            }

            var pcs = new ParamedicCollection();
            pcs.Query.Where(pcs.Query.ParamedicID.In(psds.Select(p => p.ParamedicID)));
            pcs.Query.Load();

            var aps = new AppointmentCollection();
            aps.Query.Where(aps.Query.ServiceUnitID == sub.ServiceUnitID, aps.Query.ParamedicID.In(psds.Select(p => p.ParamedicID)), aps.Query.AppointmentDate.Date() == parsed.Date, aps.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
            aps.Query.Load();

            var listResponse = new List<ListScheduleResponse>();

            foreach (var ps in pss)
            {
                if (!pcs.Any(p => p.ParamedicID == ps.ParamedicID)) continue;

                var scheduleResponse = new ListScheduleResponse();
                scheduleResponse.doctorId = ps.ParamedicID;
                scheduleResponse.doctorName = pcs.Single(p => p.ParamedicID == ps.ParamedicID).ParamedicName;

                var times = new List<ListScheduleTimeResponse>();
                foreach (var psd in psds.Where(p => p.ParamedicID == ps.ParamedicID))
                {
                    var ot = new OperationalTime();
                    if (!ot.LoadByPrimaryKey(psd.OperationalTimeID)) continue;

                    if (!string.IsNullOrWhiteSpace(ot.StartTime1) && !string.IsNullOrWhiteSpace(ot.EndTime1))
                    {
                        times.Add(new ListScheduleTimeResponse()
                        {
                            start_time = ot.StartTime1,
                            end_time = ot.EndTime1,
                            quota_left = (ps.QuotaBpjsOnline ?? 0) == 0 ? 0 : (ps.QuotaBpjsOnline ?? 0) -
                                aps.Count(a => a.ParamedicID == ps.ParamedicID &&
                                    TimeSpan.ParseExact(a.AppointmentTime, "hh\\:mm", null) >= TimeSpan.ParseExact(ot.StartTime1.Trim(), "hh\\:mm", null) &&
                                    TimeSpan.ParseExact(a.AppointmentTime, "hh\\:mm", null) <= TimeSpan.ParseExact(ot.EndTime1.Trim(), "hh\\:mm", null))
                        });
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                    {
                        times.Add(new ListScheduleTimeResponse()
                        {
                            start_time = ot.StartTime2,
                            end_time = ot.EndTime2,
                            quota_left = (ps.QuotaBpjsOnline ?? 0) == 0 ? 0 : (ps.QuotaBpjsOnline ?? 0) -
                                aps.Count(a => a.ParamedicID == ps.ParamedicID &&
                                    TimeSpan.ParseExact(a.AppointmentTime, "hh\\:mm", null) >= TimeSpan.ParseExact(ot.StartTime2.Trim(), "hh\\:mm", null) &&
                                    TimeSpan.ParseExact(a.AppointmentTime, "hh\\:mm", null) <= TimeSpan.ParseExact(ot.EndTime2.Trim(), "hh\\:mm", null))
                        });
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                    {
                        times.Add(new ListScheduleTimeResponse()
                        {
                            start_time = ot.StartTime3,
                            end_time = ot.EndTime3,
                            quota_left = (ps.QuotaBpjsOnline ?? 0) == 0 ? 0 : (ps.QuotaBpjsOnline ?? 0) -
                                aps.Count(a => a.ParamedicID == ps.ParamedicID &&
                                    TimeSpan.ParseExact(a.AppointmentTime, "hh\\:mm", null) >= TimeSpan.ParseExact(ot.StartTime3.Trim(), "hh\\:mm", null) &&
                                    TimeSpan.ParseExact(a.AppointmentTime, "hh\\:mm", null) <= TimeSpan.ParseExact(ot.EndTime3.Trim(), "hh\\:mm", null))
                        });
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                    {
                        times.Add(new ListScheduleTimeResponse()
                        {
                            start_time = ot.StartTime4,
                            end_time = ot.EndTime4,
                            quota_left = (ps.QuotaBpjsOnline ?? 0) == 0 ? 0 : (ps.QuotaBpjsOnline ?? 0) -
                                aps.Count(a => a.ParamedicID == ps.ParamedicID &&
                                    TimeSpan.ParseExact(a.AppointmentTime, "hh\\:mm", null) >= TimeSpan.ParseExact(ot.StartTime4.Trim(), "hh\\:mm", null) &&
                                    TimeSpan.ParseExact(a.AppointmentTime, "hh\\:mm", null) <= TimeSpan.ParseExact(ot.EndTime4.Trim(), "hh\\:mm", null))
                        });
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                    {
                        times.Add(new ListScheduleTimeResponse()
                        {
                            start_time = ot.StartTime5,
                            end_time = ot.EndTime5,
                            quota_left = (ps.QuotaBpjsOnline ?? 0) == 0 ? 0 : (ps.QuotaBpjsOnline ?? 0) -
                                aps.Count(a => a.ParamedicID == ps.ParamedicID &&
                                    TimeSpan.ParseExact(a.AppointmentTime, "hh\\:mm", null) >= TimeSpan.ParseExact(ot.StartTime5.Trim(), "hh\\:mm", null) &&
                                    TimeSpan.ParseExact(a.AppointmentTime, "hh\\:mm", null) <= TimeSpan.ParseExact(ot.EndTime5.Trim(), "hh\\:mm", null))
                        });
                    }
                }

                scheduleResponse.times = times;

                listResponse.Add(scheduleResponse);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                metaData = new
                {
                    code = HttpStatusCode.OK,
                    message = "OK"
                },
                data = listResponse
            });
        }

        private class ListScheduleResponse
        {
            public string doctorId { get; set; }
            public string doctorName { get; set; }
            public List<ListScheduleTimeResponse> times { get; set; }
        }

        private class ListScheduleTimeResponse
        {
            public string start_time { get; set; }
            public string end_time { get; set; }
            public int quota_left { get; set; }
        }

        [JakSehatCustomAuthorization]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("jaksehat/appointment/sisa-antrian/{bookingCode}")]
        public HttpResponseMessage FindAppointment(string bookingCode)
        {
            var app = new Appointment();
            if (!app.LoadByPrimaryKey(bookingCode))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    metaData = new
                    {
                        code = (int)HttpStatusCode.Unauthorized,
                        message = "data tidak ditemukan"
                    }
                });
            }

            var su = new ServiceUnit();
            su.LoadByPrimaryKey(app.ServiceUnitID);

            var p = new Paramedic();
            p.LoadByPrimaryKey(app.ParamedicID);

            var g = new Guarantor();
            g.LoadByPrimaryKey(app.GuarantorID);

            var patient = new Patient();
            patient.LoadByPrimaryKey(app.PatientID);

            var psd = new ParamedicScheduleDate();
            psd.Query.Where(psd.Query.ServiceUnitID == su.ServiceUnitID, psd.Query.ParamedicID == p.ParamedicID, psd.Query.ScheduleDate.Date() == app.AppointmentDate.Value.Date);
            psd.Query.Load();

            var ot = new OperationalTime();
            ot.LoadByPrimaryKey(psd.OperationalTimeID);

            var startTime = string.Empty;
            var endTime = string.Empty;

            if (!string.IsNullOrWhiteSpace(ot.StartTime1) && !string.IsNullOrWhiteSpace(ot.EndTime1))
            {
                if (TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) >= TimeSpan.ParseExact(ot.StartTime1.Trim(), "hh\\:mm", null) &&
                    TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) <= TimeSpan.ParseExact(ot.EndTime1.Trim(), "hh\\:mm", null))
                {
                    startTime = ot.StartTime1;
                    endTime = ot.EndTime1;
                }
            }

            if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
            {
                if (TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) >= TimeSpan.ParseExact(ot.StartTime2.Trim(), "hh\\:mm", null) &&
                    TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) <= TimeSpan.ParseExact(ot.EndTime2.Trim(), "hh\\:mm", null))
                {
                    startTime = ot.StartTime2;
                    endTime = ot.EndTime2;
                }
            }

            if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
            {
                if (TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) >= TimeSpan.ParseExact(ot.StartTime3.Trim(), "hh\\:mm", null) &&
                    TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) <= TimeSpan.ParseExact(ot.EndTime3.Trim(), "hh\\:mm", null))
                {
                    startTime = ot.StartTime3;
                    endTime = ot.EndTime3;
                }
            }

            if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
            {
                if (TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) >= TimeSpan.ParseExact(ot.StartTime4.Trim(), "hh\\:mm", null) &&
                    TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) <= TimeSpan.ParseExact(ot.EndTime4.Trim(), "hh\\:mm", null))
                {
                    startTime = ot.StartTime4;
                    endTime = ot.EndTime4;
                }
            }

            if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
            {
                if (TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) >= TimeSpan.ParseExact(ot.StartTime5.Trim(), "hh\\:mm", null) &&
                    TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) <= TimeSpan.ParseExact(ot.EndTime5.Trim(), "hh\\:mm", null))
                {
                    startTime = ot.StartTime5;
                    endTime = ot.EndTime5;
                }
            }

            var apps = new AppointmentCollection();
            apps.Query.Where(apps.Query.ServiceUnitID == su.ServiceUnitID, apps.Query.ParamedicID == p.ParamedicID, apps.Query.AppointmentDate.Date() == app.AppointmentDate,
                apps.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel, apps.Query.AppointmentQue < (app.AppointmentQue ?? 0));
            apps.Query.Where(apps.Query.AppointmentTime >= startTime && apps.Query.AppointmentTime <= endTime);
            apps.Query.OrderBy(apps.Query.AppointmentQue.Descending);
            apps.Query.Load();

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                metaData = new
                {
                    code = HttpStatusCode.OK,
                    message = "OK"
                },
                data = new
                {
                    noMR = patient.MedicalNo,
                    bookingDate = app.AppointmentDate.Value.ToString("yyyy-MM-dd"),
                    policlinicID = app.ServiceUnitID,
                    policlinicName = su.ServiceUnitName,
                    doctorID = app.ParamedicID,
                    doctorName = p.ParamedicName,
                    paymentID = app.GuarantorID,
                    paymentName = g.GuarantorName,
                    referrralNumber = app.ReferenceNumber,
                    bpjsNumber = app.GuarantorCardNo,
                    startTime,
                    endTime,
                    bookingCode = $"{su.ShortName}-{app.AppointmentQue ?? 0}",
                    queueNumber = (app.AppointmentQue ?? 0).ToString(),
                    queue = new
                    {
                        untilMyTurn = apps.Any() ? apps.Count(a => a.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusClosed) : 0,
                        currentBookingCode = apps.Any() ? apps[0].AppointmentNo : string.Empty,
                        currentNumber = apps.Any() ? $"{su.ShortName}-{apps[0].AppointmentQue ?? 0}" : string.Empty
                    }
                }
            });
        }

        [JakSehatCustomAuthorization]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("jaksehat/appointment/cancel")]
        public HttpResponseMessage CancelAppointment(CancelAppointmentRequest param)
        {
            var app = new Appointment();
            if (!app.LoadByPrimaryKey(param.bookingCode))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    metaData = new
                    {
                        code = (int)HttpStatusCode.Unauthorized,
                        message = "data tidak ditemukan"
                    }
                });
            }

            app.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusCancel;
            app.LastUpdateByUserID = "jaksehat";
            app.LastUpdateDateTime = DateTime.Now;
            app.Save();

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                metaData = new
                {
                    code = HttpStatusCode.OK,
                    message = "OK. 1 appointment(s) has been canceled"
                }
            });
        }

        public class CancelAppointmentRequest
        {
            public string noMR { get; set; }
            public string bookingCode { get; set; }
        }

        [JakSehatCustomAuthorization]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("jaksehat/appointment/search/{noMr}/{startDate}/{endDate}")]
        public HttpResponseMessage FindAppointments(string noMr, string startDate, string endDate)
        {
            var patient = new Patient();
            if (!patient.LoadByMedicalNo(noMr))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    metaData = new
                    {
                        code = (int)HttpStatusCode.Unauthorized,
                        message = "data tidak ditemukan"
                    }
                });
            }

            var format = "yyyy-MM-dd";
            if (!DateTime.TryParseExact(startDate, format, null, System.Globalization.DateTimeStyles.None, out var parsedStart) ||
                !DateTime.TryParseExact(startDate, format, null, System.Globalization.DateTimeStyles.None, out var parsedEnd))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    metaData = new
                    {
                        code = (int)HttpStatusCode.Unauthorized,
                        message = "data tidak ditemukan"
                    }
                });
            }

            var apps = new AppointmentCollection();
            apps.Query.Where(apps.Query.PatientID == patient.PatientID, apps.Query.AppointmentDate.Date() >= parsedStart.Date, apps.Query.AppointmentDate.Date() <= parsedEnd.Date);
            if (!apps.Query.Load())
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    metaData = new
                    {
                        code = (int)HttpStatusCode.Unauthorized,
                        message = "data tidak ditemukan"
                    }
                });
            }

            var suc = new ServiceUnitCollection();
            suc.Query.Where(suc.Query.ServiceUnitID.In(apps.Select(a => a.ServiceUnitID)));
            suc.Query.Load();

            var pc = new ParamedicCollection();
            pc.Query.Where(pc.Query.ParamedicID.In(apps.Select(a => a.ParamedicID)));
            pc.Query.Load();

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                metaData = new
                {
                    code = HttpStatusCode.OK,
                    message = "OK"
                },
                data = apps.Select(a => new FindAppointmentResponse()
                {
                    bookingCode = a.AppointmentNo,
                    noMR = patient.MedicalNo,
                    patientName = patient.PatientName,
                    bookingDate = a.AppointmentDate.Value.ToString("yyyy-MM-dd"),
                    policlinicID = a.ServiceUnitID,
                    policlinicName = suc.Single(s => s.ServiceUnitID == a.ServiceUnitID).ServiceUnitName,
                    doctorID = a.ParamedicID,
                    doctorName = pc.Single(s => s.ParamedicID == a.ParamedicID).ParamedicID,
                    paymentID = a.GuarantorID,
                    queueNumber = (a.AppointmentQue ?? 0).ToString(),
                    startTime = FindAppointmentTime(a, true),
                    endTime = FindAppointmentTime(a, false),
                    waitingNumber = a.AppointmentQue ?? 0,
                    statusID = a.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusCancel ? 3 : (a.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusClosed ? 2 : 1)
                }).ToList()
            });
        }

        private class FindAppointmentResponse
        {
            public string bookingCode { get; set; }
            public string noMR { get; set; }
            public string patientName { get; set; }
            public string bookingDate { get; set; }
            public string policlinicID { get; set; }
            public string policlinicName { get; set; }
            public string doctorID { get; set; }
            public string doctorName { get; set; }
            public string paymentID { get; set; }
            public string queueNumber { get; set; }
            public string startTime { get; set; }
            public string endTime { get; set; }
            public int waitingNumber { get; set; }
            public int statusID { get; set; }
        }

        private string FindAppointmentTime(Appointment app, bool isStartTime)
        {
            var psd = new ParamedicScheduleDate();
            psd.Query.Where(psd.Query.ServiceUnitID == app.ServiceUnitID, psd.Query.ParamedicID == app.ParamedicID, psd.Query.ScheduleDate.Date() == app.AppointmentDate.Value.Date);
            psd.Query.Load();

            var ot = new OperationalTime();
            ot.LoadByPrimaryKey(psd.OperationalTimeID);

            var startTime = string.Empty;
            var endTime = string.Empty;

            if (!string.IsNullOrWhiteSpace(ot.StartTime1) && !string.IsNullOrWhiteSpace(ot.EndTime1))
            {
                if (TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) >= TimeSpan.ParseExact(ot.StartTime1.Trim(), "hh\\:mm", null) &&
                    TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) <= TimeSpan.ParseExact(ot.EndTime1.Trim(), "hh\\:mm", null))
                {
                    startTime = ot.StartTime1;
                    endTime = ot.EndTime1;
                }
            }

            if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
            {
                if (TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) >= TimeSpan.ParseExact(ot.StartTime2.Trim(), "hh\\:mm", null) &&
                    TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) <= TimeSpan.ParseExact(ot.EndTime2.Trim(), "hh\\:mm", null))
                {
                    startTime = ot.StartTime2;
                    endTime = ot.EndTime2;
                }
            }

            if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
            {
                if (TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) >= TimeSpan.ParseExact(ot.StartTime3.Trim(), "hh\\:mm", null) &&
                    TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) <= TimeSpan.ParseExact(ot.EndTime3.Trim(), "hh\\:mm", null))
                {
                    startTime = ot.StartTime3;
                    endTime = ot.EndTime3;
                }
            }

            if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
            {
                if (TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) >= TimeSpan.ParseExact(ot.StartTime4.Trim(), "hh\\:mm", null) &&
                    TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) <= TimeSpan.ParseExact(ot.EndTime4.Trim(), "hh\\:mm", null))
                {
                    startTime = ot.StartTime4;
                    endTime = ot.EndTime4;
                }
            }

            if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
            {
                if (TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) >= TimeSpan.ParseExact(ot.StartTime5.Trim(), "hh\\:mm", null) &&
                    TimeSpan.ParseExact(app.AppointmentTime, "hh\\:mm", null) <= TimeSpan.ParseExact(ot.EndTime5.Trim(), "hh\\:mm", null))
                {
                    startTime = ot.StartTime5;
                    endTime = ot.EndTime5;
                }
            }

            return isStartTime ? startTime : endTime;
        }

        [JakSehatCustomAuthorization]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("jaksehat/appointment/create")]
        public HttpResponseMessage CreateAppointment(CreateAppointmenrRequest param)
        {
            var su = new ServiceUnit();
            if (!su.LoadByPrimaryKey(param.policlinicID))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    metaData = new
                    {
                        code = (int)HttpStatusCode.Unauthorized,
                        message = "policlinicID tidak ditemukan"
                    }
                });
            }

            var p = new Paramedic();
            if (!p.LoadByPrimaryKey(param.doctorID))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    metaData = new
                    {
                        code = (int)HttpStatusCode.Unauthorized,
                        message = "doctorID tidak ditemukan"
                    }
                });
            }

            var pasien = new Patient();
            if (!pasien.LoadByMedicalNo(param.noMR))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    metaData = new
                    {
                        code = (int)HttpStatusCode.Unauthorized,
                        message = "noMR tidak ditemukan"
                    }
                });
            }

            var g = new Guarantor();
            if (!g.LoadByPrimaryKey(param.paymentID))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    metaData = new
                    {
                        code = (int)HttpStatusCode.Unauthorized,
                        message = "paymentID tidak ditemukan"
                    }
                });
            }

            if (!DateTime.TryParseExact(param.bookingDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var parsed))
            {
                return Request.CreateResponse((HttpStatusCode)422, new
                {
                    metaData = new
                    {
                        code = 422,
                        message = "bookingDate tidak ditemukan"
                    }
                });
            }

            var ps = new ParamedicSchedule();
            ps.LoadByPrimaryKey(su.ServiceUnitID, p.ParamedicID, parsed.Year.ToString());

            var psd = new ParamedicScheduleDate();
            psd.LoadByPrimaryKey(su.ServiceUnitID, p.ParamedicID, parsed.Year.ToString(), parsed.Date);

            var ot = new OperationalTime();
            ot.LoadByPrimaryKey(psd.OperationalTimeID);

            var startTime = string.Empty;
            var endTime = string.Empty;

            if (!string.IsNullOrWhiteSpace(ot.StartTime1) && !string.IsNullOrWhiteSpace(ot.EndTime1))
            {
                if (TimeSpan.ParseExact(param.startTime, "hh\\:mm", null) == TimeSpan.ParseExact(ot.StartTime1.Trim(), "hh\\:mm", null) &&
                    TimeSpan.ParseExact(param.endTime, "hh\\:mm", null) == TimeSpan.ParseExact(ot.EndTime1.Trim(), "hh\\:mm", null))
                {
                    startTime = ot.StartTime1;
                    endTime = ot.EndTime1;
                }
            }

            if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
            {
                if (TimeSpan.ParseExact(param.startTime, "hh\\:mm", null) == TimeSpan.ParseExact(ot.StartTime2.Trim(), "hh\\:mm", null) &&
                    TimeSpan.ParseExact(param.endTime, "hh\\:mm", null) == TimeSpan.ParseExact(ot.EndTime2.Trim(), "hh\\:mm", null))
                {
                    startTime = ot.StartTime2;
                    endTime = ot.EndTime2;
                }
            }

            if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
            {
                if (TimeSpan.ParseExact(param.startTime, "hh\\:mm", null) == TimeSpan.ParseExact(ot.StartTime3.Trim(), "hh\\:mm", null) &&
                    TimeSpan.ParseExact(param.endTime, "hh\\:mm", null) == TimeSpan.ParseExact(ot.EndTime3.Trim(), "hh\\:mm", null))
                {
                    startTime = ot.StartTime3;
                    endTime = ot.EndTime3;
                }
            }

            if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
            {
                if (TimeSpan.ParseExact(param.startTime, "hh\\:mm", null) == TimeSpan.ParseExact(ot.StartTime4.Trim(), "hh\\:mm", null) &&
                    TimeSpan.ParseExact(param.endTime, "hh\\:mm", null) == TimeSpan.ParseExact(ot.EndTime4.Trim(), "hh\\:mm", null))
                {
                    startTime = ot.StartTime4;
                    endTime = ot.EndTime4;
                }
            }

            if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
            {
                if (TimeSpan.ParseExact(param.startTime, "hh\\:mm", null) == TimeSpan.ParseExact(ot.StartTime5.Trim(), "hh\\:mm", null) &&
                    TimeSpan.ParseExact(param.endTime, "hh\\:mm", null) == TimeSpan.ParseExact(ot.EndTime5.Trim(), "hh\\:mm", null))
                {
                    startTime = ot.StartTime5;
                    endTime = ot.EndTime5;
                }
            }

            TimeSpan? time = TimeSpan.ParseExact(startTime, "hh\\:mm", null);
            int que = 1;
            bool exist = false;

            var availableTimes = new List<AppointmentTime>();
            while (time >= TimeSpan.ParseExact(startTime, "hh\\:mm", null) && time <= TimeSpan.ParseExact(endTime, "hh\\:mm", null))
            {
                availableTimes.Add(new AppointmentTime()
                {
                    Time = time.Value,
                    Que = que,
                    AppointmentNo = string.Empty
                });
                time = time.Value.Add(new TimeSpan(0, ps.ExamDuration ?? 0, 0));
                que++;
            }

            var appointments = new AppointmentCollection();
            appointments.Query.Where(
                appointments.Query.ServiceUnitID == su.ServiceUnitID,
                appointments.Query.ParamedicID == p.ParamedicID,
                appointments.Query.AppointmentDate.Date() == parsed.Date,
                appointments.Query.AppointmentTime >= startTime,
                appointments.Query.AppointmentTime <= endTime,
                appointments.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
            appointments.Query.Load();
            if (!appointments.Any())
            {
                time = TimeSpan.ParseExact(startTime, "hh\\:mm", null);
                que = 1;
            }
            else
            {
                foreach (var availableTime in availableTimes)
                {
                    var appointment = appointments.SingleOrDefault(a => a.AppointmentTime == availableTime.Time.ToString("hh\\:mm"));
                    if (appointment != null)
                    {
                        availableTime.Que = appointment.AppointmentQue ?? 0;
                        availableTime.AppointmentNo = appointment.AppointmentNo;
                    }
                }

                var aTime = availableTimes.Where(a => string.IsNullOrWhiteSpace(a.AppointmentNo)).First();

                time = aTime.Time;
                que = aTime.Que;
            }

            var autoNumberLastAntrean = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.AppointmentNo);
            var antrean = new Appointment();
            antrean.Query.es.Top = 1;
            antrean.Query.Where(antrean.Query.ServiceUnitID == su.ServiceUnitID, antrean.Query.ParamedicID == p.ParamedicID, antrean.Query.AppointmentDate == parsed.Date, antrean.Query.GuarantorCardNo == param.bpjsNumber, antrean.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
            if (antrean.Query.Load())
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    metaData = new
                    {
                        code = (int)HttpStatusCode.BadRequest,
                        message = "Tidak diperbolehkan daftar poli yang sama"
                    }
                });
            }

            antrean = new Appointment();
            antrean.AppointmentNo = autoNumberLastAntrean.LastCompleteNumber;
            antrean.AppointmentQue = que;
            antrean.ServiceUnitID = su.ServiceUnitID;
            antrean.ParamedicID = p.ParamedicID;
            antrean.PatientID = pasien.PatientID;
            antrean.AppointmentDate = parsed.Date;
            antrean.AppointmentTime = time.Value.ToString("hh\\:mm");
            antrean.VisitTypeID = string.Empty;
            antrean.VisitDuration = Convert.ToByte(ps.ExamDuration ?? 0);
            antrean.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusOpen;
            antrean.FirstName = pasien.FirstName;
            antrean.MiddleName = pasien.MiddleName;
            antrean.LastName = pasien.LastName;
            antrean.StreetName = pasien.StreetName;
            antrean.District = pasien.District;
            antrean.City = pasien.City;
            antrean.County = pasien.County;
            antrean.State = pasien.State;
            antrean.ZipCode = pasien.ZipCode;
            antrean.PhoneNo = pasien.PhoneNo;
            antrean.FaxNo = pasien.FaxNo;
            antrean.Email = pasien.Email;
            antrean.MobilePhoneNo = pasien.MobilePhoneNo;
            antrean.Notes = string.Empty; //"Peserta harap 60 menit lebih awal guna pencatatan administrasi";
            antrean.str.PatientPIC = string.Empty;
            antrean.str.OfficerPIC = string.Empty;
            antrean.str.FollowUpDateTime = string.Empty;
            antrean.LastUpdateDateTime = DateTime.Now;
            antrean.LastUpdateByUserID = "jaksehat";
            antrean.LastCreateDateTime = DateTime.Now;
            antrean.LastCreateByUserID = "jaksehat";
            antrean.DateOfBirth = pasien.DateOfBirth;
            antrean.GuarantorID = g.GuarantorID;
            antrean.FromRegistrationNo = string.Empty;
            antrean.EmployeeNo = string.Empty;
            antrean.EmployeeJobTitleName = string.Empty;
            antrean.EmployeeJobDepartementName = string.Empty;
            antrean.Sex = pasien.Sex;
            antrean.BirthPlace = pasien.CityOfBirth;
            antrean.Ssn = pasien.Ssn;
            antrean.SRSalutation = pasien.SRSalutation;
            antrean.SRNationality = pasien.SRNationality;
            antrean.SROccupation = pasien.SROccupation;
            antrean.SRMaritalStatus = pasien.SRMaritalStatus;
            antrean.ItemID = string.Empty;
            antrean.SRReferralGroup = string.Empty;
            antrean.ReferralName = string.Empty;
            antrean.GuarantorCardNo = param.bpjsNumber;
            antrean.ReferenceNumber = param.noreferrralNumberMR;
            antrean.ReferenceType = 0;
            antrean.SRAppoinmentType = string.Empty;

            using (var trans = new esTransactionScope())
            {
                autoNumberLastAntrean.Save();
                antrean.Save();

                trans.Complete();
            }

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                metaData = new
                {
                    code = HttpStatusCode.OK,
                    message = "OK"
                },
                data = new
                {
                    bookingCode = antrean.AppointmentNo,
                    queueNumber = (antrean.AppointmentQue ?? 0).ToString(),
                    waitingNumber = antrean.AppointmentQue ?? 0
                }
            });
        }

        public class CreateAppointmenrRequest
        {
            public string noMR { get; set; }
            public string bookingDate { get; set; }
            public string policlinicID { get; set; }
            public string doctorID { get; set; }
            public string paymentID { get; set; }
            public string noreferrralNumberMR { get; set; }
            public string bpjsNumber { get; set; }
            public string startTime { get; set; }
            public string endTime { get; set; }
        }

        private class AppointmentTime
        {
            public TimeSpan Time { get; set; }
            public int Que { get; set; }
            public string AppointmentNo { get; set; }
        }
    }
}