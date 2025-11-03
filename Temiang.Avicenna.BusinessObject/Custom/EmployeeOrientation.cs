namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeOrientation
    {
        public string OrientationDuration
        {
            get { return GetColumn("refTo_OrientationDuration").ToString(); }
            set { SetColumn("refTo_OrientationDuration", value); }
        }

        public bool? IsParticularOrientation
        {
            get { return (bool?)GetColumn("refTo_IsParticularOrientation"); }
            set { SetColumn("refTo_IsParticularOrientation", value); }
        }
    }
}
