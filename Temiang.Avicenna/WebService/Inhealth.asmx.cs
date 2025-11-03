using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for Inhealth
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Inhealth : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public RadComboBoxItemData[] GetDiagnosa(object context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["filter"]).ToLower();

            var diags = new DiagnoseCollection();
            diags.Query.Where(diags.Query.Or(diags.Query.DiagnoseID.Like(string.Format("%{0}%", filter)), diags.Query.DiagnoseName.Like(string.Format("%{0}%", filter))));
            diags.Query.Load();

            var result = new List<RadComboBoxItemData>(diags.Count());
            foreach (var data in diags)
            {
                var item = new RadComboBoxItemData();
                item.Text = data.DiagnoseID + " - " + data.DiagnoseName;
                item.Value = data.DiagnoseID;
                result.Add(item);
            }
            return result.ToArray();
        }
    }
}
