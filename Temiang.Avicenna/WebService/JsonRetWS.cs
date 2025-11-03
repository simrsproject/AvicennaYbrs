using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using fastJSON;
using DevExpress.Skins;

namespace Temiang.Avicenna.WebService
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

        public string JSonRetFormatted(object o)
        {
            return JSonRetFormatted(o, true, "");
        }

        public string JSonRetFormatted(object o, bool status)
        {
            return JSonRetFormatted(o, status, "");
        }

        public string JSonRetFormatted(object o, bool status, string errorCode)
        {
            JsonRet ret = new JsonRet();
            ret.setValue(status, errorCode, o);
            //return JSON.ToJSON(ret);
            //fastJSON.JSON.ToNiceJSON(ret);

            var oJS = new System.Web.Script.Serialization.JavaScriptSerializer();
            oJS.MaxJsonLength = 2147483644;
            return oJS.Serialize(ret);
        }

        public string ConvertDataTabletoJSON(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(ConvertDataTabletoObject(dt));
        }

        public List<Dictionary<string, object>> ConvertDataTabletoObject(DataTable dt)
        {
            DataTableAddColumYMDHMS(dt);

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                rows.Add(ConvertDataRowtoObjectWithoutAddingRowYMDHMS(dr));
            }
            return rows;
        }

        public string ConvertDataRowtoJSON(DataRow dr)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(ConvertDataRowtoObject(dr));
        }

        private Dictionary<string, object> ConvertDataRowtoObjectWithoutAddingRowYMDHMS(DataRow dr)
        {
            Dictionary<string, object> row;
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dr.Table.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            return row;
        }

        private void DataTableAddColumYMDHMS(DataTable dt)
        {
            List<string> columNamesOfDateTime = new List<string>();
            foreach (System.Data.DataColumn c in dt.Columns) {
                if (c.DataType == typeof(DateTime)) {
                    columNamesOfDateTime.Add(c.ColumnName);
                }
            }
            if (columNamesOfDateTime.Count > 0) {
                foreach (var columName in columNamesOfDateTime)
                {
                    var nc = dt.Columns.Add(columName + "_yMdHms", typeof(string));
                    // set value
                    foreach (System.Data.DataRow xrow in dt.Rows) {
                        if (xrow[columName] is DBNull)
                        {
                            //skip, no value to be convert
                        }
                        else
                        {
                            xrow[nc] = ((DateTime)xrow[columName]).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                    }
                }
            }
            dt.AcceptChanges();
        }

        public Dictionary<string, object> ConvertDataRowtoObject(DataRow dr)
        {
            var dt = dr.Table;
            DataTableAddColumYMDHMS(dt);

            return ConvertDataRowtoObjectWithoutAddingRowYMDHMS(dr);
        }
    }
}
