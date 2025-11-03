using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.SirsDinkes.Eis.Json
{
    public class KetersediaanBed
    {
        public class Post
        {
            public class Request
            {
                public class Kapasitas
                {
                    [JsonProperty("kapasitas_icu_tekanan_negatif_dengan_ventilator_covid")]
                    public string KapasitasIcuTekananNegatifDenganVentilatorCovid;

                    [JsonProperty("kapasitas_icu_tekanan_negatif_tanpa_ventilator_covid")]
                    public string KapasitasIcuTekananNegatifTanpaVentilatorCovid;

                    [JsonProperty("kapasitas_icu_tanpa_tekanan_negatif_dengan_ventilator_covid")]
                    public string KapasitasIcuTanpaTekananNegatifDenganVentilatorCovid;

                    [JsonProperty("kapasitas_icu_tanpa_tekanan_negatif_tanpa_ventilator_covid")]
                    public string KapasitasIcuTanpaTekananNegatifTanpaVentilatorCovid;

                    [JsonProperty("kapasitas_isolasi_tekanan_negatif_covid_pria")]
                    public string KapasitasIsolasiTekananNegatifCovidPria;

                    [JsonProperty("kapasitas_isolasi_tekanan_negatif_covid_wanita")]
                    public string KapasitasIsolasiTekananNegatifCovidWanita;

                    [JsonProperty("kapasitas_isolasi_tekanan_negatif_covid_anak")]
                    public string KapasitasIsolasiTekananNegatifCovidAnak;

                    [JsonProperty("kapasitas_isolasi_tanpa_tekanan_negatif_covid_pria")]
                    public string KapasitasIsolasiTanpaTekananNegatifCovidPria;

                    [JsonProperty("kapasitas_isolasi_tanpa_tekanan_negatif_covid_wanita")]
                    public string KapasitasIsolasiTanpaTekananNegatifCovidWanita;

                    [JsonProperty("kapasitas_isolasi_tanpa_tekanan_negatif_covid_anak")]
                    public string KapasitasIsolasiTanpaTekananNegatifCovidAnak;

                    [JsonProperty("kapasitas_nicu_covid")]
                    public string KapasitasNicuCovid;

                    [JsonProperty("kapasitas_picu_covid")]
                    public string KapasitasPicuCovid;

                    [JsonProperty("kapasitas_perina_covid")]
                    public string KapasitasPerinaCovid;

                    [JsonProperty("kapasitas_ok_covid")]
                    public string KapasitasOkCovid;

                    [JsonProperty("kapasitas_hd_covid")]
                    public string KapasitasHdCovid;

                    [JsonProperty("kapasitas_igd_covid")]
                    public string KapasitasIgdCovid;

                    [JsonProperty("kapasitas_vip_non_covid")]
                    public string KapasitasVipNonCovid;

                    [JsonProperty("kapasitas_kelas_1_non_covid_pria")]
                    public string KapasitasKelas1NonCovidPria;

                    [JsonProperty("kapasitas_kelas_1_non_covid_wanita")]
                    public string KapasitasKelas1NonCovidWanita;

                    [JsonProperty("kapasitas_kelas_1_non_covid_anak")]
                    public string KapasitasKelas1NonCovidAnak;

                    [JsonProperty("kapasitas_kelas_2_non_covid_pria")]
                    public string KapasitasKelas2NonCovidPria;

                    [JsonProperty("kapasitas_kelas_2_non_covid_wanita")]
                    public string KapasitasKelas2NonCovidWanita;

                    [JsonProperty("kapasitas_kelas_2_non_covid_anak")]
                    public string KapasitasKelas2NonCovidAnak;

                    [JsonProperty("kapasitas_kelas_3_non_covid_pria")]
                    public string KapasitasKelas3NonCovidPria;

                    [JsonProperty("kapasitas_kelas_3_non_covid_wanita")]
                    public string KapasitasKelas3NonCovidWanita;

                    [JsonProperty("kapasitas_kelas_3_non_covid_anak")]
                    public string KapasitasKelas3NonCovidAnak;

                    [JsonProperty("kapasitas_hcu_non_covid")]
                    public string KapasitasHcuNonCovid;

                    [JsonProperty("kapasitas_iccu_non_covid")]
                    public string KapasitasIccuNonCovid;

                    [JsonProperty("kapasitas_icu_non_covid")]
                    public string KapasitasIcuNonCovid;

                    [JsonProperty("kapasitas_nicu_non_covid")]
                    public string KapasitasNicuNonCovid;

                    [JsonProperty("kapasitas_picu_non_covid")]
                    public string KapasitasPicuNonCovid;

                    [JsonProperty("kapasitas_perina_non_covid")]
                    public string KapasitasPerinaNonCovid;

                    [JsonProperty("kapasitas_ok_non_covid")]
                    public string KapasitasOkNonCovid;

                    [JsonProperty("kapasitas_hd_non_covid")]
                    public string KapasitasHdNonCovid;

                    [JsonProperty("kapasitas_isolasi_non_covid")]
                    public string KapasitasIsolasiNonCovid;

                    [JsonProperty("kapasitas_igd_non_covid")]
                    public string KapasitasIgdNonCovid;
                }

                public class Kosong
                {
                    [JsonProperty("kosong_icu_tekanan_negatif_dengan_ventilator_covid")]
                    public string KosongIcuTekananNegatifDenganVentilatorCovid;

                    [JsonProperty("kosong_icu_tekanan_negatif_tanpa_ventilator_covid")]
                    public string KosongIcuTekananNegatifTanpaVentilatorCovid;

                    [JsonProperty("kosong_icu_tanpa_tekanan_negatif_dengan_ventilator_covid")]
                    public string KosongIcuTanpaTekananNegatifDenganVentilatorCovid;

                    [JsonProperty("kosong_icu_tanpa_tekanan_negatif_tanpa_ventilator_covid")]
                    public string KosongIcuTanpaTekananNegatifTanpaVentilatorCovid;

                    [JsonProperty("kosong_isolasi_tekanan_negatif_covid_pria")]
                    public string KosongIsolasiTekananNegatifCovidPria;

                    [JsonProperty("kosong_isolasi_tekanan_negatif_covid_wanita")]
                    public string KosongIsolasiTekananNegatifCovidWanita;

                    [JsonProperty("kosong_isolasi_tekanan_negatif_covid_anak")]
                    public string KosongIsolasiTekananNegatifCovidAnak;

                    [JsonProperty("kosong_isolasi_tanpa_tekanan_negatif_covid_pria")]
                    public string KosongIsolasiTanpaTekananNegatifCovidPria;

                    [JsonProperty("kosong_isolasi_tanpa_tekanan_negatif_covid_wanita")]
                    public string KosongIsolasiTanpaTekananNegatifCovidWanita;

                    [JsonProperty("kosong_isolasi_tanpa_tekanan_negatif_covid_anak")]
                    public string KosongIsolasiTanpaTekananNegatifCovidAnak;

                    [JsonProperty("kosong_nicu_covid")]
                    public string KosongNicuCovid;

                    [JsonProperty("kosong_picu_covid")]
                    public string KosongPicuCovid;

                    [JsonProperty("kosong_perina_covid")]
                    public string KosongPerinaCovid;

                    [JsonProperty("kosong_ok_covid")]
                    public string KosongOkCovid;

                    [JsonProperty("kosong_hd_covid")]
                    public string KosongHdCovid;

                    [JsonProperty("kosong_igd_covid")]
                    public string KosongIgdCovid;

                    [JsonProperty("kosong_vip_non_covid")]
                    public string KosongVipNonCovid;

                    [JsonProperty("kosong_kelas_1_non_covid_pria")]
                    public string KosongKelas1NonCovidPria;

                    [JsonProperty("kosong_kelas_1_non_covid_wanita")]
                    public string KosongKelas1NonCovidWanita;

                    [JsonProperty("kosong_kelas_1_non_covid_anak")]
                    public string KosongKelas1NonCovidAnak;

                    [JsonProperty("kosong_kelas_2_non_covid_pria")]
                    public string KosongKelas2NonCovidPria;

                    [JsonProperty("kosong_kelas_2_non_covid_wanita")]
                    public string KosongKelas2NonCovidWanita;

                    [JsonProperty("kosong_kelas_2_non_covid_anak")]
                    public string KosongKelas2NonCovidAnak;

                    [JsonProperty("kosong_kelas_3_non_covid_pria")]
                    public string KosongKelas3NonCovidPria;

                    [JsonProperty("kosong_kelas_3_non_covid_wanita")]
                    public string KosongKelas3NonCovidWanita;

                    [JsonProperty("kosong_kelas_3_non_covid_anak")]
                    public string KosongKelas3NonCovidAnak;

                    [JsonProperty("kosong_hcu_non_covid")]
                    public string KosongHcuNonCovid;

                    [JsonProperty("kosong_iccu_non_covid")]
                    public string KosongIccuNonCovid;

                    [JsonProperty("kosong_icu_non_covid")]
                    public string KosongIcuNonCovid;

                    [JsonProperty("kosong_nicu_non_covid")]
                    public string KosongNicuNonCovid;

                    [JsonProperty("kosong_picu_non_covid")]
                    public string KosongPicuNonCovid;

                    [JsonProperty("kosong_perina_non_covid")]
                    public string KosongPerinaNonCovid;

                    [JsonProperty("kosong_ok_non_covid")]
                    public string KosongOkNonCovid;

                    [JsonProperty("kosong_hd_non_covid")]
                    public string KosongHdNonCovid;

                    [JsonProperty("kosong_isolasi_non_covid")]
                    public string KosongIsolasiNonCovid;

                    [JsonProperty("kosong_igd_non_covid")]
                    public string KosongIgdNonCovid;
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

        public class Get
        {
            public class Response
            {
                public class Kapasitas
                {
                    [JsonProperty("kapasitas_icu_tekanan_negatif_dengan_ventilator_covid")]
                    public string KapasitasIcuTekananNegatifDenganVentilatorCovid;

                    [JsonProperty("kapasitas_icu_tekanan_negatif_tanpa_ventilator_covid")]
                    public string KapasitasIcuTekananNegatifTanpaVentilatorCovid;

                    [JsonProperty("kapasitas_icu_tanpa_tekanan_negatif_dengan_ventilator_covid")]
                    public string KapasitasIcuTanpaTekananNegatifDenganVentilatorCovid;

                    [JsonProperty("kapasitas_icu_tanpa_tekanan_negatif_tanpa_ventilator_covid")]
                    public string KapasitasIcuTanpaTekananNegatifTanpaVentilatorCovid;

                    [JsonProperty("kapasitas_isolasi_tekanan_negatif_covid_pria")]
                    public string KapasitasIsolasiTekananNegatifCovidPria;

                    [JsonProperty("kapasitas_isolasi_tekanan_negatif_covid_wanita")]
                    public string KapasitasIsolasiTekananNegatifCovidWanita;

                    [JsonProperty("kapasitas_isolasi_tekanan_negatif_covid_anak")]
                    public string KapasitasIsolasiTekananNegatifCovidAnak;

                    [JsonProperty("kapasitas_isolasi_tanpa_tekanan_negatif_covid_pria")]
                    public string KapasitasIsolasiTanpaTekananNegatifCovidPria;

                    [JsonProperty("kapasitas_isolasi_tanpa_tekanan_negatif_covid_wanita")]
                    public string KapasitasIsolasiTanpaTekananNegatifCovidWanita;

                    [JsonProperty("kapasitas_isolasi_tanpa_tekanan_negatif_covid_anak")]
                    public string KapasitasIsolasiTanpaTekananNegatifCovidAnak;

                    [JsonProperty("kapasitas_nicu_covid")]
                    public string KapasitasNicuCovid;

                    [JsonProperty("kapasitas_picu_covid")]
                    public string KapasitasPicuCovid;

                    [JsonProperty("kapasitas_perina_covid")]
                    public string KapasitasPerinaCovid;

                    [JsonProperty("kapasitas_ok_covid")]
                    public string KapasitasOkCovid;

                    [JsonProperty("kapasitas_hd_covid")]
                    public string KapasitasHdCovid;

                    [JsonProperty("kapasitas_igd_covid")]
                    public string KapasitasIgdCovid;

                    [JsonProperty("kapasitas_vip_non_covid")]
                    public string KapasitasVipNonCovid;

                    [JsonProperty("kapasitas_kelas_1_non_covid_pria")]
                    public string KapasitasKelas1NonCovidPria;

                    [JsonProperty("kapasitas_kelas_1_non_covid_wanita")]
                    public string KapasitasKelas1NonCovidWanita;

                    [JsonProperty("kapasitas_kelas_1_non_covid_anak")]
                    public string KapasitasKelas1NonCovidAnak;

                    [JsonProperty("kapasitas_kelas_2_non_covid_pria")]
                    public string KapasitasKelas2NonCovidPria;

                    [JsonProperty("kapasitas_kelas_2_non_covid_wanita")]
                    public string KapasitasKelas2NonCovidWanita;

                    [JsonProperty("kapasitas_kelas_2_non_covid_anak")]
                    public string KapasitasKelas2NonCovidAnak;

                    [JsonProperty("kapasitas_kelas_3_non_covid_pria")]
                    public string KapasitasKelas3NonCovidPria;

                    [JsonProperty("kapasitas_kelas_3_non_covid_wanita")]
                    public string KapasitasKelas3NonCovidWanita;

                    [JsonProperty("kapasitas_kelas_3_non_covid_anak")]
                    public string KapasitasKelas3NonCovidAnak;

                    [JsonProperty("kapasitas_hcu_non_covid")]
                    public string KapasitasHcuNonCovid;

                    [JsonProperty("kapasitas_iccu_non_covid")]
                    public string KapasitasIccuNonCovid;

                    [JsonProperty("kapasitas_icu_non_covid")]
                    public string KapasitasIcuNonCovid;

                    [JsonProperty("kapasitas_nicu_non_covid")]
                    public string KapasitasNicuNonCovid;

                    [JsonProperty("kapasitas_picu_non_covid")]
                    public string KapasitasPicuNonCovid;

                    [JsonProperty("kapasitas_perina_non_covid")]
                    public string KapasitasPerinaNonCovid;

                    [JsonProperty("kapasitas_ok_non_covid")]
                    public string KapasitasOkNonCovid;

                    [JsonProperty("kapasitas_hd_non_covid")]
                    public string KapasitasHdNonCovid;

                    [JsonProperty("kapasitas_isolasi_non_covid")]
                    public string KapasitasIsolasiNonCovid;

                    [JsonProperty("kapasitas_igd_non_covid")]
                    public string KapasitasIgdNonCovid;
                }

                public class Kosong
                {
                    [JsonProperty("kosong_icu_tekanan_negatif_dengan_ventilator_covid")]
                    public string KosongIcuTekananNegatifDenganVentilatorCovid;

                    [JsonProperty("kosong_icu_tekanan_negatif_tanpa_ventilator_covid")]
                    public string KosongIcuTekananNegatifTanpaVentilatorCovid;

                    [JsonProperty("kosong_icu_tanpa_tekanan_negatif_dengan_ventilator_covid")]
                    public string KosongIcuTanpaTekananNegatifDenganVentilatorCovid;

                    [JsonProperty("kosong_icu_tanpa_tekanan_negatif_tanpa_ventilator_covid")]
                    public string KosongIcuTanpaTekananNegatifTanpaVentilatorCovid;

                    [JsonProperty("kosong_isolasi_tekanan_negatif_covid_pria")]
                    public string KosongIsolasiTekananNegatifCovidPria;

                    [JsonProperty("kosong_isolasi_tekanan_negatif_covid_wanita")]
                    public string KosongIsolasiTekananNegatifCovidWanita;

                    [JsonProperty("kosong_isolasi_tekanan_negatif_covid_anak")]
                    public string KosongIsolasiTekananNegatifCovidAnak;

                    [JsonProperty("kosong_isolasi_tanpa_tekanan_negatif_covid_pria")]
                    public string KosongIsolasiTanpaTekananNegatifCovidPria;

                    [JsonProperty("kosong_isolasi_tanpa_tekanan_negatif_covid_wanita")]
                    public string KosongIsolasiTanpaTekananNegatifCovidWanita;

                    [JsonProperty("kosong_isolasi_tanpa_tekanan_negatif_covid_anak")]
                    public string KosongIsolasiTanpaTekananNegatifCovidAnak;

                    [JsonProperty("kosong_nicu_covid")]
                    public string KosongNicuCovid;

                    [JsonProperty("kosong_picu_covid")]
                    public string KosongPicuCovid;

                    [JsonProperty("kosong_perina_covid")]
                    public string KosongPerinaCovid;

                    [JsonProperty("kosong_ok_covid")]
                    public string KosongOkCovid;

                    [JsonProperty("kosong_hd_covid")]
                    public string KosongHdCovid;

                    [JsonProperty("kosong_igd_covid")]
                    public string KosongIgdCovid;

                    [JsonProperty("kosong_vip_non_covid")]
                    public string KosongVipNonCovid;

                    [JsonProperty("kosong_kelas_1_non_covid_pria")]
                    public string KosongKelas1NonCovidPria;

                    [JsonProperty("kosong_kelas_1_non_covid_wanita")]
                    public string KosongKelas1NonCovidWanita;

                    [JsonProperty("kosong_kelas_1_non_covid_anak")]
                    public string KosongKelas1NonCovidAnak;

                    [JsonProperty("kosong_kelas_2_non_covid_pria")]
                    public string KosongKelas2NonCovidPria;

                    [JsonProperty("kosong_kelas_2_non_covid_wanita")]
                    public string KosongKelas2NonCovidWanita;

                    [JsonProperty("kosong_kelas_2_non_covid_anak")]
                    public string KosongKelas2NonCovidAnak;

                    [JsonProperty("kosong_kelas_3_non_covid_pria")]
                    public string KosongKelas3NonCovidPria;

                    [JsonProperty("kosong_kelas_3_non_covid_wanita")]
                    public string KosongKelas3NonCovidWanita;

                    [JsonProperty("kosong_kelas_3_non_covid_anak")]
                    public string KosongKelas3NonCovidAnak;

                    [JsonProperty("kosong_hcu_non_covid")]
                    public string KosongHcuNonCovid;

                    [JsonProperty("kosong_iccu_non_covid")]
                    public string KosongIccuNonCovid;

                    [JsonProperty("kosong_icu_non_covid")]
                    public string KosongIcuNonCovid;

                    [JsonProperty("kosong_nicu_non_covid")]
                    public string KosongNicuNonCovid;

                    [JsonProperty("kosong_picu_non_covid")]
                    public string KosongPicuNonCovid;

                    [JsonProperty("kosong_perina_non_covid")]
                    public string KosongPerinaNonCovid;

                    [JsonProperty("kosong_ok_non_covid")]
                    public string KosongOkNonCovid;

                    [JsonProperty("kosong_hd_non_covid")]
                    public string KosongHdNonCovid;

                    [JsonProperty("kosong_isolasi_non_covid")]
                    public string KosongIsolasiNonCovid;

                    [JsonProperty("kosong_igd_non_covid")]
                    public string KosongIgdNonCovid;
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

                public class Bed
                {
                    [JsonProperty("covid")]
                    public Covid Covid;

                    [JsonProperty("non_covid")]
                    public NonCovid NonCovid;
                }

                public class Data
                {
                    [JsonProperty("faskes")]
                    public string Faskes;

                    [JsonProperty("bed")]
                    public Bed Bed;

                    [JsonProperty("updated_at")]
                    public string UpdatedAt;
                }

                public class Root
                {
                    [JsonProperty("code")]
                    public int Code;

                    [JsonProperty("messages")]
                    public string Messages;

                    [JsonProperty("data")]
                    public Data Data;
                }
            }
        }
    }
}
