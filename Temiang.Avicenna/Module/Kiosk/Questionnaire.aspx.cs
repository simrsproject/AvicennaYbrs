using System;
using System.Web.UI;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Kiosk
{
    public partial class Questionnaire : Page //BasePageBootstrap
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = "";
        }

        protected void Page_Init(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected string GetScriptQuesionnaireList() {
            var qfColl = new QuestionFormCollection();
            qfColl.Query.Where(qfColl.Query.SRQuestionFormType == "QSNR", qfColl.Query.IsActive == true);

            if (qfColl.LoadAll())
            {
                var arr = qfColl.Select(x => string.Format("SetLeftMenu(\"{0}\", \"LoadPageQuestionnaire('{1}','{0}','')\", \"{2}\");", 
                    x.QuestionFormName, x.QuestionFormID, IcoStyle(x.QuestionFormID))).ToArray();
                return string.Join("", arr);
            }
            else {
                return string.Empty;
            }
        }

        private string IcoStyle(string FormID) { 
            switch (FormID)
            {
                case "QSNRD": {
                        return "fas fa-ambulance";
                    }
                case "QSNRI": {
                        return "fas fa-bed";
                    }
                case "QSNRJ": {

                        return "fas fa-stethoscope";
                    }
                default: {
                        return "fas fa-folder-open";
                    }
            }
        }
    }
}