using System;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppParameter
    {
        public enum ParameterItem
        {
            IsSatuSehatDirectSend,
            ComboBoxDataServiceMaxResultRecord,
            IsEmrChiefComplaintTextRequired,
            IsAutoSaveMdsDpjpToSepFolderAfterSave,
            IsAutoSaveOtherExamResultToDocFolderAfterSave,
            PartographColCount,
            IsAssessmentFamilyOrPatientSign,
            IsAssessmentPhysicianSign,
            IsMedicalNoContainStrip,
            IsEmrIcdXListMandatory,
            acc_CoaRevenueTopLevel,
            acc_IsInventoryBySrRegServiceUnit,
            IsEmrPhysicianAssessmentMandatory,
            IsEmrAssementDiagnoseTextVisible,
            IsEmrAssementDiffDiagnoseTextVisible,
            IsEmrContinueAssementSubjectivetVisible,
            IsEmrContinueAssementAssessmentVisible,
            IsIwlUsingTemperature,
            IsShowOtherParamedicBillingAtEmr,
            IsAutoApprovedServiceUnitTxFromEmr,
            IsAutoApprovedExamOrderFromEmr,
            AutoSaveInterval,
            ParamedicFirstTransChargesItemIds,
            ServiceUnitIdListForUdd,
            AssessmentPhotoSize,
            IsEmrListShowPlafondProgress,
            IsOtcFreeRecipeMargin,
            acc_IsAutoJournalARPayment,
            acc_AutoJournalArApInTwoStep,
            acc_IsAutoJournalARInvoicing,
            acc_IsAutoJournalCashSalesToCustomer,
            acc_IsAutoJournalSales,
            DriveThruAutoBillItemID,
            DriveThruServiceUnitID,
            DriveThruGoogleSpreadsheetId,
            DriveThruGoogleUser,
            DriveThruGoogleAppName,
            IsMedicalDischargeSummaryDefaultValue,
            IsMedicalDischargeSummaryPrescDefaultValue,
            IsMedicalDischargeSummaryHomPrescAll,
            IsMedicalDischargeSummaryPrescJustItemName,
            GuarantorTypeBpjs,
            GuarantorTypeBpjsKapitasi,
            PCarePassword,
            PCareTimeOutInSecond,

            PatientDocumentScanCompression,
            IsLogProgramAccess,
            vs_HeartRateID,
            vs_SystolicID,
            vs_DiastolicID,
            vs_RespiratoryID,
            vs_TemperatureID,
            CasemixValidationRegistrationType,
            ClinicalPathwayRegistrationType,
            BpjsIgdUgdBridgingID,
            RegistrationTypeForAccrualJournal,
            EmployeeStatueResignReference,
            AppointmentStatusCancel,
            AntrolPrintLabelOnKiosk,
            AppointmentStatusClosed,
            AppointmentStatusOpen,
            AppointmentStatusConfirmed,
            AppointmentStatusNoResponse,
            AppointmentStatusBooked,
            BedStatusOccupied,
            BedStatusPending,
            BedStatusUnoccupied,
            BedStatusBooked,
            BedStatusGoToOperatingRoom,
            BedStatusReserved,
            BedStatusCleaning,
            BedStatusRepaired,
            BusinessMethodAllIn,
            BusinessMethodCoverage,
            BusinessMethodFlavon,
            BusinessMethodBpjs,
            CompleteStatusRM,
            ClusterPatientClassID,
            ClusterPatientDepartmentID,
            DefaultTariffClass,
            DefaultTariffType,
            DiscountValueType,
            DrugAllergenGroupID,
            FoodAllergenGroupID,
            EmergencyDepartmentID,
            MedicalSupportDepartmentID,
            EmergencyPatientClassID,
            GuarantorRuleTypeDiscount,
            GuarantorRuleTypeMargin,
            GuarantorRuleTypePlavon,
            HealtScreeningServiceUnitID,
            HealthcareInitialAppsVersion,
            EklaimRemoveDashSeparatorOnMedicalNo,
            InPatientDepartmentID,
            ItemGroupMaterai,
            ItemGroupMcuPackageID,
            MaxResultRecord,
            MaxResultRecordEmrList,
            MpiSecretKey,
            OutPatientClassID,
            OutPatientDepartmentID,
            MedicalCheckUpDepartmentID,
            OTCPrescriptionPatientID,
            ParamedicTariffComponentID,
            PayableStatusClosed,
            PayableStatusPaid,
            PayableStatusProcess,
            PayableStatusVerify,
            PaymentMethodPackageBalance,
            PaymentMethodCash,
            PaymentMethodBiaya,
            PaymentMethodCreditCard,
            PaymentMethodDebitCard,
            PaymentMethodTransfer,
            PaymentMethodQris,
            PaymentTypeCorporateAR,
            PaymentTypeBackOfficePayment,
            PaymentTypeDiscount,
            PaymentTypeDownPayment,
            PaymentTypePayment,
            PaymentTypePersonalAR,
            PaymentTypeSaldoAR,
            PaymentMethodCashAR,
            PaymentTypeReturn,
            PharmacyDepartmentID,
            PhysicianTypeAnesthetic,
            PhysicianTypeAssistant,
            PhysicianTypeAssAnesthesia,
            PhysicianTypeInstrumentator,
            RadiologyNoFormat,
            RadiologyNoAutoCreate,
            RadiologyParamedicId,
            RadiologyUnitID, //--> double with ServiceUnitRadiologyID
            ReceivableStatusClosed,
            ReceivableStatusPaid,
            ReceivableStatusProcess,
            ReceivableStatusVerify,
            ReceivableTypeCorporate,
            ReceivableTypePersonal,
            RoundingPayment,
            RoundingPaymentWithCard,
            RoundingTransaction,
            RoundingPrescription,
            RoundingGlobalTransaction,
            SelfGuarantorID,
            DefaultRetrirementAge,
            ServiceUnitForRequestOrder,
            ServiceUnitPharmacyID,
            ServiceUnitPharmacyIdOpr,
            ServiceUnitPharmacyIdPos,
            ServiceUnitKiaId,
            ServiceUnitImmunizationId,
            ServiceUnitObstetricsId,
            ServiceUnitRadiologyID,
            ServiceUnitRadiologyID2,
            ServiceUnitRadiologyIDs,
            ServiceUnitMedicalRehabId,
            ServiceUnitRadiologyIdArray,
            ShiftAfternoon,
            ShiftMorning,
            ShiftNight,
            ShiftStartAfternoon,
            ShiftStartMorning,
            ShiftStartNight,
            TaxPercentage,
            ServiceUnitLaboratoryID,
            ServiceUnitLaboratoryIdArray,
            ServiceUnitMcuId,
            TariffComponentJasaSaranaID,
            TariffComponentJasaMedisID,
            InvoicePaymentCash,
            InvoicePaymentDiscount,
            InvoicePaymentGiro,
            OrderResultFolderPath,
            FinanceHead,
            FinanceHeadJob2,
            FinanceHeadID,
            FinanceHeadJob,
            PrescriptionReturnAdminValue,
            PaymentTypePaymentName,
            PaymentMethodCashName,
            PaymentMethodTransferName,
            InventoryHeadOfficer,
            GuarantorTypeMemberID,
            PaymentTypeInvoiceSupplierPayment,
            PaymentTypeCredit,
            PPH22,
            PPH23,
            PPH23NonNpwp,
            PercentPph21Base,
            InvoiceTermOfPayment,
            OpticDepartmentID,
            MedicalFileCategoryIn,
            MedicalFileCategoryOut,
            MedicalFileStatusConfirm,
            MedicalFileStatusRequest,
            RentalRoomsPercentage,
            IsPhysicianFeeUsingTaxCalculation,
            CurrencyRupiahID,
            HealthcareID,
            IsRegistrationPrintAutomatic,
            RegistrationSlipRpt,
            RegistrationSlipKioskRpt,
            RegistrationInpatientIdentityRpt,
            RegistrationLabelRpt,
            RegistrationLabelOpRpt,
            RegistrationLabelEmRpt,
            RegistrationLabelNewPatientRpt,
            EmployeeMedicalInsuranceFormRpt,
            EmployeeMaritalStatusForMedicalInsurance,
            IsVisibleEmployeeMedicalInsuranceForm,
            RecruitmentTestInterview,
            DefaultSurgeryTime,
            OperatingRoomBookingLimit,
            TracerRpt,
            TracerOpRpt,
            TracerErRpt,
            PatientIdCardRpt,
            EmployeeClinicalAssignmentLetterKomedRpt,
            EmployeeClinicalAssignmentLetterKomkepRpt,
            EmployeeClinicalAssignmentLetterKtklRpt,
            InPatientServiceUnitID,
            PatientCardItemID,
            OperatingRoomServiceUnitID,
            IsOperatingRoomResetPrice,
            IsOperatingRoomResetPriceLastClass,
            IsOperatingRoomResetPriceHighestClass,
            IsAllowEditProcedureChargeClass,
            ParamedicTeamStatusMain,
            PharmacyHead,
            PharmacyHeadHomeAddr,
            PharmacyHeadJob,
            Director,
            PicDirectorForFinanceLogistic,
            PicManagingDirector,
            PicManagingDirectorForInvoicing,
            PicManagingDirectorPhoneNo,
            PicPurchasing,
            PicFinance2,
            PicAsFinance2,
            PicLogisticHead,
            PicWarehouseHead,
            IsCreateItemIdProductAutomatic,
            IsCreateItemIdProductAutomaticUseGroupInitial,
            IsCreateItemIdProductAutomaticUseNameSeparated,
            IsCreateItemIdServiceAutomaticUseGroupInitial,
            IsCreateSupplierIdAutomatic,
            IsCreateFabricIdAutomatic,
            IsCreateParamedicIdAutomatic,
            IsCreateGuarantorIdAutomatic,
            IsCreateFoodIdAutomatic,
            IsCreateReferralGroupIdAutomatic,
            IsCreateReferralIdAutomatic,
            ItemProductCostPriceType,
            RecipeMarginValueNonCompound,
            RegistrationTypeInpatient,
            RegistrationTypeOutpatient,
            RegistrationTypeEmergency,
            FinanceFaxNoDirect,
            IsUppercasePatientID,
            IsRegistrationPrintLabel,
            IsRegistrationOpPrintLabel,
            IsRegistrationEmPrintLabel,
            IsRegistrationPrintLabelNewPatient,
            IsRegistrationPrintSlip,
            IsRegistrationIdentity,
            IsRegistrationPrintReceipt,
            IsRegistrationPrintTicket,

            IsRegistrationMcuPrintSlip,
            IsRegistrationMcuPrintTicket,
            IsRegistrationMcuPrintLabel,
            RegistrationSlipMcuRpt,
            RegistrationTicketMcuRpt,
            RegistrationLabelMcuRpt,

            IsRegistrationTracer,
            IsRegistrationTracerToAllReg,
            IsRegistrationTracerToAllRegType,
            RegistrationTracerToAllRegTypeExc,
            RegistrationReceiptRpt,
            RegistrationTicketRpt,
            IsRegistrationPrintPatientIdCard,
            IsSelfCheckinPrintingSEP,
            SepProgramIdRpt,
            IsAutoClosedRegOnDischargePermit,
            IsAutoClosedRegOpOnPayment,
            IsAutoClosedRegIpOnPayment,
            TariffComponentPriceVisible,
            IsTariffComponentPriceVisibleForBilling,
            RegistrationCanChangeBedNo,
            IsAllowMultipleRegOp,
            IsAllowMultipleRegOpWithSameUnitAndPhysician,
            IsReferPatientUsingClassBefore,
            PatientInTypeIp,
            PatientInTypeOp,
            PatientInTypeEr,
            TariffComponentBhp,
            RecipeMarginValueCompound,
            HealthcareInitial,
            IsInventoryIssueUsingRequest, //Obsolete, baca dari parameter list yg dikirim di form InventoryIssueDetail.aspx.cs
            IsUsingHisInterop,
            IsUsingHisInteropWithMultipleConnection,
            IsUsingHisInteropCorrection,
            HisInteropConfigName,
            IsUsingHisInteropToHcLab,
            MpiUrlApi,
            DefektaSupplierID,
            OperatingTheaterClusterID,
            RequestOrderServiceUnitID,
            TransferOrderServiceUnitID,
            PrinterManagerHostDefault,
            TimeLimitForVoidPayment,
            GuarantorAskesID,
            ItemGroupBmhp,
            GuarantorJamsostekID,
            GuarantorEmployeeID,
            PettyCashUnitFinanceID,
            PettyCashUnitCashierID,
            IsUsingPettyCash,
            CoaUsingClass,
            acc_IsUnitBasedProductAccount,
            acc_IsAutoApprovedPayment,
            acc_IsAutoApprovedPaymentBackOffice,
            IsUsingHumanResourcesModul,
            GuarantorTypeEmployee,
            GuarantorTypeInsurance,
            GuarantorTypeDiscount,
            ItemIdOngkir,
            UsingPromotion,
            IsUsingTerminalDigitMedicalNo,
            IsUsingIntermBill,
            IsUsingExtramuralItem,
            IsUsingApprovalPurchaseRequest,
            IsPhycisianInRegEditable, // settingan edit dokter di grid registration tdk muncul jika tdk ingin digunakan fasilitas ini
            IsGuarantorInRegEditable, // settingan edit guarantor di grid registration tdk muncul jika tdk ingin digunakan fasilitas ini
            NonClassID,
            LocationKitchenID,
            CitoPercentage,
            IsUsingPasswordPolicy,
            IsIndependentVoidRegistration,
            IsUsingRoomingIn,
            IsShowGenderOnBedInformationStatus,
            MaxDiscTxInPercentage,
            MaxDiscTxTariffRsInPercentage,
            IsSharePurchaseDiscToPatient,
            IsSharePurchaseDiscToPatientFull,
            IsAllowEditPorDate,
            TablePatientFieldValidation,
            TableRegistrationResponsiblePersonFieldValidation,
            ClosingJournalWithoutAllApproved,
            MaxAttemptFailedLogin,
            PasswordMinimumLength,
            PasswordUpperCaseLength,
            PasswordLowerCaseLength,
            PasswordNumericLength,
            PasswordNonAlphaLength,
            acc_IsAutoApprovedPOReceived,
            acc_IsAutoApprovedConsReceived,
            acc_IsAutoApprovedConsReturned,
            acc_IsAutoApprovedInventoryIssue,
            acc_IsAutoApprovedInventoryAdjustment,
            acc_IsAutoApprovedInventoryStockOpname,
            acc_IsAutoApprovedInventoryProduction,
            acc_IsAutoApprovedInventoryDistribution,
            acc_IsAutoJournalCashPOReceived,
            acc_IsAutoJournalHPP,
            acc_IsAutoJournalSalesDiscount,
            acc_IsJournalTaxAndDiscountSeparated,
            acc_IsAutoJournalFinalizeBilling,
            acc_IsJournalCashBased,
            acc_IsEnableGuarDiscProrataToRevenue,
            acc_IsEnableParamedicPayable,
            acc_IsJournalAccualNoTemporary,
            acc_IsJournalPatReceivedIGD,
            acc_IsAutoCashEntryOnPaymentPatient,
            acc_IsAutoCashEntryOnAssetAuction,
            acc_IsAutoCashEntryOnPayroll,
            acc_IsJournalPackageB4Approve,
            acc_IsAutoJournalPayroll,
            acc_IsAutoApprovedPayroll,
            acc_IsJournalPayrollWithDefaultNormalBalance,
            acc_IsJournalPayrollWithDirectIndirectCost,
            acc_IsJournalPayrollWithDirectIndirectCost2,
            acc_IsAutoJournalAssetDestruction,
            acc_IsAutoJournalAssetAuction,
            acc_IsAutoJournalAssetMovement,
            acc_IsAutoApprovedAssetDestruction,
            acc_IsAutoApprovedAssetAuction,
            acc_IsAutoApprovedAssetMovement,
            IsOutPatientInculeInAdminCalculation,
            InventoryPhaHeadOfficer,
            PicMedicalDirector,
            PicPharmacyCoordinator,
            PicHeadOfAdmitting,
            PicWarehouse,
            DpAmtClassVip,
            DpAmtClassI,
            DpAmtClassII,
            DpAmtClassIII,
            DpAmtClassIcu,
            IsPhysicianFeeBasedOnPayment,
            IsPhysicianFeeArBasedOnPayment,
            IsPhysicianFeeArPaidBasedOnPayment,
            IsPhysicianFeeArCreateOnArReceipt,
            DependentsOfEmployeesGuarantorID,
            IsPemisahanCOAUangRacikan,
            PicCeo,
            DischargeConditionDieLessThen48,
            DischargeConditionDieMoreThen48,
            DischargeConditionDie,
            IsRADTLinkToHumanResourcesModul,
            HealthcareLicenseNumber,
            HealthcareMarketingEmailAddr,
            HealthcareFinanceEmailAddr,
            BankCashierID,
            IsNavigateUrlForDistributionWithStockInfo,
            IsCreateCustomerIdAutomatic,
            GuarantorTypeSelf,
            DefaultTerm,
            DefaultPurchaseOrderType,
            DefaultDownPaymentType,
            IntervalPatientLastVisit,
            DefaultClassMenuStandard,
            IsSeparationBetweenOutpatientAndInpatientSupplies,
            IsDistributionMenuIsUsedAsItemRequestMenu,
            IsCashPurchaseOrderUpdatePrice,
            MedicalRecordServiceUnitID,
            IsItemProductAllowEditByUserVerificated,
            IsAdminCalcIncludeItemProduct,
            IsAdminCalcBeforeDiscount,
            IsDistributionAutoConfirm,
            IsSeparatePaymentForOpConsul,
            IsPurcReturnWithPrice,
            IsAutoClosedDistRequest,
            WorkStatusOpen,
            WorkStatusClosed,
            WorkStatusDone,
            WorkStatusCancelled,
            WorkStatusThirdParties,
            WorkStatusWaitingForParts,
            WorkTypePreventive,
            WorkTypeProject,
            WorkPriorityNormal,
            WorkPriorityRoutine,
            WorkTradeSanitation,
            ChargeBedExecutionTime,
            IsPoAndPorInTheSameUnit, // settingan di POR apakah unit pengadaan sama dengan unit penerimaan
            IsPoWithThreeTypesOfTaxes, // settingan di PO apakah menggunakan 3 pilihan ppn (tanpa, dg, include)
            IsPorCanChangeThePrice, // settingan di POR apakah bisa ubah harga & diskon 
            IsShowSystemQtyInStockTacking,
            IsItemInventoryNameUsingUpperCase,
            VoidReasonBatalBerobat,
            IsUpdatePrescriptionPriceWhenRecal,
            IsUpdatePriceUsingParentRuleWhenRecal,
            IsAllowDiscountInvoice, // settingan di Invoice apakah boleh diskon atau gak

            //IsUsingSeparateNoForEachPharmacyUnit, // settingan Resep & Retur resep, apakah no-ny terpisah antara rawat inap & rawat jalan
            IsForceUseNoIntermbill, // settingan di verif billing v2, u/ Print(Detail) apakah u/ transaksi yg dintermbill saja (No) atau semua transaksi (Yes)
            IsRegistrationRequiredSMF,
            IsInventoryIncludeTax,
            IsInventoryIncludeDiscount,
            IsStockOpnamePerGroupItem,
            Ppn,
            coa_ContemporaryReceivable,
            IsPhysicianFeeCalcBasedOnGuarantorCategory,
            IsDistReqOrPurcReqUsingBudgetPlan,
            MainDistributionServiceUnitIDForNonMedical,
            MainDistributionLocationIDForNonMedical, // lokasi utama distribusi, digunakan untuk validasi budget plan. budget plan hanya akan di-validasi jika permintaan ke lokasi ini atau distribusi dari lokasi ini
            MainPurchasingUnitIDForNonMedical, // puchasing unit untuk validasi budget plan & konsinyasi
            MainPurchasingUnitIDForMedical, // puchasing unit untuk konsinyasi
            SubLedgerGroupIdGuarantor, // ini buat create otomatis subledger pada saat entry master guarantor (kalo guarantor gak pake subledger, isi value = '')
            SubLedgerGroupIdServiceUnit,
            SubLedgerGroupIdSupplier,
            SubLedgerGroupIdParamedic,
            ItemIdAdmRjGuar,
            IsPurcReturnCanChangePrice,
            IsRequestChangeItemProductUpdatePriceSupplierItem,
            ServiceUnitVkId,
            IsUsingMealOrderVerification,
            IsUseStandardMealMenuForAllClass,
            LiquidFoodId,
            BlenderizedFoodId,
            DefaultMenuStandard,
            DefaultChecklistArPayment,
            IsShowPrescPriceOnDisplayDoctor,
            IsPatientCardPrintedOnlyForOutpatients,
            IsRegReferralGroupMandatory,
            IsRegReferralMandatory,
            IsHideOpenCloseOnVerificationForUser,
            IsProductionOfGoodUpdatingTariff,
            IsCreateAssetIdAutomatic,
            IsAutoCreateApplicantNo,
            IsUpdatePhysicianLookingPhysicianFeeVerification,
            PicCeo2,
            BankNameForSlipBank,
            BankAccNameForSlipBank,
            BankAccNoForSlipBank,
            BankCabangSlipBank,
            BankAccNameForSlipBankKalbar,
            BankAccNoForSlipBankKalbar,
            BankNameForSlipBankKalbar,
            BankCabangSlipBankKalbar,
            AssetsStatusActive,
            AssetsStatusInActive,
            AssetsStatusDisposed,
            AssetsStatusLost,
            AssetsStatusSold,
            AssetsStatusDamaged,
            acc_AssetsStatusDepreciationJournal,
            ParamedicIdDokterLuar,
            ReferralGroupDatangSendiri,
            ReferralGroupPASUS,
            ReferralGroupPASUSLabel,
            IsRefreshBeforeLockVerification,
            IsAllowGuarantorDepositBalanceMinus,
            DiagnoseTypeMain,
            DiagnoseTypeInitial,
            DiagnoseTypeDeathDiagnosis,
            SitbDiagnoseList,
            ServiceUnitPharCentralWarehouseId1,
            ServiceUnitPharCentralWarehouseId2,
            TherapyGroupAntibiotics,
            IsWorkOrderRealizationAutoReturn,
            LocationLogisticCentralWHID,
            LocationPharmacyCentralWHID,
            ProductTypeInjeksi,
            PrescShowBlncForEpisodeHistory,
            IsAutoClosedRegOnPaymentWithHoldTx,
            IsAllItemProductSalesPriceIncludeTax,
            IsAutoClosedRegIpOnDischarge,
            IsPrescOrderNeedSoape,
            IsUsingCpoeModule,
            ServiceUnitPathologyAnatomyID,
            IsStockInformationFilterByAuthorizationUnit,
            PhysicianIsRequiredAtRegistration,
            AutoCheckOutOnPayment,
            IsMaterialUsedAwoNeedRequest,
            TheSellingPriceBasedOnTheHighestPrice,
            TheSellingPriceBasedOnTheHighestPriceByPeriod,
            TheSellingPriceBasedOnTheHighestPricePurchPeriod,
            IncidentFollowUpInvestigation,
            PoNonMasterDefPAccount, /*default selected product account for non master item of purchase order*/
            IsNeedEdControlOnPOR,
            IsNeedPriceControlOnPOR,
            IsNeedQtyControlOnPOR,
            TheMinimumTimeLimitEdControlOnPOR,
            IsBookingBedCharged,
            IsUsingHetAsMaxSalesPrice,
            IsAutoClosedRegErOnTransfer,
            IsAutoClosedRegOpOnTransfer,
            IsAutoClosedRegFromOnCheckinConfirmed,
            IsAutoSaveMedicalFileBin,
            MaxMedicalFileBinNo,
            IsPOWithStockInfo,
            AssasmentObgynPenyKandungan,
            AssasmentObgynPoliKebidanan,
            IsTxUsingEdDetail,
            IsEnabledStockWithEdControl,
            IsDisplayPrintButtonInRegistrationFrom,
            IsMealOrderUsingThe10Plus1Rule,
            IsAllowEditPoFromReorderProcess,
            IsAllowEditAmountApInvoice,
            IsBillingAdjustEnabled,
            IsRegSEPMandatory,
            IsReducePriceWhenDeletingMcuPackageDetails,
            IsAllowCorrectionOfIntermBillsTransaction,
            IsAllowEditPoDate,
            ServiceUnitLaundryID,
            ItemGroupFisiotherapyID,
            ItemGroupPathologyAnatomyID,
            IsAllowEditRegistrationDate,
            IsAllowEditDischargeDate,

            //HRD & Payroll
            ReimbursementFactorUnlimit,
            ReimbursementFactorNominal,
            ReimbursementFactorBasicFactor,
            ReimbursementFactorCharacteristics,
            DependentIncludeEmployee,
            UnusedBalanceCarryOver,
            HumanResourceUserID,
            SalaryComponentIdForBasicSalary,
            IsUsingAvgForSalesToBranchPrice,
            DefaultSalaryTemplateID,
            ProcessTypePositionGrade,
            ProcessTypeSalary,
            ProcessTypeOvertime,
            SalaryTypeLoan,
            SalaryTypeOvertime,
            SalaryComponentIdForStructuralBenefits,

            // Nursing care
            NursingAssessmentDO,
            NursingAssessmentDS,
            MinDayBeforeBookingMJkn,
            MaxDayBeforeBookingMJkn,
            MinDayAfterBookingMJkn,
            MaxHourCheckInMJknKiosk,
            IsAutoRefreshEhrList,
            IsAutoPrintPrescriptionOrder,
            ValidateGuarantorContractPeriode,
            ServiceUnitBloodBankID,
            ServiceUnitOperationRoomID,
            ServiceUnitCssdID,
            LisInterop,
            LisCriticalFieldName,
            TransEnty_ShowFilterDateReg,
            IsProductionOfGoodsAutoCssdReceived,
            AllowPOCashInPOR,
            PrescriptionReturnRecipeAmountReturned,
            IsPrescSalesOpNeedSoape,
            QuestionFormIdForWeightHeightIpr,
            QuestionFormIdForWeightHeightOpr,
            QuestionIdForWeight,
            QuestionIdForHeight,
            QuestionIdBodyMassIndex,
            QuestionIdForWeightBaby,
            QuestionIdForHeightBaby,
            GuarantorTypeCompany,
            ValidateGuarantorCardNo,
            ValidateInsuranceID,
            IsBillVerifARGuarantorExclDisc,

            // lokadok (pihak ketiga)
            IsConnectToLokadok,

            BpjsCoverageFormula,
            IsUseApprovalLevel,
            IsDistributionUseApprovalLevel,
            IsHigherApprovalLevelCanBypass,

            EmailAddress,
            EmailPassword,
            EmailPort,
            EmailHost,

            EmailAddressHO,

            IsAllowAdditionalAP,
            PpnOutRJ,
            PpnOutRD,

            AplicationErrorEmailAddress,
            IsStockOpnameFormPerBin,
            IsFeeCalculatedOnTransaction,
            IsTarifCompPhysicianDiscountMaxByShare,
            IsValidatedMedicalFileReceived,
            IsDpReturnUsingChecklist,
            BridgingTypeBpjs,
            IsPrOutstandingListBasedOnCalcQtyOrder,
            IsJobOrderRealizationNeedConfirm,
            IsNeedVoidReasonOnPrescriptionSales,
            IsPphUsesAfixedValue,
            IsAutoFreezeLocationOnStockOpnameAdd,
            IsUnApproveDisabledIfPerClosed,
            IsMinMaxItemBalanceAutoUpdate, //OBSOLETE
            IsMinMaxItemBalanceByStockGroupAutoUpdate,
            PeriodDayHistUsingForCalcMinBalance,
            PeriodDayHistUsingForCalcMaxBalance,
            PeriodDayHistUsingForCalcMinBalPerStockGroup,
            PeriodDayHistUsingForCalcMaxBalPerStockGroup,
            IsInvoicePaymentSeparatedNo,
            IsAutoPrintEtiquette,
            IsAutoPrintSEP,
            IsAutoPrintPrescriptionReceipt,
            IsAutoPrintDistributionReceipt,
            IsAutoPrintStockAdjustmentReceipt,
            IsShowPriceInPurchaseRequest,

            IsPorByStockGroup,
            IsPorBySupplierItem,
            IsPorByProductAccount,
            IsPrescriptionReturnAdminChecked,
            IsPrescriptionDiscountIncludeR,
            IsPOCanEditTax,
            IsPaymentCheckBeforePatientTrans,
            IsLogUserAccessProgram,
            IsUsingRoundingDown,
            IsUsingRoundingDownWithBalancing,
            IsPORoundingDownZeroDigit,
            IsPOCanChangeConversion,
            IsPOCanChangePurchaseUnit,
            IsPOCanChangeQty,
            KioskQueueSlipRpt,
            StockOpnameRowPerPage,
            ParamedicIdLabDefault,
            ParamedicIdRadDefault,
            AppProgramPrintLabelMCU,
            ProgramIdPrintUddEtiquette,
            ProgramIdPrintSurgeryCostEstimation,
            ProgramIdPrintSurgeryBilling,
            ProgramIdPrintDistributionReceipt,
            ProgramIdPrintStockAdjustmentReceipt,
            ProgramIdPrintJobDescription,
            ProgramIdPrintExamOrderOtherResult,
            IsPrescriptionPendingDelivery,
            IsPrescriptionReturnToOneLocation,
            IsPrescriptionReturnToOneLocationWithUserDefUnit,
            IsTestResultAllowModifDate,
            IsManualUserHostName,
            ParamedicTeamStatusDpjpID,
            ParamedicTeamStatusSharingID,
            UserTypeNurse,
            UserTypeDoctor,
            UserTypeNutritionists,
            ServiceUnitIDForIGD,
            IsPrescriptionOnlyInStock,
            AssessmentAcuteRangeInDay,
            AssessmentChronicRangeInDay,
            PhysiotherapyServiceUnitIDs,
            IsNeedAllowCheckoutConfirmedForDischarge,
            IsSeparationOfItemPurchaseCategorization,

            SRReferralGroupDefault,
            IsDisplayRegDateTimeUseCreateDate,
            PhysicianSenderReferralGroups,
            PatientIDForCafe,
            ServiceUnitIDForCafe,
            IsDistributionRequestBasedOnItemsPerLocation,
            IsPurchaseRequestsUsingItemUnit,
            IsAllowPaymentReturnFromCashEntry,
            IsDefaultPaymentReturnFromCashEntry,
            IsControlEatingPatientByNutritionists,
            IsEmrDiagnoseFreeText,
            IsPhysicianPrescriptionSalesDefaultEmpty,
            IsAutoBlacklistOnPersonalAr,
            IsAllowPrescriptionReturnForMultipleRegistration,
            IsPoBasedOnPr,
            IsServiceUnitPrescriptionSalesDefaultEmpty,
            IsCoaAPNonMedicSeparated,
            acc_IsPpnPurchasing,
            acc_IsPpnPurchasingNonMedical,
            PatientInTypeTrueEmergency,
            DefaultUserPassword,
            DefaultParamedicTeamOnEmrList,
            IsAllowCorrectionForIntermBillTx,
            IsListItemForTxOnlyInStock,
            IsRegValidateResponsibleName,
            IsPatientIprOnPrescSalesForCheckinConfirmedOnly,
            IsMoveRecordOnPrescSalesIncludeVoid,
            IsRegistrationVoidReasonRequired,
            IsNeedValidateMobilePhoneNo,
            IsCashEntryShowReceivedFromPaidTo,
            IsDisplayExecutionDateOnPrescriptionSales,
            IsDisplayServiceUnitBookingNoOnTransactionEntry,
            IsDisplayKiaCaseAndObstetricTypeOnTransactionEntry,
            IsBedNeedConfirmation,
            IsBedNeedCleanedProcess,
            IsWorkTradeMandatory,
            IsDischargeDateOnEmrMandatory,
            IsCloseRegOnDischargeEmr,
            SupplierNonPkpTaxStatusDefault,
            IsCloseOutstandingIssueRequest,
            pphFeeBase,
            IsPhysicianFeeVerificationPaidOnly,
            IsAPVerifNeedValidate,
            IsMealOrderValidationForIncompleteItem,
            IsEpisodeDiagValidateExtCauseAndMorp,
            ItemIdImunisasiTT1,
            ItemIdImunisasiTT2,
            ServiceUnitImunisasiTTId,
            IsAutoPrintCafeSlipOrder,
            TariffPriceVisibleOnlyForAdm,
            DefaultConsumeMethod,
            DefaultDosageUnit,
            ServiceUnitCashierID,
            IsUsingCashManagement,
            IsPhysicianFeeCekPaymentUnpaid,
            IsPhysicianFeeCalculatePreFee,
            IsBypassCashierAuthorization,
            IsDiagAndProcListRestoreValueFromCookie,
            IsDiagAndProcListFilterParameter,
            IsReadonlyMedicalNoOnPatientEntry,
            IsReadonlyMedicalNoOnEditPatientEntry,
            IsReadonlyMedicalNoOnUpdateMrnPatient,
            IsReadonlyPatientNameOnEditPatientEntry,
            IsPhysicianFeeShowProcedureNote,
            IsApInvoiceCanChangeThePrice,
            IsMedRecCanChangePatientDischarge,
            IsValidateNoteOnJobOrderLab,
            IsValidateNoteOnAllJobOrder,
            IsMandatoryEmrRegDetail,
            IsPOWithStockInfoTotal,
            IsDefaultEmptyPhysicianOnTransactionEntry,
            IsAutoClosedOnPrApprovalZero,
            acc_IsCoaCashierPaymentTransferFromPaymentMethod,
            IsAutoChargeBedOnRegistration,
            IsItemBinIdAutoCreate,
            IsArPaymentExcessToDiscount,
            FeePaidPercentage,
            ServiceUnitLogisticCentralWarehouseId,
            IsFeePaidPercentageBasedOnTotalInvoice,
            IsPORTaxTypeEnabled,
            IsPhysicianFeeVerifCorrectionAutoCheck,
            IsAllowInventoryIssueWithoutRequest,
            IsInventoryIssueNeedConfirm,
            IsPrescriptionLoadLastBought,
            IsAllowRegistrationEmrChangePhysician,
            IsShowSystemQtyInStockTackingOnBarcode,
            IsPrescriptionReturnNoFormatBasedOnRegType,
            IsFeeCalculatePercentagePaidOnPayment,
            IsShowClassNameOnDispBedinfo,
            IsValidateBpjsCoveredItemOnTx,
            IsReOrderPoBasedOnPrWithSeparatePurchasingUnit,
            IsEnabledReferByPhyisicianOnRegistration,
            acc_JournalPORDate,
            IsBudgetingMedical,
            IsBudgetingNonMedical,
            IsBudgetingKitchen,
            IsAllowVoidRegistrationOnTransfer,
            SepFolder,
            IsAutoApprovePackage,
            IsGuarantorValidateCOA,
            IsUnmergeBillingCheckingIntermbillProcess,
            IsPaymentShowTransactionListForAllRegType,
            IsValidateProductAccountOnItem,
            IsRunTheCostCalculationCleanUpProcess,
            IsCheckinConfirmationUsingDetails,
            FirstTimeCheckMarkForTransfusionMonitoring,
            LblCaptionCheckMarkForTransfusionMonitoring,
            IsBypassBloodCrossMatching,
            IsNeedBloodSample,
            IsNeedSpecimenOnJo,
            IsPrescriptionReviewActived,

            IsByPassEmrUserTypeRestriction,
            IsSoapeCanEntryByUserNonPhsycian,
            IsSickLetterCanEntryByUserNonPhsycian,
            IsPrescriptionCanEntryByUserNonPhsycian,
            IsExamOrderCanEntryByUserNonPhsycian,
            IsReferToSpecialistCanEntryByUserNonPhsycian,
            IsDischargeCanEntryByUserNonPhsycian,
            IsSurgicalCanEntryByUserNonPhsycian,
            IsOperatingNotesCanEntryByUserNonPhsycian,
            NormalTemperature,
            DeadlineEdited,
            PatientHandOverFormID,
            GMT,
            IsMandatoryRegNoOnServiceUnitBooking,
            IsDisplayRegNoOnServiceUnitBooking,
            MultipleForRewardPoints,
            MultipleForRewardPointsForEmployee,
            RewardPointsForPatientGeneral,
            RewardPointsForPatientGuarantee,
            IntervalOrderWarning,
            IntervalTrainingEvaluationSchedule,
            PrescriptionQueueStdiItemID,
            ReservationMaxDuration,
            ReservationMaxDurationForInternal,
            DischargeMethodRefer,
            DischargeMethodInCare,
            ProgramIdPatientLabel,
            ItemIdBloodCrossMatching,
            IsShowExternalQueue,
            PorBaseSalesDay,
            PorForStockDay,
            IsValidateDiagnosisOnRealizationOrderOp,
            IsFoodSelectedByType,
            PrescriptionOrderSlipID,
            IsPrescriptionMustVerifyByDpjp,
            IsPrescriptionNonIPMustDiagnoseMainFirst,
            IsPaymentReceiveAllowBackdated,
            ExcessPaymentAmount,
            IsAllowExcessPaymentAmountPlus,
            IsFeeCalculateProporsionalOnPayment,
            IsFeeEnableRemunByGuarantor,
            IsFeeEnableDualBruto,
            IsFeeTaxProgressiveMonthly,
            IsDemo,
            TmpFolder,
            IsAntibioticRestriction,
            AntibioticRestrictionForLine,
            FoodGroupOneCarbohydrate,
            FoodGroupOneDishMeal,
            IsDisableInventoryStatusOnEditItemProduct,
            IsOvertimeUseApprovalLevel,
            IsEmployeeLeaveUseTwoApprovalLevel,
            EmployeeLeaveApprovalLevel,
            IsUsingPreceptorAsProfessionalIndirectSupervisor,
            IsEmployeeLeavePayCutVisible,
            ParamedicTypeDoctors,
            ItemGroupKitchen,
            IsVisibleItemGroupOnTx,
            IsCollapsedPatientInformationOnBilling,
            IsCollapsedTransactionFilterOnBilling,
            IsAutoChargeBedBasedOnDischargeDate,
            IsCafeAutoPrintPaymentReceive,
            AppProgramCafePaymentReceive,
            DistributionRequestBasedOnLocationToRestriction,
            PurcOrderItemTypeRestrictionForItemSupplier,
            IsValidatedSpecimenOnOrderRealization,
            IsReadOnlyDiscountOnPrescription,
            IsVisibleBtnPurcReqOnDistribution,
            IsPurchaseRequestBasedOnItemsPerLocation,
            IsPurchaseRequestBasedOnItemCategory,
            IsProcurementForItemMedicBasedOnInvCategory,
            IsProcurementForItemNonMedicBasedOnInvCategory,
            IsProcurementForItemKitchenBasedOnInvCategory,
            IsAllowDiscountOnTransEntry,
            IsPrintPatientCardOnNewBornInfant,
            IsRegistrationInpatientOnlyForNewBornInfant,
            IsAdditionalMealOrderUsedClassMenuStandard,
            IsAutoTransfusionBillProceedOnBloodDistribution,
            IsVisibleRequestTypeOnPurchaseRequestPicklist,
            IsCreateZipCodeIdAutomatic,
            IsBridgingBillingBpjs,
            IsBridgingBillingBpjsWithCostSharing,
            IsConsignmentReceivedItemBySupplier,

            IsPrescriptionIprMustAssessmentFirst,
            IsPrescriptionOprMustAssessmentFirst,
            IsPrescriptionEmrMustAssessmentFirst,

            IsExamOrderIprMustAssessmentFirst,
            IsExamOrderOprMustAssessmentFirst,
            IsExamOrderEmrMustAssessmentFirst,

            IsAllowCopyPrescOther,
            DefaultGuarantorKiosk,
            RegistrationTypeOuterEtiquettePrintRestrictions,
            IsAllowSkipAutoBillOnRegistrationOpr,
            EmptyDoctorId,
            DoctorOnDutyId,
            AjaxCounter,
            IsAllowSubstituteDoctorOnRegistrationOpr,
            IsUsingRisPacsInterop,
            IsHideConfidentialLabResult,
            IsVisibleTrProcedureOnBookingRealization,
            IsUsingMappingServiceUnitProcedure,
            IsPrescOrderHandlingBasedOnDispensary,
            IsAllowDirectPrescOnInpatientSalesHandling,
            IsShowRegConsulOnVerificationBilling,
            IsVisibleAllAppointmentStatusOnList,
            IsNeedVoidReasonOnPaymentReceive,
            IsVisibleKwi,
            IsUsingDoubleEmployeeNo,
            ApplicationDocumentFolder,
            EmployeeDocumentFolder,
            TmpDocumentFolder,
            PerformancePlanDocumentFolder,
            SoundFolder,
            SoundFolderURL,
            IsShowScanDocumentConfirm,
            IsSeparateScheduleAndAttendanceSheet,
            IsShowPrintLabelOnTransEntry,
            AppProgramServiceUnitPatientLabel,
            IsPrescriptionShowStock,
            IsRecipeMarginValueForEachItemCompound,
            WebCamWidth,
            WebCamHeight,
            WebCamMaxWidth,
            WebCamMaxHeight,
            WebCamIdCardWidth,
            WebCamIdCardHeight,
            IsAutobillIprActivated,
            NonInPatientBpjsPlafond,
            IsThrIncludeInWageProcess,
            IsAllowExecutionDateForward,
            PrescriptionCategoryHomePresID,
            IsAutoChargeBedFilterLock,
            IsEklaimGroupUsingDefaultValue,
            IsAutoInsertRegistrationNoteFromRegistration,
            MedicationWillOutOfBalanceInDay,
            RisPacsInteropVendor,
            IsUsingValidationOnServiveUnitBooking,
            IsUsingBKUModule,
            IsAssetDepreciationCreateByAccounting,
            IsOpenAntrianBridging,
            IsDistributionOnlyBasedOnRequest,
            IsDistributionRequestOnlyForUnderMinValue,
            IsDistributionRequestMustNotExceedCWStock,
            ServiceUnitSanitationId,
            ServiceUnitPurchasingId,
            IsUsingSingleUnitIPSRS,
            WorkOrderRealizationAutoGenerateTx,
            IsUsingCentralizedPurchaseRequest,
            ItemUnitKg,
            IsRptInPreviewMode,
            IsNsOutcomeShowScale,
            EmployeeIncidentTypeNSI,
            EmployeeIncidentTypeEBF,
            NeedleTypeNSI,
            EmployeeStatusActive,
            EmployeeRelationshipSelf,
            EmployeeLeaveAnnualLeave,
            EmployeeAnnualLeaveStartPeriod,
            EmploymentTypePermanent,
            EmploymentTypeForAnnualLeave,
            PersonalLicenseTypeSTR,
            PersonalLicenseTypeSPK,
            IsAllowEditEmployeeAnnualLeaveEndPeriod,
            DeadlineMedicalRecordEditableAfterDischarge,
            DeadlineMedicalRecordAddableAfterDischarge,
            DefaultValueSpecimenTakenBy,
            IsUserTypeDoctorNoSaveConfirm,
            IsCentralizedCssd,
            IsShowSystemQtyInCssdStockTacking,
            IsShowSearchMenu,
            NsOutcome,
            NsOutcome02,
            NsIntervention,
            NsSymptom,
            NsIsShowDiagnosaCode,
            AppraisalVersionNo,
            IsGenericMustEqualZatActive,
            IsUsingFourLevelOrganizationUnit,
            IsFilterVehicleAndDriverOnScheduled,
            EmployeeTypeForLogbook,
            IsKioskEnableBPJS,
            IsKioskEnableQRCode,
            CssdSenderBySelf,
            CssdSenderByOtherUnit,
            CssdStockOpnameRowPerPage,
            IsUsingEmployeeNeedleStickInjuryFollowUp,
            IsAllowVoidServiceUnitBookingRealization,
            IsValidateEdOnDistribution,
            DayLimitValidationServiceUnitBooking,
            IsAutomaticChargeBedReprocessIncludeAutoBillItem,
            IsShowCrossMatchingPrintLabel,
            MaxLosToDisplayTransactionList,
            DiscountReasonSelisihKlaimBpjs,
            IsPatientBpjsNoMandatory,
            IsAccWriteDownPaymentOnReceivingInvoice,
            BudgetOfAssetNeedExtraApprovalLimit,
            IsParamedicFeePaymentEnableDraft,
            IsParamedicFeePaymentEnableGuaranteeFee,
            IsAllPhysicianAllowEditMedicalDischarge,
            SOPDirectoryUrl,
            IsUsingLimitQuotaInPhysicianSchedule,
            IsPathologyAnatomyDiagnoseFreeText,
            IsPathologyAnatomyLocationFreeText,
            IsPathologyAnatomyWithImpressionResult,
            IsPathologyAnatomyIhkWithMammaeResult,
            IsPathologyAnatomyWithTestResult,
            DayLimitDefaultDiagAndProcList,
            AppointmentGetListDateRangeLimit,
            IsAllowDoubleItemServiceOnTxEntry,
            IsVisiblePrintBillingPaymentPermit,
            JournalEntrySearchRangeFilter,
            IsAccountReceivableByDischargeDate,
            IsValidateMaxQtyItemConsumptions,
            IsEnabledDispensaryOnPrescriptionOrderRealization,
            IsUsingFactoryInTheItemProcurementProcess,
            IsVisibleOtc,
            IsVisibleGuarantorAutoBillItem,
            IsJobOrderRealizationListByOrderDate,
            IsInventoryIssueListByTransactionDate,
            IsARPaymentShowRemaining,
            IsClosingApAdvanceWithPayment,
            IsClosingApZeroWithPayment,
            IsUsingNewDuplicatePatientDataChecking,
            IsUsingUserAccessForEditPatient,
            IsSeparateLaboratoryUnit,
            IsAncillaryServicePhysicianSenderFreeText,
            IsApInvoiceIncPPN,
            IsRegistrationLinkToPatientDocument,
            IsUsingValidationImplementationDateTimeOnPpaNotes,
            IsAutoCreateNewPrescriptionTxOnUnapproval,
            IsUsingValidationOnServiceUnitBookingRealization,
            IsEmrListUsingExternalQueNo,
            HaisMonitoringProgramName,
            AppointmentTypeControlPlan,
            AppointmentTypeWebService,
            IsItemPickerListOrderByName,
            IsUsingProcurementTypeInPO,
            IsFilterPrescUddListOnlyWithValidTx,
            IsPathologyAnatomyResultTypeCanBeMoreThanOne,
            IsAllowEditPorAmountOnApInvoice,
            IsCanProcessExceededRequestOnInventoryIssueOut,
            IsUsingItemSubGroup,
            ExcelFileExtension,
            IsReadonlyStockQtyOnTransChargesItem,
            EmailSender,
            IsNeedVoidReasonOnArInvoicing,
            IsJobOrderRealizationListByCitoStatus,
            IsShowPrescriptionHistoryOnRegistration,
            IsUsingGoogleForm,
            IsVisibleGuarantorFilterOnPlafondInformationList,
            IsVisibleClinicalDiagnosisOnJobOrderRealization,
            IsVisible23PrescFilterOnPlafondInformationList,
            IsVisibleTemplateForDirectPrescription,
            IsMandatoryConsTime,
            IsUsingCheckListForMatrixServiceUnitItemService,
            acc_IsUsingBkuAccount,
            acc_IsCoaInvoiceGuarantorSplitIprOpr,
            acc_IsAccrualProrataRevenue,
            DiscountReasonBillRounding,
            IsPrescriptionDiscountAfterRounding,
            IsUsingBillingSlipInEnglish,
            IsFoodSelectedByMenuItemFoodGroup,
            MenuItemFoodGroupStandard,
            IsMandatoryDistributionTypeOnDirectDistribution,
            IsUsedPrintSlipLogForBillingStatement,
            IsUsedPrintSlipLogForPaymentReceipt,
            IsUsingAssetIdNewNumberingFormat,
            IsUsingAssetIdNumberingFormatWithSplitCategory,
            IsPorTaxBasedOnPo,
            IsUsingItemSubBin,
            IsParamedicFeeVerifPaymentFilterByClosingBilling,
            IsAllowEditDateTimeImplementation,
            IsShowRealizationOrderTransactionStatus,
            IsUsingValidationUserAccessOnPaymentReceive,
            IsUsingValidationPendingBalance,
            IsPrescriptionQueueForInpatient,
            IsJobOrderRealizationListWith2Tabs,
            IsShowInfoTotalPatientRegistration,
            IsDistributionRequestUsingPickFromUsedHistoryV2,
            IntervalRefreshPrescriptionOrderList,
            IsTestResultListWithDefaultOutstanding,
            IsRegistrationListWithCreatedDateTime,
            IsBillingStatementLosCalculationWithAdd1Day,
            IsBillingStatementRegDateUsingCheckinConfirmed,
            IsPrescriptionUnApprovalCreateNewNumber,
            IsDistributionRequestPickListWithBalanceToInfo,
            IsUsingDefaultConsumeMethodFor23DaysPrescription,
            IsPrescriptionSplitBillActived,
            IsSharePurchaseDiscToCustomer,
            IsLockLocationPharmacy,
            IsPatientOprOnPrescSalesForPolyclinicOnly,
            IsPorUsingChecklistItem,
            IsShowPrintLabel1InJobOrderRealizationList,
            IsShowBalanceInfoInDistributionRequest,
            GuarantorIdExeptionForRecipeAmount,
            IsMandatoryPrescriptionCategory,
            IsMandatoryDrugAllergen,
            DayLimitEmployeeLicenseWarning,
            IsFeeTaxBeforeDiscount,
            IsRasproEnable,
            RasproEnableForRegistrationTypes,
            QuestionFormEmployeeSafetyCultureIncidentReports,
            QuestionFormPatientIdentificationCompliance,
            QuestionFormCredentialing,
            acc_IsJournalAssets,
            AntibioticMaxConsumeDay,
            acc_JournalAssetsAmount,
            acc_AssetInventoryAmountLimit,
            acc_EconomicLifeInYearLimit,
            IsVoucherListShowVoid,
            IsServiceUnitBookingUsingBodyDiagramServiceUnit,
            IsAutoDeleteBalanceOnInActiveItem,
            IsUddSetupMustReconFirst,
            acc_AssetDepreciationAmountLimit,
            IsUseApprovalLevelforPOWithUserRestriction,
            PrefixOnoSysmexInterop,
            IsItemPickerListOrderUsingGroupButton,
            IsDefaultEmptyDateOnEKlaimList,
            IsCentralizedLaundrie,
            IsMandatoryInterventionReason,
            IsCssdExpiredValidateInReceiveDetail,
            IsCssdUsingDttTerm,
            IsCssdStockValidateInDistribution,
            IsPromoPackageActivated,
            IsPersonalWorkExperienceUsingDatePeriod,
            IsAllowSanitationWasteBalanceMinus,
            PersonalContactTypeEmail,
            acc_IsAutoApprovedTransaction,
            IsShowArReceiptInVerificationAndPaymentList,
            IsSaveHistoryInImportBpjsVerification,
            IsVerificationBillingAuthorizationActivated,
            MaximumQtyBloodBagRequestPassedCasemix,
            PurchaseRequestOutstandingListOrderBy,
            IsDistributionOnlyInStock,
            IsAutoInsertToEmployeePeriodicSalaryOvertime,
            KPI_IsShowDenum,
            IsCrmMembershipActive,
            IsCredentialingWithPrerequisite,
            IsCompetencyAssessmentUsingSingleEvaluator,
            EmploymentTypeCi,
            EmployeeProfessionGroupMedical,
            EmployeeProfessionGroupNursing,
            EmployeeProfessionGroupKtkl,
            EmployeeStatusInActive,
            IsValidateEmployeeLeaveWithPayCutCantCrossMonth,
            EmrHistoryRegistrationCount,
            IsPrescOrderLocUseMainLoc,
            IntNotesVerifLabel,
            IntNotesVerifLabelReview,
            acc_IsPackageRevenueOnMainPackage,
            acc_CoaPackageDiff,
            acc_CoaPackageDiffMin,
            IsAllowEditAssetGroup,
            IsCustomPivotFilterByUser,
            TriageStdRefId,
            ESignUrlRoot,
            ESignUserId,
            ESignPassword,
            IsPatientTransferUsingFilterToClass,
            DiscountProrataToRevenueDateStart,
            RefDischargeConditionForPresentStatus,
            RemunBudgedPercentage,
            ServiceUnitIdIgdForRemun,
            IsOverpaymentProrataToRevenue,
            CranialisStdRefId,
            IsBloodDistributionReceivedByCombobox,
            IsAplicaresByRoomName,
            IsAutoRecruitmentPlanName,
            IsDisableClassOnRequestChangeItemProduct,
            acc_PatientReceivableDateStart,
            IsFamilyOrPatientSignature,
            IsAllPhysicianOnSbar,
            IsAutoKioskQueueStatusSkippedForPrescription,
            IsSignMandatoryOnOperatingNotes,
            IsMandatoryEpisodeProcedureOnOperatingNotes,
            QueueDisplayScrollingText,
            QueueDisplayScrollingDurationText,
            QueueDisplaySloganHealthcare,
            IsUsingAllICD10,
            IsRL5354IncludeICD10O,
            IsSeparatedRounding,
            IsUsingRoundingPaymentAR,
            SatuSehatBridgingTypeID,
            SatuSehatClientID,
            SatuSehatClientSecretKey,
            SatuSehatConsentUrl,
            SatuSehatOrganizationID,
            SatuSehatAuthUrl,
            SatuSehatBaseUrl,

            GoogleClientID,
            GoogleProjectID,
            GoogleClientSecret,
            GoogleEmpRecruitmentSpreadsheetID,
            GoogleEmpRecruitmentUser,
            GoogleEmpRecruitmentAppName,
            IsCheckallDistributedPrint,
            IsUsingRoundingPaymentAP,
            IsHandHygieneNoteNoValidation,
            IsEnabledEmployeeRecruitmentGoogleForm,
            IsUsingParamedicFeeByTeam,
            IsUsingGuarantorPrefixForQueueCodeKioskV2,
            IsShowIcd9cmInProgressNoteEntry,
            IsValidateParamedicSBAR,
            GuarantorDocumentFileNameFormula,
            IsGuarantorDocumentPathUseFormula,
            GuarantorDocumentPathFormula,
            IsEmrIcdXTextReadOnly,
            IsAllowEditPatientFromVerificationBilling,
            ServiceUnitIDForPaymentReceivedARBankCostSubledger,
            IsUsingBankCostSubledgerForPaymentReceivedAR,
            IsUsingQueueCodeByPhysicianKioskV2,
            ItemGroupIDMedicationResume,
            IsNewdetailResultPapSmear,
            IsEnabledAddNewItemCSSD,
            IsUsingItemConsAndExpFactorOnJORealizationList,
            isUsingDefultGcs,
            IsNewBudgetingAutomatisItemMasterProduct,
            IsUsingKioskQueNoFormat,
            IsAntrolCreateRegistrationQueue,
            AssessmentTypeIDsForShowPanelFdolm,
            IsUsingSplitPainScaleAndFlaccBasedOnAge,
            SplitPainScaleAndFlaccAgeValue,
            IsEmrHideDivPeEntryOnAssessmentEntry,
            RptTableToImageReducePctg,
            PrescriptionDisplayColumnsDef,
            MaxChronicDrugPrescriptionInDays,
            IsSoapFromPysicalExamIncludeNormalValue,
            Is23DaysPrescriptionUseChronicGuarantor,
            ValueForTakingQueueBeforeStartTime,
            DischargeMethodDefaultOpr,
            DischargeMethodDefaultEmr,
            IsSignMandatoryOnAssessmentEntry,
            IsShowCurb65ScoreInAssesmentAndMDS,
            IsUsingNumberingSettingAppointmentNoWebService,
            IsMultipleSynonymValueForDiagnoseAndProcedure,
            TablePatientBirthRecordFieldValidation,
            IsUsingMultipleScoringSupervisor,
            IsAssessmentAutoSaveMds,
            IsAssessmentAutoSaveMdsCasemixWithGuarantorDoc,
            IsBillingEmrAddButtonEnabled,
            IsParamedicAbsentByIMMADokter,
            IsAllowEditDiagnosisOnEpisodeProcedureEMR,
            IsNewDisplayCasemixCenter,
            IsNotesforCompoundPresc,
            IsiDRGIntegration,
            IsUseCurrentDateRegistration
        }

        public class DefaultRecordValue
        {
            // Urutan --> [ParameterName] | [ParameterValue]  | [ParameterType]  | [IsUsedBySystem]
            public const string IsSatuSehatDirectSend = "Is SatuSehat Direct Send (Yes/No)?|No| |false";
            public const string IsEmrChiefComplaintTextRequired = "Is ChiefComplaint Mandatory in assessment entry (Yes/No)?|No| |false";
            public const string IsAutoSaveMdsDpjpToSepFolderAfterSave = "Is Auto Save MDS DPJP To SEP Folder After Save (Yes/No)?|Yes| |false";
            public const string IsAutoSaveOtherExamResultToDocFolderAfterSave = "Is Auto Save Other Exam Result To Doc Folder After Save (Yes/No)?|No;RSI:Yes| |false";
            public const string PartographColCount = "Partograph Column Count (max 46)|46| |false";
            public const string IsAssessmentFamilyOrPatientSign = "Is Family Or Patient Sign in Assessment Visible (Yes/No)?|No| |false";
            public const string IsAssessmentPhysicianSign = "Is Physician Sign in Assessment Visible (Yes/No)?|Yes| |false";
            public const string IsMedicalNoContainStrip = "Is MedicalNo Contain Strip (Yes/No)?|No| |false";
            public const string IsEmrIcdXListMandatory = "Is Emr ICD X List Mandatory (Yes/No)?|No| |false";
            public const string IsEmrIcdXTextReadOnly = "Is Emr ICD X Text ReadOnly (Yes/No)?|No| |false";
            public const string GuarantorDocumentFileNameFormula = "Guarantor Document File Name Formula ?|BPJSSEPNO(R6)+DG+_DI+_TN| |false";

            public const string IsGuarantorDocumentPathUseFormula = "Is Guarantor Document Path And File Name Use Formula (Yes/No)?|No| |false";
            public const string GuarantorDocumentPathFormula = "Guarantor Document Path Formula ?|MM+YY>RT>DD| |false";

            public const string IsShowIcd9cmInProgressNoteEntry = "Is Show ICD 9CM In Progress Note Entry (Yes/No)?|No;RSSTJ:Yes| |false";
            public const string IsEmrAssementDiagnoseTextVisible = "Is Emr Assement Diagnose Text Entry Visible (Yes/No)?|Yes| |false";
            public const string IsEmrAssementDiffDiagnoseTextVisible = "Is Emr Assement Different Diagnose Text Entry Visible (Yes/No)?|Yes| |false";
            public const string IsEmrContinueAssementSubjectivetVisible = "Is Emr Continue Assement Subjectivet Text Entry Visible (Yes/No)?|Yes| |false";
            public const string IsEmrContinueAssementAssessmentVisible = "Is Emr Continue Assement Assessment Text Entry Visible (Yes/No)?|Yes| |false";

            public const string GoogleEmpRecruitmentSpreadsheetID = "Employee Recruitment Google Form SpreadsheetId|1nzo7WwgW8wibm1DymiMs0eruOx2uByM4ZMc-Xt58FMo||False";
            public const string GoogleEmpRecruitmentUser = "Employee Recruitment Google Form User|employee-recruitment@avicennahis.iam.gserviceaccount.com||False";
            public const string GoogleEmpRecruitmentAppName = "Employee Recruitment Google Form AppName|AvicennaHIS||False";

            public const string GoogleClientID = "Google Auth ClientID|969227508221-3d97qhdnvnjd07r0pjituvsoun2ceff7.apps.googleusercontent.com||False";
            public const string GoogleProjectID = "Google Auth ProjectID|avicennahis||False";
            public const string GoogleClientSecret = "Google Auth Client Secret|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||False";
            public const string IsEnabledEmployeeRecruitmentGoogleForm = "Is Enabled Employee Recruitment Import from Google Form (Yes/No)?|No| |false";

            public const string SatuSehatBridgingTypeID = "SatuSehat BridgingType ID|BridgingType-???| |False";
            public const string acc_CoaRevenueTopLevel = "Coa Revenue Top Level|;RSSMCB:X4.00.00.00.00 | |False";
            public const string acc_IsInventoryBySrRegServiceUnit = "Is Inventory By SR Registration of Service Unit|No;RSISB:Yes| |False";
            public const string IsEmrPhysicianAssessmentMandatory = "Is Physician Assessment Mandatory (Yes/No)|No;RSYS:Yes;| |false";
            public const string IsIwlUsingTemperature = "Is Iwl Using Temperature Formula (Yes/No)|Yes;RSUTAMA:No;| |false";
            public const string IsShowOtherParamedicBillingAtEmr = "Is Show Other Paramedic Billing At EMR (Yes/No)|Yes;RSRM:No;| |false";
            public const string IsAutoApprovedServiceUnitTxFromEmr = "Is Auto Approved Save Service Unit Transaction From EMR (Yes/No)|No| |false";
            public const string IsAutoApprovedExamOrderFromEmr = "Is Auto Approved Save Exam Order From EMR (Yes/No)|No| |false";
            public const string AutoSaveInterval = "Auto Save Interval in minute (ex: 1/2, 3/4, 1 etc)|0;RSTJ:1/2;| |false";
            public const string ParamedicFirstTransChargesItemIds = "Paramedic First TransactionCharges ItemID (use sep ;)|||false";
            public const string ESignUrlRoot = "ESign Web Service Url Root|http://10.15.37.39| |false";
            public const string ESignUserId = "ESign UserId|esign| |false";
            public const string ESignPassword = "ESign Password|qwerty| |false";
            public const string TriageStdRefId = "Triage StandardReferenceID|Triage| |false";
            public const string IsPrescOrderLocUseMainLoc = "Is Prescription Order Location Use Main Location (Yes/No)?|Yes| |false";
            public const string EmrHistoryRegistrationCount = "Emr Non Inpatient show history registration count|15| |false";
            public const string IsOtcFreeRecipeMargin = "Is OTC Sales Free Recipe Margin (Yes/No)?|No;RSUTAMA:Yes;KLUTAMA:Yes| |false";
            public const string acc_IsAutoJournalSales = "Is Auto Journal Sales To Customer (Yes/No)?|Yes| |false";
            public const string acc_IsAutoJournalCashSalesToCustomer = "Is Auto Journal Cash Sales To Customer (Yes/No)?|Yes| |false";

            public const string DriveThruGoogleSpreadsheetId = "Drive Thru Registration Google Spreadsheet Id|| |false";
            public const string DriveThruServiceUnitID = "Drive Thru ServiceUnitID ([Test Caption]:[ServiceUnitID],) ?|PCR:DTPCR,Swab Antigen:DTSWB,Rapid Test Antibody:DTRPD| |False";
            public const string DriveThruAutoBillItemID = "Drive Thru AutoBill ItemID ([Test Caption]:[ItemID],)?|PCR:PCR-004,Swab Antigen:R01005,Rapid Test Antibody:R01004| |False";
            public const string DriveThruGoogleUser = "Drive Thru GoogleWebAuthorization User|;RSUTAMA:utamahospital@gmail.com||false";
            public const string DriveThruGoogleAppName = "DriveThru Google Application Name|AvicennaDriveThru||false";

            public const string IsMedicalDischargeSummaryDefaultValue = "Is Medical Discharge Summary Default Value From History (Yes/No) ?|Yes| |False";
            public const string IsMedicalDischargeSummaryPrescDefaultValue = "Is Medical Discharge Summary Default Value for Prescription From History (Yes/No) ?|Yes;RSPM:No| |False";
            public const string IsMedicalDischargeSummaryHomPrescAll = "Is Medical Discharge Summary Home Prescription from all Medication  (Yes/No) ?|Yes| |False";
            public const string IsMedicalDischargeSummaryPrescJustItemName = "Is Medical Discharge Summary Presc Hist Just ItemName  (Yes/No) ?|No;RSYS:Yes| |False";

            public const string PatientDocumentScanCompression = "Patient Document Scan Compression|200| |False";
            public const string IsLogProgramAccess = "Is Log Program Access|No| |True";
            public const string vs_HeartRateID = "VitalSignID  for Heart Rate|HEART| |False";
            public const string vs_SystolicID = "VitalSignID  for Blood Pressure - Systolic|BP2| |False";
            public const string vs_DiastolicID = "VitalSignID  for Blood Pressure - Diastolic|BP1| |False";
            public const string vs_RespiratoryID = "VitalSignID  for Respiratory|RESP| |False";
            public const string vs_TemperatureID = "VitalSignID  for Temperature|TEMP| |False";
            public const string acc_IsAutoApprovedConsReceived = "Setting for Consignment Received AUTO Approved (Yes/No)|Yes| |False";
            public const string acc_IsAutoApprovedConsReturned = "Setting for Consignment Returned AUTO Approved (Yes/No)|Yes| |False";
            public const string acc_IsAutoApprovedInventoryIssue = "Setting for Inventory Issue AUTO Approved (Yes/No)|No| |False";
            public const string acc_IsAutoApprovedInventoryDistribution = "Setting for Inventory Distribution AUTO Approved (Yes/No)|Yes| |False";
            public const string acc_IsAutoApprovedPayment = "Setting for Payment AUTO Approved (Yes/No)|No| |False";
            public const string acc_IsAutoApprovedPaymentBackOffice = "Setting for Back Office Payment AUTO Approved (Yes/No)|Yes;RSMM:No| |False";
            public const string acc_IsAutoApprovedPOReceived = "Setting for PO Received AUTO Approved (Yes/No)|No| |False";
            public const string acc_IsAutoJournalAPPayment = "Setting for AP Payment Auto Journal (Yes/No)|Yes| |False";
            public const string acc_IsAutoJournalARPayment = "Setting for AR Payment Auto Journal (Yes/No)|Yes| |False";
            public const string acc_IsAutoJournalCashPOReceived = "Setting for PO Receiceved Cash Type Journal|Yes| |False";
            public const string acc_IsAutoJournalConsInvoicing = "Setting for Consignment Invoicing Auto Journal (Yes/No)|Yes| |False";
            public const string acc_IsAutoJournalConsReceived = "Setting for Consignment Received Auto Journal (Yes/No)|Yes| |False";
            public const string acc_IsAutoJournalConsReturned = "Setting for Consignment Returned Auto Journal (Yes/No)|Yes| |False";
            public const string acc_IsAutoJournalFeePayment = "Setting Paramedic Fee Payment Auto Journal(Yes/No)|Yes| |False";
            public const string acc_IsAutoJournalFeeVerification = "Setting Paramedic Fee Verif Auto Journal (Yes/No)|Yes| |False";
            public const string acc_IsAutoJournalFinalizeBilling = "Setting For Journal Finalize Billing(Yes/No)|No| |False";
            public const string acc_IsAutoJournalHPP = "Setting for HPP Auto Journal(Yes/No)|Yes| |False";
            public const string acc_IsAutoJournalInvIssue = "Setting for Inventory Issue Auto Journal (Yes/No)|Yes| |False";
            public const string acc_IsAutoJournalPhysicianFeeBeforeVerification = "Setting for Physician Service Fee AUTO Journal Before Verification|Yes| |False";
            public const string acc_IsAutoJournalPOReceived = "Setting for PO Received Auto Journal (Yes/No)|Yes| |False";
            public const string acc_IsAutoJournalSalesDiscount = "Setting for Service Unit Transaction & Pharmacy Sales Discount Auto Journal(Yes/No)|No| |False";
            public const string acc_IsAutoJournalSalesToBranch = "Setting for Sales To Branch Auto Journal (Yes/No)|Yes| |False";
            public const string acc_IsAutoJournalPayroll = "Setting for Payroll Auto Journal (Yes/No)|Yes;RSMP:No;| |False";
            public const string acc_IsAutoCashEntryOnAssetAuction = "Is Auto Insert Cash Entry On Asset Auction? (Yes/No)|No| |True";
            public const string acc_IsAutoCashEntryOnPayroll = "Is Auto Insert Cash Entry On Payroll? (Yes/No)|No| |True";
            public const string acc_IsJournalPayrollWithDefaultNormalBalance = "Is Journal Payroll With Default Normal Balance (Yes/No)|Yes;RSMM:No;| |True";
            public const string acc_IsJournalPayrollWithDirectIndirectCost = "Is Journal Payroll With Direct / Indirect Cost (Yes/No)|No;YBRSGKP:Yes;| |True";
            public const string acc_IsJournalCashBased = "Is Journal Cash Based? (Yes/No)|Yes| |False";
            public const string acc_IsEnableGuarDiscProrataToRevenue = "Is Enable Journal / Excess Prorata to Revenue? (Yes/No)|No;YBRSGKP:Yes| |False";
            public const string acc_IsEnableParamedicPayable = "Is Enable Journal Accrual Paramedic Payable? (Yes/No)|No;RSKS:Yes| |False";
            public const string acc_IsJournalAccualNoTemporary = "Is Journal Accrual using No Temporary? (Yes/No)|Yes;GRHA:No| |False";
            public const string acc_IsUnitBasedInventoryIssueCost = "Setting for Inventory Cost by Unit Location (Yes/No)|No||False";
            public const string acc_IsUnitBasedProductAccount = "Setting for Unit Based product Account (Yes/No)|No| |False";
            public const string acc_IsJournalPackageB4Approve = "Setting for Journal Package before approve detail (Yes/No)|No;GRHA:Yes;| |False";
            public const string acc_IsUsingBkuAccount = "Is Using BKU Account? (Yes/No)|No;RSTJ:Yes| |True";
            public const string acc_IsCoaInvoiceGuarantorSplitIprOpr = "Is Coa Invoice Guarantor Split Ipr Opr? (Yes/No)|No;RSISB:Yes| |False";
            public const string acc_IsAccrualProrataRevenue = "Is Accrual Prorata Revenue? (Yes/No)|No;RSKS:Yes| |False";
            public const string acc_IsAutoJournalAssetDestruction = "Is Auto Journal Asset Destruction ? (Yes/No)|Yes| |False";
            public const string acc_IsAutoJournalAssetAuction = "Is Auto Journal Asset Auction? (Yes/No)|Yes| |False";
            public const string acc_IsAutoJournalAssetMovement = "Is Auto Journal Asset Movement? (Yes/No)|Yes| |False";
            public const string acc_IsAutoApprovedAssetDestruction = "Is Auto Approved Asset Destruction? (Yes/No)|Yes| |False";
            public const string acc_IsAutoApprovedAssetAuction = "Is Auto Approved Asset Auction? (Yes/No)|Yes| |False";
            public const string acc_IsAutoApprovedAssetMovement = "Is Auto Approved Asset Movement? (Yes/No)|Yes| |False";
            public const string AdminRad = "Administrasi Radiologi|-| |True";
            public const string Apoteker = "Nama Apoteker|APOTEKER : .....| |False";
            public const string ApotekerNo = "Izin Apoteker|.............................| |False";
            public const string AppointmentStatusCancel = "Appointment Status : Cancel|AppoinmentStatus-003| |False";
            public const string AppointmentStatusClosed = "Appointment Status : Closed|AppoinmentStatus-004| |False";
            public const string AppointmentStatusConfirmed = "Appointment Status : Confirmed|AppoinmentStatus-002| |False";
            public const string AppointmentStatusOpen = "Appointment Status : Open|AppoinmentStatus-001| |False";
            public const string AppointmentStatusNoResponse = "Appointment Status : No Response|| |False";
            public const string AppointmentStatusBooked = "Appointment Status : Booked|06| |False";
            public const string BankAccNameForSlipBank = "Bank Account Name For Slip Bank|PT. Surya Mitra Insani| |False";
            public const string BankAccNameForSlipBankKalbar = "Bank Account Name For Slip Bank|PT. Surya Mitra Insani| |False";
            public const string BankAccNoForSlipBank = "Bank Account No. For Slip Bank|8670025200| |False";
            public const string BankAccNoForSlipBankKalbar = "Bank Account No. For Slip Bank Kalbar|155-00-9977800-6| |False";
            public const string BankCabangSlipBank = "Bank Cabang Slip Bank|Kantor Cabang Pembantu Cipondoh| |False";
            public const string BankCabangSlipBank2 = "Bank Cabang Slip Bank 2|Kantor Cabang Kisamaun Tangerang| |False";
            public const string BankCashierID = "Bank ID for Cashier|K003| |False";
            public const string BankNameForSlipBank = "Bank Name For Slip Bank|Bank Central Asia| |False";
            public const string BankNameForSlipBankKalbar = "Bank Name For Slip Bank Kalbar|Bank Mandiri| |False";
            public const string BedStatusBooked = "Bed Status : Booked|BedStatus-03| |False";
            public const string BedStatusGoToOperatingRoom = "Bed Status : Go To Operating Room|| |False";
            public const string BedStatusOccupied = "Bed Status : Occupied|BedStatus-02| |False";
            public const string BedStatusPending = "Bed Status : Pending|BedStatus-04| |False";
            public const string BedStatusUnoccupied = "Bed Status : Ready|BedStatus-01| |False";
            public const string BedStatusCleaning = "Bed Status : Cleaning|BedStatus-05| |False";
            public const string BedStatusReserved = "Bed Status : Reserved|BedStatus-06| |False";
            public const string BedStatusRepaired = "Bed Status : Repaired|BedStatus-07| |False";
            public const string BillingGroupAdministrativeCostsID = "BillingGroup ID for Administrative Costs|999| |False";
            public const string BillingGroupPharmacyID = "BillingGroup ID for Pharmacy|017| |False";
            public const string BillingGroupTariffRoom = "Billing Group For Tariff Room|AD| |True";
            public const string BlenderizedFoodId = "Blenderized Food ID|| |False";
            public const string DefaultMenuStandard = "Default Menu ID Standard for Extra Meal Order|| |False";
            public const string BusinessMethodAllIn = "Business Method : All In|BusinessMethod-003| |False";
            public const string BusinessMethodBpjs = "Business Method : BPJS|BusinessMethod-004| |True";
            public const string BusinessMethodCoverage = "Business Method : Coverage|BusinessMethod-001| |False";
            public const string BusinessMethodFlavon = "Business Method : Plafond|BusinessMethod-002| |False";
            public const string ChargeBedExecutionTime = "Charge BedExecution Time : format (HH:mm)|14:00||True";
            public const string ClosingJournalWithoutAllApproved = "Setting for ClosingJournal Without All Approved|1| |False";
            public const string ClusterPatientClassID = "Class ID for Cluster Patient|none| |False";
            public const string ClusterPatientDepartmentID = "Department ID for Cluster Patient|D01| |False";
            public const string coa_AccrualIncome = "Chart Of Account for Income (Acrrual)|1.01.04.03.01| |False";
            public const string coa_BankCost = "COA for Bank Cost|5.88.01.01.02| |False";
            public const string coa_CompIDForBillingAdm = "Component Tariff for Billing Total Adm Fee|01| |False";
            public const string coa_ContemporaryPayment = "COA Contemporary Payment|1.01.04.03.02  | |False";
            public const string coa_ContemporaryReceivable = "COA Piutang Temporary|1.01.03.03.01| |False";
            public const string coa_DirectPrescReturn = "Chart Of Account for Direct Prescription Return|| |False";
            public const string coa_DownPayment = "Chart Of Account forDown Payment Receive|2.01.05.01.02| |False";
            public const string coa_DownPaymentReturn = "Chart Of Account for Down Payment Balance|1.01.01.01.99  | |False";
            public const string coa_Gain = "coa Laba tahun Lalu|3.03.01.01.01| |False";
            public const string coa_IncomeSummary = "Coa Income Summary|3.03.03.01.01| |False";
            public const string coa_inventory_loss = "COA Inventory Loss|||False";
            public const string coa_inventory_profit = "COA Inventory Profit|||False";
            public const string coa_ItemIdBillingTotalAdmFee = "Item Id for Billing Adm Fee (Linking to COA)|AA02.001| |False";
            public const string coa_Paket = "Chart Of Account for Subsidi Paket|| |False";
            public const string coa_PaymentRoundingAmount = "COA for Payment Rounding Amount|5.01.10.01.05||False";
            public const string coa_PhycisianTax = "COA for PPH21 of Phycisian|| |False";
            public const string coa_PhysicianDebt = "Hutang Honor Dokter|2.01.03.02.01| |False";
            public const string coa_PhysicianPayment = "Hutang Honor Dokter RS|2.01.03.02.01| |False";
            public const string coa_PhysicianPaymentRef = "Hutang Honor Dokter Mitra|2.01.03.02.02| |False";
            public const string coa_PhysicianPaymentVerification = "COA for Physician payment Verification|6.02.01.01.01||False";
            public const string coa_Pph22 = "Chart Of Account for PPH 22|2.01.03.01.02  | |False";
            public const string coa_Pph23 = "Chart Of Account for PPH 23|2.01.03.01.03  | |False";
            public const string coa_Ppn = "Chart Of Account for PPN|1.01.08.02.01  | |False";
            public const string coa_PpnBahanMedis = "Chart Of Account for PPN Bahan Medis|1.01.08.02.01  ||False";
            public const string coa_PpnNonMedic = "Chart Of Account for PPN Non Medic|1.01.08.02.02  | |False";
            public const string coa_PurchaseCash = "COA for PO Type Cash|2.01.03.03.01  | |True";
            public const string coa_PurchaseDiscount = "COA Purchase Discount|| |False";
            public const string coa_ReceiveableDiscount = "COA for Receivable Discount|4.99.01.01.03  | |False";
            public const string coa_RetainedEarning = "Coa Retained Earning|3.03.02.01.01| |False";
            public const string coa_StampAmountPOR = "COA Stamp Amount (Biaya Materai dari POR)|||False";
            public const string COAPendapatanUangRacikan = "COA Pendapatan Uang Racikan|||False";
            public const string CoaUsingClass = "Chart Of Account Using Class|0| |False";
            public const string CompleteStatusRM = "Medical Record Status : Complete|1| |False";
            public const string ConnectToMpi = "Is Connect To Mpi ?|no| |False";
            public const string CurrencyRupiahID = "Currency ID for Rupiah|IDR| |False";
            public const string DefaultChecklistArPayment = "Default Checklist AR Payment Picklist (1 = true; 0 = false)|1| |True";
            public const string DefaultClassMenuStandard = "Default Class for Menu Standard (for Additional Meal Order)|A1| |False";
            public const string DefaultDownPaymentType = "Default Value for Down Payment Type (Re-Order Purchase)|DownPaymentType-000| |False";
            public const string DefaultPurchaseOrderType = "Default Value for Purchase Order Type (Re-Order Purchase)|CR| |False";
            public const string DefaultSalaryTemplateID = "Default Salary Template ID (Default: -1)|0||False";
            public const string DefaultSurgeryTime = "Default Value for Surgery Time|30| |True";
            public const string DefaultTariffClass = "Default Value for Tariff Class|Z1| |False";
            public const string DefaultTariffType = "Default Value for Tariff Type|TariffType-001| |False";
            public const string DefaultTerm = "Default Value for Term (Re-Order Purchase)|1M| |False";
            public const string DefektaSupplierID = "Defekta Supplier ID|| |False";
            public const string DependentIncludeEmployee = "Included in Employee Medical Benefit Limit|1| |True";
            public const string DependentsOfEmployeesGuarantorID = "Guarantor ID for Dependents Of Employees|C197,C281,C322| |False";
            public const string DiagnoseTypeMain = "Diagnose Type : Main|DiagnoseType-001| |False";
            public const string DiagnoseTypeInitial = "Diagnose Type : Initial|DiagnoseType-006| |False";
            public const string DiagnoseTypeDeathDiagnosis = "Diagnose Type : Death Diagnosis|| |False";
            public const string Director = "Director|| |False";
            public const string DischargeConditionDie = "Discharge Condition: Die|E05| |True";
            public const string DischargeConditionDieLessThen48 = "Discharge Condition: Die Less Then 48 Hours|I04| |True";
            public const string DischargeConditionDieMoreThen48 = "Discharge Condition: Die More Then 48 Hours|I05| |True";
            public const string DiscountValueType = "Value Type for Discount|ValueType-001| |False";
            public const string DocFileAnalysisForRegEMR = "File Analysis Of Document List For Registration of Emergency|03| |False";
            public const string DocFileAnalysisForRegIPR = "File Analysis Of Document List For Registration of Inpatient|02| |False";
            public const string DocFileAnalysisForRegOPR = "File Analysis Of Document List For Registration of Outpatient|01| |False";
            public const string DpAmtClassI = "Down Payment Amount For Class I|2500000| |False";
            public const string DpAmtClassIcu = "Down Payment Amount for ICU|7000000| |False";
            public const string DpAmtClassII = "Down Payment Amount For Class II|2000000| |False";
            public const string DpAmtClassIII = "Down Payment Amount For Class III|800000| |False";
            public const string DpAmtClassVip = "Down Payment Amount For Class VIP & SVIP|8000000| |False";
            public const string DrugAllergenGroupID = "Group ID for Drug Allergen |DrugAllergen| |False";
            public const string FoodAllergenGroupID = "Group ID for Food Allergen |FoodAllergen| |False";
            public const string EmergencyDepartmentID = "Department ID for Emergency|D01| |False";
            public const string EmergencyPatientClassID = "Class ID for Emergency Patient|Z1| |False";
            public const string EMRHead = "Person In Charge of EMR|-| |True";
            public const string FinanceFaxNoDirect = "Finance Fax No Direct|0561-742242| |False";
            public const string FinanceHead = "Person In Charge of Finance Head|Timbul Napitupulu| |True";
            public const string FinanceHeadDirector = "Person In Charge of Finance Director|Carie ....| |True";
            public const string FinanceHeadID = "Finance Head ID|none| |False";
            public const string FinanceHeadJob = "FinanceHeadJob|Kepala Keuangan| |False";
            public const string FinanceHeadJob2 = "FinanceHeadJob 2|Accounting,Tax & IT Senior Mgr| |False";
            public const string GuarantorAskesID = "Guarantor ID for ASKES|| |True";
            public const string GuarantorEmployeeID = "Guarantor ID for Employee|GRSUI| |True";
            public const string GuarantorRuleTypeDiscount = "Guarantor Rule Type : Discount|DISC| |False";
            public const string GuarantorRuleTypeMargin = "Guarantor Rule Type : Margin|MRG| |False";
            public const string GuarantorRuleTypePlavon = "Guarantor Rule Type : Plafond|FLA| |False";
            public const string GuarantorTypeDiscount = "Guarantor Type ID for Discount|08| |False";
            public const string GuarantorTypeEmployee = "Guarantor Type for Employee|07| |True";
            public const string GuarantorTypeInsurance = "Guarantor Type ID for Insurance|01| |False";
            public const string GuarantorTypeMemberID = "Guarantor Type ID for Member|08| |False";
            public const string GuarantorTypeSelf = "Guarantor Type for Self|00| |True";
            public const string HeadOfRadiology = "Person In Charge of Radiology Head|dr. F.X. Purwasi, SpRad.| |True";
            public const string HealthcareFinanceEmailAddr = "Healthcare Finance Email Address|penagihan_rsui@yahoo.com| |False";
            public const string HealthcareID = "Healthcare ID|001| |True";
            public const string HealthcareInitial = "Healthcare Initial|RSUI| |True";
            public const string HealthcareLicenseNumber = "Healthcare license Number|....................| |False";
            public const string HealthcareMarketingEmailAddr = "Healthcare Marketing Email Address|....................| |False";
            public const string HealtScreeningServiceUnitID = "HealtScreeningServiceUnitID|A08||False";
            public const string HisInteropConfigName = "His Interop Config Name|RSUI_LIS_INTEROP||True";
            public const string HRDHead = "HRD Head|| |False";
            public const string HRDKabag = "HRD - KABAG|.....................| |False";
            public const string HRDKasubbagPembinaan = "HRD - KASUBBAG Pembinaan|.....................| |False";
            public const string HumanResourceUserID = "Human Resource Auth User ID (comma separated, default : string empty)|||True";
            public const string IdFormAntrian = "Id form di cetakan Antrian|FRM-ADM-002 REV:002| |False";
            public const string InPatientDepartmentID = "Department ID for Inpatient|D03| |False";
            public const string InPatientServiceUnitID = "Service Unit ID for Inpatient |D03| |False";
            public const string IntervalPatientLastVisit = "Interval Patient Last Visit for Show Registration Message (in Year)|3| |False";
            public const string InventoryHeadOfficer = "Inventory Head Officer|-| |False";
            public const string InventoryPhaHeadOfficer = "Inventory Pharmacy Head Officer|| |False";
            public const string InvoicePaymentCash = "Invoice Payment for Cash|PaymentMethod-001| |False";
            public const string InvoiceTermOfPayment = "Default Value for Invoice Term Of Payment|15| |False";
            public const string IsAdminCalcBeforeDiscount = "Is Admin Calculation Before Discount? (Yes / No)|Yes| |True";
            public const string IsAdminCalcIncludeItemProduct = "Is Admin Calculation Include Item Product? (Yes / No)|Yes| |True";
            public const string IsAllowDiscountInvoice = "Is Allow Discount Invoice (Yes/No)|No| |True";
            public const string IsAllowEditPorDate = "Is Allow Edit POR Date? (Yes / No)|Yes| |True";
            public const string IsAllowEditProcedureChargeClass = "Is Allow Edit Procedure Charge Class (Billing Verification Form)? (Yes / No)|Yes| |True";
            public const string IsAllowGuarantorDepositBalanceMinus = "Is Allow Guarantor Deposit Balance Minus? (Yes/No)|No| |False";
            public const string IsAllowMultipleRegOp = "Is Allow Multiple Registration Outpatient?|Yes| |True";
            public const string IsAllowMultipleRegOpWithSameUnitAndPhysician = "Is Allow Multiple Registration Outpatient With Same Unit And Physician? (Yes/No)|No| |False";
            public const string IsAutoClosedDistRequest = "Is Auto Closed Dist Request After Distribution? (Yes / No)|No| |True";
            public const string IsAutoClosedRegIpOnPayment = "Is Auto Closed Reg-IP On Payment ? (Yes or No)|No| |False";
            public const string IsAutoClosedRegOnPaymentWithHoldTx = "Is Auto Closed Registration On Payment With Hold Transaction? (Yes or No)|No| |False";
            public const string IsAutoClosedRegOnDischargePermit = "Is Auto Closed Registration On Discharge Permit ? (Yes/No)|No;RSI:Yes| |False";
            public const string IsAutoClosedRegOpOnPayment = "Is Auto Closed Reg-OP On Payment ? (Yes or No)|No| |False";
            public const string IsCashPurchaseOrderUpdatePrice = "Is Cash Purchase Order Update Price (Yes/No)|No||False";
            public const string IsCreateAssetIdAutomatic = "Is Create Asset Id Automatic (Yes/No)|No| |True";
            public const string IsAutoCreateApplicantNo = "Is Auto Create Applicant No (Yes/No)|No;RSBK:Yes| |True";
            public const string IsCreateCustomerIdAutomatic = "Is Create Customer Id Automatic (Yes/No)|No| |False";
            public const string IsCreateItemIdProductAutomatic = "Is Create Item ID Product Automatic ? (Yes or No)|Yes| |True";
            public const string IsCreateItemIdProductAutomaticUseGroupInitial = "Is Create Item ID Product Automatic Use Group Initial? (Yes or No)|No;GRHA:Yes| |True";
            public const string IsCreateItemIdProductAutomaticUseNameSeparated = "Is Create Item ID Product Automatic Use Name With Separated Item Type? (Yes or No)|No;RSYS:Yes| |True";
            public const string IsCreateItemIdServiceAutomaticUseGroupInitial = "Is Create Item ID Product Service Use Group Initial? (Yes or No)|No;RSMM:Yes| |True";
            public const string IsCreateParamedicIdAutomatic = "Is Create Paramedic Id Automatic|No| |True";
            public const string IsCreateSupplierIdAutomatic = "Is Create Supplier Id Automatic (Yes/No)|Yes| |True";
            public const string IsCreateFabricIdAutomatic = "Is Create Fabric Id Automatic (Yes/No)|Yes| |True";
            public const string IsDistReqOrPurcReqUsingBudgetPlan = "Is Distibution Request Or Purchase Request Using Budget Plan Approval|No| |True";
            public const string IsDistributionAutoConfirm = "IsDistributionAutoConfirm|Yes||True";
            public const string IsDistributionMenuIsUsedAsItemRequestMenu = "Is Distribution Menu Is Used As Item Request Menu? (Yes / No)|Yes| |True";
            public const string IsForceUseNoIntermbill = "Is Force Use No Intermbill In Print Detail Billing Patient? (Yes/No)|Yes| |True";
            public const string IsGuarantorInRegEditable = "Guarantor Editable in Registration grid Setting|Yes||False";
            public const string IsHideOpenCloseOnVerificationForUser = "Is hide button open/close registration in verification for non administrator? (Yes/No)|Yes| |False";
            public const string IsIndependentVoidRegistration = "IsIndependentVoidRegistration|Yes||True";
            public const string isInpatientUsingClass = "is Inpatient Using Class|0||False";
            public const string IsInventoryIncludeTax = "Is Inventory Include Tax (perhitungan harga persediaan plus ppn atau tidak)|No| |True";
            public const string IsInventoryIncludeDiscount = "Is Inventory Include Discount (perhitungan harga persediaan minus diskon atau tidak)|Yes| |True";
            public const string IsItemInventoryNameUsingUpperCase = "Is Item Name for Inventory Using Upper Case? (Yes/No)|Yes| |True";
            public const string IsItemProductAllowEditByUserVerificated = "Is Item Product Allow Edit By User Verificated (Yes/No)|Yes| |True";
            public const string IsItemProductIncludeInAdminCalc = "Is Item Product Include In Admin Calculation? (Yes / No)|Yes| |True";
            public const string IsNavigateUrlForDistributionWithStockInfo = "Navigate Url For Distribution With Stock Info? (Yes or No)|Yes| |True";
            public const string IsOperatingRoomResetPrice = "Is Operating Room Reset Price? (Yes / No)|Yes| |False";
            public const string IsOperatingRoomResetPriceLastClass = "Is Operating Room Reset Price with Last Class? (Yes / No)|Yes| |True";
            public const string IsOutPatientInculeInAdminCalculation = "Parameter is outpatient reg include on admin calculation, Value : Yes/No Default : No|No||True";
            public const string IsPatientCardPrintedOnlyForOutpatients = "Is Patient Card Printed Only For Outpatients? (Yes/No)|Yes| |False";
            public const string IsPatientEpisodeHistoryHideClosed = "Hide closed registration for Patient Episode And History List|Yes||True";
            public const string IsPemisahanCOAUangRacikan = "Pemisahan COA Uang Racikan|0||False";
            public const string IsPhycisianInRegEditable = "Phycisian Editable in Registration grid Setting|Yes||False";
            public const string IsPhysicianFeeCalcBasedOnGuarantorCategory = "Is Physician Fee Calc Based On Guarantor Category (Yes/No)|Yes| |True";
            public const string IsPhysicianFeeArBasedOnPayment = "Is Physician Fee For A/R Based On Payment (Yes/No)|No| |True";
            public const string IsPhysicianFeeArCreateOnArReceipt = "Is Physician Fee for A/R Create On A/R Receipt (Yes/No)|Yes| |True";
            public const string IsPhysicianFeeArPaidBasedOnPayment = "Is Physician Fee For A/R Paid Based On Payment (Yes/No)|No| |True";
            public const string IsPhysicianFeeBasedOnPayment = "Is Physician Fee Based On Payment (Yes/No)|Yes| |True";
            public const string IsPhysicianFeeUsingTaxCalculation = "Is Physician Fee Using Tax Calculation ?|0| |True";
            public const string IsPoAndPorInTheSameUnit = "Is PO And POR In The Same Unit? (Yes / No)|No| |True";
            public const string IsPorCanChangeThePrice = "Is POR Can Change The Price? (Yes/No)|No| |True";
            public const string IsPoWithThreeTypesOfTaxes = "Is PO With Three Types Of Taxes? (Yes/No)|Yes| |True";
            public const string IsProductionOfGoodUpdatingTariff = "Is Production Of Good Updating Tariff|No| |False";
            public const string IsPurcReturnWithPrice = "Is Purchase Return With Price|No| |True";
            public const string IsRADTLinkToHumanResourcesModul = "Is RADT Link To Human Resources Modul?|No| |True";
            public const string IsReferPatientUsingClassBefore = "Is Refer Patient To Other Unit Using Class Before?|Yes| |True";
            public const string IsRefreshBeforeLockVerification = "Refresh Before Lock|Yes| |False";
            public const string IsRegistrationEmPrintLabel = "Is Registration Emergency Print Automatic Label? (Yes or No)|No| |True";
            public const string IsRegistrationPrintLabelNewPatient = "Is Registration Print Automatic New Patient Label? (Yes or No)|No;RSSMCB:Yes| |True";
            public const string IsRegistrationIdentity = "Is Print Registration Identity Automatic|No| |True";
            public const string IsRegistrationOpPrintLabel = "Is Registration Outpatient Print Automatic Label? (Yes or No)|No| |True";
            public const string IsRegistrationPrintAutomatic = "Is Registration Print Automatic ? (Yes or No)|Yes| |False";
            public const string IsRegistrationPrintLabel = "Is Registration Print Automatic Label? (Yes or No)|No| |False";
            public const string IsRegistrationPrintReceipt = "Is Registration Print Auto Receipt? (Yes or No)|Yes| |False";
            public const string IsRegistrationPrintSlip = "Is Registration Print Automatic Slip? (Yes or No)|No| |False";
            public const string IsRegistrationPrintTicket = "Is Registration Print Auto Ticket? (Yes or No)|No| |False";

            public const string IsRegistrationMcuPrintSlip = "Is Registration MCU Auto Print Slip? (Yes or No)|No| |False";
            public const string IsRegistrationMcuPrintTicket = "Is Registration MCU Auto Print Ticket? (Yes or No)|No| |False";
            public const string IsRegistrationMcuPrintLabel = "Is Registration MCU Auto Print Label? (Yes or No)|No| |False";
            public const string RegistrationSlipMcuRpt = "Program ID for Registration Slip of MCU|| |True";
            public const string RegistrationTicketMcuRpt = "Program ID for Registration Ticket of MCU|| |True";
            public const string RegistrationLabelMcuRpt = "Program ID for Registration Label of MCU|| |True";

            public const string EmployeeClinicalAssignmentLetterKomedRpt = "Program ID for Employee Clinical Assignment Letter - Komed|| |False";
            public const string EmployeeClinicalAssignmentLetterKomkepRpt = "Program ID for Employee Clinical Assignment Letter - Komed|| |False";
            public const string EmployeeClinicalAssignmentLetterKtklRpt = "Program ID for Employee Clinical Assignment Letter - Komed|| |False";

            public const string IsRegistrationRequiredSMF = "Is Registration Required SMF? (Yes/No)|Yes| |True";
            public const string IsRegistrationTracer = "Is Print Registration Tracer Automatic?(Yes/No)|Yes| |True";
            public const string IsRegReferralGroupMandatory = "Is Referral group in registration mandatory? (Yes/No)|Yes| |False";
            public const string IsSeparatePaymentForOpConsul = "Is Separate Payment For Op Consul? (Yes / No)|Yes| |True";
            public const string IsSeparationBetweenOutpatientAndInpatientSupplies = "Is Separation Between Outpatient And Inpatient Supplies (Yes / No)|No| |True";
            public const string IsSharePurchaseDiscToPatient = "Is Share Purchase Disc To Patient? (Yes / No)|No| |True";
            public const string IsShowPrescPriceOnDisplayDoctor = "Is Show Prescription Price On Display Doctor (Yes / No)|Yes;YBRSGKP:No| |True";
            public const string IsShowSystemQtyInStockTacking = "Is Show System Qty In Stock Tacking? (Yes/No)|No| |True";
            public const string IsStockOpnamePerGroupItem = "Is Stock Opname Per Group Item ? (Yes / No)|Yes| |True";
            public const string IsUpdatePhysicianLookingPhysicianFeeVerification = "Is Update physician looking physicianfee verification or else IsHoldTransactionEntry and IsClosed|Yes| |False";
            public const string IsUpdatePrescriptionPriceWhenRecal = "Is Update Prescription Price When Recal? (Yes/No)|No| |True";
            public const string IsUpdatePriceUsingParentRuleWhenRecal = "Is Update Price Using Parent Rule (SRRegistrationType from parent Regno) When Recal? (Yes/No)|No;RSRM:Yes| |True";
            public const string IsUppercasePatientID = "Is Uppercase Patient Identity (Yes or No)?|Yes| |True";
            public const string IsUsingApprovalPurchaseRequest = "Is Using Approval Purchase Request|No||True";
            public const string IsUsingAvgForSalesToBranchPrice = "Is Using Average For Sales To Branch Price(Yes/No)|Yes| |False";
            public const string IsUsingHisInterop = "Is Using His Interop|Yes||True";
            public const string IsUsingHisInteropCorrection = "Is Using His Interop Correction? (Yes/No)|No||True";
            public const string IsUsingHisInteropToHcLab = "Is Using His Interop To HC Lab? (Yes/No)|No||True";
            public const string IsUsingHisInteropWithMultipleConnection = "Is Using His Interop With Multiple Connection? (Yes/No)|No||True";
            public const string IsUsingHumanResourcesModul = "Is Using Human Resources Modul?|No| |True";
            public const string IsUsingIntermBill = "Is Using Interm Bill? (Yes or No)|Yes| |True";
            public const string IsUsingExtramuralItem = "Is Using Extramural Item? (Yes or No)|No;YBRSGKP:Yes| |True";
            public const string IsUsingPettyCash = "Is Using Petty Cash? (1:Yes, 0:No)|0| |False";
            public const string IsUsingPrescriptionOrder = "Is Using Prescription Order for Inpatient (Yes / No)|No| |True";
            public const string IsUsingRoomingIn = "Is Using Rooming In? (Yes or No)|No| |True";
            public const string IsShowGenderOnBedInformationStatus = "Is Show Gender On Bed Information Status? (Yes or No)|Yes;RSMMP:No|�|True";
            //public const string IsUsingSeparateNoForEachPharmacyUnit = "Is Using Separate No For Each Pharmacy Unit? (Yes/No)|No| |True";
            public const string IsUsingTerminalDigitMedicalNo = "Is Using Terminal Digit MedicalNo|No||True";
            public const string IsPOWithStockInfo = "PO With Stock Info (Yes/No) |No||False";
            public const string IsWorkOrderRealizationAutoReturn = "Is Work Order Realization Auto Return to Logistic Central WH|No| |False";
            public const string ItemDiagnostic = "Item Type for Item Diagnostic|51| |True";
            public const string ItemGroupMaterai = "Item Group for Materai|AT02| |False";
            public const string ItemGroupMcuPackageID = "Item Group Mcu Package ID|PM01| |False";
            public const string ItemIdOngkir = "Item ID for Ongkos Kirim Lab Luar|| |False";
            public const string ItemLaboratory = "Item Type for Item Laboratory|31| |True";
            public const string ItemOptic = "Item Type for Item Optic|71| |True";
            public const string ItemPackage = "Item Type for Item Package|61| |True";
            public const string ItemProductCostPriceType = "Item Product Cost Price Type (AVG / FIFO)|AVG| |True";
            public const string ItemProductMedic = "Item Type for Item Product Medical|11| |True";
            public const string ItemProductNonMedic = "Item Type for Item Product Non Medical|21| |True";
            public const string ItemRadiology = "Item Type for Item Radiology|41| |True";
            public const string ItemService = "Item Type for Item Service|01| |True";
            public const string Kiosk_ListPoli1 = "Setting For Service Unit Of Kiosk|D01.C01;D01.C02;D01.C03;D01.C04;D01.C05;D01.C06;D01.C07;D01.C08;D01.C09;D01.C10;D01.C11;D01.C12| |False";
            public const string Kiosk_ListPoli2 = "Setting For Service Unit Of Kiosk|D01.C13;D01.C14;D01.C15;D01.C16;D01.C17;D01.C18;D01.C19;D01.C20;D01.C21;D01.C22;D01.C23;D01.C24| |False";
            public const string Kiosk_ListPoli3 = "Setting For Service Unit Of Kiosk|D01.C25;D01.C26;D01.C27;D01.C28;D01.C29;D01.C30;D01.C32| |False";
            public const string LocationKitchenID = "Location ID for Kitchen|LOC017| |True";
            public const string LocationLogisticCentralWHID = "Location ID for Logistic Central Warehouse|| |False";
            public const string MainDistributionServiceUnitIDForNonMedical = "Main Service Unit Of Distribution Of Non Medical Item|..| |True";
            public const string MainDistributionLocationIDForNonMedical = "Main Location Of Distribution Of Non Medical Item|..| |True";
            public const string MainPurchasingUnitIDForNonMedical = "Main Purchasing Unit Of Non Medical Item|..| |False";
            public const string MainPurchasingUnitIDForMedical = "Main Purchasing Unit Of Medical Item|..| |False";
            public const string MaxDiscTxInPercentage = "Max Discount Transaction Tariff Component DR (Percentage)|100| |False";
            public const string MaxDiscTxTariffRsInPercentage = "Max Discount Transaction Tariff Component RS (Percentage)|100| |False";
            public const string MaxResultRecord = "Max Result Record|150| |False";
            public const string MaxResultRecordEmrList = "Max Result Record for EMR List Page|150| |False";
            public const string ComboBoxDataServiceMaxResultRecord = "ComboBox DataService Max Result Record|50| |False";
            public const string MedicalFileCategoryIn = "Medical File Category : In|1||False";
            public const string MedicalFileCategoryOut = "Medical File Category : Out|2||False";
            public const string MedicalFileStatusConfirm = "Medical File Status : Confirm|2||False";
            public const string MedicalFileStatusRequest = "Medical File Status : Request|1||False";
            public const string MedicalRecordServiceUnitID = "Service Unit ID for Medical Record|D01.E01| |False";
            public const string MedicalSupportDepartmentID = "Department ID for Medical Support|D02| |False";
            public const string MpiUrlApi = "MpiUrlApi|none| |False";
            public const string NarkotikaID = "Kode Narkotika di ItemGroup|1102| |False";
            public const string NonClassID = "Non Class ID|| |False";
            public const string NursingAssessmentDO = "Nursing Assessment DO|As322| |False";
            public const string NursingAssessmentDS = "Nursing Assessment DS|As321| |False";
            public const string OperatingRoomServiceUnitID = "Service Unit ID for Operating Room|E-A01| |False";
            public const string OperatingTheaterClusterID = "Cluster ID for OperatingTheater|| |False";
            public const string OpticDepartmentID = "Department ID for Optic || |False";
            public const string OrderResultFolderPath = "Order Result Folder Path|none| |False";
            public const string OTCPrescriptionPatientID = "Patient ID for OTC Prescription|P1412-000141329| |False";
            public const string OutPatientClassID = "Class ID for Outpatient|Z1| |False";
            public const string OutPatientDepartmentID = "Department ID for Outpatient |D01| |False";
            public const string MedicalCheckUpDepartmentID = "Department ID for Medical Check Up || |False";
            public const string ParamedicIdDokterLuar = "Paramedic ID : Dokter Luar|none| |False";
            public const string ParamedicTariffComponentID = "ParamedicTariffComponentID|02| |False";
            public const string ParamedicTeamStatusMain = "Paramedic Team Status : Main (Dokter Utama)|01| |False";
            public const string PatientCardItemID = "PatientCardItemID|||False";
            public const string PatientCenterUrl = "Url for Patient Center|| |True";
            public const string PatientInTypeEr = "Patient In Type : Emergency|PatientInType-003| |True";
            public const string PatientInTypeIp = "Patient In Type : Inpatient|PatientInType-001| |True";
            public const string PatientInTypeOp = "Patient In Type : Outpatient|PatientInType-002| |True";
            public const string PayableStatusPaid = "Payable Status : Paid|2| |False";
            public const string PayableStatusProcess = "Payable Status : Process|0| |False";
            public const string PayableStatusVerify = "Payable Status : Verify|1| |False";
            public const string PaymentMethodBiaya = "Payment Method Biaya (AR)|PaymentMethod-005||True";
            public const string PaymentMethodCash = "Payment Method : Cash|PaymentMethod-001| |False";
            public const string PaymentMethodCashName = "Payment Method Name for Cash|Cash| |False";
            public const string PaymentMethodCreditCard = "Payment Method : Credit Card|PaymentMethod-002| |False";
            public const string PaymentMethodDebitCard = "Payment Method : Debit Card|PaymentMethod-003| |False";
            public const string PaymentMethodDiscount = "Payment Method : Discount|PaymentMethod-005| |False";
            public const string PaymentMethodPackageBalance = "Payment Method : Package Balance|PaymentMethod-005| |True";
            public const string PaymentMethodTransfer = "Payment Method : Transfer|PaymentMethod-004| |False";
            public const string PaymentMethodQris = "Payment Method : QRIS|;RSIAMTP:PaymentMethod-007| |True";
            public const string PaymentMethodTransferName = "Payment Method Name for Transfer|Transfer| |False";
            public const string PaymentTypeCorporateAR = "Payment Type : Corporate A/R|PaymentType-004| |False";
            public const string PaymentTypeBackOfficePayment = "Payment Type : Corporate A/R|PaymentType-006| |False";
            public const string PaymentTypeDiscount = "Payment Type : Discount|PaymentType-005| |False";
            public const string PaymentTypeDownPayment = "Payment Type : Down Payment|PaymentType-001| |False";
            public const string PaymentTypePayment = "Payment Type : Payment|PaymentType-002| |False";
            public const string PaymentTypePaymentName = "Payment Type Name for Payment|Payment| |False";
            public const string PaymentTypePersonalAR = "Payment Type : Personal A/R|PaymentType-003| |False";
            public const string PaymentTypeReturn = "Payment Type : Return|PaymentType-005| |False";
            public const string PaymentTypeSaldoAR = "Payment Type : Saldo A/R|PaymentType-008| |False";
            public const string PaymentTypeCredit = "Payment Type : Credit (Asset)|PaymentType-011| |False";
            public const string PenJaRad = "Penanggung Jawab Radiologi|-| |True";
            public const string PercentPph21Base = "Percentage of Basic Cutting for PPh 21|50| |False";
            public const string PettyCashUnitCashierID = "Petty Cash Unit ID for Cashier|002| |False";
            public const string PettyCashUnitFinanceID = "Petty Cash Unit ID for Finance|001| |False";
            public const string PharmacyDepartmentID = "Department ID of Pharmacy|D02| |False";
            public const string PharmacyHead = "Pharmacy Head|Nurhasanah, S.Apt| |False";
            public const string PharmacyHeadHomeAddr = "Pharmacy Head Home Address|......| |False";
            public const string PharmacyHeadJob = "Pharmacy Head Job|Farmasi RS. Usada Insani| |False";
            public const string PharmacyHeadLicenseNo = "Pharmacy Head License No.|......| |False";
            public const string PhysicianTypeAnesthetic = "Physician Type : Anesthetic|ParamedicType-003| |False";
            public const string PhysicianTypeAssAnesthesia = "Physician Type : Assistant Anesthesia|ParamedicType-007| |True";
            public const string PhysicianTypeAssistant = "Physician Type : Assistant|ParamedicType-004| |False";
            public const string PhysicianTypeInstrumentator = "Physician Type : Instrumentator|ParamedicType-008| |False";
            public const string PicFinance = "Person In Charge of Finance|Timbul Napitupulu| |False";
            public const string PicHeadOfAdmitting = "Person In Charge Of Admitting|.....| |False";
            public const string PicManagingDirector = "Person In Charge of Managing Director|Drg. Kuntari Retno, MARS| |False";
            public const string PicManagingDirectorForInvoicing = "Person In Charge of Managing Director For Invoicing|| |False";
            public const string PicManagingDirectorPhoneNo = "Phone No of Person In Charge of Managing Director For Invoicing|| |False";
            public const string PicMedicalDirector = "Person In Charge of Medical Director|| |False";
            public const string PicMedicalRecord = "Person In Charge of Medical Record|Nur Amalia, Amd. Perkes| |False";
            public const string PicPharmacyCoordinator = "Person In Charge of Pharmacy Coordinator|| |False";
            public const string PicPurchasing = "Person In Charge of Purchasing|Maria Karim| |False";
            public const string PicReceivingWareHouse = "Person In Charge of Receiving WareHouse|-| |False";
            public const string PicWarehouse = "Person In Charge of Warehouse|......| |False";
            public const string PPH22 = "PPH 22|1.5| |False";
            public const string Ppn = "PPn u/ penjualan (dalam persentase)|10| |False";
            public const string PrescriptionEnableCustomEtiquette = "Prescription Enable Custom Etiquette (0=no, 1=yes)|1| |True";
            public const string PrescriptionReturnAdminValue = "Prescription Return Admin Value (Percentage)|10| |False";
            public const string PrescShowBlncForEpisodeHistory = "Prescription Show Balance For Doctor (Episode n History)|No||True";
            public const string PrinterManagerHostDefault = "PrinterManagerHostDefault|printerserver||True";
            public const string ProcessTypePositionGrade = "Payroll Process Type Position Grade (SRProcessType)|06||False";
            public const string ProcessTypeSalary = "Payroll Process Type : Salary|01||False";
            public const string ProcessTypeOvertime = "Payroll Process Type : Overtime|02;RSMM:01;RSMP:01||False";
            public const string ProductAccountIdForStencils = "Product Account Id For Item Stencils|310| |False";
            public const string ProductTypeInjeksi = "Product Type : Injeksi|008| |False";
            public const string PsikotropikaID = "Kode Psikotropika di ItemGroup|1101| |False";
            public const string RadiologyNoFormat = "Radiology No Format (Date Format)|yyyyMMddHHmmss| |False";
            public const string RadiologyParamedicId = "Radiology Paramedic Id|| |True";
            public const string RadiologyUnitID = "RadiologyUnitID|C-A02| |False";
            public const string ReceivableStatusClosed = "Receivable Status : Closed|3| |False";
            public const string ReceivableStatusPaid = "Receivable Status : Paid|2| |False";
            public const string ReceivableStatusProcess = "Receivable Status : Process|0| |False";
            public const string ReceivableStatusVerify = "Receivable Status : Verify|1| |False";
            public const string ReceivableTypeCorporate = "Receivable Type : Corporate|01| |False";
            public const string ReceivableTypePersonal = "Receivable Type : Personal|02| |False";
            public const string RecipeMarginValueCompound = "Margin for Compound Type Recipe (%)|0||True";
            public const string RecipeMarginValueNonCompound = "Recipe Margin Value for Item Non Compound|500| |False";
            public const string ReferralGroupDatangSendiri = "Referral Group : Datang Sendiri|| |False";
            public const string ReferralGroupPASUS = "Referral Group : PASUS (Pasien Khusus)|XXX| |False";
            public const string ReferralGroupPASUSLabel = "Referral Group PASUS Label (used in the transaction entry)|-PASIEN KHUSUS-| |False";
            public const string RegistrationCanChangeBedNo = "Registration Can Change BedNo|1| |True";
            public const string RegistrationInpatientIdentityRpt = "Registration Inpatient Identity|| |True";
            public const string RegistrationLabelEmRpt = "Program ID for RegistrationLabelEmRpt|SLP.01.0021| |True";
            public const string RegistrationLabelOpRpt = "Program ID for RegistrationLabelOpRpt|SLP.01.0055| |True";
            public const string RegistrationLabelRpt = "Program ID for RegistrationLabelRpt|| |True";
            public const string RegistrationLabelNewPatientRpt = "Program ID for RegistrationLabelNewPatientRpt|;RSSMCB:SLP.01.0124| |True";
            public const string RegistrationReceiptRpt = "RegistrationReceiptRpt|SLP.01.0014| |True";
            public const string RegistrationSlipKioskRpt = "Program ID for RegistrationSlipKioskRpt|SLP.01.0056| |False";
            public const string RegistrationSlipRpt = "Program ID for RegistrationSlipRpt|| |False";
            public const string RegistrationTicketRpt = "RegistrationTicketRpt|| |True";
            public const string RegistrationTypeEmergency = "RegistrationType : Emergency|EMR| |True";
            public const string RegistrationTypeInpatient = "RegistrationType : Inpatient|IPR| |True";
            public const string RegistrationTypeOutpatient = "RegistrationType : Outpatient|OPR| |True";
            public const string IsSelfCheckinPrintingSEP = "Is Self Checkin Printing SEP? (Yes / No)|Yes;| |False";
            public const string SepProgramIdRpt = "SEP Report Program ID|XML.01.0042| |False";
            public const string EmployeeMedicalInsuranceFormRpt = "Program ID for EmployeeMedicalInsuranceFormRpt|| |False";
            public const string EmployeeMaritalStatusForMedicalInsurance = "Employee Marital Status For Medical Insurance (100% Guarantee, separated by [,])|;RSBK:Z4| |False";
            public const string IsVisibleEmployeeMedicalInsuranceForm = "Is Visible Employee Medical Insurance Form? (Yes/No)|No;YBRSGKP:Yes| |True";
            public const string RecruitmentTestInterview = "Recruitment Test : Interview (separated by [,])|03,05| |False";
            public const string ReimbursementFactorBasicFactor = "Reimbursement Factor Based Basic Salary|3| |True";
            public const string ReimbursementFactorCharacteristics = "Reimbursement Factor Base On Characteristics|4| |True";
            public const string ReimbursementFactorNominal = "Reimbursement Factor Nominal Value|2| |True";
            public const string ReimbursementFactorUnlimit = "Reimbursement Unlimit Factor|1| |True";
            public const string RentalRoomsPercentage = "Rental Rooms Percentage|0| |False";
            public const string RoundingGlobalTransaction = "Rounding for Global Transaction|1| |False";
            public const string RoundingPayment = "Rounding for Payment|1| |False";
            public const string RoundingPrescription = "Rounding for Prescription (Line Amount)|1| |False";
            public const string RoundingTransaction = "Rounding for Transaction|1| |False";
            public const string SalaryComponentIdForBasicSalary = "Salary ComponentID For Basic Salary|1| |False";
            public const string SelfGuarantorID = "Guarantor ID for Self|SELF| |False";
            public const string ServiceUnitForRequestOrder = "Service Unit ID for Request Order|| |False";
            public const string ServiceUnitImmunizationId = "Service Unit ID for Immunization|A-B08| |False";
            public const string ServiceUnitKiaId = "Service Unit ID for KIA|A-B08| |True";
            public const string ServiceUnitLaboratoryID = "Service Unit ID for Laboratory|C-A03| |False";
            public const string ServiceUnitLaboratoryIdArray = "Service Unit ID for Laboratory (separated by [,])|| |False";
            public const string ServiceUnitMcuId = "Service Unit ID for MCU|A-C04| |False";
            public const string ServiceUnitMedicalRehabId = "Service Unit ID for Medical Rehabilitation|D02.B04| |False";
            public const string ServiceUnitObstetricsId = "Service Unit ID for Obstetrics|A-A06| |False";
            public const string ServiceUnitOperationRoomID = "Service Unit ID for Operation Room|D01.A04| |False";
            public const string ServiceUnitOpticID = "Service Unit ID for Optic|| |False";
            public const string ServiceUnitPharCentralWarehouseId1 = "Service Unit ID for Pharmacy Central Warehouse I|D02.A03| |False";
            public const string ServiceUnitPharCentralWarehouseId2 = "Service Unit ID for Pharmacy Central Warehouse II|D02.A04| |False";
            public const string ServiceUnitPharmacyID = "Service Unit ID for Pharmacy|C-C02| |False";
            public const string ServiceUnitPharmacyIdOpr = "Service Unit ID for Pharmacy Outpatient|C-C03| |False";
            public const string ServiceUnitRadiologyID = "Service Unit ID for Radiology|C-A02| |True";
            public const string ServiceUnitRadiologyID2 = "Service Unit ID for Radiology II|C-A02| |False";
            public const string ServiceUnitRadiologyIdArray = "Service Unit ID for Radiology (separated by [,])|| |True";
            public const string ServiceUnitStencilsID = "Service Unit ID for Stencils|| |True";
            public const string ServiceUnitVkId = "Service Unit ID for VK|| |False";
            public const string ShiftAfternoon = "Shift ID for Afternoon (Siang)|ShiftID-002| |False";
            public const string ShiftMorning = "Shift ID for Morning (Pagi)|ShiftID-001| |False";
            public const string ShiftNight = "Shift ID for Night (Malam)|ShiftID-003| |False";
            public const string ShiftStartAfternoon = "Shift Start for Afternoon (jam mulai shift siang)|14:00| |False";
            public const string ShiftStartMorning = "Shift Start for Morning (jam mulai shift pagi)|07:00| |False";
            public const string ShiftStartNight = "Shift Start for Night (jam mulai shift malam)|21:00| |False";
            //public const string  StatusABC-A = "Status ABC - A|75| |True";
            //public const string  StatusABC-B = "Status ABC - B|15| |True";
            //public const string  StatusABC-C = "Status ABC - C|10| |True";
            public const string SubLedgerGroupIdGuarantor = "SubLedger Group Id Guarantor|4| |False";
            public const string SubLedgerGroupIdServiceUnit = "SubLedger Group Id Service Unit|2| |False";
            public const string SubLedgerGroupIdSupplier = "SubLedger Group Id Supplier|3| |False";
            public const string TablePatientFieldValidation = "Table Patient Field Validation|| |True";
            public const string TableRegistrationResponsiblePersonFieldValidation = "Table Registration Responsible Person Field Validation (NameOfTheResponsible,SRRelationship,SROccupation,HomeAddress,PhoneNo,Ssn)|| |True";
            public const string TariffComponentBhp = "Tariff Component : Bahan Habis Pakai|09| |False";
            public const string TariffComponentJasaMedisID = "Tariff Component ID for Jasa Medis |02,03| |False";
            public const string TariffComponentJasaSaranaID = "Tariff Component ID for Jasa Sarana |01| |False";
            public const string TariffComponentPriceVisible = "Tariff Component Price Visible (1:True, 0:False)|1||True";
            public const string TariffComponentPrimaryPhysicianID = "Tariff Component ID for Primary Physician|02| |False";
            public const string TaxPercentage = "TaxPercentage (PPN)|10| |False";
            public const string TherapyGroupAntibiotics = "Therapy Group : Antibiotics|7| |False";
            public const string TimeLimitForVoidPayment = "Time Limit For Un-Approval/Void Payment in Hour(s)|10| |False";
            public const string TracerRpt = "Program ID for Tracer Report|SLP.01.0012| |False";
            public const string UMR = "Upah Minimum Regional Daerah|0| |False";
            public const string UnusedBalanceCarryOver = "Unused Balance Added to the following period|2| |True";
            public const string UsingPromotion = "Promotion Type in Payment (Yes/No)|No| |False";
            public const string TheSellingPriceBasedOnTheHighestPrice = "The Selling Price Based On The Highest Price (Yes/No)|Yes| |True";
            public const string TheSellingPriceBasedOnTheHighestPriceByPeriod = "The Selling Price Based On The Highest Price By Period (Yes/No)|No;RSIMT:Yes| |True";
            public const string TheSellingPriceBasedOnTheHighestPricePurchPeriod = "The Selling Price Based On The Highest Price Purchase Period (last ... purchases)|3| |True";
            public const string WorkPriorityNormal = "Work Priority : Normal|01| |False";
            public const string WorkStatusOpen = "Work Status : Open|01| |False";
            public const string WorkStatusClosed = "Work Status : Closed|02| |False";
            public const string WorkStatusCancelled = "Work Status : Cancelled|03| |False";
            public const string WorkStatusDone = "Work Status : Done|04| |False";
            public const string WorkStatusWaitingForParts = "Work Status : Waiting For Parts|05| |False";
            public const string WorkStatusThirdParties = "WorkStatus : Third Parties|06| |False";
            public const string WorkTypeProject = "Work Type : Project|04| |False";
            public const string PicWarehouseHead = "PicWarehouseHead || |False";

            public const string IsAutoRefreshEhrList = "Auto refresh EHR patient list every 1 minute (Yes/No)|Yes| |False";
            public const string IsAutoPrintPrescriptionOrder = "Auto Print Physician Prescription Order via direct printer(Yes/No)|Yes| |False";
            public const string IsSoapeCanEntryByUserNonPhsycian = "Soape or Assessment Can Entry By User Non Phsycian (Yes/No)|No| |False";
            public const string IsSickLetterCanEntryByUserNonPhsycian = "Sick Letter Can Entry By User Non Phsycian (Yes/No)|Yes| |False";
            public const string IsPrescriptionCanEntryByUserNonPhsycian = "Prescription Can Entry By User Non Phsycian (Yes/No)|Yes| |False";
            public const string IsExamOrderCanEntryByUserNonPhsycian = "Exam Order Can Entry By User Non Phsycian (Yes/No)|Yes| |False";
            public const string IsReferToSpecialistCanEntryByUserNonPhsycian = "Refer To Specialist Can Entry By User Non Phsycian (Yes/No)|Yes| |False";
            public const string IsDischargeCanEntryByUserNonPhsycian = "Discharge Can Entry By User Non Phsycian (Yes/No)|Yes| |False";
            public const string IsSurgicalCanEntryByUserNonPhsycian = "Surgical Can Entry By User Non Phsycian (Yes/No)|Yes| |False";
            public const string IsOperatingNotesCanEntryByUserNonPhsycian = "Operating Notes Can Entry By User Non Phsycian (Yes/No)|Yes| |False";
            public const string IsUsingHetAsMaxSalesPrice = "Is Using HET As Max Sales Price? (Yes/No)|Yes| |False";
            public const string PhysicianIsRequiredAtRegistration = "Physician Required At Registration (Yes/No)|Yes| |False";
            public const string LisInterop = "Interop to Lab Information System|SYSMEX| |True";
            public const string LisCriticalFieldName = "Interop Lis Critical Field Name|;RSI:rpt_flag_to| |False";
            public const string IsUseApprovalLevel = "Is Use Approval Level? (Yes/No)|No| |True";
            public const string IsDistributionUseApprovalLevel = "Is Use Distribution Approval Level? (Yes/No)|No;RSSMCB:Yes| |True"; // Deby 23 Maret 2018
            public const string IsHigherApprovalLevelCanBypass = "Is higher approval level can bypass? (Yes/No)|No| |True";

            public const string EmailAddress = "Email Address for send email|AvicennaHis.SCI@gmail.com| |False";
            public const string EmailPassword = "Email Password for send email|sciadmin88| |False";
            public const string EmailPort = "Email Port for send email|587| |False";
            public const string EmailHost = "Email Host for send email|smtp.gmail.com| |False";

            public const string EmailAddressHO = "Email Address Head Office for budgeting approval|AvicennaHis.SCI@gmail.com| |False";

            public const string IsAllowAdditionalAP = "Allow create Additional AP or non PO|No| |False";
            public const string PpnOutRJ = "PPN Keluaran Obat Alkes RJ (dalam persentase)|0| |False";
            public const string PpnOutRD = "PPN Keluaran Obat Alkes RD (dalam persentase)|0| |False";

            public const string AplicationErrorEmailAddress = "Email Address for send email application error|avicennahis.sci.logerror@gmail.com| |true";
            public const string IsStockOpnameFormPerBin = "Stock Opname form per Bin|No| |False"; // Handono 28 Juni 2017
            public const string IsUnApproveDisabledIfPerClosed = "Is UnApprove Disabled If Periode Closed? (Yes/No)|No| |True"; // Handono 13 Okt 2017
            public const string IsPphUsesAfixedValue = "Is Pph Uses A fixed Value? (Yes/No)|No| |True";

            public const string IsMinMaxItemBalanceAutoUpdate = "Is Minimum & Maximum Inventory Balance per Location Auto Update? (Yes/No) **OBSOLETE|No| |False"; //Handono 26 Jan 2018
            public const string IsMinMaxItemBalanceByStockGroupAutoUpdate = "Is Minimum & Maximum Inventory Balance per Stock Group Auto Update? (Yes/No)|No;RSSMCB:Yes| |False"; // Handono 26 Jan 2018

            public const string PeriodDayHistUsingForCalcMinBalance = "Period day history using item for calculate minimum balance (day)|3| |False"; // Handono 22 Nov 2017
            public const string PeriodDayHistUsingForCalcMaxBalance = "Period day history using item for calculate maximum balance (day)|42| |False"; // Handono 22 Nov 2017
            public const string PeriodDayHistUsingForCalcMinBalPerStockGroup = "Period day history using item for calculate minimum balance per stock group (day)|7| |False"; // Handono 22 Nov 2017
            public const string PeriodDayHistUsingForCalcMaxBalPerStockGroup = "Period day history using item for calculate maximum balance per stock group(day)|42| |False"; // Handono 22 Nov 2017

            public const string IsAllowCorrectionOfIntermBillsTransaction = "Is Allow Correction Of IntermBills Transaction? (Yes/No)|No| |True";
            public const string IsPorByStockGroup = "Is Purchase Request By Stock Group? (Yes/No)|No;RSSMCB:Yes| |False";
            public const string IsPorBySupplierItem = "Is Purchase Request By Supplier Item? (Yes/No)|No;RSSMCB:Yes| |False";
            public const string IsPorByProductAccount = "Is Purchase Request Product Account? (Yes/No)|No;RSSMCB:Yes| |False";
            public const string IsShowPriceInPurchaseRequest = "Is Show Price In Purchase Request? (Yes/No)|No| |False";
            public const string IsPaymentCheckBeforePatientTrans = "Is Payment Remain Check Before Patient Transaction ? (Yes/No)|No;RSSMCB:Yes| |False";
            public const string IsLogUserAccessProgram = "Is Log User Access Program ? (Yes/No)|No| |true";

            public const string IsUsingRoundingDown = "Is Using Rounding Down? (Yes/No)|No;RSSMCB:Yes| |False"; // Deby 15 Maret 2018
            public const string IsUsingRoundingDownWithBalancing = "Is Using Rounding Down With Balancing [Exp. rounding=500; amount=1450 --> result=Yes:1500,No:1000; amount=1245 --> result=Yes:1000,No:1000]? (Yes/No)|No;YBRSGKP:Yes| |False"; // Deby 17 Oct 2023
            public const string IsAutoPrintPrescriptionReceipt = "Is Auto Print Prescription Receipt? (Yes/No)|No;RSSMCB:Yes| |False";
            public const string IsAutoPrintDistributionReceipt = "Is Auto Print Distribution Receipt? (Yes/No)|No| |False";
            public const string IsAutoPrintStockAdjustmentReceipt = "Is Auto Print Stock Adjustment Receipt? (Yes/No)|No| |False";
            public const string IsPOCanChangeConversion = "Is PO Can Change Conversion? (Yes/No)|No;RSSMCB:Yes;GRHA:Yes| |False";
            public const string IsPOCanChangePurchaseUnit = "Is PO Can Change Purchase Unit? (Yes/No)|No;RSMM:Yes| |True";
            public const string IsPOCanChangeQty = "Is PO Can Change Quantity? (Yes/No)|No;RSMM:Yes| |True";
            public const string KioskQueueSlipRpt = "Program ID for KioskQueueSlipRpt| | |False";
            public const string StockOpnameRowPerPage = "Stock Opname Row Per Page|31| |True";
            public const string ParamedicIdLabDefault = "Paramedic ID Default For Laboratory Transaction|| |False";
            public const string ParamedicIdRadDefault = "Paramedic ID Default For Radiology Transaction|| |False";
            public const string AppProgramPrintLabelMCU = "App Program Print Label MCU (Menu Health Screening Transaction)|| |True";
            public const string IsPrescriptionPendingDelivery = "Is Prescription Pending Delivery Activated (Yes/No)|No;RSMP:Yes| |True";
            public const string IsPrescriptionReturnToOneLocation = "Is Prescription Return To One Location? (Yes/No)|No;RSSMCB:Yes| |True"; // Deby 8 May 2018
            public const string IsPrescriptionReturnToOneLocationWithUserDefUnit = "Is Prescription Return To One Location With User Default Unit? (Yes/No)|No;RSTJ:Yes| |True"; // Deby 8 May 2018
            public const string IsTestResultAllowModifDate = "Is Test Result Allow Modif Date? (Yes/No)|No;RSPM:Yes;RSUI:Yes;EMC:Yes| |True";
            public const string IsManualUserHostName = "Is Manual User Host Name define|No;GRHA:Yes| |false";
            public const string ParamedicTeamStatusDpjpID = "Paramedic Team DPJP (Dokter Penanggungjawab Pasien)|01| |false";
            public const string ParamedicTeamStatusSharingID = "Paramedic Team Rawat Bersama|06| |false";
            public const string UserTypeDoctor = "User Type Doctor|DTR| |false";
            public const string UserTypeNurse = "User Type Nurse|NRS| |false";
            public const string UserTypeNutritionists = "User Type Nutritionists|NUT| |false";
            public const string ServiceUnitIDForIGD = "ServiceUnit ID For IGD|D02.01| |false";
            public const string IsPrescriptionOnlyInStock = "Is prescription item selection show only for stock>0|No;RSUSKY:Yes;RSMM:Yes| |false";
            public const string AssessmentAcuteRangeInDay = "Redirect to Assessment entry for patient Acute Range (days)?|30| |false";
            public const string AssessmentChronicRangeInDay = "Redirect to Assessment for patient Chronic Range (days)?|92| |false";
            public const string PhysiotherapyServiceUnitIDs = "Physiotherapy ServiceUnit ID (Separator by ;)|D01.03.13;| |false";
            public const string IsNeedAllowCheckoutConfirmedForDischarge = "Is Need Allow Checkout Confirmed For Discharge? (Yes/No)|No;RSMM:Yes| |True";
            public const string IsUseStandardMealMenuForAllClass = "Is Use Standard Meal Menu For All Class? (Yes/No)|No;RSMM:Yes| |True";
            public const string SRReferralGroupDefault = "Default value for SRRefferalGroup? || |True";
            public const string IsDisplayRegDateTimeUseCreateDate = "Is Display Registration Date and Time taken from Create Date? |No;RSSMCB:Yes;RSUI:Yes;RSPM:Yes;EMC:Yes| |True";
            public const string PhysicianSenderReferralGroups = "Physician sender referral groups (separated by comma (,))|;RSSMCB:03,05,15;RSUSKY:03,05;RSUI:01;RSPM:01;EMC:01| |True";
            public const string PatientIDForCafe = "Patient ID for Cafe Customer|P000000-000000| |True";
            public const string ServiceUnitIDForCafe = "Service Unit ID for Cafe|| |False";
            public const string IsDistributionRequestBasedOnItemsPerLocation = "Is Distribution Request Based On Items Per Location? (Yes/No)|No;RSMM:Yes| |True";
            public const string IsOperatingRoomResetPriceHighestClass = "Is Operating Room Reset Price Highest Class (Yes/No)|No| |False";
            public const string IsPurchaseRequestsUsingItemUnit = "Is Purchase Requests Using Item Unit? (Yes/No)|No;RSMM:Yes| |True";
            public const string QuestionIdBodyMassIndex = "Question ID For Body Mass Index|GEN.SGN.03| |False";
            public const string RadiologyNoAutoCreate = "Radiology No Auto Create? (Yes/No)|Yes;RSMM:No| |True";
            public const string IsAllowPaymentReturnFromCashEntry = "Is Allow Payment Return From Cash Entry (Yes/No)|No;RSUI:Yes;RSPM:Yes;EMC:Yes| |True";
            public const string IsDefaultPaymentReturnFromCashEntry = "Is Default Payment Return From Cash Entry (Yes/No)|No| |True";
            public const string IsControlEatingPatientByNutritionists = "Is Control Eating Patient By Nutritionists (Yes/No)|No;RSMM:Yes| |True";
            public const string IsEmrDiagnoseFreeText = "Is EMR Diagnose using Free Text (Yes/No)|No;RSMM:Yes| |True";
            public const string IsPhysicianPrescriptionSalesDefaultEmpty = "Is Physician for Prescription Sales Handling Default Empty (Yes/No)|No;RSMM:Yes| |True";
            public const string IsAutoBlacklistOnPersonalAr = "Is Auto Blacklist On Personal A/R? (Yes/No)|No;RSMM:Yes| |True";
            public const string IsAllowPrescriptionReturnForMultipleRegistration = "Is Allow Prescription Return For Multiple Registration? (Yes/No)|No;RSMM:Yes| |True";
            public const string IsPoBasedOnPr = "Is PO Based On PR? (Yes/No)|No;RSMM:Yes| |True";
            public const string IsServiceUnitPrescriptionSalesDefaultEmpty = "Is Service Unit for Prescription Sales Handling Default Empty? (Yes/No)|No;RSMM:Yes| |True";
            public const string IsCoaAPNonMedicSeparated = "Is COA AP for Non Medical Different from COA AP Medical? (Yes/No)|No;RSMM:Yes| |True";
            public const string acc_IsPpnPurchasing = "Is PPn Purchasing create journal? (Yes/No)|Yes;RSMM:No| |True";
            public const string acc_IsPpnPurchasingNonMedical = "Is PPn Purchasing non medical create journal? (Yes/No)|Yes;YBRSGKP:No| |True";

            public const string PatientInTypeTrueEmergency = "Patient In Type : True Emergency|01| |False";
            public const string DefaultUserPassword = "Default User Password|123| |False";
            public const string DefaultParamedicTeamOnEmrList = "Default Paramedic Team On Emr List (fill value with ALL or REGTOME)|ALL| |True";
            public const string IsAllowCorrectionForIntermBillTx = "Is Allow Correction For IntermBill Transaction? (Yes/No)|No| |False";
            public const string IsListItemForTxOnlyInStock = "Is List Item For Transaction Only In Stock? (Yes/No)|No;RSMM:Yes| |True";
            public const string IsAutoClosedRegOpOnTransfer = "Is Auto Closed Registration Outpatient On Transfer? (Yes/No)|Yes| |True";
            public const string IsAutoClosedRegFromOnCheckinConfirmed = "Is Auto Closed Registration From On Checkin Confirmed? (Yes/No)|No;YBRSGKP:Yes| |True";
            public const string IsRegValidateResponsibleName = "Is Registration Validate Responsible Person? (Yes/No)|No;RSSMCB:Yes| |True";
            public const string IsPatientIprOnPrescSalesForCheckinConfirmedOnly = "Is list patient IPR on Prescription Sales only for patient who have been checkin confirmed? (Yes/No)|Yes;RSMM:No| |True";
            public const string IsMoveRecordOnPrescSalesIncludeVoid = "Is Move Record On Prescription Sales Include Transaction Void? (Yes/No)|Yes;RSMM:No| |True";
            public const string IsRegistrationVoidReasonRequired = "Is Registration Void Reason Required? (Yes/No)|No;RSBHP:Yes| |True";
            public const string IsEnabledStockWithEdControl = "Is Enabled Stock With ED Control? (Yes/No)|No| |True";
            public const string IsSeparationOfItemPurchaseCategorization = "Is Separation Of Item Purchase Categorization? (Yes/No)|No;RSMM:Yes| |True";
            public const string GuarantorTypeBPJS = "Guarantor Type BPJS (see AppStandardRef: GuarantorType) |09| |True";
            public const string GuarantorTypeBpjsKapitasi = "Guarantor Type BPJS Kapitasi (see AppStandardRef: GuarantorType) || |false";
            public const string IsCashEntryShowReceivedFromPaidTo = "Is Cash Entry Shows Received From or Paid To? (Yes/No)|No;RSMM:Yes| |False";
            public const string IsDisplayExecutionDateOnPrescriptionSales = "Is Display Execution Date On Prescription Sales? (Yes/No)|No;RSMM:Yes| |True";
            public const string IsDisplayServiceUnitBookingNoOnTransactionEntry = "Is Display Service Unit BookingNo On Transaction Entry? (Yes/No)|No| |True";
            public const string IsDisplayKiaCaseAndObstetricTypeOnTransactionEntry = "Is Display Kia Case & Obstetric Type On Transaction Entry? (Yes/No)|No| |True";
            public const string IsBedNeedConfirmation = "Is Bed Need Confirmation? (Yes/No)|Yes| |True";
            public const string IsBedNeedCleanedProcess = "Is Bed Need Cleaned Process? (Yes/No)|No| |True";
            public const string IsWorkTradeMandatory = "Is Work Trade Mandatory On Work Order Realization? (Yes/No)|No;RSMM:Yes| |True";
            public const string IsAllowEditDischargeDate = "Is Allow Edit Discharge Date? (Yes/No)|Yes;RSMM:No| |True";
            public const string IsDischargeDateOnEmrMandatory = "Is Discharge Date Mandatory On Emergency? (Yes/No)|Yes;RSMM:No| |True";
            public const string IsCloseRegOnDischargeEmr = "Is Close Reg On Discharge Emr? (Yes/No)|Yes;RSMM:No| |True";
            public const string SupplierNonPkpTaxStatusDefault = "Supplier Non PKP Tax Status Default (1: Inclulde Tax; 2: No Tax)|1;RSMM:2| |True";
            public const string IsCloseOutstandingIssueRequest = "Is Close Outstanding Inventory Issue Request On Realization? (Yes/No)|No;RSSMCB:Yes| |True";
            public const string pphFeeBase = "Pph 21 base calculation (0:calculated fee, 1:tariff component fee)|0;RSSMCB:0| |True";
            public const string IsPhysicianFeeVerificationPaidOnly = "Is Physician Fee Verification Shows Paid Only? (Yes/No)|No;GRHA:Yes| |True";
            public const string IsAPVerifNeedValidate = "Is AP Verification Needs To Validate Payment Type And Bank? (Yes/No)|Yes;RSSMCB:No| |True";
            public const string IsMealOrderValidationForIncompleteItem = "Is MealOrder Validation For Incomplete Item? (Yes/No)|No;RSMM:Yes| |True";
            public const string IsEpisodeDiagValidateExtCauseAndMorp = "Is Episode Diagnosis Needs To Validate External Cause And Morphology? (Yes/No)|Yes;RSUI:No;RSPM:No;EMC:No| |False";
            public const string ItemIdImunisasiTT1 = "Item ID for Imunisasi TT1 (Separate by [,])|...| |False";
            public const string ItemIdImunisasiTT2 = "Item ID for Imunisasi TT2 (Separate by [,])|...| |False";
            public const string ServiceUnitImunisasiTTId = "Service Unit ID for Imunisasi TT1 & TT2 (Separate by [,])|...| |False";
            public const string IsAutoPrintCafeSlipOrder = "Is Auto Print Slip For Cafe Order? (Yes/No)|No| |False";
            public const string TariffPriceVisibleOnlyForAdm = "Tariff Price Visible Only For Adm Billing? (Yes/No)|No;RSBHP:Yes| |True";
            public const string DefaultConsumeMethod = "Default Consume Method : Aturan Pakai Diketahui|999| |False";
            public const string DefaultDosageUnit = "Default Dosage Unit|.| |False";
            public const string ServiceUnitPharmacyIdPos = "Service Unit ID for Pharmacy POS|.| |False";
            public const string ServiceUnitCashierID = "Service Unit ID for Cashier|.| |False";
            public const string IsUsingCashManagement = "Is Using Cash Management? (Yes/No)|No| |True";
            public const string IsPhysicianFeeCekPaymentUnpaid = "Is Physician Fee Calculation Check Payment Percentage Of Unpaid Transaction? (Yes/No)|No| |False";
            public const string IsPhysicianFeeCalculatePreFee = "Is Physician Fee Calculate Pre Service Fee? (Yes/No)|No; RSCDR:Yes| |True";
            public const string IsBypassCashierAuthorization = "Is Bypass Cashier Authorization? (Yes/No)|No;RSMM:Yes| |True";
            public const string IsDiagAndProcListRestoreValueFromCookie = "Is Diag And Proc List Restore Value From Cookie? (Yes/No)|Yes;RSMM:No| |True";
            public const string IsDiagAndProcListFilterParameter = "Is Diag And Proc List Filter Parameter? (Yes/No)|Yes;RSMP:No| |True";
            public const string IsReadonlyMedicalNoOnPatientEntry = "Is Readonly MedicalNo on Patient Entry? (Yes/No)|No| |True";
            public const string IsReadonlyMedicalNoOnEditPatientEntry = "Is Readonly MedicalNo on Edit Patient Entry? (Yes/No)|No;RSI:Yes| |True";
            public const string IsReadonlyMedicalNoOnUpdateMrnPatient = "Is Readonly MedicalNo on Update MRN Patient? (Yes/No)|No;RSI:Yes| |True";
            public const string IsReadonlyPatientNameOnEditPatientEntry = "Is Readonly Patient Name on Edit Patient Entry? (Yes/No)|No;RSI:Yes| |True";
            public const string IsPhysicianFeeShowProcedureNote = "Is Physician Fee Show Procedure Note On Verification? (Yes/No)|No;RSSMCB:Yes| |True";
            public const string IsApInvoiceCanChangeThePrice = "Is A/P Invoice Can Change The Price? (Yes/No)|Yes;RSSMCB:No| |True";
            public const string IsMedRecCanChangePatientDischarge = "Is Medical Record Can Change Patient Discharge? (Yes/No)|No| |True";
            public const string IsValidateNoteOnJobOrderLab = "Is Validate Note On Job Order Laboratory? (Yes/No)|No;RSUI:Yes;RSPM:Yes;EMC:Yes| |True";
            public const string IsValidateNoteOnAllJobOrder = "Is Validate Note On All Job Order? (Yes/No)|No;RSYS:Yes| |False";
            public const string IsMandatoryEmrRegDetail = "Is Mandatory Emr Registration Detail? (Yes/No)|Yes| |True";
            public const string IsPOWithStockInfoTotal = "Is PO With Stock Info Total? (Yes/No)|Yes;RSMM:No| |True";
            public const string IsDefaultEmptyPhysicianOnTransactionEntry = "Is Default Empty Physician On Transaction Entry? (Yes/No)|No;RSMM:Yes| |True";
            public const string IsAutoClosedOnPrApprovalZero = "Is Auto Closed On PR Approval Zero? (Yes/No)|No;RSMM:Yes| |True";
            public const string acc_IsCoaCashierPaymentTransferFromPaymentMethod = "Is Cashier Payment Transfer Get COA Priority From Payment Method? (Yes/No)|No;RSMM:Yes| |True";
            public const string IsAutoChargeBedOnRegistration = "Is Auto ChargeBed On Registration? (Yes/No)|No| |True";
            public const string IsItemBinIdAutoCreate = "Is Item Bin ID Auto Create? (Yes/No)|No| |True";
            public const string AssetsStatusActive = "Assets Status : Active|1| |True";
            public const string AssetsStatusInActive = "Assets Status : In-Active|2| |True";
            public const string AssetsStatusDisposed = "Assets Status : Disposed|3| |True";
            public const string AssetsStatusLost = "Assets Status : Lost|4| |True";
            public const string AssetsStatusSold = "Assets Status : Sold|5| |True";
            public const string AssetsStatusDamaged = "Assets Status : Damaged|6| |True";
            public const string IsArPaymentExcessToDiscount = "Is ARPayment Excess To Discount? (Yes/No)|No;RSSMCB:Yes| |True";
            public const string FeePaidPercentage = "Fee Paid Percentage|100| |False";
            public const string ServiceUnitLogisticCentralWarehouseId = "Service Unit Logistic Central Warehouse Id|| |False";
            public const string IsFeePaidPercentageBasedOnTotalInvoice = "Is Fee Paid Percentage Based On Total Invoice|No;RSSMCB:Yes| |False";
            public const string IsPORTaxTypeEnabled = "Is POR TaxType Enabled ? (Yes/No)|Yes;RSSMCB:No| |False";
            public const string IsPhysicianFeeVerifCorrectionAutoCheck = "IsPhysicianFeeVerifCorrectionAutoCheck? (Yes/No)|Yes;RSSMCB:No| |False";
            public const string IsAllowInventoryIssueWithoutRequest = "Is Allow Inventory Issue Without Request? (Yes/No)|No;RSSMCB:Yes| |False";
            public const string IsInventoryIssueNeedConfirm = "Is Inventory Issue Need Confirm? (Yes/No)|No;GRHA:Yes| |False";
            public const string IsPrescriptionLoadLastBought = "Is Prescription Load Last Bought? (Yes/No)|Yes| |False";
            public const string IsAllowRegistrationEmrChangePhysician = "Is Allow Registration Emergency Change Physician? (Yes/No)|No;RSSMCB:Yes| |False";
            public const string IsShowSystemQtyInStockTackingOnBarcode = "Is Show System Qty In Stock Tacking On Barcode Scaner? (Yes/No)|No;GRHA:Yes| |False";
            public const string IsPrescriptionReturnNoFormatBasedOnRegType = "Is Prescription Return Number Format Based On Registration Type? (Yes/No)|No| |True";
            public const string IsFeeCalculatePercentagePaidOnPayment = "Is Fee Calculate Percentage Paid On Payment? (Yes/No)|No;RSSMCB:Yes;GRHA:Yes;RSMM:Yes| |False";
            public const string IsShowClassNameOnDispBedinfo = "Is Show Class Name On Bedinfo Display? (Yes/No)|Yes;GRHA:No| |False";
            public const string IsValidateBpjsCoveredItemOnTx = "Is Validate Bpjs Covered Item On Transaction? (Yes/No)|No| |False";
            public const string MaxMedicalFileBinNo = "Max Medical File Bin Number)|27| |False";
            public const string IsReOrderPoBasedOnPrWithSeparatePurchasingUnit = "Is Re-Order PO Based On PR With Separate Purchasing Unit? (Yes/No)|No;RSSMCB:Yes| |False";
            public const string IsEnabledReferByPhyisicianOnRegistration = "Is Enabled Refer By Phyisician On Registration? (Yes/No)|No;RSMM:Yes| |False";
            public const string acc_JournalPORDate = "Date For Journal Of POR (0:POR Date, 1:Approval Date)|0;RSSMCB:1| |False";

            public const string IsBudgetingMedical = "Is Control Budget for Item Medical? (Yes/No)|No| |False";
            public const string IsBudgetingNonMedical = "Is Control Budget for Item Non Medical? (Yes/No)|No| |False";
            public const string IsBudgetingKitchen = "Is Control Budget for Item Kitchen? (Yes/No)|No| |False";
            public const string IsAllowVoidRegistrationOnTransfer = "Is Allow Void Registration On Transfer? (Yes/No)|No;RSSMCB:Yes| |False";
            public const string SepFolder = "Main folder for save SEP Document ?|C:\\SEP DOCUMENT| |false";
            public const string IsAutoApprovePackage = "Is Auto Approve Package? (Yes/No)|No| |False";
            public const string IsGuarantorValidateCOA = "Is Master Guarantor Validate COA? (Yes/No)|Yes;RSSMCB:No| |False";
            public const string IsUnmergeBillingCheckingIntermbillProcess = "Is Unmerge Billing Checking Interim Bill Process? (Yes/No)|No| |False";
            public const string IsPaymentShowTransactionListForAllRegType = "Is Payment Receive Show Transaction List For All Registration Type? (Yes/No)|No;RSSMCB:Yes| |False";
            public const string IsValidateProductAccountOnItem = "Is Validate Product Account On Item Product Medic, Non Medic and Kitchen? (Yes/No)|Yes;RSSMCB:No| |False";
            public const string IsRunTheCostCalculationCleanUpProcess = "Is Run The Cost Calculation Clean Up Process On Billing Recalculation? (Yes/No)|No| |True";
            public const string IsCheckinConfirmationUsingDetails = "Is Checkin Confirmation Using Details? (Yes/No)|No;RSMM:Yes| |True";
            public const string FirstTimeCheckMarkForTransfusionMonitoring = "First Time Check Mark For Transfusion Monitoring|15| |True";
            public const string LblCaptionCheckMarkForTransfusionMonitoring = "Label Caption Check Mark For Transfusion Monitoring (Separate by [,])|-| |True";

            public const string IsBypassBloodCrossMatching = "IsBypassBloodCrossMatching? (Yes/No)|Yes| |True";
            public const string IsNeedBloodSample = "Is Need Blood Sample (Blood Bank Modul)? (Yes/No)|No| |True";
            public const string IsNeedSpecimenOnJo = "Is Need Specimen On Job Order? (Yes/No)|No;YBRSGKP:Yes| |True";
            public const string IsPrescriptionReviewActived = "Is Prescription Review Actived? (Yes/No)|No;RSMM:Yes| |True";
            public const string IsByPassEmrUserTypeRestriction = "Is ByPass EMR User Type Restriction? (Yes/No)|No| |True";
            public const string NormalTemperature = "Normal Temperature|37.5| |True";
            public const string DeadlineEdited = "Standard Deadline Edited|24| |True";
            public const string PatientHandOverFormID = "Patient Hand Over Question Form ID (utk report asesmen)|PATIENTTRF| |True";
            public const string GMT = "Greenwich Mean Time (GMT)|7| |True";
            public const string IsMandatoryRegNoOnServiceUnitBooking = "Is Mandatory RegNo On Service Unit Booking? (Yes/No)|No| |True";
            public const string IsDisplayRegNoOnServiceUnitBooking = "Is Display RegNo On Service Unit Booking? (Yes/No)|No;RSMM:Yes| |True";
            public const string IntervalOrderWarning = "Duplicate Order Warning Interval In Hour? |0| |False";
            public const string IntervalTrainingEvaluationSchedule = "Interval Training Evaluation Schedule (in month, calculated from the start date of training)|6| |False";
            public const string PrescriptionQueueStdiItemID = "Stdi ItemID for prescription queue || |False";
            public const string MultipleForRewardPoints = "Multiple For Reward Points (Rp.) |250000| |False";
            public const string MultipleForRewardPointsForEmployee = "Multiple For Reward Points For Employee (Rp.) |50000| |False";
            public const string RewardPointsForPatientGeneral = "Reward Points For Patient General |5| |False";
            public const string RewardPointsForPatientGuarantee = "Reward Points ForP atient Guarantee |3| |False";
            public const string ReservationMaxDuration = "Reservation Max Duration In Hour? |3| |False";
            public const string ReservationMaxDurationForInternal = "Reservation Max Duration (for Inpatient) In Day? |7| |False";
            public const string DischargeMethodRefer = "Discharge Method Refer (split with [,])|I02| |False";
            public const string DischargeMethodInCare = "Discharge Method In Care (split with [_]) |O02_O03_E10_E02_E14| |False";
            public const string ItemIdBloodCrossMatching = "Item ID for Blood Cross Matching || |False";
            public const string IsShowExternalQueue = "Is Show External Queue? (Yes/No)|Yes| |True";
            public const string ProgramIdPatientLabel = "Program ID for Patient Label |SLP.01.0008| |False";
            public const string ProgramIdPrintUddEtiquette = "Program ID Print Udd Etiquette (Pharmaceutical Care >> Medication Setup Status) || |False";
            public const string ProgramIdPrintSurgeryCostEstimation = "Program ID Print Surgery Cost Estimation (Verification & Finalize Billing >> Tab Interim Bill List)|| |False";
            public const string ProgramIdPrintSurgeryBilling = "Program ID Print Surgery Billing (Verification & Finalize Billing)|| |False";
            public const string ProgramIdPrintDistributionReceipt = "Program ID Print Direct Distribution Receipt|| |False";
            public const string ProgramIdPrintStockAdjustmentReceipt = "Program ID Print Direct Stock Adjustment Receipt|| |False";
            public const string ProgramIdPrintJobDescription = "Program ID Print Job Description (Employee Position)|| |False";
            public const string ProgramIdPrintExamOrderOtherResult = "Program ID Print Exam Order Other Result|XML.02.0027| |False";
            public const string PorBaseSalesDay = "POR Automated Request Base Sales Day COunt|30| |False";
            public const string PorForStockDay = "POR Automated Request For Stock Day Count|7| |False";
            public const string IsValidateDiagnosisOnRealizationOrderOp = "Is Validate Diagnosis On Realization Order for Outpatient? (Yes/No)|Yes| |False";
            public const string IsFoodSelectedByType = "Is Food Selected By Type? (Yes/No)|No| |True";
            public const string PrescriptionOrderSlipID = "Prescription Order Slip ID || |False";
            public const string IsPrescriptionMustVerifyByDpjp = "Is Prescription Must Verify By Dpjp ? (Yes/No)|No| |False";
            public const string IsPrescriptionNonIPMustDiagnoseMainFirst = "Is Add Prescription Non IP Must Diagnose Main First ? (Yes/No)|No| |False";
            public const string IsPaymentReceiveAllowBackdated = "Is Payment Receive Allow Backdated? (Yes/No)|No| |False";
            public const string ExcessPaymentAmount = "Excess Payment Amount|0.00| |False";
            public const string IsAllowExcessPaymentAmountPlus = "Is Allow Excess Payment Amount Plus? (Yes/No)|No;RSSM:Yes| |True";
            public const string IsFeeCalculateProporsionalOnPayment = "Is Fee Calculate Proporsional On Payment? (Yes/No)|No| |False";
            public const string IsFeeEnableRemunByGuarantor = "Is Fee Enable Remun By Guarantor? (Yes/No)|No;RSTJ:Yes| |False";
            public const string IsFeeEnableDualBruto = "Is Fee Enable Dual Bruto? (Yes/No)|No;RSYS:Yes| |True";
            public const string IsFeeTaxProgressiveMonthly = "Is Fee Tax Progressive Monthly? (Yes/No)|Yes| |True";
            public const string IsDemo = "Is Demo Purpose Only? (Yes/No)|No| |True";
            public const string TmpFolder = "Temporary folder for save temp Document ?|C:\\TMP| |True";
            public const string IsAntibioticRestriction = "Is Antibiotic Use Restriction? (Yes/No)|No| |True";
            public const string AntibioticRestrictionForLine = "Antibiotic Restriction for Antibiotic Line|| |False";
            public const string FoodGroupOneCarbohydrate = "Food Group : Carbohydrate|I| |True";
            public const string FoodGroupOneDishMeal = "Food Group : One Dish Meal|IX| |True";
            public const string IsDisableInventoryStatusOnEditItemProduct = "Is Disable Inventory Status On Edit Item Product? (Yes/No)|No;RSPM:Yes| |True";
            public const string IsOvertimeUseApprovalLevel = "Is Use Approval Level for Overtime? (Yes/No)|Yes| |True";
            public const string IsEmployeeLeaveUseTwoApprovalLevel = "Is Use 2 Approval Level for Employee Leave? (Yes/No)|No;RSMM:Yes| |True";
            public const string EmployeeLeaveApprovalLevel = "Employee Leave Approval Level (select one: 1, 2 or 3)|1;RSYS:3| |True";
            public const string IsUsingPreceptorAsProfessionalIndirectSupervisor = "Is Employee Leave Approval Using Preceptor as Professional Indirect Supervisor? (Yes/No: Leader Organization Unit)|No;RSYS:Yes| |True";
            public const string IsEmployeeLeavePayCutVisible = "Is Employee Leave Pay Cut Visible? (Yes/No)|No;YBRSGKP:Yes| |True";
            public const string ParamedicTypeDoctors = "Paramedic Type Doctors (Separate by [,])|ParamedicType-001,ParamedicType-002,ParamedicType-010,ParamedicType-011| |False";
            public const string ItemGroupKitchen = "Item Group Kitchen (Separate by [,])|| |False";
            public const string IsVisibleItemGroupOnTx = "Is Visible Item Group On Tx? (Yes/No)|No| |True";
            public const string IsCollapsedPatientInformationOnBilling = "Is Collapsed Patient Information On Billing Verification? (Yes/No)|Yes;YBRSGKP:No| |True";
            public const string IsCollapsedTransactionFilterOnBilling = "Is Collapsed Transaction Filter On Billing Verification? (Yes/No)|No;YBRSGKP:Yes| |True";
            public const string IsAutoChargeBedBasedOnDischargeDate = "Is Auto Charge Bed Based On Discharge Date? (Yes/No)|Yes| |False";
            public const string IsCafeAutoPrintPaymentReceive = "Is Cafe Auto Print Payment Receive? (Yes/No)|Yes| |False";
            public const string AppProgramCafePaymentReceive = "App Program Cafe Payment Receive|| |False";
            public const string DistributionRequestBasedOnLocationToRestriction = "Distribution Request Based On Location To Restriction (Separate by [,])|| |False";
            public const string PurcOrderItemTypeRestrictionForItemSupplier = "Purchase Order Item Type Restriction For Filter Item By Supplier (Separate by [,])|;YBRSGKP:11| |True";
            public const string IsRegistrationTracerToAllReg = "Is Registration Tracer To All Registration (New & Old patient)? (Yes/No)|No;RSUI:Yes;RSPM:Yes;RSSMCB:Yes| |False";
            public const string RegistrationTracerToAllRegTypeExc = "Registration Tracer To All Registration Type Exception (IPR/OPR/EMR/MCU)|;GRHA:MCU;RSSMCB:IPR| |False";
            public const string IsValidatedSpecimenOnOrderRealization = "Is Validated Specimen On Order Realization? (Yes/No)|No| |False";
            public const string IsReadOnlyDiscountOnPrescription = "Is Read Only Discount On Prescription? (Yes/No)|Yes| |True";
            public const string IsVisibleBtnPurcReqOnDistribution = "Is Visible BtnPurcReq On Distribution? (Yes/No)|No| |True";
            public const string IsPurchaseRequestBasedOnItemsPerLocation = "Is Purchase Request Based On Items Per Location? (Yes/No))|No;YBRSGKP:Yes| |True";
            public const string IsPurchaseRequestBasedOnItemCategory = "Is Purchase Request Based On Item Category? (Yes/No))|Yes;YBRSGKP:No| |True";
            public const string IsProcurementForItemMedicBasedOnInvCategory = "Is Procurement For Item Medic Based On Inventory Category? (Yes/No))|No| |False";
            public const string IsProcurementForItemNonMedicBasedOnInvCategory = "Is Procurement For Item Non Medic Based On Inventory Category? (Yes/No))|Yes| |False";
            public const string IsProcurementForItemKitchenBasedOnInvCategory = "Is Procurement For Item Kitchen Based On Inventory Category? (Yes/No))|No| |False";
            public const string IsAllowDiscountOnTransEntry = "Is Allow Discount On Transaction Entry? (Yes/No))|Yes;YBRSGKP:No| |True";
            public const string IsTariffComponentPriceVisibleForBilling = "Is Tariff Component Price Visible For Billing? (Yes/No)|Yes| |True";
            public const string IsPrintPatientCardOnNewBornInfant = "Is Print Patient Card On New Born Infant? (Yes/No)|No| |False";
            public const string IsRegistrationInpatientOnlyForNewBornInfant = "Is Registration Inpatient Only For New Born Infant? (Yes/No)|No;YBRSGKP:Yes| |False";
            public const string IsAdditionalMealOrderUsedClassMenuStandard = "Is Additional Meal Order Used Class Menu Standard? (Yes/No)|Yes| |False";
            public const string IsAutoTransfusionBillProceedOnBloodDistribution = "Is Auto Transfusion Bill Proceed On Blood Distribution? (Yes/No: On Tranfusion Monitoring)|Yes| |False";
            public const string IsVisibleRequestTypeOnPurchaseRequestPicklist = "Is Visible Request Type On Purchase Request Picklist? (Yes/No)|No| |True";
            public const string IsCreateZipCodeIdAutomatic = "Is Create Zip Code ID Automatic? (Yes/No)|No| |True";
            public const string IsBridgingBillingBpjs = "Is Bridging Billing BPJS? (Yes/No)|Yes| |False";
            public const string IsBridgingBillingBpjsWithCostSharing = "Is Bridging Billing BPJS With Cost Sharing (Charge Class VIP/SVIP with Coverage Class I: compare [75% from Plafond] Vs [Total Billing - Plafond], take the lowest value)? (Yes/No)|No;RSTJ:Yes| |False";
            public const string IsConsignmentReceivedItemBySupplier = "Is Consignment Received Item By Supplier? (Yes/No)|No;YBRSGKP:Yes| |True";

            public const string IsPrescriptionIprMustAssessmentFirst = "Is add Prescription for InPatient Reg Must Assessment First ? (Yes/No)|No;YBRSGKP:Yes| |False";
            public const string IsPrescriptionOprMustAssessmentFirst = "Is add Prescription for OutPatient Reg Must Assessment First ? (Yes/No)|No;YBRSGKP:Yes| |False";
            public const string IsPrescriptionEmrMustAssessmentFirst = "Is add Prescription for Emergency (IGD) Reg Must Assessment First ? (Yes/No)|No;YBRSGKP:Yes| |False";
            public const string IsExamOrderIprMustAssessmentFirst = "Is add Exam Order for InPatient Reg Must Assessment First ? (Yes/No)|No;YBRSGKP:Yes| |False";
            public const string IsExamOrderOprMustAssessmentFirst = "Is add Exam Order for OutPatient Reg Must Assessment First ? (Yes/No)|No;YBRSGKP:Yes| |False";
            public const string IsExamOrderEmrMustAssessmentFirst = "Is add Exam Order for Emergency (IGD) Reg Must Assessment First ? (Yes/No)|No;YBRSGKP:Yes| |False";

            public const string IsAllowCopyPrescOther = "Is Allow Copy Prescription from Other Paramedic ? (Yes/No)|Yes;YBRSGKP:No| |False";
            public const string DefaultGuarantorKiosk = "Default Guarantor ID for Kiosk Registration (leave empty for default SELF)|;YBRSGKP:SELF.RJ| |False";
            public const string RegistrationTypeOuterEtiquettePrintRestrictions = "Registration Type Outer Etiquette Print Restrictions (IPR/OPR/EMR/MCU)|IPR,OPR,EMR,MCU;YBRSGKP:IPR,EMR| |False";
            public const string AjaxCounter = "Ajax Counter untuk refresh client browser|0| |False";
            public const string ServiceUnitRadiologyIDs = "List ServiceUnitID Radiologi gunakan sep ; (titik koma) jika lebih dari 1|| |False";
            public const string IsAutoPrintSEP = "Is Auto Print S.E.P.? (Yes/No)|Yes| |False";
            public const string IsAllowSkipAutoBillOnRegistrationOpr = "Is Allow Skip Auto Bill On Registration Opr? (Yes/No)|No| |True";
            public const string EmptyDoctorId = "Empty Doctor ID (separated by comma (,))|| |False";
            public const string DoctorOnDutyId = "Doctor On Duty ID (Emergency Room)|| |False";
            public const string IsUsingRisPacsInterop = "Is Using RIS / PACS Interop? (Yes/No)|No;YBRSGKP:Yes| |False";
            public const string IsHideConfidentialLabResult = "Is hide Confidential Lab Result? (Yes/No)|No;YBRSGKP:Yes| |False";
            public const string IsAllowSubstituteDoctorOnRegistrationOpr = "Is Allow Substitute Doctor On Registration Opr? (Yes/No)|No| |True";
            public const string IsVisibleTrProcedureOnBookingRealization = "Is Visible TrProcedure On Booking Realization? (Yes/No)|No;YBRSGKP:Yes| |True";
            public const string IsUsingMappingServiceUnitProcedure = "Is Using Mapping Service Unit Procedure? (Yes/No)|No;RSSTJ:Yes| |True";
            public const string IsPrescOrderHandlingBasedOnDispensary = "Is Prescription Order Handling Based On Dispensary? (Yes/No)|No;YBRSGKP:Yes| |True"; //if no then based on registration type
            public const string IsAllowDirectPrescOnInpatientSalesHandling = "Is Allow Direct Prescription On Inpatient Sales Handling? (Yes/No)|Yes;YBRSGKP:No| |True";
            public const string IsShowRegConsulOnVerificationBilling = "Is Show Registration Consul On Verification Billing? (Yes/No)|No;RSMM:Yes| |True";
            public const string IsCreateFoodIdAutomatic = "Is Create Food Id Automatic? (Yes/No)|No| |True";
            public const string IsVisibleAllAppointmentStatusOnList = "Is Visible All Appointment Status On List? (Yes/No)|No;RSUI:Yes| |False";
            public const string IsNeedVoidReasonOnPaymentReceive = "Is Need Void Reason On Payment Receive? (Yes/No)|No;RSUI:Yes| |False";
            public const string IsVisibleKwi = "Is Visible KWI? (Yes/No)|No| |True";
            public const string IsUsingDoubleEmployeeNo = "Is Using Double EmployeeNo? (Yes/No)|No| |True";
            public const string IsSeparateScheduleAndAttendanceSheet = "IsSeparateScheduleAndAttendanceSheet? (Yes/No)|No;RSMP:Yes| |True";
            public const string ServiceUnitPathologyAnatomyID = "Service Unit Pathology Anatomy ID|| |False";
            public const string ServiceUnitCssdID = "Service Unit CSSD ID|| |False";

            public const string ApplicationDocumentFolder = "Application Document Folder for save file Attachment ?|| |false";
            public const string EmployeeDocumentFolder = "Employee Document Folder for save file Attachment ?|| |false";
            public const string TmpDocumentFolder = "Tmp Document Folder for Import from Excel|| |false";
            public const string PerformancePlanDocumentFolder = "Performance Plan (Renkin) Document Folder for save file Attachment ?|| |false";
            public const string SoundFolder = "Save Path File|| |false";
            public const string SoundFolderURL = "Save Path File URL|| |false";
            public const string IsShowScanDocumentConfirm = "Show confirm Upload / Scan Additional Document after save new Registration ? (Yes/No)|No| |false";
            public const string IsShowPrintLabelOnTransEntry = "Show Print Patient Sticker On Trans Entry? (Yes/No)|No;RSSMCB:Yes;RSBK:Yes| |false";
            public const string AppProgramServiceUnitPatientLabel = "AppProgram for Service Unit Patient Label? (Yes/No)|;RSBK:XML.YBRS.01.032| |true";
            public const string IsPrescriptionShowStock = "Prescription Entry Showing Stock in item selection (EMR)? (Yes/No)|Yes;RSBK:No| |false";
            public const string IsRecipeMarginValueForEachItemCompound = "Is Recipe Margin Value Applied To Each Item Compound? (Yes/No)|No;RSBK:Yes| |true";

            public const string WebCamWidth = "WebCam live preview and capture width (Default 800)|800| |false";
            public const string WebCamHeight = "WebCam live preview and capture height (Default 600)|600| |false";
            public const string WebCamMaxWidth = "WebCam maximum resolution width (Default 2592)|2592| |false";
            public const string WebCamMaxHeight = "WebCam maximum resolution height (Default 1944)|1944| |false";
            public const string WebCamIdCardWidth = "WebCam capture ID Card crop width (Default 800)|800| |false";
            public const string WebCamIdCardHeight = "WebCam capture ID Card crop height (Default 600)|600| |false";

            public const string IsAutobillIprActivated = "Is Autobill IPR Activated? (Yes/No)|No;RSBK:Yes| |true";
            public const string NonInPatientBpjsPlafond = "Non InPatient Bpjs Plafond|10000| |false";
            public const string IsThrIncludeInWageProcess = "Is THR Include In Wage Process? (Yes/No)|No;RSMP:Yes| |true";
            public const string IsAllowExecutionDateForward = "Is Allow Execution Date Forward? (Yes/No)|No;RSUI:Yes| |true";
            public const string PrescriptionCategoryHomePresID = "Prescription Category Home Prescription ID?|DISC| |false";

            public const string IsAutoChargeBedFilterLock = "Is Auto Charge Bed Filter Lock? (Yes/No)|No;RSBK:Yes| |false";
            public const string IsEklaimGroupUsingDefaultValue = "Is E-Klaim Group Using Default Value? (Yes/No)|Yes;RSI:No| |false";
            public const string IsAutoInsertRegistrationNoteFromRegistration = "Is Auto Insert Registration Note From Registration? (Yes/No)|No| |false";
            public const string MedicationWillOutOfBalanceInDay = "Medication will out of balance in day?|1| |false";
            public const string RisPacsInteropVendor = "Ris Pacs Interop Vendor|;RSUI:INTIWID;RSBK:INTIWID;RSRG:INTIWID;RSPM:NOVAWEB;RSUI:NOVAWEB| |false";
            public const string IsUsingValidationOnServiveUnitBooking = "Is Using Validation On Servive Unit Booking? (Yes/No)|No| |false";
            public const string IsAssetDepreciationCreateByAccounting = "Is Asset Depreciation Create By Accounting? (Yes/No)|No;YBRSGKP:Yes| |true";
            public const string IsOpenAntrianBridging = "Open Antrian Bridging (BPJS JKN)? (Yes/No)|No;EMC:Yes| |true";
            public const string IsDistributionOnlyBasedOnRequest = "Is Distribution Only Based On Request? (Yes/No)|No;RSMP:Yes| |true";
            public const string IsDistributionRequestOnlyForUnderMinValue = "Is Distribution Request Only For Under Min Value? (Yes/No)|No;RSMP:Yes| |true";
            public const string IsDistributionRequestMustNotExceedCWStock = "Is Distribution Request Must Not Exceed Central Warehouse Stock? (Yes/No)|No;RSMP:Yes| |true";
            public const string ServiceUnitSanitationId = "Service Unit Sanitation Id|| |false";
            public const string ServiceUnitPurchasingId = "Service Unit Purchasing ID for PO Asset validation (split with [,])|;RSI:D4.0.05| |false";
            public const string IsUsingSingleUnitIPSRS = "Is Using Single Unit IPSRS (Sanitasi=IPSRS)? (Yes/No)|No;RSBK:Yes| |false";
            public const string WorkOrderRealizationAutoGenerateTx = "Work Order Realization Auto Generate Transaction: (PR/DR/IR)|DR| |true";
            public const string IsUsingCentralizedPurchaseRequest = "Is Using Centralized Purchase Request? (Yes/No)|Yes| |true";
            public const string ItemUnitKg = "Item Unit ID : KILOGRAM|KG| |false";
            public const string IsRptInPreviewMode = "Report Viewer Mode? (Yes/No)|Yes;EMC:No| |false";
            public const string IsNsOutcomeShowScale = "Report Viewer Mode? (Yes/No)|No;YBRSGKP:Yes| |false";
            public const string EmployeeIncidentTypeNSI = "Employee Incident Type ID for Needle Stick Injury (NSI)|03| |false";
            public const string EmployeeIncidentTypeEBF = "Employee Incident Type ID for Exposed to Body Fluid (separated by comma (,))|03,04| |false";
            public const string NeedleTypeNSI = "Needle Type for NSI (separated by comma (,))|2,3| |false";
            public const string EmployeeStatusActive = "Employee Status : Active|1| |false";
            public const string EmployeeRelationshipSelf = "Employee Relationship : Self |Relationship-000| |false";
            public const string EmployeeLeaveAnnualLeave = "Employee Leave : Annual Leave (Cuti Tahunan)|01| |false";
            public const string EmployeeAnnualLeaveStartPeriod = "Employee Annual Leave Start Period (with format : mm/dd/)|01/01/| |false";
            public const string EmploymentTypePermanent = "Employment Type: Permanent|1| |false";
            public const string EmploymentTypeForAnnualLeave = "Employment Type For Annual Leave (separated by comma (,))|1| |false";
            public const string PersonalLicenseTypeSTR = "Personal License Type: STR|;YBRSGKP:002| |false";
            public const string PersonalLicenseTypeSPK = "Personal License Type: SPK|;YBRSGKP:004| |false";
            public const string IsAllowEditEmployeeAnnualLeaveEndPeriod = "Is Allow Edit Employee Annual Leave End Period? (Yes/No)|No;YBRSGKP:Yes| |true";
            public const string IsUsingBKUModule = "Is Using BKU Module? (Yes/No)|No| |true";
            public const string DeadlineMedicalRecordEditableAfterDischarge = "Deadline Medical Record Editable After Discharge (Hour)|48| |false|Edit this data not allowed because this patient has discharge over than {0} hour ago, please contact IT";
            public const string DeadlineMedicalRecordAddableAfterDischarge = "Deadline Medical Record Addable After Discharge (Hour)|24| |false|Add data not allowed because this patient has discharge over than {0} hour ago, please contact IT";
            public const string DefaultValueSpecimenTakenBy = "Exam Order default value specimen taken by|lab| |false";
            public const string IsUserTypeDoctorNoSaveConfirm = "Is User Type Doctor No Need Save Confirm|No| |false";
            public const string IsCentralizedCssd = "Is Centralized CSSD? (Yes/No)|No;;RSISB:Yes| |true";
            public const string IsShowSystemQtyInCssdStockTacking = "Is Show System Qty In CSSD Stock Tacking? (Yes/No)|No;RSISB:Yes| |true";
            public const string IsShowSearchMenu = "Is Show Search Menu? (Yes/No)|No;YBRSGKP:Yes| |true";
            public const string NsOutcome = "Nursing Care Outcome Label|Outcomes;YBRSGKP:Outcomes (SLKI)| |true";
            public const string NsOutcome02 = "Midwifery Care Outcome Label|Immediate Actions;YBRSGKP:Outcomes| |true";
            public const string NsIntervention = "Nursing Care Intervention Label|Interventions;YBRSGKP:Interventions (SIKI)| |true";
            public const string NsSymptom = "Nursing Care Sysmtom Label|dibuktikan dengan| |false";
            public const string NsIsShowDiagnosaCode = "Nursing Care Is Show Diagnosis Code? (Yes/No)|Yes| |false";
            public const string AppraisalVersionNo = "Appraisal Version No. (2: RSMM; 3:YBRSGKP)|2;YBRSGKP:3| |true";
            public const string IsGenericMustEqualZatActive = "Is Generic Must Equal Zat Active (Item Product Medic Master)|No;YBRSGKP:Yes| |true";
            public const string IsUsingFourLevelOrganizationUnit = "Is Using Four (4) Level Organization Unit? (Yes/No)|No;YBRSGKP:Yes| |True";
            public const string IsFilterVehicleAndDriverOnScheduled = "Filter Vehicle And Driver On Scheduled When Choosing the Driver and the Vehicle|No;| |false";
            public const string EmployeeTypeForLogbook = "Employee Type For Logbook (Example: Perawat, Bidan; separated by comma (,))|02| |false";
            public const string IsKioskEnableBPJS = "Is Kiosk Enable BPJS?|No;| |false";
            public const string IsKioskEnableQRCode = "Is Kiosk Enable QRCode Scanner?|No;| |false";
            public const string CssdSenderBySelf = "CSSD Sender By : Self (Cssd)|01| |false";
            public const string CssdSenderByOtherUnit = "CSSD Sender By : Other Unit|02| |false";
            public const string CssdStockOpnameRowPerPage = "CSSD Stock Opname Row Per Page|30| |true";
            public const string IsUsingEmployeeNeedleStickInjuryFollowUp = "Is Using Employee Needle Stick Injury FollowUp? (Yes/No)|No| |true";
            public const string IsAllowVoidServiceUnitBookingRealization = "Is Allow Void Service Unit Booking Realization? (Yes/No)|No;RSI:Yes| |true";
            public const string IsValidateEdOnDistribution = "Is Validate Expired Date On Distribution? (Yes/No)|No;RSTJ:Yes| |true";
            public const string DayLimitValidationServiceUnitBooking = "Day Limit Validation Service Unit Booking (in day)|90| |false";
            public const string IsAutomaticChargeBedReprocessIncludeAutoBillItem = "Is Automatic Charge Bed Reprocess Include Auto Bill Item|No| |false";
            public const string IsAccWriteDownPaymentOnReceivingInvoice = "Is Accounting Write Purcashing Down Payment On Receiving Invoice? (Yes/No)|Yes;YBRSGKP:No| |true";
            public const string BudgetOfAssetNeedExtraApprovalLimit = "Limit mininum of approval of budgeting of asset?|0;YBRSGKP:100000000| |false";
            public const string IsParamedicFeePaymentEnableDraft = "Is Paramedic Fee Payment Enable Draft?|Yes;YBRSGKP:No| |false";
            public const string IsParamedicFeePaymentEnableGuaranteeFee = "Is Paramedic Fee Payment Enable Guarantee Fee?|No| |false";
            public const string IsAllPhysicianAllowEditMedicalDischarge = "Is All Physician Allow Edit Medical Discharge Summary?|No;RSTJ:Yes| |false";
            public const string IsShowCrossMatchingPrintLabel = "Is Show Cross Matching Print Label? (Yes/No)|No;RSTJ:Yes| |false";
            public const string MaxLosToDisplayTransactionList = "Max LOS To Display Transaction List On Billing Verification (in days)|90| |false";
            public const string DiscountReasonSelisihKlaimBpjs = "Discount Reason : Selisih Klaim BPJS|DiscountReason-011| |false";
            public const string IsPatientBpjsNoMandatory = "Is Patient BPJS No Mandatory? (Yes/No)|No;RSTJ:Yes| |true";
            public const string SOPDirectoryUrl = "Directory URL of Standard Operation Procedures|../sop/| |false";
            public const string IsUsingLimitQuotaInPhysicianSchedule = "Is Using Limit Quota In Physician Schedule? (Yes/No)|No;RSTJ:Yes| |true";
            public const string IsPathologyAnatomyDiagnoseFreeText = "Is Pathology Anatomy Diagnose Free Text? (Yes/No)|No;RSTJ:Yes| |true";
            public const string IsPathologyAnatomyLocationFreeText = "Is Pathology Anatomy Location Free Text? (Yes/No)|No;RSTJ:Yes| |true";
            public const string IsPathologyAnatomyWithImpressionResult = "Is Pathology Anatomy WithImpressionResult? (Yes/No)|Yes;RSTJ:No| |true";
            public const string IsPathologyAnatomyIhkWithMammaeResult = "Is Pathology Anatomy - Immunohistochemistry With Mammae Result? (Yes/No)|Yes;RSTJ:No| |true";
            public const string IsPathologyAnatomyWithTestResult = "Is Pathology Anatomy Result Using Form Test Result? (Yes/No)|No;RSUSKY:Yes| |true";
            public const string DayLimitDefaultDiagAndProcList = "Day Limit (H- Registration/Discharge Date) Default Diag And Proc Record List (in day)|3| |false";
            public const string AppointmentGetListDateRangeLimit = "Appointment Get List Date Range Limit (in day)|31| |false";
            public const string IsAllowDoubleItemServiceOnTxEntry = "Is Allow Double Item Service On Transaction Entry? (Yes/No)|No;RSTJ:Yes| |false";
            public const string IsVisiblePrintBillingPaymentPermit = "Is Visible Print Billing Payment Permit? (Yes/No)|No| |true";
            public const string JournalEntrySearchRangeFilter = "Journal Entry Search RangeFilter (0:All,1:ThisMonth,2:OneLastMonth,3:ThisYear,4:OneLastYear)|0| |False";
            public const string IsAccountReceivableByDischargeDate = "Is Journal Patient Receivable (Account Receivale) based on DischargeDate? (Yes/No)|No;YBRSGKP:Yes| |False";
            public const string IsValidateMaxQtyItemConsumptions = "Is Validate Max Qty Item Consumptions? (Yes/No)|No| |False";
            public const string IsEnabledDispensaryOnPrescriptionOrderRealization = "Is Enabled Dispensary On Prescription Order Realization? (Yes/No)|No;RSTJ:Yes| |True";
            public const string IsUsingFactoryInTheItemProcurementProcess = "Is Using Factory In The Item Procurement Process (PO)? (Yes/No)|No;RSSTJ:Yes| |True";
            public const string IsVisibleOtc = "Is Visible OTC? (Yes/No)|Yes;RSTJ:No| |True";
            public const string IsVisibleGuarantorAutoBillItem = "Is Visible Guarantor Auto Bill Item Setting? (Yes/No)|No| |True";
            public const string IsJobOrderRealizationListByOrderDate = "Is Job Order Realization List By Order Date? (Yes/No)|Yes;RSPM:No| |True";
            public const string IsInventoryIssueListByTransactionDate = "Is Inventory Issue List By Transaction Date? (Yes/No)|Yes;RSYS:No| |True";
            public const string IsARPaymentShowRemaining = "Is AR Payment Show Remaining? (Yes/No)|No;YBRSGKP:Yes| |False";
            public const string IsClosingApAdvanceWithPayment = "Is Closing AP Advance With Payment? (Yes/No)|No;RSBK:Yes| |False";
            public const string IsClosingApZeroWithPayment = "Is Closing AP Zero With Payment? (Yes/No)|No| |True";
            public const string IsUsingNewDuplicatePatientDataChecking = "Is Using New Duplicate Patient Data Checking? (Yes/No)|No;RSI:Yes| |False";
            public const string IsUsingUserAccessForEditPatient = "Is Using User Access For Edit Patient? (Yes/No)|No;RSUI:Yes| |True";
            public const string IsSeparateLaboratoryUnit = "Is Separate Laboratory Unit? (Yes/No)|No;RSTJ:Yes| |True";
            public const string IsAncillaryServicePhysicianSenderFreeText = "Is Ancillary Service Physician Sender Free Text? (Yes/No)|No;RSTJ:Yes| |True";
            public const string IsCreateReferralGroupIdAutomatic = "Is Create Referral Group Id Automatic? (Yes/No)|No;RSI:Yes| |True";
            public const string IsCreateReferralIdAutomatic = "Is Create Referral Id Automatic? (Yes/No)|No;RSI:Yes| |True";
            public const string IsApInvoiceIncPPN = "Is Ap Invoice Inc PPN? (Yes/No)|Yes;RSTJ:No| |False";
            public const string IsRegistrationLinkToPatientDocument = "Is Registration Link To Patient Document? (Yes/No)|No;RSUI:Yes| |True";
            public const string IsUsingValidationImplementationDateTimeOnPpaNotes = "Is Using Validation Implementation DateTime On PPA Notes (must not exceed the system date)? (Yes/No)|No;RSPM:Yes| |False";
            public const string IsAutoCreateNewPrescriptionTxOnUnapproval = "Is Auto Create New Prescription Transaction On Unapproval? (Yes/No)|Yes| |False";
            public const string IsUsingValidationOnServiceUnitBookingRealization = "Is Using Validation On Service Unit Booking Realization? (Yes/No)|No;RSTJ:Yes| |False";
            public const string IsEmrListUsingExternalQueNo = "Is EMR List Using External Que No? (Yes/No)|Yes;RSI:No| |True";
            public const string HaisMonitoringProgramName = "Hais Monitoring Program Name (select one : AVC, NAT, or INT)|AVC;RSISB:INT| |True";
            public const string AppointmentTypeControlPlan = "Appointment Type : Control Plan|CP| |True";
            public const string AppointmentTypeWebService = "Appointment Type : Web Service|WS| |True";
            public const string IsItemPickerListOrderByName = "Is Item Picker List Order By Name? (Yes/No)|No| |False";
            public const string IsUsingProcurementTypeInPO = "Is Using Procurement Type In PO? (Yes/No)|No;RSTJ:Yes| |True";
            public const string IsFilterPrescUddListOnlyWithValidTx = "Is Filter Presc. UDD List Only With Valid Tx? (Yes/No)|Yes| |True";
            public const string IsPathologyAnatomyResultTypeCanBeMoreThanOne = "Is Pathology Anatomy Result Type Can Be More Than One? (Yes/No)|No;RSTJ:Yes| |True";
            public const string IsAllowEditPorAmountOnApInvoice = "Is Allow Edit POR Amount On A/P Invoice? (Yes/No)|No| |True";
            public const string IsCanProcessExceededRequestOnInventoryIssueOut = "Is Can Process Exceeded Request On Inventory Issue Out? (Yes/No)|No;RSBK:Yes| |True";
            public const string IsUsingItemSubGroup = "Is Using Item Sub Group? (Yes/No)|No| |True";
            public const string ExcelFileExtension = "Excel File Extension|.xlsx| |True";
            public const string IsReadonlyStockQtyOnTransChargesItem = "Is Readonly Stock Qty On TransChargesItem? (Yes/No)|No;RSPM:Yes| |True";
            public const string EmailSender = "Email Sender|IT Department| |False";
            public const string IsNeedVoidReasonOnArInvoicing = "Is Need Void Reason On A/R Invoicing? (Yes/No)|No;RSUI:Yes| |True";
            public const string IsJobOrderRealizationListByCitoStatus = "Is Job Order Realization List By Cito Status? (Yes/No)|No;RSSMCB:Yes| |True";
            public const string IsShowPrescriptionHistoryOnRegistration = "Is Show Prescription History On Registration? (Yes/No)|No;RSEE:Yes;SCI:Yes| |True";
            public const string IsUsingGoogleForm = "Is Using Google Form? (Yes/No)|Yes| |True";
            public const string IsVisibleGuarantorFilterOnPlafondInformationList = "Is Visible Guarantor Filter On Plafond Information List? (Yes/No)|No;RSI:Yes| |True";
            public const string IsVisibleClinicalDiagnosisOnJobOrderRealization = "Is Visible Clinical Diagnosis On Job Order Realization? (Yes/No)|No;RSTJ:Yes| |True";
            public const string IsVisible23PrescFilterOnPlafondInformationList = "Is Visible 23 Days Prescription Filter On Plafond Information List? (Yes/No)|No;RSI:Yes| |True";
            public const string IsVisibleTemplateForDirectPrescription = "Is Visible Template For Direct Prescription? (Yes/No)|No| |True";
            public const string IsMandatoryConsTime = "Is Mandatory Cons. Time? (Yes/No)|No;RSI:Yes| |True";
            public const string IsUsingCheckListForMatrixServiceUnitItemService = "IsUsingCheckListForMatrixServiceUnitItemService? (Yes/No)|No;RSYS:Yes| |True";
            public const string DiscountReasonBillRounding = "Discount Reason : Bill Rounding (Diskon Pembulatan Tagihan A/R)|-;YBRSGKP:DiscountReason-013| |False";
            public const string IsPrescriptionDiscountAfterRounding = "Is Prescription Discount After Rounding? (Yes/No)|No;RSBK:Yes| |False";
            public const string IsUsingBillingSlipInEnglish = "Is Using Billing Slip In English? (Yes/No)|No;RSJKT:Yes;SCI:Yes| |True";
            public const string IsFoodSelectedByMenuItemFoodGroup = "Is Food Selected By Menu Item Food Group? (Yes/No)|No;RSYS:Yes;SCI:Yes| |True";
            public const string MenuItemFoodGroupStandard = "Menu Item Food Group : Standard|00| |False";
            public const string IsNeedValidateMobilePhoneNo = "Is Need Validate Mobile Phone No (Patient)? (Yes/No)|No;RSMMP:Yes| |False";
            public const string IsMandatoryDistributionTypeOnDirectDistribution = "Is Mandatory Distribution Type On Direct Distribution? (Yes/No)|No;RSJKT:Yes| |True";
            public const string ItemGroupFisiotherapyID = "Item Group Fisiotherapy ID|-| |False";
            public const string ItemGroupPathologyAnatomyID = "Item Group Pathology Anatomy ID (separated by [,])|| |False";
            public const string IsUsedPrintSlipLogForBillingStatement = "Is Used Print Slip Log For Billing Statement? (Yes/No)|No| |True";
            public const string IsUsedPrintSlipLogForPaymentReceipt = "Is Used Print Slip Log For Payment Receipt? (Yes/No)|No| |True";
            public const string IsUsingAssetIdNewNumberingFormat = "Is Using Asset ID New Numbering Format? (Yes/No)|No;RSSMCB:Yes| |True";
            public const string IsUsingAssetIdNumberingFormatWithSplitCategory = "Is Using Asset ID Numbering Format With Split Category (Asset/Inventory)? (Yes/No)|Yes;RSSMCB:No| |True";
            public const string IsPorTaxBasedOnPo = "Is POR Tax Based On PO? (Yes/No)|No| |False";
            public const string IsUsingItemSubBin = "Is Using Item Sub Bin? (Yes/No)|No| |True";
            public const string IsParamedicFeeVerifPaymentFilterByClosingBilling = "Is Paramedic Fee Verification Payment Date Is Filtered By Closing Billing? (Yes/No)|No;RSTJ:Yes| |False";
            public const string IsAllowEditDateTimeImplementation = "Is Allow Edit DateTime Implementation? (Yes / No)|No;RSMMP:Yes| |False";
            public const string IsShowRealizationOrderTransactionStatus = "Is Show Realization Order Transaction Status? (Yes / No)|No| |True";
            public const string IsUsingValidationUserAccessOnPaymentReceive = "Is Using Validation User Access On Payment Receive? (Yes/No)|No| |True";
            public const string IsUsingValidationPendingBalance = "Is Using Validation Pending Balance? (Yes/No)|No;RSTJ:Yes| |True";
            public const string IsPrescriptionQueueForInpatient = "Is Prescription Queue For Inpatient? (Yes/No)|Yes;RSYS:No| |True";
            public const string IsJobOrderRealizationListWith2Tabs = "Is Job Order Realization List With 2 Tabs? (Yes/No)|No;RSYS:Yes| |True";
            public const string AssessmentPhotoSize = "Assessment Photo Size|400| |False";
            public const string IsShowInfoTotalPatientRegistration = "Is Show Info Total Patient Registration (List Prescription Sales)? (Yes/No)|No;RSYS:Yes| |True";
            public const string IsEmrListShowPlafondProgress = "Is Emr List Show Plafond Progress ? (Yes/No)|No| |False";
            public const string IsDistributionRequestUsingPickFromUsedHistoryV2 = "Is Distribution Request Using Pick From Used History V2? (Yes/No)|No;RSYS:Yes| |False";
            public const string IntervalRefreshPrescriptionOrderList = "Interval Refresh Prescription Order List (in milisecond)|30000;RSYS:6000| |False";
            public const string IsTestResultListWithDefaultOutstanding = "Is Test Result List With Default Outstanding? (Yes/No)|No;RSYS:Yes| |False";
            public const string IsRegistrationListWithCreatedDateTime = "Is Registration List With Created Date/Time? (Yes/No)|No;RSYS:Yes;RSSMCB:Yes| |True";
            public const string IsBillingStatementLosCalculationWithAdd1Day = "Is Billing Statement LOS Calculation With Add 1 Day? (Yes/No)|Yes;RSYS:No| |True";
            public const string IsBillingStatementRegDateUsingCheckinConfirmed = "Is Billing Statement Reg. Date Using Checkin Confirmed? (Yes/No)|Yes;RSYS:No| |True";
            public const string IsPrescriptionUnApprovalCreateNewNumber = "Is Prescription UnApproval Create New Number? (Yes/No)|Yes;RSYS:No| |True";
            public const string IsDistributionRequestPickListWithBalanceToInfo = "Is Distribution Request PickList With BalanceTo Info? (Yes/No)|No;RSYS:Yes| |True";
            public const string IsUsingDefaultConsumeMethodFor23DaysPrescription = "Is Using Default Consume Method For 23Days Prescription? (Yes/No)|No;RSYS:Yes| |True";
            public const string IsPrescriptionSplitBillActived = "Is Prescription Split Bill Actived? (Yes/No)|No;RSYS:Yes| |True";
            public const string IsSharePurchaseDiscToCustomer = "Is Share Purchase Discount To Customer? (Yes/No)|No;RSYS:Yes| |True";
            public const string IsLockLocationPharmacy = "Is Lock Location Pharmacy? (Yes/No)|No;RSYS:Yes| |True";
            public const string IsPatientOprOnPrescSalesForPolyclinicOnly = "Is Patient OPR On Presc. Sales For Polyclinic Only? (Yes/No)|No;RSYS:Yes| |True";
            public const string IsPorUsingChecklistItem = "Is POR Using Checklist Item? (Yes/No)|No;RSYS:Yes| |True";
            public const string IsShowPrintLabel1InJobOrderRealizationList = "Is Show Print Label1 In Job Order Realization List? (Yes/No)|No;RSTJ:Yes;RSJKT:Yes| |True";
            public const string IsShowBalanceInfoInDistributionRequest = "Is Show Balance Info In Distribution Request? (Yes/No)|No;RSYS:Yes| |True";
            public const string GuarantorIdExeptionForRecipeAmount = "Guarantor ID Exeption For Recipe Amount (split with [,])|| |False";
            public const string IsMandatoryPrescriptionCategory = "Is Mandatory Prescription Category? (Yes/No)|No;RSPM:Yes| |False";
            public const string IsMandatoryDrugAllergen = "Is Mandatory Drug Allergen? (Yes/No)|No;RSTJ:Yes| |False";
            public const string DayLimitEmployeeLicenseWarning = "Day Limit Employee License Warning|180| |False";
            public const string IsFeeTaxBeforeDiscount = "Is Fee Tax Before Discount? (Yes/No)|No| |True";
            public const string IsRasproEnable = "Is Antibiotic Suggestion Enable (RASPRO)? (Yes/No)|No| |True";
            public const string RasproEnableForRegistrationTypes = "Raspro Enable For Inpatient and Registration Types (ex. IGD;OPR)|| |false";
            public const string QuestionFormEmployeeSafetyCultureIncidentReports = "Question Form : Employee Safety Culture Incident Reports|KEHRS| |True";
            public const string QuestionFormPatientIdentificationCompliance = "Question Form : Patient Identification Compliance|IMKIP| |True";
            public const string QuestionFormCredentialing = "Question Form : Credentialing|RKK| |True";
            public const string AntibioticMaxConsumeDay = "Standard maximal day consume Antibiotic (default 7)|7| |False";
            public const string acc_IsJournalAssets = "Is Journal Assets (POR)? (Yes/No)|No| |False";
            public const string acc_JournalAssetsAmount = "Journal Assets Amount|10000000| |False";
            public const string acc_AssetInventoryAmountLimit = "Asset Inventory Amount Limit (as Cost for PO asset validation)|500000| |False";
            public const string acc_EconomicLifeInYearLimit = "Economic Life In Year Limit (for PO asset validation)|1| |False";
            public const string acc_AssetDepreciationAmountLimit = "Asset Depreciation Amount Limit|10000000| |False";
            public const string ServiceUnitIdListForUdd = "ServiceUnitId List For Udd Transaction|;RSI:D3.0.01.5;D3.0.01.1| |False";
            public const string IsVoucherListShowVoid = "Is Voucher List Show Void? (Yes/No)|No;RSBK:Yes| |False";
            public const string IsServiceUnitBookingUsingBodyDiagramServiceUnit = "Is Service Unit Booking Using Body Diagram Service Unit? (Yes/No)|No;RSMM:Yes| |True";
            public const string IsUddSetupMustReconFirst = "Is Udd Setup must recon first? (Yes/No)|No| |True";
            public const string IsAutoDeleteBalanceOnInActiveItem = "Is Auto Delete Balance On In Active Item? (Yes/No)|No;RSI:Yes| |False";
            public const string IsUseApprovalLevelforPOWithUserRestriction = "Is Use Approval Level for PO With User Restriction? (Yes/No)|No;RSSMCB:Yes| |True";
            public const string PrefixOnoSysmexInterop = "Prefix ONO Sysmex Interop|;RSUI:TG;RSPM:ST| |True";
            public const string IsItemPickerListOrderUsingGroupButton = "Is Item Picker List Order Using Group Button? (Yes/No)|No;RSSMCB:Yes| |True";
            public const string IsDefaultEmptyDateOnEKlaimList = "Is Default Empty Date On E-Klaim List? (Yes/No)|No;RSI:Yes| |True";
            public const string IsCentralizedLaundrie = "Is Centralized Laundrie? (Yes/No)|No;RSI:Yes| |True";
            public const string IsMandatoryInterventionReason = "Is Mandatory Intervention Reason? (Yes/No)|No| |False";
            public const string IsCssdExpiredValidateInReceiveDetail = "Is CSSD Expired Validate In Receive Detail? (Yes/No: In Process)|Yes;YBRSGKP:No| |True";
            public const string IsCssdUsingDttTerm = "Is CSSD Using DTT Term? (Yes: Checklist DTT/No: Radiobutton Temperature type)|Yes;RSISB:No| |True";
            public const string IsCssdStockValidateInDistribution = "Is CSSD Stock Validate In Distribution|No| |True";
            public const string IsPromoPackageActivated = "Is Promo Package Activated (at patient registration)? (Yes/No)|No| |False";
            public const string IsPersonalWorkExperienceUsingDatePeriod = "Is Personal Work Experience Using Date Period? (Yes/No)|No;RSMM:Yes;RSMP:Yes| |True";
            public const string IsAllowSanitationWasteBalanceMinus = "Is Allow Sanitation Waste Balance Minus? (Yes/No)|No| |True";
            public const string PersonalContactTypeEmail = "Personal Contact Type : Email|04;YBRSGKP:03| |False";
            public const string acc_IsAutoApprovedTransaction = "Setting for Transaction AUTO Approved (Yes/No)|No;RSISB:Yes| |False";
            public const string ClinicalPathwayRegistrationType = "Clinical Pathway Registration Type (separated by [,])|| |False";
            public const string IsShowArReceiptInVerificationAndPaymentList = "Is Show A/R Receipt In Verification And Payment List? (Yes/No)|No;RSTJ:Yes| |True";
            public const string IsSaveHistoryInImportBpjsVerification = "Is Save History In Import BPJS Verification (A/R Creating Invoice)? (Yes/No)|No;RSTJ:Yes| |True";
            public const string InvoicePaymentDiscount = "Invoice Payment for Discount|PaymentMethod-005| |False";
            public const string IsVerificationBillingAuthorizationActivated = "Is Verification Billing Authorization Activated? (Yes/No)|No| |False";
            public const string MaximumQtyBloodBagRequestPassedCasemix = "Maximum Qty Blood Bag Request Passed Casemix|2| |False";
            public const string PurchaseRequestOutstandingListOrderBy = "Purchase Request Outstanding List Order By (fill with [TransDate/ApprDate],[Asc/Desc])|TransDate,Asc;RSPP:TransDate,Desc;RSI:ApprDate,Desc| |True";
            public const string IsDistributionOnlyInStock = "Is Distribution Only In Stock? (Yes/No)|No;RSYS:Yes| |True";
            public const string IsAutoInsertToEmployeePeriodicSalaryOvertime = "Is Auto Insert To Employee Periodic Salary Overtime? (Yes/No)|No;RSMM:Yes| |True";
            public const string KPI_IsShowDenum = "KPI Is Show Denum? (Yes/No)|No| |False";
            public const string IsCrmMembershipActive = "Is CRM-Membership Active? (Yes/No)|No;RSSMCB:Yes| |True";
            public const string IsCredentialingWithPrerequisite = "Is Credentialing With Prerequisite? (Yes/No)|No| |True";
            public const string IsCompetencyAssessmentUsingSingleEvaluator = "Is Credential Competency Assessment Using Single Evaluator? (Yes/No)|Yes;RSI:No| |True";
            public const string EmployeeProfessionGroupMedical = "Employee Profession Group : Medical|01| |False";
            public const string EmployeeProfessionGroupNursing = "Employee Profession Group : Nursing|02| |False";
            public const string EmployeeProfessionGroupKtkl = "Employee Profession Group : KTKL|03| |False";
            public const string EmploymentTypeCi = "Employment Type : CI|CI| |False";
            public const string EmployeeStatusInActive = "Employee Status In-Active|8,9,10,11| |False";
            public const string IsValidateEmployeeLeaveWithPayCutCantCrossMonth = "Is Validate Employee Leave With Pay Cut Can Not Cross Month? (Yes/No)|Yes| |True";
            public const string IntNotesVerifLabel = "Notes Verif label (TBAK,TBK,Verif) |TBK| |false";
            public const string IntNotesVerifLabelReview = "Notes Verif label Review (Verif,Review) |Verif| |false";
            public const string acc_IsPackageRevenueOnMainPackage = "Is revenue of the package embedded on journal of MainPackage (Yes/No) |No;RSISB:Yes| |false";
            public const string acc_CoaPackageDiff = "Accounting Coa Code Package Difference (Plus) || |false";
            public const string acc_CoaPackageDiffMin = "Accounting Coa Code Package Difference (Minus)|| |false";
            public const string IsAllowEditAssetGroup = "Is Allow Edit Asset Group? (Yes/No)|No;RSI:Yes| |True";
            public const string IsCustomPivotFilterByUser = "Is Custom Pivot Filter By User ? (Yes/No)|Yes;RSMP:No| |True";
            public const string IsPatientTransferUsingFilterToClass = "Is Patient Transfer Using Filter To Class? (Yes/No)|No;RSYS:Yes| |True";
            public const string DiscountProrataToRevenueDateStart = "Discount Prorata To Revenue Date Start (yyyy-MM-dd)|2099-01-01;YBRSGKP:2023-01-01| |False";
            public const string IsOverpaymentProrataToRevenue = "Is Overpayment Prorata To Revenue? (Yes/No)|Yes| |False";

            public const string RefDischargeConditionForPresentStatus = "Reference DischargeCondition for Present Status|IPR;YBRSGKP:PS| |false";
            public const string RemunBudgedPercentage = "Remun Budged Percentage (RSUDCDR)|35| |false";
            public const string ServiceUnitIdIgdForRemun = "Service Unit Id Igd For Remun (Multiple value separated by semicolon (;)) (RSUDCDR)|| |false";
            public const string CranialisStdRefId = "Cranialis StandardReferenceID|NervusCranialis| |True";
            public const string IsReceivedByCombobox = "Blood Distribution Received By Combobox (Yes/No) |No||False";
            public const string IsAplicaresByRoomName = "Is Aplicares By Room Name ? (Yes/No)|No;RSEE:Yes| |False";
            public const string IsAutoRecruitmentPlanName = "Is Auto Recruitment Plan Name ? (Yes/No)|No;RSEE:Yes| |False";
            public const string IsDisableClassOnRequestChangeItemProduct = "Is Disable Class Selection On Request Change For Item Product ? (Yes/No)|No;RSRM:Yes| |False";
            public const string acc_PatientReceivableDateStart = "Patient Receivable Date Start (yyyy-MM-dd)|1970-01-01| |False";
            public const string IsAllPhysicianOnSbar = "Is All Physician On SBAR ? (Yes/No)|No;RSYS:Yes| |False";

            public const string IsFamilyOrPatientSignature = "Is Family Or Patient Signature ? (Yes/No)|No;RSYS:Yes| |False";
            public const string IsAutoKioskQueueStatusSkippedForPrescription = "Is Auto Kiosk Queue Status Skipped For Prescription? (Yes/No)|No;RSI:Yes| |False";
            public const string IsSignMandatoryOnOperatingNotes = "Is Sign Mandatory On Operating Notes ? (Yes/No)|No;RSYS:Yes| |False";
            public const string IsMandatoryEpisodeProcedureOnOperatingNotes = "Is Mandatory Episode Procedure On Operating Notes? (Yes/No)|No;RSYS:Yes| |False";
            public const string QueueDisplayScrollingText = "Queue Display Scrolling Text|Text Marquee| |False";
            public const string QueueDisplayScrollingDurationText = "Queue Display Scrolling Duration Text|20s| |False";
            public const string QueueDisplaySloganHealthcare = "Queue Display Slogan Healthcare|Slogan RS| |False";

            public const string IsUsingAllICD10 = "Is Using All ICD10 (Yes/No)|No;GPI:Yes;| |false";
            public const string IsRL5354IncludeICD10O = "RL 53 & 54 Include ICD10 Code O (Yes/No)|No;GPI:Yes;| |false";
            public const string IsSeparatedRounding = "Is Separated Rounding? (Yes/No)|No;RSKS:Yes| |False";
            public const string IsUsingRoundingPaymentAR = "Is Using Rounding Payment AR? (Yes,No)|No;RSKS:Yes| |False";
            public const string IsCheckallDistributedPrint = " Defult Check all print Distribution Portion (Yes/No) |No||False";
            public const string IsUsingRoundingPaymentAP = "Is Using Rounding Payment AP? (Yes/No)|No;RSKS:Yes|�|False";
            public const string IsHandHygieneNoteNoValidation = "Is HandH ygiene Note No Validation? (Yes/No)|No;RSISB:Yes|�|False";
            public const string IsUsingParamedicFeeByTeam = "Is Using Paramedic Fee By Team? (Yes/No)|No;RSKS:Yes| |False";
            public const string IsUsingGuarantorPrefixForQueueCodeKioskV2 = "Is Using Guarantor Prefix For Queue Code Kiosk V2? (Yes/No)|No;RSKS:Yes| |False";
            public const string IsValidateParamedicSBAR = "Is Validate Paramedic SBAR (Yes/No)|Yes;RSISB:No| |False";
            public const string IsAllowEditPatientFromVerificationBilling = "Is Allow Edit Patient From Verification Billing (Yes/No)|No;RSKS:Yes| |False";
            public const string ServiceUnitIDForPaymentReceivedARBankCostSubledger = "Service Unit ID For Payment Received AR Bank Cost Subledger|||False";
            public const string IsUsingBankCostSubledgerForPaymentReceivedAR = "Is Using Bank Cost Subledger For Payment Received AR (Yes/No)|No;RSKS:Yes| |False";
            public const string IsUsingQueueCodeByPhysicianKioskV2 = "Is Using Queue Code By Physician Kiosk V2 (Yes/No)|No||False";
            public const string ItemGroupIDMedicationResume = "Item Group Medication In Medical Discharge Summary (separated by [,])|| |False";
            public const string IsNewdetailResultPapSmear = "Is New Detail Result Pap Smear (Yes/No)?|No;RSISB:Yes| |false";
            public const string IsEnabledAddNewItemCSSD = "Is Enabled Add New Item CSSD (Yes/No)?|No;RSISB:Yes| |false";
            public const string IsUsingItemConsAndExpFactorOnJORealizationList = "Is Using Item Consumption and Exposure Factor On Job Order Realization List (Yes/No)|No;RSYS:Yes| |False";
            public const string isUsingDefultGcs = "Is Using Defult Gcs (Yes/No)?|No;RSISB:Yes| |false";
            public const string IsNewBudgetingAutomatisItemMasterProduct = "Is New Budgeting Automatis Item Master Product (Yes/No)?|No;RSISB:Yes| |false";
            public const string IsUsingKioskQueNoFormat = "Is Using Kiosk Queue Number Format (Yes/No)?|No;RSIMT:Yes| |true";
            public const string IsAntrolCreateRegistrationQueue = "Is Antrol Create Registration Queue (Yes/No)?|Yes| |false";
            public const string AssessmentTypeIDsForShowPanelFdolm = "List Asesment Yang Menggunakan Panel First day of last menstruation, gunakan titik koma (;) jika lebih dari satu|||false";
            public const string IsUsingSplitPainScaleAndFlaccBasedOnAge = "Is Using Split Pain Scale And Flacc Based On Age (Yes/No)?|No;RSSTJ:Yes| |false";
            public const string SplitPainScaleAndFlaccAgeValue = "Split Pain Scale And Flacc Age Value in number|| |false";
            public const string IsEmrHideDivPeEntryOnAssessmentEntry = "Is Emr Hide Div Physical Exam Entry On Assessment Entry (Yes/No)?|No;RSSTJ:Yes|�|false";
            public const string OperatingRoomBookingLimit = "Operating Room Booking Limit�(in�hours)|0| |False";

            public const string RptTableToImageReducePctg = "Percentage of Image Size from Html Table to be reduced in report|100| |false";
            public const string PrescriptionDisplayColumnsDef = "Prescription Display Columns Definition|MedicalNo,PrescriptionNo,Flag;RSIMT:QueueNo,Flag| |false";
            public const string IsSoapFromPysicalExamIncludeNormalValue = "Is Soap From Pysical Exam Include Normal Value? (Yes/No)|Yes| |false";
            
            public const string MaxChronicDrugPrescriptionInDays = "Maximum Chronic Drug Prescription in days|7| |False";
            public const string Is23DaysPrescriptionUseChronicGuarantor = "Is 23 Days Prescription Use Kronis Guarantor|;RSI:B2294| |False";
            public const string ValueForTakingQueueBeforeStartTime = "Waktu Yang Diizinkan Untuk Mengambil Antrian Sebelum Jam Mulai (dalam menit)|60| |False";
            public const string DischargeMethodDefaultOpr = "Default Value Discharge Method MDS Outpatient|O07;RSBK:O01;RSRG:O04| |False";
            public const string DischargeMethodDefaultEmr = "Default Value Discharge Method MDS Emergency|E03| |False";
            public const string IsSignMandatoryOnAssessmentEntry = "Is Sign Mandatory On Assessment Entry? (Yes/No)|No| |False";
            public const string IsShowCurb65ScoreInAssesmentAndMDS = "Show CURB 65 Score In Assesment and MDS (Yes/No)?|No;RSI:Yes|�|false";
            public const string IsUsingNumberingSettingAppointmentNoWebService = "Numbering Setting for Appointment Web Service (Yes/No)?|No;RSI:Yes| |false";
            public const string IsMultipleSynonymValueForDiagnoseAndProcedure = "Is Multiple Synonym Value For Diagnose And Procedure (Yes/No)?|No;RSI:Yes| |false";
            public const string TablePatientBirthRecordFieldValidation = "Table Patient Birth Record Field Validation|| |false";
            public const string IsUsingMultipleScoringSupervisor = "Is Using Multiple Scoring Supervisor (Yes/No)?|No;RSBK:Yes| |false";
            public const string IsAssessmentAutoSaveMds = "Is Assessment Auto Save Mds (Yes/No)?|No;RSI:Yes| |false";
            public const string IsAssessmentAutoSaveMdsCasemixWithGuarantorDoc = "Is Assessment Auto Save Mds Casemix with Guarantor Document (Yes/No)?|No| |false";
            public const string IsParamedicAbsentByIMMADokter = "Is Using Validation For Login Paramedic If Absent From IMMA Dokter (Yes/No)?|No;RSI:Yes| |false";
            public const string IsAllowEditDiagnosisOnEpisodeProcedureEMR = "Is Allow Edit Diagnosis On Episode Procedure EMR? (Yes/No)|No;RSI:Yes| |False";
            public const string IsBillingEmrAddButtonEnabled = "IsBillingEmrAddButtonEnabled? (Yes/No)|No;RSI:Yes| |False";
            public const string IsNewDisplayCasemixCenter = "Is New Display Casemix Center (Yes/No)?|No;RSI:Yes| |false";
            public const string IsNotesforCompoundPresc = "Is Notes for Compound Prescription? (Yes/No)|No;RSI:No| |False";
            public const string IsUseCurrentDateRegistration = "Is This Filter Use Current Date Registration? (Yes/No)|No;RSI:No| |False";
        }

        public static AppParameter GetParameter(ParameterItem parameterItem)
        {
            var entity = new AppParameter();
            if (entity.LoadByPrimaryKey(parameterItem.ToString()))
            {
                return entity;
            }

            return CreateDefaultParameter(parameterItem);
        }
        public static string GetParameterValue(ParameterItem parameterItem)
        {
            //  Get Parameter Value
            var qr = new AppParameterQuery("a");
            qr.Select(qr.ParameterValue);
            qr.es.Top = 1;
            qr.Where(qr.ParameterID == parameterItem.ToString());
            var result = qr.ExecuteScalar();
            if (result != null)
                return result.ToString();

            var ent = CreateDefaultParameter(parameterItem);

            return ent.ParameterValue;
        }

        public static object GetParameterNullableValue(ParameterItem parameterItem)
        {
            //  Get Parameter Value
            var qr = new AppParameterQuery("a");
            qr.Select(qr.ParameterValue);
            qr.es.Top = 1;
            qr.Where(qr.ParameterID == parameterItem.ToString());
            var result = qr.ExecuteScalar();
            return result;
        }
        private static AppParameter CreateDefaultParameter(ParameterItem parameterItem)
        {
            AppParameter entity;
            //  Insert default value via reflection
            var defaultRecordValue = GetDefaultRecordValue(parameterItem.ToString());

            if (string.IsNullOrEmpty(defaultRecordValue))
            {
                throw new Exception(string.Format("Parameter {0} not defined.", parameterItem));
            }

            var defaultRecordValues = defaultRecordValue.Split('|');
            // Urutan --> [ParameterName]|[ParameterValue]|[ParameterType]|[IsUsedBySystem]
            entity = new AppParameter
            {
                ParameterID = parameterItem.ToString(),
                ParameterName = defaultRecordValues[0],
                ParameterType = defaultRecordValues[2],
                IsUsedBySystem = Convert.ToBoolean(defaultRecordValues[3])

            };

            if (defaultRecordValues.Length > 4)
                entity.Message = defaultRecordValues[4];

            // Custom per HealthcareInitialAppsVersion
            var defVal = defaultRecordValues[1];
            if (defVal.Contains(";"))
            {
                var defVals = defVal.Split(';');
                entity.ParameterValue = defVals[0];
                var healthcareInitialAppsVersion =
                    GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion).ToLower();
                var healthcareInitial =
                    GetParameterValue(AppParameter.ParameterItem.HealthcareInitial).ToLower();
                foreach (string val in defVals)
                {
                    if (val.Contains(":"))
                    {
                        if (healthcareInitialAppsVersion.Equals((val.Split(':')[0]).ToLower()))
                        {
                            entity.ParameterValue = val.Split(':')[1];
                        }
                        else if (healthcareInitial.Equals((val.Split(':')[0]).ToLower()))
                        {
                            entity.ParameterValue = val.Split(':')[1];
                        }
                    }
                }
            }
            else
                entity.ParameterValue = defVal;

            entity.Save();
            return entity;
        }

        public static string GetDefaultRecordValue(string parameterID)
        {
            var defaultValue = string.Empty;
            var field = typeof(DefaultRecordValue).GetField(parameterID);
            if (field != null)
                defaultValue = field.GetRawConstantValue().ToString();
            return defaultValue;
        }

        public static bool IsNo(ParameterItem parameterItem)
        {
            return !IsYes(parameterItem);
        }

        public static bool IsYes(ParameterItem parameterItem)
        {
            return IsYes(GetParameterValue(parameterItem));
        }
        public static bool IsYes(string parValue)
        {
            var valLower = parValue.ToLower();
            return valLower == "yes" || valLower == "true";
        }
        public override void Save()
        {
            switch (this.ParameterID)
            {
                case "IsMinMaxItemBalanceByStockGroupAutoUpdate":
                    {
                        if (es.IsModified
                            && this.ParameterValue.ToLower() == "yes"
                            && GetOriginalColumnValue("ParameterValue").ToString().ToLower().Equals("no"))
                        {
                            UpdateMinMaxItemBalanceByStockGroup(null, null);
                        }
                        else
                            base.Save();
                        break;
                    }
                case "PeriodDayHistUsingForCalcMinBalPerStockGroup":
                    {
                        if (es.IsModified
                            && AppParameter.IsYes(ParameterItem.IsMinMaxItemBalanceByStockGroupAutoUpdate)
                            && !GetColumn("ParameterValue").Equals(GetOriginalColumnValue("ParameterValue")))
                        {
                            var dayForMin = this.ParameterValue.ToInt();
                            UpdateMinMaxItemBalanceByStockGroup(dayForMin, null);
                        }
                        else
                            base.Save();
                        break;
                    }
                case "PeriodDayHistUsingForCalcMaxBalPerStockGroup":
                    {
                        if (es.IsModified
                            && AppParameter.IsYes(ParameterItem.IsMinMaxItemBalanceByStockGroupAutoUpdate)
                            && !GetColumn("ParameterValue").Equals(GetOriginalColumnValue("ParameterValue")))
                        {
                            var dayForMax = this.ParameterValue.ToInt();
                            UpdateMinMaxItemBalanceByStockGroup(null, dayForMax);
                        }
                        else
                            base.Save();
                        break;
                    }

                case "IsMinMaxItemBalanceAutoUpdate":
                    {
                        if (es.IsModified
                            && this.ParameterValue.ToLower() == "yes"
                            && GetOriginalColumnValue("ParameterValue").ToString().ToLower().Equals("no"))
                        {
                            UpdateMinMaxItemBalance(null, null);
                        }
                        else
                            base.Save();
                        break;
                    }

                case "PeriodDayHistUsingForCalcMinBalance":
                    {
                        if (es.IsModified
                            && AppParameter.IsYes(ParameterItem.IsMinMaxItemBalanceAutoUpdate)
                            && !GetColumn("ParameterValue").Equals(GetOriginalColumnValue("ParameterValue")))
                        {
                            var dayForMin = this.ParameterValue.ToInt();
                            UpdateMinMaxItemBalance(dayForMin, null);
                        }
                        else
                            base.Save();
                        break;
                    }

                case "PeriodDayHistUsingForCalcMaxBalance":
                    {
                        if (es.IsModified
                            && AppParameter.IsYes(ParameterItem.IsMinMaxItemBalanceAutoUpdate)
                            && !GetColumn("ParameterValue").Equals(GetOriginalColumnValue("ParameterValue")))
                        {
                            var dayForMax = this.ParameterValue.ToInt();
                            UpdateMinMaxItemBalance(null, dayForMax);
                        }
                        else
                            base.Save();
                        break;
                    }
                default:
                    base.Save();
                    break;
            }


        }
        #region trigger
        private void UpdateMinMaxItemBalanceByStockGroup(int? dayForMin, int? dayForMax)
        {
            // UpdateMinMaxItemBalanceByStockGroup
            using (var trans = new esTransactionScope())
            {
                if (dayForMin == null)
                    dayForMin =
                       AppParameter.GetParameterValue(
                           AppParameter.ParameterItem.PeriodDayHistUsingForCalcMinBalPerStockGroup).ToInt();

                if (dayForMax == null)
                    dayForMax =
                       AppParameter.GetParameterValue(
                           AppParameter.ParameterItem.PeriodDayHistUsingForCalcMaxBalPerStockGroup).ToInt();

                var sdti = new AppStandardReferenceItemCollection();
                sdti.Query.Where(sdti.Query.StandardReferenceID == "StockGroup");
                sdti.LoadAll();
                foreach (var entity in sdti)
                {
                    Location.UpdateMinMaxItemBalanceByStockGroup(entity.ItemID, dayForMin ?? 0, dayForMax ?? 0);
                }
                base.Save();
                trans.Complete();
            }
        }

        private void UpdateMinMaxItemBalance(int? dayForMin, int? dayForMax)
        {

            // UpdateMinMaxItemBalance
            using (var trans = new esTransactionScope())
            {
                if (dayForMin == null)
                    dayForMin =
                       AppParameter.GetParameterValue(AppParameter.ParameterItem.PeriodDayHistUsingForCalcMinBalance)
                           .ToInt();

                if (dayForMax == null)
                    dayForMax =
                       AppParameter.GetParameterValue(AppParameter.ParameterItem.PeriodDayHistUsingForCalcMaxBalance)
                           .ToInt();

                var loc = new LocationCollection();
                loc.Query.Where(loc.Query.IsAutoUpdateStockMinMax == true);
                loc.LoadAll();
                foreach (var entity in loc)
                {
                    Location.UpdateStockMinMaxPerLocation(entity.LocationID, dayForMin ?? 0, dayForMax ?? 0);
                }
                base.Save();
                trans.Complete();
            }
        }


        #endregion
    }
}
