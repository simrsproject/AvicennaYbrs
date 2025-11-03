namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeTaxStatus
    {
        public string TaxStatusName
        {
            get { return GetColumn("refToAppStdRef_TaxStatusName").ToString(); }
            set { SetColumn("refToAppStdRef_TaxStatusName", value); }
        }

        public bool? IsClosed
        {
            get { return (bool)GetColumn("refToClosingWageTransaction_IsClosed"); }
            set { SetColumn("refToClosingWageTransaction_IsClosed", value); }
        }
    }
}
