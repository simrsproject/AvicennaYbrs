using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.Bridging.PCare.Common
{
    public class Constant
    {
        public const string DateFormatPCare = "dd-MM-yyyy";

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
