using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Web.Script.Services;
using Temiang.Avicenna.Common;
using Newtonsoft.Json;


namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for Applicares
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Applicares : System.Web.Services.WebService
    {
        [WebMethod]
        public string InsertRuangan()
        {
            if (AppSession.Parameter.IsAplicaresByRoomName)
            {
                foreach (DataRow row in new ServiceUnitCollection().InpatientBedAvailabilityV2().Rows)
                {
                    var service = new Common.BPJS.Applicare.Service();
                    var response = service.InsertRuangan(new Common.BPJS.Applicare.RuanganBaru.RootObject()
                    {
                        kodekelas = row["BpjsClassID"].ToString(),
                        koderuang = row["RoomID"].ToString(),
                        namaruang = row["ServiceUnitName"].ToString(),
                        kapasitas = row["Capacity"].ToString(),
                        tersedia = row["Available"].ToString(),
                        tersediapria = "0",
                        tersediawanita = "0",
                        tersediapriawanita = "0"
                    });
                    var meta = fastJSON.JSON.ToObject<Common.BPJS.MetadataResponse>(response);
                    if (meta.Metadata.Code != "1") return String.Format("Server message (HTTP {0}: {1}).", meta.Metadata.Code, meta.Metadata.Message);
                }
                return "success";
            }
            else
            {
                var str = string.Empty;
                foreach (DataRow row in new ServiceUnitCollection().InpatientBedAvailability().Rows)
                {
                    var param = new Common.BPJS.Applicare.RuanganBaru.RootObject()
                    {
                        kodekelas = row["BpjsClassID"].ToString(),
                        koderuang = row["RoomID"].ToString(),
                        namaruang = row["ServiceUnitName"].ToString(),
                        kapasitas = row["Capacity"].ToString(),
                        tersedia = row["Available"].ToString(),
                        tersediapria = "0",
                        tersediawanita = "0",
                        tersediapriawanita = "0"
                    };

                    var service = new Common.BPJS.Applicare.Service();
                    var response = service.InsertRuangan(param);
                    var meta = fastJSON.JSON.ToObject<Common.BPJS.MetadataResponse>(response);
                    if (meta.Metadata.Code != "1") str += String.Format("Server message (HTTP {0}: {1} {2}).", meta.Metadata.Code, meta.Metadata.Message, JsonConvert.SerializeObject(param));
                }
                return !string.IsNullOrWhiteSpace(str) ? str : "success";
            }

        }

        [WebMethod]
        public string InsertSatuRuangan(string kodeKelasBpjs, string kodeRuang, string namaRuang, string kapasitas, string tersedia)
        {
            var service = new Common.BPJS.Applicare.Service();
            var response = service.InsertRuangan(new Common.BPJS.Applicare.RuanganBaru.RootObject()
            {
                kodekelas = kodeKelasBpjs,
                koderuang = kodeRuang,
                namaruang = namaRuang,
                kapasitas = kapasitas,
                tersedia = tersedia,
                tersediapria = "0",
                tersediawanita = "0",
                tersediapriawanita = "0"
            });
            var meta = fastJSON.JSON.ToObject<Common.BPJS.MetadataResponse>(response);
            if (meta.Metadata.Code != "1") return String.Format("Server message (HTTP {0}: {1}).", meta.Metadata.Code, meta.Metadata.Message);
            return "success";
        }

        [WebMethod]
        public string UpdateRuangan()
        {
            if (AppSession.Parameter.IsAplicaresByRoomName)
            {
                foreach (DataRow row in new ServiceUnitCollection().InpatientBedAvailabilityV2().Rows)
                {
                    var service = new Common.BPJS.Applicare.Service();
                    var response = service.DeleteRuangan(new Temiang.Avicenna.Common.BPJS.Applicare.HapusRuangan.RootObject()
                    {
                        kodekelas = row["BpjsClassID"].ToString(),
                        koderuang = row["RoomID"].ToString()
                    });
                    var meta = fastJSON.JSON.ToObject<Common.BPJS.MetadataResponse>(response);
                    //if (meta.Metadata.Code != 1) return String.Format("Server message (HTTP {0}: {1}).", meta.Metadata.Code, meta.Metadata.Message);
                    if (meta.Metadata.Code == "0")
                    {
                        var str = InsertSatuRuangan(row["BpjsClassID"].ToString(), row["ServiceUnitID"].ToString(), row["ServiceUnitName"].ToString(), row["Capacity"].ToString(), row["Available"].ToString());
                        //if (str != "success") return String.Format("Server message (HTTP {0}: {1}).", meta.Metadata.Code, meta.Metadata.Message);
                    }
                    service = new Common.BPJS.Applicare.Service();
                    response = service.UpdateRuangan(new Common.BPJS.Applicare.UpdateKetersediaanTempatTidur.RootObject()
                    {
                        kodekelas = row["BpjsClassID"].ToString(),
                        koderuang = row["RoomID"].ToString(),
                        namaruang = row["ServiceUnitName"].ToString(),
                        kapasitas = row["Capacity"].ToString(),
                        tersedia = row["Available"].ToString(),
                        tersediapria = "0",
                        tersediawanita = "0",
                        tersediapriawanita = "0"
                    });
                    meta = fastJSON.JSON.ToObject<Common.BPJS.MetadataResponse>(response);
                    //if (meta.Metadata.Code != 1) return String.Format("Server message (HTTP {0}: {1}).", meta.Metadata.Code, meta.Metadata.Message);
                    if (meta.Metadata.Code == "0")
                    {
                        var str = InsertSatuRuangan(row["BpjsClassID"].ToString(), row["RoomID"].ToString(), row["ServiceUnitName"].ToString(), row["Capacity"].ToString(), row["Available"].ToString());
                        //if (str != "success") return String.Format("Server message (HTTP {0}: {1}).", meta.Metadata.Code, meta.Metadata.Message);
                    }
                }
                return "success";
            }
            else
            {
                DeleteSemuaRuangan();
                InsertRuangan();

                //foreach (DataRow row in new ServiceUnitCollection().InpatientBedAvailability().Rows)
                //{
                //    var service = new Common.BPJS.Applicare.Service();
                //    var response = service.DeleteRuangan(new Temiang.Avicenna.Common.BPJS.Applicare.HapusRuangan.RootObject()
                //    {
                //        kodekelas = row["BpjsClassID"].ToString(),
                //        koderuang = row["RoomID"].ToString()
                //    });
                //    var meta = fastJSON.JSON.ToObject<Common.BPJS.MetadataResponse>(response);
                //    //if (meta.Metadata.Code != 1) return String.Format("Server message (HTTP {0}: {1}).", meta.Metadata.Code, meta.Metadata.Message);
                //    if (meta.Metadata.Code == "0")
                //    {
                //        var str = InsertSatuRuangan(row["BpjsClassID"].ToString(), row["RoomID"].ToString(), row["ServiceUnitName"].ToString(), row["Capacity"].ToString(), row["Available"].ToString());
                //        //if (str != "success") return String.Format("Server message (HTTP {0}: {1}).", meta.Metadata.Code, meta.Metadata.Message);
                //    }
                //    service = new Common.BPJS.Applicare.Service();
                //    response = service.UpdateRuangan(new Common.BPJS.Applicare.UpdateKetersediaanTempatTidur.RootObject()
                //    {
                //        kodekelas = row["BpjsClassID"].ToString(),
                //        koderuang = row["RoomID"].ToString(),
                //        namaruang = row["ServiceUnitName"].ToString(),
                //        kapasitas = row["Capacity"].ToString(),
                //        tersedia = row["Available"].ToString(),
                //        tersediapria = "0",
                //        tersediawanita = "0",
                //        tersediapriawanita = "0"
                //    });
                //    meta = fastJSON.JSON.ToObject<Common.BPJS.MetadataResponse>(response);
                //    //if (meta.Metadata.Code != 1) return String.Format("Server message (HTTP {0}: {1}).", meta.Metadata.Code, meta.Metadata.Message);
                //    if (meta.Metadata.Code == "0")
                //    {
                //        var str = InsertSatuRuangan(row["BpjsClassID"].ToString(), row["RoomID"].ToString(), row["ServiceUnitName"].ToString(), row["Capacity"].ToString(), row["Available"].ToString());
                //        //if (str != "success") return String.Format("Server message (HTTP {0}: {1}).", meta.Metadata.Code, meta.Metadata.Message);
                //    }
                //}
                return "success";
            }

        }

        [WebMethod]
        public string DeleteRuangan(string classID, string roomID)
        {
            var service = new Common.BPJS.Applicare.Service();
            var response = service.DeleteRuangan(new Common.BPJS.Applicare.HapusRuangan.RootObject()
            {
                kodekelas = classID,
                koderuang = roomID
            });
            var meta = fastJSON.JSON.ToObject<Common.BPJS.MetadataResponse>(response);
            return String.Format("Server message (HTTP {0}: {1}).", meta.Metadata.Code, meta.Metadata.Message);
        }

        [WebMethod]
        public string DeleteSemuaRuangan()
        {
            var list = ReadSemuaRuangan();
            if (list.Metadata.Code != "1") return String.Format("Server message (HTTP {0}: {1}).", list.Metadata.Code, list.Metadata.Message);
            else
            {
                foreach (var entity in list.Response.List)
                {
                    var service = new Common.BPJS.Applicare.Service();
                    var response = service.DeleteRuangan(new Common.BPJS.Applicare.HapusRuangan.RootObject()
                    {
                        kodekelas = entity.Kodekelas,
                        koderuang = entity.Koderuang
                    });
                    var meta = fastJSON.JSON.ToObject<Common.BPJS.MetadataResponse>(response);
                    if (meta.Metadata.Code != "1") return String.Format("Server message (HTTP {0}: {1}).", meta.Metadata.Code, meta.Metadata.Message);
                }
            }
            return "success";
        }

        [WebMethod]
        public string ReadRuangan()
        {
            var service = new Common.BPJS.Applicare.Service();
            var response = service.ReadRuangan(10000);
            var meta = fastJSON.JSON.ToObject<Common.BPJS.Applicare.KetersediaanKamarRS.KetersediaanKamar>(response);
            if (meta.Metadata.Code != "1") return String.Format("Server message (HTTP {0}: {1}).", meta.Metadata.Code, meta.Metadata.Message);
            return JsonConvert.SerializeObject(meta.Response);
        }

        public Common.BPJS.Applicare.KetersediaanKamarRS.KetersediaanKamar ReadSemuaRuangan()
        {
            var service = new Common.BPJS.Applicare.Service();
            var response = service.ReadRuangan(10000);
            var meta = fastJSON.JSON.ToObject<Common.BPJS.Applicare.KetersediaanKamarRS.KetersediaanKamar>(response);
            return meta;
        }

        [WebMethod]
        public string ReadReferensiKelas()
        {
            var service = new Common.BPJS.Applicare.Service();
            var kelas = service.ReadReferensiKelas();
            return Newtonsoft.Json.JsonConvert.SerializeObject(kelas);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BedAvailability()
        {
            var table = new ServiceUnitCollection().InpatientBedAvailabilityNonApplicares();
            var json = new JsonRetWS();
            if (table.Rows.Count > 0) Context.Response.Write(json.JSonRetFormatted(json.ConvertDataTabletoObject(table)));
            else Context.Response.Write(json.JSonRetFormatted("No data is available", false, "100"));
        }

        public XmlElement Serialize(object transformObject)
        {
            XmlElement serializedElement = null;
            try
            {
                var memStream = new MemoryStream();
                var serializer = new XmlSerializer(transformObject.GetType());
                serializer.Serialize(memStream, transformObject);
                memStream.Position = 0;
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(memStream);
                serializedElement = xmlDoc.DocumentElement;
            }
            catch (Exception e)
            {

            }
            return serializedElement;
        }

        [WebMethod]
        public string UpdateDiagnose()
        {
            var diagnoses = new BusinessObject.DiagnoseCollection();
            diagnoses.LoadAll();

            foreach (var diagnose in diagnoses)
            {
                try
                {
                    var svc = new Common.BPJS.VClaim.v11.Service();
                    var response = svc.GetDiagnosa(diagnose.DiagnoseID);
                    if (response.MetaData.IsValid)
                    {
                        var diag = response.Response.Diagnosa.SingleOrDefault(d => d.Kode == diagnose.DiagnoseID);
                        if (diag != null)
                        {
                            diagnose.DiagnoseID = diag.Kode;
                            diagnose.DiagnoseName = diag.Nama.Split('-')[1].Trim();
                        }
                    }
                }
                catch (Exception e)
                {
                    continue;
                }
            }

            diagnoses.Save();

            return "ok";
        }

        [WebMethod]
        public string StatistikRS()
        {
            var table = new DataTable();
            if (AppSession.Parameter.IsAplicaresByRoomName) table = new ServiceUnitCollection().InpatientBedAvailabilityV2();
            else table = new ServiceUnitCollection().InpatientBedAvailability();

            var kapasitas = table.AsEnumerable().Sum(d => d.Field<int>("Capacity"));
            var tersedia = table.AsEnumerable().Sum(d => d.Field<int>("Available"));

            return $"kapasitas : {kapasitas}, tersedia : {tersedia}";
        }

        [WebMethod]
        public string StatistikApplicares()
        {
            var data = ReadSemuaRuangan();

            var kapasitas = data.Response.List.Sum(d => d.Kapasitas);
            var tersedia = data.Response.List.Sum(d => d.Tersedia);

            return $"kapasitas : {kapasitas}, tersedia : {tersedia}";
        }

        [WebMethod(EnableSession = true)]
        public string GenerateSkdp(string appointmentNo)
        {
            var table = string.IsNullOrWhiteSpace(appointmentNo) ? new AppointmentCollection().BpjsOpenAppointment(AppEnum.BridgingType.BPJS.ToString(), AppSession.Parameter.GuarantorAskesID[0], AppSession.Parameter.AppointmentStatusOpen, DateTime.Now.Date) :
                new AppointmentCollection().BpjsOpenAppointment(AppEnum.BridgingType.BPJS.ToString(), AppSession.Parameter.GuarantorAskesID[0], AppSession.Parameter.AppointmentStatusOpen, appointmentNo);
            foreach (DataRow row in table.Rows)
            {
                var svc = new Common.BPJS.VClaim.v11.Service();
                var param = new Common.BPJS.VClaim.v11.RencanaKontrol.Insert.Request.Root()
                {
                    Request = new Common.BPJS.VClaim.v11.RencanaKontrol.Insert.Request.TRequest()
                    {
                        NoSEP = row["NoSEP"].ToString(),
                        KodeDokter = row["pb"].ToString(),
                        PoliKontrol = row["sub"].ToString(),
                        TglRencanaKontrol = Convert.ToDateTime(row["AppointmentDate"]).ToString("yyyy-MM-dd"),
                        User = row["LastCreateByUserID"].ToString()
                    }
                };
                var response = svc.Insert(param);

                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = string.Empty,
                    UrlAddress = "RencanaKontrolService",
                    Params = JsonConvert.SerializeObject(param),
                    Response = JsonConvert.SerializeObject(response),
                    Totalms = 0
                };
                log.Save();

                if (!response.MetaData.IsValid) continue;

                var entity = new BusinessObject.Appointment();
                entity.LoadByPrimaryKey(row["AppointmentNo"].ToString());
                entity.ReferenceNumber = response.Response.NoSuratKontrol;
                entity.LastUpdateByUserID = "WEBSERVICE";
                entity.LastUpdateDateTime = DateTime.Now;
                entity.Save();

                // antrol
                var patient = new Patient();
                patient.LoadByPrimaryKey(entity.PatientID);

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(entity.ServiceUnitID);

                var sub = new ServiceUnitBridging();
                sub.Query.Where(sub.Query.ServiceUnitID == entity.ServiceUnitID &&
                                sub.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                sub.Query.Load();

                var exclude = new[] { "HDL", "IRM" };
                if (!exclude.Contains(sub.BridgingID.Split(';')[0]))
                {
                    var p = new Paramedic();
                    p.LoadByPrimaryKey(entity.ParamedicID);

                    var pb = new ParamedicBridging();
                    pb.Query.Where(pb.Query.ParamedicID == entity.ParamedicID &&
                                   pb.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                    pb.Query.Load();

                    var ps = new ParamedicSchedule();
                    ps.LoadByPrimaryKey(entity.ServiceUnitID, entity.ParamedicID,
                        entity.AppointmentDate.Value.Year.ToString());

                    var psd = new ParamedicScheduleDate();
                    psd.LoadByPrimaryKey(entity.ServiceUnitID, entity.ParamedicID,
                        entity.AppointmentDate.Value.Year.ToString(), entity.AppointmentDate.Value.Date);

                    var ot = new OperationalTime();
                    ot.LoadByPrimaryKey(psd.OperationalTimeID);

                    var jam = TimeSpan.ParseExact(entity.AppointmentTime, "hh\\:mm", null);
                    string waktu = string.Empty;

                    if (!string.IsNullOrWhiteSpace(ot.StartTime1) && !string.IsNullOrWhiteSpace(ot.EndTime1))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime1, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime1, "hh\\:mm", null);

                        if (jam >= ot1 && jam <= ot2)
                        {
                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime2, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime2, "hh\\:mm", null);

                        if (jam >= ot1 && jam <= ot2)
                        {
                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime3, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime3, "hh\\:mm", null);

                        if (jam >= ot1 && jam <= ot2)
                        {
                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime4, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime4, "hh\\:mm", null);

                        if (jam >= ot1 && jam <= ot2)
                        {
                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                    {
                        var ot1 = TimeSpan.ParseExact(ot.StartTime5, "hh\\:mm", null);
                        var ot2 = TimeSpan.ParseExact(ot.EndTime5, "hh\\:mm", null);

                        if (jam >= ot1 && jam <= ot2)
                        {
                            waktu = $"{ot1.ToString("hh\\:mm")}-{ot2.ToString("hh\\:mm")}";
                        }
                    }

                    var antreanDateTime = Convert.ToDateTime(entity.AppointmentDate.Value.ToString("yyyy-MM-dd") + ' ' +
                                                             entity.AppointmentTime + ":00");

                    var jam2 = waktu.Split('-');

                    var appt = new BusinessObject.AppointmentCollection();
                    appt.Query.Where(appt.Query.ServiceUnitID == entity.ServiceUnitID,
                        appt.Query.ParamedicID == entity.ParamedicID,
                        appt.Query.AppointmentDate.Date() == entity.AppointmentDate.Value.Date,
                        appt.Query.AppointmentTime >= jam2[0].Trim(),
                        appt.Query.AppointmentTime <= jam2[1].Trim(),
                        appt.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                    );
                    var apptAvailable = appt.Query.Load();

                    var antrol = new Common.BPJS.Antrian.Tambah.Request.Root()
                    {
                        Kodebooking = entity.AppointmentNo,
                        Jenispasien = AppSession.Parameter.GuarantorAskesID.Contains(entity.GuarantorID)
                            ? "JKN"
                            : "NON JKN",
                        Nomorkartu = patient.GuarantorCardNo,
                        Nik = patient.Ssn,
                        Nohp = string.IsNullOrWhiteSpace(entity.MobilePhoneNo)
                            ? patient.PhoneNo
                            : patient.MobilePhoneNo,
                        Kodepoli = sub.BridgingID.Split(';')[1],
                        Namapoli = su.ServiceUnitName,
                        Pasienbaru = 0,
                        Norm = patient.MedicalNo,
                        Tanggalperiksa = entity.AppointmentDate.Value.Date.ToString("yyyy-MM-dd"),
                        Kodedokter = pb.BridgingID.ToInt(),
                        Namadokter = p.ParamedicName,
                        Jampraktek = waktu,
                        Jeniskunjungan = 3,
                        Nomorreferensi = entity.ReferenceNumber,
                        Nomorantrean = $"{su.ShortName}{p.ParamedicInitial} - {(entity.AppointmentQue ?? 1)}",
                        Angkaantrean = entity.AppointmentQue ?? 1,
                        Estimasidilayani = Convert.ToInt64(antreanDateTime.AddHours(-7)
                            .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds),
                        Sisakuotajkn = (ps.QuotaBpjsOnline ?? 0) -
                                       appt.Count(a => a.GuarantorID == AppSession.Parameter.GuarantorAskesID[0]),
                        Kuotajkn = ps.QuotaBpjsOnline ?? 0,
                        Sisakuotanonjkn = (ps.QuotaOnline ?? 0) -
                                          appt.Count(a => a.GuarantorID != AppSession.Parameter.GuarantorAskesID[0]),
                        Kuotanonjkn = ps.QuotaOnline ?? 0,
                        Keterangan = "Peserta harap 30 menit lebih awal guna pencatatan administrasi"
                    };

                    var svc2 = new Common.BPJS.Antrian.Service();
                    var response2 = svc2.TambahAntrian(antrol);

                    var log2 = new WebServiceAPILog
                    {
                        DateRequest = DateTime.Now,
                        IPAddress = string.Empty,
                        UrlAddress = "RencanaKontrolService",
                        Params = JsonConvert.SerializeObject(antrol),
                        Response = JsonConvert.SerializeObject(response2),
                        Totalms = 0
                    };
                    log2.Save();
                }
            }

            return "success";
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public object GetTokenIcare(string noka, int kodeDokter)
        {
            var svc = new Common.BPJS.Icare.Service();
            var response = svc.GetToken(new Common.BPJS.Icare.Json.Request.Root()
            {
                Param = noka,
                Kodedokter = kodeDokter
            });
            return response;
        }
    }
}
