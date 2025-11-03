using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Security;
using System.Web;
using System.Web.Services;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for LinkLis
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class LinkLis : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string LoadParameterItem()
        {
            var svc = new Common.LinkLis.Service();
            var pem = svc.GetListPemeriksaan();
            foreach (var e in pem.ListPemeriksaan)
            {
                if (e.ListPemeriksaan == string.Empty && e.NamaPemeriksaan == null) continue;

                var x = new BusinessObject.Interop.LINKLIS.ListPemeriksaan();
                x.es.Connection.Name = AppConstant.HIS_INTEROP.LINK_LIS_INTEROP_CONNECTION_NAME;
                x.Query.Where(x.Query.KodePemeriksaan == e.ListPemeriksaan, x.Query.NamaPemeriksaan == e.NamaPemeriksaan);
                if (x.Query.Load()) continue;
                x = new BusinessObject.Interop.LINKLIS.ListPemeriksaan()
                {
                    KodePemeriksaan = e.ListPemeriksaan,
                    NamaPemeriksaan = e.NamaPemeriksaan
                };
                x.es.Connection.Name = AppConstant.HIS_INTEROP.LINK_LIS_INTEROP_CONNECTION_NAME;
                x.Save();
            }

            var pems = new BusinessObject.Interop.LINKLIS.ListPemeriksaanCollection();
            pems.es.Connection.Name = AppConstant.HIS_INTEROP.LINK_LIS_INTEROP_CONNECTION_NAME;
            pems.LoadAll();
            foreach (var e in pems)
            {
                svc = new Common.LinkLis.Service();
                var par = svc.GetListParameter(e.KodePemeriksaan);
                foreach (var f in par.ListParameter)
                {
                    var x = new BusinessObject.Interop.LINKLIS.ListParameter();
                    x.es.Connection.Name = AppConstant.HIS_INTEROP.LINK_LIS_INTEROP_CONNECTION_NAME;
                    x.Query.Where(x.Query.KodeParameter == f.Kode, x.Query.KodePemeriksaan == e.KodePemeriksaan, x.Query.NamaParameter == f.NamaPemeriksaan);
                    if (x.Query.Load()) continue;
                    x = new BusinessObject.Interop.LINKLIS.ListParameter()
                    {
                        KodeParameter = f.Kode,
                        KodePemeriksaan = e.KodePemeriksaan,
                        NamaParameter = f.NamaPemeriksaan
                    };
                    x.es.Connection.Name = AppConstant.HIS_INTEROP.LINK_LIS_INTEROP_CONNECTION_NAME;
                    x.Save();
                }
            }

            return "Hello World";
        }
    }
}
