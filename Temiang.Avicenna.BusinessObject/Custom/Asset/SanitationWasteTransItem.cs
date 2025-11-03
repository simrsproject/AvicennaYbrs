namespace Temiang.Avicenna.BusinessObject
{
    public partial class SanitationWasteTransItem
    {
        public string WasteTypeName
        {
            get { return GetColumn("refToStdRef_WasteTypeName").ToString(); }
            set { SetColumn("refToStdRef_WasteTypeName", value); }
        }

        public string ServiceUnitName
        {
            get { return GetColumn("refToUnit_ServiceUnitName").ToString(); }
            set { SetColumn("refToUnit_ServiceUnitName", value); }
        }
    }
}
