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
using Temiang.Avicenna.Bridging.Antrean.ParameterClass;

namespace Temiang.Avicenna.Bridging.Controllers
{
    public class EisDinkesController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("eis/kunjungan")]
        public HttpResponseMessage DataKunjunganPerPoli(EisDinkes.Json.DataKunjunganPerPoli.Request.Root param)
        {
            var h = new Healthcare();
            h.LoadByPrimaryKey(AppSession.Parameter.HealthcareID);

            var reg = new RegistrationQuery("a");
            var su = new ServiceUnitQuery("b");
            var sub = new ServiceUnitBridgingQuery("c");

            reg.Select(sub.BridgingID, reg.RegistrationNo.Count());
            reg.InnerJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID);
            reg.InnerJoin(sub).On(su.ServiceUnitID == sub.ServiceUnitID && sub.SRBridgingType == AppEnum.BridgingType.BPJS);
            reg.Where(reg.SRRegistrationType == AppConstant.RegistrationType.InPatient, reg.IsVoid == false);
            reg.Where($"<CAST(a.RegistrationDate + ' ' + a.RegistrationTime AS DATETIME) BETWEEN '{param.Date.Start}' AND '{param.Date.End}'>");
            reg.GroupBy(sub.BridgingID);

            var table = reg.LoadDataTable();
            if (table.AsEnumerable().Any())
            {
                var kunjungan = new List<EisDinkes.Json.DataKunjunganPerPoli.Response.Kunjungan>();
                foreach (DataRow row in table.Rows)
                {
                    kunjungan.Add(new EisDinkes.Json.DataKunjunganPerPoli.Response.Kunjungan()
                    {
                        KodePoli = row[0].ToString(),
                        Total = Convert.ToInt32(row[1])
                    });
                }

                return Request.CreateResponse(HttpStatusCode.OK, new EisDinkes.Json.DataKunjunganPerPoli.Response.Root()
                {
                    Code = HttpStatusCode.OK.ToString().ToInt(),
                    Messages = "SUCCESS",
                    Data = new EisDinkes.Json.DataKunjunganPerPoli.Response.Data()
                    {
                        Faskes = h.HealthcareName,
                        Kunjungan = kunjungan
                    }
                });
            }
            else return Request.CreateResponse(HttpStatusCode.OK, new EisDinkes.Json.DataKunjunganPerPoli.Response.Root()
            {
                Code = HttpStatusCode.OK.ToString().ToInt(),
                Messages = "SUCCESS",
                Data = new EisDinkes.Json.DataKunjunganPerPoli.Response.Data()
                {
                    Faskes = h.HealthcareName,
                    Kunjungan = null
                }
            });
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("eis/diagnosa")]
        public HttpResponseMessage DataDiagnosa(EisDinkes.Json.DataTop10Diagnosa.Request.Root param)
        {
            var h = new Healthcare();
            h.LoadByPrimaryKey(AppSession.Parameter.HealthcareID);

            var reg = new RegistrationQuery("a");
            var diag = new EpisodeDiagnoseQuery("b");

            reg.Select(reg.SRRegistrationType, diag.DiagnoseID, reg.RegistrationNo.Count());
            reg.InnerJoin(diag).On(reg.RegistrationNo == diag.RegistrationNo && diag.SRDiagnoseType == AppSession.Parameter.DiagnoseTypeMain && diag.IsVoid == false);
            reg.Where(reg.IsVoid == false);
            reg.Where($"<CAST(a.RegistrationDate + ' ' + a.RegistrationTime AS DATETIME) BETWEEN '{param.Date.Start}' AND '{param.Date.End}'>");
            reg.GroupBy(reg.SRRegistrationType, diag.DiagnoseID);

            var table = reg.LoadDataTable();
            if (table.AsEnumerable().Any())
            {
                var diagIgd = new List<EisDinkes.Json.DataTop10Diagnosa.Response.DiagnosaIgd>();
                foreach (DataRow row in table.AsEnumerable().Where(t => t.Field<string>("SRRegistrationType") == AppConstant.RegistrationType.EmergencyPatient))
                {
                    diagIgd.Add(new EisDinkes.Json.DataTop10Diagnosa.Response.DiagnosaIgd()
                    {
                        KodeDiagnosa = row[1].ToString(),
                        Total = Convert.ToInt32(row[2])
                    });
                }

                var diagOut = new List<EisDinkes.Json.DataTop10Diagnosa.Response.DiagnosaRalan>();
                foreach (DataRow row in table.AsEnumerable().Where(t => new string[] 
                {   
                    AppConstant.RegistrationType.OutPatient, 
                    AppConstant.RegistrationType.Ancillary, 
                    AppConstant.RegistrationType.ClusterPatient, 
                    AppConstant.RegistrationType.MedicalCheckUp 
                }.Contains(t.Field<string>("SRRegistrationType"))))
                {
                    diagOut.Add(new EisDinkes.Json.DataTop10Diagnosa.Response.DiagnosaRalan()
                    {
                        KodeDiagnosa = row[1].ToString(),
                        Total = Convert.ToInt32(row[2])
                    });
                }

                var diagRanap = new List<EisDinkes.Json.DataTop10Diagnosa.Response.DiagnosaRanap>();
                foreach (DataRow row in table.AsEnumerable().Where(t => t.Field<string>("SRRegistrationType") == AppConstant.RegistrationType.InPatient))
                {
                    diagRanap.Add(new EisDinkes.Json.DataTop10Diagnosa.Response.DiagnosaRanap()
                    {
                        KodeDiagnosa = row[1].ToString(),
                        Total = Convert.ToInt32(row[2])
                    });
                }

                return Request.CreateResponse(HttpStatusCode.OK, new EisDinkes.Json.DataTop10Diagnosa.Response.Root()
                {
                    Code = HttpStatusCode.OK.ToString().ToInt(),
                    Messages = "SUCCESS",
                    Data = new EisDinkes.Json.DataTop10Diagnosa.Response.Data()
                    {
                        Faskes = h.HealthcareName,
                        Diagnosa = new EisDinkes.Json.DataTop10Diagnosa.Response.Diagnosa()
                        {
                            DiagnosaIgd = diagIgd,
                            DiagnosaRalan = diagOut,
                            DiagnosaRanap = diagRanap
                        }
                    }
                });
            }
            else return Request.CreateResponse(HttpStatusCode.OK, new EisDinkes.Json.DataTop10Diagnosa.Response.Root()
            {
                Code = HttpStatusCode.OK.ToString().ToInt(),
                Messages = "SUCCESS",
                Data = new EisDinkes.Json.DataTop10Diagnosa.Response.Data()
                {
                    Faskes = h.HealthcareName,
                    Diagnosa = null
                }
            });
        }
    }
}