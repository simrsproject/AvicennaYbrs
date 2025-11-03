namespace Temiang.Avicenna.BusinessObject
{
    public partial class WageStructureAndScaleItem
    {
        public string WageStructureAndScaleItemName
        {
            get { return GetColumn("refToStdRefItem_WageStructureAndScaleItem").ToString(); }
            set { SetColumn("refToStdRefItem_WageStructureAndScaleItem", value); }
        }
    }
}
