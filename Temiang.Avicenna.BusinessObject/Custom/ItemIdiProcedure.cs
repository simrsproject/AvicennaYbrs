namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemIdiProcedure
    {
        public string ProcedureName
        {
            get { return GetColumn("refToProcedure_ProcedureName").ToString(); }
            set { SetColumn("refToProcedure_ProcedureName", value); }
        }
    }
}
