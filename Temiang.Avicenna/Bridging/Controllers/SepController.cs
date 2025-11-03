using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using Newtonsoft.Json;
using Temiang.Dal.Interfaces;
using System.Text;

namespace Temiang.Avicenna.Bridging.Controllers
{
    public class SepController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("sep/rencanakontrol/{noSuratKontrol}")]
        public HttpResponseMessage GetRencanaKontrol(string noSuratKontrol)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetRencanaKontrolByNoSuratKontrol(noSuratKontrol);
            if (response.MetaData.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.OK, response.Response);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, new
            {
                metaData = new
                {
                    code = response.MetaData.Code,
                    message = response.MetaData.Message
                }
            });
        }
    }
}