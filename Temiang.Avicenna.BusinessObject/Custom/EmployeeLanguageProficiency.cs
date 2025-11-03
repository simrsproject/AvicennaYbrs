using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeLanguageProficiency
    {
        public string LanguageName
        {
            get { return GetColumn("refToLanguage").ToString(); }
            set { SetColumn("refToLanguage", value); }
        }

        public string ConversationName
        {
            get { return GetColumn("refToConversation").ToString(); }
            set { SetColumn("refToConversation", value); }
        }

        public string TranslationName
        {
            get { return GetColumn("refToTranslation").ToString(); }
            set { SetColumn("refToTranslation", value); }
        }
    }
}
