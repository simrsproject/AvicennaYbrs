using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using System.Configuration;
using System.Linq;

namespace Temiang.Avicenna.Common
{
    public class AppSession
    {
        public static UserLogin UserLogin
        {
            get
            {
                object obj = HttpContext.Current.Session["_UserLogin"];
                if (obj == null)
                {
                    var user = new UserLogin();
                    HttpContext.Current.Session["_UserLogin"] = user;
                    return user;
                }

                return (UserLogin)obj;
            }
            set
            {
                //object obj = HttpContext.Current.Session["_UserLogin"];
                //if (obj != null)
                //{
                //    if (value.UserLogID != null && ((UserLogin)obj).UserLogID != value.UserLogID)
                //    {
                //        HttpContext.Current.Session["_UserLogin"] = value;
                //        return;
                //    }
                //}

                // Jika ada UserLogID nya maka berasal dari pindah aplikasi dan jangan buat log baru
                if (value.UserLogID == null)
                {
                    value.UserLogID = CreateUserLog(value);
                }
                HttpContext.Current.Session["_UserLogin"] = value;
            }
        }

        private static Int64? CreateUserLog(UserLogin value)
        {
            // Create Log
            var userLog = new UserLog
            {
                ApplicationID = ApplicationSettings.DefaultApplication == null ? string.Empty : ApplicationSettings.DefaultApplication.Name,
                SessionID = HttpContext.Current.Session.SessionID,
                UserID = value.UserID,
                LoginDateTime = DateTime.Now,
                ClientIP = value.UserHostName,
                BrowserInfo = Helper.GetBrowserInfo()
            };
            userLog.Save();
            return userLog.UserLogID;
        }

        #region Report & Pivot Parameter
        public static string PrintCustomPivotID
        {
            get
            {
                object obj = HttpContext.Current.Session["_CustomPivotID"];
                if (obj == null)
                    HttpContext.Current.Session["_CustomPivotID"] = "";

                return (string)HttpContext.Current.Session["_CustomPivotID"];
            }
            set { HttpContext.Current.Session["_CustomPivotID"] = value; }
        }

        public static string PrintJobReportID
        {
            get
            {
                object obj = HttpContext.Current.Session["_ReportID"];
                if (obj == null)
                    HttpContext.Current.Session["_ReportID"] = "";

                return (string)HttpContext.Current.Session["_ReportID"];
            }
            set { HttpContext.Current.Session["_ReportID"] = value; }
        }

        public static bool PrintShowToolBarPrint
        {
            get
            {
                object obj = HttpContext.Current.Session["_ToolBarPrint"];
                if (obj == null)
                    HttpContext.Current.Session["_ToolBarPrint"] = true;

                return (bool)HttpContext.Current.Session["_ToolBarPrint"];
            }
            set { HttpContext.Current.Session["_ToolBarPrint"] = value; }
        }
        public static PrintJobParameterCollection PrintJobParameters
        {
            get
            {
                object obj = HttpContext.Current.Session["_PrintJobParameters"];
                if (obj == null)
                    return null;
                return (PrintJobParameterCollection)HttpContext.Current.Session["_PrintJobParameters"];
            }
            set { HttpContext.Current.Session["_PrintJobParameters"] = value; }
        }
        #endregion

        public static Exception LastErrorException
        {
            get
            {
                object obj = HttpContext.Current.Session["_LastErrorException"];
                if (obj == null)
                    return null;
                return (Exception)obj;
            }
            set
            {
                if (HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session["_LastErrorException"] = value;
                }
            }
        }

        public class Parameter
        {
            #region common function
            public static void ClearParameterCache()
            {
                var parCaches = new List<string>();
                foreach (DictionaryEntry cache in HttpContext.Current.Cache)
                {
                    var key = cache.Key.ToString();
                    if (key.Contains("p_"))
                        parCaches.Add(key);
                }

                foreach (var key in parCaches)
                {
                    HttpContext.Current.Cache.Remove(key);
                }
            }
            public static void SetParameterValue(string parameterID, object value)
            {
                if (parameterID.ToLower().Contains("coa"))
                {
                    ChartOfAccounts coa = ChartOfAccounts.Get(value.ToString());
                    if (coa != null)
                    {
                        var retVal = coa.ChartOfAccountId.Value;

                        System.Web.HttpContext.Current.Cache["par_" + parameterID] = retVal;
                        HttpContext.Current.Cache["par_" + parameterID] = retVal;
                    }
                    else
                    {
                        HttpContext.Current.Cache["par_" + parameterID] = value;
                    }
                }
                else
                {
                    HttpContext.Current.Cache["par_" + parameterID] = value;
                }
            }

            public static object GetParameterValue(AppParameter.ParameterItem parameterItem)
            {
                string parameterID = Enum.GetName(typeof(AppParameter.ParameterItem), parameterItem);
                object obj = HttpContext.Current.Cache["par_" + parameterID];
                if (obj == null)
                {
                    obj = AppParameter.GetParameterValue(parameterItem);
                    SetParameterValue(parameterID, obj);
                }

                return obj;
            }

            public static void Load(string parameterID)
            {
                var qr = new AppParameterQuery("a");
                qr.Select(qr.ParameterValue);
                qr.es.Top = 1;
                qr.Where(qr.ParameterID == parameterID);
                var result = qr.ExecuteScalar();
                if (result != null)
                    SetParameterValue(parameterID, result.ToString());
            }

            public static string GetParameterValueString(AppParameter.ParameterItem parameterItem)
            {
                var val = GetParameterValue(parameterItem);
                return val == null ? string.Empty : val.ToString();
            }
            public static object GetParameterNullableValue(AppParameter.ParameterItem parameterItem)
            {
                string parameterID = Enum.GetName(typeof(AppParameter.ParameterItem), parameterItem);
                object obj = HttpContext.Current.Cache["par_" + parameterID];
                if (obj == null)
                {
                    obj = AppParameter.GetParameterNullableValue(parameterItem);
                    SetParameterValue(parameterID, obj ?? -1);
                }

                return obj;
            }

            public static bool IsYes(AppParameter.ParameterItem parameterItem)
            {
                return AppParameter.IsYes(GetParameterValue(parameterItem).ToString());
            }
            #endregion

            #region PCare
            public static string GuarantorTypeBpjsKapitasi
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.GuarantorTypeBpjsKapitasi)); }
            }
            #endregion

            public static bool IsMedicalNoContainStrip => IsYes(AppParameter.ParameterItem.IsMedicalNoContainStrip);

            public static string GuarantorTypeBpjs => Convert.ToString(GetParameterValue(AppParameter.ParameterItem.GuarantorTypeBpjs));

            public static string[] CasemixValidationRegistrationType
            {
                get { return GetParameterValue(AppParameter.ParameterItem.CasemixValidationRegistrationType).ToString().Split(','); }
            }

            public static string[] ClinicalPathwayRegistrationType
            {
                get { return GetParameterValue(AppParameter.ParameterItem.ClinicalPathwayRegistrationType).ToString().Split(','); }
            }

            public static bool IsEmrPhysicianAssessmentMandatory
            {
                get { return IsYes(AppParameter.ParameterItem.IsEmrPhysicianAssessmentMandatory); }
            }


            public static bool IsShowSearchMenu => IsYes(AppParameter.ParameterItem.IsShowSearchMenu);

            public static bool IsPrescriptionOnlyInStock
            {
                get { return IsYes(AppParameter.ParameterItem.IsPrescriptionOnlyInStock); }
            }

            public static string TransferOrderServiceUnitID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.TransferOrderServiceUnitID)); }
            }

            public static string PaymentTypeReturn
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentTypeReturn)); }
            }

            public static string PaymentTypeDownPayment
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentTypeDownPayment)); }
            }

            public static string PaymentTypePayment
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentTypePayment)); }
            }

            public static string PaymentTypePaymentName
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentTypePaymentName)); }
            }

            public static string PaymentTypePersonalAR
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentTypePersonalAR)); }
            }

            public static string PaymentTypeCorporateAR
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentTypeCorporateAR)); }
            }

            public static string PaymentTypeBackOfficePayment
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentTypeBackOfficePayment)); }
            }

            public static string PaymentTypeSaldoAR
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentTypeSaldoAR)); }
            }

            public static string PaymentTypeDiscount
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentTypeDiscount)); }
            }

            public static string PaymentTypeInvoiceSupplierPayment
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentTypeInvoiceSupplierPayment)); }
            }

            public static string PaymentTypeCredit
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentTypeCredit)); }
            }

            public static string PaymentMethodCash
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentMethodCash)); }
            }

            public static string PaymentMethodBiaya
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentMethodBiaya)); }
            }

            public static string PaymentMethodCashName
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentMethodCashName)); }
            }

            public static string PaymentMethodCreditCard
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentMethodCreditCard)); }
            }

            public static string PaymentMethodTransfer
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentMethodTransfer)); }
            }

            public static string PaymentMethodTransferName
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentMethodTransferName)); }
            }

            public static string PaymentMethodDebitCard
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentMethodDebitCard)); }
            }

            public static string PaymentMethodCashAR
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentMethodCashAR)); }
            }

            public static string PaymentMethodQris
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentMethodQris)); }
            }

            public static int MaxResultRecord
            {
                get
                {
                    object parValue = GetParameterValue(AppParameter.ParameterItem.MaxResultRecord);
                    return Convert.ToInt32(string.IsNullOrEmpty(parValue.ToString()) ? "500" : parValue);
                }
            }
            public static int MaxResultRecordEmrList
            {
                get
                {
                    object parValue = GetParameterValue(AppParameter.ParameterItem.MaxResultRecordEmrList);
                    return Convert.ToInt32(string.IsNullOrEmpty(parValue.ToString()) ? "500" : parValue);
                }
            }
            public static string InPatientDepartmentID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.InPatientDepartmentID)); }
            }

            public static string OutPatientDepartmentID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.OutPatientDepartmentID)); }
            }

            public static string MedicalCheckUpDepartmentID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.MedicalCheckUpDepartmentID)); }
            }

            public static string ClusterPatientDepartmentID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ClusterPatientDepartmentID)); }
            }

            public static string EmergencyDepartmentID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmergencyDepartmentID)); }
            }

            public static string MedicalSupportDepartmentID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.MedicalSupportDepartmentID)); }
            }

            public static string PharmacyDepartmentID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PharmacyDepartmentID)); }
            }

            public static string DiscountValueType
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DiscountValueType)); }
            }

            //Class ID
            public static string OutPatientClassID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.OutPatientClassID)); }
            }

            public static string ClusterPatientClassID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ClusterPatientClassID)); }
            }

            public static string EmergencyPatientClassID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmergencyPatientClassID)); }
            }

            //Bed Status Parameter
            public static string BedStatusUnoccupied
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.BedStatusUnoccupied)); }
            }

            public static string BedStatusPending
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.BedStatusPending)); }
            }

            public static string BedStatusOccupied
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.BedStatusOccupied)); }
            }

            public static string BedStatusGoToOperatingRoom
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.BedStatusGoToOperatingRoom)); }
            }

            public static string BedStatusBooked
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.BedStatusBooked)); }
            }

            public static string BedStatusReserved
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.BedStatusReserved)); }
            }

            public static string BedStatusCleaning
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.BedStatusCleaning)); }
            }

            public static string BedStatusRepaired
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.BedStatusRepaired)); }
            }

            //Appointment Status Parameter
            public static string AppointmentStatusConfirmed
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AppointmentStatusConfirmed)); }
            }

            public static string AppointmentStatusOpen
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AppointmentStatusOpen)); }
            }

            public static string AppointmentStatusClosed
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AppointmentStatusClosed)); }
            }

            public static string AppointmentStatusCancel
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AppointmentStatusCancel)); }
            }

            public static string AntrolPrintLabelOnKiosk
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AntrolPrintLabelOnKiosk)); }
            }

            public static string AppointmentStatusNoResponse
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AppointmentStatusNoResponse)); }
            }
            public static string AppointmentStatusBooked
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AppointmentStatusBooked)); }
            }

            public static string EmployeeStatueResignReference
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmployeeStatueResignReference)); }
            }

            public static string DefaultRetrirementAge
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DefaultRetrirementAge)); }
            }

            //Self Guarantor Parameter
            public static string SelfGuarantor
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.SelfGuarantorID)); }
            }

            public static string RequestOrderServiceUnitID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.RequestOrderServiceUnitID)); }
            }

            public static string GuarantorRuleTypeDiscount
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.GuarantorRuleTypeDiscount)); }
            }

            public static string GuarantorRuleTypeMargin
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.GuarantorRuleTypeMargin)); }
            }

            public static string GuarantorRuleTypePlavon
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.GuarantorRuleTypePlavon)); }
            }

            //BusinessMethod
            public static string BusinessMethodCoverage
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.BusinessMethodCoverage)); }
            }

            public static string BusinessMethodFlavon
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.BusinessMethodFlavon)); }
            }

            public static string BusinessMethodAllIn
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.BusinessMethodAllIn)); }
            }

            //public static string BusinessMethodBpjs
            //{
            //    get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.BusinessMethodBpjs)); }
            //}

            //Rounding
            public static decimal RoundingTransaction
            {
                get { return Convert.ToDecimal(GetParameterValue(AppParameter.ParameterItem.RoundingTransaction)); }
            }

            public static decimal RoundingPayment
            {
                get { return Convert.ToDecimal(GetParameterValue(AppParameter.ParameterItem.RoundingPayment)); }
            }

            public static decimal RoundingPaymentWithCard
            {
                get { return Convert.ToDecimal(GetParameterValue(AppParameter.ParameterItem.RoundingPaymentWithCard)); }
            }


            public static decimal RoundingPrescription
            {
                get { return Convert.ToDecimal(GetParameterValue(AppParameter.ParameterItem.RoundingPrescription)); }
            }

            public static decimal RoundingGlobalTransaction
            {
                get { return Convert.ToDecimal(GetParameterValue(AppParameter.ParameterItem.RoundingGlobalTransaction)); }
            }

            //Physician Type Anesthetic
            public static string PhysicianTypeAnesthetic
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PhysicianTypeAnesthetic)); }
            }

            public static string PhysicianTypeAssistant
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PhysicianTypeAssistant)); }
            }

            public static string PhysicianTypeAssAnesthesia
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PhysicianTypeAssAnesthesia)); }
            }

            public static string PhysicianTypeInstrumentator
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PhysicianTypeInstrumentator)); }
            }

            public static string DrugAllergenGroupID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DrugAllergenGroupID)); }
            }

            public static string FoodAllergenGroupID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.FoodAllergenGroupID)); }
            }

            public static double TaxPercentage
            {
                get { return Convert.ToDouble(GetParameterValue(AppParameter.ParameterItem.TaxPercentage)); }
            }

            public static double Ppn
            {
                get { return Convert.ToDouble(GetParameterValue(AppParameter.ParameterItem.Ppn)); }
            }

            public static string ParamedicTariffComponentID
            {
                get { return GetParameterValue(AppParameter.ParameterItem.ParamedicTariffComponentID).ToString(); }
            }

            //Default Tariff Class
            public static string DefaultTariffClass
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DefaultTariffClass)); }
            }

            public static string DefaultTariffType
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DefaultTariffType)); }
            }

            public static string RadiologyNoFormat
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.RadiologyNoFormat)); }
            }

            [Obsolete("Obsolete, ganti dgn IsRadiologyNoAutoCreate", true)]
            public static string RadiologyNoAutoCreate
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.RadiologyNoAutoCreate)); }
            }
            public static bool IsRadiologyNoAutoCreate
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.RadiologyNoAutoCreate);
                }
            }
            public static string RadiologyParamedicId
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.RadiologyParamedicId)); }
            }

            //AP Payable Status
            public static string PayableStatusClosed
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PayableStatusClosed)); }
            }

            public static string PayableStatusPaid
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PayableStatusPaid)); }
            }

            public static string PayableStatusProcess
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PayableStatusProcess)); }
            }

            public static string PayableStatusVerify
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PayableStatusVerify)); }
            }

            //AR Receivable Status
            public static string ReceivableStatusClosed
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ReceivableStatusClosed)); }
            }

            public static string ReceivableStatusPaid
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ReceivableStatusPaid)); }
            }

            public static string ReceivableStatusProcess
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ReceivableStatusProcess)); }
            }

            public static string ReceivableStatusVerify
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ReceivableStatusVerify)); }
            }

            //Receivable Type
            public static string ReceivableTypeCorporate
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ReceivableTypeCorporate)); }
            }

            public static string ReceivableTypePersonal
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ReceivableTypePersonal)); }
            }

            //Prescription
            public static string OTCPrescriptionPatientID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.OTCPrescriptionPatientID)); }
            }

            public static string ServiceUnitPharmacyID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitPharmacyID)); }
            }

            public static string ServiceUnitPharmacyIdOpr
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitPharmacyIdOpr)); }
            }

            public static string ServiceUnitPharmacyIdPos
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitPharmacyIdPos)); }
            }

            public static string ServiceUnitRadiologyID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitRadiologyID)); }
            }

            public static string ServiceUnitRadiologyID2
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitRadiologyID2)); }
            }
            public static string ServiceUnitRadiologyIDs
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitRadiologyIDs)); }
            }
            public static string ServiceUnitMedicalRehabId
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitMedicalRehabId)); }
            }

            public static string[] ServiceUnitRadiologyIdArray
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitRadiologyIdArray)).Split(','); }
            }

            public static string ServiceUnitKiaId
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitKiaId)); }
            }

            public static string ServiceUnitImmunizationId
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitImmunizationId)); }
            }

            public static string ServiceUnitObstetricsId
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitObstetricsId)); }
            }

            public static string OperatingTheaterClusterID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.OperatingTheaterClusterID)); }
            }

            public static string DefektaSupplierID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DefektaSupplierID)); }
            }

            public static string ServiceUnitLaboratoryID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitLaboratoryID)); }
            }

            public static string[] ServiceUnitLaboratoryIdArray
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitLaboratoryIdArray)).Split(','); }
            }

            public static string ServiceUnitMcuId
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitMcuId)); }
            }

            public static string TariffComponentJasaSaranaID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.TariffComponentJasaSaranaID)); }
            }

            public static string TariffComponentJasaMedisID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.TariffComponentJasaMedisID)); }
            }

            //InvoicePayment
            public static string InvoicePaymentCash
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.InvoicePaymentCash)); }
            }

            public static string InvoicePaymentDiscount
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.InvoicePaymentDiscount)); }
            }

            public static string InvoicePaymentGiro
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.InvoicePaymentGiro)); }
            }

            public static string OrderResultFolderPath
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.OrderResultFolderPath)); }
            }

            public static string FinanceHead
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.FinanceHead)); }
            }

            public static string FinanceHeadJob
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.FinanceHeadJob)); }
            }

            public static string FinanceHeadID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.FinanceHeadID)); }
            }

            public static decimal? PrescriptionReturnAdminValue
            {
                get { return Convert.ToDecimal(GetParameterValue(AppParameter.ParameterItem.PrescriptionReturnAdminValue)); }
            }

            public static string GuarantorTypeMemberID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.GuarantorTypeMemberID)); }
            }

            public static string InventoryHeadOfficer
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.InventoryHeadOfficer)); }
            }

            public static string InvoiceTermOfPayment
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.InvoiceTermOfPayment)); }
            }

            public static string OpticDepartmentID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.OpticDepartmentID)); }
            }

            public static string CompleteStatusRM
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.CompleteStatusRM)); }
            }

            public static string MedicalFileCategoryIn
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.MedicalFileCategoryIn)); }
            }

            public static string MedicalFileCategoryOut
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.MedicalFileCategoryOut)); }
            }

            public static string MedicalFileStatusConfirm
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.MedicalFileStatusConfirm)); }
            }

            public static string MedicalFileStatusRequest
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.MedicalFileStatusRequest)); }
            }

            public static double RentalRoomsPercentage
            {
                get { return Convert.ToDouble(GetParameterValue(AppParameter.ParameterItem.RentalRoomsPercentage)); }
            }

            //public static string ItemService
            //{
            //    get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ItemService)); }
            //}

            //public static string ItemProductMedic
            //{
            //    get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ItemProductMedic)); }
            //}

            //public static string ItemProductNonMedic
            //{
            //    get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ItemProductNonMedic)); }
            //}

            //public static string ItemRadiology
            //{
            //    get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ItemRadiology)); }
            //}

            //public static string ItemLaboratory
            //{
            //    get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ItemLaboratory)); }
            //}

            public static string ItemGroupMaterai
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ItemGroupMaterai)); }
            }

            public static string PaymentMethodPackageBalance
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PaymentMethodPackageBalance)); }
            }

            public static string HealthcareID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.HealthcareID)); }
            }

            //public static string IsRegistrationPrintAutomatic
            //{
            //    get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.IsRegistrationPrintAutomatic)); }
            //}
            public static bool IsRegistrationPrintAutomatic
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsRegistrationPrintAutomatic);
                }
            }
            public static string RegistrationSlipRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.RegistrationSlipRpt)); }
            }

            public static string RegistrationSlipKioskRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.RegistrationSlipKioskRpt)); }
            }

            public static string RegistrationInpatientIdentityRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.RegistrationInpatientIdentityRpt)); }
            }

            public static string RegistrationLabelRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.RegistrationLabelRpt)); }
            }

            public static string RegistrationLabelOpRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.RegistrationLabelOpRpt)); }
            }

            public static string RegistrationLabelEmRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.RegistrationLabelEmRpt)); }
            }

            public static string RegistrationLabelNewPatientRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.RegistrationLabelNewPatientRpt)); }
            }

            public static string EmployeeMedicalInsuranceFormRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmployeeMedicalInsuranceFormRpt)); }
            }

            public static string EmployeeMaritalStatusForMedicalInsurance
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmployeeMaritalStatusForMedicalInsurance)); }
            }

            public static bool IsVisibleEmployeeMedicalInsuranceForm
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsVisibleEmployeeMedicalInsuranceForm);
                }
            }

            public static string RecruitmentTestInterview
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.RecruitmentTestInterview)); }
            }

            public static string DefaultSurgeryTime
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DefaultSurgeryTime)); }
            }

            public static string OperatingRoomBookingLimit
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.OperatingRoomBookingLimit)); }
            }

            public static string TracerRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.TracerRpt)); }
            }

            public static string TracerOpRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.TracerOpRpt)); }
            }

            public static string TracerErRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.TracerErRpt)); }
            }

            public static string PatientIdCardRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PatientIdCardRpt)); }
            }

            public static string EmployeeClinicalAssignmentLetterKomedRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmployeeClinicalAssignmentLetterKomedRpt)); }
            }

            public static string EmployeeClinicalAssignmentLetterKomkepRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmployeeClinicalAssignmentLetterKomkepRpt)); }
            }

            public static string EmployeeClinicalAssignmentLetterKtklRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmployeeClinicalAssignmentLetterKtklRpt)); }
            }

            public static string InPatientServiceUnitID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.InPatientServiceUnitID)); }
            }

            public static string PatientCardItemID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PatientCardItemID)); }
            }

            public static string ParamedicTeamStatusMain
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ParamedicTeamStatusMain)); }
            }

            //HRD & Payroll
            public static string ReimbursementFactorUnlimit
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ReimbursementFactorUnlimit)); }
            }

            public static string ReimbursementFactorNominal
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ReimbursementFactorNominal)); }
            }

            public static string ReimbursementFactorBasicFactor
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ReimbursementFactorBasicFactor)); }
            }

            public static string ReimbursementFactorCharacteristics
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ReimbursementFactorCharacteristics)); }
            }

            public static string DependentIncludeEmployee
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DependentIncludeEmployee)); }
            }

            public static string UnusedBalanceCarryOver
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.UnusedBalanceCarryOver)); }
            }

            public static bool IsUppercasePatientID
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUppercasePatientID);
                }
            }

            public static bool IsRegistrationPrintLabel
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsRegistrationPrintLabel);
                }
            }

            public static bool IsRegistrationOpPrintLabel
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsRegistrationOpPrintLabel);
                }
            }

            public static bool IsRegistrationEmPrintLabel
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsRegistrationEmPrintLabel);
                }
            }

            public static bool IsRegistrationPrintLabelNewPatient
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsRegistrationPrintLabelNewPatient);
                }
            }

            public static bool IsRegistrationPrintSlip
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsRegistrationPrintSlip);
                }
            }

            public static bool IsRegistrationIdentity
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsRegistrationIdentity);
                }
            }

            public static bool IsRegistrationPrintReceipt
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsRegistrationPrintReceipt);
                }
            }

            public static bool IsRegistrationPrintTicket
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsRegistrationPrintTicket);
                }
            }

            public static bool IsRegistrationMcuPrintSlip
            {
                get { return IsYes(AppParameter.ParameterItem.IsRegistrationMcuPrintSlip); }
            }
            public static bool IsRegistrationMcuPrintTicket
            {
                get { return IsYes(AppParameter.ParameterItem.IsRegistrationMcuPrintTicket); }
            }
            public static bool IsRegistrationMcuPrintLabel
            {
                get { return IsYes(AppParameter.ParameterItem.IsRegistrationMcuPrintLabel); }
            }

            public static string RegistrationSlipMcuRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.RegistrationSlipMcuRpt)); }
            }
            public static string RegistrationTicketMcuRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.RegistrationTicketMcuRpt)); }
            }
            public static string RegistrationLabelMcuRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.RegistrationLabelMcuRpt)); }
            }

            public static bool IsRegistrationTracer
            {
                get { return IsYes(AppParameter.ParameterItem.IsRegistrationTracer); }
            }

            public static bool IsRegistrationTracerToAllReg
            {
                get { return IsYes(AppParameter.ParameterItem.IsRegistrationTracerToAllReg); }
            }

            public static bool IsRegistrationTracerToAllRegType
            {
                get { return IsYes(AppParameter.ParameterItem.IsRegistrationTracerToAllRegType); }
            }

            public static string RegistrationTracerToAllRegTypeExc
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.RegistrationTracerToAllRegTypeExc)); }
            }

            public static string RegistrationReceiptRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.RegistrationReceiptRpt)); }
            }

            public static string RegistrationTicketRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.RegistrationTicketRpt)); }
            }

            public static bool IsRegistrationPrintPatientIdCard
            {
                get { return IsYes(AppParameter.ParameterItem.IsRegistrationPrintPatientIdCard); }
            }

            public static bool IsSelfCheckinPrintingSEP
            {
                get { return IsYes(AppParameter.ParameterItem.IsSelfCheckinPrintingSEP); }
            }
            public static string SepProgramIdRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.SepProgramIdRpt)); }
            }


            public static bool IsAutoClosedRegOnDischargePermit
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutoClosedRegOnDischargePermit); }
            }

            public static bool IsAutoClosedRegOpOnPayment
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutoClosedRegOpOnPayment); }
            }

            public static bool IsAutoClosedRegIpOnPayment
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutoClosedRegIpOnPayment); }
            }

            public static bool TariffComponentPriceVisible
            {
                get { return Convert.ToBoolean(Convert.ToInt16(GetParameterValue(AppParameter.ParameterItem.TariffComponentPriceVisible))); }
            }

            public static bool IsTariffComponentPriceVisibleForBilling
            {
                get { return IsYes(AppParameter.ParameterItem.IsTariffComponentPriceVisibleForBilling); }
            }

            public static bool RegistrationCanChangeBedNo
            {
                get { return Convert.ToBoolean(Convert.ToInt16(GetParameterValue(AppParameter.ParameterItem.RegistrationCanChangeBedNo))); }
            }

            public static bool IsAllowMultipleRegOp
            {
                get { return IsYes(AppParameter.ParameterItem.IsAllowMultipleRegOp); }
            }

            public static bool IsAllowMultipleRegOpWithSameUnitAndPhysician
            {
                get { return IsYes(AppParameter.ParameterItem.IsAllowMultipleRegOpWithSameUnitAndPhysician); }
            }

            public static bool IsReferPatientUsingClassBefore
            {
                get { return IsYes(AppParameter.ParameterItem.IsReferPatientUsingClassBefore); }
            }

            public static string PatientInTypeIp
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PatientInTypeIp)); }
            }

            public static string PatientInTypeOp
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PatientInTypeOp)); }
            }

            public static string PatientInTypeEr
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PatientInTypeEr)); }
            }

            public static decimal RecipeMarginValueCompound
            {
                get
                {
                    decimal value;
                    var param = (string)GetParameterValue(AppParameter.ParameterItem.RecipeMarginValueCompound);
                    return decimal.TryParse(param, out value) ? Convert.ToDecimal(param) : 0;
                }
            }

            public static decimal RecipeMarginValueNonCompound
            {
                get
                {
                    decimal value;
                    var param = (string)GetParameterValue(AppParameter.ParameterItem.RecipeMarginValueNonCompound);
                    return decimal.TryParse(param, out value) ? Convert.ToDecimal(param) : 0;
                }
            }

            public static bool IsAutoRefreshEhrList
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAutoRefreshEhrList);
                }
            }
            public static bool IsAutoPrintPrescriptionOrder
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAutoPrintPrescriptionOrder);
                }
            }
            public static int? MinDayBeforeBookingMJkn
            {
                get
                {
                    var value = GetParameterNullableValue(AppParameter.ParameterItem.MinDayBeforeBookingMJkn);
                    if (value is string)
                    {
                        value = value.ToInt();
                    }
                    return (int?)value;
                }
            }
            public static int? MaxDayBeforeBookingMJkn
            {
                get
                {
                    var value = GetParameterNullableValue(AppParameter.ParameterItem.MaxDayBeforeBookingMJkn);
                    if (value is string)
                    {
                        value = value.ToInt();
                    }
                    return (int?)value;
                }
            }
            public static int? MinDayAfterBookingMJkn
            {
                get
                {
                    var value = GetParameterNullableValue(AppParameter.ParameterItem.MinDayAfterBookingMJkn);
                    if (value is string)
                    {
                        value = value.ToInt();
                    }
                    return (int?)value;
                }
            }
            public static int? MaxHourCheckInMJknKiosk
            {
                get
                {
                    var value = GetParameterNullableValue(AppParameter.ParameterItem.MaxHourCheckInMJknKiosk);
                    if (value is string)
                    {
                        value = value.ToInt();
                    }
                    return (int?)value;
                }
            }
            public static string HealthcareInitial
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.HealthcareInitial)); }
            }
            public static string HealthcareInitialAppsVersion
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion)); }
            }

            public static string IntNotesVerifLabel
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.IntNotesVerifLabel)); }
            }

            public static string IntNotesVerifLabelReview
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.IntNotesVerifLabelReview)); }
            }

            public static string EklaimRemoveDashSeparatorOnMedicalNo
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EklaimRemoveDashSeparatorOnMedicalNo)); }
            }

            public static bool IsUsingHisInterop
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingHisInterop); }
            }

            public static bool IsUsingHisInteropWithMultipleConnection
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingHisInteropWithMultipleConnection); }
            }

            public static bool IsUsingHisInteropCorrection
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingHisInteropCorrection); }
            }

            public static bool IsUsingHisInteropToHcLab
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingHisInteropToHcLab); }
            }

            public static string HisInteropConfigName
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.HisInteropConfigName)); }
            }

            public static bool IsiDRGIntegration
            {
                get { return AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsiDRGIntegration); }
            }

            public static string CurrencyRupiahID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.CurrencyRupiahID)); }
            }

            public static double TimeLimitForVoidPayment
            {
                get { return Convert.ToDouble(GetParameterValue(AppParameter.ParameterItem.TimeLimitForVoidPayment)); }
            }

            public static string[] GuarantorAskesID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.GuarantorAskesID)).Split(','); }
            }

            public static string[] ItemGroupBmhp
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.GuarantorAskesID)).Split(','); }
            }

            public static string[] BpjsIgdUgdBridgingID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.BpjsIgdUgdBridgingID)).Split(','); }
            }

            public static string GuarantorJamsostekID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.GuarantorJamsostekID)); }
            }

            public static string GuarantorEmployeeID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.GuarantorEmployeeID)); }
            }

            //public static string PettyCashUnitFinanceID
            //{
            //    get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PettyCashUnitFinanceID)); }
            //}

            //public static string PettyCashUnitCashierID
            //{
            //    get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PettyCashUnitCashierID)); }
            //}

            //public static string IsUsingPettyCash
            //{
            //    get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.IsUsingPettyCash)); }
            //}

            public static bool IsUsingHumanResourcesModul
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingHumanResourcesModul);
                }
            }

            public static string IsUsingAllICD10
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.IsUsingAllICD10)); }
            }
            public static string IsRL5354IncludeICD10O
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.IsRL5354IncludeICD10O)); }
            }
            public static string GuarantorTypeEmployee
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.GuarantorTypeEmployee)); }
            }

            public static string GuarantorTypeInsurance
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.GuarantorTypeInsurance)); }
            }

            public static string GuarantorTypeDiscount
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.GuarantorTypeDiscount)); }
            }

            public static string ItemIdOngkir
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ItemIdOngkir)); }
            }

            public static bool IsUsingPromotion
            {
                get { return IsYes(AppParameter.ParameterItem.UsingPromotion); }
            }

            public static bool IsUsingTerminalDigitMedicalNo
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingTerminalDigitMedicalNo); }
            }

            public static bool IsUsingIntermBill
            {
                get
                {
                    return true;
                    //settingan lama (client sebelum rssa: rscm, jec), skr semua sudah pake intermbill
                    //return IsYes(AppParameter.ParameterItem.IsUsingIntermBill); 
                }
            }

            public static bool IsUsingExtramuralItem
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingExtramuralItem); }
            }

            public static bool IsUsingApprovalPurchaseRequest
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingApprovalPurchaseRequest); }
            }

            public static bool IsPhycisianInRegEditable
            {
                get { return IsYes(AppParameter.ParameterItem.IsPhycisianInRegEditable); }
            }

            public static bool IsGuarantorInRegEditable
            {
                get { return IsYes(AppParameter.ParameterItem.IsGuarantorInRegEditable); }
            }

            public static string NonClassID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.NonClassID)); }
            }

            public static string LocationKitchenID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.LocationKitchenID)); }
            }

            public static double CitoPercentage
            {
                get { return Convert.ToDouble(GetParameterValue(AppParameter.ParameterItem.CitoPercentage)); }
            }

            public static bool IsIndependentVoidRegistration
            {
                get { return IsYes(AppParameter.ParameterItem.IsIndependentVoidRegistration); }
            }

            public static bool IsUsingRoomingIn
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingRoomingIn); }
            }

            public static bool IsShowGenderOnBedInformationStatus
            {
                get { return IsYes(AppParameter.ParameterItem.IsShowGenderOnBedInformationStatus); }
            }

            public static decimal MaxDiscTxInPercentage
            {
                get { return Convert.ToDecimal(GetParameterValue(AppParameter.ParameterItem.MaxDiscTxInPercentage)); }
            }

            public static decimal MaxDiscTxTariffRsInPercentage
            {
                get { return Convert.ToDecimal(GetParameterValue(AppParameter.ParameterItem.MaxDiscTxTariffRsInPercentage)); }
            }

            public static bool IsSharePurchaseDiscToPatient
            {
                get { return IsYes(AppParameter.ParameterItem.IsSharePurchaseDiscToPatient); }
            }

            public static bool IsAllowEditPorDate
            {
                get { return IsYes(AppParameter.ParameterItem.IsAllowEditPorDate); }
            }

            public static bool IsAllowEditDateTimeImplementation
            {
                get { return IsYes(AppParameter.ParameterItem.IsAllowEditDateTimeImplementation); }
            }

            public static string TablePatientFieldValidation
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.TablePatientFieldValidation)); }
            }

            public static string TableRegistrationResponsiblePersonFieldValidation
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.TableRegistrationResponsiblePersonFieldValidation)); }
            }

            public static string ClosingJournalWithoutAllApproved
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ClosingJournalWithoutAllApproved)); }
            }

            public static string HumanResourceUserID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.HumanResourceUserID)); }
            }

            public static bool IsOperatingRoomResetPrice
            {
                get { return IsYes(AppParameter.ParameterItem.IsOperatingRoomResetPrice); }
            }

            public static bool IsOperatingRoomResetPriceLastClass
            {
                get { return IsYes(AppParameter.ParameterItem.IsOperatingRoomResetPriceLastClass); }
            }

            public static bool IsOperatingRoomResetPriceHighestClass
            {
                get { return IsYes(AppParameter.ParameterItem.IsOperatingRoomResetPriceHighestClass); }
            }

            public static bool IsAllowEditProcedureChargeClass
            {
                get { return IsYes(AppParameter.ParameterItem.IsAllowEditProcedureChargeClass); }
            }

            public static bool IsOutPatientInculeInAdminCalculation
            {
                get { return IsYes(AppParameter.ParameterItem.IsOutPatientInculeInAdminCalculation); }
            }

            public static string PicManagingDirector
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PicManagingDirector)); }
            }

            public static string PicManagingDirectorForInvoicing
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PicManagingDirectorForInvoicing)); }
            }

            public static string PicManagingDirectorPhoneNo
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PicManagingDirectorPhoneNo)); }
            }

            public static string PicHeadOfAdmitting
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PicHeadOfAdmitting)); }
            }

            public static string PicWarehouse
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PicWarehouse)); }
            }

            public static double DpAmtClassVip
            {
                get { return Convert.ToDouble(GetParameterValue(AppParameter.ParameterItem.DpAmtClassVip)); }
            }

            public static double DpAmtClassI
            {
                get { return Convert.ToDouble(GetParameterValue(AppParameter.ParameterItem.DpAmtClassI)); }
            }

            public static double DpAmtClassII
            {
                get { return Convert.ToDouble(GetParameterValue(AppParameter.ParameterItem.DpAmtClassII)); }
            }

            public static double DpAmtClassIII
            {
                get { return Convert.ToDouble(GetParameterValue(AppParameter.ParameterItem.DpAmtClassIII)); }
            }

            public static double DpAmtClassIcu
            {
                get { return Convert.ToDouble(GetParameterValue(AppParameter.ParameterItem.DpAmtClassIcu)); }
            }

            public static bool IsPhysicianFeeBasedOnPayment
            {
                get { return IsYes(AppParameter.ParameterItem.IsPhysicianFeeBasedOnPayment); }
            }

            public static bool IsPhysicianFeeArPaidBasedOnPayment
            {
                get { return IsYes(AppParameter.ParameterItem.IsPhysicianFeeArPaidBasedOnPayment); }
            }

            public static bool IsPhysicianFeeArCreateOnArReceipt
            {
                get { return IsYes(AppParameter.ParameterItem.IsPhysicianFeeArCreateOnArReceipt); }
            }

            public static string DependentsOfEmployeesGuarantorID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DependentsOfEmployeesGuarantorID)); }
            }

            public static string SalaryComponentIdForBasicSalary
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.SalaryComponentIdForBasicSalary)); }
            }

            public static string DischargeConditionDieLessThen48
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DischargeConditionDieLessThen48)); }
            }

            public static string DischargeConditionDieMoreThen48
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DischargeConditionDieMoreThen48)); }
            }

            public static string DischargeConditionDie
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DischargeConditionDie)); }
            }

            public static bool IsRADTLinkToHumanResourcesModul
            {
                get { return IsYes(AppParameter.ParameterItem.IsRADTLinkToHumanResourcesModul); }
            }

            public static string BankCashierID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.BankCashierID)); }
            }

            //public static bool IsNavigateUrlForDistributionWithStockInfo
            //{
            //    get { return IsYes(AppParameter.ParameterItem.IsNavigateUrlForDistributionWithStockInfo); }
            //}

            public static string GuarantorTypeSelf
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.GuarantorTypeSelf)); }
            }

            public static string DefaultTerm
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DefaultTerm)); }
            }

            public static string DefaultPurchaseOrderType
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DefaultPurchaseOrderType)); }
            }

            public static string DefaultDownPaymentType
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DefaultDownPaymentType)); }
            }

            public static int IntervalPatientLastVisit
            {
                get { return Convert.ToInt32(GetParameterValue(AppParameter.ParameterItem.IntervalPatientLastVisit)); }
            }

            public static string DefaultClassMenuStandard
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DefaultClassMenuStandard)); }
            }

            public static string DefaultSalaryTemplateID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DefaultSalaryTemplateID)); }
            }

            public static bool IsSeparationBetweenOutpatientAndInpatientSupplies
            {
                get { return IsYes(AppParameter.ParameterItem.IsSeparationBetweenOutpatientAndInpatientSupplies); }
            }

            public static bool IsDistributionMenuIsUsedAsItemRequestMenu
            {
                get { return IsYes(AppParameter.ParameterItem.IsDistributionMenuIsUsedAsItemRequestMenu); }
            }

            public static string ProcessTypePositionGrade
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ProcessTypePositionGrade)); }
            }

            public static string ProcessTypeSalary
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ProcessTypeSalary)); }
            }

            public static string ProcessTypeOvertime
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ProcessTypeOvertime)); }
            }

            public static string MedicalRecordServiceUnitID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.MedicalRecordServiceUnitID)); }
            }

            public static bool IsItemProductAllowEditByUserVerificated
            {
                get { return IsYes(AppParameter.ParameterItem.IsItemProductAllowEditByUserVerificated); }
            }

            public static bool IsAdminCalcIncludeItemProduct
            {
                get { return IsYes(AppParameter.ParameterItem.IsAdminCalcIncludeItemProduct); }
            }

            //public static bool IsAdminCalcBeforeDiscount
            //{
            //    get { return IsYes(AppParameter.ParameterItem.IsAdminCalcBeforeDiscount); }
            //}

            public static bool IsDistributionAutoConfirm
            {
                get { return IsYes(AppParameter.ParameterItem.IsDistributionAutoConfirm); }
            }

            public static bool IsSeparatePaymentForOpConsul
            {
                get { return IsYes(AppParameter.ParameterItem.IsSeparatePaymentForOpConsul); }
            }

            public static bool IsPurcReturnWithPrice
            {
                get { return IsYes(AppParameter.ParameterItem.IsPurcReturnWithPrice); }
            }

            public static string WorkStatusOpen
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.WorkStatusOpen)); }
            }

            public static string WorkStatusClosed
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.WorkStatusClosed)); }
            }

            public static string WorkStatusDone
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.WorkStatusDone)); }
            }

            public static string WorkStatusWaitingForParts
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.WorkStatusWaitingForParts)); }
            }

            public static string WorkStatusThirdParties
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.WorkStatusThirdParties)); }
            }

            public static string WorkStatusCancelled
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.WorkStatusCancelled)); }
            }

            public static string WorkTypePreventive
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.WorkTypePreventive)); }
            }

            public static string WorkTypeProject
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.WorkTypeProject)); }
            }

            public static string WorkPriorityNormal
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.WorkPriorityNormal)); }
            }

            public static string WorkPriorityRoutine
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.WorkPriorityRoutine)); }
            }

            public static string WorkTradeSanitation
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.WorkTradeSanitation)); }
            }

            public static string ChargeBedExecutionTime
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ChargeBedExecutionTime)); }
            }

            public static bool IsPoAndPorInTheSameUnit
            {
                get { return IsYes(AppParameter.ParameterItem.IsPoAndPorInTheSameUnit); }
            }

            //public static bool IsPoWithThreeTypesOfTaxes
            //{ 
            //    get { return IsYes(AppParameter.ParameterItem.IsPoWithThreeTypesOfTaxes); }
            //}

            public static bool IsPorCanChangeThePrice
            {
                get { return IsYes(AppParameter.ParameterItem.IsPorCanChangeThePrice); }
            }

            public static bool IsShowSystemQtyInStockTacking
            {
                get { return IsYes(AppParameter.ParameterItem.IsShowSystemQtyInStockTacking); }
            }

            public static bool IsItemInventoryNameUsingUpperCase
            {
                get { return IsYes(AppParameter.ParameterItem.IsItemInventoryNameUsingUpperCase); }
            }

            public static string VoidReasonBatalBerobat
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.VoidReasonBatalBerobat)); }
            }

            public static bool IsUpdatePrescriptionPriceWhenRecal
            {
                get { return IsYes(AppParameter.ParameterItem.IsUpdatePrescriptionPriceWhenRecal); }
            }

            public static bool IsUpdatePriceUsingParentRuleWhenRecal
            {
                get { return IsYes(AppParameter.ParameterItem.IsUpdatePriceUsingParentRuleWhenRecal); }
            }

            public static bool IsAllowDiscountInvoice
            {
                get { return IsYes(AppParameter.ParameterItem.IsAllowDiscountInvoice); }
            }

            //public static string IsUsingSeparateNoForEachPharmacyUnit
            //{
            //    get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.IsUsingSeparateNoForEachPharmacyUnit)); }
            //}

            public static bool IsForceUseNoIntermbill
            {
                get { return IsYes(AppParameter.ParameterItem.IsForceUseNoIntermbill); }
            }

            public static bool IsRegistrationRequiredSMF
            {
                get { return IsYes(AppParameter.ParameterItem.IsRegistrationRequiredSMF); }
            }

            public static bool IsStockOpnamePerGroupItem
            {
                get { return IsYes(AppParameter.ParameterItem.IsStockOpnamePerGroupItem); }
            }

            public static bool IsPhysicianFeeCalcBasedOnGuarantorCategory
            {
                get { return IsYes(AppParameter.ParameterItem.IsPhysicianFeeCalcBasedOnGuarantorCategory); }
            }

            public static bool IsDistReqOrPurcReqUsingBudgetPlan
            {
                get { return IsYes(AppParameter.ParameterItem.IsDistReqOrPurcReqUsingBudgetPlan); }
            }

            public static string MainDistributionServiceUnitIDForNonMedical
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.MainDistributionServiceUnitIDForNonMedical)); }
            }

            public static string MainDistributionLocationIDForNonMedical
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.MainDistributionLocationIDForNonMedical)); }
            }

            public static string MainPurchasingUnitIDForNonMedical
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.MainPurchasingUnitIDForNonMedical)); }
            }

            public static string MainPurchasingUnitIDForMedical
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.MainPurchasingUnitIDForMedical)); }
            }

            public static string SubLedgerGroupIdGuarantor
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.SubLedgerGroupIdGuarantor)); }
            }

            public static string SubLedgerGroupIdServiceUnit
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.SubLedgerGroupIdServiceUnit)); }
            }

            public static string SubLedgerGroupIdSupplier
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.SubLedgerGroupIdSupplier)); }
            }

            public static string SubLedgerGroupIdParamedic
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.SubLedgerGroupIdParamedic)); }
            }

            public static string ItemIdAdmRjGuar
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ItemIdAdmRjGuar)); }
            }

            public static bool IsPurcReturnCanChangePrice
            {
                get { return IsYes(AppParameter.ParameterItem.IsPurcReturnCanChangePrice); }
            }

            public static bool IsRequestChangeItemProductUpdatePriceSupplierItem
            {
                get { return IsYes(AppParameter.ParameterItem.IsRequestChangeItemProductUpdatePriceSupplierItem); }
            }

            public static string ServiceUnitVkId
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitVkId)); }
            }
            public static string LisInterop
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.LisInterop)); }
            }

            public static string LisCriticalFieldName
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.LisCriticalFieldName)); }
            }

            public static bool IsUsingMealOrderVerification
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingMealOrderVerification); }
            }

            public static bool IsCustomPivotFilterByUser
            {
                get { return IsYes(AppParameter.ParameterItem.IsCustomPivotFilterByUser); }
            }

            public static bool IsUseStandardMealMenuForAllClass
            {
                get { return IsYes(AppParameter.ParameterItem.IsUseStandardMealMenuForAllClass); }
            }

            public static string LiquidFoodId
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.LiquidFoodId)); }
            }

            public static string BlenderizedFoodId
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.BlenderizedFoodId)); }
            }

            public static string DefaultMenuStandard
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DefaultMenuStandard)); }
            }

            public static string DefaultChecklistArPayment
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DefaultChecklistArPayment)); }
            }

            public static bool IsShowPrescPriceOnDisplayDoctor
            {
                get { return IsYes(AppParameter.ParameterItem.IsShowPrescPriceOnDisplayDoctor); }
            }

            public static bool IsPatientCardPrintedOnlyForOutpatients
            {
                get { return IsYes(AppParameter.ParameterItem.IsPatientCardPrintedOnlyForOutpatients); }
            }

            public static bool IsRegReferralGroupMandatory
            {
                get { return IsYes(AppParameter.ParameterItem.IsRegReferralGroupMandatory); }
            }

            public static bool IsRegReferralMandatory
            {
                get { return IsYes(AppParameter.ParameterItem.IsRegReferralMandatory); }
            }

            public static bool IsHideOpenCloseOnVerificationForUser
            {
                get { return IsYes(AppParameter.ParameterItem.IsHideOpenCloseOnVerificationForUser); }
            }

            public static bool IsProductionOfGoodUpdatingTariff
            {
                get { return IsYes(AppParameter.ParameterItem.IsProductionOfGoodUpdatingTariff); }
            }

            public static bool IsCreateAssetIdAutomatic
            {
                get { return IsYes(AppParameter.ParameterItem.IsCreateAssetIdAutomatic); }
            }

            public static bool IsAutoCreateApplicantNo
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutoCreateApplicantNo); }
            }

            public static bool IsUpdatePhysicianLookingPhysicianFeeVerification
            {
                get { return IsYes(AppParameter.ParameterItem.IsUpdatePhysicianLookingPhysicianFeeVerification); }
            }

            public static string AssetsStatusActive
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AssetsStatusActive)); }
            }

            public static string AssetsStatusInActive
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AssetsStatusInActive)); }
            }

            public static string AssetsStatusDisposed
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AssetsStatusDisposed)); }
            }

            public static string AssetsStatusLost
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AssetsStatusLost)); }
            }

            public static string AssetsStatusSold
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AssetsStatusSold)); }
            }

            public static string AssetsStatusDamaged
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AssetsStatusDamaged)); }
            }

            public static string[] AssetsStatusDepreciationJournal
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.acc_AssetsStatusDepreciationJournal)).Split(','); }
            }

            public static string ParamedicIdDokterLuar
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ParamedicIdDokterLuar)); }
            }

            public static string ReferralGroupDatangSendiri
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ReferralGroupDatangSendiri)); }
            }

            public static string ReferralGroupPASUS
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ReferralGroupPASUS)); }
            }

            public static string ReferralGroupPASUSLabel
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ReferralGroupPASUSLabel)); }
            }

            public static bool IsRefreshBeforeLockVerification
            {
                get { return IsYes(AppParameter.ParameterItem.IsRefreshBeforeLockVerification); }
            }

            public static bool IsAllowGuarantorDepositBalanceMinus
            {
                get { return IsYes(AppParameter.ParameterItem.IsAllowGuarantorDepositBalanceMinus); }
            }

            public static string DiagnoseTypeMain
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain)); }
            }

            public static string DiagnoseTypeInitial
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeInitial)); }
            }

            public static string DiagnoseTypeDeathDiagnosis
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeDeathDiagnosis)); }
            }

            public static string[] SitbDiagnoseList
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.SitbDiagnoseList)).Split(','); }
            }

            public static string ServiceUnitPharCentralWarehouseId1
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitPharCentralWarehouseId1)); }
            }

            public static string ServiceUnitPharCentralWarehouseId2
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitPharCentralWarehouseId2)); }
            }

            public static string TherapyGroupAntibiotics
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.TherapyGroupAntibiotics)); }
            }

            public static bool IsWorkOrderRealizationAutoReturn
            {
                get { return IsYes(AppParameter.ParameterItem.IsWorkOrderRealizationAutoReturn); }
            }

            public static string LocationLogisticCentralWHID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.LocationLogisticCentralWHID)); }
            }

            public static string LocationPharmacyCentralWHID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.LocationPharmacyCentralWHID)); }
            }

            public static string ProductTypeInjeksi
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ProductTypeInjeksi)); }
            }

            public static string PrescShowBlncForEpisodeHistory
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PrescShowBlncForEpisodeHistory)); }
            }

            public static bool IsAutoClosedRegOnPaymentWithHoldTx
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutoClosedRegOnPaymentWithHoldTx); }
            }

            public static bool IsAutoClosedRegIpOnDischarge
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutoClosedRegIpOnDischarge); }
            }

            public static string NursingAssessmentDO
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.NursingAssessmentDO)); }
            }

            public static string NursingAssessmentDS
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.NursingAssessmentDS)); }
            }

            public static bool IsPrescOrderNeedSoape
            {
                get { return IsYes(AppParameter.ParameterItem.IsPrescOrderNeedSoape); }
            }

            public static bool IsUsingCpoeModule
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingCpoeModule); }
            }

            public static string ServiceUnitPathologyAnatomyID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitPathologyAnatomyID)); }
            }

            public static string PhysicianIsRequiredAtRegistration
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PhysicianIsRequiredAtRegistration)); }
            }

            public static string AutoCheckOutOnPayment
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AutoCheckOutOnPayment)); }
            }

            public static bool IsMaterialUsedAwoNeedRequest
            {
                get { return IsYes(AppParameter.ParameterItem.IsMaterialUsedAwoNeedRequest); }
            }

            public static string IncidentFollowUpInvestigation
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.IncidentFollowUpInvestigation)); }
            }

            public static string ValidateGuarantorContractPeriode
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ValidateGuarantorContractPeriode)); }
            }

            public static string ServiceUnitBloodBankID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitBloodBankID)); }
            }

            public static string ServiceUnitOperationRoomID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitOperationRoomID)); }
            }

            public static string SalaryTypeLoan
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.SalaryTypeLoan)); }
            }

            public static string SalaryTypeOvertime
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.SalaryTypeOvertime)); }
            }

            public static string PoNonMasterDefPAccount
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PoNonMasterDefPAccount)); }
            }

            public static string ServiceUnitCssdID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitCssdID)); }
            }

            public static string TransEnty_ShowFilterDateReg
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.TransEnty_ShowFilterDateReg)); }
            }

            public static bool IsProductionOfGoodsAutoCssdReceived
            {
                get { return IsYes(AppParameter.ParameterItem.IsProductionOfGoodsAutoCssdReceived); }
            }

            public static string AllowPOCashInPOR
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AllowPOCashInPOR)); }
            }

            public static double BpjsCoverageFormula
            {
                get { return Convert.ToDouble(GetParameterValue(AppParameter.ParameterItem.BpjsCoverageFormula)); }
            }

            public static string PrescriptionReturnRecipeAmountReturned
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PrescriptionReturnRecipeAmountReturned)); }
            }

            public static bool IsPrescSalesOpNeedSoape
            {
                get { return IsYes(AppParameter.ParameterItem.IsPrescSalesOpNeedSoape); }
            }

            public static string IsConnectToLokadok
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.IsConnectToLokadok)); }
            }

            public static string[] QuestionIdForWeight
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.QuestionIdForWeight)).Split(','); }
            }

            public static string[] QuestionIdForHeight
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.QuestionIdForHeight)).Split(','); }
            }

            public static string[] QuestionIdBodyMassIndex
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.QuestionIdBodyMassIndex)).Split(','); }
            }

            public static string GuarantorTypeCompany
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.GuarantorTypeCompany)); }
            }

            public static string ValidateGuarantorCardNo
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ValidateGuarantorCardNo)); }
            }

            public static string ValidateInsuranceID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ValidateInsuranceID)); }
            }

            public static bool IsBillVerifARGuarantorExclDisc
            {
                get { return IsYes(AppParameter.ParameterItem.IsBillVerifARGuarantorExclDisc); }
            }

            public static bool IsNeedEdControlOnPOR
            {
                get { return IsYes(AppParameter.ParameterItem.IsNeedEdControlOnPOR); }
            }

            public static bool IsNeedPriceControlOnPOR
            {
                get { return IsYes(AppParameter.ParameterItem.IsNeedPriceControlOnPOR); }
            }

            public static bool IsNeedQtyControlOnPOR
            {
                get { return IsYes(AppParameter.ParameterItem.IsNeedQtyControlOnPOR); }
            }

            public static string TheMinimumTimeLimitEdControlOnPOR
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.TheMinimumTimeLimitEdControlOnPOR)); }
            }

            public static bool IsBookingBedCharged
            {
                get { return IsYes(AppParameter.ParameterItem.IsBookingBedCharged); }
            }

            public static bool IsUsingHetAsMaxSalesPrice
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingHetAsMaxSalesPrice); }
            }

            public static bool IsAutoClosedRegErOnTransfer
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutoClosedRegErOnTransfer); }
            }

            public static bool IsAutoClosedRegOpOnTransfer
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutoClosedRegOpOnTransfer); }
            }

            public static bool IsAutoClosedRegFromOnCheckinConfirmed
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutoClosedRegFromOnCheckinConfirmed); }
            }

            public static bool IsAutoSaveMedicalFileBin
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutoSaveMedicalFileBin); }
            }

            public static string MaxMedicalFileBinNo
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.MaxMedicalFileBinNo)); }
            }

            public static bool IsPOWithStockInfo
            {
                get { return IsYes(AppParameter.ParameterItem.IsPOWithStockInfo); }
            }

            public static bool IsUseApprovalLevel
            {
                get
                {
                    var isUseApprovalLevel = Convert.ToString(GetParameterValue(AppParameter.ParameterItem.IsUseApprovalLevel));
                    return isUseApprovalLevel.ToLower() == "yes";
                }
            }

            public static bool IsDistributionUseApprovalLevel
            {
                get
                {
                    var isDistUseApprovalLevel = Convert.ToString(GetParameterValue(AppParameter.ParameterItem.IsDistributionUseApprovalLevel));
                    return isDistUseApprovalLevel.ToLower() == "yes";
                }
            }

            public static string AssasmentObgynPenyKandungan
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AssasmentObgynPenyKandungan)); }
            }

            public static string AssasmentObgynPoliKebidanan
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AssasmentObgynPoliKebidanan)); }
            }

            public static bool IsTxUsingEdDetail
            {
                get { return IsYes(AppParameter.ParameterItem.IsTxUsingEdDetail); }
            }


            public static string PpnOutRJ
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PpnOutRJ)); }
            }

            public static string PpnOutRD
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PpnOutRD)); }
            }

            public static bool IsDisplayPrintButtonInRegistrationFrom
            {
                get { return IsYes(AppParameter.ParameterItem.IsDisplayPrintButtonInRegistrationFrom); }
            }

            public static bool IsFeeCalculatedOnTransaction
            {
                get { return IsYes(AppParameter.ParameterItem.IsFeeCalculatedOnTransaction); }
            }

            public static bool IsTarifCompPhysicianDiscountMaxByShare
            {
                get { return IsYes(AppParameter.ParameterItem.IsTarifCompPhysicianDiscountMaxByShare); }
            }

            public static bool IsValidatedMedicalFileReceived
            {
                get { return IsYes(AppParameter.ParameterItem.IsValidatedMedicalFileReceived); }
            }

            public static bool IsDpReturnUsingChecklist
            {
                get { return IsYes(AppParameter.ParameterItem.IsDpReturnUsingChecklist); }
            }

            public static string BridgingTypeBpjs
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.BridgingTypeBpjs)); }
            }

            public static bool IsPrOutstandingListBasedOnCalcQtyOrder
            {
                get { return IsYes(AppParameter.ParameterItem.IsPrOutstandingListBasedOnCalcQtyOrder); }
            }

            public static bool IsJobOrderRealizationNeedConfirm
            {
                get { return IsYes(AppParameter.ParameterItem.IsJobOrderRealizationNeedConfirm); }
            }

            public static bool IsNeedVoidReasonOnPrescriptionSales
            {
                get { return IsYes(AppParameter.ParameterItem.IsNeedVoidReasonOnPrescriptionSales); }
            }

            public static bool IsPphUsesAfixedValue
            {
                get { return IsYes(AppParameter.ParameterItem.IsPphUsesAfixedValue); }
            }

            public static bool IsAutoFreezeLocationOnStockOpnameAdd
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutoFreezeLocationOnStockOpnameAdd); }
            }

            public static string SalaryComponentIdForStructuralBenefits
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.SalaryComponentIdForStructuralBenefits)); }
            }

            //settingan dipindah ke table MenuVersion
            //public static bool IsMealOrderUsingThe10Plus1Rule
            //{
            //    get { return IsYes(AppParameter.ParameterItem.IsMealOrderUsingThe10Plus1Rule); }
            //}

            public static bool IsAllowEditPoFromReorderProcess
            {
                get { return IsYes(AppParameter.ParameterItem.IsAllowEditPoFromReorderProcess); }
            }

            public static bool IsAllowEditAmountApInvoice
            {
                get { return IsYes(AppParameter.ParameterItem.IsAllowEditAmountApInvoice); }
            }


            public static bool IsBillingAdjustEnabled
            {
                get { return IsYes(AppParameter.ParameterItem.IsBillingAdjustEnabled); }
            }

            public static bool IsRegSEPMandatory
            {
                get { return IsYes(AppParameter.ParameterItem.IsRegSEPMandatory); }
            }

            public static bool IsReducePriceWhenDeletingMcuPackageDetails
            {
                get { return IsYes(AppParameter.ParameterItem.IsReducePriceWhenDeletingMcuPackageDetails); }
            }

            public static bool IsAllowCorrectionOfIntermBillsTransaction
            {
                get { return IsYes(AppParameter.ParameterItem.IsAllowCorrectionOfIntermBillsTransaction); }
            }

            public static bool IsAllowEditPoDate
            {
                get { return IsYes(AppParameter.ParameterItem.IsAllowEditPoDate); }
            }

            public static string ServiceUnitLaundryID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitLaundryID)); }
            }

            public static string ItemGroupFisiotherapyID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ItemGroupFisiotherapyID)); }
            }

            public static string ItemGroupPathologyAnatomyID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ItemGroupPathologyAnatomyID)); }
            }

            public static bool IsAllowEditRegistrationDate
            {
                get { return IsYes(AppParameter.ParameterItem.IsAllowEditRegistrationDate); }
            }

            public static bool IsAllowEditDischargeDate
            {
                get { return IsYes(AppParameter.ParameterItem.IsAllowEditDischargeDate); }
            }

            public static bool IsInvoicePaymentSeparatedNo
            {
                get { return IsYes(AppParameter.ParameterItem.IsInvoicePaymentSeparatedNo); }
            }

            public static bool IsAutoPrintEtiquette
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutoPrintEtiquette); }
            }

            public static bool IsAutoPrintSEP
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutoPrintSEP); }
            }

            public static bool IsAutoPrintPrescriptionReceipt
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutoPrintPrescriptionReceipt); }
            }

            public static bool IsAutoPrintDistributionReceipt
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutoPrintDistributionReceipt); }
            }

            public static bool IsAutoPrintStockAdjustmentReceipt
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutoPrintStockAdjustmentReceipt); }
            }

            public static bool IsShowPriceInPurchaseRequest
            {
                get { return IsYes(AppParameter.ParameterItem.IsShowPriceInPurchaseRequest); }
            }

            public static bool IsPrescriptionReturnAdminChecked
            {
                get { return IsYes(AppParameter.ParameterItem.IsPrescriptionReturnAdminChecked); }
            }

            public static bool IsPrescriptionDiscountIncludeR
            {
                get { return IsYes(AppParameter.ParameterItem.IsPrescriptionDiscountIncludeR); }
            }

            public static bool IsPOCanEditTax
            {
                get { return IsYes(AppParameter.ParameterItem.IsPOCanEditTax); }
            }

            public static bool IsUsingRoundingDown
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingRoundingDown); }
            }

            public static bool IsUsingRoundingDownWithBalancing
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingRoundingDownWithBalancing); }
            }

            public static bool IsPORoundingDownZeroDigit
            {
                get { return IsYes(AppParameter.ParameterItem.IsPORoundingDownZeroDigit); }
            }

            [Obsolete("Obsolete, baca dari parameter list yg dikirim di form InventoryIssueDetail.aspx.cs", true)]
            public static bool IsInventoryIssueUsingRequest
            {
                get { return IsYes(AppParameter.ParameterItem.IsInventoryIssueUsingRequest); }
            }

            public static bool IsPOCanChangeConversion
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPOCanChangeConversion);
                }
            }

            public static bool IsPOCanChangePurchaseUnit
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPOCanChangePurchaseUnit);
                }
            }

            public static bool IsPOCanChangeQty
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPOCanChangeQty);
                }
            }

            public static string KioskQueueSlipRpt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.KioskQueueSlipRpt)); }
            }

            public static string ParamedicIdLabDefault
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ParamedicIdLabDefault)); }
            }

            public static string ParamedicIdRadDefault
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ParamedicIdRadDefault)); }
            }

            public static string AppProgramPrintLabelMCU
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AppProgramPrintLabelMCU)); }
            }

            public static string ProgramIdPrintUddEtiquette
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ProgramIdPrintUddEtiquette)); }
            }

            public static string ProgramIdPrintSurgeryCostEstimation
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ProgramIdPrintSurgeryCostEstimation)); }
            }

            public static string ProgramIdPrintSurgeryBilling
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ProgramIdPrintSurgeryBilling)); }
            }

            public static string ProgramIdPrintDistributionReceipt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ProgramIdPrintDistributionReceipt)); }
            }

            public static string ProgramIdPrintStockAdjustmentReceipt
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ProgramIdPrintStockAdjustmentReceipt)); }
            }

            public static string ProgramIdPrintJobDescription
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ProgramIdPrintJobDescription)); }
            }

            public static string ProgramIdPrintExamOrderOtherResult
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ProgramIdPrintExamOrderOtherResult)); }
            }

            public static bool IsPrescriptionPendingDelivery
            {
                get { return IsYes(AppParameter.ParameterItem.IsPrescriptionPendingDelivery); }
            }

            public static bool IsPrescriptionReturnToOneLocation
            {
                get { return IsYes(AppParameter.ParameterItem.IsPrescriptionReturnToOneLocation); }
            }

            public static bool IsPrescriptionReturnToOneLocationWithUserDefUnit
            {
                get { return IsYes(AppParameter.ParameterItem.IsPrescriptionReturnToOneLocationWithUserDefUnit); }
            }

            public static bool IsTestResultAllowModifDate
            {
                get { return IsYes(AppParameter.ParameterItem.IsTestResultAllowModifDate); }
            }

            public static bool IsManualUserHostName
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsManualUserHostName);
                }
            }
            public static bool IsNeedAllowCheckoutConfirmedForDischarge
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsNeedAllowCheckoutConfirmedForDischarge);
                }
            }

            public static string SRReferralGroupDefault
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.SRReferralGroupDefault)); }
            }

            public static bool IsDisplayRegDateTimeUseCreateDate
            {
                get { return IsYes(AppParameter.ParameterItem.IsDisplayRegDateTimeUseCreateDate); }
            }

            public static string PhysicianSenderReferralGroups
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PhysicianSenderReferralGroups)); }
            }

            public static string PatientIDForCafe
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PatientIDForCafe)); }
            }

            public static string ServiceUnitIDForCafe
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitIDForCafe)); }
            }

            public static bool IsDistributionRequestBasedOnItemsPerLocation
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsDistributionRequestBasedOnItemsPerLocation);
                }
            }

            public static bool IsAllowPaymentReturnFromCashEntry
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAllowPaymentReturnFromCashEntry);
                }
            }

            public static bool IsDefaultPaymentReturnFromCashEntry
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsDefaultPaymentReturnFromCashEntry);
                }
            }

            public static bool IsControlEatingPatientByNutritionists
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsControlEatingPatientByNutritionists);
                }
            }

            public static bool IsPhysicianPrescriptionSalesDefaultEmpty
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPhysicianPrescriptionSalesDefaultEmpty);
                }
            }

            public static bool IsAutoBlacklistOnPersonalAr
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAutoBlacklistOnPersonalAr);
                }
            }
            public static bool IsCoaAPNonMedicSeparated
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsCoaAPNonMedicSeparated);
                }
            }

            public static bool IsAllowPrescriptionReturnForMultipleRegistration
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAllowPrescriptionReturnForMultipleRegistration);
                }
            }

            public static bool IsPoBasedOnPr
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPoBasedOnPr);
                }
            }

            public static bool IsServiceUnitPrescriptionSalesDefaultEmpty
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsServiceUnitPrescriptionSalesDefaultEmpty);
                }
            }

            public static string PatientInTypeTrueEmergency
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PatientInTypeTrueEmergency)); }
            }

            public static string DefaultUserPassword
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DefaultUserPassword)); }
            }

            public static bool IsAllowCorrectionForIntermBillTx
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAllowCorrectionForIntermBillTx);
                }
            }

            public static string DefaultParamedicTeamOnEmrList
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DefaultParamedicTeamOnEmrList)); }
            }

            public static bool IsListItemForTxOnlyInStock
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsListItemForTxOnlyInStock);
                }
            }

            public static bool IsRegValidateResponsibleName
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsRegValidateResponsibleName);
                }
            }

            public static bool IsNeedValidateMobilePhoneNo
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsNeedValidateMobilePhoneNo);
                }
            }

            public static bool IsPatientIprOnPrescSalesForCheckinConfirmedOnly
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPatientIprOnPrescSalesForCheckinConfirmedOnly);
                }
            }

            public static bool IsMoveRecordOnPrescSalesIncludeVoid
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsMoveRecordOnPrescSalesIncludeVoid);
                }
            }

            public static bool IsRegistrationVoidReasonRequired
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsRegistrationVoidReasonRequired);
                }
            }

            public static string GuarantorTypeBPJS
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.GuarantorTypeBpjs)); }
            }

            public static bool IsCashEntryShowReceivedFromPaidTo
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsCashEntryShowReceivedFromPaidTo);
                }
            }

            public static bool IsDisplayServiceUnitBookingNoOnTransactionEntry
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsDisplayServiceUnitBookingNoOnTransactionEntry);
                }
            }

            public static bool IsDisplayKiaCaseAndObstetricTypeOnTransactionEntry
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsDisplayKiaCaseAndObstetricTypeOnTransactionEntry);
                }
            }

            public static bool IsDisplayExecutionDateOnPrescriptionSales
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsDisplayExecutionDateOnPrescriptionSales);
                }
            }

            public static bool IsBedNeedConfirmation
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsBedNeedConfirmation);
                }
            }

            public static bool IsBedNeedCleanedProcess
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsBedNeedCleanedProcess);
                }
            }

            public static bool IsWorkTradeMandatory
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsWorkTradeMandatory);
                }
            }

            public static bool IsDischargeDateOnEmrMandatory
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsDischargeDateOnEmrMandatory);
                }
            }

            public static bool IsCloseRegOnDischargeEmr
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsCloseRegOnDischargeEmr);
                }
            }

            public static string SupplierNonPkpTaxStatusDefault
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.SupplierNonPkpTaxStatusDefault)); }
            }

            public static bool IsCloseOutstandingIssueRequest
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsCloseOutstandingIssueRequest);
                }
            }

            public static bool IsPhysicianFeeVerificationPaidOnly
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPhysicianFeeVerificationPaidOnly);
                }
            }

            public static bool IsAPVerifNeedValidate
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAPVerifNeedValidate);
                }
            }

            public static bool IsMealOrderValidationForIncompleteItem
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsMealOrderValidationForIncompleteItem);
                }
            }

            public static bool IsEpisodeDiagValidateExtCauseAndMorp
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsEpisodeDiagValidateExtCauseAndMorp);
                }
            }

            public static string[] ItemIdImunisasiTT1
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ItemIdImunisasiTT1)).Split(','); }
            }

            public static string[] ItemIdImunisasiTT2
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ItemIdImunisasiTT2)).Split(','); }
            }

            public static string[] ServiceUnitImunisasiTTId
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitImunisasiTTId)).Split(','); }
            }

            public static bool IsAutoPrintCafeSlipOrder
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAutoPrintCafeSlipOrder);
                }
            }
            [Obsolete("Obsolete, Ganti dgn IsTariffPriceVisibleOnlyForAdm", true)]
            public static bool TariffPriceVisibleOnlyForAdm
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.TariffPriceVisibleOnlyForAdm);
                }
            }
            public static bool IsTariffPriceVisibleOnlyForAdm
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.TariffPriceVisibleOnlyForAdm);
                }
            }
            public static string DefaultConsumeMethod
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DefaultConsumeMethod)); }
            }

            public static string DefaultDosageUnit
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DefaultDosageUnit)); }
            }

            public static string ServiceUnitCashierID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitCashierID)); }
            }

            public static bool IsUsingCashManagement
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingCashManagement);
                }
            }

            public static bool IsBypassCashierAuthorization
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsBypassCashierAuthorization);
                }
            }

            public static bool IsDiagAndProcListRestoreValueFromCookie
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsDiagAndProcListRestoreValueFromCookie);
                }
            }

            public static bool IsDiagAndProcListFilterParameter
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsDiagAndProcListFilterParameter);
                }
            }

            public static bool IsReadonlyMedicalNoOnPatientEntry
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsReadonlyMedicalNoOnPatientEntry);
                }
            }

            public static bool IsReadonlyMedicalNoOnEditPatientEntry
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsReadonlyMedicalNoOnEditPatientEntry);
                }
            }

            public static bool IsReadonlyMedicalNoOnUpdateMrnPatient
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsReadonlyMedicalNoOnUpdateMrnPatient);
                }
            }

            public static bool IsReadonlyPatientNameOnEditPatientEntry
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsReadonlyPatientNameOnEditPatientEntry);
                }
            }

            public static bool IsPhysicianFeeShowProcedureNote
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPhysicianFeeShowProcedureNote);
                }
            }

            public static bool IsApInvoiceCanChangeThePrice
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsApInvoiceCanChangeThePrice);
                }
            }
            public static bool IsMedRecCanChangePatientDischarge
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsMedRecCanChangePatientDischarge);
                }
            }

            public static bool IsValidateNoteOnJobOrderLab
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsValidateNoteOnJobOrderLab);
                }
            }

            public static bool IsValidateNoteOnAllJobOrder
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsValidateNoteOnAllJobOrder);
                }
            }

            public static bool IsMandatoryEmrRegDetail
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsMandatoryEmrRegDetail);
                }
            }

            public static bool IsDefaultEmptyPhysicianOnTransactionEntry
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsDefaultEmptyPhysicianOnTransactionEntry);
                }
            }

            public static bool IsAutoClosedOnPrApprovalZero
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAutoClosedOnPrApprovalZero);
                }
            }

            public static bool IsAutoChargeBedOnRegistration
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAutoChargeBedOnRegistration);
                }
            }

            public static bool IsItemBinIdAutoCreate
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsItemBinIdAutoCreate);
                }
            }

            public static bool IsArPaymentExcessToDiscount
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsArPaymentExcessToDiscount);
                }
            }

            public static string ServiceUnitLogisticCentralWarehouseId
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitLogisticCentralWarehouseId)); }
            }

            public static bool IsPORTaxTypeEnabled
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPORTaxTypeEnabled);
                }
            }

            public static bool IsPhysicianFeeVerifCorrectionAutoCheck
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPhysicianFeeVerifCorrectionAutoCheck);
                }
            }

            public static bool IsAllowInventoryIssueWithoutRequest
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAllowInventoryIssueWithoutRequest);
                }
            }

            public static bool IsInventoryIssueNeedConfirm
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsInventoryIssueNeedConfirm);
                }
            }

            public static bool IsPrescriptionLoadLastBought
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPrescriptionLoadLastBought);
                }
            }

            public static bool IsAllowRegistrationEmrChangePhysician
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAllowRegistrationEmrChangePhysician);
                }
            }

            public static bool IsPrescriptionReturnNoFormatBasedOnRegType
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPrescriptionReturnNoFormatBasedOnRegType);
                }
            }


            public static bool IsReOrderPoBasedOnPrWithSeparatePurchasingUnit
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsReOrderPoBasedOnPrWithSeparatePurchasingUnit);
                }
            }

            public static bool IsEnabledReferByPhyisicianOnRegistration
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsEnabledReferByPhyisicianOnRegistration);
                }
            }

            public static bool IsAllowVoidRegistrationOnTransfer
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAllowVoidRegistrationOnTransfer);
                }
            }


            public static bool IsGuarantorValidateCOA
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsGuarantorValidateCOA);
                }
            }

            public static bool IsUnmergeBillingCheckingIntermbillProcess
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUnmergeBillingCheckingIntermbillProcess);
                }
            }

            public static bool IsPaymentShowTransactionListForAllRegType
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPaymentShowTransactionListForAllRegType);
                }
            }

            public static bool IsValidateProductAccountOnItem
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsValidateProductAccountOnItem);
                }
            }

            public static bool IsRunTheCostCalculationCleanUpProcess
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsRunTheCostCalculationCleanUpProcess);
                }
            }

            public static bool IsCheckinConfirmationUsingDetails
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsCheckinConfirmationUsingDetails);
                }
            }

            public static string FirstTimeCheckMarkForTransfusionMonitoring
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.FirstTimeCheckMarkForTransfusionMonitoring)); }
            }

            public static string LblCaptionCheckMarkForTransfusionMonitoring
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.LblCaptionCheckMarkForTransfusionMonitoring)); }
            }

            public static bool IsBypassBloodCrossMatching
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsBypassBloodCrossMatching);
                }
            }

            public static bool IsNeedBloodSample
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsNeedBloodSample);
                }
            }

            public static bool IsNeedSpecimenOnJo
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsNeedSpecimenOnJo);
                }
            }

            public static bool IsPrescriptionReviewActived
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPrescriptionReviewActived);
                }
            }


            public static bool IsValidateBpjsCoveredItemOnTx
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsValidateBpjsCoveredItemOnTx);
                }
            }

            public static bool IsAutoApprovePackage
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAutoApprovePackage);
                }
            }
            public static bool IsByPassEmrUserTypeRestriction
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsByPassEmrUserTypeRestriction);
                }
            }
            public static bool IsMandatoryRegNoOnServiceUnitBooking
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsMandatoryRegNoOnServiceUnitBooking);
                }
            }

            public static bool IsDisplayRegNoOnServiceUnitBooking
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsDisplayRegNoOnServiceUnitBooking);
                }
            }

            public static int IntervalOrderWarning
            {
                get
                {
                    return System.Convert.ToInt32(GetParameterValue(AppParameter.ParameterItem.IntervalOrderWarning));
                }
            }

            public static int IntervalTrainingEvaluationSchedule
            {
                get
                {
                    return System.Convert.ToInt32(GetParameterValue(AppParameter.ParameterItem.IntervalTrainingEvaluationSchedule));
                }
            }

            public static decimal MultipleForRewardPoints
            {
                get { return Convert.ToDecimal(GetParameterValue(AppParameter.ParameterItem.MultipleForRewardPoints)); }
            }

            public static decimal MultipleForRewardPointsForEmployee
            {
                get { return Convert.ToDecimal(GetParameterValue(AppParameter.ParameterItem.MultipleForRewardPointsForEmployee)); }
            }

            public static decimal RewardPointsForPatientGeneral
            {
                get { return Convert.ToDecimal(GetParameterValue(AppParameter.ParameterItem.RewardPointsForPatientGeneral)); }
            }
            public static decimal RewardPointsForPatientGuarantee
            {
                get { return Convert.ToDecimal(GetParameterValue(AppParameter.ParameterItem.RewardPointsForPatientGuarantee)); }
            }

            public static string PrescriptionQueueStdiItemID
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PrescriptionQueueStdiItemID)); }
            }

            public static int ReservationMaxDuration
            {
                get
                {
                    return System.Convert.ToInt32(GetParameterValue(AppParameter.ParameterItem.ReservationMaxDuration));
                }
            }

            public static int ReservationMaxDurationForInternal
            {
                get
                {
                    return System.Convert.ToInt32(GetParameterValue(AppParameter.ParameterItem.ReservationMaxDurationForInternal));
                }
            }

            public static string[] DischargeMethodRefer
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DischargeMethodRefer)).Split(','); }
            }

            public static string DischargeMethodInCare
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DischargeMethodInCare)); }
            }

            public static string ItemIdBloodCrossMatching
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ItemIdBloodCrossMatching)); }
            }

            public static bool IsShowExternalQueue
            {
                get { return IsYes(AppParameter.ParameterItem.IsShowExternalQueue); }
            }

            public static string[] ProgramIdPatientLabel
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ProgramIdPatientLabel)).Split('|'); }
            }

            public static bool IsValidateDiagnosisOnRealizationOrderOp
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsValidateDiagnosisOnRealizationOrderOp);
                }
            }

            public static bool IsFoodSelectedByType
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsFoodSelectedByType);
                }
            }

            public static string PrescriptionOrderSlipID
            {
                get
                {
                    return GetParameterValue(AppParameter.ParameterItem.PrescriptionOrderSlipID).ToString();
                }
            }

            public static bool IsPaymentReceiveAllowBackdated
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPaymentReceiveAllowBackdated);
                }
            }

            public static double ExcessPaymentAmount
            {
                get { return Convert.ToDouble(GetParameterValue(AppParameter.ParameterItem.ExcessPaymentAmount)); }
            }

            public static bool IsAllowExcessPaymentAmountPlus
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAllowExcessPaymentAmountPlus);
                }
            }

            public static bool IsDemo
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsDemo);
                }
            }

            public static bool acc_IsJournalCashBased
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.acc_IsJournalCashBased);
                }
            }

            public static bool acc_IsEnableGuarDiscProrataToRevenue
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.acc_IsEnableGuarDiscProrataToRevenue);
                }
            }

            public static bool IsAntibioticRestriction
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAntibioticRestriction);
                }
            }
            public static bool IsRasproEnable
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsRasproEnable);
                }
            }

            public static string FoodGroupOneCarbohydrate
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.FoodGroupOneCarbohydrate)); }
            }

            public static string[] FoodGroupOneDishMeal
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.FoodGroupOneDishMeal)).Split(','); }
            }

            public static bool IsDisableInventoryStatusOnEditItemProduct
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsDisableInventoryStatusOnEditItemProduct);
                }
            }

            public static bool IsOvertimeUseApprovalLevel
            {
                get { return IsYes(AppParameter.ParameterItem.IsOvertimeUseApprovalLevel); }
            }

            //public static bool IsEmployeeLeaveUseTwoApprovalLevel
            //{
            //    get { return IsYes(AppParameter.ParameterItem.IsEmployeeLeaveUseTwoApprovalLevel); }
            //}

            public static string EmployeeLeaveApprovalLevel
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmployeeLeaveApprovalLevel)); }
            }

            public static bool IsUsingPreceptorAsProfessionalIndirectSupervisor
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingPreceptorAsProfessionalIndirectSupervisor); }
            }

            public static bool IsEmployeeLeavePayCutVisible
            {
                get { return IsYes(AppParameter.ParameterItem.IsEmployeeLeavePayCutVisible); }
            }

            public static string[] ParamedicTypeDoctors
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ParamedicTypeDoctors)).Split(','); }
            }

            public static string[] ItemGroupKitchen
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ItemGroupKitchen)).Split(','); }
            }

            public static bool IsVisibleItemGroupOnTx
            {
                get { return IsYes(AppParameter.ParameterItem.IsVisibleItemGroupOnTx); }
            }

            public static bool IsCollapsedPatientInformationOnBilling
            {
                get { return IsYes(AppParameter.ParameterItem.IsCollapsedPatientInformationOnBilling); }
            }

            public static bool IsCollapsedTransactionFilterOnBilling
            {
                get { return IsYes(AppParameter.ParameterItem.IsCollapsedTransactionFilterOnBilling); }
            }

            public static bool IsAutoChargeBedBasedOnDischargeDate
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutoChargeBedBasedOnDischargeDate); }
            }

            public static bool IsCafeAutoPrintPaymentReceive
            {
                get { return IsYes(AppParameter.ParameterItem.IsCafeAutoPrintPaymentReceive); }
            }
            public static string AppProgramCafePaymentReceive
            {
                get { return AppParameter.ParameterItem.AppProgramCafePaymentReceive.ToString(); }
            }

            public static string[] DistributionRequestBasedOnLocationToRestriction
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DistributionRequestBasedOnLocationToRestriction)).Split(','); }
            }

            public static string PurcOrderItemTypeRestrictionForItemSupplier
            {
                get
                {
                    return GetParameterValue(AppParameter.ParameterItem.PurcOrderItemTypeRestrictionForItemSupplier).ToString();
                }
            }

            public static bool IsValidatedSpecimenOnOrderRealization
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsValidatedSpecimenOnOrderRealization);
                }
            }
            public static bool IsReadOnlyDiscountOnPrescription
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsReadOnlyDiscountOnPrescription);
                }
            }

            public static bool IsVisibleBtnPurcReqOnDistribution
            {
                get { return IsYes(AppParameter.ParameterItem.IsVisibleBtnPurcReqOnDistribution); }
            }
            public static bool IsPurchaseRequestBasedOnItemsPerLocation
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPurchaseRequestBasedOnItemsPerLocation);
                }
            }

            public static bool IsPurchaseRequestBasedOnItemCategory
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPurchaseRequestBasedOnItemCategory);
                }
            }

            public static bool IsProcurementForItemMedicBasedOnInvCategory
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsProcurementForItemMedicBasedOnInvCategory);
                }
            }

            public static bool IsProcurementForItemNonMedicBasedOnInvCategory
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsProcurementForItemNonMedicBasedOnInvCategory);
                }
            }

            public static bool IsProcurementForItemKitchenBasedOnInvCategory
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsProcurementForItemKitchenBasedOnInvCategory);
                }
            }

            public static bool IsAllowDiscountOnTransEntry
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAllowDiscountOnTransEntry);
                }
            }

            public static bool IsPrintPatientCardOnNewBornInfant
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPrintPatientCardOnNewBornInfant);
                }
            }

            public static bool IsRegistrationInpatientOnlyForNewBornInfant
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsRegistrationInpatientOnlyForNewBornInfant);
                }
            }

            public static bool IsAdditionalMealOrderUsedClassMenuStandard
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAdditionalMealOrderUsedClassMenuStandard);
                }
            }

            public static bool IsAutoTransfusionBillProceedOnBloodDistribution
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAutoTransfusionBillProceedOnBloodDistribution);
                }
            }

            public static bool IsVisibleRequestTypeOnPurchaseRequestPicklist
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsVisibleRequestTypeOnPurchaseRequestPicklist);
                }
            }

            public static bool IsBridgingBillingBpjs
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsBridgingBillingBpjs);
                }
            }

            public static bool IsBridgingBillingBpjsWithCostSharing
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsBridgingBillingBpjsWithCostSharing);
                }
            }

            public static bool IsConsignmentReceivedItemBySupplier
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsConsignmentReceivedItemBySupplier);
                }
            }

            public static string DefaultGuarantorKiosk
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DefaultGuarantorKiosk)); }
            }

            public static string RegistrationTypeOuterEtiquettePrintRestrictions
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.RegistrationTypeOuterEtiquettePrintRestrictions)); }
            }

            public static string AjaxCounter
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AjaxCounter)); }
            }

            public static bool IsAllowSkipAutoBillOnRegistrationOpr
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAllowSkipAutoBillOnRegistrationOpr);
                }
            }

            public static string[] EmptyDoctorId
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmptyDoctorId)).Split(','); }
            }

            public static string DoctorOnDutyId
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DoctorOnDutyId)); }
            }

            public static bool IsAllowSubstituteDoctorOnRegistrationOpr
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAllowSubstituteDoctorOnRegistrationOpr);
                }
            }

            public static bool IsVisibleTrProcedureOnBookingRealization
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsVisibleTrProcedureOnBookingRealization);
                }
            }

            public static bool IsUsingMappingServiceUnitProcedure
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingMappingServiceUnitProcedure);
                }
            }

            public static bool IsPrescOrderHandlingBasedOnDispensary
            {
                get { return IsYes(AppParameter.ParameterItem.IsPrescOrderHandlingBasedOnDispensary); }
            }

            public static bool IsAllowDirectPrescOnInpatientSalesHandling
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAllowDirectPrescOnInpatientSalesHandling);
                }
            }

            public static bool IsShowRegConsulOnVerificationBilling
            {
                get { return IsYes(AppParameter.ParameterItem.IsShowRegConsulOnVerificationBilling); }
            }

            public static bool IsCreateFoodIdAutomatic
            {
                get { return IsYes(AppParameter.ParameterItem.IsCreateFoodIdAutomatic); }
            }

            public static bool IsVisibleAllAppointmentStatusOnList
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsVisibleAllAppointmentStatusOnList);
                }
            }

            public static bool IsNeedVoidReasonOnPaymentReceive
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsNeedVoidReasonOnPaymentReceive);
                }
            }

            public static bool IsVisibleKwi
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsVisibleKwi);
                }
            }

            public static bool IsUsingDoubleEmployeeNo
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingDoubleEmployeeNo);
                }
            }

            public static bool IsSeparateScheduleAndAttendanceSheet
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsSeparateScheduleAndAttendanceSheet);
                }
            }

            public static bool IsAplicaresByRoomName
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAplicaresByRoomName);
                }
            }

            public static bool IsHandHygieneNoteNoValidation
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsHandHygieneNoteNoValidation);
                }
            }

            public static string[] ItemGroupIDMedicationResume
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ItemGroupIDMedicationResume)).Split(','); }
            }

            public static string ApplicationDocumentFolder
            {
                get
                {
                    var appDocFolder = GetParameterValueString(AppParameter.ParameterItem.ApplicationDocumentFolder);
                    if (string.IsNullOrEmpty(appDocFolder))
                        appDocFolder = HttpContext.Current.Server.MapPath("~/App_Document");

                    return appDocFolder;
                }
            }

            public static string EmployeeDocumentFolder
            {
                get
                {
                    var appDocFolder = GetParameterValueString(AppParameter.ParameterItem.EmployeeDocumentFolder);
                    if (string.IsNullOrEmpty(appDocFolder))
                        appDocFolder = HttpContext.Current.Server.MapPath("~/App_Document");

                    return appDocFolder;
                }
            }

            public static string TmpDocumentFolder
            {
                get
                {
                    var tmp_doc = GetParameterValueString(AppParameter.ParameterItem.TmpDocumentFolder);
                    if (string.IsNullOrEmpty(tmp_doc))
                        tmp_doc = HttpContext.Current.Server.MapPath("~/App_Document");

                    return tmp_doc;
                }
            }

            public static string PerformancePlanDocumentFolder
            {
                get
                {
                    var appDocFolder = GetParameterValueString(AppParameter.ParameterItem.PerformancePlanDocumentFolder);
                    if (string.IsNullOrEmpty(appDocFolder))
                        appDocFolder = HttpContext.Current.Server.MapPath("~/App_Document");

                    return appDocFolder;
                }
            }

            public static string SoundFolder
            {
                get
                {
                    var appSFolder = GetParameterValueString(AppParameter.ParameterItem.SoundFolder);
                    if (string.IsNullOrEmpty(appSFolder))
                        throw new Exception("Parameter SoundFolder is not configured yet");
                    //appSFolder = HttpContext.Current.Server.MapPath("~/App_Document");

                    return appSFolder;
                }
            }
            public static string SoundFolderURL
            {
                get
                {
                    var appSFolder = GetParameterValueString(AppParameter.ParameterItem.SoundFolderURL);
                    if (string.IsNullOrEmpty(appSFolder))
                        throw new Exception("Parameter SoundFolder URL is not configured yet");

                    return appSFolder;
                }
            }

            public static bool IsShowPrintLabelOnTransEntry
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsShowPrintLabelOnTransEntry);
                }
            }
            public static string AppProgramServiceUnitPatientLabel
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AppProgramServiceUnitPatientLabel)); }
            }

            public static bool IsAutobillIprActivated
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAutobillIprActivated);
                }
            }

            public static bool IsThrIncludeInWageProcess
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsThrIncludeInWageProcess);
                }
            }

            public static bool IsAllowExecutionDateForward
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAllowExecutionDateForward);
                }
            }

            public static bool IsAutoChargeBedFilterLock
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutoChargeBedFilterLock); }
            }

            public static bool IsEklaimGroupUsingDefaultValue
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsEklaimGroupUsingDefaultValue);
                }
            }

            public static bool IsAutoInsertRegistrationNoteFromRegistration
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAutoInsertRegistrationNoteFromRegistration);
                }
            }

            public static bool IsUsingValidationOnServiveUnitBooking
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingValidationOnServiveUnitBooking);
                }
            }

            public static bool IsAssetDepreciationCreateByAccounting
            {
                get { return IsYes(AppParameter.ParameterItem.IsAssetDepreciationCreateByAccounting); }
            }

            public static bool IsDistributionOnlyBasedOnRequest
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsDistributionOnlyBasedOnRequest);
                }
            }

            public static bool IsDistributionRequestOnlyForUnderMinValue
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsDistributionRequestOnlyForUnderMinValue);
                }
            }

            public static bool IsDistributionRequestMustNotExceedCWStock
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsDistributionRequestMustNotExceedCWStock);
                }
            }


            public static string ServiceUnitSanitationId
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitSanitationId)); }
            }

            public static string[] ServiceUnitPurchasingId
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ServiceUnitPurchasingId)).Split(','); }
            }

            public static bool IsUsingSingleUnitIPSRS
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingSingleUnitIPSRS);
                }
            }

            public static string WorkOrderRealizationAutoGenerateTx
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.WorkOrderRealizationAutoGenerateTx)); }
            }

            public static bool IsUsingCentralizedPurchaseRequest
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingCentralizedPurchaseRequest);
                }
            }

            public static string ItemUnitKg
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ItemUnitKg)); }
            }

            public static bool IsRptInPreviewMode
            {
                get { return IsYes(AppParameter.ParameterItem.IsRptInPreviewMode); }
            }
            public static bool IsNsOutcomeShowScale
            {
                get { return IsYes(AppParameter.ParameterItem.IsNsOutcomeShowScale); }
            }

            public static string EmployeeIncidentTypeNSI
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmployeeIncidentTypeNSI)); }
            }

            public static string[] EmployeeIncidentTypeEBF
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmployeeIncidentTypeEBF)).Split(','); }
            }

            public static string[] NeedleTypeNSI
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.NeedleTypeNSI)).Split(','); }
            }

            public static string EmployeeStatusActive
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmployeeStatusActive)); }
            }

            public static string EmployeeRelationshipSelf
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmployeeRelationshipSelf)); }
            }

            public static string EmployeeLeaveAnnualLeave
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmployeeLeaveAnnualLeave)); }
            }

            public static string EmployeeAnnualLeaveStartPeriod
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmployeeAnnualLeaveStartPeriod)); }
            }

            public static string EmploymentTypePermanent
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmploymentTypePermanent)); }
            }

            public static string EmploymentTypeForAnnualLeave
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmploymentTypeForAnnualLeave)); }
            }

            public static string PersonalLicenseTypeSTR
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PersonalLicenseTypeSTR)); }
            }

            public static string PersonalLicenseTypeSPK
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PersonalLicenseTypeSPK)); }
            }

            public static bool IsAllowEditEmployeeAnnualLeaveEndPeriod
            {
                get { return IsYes(AppParameter.ParameterItem.IsAllowEditEmployeeAnnualLeaveEndPeriod); }
            }

            public static bool IsUsingBKUModule
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingBKUModule); }
            }

            public static bool IsUserTypeDoctorNoSaveConfirm
            {
                get { return IsYes(AppParameter.ParameterItem.IsUserTypeDoctorNoSaveConfirm); }
            }

            public static bool IsCentralizedCssd
            {
                get { return IsYes(AppParameter.ParameterItem.IsCentralizedCssd); }
            }

            public static bool IsShowSystemQtyInCssdStockTacking
            {
                get { return IsYes(AppParameter.ParameterItem.IsShowSystemQtyInCssdStockTacking); }
            }

            public static string NsOutcome
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.NsOutcome)); }
            }
            public static string NsOutcome02
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.NsOutcome02)); }
            }
            public static string NsIntervention
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.NsIntervention)); }
            }

            public static string AppraisalVersionNo
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AppraisalVersionNo)); }
            }

            public static bool IsUsingFourLevelOrganizationUnit
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingFourLevelOrganizationUnit);
                }
            }

            public static bool IsFilterVehicleAndDriverOnScheduled
            {
                get { return IsYes(AppParameter.ParameterItem.IsFilterVehicleAndDriverOnScheduled); }
            }

            public static string[] EmployeeTypeForLogbook
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmployeeTypeForLogbook)).Split(','); }
            }

            public static bool IsKioskEnableBPJS
            {
                get { return IsYes(AppParameter.ParameterItem.IsKioskEnableBPJS); }
            }

            public static bool IsLogProgramAccess
            {
                get { return IsYes(AppParameter.ParameterItem.IsLogProgramAccess); }
            }

            public static bool IsKioskEnableQRCode
            {
                get { return IsYes(AppParameter.ParameterItem.IsKioskEnableQRCode); }
            }

            public static bool IsFeeCalculateProporsionalOnPayment
            {
                get { return IsYes(AppParameter.ParameterItem.IsFeeCalculateProporsionalOnPayment); }
            }

            public static bool IsFeeEnableRemunByGuarantor
            {
                get { return IsYes(AppParameter.ParameterItem.IsFeeEnableRemunByGuarantor); }
            }
            public static bool IsFeeEnableDualBruto
            {
                get { return IsYes(AppParameter.ParameterItem.IsFeeEnableDualBruto); }
            }

            public static bool IsFeeTaxProgressiveMonthly
            {
                get { return IsYes(AppParameter.ParameterItem.IsFeeTaxProgressiveMonthly); }
            }

            public static string CssdSenderBySelf
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.CssdSenderBySelf)); }
            }

            public static string CssdSenderByOtherUnit
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.CssdSenderByOtherUnit)); }
            }

            public static bool IsUsingEmployeeNeedleStickInjuryFollowUp
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingEmployeeNeedleStickInjuryFollowUp);
                }
            }

            public static bool IsAllowVoidServiceUnitBookingRealization
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAllowVoidServiceUnitBookingRealization);
                }
            }

            public static bool IsValidateEdOnDistribution
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsValidateEdOnDistribution);
                }
            }

            public static bool IsEnabledStockWithEdControl
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsEnabledStockWithEdControl);
                }
            }

            public static decimal BudgetOfAssetNeedExtraApprovalLimit
            {
                get { return System.Convert.ToDecimal(GetParameterValue(AppParameter.ParameterItem.BudgetOfAssetNeedExtraApprovalLimit)); }
            }

            public static int DayLimitValidationServiceUnitBooking
            {
                get { return System.Convert.ToInt32(GetParameterValue(AppParameter.ParameterItem.DayLimitValidationServiceUnitBooking)); }
            }

            public static bool IsParamedicFeePaymentEnableDraft
            {
                get { return IsYes(AppParameter.ParameterItem.IsParamedicFeePaymentEnableDraft); }
            }

            public static bool IsParamedicFeePaymentEnableGuaranteeFee
            {
                get { return IsYes(AppParameter.ParameterItem.IsParamedicFeePaymentEnableGuaranteeFee); }
            }

            public static bool acc_IsJournalPayrollWithDirectIndirectCost
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.acc_IsJournalPayrollWithDirectIndirectCost);
                }
            }

            public static bool acc_IsAutoJournalPayroll
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.acc_IsAutoJournalPayroll);
                }
            }

            public static bool acc_IsJournalPayrollWithDefaultNormalBalance
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.acc_IsJournalPayrollWithDefaultNormalBalance);
                }
            }

            public static bool acc_IsAutoJournalAssetDestruction
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.acc_IsAutoJournalAssetDestruction);
                }
            }

            public static bool acc_IsAutoJournalAssetAuction
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.acc_IsAutoJournalAssetAuction);
                }
            }

            public static bool acc_IsAutoJournalAssetMovement
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.acc_IsAutoJournalAssetMovement);
                }
            }

            public static bool IsAutomaticChargeBedReprocessIncludeAutoBillItem
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutomaticChargeBedReprocessIncludeAutoBillItem); }
            }

            public static bool IsShowCrossMatchingPrintLabel
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsShowCrossMatchingPrintLabel);
                }
            }

            public static int MaxLosToDisplayTransactionList
            {
                get { return Convert.ToInt32(GetParameterValue(AppParameter.ParameterItem.MaxLosToDisplayTransactionList)); }
            }

            public static string DiscountReasonSelisihKlaimBpjs
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DiscountReasonSelisihKlaimBpjs)); }
            }

            public static bool IsPatientBpjsNoMandatory
            {
                get { return IsYes(AppParameter.ParameterItem.IsPatientBpjsNoMandatory); }
            }

            public static string SOPDirectoryUrl
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.SOPDirectoryUrl)); }
            }

            public static bool IsUsingLimitQuotaInPhysicianSchedule
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingLimitQuotaInPhysicianSchedule);
                }
            }

            public static bool IsPathologyAnatomyDiagnoseFreeText
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPathologyAnatomyDiagnoseFreeText);
                }
            }

            public static bool IsPathologyAnatomyLocationFreeText
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPathologyAnatomyLocationFreeText);
                }
            }

            public static bool IsPathologyAnatomyWithImpressionResult
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPathologyAnatomyWithImpressionResult);
                }
            }

            public static bool IsPathologyAnatomyIhkWithMammaeResult
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPathologyAnatomyIhkWithMammaeResult);
                }
            }

            public static bool IsPathologyAnatomyWithTestResult
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPathologyAnatomyWithTestResult);
                }
            }

            public static int DayLimitDefaultDiagAndProcList
            {
                get { return System.Convert.ToInt32(GetParameterValue(AppParameter.ParameterItem.DayLimitDefaultDiagAndProcList)); }
            }

            public static int AppointmentGetListDateRangeLimit
            {
                get { return System.Convert.ToInt32(GetParameterValue(AppParameter.ParameterItem.AppointmentGetListDateRangeLimit)); }
            }

            public static bool IsAllowDoubleItemServiceOnTxEntry
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAllowDoubleItemServiceOnTxEntry);
                }
            }

            public static bool IsVisiblePrintBillingPaymentPermit
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsVisiblePrintBillingPaymentPermit);
                }
            }

            public static string JournalEntrySearchRangeFilter
            {
                get
                {
                    return GetParameterValue(AppParameter.ParameterItem.JournalEntrySearchRangeFilter).ToString();
                }
            }

            public static bool IsAccountReceivableByDischargeDate
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAccountReceivableByDischargeDate);
                }
            }

            public static bool IsValidateMaxQtyItemConsumptions
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsValidateMaxQtyItemConsumptions);
                }
            }

            public static bool IsEnabledDispensaryOnPrescriptionOrderRealization
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsEnabledDispensaryOnPrescriptionOrderRealization);
                }
            }

            public static bool IsUsingFactoryInTheItemProcurementProcess
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingFactoryInTheItemProcurementProcess);
                }
            }

            public static bool IsVisibleOtc
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsVisibleOtc);
                }
            }

            public static bool IsVisibleGuarantorAutoBillItem
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsVisibleGuarantorAutoBillItem);
                }
            }

            public static bool IsJobOrderRealizationListByOrderDate
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsJobOrderRealizationListByOrderDate);
                }
            }

            public static bool IsInventoryIssueListByTransactionDate
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsInventoryIssueListByTransactionDate);
                }
            }

            public static bool IsARPaymentShowRemaining
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsARPaymentShowRemaining);
                }
            }

            public static bool IsClosingApAdvanceWithPayment
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsClosingApAdvanceWithPayment);
                }
            }

            public static bool IsClosingApZeroWithPayment
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsClosingApZeroWithPayment);
                }
            }

            public static bool IsUsingNewDuplicatePatientDataChecking
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingNewDuplicatePatientDataChecking);
                }
            }

            public static bool IsUsingUserAccessForEditPatient
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingUserAccessForEditPatient);
                }
            }

            public static bool IsSeparateLaboratoryUnit
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsSeparateLaboratoryUnit);
                }
            }

            public static bool IsAncillaryServicePhysicianSenderFreeText
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAncillaryServicePhysicianSenderFreeText);
                }
            }

            public static bool IsApInvoiceIncPPN
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsApInvoiceIncPPN);
                }
            }

            public static bool IsRegistrationLinkToPatientDocument
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsRegistrationLinkToPatientDocument);
                }
            }

            public static bool IsUsingValidationImplementationDateTimeOnPpaNotes
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingValidationImplementationDateTimeOnPpaNotes); }
            }

            public static bool IsAutoCreateNewPrescriptionTxOnUnapproval
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAutoCreateNewPrescriptionTxOnUnapproval);
                }
            }

            public static bool IsUsingValidationOnServiceUnitBookingRealization
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingValidationOnServiceUnitBookingRealization);
                }
            }

            public static bool IsEmrListUsingExternalQueNo
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsEmrListUsingExternalQueNo);
                }
            }

            public static string HaisMonitoringProgramName
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.HaisMonitoringProgramName)); }
            }

            public static string AppointmentTypeControlPlan
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AppointmentTypeControlPlan)); }
            }

            public static string AppointmentTypeWebService
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AppointmentTypeWebService)); }
            }

            public static bool IsItemPickerListOrderByName
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsItemPickerListOrderByName);
                }
            }

            public static bool IsUsingProcurementTypeInPO
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingProcurementTypeInPO);
                }
            }

            public static bool IsFilterPrescUddListOnlyWithValidTx
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsFilterPrescUddListOnlyWithValidTx);
                }
            }

            public static bool IsPathologyAnatomyResultTypeCanBeMoreThanOne
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPathologyAnatomyResultTypeCanBeMoreThanOne);
                }
            }

            public static bool IsAllowEditPorAmountOnApInvoice
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAllowEditPorAmountOnApInvoice);
                }
            }

            public static bool IsCanProcessExceededRequestOnInventoryIssueOut
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsCanProcessExceededRequestOnInventoryIssueOut);
                }
            }

            public static bool IsUsingItemSubGroup
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingItemSubGroup);
                }
            }

            public static string ExcelFileExtension
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ExcelFileExtension)); }
            }

            public static bool IsReadonlyStockQtyOnTransChargesItem
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsReadonlyStockQtyOnTransChargesItem);
                }
            }

            public static string EmailSender
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmailSender)); }
            }

            public static bool IsNeedVoidReasonOnArInvoicing
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsNeedVoidReasonOnArInvoicing);
                }
            }

            public static bool IsJobOrderRealizationListByCitoStatus
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsJobOrderRealizationListByCitoStatus);
                }
            }

            public static bool IsShowPrescriptionHistoryOnRegistration
            {
                get { return IsYes(AppParameter.ParameterItem.IsShowPrescriptionHistoryOnRegistration); }
            }

            public static bool IsUsingGoogleForm
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingGoogleForm); }
            }

            public static bool IsVisibleGuarantorFilterOnPlafondInformationList
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsVisibleGuarantorFilterOnPlafondInformationList);
                }
            }

            public static bool IsVisibleClinicalDiagnosisOnJobOrderRealization
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsVisibleClinicalDiagnosisOnJobOrderRealization);
                }
            }

            public static bool IsVisible23PrescFilterOnPlafondInformationList
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsVisible23PrescFilterOnPlafondInformationList);
                }
            }

            public static bool IsVisibleTemplateForDirectPrescription
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsVisibleTemplateForDirectPrescription);
                }
            }

            public static bool IsMandatoryConsTime
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsMandatoryConsTime);
                }
            }

            public static bool IsUsingCheckListForMatrixServiceUnitItemService
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingCheckListForMatrixServiceUnitItemService);
                }
            }

            public static bool acc_IsUsingBkuAccount
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.acc_IsUsingBkuAccount);
                }
            }

            public static bool acc_IsCoaInvoiceGuarantorSplitIprOpr
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.acc_IsCoaInvoiceGuarantorSplitIprOpr);
                }
            }

            public static string DiscountReasonBillRounding
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.DiscountReasonBillRounding)); }
            }

            public static bool IsPrescriptionDiscountAfterRounding
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPrescriptionDiscountAfterRounding);
                }
            }

            public static bool IsUsingBillingSlipInEnglish
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingBillingSlipInEnglish);
                }
            }

            public static bool IsFoodSelectedByMenuItemFoodGroup
            {
                get { return IsYes(AppParameter.ParameterItem.IsFoodSelectedByMenuItemFoodGroup); }
            }

            public static string MenuItemFoodGroupStandard
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.MenuItemFoodGroupStandard)); }
            }

            public static bool IsMandatoryDistributionTypeOnDirectDistribution
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsMandatoryDistributionTypeOnDirectDistribution);
                }
            }

            public static bool IsUsedPrintSlipLogForBillingStatement
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsedPrintSlipLogForBillingStatement);
                }
            }

            public static bool IsUsedPrintSlipLogForPaymentReceipt
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsedPrintSlipLogForPaymentReceipt);
                }
            }

            public static bool IsUsingAssetIdNewNumberingFormat
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingAssetIdNewNumberingFormat);
                }
            }

            public static bool IsUsingAssetIdNumberingFormatWithSplitCategory
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingAssetIdNumberingFormatWithSplitCategory);
                }
            }

            public static bool IsUsingRisPacsInterop
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingRisPacsInterop);
                }
            }

            public static bool IsPorTaxBasedOnPo
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPorTaxBasedOnPo);
                }
            }

            public static bool IsUsingItemSubBin
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingItemSubBin);
                }
            }

            public static bool IsParamedicFeeVerifPaymentFilterByClosingBilling
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsParamedicFeeVerifPaymentFilterByClosingBilling);
                }
            }

            public static bool IsShowRealizationOrderTransactionStatus
            {
                get { return IsYes(AppParameter.ParameterItem.IsShowRealizationOrderTransactionStatus); }
            }

            public static bool IsUsingValidationUserAccessOnPaymentReceive
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingValidationUserAccessOnPaymentReceive); }
            }

            public static bool IsUsingValidationPendingBalance
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingValidationPendingBalance); }
            }

            public static bool IsPrescriptionQueueForInpatient
            {
                get { return IsYes(AppParameter.ParameterItem.IsPrescriptionQueueForInpatient); }
            }

            public static bool IsJobOrderRealizationListWith2Tabs
            {
                get { return IsYes(AppParameter.ParameterItem.IsJobOrderRealizationListWith2Tabs); }
            }

            public static bool IsShowInfoTotalPatientRegistration
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsShowInfoTotalPatientRegistration);
                }
            }

            public static bool IsDistributionRequestUsingPickFromUsedHistoryV2
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsDistributionRequestUsingPickFromUsedHistoryV2);
                }
            }

            public static int IntervalRefreshPrescriptionOrderList
            {
                get { return System.Convert.ToInt32(GetParameterValue(AppParameter.ParameterItem.IntervalRefreshPrescriptionOrderList)); }
            }

            public static bool IsTestResultListWithDefaultOutstanding
            {
                get { return IsYes(AppParameter.ParameterItem.IsTestResultListWithDefaultOutstanding); }
            }

            public static bool IsRegistrationListWithCreatedDateTime
            {
                get { return IsYes(AppParameter.ParameterItem.IsRegistrationListWithCreatedDateTime); }
            }

            public static bool IsBillingStatementLosCalculationWithAdd1Day
            {
                get { return IsYes(AppParameter.ParameterItem.IsBillingStatementLosCalculationWithAdd1Day); }
            }

            public static bool IsBillingStatementRegDateUsingCheckinConfirmed
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsBillingStatementRegDateUsingCheckinConfirmed);
                }
            }

            public static bool IsPrescriptionUnApprovalCreateNewNumber
            {
                get { return IsYes(AppParameter.ParameterItem.IsPrescriptionUnApprovalCreateNewNumber); }
            }

            public static bool IsDistributionRequestPickListWithBalanceToInfo
            {
                get { return IsYes(AppParameter.ParameterItem.IsDistributionRequestPickListWithBalanceToInfo); }
            }

            public static bool IsUsingDefaultConsumeMethodFor23DaysPrescription
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingDefaultConsumeMethodFor23DaysPrescription); }
            }

            public static bool IsPrescriptionSplitBillActived
            {
                get { return IsYes(AppParameter.ParameterItem.IsPrescriptionSplitBillActived); }
            }

            public static bool IsSharePurchaseDiscToCustomer
            {
                get { return IsYes(AppParameter.ParameterItem.IsSharePurchaseDiscToCustomer); }
            }

            public static bool IsLockLocationPharmacy
            {
                get { return IsYes(AppParameter.ParameterItem.IsLockLocationPharmacy); }
            }

            public static bool IsPatientOprOnPrescSalesForPolyclinicOnly
            {
                get { return IsYes(AppParameter.ParameterItem.IsPatientOprOnPrescSalesForPolyclinicOnly); }
            }

            public static bool IsPorUsingChecklistItem
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPorUsingChecklistItem);
                }
            }

            public static bool IsShowPrintLabel1InJobOrderRealizationList
            {
                get { return IsYes(AppParameter.ParameterItem.IsShowPrintLabel1InJobOrderRealizationList); }
            }

            public static bool IsShowBalanceInfoInDistributionRequest
            {
                get { return IsYes(AppParameter.ParameterItem.IsShowBalanceInfoInDistributionRequest); }
            }

            public static string[] GuarantorIdExeptionForRecipeAmount
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.GuarantorIdExeptionForRecipeAmount)).Split(','); }
            }

            public static bool IsMandatoryPrescriptionCategory
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsMandatoryPrescriptionCategory);
                }
            }

            public static bool IsMandatoryDrugAllergen
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsMandatoryDrugAllergen);
                }
            }

            public static int DayLimitEmployeeLicenseWarning
            {
                get { return System.Convert.ToInt32(GetParameterValue(AppParameter.ParameterItem.DayLimitEmployeeLicenseWarning)); }
            }

            public static string QuestionFormEmployeeSafetyCultureIncidentReports
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.QuestionFormEmployeeSafetyCultureIncidentReports)); }
            }

            public static string QuestionFormPatientIdentificationCompliance
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.QuestionFormPatientIdentificationCompliance)); }
            }

            public static string QuestionFormCredentialing
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.QuestionFormCredentialing)); }
            }

            public static bool IsVoucherListShowVoid
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsVoucherListShowVoid);
                }
            }

            public static bool IsServiceUnitBookingUsingBodyDiagramServiceUnit
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsServiceUnitBookingUsingBodyDiagramServiceUnit);
                }
            }

            public static bool IsAutoDeleteBalanceOnInActiveItem
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAutoDeleteBalanceOnInActiveItem);
                }
            }

            public static decimal acc_AssetDepreciationAmountLimit
            {
                get { return System.Convert.ToDecimal(GetParameterValue(AppParameter.ParameterItem.acc_AssetDepreciationAmountLimit)); }
            }

            public static bool IsUseApprovalLevelforPOWithUserRestriction
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUseApprovalLevelforPOWithUserRestriction);
                }
            }

            public static string PrefixOnoSysmexInterop
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PrefixOnoSysmexInterop)); }
            }

            public static bool IsItemPickerListOrderUsingGroupButton
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsItemPickerListOrderUsingGroupButton);
                }
            }

            public static bool IsDefaultEmptyDateOnEKlaimList
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsDefaultEmptyDateOnEKlaimList);
                }
            }

            public static bool IsCentralizedLaundrie
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsCentralizedLaundrie);
                }
            }

            public static bool IsMandatoryInterventionReason
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsMandatoryInterventionReason);
                }
            }

            public static bool IsCssdExpiredValidateInReceiveDetail
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsCssdExpiredValidateInReceiveDetail);
                }
            }

            public static bool IsCssdUsingDttTerm
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsCssdUsingDttTerm);
                }
            }

            public static bool IsCssdStockValidateInDistribution
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsCssdStockValidateInDistribution);
                }
            }


            public static bool IsPromoPackageActivated
            {
                get { return IsYes(AppParameter.ParameterItem.IsPromoPackageActivated); }
            }

            public static bool IsPersonalWorkExperienceUsingDatePeriod
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPersonalWorkExperienceUsingDatePeriod);
                }
            }

            public static bool IsAllowSanitationWasteBalanceMinus
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAllowSanitationWasteBalanceMinus);
                }
            }

            public static string PersonalContactTypeEmail
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.PersonalContactTypeEmail)); }
            }

            public static bool IsShowArReceiptInVerificationAndPaymentList
            {
                get { return IsYes(AppParameter.ParameterItem.IsShowArReceiptInVerificationAndPaymentList); }
            }

            public static bool IsSaveHistoryInImportBpjsVerification
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsSaveHistoryInImportBpjsVerification);
                }
            }

            public static bool IsVerificationBillingAuthorizationActivated
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsVerificationBillingAuthorizationActivated);
                }
            }

            public static Int16 MaximumQtyBloodBagRequestPassedCasemix
            {
                get { return System.Convert.ToInt16(GetParameterValue(AppParameter.ParameterItem.MaximumQtyBloodBagRequestPassedCasemix)); }
            }

            public static bool IsReferToSpecialistCanEntryByUserNonPhsycian
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsReferToSpecialistCanEntryByUserNonPhsycian);
                }
            }

            public static string[] PurchaseRequestOutstandingListOrderBy
            {
                get { return GetParameterValueString(AppParameter.ParameterItem.PurchaseRequestOutstandingListOrderBy).ToString().Split(','); }
            }

            public static bool IsDistributionOnlyInStock
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsDistributionOnlyInStock);
                }
            }

            public static bool IsAutoInsertToEmployeePeriodicSalaryOvertime
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAutoInsertToEmployeePeriodicSalaryOvertime);
                }
            }

            public static bool KPI_IsShowDenum
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.KPI_IsShowDenum);
                }
            }

            public static bool IsCrmMembershipActive
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsCrmMembershipActive);
                }
            }

            public static bool IsCredentialingWithPrerequisite
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsCredentialingWithPrerequisite);
                }
            }

            public static bool IsCompetencyAssessmentUsingSingleEvaluator
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsCompetencyAssessmentUsingSingleEvaluator);
                }
            }

            public static string EmploymentTypeCi
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmploymentTypeCi)); }
            }

            public static string EmployeeStatusInActive
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.EmployeeStatusInActive)); }
            }

            public static string EmployeeProfessionGroupMedical
            {
                get
                {
                    return GetParameterValueString(AppParameter.ParameterItem.EmployeeProfessionGroupMedical).ToString();
                }
            }

            public static string EmployeeProfessionGroupNursing
            {
                get
                {
                    return GetParameterValueString(AppParameter.ParameterItem.EmployeeProfessionGroupNursing).ToString();
                }
            }

            public static string EmployeeProfessionGroupKtkl
            {
                get
                {
                    return GetParameterValueString(AppParameter.ParameterItem.EmployeeProfessionGroupKtkl).ToString();
                }
            }

            public static bool IsValidateEmployeeLeaveWithPayCutCantCrossMonth
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsValidateEmployeeLeaveWithPayCutCantCrossMonth);
                }
            }


            public static bool IsAllowEditAssetGroup
            {
                get { return IsYes(AppParameter.ParameterItem.IsAllowEditAssetGroup); }
            }

            public static bool IsPatientTransferUsingFilterToClass
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsPatientTransferUsingFilterToClass);
                }
            }
            public static decimal RemunBudgedPercentage
            {
                get { return System.Convert.ToDecimal(GetParameterValue(AppParameter.ParameterItem.RemunBudgedPercentage)); }
            }

            public static bool IsAutoRecruitmentPlanName
            {
                get { return IsYes(AppParameter.ParameterItem.IsAutoRecruitmentPlanName); }
            }

            public static bool IsDisableClassOnRequestChangeItemProduct
            {
                get { return IsYes(AppParameter.ParameterItem.IsDisableClassOnRequestChangeItemProduct); }
            }

            public static bool IsAllPhysicianOnSbar
            {
                get { return IsYes(AppParameter.ParameterItem.IsAllPhysicianOnSbar); }
            }

            public static bool IsAutoKioskQueueStatusSkippedForPrescription
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAutoKioskQueueStatusSkippedForPrescription);
                }
            }

            public static string QueueDisplayScrollingText
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.QueueDisplayScrollingText)); }
            }

            public static string QueueDisplayScrollingDurationText
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.QueueDisplayScrollingDurationText)); }
            }

            public static string QueueDisplaySloganHealthcare
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.QueueDisplaySloganHealthcare)); }
            }

            public static bool IsCheckallDistributedPrint
            {
                get { return IsYes(AppParameter.ParameterItem.IsCheckallDistributedPrint); }
            }

            public static bool IsUsingRoundingPaymentAP
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingRoundingPaymentAP); }
            }

            public static bool IsUsingParamedicFeeByTeam
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingParamedicFeeByTeam); }
            }

            public static bool IsUsingGuarantorPrefixForKioskV2
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingGuarantorPrefixForQueueCodeKioskV2); }
            }

            public static bool IsValidateParamedicSBAR
            {
                get { return IsYes(AppParameter.ParameterItem.IsValidateParamedicSBAR); }
            }

            public static bool IsAllowEditPatientFromVerificationBilling
            {
                get { return IsYes(AppParameter.ParameterItem.IsAllowEditPatientFromVerificationBilling); }
            }

            public static bool IsUsingQueueCodeByPhysicianKioskV2
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingQueueCodeByPhysicianKioskV2); }
            }

            public static bool IsEnabledAddNewItemCSSD
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsEnabledAddNewItemCSSD);
                }
            }

            public static bool IsNewBudgetingAutomatisItemMasterProduct
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsNewBudgetingAutomatisItemMasterProduct);
                }
            }

            public static bool IsUsingKioskQueNoFormat
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsUsingKioskQueNoFormat);
                }
            }

            public static bool IsAntrolCreateRegistrationQueue
            {
                get
                {
                    return IsYes(AppParameter.ParameterItem.IsAntrolCreateRegistrationQueue);
                }
            }
            public static string AssessmentTypeIDsForShowPanelFdolm
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.AssessmentTypeIDsForShowPanelFdolm)); }
            }

            public static bool IsUsingSplitPainScaleAndFlaccBasedOnAge
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingSplitPainScaleAndFlaccBasedOnAge); }
            }

            public static int SplitPainScaleAndFlaccAgeValue
            {
                get { return Convert.ToInt32(GetParameterValue(AppParameter.ParameterItem.SplitPainScaleAndFlaccAgeValue)); }
            }

            public static string[] PrescriptionDisplayColumnsDef
            {
                get
                {
                    var strs = GetParameterValue(AppParameter.ParameterItem.PrescriptionDisplayColumnsDef).ToString().Split(',');
                    for (var i = 0; i < strs.Length; i++)
                    {
                        strs[i] = strs[i].Trim();
                    }
                    return strs;
                }
            }

            public static int MaxChronicDrugPrescriptionInDays
            {
                get { return Convert.ToInt32(GetParameterValue(AppParameter.ParameterItem.MaxChronicDrugPrescriptionInDays)); }
            }

            public static string ValueForTakingQueueBeforeStartTime
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.ValueForTakingQueueBeforeStartTime)); }
            }

            public static bool IsMultipleSynonymValueForDiagnoseAndProcedure
            {
                get { return IsYes(AppParameter.ParameterItem.IsMultipleSynonymValueForDiagnoseAndProcedure); }
            }

            public static string TablePatientBirthRecordFieldValidation
            {
                get { return Convert.ToString(GetParameterValue(AppParameter.ParameterItem.TablePatientBirthRecordFieldValidation)); }
            }

            public static bool IsUsingMultipleScoringSupervisor
            {
                get { return IsYes(AppParameter.ParameterItem.IsUsingMultipleScoringSupervisor); }
            }
        }

        public class Application
        {
            public static string Skin
            {
                get
                {
                    var parName = "p_Skin";
                    object obj = HttpContext.Current.Cache[parName];
                    if (obj == null)
                    {
                        var skinStyle = ConfigurationSettings.AppSettings["DefaultStyle"];
                        HttpContext.Current.Cache[parName] = skinStyle;
                    }
                    return (string)HttpContext.Current.Cache[parName];
                }
            }

            public static bool IsNewMenuStyle
            {
                get
                {
                    var parName = "p_IsNewMenuStyle";
                    object obj = HttpContext.Current.Cache[parName];
                    if (obj == null)
                    {
                        var val = ConfigurationSettings.AppSettings["IsNewMenuStyle"].Trim().ToLower();
                        HttpContext.Current.Cache[parName] = (val == "yes" || val == "true");
                    }
                    return (bool)HttpContext.Current.Cache[parName];
                }
            }

            public static string VendorIdentifier
            {
                get
                {
                    var parName = "p_VendorIdentifier";
                    object obj = HttpContext.Current.Cache[parName];
                    if (obj == null)
                    {
                        var vendorIdentifier = ConfigurationSettings.AppSettings["VendorIdentifier"];
                        HttpContext.Current.Cache[parName] = vendorIdentifier;
                    }
                    return (string)HttpContext.Current.Cache[parName];
                }
            }

            public static bool IsHisMode
            {
                get
                {
                    var parName = "p_IsHisMode";
                    object obj = HttpContext.Current.Cache[parName];
                    if (obj == null)
                    {
                        var skinStyle = ConfigurationSettings.AppSettings["DefaultStyle"].Trim().ToLower();
                        var vendorIdentifier = ConfigurationSettings.AppSettings["VendorIdentifier"];
                        HttpContext.Current.Cache[parName] = (skinStyle == "webblue" && vendorIdentifier == "0");
                    }

                    return (bool)HttpContext.Current.Cache[parName];
                }
            }

            public static bool IsModuleLaundryActive
            {
                get
                {
                    var parName = "p_IsModuleLaundryActive";
                    object obj = HttpContext.Current.Cache[parName];
                    if (obj == null)
                    {
                        var isActive = false;
                        var appprg = new AppProgram();
                        if (appprg.LoadByPrimaryKey(AppConstant.Program.LaundererProcess) && appprg.IsVisible == true)
                            isActive = true;

                        HttpContext.Current.Cache[parName] = (isActive == true);
                    }

                    return (bool)HttpContext.Current.Cache[parName];
                }
            }

            public static bool IsModuleCredentialActive
            {
                get
                {
                    var parName = "p_IsModuleCredentialActive";
                    object obj = HttpContext.Current.Cache[parName];
                    if (obj == null)
                    {
                        var isActive = false;
                        var appprg = new AppProgram();
                        if (appprg.LoadByPrimaryKey(AppConstant.Program.CredentialApplication) && appprg.IsVisible == true)
                            isActive = true;

                        HttpContext.Current.Cache[parName] = (isActive == true);
                    }

                    return (bool)HttpContext.Current.Cache[parName];
                }
            }

            public static bool IsModuleCredential2Active
            {
                get
                {
                    var parName = "p_IsModuleCredential2Active";
                    object obj = HttpContext.Current.Cache[parName];
                    if (obj == null)
                    {
                        var isActive = false;
                        var appprg = new AppProgram();
                        if (appprg.LoadByPrimaryKey(AppConstant.Program.CredentialApplication2) && appprg.IsVisible == true)
                            isActive = true;

                        HttpContext.Current.Cache[parName] = (isActive == true);
                    }

                    return (bool)HttpContext.Current.Cache[parName];
                }
            }

            public static bool IsMenuClinicalPerformanceAppraisalActive
            {
                get
                {
                    var parName = "p_IsMenuClinicalPerformanceAppraisalActive";
                    object obj = HttpContext.Current.Cache[parName];
                    if (obj == null)
                    {
                        var isActive = false;
                        var appprg = new AppProgram();
                        if (appprg.LoadByPrimaryKey(AppConstant.Program.ClinicalPerformanceAppraisalScoresheet) && appprg.IsVisible == true)
                            isActive = true;

                        HttpContext.Current.Cache[parName] = (isActive == true);
                    }

                    return (bool)HttpContext.Current.Cache[parName];
                }
            }

            public static bool IsMenuCssdDecontaminationActive
            {
                get
                {
                    var parName = "p_IsMenuCssdDecontamiActive";
                    object obj = HttpContext.Current.Cache[parName];
                    if (obj == null)
                    {
                        var isActive = false;
                        var appprg = new AppProgram();
                        if (appprg.LoadByPrimaryKey(AppConstant.Program.CssdDecontaminationImmersion) && appprg.IsVisible == true)
                            isActive = true;

                        HttpContext.Current.Cache[parName] = (isActive == true);
                    }

                    return (bool)HttpContext.Current.Cache[parName];
                }
            }

            public static bool IsMenuCssdFeasibilityTestActive
            {
                get
                {
                    var parName = "p_IsMenuCssdFeasibilityTestActive";
                    object obj = HttpContext.Current.Cache[parName];
                    if (obj == null)
                    {
                        var isActive = false;
                        var appprg = new AppProgram();
                        if (appprg.LoadByPrimaryKey(AppConstant.Program.CssdFeasibilityTest) && appprg.IsVisible == true)
                            isActive = true;

                        HttpContext.Current.Cache[parName] = (isActive == true);
                    }

                    return (bool)HttpContext.Current.Cache[parName];
                }
            }

            public static bool IsMenuCssdPackagingActive
            {
                get
                {
                    var parName = "p_IsMenuCssdPackagingActive";
                    object obj = HttpContext.Current.Cache[parName];
                    if (obj == null)
                    {
                        var isActive = false;
                        var appprg = new AppProgram();
                        if (appprg.LoadByPrimaryKey(AppConstant.Program.CssdPackagingItem) && appprg.IsVisible == true)
                            isActive = true;

                        HttpContext.Current.Cache[parName] = (isActive == true);
                    }

                    return (bool)HttpContext.Current.Cache[parName];
                }
            }

            public static bool IsMenuPathologyAnatomyActive
            {
                get
                {
                    var parName = "p_IsMenuPathologyAnatomyActive";
                    object obj = HttpContext.Current.Cache[parName];
                    if (obj == null)
                    {
                        var isActive = false;
                        var appprg = new AppProgram();
                        if (appprg.LoadByPrimaryKey(AppConstant.Program.PA_Cytology) && appprg.IsVisible == true)
                            isActive = true;

                        HttpContext.Current.Cache[parName] = (isActive == true);
                    }

                    return (bool)HttpContext.Current.Cache[parName];
                }
            }

            public static bool IsMenuMasterItemProductExportAble(string programId)
            {
                var parName = "p_IsMenuMasterItemProductExportAble_" + programId;
                object obj = HttpContext.Current.Cache[parName];
                if (obj == null)
                {
                    var isEnabled = false;
                    var appprg = new AppProgram();
                    if (appprg.LoadByPrimaryKey(programId) && appprg.IsProgramExportAble == true)
                        isEnabled = true;

                    HttpContext.Current.Cache[parName] = (isEnabled == true);
                }

                return (bool)HttpContext.Current.Cache[parName];
            }
        }
    }
}

