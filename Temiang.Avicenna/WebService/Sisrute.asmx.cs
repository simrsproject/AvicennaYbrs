using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Telerik.Web.UI;
using Newtonsoft.Json;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for Sisrute
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Sisrute : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public RadComboBoxItemData[] GetDiagnosaIcdX(object context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["filter"]).ToLower();

            var svc = new Common.Sisrute.Service();
            var response = JsonConvert.DeserializeObject<Common.Sisrute.Referensi.Diagnosa>(svc.GetReferensi(filter, 0));

            //if (!response.MetaData.IsValid) return null;
            var result = new List<RadComboBoxItemData>(response.Data.Count());
            foreach (var data in response.Data)
            {
                var item = new RadComboBoxItemData();
                item.Text = data.NAMA;
                item.Value = data.KODE;
                result.Add(item);
            }
            return result.ToArray();
        }

        public static RadComboBoxItemData[] GetDiagnosaIcdX(string filter)
        {
            var svc = new Common.Sisrute.Service();
            var response = JsonConvert.DeserializeObject<Common.Sisrute.Referensi.Diagnosa>(svc.GetReferensi(filter, 0));

            //if (!response.MetaData.IsValid) return null;
            var result = new List<RadComboBoxItemData>(response.Data.Count());
            foreach (var data in response.Data)
            {
                var item = new RadComboBoxItemData();
                item.Text = data.NAMA;
                item.Value = data.KODE;
                result.Add(item);
            }
            return result.ToArray();
        }

        [WebMethod]
        public RadComboBoxItemData[] GetDiagnosaIcd9Cm(object context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["filter"]).ToLower();

            var proc = new BusinessObject.ProcedureCollection();
            proc.Query.Where(
                proc.Query.Or(
                    proc.Query.ProcedureID.Like("%" + filter + "%"),
                    proc.Query.ProcedureName.Like("%" + filter + "%")
                )
            );
            proc.Query.Load();

            //if (!response.MetaData.IsValid) return null;
            var result = new List<RadComboBoxItemData>(proc.Count());
            foreach (var data in proc)
            {
                var item = new RadComboBoxItemData();
                item.Text = data.ProcedureName;
                item.Value = data.ProcedureID;
                result.Add(item);
            }
            return result.ToArray();
        }
    }
}
