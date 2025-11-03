using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.Common.BPJS.Apotek
{
    public class Enum
    {
        public sealed class JenisObat
        {
            private readonly String name;
            private readonly int value;

            public static readonly JenisObat Semua = new JenisObat(0, "0");
            public static readonly JenisObat Obat_PRB = new JenisObat(1, "1");
            public static readonly JenisObat Obat_Kronis_Blm_Stabil = new JenisObat(2, "2");
            public static readonly JenisObat Obat_Kemoterapi = new JenisObat(3, "3");

            private JenisObat(int value, String name)
            {
                this.name = name;
                this.value = value;
            }

            public override String ToString()
            {
                return name;
            }
        }
        public sealed class StatusVerif
        {
            private readonly String name;
            private readonly int value;

            public static readonly StatusVerif Belum_diverifikasi = new StatusVerif(0, "0");
            public static readonly StatusVerif Sudah_Verifikasi = new StatusVerif(1, "1");

            private StatusVerif(int value, String name)
            {
                this.name = name;
                this.value = value;
            }

            public override String ToString()
            {
                return name;
            }
        }
    }
}