using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.Common.BPJS.VClaim
{
    public class Enum
    {
        public sealed class JenisFaskes
        {
            private readonly String name;
            private readonly int value;

            public static readonly JenisFaskes Faskes_1 = new JenisFaskes(1, "1");
            public static readonly JenisFaskes RS = new JenisFaskes(2, "2");

            private JenisFaskes(int value, String name)
            {
                this.name = name;
                this.value = value;
            }

            public override String ToString()
            {
                return name;
            }
        }

        public sealed class SearchPeserta
        {
            private readonly String name;
            private readonly int value;

            public static readonly SearchPeserta NoPeserta = new SearchPeserta(1, "nokartu");
            public static readonly SearchPeserta NIK = new SearchPeserta(2, "nik");

            private SearchPeserta(int value, String name)
            {
                this.name = name;
                this.value = value;
            }

            public override String ToString()
            {
                return name;
            }
        }

        public sealed class ApproveType
        {
            private readonly String name;
            private readonly int value;

            public static readonly ApproveType Pengajuan = new ApproveType(1, "pengajuanSEP");
            public static readonly ApproveType Approval = new ApproveType(2, "aprovalSEP");

            private ApproveType(int value, String name)
            {
                this.name = name;
                this.value = value;
            }

            public override String ToString()
            {
                return name;
            }
        }

        public sealed class JenisPelayanan
        {
            private readonly String name;
            private readonly int value;

            public static readonly JenisPelayanan Inap = new JenisPelayanan(1, "1");
            public static readonly JenisPelayanan Jalan = new JenisPelayanan(2, "2");

            private JenisPelayanan(int value, String name)
            {
                this.name = name;
                this.value = value;
            }

            public override String ToString()
            {
                return name;
            }
        }

        public sealed class StatusKlaim
        {
            private readonly String name;
            private readonly int value;

            public static readonly StatusKlaim Proses = new StatusKlaim(1, "1");
            public static readonly StatusKlaim Pending = new StatusKlaim(2, "2");
            public static readonly StatusKlaim Klaim = new StatusKlaim(3, "3");

            private StatusKlaim(int value, String name)
            {
                this.name = name;
                this.value = value;
            }

            public override String ToString()
            {
                return name;
            }
        }

        public sealed class SearchRujukan
        {
            private readonly String name;
            private readonly int value;

            public static readonly SearchRujukan NoRujukan = new SearchRujukan(1, "1");
            public static readonly SearchRujukan NoPeserta = new SearchRujukan(2, "2");

            private SearchRujukan(int value, String name)
            {
                this.name = name;
                this.value = value;
            }

            public override String ToString()
            {
                return name;
            }
        }

        public sealed class FilterRencanaKontrol
        {
            private readonly String name;
            private readonly int value;

            public static readonly FilterRencanaKontrol TanggalEntry = new FilterRencanaKontrol(1, "1");
            public static readonly FilterRencanaKontrol TanggalRencanaKontrol = new FilterRencanaKontrol(2, "2");

            private FilterRencanaKontrol(int value, String name)
            {
                this.name = name;
                this.value = value;
            }

            public override String ToString()
            {
                return name;
            }
        }

        public sealed class JenisKontrol
        {
            private readonly String name;
            private readonly int value;

            public static readonly JenisKontrol SPRI = new JenisKontrol(1, "1");
            public static readonly JenisKontrol Kontrol = new JenisKontrol(2, "2");

            private JenisKontrol(int value, String name)
            {
                this.name = name;
                this.value = value;
            }

            public override String ToString()
            {
                return name;
            }
        }

        public sealed class JenisRujukan
        {
            private readonly String name;
            private readonly int value;

            public static readonly JenisRujukan FKTP = new JenisRujukan(1, "1");
            public static readonly JenisRujukan FKTRL = new JenisRujukan(2, "2");

            private JenisRujukan(int value, String name)
            {
                this.name = name;
                this.value = value;
            }

            public override String ToString()
            {
                return name;
            }
        }

        public sealed class Identitas
        {
            private readonly String name;
            private readonly int value;

            public static readonly Identitas NIK = new Identitas(1, "nik");
            public static readonly Identitas NOKA = new Identitas(2, "noka");

            private Identitas(int value, String name)
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
