using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PositionPhysical
    {
        public string PhysicalCharacteristicName
        {
            get { return GetColumn("refToCharacteristicName_PositionPhysical").ToString(); }
            set { SetColumn("refToCharacteristicName_PositionPhysical", value); }
        }

        public string OperandTypeName
        {
            get { return GetColumn("refToOperandTypeName_PositionPhysical").ToString(); }
            set { SetColumn("refToOperandTypeName_PositionPhysical", value); }
        }

        public string MeasurementName 
        {
            get { return GetColumn("refToMeasurementName_PositionPhysical").ToString(); }
            set { SetColumn("refToMeasurementName_PositionPhysical", value); }
        }
    }
}
