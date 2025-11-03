using System;
using System.Linq;
using System.Web.Services;
using System.Web.Script.Services;
using Temiang.Avicenna.BusinessObject;
using System.Data;

namespace Temiang.Avicenna.WebService.V1_1
{
    public class AppointmentNRegistrationDataService : V0.AppointmentNRegistrationDataService
    {
        #region MasterDataReferensi

        #region Paramedic
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicGetList(string AccessKey, string ParamedicID, string ParamedicName, string SmfID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var parq = new ParamedicQuery("par");
                var suparq = new ServiceUnitParamedicQuery("supar");
                var suq = new ServiceUnitQuery("su");
                var appparq = new AppParameterQuery("apppar");
                var smf = new SmfQuery("smf");
                var usr = new AppUserQuery("usr");

                parq.InnerJoin(suparq).On(parq.ParamedicID == suparq.ParamedicID)
                    .InnerJoin(suq).On(suparq.ServiceUnitID == suq.ServiceUnitID)
                    .InnerJoin(appparq).On(appparq.ParameterID == "OutPatientDepartmentID" && appparq.ParameterValue == suq.DepartmentID)
                    .InnerJoin(smf).On(parq.SRParamedicRL1 == smf.SmfID)
                    .LeftJoin(usr).On(parq.ParamedicID == usr.ParamedicID)
                    .Where(parq.IsActive == true, suq.IsActive == true, suq.SRRegistrationType == "OPR");

                SetListParameters(parq, "par.ParamedicID", ParamedicID);

                FieldDB[] ffAnd = {
                    new FieldDB(){ FieldName = "par.ParamedicName", FieldValue = ParamedicName, FieldIdentifier = "ParamedicName" },
                    new FieldDB(){ FieldName = "par.SRParamedicRL1", FieldValue = SmfID, FieldIdentifier = "SmfID" },
                };
                SetListParametersLike(parq, ffAnd);

                parq.Select(parq.ParamedicID, parq.ParamedicName,
                    parq.SRParamedicRL1.As("SmfID"), smf.SmfName, usr.UserID);
                parq.es.Distinct = true;
                var tbl = parq.LoadDataTable();
                AttachImage(tbl);
                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicGetOne(string AccessKey, string ParamedicID, string UserID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(ParamedicMetadata.ColumnNames.ParamedicID, ParamedicID);
                InspectStringRequired(AppUserMetadata.ColumnNames.UserID, UserID);

                var parq = new ParamedicQuery("parq");
                var smf = new SmfQuery("smf");
                var usr = new AppUserQuery("usr");

                parq
                    .InnerJoin(smf).On(parq.SRParamedicRL1 == smf.SmfID)
                    .LeftJoin(usr).On(parq.ParamedicID == usr.ParamedicID)
                    .Where(parq.ParamedicID.Equal(string.Format("{0}", ParamedicID)), usr.UserID.Equal(string.Format("{0}", UserID)));

                parq.Select(parq.ParamedicID, parq.ParamedicName,
                    parq.SRParamedicRL1.As("SmfID"), smf.SmfName, usr.UserID);
                var tbl = parq.LoadDataTable();
                AttachImage(tbl);
                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        private void AttachImage(DataTable dt) {

            var parids = dt.AsEnumerable().Select(x => x["ParamedicID"].ToString()).Distinct().ToArray();
            var parColl = new ParamedicCollection();
            if (parids.Count() > 0) {
                parColl.Query.Where(parColl.Query.ParamedicID.In(parids));
                parColl.LoadAll();
            }
            var c = dt.Columns.Add("Foto64", typeof(string));
            foreach (System.Data.DataRow r in dt.Rows)
            {
                var par = parColl.Where(x => x.ParamedicID == r["ParamedicID"].ToString()).FirstOrDefault();
                if (par != null) {
                    if (par.Foto != null) {
                        r["Foto64"] = Convert.ToBase64String(par.Foto);
                    }
                }
            }
            dt.AcceptChanges();
        }
        #endregion

        #region SMF
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SmfGetList(string AccessKey, string SmfID, string SmfName)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var smfq = new SmfQuery("smf");

                FieldDB[] ff = {
                    new FieldDB(){ FieldName = SmfMetadata.ColumnNames.SmfID, FieldValue = SmfID } ,
                    new FieldDB(){ FieldName = SmfMetadata.ColumnNames.SmfName, FieldValue = SmfName }
                };
                SetListParametersLike(smfq, ff);
                smfq.Select(smfq.SmfID, smfq.SmfName);

                var tbl = smfq.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SmfGetOne(string AccessKey, string SmfID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(SmfMetadata.ColumnNames.SmfID, SmfID);

                var entity = new SmfQuery();
                entity.Where(entity.SmfID.Like(string.Format("{0}", SmfID)));

                entity.Select(entity.SmfID, entity.SmfName);
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
