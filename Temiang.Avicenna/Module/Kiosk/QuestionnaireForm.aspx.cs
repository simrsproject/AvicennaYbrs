using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Kiosk
{
    public partial class QuestionnaireForm : System.Web.UI.Page
    {
        private string FormID {
            get {
                return Request.QueryString["qfid"];
            }
        }
        private string TransactionNo {
            get {
                return Request.QueryString["transno"];
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private string ScriptJS = "";
        public string RenderScriptJS() {
            return ScriptJS;
        }
        public string RenderQuestion() {
            var str = "";
            var qgColl = new QuestionGroupCollection();
            qgColl.LoadGroupByFormID(FormID);
            var qColl = new QuestionCollection();
            qColl.LoadByFormID(FormID);

            var qas = qColl.Where(q => (new string[] { "cbo", "cbt", "cb2" }).Contains(q.SRAnswerType.ToLower()))
                .Select(q => q.QuestionAnswerSelectionID).ToList().Distinct().ToList();
            qas.Remove(string.Empty);

            var qaslColl = new QuestionAnswerSelectionLineCollection();
            if (qas.Count > 0)
            {
                qaslColl.Query.Where(qaslColl.Query.QuestionAnswerSelectionID.In(qas));
                qaslColl.LoadAll();
            }

            var phrlColl = new PatientHealthRecordLineCollection();
            if (!string.IsNullOrEmpty(TransactionNo))
            {
                phrlColl.LoadByTransactionNo(TransactionNo);
            }

            foreach (var qg in qgColl) {
                var strq = "";
                var qs = qColl.Where(x => x.QuestionGroupID == qg.QuestionGroupID);
                foreach (var q in qs) {
                    switch (q.SRAnswerType.ToLower()) {
                        case "lbl": {
                                strq += GetLabel(q);
                                break;
                            }
                        case "txt": {
                                strq += GetTXT(q, phrlColl.Where(x => x.QuestionID == q.QuestionID && x.QuestionGroupID == qg.QuestionGroupID).FirstOrDefault());
                                break;
                            }
                        case "mem":
                            {
                                strq += GetMemo(q, phrlColl.Where(x => x.QuestionID == q.QuestionID && x.QuestionGroupID == qg.QuestionGroupID).FirstOrDefault());
                                break;
                            }
                        case "cbo": {
                                if (q.QuestionAnswerSelectionID.Contains("QSNRRPP"))
                                {
                                    strq += GetOptionEmo5(q, qaslColl.Where(x => x.QuestionAnswerSelectionID == q.QuestionAnswerSelectionID).ToArray(),
                                        phrlColl.Where(x => x.QuestionID == q.QuestionID && x.QuestionGroupID == qg.QuestionGroupID).FirstOrDefault());
                                }
                                else
                                {
                                    strq += GetOption(q, qaslColl.Where(x => x.QuestionAnswerSelectionID == q.QuestionAnswerSelectionID).ToArray(),
                                        phrlColl.Where(x => x.QuestionID == q.QuestionID && x.QuestionGroupID == qg.QuestionGroupID).FirstOrDefault());
                                }
                                break;
                            }
                        case "cbt": {
                                strq += GetOptionVertical(q, qaslColl.Where(x => x.QuestionAnswerSelectionID == q.QuestionAnswerSelectionID).ToArray(),
                                        phrlColl.Where(x => x.QuestionID == q.QuestionID && x.QuestionGroupID == qg.QuestionGroupID).FirstOrDefault());
                                break;
                            }
                        case "num": {
                                strq += GetTXTNum(q, phrlColl.Where(x => x.QuestionID == q.QuestionID && x.QuestionGroupID == qg.QuestionGroupID).FirstOrDefault());
                                break;
                            }
                        case "dat": {
                                strq += GetTXTDate(q, phrlColl.Where(x => x.QuestionID == q.QuestionID && x.QuestionGroupID == qg.QuestionGroupID).FirstOrDefault());
                                break;
                            }
                        default: {
                                break;
                            }
                    }
                }
                str += GetGroup(qg.QuestionGroupName, strq);
            }
            return str;
        }

        private string GetGroup(string GroupTitle, string GroupContent) {
            string str = "";
            str += "<div class=\"card card-secondary\">";
            str += "<div class=\"card-header\">";
            str += string.Format("<h3 class=\"card-title\">{0}</h3>", GroupTitle);
            str += "</div>";
            str += string.Format("<div class=\"card-body\">{0}", GroupContent);
            str += "</div>";
            str += "</div>";
            return str;
        }
        private string GetLabel(Question q) {
            string str = "";
            str += "<div class=\"form-group row my-0\">";
            str += string.Format("<label class=\"col-sm-12 col-form-label\">{0}</label>", q.QuestionText);
            str += "</div>";
            return str;
        }
        private string GetTXTDate(Question q, PatientHealthRecordLine phrl)
        {
            string str = "";
            str += "<div class=\"form-group row my-0\">";
            str += string.Format("<label for=\"{0}\" class=\"col-sm-4 col-form-label\">{1}</label>", q.QuestionID, q.QuestionText);
            str += "<div class=\"col-sm-8\">";
            str += "<div class=\"input-group\">";
            str += "<div class=\"input-group-prepend\">";
            str += "<span class=\"input-group-text\"><i class=\"fa fa-calendar\"></i></span>";
            str += "</div>";
            str += string.Format("<input type=\"text\" class=\"form-control {1}\" data-inputmask-alias=\"datetime\" data-inputmask-inputformat=\"mm/dd/yyyy\" data-mask id=\"{0}\" name=\"{0}\" value=\"{2}\">",
                q.QuestionID, (q.IsMandatory ?? true) ? "validate[required]" : "", phrl == null ? "" : phrl.QuestionAnswerText);
            str += "</div>";
            str += "</div>";
            str += "</div>";

            // script
            ScriptJS += "$('#"+ q.QuestionID + "').inputmask('mm/dd/yyyy', { 'placeholder': 'mm/dd/yyyy' });";
            return str;
        }
        private string GetTXTNum(Question q, PatientHealthRecordLine phrl)
        {
            string str = "";
            str += "<div class=\"form-group row my-0\">";
            str += string.Format("<label for=\"{0}\" class=\"col-sm-4 col-form-label\">{1}</label>", q.QuestionID, q.QuestionText);
            str += "<div class=\"col-sm-8\">";
            var validate = string.Empty;
            validate += ((q.IsMandatory ?? true) ? ((validate.Length > 0 ? "," : "") + "required") : "");
            validate += (validate.Length > 0 ? "," : "") + ((q.AnswerDecimalDigit ?? 0) == 0 ? "custom[integer]" : "custom[number]");
            validate += (q.AnswerWidth.HasValue ? (validate.Length > 0 ? "," : "") + string.Format("min[{0}]", q.AnswerWidth.Value.ToString()) : "");
            validate += (q.AnswerWidth2.HasValue ? (validate.Length > 0 ? "," : "") + string.Format("max[{0}]", q.AnswerWidth2.Value.ToString()) : "");
            str += string.Format("<input type=\"text\" class=\"form-control {1}\" id=\"{0}\" name=\"{0}\" value=\"{2}\" placeholder=\"Enter ...\">",
            q.QuestionID, !string.IsNullOrEmpty(validate) ? string.Format("validate[{0}]",validate) : "", phrl == null ? "" : phrl.QuestionAnswerText);
            str += "</div>";
            str += "</div>";
            return str;
        }
        private string GetTXT(Question q, PatientHealthRecordLine phrl) {
            string str = "";
            str += "<div class=\"form-group row my-0\">";
            str += string.Format("<label for=\"{0}\" class=\"col-sm-4 col-form-label\">{1}</label>",q.QuestionID, q.QuestionText);
            str += "<div class=\"col-sm-8\">";
            str += string.Format("<input type=\"text\" class=\"form-control {1}\" id=\"{0}\" name=\"{0}\" value=\"{2}\" placeholder=\"Enter ...\">",
                q.QuestionID, (q.IsMandatory ?? true) ? "validate[required]" : "", phrl == null ? "":phrl.QuestionAnswerText);
            str += "</div>";
            str += "</div>";
            return str;
        }
        private string GetMemo(Question q, PatientHealthRecordLine phrl)
        {
            string str = "";
            str += "<div class=\"form-group row my-0\">";
            str += string.Format("<label for=\"{0}\" class=\"col-sm-4 col-form-label\">{1}</label>", q.QuestionID, q.QuestionText);
            str += "<div class=\"col-sm-8\">";
            str += string.Format("<textarea class=\"form-control {1}\" rows=\"3\" id=\"{0}\" name=\"{0}\" placeholder=\"Enter ...\">{2}</textarea>", 
                q.QuestionID, (q.IsMandatory ?? true) ? "validate[required]" : "", phrl == null ? "" : phrl.QuestionAnswerText);
            str += "</div>";
            str += "</div>";
            return str;
        }
        private string GetOptionVertical(Question q, QuestionAnswerSelectionLine[] qaslColl, PatientHealthRecordLine phrl)
        {
            string str = "";
            str += "<div class=\"form-group row my-0\">";
            str += string.Format("<label for=\"{0}\" class=\"col-sm-4 col-form-label\">{1}</label>", q.QuestionID, q.QuestionText);
            str += "<div class=\"col-sm-8\">";
            str += "<div class=\"row\">";
            str += "<div class=\"col-sm-12\">";
            str += "<div class=\"row\">";
            foreach (var qasl in qaslColl)
            {
                str += "<div class=\"col-sm-5 custom-control custom-radio\">";
                str += string.Format("<input class=\"custom-control-input {2}\" type=\"radio\" id=\"{0}_{1}\" name=\"{0}\" value=\"{1}\" {3}>",
                    q.QuestionID, qasl.QuestionAnswerSelectionLineID, (q.IsMandatory ?? true) ? "validate[required]" : "",
                    phrl == null ? "" : (phrl.QuestionAnswerSelectionLineID == qasl.QuestionAnswerSelectionLineID ? "checked" : ""));
                str += string.Format("<label for=\"{0}_{1}\" class=\"custom-control-label\">{2}</label>", q.QuestionID, qasl.QuestionAnswerSelectionLineID, qasl.QuestionAnswerSelectionLineText);
                str += "</div>";
                str += "<div class=\"col-sm-7\">";
                if (qasl.QuestionAnswerSelectionLineText.Contains(":"))
                {
                    str += string.Format("<input type=\"text\" class=\"form-control {1}\" id=\"{0}_2\" name=\"{0}_2\" value=\"{2}\" placeholder=\"Enter ...\">",
                        q.QuestionID, (q.IsMandatory ?? true) ? "" : "", phrl == null ? "" : phrl.QuestionAnswerText2);
                }
                str += "</div>";
            }
            str += "</div>";
            str += "</div>";
            str += "</div>";
            str += "</div>";
            str += "</div>";
            return str;
        }
        private string GetOption(Question q, QuestionAnswerSelectionLine[] qaslColl, PatientHealthRecordLine phrl) {
            string str = "";
            str += "<div class=\"form-group row my-0\">";
            str += string.Format("<label for=\"{0}\" class=\"col-sm-4 col-form-label\">{1}</label>", q.QuestionID, q.QuestionText);
            str += "<div class=\"col-sm-8\">";
            str += "<div class=\"row\">";
            foreach (var qasl in qaslColl) {
                str += string.Format("<div class=\"col-sm-{0} custom-control custom-radio\">", System.Convert.ToInt16(12 / qaslColl.Count()).ToString());
                str += string.Format("<input class=\"custom-control-input {2}\" type=\"radio\" id=\"{0}_{1}\" name=\"{0}\" value=\"{1}\" {3}>", 
                    q.QuestionID, qasl.QuestionAnswerSelectionLineID, (q.IsMandatory ?? true) ? "validate[required]" : "", 
                    phrl == null ? "" : (phrl.QuestionAnswerSelectionLineID == qasl.QuestionAnswerSelectionLineID ? "checked" : ""));
                str += string.Format("<label for=\"{0}_{1}\" class=\"custom-control-label\">{2}</label>", q.QuestionID, qasl.QuestionAnswerSelectionLineID, qasl.QuestionAnswerSelectionLineText);
                str += "</div>";
            }
            str += "</div>";
            str += "</div>";
            str += "</div>";
            return str;
        }
        private string GetOptionEmo5(Question q, QuestionAnswerSelectionLine[] qaslColl, PatientHealthRecordLine phrl)
        {
            string str = "";
            str += "<div class=\"form-group row my-0\">";
            str += string.Format("<label for=\"{0}\" class=\"col-sm-4 col-form-label\">{1}</label>", q.QuestionID, q.QuestionText);
            str += "<div class=\"col-sm-8\">";
            str += "<div class=\"row\">";
            foreach (var qasl in qaslColl.OrderBy(x => x.QuestionAnswerSelectionID))
            {
                str += "<div class=\"col-sm-2 emo-selector\">";
                str += string.Format("<input class=\"{3}\" type=\"radio\" id=\"{0}_{1}\" name=\"{0}\" value=\"{1}\" {4}>", 
                    q.QuestionID, qasl.QuestionAnswerSelectionLineID, q.QuestionID, (q.IsMandatory ?? true) ? "validate[required]" : "",
                    phrl == null ? "" : (phrl.QuestionAnswerSelectionLineID == qasl.QuestionAnswerSelectionLineID ? "checked" : ""));
                str += string.Format("<label for=\"{0}_{1}\" class=\"emo emo{2}\"></label>", q.QuestionID, qasl.QuestionAnswerSelectionLineID, System.Convert.ToInt16(qasl.QuestionAnswerSelectionLineID));
                str += "</div>";
            }
            str += "</div>";
            str += "</div>";
            str += "</div>";
            return str;
        }
    }
}