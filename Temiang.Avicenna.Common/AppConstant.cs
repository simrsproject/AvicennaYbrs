using System.Globalization;

namespace Temiang.Avicenna.Common
{
    public class AppConstant
    {
        public class HIS_INTEROP
        {
            public const string PAC_HIS_INTEROP_CONNECTION_NAME = "PAC_HIS_INTEROP";
            public const string RSCH_LIS_INTEROP_CONNECTION_NAME = "RSCH_LIS_INTEROP";
            public const string SYSMEX_LIS_INTEROP_CONNECTION_NAME = "SYSMEX_LIS_INTEROP";
            public const string PRODIA_LIS_INTEROP_CONNECTION_NAME = "PRODIA_LIS_INTEROP";
            public const string VANSLAB_LIS_INTEROP_CONNECTION_NAME = "VANSLAB_LIS_INTEROP";
            public const string WYNAKOM_LIS_INTEROP_CONNECTION_NAME = "WYNAKOM_LIS_INTEROP";
            public const string LINK_LIS_INTEROP_CONNECTION_NAME = "LINK_LIS_INTEROP";
            public const string ROCHE_LIS_INTEROP_CONNECTION_NAME = "ROCHE_LIS_INTEROP";
            public const string VANSLITE_LIS_INTEROP_CONNECTION_NAME = "VANSLITE_LIS_INTEROP";
            public const string ELIMS_LIS_INTEROP_CONNECTION_NAME = "ELIMS_LIS_INTEROP";
            public const string ASIA_LAB_LIS_INTEROP_CONNECTION_NAME = "ASIA_LAB_LIS_INTEROP";
            public const string MEDICLAB_LIS_INTEROP_CONNECTION_NAME = "MEDICLAB_LIS_INTEROP";
            public const string TERASLIS_LIS_INTEROP_INTEROP_CONNECTION_NAME = "TERASLIS_LIS_INTEROP";
            public const string JASA_RAHAJA_INTEROP_CONNECTION_NAME = "JASA_RAHARJA_INTEROP";
        }

        public class Program
        {
            #region Menu from  clinic app
            public const string AR_CUSTOMER_INVOICING = "05.03.11";
            public const string AR_CUSTOMER_VERIFICATION = "05.03.12";
            public const string AR_CUSTOMER_PAYMENT = "05.03.13";

            public const string Sales = "02.03.21";
            public const string SalesReturn = "02.03.22";

            public const string ProcessDataToPCare = "01.01.99";
            #endregion

            public const string ProcessDataToSatuSehat = "01.15.20";

            public const string CasemixApproval = "01.20.12";
            public const string CasemixCoverage = "01.20.12A";


            // RADT
            public const string AbRestriction = "01.03.61";
            public const string HealthRecordPraReg = "01.82";
            public const string SelfServiceRegistration = "01.01.11";
            public const string Appointment = "01.06";
            public const string AppointmentStatus = "01.06.01";
            public const string CloseAppointment = "01.07";
            public const string HealthScreeningAppointment = "01.17.05";
            public const string HealthScreeningCloseAppointment = "01.17.06";
            public const string RefferToSpecialist = "01.01.03";
            public const string RefferToCluster = "01.04.02";
            public const string ClinicQueingInformationStatus = "01.01.04";
            public const string Admitting = "01.02.01";
            public const string CheckInConfirmation = "01.02.02";
            public const string RoomBooking = "01.02.03";
            public const string BedMaintenance = "01.02.04";
            public const string PatientTransfer = "01.02.05";
            public const string PatientDischarge = "01.02.06";
            public const string BedInformationStatus = "01.02.07";
            public const string BedInformationAndReserveStatus = "01.02.30";
            public const string PatientBirthRecord = "01.02.11";
            public const string RefferToClusterFromInpatient = "01.02.12";
            public const string OldPatientInformation = "01.02.16";
            public const string BedBooking = "01.02.17";
            public const string BedBookingRelease = "01.02.18";
            public const string PhysicianTeam = "01.02.19";
            public const string UpdateBedStatusForPatientSurgery = "01.02.20";
            public const string PatientDischargeCancellation = "01.02.21";
            public const string DetailRegistrationEmrDischarge = "01.05.09";
            public const string UpdateGuarantor = "01.02.23";
            public const string DetailRegistrationOpDischarge = "01.01.10";
            public const string PatientDischargeHistory = "01.02.24";
            public const string PatientDischargePlan = "01.02.25";
            public const string PhysicianInformation = "01.02.26";
            public const string Census = "01.02.27";
            public const string HospitalizedPatients = "01.02.28";
            public const string PatientTransferHistory = "01.02.29";
            public const string INOSInfectionMonitoring = "01.02.31";
            public const string IncompleteMedicalRecordFile = "01.02.32";

            public const string Smf = "01.03.01";
            public const string Paramedic = "01.03.02";
            public const string Department = "01.03.04";
            public const string DTD = "01.03.06";
            public const string Diagnosis = "01.03.06B";
            public const string ServiceClass = "01.03.07";
            public const string ServiceUnit = "01.03.08";
            public const string OperationalTime = "01.03.09";
            public const string ParamedicSchedule = "01.03.10";
            public const string ParamedicScheduleAnc = "01.03.10A";
            public const string ParamedicScheduleAdditionalQuota = "01.03.10B";
            public const string VisitType = "01.03.11";
            public const string Referral = "01.03.12";
            public const string ServiceItem = "01.03.13";
            public const string DiagnosticItem = "01.03.14";
            public const string LaboratoryItem = "01.03.15";
            public const string PackageItem = "01.03.16";
            public const string GroupItem = "01.03.17";
            public const string GroupItemProduct = "01.03.17B";
            public const string AsssmentType = "01.03.60";
            public const string ServiceRoom = "01.03.18";
            public const string Embalace = "01.03.19";
            public const string SubSpecialty = "01.03.20";
            public const string Procedure = "01.03.21";
            public const string ZipCode = "01.03.22";
            public const string EdcMachine = "01.03.23";
            public const string RadiologyItem = "01.03.24";
            public const string TestResultTemplate = "01.03.25";
            public const string RlMasterReport = "01.03.34";
            public const string RlMasterReportV2 = "01.03.34B";
            public const string CompoundTemplate = "01.03.32";
            public const string EmergencyDiagnose = "01.03.33";
            public const string TestResult = "02.01.20";
            public const string PatientRelated = "01.11";
            public const string PhysicianLeave = "01.13";
            public const string PhysicianLeaveInformation = "01.13.01";
            public const string PhysicianScheduleInformation = "01.13.02";
            public const string ReasonForTreatment = "01.03.36";
            public const string NumberOfBed = "01.03.41";
            public const string Morphology = "01.03.42";
            public const string ClinicalPathway = "01.03.48";
            public const string RiskFactors = "01.03.49";
            public const string PmkpStandardReference = "01.03.50";
            public const string BodyDiagram = "01.03.45";
            public const string ImageTemplate = "01.03.46";
            public const string AsessmentType = "01.03.60";
            public const string QueueingSound = "01.03.63";

            public const string ClusterCoordination = "01.04.03";
            public const string ClusterPatientEpisode = "01.04.04";

            public const string OutPatientEpisode = "01.01.05";
            public const string EmergencyPatientEpisode = "01.05.02";
            public const string InPatientEpisode = "01.02.08";

            public const string OutPatientEpisodeAndHistory = "01.01.08";
            public const string ClusterEpisodeAndHistory = "01.04.08";
            public const string EmergencyEpisodeAndHistory = "01.05.07";
            public const string InPatientEpisodeAndHistory = "01.02.13";
            public const string Reservation = "01.02.15";

            public const string RegistrationQueList = "01.80";
            public const string OutPatientRegistration = "01.01.02";
            public const string AncillaryRegistration = "01.01.02A";
            public const string ClusterPatientRegistration = "01.04.01";
            public const string EmergencyPatientRegistration = "01.05.01";
            public const string EmergencyPatientVitalsign = "01.05.03";
            public const string RefferToClusterFromEmergency = "01.05.06";

            public const string ClusterPatientCloseRegistration = "01.04.05";
            public const string EmergencyPatientCloseRegistration = "01.05.04";
            public const string InPatientCloseRegistration = "01.02.09";
            public const string HealthScreeningCloseRegistration = "01.17.03";
            public const string OutPatientCloseRegistration = "01.01.06";

            public const string ClusterPatientCloseOpenRegistration = "01.04.06";
            public const string EmergencyPatientCloseOpenRegistration = "01.05.05";
            public const string OutPatientCloseOpenRegistration = "01.01.07";
            public const string InPatientCloseOpenRegistration = "01.02.10";
            public const string HealthScreeningCloseOpenRegistration = "01.17.04";

            public const string EmergencyPatientUpdateRegistrationDate = "01.05.08";
            public const string OutPatientUpdateRegistrationDate = "01.01.09";
            public const string InPatientUpdateRegistrationDate = "01.02.22";
            public const string MedicalCheckupUpdateRegistrationDate = "01.17.08";

            public const string MedicalRecordHistory = "01.09";
            public const string MedicalRecordEditor = "01.10";
            public const string PatientDiagnosisAndProcedureEntry = "01.12.01";

            public const string ClusterQueingInformationStatus = "01.04.07";
            public const string DocumentFiles = "01.15.01";

            public const string DocumentDefinition = "01.15.02";
            public const string AnalysisDocument = "01.15.03"; //rsch version
            public const string MedicalRecordFileCompletenessAnalysis = "01.15.03A"; //rsstj version
            public const string MedicalRecordFileBorrowed = "01.15.04";
            public const string MedicalRecordFileReturned = "01.15.09";
            public const string MedicalFileStatus = "01.15.05";
            public const string DownloadDocumentTemplate = "01.15.10";
           

            public const string FileReceive = "01.15.06";
            public const string PatientDocument = "01.15.07";
            public const string PatientHealthRecord = "01.15.17";
            public const string RlReport = "01.15.08";
            public const string RlReportV2025 = "01.15.08B";

            public const string HealthScreeningTransaction = "01.17.01";
            public const string HealthScreeningRegistration = "01.17.02";
            public const string HealthScreeningMonitoring = "01.17.07";

            public const string UpdateReferral = "01.18";
            public const string UpdateMrnRegistration = "01.15.11";
            public const string PatientBlacklist = "01.15.12";
            public const string UpdateMrnPatient = "01.15.13";
            public const string CloseOpenMedicalRecordStatus = "01.15.14";
            public const string PatientResearch = "01.15.15";

            public const string QualityIndicatorSurvey = "01.15.16";

            public const string OutPatientVoidRegistration = "01.01.12";

            // PatientSafety
            public const string PatientIncident = "01.19.01";
            public const string PatientIncidentVerification = "01.19.02";
            public const string PatientIncidentInvestigation = "01.19.03";
            public const string PatientIncidentInvestigationRealization = "01.19.04";
            public const string EmployeeHealthAndSafety = "01.19.11";
            public const string DataReport = "01.19.16";
            public const string RiskManagement = "01.19.21";
            public const string Ppi = "01.19.22";
            public const string PpiInfectionSurveillance = "01.19.22A";
            public const string PpiProcedureSurveillance = "01.19.22B";
            public const string PpiNeedlePuncturedSurveillance = "01.19.22C";
            public const string PpiNeedlePuncturedSurveillanceVerified = "01.19.22D";
            public const string HandHygiene = "01.19.23";
            public const string ApdSurvey = "01.19.24";
            public const string ComplaintResponseTime = "01.19.25";
            public const string PatientIdentificationCompliance = "01.19.26";
            public const string GeneralCleanliness = "01.19.27";
            public const string MedicalStorage = "01.19.28";
            public const string ManagementWaste = "01.19.29";
            public const string MonitoringToilet = "01.19.30";
            public const string ComplianceWasteDisposal = "01.19.31";
            public const string ManagementSharpsWaste = "01.19.32";
            public const string LinenHandling = "01.19.33";
            public const string ComplianceWithEffortstoPreventTheRiskofPatientFalls = "01.19.34";


            public const string QualityIndicatorReport = "01.19.99";

            // BPJS
            public const string BpjsSisrute = "01.20.10";
            public const string BpjsSep = "01.20.01";
            public const string BpjsSepMonitoring = "01.20.02";
            public const string BpjsCheckout = "01.20.02";
            public const string BpjsApplicares = "01.20.03";
            public const string BpjsPlafondInformation = "01.20.11";
            public const string Sisrute = "01.20.04";
            public const string BpjsRujukan = "01.20.13";
            public const string BpjsApproval = "01.20.14";
            public const string BpjsRujukBalik = "01.20.15";
            public const string BpjsRencanaKontrol = "01.20.16";
            public const string BpjsInternalSep = "01.20.17";
            public const string BpjsFingerprint = "01.20.18";
            public const string AntrianOnlineDashboard = "01.20.19";

            public const string MonitoringDataKunjungan = "01.20.20";
            public const string MonitoringDataKlaim = "01.20.21";

            public const string InhealthSjp = "01.21.01";

            public const string KemenkesLaporanKematian = "01.22.01";
            public const string KemenkesSitb = "01.22.02";

            // RADT Master
            //  Questionaire
            public const string QuestionForm = "01.03.26";
            public const string QuestionGroup = "01.03.27";
            public const string Question = "01.03.28";
            public const string QuestionInGroup = "01.03.29";
            public const string QuestionGroupInForm = "01.03.30";
            public const string QuestionFormInServiceUnit = "01.03.31";
            public const string QuestionAnswerSelection = "01.03.47";
            public const string ExamSummaryTemplate = "01.03.35";

            // PatientSafety
            public const string IncidentType = "01.03.37";
            public const string RiskGrading = "01.03.38";
            public const string RiskGradingMtx = "01.03.39";
            public const string IncidentOtherMaster = "01.03.40";
            public const string ContributoryFactorsClassificationFramework = "01.03.43";

            // Charges
            public const string ServiceUnitTransaction = "02.01.01";
            public const string JobOrderTransaction = "02.01.02";
            public const string JobOrderRealisation = "02.01.03"; // RSCH: JobOrderRealizationIp
            public const string JobOrderRealizationLabOp = "02.01.22";
            public const string JobOrderRealizationLabOpToLIS = "02.01.23";
            public const string JobOrderCorrection = "02.01.15";
            public const string LaboratoryResult = "02.01.27";
            public const string JobOrderConfirmed = "02.01.28";
            public const string LabBloodSamplesSubmittingProcess = "02.01.30";
            public const string LabBloodSamplesReceptionProcess = "02.01.31";
            public const string DiagnosticSupportTransaction = "02.01.04";
            public const string ServiceUnitTransactionCorrection = "02.01.05";

            public const string ServiceUnitBooking = "02.01.06";
            public const string ServiceUnitBookingRealization = "02.01.07";
            public const string ServiceUnitBookingOperationNotes = "02.01.21";
            public const string ServiceUnitBookingStatus = "02.01.32";
            public const string ServiceUnitBookingForSurgery = "02.01.33";
            public const string ServiceUnitRealizationForSurgery = "02.01.34";

            public const string EpisodeAndHistory = "02.01.08";
            public const string PatientHealthRecordAndDocument = "02.01.09";
            public const string PatientHealthRecordAndDocumentMCU = "02.01.17";
            public const string ServiceUnitVisiteEntry = "02.01.14";
            public const string FilmConsumptionEntry = "02.01.18";
            public const string UpdateICUPhysician = "02.01.18b";
            public const string NonPatientCustomerCharges = "02.01.19";
            public const string NonPatientCustomerChargesItemReturn = "02.01.19b";

            //verification
            public const string ServiceUnitTransactionVerification = "02.01.10";
            public const string JobOrderRealisationVerification = "02.01.11";
            public const string DiagnosticSupportTransactionVerification = "02.01.12";
            public const string ServiceUnitTransactionCorrectionVerification = "02.01.13";
            public const string ServiceUnitTransactionCorrectionVerificationAncillary = "02.01.13A";
            public const string JobOrderTransactionForCashier = "02.01.16";

            public const string VerificationFinalizeBilling = "02.02.01";
            public const string PrintOutBillingStatement = "02.02.02";
            public const string MergeBilling = "02.02.03";
            public const string PrintOutBillingTemporaryStatement = "02.02.04";
            public const string IntermBill = "02.02.05";
            public const string PrintOutIntermBill = "02.02.06";
            public const string IntermBillAll = "02.02.07";
            public const string PrintOutIntermBillAll = "02.02.08";
            //public const string RssaVerificationFinalizeBilling = "02.02.09";
            public const string UnLockFinalizeBilling = "02.02.10";
            public const string BillingToPatient = "02.02.11";
            public const string BillingToPatientVerification = "02.02.12";
            public const string BillingToPatientPayment = "02.02.13";
            public const string SurgeryCostEstimation = "02.02.14";
            public const string PrintOutBillingStatementForMarketing = "02.02.15";

            public const string PrescriptionRealization = "02.03.01";
            public const string PrescriptionSales = "02.03.02";
            public const string PrescriptionReturn = "02.03.03";
            public const string PrescriptionDoseRealization = "02.03.04";
            public const string PrescriptionPending = "02.03.05";
            public const string DirectPrescriptionReturn = "02.03.06";
            public const string SpactaclePrescription = "02.03.07";
            public const string SpactaclePrescriptionRealization = "02.03.08";
            public const string PrescriptionReturnUnpaid = "02.03.09";
            public const string DirectPrescriptionReturnPaid = "02.03.10";
            public const string DirectPrescriptionReturnUnpaid = "02.03.11";
            public const string RecalculatedPrescription = "02.03.12";
            public const string PrescriptionRealizationOpr = "02.03.13";
            public const string PrescriptionSalesOpr = "02.03.14";
            public const string PrescriptionSalesPos = "02.03.15";
            public const string PrescriptionReturnOrder = "02.03.16";
            public const string PrescriptionUddOpr = "02.03.18";
            public const string PrescriptionUddIpr = "02.03.19";
            public const string PrescriptionVerification = "02.03.20";

            public const string PaymentReceive = "02.04.01";
            public const string PaymentReceiveCashier = "02.04.01C";
            public const string PaymentReturn = "02.04.02";
            public const string PaymentReturnCashier = "02.04.02C";
            public const string PaymentReceipt = "02.04.03";
            public const string DownPayment = "02.04.04";
            public const string DownPaymentCashier = "02.04.04C";
            public const string PaymentReceiveDirect = "02.04.05";
            public const string PaymentReceiveReturn = "02.04.06";
            public const string PaymentReceiveReturnCashier = "02.04.06C";
            public const string PaymentReceiveLinkToPettyCash = "02.04.07";
            public const string PaymentReceiveReturnLinkToPettyCash = "02.04.08";
            public const string PostageExpensesForLab = "02.04.09";
            public const string PaymentReceivePatientActive = "02.04.10";
            public const string PaymentReceiveRename = "02.04.11";
            public const string ClosingPayment = "02.04.12";
            public const string PatientDepositReceive = "02.04.13";
            public const string PatientDepositReturn = "02.04.14";
            public const string ClosingVisiteDownPayment = "02.04.15";
            public const string CashierOpeningBalance = "02.04.21";
            public const string CashierCheckin = "02.04.22";
            public const string CashierClosingBalance = "02.04.23";

            public const string PatientFinancialControl = "02.06.01";

            public const string CpoeInPatient = "02.01.24";
            public const string CpoeEmergency = "02.01.25";
            public const string CpoeOutPatient = "02.01.26";
            public const string ElectronicHealthRecord = "02.01.26"; // Cpoe untuk charitas diberi nama Ehr
            public const string ElectronicMedicalRecord = "02.01.26";
            public const string LabTestResultAnalysis = "02.01.29";
            public const string HealthcareCertificate = "02.01.90";
            public const string DischargeSummaryESign = "02.01.40";


            // Inventory
            public const string RequestOrder = "03.01.02";
            public const string RequestOrderAsset = "03.01.02A";
            public const string RequestOrderAssetApproval = "03.01.02B";
            public const string RequestOrderConsignment = "03.01.02C";
            public const string PurchaseOrder = "03.01.03";
            public const string PurchaseOrderFilteredBySupplierType = "03.01.03b"; /*PO Filtered by Supplier Type*/
            public const string PurchaseOrderOutstanding = "03.01.04";
            public const string PurchaseOrderUnApproval = "03.01.12";
            public const string PurchaseOrderConsignment = "03.01.05";
            public const string RequestOrderOutstanding = "03.01.06";
            public const string RequestOrderApproval = "03.01.07";
            public const string RequestOrderUnApproval = "03.01.07b";
            public const string ReOrderPurchaseOrder = "03.01.08";
            public const string ReOrderPoBasedOnPr = "03.01.09";
            public const string PlanningAndOrderStock = "03.01.10";
            public const string MergePurchaseOrder = "03.01.11";

            public const string ReceivingOrder = "03.02.01";
            public const string ReceivingOrderConsignment = "03.02.01C";
            public const string GrantsReceive = "03.02.01G";
            public const string Distribution = "03.02.02";
            public const string DistributionRequest = "03.02.05";
            public const string DistributionConfirm = "03.02.06";
            public const string DistributionForPurchasingUnit = "03.02.12";
            public const string InventoryIssue = "03.02.03";
            public const string PurchaseOrderReturn = "03.02.04";
            public const string PurchaseOrderReturnConsignment = "03.02.04C";
            public const string DistributionRequestOutstanding = "03.02.09";
            public const string ConsignmentReceive = "03.02.07";
            public const string DirectPurchase = "03.02.10";
            public const string ProductionOfGoods = "03.02.08";
            public const string ConsignmentReturn = "03.02.11";
            public const string ReceivingOrderUpdatePrice = "03.02.13";
            public const string ReceivingOrderUpdateInvoiceSupplierNo = "03.02.13A"; // ini sama dengan "03.02.13" codingnya, cuma dibedakan oleh querystring uP
            public const string ReOrderDistribution = "03.02.14";
            public const string SalesToBranch = "03.02.15";
            public const string SalesToBranchReturn = "03.02.16";
            public const string BudgetPlan = "03.02.17";
            public const string ItemRequestMaintenance = "03.02.18";
            public const string RequestItemNonStock = "03.02.19";
            public const string ConfirmationRequestItemNonStock = "03.02.20";
            public const string DestructionOfExpiredItems = "03.02.21";
            public const string BudgetPlanApproval = "03.02.22";
            public const string ReceivingOrderConfirmation = "03.02.23";
            public const string ReceiptOfSubstituteItem = "03.02.24";
            public const string InventoryIssueRequest = "03.02.25";
            public const string InventoryIssueRequest2 = "03.02.25B"; // non rutin
            public const string InventoryIssueRequestMaintenance = "03.02.26";
            public const string ConsignmentTransfer = "03.02.27";
            public const string InventoryIssueConfirm = "03.02.28";

            public const string StockAdjustment = "03.03.01";
            public const string StockAdjustmentPlus = "03.03.01B";
            public const string StockOpname = "03.03.02";
            public const string StockInformation = "03.03.03";

            public const string ItemInformation = "03.04.01";

            public const string Fabric = "03.09.01";
            public const string ItemProductMedical = "03.09.02";
            public const string ItemProductNonMedical = "03.09.03";
            public const string ItemProductMedicalDirectPurchase = "03.09.10";
            public const string ItemKitchen = "03.09.11";
            public const string ItemOptic = "03.09.07";
            public const string LocationPermission = "03.09.04";
            public const string Location = "03.09.05";
            public const string LocationTemplate = "03.09.12";
            public const string Therapy = "03.09.06";
            public const string Label = "03.09.13";
            public const string ZatActive = "03.09.14";
            public const string Indication = "03.09.15";
            public const string ProductionFormula = "03.09.08";
            public const string ReOrderPoint = "03.09.09";
            public const string DosageUnit = "03.09.16";
            public const string ConsumeMethod = "03.09.17";
            public const string ItemProductDeductionDetail = "03.09.18";
            public const string InventoryStandardReference = "03.09.99";

            // EMR
            //public const string MedicationSetupStatus = "04.02.01";
            //public const string MedicationVerificationStatus = "04.02.02";
            //public const string MedicationRealizedStatus = "04.02.03";

            public const string NutritionCareTerminologyDomain = "04.99.01";
            public const string NutritionCareTerminologyDiagnose = "04.99.02";
            public const string NutritionCareTerminologyEtiology = "04.99.03";
            public const string NutritionCareTerminologyIntervention = "04.99.04";
            public const string NutritionCareAssessmentQuestion = "04.99.05";
            public const string Ppra = "04.03.01";

            // Finance
            public const string VOUCHER_VERIFY_CASHIER = "05.01.01";
            public const string VOUCHER_VERIFY_AR = "05.01.02";
            public const string VOUCHER_VERIFY_AP = "05.01.06";
            public const string VOUCHER_VERIFY_CASH_ENTRY = "05.01.07";
            public const string VOUCHER_VERIFY_FEE = "05.01.08";
            public const string VOUCHER_VERIFY_INV_ISSUE = "05.01.12";

            public const string VOUCHER_MEMORIAL = "05.01.03";
            public const string VOUCHER_LEDGER_BALANCE = "05.01.04";
            public const string VOUCHER_SUBLEDGER_BALANCE = "05.01.05";
            public const string VOUCHER_AUTOMATIC = "-";
            public const string VOUCHER_AUTO_MAINTENANCE = "05.01.13";

            public const string CASH_ENTRY = "05.01.10";
            public const string RECONCILE = "05.01.14";
            public const string CASHIER_CORRECTION = "05.01.15";
            public const string Journal_Reconcile = "05.01.16";

            public const string CASH_TRANSACTION_LIST = "05.09.16";
            public const string OP_MTX = "05.09.24";

            public const string BILLING_GUARANTOR = "05.02.01";
            public const string BILLING_EMPLOYEE = "05.02.02";
            public const string BILLING_SUPPLIER = "05.02.03";
            public const string BILLING_LOAN_BANK = "05.02.04";

            public const string BUDGETING = "05.05.01";
            public const string BUDGETING_APPROVAL = "05.05.02";
            public const string BUDGETING_ITEM_APPROVAL = "05.05.03 ";

            public const string SETTING_PROCESS = "05.06.01";
            public const string CLOSING_PERIODE = "05.06.02";
            public const string SETTING_VOUCHER = "05.06.03";
            public const string INCOME_JOURNAL = "05.01.11";

            public const string ITEM_TARIFF_REQUEST = "05.08.01";
            public const string ITEM_PRODUCT_TARIFF_REQUEST = "05.08.02";
            public const string TARIFF_QUERY = "05.08.03";
            public const string ItemMatrix = "05.08.04";
            public const string ItemServiceTariffRequest2 = "05.08.05";
            public const string ItemServiceTariffRequestImport = "05.08.05B";
            public const string ItemServiceTariffRequestImportNew = "05.08.05C";
            public const string TariffUpdateDiscountAndVariableStatus = "05.08.06";
            public const string ItemTariffRequestProcess = "05.08.07";

            public const string BANK = "05.09.01";
            public const string CHART_OF_ACCOUNT = "05.09.03";
            public const string ACCOUNT_SUB_GROUP = "05.09.02";
            public const string GUARANTOR = "05.09.04";
            public const string EMPLOYEE = "05.09.05";
            public const string SUPPLIER = "05.09.06";
            public const string VOUCHER_CODE = "05.09.07";
            public const string BEGINNING_BALANCE = "05.09.08";
            public const string MARGIN = "05.09.09";
            public const string PAYMENTTYPE = "05.09.10";
            public const string ITEM_COST = "05.09.11";
            public const string PRODUCTACCOUNT = "05.09.12";
            public const string CURRENCY = "05.09.13";
            public const string GuarantorWithSimpleUi = "05.09.19";
            public const string SalesDiscountForItemProduct = "05.09.25";
            public const string BpjsPackage = "05.09.26";
            public const string CUSTOMER = "05.09.27";
            public const string TariffComponent = "05.09.28";
            public const string PhysicianFeeByArSetting = "05.09.29";
            public const string PhysicianFeeByServiceSetting = "05.09.30";
            public const string ServiceFeeSetting = "05.09.30Pre";
            public const string PhysicianFeeDeductionSetting = "05.09.31";
            public const string PhysicianFeeRemunByIdiSetting = "05.09.39";
            public const string ApprovalRange = "05.09.32";
            public const string JournalModuleGroup = "05.09.33";
            public const string ItemConditionRule = "05.09.35";

            public const string AR_INVOICING = "05.03.01";
            public const string AR_INVOICING_ADDITIONAL = "05.03.07";
            public const string AR_VERIFICATION = "05.03.02";
            public const string AR_PAYMENT = "05.03.03";
            public const string AR_WRITEOFF = "05.03.05";
            public const string AR_ADDING = "05.03.04";
            public const string AR_ADDPAYMENT = "05.03.06";
            public const string AR_MERGE_RECEIPT = "05.03.08";

            public const string AP_INVOICING = "05.04.01";
            public const string AP_INVOICING2 = "05.04.01B";
            public const string AP_VERIFICATION = "05.04.02";
            public const string AP_PAYMENTORDERS = "05.04.08";
            public const string AP_PAYMENT = "05.04.03";
            public const string AP_WRITEOFF = "05.04.05";
            public const string AP_ADDING = "05.04.04";
            public const string AP_INVOICING_CONSIGNMENT = "05.04.06";
            public const string AP_ADDPAYMENT = "05.04.07";

            public const string PettyCash = "05.21.01";
            public const string GuarantorDeposit = "05.21.02";
            public const string ParamedicFeeTax = "05.09.22";
            public const string ParamedicFeeCalculation = "05.07.01";
            public const string ParamedicFeeAddDeduc = "05.07.02";
            public const string ParamedicFeeVerification = "05.07.03";
            public const string ParamedicFeeVerificationPerRegNo = "05.07.05";
            public const string ParamedicFeeVerificationPerFilter = "05.07.07";
            public const string ParamedicFeePayment = "05.07.04";
            public const string ParamedicFeeCostJournal = "05.07.06";
            public const string ParamedicFeeDraft = "05.07.11";
            public const string DocumentSignature = "05.09.23";
            public const string RecipeMargin = "05.09.24";
            public const string SupplierContract = "05.09.14";
            public const string AccountingParameter = "05.09.15";
            public const string ParamedicFeeByNumberOfPatientsCalculation = "05.07.12";
            public const string ParamedicFeeByNumberOfPatientsVerification = "05.07.13";
            public const string ParamedicFeeRemunerationByIDI = "05.07.15";

            public const string SurgicalPackage = "05.09.17";
            public const string GuarantorSurgicalPackageCovered = "05.09.18";
            public const string GuarantorItemRule = "05.09.20";
            public const string GuarantorDocumentFiles = "05.09.21";
            public const string GuarantorDocumentChecklistDefinition = "05.09.36"; //"01.15.02";
            public const string ParamedicFeeByNumberOfPatientsRangeAmount = "05.09.29";
            public const string PphProgressiveTax = "05.09.34";
            public const string ItemIDI = "05.09.37";
            public const string CoorporateGrade = "05.09.38";
            public const string VisitPackage = "05.09.40";
            public const string ItemServiceProcedure = "05.09.41";

            public const string BkuMasuk = "05.89.01";
            public const string BkuKeluar = "05.89.02";

            //Asset Management
            public const string ASSET_GROUP = "05.10.01";
            public const string ASSET_ITEM = "05.10.02";
            public const string ASSET_ITEM_NONFIXEDASSETS = "05.10.03";
            public const string AssetHolidaySchedule = "05.10.04";
            public const string ASSET_DEPRECIATION = "05.10.05";
            public const string AssetStandardReference = "05.10.51";

            public const string ASSET_MOVEMENT = "05.10.06";
            public const string ASSET_MOVEMENT_REQUEST = "05.10.08";
            public const string AssetStatusChange = "05.10.07";
            public const string AssetAuction = "05.10.09";

            public const string AssetPreventiveMaintenanceSchedule = "05.10.10";
            public const string AssetPreventiveMaintenanceManualSchedule = "05.10.11";
            public const string AssetPreventiveMaintenance = "05.10.12";
            public const string AssetWorkOrder = "05.10.13";
            public const string AssetWorkOrderRealization = "05.10.14";
            public const string AssetWorkOrderPoint = "05.10.14B";
            public const string AssetWorkOrderClosing = "05.10.15";

            public const string AssetWorkOrderSentToThirdParties = "05.10.21";
            public const string AssetWorkOrderReceivedFromThirdParties = "05.10.22";

            public const string ASSET_CLOSING = "05.10.28";

            //Sanitation (IPSRS)
            public const string SanitationMaintenanceActivitySchedule = "18.01.01";
            public const string SanitationMaintenanceActivityProcess = "18.01.02";

            public const string SanitationActivityWorkOrder = "18.02.01";
            public const string SanitationActivityWorkOrderRealization = "18.02.02";
            public const string SanitationActivityWorkOrderClosing = "18.02.03";
            public const string SanitationActivityResult = "18.02.04";
            public const string SanitationControlSheet = "18.02.05";

            public const string SanitationWasteReceipt = "18.03.01";
            public const string SanitationWasteDisposal = "18.03.02";
            public const string SanitationWasteInformation = "18.03.03";

            public const string SanitationActivityResultTemplate = "18.09.01";

            //Nutrient
            public const string DietPatients = "06.02.01";
            public const string DietLiquidPatients = "06.02.01B";
            public const string MealOrder = "06.02.02";
            public const string AdditionalMealOrder = "06.02.03";
            public const string DistributionPortion = "06.02.04";
            public const string DistributionPortionChecked = "06.02.04B";
            public const string AtePatientsControl = "06.02.05";
            public const string MealOrderVerification = "06.02.06";
            public const string MealOrderChange = "06.02.07";
            public const string MealOrderChangeDc = "06.02.07B";
            public const string MealOrderChangeSc = "06.02.07C";
            public const string DistributionOfLiquidFood = "06.02.08";
            public const string SnackOrder = "06.02.11";
            public const string EventMealOrder = "06.02.12";
            public const string ExtraMealOrder = "06.02.13"; // order makan u/ Dokter, HD, MCU
            public const string NonPatientCustomerMealOrder = "06.02.14";
            public const string NonPatientCustomerDistributionPortion = "06.02.15";
            public const string MealOrderOutpatient = "06.02.16";
            public const string DistributionPortionOutpatient = "06.02.17";

            public const string MenuInitialization = "06.03.01";
            public const string MealOrderDateInitialization = "06.03.02";
            public const string UnitClassMenuSetting = "06.03.03";
            public const string ClassMenuSetting = "06.03.03B";
            public const string MenuExtraInitialization = "06.03.04";
            public const string UnitClassMenuExtraSetting = "06.03.05";
            public const string LiquidFoodSetting = "06.03.06";
            public const string LiquidFoodDietSetting = "06.03.07";

            public const string Food = "06.09.01";
            public const string FoodCafetaria = "06.09.01B";
            public const string Snack = "06.09.02";
            public const string MenuVersion = "06.09.06";
            public const string Menu = "06.09.07";
            public const string MenuItem = "06.09.08";
            public const string MenuExtraVersion = "06.09.09";
            public const string MenuExtraItem = "06.09.10";
            public const string MenuExtraItemFood = "06.09.12";

            public const string Diet = "06.09.11";

            //HR
            public const string PositionGrade = "07.01.01";
            public const string PositionLevel = "07.01.02";
            public const string Position = "07.01.03";
            public const string PositionQualification = "07.01.04";

            public const string PersonalInfo = "07.02.01";
            public const string PersonalInfoAdvanced = "07.02.02";
            public const string EmployeeWorkingInfo = "07.02.04";
            public const string EmployeeWorkSchedule = "07.02.05";
            public const string EmployeeDisciplinary = "07.02.06";
            public const string EmployeeDisciplinaryHistory = "07.02.07";
            //public const string EmployeeForms = "07.02.08";

            public const string QueryPersonalAddress = "07.03.02";
            public const string QueryPersonalContact = "07.03.03";
            public const string QueryPersonalFamily = "07.03.04";
            public const string QueryPersonalEmergency = "07.03.05";
            public const string QueryPersonalWorkExperience = "07.03.06";
            public const string QueryPersonalEducationHistory = "07.03.07";
            public const string QueryPersonalLicence = "07.03.08";
            public const string QueryPersonalPhysical = "07.03.09";
            public const string QueryPersonalAge = "07.03.10";
            public const string QueryPersonalReligion = "07.03.11";
            public const string QueryPersonalBloodType = "07.03.12";

            public const string QueryEmploymentPeriod = "07.03.20";
            public const string QueryEmployeeOrganization = "07.03.21";
            public const string QueryEmployeePosition = "07.03.22";
            public const string QueryEmployeeGrade = "07.03.23";
            public const string QueryEmployeeAchievement = "07.03.24";
            public const string QueryEmployeeYearInService = "07.03.24";

            public const string HumanBasePeriod = "07.09.01";
            public const string Award = "07.09.02";
            public const string OrganizationUnit = "07.09.03";
            public const string EducationLevel = "07.09.04";
            public const string Profile = "07.09.05";
            public const string EmployeeGradeMaster = "07.09.06";
            public const string Profession = "07.09.07";
            public const string RL4 = "07.09.08";
            public const string EmployeeFormsTemplate = "07.09.12";

            //Recruitment
            public const string ApplicantInfo = "07.04.01";
            public const string ApplicantPersonalInfo = "07.08.01";
            public const string ApplicantWorkingInfo = "07.08.02";
            public const string PersonnelRequisition = "07.04.02";
            public const string JobOpportunity = "07.04.03";
            public const string EvaluationPerCandidate = "07.04.04";
            public const string EvaluationPerSelectionProcess = "07.04.05";
            public const string StandartSelectionProses = "07.09.10";
            public const string RecruitmentPlan = "07.09.11";

            // Medical Manegement
            public const string EmployeeMedicalBenefit = "07.05.01";
            public const string MedicalBenefitClaim = "07.05.02";
            public const string EmployeeMedicalAdjustment = "07.05.03";
            public const string MedicalEmployeeInitial = "07.05.08";
            public const string GenerateEmployeeMedical = "07.05.09";

            //K3RS
            public const string K3RS_EmployeeMedicalHistory = "07.06.01";
            public const string K3RS_EmployeeMedicalList = "07.06.02";
            public const string K3RS_EmployeeIncident = "07.06.03";
            public const string K3RS_EmployeeIncidentVerification = "07.06.04";
            public const string K3RS_EmployeeNeedleStickInjury = "07.06.05";
            public const string K3RS_EmployeeNeedleStickInjuryFollowUp = "07.06.06";
            public const string K3RS_EmployeeHealthTestResult = "07.06.07";
            public const string K3RS_Form = "07.06.11";
            public const string K3RS_FormTemplate = "07.06.12";

            //KEPK
            public const string KEPK_ResearchLetter = "07.13.01";
            public const string KEPK_StandardReference = "07.13.02";

            public const string EmployeeTraining = "07.07.01";
            public const string EmployeeTrainingPoint = "07.07.01A";
            public const string EmployeeTrainingProposal = "07.07.02";
            public const string EmployeeTrainingProposal2 = "07.07.02A";
            public const string EmployeeOrientation = "07.07.03";
            public const string EmployeeTrainingEvaluation = "07.07.04";
            //--------------------------
            public const string EmployeeTrainingType = "07.07.11";
            public const string EmployeeTrainingAssessmentAspect = "07.07.12";
            public const string EmployeeTrainingAssessmentCriteria = "07.07.13";

            public const string HospitalInfo = "07.09.20";
            public const string InsuranceCompany = "07.09.21";
            public const string MedicalBenefitType = "07.09.22";

            public const string EmployeeLeave = "07.10.01";
            public const string EmployeeLeaveRequest = "07.10.02";
            public const string EmployeeLeaveRequest2 = "07.10.02A";
            public const string EmployeeLeaveApproval = "07.10.02B";
            public const string EmployeeLeaveApprovalAdmin = "07.10.02D";
            public const string EmployeeLeaveApproval1 = "07.10.02E";
            public const string EmployeeLeaveApproval2 = "07.10.02C";
            
            public const string EmployeeLeaveVerified = "07.10.03";
            public const string EmployeeLeaveProcess = "07.10.04";

            //KEHRS
            public const string KEHRS_SafetyCultureIncidentReports = "07.15.01";
            public const string KEHRS_SafetyCultureIncidentReportsVerification = "07.15.02";
            public const string KEHRS_SafetyCultureIncidentReportsConclusion = "07.15.03";


            //Appraisal
            public const string AppraisalQuestioner = "07.11.01";
            public const string AppraisalParticipant = "07.11.02";
            public const string AppraisalScoring = "07.11.03";
            public const string AppraisalEvaluation = "07.11.04";
            public const string AppraisalIntervention = "07.11.05";
            public const string AppraisalRecapitulation = "07.11.06";
            public const string AppraisalRecapitulationAdmin = "07.11.06A";
            public const string AppraisalConclusion = "07.11.07";

            public const string EmployeePermission = "07.12.01";
            public const string EmployeePermissionVerified = "07.12.02";

            public const string EmployeeLogbook = "07.14.01";
            public const string EmployeeLogbookMedicalCommitte = "07.14.02";
            public const string EmployeeLogbookNursingCommitte = "07.14.03";
            public const string EmployeeLogbookKtkl = "07.14.04";

            // RENKIN
            public const string PerformancePlanStructureSettings = "07.16.01";
            public const string PerformancePlanActivityInput = "07.16.06";
            public const string PerformancePlanActivityValidation = "07.16.07";

            public const string PerformancePlanJptInput = "07.16.11";
            public const string PerformancePlanJptRealization = "07.16.12";
            public const string PerformancePlanJptVerification = "07.16.13";
            public const string PerformancePlanJptValidation = "07.16.14";

            public const string PerformancePlanNonJptInput = "07.16.16";
            public const string PerformancePlanNonJptVerification = "07.16.17";
            public const string PerformancePlanNonJptRealization = "07.16.18";
            public const string PerformancePlanNonJptValidation = "07.16.19";
            public const string PerformancePlanNonJptClosingPeriod = "07.16.20";

            public const string PerformancePlanPppkInput = "07.16.21";
            public const string PerformancePlanPppkVerification = "07.16.22";
            public const string PerformancePlanPppkRealization = "07.16.23";
            public const string PerformancePlanPppkValidation = "07.16.24";
            public const string PerformancePlanPppkBehavioralAssessment = "07.16.25";

            public const string PerformancePlanJpt = "07.16.31";
            public const string PerformancePlanNonJpt = "07.16.32";
            public const string PerformancePlanPppk = "07.16.33";
            public const string PerformancePlanAspectsOfBehaviorAssessment = "07.16.34";
            public const string PerformancePlanAspectsOfBehaviorAssessmentGrade = "07.16.35";
            //---
            public const string PerformancePlanJptSchedule = "07.16.36";
            public const string PerformancePlanNonJptSchedule = "07.16.37";
            public const string PerformancePlanPppkSchedule = "07.16.38";

            public const string Renkin = "07.16.51";
            public const string RenkinPeriode = "07.16.52";
            public const string RenkinTransaction = "07.16.53";
            public const string RenkinTransactionVerification = "07.16.54";
            //public const string RenkinAktivitas = "07.16.13";
            //public const string RenkinTransactionApproval = "07.16.10";

            //Credentialing
            //-- ybrs/process ---------------------------------------------------------------------
            public const string CredentialCompetencyAssessmentApplication = "07.17.01";
            public const string CredentialCompetencyAssessmentEvaluator = "07.17.02";
            public const string CredentialCompetencyAssessmentProcess = "07.17.03";
            public const string CredentialApplication = "07.17.04";
            public const string CredentialProcessMedicalCommittee = "07.17.05";
            public const string CredentialProcessNursingCommittee = "07.17.06";
            public const string CredentialProcessKtkl = "07.17.07";
            public const string RecommendationLetterMedicalCommittee = "07.17.08";
            public const string RecommendationLetterNursingCommittee = "07.17.09";
            public const string RecommendationLetterKtkl = "07.17.10";
            public const string ClinicalAssignmentLetter = "07.17.11";
            public const string ClinicalAssignmentLetter_Komed = "07.17.11A";
            public const string ClinicalAssignmentLetter_Komkep = "07.17.11B";
            public const string ClinicalAssignmentLetter_Ktkl = "07.17.11C";

            public const string MedicCredentialSelfAssessment = "07.17.12";
            public const string MedicCredentialSelfAssessmentAdmin = "07.17.12A";
            public const string MedicCredentialApprovalBySupervisor = "07.17.13";
            public const string MedicCredentialApprovalBySubCommittee = "07.17.14";
            public const string MedicCredentialApprovalByMedicalCommittee = "07.17.15";
            public const string MedicCredentialApprovalByDirector = "07.17.16";

            public const string CredentialingStatusIndividual = "07.17.20";
            public const string CredentialingStatusMedicalCommittee = "07.17.20A";
            public const string CredentialingStatusNursingCommittee = "07.17.20B";
            public const string CredentialingStatusKtkl = "07.17.20C";
            public const string CredentialingStatusIndividualMedic = "07.17.20M";

            //-- rsch/process_v2 ------------------------------------------------------------------
            public const string CredentialApplication2 = "07.17.21";
            public const string CredentialApplication2Admin = "07.17.21A";
            public const string CredentialUpdateDocument = "07.17.22";

            public const string CredentialDisposition = "07.17.23";

            public const string CredentialDocumentChecking = "07.17.24";
            public const string CredentialDocumentChecking_Komed = "07.17.24A";
            public const string CredentialDocumentChecking_Komkep = "07.17.24B";
            public const string CredentialDocumentChecking_Ktkl = "07.17.24C";

            public const string CredentialScheduling = "07.17.26";
            public const string CredentialScheduling_Komed = "07.17.26A";
            public const string CredentialScheduling_Komkep = "07.17.26B";
            public const string CredentialScheduling_Ktkl = "07.17.26C";

            public const string CredentialInvitation = "07.17.27";
            public const string CredentialInvitation_Komed = "07.17.27A";
            public const string CredentialInvitation_Komkep = "07.17.27B";
            public const string CredentialInvitation_Ktkl = "07.17.27C";

            public const string CredentialEthnicQuestionnaire = "07.17.28";
            public const string CredentialCompetencyAssessment = "07.17.29";
            public const string CredentialCompetencyAssessmentMedical = "07.17.29A";

            public const string CredentialObservationInstrumentEvaluator = "07.17.30";
            public const string CredentialObservationInstrumentAssessment = "07.17.31";

            public const string CredentialRecomendation = "07.17.33";
            public const string CredentialRecomendation_Komed = "07.17.33A";
            public const string CredentialRecomendation_Komkep = "07.17.33B";
            public const string CredentialRecomendation_Ktkl = "07.17.33C";
            public const string CredentialRecomendation_Ci = "07.17.33D";
            public const string CredentialApprovalBySubCommitte = "07.17.34";
            public const string CredentialApprovalBySubCommitte_Komed = "07.17.34A";
            public const string CredentialApprovalBySubCommitte_Komkep = "07.17.34B";
            public const string CredentialApprovalBySubCommitte_Ktkl = "07.17.34C";
            public const string CredentialApprovalByCommitte = "07.17.35";
            public const string CredentialApprovalByCommitte_Komed = "07.17.35A";
            public const string CredentialApprovalByCommitte_Komkep = "07.17.35B";
            public const string CredentialApprovalByCommitte_Ktkl = "07.17.35C";
            public const string CredentialApprovalByDirector = "07.17.36";

            public const string CredentialingStatus = "07.17.37";
            public const string CredentialingStatus_Komed = "07.17.37A";
            public const string CredentialingStatus_Komkep = "07.17.37B";
            public const string CredentialingStatus_Ktkl = "07.17.37C";

            //-- master ------------------------------------------------------------------
            public const string CredentialQuestionnaire = "07.17.91";
            public const string ClinicalWorkArea = "07.17.92";
            public const string CredentialObservationInstrumentQuestionnaire = "07.17.93";
            //--
            public const string ClinicalPerformanceAppraisalScoresheet = "07.18.01";
            public const string ClinicalPerformanceAppraisalQuestionnaire = "07.18.02";

            //Payroll
            public const string Ptkp = "08.01.01";
            public const string Pkp = "08.01.02";
            public const string BiayaJabatan = "08.01.03";
            public const string SeveranceTax = "08.01.04";
            public const string PensionTax = "08.01.05";
            public const string TERMonthly = "08.01.06";

            public const string EmployeeSalaryInfo = "08.02.01";
            public const string ThrInfo = "08.02.02";

            public const string AttendanceOutstandingList = "08.05.03";
            public const string MonthlyAttendance = "08.03.01";
            public const string EmployeeLeaveCashable = "08.03.02";
            public const string EmployeeLoan = "08.03.03";
            public const string PeriodicSalary = "08.03.04";
            public const string UpdateEmployeeSalary = "08.03.05";
            public const string EmployeeOvertime = "08.03.06";
            public const string EmployeeOvertimeApproval = "08.03.06B";
            public const string EmployeeOvertimeVerified = "08.03.06C";
            public const string EmployeePeriodicStructuralBenefits = "08.03.07";
            public const string ExportPayroll = "08.03.99";

            //Process
            public const string ProcessClosingWageTransaction = "08.04.01";
            public const string ProcessUpdateBasicSalaryByPositionGrade = "08.04.02";
            public const string ProcessClosingThrTransaction = "08.04.03";

            //Leave
            public const string LeaveRequest = "08.05.02";

            // remun
            public const string EmployeeRemun = "08.06.01";
            public const string EmployeeRemunerationPosition = "08.06.02"; // rsbk
            public const string EmployeeRemunerationBase = "08.06.03"; // rsbk
            public const string EmployeeIncentiveProcess = "08.06.04"; // rsbk
            

            //Master
            public const string Healthcare = "08.09.01";
            public const string PayrollPeriod = "08.09.02";
            public const string StandardSalary = "08.09.03";
            public const string SalaryComponentRounding = "08.09.04";
            public const string AttedanceMatrix = "08.09.05";
            public const string SalaryComponent = "08.09.06";
            public const string SalaryTemplate = "08.09.07";
            public const string ThrSchedule = "08.09.08";
            public const string StructuralBenefits = "08.09.09";
            public const string OvertimeFormula = "08.09.10";
            public const string DisciplinarySanctions = "08.09.11";
            public const string SalaryScale = "08.09.12";
            public const string IncentivePosition = "08.09.13";
            public const string WageStructureAndScale = "08.09.14";
            public const string WageStructureAndScalePoints = "08.09.15";
            public const string WageStructureAndScaleWorkGroup = "08.09.16";
            public const string WorkingHour = "08.09.20";

            //Attendance
            public const string WorkingSchedule = "08.05.11";
            public const string WorkingScheduleAdmin = "08.05.13";
            public const string WorkingScheduleIntervention = "08.05.12";
            public const string WorkingScheduleInterventionAdmin = "08.05.14";

            // Control Panel
            public const string UserGroup = "95.01.01";
            public const string User = "95.01.02";
            public const string UserServiceunit = "95.01.04";
            public const string Announcement = "95.07";
            public const string ChangePassword = "95.01.03";
            public const string AuditLogView = "95.02.01";
            public const string AuditLogSetting = "95.02.02";
            public const string StandardReference = "95.03.01";
            public const string ParameterSetting = "95.03.02";
            public const string AutoNumbering = "95.03.03";
            public const string TransactionCodeNumbering = "95.03.04";
            public const string TransactionCodeMultilevelApproval = "95.03.05";
            public const string HealthcareInfo = "95.03.06";
            public const string PrinterLocation = "95.04.01";
            public const string UserHostPrinter = "95.04.02";
            public const string ClearPrintJob = "95.04.03";
            public const string AutomaticChargeBed = "95.05.01";

            // Nursing Care
            public const string NursingCareStandard = "10.01.01";
            //public const string NursingAssessment = "10.10.01";
            public const string NursingAssessmentQuestion = "10.10.01";
            public const string NursingDiagnosa = "10.10.02";
            public const string NursingDomain = "10.10.03";
            public const string NursingDiag = "10.10.04";
            public const string NursingProblem = "10.10.05";
            public const string NursingNOC = "10.10.06";
            public const string NursingNOCObjcetive = "10.10.07";
            public const string NursingNIC = "10.10.08";
            public const string NursingNICImplementation = "10.10.09";
            public const string NursingNICImplementationCustom = "10.10.09b";
            public const string NursingDiagnosaTemplate = "10.10.10";

            public const string NonPatientCustomerChargesMobile = "11";

            // Pathology Anatomy
            public const string PA_Cytology = "12.01.01";
            public const string PA_Histology = "12.01.02";
            public const string PA_PapSmear = "12.01.03";
            public const string PA_Immunohistochemistry = "12.01.04";
            public const string PA_FNAB = "12.01.05";
            public const string PA_VC = "12.01.06";
            public const string PA_Histochemistry = "12.01.07";

            public const string PA_ReExaminationCytology = "12.02.01";
            public const string PA_ReExaminationHistology = "12.02.02";
            public const string PA_ReExaminationPapSmear = "12.02.03";
            public const string PA_ReExaminationImmunohistochemistry = "12.02.04";
            public const string PA_ReExaminationFNAB = "12.02.05";
            public const string PA_ReExaminationVC = "12.02.06";
            public const string PA_ReExaminationHistochemistry = "12.02.07";

            //--master--//
            public const string PA_SourceOfTissue = "12.10.01";
            public const string PA_Tissue = "12.10.02";
            public const string PA_LocationOfCytology = "12.10.03";
            public const string PA_CytologicDiagnosis = "12.10.04";
            public const string PA_HistologyDiagnosis = "12.10.05";
            public const string PA_ImpressionGroup = "12.10.06";
            public const string PA_CytologyTemplate = "12.10.07A";
            public const string PA_HistopathologyTemplate = "12.10.07B";
            public const string PA_ImmunohistochemistryTemplate = "12.10.07C";
            public const string PA_FNABTemplate = "12.10.07D";
            public const string PA_VriesCoupeTemplate = "12.10.07E";
            public const string PA_HistochemistryTemplate = "12.10.07F";
            public const string PA_FNABDiagnosis = "12.10.08";
            public const string PA_VriesCoupeDiagnosis = "12.10.09";


            // Blood Bank
            public const string BloodBankRequest = "13.01.01";
            public const string BloodBankReceived = "13.01.02";
            public const string BloodBankTransfusionMonitoring = "13.01.03";
            public const string BloodBankSampleSubmitted = "13.01.04";
            public const string BloodBankSampleReception = "13.01.04B";
            public const string BloodBankCrossMatching = "13.01.05";
            public const string BloodBankRequestHistory = "13.01.06";
            public const string BloodBankReturn = "13.01.07";

            public const string BloodStockReceived = "13.02.01";
            public const string BloodStockTransformation = "13.02.02";
            public const string BloodStockExtermination = "13.02.03";
            public const string BloodStockInformation = "13.02.09";

            //--master--//
            public const string BloodBankStandardReference = "13.10.01";

            // CSSD
            public const string CssdSterileItemsRequest = "14.01.01"; //~/Module/Cssd/Transaction/SterileItemsRequest/SterileItemsRequestList.aspx
            public const string CssdSterileItemsReceived = "14.01.02"; //~/Module/Cssd/Transaction/SterileItemsReceived/SterileItemsReceivedVerificationList.aspx

            public const string CssdDecontaminationImmersion = "14.02.01";
            public const string CssdDecontaminationAbstersion = "14.02.02";
            public const string CssdDecontaminationDrying = "14.02.03";

            public const string CssdFeasibilityTest = "14.03.01";

            public const string CssdPackagingItem = "14.04.01";

            public const string CssdUltrasoundProcess = "14.05.01";
            public const string CssdSterilizationProcess = "14.05.02"; //Sterilization Process
            public const string CssdDttProcess = "14.05.03"; //High Level Disinfection (DTT) Process

            public const string CssdSterileItemsReturned = "14.06.01"; //desentralisasi: Return of Materials / Instruments
            public const string CssdSterileItemsReturnedClose = "14.06.02"; //Return of Materials / Instruments Close
            public const string CssdSterileItemsReturnedInfo = "14.06.03"; //Return of Materials / Instruments Information

            public const string CssdSterileItemsDistribution = "14.06.11"; //sentralisasi: Distribution
            public const string CssdStockOpname = "14.06.12";

            public const string CssdSterileItemsReceivedInfo = "14.06.21";
            public const string CssdStockInformation = "14.06.22";
            
            //--master--//
            public const string CssdStandardReference = "14.10.01";
            public const string CssdItem = "14.10.02";
            public const string CssdMachine = "14.10.03";

            //Laundry
            public const string LaundryReceived = "15.01.01";
            public const string LaundererProcess = "15.01.02";
            public const string LaundryReceivedInfectious = "15.02.01";
            public const string LaundererProcessInfectious = "15.02.02";
            public const string LaundrySortingProcess = "15.03.01";
            public const string LaundererProcessRewashing = "15.03.02";
            public const string LaundryRecapitulation = "15.04.01";
            public const string LaundryDistribution = "15.04.02";
            public const string LaundryReturnDistribution = "15.04.03";
            public const string LaundryReturned = "15.05.01";
            public const string LaundryReturnedInfo = "15.05.02";
            public const string LinenItemsRepairing = "15.06.01";
            public const string LinenItemsExtermination = "15.06.02";
            public const string LaundryStandardReference = "15.09.01";
            public const string ItemLinen = "15.09.02";
            public const string WashingMachine = "15.09.03";
            public const string LaundryWashingProgramType = "15.09.04";

            //jasa raharja
            public const string JasaRaharjaCommunicationLog = "95.90.01";
            public const string JasaRaharjaDataQuery = "05.90.01";

            //inacbg
            public const string InacbgProcess = "05.91.01";

            public const string MyHome = "99.99.99";
            public const string QueueList = "01.81";

            //CRM
            public const string Membership = "11.01.01";
            public const string MembershipEmployee = "11.01.03";
            public const string MembershipItemRedemption = "11.01.02";
            //--Master
            public const string MembershipItemRedeem = "11.99.01";

            // PharmaceuticalCare
            public const string PharmaceuticalCare = "04.04.01";
            public const string PrecriptionReview = "04.04.02";
            public const string DrugInfService = "04.04.03";

            // Ambulance
            public const string AmbulanceOrder = "17.01";
            public const string AmbulanceRealization = "17.02";
            public const string Vehicle = "17.10.01";
            public const string Driver = "17.10.02";
        }

        public class Report
        {
            public class InitialAssessment
            {
                public class OutPatient
                {
                    public const string General = "SLP.01.AGENRL";
                    public const string General2 = "SLP.01.AGENRL2";

                    public const string Dentis = "SLP.01.ADENTS";
                    public const string Dentis2 = "SLP.01.ADENTS2";

                    public const string Nursing = "SLP.01.ANURSE";
                    public const string NursingAndGynecology = "SLP.01.ANURSGY";

                    public const string Surgery = "SLP.01.ASURGI";
                    public const string Eye = "SLP.01.AEYE";
                    public const string Psychiatry = "SLP.01.APSYCY";
                    public const string Kid = "SLP.01.AKID";
                    public const string Heart = "SLP.01.AHEART";
                    public const string Internal = "SLP.01.AINTER";
                    public const string Lung = "SLP.01.ALUNG";
                    public const string Tht = "SLP.01.ATHT";
                    public const string Gynecology = "SLP.01.APKAND";
                    public const string Neurology = "SLP.01.ANEURO";
                    public const string Skin = "SLP.01.ASKIN";
                    public const string Psychology = "SLP.01.APSYCO";
                    public const string Nutrient = "SLP.01.ANUTRI";
                    public const string Rehabilitation = "SLP.01.AREHAB";
                    public const string Igd = "SLP.01.AIGD";
                }


                public class InPatient
                {
                    public const string Kid = "SLP.IP.AKID";
                    public const string Skin = "SLP.IP.ASKIN";
                    public const string Psychiatry = "SLP.IP.APSYCY";
                    public const string Lung = "SLP.IP.ALUNG";
                    public const string Eye = "SLP.IP.AEYE";
                    public const string Neurology = "SLP.IP.ANEURO";
                    public const string Nursing = "SLP.IP.ANURSE";
                    public const string Stroke = "SLP.IP.ASTROK";
                    public const string Surgery = "SLP.IP.ASURGI";
                    public const string Tht = "SLP.IP.ATHT";
                    public const string Heart = "SLP.IP.AHEART";
                    public const string Internal = "SLP.IP.AINTER";

                }
            }
            public class ContinuedAssessment
            {
                public const string General = "SLP.CA.AGENRL";
                public const string Dentis = "SLP.CA.ADENTS";
                public const string Nursing = "SLP.CA.ANURSE";
                public const string Gynecology = "SLP.CA.APKAND";
            }
            public const string MedicalHistory = "SLP.01.0022";
            public const string PhysicalExam = "SLP.01.0023";
            public const string SystemicExam = "SLP.01.0024";
            public const string ExaminationSummary = "SLP.01.0054";
            public const string ResumeRawatJalan = "SLP.01.0085";
            public const string RingkasanPenyakitPasien = "SLP.01.0086";
            public const string PhysicianStatement = "SLP.01.0087";
            public const string ResumeMedisRawatInap = "SLP.01.0089";
            public const string PrescriptionOrderSlip = "SLP.02.0078";
            public const string PatientLabel = "SLP.01.0008";
            public const string BabyWirstband = "SLP.01.0019";
            public const string ReferNotes = "SLP.01.0081";
            public const string RM12_ReferNotes = "SLP.01.RM12";
            public const string SOAP = "SLP.01.0082";
            public const string SickLetter = "SLP.01.0083";
            public const string HealthLetter = "SLP.01.0201";
            public const string IntegratedNotesRekap = "SLP.10.0008";

            public const string InpatientAdmissionLetter = "SLP.01.0084";
            public const string RM6d = "SLP.01.0095";
            public const string ClinicalExamResults = "SLP.01.0111";
            public const string JobOrderNotesDiagnostic = "SLP.02.0077";
            public const string TransactionReceipt = "SLP.02.0080";
            public const string BillingStatementOutpatient = "SLP.02.0076";
            public const string AncillaryStruk = "SLP.01.0020";
            public const string TestReceipt = "SLP.02.0081";
            public const string JobOrderNotes = "SLP.02.0005";
            public const string JobOrderNotes2 = "SLP.02.0091";
            public const string PrintLabel1 = "XML.02.0072";
            public const string BpjsAntrolRencanaKunjunganKiosk = "XML.01.0042f";

            //xml
            public const string CetakMateriMcu = "XML.01.0007";
            public const string CetakLabelPengantarKeKlinikDiagnostik = "XML.02.0007";
            public const string CetakAmplopOrLabelDiagnostik = "XML.02.0008";
            public const string CetakAmplopAudiometri = "XML.02.0018";
            public const string CetakAmplopSpirometri = "XML.02.0019";
            public const string CetakanPasienEdukasi = "XML.04.0001";

            public const string JobOrderLabelDiagnostic = "SLP.02.0093";
            public const string PatientDischargePermit = "XML.YBRS.01.015";
            public const string BillingPaymentPermit = "SLP.BILL.0001";

            //his2008
            public const string SurgeryRegistrationReceipt = "SLP.02.0079";
            public const string SpectaclePrescriptionSlip = "";
            public const string PurchaseReceiveAgreement = "SLP.03.0004";
            public const string PurchaseReceiveAgreementVer2 = "SLP.03.0022";
            public const string PurchaseReceiveSlip = "SLP.03.0005";
            public const string PurchaseReceiveSlipCash = "SLP.03.0023";
            public const string PurchaseOrderReceiveKitchen = "SLP.03.0033";
            public const string PurchaseReceiveByInvoiceSupplierNoSlip = "SLP.03.0014";
            public const string DownPaymentReceiptSlip = "SLP.02.0004";
            public const string DownPaymentReturnReceiptSlip = "SLP.02.0011";
            public const string BillingDetail = "SLP.02.0003";
            public const string BillingSummary = "SLP.02.0014";
            public const string BillingInformation = "SLP.02.0001";
            public const string _BillingStatementDetailWithComponentTariff = "SLP.02.0072"; // deactivated
            public const string BillingStatementDetailWithComponentTariffDraft = "SLP.02.0073";
            public const string PaymentReceiveReceiptNoDP = "SLP.02.0074";

            public const string BillingTemporaryStatementDetail = "SLP.02.0029";
            public const string DownPaymentListStatement = "SLP.02.0030";
            public const string Deposit1Statement = "SLP.02.0002";
            public const string Deposit2Statement = "SLP.02.0031";
            public const string BillingIntermBillStatement = "SLP.02.0034";
            public const string DepositIntermBillStatement = "SLP.02.0035";

            public const string PaymentReceiptAllDirect = "SLP.02.0032";
            public const string PaymentReceiptAllDirectDetail = "SLP.02.0033";

            public const string PrescriptionSlip = "SLP.02.0010";
            public const string PrescriptionReceiptSlip = "SLP.02.0012";
            public const string PrescriptionReturnReceiptSlip = "SLP.02.0013";
            public const string PrescriptionOnlineSlip = "SLP.02.0009";
            public const string PrescriptionEtiket = "SLP.02.0008";
            public const string PrescriptionEtiketLr = "SLP.02.0007";
            public const string PaymentReceipt = "RPT.02.0040";
            public const string PaymentReceiptBillingStatement = "RPT.02.0041";
            public const string BillingPrescription = "SLP.02.0026";
            public const string RSSA_PaymentReceiveReceipt = "SLP.02.0027";
            public const string TestResultNative = "SLP.01.08.01";
            public const string TestResultEnglish = "SLP.01.08.02";
            public const string EtiketRadiology = "SLP.01.08.03";
            public const string RssaBillingStatementForAskes = "SLP.02.0036";
            public const string RssaBillingPrescription = "SLP.02.0037";
            public const string BillingGuarantorStatementDetail = "SLP.02.0038";
            public const string BillingGuarantorStatementDetailEN = "SLP.02.0038EN";//english
            public const string BillingIntermStatementPatientDetail = "SLP.02.0039";
            public const string BillingIntermStatementForMarketing = "SLP.02.0108";
            public const string RssaBillingTemporaryStatement = "SLP.02.0041";
            public const string RssaReceiptPostageExpensesForLab = "SLP.02.0042";
            public const string StatementOfDebt = "SLP.02.0043";
            public const string PaymentReceiptPrescDetail = "SLP.02.0044";
            public const string RssaBillingToSelectedClassStatementDetail = "SLP.02.0045";
            public const string BillingStatementDetail2 = "SLP.02.0046";
            public const string BillingStatementDetail2EN = "SLP.02.0046EN";//english
            public const string BillingStatementDetail2WithR = "SLP.02.0046R";
            public const string LaboratoryResult = "XML.02.0024";
            public const string BillingStatementDetailByDate = "SLP.02.0107";
            public const string BillingStatementDetailByDateEN = "SLP.02.0107EN";//english

            public const string PackagePatientStatementDetail = "XML.02.0006"; //billing patient berdasarkan no bayar
            public const string BillingPatientStatementDetail = "SLP.02.0047"; //billing patient berdasarkan no bayar
            public const string RSSA_Slip_Kalbar = "SLP.02.0048";
            public const string RSSA_PrescriptionSlip = "SLP.02.0022";
            public const string RSSA_PrescriptionReceiptSlip = "SLP.02.0023";
            public const string PrescriptionReceiptSlipIncUnapproved = "SLP.02.0054";
            public const string RSSA_PrescriptionReturnReceiptSlip = "SLP.02.0024";
            public const string RSSA_PrescriptionReturnReceiptDetailSlip = "SLP.02.0082";
            public const string RSSA_PrescriptionReceiptRevisionSlip = "SLP.02.0040";
            public const string PrescriptionReceiptHandoverSlip = "SLP.02.0083";
            public const string PaymentReturnReceipt = "SLP.02.0025";
            public const string PaymentReceiptRevision = "SLP.02.0028";
            public const string BillingPatientStatementDetail2 = "SLP.02.0052"; //billing patient berdasarkan no bayar
            public const string BillingGuarantorStatementDetail2 = "SLP.02.0053";
            public const string JobOrderReceipt = "SLP.02.0055";
            public const string PrescriptionOrder = "SLP.02.0056";
            public const string BillingStatementRekap = "SLP.02.0057";
            public const string BillingStatementRekapEN = "SLP.02.0057EN";//english
            public const string BillingStatementLabFarLog = "SLP.02.0058";
            public const string BillingStatementDetail2Pribadi = "SLP.02.0059";
            public const string BillingStatementDetail2PribadiEN = "SLP.02.0059EN";//english
            public const string BillingStatementRekapPribadi = "SLP.02.0060";
            public const string BillingStatementRekapPribadiEN = "SLP.02.0060EN";//english
            public const string BillingStatementLabFarLogPribadi = "SLP.02.0061";
            public const string BillingStatementRekap2 = "SLP.02.0062";

            public const string PaymentReceiptSlip = "SLP.02.0006";
            public const string PaymentReceiptDetail = "SLP.02.0015";
            public const string PaymentReceiptGlobal = "SLP.02.0016";
            public const string PaymentReceiptDetailOutPatient = "SLP.02.0017";
            public const string DownPaymentReceiptDetailOutPatient = "SLP.02.0018";
            public const string RSSA_PaymentRRtInPatientP = "SLP.02.0019";
            public const string RSSA_PaymentRRtInPatientG = "SLP.02.0020";
            public const string RSSA_Slip_Mandiri = "SLP.02.0021";
            public const string PaymentReceiptSlip2 = "SLP.02.0051";

            public const string PrescriptionQueSlip = "SLP.02.0084";

            public const string PaymentReceiptDetailOutPatient2 = "SLP.02.0085";
            public const string JobOrderLabel = "SLP.02.0088";
            public const string JobOrderLabelLarge = "SLP.02.0094";
            public const string OperationCostEstimationForm = "SLP.02.0089";
            public const string OperationCostEstimationFormSummary = "SLP.02.0090";
            public const string JobOrderLabelRadiologi = "SLP.02.0095";
            public const string Permit = "SLP.02.0096"; /*surat izin pulang*/

            public const string BillingStatementBpjs = "SLP.02.1001"; /*modif dari SLP.02.0057*/
            public const string BillingStatementBpjsWithPrice = "SLP.02.1001B"; /*modif dari SLP.02.0057*/
            public const string BillingStatementBpjsByPaymentReceipt = "SLP.02.1002"; /*SLP.02.0038*/
            public const string BillingStatementPaymentReceiptGuarantorOnly = "SLP.02.1004"; /*SLP.02.0038*/
            public const string BillingStatementPaymentReceiptGuarantorOnlyEN = "SLP.02.1004EN"; /*SLP.02.0038*/
            public const string PatientDepositReceive = "XML.02.0046";

            public const string PatientTransfer = "XML.01.087";
            public const string PersyaratanTurunKls = "XML.01.088";

            public const string BpjsSEP = "XML.01.0042";
            public const string BpjsESEP = "XML.01.0042e";

            //asset
            public const string AssetWorkOrder = "SLP.05.1001";

            //nutrient
            public const string MealOrderOptionalMenuSlip = "SLP.06.0001";
            public const string MealOrderStandardMenuSlip = "SLP.06.0002";
            public const string DistributionOptionalMenuSlip = "SLP.06.0003";
            public const string DistributionStandardMenuSlip = "SLP.06.0004";
            public const string DistributionLiquidMenuSlip = "SLP.06.0006";
            public const string DistributionPortionOprSlip = "SLP.06.0007";

            public const string LetterOrderFoodRpt = "RPT.06.0003";

            //report
            public const string SalesMedicalGlobalRpt = "RPT.02.0051";
            public const string SalesByMedicalItemRpt = "RPT.02.0052";
            public const string SalesByMedicalItemGroupRpt = "RPT.02.0053";

            public const string DistributionGlobalRpt = "RPT.03.0036";
            public const string DistributionDetailRpt = "RPT.03.0037";

            public const string JournalVoucher = "RPT.05.0001";
            //public const string DailyRecapInpatientUnitRpt = "RPT.01.0025";
            public const string AP_Invoicing = "SLP.03.0020";
            public const string AP_Invoicing2 = "SLP.03.0021";
            public const string AP_PaymentOrder = "SLP.05.0026";
            public const string AP_PaymentOrder2 = "SLP.05.0026B";
            public const string RSPPAR_Invoicing = "RPT.02.0002"; //"RPT.02.0002"; 
            public const string AR_Invoicing = "SLP.05.0006"; //"RPT.02.0002";
            public const string AR_Invoicing_Bukopin = "SLP.05.0007"; //"RPT.02.0002";
            public const string AR_Invoicing_BNI = "SLP.05.0010";
            public const string AR_Invoicing_BCA = "SLP.05.0012";
            public const string AR_PaymentReceiveReceipt = "SLP.05.0011";
            public const string RSUI_AP_Invoicing = "SLP.05.0013";
            public const string AR_InvoicingDetailRpt = "SLP.05.0014";
            public const string AR_InvoicingDetailRpt2 = "SLP.05.0016";
            public const string AR_InvoicingDetailRptIpr = "SLP.05.0017";
            public const string AR_InvoicingDetailRptIpr2 = "SLP.05.0018";
            public const string AR_InvoicingReceipt = "SLP.05.0019";
            public const string CashTransactionVoucher = "RPT.05.0002";
            public const string PhysicianFeeRentalRoomsSlip = "SLP.05.0003";
            public const string PhysicianFeeSlip = "SLP.05.0004";
            public const string PhysicianFeeAddDeducSlip = "SLP.05.0001";
            public const string PhysicianFeeSummarySlip = "SLP.05.0005";
            public const string PhysicianFeePaymentSlip = "SLP.05.0002";
            public const string PhysicianFeeAddDeducSlipPayment = "SLP.05.0006";
            public const string PhysicianFeeRentalRoomsSlipPayment = "SLP.05.0007";
            public const string PhysicianFeeSlipPayment = "SLP.05.0008";
            public const string PhysicianFeePaymentDetailSlip = "SLP.05.0009";
            public const string ARPaymentSlip = "SLP.05.0010";
            public const string ARPaymentSlipPersonal = "SLP.05.0020";
            public const string TracerRpt = "SLP.01.0012";
            public const string RegistrationLabel = "SLP.01.0055";
            public const string PrescriptionSalesPerGuarantor = "RPT.02.0070";
            public const string PhysicianFeeCalculationDraftSlip = "SLP.02.0049";
            public const string PhysicianFeeVerificationSlip = "SLP.02.0050";
            public const string PhysicianFeeCalculationDraftSlip2 = "SLP.05.0022";
            public const string PhysicianFeeCalculationDraftSlipByItem = "SLP.05.0024";
            public const string PhysicianFeeCalculationDraftSlip2ByItem = "SLP.05.0025";
            public const string PhysicianFeePaymentPerGroup = "SLP.05.0027";
            public const string PhysicianFeePaymentPerGroupHeader = "SLP.05.0028";

            public const string RSCH_REKAP_RESEP_PASIEN = "XML.02.0006";
            public const string RSUI_LABEL_GIZI_PASIEN = "XML.01.0004a";

            //Blood Bank
            public const string BloodBankCrossMatchingLabel = "SLP.13.0001";


            //RL Repoting
            public const string RL1_2 = "SLP.01.0025";
            public const string RL3_1V2025 = "SLP.01.0025b";
            public const string RL1_3 = "SLP.01.0026";
            public const string RL2 = "SLP.01.0027";
            public const string RL3_1 = "SLP.01.0028";
            public const string RL3_2 = "SLP.01.0029";
            public const string RL3_2V2025 = "SLP.01.0029b";
            public const string RL3_3 = "SLP.01.0030";
            public const string RL3_4 = "SLP.01.0031";
            public const string RL3_5 = "SLP.01.0032";
            public const string RL3_6 = "SLP.01.0033";
            public const string RL3_7 = "SLP.01.0034";
            public const string RL3_8 = "SLP.01.0035";
            public const string RL3_9 = "SLP.01.0036";
            public const string RL3_10 = "SLP.01.0037";
            public const string RL3_10V2025 = "SLP.01.0037B";
            public const string RL3_11 = "SLP.01.0038";
            public const string RL3_11V2025 = "SLP.01.0038b";
            public const string RL3_12 = "SLP.01.0039";
            public const string RL3_12V2025 = "SLP.01.0039V";
            public const string RL3_13 = "SLP.01.0040";
            public const string RL3_13b = "SLP.01.0041";
            public const string RL3_13V2025 = "SLP.01.0041V";
            public const string RL3_14 = "SLP.01.0042";
            public const string RL3_15 = "SLP.01.0043";
            public const string RL3_16V2025 = "SLP.01.0060";
            public const string RL3_19 = "SLP.01.0125";
            public const string RL4A = "SLP.01.0044";
            public const string RL4ASebab = "SLP.01.0045";
            public const string RL4B = "SLP.01.0046";
            public const string RL4BSebab = "SLP.01.0047";
            public const string RL4_3V2025 = "SLP.01.0129v";
            public const string RL5_1 = "SLP.01.0048";
            public const string RL5_2 = "SLP.01.0049";
            public const string RL5_3 = "SLP.01.0050";
            public const string RL5_4 = "SLP.01.0051";
            public const string RL5_1_kemenkes = "SLP.01.0052";
            public const string DataRpt = "SLP.01.0088";
            public const string RL3_17 = "SLP.01.0127";
            public const string RL3_9v2025 = "SLP.01.0128";
            public const string RL4_2V2025 = "SLP.01.0129";
            public const string RL3_3V = "SLP.01.0029v";
            public const string RL3_4_V2025 = "SLP.01.0053";
            public const string RL3_5_V2025 = "SLP.01.0130";
            public const string RL3_14_V2025 = "SLP.01.0126";
            public const string RL3_6V2025 = "SLP.01.0033b";
            public const string RL3_15V2025 = "SLP.01.0043b";
            public const string RL4_1V2025 = "SLP.01.0044B";
            public const string RL5_2V2025 = "SLP.01.0051B";
            public const string RL5_3V2025 = "SLP.01.0132";
            public const string RL51V2025 = "SLP.01.0046v";
            public const string RL3_8V2025 = "SLP.01.0134";
            public const string RL3_7V2025 = "SLP.01.0034V";

            public const string BPJSSep = "XML.01.0042"; //= "XML.01.0023";
            public const string ResumeSensusHarian = "RPT.01.0161"; //RADT-ResumeSensusHarian
            public const string SensusHarianPenderitaDirawat = "RPT.01.0007"; //RADT-DailyCensusOfPatientsInCareRpt
            public const string MotherChildWristband = "SLP.01.0094";
            public const string PatientIdentity = "SLP.01.0002";

            public const string NursingCareAssessment = "SLP.10.0001";
            public const string NursingCareDiagnoseAndPlanning = "SLP.10.0002";
            public const string NursingCareNotes = "SLP.10.0003";
            public const string NursingCareProgressNotes = "SLP.10.0004";
            public const string NursingCareIntegratedNotes = "SLP.10.0005";

            //Inventory
            //slp
            public const string PurchaseRequestSlp = "SLP.03.0006";
            public const string DistributionRequestSlp = "SLP.03.0013";
            public const string InventoryIssueRequestSlp = "SLP.03.0035";
            public const string Location_ItemMedic = "SLP.03.0030.11";
            public const string Location_ItemNonMedic = "SLP.03.0030.21";
            public const string Location_ItemKitchen = "SLP.03.0030.81";
            public const string PriceQuoteRequest = "SLP.03.0034";

            //Asset
            //rpt
            public const string PreventiveMaintenanceSchedule = "RPT.05.1001";

            //slp
            public const string SentToThirdPartiesSlp = "SLP.05.1005";

            public const string ServiceUnitBooking = "XML.RSMM.04.0001";

            public const string RadiologyResult = "XML.02.0027";

            public const string Pathology2Result = "SLP.01.08.01";

            //payroll
            public const string SlipSalaryPerEmployee = "SLP.08.0001";
            public const string SlipThrPerEmployee = "SLP.08.0002";

            public const string Appraisal1 = "SLP.07.11.01";

            //emr-laporan operasi & laporan anestesi
            public const string OperatingNotesRpt = "SLP.EMR.OPR.01";
            public const string AnesthesistNotesRpt = "SLP.EMR.ANS.01";

            //0003994 daniel
            public const string TemplatePpaNotes = "SLP.10.0009";
        }

        public class Message
        {
            public const string PasswordCantEmpty = "Password can't be empty.";
            public const string PasswordConfirmNotValid = "Password Confirm must be the same with Password.";
            public const string DuplicateKey = "Record with this key/value has registered.";
            public const string RecordNotExist = "Record not exist, maybe has deleted by other user.";
            public const string RecordHasApproved = "This data has been approved.";
            public const string RecordHasNotApproved = "This data has not been approved.";
            public const string RecordHasVerified = "This data has been verified.";
            public const string RecordCanNotEdited = "Data can't be edited.";
            public const string RecordCanNotDeleted = "Data can't be deleted.";
            public const string RecordUsedByOther = "This data having relation with another.";
            public const string RecordHasClosed = "This data has been closed.";
            public const string RecordHasVoided = "This data has been void.";
            public const string RecordDetailEmpty = "Data can't be save because detail is empty.";
            public const string BedAlreadyRegistered = "Bed is already registered to another patient.";
            public const string SelectValidSupplier = "Please select valid supplier.";
            public const string TRANSACTION_DATE_HAS_CLOSED = "Selected Transaction Date has been closed.";
            public const string RecordHasUsed = "This data has been used in the system.";

            public const string LEVEL1_NOT_EXIST = "Account Level 1 is not exist. Please, Create Account Level 1 first ...";
            public const string LEVEL2_NOT_EXIST = "Account Level 2 is not exist. Please, Create Account Level 2 first ...";
            public const string LEVEL3_NOT_EXIST = "Account Level 3 is not exist. Please, Create Account Level 3 first ...";
            public const string LEVEL4_NOT_EXIST = "Account Level 4 is not exist. Please, Create Account Level 4 first ...";

            public const string LEVEL1_CANNT_DELETED = "Data can't be delete, Account Level 1 has exist. Delete Account Level 1 first ...";
            public const string LEVEL2_CANNT_DELETED = "Data can't be delete, Account Level 2 has exist. Delete Account Level 2 first ...";
            public const string LEVEL3_CANNT_DELETED = "Data can't be delete, Account Level 3 has exist. Delete Account Level 3 first ...";
            public const string LEVEL4_CANNT_DELETED = "Data can't be delete, Account Level 4 has exist. Delete Account Level 4 first ...";
            public const string LEVEL5_CANNT_DELETED = "Data can't be delete, Account Level 5 has exist. Delete Account Level 5 first ...";

            public const string PasswordRequired = "Password required.";
        }

        public class RegistrationType
        {
            public const string ClusterPatient = "CTR";
            public const string EmergencyPatient = "EMR";
            public const string InPatient = "IPR";
            public const string OutPatient = "OPR";
            public const string MedicalCheckUp = "MCU";
            public const string Ancillary = "ANC";
        }

        public class AppoinmentType
        {
            public const string OutPatient = "OPR";
            public const string MedicalCheckUp = "MCU";
        }

        public class BillingLink
        {
            public const string GUARANTOR = "L01";
            public const string EMPLOYEE = "L02";
            public const string SUPPLIER = "L03";
            public const string LOAN_BANK = "L04";
            public const string DOCTOR = "L05";
        }

        public class BillingLinkType
        {
            public const string ADDNEW = "1";
            public const string PAYMENT = "2";
            public const string RETURN = "3";
            public const string CANCEL = "4";
        }

        public class SubsidiaryType
        {
            public const string BANK = "S02";
            public const string GUARANTOR = "S04";
            public const string EMPLOYEE = "S05";
            public const string DONATOR = "S07";
            public const string SUPPLIER = "S08";
        }

        public class VoucherType
        {
            public const string RECEIVE = "V01";
            public const string PAYMENT = "V02";
            public const string MEMORIAL = "V03";
            public const string AUTOMATIC = "V04";
        }

        public class VoucherTransType
        {
            public const string DEBET = "0";
            public const string CREDIT = "1";
        }

        public class AcctGroup
        {
            public const string ASSETS = "01";
            public const string LIABILITIES = "02";
            public const string CAPITAL = "03";
            public const string REVENUE = "04";
            public const string OPR_EXPENSE = "05";
            public const string GEN_EXPENSE = "06";
            public const string NON_OPR_INCOME = "07";
            public const string NON_OPR_EXPENSE = "08";
        }

        public class InvoiceStatus
        {
            public const string PROCESS = "0";
            public const string VERIFY = "1";
            public const string PAYMENT = "2";
            public const string CLOSED = "3";
        }

        public class InvoicePayment
        {
            public const string CASH = "1";
            public const string BANK = "2";
            public const string CHEQUE = "3";
            public const string OTHER = "4";
        }

        public class ParamedicFeeAdjustType
        {
            public const string ADD = "01";
            public const string DEDUC = "02";
        }

        public class DisplayFormat
        {
            public static CultureInfo DateCultureInfo
            {
                get
                {
                    return new CultureInfo("id-ID");
                }
            }
            public static CultureInfo NumericCultureInfo
            {
                get
                {
                    return new CultureInfo("en-US");
                }
            }

            // Susunan tanggal harus sesuai dengan properties DateCultureInfo 
            public const string Date = "dd/MM/yyyy";
            public const string DateTime = "dd/MM/yyyy HH:mm";
            public const string DateTimeSecond = "dd/MM/yyyy HH:mm:ss";
            public const string DateLong = "dd MMM yyyy";
            public const string DateShortMonth = "dd-MMM-yyyy";
            public const string LongDatePattern = "dd-MMM-yyyy HH:mm:ss";
            public const string DateHourMinute = "dd/MM/yyyy HH:mm";
            public const string DateShortMonthHourMinute = "dd-MMM-yyyy HH:mm";
            public const string HourMin = "HH:mm";
            public const string Date2 = "dd MMMM yyyy";

            public const string Qty = "n0";
            public const string Amount = "n0";
            public const string TotalAmount = "n0";

            // Untuk format tgl pada script SQL
            public const string DateSql = "yyyy-MM-dd";

        }

        public class TransChargesType
        {
            public const string HealthScreening = "HSC";
            public const string JobOrder = "JOR";
            public const string ServiceUnitTransaction = "SUT";
        }

        public class UserAccessType
        {
            public const string Delete = "delete";
            public const string Void = "void";
            public const string UnApproved = "unapproved";
        }

        public class Module
        {
            //parent program id for human resource & payroll module
            public static readonly string[] HumanResourceModules = new[] { "07", "08" };
        }

        public class HtmlChar
        {
            public const string Bullet = "\u2022";
            public const string Spasi = "&nbsp;";
            public const string Tab = "&nmsp;";
            public const string BreakLine = "<br />";
        }

        public class AntibioticLevel
        {
            public const int AllAntibiotic = 9999;
            public const int StepUp = 1000;
            public const int StepDown = 1001;
            public const int SwitchIvToOral = 1003;
            public const int AddAntibiotic = 1002;
            public const int NoNeedAntibiotic = 0;
            public const int UseAbNonProphylaxis = 1004;
        }
        public class RasproType
        {
            public const string Rasal = "RASAL";
            public const string Raslan = "RASLAN";
            public const string Raspatur = "RASPATUR";
            public const string Prophylaxis = "PPLAXIS";
            public const string Raspraja = "RASPRAJA";
            
        }

        public class NsDiagnosaType
        {
            public const string Nursing = "01";
            public const string Midwifery = "02";
            public const string Nutrition = "03";
        }
    }
}
