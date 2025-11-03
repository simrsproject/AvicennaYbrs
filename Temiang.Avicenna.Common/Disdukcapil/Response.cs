using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.Disdukcapil
{
    public class Response
    {
        [Serializable]
        public class Content
        {
            [JsonProperty("NO_KK")]
            public string NOKK;

            [JsonProperty("NIK")]
            public string NIK;

            [JsonProperty("NAMA_LGKP")]
            public string NAMALGKP;

            [JsonProperty("KAB_NAME")]
            public string KABNAME;

            [JsonProperty("AGAMA")]
            public string AGAMA;

            [JsonProperty("NAMA_LGKP_AYAH")]
            public string NAMALGKPAYAH;

            [JsonProperty("NO_RW")]
            public string NORW;

            [JsonProperty("KEC_NAME")]
            public string KECNAME;

            [JsonProperty("JENIS_PKRJN")]
            public string JENISPKRJN;

            [JsonProperty("NO_RT")]
            public string NORT;

            [JsonProperty("ALAMAT")]
            public string ALAMAT;

            [JsonProperty("TMPT_LHR")]
            public string TMPTLHR;

            [JsonProperty("PDDK_AKH")]
            public string PDDKAKH;

            [JsonProperty("STATUS_KAWIN")]
            public string STATUSKAWIN;

            [JsonProperty("NAMA_LGKP_IBU")]
            public string NAMALGKPIBU;

            [JsonProperty("PROP_NAME")]
            public string PROPNAME;

            [JsonProperty("KEL_NAME")]
            public string KELNAME;

            [JsonProperty("JENIS_KLMIN")]
            public string JENISKLMIN;

            [JsonProperty("TGL_LHR")]
            public string TGLLHR;
        }

        public class Root
        {
            [JsonProperty("content")]
            public List<Content> Content;

            [JsonProperty("copyright")]
            public string Copyright;
        }
    }

    public class ResponseTarakan
    {
        [Serializable]
        public class NOAKTALHR
        {
        }

        [Serializable]
        public class Root
        {
            [JsonProperty("NIK")]
            public string NIK;

            [JsonProperty("NO_KK")]
            public string NOKK;

            [JsonProperty("NAMA_LGKP")]
            public string NAMALGKP;

            [JsonProperty("JENIS_KLMIN")]
            public string JENISKLMIN;

            [JsonProperty("TMPT_LHR")]
            public string TMPTLHR;

            [JsonProperty("TGL_LHR")]
            public string TGLLHR;

            [JsonProperty("AKTA_LHR")]
            public string AKTALHR;

            [JsonProperty("NO_AKTA_LHR")]
            public string NOAKTALHR;

            [JsonProperty("DSC_GOL_DRH")]
            public string DSCGOLDRH;

            [JsonProperty("DSC_STAT_KWN")]
            public string DSCSTATKWN;

            [JsonProperty("DSC_STAT_HBKEL")]
            public string DSCSTATHBKEL;

            [JsonProperty("JENIS_PKRJN")]
            public string JENISPKRJN;

            [JsonProperty("DSC_JENIS_PKRJN")]
            public string DSCJENISPKRJN;

            [JsonProperty("NO_PROP")]
            public string NOPROP;

            [JsonProperty("NM_PROP")]
            public string NMPROP;

            [JsonProperty("NO_KAB")]
            public string NOKAB;

            [JsonProperty("NM_KAB")]
            public string NMKAB;

            [JsonProperty("NO_KEC")]
            public string NOKEC;

            [JsonProperty("NM_KEC")]
            public string NMKEC;

            [JsonProperty("NO_KEL")]
            public string NOKEL;

            [JsonProperty("NM_KEL")]
            public string NMKEL;

            [JsonProperty("ALAMAT")]
            public string ALAMAT;

            [JsonProperty("NO_RT")]
            public string NORT;

            [JsonProperty("NO_RW")]
            public string NORW;

            [JsonProperty("UMUR")]
            public string UMUR;

            [JsonProperty("STATUS")]
            public string STATUS;
        }
    }

    public class ResponseKtpReader
    {
        public class Root
        {
            [JsonProperty("type")]
            public string Type;

            [JsonProperty("nik")]
            public string Nik;

            [JsonProperty("namaLengkap")]
            public string NamaLengkap;

            [JsonProperty("jenisKelamin")]
            public string JenisKelamin;

            [JsonProperty("tempatLahir")]
            public string TempatLahir;

            [JsonProperty("tanggalLahir")]
            public string TanggalLahir;

            [JsonProperty("agama")]
            public string Agama;

            [JsonProperty("statusKawin")]
            public string StatusKawin;

            [JsonProperty("jenisPekerjaan")]
            public string JenisPekerjaan;

            [JsonProperty("namaProvinsi")]
            public string NamaProvinsi;

            [JsonProperty("namaKabupaten")]
            public string NamaKabupaten;

            [JsonProperty("namaKecamatan")]
            public string NamaKecamatan;

            [JsonProperty("namaKelurahan")]
            public string NamaKelurahan;

            [JsonProperty("alamat")]
            public string Alamat;

            [JsonProperty("nomorRt")]
            public string NomorRt;

            [JsonProperty("nomorRw")]
            public string NomorRw;

            [JsonProperty("desa")]
            public string Desa;

            [JsonProperty("kodePos")]
            public string KodePos;

            [JsonProperty("golonganDarah")]
            public string GolonganDarah;

            [JsonProperty("statusEktp")]
            public string StatusEktp;

            [JsonProperty("kewarganegaraan")]
            public string Kewarganegaraan;

            [JsonProperty("berlakuHingga")]
            public string BerlakuHingga;

            [JsonProperty("foto")]
            public string Foto;

            [JsonProperty("ttd")]
            public string Ttd;

            [JsonProperty("responseCode")]
            public string ResponseCode;

            [JsonProperty("responseDesc")]
            public string ResponseDesc;

            [JsonProperty("activeCard")]
            public string ActiveCard;

            [JsonProperty("validCard")]
            public string ValidCard;

            [JsonProperty("typeAuth")]
            public string TypeAuth;

            [JsonProperty("fingerAuth")]
            public string FingerAuth;

            [JsonProperty("nameAuth")]
            public string NameAuth;

            [JsonProperty("nikAuth")]
            public string NikAuth;

            [JsonProperty("serialNumber")]
            public string SerialNumber;

            [JsonProperty("fpImage")]
            public string FpImage;
        }
    }
}
