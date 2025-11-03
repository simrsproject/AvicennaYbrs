namespace Temiang.Avicenna.BusinessObject
{
    public partial class AssetWorkOrderImplementer
    {
        public string UserName
        {
            get { return GetColumn("refToAppUser_UserName").ToString(); }
            set { SetColumn("refToAppUser_UserName", value); }
        }
    }
}
