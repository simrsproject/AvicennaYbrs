using System;
using System.Linq;
using System.Web.Services;
using System.Web.Script.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.Interfaces;
//using Telerik.Reporting;
using DevExpress.Office.Utils;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using DevExpress.XtraPrinting;
using System.IO;
using System.Net;

namespace Temiang.Avicenna.WebService.V0
{
    public class AppointmentNRegistrationDataService : BaseDataService
    {
        #region MasterDataReferensi
        #region ServiceUnit
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitGetList(string AccessKey, string ServiceUnitID, string ServiceUnitName)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var suq = new ServiceUnitQuery("su");
                var parq = new ParamedicQuery("par");
                var suparq = new ServiceUnitParamedicQuery("supar");
                var appparq = new AppParameterQuery("apppar");

                suq.InnerJoin(suparq).On(suq.ServiceUnitID == suparq.ServiceUnitID)
                    .InnerJoin(parq).On(suparq.ParamedicID == parq.ParamedicID)
                    .InnerJoin(appparq).On(appparq.ParameterID == "OutPatientDepartmentID" && appparq.ParameterValue == suq.DepartmentID)
                    .Where(parq.IsActive == true, suq.IsActive == true, suq.SRRegistrationType == "OPR");

                var str = AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion);
                if (str == "GRHA")
                {
                    suq.Where(suq.IsShowOnKiosk == true);
                }

                SetListParameters(suq, "su.ServiceUnitID", ServiceUnitID);
                SetListParameters(suq, "su.ServiceUnitName", ServiceUnitName);

                suq.Select(suq.ServiceUnitID, suq.ServiceUnitName);
                suq.es.Distinct = true;
                var tbl = suq.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitSearch(string AccessKey, string Keyword)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var suq = new ServiceUnitQuery("su");
                var parq = new ParamedicQuery("par");
                var suparq = new ServiceUnitParamedicQuery("supar");
                var appparq = new AppParameterQuery("apppar");

                suq.InnerJoin(suparq).On(suq.ServiceUnitID == suparq.ServiceUnitID)
                    .InnerJoin(parq).On(suparq.ParamedicID == parq.ParamedicID)
                    .InnerJoin(appparq).On(appparq.ParameterID == "OutPatientDepartmentID" && appparq.ParameterValue == suq.DepartmentID)
                    .Where(parq.IsActive == true, suq.IsActive == true, suq.SRRegistrationType == "OPR");

                var str = AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion);
                if (str == "GRHA")
                {
                    suq.Where(suq.IsShowOnKiosk == true);
                }

                FieldDB[] ff = {
                    new FieldDB(){ FieldName = "su.ServiceUnitID", FieldValue = Keyword } ,
                    new FieldDB(){ FieldName = "su.ServiceUnitName", FieldValue = Keyword }
                };
                SetListParametersOR(suq, ff);

                suq.Select(suq.ServiceUnitID, suq.ServiceUnitName);
                suq.es.Distinct = true;

                var tbl = suq.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitGetOne(string AccessKey, string ServiceUnitID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(ServiceUnitMetadata.ColumnNames.ServiceUnitID, ServiceUnitID);

                var entity = new ServiceUnitQuery();
                entity.Where(entity.ServiceUnitID.Like(string.Format("{0}", ServiceUnitID)));

                entity.Select(entity.ServiceUnitID, entity.ServiceUnitName);
                var tbl = entity.LoadDataTable();

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
        public void ServiceUnitMedicalSupport(string AccessKey, string Keyword)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var suq = new ServiceUnitQuery("su");

                suq.Where(suq.IsActive == true);

                bool isKeyWord = false;
                if (Keyword.ToLower().Contains("joborder"))
                {
                    suq.Where(suq.IsUsingJobOrder == true);
                    isKeyWord = true;
                }
                if (Keyword.ToLower().Contains("pharmacy") || Keyword.ToLower().Contains("dispensary"))
                {
                    suq.Where(suq.IsDispensaryUnit == true);
                    isKeyWord = true;
                }
                if (!isKeyWord)
                {
                    throw new Exception(ErrFieldInvalidValue.Replace(
                        GetErrorMessage(ErrFieldInvalidValue),
                        "Invalid keyword. Available keyword: joborder, pharmacy"));
                }


                suq.Select(suq.ServiceUnitID, suq.ServiceUnitName);
                suq.es.Distinct = true;

                var tbl = suq.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion

        #region ServiceUnitParamedic
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitParamedicGetList(string AccessKey, string ServiceUnitID, string ParamedicID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var suparq = new ServiceUnitParamedicQuery("supar");
                var suq = new ServiceUnitQuery("su");
                var parq = new ParamedicQuery("par");
                var appparq = new AppParameterQuery("apppar");

                suparq.InnerJoin(suq).On(suq.ServiceUnitID == suparq.ServiceUnitID)
                    .InnerJoin(parq).On(suparq.ParamedicID == parq.ParamedicID)
                    .InnerJoin(appparq).On(appparq.ParameterID == "OutPatientDepartmentID" && appparq.ParameterValue == suq.DepartmentID)
                    .Where(parq.IsActive == true, suq.IsActive == true, suq.SRRegistrationType == "OPR");

                var str = AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion);
                if (str == "GRHA")
                {
                    suparq.Where(suparq.IsUsingQue == true, suq.IsShowOnKiosk == true);
                }

                SetListParameters(suparq, "su.ServiceUnitID", ServiceUnitID);
                SetListParameters(suparq, "par.ParamedicID", ParamedicID);

                suparq.Select(suq.ServiceUnitID, suq.ServiceUnitName, parq.ParamedicID, parq.ParamedicName,
                    suparq.DefaultRoomID, suparq.IsUsingQue,
                    @"<CASE WHEN (supar.IsAcceptBPJS = 1 AND supar.IsAcceptNonBPJS = 1) THEN 'Multiple' WHEN supar.IsAcceptBPJS = 1 THEN 'BPJS' WHEN supar.IsAcceptNonBPJS = 1 THEN 'Non BPJS' ELSE '-' END AS 'Type'>");

                var tbl = suparq.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion

        #region Guarantor
        /// <summary>
        /// Get Guarantor List
        /// </summary>
        /// <param name="GuarantorID">GuarantorID Optional</param>
        /// <param name="GuarantorName">GuarantorName Optional</param>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GuarantorGetList(string AccessKey, string GuarantorID, string GuarantorName)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new GuarantorQuery();
                SetListParameters(query, GuarantorMetadata.ColumnNames.GuarantorID, GuarantorID);
                SetListParameters(query, GuarantorMetadata.ColumnNames.GuarantorName, GuarantorName);
                query.Select(query.GuarantorID, query.GuarantorName);
                query.Where(query.IsActive == true,
                    query.ContractStart <= DateTime.Now.Date,
                    query.ContractEnd >= DateTime.Now.Date);
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
        public void GuarantorSearch(string AccessKey, string Keyword)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new GuarantorQuery();
                FieldDB[] ff = {
                    new FieldDB(){ FieldName = GuarantorMetadata.ColumnNames.GuarantorID, FieldValue = Keyword } ,
                    new FieldDB(){ FieldName = GuarantorMetadata.ColumnNames.GuarantorName, FieldValue = Keyword }
                };
                SetListParametersOR(query, ff);
                query.Select(query.GuarantorID, query.GuarantorName);
                query.Where(query.IsActive == true,
                     query.ContractStart <= DateTime.Now.Date,
                     query.ContractEnd >= DateTime.Now.Date);
                var tbl = query.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        /// <summary>
        /// Get Guarantor By GurantorID
        /// </summary>
        /// <param name="GuarantorID">GuarantorID Required</param>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GuarantorGetOne(string AccessKey, string GuarantorID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(GuarantorMetadata.ColumnNames.GuarantorID, GuarantorID);

                var entity = new GuarantorQuery();
                if (!string.IsNullOrEmpty(GuarantorID))
                {
                    entity.Where(entity.GuarantorID.Like(string.Format("{0}", GuarantorID)));
                }

                entity.Select(entity.GuarantorID, entity.GuarantorName);
                var tbl = entity.LoadDataTable();

                InspectOneResult(tbl);

                var ret = tbl.AsEnumerable().Select(x =>
                    new
                    {
                        GuarantorID = x.Field<string>(GuarantorMetadata.ColumnNames.GuarantorID),
                        GuarantorName = x.Field<string>(GuarantorMetadata.ColumnNames.GuarantorName)
                    }).First();
                WriteResponseAndLog(log, JSonRetFormatted(ret));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion

        #region ZipCode
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ZipCodeGetList(string AccessKey, string ZipCode, string District, string County, string City)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ZipCodeQuery();
                SetListParameters(query, ZipCodeMetadata.ColumnNames.ZipCode, ZipCode);
                SetListParameters(query, ZipCodeMetadata.ColumnNames.District, District);
                SetListParameters(query, ZipCodeMetadata.ColumnNames.County, County);
                SetListParameters(query, ZipCodeMetadata.ColumnNames.City, City);
                query.Select(query.ZipCode, query.District, query.County, query.City);
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
        public void ZipCodeSearch(string AccessKey, string Keyword)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ZipCodeQuery();
                FieldDB[] ff = {
                    new FieldDB(){ FieldName = ZipCodeMetadata.ColumnNames.ZipCode, FieldValue = Keyword } ,
                    new FieldDB(){ FieldName = ZipCodeMetadata.ColumnNames.District, FieldValue = Keyword } ,
                    new FieldDB(){ FieldName = ZipCodeMetadata.ColumnNames.County, FieldValue = Keyword } ,
                    new FieldDB(){ FieldName = ZipCodeMetadata.ColumnNames.City, FieldValue = Keyword }
                };
                SetListParametersOR(query, ff);
                query.Select(query.ZipCode, query.District, query.County, query.City);
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
        public void ZipCodeGetOne(string AccessKey, string ZipCode)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(GuarantorMetadata.ColumnNames.ZipCode, ZipCode);

                var entity = new ZipCodeQuery();
                if (!string.IsNullOrEmpty(ZipCode))
                {
                    entity.Where(entity.ZipCode.Like(string.Format("{0}", ZipCode)));
                }
                entity.Select(entity.ZipCode, entity.District, entity.County, entity.City);
                var tbl = entity.LoadDataTable();

                InspectOneResult(tbl);

                var ret = tbl.AsEnumerable().Select(x =>
                    new
                    {
                        ZipCode = x.Field<string>(GuarantorMetadata.ColumnNames.ZipCode),
                        District = x.Field<string>(GuarantorMetadata.ColumnNames.District),
                        County = x.Field<string>(GuarantorMetadata.ColumnNames.County),
                        City = x.Field<string>(GuarantorMetadata.ColumnNames.City)
                    }).First();

                WriteResponseAndLog(log, JSonRetFormatted(ret));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion

        #region Appointment Status
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppointmentStatusGetList(string AccessKey, string AppointmentStatusID, string AppointmentStatusName)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppStandardReferenceItemQuery();
                query.Where(query.StandardReferenceID == "AppointmentStatus");
                query.Select(query.ItemID.As("AppointmentStatusID"), query.ItemName.As("AppointmentStatusName"));
                SetListParameters(query, AppStandardReferenceItemMetadata.ColumnNames.ItemID, AppointmentStatusID);
                SetListParameters(query, AppStandardReferenceItemMetadata.ColumnNames.ItemName, AppointmentStatusName);
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
        public void AppointmentStatusGetOne(string AccessKey, string AppointmentStatusID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired("AppointmentStatusID", AppointmentStatusID);

                var query = new AppStandardReferenceItemQuery();
                if (!string.IsNullOrEmpty(AppointmentStatusID))
                {
                    query.Where(query.ItemID.Like(string.Format("{0}", AppointmentStatusID)));
                }
                query.Where(query.StandardReferenceID == "AppointmentStatus");

                query.Select(query.ItemID.As("AppointmentStatusID"), query.ItemName.As("AppointmentStatusName"));

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion

        //#region VisitType
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public void VisitTypeGetListByServiceUnit(string ServiceUnitID)
        //{
        //    var log = LogAdd();
        //    try
        //    {
        //        InspectStringRequired(ServiceUnitVisitTypeMetadata.ColumnNames.ServiceUnitID, ServiceUnitID);

        //        var query = new VisitTypeQuery("a");
        //        var suvtQuery = new ServiceUnitVisitTypeQuery("suvt");
        //        query.InnerJoin(suvtQuery).On(query.VisitTypeID == suvtQuery.VisitTypeID)
        //            .Select(query.VisitTypeID, query.VisitTypeName);
        //        SetListParameters(query, "suvt.ServiceUnitID", ServiceUnitID);
        //        var tbl = query.LoadDataTable();

        //        WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
        //    }
        //}

        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public void VisitTypeGetOne(string VisitTypeID)
        //{
        //    var log = LogAdd();
        //    try
        //    {
        //        InspectStringRequired(VisitTypeMetadata.ColumnNames.VisitTypeID, VisitTypeID);

        //        var entity = new VisitTypeQuery();
        //        if (!string.IsNullOrEmpty(VisitTypeID))
        //        {
        //            entity.Where(entity.VisitTypeID.Like(string.Format("{0}", VisitTypeID)));
        //        }

        //        entity.Select(entity.VisitTypeID, entity.VisitTypeName);
        //        var tbl = entity.LoadDataTable();

        //        InspectOneResult(tbl);

        //        var ret = tbl.AsEnumerable().Select(x =>
        //            new
        //            {
        //                VisitTypeID = x.Field<string>(VisitTypeMetadata.ColumnNames.VisitTypeID),
        //                VisitTypeName = x.Field<string>(VisitTypeMetadata.ColumnNames.VisitTypeName)
        //            }).First();
        //        WriteResponseAndLog(log, JSonRetFormatted(ret));
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
        //    }
        //}
        //#endregion

        #region AppStdRef

        #region StandardRef
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void StandardRefGetList(string AccessKey, string StandardRefID, string ItemID, string ItemName)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired("StandardRefID", StandardRefID);

                var query = new AppStandardReferenceItemQuery();
                SetListParameters(query, AppStandardReferenceItemMetadata.ColumnNames.StandardReferenceID, StandardRefID);
                SetListParameters(query, AppStandardReferenceItemMetadata.ColumnNames.ItemID, ItemID);
                SetListParameters(query, AppStandardReferenceItemMetadata.ColumnNames.ItemName, ItemName);
                query.Select(query.ItemID, query.ItemName)
                    .Where(query.IsActive == true);

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
        public void StandardRefSearch(string AccessKey, string StandardRefID, string Keyword)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired("StandardRefID", StandardRefID);

                var query = new AppStandardReferenceItemQuery();

                SetListParameters(query, AppStandardReferenceItemMetadata.ColumnNames.StandardReferenceID, StandardRefID);

                FieldDB[] ff = {
                    new FieldDB(){ FieldName = AppStandardReferenceItemMetadata.ColumnNames.ItemID, FieldValue = Keyword } ,
                    new FieldDB(){ FieldName = AppStandardReferenceItemMetadata.ColumnNames.ItemName, FieldValue = Keyword }
                };
                SetListParametersOR(query, ff);
                query.Select(query.ItemID, query.ItemName)
                    .Where(query.IsActive == true);
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
        public void StandardRefGetOne(string AccessKey, string StandardRefName, string ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired("StandardRefName", StandardRefName);

                InspectStringRequired(AppStandardReferenceItemMetadata.ColumnNames.ItemID, ItemID);

                var query = new AppStandardReferenceItemQuery();
                if (!string.IsNullOrEmpty(ItemID))
                {
                    query.Where(query.StandardReferenceID == StandardRefName,
                        query.ItemID.Like(string.Format("{0}", ItemID)));
                }
                query.Select(query.ItemID, query.ItemName)
                    .Where(query.IsActive == true);
                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                var ret = tbl.AsEnumerable().Select(x =>
                    new
                    {
                        ItemID = x.Field<string>(AppStandardReferenceItemMetadata.ColumnNames.ItemID),
                        ItemName = x.Field<string>(AppStandardReferenceItemMetadata.ColumnNames.ItemName)
                    }).First();

                WriteResponseAndLog(log, JSonRetFormatted(ret));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion

        #endregion AppStdRef

        #endregion

        #region ParamedicScheduleDate
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicScheduleDateGetList(string AccessKey, string DateStart, string DateEnd, string ParamedicID, string ServiceUnitID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                if (string.IsNullOrEmpty(DateStart))
                    throw new Exception(ErrFieldRequired.Replace(GetErrorMessage(ErrFieldRequired), "DateStart required"));
                if (string.IsNullOrEmpty(DateEnd))
                    throw new Exception(ErrFieldRequired.Replace(GetErrorMessage(ErrFieldRequired), "DateEnd required"));

                var dateS = DateTime.ParseExact(DateStart, "yyyy-MM-dd", null);
                var dateE = DateTime.ParseExact(DateEnd, "yyyy-MM-dd", null);

                var query = new ParamedicScheduleDateQuery("psd");
                var par = new ParamedicQuery("p");
                var su = new ServiceUnitQuery("su");
                var ot = new OperationalTimeQuery("ot");
                var ps = new ParamedicScheduleQuery("ps");

                query.InnerJoin(ps).On(query.ServiceUnitID == ps.ServiceUnitID && query.ParamedicID == ps.ParamedicID && query.PeriodYear == ps.PeriodYear)
                    .InnerJoin(par).On(query.ParamedicID == par.ParamedicID)
                    .InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID)
                    .InnerJoin(ot).On(query.OperationalTimeID == ot.OperationalTimeID)
                    .Select(
                        query.ServiceUnitID,
                        su.ServiceUnitName,
                        query.ParamedicID,
                        par.ParamedicName,
                        query.ScheduleDate,
                        su.SrqueueinglocationReg,
                        ps.Quota,
                        ps.QuotaBpjs,
                        ot.OperationalTimeName,
                        ot.StartTime1, ot.EndTime1,
                        ot.StartTime2, ot.EndTime2,
                        ot.StartTime3, ot.EndTime3,
                        ot.StartTime4, ot.EndTime4,
                        ot.StartTime5, ot.EndTime5
                    ).Where(query.ScheduleDate.Between(dateS, dateE), su.IsActive == true);

                SetListParameters(query, "psd.ParamedicID", ParamedicID);
                SetListParameters(query, "psd.ServiceUnitID", ServiceUnitID);
                var tbl = query.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion

        #region Patient
        private void PatientSelect(PatientQuery query)
        {
            query.Select(
                        query.PatientID, query.MedicalNo,
                        query.FirstName, query.MiddleName, query.LastName, query.DateOfBirth,
                        query.StreetName, query.District, query.City, query.County, query.State, query.ZipCode,
                        query.PhoneNo, query.MobilePhoneNo, query.Email, query.Ssn, query.GuarantorID,
                        query.CityOfBirth.As("BirthPlace"), query.Sex
                    );
            query.Where(query.IsActive == 1);
            /*
            - Agama
            - Pendidikan
            - Pekerjaan
            - Status Pernikahan
            - Nama Keluarga
            - Alamat Keluarga
            - Hubungan Keluarga dengan pasien
            - Nomor Telepon Keluarga
            */
            var rlg = new AppStandardReferenceItemQuery("rlg");
            var edu = new AppStandardReferenceItemQuery("edu");
            var ocu = new AppStandardReferenceItemQuery("ocu");
            var mar = new AppStandardReferenceItemQuery("mar");
            var ttl = new AppStandardReferenceItemQuery("ttl");


            query.LeftJoin(rlg).On(rlg.StandardReferenceID == "Religion" && query.SRReligion == rlg.ItemID)
                .Select(query.SRReligion, rlg.ItemName.As("SRReligionName"))
                .LeftJoin(edu).On(edu.StandardReferenceID == "Education" && query.SREducation == edu.ItemID)
                .Select(query.SREducation, edu.ItemName.As("SREducationName"))
                .LeftJoin(ocu).On(ocu.StandardReferenceID == "Occupation" && query.SROccupation == ocu.ItemID)
                .Select(query.SROccupation, ocu.ItemName.As("SROccupationName"))
                .LeftJoin(mar).On(mar.StandardReferenceID == "MaritalStatus" && query.SRMaritalStatus == mar.ItemID)
                .Select(query.SRMaritalStatus, mar.ItemName.As("SRMaritalStatusName"))
                .LeftJoin(ttl).On(ttl.StandardReferenceID == "Title" && query.SRTitle == ttl.ItemID)
                .Select(query.SRTitle, ttl.ItemName.As("SRTitleName"));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientGetOne(string AccessKey, string PatientID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(PatientMetadata.ColumnNames.PatientID, PatientID);

                var query = new PatientQuery("p");
                query.Where(query.PatientID == PatientID);
                PatientSelect(query);

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
        public void PatientGetOneByMedicalNo(string AccessKey, string MedicalNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(PatientMetadata.ColumnNames.MedicalNo, MedicalNo);

                var query = new PatientQuery("p");
                query.Where(query.MedicalNo == MedicalNo);
                PatientSelect(query);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientUpdate(string AccessKey, string MedicalNo, string Sex, string StreetName, string District, string County, string City,  string State, string ZipCode,
             string PhoneNo, string Email,  string MobilePhoneNo
            )
        {
            var log = LogAdd();
            try
            {
                var UserID = ValidateAccessKey(AccessKey);

                InspectStringRequired(PatientMetadata.ColumnNames.MedicalNo, MedicalNo);

                SetUserLoginSession(UserID);

                // 
                var Patient = PatientSetEntityValue(MedicalNo, Sex,
                    StreetName, District, County, City, State, ZipCode,
                    PhoneNo, Email, MobilePhoneNo
                    );

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(Patient)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        private DataRow PatientSetEntityValue(string medicalNo, string sex, string streetName, string district, string county,
          string city,  string state, string zipCode,
          string phoneNo, string email,  string mobilePhoneNo)
        {
            // Create a new Patient object
            var pt = new Patient();

            // Load the patient using the MedicalNo (assuming the method LoadByPrimaryKey is available in your class)
            if (pt.LoadByMedicalNo(medicalNo))
            {
                // Update the Patient fields with the new data
                pt.MobilePhoneNo = mobilePhoneNo;
                pt.PhoneNo = phoneNo;
                pt.Email = email;
                pt.StreetName = streetName;  
                pt.District = district;
                pt.County = county;
                pt.City = city;                   
                pt.State = state;            
                pt.ZipCode = zipCode;       
                pt.Sex = sex;               
                pt.LastUpdateByUserID = "WEBSERVICE";
                pt.LastUpdateDateTime = DateTime.Now; // Assuming you are using current datetime (adjust if using a specific format)

                // Save the updated Patient object
                pt.Save();  // Save the updates to the database or data source

                // After saving, create a DataTable to hold the updated patient data and return the updated row

                DataTable dt = new DataTable();

                // Define columns for the DataTable based on the Patient object
                dt.Columns.Add("PatientID");
                dt.Columns.Add("MedicalNo");
                dt.Columns.Add("StreetName");
                dt.Columns.Add("District");
                dt.Columns.Add("City");
                dt.Columns.Add("County");
                dt.Columns.Add("State");
                dt.Columns.Add("ZipCode");
                dt.Columns.Add("PhoneNo");
                dt.Columns.Add("Email");
                dt.Columns.Add("Sex");
                dt.Columns.Add("MobilePhoneNo");
                dt.Columns.Add("LastUpdateByUserID");
                dt.Columns.Add("LastUpdateDateTime");

                // Create a DataRow to hold the updated patient data
                DataRow row = dt.NewRow();
                row["PatientID"] = pt.PatientID;
                row["MedicalNo"] = pt.MedicalNo;
                row["StreetName"] = pt.StreetName;
                row["District"] = pt.District;
                row["City"] = pt.City;
                row["County"] = pt.County;
                row["State"] = pt.State;
                row["ZipCode"] = pt.ZipCode;
                row["PhoneNo"] = pt.PhoneNo;
                row["Email"] = pt.Email;
                row["Sex"] = pt.Sex;
                row["MobilePhoneNo"] = pt.MobilePhoneNo;
                row["LastUpdateByUserID"] = pt.LastUpdateByUserID;
                row["LastUpdateDateTime"] = pt.LastUpdateDateTime;

                // Return the DataRow with updated data
                return row;
            }
            else
            {
                // Handle the case where the patient is not found
                throw new Exception("Patient with the specified ID not found.");
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientGetPhoto(string AccessKey, string PatientID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(PatientMetadata.ColumnNames.PatientID, PatientID);

                var ph = new PatientImage();
                if (ph.LoadByPrimaryKey(PatientID))
                {
                    WriteResponseAndLog(log, JSonRetFormatted(Convert.ToBase64String(ph.Photo)));
                }
                else
                {
                    WriteResponseAndLog(log, JSonRetFormatted(null));
                }
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientUpdatePhoto(string AccessKey, string MedicalNo, string FileData)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(PatientMetadata.ColumnNames.MedicalNo, MedicalNo);

                var patientImageData = PatientUpdatePhotoSetEntityValue(MedicalNo, FileData);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(patientImageData)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        private DataRow PatientUpdatePhotoSetEntityValue(string MedicalNo, string FileData)
        {
            var pat = new Patient();
            if (pat.LoadByMedicalNo(MedicalNo))
            {
                var imgByteArr = File.ReadAllBytes(FileData);

                var pi = new PatientImage();
                if (pi.LoadByPrimaryKey(pat.PatientID))
                {
                    // Update the Patient fields with the new data
                    pi.Photo = imgByteArr;
                    pi.LastUpdateByUserID = "WEBSERVICE";
                    pi.LastUpdateDateTime = DateTime.Now; // Assuming you are using current datetime (adjust if using a specific format)
                }
                else
                {
                    pi.AddNew();
                    pi.PatientID = pat.PatientID;
                    pi.Photo = imgByteArr;
                    pi.LastUpdateByUserID = "WEBSERVICE";
                    pi.LastUpdateDateTime = DateTime.Now; // Assuming you are using current datetime (adjust if using a specific format)
                }

                // Save the updated Patient object
                pi.Save();  // Save the updates to the database or data source

                // After saving, create a DataTable to hold the updated patient data and return the updated row
                DataTable dt = new DataTable();

                // Define columns for the DataTable based on the Patient object
                dt.Columns.Add("PatientID");
                dt.Columns.Add("Photo");
                dt.Columns.Add("LastUpdateByUserID");
                dt.Columns.Add("LastUpdateDateTime");

                // Create a DataRow to hold the updated patient data
                DataRow row = dt.NewRow();
                row["PatientID"] = pi.PatientID;
                row["Photo"] = pi.Photo;
                row["LastUpdateByUserID"] = pi.LastUpdateByUserID;
                row["LastUpdateDateTime"] = pi.LastUpdateDateTime;

                // Return the DataRow with updated data
                return row;

            }
            else
            {
                // Handle the case where the patient is not found
                throw new Exception("Patient with the specified ID not found.");
            }
        }

        static void Main(string fileData)
        {
            string filePath = fileData;  // Path ke file gambar lokal
            string url = "http://localhost:56211/WebService/V1_1/AppointmentWS.asmx/PatientUpdatePhoto";  // URL layanan ASMX

            // Baca file dan konversi ke byte array
            byte[] fileBytes = File.ReadAllBytes(filePath);

            // Buat permintaan HTTP POST
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            // Convert byte array ke Base64 string untuk dikirim melalui jaringan
            string base64File = Convert.ToBase64String(fileBytes);

            // Buat data POST
            string postData = $"fileData={Uri.EscapeDataString(base64File)}&fileName={Uri.EscapeDataString(Path.GetFileName(filePath))}";

            // Tulis data POST ke request stream
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(postData);
            }

            // Dapatkan respon dari server
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string result = reader.ReadToEnd();
                    Console.WriteLine("Response: " + result);
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientSearchByField(string AccessKey, string MedicalNo, string Name,
            string DateOfBirth, string Address, string PhoneNo, string Email, string Ssn)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                FieldDB[] ff = {
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.MedicalNo, FieldValue = MedicalNo } ,
                    new FieldDB(){ FieldName = "RTRIM(FirstName + ' ' + RTRIM(MiddleName + ' ' + LastName))", FieldValue = Name, FieldIdentifier = "Name" } ,
                    new FieldDB(){ FieldName = "CONVERT(char(10), DateOfBirth,120)", FieldValue = DateOfBirth, FieldIdentifier = "DateOfBirth (yyyy-MM-dd)" } ,
                    new FieldDB(){ FieldName = "RTRIM(StreetName + ' ' + RTRIM(District + ' ' + RTRIM(City + ' ' + RTRIM(County + ' ' + RTRIM(State + ' ' + ISNULL(ZipCode,''))))))", FieldValue = Address, FieldIdentifier = "Address" } ,
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.PhoneNo, FieldValue = PhoneNo } ,
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.MobilePhoneNo, FieldValue = PhoneNo } ,
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.Email, FieldValue = Email },
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.Ssn, FieldValue = Ssn }
                };

                InspectStringRequiredOR(ff);

                var query = new PatientQuery("p");
                PatientSelect(query);

                FieldDB[] ffAnd = {
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.MedicalNo, FieldValue = MedicalNo } ,
                    new FieldDB(){ FieldName = "RTRIM(FirstName + ' ' + RTRIM(MiddleName + ' ' + LastName))", FieldValue = Name, FieldIdentifier = "Name" } ,
                    //new FieldDB(){ FieldName = "CONVERT(char(10), DateOfBirth,120)", FieldValue = DateOfBirth, FieldIdentifier = "DateOfBirth (yyyy-MM-dd)" } ,
                    new FieldDB(){ FieldName = "RTRIM(StreetName + ' ' + RTRIM(District + ' ' + RTRIM(City + ' ' + RTRIM(County + ' ' + RTRIM(State + ' ' + ISNULL(ZipCode,''))))))", FieldValue = Address, FieldIdentifier = "Address" } ,
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.Email, FieldValue = Email },
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.Ssn, FieldValue = Ssn }
                };
                SetListParametersLike(query, ffAnd);
                if (!string.IsNullOrEmpty(DateOfBirth))
                {
                    SetListParametersEquals(query, "DateOfBirth", DateOfBirth);
                }

                FieldDB[] ffOr = {
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.PhoneNo, FieldValue = PhoneNo } ,
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.MobilePhoneNo, FieldValue = PhoneNo }
                };
                SetListParametersOR(query, ffOr);

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
        public void PatientSearchByFieldV2(string AccessKey, string MedicalNo, string Name,
            string DateOfBirth, string Address, string PhoneNo, string Email, string Ssn, string Sex)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                FieldDB[] ff = {
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.MedicalNo, FieldValue = MedicalNo } ,
                    new FieldDB(){ FieldName = "RTRIM(FirstName + ' ' + RTRIM(MiddleName + ' ' + LastName))", FieldValue = Name, FieldIdentifier = "Name" } ,
                    new FieldDB(){ FieldName = "CONVERT(char(10), DateOfBirth,120)", FieldValue = DateOfBirth, FieldIdentifier = "DateOfBirth (yyyy-MM-dd)" } ,
                    new FieldDB(){ FieldName = "RTRIM(StreetName + ' ' + RTRIM(District + ' ' + RTRIM(City + ' ' + RTRIM(County + ' ' + RTRIM(State + ' ' + ISNULL(ZipCode,''))))))", FieldValue = Address, FieldIdentifier = "Address" } ,
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.PhoneNo, FieldValue = PhoneNo } ,
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.MobilePhoneNo, FieldValue = PhoneNo } ,
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.Email, FieldValue = Email },
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.Ssn, FieldValue = Ssn }
                };

                InspectStringRequiredOR(ff);

                var query = new PatientQuery("p");
                PatientSelect(query);

                FieldDB[] ffAnd = {
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.MedicalNo, FieldValue = MedicalNo } ,
                    new FieldDB(){ FieldName = "RTRIM(FirstName + ' ' + RTRIM(MiddleName + ' ' + LastName))", FieldValue = Name, FieldIdentifier = "Name" } ,
                    new FieldDB(){ FieldName = "CONVERT(char(10), DateOfBirth,120)", FieldValue = DateOfBirth, FieldIdentifier = "DateOfBirth (yyyy-MM-dd)" } ,
                    new FieldDB(){ FieldName = "RTRIM(StreetName + ' ' + RTRIM(District + ' ' + RTRIM(City + ' ' + RTRIM(County + ' ' + RTRIM(State + ' ' + ISNULL(ZipCode,''))))))", FieldValue = Address, FieldIdentifier = "Address" } ,
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.Email, FieldValue = Email },
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.Ssn, FieldValue = Ssn },
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.Sex, FieldValue = Sex }
                };
                SetListParametersLike(query, ffAnd);

                FieldDB[] ffOr = {
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.PhoneNo, FieldValue = PhoneNo } ,
                    new FieldDB(){ FieldName = PatientMetadata.ColumnNames.MobilePhoneNo, FieldValue = PhoneNo }
                };
                SetListParametersOR(query, ffOr);

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
        public void PatientSearch(string AccessKey, string Keyword)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired("KeyWord", Keyword);

                var query = new PatientQuery("p");
                PatientSelect(query);
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
        public void PatientEmergencyContactGetOne(string AccessKey, string PatientID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(PatientMetadata.ColumnNames.PatientID, PatientID);

                var query = new PatientEmergencyContactQuery("p");
                query.Where(query.PatientID == PatientID);
                query.Select(
                    query.PatientID, query.ContactName, query.SRRelationship,
                    query.StreetName, query.District, query.City, query.County, query.State, query.ZipCode,
                    query.PhoneNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        #endregion

        #region UserRegistration
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppUserListByGroup(string AccessKey, string UserGroupID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(AppUserGroupMetadata.ColumnNames.UserGroupID, UserGroupID);

                var u = new AppUserQuery("u");
                var uug = new AppUserUserGroupQuery("uug");
                var ug = new AppUserGroupQuery("ug");
                u.InnerJoin(uug).On(u.UserID == uug.UserID)
                    .InnerJoin(ug).On(uug.UserGroupID == ug.UserGroupID)
                    .Where(uug.UserGroupID == UserGroupID)
                    .Select(u.UserID, u.UserName, uug.UserGroupID, ug.UserGroupName);

                var tbl = u.LoadDataTable();

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataTabletoObject(tbl)));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion

        #region Appointment common
        public static DataTable GenerateDataTableSlots(string Version)
        {
            var dtb = new DataTable("AppointmentSlotTime");

            //column
            var dc = new DataColumn("ServiceUnitID", Type.GetType("System.String"));
            dtb.Columns.Add(dc);
            dc = new DataColumn("ServiceUnitName", Type.GetType("System.String"));
            dtb.Columns.Add(dc);
            dc = new DataColumn("ParamedicID", Type.GetType("System.String"));
            dtb.Columns.Add(dc);
            dc = new DataColumn("ParamedicName", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("AppointmentDate", Type.GetType("System.DateTime"));
            dtb.Columns.Add(dc);
            dc = new DataColumn("AppointmentTime", Type.GetType("System.String"));
            dtb.Columns.Add(dc);
            dc = new DataColumn("AppointmentQue", Type.GetType("System.String"));
            dtb.Columns.Add(dc);
            dc = new DataColumn("AppointmentNo", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("PatientID", Type.GetType("System.String"));
            dtb.Columns.Add(dc);
            dc = new DataColumn("FirstName", Type.GetType("System.String"));
            dtb.Columns.Add(dc);
            dc = new DataColumn("MiddleName", Type.GetType("System.String"));
            dtb.Columns.Add(dc);
            dc = new DataColumn("LastName", Type.GetType("System.String"));
            dtb.Columns.Add(dc);
            dc = new DataColumn("DateOfBirth", Type.GetType("System.DateTime"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("StreetName", Type.GetType("System.String"));
            dtb.Columns.Add(dc);
            dc = new DataColumn("District", Type.GetType("System.String"));
            dtb.Columns.Add(dc);
            dc = new DataColumn("City", Type.GetType("System.String"));
            dtb.Columns.Add(dc);
            dc = new DataColumn("County", Type.GetType("System.String"));
            dtb.Columns.Add(dc);
            dc = new DataColumn("State", Type.GetType("System.String"));
            dtb.Columns.Add(dc);
            dc = new DataColumn("ZipCode", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("PhoneNo", Type.GetType("System.String"));
            dtb.Columns.Add(dc);
            dc = new DataColumn("Email", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("GuarantorID", Type.GetType("System.String"));
            dtb.Columns.Add(dc);
            dc = new DataColumn("GuarantorName", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("Notes", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("AppointmentStatus", Type.GetType("System.String"));
            dtb.Columns.Add(dc);
            dc = new DataColumn("AppointmentStatusName", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("MedicalNo", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("BirthPlace", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("Sex", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("Ssn", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            //dc = new DataColumn("ExamDuration", Type.GetType("System.Int32"));
            //dtb.Columns.Add(dc);

            if (Version != "1_0")
            {
                dc = new DataColumn("MobilePhoneNo", Type.GetType("System.String"));
                dtb.Columns.Add(dc);


                dc = new DataColumn("Quota", Type.GetType("System.String"));
                dtb.Columns.Add(dc);

                dc = new DataColumn("JmlhBO", Type.GetType("System.String"));
                dtb.Columns.Add(dc);

                dc = new DataColumn("JmlhReg", Type.GetType("System.String"));
                dtb.Columns.Add(dc);

                dc = new DataColumn("SisaQuota", Type.GetType("System.String"));
                dtb.Columns.Add(dc);
            }
            dc = new DataColumn("AppointmentQueFormattedNo", Type.GetType("System.String"));
            dtb.Columns.Add(dc);


            return dtb;
        }

        public static DataTable AppointmentSlotTime(string Version, string ServiceUnitID, string ParamedicID,
            DateTime dateStart, DateTime dateFinish)
        {
            //if (string.IsNullOrEmpty(ServiceUnitID))
            //    throw new Exception(ErrFieldRequired.Replace(GetErrorMessage(ErrFieldRequired), "ServiceUnitID required"));
            //if (string.IsNullOrEmpty(ParamedicID))
            //    throw new Exception(ErrFieldRequired.Replace(GetErrorMessage(ErrFieldRequired), "ParamedicID required"));

            var dtbSlot = GenerateDataTableSlots(Version);
            var dc = new DataColumn("CreateByUserID", Type.GetType("System.String"));
            dtbSlot.Columns.Add(dc);


            var sch = new ParamedicScheduleDateQuery("a");
            var ot = new OperationalTimeQuery("b");
            var par = new ParamedicScheduleQuery("c");
            var pld = new VwParamedicLeaveDateQuery("d");

            sch.es.Distinct = true;
            sch.Select(
                sch.ScheduleDate,
                sch.ServiceUnitID,
                sch.ParamedicID,
                ot.StartTime1,
                ot.EndTime1,
                ot.StartTime2,
                ot.EndTime2,
                ot.StartTime3,
                ot.EndTime3,
                ot.StartTime4,
                ot.EndTime4,
                ot.StartTime5,
                ot.EndTime5,
                par.ExamDuration,
                    @"<CAST(ISNULL(a.IsClosedTime1, 0) AS VARCHAR) AS 'IsClosedTime1'>",
                    @"<CAST(ISNULL(a.IsClosedTime2, 0) AS VARCHAR) AS 'IsClosedTime2'>",
                    @"<CAST(ISNULL(a.IsClosedTime3, 0) AS VARCHAR) AS 'IsClosedTime3'>",
                    @"<CAST(ISNULL(a.IsClosedTime4, 0) AS VARCHAR) AS 'IsClosedTime4'>",
                    @"<CAST(ISNULL(a.IsClosedTime5, 0) AS VARCHAR) AS 'IsClosedTime5'>",
                    @"<ISNULL(c.Quota, 0)+ISNULL(c.QuotaOnline, 0)+ISNULL(c.QuotaBpjs, 0)+ISNULL(c.QuotaBpjsOnline, 0) AS 'Quota'>",
                    @"<ISNULL(a.AddQuota, 0)+ISNULL(a.AddQuotaOnline, 0)+ISNULL(a.AddQuotaBpjs, 0)+ISNULL(a.AddQuotaBpjsOnline, 0) AS 'AddQuota'>"
                );
            sch.InnerJoin(ot).On(sch.OperationalTimeID == ot.OperationalTimeID);
            sch.InnerJoin(par).On(
                sch.ServiceUnitID == par.ServiceUnitID &&
                sch.ParamedicID == par.ParamedicID &&
                sch.PeriodYear == par.PeriodYear
                );
            sch.LeftJoin(pld).On(sch.ParamedicID == pld.ParamedicID && sch.ScheduleDate == pld.LeaveDate);

            sch.Where(
                sch.ScheduleDate.Between(dateStart, dateFinish)
                );
            if (!string.IsNullOrEmpty(ServiceUnitID))
            {
                sch.Where(sch.ServiceUnitID == ServiceUnitID);
            }
            if (!string.IsNullOrEmpty(ParamedicID))
            {
                sch.Where(sch.ParamedicID == ParamedicID);
            }
            var list = sch.LoadDataTable();

            double duration = 0;

            foreach (DataRow row in list.Rows)
            {
                duration = Convert.ToDouble(row["ExamDuration"]);

                int quota = 0;
                if (Convert.ToInt32(row["Quota"]) > 0)
                    quota = Convert.ToInt32(row["Quota"]) + Convert.ToInt32(row["AddQuota"]);

                //time 1
                if (row["StartTime1"].ToString().Trim() != string.Empty && row["EndTime1"].ToString().Trim() != string.Empty)
                {
                    var i = 1;
                    var dt1 = Convert.ToDateTime(row["ScheduleDate"]).Date + TimeSpan.Parse(row["StartTime1"].ToString().Trim());
                    var dt2 = Convert.ToDateTime(row["ScheduleDate"]).Date + TimeSpan.Parse(row["EndTime1"].ToString().Trim());
                    while (dt1 < dt2)
                    {
                        if (quota == 0 || i <= quota)
                        {
                            DataRow drSlot = dtbSlot.NewRow();
                            // Dirubah menggunakan nama columns karena error akibat ada penyisipan columns baru (Handono 230628)
                            //dr[0] = row["ServiceUnitID"].ToString();
                            //dr[1] = row["ParamedicID"].ToString();
                            //dr[2] = dt1.Date;
                            //dr[3] = dt1.ToString("HH:mm");
                            //dr[4] = i;

                            drSlot["ServiceUnitID"] = row["ServiceUnitID"].ToString();
                            drSlot["ParamedicID"] = row["ParamedicID"].ToString();
                            drSlot["AppointmentDate"] = dt1.Date;
                            drSlot["AppointmentTime"] = dt1.ToString("HH:mm");
                            drSlot["AppointmentQue"] = i;

                            #region comment
                            /*
                                0	ServiceUnitID
                                1	ParamedicID
                                2	AppointmentDate
                                3	AppointmentTime
                                4	AppointmentQue
                                5	AppointmentNo
                                6	PatientID
                                7	FirstName
                                8	MiddleName
                                9	LastName
                                0	DateOfBirth
                                1	StreetName
                                2	District
                                3	City
                                4	County
                                5	State
                                6	ZipCode
                                7	PhoneNo
                                8	Email
                                9	GuarantorID
                                0	Notes
                                1	SRAppointmentStatus
                             */
                            #endregion

                            dtbSlot.Rows.Add(drSlot);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                        else
                            break;

                    }
                }
                //time 2
                if (row["StartTime2"].ToString().Trim() != string.Empty && row["EndTime2"].ToString().Trim() != string.Empty)
                {
                    var i = 1;
                    var dt1 = Convert.ToDateTime(row["ScheduleDate"]).Date + TimeSpan.Parse(row["StartTime2"].ToString().Trim());
                    var dt2 = Convert.ToDateTime(row["ScheduleDate"]).Date + TimeSpan.Parse(row["EndTime2"].ToString().Trim());
                    while (dt1 < dt2)
                    {
                        if (quota == 0 || i <= quota)
                        {
                            DataRow drSlot = dtbSlot.NewRow();
                            // Dirubah menggunakan nama columns karena error akibat ada penyisipan columns baru dan supaya kedepannya aman (Handono 230628)
                            //dr[0] = row["ServiceUnitID"].ToString();
                            //dr[1] = row["ParamedicID"].ToString();
                            //dr[2] = dt1.Date;
                            //dr[3] = dt1.ToString("HH:mm");
                            //dr[4] = i;

                            drSlot["ServiceUnitID"] = row["ServiceUnitID"].ToString();
                            drSlot["ParamedicID"] = row["ParamedicID"].ToString();
                            drSlot["AppointmentDate"] = dt1.Date;
                            drSlot["AppointmentTime"] = dt1.ToString("HH:mm");
                            drSlot["AppointmentQue"] = i;

                            dtbSlot.Rows.Add(drSlot);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                        else
                            break;
                    }
                }
                //time 3
                if (row["StartTime3"].ToString().Trim() != string.Empty && row["EndTime3"].ToString().Trim() != string.Empty)
                {
                    var i = 1;
                    var dt1 = Convert.ToDateTime(row["ScheduleDate"]).Date + TimeSpan.Parse(row["StartTime3"].ToString().Trim());
                    var dt2 = Convert.ToDateTime(row["ScheduleDate"]).Date + TimeSpan.Parse(row["EndTime3"].ToString().Trim());
                    while (dt1 < dt2)
                    {
                        if (quota == 0 || i <= quota)
                        {
                            DataRow drSlot = dtbSlot.NewRow();
                            // Dirubah menggunakan nama columns karena error akibat ada penyisipan columns baru dan supaya kedepannya aman (Handono 230628)
                            //dr[0] = row["ServiceUnitID"].ToString();
                            //dr[1] = row["ParamedicID"].ToString();
                            //dr[2] = dt1.Date;
                            //dr[3] = dt1.ToString("HH:mm");
                            //dr[4] = i;

                            drSlot["ServiceUnitID"] = row["ServiceUnitID"].ToString();
                            drSlot["ParamedicID"] = row["ParamedicID"].ToString();
                            drSlot["AppointmentDate"] = dt1.Date;
                            drSlot["AppointmentTime"] = dt1.ToString("HH:mm");
                            drSlot["AppointmentQue"] = i;

                            dtbSlot.Rows.Add(drSlot);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                        else
                            break;
                    }
                }
                //time 4
                if (row["StartTime4"].ToString().Trim() != string.Empty && row["EndTime4"].ToString().Trim() != string.Empty)
                {
                    var i = 1;
                    var dt1 = Convert.ToDateTime(row["ScheduleDate"]).Date + TimeSpan.Parse(row["StartTime4"].ToString().Trim());
                    var dt2 = Convert.ToDateTime(row["ScheduleDate"]).Date + TimeSpan.Parse(row["EndTime4"].ToString().Trim());
                    while (dt1 < dt2)
                    {
                        if (quota == 0 || i <= quota)
                        {
                            DataRow drSlot = dtbSlot.NewRow();
                            // Dirubah menggunakan nama columns karena error akibat ada penyisipan columns baru dan supaya kedepannya aman (Handono 230628)
                            //dr[0] = row["ServiceUnitID"].ToString();
                            //dr[1] = row["ParamedicID"].ToString();
                            //dr[2] = dt1.Date;
                            //dr[3] = dt1.ToString("HH:mm");
                            //dr[4] = i;
                            drSlot["ServiceUnitID"] = row["ServiceUnitID"].ToString();
                            drSlot["ParamedicID"] = row["ParamedicID"].ToString();
                            drSlot["AppointmentDate"] = dt1.Date;
                            drSlot["AppointmentTime"] = dt1.ToString("HH:mm");
                            drSlot["AppointmentQue"] = i;
                            dtbSlot.Rows.Add(drSlot);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                        else
                            break;
                    }
                }
                //time 5
                if (row["StartTime5"].ToString().Trim() != string.Empty && row["EndTime5"].ToString().Trim() != string.Empty)
                {
                    var i = 1;
                    var dt1 = Convert.ToDateTime(row["ScheduleDate"]).Date + TimeSpan.Parse(row["StartTime5"].ToString().Trim());
                    var dt2 = Convert.ToDateTime(row["ScheduleDate"]).Date + TimeSpan.Parse(row["EndTime5"].ToString().Trim());
                    while (dt1 < dt2)
                    {
                        if (quota == 0 || i <= quota)
                        {
                            DataRow drSlot = dtbSlot.NewRow();
                            // Dirubah menggunakan nama columns karena error akibat ada penyisipan columns baru dan supaya kedepannya aman (Handono 230628)
                            //dr[0] = row["ServiceUnitID"].ToString();
                            //dr[1] = row["ParamedicID"].ToString();
                            //dr[2] = dt1.Date;
                            //dr[3] = dt1.ToString("HH:mm");
                            //dr[4] = i;

                            drSlot["ServiceUnitID"] = row["ServiceUnitID"].ToString();
                            drSlot["ParamedicID"] = row["ParamedicID"].ToString();
                            drSlot["AppointmentDate"] = dt1.Date;
                            drSlot["AppointmentTime"] = dt1.ToString("HH:mm");
                            drSlot["AppointmentQue"] = i;
                            dtbSlot.Rows.Add(drSlot);

                            dt1 = dt1.AddMinutes(duration);
                            i++;
                        }
                        else
                            break;
                    }
                }
            }

            var appt = AppointmentList(ServiceUnitID, ParamedicID, dateStart, dateFinish);

            foreach (var entity in appt)
            {
                var slots = dtbSlot.AsEnumerable().Where(row =>
                    row.Field<string>("ServiceUnitID") == entity.ServiceUnitID &&
                    row.Field<string>("ParamedicID") == entity.ParamedicID &&
                    row.Field<DateTime>("AppointmentDate") == entity.AppointmentDate &&
                    row.Field<string>("AppointmentTime") == entity.AppointmentTime);
                switch (slots.Count())
                {
                    case 0:
                        {
                            //throw new Exception(ErrDataApptSlotNotFound
                            //    .Replace(GetErrorMessage(ErrDataApptSlotNotFound),
                            //        string.Format("Slot time not found for appointment {0}", entity.AppointmentNo)));
                            break;
                        }
                    case 1:
                        {

                            var slot = slots.First();
                            // Dirubah menggunakan nama columns karena error akibat ada penyisipan columns baru dan supaya kedepannya aman (Handono 230628)
                            //slot[5] = entity.AppointmentNo;
                            //slot[6] = entity.PatientID ?? string.Empty;
                            //slot[7] = entity.FirstName;
                            //slot[8] = entity.MiddleName;
                            //slot[9] = entity.LastName;
                            //if (entity.DateOfBirth.HasValue)
                            //{
                            //    slot[10] = entity.DateOfBirth.Value;
                            //}
                            //slot[11] = entity.StreetName;
                            //slot[12] = entity.District;
                            //slot[13] = entity.City;
                            //slot[14] = entity.County;
                            //slot[15] = entity.State;
                            //slot[16] = entity.ZipCode ?? string.Empty;
                            //slot[17] = entity.PhoneNo;
                            //slot[18] = entity.Email;
                            //slot[19] = entity.GuarantorID;
                            //slot[20] = entity.Notes;
                            //slot[21] = entity.SRAppointmentStatus;
                            //slot[22] = entity.MedicalNo;
                            //slot[23] = entity.BirthPlace;
                            //slot[24] = entity.Sex;
                            //slot[25] = entity.Ssn;

                            slot["AppointmentNo"] = entity.AppointmentNo;
                            slot["PatientID"] = entity.PatientID ?? string.Empty;
                            slot["FirstName"] = entity.FirstName;
                            slot["MiddleName"] = entity.MiddleName;
                            slot["LastName"] = entity.LastName;
                            if (entity.DateOfBirth.HasValue)
                            {
                                slot["DateOfBirth"] = entity.DateOfBirth.Value;
                            }
                            slot["StreetName"] = entity.StreetName;
                            slot["District"] = entity.District;
                            slot["City"] = entity.City;
                            slot["County"] = entity.County;
                            slot["State"] = entity.State;
                            slot["ZipCode"] = entity.ZipCode ?? string.Empty;
                            slot["PhoneNo"] = entity.PhoneNo;
                            slot["Email"] = entity.Email;
                            slot["GuarantorID"] = entity.GuarantorID;
                            slot["Notes"] = entity.Notes;
                            slot["AppointmentStatus"] = entity.SRAppointmentStatus;
                            slot["MedicalNo"] = entity.MedicalNo;
                            slot["BirthPlace"] = entity.BirthPlace;
                            slot["Sex"] = entity.Sex;
                            slot["Ssn"] = entity.Ssn;

                            if (Version != "1_0")
                            {

                                slot["MobilePhoneNo"] = entity.MobilePhoneNo;

                                DateTime currentDate = DateTime.Now;  // Mendapatkan tanggal dan waktu saat ini
                                int PeriodYear = currentDate.Year;  // Mengambil tahun dari DateTime


                                var quota = new ParamedicSchedule();
                                quota.LoadByPrimaryKey(ServiceUnitID, ParamedicID, PeriodYear.ToString());
                                slot["Quota"] = (quota.Quota ?? 0) + (quota.QuotaOnline ?? 0) + (quota.QuotaBpjs ?? 0) + (quota.QuotaBpjsOnline ?? 0);


                                var regColl = new RegistrationCollection();

                                regColl.Query.Where(
                                    regColl.Query.ServiceUnitID == ServiceUnitID,
                                    regColl.Query.ParamedicID == ParamedicID,
                                    regColl.Query.RegistrationDate.Between(dateStart, dateFinish),
                                    regColl.Query.IsVoid == false,
                                    regColl.Query.AppointmentNo == ""
                                    );

                                regColl.LoadAll();
                                int regcount = regColl.Count();


                                var aptColl = new AppointmentCollection();

                                aptColl.Query.Where(
                                    aptColl.Query.ServiceUnitID == ServiceUnitID,
                                    aptColl.Query.ParamedicID == ParamedicID,
                                    aptColl.Query.AppointmentDate.Between(dateStart, dateFinish),
                                    aptColl.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                                    );

                                aptColl.LoadAll();
                                int aptcount = aptColl.Count();

                                slot["JmlhBO"] = aptcount;
                                slot["JmlhReg"] = regcount;
                                slot["SisaQuota"] = (quota.Quota ?? 0) + (quota.QuotaOnline ?? 0) + (quota.QuotaBpjs ?? 0) + (quota.QuotaBpjsOnline ?? 0) - (aptcount + regcount);

                            }



                            slot["CreateByUserID"] = entity.LastCreateByUserID;

                            break;
                        }
                    default:
                        {
                            throw new Exception(ErrDataMultipleApptSlotFound
                                .Replace(GetErrorMessage(ErrDataMultipleApptSlotFound),
                                    string.Format("Multiple slot time for appointment {0}", entity.AppointmentNo)));
                            break;
                        }
                }
            }

            // Registration
            var regs = new RegistrationCollection();
            regs.Query.Where(
                regs.Query.ServiceUnitID == ServiceUnitID,
                regs.Query.ParamedicID == ParamedicID,
                regs.Query.RegistrationDate.Between(dateStart, dateFinish),
                regs.Query.IsVoid == false
                );
            if (regs.LoadAll())
            {
                var patColl = new PatientCollection();
                patColl.Query.Where(patColl.Query.PatientID.In(regs.Select(r => r.PatientID)));
                if (patColl.LoadAll())
                {
                    foreach (var entity in regs)
                    {
                        var pat = patColl.Where(pc => pc.PatientID == entity.PatientID).FirstOrDefault();
                        if (pat != null)
                        {
                            var slots = dtbSlot.AsEnumerable().Where(row =>
                                row.Field<string>("ServiceUnitID") == entity.ServiceUnitID &&
                                row.Field<string>("ParamedicID") == entity.ParamedicID &&
                                row.Field<DateTime>("AppointmentDate") == entity.RegistrationDate &&
                                row.Field<string>("AppointmentTime") == entity.RegistrationTime);
                            switch (slots.Count())
                            {
                                case 0:
                                    {
                                        //throw new Exception(ErrDataApptSlotNotFound
                                        //    .Replace(GetErrorMessage(ErrDataApptSlotNotFound),
                                        //        string.Format("Slot time not found for appointment {0}", entity.AppointmentNo)));
                                        break;
                                    }
                                case 1:
                                    {
                                        var slot = slots.First();
                                        if (string.IsNullOrEmpty(slot["AppointmentNo"].ToString()))
                                        {
                                            // Dirubah menggunakan nama columns karena error akibat ada penyisipan columns baru dan supaya kedepannya aman (Handono 230628)
                                            //slot[5] = entity.RegistrationNo;
                                            //slot[6] = entity.PatientID ?? string.Empty;
                                            //slot[7] = pat.PatientName;
                                            //slot[8] = pat.MiddleName;
                                            //slot[9] = pat.LastName;
                                            //if (pat.DateOfBirth.HasValue)
                                            //{
                                            //    slot[10] = pat.DateOfBirth.Value;
                                            //}
                                            //slot[11] = pat.StreetName;
                                            //slot[12] = pat.District;
                                            //slot[13] = pat.City;
                                            //slot[14] = pat.County;
                                            //slot[15] = pat.State;
                                            //slot[16] = pat.ZipCode ?? string.Empty;
                                            //slot[17] = pat.PhoneNo;
                                            //slot[18] = pat.Email;
                                            //slot[19] = pat.GuarantorID;
                                            //slot[20] = pat.Notes;
                                            //slot[21] = AppSession.Parameter.AppointmentStatusClosed; //string.Empty;// entity.SRAppointmentStatus;
                                            //slot[22] = pat.MedicalNo;
                                            //slot[23] = pat.CityOfBirth;
                                            //slot[24] = pat.Sex;
                                            //slot[25] = pat.Ssn;
                                            //if (Version != "1_0")
                                            //{
                                            //    slot[26] = pat.MobilePhoneNo;
                                            //}

                                            slot["AppointmentNo"] = entity.RegistrationNo;
                                            slot["PatientID"] = entity.PatientID ?? string.Empty;
                                            slot["FirstName"] = pat.FirstName;
                                            slot["MiddleName"] = pat.MiddleName;
                                            slot["LastName"] = pat.LastName;
                                            if (pat.DateOfBirth.HasValue)
                                            {
                                                slot["DateOfBirth"] = pat.DateOfBirth.Value;
                                            }
                                            slot["StreetName"] = pat.StreetName;
                                            slot["District"] = pat.District;
                                            slot["City"] = pat.City;
                                            slot["County"] = pat.County;
                                            slot["State"] = pat.State;
                                            slot["ZipCode"] = pat.ZipCode ?? string.Empty;
                                            slot["PhoneNo"] = pat.PhoneNo;
                                            slot["Email"] = pat.Email;
                                            slot["GuarantorID"] = pat.GuarantorID;
                                            slot["Notes"] = pat.Notes;
                                            slot["AppointmentStatus"] = AppSession.Parameter.AppointmentStatusClosed; //string.Empty;// entity.SRAppointmentStatus;
                                            slot["MedicalNo"] = pat.MedicalNo;
                                            slot["BirthPlace"] = pat.CityOfBirth;
                                            slot["Sex"] = pat.Sex;
                                            slot["Ssn"] = pat.Ssn;
                                            if (Version != "1_0")
                                            {
                                                slot["MobilePhoneNo"] = pat.MobilePhoneNo;
                                                DateTime currentDate = DateTime.Now;  // Mendapatkan tanggal dan waktu saat ini
                                                int PeriodYear = currentDate.Year;  // Mengambil tahun dari DateTime


                                                var quota = new ParamedicSchedule();
                                                quota.LoadByPrimaryKey(ServiceUnitID, ParamedicID, PeriodYear.ToString());
                                                slot["Quota"] = (quota.Quota ?? 0) + (quota.QuotaOnline ?? 0) + (quota.QuotaBpjs ?? 0) + (quota.QuotaBpjsOnline ?? 0);


                                                var regColl = new RegistrationCollection();

                                                regColl.Query.Where(
                                                    regColl.Query.ServiceUnitID == ServiceUnitID,
                                                    regColl.Query.ParamedicID == ParamedicID,
                                                    regColl.Query.RegistrationDate.Between(dateStart, dateFinish),
                                                    regColl.Query.IsVoid == false,
                                                    regColl.Query.AppointmentNo == ""
                                                    );

                                                regColl.LoadAll();
                                                int regcount = regColl.Count();


                                                var aptColl = new AppointmentCollection();

                                                aptColl.Query.Where(
                                                    aptColl.Query.ServiceUnitID == ServiceUnitID,
                                                    aptColl.Query.ParamedicID == ParamedicID,
                                                    aptColl.Query.AppointmentDate.Between(dateStart, dateFinish),
                                                    aptColl.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                                                    );

                                                aptColl.LoadAll();
                                                int aptcount = aptColl.Count();

                                                slot["JmlhBO"] = aptcount;
                                                slot["JmlhReg"] = regcount;
                                                slot["SisaQuota"] = (quota.Quota ?? 0) + (quota.QuotaOnline ?? 0) + (quota.QuotaBpjs ?? 0) + (quota.QuotaBpjsOnline ?? 0) - (aptcount + regcount);
                                            }
                                            slot["CreateByUserID"] = entity.LastCreateUserID;
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        throw new Exception(ErrDataMultipleApptSlotFound
                                            .Replace(GetErrorMessage(ErrDataMultipleApptSlotFound),
                                                string.Format("Multiple slot time for appointment {0}", entity.AppointmentNo)));
                                        break;
                                    }
                            }
                        }
                    }
                }
            }

            // remove appointment yang masih kosong dan dokternya cuti
            if (dtbSlot.Rows.Count > 0)
            {
                var pidList = dtbSlot.AsEnumerable().Select(x => x["ParamedicID"].ToString()).Distinct().ToList();
                foreach (var pid in pidList)
                {
                    var dDate = dateStart;
                    do
                    {
                        var dLeaveColl = GetPhycisianOnLeave(pid, dDate);
                        if (dLeaveColl.Count > 0)
                        {
                            // remove dari list slot
                            var rowToRemove = dtbSlot.AsEnumerable().Where(c => c["ParamedicID"].ToString() == pid &&
                                (DateTime)c["AppointmentDate"] == dDate && string.IsNullOrEmpty(c["AppointmentNo"].ToString())).ToList();
                            foreach (var ctr in rowToRemove)
                            {
                                //coll.DetachEntity(ctr);
                                ctr.Delete();
                            }
                        }

                        dDate = dDate.AddDays(1);
                    } while (dDate <= dateFinish);
                }
            }

            dtbSlot.AcceptChanges();

            return dtbSlot;
        }

        private static BusinessObject.AppointmentCollection AppointmentList(string serviceUnitID, string paramedicID,
            DateTime dateStart, DateTime dateFinish)
        {
            var query = new AppointmentQuery("a");
            var unit = new ServiceUnitQuery("b");
            var medic = new ParamedicQuery("c");
            var patient = new PatientQuery("e");

            query.Select(
                query.AppointmentNo,
                query.AppointmentQue,
                query.AppointmentDate,
                query.AppointmentTime,
                query.ServiceUnitID,
                query.ParamedicID,
                query.PatientID,
                query.FirstName, query.MiddleName, query.LastName,
                query.DateOfBirth,
                query.StreetName, query.District, query.City, query.County, query.State,
                query.ZipCode, query.PhoneNo, query.Email,
                query.GuarantorID,
                query.Notes,
                query.SRAppointmentStatus,
                patient.MedicalNo.As("refToPatient_MedicalNo"),
                query.BirthPlace,
                query.Sex,
                query.Ssn,
                query.MobilePhoneNo,
                query.LastCreateByUserID
                );
            query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
            query.InnerJoin(medic).On(query.ParamedicID == medic.ParamedicID);
            query.LeftJoin(patient).On(query.PatientID == patient.PatientID);

            query.Where(
                query.AppointmentDate.Between(dateStart, dateFinish));
            if (!string.IsNullOrEmpty(serviceUnitID))
            {
                query.Where(query.ServiceUnitID == serviceUnitID);
            }
            if (!string.IsNullOrEmpty(paramedicID))
            {
                query.Where(query.ParamedicID == paramedicID);
            }

            query.Where(query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);

            var coll = new BusinessObject.AppointmentCollection();
            coll.Load(query);

            return coll;
        }

        private static string GetOneParamedicBPJS(string ServiceUnitID, DateTime AppointmentDate)
        {
            // cari dokter di unit terkait yang terima bpjs
            var puc = new ServiceUnitParamedicCollection();
            puc.Query.Where(puc.Query.ServiceUnitID == ServiceUnitID, puc.Query.IsAcceptBPJS == true);
            if (!puc.LoadAll())
            {
                throw new Exception(ErrDataNotFound.Replace(GetErrorMessage(ErrDataNotFound), "Paramedic not found"));
            }
            // ambil yang tidak cuti di tanggal appointment
            List<string> parLeave = new List<string>();
            foreach (var pu in puc)
            {
                parLeave.Add(ValidatePhycisianOnLeave(pu.ParamedicID, AppointmentDate, "id"));
            }

            var parAvailable = puc.Where(a => !parLeave.Contains(a.ParamedicID)).ToList();

            var pSlots = new Dictionary<string, decimal>();

            foreach (var para in parAvailable)
            {
                // cari slot proporsional
                var dtbSlots = AppointmentSlotTime("teing ah", ServiceUnitID, para.ParamedicID, AppointmentDate, AppointmentDate);
                if (dtbSlots.Columns.Contains("CreateByUserID"))
                    dtbSlots.Columns.Remove("CreateByUserID");
                if (dtbSlots.Rows.Count > 0)
                {
                    pSlots.Add(para.ParamedicID,
                       dtbSlots.AsEnumerable()
                       .Where(r => !string.IsNullOrEmpty(r.Field<string>("FirstName"))).Count() / dtbSlots.Rows.Count);
                }
            }

            // ambil yang paling kecil, artinya paling sedikit pasiennya
            var selectedPar = new KeyValuePair<string, decimal>();
            foreach (var p in pSlots)
            {
                if (string.IsNullOrEmpty(selectedPar.Key))
                {
                    selectedPar = p;
                }
                else if (selectedPar.Value > p.Value)
                {
                    selectedPar = p;
                }
            }

            return selectedPar.Key;
        }

        public static DataRow AppointmentSetEntityValue(string Version, string ServiceUnitID, string ParamedicID,
            string AppointmentDate, string AppointmentTime, string AppointmentNo,
            string PatientID, string FirstName, string MiddleName, string LastName,
            string DateOfBirth, string BirthPlace, string Sex,
            string StreetName, string District, string City, string County, string State, string ZipCode,
            string PhoneNo, string Email, string Ssn,
            string GuarantorID, string Notes, string AppointmentStatus, string MobilePhoneNo,
            string Nomorkartu, string NomorReferensi, int JenisReferensi, string UserID, string AppointmentType, string fromRegistrationNo = null, string fromRegistrationNoMds = null)
        {
            ServiceUnitID = ServiceUnitID.Trim();
            ParamedicID = ParamedicID.Trim();
            AppointmentNo = AppointmentNo.Trim();
            PatientID = PatientID.Trim();

            var dAppointmentDate = string.IsNullOrEmpty(Version) ? Convert.ToDateTime(AppointmentDate) : DateTime.ParseExact(AppointmentDate, "yyyy-MM-dd", null);

            if (ParamedicID == "AUTOBPJS")
            {
                ParamedicID = GetOneParamedicBPJS(ServiceUnitID, dAppointmentDate);
            }
            if (string.IsNullOrEmpty(ParamedicID))
            {
                throw new Exception(ErrDataNotFound.Replace(GetErrorMessage(ErrDataNotFound), "Paramedic schedule not yet available"));
            }

            var psdColl = new ParamedicScheduleDateCollection();
            psdColl.Query.Where(
                psdColl.Query.ServiceUnitID == ServiceUnitID,
                psdColl.Query.ParamedicID == ParamedicID,
                psdColl.Query.ScheduleDate == dAppointmentDate);
            if (psdColl.LoadAll())
            {
                if (psdColl.Count > 1)
                    throw new Exception(ErrDataMultipleScheFound.Replace(GetErrorMessage(ErrDataMultipleScheFound),
                        string.Format("Multiple schedule for service unit {0}, paramedic {1}, date {2}",
                        ServiceUnitID, ParamedicID, AppointmentDate)));
                var psd = psdColl.First();

                var ps = new ParamedicSchedule();
                ps.Query.Where(
                    ps.Query.ServiceUnitID == psd.ServiceUnitID,
                    ps.Query.ParamedicID == psd.ParamedicID,
                    ps.Query.PeriodYear == psd.PeriodYear
                );
                if (!ps.Load(ps.Query))
                    throw new Exception(ErrDataScheNotFound.Replace(GetErrorMessage(ErrDataScheNotFound),
                        string.Format("Related schedule not found for service unit {0}, paramedic {1}, date {2}",
                    ServiceUnitID, ParamedicID, AppointmentDate)));

                // validasi dokter cuti
                //string valMsg = RegistrationWS.ValidatePhycisianOnLeave(ParamedicID, (new DateTime()).NowAtSqlServer().Date, "en");
                //if (!string.IsNullOrEmpty(valMsg))
                //{
                //    throw new Exception(ErrDataApptSlotNotFound.Replace(GetErrorMessage(ErrDataApptSlotNotFound),
                //            string.Format("Selected paramedic is on leave, paramedic {0}, date {1}",
                //        ParamedicID, AppointmentDate)));
                //}

                string valMsg = RegistrationWS.ValidatePhycisianOnLeave(ParamedicID, dAppointmentDate, "en");
                if (!string.IsNullOrEmpty(valMsg))
                {
                    throw new Exception(ErrDataApptSlotNotFound.Replace(GetErrorMessage(ErrDataApptSlotNotFound),
                            string.Format("Selected paramedic is on leave, paramedic {0}, date {1}",
                        ParamedicID, AppointmentDate)));
                }

                BusinessObject.Appointment apt = new BusinessObject.Appointment();
                var isNew = string.IsNullOrEmpty(AppointmentNo);

                // validasi appointment lebih dari 1 di hari yg sama
                var appts = new AppointmentCollection();
                appts.Query.Where(appts.Query.ServiceUnitID == ServiceUnitID,
                    appts.Query.ParamedicID == ParamedicID,
                    appts.Query.PatientID == PatientID,
                    appts.Query.AppointmentDate == dAppointmentDate,
                    appts.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);

                if (!isNew)
                    appts.Query.Where(appts.Query.SRAppoinmentType != AppSession.Parameter.AppointmentTypeWebService);

                appts.LoadAll();
                if (appts.Count > 0)
                {
                    throw new Exception(ErrDataMultipleFound.Replace(GetErrorMessage(ErrDataMultipleFound),
                           string.Format("The patient has been registered for the intended date and physician, date {1}, physician {0}",
                       ParamedicID, AppointmentDate)));
                }

                if (!isNew)
                {
                    if (!apt.LoadByPrimaryKey(AppointmentNo))
                    {
                        throw new Exception(ErrDataNotFound.Replace(GetErrorMessage(ErrDataNotFound),
                            string.Format("Appointment number {0} not found", AppointmentNo)));
                    }
                }

                apt.ServiceUnitID = ServiceUnitID;
                apt.ParamedicID = ParamedicID;
                apt.str.PatientID = PatientID;
                apt.AppointmentDate = dAppointmentDate.Date;

                apt.VisitTypeID = "VT001";
                apt.VisitDuration = Convert.ToByte(ps.ExamDuration ?? 0);
                apt.SRAppointmentStatus = AppointmentStatus; // "AppoinmentStatus-001";
                apt.SRAppoinmentType = AppointmentType;
                apt.FirstName = FirstName;
                apt.MiddleName = MiddleName;
                apt.LastName = LastName;

                DateTime Temp;
                if (DateTime.TryParse(DateOfBirth, out Temp) == true)
                    apt.DateOfBirth = string.IsNullOrEmpty(Version) ? Convert.ToDateTime(DateOfBirth) : DateTime.ParseExact(DateOfBirth, "yyyy-MM-dd", null);

                apt.GuarantorID = GuarantorID;
                apt.Notes = Notes;

                apt.StreetName = StreetName;
                apt.District = District;
                apt.City = City;
                apt.County = County;
                apt.State = State;
                apt.str.ZipCode = ZipCode;
                apt.PhoneNo = PhoneNo;
                apt.Email = Email;
                apt.FaxNo = string.Empty;
                apt.BirthPlace = BirthPlace;
                apt.Sex = Sex;
                apt.Ssn = Ssn;
                apt.MobilePhoneNo = MobilePhoneNo;

                if (!string.IsNullOrEmpty(PatientID))
                {
                    var pt = new Patient();
                    if (pt.LoadByPrimaryKey(PatientID))
                    {
                        pt.MobilePhoneNo = MobilePhoneNo;
                        pt.PhoneNo = PhoneNo;
                        pt.Email = Email;
                        pt.Save();
                    }
                }

                // bpjs
                apt.GuarantorCardNo = Nomorkartu;
                apt.ReferenceNumber = NomorReferensi;
                apt.ReferenceType = JenisReferensi;

                var dtbSlots = AppointmentSlotTime(Version, ServiceUnitID, ParamedicID, dAppointmentDate, dAppointmentDate);


                DataRow slot;
                var AutoTime = AppointmentTime.Split('_');
                if (AutoTime[0] == "AUTO")
                {
                    if (dtbSlots.AsEnumerable().Where(row => string.IsNullOrEmpty(row.Field<string>("AppointmentNo"))).Any())
                    {
                        if (AutoTime.Length == 1)
                        {
                            slot = dtbSlots.AsEnumerable().Where(row => string.IsNullOrEmpty(row.Field<string>("AppointmentNo"))).First();
                        }
                        else if (AutoTime.Length == 2 && Helper.IsNumeric(AutoTime[1]))
                        {
                            if (dtbSlots.AsEnumerable().Where(row => string.IsNullOrEmpty(row.Field<string>("AppointmentNo")) &&
                                System.Convert.ToInt32(row.Field<string>("AppointmentQue")) >= System.Convert.ToInt32(AutoTime[1])).Any())
                            {
                                slot = dtbSlots.AsEnumerable().Where(row => string.IsNullOrEmpty(row.Field<string>("AppointmentNo")) &&
                                        System.Convert.ToInt32(row.Field<string>("AppointmentQue")) >= System.Convert.ToInt32(AutoTime[1])).First();
                            }
                            else
                            {
                                throw new Exception(ErrDataApptSlotNotFound.Replace(GetErrorMessage(ErrDataApptSlotNotFound),
                                   string.Format("Appointment slot is full")));
                            }
                        }
                        else if (AutoTime.Length == 3 && Helper.IsNumeric(AutoTime[1]) && Helper.IsNumeric(AutoTime[2]))
                        {
                            if (dtbSlots.AsEnumerable().Where(row => string.IsNullOrEmpty(row.Field<string>("AppointmentNo")) &&
                                System.Convert.ToInt32(row.Field<string>("AppointmentQue")) >= System.Convert.ToInt32(AutoTime[1])).Any())
                            {
                                if (dtbSlots.AsEnumerable().Where(row => !string.IsNullOrEmpty(row.Field<string>("AppointmentNo")) &&
                                    System.Convert.ToInt32(row.Field<string>("AppointmentQue")) >= System.Convert.ToInt32(AutoTime[1]) &&
                                    row.Field<string>("CreateByUserID") == UserID).Count() >= System.Convert.ToInt32(AutoTime[2]))
                                {
                                    throw new Exception(ErrDataApptSlotNotFound.Replace(GetErrorMessage(ErrDataApptSlotNotFound),
                                       string.Format("Appointment slot is full")));
                                }
                                slot = dtbSlots.AsEnumerable().Where(row => string.IsNullOrEmpty(row.Field<string>("AppointmentNo")) &&
                                        System.Convert.ToInt32(row.Field<string>("AppointmentQue")) >= System.Convert.ToInt32(AutoTime[1])).First();
                            }
                            else
                            {
                                throw new Exception(ErrDataApptSlotNotFound.Replace(GetErrorMessage(ErrDataApptSlotNotFound),
                                   string.Format("Appointment slot is full")));
                            }
                        }
                        else
                        {
                            throw new Exception(ErrUnspecified.Replace(GetErrorMessage(ErrUnspecified),
                                    "Invalid input parameter of AppointmentTime"));
                        }
                    }
                    else
                    {
                        if (dtbSlots.Rows.Count == 0)
                        {
                            throw new Exception(ErrDataApptSlotNotFound.Replace(GetErrorMessage(ErrDataApptSlotNotFound),
                                "Appointment slot has not been configured yet"));
                        }
                        else
                        {
                            throw new Exception(ErrDataApptSlotNotFound.Replace(GetErrorMessage(ErrDataApptSlotNotFound),
                                string.Format("Appointment slot is full")));
                        }
                    }
                }
                else
                {
                    var slots = dtbSlots.AsEnumerable().Where(row => row.Field<string>("AppointmentTime") == AppointmentTime);

                    switch (slots.Count())
                    {
                        case (0):
                            {
                                throw new Exception(ErrDataApptSlotNotFound.Replace(GetErrorMessage(ErrDataApptSlotNotFound),
                                    string.Format("Can not find appointment time slot {0}", AppointmentTime)));
                                break;
                            }
                        case (1):
                            {
                                slot = slots.First();
                                break;
                            }
                        default:
                            {
                                throw new Exception(ErrDataMultipleApptSlotFound.Replace(GetErrorMessage(ErrDataMultipleApptSlotFound),
                                               string.Format("Multiple appointment time slot {0}", AppointmentTime)));
                                break;
                            }
                    }
                }

                apt.AppointmentTime = slot["AppointmentTime"].ToString();

                if (isNew)
                {
                    // validation for new appointment
                    if (!string.IsNullOrEmpty(slot["AppointmentNo"].ToString()))
                    {
                        throw new Exception(ErrDataApptConflict.Replace(GetErrorMessage(ErrDataApptConflict),
                            string.Format("Slot {0} has been taken, create new appointment failed", AppointmentTime)));
                    }
                    // validate for registration
                    var regs = new RegistrationCollection();
                    regs.Query.Where(regs.Query.ServiceUnitID == ServiceUnitID,
                        regs.Query.ParamedicID == ParamedicID,
                        regs.Query.RegistrationDate == AppointmentDate,
                        regs.Query.RegistrationTime == AppointmentTime,
                        regs.Query.IsVoid == 0);
                    if (regs.LoadAll())
                    {
                        throw new Exception(ErrDataApptConflict.Replace(GetErrorMessage(ErrDataApptConflict),
                            string.Format("Slot {0} has been taken by another registration, create new appointment failed", AppointmentTime)));
                    }
                }
                else
                {
                    // validation for appointment update
                    if (slot["ServiceUnitID"].ToString() != ServiceUnitID ||
                        slot["ParamedicID"].ToString() != ParamedicID ||
                        System.Convert.ToDateTime(slot["AppointmentDate"]) != dAppointmentDate ||
                        slot["AppointmentTime"].ToString() != AppointmentTime)
                    {
                        // perubahan slot appointment
                        // pastikan slot tujuan belum terisi
                        if (!string.IsNullOrEmpty(slot["AppointmentNo"].ToString()))
                        {
                            throw new Exception(ErrDataApptConflict.Replace(GetErrorMessage(ErrDataApptConflict),
                                string.Format("Slot {0} has been taken, moving appoiment to new slot failed", AppointmentTime)));
                        }
                    }
                }

                apt.AppointmentQue = int.Parse(slot["AppointmentQue"].ToString());

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(apt.ServiceUnitID);

                var aptQue = new AppointmentQueueing();
                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (isNew)
                    {
                        AppAutoNumberLast _autoNumber;
                        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingNumberingSettingAppointmentNoWebService) == "Yes" && AppSession.Parameter.AppointmentTypeWebService == AppointmentType)
                        {
                            _autoNumber = Helper.GetNewAutoNumber(dAppointmentDate.Date, AppEnum.AutoNumber.AppointmentNoWebService, "", UserID);
                            apt.AppointmentNo = _autoNumber.LastCompleteNumber;
                        }
                        else
                        {
                            _autoNumber = Helper.GetNewAutoNumber(dAppointmentDate.Date, AppEnum.AutoNumber.AppointmentNo, "", UserID);
                            apt.AppointmentNo = _autoNumber.LastCompleteNumber;
                        }
                        _autoNumber.Save();

                        apt.LastCreateByUserID = UserID;
                        apt.LastCreateDateTime = (new DateTime()).NowAtSqlServer();

                        if (aptQue.SetQueForReg(apt, AppSession.Parameter.GuarantorAskesID[0].Contains(apt.GuarantorID) ? "02" : AppSession.Parameter.SelfGuarantor.Equals(apt.GuarantorID) ? "01" : "03", su, UserID, false))
                        {
                            aptQue.SRKioskQueueStatus = "04"; // skipped, pasien tidak ambil lagi nomor antrian tapi harusnya ada loket khusus pasien appointment atau lewat pendaftaran mandiri
                            aptQue.Save();
                        }
                    }
                    else
                    {
                        aptQue.Query.Where(aptQue.Query.AppointmentNo == apt.AppointmentNo);
                        aptQue.Query.OrderBy(aptQue.Query.Id.Ascending);
                        aptQue.Query.es.Top = 1;
                        if (!aptQue.Load(aptQue.Query))
                        {
                            // create the empty one
                            aptQue.AddNew();
                            aptQue.FormattedNo = "";
                        }
                    }

                    //Last Update Status
                    apt.LastUpdateByUserID = UserID;
                    apt.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    // Save RegistrationNo asal Appointment
                    // Untuk Merge Billing untuk keperluan Claim ke guarantor (Handono 231110 req by Imel)
                    if (!string.IsNullOrWhiteSpace(fromRegistrationNo))
                        apt.FromRegistrationNo = fromRegistrationNo;

                    if (!string.IsNullOrWhiteSpace(fromRegistrationNoMds))
                        apt.FromRegistrationNoMds = fromRegistrationNoMds;

                    apt.Save();

                    // requery untuk validasi, ada kasus request yang hampir bersamaan dapat slot yang sama
                    var apptColl = new AppointmentCollection();
                    apptColl.Query.Where(
                        apptColl.Query.AppointmentDate == apt.AppointmentDate,
                        apptColl.Query.ServiceUnitID == apt.ServiceUnitID,
                        apptColl.Query.ParamedicID == apt.ParamedicID,
                        apptColl.Query.AppointmentTime == apt.AppointmentTime,
                        apptColl.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                        );
                    if (apptColl.LoadAll() && apptColl.Count > 1)
                    {
                        throw new Exception(ErrDataApptConflict.Replace(GetErrorMessage(ErrDataApptConflict),
                            string.Format("Slot {0} has been taken, create new appointment failed", AppointmentTime)));
                    }

                    if (AppSession.Parameter.HealthcareInitial == "RSTJ")
                    {
                        var taskId = string.IsNullOrWhiteSpace(apt.PatientID) ? "2" : "1";

                        var task = new BusinessObject.Interop.TARAKAN.AppointmentOnlineTask();
                        if (!task.LoadByPrimaryKey(apt.AppointmentNo, taskId))
                        {
                            task.AppointmentNo = apt.AppointmentNo;
                            task.TaskId = taskId;
                            task.Timestamp = Temiang.Avicenna.Common.BPJS.Helper.GetUnixTimeStamp();
                            task.Save();
                        }
                    }

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                if (dtbSlots.Columns.Contains("CreateByUserID"))
                    dtbSlots.Columns.Remove("CreateByUserID");

                //slot[0] = apt.ServiceUnitID;
                //slot[1] = apt.ParamedicID;
                //slot[2] = apt.AppointmentDate;
                //slot[3] = apt.AppointmentTime;
                //slot[4] = apt.AppointmentQue;
                //slot[5] = apt.AppointmentNo;
                //slot[6] = apt.PatientID ?? string.Empty;
                //slot[7] = apt.FirstName;
                //slot[8] = apt.MiddleName;
                //slot[9] = apt.LastName;
                //if (apt.DateOfBirth.HasValue)
                //{
                //    slot[10] = apt.DateOfBirth.Value;
                //}
                //slot[11] = apt.StreetName;
                //slot[12] = apt.District;
                //slot[13] = apt.City;
                //slot[14] = apt.County;
                //slot[15] = apt.State;
                //slot[16] = apt.ZipCode ?? string.Empty;
                //slot[17] = apt.PhoneNo;
                //slot[18] = apt.Email;
                //slot[19] = apt.GuarantorID;
                //slot[20] = apt.Notes;
                //slot[21] = apt.SRAppointmentStatus;
                //slot[22] = "";
                //slot[23] = apt.BirthPlace;
                //slot[24] = apt.Sex;
                //slot[25] = apt.Ssn;
                //if (Version != "1_0")
                //{
                //    slot[26] = apt.MobilePhoneNo;
                //}
                //if (!string.IsNullOrEmpty(apt.PatientID))
                //{
                //    var pat = new Patient();
                //    if (pat.LoadByPrimaryKey(apt.PatientID))
                //    {
                //        slot[22] = pat.MedicalNo;
                //    }
                //}

                slot["ServiceUnitID"] = apt.ServiceUnitID;
                slot["ParamedicID"] = apt.ParamedicID;
                slot["AppointmentDate"] = apt.AppointmentDate;
                slot["AppointmentTime"] = apt.AppointmentTime;
                slot["AppointmentQue"] = apt.AppointmentQue;
                slot["AppointmentNo"] = apt.AppointmentNo;
                slot["PatientID"] = apt.PatientID ?? string.Empty;
                slot["FirstName"] = apt.FirstName;
                slot["MiddleName"] = apt.MiddleName;
                slot["LastName"] = apt.LastName;
                if (apt.DateOfBirth.HasValue)
                {
                    slot["DateOfBirth"] = apt.DateOfBirth.Value;
                }
                slot["StreetName"] = apt.StreetName;
                slot["District"] = apt.District;
                slot["City"] = apt.City;
                slot["County"] = apt.County;
                slot["State"] = apt.State;
                slot["ZipCode"] = apt.ZipCode ?? string.Empty;
                slot["PhoneNo"] = apt.PhoneNo;
                slot["Email"] = apt.Email;
                slot["GuarantorID"] = apt.GuarantorID;
                slot["Notes"] = apt.Notes;
                slot["AppointmentStatus"] = apt.SRAppointmentStatus;
                slot["MedicalNo"] = "";
                slot["BirthPlace"] = apt.BirthPlace;
                slot["Sex"] = apt.Sex;
                slot["Ssn"] = apt.Ssn;
                if (Version != "1_0")
                {
                    slot["MobilePhoneNo"] = apt.MobilePhoneNo;
                }
                if (!string.IsNullOrEmpty(apt.PatientID))
                {
                    var pat = new Patient();
                    if (pat.LoadByPrimaryKey(apt.PatientID))
                    {
                        slot["MedicalNo"] = pat.MedicalNo;
                    }
                }

                slot["AppointmentQueFormattedNo"] = aptQue.FormattedNo;
                return slot;
            }
            else
            {
                throw new Exception(ErrDataScheNotFound.Replace(GetErrorMessage(ErrDataScheNotFound),
                    string.Format("Related schedule not found for service unit {0}, paramedic {1}, date {2}",
                        ServiceUnitID, ParamedicID, AppointmentDate)));
            }
        }

        public static DataRow AppointmentPostRanapSetEntityValue(string Version, string ServiceUnitID, string ParamedicID,
            string AppointmentDate, string AppointmentTime, string AppointmentNo,
            string PatientID, string FirstName, string MiddleName, string LastName,
            string DateOfBirth, string BirthPlace, string Sex,
            string StreetName, string District, string City, string County, string State, string ZipCode,
            string PhoneNo, string Email, string Ssn,
            string GuarantorID, string Notes, string AppointmentStatus, string MobilePhoneNo,
            string Nomorkartu, string NomorReferensi, int JenisReferensi, string UserID, string AppointmentType, string fromRegistrationNo = null, string fromRegistrationNoMds = null)
        {
            ServiceUnitID = ServiceUnitID.Trim();
            ParamedicID = ParamedicID.Trim();
            AppointmentNo = AppointmentNo.Trim();
            PatientID = PatientID.Trim();

            var dAppointmentDate = string.IsNullOrEmpty(Version) ? Convert.ToDateTime(AppointmentDate) : DateTime.ParseExact(AppointmentDate, "yyyy-MM-dd", null);

            if (ParamedicID == "AUTOBPJS")
            {
                ParamedicID = GetOneParamedicBPJS(ServiceUnitID, dAppointmentDate);
            }
            if (string.IsNullOrEmpty(ParamedicID))
            {
                throw new Exception(ErrDataNotFound.Replace(GetErrorMessage(ErrDataNotFound), "Paramedic schedule not yet available"));
            }

            var psdColl = new ParamedicScheduleDateCollection();
            psdColl.Query.Where(
                psdColl.Query.ServiceUnitID == ServiceUnitID,
                psdColl.Query.ParamedicID == ParamedicID,
                psdColl.Query.ScheduleDate == dAppointmentDate);
            if (psdColl.LoadAll())
            {
                if (psdColl.Count > 1)
                    throw new Exception(ErrDataMultipleScheFound.Replace(GetErrorMessage(ErrDataMultipleScheFound),
                        string.Format("Multiple schedule for service unit {0}, paramedic {1}, date {2}",
                        ServiceUnitID, ParamedicID, AppointmentDate)));
                var psd = psdColl.First();

                var ps = new ParamedicSchedule();
                ps.Query.Where(
                    ps.Query.ServiceUnitID == psd.ServiceUnitID,
                    ps.Query.ParamedicID == psd.ParamedicID,
                    ps.Query.PeriodYear == psd.PeriodYear
                );
                if (!ps.Load(ps.Query))
                    throw new Exception(ErrDataScheNotFound.Replace(GetErrorMessage(ErrDataScheNotFound),
                        string.Format("Related schedule not found for service unit {0}, paramedic {1}, date {2}",
                    ServiceUnitID, ParamedicID, AppointmentDate)));

                // validasi dokter cuti
                //string valMsg = RegistrationWS.ValidatePhycisianOnLeave(ParamedicID, (new DateTime()).NowAtSqlServer().Date, "en");
                //if (!string.IsNullOrEmpty(valMsg))
                //{
                //    throw new Exception(ErrDataApptSlotNotFound.Replace(GetErrorMessage(ErrDataApptSlotNotFound),
                //            string.Format("Selected paramedic is on leave, paramedic {0}, date {1}",
                //        ParamedicID, AppointmentDate)));
                //}

                string valMsg = RegistrationWS.ValidatePhycisianOnLeave(ParamedicID, dAppointmentDate, "en");
                if (!string.IsNullOrEmpty(valMsg))
                {
                    throw new Exception(ErrDataApptSlotNotFound.Replace(GetErrorMessage(ErrDataApptSlotNotFound),
                            string.Format("Selected paramedic is on leave, paramedic {0}, date {1}",
                        ParamedicID, AppointmentDate)));
                }

                BusinessObject.Appointment apt = new BusinessObject.Appointment();
                var isNew = string.IsNullOrEmpty(AppointmentNo);

                if (!isNew)
                {
                    if (!apt.LoadByPrimaryKey(AppointmentNo))
                    {
                        throw new Exception(ErrDataNotFound.Replace(GetErrorMessage(ErrDataNotFound),
                            string.Format("Appointment number {0} not found", AppointmentNo)));
                    }
                }

                apt.ServiceUnitID = ServiceUnitID;
                apt.ParamedicID = ParamedicID;
                apt.str.PatientID = PatientID;
                apt.AppointmentDate = dAppointmentDate.Date;

                apt.VisitTypeID = "VT001";
                apt.VisitDuration = Convert.ToByte(ps.ExamDuration ?? 0);
                apt.SRAppointmentStatus = AppointmentStatus; // "AppoinmentStatus-001";
                apt.SRAppoinmentType = AppointmentType;
                apt.FirstName = FirstName;
                apt.MiddleName = MiddleName;
                apt.LastName = LastName;

                DateTime Temp;
                if (DateTime.TryParse(DateOfBirth, out Temp) == true)
                    apt.DateOfBirth = string.IsNullOrEmpty(Version) ? Convert.ToDateTime(DateOfBirth) : DateTime.ParseExact(DateOfBirth, "yyyy-MM-dd", null);

                apt.GuarantorID = GuarantorID;
                apt.Notes = Notes;

                apt.StreetName = StreetName;
                apt.District = District;
                apt.City = City;
                apt.County = County;
                apt.State = State;
                apt.str.ZipCode = ZipCode;
                apt.PhoneNo = PhoneNo;
                apt.Email = Email;
                apt.FaxNo = string.Empty;
                apt.BirthPlace = BirthPlace;
                apt.Sex = Sex;
                apt.Ssn = Ssn;
                apt.MobilePhoneNo = MobilePhoneNo;

                if (!string.IsNullOrEmpty(PatientID))
                {
                    var pt = new Patient();
                    if (pt.LoadByPrimaryKey(PatientID))
                    {
                        pt.MobilePhoneNo = MobilePhoneNo;
                        pt.PhoneNo = PhoneNo;
                        pt.Email = Email;
                        pt.Save();
                    }
                }

                // bpjs
                apt.GuarantorCardNo = Nomorkartu;
                apt.ReferenceNumber = NomorReferensi;
                apt.ReferenceType = JenisReferensi;

                var dtbSlots = AppointmentSlotTime(Version, ServiceUnitID, ParamedicID, dAppointmentDate, dAppointmentDate);


                DataRow slot;
                var AutoTime = AppointmentTime.Split('_');
                if (AutoTime[0] == "AUTO")
                {
                    if (dtbSlots.AsEnumerable().Where(row => string.IsNullOrEmpty(row.Field<string>("AppointmentNo"))).Any())
                    {
                        if (AutoTime.Length == 1)
                        {
                            slot = dtbSlots.AsEnumerable().Where(row => string.IsNullOrEmpty(row.Field<string>("AppointmentNo"))).First();
                        }
                        else if (AutoTime.Length == 2 && Helper.IsNumeric(AutoTime[1]))
                        {
                            if (dtbSlots.AsEnumerable().Where(row => string.IsNullOrEmpty(row.Field<string>("AppointmentNo")) &&
                                System.Convert.ToInt32(row.Field<string>("AppointmentQue")) >= System.Convert.ToInt32(AutoTime[1])).Any())
                            {
                                slot = dtbSlots.AsEnumerable().Where(row => string.IsNullOrEmpty(row.Field<string>("AppointmentNo")) &&
                                        System.Convert.ToInt32(row.Field<string>("AppointmentQue")) >= System.Convert.ToInt32(AutoTime[1])).First();
                            }
                            else
                            {
                                throw new Exception(ErrDataApptSlotNotFound.Replace(GetErrorMessage(ErrDataApptSlotNotFound),
                                   string.Format("Appointment slot is full")));
                            }
                        }
                        else if (AutoTime.Length == 3 && Helper.IsNumeric(AutoTime[1]) && Helper.IsNumeric(AutoTime[2]))
                        {
                            if (dtbSlots.AsEnumerable().Where(row => string.IsNullOrEmpty(row.Field<string>("AppointmentNo")) &&
                                System.Convert.ToInt32(row.Field<string>("AppointmentQue")) >= System.Convert.ToInt32(AutoTime[1])).Any())
                            {
                                if (dtbSlots.AsEnumerable().Where(row => !string.IsNullOrEmpty(row.Field<string>("AppointmentNo")) &&
                                    System.Convert.ToInt32(row.Field<string>("AppointmentQue")) >= System.Convert.ToInt32(AutoTime[1]) &&
                                    row.Field<string>("CreateByUserID") == UserID).Count() >= System.Convert.ToInt32(AutoTime[2]))
                                {
                                    throw new Exception(ErrDataApptSlotNotFound.Replace(GetErrorMessage(ErrDataApptSlotNotFound),
                                       string.Format("Appointment slot is full")));
                                }
                                slot = dtbSlots.AsEnumerable().Where(row => string.IsNullOrEmpty(row.Field<string>("AppointmentNo")) &&
                                        System.Convert.ToInt32(row.Field<string>("AppointmentQue")) >= System.Convert.ToInt32(AutoTime[1])).First();
                            }
                            else
                            {
                                throw new Exception(ErrDataApptSlotNotFound.Replace(GetErrorMessage(ErrDataApptSlotNotFound),
                                   string.Format("Appointment slot is full")));
                            }
                        }
                        else
                        {
                            throw new Exception(ErrUnspecified.Replace(GetErrorMessage(ErrUnspecified),
                                    "Invalid input parameter of AppointmentTime"));
                        }
                    }
                    else
                    {
                        if (dtbSlots.Rows.Count == 0)
                        {
                            throw new Exception(ErrDataApptSlotNotFound.Replace(GetErrorMessage(ErrDataApptSlotNotFound),
                                "Appointment slot has not been configured yet"));
                        }
                        else
                        {
                            throw new Exception(ErrDataApptSlotNotFound.Replace(GetErrorMessage(ErrDataApptSlotNotFound),
                                string.Format("Appointment slot is full")));
                        }
                    }
                }
                else
                {
                    var slots = dtbSlots.AsEnumerable().Where(row => row.Field<string>("AppointmentTime") == AppointmentTime);

                    switch (slots.Count())
                    {
                        case (0):
                            {
                                throw new Exception(ErrDataApptSlotNotFound.Replace(GetErrorMessage(ErrDataApptSlotNotFound),
                                    string.Format("Can not find appointment time slot {0}", AppointmentTime)));
                                break;
                            }
                        case (1):
                            {
                                slot = slots.First();
                                break;
                            }
                        default:
                            {
                                throw new Exception(ErrDataMultipleApptSlotFound.Replace(GetErrorMessage(ErrDataMultipleApptSlotFound),
                                               string.Format("Multiple appointment time slot {0}", AppointmentTime)));
                                break;
                            }
                    }
                }

                apt.AppointmentTime = slot["AppointmentTime"].ToString();

                if (isNew)
                {
                    // validation for new appointment
                    if (!string.IsNullOrEmpty(slot["AppointmentNo"].ToString()))
                    {
                        throw new Exception(ErrDataApptConflict.Replace(GetErrorMessage(ErrDataApptConflict),
                            string.Format("Slot {0} has been taken, create new appointment failed", AppointmentTime)));
                    }
                    // validate for registration
                    var regs = new RegistrationCollection();
                    regs.Query.Where(regs.Query.ServiceUnitID == ServiceUnitID,
                        regs.Query.ParamedicID == ParamedicID,
                        regs.Query.RegistrationDate == AppointmentDate,
                        regs.Query.RegistrationTime == AppointmentTime,
                        regs.Query.IsVoid == 0);
                    if (regs.LoadAll())
                    {
                        throw new Exception(ErrDataApptConflict.Replace(GetErrorMessage(ErrDataApptConflict),
                            string.Format("Slot {0} has been taken by another registration, create new appointment failed", AppointmentTime)));
                    }
                }
                else
                {
                    // validation for appointment update
                    if (slot["ServiceUnitID"].ToString() != ServiceUnitID ||
                        slot["ParamedicID"].ToString() != ParamedicID ||
                        System.Convert.ToDateTime(slot["AppointmentDate"]) != dAppointmentDate ||
                        slot["AppointmentTime"].ToString() != AppointmentTime)
                    {
                        // perubahan slot appointment
                        // pastikan slot tujuan belum terisi
                        if (!string.IsNullOrEmpty(slot["AppointmentNo"].ToString()))
                        {
                            throw new Exception(ErrDataApptConflict.Replace(GetErrorMessage(ErrDataApptConflict),
                                string.Format("Slot {0} has been taken, moving appoiment to new slot failed", AppointmentTime)));
                        }
                    }
                }

                apt.AppointmentQue = int.Parse(slot["AppointmentQue"].ToString());

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(apt.ServiceUnitID);

                var aptQue = new AppointmentQueueing();
                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (isNew)
                    {
                        AppAutoNumberLast _autoNumber;
                        _autoNumber = Helper.GetNewAutoNumber(dAppointmentDate.Date, AppEnum.AutoNumber.AppointmentNoPostRanap, "", UserID);
                        apt.AppointmentNo = _autoNumber.LastCompleteNumber;
                        _autoNumber.Save();

                        apt.LastCreateByUserID = UserID;
                        apt.LastCreateDateTime = (new DateTime()).NowAtSqlServer();

                        if (aptQue.SetQueForReg(apt, AppSession.Parameter.GuarantorAskesID[0].Contains(apt.GuarantorID) ? "02" : AppSession.Parameter.SelfGuarantor.Equals(apt.GuarantorID) ? "01" : "03", su, UserID, false))
                        {
                            aptQue.SRKioskQueueStatus = "04"; // skipped, pasien tidak ambil lagi nomor antrian tapi harusnya ada loket khusus pasien appointment atau lewat pendaftaran mandiri
                            aptQue.Save();
                        }
                    }
                    else
                    {
                        aptQue.Query.Where(aptQue.Query.AppointmentNo == apt.AppointmentNo);
                        aptQue.Query.OrderBy(aptQue.Query.Id.Ascending);
                        aptQue.Query.es.Top = 1;
                        if (!aptQue.Load(aptQue.Query))
                        {
                            // create the empty one
                            aptQue.AddNew();
                            aptQue.FormattedNo = "";
                        }
                    }

                    //Last Update Status
                    apt.LastUpdateByUserID = UserID;
                    apt.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    // Save RegistrationNo asal Appointment
                    // Untuk Merge Billing untuk keperluan Claim ke guarantor (Handono 231110 req by Imel)
                    if (!string.IsNullOrWhiteSpace(fromRegistrationNo))
                        apt.FromRegistrationNo = fromRegistrationNo;

                    if (!string.IsNullOrWhiteSpace(fromRegistrationNoMds))
                        apt.FromRegistrationNoMds = fromRegistrationNoMds;

                    apt.Save();

                    // requery untuk validasi, ada kasus request yang hampir bersamaan dapat slot yang sama
                    var apptColl = new AppointmentCollection();
                    apptColl.Query.Where(
                        apptColl.Query.AppointmentDate == apt.AppointmentDate,
                        apptColl.Query.ServiceUnitID == apt.ServiceUnitID,
                        apptColl.Query.ParamedicID == apt.ParamedicID,
                        apptColl.Query.AppointmentTime == apt.AppointmentTime,
                        apptColl.Query.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel
                        );
                    if (apptColl.LoadAll() && apptColl.Count > 1)
                    {
                        throw new Exception(ErrDataApptConflict.Replace(GetErrorMessage(ErrDataApptConflict),
                            string.Format("Slot {0} has been taken, create new appointment failed", AppointmentTime)));
                    }

                    if (AppSession.Parameter.HealthcareInitial == "RSTJ")
                    {
                        var taskId = string.IsNullOrWhiteSpace(apt.PatientID) ? "2" : "1";

                        var task = new BusinessObject.Interop.TARAKAN.AppointmentOnlineTask();
                        if (!task.LoadByPrimaryKey(apt.AppointmentNo, taskId))
                        {
                            task.AppointmentNo = apt.AppointmentNo;
                            task.TaskId = taskId;
                            task.Timestamp = Temiang.Avicenna.Common.BPJS.Helper.GetUnixTimeStamp();
                            task.Save();
                        }
                    }

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                if (dtbSlots.Columns.Contains("CreateByUserID"))
                    dtbSlots.Columns.Remove("CreateByUserID");

                //slot[0] = apt.ServiceUnitID;
                //slot[1] = apt.ParamedicID;
                //slot[2] = apt.AppointmentDate;
                //slot[3] = apt.AppointmentTime;
                //slot[4] = apt.AppointmentQue;
                //slot[5] = apt.AppointmentNo;
                //slot[6] = apt.PatientID ?? string.Empty;
                //slot[7] = apt.FirstName;
                //slot[8] = apt.MiddleName;
                //slot[9] = apt.LastName;
                //if (apt.DateOfBirth.HasValue)
                //{
                //    slot[10] = apt.DateOfBirth.Value;
                //}
                //slot[11] = apt.StreetName;
                //slot[12] = apt.District;
                //slot[13] = apt.City;
                //slot[14] = apt.County;
                //slot[15] = apt.State;
                //slot[16] = apt.ZipCode ?? string.Empty;
                //slot[17] = apt.PhoneNo;
                //slot[18] = apt.Email;
                //slot[19] = apt.GuarantorID;
                //slot[20] = apt.Notes;
                //slot[21] = apt.SRAppointmentStatus;
                //slot[22] = "";
                //slot[23] = apt.BirthPlace;
                //slot[24] = apt.Sex;
                //slot[25] = apt.Ssn;
                //if (Version != "1_0")
                //{
                //    slot[26] = apt.MobilePhoneNo;
                //}
                //if (!string.IsNullOrEmpty(apt.PatientID))
                //{
                //    var pat = new Patient();
                //    if (pat.LoadByPrimaryKey(apt.PatientID))
                //    {
                //        slot[22] = pat.MedicalNo;
                //    }
                //}

                slot["ServiceUnitID"] = apt.ServiceUnitID;
                slot["ParamedicID"] = apt.ParamedicID;
                slot["AppointmentDate"] = apt.AppointmentDate;
                slot["AppointmentTime"] = apt.AppointmentTime;
                slot["AppointmentQue"] = apt.AppointmentQue;
                slot["AppointmentNo"] = apt.AppointmentNo;
                slot["PatientID"] = apt.PatientID ?? string.Empty;
                slot["FirstName"] = apt.FirstName;
                slot["MiddleName"] = apt.MiddleName;
                slot["LastName"] = apt.LastName;
                if (apt.DateOfBirth.HasValue)
                {
                    slot["DateOfBirth"] = apt.DateOfBirth.Value;
                }
                slot["StreetName"] = apt.StreetName;
                slot["District"] = apt.District;
                slot["City"] = apt.City;
                slot["County"] = apt.County;
                slot["State"] = apt.State;
                slot["ZipCode"] = apt.ZipCode ?? string.Empty;
                slot["PhoneNo"] = apt.PhoneNo;
                slot["Email"] = apt.Email;
                slot["GuarantorID"] = apt.GuarantorID;
                slot["Notes"] = apt.Notes;
                slot["AppointmentStatus"] = apt.SRAppointmentStatus;
                slot["MedicalNo"] = "";
                slot["BirthPlace"] = apt.BirthPlace;
                slot["Sex"] = apt.Sex;
                slot["Ssn"] = apt.Ssn;
                if (Version != "1_0")
                {
                    slot["MobilePhoneNo"] = apt.MobilePhoneNo;
                }
                if (!string.IsNullOrEmpty(apt.PatientID))
                {
                    var pat = new Patient();
                    if (pat.LoadByPrimaryKey(apt.PatientID))
                    {
                        slot["MedicalNo"] = pat.MedicalNo;
                    }
                }

                slot["AppointmentQueFormattedNo"] = aptQue.FormattedNo;
                return slot;
            }
            else
            {
                throw new Exception(ErrDataScheNotFound.Replace(GetErrorMessage(ErrDataScheNotFound),
                    string.Format("Related schedule not found for service unit {0}, paramedic {1}, date {2}",
                        ServiceUnitID, ParamedicID, AppointmentDate)));
            }
        }

        public void AppointmentToDataRow(string Version, Appointment apt, DataRow dr, AppointmentQueueingCollection aptqColl)
        {

            //dr[0] = apt.ServiceUnitID;
            //dr[1] = apt.ServiceUnitName;
            //dr[2] = apt.ParamedicID;
            //dr[3] = apt.ParamedicName;
            //dr[4] = apt.AppointmentDate;
            //dr[5] = apt.AppointmentTime;
            //dr[6] = apt.AppointmentQue;
            //dr[7] = apt.AppointmentNo;
            //dr[8] = apt.PatientID ?? string.Empty;
            //dr[9] = apt.FirstName;
            //dr[10] = apt.MiddleName;
            //dr[11] = apt.LastName;
            //if (apt.DateOfBirth.HasValue)
            //{
            //    dr[12] = apt.DateOfBirth.Value;
            //}
            //dr[13] = apt.StreetName;
            //dr[14] = apt.District;
            //dr[15] = apt.City;
            //dr[16] = apt.County;
            //dr[17] = apt.State;
            //dr[18] = apt.ZipCode ?? string.Empty;
            //dr[19] = apt.PhoneNo;
            //dr[20] = apt.Email;
            //dr[21] = apt.GuarantorID;
            //dr[22] = apt.GuarantorName;
            //dr[23] = apt.Notes;
            //dr[24] = apt.SRAppointmentStatus;
            //dr[25] = apt.AppointmentStatusName;
            //dr[26] = apt.MedicalNo;
            //dr[27] = apt.BirthPlace;
            //dr[28] = apt.Sex;
            //dr[29] = apt.Ssn;
            //if (Version != "1_0")
            //{
            //    dr[30] = apt.MobilePhoneNo;
            //}

            dr["ServiceUnitID"] = apt.ServiceUnitID;
            dr["ServiceUnitName"] = apt.ServiceUnitName;
            dr["ParamedicID"] = apt.ParamedicID;
            dr["ParamedicName"] = apt.ParamedicName;
            dr["AppointmentDate"] = apt.AppointmentDate;
            dr["AppointmentTime"] = apt.AppointmentTime;
            dr["AppointmentQue"] = apt.AppointmentQue;
            dr["AppointmentNo"] = apt.AppointmentNo;
            dr["PatientID"] = apt.PatientID ?? string.Empty;
            dr["FirstName"] = apt.FirstName;
            dr["MiddleName"] = apt.MiddleName;
            dr["LastName"] = apt.LastName;
            if (apt.DateOfBirth.HasValue)
            {
                dr["DateOfBirth"] = apt.DateOfBirth.Value;
            }
            dr["StreetName"] = apt.StreetName;
            dr["District"] = apt.District;
            dr["City"] = apt.City;
            dr["County"] = apt.County;
            dr["State"] = apt.State;
            dr["ZipCode"] = apt.ZipCode ?? string.Empty;
            dr["PhoneNo"] = apt.PhoneNo;
            dr["Email"] = apt.Email;
            dr["GuarantorID"] = apt.GuarantorID;
            dr["GuarantorName"] = apt.GuarantorName;
            dr["Notes"] = apt.Notes;
            dr["AppointmentStatus"] = apt.SRAppointmentStatus;
            dr["AppointmentStatusName"] = apt.AppointmentStatusName;
            dr["MedicalNo"] = apt.MedicalNo;
            dr["BirthPlace"] = apt.BirthPlace;
            dr["Sex"] = apt.Sex;
            dr["Ssn"] = apt.Ssn;
            if (Version != "1_0")
            {
                dr["MobilePhoneNo"] = apt.MobilePhoneNo;
            }

            dr["AppointmentQueFormattedNo"] = "";
            var aptq = aptqColl.Where(a => a.AppointmentNo == apt.AppointmentNo).OrderBy(a => a.Id).FirstOrDefault();
            if (aptq != null)
            {
                dr["AppointmentQueFormattedNo"] = aptq.FormattedNo;
            }
        }

        #endregion

        #region Registration Common
        public static VwParamedicLeaveDateCollection GetPhycisianOnLeave(string ParamedicID, DateTime date)
        {
            var pldQuery = new VwParamedicLeaveDateQuery();
            pldQuery.Where(
                pldQuery.ParamedicID == ParamedicID &&
                pldQuery.LeaveDate == date //(new DateTime()).NowAtSqlServer().Date
            );
            var coll = new VwParamedicLeaveDateCollection();
            coll.Load(pldQuery);
            return coll;
        }
        public static string ValidatePhycisianOnLeave(string ParamedicID, DateTime date, string Lang)
        {
            //var pldQuery = new VwParamedicLeaveDateQuery();
            //pldQuery.Where(
            //    pldQuery.ParamedicID == ParamedicID &&
            //    pldQuery.LeaveDate == date //(new DateTime()).NowAtSqlServer().Date
            //    );
            //DataTable dtPld = pldQuery.LoadDataTable();
            var coll = GetPhycisianOnLeave(ParamedicID, date);
            if (coll.Count > 0)
            {
                return (Lang == "en") ? "Physician on leave, please contact registration staff for more information." : "Dokter yang bersangkutan sedang cuti, silahkan menghubungi staff pendaftaran untuk keterangan lebih lanjut.";
            }
            return string.Empty;
        }


        private static void PatientCreateNewEmercencyContact(EmergencyContact emContact)
        {
            emContact.AddNew();

        }
        public static void PatientSetValue(Patient pat,
            string FirstName, string MiddleName, string LastName, string DateOfBirth,
            string BirthPlace, string Sex,
            string StreetName, string District, string City, string County, string State,
            string ZipCode, string PhoneNo, string MobilePhoneNo, string Email, string Ssn, string GuarantorID, string Notes,
            string SRReligion, string SREducation, string SROccupation, string SRMaritalStatus,
            string SRTitle)
        {
            pat.FirstName = FirstName;
            pat.MiddleName = MiddleName;
            pat.LastName = LastName;
            DateTime Temp;
            if (DateTime.TryParse(DateOfBirth, out Temp) == true)
                pat.DateOfBirth = DateTime.ParseExact(DateOfBirth, "yyyy-MM-dd", null);
            pat.CityOfBirth = BirthPlace;
            pat.Sex = Sex;
            pat.StreetName = StreetName;
            pat.District = District;
            pat.City = City;
            pat.County = County;
            pat.State = State;
            pat.ZipCode = ZipCode;
            pat.PhoneNo = PhoneNo;
            if (!string.IsNullOrEmpty(MobilePhoneNo))
            {
                pat.MobilePhoneNo = MobilePhoneNo;
            }
            pat.Email = Email;
            pat.Ssn = Ssn;
            pat.GuarantorID = GuarantorID;
            pat.Notes = Notes;

            pat.SRReligion = SRReligion;
            pat.SREducation = SREducation;
            pat.SROccupation = SROccupation;
            pat.SRMaritalStatus = SRMaritalStatus;
            pat.SRTitle = SRTitle;

            if (pat.es.IsAdded)
            {
                pat.SRSalutation = string.Empty;
                pat.SRBloodType = string.Empty;
                pat.BloodRhesus = "+";
                pat.SREthnic = "LL";
                pat.SRNationality = "01";
                pat.SRPatientCategory = "PatientCategory-001";
                pat.SRMedicalFileBin = string.Empty;
                pat.SRMedicalFileStatus = string.Empty;
                pat.Company = string.Empty;
                pat.FaxNo = string.Empty;
                pat.MobilePhoneNo = string.Empty;
                pat.OldMedicalNo = string.Empty;
                pat.AccountNo = string.Empty;
                pat.PictureFileName = string.Empty;
                pat.IsDonor = false;
                pat.NumberOfDonor = 0;
                pat.IsBlackList = false;
                pat.IsAlive = true;
                pat.IsActive = true;

                pat.NumberOfVisit = 0;
                pat.ValidateCreateNew();
            }
        }

        public static string CreateRegistrationFromAppointment(string AppointmentNo, string Lang)
        {
            //if (string.IsNullOrEmpty(AppointmentNo)) throw new Exception("AppointmentNo required");
            //var appt = new Appointment();
            //if (!appt.LoadByPrimaryKey(AppointmentNo))
            //{
            //    throw new Exception((Lang == "en") ? "Appointment not found" : "Appointment tidak ditemukan");
            //}
            //if (string.IsNullOrEmpty(appt.PatientID) && !AllowCreateNewPatient)
            //{
            //    throw new Exception((Lang == "en") ? "Can not create registration for new patient, please contact registration officer" : "Maaf, khusus pasien baru harus melakukan registrasi pada counter pandaftaran");
            //}

            return CreateRegistration(AppointmentNo, false,
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", Lang);
        }

        public static void RegistrationSaveEntity(Appointment appt, Registration reg, Patient patient,
            PatientEmergencyContact patEmContact, RegistrationResponsiblePerson regResp,
            EmergencyContact regEmContact, ServiceUnitQue que, MergeBilling billing,
            TransCharges chargesHD, TransChargesItemCollection chargesDT, TransChargesItemCompCollection compDT,
            TransChargesItemConsumptionCollection consDT, CostCalculationCollection cost,
            MedicalFileStatus fileStatus, out string itemNoStock, MedicalRecordFileStatus mrFileStatus,
            AppAutoNumberLast _autoNumberReg, AppAutoNumberLast _autoNumberLastPID)
        {
            AppAutoNumberLast _autoNumberMRN;

            itemNoStock = string.Empty;

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(reg.ServiceUnitID);

            using (var trans = new esTransactionScope())
            {
                //Nurul - Taruh di atas supaya AppointmentNo nya terisi dengan RegistrationNo, dan RegistrationNo tidak null
                _autoNumberReg = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, BusinessObject.Reference.TransactionCode.Registration,
                                                             AppSession.Parameter.OutPatientDepartmentID);
                //Registrasi
                if (reg.RegistrationNo == string.Empty) reg.RegistrationNo = _autoNumberReg.LastCompleteNumber;
                //AutoNumber
                _autoNumberReg.Save();

                reg.Save();

                //Appointment
                if (appt != null)
                {
                    appt.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusClosed;
                    appt.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
                    appt.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    appt.Save();

                    var aQue = new AppointmentQueueing();
                    if (aQue.SetQueForPoli(appt.AppointmentNo, AppSession.UserLogin.UserID))
                        aQue.Save();
                }
                else
                {
                    var aQue = new AppointmentQueueing();
                    if (aQue.SetQueForPoliByReg(reg, AppSession.Parameter.GuarantorAskesID[0].Contains(reg.GuarantorID) ? "02" : AppSession.Parameter.SelfGuarantor.Equals(reg.GuarantorID) ? "01" : "03", unit, AppSession.UserLogin.UserID, false))
                        aQue.Save();
                }

                if (patient.es.IsAdded)
                {
                    _autoNumberLastPID.Save();
                }

                //Patient
                if (string.IsNullOrEmpty(patient.MedicalNo))
                {
                    if (unit.IsGenerateMedicalNo ?? false)
                    {
                        var pat = new PatientCollection();
                        do
                        {
                            _autoNumberMRN = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MedicalNo);
                            patient.MedicalNo = _autoNumberMRN.LastCompleteNumber;
                            _autoNumberMRN.Save();

                            pat.QueryReset();
                            pat.Query.Where(pat.Query.MedicalNo.Trim() == patient.MedicalNo);
                            pat.LoadAll();

                        } while (pat.Any());
                    }
                }
                //patient.NumberOfVisit++;
                patient.Save();
                patEmContact.Save();

                if (regResp.RegistrationNo == string.Empty) regResp.RegistrationNo = reg.RegistrationNo;
                regResp.Save();

                if (regEmContact.RegistrationNo == string.Empty) regEmContact.RegistrationNo = reg.RegistrationNo;
                regEmContact.Save();

                //ServiceUnitQue: txtParamedicID & txtServiceUnitID disable bila modus edit
                if (que.RegistrationNo == string.Empty) que.RegistrationNo = reg.RegistrationNo;
                que.Save();

                //MergeBilling
                if (billing.RegistrationNo == string.Empty) billing.RegistrationNo = reg.RegistrationNo;
                billing.Save();

                if (true)
                {
                    //auto bill
                    if (chargesDT.Count > 0)
                    {
                        if (chargesHD.RegistrationNo == string.Empty) chargesHD.RegistrationNo = reg.RegistrationNo;
                        chargesHD.Save();

                        // stock calculation
                        // charges
                        var chargesBalances = new ItemBalanceCollection();
                        var chargesDetailBalances = new ItemBalanceDetailCollection();
                        var chargesDetailBalanceEds = new ItemBalanceDetailEdCollection();
                        var chargesMovements = new ItemMovementCollection();

                        ItemBalance.PrepareItemBalances(chargesDT, unit.ServiceUnitID, unit.GetMainLocationId(unit.ServiceUnitID), AppSession.UserLogin.UserID,
                            true, ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, ref chargesDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl, out itemNoStock);

                        chargesDT.Save();
                        compDT.Save();

                        foreach (var c in cost)
                        {
                            if (c.RegistrationNo == string.Empty) c.RegistrationNo = reg.RegistrationNo;
                        }
                        cost.Save();

                        if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                        {
                            // extract fee
                            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                            feeColl.SetFeeByTCIC(compDT, AppSession.UserLogin.UserID);
                            feeColl.Save();
                            //feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                            //feeColl.Save();
                        }

                        if (chargesBalances != null)
                            chargesBalances.Save();
                        if (chargesDetailBalances != null)
                            chargesDetailBalances.Save();
                        if (chargesDetailBalanceEds != null)
                            chargesDetailBalanceEds.Save();
                        if (chargesMovements != null)
                            chargesMovements.Save();

                        // consumption
                        var consumptionBalances = new ItemBalanceCollection();
                        var consumptionDetailBalances = new ItemBalanceDetailCollection();
                        var consumptionDetailBalanceEds = new ItemBalanceDetailEdCollection();
                        var consumptionMovements = new ItemMovementCollection();

                        ItemBalance.PrepareItemBalances(consDT, unit.ServiceUnitID, unit.GetMainLocationId(unit.ServiceUnitID), AppSession.UserLogin.UserID,
                            ref consumptionBalances, ref consumptionDetailBalances, ref consumptionMovements, ref consumptionDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl,
                            out itemNoStock);

                        if (!string.IsNullOrEmpty(itemNoStock))
                            return;

                        consDT.Save();

                        if (consumptionBalances != null)
                            consumptionBalances.Save();
                        if (consumptionDetailBalances != null)
                            consumptionDetailBalances.Save();
                        if (consumptionDetailBalanceEds != null)
                            consumptionDetailBalanceEds.Save();
                        if (consumptionMovements != null)
                            consumptionMovements.Save();
                    }

                    //Medical Status Files
                    //if (fileStatus != null)
                    //    fileStatus.Save();
                    if (mrFileStatus != null)
                    {
                        try
                        {
                            if (mrFileStatus.RegistrationNo == string.Empty) mrFileStatus.RegistrationNo = reg.RegistrationNo;
                            mrFileStatus.Save();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                //throw new Exception("break for testing");

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            try
            {
                /* Automatic Journal Testing Start */
                //if (!AppSession.Parameter.IsUsingIntermBill)
                //{
                //    int? journalId = JournalTransactions.AddNewIncomeJournal(chargesHD, compDT, reg, unit, cost,
                //                                                             "SU", AppSession.UserLogin.UserID, 0);
                //}
                /* Automatic Journal Testing End */


                /* Automatic Journal Testing Start */
                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                {
                    if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                    {
                        JournalTransactions.AddNewPatientIncomeAccrual(BusinessObject.JournalType.Income, chargesHD.TransactionNo, AppSession.UserLogin.UserID, 0);
                    }
                    else
                    {
                        var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                        if (type.Contains(reg.SRRegistrationType))
                        {
                            //auto bill
                            int? journalId = JournalTransactions.AddNewIncomeJournalTemporary(chargesHD, compDT, reg, unit, cost,
                                                                                     "SU", AppSession.UserLogin.UserID, 0);
                        }
                    }

                }
                /* Automatic Journal Testing End */
            }
            catch (Exception ex)
            {
            }
        }

        public static void RegistrationSetEntityValue(Appointment appt_, Registration reg, Patient patient, bool IsGuarantorIdFromPatient,
        RegistrationResponsiblePerson regResp, EmergencyContact regEmContact,
        ServiceUnitQue que, MergeBilling billing, TransCharges chargesHD, TransChargesItemCollection TransChargesItemsDT,
        TransChargesItemCompCollection TransChargesItemsDTComp, TransChargesItemConsumptionCollection TransChargesItemsDTConsumption,
        CostCalculationCollection CostCalculations, MedicalFileStatus fileStatus, string ServiceUnitID,
        string ParamedicID, MedicalRecordFileStatus mrFileStatus, AppAutoNumberLast _autoNumberTrans,
        string NameOfTheResponsible, string SRRelationship, string ResponsibleAddress,
        string ResponsiblePhoneNo, string Lang, DateTime? RegistrationDate = null)
        {
            var dateNow = RegistrationDate ?? (new DateTime()).NowAtSqlServer();

            #region validasi duplikasi reg
            if (!AppSession.Parameter.IsAllowMultipleRegOp)
            {
                var regColl = new RegistrationCollection();
                regColl.Query.Where(regColl.Query.PatientID == patient.PatientID,
                    regColl.Query.IsVoid == false,
                    regColl.Query.IsClosed == false,
                    regColl.Query.RegistrationDate.Date() == dateNow.Date);
                if (regColl.LoadAll())
                {
                    throw new Exception(ErrDataMultipleFound.Replace(
                        GetErrorMessage(ErrDataMultipleFound),
                        "Patient is already registered, multiple registration is invalid."));
                }
            }

            #endregion

            #region Registration

            reg.AddNew();
            string GuarantorID = appt_ == null ?
                (string.IsNullOrEmpty(AppSession.Parameter.DefaultGuarantorKiosk) ? AppSession.Parameter.SelfGuarantor : AppSession.Parameter.DefaultGuarantorKiosk) :
                patient.GuarantorID;

            if (IsGuarantorIdFromPatient)
            {
                GuarantorID = patient.GuarantorID;
            }

            reg.RegistrationNo = string.Empty; //_autoNumberReg.LastCompleteNumber;
            reg.SRRegistrationType = AppConstant.RegistrationType.OutPatient;
            reg.ParamedicID = ParamedicID; //appt.ParamedicID;
            reg.GuarantorID = GuarantorID;
            reg.PatientID = patient.PatientID; //appt.PatientID;

            //reg.ClassID = AppSession.Parameter.OutPatientClassID;

            var classId = AppSession.Parameter.OutPatientClassID;

            var unitQueury = new ServiceUnitQuery();
            unitQueury.Where(unitQueury.ServiceUnitID == ServiceUnitID);
            unitQueury.Select(unitQueury.DefaultChargeClassID);
            var u = new ServiceUnit();
            if (u.Load(unitQueury) && !string.IsNullOrEmpty(u.DefaultChargeClassID))
            {
                classId = u.DefaultChargeClassID;
            }

            reg.ClassID = classId;

            reg.RegistrationDate = dateNow.Date;
            reg.RegistrationTime = dateNow.ToString("HH:mm");
            reg.AppointmentNo = appt_ == null ? "" : appt_.AppointmentNo;
            reg.AgeInYear = byte.Parse(Helper.GetAgeInYear(patient.DateOfBirth ?? dateNow.Date).ToString());
            reg.AgeInMonth = byte.Parse(Helper.GetAgeInMonth(patient.DateOfBirth ?? dateNow.Date).ToString());
            reg.AgeInDay = byte.Parse(Helper.GetAgeInDay(patient.DateOfBirth ?? dateNow.Date).ToString());
            reg.SRShift = Registration.GetShiftID(); //Helper.GetShiftID(appt.AppointmentTime);
            reg.AccountNo = string.Empty;
            reg.SRPatientInType = AppSession.Parameter.PatientInTypeOp;
            reg.InsuranceID = string.Empty;
            reg.SRPatientCategory = patient.SRPatientCategory;
            //reg.SRERCaseType
            //reg.SRVisitReason = 
            var guarantor = new Guarantor();
            if (guarantor.LoadByPrimaryKey(GuarantorID))
            {
                reg.SRBussinesMethod = guarantor.SRBusinessMethod;
                reg.PlavonAmount = 0;
            }
            else
            {
                throw new Exception((Lang == "en") ?
                   "Invalid App Parameter (DefaultGuarantorKiosk / SelfGuarantor) value, guarantor not found, please contact administrator!" :
                   "Parameter (DefaultGuarantorKiosk / SelfGuarantor) tidak sah, guarantor tidak ditemukan, silahkan menghubungi administrator!"
               );
            }

            regResp.AddNew();
            regResp.RegistrationNo = string.Empty; //reg.RegistrationNo;
            regResp.NameOfTheResponsible = NameOfTheResponsible;
            regResp.SRRelationship = SRRelationship;
            regResp.HomeAddress = ResponsibleAddress;
            regResp.PhoneNo = ResponsiblePhoneNo;

            regEmContact.AddNew();
            regEmContact.RegistrationNo = string.Empty; //reg.RegistrationNo;

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(ServiceUnitID);
            reg.DepartmentID = unit.DepartmentID;
            reg.ServiceUnitID = ServiceUnitID;

            var roomQ = new ServiceRoomQuery();
            roomQ.Where(roomQ.ServiceUnitID == ServiceUnitID);
            var rooms = new ServiceRoomCollection();
            rooms.Load(roomQ);
            if (rooms.Count == 0)
            {
                throw new Exception((Lang == "en") ?
                    "There is no room for unit " + unit.ServiceUnitName + " defined, please contact administrator!" :
                    "Ruang praktek untuk unit " + unit.ServiceUnitName + " belum terdefinisi, silahkan menghubungi administrator!"
                );
            }
            else if (rooms.Count == 1)
            {
                reg.RoomID = rooms[0].RoomID;
            }
            else
            {
                var defRoomNoSetting = reg.RoomID = rooms[0].RoomID;
                rooms = new ServiceRoomCollection();
                rooms.Query.Where(
                    rooms.Query.ServiceUnitID == ServiceUnitID,
                    rooms.Query.ParamedicID1 == ParamedicID
                    );
                rooms.LoadAll();

                reg.RoomID = rooms.Count == 1 ? rooms[0].RoomID : string.Empty;

                if (rooms.Count != 1)
                {
                    // cari default room untuk Service Unit dan Dokter yang bersangkutan
                    var supC = new ServiceUnitParamedicCollection();
                    supC.Query.Where(supC.Query.ServiceUnitID == ServiceUnitID,
                        supC.Query.ParamedicID == ParamedicID);
                    supC.LoadAll();
                    reg.RoomID = supC.Count > 0 ? supC[0].DefaultRoomID : string.Empty;
                }
                if (reg.RoomID == string.Empty) reg.RoomID = defRoomNoSetting;
            }
            // reg.BedID

            if (!string.IsNullOrEmpty(unit.DefaultChargeClassID))
            {
                reg.ClassID = unit.DefaultChargeClassID;
            }
            reg.ChargeClassID = reg.ClassID;
            reg.CoverageClassID = reg.ClassID;

            reg.VisitTypeID = appt_ == null ? "VT001" /*dipatok dulu ya*/ : appt_.VisitTypeID;
            reg.IsPrintingPatientCard = false;
            reg.IsTransferedToInpatient = false;
            reg.IsNewBornInfant = false;
            reg.IsParturition = false;
            reg.IsRoomIn = false;
            reg.IsHoldTransactionEntry = false;
            reg.IsHasCorrection = false;
            reg.IsEMRValid = false;
            reg.IsBackDate = false;
            reg.IsVoid = false;
            reg.IsClosed = false;
            reg.IsFromDispensary = false;
            if (reg.es.IsAdded)
            {
                reg.LastCreateUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;
                reg.LastCreateDateTime = (new DateTime()).NowAtSqlServer();
            }
            reg.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
            reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            // new visit
            var query = new RegistrationQuery();
            query.es.Top = 1;
            query.Where
                (
                    query.PatientID == reg.PatientID,
                    query.ServiceUnitID == reg.ServiceUnitID
                );

            var entity = new Registration();
            reg.IsNewVisit = !entity.Load(query);

            reg.IsGlobalPlafond = true;
            reg.SmfID = string.Empty;
            reg.PatientAdm = 0;
            reg.GuarantorAdm = 0;
            #endregion

            #region Patient
            patient.GuarantorID = GuarantorID;
            if (appt_ == null)
            {
                patient.LastVisitDate = dateNow; //(new DateTime()).NowAtSqlServer();
            }
            else
            {
                patient.LastVisitDate = appt_.AppointmentDate;
                if (!string.IsNullOrEmpty(appt_.PhoneNo) && appt_.PhoneNo != patient.PhoneNo)
                {
                    patient.PhoneNo = appt_.PhoneNo;
                }
                if (!string.IsNullOrEmpty(appt_.MobilePhoneNo) && appt_.MobilePhoneNo != patient.MobilePhoneNo)
                {
                    patient.MobilePhoneNo = appt_.MobilePhoneNo;
                }
            }
            //patient.LastVisitDate = appt_ == null ? (new DateTime()).NowAtSqlServer() : appt_.AppointmentDate;
            patient.NumberOfVisit++;

            patient.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
            patient.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();


            #endregion
            #region ServiceUnitQue

            //Add
            que.AddNew();
            que.QueDate = reg.RegistrationDate.Value + TimeSpan.Parse(reg.RegistrationTime);
            que.RegistrationNo = string.Empty; //reg.RegistrationNo;
            que.ParamedicID = reg.ParamedicID;
            que.ServiceUnitID = reg.ServiceUnitID;

            var medic = new Paramedic();
            medic.LoadByPrimaryKey(que.ParamedicID);
            //if (medic.IsUsingQue ?? false)
            //{
            //    que.QueNo = appt.AppointmentQue;
            //}
            //else
            //    que.QueNo = ServiceUnitQue.GetNewQueNo(appt.ServiceUnitID, appt.ParamedicID, reg.RegistrationDate ?? (new DateTime()).NowAtSqlServer().Date);

            // --------------------------
            var sch = new ParamedicScheduleDate();
            if (sch.LoadByPrimaryKey(reg.ServiceUnitID, reg.ParamedicID,
                                     reg.RegistrationDate.Value.Year.ToString(), reg.RegistrationDate.Value.Date))
            {
                var sp = new ServiceUnitParamedic();
                if (sp.LoadByPrimaryKey(reg.ServiceUnitID, reg.ParamedicID))
                {
                    if (sp.IsUsingQue ?? false)
                    {
                        if (appt_ == null)
                        {
                            // cari slot otomatis
                            var slots = Registration.AppointmentSlotTime(reg.ServiceUnitID, reg.ParamedicID,
                                reg.RegistrationDate.Value.Date, true);

                            if (slots.Rows.Count < 2)
                            {
                                throw new Exception((Lang == "en") ?
                                    "Schedule for physician " + medic.ParamedicName + " doesn't exist!" :
                                    "Jadwal praktek untuk dokter " + medic.ParamedicName + " belum ada!"
                                );
                            }

                            int slotNo = 0;
                            string slotTime = string.Empty;
                            foreach (System.Data.DataRow s in slots.Rows)
                            {
                                if (s["SlotNo"].ToString() == "0") continue;

                                DateTime sDate = (DateTime)s["Start"];
                                //if (sDate < (new DateTime()).NowAtSqlServer()) continue; // jam untuk slot ini sudah lewat

                                DateTime endDTime = (DateTime)s["OperationalEnd"];
                                if (endDTime < (new DateTime()).NowAtSqlServer()) continue; // jam praktek sudah habis

                                var aSubject = s["Subject"].ToString().Split('-');
                                if (aSubject.Length > 2) continue; // this slot is not empty

                                slotNo = System.Convert.ToInt16(s["SlotNo"]);
                                que.QueNo = slotNo;
                                reg.RegistrationTime = sDate.TimeOfDay.ToString().Substring(0, 5);
                                que.QueDate = reg.RegistrationDate.Value + TimeSpan.Parse(reg.RegistrationTime);
                                break;
                            }
                            if (slotNo == 0)
                            {
                                throw new Exception((Lang == "en") ?
                                    "Schedule for physician " + medic.ParamedicName + " is full!" :
                                    "Jadwal praktek untuk dokter " + medic.ParamedicName + " sudah penuh untuk hari ini!"
                                );
                            }
                            //if (slotNo == 0)
                            //{
                            //    // create new que
                            //    que.QueNo = ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, ParamedicID, reg.RegistrationDate.Value.Date);
                            //}
                        }
                        else
                        {
                            // pick from appointment
                            que.QueNo = appt_.AppointmentQue;
                        }
                    }
                    else
                        que.QueNo = ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, ParamedicID, reg.RegistrationDate.Value.Date);
                }
                else
                    que.QueNo = ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, ParamedicID, reg.RegistrationDate.Value.Date);
            }
            else
                que.QueNo = ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, ParamedicID, reg.RegistrationDate.Value.Date);
            // --------------------------


            que.ServiceRoomID = reg.RoomID;
            que.IsFromReferProcess = false;
            que.StartTime = que.QueDate;
            que.IsStopped = false;
            que.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
            que.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            reg.RegistrationQue = que.QueNo;

            #endregion
            #region auto bill & visite item (outpatient)

            if (true)
            {
                var patientCardItemId = AppSession.Parameter.PatientCardItemID;

                var grr = new Guarantor();
                grr.LoadByPrimaryKey(reg.GuarantorID);

                var regCount = new RegistrationCollection();
                regCount.Query.Where(regCount.Query.PatientID == reg.PatientID,
                                     regCount.Query.RegistrationDate == reg.RegistrationDate,
                                     regCount.Query.SRRegistrationType == reg.SRRegistrationType,
                                     regCount.Query.IsVoid == false);
                regCount.LoadAll();

                #region GetListAutoBill
                var billColl = new ServiceUnitAutoBillItemCollection();
                if (string.IsNullOrEmpty(reg.VisiteRegistrationNo))
                {
                    billColl.Query.Where
                        (
                            billColl.Query.ServiceUnitID == reg.ServiceUnitID,
                            billColl.Query.IsActive == true,
                            billColl.Query.ItemID != patientCardItemId
                        );
                    billColl.Query.Where(billColl.Query.IsGenerateOnRegistration == true);
                    billColl.LoadAll();

                    // paramedic autobill
                    var parColl = new ParamedicAutoBillItemCollection();
                    parColl.Query.Where
                        (
                            parColl.Query.ParamedicID == reg.ParamedicID,
                            parColl.Query.ServiceUnitID == reg.ServiceUnitID,
                            parColl.Query.IsActive == true,
                            parColl.Query.IsGenerateOnRegistration == true
                        );
                    parColl.LoadAll();

                    foreach (var par in parColl)
                    {
                        var suabi = billColl.AddNew();
                        suabi.ServiceUnitID = string.Empty;
                        suabi.ItemID = par.ItemID;
                        suabi.Quantity = par.Quantity;

                        var item = new ItemService();
                        suabi.SRItemUnit = item.LoadByPrimaryKey(suabi.ItemID) ? item.SRItemUnit : "X";

                        suabi.IsAutoPayment = false;
                        suabi.IsActive = true;
                        suabi.IsGenerateOnRegistration = par.IsGenerateOnRegistration;
                        suabi.IsGenerateOnNewRegistration = par.IsGenerateOnRegistration;
                        suabi.IsGenerateOnReferral = par.IsGenerateOnReferral;
                        suabi.IsGenerateOnFirstRegistration = false;
                        suabi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        suabi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                    // end of paramedic autobill

                    if (AppSession.Parameter.IsVisibleGuarantorAutoBillItem)
                    {
                        // guarantor autobill
                        var guarColl = new GuarantorAutoBillItemCollection();
                        guarColl.Query.Where
                            (
                                guarColl.Query.GuarantorID == reg.GuarantorID,
                                guarColl.Query.ServiceUnitID == reg.ServiceUnitID,
                                guarColl.Query.IsActive == true,
                                guarColl.Query.IsGenerateOnRegistration == true
                            );
                        guarColl.LoadAll();
                        foreach (var guar in guarColl)
                        {
                            var suabi = billColl.AddNew();
                            suabi.ServiceUnitID = string.Empty;
                            suabi.ItemID = guar.ItemID;
                            suabi.Quantity = guar.Quantity;

                            var item = new ItemService();
                            suabi.SRItemUnit = item.LoadByPrimaryKey(suabi.ItemID) ? item.SRItemUnit : "X";

                            suabi.IsAutoPayment = false;
                            suabi.IsActive = true;
                            suabi.IsGenerateOnRegistration = guar.IsGenerateOnRegistration;
                            suabi.IsGenerateOnNewRegistration = guar.IsGenerateOnRegistration;
                            suabi.IsGenerateOnReferral = guar.IsGenerateOnReferral;
                            suabi.IsGenerateOnFirstRegistration = guar.IsGenerateOnFirstRegistration;
                            suabi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            suabi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        }
                        // end of guarantor autobill
                    }

                    foreach (var bill in billColl)
                    {
                        if (bill.IsGenerateOnFirstRegistration == true && regCount.Count > 0) bill.MarkAsDeleted();
                    }
                }
                else
                {
                    //var visites = new TransPaymentItemVisiteCollection();
                    //visites.Query.Where(visites.Query.PaymentNo == cboVisite.SelectedValue);
                    //visites.LoadAll();

                    //foreach (var visite in visites)
                    //{
                    //    var bill = billColl.AddNew();
                    //    bill.ServiceUnitID = reg.ServiceUnitID;
                    //    bill.ItemID = visite.ItemID;
                    //    bill.Quantity = 1;
                    //    bill.SRItemUnit = "X";
                    //    bill.IsAutoPayment = true;
                    //    bill.IsActive = true;
                    //    bill.IsGenerateOnRegistration = true;
                    //    bill.IsGenerateOnNewRegistration = false;
                    //    bill.IsGenerateOnReferral = false;
                    //    bill.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    //    bill.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
                    //}
                }
                #endregion
                #region Create Header For Autobill trans
                if (billColl.Count > 0)
                {
                    _autoNumberTrans = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.TransactionNo);
                    chargesHD.TransactionNo = _autoNumberTrans.LastCompleteNumber;
                    _autoNumberTrans.Save();

                    chargesHD.RegistrationNo = string.Empty; //reg.RegistrationNo;
                    chargesHD.TransactionDate = (new DateTime()).NowAtSqlServer().Date; //reg.RegistrationDate;
                    chargesHD.ExecutionDate = reg.RegistrationDate;
                    chargesHD.ReferenceNo = string.Empty;
                    chargesHD.FromServiceUnitID = reg.ServiceUnitID;
                    chargesHD.ToServiceUnitID = reg.ServiceUnitID;
                    chargesHD.ClassID = reg.ChargeClassID;
                    chargesHD.RoomID = reg.RoomID;
                    chargesHD.BedID = reg.BedID;
                    chargesHD.DueDate = (new DateTime()).NowAtSqlServer().Date;
                    chargesHD.SRShift = reg.SRShift;
                    chargesHD.SRItemType = string.Empty;
                    chargesHD.IsProceed = false;
                    chargesHD.IsBillProceed = true;
                    chargesHD.IsApproved = true;
                    chargesHD.IsVoid = false;
                    chargesHD.IsOrder = false;
                    chargesHD.IsCorrection = false;
                    chargesHD.IsClusterAssign = false;
                    chargesHD.IsAutoBillTransaction = true;
                    chargesHD.Notes = string.Empty;
                    chargesHD.SurgicalPackageID = string.Empty;
                    chargesHD.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
                    chargesHD.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    chargesHD.IsPackage = false;
                    chargesHD.IsRoomIn = reg.IsRoomIn;
                    var room = new ServiceRoom();
                    room.LoadByPrimaryKey(reg.RoomID);
                    chargesHD.TariffDiscountForRoomIn = room.TariffDiscountForRoomIn;
                    //chargesHD.PackageReferenceNo = null;
                }
                #endregion
                #region Create Detail For Autobill trans
                //------

                //------
                string seqNo = string.Empty;
                foreach (ServiceUnitAutoBillItem billItem in billColl)
                {
                    var item = new Item();
                    item.LoadByPrimaryKey(billItem.ItemID);
                    string itemTypeHD = item.SRItemType;

                    seqNo = TransChargesItemsDT.Count == 0 ? "001" : string.Format("{0:000}", int.Parse(TransChargesItemsDT[TransChargesItemsDT.Count - 1].SequenceNo) + 1);

                    var chargesDT = TransChargesItemsDT.AddNew();
                    chargesDT.TransactionNo = chargesHD.TransactionNo;
                    chargesDT.SequenceNo = seqNo;
                    chargesDT.ReferenceNo = string.Empty;
                    chargesDT.ReferenceSequenceNo = string.Empty;
                    chargesDT.ItemID = billItem.ItemID;
                    chargesDT.ChargeClassID = reg.ChargeClassID;
                    chargesDT.ParamedicID = string.Empty;

                    //var tariff = (Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, grr.SRTariffType, chargesHD.ClassID, chargesHD.ClassID, billItem.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                    //              Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, chargesHD.ClassID, billItem.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                    //             (Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, chargesHD.ClassID, chargesHD.ClassID, billItem.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                    //              Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, chargesHD.ClassID, billItem.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));

                    var tariff = (Helper.Tariff.GetItemTariff(chargesHD.ExecutionDate.Value, grr.SRTariffType, chargesHD.ClassID, chargesHD.ClassID, billItem.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                  Helper.Tariff.GetItemTariff(chargesHD.ExecutionDate.Value, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, chargesHD.ClassID, billItem.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                 (Helper.Tariff.GetItemTariff(chargesHD.ExecutionDate.Value, AppSession.Parameter.DefaultTariffType, chargesHD.ClassID, chargesHD.ClassID, billItem.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                  Helper.Tariff.GetItemTariff(chargesHD.ExecutionDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, chargesHD.ClassID, billItem.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));

                    if (tariff == null) throw new Exception((Lang == "en") ?
                        string.Format("Invalid price for autobill item of {0}", item.ItemName) :
                        string.Format("Tarif untuk autobill {0} belum diatur", item.ItemName));

                    chargesDT.IsAdminCalculation = tariff.IsAdminCalculation ?? false;

                    switch (itemTypeHD)
                    {
                        case BusinessObject.Reference.ItemType.Service:
                            var service = new ItemService();
                            service.LoadByPrimaryKey(billItem.ItemID);
                            chargesDT.SRItemUnit = service.SRItemUnit;

                            chargesDT.CostPrice = tariff.Price ?? 0;
                            break;
                        case BusinessObject.Reference.ItemType.Medical:
                            var ipm = new ItemProductMedic();
                            ipm.LoadByPrimaryKey(billItem.ItemID);
                            chargesDT.SRItemUnit = ipm.SRItemUnit;

                            chargesDT.CostPrice = ipm.CostPrice ?? 0;
                            break;
                        case BusinessObject.Reference.ItemType.NonMedical:
                            var ipn = new ItemProductNonMedic();
                            ipn.LoadByPrimaryKey(billItem.ItemID);
                            chargesDT.SRItemUnit = ipn.SRItemUnit;

                            chargesDT.CostPrice = ipn.CostPrice ?? 0;
                            break;
                        case BusinessObject.Reference.ItemType.Laboratory:
                        case BusinessObject.Reference.ItemType.Diagnostic:
                        case BusinessObject.Reference.ItemType.Radiology:
                            chargesDT.SRItemUnit = string.Empty;
                            chargesDT.CostPrice = tariff.Price ?? 0;
                            break;
                    }

                    chargesDT.IsVariable = false;
                    chargesDT.IsCito = false;
                    chargesDT.IsCitoInPercent = false;
                    chargesDT.BasicCitoAmount = (decimal)0D;
                    chargesDT.ChargeQuantity = billItem.Quantity;

                    if (itemTypeHD == BusinessObject.Reference.ItemType.Medical || itemTypeHD == BusinessObject.Reference.ItemType.NonMedical)
                        chargesDT.StockQuantity = billItem.Quantity;
                    else
                        chargesDT.StockQuantity = (decimal)0D;

                    var itemRooms = new AppStandardReferenceItemCollection();
                    itemRooms.Query.Where(itemRooms.Query.StandardReferenceID == "ItemTariffRoom",
                                          itemRooms.Query.ItemID == billItem.ItemID, itemRooms.Query.IsActive == true);
                    itemRooms.LoadAll();
                    chargesDT.IsItemRoom = itemRooms.Count > 0;

                    chargesDT.Price = tariff.Price ?? 0;
                    if (chargesDT.IsItemRoom == true && chargesHD.IsRoomIn == true)
                        chargesDT.Price = chargesDT.Price - (chargesDT.Price * chargesHD.TariffDiscountForRoomIn / 100);

                    chargesDT.DiscountAmount = (decimal)0D;

                    chargesDT.CitoAmount = (decimal)0D;
                    chargesDT.RoundingAmount = Helper.RoundingDiff;
                    chargesDT.SRDiscountReason = string.Empty;
                    chargesDT.IsAssetUtilization = false;
                    chargesDT.AssetID = string.Empty;
                    chargesDT.IsBillProceed = true;
                    chargesDT.IsOrderRealization = false;
                    chargesDT.IsPackage = false;
                    chargesDT.IsApprove = true;
                    chargesDT.IsVoid = false;
                    chargesDT.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
                    chargesDT.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    chargesDT.ParentNo = string.Empty;
                    chargesDT.SRCenterID = string.Empty;
                    chargesDT.ItemConditionRuleID = string.Empty;

                    if (Helper.GuarantorBpjsCasemix.Contains(reg.GuarantorID) && AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                        chargesDT.IsCasemixApproved = Helper.IsCasemixApproved(chargesDT.ItemID, chargesDT.ChargeQuantity ?? 0, reg.RegistrationNo, chargesDT.TransactionNo, reg.GuarantorID, false);
                    else
                        chargesDT.IsCasemixApproved = true;

                    #region item component

                    var compQuery = new ItemTariffComponentQuery();
                    compQuery.es.Top = 1;
                    compQuery.Where
                        (
                            compQuery.SRTariffType == grr.SRTariffType,
                            compQuery.ItemID == billItem.ItemID,
                            compQuery.ClassID == reg.ChargeClassID,
                            compQuery.StartingDate <= (new DateTime()).NowAtSqlServer().Date
                        );

                    //var compColl = Helper.Tariff.GetItemTariffComponentCollection(chargesHD.TransactionDate.Value, grr.SRTariffType, chargesHD.ClassID, billItem.ItemID);
                    //if (!compColl.Any())
                    //    compColl = Helper.Tariff.GetItemTariffComponentCollection(chargesHD.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, billItem.ItemID);

                    var compColl = Helper.Tariff.GetItemTariffComponentCollection(chargesHD.ExecutionDate.Value, grr.SRTariffType, chargesHD.ClassID, billItem.ItemID);
                    if (!compColl.Any())
                        compColl = Helper.Tariff.GetItemTariffComponentCollection(chargesHD.ExecutionDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, billItem.ItemID);

                    var p = string.Empty;
                    foreach (var comp in compColl)
                    {
                        var compCharges = TransChargesItemsDTComp.AddNew();
                        compCharges.TransactionNo = chargesHD.TransactionNo;
                        compCharges.SequenceNo = seqNo;
                        compCharges.TariffComponentID = comp.TariffComponentID;
                        if (chargesHD.IsRoomIn == true && chargesDT.IsItemRoom == true)
                            compCharges.Price = comp.Price - (comp.Price * chargesHD.TariffDiscountForRoomIn / 100);
                        else
                            compCharges.Price = comp.Price;
                        if (string.IsNullOrEmpty(reg.VisiteRegistrationNo))
                            compCharges.DiscountAmount = (decimal)0D;
                        else
                        {
                            //var visites = new TransPaymentItemVisiteQuery();
                            //visites.SelectAll().Where(visites.PaymentNo == cboVisite.SelectedValue);
                            //var visit = new TransPaymentItemVisite();
                            //visit.Load(visites);
                            //compCharges.DiscountAmount = visit.Price * (visit.Discount / 100);
                        }
                        compCharges.CitoAmount = (decimal)0D;

                        var tcomp = new TariffComponent();
                        tcomp.LoadByPrimaryKey(comp.TariffComponentID);

                        if (tcomp.IsTariffParamedic ?? false)
                            compCharges.ParamedicID = reg.ParamedicID;
                        else
                            compCharges.ParamedicID = string.Empty;


                        compCharges.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
                        compCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        if (!string.IsNullOrEmpty(compCharges.ParamedicID))
                        {
                            if (tcomp.IsPrintParamedicInSlip ?? false)
                            {
                                var par = new Paramedic();
                                par.LoadByPrimaryKey(compCharges.ParamedicID);
                                if (par.IsPrintInSlip ?? true)
                                    p = p.Length == 0 ? par.ParamedicName : p + "; " + par.ParamedicName;
                            }
                        }
                    }
                    chargesDT.ParamedicCollectionName = p;

                    #endregion

                    #region Item Consumption

                    var consQuery = new ItemConsumptionQuery();
                    consQuery.Where(consQuery.ItemID == billItem.ItemID);

                    var consColl = new ItemConsumptionCollection();
                    consColl.Load(consQuery);

                    foreach (var cons in consColl)
                    {
                        var consCharges = TransChargesItemsDTConsumption.AddNew();
                        consCharges.TransactionNo = chargesHD.TransactionNo;
                        consCharges.SequenceNo = seqNo;
                        consCharges.DetailItemID = cons.ItemID;
                        consCharges.Qty = cons.Qty;
                        consCharges.SRItemUnit = cons.SRItemUnit;
                        consCharges.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
                        consCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    }

                    #endregion
                }
                #endregion
                #region auto calculation

                if (TransChargesItemsDT.Count > 0)
                {
                    var grrID = reg.GuarantorID;
                    if (grrID == AppSession.Parameter.SelfGuarantor)
                    {
                        var pat = new Patient();
                        pat.LoadByPrimaryKey(reg.PatientID);
                        if (!string.IsNullOrEmpty(pat.MemberID))
                            grrID = pat.MemberID;
                    }

                    DataTable tblCovered = Helper.GetCoveredItems((Registration)reg, reg.SRBussinesMethod, reg.PlavonAmount ?? 0, reg.IsGlobalPlafond ?? false,
                        reg.ChargeClassID, reg.CoverageClassID, grrID, TransChargesItemsDT.Select(t => t.ItemID).ToArray(),
                        (new DateTime()).NowAtSqlServer(), new RegistrationItemRuleCollection(), false);

                    //DataTable tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, reg.SRBussinesMethod, reg.PlavonAmount ?? 0, reg.IsGlobalPlafond ?? false,
                    //    reg.ChargeClassID, reg.CoverageClassID, grrID, TransChargesItemsDT.Select(t => t.ItemID).ToArray(),
                    //    (new DateTime()).NowAtSqlServer(), new RegistrationItemRuleCollection(), false);

                    foreach (TransChargesItem detail in TransChargesItemsDT)
                    {
                        var rowCovered = tblCovered.AsEnumerable().Where(t => t.Field<string>("ItemID") == detail.ItemID &&
                                                                              t.Field<bool>("IsInclude")).SingleOrDefault();

                        //TransChargesItemComps
                        if (rowCovered != null)
                        {
                            decimal? discount = 0;
                            bool isDiscount = false, isMargin = false;
                            foreach (var comp in TransChargesItemsDTComp.Where(t => t.TransactionNo == detail.TransactionNo &&
                                                                                    t.SequenceNo == detail.SequenceNo)
                                                                        .OrderBy(t => t.TariffComponentID))
                            {
                                decimal? amountValue = (decimal?)rowCovered["AmountValue"];
                                if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                                {
                                    if ((comp.Price - comp.DiscountAmount) <= 0)
                                        continue;

                                    if ((bool)rowCovered["IsValueInPercent"])
                                    {
                                        comp.DiscountAmount += (amountValue / 100) * comp.Price;
                                        comp.AutoProcessCalculation = 0 - (amountValue / 100) * comp.Price;
                                    }
                                    else
                                    {
                                        if (!isDiscount)
                                        {
                                            if (discount == 0)
                                            {
                                                if (comp.Price >= amountValue)
                                                {
                                                    comp.DiscountAmount += amountValue;
                                                    comp.AutoProcessCalculation = 0 - amountValue;
                                                    isDiscount = true;
                                                }
                                                else
                                                {
                                                    comp.DiscountAmount += comp.Price;
                                                    comp.AutoProcessCalculation = 0 - comp.Price;
                                                    discount = amountValue - comp.Price;
                                                }
                                            }
                                            else
                                            {
                                                if (comp.Price >= discount)
                                                {
                                                    comp.DiscountAmount += discount;
                                                    comp.AutoProcessCalculation = 0 - discount;
                                                    isDiscount = true;
                                                }
                                                else
                                                {
                                                    comp.DiscountAmount += comp.Price;
                                                    comp.AutoProcessCalculation = 0 - comp.Price;
                                                    discount -= comp.Price;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                                {
                                    if ((bool)rowCovered["IsValueInPercent"])
                                    {
                                        comp.AutoProcessCalculation = (amountValue / 100) * comp.Price;
                                        comp.Price += (amountValue / 100) * comp.Price;

                                    }
                                    else
                                    {
                                        if (!isMargin)
                                        {
                                            comp.Price += amountValue;
                                            comp.AutoProcessCalculation = amountValue;
                                            isMargin = true;
                                        }
                                    }
                                }
                            }
                        }

                        //TransChargesItems
                        if (TransChargesItemsDTComp.Count > 0)
                        {
                            detail.AutoProcessCalculation = TransChargesItemsDTComp.Where(t => t.TransactionNo == detail.TransactionNo &&
                                                                                               t.SequenceNo == detail.SequenceNo)
                                                                                   .Sum(t => t.AutoProcessCalculation);
                            if (detail.AutoProcessCalculation < 0)
                            {
                                detail.DiscountAmount += detail.ChargeQuantity * Math.Abs(detail.AutoProcessCalculation ?? 0);

                                if (detail.DiscountAmount > detail.Price)
                                {
                                    detail.DiscountAmount = detail.Price;
                                    detail.AutoProcessCalculation = 0 - detail.Price;
                                }
                            }
                            else if (detail.AutoProcessCalculation > 0)
                                detail.Price += detail.AutoProcessCalculation;
                        }
                        else
                        {
                            if (rowCovered != null)
                            {
                                if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                                {
                                    if ((bool)rowCovered["IsValueInPercent"])
                                    {
                                        detail.DiscountAmount += (detail.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * detail.Price);
                                        detail.AutoProcessCalculation = 0 - (detail.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * detail.Price);
                                    }
                                    else
                                    {
                                        detail.DiscountAmount += (detail.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];
                                        detail.AutoProcessCalculation = 0 - (detail.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];
                                    }

                                    if (detail.DiscountAmount > detail.Price)
                                        detail.DiscountAmount = detail.Price;
                                }
                                else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                                {
                                    if ((bool)rowCovered["IsValueInPercent"])
                                    {
                                        detail.AutoProcessCalculation = ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;
                                        detail.Price += ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;

                                    }
                                    else
                                    {
                                        detail.Price += (decimal)rowCovered["AmountValue"];
                                        detail.AutoProcessCalculation = (decimal)rowCovered["AmountValue"];
                                    }
                                }
                            }
                        }

                        //post
                        decimal? total = ((detail.ChargeQuantity * detail.Price) - detail.DiscountAmount) + detail.CitoAmount;
                        //var calc = new Helper.CostCalculation(grrID, detail.ItemID, total ?? 0, tblCovered, detail.ChargeQuantity ?? 0,
                        //                                          detail.IsCito ?? false,
                        //                                          detail.IsCitoInPercent ?? false,
                        //                                          detail.BasicCitoAmount ?? 0, detail.Price ?? 0,
                        //                                          chargesHD.IsRoomIn ?? false, detail.IsItemRoom ?? false,
                        //                                          chargesHD.TariffDiscountForRoomIn ?? 0, detail.DiscountAmount ?? 0, false,
                        //                                          detail.ItemConditionRuleID, chargesHD.TransactionDate.Value, detail.IsVariable ?? false);

                        var calc = new Helper.CostCalculation(grrID, detail.ItemID, total ?? 0, tblCovered, detail.ChargeQuantity ?? 0,
                                                                  detail.IsCito ?? false,
                                                                  detail.IsCitoInPercent ?? false,
                                                                  detail.BasicCitoAmount ?? 0, detail.Price ?? 0,
                                                                  chargesHD.IsRoomIn ?? false, detail.IsItemRoom ?? false,
                                                                  chargesHD.TariffDiscountForRoomIn ?? 0, detail.DiscountAmount ?? 0, false,
                                                                  detail.ItemConditionRuleID, chargesHD.ExecutionDate.Value, detail.IsVariable ?? false);

                        CostCalculation cost = CostCalculations.AddNew();
                        cost.RegistrationNo = string.Empty; //reg.RegistrationNo;
                        cost.TransactionNo = detail.TransactionNo;
                        cost.SequenceNo = detail.SequenceNo;
                        cost.ItemID = detail.ItemID;

                        //start here
                        decimal? totaltrans = calc.GuarantorAmount + calc.PatientAmount + (detail.DiscountAmount ?? 0);
                        decimal? totaldisc = detail.DiscountAmount ?? 0;

                        if (reg.SRBussinesMethod == AppSession.Parameter.BusinessMethodFlavon)
                        {
                            if (totaldisc >= totaltrans)
                            {
                                cost.GuarantorAmount = 0;
                                cost.PatientAmount = 0;
                            }
                            else
                            {
                                cost.GuarantorAmount = totaltrans - totaldisc;
                                cost.PatientAmount = 0;
                            }
                            cost.DiscountAmount = totaldisc;
                        }
                        else
                        {
                            if (calc.GuarantorAmount > 0)
                            {
                                cost.DiscountAmount = totaldisc > (calc.GuarantorAmount + detail.DiscountAmount)
                                                           ? (calc.GuarantorAmount + detail.DiscountAmount)
                                                           : totaldisc;

                                cost.GuarantorAmount = totaldisc > (calc.GuarantorAmount + detail.DiscountAmount)
                                                           ? 0
                                                           : (calc.GuarantorAmount + detail.DiscountAmount) - totaldisc;
                                cost.PatientAmount = calc.PatientAmount;

                            }
                            else
                            {
                                cost.DiscountAmount = totaldisc > calc.PatientAmount + detail.DiscountAmount
                                                          ? calc.PatientAmount + detail.DiscountAmount
                                                          : totaldisc;

                                cost.PatientAmount = totaldisc > calc.PatientAmount + detail.DiscountAmount
                                                         ? 0
                                                         : calc.PatientAmount + detail.DiscountAmount - totaldisc;
                                cost.GuarantorAmount = calc.GuarantorAmount;
                            }

                            if (totaldisc > cost.DiscountAmount)
                            {
                                //hitung ulang diskon di TransChargesItem & TransChargesItemComp
                                var compColl = TransChargesItemsDTComp.Where(
                                        t =>
                                        t.TransactionNo == detail.TransactionNo &&
                                        t.SequenceNo == detail.SequenceNo)
                                        .OrderBy(t => t.TariffComponentID);
                                var i = compColl.Count();

                                foreach (var compEntity in compColl)
                                {
                                    compEntity.DiscountAmount = i == 1
                                                               ? (cost.DiscountAmount / Math.Abs(detail.ChargeQuantity ?? 0))
                                                               : (compEntity.Price + compEntity.CitoAmount) * (cost.DiscountAmount / detail.DiscountAmount);

                                    var fee = compEntity.CalculateParamedicPercentDiscount(
                                        AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                        cost.RegistrationNo, detail.ItemID, (compEntity.DiscountAmount ?? 0),
                                        AppSession.UserLogin.UserID, chargesHD.ClassID, chargesHD.ToServiceUnitID);

                                }

                                detail.DiscountAmount = cost.DiscountAmount;
                                detail.Save();
                            }
                        }
                        //end

                        //cost.PatientAmount = calc.PatientAmount;
                        //cost.GuarantorAmount = calc.GuarantorAmount;
                        //cost.DiscountAmount = detail.DiscountAmount;
                        cost.IsPackage = detail.IsPackage;
                        cost.ParentNo = detail.ParentNo;
                        cost.ParamedicAmount = detail.ChargeQuantity * TransChargesItemsDTComp.Where(comp => comp.TransactionNo == detail.TransactionNo &&
                                                                                                             comp.SequenceNo == detail.SequenceNo &&
                                                                                                             !string.IsNullOrEmpty(comp.ParamedicID))
                                                                                              .Sum(comp => comp.Price - comp.DiscountAmount);
                        cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        cost.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
                    }
                }

                #endregion

                reg.RemainingAmount = CostCalculations.Sum(c => (c.PatientAmount + c.GuarantorAmount));
            }


            #endregion

            #region Merge Billing
            if (true)
            {
                billing.RegistrationNo = string.Empty; //reg.RegistrationNo;
                billing.FromRegistrationNo = string.Empty;
                billing.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
                billing.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
            #endregion
            #region Insert Medical File Status


            //var bStatus = fileStatus.LoadByPrimaryKey(reg.PatientID);

            //if (!bStatus) //&& !contact.LoadByPrimaryKey(txtRegistrationNo.Text))
            //    fileStatus.AddNew();

            ////if (!fileStatus.LoadByPrimaryKey(reg.PatientID))
            ////{
            //    //fileStatus.AddNew();
            //    //if (fileStatus != null)
            //    //{
            //    fileStatus.PatientID = reg.PatientID;
            //    fileStatus.TransactionDate = (new DateTime()).NowAtSqlServer().Date;
            //    fileStatus.SRMedicalFileStatusCategory = AppSession.Parameter.MedicalFileCategoryOut;
            //    fileStatus.SRMedicalFileStatus = AppSession.Parameter.MedicalFileStatusRequest;
            //    fileStatus.Expeditor = string.Empty;
            //    fileStatus.DepartmentID = reg.DepartmentID;
            //    fileStatus.ServiceUnitID = reg.ServiceUnitID;
            //    fileStatus.ParamedicID = reg.ParamedicID;
            //    fileStatus.Notes = string.Empty;

            //    //Last Update Status
            //    if (fileStatus.es.IsAdded || fileStatus.es.IsModified)
            //    {
            //        fileStatus.LastUpdateByUserID = AppSession.UserLogin.UserID; // AppSession.UserLogin.UserID;;
            //        fileStatus.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            //    }
            ////}
            #endregion Insert Medical File Status
            #region Insert Medical Record File Status

            if (!mrFileStatus.LoadByPrimaryKey(reg.RegistrationNo))
            {
                mrFileStatus.AddNew();

                mrFileStatus.RegistrationNo = string.Empty; //reg.RegistrationNo;
                mrFileStatus.FileOutDate = (new DateTime()).NowAtSqlServer();

                mrFileStatus.SRMedicalFileCategory = AppSession.Parameter.MedicalFileCategoryOut;
                mrFileStatus.SRMedicalFileStatus = AppSession.Parameter.MedicalFileStatusRequest;
                mrFileStatus.Notes = string.Empty;
                mrFileStatus.RequestByUserID = AppSession.UserLogin.UserID;

                mrFileStatus.LastUpdateByUserID = AppSession.UserLogin.UserID;
                mrFileStatus.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
            #endregion Insert Medical File Status
        }
        public static string CreateRegistration(string AppointmentNo, bool AllowCreateNewPatient,
            string FirstName, string MiddleName, string LastName, string DateOfBirth,
            string BirthPlace, string Sex,
            string StreetName, string District, string City, string County, string State,
            string ZipCode, string PhoneNo, string MobilePhoneNo, string Email, string Ssn, string GuarantorID, string Notes,
            string SRReligion, string SREducation, string SROccupation, string SRMaritalStatus, string SRTitle,
            string ResponsibleName, string SRRelationship, string ResponsibleAddress, string ResponsiblePhoneNo,
            string Lang)
        {
            if (string.IsNullOrEmpty(AppointmentNo)) throw new Exception("AppointmentNo required");
            var appt = new Appointment();
            if (!appt.LoadByPrimaryKey(AppointmentNo))
            {
                throw new Exception((Lang == "en") ? "Appointment not found" : "Appointment tidak ditemukan");
            }
            if (string.IsNullOrEmpty(appt.PatientID) && !AllowCreateNewPatient)
            {
                throw new Exception((Lang == "en") ? "Can not create registration for new patient, please contact registration officer" : "Maaf, khusus pasien baru harus melakukan registrasi pada counter pandaftaran");
            }
            if (appt.AppointmentDate.Value.Date != (new DateTime()).NowAtSqlServer().Date)
            {
                throw new Exception((Lang == "en") ? "Invalid registration date, registration can only be created for today appointment" : "Tanggal registrasi tidak sah, registrasi hanya bisa dibuat untuk appointment hari ini");
            }

            string valMsg = ValidatePhycisianOnLeave(appt.ParamedicID, (new DateTime()).NowAtSqlServer().Date, Lang);
            if (!string.IsNullOrEmpty(valMsg)) throw new Exception(valMsg);

            var patient = new Patient();
            var patEmContact = new PatientEmergencyContact();
            var _autoNumberLastPID = new AppAutoNumberLast();

            if (string.IsNullOrEmpty(appt.PatientID) && !AllowCreateNewPatient)
            {
                // Error patient not found
                throw new Exception((Lang == "en") ? "Patient Not Found" : "Data pasien tidak ditemukan");
            }
            if (string.IsNullOrEmpty(appt.PatientID) && AllowCreateNewPatient)
            {
                patient.AddNew();
            }
            else
            {
                if (!patient.LoadByPrimaryKey(appt.PatientID))
                {
                    // Error patient not found
                    throw new Exception((Lang == "en") ? "Patient Not Found" : "Data pasien tidak ditemukan");
                }
            }

            PatientSetValue(patient, FirstName, MiddleName, LastName, DateOfBirth, BirthPlace, Sex,
                StreetName, District, City, County, State, ZipCode, PhoneNo, MobilePhoneNo, Email, Ssn, GuarantorID, Notes,
                SRReligion, SREducation, SROccupation, SRMaritalStatus, SRTitle);


            if (!patient.es.IsAdded)
            {

            }
            else
            {
                _autoNumberLastPID = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PatientID);
                patient.PatientID = _autoNumberLastPID.LastCompleteNumber;
            }

            if (!patEmContact.LoadByPrimaryKey(patient.PatientID))
            {
                // create new one
                //patEmContact.AddNew();
                patEmContact = new PatientEmergencyContact();

                patEmContact.PatientID = patient.PatientID;
            }

            // cek sudah ada registrasi atau belum untuk appointment bersangkutan
            var regQuery = new RegistrationQuery();
            regQuery.es.Top = 1;
            regQuery.Where(regQuery.AppointmentNo == appt.AppointmentNo && regQuery.IsVoid == false);
            var regCollection = new RegistrationCollection();
            Registration reg = null;
            regCollection.Load(regQuery);

            if (regCollection.Count > 0)
            {
                // appointment is registered, continue to print
                reg = regCollection[0];
            }

            if (reg == null)
            {
                // not yet registered, continue to register
                // ------------------------start
                var entity = new Registration();
                var regResp = new RegistrationResponsiblePerson();
                var regEmContact = new EmergencyContact();

                var que = new ServiceUnitQue();
                var chargesHD = new TransCharges();
                var fileStatus = new MedicalFileStatus();
                var mrFileStatus = new MedicalRecordFileStatus();
                var billing = new MergeBilling();
                var _autoNumberReg = new AppAutoNumberLast();
                var _autoNumberTrans = new AppAutoNumberLast();
                TransChargesItemCollection TransChargesItemsDT = new TransChargesItemCollection();
                TransChargesItemCompCollection TransChargesItemsDTComp = new TransChargesItemCompCollection();
                TransChargesItemConsumptionCollection TransChargesItemsDTConsumption = new TransChargesItemConsumptionCollection();
                CostCalculationCollection CostCalculations = new CostCalculationCollection();

                reg = new Registration();

                RegistrationSetEntityValue(appt, entity, patient, false, regResp, regEmContact, que, billing,
                    chargesHD, TransChargesItemsDT, TransChargesItemsDTComp, TransChargesItemsDTConsumption, CostCalculations,
                    fileStatus, appt.ServiceUnitID, appt.ParamedicID, mrFileStatus,
                    _autoNumberTrans,
                    ResponsibleName, SRRelationship, ResponsibleAddress, ResponsiblePhoneNo, "en");

                string itemNoStock;

                RegistrationSaveEntity(appt, entity, patient, patEmContact, regResp, regEmContact, que, billing,
                    chargesHD, TransChargesItemsDT, TransChargesItemsDTComp, TransChargesItemsDTConsumption, CostCalculations,
                    fileStatus, out itemNoStock, mrFileStatus, _autoNumberReg, _autoNumberLastPID);

                reg = entity;
            }

            // print slip di bagian pendaftaran, mengikuti setting registrasi rawat jalan
            if (AppSession.Parameter.IsRegistrationPrintSlip)
            {
                var parametersSlip = new PrintJobParameterCollection();
                parametersSlip.AddNew("p_RegistrationNo", reg.RegistrationNo, null, null);
                PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationSlipRpt, parametersSlip, AppSession.UserLogin.UserID);
            }


            // print slip di counter kiosk
            //PrintSlip(reg.RegistrationNo);

            // reset interface
            //ResetInterfaceRegistration();

            //return (Lang == "en") ? "Thank you for using self-registration service" : "Terima kasih sudah menggunakan layanan pendaftaran mandiri";
            return reg.RegistrationNo;
        }

        public static string CreateRegistrationFromMobileApp(string MedicalNo, DateTime RegistrationDate,
            string ServiceUnitID, string ParamedicID, string SRPaymentMethod, string BankID, string SRCardProvider, string SRCardType, string EDCMachineID, Decimal Amount, string Lang)
        {
            string valMsg = ValidatePhycisianOnLeave(ParamedicID, RegistrationDate.Date, Lang);
            if (!string.IsNullOrEmpty(valMsg)) throw new Exception(valMsg);

            var _autoNumberLastPID = new AppAutoNumberLast();

            var patient = new Patient();
            var patEmContact = new PatientEmergencyContact();

            patient.LoadByMedicalNo(MedicalNo);

            string ResponsibleName = "";
            string SRRelationship = "";
            string ResponsibleAddress = "";
            string ResponsiblePhoneNo = "";

            if (patEmContact.LoadByPrimaryKey(patient.PatientID))
            {
                ResponsibleName = patEmContact.ContactName;
                SRRelationship = patEmContact.SRRelationship;
                ResponsibleAddress = patEmContact.StreetName;
                ResponsiblePhoneNo = patEmContact.PhoneNo;
            }

            Registration reg = null;
            Appointment appt = null;

            if (reg == null)
            {
                // not yet registered, continue to register
                // ------------------------start
                var entity = new Registration();
                var regResp = new RegistrationResponsiblePerson();
                var regEmContact = new EmergencyContact();

                var que = new ServiceUnitQue();
                var chargesHD = new TransCharges();
                var fileStatus = new MedicalFileStatus();
                var mrFileStatus = new MedicalRecordFileStatus();
                var billing = new MergeBilling();
                var _autoNumberReg = new AppAutoNumberLast();
                var _autoNumberTrans = new AppAutoNumberLast();
                TransChargesItemCollection TransChargesItemsDT = new TransChargesItemCollection();
                TransChargesItemCompCollection TransChargesItemsDTComp = new TransChargesItemCompCollection();
                TransChargesItemConsumptionCollection TransChargesItemsDTConsumption = new TransChargesItemConsumptionCollection();
                CostCalculationCollection CostCalculations = new CostCalculationCollection();

                reg = new Registration();

                RegistrationSetEntityValue(appt, entity, patient, false, regResp, regEmContact, que, billing,
                    chargesHD, TransChargesItemsDT, TransChargesItemsDTComp, TransChargesItemsDTConsumption, CostCalculations,
                    fileStatus, ServiceUnitID, ParamedicID, mrFileStatus,
                    _autoNumberTrans,
                    ResponsibleName, SRRelationship, ResponsibleAddress, ResponsiblePhoneNo, "en", RegistrationDate);

                if ((entity.RemainingAmount ?? 0) != Amount)
                {
                    double diff = Convert.ToDouble(Amount) - Convert.ToDouble(entity.RemainingAmount);
                    double excess = Math.Abs(AppSession.Parameter.ExcessPaymentAmount);

                    var total = string.Format("{0:n2}", (entity.RemainingAmount ?? 0));
                    
                    if (diff < (-1) * excess)
                    {
                        return "XXX-|" + total;
                    }

                    if (!AppSession.Parameter.IsAllowExcessPaymentAmountPlus && diff > excess)
                    {
                        return "XXX+|" + total;
                    }
                }

                string itemNoStock;

                RegistrationSaveEntity(appt, entity, patient, patEmContact, regResp, regEmContact, que, billing,
                    chargesHD, TransChargesItemsDT, TransChargesItemsDTComp, TransChargesItemsDTConsumption, CostCalculations,
                    fileStatus, out itemNoStock, mrFileStatus, _autoNumberReg, _autoNumberLastPID);

                SetPayment(entity, SRPaymentMethod, BankID, SRCardProvider, SRCardType, EDCMachineID, Amount);

                reg = entity;
            }

            // print slip di bagian pendaftaran, mengikuti setting registrasi rawat jalan
            if (AppSession.Parameter.IsRegistrationPrintSlip)
            {
                var parametersSlip = new PrintJobParameterCollection();
                parametersSlip.AddNew("p_RegistrationNo", reg.RegistrationNo, null, null);
                PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationSlipRpt, parametersSlip, AppSession.UserLogin.UserID);
            }


            // print slip di counter kiosk
            //PrintSlip(reg.RegistrationNo);

            // reset interface
            //ResetInterfaceRegistration();

            //return (Lang == "en") ? "Thank you for using self-registration service" : "Terima kasih sudah menggunakan layanan pendaftaran mandiri";
            return reg.RegistrationNo;
        }

        public DataTable RegistrationReturn(string RegistrationNo)
        {
            var regQuery = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var su = new ServiceUnitQuery("su");
            var par = new ParamedicQuery("par");
            regQuery.InnerJoin(pat).On(regQuery.PatientID == pat.PatientID)
                .InnerJoin(su).On(regQuery.ServiceUnitID == su.ServiceUnitID)
                .InnerJoin(par).On(regQuery.ParamedicID == par.ParamedicID)
                .Select(regQuery.RegistrationNo, regQuery.SRRegistrationType, regQuery.ParamedicID, par.ParamedicName,
                regQuery.PatientID, pat.MedicalNo, pat.FirstName, pat.MiddleName, pat.LastName, pat.DateOfBirth,
                regQuery.RegistrationDate, regQuery.AppointmentNo, regQuery.ServiceUnitID, su.ServiceUnitName,
                regQuery.RegistrationQue, regQuery.ExternalQueNo, regQuery.IsNewVisit)
                .Where(regQuery.RegistrationNo == RegistrationNo);
            return regQuery.LoadDataTable();
        }
        #endregion

        #region PatientEmergencyContact
        public DataTable PatientEmergencyContactReturn(string PatientID)
        {
            var pecQuery = new PatientEmergencyContactQuery("pec");
            pecQuery.Select(pecQuery)
                .Where(pecQuery.PatientID == PatientID);
            return pecQuery.LoadDataTable();
        }
        #endregion

        public static void SetPayment(Registration reg, string SRPaymentMethod, string BankID, string SRCardProvider, string SRCardType, string EDCMachineID, decimal PaymentAmount)
        {
            var tp = new TransPayment();
            tp.AddNew();

            #region auto payment
            AppAutoNumberLast _autoNumberPayment = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PaymentNo);
            tp.PaymentNo = _autoNumberPayment.LastCompleteNumber;
            _autoNumberPayment.Save();
            #endregion

            tp.TransactionCode = BusinessObject.Reference.TransactionCode.Payment;
            tp.RegistrationNo = reg.RegistrationNo;
            tp.PaymentDate = (new DateTime()).NowAtSqlServer().Date;//reg.RegistrationDate;
            tp.PaymentTime = (new DateTime()).NowAtSqlServer().ToString("HH:mm"); //reg.RegistrationTime;

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);

            tp.PrintReceiptAsName = pat.PatientName;
            tp.TotalPaymentAmount = PaymentAmount;
            tp.RemainingAmount = 0;
            tp.PrintNumber = 0;
            tp.PaymentReceiptNo = string.Empty;
            tp.CreatedBy = AppSession.UserLogin.UserID;
            tp.IsVoid = false;
            tp.IsApproved = false;
            tp.Notes = string.Empty;
            tp.LastUpdateByUserID = AppSession.UserLogin.UserID;
            tp.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            tp.GuarantorID = reg.GuarantorID;

            var tpiColl = new TransPaymentItemCollection();

            var tpi = tpiColl.AddNew();
            tpi.PaymentNo = tp.PaymentNo;
            tpi.SequenceNo = "001";
            tpi.SRPaymentType = AppSession.Parameter.PaymentTypePayment;
            tpi.SRPaymentMethod = SRPaymentMethod;
            tpi.str.SRCardProvider = SRCardProvider;
            tpi.str.SRCardType = SRCardType;
            tpi.str.SRDiscountReason = string.Empty;
            tpi.str.EDCMachineID = EDCMachineID;
            tpi.CardHolderName = string.Empty;
            tpi.CardFeeAmount = 0;
            tpi.BankID = BankID;
            tpi.Amount = tp.TotalPaymentAmount;
            tpi.Balance = 0;
            tpi.IsFromDownPayment = false;
            tpi.LastUpdateByUserID = AppSession.UserLogin.UserID;
            tpi.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            var tpioColl = new TransPaymentItemOrderCollection();

            var tcq = new TransChargesQuery();
            tcq.Where(tcq.RegistrationNo == reg.RegistrationNo);
            tcq.Select(tcq.TransactionNo);

            var tciColl = new TransChargesItemCollection();
            tciColl.Query.Where(tciColl.Query.TransactionNo.In(tcq));
            tciColl.LoadAll();

            foreach (var tci in tciColl)
            {
                var tpio = tpioColl.AddNew();
                tpio.PaymentNo = tp.PaymentNo;
                tpio.TransactionNo = tci.TransactionNo;
                tpio.SequenceNo = tci.SequenceNo;
                tpio.ItemID = tci.ItemID;
                tpio.Qty = tci.ChargeQuantity ?? 0;
                tpio.Price = tci.Price ?? 0;
                tpio.LastUpdateDateTime = DateTime.Now;
                tpio.LastUpdateByUserID = AppSession.UserLogin.UserID;
                tpio.IsPaymentProceed = false;
                tpio.IsPaymentReturned = false;
                tpio.JournalIncomePaymentNo = string.Empty;
                tpio.Total = tpio.Qty * tpio.Price;
            }

            tp.Save();
            tpiColl.Save();
            tpioColl.Save();

            var tpiibColl = new TransPaymentItemIntermBillCollection();
            var tpiibgColl = new TransPaymentItemIntermBillGuarantorCollection();
            
            double remainingAmountPatient = Convert.ToDouble(tp.RemainingAmount);
            double remainingAmountGuarantor = 0;

            Helper.Payment.SetApproval(tp, tpiColl, tpioColl, tpiibColl, tpiibgColl, true, remainingAmountPatient, remainingAmountGuarantor, "PaymentWS");
        }
    }
}
