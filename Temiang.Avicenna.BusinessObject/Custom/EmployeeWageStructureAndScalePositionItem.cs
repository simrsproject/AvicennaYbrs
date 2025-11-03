namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeWageStructureAndScalePositionItem
    {
        public string WageStructureAndScaleTypeName
        {
            get { return GetColumn("refToStdRef_WageStructureAndScaleType").ToString(); }
            set { SetColumn("refToStdRef_WageStructureAndScaleType", value); }
        }

        public string WageStructureAndScaleName
        {
            get { return GetColumn("refToWageStructureAndScale_WageStructureAndScaleName").ToString(); }
            set { SetColumn("refToWageStructureAndScale_WageStructureAndScaleName", value); }
        }

        public string WageStructureAndScaleItemName
        {
            get { return GetColumn("refToStdRef_WageStructureAndScaleItem").ToString(); }
            set { SetColumn("refToStdRef_WageStructureAndScaleItem", value); }
        }
    }
}
