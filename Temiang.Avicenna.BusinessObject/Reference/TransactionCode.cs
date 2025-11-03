namespace Temiang.Avicenna.BusinessObject.Reference
{
    public class TransactionCode
    {
        public const string Sales = "101";
        public const string SalesReturn = "102";

        public const string Registration = "001";
        public const string Appointment = "002";
        public const string Charges = "003";
        public const string JobOrder = "005";
        public const string ExternalJobOrder = "006";
        public const string PatientTransfer = "007";
        public const string Payment = "016";
        public const string PaymentReturn = "017";
        public const string DownPayment = "018";
        public const string DownPaymentReturn = "019";
        public const string PaymentReceipt = "032";
        public const string PurchaseRequest = "034";
        public const string PriceQuoteRequest = "035";
        public const string PurchaseOrder = "037";
        public const string PurchaseOrderReceive = "040";
        public const string GrantsReceive = "041";
        public const string PurchaseOrderReturn = "043";
        public const string ReceiptOfSubstitute = "044";
        public const string ConsignmentReceive = "045";
        public const string Distribution = "050";
        public const string DistributionConfirm = "055";
        public const string DistributionRequest = "057";
        public const string RequestNonStockItems = "058";
        public const string ConfirmationRequestNonStockItems = "059";
        public const string ConsignmentTransfer = "060";
        public const string StockConsumption = "073";
        public const string StockAdjustment = "074";
        public const string StockTaking = "075";
        public const string InventoryIssueOut = "082";
        public const string InventoryIssueRequestOut = "084";
        public const string InventoryIssueOutForOtherUnit = "085";
        public const string ItemTariffRequest = "086";
        public const string Prescription = "091";
        public const string DischargePrescription = "092";
        public const string SpectaclePrescription = "093";
        public const string PrescriptionReturn = "094";
        public const string DirectPurchase = "095";
        public const string ReferralOrder = "098";
        public const string PackageJobOrder = "131";
        public const string AccountPayable = "140";
        public const string AccountReceivable = "150";
        public const string PhysicianLeave = "099";
        public const string ProductionOfGoods = "046";
        public const string ConsignmentReturn = "047";
        public const string SalesToBranch = "048";
        public const string SalesToBranchReturn = "049";
        public const string RegistrationInfo = "100";
        public const string BudgetPlan = "140";
        public const string NonPatientCustomerharges = "008";
        public const string SnackOrder = "132";
        public const string DestructionOfExpiredItems = "083";
        public const string AssetWorkOrder = "201";
        public const string AssetWorkOrderRealization = "202";
        public const string AssetPM = "203"; //AssetPreventiveMaintenance
        public const string SanitationWorkOrder = "204";
        public const string SanitationMaintenanceActivity = "205";
        public const string BudgetPlanApproval = "141";
        public const string AssetId = "200";
        public const string Cssd = "301";
        public const string RemunerationByIdi = "142";
        public const string RemunerationEmp = "143";
        public const string LaundryWashingProcess = "501";
        public const string LinenItemsExtermination = "502";
    }

    public class StockGroup
    {
        public const string OutPatient = "OP";
        public const string InPatient = "IP";
        public const string Laboratorium = "LB";
    }
}