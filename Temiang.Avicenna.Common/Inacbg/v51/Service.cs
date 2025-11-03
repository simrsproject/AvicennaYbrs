using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.Inacbg.v51
{
    public class Service
    {
        public Claim.Response.Result Insert(Inacbg.v51.Claim.Create.Data tsep)
        {
            var param = string.Concat(new string[]
                {
                    "nomor_kartu=",tsep.nomor_kartu,
                    "&nomor_sep=",tsep.nomor_sep,
                    "&nomor_rm=",tsep.nomor_rm,
                    "&nama_pasien=",tsep.nama_pasien,
                    "&tgl_lahir=",tsep.tgl_lahir,
                    "&gender=",tsep.gender
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Inacbg.Helper.PopulateWebRequest("new_claim", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Claim.Response.Result>(sr.ReadToEnd());
            }
        }

        public Claim.Response.Result Update(Inacbg.v51.Patient.Update.Data tsep)
        {
            var param = string.Concat(new string[]
                {
                    "nomor_kartu=",tsep.nomor_kartu,
                    "&nomor_rm=",tsep.nomor_rm,
                    "&nama_pasien=",tsep.nama_pasien,
                    "&tgl_lahir=",tsep.tgl_lahir,
                    "&gender=",tsep.gender
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Inacbg.Helper.PopulateWebRequest("update_patient", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Claim.Response.Result>(sr.ReadToEnd());
            }
        }

        public Claim.Response.Result Delete(Inacbg.v51.Patient.Delete.Data tsep)
        {
            var param = string.Concat(new string[]
                {
                    "&nomor_rm=",tsep.nomor_rm,
                    "&coder_nik=",string.IsNullOrEmpty(tsep.coder_nik) ? Helper.InacbgUserID : tsep.coder_nik
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Inacbg.Helper.PopulateWebRequest("delete_patient", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Claim.Response.Result>(sr.ReadToEnd());
            }
        }

        public Detail.Response.Result Insert(Inacbg.v51.Detail.Data tsep)
        {
            var param = string.Concat(new string[]
                {
                    "nomor_sep=",tsep.nomor_sep,
                    "&nomor_kartu=",tsep.nomor_kartu,
                    "&tgl_masuk=",tsep.tgl_masuk,
                    "&tgl_pulang=",tsep.tgl_pulang,
                    "&jenis_rawat=",tsep.jenis_rawat,
                    "&kelas_rawat=",tsep.kelas_rawat,
                    "&adl_sub_acute=",tsep.adl_sub_acute,
                    "&adl_chronic=",tsep.adl_chronic,
                    "&icu_indikator=",tsep.icu_indikator,
                    "&icu_los=",tsep.icu_los,
                    "&ventilator_hour=",tsep.ventilator_hour,
                    "&upgrade_class_ind=",tsep.upgrade_class_ind,
                    "&upgrade_class_class=",tsep.upgrade_class_class,
                    "&upgrade_class_los=",tsep.upgrade_class_los,
                    "&add_payment_pct=",tsep.add_payment_pct,
                    "&birth_weight=",tsep.birth_weight,
                    "&discharge_status=",tsep.discharge_status,
                    "&diagnosa=",tsep.diagnosa,
                    "&procedure=",tsep.procedure,
                    "&prosedur_non_bedah=",tsep.tarif_rs.prosedur_non_bedah,
                    "&prosedur_bedah=",tsep.tarif_rs.prosedur_bedah,
                    "&konsultasi=",tsep.tarif_rs.konsultasi,
                    "&tenaga_ahli=",tsep.tarif_rs.tenaga_ahli,
                    "&keperawatan=",tsep.tarif_rs.keperawatan,
                    "&penunjang=",tsep.tarif_rs.penunjang,
                    "&radiologi=",tsep.tarif_rs.radiologi,
                    "&laboratorium=",tsep.tarif_rs.laboratorium,
                    "&pelayanan_darah=",tsep.tarif_rs.pelayanan_darah,
                    "&rehabilitasi=",tsep.tarif_rs.rehabilitasi,
                    "&kamar=",tsep.tarif_rs.kamar,
                    "&rawat_intensif=",tsep.tarif_rs.rawat_intensif,
                    "&obat=",tsep.tarif_rs.obat,
                    "&alkes=",tsep.tarif_rs.alkes,
                    "&bmhp=",tsep.tarif_rs.bmhp,
                    "&sewa_alat=",tsep.tarif_rs.sewa_alat,
                    "&tarif_poli_eks=",tsep.tarif_poli_eks,
                    "&nama_dokter=",tsep.nama_dokter,
                    "&kode_tarif=",tsep.kode_tarif,
                    "&payor_id=",tsep.payor_id,
                    "&payor_cd=",tsep.payor_cd,
                    "&cob_cd=",tsep.cob_cd,
                    "&coder_nik=",string.IsNullOrEmpty(tsep.coder_nik) ? Helper.InacbgUserID : tsep.coder_nik
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Inacbg.Helper.PopulateWebRequest("set_claim_data", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Detail.Response.Result>(sr.ReadToEnd());
            }
        }

        public Detail.Response.Result UpdateDiagnose(Inacbg.v51.Detail.Data tsep)
        {
            var param = string.Concat(new string[]
                {
                    "nomor_sep=",tsep.nomor_sep,
                    "&payor_id=",tsep.payor_id,
                    "&diagnosa=",tsep.diagnosa,
                    "&coder_nik=",string.IsNullOrEmpty(tsep.coder_nik) ? Helper.InacbgUserID : tsep.coder_nik
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Inacbg.Helper.PopulateWebRequest("set_diagnose_data", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Detail.Response.Result>(sr.ReadToEnd());
            }
        }

        public Detail.Response.Result UpdateProcedure(Inacbg.v51.Detail.Data tsep)
        {
            var param = string.Concat(new string[]
                {
                    "nomor_sep=",tsep.nomor_sep,
                    "&payor_id=",tsep.payor_id,
                    "&procedure=",tsep.procedure,
                    "&coder_nik=",string.IsNullOrEmpty(tsep.coder_nik) ? Helper.InacbgUserID : tsep.coder_nik
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Inacbg.Helper.PopulateWebRequest("set_procedure_data", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Detail.Response.Result>(sr.ReadToEnd());
            }
        }

        public Grouper.Grouper1.Result.Data Grouper1(Grouper.Grouper1.Data tsep)
        {
            var param = string.Concat(new string[]
                {
                    "nomor_sep=",tsep.nomor_sep
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Inacbg.Helper.PopulateWebRequest("grouper1", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Grouper.Grouper1.Result.Data>(sr.ReadToEnd());
            }
        }

        public Grouper.Grouper2.Result.Data Grouper2(Grouper.Grouper2.Data tsep)
        {
            var param = string.Concat(new string[]
                {
                    "nomor_sep=",tsep.nomor_sep,
                    "&special_cmg=",tsep.special_cmg
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Inacbg.Helper.PopulateWebRequest("grouper2", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Grouper.Grouper2.Result.Data>(sr.ReadToEnd());
            }
        }

        public Claim.Get.GetDetailResponse.Response GetDetail(Claim.Get.GetDetail.Data tsep)
        {
            var param = string.Concat(new string[]
                {
                    "nomor_sep=",tsep.nomor_sep
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Inacbg.Helper.PopulateWebRequest("get_claim_data", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Claim.Get.GetDetailResponse.Response>(sr.ReadToEnd());
            }
        }

        public Claim.Final.Response.Result Final(Inacbg.v51.Claim.Final.Data tsep)
        {
            var param = string.Concat(new string[]
                {
                    "nomor_sep=",tsep.nomor_sep,
                    "&coder_nik=",string.IsNullOrEmpty(tsep.coder_nik) ? Helper.InacbgUserID : tsep.coder_nik
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Inacbg.Helper.PopulateWebRequest("claim_final", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Claim.Final.Response.Result>(sr.ReadToEnd());
            }
        }

        public Claim.Edit.Response.Result Edit(Inacbg.v51.Claim.Edit.Data tsep)
        {
            var param = string.Concat(new string[]
                {
                    "nomor_sep=",tsep.nomor_sep
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Inacbg.Helper.PopulateWebRequest("reedit_claim", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Claim.Edit.Response.Result>(sr.ReadToEnd());
            }
        }

        public Claim.Send.Individual.Result Send(Inacbg.v51.Claim.Create.Data tsep)
        {
            var param = string.Concat(new string[]
                {
                    "nomor_sep=",tsep.nomor_sep
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Inacbg.Helper.PopulateWebRequest("send_claim_individual", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                return JsonConvert.DeserializeObject<Claim.Send.Individual.Result>(sr.ReadToEnd());
            }
        }

        public Claim.Print.Response Print(Inacbg.v51.Claim.Create.Data tsep)
        {
            var param = string.Concat(new string[]
                {
                    "nomor_sep=",tsep.nomor_sep
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Inacbg.Helper.PopulateWebRequest("claim_print", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                return JsonConvert.DeserializeObject<Claim.Print.Response>(sr.ReadToEnd());
            }
        }

        public Procedure.Result.Data Search(Inacbg.v51.Procedure.Search.Data tsep, bool isDiagnosis)
        {
            var param = string.Concat(new string[]
                {
                    "keyword=",tsep.keyword
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Inacbg.Helper.PopulateWebRequest(isDiagnosis ? "search_diagnosis" : "search_procedures", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                var str = sr.ReadToEnd();
                var data = JsonConvert.DeserializeObject<Procedure.Result.Data2>(str);
                if (data.Response.Count == 0)
                {
                    return new Procedure.Result.Data()
                    {
                        Metadata = new Procedure.Result.Metadata()
                        {
                            Code = "201",
                            Message = "Data tidak ditemukan"
                        },
                        Response = new Procedure.Result.Response()
                        {
                            Data = new string[][] { }
                        }
                    };
                }
                else return JsonConvert.DeserializeObject<Procedure.Result.Data>(str);
            }
        }
    }
}
