namespace Temiang.Avicenna.BusinessObject
{
    public partial class RenkinPeriode
    {
        public string RenkinPeriodeStatusName
        {
            get { return GetColumn("refTo_RenkinPeriodeStatusName").ToString(); }
            set { SetColumn("refTo_RenkinPeriodeStatusName", value); }
        }
    }
}
