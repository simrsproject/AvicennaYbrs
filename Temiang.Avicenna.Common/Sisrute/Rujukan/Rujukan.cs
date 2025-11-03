using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.Common.Sisrute.Rujukan
{
    public class SetRujukan
    {
        public class PASIEN
        {
            public string NORM { get; set; }
            public string NIK { get; set; }
            public string NO_KARTU_JKN { get; set; }
            public string NAMA { get; set; }
            public string JENIS_KELAMIN { get; set; }
            public string TANGGAL_LAHIR { get; set; }
            public string TEMPAT_LAHIR { get; set; }
            public string ALAMAT { get; set; }
            public string KONTAK { get; set; }
        }

        public class DOKTER
        {
            public string NIK { get; set; }
            public string NAMA { get; set; }
        }

        public class PETUGAS
        {
            public string NIK { get; set; }
            public string NAMA { get; set; }
        }

        public class RUJUKAN
        {
            public string JENIS_RUJUKAN { get; set; }
            public string TANGGAL { get; set; }
            public string FASKES_TUJUAN { get; set; }
            public string ALASAN { get; set; }
            public string ALASAN_LAINNYA { get; set; }
            public string DIAGNOSA { get; set; }
            public DOKTER DOKTER { get; set; }
            public PETUGAS PETUGAS { get; set; }
        }

        public class KONDISIUMUM
        {
            public string ANAMNESIS_DAN_PEMERIKSAAN_FISIK { get; set; }
            public string KESADARAN { get; set; }
            public string TEKANAN_DARAH { get; set; }
            public string FREKUENSI_NADI { get; set; }
            public string SUHU { get; set; }
            public string PERNAPASAN { get; set; }
            public string KEADAAN_UMUM { get; set; }
            public string NYERI { get; set; }
            public string ALERGI { get; set; }
        }

        public class PENUNJANG
        {
            public string LABORATORIUM { get; set; }
            public string RADIOLOGI { get; set; }
            public string TERAPI_ATAU_TINDAKAN { get; set; }
        }

        public class RootObject
        {
            public PASIEN PASIEN { get; set; }
            public RUJUKAN RUJUKAN { get; set; }
            public KONDISIUMUM KONDISI_UMUM { get; set; }
            public PENUNJANG PENUNJANG { get; set; }
        }
    }

    public class JawabRujukan
    {
        public class RootObject
        {
            public string DITERIMA { get; set; }
            public string KETERANGAN { get; set; }
            public SetRujukan.PETUGAS PETUGAS { get; set; }
        }
    }

    public class BatalRujukan
    {
        public class RootObject
        {
            public SetRujukan.PETUGAS PETUGAS { get; set; }
        }
    }
}
