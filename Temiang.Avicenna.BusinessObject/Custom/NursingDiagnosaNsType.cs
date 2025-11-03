namespace Temiang.Avicenna.BusinessObject
{
    public partial class NursingDiagnosaNsType
    {
        #region Private

        #endregion

        #region Public
        public string RefToSRNsTypeName
        {
            get { return GetColumn("refToNsType_SRNsTypeName").ToString(); }
            set { SetColumn("refToNsType_SRNsTypeName", value); }
        }
        #endregion
    }
}
