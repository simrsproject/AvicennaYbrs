using System;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using Temiang.Avicenna.Bridging.Antrean.ResponseClass;
using Temiang.Avicenna.Bridging.Antrean.ParameterClass;
using Temiang.Avicenna.Bridging.Base;
using Temiang.Avicenna.BusinessObject;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;
using Telerik.Web.UI;
using System.Web;
using Temiang.Avicenna.Common;
using System.Security.Cryptography;
using System.Text;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Bridging
{
    /// <summary>
    /// Bridging RS - BPJS
    /// </summary>
    /// Nama Method berdasarkan dokumentasi yg diberikan
    /// Nama url berdasarkan permintaan BPS spt dibawah
    /// GET Token : http://rs.com/antrol_bpjs/gettoken
    /// GET Antrian : http://rs.com/antrol_bpjs/antrian
    /// GET Rekap Antrian : http://rs.com/antrol_bpjs/rekap
    /// GET Booking Operasi : http://rs.com/antrol_bpjs/antrianOperasi
    /// GET Jadwal Operasi : http://rs.com/antrol_bpjs/rekapOperasi
    public class AntreanController : ApiController
    {

        [HttpPost]
        [Route("antrol_bpjs/auth")]
        public HttpResponseMessage GetToken(TokenParam parVal)
        {
            var metadata = new MetaData();

            string tokenSource;
            if (AuthenticationMiddleware.ValidateUser(parVal.Username, parVal.Password))
            {
                tokenSource = string.Format("{0}:{1}:{2}:{3}:{4}", AuthenticationMiddleware.GetUnixTimeStamp(0), parVal.Username, parVal.Password, AuthenticationMiddleware.GetUnixTimeStamp(10), AuthenticationMiddleware.GetXSignature(parVal.Username, parVal.Password));
                var responseOk = new
                {
                    response = new
                    {
                        token = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(tokenSource))
                    },
                    metadata = metadata
                };
                return Request.CreateResponse(HttpStatusCode.OK, responseOk);
            }

            //Fail
            var responseFail = new
            {
                response = new
                {
                    token = string.Empty
                },
                metadata = new Base.MetaData("403", "authentication failed")
            };
            return Request.CreateResponse(HttpStatusCode.Forbidden, responseFail);
        }

        protected WebServiceAPILog LogAdd()
        {
            var log = new WebServiceAPILog();
            log.AddNew();
            log.DateRequest = DateTime.Now;
            log.UrlAddress = HttpContext.Current.Request.Url.PathAndQuery;
            log.IPAddress = Helper.GetIP4Address();

            string pars = string.Empty;
            string[] keys = HttpContext.Current.Request.Form.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                if (pars.Length > 0) pars += "&";
                pars += string.Format("{0}={1}", keys[i], HttpContext.Current.Request.Form[keys[i]]);
            }
            log.Params = pars;
            log.Save();
            return log;
        }

        protected void LogSave(WebServiceAPILog log, object Response)
        {
            log.Response = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(Response);
            log.Totalms = System.Convert.ToInt32((DateTime.Now - log.DateRequest).Value.TotalMilliseconds);
            log.Save();
        }

        protected static string GetErrorMessage(string errorConst)
        {
            var strs = errorConst.Split('|');
            if (strs.Length > 1)
            {
                return strs[1];
            }
            else
            {
                return strs[0];
            }
        }

        //[CustomAuthorization]
        [HttpPost]
        [Route("antrol_bpjs/antrian")]
        public HttpResponseMessage GetNoAntrean(NoAntreanParam parVal)
        {
            //Sample Parameter:
            //{
            //    "nomorkartu": "0000000000123",
            //    "nik": "3506141308950002",
            //    "notelp": "081123456778",
            //    "tanggalperiksa": "2019-12-11",
            //    "kodepoli": "001",
            //    "nomorreferensi": "0001R0040116A000001",
            //    "jenisreferensi": 1,
            //    "jenisrequest": 2,
            //    "polieksekutif": 0
            //}

            //TODO: Define Response value
            //var response = new
            //{
            //    response = new
            //    {
            //        nomorkartu = "0000000000123",
            //        nik = "3506141308950002",
            //        notelp = "081123456778",
            //        tanggalperiksa = "2019-12-11",
            //        kodepoli = "001",
            //        nomorreferensi = "0001R0040116A000001",
            //        jenisreferensi = 1,
            //        jenisrequest = 2,
            //        polieksekutif = 0
            //    },
            //    metadata = new MetaData("200", "OK")
            //};

            var log = LogAdd();

            try
            {
                if (!(new int[] { 1, 2 }).Contains(parVal.JenisRequest))
                {
                    throw new Exception("Invalid request type");
                }
                if (!(new int[] { 1, 2 }).Contains(parVal.JenisReferensi))
                {
                    throw new Exception("Invalid reference type");
                }
                if (parVal.JenisRequest == 1)
                {
                    throw new Exception("Request type [1] not yet available");
                }

                if (string.IsNullOrWhiteSpace(parVal.TanggalPeriksa))
                {
                    throw new Exception("Invalid selected date");
                }

                string format = "yyyy-MM-dd";
                DateTime parsed;
                DateTime.TryParseExact(parVal.TanggalPeriksa, format, null, System.Globalization.DateTimeStyles.None, out parsed);

                if (parsed.Date < DateTime.Now.Date)
                {
                    throw new Exception("Invalid selected date");
                }

                var subColl = new ServiceUnitBridgingCollection();
                if (subColl.LoadByBridgingID(parVal.KodePoli))
                {
                    if (subColl.Count == 1)
                    {
                        if (string.IsNullOrEmpty(parVal.Nomorkartu)) throw new Exception("Invalid card number");
                        else
                        {
                            var svc = new Common.BPJS.VClaim.v11.Service();
                            var response = svc.GetPeserta(Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta, parVal.Nomorkartu, parsed);
                            if (!response.MetaData.IsValid)
                                throw new Exception("Invalid card number, not found");
                        }

                        if (string.IsNullOrEmpty(parVal.NomorReferensi)) throw new Exception("Invalid referrence number");
                        else
                        {
                            var svc = new Common.BPJS.VClaim.v11.Service();
                            var response = svc.GetRujukan(true, parVal.NomorReferensi, Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);
                            if (response.MetaData.IsValid)
                            {
                                DateTime.TryParseExact(response.Response.Rujukan.TglKunjungan, format, null, System.Globalization.DateTimeStyles.None, out var parsedRujukan);
                                parsedRujukan = parsedRujukan.AddDays(90);
                                if (parsed > parsedRujukan)
                                    throw new Exception("Invalid selected date, referrence date expired");
                            }
                            else
                            {
                                response = svc.GetRujukan(true, parVal.NomorReferensi, Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                                if (response.MetaData.IsValid)
                                {
                                    DateTime.TryParseExact(response.Response.Rujukan.TglKunjungan, format, null, System.Globalization.DateTimeStyles.None, out var parsedRujukan);
                                    parsedRujukan = parsedRujukan.AddDays(90);
                                    if (parsed > parsedRujukan)
                                        throw new Exception("Invalid selected date, referrence date expired");
                                }
                                else
                                    throw new Exception("Invalid referrence number");
                            }
                        }

                        var pat = new Patient();
                        pat.SetEmpty();
                        pat.Ssn = parVal.Nik;
                        pat.PhoneNo = parVal.NoTelp;

                        pat.GetpatientByGuarantorCardNo(parVal.Nomorkartu, AppSession.Parameter.GuarantorTypeBPJS);

                        var wak = new WebServiceAccessKey();
                        if (wak.LoadByPrimaryKey(HttpContext.Current.User.Identity.Name))
                        {
                            //
                            var ws = new Temiang.Avicenna.WebService.V1_1.AppointmentWS();
                            var aptNoWithIdent = ws.AppointmentCreateByPoliBPJS(wak.AccessKey, subColl.First().ServiceUnitID, parVal.TanggalPeriksa,
                                pat.PatientID, pat.FirstName, pat.MiddleName, pat.LastName, pat.DateOfBirth.Value.ToString("yyyy-MM-dd"),
                                pat.StreetName, pat.District, pat.County,
                                pat.City, pat.State, pat.ZipCode, pat.PhoneNo, pat.Email, pat.GuarantorID, pat.Notes,
                                pat.CityOfBirth, pat.Sex, pat.Ssn, pat.MobilePhoneNo,
                                parVal.Nomorkartu, parVal.NomorReferensi, parVal.JenisReferensi);

                            var aptNo = aptNoWithIdent.Split('|')[0];

                            var apt = new BusinessObject.Appointment();
                            apt.LoadByPrimaryKey(aptNo);
                            var su = new ServiceUnit();
                            su.LoadByPrimaryKey(apt.ServiceUnitID);
                            var par = new Paramedic();
                            par.LoadByPrimaryKey(apt.ParamedicID);

                            if (aptNoWithIdent.Split('|')[1] == "new")
                            {
                                var response = new
                                {
                                    response = new
                                    {
                                        nomorantrean = apt.AppointmentQue.ToString(),
                                        kodebooking = apt.AppointmentNo,
                                        jenisantrean = parVal.JenisRequest,
                                        estimasidilayani = new DateTimeOffset(
                                            apt.AppointmentDate.Value
                                                .Add(TimeSpan.Parse(apt.AppointmentTime))).ToUnixTimeMilliseconds(),
                                        namapoli = subColl.First().BridgingName,
                                        namadokter = par.ParamedicName
                                    },
                                    metadata = new MetaData()
                                };
                                LogSave(log, response);
                                return Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                            else
                            {
                                var response = new
                                {
                                    response = new
                                    {
                                        nomorantrean = apt.AppointmentQue.ToString(),
                                        kodebooking = apt.AppointmentNo,
                                        jenisantrean = parVal.JenisRequest,
                                        estimasidilayani = new DateTimeOffset(
                                            apt.AppointmentDate.Value
                                                .Add(TimeSpan.Parse(apt.AppointmentTime))).ToUnixTimeMilliseconds(),
                                        namapoli = subColl.First().BridgingName,
                                        namadokter = par.ParamedicName
                                    },
                                    metadata = new MetaData("201", "Queue numbers can only be retrieved once")
                                };
                                LogSave(log, response);
                                return Request.CreateResponse(HttpStatusCode.Created, response);
                            }
                        }
                        else
                        {
                            throw new Exception("Invalid key access");
                        }
                    }
                    else
                    {
                        throw new Exception(string.Format("Invalid bridging configuration, ambiguous service unit {0}", parVal.KodePoli));
                    }
                }
                else
                {
                    throw new Exception(string.Format("Invalid bridging configuration, service unit {0} not found", parVal.KodePoli));
                }
            }
            catch (Exception ex)
            {
                var response = new
                {
                    metadata = new MetaData("201", GetErrorMessage(ex.Message))
                };

                LogSave(log, response);

                return Request.CreateResponse(HttpStatusCode.Created, response);
            }
        }

        //[Authorize(Roles = "SuperAdmin, Admin, User")]
        //[CustomAuthorization]
        [HttpPost]
        [Route("antrol_bpjs/rekap")]
        public HttpResponseMessage GetRekapAntrean(RekapAntreanParam parVal)
        {
            //Sample Parameter:
            //{
            //    "tanggalperiksa": "2019-12-11",
            //    "kodepoli": "001",
            //    "polieksekutif": 0
            //}
            //TODO: Define Response value
            var log = LogAdd();

            try
            {
                var subColl = new ServiceUnitBridgingCollection();
                if (subColl.LoadByBridgingID(parVal.kodepoli))
                {
                    var su = new ServiceUnit();
                    if (su.GetByBpjsCode(subColl.First().BridgingID))
                    {
                        var apptColl = new BusinessObject.AppointmentCollection();
                        apptColl.Query.Where(apptColl.Query.AppointmentDate == parVal.tanggalperiksa,
                            apptColl.Query.ServiceUnitID == su.ServiceUnitID,
                            apptColl.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
                        apptColl.LoadAll();

                        var response = new
                        {
                            response = new
                            {
                                namapoli = subColl.First().BridgingName,
                                totalantrean = apptColl.Count,
                                jumlahterlayani = apptColl.Where(a => a.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusClosed).Count(),
                                lastupdate = apptColl.Count == 0 ? new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds() :
                                    new DateTimeOffset(apptColl.OrderByDescending(x => x.LastUpdateDateTime).First().LastUpdateDateTime.Value).ToUnixTimeMilliseconds()
                            },
                            metadata = new MetaData()
                        };
                        LogSave(log, response);
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        throw new Exception("Invalid service unit bridging");
                    }
                }
                else
                {
                    throw new Exception("Invalid service unit bridging");
                }
            }
            catch (Exception ex)
            {
                var response = new
                {
                    metadata = new MetaData("201", GetErrorMessage(ex.Message))
                };
                LogSave(log, response);
                return Request.CreateResponse(HttpStatusCode.Created, response);
            }
        }

        //[Authorize(Roles = "SuperAdmin, Admin, User")]
        //[CustomAuthorization]
        [HttpPost]
        [Route("antrol_bpjs/antrianOperasi")]
        public HttpResponseMessage GetKodeBookingOperasi(KodeBookingOperasiParam parVal)
        {
            var log = LogAdd();
            try
            {
                // cari pasien berdasarkan nomor
                if (string.IsNullOrEmpty(parVal.Nopeserta)) throw new Exception("Invalid card number");
                else
                {
                    var svc = new Common.BPJS.VClaim.v11.Service();
                    var resp = svc.GetPeserta(Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta, parVal.Nopeserta, DateTime.Now.Date);
                    if (!resp.MetaData.IsValid)
                        throw new Exception("Invalid card number, not found.");
                }

                var patColl = new PatientCollection();
                var patq = new PatientQuery("patq");
                var guarq = new GuarantorQuery("guarq");
                var pat = new Patient();

                patq.InnerJoin(guarq).On(patq.GuarantorID == guarq.GuarantorID)
                    .Where(patq.GuarantorCardNo == parVal.Nopeserta, patq.IsActive == true,
                        guarq.IsActive == true, guarq.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS /*bpjs*/
                    );
                if (patColl.Load(patq))
                {
                    pat = patColl.First();
                }
                else if (patColl.Count > 1)
                {
                    throw new Exception("Invalid card number, found multiple record with the same card number");
                }
                else
                {
                    //  cari di registrasi
                    var regColl = new RegistrationCollection();
                    var regq = new RegistrationQuery("regq");
                    guarq = new GuarantorQuery("guarq");
                    regq.InnerJoin(guarq).On(regq.GuarantorID == guarq.GuarantorID)
                        .Where(regq.GuarantorCardNo == parVal.Nopeserta, regq.IsVoid == false,
                            guarq.IsActive == true, guarq.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS /*bpjs*/
                        );

                    if (regColl.Load(regq))
                    {
                        pat.LoadByPrimaryKey(regColl.First().PatientID);
                    }
                    else
                    {
                        throw new Exception("Invalid card number, data not found.");
                    }
                }

                var subColl = new ServiceUnitBookingCollection();
                var subq = new ServiceUnitBookingQuery("subq");
                subq.Where(
                    subq.PatientID == pat.PatientID,
                    subq.IsVoid == false
                ).OrderBy(subq.BookingDateTimeFrom.Descending);
                subColl.Load(subq);

                //TODO: Define Response value
                var kboList = new List<KodeBookingOperasi>();
                foreach (var sub in subColl)
                {
                    // get latest poly by booking date
                    var regColl = new RegistrationCollection();
                    var regq = new RegistrationQuery("regq");
                    guarq = new GuarantorQuery("guarq");
                    // var sbq = new ServiceUnitBridgingQuery("sbq");
                    regq.InnerJoin(guarq).On(regq.GuarantorID == guarq.GuarantorID)
                        .Where(regq.GuarantorCardNo == parVal.Nopeserta, regq.IsVoid == false,
                            guarq.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS /*bpjs*/,
                            regq.RegistrationDate <= sub.BookingDateTimeFrom,
                            regq.SRRegistrationType == "OPR"
                        ).OrderBy(regq.RegistrationDate.Descending);
                    regq.es.Top = 1;
                    var kodePoli = ""; var nmPoli = "";
                    if (regColl.Load(regq))
                    {
                        var sbColl = new ServiceUnitBridgingCollection();
                        if (sbColl.LoadByServiceUnitID(regColl.First().ServiceUnitID))
                        {
                            kodePoli = sbColl.First().BridgingID;
                            nmPoli = sbColl.First().BridgingName;
                        }
                    }
                    kboList.Add(new KodeBookingOperasi()
                    {
                        KodeBooking = sub.BookingNo,
                        TanggalOperasi = sub.BookingDateTimeFrom.Value.ToString("yyyy-MM-dd"),
                        JenisTindakan = sub.Diagnose,
                        KodePoli = kodePoli,
                        NamaPoli = nmPoli,
                        Terlaksana = (sub.RegistrationNo == string.Empty || sub.IsApproved.Value == false) ? 0 : 1
                    });
                }

                //kboList.Add(new KodeBookingOperasi()
                //{
                //    KodeBooking = "123456ZXC",
                //    TanggalOperasi = "2019-12-11",
                //    JenisTindakan = "operasi gigi",
                //    KodePoli = "001",
                //    NamaPoli = "Poli Bedah Mulut",
                //    Terlaksana = 0
                //});

                var response = new
                {
                    response = new
                    {
                        list = kboList.Where(j => j.Terlaksana == 0) // hanya yg blm terlaksana saja
                    },
                    metadata = new MetaData()
                };
                LogSave(log, response);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    metadata = new MetaData("201", GetErrorMessage(ex.Message))
                };
                LogSave(log, response);
                return Request.CreateResponse(HttpStatusCode.Created, response);
            }
        }

        //[Authorize(Roles = "SuperAdmin, Admin, User")]
        //[CustomAuthorization]
        [HttpPost]
        [Route("antrol_bpjs/rekapOperasi")]
        public HttpResponseMessage GetJadwalOperasi(JadwalOperasiParam parVal)
        {
            var log = LogAdd();
            try
            {
                var dateS = DateTime.ParseExact(parVal.TanggalAwal, "yyyy-MM-dd", null);
                var dateE = DateTime.ParseExact(parVal.TanggalAkhir, "yyyy-MM-dd", null);

                if (dateS > dateE)
                {
                    throw new Exception("Start date can not be more than end date");
                }

                var subColl = new ServiceUnitBookingCollection();
                subColl.Query.Where(subColl.Query.BookingDateTimeFrom >= dateS, subColl.Query.BookingDateTimeFrom < dateE.AddDays(1),
                        subColl.Query.IsVoid == false
                    );
                subColl.LoadAll();

                //TODO: Define Response value
                var joList = new List<JadwalOperasi>();
                foreach (var sub in subColl)
                {
                    // get latest poly by booking date
                    var regColl = new RegistrationCollection();
                    var regq = new RegistrationQuery("regq");
                    var guarq = new GuarantorQuery("guarq");
                    regq.InnerJoin(guarq).On(regq.GuarantorID == guarq.GuarantorID)
                        .Where(regq.PatientID == sub.PatientID, regq.IsVoid == false,
                            regq.RegistrationDate <= sub.BookingDateTimeFrom,
                            regq.SRRegistrationType == "OPR"
                        ).OrderBy(regq.RegistrationDate.Descending)
                        .Select(regq, guarq.SRGuarantorType.As("refToPatient_MedicalNo")); /*pake field medicalno aja deh*/
                    regq.es.Top = 1;
                    var kodePoli = ""; var nmPoli = ""; var NoPeserta = "";
                    if (regColl.Load(regq))
                    {
                        var sbColl = new ServiceUnitBridgingCollection();

                        if (sbColl.LoadByServiceUnitID(regColl.First().ServiceUnitID))
                        {
                            kodePoli = sbColl.First().BridgingID;
                            nmPoli = sbColl.First().BridgingName;
                        }
                        NoPeserta = regColl.First().MedicalNo == AppSession.Parameter.GuarantorTypeBPJS /*pinjam field medicalno*/ ? (regColl.First().GuarantorCardNo ?? "") : "";
                    }

                    joList.Add(new JadwalOperasi()
                    {
                        KodeBooking = sub.BookingNo,
                        TanggalOperasi = sub.BookingDateTimeFrom.Value.ToString("yyyy-MM-dd"),
                        JenisTindakan = sub.Diagnose,
                        KodePoli = kodePoli,
                        NamaPoli = nmPoli,
                        Terlaksana = (sub.RegistrationNo == string.Empty || sub.IsApproved.Value == false) ? 0 : 1,
                        NoPeserta = NoPeserta,
                        LastUpdate = new DateTimeOffset(sub.LastUpdateDateTime.Value).ToUnixTimeMilliseconds()
                    });
                }

                var response = new
                {
                    response = new
                    {
                        list = joList.Where(j => j.Terlaksana == 0) // hanya yg blm terlaksana saja
                    },
                    metadata = new MetaData()
                };
                LogSave(log, response);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    metadata = new MetaData("201", GetErrorMessage(ex.Message))
                };
                LogSave(log, response);
                return Request.CreateResponse(HttpStatusCode.Created, response);
            }
        }

        //versi 2.0
        //[CustomAuthorization]
        [HttpPost]
        [Route("antrol_bpjs/statusantrean")]
        public HttpResponseMessage GetStatusAntrean(StatusAntreanParam parVal)
        {
            var log = LogAdd();

            try
            {
                if (string.IsNullOrWhiteSpace(parVal.kodepoli))
                {
                    throw new Exception("Invalid kodepoli");
                }

                if (string.IsNullOrWhiteSpace(parVal.kodedokter))
                {
                    throw new Exception("Invalid kodedokter");
                }

                if (string.IsNullOrWhiteSpace(parVal.tanggalperiksa))
                {
                    throw new Exception("Invalid tanggalperiksa");
                }

                if (string.IsNullOrWhiteSpace(parVal.jampraktek) || !parVal.jampraktek.Contains("-"))
                {
                    throw new Exception("Invalid jampraktek");
                }

                string format = "yyyy-MM-dd";
                DateTime parsed;
                DateTime.TryParseExact(parVal.tanggalperiksa, format, null, System.Globalization.DateTimeStyles.None, out parsed);

                if (parsed.Date < DateTime.Now.Date)
                {
                    throw new Exception("Invalid selected date");
                }

                var subColl = new ServiceUnitBridgingCollection();
                if (subColl.LoadByBridgingID(parVal.kodepoli))
                {
                    if (subColl.Count == 1)
                    {
                        var parColl = new ParamedicBridging();
                        parColl.Query.Where(parColl.Query.SRBridgingType == AppEnum.BridgingType.BPJS, parColl.Query.BridgingID == parVal.kodedokter);
                        if (!parColl.Query.Load())
                        {
                            throw new Exception(string.Format("Invalid kodedokter, {0} not found", parVal.kodedokter));
                        }

                        var wak = new WebServiceAccessKey();
                        if (wak.LoadByPrimaryKey(HttpContext.Current.User.Identity.Name))
                        {
                            var psd = new ParamedicScheduleDate();
                            psd.Query.Where(psd.Query.ServiceUnitID == subColl[0].ServiceUnitID, psd.Query.ParamedicID == parColl.ParamedicID, psd.Query.ScheduleDate == parsed);
                            if (!psd.Query.Load())
                            {
                                LogSave(log, new
                                {
                                    metadata = new MetaData("201", "No schedule.")
                                });
                                return Request.CreateResponse(HttpStatusCode.OK, new
                                {
                                    metadata = new MetaData("201", "No schedule.")
                                });
                            }

                            var aptColl = new BusinessObject.AppointmentCollection();
                            aptColl.Query.Where(aptColl.Query.ServiceUnitID == subColl[0].ServiceUnitID,
                                aptColl.Query.ParamedicID == parColl.ParamedicID,
                                aptColl.Query.AppointmentDate == parsed,
                                aptColl.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
                            aptColl.Query.Load();

                            var su = new ServiceUnit();
                            su.LoadByPrimaryKey(subColl[0].ServiceUnitID);

                            var pm = new Paramedic();
                            pm.LoadByPrimaryKey(parColl.ParamedicID);

                            var response = new
                            {
                                response = new
                                {
                                    namapoli = su.ServiceUnitName,
                                    namadokter = pm.ParamedicName,
                                    totalantrean = aptColl.Count,
                                    sisaantrean = 50 - aptColl.Count(a => a.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusConfirmed), // dipatok 50, krn blm jelas dan tidak ada di applikasi
                                    antreanpanggil = aptColl.Max(a => a.AppointmentQue),
                                    sisakuotajkn = 25 - aptColl.Count(a => a.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusConfirmed && AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)),
                                    kuotajkn = 25,
                                    sisakuotanonjkn = 25 - aptColl.Count(a => a.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusConfirmed && !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)),
                                    kuotanonjkn = 25,
                                    keterangan = string.Empty
                                },
                                metadata = new MetaData()
                            };
                            LogSave(log, response);
                            return Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                        else
                        {
                            throw new Exception("Invalid key access");
                        }
                    }
                    else
                    {
                        throw new Exception(string.Format("Invalid kodepoli, ambiguous {0}", parVal.kodepoli));
                    }
                }
                else
                {
                    throw new Exception(string.Format("Invalid kodepoli, {0} not found", parVal.kodepoli));
                }
            }
            catch (Exception ex)
            {
                var response = new
                {
                    metadata = new MetaData("201", GetErrorMessage(ex.Message))
                };

                LogSave(log, response);

                return Request.CreateResponse(HttpStatusCode.Created, response);
            }
        }

        //[CustomAuthorization]
        [HttpPost]
        [Route("antrol_bpjs/batalantrean")]
        public HttpResponseMessage SetBatalAntrean(BatalAntreanParam parVal)
        {
            var log = LogAdd();

            try
            {
                if (string.IsNullOrWhiteSpace(parVal.kodebooking))
                {
                    throw new Exception("Invalid kodebooking");
                }

                if (string.IsNullOrWhiteSpace(parVal.keterangan))
                {
                    throw new Exception("Invalid keterangan");
                }

                var wak = new WebServiceAccessKey();
                if (wak.LoadByPrimaryKey(HttpContext.Current.User.Identity.Name))
                {
                    var apt = new BusinessObject.Appointment();
                    if (!apt.LoadByPrimaryKey(parVal.kodebooking))
                    {
                        LogSave(log, new
                        {
                            metadata = new MetaData("201", $"invalid kodebooking : {parVal.kodebooking}")
                        });
                        return Request.CreateResponse(HttpStatusCode.OK, new
                        {
                            metadata = new MetaData("201", $"invalid kodebooking : {parVal.kodebooking}")
                        });
                    }

                    apt.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusCancel;
                    apt.Notes = parVal.keterangan;

                    apt.Save();

                    var response = new
                    {
                        metadata = new MetaData()
                    };
                    LogSave(log, response);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    throw new Exception("Invalid key access");
                }
            }
            catch (Exception ex)
            {
                var response = new
                {
                    metadata = new MetaData("201", GetErrorMessage(ex.Message))
                };

                LogSave(log, response);

                return Request.CreateResponse(HttpStatusCode.Created, response);
            }
        }

        //[CustomAuthorization]
        [HttpPost]
        [Route("antrol_bpjs/checkinantrean")]
        public HttpResponseMessage SetCheckIn(CheckInAntreanParam parVal)
        {
            var log = LogAdd();

            try
            {
                if (string.IsNullOrWhiteSpace(parVal.kodebooking))
                {
                    throw new Exception("Invalid kodebooking");
                }

                if (string.IsNullOrWhiteSpace(parVal.waktu))
                {
                    throw new Exception("Invalid keterangan");
                }

                var wak = new WebServiceAccessKey();
                if (wak.LoadByPrimaryKey(HttpContext.Current.User.Identity.Name))
                {
                    var apt = new BusinessObject.Appointment();
                    if (!apt.LoadByPrimaryKey(parVal.kodebooking))
                    {
                        LogSave(log, new
                        {
                            metadata = new MetaData("201", $"invalid kodebooking : {parVal.kodebooking}")
                        });
                        return Request.CreateResponse(HttpStatusCode.OK, new
                        {
                            metadata = new MetaData("201", $"invalid kodebooking : {parVal.kodebooking}")
                        });
                    }
                    apt.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusConfirmed;

                    apt.Save();

                    var response = new
                    {
                        metadata = new MetaData()
                    };
                    LogSave(log, response);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    throw new Exception("Invalid key access");
                }
            }
            catch (Exception ex)
            {
                var response = new
                {
                    metadata = new MetaData("201", GetErrorMessage(ex.Message))
                };

                LogSave(log, response);

                return Request.CreateResponse(HttpStatusCode.Created, response);
            }
        }

        //[CustomAuthorization]
        [HttpPost]
        [Route("antrol_bpjs/pasienbaru")]
        public HttpResponseMessage SetPasienBaru(PasienBaruAntrean parVal)
        {
            var log = LogAdd();

            try
            {
                var wak = new WebServiceAccessKey();
                if (wak.LoadByPrimaryKey(HttpContext.Current.User.Identity.Name))
                {
                    string format = "yyyy-MM-dd";
                    DateTime parsed;
                    DateTime.TryParseExact(parVal.Tanggallahir, format, null, System.Globalization.DateTimeStyles.None, out parsed);

                    var autoNumberLastPID = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PatientID);
                    var autoNumberLastMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);

                    var patient = new Patient()
                    {
                        PatientID = autoNumberLastPID.LastCompleteNumber,
                        MedicalNo = autoNumberLastMRN.LastCompleteNumber,
                        GuarantorID = AppSession.Parameter.GuarantorAskesID[0],
                        GuarantorCardNo = parVal.Nomorkartu,
                        FamilyRegisterNo = parVal.Nomorkk,
                        FirstName = parVal.Nama,
                        Sex = parVal.Jeniskelamin == "L" ? "M" : "F",
                        DateOfBirth = parsed,
                        MobilePhoneNo = parVal.Nohp,
                        StreetName = $"{parVal.Alamat} RT. {parVal.Rt} RW. {parVal.Rw}",
                        District = parVal.Namakec,
                        County = parVal.Namakel,
                        City = parVal.Namadati2,
                        State = parVal.Namaprop
                    };

                    using (var trans = new esTransactionScope())
                    {
                        autoNumberLastPID.Save();
                        autoNumberLastMRN.Save();
                        patient.Save();

                        trans.Complete();
                    }

                    var response = new
                    {
                        response = new
                        {
                            norm = patient.MedicalNo
                        },
                        metadata = new MetaData("200", "Harap datang ke admisi untuk melengkapi data rekam medis")
                    };
                    LogSave(log, response);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    throw new Exception("Invalid key access");
                }
            }
            catch (Exception ex)
            {
                var response = new
                {
                    metadata = new MetaData("201", GetErrorMessage(ex.Message))
                };

                LogSave(log, response);

                return Request.CreateResponse(HttpStatusCode.Created, response);
            }
        }

        //[CustomAuthorization]
        [HttpPost]
        [Route("antrol_bpjs/sisaantrean")]
        public HttpResponseMessage GetSisaAntrean(SisaAntreanParam parVal)
        {
            var log = LogAdd();

            try
            {
                var wak = new WebServiceAccessKey();
                if (wak.LoadByPrimaryKey(HttpContext.Current.User.Identity.Name))
                {
                    var apt = new BusinessObject.Appointment();
                    apt.LoadByPrimaryKey(parVal.kodebooking);

                    var su = new ServiceUnit();
                    su.LoadByPrimaryKey(apt.ServiceUnitID);

                    var pm = new Paramedic();
                    pm.LoadByPrimaryKey(apt.ParamedicID);

                    var aptColl = new BusinessObject.AppointmentCollection();
                    aptColl.Query.Where(aptColl.Query.ServiceUnitID == apt.ServiceUnitID,
                        aptColl.Query.ParamedicID == apt.ParamedicID,
                        aptColl.Query.AppointmentDate == apt.AppointmentDate,
                        aptColl.Query.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusConfirmed,
                        aptColl.Query.AppointmentQue < apt.AppointmentQue);
                    aptColl.Query.Load();

                    var response = new
                    {
                        response = new
                        {
                            nomorantrean = parVal.kodebooking,
                            namapoli = su.ServiceUnitName,
                            namadokter = pm.ParamedicName,
                            sisaantrean = aptColl.Count,
                            antreanpanggil = aptColl.Max(a => a.AppointmentQue),
                            waktutunggu = aptColl.Count * 6000,
                            keterangan = string.Empty
                        },
                        metadata = new MetaData()
                    };
                    LogSave(log, response);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    throw new Exception("Invalid key access");
                }
            }
            catch (Exception ex)
            {
                var response = new
                {
                    metadata = new MetaData("201", GetErrorMessage(ex.Message))
                };

                LogSave(log, response);

                return Request.CreateResponse(HttpStatusCode.Created, response);
            }
        }

        //[CustomAuthorization]
        [HttpPost]
        [Route("antrol_bpjs/getantrean")]
        public HttpResponseMessage GetAntrean(GetNoAntreanParam parVal)
        {
            var log = LogAdd();

            try
            {
                var wak = new WebServiceAccessKey();
                if (wak.LoadByPrimaryKey(HttpContext.Current.User.Identity.Name))
                {
                    var subColl = new ServiceUnitBridgingCollection();
                    if (subColl.LoadByBridgingID(parVal.Kodepoli))
                    {
                        if (subColl.Count > 1)
                        {
                            throw new Exception(string.Format("Invalid kodepoli, ambiguous {0}", parVal.Kodepoli));
                        }
                    }

                    var parColl = new ParamedicBridging();
                    parColl.Query.Where(parColl.Query.SRBridgingType == AppEnum.BridgingType.BPJS, parColl.Query.BridgingID == parVal.Kodedokter);
                    if (!parColl.Query.Load())
                    {
                        throw new Exception(string.Format("Invalid kodedokter, {0} not found", parVal.Kodedokter));
                    }

                    string format = "yyyy-MM-dd";
                    DateTime parsed;
                    DateTime.TryParseExact(parVal.Tanggalperiksa, format, null, System.Globalization.DateTimeStyles.None, out parsed);

                    var apt = new BusinessObject.Appointment();
                    apt.Query.Where(
                        apt.Query.AppointmentDate == parsed,
                        apt.Query.GuarantorCardNo == parVal.Nomorkartu,
                        apt.Query.ReferenceNumber == parVal.Nomorreferensi,
                        apt.Query.Ssn == parVal.Nik,
                        apt.ParamedicID = parColl.ParamedicID,
                        apt.ServiceUnitID = subColl[0].ServiceUnitID
                        );
                    if (!apt.Query.Load())
                    {
                        LogSave(log, new
                        {
                            metadata = new MetaData("201", $"not available")
                        });
                        return Request.CreateResponse(HttpStatusCode.OK, new
                        {
                            metadata = new MetaData("201", $"not available")
                        });
                    }

                    var su = new ServiceUnit();
                    su.LoadByPrimaryKey(apt.ServiceUnitID);

                    var pm = new Paramedic();
                    pm.LoadByPrimaryKey(apt.ParamedicID);

                    var aptColl = new BusinessObject.AppointmentCollection();
                    aptColl.Query.Where(aptColl.Query.ServiceUnitID == apt.ServiceUnitID,
                        aptColl.Query.ParamedicID == apt.ParamedicID,
                        aptColl.Query.AppointmentDate == apt.AppointmentDate,
                        aptColl.Query.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusConfirmed,
                        aptColl.Query.AppointmentQue < apt.AppointmentQue);
                    aptColl.Query.Load();

                    var regColl = new RegistrationCollection();
                    regColl.Query.Where(regColl.Query.PatientID == apt.PatientID, regColl.Query.IsVoid == false);
                    regColl.Query.Load();

                    var time = TimeSpan.Parse(apt.AppointmentTime);

                    var response = new
                    {
                        response = new
                        {
                            nomorantrean = apt.AppointmentQue,
                            angkaantrean = apt.AppointmentQue,
                            kodebooking = apt.AppointmentNo,
                            pasienbaru = string.IsNullOrEmpty(apt.MedicalNo) ? "0" : (regColl.Count > 0 ? "1" : "0"),
                            norm = apt.MedicalNo,
                            namapoli = su.ServiceUnitName,
                            namadokter = pm.ParamedicName, //apt.AppointmentDate + apt.AppointmentTime + (apt.AppointmentQue*apt.VisitDuration)
                            estimasidilayani = ((new DateTime(apt.AppointmentDate.Value.Year, apt.AppointmentDate.Value.Month, apt.AppointmentDate.Value.Day, time.Hours, time.Minutes, 0, DateTimeKind.Utc) - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds - 
                                (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds).ToString(),
                            sisakuotajkn = 25 - aptColl.Count(a => AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)),
                            kuotajkn = 25,
                            sisakuotanonjkn= 25 - aptColl.Count(a => !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)),
                            kuotanonjkn = 25,
                            keterangan = "Peserta harap 60 menit lebih awal guna pencatatan administrasi."
                        },
                        metadata = new MetaData()
                    };
                    LogSave(log, response);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    throw new Exception("Invalid key access");
                }
            }
            catch (Exception ex)
            {
                var response = new
                {
                    metadata = new MetaData("201", GetErrorMessage(ex.Message))
                };

                LogSave(log, response);

                return Request.CreateResponse(HttpStatusCode.Created, response);
            }
        }
    }
}
