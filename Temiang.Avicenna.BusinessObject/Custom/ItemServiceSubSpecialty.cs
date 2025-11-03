namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemServiceSubSpecialty
    {
        public string SubSpecialtyName
        {
            get { return GetColumn("refToSubSpecialty_SubSpecialtyName").ToString(); }
            set { SetColumn("refToSubSpecialty_SubSpecialtyName", value); }
        }
    }
}
