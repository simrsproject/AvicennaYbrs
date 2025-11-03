using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.BPJS.VClaim.v11.RujukanBalik
{
    public class MetaData
    {
        public bool IsValid { get { return Code == "200"; } }

        [JsonProperty("code")]
        public string Code;

        [JsonProperty("message")]
        public string Message;
    }

    public class Obat
    {
        [JsonProperty("kdObat")]
        public string KdObat { get; set; }

        [JsonProperty("nmObat")]
        public string NmObat { get; set; }

        [JsonProperty("signa1")]
        public string Signa1 { get; set; }

        [JsonProperty("signa2")]
        public string Signa2 { get; set; }

        [JsonProperty("jmlObat")]
        public string JmlObat { get; set; }
    }

    public class Insert
    {
        public class Request
        {
            public class TPrb
            {
                [JsonProperty("noSep")]
                public string NoSep;

                [JsonProperty("noKartu")]
                public string NoKartu;

                [JsonProperty("alamat")]
                public string Alamat;

                [JsonProperty("email")]
                public string Email;

                [JsonProperty("programPRB")]
                public string ProgramPRB;

                [JsonProperty("kodeDPJP")]
                public string KodeDPJP;

                [JsonProperty("keterangan")]
                public string Keterangan;

                [JsonProperty("saran")]
                public string Saran;

                [JsonProperty("user")]
                public string User;

                [JsonProperty("obat")]
                public List<Obat> Obat;
            }

            public class TRequest
            {
                [JsonProperty("t_prb")]
                public TPrb TPrb;
            }

            public class Root
            {
                [JsonProperty("request")]
                public TRequest Request;
            }
        }

        public class Response
        {
            public class AsalFaskes
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
            }

            public class DPJP
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
            }

            public class List
            {
                [JsonProperty("nmObat")]
                public string NmObat;

                [JsonProperty("signa")]
                public string Signa;

                [JsonProperty("jmlObat")]
                public string JmlObat;
            }

            public class Obat
            {
                [JsonProperty("list")]
                public List<List> List;
            }

            public class Peserta
            {
                [JsonProperty("noKartu")]
                public string NoKartu;

                [JsonProperty("nama")]
                public string Nama;

                [JsonProperty("tglLahir")]
                public string TglLahir;

                [JsonProperty("kelamin")]
                public string Kelamin;

                [JsonProperty("noTelepon")]
                public string NoTelepon;

                [JsonProperty("email")]
                public string Email;

                [JsonProperty("alamat")]
                public string Alamat;

                [JsonProperty("asalFaskes")]
                public AsalFaskes AsalFaskes;
            }

            public class ProgramPRB
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
            }

            public class TResponse
            {
                [JsonProperty("noSRB")]
                public string NoSRB;

                [JsonProperty("noSEP")]
                public object NoSEP;

                [JsonProperty("tglSRB")]
                public string TglSRB;

                [JsonProperty("keterangan")]
                public string Keterangan;

                [JsonProperty("saran")]
                public string Saran;

                [JsonProperty("programPRB")]
                public ProgramPRB ProgramPRB;

                [JsonProperty("DPJP")]
                public DPJP DPJP;

                [JsonProperty("peserta")]
                public Peserta Peserta;

                [JsonProperty("obat")]
                public Obat Obat;
            }

            public class Root
            {
                [JsonProperty("metaData")]
                public MetaData MetaData;

                [JsonProperty("response")]
                public TResponse Response;
            }
        }
    }

    public class Update
    {
        public class Request
        {
            public class TPrb
            {
                [JsonProperty("noSrb")]
                public string NoSrb;

                [JsonProperty("noSep")]
                public string NoSep;

                [JsonProperty("alamat")]
                public string Alamat;

                [JsonProperty("email")]
                public string Email;

                [JsonProperty("kodeDPJP")]
                public string KodeDPJP;

                [JsonProperty("keterangan")]
                public string Keterangan;

                [JsonProperty("saran")]
                public string Saran;

                [JsonProperty("user")]
                public string User;

                [JsonProperty("obat")]
                public List<Obat> Obat;
            }

            public class TRequest
            {
                [JsonProperty("t_prb")]
                public TPrb TPrb;
            }

            public class Root
            {
                [JsonProperty("request")]
                public TRequest Request;
            }
        }

        public class Response
        {
            public class Root
            {
                [JsonProperty("metaData")]
                public MetaData MetaData;

                [JsonProperty("response")]
                public string Response;
            }
        }
    }

    public class Delete
    {
        public class Request
        {
            public class TPrb
            {
                [JsonProperty("noSrb")]
                public string NoSrb;

                [JsonProperty("noSep")]
                public string NoSep;

                [JsonProperty("user")]
                public string User;
            }

            public class TRequest
            {
                [JsonProperty("t_prb")]
                public TPrb TPrb;
            }

            public class Root
            {
                [JsonProperty("request")]
                public TRequest Request;
            }
        }

        public class Response
        {
            public class Root
            {
                [JsonProperty("metaData")]
                public MetaData MetaData;

                [JsonProperty("response")]
                public string Response;
            }
        }
    }

    public class Select
    {
        public class Response
        {
            public class DPJP
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
            }

            public class TObat
            {
                [JsonProperty("obat")]
                public List<Obat> Obat;
            }

            public class AsalFaskes
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
            }

            public class Peserta
            {
                [JsonProperty("alamat")]
                public string Alamat;

                [JsonProperty("asalFaskes")]
                public AsalFaskes AsalFaskes;

                [JsonProperty("email")]
                public string Email;

                [JsonProperty("kelamin")]
                public string Kelamin;

                [JsonProperty("nama")]
                public string Nama;

                [JsonProperty("noKartu")]
                public string NoKartu;

                [JsonProperty("noTelepon")]
                public string NoTelepon;

                [JsonProperty("tglLahir")]
                public string TglLahir;
            }

            public class ProgramPRB
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
            }

            public class Prb
            {
                [JsonProperty("DPJP")]
                public DPJP DPJP;

                [JsonProperty("noSEP")]
                public string NoSEP;

                [JsonProperty("noSRB")]
                public string NoSRB;

                [JsonProperty("obat")]
                public TObat Obat;

                [JsonProperty("peserta")]
                public Peserta Peserta;

                [JsonProperty("programPRB")]
                public ProgramPRB ProgramPRB;

                [JsonProperty("keterangan")]
                public string Keterangan;

                [JsonProperty("saran")]
                public string Saran;

                [JsonProperty("tglSRB")]
                public string TglSRB;
            }

            public class TResponse
            {
                [JsonProperty("prb")]
                public Prb Prb;
            }

            public class Root
            {
                [JsonProperty("metaData")]
                public MetaData MetaData;

                [JsonProperty("response")]
                public TResponse Response;
            }
        }

        public class ResponseList
        {
            public class DPJP
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
            }

            public class Peserta
            {
                [JsonProperty("alamat")]
                public string Alamat;

                [JsonProperty("email")]
                public string Email;

                [JsonProperty("nama")]
                public string Nama;

                [JsonProperty("noKartu")]
                public string NoKartu;

                [JsonProperty("noTelepon")]
                public string NoTelepon;
            }

            public class ProgramPRB
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
            }

            public class List
            {
                [JsonProperty("DPJP")]
                public DPJP DPJP;

                [JsonProperty("noSEP")]
                public string NoSEP;

                [JsonProperty("noSRB")]
                public string NoSRB;

                [JsonProperty("peserta")]
                public Peserta Peserta;

                [JsonProperty("programPRB")]
                public ProgramPRB ProgramPRB;

                [JsonProperty("keterangan")]
                public string Keterangan;

                [JsonProperty("saran")]
                public string Saran;

                [JsonProperty("tglSRB")]
                public string TglSRB;
            }

            public class Prb
            {
                [JsonProperty("list")]
                public List<List> List;
            }

            public class TResponse
            {
                [JsonProperty("prb")]
                public Prb Prb;
            }

            public class Root
            {
                [JsonProperty("metaData")]
                public MetaData MetaData;

                [JsonProperty("response")]
                public TResponse Response;
            }
        }
    }
}
