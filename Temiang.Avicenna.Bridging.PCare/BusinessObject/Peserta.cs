using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.Bridging.PCare.Common;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class Peserta
    {
        [JsonProperty("noKartu")]
        public string NoKartu { get; set; }

        [JsonProperty("nama")]
        public string Nama { get; set; }

        [JsonProperty("hubunganKeluarga")]
        public string HubunganKeluarga { get; set; }

        [JsonProperty("sex")]
        public string Sex { get; set; }

        [JsonProperty("tglLahir")]
        public string TglLahir { get; set; }

        [JsonProperty("tglMulaiAktif")]
        public string TglMulaiAktif { get; set; }

        [JsonProperty("tglAkhirBerlaku")]
        public string TglAkhirBerlaku { get; set; }

        [JsonProperty("kdProviderPst")]
        public Provider KdProviderPst { get; set; }

        [JsonProperty("kdProviderGigi")]
        public Provider KdProviderGigi { get; set; }

        [JsonProperty("jnsKelas")]
        public Jenis JnsKelas { get; set; }

        [JsonProperty("jnsPeserta")]
        public Jenis JnsPeserta { get; set; }

        [JsonProperty("golDarah")]
        public string GolDarah { get; set; }

        [JsonProperty("noHP")]
        public string NoHP { get; set; }

        [JsonProperty("noKTP")]
        public string NoKTP { get; set; }

        [JsonProperty("aktif")]
        public bool Aktif { get; set; }

        [JsonProperty("ketAktif")]
        public string KetAktif { get; set; }

        [JsonProperty("asuransi")]
        public Asuransi Asuransi { get; set; }


        public PesertaGet SaveToLocalDataBase(string id, string byMethod = "bpjsno")
        {
            var bridgingUtils = new Utils();

            PesertaGet result;
            if (byMethod.Equals("nik"))
                result = bridgingUtils.PesertaByNik(id);
            else
                result = bridgingUtils.Peserta(id);

            if (result == null) return null;
            if (result.IsOk)
            {
                var peserta = result.Response;
                var ent = new BpjsPeserta();
                ent.LoadByPrimaryKey(peserta.NoKartu);
                ent.NoKartu = peserta.NoKartu;
                ent.Nama = peserta.Nama;
                ent.HubunganKeluarga = peserta.HubunganKeluarga;
                ent.Sex = peserta.Sex;

                if (peserta.TglLahir == null)
                    ent.str.TglLahir = peserta.TglLahir;
                else
                    ent.TglLahir = DateTime.ParseExact(peserta.TglLahir, Constant.DateFormatPCare, null);

                if (peserta.TglMulaiAktif == null)
                    ent.str.TglMulaiAktif = peserta.TglMulaiAktif;
                else
                    ent.TglMulaiAktif = DateTime.ParseExact(peserta.TglMulaiAktif, Constant.DateFormatPCare, null);

                if (peserta.TglAkhirBerlaku == null)
                    ent.str.TglAkhirBerlaku = peserta.TglAkhirBerlaku;
                else
                    ent.TglAkhirBerlaku = DateTime.ParseExact(peserta.TglAkhirBerlaku, Constant.DateFormatPCare, null);

                ent.str.KdProviderPst_kdProvider = peserta.KdProviderPst.KdProvider;
                ent.KdProviderPst_nmProvider = peserta.KdProviderPst.NmProvider;
                ent.KdProviderGigi_kdProvider = peserta.KdProviderGigi.KdProvider;
                ent.KdProviderGigi_nmProvider = peserta.KdProviderGigi.NmProvider;
                ent.JnsKelas_kode = peserta.JnsKelas.Kode;
                ent.JnsKelas_nama = peserta.JnsKelas.Nama;
                ent.JnsPeserta_kode = peserta.JnsPeserta.Kode;
                ent.JnsPeserta_nama = peserta.JnsPeserta.Nama;
                ent.GolDarah = peserta.GolDarah;
                ent.NoHP = peserta.NoHP;
                ent.NoKTP = peserta.NoKTP;
                ent.Aktif = peserta.Aktif;
                ent.KetAktif = peserta.KetAktif;
                ent.Asuransi_kdAsuransi = peserta.Asuransi.KdAsuransi;
                ent.Asuransi_nmAsuransi = peserta.Asuransi.NmAsuransi;
                ent.Asuransi_noAsuransi = peserta.Asuransi.NoAsuransi;
                ent.Save();
                return result;
            }

            // Save
            return result;
        }
    }

}
