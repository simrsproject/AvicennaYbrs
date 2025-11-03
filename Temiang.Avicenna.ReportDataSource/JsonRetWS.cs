using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using fastJSON;

namespace Temiang.Avicenna.ReportDataSource
{
    public class JsonRetWS : System.Web.Services.WebService
    {
        public class jQueryDatatableReturn
        {
            public string status;
            public int draw;
            public int recordsTotal;
            public int recordsFiltered;
            public object data;

            public string Serialize() {
                var sRet = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(this);
                return sRet;
            }
        }

        public class jQueryDatatableRequest {
            public int limit, start, orderColumn, draw; 
            public string searchKey, orderDir;

            public void RetrieveQueryString()
            {
                limit = System.Convert.ToInt32(HttpContext.Current.Request.Params["length"] == null ? "0" : HttpContext.Current.Request.Params["length"]);
                start = System.Convert.ToInt32(HttpContext.Current.Request.Params["start"] == null ? "0" : HttpContext.Current.Request.Params["start"]);
                searchKey = HttpContext.Current.Request.Params["search[value]"] == null ? "" : HttpContext.Current.Request.Params["search[value]"];
                orderColumn = System.Convert.ToInt32(HttpContext.Current.Request.Params["order[0][column]"] == null ? "" : HttpContext.Current.Request.Params["order[0][column]"]);
                orderDir = HttpContext.Current.Request.Params["order[0][dir]"] == null ? "" : HttpContext.Current.Request.Params["order[0][dir]"];
                draw = System.Convert.ToInt32(HttpContext.Current.Request.Params["draw"] == null ? "0" : HttpContext.Current.Request.Params["draw"]);
            }

            public string GetColumnName(int colIndex) {
                return HttpContext.Current.Request.Params[string.Format("columns[{0}][name]", colIndex.ToString())] == null ? "" :
                    HttpContext.Current.Request.Params[string.Format("columns[{0}][name]", colIndex.ToString())];
            }
        }

        public class JsonRet
        {
            public string status;
            public string errorCode;
            public object data;

            public void setValue(bool Status, string ErrorCode, object Data)
            {
                status = Status == true ? "OK" : "ERR";
                errorCode = ErrorCode;
                data = Data;
            }
        }

        public string JSonRetFormattedObjectOnly(object o)
        {
            return JSON.ToJSON(o);
        }

        public string JSonRetFormatted(object o)
        {
            return JSonRetFormatted(o, true, "");

            //JsonRet ret = new JsonRet();
            //ret.setValue(true,"", o);
            ////return JSON.ToJSON(ret);

            //var sRet = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(ret);
            //return sRet;
        }

        public string JSonRetFormatted(object o, bool status)
        {
            return JSonRetFormatted(o, status, "");

            //JsonRet ret = new JsonRet();
            //ret.setValue(status, "", o);
            ////return JSON.ToJSON(ret);

            //var sRet = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(ret);
            //return sRet;
        }

        public string JSonRetFormatted(object o, bool status, string errorCode)
        {
            JsonRet ret = new JsonRet();
            ret.setValue(status, errorCode, o);
            //return JSON.ToJSON(ret);

            var sRet = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(ret);
            return sRet;
        }

        public string ConvertDataTabletoJSON(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(ConvertDataTabletoObject(dt));
        }

        public List<Dictionary<string, object>> ConvertDataTabletoObject(DataTable dt)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return rows;
        }

        public string ConvertDataRowtoJSON(DataRow dr)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(ConvertDataRowtoObject(dr));
        }

        public Dictionary<string, object> ConvertDataRowtoObject(DataRow dr)
        {
            Dictionary<string, object> row;
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dr.Table.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            return row;
        }

        public object MergeJsonData(object item, params object[] items)
        {
            if (item == null || items == null)
                return item ??  new object();

            var result = new Dictionary<string, object>();
            foreach (System.Reflection.PropertyInfo fi in item.GetType().GetProperties().Where(x => x.CanRead))
            {
                var value = fi.GetValue(item, null);
                result[fi.Name] = value;
            }

            foreach (object obj in items)
            {
                foreach (System.Reflection.PropertyInfo fi in obj.GetType().GetProperties().Where(x => x.CanRead))
                {
                    var value = fi.GetValue(obj, null);
                    result[fi.Name] = value;
                }
            }

            return result;
        }
    }
}
