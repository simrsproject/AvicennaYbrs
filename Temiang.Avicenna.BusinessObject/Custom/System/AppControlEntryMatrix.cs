namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppControlEntryMatrix
    {
        public string ControlUrl
        {
            get { return GetColumn("refToAppControl_ControlUrl").ToString(); }
            set { SetColumn("refToAppControl_ControlUrl", value); }
        }
        public string Description
        {
            get { return GetColumn("refToAppControl_Description").ToString(); }
            set { SetColumn("refToAppControl_Description", value); }
        }
    }
}
