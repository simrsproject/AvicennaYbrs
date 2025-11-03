using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.Inacbg.v58
{
    public class Service
    {
        public v58.Detail.Response.Result Insert(Inacbg.v58.Detail.Data tsep)
        {
            var param = string.Concat(new string[]
                {
                    "nomor_sep=",tsep.nomor_sep,
                    "&nomor_kartu=",tsep.nomor_kartu,
                    "&tgl_masuk=",tsep.tgl_masuk,
                    "&tgl_pulang=",tsep.tgl_pulang,
                    "&cara_masuk=",tsep.cara_masuk,
                    "&jenis_rawat=",tsep.jenis_rawat,
                    "&kelas_rawat=",tsep.kelas_rawat,
                    "&adl_sub_acute=",tsep.adl_sub_acute,
                    "&adl_chronic=",tsep.adl_chronic,
                    "&icu_indikator=",tsep.icu_indikator,
                    "&icu_los=",tsep.icu_los,
                    "&ventilator_hour=",tsep.ventilator_hour,
                    "&use_ind=",tsep.use_ind,
                    "&start_dttm=",tsep.start_dttm,
                    "&stop_dttm=",tsep.stop_dttm,
                    "&upgrade_class_ind=",tsep.upgrade_class_ind,
                    "&upgrade_class_class=",tsep.upgrade_class_class,
                    "&upgrade_class_los=",tsep.upgrade_class_los,
                    "&upgrade_class_payor=",tsep.upgrade_class_payor,
                    "&add_payment_pct=",tsep.add_payment_pct,
                    "&birth_weight=",tsep.birth_weight,
                    "&sistole=",tsep.sistole,
                    "&diastole=",tsep.diastole,
                    "&discharge_status=",tsep.discharge_status,
                    "&diagnosa=",tsep.diagnosa,
                    "&procedure=",tsep.procedure,
                    "&diagnosa_inagrouper=",tsep.diagnosa_inagrouper,
                    "&procedure_inagrouper=",tsep.procedure_inagrouper,

                    //tarif rs
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
                    "&obat_kronis=",tsep.tarif_rs.obat_kronis,
                    "&obat_kemoterapi=",tsep.tarif_rs.obat_kemoterapi,
                    "&alkes=",tsep.tarif_rs.alkes,
                    "&bmhp=",tsep.tarif_rs.bmhp,
                    "&sewa_alat=",tsep.tarif_rs.sewa_alat,

                    //v54 -- start
                    "&pemulasaraan_jenazah=",tsep.pemulasaraan_jenazah,
                    "&kantong_jenazah=",tsep.kantong_jenazah,
                    "&peti_jenazah=",tsep.peti_jenazah,
                    "&plastik_erat=",tsep.plastik_erat,
                    "&desinfektan_jenazah=",tsep.desinfektan_jenazah,
                    "&mobil_jenazah=",tsep.mobil_jenazah,
                    "&desinfektan_mobil_jenazah=",tsep.desinfektan_mobil_jenazah,
                    "&covid19_status_cd=",tsep.covid19_status_cd,
                    "&nomor_kartu_t=",tsep.nomor_kartu_t,
                    "&episodes=",tsep.episodes,
                    "&covid19_cc_ind=",tsep.covid19_cc_ind,
                    "&covid19_rs_darurat_ind=",tsep.covid19_rs_darurat_ind,
                    "&covid19_co_insidense_ind=",tsep.covid19_co_insidense_ind,

                    "&lab_asam_laktat=",tsep.covid19_penunjang_pengurang.lab_asam_laktat,
                    "&lab_procalcitonin=",tsep.covid19_penunjang_pengurang.lab_procalcitonin,
                    "&lab_crp=",tsep.covid19_penunjang_pengurang.lab_crp,
                    "&lab_kultur=",tsep.covid19_penunjang_pengurang.lab_kultur,
                    "&lab_d_dimer=",tsep.covid19_penunjang_pengurang.lab_d_dimer,
                    "&lab_pt=",tsep.covid19_penunjang_pengurang.lab_pt,
                    "&lab_aptt=",tsep.covid19_penunjang_pengurang.lab_aptt,
                    "&lab_waktu_pendarahan=",tsep.covid19_penunjang_pengurang.lab_waktu_pendarahan,
                    "&lab_anti_hiv=",tsep.covid19_penunjang_pengurang.lab_anti_hiv,
                    "&lab_analisa_gas=",tsep.covid19_penunjang_pengurang.lab_analisa_gas,
                    "&lab_albumin=",tsep.covid19_penunjang_pengurang.lab_albumin,
                    "&rad_thorax_ap_pa=",tsep.covid19_penunjang_pengurang.rad_thorax_ap_pa,
                    //v54 --end

                    "&terapi_konvalesen=",tsep.terapi_konvalesen,
                    "&akses_naat=",tsep.akses_naat,
                    "&isoman_ind=",tsep.isoman_ind,
                    "&bayi_lahir_status_cd=",tsep.bayi_lahir_status_cd,

                    "&dializer_single_use=",tsep.dializer_single_use,
                    "&kantong_darah=",tsep.kantong_darah,
                    "&menit_1_appearance=",tsep.menit_1_appearance,
                    "&menit_1_pulse=",tsep.menit_1_pulse,
                    "&menit_1_grimace=",tsep.menit_1_grimace,
                    "&menit_1_activity=",tsep.menit_1_activity,
                    "&menit_1_respiration=",tsep.menit_1_respiration,
                    "&menit_5_appearance=",tsep.menit_5_appearance,
                    "&menit_5_pulse=",tsep.menit_5_pulse,
                    "&menit_5_grimace=",tsep.menit_5_grimace,
                    "&menit_5_activity=",tsep.menit_5_activity,
                    "&menit_5_respiration=",tsep.menit_5_respiration,
                    "&usia_kehamilan=",tsep.usia_kehamilan,
                    "&gravida=",tsep.gravida,
                    "&partus=",tsep.partus,
                    "&abortus=",tsep.abortus,
                    "&onset_kontraksi=",tsep.onset_kontraksi,
                    "&delivery=",string.IsNullOrWhiteSpace(tsep.delivery) ? "[]" : tsep.delivery,

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

            using (HttpWebResponse response = Helper.PopulateWebRequest("set_claim_data", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<v58.Detail.Response.Result>(sr.ReadToEnd());
            }
        }

        public v58.Claim.Get.GetDetailResponse.Root GetDetail(v51.Claim.Get.GetDetail.Data tsep)
        {
            var param = string.Concat(new string[]
                {
                    "nomor_sep=",tsep.nomor_sep
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Helper.PopulateWebRequest("get_claim_data", Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<v58.Claim.Get.GetDetailResponse.Root>(sr.ReadToEnd());
            }
        }
    }
}
