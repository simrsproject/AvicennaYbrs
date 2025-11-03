using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.BusinessObject.Common.Inacbg.v58.Detail
{
    public class Metadata : v51.Detail.Metadata
    {

    }

    public class Data : v54.Detail.Data
    {
        public string cara_masuk { get; set; }
        public string use_ind { get; set; }
        public string start_dttm { get; set; }
        public string stop_dttm { get; set; }
        public string upgrade_class_payor { get; set; }
        public string sistole { get; set; }
        public string diastole { get; set; }
        public string dializer_single_use { get; set; }
        public string kantong_darah { get; set; }
        public string menit_1_appearance { get; set; }
        public string menit_1_pulse { get; set; }
        public string menit_1_grimace { get; set; }
        public string menit_1_activity { get; set; }
        public string menit_1_respiration { get; set; }
        public string menit_5_appearance { get; set; }
        public string menit_5_pulse { get; set; }
        public string menit_5_grimace { get; set; }
        public string menit_5_activity { get; set; }
        public string menit_5_respiration { get; set; }
        public string usia_kehamilan { get; set; }
        public string gravida { get; set; }
        public string partus { get; set; }
        public string abortus { get; set; }
        public string onset_kontraksi { get; set; }
        public string delivery { get; set; }
    }

    public class RootObject : v54.Detail.RootObject
    {

    }

    public class Response : v51.Detail.Response
    {

    }
}
