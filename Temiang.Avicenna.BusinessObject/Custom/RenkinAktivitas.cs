namespace Temiang.Avicenna.BusinessObject
{
    public partial class RenkinAktivitas
    {
        public string RenkinAktivitasStatusName
        {
            get { return GetColumn("refTo_RenkinAktivitasStatusName").ToString(); }
            set { SetColumn("refTo_RenkinAktivitasStatusName", value); }
        }
    }
}
