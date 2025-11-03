/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 08/08/19 9:33:04 AM
===============================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Dal.Interfaces;
using System.Web.Script.Services;

namespace Temiang.Avicenna.WebService.AvicennaMobile
{
    /// <summary>
    /// Summary description for DataService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public partial class DataServiceWsv : V0.BaseDataService
    {

        #region Generated Function


        #region AbsentCode
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AbsentCodeGet(string AccessKey, Int32 AbsentCodeID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AbsentCodeQuery();
                query.Where(query.AbsentCodeID == AbsentCodeID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AbsentCode

        #region AddMealOrder
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AddMealOrderGet(string AccessKey, String OrderNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AddMealOrderQuery();
                query.Where(query.OrderNo == OrderNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AddMealOrder

        #region AddMealOrderItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AddMealOrderItemGet(string AccessKey, String OrderNo, String FoodID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AddMealOrderItemQuery();
                query.Where(query.OrderNo == OrderNo, query.FoodID == FoodID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AddMealOrderItem

        #region AddMealOrderItemDetail
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AddMealOrderItemDetailGet(string AccessKey, String OrderNo, String FoodID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AddMealOrderItemDetailQuery();
                query.Where(query.OrderNo == OrderNo, query.FoodID == FoodID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AddMealOrderItemDetail

        #region AdvertisedPersonnelRequisition
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AdvertisedPersonnelRequisitionGet(string AccessKey, Int32 AdvertisedPersonnelRequisitionID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AdvertisedPersonnelRequisitionQuery();
                query.Where(query.AdvertisedPersonnelRequisitionID == AdvertisedPersonnelRequisitionID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AdvertisedPersonnelRequisition

        #region AnalysisDocument
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AnalysisDocumentGet(string AccessKey, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AnalysisDocumentQuery();
                query.Where(query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AnalysisDocument

        #region AnalysisDocumentItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AnalysisDocumentItemGet(string AccessKey, String RegistrationNo, Int32 DocumentFilesID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AnalysisDocumentItemQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.DocumentFilesID == DocumentFilesID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AnalysisDocumentItem

        #region AppAutoNumber
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppAutoNumberGet(string AccessKey, String SRAutoNumber, DateTime EffectiveDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppAutoNumberQuery();
                query.Where(query.SRAutoNumber == SRAutoNumber, query.EffectiveDate == EffectiveDate);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppAutoNumber

        #region AppAutoNumberLast
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppAutoNumberLastGet(string AccessKey, String SRAutoNumber, DateTime EffectiveDate, String DepartmentInitial, Int32 YearNo, Int32 MonthNo, Int32 DayNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppAutoNumberLastQuery();
                query.Where(query.SRAutoNumber == SRAutoNumber, query.EffectiveDate == EffectiveDate, query.DepartmentInitial == DepartmentInitial, query.YearNo == YearNo, query.MonthNo == MonthNo, query.DayNo == DayNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppAutoNumberLast

        #region AppAutoNumberTransactionCode
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppAutoNumberTransactionCodeGet(string AccessKey, String SRTransactionCode)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppAutoNumberTransactionCodeQuery();
                query.Where(query.SRTransactionCode == SRTransactionCode);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppAutoNumberTransactionCode

        #region AppControl
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppControlGet(string AccessKey, String ControlID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppControlQuery();
                query.Where(query.ControlID == ControlID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppControl

        #region AppControlEntryMatrix
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppControlEntryMatrixGet(string AccessKey, String HealthcareInitialAppsVersion, String EntryType, String ControlID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppControlEntryMatrixQuery();
                query.Where(query.HealthcareInitialAppsVersion == HealthcareInitialAppsVersion, query.EntryType == EntryType, query.ControlID == ControlID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppControlEntryMatrix

        #region ApplicantAppliedPositions
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ApplicantAppliedPositionsGet(string AccessKey, Int32 ApplicantAppliedPositionsID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ApplicantAppliedPositionsQuery();
                query.Where(query.ApplicantAppliedPositionsID == ApplicantAppliedPositionsID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ApplicantAppliedPositions

        #region ApplicantContact
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ApplicantContactGet(string AccessKey, Int32 ApplicantContactID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ApplicantContactQuery();
                query.Where(query.ApplicantContactID == ApplicantContactID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ApplicantContact

        #region ApplicantEducationHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ApplicantEducationHistoryGet(string AccessKey, Int32 ApplicantEducationHistoryID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ApplicantEducationHistoryQuery();
                query.Where(query.ApplicantEducationHistoryID == ApplicantEducationHistoryID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ApplicantEducationHistory

        #region ApplicantFamily
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ApplicantFamilyGet(string AccessKey, Int32 ApplicantFamilyID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ApplicantFamilyQuery();
                query.Where(query.ApplicantFamilyID == ApplicantFamilyID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ApplicantFamily

        #region ApplicantInfo
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ApplicantInfoGet(string AccessKey, Int32 ApplicantID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ApplicantInfoQuery();
                query.Where(query.ApplicantID == ApplicantID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ApplicantInfo

        #region ApplicantLicence
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ApplicantLicenceGet(string AccessKey, Int32 ApplicantLicenceID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ApplicantLicenceQuery();
                query.Where(query.ApplicantLicenceID == ApplicantLicenceID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ApplicantLicence

        #region ApplicantPhysical
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ApplicantPhysicalGet(string AccessKey, Int32 ApplicantPhysicalID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ApplicantPhysicalQuery();
                query.Where(query.ApplicantPhysicalID == ApplicantPhysicalID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ApplicantPhysical

        #region ApplicantPsychological
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ApplicantPsychologicalGet(string AccessKey, Int32 ApplicantPsychologicalID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ApplicantPsychologicalQuery();
                query.Where(query.ApplicantPsychologicalID == ApplicantPsychologicalID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ApplicantPsychological

        #region ApplicantReferences
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ApplicantReferencesGet(string AccessKey, Int32 ApplicantReferencesID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ApplicantReferencesQuery();
                query.Where(query.ApplicantReferencesID == ApplicantReferencesID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ApplicantReferences

        #region ApplicantWorkExperience
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ApplicantWorkExperienceGet(string AccessKey, Int32 ApplicantWorkExperienceID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ApplicantWorkExperienceQuery();
                query.Where(query.ApplicantWorkExperienceID == ApplicantWorkExperienceID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ApplicantWorkExperience

        #region AppMessage
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppMessageGet(string AccessKey, String MessageID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppMessageQuery();
                query.Where(query.MessageID == MessageID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppMessage

        #region Appointment
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppointmentGet(string AccessKey, String AppointmentNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppointmentQuery();
                query.Where(query.AppointmentNo == AppointmentNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Appointment

        #region AppointmentLokadok
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppointmentLokadokGet(string AccessKey, Int64 appt_id)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppointmentLokadokQuery();
                query.Where(query.ApptId == appt_id);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppointmentLokadok

        #region AppParameter
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppParameterGet(string AccessKey, String ParameterID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppParameterQuery();
                query.Where(query.ParameterID == ParameterID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppParameter

        #region AppProgram
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppProgramGet(string AccessKey, String ProgramID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppProgramQuery();
                query.Where(query.ProgramID == ProgramID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppProgram

        #region AppProgramHealthcare
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppProgramHealthcareGet(string AccessKey, String ProgramID, String HealthcareInitial)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppProgramHealthcareQuery();
                query.Where(query.ProgramID == ProgramID, query.HealthcareInitial == HealthcareInitial);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppProgramHealthcare

        #region AppProgramRelated
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppProgramRelatedGet(string AccessKey, String ProgramID, String RelatedProgramID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppProgramRelatedQuery();
                query.Where(query.ProgramID == ProgramID, query.RelatedProgramID == RelatedProgramID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppProgramRelated

        #region AppReportParameter
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppReportParameterGet(string AccessKey, String ProgramID, String ParameterName)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppReportParameterQuery();
                query.Where(query.ProgramID == ProgramID, query.ParameterName == ParameterName);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppReportParameter

        #region AppReportParameterHealthcare
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppReportParameterHealthcareGet(string AccessKey, String ProgramID, String HealthcareInitial, String ParameterName)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppReportParameterHealthcareQuery();
                query.Where(query.ProgramID == ProgramID, query.HealthcareInitial == HealthcareInitial, query.ParameterName == ParameterName);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppReportParameterHealthcare

        #region AppReportPivot
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppReportPivotGet(string AccessKey, String ProgramID, Int32 CustomPivotID, String FieldCaption)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppReportPivotQuery();
                query.Where(query.ProgramID == ProgramID, query.CustomPivotID == CustomPivotID, query.FieldCaption == FieldCaption);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppReportPivot

        #region ApprovalRange
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ApprovalRangeGet(string AccessKey, String ApprovalRangeID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ApprovalRangeQuery();
                query.Where(query.ApprovalRangeID == ApprovalRangeID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ApprovalRange

        #region ApprovalRangeUser
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ApprovalRangeUserGet(string AccessKey, String ApprovalRangeID, Int32 ApprovalLevel, String UserID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ApprovalRangeUserQuery();
                query.Where(query.ApprovalRangeID == ApprovalRangeID, query.ApprovalLevel == ApprovalLevel, query.UserID == UserID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ApprovalRangeUser

        #region ApprovalTransaction
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ApprovalTransactionGet(string AccessKey, String TransactionNo, Int32 ApprovalLevel, String UserID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ApprovalTransactionQuery();
                query.Where(query.TransactionNo == TransactionNo, query.ApprovalLevel == ApprovalLevel, query.UserID == UserID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ApprovalTransaction

        #region AppStandardReference
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppStandardReferenceGet(string AccessKey, String StandardReferenceID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppStandardReferenceQuery();
                query.Where(query.StandardReferenceID == StandardReferenceID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppStandardReference

        #region AppStandardReferenceItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppStandardReferenceItemGet(string AccessKey, String StandardReferenceID, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppStandardReferenceItemQuery();
                query.Where(query.StandardReferenceID == StandardReferenceID, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppStandardReferenceItem

        #region AppUser
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppUserGet(string AccessKey, String UserID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppUserQuery();
                query.Where(query.UserID == UserID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppUser

        #region AppUserCustomPivot
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppUserCustomPivotGet(string AccessKey, Int32 CustomPivotID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppUserCustomPivotQuery();
                query.Where(query.CustomPivotID == CustomPivotID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppUserCustomPivot

        #region AppUserGroup
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppUserGroupGet(string AccessKey, String UserGroupID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppUserGroupQuery();
                query.Where(query.UserGroupID == UserGroupID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppUserGroup

        #region AppUserGroupProgram
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppUserGroupProgramGet(string AccessKey, String UserGroupID, String ProgramID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppUserGroupProgramQuery();
                query.Where(query.UserGroupID == UserGroupID, query.ProgramID == ProgramID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppUserGroupProgram

        #region AppUserServiceUnit
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppUserServiceUnitGet(string AccessKey, String UserID, String ServiceUnitID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppUserServiceUnitQuery();
                query.Where(query.UserID == UserID, query.ServiceUnitID == ServiceUnitID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppUserServiceUnit

        #region AppUserUserGroup
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AppUserUserGroupGet(string AccessKey, String UserID, String UserGroupID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AppUserUserGroupQuery();
                query.Where(query.UserID == UserID, query.UserGroupID == UserGroupID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AppUserUserGroup

        #region AskesCovered
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AskesCoveredGet(string AccessKey, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AskesCoveredQuery();
                query.Where(query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AskesCovered

        #region AskesCovered2
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AskesCovered2Get(string AccessKey, String RegistrationNo, String SeqNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AskesCovered2Query();
                query.Where(query.RegistrationNo == RegistrationNo, query.SeqNo == SeqNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AskesCovered2

        #region AssessmentTypeBodyDiagram
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssessmentTypeBodyDiagramGet(string AccessKey, String SRAssessmentType, String BodyID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssessmentTypeBodyDiagramQuery();
                query.Where(query.SRAssessmentType == SRAssessmentType, query.BodyID == BodyID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssessmentTypeBodyDiagram

        #region Asset
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetGet(string AccessKey, String AssetID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetQuery();
                query.Where(query.AssetID == AssetID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Asset

        #region AssetBook
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetBookGet(string AccessKey, String AssetBookID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetBookQuery();
                query.Where(query.AssetBookID == AssetBookID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetBook

        #region AssetDepreciationMethod
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetDepreciationMethodGet(string AccessKey, String DepreciationMethodID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetDepreciationMethodQuery();
                query.Where(query.DepreciationMethodID == DepreciationMethodID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetDepreciationMethod

        #region AssetDepreciationPost
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetDepreciationPostGet(string AccessKey, Int32 AssetDepreciationPostId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetDepreciationPostQuery();
                query.Where(query.AssetDepreciationPostId == AssetDepreciationPostId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetDepreciationPost

        #region AssetGroup
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetGroupGet(string AccessKey, String AssetGroupId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetGroupQuery();
                query.Where(query.AssetGroupId == AssetGroupId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetGroup

        #region AssetInventoriedHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetInventoriedHistoryGet(string AccessKey, String AssetID, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetInventoriedHistoryQuery();
                query.Where(query.AssetID == AssetID, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetInventoriedHistory

        #region AssetItemService
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetItemServiceGet(string AccessKey, String ItemID, String AssetID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetItemServiceQuery();
                query.Where(query.ItemID == ItemID, query.AssetID == AssetID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetItemService

        #region AssetLocation
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetLocationGet(string AccessKey, String AssetLocationID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetLocationQuery();
                query.Where(query.AssetLocationID == AssetLocationID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetLocation

        #region AssetMaintenanceDt
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetMaintenanceDtGet(string AccessKey, Int32 MaintenanceItemId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetMaintenanceDtQuery();
                query.Where(query.MaintenanceItemId == MaintenanceItemId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetMaintenanceDt

        #region AssetMaintenanceHd
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetMaintenanceHdGet(string AccessKey, String TransactionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetMaintenanceHdQuery();
                query.Where(query.TransactionNo == TransactionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetMaintenanceHd

        #region AssetMaintenanceOrder
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetMaintenanceOrderGet(string AccessKey, String JobOrderNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetMaintenanceOrderQuery();
                query.Where(query.JobOrderNo == JobOrderNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetMaintenanceOrder

        #region AssetMovement
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetMovementGet(string AccessKey, String AssetMovementNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetMovementQuery();
                query.Where(query.AssetMovementNo == AssetMovementNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetMovement

        #region AssetPostingStatus
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetPostingStatusGet(string AccessKey, Int32 PostingId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetPostingStatusQuery();
                query.Where(query.PostingId == PostingId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetPostingStatus

        #region AssetPreventiveMaintenance
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetPreventiveMaintenanceGet(string AccessKey, String PMNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetPreventiveMaintenanceQuery();
                query.Where(query.PMNo == PMNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetPreventiveMaintenance

        #region AssetPreventiveMaintenanceSchedule
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetPreventiveMaintenanceScheduleGet(string AccessKey, String AssetID, DateTime ScheduleDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetPreventiveMaintenanceScheduleQuery();
                query.Where(query.AssetID == AssetID, query.ScheduleDate == ScheduleDate);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetPreventiveMaintenanceSchedule

        #region AssetPreventiveMaintenanceSchedulePeriod
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetPreventiveMaintenanceSchedulePeriodGet(string AccessKey, String AssetID, String PeriodYear)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetPreventiveMaintenanceSchedulePeriodQuery();
                query.Where(query.AssetID == AssetID, query.PeriodYear == PeriodYear);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetPreventiveMaintenanceSchedulePeriod

        #region AssetPreventiveMaintenanceSchedulePeriodDate
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetPreventiveMaintenanceSchedulePeriodDateGet(string AccessKey, String AssetID, String PeriodYear, DateTime PeriodDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetPreventiveMaintenanceSchedulePeriodDateQuery();
                query.Where(query.AssetID == AssetID, query.PeriodYear == PeriodYear, query.PeriodDate == PeriodDate);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetPreventiveMaintenanceSchedulePeriodDate

        #region AssetStatusHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetStatusHistoryGet(string AccessKey, Int32 SeqNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetStatusHistoryQuery();
                query.Where(query.SeqNo == SeqNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetStatusHistory

        #region AssetSubGroup
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetSubGroupGet(string AccessKey, String AssetGroupId, String AssetSubGroupId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetSubGroupQuery();
                query.Where(query.AssetGroupId == AssetGroupId, query.AssetSubGroupId == AssetSubGroupId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetSubGroup

        #region AssetUtilization
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetUtilizationGet(string AccessKey, String AssetID, String PeriodNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetUtilizationQuery();
                query.Where(query.AssetID == AssetID, query.PeriodNo == PeriodNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetUtilization

        #region AssetWorkOrder
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetWorkOrderGet(string AccessKey, String OrderNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetWorkOrderQuery();
                query.Where(query.OrderNo == OrderNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetWorkOrder

        #region AssetWorkOrderImplementer
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetWorkOrderImplementerGet(string AccessKey, String OrderNo, String UserID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetWorkOrderImplementerQuery();
                query.Where(query.OrderNo == OrderNo, query.UserID == UserID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetWorkOrderImplementer

        #region AssetWorkOrderItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AssetWorkOrderItemGet(string AccessKey, String OrderNo, String SeqNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AssetWorkOrderItemQuery();
                query.Where(query.OrderNo == OrderNo, query.SeqNo == SeqNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AssetWorkOrderItem

        #region AtePatientsControl
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AtePatientsControlGet(string AccessKey, String OrderNo, String SRMealSet)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AtePatientsControlQuery();
                query.Where(query.OrderNo == OrderNo, query.SRMealSet == SRMealSet);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AtePatientsControl

        #region AttedanceMatrix
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AttedanceMatrixGet(string AccessKey, Int32 AttedanceMatrixID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AttedanceMatrixQuery();
                query.Where(query.AttedanceMatrixID == AttedanceMatrixID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AttedanceMatrix

        #region AuditLog
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AuditLogGet(string AccessKey, Int32 AuditLogID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AuditLogQuery();
                query.Where(query.AuditLogID == AuditLogID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AuditLog

        #region AuditLogData
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AuditLogDataGet(string AccessKey, Int32 AuditLogID, String ColumnName)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AuditLogDataQuery();
                query.Where(query.AuditLogID == AuditLogID, query.ColumnName == ColumnName);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AuditLogData

        #region AuditLogSetting
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AuditLogSettingGet(string AccessKey, String TableName)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AuditLogSettingQuery();
                query.Where(query.TableName == TableName);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AuditLogSetting

        #region AveragePriceHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AveragePriceHistoryGet(string AccessKey, String TransactionNo, String ItemID, Decimal OldAveragePrice)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AveragePriceHistoryQuery();
                query.Where(query.TransactionNo == TransactionNo, query.ItemID == ItemID, query.OldAveragePrice == OldAveragePrice);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion AveragePriceHistory

        #region Award
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AwardGet(string AccessKey, Int32 AwardID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new AwardQuery();
                query.Where(query.AwardID == AwardID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Award

        #region Bank
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BankGet(string AccessKey, String BankID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BankQuery();
                query.Where(query.BankID == BankID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Bank

        #region BankAccount
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BankAccountGet(string AccessKey, String BankID, String BankAccountNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BankAccountQuery();
                query.Where(query.BankID == BankID, query.BankAccountNo == BankAccountNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion BankAccount

        #region BankAccountBalance
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BankAccountBalanceGet(string AccessKey, Int32 BalanceId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BankAccountBalanceQuery();
                query.Where(query.BalanceId == BalanceId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion BankAccountBalance

        #region Bed
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BedGet(string AccessKey, String BedID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BedQuery();
                query.Where(query.BedID == BedID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Bed

        #region BedManagement
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BedManagementGet(string AccessKey, Int64 BedManagementID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BedManagementQuery();
                query.Where(query.BedManagementID == BedManagementID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion BedManagement

        #region BedRoomIn
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BedRoomInGet(string AccessKey, String BedID, String RegistrationNo, DateTime DateOfEntry, String TimeOfEntry)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BedRoomInQuery();
                query.Where(query.BedID == BedID, query.RegistrationNo == RegistrationNo, query.DateOfEntry == DateOfEntry, query.TimeOfEntry == TimeOfEntry);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion BedRoomIn

        #region BiayaJabatan
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BiayaJabatanGet(string AccessKey, Int32 BiayaJabatanID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BiayaJabatanQuery();
                query.Where(query.BiayaJabatanID == BiayaJabatanID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion BiayaJabatan

        #region BillingAdjustItemGroupSetting
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BillingAdjustItemGroupSettingGet(string AccessKey, String ItemGroupID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BillingAdjustItemGroupSettingQuery();
                query.Where(query.ItemGroupID == ItemGroupID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion BillingAdjustItemGroupSetting

        #region BillingAdjustItemSetting
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BillingAdjustItemSettingGet(string AccessKey, Int32 Id)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BillingAdjustItemSettingQuery();
                query.Where(query.Id == Id);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion BillingAdjustItemSetting

        #region BillingToPatient
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BillingToPatientGet(string AccessKey, String BillingNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BillingToPatientQuery();
                query.Where(query.BillingNo == BillingNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion BillingToPatient

        #region BillTransferHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BillTransferHistoryGet(string AccessKey, String RegistrationNo, DateTime ProcessDateTime, String ProcessByUserID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BillTransferHistoryQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.ProcessDateTime == ProcessDateTime, query.ProcessByUserID == ProcessByUserID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion BillTransferHistory

        #region BirthAttendantsRecord
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BirthAttendantsRecordGet(string AccessKey, String RegistrationNo, String ParamedicID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BirthAttendantsRecordQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.ParamedicID == ParamedicID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion BirthAttendantsRecord

        #region BirthRecord
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BirthRecordGet(string AccessKey, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BirthRecordQuery();
                query.Where(query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion BirthRecord

        #region BodyDiagram
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BodyDiagramGet(string AccessKey, String BodyID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BodyDiagramQuery();
                query.Where(query.BodyID == BodyID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion BodyDiagram

        #region BodyDiagramServiceUnit
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BodyDiagramServiceUnitGet(string AccessKey, String BodyID, String ServiceUnitID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BodyDiagramServiceUnitQuery();
                query.Where(query.BodyID == BodyID, query.ServiceUnitID == ServiceUnitID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion BodyDiagramServiceUnit

        #region BpjsCMG
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BpjsCMGGet(string AccessKey, String NoSEP, String KodeCMG)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BpjsCMGQuery();
                query.Where(query.NoSEP == NoSEP, query.KodeCMG == KodeCMG);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion BpjsCMG

        #region BpjsPackage
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BpjsPackageGet(string AccessKey, String PackageID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BpjsPackageQuery();
                query.Where(query.PackageID == PackageID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion BpjsPackage

        #region BpjsPackageTariff
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BpjsPackageTariffGet(string AccessKey, String PackageID, DateTime StartingDate, String ClassID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BpjsPackageTariffQuery();
                query.Where(query.PackageID == PackageID, query.StartingDate == StartingDate, query.ClassID == ClassID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion BpjsPackageTariff

        #region BpjsSEP
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BpjsSEPGet(string AccessKey, Int64 SepID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new BpjsSEPQuery();
                query.Where(query.SepID == SepID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion BpjsSEP

        #region CashManagement
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CashManagementGet(string AccessKey, String TransactionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new CashManagementQuery();
                query.Where(query.TransactionNo == TransactionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion CashManagement

        #region CashManagementCashier
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CashManagementCashierGet(string AccessKey, String TransactionNo, String CashierUserID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new CashManagementCashierQuery();
                query.Where(query.TransactionNo == TransactionNo, query.CashierUserID == CashierUserID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion CashManagementCashier

        #region CashTransactionBalance
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CashTransactionBalanceGet(string AccessKey, Int32 TxnBalanceId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new CashTransactionBalanceQuery();
                query.Where(query.TxnBalanceId == TxnBalanceId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion CashTransactionBalance

        #region CashTransactionDetail
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CashTransactionDetailGet(string AccessKey, Int32 DetailId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new CashTransactionDetailQuery();
                query.Where(query.DetailId == DetailId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion CashTransactionDetail

        #region CashTransactionList
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CashTransactionListGet(string AccessKey, String ListId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new CashTransactionListQuery();
                query.Where(query.ListId == ListId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion CashTransactionList

        #region CashTransactionListItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CashTransactionListItemGet(string AccessKey, Int32 ListItemId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new CashTransactionListItemQuery();
                query.Where(query.ListItemId == ListItemId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion CashTransactionListItem

        #region CensusBalance
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CensusBalanceGet(string AccessKey, DateTime CensusDate, String ServiceUnitID, String ClassID, String SmfID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new CensusBalanceQuery();
                query.Where(query.CensusDate == CensusDate, query.ServiceUnitID == ServiceUnitID, query.ClassID == ClassID, query.SmfID == SmfID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion CensusBalance

        #region ChargeBedAutoBillMatrix
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ChargeBedAutoBillMatrixGet(string AccessKey, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ChargeBedAutoBillMatrixQuery();
                query.Where(query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ChargeBedAutoBillMatrix

        #region ChartOfAccountBalances
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ChartOfAccountBalancesGet(string AccessKey, Int32 BalanceId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ChartOfAccountBalancesQuery();
                query.Where(query.BalanceId == BalanceId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ChartOfAccountBalances

        #region ChartOfAccounts
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ChartOfAccountsGet(string AccessKey, Int32 ChartOfAccountId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ChartOfAccountsQuery();
                query.Where(query.ChartOfAccountId == ChartOfAccountId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ChartOfAccounts

        #region CheckinConfirmHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CheckinConfirmHistoryGet(string AccessKey, Guid CheckinConfirmId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new CheckinConfirmHistoryQuery();
                query.Where(query.CheckinConfirmId == CheckinConfirmId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion CheckinConfirmHistory

        #region Class
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClassGet(string AccessKey, String ClassID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ClassQuery();
                query.Where(query.ClassID == ClassID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Class

        #region ClassBridging
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClassBridgingGet(string AccessKey, String ClassID, String SRBridgingType)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ClassBridgingQuery();
                query.Where(query.ClassID == ClassID, query.SRBridgingType == SRBridgingType);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ClassBridging

        #region ClinicalExamResults
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClinicalExamResultsGet(string AccessKey, String RegistrationNo, String ParamedicID, String Title)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ClinicalExamResultsQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.ParamedicID == ParamedicID, query.Title == Title);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ClinicalExamResults

        #region ClinicalPathway
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClinicalPathwayGet(string AccessKey, String RegistrationNo, String PathwayID, Int32 PathwayItemSeqNo, Int32 DayNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ClinicalPathwayQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.PathwayID == PathwayID, query.PathwayItemSeqNo == PathwayItemSeqNo, query.DayNo == DayNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ClinicalPathway

        #region ClosingAccounting
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClosingAccountingGet(string AccessKey, String YearNo, String MonthNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ClosingAccountingQuery();
                query.Where(query.YearNo == YearNo, query.MonthNo == MonthNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ClosingAccounting

        #region ClosingWageTransaction
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClosingWageTransactionGet(string AccessKey, Int32 PayrollPeriodID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ClosingWageTransactionQuery();
                query.Where(query.PayrollPeriodID == PayrollPeriodID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ClosingWageTransaction

        #region CompanyEducationProfile
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CompanyEducationProfileGet(string AccessKey, Int32 CompanyEducationProfileID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new CompanyEducationProfileQuery();
                query.Where(query.CompanyEducationProfileID == CompanyEducationProfileID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion CompanyEducationProfile

        #region CompanyFieldOfWorkProfile
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CompanyFieldOfWorkProfileGet(string AccessKey, Int32 CompanyFieldOfWorkProfileID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new CompanyFieldOfWorkProfileQuery();
                query.Where(query.CompanyFieldOfWorkProfileID == CompanyFieldOfWorkProfileID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion CompanyFieldOfWorkProfile

        #region CompanyLaborProfile
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CompanyLaborProfileGet(string AccessKey, Int32 CompanyLaborProfileID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new CompanyLaborProfileQuery();
                query.Where(query.CompanyLaborProfileID == CompanyLaborProfileID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion CompanyLaborProfile

        #region ConsumeMethod
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ConsumeMethodGet(string AccessKey, String SRConsumeMethod)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ConsumeMethodQuery();
                query.Where(query.SRConsumeMethod == SRConsumeMethod);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ConsumeMethod

        #region ContributoryFactorsClassificationFramework
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ContributoryFactorsClassificationFrameworkGet(string AccessKey, String FactorID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ContributoryFactorsClassificationFrameworkQuery();
                query.Where(query.FactorID == FactorID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ContributoryFactorsClassificationFramework

        #region ContributoryFactorsClassificationFrameworkItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ContributoryFactorsClassificationFrameworkItemGet(string AccessKey, String FactorID, String FactorItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ContributoryFactorsClassificationFrameworkItemQuery();
                query.Where(query.FactorID == FactorID, query.FactorItemID == FactorItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ContributoryFactorsClassificationFrameworkItem

        #region ContributoryFactorsClassificationFrameworkItemComponent
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ContributoryFactorsClassificationFrameworkItemComponentGet(string AccessKey, String FactorID, String FactorItemID, String ComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ContributoryFactorsClassificationFrameworkItemComponentQuery();
                query.Where(query.FactorID == FactorID, query.FactorItemID == FactorItemID, query.ComponentID == ComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ContributoryFactorsClassificationFrameworkItemComponent

        #region CostCalculation
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CostCalculationGet(string AccessKey, String RegistrationNo, String TransactionNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new CostCalculationQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion CostCalculation

        #region CostCalculationBuffer
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CostCalculationBufferGet(string AccessKey, String RegistrationNo, String GuarantorID, String TransactionNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new CostCalculationBufferQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.GuarantorID == GuarantorID, query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion CostCalculationBuffer

        #region CostCalculationHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CostCalculationHistoryGet(string AccessKey, String RecalculationProcessNo, String RegistrationNo, String TransactionNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new CostCalculationHistoryQuery();
                query.Where(query.RecalculationProcessNo == RecalculationProcessNo, query.RegistrationNo == RegistrationNo, query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion CostCalculationHistory

        #region CostCalculationIntermBillTemp
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CostCalculationIntermBillTempGet(string AccessKey, String RegistrationNo, String TransactionNo, String SequenceNo, String IntermBillNo, String PaymentNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new CostCalculationIntermBillTempQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo, query.IntermBillNo == IntermBillNo, query.PaymentNo == PaymentNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion CostCalculationIntermBillTemp

        #region CostCalculationTemp
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CostCalculationTempGet(string AccessKey, String RegistrationNo, String TransactionNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new CostCalculationTempQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion CostCalculationTemp

        #region CurrencyRate
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CurrencyRateGet(string AccessKey, String CurrencyID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new CurrencyRateQuery();
                query.Where(query.CurrencyID == CurrencyID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion CurrencyRate

        #region Customer
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CustomerGet(string AccessKey, String CustomerID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new CustomerQuery();
                query.Where(query.CustomerID == CustomerID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Customer

        #region DataRptItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DataRptItemGet(string AccessKey, String SRDataRpt, String ItemID, DateTime TransactionDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DataRptItemQuery();
                query.Where(query.SRDataRpt == SRDataRpt, query.ItemID == ItemID, query.TransactionDate == TransactionDate);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion DataRptItem

        #region DataRptMaster
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DataRptMasterGet(string AccessKey, String SRDataRpt, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DataRptMasterQuery();
                query.Where(query.SRDataRpt == SRDataRpt, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion DataRptMaster

        #region Department
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DepartmentGet(string AccessKey, String DepartmentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DepartmentQuery();
                query.Where(query.DepartmentID == DepartmentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Department

        #region DhfPatientLaboratoryResults
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DhfPatientLaboratoryResultsGet(string AccessKey, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DhfPatientLaboratoryResultsQuery();
                query.Where(query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion DhfPatientLaboratoryResults

        #region Diagnose
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DiagnoseGet(string AccessKey, String DiagnoseID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DiagnoseQuery();
                query.Where(query.DiagnoseID == DiagnoseID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Diagnose

        #region Diet
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DietGet(string AccessKey, String DietID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DietQuery();
                query.Where(query.DietID == DietID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Diet

        #region DietComplication
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DietComplicationGet(string AccessKey, String DietID, String DietComplicationID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DietComplicationQuery();
                query.Where(query.DietID == DietID, query.DietComplicationID == DietComplicationID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion DietComplication

        #region DietComplicationPatient
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DietComplicationPatientGet(string AccessKey, String TransactionNo, String DietID, String DietComplicationID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DietComplicationPatientQuery();
                query.Where(query.TransactionNo == TransactionNo, query.DietID == DietID, query.DietComplicationID == DietComplicationID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion DietComplicationPatient

        #region DietLiquidPatient
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DietLiquidPatientGet(string AccessKey, String TransactionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DietLiquidPatientQuery();
                query.Where(query.TransactionNo == TransactionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion DietLiquidPatient

        #region DietLiquidPatientItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DietLiquidPatientItemGet(string AccessKey, String TransactionNo, String DietTime, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DietLiquidPatientItemQuery();
                query.Where(query.TransactionNo == TransactionNo, query.DietTime == DietTime, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion DietLiquidPatientItem

        #region DietLiquidPatientTime
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DietLiquidPatientTimeGet(string AccessKey, String TransactionNo, String DietTime)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DietLiquidPatientTimeQuery();
                query.Where(query.TransactionNo == TransactionNo, query.DietTime == DietTime);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion DietLiquidPatientTime

        #region DietPatient
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DietPatientGet(string AccessKey, String TransactionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DietPatientQuery();
                query.Where(query.TransactionNo == TransactionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion DietPatient

        #region DietPatientItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DietPatientItemGet(string AccessKey, String TransactionNo, String DietID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DietPatientItemQuery();
                query.Where(query.TransactionNo == TransactionNo, query.DietID == DietID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion DietPatientItem

        #region DistributionPortion
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DistributionPortionGet(string AccessKey, String OrderNo, String SRMealSet)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DistributionPortionQuery();
                query.Where(query.OrderNo == OrderNo, query.SRMealSet == SRMealSet);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion DistributionPortion

        #region DocumentDefinition
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DocumentDefinitionGet(string AccessKey, Int32 DocumentDefinitionID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DocumentDefinitionQuery();
                query.Where(query.DocumentDefinitionID == DocumentDefinitionID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion DocumentDefinition

        #region DocumentDefinitionItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DocumentDefinitionItemGet(string AccessKey, Int32 DocumentDefinitionID, Int32 DocumentFilesID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DocumentDefinitionItemQuery();
                query.Where(query.DocumentDefinitionID == DocumentDefinitionID, query.DocumentFilesID == DocumentFilesID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion DocumentDefinitionItem

        #region DocumentFiles
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DocumentFilesGet(string AccessKey, Int32 DocumentFilesID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DocumentFilesQuery();
                query.Where(query.DocumentFilesID == DocumentFilesID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion DocumentFiles

        #region DocumentSignature
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DocumentSignatureGet(string AccessKey, String TransactionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DocumentSignatureQuery();
                query.Where(query.TransactionNo == TransactionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion DocumentSignature

        #region DocumentSignatureItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DocumentSignatureItemGet(string AccessKey, String TransactionNo, Decimal MinAmount)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DocumentSignatureItemQuery();
                query.Where(query.TransactionNo == TransactionNo, query.MinAmount == MinAmount);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion DocumentSignatureItem

        #region Donator
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DonatorGet(string AccessKey, String DonatorID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DonatorQuery();
                query.Where(query.DonatorID == DonatorID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Donator

        #region Dtd
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DtdGet(string AccessKey, String DtdNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new DtdQuery();
                query.Where(query.DtdNo == DtdNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Dtd

        #region EDCMachine
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EDCMachineGet(string AccessKey, String EDCMachineID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EDCMachineQuery();
                query.Where(query.EDCMachineID == EDCMachineID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EDCMachine

        #region EDCMachineTariff
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EDCMachineTariffGet(string AccessKey, String EDCMachineID, String SRCardType)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EDCMachineTariffQuery();
                query.Where(query.EDCMachineID == EDCMachineID, query.SRCardType == SRCardType);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EDCMachineTariff

        #region Embalace
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmbalaceGet(string AccessKey, String EmbalaceID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmbalaceQuery();
                query.Where(query.EmbalaceID == EmbalaceID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Embalace

        #region EmergencyContact
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmergencyContactGet(string AccessKey, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmergencyContactQuery();
                query.Where(query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmergencyContact

        #region EmergencyDiagnose
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmergencyDiagnoseGet(string AccessKey, String EmrDiagnoseID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmergencyDiagnoseQuery();
                query.Where(query.EmrDiagnoseID == EmrDiagnoseID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmergencyDiagnose

        #region Employee
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeGet(string AccessKey, String EmployeeID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeQuery();
                query.Where(query.EmployeeID == EmployeeID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Employee

        #region EmployeeAchievement
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeAchievementGet(string AccessKey, Int32 EmployeeAchievementID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeAchievementQuery();
                query.Where(query.EmployeeAchievementID == EmployeeAchievementID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeAchievement

        #region EmployeeDisciplinary
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeDisciplinaryGet(string AccessKey, Int32 EmployeeDisciplinaryID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeDisciplinaryQuery();
                query.Where(query.EmployeeDisciplinaryID == EmployeeDisciplinaryID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeDisciplinary

        #region EmployeeEmploymentPeriod
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeEmploymentPeriodGet(string AccessKey, Int32 EmployeeEmploymentPeriodID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeEmploymentPeriodQuery();
                query.Where(query.EmployeeEmploymentPeriodID == EmployeeEmploymentPeriodID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeEmploymentPeriod

        #region EmployeeGrade
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeGradeGet(string AccessKey, Int32 EmployeeGradeID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeGradeQuery();
                query.Where(query.EmployeeGradeID == EmployeeGradeID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeGrade

        #region EmployeeGradeMaster
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeGradeMasterGet(string AccessKey, Int32 EmployeeGradeMasterID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeGradeMasterQuery();
                query.Where(query.EmployeeGradeMasterID == EmployeeGradeMasterID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeGradeMaster

        #region EmployeeHealthAndSafety
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeHealthAndSafetyGet(string AccessKey, String EmployeeHealthAndSafetyNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeHealthAndSafetyQuery();
                query.Where(query.EmployeeHealthAndSafetyNo == EmployeeHealthAndSafetyNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeHealthAndSafety

        #region EmployeeLanguageProficiency
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeLanguageProficiencyGet(string AccessKey, Int32 EmployeeLanguageProficiencyID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeLanguageProficiencyQuery();
                query.Where(query.EmployeeLanguageProficiencyID == EmployeeLanguageProficiencyID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeLanguageProficiency

        #region EmployeeLeave
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeLeaveGet(string AccessKey, Int64 EmployeeLeaveID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeLeaveQuery();
                query.Where(query.EmployeeLeaveID == EmployeeLeaveID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeLeave

        #region EmployeeLeaveCashable
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeLeaveCashableGet(string AccessKey, Int32 EmployeeLeaveCashableID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeLeaveCashableQuery();
                query.Where(query.EmployeeLeaveCashableID == EmployeeLeaveCashableID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeLeaveCashable

        #region EmployeeLeaveRequest
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeLeaveRequestGet(string AccessKey, Int64 LeaveRequestID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeLeaveRequestQuery();
                query.Where(query.LeaveRequestID == LeaveRequestID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeLeaveRequest

        #region EmployeeLoan
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeLoanGet(string AccessKey, Int32 EmployeeLoanID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeLoanQuery();
                query.Where(query.EmployeeLoanID == EmployeeLoanID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeLoan

        #region EmployeeLoanDetail
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeLoanDetailGet(string AccessKey, Int32 EmployeeLoanDetailID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeLoanDetailQuery();
                query.Where(query.EmployeeLoanDetailID == EmployeeLoanDetailID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeLoanDetail

        #region EmployeeLoanItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeLoanItemGet(string AccessKey, Int32 EmployeeLoanDetailID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeLoanItemQuery();
                query.Where(query.EmployeeLoanDetailID == EmployeeLoanDetailID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeLoanItem

        #region EmployeeMedicalAdjustment
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeMedicalAdjustmentGet(string AccessKey, Int32 EmployeeMedicalAdjustmentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeMedicalAdjustmentQuery();
                query.Where(query.EmployeeMedicalAdjustmentID == EmployeeMedicalAdjustmentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeMedicalAdjustment

        #region EmployeeMedicalBenefit
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeMedicalBenefitGet(string AccessKey, Int64 EmployeeMedicalBenefitID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeMedicalBenefitQuery();
                query.Where(query.EmployeeMedicalBenefitID == EmployeeMedicalBenefitID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeMedicalBenefit

        #region EmployeeMiscellaneousBenefit
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeMiscellaneousBenefitGet(string AccessKey, Int32 EmployeeMiscellaneousBenefitID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeMiscellaneousBenefitQuery();
                query.Where(query.EmployeeMiscellaneousBenefitID == EmployeeMiscellaneousBenefitID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeMiscellaneousBenefit

        #region EmployeeOrganization
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeOrganizationGet(string AccessKey, Int32 EmployeeOrganizationID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeOrganizationQuery();
                query.Where(query.EmployeeOrganizationID == EmployeeOrganizationID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeOrganization

        #region EmployeeOvertime
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeOvertimeGet(string AccessKey, String TransactionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeOvertimeQuery();
                query.Where(query.TransactionNo == TransactionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeOvertime

        #region EmployeeOvertimeItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeOvertimeItemGet(string AccessKey, String TransactionNo, Int32 PersonID, Int32 SalaryComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeOvertimeItemQuery();
                query.Where(query.TransactionNo == TransactionNo, query.PersonID == PersonID, query.SalaryComponentID == SalaryComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeOvertimeItem

        #region EmployeePeriodicSalary
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeePeriodicSalaryGet(string AccessKey, Int32 EmployeePeriodicSalaryID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeePeriodicSalaryQuery();
                query.Where(query.EmployeePeriodicSalaryID == EmployeePeriodicSalaryID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeePeriodicSalary

        #region EmployeePeriodicStructuralBenefits
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeePeriodicStructuralBenefitsGet(string AccessKey, Int32 CounterID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeePeriodicStructuralBenefitsQuery();
                query.Where(query.CounterID == CounterID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeePeriodicStructuralBenefits

        #region EmployeePosition
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeePositionGet(string AccessKey, Int32 EmployeePositionID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeePositionQuery();
                query.Where(query.EmployeePositionID == EmployeePositionID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeePosition

        #region EmployeePositionGrade
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeePositionGradeGet(string AccessKey, Int64 EmployeePositionGradeID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeePositionGradeQuery();
                query.Where(query.EmployeePositionGradeID == EmployeePositionGradeID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeePositionGrade

        #region EmployeeRL4
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeRL4Get(string AccessKey, Int32 EmployeeRL4ID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeRL4Query();
                query.Where(query.EmployeeRL4ID == EmployeeRL4ID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeRL4

        #region EmployeeSalaryInfo
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeSalaryInfoGet(string AccessKey, Int32 PersonID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeSalaryInfoQuery();
                query.Where(query.PersonID == PersonID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeSalaryInfo

        #region EmployeeSalaryMatrix
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeSalaryMatrixGet(string AccessKey, Int64 EmployeeSalaryMatrixID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeSalaryMatrixQuery();
                query.Where(query.EmployeeSalaryMatrixID == EmployeeSalaryMatrixID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeSalaryMatrix

        #region EmployeeTraining
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeTrainingGet(string AccessKey, Int32 EmployeeTrainingID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeTrainingQuery();
                query.Where(query.EmployeeTrainingID == EmployeeTrainingID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeTraining

        #region EmployeeTrainingHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeTrainingHistoryGet(string AccessKey, Int32 EmployeeTrainingHistoryID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeTrainingHistoryQuery();
                query.Where(query.EmployeeTrainingHistoryID == EmployeeTrainingHistoryID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeTrainingHistory

        #region EmployeeWorkingInfo
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EmployeeWorkingInfoGet(string AccessKey, Int32 PersonID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EmployeeWorkingInfoQuery();
                query.Where(query.PersonID == PersonID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EmployeeWorkingInfo

        #region EpisodeBodyDiagram
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EpisodeBodyDiagramGet(string AccessKey, String RegistrationNo, Int32 SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EpisodeBodyDiagramQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EpisodeBodyDiagram

        #region EpisodeDiagnose
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EpisodeDiagnoseGet(string AccessKey, String RegistrationNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EpisodeDiagnoseQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EpisodeDiagnose

        #region EpisodeProcedure
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EpisodeProcedureGet(string AccessKey, String RegistrationNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EpisodeProcedureQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EpisodeProcedure

        #region EpisodeSOAPE
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EpisodeSOAPEGet(string AccessKey, String RegistrationNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EpisodeSOAPEQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EpisodeSOAPE

        #region EventMealOrder
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EventMealOrderGet(string AccessKey, String OrderNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EventMealOrderQuery();
                query.Where(query.OrderNo == OrderNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EventMealOrder

        #region EventMealOrderItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EventMealOrderItemGet(string AccessKey, String OrderNo, String FoodID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new EventMealOrderItemQuery();
                query.Where(query.OrderNo == OrderNo, query.FoodID == FoodID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion EventMealOrderItem

        #region ExamSummary
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ExamSummaryGet(string AccessKey, String ExamSummaryID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ExamSummaryQuery();
                query.Where(query.ExamSummaryID == ExamSummaryID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ExamSummary

        #region ExamSummaryResult
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ExamSummaryResultGet(string AccessKey, String RegistrationNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ExamSummaryResultQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ExamSummaryResult

        #region Fabric
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void FabricGet(string AccessKey, String FabricID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new FabricQuery();
                query.Where(query.FabricID == FabricID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Fabric

        #region FamilyMedicalHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void FamilyMedicalHistoryGet(string AccessKey, String PatientID, String SRMedicalDisease)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new FamilyMedicalHistoryQuery();
                query.Where(query.PatientID == PatientID, query.SRMedicalDisease == SRMedicalDisease);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion FamilyMedicalHistory

        #region Food
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void FoodGet(string AccessKey, String FoodID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new FoodQuery();
                query.Where(query.FoodID == FoodID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Food

        #region FoodItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void FoodItemGet(string AccessKey, String FoodID, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new FoodItemQuery();
                query.Where(query.FoodID == FoodID, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion FoodItem

        #region Guarantor
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GuarantorGet(string AccessKey, String GuarantorID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new GuarantorQuery();
                query.Where(query.GuarantorID == GuarantorID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Guarantor

        #region GuarantorBridging
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GuarantorBridgingGet(string AccessKey, String GuarantorID, String SRBridgingType)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new GuarantorBridgingQuery();
                query.Where(query.GuarantorID == GuarantorID, query.SRBridgingType == SRBridgingType);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion GuarantorBridging

        #region GuarantorDeposit
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GuarantorDepositGet(string AccessKey, String TransactionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new GuarantorDepositQuery();
                query.Where(query.TransactionNo == TransactionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion GuarantorDeposit

        #region GuarantorDepositBalance
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GuarantorDepositBalanceGet(string AccessKey, String GuarantorID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new GuarantorDepositBalanceQuery();
                query.Where(query.GuarantorID == GuarantorID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion GuarantorDepositBalance

        #region GuarantorDepositMovement
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GuarantorDepositMovementGet(string AccessKey, Guid MovementID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new GuarantorDepositMovementQuery();
                query.Where(query.MovementID == MovementID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion GuarantorDepositMovement

        #region GuarantorInfo
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GuarantorInfoGet(string AccessKey, String GuarantorInfoID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new GuarantorInfoQuery();
                query.Where(query.GuarantorInfoID == GuarantorInfoID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion GuarantorInfo

        #region GuarantorInfoSummary
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GuarantorInfoSummaryGet(string AccessKey, String GuarantorID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new GuarantorInfoSummaryQuery();
                query.Where(query.GuarantorID == GuarantorID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion GuarantorInfoSummary

        #region GuarantorItemPrescriptionRule
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GuarantorItemPrescriptionRuleGet(string AccessKey, String GuarantorID, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new GuarantorItemPrescriptionRuleQuery();
                query.Where(query.GuarantorID == GuarantorID, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion GuarantorItemPrescriptionRule

        #region GuarantorItemRule
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GuarantorItemRuleGet(string AccessKey, String GuarantorID, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new GuarantorItemRuleQuery();
                query.Where(query.GuarantorID == GuarantorID, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion GuarantorItemRule

        #region GuarantorItemRuleTariffComponent
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GuarantorItemRuleTariffComponentGet(string AccessKey, String GuarantorID, String ItemID, String TariffComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new GuarantorItemRuleTariffComponentQuery();
                query.Where(query.GuarantorID == GuarantorID, query.ItemID == ItemID, query.TariffComponentID == TariffComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion GuarantorItemRuleTariffComponent

        #region GuarantorItemTypeRule
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GuarantorItemTypeRuleGet(string AccessKey, String GuarantorID, String SRItemType)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new GuarantorItemTypeRuleQuery();
                query.Where(query.GuarantorID == GuarantorID, query.SRItemType == SRItemType);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion GuarantorItemTypeRule

        #region GuarantorServiceUnitRule
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GuarantorServiceUnitRuleGet(string AccessKey, String GuarantorID, String ServiceUnitID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new GuarantorServiceUnitRuleQuery();
                query.Where(query.GuarantorID == GuarantorID, query.ServiceUnitID == ServiceUnitID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion GuarantorServiceUnitRule

        #region GuarantorSurgicalPackageCovered
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GuarantorSurgicalPackageCoveredGet(string AccessKey, String GuarantorID, String PackageID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new GuarantorSurgicalPackageCoveredQuery();
                query.Where(query.GuarantorID == GuarantorID, query.PackageID == PackageID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion GuarantorSurgicalPackageCovered

        #region GuarantorSurgicalPackageCoveredItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GuarantorSurgicalPackageCoveredItemGet(string AccessKey, String GuarantorID, String PackageID, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new GuarantorSurgicalPackageCoveredItemQuery();
                query.Where(query.GuarantorID == GuarantorID, query.PackageID == PackageID, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion GuarantorSurgicalPackageCoveredItem

        #region Healthcare
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void HealthcareGet(string AccessKey, String HealthcareID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new HealthcareQuery();
                query.Where(query.HealthcareID == HealthcareID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Healthcare

        #region HealthRecord
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void HealthRecordGet(string AccessKey, String PatientID, String QuestionFormID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new HealthRecordQuery();
                query.Where(query.PatientID == PatientID, query.QuestionFormID == QuestionFormID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion HealthRecord

        #region HealthRecordLine
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void HealthRecordLineGet(string AccessKey, String PatientID, String QuestionFormID, String QuestionGroupID, String QuestionID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new HealthRecordLineQuery();
                query.Where(query.PatientID == PatientID, query.QuestionFormID == QuestionFormID, query.QuestionGroupID == QuestionGroupID, query.QuestionID == QuestionID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion HealthRecordLine

        #region HL7Message
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void HL7MessageGet(string AccessKey, Guid MessageID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new HL7MessageQuery();
                query.Where(query.MessageID == MessageID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion HL7Message

        #region HolidaySchedule
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void HolidayScheduleGet(string AccessKey, String PeriodYear, DateTime HolidayDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new HolidayScheduleQuery();
                query.Where(query.PeriodYear == PeriodYear, query.HolidayDate == HolidayDate);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion HolidaySchedule

        #region HospitalInfo
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void HospitalInfoGet(string AccessKey, Int32 HospitalInfoID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new HospitalInfoQuery();
                query.Where(query.HospitalInfoID == HospitalInfoID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion HospitalInfo

        #region HumanBasePeriod
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void HumanBasePeriodGet(string AccessKey, Int32 HumanBasePeriodID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new HumanBasePeriodQuery();
                query.Where(query.HumanBasePeriodID == HumanBasePeriodID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion HumanBasePeriod

        #region ImageTemplate
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ImageTemplateGet(string AccessKey, String ImageTemplateID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ImageTemplateQuery();
                query.Where(query.ImageTemplateID == ImageTemplateID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ImageTemplate

        #region Immunization
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ImmunizationGet(string AccessKey, String ImmunizationID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ImmunizationQuery();
                query.Where(query.ImmunizationID == ImmunizationID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Immunization

        #region IncidentType
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void IncidentTypeGet(string AccessKey, String SRIncidentType, String ComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new IncidentTypeQuery();
                query.Where(query.SRIncidentType == SRIncidentType, query.ComponentID == ComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion IncidentType

        #region IncidentTypeItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void IncidentTypeItemGet(string AccessKey, String SRIncidentType, String ComponentID, String SubComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new IncidentTypeItemQuery();
                query.Where(query.SRIncidentType == SRIncidentType, query.ComponentID == ComponentID, query.SubComponentID == SubComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion IncidentTypeItem

        #region IncomeJournalStatus
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void IncomeJournalStatusGet(string AccessKey, Int32 Id)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new IncomeJournalStatusQuery();
                query.Where(query.Id == Id);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion IncomeJournalStatus

        #region Indication
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void IndicationGet(string AccessKey, String IndicationID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new IndicationQuery();
                query.Where(query.IndicationID == IndicationID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Indication

        #region InhealthSJP
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void InhealthSJPGet(string AccessKey, String nosjp)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new InhealthSJPQuery();
                query.Where(query.Nosjp == nosjp);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion InhealthSJP

        #region InitialGL
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void InitialGLGet(string AccessKey, String YearNo, String MonthNo, String AccountID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new InitialGLQuery();
                query.Where(query.YearNo == YearNo, query.MonthNo == MonthNo, query.AccountID == AccountID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion InitialGL

        #region InitialGLItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void InitialGLItemGet(string AccessKey, String YearNo, String MonthNo, String AccountID, String SRAcctSubsidiary, String SubsidiaryID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new InitialGLItemQuery();
                query.Where(query.YearNo == YearNo, query.MonthNo == MonthNo, query.AccountID == AccountID, query.SRAcctSubsidiary == SRAcctSubsidiary, query.SubsidiaryID == SubsidiaryID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion InitialGLItem

        #region InitialLeaveType
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void InitialLeaveTypeGet(string AccessKey, Int32 InitialLeaveTypeID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new InitialLeaveTypeQuery();
                query.Where(query.InitialLeaveTypeID == InitialLeaveTypeID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion InitialLeaveType

        #region IntermBill
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void IntermBillGet(string AccessKey, String IntermBillNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new IntermBillQuery();
                query.Where(query.IntermBillNo == IntermBillNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion IntermBill

        #region InvoiceAdjusment
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void InvoiceAdjusmentGet(string AccessKey, String TransactionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new InvoiceAdjusmentQuery();
                query.Where(query.TransactionNo == TransactionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion InvoiceAdjusment

        #region Invoices
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void InvoicesGet(string AccessKey, String InvoiceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new InvoicesQuery();
                query.Where(query.InvoiceNo == InvoiceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Invoices

        #region InvoicesItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void InvoicesItemGet(string AccessKey, String InvoiceNo, String PaymentNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new InvoicesItemQuery();
                query.Where(query.InvoiceNo == InvoiceNo, query.PaymentNo == PaymentNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion InvoicesItem

        #region InvoiceSupplier
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void InvoiceSupplierGet(string AccessKey, String InvoiceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new InvoiceSupplierQuery();
                query.Where(query.InvoiceNo == InvoiceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion InvoiceSupplier

        #region InvoiceSupplierItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void InvoiceSupplierItemGet(string AccessKey, String InvoiceNo, String TransactionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new InvoiceSupplierItemQuery();
                query.Where(query.InvoiceNo == InvoiceNo, query.TransactionNo == TransactionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion InvoiceSupplierItem

        #region InvoiceSupplierItemConsignment
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void InvoiceSupplierItemConsignmentGet(string AccessKey, String InvoiceNo, String TransactionNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new InvoiceSupplierItemConsignmentQuery();
                query.Where(query.InvoiceNo == InvoiceNo, query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion InvoiceSupplierItemConsignment

        #region Item
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemGet(string AccessKey, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemQuery();
                query.Where(query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Item

        #region ItemBalance
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemBalanceGet(string AccessKey, String LocationID, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemBalanceQuery();
                query.Where(query.LocationID == LocationID, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemBalance

        #region ItemBalanceByPeriod
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemBalanceByPeriodGet(string AccessKey, String LocationID, Int32 PeriodYear, Int32 PeriodMonth, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemBalanceByPeriodQuery();
                query.Where(query.LocationID == LocationID, query.PeriodYear == PeriodYear, query.PeriodMonth == PeriodMonth, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemBalanceByPeriod

        #region ItemBalanceByStockGroup
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemBalanceByStockGroupGet(string AccessKey, String SRStockGroup, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemBalanceByStockGroupQuery();
                query.Where(query.SRStockGroup == SRStockGroup, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemBalanceByStockGroup

        #region ItemBalanceDetail
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemBalanceDetailGet(string AccessKey, String LocationID, String ItemID, String ReferenceNo, DateTime BalanceDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemBalanceDetailQuery();
                query.Where(query.LocationID == LocationID, query.ItemID == ItemID, query.ReferenceNo == ReferenceNo, query.BalanceDate == BalanceDate);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemBalanceDetail

        #region ItemBalanceExpire
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemBalanceExpireGet(string AccessKey, String LocationID, String ItemID, DateTime ExpiredDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemBalanceExpireQuery();
                query.Where(query.LocationID == LocationID, query.ItemID == ItemID, query.ExpiredDate == ExpiredDate);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemBalanceExpire

        #region ItemBridging
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemBridgingGet(string AccessKey, String ItemID, String SRBridgingType, String BridgingID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemBridgingQuery();
                query.Where(query.ItemID == ItemID, query.SRBridgingType == SRBridgingType, query.BridgingID == BridgingID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemBridging

        #region ItemConditionRule
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemConditionRuleGet(string AccessKey, String ItemConditionRuleID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemConditionRuleQuery();
                query.Where(query.ItemConditionRuleID == ItemConditionRuleID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemConditionRule

        #region ItemConditionRuleServiceUnit
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemConditionRuleServiceUnitGet(string AccessKey, String ItemConditionRuleID, String ServiceUnitID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemConditionRuleServiceUnitQuery();
                query.Where(query.ItemConditionRuleID == ItemConditionRuleID, query.ServiceUnitID == ServiceUnitID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemConditionRuleServiceUnit

        #region ItemConsumption
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemConsumptionGet(string AccessKey, String ItemID, String DetailItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemConsumptionQuery();
                query.Where(query.ItemID == ItemID, query.DetailItemID == DetailItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemConsumption

        #region ItemDiagnostic
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemDiagnosticGet(string AccessKey, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemDiagnosticQuery();
                query.Where(query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemDiagnostic

        #region ItemGroup
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemGroupGet(string AccessKey, String ItemGroupID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemGroupQuery();
                query.Where(query.ItemGroupID == ItemGroupID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemGroup

        #region ItemKitchen
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemKitchenGet(string AccessKey, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemKitchenQuery();
                query.Where(query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemKitchen

        #region ItemLaboratory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemLaboratoryGet(string AccessKey, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemLaboratoryQuery();
                query.Where(query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemLaboratory

        #region ItemLaboratoryDetail
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemLaboratoryDetailGet(string AccessKey, String ItemID, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemLaboratoryDetailQuery();
                query.Where(query.ItemID == ItemID, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemLaboratoryDetail

        #region ItemLaboratoryProfile
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemLaboratoryProfileGet(string AccessKey, String ParentItemID, String DetailItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemLaboratoryProfileQuery();
                query.Where(query.ParentItemID == ParentItemID, query.DetailItemID == DetailItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemLaboratoryProfile

        #region ItemMovement
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemMovementGet(string AccessKey, Guid MovementID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemMovementQuery();
                query.Where(query.MovementID == MovementID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemMovement

        #region ItemMovementPerDate
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemMovementPerDateGet(string AccessKey, DateTime MovementDate, String LocationID, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemMovementPerDateQuery();
                query.Where(query.MovementDate == MovementDate, query.LocationID == LocationID, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemMovementPerDate

        #region ItemPackage
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemPackageGet(string AccessKey, String ItemID, String DetailItemID, String ServiceUnitID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemPackageQuery();
                query.Where(query.ItemID == ItemID, query.DetailItemID == DetailItemID, query.ServiceUnitID == ServiceUnitID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemPackage

        #region ItemPackageTariffComponent
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemPackageTariffComponentGet(string AccessKey, String ItemID, String DetailItemID, String TariffComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemPackageTariffComponentQuery();
                query.Where(query.ItemID == ItemID, query.DetailItemID == DetailItemID, query.TariffComponentID == TariffComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemPackageTariffComponent

        #region ItemProductConsumeUnitMatrix
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemProductConsumeUnitMatrixGet(string AccessKey, String ItemID, String SRItemUnit, String SRConsumeUnit)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemProductConsumeUnitMatrixQuery();
                query.Where(query.ItemID == ItemID, query.SRItemUnit == SRItemUnit, query.SRConsumeUnit == SRConsumeUnit);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemProductConsumeUnitMatrix

        #region ItemProductDeductionDetail
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemProductDeductionDetailGet(string AccessKey, String DeductionID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemProductDeductionDetailQuery();
                query.Where(query.DeductionID == DeductionID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemProductDeductionDetail

        #region ItemProductDosageDetail
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemProductDosageDetailGet(string AccessKey, String ItemID, String SRDosageUnit)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemProductDosageDetailQuery();
                query.Where(query.ItemID == ItemID, query.SRDosageUnit == SRDosageUnit);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemProductDosageDetail

        #region ItemProductLog
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemProductLogGet(string AccessKey, String TariffRequestNo, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemProductLogQuery();
                query.Where(query.TariffRequestNo == TariffRequestNo, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemProductLog

        #region ItemProductMargin
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemProductMarginGet(string AccessKey, String MarginID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemProductMarginQuery();
                query.Where(query.MarginID == MarginID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemProductMargin

        #region ItemProductMarginClassValue
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemProductMarginClassValueGet(string AccessKey, String MarginID, String SequenceNo, String ClassID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemProductMarginClassValueQuery();
                query.Where(query.MarginID == MarginID, query.SequenceNo == SequenceNo, query.ClassID == ClassID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemProductMarginClassValue

        #region ItemProductMarginValue
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemProductMarginValueGet(string AccessKey, String MarginID, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemProductMarginValueQuery();
                query.Where(query.MarginID == MarginID, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemProductMarginValue

        #region ItemProductMedic
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemProductMedicGet(string AccessKey, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemProductMedicQuery();
                query.Where(query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemProductMedic

        #region ItemProductMedicIndication
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemProductMedicIndicationGet(string AccessKey, String ItemID, String IndicationID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemProductMedicIndicationQuery();
                query.Where(query.ItemID == ItemID, query.IndicationID == IndicationID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemProductMedicIndication

        #region ItemProductMedicLabel
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemProductMedicLabelGet(string AccessKey, String ItemID, String LabelID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemProductMedicLabelQuery();
                query.Where(query.ItemID == ItemID, query.LabelID == LabelID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemProductMedicLabel

        #region ItemProductMedicMarginDetail
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemProductMedicMarginDetailGet(string AccessKey, String ItemID, String ClassID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemProductMedicMarginDetailQuery();
                query.Where(query.ItemID == ItemID, query.ClassID == ClassID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemProductMedicMarginDetail

        #region ItemProductMedicZatActive
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemProductMedicZatActiveGet(string AccessKey, String ItemID, String ZatActiveID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemProductMedicZatActiveQuery();
                query.Where(query.ItemID == ItemID, query.ZatActiveID == ZatActiveID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemProductMedicZatActive

        #region ItemProductNonMedic
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemProductNonMedicGet(string AccessKey, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemProductNonMedicQuery();
                query.Where(query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemProductNonMedic

        #region ItemProductSalesDiscount
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemProductSalesDiscountGet(string AccessKey, String SalesDiscID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemProductSalesDiscountQuery();
                query.Where(query.SalesDiscID == SalesDiscID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemProductSalesDiscount

        #region ItemRadiology
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemRadiologyGet(string AccessKey, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemRadiologyQuery();
                query.Where(query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemRadiology

        #region ItemSalesPerDate
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemSalesPerDateGet(string AccessKey, DateTime MovementDate, String SRStockGroup, String ItemID, String ServiceUnitID, String LocationID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemSalesPerDateQuery();
                query.Where(query.MovementDate == MovementDate, query.SRStockGroup == SRStockGroup, query.ItemID == ItemID, query.ServiceUnitID == ServiceUnitID, query.LocationID == LocationID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemSalesPerDate

        #region ItemService
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemServiceGet(string AccessKey, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemServiceQuery();
                query.Where(query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemService

        #region ItemServiceSubSpecialty
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemServiceSubSpecialtyGet(string AccessKey, String ItemID, String SubSpecialtyID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemServiceSubSpecialtyQuery();
                query.Where(query.ItemID == ItemID, query.SubSpecialtyID == SubSpecialtyID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemServiceSubSpecialty

        #region ItemStockOpnameApproval
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemStockOpnameApprovalGet(string AccessKey, String TransactionNo, Int32 PageNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemStockOpnameApprovalQuery();
                query.Where(query.TransactionNo == TransactionNo, query.PageNo == PageNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemStockOpnameApproval

        #region ItemStockOpnamePrevBalance
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemStockOpnamePrevBalanceGet(string AccessKey, String TransactionNo, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemStockOpnamePrevBalanceQuery();
                query.Where(query.TransactionNo == TransactionNo, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemStockOpnamePrevBalance

        #region ItemTariff
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemTariffGet(string AccessKey, String SRTariffType, String ItemID, String ClassID, DateTime StartingDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemTariffQuery();
                query.Where(query.SRTariffType == SRTariffType, query.ItemID == ItemID, query.ClassID == ClassID, query.StartingDate == StartingDate);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemTariff

        #region ItemTariffComponent
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemTariffComponentGet(string AccessKey, String SRTariffType, String ItemID, String ClassID, DateTime StartingDate, String TariffComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemTariffComponentQuery();
                query.Where(query.SRTariffType == SRTariffType, query.ItemID == ItemID, query.ClassID == ClassID, query.StartingDate == StartingDate, query.TariffComponentID == TariffComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemTariffComponent

        #region ItemTariffComponentUpdateHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemTariffComponentUpdateHistoryGet(string AccessKey, String RequestNo, String SRTariffType, String ItemID, String ClassID, DateTime StartingDate, String TariffComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemTariffComponentUpdateHistoryQuery();
                query.Where(query.RequestNo == RequestNo, query.SRTariffType == SRTariffType, query.ItemID == ItemID, query.ClassID == ClassID, query.StartingDate == StartingDate, query.TariffComponentID == TariffComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemTariffComponentUpdateHistory

        #region ItemTariffRequest
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemTariffRequestGet(string AccessKey, String TariffRequestNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemTariffRequestQuery();
                query.Where(query.TariffRequestNo == TariffRequestNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemTariffRequest

        #region ItemTariffRequest2
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemTariffRequest2Get(string AccessKey, String TariffRequestNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemTariffRequest2Query();
                query.Where(query.TariffRequestNo == TariffRequestNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemTariffRequest2

        #region ItemTariffRequest2Item
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemTariffRequest2ItemGet(string AccessKey, String TariffRequestNo, String ItemID, String ClassID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemTariffRequest2ItemQuery();
                query.Where(query.TariffRequestNo == TariffRequestNo, query.ItemID == ItemID, query.ClassID == ClassID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemTariffRequest2Item

        #region ItemTariffRequest2ItemComp
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemTariffRequest2ItemCompGet(string AccessKey, String TariffRequestNo, String ItemID, String ClassID, String TariffComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemTariffRequest2ItemCompQuery();
                query.Where(query.TariffRequestNo == TariffRequestNo, query.ItemID == ItemID, query.ClassID == ClassID, query.TariffComponentID == TariffComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemTariffRequest2ItemComp

        #region ItemTariffRequestItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemTariffRequestItemGet(string AccessKey, String TariffRequestNo, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemTariffRequestItemQuery();
                query.Where(query.TariffRequestNo == TariffRequestNo, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemTariffRequestItem

        #region ItemTariffRequestItemComp
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemTariffRequestItemCompGet(string AccessKey, String TariffRequestNo, String ItemID, String TariffComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemTariffRequestItemCompQuery();
                query.Where(query.TariffRequestNo == TariffRequestNo, query.ItemID == ItemID, query.TariffComponentID == TariffComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemTariffRequestItemComp

        #region ItemTariffRequestItemToImport
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemTariffRequestItemToImportGet(string AccessKey, String ReferenceNo, DateTime StartingDate, String SRTariffType, String ItemID, String ClassID, String TariffComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemTariffRequestItemToImportQuery();
                query.Where(query.ReferenceNo == ReferenceNo, query.StartingDate == StartingDate, query.SRTariffType == SRTariffType, query.ItemID == ItemID, query.ClassID == ClassID, query.TariffComponentID == TariffComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemTariffRequestItemToImport

        #region ItemTariffUpdateHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemTariffUpdateHistoryGet(string AccessKey, String RequestNo, String SRTariffType, String ItemID, String ClassID, DateTime StartingDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemTariffUpdateHistoryQuery();
                query.Where(query.RequestNo == RequestNo, query.SRTariffType == SRTariffType, query.ItemID == ItemID, query.ClassID == ClassID, query.StartingDate == StartingDate);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemTariffUpdateHistory

        #region ItemTransaction
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemTransactionGet(string AccessKey, String TransactionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemTransactionQuery();
                query.Where(query.TransactionNo == TransactionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemTransaction

        #region ItemTransactionItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemTransactionItemGet(string AccessKey, String TransactionNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemTransactionItemQuery();
                query.Where(query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemTransactionItem

        #region ItemTransactionItemBak
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemTransactionItemBakGet(string AccessKey, String TransactionNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemTransactionItemBakQuery();
                query.Where(query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemTransactionItemBak

        #region ItemTransactionItemEd
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemTransactionItemEdGet(string AccessKey, String TransactionNo, String SequenceNo, DateTime ExpiredDate, String BatchNumber)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemTransactionItemEdQuery();
                query.Where(query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo, query.ExpiredDate == ExpiredDate, query.BatchNumber == BatchNumber);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemTransactionItemEd

        #region ItemTransactionItemHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ItemTransactionItemHistoryGet(string AccessKey, String TransactionNo, String LocationID, String ItemID, String ReferenceNo, DateTime BalanceDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ItemTransactionItemHistoryQuery();
                query.Where(query.TransactionNo == TransactionNo, query.LocationID == LocationID, query.ItemID == ItemID, query.ReferenceNo == ReferenceNo, query.BalanceDate == BalanceDate);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ItemTransactionItemHistory

        #region JobOpportunity
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void JobOpportunityGet(string AccessKey, Int32 JobOpportunityID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new JobOpportunityQuery();
                query.Where(query.JobOpportunityID == JobOpportunityID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion JobOpportunity

        #region JournalCodes
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void JournalCodesGet(string AccessKey, Int32 JournalCodeId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new JournalCodesQuery();
                query.Where(query.JournalCodeId == JournalCodeId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion JournalCodes

        #region JournalGroup
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void JournalGroupGet(string AccessKey, Int32 JournalGroupID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new JournalGroupQuery();
                query.Where(query.JournalGroupID == JournalGroupID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion JournalGroup

        #region JournalGroupDetail
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void JournalGroupDetailGet(string AccessKey, Int32 JournalDetailID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new JournalGroupDetailQuery();
                query.Where(query.JournalDetailID == JournalDetailID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion JournalGroupDetail

        #region JournalGroupUser
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void JournalGroupUserGet(string AccessKey, Int32 JournalUserID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new JournalGroupUserQuery();
                query.Where(query.JournalUserID == JournalUserID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion JournalGroupUser

        #region JournalMessage
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void JournalMessageGet(string AccessKey, Int32 JournalID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new JournalMessageQuery();
                query.Where(query.JournalID == JournalID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion JournalMessage

        #region JournalTransactionDetails
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void JournalTransactionDetailsGet(string AccessKey, Int32 DetailId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new JournalTransactionDetailsQuery();
                query.Where(query.DetailId == DetailId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion JournalTransactionDetails

        #region JournalTransactions
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void JournalTransactionsGet(string AccessKey, Int32 JournalId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new JournalTransactionsQuery();
                query.Where(query.JournalId == JournalId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion JournalTransactions

        #region KioskQueue
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void KioskQueueGet(string AccessKey, Int64 KioskQueueID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new KioskQueueQuery();
                query.Where(query.KioskQueueID == KioskQueueID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion KioskQueue

        #region Labell
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void LabellGet(string AccessKey, String LabelID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new LabellQuery();
                query.Where(query.LabelID == LabelID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Labell

        #region LeaveRequest
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void LeaveRequestGet(string AccessKey, Int32 LeaveRequestID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new LeaveRequestQuery();
                query.Where(query.LeaveRequestID == LeaveRequestID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion LeaveRequest

        #region LiquidFoodDiet
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void LiquidFoodDietGet(string AccessKey, String DietID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new LiquidFoodDietQuery();
                query.Where(query.DietID == DietID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion LiquidFoodDiet

        #region LiquidFoodDietTime
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void LiquidFoodDietTimeGet(string AccessKey, String DietID, String Time)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new LiquidFoodDietTimeQuery();
                query.Where(query.DietID == DietID, query.Time == Time);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion LiquidFoodDietTime

        #region LiquidFoodDietTimeGender
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void LiquidFoodDietTimeGenderGet(string AccessKey, String DietID, String Time, String Gender)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new LiquidFoodDietTimeGenderQuery();
                query.Where(query.DietID == DietID, query.Time == Time, query.Gender == Gender);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion LiquidFoodDietTimeGender

        #region LiquidFoodTime
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void LiquidFoodTimeGet(string AccessKey, String StandardReferenceID, String ItemID, String Time)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new LiquidFoodTimeQuery();
                query.Where(query.StandardReferenceID == StandardReferenceID, query.ItemID == ItemID, query.Time == Time);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion LiquidFoodTime

        #region LiquidFoodTimeGender
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void LiquidFoodTimeGenderGet(string AccessKey, String StandardReferenceID, String ItemID, String Time, String Gender)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new LiquidFoodTimeGenderQuery();
                query.Where(query.StandardReferenceID == StandardReferenceID, query.ItemID == ItemID, query.Time == Time, query.Gender == Gender);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion LiquidFoodTimeGender

        #region Location
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void LocationGet(string AccessKey, String LocationID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new LocationQuery();
                query.Where(query.LocationID == LocationID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Location

        #region MealOrder
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MealOrderGet(string AccessKey, String OrderNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MealOrderQuery();
                query.Where(query.OrderNo == OrderNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MealOrder

        #region MealOrderDateInit
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MealOrderDateInitGet(string AccessKey, DateTime MealOrderDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MealOrderDateInitQuery();
                query.Where(query.MealOrderDate == MealOrderDate);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MealOrderDateInit

        #region MealOrderItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MealOrderItemGet(string AccessKey, String OrderNo, String SRMealSet, String FoodID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MealOrderItemQuery();
                query.Where(query.OrderNo == OrderNo, query.SRMealSet == SRMealSet, query.FoodID == FoodID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MealOrderItem

        #region MealOrderItemLiquid
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MealOrderItemLiquidGet(string AccessKey, String OrderNo, String MealTime)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MealOrderItemLiquidQuery();
                query.Where(query.OrderNo == OrderNo, query.MealTime == MealTime);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MealOrderItemLiquid

        #region MealOrderItemPlan
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MealOrderItemPlanGet(string AccessKey, String OrderNo, String SRMealSet, String FoodID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MealOrderItemPlanQuery();
                query.Where(query.OrderNo == OrderNo, query.SRMealSet == SRMealSet, query.FoodID == FoodID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MealOrderItemPlan

        #region MealOrderItemSetting
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MealOrderItemSettingGet(string AccessKey, String OrderNo, String SRMealSet)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MealOrderItemSettingQuery();
                query.Where(query.OrderNo == OrderNo, query.SRMealSet == SRMealSet);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MealOrderItemSetting

        #region MedicalBenefitClaim
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicalBenefitClaimGet(string AccessKey, Int32 MedicalBenefitClaimID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MedicalBenefitClaimQuery();
                query.Where(query.MedicalBenefitClaimID == MedicalBenefitClaimID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MedicalBenefitClaim

        #region MedicalBenefitClaimItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicalBenefitClaimItemGet(string AccessKey, Int32 MedicalBenefitClaimItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MedicalBenefitClaimItemQuery();
                query.Where(query.MedicalBenefitClaimItemID == MedicalBenefitClaimItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MedicalBenefitClaimItem

        #region MedicalBenefitInfo
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicalBenefitInfoGet(string AccessKey, Int32 MedicalBenefitInfoID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MedicalBenefitInfoQuery();
                query.Where(query.MedicalBenefitInfoID == MedicalBenefitInfoID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MedicalBenefitInfo

        #region MedicalBenefitRuleDefinition
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicalBenefitRuleDefinitionGet(string AccessKey, Int64 MedicalBenefitRuleDefinitionID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MedicalBenefitRuleDefinitionQuery();
                query.Where(query.MedicalBenefitRuleDefinitionID == MedicalBenefitRuleDefinitionID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MedicalBenefitRuleDefinition

        #region MedicalDischargeSummary
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicalDischargeSummaryGet(string AccessKey, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MedicalDischargeSummaryQuery();
                query.Where(query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MedicalDischargeSummary

        #region MedicalEmployeeInitial
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicalEmployeeInitialGet(string AccessKey, Int32 MedicalEmployeeInitialID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MedicalEmployeeInitialQuery();
                query.Where(query.MedicalEmployeeInitialID == MedicalEmployeeInitialID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MedicalEmployeeInitial

        #region MedicalFileStatus
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicalFileStatusGet(string AccessKey, String PatientID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MedicalFileStatusQuery();
                query.Where(query.PatientID == PatientID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MedicalFileStatus

        #region MedicalRecordFileBorrowed
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicalRecordFileBorrowedGet(string AccessKey, String TransactionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MedicalRecordFileBorrowedQuery();
                query.Where(query.TransactionNo == TransactionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MedicalRecordFileBorrowed

        #region MedicalRecordFileStatus
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicalRecordFileStatusGet(string AccessKey, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MedicalRecordFileStatusQuery();
                query.Where(query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MedicalRecordFileStatus

        #region MedicalRecordFileStatusMovement
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicalRecordFileStatusMovementGet(string AccessKey, String RegistrationNo, String LastPositionServiceUnitID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MedicalRecordFileStatusMovementQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.LastPositionServiceUnitID == LastPositionServiceUnitID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MedicalRecordFileStatusMovement

        #region MedicationReceive
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicationReceiveGet(string AccessKey, Int64 MedicationReceiveNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MedicationReceiveQuery();
                query.Where(query.MedicationReceiveNo == MedicationReceiveNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MedicationReceive

        #region MedicationReceiveAppropriate
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicationReceiveAppropriateGet(string AccessKey, Int64 MedicationReceiveNo, String AppropriateType)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MedicationReceiveAppropriateQuery();
                query.Where(query.MedicationReceiveNo == MedicationReceiveNo, query.AppropriateType == AppropriateType);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MedicationReceiveAppropriate

        #region MedicationReceiveFromPatient
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicationReceiveFromPatientGet(string AccessKey, Int64 MedicationReceiveNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MedicationReceiveFromPatientQuery();
                query.Where(query.MedicationReceiveNo == MedicationReceiveNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MedicationReceiveFromPatient

        #region MedicationReceiveStatus
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicationReceiveStatusGet(string AccessKey, Int64 MedicationReceiveNo, DateTime StatusDateTime)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MedicationReceiveStatusQuery();
                query.Where(query.MedicationReceiveNo == MedicationReceiveNo, query.StatusDateTime == StatusDateTime);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MedicationReceiveStatus

        #region MedicationReceiveUsed
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicationReceiveUsedGet(string AccessKey, Int64 MedicationReceiveNo, Int32 SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MedicationReceiveUsedQuery();
                query.Where(query.MedicationReceiveNo == MedicationReceiveNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MedicationReceiveUsed

        #region Menu
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MenuGet(string AccessKey, String MenuID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MenuQuery();
                query.Where(query.MenuID == MenuID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Menu

        #region MenuItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MenuItemGet(string AccessKey, String MenuItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MenuItemQuery();
                query.Where(query.MenuItemID == MenuItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MenuItem

        #region MenuItemExtra
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MenuItemExtraGet(string AccessKey, String SeqNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MenuItemExtraQuery();
                query.Where(query.SeqNo == SeqNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MenuItemExtra

        #region MenuItemExtraFood
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MenuItemExtraFoodGet(string AccessKey, String SeqNo, String SRDayName, String FoodID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MenuItemExtraFoodQuery();
                query.Where(query.SeqNo == SeqNo, query.SRDayName == SRDayName, query.FoodID == FoodID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MenuItemExtraFood

        #region MenuItemFood
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MenuItemFoodGet(string AccessKey, String MenuItemID, String SRMealSet, String FoodID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MenuItemFoodQuery();
                query.Where(query.MenuItemID == MenuItemID, query.SRMealSet == SRMealSet, query.FoodID == FoodID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MenuItemFood

        #region MenuSetting
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MenuSettingGet(string AccessKey, DateTime StartingDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MenuSettingQuery();
                query.Where(query.StartingDate == StartingDate);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MenuSetting

        #region MenuVersion
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MenuVersionGet(string AccessKey, String VersionID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MenuVersionQuery();
                query.Where(query.VersionID == VersionID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MenuVersion

        #region MergeBilling
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MergeBillingGet(string AccessKey, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MergeBillingQuery();
                query.Where(query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MergeBilling

        #region MergeBillingHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MergeBillingHistoryGet(string AccessKey, String RegistrationNo, String FromRegistrationNoBefore, String FromRegistrationNoAfter, DateTime LastUpdateDateTime)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MergeBillingHistoryQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.FromRegistrationNoBefore == FromRegistrationNoBefore, query.FromRegistrationNoAfter == FromRegistrationNoAfter, query.LastUpdateDateTime == LastUpdateDateTime);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MergeBillingHistory

        #region MonthlyAttedance
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MonthlyAttendanceGet(string AccessKey, Int64 MontlyAttendanceID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MonthlyAttendanceQuery();
                query.Where(query.MonthlyAttendanceID == MontlyAttendanceID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MonthlyAttedance

        #region MonthlyAttendanceDetail
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MonthlyAttendanceDetailGet(string AccessKey, Int64 MonthlyAttendanceDetailID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MonthlyAttendanceDetailQuery();
                query.Where(query.MonthlyAttendanceDetailID == MonthlyAttendanceDetailID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion MonthlyAttendanceDetail

        #region Morphology
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MorphologyGet(string AccessKey, String MorphologyID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new MorphologyQuery();
                query.Where(query.MorphologyID == MorphologyID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Morphology

        #region NccInacbg
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NccInacbgGet(string AccessKey, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new NccInacbgQuery();
                query.Where(query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion NccInacbg

        #region NosocomialMonitoring
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NosocomialMonitoringGet(string AccessKey, String RegistrationNo, Int32 MonitoringNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new NosocomialMonitoringQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.MonitoringNo == MonitoringNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion NosocomialMonitoring

        #region NosocomialMonitoringCatheter
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NosocomialMonitoringCatheterGet(string AccessKey, String RegistrationNo, Int32 MonitoringNo, Int32 SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new NosocomialMonitoringCatheterQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.MonitoringNo == MonitoringNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion NosocomialMonitoringCatheter

        #region NosocomialMonitoringEtt
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NosocomialMonitoringEttGet(string AccessKey, String RegistrationNo, Int32 MonitoringNo, Int32 SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new NosocomialMonitoringEttQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.MonitoringNo == MonitoringNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion NosocomialMonitoringEtt

        #region NosocomialMonitoringInfus
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NosocomialMonitoringInfusGet(string AccessKey, String RegistrationNo, Int32 MonitoringNo, Int32 SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new NosocomialMonitoringInfusQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.MonitoringNo == MonitoringNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion NosocomialMonitoringInfus

        #region NosocomialMonitoringNgt
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NosocomialMonitoringNgtGet(string AccessKey, String RegistrationNo, Int32 MonitoringNo, Int32 SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new NosocomialMonitoringNgtQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.MonitoringNo == MonitoringNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion NosocomialMonitoringNgt

        #region NosocomialMonitoringSurgery
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NosocomialMonitoringSurgeryGet(string AccessKey, String RegistrationNo, Int32 MonitoringNo, Int32 SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new NosocomialMonitoringSurgeryQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.MonitoringNo == MonitoringNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion NosocomialMonitoringSurgery

        #region NumberOfBed
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NumberOfBedGet(string AccessKey, DateTime StartingDate, String ServiceUnitID, String ClassID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new NumberOfBedQuery();
                query.Where(query.StartingDate == StartingDate, query.ServiceUnitID == ServiceUnitID, query.ClassID == ClassID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion NumberOfBed

        #region NursingAssessment
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NursingAssessmentGet(string AccessKey, String NursingAssessmentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new NursingAssessmentQuery();
                query.Where(query.NursingAssessmentID == NursingAssessmentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion NursingAssessment

        #region NursingAssessmentDiagnosa
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NursingAssessmentDiagnosaGet(string AccessKey, String QuestionID, String NursingDiagnosaID, Int32 AgeInMonthStart, Int32 AgeInMonthEnd)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new NursingAssessmentDiagnosaQuery();
                query.Where(query.QuestionID == QuestionID, query.NursingDiagnosaID == NursingDiagnosaID, query.AgeInMonthStart == AgeInMonthStart, query.AgeInMonthEnd == AgeInMonthEnd);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion NursingAssessmentDiagnosa

        #region NursingAssessmentTransDT
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NursingAssessmentTransDTGet(string AccessKey, Int64 ID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new NursingAssessmentTransDTQuery();
                query.Where(query.Id == ID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion NursingAssessmentTransDT

        #region NursingAssessmentTransHD
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NursingAssessmentTransHDGet(string AccessKey, Int64 ID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new NursingAssessmentTransHDQuery();
                query.Where(query.Id == ID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion NursingAssessmentTransHD

        #region NursingDiagnosa
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NursingDiagnosaGet(string AccessKey, String NursingDiagnosaID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new NursingDiagnosaQuery();
                query.Where(query.NursingDiagnosaID == NursingDiagnosaID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion NursingDiagnosa

        #region NursingDiagnosaEvaluation
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NursingDiagnosaEvaluationGet(string AccessKey, Int64 ID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new NursingDiagnosaEvaluationQuery();
                query.Where(query.Id == ID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion NursingDiagnosaEvaluation

        #region NursingDiagnosaServiceUnit
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NursingDiagnosaServiceUnitGet(string AccessKey, String NursingDiagnosaID, String ServiceUnitID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new NursingDiagnosaServiceUnitQuery();
                query.Where(query.NursingDiagnosaID == NursingDiagnosaID, query.ServiceUnitID == ServiceUnitID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion NursingDiagnosaServiceUnit

        #region NursingDiagnosaTemplate
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NursingDiagnosaTemplateGet(string AccessKey, Int32 TemplateID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new NursingDiagnosaTemplateQuery();
                query.Where(query.TemplateID == TemplateID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion NursingDiagnosaTemplate

        #region NursingDiagnosaTemplateDetail
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NursingDiagnosaTemplateDetailGet(string AccessKey, Int32 TemplateID, String QuestionID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new NursingDiagnosaTemplateDetailQuery();
                query.Where(query.TemplateID == TemplateID, query.QuestionID == QuestionID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion NursingDiagnosaTemplateDetail

        #region NursingDiagnosaTransDT
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NursingDiagnosaTransDTGet(string AccessKey, Int64 ID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new NursingDiagnosaTransDTQuery();
                query.Where(query.ID == ID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion NursingDiagnosaTransDT

        #region NursingTransHD
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NursingTransHDGet(string AccessKey, String TransactionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new NursingTransHDQuery();
                query.Where(query.TransactionNo == TransactionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion NursingTransHD

        #region OperationalTime
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void OperationalTimeGet(string AccessKey, String OperationalTimeID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new OperationalTimeQuery();
                query.Where(query.OperationalTimeID == OperationalTimeID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion OperationalTime

        #region OperationCostEstimation
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void OperationCostEstimationGet(string AccessKey, String DiagnoseID, String ProcedureID, String SRProcedureCategory, String ClassID, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new OperationCostEstimationQuery();
                query.Where(query.DiagnoseID == DiagnoseID, query.ProcedureID == ProcedureID, query.SRProcedureCategory == SRProcedureCategory, query.ClassID == ClassID, query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion OperationCostEstimation

        #region OperationCostEstimationItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void OperationCostEstimationItemGet(string AccessKey, String DiagnoseID, String ProcedureID, String SRProcedureCategory, String ClassID, String RegistrationNo, String ItemGroupID, String SRBillingGroup)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new OperationCostEstimationItemQuery();
                query.Where(query.DiagnoseID == DiagnoseID, query.ProcedureID == ProcedureID, query.SRProcedureCategory == SRProcedureCategory, query.ClassID == ClassID, query.RegistrationNo == RegistrationNo, query.ItemGroupID == ItemGroupID, query.SRBillingGroup == SRBillingGroup);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion OperationCostEstimationItem

        #region OperationNotesTemplate
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void OperationNotesTemplateGet(string AccessKey, Int32 TemplateID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new OperationNotesTemplateQuery();
                query.Where(query.TemplateID == TemplateID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion OperationNotesTemplate

        #region OrganizationUnit
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void OrganizationUnitGet(string AccessKey, Int32 OrganizationUnitID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new OrganizationUnitQuery();
                query.Where(query.OrganizationUnitID == OrganizationUnitID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion OrganizationUnit

        #region Paramedic
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicGet(string AccessKey, String ParamedicID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicQuery();
                query.Where(query.ParamedicID == ParamedicID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Paramedic

        #region ParamedicAutoBillItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicAutoBillItemGet(string AccessKey, String ParamedicID, String ServiceUnitID, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicAutoBillItemQuery();
                query.Where(query.ParamedicID == ParamedicID, query.ServiceUnitID == ServiceUnitID, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicAutoBillItem

        #region ParamedicBridging
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicBridgingGet(string AccessKey, String ParamedicID, String SRBridgingType, String BridgingID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicBridgingQuery();
                query.Where(query.ParamedicID == ParamedicID, query.SRBridgingType == SRBridgingType, query.BridgingID == BridgingID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicBridging

        #region ParamedicFeeAddDeduc
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeAddDeducGet(string AccessKey, String TransactionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeAddDeducQuery();
                query.Where(query.TransactionNo == TransactionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeAddDeduc

        #region ParamedicFeeAddDeducCoaItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeAddDeducCoaItemGet(string AccessKey, Int32 ListItemId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeAddDeducCoaItemQuery();
                query.Where(query.ListItemId == ListItemId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeAddDeducCoaItem

        #region ParamedicFeeByArSetting
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeByArSettingGet(string AccessKey, Int32 Id)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeByArSettingQuery();
                query.Where(query.Id == Id);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeByArSetting

        #region ParamedicFeeByFee4ServiceSetting
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeByFee4ServiceSettingGet(string AccessKey, Int32 Id)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeByFee4ServiceSettingQuery();
                query.Where(query.Id == Id);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeByFee4ServiceSetting

        #region ParamedicFeeByNumberOfPatients
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeByNumberOfPatientsGet(string AccessKey, DateTime RegistrationDate, String ParamedicID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeByNumberOfPatientsQuery();
                query.Where(query.RegistrationDate == RegistrationDate, query.ParamedicID == ParamedicID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeByNumberOfPatients

        #region ParamedicFeeByNumberOfPatientsDetail
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeByNumberOfPatientsDetailGet(string AccessKey, DateTime RegistrationDate, String ParamedicID, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeByNumberOfPatientsDetailQuery();
                query.Where(query.RegistrationDate == RegistrationDate, query.ParamedicID == ParamedicID, query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeByNumberOfPatientsDetail

        #region ParamedicFeeByNumberOfPatientsRangeAmount
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeByNumberOfPatientsRangeAmountGet(string AccessKey, Int32 CounterID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeByNumberOfPatientsRangeAmountQuery();
                query.Where(query.CounterID == CounterID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeByNumberOfPatientsRangeAmount

        #region ParamedicFeeByServiceSetting
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeByServiceSettingGet(string AccessKey, Int32 Id)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeByServiceSettingQuery();
                query.Where(query.Id == Id);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeByServiceSetting

        #region ParamedicFeeDeductions
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeDeductionsGet(string AccessKey, String TransactionNo, String SequenceNo, String TariffComponentID, Int32 DeductionID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeDeductionsQuery();
                query.Where(query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo, query.TariffComponentID == TariffComponentID, query.DeductionID == DeductionID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeDeductions

        #region ParamedicFeeDeductionSetting
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeDeductionSettingGet(string AccessKey, Int32 DeductionID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeDeductionSettingQuery();
                query.Where(query.DeductionID == DeductionID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeDeductionSetting

        #region ParamedicFeeGuarantorCategory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeGuarantorCategoryGet(string AccessKey, String ParamedicID, String SRPhysicianFeeType)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeGuarantorCategoryQuery();
                query.Where(query.ParamedicID == ParamedicID, query.SRPhysicianFeeType == SRPhysicianFeeType);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeGuarantorCategory

        #region ParamedicFeeGuarantorCategoryItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeGuarantorCategoryItemGet(string AccessKey, String ParamedicID, String SRPhysicianFeeType, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeGuarantorCategoryItemQuery();
                query.Where(query.ParamedicID == ParamedicID, query.SRPhysicianFeeType == SRPhysicianFeeType, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeGuarantorCategoryItem

        #region ParamedicFeeGuarantorCategoryItemComp
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeGuarantorCategoryItemCompGet(string AccessKey, String ParamedicID, String SRPhysicianFeeType, String ItemID, String TariffComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeGuarantorCategoryItemCompQuery();
                query.Where(query.ParamedicID == ParamedicID, query.SRPhysicianFeeType == SRPhysicianFeeType, query.ItemID == ItemID, query.TariffComponentID == TariffComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeGuarantorCategoryItemComp

        #region ParamedicFeeItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeItemGet(string AccessKey, String ParamedicID, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeItemQuery();
                query.Where(query.ParamedicID == ParamedicID, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeItem

        #region ParamedicFeeItemComp
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeItemCompGet(string AccessKey, String ParamedicID, String ItemID, String TariffComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeItemCompQuery();
                query.Where(query.ParamedicID == ParamedicID, query.ItemID == ItemID, query.TariffComponentID == TariffComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeItemComp

        #region ParamedicFeeItemGuarantor
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeItemGuarantorGet(string AccessKey, String ParamedicID, String ItemID, String GuarantorID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeItemGuarantorQuery();
                query.Where(query.ParamedicID == ParamedicID, query.ItemID == ItemID, query.GuarantorID == GuarantorID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeItemGuarantor

        #region ParamedicFeeItemGuarantorComp
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeItemGuarantorCompGet(string AccessKey, String ParamedicID, String ItemID, String GuarantorID, String TariffComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeItemGuarantorCompQuery();
                query.Where(query.ParamedicID == ParamedicID, query.ItemID == ItemID, query.GuarantorID == GuarantorID, query.TariffComponentID == TariffComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeItemGuarantorComp

        #region ParamedicFeePaymentDt
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeePaymentDtGet(string AccessKey, String PaymentNo, String VerificationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeePaymentDtQuery();
                query.Where(query.PaymentNo == PaymentNo, query.VerificationNo == VerificationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeePaymentDt

        #region ParamedicFeePaymentGroup
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeePaymentGroupGet(string AccessKey, String PaymentGroupNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeePaymentGroupQuery();
                query.Where(query.PaymentGroupNo == PaymentGroupNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeePaymentGroup

        #region ParamedicFeePaymentHd
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeePaymentHdGet(string AccessKey, String PaymentNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeePaymentHdQuery();
                query.Where(query.PaymentNo == PaymentNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeePaymentHd

        #region ParamedicFeeProgressiveTax
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeProgressiveTaxGet(string AccessKey, Int32 CounterID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeProgressiveTaxQuery();
                query.Where(query.CounterID == CounterID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeProgressiveTax

        #region ParamedicFeeRemun
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeRemunGet(string AccessKey, String ParamedicID, Int32 Year, Int32 Month)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeRemunQuery();
                query.Where(query.ParamedicID == ParamedicID, query.Year == Year, query.Month == Month);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeRemun

        #region ParamedicFeeTaxCalculation
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeTaxCalculationGet(string AccessKey, Int64 id)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeTaxCalculationQuery();
                query.Where(query.id == id);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeTaxCalculation

        #region ParamedicFeeTaxCalculationDt
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeTaxCalculationDtGet(string AccessKey, String VerificationNo, Decimal Percentage, Decimal Gross)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeTaxCalculationDtQuery();
                query.Where(query.VerificationNo == VerificationNo, query.Percentage == Percentage, query.Gross == Gross);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeTaxCalculationDt

        #region ParamedicFeeTaxCalculationHd
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeTaxCalculationHdGet(string AccessKey, String VerificationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeTaxCalculationHdQuery();
                query.Where(query.VerificationNo == VerificationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeTaxCalculationHd

        #region ParamedicFeeTransChargesItemComp
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeTransChargesItemCompGet(string AccessKey, String TransactionNo, String SequenceNo, String TariffComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeTransChargesItemCompQuery();
                query.Where(query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo, query.TariffComponentID == TariffComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeTransChargesItemComp

        #region ParamedicFeeTransChargesItemCompByDischargeDate
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeTransChargesItemCompByDischargeDateGet(string AccessKey, String TransactionNo, String SequenceNo, String TariffComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeTransChargesItemCompByDischargeDateQuery();
                query.Where(query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo, query.TariffComponentID == TariffComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeTransChargesItemCompByDischargeDate

        #region ParamedicFeeTransChargesItemCompSettled
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeTransChargesItemCompSettledGet(string AccessKey, String PaymentNo, String TransactionNo, String SequenceNo, String TariffComponentID, Boolean IsFromAr, Boolean IsReturn)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeTransChargesItemCompSettledQuery();
                query.Where(query.PaymentNo == PaymentNo, query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo, query.TariffComponentID == TariffComponentID, query.IsFromAr == IsFromAr, query.IsReturn == IsReturn);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeTransChargesItemCompSettled

        #region ParamedicFeeVerification
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeVerificationGet(string AccessKey, String VerificationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeVerificationQuery();
                query.Where(query.VerificationNo == VerificationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeVerification

        #region ParamedicFeeVerificationRentalRooms
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicFeeVerificationRentalRoomsGet(string AccessKey, String VerificationNo, String TransactionNo, String SequenceNo, String TariffComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicFeeVerificationRentalRoomsQuery();
                query.Where(query.VerificationNo == VerificationNo, query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo, query.TariffComponentID == TariffComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicFeeVerificationRentalRooms

        #region ParamedicLeave
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicLeaveGet(string AccessKey, String TransactionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicLeaveQuery();
                query.Where(query.TransactionNo == TransactionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicLeave

        #region ParamedicLeaveDate
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicLeaveDateGet(string AccessKey, String TransactionNo, DateTime LeaveDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicLeaveDateQuery();
                query.Where(query.TransactionNo == TransactionNo, query.LeaveDate == LeaveDate);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicLeaveDate

        #region ParamedicLeaveExeptionUnit
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicLeaveExeptionUnitGet(string AccessKey, String TransactionNo, String ServiceUnitID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicLeaveExeptionUnitQuery();
                query.Where(query.TransactionNo == TransactionNo, query.ServiceUnitID == ServiceUnitID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicLeaveExeptionUnit

        #region ParamedicSchedule
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicScheduleGet(string AccessKey, String ServiceUnitID, String ParamedicID, String PeriodYear)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicScheduleQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID, query.ParamedicID == ParamedicID, query.PeriodYear == PeriodYear);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicSchedule

        #region ParamedicScheduleDate
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicScheduleDateGet(string AccessKey, String ServiceUnitID, String ParamedicID, String PeriodYear, DateTime ScheduleDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicScheduleDateQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID, query.ParamedicID == ParamedicID, query.PeriodYear == PeriodYear, query.ScheduleDate == ScheduleDate);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicScheduleDate

        #region ParamedicTeam
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ParamedicTeamGet(string AccessKey, String RegistrationNo, String ParamedicID, DateTime StartDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ParamedicTeamQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.ParamedicID == ParamedicID, query.StartDate == StartDate);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ParamedicTeam

        #region PastMedicalHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PastMedicalHistoryGet(string AccessKey, String PatientID, String SRMedicalDisease)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PastMedicalHistoryQuery();
                query.Where(query.PatientID == PatientID, query.SRMedicalDisease == SRMedicalDisease);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PastMedicalHistory

        #region PastSurgicalHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PastSurgicalHistoryGet(string AccessKey, String PatientID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PastSurgicalHistoryQuery();
                query.Where(query.PatientID == PatientID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PastSurgicalHistory

        #region PastTransfusionHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PastTransfusionHistoryGet(string AccessKey, String PatientID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PastTransfusionHistoryQuery();
                query.Where(query.PatientID == PatientID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PastTransfusionHistory

        #region PathologyAnatomy
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PathologyAnatomyGet(string AccessKey, String ResultNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PathologyAnatomyQuery();
                query.Where(query.ResultNo == ResultNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PathologyAnatomy

        #region PathologyAnatomyDiagnosis
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PathologyAnatomyDiagnosisGet(string AccessKey, String ResultType, String DiagnosisID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PathologyAnatomyDiagnosisQuery();
                query.Where(query.ResultType == ResultType, query.DiagnosisID == DiagnosisID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PathologyAnatomyDiagnosis

        #region PathologyAnatomyLocationOfCytology
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PathologyAnatomyLocationOfCytologyGet(string AccessKey, String LocationID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PathologyAnatomyLocationOfCytologyQuery();
                query.Where(query.LocationID == LocationID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PathologyAnatomyLocationOfCytology

        #region PathologyAnatomySourceOfTissue
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PathologyAnatomySourceOfTissueGet(string AccessKey, String SourceOfTissueID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PathologyAnatomySourceOfTissueQuery();
                query.Where(query.SourceOfTissueID == SourceOfTissueID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PathologyAnatomySourceOfTissue

        #region PathologyAnatomyTissue
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PathologyAnatomyTissueGet(string AccessKey, String TissueID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PathologyAnatomyTissueQuery();
                query.Where(query.TissueID == TissueID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PathologyAnatomyTissue

        #region Pathway
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PathwayGet(string AccessKey, String PathwayID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PathwayQuery();
                query.Where(query.PathwayID == PathwayID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Pathway

        #region PathwayDiagnoseItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PathwayDiagnoseItemGet(string AccessKey, String PathwayID, String DiagnoseID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PathwayDiagnoseItemQuery();
                query.Where(query.PathwayID == PathwayID, query.DiagnoseID == DiagnoseID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PathwayDiagnoseItem

        #region PathwayItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PathwayItemGet(string AccessKey, String PathwayID, Int32 PathwayItemSeqNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PathwayItemQuery();
                query.Where(query.PathwayID == PathwayID, query.PathwayItemSeqNo == PathwayItemSeqNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PathwayItem

        #region PathwayItemExecution
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PathwayItemExecutionGet(string AccessKey, String PathwayID, Int32 PathwayItemSeqNo, Int32 DayNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PathwayItemExecutionQuery();
                query.Where(query.PathwayID == PathwayID, query.PathwayItemSeqNo == PathwayItemSeqNo, query.DayNo == DayNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PathwayItemExecution

        #region Patient
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientGet(string AccessKey, String PatientID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientQuery();
                query.Where(query.PatientID == PatientID);

                var tbl = query.LoadDataTable();
                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Patient

        #region PatientAllergy
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientAllergyGet(string AccessKey, String AllergyGroup, String Allergen, String PatientID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientAllergyQuery();
                query.Where(query.AllergyGroup == AllergyGroup, query.Allergen == Allergen, query.PatientID == PatientID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientAllergy

        #region PatientAssessment
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientAssessmentGet(string AccessKey, String RegistrationInfoMedicID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientAssessmentQuery();
                query.Where(query.RegistrationInfoMedicID == RegistrationInfoMedicID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientAssessment

        #region PatientBirthRecord
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientBirthRecordGet(string AccessKey, String PatientID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientBirthRecordQuery();
                query.Where(query.PatientID == PatientID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientBirthRecord

        #region PatientBlackListHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientBlackListHistoryGet(string AccessKey, String PatientID, Boolean IsBlackList, DateTime LastUpdateDateTime)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientBlackListHistoryQuery();
                query.Where(query.PatientID == PatientID, query.IsBlackList == IsBlackList, query.LastUpdateDateTime == LastUpdateDateTime);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientBlackListHistory

        #region PatientChildBirthHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientChildBirthHistoryGet(string AccessKey, String PatientID, Int32 SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientChildBirthHistoryQuery();
                query.Where(query.PatientID == PatientID, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientChildBirthHistory

        #region PatientDischargeHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientDischargeHistoryGet(string AccessKey, String RegistrationNo, String BedID, DateTime DischargeDate, String DischargeTime, DateTime LastUpdateDateTime)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientDischargeHistoryQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.BedID == BedID, query.DischargeDate == DischargeDate, query.DischargeTime == DischargeTime, query.LastUpdateDateTime == LastUpdateDateTime);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientDischargeHistory

        #region PatientDocument
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientDocumentGet(string AccessKey, Int64 PatientDocumentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientDocumentQuery();
                query.Where(query.PatientDocumentID == PatientDocumentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientDocument

        #region PatientDocumentImage
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientDocumentImageGet(string AccessKey, String PatientID, Int32 SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientDocumentImageQuery();
                query.Where(query.PatientID == PatientID, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientDocumentImage

        #region PatientEducation
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientEducationGet(string AccessKey, String RegistrationNo, Int32 SeqNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientEducationQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.SeqNo == SeqNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientEducation

        #region PatientEducationLine
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientEducationLineGet(string AccessKey, String RegistrationNo, Int32 SeqNo, String SRPatientEducation)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientEducationLineQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.SeqNo == SeqNo, query.SRPatientEducation == SRPatientEducation);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientEducationLine

        #region PatientEmergencyContact
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientEmergencyContactGet(string AccessKey, String PatientID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientEmergencyContactQuery();
                query.Where(query.PatientID == PatientID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientEmergencyContact

        #region PatientFluidBalance
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientFluidBalanceGet(string AccessKey, String RegistrationNo, Int32 SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientFluidBalanceQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientFluidBalance

        #region PatientFluidBalanceDetail
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientFluidBalanceDetailGet(string AccessKey, String RegistrationNo, Int32 SequenceNo, Int32 DetailSequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientFluidBalanceDetailQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.SequenceNo == SequenceNo, query.DetailSequenceNo == DetailSequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientFluidBalanceDetail

        #region PatientGenogram
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientGenogramGet(string AccessKey, String PatientID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientGenogramQuery();
                query.Where(query.PatientID == PatientID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientGenogram

        #region PatientHealthRecord
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientHealthRecordGet(string AccessKey, String TransactionNo, String RegistrationNo, String QuestionFormID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientHealthRecordQuery();
                query.Where(query.TransactionNo == TransactionNo, query.RegistrationNo == RegistrationNo, query.QuestionFormID == QuestionFormID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientHealthRecord

        #region PatientHealthRecordLine
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientHealthRecordLineGet(string AccessKey, String TransactionNo, String RegistrationNo, String QuestionFormID, String QuestionGroupID, String QuestionID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientHealthRecordLineQuery();
                query.Where(query.TransactionNo == TransactionNo, query.RegistrationNo == RegistrationNo, query.QuestionFormID == QuestionFormID, query.QuestionGroupID == QuestionGroupID, query.QuestionID == QuestionID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientHealthRecordLine

        #region PatientImage
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientImageGet(string AccessKey, String PatientID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientImageQuery();
                query.Where(query.PatientID == PatientID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientImage

        #region PatientImmunization
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientImmunizationGet(string AccessKey, String PatientID, String ImmunizationID, Int32 ImmunizationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientImmunizationQuery();
                query.Where(query.PatientID == PatientID, query.ImmunizationID == ImmunizationID, query.ImmunizationNo == ImmunizationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientImmunization

        #region PatientIncident
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientIncidentGet(string AccessKey, String PatientIncidentNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientIncidentQuery();
                query.Where(query.PatientIncidentNo == PatientIncidentNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientIncident

        #region PatientIncidentComponentType
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientIncidentComponentTypeGet(string AccessKey, String PatientIncidentNo, String SRIncidentType, String ComponentID, String SubComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientIncidentComponentTypeQuery();
                query.Where(query.PatientIncidentNo == PatientIncidentNo, query.SRIncidentType == SRIncidentType, query.ComponentID == ComponentID, query.SubComponentID == SubComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientIncidentComponentType

        #region PatientIncidentInvestigation
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientIncidentInvestigationGet(string AccessKey, String PatientIncidentNo, String ServiceUnitID, String SeqNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientIncidentInvestigationQuery();
                query.Where(query.PatientIncidentNo == PatientIncidentNo, query.ServiceUnitID == ServiceUnitID, query.SeqNo == SeqNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientIncidentInvestigation

        #region PatientIncidentKTD
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientIncidentKTDGet(string AccessKey, String PatientIncidentNo, String SRIncidentKTD)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientIncidentKTDQuery();
                query.Where(query.PatientIncidentNo == PatientIncidentNo, query.SRIncidentKTD == SRIncidentKTD);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientIncidentKTD

        #region PatientIncidentRelatedUnit
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientIncidentRelatedUnitGet(string AccessKey, String PatientIncidentNo, String ServiceUnitID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientIncidentRelatedUnitQuery();
                query.Where(query.PatientIncidentNo == PatientIncidentNo, query.ServiceUnitID == ServiceUnitID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientIncidentRelatedUnit

        #region PatientIncidentSafetyGoals
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientIncidentSafetyGoalsGet(string AccessKey, String PatientIncidentNo, String SRSafetyGoals)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientIncidentSafetyGoalsQuery();
                query.Where(query.PatientIncidentNo == PatientIncidentNo, query.SRSafetyGoals == SRSafetyGoals);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientIncidentSafetyGoals

        #region PatientIncidentUnderlyingCausesItemComponent
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientIncidentUnderlyingCausesItemComponentGet(string AccessKey, String PatientIncidentNo, String ServiceUnitID, String FactorID, String FactorItemID, String ComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientIncidentUnderlyingCausesItemComponentQuery();
                query.Where(query.PatientIncidentNo == PatientIncidentNo, query.ServiceUnitID == ServiceUnitID, query.FactorID == FactorID, query.FactorItemID == FactorItemID, query.ComponentID == ComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientIncidentUnderlyingCausesItemComponent

        #region PatientOdontogram
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientOdontogramGet(string AccessKey, String PatientID, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientOdontogramQuery();
                query.Where(query.PatientID == PatientID, query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientOdontogram

        #region PatientReceivableMonthlySummaryDetail
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientReceivableMonthlySummaryDetailGet(string AccessKey, Int64 DetailID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientReceivableMonthlySummaryDetailQuery();
                query.Where(query.DetailID == DetailID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientReceivableMonthlySummaryDetail

        #region PatientRelated
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientRelatedGet(string AccessKey, String PatientID, String RelatedPatientID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientRelatedQuery();
                query.Where(query.PatientID == PatientID, query.RelatedPatientID == RelatedPatientID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientRelated

        #region PatientTransfer
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientTransferGet(string AccessKey, String TransferNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientTransferQuery();
                query.Where(query.TransferNo == TransferNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientTransfer

        #region PatientTransferHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientTransferHistoryGet(string AccessKey, String RegistrationNo, String TransferNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientTransferHistoryQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.TransferNo == TransferNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientTransferHistory

        #region PatientVitalSignMonitoring
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientVitalSignMonitoringGet(string AccessKey, String RegistrationNo, String OrderNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientVitalSignMonitoringQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.OrderNo == OrderNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientVitalSignMonitoring

        #region PatientVitalSignMonitoringItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PatientVitalSignMonitoringItemGet(string AccessKey, String RegistrationNo, String OrderNo, String VitalSignID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PatientVitalSignMonitoringItemQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.OrderNo == OrderNo, query.VitalSignID == VitalSignID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PatientVitalSignMonitoringItem

        #region PaymentMethod
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PaymentMethodGet(string AccessKey, String SRPaymentTypeID, String SRPaymentMethodID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PaymentMethodQuery();
                query.Where(query.SRPaymentTypeID == SRPaymentTypeID, query.SRPaymentMethodID == SRPaymentMethodID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PaymentMethod

        #region PaymentType
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PaymentTypeGet(string AccessKey, String SRPaymentTypeID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PaymentTypeQuery();
                query.Where(query.SRPaymentTypeID == SRPaymentTypeID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PaymentType

        #region PayrollPeriod
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PayrollPeriodGet(string AccessKey, Int32 PayrollPeriodID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PayrollPeriodQuery();
                query.Where(query.PayrollPeriodID == PayrollPeriodID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PayrollPeriod

        #region PensionTax
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PensionTaxGet(string AccessKey, Int32 PensionTaxID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PensionTaxQuery();
                query.Where(query.PensionTaxID == PensionTaxID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PensionTax

        #region PersonalAddress
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PersonalAddressGet(string AccessKey, Int32 PersonalAddressID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PersonalAddressQuery();
                query.Where(query.PersonalAddressID == PersonalAddressID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PersonalAddress

        #region PersonalContact
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PersonalContactGet(string AccessKey, Int32 PersonalContactID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PersonalContactQuery();
                query.Where(query.PersonalContactID == PersonalContactID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PersonalContact

        #region PersonalEducationHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PersonalEducationHistoryGet(string AccessKey, Int32 PersonalEducationHistoryID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PersonalEducationHistoryQuery();
                query.Where(query.PersonalEducationHistoryID == PersonalEducationHistoryID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PersonalEducationHistory

        #region PersonalEmergencyContact
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PersonalEmergencyContactGet(string AccessKey, Int32 PersonalEmergencyContactID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PersonalEmergencyContactQuery();
                query.Where(query.PersonalEmergencyContactID == PersonalEmergencyContactID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PersonalEmergencyContact

        #region PersonalFamily
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PersonalFamilyGet(string AccessKey, Int32 PersonalFamilyID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PersonalFamilyQuery();
                query.Where(query.PersonalFamilyID == PersonalFamilyID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PersonalFamily

        #region PersonalIdentification
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PersonalIdentificationGet(string AccessKey, Int32 PersonalIdentificationID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PersonalIdentificationQuery();
                query.Where(query.PersonalIdentificationID == PersonalIdentificationID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PersonalIdentification

        #region PersonalInfo
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PersonalInfoGet(string AccessKey, Int32 PersonID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PersonalInfoQuery();
                query.Where(query.PersonID == PersonID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PersonalInfo

        #region PersonalLicence
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PersonalLicenceGet(string AccessKey, Int32 PersonalLicenceID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PersonalLicenceQuery();
                query.Where(query.PersonalLicenceID == PersonalLicenceID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PersonalLicence

        #region PersonalOrganization
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PersonalOrganizationGet(string AccessKey, Int32 PersonalOrganizationID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PersonalOrganizationQuery();
                query.Where(query.PersonalOrganizationID == PersonalOrganizationID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PersonalOrganization

        #region PersonalPhysical
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PersonalPhysicalGet(string AccessKey, Int32 PersonalPhysicalID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PersonalPhysicalQuery();
                query.Where(query.PersonalPhysicalID == PersonalPhysicalID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PersonalPhysical

        #region PersonalRecruitmentTest
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PersonalRecruitmentTestGet(string AccessKey, Int32 PersonalRecruitmentTestID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PersonalRecruitmentTestQuery();
                query.Where(query.PersonalRecruitmentTestID == PersonalRecruitmentTestID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PersonalRecruitmentTest

        #region PersonalWorkExperience
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PersonalWorkExperienceGet(string AccessKey, Int32 PersonalWorkExperienceID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PersonalWorkExperienceQuery();
                query.Where(query.PersonalWorkExperienceID == PersonalWorkExperienceID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PersonalWorkExperience

        #region PersonnelRequisition
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PersonnelRequisitionGet(string AccessKey, Int32 PersonnelRequisitionID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PersonnelRequisitionQuery();
                query.Where(query.PersonnelRequisitionID == PersonnelRequisitionID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PersonnelRequisition

        #region PettyCash
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PettyCashGet(string AccessKey, String TransactionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PettyCashQuery();
                query.Where(query.TransactionNo == TransactionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PettyCash

        #region PettyCashItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PettyCashItemGet(string AccessKey, String TransactionNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PettyCashItemQuery();
                query.Where(query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PettyCashItem

        #region Pkp
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PkpGet(string AccessKey, Int32 PkpID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PkpQuery();
                query.Where(query.PkpID == PkpID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Pkp

        #region Position
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PositionGet(string AccessKey, Int32 PositionID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PositionQuery();
                query.Where(query.PositionID == PositionID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Position

        #region PositionDuty
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PositionDutyGet(string AccessKey, Int32 PositionDutyID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PositionDutyQuery();
                query.Where(query.PositionDutyID == PositionDutyID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PositionDuty

        #region PositionEducation
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PositionEducationGet(string AccessKey, Int32 PositionEducationID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PositionEducationQuery();
                query.Where(query.PositionEducationID == PositionEducationID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PositionEducation

        #region PositionEmploymentCompany
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PositionEmploymentCompanyGet(string AccessKey, Int32 PositionEmploymentCompanyID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PositionEmploymentCompanyQuery();
                query.Where(query.PositionEmploymentCompanyID == PositionEmploymentCompanyID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PositionEmploymentCompany

        #region PositionFunctionalArea
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PositionFunctionalAreaGet(string AccessKey, Int32 PositionFunctionalAreaID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PositionFunctionalAreaQuery();
                query.Where(query.PositionFunctionalAreaID == PositionFunctionalAreaID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PositionFunctionalArea

        #region PositionGrade
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PositionGradeGet(string AccessKey, Int32 PositionGradeID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PositionGradeQuery();
                query.Where(query.PositionGradeID == PositionGradeID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PositionGrade

        #region PositionLevel
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PositionLevelGet(string AccessKey, Int32 PositionLevelID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PositionLevelQuery();
                query.Where(query.PositionLevelID == PositionLevelID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PositionLevel

        #region PositionLicense
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PositionLicenseGet(string AccessKey, Int32 PositionLicenseID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PositionLicenseQuery();
                query.Where(query.PositionLicenseID == PositionLicenseID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PositionLicense

        #region PositionPersonal
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PositionPersonalGet(string AccessKey, Int32 PositionPersonalID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PositionPersonalQuery();
                query.Where(query.PositionPersonalID == PositionPersonalID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PositionPersonal

        #region PositionPhysical
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PositionPhysicalGet(string AccessKey, Int32 PositionPhysicalID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PositionPhysicalQuery();
                query.Where(query.PositionPhysicalID == PositionPhysicalID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PositionPhysical

        #region PositionPsychological
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PositionPsychologicalGet(string AccessKey, Int32 PositionPsychologicalID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PositionPsychologicalQuery();
                query.Where(query.PositionPsychologicalID == PositionPsychologicalID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PositionPsychological

        #region PositionResponsibility
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PositionResponsibilityGet(string AccessKey, Int32 PositionResponsibilityID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PositionResponsibilityQuery();
                query.Where(query.PositionResponsibilityID == PositionResponsibilityID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PositionResponsibility

        #region PositionWorkExperience
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PositionWorkExperienceGet(string AccessKey, Int32 PositionWorkExperienceID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PositionWorkExperienceQuery();
                query.Where(query.PositionWorkExperienceID == PositionWorkExperienceID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PositionWorkExperience

        #region PostingStatus
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PostingStatusGet(string AccessKey, Int32 PostingId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PostingStatusQuery();
                query.Where(query.PostingId == PostingId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PostingStatus

        #region PphProgressiveTax
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PphProgressiveTaxGet(string AccessKey, Int32 CounterID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PphProgressiveTaxQuery();
                query.Where(query.CounterID == CounterID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PphProgressiveTax

        #region PpiAntimicrobialApplications
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PpiAntimicrobialApplicationsGet(string AccessKey, String RegistrationNo, String SRTherapyGroup, String TherapyID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PpiAntimicrobialApplicationsQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.SRTherapyGroup == SRTherapyGroup, query.TherapyID == TherapyID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PpiAntimicrobialApplications

        #region PpiDiseaseFactors
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PpiDiseaseFactorsGet(string AccessKey, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PpiDiseaseFactorsQuery();
                query.Where(query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PpiDiseaseFactors

        #region PpiInfection
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PpiInfectionGet(string AccessKey, String RegistrationNo, String SRInfectionType)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PpiInfectionQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.SRInfectionType == SRInfectionType);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PpiInfection

        #region PpiNeedlePunctured
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PpiNeedlePuncturedGet(string AccessKey, String TransactionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PpiNeedlePuncturedQuery();
                query.Where(query.TransactionNo == TransactionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PpiNeedlePunctured

        #region PpiProcedureSurveillance
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PpiProcedureSurveillanceGet(string AccessKey, String BookingNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PpiProcedureSurveillanceQuery();
                query.Where(query.BookingNo == BookingNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PpiProcedureSurveillance

        #region PpiProcedureSurveillanceUseOfAntibiotics
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PpiProcedureSurveillanceUseOfAntibioticsGet(string AccessKey, String BookingNo, String ItemID, DateTime StartDate)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PpiProcedureSurveillanceUseOfAntibioticsQuery();
                query.Where(query.BookingNo == BookingNo, query.ItemID == ItemID, query.StartDate == StartDate);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PpiProcedureSurveillanceUseOfAntibiotics

        #region PpiRiskFactors
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PpiRiskFactorsGet(string AccessKey, String RegistrationNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PpiRiskFactorsQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PpiRiskFactors

        #region PpiRiskFactorsItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PpiRiskFactorsItemGet(string AccessKey, String RegistrationNo, String SequenceNo, DateTime DateOfInfection, String SRSignsOfInfection)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PpiRiskFactorsItemQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.SequenceNo == SequenceNo, query.DateOfInfection == DateOfInfection, query.SRSignsOfInfection == SRSignsOfInfection);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PpiRiskFactorsItem

        #region Printer
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PrinterGet(string AccessKey, String PrinterID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PrinterQuery();
                query.Where(query.PrinterID == PrinterID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Printer

        #region PrintJob
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PrintJobGet(string AccessKey, Int64 PrintNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PrintJobQuery();
                query.Where(query.PrintNo == PrintNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PrintJob

        #region PrintJobLog
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PrintJobLogGet(string AccessKey, Int64 PrintNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PrintJobLogQuery();
                query.Where(query.PrintNo == PrintNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PrintJobLog

        #region PrmrjFollowUp
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PrmrjFollowUpGet(string AccessKey, String RegistrationInfoMedicID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PrmrjFollowUpQuery();
                query.Where(query.RegistrationInfoMedicID == RegistrationInfoMedicID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion PrmrjFollowUp

        #region Procedure
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ProcedureGet(string AccessKey, String ProcedureID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ProcedureQuery();
                query.Where(query.ProcedureID == ProcedureID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Procedure

        #region ProductAccount
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ProductAccountGet(string AccessKey, String ProductAccountID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ProductAccountQuery();
                query.Where(query.ProductAccountID == ProductAccountID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ProductAccount

        #region ProductAccountGuarantorGroup
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ProductAccountGuarantorGroupGet(string AccessKey, String ProductAccountID, String SRGuarantorIncomeGroup)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ProductAccountGuarantorGroupQuery();
                query.Where(query.ProductAccountID == ProductAccountID, query.SRGuarantorIncomeGroup == SRGuarantorIncomeGroup);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ProductAccountGuarantorGroup

        #region ProductionFormula
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ProductionFormulaGet(string AccessKey, String FormulaID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ProductionFormulaQuery();
                query.Where(query.FormulaID == FormulaID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ProductionFormula

        #region ProductionFormulaItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ProductionFormulaItemGet(string AccessKey, String FormulaID, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ProductionFormulaItemQuery();
                query.Where(query.FormulaID == FormulaID, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ProductionFormulaItem

        #region ProductionOfGoods
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ProductionOfGoodsGet(string AccessKey, String ProductionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ProductionOfGoodsQuery();
                query.Where(query.ProductionNo == ProductionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ProductionOfGoods

        #region ProductionOfGoodsItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ProductionOfGoodsItemGet(string AccessKey, String ProductionNo, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ProductionOfGoodsItemQuery();
                query.Where(query.ProductionNo == ProductionNo, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ProductionOfGoodsItem

        #region Ptkp
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PtkpGet(string AccessKey, Int32 PtkpID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new PtkpQuery();
                query.Where(query.PtkpID == PtkpID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Ptkp

        #region Question
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void QuestionGet(string AccessKey, String QuestionID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new QuestionQuery();
                query.Where(query.QuestionID == QuestionID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Question

        #region QuestionAnswerSelection
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void QuestionAnswerSelectionGet(string AccessKey, String QuestionAnswerSelectionID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new QuestionAnswerSelectionQuery();
                query.Where(query.QuestionAnswerSelectionID == QuestionAnswerSelectionID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion QuestionAnswerSelection

        #region QuestionAnswerSelectionLine
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void QuestionAnswerSelectionLineGet(string AccessKey, String QuestionAnswerSelectionID, String QuestionAnswerSelectionLineID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new QuestionAnswerSelectionLineQuery();
                query.Where(query.QuestionAnswerSelectionID == QuestionAnswerSelectionID, query.QuestionAnswerSelectionLineID == QuestionAnswerSelectionLineID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion QuestionAnswerSelectionLine

        #region QuestionForm
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void QuestionFormGet(string AccessKey, String QuestionFormID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new QuestionFormQuery();
                query.Where(query.QuestionFormID == QuestionFormID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion QuestionForm

        #region QuestionFormInServiceUnit
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void QuestionFormInServiceUnitGet(string AccessKey, String ServiceUnitID, String QuestionFormID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new QuestionFormInServiceUnitQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID, query.QuestionFormID == QuestionFormID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion QuestionFormInServiceUnit

        #region QuestionGroup
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void QuestionGroupGet(string AccessKey, String QuestionGroupID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new QuestionGroupQuery();
                query.Where(query.QuestionGroupID == QuestionGroupID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion QuestionGroup

        #region QuestionGroupInForm
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void QuestionGroupInFormGet(string AccessKey, String QuestionFormID, String QuestionGroupID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new QuestionGroupInFormQuery();
                query.Where(query.QuestionFormID == QuestionFormID, query.QuestionGroupID == QuestionGroupID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion QuestionGroupInForm

        #region QuestionInGroup
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void QuestionInGroupGet(string AccessKey, String QuestionGroupID, String QuestionID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new QuestionInGroupQuery();
                query.Where(query.QuestionGroupID == QuestionGroupID, query.QuestionID == QuestionID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion QuestionInGroup

        #region ReasonsForTreatment
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ReasonsForTreatmentGet(string AccessKey, String SRReasonVisit, String ReasonsForTreatmentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ReasonsForTreatmentQuery();
                query.Where(query.SRReasonVisit == SRReasonVisit, query.ReasonsForTreatmentID == ReasonsForTreatmentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ReasonsForTreatment

        #region ReasonsForTreatmentDesc
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ReasonsForTreatmentDescGet(string AccessKey, String SRReasonVisit, String ReasonsForTreatmentID, String ReasonsForTreatmentDescID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ReasonsForTreatmentDescQuery();
                query.Where(query.SRReasonVisit == SRReasonVisit, query.ReasonsForTreatmentID == ReasonsForTreatmentID, query.ReasonsForTreatmentDescID == ReasonsForTreatmentDescID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ReasonsForTreatmentDesc

        #region RecalculationProcessHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RecalculationProcessHistoryGet(string AccessKey, String RecalculationProcessNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RecalculationProcessHistoryQuery();
                query.Where(query.RecalculationProcessNo == RecalculationProcessNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RecalculationProcessHistory

        #region RecipeMarginValue
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RecipeMarginValueGet(string AccessKey, Int32 CounterID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RecipeMarginValueQuery();
                query.Where(query.CounterID == CounterID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RecipeMarginValue

        #region RecruitmentMethod
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RecruitmentMethodGet(string AccessKey, Int32 RecruitmentMethodID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RecruitmentMethodQuery();
                query.Where(query.RecruitmentMethodID == RecruitmentMethodID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RecruitmentMethod

        #region RecruitmentPlan
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RecruitmentPlanGet(string AccessKey, Int32 RecruitmentPlanID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RecruitmentPlanQuery();
                query.Where(query.RecruitmentPlanID == RecruitmentPlanID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RecruitmentPlan

        #region Referral
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ReferralGet(string AccessKey, String ReferralID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ReferralQuery();
                query.Where(query.ReferralID == ReferralID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Referral

        #region Registration
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationGet(string AccessKey, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationQuery();
                query.Where(query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Registration

        #region RegistrationApproximateCoverageDetail
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationApproximateCoverageDetailGet(string AccessKey, String RegistrationNo, String ClassID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationApproximateCoverageDetailQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.ClassID == ClassID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationApproximateCoverageDetail

        #region RegistrationBpjsPackage
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationBpjsPackageGet(string AccessKey, String RegistrationNo, String PackageID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationBpjsPackageQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.PackageID == PackageID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationBpjsPackage

        #region RegistrationCoverageDetail
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationCoverageDetailGet(string AccessKey, String RegistrationNo, String ClassID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationCoverageDetailQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.ClassID == ClassID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationCoverageDetail

        #region RegistrationDischargeDetail
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationDischargeDetailGet(string AccessKey, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationDischargeDetailQuery();
                query.Where(query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationDischargeDetail

        #region RegistrationDiscountRule
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationDiscountRuleGet(string AccessKey, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationDiscountRuleQuery();
                query.Where(query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationDiscountRule

        #region RegistrationDocumentCheckList
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationDocumentCheckListGet(string AccessKey, String RegistrationNo, Int32 DocumentFilesID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationDocumentCheckListQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.DocumentFilesID == DocumentFilesID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationDocumentCheckList

        #region RegistrationGuarantor
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationGuarantorGet(string AccessKey, String RegistrationNo, String GuarantorID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationGuarantorQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.GuarantorID == GuarantorID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationGuarantor

        #region RegistrationGuarantorHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationGuarantorHistoryGet(string AccessKey, String RegistrationNo, String FromGuarantorID, String ToGuarantorID, DateTime LastUpdateDateTime)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationGuarantorHistoryQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.FromGuarantorID == FromGuarantorID, query.ToGuarantorID == ToGuarantorID, query.LastUpdateDateTime == LastUpdateDateTime);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationGuarantorHistory

        #region RegistrationInfo
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationInfoGet(string AccessKey, String RegistrationInfoID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationInfoQuery();
                query.Where(query.RegistrationInfoID == RegistrationInfoID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationInfo

        #region RegistrationInfoMedic
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationInfoMedicGet(string AccessKey, String RegistrationInfoMedicID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationInfoMedicQuery();
                query.Where(query.RegistrationInfoMedicID == RegistrationInfoMedicID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationInfoMedic

        #region RegistrationInfoMedical
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationInfoMedicalGet(string AccessKey, Int64 ID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationInfoMedicalQuery();
                query.Where(query.Id == ID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationInfoMedical

        #region RegistrationInfoMedicBodyDiagram
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationInfoMedicBodyDiagramGet(string AccessKey, String RegistrationInfoMedicID, String BodyID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationInfoMedicBodyDiagramQuery();
                query.Where(query.RegistrationInfoMedicID == RegistrationInfoMedicID, query.BodyID == BodyID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationInfoMedicBodyDiagram

        #region RegistrationInfoMedicVitalSign
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationInfoMedicVitalSignGet(string AccessKey, String RegistrationInfoMedicID, String VitalSignID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationInfoMedicVitalSignQuery();
                query.Where(query.RegistrationInfoMedicID == RegistrationInfoMedicID, query.VitalSignID == VitalSignID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationInfoMedicVitalSign

        #region RegistrationInfoSumary
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationInfoSumaryGet(string AccessKey, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationInfoSumaryQuery();
                query.Where(query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationInfoSumary

        #region RegistrationItemRule
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationItemRuleGet(string AccessKey, String RegistrationNo, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationItemRuleQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationItemRule

        #region RegistrationMeasuredGoal
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationMeasuredGoalGet(string AccessKey, String RegistrationNo, Int32 SeqNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationMeasuredGoalQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.SeqNo == SeqNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationMeasuredGoal

        #region RegistrationPathway
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationPathwayGet(string AccessKey, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationPathwayQuery();
                query.Where(query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationPathway

        #region RegistrationPlafondHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationPlafondHistoryGet(string AccessKey, Int64 HistoryID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationPlafondHistoryQuery();
                query.Where(query.HistoryID == HistoryID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationPlafondHistory

        #region RegistrationPlafondRule
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationPlafondRuleGet(string AccessKey, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationPlafondRuleQuery();
                query.Where(query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationPlafondRule

        #region RegistrationResponsiblePerson
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationResponsiblePersonGet(string AccessKey, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationResponsiblePersonQuery();
                query.Where(query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationResponsiblePerson

        #region RegistrationTariffComponentDiscountRule
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegistrationTariffComponentDiscountRuleGet(string AccessKey, String RegistrationNo, String TariffComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RegistrationTariffComponentDiscountRuleQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.TariffComponentID == TariffComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RegistrationTariffComponentDiscountRule

        #region Reservation
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ReservationGet(string AccessKey, String ReservationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ReservationQuery();
                query.Where(query.ReservationNo == ReservationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Reservation

        #region RevenueByCashBasis
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RevenueByCashBasisGet(string AccessKey, DateTime StartDate, DateTime EndDate, String UserID, String PaymentNo, String PaymentReferenceNo, String TransactionNo, String SequenceNo, String TariffComponentName, String TxType)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RevenueByCashBasisQuery();
                query.Where(query.StartDate == StartDate, query.EndDate == EndDate, query.UserID == UserID, query.PaymentNo == PaymentNo, query.PaymentReferenceNo == PaymentReferenceNo, query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo, query.TariffComponentName == TariffComponentName, query.TxType == TxType);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RevenueByCashBasis

        #region RiskFactors
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RiskFactorsGet(string AccessKey, String SRRiskFactorsType, String RiskFactorsID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RiskFactorsQuery();
                query.Where(query.SRRiskFactorsType == SRRiskFactorsType, query.RiskFactorsID == RiskFactorsID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RiskFactors

        #region RiskGrading
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RiskGradingGet(string AccessKey, String RiskGradingID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RiskGradingQuery();
                query.Where(query.RiskGradingID == RiskGradingID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RiskGrading

        #region RiskGradingMtx
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RiskGradingMtxGet(string AccessKey, String SRClinicalImpact, String SRIncidentProbabilityFrequency)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RiskGradingMtxQuery();
                query.Where(query.SRClinicalImpact == SRClinicalImpact, query.SRIncidentProbabilityFrequency == SRIncidentProbabilityFrequency);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RiskGradingMtx

        #region RL4Education
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RL4EducationGet(string AccessKey, Int32 RL4EducationID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RL4EducationQuery();
                query.Where(query.RL4EducationID == RL4EducationID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RL4Education

        #region RlMasterReport
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RlMasterReportGet(string AccessKey, Int32 RlMasterReportID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RlMasterReportQuery();
                query.Where(query.RlMasterReportID == RlMasterReportID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RlMasterReport

        #region RlMasterReportItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RlMasterReportItemGet(string AccessKey, Int32 RlMasterReportItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RlMasterReportItemQuery();
                query.Where(query.RlMasterReportItemID == RlMasterReportItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RlMasterReportItem

        #region RlTxReport
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RlTxReportGet(string AccessKey, String RlTxReportNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RlTxReportQuery();
                query.Where(query.RlTxReportNo == RlTxReportNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RlTxReport

        #region RlTxReport12Item
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RlTxReport12ItemGet(string AccessKey, String PeriodMonth, String PeriodYear)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RlTxReport12ItemQuery();
                query.Where(query.PeriodMonth == PeriodMonth, query.PeriodYear == PeriodYear);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RlTxReport12Item

        #region RlTxReport2
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RlTxReport2Get(string AccessKey, String RlTxReportNo, Int32 RlMasterReportItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RlTxReport2Query();
                query.Where(query.RlTxReportNo == RlTxReportNo, query.RlMasterReportItemID == RlMasterReportItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RlTxReport2

        #region RlTxReport4A
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RlTxReport4AGet(string AccessKey, String RlTxReportNo, Int32 RlMasterReportItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RlTxReport4AQuery();
                query.Where(query.RlTxReportNo == RlTxReportNo, query.RlMasterReportItemID == RlMasterReportItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RlTxReport4A

        #region RlTxReport4ASebab
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RlTxReport4ASebabGet(string AccessKey, String RlTxReportNo, Int32 RlMasterReportItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RlTxReport4ASebabQuery();
                query.Where(query.RlTxReportNo == RlTxReportNo, query.RlMasterReportItemID == RlMasterReportItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RlTxReport4ASebab

        #region RlTxReport4B
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RlTxReport4BGet(string AccessKey, String RlTxReportNo, Int32 RlMasterReportItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RlTxReport4BQuery();
                query.Where(query.RlTxReportNo == RlTxReportNo, query.RlMasterReportItemID == RlMasterReportItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RlTxReport4B

        #region RlTxReport4BSebab
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RlTxReport4BSebabGet(string AccessKey, String RlTxReportNo, Int32 RlMasterReportItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new RlTxReport4BSebabQuery();
                query.Where(query.RlTxReportNo == RlTxReportNo, query.RlMasterReportItemID == RlMasterReportItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion RlTxReport4BSebab

        #region SalaryComponent
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SalaryComponentGet(string AccessKey, Int32 SalaryComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SalaryComponentQuery();
                query.Where(query.SalaryComponentID == SalaryComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SalaryComponent

        #region SalaryComponentRounding
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SalaryComponentRoundingGet(string AccessKey, Int32 SalaryComponentRoundingID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SalaryComponentRoundingQuery();
                query.Where(query.SalaryComponentRoundingID == SalaryComponentRoundingID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SalaryComponentRounding

        #region SalaryComponentRuleDefinition
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SalaryComponentRuleDefinitionGet(string AccessKey, Int64 SalaryComponentRuleDefinitionID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SalaryComponentRuleDefinitionQuery();
                query.Where(query.SalaryComponentRuleDefinitionID == SalaryComponentRuleDefinitionID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SalaryComponentRuleDefinition

        #region SalaryComponentRuleMatrix
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SalaryComponentRuleMatrixGet(string AccessKey, Int32 SalaryComponentRuleMatrixID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SalaryComponentRuleMatrixQuery();
                query.Where(query.SalaryComponentRuleMatrixID == SalaryComponentRuleMatrixID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SalaryComponentRuleMatrix

        #region SalaryTemplate
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SalaryTemplateGet(string AccessKey, Int32 SalaryTemplateID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SalaryTemplateQuery();
                query.Where(query.SalaryTemplateID == SalaryTemplateID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SalaryTemplate

        #region SalaryTemplateItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SalaryTemplateItemGet(string AccessKey, Int32 SalaryTemplateID, Int32 SalaryComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SalaryTemplateItemQuery();
                query.Where(query.SalaryTemplateID == SalaryTemplateID, query.SalaryComponentID == SalaryComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SalaryTemplateItem

        #region ServiceRoom
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceRoomGet(string AccessKey, String RoomID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceRoomQuery();
                query.Where(query.RoomID == RoomID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceRoom

        #region ServiceUnit
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitGet(string AccessKey, String ServiceUnitID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnit

        #region ServiceUnitAssessmentType
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitAssessmentTypeGet(string AccessKey, String ServiceUnitID, String SRAssessmentType)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitAssessmentTypeQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID, query.SRAssessmentType == SRAssessmentType);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnitAssessmentType

        #region ServiceUnitAutoBillItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitAutoBillItemGet(string AccessKey, String ServiceUnitID, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitAutoBillItemQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnitAutoBillItem

        #region ServiceUnitBooking
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitBookingGet(string AccessKey, String BookingNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitBookingQuery();
                query.Where(query.BookingNo == BookingNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnitBooking

        //#region ServiceUnitBookingForm
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public void ServiceUnitBookingFormGet(string AccessKey, String BookingNo, String QuestionFormID)
        //{
        //    var log = LogAdd();
        //    try
        //    {
        //        ValidateAccessKey(AccessKey);

        //        var query = new ServiceUnitBookingFormQuery();
        //        query.Where(query.BookingNo == BookingNo, query.QuestionFormID == QuestionFormID);

        //        var tbl = query.LoadDataTable();

        //        InspectOneResult(tbl);

        //        WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
        //    }
        //}
        //#endregion ServiceUnitBookingForm

        #region ServiceUnitBridging
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitBridgingGet(string AccessKey, String ServiceUnitID, String SRBridgingType, String BridgingID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitBridgingQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID, query.SRBridgingType == SRBridgingType, query.BridgingID == BridgingID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnitBridging

        #region ServiceUnitClassMealSetMenuSetting
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitClassMealSetMenuSettingGet(string AccessKey, String ServiceUnitID, String ClassID, String SRMealSet)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitClassMealSetMenuSettingQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID, query.ClassID == ClassID, query.SRMealSet == SRMealSet);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnitClassMealSetMenuSetting

        #region ServiceUnitClassMenuExtraSetting
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitClassMenuExtraSettingGet(string AccessKey, String ServiceUnitID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitClassMenuExtraSettingQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnitClassMenuExtraSetting

        #region ServiceUnitClassMenuSetting
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitClassMenuSettingGet(string AccessKey, String ServiceUnitID, String ClassID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitClassMenuSettingQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID, query.ClassID == ClassID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnitClassMenuSetting

        #region ServiceUnitImageTemplate
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitImageTemplateGet(string AccessKey, String ServiceUnitID, String ImageTemplateID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitImageTemplateQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID, query.ImageTemplateID == ImageTemplateID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnitImageTemplate

        #region ServiceUnitItemService
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitItemServiceGet(string AccessKey, String ServiceUnitID, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitItemServiceQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnitItemService

        #region ServiceUnitItemServiceClass
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitItemServiceClassGet(string AccessKey, String ServiceUnitID, String ItemID, String ClassID, String TariffComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitItemServiceClassQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID, query.ItemID == ItemID, query.ClassID == ClassID, query.TariffComponentID == TariffComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnitItemServiceClass

        #region ServiceUnitItemServiceCompMapping
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitItemServiceCompMappingGet(string AccessKey, String ServiceUnitID, String ItemID, String TariffComponentID, String SRRegistrationType, String SRGuarantorIncomeGroup)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitItemServiceCompMappingQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID, query.ItemID == ItemID, query.TariffComponentID == TariffComponentID, query.SRRegistrationType == SRRegistrationType, query.SRGuarantorIncomeGroup == SRGuarantorIncomeGroup);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnitItemServiceCompMapping

        #region ServiceUnitLocation
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitLocationGet(string AccessKey, String ServiceUnitID, String LocationID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitLocationQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID, query.LocationID == LocationID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnitLocation

        #region ServiceUnitParamedic
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitParamedicGet(string AccessKey, String ServiceUnitID, String ParamedicID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitParamedicQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID, query.ParamedicID == ParamedicID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnitParamedic

        #region ServiceUnitProductAccountMapping
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitProductAccountMappingGet(string AccessKey, String LocationId, String ServiceUnitId, String ProductAccountId, String SRRegistrationType)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitProductAccountMappingQuery();
                query.Where(query.LocationId == LocationId, query.ServiceUnitId == ServiceUnitId, query.ProductAccountId == ProductAccountId, query.SRRegistrationType == SRRegistrationType);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnitProductAccountMapping

        #region ServiceUnitQue
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitQueGet(string AccessKey, String ServiceUnitID, String ParamedicID, DateTime QueDate, Int32 QueNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitQueQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID, query.ParamedicID == ParamedicID, query.QueDate == QueDate, query.QueNo == QueNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnitQue

        #region ServiceUnitSchedule
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitScheduleGet(string AccessKey, String ServiceUnitID, Int32 DayOfWeek)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitScheduleQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID, query.DayOfWeek == DayOfWeek);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnitSchedule

        #region ServiceUnitTransactionCode
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitTransactionCodeGet(string AccessKey, String ServiceUnitID, String SRTransactionCode)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitTransactionCodeQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID, query.SRTransactionCode == SRTransactionCode);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnitTransactionCode

        #region ServiceUnitVisitType
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitVisitTypeGet(string AccessKey, String ServiceUnitID, String VisitTypeID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitVisitTypeQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID, query.VisitTypeID == VisitTypeID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnitVisitType

        #region ServiceUnitVitalSign
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ServiceUnitVitalSignGet(string AccessKey, String ServiceUnitID, String VitalSignID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ServiceUnitVitalSignQuery();
                query.Where(query.ServiceUnitID == ServiceUnitID, query.VitalSignID == VitalSignID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ServiceUnitVitalSign

        #region SettingRopHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SettingRopHistoryGet(string AccessKey, Guid RopHistoryID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SettingRopHistoryQuery();
                query.Where(query.RopHistoryID == RopHistoryID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SettingRopHistory

        #region SeveranceTax
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SeveranceTaxGet(string AccessKey, Int32 SeveranceTaxID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SeveranceTaxQuery();
                query.Where(query.SeveranceTaxID == SeveranceTaxID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SeveranceTax

        #region SickLetter
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SickLetterGet(string AccessKey, String RegistrationNo, String ParamedicID, String SRLetterType)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SickLetterQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.ParamedicID == ParamedicID, query.SRLetterType == SRLetterType);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SickLetter

        #region Smf
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SmfGet(string AccessKey, String SmfID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SmfQuery();
                query.Where(query.SmfID == SmfID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Smf

        #region Snack
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SnackGet(string AccessKey, String SnackID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SnackQuery();
                query.Where(query.SnackID == SnackID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Snack

        #region SnackOrder
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SnackOrderGet(string AccessKey, String SnackOrderNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SnackOrderQuery();
                query.Where(query.SnackOrderNo == SnackOrderNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SnackOrder

        #region SnackOrderItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SnackOrderItemGet(string AccessKey, String SnackOrderNo, String SnackID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SnackOrderItemQuery();
                query.Where(query.SnackOrderNo == SnackOrderNo, query.SnackID == SnackID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SnackOrderItem

        #region StandardSalary
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void StandardSalaryGet(string AccessKey, Int32 StandardSalaryID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new StandardSalaryQuery();
                query.Where(query.StandardSalaryID == StandardSalaryID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion StandardSalary

        #region StandardSalaryFaktor
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void StandardSalaryFaktorGet(string AccessKey, Int32 StandardSalaryFaktorID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new StandardSalaryFaktorQuery();
                query.Where(query.StandardSalaryFaktorID == StandardSalaryFaktorID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion StandardSalaryFaktor

        #region StandartSelectionProses
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void StandartSelectionProsesGet(string AccessKey, Int32 StandartSelectionProsesId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new StandartSelectionProsesQuery();
                query.Where(query.StandartSelectionProsesId == StandartSelectionProsesId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion StandartSelectionProses

        #region StructuralBenefits
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void StructuralBenefitsGet(string AccessKey, Int32 OrganizationUnitID, Int32 PositionID, DateTime ValidFrom)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new StructuralBenefitsQuery();
                query.Where(query.OrganizationUnitID == OrganizationUnitID, query.PositionID == PositionID, query.ValidFrom == ValidFrom);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion StructuralBenefits

        #region SubLedgerBalances
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SubLedgerBalancesGet(string AccessKey, Int32 SubLedgerBalanceId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SubLedgerBalancesQuery();
                query.Where(query.SubLedgerBalanceId == SubLedgerBalanceId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SubLedgerBalances

        #region SubLedgerGroups
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SubLedgerGroupsGet(string AccessKey, Int32 SubLedgerGroupId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SubLedgerGroupsQuery();
                query.Where(query.SubLedgerGroupId == SubLedgerGroupId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SubLedgerGroups

        #region SubLedgers
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SubLedgersGet(string AccessKey, Int32 SubLedgerId)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SubLedgersQuery();
                query.Where(query.SubLedgerId == SubLedgerId);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SubLedgers

        #region SubSpecialty
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SubSpecialtyGet(string AccessKey, String SubSpecialtyID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SubSpecialtyQuery();
                query.Where(query.SubSpecialtyID == SubSpecialtyID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SubSpecialty

        #region Supplier
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SupplierGet(string AccessKey, String SupplierID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SupplierQuery();
                query.Where(query.SupplierID == SupplierID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Supplier

        #region SupplierBank
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SupplierBankGet(string AccessKey, String SupplierID, String BankAccountNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SupplierBankQuery();
                query.Where(query.SupplierID == SupplierID, query.BankAccountNo == BankAccountNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SupplierBank

        #region SupplierContract
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SupplierContractGet(string AccessKey, String TransactionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SupplierContractQuery();
                query.Where(query.TransactionNo == TransactionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SupplierContract

        #region SupplierContractItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SupplierContractItemGet(string AccessKey, String TransactionNo, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SupplierContractItemQuery();
                query.Where(query.TransactionNo == TransactionNo, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SupplierContractItem

        #region SupplierFabric
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SupplierFabricGet(string AccessKey, String FabricID, String SupplierID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SupplierFabricQuery();
                query.Where(query.FabricID == FabricID, query.SupplierID == SupplierID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SupplierFabric

        #region SupplierItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SupplierItemGet(string AccessKey, String SupplierID, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SupplierItemQuery();
                query.Where(query.SupplierID == SupplierID, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SupplierItem

        #region SupplierLocation
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SupplierLocationGet(string AccessKey, String SupplierID, String LocationID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SupplierLocationQuery();
                query.Where(query.SupplierID == SupplierID, query.LocationID == LocationID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SupplierLocation

        #region SurgicalPackage
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SurgicalPackageGet(string AccessKey, String PackageID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new SurgicalPackageQuery();
                query.Where(query.PackageID == PackageID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion SurgicalPackage

        #region TariffComponent
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TariffComponentGet(string AccessKey, String TariffComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TariffComponentQuery();
                query.Where(query.TariffComponentID == TariffComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TariffComponent

        #region TestResult
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TestResultGet(string AccessKey, String TransactionNo, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TestResultQuery();
                query.Where(query.TransactionNo == TransactionNo, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TestResult

        #region TestResultTemplate
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TestResultTemplateGet(string AccessKey, Int64 TestResultTemplateID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TestResultTemplateQuery();
                query.Where(query.TestResultTemplateID == TestResultTemplateID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TestResultTemplate

        #region Therapy
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TherapyGet(string AccessKey, String TherapyID, String SRTherapyGroup)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TherapyQuery();
                query.Where(query.TherapyID == TherapyID, query.SRTherapyGroup == SRTherapyGroup);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion Therapy

        #region TmpItemRequestMaintenance
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TmpItemRequestMaintenanceGet(string AccessKey, String UserID, DateTime TransDate, String ToServiceUnitID, String FollowUpID, String TransactionNo, String SequenceNo, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TmpItemRequestMaintenanceQuery();
                query.Where(query.UserID == UserID, query.TransDate == TransDate, query.ToServiceUnitID == ToServiceUnitID, query.FollowUpID == FollowUpID, query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TmpItemRequestMaintenance

        #region TransCharges
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransChargesGet(string AccessKey, String TransactionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransChargesQuery();
                query.Where(query.TransactionNo == TransactionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransCharges

        #region TransChargesHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransChargesHistoryGet(string AccessKey, String RecalculationProcessNo, String TransactionNo, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransChargesHistoryQuery();
                query.Where(query.RecalculationProcessNo == RecalculationProcessNo, query.TransactionNo == TransactionNo, query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransChargesHistory

        #region TransChargesItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransChargesItemGet(string AccessKey, String TransactionNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransChargesItemQuery();
                query.Where(query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransChargesItem

        #region TransChargesItemComp
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransChargesItemCompGet(string AccessKey, String TransactionNo, String SequenceNo, String TariffComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransChargesItemCompQuery();
                query.Where(query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo, query.TariffComponentID == TariffComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransChargesItemComp

        #region TransChargesItemCompHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransChargesItemCompHistoryGet(string AccessKey, String RecalculationProcessNo, String TransactionNo, String SequenceNo, String TariffComponentID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransChargesItemCompHistoryQuery();
                query.Where(query.RecalculationProcessNo == RecalculationProcessNo, query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo, query.TariffComponentID == TariffComponentID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransChargesItemCompHistory

        #region TransChargesItemCompTempPaymentReturn
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransChargesItemCompTempPaymentReturnGet(string AccessKey, String TransactionNo, String SequenceNo, String TariffComponentID, String IntermBillNo, String PaymentNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransChargesItemCompTempPaymentReturnQuery();
                query.Where(query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo, query.TariffComponentID == TariffComponentID, query.IntermBillNo == IntermBillNo, query.PaymentNo == PaymentNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransChargesItemCompTempPaymentReturn

        #region TransChargesItemConsumption
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransChargesItemConsumptionGet(string AccessKey, String TransactionNo, String SequenceNo, String DetailItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransChargesItemConsumptionQuery();
                query.Where(query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo, query.DetailItemID == DetailItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransChargesItemConsumption

        #region TransChargesItemFilmConsumption
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransChargesItemFilmConsumptionGet(string AccessKey, String TransactionNo, String SequenceNo, String SRFilmID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransChargesItemFilmConsumptionQuery();
                query.Where(query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo, query.SRFilmID == SRFilmID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransChargesItemFilmConsumption

        #region TransChargesItemHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransChargesItemHistoryGet(string AccessKey, String RecalculationProcessNo, String TransactionNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransChargesItemHistoryQuery();
                query.Where(query.RecalculationProcessNo == RecalculationProcessNo, query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransChargesItemHistory

        #region TransChargesItemTempCoverage
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransChargesItemTempCoverageGet(string AccessKey, String RegistrationNo, String TransactionNo, String SequenceNo, String ChargeClassID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransChargesItemTempCoverageQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo, query.ChargeClassID == ChargeClassID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransChargesItemTempCoverage

        #region TransChargesItemTempPaymentReturn
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransChargesItemTempPaymentReturnGet(string AccessKey, String TransactionNo, String SequenceNo, String IntermBillNo, String PaymentNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransChargesItemTempPaymentReturnQuery();
                query.Where(query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo, query.IntermBillNo == IntermBillNo, query.PaymentNo == PaymentNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransChargesItemTempPaymentReturn

        #region TransChargesVisiteItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransChargesVisiteItemGet(string AccessKey, String TransactionNo, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransChargesVisiteItemQuery();
                query.Where(query.TransactionNo == TransactionNo, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransChargesVisiteItem

        #region TransChargesVisiteItemRealization
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransChargesVisiteItemRealizationGet(string AccessKey, String TransactionNo, String ItemID, String RegistrationNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransChargesVisiteItemRealizationQuery();
                query.Where(query.TransactionNo == TransactionNo, query.ItemID == ItemID, query.RegistrationNo == RegistrationNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransChargesVisiteItemRealization

        #region TransPayment
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPaymentGet(string AccessKey, String PaymentNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPaymentQuery();
                query.Where(query.PaymentNo == PaymentNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPayment

        #region TransPaymentCorrection
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPaymentCorrectionGet(string AccessKey, String PaymentCorrectionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPaymentCorrectionQuery();
                query.Where(query.PaymentCorrectionNo == PaymentCorrectionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPaymentCorrection

        #region TransPaymentItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPaymentItemGet(string AccessKey, String PaymentNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPaymentItemQuery();
                query.Where(query.PaymentNo == PaymentNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPaymentItem

        #region TransPaymentItemCorrection
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPaymentItemCorrectionGet(string AccessKey, String PaymentCorrectionNo, String PaymentNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPaymentItemCorrectionQuery();
                query.Where(query.PaymentCorrectionNo == PaymentCorrectionNo, query.PaymentNo == PaymentNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPaymentItemCorrection

        #region TransPaymentItemIntermBill
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPaymentItemIntermBillGet(string AccessKey, String PaymentNo, String IntermBillNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPaymentItemIntermBillQuery();
                query.Where(query.PaymentNo == PaymentNo, query.IntermBillNo == IntermBillNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPaymentItemIntermBill

        #region TransPaymentItemIntermBillGuarantor
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPaymentItemIntermBillGuarantorGet(string AccessKey, String PaymentNo, String IntermBillNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPaymentItemIntermBillGuarantorQuery();
                query.Where(query.PaymentNo == PaymentNo, query.IntermBillNo == IntermBillNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPaymentItemIntermBillGuarantor

        #region TransPaymentItemOrder
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPaymentItemOrderGet(string AccessKey, String PaymentNo, String TransactionNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPaymentItemOrderQuery();
                query.Where(query.PaymentNo == PaymentNo, query.TransactionNo == TransactionNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPaymentItemOrder

        #region TransPaymentItemVisite
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPaymentItemVisiteGet(string AccessKey, String PaymentNo, String PatientID, String ItemID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPaymentItemVisiteQuery();
                query.Where(query.PaymentNo == PaymentNo, query.PatientID == PatientID, query.ItemID == ItemID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPaymentItemVisite

        #region TransPaymentPatient
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPaymentPatientGet(string AccessKey, String PaymentNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPaymentPatientQuery();
                query.Where(query.PaymentNo == PaymentNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPaymentPatient

        #region TransPaymentPatientItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPaymentPatientItemGet(string AccessKey, String PaymentNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPaymentPatientItemQuery();
                query.Where(query.PaymentNo == PaymentNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPaymentPatientItem

        #region TransPaymentReceipt
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPaymentReceiptGet(string AccessKey, String PaymentReceiptNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPaymentReceiptQuery();
                query.Where(query.PaymentReceiptNo == PaymentReceiptNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPaymentReceipt

        #region TransPaymentReceiptItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPaymentReceiptItemGet(string AccessKey, String PaymentReceiptNo, String PaymentNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPaymentReceiptItemQuery();
                query.Where(query.PaymentReceiptNo == PaymentReceiptNo, query.PaymentNo == PaymentNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPaymentReceiptItem

        #region TransPrescription
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPrescriptionGet(string AccessKey, String PrescriptionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPrescriptionQuery();
                query.Where(query.PrescriptionNo == PrescriptionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPrescription

        #region TransPrescriptionFloorSeqNo
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPrescriptionFloorSeqNoGet(string AccessKey, DateTime PrescriptionDate, String SRFloor, String ServiceUnitID, String ShiftID, String Rtype)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPrescriptionFloorSeqNoQuery();
                query.Where(query.PrescriptionDate == PrescriptionDate, query.SRFloor == SRFloor, query.ServiceUnitID == ServiceUnitID, query.ShiftID == ShiftID, query.Rtype == Rtype);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPrescriptionFloorSeqNo

        #region TransPrescriptionHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPrescriptionHistoryGet(string AccessKey, String RecalculationProcessNo, String PrescriptionNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPrescriptionHistoryQuery();
                query.Where(query.RecalculationProcessNo == RecalculationProcessNo, query.PrescriptionNo == PrescriptionNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPrescriptionHistory

        #region TransPrescriptionItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPrescriptionItemGet(string AccessKey, String PrescriptionNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPrescriptionItemQuery();
                query.Where(query.PrescriptionNo == PrescriptionNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPrescriptionItem

        #region TransPrescriptionItemEtiquette
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPrescriptionItemEtiquetteGet(string AccessKey, String PrescriptionNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPrescriptionItemEtiquetteQuery();
                query.Where(query.PrescriptionNo == PrescriptionNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPrescriptionItemEtiquette

        #region TransPrescriptionItemHistory
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPrescriptionItemHistoryGet(string AccessKey, String RecalculationProcessNo, String PrescriptionNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPrescriptionItemHistoryQuery();
                query.Where(query.RecalculationProcessNo == RecalculationProcessNo, query.PrescriptionNo == PrescriptionNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPrescriptionItemHistory

        #region TransPrescriptionItemTempCoverage
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPrescriptionItemTempCoverageGet(string AccessKey, String RegistrationNo, String PrescriptionNo, String SequenceNo, String ChargeClassID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPrescriptionItemTempCoverageQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.PrescriptionNo == PrescriptionNo, query.SequenceNo == SequenceNo, query.ChargeClassID == ChargeClassID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPrescriptionItemTempCoverage

        #region TransPrescriptionItemTemplate
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPrescriptionItemTemplateGet(string AccessKey, String TemplateNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPrescriptionItemTemplateQuery();
                query.Where(query.TemplateNo == TemplateNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPrescriptionItemTemplate

        #region TransPrescriptionItemTempPaymentReturn
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPrescriptionItemTempPaymentReturnGet(string AccessKey, String Prescription, String SequenceNo, String IntermBillNo, String PaymentNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPrescriptionItemTempPaymentReturnQuery();
                query.Where(query.Prescription == Prescription, query.SequenceNo == SequenceNo, query.IntermBillNo == IntermBillNo, query.PaymentNo == PaymentNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPrescriptionItemTempPaymentReturn

        #region TransPrescriptionItemUnitDose
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TransPrescriptionItemUnitDoseGet(string AccessKey, String PrescriptionNo, String SequenceNo)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TransPrescriptionItemUnitDoseQuery();
                query.Where(query.PrescriptionNo == PrescriptionNo, query.SequenceNo == SequenceNo);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TransPrescriptionItemUnitDose

        #region TreatmentForAnimalBites
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TreatmentForAnimalBitesGet(string AccessKey, String RegistrationNo, String SRTreatmentForAnimalBites)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new TreatmentForAnimalBitesQuery();
                query.Where(query.RegistrationNo == RegistrationNo, query.SRTreatmentForAnimalBites == SRTreatmentForAnimalBites);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion TreatmentForAnimalBites

        #region UserHostPrinter
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void UserHostPrinterGet(string AccessKey, String UserHostName)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new UserHostPrinterQuery();
                query.Where(query.UserHostName == UserHostName);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion UserHostPrinter

        #region UserHostPrinterOther
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void UserHostPrinterOtherGet(string AccessKey, String UserHostName, String ProgramID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new UserHostPrinterOtherQuery();
                query.Where(query.UserHostName == UserHostName, query.ProgramID == ProgramID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion UserHostPrinterOther

        #region UserLog
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void UserLogGet(string AccessKey, Int64 UserLogID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new UserLogQuery();
                query.Where(query.UserLogID == UserLogID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion UserLog

        #region UserProgramLog
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void UserProgramLogGet(string AccessKey, Int64 UserProgramLogID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new UserProgramLogQuery();
                query.Where(query.UserProgramLogID == UserProgramLogID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion UserProgramLog

        #region VisitType
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VisitTypeGet(string AccessKey, String VisitTypeID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new VisitTypeQuery();
                query.Where(query.VisitTypeID == VisitTypeID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion VisitType

        #region VitalSign
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VitalSignGet(string AccessKey, String VitalSignID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new VitalSignQuery();
                query.Where(query.VitalSignID == VitalSignID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion VitalSign

        #region VitalSignEws
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VitalSignEwsGet(string AccessKey, String VitalSignID, Int32 StartAgeInDay)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new VitalSignEwsQuery();
                query.Where(query.VitalSignID == VitalSignID, query.StartAgeInDay == StartAgeInDay);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion VitalSignEws

        #region VitalSignEwsLevel
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void VitalSignEwsLevelGet(string AccessKey, String VitalSignID, Int32 StartAgeInDay, Decimal StartValue)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new VitalSignEwsLevelQuery();
                query.Where(query.VitalSignID == VitalSignID, query.StartAgeInDay == StartAgeInDay, query.StartValue == StartValue);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion VitalSignEwsLevel

        #region WageTransaction
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void WageTransactionGet(string AccessKey, Int64 WageTransactionID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new WageTransactionQuery();
                query.Where(query.WageTransactionID == WageTransactionID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion WageTransaction

        #region WageTransactionItem
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void WageTransactionItemGet(string AccessKey, Int64 WageTransactionItemID, Int64 WageTransactionID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new WageTransactionItemQuery();
                query.Where(query.WageTransactionItemID == WageTransactionItemID, query.WageTransactionID == WageTransactionID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion WageTransactionItem

        #region WebServiceAccessKey
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void WebServiceAccessKeyGet(string AccessKey, String ClientCode)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new WebServiceAccessKeyQuery();
                query.Where(query.ClientCode == ClientCode);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion WebServiceAccessKey

        #region WebServiceAPILog
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void WebServiceAPILogGet(string AccessKey, Int32 ID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new WebServiceAPILogQuery();
                query.Where(query.ID == ID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion WebServiceAPILog

        #region ZatActive
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ZatActiveGet(string AccessKey, String ZatActiveID)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ZatActiveQuery();
                query.Where(query.ZatActiveID == ZatActiveID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ZatActive

        #region ZipCode
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ZipCodeGet(string AccessKey, String ZipCode)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                var query = new ZipCodeQuery();
                query.Where(query.ZipCode == ZipCode);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);

                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
        #endregion ZipCode

        #endregion
    }
}

