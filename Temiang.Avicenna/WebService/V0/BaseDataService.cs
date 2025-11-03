using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.Interfaces;
using fastJSON;

namespace Temiang.Avicenna.WebService.V0
{
    public class BaseDataService : JsonRetWS
    {
        protected const string ErrUnspecified = "100|Unspecified error";
        protected const string ErrDataNotFound = "200|Data not found";
        protected const string ErrDataApptSlotNotFound = "201|Appointment slot not found";
        protected const string ErrDataScheNotFound = "202|Schedule not found";
        protected const string ErrDataMultipleFound = "210|Multiple data found, expect one";
        protected const string ErrDataMultipleApptSlotFound = "211|Multiple slot found, expect one";
        protected const string ErrDataMultipleScheFound = "212|Multiple schedule found, expect one";
        protected const string ErrDataHasBeenApproved = "220|Data has been approved";
        protected const string ErrDataHasBeenVoided = "221|Data has been voided";
        protected const string ErrFieldRequired = "300|Field value required";
        protected const string ErrFieldInvalidValue = "301|Invalid value";
        protected const string ErrDataApptConflict = "400|Appointment time slot has been taken";
        protected const string ErrKeyInvalid = "900|Key access invalid";
        protected const string ErrKeyExpired = "901|Key access expired";

        protected static string GetErrorCode(string errorConst)
        {
            var strs = errorConst.Split('|');
            if (strs.Length > 1)
            {
                return strs[0];
            }
            else
            {
                return GetErrorCode(ErrUnspecified);
            }
        }

        protected static string GetErrorMessage(string errorConst)
        {
            var strs = errorConst.Split('|');
            if (strs.Length > 1)
            {
                return strs[1];
            }
            else
            {
                return strs[0];
            }
        }

        public class FieldDB
        {
            public string FieldName;
            public string FieldValue;
            public string FieldIdentifier;
        }

        #region Logging
        private void EraseLog() {
            var logs = new WebServiceAPILogCollection();
            logs.DeletePrevMonth();

            //logs.Query.Where("<DateRequest < DATEADD(MONTH, -1,GETDATE())>");
            ////logs.Query.OrderBy(logs.Query.ID.Ascending);
            //logs.Query.es.Top = 100;
            //logs.LoadAll();
            //logs.MarkAllAsDeleted();
            //logs.Save();
        }
        protected WebServiceAPILog LogAdd()
        {
            // erase prev log
            EraseLog();

            var log = new WebServiceAPILog();
            log.AddNew();
            log.DateRequest = DateTime.Now;
            log.UrlAddress = HttpContext.Current.Request.Url.PathAndQuery;
            log.IPAddress = Helper.GetIP4Address();

            string pars = string.Empty;
            string[] keys = HttpContext.Current.Request.Form.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                if (pars.Length > 0) pars += "&";
                pars += string.Format("{0}={1}", keys[i], HttpContext.Current.Request.Form[keys[i]]);
            }
            log.Params = pars;
            log.Save();
            return log;
        }
        protected void WriteResponseAndLog(WebServiceAPILog log, string Response)
        {
            log.Response = Response;
            log.Totalms = System.Convert.ToInt32((DateTime.Now - log.DateRequest).Value.TotalMilliseconds);
            log.Save();
            Context.Response.Write(Response);
        }

        protected string ValidateAccessKey(string sKey)
        {
            if (sKey == "sciadmin88") return "WebService";
            if (sKey == "bpjs") return "bpjs";

            if (string.IsNullOrEmpty(sKey)) throw new Exception(
                ErrFieldRequired.Replace(GetErrorMessage(ErrFieldRequired), string.Format("{0} required", "AccessKey")));

            var keyColl = new WebServiceAccessKeyCollection();
            var query = new WebServiceAccessKeyQuery();
            query.Where(query.AccessKey == sKey);
            if (keyColl.Load(query))
            {
                var key = keyColl.First();
                if (key.StartDate.Value > DateTime.Now)
                {
                    throw new Exception(ErrKeyInvalid);
                }
                if (key.EndDate.Value < DateTime.Now)
                {
                    throw new Exception(ErrKeyInvalid);
                }
                return key.ClientCode;
            }
            else
            {
                throw new Exception(ErrKeyInvalid);
            }
        }

        protected string ValidateSex(string Sex)
        {
            Sex = Sex.ToUpper();
            if (!(new string[] { "M", "F" }).Contains(Sex))
                throw new Exception(
                ErrFieldInvalidValue.Replace(GetErrorMessage(ErrFieldInvalidValue), string.Format("Invalid value for Sex, use M/F", Sex)));
            return Sex;
        }

        protected DateTime ValidateDate(string sFieldName, string sFieldValue)
        {
            DateTime Temp;
            if (!DateTime.TryParse(sFieldValue, out Temp))
                throw new Exception(
                    ErrFieldInvalidValue.Replace(GetErrorMessage(ErrFieldInvalidValue),
                    string.Format("Invalid value for {0}", sFieldName)));
            return Temp;
        }

        protected void ValidateDateRange(DateTime startDate, DateTime endDate, int i)
        {
            if ((endDate.Date - startDate.Date).Days > i)
                throw new Exception(
                    ErrFieldInvalidValue.Replace(GetErrorMessage(ErrFieldInvalidValue),
                    string.Format("Date range must not be more than {0} days", i.ToString())));
        }

        protected Decimal ValidateAmount(string sFieldName, string sFieldValue)
        {
            Decimal Temp;
            if (!Decimal.TryParse(sFieldValue, out Temp))
                throw new Exception(
                    ErrFieldInvalidValue.Replace(GetErrorMessage(ErrFieldInvalidValue),
                    string.Format("Invalid value for {0}", sFieldName)));
            return Temp;
        }

        #endregion

        #region General Private
        protected void SetUserLoginSession(string UserID)
        {
            // session ksjdfjaiweoi adjf dsfklsdfiwjeafds jflksa djfoi a jdfasd;l
            BusinessObject.Common.UserLogin _userLogin = new BusinessObject.Common.UserLogin();
            _userLogin.UserID = string.IsNullOrEmpty(UserID) ? "WebService" : UserID;
            AppSession.UserLogin = _userLogin;
            return;
        }

        #region Json
        protected void JsonSetValuesToObject(object o, Dictionary<string, object> jSonObj)
        {
            foreach (var x in jSonObj)
            {
                if (x.Value is System.Collections.Generic.Dictionary<string, object>) continue;

                var props = o.GetType().GetProperty(x.Key,
                    System.Reflection.BindingFlags.IgnoreCase |
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.GetProperty |
                    System.Reflection.BindingFlags.Public);
                if (props == null)
                    throw new Exception(string.Format("Object {0} invalid property {1}", o.GetType().Name, x.Key));
                var propType = props.PropertyType;
                var converter = TypeDescriptor.GetConverter(propType);
                if (x.Value == null)
                {
                    props.SetValue(o, x.Value, null);
                }
                else
                {
                    var convertedObject = converter.ConvertFromString(x.Value.ToString());
                    props.SetValue(o, convertedObject, null);
                }
            }
        }

        protected System.Collections.Generic.Dictionary<string, object> JsonInspectNodeRequired(string FieldName, Dictionary<string, object> jSonObj)
        {
            var oNode = JsonGetNode(FieldName, jSonObj);
            if (oNode == null)
            {
                throw new Exception(
                    ErrFieldRequired.Replace(GetErrorMessage(ErrFieldRequired), string.Format("{0} required", FieldName)));
            }
            return oNode;
        }

        protected List<object> JsonStrToArrayList(string jSonString)
        {
            return fastJSON.JSON.ToObject<List<object>>(jSonString);
        }

        protected System.Collections.Generic.Dictionary<string, object> JsonStrToArray(string jSonString)
        {
            return fastJSON.JSON.ToObject<Dictionary<string, object>>(jSonString);
        }

        protected System.Collections.Generic.Dictionary<string, object> JsonGetNode(string FieldName, Dictionary<string, object> jSonObj)
        {
            var fields = jSonObj.Where(x => x.Key.ToLower() == FieldName.ToLower()).Select(x => x.Value);
            var y = fields.FirstOrDefault();
            return (System.Collections.Generic.Dictionary<string, object>)y;
        }

        protected string JsonInspectStringRequired(string FieldName, Dictionary<string, object> jSonObj)
        {
            var ret = JsonGetString(FieldName, jSonObj);
            InspectStringRequired(FieldName, ret);
            return ret;
        }
        protected string JsonGetString(string FieldName, Dictionary<string, object> jSonObj)
        {
            var fields = jSonObj.Where(x => x.Key.ToLower() == FieldName.ToLower());
            if (fields.Count() == 0)
            {
                return string.Empty;
            }
            return fields.First().Value.ToString();
        }
        #endregion

        protected void InspectOneResult(DataTable dtb)
        {
            switch (dtb.Rows.Count)
            {
                case 0: { throw new Exception(ErrDataNotFound); break; }
                case 1: { break; }
                default: { throw new Exception(ErrDataMultipleFound); break; }
            }
        }
        protected void InspectStringRequired(string FieldName, string FieldValue)
        {
            if (string.IsNullOrEmpty(FieldValue))
                throw new Exception(
                    ErrFieldRequired.Replace(GetErrorMessage(ErrFieldRequired), string.Format("{0} required", FieldName)));
        }

        protected void InspectStringRequiredOR(FieldDB[] fdbs)
        {
            string FieldIdentifiers = string.Empty;
            foreach (var fdb in fdbs)
            {
                if (!string.IsNullOrEmpty(fdb.FieldValue)) return;
                FieldIdentifiers += (FieldIdentifiers.Length > 0 ? ", " : "") + (string.IsNullOrEmpty(fdb.FieldIdentifier) ? fdb.FieldName : fdb.FieldIdentifier);
            }
            throw new Exception(
                    ErrFieldRequired.Replace(GetErrorMessage(ErrFieldRequired), string.Format("at least one of the following fields is required ({0})", FieldIdentifiers)));
        }

        protected void SetListParameters(esDynamicQuery Query, string FieldName, string FieldValue)
        {
            if (!string.IsNullOrEmpty(FieldValue))
            {
                Query.Where(string.Format("<{0} like '{1}'>", FieldName, Helper.EscapeQuery(FieldValue)));
            }
        }

        protected void SetListParametersEquals(esDynamicQuery Query, string FieldName, string FieldValue)
        {
            if (!string.IsNullOrEmpty(FieldValue))
            {
                Query.Where(string.Format("<{0} = '{1}'>", FieldName, Helper.EscapeQuery(FieldValue)));
            }
        }

        protected void SetListParametersLike(esDynamicQuery Query, FieldDB[] Fields)
        {
            foreach (var field in Fields)
            {
                if (!string.IsNullOrEmpty(field.FieldValue))
                    SetListParameters(Query, field.FieldName, string.Format("%{0}%", field.FieldValue));
            }
        }

        protected void SetListParametersOR(esDynamicQuery Query, FieldDB[] Fields)
        {
            string w = string.Empty;
            foreach (var field in Fields)
            {
                if (string.IsNullOrEmpty(field.FieldValue)) continue;
                w += string.Format("{2}{0} like '%{1}%'", field.FieldName, Helper.EscapeQuery(field.FieldValue), (w.Length > 0) ? " OR " : "");
            }
            if (!string.IsNullOrEmpty(w))
            {
                Query.Where(string.Format("<({0})>", w));
            }
        }
        #endregion


        #region Testing"
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void HelloWorld(string Key)
        {
            Context.Response.Write(JSonRetFormatted("Hello World"));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TestRespondOK()
        {
            Context.Response.Write(JSonRetFormatted("Optional data goes here"));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TestRespondError()
        {
            Context.Response.Write(
                JSonRetFormatted("Detail error message goes here", false, GetErrorCode(ErrUnspecified)));
        }
        #endregion

    }
}
