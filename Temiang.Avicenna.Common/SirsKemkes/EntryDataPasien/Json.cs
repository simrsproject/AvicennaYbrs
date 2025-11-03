using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.SirsKemkes.EntryDataPasien
{
    public class Request
    {
        public class RekapPasienMasuk
        {
            [JsonProperty("tanggal")]
            public string Tanggal;

            [JsonProperty("igd_suspect_l")]
            public string IgdSuspectL;

            [JsonProperty("igd_suspect_p")]
            public string IgdSuspectP;

            [JsonProperty("igd_confirm_l")]
            public string IgdConfirmL;

            [JsonProperty("igd_confirm_p")]
            public string IgdConfirmP;

            [JsonProperty("rj_suspect_l")]
            public string RjSuspectL;

            [JsonProperty("rj_suspect_p")]
            public string RjSuspectP;

            [JsonProperty("rj_confirm_l")]
            public string RjConfirmL;

            [JsonProperty("rj_confirm_p")]
            public string RjConfirmP;

            [JsonProperty("ri_suspect_l")]
            public string RiSuspectL;

            [JsonProperty("ri_suspect_p")]
            public string RiSuspectP;

            [JsonProperty("ri_confirm_l")]
            public string RiConfirmL;

            [JsonProperty("ri_confirm_p")]
            public string RiConfirmP;
        }

        public class RekapPasienDirawatDenganKomorbid
        {
            [JsonProperty("tanggal")]
            public string Tanggal;

            [JsonProperty("icu_dengan_ventilator_suspect_l")]
            public string IcuDenganVentilatorSuspectL;

            [JsonProperty("icu_dengan_ventilator_suspect_p")]
            public string IcuDenganVentilatorSuspectP;

            [JsonProperty("icu_dengan_ventilator_confirm_l")]
            public string IcuDenganVentilatorConfirmL;

            [JsonProperty("icu_dengan_ventilator_confirm_p")]
            public string IcuDenganVentilatorConfirmP;

            [JsonProperty("icu_tanpa_ventilator_suspect_l")]
            public string IcuTanpaVentilatorSuspectL;

            [JsonProperty("icu_tanpa_ventilator_suspect_p")]
            public string IcuTanpaVentilatorSuspectP;

            [JsonProperty("icu_tanpa_ventilator_confirm_l")]
            public string IcuTanpaVentilatorConfirmL;

            [JsonProperty("icu_tanpa_ventilator_confirm_p")]
            public string IcuTanpaVentilatorConfirmP;

            [JsonProperty("icu_tekanan_negatif_dengan_ventilator_suspect_l")]
            public string IcuTekananNegatifDenganVentilatorSuspectL;

            [JsonProperty("icu_tekanan_negatif_dengan_ventilator_suspect_p")]
            public string IcuTekananNegatifDenganVentilatorSuspectP;

            [JsonProperty("icu_tekanan_negatif_dengan_ventilator_confirm_l")]
            public string IcuTekananNegatifDenganVentilatorConfirmL;

            [JsonProperty("icu_tekanan_negatif_dengan_ventilator_confirm_p")]
            public string IcuTekananNegatifDenganVentilatorConfirmP;

            [JsonProperty("icu_tekanan_negatif_tanpa_ventilator_suspect_l")]
            public string IcuTekananNegatifTanpaVentilatorSuspectL;

            [JsonProperty("icu_tekanan_negatif_tanpa_ventilator_suspect_p")]
            public string IcuTekananNegatifTanpaVentilatorSuspectP;

            [JsonProperty("icu_tekanan_negatif_tanpa_ventilator_confirm_l")]
            public string IcuTekananNegatifTanpaVentilatorConfirmL;

            [JsonProperty("icu_tekanan_negatif_tanpa_ventilator_confirm_p")]
            public string IcuTekananNegatifTanpaVentilatorConfirmP;

            [JsonProperty("isolasi_tekanan_negatif_suspect_l")]
            public string IsolasiTekananNegatifSuspectL;

            [JsonProperty("isolasi_tekanan_negatif_suspect_p")]
            public string IsolasiTekananNegatifSuspectP;

            [JsonProperty("isolasi_tekanan_negatif_confirm_l")]
            public string IsolasiTekananNegatifConfirmL;

            [JsonProperty("isolasi_tekanan_negatif_confirm_p")]
            public string IsolasiTekananNegatifConfirmP;

            [JsonProperty("isolasi_tanpa_tekanan_negatif_suspect_l")]
            public string IsolasiTanpaTekananNegatifSuspectL;

            [JsonProperty("isolasi_tanpa_tekanan_negatif_suspect_p")]
            public string IsolasiTanpaTekananNegatifSuspectP;

            [JsonProperty("isolasi_tanpa_tekanan_negatif_confirm_l")]
            public string IsolasiTanpaTekananNegatifConfirmL;

            [JsonProperty("isolasi_tanpa_tekanan_negatif_confirm_p")]
            public string IsolasiTanpaTekananNegatifConfirmP;

            [JsonProperty("nicu_khusus_covid_suspect_l")]
            public string NicuKhususCovidSuspectL;

            [JsonProperty("nicu_khusus_covid_suspect_p")]
            public string NicuKhususCovidSuspectP;

            [JsonProperty("nicu_khusus_covid_confirm_l")]
            public string NicuKhususCovidConfirmL;

            [JsonProperty("nicu_khusus_covid_confirm_p")]
            public string NicuKhususCovidConfirmP;

            [JsonProperty("picu_khusus_covid_suspect_l")]
            public string PicuKhususCovidSuspectL;

            [JsonProperty("picu_khusus_covid_suspect_p")]
            public string PicuKhususCovidSuspectP;

            [JsonProperty("picu_khusus_covid_confirm_l")]
            public string PicuKhususCovidConfirmL;

            [JsonProperty("picu_khusus_covid_confirm_p")]
            public string PicuKhususCovidConfirmP;
        }

        public class RekapPasienDirawatTanpaKomorbid
        {
            [JsonProperty("tanggal")]
            public string Tanggal;

            [JsonProperty("icu_dengan_ventilator_suspect_l")]
            public string IcuDenganVentilatorSuspectL;

            [JsonProperty("icu_dengan_ventilator_suspect_p")]
            public string IcuDenganVentilatorSuspectP;

            [JsonProperty("icu_dengan_ventilator_confirm_l")]
            public string IcuDenganVentilatorConfirmL;

            [JsonProperty("icu_dengan_ventilator_confirm_p")]
            public string IcuDenganVentilatorConfirmP;

            [JsonProperty("icu_tanpa_ventilator_suspect_l")]
            public string IcuTanpaVentilatorSuspectL;

            [JsonProperty("icu_tanpa_ventilator_suspect_p")]
            public string IcuTanpaVentilatorSuspectP;

            [JsonProperty("icu_tanpa_ventilator_confirm_l")]
            public string IcuTanpaVentilatorConfirmL;

            [JsonProperty("icu_tanpa_ventilator_confirm_p")]
            public string IcuTanpaVentilatorConfirmP;

            [JsonProperty("icu_tekanan_negatif_dengan_ventilator_suspect_l")]
            public string IcuTekananNegatifDenganVentilatorSuspectL;

            [JsonProperty("icu_tekanan_negatif_dengan_ventilator_suspect_p")]
            public string IcuTekananNegatifDenganVentilatorSuspectP;

            [JsonProperty("icu_tekanan_negatif_dengan_ventilator_confirm_l")]
            public string IcuTekananNegatifDenganVentilatorConfirmL;

            [JsonProperty("icu_tekanan_negatif_dengan_ventilator_confirm_p")]
            public string IcuTekananNegatifDenganVentilatorConfirmP;

            [JsonProperty("icu_tekanan_negatif_tanpa_ventilator_suspect_l")]
            public string IcuTekananNegatifTanpaVentilatorSuspectL;

            [JsonProperty("icu_tekanan_negatif_tanpa_ventilator_suspect_p")]
            public string IcuTekananNegatifTanpaVentilatorSuspectP;

            [JsonProperty("icu_tekanan_negatif_tanpa_ventilator_confirm_l")]
            public string IcuTekananNegatifTanpaVentilatorConfirmL;

            [JsonProperty("icu_tekanan_negatif_tanpa_ventilator_confirm_p")]
            public string IcuTekananNegatifTanpaVentilatorConfirmP;

            [JsonProperty("isolasi_tekanan_negatif_suspect_l")]
            public string IsolasiTekananNegatifSuspectL;

            [JsonProperty("isolasi_tekanan_negatif_suspect_p")]
            public string IsolasiTekananNegatifSuspectP;

            [JsonProperty("isolasi_tekanan_negatif_confirm_l")]
            public string IsolasiTekananNegatifConfirmL;

            [JsonProperty("isolasi_tekanan_negatif_confirm_p")]
            public string IsolasiTekananNegatifConfirmP;

            [JsonProperty("isolasi_tanpa_tekanan_negatif_suspect_l")]
            public string IsolasiTanpaTekananNegatifSuspectL;

            [JsonProperty("isolasi_tanpa_tekanan_negatif_suspect_p")]
            public string IsolasiTanpaTekananNegatifSuspectP;

            [JsonProperty("isolasi_tanpa_tekanan_negatif_confirm_l")]
            public string IsolasiTanpaTekananNegatifConfirmL;

            [JsonProperty("isolasi_tanpa_tekanan_negatif_confirm_p")]
            public string IsolasiTanpaTekananNegatifConfirmP;

            [JsonProperty("nicu_khusus_covid_suspect_l")]
            public string NicuKhususCovidSuspectL;

            [JsonProperty("nicu_khusus_covid_suspect_p")]
            public string NicuKhususCovidSuspectP;

            [JsonProperty("nicu_khusus_covid_confirm_l")]
            public string NicuKhususCovidConfirmL;

            [JsonProperty("nicu_khusus_covid_confirm_p")]
            public string NicuKhususCovidConfirmP;

            [JsonProperty("picu_khusus_covid_suspect_l")]
            public string PicuKhususCovidSuspectL;

            [JsonProperty("picu_khusus_covid_suspect_p")]
            public string PicuKhususCovidSuspectP;

            [JsonProperty("picu_khusus_covid_confirm_l")]
            public string PicuKhususCovidConfirmL;

            [JsonProperty("picu_khusus_covid_confirm_p")]
            public string PicuKhususCovidConfirmP;
        }

        public class RekapPasienKeluar
        {
            public class Simpan
            {
                [JsonProperty("tanggal")]
                public string Tanggal;

                [JsonProperty("sembuh")]
                public string Sembuh;

                [JsonProperty("discarded")]
                public string Discarded;

                [JsonProperty("meninggal_komorbid")]
                public string MeninggalKomorbid;

                [JsonProperty("meninggal_tanpa_komorbid")]
                public string MeninggalTanpaKomorbid;

                [JsonProperty("meninggal_prob_pre_komorbid")]
                public string MeninggalProbPreKomorbid;

                [JsonProperty("meninggal_prob_neo_komorbid")]
                public string MeninggalProbNeoKomorbid;

                [JsonProperty("meninggal_prob_bayi_komorbid")]
                public string MeninggalProbBayiKomorbid;

                [JsonProperty("meninggal_prob_balita_komorbid")]
                public string MeninggalProbBalitaKomorbid;

                [JsonProperty("meninggal_prob_anak_komorbid")]
                public string MeninggalProbAnakKomorbid;

                [JsonProperty("meninggal_prob_remaja_komorbid")]
                public string MeninggalProbRemajaKomorbid;

                [JsonProperty("meninggal_prob_dws_komorbid")]
                public string MeninggalProbDwsKomorbid;

                [JsonProperty("meninggal_prob_lansia_komorbid")]
                public string MeninggalProbLansiaKomorbid;

                [JsonProperty("meninggal_prob_pre_tanpa_komorbid")]
                public string MeninggalProbPreTanpaKomorbid;

                [JsonProperty("meninggal_prob_neo_tanpa_komorbid")]
                public string MeninggalProbNeoTanpaKomorbid;

                [JsonProperty("meninggal_prob_bayi_tanpa_komorbid")]
                public string MeninggalProbBayiTanpaKomorbid;

                [JsonProperty("meninggal_prob_balita_tanpa_komorbid")]
                public string MeninggalProbBalitaTanpaKomorbid;

                [JsonProperty("meninggal_prob_anak_tanpa_komorbid")]
                public string MeninggalProbAnakTanpaKomorbid;

                [JsonProperty("meninggal_prob_remaja_tanpa_komorbid")]
                public string MeninggalProbRemajaTanpaKomorbid;

                [JsonProperty("meninggal_prob_dws_tanpa_komorbid")]
                public string MeninggalProbDwsTanpaKomorbid;

                [JsonProperty("meninggal_prob_lansia_tanpa_komorbid")]
                public string MeninggalProbLansiaTanpaKomorbid;

                [JsonProperty("meninggal _discarded_komorbid")]
                public string MeninggalDiscardedKomorbid;

                [JsonProperty("meninggal _discarded_tanpa_komorbid")]
                public string MeninggalDiscardedTanpaKomorbid;

                [JsonProperty("dirujuk")]
                public string Dirujuk;

                [JsonProperty("isman")]
                public string Isman;

                [JsonProperty("aps")]
                public string Aps;
            }

            public class Hapus
            {
                [JsonProperty("tanggal")]
                public string Tanggal;
            }
        }

        public class EntryDataRuangandanTempatTidur
        {
            [JsonProperty("id_tt")]
            public string IdTt;

            [JsonProperty("ruang")]
            public string Ruang;

            [JsonProperty("jumlah_ruang")]
            public string JumlahRuang;

            [JsonProperty("jumlah")]
            public string Jumlah;

            [JsonProperty("terpakai")]
            public string Terpakai;

            [JsonProperty("prepare")]
            public string Prepare;

            [JsonProperty("prepare_plan")]
            public string PreparePlan;

            [JsonProperty("covid")]
            public int Covid;
        }

    }

    public class Response
    {
        public class RekapPasienMasuk
        {
            public class TRekapPasienMasuk
            {
                [JsonProperty("status")]
                public string Status;

                [JsonProperty("message")]
                public string Message;
            }

            public class Root
            {
                [JsonProperty("RekapPasienMasuk")]
                public List<TRekapPasienMasuk> RekapPasienMasuk;
            }
        }

        public class RekapPasienDirawatDenganKomorbid
        {
            public class TRekapPasienDirawatDenganKomorbid
            {
                [JsonProperty("status")]
                public string Status;

                [JsonProperty("message")]
                public string Message;
            }

            public class Root
            {
                [JsonProperty("RekapPasienDirawatDenganKomorbid")]
                public List<TRekapPasienDirawatDenganKomorbid> RekapPasienMasuk;
            }
        }

        public class RekapPasienDirawatTanpaKomorbid
        {
            public class TRekapPasienDirawatTanpaKomorbid
            {
                [JsonProperty("status")]
                public string Status;

                [JsonProperty("message")]
                public string Message;
            }

            public class Root
            {
                [JsonProperty("RekapPasienDirawatTanpaKomorbid")]
                public List<TRekapPasienDirawatTanpaKomorbid> RekapPasienMasuk;
            }
        }

        public class RekapPasienKeluar
        {
            public class TRekapPasienKeluar
            {
                [JsonProperty("status")]
                public string Status;

                [JsonProperty("message")]
                public string Message;
            }

            public class Root
            {
                [JsonProperty("RekapPasienKeluar")]
                public List<TRekapPasienKeluar> RekapPasienMasuk;
            }
        }

        public class KelompokUsiaProbable
        {
            public class TKelompokUsiaProbable
            {
                [JsonProperty("id")]
                public string Id;

                [JsonProperty("kelompok_usia")]
                public string KelompokUsia;

                [JsonProperty("deskripsi")]
                public string Deskripsi;
            }

            public class Root
            {
                [JsonProperty("kelompok_usia_probable")]
                public List<TKelompokUsiaProbable> KelompokUsiaProbable;
            }
        }
    }
}
