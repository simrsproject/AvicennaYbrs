using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Temiang.Avicenna.Common.Sisrute.Rujukan
{
    public class Post : Common.Response
    {

        [Serializable]
        public class JENISKELAMIN
        {

            [JsonProperty("KODE")]
            public string KODE { get; set; }

            [JsonProperty("NAMA")]
            public string NAMA { get; set; }
        }

        [Serializable]
        public class PASIEN
        {

            [JsonProperty("NORM")]
            public string NORM { get; set; }

            [JsonProperty("NAMA")]
            public string NAMA { get; set; }

            [JsonProperty("KONTAK")]
            public string KONTAK { get; set; }

            [JsonProperty("ALAMAT")]
            public string ALAMAT { get; set; }

            [JsonProperty("TEMPAT_LAHIR")]
            public string TEMPATLAHIR { get; set; }

            [JsonProperty("TANGGAL_LAHIR")]
            public string TANGGALLAHIR { get; set; }

            [JsonProperty("JENIS_KELAMIN")]
            public JENISKELAMIN JENISKELAMIN { get; set; }

            [JsonProperty("NO_KARTU_JKN")]
            public string NOKARTUJKN { get; set; }

            [JsonProperty("NIK")]
            public string NIK { get; set; }
        }

        [Serializable]
        public class FASKESASAL
        {

            [JsonProperty("KODE")]
            public string KODE { get; set; }

            [JsonProperty("NAMA")]
            public string NAMA { get; set; }
        }

        [Serializable]
        public class FASKESTUJUAN
        {

            [JsonProperty("KODE")]
            public string KODE { get; set; }

            [JsonProperty("NAMA")]
            public string NAMA { get; set; }
        }

        [Serializable]
        public class JENISRUJUKAN
        {

            [JsonProperty("KODE")]
            public string KODE { get; set; }

            [JsonProperty("NAMA")]
            public string NAMA { get; set; }
        }

        [Serializable]
        public class ALASAN
        {

            [JsonProperty("KODE")]
            public string KODE { get; set; }

            [JsonProperty("NAMA")]
            public string NAMA { get; set; }
        }

        [Serializable]
        public class STATUS
        {

            [JsonProperty("KODE")]
            public string KODE { get; set; }

            [JsonProperty("NAMA")]
            public string NAMA { get; set; }
        }

        [Serializable]
        public class DOKTER
        {

            [JsonProperty("NIK")]
            public string NIK { get; set; }

            [JsonProperty("NAMA")]
            public string NAMA { get; set; }
        }

        [Serializable]
        public class PETUGAS
        {

            [JsonProperty("NIK")]
            public string NIK { get; set; }

            [JsonProperty("NAMA")]
            public string NAMA { get; set; }
        }

        [Serializable]
        public class DIAGNOSA
        {

            [JsonProperty("KODE")]
            public string KODE { get; set; }

            [JsonProperty("NAMA")]
            public string NAMA { get; set; }
        }

        [Serializable]
        public class RUJUKAN
        {

            [JsonProperty("NOMOR")]
            public string NOMOR { get; set; }

            [JsonProperty("FASKES_ASAL")]
            public FASKESASAL FASKESASAL { get; set; }

            [JsonProperty("FASKES_TUJUAN")]
            public FASKESTUJUAN FASKESTUJUAN { get; set; }

            [JsonProperty("JENIS_RUJUKAN")]
            public JENISRUJUKAN JENISRUJUKAN { get; set; }

            [JsonProperty("ALASAN")]
            public ALASAN ALASAN { get; set; }

            [JsonProperty("ALASAN_LAINNYA")]
            public string ALASANLAINNYA { get; set; }

            [JsonProperty("STATUS")]
            public STATUS STATUS { get; set; }

            [JsonProperty("TANGGAL")]
            public string TANGGAL { get; set; }

            [JsonProperty("DOKTER")]
            public DOKTER DOKTER { get; set; }

            [JsonProperty("PETUGAS")]
            public PETUGAS PETUGAS { get; set; }

            [JsonProperty("DIAGNOSA")]
            public DIAGNOSA DIAGNOSA { get; set; }
        }

        [Serializable]
        public class KESADARAN
        {

            [JsonProperty("KODE")]
            public string KODE { get; set; }

            [JsonProperty("NAMA")]
            public string NAMA { get; set; }
        }

        [Serializable]
        public class NYERI
        {

            [JsonProperty("KODE")]
            public string KODE { get; set; }

            [JsonProperty("NAMA")]
            public string NAMA { get; set; }
        }

        [Serializable]
        public class KONDISIUMUM
        {

            [JsonProperty("ANAMNESIS_DAN_PEMERIKSAAN_FISIK")]
            public string ANAMNESISDANPEMERIKSAANFISIK { get; set; }

            [JsonProperty("KESADARAN")]
            public KESADARAN KESADARAN { get; set; }

            [JsonProperty("TEKANAN_DARAH")]
            public string TEKANANDARAH { get; set; }

            [JsonProperty("FREKUENSI_NADI")]
            public string FREKUENSINADI { get; set; }

            [JsonProperty("SUHU")]
            public string SUHU { get; set; }

            [JsonProperty("PERNAPASAN")]
            public string PERNAPASAN { get; set; }

            [JsonProperty("NYERI")]
            public NYERI NYERI { get; set; }

            [JsonProperty("KEADAAN_UMUM")]
            public string KEADAANUMUM { get; set; }

            [JsonProperty("ALERGI")]
            public string ALERGI { get; set; }
        }

        [Serializable]
        public class PENUNJANG
        {

            [JsonProperty("LABORATORIUM")]
            public string LABORATORIUM { get; set; }

            [JsonProperty("RADIOLOGI")]
            public string RADIOLOGI { get; set; }

            [JsonProperty("TERAPI_ATAU_TINDAKAN")]
            public string TERAPIATAUTINDAKAN { get; set; }
        }
        [Serializable]
        public class Data
        {

            [JsonProperty("PASIEN")]
            public PASIEN PASIEN { get; set; }

            [JsonProperty("RUJUKAN")]
            public RUJUKAN RUJUKAN { get; set; }

            [JsonProperty("KONDISI_UMUM")]
            public KONDISIUMUM KONDISIUMUM { get; set; }

            [JsonProperty("PENUNJANG")]
            public PENUNJANG PENUNJANG { get; set; }
        }

        [JsonProperty("data")]
        public Data Datum { get; set; }
    }
}
