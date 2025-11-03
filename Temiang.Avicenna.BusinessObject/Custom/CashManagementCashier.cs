namespace Temiang.Avicenna.BusinessObject
{
    public partial class CashManagementCashier
    {
        public string CashierUserName
        {
            get { return GetColumn("refToAppUser_UserName").ToString(); }
            set { SetColumn("refToAppUser_UserName", value); }
        }
    }
}
