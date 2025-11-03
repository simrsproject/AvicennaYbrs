using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiang.Avicenna.Module.RADT.PharmaceuticalCare.DrugObservation
{
    public class DrugItem
    {
        public Int32 MedicationReceiveNo { get; set; }
        public string ItemDescription { get; set; }
        public string ConsumeMethod { get; set; }
        public DrugItem(int medicationReceiveNo, string itemDescription, string consumeMethod)
        {
            MedicationReceiveNo = medicationReceiveNo;
            ItemDescription = itemDescription;
            ConsumeMethod = consumeMethod;
        }
    }
}