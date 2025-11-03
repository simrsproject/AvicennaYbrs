//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Text;

//namespace Temiang.Avicenna.BusinessObject
//{
//    public partial class ReferInPatient
//    {
//        public override void Save()
//        {
//            // Save RegistrationInfoMedic untuk new di ReferInPatientHistEntry krn di BO belum bisa generate RegistrationInfoMedicID baru 
//            if (this.es.IsModified)
//            {
//                // Anggap sbg isi Answer
//                var ent = new RegistrationInfoMedic();
//                var qr = new RegistrationInfoMedicQuery();
//                qr.Where(qr.RegistrationNo == RegistrationNo, qr.ReferenceNo == this.SequenceNo, qr.SRMedicalNotesInputType == "REF");
//                qr.es.Top = 1;
//                ent.Load(qr);
//                if (!string.IsNullOrEmpty(ent.RegistrationInfoMedicID))
//                {
//                    ent.Info4 = this.Answer;
//                    ent.Save();
//                }

//            }

//            base.Save();
//        }
//    }
//}
