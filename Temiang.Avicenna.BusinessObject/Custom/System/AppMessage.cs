using System.Data.SqlClient;
using System.Reflection;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppMessage
    {
        public enum MessageIdEnum
        {
            AlertMsgTitle,
            AlreadyClosed,
            CancelConfirmation,
            CommonMsg,
            ConfirmationMsgTitle,
            CreatedPo,
            DuplicatePrimaryKey,
            DuplicateCode,
            DeleteConflicted,
            EmailCanNotEmpty,
            StopSceduleDateMustBeGreatherOrEqual,
            InvalidValue,
            Mandatory,
            MandatoryDateTime,
            RecordDetailAlreadyInTable,
            RecordNotReviewedYet,
            RecordNotVerifiedYet,
            RecordAlreadyReviewed,
            RecordAlreadyApproved,
            RecordAlreadyProcess,
            RecordAlreadySelected,
            RecordAlreadyPrinted,
            RecordAlreadyVoid,
            RecordAlreadyDeleted,
            RecordDoesNotExist,
            RecordAlreadyExist,
            UserNotExist,
            RecordLockChanged,
            RecordLocked,
            RecordLockedReview,
            OverrideConfirm,
            RecordUsedBySystem,
            ReviewConfirmation,
            VoidReviewConfirmation,
            VoidCancelOrderConfirmation,
            CantApprWrongRevNo,
            InsufficientStock,
            MaxReceivedItemPor,
            InvalidValueVitalSign,
            ValidationNo,
            JobOrderNotRealizeYet,
            ItemNotSentToAutoPack,
            NoDataToSave,
            NoDataToDisplay,
            MustBeGreater,
            MustBeLess,
            MustBeLessOrEqual,
            MustBeGreaterOrEqual,
            MustBeRange,
            CantBeLower,
            MimsExcelRecordNotValid,
            LocationInProcess,
            UserEmailExist,
            CompletedFirst,
            ConfirmPasswordWrong,
            ConfirmNewPasswordWrong,
            SaveConfirmation,
            DischargeConfirmation,
            ValueMustNumeric,
            ArgumentNumberMustSame,
            SessionTimeOut,
            MustBeEqual,
            UnableDelete,
            UnablePrint,
            UnableSave,
            UnableEdit,
            UnableVoid,
            UnableReview,
            RecordUsedByProcess,
            MarkAsBlackList,
            LockedTransaction,
            SetTransactionNo,
            SetServiceUnitOrderType,
            ErrorMsgForStock,
            ErrorMsgForStockDrugAdministration,
            SetTransactionCoa,
            SetTransactionCodeSource,
            Undefined,
            ProsesSaveConfirmation,
            FreeMessage,
            CantUsePaymentAdvance,
            MrpMessage,
            PastDue,
            LookUpNotValid,
            JournalAlredyClose,
            MustSelect1Leader,
            ReplaceRegParamedic,
            ItemUnitUndefined,
            RegistrationDischarge,
            NoDataSelected,
            DoesnotHaveProfileAccess,
            NoSchedule,
            Holiday,
            JournalNotBalacance,
            ErrorMessageBOM,
            MedicalNoDoesNotExist,
            RegisteredParamedic,
            WarningMessageStocOpname,
            SetTransactionCoaForAgcItem,
            CantDeactiveRoom,
            RecordMustBeActive,
            InvalidForValue,
            BudgetCalculateQtyWarning,
            ForecastCalculateQtyWarning,
            CannotZeroValue,
            SelectDataToEdit,
            SelectDataToDelete,
            ItemAlreadyExistInPreviousLevel,
            ClosePOSuccess,
            FiscalPeriodeWarining,
            ValidateDateOfDeath,
            ValidateTimeOfDeath,
            VoidCancelOrderandRegistrationConfirmation,
            PatientAlreadyBlackList,
            SingleDoseInputItemConfirmation,
            SingleDoseItemTypeNotValid,
            CancelledRecord,
            AppointmentCanotDeleted,
            HoldTransaction,
            OutstandingTransaction,
            NotValidIDLookUp,
            CannotInsertValueFor,
            ErrorMonthlyOpening,
            PrintFailed,
            PrintSuccess,
            NotPrinted,
            CantSelectAutomation,
            ReportParameterEmpty,
            BedTransferInProgress,
            NoBedTransfer,
            RefreshConfirmation,
            MustBeDifferent,
            MustChooseSelectedOnes,
            SupplierItemInRange,
            LengthNameOnCard,
            DuplicateItemBom,
            TransactionCorrectionLimit,
            MAX_DURATION_MEDICATION,
            ItemGroupServiceUnit
        }

        public class DefaultMessage
        {
            public const string TransactionCorrectionLimit = "Patient must be discharged maximum {0} days ago";
            public const string BedInUse = "Bed already in used by another Registration. You should fill the Bed again.";
            public const string RefreshConfirmation = "Transaction have been changed. Do you want to refresh patient's transaction?";
            public const string NoBedTransfer = "This Patient does not change bed.";
            public const string BedTransferInProgress = "Bed Transfer in Progress";
            public const string AlreadyClosed = "{0} already closed.";
            public const string CantSelectAutomation = "Can't Select Automation Charges Item, Registered Physician is empty. ";
            public const string AlertMsgTitle = "Alert";
            public const string CancelConfirmation = "Data will be cancel. Continue ?";
            public const string CommonMsg = "Sorry for the inconvenience!. Please try again, if still problem, please contact IT Department";
            public const string ConfirmationMsgTitle = "Confirmation";
            public const string ConfirmNewPasswordWrong = "Password must have at least 1 number, 1 special character, not start with '.', not contain character '>' '<' '|' '_' and more than 6 characters.";
            public const string CannotInsertValueFor = "Can't insert {0}, because {1}";
            public const string DuplicatePrimaryKey = "Record ID / Code has registered";
            public const string DuplicateCode = "This Code is already registered, please change to other code";
            public const string InvalidValue = "Invalid value for ";
            public const string InvalidForValue = "Invalid value for {0}";
            public const string EmailCanNotEmpty = "Email Can't Empty";
            public const string Mandatory = "You should fill the ";
            public const string MandatoryDateTime = "You should fill the {0}";
            public const string MustChooseSelectedOnes = "You should choose the ";
            public const string RecordDetailAlreadyInTable = "This record has already in data detail";
            public const string RecordNotReviewedYet = "This record is not reviewed yet.";
            public const string RecordNotVerifiedYet = "Job Order {0} is not verified yet.";
            public const string RecordAlreadyReviewed = "This record is already reviewed.";
            public const string RecordAlreadyPrinted = "This record is already printed.";
            public const string RecordAlreadyProcess = "This record is already processed. ";
            public const string RecordAlreadySelected = "This record is already selected";
            public const string RecordAlreadyVoid = "This record is already void";
            public const string RecordAlreadyApproved = "This record is already approved";
            public const string AppointmentCanotDeleted = "This appointment can't be deleted";
            public const string RecordAlreadyDeleted = "This record is already deleted";
            public const string RecordDoesNotExist = "This record does not exist.";
            public const string RecordAlreadyExist = "This record is already exist.";
            public const string RecordUsedBySystem = "This record has been used in another process, can't modified";
            public const string RecordLockChanged = "Current record has override by user {0}  in {1} since {2} minutes ago";
            public const string RecordLocked = "Current record still edited by user {0}  in {1} since {2} minutes ago.";
            public const string RecordLockedReview = "Current record still review by user {0}  in {1} since {2} minutes ago.";
            public const string OverrideConfirm = "Are you willing override ?";
            public const string ReviewConfirmation = "Review this data, continue ?";
            public const string SaveConfirmation = "Save this data, continue ?";
            public const string DischargeConfirmation = "Patient already discharged at {0}. Do you want to continue?";
            public const string ProsesSaveConfirmation = "Process and Save this data, continue ?";
            public const string VoidReviewConfirmation = "Void this data, continue ?";
            public const string VoidCancelOrderandRegistrationConfirmation = "Are you sure want to void? Registration and Order will be cancelled";
            public const string VoidCancelOrderConfirmation = "Are you sure want to void? Order will be cancelled";
            public const string CantApprWrongRevNo = "Sorry you can't approve, Revision No that you enter not correct";
            public const string InsufficientStock = "Insufficient stock on item {0} , stock {1} only {2} {3}.\n";
            public const string MaxReceivedItemPor = "Insufficient stock Receive on item {0} in PO no:{1}, stock receive maximal {2} {3}.";
            public const string ValidationNo = "Number Only";
            public const string JobOrderNotRealizeYet = "Job order no {0} is not realization yet";
            public const string NoDataToSave = "There is no data to save";
            public const string ItemNotSentToAutoPack = "There are drugs that have not been sent to the autopack machine.";
            public const string NoDataToDisplay = "There is no data to display";
            public const string MustBeGreater = "{0} must be greater than {1}";
            public const string MustBeLess = "{0} must be less than {1}";
            public const string MustBeLessOrEqual = "{0} must be less than or equal {1}";
            public const string MustBeGreaterOrEqual = "{0} must be greater than or equal {1}";
            public const string MustBeRange = "The value must be in range {0} and {1}";
            public const string CantBeLower = "{0} can't be lower than {1}";
            public const string MimsExcelRecordNotValid = "Row in Excel not valid, please correct first. For detail download Excel result";
            public const string LocationInProcess = "{0} is on stock taking process, please wait until stock taking process is completed";
            public const string UserEmailExist = "A username for that e-mail address already exists.Please enter a different e-mail address.";
            public const string CompletedFirst = "Please completed mandatory field first";
            public const string ConfirmPasswordWrong = "Password is not match with confirm password.";
            public const string DeleteConflicted = "This record still used, delete aborted.";
            public const string ValueMustNumeric = "{0} must numeric.";
            public const string InvalidValueVitalSign = "{0}";
            public const string ArgumentNumberMustSame = "The number of arguments must be the same.";
            public const string SessionTimeOut = "Session login has time out. This could occur because the server does recycle memory or too long inactivity.";
            public const string MustBeEqual = "{0} must be equal {1}";
            public const string UnableDelete = "Sorry, you can't delete.";
            public const string UnablePrint = "Sorry, you can't print.";
            public const string UnableEdit = "Sorry, you can't edit.";
            public const string UnableReview = "Sorry, you can't Review.";
            public const string UnableVoid = "Sorry, You can't Void.";
            public const string UnableSave = "Sorry, you can't save.";
            public const string RecordUsedByProcess = "This record already used in some process";
            public const string MarkAsBlackList = "This Patient marked as black list\nReason: {0}";
            public const string LockedTransaction = "This registration is locked for transaction";
            public const string SetTransactionNo = "Please contact your administrator to setting transaction no";
            public const string ErrorMsgForStock = "Insufficient stock on item {0}, stock balance only {1} {2}\n";
            public const string ErrorMsgForStockDrugAdministration = "Insufficient stock on item {0} for JO No {3} Administation Date Time {4}, stock balance only {1} {2}\n";
            public const string ErrorMessageBOM = "Item cost for item [{0}] {1} not define in site {2}";
            public const string MedicalNoDoesNotExist = "This MedicalNo does not exist.";
            public const string MustSelect1Leader = "Must Select 1 Leader";
            public const string ReplaceRegParamedic = "Are you sure would like to change the Physician Leader? It will update patient Registered Physician";
            public const string SetTransactionCoa = "Please contact your administrator to setting coa code for this transaction.";
            public const string SetTransactionCodeSource = "Please contact your administrator to setting table Transaction Code Source for this transaction.";
            public const string SetServiceUnitOrderType = "Please contact your administration to setting service unit order type.";
            public const string StopSceduleDateMustBeGreatherOrEqual = "Stop date schedule must be greater than or equal Begin date schedule";
            public const string Undefined = "UNDEFINED";
            public const string DoesnotHaveProfileAccess = "User doesn't have profile access";
            public const string FreeMessage = "{0}";
            public const string CantUsePaymentAdvance = "Transaction - Payment Advance Cannot be Use";
            public const string MrpMessage = "Process and Save this data, continue ?";
            public const string PastDue = "Past Due Start Date";
            public const string LookUpNotValid = "{0} Look Up Data is not valid";
            public const string JournalAlredyClose = "Selected Date Already Monthly Closed";
            public const string ItemUnitUndefined = "Item Unit Convertion UNDEFINED, Set Item Master";
            public const string RegistrationDischarge = "This registration already discharge.";
            public const string NoDataSelected = "No Data Selected";
            public const string NoSchedule = "No Schedule On This Day";
            public const string Holiday = "On Holiday";
            public const string JournalNotBalacance = "Can't continue this proccess, journal is not balance.";
            public const string WarningMessageStocOpname = "Maximal Display 100 rows";
            public const string SetTransactionCoaForAgcItem = "Please contact your administrator to setting coa code for this transaction. you must setting AGC Item Type {0}, Item Group {1}, AGC Code {2}";
            public const string BudgetCalculateQtyWarning = "field Calculated Budget can't be zero value! \n";
            public const string ForecastCalculateQtyWarning = "field Forecast Qty can't be zero value! \n";
            public const string CreatedPo = "PO Created With No ";
            public const string CannotZeroValue = "{0} can't be zero value! \n";
            public const string SelectDataToEdit = "Select data to edit";
            public const string SelectDataToDelete = "Select data to delete";
            public const string SingleDoseItemTypeNotValid = "This Item could not be barcode, because incorrect Item Type master";
            public const string ItemAlreadyExistInPreviousLevel = "Item {0} exist in same root in previouse level";
            public const string ClosePOSuccess = "successfully close Purchase order";
            public const string FiscalPeriodeWarining = "Fiscal Periode {0} Is Alredy Exists On Year {1}";
            public const string ValidateDateOfDeath = "Date of Death must less than or equal today.";
            public const string ValidateTimeOfDeath = "Time of Death must be filled.";
            public const string PatientAlreadyBlackList = "Patient {0} Already Blacklist";
            public const string SingleDoseInputItemConfirmation = "This item is already input since {0} ago in Transaction Entry (Non-CPOE). \n Are you sure to input this item again ? ";
            public const string CancelledRecord = "This record is already cancelled.";
            public const string HoldTransaction = "This registration is locked for transaction. Please contact your billing\n";
            //public const string HoldTransactionReview = "Cannot review, recapitulation is in progress. \n";
            public const string NotValidIDLookUp = "Don't use character _ or . in {0}";
            public const string UserNotExist = "User does not exist in {0}";
            public const string MonthlyOpening = "Cannot contin" +
                                                 "ue process Monthly Opening, there is no data transaction monthly closing on period {0}";
            public const string OutstandingTransaction = "There are some data of {0} that need to review or send to third party.";
            public const string PrintFailed = "Print Failed";
            public const string PrintSuccess = "Print Success";
            public const string NotPrinted = "Not Printed, Qty 0";
            public const string ReportParameterEmpty = "Report Parameter {0} must be filled";
            public const string MustBeDifferent = "{0} must be different from {1}";
            public const string SupplierItemInRange = "Item {0} with supplier {1} is already exist in range {2} - {3}";
            public const string LengthNameOnCard = "Now 'Name On Card' Length is {0} Char(s), Recommended {1} Char(s)";
            public const string DuplicateItemBom = "Cannot save, there is duplicate detail item bom.";
            public const string MAX_DURATION_MEDICATION = "Your duration is greater than maximum duration ({0} day(s))";
            public const string ItemGroupServiceUnit = "please setting service unit location for this {0}";
        }

        public static AppMessage LoadByPrimaryKeyAndAddIfNotExist(MessageIdEnum messageID)
        {
            //Get MessageText
            var entity = new AppMessage();
            if (entity.LoadByPrimaryKey(messageID.ToString()))
            {
                return entity;
            }

            //Insert
            //Get default value via reflection
            var defaultMessage = string.Empty;
            var field = typeof(DefaultMessage).GetField(messageID.ToString());
            if (field != null)
                defaultMessage = field.GetRawConstantValue().ToString();

            entity = new AppMessage { MessageID = messageID.ToString(), MessageText = defaultMessage, IsError = false};
            entity.Save();

            //Return 
            return entity;
        }
        public static string GetMessageText(MessageIdEnum messageID)
        {
            //Get MessageText
            var entity = LoadByPrimaryKeyAndAddIfNotExist(messageID);
            return string.IsNullOrEmpty(entity.MessageTextCustom) ? entity.MessageText : entity.MessageTextCustom;
        }
    }
}
