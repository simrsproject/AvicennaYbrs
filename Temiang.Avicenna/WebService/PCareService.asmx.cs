using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using DevExpress.XtraBars.Alerter;
using Microsoft.SqlServer.Server;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for RegistrationWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PCareService : System.Web.Services.WebService
    {
        [WebMethod]
        public string CheckBpjsNo(string bpjsNo)
        {
            var pCareUserID = ConfigurationManager.AppSettings["PCareUserID"];

            if (string.IsNullOrEmpty(bpjsNo))
                return "alert|BPJS No required for guarantor type BPJS.";

            // Download PCare Data
            var bridging = new Bridging.PCare.BusinessObject.Peserta();
            try
            {
                var result = bridging.SaveToLocalDataBase(bpjsNo);
                if (result == null)
                {
                    return "confirm|Problem retrieving data from PCare";
                }

                if (result.IsOk)
                {
                    var bpjsPeserta = new BpjsPeserta();
                    if (bpjsPeserta.LoadByPrimaryKey(bpjsNo))
                    {
                        return CheckBpjsStatus(bpjsPeserta, pCareUserID);
                    }
                    return "confirm|Problem retrieving data from PCare";
                }

                if (result.MetaData.Code == "204")
                    return string.Format("alert|BPJS No {0} not registered in PCare service.", bpjsNo);

                return string.Format("confirm|{0}", result.MetaData.MessageDescription);
            }
            catch (Exception ex)
            {
                return "confirm|Problem retrieving data from PCare\n" + ex.Message;
            }
        }

        private string CheckBpjsStatusFromLocalDB(string bpjsNo, string pCareUserID)
        {
            var bpjsPeserta = new BpjsPeserta();
            if (bpjsPeserta.LoadByPrimaryKey(bpjsNo))
            {
                return CheckBpjsStatus(bpjsPeserta, pCareUserID);
            }
            return "confirm|Problem retrieving data BPJS. ";
        }

        private string CheckBpjsStatus(BpjsPeserta bpjsPeserta, string pCareUserID)
        {
            var info = string.Empty;
            if (!bpjsPeserta.Aktif ?? false)
            {
                info = string.Format("- Not active with status {0}\n", bpjsPeserta.KetAktif);
            }
            if (bpjsPeserta.KdProviderPst_kdProvider != pCareUserID)
            {
                info = string.Concat(info, string.Format("- This BPJS member [{0}] {1}, .\n", bpjsPeserta.KdProviderPst_kdProvider, bpjsPeserta.KdProviderPst_nmProvider));
            }

            if (!string.IsNullOrEmpty(info))
            {
                var title = string.Format("confirm|Status BPJS no {0} {1} :\n\n", bpjsPeserta.NoKartu, bpjsPeserta.Nama);
                return string.Concat(title, info, "\n");
            }
            return "OK";
        }

    }
}
