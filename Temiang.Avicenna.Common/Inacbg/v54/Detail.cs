using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Temiang.Avicenna.Common.Inacbg.v54.Detail
{

    public class Metadata : v51.Detail.Metadata
    {

    }

    public class Covid19PenunjangPengurang
    {
        public string lab_asam_laktat { get; set; }

        public string lab_procalcitonin { get; set; }

        public string lab_crp { get; set; }

        public string lab_kultur { get; set; }

        public string lab_d_dimer { get; set; }

        public string lab_pt { get; set; }

        public string lab_aptt { get; set; }

        public string lab_waktu_pendarahan { get; set; }

        public string lab_anti_hiv { get; set; }

        public string lab_analisa_gas { get; set; }

        public string lab_albumin { get; set; }

        public string rad_thorax_ap_pa { get; set; }
    }

    public class TarifRs : v51.Detail.TarifRs
    {
        public string obat_kronis { get; set; }

        public string obat_kemoterapi { get; set; }
    }

    public class Data : v51.Detail.Data
    {

        public TarifRs tarif_rs { get; set; }

        public string pemulasaraan_jenazah { get; set; }

        public string kantong_jenazah { get; set; }

        public string peti_jenazah { get; set; }

        public string plastik_erat { get; set; }

        public string desinfektan_jenazah { get; set; }

        public string mobil_jenazah { get; set; }

        public string desinfektan_mobil_jenazah { get; set; }

        public string covid19_status_cd { get; set; }

        public string diagnosa_inagrouper { get; set; }

        public string procedure_inagrouper { get; set; }

        public string nomor_kartu_t { get; set; }

        public string episodes { get; set; }

        public string covid19_cc_ind { get; set; }

        public string covid19_rs_darurat_ind { get; set; }

        public string covid19_co_insidense_ind { get; set; }

        public Covid19PenunjangPengurang covid19_penunjang_pengurang { get; set; }

        public string terapi_konvalesen { get; set; }

        public string akses_naat { get; set; }

        public string isoman_ind { get; set; }

        public string bayi_lahir_status_cd { get; set; }
    }

    public class RootObject : v51.Detail.RootObject
    {

    }

    public class Response : v51.Detail.Response
    {

    }
}
