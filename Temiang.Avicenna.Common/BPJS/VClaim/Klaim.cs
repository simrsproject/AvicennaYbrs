using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.BPJS.VClaim.Klaim
{
    public class Insert
    {

        public class Poli
        {
            public string poli { get; set; }
        }

        public class Perawatan
        {
            public string ruangRawat { get; set; }
            public string kelasRawat { get; set; }
            public string spesialistik { get; set; }
            public string caraKeluar { get; set; }
            public string kondisiPulang { get; set; }
        }

        public class Diagnosa
        {
            public string kode { get; set; }
            public string level { get; set; }
        }

        public class Procedure
        {
            public string kode { get; set; }
        }

        public class DirujukKe
        {
            public string kodePPK { get; set; }
        }

        public class KontrolKembali
        {
            public string tglKontrol { get; set; }
            public string poli { get; set; }
        }

        public class RencanaTL
        {
            public string tindakLanjut { get; set; }
            public DirujukKe dirujukKe { get; set; }
            public KontrolKembali kontrolKembali { get; set; }
        }

        public class TLpk
        {
            public string noSep { get; set; }
            public string tglMasuk { get; set; }
            public string tglKeluar { get; set; }
            public string jaminan { get; set; }
            public Poli poli { get; set; }
            public Perawatan perawatan { get; set; }
            public List<Diagnosa> diagnosa { get; set; }
            public List<Procedure> procedure { get; set; }
            public RencanaTL rencanaTL { get; set; }
            public string DPJP { get; set; }
            public string user { get; set; }
        }

        public class Request
        {
            public TLpk t_lpk { get; set; }
        }

        public class RootObject
        {
            public Request request { get; set; }
        }

        public class Response : Metadata
        {

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }

            [JsonProperty("response")]
            public string Data { get; set; }
        }
    }

    public class Update : Insert
    {

    }

    public class Delete
    {

        public class TLpk
        {
            public string noSep { get; set; }
        }

        public class Request
        {
            public TLpk t_lpk { get; set; }
        }

        public class RootObject
        {
            public Request request { get; set; }
        }

        public class Response : Metadata
        {

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }

            [JsonProperty("response")]
            public string Data { get; set; }
        }
    }

    public class Search
    {

        public class Dokter
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class DPJP
        {

            [JsonProperty("dokter")]
            public Dokter Dokter { get; set; }
        }

        public class List3
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class List2
        {

            [JsonProperty("level")]
            public string Level { get; set; }

            [JsonProperty("list")]
            public List3 List { get; set; }
        }

        public class Diagnosa
        {

            [JsonProperty("list")]
            public List2[] List { get; set; }
        }

        public class CaraKeluar
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class KelasRawat
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class KondisiPulang
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class RuangRawat
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class Spesialistik
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class Perawatan
        {

            [JsonProperty("caraKeluar")]
            public CaraKeluar CaraKeluar { get; set; }

            [JsonProperty("kelasRawat")]
            public KelasRawat KelasRawat { get; set; }

            [JsonProperty("kondisiPulang")]
            public KondisiPulang KondisiPulang { get; set; }

            [JsonProperty("ruangRawat")]
            public RuangRawat RuangRawat { get; set; }

            [JsonProperty("spesialistik")]
            public Spesialistik Spesialistik { get; set; }
        }

        public class Peserta
        {

            [JsonProperty("kelamin")]
            public string Kelamin { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }

            [JsonProperty("noKartu")]
            public string NoKartu { get; set; }

            [JsonProperty("noMR")]
            public string NoMR { get; set; }

            [JsonProperty("tglLahir")]
            public string TglLahir { get; set; }
        }

        public class Poli2
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }
        }

        public class Poli
        {

            [JsonProperty("eksekutif")]
            public string Eksekutif { get; set; }

            [JsonProperty("poli")]
            public Poli2 Poli2 { get; set; }
        }

        public class List5
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class List4
        {

            [JsonProperty("list")]
            public List5 List { get; set; }
        }

        public class Procedure
        {

            [JsonProperty("list")]
            public List4[] List { get; set; }
        }

        public class List
        {

            [JsonProperty("DPJP")]
            public DPJP DPJP { get; set; }

            [JsonProperty("diagnosa")]
            public Diagnosa Diagnosa { get; set; }

            [JsonProperty("jnsPelayanan")]
            public string JnsPelayanan { get; set; }

            [JsonProperty("noSep")]
            public string NoSep { get; set; }

            [JsonProperty("perawatan")]
            public Perawatan Perawatan { get; set; }

            [JsonProperty("peserta")]
            public Peserta Peserta { get; set; }

            [JsonProperty("poli")]
            public Poli Poli { get; set; }

            [JsonProperty("procedure")]
            public Procedure Procedure { get; set; }

            [JsonProperty("rencanaTL")]
            public object RencanaTL { get; set; }

            [JsonProperty("tglKeluar")]
            public string TglKeluar { get; set; }

            [JsonProperty("tglMasuk")]
            public string TglMasuk { get; set; }
        }

        public class Lpk
        {

            [JsonProperty("list")]
            public List[] List { get; set; }
        }

        public class Data
        {

            [JsonProperty("lpk")]
            public Lpk Lpk { get; set; }
        }

        public class Response : Metadata
        {

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }

            [JsonProperty("response")]
            public Data Data { get; set; }
        }
    }
}
