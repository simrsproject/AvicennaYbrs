namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicScheduleDateItem
    {
        public string OperationalTimeName
        {
            get { return GetColumn("refToOperationalTime_OperationalTimeName").ToString(); }
            set { SetColumn("refToOperationalTime_OperationalTimeName", value); }
        }

        public string OperationalTimeBackcolor
        {
            get { return GetColumn("refToOperationalTime_OperationalTimeBackcolor").ToString(); }
            set { SetColumn("refToOperationalTime_OperationalTimeBackcolor", value); }
        }
    }
}
