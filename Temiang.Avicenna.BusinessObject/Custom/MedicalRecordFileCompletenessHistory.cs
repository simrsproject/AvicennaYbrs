namespace Temiang.Avicenna.BusinessObject
{
    public partial class MedicalRecordFileCompletenessHistory
    {
        public string SubmitBy
        {
            get { return GetColumn("refToUser_SubmitBy").ToString(); }
            set { SetColumn("refToUser_SubmitBy", value); }
        }

        public string ReturnBy
        {
            get { return GetColumn("refToUser_ReturnBy").ToString(); }
            set { SetColumn("refToUser_ReturnBy", value); }
        }
    }
}
