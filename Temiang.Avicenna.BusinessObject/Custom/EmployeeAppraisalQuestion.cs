namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeAppraisalQuestion
    {
        public string QuestionerName
        {
            get { return GetColumn("refAppraisalQuestion_QuestionerName").ToString(); }
            set { SetColumn("refAppraisalQuestion_QuestionerName", value); }
        }

        public string QuestionerNo
        {
            get { return GetColumn("refAppraisalQuestion_QuestionerNo").ToString(); }
            set { SetColumn("refAppraisalQuestion_QuestionerNo", value); }
        }
    }
}
