using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Temiang.Avicenna.BusinessObject.Common.Inacbg.v51.Detail
{

    public class Metadata
    {
        public string nomor_sep { get; set; }
    }

    public class TarifRs
    {
        public string prosedur_non_bedah { get; set; }
        public string prosedur_bedah { get; set; }
        public string konsultasi { get; set; }
        public string tenaga_ahli { get; set; }
        public string keperawatan { get; set; }
        public string penunjang { get; set; }
        public string radiologi { get; set; }
        public string laboratorium { get; set; }
        public string pelayanan_darah { get; set; }
        public string rehabilitasi { get; set; }
        public string kamar { get; set; }
        public string rawat_intensif { get; set; }
        public string obat { get; set; }
        public string alkes { get; set; }
        public string bmhp { get; set; }
        public string sewa_alat { get; set; }
    }

    public class Data
    {
        public string nomor_sep { get; set; }
        public string nomor_kartu { get; set; }
        public string tgl_masuk { get; set; }
        public string tgl_pulang { get; set; }
        public string jenis_rawat { get; set; }
        public string kelas_rawat { get; set; }
        public string adl_sub_acute { get; set; }
        public string adl_chronic { get; set; }
        public string icu_indikator { get; set; }
        public string icu_los { get; set; }
        public string ventilator_hour { get; set; }
        public string upgrade_class_ind { get; set; }
        public string upgrade_class_class { get; set; }
        public string upgrade_class_los { get; set; }
        public string add_payment_pct { get; set; }
        public string birth_weight { get; set; }
        public string discharge_status { get; set; }
        public string diagnosa { get; set; }
        public string procedure { get; set; }
        public TarifRs tarif_rs { get; set; }
        public string tarif_poli_eks { get; set; }
        public string nama_dokter { get; set; }
        public string kode_tarif { get; set; }
        public string payor_id { get; set; }
        public string payor_cd { get; set; }
        public string cob_cd { get; set; }
        public string coder_nik { get; set; }
    }

    public class RootObject
    {
        public Metadata metadata { get; set; }

        public Data data { get; set; }
    }

    public class Response
    {
        public class Metadata : Inacbg.Metadata
        {

            [JsonProperty("error_no")]
            public string ErrorNo { get; set; }
        }

        public class Result
        {

            [JsonProperty("metadata")]
            public Metadata Metadata { get; set; }
        }
    }
}
