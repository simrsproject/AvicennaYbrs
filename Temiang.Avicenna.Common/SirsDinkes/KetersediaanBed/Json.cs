using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.SirsDinkes.KetersediaanBed
{
    public class Request
    {
        public class Kapasitas
        {
            [JsonProperty("kapasitas_icu_tekanan_negatif_dengan_ventilator_covid")]
            public int KapasitasIcuTekananNegatifDenganVentilatorCovid;

            [JsonProperty("kapasitas_icu_tekanan_negatif_tanpa_ventilator_covid")]
            public int KapasitasIcuTekananNegatifTanpaVentilatorCovid;

            [JsonProperty("kapasitas_icu_tanpa_tekanan_negatif_dengan_ventilator_covid")]
            public int KapasitasIcuTanpaTekananNegatifDenganVentilatorCovid;

            [JsonProperty("kapasitas_icu_tanpa_tekanan_negatif_tanpa_ventilator_covid")]
            public int KapasitasIcuTanpaTekananNegatifTanpaVentilatorCovid;

            [JsonProperty("kapasitas_isolasi_tekanan_negatif_covid_pria")]
            public int KapasitasIsolasiTekananNegatifCovidPria;

            [JsonProperty("kapasitas_isolasi_tekanan_negatif_covid_wanita")]
            public int KapasitasIsolasiTekananNegatifCovidWanita;

            [JsonProperty("kapasitas_isolasi_tekanan_negatif_covid_anak")]
            public int KapasitasIsolasiTekananNegatifCovidAnak;

            [JsonProperty("kapasitas_isolasi_tanpa_tekanan_negatif_covid_pria")]
            public int KapasitasIsolasiTanpaTekananNegatifCovidPria;

            [JsonProperty("kapasitas_isolasi_tanpa_tekanan_negatif_covid_wanita")]
            public int KapasitasIsolasiTanpaTekananNegatifCovidWanita;

            [JsonProperty("kapasitas_isolasi_tanpa_tekanan_negatif_covid_anak")]
            public int KapasitasIsolasiTanpaTekananNegatifCovidAnak;

            [JsonProperty("kapasitas_nicu_covid")]
            public int KapasitasNicuCovid;

            [JsonProperty("kapasitas_picu_covid")]
            public int KapasitasPicuCovid;

            [JsonProperty("kapasitas_perina_covid")]
            public int KapasitasPerinaCovid;

            [JsonProperty("kapasitas_ok_covid")]
            public int KapasitasOkCovid;

            [JsonProperty("kapasitas_hd_covid")]
            public int KapasitasHdCovid;

            [JsonProperty("kapasitas_igd_covid")]
            public int KapasitasIgdCovid;

            [JsonProperty("kapasitas_vip_non_covid")]
            public int KapasitasVipNonCovid;

            [JsonProperty("kapasitas_kelas_1_non_covid_pria")]
            public int KapasitasKelas1NonCovidPria;

            [JsonProperty("kapasitas_kelas_1_non_covid_wanita")]
            public int KapasitasKelas1NonCovidWanita;

            [JsonProperty("kapasitas_kelas_1_non_covid_anak")]
            public int KapasitasKelas1NonCovidAnak;

            [JsonProperty("kapasitas_kelas_2_non_covid_pria")]
            public int KapasitasKelas2NonCovidPria;

            [JsonProperty("kapasitas_kelas_2_non_covid_wanita")]
            public int KapasitasKelas2NonCovidWanita;

            [JsonProperty("kapasitas_kelas_2_non_covid_anak")]
            public int KapasitasKelas2NonCovidAnak;

            [JsonProperty("kapasitas_kelas_3_non_covid_pria")]
            public int KapasitasKelas3NonCovidPria;

            [JsonProperty("kapasitas_kelas_3_non_covid_wanita")]
            public int KapasitasKelas3NonCovidWanita;

            [JsonProperty("kapasitas_kelas_3_non_covid_anak")]
            public int KapasitasKelas3NonCovidAnak;

            [JsonProperty("kapasitas_hcu_non_covid")]
            public int KapasitasHcuNonCovid;

            [JsonProperty("kapasitas_iccu_non_covid")]
            public int KapasitasIccuNonCovid;

            [JsonProperty("kapasitas_icu_non_covid")]
            public int KapasitasIcuNonCovid;

            [JsonProperty("kapasitas_nicu_non_covid")]
            public int KapasitasNicuNonCovid;

            [JsonProperty("kapasitas_picu_non_covid")]
            public int KapasitasPicuNonCovid;

            [JsonProperty("kapasitas_perina_non_covid")]
            public int KapasitasPerinaNonCovid;

            [JsonProperty("kapasitas_ok_non_covid")]
            public int KapasitasOkNonCovid;

            [JsonProperty("kapasitas_hd_non_covid")]
            public int KapasitasHdNonCovid;

            [JsonProperty("kapasitas_isolasi_non_covid")]
            public int KapasitasIsolasiNonCovid;

            [JsonProperty("kapasitas_igd_non_covid")]
            public int KapasitasIgdNonCovid;
        }

        public class Kosong
        {
            [JsonProperty("kosong_icu_tekanan_negatif_dengan_ventilator_covid")]
            public int KosongIcuTekananNegatifDenganVentilatorCovid;

            [JsonProperty("kosong_icu_tekanan_negatif_tanpa_ventilator_covid")]
            public int KosongIcuTekananNegatifTanpaVentilatorCovid;

            [JsonProperty("kosong_icu_tanpa_tekanan_negatif_dengan_ventilator_covid")]
            public int KosongIcuTanpaTekananNegatifDenganVentilatorCovid;

            [JsonProperty("kosong_icu_tanpa_tekanan_negatif_tanpa_ventilator_covid")]
            public int KosongIcuTanpaTekananNegatifTanpaVentilatorCovid;

            [JsonProperty("kosong_isolasi_tekanan_negatif_covid_pria")]
            public int KosongIsolasiTekananNegatifCovidPria;

            [JsonProperty("kosong_isolasi_tekanan_negatif_covid_wanita")]
            public int KosongIsolasiTekananNegatifCovidWanita;

            [JsonProperty("kosong_isolasi_tekanan_negatif_covid_anak")]
            public int KosongIsolasiTekananNegatifCovidAnak;

            [JsonProperty("kosong_isolasi_tanpa_tekanan_negatif_covid_pria")]
            public int KosongIsolasiTanpaTekananNegatifCovidPria;

            [JsonProperty("kosong_isolasi_tanpa_tekanan_negatif_covid_wanita")]
            public int KosongIsolasiTanpaTekananNegatifCovidWanita;

            [JsonProperty("kosong_isolasi_tanpa_tekanan_negatif_covid_anak")]
            public int KosongIsolasiTanpaTekananNegatifCovidAnak;

            [JsonProperty("kosong_nicu_covid")]
            public int KosongNicuCovid;

            [JsonProperty("kosong_picu_covid")]
            public int KosongPicuCovid;

            [JsonProperty("kosong_perina_covid")]
            public int KosongPerinaCovid;

            [JsonProperty("kosong_ok_covid")]
            public int KosongOkCovid;

            [JsonProperty("kosong_hd_covid")]
            public int KosongHdCovid;

            [JsonProperty("kosong_igd_covid")]
            public int KosongIgdCovid;

            [JsonProperty("kosong_vip_non_covid")]
            public int KosongVipNonCovid;

            [JsonProperty("kosong_kelas_1_non_covid_pria")]
            public int KosongKelas1NonCovidPria;

            [JsonProperty("kosong_kelas_1_non_covid_wanita")]
            public int KosongKelas1NonCovidWanita;

            [JsonProperty("kosong_kelas_1_non_covid_anak")]
            public int KosongKelas1NonCovidAnak;

            [JsonProperty("kosong_kelas_2_non_covid_pria")]
            public int KosongKelas2NonCovidPria;

            [JsonProperty("kosong_kelas_2_non_covid_wanita")]
            public int KosongKelas2NonCovidWanita;

            [JsonProperty("kosong_kelas_2_non_covid_anak")]
            public int KosongKelas2NonCovidAnak;

            [JsonProperty("kosong_kelas_3_non_covid_pria")]
            public int KosongKelas3NonCovidPria;

            [JsonProperty("kosong_kelas_3_non_covid_wanita")]
            public int KosongKelas3NonCovidWanita;

            [JsonProperty("kosong_kelas_3_non_covid_anak")]
            public int KosongKelas3NonCovidAnak;

            [JsonProperty("kosong_hcu_non_covid")]
            public int KosongHcuNonCovid;

            [JsonProperty("kosong_iccu_non_covid")]
            public int KosongIccuNonCovid;

            [JsonProperty("kosong_icu_non_covid")]
            public int KosongIcuNonCovid;

            [JsonProperty("kosong_nicu_non_covid")]
            public int KosongNicuNonCovid;

            [JsonProperty("kosong_picu_non_covid")]
            public int KosongPicuNonCovid;

            [JsonProperty("kosong_perina_non_covid")]
            public int KosongPerinaNonCovid;

            [JsonProperty("kosong_ok_non_covid")]
            public int KosongOkNonCovid;

            [JsonProperty("kosong_hd_non_covid")]
            public int KosongHdNonCovid;

            [JsonProperty("kosong_isolasi_non_covid")]
            public int KosongIsolasiNonCovid;

            [JsonProperty("kosong_igd_non_covid")]
            public int KosongIgdNonCovid;
        }

        public class Covid
        {
            [JsonProperty("kapasitas")]
            public Kapasitas Kapasitas;

            [JsonProperty("kosong")]
            public Kosong Kosong;
        }

        public class NonCovid
        {
            [JsonProperty("kapasitas")]
            public Kapasitas Kapasitas;

            [JsonProperty("kosong")]
            public Kosong Kosong;
        }

        public class Root
        {
            [JsonProperty("covid")]
            public Covid Covid;

            [JsonProperty("non_covid")]
            public NonCovid NonCovid;
        }

    }

    public class Response
    {
        public class Root
        {
            [JsonProperty("code")]
            public int Code;

            [JsonProperty("messages")]
            public string Messages;
        }
    }
}
