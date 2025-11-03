namespace Temiang.Avicenna.BusinessObject
{
    public partial class NursingDiagnosaTemplateDetail
    {
        public string QuestionText
        {
            get { return GetColumn("refToQuestion_QuestionText").ToString(); }
            set { SetColumn("refToQuestion_QuestionText", value); }
        }
    }
}
