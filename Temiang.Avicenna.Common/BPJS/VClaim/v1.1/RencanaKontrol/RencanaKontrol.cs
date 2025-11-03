using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.BPJS.VClaim.v11.RencanaKontrol
{
    public class MetaData
    {
        public bool IsValid { get { return Code == "200"; } }

        [JsonProperty("code")]
        public string Code;

        [JsonProperty("message")]
        public string Message;
    }

    public class Insert
    {
        public class Request
        {
            public class TRequest
            {
                [JsonProperty("noSEP")]
                public string NoSEP;

                [JsonProperty("kodeDokter")]
                public string KodeDokter;

                [JsonProperty("poliKontrol")]
                public string PoliKontrol;

                [JsonProperty("tglRencanaKontrol")]
                public string TglRencanaKontrol;

                [JsonProperty("user")]
                public string User;
            }

            public class Root
            {
                [JsonProperty("request")]
                public TRequest Request;
            }
        }

        public class Response
        {
            public class TResponse
            {
                [JsonProperty("noSuratKontrol")]
                public string NoSuratKontrol;

                [JsonProperty("tglRencanaKontrol")]
                public string TglRencanaKontrol;

                [JsonProperty("namaDokter")]
                public string NamaDokter;

                [JsonProperty("noKartu")]
                public string NoKartu;

                [JsonProperty("nama")]
                public string Nama;

                [JsonProperty("kelamin")]
                public string Kelamin;

                [JsonProperty("tglLahir")]
                public string TglLahir;
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
            public class TRequest
            {
                [JsonProperty("noSuratKontrol")]
                public string NoSuratKontrol;

                [JsonProperty("noSEP")]
                public string NoSEP;

                [JsonProperty("kodeDokter")]
                public string KodeDokter;

                [JsonProperty("poliKontrol")]
                public string PoliKontrol;

                [JsonProperty("tglRencanaKontrol")]
                public string TglRencanaKontrol;

                [JsonProperty("user")]
                public string User;
            }

            public class Root
            {
                [JsonProperty("request")]
                public TRequest Request;
            }
        }

        public class Response
        {
            public class TResponse
            {
                [JsonProperty("noSuratKontrol")]
                public string NoSuratKontrol;

                [JsonProperty("tglRencanaKontrol")]
                public string TglRencanaKontrol;

                [JsonProperty("namaDokter")]
                public string NamaDokter;

                [JsonProperty("noKartu")]
                public string NoKartu;

                [JsonProperty("nama")]
                public string Nama;

                [JsonProperty("kelamin")]
                public string Kelamin;

                [JsonProperty("tglLahir")]
                public string TglLahir;
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

    public class Delete
    {
        public class Request
        {
            public class TSuratkontrol
            {
                [JsonProperty("noSuratKontrol")]
                public string NoSuratKontrol;

                [JsonProperty("user")]
                public string User;
            }

            public class TRequest
            {
                [JsonProperty("t_suratkontrol")]
                public TSuratkontrol TSuratkontrol;
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
        public class ResposeSep
        {
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

                [JsonProperty("hakKelas")]
                public string HakKelas;
            }

            public class ProvUmum
            {
                [JsonProperty("kdProvider")]
                public string KdProvider;

                [JsonProperty("nmProvider")]
                public string NmProvider;
            }

            public class ProvPerujuk
            {
                [JsonProperty("kdProviderPerujuk")]
                public string KdProviderPerujuk;

                [JsonProperty("nmProviderPerujuk")]
                public string NmProviderPerujuk;

                [JsonProperty("asalRujukan")]
                public string AsalRujukan;

                [JsonProperty("noRujukan")]
                public string NoRujukan;

                [JsonProperty("tglRujukan")]
                public string TglRujukan;
            }

            public class Response
            {
                [JsonProperty("noSep")]
                public string NoSep;

                [JsonProperty("tglSep")]
                public string TglSep;

                [JsonProperty("jnsPelayanan")]
                public string JnsPelayanan;

                [JsonProperty("poli")]
                public string Poli;

                [JsonProperty("diagnosa")]
                public string Diagnosa;

                [JsonProperty("peserta")]
                public Peserta Peserta;

                [JsonProperty("provUmum")]
                public ProvUmum ProvUmum;

                [JsonProperty("provPerujuk")]
                public ProvPerujuk ProvPerujuk;
            }

            public class Root
            {
                [JsonProperty("metaData")]
                public MetaData MetaData;

                [JsonProperty("response")]
                public Response Response;
            }
        }

        public class ResponseSuratKontrol
        {
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

                [JsonProperty("hakKelas")]
                public string HakKelas;
            }

            public class ProvPerujuk
            {
                [JsonProperty("kdProviderPerujuk")]
                public string KdProviderPerujuk;

                [JsonProperty("nmProviderPerujuk")]
                public string NmProviderPerujuk;

                [JsonProperty("asalRujukan")]
                public string AsalRujukan;

                [JsonProperty("noRujukan")]
                public string NoRujukan;

                [JsonProperty("tglRujukan")]
                public string TglRujukan;
            }

            public class ProvUmum
            {
                [JsonProperty("kdProvider")]
                public string KdProvider;

                [JsonProperty("nmProvider")]
                public string NmProvider;
            }

            public class Response
            {
                [JsonProperty("noSuratKontrol")]
                public string NoSuratKontrol;

                [JsonProperty("tglRencanaKontrol")]
                public string TglRencanaKontrol;

                [JsonProperty("tglTerbit")]
                public string TglTerbit;

                [JsonProperty("jnsKontrol")]
                public string JnsKontrol;

                [JsonProperty("poliTujuan")]
                public string PoliTujuan;

                [JsonProperty("namaPoliTujuan")]
                public string NamaPoliTujuan;

                [JsonProperty("kodeDokter")]
                public string KodeDokter;

                [JsonProperty("namaDokter")]
                public string NamaDokter;

                [JsonProperty("flagKontrol")]
                public string FlagKontrol;

                [JsonProperty("kodeDokterPembuat")]
                public string KodeDokterPembuat;

                [JsonProperty("namaDokterPembuat")]
                public string NamaDokterPembuat;

                [JsonProperty("namaJnsKontrol")]
                public string NamaJnsKontrol;

                [JsonProperty("sep")]
                public Sep Sep;

                [JsonProperty("provUmum")]
                public ProvUmum ProvUmum;

                [JsonProperty("provPerujuk")]
                public ProvPerujuk ProvPerujuk;
            }

            public class Root
            {
                [JsonProperty("metaData")]
                public MetaData MetaData;

                [JsonProperty("response")]
                public Response Response;
            }

            public class Sep
            {
                [JsonProperty("noSep")]
                public string NoSep;

                [JsonProperty("tglSep")]
                public string TglSep;

                [JsonProperty("jnsPelayanan")]
                public string JnsPelayanan;

                [JsonProperty("poli")]
                public string Poli;

                [JsonProperty("diagnosa")]
                public string Diagnosa;

                [JsonProperty("peserta")]
                public Peserta Peserta;

                [JsonProperty("provUmum")]
                public ProvUmum ProvUmum;

                [JsonProperty("provPerujuk")]
                public ProvPerujuk ProvPerujuk;
            }
        }

        public class ResponseSuratKontrolList
        {
            public class List
            {
                [JsonProperty("noSuratKontrol")]
                public string NoSuratKontrol;

                [JsonProperty("jnsPelayanan")]
                public string JnsPelayanan;

                [JsonProperty("jnsKontrol")]
                public string JnsKontrol;

                [JsonProperty("namaJnsKontrol")]
                public string NamaJnsKontrol;

                [JsonProperty("tglRencanaKontrol")]
                public string TglRencanaKontrol;

                [JsonProperty("tglTerbitKontrol")]
                public string TglTerbitKontrol;

                [JsonProperty("noSepAsalKontrol")]
                public string NoSepAsalKontrol;

                [JsonProperty("poliAsal")]
                public string PoliAsal;

                [JsonProperty("namaPoliAsal")]
                public string NamaPoliAsal;

                [JsonProperty("poliTujuan")]
                public string PoliTujuan;

                [JsonProperty("namaPoliTujuan")]
                public string NamaPoliTujuan;

                [JsonProperty("tglSEP")]
                public string TglSEP;

                [JsonProperty("kodeDokter")]
                public string KodeDokter;

                [JsonProperty("namaDokter")]
                public string NamaDokter;

                [JsonProperty("noKartu")]
                public string NoKartu;

                [JsonProperty("nama")]
                public string Nama;

                [JsonProperty("terbitSEP")]
                public string TerbitSEP;
            }

            public class Response
            {
                [JsonProperty("list")]
                public List<List> List;
            }

            public class Root
            {
                [JsonProperty("metaData")]
                public MetaData MetaData;

                [JsonProperty("response")]
                public Response Response;
            }
        }

        public class ResponseSpesialistikList
        {
            public class List
            {
                [JsonProperty("kodePoli")]
                public string KodePoli;

                [JsonProperty("namaPoli")]
                public string NamaPoli;

                [JsonProperty("kapasitas")]
                public string Kapasitas;

                [JsonProperty("jmlRencanaKontroldanRujukan")]
                public string JmlRencanaKontroldanRujukan;

                [JsonProperty("persentase")]
                public string Persentase;
            }

            public class Response
            {
                [JsonProperty("list")]
                public List<List> List;
            }

            public class Root
            {
                [JsonProperty("metaData")]
                public MetaData MetaData;

                [JsonProperty("response")]
                public Response Response;
            }
        }

        public class ResponseJadwalPraktekDokterList
        {
            public class List
            {
                [JsonProperty("kodeDokter")]
                public string KodeDokter;

                [JsonProperty("namaDokter")]
                public string NamaDokter;

                [JsonProperty("jadwalPraktek")]
                public string JadwalPraktek;

                [JsonProperty("kapasitas")]
                public string Kapasitas;
            }

            public class Response
            {
                [JsonProperty("list")]
                public List<List> List;
            }

            public class Root
            {
                [JsonProperty("metaData")]
                public MetaData MetaData;

                [JsonProperty("response")]
                public Response Response;
            }
        }
    }

    public class InsertSpri
    {
        public class Request
        {
            public class TRequest
            {
                [JsonProperty("noKartu")]
                public string NoKartu;

                [JsonProperty("kodeDokter")]
                public string KodeDokter;

                [JsonProperty("poliKontrol")]
                public string PoliKontrol;

                [JsonProperty("tglRencanaKontrol")]
                public string TglRencanaKontrol;

                [JsonProperty("user")]
                public string User;
            }

            public class Root
            {
                [JsonProperty("request")]
                public TRequest Request;
            }
        }

        public class Response
        {
            public class TResponse
            {
                [JsonProperty("noSPRI")]
                public string NoSPRI;

                [JsonProperty("tglRencanaKontrol")]
                public string TglRencanaKontrol;

                [JsonProperty("namaDokter")]
                public string NamaDokter;

                [JsonProperty("noKartu")]
                public string NoKartu;

                [JsonProperty("nama")]
                public string Nama;

                [JsonProperty("kelamin")]
                public string Kelamin;

                [JsonProperty("tglLahir")]
                public string TglLahir;

                [JsonProperty("namaDiagnosa")]
                public object NamaDiagnosa;
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

    public class UpdateSpri
    {
        public class Request
        {
            public class TRequest
            {
                [JsonProperty("noSPRI")]
                public string NoSPRI;

                [JsonProperty("kodeDokter")]
                public string KodeDokter;

                [JsonProperty("poliKontrol")]
                public string PoliKontrol;

                [JsonProperty("tglRencanaKontrol")]
                public string TglRencanaKontrol;

                [JsonProperty("user")]
                public string User;
            }

            public class Root
            {
                [JsonProperty("request")]
                public TRequest Request;
            }
        }

        public class Response
        {
            public class TResponse
            {
                [JsonProperty("noSPRI")]
                public string NoSPRI;

                [JsonProperty("tglRencanaKontrol")]
                public string TglRencanaKontrol;

                [JsonProperty("namaDokter")]
                public string NamaDokter;

                [JsonProperty("noKartu")]
                public string NoKartu;

                [JsonProperty("nama")]
                public string Nama;

                [JsonProperty("kelamin")]
                public string Kelamin;

                [JsonProperty("tglLahir")]
                public string TglLahir;

                [JsonProperty("namaDiagnosa")]
                public object NamaDiagnosa;
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
