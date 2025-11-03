using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeePosition
    {
        public string PositionName
        {
            get { return GetColumn("refToPosition_PositionName").ToString(); }
            set { SetColumn("refToPosition_PositionName", value); }
        }

        public short? CoorporateGradeLevel
        {
            get {
                var o = GetColumn("refToCoorporateGrade_Level");
                if (o is DBNull)
                {
                    return new short?();
                }
                else {
                    return Convert.ToInt16(o);
                }
                }
            set {
                if (value == null)
                {
                    SetColumn("refToCoorporateGrade_Level", DBNull.Value);
                }
                else {
                    SetColumn("refToCoorporateGrade_Level", value);
                }
            }
        }

    }
}
