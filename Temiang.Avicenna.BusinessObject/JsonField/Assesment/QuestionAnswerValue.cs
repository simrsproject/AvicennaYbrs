using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class QuestionAnswerValue
    {
        [JsonProperty("qid")]
        public string QuestionID { get; set; }

        [JsonProperty("qaprefix", NullValueHandling = NullValueHandling.Ignore)]
        public string QuestionAnswerPrefix { get; set; }

        [JsonProperty("qasufix", NullValueHandling = NullValueHandling.Ignore)]
        public string QuestionAnswerSuffix { get; set; }

        [JsonProperty("qaselid", NullValueHandling = NullValueHandling.Ignore)]
        public string QuestionAnswerSelectionLineID { get; set; }

        [JsonProperty("qatext", NullValueHandling = NullValueHandling.Ignore)]
        public string QuestionAnswerText { get; set; }

        [JsonProperty("qanum", NullValueHandling = NullValueHandling.Ignore)]
        public double? QuestionAnswerNum { get; set; }

        [JsonProperty("qatype", NullValueHandling = NullValueHandling.Ignore)]
        public string SRAnswerType { get; set; }


    }

    public class QuestionGroupAnswerValue
    {
        [JsonProperty("qfid", NullValueHandling = NullValueHandling.Ignore)]
        public string QuestionFormID { get; set; }

        [JsonProperty("qgid")]
        public string QuestionGroupID { get; set; }        
        
        [JsonProperty("sum", NullValueHandling = NullValueHandling.Ignore)]
        public string Summary { get; set; }

        private List<QuestionAnswerValue> _qavalue;
        [JsonProperty("answers", NullValueHandling = NullValueHandling.Ignore)]
        public List<QuestionAnswerValue> QuestionAnswerValues
        {
            get { return _qavalue ?? (_qavalue = new List<QuestionAnswerValue>()); }
            set { _qavalue = value; }
        }
    }
}
