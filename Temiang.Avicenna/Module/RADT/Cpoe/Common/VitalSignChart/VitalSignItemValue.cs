using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public class VitalSignItemValue
    {
        public int No { get; set; }
        public string TransactionNo { get; set; }
        public DateTime Time { get; set; }
        public string VitalSignID { get; set; }
        public string VitalSignID2 { get; set; }
        public int Level { get; set; }
        public Decimal Value { get; set; }
        public Decimal Value2 { get; set; }
        public int TotalScore { get; set; }
        public bool IsExistLevel3 { get; set; }
        public string ValueInString { get; set; }
        public string ValueInString2 { get; set; }
        public string ValueInSelectionLineID { get; set; }
        public string ValueInSelectionLineID2 { get; set; }

        // MEOWS
        public int Level2Count { get; set; }
        public int Level3Count { get; set; }

    }
}