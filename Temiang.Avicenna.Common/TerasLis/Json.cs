using Newtonsoft.Json;
using System.Collections.Generic;

namespace Temiang.Avicenna.Common.TerasLis.Json
{
    public class Response
    {
        public class Auth
        {
            [JsonProperty("status")]
            public int? Status;

            [JsonProperty("token")]
            public string Token;
        }

        public class Order
        {
            public class DataIn
            {
                [JsonProperty("data_pasien")]
                public DataPasien DataPasien;

                [JsonProperty("data_order")]
                public DataOrder DataOrder;

                [JsonProperty("pemeriksaan")]
                public List<Pemeriksaan> Pemeriksaan;

                [JsonProperty("no_lab")]
                public string NoLab;
            }

            public class DataOrder
            {
                [JsonProperty("status_pasien")]
                public string StatusPasien;

                [JsonProperty("ruang")]
                public string Ruang;

                [JsonProperty("dokter_pengirim")]
                public string DokterPengirim;

                [JsonProperty("dokter_pk")]
                public string DokterPk;

                [JsonProperty("bahasa")]
                public string Bahasa;

                [JsonProperty("diagnosa")]
                public string Diagnosa;

                [JsonProperty("cito")]
                public int? Cito;

                [JsonProperty("golongan")]
                public int? Golongan;
            }

            public class DataPasien
            {
                [JsonProperty("no_rekam")]
                public int? NoRekam;

                [JsonProperty("no_ref")]
                public string NoRef;

                [JsonProperty("no_bpjs")]
                public string NoBpjs;

                [JsonProperty("sebutan")]
                public string Sebutan;

                [JsonProperty("nama_pasien")]
                public string NamaPasien;

                [JsonProperty("jenis_kelamin")]
                public string JenisKelamin;

                [JsonProperty("tgl_lahir")]
                public string TglLahir;

                [JsonProperty("y")]
                public int? Y;

                [JsonProperty("m")]
                public int? M;

                [JsonProperty("d")]
                public int? D;

                [JsonProperty("jam")]
                public int? Jam;

                [JsonProperty("alamat")]
                public string Alamat;

                [JsonProperty("telp")]
                public string Telp;
            }

            public class Pemeriksaan
            {
                [JsonProperty("id_pemeriksaan")]
                public int? IdPemeriksaan;

                [JsonProperty("status")]
                public string Status;
            }

            public class Root
            {
                [JsonProperty("data_in")]
                public DataIn DataIn;

                [JsonProperty("no_lab")]
                public string NoLab;

                [JsonProperty("status")]
                public int? Status;

                [JsonProperty("pesan")]
                public string Pesan;

                [JsonProperty("id")]
                public string Id;
            }
        }

        public class Result
        {
            public class Catatan
            {
                [JsonProperty("analis")]
                public string Analis;

                [JsonProperty("pk")]
                public string Pk;

                [JsonProperty("kritis")]
                public List<string> Kritis;
            }

            public class Order
            {
                [JsonProperty("id")]
                public string Id;

                [JsonProperty("HID")]
                public string HID;

                [JsonProperty("tgl")]
                public string Tgl;

                [JsonProperty("no_order")]
                public string NoOrder;

                [JsonProperty("no_index1")]
                public string NoIndex1;

                [JsonProperty("no_index2")]
                public string NoIndex2;

                [JsonProperty("id_client")]
                public string IdClient;

                [JsonProperty("token")]
                public string Token;

                [JsonProperty("status_order")]
                public string StatusOrder;

                [JsonProperty("status_pasien")]
                public string StatusPasien;

                [JsonProperty("pengirim_ruang")]
                public string PengirimRuang;

                [JsonProperty("dokter_pengirim")]
                public string DokterPengirim;

                [JsonProperty("diagnosa")]
                public string Diagnosa;

                [JsonProperty("pcr")]
                public string Pcr;

                [JsonProperty("specimen")]
                public string Specimen;

                [JsonProperty("no_specimen")]
                public string NoSpecimen;

                [JsonProperty("sampleke")]
                public string Sampleke;

                [JsonProperty("faskes")]
                public string Faskes;

                [JsonProperty("faskes_provinsi")]
                public string FaskesProvinsi;

                [JsonProperty("faskes_kota")]
                public string FaskesKota;

                [JsonProperty("faskes_ket")]
                public string FaskesKet;

                [JsonProperty("tgl_gejala")]
                public string TglGejala;

                [JsonProperty("tgl_pengambilan_spec")]
                public string TglPengambilanSpec;

                [JsonProperty("tgl_pengiriman_spec")]
                public string TglPengirimanSpec;

                [JsonProperty("nama_lab")]
                public string NamaLab;

                [JsonProperty("hasil_dikirim")]
                public string HasilDikirim;

                [JsonProperty("dokter_pathologi")]
                public string DokterPathologi;

                [JsonProperty("bahasa_cetak")]
                public string BahasaCetak;

                [JsonProperty("jasa_rs")]
                public string JasaRs;

                [JsonProperty("jasa_konsul")]
                public string JasaKonsul;

                [JsonProperty("status")]
                public string Status;

                [JsonProperty("update_date")]
                public string UpdateDate;

                [JsonProperty("update_by")]
                public string UpdateBy;

                [JsonProperty("alasan_batal")]
                public string AlasanBatal;

                [JsonProperty("tgl_cekin")]
                public string TglCekin;

                [JsonProperty("cekin_by")]
                public string CekinBy;

                [JsonProperty("tgl_selesai")]
                public string TglSelesai;

                [JsonProperty("selesai_by")]
                public string SelesaiBy;

                [JsonProperty("tgl_validasi")]
                public string TglValidasi;

                [JsonProperty("validasi_by")]
                public string ValidasiBy;

                [JsonProperty("validasi_status")]
                public string ValidasiStatus;

                [JsonProperty("catatan")]
                public Catatan Catatan;

                [JsonProperty("cancel_by")]
                public string CancelBy;

                [JsonProperty("CreateBy")]
                public string CreateBy;

                [JsonProperty("cetak")]
                public string Cetak;

                [JsonProperty("token_pasien")]
                public string TokenPasien;

                [JsonProperty("tanggal")]
                public string Tanggal;

                [JsonProperty("no_ref")]
                public string NoRef;

                [JsonProperty("no_rekam")]
                public string NoRekam;

                [JsonProperty("no_bpjs")]
                public string NoBpjs;

                [JsonProperty("sebutan")]
                public string Sebutan;

                [JsonProperty("nama_peserta")]
                public string NamaPeserta;

                [JsonProperty("jenis_kelamin")]
                public string JenisKelamin;

                [JsonProperty("tangal_lahir")]
                public string TangalLahir;

                [JsonProperty("usia_tahun")]
                public string UsiaTahun;

                [JsonProperty("usia_bulan")]
                public string UsiaBulan;

                [JsonProperty("usia_hari")]
                public string UsiaHari;

                [JsonProperty("usia_jam")]
                public string UsiaJam;

                [JsonProperty("alamat")]
                public string Alamat;

                [JsonProperty("no_hp")]
                public string NoHp;

                [JsonProperty("email")]
                public object Email;

                [JsonProperty("last_update")]
                public string LastUpdate;

                [JsonProperty("register_by")]
                public string RegisterBy;

                [JsonProperty("log")]
                public object Log;

                [JsonProperty("alamat_domisili")]
                public string AlamatDomisili;

                [JsonProperty("rt")]
                public string Rt;

                [JsonProperty("rw")]
                public string Rw;

                [JsonProperty("kecamatan")]
                public string Kecamatan;

                [JsonProperty("kelurahan")]
                public string Kelurahan;

                [JsonProperty("kota")]
                public string Kota;

                [JsonProperty("provinsi")]
                public string Provinsi;

                [JsonProperty("kewarganegaraan")]
                public string Kewarganegaraan;

                [JsonProperty("kewarganegaraan_detail")]
                public string KewarganegaraanDetail;
            }

            public class Pemeriksaan
            {
                [JsonProperty("id")]
                public string Id;

                [JsonProperty("createDate")]
                public string CreateDate;

                [JsonProperty("ido")]
                public string Ido;

                [JsonProperty("idi")]
                public string Idi;

                [JsonProperty("idh")]
                public string Idh;

                [JsonProperty("cekin_status")]
                public string CekinStatus;

                [JsonProperty("validasi_status")]
                public string ValidasiStatus;

                [JsonProperty("hasil")]
                public string Hasil;

                [JsonProperty("status")]
                public string Status;

                [JsonProperty("tanggal_cekin")]
                public string TanggalCekin;

                [JsonProperty("tanggal_hasil")]
                public string TanggalHasil;

                [JsonProperty("tanggal_validasi")]
                public string TanggalValidasi;

                [JsonProperty("cekin_oleh")]
                public string CekinOleh;

                [JsonProperty("validasi_oleh")]
                public string ValidasiOleh;

                [JsonProperty("hasil_oleh")]
                public string HasilOleh;

                [JsonProperty("statusx")]
                public string Statusx;

                [JsonProperty("kode_alat")]
                public object KodeAlat;

                [JsonProperty("history")]
                public string History;

                [JsonProperty("has_ket")]
                public string HasKet;

                [JsonProperty("nilnor")]
                public string Nilnor;

                [JsonProperty("flag")]
                public string Flag;

                [JsonProperty("kritis")]
                public string Kritis;

                [JsonProperty("NmTestInd")]
                public string NmTestInd;

                [JsonProperty("UnitTest")]
                public string UnitTest;
            }

            public class Root
            {
                [JsonProperty("pemeriksaan")]
                public List<Pemeriksaan> Pemeriksaan;

                [JsonProperty("order")]
                public Order Order;
            }
        }
    }

    public class Request
    {
        public class Order
        {
            public class DataOrder
            {
                [JsonProperty("status_pasien")]
                public string StatusPasien;

                [JsonProperty("ruang")]
                public string Ruang;

                [JsonProperty("dokter_pengirim")]
                public string DokterPengirim;

                [JsonProperty("dokter_pk")]
                public string DokterPk;

                [JsonProperty("bahasa")]
                public string Bahasa;

                [JsonProperty("diagnosa")]
                public string Diagnosa;

                [JsonProperty("cito")]
                public int? Cito;

                [JsonProperty("golongan")]
                public int? Golongan;
            }

            public class DataPasien
            {
                [JsonProperty("no_rekam")]
                public int NoRekam;

                [JsonProperty("no_ref")]
                public string NoRef;

                [JsonProperty("no_bpjs")]
                public string NoBpjs;

                [JsonProperty("sebutan")]
                public string Sebutan;

                [JsonProperty("nama_pasien")]
                public string NamaPasien;

                [JsonProperty("jenis_kelamin")]
                public string JenisKelamin;

                [JsonProperty("tgl_lahir")]
                public string TglLahir;

                [JsonProperty("y")]
                public int? Y;

                [JsonProperty("m")]
                public int? M;

                [JsonProperty("d")]
                public int? D;

                [JsonProperty("jam")]
                public int? Jam;

                [JsonProperty("alamat")]
                public string Alamat;

                [JsonProperty("telp")]
                public string Telp;
            }

            public class Pemeriksaan
            {
                [JsonProperty("id_pemeriksaan")]
                public int? IdPemeriksaan;

                [JsonProperty("status")]
                public string Status;
            }

            public class Root
            {
                [JsonProperty("data_pasien")]
                public DataPasien DataPasien;

                [JsonProperty("data_order")]
                public DataOrder DataOrder;

                [JsonProperty("pemeriksaan")]
                public List<Pemeriksaan> Pemeriksaan;

                [JsonProperty("no_lab")]
                public string NoLab;
            }
        }
    }
}
