using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.ReportDataSource.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.ReportDataSource.GenericRpt
{
    /// <summary>
    /// Summary description for Assessment
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class GetJson : BaseDataService
    {
        #region Shared Method
        #endregion

        #region WS
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessKey"></param>
        /// <param name="jSonQuery"></param>
        /// <param name="jSonParam"></param>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Get(string accessKey, string jSonQueryAndParam)
        {
            try
            {
                var XaccessKey = FixParameterV2(accessKey);
                if (XaccessKey != accessKey) {
                    accessKey = XaccessKey;
                    jSonQueryAndParam = FixParameterV2(jSonQueryAndParam);
                }

                ValidateAccessKey(accessKey);

                var jsO = Helper.JsonStrToArray(jSonQueryAndParam);
                var jsOQuery = JsonGetNodeArrayRequired("query", jsO);
                var jsOParam = JsonGetNodeArrayRequired("param", jsO);

                var ret = GetJsonQuery(jsOQuery[0], jsOParam[0]);

                Context.Response.Write(JSonRetFormattedObjectOnly(ret));
            }
            catch (Exception ex)
            {
                Context.Response.Write(ex.Message);
            }
        }
        #endregion

        #region Private
        private Dictionary<string, object> GetJsonQuery(Dictionary<string, object> jsOQuery, Dictionary<string, object> jsOParam) {
            // do something here
            var ret = new Dictionary<string, object>();
            foreach (var jsq in jsOQuery) {
                ret.Add(jsq.Key, ExecuteQuery((Dictionary<string, object>)jsq.Value, jsOParam, new Dictionary<string, object>(), 1));
            }

            return ret;
        }

        private object ExecuteQuery(Dictionary<string, object> Query, Dictionary<string, object> jsOParam,
            Dictionary<string, object> jsOParent, int UniqueID) {

            Dictionary<string, object> sNextLevel = new Dictionary<string, object>();

            foreach (var x in jsOParam) {
                if (!(Query["sQuery"].ToString()).Contains(x.Key)) continue;

                UniqueID+=1;
                if (x.Value.GetType() == typeof(string))
                {
                    Query["sQuery"] = Query["sQuery"].ToString().Replace(x.Key, string.Format("'{0}'", x.Value.ToString()));
                }
                else {
                    // jika query maka apa bro??
                    Query["sQuery"] = (Query["sQuery"]).ToString().Replace(x.Key, string.Empty);
                    sNextLevel.Add(x.Key, x.Value);
                }
            }

            foreach (var nl in jsOParent) {
                Query["sQuery"] = Query["sQuery"].ToString().Replace("@" + nl.Key, nl.Value.ToString());
            }

            var dtb = GetDataTableDirect(Query["sQuery"].ToString());

            if (Query["returnType"].ToInt() == 0)
            {
                var lDict = new List<Dictionary<string, object>>();
                foreach (DataRow dr in dtb.Rows)
                {
                    var rtj = ConvertDataRowtoObject(dr);
                    lDict.Add(rtj);
                    foreach (var nLevel in sNextLevel)
                    {
                        var aa = ExecuteQuery((Dictionary<string, object>)nLevel.Value, jsOParam, rtj, UniqueID);
                        rtj.Add(nLevel.Key.Replace("@", ""), aa);
                    }
                }

                return lDict;
            }
            else {
                var lDict = new Dictionary<string, object>();
                // convert 1st row only
                foreach (DataRow dr in dtb.Rows)
                {
                    var rtj = ConvertDataRowtoObject(dr);
                    lDict = rtj;
                    foreach (var nLevel in sNextLevel)
                    {
                        var aa = ExecuteQuery((Dictionary<string, object>)nLevel.Value, jsOParam, rtj, UniqueID);
                        rtj.Add(nLevel.Key.Replace("@", ""), aa);
                    }
                    break;
                }

                return lDict;
            }

            
        }

        private bool IsQuery(string query) {
            return query.ToUpper().Contains("SELECT");
        }
        #endregion
    }
}
