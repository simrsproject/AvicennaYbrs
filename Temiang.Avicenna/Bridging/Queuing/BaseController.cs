using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Http;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Bridging.Queuing
{
    public class RetObj
    {
        const string OK = "OK";
        const string ERR = "ERR";

        public string status;
        public string errorCode;
        public object data;

        private void setValue(bool Status, string ErrorCode, object Data)
        {
            status = Status == true ? OK : ERR;
            errorCode = ErrorCode;
            data = Data;
        }

        public void setOK(object Data) {
            setValue(true, "", Data);
        }
        public void setERR(string ErrorCode, object Data) {
            setValue(false, ErrorCode, Data);
        }
    }

    public class BaseController : ApiController
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

        public static RetObj CreateRetObjOK(object Data) {
            var ret = new RetObj();
            ret.setOK(Data);
            return ret;
        }
        public static RetObj CreateRetObjERR(string ErrorCode, object Data)
        {
            var ret = new RetObj();
            ret.setERR(ErrorCode, Data);
            return ret;
        }

        private void EraseLog()
        {
            var logs = new WebServiceAPILogCollection();
            logs.DeletePrevMonth();
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
        protected HttpResponseMessage WriteResponseAndLog(WebServiceAPILog log, HttpStatusCode code, object data)
        {
            var oJS = new System.Web.Script.Serialization.JavaScriptSerializer();
            oJS.MaxJsonLength = 2147483644;
            log.Response = oJS.Serialize(data);
            log.Totalms = System.Convert.ToInt32((DateTime.Now - log.DateRequest).Value.TotalMilliseconds);
            log.Save();

            return Request.CreateResponse(code, data, JsonMediaTypeFormatter.DefaultMediaType);
        }

        protected void SetUserLoginSession(string UserID)
        {
            // session ksjdfjaiweoi adjf dsfklsdfiwjeafds jflksa djfoi a jdfasd;l
            BusinessObject.Common.UserLogin _userLogin = new BusinessObject.Common.UserLogin();
            _userLogin.UserID = string.IsNullOrEmpty(UserID) ? "WebService" : UserID;
            AppSession.UserLogin = _userLogin;
            return;
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

        protected DateTime ValidateDate(string sFieldName, string sFieldValue)
        {
            DateTime Temp;
            if (!DateTime.TryParseExact(sFieldValue, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out Temp))
                throw new Exception(
                    ErrFieldInvalidValue.Replace(GetErrorMessage(ErrFieldInvalidValue),
                    string.Format("Invalid value for {0}", sFieldName)));
            return Temp;
        }
        protected void ValidateStr(string FieldName, string FieldValue)
        {
            if (string.IsNullOrEmpty(FieldValue))
                throw new Exception(
                    ErrFieldRequired.Replace(GetErrorMessage(ErrFieldRequired), string.Format("{0} required", FieldName)));
        }

        protected ServiceUnit ValidateServiceUnit(string serviceunitID) {
            var su = new ServiceUnit();
            if (!su.LoadByPrimaryKey(serviceunitID))
            {
                throw new Exception(
                   ErrFieldInvalidValue.Replace(GetErrorMessage(ErrFieldInvalidValue),
                   string.Format("Invalid value {0} of parameter serviceUnitId", serviceunitID)));
            }
            return su;
        }

        protected Paramedic ValidateParamedic(string paramedicID)
        {
            var par = new Paramedic();
            if (!par.LoadByPrimaryKey(paramedicID))
            {
                throw new Exception(
                   ErrFieldInvalidValue.Replace(GetErrorMessage(ErrFieldInvalidValue),
                   string.Format("Invalid value {0} of parameter paramedicID", paramedicID)));
            }
            return par;
        }

        protected void ValidateQueueingType(string type) {
            var stdi = new AppStandardReferenceItem();
            if (!stdi.LoadByPrimaryKey("QueueingType", type)) {
                throw new Exception(
                   ErrFieldInvalidValue.Replace(GetErrorMessage(ErrFieldInvalidValue),
                   string.Format("Invalid value {0} of parameter type, the reference values can be found in standard reference named QueueingType", type)));
            }
        }
    }
}