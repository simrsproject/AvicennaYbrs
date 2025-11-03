using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Configuration;
using System.IO;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.Worklist.MM2100
{
    public class Service
    {
        public static string PacsWorklistServiceUrlLocation
        {
            get { return ConfigurationManager.AppSettings["InacbgServiceUrlLocation"]; }
        }

        private HttpWebRequest PopulateWebRequest(string url, BPJS.Helper.WebRequestMethod method, BPJS.Helper.WebRequestContentType contentType, string parameter)
        {
            var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
            webrequest.Method = method.ToString();

            if (method != BPJS.Helper.WebRequestMethod.GET) webrequest.ContentType = contentType.ToString();

            var formData = Encoding.UTF8.GetBytes(parameter.ToString());
            webrequest.ContentLength = formData.Length;

            using (var post = webrequest.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            return webrequest;
        }

        public void NewWorklist(string transactionNo, TransChargesItemCollection tci)
        {
            var tc = new TransCharges();
            tc.LoadByPrimaryKey(transactionNo);

            var reg = new Registration();
            reg.LoadByPrimaryKey(tc.RegistrationNo);

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);

            foreach (var entity in tci.Where(t => (t.IsOrderRealization ?? false)))
            {
                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);

                if (item.SRItemType != ItemType.Radiology) continue;

                var medic = new Paramedic();
                medic.LoadByPrimaryKey(entity.ParamedicID);

                var _autoNumber = Helper.GetNewAutoNumber(tc.ExecutionDate.Value.Date, AppEnum.AutoNumber.CommunicationID);
                var stepID = _autoNumber.LastCompleteNumber;
                _autoNumber.Save();

                var param = string.Concat(new string[]
                {
                    "patientName=",pat.PatientName,
                    "&patientID=",pat.MedicalNo,
                    "&patientBirthDate=",pat.DateOfBirth.Value.ToString("yyyyMMdd"),
                    "&patientGender=",pat.Sex,
                    "&procedureStepStartDate=",tc.ExecutionDate.Value.ToString("yyyyMMdd"),
                    "&procedureStepStartTime=",tc.ExecutionDate.Value.ToString("HHmmss"),
                    "&performingPhysicianName=",medic.ParamedicName,
                    "&procedureStepDescription=",item.ItemName,
                    "&procedureStepID=",stepID,
                    "&modalityID=", item.ItemIDExternal
                });

                entity.CommunicationID = stepID;

                var sb = new StringBuilder();
                sb.Append(param);

                var url = PacsWorklistServiceUrlLocation + "/new_pacs_worklist";
                using (HttpWebResponse response = PopulateWebRequest(url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                    var sr = new StreamReader(response.GetResponseStream());
                    var msg = JsonConvert.DeserializeObject<Response>(sr.ReadToEnd());
                }
            }

            tci.Save();
        }
    }

    public class Response
    {
        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("debug")]
        public string Debug { get; set; }
    }
}
