using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PositionFunctionalArea
    {
        public string PositionFunctionalAreaName
        {
            get { return GetColumn("refToHR_PositionFunctionalAreaName").ToString(); }
            set { SetColumn("refToHR_PositionFunctionalAreaName", value); }
        }
        
    }
}
