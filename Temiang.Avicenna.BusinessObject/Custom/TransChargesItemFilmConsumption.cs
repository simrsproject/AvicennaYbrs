namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransChargesItemFilmConsumption
    {
        public string FilmName
        {
            get { return GetColumn("refToAppStandardReferenceItem_Film").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_Film", value); }
        }
    }
}
