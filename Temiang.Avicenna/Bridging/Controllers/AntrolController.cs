using DevExpress.XtraPrinting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using Telerik.Reporting.Svg.ExCSS;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.Common.BPJS.VClaim.v11;
using Temiang.Dal.Interfaces;
using Paramedic = Temiang.Avicenna.BusinessObject.Paramedic;

namespace Temiang.Avicenna.Bridging.Controllers
{
    public class AntrolController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("antrol/auth")]
        public HttpResponseMessage GetToken()
        {
            if (!Request.Headers.TryGetValues("x-username", out var username) || !Request.Headers.TryGetValues("x-password", out var password))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    metadata = new
                    {
                        code = (int)HttpStatusCode.BadRequest,
                        message = "Bad Request"
                    }
                });
            }

            string tokenSource;
            if (AuthenticationMiddleware.ValidateUser(username.Single(), password.Single()))
            {
                tokenSource = string.Format("{0}:{1}:{2}:{3}:{4}", AuthenticationMiddleware.GetUnixTimeStamp(0), username.Single(), password.Single(), AuthenticationMiddleware.GetUnixTimeStamp(10), AuthenticationMiddleware.GetXSignature(username.Single(), password.Single()));
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    metadata = new
                    {
                        code = (int)HttpStatusCode.OK,
                        message = "Sukses"
                    },
                    response = new
                    {
                        token = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(tokenSource))
                    }
                });
            }

            return Request.CreateResponse(HttpStatusCode.Created, new
            {
                metadata = new
                {
                    code = (int)HttpStatusCode.Created,
                    message = "Username atau Password Tidak Sesuai"
                }
            });
        }

        //[CustomAuthorization]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("antrol/statusantrean")]
        public HttpResponseMessage StatusAntrean(Antrol.StatusAntrean.Request.Root param)
        {
            {
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "10.200.200.188",
                    UrlAddress = "StatusAntrean",
                    Params = JsonConvert.SerializeObject(param),
                    Response = string.Empty,
                    Totalms = 0
                };
                log.Save();
            }

            var poli = new ServiceUnitBridging();
            poli.Query.Where(poli.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
            poli.Query.Where($"< SUBSTRING(BridgingID, CHARINDEX(';', BridgingID) + 1, 3) = '{param.Kodepoli}'>");
            if (!poli.Query.Load())
            {
                poli = new ServiceUnitBridging();
                poli.Query.Where(poli.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                poli.Query.Where($"< SUBSTRING(BridgingID, 0, CHARINDEX(';', BridgingID)) = '{param.Kodepoli}'>");
                if (!poli.Query.Load())
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        metadata = new
                        {
                            Code = (int)HttpStatusCode.NotFound, // 404
                            Message = $"Kode Poli Tidak Ditemukan"
                        }
                    });
                }
            }

            var su = new ServiceUnit();
            su.LoadByPrimaryKey(poli.ServiceUnitID);

            var dokter = new ParamedicBridging();
            dokter.Query.Where(dokter.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString() && dokter.Query.BridgingID == param.Kodedokter);
            if (!dokter.Query.Load())
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.StatusAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Dokter Tidak Ditemukan"
                    }
                });
            }

            string format = "yyyy-MM-dd";
            if (!DateTime.TryParseExact(param.Tanggalperiksa, format, null, System.Globalization.DateTimeStyles.None, out var parsed))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.StatusAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Format Tanggal Tidak Sesuai, format yang benar adalah yyyy-mm-dd"
                    }
                });
            }

            if (parsed.Date < DateTime.Now.Date)
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.StatusAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Tanggal Periksa Tidak Berlaku"
                    }
                });
            }

            var p = new Paramedic();
            p.LoadByPrimaryKey(dokter.ParamedicID);

            var svc = new Common.BPJS.Antrian.Service();
            var jadwal = svc.GetJadwalDokter(param.Kodepoli, param.Tanggalperiksa);
            if (!jadwal.Metadata.IsAntrolValid)
            {
                svc = new Common.BPJS.Antrian.Service();
                jadwal = svc.GetJadwalDokter(poli.BridgingID.Split(';')[0], param.Tanggalperiksa);
                if (!jadwal.Metadata.IsAntrolValid)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.StatusAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message =
                                $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                        }
                    });
                }
            }

            var ps = new ParamedicSchedule();
            if (!ps.LoadByPrimaryKey(su.ServiceUnitID, p.ParamedicID, parsed.Year.ToString()))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.StatusAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                    }
                });
            }

            var jam = param.Jampraktek.Split('-');

            var appt = new AppointmentCollection();
            appt.Query.Where(appt.Query.ServiceUnitID == poli.ServiceUnitID,
                appt.Query.ParamedicID == dokter.ParamedicID,
                //appt.Query.AppointmentDate.Date() == parsed.Date,
                appt.Query.AppointmentDate >= parsed.Date, appt.Query.AppointmentDate < parsed.Date.AddDays(1),
                appt.Query.AppointmentTime >= jam[0].Trim(),
                appt.Query.AppointmentTime <= jam[1].Trim()
                );
            //if (AppSession.Parameter.HealthcareID != "RSES") // kerinci
            appt.Query.Where(appt.Query.SRAppointmentStatus.NotIn(AppSession.Parameter.AppointmentStatusCancel));
            var apptAvailable = appt.Query.Load();

            return Request.CreateResponse(HttpStatusCode.OK, new Antrol.StatusAntrean.Response.Root
            {
                Response = new Antrol.StatusAntrean.Response.TResponse()
                {
                    Namapoli = su.ServiceUnitName,
                    Namadokter = p.ParamedicName,
                    Totalantrean = apptAvailable ? appt.Count() : 0,
                    Sisaantrean = ((ps.QuotaBpjsOnline ?? 0) + (ps.QuotaOnline ?? 0)) - (apptAvailable ? appt.Count() : 0),
                    Antreanpanggil = apptAvailable ? appt.Where(a => !new string[] { AppSession.Parameter.AppointmentStatusCancel }.Contains(a.SRAppointmentStatus)).OrderBy(a => a.AppointmentQue).Select(a => a.AppointmentNo.ToString()).Take(1).Single() : "-",
                    Sisakuotajkn = (ps.QuotaBpjsOnline ?? 0) - (apptAvailable ? appt.Count(a => AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) : 0) <= 0 ? 0 : (ps.QuotaBpjsOnline ?? 0) - (apptAvailable ? appt.Count(a => AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) : 0),
                    Kuotajkn = ps.QuotaBpjsOnline ?? 0,
                    Sisakuotanonjkn = (ps.QuotaOnline ?? 0) - (apptAvailable ? appt.Count(a => !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) : 0) <= 0 ? 0 : (ps.QuotaOnline ?? 0) - (apptAvailable ? appt.Count(a => !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) : 0),
                    Kuotanonjkn = ps.QuotaOnline ?? 0,
                    Keterangan = string.Empty
                },
                metadata = new Antrol.StatusAntrean.Response.Metadata()
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = $"Sukses"
                }
            });
        }

        [CustomAuthorization]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("antrol/updatewaktutaskid")]
        public HttpResponseMessage UpdateWaktuTaskId(Common.BPJS.Antrian.Update.WaktuAntrianManual.Request.Root param)
        {
            //string format = "yyyy-MM-dd HH:mm:ss";
            //DateTime.TryParseExact(param.Waktu, format, null, System.Globalization.DateTimeStyles.None, out var parsed);

            //var waktu = Convert.ToInt64(parsed.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);

            var svcAntrol = new Common.BPJS.Antrian.Service();
            var responseAntrol = svcAntrol.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
            {
                Kodebooking = param.Kodebooking,
                Taskid = param.Taskid,
                Waktu = param.Waktu.ToInt()
            });

            {
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "10.200.200.188",
                    UrlAddress = "UpdateWaktuTaskId",
                    Params = JsonConvert.SerializeObject(param),
                    Response = JsonConvert.SerializeObject(responseAntrol),
                    Totalms = 0
                };
                log.Save();
            }

            return Request.CreateResponse(HttpStatusCode.OK, responseAntrol);
        }

        [CustomAuthorization]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("antrol/listwaktutaskid")]
        public HttpResponseMessage ListWaktuTaskId(Common.BPJS.Antrian.List.TaskId.Request.Root param)
        {
            var svcAntrol = new Common.BPJS.Antrian.Service();
            var responseAntrol = svcAntrol.GetListWaktuTaskId(param);

            return Request.CreateResponse(HttpStatusCode.OK, responseAntrol);
        }

        //[CustomAuthorization]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("antrol/ambilantrean")]
        public HttpResponseMessage AmbilAntrean(Antrol.AmbilAntrean.Request.Root param)
        {
            try
            {
                {
                    var log = new WebServiceAPILog
                    {
                        DateRequest = DateTime.Now,
                        IPAddress = "10.200.200.188",
                        UrlAddress = "AmbilAntrean",
                        Params = JsonConvert.SerializeObject(param),
                        Response = string.Empty,
                        Totalms = 0
                    };
                    log.Save();
                }

                if (string.IsNullOrWhiteSpace(param.Nomorkartu))
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Nomor Kartu Belum Diisi"
                        }
                    });
                }

                if (param.Nomorkartu.Length < 13 || param.Nomorkartu.Length > 13)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Format Nomor Kartu Tidak Sesuai"
                        }
                    });
                }

                if (!Common.Helper.IsNumeric(param.Nomorkartu))
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Format Nomor Kartu Tidak Sesuai"
                        }
                    });
                }

                if (string.IsNullOrWhiteSpace(param.Nik))
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"NIK Kartu Belum Diisi"
                        }
                    });
                }

                if (param.Nik.Length < 16 || param.Nik.Length > 16)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Format NIK Tidak Sesuai"
                        }
                    });
                }

                var polis = new ServiceUnitBridgingCollection();
                polis.Query.Where(polis.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                polis.Query.Where($"< SUBSTRING(BridgingID, CHARINDEX(';', BridgingID) + 1, 3) = '{param.Kodepoli}'>");
                if (!polis.Query.Load())
                {
                    polis = new ServiceUnitBridgingCollection();
                    polis.Query.Where(polis.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                    polis.Query.Where($"< SUBSTRING(BridgingID, 0, CHARINDEX(';', BridgingID)) = '{param.Kodepoli}'>");
                    if (!polis.Query.Load())
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, new
                        {
                            metadata = new
                            {
                                Code = (int)HttpStatusCode.NotFound, // 404
                                Message = $"Kode Poli {param.Kodepoli} Tidak Ditemukan"
                            }
                        });
                    }
                }

                var dokter = new ParamedicBridging();
                dokter.Query.Where(dokter.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString() && dokter.Query.BridgingID == param.Kodedokter);
                if (!dokter.Query.Load())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Dokter Tidak Ditemukan"
                        }
                    });
                }

                string format = "yyyy-MM-dd";
                if (!DateTime.TryParseExact(param.Tanggalperiksa, format, null, System.Globalization.DateTimeStyles.None, out var parsed))
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Format Tanggal Tidak Sesuai, format yang benar adalah yyyy-mm-dd"
                        }
                    });
                }

                if (parsed.Date < DateTime.Now.Date)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Tanggal Periksa Tidak Berlaku"
                        }
                    });
                }

                if (string.IsNullOrWhiteSpace(param.Nomorreferensi))
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"No Referensi Tidak Boleh Kosong"
                        }
                    });
                }

                //var minDay = 0;
                //if (AppSession.Parameter.HealthcareInitial == "RSI") minDay = 1;
                //if (AppSession.Parameter.MinDayBookingMJkn > -1)
                //    minDay = AppSession.Parameter.MinDayBookingMJkn ?? 0;

                //var maxDay = 365;
                ////if (AppSession.Parameter.HealthcareInitial == "RSGPI") maxDay = 7;
                ////if (AppSession.Parameter.HealthcareInitial == "RSI") maxDay = 1;
                ////else
                //if (AppSession.Parameter.HealthcareInitial == "RSSTJ") maxDay = 8;
                //else if (AppSession.Parameter.HealthcareInitial == "RSIMT") maxDay = 90;
                //else if (AppSession.Parameter.HealthcareInitial == "RSYS") maxDay = 14;
                //if (AppSession.Parameter.MaxDayBookingMJkn > -1)
                //    maxDay = AppSession.Parameter.MaxDayBookingMJkn ?? 0;

                //var minDate = parsed.Date.AddDays(-1 * minDay);
                //var maxDate = DateTime.Now.Date.AddDays(maxDay);

                //if (minDay > 0 && DateTime.Now.Date < minDate)
                //{
                //    return Request.CreateResponse(HttpStatusCode.Created, new
                //    {
                //        metadata = new Antrol.AmbilAntrean.Response.Metadata()
                //        {
                //            Code = (int)HttpStatusCode.Created,
                //            Message = $"Booking dapat dilakukan minimal {minDay} hari sebelumnya"
                //        }
                //    });
                //}
                //else if (parsed.Date >= minDate && parsed.Date <= maxDate)
                //{
                //    // ok
                //}
                //else if (parsed.Date > maxDate)
                //{
                //    return Request.CreateResponse(HttpStatusCode.Created, new
                //    {
                //        metadata = new Antrol.AmbilAntrean.Response.Metadata()
                //        {
                //            Code = (int)HttpStatusCode.Created,
                //            Message = $"Booking dapat dilakukan maksimal {maxDay} hari berikutnya"
                //        }
                //    });
                //}

                var minDayBefore = 0;
                if (AppSession.Parameter.HealthcareInitial == "RSI") minDayBefore = 1;
                if (AppSession.Parameter.MinDayBeforeBookingMJkn > -1) minDayBefore = AppSession.Parameter.MinDayBeforeBookingMJkn ?? 0;
                var tglMaxRencana = parsed.Date.AddDays(-1 * minDayBefore);

                var maxDayBefore = 365;
                if (AppSession.Parameter.MaxDayBeforeBookingMJkn > -1) maxDayBefore = AppSession.Parameter.MaxDayBeforeBookingMJkn ?? 0;
                var tglMinRencana = parsed.Date.AddDays(-1 * maxDayBefore);

                var minDayAfter = 365;
                if (AppSession.Parameter.HealthcareInitial == "RSSTJ") minDayAfter = 8;
                else if (AppSession.Parameter.HealthcareInitial == "RSIMT") minDayAfter = 90;
                else if (AppSession.Parameter.HealthcareInitial == "RSYS") minDayAfter = 14;
                if (AppSession.Parameter.MinDayAfterBookingMJkn > -1) minDayAfter = AppSession.Parameter.MinDayAfterBookingMJkn ?? 0;

                if (DateTime.Now.Date < tglMinRencana.Date)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Booking dapat dilakukan minimal {maxDayBefore} hari sebelumnya"
                        }
                    });
                }
                if (minDayBefore > 0)
                {
                    if (DateTime.Now.Date > tglMaxRencana.Date)
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, new
                        {
                            metadata = new Antrol.AmbilAntrean.Response.Metadata()
                            {
                                Code = (int)HttpStatusCode.Created,
                                Message = $"Booking dapat dilakukan maksimal {minDayBefore} hari sebelumnya"
                            }
                        });
                    }
                }

                var p = new Paramedic();
                p.LoadByPrimaryKey(dokter.ParamedicID);

                var su = new BusinessObject.ServiceUnit();
                var ps = new ParamedicSchedule();
                var psd = new ParamedicScheduleDate();

                var jam = param.Jampraktek.Split('-');
                var jam1 = TimeSpan.ParseExact(jam[0].Trim(), "hh\\:mm", null);
                var jam2 = TimeSpan.ParseExact(jam[1].Trim(), "hh\\:mm", null);
                TimeSpan? waktu1 = null, waktu2 = null;

                var ot = new OperationalTime();

                foreach (var poli in polis)
                {
                    psd = new ParamedicScheduleDate();
                    if (!psd.LoadByPrimaryKey(poli.ServiceUnitID, p.ParamedicID, parsed.Year.ToString(), parsed.Date)) polis.DetachEntity(poli);
                }

                var isjadwal = false;
                foreach (var poli in polis)
                {
                    su = new BusinessObject.ServiceUnit();
                    su.LoadByPrimaryKey(poli.ServiceUnitID);

                    ps = new ParamedicSchedule();
                    if (!ps.LoadByPrimaryKey(su.ServiceUnitID, p.ParamedicID, parsed.Year.ToString())) continue;
                    if (ps.QuotaBpjsOnline == 0) continue;

                    psd = new ParamedicScheduleDate();
                    if (!psd.LoadByPrimaryKey(su.ServiceUnitID, p.ParamedicID, parsed.Year.ToString(), parsed.Date)) continue;

                    ot = new OperationalTime();
                    if (!ot.LoadByPrimaryKey(psd.OperationalTimeID)) continue;

                    if (!string.IsNullOrWhiteSpace(ot.StartTime1) && !string.IsNullOrWhiteSpace(ot.EndTime1))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime1, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime1, "hh\\:mm", null);

                        if (jam1 == ot1 && jam2 == ot2)
                        {
                            waktu1 = ot1;
                            waktu2 = ot2;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime2, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime2, "hh\\:mm", null);

                        if (jam1 == ot1 && jam2 == ot2)
                        {
                            waktu1 = ot1;
                            waktu2 = ot2;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime3, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime3, "hh\\:mm", null);

                        if (jam1 == ot1 && jam2 == ot2)
                        {
                            waktu1 = ot1;
                            waktu2 = ot2;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime4, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime4, "hh\\:mm", null);

                        if (jam1 == ot1 && jam2 == ot2)
                        {
                            waktu1 = ot1;
                            waktu2 = ot2;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime5, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime5, "hh\\:mm", null);

                        if (jam1 == ot1 && jam2 == ot2)
                        {
                            waktu1 = ot1;
                            waktu2 = ot2;
                        }
                    }

                    if (waktu1 != null && waktu2 != null) isjadwal = true;
                    if ($"{jam1.ToString("hh\\:mm")}-{jam2.ToString("hh\\:mm")}" == $"{waktu1?.ToString("hh\\:mm")}-{waktu2?.ToString("hh\\:mm")}") isjadwal = true;
                    if (isjadwal) break;
                }

                if (!isjadwal)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                        }
                    });
                }

                var svc = new Common.BPJS.Antrian.Service();
                //var jadwal = svc.GetJadwalDokter(param.Kodepoli, param.Tanggalperiksa);
                //if (!jadwal.Metadata.IsAntrolValid)
                //{
                //    var jadwalexist = false;
                //    foreach (var poli in polis)
                //    {
                //        svc = new Common.BPJS.Antrian.Service();
                //        jadwal = svc.GetJadwalDokter(poli.BridgingID.Split(';')[0], param.Tanggalperiksa);
                //        if (jadwal.Metadata.IsAntrolValid)
                //        {
                //            jadwalexist = true;

                //            var day = 0;
                //            if (parsed.DayOfWeek == DayOfWeek.Sunday) day = 7;
                //            else day = (int)parsed.DayOfWeek;

                //            if (jadwal.Response.List == null && !jadwal.Response.List.Any())
                //            {
                //                return Request.CreateResponse(HttpStatusCode.Created, new
                //                {
                //                    metadata = new Antrol.AmbilAntrean.Response.Metadata()
                //                    {
                //                        Code = (int)HttpStatusCode.Created,
                //                        Message = $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                //                    }
                //                });
                //            }
                //            break;
                //        }
                //    }
                //    if (!jadwalexist)
                //    {
                //        return Request.CreateResponse(HttpStatusCode.Created, new
                //        {
                //            metadata = new Antrol.AmbilAntrean.Response.Metadata()
                //            {
                //                Code = (int)HttpStatusCode.Created,
                //                Message =
                //                    $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                //            }
                //        });
                //    }
                //}
                //else
                //{
                //    var day = 0;
                //    if (parsed.DayOfWeek == DayOfWeek.Sunday) day = 7;
                //    else day = (int)parsed.DayOfWeek;

                //    if (jadwal.Response.List == null && !jadwal.Response.List.Any())
                //    {
                //        return Request.CreateResponse(HttpStatusCode.Created, new
                //        {
                //            metadata = new Antrol.AmbilAntrean.Response.Metadata()
                //            {
                //                Code = (int)HttpStatusCode.Created,
                //                Message = $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                //            }
                //        });
                //    }
                //}

                if (parsed.Date == DateTime.Now.Date)
                {
                    if (DateTime.Now.TimeOfDay > jam2)
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, new
                        {
                            metadata = new Antrol.AmbilAntrean.Response.Metadata()
                            {
                                Code = (int)HttpStatusCode.Created,
                                Message = $"Pendaftaran Ke Poli {su.ServiceUnitName} Sudah Tutup Jam {jam2.ToString("hh\\:mm")}"
                            }
                        });
                    }
                }

                ps = new ParamedicSchedule();
                if (!ps.LoadByPrimaryKey(su.ServiceUnitID, p.ParamedicID, parsed.Year.ToString()))
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                        }
                    });
                }
                else
                {
                    if ((ps.QuotaBpjsOnline ?? 0) == 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, new
                        {
                            metadata = new Antrol.AmbilAntrean.Response.Metadata()
                            {
                                Code = (int)HttpStatusCode.Created,
                                Message = $"Kapasitas antrian sudah penuh"
                            }
                        });
                    }
                    else
                    {
                        var appt = new AppointmentCollection();
                        appt.Query.Where(appt.Query.ServiceUnitID == su.ServiceUnitID,
                            appt.Query.ParamedicID == dokter.ParamedicID,
                            //appt.Query.AppointmentDate.Date() == parsed.Date,
                            appt.Query.AppointmentDate >= parsed.Date, appt.Query.AppointmentDate < parsed.Date.AddDays(1),
                            appt.Query.AppointmentTime >= jam[0].Trim(),
                            appt.Query.AppointmentTime <= jam[1].Trim()
                            );
                        if (AppSession.Parameter.HealthcareID != "RSES") // kerinci
                            appt.Query.Where(appt.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
                        appt.Query.Load();
                        if (appt.Count >= (ps.QuotaBpjsOnline ?? 0))
                        {
                            return Request.CreateResponse(HttpStatusCode.Created, new
                            {
                                metadata = new Antrol.AmbilAntrean.Response.Metadata()
                                {
                                    Code = (int)HttpStatusCode.Created,
                                    Message = $"Kapasitas antrian sudah penuh"
                                }
                            });
                        }
                    }
                }

                var pasienBaru = string.IsNullOrWhiteSpace(param.Norm);

                var pasien = new Patient();
                if (pasienBaru)
                {
                    pasien.Query.es.Top = 1;
                    pasien.Query.Where(pasien.Query.Ssn == param.Nik);
                    pasien.Query.OrderBy(pasien.Query.LastUpdateDateTime.Descending);
                    pasienBaru = !pasien.Query.Load();
                    if (pasienBaru)
                    {
                        pasien = new Patient();
                        pasien.Query.es.Top = 1;
                        pasien.Query.Where(pasien.Query.GuarantorCardNo == param.Nomorkartu);
                        pasien.Query.OrderBy(pasien.Query.LastUpdateDateTime.Descending);
                        pasienBaru = !pasien.Query.Load();
                    }
                }
                else
                {
                    var norm = param.Norm.Replace("-", string.Empty);

                    pasien = new Patient();
                    pasien.Query.es.Top = 1;
                    pasien.Query.Where($"< REPLACE(MedicalNo, '-', '') = '{norm}'>");
                    pasien.Query.OrderBy(pasien.Query.LastUpdateDateTime.Descending);
                    pasienBaru = !pasien.Query.Load();
                    if (pasienBaru)
                    {
                        pasien = new Patient();
                        pasien.Query.es.Top = 1;
                        pasien.Query.Where(pasien.Query.OldMedicalNo == norm);
                        pasien.Query.OrderBy(pasien.Query.LastUpdateDateTime.Descending);
                        pasienBaru = !pasien.Query.Load();
                        if (pasienBaru)
                        {

                            pasien = new Patient();
                            pasien.Query.es.Top = 1;
                            pasien.Query.Where(pasien.Query.Ssn == param.Nik);
                            pasien.Query.OrderBy(pasien.Query.LastUpdateDateTime.Descending);
                            pasienBaru = !pasien.Query.Load();
                            if (pasienBaru)
                            {
                                pasien = new Patient();
                                pasien.Query.es.Top = 1;
                                pasien.Query.Where(pasien.Query.GuarantorCardNo == param.Nomorkartu);
                                pasien.Query.OrderBy(pasien.Query.LastUpdateDateTime.Descending);
                                pasienBaru = !pasien.Query.Load();
                            }
                        }
                    }
                }

                if (pasienBaru)
                {
                    if (new string[] { "RSARS", "RSIAM" }.Contains(AppSession.Parameter.HealthcareInitial))
                        return Request.CreateResponse(HttpStatusCode.ResetContent, new
                        {
                            metadata = new Antrol.AmbilAntrean.Response.Metadata()
                            {
                                Code = (int)HttpStatusCode.ResetContent,
                                Message = $"Data pasien ini tidak ditemukan, silahkan Melakukan Registrasi Pasien Baru"
                            }
                        });
                    else
                        return Request.CreateResponse(HttpStatusCode.Accepted, new
                        {
                            metadata = new Antrol.AmbilAntrean.Response.Metadata()
                            {
                                Code = (int)HttpStatusCode.Accepted,
                                Message = $"Data pasien ini tidak ditemukan, silahkan Melakukan Registrasi Pasien Baru"
                            }
                        });
                }

                var contact = new PatientEmergencyContact();

                if (pasienBaru)
                {
                    pasien = new Patient();
                    pasien.Ssn = param.Nik;
                    pasien.SRSalutation = string.Empty;
                    pasien.FirstName = string.Empty;
                    pasien.MiddleName = string.Empty;
                    pasien.LastName = string.Empty;
                    pasien.ParentSpouseName = string.Empty;
                    pasien.CityOfBirth = string.Empty;
                    pasien.DateOfBirth = new DateTime(2000, 1, 1);
                    pasien.Sex = "M";
                    pasien.SRBloodType = string.Empty;
                    pasien.BloodRhesus = string.Empty;
                    pasien.SREthnic = string.Empty;
                    pasien.SREducation = string.Empty;
                    pasien.SRMaritalStatus = string.Empty;
                    pasien.SRNationality = string.Empty;
                    pasien.SROccupation = string.Empty;
                    pasien.SRTitle = string.Empty;
                    pasien.SRPatientCategory = string.Empty;
                    pasien.SRReligion = string.Empty;
                    pasien.SRMedicalFileBin = string.Empty;
                    pasien.SRMedicalFileStatus = string.Empty;
                    pasien.GuarantorID = AppSession.Parameter.GuarantorAskesID[0];
                    pasien.Company = string.Empty;
                    pasien.StreetName = string.Empty;
                    pasien.District = string.Empty;
                    pasien.City = string.Empty;
                    pasien.County = string.Empty;
                    pasien.State = string.Empty;
                    pasien.str.ZipCode = string.Empty;
                    pasien.PhoneNo = string.Empty;
                    pasien.FaxNo = string.Empty;
                    pasien.Email = string.Empty;
                    pasien.MobilePhoneNo = string.IsNullOrWhiteSpace(param.Nohp) ? string.Empty : param.Nohp;
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
                    pasien.LastUpdateByUserID = "mobilejkn";
                    pasien.PackageBalance = 0;
                    pasien.HealthcareID = AppSession.Parameter.HealthcareID;
                    pasien.str.ResponTime = string.Empty;
                    pasien.SRInformationFrom = string.Empty;
                    pasien.SRPatienRelation = string.Empty;
                    pasien.PersonID = 0;
                    pasien.EmployeeNumber = string.Empty;
                    pasien.SREmployeeRelationship = string.Empty;
                    pasien.GuarantorCardNo = param.Nomorkartu;
                    pasien.IsNonPatient = false;
                    pasien.ParentSpouseAge = 0;
                    pasien.SRParentSpouseOccupation = string.Empty;
                    pasien.ParentSpouseOccupationDesc = string.Empty;
                    pasien.SRMotherOccupation = string.Empty;
                    pasien.MotherOccupationDesc = string.Empty;
                    pasien.MotherName = string.Empty;
                    pasien.MotherAge = 0;
                    pasien.IsNotPaidOff = false;
                    pasien.ParentSpouseMedicalNo = string.Empty;
                    pasien.MotherMedicalNo = string.Empty;
                    pasien.CompanyAddress = string.Empty;
                    pasien.CreatedByUserID = "mobilejkn";
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
                    pasien.FamilyRegisterNo = string.Empty;
                    pasien.IsSyncWithDukcapil = false;

                    contact.PatientID = pasien.PatientID;
                    contact.ContactName = string.Empty;
                    contact.SRRelationship = string.Empty;
                    contact.StreetName = string.Empty;
                    contact.District = string.Empty;
                    contact.City = string.Empty;
                    contact.County = string.Empty;
                    contact.State = string.Empty;
                    contact.str.ZipCode = string.Empty;
                    contact.FaxNo = string.Empty;
                    contact.Email = string.Empty;
                    contact.PhoneNo = string.Empty;
                    contact.MobilePhoneNo = string.Empty;
                    contact.Notes = string.Empty;
                    contact.LastUpdateDateTime = DateTime.Now;
                    contact.LastUpdateByUserID = "mobilejkn";
                    contact.SROccupation = string.Empty;
                    contact.Ssn = string.Empty;
                }
                else
                {
                    pasien.GuarantorCardNo = param.Nomorkartu;
                    pasien.Ssn = param.Nik;
                    pasien.MobilePhoneNo = string.IsNullOrWhiteSpace(param.Nohp) ? (string.IsNullOrWhiteSpace(pasien.MobilePhoneNo) ? string.Empty : pasien.MobilePhoneNo) : param.Nohp;
                }

                var antrean = new Appointment();
                antrean.Query.Where(
                    antrean.Query.PatientID == pasien.PatientID,
                    //antrean.Query.AppointmentDate.Date() == parsed.Date
                    antrean.Query.AppointmentDate >= parsed.Date, antrean.Query.AppointmentDate < parsed.Date.AddDays(1)
                );
                //antrean.Query.Where(antrean.Query.ServiceUnitID == su.ServiceUnitID, antrean.Query.ParamedicID == p.ParamedicID, antrean.Query.PatientID == pasien.PatientID, antrean.Query.AppointmentDate.Date() == parsed.Date);
                //if (AppSession.Parameter.HealthcareID != "RSES")
                antrean.Query.Where(antrean.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
                var apptExist = antrean.Query.Load();
                if (apptExist)
                {
                    if (string.IsNullOrWhiteSpace(antrean.Notes))
                    {
                        antrean.Notes = "-";
                        antrean.Save();

                        return Request.CreateResponse(HttpStatusCode.Created, new
                        {
                            metadata = new Antrol.AmbilAntrean.Response.Metadata()
                            {
                                Code = (int)HttpStatusCode.Created,
                                Message = $"Nomor Antrean Hanya Dapat Diambil 1 Kali Pada Tanggal Yang Sama"
                            }
                        });
                    }
                    else
                    {
                        var antreanDateTime = Convert.ToDateTime(antrean.AppointmentDate.Value.ToString("yyyy-MM-dd") + ' ' + antrean.AppointmentTime + ":00");

                        var appt = new AppointmentCollection();
                        appt.Query.Where(appt.Query.ServiceUnitID == su.ServiceUnitID,
                            appt.Query.ParamedicID == dokter.ParamedicID,
                            //appt.Query.AppointmentDate.Date() == parsed.Date,
                            appt.Query.AppointmentDate >= parsed.Date, appt.Query.AppointmentDate < parsed.Date.AddDays(1),
                            appt.Query.AppointmentTime >= jam[0].Trim(),
                            appt.Query.AppointmentTime <= jam[1].Trim()
                            );
                        if (AppSession.Parameter.HealthcareID != "RSES") // kerinci
                            appt.Query.Where(appt.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
                        var apptAvailable = appt.Query.Load();

                        return Request.CreateResponse(HttpStatusCode.OK, new Antrol.AmbilAntrean.Response.Root
                        {
                            Response = new Antrol.AmbilAntrean.Response.TResponse()
                            {
                                Nomorantrean = $"{(string.IsNullOrWhiteSpace(su.QueueCode) ? su.ShortName : su.QueueCode)}{(string.IsNullOrWhiteSpace(p.ParamedicQueueCode) ? p.ParamedicInitial : p.ParamedicQueueCode)} - {(antrean.AppointmentQue ?? 1)}",
                                Angkaantrean = antrean.AppointmentQue ?? 1,
                                Kodebooking = antrean.AppointmentNo,
                                Norm = pasien.MedicalNo,
                                Namapoli = su.ServiceUnitName,
                                Namadokter = p.ParamedicName,
                                Estimasidilayani = Convert.ToInt64(antreanDateTime.AddHours(-7).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds),
                                Sisakuotajkn = (ps.QuotaBpjsOnline ?? 0) - appt.Count(a => AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) <= 0 ? 0 : (ps.QuotaBpjsOnline ?? 0) - appt.Count(a => AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)),
                                Kuotajkn = ps.QuotaBpjsOnline ?? 0,
                                Sisakuotanonjkn = (ps.QuotaOnline ?? 0) - appt.Count(a => !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) <= 0 ? 0 : (ps.QuotaOnline ?? 0) - appt.Count(a => !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)),
                                Kuotanonjkn = ps.QuotaOnline ?? 0,
                                Keterangan = "Peserta harap 60 menit lebih awal guna pencatatan administrasi"
                            },
                            metadata = new Antrol.AmbilAntrean.Response.Metadata()
                            {
                                Code = (int)HttpStatusCode.OK,
                                Message = $"Sukses"
                            }
                        });
                    }
                }

                if (string.IsNullOrWhiteSpace(psd.OperationalTimeID))
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                        }
                    });
                }

                ot = new OperationalTime();
                if (!ot.LoadByPrimaryKey(psd.OperationalTimeID))
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                        }
                    });
                }

                if (!string.IsNullOrWhiteSpace(ot.StartTime1) && !string.IsNullOrWhiteSpace(ot.EndTime1))
                {
                    var ot1 = TimeSpan.ParseExact(ot.StartTime1, "hh\\:mm", null);
                    var ot2 = TimeSpan.ParseExact(ot.EndTime1, "hh\\:mm", null);

                    if ((jam1 <= ot1.Add(new TimeSpan(1, 0, 0)) && jam1 >= ot1.Add(new TimeSpan(-1, 0, 0))) && (jam2 <= ot2.Add(new TimeSpan(1, 0, 0)) && jam2 >= ot2.Add(new TimeSpan(-1, 0, 0))))
                    {
                        waktu1 = ot1;
                        waktu2 = ot2;
                    }
                }

                if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                {
                    var ot1 = TimeSpan.ParseExact(ot.StartTime2, "hh\\:mm", null);
                    var ot2 = TimeSpan.ParseExact(ot.EndTime2, "hh\\:mm", null);

                    if ((jam1 <= ot1.Add(new TimeSpan(1, 0, 0)) && jam1 >= ot1.Add(new TimeSpan(-1, 0, 0))) && (jam2 <= ot2.Add(new TimeSpan(1, 0, 0)) && jam2 >= ot2.Add(new TimeSpan(-1, 0, 0))))
                    {
                        waktu1 = ot1;
                        waktu2 = ot2;
                    }
                }

                if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                {
                    var ot1 = TimeSpan.ParseExact(ot.StartTime3, "hh\\:mm", null);
                    var ot2 = TimeSpan.ParseExact(ot.EndTime3, "hh\\:mm", null);

                    if ((jam1 <= ot1.Add(new TimeSpan(1, 0, 0)) && jam1 >= ot1.Add(new TimeSpan(-1, 0, 0))) && (jam2 <= ot2.Add(new TimeSpan(1, 0, 0)) && jam2 >= ot2.Add(new TimeSpan(-1, 0, 0))))
                    {
                        waktu1 = ot1;
                        waktu2 = ot2;
                    }
                }

                if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                {
                    var ot1 = TimeSpan.ParseExact(ot.StartTime4, "hh\\:mm", null);
                    var ot2 = TimeSpan.ParseExact(ot.EndTime4, "hh\\:mm", null);

                    if ((jam1 <= ot1.Add(new TimeSpan(1, 0, 0)) && jam1 >= ot1.Add(new TimeSpan(-1, 0, 0))) && (jam2 <= ot2.Add(new TimeSpan(1, 0, 0)) && jam2 >= ot2.Add(new TimeSpan(-1, 0, 0))))
                    {
                        waktu1 = ot1;
                        waktu2 = ot2;
                    }
                }

                if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                {
                    var ot1 = TimeSpan.ParseExact(ot.StartTime5, "hh\\:mm", null);
                    var ot2 = TimeSpan.ParseExact(ot.EndTime5, "hh\\:mm", null);

                    if ((jam1 <= ot1.Add(new TimeSpan(1, 0, 0)) && jam1 >= ot1.Add(new TimeSpan(-1, 0, 0))) && (jam2 <= ot2.Add(new TimeSpan(1, 0, 0)) && jam2 >= ot2.Add(new TimeSpan(-1, 0, 0))))
                    {
                        waktu1 = ot1;
                        waktu2 = ot2;
                    }
                }

                if ((ps.ExamDuration ?? 0) == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                        }
                    });
                }

                if (waktu1 == null || waktu2 == null)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                        }
                    });
                }

                TimeSpan? time = waktu1;
                int que = 0;
                var list = Registration.AppointmentSlotTime(su.ServiceUnitID, p.ParamedicID, parsed.Date, true).AsEnumerable().Where(d => int.TryParse(d.Field<string>("SlotNo"), out _));
                var item = list.AsEnumerable().FirstOrDefault(d => int.Parse(d.Field<string>("SlotNo")) > 0 && d.Field<DateTime>("Start").TimeOfDay >= waktu1 && d.Field<DateTime>("Start").TimeOfDay <= waktu2);
                if (item != null)
                {
                    que = Convert.ToInt32(item["SlotNo"]);
                    time = Convert.ToDateTime(item["Start"]).TimeOfDay;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Kapasitas Antrian Sudah Penuh"
                        }
                    });
                }

                var autoNumberLastAntrean = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.AppointmentNo);
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
                antrean.LastUpdateByUserID = "mobilejkn";
                antrean.LastCreateDateTime = DateTime.Now;
                antrean.LastCreateByUserID = "mobilejkn";
                antrean.DateOfBirth = pasien.DateOfBirth;
                antrean.GuarantorID = AppSession.Parameter.GuarantorAskesID[0];
                antrean.FromRegistrationNo = string.Empty;
                antrean.EmployeeNo = string.Empty;
                antrean.EmployeeJobTitleName = string.Empty;
                antrean.EmployeeJobDepartementName = string.Empty;
                antrean.Sex = pasien.Sex;
                antrean.BirthPlace = pasien.CityOfBirth;
                antrean.Ssn = param.Nik;
                antrean.SRSalutation = pasien.SRSalutation;
                antrean.SRNationality = pasien.SRNationality;
                antrean.SROccupation = pasien.SROccupation;
                antrean.SRMaritalStatus = pasien.SRMaritalStatus;
                antrean.ItemID = string.Empty;
                antrean.SRReferralGroup = string.Empty;
                antrean.ReferralName = string.Empty;
                antrean.GuarantorCardNo = param.Nomorkartu;
                antrean.ReferenceNumber = param.Nomorreferensi;
                antrean.ReferenceType = 0;
                antrean.SRAppoinmentType = param.Jeniskunjungan.ToString();

                using (var trans = new esTransactionScope())
                {
                    if (pasienBaru)
                    {
                        var autoNumberLastPID = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PatientID);
                        var autoNumberLastMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);

                        pasien.PatientID = autoNumberLastPID.LastCompleteNumber;
                        pasien.MedicalNo = autoNumberLastMRN.LastCompleteNumber;

                        autoNumberLastPID.Save();
                        autoNumberLastMRN.Save();

                        pasien.Save();
                        contact.Save();
                    }
                    else pasien.Save();
                    autoNumberLastAntrean.Save();

                    var q = WebService.KioskQueue.QueueAdd(antrean.AppointmentDate.Value, "B", "antrol", false, false);
                    antrean.FaxNo = q.KioskQueueNo;

                    // cek ulang
                    list = Registration.AppointmentSlotTime(su.ServiceUnitID, p.ParamedicID, parsed.Date, true).AsEnumerable().Where(d => int.TryParse(d.Field<string>("SlotNo"), out _));
                    item = list.AsEnumerable().FirstOrDefault(d => int.Parse(d.Field<string>("SlotNo")) > 0 && d.Field<DateTime>("Start").TimeOfDay >= waktu1 && d.Field<DateTime>("Start").TimeOfDay <= waktu2);
                    if (item != null)
                    {
                        que = Convert.ToInt32(item["SlotNo"]);
                        time = Convert.ToDateTime(item["Start"]).TimeOfDay;
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, new
                        {
                            metadata = new Antrol.AmbilAntrean.Response.Metadata()
                            {
                                Code = (int)HttpStatusCode.Created,
                                Message = $"Kapasitas Antrian Sudah Penuh"
                            }
                        });
                    }
                    antrean.AppointmentQue = que;
                    antrean.AppointmentTime = time.Value.ToString("hh\\:mm");
                    antrean.Save();

                    var aptQue = new AppointmentQueueing();
                    if (aptQue.SetQueForReg(antrean, AppSession.Parameter.GuarantorAskesID.Contains(antrean.GuarantorID) ? "02" : AppSession.Parameter.SelfGuarantor.Equals(antrean.GuarantorID) ? "01" : "03", su, "antrol", false)) aptQue.Save();

                    #region Antrian Poli
                    // antrian v2, tambahkan antrian ke poli
                    var aQue = new AppointmentQueueing();
                    if (aQue.SetQueForPoli(antrean.AppointmentNo, "antrol")) aQue.Save();
                    #endregion

                    trans.Complete();
                }

                {
                    var antreanDateTime = Convert.ToDateTime(antrean.AppointmentDate.Value.ToString("yyyy-MM-dd") + ' ' + antrean.AppointmentTime + ":00");

                    {
                        var log = new WebServiceAPILog
                        {
                            DateRequest = DateTime.Now,
                            IPAddress = "10.200.200.188",
                            UrlAddress = "AmbilAntrean",
                            Params = JsonConvert.SerializeObject(param),
                            Response = Convert.ToInt64(antreanDateTime.AddHours(-7).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds).ToString()
                        };
                        log.Save();
                    }

                    var appt = new AppointmentCollection();
                    appt.Query.Where(appt.Query.ServiceUnitID == su.ServiceUnitID,
                        appt.Query.ParamedicID == dokter.ParamedicID,
                        //appt.Query.AppointmentDate.Date() == parsed.Date,
                        appt.Query.AppointmentDate >= parsed.Date, appt.Query.AppointmentDate < parsed.Date.AddDays(1),
                        appt.Query.AppointmentTime >= jam[0].Trim(),
                        appt.Query.AppointmentTime <= jam[1].Trim()
                        );
                    //if (AppSession.Parameter.HealthcareID != "RSES") // kerinci
                    appt.Query.Where(appt.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
                    var apptAvailable = appt.Query.Load();

                    var response = new Antrol.AmbilAntrean.Response.Root
                    {
                        Response = new Antrol.AmbilAntrean.Response.TResponse()
                        {
                            Nomorantrean = $"{(string.IsNullOrWhiteSpace(su.QueueCode) ? su.ShortName : su.QueueCode)}{(string.IsNullOrWhiteSpace(p.ParamedicQueueCode) ? p.ParamedicInitial : p.ParamedicQueueCode)} - {(antrean.AppointmentQue ?? 1)}",
                            Angkaantrean = antrean.AppointmentQue ?? 1,
                            Kodebooking = antrean.AppointmentNo,
                            Norm = pasien.MedicalNo,
                            Namapoli = su.ServiceUnitName,
                            Namadokter = p.ParamedicName,
                            Estimasidilayani = Convert.ToInt64(antreanDateTime.AddHours(-7).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds),
                            Sisakuotajkn = (ps.QuotaBpjsOnline ?? 0) - appt.Count(a => AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) <= 0 ? 0 : (ps.QuotaBpjsOnline ?? 0) - appt.Count(a => AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)),
                            Kuotajkn = ps.QuotaBpjsOnline ?? 0,
                            Sisakuotanonjkn = (ps.QuotaOnline ?? 0) - appt.Count(a => !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) <= 0 ? 0 : (ps.QuotaOnline ?? 0) - appt.Count(a => !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)),
                            Kuotanonjkn = ps.QuotaOnline ?? 0,
                            Keterangan = "Peserta harap 60 menit lebih awal guna pencatatan administrasi"
                        },
                        metadata = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = pasienBaru ? (int)HttpStatusCode.Accepted : (int)HttpStatusCode.OK,
                            Message = pasienBaru ? "Pasien Baru" : $"Sukses"
                        }
                    };

                    {
                        var log = new WebServiceAPILog
                        {
                            DateRequest = DateTime.Now,
                            IPAddress = "10.200.200.188",
                            UrlAddress = "AmbilAntrean",
                            Params = JsonConvert.SerializeObject(param),
                            Response = JsonConvert.SerializeObject(response),
                            Totalms = 0
                        };
                        log.Save();
                    }

                    return Request.CreateResponse(pasienBaru ? HttpStatusCode.Accepted : HttpStatusCode.OK, response);
                }
            }
            catch (Exception ex)
            {
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "10.200.200.188",
                    UrlAddress = "AmbilAntrean",
                    Params = JsonConvert.SerializeObject(param),
                    Response = JsonConvert.SerializeObject(new
                    {
                        ex.Source,
                        ex.Message,
                        ex.StackTrace,
                        InnerException = ex.InnerException == null ? null : new
                        {
                            ex.Source,
                            ex.Message,
                            ex.StackTrace
                        }
                    }),
                    Totalms = 0
                };
                log.Save();

                return Request.CreateResponse(HttpStatusCode.NoContent, new
                {
                    metadata = new Antrol.AmbilAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.NoContent, // 204
                        Message = "Check In Gagal, Dicoba Kembali"
                    }
                });
            }
        }

        //[CustomAuthorization]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("antrol/tambahantrean")]
        public HttpResponseMessage TambahAntrean(Antrol.AmbilAntrean.Request.Root param)
        {
            var log = new WebServiceAPILog();

            var meta = new Antrol.AmbilAntrean.Response.Metadata();

            try
            {
                if (AppSession.Parameter.HealthcareInitial == "RSI")
                {
                    log = new WebServiceAPILog();
                    log.Query.Where(log.Query.DateRequest > DateTime.Now.AddDays(-1).Date &&
                                    log.Query.DateRequest < DateTime.Now.AddDays(1).Date &&
                                    log.Query.IPAddress == Helper.GetUserHostName() &&
                                    log.Query.UrlAddress == "TRAP - TambahAntrean" &&
                                    log.Query.Params == JsonConvert.SerializeObject(param) &&
                                    log.Query.Response == string.Empty);
                    if (log.Query.Load())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, new
                        {
                            metadata = new Antrol.AmbilAntrean.Response.Metadata()
                            {
                                Code = (int)HttpStatusCode.Created,
                                Message = $"Process already exist"
                            }
                        });
                    }
                }

                log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = Helper.GetUserHostName(),
                    UrlAddress = "TRAP - TambahAntrean",
                    Params = JsonConvert.SerializeObject(param),
                    Response = string.Empty,
                    Totalms = 0
                };
                log.Save();

                var antrean = new Appointment();

                if (string.IsNullOrWhiteSpace(param.CreatedBy)) param.CreatedBy = "mobilejkn";

                if (string.IsNullOrWhiteSpace(param.Nomorkartu))
                {
                    meta = new Antrol.AmbilAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Nomor Kartu Belum Diisi"
                    };
                    log.Response = JsonConvert.SerializeObject(meta);
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = meta
                    });
                }

                if (param.Nomorkartu.Length < 13 || param.Nomorkartu.Length > 13)
                {
                    meta = new Antrol.AmbilAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Format Nomor Kartu Tidak Sesuai"
                    };
                    log.Response = JsonConvert.SerializeObject(meta);
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = meta
                    });
                }

                if (!Common.Helper.IsNumeric(param.Nomorkartu))
                {
                    meta = new Antrol.AmbilAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Format Nomor Kartu Tidak Sesuai"
                    };
                    log.Response = JsonConvert.SerializeObject(meta);
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = meta
                    });
                }

                if (string.IsNullOrWhiteSpace(param.Nik))
                {
                    meta = new Antrol.AmbilAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"NIK Kartu Belum Diisi"
                    };
                    log.Response = JsonConvert.SerializeObject(meta);
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = meta
                    });
                }

                if (param.Nik.Length < 16 || param.Nik.Length > 16)
                {
                    meta = new Antrol.AmbilAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Format NIK Tidak Sesuai"
                    };
                    log.Response = JsonConvert.SerializeObject(meta);
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = meta
                    });
                }

                string format = "yyyy-MM-dd";
                if (!DateTime.TryParseExact(param.Tanggalperiksa, format, null, System.Globalization.DateTimeStyles.None, out var parsed))
                {
                    meta = new Antrol.AmbilAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Format Tanggal Tidak Sesuai, format yang benar adalah yyyy-mm-dd"
                    };
                    log.Response = JsonConvert.SerializeObject(meta);
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = meta
                    });
                }

                if (parsed.Date < DateTime.Now.Date)
                {
                    meta = new Antrol.AmbilAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Tanggal Periksa Tidak Berlaku"
                    };
                    log.Response = JsonConvert.SerializeObject(meta);
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = meta
                    });
                }

                {
                    var ws = new Common.BPJS.VClaim.v11.Service();
                    var riwayat = ws.GetDataHistoriPelayananPeserta(param.Nomorkartu, parsed.Date.AddDays(-1).ToString("yyyy-MM-dd"), parsed.Date);
                    if (riwayat.MetaData.IsValid)
                    {
                        if (riwayat.Response.Histori != null)
                        {
                            var data = riwayat.Response.Histori.SingleOrDefault(r => r.TglSep == parsed.ToString("yyyy-MM-dd") && r.Poli.ToLower() != "igd");
                            if (data != null)
                            {
                                meta = new Antrol.AmbilAntrean.Response.Metadata()
                                {
                                    Code = (int)HttpStatusCode.Created,
                                    Message = string.Format("Pasien telah melakukan kunjungan di {0}, pada tanggal {1}{2}", data.PpkPelayanan, data.TglSep, ", " + data.Poli)
                                };
                                log.Response = JsonConvert.SerializeObject(meta);
                                log.Save();

                                return Request.CreateResponse(HttpStatusCode.Created, new
                                {
                                    metadata = meta
                                });
                            }
                        }
                    }
                }

                var polis = new ServiceUnitBridgingCollection();
                polis.Query.Where(polis.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                if (string.IsNullOrWhiteSpace(param.ServiceUnitID)) polis.Query.Where($"< SUBSTRING(BridgingID, CHARINDEX(';', BridgingID) + 1, 3) = '{param.Kodepoli}'>");
                else polis.Query.Where(polis.Query.ServiceUnitID == param.ServiceUnitID);
                if (!polis.Query.Load())
                {
                    polis = new ServiceUnitBridgingCollection();
                    polis.Query.Where(polis.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                    polis.Query.Where($"< SUBSTRING(BridgingID, 0, CHARINDEX(';', BridgingID)) = '{param.Kodepoli}'>");
                    if (!polis.Query.Load())
                    {
                        meta = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.NotFound, // 404
                            Message = $"Kode Poli {param.Kodepoli} Tidak Ditemukan"
                        };
                        log.Response = JsonConvert.SerializeObject(meta);
                        log.Save();

                        return Request.CreateResponse(HttpStatusCode.NotFound, new
                        {
                            metadata = meta
                        });
                    }
                }

                var dokter = new ParamedicBridging();
                dokter.Query.Where(dokter.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString() && dokter.Query.BridgingID == param.Kodedokter);
                if (!dokter.Query.Load())
                {
                    meta = new Antrol.AmbilAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Dokter Tidak Ditemukan"
                    };
                    log.Response = JsonConvert.SerializeObject(meta);
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = meta
                    });
                }

                var p = new Paramedic();
                p.LoadByPrimaryKey(dokter.ParamedicID);

                var svc = new Common.BPJS.Antrian.Service();

                svc = new Common.BPJS.Antrian.Service();
                var jadwal = svc.GetJadwalDokter(param.Kodepoli, param.Tanggalperiksa);
                if (!jadwal.Metadata.IsAntrolValid)
                {
                    var jadwalexist = false;
                    foreach (var poli in polis)
                    {
                        svc = new Common.BPJS.Antrian.Service();
                        jadwal = svc.GetJadwalDokter(poli.BridgingID.Split(';')[0], param.Tanggalperiksa);
                        if (jadwal.Metadata.IsAntrolValid)
                        {
                            jadwalexist = true;

                            var day = 0;
                            if (parsed.DayOfWeek == DayOfWeek.Sunday) day = 7;
                            else day = (int)parsed.DayOfWeek;

                            if (jadwal.Response.List == null && !jadwal.Response.List.Any())
                            {
                                meta = new Antrol.AmbilAntrean.Response.Metadata()
                                {
                                    Code = (int)HttpStatusCode.Created,
                                    Message = $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                                };
                                log.Response = JsonConvert.SerializeObject(meta);
                                log.Save();

                                return Request.CreateResponse(HttpStatusCode.Created, new
                                {
                                    metadata = meta
                                });
                            }

                            break;
                        }
                    }
                    if (!jadwalexist)
                    {
                        meta = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message =
                                $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                        };
                        log.Response = JsonConvert.SerializeObject(meta);
                        log.Save();

                        return Request.CreateResponse(HttpStatusCode.Created, new
                        {
                            metadata = meta
                        });
                    }
                }
                else
                {
                    var day = 0;
                    if (parsed.DayOfWeek == DayOfWeek.Sunday) day = 7;
                    else day = (int)parsed.DayOfWeek;

                    if (jadwal.Response.List == null && !jadwal.Response.List.Any())
                    {
                        meta = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                        };
                        log.Response = JsonConvert.SerializeObject(meta);
                        log.Save();

                        return Request.CreateResponse(HttpStatusCode.Created, new
                        {
                            metadata = meta
                        });
                    }
                }

                var su = new BusinessObject.ServiceUnit();
                var psd = new ParamedicScheduleDate();

                var isjadwal = false;
                foreach (var poli in polis)
                {
                    su = new BusinessObject.ServiceUnit();
                    su.LoadByPrimaryKey(poli.ServiceUnitID);

                    psd = new ParamedicScheduleDate();
                    isjadwal = psd.LoadByPrimaryKey(su.ServiceUnitID, p.ParamedicID, parsed.Year.ToString(), parsed.Date);
                    if (isjadwal) break;
                }

                if (!isjadwal)
                {
                    meta = new Antrol.AmbilAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                    };
                    log.Response = JsonConvert.SerializeObject(meta);
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = meta
                    });
                }

                var jam = param.Jampraktek.Split('-');
                var jam1 = TimeSpan.ParseExact(jam[0].Trim(), "hh\\:mm", null);
                var jam2 = TimeSpan.ParseExact(jam[1].Trim(), "hh\\:mm", null);

                if (parsed.Date == DateTime.Now.Date)
                {
                    if (DateTime.Now.TimeOfDay > jam2)
                    {
                        meta = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Pendaftaran Ke Poli {su.ServiceUnitName} Sudah Tutup Jam {jam2.ToString("hh\\:mm")}"
                        };
                        log.Response = JsonConvert.SerializeObject(meta);
                        log.Save();

                        return Request.CreateResponse(HttpStatusCode.Created, new
                        {
                            metadata = meta
                        });
                    }
                }

                var ps = new ParamedicSchedule();
                if (!ps.LoadByPrimaryKey(su.ServiceUnitID, p.ParamedicID, parsed.Year.ToString()))
                {
                    meta = new Antrol.AmbilAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                    };
                    log.Response = JsonConvert.SerializeObject(meta);
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = meta
                    });
                }
                else
                {
                    if ((ps.QuotaBpjsOnline ?? 0) == 0)
                    {
                        meta = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Kapasitas antrian sudah penuh"
                        };
                        log.Response = JsonConvert.SerializeObject(meta);
                        log.Save();

                        return Request.CreateResponse(HttpStatusCode.Created, new
                        {
                            metadata = meta
                        });
                    }
                    else
                    {
                        var appt = new AppointmentCollection();
                        appt.Query.Where(appt.Query.ServiceUnitID == su.ServiceUnitID,
                            appt.Query.ParamedicID == dokter.ParamedicID,
                            //appt.Query.AppointmentDate.Date() == parsed.Date,
                            appt.Query.AppointmentDate >= parsed.Date, appt.Query.AppointmentDate < parsed.Date.AddDays(1),
                            appt.Query.AppointmentTime >= jam[0].Trim(),
                            appt.Query.AppointmentTime <= jam[1].Trim()
                            );
                        if (AppSession.Parameter.HealthcareID != "RSES") // kerinci
                            appt.Query.Where(appt.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
                        appt.Query.Load();
                        if (appt.Count >= (ps.QuotaBpjsOnline ?? 0))
                        {
                            meta = new Antrol.AmbilAntrean.Response.Metadata()
                            {
                                Code = (int)HttpStatusCode.Created,
                                Message = $"Kapasitas antrian sudah penuh"
                            };
                            log.Response = JsonConvert.SerializeObject(meta);
                            log.Save();

                            return Request.CreateResponse(HttpStatusCode.Created, new
                            {
                                metadata = meta
                            });
                        }
                    }
                }

                var ot = new OperationalTime();
                if (!ot.LoadByPrimaryKey(psd.OperationalTimeID))
                {
                    meta = new Antrol.AmbilAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                    };
                    log.Response = JsonConvert.SerializeObject(meta);
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = meta
                    });
                }

                TimeSpan? waktu1 = null, waktu2 = null;

                if (!string.IsNullOrWhiteSpace(ot.StartTime1) && !string.IsNullOrWhiteSpace(ot.EndTime1))
                {
                    var ot1 = TimeSpan.ParseExact(ot.StartTime1, "hh\\:mm", null);
                    var ot2 = TimeSpan.ParseExact(ot.EndTime1, "hh\\:mm", null);

                    if (jam1 == ot1 && jam2 == ot2)
                    {
                        waktu1 = ot1;
                        waktu2 = ot2;
                    }
                }

                if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                {
                    var ot1 = TimeSpan.ParseExact(ot.StartTime2, "hh\\:mm", null);
                    var ot2 = TimeSpan.ParseExact(ot.EndTime2, "hh\\:mm", null);

                    if (jam1 == ot1 && jam2 == ot2)
                    {
                        waktu1 = ot1;
                        waktu2 = ot2;
                    }
                }

                if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                {
                    var ot1 = TimeSpan.ParseExact(ot.StartTime3, "hh\\:mm", null);
                    var ot2 = TimeSpan.ParseExact(ot.EndTime3, "hh\\:mm", null);

                    if (jam1 == ot1 && jam2 == ot2)
                    {
                        waktu1 = ot1;
                        waktu2 = ot2;
                    }
                }

                if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                {
                    var ot1 = TimeSpan.ParseExact(ot.StartTime4, "hh\\:mm", null);
                    var ot2 = TimeSpan.ParseExact(ot.EndTime4, "hh\\:mm", null);

                    if (jam1 == ot1 && jam2 == ot2)
                    {
                        waktu1 = ot1;
                        waktu2 = ot2;
                    }
                }

                if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                {
                    var ot1 = TimeSpan.ParseExact(ot.StartTime5, "hh\\:mm", null);
                    var ot2 = TimeSpan.ParseExact(ot.EndTime5, "hh\\:mm", null);

                    if (jam1 == ot1 && jam2 == ot2)
                    {
                        waktu1 = ot1;
                        waktu2 = ot2;
                    }
                }

                if ((ps.ExamDuration ?? 0) == 0)
                {
                    meta = new Antrol.AmbilAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                    };
                    log.Response = JsonConvert.SerializeObject(meta);
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = meta
                    });
                }

                if (waktu1 == null || waktu2 == null)
                {
                    meta = new Antrol.AmbilAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                    };
                    log.Response = JsonConvert.SerializeObject(meta);
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = meta
                    });
                }
                else
                {
                    if ($"{jam1.ToString("hh\\:mm")}-{jam2.ToString("hh\\:mm")}" != $"{waktu1?.ToString("hh\\:mm")}-{waktu2?.ToString("hh\\:mm")}")
                    {
                        meta = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                        };
                        log.Response = JsonConvert.SerializeObject(meta);
                        log.Save();

                        return Request.CreateResponse(HttpStatusCode.Created, new
                        {
                            metadata = meta
                        });
                    }
                }

                var pasienBaru = string.IsNullOrWhiteSpace(param.Norm);

                var pasien = new Patient();
                if (pasienBaru)
                {
                    pasien.Query.es.Top = 1;
                    pasien.Query.Where(pasien.Query.Ssn == param.Nik);
                    pasien.Query.OrderBy(pasien.Query.LastUpdateDateTime.Descending);
                    pasienBaru = !pasien.Query.Load();
                    if (pasienBaru)
                    {
                        pasien = new Patient();
                        pasien.Query.es.Top = 1;
                        pasien.Query.Where(pasien.Query.GuarantorCardNo == param.Nomorkartu);
                        pasien.Query.OrderBy(pasien.Query.LastUpdateDateTime.Descending);
                        pasienBaru = !pasien.Query.Load();
                    }
                }
                else
                {
                    var norm = param.Norm.Replace("-", string.Empty);

                    pasien = new Patient();
                    pasien.Query.es.Top = 1;
                    pasien.Query.Where($"< REPLACE(MedicalNo, '-', '') = '{norm}'>");
                    pasien.Query.OrderBy(pasien.Query.LastUpdateDateTime.Descending);
                    pasienBaru = !pasien.Query.Load();
                    if (pasienBaru)
                    {
                        pasien = new Patient();
                        pasien.Query.es.Top = 1;
                        pasien.Query.Where(pasien.Query.OldMedicalNo == norm);
                        pasien.Query.OrderBy(pasien.Query.LastUpdateDateTime.Descending);
                        pasienBaru = !pasien.Query.Load();
                        if (pasienBaru)
                        {

                            pasien = new Patient();
                            pasien.Query.es.Top = 1;
                            pasien.Query.Where(pasien.Query.Ssn == param.Nik);
                            pasien.Query.OrderBy(pasien.Query.LastUpdateDateTime.Descending);
                            pasienBaru = !pasien.Query.Load();
                            if (pasienBaru)
                            {
                                pasien = new Patient();
                                pasien.Query.es.Top = 1;
                                pasien.Query.Where(pasien.Query.GuarantorCardNo == param.Nomorkartu);
                                pasien.Query.OrderBy(pasien.Query.LastUpdateDateTime.Descending);
                                pasienBaru = !pasien.Query.Load();
                            }
                        }
                    }
                }

                antrean = new Appointment();
                if (!pasienBaru)
                {
                    pasien.MobilePhoneNo = param.Nohp;
                    pasien.Save();

                    antrean = new Appointment();
                    antrean.Query.Where(
                        antrean.Query.ServiceUnitID == su.ServiceUnitID,
                        antrean.Query.ParamedicID == p.ParamedicID,
                        antrean.Query.PatientID == pasien.PatientID,
                        //antrean.Query.AppointmentDate.Date() == parsed.Date
                        antrean.Query.AppointmentDate >= parsed.Date, antrean.Query.AppointmentDate < parsed.Date.AddDays(1)
                    );
                    //if (AppSession.Parameter.HealthcareID != "RSES") // kerinci
                    antrean.Query.Where(antrean.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
                    var apptExist = antrean.Query.Load();
                    if (apptExist)
                    {
                        antrean.MobilePhoneNo = param.Nohp;
                        antrean.Save();

                        var antreanDateTime = Convert.ToDateTime(antrean.AppointmentDate.Value.ToString("yyyy-MM-dd") + ' ' + antrean.AppointmentTime + ":00");

                        var appt = new AppointmentCollection();
                        appt.Query.Where(appt.Query.ServiceUnitID == su.ServiceUnitID,
                            appt.Query.ParamedicID == dokter.ParamedicID,
                            //appt.Query.AppointmentDate.Date() == parsed.Date,
                            appt.Query.AppointmentDate >= parsed.Date, appt.Query.AppointmentDate < parsed.Date.AddDays(1),
                            appt.Query.AppointmentTime >= jam[0].Trim(),
                            appt.Query.AppointmentTime <= jam[1].Trim());
                        //if (AppSession.Parameter.HealthcareID != "RSES") // kerinci
                        appt.Query.Where(appt.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
                        appt.Query.Load();

                        svc = new Common.BPJS.Antrian.Service();
                        var response = svc.GetAntreanPerKodeBooking(antrean.AppointmentNo);
                        if (!response.Metadata.IsAntrolValid)
                        {
                            if (string.IsNullOrWhiteSpace(antrean.ReferenceNumber))
                            {
                                antrean.ReferenceNumber = param.Nomorreferensi;
                                antrean.Save();
                            }

                            var exist = new Common.BPJS.Antrian.Tambah.Request.Root()
                            {
                                Kodebooking = antrean.AppointmentNo,
                                Jenispasien = AppSession.Parameter.GuarantorAskesID.Contains(antrean.GuarantorID) ? "JKN" : "NON JKN",
                                Nomorkartu = pasien.GuarantorCardNo,
                                Nik = pasien.Ssn,
                                Nohp = string.IsNullOrWhiteSpace(pasien.MobilePhoneNo) ? pasien.PhoneNo : pasien.MobilePhoneNo,
                                Kodepoli = param.Kodepoli,
                                Namapoli = su.ServiceUnitName,
                                Pasienbaru = string.IsNullOrWhiteSpace(antrean.PatientID) ? 1 : 0,
                                Norm = string.IsNullOrWhiteSpace(antrean.PatientID) ? string.Empty : pasien.MedicalNo,
                                Tanggalperiksa = antrean.AppointmentDate.Value.Date.ToString("yyyy-MM-dd"),
                                Kodedokter = dokter.BridgingID.ToInt(),
                                Namadokter = p.ParamedicName,
                                Jampraktek = $"{waktu1.Value.ToString("hh\\:mm")}-{waktu2.Value.ToString("hh\\:mm")}",
                                Jeniskunjungan = param.Jeniskunjungan, //string.IsNullOrWhiteSpace(antrean.SRAppoinmentType) ? 2 : antrean.SRAppoinmentType.ToInt(),
                                Nomorreferensi = antrean.ReferenceNumber,
                                Nomorantrean = $"{(string.IsNullOrWhiteSpace(su.QueueCode) ? su.ShortName : su.QueueCode)}{(string.IsNullOrWhiteSpace(p.ParamedicQueueCode) ? p.ParamedicInitial : p.ParamedicQueueCode)} - {(antrean.AppointmentQue ?? 1)}",
                                Angkaantrean = antrean.AppointmentQue ?? 1,
                                Estimasidilayani = Convert.ToInt64(antreanDateTime.AddHours(-7).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds),
                                Sisakuotajkn = (ps.QuotaBpjsOnline ?? 0) - appt.Count(a => AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) <= 0 ? 0 : (ps.QuotaBpjsOnline ?? 0) - appt.Count(a => AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)),
                                Kuotajkn = ps.QuotaBpjsOnline ?? 0,
                                Sisakuotanonjkn = (ps.QuotaOnline ?? 0) - appt.Count(a => !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) <= 0 ? 0 : (ps.QuotaOnline ?? 0) - appt.Count(a => !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)),
                                Kuotanonjkn = ps.QuotaOnline ?? 0,
                                Keterangan = "Peserta harap 30 menit lebih awal guna pencatatan administrasi"
                            };
                            svc = new Common.BPJS.Antrian.Service();
                            var responseExist = svc.TambahAntrian(exist);

                            var logExist = new WebServiceAPILog
                            {
                                DateRequest = DateTime.Now,
                                IPAddress = Helper.GetUserHostName(),
                                UrlAddress = "TRAP EXIST - TambahAntrean",
                                Params = JsonConvert.SerializeObject(exist),
                                Response = JsonConvert.SerializeObject(responseExist),
                                Totalms = 0
                            };
                            logExist.Save();

                            log.Response = JsonConvert.SerializeObject(responseExist);
                            log.Save();
                        }

                        return Request.CreateResponse(HttpStatusCode.OK, new Antrol.AmbilAntrean.Response.Root
                        {
                            Response = new Antrol.AmbilAntrean.Response.TResponse()
                            {
                                Nomorantrean = $"{(string.IsNullOrWhiteSpace(su.QueueCode) ? su.ShortName : su.QueueCode)}{(string.IsNullOrWhiteSpace(p.ParamedicQueueCode) ? p.ParamedicInitial : p.ParamedicQueueCode)} - {(antrean.AppointmentQue ?? 1)}",
                                Angkaantrean = antrean.AppointmentQue ?? 1,
                                Kodebooking = antrean.AppointmentNo,
                                Norm = pasien.MedicalNo,
                                Namapoli = su.ServiceUnitName,
                                Namadokter = p.ParamedicName,
                                Estimasidilayani = Convert.ToInt64(antreanDateTime.AddHours(-7).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds),
                                Sisakuotajkn = (ps.QuotaBpjsOnline ?? 0) - appt.Count(a => AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) <= 0 ? 0 : (ps.QuotaBpjsOnline ?? 0) - appt.Count(a => AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)),
                                Kuotajkn = ps.QuotaBpjsOnline ?? 0,
                                Sisakuotanonjkn = (ps.QuotaOnline ?? 0) - appt.Count(a => !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) <= 0 ? 0 : (ps.QuotaOnline ?? 0) - appt.Count(a => !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)),
                                Kuotanonjkn = ps.QuotaOnline ?? 0,
                                Keterangan = "Peserta harap 60 menit lebih awal guna pencatatan administrasi",
                                EstimasiJam = antrean.AppointmentTime,
                                ShowQrCode = AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP" ? "1" : "0"
                            },
                            metadata = new Antrol.AmbilAntrean.Response.Metadata()
                            {
                                Code = (int)HttpStatusCode.OK,
                                Message = $"Sukses"
                            }
                        });
                    }
                }

                var autoNumberLastPID = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PatientID);
                var autoNumberLastMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);
                var contact = new PatientEmergencyContact();

                if (pasienBaru)
                {
                    var svc1 = new Common.BPJS.VClaim.v11.Service();
                    var response = svc1.GetPeserta(Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta, param.Nomorkartu, DateTime.Now.Date);
                    if (!response.MetaData.IsValid)
                    {
                        meta = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = response.MetaData.Message
                        };
                        log.Response = JsonConvert.SerializeObject(meta);
                        log.Save();

                        return Request.CreateResponse(HttpStatusCode.Created, new
                        {
                            metadata = meta
                        });
                    }

                    var peserta = response.Response.Peserta;

                    pasien = new Patient();

                    autoNumberLastPID = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PatientID);
                    autoNumberLastMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);

                    pasien.PatientID = autoNumberLastPID.LastCompleteNumber;
                    pasien.MedicalNo = autoNumberLastMRN.LastCompleteNumber;
                    pasien.Ssn = peserta.Nik;
                    pasien.SRSalutation = string.Empty;
                    pasien.FirstName = peserta.Nama;
                    pasien.MiddleName = string.Empty;
                    pasien.LastName = string.Empty;
                    pasien.ParentSpouseName = string.Empty;
                    pasien.CityOfBirth = string.Empty;
                    pasien.DateOfBirth = DateTime.ParseExact(peserta.TglLahir, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None).Date;
                    pasien.Sex = peserta.Sex.ToLower() == "l" ? "M" : "F";
                    pasien.SRBloodType = string.Empty;
                    pasien.BloodRhesus = string.Empty;
                    pasien.SREthnic = string.Empty;
                    pasien.SREducation = string.Empty;
                    pasien.SRMaritalStatus = string.Empty;
                    pasien.SRNationality = string.Empty;
                    pasien.SROccupation = string.Empty;
                    pasien.SRTitle = string.Empty;
                    pasien.SRPatientCategory = string.Empty;
                    pasien.SRReligion = string.Empty;
                    pasien.SRMedicalFileBin = string.Empty;
                    pasien.SRMedicalFileStatus = string.Empty;
                    pasien.GuarantorID = AppSession.Parameter.GuarantorAskesID[0];
                    pasien.Company = string.Empty;
                    pasien.StreetName = string.Empty;
                    pasien.District = string.Empty;
                    pasien.City = string.Empty;
                    pasien.County = string.Empty;
                    pasien.State = string.Empty;
                    pasien.str.ZipCode = string.Empty;
                    pasien.PhoneNo = string.Empty;
                    pasien.FaxNo = string.Empty;
                    pasien.Email = string.Empty;
                    pasien.MobilePhoneNo = string.IsNullOrWhiteSpace(param.Nohp) ? string.Empty : param.Nohp;
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
                    pasien.LastUpdateByUserID = param.CreatedBy;
                    pasien.PackageBalance = 0;
                    pasien.HealthcareID = AppSession.Parameter.HealthcareID;
                    pasien.str.ResponTime = string.Empty;
                    pasien.SRInformationFrom = string.Empty;
                    pasien.SRPatienRelation = string.Empty;
                    pasien.PersonID = 0;
                    pasien.EmployeeNumber = string.Empty;
                    pasien.SREmployeeRelationship = string.Empty;
                    pasien.GuarantorCardNo = peserta.NoKartu;
                    pasien.IsNonPatient = false;
                    pasien.ParentSpouseAge = 0;
                    pasien.SRParentSpouseOccupation = string.Empty;
                    pasien.ParentSpouseOccupationDesc = string.Empty;
                    pasien.SRMotherOccupation = string.Empty;
                    pasien.MotherOccupationDesc = string.Empty;
                    pasien.MotherName = string.Empty;
                    pasien.MotherAge = 0;
                    pasien.IsNotPaidOff = false;
                    pasien.ParentSpouseMedicalNo = string.Empty;
                    pasien.MotherMedicalNo = string.Empty;
                    pasien.CompanyAddress = string.Empty;
                    pasien.CreatedByUserID = param.CreatedBy;
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
                    pasien.FamilyRegisterNo = string.Empty;
                    pasien.IsSyncWithDukcapil = false;

                    contact.PatientID = pasien.PatientID;
                    contact.ContactName = string.Empty;
                    contact.SRRelationship = string.Empty;
                    contact.StreetName = string.Empty;
                    contact.District = string.Empty;
                    contact.City = string.Empty;
                    contact.County = string.Empty;
                    contact.State = string.Empty;
                    contact.str.ZipCode = string.Empty;
                    contact.FaxNo = string.Empty;
                    contact.Email = string.Empty;
                    contact.PhoneNo = string.Empty;
                    contact.MobilePhoneNo = string.Empty;
                    contact.Notes = string.Empty;
                    contact.LastUpdateDateTime = DateTime.Now;
                    contact.LastUpdateByUserID = param.CreatedBy;
                    contact.SROccupation = string.Empty;
                    contact.Ssn = string.Empty;
                }
                else
                {
                    pasien.GuarantorCardNo = param.Nomorkartu;
                    pasien.Ssn = param.Nik;
                    pasien.MobilePhoneNo = string.IsNullOrWhiteSpace(param.Nohp) ? (string.IsNullOrWhiteSpace(pasien.MobilePhoneNo) ? string.Empty : pasien.MobilePhoneNo) : param.Nohp;
                    pasien.Save();
                }

                if (string.IsNullOrWhiteSpace(psd.OperationalTimeID))
                {
                    meta = new Antrol.AmbilAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                    };
                    log.Response = JsonConvert.SerializeObject(meta);
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = meta
                    });
                }

                TimeSpan? time = waktu1;
                int que = 1;

                var list = Registration.AppointmentSlotTime(su.ServiceUnitID, p.ParamedicID, parsed.Date, true).AsEnumerable().Where(d => int.TryParse(d.Field<string>("SlotNo"), out _));
                var item = list.AsEnumerable().FirstOrDefault(d => int.Parse(d.Field<string>("SlotNo")) > 0 && d.Field<DateTime>("Start").TimeOfDay >= waktu1 && d.Field<DateTime>("Start").TimeOfDay <= waktu2);
                if (item != null)
                {
                    que = Convert.ToInt32(item["SlotNo"]);
                    time = Convert.ToDateTime(item["Start"]).TimeOfDay;
                }
                else
                {
                    meta = new Antrol.AmbilAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Kapasitas Antrian Sudah Penuh"
                    };
                    log.Response = JsonConvert.SerializeObject(meta);
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = meta
                    });
                }

                var autoNumberLastAntrean = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.AppointmentNo);
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
                antrean.Notes = "Antrian Online BPJS"; //"Peserta harap 60 menit lebih awal guna pencatatan administrasi";
                antrean.str.PatientPIC = string.Empty;
                antrean.str.OfficerPIC = string.Empty;
                antrean.str.FollowUpDateTime = string.Empty;
                antrean.LastUpdateDateTime = DateTime.Now;
                antrean.LastUpdateByUserID = param.CreatedBy;
                antrean.LastCreateDateTime = DateTime.Now;
                antrean.LastCreateByUserID = param.CreatedBy;
                antrean.DateOfBirth = pasien.DateOfBirth;
                antrean.GuarantorID = AppSession.Parameter.GuarantorAskesID[0];
                antrean.FromRegistrationNo = string.Empty;
                antrean.EmployeeNo = string.Empty;
                antrean.EmployeeJobTitleName = string.Empty;
                antrean.EmployeeJobDepartementName = string.Empty;
                antrean.Sex = pasien.Sex;
                antrean.BirthPlace = pasien.CityOfBirth;
                antrean.Ssn = param.Nik;
                antrean.SRSalutation = pasien.SRSalutation;
                antrean.SRNationality = pasien.SRNationality;
                antrean.SROccupation = pasien.SROccupation;
                antrean.SRMaritalStatus = pasien.SRMaritalStatus;
                antrean.ItemID = string.Empty;
                antrean.SRReferralGroup = string.Empty;
                antrean.ReferralName = string.Empty;
                antrean.GuarantorCardNo = param.Nomorkartu;
                antrean.ReferenceNumber = param.Nomorreferensi;
                antrean.ReferenceType = 0;
                antrean.SRAppoinmentType = param.Jeniskunjungan.ToString();

                {
                    Common.BPJS.MetadataResponse response;
                    DateTime antreanDateTime;
                    AppointmentCollection appt;

                    using (var trans = new esTransactionScope())
                    {
                        if (pasienBaru)
                        {
                            autoNumberLastPID.Save();
                            autoNumberLastMRN.Save();
                            pasien.Save();
                            contact.Save();
                        }
                        autoNumberLastAntrean.Save();

                        if (AppSession.Parameter.HealthcareID == "RSBK")
                        {
                            var q = WebService.KioskQueue.QueueAdd(antrean.AppointmentDate.Value, "B", param.CreatedBy, false, false);
                            antrean.FaxNo = q.KioskQueueNo;
                        }
                        else
                        {
                            var q = WebService.KioskQueue.QueueAdd(antrean.AppointmentDate.Value, "B", param.CreatedBy, (!string.IsNullOrWhiteSpace(AppSession.Parameter.AntrolPrintLabelOnKiosk) && AppSession.Parameter.AntrolPrintLabelOnKiosk.ToLower() == "yes"), false);
                            antrean.FaxNo = q.KioskQueueNo;
                        }
                        antrean.Save();

                        if (AppSession.Parameter.IsAntrolCreateRegistrationQueue)
                        {
                            var aptQue = new AppointmentQueueing();
                            if (aptQue.SetQueForReg(antrean, AppSession.Parameter.GuarantorAskesID.Contains(antrean.GuarantorID) ? "02" : AppSession.Parameter.SelfGuarantor.Equals(antrean.GuarantorID) ? "01" : "03", su, param.CreatedBy, false)) aptQue.Save();
                        }

                        #region Antrian Poli
                        // antrian v2, tambahkan antrian ke poli
                        var aQue = new AppointmentQueueing();
                        if (aQue.SetQueForPoli(antrean.AppointmentNo, param.CreatedBy)) aQue.Save();
                        #endregion

                        {
                            antreanDateTime = Convert.ToDateTime(antrean.AppointmentDate?.ToString("yyyy-MM-dd") + ' ' + antrean.AppointmentTime + ":00");

                            appt = new AppointmentCollection();
                            appt.Query.Where(appt.Query.ServiceUnitID == su.ServiceUnitID,
                                appt.Query.ParamedicID == dokter.ParamedicID,
                                //appt.Query.AppointmentDate.Date() == parsed.Date,
                                appt.Query.AppointmentDate >= parsed.Date, appt.Query.AppointmentDate < parsed.Date.AddDays(1),
                                appt.Query.AppointmentTime >= jam[0].Trim(),
                                appt.Query.AppointmentTime <= jam[1].Trim()
                                );
                            //if (AppSession.Parameter.HealthcareID != "RSES") // kerinci
                            appt.Query.Where(appt.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
                            var apptAvailable = appt.Query.Load();

                            var tambah = new Common.BPJS.Antrian.Tambah.Request.Root()
                            {
                                Kodebooking = antrean.AppointmentNo,
                                Jenispasien = AppSession.Parameter.GuarantorAskesID.Contains(antrean.GuarantorID) ? "JKN" : "NON JKN",
                                Nomorkartu = antrean.GuarantorCardNo,
                                Nik = antrean.Ssn,
                                Nohp = antrean.MobilePhoneNo,
                                Kodepoli = param.Kodepoli,
                                Namapoli = su.ServiceUnitName,
                                Pasienbaru = string.IsNullOrWhiteSpace(antrean.PatientID) ? 1 : 0,
                                Norm = string.IsNullOrWhiteSpace(antrean.PatientID) ? string.Empty : pasien.MedicalNo,
                                Tanggalperiksa = antrean.AppointmentDate.Value.Date.ToString("yyyy-MM-dd"),
                                Kodedokter = dokter.BridgingID.ToInt(),
                                Namadokter = p.ParamedicName,
                                Jampraktek = $"{waktu1.Value.ToString("hh\\:mm")}-{waktu2.Value.ToString("hh\\:mm")}",
                                Jeniskunjungan = param.Jeniskunjungan, //string.IsNullOrWhiteSpace(antrean.SRAppoinmentType) ? 2 : antrean.SRAppoinmentType.ToInt(),
                                Nomorreferensi = antrean.ReferenceNumber,
                                Nomorantrean = $"{(string.IsNullOrWhiteSpace(su.QueueCode) ? su.ShortName : su.QueueCode)}{(string.IsNullOrWhiteSpace(p.ParamedicQueueCode) ? p.ParamedicInitial : p.ParamedicQueueCode)} - {(antrean.AppointmentQue ?? 1)}",
                                Angkaantrean = antrean.AppointmentQue ?? 1,
                                Estimasidilayani = Convert.ToInt64(antreanDateTime.AddHours(-7).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds),
                                Sisakuotajkn = (ps.QuotaBpjsOnline ?? 0) - appt.Count(a => AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) <= 0 ? 0 : (ps.QuotaBpjsOnline ?? 0) - appt.Count(a => AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)),
                                Kuotajkn = ps.QuotaBpjsOnline ?? 0,
                                Sisakuotanonjkn = (ps.QuotaOnline ?? 0) - appt.Count(a => !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) <= 0 ? 0 : (ps.QuotaOnline ?? 0) - appt.Count(a => !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)),
                                Kuotanonjkn = ps.QuotaOnline ?? 0,
                                Keterangan = "Peserta harap 30 menit lebih awal guna pencatatan administrasi"
                            };

                            svc = new Common.BPJS.Antrian.Service();
                            response = svc.TambahAntrian(tambah);
                            {
                                var logTambah = new WebServiceAPILog
                                {
                                    DateRequest = DateTime.Now,
                                    IPAddress = "10.200.200.188",
                                    UrlAddress = "TambahAntrean",
                                    Params = JsonConvert.SerializeObject(tambah),
                                    Response = JsonConvert.SerializeObject(response)
                                };
                                logTambah.Save();
                            }
                            if (!response.Metadata.IsAntrolValid)
                            {
                                log.Response = JsonConvert.SerializeObject(response);
                                log.Save();

                                return Request.CreateResponse(HttpStatusCode.Created, new
                                {
                                    metadata = new Antrol.AmbilAntrean.Response.Metadata()
                                    {
                                        Code = (int)HttpStatusCode.Created,
                                        Message = response.Metadata.Message
                                    }
                                });
                            }

                            if (pasienBaru)
                            {
                                var task = new WebServiceAPILog
                                {
                                    DateRequest = DateTime.Now,
                                    IPAddress = "10.200.200.188",
                                    UrlAddress = "TambahAntrean",
                                    Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                    {
                                        Kodebooking = antrean.AppointmentNo,
                                        Taskid = 1,
                                        Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                                    }),
                                    Response = string.Empty,
                                    Totalms = 0
                                };

                                svc = new Common.BPJS.Antrian.Service();
                                var responseTask1 = svc.UpdateWaktuAntrian(JsonConvert.DeserializeObject<Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root>(task.Params));

                                task.Response = JsonConvert.SerializeObject(responseTask1);
                                task.Save();

                                task = new WebServiceAPILog
                                {
                                    DateRequest = DateTime.Now,
                                    IPAddress = "10.200.200.188",
                                    UrlAddress = "TambahAntrean",
                                    Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                    {
                                        Kodebooking = antrean.AppointmentNo,
                                        Taskid = 2,
                                        Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                                    }),
                                    Response = string.Empty,
                                    Totalms = 0
                                };

                                svc = new Common.BPJS.Antrian.Service();
                                var responseTask2 = svc.UpdateWaktuAntrian(JsonConvert.DeserializeObject<Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root>(task.Params));

                                task.Response = JsonConvert.SerializeObject(responseTask2);
                                task.Save();
                            }

                            if (new string[] { "RSIMT", "RSEE" }.Contains(AppSession.Parameter.HealthcareInitial))
                            {
                                var aq = new AppointmentQueueing();
                                aq.Query.es.Top = 1;
                                aq.Query.Where(aq.Query.AppointmentNo == antrean.AppointmentNo);
                                if (aq.Query.Load())
                                {
                                    var parametersSlip = new PrintJobParameterCollection();
                                    parametersSlip.AddNew("p_KioskQueueNo", aq.FormattedNo, null, null);
                                    PrintManager.CreatePrintJob(AppSession.Parameter.KioskQueueSlipRpt, parametersSlip, param.CreatedBy);
                                }
                            }

                            if (response.Metadata.IsAntrolValid) trans.Complete();
                        }
                    }

                    log.Response = JsonConvert.SerializeObject(response);
                    log.Save();

                    if (AppSession.Parameter.HealthcareInitial == "RSI")
                    {
                        var parametersSEP = new PrintJobParameterCollection();
                        parametersSEP.AddNew("p_AppointmentNo", antrean.AppointmentNo, null, null);
                        PrintManager.CreatePrintJob(AppConstant.Report.BpjsAntrolRencanaKunjunganKiosk, parametersSEP, "kiosk");
                    }

                    return Request.CreateResponse(pasienBaru ? HttpStatusCode.Accepted : HttpStatusCode.OK, new Antrol.TambahAntrean.Response.Root
                    {
                        Response = new Antrol.TambahAntrean.Response.TResponse()
                        {
                            Nomorantrean = $"{(string.IsNullOrWhiteSpace(su.QueueCode) ? su.ShortName : su.QueueCode)}{(string.IsNullOrWhiteSpace(p.ParamedicQueueCode) ? p.ParamedicInitial : p.ParamedicQueueCode)} - {(antrean.AppointmentQue ?? 1)}",
                            Nomorantreanregistrasi = antrean.FaxNo,
                            Angkaantrean = antrean.AppointmentQue ?? 1,
                            Kodebooking = antrean.AppointmentNo,
                            Norm = pasien.MedicalNo,
                            Namapoli = su.ServiceUnitName,
                            Namadokter = p.ParamedicName,
                            Estimasidilayani = Convert.ToInt64(antreanDateTime.AddHours(-7).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds),
                            Sisakuotajkn = (ps.QuotaBpjsOnline ?? 0) - appt.Count(a => AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) <= 0 ? 0 : (ps.QuotaBpjsOnline ?? 0) - appt.Count(a => AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)),
                            Kuotajkn = ps.QuotaBpjsOnline ?? 0,
                            Sisakuotanonjkn = (ps.QuotaOnline ?? 0) - appt.Count(a => !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) <= 0 ? 0 : (ps.QuotaOnline ?? 0) - appt.Count(a => !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)),
                            Kuotanonjkn = ps.QuotaOnline ?? 0,
                            Keterangan = "Peserta harap 60 menit lebih awal guna pencatatan administrasi",
                            EstimasiJam = antrean.AppointmentTime,
                            ShowQrCode = AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP" ? "1" : "0"

                        },
                        metadata = new Antrol.TambahAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.OK, //pasienBaru ? (int)HttpStatusCode.Accepted : (int)HttpStatusCode.OK,
                            Message = pasienBaru ? "Pasien Baru" : $"Sukses"
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                meta = new Antrol.AmbilAntrean.Response.Metadata()
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Message = JsonConvert.SerializeObject(new
                    {
                        ex.Source,
                        ex.Message,
                        ex.StackTrace,
                        InnerException = ex.InnerException == null ? null : new
                        {
                            ex.Source,
                            ex.Message,
                            ex.StackTrace
                        }
                    })
                };
                log.Response = JsonConvert.SerializeObject(meta);
                log.Save();

                return Request.CreateResponse(HttpStatusCode.NoContent, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.Created, // 201
                        Message = "Tambah Antrean Gagal, Dicoba Kembali"
                    }
                });
            }
        }

        [CustomAuthorization]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("antrol/sisaantrean")]
        public HttpResponseMessage SisaAntrean(Antrol.SisaAntrean.Request.Root param)
        {
            var kodeBooking = param.Kodebooking.Replace("/", "-");

            var appt = new Appointment();
            if (!appt.LoadByPrimaryKey(kodeBooking))
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.SisaAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Antrean Tidak Ditemukan"
                    }
                });
            else
            {
                if (appt.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusCancel)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.SisaAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Antrean Tidak Ditemukan atau Sudah Dibatalkan"
                        }
                    });
                }
                else if (appt.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusClosed)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.SisaAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Pasien Sudah Dilayani, Antrean Tidak Dapat Dibatalkan"
                        }
                    });
                }
                //else if (appt.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusConfirmed)
                //{
                //    return Request.CreateResponse(HttpStatusCode.Created, new
                //    {
                //        metadata = new Antrol.SisaAntrean.Response.Metadata()
                //        {
                //            Code = (int)HttpStatusCode.Created,
                //            Message = $"Anda Belum Melakukan Check In"
                //        }
                //    });
                //}
            }

            var su = new ServiceUnit();
            su.LoadByPrimaryKey(appt.ServiceUnitID);

            var p = new Paramedic();
            p.LoadByPrimaryKey(appt.ParamedicID);

            var psd = new ParamedicScheduleDate();
            psd.LoadByPrimaryKey(su.ServiceUnitID, p.ParamedicID, appt.AppointmentDate?.Year.ToString(), appt.AppointmentDate.Value.Date);

            var jam = TimeSpan.ParseExact(appt.AppointmentTime, "hh\\:mm", null);

            var ps = new ParamedicSchedule();
            ps.LoadByPrimaryKey(su.ServiceUnitID, p.ParamedicID, appt.AppointmentDate?.Year.ToString());

            var ot = new OperationalTime();
            ot.LoadByPrimaryKey(psd.OperationalTimeID);

            TimeSpan? waktu1 = null, waktu2 = null;

            if (!string.IsNullOrWhiteSpace(ot.StartTime1) && !string.IsNullOrWhiteSpace(ot.EndTime1))
            {
                var ot1 = TimeSpan.ParseExact(ot.StartTime1, "hh\\:mm", null);
                var ot2 = TimeSpan.ParseExact(ot.EndTime1, "hh\\:mm", null);

                if (jam >= ot1 && jam <= ot2)
                {
                    waktu1 = ot1;
                    waktu2 = ot2;
                }
            }

            if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
            {
                var ot1 = TimeSpan.ParseExact(ot.StartTime2, "hh\\:mm", null);
                var ot2 = TimeSpan.ParseExact(ot.EndTime2, "hh\\:mm", null);

                if (jam >= ot1 && jam <= ot2)
                {
                    waktu1 = ot1;
                    waktu2 = ot2;
                }
            }

            if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
            {
                var ot1 = TimeSpan.ParseExact(ot.StartTime3, "hh\\:mm", null);
                var ot2 = TimeSpan.ParseExact(ot.EndTime3, "hh\\:mm", null);

                if (jam >= ot1 && jam <= ot2)
                {
                    waktu1 = ot1;
                    waktu2 = ot2;
                }
            }

            if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
            {
                var ot1 = TimeSpan.ParseExact(ot.StartTime4, "hh\\:mm", null);
                var ot2 = TimeSpan.ParseExact(ot.EndTime4, "hh\\:mm", null);

                if (jam >= ot1 && jam <= ot2)
                {
                    waktu1 = ot1;
                    waktu2 = ot2;
                }
            }

            if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
            {
                var ot1 = TimeSpan.ParseExact(ot.StartTime5, "hh\\:mm", null);
                var ot2 = TimeSpan.ParseExact(ot.EndTime5, "hh\\:mm", null);

                if (jam >= ot1 && jam <= ot2)
                {
                    waktu1 = ot1;
                    waktu2 = ot2;
                }
            }

            var apptList = new AppointmentCollection();
            apptList.Query.Where(
                apptList.Query.ServiceUnitID == appt.ServiceUnitID,
                apptList.Query.ParamedicID == appt.ParamedicID,
                apptList.Query.AppointmentDate == appt.AppointmentDate,
                apptList.Query.AppointmentTime >= waktu1.Value.ToString("hh\\:mm"),
                apptList.Query.AppointmentTime <= waktu2.Value.ToString("hh\\:mm"),
                apptList.Query.AppointmentQue < appt.AppointmentQue
                );
            //if (AppSession.Parameter.HealthcareID != "RSES") // kerinci
            apptList.Query.Where(apptList.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
            apptList.Query.OrderBy(apptList.Query.AppointmentTime.Ascending);
            apptList.Query.Load();

            var antreanDateTime = Convert.ToDateTime(appt.AppointmentDate.Value.ToString("yyyy-MM-dd") + ' ' + appt.AppointmentTime + ":00");
            var time = antreanDateTime.Subtract(DateTime.Now).TotalSeconds;

            var response = new Antrol.SisaAntrean.Response.Root
            {
                Response = new Antrol.SisaAntrean.Response.TResponse()
                {
                    Nomorantrean = $"{(string.IsNullOrWhiteSpace(su.QueueCode) ? su.ShortName : su.QueueCode)}{(string.IsNullOrWhiteSpace(p.ParamedicQueueCode) ? p.ParamedicInitial : p.ParamedicQueueCode)} - {(appt.AppointmentQue ?? 1)}",
                    Namapoli = su.ServiceUnitName,
                    Namadokter = p.ParamedicName,
                    Sisaantrean = apptList.Any() ? apptList.Count(a => a.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel) : 0,
                    Antreanpanggil = apptList.Any() ? (apptList.FirstOrDefault(a => a.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel).AppointmentNo ?? "-").ToString() : "-",
                    Waktutunggu = time <= 0 ? 0 : Convert.ToInt64(antreanDateTime.AddHours(-7).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds),
                    Keterangan = string.Empty
                },
                metadata = new Antrol.SisaAntrean.Response.Metadata()
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = $"Ok"
                }
            };

            {
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "10.200.200.188",
                    UrlAddress = "SisaAntrean",
                    Params = JsonConvert.SerializeObject(param),
                    Response = JsonConvert.SerializeObject(response),
                    Totalms = 0
                };
                log.Save();
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [CustomAuthorization]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("antrol/batalantrean")]
        public HttpResponseMessage BatalAntrean(Antrol.BatalAntrean.Request.Root param)
        {
            {
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "10.200.200.188",
                    UrlAddress = "BatalAntrean",
                    Params = JsonConvert.SerializeObject(param),
                    Response = string.Empty,
                    Totalms = 0
                };
                log.Save();
            }

            var kodeBooking = param.Kodebooking.Replace("/", "-");

            var appt = new Appointment();
            if (!appt.LoadByPrimaryKey(kodeBooking))
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.BatalAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Antrean Tidak Ditemukan"
                    }
                });
            else
            {
                if (appt.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusCancel)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.BatalAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Antrean Tidak Ditemukan atau Sudah Dibatalkan"
                        }
                    });
                }
                else if (appt.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusClosed)
                {
                    var reg = new Registration();
                    reg.Query.es.Top = 1;
                    reg.Query.Where(reg.Query.AppointmentNo == appt.AppointmentNo);
                    if (reg.Query.Load())
                    {
                        if (!(reg.IsVoid ?? false))
                        {
                            return Request.CreateResponse(HttpStatusCode.Created, new
                            {
                                metadata = new Antrol.BatalAntrean.Response.Metadata()
                                {
                                    Code = (int)HttpStatusCode.Created,
                                    Message = $"Pasien Sudah Dilayani, Antrean Tidak Dapat Dibatalkan"
                                }
                            });
                        }
                    }
                }
            }

            appt.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusCancel;
            appt.LastCreateDateTime = DateTime.Now;
            appt.LastCreateByUserID = "mjkn";
            appt.Save();

            //var svc = new Common.BPJS.Antrian.Service();
            //var response = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
            //{
            //    Kodebooking = param.Kodebooking,
            //    Taskid = 99,
            //    Waktu = Convert.ToInt64(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds)
            //});
            //if (response.IsAntrolValid)
            //{
            //    appt.Save();

            return Request.CreateResponse(HttpStatusCode.OK, new Antrol.CheckIn.Response.Root
            {
                metadata = new Antrol.CheckIn.Response.Metadata()
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = $"Ok"
                }
            });
            //}

            //return Request.CreateResponse(HttpStatusCode.Created, new Antrol.BatalAntrean.Response.Root
            //{
            //    metadata = new Antrol.BatalAntrean.Response.Metadata()
            //    {
            //        Code = (int)HttpStatusCode.Created,
            //        Message = response.Message
            //    }
            //});
        }

        [CustomAuthorization]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("antrol/ckeckin")]
        [System.Web.Http.Route("antrol/checkin")]
        public HttpResponseMessage CheckIn(Antrol.CheckIn.Request.Root param)
        {
            var log = new WebServiceAPILog
            {
                DateRequest = DateTime.Now,
                IPAddress = "10.200.200.188",
                UrlAddress = "CheckIn",
                Params = JsonConvert.SerializeObject(param),
                Response = string.Empty,
                Totalms = 0
            };

            var kodeBooking = param.Kodebooking.Replace("/", "-");

            var appt = new Appointment();
            if (!appt.LoadByPrimaryKey(kodeBooking))
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.CheckIn.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Antrean Tidak Ditemukan atau Sudah Dibatalkan"
                    }
                });
            else
            {
                if (new[] { AppSession.Parameter.AppointmentStatusCancel, AppSession.Parameter.AppointmentStatusClosed }.Contains(appt.SRAppointmentStatus))
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.CheckIn.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Antrean Tidak Ditemukan atau Sudah Dibatalkan"
                        }
                    });
                }
            }

            appt.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusConfirmed;
            appt.LastUpdateDateTime = DateTime.Now;
            appt.LastUpdateByUserID = "mjkn";

            var patient = new Patient();
            patient.LoadByPrimaryKey(appt.PatientID);
            if (string.IsNullOrWhiteSpace(patient.MedicalNo))
            {
                if (param.Waktu == 0) param.Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds());

                var svc = new Common.BPJS.Antrian.Service();
                var response = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                {
                    Kodebooking = param.Kodebooking,
                    Taskid = 1,
                    Waktu = param.Waktu
                });

                log.Response = JsonConvert.SerializeObject(response);
                log.Save();

                //var waktu = DateTimeOffset.FromUnixTimeMilliseconds(param.Waktu).UtcDateTime;

                log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "10.200.200.188",
                    UrlAddress = "CheckIn",
                    Params = JsonConvert.SerializeObject(param),
                    Response = string.Empty,
                    Totalms = 0
                };
                svc = new Common.BPJS.Antrian.Service();
                response = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                {
                    Kodebooking = param.Kodebooking,
                    Taskid = 2,
                    Waktu = param.Waktu
                });

                log.Response = JsonConvert.SerializeObject(response);
                log.Save();
            }

            //if (response.Metadata.IsAntrolValid)
            //{
            appt.Save();

            return Request.CreateResponse(HttpStatusCode.OK, new Antrol.CheckIn.Response.Root
            {
                metadata = new Antrol.CheckIn.Response.Metadata()
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = $"Ok"
                }
            });
            //}

            //return Request.CreateResponse(HttpStatusCode.Created, new Antrol.CheckIn.Response.Root
            //{
            //    metadata = new Antrol.CheckIn.Response.Metadata()
            //    {
            //        Code = (int)HttpStatusCode.Created,
            //        Message = $"{response.Metadata.Message}, task id 1 ws bpjs"
            //    }
            //});

            //return PostSelfCheckIn(new Antrol.CheckIn.Request.SelfChechkIn()
            //{
            //    NoRujukan = appt.AppointmentNo,
            //    Tipe = 1
            //});
        }

        [CustomAuthorization]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("antrol/infopasienbaru")]
        public HttpResponseMessage InfoPasienBaru(Antrol.InfoPasienBaru.Request.Root param)
        {
            if (string.IsNullOrWhiteSpace(param.Nomorkartu))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Nomor Kartu Belum Diisi"
                    }
                });
            }

            if (param.Nomorkartu.Length < 13 || param.Nomorkartu.Length > 13)
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Format Nomor Kartu Tidak Sesuai"
                    }
                });
            }

            if (!Common.Helper.IsNumeric(param.Nomorkartu))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Format Nomor Kartu Tidak Sesuai"
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(param.Nik))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"NIK Kartu Belum Diisi"
                    }
                });
            }

            if (param.Nik.Length < 16 || param.Nik.Length > 16)
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Format NIK Tidak Sesuai"
                    }
                });
            }

            if (!Common.Helper.IsNumeric(param.Nomorkartu))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Format NIK Tidak Sesuai"
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(param.Nomorkk))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Nomor KK Belum Diisi"
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(param.Nama))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Nama Belum Diisi"
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(param.Jeniskelamin))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Jenis Kelamin Belum Diisi"
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(param.Tanggallahir))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Tanggal Lahir Belum Diisi"
                    }
                });
            }

            string format = "yyyy-MM-dd";
            if (!DateTime.TryParseExact(param.Tanggallahir, format, null, System.Globalization.DateTimeStyles.None, out var parsed))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Format Tanggal Lahir Tidak Sesuai"
                    }
                });
            }

            if (parsed.Date > DateTime.Now.Date)
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Format Tanggal Lahir Tidak Sesuai"
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(param.Alamat))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Alamat Belum Diisi"
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(param.Kodeprop))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Kode Propinsi Belum Diisi"
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(param.Namaprop))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Nama Propinsi Belum Diisi"
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(param.Kodedati2))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Kode Dati 2 Belum Diisi"
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(param.Namadati2))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Dati 2 Belum Diisi"
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(param.Kodekec))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Kode Kecamatan Belum Diisi"
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(param.Namakec))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Nama Kecamatan Belum Diisi"
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(param.Kodekel))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Kode Kelurahan Belum Diisi"
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(param.Namakel))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Nama Kelurahan Belum Diisi"
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(param.Rt))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"RT Belum Diisi"
                    }
                });
            }

            if (string.IsNullOrWhiteSpace(param.Rw))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"RW Belum Diisi"
                    }
                });
            }

            {
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "10.200.200.188",
                    UrlAddress = "InfoPasienBaru",
                    Params = JsonConvert.SerializeObject(param),
                    Response = string.Empty,
                    Totalms = 0
                };
                log.Save();
            }

            var autoNumberLastPID = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PatientID);
            var autoNumberLastMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);

            var contact = new PatientEmergencyContact();

            var pasien = new Patient();
            pasien.Query.Where(
                pasien.Query.GuarantorCardNo == param.Nomorkartu,
                pasien.Query.Ssn == param.Nik
            );

            if (pasien.Query.Load())
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Data Peserta Sudah Pernah Dientrikan"
                    }
                });
            }

            pasien = new Patient();

            autoNumberLastPID = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PatientID);
            pasien.PatientID = autoNumberLastPID.LastCompleteNumber;
            pasien.MedicalNo = autoNumberLastMRN.LastCompleteNumber;
            pasien.Ssn = param.Nik;
            pasien.SRSalutation = string.Empty;
            pasien.FirstName = param.Nama;
            pasien.MiddleName = string.Empty;
            pasien.LastName = string.Empty;
            pasien.ParentSpouseName = string.Empty;
            pasien.CityOfBirth = string.Empty;

            pasien.DateOfBirth = parsed;

            pasien.Sex = param.Jeniskelamin == "L" ? "M" : "F";
            pasien.SRBloodType = string.Empty;
            pasien.BloodRhesus = string.Empty;
            pasien.SREthnic = string.Empty;
            pasien.SREducation = string.Empty;
            pasien.SRMaritalStatus = string.Empty;
            pasien.SRNationality = string.Empty;
            pasien.SROccupation = string.Empty;
            pasien.SRTitle = string.Empty;
            pasien.SRPatientCategory = string.Empty;
            pasien.SRReligion = string.Empty;
            pasien.SRMedicalFileBin = string.Empty;
            pasien.SRMedicalFileStatus = string.Empty;
            pasien.GuarantorID = AppSession.Parameter.GuarantorAskesID[0];
            pasien.Company = string.Empty;
            pasien.StreetName = $"{param.Alamat} RT : {param.Rt} RW : {param.Rw}";
            pasien.District = param.Namakel;
            pasien.City = param.Namadati2;
            pasien.County = param.Namakec;
            pasien.State = param.Namaprop;
            pasien.str.ZipCode = string.Empty;
            pasien.PhoneNo = string.Empty;
            pasien.FaxNo = string.Empty;
            pasien.Email = string.Empty;
            pasien.MobilePhoneNo = param.Nohp;
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
            pasien.LastUpdateByUserID = "mjkn";
            pasien.PackageBalance = 0;
            pasien.HealthcareID = AppSession.Parameter.HealthcareID;
            pasien.str.ResponTime = string.Empty;
            pasien.SRInformationFrom = string.Empty;
            pasien.SRPatienRelation = string.Empty;
            pasien.PersonID = 0;
            pasien.EmployeeNumber = string.Empty;
            pasien.SREmployeeRelationship = string.Empty;
            pasien.GuarantorCardNo = param.Nomorkartu;
            pasien.IsNonPatient = false;
            pasien.ParentSpouseAge = 0;
            pasien.SRParentSpouseOccupation = string.Empty;
            pasien.ParentSpouseOccupationDesc = string.Empty;
            pasien.SRMotherOccupation = string.Empty;
            pasien.MotherOccupationDesc = string.Empty;
            pasien.MotherName = string.Empty;
            pasien.MotherAge = 0;
            pasien.IsNotPaidOff = false;
            pasien.ParentSpouseMedicalNo = string.Empty;
            pasien.MotherMedicalNo = string.Empty;
            pasien.CompanyAddress = string.Empty;
            pasien.CreatedByUserID = "mjkn";
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
            pasien.FamilyRegisterNo = param.Nomorkk;
            pasien.IsSyncWithDukcapil = false;

            contact.PatientID = pasien.PatientID;
            contact.ContactName = string.Empty;
            contact.SRRelationship = string.Empty;
            contact.StreetName = string.Empty;
            contact.District = string.Empty;
            contact.City = string.Empty;
            contact.County = string.Empty;
            contact.State = string.Empty;
            contact.str.ZipCode = string.Empty;
            contact.FaxNo = string.Empty;
            contact.Email = string.Empty;
            contact.PhoneNo = string.Empty;
            contact.MobilePhoneNo = string.Empty;
            contact.Notes = string.Empty;
            contact.LastUpdateDateTime = DateTime.Now;
            contact.LastUpdateByUserID = "mjkn";
            contact.SROccupation = string.Empty;
            contact.Ssn = string.Empty;

            //try
            //{
            using (var trans = new esTransactionScope())
            {
                autoNumberLastPID.Save();
                autoNumberLastMRN.Save();
                pasien.Save();
                contact.Save();

                trans.Complete();
            }
            //}
            //catch (Exception ex)
            //{
            //    var log = new WebServiceAPILog();
            //    log.DateRequest = DateTime.Now;
            //    log.IPAddress = "antrol";
            //    log.UrlAddress = "infopasienbaru";
            //    log.Params = JsonConvert.SerializeObject(param);
            //    log.Response = JsonConvert.SerializeObject(new
            //    {
            //        ex.Source,
            //        ex.Message,
            //        ex.StackTrace,
            //        InnerException = ex.InnerException == null ? null : new
            //        {
            //            ex.Source,
            //            ex.Message,
            //            ex.StackTrace
            //        }
            //    });
            //    log.Totalms = 0;
            //    log.Save();
            //}

            //var xxx = new WebServiceAPILogCollection();

            //xxx.Query.Where(xxx.Query.UrlAddress == "ambilantrean");
            //xxx.Query.OrderBy(xxx.Query.DateRequest.Descending);
            //xxx.Query.Load();

            //var yyy = xxx.FirstOrDefault(x => JsonConvert.DeserializeObject<Antrol.AmbilAntrean.Request.Root>(x.Params).Nomorkartu == param.Nomorkartu && JsonConvert.DeserializeObject<Antrol.AmbilAntrean.Request.Root>(x.Params).Norm == string.Empty);
            //if (yyy != null)
            //{
            //    var ambil = JsonConvert.DeserializeObject<Antrol.AmbilAntrean.Request.Root>(yyy.Params);
            //    ambil.Norm = pasien.MedicalNo;

            //    return TambahAntrean(ambil);
            //}

            return Request.CreateResponse(HttpStatusCode.OK, new Antrol.InfoPasienBaru.Response.Root
            {
                Response = new Antrol.InfoPasienBaru.Response.TResponse()
                {
                    Norm = pasien.MedicalNo
                },
                metadata = new Antrol.InfoPasienBaru.Response.Metadata()
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = $"Harap datang ke admisi untuk melengkapi data rekam medis"
                }
            });
        }

        //[CustomAuthorization]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("antrol/jadwaloperasirs")]
        public HttpResponseMessage JadwalOperasiRs(Antrol.JadwalOperasiRs.Request.Root param)
        {
            string format = "yyyy-MM-dd";
            if (!DateTime.TryParseExact(param.Tanggalawal, format, null, System.Globalization.DateTimeStyles.None, out var parsedFrom))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.JadwalOperasiRs.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Format Tanggal Tidak Sesuai"
                    }
                });
            }

            if (!DateTime.TryParseExact(param.Tanggalakhir, format, null, System.Globalization.DateTimeStyles.None, out var parsedTo))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.JadwalOperasiRs.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Format Tanggal Tidak Sesuai"
                    }
                });
            }

            if (parsedTo < parsedFrom)
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.JadwalOperasiRs.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Tanggal Akhir Tidak Boleh Lebih Kecil dari Tanggal Awal"
                    }
                });
            }

            {
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "10.200.200.188",
                    UrlAddress = "JadwalOperasiRs",
                    Params = JsonConvert.SerializeObject(param),
                    Response = string.Empty,
                    Totalms = 0
                };
                log.Save();
            }

            //var svc = new Common.BPJS.Antrian.Service();
            //var response = svc.GetReferensiPoli();
            //if (!response.Metadata.IsAntrolValid)
            //{
            //    return Request.CreateResponse(HttpStatusCode.Created, new
            //    {
            //        metadata = new Antrol.JadwalOperasiRs.Response.Metadata()
            //        {
            //            Code = (int)HttpStatusCode.Created,
            //            Message = response.Metadata.Message
            //        }
            //    });
            //}

            //var list = response.Response.List.Where(x => x.Nmpoli.Contains("BEDAH")).Select(x =>
            //new
            //{
            //    kode = string.Format("{0};{1}", x.Kdpoli, x.Kdsubspesialis)
            //});

            //var subs = new ServiceUnitBridgingCollection();
            //subs.Query.Where(subs.Query.SRBridgingType == AppEnum.BridgingType.ANTROL, list.Select(x => x.kode).Contains(subs.Query.BridgingID));
            //if (!subs.Query.Load())
            //{
            //    return Request.CreateResponse(HttpStatusCode.Created, new Antrol.JadwalOperasiRs.Response.Root
            //    {
            //        metadata = new Antrol.JadwalOperasiRs.Response.Metadata()
            //        {
            //            Code = (int)HttpStatusCode.Created,
            //            Message = $"Poli bedah Tidak Ditemukan"
            //        }
            //    });
            //}

            var sub = new ServiceUnitBookingQuery("a");
            var su = new ServiceUnitQuery("b");
            var subr = new ServiceUnitBridgingQuery("c");
            var p = new PatientQuery("d");
            var reg = new RegistrationQuery("e");

            sub.Select(
                sub.BookingNo,
                sub.BookingDateTimeFrom,
                sub.Diagnose,
                subr.BridgingID,
                su.ServiceUnitName,
                sub.IsApproved,
                p.GuarantorCardNo,
                p.LastUpdateDateTime
                );
            sub.LeftJoin(reg).On(sub.RegistrationNo == reg.RegistrationNo);
            sub.LeftJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID);
            sub.LeftJoin(subr).On(su.ServiceUnitID == subr.ServiceUnitID && subr.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
            sub.InnerJoin(p).On(sub.PatientID == p.PatientID);
            sub.Where(sub.BookingDateTimeFrom.Date() >= parsedFrom.Date, sub.BookingDateTimeTo.Date() <= parsedTo.Date, sub.IsVoid == false, p.GuarantorID.In(AppSession.Parameter.GuarantorAskesID));
            //sub.Where("< ISNULL(a.IsValidate, CAST(0 AS BIT)) = 0>");
            sub.OrderBy(sub.BookingDateTimeFrom.Ascending);

            var table = sub.LoadDataTable();
            if (table.Rows.Count == 0)
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.JadwalOperasiRs.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.OK,
                        Message = $"data tidak ditemukan"
                    }
                });

            var list = new List<Antrol.JadwalOperasiRs.Response.List>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Antrol.JadwalOperasiRs.Response.List()
                {
                    Kodebooking = row[0].ToString(),
                    Tanggaloperasi = Convert.ToDateTime(row[1]).ToString("yyyy-MM-dd"),
                    Jenistindakan = row[2].ToString(),
                    Kodepoli = string.IsNullOrWhiteSpace(row[3].ToString()) ? string.Empty : row[3].ToString().Split(';')[0],
                    Namapoli = row[4].ToString(),
                    //Terlaksana = row[5] != DBNull.Value ? Convert.ToBoolean(row[5]) ? 1 : 0 : 0,
                    Terlaksana = DateTime.Now < Convert.ToDateTime(row[1]) ? 0 : 1,
                    Nopeserta = row[6].ToString(),
                    Lastupdate = Convert.ToInt64(Convert.ToDateTime(row[7]).AddHours(-7).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds)
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new Antrol.JadwalOperasiRs.Response.Root
            {
                Response = new Antrol.JadwalOperasiRs.Response.TResponse()
                {
                    //List = list.Where(l => l.Terlaksana == 0).ToList()
                    List = list
                },
                metadata = new Antrol.JadwalOperasiRs.Response.Metadata()
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = $"Ok"
                }
            });
        }

        //[CustomAuthorization]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("antrol/jadwaloperasipasien")]
        public HttpResponseMessage JadwalOperasiPasien(Antrol.JadwalOperasiPasien.Request.Root param)
        {
            if (string.IsNullOrWhiteSpace(param.Nopeserta))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.JadwalOperasiPasien.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Nomor Kartu Belum Diisi"
                    }
                });
            }

            if (param.Nopeserta.Length < 13 || param.Nopeserta.Length > 13)
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.JadwalOperasiPasien.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Format Nomor Kartu Tidak Sesuai"
                    }
                });
            }

            if (!Common.Helper.IsNumeric(param.Nopeserta))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.JadwalOperasiPasien.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Format Nomor Kartu Tidak Sesuai"
                    }
                });
            }

            {
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "10.200.200.188",
                    UrlAddress = "JadwalOperasiPasien",
                    Params = JsonConvert.SerializeObject(param),
                    Response = string.Empty,
                    Totalms = 0
                };
                log.Save();
            }

            var sub = new ServiceUnitBookingQuery("a");
            var su = new ServiceUnitQuery("b");
            var subr = new ServiceUnitBridgingQuery("c");
            var p = new PatientQuery("d");
            var reg = new RegistrationQuery("e");

            sub.Select(
                sub.BookingNo,
                sub.BookingDateTimeFrom,
                sub.Diagnose,
                subr.BridgingID,
                su.ServiceUnitName,
                sub.IsApproved,
                p.GuarantorCardNo,
                p.LastUpdateDateTime
                );
            sub.LeftJoin(reg).On(sub.RegistrationNo == reg.RegistrationNo);
            sub.LeftJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID);
            sub.LeftJoin(subr).On(su.ServiceUnitID == subr.ServiceUnitID && subr.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
            sub.InnerJoin(p).On(sub.PatientID == p.PatientID);
            sub.Where(p.GuarantorCardNo == param.Nopeserta, sub.IsVoid == false, reg.GuarantorID.In(AppSession.Parameter.GuarantorAskesID));
            //sub.Where("< ISNULL(a.IsValidate, CAST(0 AS BIT)) = 0>");

            var table = sub.LoadDataTable();
            if (table.Rows.Count == 0)
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.JadwalOperasiRs.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.OK,
                        Message = $"data tidak ditemukan"
                    }
                });

            var list = new List<Antrol.JadwalOperasiPasien.Response.List>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Antrol.JadwalOperasiPasien.Response.List()
                {
                    Kodebooking = row[0].ToString(),
                    Tanggaloperasi = Convert.ToDateTime(row[1]).ToString("yyyy-MM-dd"),
                    Jenistindakan = row[2].ToString(),
                    Kodepoli = string.IsNullOrWhiteSpace(row[3].ToString()) ? string.Empty : row[3].ToString().Split(';')[0],
                    Namapoli = row[4].ToString(),
                    //Terlaksana = row[5] != DBNull.Value ? Convert.ToBoolean(row[5]) ? 1 : 0 : 0
                    Terlaksana = DateTime.Now < Convert.ToDateTime(row[1]) ? 0 : 1
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new Antrol.JadwalOperasiPasien.Response.Root
            {
                Response = new Antrol.JadwalOperasiPasien.Response.TResponse()
                {
                    List = list.Where(l => l.Terlaksana == 0).ToList()
                },
                metadata = new Antrol.JadwalOperasiPasien.Response.Metadata()
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = $"Ok"
                }
            });
        }

        [CustomAuthorization]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("antrol/ambilantreanfarmasi")]
        public HttpResponseMessage AmbilAntreanFarmasi(Antrol.JadwalOperasiPasien.Request.Root param)
        {
            if (string.IsNullOrWhiteSpace(param.Nopeserta))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.JadwalOperasiPasien.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Nomor Kartu Belum Diisi"
                    }
                });
            }

            if (param.Nopeserta.Length < 13 || param.Nopeserta.Length > 13)
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.JadwalOperasiPasien.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Format Nomor Kartu Tidak Sesuai"
                    }
                });
            }

            if (!Common.Helper.IsNumeric(param.Nopeserta))
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.JadwalOperasiPasien.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Format Nomor Kartu Tidak Sesuai"
                    }
                });
            }

            {
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "10.200.200.188",
                    UrlAddress = "JadwalOperasiPasien",
                    Params = JsonConvert.SerializeObject(param),
                    Response = string.Empty,
                    Totalms = 0
                };
                log.Save();
            }

            var sub = new ServiceUnitBookingQuery("a");
            var su = new ServiceUnitQuery("b");
            var subr = new ServiceUnitBridgingQuery("c");
            var p = new PatientQuery("d");
            var reg = new RegistrationQuery("e");

            sub.Select(
                sub.BookingNo,
                sub.BookingDateTimeFrom,
                sub.Diagnose,
                subr.BridgingID,
                su.ServiceUnitName,
                sub.IsValidate,
                p.GuarantorCardNo,
                p.LastUpdateDateTime
                );
            sub.LeftJoin(reg).On(sub.RegistrationNo == reg.RegistrationNo);
            sub.LeftJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID);
            sub.LeftJoin(subr).On(su.ServiceUnitID == subr.ServiceUnitID && subr.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
            sub.InnerJoin(p).On(sub.PatientID == p.PatientID);
            sub.Where(p.GuarantorCardNo == param.Nopeserta, sub.IsVoid == false, reg.GuarantorID.In(AppSession.Parameter.GuarantorAskesID));
            //sub.Where("< ISNULL(a.IsValidate, CAST(0 AS BIT)) = 0>");

            var table = sub.LoadDataTable();
            if (table.Rows.Count == 0)
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.JadwalOperasiRs.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.OK,
                        Message = $"data tidak ditemukan"
                    }
                });

            var list = new List<Antrol.JadwalOperasiPasien.Response.List>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Antrol.JadwalOperasiPasien.Response.List()
                {
                    Kodebooking = row[0].ToString(),
                    Tanggaloperasi = Convert.ToDateTime(row[1]).ToString("yyyy-MM-dd"),
                    Jenistindakan = row[2].ToString(),
                    Kodepoli = string.IsNullOrWhiteSpace(row[3].ToString()) ? string.Empty : row[3].ToString().Split(';')[0],
                    Namapoli = row[4].ToString(),
                    Terlaksana = row[5] != DBNull.Value ? Convert.ToBoolean(row[5]) ? 1 : 0 : 0
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new Antrol.JadwalOperasiPasien.Response.Root
            {
                Response = new Antrol.JadwalOperasiPasien.Response.TResponse()
                {
                    List = list
                },
                metadata = new Antrol.JadwalOperasiPasien.Response.Metadata()
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = $"Ok"
                }
            });
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("antrol/jadwaldokterhfis/{kodePoli}/{tanggalPeriksa}")]
        public HttpResponseMessage JadwalDokterHfis(string kodePoli, string tanggalPeriksa)
        {
            var svc = new Common.BPJS.Antrian.Service();
            var jadwal = svc.GetJadwalDokter(kodePoli, tanggalPeriksa);

            return Request.CreateResponse(HttpStatusCode.OK, jadwal);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("antrol/selfcheckin/postselfcheckIn")]
        // tipePencarian : 1 = by no antrian, 2 = by no mr
        public HttpResponseMessage PostSelfCheckIn(Antrol.CheckIn.Request.SelfChechkIn param)
        {
            var log = new WebServiceAPILog();

            if (AppSession.Parameter.HealthcareInitial == "RSI")
            {
                try
                {
                    var str = Encoding.UTF8.GetString(Convert.FromBase64String(param.NoRujukan));
                    var decode = JsonConvert.DeserializeObject<Antrol.CheckIn.Request64String.Root>(str);
                    if (decode != null && !string.IsNullOrWhiteSpace(decode.NomorAntrean)) param.NoRujukan = decode.KodeBooking;
                }
                catch
                {

                }

                log = new WebServiceAPILog();
                log.Query.Where(log.Query.DateRequest > DateTime.Now.AddDays(-1).Date &&
                                log.Query.DateRequest < DateTime.Now.AddDays(1).Date &&
                                log.Query.IPAddress == Helper.GetUserHostName() &&
                                log.Query.UrlAddress == "TRAP - PostSelfCheckIn" &&
                                log.Query.Params == JsonConvert.SerializeObject(param) &&
                                log.Query.Response == string.Empty);
                if (log.Query.Load())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Process already exist"
                        }
                    });
                }
            }

            log = new WebServiceAPILog
            {
                DateRequest = DateTime.Now,
                IPAddress = Helper.GetUserHostName(),
                UrlAddress = "TRAP - PostSelfCheckIn",
                Params = JsonConvert.SerializeObject(param),
                Response = string.Empty,
                Totalms = 0
            };
            log.Save();

            if (string.IsNullOrWhiteSpace(param.NoRujukan))
            {
                log.Response = JsonConvert.SerializeObject(new
                {
                    Code = (int)HttpStatusCode.NotAcceptable, // 406
                    Message = $"Nomor Tidak Diisi"
                });
                log.Save();

                return Request.CreateResponse(HttpStatusCode.NotAcceptable, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.NotAcceptable, // 406
                        Message = $"Nomor Tidak Diisi"
                    }
                });
            }

            var message = string.Empty;

            var appt = new Appointment();
            appt.Query.Where(appt.Query.ReferenceNumber == param.NoRujukan);
            if (!appt.Query.Load())
            {
                appt = new Appointment();
                appt.Query.Where(appt.Query.AppointmentNo == param.NoRujukan);
                if (!appt.Query.Load())
                {
                    appt = new Appointment();
                    appt.Query.Where(
                        appt.Query.GuarantorCardNo == param.NoRujukan &&
                        //appt.Query.AppointmentDate.Date() == DateTime.Now.Date &&
                        appt.Query.AppointmentDate >= DateTime.Now.Date, appt.Query.AppointmentDate < DateTime.Now.Date.AddDays(1),
                        appt.Query.SRAppointmentStatus.NotIn(AppSession.Parameter.AppointmentStatusCancel, AppSession.Parameter.AppointmentStatusClosed));
                    if (!appt.Query.Load())
                    {
                        log.Response = JsonConvert.SerializeObject(new
                        {
                            Code = (int)HttpStatusCode.NotFound, // 404
                            Message = $"Antrian Tidak Ditemukan"
                        });
                        log.Save();

                        return Request.CreateResponse(HttpStatusCode.NotFound, new
                        {
                            metadata = new
                            {
                                Code = (int)HttpStatusCode.NotFound, // 404
                                Message = $"Antrian Tidak Ditemukan"
                            }
                        });
                    }
                }
            }

            var maxCheckIn = 2;
            if (new string[] { "RSI", "RSIMT" }.Contains(AppSession.Parameter.HealthcareInitial)) maxCheckIn = 1;

            if (AppSession.Parameter.MaxHourCheckInMJknKiosk > -1)
                maxCheckIn = AppSession.Parameter.MaxHourCheckInMJknKiosk ?? 0;

            var antreanDateTime = Convert.ToDateTime(appt.AppointmentDate?.ToString("yyyy-MM-dd") + " " + appt.AppointmentTime + ":00");
            //if (!new string[] { "RSI", "RSBK", "RSRG", "RSIMT", "RSHM", "RSJKT" }.Contains(AppSession.Parameter.HealthcareInitial))
            //{
            if (ConfigurationManager.AppSettings["ValidasiCheckin"] == "true")
            {
                if (maxCheckIn > 0)
                {
                    if (antreanDateTime.Subtract(DateTime.Now).TotalHours > maxCheckIn)
                    {
                        //var logHari = new WebServiceAPILog
                        //{
                        //    DateRequest = DateTime.Now,
                        //    IPAddress = "10.200.200.188",
                        //    UrlAddress = "SelfChechkIn",
                        //    Params = JsonConvert.SerializeObject(new
                        //    {
                        //        param.Param,
                        //        param.NoRujukan,
                        //        param.Tipe,
                        //        antreanDateTime = antreanDateTime.ToString("HH:mm:ss"),
                        //        nowDateTime = DateTime.Now.ToString("HH:mm:ss"),
                        //        antreanDateTime.Subtract(DateTime.Now).TotalHours
                        //    }),
                        //    Response = JsonConvert.SerializeObject(new
                        //    {
                        //        metadata = new
                        //        {
                        //            Code = (int)HttpStatusCode.NotFound, // 404
                        //            Message = $"Check In Dapat Dilakukan Dalam Waktu {maxCheckIn} Jam Sebelum Waktu Pelananan"
                        //        }
                        //    })
                        //};
                        //logHari.Save();

                        log.Response = JsonConvert.SerializeObject(new
                        {
                            Code = (int)HttpStatusCode.NotFound, // 404
                            Message = $"Check In Dapat Dilakukan Dalam Waktu {maxCheckIn} Jam Sebelum Waktu Pelayanan"
                        });
                        log.Save();

                        return Request.CreateResponse(HttpStatusCode.NotFound, new
                        {
                            metadata = new
                            {
                                Code = (int)HttpStatusCode.NotFound, // 404
                                Message = $"Check In Dapat Dilakukan Dalam Waktu {maxCheckIn} Jam Sebelum Waktu Pelayanan"
                            }
                        });
                    }
                }
            }

            //}

            //if (string.IsNullOrWhiteSpace(param.Param))
            //{
            //    if (new string[] { "RSI" }.Contains(AppSession.Parameter.HealthcareInitial))
            //    {
            //        if (antreanDateTime.Subtract(DateTime.Now).TotalHours > 1)
            //        {
            //            log.Response = JsonConvert.SerializeObject(new
            //            {
            //                Code = (int)HttpStatusCode.NotFound, // 404
            //                Message = $"Check In Dapat Dilakukan Dalam Waktu {maxCheckIn} Jam Sebelum Waktu Pelananan"
            //            });
            //            log.Save();

            //            return Request.CreateResponse(HttpStatusCode.NotFound, new
            //            {
            //                metadata = new
            //                {
            //                    Code = (int)HttpStatusCode.NotFound, // 404
            //                    Message = $"Check In Dapat Dilakukan Dalam Waktu {maxCheckIn} Jam Sebelum Waktu Pelananan"
            //                }
            //            });
            //        }
            //    }
            //}

            var pasien = new Patient();
            pasien.LoadByPrimaryKey(appt.PatientID);

            if (!(pasien.IsActive ?? false) || !(pasien.IsAlive ?? false) || (pasien.IsBlackList ?? false))
            {
                log.Response = JsonConvert.SerializeObject(new
                {
                    Code = (int)HttpStatusCode.NotFound, // 404
                    Message = $"Terdapat Kendala Pada Data Pasien, Silahkan Menghubungi Unit Registrasi Pasien"
                });
                log.Save();

                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.NotFound, // 404
                        Message = $"Terdapat Kendala Pada Data Pasien, Silahkan Menghubungi Unit Registrasi Pasien"
                    }
                });
            }

            var svc = new Common.BPJS.VClaim.v11.Service();

            var poli = new ServiceUnitBridging();
            poli.Query.Where(poli.Query.ServiceUnitID == appt.ServiceUnitID && poli.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
            poli.Query.Load();

            var su = new ServiceUnit();
            su.LoadByPrimaryKey(poli.ServiceUnitID);

            var dokter = new ParamedicBridging();
            dokter.Query.Where(dokter.Query.ParamedicID == appt.ParamedicID && dokter.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
            dokter.Query.Load();

            var pmedic = new Paramedic();
            pmedic.LoadByPrimaryKey(dokter.ParamedicID);

            var antsvc = new Common.BPJS.Antrian.Service();
            var antrean = antsvc.GetAntreanPerKodeBooking(appt.AppointmentNo);
            if (!antrean.Metadata.IsAntrolValid)
            {
                antsvc = new Common.BPJS.Antrian.Service();
                var jadwal = antsvc.GetJadwalDokter(poli.BridgingID.Split(';')[0], appt.AppointmentDate?.Date.ToString("yyyy-MM-dd"));
                if (!jadwal.Metadata.IsAntrolValid)
                {
                    log.Response = JsonConvert.SerializeObject(new
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Jadwal Dokter {pmedic.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                    });
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Jadwal Dokter {pmedic.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                        }
                    });
                }
                else
                {
                    var day = 0;
                    if (appt.AppointmentDate?.Date.DayOfWeek == DayOfWeek.Sunday) day = 7;
                    else day = (int)appt.AppointmentDate?.Date.DayOfWeek;

                    if (jadwal.Response.List == null && !jadwal.Response.List.Any())
                    {
                        log.Response = JsonConvert.SerializeObject(new
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Jadwal Dokter {pmedic.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                        });
                        log.Save();

                        return Request.CreateResponse(HttpStatusCode.Created, new
                        {
                            metadata = new
                            {
                                Code = (int)HttpStatusCode.Created,
                                Message = $"Jadwal Dokter {pmedic.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                            }
                        });
                    }
                }

                var psd = new ParamedicScheduleDate();
                if (!psd.LoadByPrimaryKey(su.ServiceUnitID, pmedic.ParamedicID, appt.AppointmentDate?.Year.ToString(), appt.AppointmentDate.Value.Date))
                {
                    log.Response = JsonConvert.SerializeObject(new
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Jadwal Dokter {pmedic.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                    });
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Jadwal Dokter {pmedic.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                        }
                    });
                }

                var ps = new ParamedicSchedule();
                if (!ps.LoadByPrimaryKey(su.ServiceUnitID, pmedic.ParamedicID, appt.AppointmentDate?.Year.ToString()))
                {
                    log.Response = JsonConvert.SerializeObject(new
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Jadwal Dokter {pmedic.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                    });
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Jadwal Dokter {pmedic.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                        }
                    });
                }
                else
                {
                    if ((ps.QuotaBpjsOnline ?? 0) == 0)
                    {
                        log.Response = JsonConvert.SerializeObject(new
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Kapasitas antrian sudah penuh"
                        });
                        log.Save();

                        return Request.CreateResponse(HttpStatusCode.Created, new
                        {
                            metadata = new
                            {
                                Code = (int)HttpStatusCode.Created,
                                Message = $"Kapasitas antrian sudah penuh"
                            }
                        });
                    }
                }

                if (string.IsNullOrWhiteSpace(psd.OperationalTimeID))
                {
                    log.Response = JsonConvert.SerializeObject(new
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Jadwal Dokter {pmedic.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                    });
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Jadwal Dokter {pmedic.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                        }
                    });
                }

                var ot = new OperationalTime();
                if (!ot.LoadByPrimaryKey(psd.OperationalTimeID))
                {
                    log.Response = JsonConvert.SerializeObject(new
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Jadwal Dokter {pmedic.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                    });
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Jadwal Dokter {pmedic.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                        }
                    });
                }

                var jam = new List<string>() { ot.StartTime1, ot.EndTime1 };
                var jam1 = TimeSpan.ParseExact(jam[0].Trim(), "hh\\:mm", null);
                var jam2 = TimeSpan.ParseExact(jam[1].Trim(), "hh\\:mm", null);

                if (appt.AppointmentDate?.Date == DateTime.Now.Date)
                {
                    if (DateTime.Now.TimeOfDay > jam2)
                    {
                        log.Response = JsonConvert.SerializeObject(new
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Pendaftaran Ke Poli {su.ServiceUnitName} Sudah Tutup Jam {jam2.ToString("hh\\:mm")}"
                        });
                        log.Save();

                        return Request.CreateResponse(HttpStatusCode.Created, new
                        {
                            metadata = new
                            {
                                Code = (int)HttpStatusCode.Created,
                                Message = $"Pendaftaran Ke Poli {su.ServiceUnitName} Sudah Tutup Jam {jam2.ToString("hh\\:mm")}"
                            }
                        });
                    }
                }

                TimeSpan? waktu1 = null, waktu2 = null;

                if (!string.IsNullOrWhiteSpace(ot.StartTime1) && !string.IsNullOrWhiteSpace(ot.EndTime1))
                {
                    var ot1 = TimeSpan.ParseExact(ot.StartTime1, "hh\\:mm", null);
                    var ot2 = TimeSpan.ParseExact(ot.EndTime1, "hh\\:mm", null);

                    if (jam1 == ot1 && jam2 == ot2)
                    {
                        waktu1 = ot1;
                        waktu2 = ot2;
                    }
                }

                if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                {
                    var ot1 = TimeSpan.ParseExact(ot.StartTime2, "hh\\:mm", null);
                    var ot2 = TimeSpan.ParseExact(ot.EndTime2, "hh\\:mm", null);

                    if (jam1 == ot1 && jam2 == ot2)
                    {
                        waktu1 = ot1;
                        waktu2 = ot2;
                    }
                }

                if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                {
                    var ot1 = TimeSpan.ParseExact(ot.StartTime3, "hh\\:mm", null);
                    var ot2 = TimeSpan.ParseExact(ot.EndTime3, "hh\\:mm", null);

                    if (jam1 == ot1 && jam2 == ot2)
                    {
                        waktu1 = ot1;
                        waktu2 = ot2;
                    }
                }

                if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                {
                    var ot1 = TimeSpan.ParseExact(ot.StartTime4, "hh\\:mm", null);
                    var ot2 = TimeSpan.ParseExact(ot.EndTime4, "hh\\:mm", null);

                    if (jam1 == ot1 && jam2 == ot2)
                    {
                        waktu1 = ot1;
                        waktu2 = ot2;
                    }
                }

                if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                {
                    var ot1 = TimeSpan.ParseExact(ot.StartTime5, "hh\\:mm", null);
                    var ot2 = TimeSpan.ParseExact(ot.EndTime5, "hh\\:mm", null);

                    if (jam1 == ot1 && jam2 == ot2)
                    {
                        waktu1 = ot1;
                        waktu2 = ot2;
                    }
                }

                if ((ps.ExamDuration ?? 0) == 0)
                {
                    log.Response = JsonConvert.SerializeObject(new
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Jadwal Dokter {pmedic.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                    });
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Jadwal Dokter {pmedic.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                        }
                    });
                }

                if (waktu1 == null || waktu2 == null)
                {
                    log.Response = JsonConvert.SerializeObject(new
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Jadwal Dokter {pmedic.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                    });
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Jadwal Dokter {pmedic.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                        }
                    });
                }

                {
                    svc = new Common.BPJS.VClaim.v11.Service();
                    var srkresponse = svc.GetRencanaKontrolByNoPeserta(appt.AppointmentDate?.Date.ToString("MM"), appt.AppointmentDate?.Date.ToString("yyyy"), string.IsNullOrWhiteSpace(appt.GuarantorCardNo) ? pasien.GuarantorCardNo : appt.GuarantorCardNo, Common.BPJS.VClaim.Enum.FilterRencanaKontrol.TanggalRencanaKontrol);
                    var srk = srkresponse.Response.List.Where(r => r.TglRencanaKontrol == appt.AppointmentDate?.ToString("yyyy-MM-dd")).SingleOrDefault();
                    if (srk == null)
                    {
                        log.Response = JsonConvert.SerializeObject(new
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Data rencana kontrol tidak ditemukan"
                        });
                        log.Save();

                        return Request.CreateResponse(HttpStatusCode.Created, new
                        {
                            metadata = new
                            {
                                Code = (int)HttpStatusCode.Created,
                                Message = $"Data rencana kontrol tidak ditemukan"
                            }
                        });
                    }

                    if (string.IsNullOrWhiteSpace(appt.ReferenceNumber.Trim()))
                    {
                        appt.ReferenceNumber = srk.NoSuratKontrol;
                        appt.Save();
                    }

                    var appts = new AppointmentCollection();
                    appts.Query.Where(appt.Query.ServiceUnitID == poli.ServiceUnitID,
                        appts.Query.ParamedicID == dokter.ParamedicID,
                        //appts.Query.AppointmentDate.Date() == appt.AppointmentDate?.Date,
                        appts.Query.AppointmentDate >= appt.AppointmentDate?.Date, appts.Query.AppointmentDate < appt.AppointmentDate?.Date.AddDays(1),
                        appts.Query.AppointmentTime >= jam[0].Trim(),
                        appts.Query.AppointmentTime <= jam[1].Trim());
                    //if (AppSession.Parameter.HealthcareID != "RSES") // kerinci
                    appts.Query.Where(appts.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
                    var apptAvailable = appts.Query.Load();

                    var jeniskunjungan = 0;
                    {
                        svc = new Common.BPJS.VClaim.v11.Service();
                        var rujukanResponse = svc.GetRujukan(true, appt.ReferenceNumber, Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);
                        if (!rujukanResponse.MetaData.IsValid)
                        {
                            svc = new Common.BPJS.VClaim.v11.Service();
                            rujukanResponse = svc.GetRujukan(true, appt.ReferenceNumber, Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                            if (rujukanResponse.MetaData.IsValid) jeniskunjungan = 4;
                            else
                            {
                                svc = new Common.BPJS.VClaim.v11.Service();
                                var kontrol = svc.GetRencanaKontrolByNoSuratKontrol(appt.ReferenceNumber);
                                if (kontrol.MetaData.IsValid) jeniskunjungan = 3;
                            }
                        }
                        else jeniskunjungan = 1;
                    }

                    var tambah = new Common.BPJS.Antrian.Tambah.Request.Root()
                    {
                        Kodebooking = appt.AppointmentNo,
                        Jenispasien = AppSession.Parameter.GuarantorAskesID.Contains(appt.GuarantorID) ? "JKN" : "NON JKN",
                        Nomorkartu = string.IsNullOrWhiteSpace(appt.GuarantorCardNo) ? pasien.GuarantorCardNo : appt.GuarantorCardNo,
                        Nik = string.IsNullOrWhiteSpace(appt.Ssn) ? pasien.Ssn : appt.Ssn,
                        Nohp = string.IsNullOrWhiteSpace(appt.MobilePhoneNo) ? pasien.MobilePhoneNo : appt.MobilePhoneNo,
                        Kodepoli = poli.BridgingID.Split(';')[1],
                        Namapoli = su.ServiceUnitName,
                        Pasienbaru = string.IsNullOrWhiteSpace(appt.PatientID) ? 1 : 0,
                        Norm = string.IsNullOrWhiteSpace(appt.PatientID) ? string.Empty : pasien.MedicalNo,
                        Tanggalperiksa = appt.AppointmentDate.Value.Date.ToString("yyyy-MM-dd"),
                        Kodedokter = dokter.BridgingID.ToInt(),
                        Namadokter = pmedic.ParamedicName,
                        Jampraktek = $"{waktu1.Value.ToString("hh\\:mm")}-{waktu2.Value.ToString("hh\\:mm")}",
                        Jeniskunjungan = jeniskunjungan,
                        Nomorreferensi = srk.NoSuratKontrol,
                        Nomorantrean = $"{(string.IsNullOrWhiteSpace(su.QueueCode) ? su.ShortName : su.QueueCode)}{(string.IsNullOrWhiteSpace(pmedic.ParamedicQueueCode) ? pmedic.ParamedicInitial : pmedic.ParamedicQueueCode)} - {(appt.AppointmentQue ?? 1)}",
                        Angkaantrean = appt.AppointmentQue ?? 1,
                        Estimasidilayani = Convert.ToInt64(antreanDateTime.AddHours(-7).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds),
                        Sisakuotajkn = (ps.QuotaBpjsOnline ?? 0) - appts.Count(a => AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) <= 0 ? 0 : (ps.QuotaBpjsOnline ?? 0) - appts.Count(a => AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)),
                        Kuotajkn = ps.QuotaBpjsOnline ?? 0,
                        Sisakuotanonjkn = (ps.QuotaOnline ?? 0) - appts.Count(a => !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) <= 0 ? 0 : (ps.QuotaOnline ?? 0) - appts.Count(a => !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)),
                        Kuotanonjkn = ps.QuotaOnline ?? 0,
                        Keterangan = "Peserta harap 30 menit lebih awal guna pencatatan administrasi"
                    };

                    antsvc = new Common.BPJS.Antrian.Service();
                    var response = antsvc.TambahAntrian(tambah);

                    var logTambah = new WebServiceAPILog
                    {
                        DateRequest = DateTime.Now,
                        IPAddress = "10.200.200.188",
                        UrlAddress = "SelfChechkIn",
                        Params = JsonConvert.SerializeObject(tambah),
                        Response = JsonConvert.SerializeObject(response)
                    };
                    logTambah.Save();

                    if (!response.Metadata.IsAntrolValid)
                    {
                        log.Response = JsonConvert.SerializeObject(response);
                        log.Save();

                        return Request.CreateResponse(HttpStatusCode.NotFound, new
                        {
                            metadata = new
                            {
                                Code = (int)HttpStatusCode.NotFound, // 404
                                Message = response.Metadata.Message
                            }
                        });
                    }
                }
            }

            var leave = new ParamedicLeave();
            leave.Query.Where(leave.Query.ParamedicID == appt.ParamedicID && leave.Query.StartDate >= DateTime.Now.Date && leave.Query.EndDate <= DateTime.Now.Date && leave.Query.IsApproved == true);
            if (leave.Query.Load())
            {
                log.Response = JsonConvert.SerializeObject(new
                {
                    Code = (int)HttpStatusCode.NotFound, // 404
                    Message = $"Dokter Tujuan Tidak Hadir, Silahkan Ambil Antrian dan Menghubungi Unit Registrasi"
                });
                log.Save();
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.NotFound, // 404
                        Message = $"Dokter Tujuan Tidak Hadir, Silahkan Ambil Antrian dan Menghubungi Unit Registrasi"
                    }
                });
            }
            if (appt.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusClosed)
            {
                log.Response = JsonConvert.SerializeObject(new
                {
                    Code = (int)HttpStatusCode.Found, // 302
                    Message = $"Anda Sudah Melakukan Check In Pada Jam {appt.LastUpdateDateTime?.ToString("HH:mm:ss")}"
                });
                log.Save();

                return Request.CreateResponse(HttpStatusCode.Found, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.Found, // 302
                        Message = $"Anda Sudah Melakukan Check In Pada Jam {appt.LastUpdateDateTime?.ToString("HH:mm:ss")}"
                    }
                });
            }
            else if (appt.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusCancel)
            {
                log.Response = JsonConvert.SerializeObject(new
                {
                    Code = (int)HttpStatusCode.Found, // 302
                    Message = $"Nomor Antrian Sudah Dibatalkan"
                });
                log.Save();

                return Request.CreateResponse(HttpStatusCode.Found, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.Found, // 302
                        Message = $"Nomor Antrian Sudah Dibatalkan"
                    }
                });
            }

            var visit = new Registration();
            visit.Query.es.Top = 1;
            visit.Query.Where
            (
                visit.Query.PatientID == pasien.PatientID,
                visit.Query.IsVoid == false
            );

            // create sep by no rujukan
            if (!visit.Query.Load())
            {
                var logTask = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "antrol",
                    UrlAddress = "SelfChechkIn",
                    Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                    {
                        Kodebooking = appt.AppointmentNo,
                        Taskid = 1,
                        Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                    }),
                    Response = string.Empty,
                    Totalms = 0
                };
                var svc1 = new Common.BPJS.Antrian.Service();
                var response1 = svc1.UpdateWaktuAntrian(JsonConvert.DeserializeObject<Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root>(log.Params));
                logTask.Response = JsonConvert.SerializeObject(response1);
                logTask.Save();

                logTask = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "antrol",
                    UrlAddress = "SelfChechkIn",
                    Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                    {
                        Kodebooking = appt.AppointmentNo,
                        Taskid = 2,
                        Waktu = Convert.ToInt64(DateTimeOffset.Now.AddMinutes(3).ToUnixTimeMilliseconds())
                    })
                };
                svc1 = new Common.BPJS.Antrian.Service();
                var response2 = svc1.UpdateWaktuAntrian(JsonConvert.DeserializeObject<Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root>(log.Params));
                logTask.Response = JsonConvert.SerializeObject(response2);
                logTask.Save();

                log.Response = JsonConvert.SerializeObject(new
                {
                    Code = (int)HttpStatusCode.Found, // 302
                    Message = $"Anda adalah pasien baru, Silahkan ambil antrian dan menghubungi unit registrasi"
                });
                log.Save();

                return Request.CreateResponse(HttpStatusCode.Found, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.Found, // 302
                        Message = $"Anda adalah pasien baru, Silahkan ambil antrian dan menghubungi unit registrasi"
                    }
                });
            }

            var status = ConfigurationManager.AppSettings["BpjsAntrianStatus"] == null ? true : (ConfigurationManager.AppSettings["BpjsAntrianStatus"] == "1" ? true : false);
            var postranap = false; // rajal
            var noRujukan = string.Empty;
            var noSkdp = string.Empty;
            var asalRujukan = Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1;
            var isrujukan = false;

            if (string.IsNullOrWhiteSpace(appt.ReferenceNumber))
            {
                svc = new Service();
                var skdpresponse = svc.GetRencanaKontrolByNoPeserta(appt.AppointmentDate?.Date.ToString("MM"), appt.AppointmentDate?.Date.ToString("yyyy"), string.IsNullOrWhiteSpace(appt.GuarantorCardNo) ? pasien.GuarantorCardNo : appt.GuarantorCardNo,
                    Common.BPJS.VClaim.Enum.FilterRencanaKontrol.TanggalRencanaKontrol);
                var skdp = skdpresponse.Response.List.Where(r => r.TglRencanaKontrol == appt.AppointmentDate?.ToString("yyyy-MM-dd")).SingleOrDefault();
                if (skdp == null)
                {
                    log.Response = JsonConvert.SerializeObject(new
                    {
                        Code = (int)HttpStatusCode.Found, // 302
                        Message = $"Nomor Referensi Rujukan/SKDP tidak ditemukan"
                    });
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Found, new
                    {
                        metadata = new
                        {
                            Code = (int)HttpStatusCode.Found, // 302
                            Message = $"Nomor Referensi Rujukan/SKDP tidak ditemukan"
                        }
                    });
                }
                else
                {
                    appt.ReferenceNumber = skdp.NoSuratKontrol;
                    appt.Save();
                }
            }

            var rujukan = new Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan();
            svc = new Common.BPJS.VClaim.v11.Service();
            rujukan = svc.GetRujukan(true, appt.ReferenceNumber, Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);
            if (!rujukan.MetaData.IsValid)
            {
                svc = new Common.BPJS.VClaim.v11.Service();
                rujukan = svc.GetRujukan(true, appt.ReferenceNumber, Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                if (!rujukan.MetaData.IsValid)
                {
                    svc = new Common.BPJS.VClaim.v11.Service();
                    var kontrol = svc.GetRencanaKontrolByNoSuratKontrol(appt.ReferenceNumber);
                    if (kontrol.MetaData.IsValid)
                    {
                        if (kontrol.Response.ProvPerujuk == null)
                        {
                            if (kontrol.Response.Sep != null)
                            {
                                if (kontrol.Response.Sep.ProvPerujuk != null) noRujukan = kontrol.Response.Sep.ProvPerujuk.NoRujukan;
                            }
                        }
                        else noRujukan = kontrol.Response.ProvPerujuk.NoRujukan;

                        svc = new Common.BPJS.VClaim.v11.Service();
                        rujukan = svc.GetRujukan(true, noRujukan, Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);
                        if (rujukan.MetaData.IsValid) asalRujukan = Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1;
                        else
                        {
                            svc = new Common.BPJS.VClaim.v11.Service();
                            rujukan = svc.GetRujukan(true, noRujukan, Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                            if (rujukan.MetaData.IsValid) asalRujukan = Common.BPJS.VClaim.Enum.JenisFaskes.RS;
                        }

                        noSkdp = appt.ReferenceNumber;
                        isrujukan = false;
                    }
                    else
                        return Request.CreateResponse(HttpStatusCode.NotFound, new
                        {
                            metadata = new
                            {
                                Code = (int)HttpStatusCode.NotFound, // 404
                                Message = kontrol.MetaData.Message
                            }
                        });
                }
                else
                {
                    // cari no rujukan
                    asalRujukan = Common.BPJS.VClaim.Enum.JenisFaskes.RS;
                    noRujukan = appt.ReferenceNumber;
                    isrujukan = true;
                }
            }
            else
            {
                // cari no rujukan
                asalRujukan = Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1;
                noRujukan = appt.ReferenceNumber;
                isrujukan = true;
            }

            if (!isrujukan)
            {
                svc = new Common.BPJS.VClaim.v11.Service();
                var srkresponse = svc.GetRencanaKontrolByNoPeserta(appt.AppointmentDate?.Date.ToString("MM"), appt.AppointmentDate?.Date.ToString("yyyy"), string.IsNullOrWhiteSpace(appt.GuarantorCardNo) ? pasien.GuarantorCardNo : appt.GuarantorCardNo,
                    Common.BPJS.VClaim.Enum.FilterRencanaKontrol.TanggalRencanaKontrol);
                var srk = srkresponse.Response.List.Where(r => r.TglRencanaKontrol == appt.AppointmentDate?.ToString("yyyy-MM-dd")).SingleOrDefault();
                if (srk == null)
                {
                    log.Response = JsonConvert.SerializeObject(new
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Data rencana kontrol tidak ditemukan"
                    });
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Data rencana kontrol tidak ditemukan"
                        }
                    });
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(srk.NoSepAsalKontrol))
                    {
                        svc = new Common.BPJS.VClaim.v11.Service();
                        var responseSep = svc.GetSep(srk.NoSepAsalKontrol);
                        if (responseSep.MetaData.IsValid)
                        {
                            if (responseSep.Response.JnsPelayanan.ToLower() == "rawat inap")
                            {
                                postranap = true;
                                noRujukan = srk.NoSepAsalKontrol;
                                asalRujukan = Common.BPJS.VClaim.Enum.JenisFaskes.RS;
                            }
                        }
                    }
                }
            }

            if (postranap)
            {
                var sep = new BpjsSEP();
                if (!sep.LoadByPrimaryKey(noRujukan))
                {
                    log.Response = JsonConvert.SerializeObject(new
                    {
                        Code = (int)HttpStatusCode.NotFound, // 404
                        Message = "No SEP Rujukan tidak ditemukan"
                    });
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        metadata = new
                        {
                            Code = (int)HttpStatusCode.NotFound, // 404
                            Message = "No SEP Rujukan tidak ditemukan"
                        }
                    });
                }

                svc = new Common.BPJS.VClaim.v11.Service();
                var ppk = svc.GetFaskes(ConfigurationManager.AppSettings["BPJSHospitalID"], Common.BPJS.VClaim.Enum.JenisFaskes.RS);

                rujukan = new Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan()
                {
                    Response = new Common.BPJS.VClaim.v11.Rujukan.Select.Response()
                    {
                        Rujukan = new Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2()
                        {
                            Peserta = new Common.BPJS.VClaim.v11.Rujukan.Select.Peserta()
                            {
                                NoKartu = sep.NomorKartu,
                                HakKelas = new Common.BPJS.VClaim.v11.Rujukan.Select.HakKelas()
                                {
                                    Keterangan = "Kelas 3"
                                },
                                Nik = sep.Nik
                            },
                            PoliRujukan = new Common.BPJS.VClaim.v11.Rujukan.Select.PoliRujukan()
                            {
                                Kode = poli.BridgingID.Split(';')[0]
                            },
                            TglKunjungan = appt.AppointmentDate?.ToString("yyyy-MM-dd"),
                            NoKunjungan = sep.NoSEP,
                            ProvPerujuk = new Common.BPJS.VClaim.v11.Rujukan.Select.ProvPerujuk()
                            {
                                Kode = ConfigurationManager.AppSettings["BPJSHospitalID"],
                                Nama = $"{ConfigurationManager.AppSettings["BPJSHospitalID"]} - {string.Empty}"
                            },
                            Pelayanan = new Common.BPJS.VClaim.v11.Rujukan.Select.Pelayanan()
                            {
                                Kode = "2"
                            },
                            Diagnosa = new Common.BPJS.VClaim.v11.Rujukan.Select.Diagnosa()
                            {
                                Kode = sep.DiagnosaAwal
                            }
                        }
                    }
                };
            }

            var jumlahSep = new Common.BPJS.VClaim.v20.Rujukan.SelectDataJumlahSEPRujukan.Root();
            if (!postranap)
            {
                svc = new Common.BPJS.VClaim.v11.Service();
                jumlahSep = svc.GetDataJumlahSEPRujukan(Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1, noRujukan);
                if (!jumlahSep.MetaData.IsValid)
                {
                    svc = new Common.BPJS.VClaim.v11.Service();
                    jumlahSep = svc.GetDataJumlahSEPRujukan(Common.BPJS.VClaim.Enum.JenisFaskes.RS, noRujukan);
                }
            }
            else
            {
                jumlahSep = new Common.BPJS.VClaim.v20.Rujukan.SelectDataJumlahSEPRujukan.Root()
                {
                    MetaData = new Common.BPJS.VClaim.v20.Rujukan.SelectDataJumlahSEPRujukan.MetaData()
                    {
                        Code = "200",
                        Message = ""
                    },
                    Response = new Common.BPJS.VClaim.v20.Rujukan.SelectDataJumlahSEPRujukan.TResponse()
                    {
                        JumlahSEP = "0"
                    }
                };
            }

            var paramedic = new ParamedicBridging();
            paramedic.Query.Where(paramedic.Query.ParamedicID == appt.ParamedicID, paramedic.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
            paramedic.Query.Load();

            svc = new Common.BPJS.VClaim.v11.Service();
            var peserta = svc.GetPeserta(Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta, rujukan.Response.Rujukan.Peserta.NoKartu, DateTime.Now.Date);
            if (!peserta.MetaData.IsValid)
            {
                log.Response = JsonConvert.SerializeObject(new
                {
                    Code = (int)HttpStatusCode.NotFound, // 404
                    Message = peserta.MetaData.Message
                });
                log.Save();

                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.NotFound, // 404
                        Message = peserta.MetaData.Message
                    }
                });
            }

            var bs = new BpjsSEP();
            if (status)
            {
                var ws = new Common.BPJS.VClaim.v11.Service();
                var riwayat = ws.GetDataHistoriPelayananPeserta(rujukan.Response.Rujukan.Peserta.NoKartu, (appt.AppointmentDate ?? new DateTime()).Date.AddDays(-1).ToString("yyyy-MM-dd"), (appt.AppointmentDate ?? new DateTime()).Date);
                if (riwayat.MetaData.IsValid)
                {
                    if (riwayat.Response.Histori != null)
                    {
                        var data = riwayat.Response.Histori.SingleOrDefault(r => r.TglSep == (appt.AppointmentDate ?? new DateTime()).ToString("yyyy-MM-dd") && r.Poli.ToLower() != "igd");
                        if (data != null)
                        {
                            log.Response = JsonConvert.SerializeObject(new
                            {
                                Code = (int)HttpStatusCode.NotFound,
                                Message = string.Format("Pasien telah melakukan kunjungan di {0}, pada tanggal {1}{2}", data.PpkPelayanan, data.TglSep, ", " + data.Poli)
                            });
                            log.Save();

                            return Request.CreateResponse(HttpStatusCode.NotFound, new
                            {
                                metadata = new
                                {
                                    Code = (int)HttpStatusCode.NotFound,
                                    Message = string.Format("Pasien telah melakukan kunjungan di {0}, pada tanggal {1}{2}", data.PpkPelayanan, data.TglSep, ", " + data.Poli)
                                }
                            });
                        }
                    }
                }

                poli = new ServiceUnitBridging();
                poli.Query.Where(poli.Query.ServiceUnitID == appt.ServiceUnitID && poli.Query.SRBridgingType == AppEnum.BridgingType.BPJS.ToString());
                poli.Query.Load();

                dokter = new ParamedicBridging();
                dokter.Query.Where(dokter.Query.ParamedicID == appt.ParamedicID && dokter.Query.SRBridgingType == AppEnum.BridgingType.BPJS.ToString());
                dokter.Query.Load();

                svc = new Common.BPJS.VClaim.v11.Service();
                var tsep = new Common.BPJS.VClaim.v20.Sep.InsertRequest.TSep()
                {
                    Rujukan = new Common.BPJS.VClaim.v20.Sep.InsertRequest.Rujukan()
                    {
                        AsalRujukan = asalRujukan.ToString(),
                        TglRujukan = rujukan.Response.Rujukan.TglKunjungan,
                        NoRujukan = rujukan.Response.Rujukan.NoKunjungan,
                        PpkRujukan = rujukan.Response.Rujukan.ProvPerujuk.Kode
                    },
                    Poli = new Common.BPJS.VClaim.v20.Sep.InsertRequest.Poli()
                    {
                        Tujuan = rujukan.Response.Rujukan.PoliRujukan.Kode,
                        Eksekutif = "0"
                    },
                    Cob = new Common.BPJS.VClaim.v20.Sep.InsertRequest.TCob()
                    {
                        Cob = "0"
                    },
                    Katarak = new Common.BPJS.VClaim.v20.Sep.InsertRequest.TKatarak()
                    {
                        Katarak = "0"
                    },
                    Jaminan = new Common.BPJS.VClaim.v20.Sep.InsertRequest.Jaminan()
                    {
                        LakaLantas = "0",
                        NoLp = string.Empty,
                        Penjamin = new Common.BPJS.VClaim.v20.Sep.InsertRequest.Penjamin()
                        {
                            TglKejadian = string.Empty,
                            Keterangan = string.Empty,
                            Suplesi = new Common.BPJS.VClaim.v20.Sep.InsertRequest.TSuplesi()
                            {
                                Suplesi = "0",
                                NoSepSuplesi = string.Empty,
                                LokasiLaka = new Common.BPJS.VClaim.v20.Sep.InsertRequest.LokasiLaka()
                                {
                                    KdPropinsi = string.Empty,
                                    KdKabupaten = string.Empty,
                                    KdKecamatan = string.Empty
                                }
                            }
                        }
                    },
                    TujuanKunj = jumlahSep.MetaData.IsValid ? (jumlahSep.Response.JumlahSEP.ToInt() < 1 ? "0" : new string[] { "IRM" }.Contains(rujukan.Response.Rujukan.PoliRujukan.Kode) ? "1" : "2") : "0",
                    FlagProcedure = jumlahSep.MetaData.IsValid ? (jumlahSep.Response.JumlahSEP.ToInt() < 1 ? string.Empty : new string[] { "IRM" }.Contains(rujukan.Response.Rujukan.PoliRujukan.Kode) ? "1" : string.Empty) : string.Empty,
                    KdPenunjang = jumlahSep.MetaData.IsValid ? (jumlahSep.Response.JumlahSEP.ToInt() < 1 ? string.Empty : new string[] { "IRM" }.Contains(rujukan.Response.Rujukan.PoliRujukan.Kode) ? "3" : string.Empty) : string.Empty,
                    AssesmentPel = jumlahSep.MetaData.IsValid ? (jumlahSep.Response.JumlahSEP.ToInt() < 1 ? string.Empty : new string[] { "IRM" }.Contains(rujukan.Response.Rujukan.PoliRujukan.Kode) ? string.Empty : "5") : string.Empty,
                    Skdp = new Common.BPJS.VClaim.v20.Sep.InsertRequest.Skdp()
                    {
                        NoSurat = jumlahSep.MetaData.IsValid ? (jumlahSep.Response.JumlahSEP.ToInt() < 1 ? (postranap ? noSkdp : string.Empty) : noSkdp) : string.Empty,
                        KodeDPJP = jumlahSep.MetaData.IsValid ? (jumlahSep.Response.JumlahSEP.ToInt() < 1 ? (postranap ? paramedic.BridgingID : string.Empty) : paramedic.BridgingID) : string.Empty
                    },
                    NoKartu = rujukan.Response.Rujukan.Peserta.NoKartu,
                    TglSep = appt.AppointmentDate?.ToString("yyyy-MM-dd"),
                    PpkPelayanan = ConfigurationManager.AppSettings["BPJSHospitalID"],
                    JnsPelayanan = rujukan.Response.Rujukan.Pelayanan.Kode,
                    KlsRawat = new Common.BPJS.VClaim.v20.Sep.InsertRequest.KlsRawat()
                    {
                        KlsRawatHak = "3",
                        KlsRawatNaik = string.Empty,
                        Pembiayaan = string.Empty,
                        PenanggungJawab = string.Empty
                    },
                    DpjpLayan = paramedic.BridgingID,
                    NoMR = pasien.MedicalNo,
                    Catatan = appt.Notes,
                    DiagAwal = rujukan.Response.Rujukan.Diagnosa.Kode,
                    NoTelp = appt.MobilePhoneNo,
                    User = "kiosk"
                };

                var insert = svc.Insert(tsep);
                {
                    var logSep = new WebServiceAPILog();
                    logSep.DateRequest = DateTime.Now;
                    logSep.IPAddress = "antrol";
                    logSep.UrlAddress = "SelfChechkIn";
                    logSep.Params = JsonConvert.SerializeObject(tsep);
                    logSep.Response = JsonConvert.SerializeObject(insert);
                    logSep.Save();
                }
                if (!insert.MetaData.IsValid)
                {
                    log.Response = JsonConvert.SerializeObject(new
                    {
                        Code = (int)HttpStatusCode.NotAcceptable, // 406
                        Message = insert.MetaData.Message
                    });
                    log.Save();

                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, new
                    {
                        metadata = new
                        {
                            Code = (int)HttpStatusCode.NotAcceptable, // 406
                            Message = insert.MetaData.Message
                        }
                    });
                }

                // update table Appointment
                appt.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusClosed;
                appt.LastCreateDateTime = DateTime.Now;

                #region insert ke table BpjsSep
                bs.NoSEP = insert.Response.Sep.NoSep;
                bs.NomorKartu = insert.Response.Sep.Peserta.NoKartu;
                bs.TanggalSEP = DateTime.ParseExact(insert.Response.Sep.TglSep, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None);
                bs.TanggalRujukan = DateTime.ParseExact(rujukan.Response.Rujukan.TglKunjungan, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None);
                bs.NoRujukan = rujukan.Response.Rujukan.NoKunjungan;
                bs.PPKRujukan = rujukan.Response.Rujukan.ProvPerujuk.Kode;
                bs.NamaPPKRujukan = rujukan.Response.Rujukan.ProvPerujuk.Nama;
                bs.PPKPelayanan = ConfigurationManager.AppSettings["BPJSHospitalID"];
                bs.JenisPelayanan = rujukan.Response.Rujukan.Pelayanan.Kode;
                bs.Catatan = string.Empty;// rujukan.Response.Rujukan.Keluhan;
                bs.DiagnosaAwal = rujukan.Response.Rujukan.Diagnosa.Kode;
                bs.PoliTujuan = rujukan.Response.Rujukan.PoliRujukan.Kode;
                bs.KelasRawat = string.Empty;
                bs.LakaLantas = "0";
                bs.NoLP = string.Empty;
                bs.User = "kiosk";
                bs.NoMR = pasien.MedicalNo;
                bs.TanggalPulang = bs.TanggalSEP;
                bs.NoTransaksi = appt.AppointmentNo;
                bs.NamaPasien = peserta.Response.Peserta.Nama;
                bs.Nik = string.IsNullOrWhiteSpace(rujukan.Response.Rujukan.Peserta.Nik) ? string.Empty : rujukan.Response.Rujukan.Peserta.Nik;
                bs.JenisKelamin = peserta.Response.Peserta.Sex;
                bs.TanggalLahir = DateTime.ParseExact(peserta.Response.Peserta.TglLahir, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None);
                bs.JenisPeserta = peserta.Response.Peserta.JenisPeserta.Keterangan;
                bs.DetailKeanggotaan = appt.MobilePhoneNo;
                bs.PatientID = pasien.PatientID;
                bs.LokasiLaka = string.Empty;
                bs.NamaKelasRawat = string.Empty;
                bs.IsEksekutif = false;
                bs.IsCob = false;
                bs.PenjaminLaka = string.Empty;
                bs.NamaCob = string.Empty;
                bs.StatusPeserta = peserta.Response.Peserta.StatusPeserta.Keterangan;
                bs.Umur = peserta.Response.Peserta.Umur.UmurSaatPelayanan;
                bs.JenisRujukan = asalRujukan.ToString();
                bs.IsKatarak = false;
                bs.str.TglKejadian = string.Empty;
                bs.IsSuplesi = false;
                bs.NoSepSuplesi = string.Empty;
                bs.KodePropinsi = string.Empty;
                bs.KodeKabupaten = string.Empty;
                bs.KodeKecamatan = string.Empty;
                bs.KodeDpjp = paramedic.BridgingID;
                bs.FromRegistrationNo = string.Empty;
                bs.ProlanisPRB = string.Empty;
                bs.LastUpdateByUserID = "kiosk";
                bs.LastUpdateDateTime = DateTime.Now;
                bs.KodeDpjpPelayanan = tsep.DpjpLayan;
                bs.KlsRawatNaik = string.Empty;
                bs.Pembiayaan = string.Empty;
                bs.TujuanKunj = tsep.TujuanKunj;
                bs.FlagProcedure = tsep.FlagProcedure;
                bs.KdPenunjang = tsep.KdPenunjang;
                bs.AssesmentPel = tsep.AssesmentPel;
                bs.KodeDpjpKontrol = tsep.DpjpLayan;
                bs.NoSkdp = isrujukan ? string.Empty : noSkdp;
                bs.KlsHak = string.IsNullOrWhiteSpace(rujukan.Response.Rujukan.Peserta.HakKelas.Keterangan) ? "Kelas 3" : rujukan.Response.Rujukan.Peserta.HakKelas.Keterangan;
                bs.Save();
                #endregion
            }

            var reg = new Registration();
            try
            {
                appt.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusClosed;

                // insert / update ke registrasi dll
                #region Registration
                reg.Query.Where(reg.Query.AppointmentNo == appt.AppointmentNo);
                reg.Query.es.Top = 1;
                if (!reg.Query.Load())
                {
                    reg = new Registration();

                    var autoNumberReg = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, BusinessObject.Reference.TransactionCode.Registration, AppSession.Parameter.OutPatientDepartmentID);
                    reg.RegistrationNo = autoNumberReg.LastCompleteNumber;
                    autoNumberReg.Save();
                }

                reg.str.ParamedicID = appt.ParamedicID;

                var sup = new ServiceUnitParamedic();
                sup.Query.Where(sup.Query.ServiceUnitID == appt.ServiceUnitID, sup.Query.ParamedicID == appt.ParamedicID);
                sup.Query.Load();

                var room = new ServiceRoom();
                room.Query.es.Top = 1;
                room.Query.Where(room.Query.ServiceUnitID == appt.ServiceUnitID);
                room.Query.Load();

                reg.str.RoomID = string.IsNullOrWhiteSpace(sup.DefaultRoomID) ? room.RoomID : sup.DefaultRoomID;

                reg.PhysicianSenders = String.Empty;
                reg.GuarantorID = AppSession.Parameter.GuarantorAskesID[0];
                reg.PatientID = pasien.PatientID;

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(appt.ServiceUnitID);
                if (string.IsNullOrEmpty(unit.DefaultChargeClassID))
                {
                    reg.ClassID = AppSession.Parameter.OutPatientClassID;
                    reg.ChargeClassID = AppSession.Parameter.OutPatientClassID;
                    reg.str.CoverageClassID = AppSession.Parameter.OutPatientClassID;
                }
                else
                {
                    reg.ClassID = unit.DefaultChargeClassID;
                    reg.ChargeClassID = unit.DefaultChargeClassID;
                    reg.str.CoverageClassID = unit.DefaultChargeClassID;
                }

                reg.SRRegistrationType = AppConstant.RegistrationType.OutPatient;
                reg.RegistrationDate = appt.AppointmentDate;
                reg.RegistrationTime = appt.AppointmentTime;
                reg.AppointmentNo = appt.AppointmentNo;
                reg.AgeInYear = Convert.ToByte(Helper.GetAgeInYear(appt.DateOfBirth ?? DateTime.Now));
                reg.AgeInMonth = Convert.ToByte(Helper.GetAgeInMonth(appt.DateOfBirth ?? DateTime.Now));
                reg.AgeInDay = Convert.ToByte(Helper.GetAgeInDay(appt.DateOfBirth ?? DateTime.Now));
                reg.SRShift = Registration.GetShiftID();
                reg.AccountNo = string.Empty;
                reg.InsuranceID = string.Empty;
                reg.SmfID = string.Empty;
                reg.SRPatientInType = "PatientInType-001"; // Walk In / Datang Sendiri
                reg.IsDisability = false;
                reg.ReferByParamedicID = string.Empty;
                reg.SRPatientCategory = "PatientCategory-003"; // Pasien Asuransi / Insurance
                reg.SRPatientInCondition = string.Empty;
                reg.SRERCaseType = string.Empty;
                reg.SRVisitReason = "001"; // Konsultasi / Penyakit
                reg.ReasonsForTreatmentID = string.Empty;
                reg.ReasonsForTreatmentDescID = string.Empty;
                reg.CauseOfAccident = string.Empty;

                var guarantor = new Guarantor();
                guarantor.LoadByPrimaryKey(appt.GuarantorID);

                reg.SRBussinesMethod = guarantor.SRBusinessMethod;
                reg.PlavonAmount = 0;
                reg.PatientAdm = 0;
                reg.GuarantorAdm = 0;
                reg.ServiceUnitID = unit.ServiceUnitID;
                reg.DepartmentID = unit.DepartmentID;

                var suvt = new ServiceUnitVisitType();
                suvt.Query.es.Top = 1;
                suvt.Query.Where(suvt.Query.ServiceUnitID == appt.ServiceUnitID);
                suvt.Query.Load();
                reg.str.VisitTypeID = suvt.VisitTypeID;
                reg.str.SRReferralGroup = asalRujukan.ToString() == Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1.ToString() ? "04" : "08"; // 04 = KLINIK, 08 = RUMAH SAKIT
                reg.str.ReferralID = string.Empty;
                reg.ReferralName = bs.NamaPPKRujukan;
                reg.ExternalQueNo = string.Empty;
                reg.Anamnesis = string.Empty;
                reg.Complaint = string.Empty;
                reg.InitialDiagnose = status ? bs.DiagnosaAwal : string.Empty; //bs.DiagnosaAwal;
                reg.MedicationPlanning = string.Empty;
                reg.BpjsPackageID = string.Empty;
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
                reg.PlavonAmount = 0;
                reg.Notes = status ? (string.IsNullOrWhiteSpace(bs.Catatan) ? string.Empty : bs.Catatan) : string.Empty;// bs.Catatan;
                reg.IsFromDispensary = false;
                reg.IsSkipAutoBill = false;
                reg.IsClusterAssessment = false;

                visit = new Registration();
                visit.Query.es.Top = 1;
                visit.Query.Where
                    (
                        visit.Query.PatientID == reg.PatientID,
                        visit.Query.ServiceUnitID == reg.ServiceUnitID,
                        visit.Query.IsVoid == false
                    );
                reg.IsNewVisit = !visit.Query.Load();

                if (reg.es.IsAdded)
                {
                    reg.LastCreateUserID = "kiosk";
                    reg.LastCreateDateTime = (new DateTime()).NowAtSqlServer();
                }

                reg.FromRegistrationNo = string.Empty;
                reg.SREmployeeRelationship = string.Empty;
                reg.PersonID = null;
                reg.EmployeeNumber = null;
                reg.GuarantorCardNo = appt.GuarantorCardNo;
                reg.IsGlobalPlafond = guarantor.SRBusinessMethod == AppSession.Parameter.BusinessMethodFlavon;
                reg.BpjsSepNo = status ? bs.NoSEP : string.Empty; //bs.NoSEP;
                reg.MembershipDetailID = Registration.GetMembershipDetailId(reg.PatientID, reg.RegistrationDate.Value.Date);
                reg.LastUpdateByUserID = "kiosk";
                reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                visit = new Registration();
                visit.Query.es.Top = 1;
                visit.Query.Where
                    (
                        visit.Query.PatientID == reg.PatientID,
                        visit.Query.IsVoid == false
                    );
                reg.IsNewPatient = !visit.Query.Load();

                reg.RegistrationQue = appt.AppointmentQue ?? 0;

                #endregion

                #region Patient
                if (!string.IsNullOrEmpty(reg.FromRegistrationNo))
                {
                    var regFrom = new Registration();
                    regFrom.LoadByPrimaryKey(reg.FromRegistrationNo);
                    if ((regFrom.IsNewPatient ?? false)) reg.MembershipNo = regFrom.MembershipNo;
                    else reg.MembershipNo = string.Empty;
                }
                else reg.MembershipNo = string.Empty;

                if (!string.IsNullOrEmpty(reg.MembershipNo))
                {
                    //var x = BusinessObject.MembershipDetail.EmployeeRefferalRewardPoints(reg.MembershipNo, reg.RegistrationNo, reg.RegistrationDate ?? (new DateTime()).NowAtSqlServer(),
                    //    reg.GuarantorID, AppSession.Parameter.GuarantorTypeSelf, AppSession.Parameter.RewardPointsForPatientGeneral, AppSession.Parameter.RewardPointsForPatientGuarantee,
                    //    AppSession.UserLogin.UserID, true, reg.GuarantorID, reg.FromRegistrationNo);
                }

                pasien.GuarantorID = AppSession.Parameter.GuarantorAskesID[0];
                pasien.SRPatientCategory = reg.SRPatientCategory;
                pasien.LastVisitDate = reg.RegistrationDate;
                pasien.GuarantorCardNo = appt.GuarantorCardNo;
                pasien.IsDisability = reg.IsDisability;
                pasien.NumberOfVisit++;
                pasien.LastUpdateByUserID = "kiosk";
                pasien.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                #endregion

                #region Service Unit Que
                var que = new ServiceUnitQue();
                if (!que.LoadByPrimaryKey(reg.ServiceUnitID, reg.ParamedicID, reg.RegistrationDate.Value + TimeSpan.Parse(reg.RegistrationTime), reg.RegistrationQue ?? 0))
                {
                    que.QueDate = reg.RegistrationDate.Value + TimeSpan.Parse(reg.RegistrationTime);
                    que.RegistrationNo = reg.RegistrationNo;
                    que.ParamedicID = string.IsNullOrEmpty(reg.ParamedicID) ? string.Empty : reg.ParamedicID;
                    que.ServiceUnitID = reg.ServiceUnitID;

                    //var sch = new ParamedicScheduleDate();
                    //if (sch.LoadByPrimaryKey(reg.ServiceUnitID, reg.ParamedicID, reg.RegistrationDate.Value.Year.ToString(), reg.RegistrationDate.Value.Date))
                    {
                        var sp = new ServiceUnitParamedic();
                        if (sp.LoadByPrimaryKey(reg.ServiceUnitID, reg.ParamedicID))
                        {
                            if (sp.IsUsingQue ?? false) que.QueNo = reg.RegistrationQue;
                            else que.QueNo = ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, reg.ParamedicID, reg.RegistrationDate.Value.Date);
                        }
                        //else que.QueNo = ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, reg.ParamedicID, reg.RegistrationDate.Value.Date);
                    }
                    //else que.QueNo = ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, reg.ParamedicID, reg.RegistrationDate.Value.Date);

                    //que.QueNo = reg.RegistrationQue;
                    que.ServiceRoomID = reg.RoomID;
                    que.IsFromReferProcess = false;
                    que.StartTime = que.QueDate;
                    que.IsStopped = false;
                    que.LastUpdateByUserID = "kiosk";
                    que.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
                #endregion

                #region auto bill & visite item (outpatient)
                var chargesHD = new TransCharges();
                var TransChargesItemsDT = new TransChargesItemCollection();
                var TransChargesItemsDTComp = new TransChargesItemCompCollection();
                var TransChargesItemsDTConsumption = new TransChargesItemConsumptionCollection();
                var CostCalculations = new CostCalculationCollection();

                var billColl = new ServiceUnitAutoBillItemCollection();
                billColl.Query.Where
                (
                    billColl.Query.ServiceUnitID == reg.ServiceUnitID,
                    billColl.Query.IsActive == true,
                    billColl.Query.ItemID != AppSession.Parameter.PatientCardItemID
                );

                if (reg.IsNewPatient == true) billColl.Query.Where(billColl.Query.IsGenerateOnNewRegistration == true);
                else billColl.Query.Where(billColl.Query.IsGenerateOnRegistration == true);

                billColl.LoadAll();

                var parColl = new ParamedicAutoBillItemCollection();
                parColl.Query.Where
                    (
                        parColl.Query.ParamedicID == reg.ParamedicID,
                        parColl.Query.ServiceUnitID == reg.ServiceUnitID,
                        parColl.Query.IsActive == true,
                        parColl.Query.IsGenerateOnRegistration == true
                    );
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
                    suabi.IsGenerateOnRegistration = true;
                    suabi.IsGenerateOnNewRegistration = true;
                    suabi.IsGenerateOnReferral = false;
                    suabi.IsGenerateOnFirstRegistration = false;
                    suabi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    suabi.LastUpdateByUserID = "kiosk";
                }

                var tariffDate = DateTime.Parse(reg.RegistrationDate.Value.ToShortDateString() + " " + reg.RegistrationTime);

                if (billColl.Count > 0)
                {
                    var autoNumberTrans = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.TransactionNo);
                    chargesHD.TransactionNo = autoNumberTrans.LastCompleteNumber;
                    autoNumberTrans.Save();

                    chargesHD.RegistrationNo = reg.RegistrationNo;
                    chargesHD.TransactionDate = DateTime.Parse(reg.RegistrationDate.Value.ToShortDateString() + " " + reg.RegistrationTime);
                    chargesHD.ExecutionDate = DateTime.Parse(reg.RegistrationDate.Value.ToShortDateString() + " " + reg.RegistrationTime);
                    chargesHD.ReferenceNo = string.Empty;
                    chargesHD.FromServiceUnitID = reg.ServiceUnitID;
                    chargesHD.ToServiceUnitID = reg.ServiceUnitID;
                    chargesHD.ClassID = reg.ChargeClassID;
                    chargesHD.RoomID = reg.RoomID;
                    chargesHD.BedID = string.Empty;
                    chargesHD.DueDate = (new DateTime()).NowAtSqlServer().Date;
                    chargesHD.SRShift = reg.SRShift;
                    chargesHD.SRItemType = string.Empty;
                    chargesHD.IsProceed = false;
                    chargesHD.IsBillProceed = true;
                    chargesHD.IsApproved = true;
                    chargesHD.IsVoid = false;
                    chargesHD.IsOrder = false;
                    chargesHD.IsCorrection = false;
                    chargesHD.IsClusterAssign = false;
                    chargesHD.IsAutoBillTransaction = true;
                    chargesHD.Notes = string.Empty;
                    chargesHD.SurgicalPackageID = string.Empty;
                    chargesHD.LastUpdateByUserID = "kiosk";
                    chargesHD.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    chargesHD.CreatedByUserID = "kiosk";
                    chargesHD.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                    chargesHD.IsPackage = false;
                    chargesHD.IsRoomIn = reg.IsRoomIn;
                    //var room = new ServiceRoom();
                    //room.LoadByPrimaryKey(reg.RoomID);
                    //chargesHD.TariffDiscountForRoomIn = room.TariffDiscountForRoomIn;

                    tariffDate = chargesHD.TransactionDate.Value.Date;
                }
                else chargesHD = null;

                if (guarantor.TariffCalculationMethod == 1) tariffDate = reg.RegistrationDate.Value.Date;

                string seqNo = string.Empty;
                foreach (ServiceUnitAutoBillItem billItem in billColl)
                {
                    var item = new Item();
                    item.LoadByPrimaryKey(billItem.ItemID);
                    string itemTypeHD = item.SRItemType;

                    seqNo = TransChargesItemsDT.Count == 0 ? "001" : string.Format("{0:000}", int.Parse(TransChargesItemsDT[TransChargesItemsDT.Count - 1].SequenceNo) + 1);

                    ItemTariff tariff =
                        (Helper.Tariff.GetItemTariff(tariffDate, guarantor.SRTariffType,
                                                     chargesHD.ClassID, chargesHD.ClassID, billItem.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                         Helper.Tariff.GetItemTariff(tariffDate, guarantor.SRTariffType,
                                                     AppSession.Parameter.DefaultTariffClass, chargesHD.ClassID, billItem.ItemID,
                                                     reg.GuarantorID, false, reg.SRRegistrationType)) ??
                        (Helper.Tariff.GetItemTariff(tariffDate,
                                                     AppSession.Parameter.DefaultTariffType, chargesHD.ClassID, chargesHD.ClassID,
                                                     billItem.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                         Helper.Tariff.GetItemTariff(tariffDate,
                                                     AppSession.Parameter.DefaultTariffType,
                                                     AppSession.Parameter.DefaultTariffClass, chargesHD.ClassID, billItem.ItemID,
                                                     reg.GuarantorID, false, reg.SRRegistrationType));

                    if (tariff == null) continue; /*tidak ada tarifnya*/

                    var chargesDT = TransChargesItemsDT.AddNew();
                    chargesDT.TransactionNo = chargesHD.TransactionNo;
                    chargesDT.SequenceNo = seqNo;
                    chargesDT.ReferenceNo = string.Empty;
                    chargesDT.ReferenceSequenceNo = string.Empty;
                    chargesDT.ItemID = billItem.ItemID;
                    chargesDT.ChargeClassID = reg.ChargeClassID;
                    chargesDT.ParamedicID = string.Empty;
                    chargesDT.TariffDate = tariffDate;
                    chargesDT.IsAdminCalculation = tariff.IsAdminCalculation ?? false;

                    switch (itemTypeHD)
                    {
                        case BusinessObject.Reference.ItemType.Service:
                            var service = new ItemService();
                            service.LoadByPrimaryKey(billItem.ItemID);
                            chargesDT.SRItemUnit = service.SRItemUnit;
                            chargesDT.CostPrice = tariff.Price ?? 0;
                            break;
                        case BusinessObject.Reference.ItemType.Medical:
                            var ipm = new ItemProductMedic();
                            ipm.LoadByPrimaryKey(billItem.ItemID);
                            chargesDT.SRItemUnit = ipm.SRItemUnit;
                            chargesDT.CostPrice = ipm.CostPrice ?? 0;
                            break;
                        case BusinessObject.Reference.ItemType.NonMedical:
                            var ipn = new ItemProductNonMedic();
                            ipn.LoadByPrimaryKey(billItem.ItemID);
                            chargesDT.SRItemUnit = ipn.SRItemUnit;
                            chargesDT.CostPrice = ipn.CostPrice ?? 0;
                            break;
                        case BusinessObject.Reference.ItemType.Laboratory:
                        case BusinessObject.Reference.ItemType.Diagnostic:
                        case BusinessObject.Reference.ItemType.Radiology:
                            chargesDT.SRItemUnit = string.Empty;
                            chargesDT.CostPrice = tariff.Price ?? 0;
                            break;
                    }

                    chargesDT.IsVariable = false;
                    chargesDT.IsCito = false;
                    chargesDT.IsCitoInPercent = false;
                    chargesDT.BasicCitoAmount = (decimal)0D;
                    chargesDT.ChargeQuantity = billItem.Quantity;

                    if (itemTypeHD == BusinessObject.Reference.ItemType.Medical || itemTypeHD == BusinessObject.Reference.ItemType.NonMedical) chargesDT.StockQuantity = billItem.Quantity;
                    else chargesDT.StockQuantity = (decimal)0D;

                    var itemRooms = new AppStandardReferenceItemCollection();
                    itemRooms.Query.Where(itemRooms.Query.StandardReferenceID == "ItemTariffRoom", itemRooms.Query.ItemID == billItem.ItemID, itemRooms.Query.IsActive == true);
                    itemRooms.LoadAll();
                    chargesDT.IsItemRoom = itemRooms.Count > 0;

                    if (!string.IsNullOrEmpty(reg.ItemConditionRuleID))
                    {
                        var promo = new ItemConditionRuleItem();
                        if (promo.LoadByPrimaryKey(reg.ItemConditionRuleID, chargesDT.ItemID))
                            chargesDT.ItemConditionRuleID = reg.ItemConditionRuleID;
                        else
                            chargesDT.ItemConditionRuleID = string.Empty;
                    }
                    else
                        chargesDT.ItemConditionRuleID = string.Empty;

                    chargesDT.Price = tariff.Price ?? 0;
                    if (chargesDT.IsItemRoom == true && chargesHD.IsRoomIn == true)
                        chargesDT.Price = chargesDT.Price - (chargesDT.Price * chargesHD.TariffDiscountForRoomIn / 100);

                    if (!string.IsNullOrEmpty(chargesDT.ItemConditionRuleID))
                        chargesDT.Price = Helper.Tariff.GetItemConditionRuleTariff(chargesDT.Price ?? 0, chargesDT.ItemConditionRuleID, tariffDate);

                    chargesDT.DiscountAmount = (decimal)0D;
                    chargesDT.CitoAmount = (decimal)0D;
                    chargesDT.RoundingAmount = Helper.RoundingDiff;
                    chargesDT.SRDiscountReason = string.Empty;
                    chargesDT.IsAssetUtilization = false;
                    chargesDT.AssetID = string.Empty;
                    chargesDT.IsBillProceed = true;
                    chargesDT.IsOrderRealization = false;
                    chargesDT.IsPackage = false;
                    chargesDT.IsApprove = true;
                    chargesDT.IsVoid = false;
                    chargesDT.LastUpdateByUserID = "kiosk";
                    chargesDT.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    chargesDT.ParentNo = string.Empty;
                    chargesDT.SRCenterID = string.Empty;
                    chargesDT.CreatedByUserID = "kiosk";
                    chargesDT.CreatedDateTime = (new DateTime()).NowAtSqlServer();

                    chargesDT.IsCasemixApproved = true;

                    //if (GuarantorBPJS.Contains(reg.GuarantorID))
                    //{
                    //    if (AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                    //    {
                    //        var rpath = new RegistrationPathway();
                    //        rpath.Query.Where(rpath.Query.RegistrationNo == reg.RegistrationNo, rpath.Query.PathwayID != string.Empty, rpath.Query.PathwayStatus == "A");
                    //        if (rpath.Query.Load())
                    //        {
                    //            var rpc = new RegistrationPathwayCollection();
                    //            rpc.Query.Where(rpc.Query.RegistrationNo == reg.RegistrationNo);
                    //            if (!rpc.Query.Load()) chargesDT.IsCasemixApproved = true;
                    //            foreach (var rp in rpc)
                    //            {
                    //                if (string.IsNullOrEmpty(rp.PathwayID)) continue;
                    //                var rpic = new RegistrationPathwayItemCollection();
                    //                rpic.Query.Where(rpic.Query.PathwayID == rp.PathwayID);
                    //                rpic.Query.OrderBy(rpic.Query.PathwayItemSeqNo.Ascending);
                    //                if (!rpic.Query.Load()) continue;
                    //                foreach (var rpi in rpic)
                    //                {
                    //                    var pi = new PathwayItem();
                    //                    if (!pi.LoadByPrimaryKey(rpi.PathwayID, rpi.PathwayItemSeqNo ?? 0)) continue;
                    //                    if (chargesDT.ItemID == pi.ItemID)
                    //                    {
                    //                        chargesDT.IsCasemixApproved = true;
                    //                    }
                    //                    else
                    //                    {
                    //                        var ipm = new ItemProductMedic();
                    //                        if (ipm.LoadByPrimaryKey(chargesDT.ItemID))
                    //                        {
                    //                            if (ipm.IsFornas ?? false) chargesDT.IsCasemixApproved = true;
                    //                            else if (ipm.IsFormularium ?? false) chargesDT.IsCasemixApproved = true;
                    //                            else if (ipm.isGeneric ?? false) chargesDT.IsCasemixApproved = true;
                    //                            else if (ipm.IsNonGenericLimited ?? false) chargesDT.IsCasemixApproved = true;
                    //                            else if (AppSession.Parameter.ItemGroupBmhp.Any(g => g == chargesDT.ItemID)) chargesDT.IsCasemixApproved = true;
                    //                            else
                    //                            {
                    //                                var zats = new ItemProductMedicZatActiveCollection();
                    //                                zats.Query.Where(zats.Query.ItemID == chargesDT.ItemID);
                    //                                if (zats.Query.Load())
                    //                                {
                    //                                    if (zats.Any(z => z.ItemID == pi.ItemID)) chargesDT.IsCasemixApproved = true;
                    //                                }
                    //                            }
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            var list = new CasemixCoveredCollection();
                    //            list.Query.Where(list.Query.IsActive == true);
                    //            if (list.Query.Load())
                    //            {
                    //                var guarantorList = new CasemixCoveredGuarantorCollection();
                    //                guarantorList.Query.Where(guarantorList.Query.CasemixCoveredID.In(list.Select(l => l.CasemixCoveredID)), guarantorList.Query.GuarantorID == reg.GuarantorID);
                    //                if (guarantorList.Query.Load())
                    //                {
                    //                    var itemList = new CasemixCoveredDetail();
                    //                    itemList.Query.es.Distinct = true;
                    //                    itemList.Query.Where(itemList.Query.CasemixCoveredID.In(guarantorList.Select(g => g.CasemixCoveredID ?? -1)), itemList.Query.ItemID == chargesDT.ItemID);
                    //                    if (itemList.Query.Load())
                    //                    {
                    //                        if (itemList.Qty == 0)
                    //                        {
                    //                            chargesDT.IsCasemixApproved = true;
                    //                        }
                    //                        else
                    //                        {
                    //                            var tci = new TransChargesItemQuery("a");
                    //                            var tc = new TransChargesQuery("b");

                    //                            tci.InnerJoin(tc).On(tci.TransactionNo == tc.TransactionNo && tc.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(reg.RegistrationNo)) && tc.IsApproved == true && tc.IsVoid == false);
                    //                            tci.Where(tci.ItemID == chargesDT.ItemID, tci.IsApprove == true, tci.IsVoid == false);

                    //                            var tciList = new TransChargesItemCollection();
                    //                            if (tciList.Load(tci) && tciList.Count > 0)
                    //                            {
                    //                                if (tciList.Sum(t => t.ChargeQuantity) <= itemList.Qty) chargesDT.IsCasemixApproved = true;
                    //                            }
                    //                            else chargesDT.IsCasemixApproved = true;
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        chargesDT.IsCasemixApproved = true;
                    //    }
                    //}
                    //else
                    //{
                    //    chargesDT.IsCasemixApproved = true;
                    //}

                    #region item component
                    var compQuery = new ItemTariffComponentQuery();
                    compQuery.es.Top = 1;
                    compQuery.Where
                        (
                            compQuery.SRTariffType == guarantor.SRTariffType,
                            compQuery.ItemID == billItem.ItemID,
                            compQuery.ClassID == reg.ChargeClassID,
                            compQuery.StartingDate <= (new DateTime()).NowAtSqlServer().Date
                        );

                    var compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate,
                                                                                  guarantor.SRTariffType, chargesHD.ClassID,
                                                                                  chargesDT.ItemID);
                    if (!compColl.Any())
                        compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate,
                                                                                  guarantor.SRTariffType,
                                                                                  AppSession.Parameter.DefaultTariffClass,
                                                                                  chargesDT.ItemID);
                    if (!compColl.Any())
                        compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate,
                                                                                  AppSession.Parameter.DefaultTariffType,
                                                                                  chargesHD.ClassID, chargesDT.ItemID);
                    if (!compColl.Any())
                        compColl = Helper.Tariff.GetItemTariffComponentCollection(tariffDate,
                                                                                  AppSession.Parameter.DefaultTariffType,
                                                                                  AppSession.Parameter.DefaultTariffClass,
                                                                                  chargesDT.ItemID);

                    var p = string.Empty;
                    foreach (var comp in compColl)
                    {
                        var compCharges = TransChargesItemsDTComp.AddNew();
                        compCharges.TransactionNo = chargesHD.TransactionNo;
                        compCharges.SequenceNo = seqNo;
                        compCharges.TariffComponentID = comp.TariffComponentID;
                        if (chargesHD.IsRoomIn == true && chargesDT.IsItemRoom == true) compCharges.Price = comp.Price - (comp.Price * chargesHD.TariffDiscountForRoomIn / 100);
                        else compCharges.Price = comp.Price;

                        if (!string.IsNullOrEmpty(chargesDT.ItemConditionRuleID))
                            compCharges.Price = Helper.Tariff.GetItemConditionRuleTariff(compCharges.Price ?? 0, chargesDT.ItemConditionRuleID, tariffDate);

                        compCharges.DiscountAmount = (decimal)0D;
                        compCharges.CitoAmount = (decimal)0D;

                        var tcomp = new TariffComponent();
                        tcomp.LoadByPrimaryKey(comp.TariffComponentID);

                        if (reg.SRRegistrationType != AppConstant.RegistrationType.ClusterPatient)
                        {
                            if (tcomp.IsTariffParamedic ?? false) compCharges.ParamedicID = reg.ParamedicID;
                            else compCharges.ParamedicID = string.Empty;
                        }
                        else compCharges.ParamedicID = string.Empty;

                        compCharges.LastUpdateByUserID = "kiosk";
                        compCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        if (!string.IsNullOrEmpty(compCharges.ParamedicID))
                        {
                            var tComp = new TariffComponent();
                            if (tComp.LoadByPrimaryKey(compCharges.TariffComponentID))
                            {
                                if (tComp.IsPrintParamedicInSlip ?? false)
                                {
                                    var par = new Paramedic();
                                    par.LoadByPrimaryKey(compCharges.ParamedicID);
                                    if (par.IsPrintInSlip ?? true)
                                        p = p.Length == 0 ? par.ParamedicName : p + "; " + par.ParamedicName;
                                }
                            }
                        }
                    }
                    chargesDT.ParamedicCollectionName = p;
                    #endregion

                    #region Item Consumption
                    var consQuery = new ItemConsumptionQuery();
                    consQuery.Where(consQuery.ItemID == billItem.ItemID);

                    var consColl = new ItemConsumptionCollection();
                    consColl.Load(consQuery);

                    foreach (var cons in consColl)
                    {
                        var consCharges = TransChargesItemsDTConsumption.AddNew();
                        consCharges.TransactionNo = chargesHD.TransactionNo;
                        consCharges.SequenceNo = seqNo;
                        consCharges.DetailItemID = cons.ItemID;
                        consCharges.Qty = cons.Qty;
                        consCharges.SRItemUnit = cons.SRItemUnit;
                        consCharges.LastUpdateByUserID = "kiosk";
                        consCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    }
                    #endregion
                }

                #region auto calculation
                if (TransChargesItemsDT.Count > 0)
                {
                    var grrID = reg.GuarantorID;
                    if (grrID == AppSession.Parameter.SelfGuarantor)
                    {
                        var pat = new Patient();
                        pat.LoadByPrimaryKey(reg.PatientID);
                        if (!string.IsNullOrEmpty(pat.MemberID))
                            grrID = pat.MemberID;
                    }

                    DataTable tblCovered = Helper.GetCoveredItems((Registration)reg, reg.SRBussinesMethod, reg.PlavonAmount ?? 0, reg.IsGlobalPlafond ?? false,
                        reg.ChargeClassID, reg.CoverageClassID, grrID, TransChargesItemsDT.Select(t => t.ItemID).ToArray(),
                        tariffDate, new RegistrationItemRuleCollection(), false);

                    foreach (TransChargesItem detail in TransChargesItemsDT)
                    {
                        var rowCovered = tblCovered.AsEnumerable().Where(t => t.Field<string>("ItemID") == detail.ItemID &&
                                                                              t.Field<bool>("IsInclude")).SingleOrDefault();
                        if (rowCovered != null)
                        {
                            decimal? discount = 0;
                            bool isDiscount = false, isMargin = false;
                            foreach (var comp in TransChargesItemsDTComp.Where(t => t.TransactionNo == detail.TransactionNo &&
                                                                                    t.SequenceNo == detail.SequenceNo)
                                                                        .OrderBy(t => t.TariffComponentID))
                            {
                                decimal? amountValue = 0;
                                decimal? basicPrice = 0;
                                decimal? coveragePrice = 0;

                                if (Convert.ToBoolean(rowCovered["IsByTariffComponent"]))
                                {
                                    var array = rowCovered["TariffComponentValue"].ToString().Split(';').Where(l => l.Split('/')[2] == comp.TariffComponentID).SingleOrDefault();
                                    if (array == null)
                                    {
                                        amountValue = (decimal?)rowCovered["AmountValue"];
                                        basicPrice = (decimal?)rowCovered["BasicPrice"];
                                        coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                    }
                                    else
                                    {
                                        var list = array.Split('/');
                                        if (list == null || list.Count() == 0)
                                        {
                                            amountValue = (decimal?)rowCovered["AmountValue"];
                                            basicPrice = (decimal?)rowCovered["BasicPrice"];
                                            coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                        }
                                        else
                                        {
                                            amountValue = Convert.ToDecimal(list[3]);
                                            basicPrice = Convert.ToDecimal(list[0]);
                                            coveragePrice = Convert.ToDecimal(list[1]);
                                        }
                                    }
                                }
                                else
                                {
                                    amountValue = (decimal?)rowCovered["AmountValue"];
                                    basicPrice = (decimal?)rowCovered["BasicPrice"];
                                    coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                }

                                if (!string.IsNullOrEmpty(detail.ItemConditionRuleID))
                                {
                                    basicPrice = Helper.Tariff.GetItemConditionRuleTariff(basicPrice ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);
                                    coveragePrice = Helper.Tariff.GetItemConditionRuleTariff(coveragePrice ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);
                                }

                                if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                                {
                                    if ((comp.Price - comp.DiscountAmount) <= 0)
                                        continue;

                                    var compPrice = comp.Price;
                                    if (basicPrice > coveragePrice)
                                    {
                                        var tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, guarantor.SRTariffType,
                                            reg.CoverageClassID, comp.TariffComponentID, detail.ItemID);
                                        if (!tcomp.AsEnumerable().Any())
                                            tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, guarantor.SRTariffType,
                                                AppSession.Parameter.DefaultTariffClass, comp.TariffComponentID, detail.ItemID);
                                        if (!tcomp.AsEnumerable().Any())
                                            tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, AppSession.Parameter.DefaultTariffType,
                                                reg.CoverageClassID, comp.TariffComponentID, detail.ItemID);
                                        if (!tcomp.AsEnumerable().Any())
                                            tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, AppSession.Parameter.DefaultTariffType,
                                                AppSession.Parameter.DefaultTariffClass, comp.TariffComponentID, detail.ItemID);

                                        if (!tcomp.AsEnumerable().Any())
                                            continue;

                                        compPrice = tcomp.AsEnumerable().Select(c => c.Field<decimal>("Price")).Single();

                                        if (!string.IsNullOrEmpty(detail.ItemConditionRuleID)) compPrice = Helper.Tariff.GetItemConditionRuleTariff(compPrice ?? 0, detail.ItemConditionRuleID,
                                                    tariffDate);
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
                        if (TransChargesItemsDTComp.Count > 0)
                        {
                            detail.AutoProcessCalculation = TransChargesItemsDTComp.Where(t => t.TransactionNo == detail.TransactionNo &&
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

                                    if (!string.IsNullOrEmpty(detail.ItemConditionRuleID))
                                    {
                                        basicPrice = Helper.Tariff.GetItemConditionRuleTariff(basicPrice ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);
                                        coveragePrice = Helper.Tariff.GetItemConditionRuleTariff(coveragePrice ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);
                                    }

                                    var detailPrice = detail.Price ?? 0;
                                    if (basicPrice > coveragePrice)
                                    {
                                        var transDate = chargesHD.TransactionDate.Value.Date;
                                        if (guarantor.TariffCalculationMethod == 1) transDate = reg.RegistrationDate.Value.Date;

                                        ItemTariff tariff = (Helper.Tariff.GetItemTariff(tariffDate, guarantor.SRTariffType, reg.CoverageClassID, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                                 Helper.Tariff.GetItemTariff(tariffDate, guarantor.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                                (Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, reg.CoverageClassID, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                                 Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));
                                        if (tariff != null)
                                        {
                                            //detailPrice = tariff.Price ?? 0;
                                            detailPrice = Helper.Tariff.GetItemConditionRuleTariff(tariff.Price ?? 0, detail.ItemConditionRuleID, detail.TariffDate ?? tariffDate);
                                        }
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
                                                                  chargesHD.IsRoomIn ?? false, detail.IsItemRoom ?? false,
                                                                  chargesHD.TariffDiscountForRoomIn ?? 0, detail.DiscountAmount ?? 0, false,
                                                                  detail.ItemConditionRuleID, tariffDate, detail.IsVariable ?? false);

                        CostCalculation cost = CostCalculations.AddNew();
                        cost.RegistrationNo = reg.RegistrationNo;
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
                                cost.DiscountAmount = totaldisc > (calc.GuarantorAmount + detail.DiscountAmount)
                                                           ? (calc.GuarantorAmount + detail.DiscountAmount)
                                                           : totaldisc;

                                cost.GuarantorAmount = totaldisc > (calc.GuarantorAmount + detail.DiscountAmount)
                                                           ? 0
                                                           : (calc.GuarantorAmount + detail.DiscountAmount) - totaldisc;
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
                                var compColl = TransChargesItemsDTComp.Where(
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
                                        "kiosk", chargesHD.ClassID, chargesHD.ToServiceUnitID);

                                }

                                detail.DiscountAmount = cost.DiscountAmount;
                                detail.Save();
                            }
                        }

                        cost.IsPackage = detail.IsPackage;
                        cost.ParentNo = detail.ParentNo;
                        cost.ParamedicAmount = detail.ChargeQuantity * TransChargesItemsDTComp.Where(comp => comp.TransactionNo == detail.TransactionNo &&
                                                                                                             comp.SequenceNo == detail.SequenceNo &&
                                                                                                             !string.IsNullOrEmpty(comp.ParamedicID))
                                                                                              .Sum(comp => comp.Price - comp.DiscountAmount);
                        cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        cost.LastUpdateByUserID = "kiosk";
                    }
                }
                #endregion

                reg.RemainingAmount = CostCalculations.Sum(c => (c.PatientAmount + c.GuarantorAmount));
                #endregion

                #region Merge Billing
                var billing = new MergeBilling();
                if (!billing.LoadByPrimaryKey(reg.RegistrationNo)) billing = new MergeBilling();
                billing.RegistrationNo = reg.RegistrationNo;
                billing.FromRegistrationNo = string.Empty;
                billing.LastUpdateByUserID = "kiosk";
                billing.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                #endregion

                #region Medical Record File Status
                var fileStatus = new MedicalRecordFileStatus();
                if (!fileStatus.LoadByPrimaryKey(reg.RegistrationNo))
                {
                    fileStatus.RegistrationNo = reg.RegistrationNo;
                    fileStatus.FileOutDate = reg.RegistrationDate.Value + TimeSpan.Parse(reg.RegistrationTime);
                    fileStatus.SRMedicalFileCategory = AppSession.Parameter.MedicalFileCategoryOut;
                    fileStatus.SRMedicalFileStatus = AppSession.Parameter.MedicalFileStatusRequest;
                    fileStatus.Notes = string.Empty;
                    fileStatus.RequestByUserID = "kiosk";
                    fileStatus.LastUpdateByUserID = "kiosk";
                    fileStatus.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
                #endregion

                #region Responsible Person
                var responsible = new RegistrationResponsiblePerson();
                if (!responsible.LoadByPrimaryKey(reg.RegistrationNo)) responsible = new RegistrationResponsiblePerson();
                responsible.RegistrationNo = reg.RegistrationNo;
                responsible.NameOfTheResponsible = string.Empty;
                responsible.SRRelationship = string.Empty;
                responsible.SROccupation = string.Empty;
                responsible.JobDescription = string.Empty;
                responsible.HomeAddress = string.Empty;
                responsible.PhoneNo = string.Empty;
                responsible.Ssn = string.Empty;
                responsible.LastUpdateByUserID = "kiosk";
                responsible.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                #endregion

                #region Emergency Contact
                var emergencyContact = new EmergencyContact();
                if (!emergencyContact.LoadByPrimaryKey(reg.RegistrationNo)) emergencyContact = new EmergencyContact();
                emergencyContact.RegistrationNo = reg.RegistrationNo;
                emergencyContact.ContactName = string.Empty;
                emergencyContact.SRRelationship = string.Empty;
                emergencyContact.SROccupation = string.Empty;
                emergencyContact.Ssn = string.Empty;
                emergencyContact.StreetName = string.Empty;
                emergencyContact.District = string.Empty;
                emergencyContact.City = string.Empty;
                emergencyContact.County = string.Empty;
                emergencyContact.State = string.Empty;
                emergencyContact.str.ZipCode = string.Empty;
                emergencyContact.PhoneNo = string.Empty;
                emergencyContact.FaxNo = string.Empty;
                emergencyContact.MobilePhoneNo = string.Empty;
                emergencyContact.Email = string.Empty;
                emergencyContact.LastUpdateByUserID = "kiosk";
                emergencyContact.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                var patientEmrContact = new PatientEmergencyContact();
                if (!patientEmrContact.LoadByPrimaryKey(reg.PatientID)) patientEmrContact = new PatientEmergencyContact();
                patientEmrContact.PatientID = reg.PatientID;
                patientEmrContact.ContactName = string.Empty;
                patientEmrContact.SRRelationship = string.Empty;
                patientEmrContact.SROccupation = string.Empty;
                patientEmrContact.Ssn = string.Empty;
                patientEmrContact.StreetName = string.Empty;
                patientEmrContact.District = string.Empty;
                patientEmrContact.City = string.Empty;
                patientEmrContact.County = string.Empty;
                patientEmrContact.State = string.Empty;
                patientEmrContact.str.ZipCode = string.Empty;
                patientEmrContact.PhoneNo = string.Empty;
                patientEmrContact.FaxNo = string.Empty;
                patientEmrContact.MobilePhoneNo = string.Empty;
                patientEmrContact.Email = string.Empty;
                patientEmrContact.LastUpdateByUserID = "kiosk";
                patientEmrContact.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                #endregion

                #region Registration Info Sumary
                var registrationInfoSumary = new RegistrationInfoSumary();
                if (!registrationInfoSumary.LoadByPrimaryKey(reg.RegistrationNo))
                {
                    registrationInfoSumary.AddNew();
                    registrationInfoSumary.RegistrationNo = reg.RegistrationNo;
                    registrationInfoSumary.NoteCount = 0;
                    registrationInfoSumary.NoteMedicalCount = 0;
                    registrationInfoSumary.DocumentCheckListCount = 0;
                    registrationInfoSumary.LastUpdateByUserID = "kiosk";
                    registrationInfoSumary.LastUpdateDateTime = DateTime.Now;
                }
                #endregion

                var regno = reg.RegistrationNo;

                using (var trans = new esTransactionScope())
                {
                    appt.Save();
                    reg.Save();
                    pasien.Save();
                    que.Save();
                    billing.Save();
                    fileStatus.Save();
                    responsible.Save();
                    emergencyContact.Save();
                    registrationInfoSumary.Save();

                    if (chargesHD != null) chargesHD.Save();
                    if (TransChargesItemsDT.Count > 0) TransChargesItemsDT.Save();
                    if (TransChargesItemsDTComp.Count > 0) TransChargesItemsDTComp.Save();
                    if (TransChargesItemsDTConsumption.Count > 0) TransChargesItemsDTConsumption.Save();
                    if (CostCalculations.Count > 0) CostCalculations.Save();

                    trans.Complete();
                }

                // update task id antrol
                if (Helper.IsBpjsAntrolIntegration)
                {
                    var logTask = new WebServiceAPILog();

                    if (reg.IsNewPatient ?? false)
                    {
                        logTask = new WebServiceAPILog
                        {
                            DateRequest = DateTime.Now,
                            IPAddress = "10.200.200.188",
                            UrlAddress = "SelfChechkIn",
                            Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                            {
                                Kodebooking = appt.AppointmentNo,
                                Taskid = 1,
                                Waktu = Convert.ToInt64(DateTimeOffset.Now.AddMinutes(3).ToUnixTimeMilliseconds())
                            }),
                            Response = string.Empty,
                            Totalms = 0
                        };
                        var svcAntrol = new Common.BPJS.Antrian.Service();
                        var responseAntrol = svcAntrol.UpdateWaktuAntrian(JsonConvert.DeserializeObject<Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root>(logTask.Params));
                        logTask.Response = JsonConvert.SerializeObject(responseAntrol);
                        logTask.Save();

                        logTask = new WebServiceAPILog();
                        logTask.DateRequest = DateTime.Now;
                        logTask.IPAddress = "antrol";
                        logTask.UrlAddress = "SelfChechkIn";
                        logTask.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                        {
                            Kodebooking = appt.AppointmentNo,
                            Taskid = 2,
                            Waktu = Convert.ToInt64(DateTimeOffset.Now.AddMinutes(3).ToUnixTimeMilliseconds())
                        });

                        svcAntrol = new Common.BPJS.Antrian.Service();
                        responseAntrol = svcAntrol.UpdateWaktuAntrian(JsonConvert.DeserializeObject<Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root>(logTask.Params));
                        logTask.Response = JsonConvert.SerializeObject(responseAntrol);
                        logTask.Save();

                        log.Response = JsonConvert.SerializeObject(new
                        {
                            Code = (int)HttpStatusCode.Found, // 302
                            Message = $"Anda adalah pasien baru, Silahkan ambil antrian dan menghubungi unit registrasi"
                        });
                        log.Save();

                        return Request.CreateResponse(HttpStatusCode.Found, new
                        {
                            metadata = new
                            {
                                Code = (int)HttpStatusCode.Found, // 302
                                Message = $"Anda adalah pasien baru, Silahkan ambil antrian dan menghubungi unit registrasi"
                            }
                        });
                    }

                    {
                        logTask = new WebServiceAPILog();
                        logTask.DateRequest = DateTime.Now;
                        logTask.IPAddress = string.Empty;
                        logTask.UrlAddress = "SelfChechkIn";
                        logTask.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                        {
                            Kodebooking = appt.AppointmentNo,
                            Taskid = 3,
                            Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                        });

                        var svcAntrol = new Common.BPJS.Antrian.Service();
                        var responseAntrol = svcAntrol.UpdateWaktuAntrian(JsonConvert.DeserializeObject<Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root>(logTask.Params));
                        logTask.Response = JsonConvert.SerializeObject(responseAntrol);
                        logTask.Save();
                    }
                }

                // auto print SEP
                if (AppSession.Parameter.IsSelfCheckinPrintingSEP)
                {
                    var parametersSEP = new PrintJobParameterCollection();
                    parametersSEP.AddNew("p_NoSep", bs.NoSEP, null, null);
                    if (new string[] { "RSJKT", "RSIMT" }.Contains(AppSession.Parameter.HealthcareInitial)) PrintManager.CreatePrintJob("XML.01.0042b", parametersSEP, "kiosk");
                    else PrintManager.CreatePrintJob(AppConstant.Report.BpjsSEP, parametersSEP, "kiosk");
                }

                if (AppSession.Parameter.IsRegistrationPrintSlip)
                {
                    var parametersSlip = new PrintJobParameterCollection();
                    parametersSlip.AddNew("p_RegistrationNo", regno, null, null);
                    PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationSlipRpt, parametersSlip, "kiosk");
                }

                if (AppSession.Parameter.IsRegistrationTracer)
                {
                    var parametersSlip = new PrintJobParameterCollection();
                    parametersSlip.AddNew("p_RegistrationNo", regno, null, null);
                    PrintManager.CreatePrintJob(AppSession.Parameter.TracerOpRpt, parametersSlip, "kiosk");
                }

                if (AppSession.Parameter.AntrolPrintLabelOnKiosk.ToLower() == "yes")
                {
                    var parametersSlip = new PrintJobParameterCollection();
                    parametersSlip.AddNew("p_RegistrationNo", regno, null, null);
                    PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationLabelOpRpt, parametersSlip, "kiosk");
                }

                try
                {
                    var esep = CetakESep(bs.NoSEP);
                    var logEsep = new WebServiceAPILog();
                    logEsep.DateRequest = DateTime.Now;
                    logEsep.IPAddress = "kiosk";
                    logEsep.UrlAddress = "esep";
                    logEsep.Params = bs.NoSEP;
                    logEsep.Response = JsonConvert.SerializeObject(esep);
                    logEsep.Totalms = 0;
                    logEsep.Save();
                }
                catch (Exception ex)
                {

                }

                log.Response = JsonConvert.SerializeObject(new
                {
                    Code = (int)HttpStatusCode.OK, // 302
                    Message = $"OK"
                });
                log.Save();
            }
            catch (Exception ex)
            {
                log.Response = JsonConvert.SerializeObject(new
                {
                    Code = (int)HttpStatusCode.InternalServerError, // 302
                    Message = JsonConvert.SerializeObject(new
                    {
                        ex.Source,
                        ex.Message,
                        ex.StackTrace,
                        InnerException = ex.InnerException == null ? null : new
                        {
                            ex.Source,
                            ex.Message,
                            ex.StackTrace
                        }
                    })
                });
                log.Save();

                return Request.CreateResponse(HttpStatusCode.NoContent, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.Created, // 201
                        Message = "Check In Gagal, Dicoba Kembali"
                    }
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                metadata = new
                {
                    Code = (int)HttpStatusCode.OK, // 200
                    Message = "OK"
                }
            });
        }

        //self check-in
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("antrol/selfcheckin/GetPesertaByNoPeserta/{nomor}")]
        public HttpResponseMessage GetPesertaByNoPeserta(string nomor)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            var peserta = svc.GetPeserta(Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta, nomor, DateTime.Now.Date);
            if (!peserta.MetaData.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.NotFound, // 404
                        Message = peserta.MetaData.Message
                    }
                });
            }

            var pasiens = new PatientCollection();
            pasiens.Query.Where(pasiens.Query.GuarantorCardNo == nomor, pasiens.Query.IsActive == true);
            if (pasiens.Query.Load())
            {
                if (pasiens.Count > 1)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        metadata = new
                        {
                            Code = (int)HttpStatusCode.NotFound, // 404
                            Message = $"Terdapat duplikasi data pasien dengan nomor {nomor}"
                        }
                    });
                }
            }

            var patient = new Patient();
            patient.Query.es.Top = 1;
            patient.Query.Where(patient.Query.GuarantorCardNo == nomor);
            if (patient.Query.Load())
            {
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.OK, // 200
                        MobilePhoneNo = patient.MobilePhoneNo,
                        Message = "Ok"
                    }
                });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, new
                {
                    metadata = new Antrol.AmbilAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.NotFound,
                        Message = $"Data pasien ini tidak ditemukan, silahkan Melakukan Registrasi Pasien Baru"
                    }
                });
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("antrol/selfcheckin/GetRujukanByNoPeserta/{nomor}")]
        public HttpResponseMessage GetRujukanByNoPeserta(string nomor)
        {
            var nopeserta = string.Empty;
            //var isRencanaKontrol = false;
            //Common.BPJS.VClaim.v11.RencanaKontrol.Select.ResponseSuratKontrol.Root rencanaKontrol = null;

            var svc = new Common.BPJS.VClaim.v11.Service();
            var peserta = svc.GetPeserta(Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta, nomor, DateTime.Now.Date);
            if (!peserta.MetaData.IsValid)
            {
                svc = new Common.BPJS.VClaim.v11.Service();
                peserta = svc.GetPeserta(Common.BPJS.VClaim.Enum.SearchPeserta.NIK, nomor, DateTime.Now.Date);
                if (!peserta.MetaData.IsValid)
                {
                    svc = new Common.BPJS.VClaim.v11.Service();
                    var xx = svc.GetRujukan(true, nomor, Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);
                    if (!xx.MetaData.IsValid)
                    {
                        svc = new Common.BPJS.VClaim.v11.Service();
                        xx = svc.GetRujukan(true, nomor, Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                        if (!xx.MetaData.IsValid)
                        {
                            //svc = new Common.BPJS.VClaim.v11.Service();
                            //rencanaKontrol = svc.GetRencanaKontrolByNoSuratKontrol(nomor);
                            //if (!rencanaKontrol.MetaData.IsValid)
                            //{
                            return Request.CreateResponse(HttpStatusCode.NotFound, new
                            {
                                metadata = new
                                {
                                    Code = (int)HttpStatusCode.NotFound, // 404
                                    Message = "Data peserta / rujukan / kontrol tidak ditemukan"
                                }
                            });
                            //}
                            //else
                            //{
                            //isRencanaKontrol = true;
                            //    nopeserta = rencanaKontrol.Response.Sep.Peserta.NoKartu;
                            //}
                        }
                        else nopeserta = xx.Response.Rujukan.Peserta.NoKartu;
                    }
                    else nopeserta = xx.Response.Rujukan.Peserta.NoKartu;
                }
                else nopeserta = peserta.Response.Peserta.NoKartu;
            }
            else nopeserta = peserta.Response.Peserta.NoKartu;

            var listRujukan = new List<ListRujukan>();
            //var byRujukan = false;
            var tujuanKunjungan = string.Empty;

            //if (isRencanaKontrol && rencanaKontrol != null)
            //{
            //    if (rencanaKontrol.MetaData.IsValid)
            //        listRujukan.Add(new ListRujukan() { Text = $"{rencanaKontrol.Response.NoSuratKontrol} - {rencanaKontrol.Response.NamaPoliTujuan}", Value = $"{rencanaKontrol.Response.NoSuratKontrol}#{rencanaKontrol.Response.PoliTujuan}" });
            //}

            //if (!listRujukan.Any() && !byRujukan)
            {
                svc = new Common.BPJS.VClaim.v11.Service();
                var rencanaKontrol = svc.GetRencanaKontrolByNoPeserta(DateTime.Now.Date.ToString("MM"), DateTime.Now.Date.ToString("yyyy"), nopeserta, Common.BPJS.VClaim.Enum.FilterRencanaKontrol.TanggalRencanaKontrol);
                if (rencanaKontrol.MetaData.IsValid)
                {
                    tujuanKunjungan = "3";
                    foreach (var item in rencanaKontrol.Response.List.Where(r => DateTime.ParseExact(r.TglRencanaKontrol, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None) >= DateTime.Now.Date && r.TerbitSEP.ToLower() == "belum"))
                    {
                        listRujukan.Add(new ListRujukan() { Text = $"{item.NoSuratKontrol} - {item.NamaPoliTujuan} - (KONTROL) - {item.TglRencanaKontrol}", Value = $"{item.NoSuratKontrol}#{item.PoliTujuan}#{tujuanKunjungan}#0#{item.TglRencanaKontrol}" });
                    }
                }

                svc = new Common.BPJS.VClaim.v11.Service();
                rencanaKontrol = svc.GetRencanaKontrolByNoPeserta(DateTime.Now.AddMonths(1).Date.ToString("MM"), DateTime.Now.AddMonths(1).Date.ToString("yyyy"), nopeserta, Common.BPJS.VClaim.Enum.FilterRencanaKontrol.TanggalRencanaKontrol);
                if (rencanaKontrol.MetaData.IsValid)
                {
                    tujuanKunjungan = "3";
                    foreach (var item in rencanaKontrol.Response.List.Where(r => DateTime.ParseExact(r.TglRencanaKontrol, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None) >= DateTime.Now.Date && r.TerbitSEP.ToLower() == "belum"))
                    {
                        listRujukan.Add(new ListRujukan() { Text = $"{item.NoSuratKontrol} - {item.NamaPoliTujuan} - (KONTROL) - {item.TglRencanaKontrol}", Value = $"{item.NoSuratKontrol}#{item.PoliTujuan}#{tujuanKunjungan}#0#{item.TglRencanaKontrol}" });
                    }
                }

                //if (!listRujukan.Any())
                {
                    //var valid = false;

                    svc = new Common.BPJS.VClaim.v11.Service();
                    var rujukan = svc.GetRujukan(nopeserta, Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);
                    if (rujukan.MetaData.IsValid)
                    {
                        foreach (var item in rujukan.Response.Rujukan.Where(r => DateTime.Now.Date.Subtract(DateTime.ParseExact(r.TglKunjungan, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None)).TotalDays < 90))
                        {
                            //svc = new Common.BPJS.VClaim.v11.Service();
                            //var jumlah = svc.GetDataJumlahSEPRujukan(Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1, item.NoKunjungan);
                            //if (jumlah.MetaData.IsValid)
                            //{
                            //    if (jumlah.Response.JumlahSEP == "0") tujuanKunjungan = "1";
                            //    else tujuanKunjungan = "3";
                            //}

                            tujuanKunjungan = "1";
                            listRujukan.Add(new ListRujukan() { Text = $"{item.NoKunjungan} - {item.PoliRujukan.Nama} - (RUJUKAN AWAL)", Value = $"{item.NoKunjungan}#{item.PoliRujukan.Kode}#{tujuanKunjungan}#1" });
                            //if (!byRujukan) byRujukan = true;
                        }

                        //valid = true;
                    }

                    svc = new Common.BPJS.VClaim.v11.Service();
                    rujukan = svc.GetRujukan(nopeserta, Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                    if (rujukan.MetaData.IsValid)
                    {
                        foreach (var item in rujukan.Response.Rujukan.Where(r => DateTime.Now.Date.Subtract(DateTime.ParseExact(r.TglKunjungan, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None)).TotalDays < 90))
                        {
                            //svc = new Common.BPJS.VClaim.v11.Service();
                            //var jumlah = svc.GetDataJumlahSEPRujukan(Common.BPJS.VClaim.Enum.JenisFaskes.RS, item.NoKunjungan);
                            //if (jumlah.MetaData.IsValid)
                            //{
                            //    if (jumlah.Response.JumlahSEP == "0") tujuanKunjungan = "4";
                            //    else tujuanKunjungan = "3";
                            //}

                            tujuanKunjungan = "4";
                            listRujukan.Add(new ListRujukan() { Text = $"{item.NoKunjungan} - {item.PoliRujukan.Nama} - (RUJUKAN AWAL)", Value = $"{item.NoKunjungan}#{item.PoliRujukan.Kode}#{tujuanKunjungan}#1" });
                            //if (!byRujukan) byRujukan = true;
                        }

                        //valid = true;
                    }

                    //if (!valid)
                    //{
                    //    return Request.CreateResponse(HttpStatusCode.NotFound, new
                    //    {
                    //        metadata = new
                    //        {
                    //            Code = (int)HttpStatusCode.NotFound, // 404
                    //            Message = "Data peserta / rujukan / kontrol tidak ditemukan"
                    //        }
                    //    });
                    //}
                }

                if (!listRujukan.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        metadata = new
                        {
                            Code = (int)HttpStatusCode.NotFound, // 404
                            Message = "Data peserta / rujukan / kontrol tidak ditemukan"
                        }
                    });
                }
            }

            var patient = new Patient();
            patient.Query.es.Top = 1;
            patient.Query.Where(patient.Query.GuarantorCardNo == nopeserta);
            if (patient.Query.Load())
            {
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.OK, // 200
                        NoKartu = nopeserta,
                        MobilePhoneNo = patient.MobilePhoneNo,
                        Message = listRujukan
                    }
                });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.OK, // 200
                        NoKartu = nopeserta,
                        MobilePhoneNo = string.Empty,
                        Message = listRujukan
                    }
                });
            }
        }

        public class ListRujukan
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("antrol/selfcheckin/GetPesertaDanRujukanByNoPeserta/{nomorPeserta}/{nomorRujukan}/{byNoRujukan}")]
        public HttpResponseMessage GetPesertaDanRujukanByNoPeserta(string nomorPeserta, string nomorRujukan, string byNoRujukan)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            var peserta = svc.GetPeserta(Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta, nomorPeserta, DateTime.Now.Date);
            if (!peserta.MetaData.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.NotFound, // 404
                        Message = peserta.MetaData.Message
                    }
                });
            }

            var rujukan = new Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan();
            if (byNoRujukan == "1")
            {
                svc = new Common.BPJS.VClaim.v11.Service();
                rujukan = svc.GetRujukan(true, nomorRujukan, Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);
                if (!rujukan.MetaData.IsValid)
                {
                    svc = new Common.BPJS.VClaim.v11.Service();
                    rujukan = svc.GetRujukan(true, nomorRujukan, Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                    //if (!rujukan.MetaData.IsValid)
                    //{
                    //    svc = new Common.BPJS.VClaim.v11.Service();
                    //    var kontrol = svc.GetRencanaKontrolByNoSuratKontrol(nomorRujukan);
                    //    if (kontrol.MetaData.IsValid)
                    //    {
                    //        rujukan = new Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan()
                    //        {
                    //            MetaData = new Common.BPJS.VClaim.v11.Rujukan.Select.MetaData()
                    //            {
                    //                Code = kontrol.MetaData.Code,
                    //                Message = kontrol.MetaData.Message,
                    //            },
                    //            Response = new Common.BPJS.VClaim.v11.Rujukan.Select.Response()
                    //            {
                    //                Rujukan = new Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2()
                    //                {
                    //                    NoKunjungan = kontrol.Response.NoSuratKontrol,
                    //                    Peserta = new Common.BPJS.VClaim.v11.Rujukan.Select.Peserta()
                    //                    {
                    //                        NoKartu = peserta.Response.Peserta.NoKartu,
                    //                        Nik = peserta.Response.Peserta.Nik,
                    //                        Mr = new Common.BPJS.VClaim.v11.Rujukan.Select.Mr()
                    //                        {
                    //                            NoMR = peserta.Response.Peserta.Mr.NoMR
                    //                        }
                    //                    },
                    //                    PoliRujukan = new Common.BPJS.VClaim.v11.Rujukan.Select.PoliRujukan()
                    //                    {
                    //                        Kode = kontrol.Response.PoliTujuan,
                    //                        Nama = kontrol.Response.NamaPoliTujuan
                    //                    },
                    //                    TglKunjungan = kontrol.Response.TglRencanaKontrol
                    //                }
                    //            }
                    //        };
                    //    }
                    //    else
                    //        return Request.CreateResponse(HttpStatusCode.NotFound, new
                    //        {
                    //            metadata = new
                    //            {
                    //                Code = (int)HttpStatusCode.NotFound, // 404
                    //                Message = rujukan.MetaData.Message
                    //            }
                    //        });
                    //}
                    if (rujukan.MetaData.IsValid)
                    {
                        if (DateTime.Now.Date.Subtract(DateTime.ParseExact(rujukan.Response.Rujukan.TglKunjungan, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None)).TotalDays > 90)
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, new
                            {
                                metadata = new
                                {
                                    Code = (int)HttpStatusCode.NotFound, // 404
                                    Message = "Rujukan Sudah Tidak Berlaku, Lebih Dari 90 Hari"
                                }
                            });
                        }

                        svc = new Common.BPJS.VClaim.v11.Service();
                        var jumlahsep = svc.GetDataJumlahSEPRujukan(Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1, nomorRujukan);
                        if (jumlahsep.MetaData.IsValid && jumlahsep.Response.JumlahSEP != "0")
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, new
                            {
                                metadata = new
                                {
                                    Code = (int)HttpStatusCode.NotFound, // 404
                                    Message = "Rujukan Sudah Terbit SEP"
                                }
                            });
                        }

                        rujukan = new Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan()
                        {
                            MetaData = new Common.BPJS.VClaim.v11.Rujukan.Select.MetaData()
                            {
                                Code = rujukan.MetaData.Code,
                                Message = rujukan.MetaData.Message,
                            },
                            Response = new Common.BPJS.VClaim.v11.Rujukan.Select.Response()
                            {
                                Rujukan = new Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2()
                                {
                                    NoKunjungan = rujukan.Response.Rujukan.NoKunjungan,
                                    Peserta = new Common.BPJS.VClaim.v11.Rujukan.Select.Peserta()
                                    {
                                        NoKartu = peserta.Response.Peserta.NoKartu,
                                        Nik = peserta.Response.Peserta.Nik,
                                        Mr = new Common.BPJS.VClaim.v11.Rujukan.Select.Mr()
                                        {
                                            NoMR = peserta.Response.Peserta.Mr.NoMR
                                        }
                                    },
                                    PoliRujukan = new Common.BPJS.VClaim.v11.Rujukan.Select.PoliRujukan()
                                    {
                                        Kode = rujukan.Response.Rujukan.PoliRujukan.Kode,
                                        Nama = rujukan.Response.Rujukan.PoliRujukan.Nama
                                    },
                                    TglKunjungan = rujukan.Response.Rujukan.TglKunjungan
                                }
                            }
                        };
                    }
                }
                else
                {
                    if (DateTime.Now.Date.Subtract(DateTime.ParseExact(rujukan.Response.Rujukan.TglKunjungan, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None)).TotalDays > 90)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, new
                        {
                            metadata = new
                            {
                                Code = (int)HttpStatusCode.NotFound, // 404
                                Message = "Rujukan Sudah Tidak Berlaku, Lebih Dari 90 Hari"
                            }
                        });
                    }

                    svc = new Common.BPJS.VClaim.v11.Service();
                    var jumlahsep = svc.GetDataJumlahSEPRujukan(Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1, nomorRujukan);
                    if (jumlahsep.MetaData.IsValid && jumlahsep.Response.JumlahSEP != "0")
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, new
                        {
                            metadata = new
                            {
                                Code = (int)HttpStatusCode.NotFound, // 404
                                Message = "Rujukan Sudah Terbit SEP"
                            }
                        });
                    }

                    rujukan = new Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan()
                    {
                        MetaData = new Common.BPJS.VClaim.v11.Rujukan.Select.MetaData()
                        {
                            Code = rujukan.MetaData.Code,
                            Message = rujukan.MetaData.Message,
                        },
                        Response = new Common.BPJS.VClaim.v11.Rujukan.Select.Response()
                        {
                            Rujukan = new Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2()
                            {
                                NoKunjungan = rujukan.Response.Rujukan.NoKunjungan,
                                Peserta = new Common.BPJS.VClaim.v11.Rujukan.Select.Peserta()
                                {
                                    NoKartu = peserta.Response.Peserta.NoKartu,
                                    Nik = peserta.Response.Peserta.Nik,
                                    Mr = new Common.BPJS.VClaim.v11.Rujukan.Select.Mr()
                                    {
                                        NoMR = peserta.Response.Peserta.Mr.NoMR
                                    }
                                },
                                PoliRujukan = new Common.BPJS.VClaim.v11.Rujukan.Select.PoliRujukan()
                                {
                                    Kode = rujukan.Response.Rujukan.PoliRujukan.Kode,
                                    Nama = rujukan.Response.Rujukan.PoliRujukan.Nama
                                },
                                TglKunjungan = rujukan.Response.Rujukan.TglKunjungan
                            }
                        }
                    };
                }
            }
            else
            {
                svc = new Common.BPJS.VClaim.v11.Service();
                var kontrol = svc.GetRencanaKontrolByNoSuratKontrol(nomorRujukan);
                if (kontrol.MetaData.IsValid)
                {
                    rujukan = new Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan()
                    {
                        MetaData = new Common.BPJS.VClaim.v11.Rujukan.Select.MetaData()
                        {
                            Code = kontrol.MetaData.Code,
                            Message = kontrol.MetaData.Message,
                        },
                        Response = new Common.BPJS.VClaim.v11.Rujukan.Select.Response()
                        {
                            Rujukan = new Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2()
                            {
                                NoKunjungan = kontrol.Response.NoSuratKontrol,
                                Peserta = new Common.BPJS.VClaim.v11.Rujukan.Select.Peserta()
                                {
                                    NoKartu = peserta.Response.Peserta.NoKartu,
                                    Nik = peserta.Response.Peserta.Nik,
                                    Mr = new Common.BPJS.VClaim.v11.Rujukan.Select.Mr()
                                    {
                                        NoMR = peserta.Response.Peserta.Mr.NoMR
                                    }
                                },
                                PoliRujukan = new Common.BPJS.VClaim.v11.Rujukan.Select.PoliRujukan()
                                {
                                    Kode = kontrol.Response.PoliTujuan,
                                    Nama = kontrol.Response.NamaPoliTujuan
                                }
                            }
                        }
                    };
                }
            }

            var patient = new Patient();
            patient.Query.es.Top = 1;
            patient.Query.Where(patient.Query.GuarantorCardNo == nomorPeserta);
            patient.Query.Load();

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                metadata = new
                {
                    Code = (int)HttpStatusCode.OK, // 200
                    NoMr = patient.MedicalNo,
                    Message = rujukan
                }
            });
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("antrol/selfcheckin/GetDokterByTanggalDanKodePoli/{tanggal}/{kdpoli}")]
        // format tanggal = yyyyMMdd
        public HttpResponseMessage GetDokterByTanggalDanKodePoli(string tanggal, string kdpoli)
        {
            var date = DateTime.ParseExact(tanggal, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None);

            if (date.Date == DateTime.Now.Date)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.NotFound, // 404
                        Message = $"Rencana Kunjungan Minimal H-1"
                    }
                });
            }

            var poli = new ServiceUnitBridging();
            poli.Query.Where(poli.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
            poli.Query.Where($"< SUBSTRING(BridgingID, CHARINDEX(';', BridgingID) + 1, 3) = '{kdpoli}'>");
            if (!poli.Query.Load())
            {
                poli = new ServiceUnitBridging();
                poli.Query.Where(poli.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                poli.Query.Where($"< SUBSTRING(BridgingID, 0, CHARINDEX(';', BridgingID)) = '{kdpoli}'>");
                if (!poli.Query.Load())
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        metadata = new
                        {
                            Code = (int)HttpStatusCode.NotFound, // 404
                            Message = $"Kode Poli {kdpoli} Tidak Ditemukan"
                        }
                    });
                }
            }

            var dokter = new ParamedicQuery("a");
            var jadwal = new ParamedicScheduleDateQuery("b");
            var bridging = new ParamedicBridgingQuery("c");

            dokter.Select(bridging.BridgingID, dokter.ParamedicName);
            dokter.InnerJoin(jadwal).On(dokter.ParamedicID == jadwal.ParamedicID && jadwal.ServiceUnitID == poli.ServiceUnitID && jadwal.PeriodYear == date.Year && jadwal.ScheduleDate.Date() == date.Date);
            dokter.InnerJoin(bridging).On(dokter.ParamedicID == bridging.ParamedicID && bridging.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());

            var table = dokter.LoadDataTable();
            if (table.Rows.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.NotFound, // 404
                        Message = $"Jadwal Dokter Tidak Ditemukan"
                    }
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                metadata = new
                {
                    Code = (int)HttpStatusCode.OK, // 200
                    Message = table.AsEnumerable().Select(t => new
                    {
                        ParamedicID = t.Field<string>("BridgingID"),
                        ParamedicName = t.Field<string>("ParamedicName")
                    })
                }
            });
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("antrol/selfcheckin/GetDokterByTanggalDanKodePoliSelfCheckIn/{tanggal}/{kdpoli}/{byNoRujukan}/{noKunjungan}")]
        // format tanggal = yyyyMMdd
        public HttpResponseMessage GetDokterByTanggalDanKodePoliSelfCheckIn(string tanggal, string kdpoli, string byNoRujukan, string noKunjungan)
        {
            var date = DateTime.ParseExact(tanggal, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None);

            //if (date.Date == DateTime.Now.Date)
            //{
            //    return Request.CreateResponse(HttpStatusCode.NotFound, new
            //    {
            //        metadata = new
            //        {
            //            Code = (int)HttpStatusCode.NotFound, // 404
            //            Message = $"Rencana Kunjungan Minimal H-1"
            //        }
            //    });
            //}


            var polis = new ServiceUnitBridgingCollection();
            polis.Query.Where(polis.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
            polis.Query.Where($"< SUBSTRING(BridgingID, CHARINDEX(';', BridgingID) + 1, 3) = '{kdpoli}'>");
            if (!polis.Query.Load())
            {
                polis = new ServiceUnitBridgingCollection();
                polis.Query.Where(polis.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                polis.Query.Where($"< SUBSTRING(BridgingID, 0, CHARINDEX(';', BridgingID)) = '{kdpoli}'>");
                if (!polis.Query.Load())
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        metadata = new
                        {
                            Code = (int)HttpStatusCode.NotFound, // 404
                            Message = $"Kode Poli {kdpoli} Tidak Ditemukan"
                        }
                    });
                }
            }

            var dokter = new ParamedicQuery("a");
            var jadwal = new ParamedicScheduleDateQuery("b");
            var bridging = new ParamedicBridgingQuery("c");

            dokter.es.Distinct = true;
            dokter.Select(bridging.BridgingID, dokter.ParamedicName);
            dokter.InnerJoin(jadwal).On(dokter.ParamedicID == jadwal.ParamedicID && jadwal.ServiceUnitID.In(polis.Select(p => p.ServiceUnitID)) && jadwal.PeriodYear == date.Year && jadwal.ScheduleDate.Date() == date.Date);
            dokter.InnerJoin(bridging).On(dokter.ParamedicID == bridging.ParamedicID && bridging.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
            if (byNoRujukan == "0")
            {
                var svc = new Common.BPJS.VClaim.v11.Service();
                var skdpspri = svc.GetRencanaKontrolByNoSuratKontrol(noKunjungan);
                if (skdpspri.MetaData.IsValid)
                {
                    //var dpjpKontrol = string.IsNullOrWhiteSpace(skdpspri.Response.KodeDokterPembuat) ? skdpspri.Response.KodeDokter : skdpspri.Response.KodeDokterPembuat;
                    dokter.Where(bridging.BridgingID == skdpspri.Response.KodeDokter);
                }
            }

            var table = dokter.LoadDataTable();
            if (table.Rows.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.NotFound, // 404
                        Message = $"Jadwal Dokter Tidak Ditemukan"
                    }
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                metadata = new
                {
                    Code = (int)HttpStatusCode.OK, // 200
                    Message = table.AsEnumerable().Select(t => new
                    {
                        ParamedicID = t.Field<string>("BridgingID"),
                        ParamedicName = t.Field<string>("ParamedicName")
                    })
                }
            });
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("antrol/selfcheckin/GetJadwalDokterByTanggalKodePoliDanKodeDokter/{tanggal}/{kdpoli}/{kddokter}")]
        // format tanggal = yyyyMMdd
        // kddokter = ParamedicID
        public HttpResponseMessage GetJadwalDokterByTanggalKodePoliDanKodeDokter(string tanggal, string kdpoli, string kddokter)
        {
            var date = DateTime.ParseExact(tanggal, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None);

            var polis = new ServiceUnitBridgingCollection();
            polis.Query.Where(polis.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
            polis.Query.Where($"< SUBSTRING(BridgingID, CHARINDEX(';', BridgingID) + 1, 3) = '{kdpoli}'>");
            if (!polis.Query.Load())
            {
                polis = new ServiceUnitBridgingCollection();
                polis.Query.Where(polis.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                polis.Query.Where($"< SUBSTRING(BridgingID, 0, CHARINDEX(';', BridgingID)) = '{kdpoli}'>");
                if (!polis.Query.Load())
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        metadata = new
                        {
                            Code = (int)HttpStatusCode.NotFound, // 404
                            Message = $"Kode Poli {kdpoli} Tidak Ditemukan"
                        }
                    });
                }
            }

            var bridging = new ParamedicBridging();
            bridging.Query.Where(bridging.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString(), bridging.Query.BridgingID == kddokter);
            if (!bridging.Query.Load())
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.NotFound, // 404
                        Message = $"Kode Dokter {kddokter} Tidak Ditemukan"
                    }
                });
            }

            var p = new Paramedic();
            p.LoadByPrimaryKey(bridging.ParamedicID);

            var jadwalexist = false;
            var svc = new Common.BPJS.Antrian.Service();
            var hfis = svc.GetJadwalDokter(polis.First().BridgingID.Split(';')[0], tanggal);
            if (hfis.Metadata.IsAntrolValid)
            {
                jadwalexist = true;

                var day = 0;
                if (date.DayOfWeek == DayOfWeek.Sunday) day = 7;
                else day = (int)date.DayOfWeek;

                if (hfis.Response.List == null && !hfis.Response.List.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new
                    {
                        metadata = new
                        {
                            Code = (int)HttpStatusCode.NotFound,
                            Message = $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                        }
                    });
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new
                    {
                        Code = hfis.Metadata.Code, // 201
                        Message = hfis.Metadata.Message
                    }
                });
            }
            if (!jadwalexist)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.NotFound,
                        Message =
                            $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                    }
                });
            }

            var list = new List<ListRujukan>();
            var remove = new List<ListRujukan>();

            var su = new ServiceUnit();
            var jadwal = new ParamedicScheduleDate();
            var isjadwal = false;
            foreach (var poli in polis)
            {
                su = new ServiceUnit();
                su.LoadByPrimaryKey(poli.ServiceUnitID);

                var ps = new BusinessObject.ParamedicSchedule();
                if (!ps.LoadByPrimaryKey(poli.ServiceUnitID, bridging.ParamedicID, date.Year.ToString())) continue;
                if (ps.QuotaBpjsOnline == 0) continue;

                jadwal = new ParamedicScheduleDate();
                if (!jadwal.LoadByPrimaryKey(poli.ServiceUnitID, bridging.ParamedicID, date.Year.ToString(), date.Date)) continue;

                var dokter = new ParamedicSchedule();
                dokter.LoadByPrimaryKey(jadwal.ServiceUnitID, bridging.ParamedicID, date.Year.ToString());

                var time = new OperationalTime();
                time.LoadByPrimaryKey(jadwal.OperationalTimeID);

                var ots1 = time.StartTime1;
                var ote1 = time.EndTime1;
                if (!string.IsNullOrWhiteSpace(ots1) && !string.IsNullOrWhiteSpace(ote1)) list.Add(new ListRujukan() { Value = $"{su.ServiceUnitID}#{ots1}-{ote1}", Text = $"{ots1}-{ote1} - {su.ServiceUnitName}" });

                var ots2 = time.StartTime2;
                var ote2 = time.EndTime2;
                if (!string.IsNullOrWhiteSpace(ots2) && !string.IsNullOrWhiteSpace(ote2)) list.Add(new ListRujukan() { Value = $"{su.ServiceUnitID}#{ots2}-{ote2}", Text = $"{ots2}-{ote2} - {su.ServiceUnitName}" });

                var ots3 = time.StartTime3;
                var ote3 = time.EndTime3;
                if (!string.IsNullOrWhiteSpace(ots3) && !string.IsNullOrWhiteSpace(ote3)) list.Add(new ListRujukan() { Value = $"{su.ServiceUnitID}#{ots3}-{ote3}", Text = $"{ots3}-{ote3} - {su.ServiceUnitName}" });

                var ots4 = time.StartTime4;
                var ote4 = time.EndTime4;
                if (!string.IsNullOrWhiteSpace(ots4) && !string.IsNullOrWhiteSpace(ote4)) list.Add(new ListRujukan() { Value = $"{su.ServiceUnitID}#{ots4}-{ote4}", Text = $"{ots4}-{ote4} - {su.ServiceUnitName}" });

                var ots5 = time.StartTime5;
                var ote5 = time.EndTime5;
                if (!string.IsNullOrWhiteSpace(ots5) && !string.IsNullOrWhiteSpace(ote5)) list.Add(new ListRujukan() { Value = $"{su.ServiceUnitID}#{ots5}-{ote5}", Text = $"{ots5}-{ote5} - {su.ServiceUnitName}" });

                remove = new List<ListRujukan>();
                foreach (var item in list)
                {
                    var time1 = item.Text.Split('-')[0];
                    var time2 = item.Text.Split('-')[1];

                    var appt = new AppointmentCollection();
                    appt.Query.Where(
                        appt.Query.ServiceUnitID == su.ServiceUnitID,
                        appt.Query.ParamedicID == bridging.ParamedicID,
                        //appt.Query.AppointmentDate.Date() == date.Date,
                        appt.Query.AppointmentDate >= date.Date, appt.Query.AppointmentDate < date.Date.AddDays(1),
                        appt.Query.AppointmentTime >= time1.Trim(),
                        appt.Query.AppointmentTime <= time2.Trim(),
                        appt.Query.GuarantorID.In(AppSession.Parameter.GuarantorAskesID));
                    //if (AppSession.Parameter.HealthcareID != "RSES") // kerinci
                    appt.Query.Where(appt.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
                    if (!appt.Query.Load()) continue;
                    if (appt.Count >= dokter.QuotaBpjsOnline) remove.Add(item);
                }
                if (remove.Count > 0) list.RemoveAll(item => remove.Contains(item));
            }

            remove = new List<ListRujukan>();
            foreach (var item in list)
            {
                var time1 = item.Text.Split('-')[0];
                var time2 = item.Text.Split('-')[1];
                var time = $"{time1.Trim()}-{time2.Trim()}";
                if (hfis.Response.List.SingleOrDefault(h => h.Kodepoli == polis.First().BridgingID.Split(';')[0] && h.Kodedokter == bridging.BridgingID.ToInt() && h.Jadwal == time) == null) remove.Add(item);
            }
            if (remove.Count > 0) list.RemoveAll(item => remove.Contains(item));

            if (!list.Any())
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message =
                            $"Jadwal Dokter {p.ParamedicName} Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                    }
                });

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                metadata = new
                {
                    Code = (int)HttpStatusCode.OK, // 200
                    Message = list.OrderBy(l => l.ToString())
                }
            });
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("antrol/selfcheckin/PostRencanaKunjungan")]
        public HttpResponseMessage PostRencanaKunjungan(Antrol.AmbilAntrean.Request.Root param)
        {
            var request = param;
            request.CreatedBy = "kiosk";
            return TambahAntrean(request);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("antrol/getheadervalue")]
        public HttpResponseMessage GetHeaderValueAntrol()
        {
            var svc = new Common.BPJS.Antrian.Service();

            return Request.CreateResponse(HttpStatusCode.OK, svc.GetHeaderValue());
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("vklaim/getheadervalue")]
        public HttpResponseMessage GetHeaderValueVklaim()
        {
            var svc = new Common.BPJS.VClaim.v11.Service();

            return Request.CreateResponse(HttpStatusCode.OK, svc.GetHeaderValue());
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("sitb/getheadervalue")]
        public HttpResponseMessage GetHeaderValueSitb()
        {
            var svc = new Common.SirsKemkes.Service();

            return Request.CreateResponse(HttpStatusCode.OK, svc.GetHeaderValueSitb());
        }

        private static string[] GuarantorBPJS
        {
            get
            {
                var grr = new GuarantorBridgingCollection();
                grr.Query.es.Distinct = true;
                grr.Query.Where(grr.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(), AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString()));
                if (grr.Query.Load()) return grr.Select(g => g.GuarantorID).ToArray();
                else return new string[] { string.Empty };
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("vklaim/updatetglpulang")]
        public HttpResponseMessage UpdateTglPulang(Common.BPJS.VClaim.v20.Sep.UpdateRequest.UpdateTanggalPulang.Root param)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.UpdateTglPulang(new Common.BPJS.VClaim.v20.Sep.UpdateRequest.UpdateTanggalPulang.TSep
            {
                NoSep = param.Request.TSep.NoSep,
                StatusPulang = param.Request.TSep.StatusPulang,
                NoSuratMeninggal = string.Empty,
                TglMeninggal = string.Empty,
                NoLPManual = string.Empty,
                User = "vklaim",
                TglPulang = param.Request.TSep.TglPulang
            });

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                metadata = new
                {
                    Code = response.MetaData.Code,
                    Message = response.MetaData.Message
                },
                response = response.Response
            });
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("vklaim/datahistoripelayananpeserta/{nokartu}/{tglMulai}/{tglAkhir}")]
        public HttpResponseMessage DataHistoriPelayananPeserta(string nokartu, string tglMulai, string tglAkhir)
        {
            string format = "yyyy-MM-dd";
            DateTime.TryParseExact(tglMulai, format, null, System.Globalization.DateTimeStyles.None, out var parsedTglMulai);
            DateTime.TryParseExact(tglAkhir, format, null, System.Globalization.DateTimeStyles.None, out var parsedTglAkhir);

            var svc = new Common.BPJS.VClaim.v11.Service();
            return Request.CreateResponse(HttpStatusCode.OK, svc.GetDataHistoriPelayananPeserta(nokartu, parsedTglMulai.Date, parsedTglAkhir.Date));
        }

        //okadoc
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("okadoc/jadwaloperasirs")]
        public HttpResponseMessage JadwalOperasiRsOkadoc(Antrol.JadwalOperasiRs.Request.Root param)
        {
            return JadwalOperasiRs(param);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("okadoc/jadwaloperasipasien")]
        public HttpResponseMessage JadwalOperasiPasienOkadoc(Antrol.JadwalOperasiPasien.Request.Root param)
        {
            return JadwalOperasiPasien(param);
        }

        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("okadoc/pasienbaru")]
        public HttpResponseMessage InfoPasienBaruOkadoc(Antrol.InfoPasienBaru.Request.Root param)
        {
            return InfoPasienBaru(param);
        }

        [CustomAuthorization]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("antrol/ambilsantreanfarmasi")]
        public HttpResponseMessage AmbilAntreanFarmasi(Antrol.Farmasi.AmbilAntrean.Request.Root param)
        {
            return StatusAntreanFarmasi(param);
        }

        [CustomAuthorization]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("antrol/statusantreanfarmasi")]
        public HttpResponseMessage StatusAntreanFarmasi(Antrol.Farmasi.AmbilAntrean.Request.Root param)
        {
            var reg = new Registration();
            reg.Query.es.Top = 1;
            reg.Query.Where(reg.Query.AppointmentNo == param.kodebooking);
            if (!reg.Query.Load())
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.NotFound, // 404
                        Message = $"Kode Booking Tidak Ditemukan"
                    }
                });
            }

            var tp = new TransPrescription();
            tp.Query.es.Top = 1;
            tp.Query.Where(tp.Query.RegistrationNo == reg.RegistrationNo);
            if (!tp.Query.Load())
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.NotFound, // 404
                        Message = $"Resep Tidak ditemukan"
                    }
                });
            }

            if (tp.IsVoid ?? false)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.NotFound, // 404
                        Message = $"Resep Tidak ditemukan"
                    }
                });
            }


            if (!(tp.IsApproval ?? false))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.NotFound, // 404
                        Message = $"Resep Tidak ditemukan"
                    }
                });
            }

            var std = new AppStandardReferenceItem();
            std.LoadByPrimaryKey(AppEnum.StandardReference.KioskQueueFar.ToString(), tp.SRKioskQueueType);

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                Response = new
                {
                    jenisresep = std.Query.Load() ? std.Note : "non racikan",
                    nomorantrean = tp.KioskQueueNo,
                    totalantrean = "",
                    sisaantrean = "",
                    antreanpanggil = "",
                    keterangan = string.Empty
                },
                metadata = new Antrol.StatusAntrean.Response.Metadata()
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = $"Sukses"
                }
            });
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("vklaim/rujukan/{noRujukan}")]
        public HttpResponseMessage GetRujukan(string noRujukan)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetRujukan(noRujukan);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("antrol/dashboardperbulan/bulan/{bulan}/tahun/{tahun}/waktu/{waktu}")]
        public HttpResponseMessage DahboardPerTanggal(string bulan, string tahun, string waktu)
        {
            var svc = new Common.BPJS.Antrian.Service();
            var response = svc.DashboardPerBulan(bulan, tahun, waktu);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("antrol/getavailableque/{serviceUnitID}/{paramedicID}/{date}/{duration}")]
        public HttpResponseMessage GetAvailableQue(string serviceUnitID, string paramedicID, string date, int duration)
        {
            TimeSpan? time = TimeSpan.ParseExact("08:00", "hh\\:mm", null);
            string format = "yyyy-MM-dd";
            DateTime.TryParseExact(date, format, null, System.Globalization.DateTimeStyles.None, out var parsed);

            int que = 1;
            bool exist = false;
            while (time >= TimeSpan.ParseExact("08:00", "hh\\:mm", null) && time < TimeSpan.ParseExact("11:00", "hh\\:mm", null))
            {
                var appointment = new Appointment();
                appointment.Query.es.Top = 1;
                appointment.Query.Where(
                    appointment.Query.ServiceUnitID == serviceUnitID,
                    appointment.Query.ParamedicID == paramedicID,
                    appointment.Query.AppointmentDate == parsed.Date,
                    appointment.Query.AppointmentQue == que);
                //if (AppSession.Parameter.HealthcareID != "RSES") // kerinci
                appointment.Query.Where(appointment.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
                if (!appointment.Query.Load())
                {
                    var registration = new Registration();
                    registration.Query.es.Top = 1;
                    registration.Query.Where(
                        registration.Query.ServiceUnitID == serviceUnitID,
                        registration.Query.ParamedicID == paramedicID,
                        registration.Query.RegistrationDate == parsed.Date,
                        registration.Query.RegistrationQue == que,
                        registration.Query.IsVoid == false);
                    exist = registration.Query.Load();
                }
                else exist = true;

                if (!exist) break;

                que++;
                time = time.Value.Add(new TimeSpan(0, duration, 0));
            }

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                que,
                time
            });
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("antrol/antreanperkodebooking/{kodeBooking}")]
        public HttpResponseMessage AntreanPerKodeBooking(string kodeBooking)
        {
            var svc = new Common.BPJS.Antrian.Service();
            var response = svc.GetAntreanPerKodeBooking(kodeBooking);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("antrol/antreanbelumdilayani")]
        public HttpResponseMessage AntreanBelumDilayani()
        {
            var svc = new Common.BPJS.Antrian.Service();
            var response = svc.GetAntreanBelumDilayani(string.Empty);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("antrol/test")]
        //public HttpResponseMessage Test()
        //{
        //    var antreanDateTime = Convert.ToDateTime("2023-08-25 14:00:00");
        //    if (antreanDateTime.Subtract(DateTime.Now).TotalHours > 2)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound, new
        //        {
        //            metadata = new
        //            {
        //                Code = (int)HttpStatusCode.NotFound, // 404
        //                Message = antreanDateTime.Subtract(DateTime.Now).TotalHours
        //            }
        //        });
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK, new
        //    {
        //        metadata = new
        //        {
        //            Code = (int)HttpStatusCode.OK, // 404
        //            Message = antreanDateTime.Subtract(DateTime.Now).TotalHours
        //        }
        //    });
        //}

        //[AllowAnonymous]
        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("sekayu/test")]
        //public HttpResponseMessage SekayuTest()
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, new Antrol.InfoPasienBaru.Request.Root());
        //}

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("sekayu/statuspeserta/{tipe}/{nomor}")]
        public HttpResponseMessage SekayuStatusPeserta(string tipe, string nomor)
        {
            if (string.IsNullOrWhiteSpace(tipe) || string.IsNullOrWhiteSpace(nomor)) return Request.CreateResponse(HttpStatusCode.BadRequest, "Tipe pencarian atau nomor tidak boleh kosong");

            var patient = new Patient();
            if (tipe == "norm") patient.Query.Where(patient.Query.MedicalNo == nomor);
            else if (tipe == "nik") patient.Query.Where(patient.Query.Ssn == nomor);
            else if (tipe == "nopeserta") patient.Query.Where(patient.Query.GuarantorCardNo == nomor);
            else return Request.CreateResponse(HttpStatusCode.BadRequest, "Tipe pencarian tidak boleh kosong : norm, nik, nopeserta");

            if (patient.Query.Load())
            {
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    nik = patient.Ssn,
                    nama = patient.PatientName,
                    nokartu = patient.GuarantorCardNo,
                    nohp = patient.MobilePhoneNo,
                    norm = patient.MedicalNo,
                    pasienbaru = 0
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                nik = string.Empty,
                nama = string.Empty,
                nokartu = string.Empty,
                nohp = string.Empty,
                norm = string.Empty,
                pasienbaru = 1
            });
        }

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("sekayu/listpoli")]
        public HttpResponseMessage SekayuListPoli()
        {
            var unit = new ServiceUnitQuery("a");
            var sub = new ServiceUnitBridgingQuery("b");
            var sup = new ServiceUnitParamedicQuery("c");
            var pb = new ParamedicBridgingQuery("e");

            unit.Select
                (
                    "<substring(b.BridgingID, charindex(';', b.BridgingID) + 1, 3) AS ServiceUnitID>",
                    sub.BridgingID.As("ServiceUnitID"),
                    unit.ServiceUnitName,
                    pb.ParamedicID.Count()
                );
            unit.InnerJoin(sub).On(unit.ServiceUnitID == sub.ServiceUnitID && sub.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
            unit.InnerJoin(sup).On(unit.ServiceUnitID == sup.ServiceUnitID);
            unit.InnerJoin(pb).On(sup.ParamedicID == pb.ParamedicID && pb.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
            unit.GroupBy(sub.BridgingID, unit.ServiceUnitName);
            unit.Where(unit.IsActive == true);

            var table = unit.LoadDataTable();
            var list = table.AsEnumerable().Select(t => new
            {
                kodepoli = t.Field<string>("ServiceUnitID").ToString(),
                namapoli = t.Field<string>("ServiceUnitName").ToString(),
                //sisakuotajkn = string.Empty,
                //sisakuotanonjkn = string.Empty,
                //status = string.Empty,
                jumlahdokter = t.Field<int>("ParamedicID").ToString()
            }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                list
            });
        }

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("sekayu/listdokterpoli/{kodePoli}/{tanggal}")]
        public HttpResponseMessage SekayuListDokterPoli(string kodePoli, string tanggal)
        {
            DateTime.TryParseExact(tanggal, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var date);

            var dokter = new ParamedicQuery("a");
            var pb = new ParamedicBridgingQuery("b");
            var sup = new ServiceUnitParamedicQuery("c");
            var sub = new ServiceUnitBridgingQuery("d");

            dokter.Select
            (
                dokter.ParamedicID,
                dokter.ParamedicName,
                pb.BridgingID,
                sup.ServiceUnitID,
                "<'0' AS sisakuotajkn>",
                "<'0' AS sisakuotanonjkn>",
                "<'tutup' AS status>",
                "<'00:00-00:00' AS jampraktek>"
            );

            dokter.InnerJoin(pb).On(dokter.ParamedicID == pb.ParamedicID && pb.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
            dokter.InnerJoin(sup).On(dokter.ParamedicID == sup.ParamedicID);
            dokter.InnerJoin(sub).On(sup.ServiceUnitID == sub.ServiceUnitID && sub.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
            dokter.Where($"< SUBSTRING(d.BridgingID, CHARINDEX(';', d.BridgingID) + 1, 3) = '{kodePoli}'>");
            dokter.Where(dokter.IsActive == true);

            var table = dokter.LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                var appointments = new AppointmentCollection();
                appointments.Query.Where(appointments.Query.ServiceUnitID == row["ServiceUnitID"].ToString(),
                    appointments.Query.ParamedicID == row["ParamedicID"].ToString(),
                    //appointments.Query.AppointmentDate.Date() == date.Date,
                    appointments.Query.AppointmentDate >= date.Date, appointments.Query.AppointmentDate < date.Date.AddDays(1),
                    appointments.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
                appointments.Query.Load();

                var ps = new ParamedicSchedule();
                ps.Query.Where(ps.Query.ParamedicID == row["ParamedicID"].ToString(), ps.Query.PeriodYear == date.Year);
                if (ps.Query.Load())
                {
                    row["sisakuotajkn"] = ps.QuotaBpjsOnline - appointments.Count(a => AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) <= 0 ? 0 : ps.QuotaBpjsOnline - appointments.Count(a => AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID));
                    row["sisakuotanonjkn"] = ps.QuotaOnline - appointments.Count(a => !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID)) <= 0 ? 0 : ps.QuotaOnline - appointments.Count(a => !AppSession.Parameter.GuarantorAskesID.Contains(a.GuarantorID));
                }

                var psd = new ParamedicScheduleDate();
                psd.Query.Where(psd.Query.ParamedicID == row["ParamedicID"].ToString(), psd.Query.ScheduleDate.Date() == date.Date);
                if (psd.Query.Load())
                {
                    row["status"] = $"buka";
                    var ot = new OperationalTime();
                    ot.LoadByPrimaryKey(psd.OperationalTimeID);
                    row["jampraktek"] = $"{ot.StartTime1}-{ot.EndTime1}";

                    if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                    {

                    }
                }
                row.AcceptChanges();
            }
            table.AcceptChanges();

            var list = table.AsEnumerable().Select(t => new
            {
                namadokter = t.Field<string>("ParamedicName").ToString(),
                kodedokterbpjs = t.Field<string>("BridgingID").ToString(),
                jampraktek = t.Field<string>("jampraktek").ToString(),
                sisakuotajkn = t.Field<string>("sisakuotajkn").ToString(),
                sisakuotanonjkn = t.Field<string>("sisakuotanonjkn").ToString(),
                status = t.Field<string>("status").ToString()
            }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                list
            });
        }

        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("sekayu/ambilantrean")]
        public HttpResponseMessage SekayuAmbilAntrean(AmbilAntreanSekayuRequest param)
        {
            DateTime.TryParseExact(param.tanggalperiksa, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var date);

            //if (param.jenispasien.ToLower() == "jkn")
            //{
            var jkn = AmbilAntrean(new Antrol.AmbilAntrean.Request.Root()
            {
                Nomorkartu = param.nomorkartu,
                Nik = param.nik,
                Nohp = param.nohp,
                Kodepoli = param.kodepoli,
                Norm = param.norm,
                Tanggalperiksa = param.tanggalperiksa,
                Kodedokter = param.kodedokter,
                Jampraktek = param.jampraktek,
                Jeniskunjungan = param.jeniskunjungan,
                Nomorreferensi = param.nomorreferensi
            });

            var response = JsonConvert.DeserializeObject<Antrol.AmbilAntrean.Response.Root>(jkn.Content.ReadAsStringAsync().Result);
            if (response.metadata.Code == 200)
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    response.Response.Kodebooking,
                    param.jenispasien,
                    param.nomorkartu,
                    param.nik,
                    param.nohp,
                    param.kodepoli,
                    param.namapoli,
                    param.pasienbaru,
                    param.norm,
                    param.tanggalperiksa,
                    param.kodedokter,
                    param.namadokter,
                    param.jampraktek,
                    param.jeniskunjungan,
                    param.nomorreferensi,
                    response.Response.Nomorantrean,
                    response.Response.Angkaantrean,
                    response.Response.Estimasidilayani,
                    response.Response.Sisakuotajkn,
                    response.Response.Sisakuotanonjkn,
                    response.Response.Kuotajkn,
                    response.Response.Kuotanonjkn,
                    response.Response.Keterangan
                });
            return Request.CreateResponse(HttpStatusCode.Created, new
            {
                response.metadata.Code,
                response.metadata.Message
            });
            //}
        }

        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("sekayu/pasienbaru")]
        public HttpResponseMessage SekayuPasienBaru(Antrol.InfoPasienBaru.Request.Root param)
        {
            return InfoPasienBaru(param);
        }

        public class AmbilAntreanSekayuRequest
        {
            [JsonProperty("jenispasien")]
            public string jenispasien;
            [JsonProperty("nomorkartu")]
            public string nomorkartu;
            [JsonProperty("nik")]
            public string nik;
            [JsonProperty("nohp")]
            public string nohp;
            [JsonProperty("kodepoli")]
            public string kodepoli;
            [JsonProperty("namapoli")]
            public string namapoli;
            [JsonProperty("pasienbaru")]
            public string pasienbaru;
            [JsonProperty("norm")]
            public string norm;
            [JsonProperty("tanggalperiksa")]
            public string tanggalperiksa;
            [JsonProperty("kodedokter")]
            public int kodedokter;
            [JsonProperty("namadokter")]
            public string namadokter;
            [JsonProperty("jampraktek")]
            public string jampraktek;
            [JsonProperty("jeniskunjungan")]
            public int jeniskunjungan;
            [JsonProperty("nomorreferensi")]
            public string nomorreferensi;
        }

        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("antrol/cetakesep/{nomor}")]
        public HttpResponseMessage CetakESep(string nomor)
        {
            {
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "10.200.200.188",
                    UrlAddress = "CetakESep",
                    Params = nomor,
                    Response = string.Empty,
                    Totalms = 0
                };
                log.Save();
            }

            var prg = new AppProgram();
            if (!prg.LoadByPrimaryKey(AppConstant.Report.BpjsESEP)) return Request.CreateResponse(HttpStatusCode.OK);

            var printJobParameters = new PrintJobParameterCollection();
            printJobParameters.AddNew("p_NoSep", nomor);

            var path = Module.Reports.ReportViewer.SaveFileToGuarantorDocument(AppSession.Parameter.HealthcareInitial, AppConstant.Report.BpjsESEP, printJobParameters);

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                nosep = nomor,
                path = path
            });
        }

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("antrol/cekkuota")]
        public HttpResponseMessage CekKuota()
        {
            var ps = new ParamedicSchedule();
            ps.Query.Where(ps.Query.ServiceUnitID == "U03.02.30", ps.Query.ParamedicID == "MD-00127", ps.Query.PeriodYear == "2024");
            if (!ps.Query.Load())
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.AmbilAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Jadwal Dokter Tersebut Belum Tersedia, Silahkan Reschedule Tanggal dan Jam Praktek Lainnya"
                    }
                });
            }
            else
            {
                if ((ps.QuotaBpjsOnline ?? 0) == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Kapasitas antrian sudah penuh"
                        }
                    });
                }
                else
                {
                    var appt = new AppointmentCollection();
                    appt.Query.Where(appt.Query.ServiceUnitID == "U03.02.30",
                        appt.Query.ParamedicID == "MD-00127",
                        //appt.Query.AppointmentDate.Date() == new DateTime(2024, 6, 7).Date,
                        appt.Query.AppointmentDate >= new DateTime(2024, 6, 7).Date, appt.Query.AppointmentDate < new DateTime(2024, 6, 7).Date.AddDays(1),
                        appt.Query.AppointmentTime >= "09:30",
                        appt.Query.AppointmentTime <= "12:30",
                        appt.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                        );
                    appt.Query.Load();
                    if (appt.Count >= (ps.QuotaBpjsOnline ?? 0))
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, new
                        {
                            metadata = new Antrol.AmbilAntrean.Response.Metadata()
                            {
                                Code = (int)HttpStatusCode.Created,
                                Message = $"Kapasitas antrian sudah penuh"
                            }
                        });
                    }
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("antrol/sandbox/cekhari/{minDayBefore}/{maxDayBefore}/{tanggal}")]
        public HttpResponseMessage CekHari(int minDayBefore, int maxDayBefore, string tanggal)
        {
            var tglRencana = DateTime.ParseExact(tanggal, "yyyyMMdd", null, DateTimeStyles.None);

            var tglMaxRencana = tglRencana.Date.AddDays(-1 * minDayBefore);
            var tglMinRencana = tglRencana.Date.AddDays(-1 * maxDayBefore);

            var maxDayAfter = 365;

            if (DateTime.Now.Date < tglMinRencana.Date)
            {
                return Request.CreateResponse(HttpStatusCode.Created, new
                {
                    metadata = new Antrol.AmbilAntrean.Response.Metadata()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = $"Booking dapat dilakukan minimal {maxDayBefore} hari sebelumnya"
                    }
                });
            }
            if (minDayBefore > 0)
            {
                if (DateTime.Now.Date > tglMaxRencana.Date)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, new
                    {
                        metadata = new Antrol.AmbilAntrean.Response.Metadata()
                        {
                            Code = (int)HttpStatusCode.Created,
                            Message = $"Booking dapat dilakukan maksimal {minDayBefore} hari sebelumnya"
                        }
                    });
                }
            }

            //var minDate = parsed.Date.AddDays(-1 * minDay);
            //var maxDate = DateTime.Now.Date.AddDays(maxDay);

            //if (minDay > 0 && DateTime.Now.Date < minDate)
            //{
            //    return Request.CreateResponse(HttpStatusCode.Created, new
            //    {
            //        metadata = new Antrol.AmbilAntrean.Response.Metadata()
            //        {
            //            Code = (int)HttpStatusCode.Created,
            //            Message = $"Booking dapat dilakukan minimal {minDay} hari sebelumnya"
            //        }
            //    });
            //}
            //else if (parsed.Date >= minDate && parsed.Date <= maxDate)
            //{
            //    // ok
            //}
            //else if (parsed.Date > maxDate)
            //{
            //    return Request.CreateResponse(HttpStatusCode.Created, new
            //    {
            //        metadata = new Antrol.AmbilAntrean.Response.Metadata()
            //        {
            //            Code = (int)HttpStatusCode.Created,
            //            Message = $"Booking dapat dilakukan maksimal {maxDay} hari berikutnya"
            //        }
            //    });
            //}

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                metadata = new Antrol.AmbilAntrean.Response.Metadata()
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = $"Ok"
                }
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("antrol/selfcheckin/decodeqr")]
        public HttpResponseMessage Decode64String(String64 message)
        {
            try
            {
                var str = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(message.Message));
                var param = JsonConvert.DeserializeObject<Antrol.CheckIn.Request64String.Root>(str);

                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.OK
                    },
                    message = param
                });
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    metadata = new
                    {
                        Code = (int)HttpStatusCode.OK
                    },
                    message = string.Empty
                });
            }
        }

        public class String64
        {
            [JsonProperty("message")]
            public string Message { get; set; }
        }
    }
}