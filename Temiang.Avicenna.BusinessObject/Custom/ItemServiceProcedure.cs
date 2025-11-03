namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemServiceProcedure
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }
        public string ProcedureName
        {
            get { return GetColumn("refToStdRef_Procedure").ToString(); }
            set { SetColumn("refToStdRef_Procedure", value); }
        }
    }
}
