using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Temiang.Avicenna.Bridging.Antrean.ParameterClass
{
    public class NoAntreanParam
    {
        //{
        //    "nomorkartu": "0000000000123",
        //    "nik": "3506141308950002",
        //    "notelp": "081123456778",
        //    "tanggalperiksa": "2019-12-11",
        //    "kodepoli": "001",
        //    "nomorreferensi": "0001R0040116A000001",
        //    "jenisreferensi": 1,
        //    "jenisrequest": 2,
        //    "polieksekutif": 0
        //}
        [JsonProperty("nomorkartu")]
        public string Nomorkartu { get; set; }

        [JsonProperty("nik")]
        public string Nik { get; set; }

        [JsonProperty("notelp")]
        public string NoTelp { get; set; }

        [JsonProperty("tanggalperiksa")]
        public string TanggalPeriksa { get; set; }

        [JsonProperty("kodepoli")]
        public string KodePoli { get; set; }

        [JsonProperty("nomorreferensi")]
        public string NomorReferensi { get; set; }

        [JsonProperty("jenisreferensi")]
        public int JenisReferensi { get; set; }

        [JsonProperty("jenisrequest")]
        public int JenisRequest { get; set; }

        [JsonProperty("polieksekutif")]
        public int PoliEksekutif { get; set; }

    }

    public class StatusAntreanParam
    {
        [JsonProperty("kodepoli")]
        public string kodepoli { get; set; }

        [JsonProperty("kodedokter")]
        public string kodedokter { get; set; }

        [JsonProperty("tanggalperiksa")]
        public string tanggalperiksa { get; set; }

        [JsonProperty("jampraktek")]
        public string jampraktek { get; set; }
    }

    public class BatalAntreanParam
    {
        [JsonProperty("kodebooking")]
        public string kodebooking { get; set; }

        [JsonProperty("keterangan")]
        public string keterangan { get; set; }
    }

    public class CheckInAntreanParam
    {
        [JsonProperty("kodebooking")]
        public string kodebooking { get; set; }

        [JsonProperty("waktu")]
        public string waktu { get; set; }
    }

    public class PasienBaruAntrean
    {
        [JsonProperty("nomorkartu")]
        public string Nomorkartu;

        [JsonProperty("nik")]
        public string Nik;

        [JsonProperty("nomorkk")]
        public string Nomorkk;

        [JsonProperty("nama")]
        public string Nama;

        [JsonProperty("jeniskelamin")]
        public string Jeniskelamin;

        [JsonProperty("tanggallahir")]
        public string Tanggallahir;

        [JsonProperty("nohp")]
        public string Nohp;

        [JsonProperty("alamat")]
        public string Alamat;

        [JsonProperty("kodeprop")]
        public string Kodeprop;

        [JsonProperty("namaprop")]
        public string Namaprop;

        [JsonProperty("kodedati2")]
        public string Kodedati2;

        [JsonProperty("namadati2")]
        public string Namadati2;

        [JsonProperty("kodekec")]
        public string Kodekec;

        [JsonProperty("namakec")]
        public string Namakec;

        [JsonProperty("kodekel")]
        public string Kodekel;

        [JsonProperty("namakel")]
        public string Namakel;

        [JsonProperty("rw")]
        public string Rw;

        [JsonProperty("rt")]
        public string Rt;
    }

    public class SisaAntreanParam
    {
        [JsonProperty("kodebooking")]
        public string kodebooking { get; set; }
    }

    public class GetNoAntreanParam
    {
        [JsonProperty("nomorkartu")]
        public string Nomorkartu;

        [JsonProperty("nik")]
        public string Nik;

        [JsonProperty("nohp")]
        public string Nohp;

        [JsonProperty("kodepoli")]
        public string Kodepoli;

        [JsonProperty("norm")]
        public string Norm;

        [JsonProperty("tanggalperiksa")]
        public string Tanggalperiksa;

        [JsonProperty("kodedokter")]
        public int Kodedokter;

        [JsonProperty("jampraktek")]
        public string Jampraktek;

        [JsonProperty("jeniskunjungan")]
        public int Jeniskunjungan;

        [JsonProperty("nomorreferensi")]
        public string Nomorreferensi;
    }
}