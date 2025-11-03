using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.Bridging.SatuSehat.Common
{
    public class Constant
    {
        public const string DateFormat = "dd/MM/yyyy";

        public enum ReferenceType
        {
            Kesadaran,
            Diagnosa,
            Dokter,
            Obat,
            PoliFktp,
            PoliFktl,
            Provider,
            StatusPulang,
            Tindakan
        }
    }
}
