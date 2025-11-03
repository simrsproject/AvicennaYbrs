using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.SirsKemkes.LaporanKematian
{
    public class Json
    {
        public class Select
        {
            public class Pagination
            {
                [JsonProperty("total_number_of_pages")]
                public int TotalNumberOfPages;

                [JsonProperty("current_page")]
                public int CurrentPage;
            }

            [Serializable]
            public class Datum
            {
                [JsonProperty("id")]
                public int Id;

                [JsonProperty("nik")]
                public string Nik;

                [JsonProperty("tanggal_masuk")]
                public string TanggalMasuk;

                [JsonProperty("saturasi_oksigen")]
                public int SaturasiOksigen;

                [JsonProperty("tanggal_kematian")]
                public string TanggalKematian;

                [JsonProperty("lokasi_kematian")]
                public string LokasiKematian;

                [JsonProperty("penyebab_kematian_langsung_id")]
                public string PenyebabKematianLangsungId;

                [JsonProperty("kasus_kematian")]
                public string KasusKematian;

                [JsonProperty("status_komorbid")]
                public string StatusKomorbid;

                [JsonProperty("komorbid_1")]
                public string Komorbid1;

                [JsonProperty("komorbid_2")]
                public string Komorbid2;

                [JsonProperty("komorbid_3")]
                public string Komorbid3;

                [JsonProperty("komorbid_4")]
                public string Komorbid4;

                [JsonProperty("status_kehamilan")]
                public string StatusKehamilan;
            }

            public class Root
            {
                [JsonProperty("status")]
                public bool Status;

                [JsonProperty("message")]
                public string Message;

                [JsonProperty("pagination")]
                public Pagination Pagination;

                [JsonProperty("data")]
                public List<Datum> Data;
            }
        }

        public class Insert
        {
            public class Request
            {
                public class Root
                {
                    [JsonProperty("nik")]
                    public string Nik;

                    [JsonProperty("nama")]
                    public string Nama;

                    [JsonProperty("jenis_kelamin")]
                    public string JenisKelamin;

                    [JsonProperty("tanggal_lahir")]
                    public string TanggalLahir;

                    [JsonProperty("ktp_alamat")]
                    public string KtpAlamat;

                    [JsonProperty("ktp_kelurahan_id")]
                    public string KtpKelurahanId;

                    [JsonProperty("ktp_kecamatan_id")]
                    public string KtpKecamatanId;

                    [JsonProperty("ktp_kab_kota_id")]
                    public string KtpKabKotaId;

                    [JsonProperty("ktp_provinsi_id")]
                    public string KtpProvinsiId;

                    [JsonProperty("domisili_alamat")]
                    public string DomisiliAlamat;

                    [JsonProperty("tanggal_masuk")]
                    public string TanggalMasuk;

                    [JsonProperty("saturasi_oksigen")]
                    public int SaturasiOksigen;

                    [JsonProperty("tanggal_kematian")]
                    public string TanggalKematian;

                    [JsonProperty("lokasi_kematian_id")]
                    public int LokasiKematianId;

                    [JsonProperty("penyebab_kematian_langsung_id")]
                    public string PenyebabKematianLangsungId;

                    [JsonProperty("kasus_kematian_id")]
                    public string KasusKematianId;

                    [JsonProperty("status_komorbid")]
                    public string StatusKomorbid;

                    [JsonProperty("komorbid_1_id")]
                    public string Komorbid1Id;

                    [JsonProperty("komorbid_2_id")]
                    public string Komorbid2Id;

                    [JsonProperty("komorbid_3_id")]
                    public string Komorbid3Id;

                    [JsonProperty("komorbid_4_id")]
                    public string Komorbid4Id;

                    [JsonProperty("status_kehamilan")]
                    public string StatusKehamilan;
                }
            }

            public class Response
            {
                public class Data
                {
                    [JsonProperty("id")]
                    public int Id;

                    [JsonProperty("nik")]
                    public string Nik;
                }

                public class Root
                {
                    [JsonProperty("status")]
                    public bool Status;

                    [JsonProperty("message")]
                    public string Message;

                    [JsonProperty("data")]
                    public Data Data;
                }
            }
        }

        public class Update
        {
            public class Request
            {
                public class Root
                {
                    [JsonProperty("tanggal_masuk")]
                    public string TanggalMasuk;

                    [JsonProperty("saturasi_oksigen")]
                    public int SaturasiOksigen;

                    [JsonProperty("tanggal_kematian")]
                    public string TanggalKematian;

                    [JsonProperty("lokasi_kematian_id")]
                    public int LokasiKematianId;

                    [JsonProperty("penyebab_kematian_langsung_id")]
                    public string PenyebabKematianLangsungId;

                    [JsonProperty("kasus_kematian_id")]
                    public string KasusKematianId;

                    [JsonProperty("status_komorbid")]
                    public string StatusKomorbid;

                    [JsonProperty("komorbid_1_id")]
                    public string Komorbid1Id;

                    [JsonProperty("komorbid_2_id")]
                    public string Komorbid2Id;

                    [JsonProperty("komorbid_3_id")]
                    public string Komorbid3Id;

                    [JsonProperty("komorbid_4_id")]
                    public string Komorbid4Id;
                }
            }

            public class Response
            {
                public class Data
                {
                    [JsonProperty("id")]
                    public int Id;
                }

                public class Root
                {
                    [JsonProperty("status")]
                    public bool Status;

                    [JsonProperty("message")]
                    public string Message;

                    [JsonProperty("data")]
                    public Data Data;
                }
            }
        }

        public class Delete
        {
            public class Response
            {
                public class Root
                {
                    [JsonProperty("status")]
                    public bool Status;

                    [JsonProperty("message")]
                    public string Message;
                }
            }
        }
    }
}
