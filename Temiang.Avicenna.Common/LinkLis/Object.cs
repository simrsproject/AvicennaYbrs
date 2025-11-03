using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.LinkLis.Object
{
    public class RegistrasiPasien
    {
        public string no_rm { get; set; }
        public string nama { get; set; }
        public string alamat { get; set; }
        public string tgl_lahir { get; set; }
        public string jenis_kelamin { get; set; }
        public string status { get; set; }
    }

    public class RegistrasiPemeriksaan
    {
        public string kode_pemeriksaan { get; set; }
        public string no_rm { get; set; }
        public string id_ruangan { get; set; }
        public string id_dokter { get; set; }
        public string id_analis { get; set; }
        public string id_dokterpk { get; set; }
        public string id_status { get; set; }
    }

    public class ParameterPemeriksaan
    {
        public string kode_pemeriksaan { get; set; }
        public List<ListPemeriksaan> list_pemeriksaan { get; set; }
        public List<ListParameter> list_parameter { get; set; }
        public List<FeedbackId> feedback_id { get; set; }
        public List<FeedbackUrl> feedback_url { get; set; }
    }

    public class ListPemeriksaan
    {
        public string list_pemeriksaan { get; set; }
    }

    public class ListParameter
    {
        public string list_pemeriksaan { get; set; }
        public string list_parameter { get; set; }
    }

    public class FeedbackId
    {
        public string feedback_id { get; set; }
    }

    public class FeedbackUrl
    {
        public string feedback_url { get; set; }
    }
}
