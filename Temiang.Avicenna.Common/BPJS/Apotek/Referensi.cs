using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.BPJS.Apotek.Referensi
{
    public class Obat : Metadata
    {
        public class List
        {
            [JsonProperty("kode")]
            public string Kode;

            [JsonProperty("nama")]
            public string Nama;

            [JsonProperty("harga")]
            public string Harga;
        }

        public class Response
        {
            [JsonProperty("list")]
            public List<List> Obat;
        }

        public class Root
        {
            [JsonProperty("response")]
            public Response Response;

            [JsonProperty("metaData")]
            public Metadata MetaData;
        }
    }

    public class Spesialistik : Metadata
    {
        public class List
        {
            [JsonProperty("kode")]
            public string Kode;

            [JsonProperty("nama")]
            public string Nama;
        }

        public class Response
        {
            [JsonProperty("list")]
            public List<List> List;
        }

        public class Root
        {
            [JsonProperty("response")]
            public Response Response;

            [JsonProperty("metaData")]
            public Metadata MetaData;
        }
    }

    public class Setting : Metadata
    {
        public class Response
        {
            [JsonProperty("kode")]
            public string Kode;

            [JsonProperty("namaapoteker")]
            public string Namaapoteker;

            [JsonProperty("namakepala")]
            public string Namakepala;

            [JsonProperty("jabatankepala")]
            public string Jabatankepala;

            [JsonProperty("nipkepala")]
            public string Nipkepala;

            [JsonProperty("siup")]
            public string Siup;

            [JsonProperty("alamat")]
            public string Alamat;

            [JsonProperty("kota")]
            public string Kota;

            [JsonProperty("namaverifikator")]
            public string Namaverifikator;

            [JsonProperty("nppverifikator")]
            public string Nppverifikator;

            [JsonProperty("namapetugasapotek")]
            public string Namapetugasapotek;

            [JsonProperty("nippetugasapotek")]
            public string Nippetugasapotek;

            [JsonProperty("checkstock")]
            public string Checkstock;
        }

        public class Root
        {
            [JsonProperty("response")]
            public Response Response;

            [JsonProperty("metaData")]
            public Metadata MetaData;
        }
    }

    public class FasilitasKes : Metadata
    {
        public class Faskes
        {
            [JsonProperty("kode")]
            public string Kode;

            [JsonProperty("nama")]
            public string Nama;
        }

        public class Response
        {
            [JsonProperty("list")]
            public List<Faskes> List;
        }

        public class Root
        {
            [JsonProperty("response")]
            public Response Response;

            [JsonProperty("metaData")]
            public Metadata MetaData;
        }
    }

    public class Poliklinik
    {
        public class Poli
        {
            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class Response
        {
            [JsonProperty("list")]
            public Poli[] List { get; set; }
        }

        public class Root
        {
            [JsonProperty("response")]
            public Response Response { get; set; }

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }
        }
    }

    public class Dpho : Metadata
    {
        public class List
        {
            [JsonProperty("kodeobat")]
            public string Kodeobat;

            [JsonProperty("namaobat")]
            public string Namaobat;

            [JsonProperty("prb")]
            public string Prb;

            [JsonProperty("kronis")]
            public string Kronis;

            [JsonProperty("kemo")]
            public string Kemo;

            [JsonProperty("harga")]
            public string Harga;

            [JsonProperty("restriksi")]
            public string Restriksi;

            [JsonProperty("generik")]
            public string Generik;

            [JsonProperty("aktif")]
            public string Aktif;
        }

        public class Response
        {
            [JsonProperty("list")]
            public List<List> List;
        }

        public class Root
        {
            [JsonProperty("response")]
            public Response Response;

            [JsonProperty("metaData")]
            public Metadata MetaData;
        }
    }

    public class klaim
    {
        public class listsep
        {
            [JsonProperty("nosepapotek")]
            public string Nosepapotek { get; set; }

            [JsonProperty("nosepaasal")]
            public string Nosepaasal { get; set; }

            [JsonProperty("nokapst")]
            public string Nokartu { get; set; }

            [JsonProperty("nmpst")]
            public string Namapeserta { get; set; }

            [JsonProperty("noresep")]
            public string Noresep { get; set; }

            [JsonProperty("nmjnsobat")]
            public string Jnsobat { get; set; }

            [JsonProperty("tglpelayanan")]
            public string Tglpelayanan { get; set; }

            [JsonProperty("biayapengajuan")]
            public string Biayapengajuan { get; set; }

            [JsonProperty("biayasetujui")]
            public string Biayasetuju { get; set; }
        }

        public class Response
        {
            [JsonProperty("jumlahdata")]
            public string Jumlahdata;

            [JsonProperty("totalbiayapengajuan")]
            public string Totalbiayapengajuan;

            [JsonProperty("totalbiayasetuju")]
            public string Totalbiayasetuju;

            [JsonProperty("listsep")]
            public List<listsep> Listsep;
        }

        public class Root
        {
            [JsonProperty("response")]
            public Response Response;

            [JsonProperty("metaData")]
            public Metadata MetaData;
        }
    }

    public class Sep : Metadata
    {
        public class Response
        {
            [JsonProperty("noSep")]
            public string NoSep;

            [JsonProperty("faskesasalresep")]
            public string Faskesasalresep;

            [JsonProperty("nmfaskesasalresep")]
            public string Nmfaskesasalresep;

            [JsonProperty("nokartu")]
            public string Nokartu;

            [JsonProperty("namapeserta")]
            public string Namapeserta;

            [JsonProperty("jnskelamin")]
            public string Jnskelamin;

            [JsonProperty("tgllhr")]
            public string Tgllhr;

            [JsonProperty("pisat")]
            public string Pisat;

            [JsonProperty("kdjenispeserta")]
            public string Kdjenispeserta;

            [JsonProperty("nmjenispeserta")]
            public string nmjenispeserta;

            [JsonProperty("kodebu")]
            public string Kodebu;

            [JsonProperty("namabu")]
            public string Namabu;

            [JsonProperty("tglsep")]
            public string Tglsep;

            [JsonProperty("tglplgsep")]
            public string Tglplgsep;

            [JsonProperty("jnspelayanan")]
            public string Jnspelayanan;

            [JsonProperty("nmdiag")]
            public string Nmdiag;

            [JsonProperty("poli")]
            public string Poli;

            [JsonProperty("flagprb")]
            public string Flagprb;

            [JsonProperty("namaprb")]
            public string Namaprb;

            [JsonProperty("kodedokter")]
            public string Kodedokter;

            [JsonProperty("namadokter")]
            public string Namadokter;
        }

        public class Root
        {
            [JsonProperty("response")]
            public Response Response;

            [JsonProperty("metaData")]
            public Metadata MetaData;
        }
    }
}
