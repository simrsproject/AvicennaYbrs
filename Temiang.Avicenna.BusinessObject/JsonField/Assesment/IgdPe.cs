using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class IgdPe : BaseJsonField
    {
        public string Condition { get; set; }

        private Gcs _consciousness;
        public Gcs Consciousness
        {
            get { return _consciousness ?? (_consciousness = new Gcs()); }
            set { _consciousness = value; }
        }

        public string PshysicalExam { get; set; }
        public string AncillaryExam { get; set; }
        public string VisitType { get; set; }
        public string VisitDiedTime { get; set; }
        public string VisitDoaTime { get; set; }
        public string ReferredAction { get; set; }
        public string ReferredActionEtc { get; set; }
        public string ReferredTo { get; set; }
        public string ReferredIndication { get; set; }
        public string InPatientRoom { get; set; }
        public string InPatientDpjp { get; set; }
        public string DispositionDate { get; set; }
        public string DispositionHour { get; set; }
    }
}
