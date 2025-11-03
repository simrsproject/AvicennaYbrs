using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class EecpPe : BaseJsonField
    {
        private EecpCheckingType _checkingType;
        public EecpCheckingType CheckingType
        {
            get { return _checkingType ?? (_checkingType = new EecpCheckingType()); }
            set { _checkingType = value; }
        }

        private EecpIndication _indication;
        public EecpIndication Indication
        {
            get { return _indication ?? (_indication = new EecpIndication()); }
            set { _indication = value; }
        }

        private EecpContraindication _contraindication;
        public EecpContraindication Contraindication
        {
            get { return _contraindication ?? (_contraindication = new EecpContraindication()); }
            set { _contraindication = value; }
        }

        public string DrugCurrentConsumed { get; set; }

        public class EecpCheckingType
        {
            public bool Ekg { get; set; }
            public bool Echocardiography { get; set; }
            public int EchocardiographyEF { get; set; }
            public bool Duplex { get; set; }
            public bool UsgAbdomen { get; set; }
            public bool Treadmill { get; set; }
            public bool Angiografi { get; set; }
            public bool CTScan { get; set; }
        }

        public class EecpIndication
        {
            public bool Refractory { get; set; }
            public bool HeartFailure { get; set; }
            public string EtcIndication { get; set; }
        }

        public class EecpContraindication
        {
            public bool Regurgitation { get; set; }
            public bool Aortic { get; set; }
            public bool Hypertension { get; set; }
            public bool Thromboflebitis { get; set; }
            public bool Peripheral { get; set; }
            public bool Arrhythmia { get; set; }
            public bool Hemorrhagic { get; set; }
            public bool Pregnancy { get; set; }
            public bool TumorAbdominal { get; set; }
        }
        
    }

}
