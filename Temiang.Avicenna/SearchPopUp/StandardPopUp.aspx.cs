using System;
using System.Collections.Generic;
using System.Data;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;


namespace Temiang.Avicenna.SearchPopUp
{
    ///<summary>
    ///
    ///</summary>
    public partial class StandardPopUp : BasePage
    {
        private string PopupSearchType
        {
            get { return (string)ViewState["popUpSearch"]; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            initializeGrid(Page.Request.QueryString[0]);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lastQuery = null;

                ViewState["popUpSearch"] = Page.Request.QueryString[0];
                ViewState["popUpClientID1"] = Page.Request.QueryString["id1"];
                ViewState["popUpClientID2"] = Page.Request.QueryString["id2"] ?? "";

                txtSearch.Focus();
            }


            //Register javascript
            if (!IsCallback)
            {
                initializeGrid(Page.Request.QueryString[0]);
                string script = "";
                script += "<script type=\"text/javascript\">\n";
                script += "//<![CDATA[\n";
                script += "function clientRowClicked(sender, args)\n";
                script += "{\n";
                if (!ViewState["popUpClientID2"].Equals(""))
                {
                    script += "  var ctl=opener.$find(\"" + ViewState["popUpClientID2"] + "\");\n";
                    script += string.Format("  ctl.set_value(args.getDataKeyValue(\"{0}\"));\n",
                                            grdList.MasterTableView.ClientDataKeyNames[1]);
                }
                script += "  var ctl=opener.$find(\"" + ViewState["popUpClientID1"] + "\");\n";
                script += string.Format("  ctl.set_value(args.getDataKeyValue(\"{0}\"));\n",
                                        grdList.MasterTableView.ClientDataKeyNames[0]);
                script += "  window.close();\n";
                script += "  ctl.focus();\n";
                script += "}\n";
                script += "//]]>\n";
                script += "</script>";
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "script", script);
                grdList.ClientSettings.ClientEvents.OnRowClick = "clientRowClicked";
            }
        }

        private void addGridBoundColumn(string fieldName, string caption, int width)
        {
            GridBoundColumn col = new GridBoundColumn();
            col.DataField = fieldName;
            col.HeaderText = caption;
            col.SortExpression = fieldName;
            col.UniqueName = fieldName;
            if (width > 0)
                col.HeaderStyle.Width = width;

            grdList.Columns.Add(col);
        }

        private void addGridBoundColumnWithCurrencyFormat(string fieldName, string caption, int width)
        {
            GridBoundColumn col = new GridBoundColumn();
            col.DataField = fieldName;
            col.HeaderText = caption;
            col.SortExpression = fieldName;
            col.UniqueName = fieldName;
            if (width > 0)
                col.HeaderStyle.Width = width;
            col.DataFormatString = "{0:n2}";

            grdList.Columns.Add(col);
        }

        private void addGridBoundColumnWithDateFormat(string fieldName, string caption, int width)
        {
            GridBoundColumn col = new GridBoundColumn();
            col.DataField = fieldName;
            col.HeaderText = caption;
            col.SortExpression = fieldName;
            col.UniqueName = fieldName;
            if (width > 0)
                col.HeaderStyle.Width = width;
            col.DataFormatString = "dd/MM/yyyy";

            grdList.Columns.Add(col);
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            //Reset
            lastQuery = null;

            //Populate
            grdList.Rebind();
        }

        private void initializeGrid(string popUpSearchType)
        {
            grdList.AllowPaging = true;
            grdList.PageSize = 15;
            grdList.AllowSorting = true;
            grdList.AutoGenerateColumns = false;
            grdList.PagerStyle.Mode = GridPagerMode.NextPrev;
            grdList.Columns.Clear();
            switch (popUpSearchType)
            {
                case "Appointment":
                    addGridBoundColumn("AppointmentNo", "No", 100);
                    addGridBoundColumn("AppointmentDate", "Date", 80);
                    addGridBoundColumn("FirstName", "Name", 150);
                    addGridBoundColumn("StreetName", "Address", 150);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "AppointmentNo" };
                    break;
                case "Patient":
                    addGridBoundColumn("PatientID", "ID", 80);
                    addGridBoundColumn("MedicalNo", "Medical No", 80);
                    addGridBoundColumn("PatientName", "Name", 120);
                    addGridBoundColumn("Address", "Address", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "PatientID" };
                    break;
                case "MedicalNo":
                    addGridBoundColumn("MedicalNo", "Medical No", 80);
                    addGridBoundColumn("PatientName", "Name", 120);
                    addGridBoundColumn("Address", "Address", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "MedicalNo" };
                    break;
                case "NonInPatientCoverageClass":
                case "InPatientCoverageClass":
                case "NonInPatientClass":
                case "ChargeClass":
                case "InPatientClass":
                    addGridBoundColumn("ClassID", "ID", 100);
                    addGridBoundColumn("ClassName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "ClassID" };
                    break;
                case "ServiceRoomByServiceUnitID":
                case "ServiceRoomInPatient":
                case "ServiceRoom":
                    addGridBoundColumn("RoomID", "ID", 100);
                    addGridBoundColumn("RoomName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "RoomID" };
                    break;
                case "Fabric":
                    addGridBoundColumn("FabricID", "ID", 100);
                    addGridBoundColumn("FabricName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "FabricID" };
                    break;
                case "ItemProductMargin":
                    addGridBoundColumn("MarginID", "ID", 100);
                    addGridBoundColumn("MarginName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "MarginID" };
                    break;
                case "ItemAndZatActive":
                case "ItemDiagnostic":
                case "ItemLaboratory":
                case "ItemPackage":
                case "ItemProductNonMedical":
                case "ItemProductMedical":
                case "ItemKitchen":
                case "ItemService":
                case "ItemServiceExcludeProduct":
                case "ItemProductInventory":
                    addGridBoundColumn("ItemID", "ID", 100);
                    addGridBoundColumn("ItemName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "ItemID" };
                    break;
                case "ItemTariffByServiceUnitOrder":
                case "ItemDiagnosticTariff":
                case "ItemLaboratoryTariff":
                case "ItemProductNonMedicalTariff":
                case "ItemProductMedicalTariff":
                case "ItemServiceTariff":
                    addGridBoundColumn("ItemID", "ID", 100);
                    addGridBoundColumn("ItemName", "Name", -1);
                    addGridBoundColumnWithCurrencyFormat("Price", "Price", 100);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "ItemID" };
                    break;
                case "ItemTransactionListRequestOrder":
                    addGridBoundColumn("TransactionNo", "Transaction No", 100);
                    addGridBoundColumn("TransactionDate", "Transaction Date", -1);
                    addGridBoundColumn("ServiceUnitName", "Service Name", 100);
                    addGridBoundColumn("ItemName", "Item Name", 100);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "TransactionNo" };
                    break;
                case "ItemUnit":
                case "MarginRangeType":
                    addGridBoundColumn("ItemID", "ID", 100);
                    addGridBoundColumn("ItemName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "ItemID" };
                    break;
                case "Supplier":
                    addGridBoundColumn("SupplierID", "ID", 100);
                    addGridBoundColumn("SupplierName", "Name", 250);
                    addGridBoundColumn("Address", "Address", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "SupplierID" };
                    break;
                case "ServiceUnitJobOrder":
                case "ServiceUnitTransaction":
                case "ServiceUnitByServiceGroupID":
                case "ServiceUnitByRegistrationType":
                case "ServiceUnit":
                    addGridBoundColumn("ServiceUnitID", "ID", 100);
                    addGridBoundColumn("ServiceUnitName", "Name", -1);
                    addGridBoundColumn("DepartmentName", "Department", 200);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "ServiceUnitID" };
                    break;
                case "ServiceUnitVisitType":
                    addGridBoundColumn("VisitTypeID", "ID", 100);
                    addGridBoundColumn("VisitTypeName", "Name", -1);
                    addGridBoundColumn("VisitDuration", "Duration", 80);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "VisitTypeID" };
                    break;
                case "Referral":
                    addGridBoundColumn("ReferralID", "ID", 100);
                    addGridBoundColumn("ReferralName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "ReferralID" };
                    break;
                case "BedByServiceRoomID":
                case "Bed":
                    addGridBoundColumn("BedID", "Bed", 100);
                    addGridBoundColumn("RoomName", "Room", -1);
                    addGridBoundColumn("ClassName", "Class", 100);
                    addGridBoundColumn("BedStatus", "Bed Status", 100);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "BedID" };
                    break;
                case "RegistrationByRegType":
                case "Registration":
                    addGridBoundColumn("RegistrationNo", "Registration No", 120);
                    addGridBoundColumn("MedicalNo", "Medical No", 100);
                    addGridBoundColumn("PatientName", "Patient Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "RegistrationNo" };
                    break;
                case "ParamedicClusterDetail":
                case "ParamedicByServiceUnitID":
                case "ParamedicByRegistration":
                    addGridBoundColumn("ParamedicID", "ID", 100);
                    addGridBoundColumn("ParamedicName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "ParamedicID" };
                    break;
                case "MorphologyByDiagnoseID":
                    addGridBoundColumn("MorphologyID", "ID", 100);
                    addGridBoundColumn("MorphologyName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "MorphologyID" };
                    break;
                case "TransactionNoByRegistration":
                case "JobOrderNoByRegistration":
                    addGridBoundColumn("TransactionNo", "Transaction No", 110);
                    addGridBoundColumn("TransactionDate", "Date", 80);
                    addGridBoundColumn("ServiceUnitName", "Service Unit", -1);
                    addGridBoundColumn("RegistrationNo", "Registration No", 120);
                    addGridBoundColumn("MedicalNo", "Medical No", 70);
                    addGridBoundColumn("PatientName", "Patient Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "TransactionNo" };
                    break;
                case "ItemTariffByServiceUnitTransaction":
                case "ItemBalanceByLocation":
                    addGridBoundColumn("ItemID", "ID", 100);
                    addGridBoundColumn("ItemName", "Name", -1);
                    addGridBoundColumn("Balance", "Balance", 100);
                    addGridBoundColumnWithCurrencyFormat("Price", "Price", 100);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "ItemID" };
                    break;
                case "ItemJobOrder":
                    addGridBoundColumn("ItemID", "Item ID", 80);
                    addGridBoundColumn("ItemName", "Item Name", -1);
                    addGridBoundColumn("ServiceUnitName", "Service Unit Name", -1);
                    addGridBoundColumnWithCurrencyFormat("Price", "Price", 60);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "ItemID" };
                    break;
                case "ZipCode":
                    addGridBoundColumn("ZipCode", "ID", 100);
                    addGridBoundColumn("StreetName", "Name", 150);
                    addGridBoundColumn("District", "District", 100);
                    addGridBoundColumn("City", "City", 100);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "ZipCode" };
                    break;
                case "ServiceUnitAutoBillItem":
                case "ServiceUnitItemService":
                    addGridBoundColumn("ItemID", "Item ID", 80);
                    addGridBoundColumn("ItemName", "Item Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "ItemID" };
                    break;
                case "TransPrescriptionSales":
                    addGridBoundColumn("PrescriptionNo", "Prescription No", 110);
                    addGridBoundColumn("PrescriptionDate", "Date", 80);
                    addGridBoundColumn("ServiceUnitName", "Service Unit", -1);
                    addGridBoundColumn("RegistrationNo", "Registration No", 120);
                    addGridBoundColumn("MedicalNo", "Medical No", 70);
                    addGridBoundColumn("PatientName", "Patient Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "PrescriptionNo" };
                    break;
                case "AssetByServiceUnit":
                    addGridBoundColumn("AssetID", "Asset ID", 110);
                    addGridBoundColumn("AssetName", "Asset Name", -1);
                    addGridBoundColumn("BrandName", "Model No", -1);
                    addGridBoundColumn("SerialNumber", "Serial No", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "AssetID" };
                    break;

                #region Finance

                case "Accounts":
                    addGridBoundColumn("AccountID", "ID", 100);
                    addGridBoundColumn("AccountName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "AccountID", "AccountName" };
                    break;
                case "AcctLinkEmployee":
                    addGridBoundColumn("AccountID", "ID", 100);
                    addGridBoundColumn("AccountName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "AccountID", "AccountName" };
                    break;
                case "AcctLinkGuarantor":
                    addGridBoundColumn("AccountID", "ID", 100);
                    addGridBoundColumn("AccountName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "AccountID", "AccountName" };
                    break;
                case "AcctLinkSupplier":
                    addGridBoundColumn("AccountID", "ID", 100);
                    addGridBoundColumn("AccountName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "AccountID", "AccountName" };
                    break;
                case "AcctLinkLoanBank":
                    addGridBoundColumn("AccountID", "ID", 100);
                    addGridBoundColumn("AccountName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "AccountID", "AccountName" };
                    break;
                case "AcctInitialGL":
                    addGridBoundColumn("AccountID", "ID", 100);
                    addGridBoundColumn("AccountName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "AccountID", "AccountName" };
                    break;
                case "AcctSubGroup":
                    addGridBoundColumn("AcctSubGroupID", "ID", 100);
                    addGridBoundColumn("AcctSubGroupName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "AcctSubGroupID", "AcctSubGroupName" };
                    break;
                case "Bank":
                    addGridBoundColumn("BankID", "ID", 100);
                    addGridBoundColumn("BankName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "BankID", "BankName" };
                    break;
                case "BankHrd":
                    addGridBoundColumn("ItemID", "ID", 100);
                    addGridBoundColumn("ItemName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "ItemID", "ItemName" };
                    break;
                case "BankAccount":
                    addGridBoundColumn("BankAccountNo", "ID", 100);
                    addGridBoundColumn("Notes", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "BankAccountNo", "Notes" };
                    break;
                case "Donator":
                    addGridBoundColumn("DonatorID", "ID", 100);
                    addGridBoundColumn("DonatorName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "DonatorID", "DonatorName" };
                    break;
                case "Employee":
                    addGridBoundColumn("EmployeeID", "ID", 100);
                    addGridBoundColumn("EmployeeName", "Name", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "EmployeeID", "EmployeeName" };
                    break;
                case "VoucherCodeReceive":
                    addGridBoundColumn("VoucherCode", "Code", 100);
                    addGridBoundColumn("VoucherNote", "Description", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "VoucherCode", "VoucherNote" };
                    break;
                case "VoucherCodePayment":
                    addGridBoundColumn("VoucherCode", "Code", 100);
                    addGridBoundColumn("VoucherNote", "Description", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "VoucherCode", "VoucherNote" };
                    break;
                case "VoucherCodeMemorial":
                    addGridBoundColumn("VoucherCode", "Code", 100);
                    addGridBoundColumn("VoucherNote", "Description", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "VoucherCode", "VoucherNote" };
                    break;
                case "VoucherCodeAutomatic":
                    addGridBoundColumn("VoucherCode", "Code", 100);
                    addGridBoundColumn("VoucherNote", "Description", -1);
                    grdList.MasterTableView.ClientDataKeyNames = new string[] { "VoucherCode", "VoucherNote" };
                    break;
                case "BillGurantor":
                    addGridBoundColumn("BillingID", "No. Bill", 100);
                    addGridBoundColumn("SubsidiaryID", "Guarantor ID", 100);
                    addGridBoundColumn("GuarantorName", "Guarantor Name", -1);
                    addGridBoundColumn("BillingDate", "Date", 80);
                    addGridBoundColumn("BillingDueDate", "Due Date", 80);
                    addGridBoundColumn("SRCurrency", "Curr", 40);
                    addGridBoundColumn("BillingAmount", "Amount", 150);
                    addGridBoundColumn("BalanceAmount", "Balance", 80);
                    grdList.MasterTableView.ClientDataKeyNames = new string[]
                                                                     {
                                                                         "BillingID", "SubsidiaryID", "GuarantorName",
                                                                         "BillingDate",
                                                                         "BillingDueDate", "SRCurrency", "BillingAmount",
                                                                         "BalanceAmount"
                                                                     };
                    break;
                case "BillSupplier":
                    addGridBoundColumn("BillingID", "No. Bill", 100);
                    addGridBoundColumn("SubsidiaryID", "Supplier ID", 100);
                    addGridBoundColumn("SupplierName", "Supplier Name", -1);
                    addGridBoundColumn("BillingDate", "Date", 80);
                    addGridBoundColumn("BillingDueDate", "Due Date", 80);
                    addGridBoundColumn("SRCurrency", "Curr", 40);
                    addGridBoundColumn("BillingAmount", "Amount", 150);
                    addGridBoundColumn("BalanceAmount", "Balance", 80);
                    grdList.MasterTableView.ClientDataKeyNames = new string[]
                                                                     {
                                                                         "BillingID", "SubsidiaryID", "SupplierName",
                                                                         "BillingDate",
                                                                         "BillingDueDate", "SRCurrency", "BillingAmount",
                                                                         "BalanceAmount"
                                                                     };
                    break;
                case "BillDonator":
                    addGridBoundColumn("BillingID", "No. Bill", 100);
                    addGridBoundColumn("SubsidiaryID", "Donator ID", 100);
                    addGridBoundColumn("DonatorName", "Donator Name", -1);
                    addGridBoundColumn("BillingDate", "Date", 80);
                    addGridBoundColumn("BillingDueDate", "Due Date", 80);
                    addGridBoundColumn("SRCurrency", "Curr", 40);
                    addGridBoundColumn("BillingAmount", "Amount", 150);
                    addGridBoundColumn("BalanceAmount", "Balance", 80);
                    grdList.MasterTableView.ClientDataKeyNames = new string[]
                                                                     {
                                                                         "BillingID", "SubsidiaryID", "DonatorName",
                                                                         "BillingDate",
                                                                         "BillingDueDate", "SRCurrency", "BillingAmount",
                                                                         "BalanceAmount"
                                                                     };
                    break;
                case "BillEmployee":
                    addGridBoundColumn("BillingID", "No. Bill", 100);
                    addGridBoundColumn("SubsidiaryID", "Employee ID",
                                       100);
                    addGridBoundColumn("EmployeeName", "Employee Name", -1);
                    addGridBoundColumn("BillingDate", "Date", 80);
                    addGridBoundColumn("BillingDueDate", "Due Date", 80);
                    addGridBoundColumn("SRCurrency", "Curr", 40);
                    addGridBoundColumn("BillingAmount", "Amount", 150);
                    addGridBoundColumn("BalanceAmount", "Balance", 80);
                    grdList.MasterTableView.ClientDataKeyNames = new string[]
                                                                     {
                                                                         "BillingID", "SubsidiaryID", "EmployeeName",
                                                                         "BillingDate",
                                                                         "BillingDueDate", "SRCurrency", "BillingAmount",
                                                                         "BalanceAmount"
                                                                     };
                    break;
                case "BillLoanBank":
                    addGridBoundColumn("BillingID", "No. Bill", 100);
                    addGridBoundColumn("SubsidiaryID", "No. A/C ID", 100);
                    addGridBoundColumn("BankNotes", "Description", -1);
                    addGridBoundColumn("BillingDate", "Date", 80);
                    addGridBoundColumn("BillingDueDate", "Due Date", 80);
                    addGridBoundColumn("SRCurrency", "Curr", 40);
                    addGridBoundColumn("BillingAmount", "Amount", 150);
                    addGridBoundColumn("BalanceAmount", "Balance", 80);
                    grdList.MasterTableView.ClientDataKeyNames = new string[]
                                                                     {
                                                                         "BillingID", "SubsidiaryID", "BankNotes",
                                                                         "BillingDate",
                                                                         "BillingDueDate", "SRCurrency", "BillingAmount",
                                                                         "BalanceAmount"
                                                                     };
                    break;

                #endregion

                default:
                    addGridBoundColumn(popUpSearchType + "ID", "ID", 100);
                    addGridBoundColumn(popUpSearchType + "Name", "Name", -1);

                    grdList.MasterTableView.ClientDataKeyNames = new string[] { popUpSearchType + "ID", popUpSearchType + "Name" };
                    break;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack)
                lastQuery = null;
            switch (PopupSearchType)
            {
                case "ServiceUnitItemService":
                    grdList.DataSource = ServiceUnitItemServices;
                    break;
                case "Appointment":
                    grdList.DataSource = Appointments;
                    break;
                case "Bank":
                    grdList.DataSource = Banks;
                    break;
                case "BankHrd":
                    grdList.DataSource = BankHrds;
                    break;
                case "Supplier":
                    grdList.DataSource = Suppliers;
                    break;
                case "Paramedic":
                    grdList.DataSource = Paramedics;
                    break;
                case "Department":
                    grdList.DataSource = Departments;
                    break;
                case "Location":
                    grdList.DataSource = Locations;
                    break;
                case "ItemGroup":
                    grdList.DataSource = ItemGroups;
                    break;
                case "ItemUnit":
                    grdList.DataSource = ItemUnits;
                    break;
                case "MarginRangeType":
                    grdList.DataSource = MarginRangeTypes;
                    break;
                case "ItemProductMargin":
                    grdList.DataSource = ItemProductMargins;
                    break;
                case "Fabric":
                    grdList.DataSource = Fabrics;
                    break;
                case "ServiceUnit":
                    grdList.DataSource = ServiceUnits;
                    break;
                case "ServiceRoom":
                    grdList.DataSource = ServiceRooms;
                    break;
                case "Class":
                    grdList.DataSource = Classes;
                    break;
                case "InPatientCoverageClass":
                case "ChargeClass":
                case "InPatientClass":
                    grdList.DataSource = InPatientClasses;
                    break;
                case "ServiceUnitVisitType":
                    grdList.DataSource = ServiceUnitVisitTypes;
                    break;
                case "ItemDiagnostic":
                    grdList.DataSource = GetItems(BusinessObject.Reference.ItemType.Diagnostic);
                    break;
                case "ItemService":
                    grdList.DataSource = GetItems(BusinessObject.Reference.ItemType.Service);
                    break;
                case "ItemLaboratory":
                    grdList.DataSource = GetItems(BusinessObject.Reference.ItemType.Laboratory);
                    break;
                case "ItemPackage":
                    grdList.DataSource = GetItems(BusinessObject.Reference.ItemType.Package);
                    break;
                case "ItemProductMedical":
                    grdList.DataSource = GetItems(BusinessObject.Reference.ItemType.Medical);
                    break;
                case "ItemProductNonMedical":
                    grdList.DataSource = GetItems(BusinessObject.Reference.ItemType.NonMedical);
                    break;
                case "ItemKitchen":
                    grdList.DataSource = GetItems(BusinessObject.Reference.ItemType.Kitchen);
                    break;
                case "ItemProductInventory":
                    grdList.DataSource = GetItems(BusinessObject.Reference.ItemType.Medical, BusinessObject.Reference.ItemType.NonMedical, BusinessObject.Reference.ItemType.Kitchen);
                    break;
                case "ItemDiagnosticTariff":
                    grdList.DataSource = GetItemWithTarriffs(BusinessObject.Reference.ItemType.Diagnostic);
                    break;
                case "ItemServiceTariff":
                    grdList.DataSource = GetItemWithTarriffs(BusinessObject.Reference.ItemType.Service);
                    break;
                case "ItemLaboratoryTariff":
                    grdList.DataSource = GetItemWithTarriffs(BusinessObject.Reference.ItemType.Laboratory);
                    break;
                case "ItemProductNonMedicalTariff":
                    grdList.DataSource = GetItemWithTarriffs(BusinessObject.Reference.ItemType.NonMedical);
                    break;
                case "ItemProductMedicalTariff":
                    grdList.DataSource = GetItemWithTarriffs(BusinessObject.Reference.ItemType.Medical);
                    break;
                case "Item":
                    grdList.DataSource = GetItems(string.Empty);
                    break;
                case "ItemServiceExcludeProduct":
                    grdList.DataSource = GetItems(new string[] { BusinessObject.Reference.ItemType.Service, BusinessObject.Reference.ItemType.Radiology, BusinessObject.Reference.ItemType.Laboratory, BusinessObject.Reference.ItemType.Diagnostic, BusinessObject.Reference.ItemType.Package });
                    break;
                case "ItemAndZatActive":
                    grdList.DataSource = GetItemsAndZatActive();
                    break;
                case "ItemTransactionListRequestOrder":
                    grdList.DataSource = ItemTransactionListRequestOrders;
                    break;
                case "Referral":
                    grdList.DataSource = Referrals;
                    break;
                case "Bed":
                    grdList.DataSource = Beds;
                    break;
                case "Diagnose":
                    grdList.DataSource = Diagnoses;
                    break;
                case "Permit":
                    grdList.DataSource = LocationPermits;
                    break;
                case "VisitType":
                    grdList.DataSource = VisitTypes;
                    break;
                case "ServiceUnitByServiceGroupID":
                    grdList.DataSource = ServiceUnitByServiceGroupIDs;
                    break;
                case "ServiceUnitByRegistrationType":
                    grdList.DataSource = ServiceUnitByRegistrationTypes;
                    break;
                case "NonInPatientCoverageClass":
                case "NonInPatientClass":
                    grdList.DataSource = NonInPatientClasses;
                    break;
                case "ServiceRoomByServiceUnitID":
                    grdList.DataSource = ServiceRoomByServiceUnitIDs;
                    break;
                case "RegistrationByRegType":
                    grdList.DataSource = RegistrationByRegTypes;
                    break;
                case "Registration":
                    grdList.DataSource = Registrations;
                    break;
                case "RegistrationByDepartment":
                    grdList.DataSource = RegistrationByDepartments;
                    break;
                case "BedByServiceRoomID":
                    grdList.DataSource = BedByServiceRoomIDs;
                    break;
                case "ParamedicByServiceUnitID":
                    grdList.DataSource = ParamedicByServiceUnitIDs;
                    break;
                case "MorphologyByDiagnoseID":
                    grdList.DataSource = MorphologyByDiagnoseIDs;
                    break;
                case "MedicalNo":
                    grdList.DataSource = PatientHaveMedicalNo;
                    break;
                case "Patient":
                    grdList.DataSource = Patients;
                    break;
                case "SubSpecialty":
                    grdList.DataSource = SubSpecialtys;
                    break;
                case "ParamedicByRegistration":
                    grdList.DataSource = ParamedicByRegistrations;
                    break;
                case "Procedure":
                    grdList.DataSource = Procedures;
                    break;
                case "User":
                    grdList.DataSource = Users;
                    break;
                case "ServiceUnitTransaction":
                    grdList.DataSource = ServiceUnitTransactions;
                    break;
                case "ServiceUnitJobOrder":
                    grdList.DataSource = ServiceUnitJobOrders;
                    break;
                case "TransactionNoByRegistration":
                    grdList.DataSource = TransactionNoByRegistrations;
                    break;
                case "JobOrderNoByRegistration":
                    grdList.DataSource = JobOrderNoByRegistrations;
                    break;
                case "ItemBalanceByLocation":
                    grdList.DataSource = ItemBalanceByLocations;
                    break;
                case "ItemTariffByServiceUnitTransaction":
                    grdList.DataSource = ItemTariffByServiceUnitTransactions;
                    break;
                case "ItemTariffByServiceUnitOrder":
                    grdList.DataSource = ItemTariffByServiceUnitOrders;
                    break;
                case "ItemJobOrder":
                    grdList.DataSource = ItemJobOrders;
                    break;
                case "ZipCode":
                    grdList.DataSource = ZipCodes;
                    break;
                case "ParamedicClusterDetail":
                    grdList.DataSource = ParamedicClusterDetails;
                    break;
                case "ServiceRoomInPatient":
                    grdList.DataSource = ServiceRoomInPatients;
                    break;
                case "ServiceUnitAutoBillItem":
                    grdList.DataSource = ServiceUnitAutoBillItems;
                    break;
                case "TransPrescriptionSales":
                    grdList.DataSource = TransPrescriptionSaleses;
                    break;

                case "AssetByServiceUnit":
                    grdList.DataSource = AssetByServiceUnits;
                    break;
                case "Pathway":
                    grdList.DataSource = Pathways;
                    break;
                //#region Finance

                //case "Accounts":
                //    grdList.DataSource = Accountss;
                //    break;
                //case "AcctLinkEmployee":
                //    grdList.DataSource = AcctLinkEmployees;
                //    break;
                //case "AcctLinkGuarantor":
                //    grdList.DataSource = AcctLinkGuarantors;
                //    break;
                //case "AcctLinkSupplier":
                //    grdList.DataSource = AcctLinkSuppliers;
                //    break;
                //case "AcctLinkLoanBank":
                //    grdList.DataSource = AcctLinkLoanBanks;
                //    break;
                //case "AcctInitialGL":
                //    grdList.DataSource = AcctInitialGLs;
                //    break;
                //case "AcctSubGroup":
                //    grdList.DataSource = AcctSubGroups;
                //    break;
                //case "BankAccount":
                //    grdList.DataSource = BankAccounts;
                //    break;
                //case "Donator":
                //    grdList.DataSource = Donators;
                //    break;
                //case "Employee":
                //    grdList.DataSource = Employees;
                //    break;
                case "Guarantor":
                    grdList.DataSource = Guarantors;
                    break;
                    //case "VoucherCodeReceive":
                    //    grdList.DataSource = VoucherCodeReceives;
                    //    break;
                    //case "VoucherCodePayment":
                    //    grdList.DataSource = VoucherCodePayments;
                    //    break;
                    //case "VoucherCodeMemorial":
                    //    grdList.DataSource = VoucherCodeMemorials;
                    //    break;
                    //case "VoucherCodeAutomatic":
                    //    grdList.DataSource = VoucherCodeAutomatics;
                    //    break;
                    //case "BillGurantor":
                    //    grdList.DataSource = BillGurantors;
                    //    break;
                    //case "BillSupplier":
                    //    grdList.DataSource = BillSuppliers;
                    //    break;
                    //case "BillDonator":
                    //    grdList.DataSource = BillDonators;
                    //    break;
                    //case "BillEmployee":
                    //    grdList.DataSource = BillEmployees;
                    //    break;
                    //case "BillLoanBank":
                    //    grdList.DataSource = BillLoanBanks;
                    //    break;

                    //#endregion


            }
        }

        #region Data Source

        private object lastQuery
        {
            get { return Session["lastQueryPopup" + (string)ViewState["popUpSearch"]]; }
            set
            {
                if (value == null)
                    Session.Remove("lastQueryPopup" + (string)ViewState["popUpSearch"]);
                else
                    Session["lastQueryPopup" + (string)ViewState["popUpSearch"]] = value;

            }
        }

        private DataTable ServiceUnitItemServices
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ItemQuery query = new ItemQuery("a");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                    query.ItemID,
                    query.ItemName
                    );

                query.Where
                    (
                    query.IsActive == true,
                    query.SRItemType.NotIn(BusinessObject.Reference.ItemType.Medical,
                                           BusinessObject.Reference.ItemType.NonMedical)
                    );

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                            query.Or
                                (
                                    query.ItemID.Like(searchTextContain),
                                    query.ItemName.Like(searchTextContain)
                                )
                        );
                }

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable ServiceUnitAutoBillItems
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ItemQuery query = new ItemQuery("a");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.ItemID,
                        query.ItemName
                    );

                query.Where
                    (
                        query.IsActive == true,
                        query.SRItemType.In
                            (
                                BusinessObject.Reference.ItemType.Medical,
                                BusinessObject.Reference.ItemType.NonMedical,
                                BusinessObject.Reference.ItemType.Service
                            )
                    );

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                            query.Or
                                (
                                    query.ItemID.Like(searchTextContain),
                                    query.ItemName.Like(searchTextContain)
                                )
                        );
                }

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable Users
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                AppUserQuery query = new AppUserQuery("a");
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.UserID.Like(searchTextContain),
                            query.UserName.Like(searchTextContain)
                            )
                        );
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable ParamedicClusterDetails
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ParamedicQuery query = new ParamedicQuery("a");
                ServiceUnitParamedicQuery unit = new ServiceUnitParamedicQuery("b");

                query.InnerJoin(unit).On(query.ParamedicID == unit.ParamedicID);

                string strParam = Request.QueryString["filterBase"];
                string[] arrParam = strParam.Split('|');
                //if (!string.IsNullOrEmpty(arrParam[1]))
                //    query.Where
                //        (
                //            unit.ServiceUnitID == arrParam[0],
                //            query.ParamedicID.NotIn(arrParam[1])
                //        );
                //else
                query.Where(unit.ServiceUnitID == arrParam[0]);

                query.Where
                    (
                    query.IsActive == true,
                    query.IsAvailable == true
                    );
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.ParamedicID.Like(searchTextContain),
                            query.ParamedicName.Like(searchTextContain)
                            )
                        );

                }
                query.Select(query.ParamedicID, query.ParamedicName);

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable TransactionNoByRegistrations
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                TransChargesQuery query = new TransChargesQuery("a");
                ServiceUnitQuery unit = new ServiceUnitQuery("b");
                RegistrationQuery reg = new RegistrationQuery("c");
                PatientQuery patient = new PatientQuery("d");

                query.InnerJoin(unit).On(query.FromServiceUnitID == unit.ServiceUnitID);
                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);

                //esQueryItem date = new esQueryItem(query, "TransactionDate", esSystemType.String);
                //date = DateTime.Parse(query.TransactionDate.ToString()).ToString("dd/MM/yyyy");

                query.Where
                    (
                    query.RegistrationNo == Request.QueryString["filterBase"],
                    query.IsVoid == false,
                    query.IsOrder == false
                    );

                query.Select
                    (
                    query.TransactionNo,
                    query.TransactionDate,
                    unit.ServiceUnitName,
                    query.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable ServiceUnitJobOrders
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ServiceUnitQuery query = new ServiceUnitQuery("a");
                DepartmentQuery dept = new DepartmentQuery("b");
                query.InnerJoin(dept).On(query.DepartmentID == dept.DepartmentID);
                query.Where
                    (
                    query.IsUsingJobOrder == true,
                    //query.IsPatientTransaction == true,
                    query.IsActive == true
                    );
                query.Select
                    (
                    query.ServiceUnitID,
                    query.ServiceUnitName,
                    dept.DepartmentName
                    );
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.ServiceUnitID.Like(searchTextContain),
                            query.ServiceUnitName.Like(searchTextContain),
                            dept.DepartmentName.Like(searchTextContain)
                            )
                        );
                }

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable ServiceUnitTransactions
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ServiceUnitQuery query = new ServiceUnitQuery("a");
                DepartmentQuery dept = new DepartmentQuery("b");
                query.InnerJoin(dept).On(query.DepartmentID == dept.DepartmentID);
                query.Where
                    (
                    query.IsUsingJobOrder == false,
                    query.IsPatientTransaction == true,
                    query.IsActive == true
                    );
                query.Select
                    (
                    query.ServiceUnitID,
                    query.ServiceUnitName,
                    dept.DepartmentName
                    );
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.ServiceUnitID.Like(searchTextContain),
                            query.ServiceUnitName.Like(searchTextContain),
                            dept.DepartmentName.Like(searchTextContain)
                            )
                        );
                }

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable Procedures
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ProcedureQuery query = new ProcedureQuery("a");
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.ProcedureID.Like(searchTextContain),
                            query.ProcedureName.Like(searchTextContain)
                            )
                        );
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable ZipCodes
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ZipCodeQuery query = new ZipCodeQuery("a");
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.ZipCode.Like(searchTextContain),
                            query.StreetName.Like(searchTextContain)
                            )
                        );
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable ParamedicByRegistrations
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ParamedicQuery param;

                //paramedec from registration
                RegistrationQuery reg = new RegistrationQuery("a");
                param = new ParamedicQuery("b");
                reg.es.Top = AppSession.Parameter.MaxResultRecord;

                reg.InnerJoin(param).On(reg.ParamedicID == param.ParamedicID);
                reg.Where
                    (
                    reg.RegistrationNo == Page.Request.QueryString["filterBase"],
                    reg.IsClosed == false
                    );
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    reg.Where
                        (
                        reg.Or
                            (
                            reg.ParamedicID.Like(searchTextContain),
                            param.ParamedicName.Like(searchTextContain)
                            )
                        );
                }
                DataTable dtbReg = reg.LoadDataTable();

                //paramedic from service unit que
                ServiceUnitQueQuery que = new ServiceUnitQueQuery("a");
                param = new ParamedicQuery("b");

                que.es.Top = AppSession.Parameter.MaxResultRecord;
                que.es.Distinct = true;
                que.Select
                    (
                    que.ParamedicID,
                    param.ParamedicName
                    );
                que.InnerJoin(param).On(que.ParamedicID == param.ParamedicID);
                que.Where
                    (
                    que.RegistrationNo == Page.Request.QueryString["filterBase"],
                    que.IsClosed == false
                    );
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    que.Where
                        (
                        que.Or
                            (
                            que.ParamedicID.Like(searchTextContain),
                            param.ParamedicName.Like(searchTextContain)
                            )
                        );
                }
                DataTable dtbQue = que.LoadDataTable();

                //merge data
                dtbReg.Merge(dtbQue, true);

                lastQuery = dtbReg;
                return dtbReg;
            }
        }

        private DataTable MorphologyByDiagnoseIDs
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                MorphologyQuery query = new MorphologyQuery("a");
                query.Where(query.DiagnoseID == Page.Request.QueryString["filterBase"]);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.MorphologyID.Like(searchTextContain),
                            query.MorphologyName.Like(searchTextContain)
                            )
                        );

                    query.Select(query.MorphologyID, query.MorphologyName);
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable ParamedicByServiceUnitIDs
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ServiceUnitParamedicQuery unit = new ServiceUnitParamedicQuery("a");
                ParamedicQuery query = new ParamedicQuery("b");

                unit.InnerJoin(query).On(unit.ParamedicID == query.ParamedicID);
                unit.Where
                    (
                    unit.ServiceUnitID == Page.Request.QueryString["filterBase"],
                    query.IsActive == true,
                    query.IsAvailable == true
                    );
                unit.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    unit.Where
                        (
                        unit.Or
                            (
                            query.ParamedicID.Like(searchTextContain),
                            query.ParamedicName.Like(searchTextContain)
                            )
                        );
                }
                unit.Select(query.ParamedicID, query.ParamedicName);
                DataTable dtb = unit.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable RegistrationByRegTypes
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                RegistrationQuery query = new RegistrationQuery("a");
                PatientQuery patient = new PatientQuery("b");

                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);

                query.Where
                    (
                    query.SRRegistrationType == Page.Request.QueryString["filterBase"],
                    query.IsClosed == false,
                    query.IsVoid == false
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string[] texts = txtSearch.Text.Split(' ');
                    if (texts.Length > 1)
                    {
                        string searchTextContain0 = string.Format("%{0}%", texts[0]);
                        string searchTextContain1 = string.Format("%{0}%", texts[1]);
                        query.Where(
                            patient.FirstName.Like(searchTextContain0),
                            patient.MiddleName.Like(searchTextContain1)
                            );
                        if (texts.Length > 2)
                        {
                            string searchTextContain2 = string.Format("%{0}%", texts[2]);
                            query.Where(patient.LastName.Like(searchTextContain2));
                        }
                    }
                    else
                    {
                        string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                        query.Where
                                (
                                query.Or
                                    (
                                    patient.FirstName.Like(searchTextContain),
                                    query.RegistrationNo.Like(searchTextContain),
                                    patient.MiddleName.Like(searchTextContain),
                                    patient.LastName.Like(searchTextContain)
                                    )
                                );
                    }
                }

                query.Select
                    (
                    query.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName.As("PatientName")
                    );

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable Registrations
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                RegistrationQuery query = new RegistrationQuery("a");
                PatientQuery patient = new PatientQuery("b");

                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);

                query.Where
                    (
                    query.DepartmentID == Page.Request.QueryString["filterBase"],
                    query.IsClosed == false,
                    query.IsVoid == false
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string[] texts = txtSearch.Text.Split(' ');
                    if (texts.Length > 1)
                    {
                        string searchTextContain0 = string.Format("%{0}%", texts[0]);
                        string searchTextContain1 = string.Format("%{0}%", texts[1]);
                        query.Where(
                            patient.FirstName.Like(searchTextContain0),
                            patient.MiddleName.Like(searchTextContain1)
                            );
                        if (texts.Length > 2)
                        {
                            string searchTextContain2 = string.Format("%{0}%", texts[2]);
                            query.Where(patient.LastName.Like(searchTextContain2));
                        }
                    }
                    else
                    {
                        string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                        query.Where
                            (
                            query.Or
                                (
                                patient.FirstName.Like(searchTextContain),
                                query.RegistrationNo.Like(searchTextContain),
                                patient.MiddleName.Like(searchTextContain),
                                patient.LastName.Like(searchTextContain)
                                )
                            );
                    }
                }

                query.Select
                    (
                    query.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName.As("PatientName")
                    );

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable RegistrationByDepartments
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                RegistrationQuery query = new RegistrationQuery("a");
                PatientQuery patient = new PatientQuery("b");

                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);

                query.Where
                    (
                    query.DepartmentID == Page.Request.QueryString["filterBase"],
                    query.IsClosed == false,
                    query.IsVoid == false
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.RegistrationNo.Like(searchTextContain),
                            patient.FirstName.Like(searchTextContain)
                            )
                        );
                }

                query.Select
                    (
                    query.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName.As("PatientName")
                    );

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        //private DataTable Accounts
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        AccountsQuery query = new AccountsQuery("a");
        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.AccountID.Like(string.Format("%.{0}%", txtSearch.Text)),
        //                    query.AccountName.Like(string.Format("%.{0}%", txtSearch.Text))
        //                    )
        //                );

        //            query.Select(query.AccountID, query.AccountName);
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        private DataTable Diagnoses
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                DiagnoseQuery query = new DiagnoseQuery("a");
                query.Where(query.IsActive == true);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.DiagnoseID.Like(searchTextContain),
                            query.DiagnoseName.Like(searchTextContain)
                            )
                        );

                    query.Select(query.DiagnoseID, query.DiagnoseName);
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable Beds
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                BedQuery query = new BedQuery("a");

                ServiceRoomQuery srq = new ServiceRoomQuery("b");
                query.InnerJoin(srq).On(query.RoomID == srq.RoomID);

                ClassQuery cq = new ClassQuery("c");
                query.InnerJoin(cq).On(query.ClassID == cq.ClassID);

                AppStandardReferenceItemQuery asriq = new AppStandardReferenceItemQuery("d");
                query.InnerJoin(asriq).On(query.SRBedStatus == asriq.ItemID);

                query.Where
                    (
                    query.IsActive == true,
                    query.SRBedStatus == AppSession.Parameter.BedStatusUnoccupied
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.BedID.Like(searchTextContain),
                            srq.RoomName.Like(searchTextContain)
                            )
                        );
                }

                query.Select
                    (
                    srq.RoomName,
                    cq.ClassName,
                    query.BedID,
                    asriq.ItemName.As("BedStatus")
                    );

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable BedByServiceRoomIDs
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                BedQuery query = new BedQuery("a");

                ServiceRoomQuery srq = new ServiceRoomQuery("b");
                query.InnerJoin(srq).On(query.RoomID == srq.RoomID);

                ClassQuery cq = new ClassQuery("c");
                query.InnerJoin(cq).On(query.ClassID == cq.ClassID);

                AppStandardReferenceItemQuery asriq = new AppStandardReferenceItemQuery("d");
                query.InnerJoin(asriq).On(query.SRBedStatus == asriq.ItemID);

                query.Where
                    (
                    query.RoomID == Page.Request.QueryString["filterBase"],
                    query.IsActive == true //,
                                           //query.SRBedStatus == AppSession.Parameter.BedStatusUnoccupied
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.BedID.Like(searchTextContain),
                            srq.RoomName.Like(searchTextContain)
                            )
                        );
                }

                query.Select
                    (
                    srq.RoomName,
                    cq.ClassName,
                    query.BedID,
                    asriq.ItemName.As("BedStatus")
                    );

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable Referrals
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ReferralQuery query = new ReferralQuery("a");
                query.Where(query.IsActive == true);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.ReferralID.Like(searchTextContain),
                            query.ReferralName.Like(searchTextContain)
                            )
                        );
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable JobOrderNoByRegistrations
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                TransChargesQuery query = new TransChargesQuery("a");
                ServiceUnitQuery unit = new ServiceUnitQuery("b");
                RegistrationQuery reg = new RegistrationQuery("c");
                PatientQuery patient = new PatientQuery("d");

                query.InnerJoin(unit).On(query.FromServiceUnitID == unit.ServiceUnitID);
                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);

                query.Where
                    (
                    query.RegistrationNo == Request.QueryString["filterBase"],
                    query.IsVoid == false,
                    query.IsOrder == true
                    );

                query.Select
                    (
                    query.TransactionNo,
                    query.TransactionDate,
                    unit.ServiceUnitName,
                    query.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable TransPrescriptionSaleses
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                TransPrescriptionQuery query = new TransPrescriptionQuery("a");
                ServiceUnitQuery unit = new ServiceUnitQuery("b");
                RegistrationQuery reg = new RegistrationQuery("c");
                PatientQuery patient = new PatientQuery("d");

                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);

                query.Where
                    (
                        query.IsVoid == false,
                        query.IsApproval == true,
                        query.IsPrescriptionReturn == false
                    );

                query.Select
                    (
                        query.PrescriptionNo,
                        query.PrescriptionDate,
                        unit.ServiceUnitName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where(query.PrescriptionNo.Like(searchTextContain));
                }

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable ItemTariffByServiceUnitTransactions
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                string[] param = Request.QueryString["filterBase"].Split('|');

                ItemQuery query = new ItemQuery("a");
                ItemTariffQuery tariff = new ItemTariffQuery("b");
                ServiceUnitItemServiceQuery itemUnit = new ServiceUnitItemServiceQuery("c");
                ItemBalanceQuery balance = new ItemBalanceQuery("d");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        balance.Balance.Coalesce("0").As("Balance"),
                        tariff.Price.Coalesce("0").As("Price"),
                        query.SRItemType,
                        itemUnit.ServiceUnitID.Coalesce("''")
                    );

                query.LeftJoin(tariff).On(query.ItemID == tariff.ItemID);
                query.InnerJoin(itemUnit).On(query.ItemID == itemUnit.ItemID);
                query.LeftJoin(balance).On
                    (
                        query.ItemID == balance.ItemID &
                        balance.LocationID == param[1]
                    );

                query.Where
                    (
                        query.IsActive == true//,
                                              //tariff.ClassID == param[2]
                    );

                query.OrderBy
                    (
                        query.ItemName.Ascending,
                        tariff.StartingDate.Descending
                    );

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                            query.Or
                                (
                                    query.ItemName.Like(searchTextContain),
                                    query.ItemID.Like(searchTextContain)
                                )
                        );
                }

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;

                foreach (DataRow row in dtb.Rows)
                {
                    if ((string)row["SRItemType"] == BusinessObject.Reference.ItemType.Medical ||
                        (string)row["SRItemType"] == BusinessObject.Reference.ItemType.NonMedical)
                    {
                        //if ((decimal)row["Balance"] == 0)
                        //    row.Delete();
                    }
                    else
                    {
                        if ((string)row["ServiceUnitID"] != param[0])
                            row.Delete();
                    }
                }

                dtb.AcceptChanges();

                String item1 = string.Empty;
                String item2 = string.Empty;
                foreach (DataRow row in dtb.Rows)
                {
                    item1 = (string)row["ItemID"];
                    if (item1 != item2)
                        item2 = (string)row["ItemID"];
                    else
                        row.Delete();
                }

                dtb.AcceptChanges();

                return dtb;
            }
        }

        private DataTable ItemTariffByServiceUnitOrders
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                string[] param = Request.QueryString["filterBase"].Split('|');
                ;

                if (param.Length == 1)
                    return null;

                ItemQuery query = new ItemQuery("a");
                ItemTariffQuery tariff = new ItemTariffQuery("b");
                ServiceUnitItemServiceQuery itemUnit = new ServiceUnitItemServiceQuery("c");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        tariff.Price.Coalesce("0").As("Price")
                    );

                query.LeftJoin(tariff).On(query.ItemID == tariff.ItemID);
                query.InnerJoin(itemUnit).On(query.ItemID == itemUnit.ItemID);

                query.Where
                    (
                        query.IsActive == true,
                        //tariff.ClassID == param[1],
                        itemUnit.ServiceUnitID == param[0],
                        query.SRItemType.NotIn
                            (
                                BusinessObject.Reference.ItemType.Medical,
                                BusinessObject.Reference.ItemType.NonMedical
                            )
                    );

                query.OrderBy
                    (
                        query.ItemName.Ascending,
                        tariff.StartingDate.Descending
                    );

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                            query.Or
                                (
                                    query.ItemName.Like(searchTextContain),
                                    query.ItemID.Like(searchTextContain)
                                )
                        );
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;

                String item1 = string.Empty;
                String item2 = string.Empty;
                foreach (DataRow row in dtb.Rows)
                {
                    item1 = (string)row["ItemID"];
                    if (item1 != item2)
                        item2 = (string)row["ItemID"];
                    else
                        row.Delete();
                }

                dtb.AcceptChanges();

                return dtb;
            }
        }

        private DataTable ItemBalanceByLocations
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ItemQuery query = new ItemQuery("a");
                ItemBalanceQuery balance = new ItemBalanceQuery("b");
                ItemTariffQuery tariff = new ItemTariffQuery("c");

                esQueryItem price = new esQueryItem(tariff, "price", esSystemType.Double);
                price = tariff.Price;

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.Select
                    (
                    query.ItemID,
                    query.ItemName,
                    balance.Balance,
                    price.As("Price")
                    );
                query.LeftJoin(balance).On(query.ItemID == balance.ItemID);
                query.LeftJoin(tariff).On(query.ItemID == tariff.ItemID);
                query.Where
                    (
                    query.SRItemType == BusinessObject.Reference.ItemType.Medical,
                    query.IsActive == true,
                    balance.LocationID == Request.QueryString["filterBase"]
                    );

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                            )
                        );
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable ItemJobOrders
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ItemQuery query = new ItemQuery("a");
                ServiceUnitItemServiceQuery itemUnit = new ServiceUnitItemServiceQuery("b");
                ServiceUnitQuery unit = new ServiceUnitQuery("c");
                ItemTariffQuery tariff = new ItemTariffQuery("d");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                esQueryItem price = new esQueryItem(tariff, "price", esSystemType.Double);
                price = tariff.Price;

                query.Select
                    (
                    query.ItemID,
                    query.ItemName,
                    unit.ServiceUnitName,
                    price.As("Price")
                    );

                query.InnerJoin(itemUnit).On(query.ItemID == itemUnit.ItemID);
                query.InnerJoin(unit).On
                    (
                    itemUnit.ServiceUnitID == unit.ServiceUnitID &
                    unit.IsUsingJobOrder == 1 &
                    unit.IsActive == 1
                    );
                query.InnerJoin(tariff).On
                    (
                    query.ItemID == tariff.ItemID &
                    tariff.ClassID == Request.QueryString["filterBase"] &
                    tariff.StartingDate <= DateTime.Now.Date.ToString(AppConstant.DisplayFormat.DateSql)
                    );

                query.Where(query.IsActive == true);
                query.OrderBy
                    (
                    query.ItemName.Ascending,
                    tariff.StartingDate.Descending
                    );

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                            )
                        );
                }

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;

                String item1 = string.Empty;
                String item2 = string.Empty;
                foreach (DataRow row in dtb.Rows)
                {
                    item1 = (string)row["ItemID"];
                    if (item1 != item2)
                        item2 = (string)row["ItemID"];
                    else
                        row.Delete();
                }

                dtb.AcceptChanges();

                return dtb;
            }
        }

        private DataTable ServiceRooms
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ServiceRoomQuery query = new ServiceRoomQuery("a");
                query.Where(query.IsActive == true);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.RoomID.Like(searchTextContain),
                            query.RoomName.Like(searchTextContain)
                            )
                        );

                }
                query.Select(query.RoomID, query.RoomName);

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable Classes
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ClassQuery query = new ClassQuery("a");
                query.Where(query.IsActive == true);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.ClassID.Like(searchTextContain),
                            query.ClassName.Like(searchTextContain)
                            )
                        );

                }
                query.Select(query.ClassID, query.ClassName);

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable InPatientClasses
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ClassQuery query = new ClassQuery("a");
                query.Where(query.IsActive == true, query.IsInPatientClass == true);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.ClassID.Like(searchTextContain),
                            query.ClassName.Like(searchTextContain)
                            )
                        );

                }
                query.Select(query.ClassID, query.ClassName);

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable NonInPatientClasses
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ClassQuery query = new ClassQuery("a");
                query.Where(query.IsActive == true, query.IsInPatientClass == false);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.ClassID.Like(searchTextContain),
                            query.ClassName.Like(searchTextContain)
                            )
                        );

                }
                query.Select(query.ClassID, query.ClassName);

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable Guarantors
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                GuarantorQuery query = new GuarantorQuery("a");
                query.Where(query.IsActive == true);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.GuarantorID.Like(searchTextContain),
                            query.GuarantorName.Like(searchTextContain)
                            )
                        );

                }
                query.Select(query.GuarantorID, query.GuarantorName);

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable Paramedics
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ParamedicQuery query = new ParamedicQuery("a");
                query.Where(query.IsActive == true, query.IsAvailable == true);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.ParamedicID.Like(searchTextContain),
                            query.ParamedicName.Like(searchTextContain)
                            )
                        );

                }
                query.Select(query.ParamedicID, query.ParamedicName);

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable Suppliers
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                SupplierQuery query = new SupplierQuery("a");
                query.Where(query.IsActive == true);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.SupplierID.Like(searchTextContain),
                            query.SupplierName.Like(searchTextContain)
                            )
                        );

                }
                query.Select(query.SupplierID, query.SupplierName, query.Address);

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable Departments
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                DepartmentQuery query = new DepartmentQuery("a");
                query.Where(query.IsActive == true);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.DepartmentID.Like(searchTextContain),
                            query.DepartmentName.Like(searchTextContain)
                            )
                        );

                }
                query.Select(query.DepartmentID, query.DepartmentName);

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable Patients
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                PatientQuery query = new PatientQuery("a");
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string[] texts = txtSearch.Text.Split(' ');
                    if (texts.Length > 1)
                    {
                        string searchTextContain0 = string.Format("%{0}%", texts[0]);
                        string searchTextContain1 = string.Format("%{0}%", texts[1]);
                        query.Where(
                            query.FirstName.Like(searchTextContain0),
                            query.MiddleName.Like(searchTextContain1)
                            );
                        if (texts.Length > 2)
                        {
                            string searchTextContain2 = string.Format("%{0}%", texts[2]);
                            query.Where(query.LastName.Like(searchTextContain2));
                        }
                    }
                    else
                    {
                        string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                        query.Where
                            (
                            query.Or
                                (
                                query.FirstName.Like(searchTextContain),
                                query.MedicalNo.Like(searchTextContain),
                                query.MiddleName.Like(searchTextContain),
                                query.LastName.Like(searchTextContain)
                                )
                            );
                    }
                }
                query.Select(query.PatientID, query.MedicalNo, query.PatientName, query.Address);

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable PatientHaveMedicalNo
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                PatientQuery query = new PatientQuery("a");
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.Where(query.MedicalNo > string.Empty);
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string[] texts = txtSearch.Text.Split(' ');
                    if (texts.Length > 1)
                    {
                        string searchTextContain0 = string.Format("%{0}%", texts[0]);
                        string searchTextContain1 = string.Format("%{0}%", texts[1]);
                        query.Where(
                            query.FirstName.Like(searchTextContain0),
                            query.MiddleName.Like(searchTextContain1)
                            );
                        if (texts.Length > 2)
                        {
                            string searchTextContain2 = string.Format("%{0}%", texts[2]);
                            query.Where(query.LastName.Like(searchTextContain2));
                        }
                    }
                    else
                    {
                        string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                        query.Where
                           (
                           query.Or
                               (
                               query.MedicalNo.Like(searchTextContain),
                               query.FirstName.Like(searchTextContain)
                               )
                           );
                    }
                }
                query.Select(query.MedicalNo, query.PatientName, query.Address);

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable Locations
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                LocationQuery query = new LocationQuery("a");
                query.Where(query.IsActive == true);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.LocationID.Like(searchTextContain),
                            query.LocationName.Like(searchTextContain)
                            )
                        );

                    query.Select(query.LocationID, query.LocationName);
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable ItemGroups
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ItemGroupQuery query = new ItemGroupQuery("a");
                query.Where(query.IsActive == true);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.ItemGroupID.Like(searchTextContain),
                            query.ItemGroupName.Like(searchTextContain)
                            )
                        );

                    query.Select(query.ItemGroupID, query.ItemGroupName);
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable ItemUnits
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                AppStandardReferenceItemQuery query = new AppStandardReferenceItemQuery("a");
                query.Where
                    (
                    query.IsActive == true,
                    query.IsUsedBySystem == true
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.StandardReferenceID.Equal("ItemUnit"),
                        query.Or
                            (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                            )
                        );
                }
                else
                    query.Where(query.StandardReferenceID.Equal("ItemUnit"));
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable MarginRangeTypes
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                AppStandardReferenceItemQuery query = new AppStandardReferenceItemQuery("a");
                query.Where(query.IsActive == true, query.IsUsedBySystem == true);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.StandardReferenceID.Equal("MarginRangeType"),
                        query.Or
                            (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                            )
                        );
                }
                else
                {
                    query.Where
                        (
                        query.StandardReferenceID.Equal("MarginRangeType")
                        );
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable ItemProductMargins
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ItemProductMarginQuery query = new ItemProductMarginQuery("a");
                query.Where(query.IsActive == true);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.MarginID.Like(searchTextContain),
                            query.MarginName.Like(searchTextContain)
                            )
                        );
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable Fabrics
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                FabricQuery query = new FabricQuery("a");
                query.Where(query.IsActive == true);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.FabricID.Like(searchTextContain),
                            query.FabricName.Like(searchTextContain)
                            )
                        );
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable VisitTypes
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                VisitTypeQuery query = new VisitTypeQuery("a");
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.VisitTypeID.Like(searchTextContain),
                            query.VisitTypeName.Like(searchTextContain)
                            )
                        );
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable ServiceUnitVisitTypes
        {
            get
            {
                // Reset Untuk handle bila filterBase nya berubah
                if (!IsPostBack)
                    lastQuery = null;

                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ServiceUnitVisitTypeQuery query = new ServiceUnitVisitTypeQuery("a");
                VisitTypeQuery visitTypeQuery = new VisitTypeQuery("b");
                query.InnerJoin(visitTypeQuery).On(query.VisitTypeID == visitTypeQuery.VisitTypeID);
                query.Select
                    (
                    query.VisitTypeID,
                    visitTypeQuery.VisitTypeName,
                    query.VisitDuration
                    );
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                string serviceUnitID = Page.Request.QueryString["filterBase"];
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.ServiceUnitID.Equal(serviceUnitID),
                        query.Or
                            (
                            query.VisitTypeID.Like(searchTextContain),
                            visitTypeQuery.VisitTypeName.Like(searchTextContain)
                            )
                        );
                }
                else
                {
                    query.Where
                        (
                        query.ServiceUnitID.Equal(serviceUnitID)
                        );
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable ServiceUnits
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ServiceUnitQuery query = new ServiceUnitQuery("a");
                DepartmentQuery dept = new DepartmentQuery("b");
                query.InnerJoin(dept).On(query.DepartmentID == dept.DepartmentID);
                query.Where(query.IsActive == true);
                query.Select
                    (
                    query.ServiceUnitID,
                    query.ServiceUnitName,
                    dept.DepartmentName
                    );
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.ServiceUnitID.Like(searchTextContain),
                            query.ServiceUnitName.Like(searchTextContain),
                            dept.DepartmentName.Like(searchTextContain)
                            )
                        );
                }

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable ServiceUnitByRegistrationTypes
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ServiceUnitQuery query = new ServiceUnitQuery("a");
                DepartmentQuery dept = new DepartmentQuery("b");
                query.InnerJoin(dept).On(query.DepartmentID == dept.DepartmentID);
                query.Where
                    (
                    query.IsActive == true,
                    query.SRRegistrationType == Page.Request.QueryString["filterBase"]
                    );
                query.Select
                    (
                    query.ServiceUnitID,
                    query.ServiceUnitName,
                    dept.DepartmentName
                    );
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.ServiceUnitID.Like(searchTextContain),
                            query.ServiceUnitName.Like(searchTextContain),
                            dept.DepartmentName.Like(searchTextContain)
                            )
                        );
                }

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable ServiceUnitByServiceGroupIDs
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ServiceUnitQuery query = new ServiceUnitQuery("a");
                DepartmentQuery dept = new DepartmentQuery("b");
                query.InnerJoin(dept).On(query.DepartmentID == dept.DepartmentID);
                query.Where
                    (
                    query.IsActive == true,
                    query.DepartmentID == Page.Request.QueryString["filterBase"]
                    );
                query.Select
                    (
                    query.ServiceUnitID,
                    query.ServiceUnitName,
                    dept.DepartmentName
                    );
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.ServiceUnitID.Like(searchTextContain),
                            query.ServiceUnitName.Like(searchTextContain),
                            dept.DepartmentName.Like(searchTextContain)
                            )
                        );
                }

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable LocationPermits
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                LocationPermitQuery query = new LocationPermitQuery("a");
                query.Where(query.IsActive == true);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.PermitID.Like(searchTextContain),
                            query.PermitName.Like(searchTextContain)
                            )
                        );
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable ServiceRoomByServiceUnitIDs
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ServiceRoomQuery query = new ServiceRoomQuery("a");

                query.Where
                    (
                    query.IsActive == true,
                    query.ServiceUnitID == Page.Request.QueryString["filterBase"]
                    );
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.RoomID.Like(searchTextContain),
                            query.RoomName.Like(searchTextContain)
                            )
                        );

                    query.Select(query.RoomID, query.RoomName);
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable ServiceRoomInPatients
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                ServiceRoomQuery query = new ServiceRoomQuery("a");
                ServiceUnitQuery unit = new ServiceUnitQuery("b");
                DepartmentQuery dept = new DepartmentQuery("c");

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.Select(query.RoomID, query.RoomName);

                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.InnerJoin(dept).On
                    (
                    unit.DepartmentID == dept.DepartmentID &
                    dept.DepartmentID == AppSession.Parameter.InPatientDepartmentID
                    );

                query.Where(query.IsActive == true);

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.RoomID.Like(searchTextContain),
                            query.RoomName.Like(searchTextContain)
                            )
                        );
                }

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable SubSpecialtys
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                SubSpecialtyQuery query = new SubSpecialtyQuery("a");
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.SubSpecialtyID.Like(searchTextContain),
                            query.SubSpecialtyName.Like(searchTextContain)
                            )
                        );

                    query.Select(query.SubSpecialtyID, query.SubSpecialtyName);
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable Appointments
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                AppointmentQuery query = new AppointmentQuery("a");
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.AppointmentNo.Like(searchTextContain),
                            query.FirstName.Like(searchTextContain)
                            )
                        );

                    query.Select(query.AppointmentNo, query.AppointmentDate, query.FirstName, query.StreetName);
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable ItemTransactionListRequestOrders
        {
            get
            {
                //object obj = lastQuery;
                //if (obj != null)
                //    return ((DataTable)(obj));

                ItemTransactionQuery query = new ItemTransactionQuery("a");
                ServiceUnitQuery unit = new ServiceUnitQuery("b");
                AppStandardReferenceItemQuery std = new AppStandardReferenceItemQuery("c");
                ItemTransactionQuery po = new ItemTransactionQuery("d");

                query.LeftJoin(unit).On(query.FromServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(std).On(query.SRItemType == std.ItemID && std.StandardReferenceID == "ItemType");
                query.LeftJoin(po).On(po.ReferenceNo == query.TransactionNo &&
                                      po.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseOrder);

                query.Where
                    (
                    query.IsVoid == false,
                    query.IsApproved == true,
                    query.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseRequest,
                    po.ReferenceNo.IsNull()
                    );

                query.Select
                    (
                    query.TransactionNo,
                    query.TransactionDate,
                    unit.ServiceUnitName,
                    std.ItemName
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable AssetByServiceUnits
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                string strParam = Request.QueryString["filterBase"];
                string[] arrParam = strParam.Split('|');

                var query = new AssetQuery("a");

                query.Where
                    (
                    query.ServiceUnitID == arrParam[0],
                    query.MaintenanceServiceUnitID == arrParam[1],
                    query.SRAssetsStatus == AppSession.Parameter.AssetsStatusActive
                    );

                query.Select
                    (
                    query.AssetID,
                    query.AssetName,
                    query.SerialNumber,
                    query.BrandName
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where(query.AssetName.Like(searchTextContain));
                }
                    
                query.OrderBy(query.AssetName.Ascending, query.AssetID.Ascending);

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }
        private DataTable Pathways
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                var query = new PathwayQuery("a");

                query.Select
                    (
                    query.PathwayID,
                    query.PathwayName
                    );

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where(query.PathwayName.Like(searchTextContain));
                }
                query.Where(query.IsActive == true);
                query.OrderBy(query.PathwayName.Ascending);

                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        //#region Finance

        //private DataTable Accountss
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        AccountsQuery query = new AccountsQuery("a");
        //        query.Where(query.SRAcctLevel.Trim().GreaterThan("3"));

        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where(query.SRAcctLevel.Trim().GreaterThan
        //                            ("3"));
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.AccountID.Like(string.Format("%.{0}%",
        //                                                       txtSearch.Text)),
        //                    query.AccountName.Like(string.Format("%.{0}%",
        //                                                         txtSearch.Text))
        //                    )
        //                );
        //            query.Select(query.AccountID.As("AccountID"),
        //                         query.AccountName);
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        //private DataTable AcctLinkEmployees
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        AccountsQuery query = new AccountsQuery("a");
        //        query.Where(query.SRAcctLevel.Trim().GreaterThan("3"));
        //        query.Where(query.SRAcctLink.Trim().Equal
        //                        (AppConstant.BillingLink.EMPLOYEE));

        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where(query.SRAcctLevel.Trim().GreaterThan
        //                            ("3"));
        //            query.Where(query.SRAcctLink.Trim().Equal
        //                            (AppConstant.BillingLink.EMPLOYEE));
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.AccountID.Like(string.Format("%.{0}%",
        //                                                       txtSearch.Text)),
        //                    query.AccountName.Like(string.Format("%.{0}%",
        //                                                         txtSearch.Text))
        //                    )
        //                );
        //            query.Select(query.AccountID.As("AccountID"),
        //                         query.AccountName);
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        //private DataTable AcctLinkGuarantors
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        AccountsQuery query = new AccountsQuery("a");
        //        query.Where(query.SRAcctLevel.Trim().GreaterThan("3"));
        //        query.Where(query.SRAcctLink.Trim().Equal
        //                        (AppConstant.BillingLink.GUARANTOR));

        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where(query.SRAcctLevel.Trim().GreaterThan
        //                            ("3"));
        //            query.Where(query.SRAcctLink.Trim().Equal
        //                            (AppConstant.BillingLink.GUARANTOR));
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.AccountID.Like(string.Format("%.{0}%",
        //                                                       txtSearch.Text)),
        //                    query.AccountName.Like(string.Format("%.{0}%",
        //                                                         txtSearch.Text))
        //                    )
        //                );
        //            query.Select(query.AccountID.As("AccountID"),
        //                         query.AccountName);
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        //private DataTable AcctLinkSuppliers
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        AccountsQuery query = new AccountsQuery("a");
        //        query.Where(query.SRAcctLevel.Trim().GreaterThan("3"));
        //        query.Where(query.SRAcctLink.Trim().Equal
        //                        (AppConstant.BillingLink.SUPPLIER));

        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where(query.SRAcctLevel.Trim().GreaterThan
        //                            ("3"));
        //            query.Where(query.SRAcctLink.Trim().Equal
        //                            (AppConstant.BillingLink.SUPPLIER));
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.AccountID.Like(string.Format("%.{0}%",
        //                                                       txtSearch.Text)),
        //                    query.AccountName.Like(string.Format("%.{0}%",
        //                                                         txtSearch.Text))
        //                    )
        //                );
        //            query.Select(query.AccountID.As("AccountID"),
        //                         query.AccountName);
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        //private DataTable AcctLinkLoanBanks
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        AccountsQuery query = new AccountsQuery("a");
        //        query.Where(query.SRAcctLevel.Trim().GreaterThan("3"));
        //        query.Where(query.SRAcctLink.Trim().Equal
        //                        (AppConstant.BillingLink.LOAN_BANK));

        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where(query.SRAcctLevel.Trim().GreaterThan
        //                            ("3"));
        //            query.Where(query.SRAcctLink.Trim().Equal
        //                            (AppConstant.BillingLink.LOAN_BANK));
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.AccountID.Like(string.Format("%.{0}%",
        //                                                       txtSearch.Text)),
        //                    query.AccountName.Like(string.Format("%.{0}%",
        //                                                         txtSearch.Text))
        //                    )
        //                );
        //            query.Select(query.AccountID.As("AccountID"),
        //                         query.AccountName);
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        //private DataTable AcctInitialGLs
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        AccountsQuery query = new AccountsQuery("a");
        //        query.Where(query.SRAcctLevel.Trim().GreaterThan("3"));
        //        query.Where(query.SRAcctGroup.Trim().LessThan
        //                        (AppConstant.AcctGroup.REVENUE));

        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where(query.SRAcctLevel.Trim().GreaterThan
        //                            ("3"));
        //            query.Where(query.SRAcctGroup.Trim().LessThan
        //                            (AppConstant.AcctGroup.REVENUE));
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.AccountID.Like(string.Format("%.{0}%",
        //                                                       txtSearch.Text)),
        //                    query.AccountName.Like(string.Format("%.{0}%",
        //                                                         txtSearch.Text))
        //                    )
        //                );
        //            query.Select(query.AccountID.As("AccountID"),
        //                         query.AccountName);
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        //private DataTable AcctSubGroups
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        AcctSubGroupQuery query = new AcctSubGroupQuery("a");
        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.AcctSubGroupID.Like(string.Format("%.{0}%", txtSearch.Text)),
        //                    query.AcctSubGroupName.Like(string.Format("%.{0}%", txtSearch.Text))
        //                    )
        //                );

        //            query.Select(query.AcctSubGroupID.As
        //                             ("AcctSubGroupID"), query.AcctSubGroupName);
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        private DataTable Banks
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                BankQuery query = new BankQuery("a");
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (
                        query.Or
                            (
                            query.BankID.Like(searchTextContain),
                            query.BankName.Like(searchTextContain)
                            )
                        );

                    query.Select(query.BankID, query.BankName);
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        private DataTable BankHrds
        {
            get
            {
                object obj = lastQuery;
                if (obj != null)
                    return ((DataTable)(obj));

                var query = new AppStandardReferenceItemQuery("a");
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                        (query.StandardReferenceID == "BankHRD",
                        query.Or
                            (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                            ),
                        query.IsActive == true
                        );

                    query.Select(query.ItemID, query.ItemName);
                }
                DataTable dtb = query.LoadDataTable();
                lastQuery = dtb;
                return dtb;
            }
        }

        //private DataTable BankAccounts
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        BankAccountQuery query = new BankAccountQuery("a");
        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.BankAccountNo.Like(string.Format("%.{0}%", txtSearch.Text)),
        //            query.Notes.Like(string.Format("%.{0}%", txtSearch.Text))
        //            )
        //                )
        //            ;

        //            query.Select(query.BankAccountNo.As("BankAccountNo"),
        //                         query.Notes);
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        //private DataTable Donators
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        DonatorQuery query = new DonatorQuery("a");
        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.DonatorID.Like(string.Format("%.{0}%",
        //                                                       txtSearch.Text)),
        //                    query.DonatorName.Like(string.Format("%.{0}%",
        //                                                         txtSearch.Text))
        //                    )
        //                );

        //            query.Select(query.DonatorID.As("DonatorID"),
        //                         query.DonatorName);
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        //private DataTable Employees
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        EmployeeQuery query = new EmployeeQuery("a");
        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.EmployeeID.Like(string.Format("%.{0}%",
        //                                                        txtSearch.Text)),
        //                    query.EmployeeName.Like(string.Format("%.{0}%",
        //                                                          txtSearch.Text))
        //                    )
        //                );

        //            query.Select(query.EmployeeID.As("EmployeeID"),
        //                         query.EmployeeName);
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        //private DataTable VoucherCodeReceives
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        VoucherCodeQuery query = new VoucherCodeQuery();
        //        query.Where(query.SRVoucherType ==
        //                    AppConstant.VoucherType.RECEIVE);

        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where(query.SRVoucherType ==
        //                        AppConstant.VoucherType.RECEIVE);
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.VoucherCode.Like(string.Format("%.{0}%",
        //                                                         txtSearch.Text)),
        //                    query.VoucherNote.Like(string.Format("%.{0}%",
        //                                                         txtSearch.Text))
        //                    )
        //                );
        //            query.Select(query.VoucherCode.As("VoucherCode"),
        //                         query.VoucherNote.As("VoucherNote"));
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        //private DataTable VoucherCodePayments
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        VoucherCodeQuery query = new VoucherCodeQuery();
        //        query.Where(query.SRVoucherType ==
        //                    AppConstant.VoucherType.PAYMENT);

        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where(query.SRVoucherType ==
        //                        AppConstant.VoucherType.PAYMENT);
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.VoucherCode.Like(string.Format("%.{0}%",
        //                                                         txtSearch.Text)),
        //                    query.VoucherNote.Like(string.Format("%.{0}%",
        //                                                         txtSearch.Text))
        //                    )
        //                );
        //            query.Select(query.VoucherCode.As("VoucherCode"),
        //                         query.VoucherNote.As("VoucherNote"));
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        //private DataTable VoucherCodeMemorials
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        VoucherCodeQuery query = new VoucherCodeQuery();
        //        query.Where(query.SRVoucherType ==
        //                    AppConstant.VoucherType.MEMORIAL);

        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where(query.SRVoucherType ==
        //                        AppConstant.VoucherType.MEMORIAL);
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.VoucherCode.Like(string.Format("%.{0}%",
        //                                                         txtSearch.Text)),
        //                    query.VoucherNote.Like(string.Format("%.{0}%",
        //                                                         txtSearch.Text))
        //                    )
        //                );
        //            query.Select(query.VoucherCode.As("VoucherCode"),
        //                         query.VoucherNote.As("VoucherNote"));
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        //private DataTable VoucherCodeAutomatics
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        VoucherCodeQuery query = new VoucherCodeQuery();
        //        query.Where(query.SRVoucherType ==
        //                    AppConstant.VoucherType.AUTOMATIC);

        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where(query.SRVoucherType ==
        //                        AppConstant.VoucherType.AUTOMATIC);
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.VoucherCode.Like(string.Format("%.{0}%",
        //                                                         txtSearch.Text)),
        //                    query.VoucherNote.Like(string.Format("%.{0}%",
        //                                                         txtSearch.Text))
        //                    )
        //                );
        //            query.Select(query.VoucherCode.As("VoucherCode"),
        //                         query.VoucherNote.As("VoucherNote"));
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        //private DataTable BillGurantors
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        BillingMasterQuery query = new BillingMasterQuery("a");
        //        GuarantorQuery qgua = new GuarantorQuery("b");

        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        query.Select(query.BillingID, query.SubsidiaryID,
        //                     query.BillingDate, query.BillingDueDate,
        //                     query.SRCurrency, query.BillingConvert.As
        //                                           ("BillingAmount"),
        //                     (query.BillingConvert -
        //                      (query.LastPaidConvert + query.TransPaidConvert +
        //                       query.ReturnPaidConvert)).As("BalanceAmount"),
        //                     qgua.GuarantorName);
        //        query.LeftJoin(qgua).On(query.SubsidiaryID ==
        //                                qgua.GuarantorID);
        //        query.Where(query.AccountID.Equal
        //                        (AppSession.AcctBillingID.AccountID));
        //        query.Where(query.SRAcctLink.Equal
        //                        (AppSession.AcctBillingID.SRAcctLink));
        //        query.Where(query.SRAcctSubsidiary.Equal
        //                        (AppSession.AcctBillingID.SRAcctSubsidiary));
        //        query.Where(query.SubsidiaryID.Equal
        //                        (AppSession.AcctGuarantorID.GuarantorID));
        //        query.Where(query.BillingConvert.NotEqual
        //                        (query.LastPaidConvert + query.TransPaidConvert +
        //                         query.ReturnPaidConvert));
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.BillingID.Like(string.Format("%.{0}%",
        //                                                       txtSearch.Text)),
        //                    query.BillingNotes.Like(string.Format("%.{0}%",
        //                                                          txtSearch.Text))
        //                    )
        //                );
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        //private DataTable BillSuppliers
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        BillingMasterQuery query = new BillingMasterQuery("a");
        //        SupplierQuery qsup = new SupplierQuery("b");

        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        query.Select(query.BillingID, query.SubsidiaryID,
        //                     query.BillingDate, query.BillingDueDate,
        //                     query.SRCurrency, query.BillingConvert.As
        //                                           ("BillingAmount"),
        //                     (query.BillingConvert -
        //                      (query.LastPaidConvert + query.TransPaidConvert +
        //                       query.ReturnPaidConvert)).As("BalanceAmount"),
        //                     qsup.SupplierName);
        //        query.LeftJoin(qsup).On(query.SubsidiaryID ==
        //                                qsup.SupplierID);
        //        query.Where(query.AccountID.Equal
        //                        (AppSession.AcctBillingID.AccountID));
        //        query.Where(query.SRAcctLink.Equal
        //                        (AppSession.AcctBillingID.SRAcctLink));
        //        query.Where(query.SRAcctSubsidiary.Equal
        //                        (AppSession.AcctBillingID.SRAcctSubsidiary));
        //        query.Where(query.SubsidiaryID.Equal
        //                        (AppSession.AcctSupplierID.SupplierID));
        //        query.Where(query.BillingConvert.NotEqual
        //                        (query.LastPaidConvert + query.TransPaidConvert +
        //                         query.ReturnPaidConvert));
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.BillingID.Like(string.Format("%.{0}%",
        //                                                       txtSearch.Text)),
        //                    query.BillingNotes.Like(string.Format("%.{0}%",
        //                                                          txtSearch.Text))
        //                    )
        //                );
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        //private DataTable BillDonators
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        BillingMasterQuery query = new BillingMasterQuery("a");
        //        DonatorQuery qdon = new DonatorQuery("b");

        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        query.Select(query.BillingID, query.SubsidiaryID,
        //                     query.BillingDate, query.BillingDueDate,
        //                     query.SRCurrency, query.BillingConvert.As
        //                                           ("BillingAmount"),
        //                     (query.BillingConvert -
        //                      (query.LastPaidConvert + query.TransPaidConvert +
        //                       query.ReturnPaidConvert)).As("BalanceAmount"),
        //                     qdon.DonatorName);
        //        query.LeftJoin(qdon).On(query.SubsidiaryID ==
        //                                qdon.DonatorID);
        //        query.Where(query.AccountID.Equal
        //                        (AppSession.AcctBillingID.AccountID));
        //        query.Where(query.SRAcctLink.Equal
        //                        (AppSession.AcctBillingID.SRAcctLink));
        //        query.Where(query.SRAcctSubsidiary.Equal
        //                        (AppSession.AcctBillingID.SRAcctSubsidiary));
        //        query.Where(query.SubsidiaryID.Equal
        //                        (AppSession.AcctDonatorID.DonatorID));
        //        query.Where(query.BillingConvert.NotEqual
        //                        (query.LastPaidConvert + query.TransPaidConvert +
        //                         query.ReturnPaidConvert));
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.BillingID.Like(string.Format("%.{0}%",
        //                                                       txtSearch.Text)),
        //                    query.BillingNotes.Like(string.Format("%.{0}%",
        //                                                          txtSearch.Text))
        //                    )
        //                );
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        //private DataTable BillEmployees
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        BillingMasterQuery query = new BillingMasterQuery("a");
        //        EmployeeQuery qempl = new EmployeeQuery("b");

        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        query.Select(query.BillingID, query.SubsidiaryID,
        //                     query.BillingDate, query.BillingDueDate,
        //                     query.SRCurrency, query.BillingConvert.As
        //                                           ("BillingAmount"),
        //                     (query.BillingConvert -
        //                      (query.LastPaidConvert + query.TransPaidConvert +
        //                       query.ReturnPaidConvert)).As("BalanceAmount"),
        //                     qempl.EmployeeName);
        //        query.LeftJoin(qempl).On(query.SubsidiaryID ==
        //                                 qempl.EmployeeID);
        //        query.Where(query.AccountID.Equal
        //                        (AppSession.AcctBillingID.AccountID));
        //        query.Where(query.SRAcctLink.Equal
        //                        (AppSession.AcctBillingID.SRAcctLink));
        //        query.Where(query.SRAcctSubsidiary.Equal
        //                        (AppSession.AcctBillingID.SRAcctSubsidiary));
        //        query.Where(query.SubsidiaryID.Equal
        //                        (AppSession.AcctEmployeeID.EmployeeID));
        //        query.Where(query.BillingConvert.NotEqual
        //                        (query.LastPaidConvert + query.TransPaidConvert +
        //                         query.ReturnPaidConvert));
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.BillingID.Like(string.Format("%.{0}%",
        //                                                       txtSearch.Text)),
        //                    query.BillingNotes.Like(string.Format("%.{0}%",
        //                                                          txtSearch.Text))
        //                    )
        //                );
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        //private DataTable BillLoanBanks
        //{
        //    get
        //    {
        //        object obj = lastQuery;
        //        if (obj != null)
        //            return ((DataTable)(obj));

        //        BillingMasterQuery query = new BillingMasterQuery("a");
        //        BankAccountQuery qbac = new BankAccountQuery("b");

        //        query.es.Top = AppSession.Parameter.MaxResultRecord;
        //        query.Select(query.BillingID, query.SubsidiaryID,
        //                     query.BillingDate, query.BillingDueDate,
        //                     query.SRCurrency, query.BillingConvert.As
        //                                           ("BillingAmount"),
        //                     (query.BillingConvert -
        //                      (query.LastPaidConvert + query.TransPaidConvert +
        //                       query.ReturnPaidConvert)).As("BalanceAmount"),
        //                     qbac.Notes.As("BankNotes"));
        //        query.LeftJoin(qbac).On(query.SubsidiaryID ==
        //                                qbac.BankAccountNo);
        //        query.Where(query.AccountID.Equal
        //                        (AppSession.AcctBillingID.AccountID));
        //        query.Where(query.SRAcctLink.Equal
        //                        (AppSession.AcctBillingID.SRAcctLink));
        //        query.Where(query.SRAcctSubsidiary.Equal
        //                        (AppSession.AcctBillingID.SRAcctSubsidiary));
        //        query.Where(query.SubsidiaryID.Equal
        //                        (AppSession.AcctBankAccountID.BankAccountNo));
        //        query.Where(query.BillingConvert.NotEqual
        //                        (query.LastPaidConvert + query.TransPaidConvert +
        //                         query.ReturnPaidConvert));
        //        if (!txtSearch.Text.Trim().Equals(string.Empty))
        //        {
        //            query.Where
        //                (
        //                query.Or
        //                    (
        //                    query.BillingID.Like(string.Format("%.{0}%",
        //                                                       txtSearch.Text)),
        //                    query.BillingNotes.Like(string.Format("%.{0}%",
        //                                                          txtSearch.Text))
        //                    )
        //                );
        //        }
        //        DataTable dtb = query.LoadDataTable();
        //        lastQuery = dtb;
        //        return dtb;
        //    }
        //}

        //#endregion

        private DataTable GetItemsAndZatActive()
        {
            object obj = lastQuery;
            if (obj != null)
                return ((DataTable)(obj));

            var query = new ItemQuery("a");

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                query.ItemID,
                query.ItemName
                );
            query.Where(
                query.SRItemType != BusinessObject.Reference.ItemType.Package,
                query.IsActive == true
                );

            //if (itemTypes.Length == 1)
            //{
            //    if (itemTypes[0] != string.Empty)
            //        query.Where(query.SRItemType == itemTypes[0]);
            //}
            //else
            //{
            //    string filterItem = string.Empty;
            //    foreach (string itemType in itemTypes)
            //    {
            //        filterItem += string.Format("a.SRItemType = '{0}' OR ", itemType);
            //    }

            //    query.Where(string.Format("<{0}>", filterItem.Substring(0, filterItem.Length - 4)));
            //}

            if (!txtSearch.Text.Trim().Equals(string.Empty))
            {
                string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                query.Where(
                    query.Or(
                        query.ItemName.Like(searchTextContain),
                        query.ItemID.Like(searchTextContain)
                        )
                    );
            }

            DataTable dtb = query.LoadDataTable();

            var zat = new ZatActiveQuery();
            zat.Select(zat.ZatActiveID.As("ItemID"), zat.ZatActiveName.As("ItemName"));
            zat.Where(zat.IsActive == true);
            if (!txtSearch.Text.Trim().Equals(string.Empty))
            {
                string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                zat.Where(
                    zat.Or(
                        zat.ZatActiveName.Like(searchTextContain),
                        zat.ZatActiveID.Like(searchTextContain)
                        )
                    );
            }
            dtb.Merge(zat.LoadDataTable());

            lastQuery = dtb;
            return dtb;
        }

        private DataTable GetItems(params string[] itemTypes)
        {
            object obj = lastQuery;
            if (obj != null)
                return ((DataTable)(obj));

            var query = new ItemQuery("a");

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                query.ItemID,
                query.ItemName
                );
            query.Where(
                query.SRItemType != BusinessObject.Reference.ItemType.Package,
                query.IsActive == true
                );

            if (itemTypes.Length > 0)
            {
                if (itemTypes[0] != string.Empty)
                    query.Where(query.SRItemType.In(itemTypes));
            }
            //else
            //{
            //    string filterItem = string.Empty;
            //    foreach (string itemType in itemTypes)
            //    {
            //        filterItem += string.Format("a.SRItemType = '{0}' OR ", itemType);
            //    }

            //    query.Where(string.Format("<{0}>", filterItem.Substring(0, filterItem.Length - 4)));
            //}

            if (!txtSearch.Text.Trim().Equals(string.Empty))
            {
                string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                query.Where(
                    query.Or(
                        query.ItemName.Like(searchTextContain),
                        query.ItemID.Like(searchTextContain)
                        )
                    );
            }

            DataTable dtb = query.LoadDataTable();
            lastQuery = dtb;
            return dtb;
        }

        private DataTable GetItemWithTarriffs(string itemType)
        {
            object obj = lastQuery;
            if (obj != null)
                return ((DataTable)(obj));

            ItemQuery query = new ItemQuery("a");
            ItemTariffQuery tariff = new ItemTariffQuery("b");

            query.es.Top = AppSession.Parameter.MaxResultRecord;

            esQueryItem price = new esQueryItem(tariff, "price", esSystemType.Double);
            price = tariff.Price;
            query.Select
                (
                query.ItemID,
                query.ItemName,
                price.As("Price")
                );

            query.InnerJoin(tariff).On(query.ItemID == tariff.ItemID);
            query.Where
                (
                query.IsActive == true,
                tariff.ClassID == Request.QueryString["filterBase"]
                );

            if (itemType != string.Empty)
                query.Where(query.SRItemType == itemType);

            if (!txtSearch.Text.Trim().Equals(string.Empty))
            {
                string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                query.Where
                    (
                    query.Or
                        (
                        query.ItemName.Like(searchTextContain),
                        query.ItemID.Like(searchTextContain)
                        )
                    );
            }
            DataTable dtb = query.LoadDataTable();
            lastQuery = dtb;
            return dtb;
        }

        #endregion
    }
}