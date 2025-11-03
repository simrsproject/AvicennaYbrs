using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.Common.Inacbg.v51.Patient
{
    public class Update
    {
        public class Metadata
        {
            public string nomor_rm { get; set; }
        }

        public class Data
        {
            public string nomor_kartu { get; set; }
            public string nomor_rm { get; set; }
            public string nama_pasien { get; set; }
            public string tgl_lahir { get; set; }
            public string gender { get; set; }
        }

        public class RootObject
        {
            public Metadata metadata { get; set; }
            public Data data { get; set; }
        }
    }

    public class Delete
    {
        public class Data
        {
            public string nomor_rm { get; set; }
            public string coder_nik { get; set; }
        }

        public class RootObject
        {
            public Metadata metadata { get; set; }
            public Data data { get; set; }
        }
    }
}
