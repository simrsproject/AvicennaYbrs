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
    public class JakiHelfaController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("jaki/auth")]
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
                        code = HttpStatusCode.OK.ToString(),
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

        [CustomAuthorization]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("jaki/patient/find/{param1}/{param2}")]
        public HttpResponseMessage ValidasiPasien(string param1, string param2)
        {
            // param1 = nomr, nik, nojkn
            // param2 = value

            var patient = new Patient();
            if (param1 == "nomr")
                patient.Query.Where(patient.Query.MedicalNo == param2);
            else if (param2 == "nik")
                patient.Query.Where(patient.Query.Ssn == param2);
            else if (param2 == "nojkn")
                patient.Query.Where(patient.Query.GuarantorCardNo == param2);
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    metaData = new
                    {
                        code = HttpStatusCode.BadRequest.ToString(),
                        message = "param1 kosong"
                    }
                });
            }

            if (!patient.Query.Load())
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metaData = new
                    {
                        code = HttpStatusCode.NotFound.ToString(),
                        message = "data tidak ditemukan"
                    }
                });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    metaData = new
                    {
                        code = HttpStatusCode.OK.ToString(),
                        message = "Sukses"
                    },
                    data = new
                    {
                        noMR = patient.MedicalNo,
                        name = patient.PatientName,
                        gender = patient.Sex == "M" ? "L" : "P",
                        birthDate = patient.DateOfBirth.Value.Date.ToString("yyyy-MM-dd"),
                        nik = patient.Ssn,
                        noJKN = patient.GuarantorCardNo,
                        phone = patient.MobilePhoneNo
                    }
                });
            }
        }

        [CustomAuthorization]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("jaki/find/doctor/bypolyclinicid/{polyclinicId}/date/{visitDate}")]
        public HttpResponseMessage PoliklinikDanDokter(string polyclinicId, string visitDate)
        {
            string format = "yyyy-MM-dd";
            DateTime.TryParseExact(visitDate, format, null, System.Globalization.DateTimeStyles.None, out var parsed);

            var psd = new ParamedicScheduleDateQuery("a");
            var p = new ParamedicQuery("b");
            var ot = new OperationalTimeQuery("c");

            psd.Select(psd.ParamedicID, p.ParamedicName, ot.StartTime1, ot.EndTime1);
            psd.InnerJoin(p).On(psd.ParamedicID == p.ParamedicID);
            psd.InnerJoin(ot).On(psd.OperationalTimeID == ot.OperationalTimeID);
            psd.Where(psd.ServiceUnitID == polyclinicId, psd.ScheduleDate == parsed.Date);

            var table = psd.LoadDataTable();

            if (table.Rows.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new PoliklinikDanDokterResponse.MetaData()
                {
                    Code = HttpStatusCode.NotFound.ToString(),
                    Message = "data not found"
                });
            }
            else
            {
                var time = new List<PoliklinikDanDokterResponse.Time>();
                foreach (var row in table.AsEnumerable().Select(t => new
                {
                    doctorId = t.Field<string>("doctorId"),
                    startTime = t.Field<string>("StartTime1"),
                    endTime = t.Field<string>("EndTime1")
                }).Distinct())
                {
                    time.Add(new PoliklinikDanDokterResponse.Time()
                    {
                        DoctorId = row.doctorId,
                        StartTime = row.startTime,
                        EndTime = row.endTime,
                        QuotaLeft = 10
                    });
                }

                var data = new List<PoliklinikDanDokterResponse.Datum>();
                foreach (var row in table.AsEnumerable().Select(t => new
                {
                    doctorId = t.Field<string>("doctorId"),
                    doctorName = t.Field<string>("doctorName")
                }).Distinct())
                {
                    data.Add(new PoliklinikDanDokterResponse.Datum()
                    {
                        DoctorId = row.doctorId,
                        DoctorName = row.doctorName,
                        Times = time.Where(t => t.DoctorId == row.doctorId).ToList()
                    });
                }

                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    metaData = new
                    {
                        code = HttpStatusCode.OK.ToString(),
                        message = "Sukses"
                    },
                    data = data
                });
            }
        }

        [CustomAuthorization]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("jaki/find/referral/bpjs_number/{bpjs_number}")]
        public HttpResponseMessage CariRujukanMultiple(string bpjs_number)
        {
            var data = new List<CariRujukanMultipleResponse.Datum>();

            var svc = new Common.BPJS.VClaim.v11.Service();
            var responseRujukan = svc.GetRujukan(bpjs_number, Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);
            if (responseRujukan.MetaData.IsValid)
            {
                foreach (var entity in responseRujukan.Response.Rujukan)
                {
                    data.Add(new CariRujukanMultipleResponse.Datum()
                    {
                        NoRujukan = entity.NoKunjungan,
                        TglRujukan = entity.TglKunjungan,
                        KodePpk = entity.ProvPerujuk.Kode,
                        NamaRsPpk = entity.ProvPerujuk.Nama,
                        KodePoli = entity.PoliRujukan.Kode,
                        NamaPoli = entity.PoliRujukan.Nama
                    });
                }
            }

            responseRujukan = svc.GetRujukan(bpjs_number, Common.BPJS.VClaim.Enum.JenisFaskes.RS);
            if (responseRujukan.MetaData.IsValid)
            {
                foreach (var entity in responseRujukan.Response.Rujukan)
                {
                    data.Add(new CariRujukanMultipleResponse.Datum()
                    {
                        NoRujukan = entity.NoKunjungan,
                        TglRujukan = entity.TglKunjungan,
                        KodePpk = entity.ProvPerujuk.Kode,
                        NamaRsPpk = entity.ProvPerujuk.Nama,
                        KodePoli = entity.PoliRujukan.Kode,
                        NamaPoli = entity.PoliRujukan.Nama
                    });
                }
            }

            if (!data.Any())
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metaData = new
                    {
                        code = HttpStatusCode.NotFound.ToString(),
                        message = "data not found"
                    }
                });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    metaData = new
                    {
                        code = HttpStatusCode.OK.ToString(),
                        message = "Sukses"
                    },
                    data = data
                });
            }
        }

        [CustomAuthorization]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("jaki/appointment/create")]
        public HttpResponseMessage InsertPerjanjian([FromBody] InsertPerjanjianClass.Request request)
        {
            string format = "yyyy-MM-dd";
            DateTime parsed;
            DateTime.TryParseExact(request.BookingDate, format, null, System.Globalization.DateTimeStyles.None, out parsed);

            var unit = new ServiceUnitBridging();
            unit.Query.Where(unit.Query.SRBridgingType == AppEnum.BridgingType.BPJS && unit.Query.BridgingID == request.PoliclinicID);
            if (!unit.Query.Load())
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metaData = new
                    {
                        code = HttpStatusCode.NotFound.ToString(),
                        message = $"policlinicid : {request.PoliclinicID} tidak terdaftar"
                    }
                });
            }

            var pmedic = new ParamedicBridging();
            pmedic.Query.Where(pmedic.Query.SRBridgingType == AppEnum.BridgingType.BPJS && pmedic.Query.BridgingID == request.DoctorID);
            if (!pmedic.Query.Load())
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metaData = new
                    {
                        code = HttpStatusCode.NotFound.ToString(),
                        message = $"doctorid {request.DoctorID} tidak terdaftar"
                    }
                });
            }

            var patient = new Patient();
            if (!patient.LoadByMedicalNo(request.NoMR))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metaData = new
                    {
                        code = HttpStatusCode.NotFound.ToString(),
                        message = $"nomr {request.NoMR} tidak terdaftar."
                    }
                });
            }

            var psd = new ParamedicScheduleDate();
            if (!psd.LoadByPrimaryKey(unit.ServiceUnitID, pmedic.ParamedicID, DateTime.Now.Year.ToString(), parsed))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metaData = new
                    {
                        code = HttpStatusCode.NotFound.ToString(),
                        message = $"jadwal tidak tersedia"
                    }
                });
            }

            var queNo = 1;
            var appt = new AppointmentCollection();
            appt.Query.Where(appt.Query.ServiceUnitID == unit.ServiceUnitID,
                appt.Query.ParamedicID == pmedic.ParamedicID,
                appt.Query.AppointmentDate == parsed,
                appt.Query.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusOpen);
            if (appt.Query.Load() || appt.Any())
                queNo = appt.Count + 1;

            var entity = new Appointment();
            entity.AppointmentQue = queNo;
            entity.ServiceUnitID = unit.ServiceUnitID;
            entity.ParamedicID = pmedic.ParamedicID;
            entity.PatientID = patient.PatientID;
            entity.AppointmentDate = parsed.Date;
            entity.AppointmentTime = request.StartTime;
            entity.VisitTypeID = string.Empty;
            entity.VisitDuration = 0;
            entity.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusOpen;
            entity.FirstName = patient.FirstName;
            entity.MiddleName = patient.MiddleName;
            entity.LastName = patient.LastName;
            entity.StreetName = patient.StreetName;
            entity.District = patient.District;
            entity.City = patient.City;
            entity.County = patient.County;
            entity.State = patient.State;
            entity.ZipCode = patient.ZipCode;
            entity.PhoneNo = patient.PhoneNo;
            entity.FaxNo = patient.FaxNo;
            entity.Email = patient.Email;
            entity.MobilePhoneNo = patient.MobilePhoneNo;
            entity.Notes = string.Empty;
            entity.PatientPIC = string.Empty;
            entity.OfficerPIC = string.Empty;
            entity.str.FollowUpDateTime = string.Empty;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.LastUpdateByUserID = "jaki";
            entity.LastCreateDateTime = (new DateTime()).NowAtSqlServer();
            entity.LastCreateByUserID = "jaki";
            entity.DateOfBirth = patient.DateOfBirth;
            entity.GuarantorID = AppSession.Parameter.GuarantorAskesID[0];
            entity.FromRegistrationNo = string.Empty;
            entity.EmployeeNo = string.Empty;
            entity.EmployeeJobTitleName = string.Empty;
            entity.EmployeeJobDepartementName = string.Empty;
            entity.Sex = patient.Sex;
            entity.BirthPlace = patient.CityOfBirth;
            entity.Ssn = patient.Ssn;
            entity.SRSalutation = patient.SRSalutation;
            entity.SRNationality = patient.SRNationality;
            entity.SROccupation = patient.SROccupation;
            entity.SRMaritalStatus = patient.SRMaritalStatus;
            entity.ItemID = string.Empty;
            entity.SRReferralGroup = string.Empty;
            entity.ReferralName = string.Empty;
            entity.GuarantorCardNo = request.BpjsNumber;
            entity.ReferenceNumber = request.ReferrralNumber;
            entity.str.ReferenceType = string.Empty;

            using (var trans = new esTransactionScope())
            {
                AppAutoNumberLast autoNumber = Helper.GetNewAutoNumber(entity.AppointmentDate.Value.Date, AppEnum.AutoNumber.AppointmentNo, string.Empty, "jaki");
                entity.AppointmentNo = autoNumber.LastCompleteNumber;
                autoNumber.Save();

                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                metaData = new
                {
                    code = HttpStatusCode.OK.ToString(),
                    message = "Sukses"
                },
                data = new
                {
                    bookingCode = entity.AppointmentNo,
                    queueNumber = entity.AppointmentQue,
                    waitingNumber = 0
                }
            });
        }

        [CustomAuthorization]
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("jaki/appointment/cancel")]
        public HttpResponseMessage CancelPerjanjian([FromBody] CancelPerjanjianRequest request)
        {
            var entity = new Appointment();
            if (!entity.LoadByPrimaryKey(request.BookingCode))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metaData = new
                    {
                        code = HttpStatusCode.NotFound.ToString(),
                        message = $"bookingcode {request.BookingCode} tidak terdaftar"
                    }
                });
            }

            entity.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusCancel;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.LastUpdateByUserID = "jaki";
            entity.Save();

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                metaData = new
                {
                    code = HttpStatusCode.OK.ToString(),
                    message = "Sukses"
                }
            });
        }

        [CustomAuthorization]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("jaki/appointment/search/{noMR}/{startDate}/{endDate}")]
        public HttpResponseMessage PencarianPerjanjian(string noMR, string startDate, string endDate)
        {
            string format = "yyyy-MM-dd";
            DateTime.TryParseExact(startDate, format, null, System.Globalization.DateTimeStyles.None, out var parsedStartDate);
            DateTime.TryParseExact(startDate, format, null, System.Globalization.DateTimeStyles.None, out var parsedEndDate);

            var patient = new Patient();
            patient.LoadByMedicalNo(noMR);

            var app = new AppointmentCollection();
            app.Query.Where(app.Query.PatientID == patient.PatientID, app.Query.AppointmentDate.Between(parsedStartDate.Date, parsedEndDate.Date));
            if (app.Query.Load() || app.Any())
            {
                var data = new List<PencarianPerjanjianResponse.Datum>();
                foreach (var row in app)
                {
                    var unit = new ServiceUnit();
                    unit.LoadByPrimaryKey(row.ServiceUnitID);

                    var pmedic = new Paramedic();
                    pmedic.LoadByPrimaryKey(row.ParamedicID);

                    data.Add(new PencarianPerjanjianResponse.Datum()
                    {
                        BookingCode = row.AppointmentNo,
                        NoMR = patient.MedicalNo,
                        PatientName = patient.PatientName,
                        BookingDate = row.AppointmentDate.Value.ToString("yyyy-MM-dd"),
                        PoliclinicID = row.ServiceUnitID,
                        PoliclinicName = unit.ServiceUnitName,
                        DoctorID = row.ParamedicID,
                        DoctorName = pmedic.ParamedicName,
                        PaymentID = string.Empty,
                        QueueNumber = (row.AppointmentQue ?? 0).ToString(),
                        StartTime = row.AppointmentTime,
                        EndTime = row.AppointmentTime,
                        WaitingNumber = 0,
                        StatusID = row.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusOpen ? 1 : row.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusClosed ? 2 : 3
                    });
                }

                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    metaData = new
                    {
                        code = HttpStatusCode.OK.ToString(),
                        message = "Sukses"
                    },
                    data = data
                });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metaData = new
                    {
                        code = HttpStatusCode.NotFound.ToString(),
                        message = "data tidak ditemukan"
                    }
                });
            }
        }
    }

    public class PoliklinikDanDokterResponse
    {
        public class MetaData
        {
            [JsonProperty("code")]
            public string Code;

            [JsonProperty("message")]
            public string Message;
        }

        public class Time
        {
            [JsonProperty("doctorId")]
            public string DoctorId;

            [JsonProperty("start_time")]
            public string StartTime;

            [JsonProperty("end_time")]
            public string EndTime;

            [JsonProperty("quota_left")]
            public int QuotaLeft;
        }

        public class Datum
        {
            [JsonProperty("doctorId")]
            public string DoctorId;

            [JsonProperty("doctorName")]
            public string DoctorName;

            [JsonProperty("times")]
            public List<Time> Times;
        }

        public class Root
        {
            [JsonProperty("metaData")]
            public MetaData MetaData;

            [JsonProperty("data")]
            public List<Datum> Data;
        }
    }

    public class CariRujukanMultipleResponse
    {
        public class MetaData
        {
            [JsonProperty("code")]
            public int Code;

            [JsonProperty("message")]
            public string Message;
        }

        public class Datum
        {
            [JsonProperty("no_rujukan")]
            public string NoRujukan;

            [JsonProperty("tgl_rujukan")]
            public string TglRujukan;

            [JsonProperty("kode_ppk")]
            public string KodePpk;

            [JsonProperty("nama_rs_ppk")]
            public string NamaRsPpk;

            [JsonProperty("kode_poli")]
            public string KodePoli;

            [JsonProperty("nama_poli")]
            public string NamaPoli;
        }

        public class Root
        {
            [JsonProperty("metaData")]
            public MetaData MetaData;

            [JsonProperty("data")]
            public List<Datum> Data;
        }
    }

    public class InsertPerjanjianClass
    {
        public class Request
        {
            [JsonProperty("noMR")]
            public string NoMR;

            [JsonProperty("bookingDate")]
            public string BookingDate;

            [JsonProperty("policlinicID")]
            public string PoliclinicID;

            [JsonProperty("doctorID")]
            public string DoctorID;

            [JsonProperty("paymentID")]
            public string PaymentID;

            [JsonProperty("referrralNumber")]
            public string ReferrralNumber;

            [JsonProperty("bpjsNumber")]
            public string BpjsNumber;

            [JsonProperty("startTime")]
            public string StartTime;

            [JsonProperty("endTime")]
            public string EndTime;
        }

        public class Response
        {
            public class MetaData
            {
                [JsonProperty("code")]
                public int Code;

                [JsonProperty("message")]
                public string Message;
            }

            public class Data
            {
                [JsonProperty("bookingCode")]
                public string BookingCode;

                [JsonProperty("queueNumber")]
                public string QueueNumber;

                [JsonProperty("waitingNumber")]
                public int WaitingNumber;
            }

            public class Root
            {
                [JsonProperty("metaData")]
                public MetaData MetaData;

                [JsonProperty("data")]
                public Data Data;
            }
        }
    }

    public class CancelPerjanjianRequest
    {
        [JsonProperty("noMR")]
        public string NoMR;

        [JsonProperty("bookingCode")]
        public string BookingCode;
    }

    public class PencarianPerjanjianResponse
    {
        public class MetaData
        {
            [JsonProperty("code")]
            public int Code;

            [JsonProperty("message")]
            public string Message;
        }

        public class Datum
        {
            [JsonProperty("bookingCode")]
            public string BookingCode;

            [JsonProperty("noMR")]
            public string NoMR;

            [JsonProperty("patientName")]
            public string PatientName;

            [JsonProperty("bookingDate")]
            public string BookingDate;

            [JsonProperty("policlinicID")]
            public string PoliclinicID;

            [JsonProperty("policlinicName")]
            public string PoliclinicName;

            [JsonProperty("doctorID")]
            public string DoctorID;

            [JsonProperty("doctorName")]
            public string DoctorName;

            [JsonProperty("paymentID")]
            public string PaymentID;

            [JsonProperty("queueNumber")]
            public string QueueNumber;

            [JsonProperty("startTime")]
            public string StartTime;

            [JsonProperty("endTime")]
            public string EndTime;

            [JsonProperty("waitingNumber")]
            public int WaitingNumber;

            [JsonProperty("statusID")]
            public int StatusID;
        }

        public class Root
        {
            [JsonProperty("metaData")]
            public MetaData MetaData;

            [JsonProperty("data")]
            public List<Datum> Data;
        }
    }
}