using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using Temiang.Avicenna.BusinessObject;
using System.Web.Services;


namespace Temiang.Avicenna.WebService.AvicennaMobile
{
    /// <summary>
    /// Summary description for PatientWsv
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PatientWsv : V0.BaseDataService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientGetByMedicalNo(string AccessKey, string MedicalNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(PatientMetadata.ColumnNames.MedicalNo, MedicalNo);

                var query = new PatientQuery("p");
                query.Select(query,query.Address, query.PatientName);
                query.Where(query.MedicalNo == MedicalNo);
                query.es.Top = 1;

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientSearch(string AccessKey, string Keyword)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired("KeyWord", Keyword);

                var query = new PatientQuery();
                FieldDB[] ff = { 
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.MedicalNo, FieldValue = Keyword } ,
                    new FieldDB(){ FieldName = "RTRIM(FirstName + ' ' + RTRIM(MiddleName + ' ' + LastName))", FieldValue = Keyword } 
                };
                SetListParametersOR(query, ff);

                var tbl = query.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void LastVisitPatient(string AccessKey, string MedicalNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(PatientMetadata.ColumnNames.MedicalNo, MedicalNo);

                var patient = new Patient();
                if (patient.LoadByMedicalNo(MedicalNo))
                {
                    var query = new RegistrationQuery("r");
                    var su = new ServiceUnitQuery("su");
                    query.InnerJoin(su).On(query.ServiceUnitID==su.ServiceUnitID);

                    var par = new ParamedicQuery("par");
                    query.InnerJoin(par).On(query.ParamedicID==par.ParamedicID);

                    var gr = new GuarantorQuery("gr");
                    query.InnerJoin(gr).On(query.GuarantorID==gr.GuarantorID);

                    query.es.Top = 1;
                    query.Where(query.PatientID == patient.PatientID);
                    query.OrderBy( query.RegistrationDate.Descending, query.RegistrationDateTime.Descending);

                    query.Select(query.RegistrationDateTime, query.RegistrationDate, query.RegistrationTime, su.ServiceUnitName, par.ParamedicName, gr.GuarantorName);

                    var tbl = query.LoadDataTable();

                    InspectOneResult(tbl);

                    WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
                }
                else
                {
                    throw new Exception(ErrDataNotFound);
                }
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientGuarantorHist(string AccessKey, string MedicalNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(PatientMetadata.ColumnNames.MedicalNo, MedicalNo);

                var patient = new Patient();
                if (patient.LoadByMedicalNo(MedicalNo))
                {
                    var query = new RegistrationQuery("r");
                    var guar = new GuarantorQuery("guar");
                    query.InnerJoin(guar).On(query.GuarantorID == guar.GuarantorID);
                    query.es.Distinct = true;
                    query.Where(query.PatientID == patient.PatientID);
                    query.OrderBy(guar.GuarantorName.Ascending);

                    query.Select(guar.GuarantorID, guar.GuarantorName);

                    var tbl = query.LoadDataTable();

                    WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
                }
                else
                {
                    throw new Exception(ErrDataNotFound);
                }
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void LastVitalSignValues(string AccessKey, string MedicalNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(PatientMetadata.ColumnNames.MedicalNo, MedicalNo);

                var patient = new Patient();
                if (patient.LoadByMedicalNo(MedicalNo))
                {
                    var query = new RegistrationQuery("r");
                    query.es.Top = 1;
                    query.Where(query.PatientID == patient.PatientID);
                    query.OrderBy(query.RegistrationDateTime.Descending);

                    var reg = new Registration();
                    if (reg.Load(query))
                    {

                        var tbl = VitalSign.VitalSignLastValue(reg.RegistrationNo, reg.FromRegistrationNo, true);

                        WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
                    }
                    else
                    {
                        throw new Exception(ErrDataNotFound);
                    }
                }
                else
                {
                    throw new Exception(ErrDataNotFound);
                }
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
    }
}
