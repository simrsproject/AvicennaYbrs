namespace Temiang.Avicenna.BusinessObject
{
    public partial class CredentialQuestionnaireItem
    {
        public string CredentialQuestionLevelName
        {
            get { return GetColumn("refToAppStdRefItem_CredentialQuestionLevel").ToString(); }
            set { SetColumn("refToAppStdRefItem_CredentialQuestionLevel", value); }
        }
        public string CredentialActionTypeName
        {
            get { return GetColumn("refToAppStdRefItem_CredentialActionType").ToString(); }
            set { SetColumn("refToAppStdRefItem_CredentialActionType", value); }
        }
    }
}
