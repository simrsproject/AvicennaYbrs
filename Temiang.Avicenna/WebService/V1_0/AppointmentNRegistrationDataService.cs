using System;
using System.Web.Services;
using System.Web.Script.Services;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.WebService.V1_0
{
    public class AppointmentNRegistrationDataService : V0.AppointmentNRegistrationDataService
    {
        #region MasterDataReferensi

        #region Paramedic
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicGetList(string AccessKey, string ParamedicID, string ParamedicName)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var parq = new ParamedicQuery("par");
                var suparq = new ServiceUnitParamedicQuery("supar");
                var suq = new ServiceUnitQuery("su");
                var appparq = new AppParameterQuery("apppar");

                parq.InnerJoin(suparq).On(parq.ParamedicID == suparq.ParamedicID)
                    .InnerJoin(suq).On(suparq.ServiceUnitID == suq.ServiceUnitID)
                    .InnerJoin(appparq).On(appparq.ParameterID == "OutPatientDepartmentID" && appparq.ParameterValue == suq.DepartmentID)
                    .Where(parq.IsActive == true, suq.IsActive == true, suq.SRRegistrationType == "OPR");

                SetListParameters(parq, "par.ParamedicID", ParamedicID);
                SetListParameters(parq, "par.ParamedicName", ParamedicName);

                parq.Select(parq.ParamedicID, parq.ParamedicName);
                parq.es.Distinct = true;
                var tbl = parq.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicSearch(string AccessKey, string Keyword)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var parq = new ParamedicQuery("par");
                var suparq = new ServiceUnitParamedicQuery("supar");
                var suq = new ServiceUnitQuery("su");
                var appparq = new AppParameterQuery("apppar");

                parq.InnerJoin(suparq).On(parq.ParamedicID == suparq.ParamedicID)
                    .InnerJoin(suq).On(suparq.ServiceUnitID == suq.ServiceUnitID)
                    .InnerJoin(appparq).On(appparq.ParameterID == "OutPatientDepartmentID" && appparq.ParameterValue == suq.DepartmentID)
                    .Where(parq.IsActive == true, suq.IsActive == true, suq.SRRegistrationType == "OPR");
                parq.Select(parq.ParamedicID, parq.ParamedicName);
                parq.es.Distinct = true;

                FieldDB[] ff = { 
                    new FieldDB(){ FieldName = "par.ParamedicID", FieldValue = Keyword } ,
                    new FieldDB(){ FieldName = "par.ParamedicName", FieldValue = Keyword } 
                };
                SetListParametersOR(parq, ff);
                var tbl = parq.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicGetOne(string AccessKey, string ParamedicID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(ParamedicMetadata.ColumnNames.ParamedicID, ParamedicID);

                var entity = new ParamedicQuery();
                entity.Where(entity.ParamedicID.Like(string.Format("{0}", ParamedicID)));

                entity.Select(entity.ParamedicID, entity.ParamedicName);
                var tbl = entity.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion

        #endregion
    }
}
