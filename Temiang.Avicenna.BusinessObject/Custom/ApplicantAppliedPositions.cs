using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ApplicantAppliedPositions
    {
        public string PositionName
        {
            get { return GetColumn("refTo_PositionName").ToString(); }
            set { SetColumn("refTo_PositionName", value); }
        }
    }
}
