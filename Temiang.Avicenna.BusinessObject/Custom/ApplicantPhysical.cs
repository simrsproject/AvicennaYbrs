using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ApplicantPhysical
    {
        public string PhysicalCharacteristicName
        {
            get { return GetColumn("refToPhysicalCharacteristic_PersonalPhysical").ToString(); }
            set { SetColumn("refToPhysicalCharacteristic_PersonalPhysical", value); }
        }

        public string MeasurementCodeName
        {
            get { return GetColumn("refToMeasurementCode_PersonalPhysical").ToString(); }
            set { SetColumn("refToMeasurementCode_PersonalPhysical", value); }
        }
    }
}
