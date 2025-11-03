using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.SirsKemkes.Tuberkulosis
{
    public class Json
    {
        public class Request
        {
            [JsonProperty("id_tb_03")]
            public string IdTb03;

            [JsonProperty("kd_pasien")]
            public string KdPasien;

            [JsonProperty("nik")]
            public string Nik;

            [JsonProperty("jenis_kelamin")]
            public string JenisKelamin;

            [JsonProperty("alamat_lengkap")]
            public string AlamatLengkap;

            [JsonProperty("id_propinsi_faskes")]
            public string IdPropinsiFaskes;

            [JsonProperty("kd_kabupaten_faskes")]
            public string KdKabupatenFaskes;

            [JsonProperty("id_propinsi_pasien")]
            public string IdPropinsiPasien;

            [JsonProperty("kd_kabupaten_pasien")]
            public string KdKabupatenPasien;

            [JsonProperty("kd_fasyankes")]
            public string KdFasyankes;

            [JsonProperty("kode_icd_x")]
            public string KodeIcdX;

            [JsonProperty("tipe_diagnosis")]
            public string TipeDiagnosis;

            [JsonProperty("klasifikasi_lokasi_anatomi")]
            public string KlasifikasiLokasiAnatomi;

            [JsonProperty("klasifikasi_riwayat_pengobatan")]
            public string KlasifikasiRiwayatPengobatan;

            [JsonProperty("tanggal_mulai_pengobatan")]
            public string TanggalMulaiPengobatan;

            [JsonProperty("paduan_oat")]
            public string PaduanOat;

            [JsonProperty("sebelum_pengobatan_hasil_mikroskopis")]
            public string SebelumPengobatanHasilMikroskopis;

            [JsonProperty("sebelum_pengobatan_hasil_tes_cepat")]
            public string SebelumPengobatanHasilTesCepat;

            [JsonProperty("sebelum_pengobatan_hasil_biakan")]
            public string SebelumPengobatanHasilBiakan;

            [JsonProperty("hasil_mikroskopis_bulan_2")]
            public string HasilMikroskopisBulan2;

            [JsonProperty("hasil_mikroskopis_bulan_3")]
            public string HasilMikroskopisBulan3;

            [JsonProperty("hasil_mikroskopis_bulan_5")]
            public string HasilMikroskopisBulan5;

            [JsonProperty("akhir_pengobatan_hasil_mikroskopis")]
            public string AkhirPengobatanHasilMikroskopis;

            [JsonProperty("tanggal_hasil_akhir_pengobatan")]
            public string TanggalHasilAkhirPengobatan;

            [JsonProperty("hasil_akhir_pengobatan")]
            public string HasilAkhirPengobatan;

            [JsonProperty("tgl_lahir")]
            public string TglLahir;

            [JsonProperty("foto_toraks")]
            public string FotoToraks;
        }

        public class ResponseInsert
        {
            [JsonProperty("status")]
            public string Status;

            [JsonProperty("id_tb_03")]
            public string IdTb03;

            [JsonProperty("keterangan")]
            public string Keterangan;
        }

        public class ResponseUpdate
        {
            [JsonProperty("status")]
            public string Status;

            [JsonProperty("keterangan")]
            public string Keterangan;
        }
    }
}