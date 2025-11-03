using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ProductAccountDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ProductAccountSearch.aspx";
            UrlPageList = "ProductAccountList.aspx";
			
			ProgramID = AppConstant.Program.PRODUCTACCOUNT ; //TODO: Isi ProgramID
            WindowSearch.Height = 300;

			//StandardReference Initialize
			if (!IsPostBack)
            {
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                {
                    trChartOfAccountIdCOGSIGDTemp.Visible = true;
                    trChartOfAccountIdCOGSIPTemp.Visible = true;
                    trChartOfAccountIdCOGSOPTemp.Visible = true;

                    trSubledgerIdCOGSIGDTemp.Visible = true;
                    trSubledgerIdCOGSIPTemp.Visible = true;
                    trSubledgerIdCOGSOPTemp.Visible = true;
                }
                else
                {
                    trChartOfAccountIdCOGSIGDTemp.Visible = false;
                    trChartOfAccountIdCOGSIPTemp.Visible = false;
                    trChartOfAccountIdCOGSOPTemp.Visible = false;

                    trSubledgerIdCOGSIGDTemp.Visible = false;
                    trSubledgerIdCOGSIPTemp.Visible = false;
                    trSubledgerIdCOGSOPTemp.Visible = false;
                }
            }
			
			//PopUp Search
			if (!IsCallback)
			{
				
			}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ProductAccount());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            ProductAccount entity = new ProductAccount();
            if (entity.LoadByPrimaryKey(txtProductAccountID.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            ProductAccount entity = new ProductAccount();
            if (entity.LoadByPrimaryKey(txtProductAccountID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new ProductAccount();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            ProductAccount entity = new ProductAccount();
            if (entity.LoadByPrimaryKey(txtProductAccountID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("ProductAccountID='{0}'", txtProductAccountID.Text.Trim());
            auditLogFilter.TableName = "ProductAccount";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtProductAccountID.Enabled = (newVal == AppEnum.DataMode.New);

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ProductAccount entity = new ProductAccount();
            if (parameters.Length > 0)
            {
                String productAccountID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(productAccountID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtProductAccountID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            ProductAccount productAccount = (ProductAccount)entity;
            txtProductAccountID.Text = productAccount.ProductAccountID;
            txtProductAccountName.Text = productAccount.ProductAccountName;
            cboSRItemType.SelectedValue = productAccount.SRItemType;
            chkIsActive.Checked = productAccount.IsActive ?? false;

            if (txtProductAccountID.Text != string.Empty)
            {
                // --Income--
                int coaIncome = (productAccount.ChartOfAccountIdIncome.HasValue ? productAccount.ChartOfAccountIdIncome.Value : 0);
                int slIncome = (productAccount.SubledgerIdIncome.HasValue ? productAccount.SubledgerIdIncome.Value : 0);
                if (coaIncome != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdIncome, coaIncome);
                    if (slIncome != 0)
                        PopulateCboSubLedger(cboSubledgerIdIncome, slIncome);
                    else
                        ClearCombobox(cboSubledgerIdIncome);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdIncome);
                    ClearCombobox(cboSubledgerIdIncome);
                }

                // --Acrual--
                int coaAcrual = (productAccount.ChartOfAccountIdAcrual.HasValue ? productAccount.ChartOfAccountIdAcrual.Value : 0);
                int slAcrual = (productAccount.SubledgerIdAcrual.HasValue ? productAccount.SubledgerIdAcrual.Value : 0);
                if (coaAcrual != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdAcrual, coaAcrual);
                    if (slIncome != 0)
                        PopulateCboSubLedger(cboSubledgerIdAcrual, slAcrual);
                    else
                        ClearCombobox(cboSubledgerIdAcrual);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdAcrual);
                    ClearCombobox(cboSubledgerIdAcrual);
                }

                // --Discount--
                int coaDiscount = (productAccount.ChartOfAccountIdDiscount.HasValue ? productAccount.ChartOfAccountIdDiscount.Value : 0);
                int slDiscount = (productAccount.SubledgerIdDiscount.HasValue ? productAccount.SubledgerIdDiscount.Value : 0);
                if (coaDiscount != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdDiscount, coaDiscount);
                    if (slDiscount != 0)
                        PopulateCboSubLedger(cboSubledgerIdDiscount, slDiscount);
                    else
                        ClearCombobox(cboSubledgerIdDiscount);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdDiscount);
                    ClearCombobox(cboSubledgerIdDiscount);
                }

                // --Inventory--
                int coaInventory = (productAccount.ChartOfAccountIdInventory.HasValue ? productAccount.ChartOfAccountIdInventory.Value : 0);
                int slInventory = (productAccount.SubledgerIdInventory.HasValue ? productAccount.SubledgerIdInventory.Value : 0);
                if (coaInventory != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdInventory, coaInventory);
                    if (slInventory != 0)
                        PopulateCboSubLedger(cboSubledgerIdInventory, slInventory);
                    else
                        ClearCombobox(cboSubledgerIdInventory);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdInventory);
                    ClearCombobox(cboSubledgerIdInventory);
                }

                // --COGS--
                int coaCOGS = (productAccount.ChartOfAccountIdCOGS.HasValue ? productAccount.ChartOfAccountIdCOGS.Value : 0);
                int slCOGS = (productAccount.SubledgerIdCOGS.HasValue ? productAccount.SubledgerIdCOGS.Value : 0);
                if (coaCOGS != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdCOGS, coaCOGS);
                    if (slCOGS != 0)
                        PopulateCboSubLedger(cboSubledgerIdCOGS, slCOGS);
                    else
                        ClearCombobox(cboSubledgerIdCOGS);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdCOGS);
                    ClearCombobox(cboSubledgerIdCOGS);
                }

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                {
                    // --COGS TEMP--
                    int coaCOGSTemp = (productAccount.ChartOfAccountIdCOGSOPTemp.HasValue ? productAccount.ChartOfAccountIdCOGSOPTemp.Value : 0);
                    int slCOGSTemp = (productAccount.SubledgerIdCOGSOPTemp.HasValue ? productAccount.SubledgerIdCOGSOPTemp.Value : 0);
                    if (coaCOGS != 0)
                    {
                        PopulateCboChartOfAccount(cboChartOfAccountIdCOGSOPTemp, coaCOGSTemp);
                        if (slCOGS != 0)
                            PopulateCboSubLedger(cboSubledgerIdCOGSOPTemp, slCOGSTemp);
                        else
                            ClearCombobox(cboSubledgerIdCOGSOPTemp);
                    }
                    else
                    {
                        ClearCombobox(cboChartOfAccountIdCOGSOPTemp);
                        ClearCombobox(cboSubledgerIdCOGSOPTemp);
                    }
                }

                // --SalesReturn--
                int coaSalesReturn = (productAccount.ChartOfAccountIdSalesReturn.HasValue ? productAccount.ChartOfAccountIdSalesReturn.Value : 0);
                int slSalesReturn = (productAccount.SubledgerIdSalesReturn.HasValue ? productAccount.SubledgerIdSalesReturn.Value : 0);
                if (coaSalesReturn != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdSalesReturn, coaSalesReturn);
                    if (slSalesReturn != 0)
                        PopulateCboSubLedger(cboSubledgerIdSalesReturn, slSalesReturn);
                    else
                        ClearCombobox(cboSubledgerIdSalesReturn);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdSalesReturn);
                    ClearCombobox(cboSubledgerIdSalesReturn);
                }

                // --PurchaseReturn--
                int coaPurchaseReturn = (productAccount.ChartOfAccountIdPurchaseReturn.HasValue ? productAccount.ChartOfAccountIdPurchaseReturn.Value : 0);
                int slPurchaseReturn = (productAccount.SubledgerIdPurchaseReturn.HasValue ? productAccount.SubledgerIdPurchaseReturn.Value : 0);
                if (coaPurchaseReturn != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdPurchaseReturn, coaPurchaseReturn);
                    if (slPurchaseReturn != 0)
                        PopulateCboSubLedger(cboSubledgerIdPurchaseReturn, slPurchaseReturn);
                    else
                        ClearCombobox(cboSubledgerIdPurchaseReturn);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdPurchaseReturn);
                    ClearCombobox(cboSubledgerIdPurchaseReturn);
                }

                // --Cost--
                int coaCost = (productAccount.ChartOfAccountIdCost.HasValue ? productAccount.ChartOfAccountIdCost.Value : 0);
                int slCost = (productAccount.SubledgerIdCost.HasValue ? productAccount.SubledgerIdCost.Value : 0);
                if (coaCost != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdCost, coaCost);
                    if (slCost != 0)
                        PopulateCboSubLedger(cboSubledgerIdCost, slCost);
                    else
                        ClearCombobox(cboSubledgerIdCost);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdCost);
                    ClearCombobox(cboSubledgerIdCost);
                }



                // IN PATIENT
                // --Income--
                int coaIncomeIP = (productAccount.ChartOfAccountIdIncomeIP.HasValue ? productAccount.ChartOfAccountIdIncomeIP.Value : 0);
                int slIncomeIP = (productAccount.SubledgerIdIncomeIP.HasValue ? productAccount.SubledgerIdIncomeIP.Value : 0);
                if (coaIncomeIP != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdIncomeIP, coaIncomeIP);
                    if (slIncomeIP != 0)
                        PopulateCboSubLedger(cboSubledgerIdIncomeIP, slIncomeIP);
                    else
                        ClearCombobox(cboSubledgerIdIncomeIP);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdIncomeIP);
                    ClearCombobox(cboSubledgerIdIncomeIP);
                }

                // --Acrual--
                int coaAcrualIP = (productAccount.ChartOfAccountIdAcrualIP.HasValue ? productAccount.ChartOfAccountIdAcrualIP.Value : 0);
                int slAcrualIP = (productAccount.SubledgerIdAcrualIP.HasValue ? productAccount.SubledgerIdAcrualIP.Value : 0);
                if (coaAcrualIP != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdAcrualIP, coaAcrualIP);
                    if (slIncomeIP != 0)
                        PopulateCboSubLedger(cboSubledgerIdAcrualIP, slAcrualIP);
                    else
                        ClearCombobox(cboSubledgerIdAcrualIP);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdAcrualIP);
                    ClearCombobox(cboSubledgerIdAcrualIP);
                }

                // --Discount--
                int coaDiscountIP = (productAccount.ChartOfAccountIdDiscountIP.HasValue ? productAccount.ChartOfAccountIdDiscountIP.Value : 0);
                int slDiscountIP = (productAccount.SubledgerIdDiscountIP.HasValue ? productAccount.SubledgerIdDiscountIP.Value : 0);
                if (coaDiscountIP != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdDiscountIP, coaDiscountIP);
                    if (slDiscountIP != 0)
                        PopulateCboSubLedger(cboSubledgerIdDiscountIP, slDiscountIP);
                    else
                        ClearCombobox(cboSubledgerIdDiscountIP);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdDiscountIP);
                    ClearCombobox(cboSubledgerIdDiscountIP);
                }

                // --Inventory--
                int coaInventoryIP = (productAccount.ChartOfAccountIdInventoryIP.HasValue ? productAccount.ChartOfAccountIdInventoryIP.Value : 0);
                int slInventoryIP = (productAccount.SubledgerIdInventoryIP.HasValue ? productAccount.SubledgerIdInventoryIP.Value : 0);
                if (coaInventoryIP != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdInventoryIP, coaInventoryIP);
                    if (slInventoryIP != 0)
                        PopulateCboSubLedger(cboSubledgerIdInventoryIP, slInventoryIP);
                    else
                        ClearCombobox(cboSubledgerIdInventoryIP);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdInventoryIP);
                    ClearCombobox(cboSubledgerIdInventoryIP);
                }

                // --COGS--
                int coaCOGSIP = (productAccount.ChartOfAccountIdCOGSIP.HasValue ? productAccount.ChartOfAccountIdCOGSIP.Value : 0);
                int slCOGSIP = (productAccount.SubledgerIdCOGSIP.HasValue ? productAccount.SubledgerIdCOGSIP.Value : 0);
                if (coaCOGSIP != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdCOGSIP, coaCOGSIP);
                    if (slCOGSIP != 0)
                        PopulateCboSubLedger(cboSubledgerIdCOGSIP, slCOGSIP);
                    else
                        ClearCombobox(cboSubledgerIdCOGSIP);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdCOGSIP);
                    ClearCombobox(cboSubledgerIdCOGSIP);
                }

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                {
                    // --COGS TEMP--
                    int coaCOGSIPTemp = (productAccount.ChartOfAccountIdCOGSIPTemp.HasValue ? productAccount.ChartOfAccountIdCOGSIPTemp.Value : 0);
                    int slCOGSIPTemp = (productAccount.SubledgerIdCOGSIPTemp.HasValue ? productAccount.SubledgerIdCOGSIPTemp.Value : 0);
                    if (coaCOGSIP != 0)
                    {
                        PopulateCboChartOfAccount(cboChartOfAccountIdCOGSIPTemp, coaCOGSIPTemp);
                        if (slCOGSIP != 0)
                            PopulateCboSubLedger(cboSubledgerIdCOGSIPTemp, slCOGSIPTemp);
                        else
                            ClearCombobox(cboSubledgerIdCOGSIPTemp);
                    }
                    else
                    {
                        ClearCombobox(cboChartOfAccountIdCOGSIPTemp);
                        ClearCombobox(cboSubledgerIdCOGSIPTemp);
                    }
                }

                // --SalesReturn--
                int coaSalesReturnIP = (productAccount.ChartOfAccountIdSalesReturnIP.HasValue ? productAccount.ChartOfAccountIdSalesReturnIP.Value : 0);
                int slSalesReturnIP = (productAccount.SubledgerIdSalesReturnIP.HasValue ? productAccount.SubledgerIdSalesReturnIP.Value : 0);
                if (coaSalesReturnIP != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdSalesReturnIP, coaSalesReturnIP);
                    if (slSalesReturnIP != 0)
                        PopulateCboSubLedger(cboSubledgerIdSalesReturnIP, slSalesReturnIP);
                    else
                        ClearCombobox(cboSubledgerIdSalesReturnIP);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdSalesReturnIP);
                    ClearCombobox(cboSubledgerIdSalesReturnIP);
                }

                // --PurchaseReturn--
                int coaPurchaseReturnIP = (productAccount.ChartOfAccountIdPurchaseReturnIP.HasValue ? productAccount.ChartOfAccountIdPurchaseReturnIP.Value : 0);
                int slPurchaseReturnIP = (productAccount.SubledgerIdPurchaseReturnIP.HasValue ? productAccount.SubledgerIdPurchaseReturnIP.Value : 0);
                if (coaPurchaseReturnIP != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdPurchaseReturnIP, coaPurchaseReturnIP);
                    if (slPurchaseReturnIP != 0)
                        PopulateCboSubLedger(cboSubledgerIdPurchaseReturnIP, slPurchaseReturnIP);
                    else
                        ClearCombobox(cboSubledgerIdPurchaseReturnIP);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdPurchaseReturnIP);
                    ClearCombobox(cboSubledgerIdPurchaseReturnIP);
                }

                // --Cost--
                int coaCostIP = (productAccount.ChartOfAccountIdCostIP.HasValue ? productAccount.ChartOfAccountIdCostIP.Value : 0);
                int slCostIP = (productAccount.SubledgerIdCostIP.HasValue ? productAccount.SubledgerIdCostIP.Value : 0);
                if (coaCostIP != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdCostIP, coaCostIP);
                    if (slCostIP != 0)
                        PopulateCboSubLedger(cboSubledgerIdCostIP, slCostIP);
                    else
                        ClearCombobox(cboSubledgerIdCostIP);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdCostIP);
                    ClearCombobox(cboSubledgerIdCostIP);
                }

                
                // EMERGENCY
                // --Income--
                int coaIncomeIGD = (productAccount.ChartOfAccountIdIncomeIGD.HasValue ? productAccount.ChartOfAccountIdIncomeIGD.Value : 0);
                int slIncomeIGD = (productAccount.SubledgerIdIncomeIGD.HasValue ? productAccount.SubledgerIdIncomeIGD.Value : 0);
                if (coaIncomeIGD != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdIncomeIGD, coaIncomeIGD);
                    if (slIncomeIGD != 0)
                        PopulateCboSubLedger(cboSubledgerIdIncomeIGD, slIncomeIGD);
                    else
                        ClearCombobox(cboSubledgerIdIncomeIGD);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdIncomeIGD);
                    ClearCombobox(cboSubledgerIdIncomeIGD);
                }

                // --Acrual--
                int coaAcrualIGD = (productAccount.ChartOfAccountIdAcrualIGD.HasValue ? productAccount.ChartOfAccountIdAcrualIGD.Value : 0);
                int slAcrualIGD = (productAccount.SubledgerIdAcrualIGD.HasValue ? productAccount.SubledgerIdAcrualIGD.Value : 0);
                if (coaAcrualIGD != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdAcrualIGD, coaAcrualIGD);
                    if (slIncomeIGD != 0)
                        PopulateCboSubLedger(cboSubledgerIdAcrualIGD, slAcrualIGD);
                    else
                        ClearCombobox(cboSubledgerIdAcrualIGD);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdAcrualIGD);
                    ClearCombobox(cboSubledgerIdAcrualIGD);
                }

                // --Discount--
                int coaDiscountIGD = (productAccount.ChartOfAccountIdDiscountIGD.HasValue ? productAccount.ChartOfAccountIdDiscountIGD.Value : 0);
                int slDiscountIGD = (productAccount.SubledgerIdDiscountIGD.HasValue ? productAccount.SubledgerIdDiscountIGD.Value : 0);
                if (coaDiscountIGD != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdDiscountIGD, coaDiscountIGD);
                    if (slDiscountIGD != 0)
                        PopulateCboSubLedger(cboSubledgerIdDiscountIGD, slDiscountIGD);
                    else
                        ClearCombobox(cboSubledgerIdDiscountIGD);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdDiscountIGD);
                    ClearCombobox(cboSubledgerIdDiscountIGD);
                }

                // --Inventory--
                int coaInventoryIGD = (productAccount.ChartOfAccountIdInventoryIGD.HasValue ? productAccount.ChartOfAccountIdInventoryIGD.Value : 0);
                int slInventoryIGD = (productAccount.SubledgerIdInventoryIGD.HasValue ? productAccount.SubledgerIdInventoryIGD.Value : 0);
                if (coaInventoryIGD != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdInventoryIGD, coaInventoryIGD);
                    if (slInventoryIGD != 0)
                        PopulateCboSubLedger(cboSubledgerIdInventoryIGD, slInventoryIGD);
                    else
                        ClearCombobox(cboSubledgerIdInventoryIGD);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdInventoryIGD);
                    ClearCombobox(cboSubledgerIdInventoryIGD);
                }

                // --COGS--
                int coaCOGSIGD = (productAccount.ChartOfAccountIdCOGSIGD.HasValue ? productAccount.ChartOfAccountIdCOGSIGD.Value : 0);
                int slCOGSIGD = (productAccount.SubledgerIdCOGSIGD.HasValue ? productAccount.SubledgerIdCOGSIGD.Value : 0);
                if (coaCOGSIP != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdCOGSIGD, coaCOGSIGD);
                    if (slCOGSIGD != 0)
                        PopulateCboSubLedger(cboSubledgerIdCOGSIGD, slCOGSIGD);
                    else
                        ClearCombobox(cboSubledgerIdCOGSIGD);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdCOGSIGD);
                    ClearCombobox(cboSubledgerIdCOGSIGD);
                }

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                {
                    // --COGS TEMP--
                    int coaCOGSIGDTemp = (productAccount.ChartOfAccountIdCOGSIGDTemp.HasValue ? productAccount.ChartOfAccountIdCOGSIGDTemp.Value : 0);
                    int slCOGSIGDTemp = (productAccount.SubledgerIdCOGSIGDTemp.HasValue ? productAccount.SubledgerIdCOGSIGDTemp.Value : 0);
                    if (coaCOGSIP != 0)
                    {
                        PopulateCboChartOfAccount(cboChartOfAccountIdCOGSIGDTemp, coaCOGSIGDTemp);
                        if (slCOGSIGD != 0)
                            PopulateCboSubLedger(cboSubledgerIdCOGSIGDTemp, slCOGSIGDTemp);
                        else
                            ClearCombobox(cboSubledgerIdCOGSIGDTemp);
                    }
                    else
                    {
                        ClearCombobox(cboChartOfAccountIdCOGSIGDTemp);
                        ClearCombobox(cboSubledgerIdCOGSIGDTemp);
                    }
                }

                // --SalesReturn--
                int coaSalesReturnIGD = (productAccount.ChartOfAccountIdSalesReturnIGD.HasValue ? productAccount.ChartOfAccountIdSalesReturnIGD.Value : 0);
                int slSalesReturnIGD = (productAccount.SubledgerIdSalesReturnIGD.HasValue ? productAccount.SubledgerIdSalesReturnIGD.Value : 0);
                if (coaSalesReturnIGD != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdSalesReturnIGD, coaSalesReturnIGD);
                    if (slSalesReturnIGD != 0)
                        PopulateCboSubLedger(cboSubledgerIdSalesReturnIGD, slSalesReturnIGD);
                    else
                        ClearCombobox(cboSubledgerIdSalesReturnIGD);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdSalesReturnIGD);
                    ClearCombobox(cboSubledgerIdSalesReturnIGD);
                }

                // --PurchaseReturn--
                int coaPurchaseReturnIGD = (productAccount.ChartOfAccountIdPurchaseReturnIGD.HasValue ? productAccount.ChartOfAccountIdPurchaseReturnIGD.Value : 0);
                int slPurchaseReturnIGD = (productAccount.SubledgerIdPurchaseReturnIGD.HasValue ? productAccount.SubledgerIdPurchaseReturnIGD.Value : 0);
                if (coaPurchaseReturnIGD != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdPurchaseReturnIGD, coaPurchaseReturnIGD);
                    if (slPurchaseReturnIGD != 0)
                        PopulateCboSubLedger(cboSubledgerIdPurchaseReturnIGD, slPurchaseReturnIGD);
                    else
                        ClearCombobox(cboSubledgerIdPurchaseReturnIGD);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdPurchaseReturnIGD);
                    ClearCombobox(cboSubledgerIdPurchaseReturnIGD);
                }

                // --Cost--
                int coaCostIGD = (productAccount.ChartOfAccountIdCostIGD.HasValue ? productAccount.ChartOfAccountIdCostIGD.Value : 0);
                int slCostIGD = (productAccount.SubledgerIdCostIGD.HasValue ? productAccount.SubledgerIdCostIGD.Value : 0);
                if (coaCostIGD != 0)
                {
                    PopulateCboChartOfAccount(cboChartOfAccountIdCostIGD, coaCostIGD);
                    if (slCostIGD != 0)
                        PopulateCboSubLedger(cboSubledgerIdCostIGD, slCostIGD);
                    else
                        ClearCombobox(cboSubledgerIdCostIGD);
                }
                else
                {
                    ClearCombobox(cboChartOfAccountIdCostIGD);
                    ClearCombobox(cboSubledgerIdCostIGD);
                }

            }
            else
            {
                //OUT PATIENT
                ClearCombobox(cboChartOfAccountIdIncome);
                ClearCombobox(cboSubledgerIdIncome);
                ClearCombobox(cboChartOfAccountIdAcrual);
                ClearCombobox(cboSubledgerIdAcrual);
                ClearCombobox(cboChartOfAccountIdDiscount);
                ClearCombobox(cboSubledgerIdDiscount);
                ClearCombobox(cboChartOfAccountIdInventory);
                ClearCombobox(cboSubledgerIdInventory);
                ClearCombobox(cboChartOfAccountIdCOGS);
                ClearCombobox(cboSubledgerIdCOGS);
                ClearCombobox(cboChartOfAccountIdSalesReturn);
                ClearCombobox(cboSubledgerIdSalesReturn);
                ClearCombobox(cboChartOfAccountIdPurchaseReturn);
                ClearCombobox(cboSubledgerIdPurchaseReturn);
                ClearCombobox(cboChartOfAccountIdCost);
                ClearCombobox(cboSubledgerIdCost);

                //IN PATIENT
                ClearCombobox(cboChartOfAccountIdIncomeIP);
                ClearCombobox(cboSubledgerIdIncomeIP);
                ClearCombobox(cboChartOfAccountIdAcrualIP);
                ClearCombobox(cboSubledgerIdAcrualIP);
                ClearCombobox(cboChartOfAccountIdDiscountIP);
                ClearCombobox(cboSubledgerIdDiscountIP);
                ClearCombobox(cboChartOfAccountIdInventoryIP);
                ClearCombobox(cboSubledgerIdInventoryIP);
                ClearCombobox(cboChartOfAccountIdCOGSIP);
                ClearCombobox(cboSubledgerIdCOGSIP);
                ClearCombobox(cboChartOfAccountIdSalesReturnIP);
                ClearCombobox(cboSubledgerIdSalesReturnIP);
                ClearCombobox(cboChartOfAccountIdPurchaseReturnIP);
                ClearCombobox(cboSubledgerIdPurchaseReturnIP);
                ClearCombobox(cboChartOfAccountIdCostIP);
                ClearCombobox(cboSubledgerIdCostIP);

                //EMERGENCY
                ClearCombobox(cboChartOfAccountIdIncomeIGD);
                ClearCombobox(cboSubledgerIdIncomeIGD);
                ClearCombobox(cboChartOfAccountIdAcrualIGD);
                ClearCombobox(cboSubledgerIdAcrualIGD);
                ClearCombobox(cboChartOfAccountIdDiscountIGD);
                ClearCombobox(cboSubledgerIdDiscountIGD);
                ClearCombobox(cboChartOfAccountIdInventoryIGD);
                ClearCombobox(cboSubledgerIdInventoryIGD);
                ClearCombobox(cboChartOfAccountIdCOGSIGD);
                ClearCombobox(cboSubledgerIdCOGSIGD);
                ClearCombobox(cboChartOfAccountIdSalesReturnIGD);
                ClearCombobox(cboSubledgerIdSalesReturnIGD);
                ClearCombobox(cboChartOfAccountIdPurchaseReturnIGD);
                ClearCombobox(cboSubledgerIdPurchaseReturnIGD);
                ClearCombobox(cboChartOfAccountIdCostIGD);
                ClearCombobox(cboSubledgerIdCostIGD);

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                {
                    ClearCombobox(cboChartOfAccountIdCOGSOPTemp);
                    ClearCombobox(cboSubledgerIdCOGSOPTemp);
                    ClearCombobox(cboChartOfAccountIdCOGSIPTemp);
                    ClearCombobox(cboSubledgerIdCOGSIPTemp);
                    ClearCombobox(cboChartOfAccountIdCOGSIGDTemp);
                    ClearCombobox(cboSubledgerIdCOGSIGDTemp);
                }
            }
        }

        private void PopulateCboChartOfAccount(RadComboBox comboBox, int coaId)
        {
            ChartOfAccountsQuery coaQ = new ChartOfAccountsQuery();
            coaQ.Select(coaQ.ChartOfAccountId, coaQ.ChartOfAccountCode, coaQ.ChartOfAccountName);
            coaQ.Where(coaQ.ChartOfAccountId == coaId);
            DataTable dtbCoa = coaQ.LoadDataTable();
            comboBox.DataSource = dtbCoa;
            comboBox.DataBind();
            comboBox.SelectedValue = coaId.ToString();
        }

        private void PopulateCboSubLedger(RadComboBox comboBox, int subLedgerID)
        {
            SubLedgersQuery slQ = new SubLedgersQuery();
            slQ.Select(slQ.SubLedgerId, slQ.SubLedgerName, slQ.Description);
            slQ.Where(slQ.SubLedgerId == subLedgerID);
            DataTable dtbSl = slQ.LoadDataTable();
            comboBox.DataSource = dtbSl;
            comboBox.DataBind();
            comboBox.SelectedValue = subLedgerID.ToString();
        }

        private void ClearCombobox(RadComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Text = string.Empty;
        }


        #endregion

        #region Private Method Standard
        private void SetEntityValue(ProductAccount entity)
        {
            entity.ProductAccountID = txtProductAccountID.Text;
            entity.ProductAccountName = txtProductAccountName.Text;
            entity.SRItemType = cboSRItemType.SelectedValue;
            entity.IsActive = chkIsActive.Checked;

            // OUT PATIENT
            int chartOfAccountIdIncome = 0;
            int subLegderIdIncome = 0;
            int.TryParse(cboChartOfAccountIdIncome.SelectedValue, out chartOfAccountIdIncome);
            int.TryParse(cboSubledgerIdIncome.SelectedValue, out subLegderIdIncome);
            entity.ChartOfAccountIdIncome = chartOfAccountIdIncome;
            entity.SubledgerIdIncome = subLegderIdIncome;

            int chartOfAccountIdAcrual = 0;
            int subLegderIdAcrual = 0;
            int.TryParse(cboChartOfAccountIdAcrual.SelectedValue, out chartOfAccountIdAcrual);
            int.TryParse(cboSubledgerIdAcrual.SelectedValue, out subLegderIdAcrual);
            entity.ChartOfAccountIdAcrual = chartOfAccountIdAcrual;
            entity.SubledgerIdAcrual = subLegderIdAcrual;

            int chartOfAccountIdDiscount = 0;
            int subLegderIdDiscount = 0;
            int.TryParse(cboChartOfAccountIdDiscount.SelectedValue, out chartOfAccountIdDiscount);
            int.TryParse(cboSubledgerIdDiscount.SelectedValue, out subLegderIdDiscount);
            entity.ChartOfAccountIdDiscount = chartOfAccountIdDiscount;
            entity.SubledgerIdDiscount = subLegderIdDiscount;

            int chartOfAccountIdInventory = 0;
            int subLegderIdInventory = 0;
            int.TryParse(cboChartOfAccountIdInventory.SelectedValue, out chartOfAccountIdInventory);
            int.TryParse(cboSubledgerIdInventory.SelectedValue, out subLegderIdInventory);
            entity.ChartOfAccountIdInventory = chartOfAccountIdInventory;
            entity.SubledgerIdInventory = subLegderIdInventory;

            int chartOfAccountIdCOGS = 0;
            int subLegderIdCOGS = 0;
            int.TryParse(cboChartOfAccountIdCOGS.SelectedValue, out chartOfAccountIdCOGS);
            int.TryParse(cboSubledgerIdCOGS.SelectedValue, out subLegderIdCOGS);
            entity.ChartOfAccountIdCOGS = chartOfAccountIdCOGS;
            entity.SubledgerIdCOGS = subLegderIdCOGS;

            int chartOfAccountIdSalesReturn = 0;
            int subLegderIdSalesReturn = 0;
            int.TryParse(cboChartOfAccountIdSalesReturn.SelectedValue, out chartOfAccountIdSalesReturn);
            int.TryParse(cboSubledgerIdSalesReturn.SelectedValue, out subLegderIdSalesReturn);
            entity.ChartOfAccountIdSalesReturn = chartOfAccountIdSalesReturn;
            entity.SubledgerIdSalesReturn = subLegderIdSalesReturn;

            int chartOfAccountIdPurchaseReturn = 0;
            int subLegderIdPurchaseReturn = 0;
            int.TryParse(cboChartOfAccountIdPurchaseReturn.SelectedValue, out chartOfAccountIdPurchaseReturn);
            int.TryParse(cboSubledgerIdPurchaseReturn.SelectedValue, out subLegderIdPurchaseReturn);
            entity.ChartOfAccountIdPurchaseReturn = chartOfAccountIdPurchaseReturn;
            entity.SubledgerIdPurchaseReturn = subLegderIdPurchaseReturn;

            int chartOfAccountIdCost = 0;
            int subLegderIdCost = 0;
            int.TryParse(cboChartOfAccountIdCost.SelectedValue, out chartOfAccountIdCost);
            int.TryParse(cboSubledgerIdCost.SelectedValue, out subLegderIdCost);
            entity.ChartOfAccountIdCost = chartOfAccountIdCost;
            entity.SubledgerIdCost = subLegderIdCost;

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
            {

                int chartOfAccountIdCOGSTemp = 0;
                int subLegderIdCOGSTemp = 0;
                int.TryParse(cboChartOfAccountIdCOGSOPTemp.SelectedValue, out chartOfAccountIdCOGSTemp);
                int.TryParse(cboSubledgerIdCOGSOPTemp.SelectedValue, out subLegderIdCOGSTemp);
                entity.ChartOfAccountIdCOGSOPTemp = chartOfAccountIdCOGSTemp;
                entity.SubledgerIdCOGSOPTemp = subLegderIdCOGSTemp;
            }

            // INPATIENT
            int chartOfAccountIdIncomeIP = 0;
            int subLegderIdIncomeIP = 0;
            int.TryParse(cboChartOfAccountIdIncomeIP.SelectedValue, out chartOfAccountIdIncomeIP);
            int.TryParse(cboSubledgerIdIncomeIP.SelectedValue, out subLegderIdIncomeIP);
            entity.ChartOfAccountIdIncomeIP = chartOfAccountIdIncomeIP;
            entity.SubledgerIdIncomeIP = subLegderIdIncomeIP;

            int chartOfAccountIdAcrualIP = 0;
            int subLegderIdAcrualIP = 0;
            int.TryParse(cboChartOfAccountIdAcrualIP.SelectedValue, out chartOfAccountIdAcrualIP);
            int.TryParse(cboSubledgerIdAcrualIP.SelectedValue, out subLegderIdAcrualIP);
            entity.ChartOfAccountIdAcrualIP = chartOfAccountIdAcrualIP;
            entity.SubledgerIdAcrualIP = subLegderIdAcrualIP;

            int chartOfAccountIdDiscountIP = 0;
            int subLegderIdDiscountIP = 0;
            int.TryParse(cboChartOfAccountIdDiscountIP.SelectedValue, out chartOfAccountIdDiscountIP);
            int.TryParse(cboSubledgerIdDiscountIP.SelectedValue, out subLegderIdDiscountIP);
            entity.ChartOfAccountIdDiscountIP = chartOfAccountIdDiscountIP;
            entity.SubledgerIdDiscountIP = subLegderIdDiscountIP;

            int chartOfAccountIdInventoryIP = 0;
            int subLegderIdInventoryIP = 0;
            int.TryParse(cboChartOfAccountIdInventoryIP.SelectedValue, out chartOfAccountIdInventoryIP);
            int.TryParse(cboSubledgerIdInventoryIP.SelectedValue, out subLegderIdInventoryIP);
            entity.ChartOfAccountIdInventoryIP = chartOfAccountIdInventoryIP;
            entity.SubledgerIdInventoryIP = subLegderIdInventoryIP;

            int chartOfAccountIdCOGSIP = 0;
            int subLegderIdCOGSIP = 0;
            int.TryParse(cboChartOfAccountIdCOGSIP.SelectedValue, out chartOfAccountIdCOGSIP);
            int.TryParse(cboSubledgerIdCOGSIP.SelectedValue, out subLegderIdCOGSIP);
            entity.ChartOfAccountIdCOGSIP = chartOfAccountIdCOGSIP;
            entity.SubledgerIdCOGSIP = subLegderIdCOGSIP;

            int chartOfAccountIdSalesReturnIP = 0;
            int subLegderIdSalesReturnIP = 0;
            int.TryParse(cboChartOfAccountIdSalesReturnIP.SelectedValue, out chartOfAccountIdSalesReturnIP);
            int.TryParse(cboSubledgerIdSalesReturnIP.SelectedValue, out subLegderIdSalesReturnIP);
            entity.ChartOfAccountIdSalesReturnIP = chartOfAccountIdSalesReturnIP;
            entity.SubledgerIdSalesReturnIP = subLegderIdSalesReturnIP;

            int chartOfAccountIdPurchaseReturnIP = 0;
            int subLegderIdPurchaseReturnIP = 0;
            int.TryParse(cboChartOfAccountIdPurchaseReturnIP.SelectedValue, out chartOfAccountIdPurchaseReturnIP);
            int.TryParse(cboSubledgerIdPurchaseReturnIP.SelectedValue, out subLegderIdPurchaseReturnIP);
            entity.ChartOfAccountIdPurchaseReturnIP = chartOfAccountIdPurchaseReturnIP;
            entity.SubledgerIdPurchaseReturnIP = subLegderIdPurchaseReturnIP;

            int chartOfAccountIdCostIP = 0;
            int subLegderIdCostIP = 0;
            int.TryParse(cboChartOfAccountIdCostIP.SelectedValue, out chartOfAccountIdCostIP);
            int.TryParse(cboSubledgerIdCostIP.SelectedValue, out subLegderIdCostIP);
            entity.ChartOfAccountIdCostIP = chartOfAccountIdCostIP;
            entity.SubledgerIdCostIP = subLegderIdCostIP;

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
            {
                int chartOfAccountIdCOGSIPTemp = 0;
                int subLegderIdCOGSIPTemp = 0;
                int.TryParse(cboChartOfAccountIdCOGSIPTemp.SelectedValue, out chartOfAccountIdCOGSIPTemp);
                int.TryParse(cboSubledgerIdCOGSIPTemp.SelectedValue, out subLegderIdCOGSIPTemp);
                entity.ChartOfAccountIdCOGSIPTemp = chartOfAccountIdCOGSIPTemp;
                entity.SubledgerIdCOGSIPTemp = subLegderIdCOGSIPTemp;
            }

            // EMERGENCY
            int chartOfAccountIdIncomeIGD = 0;
            int subLegderIdIncomeIGD = 0;
            int.TryParse(cboChartOfAccountIdIncomeIGD.SelectedValue, out chartOfAccountIdIncomeIGD);
            int.TryParse(cboSubledgerIdIncomeIGD.SelectedValue, out subLegderIdIncomeIGD);
            entity.ChartOfAccountIdIncomeIGD = chartOfAccountIdIncomeIGD;
            entity.SubledgerIdIncomeIGD = subLegderIdIncomeIGD;

            int chartOfAccountIdAcrualIGD = 0;
            int subLegderIdAcrualIGD = 0;
            int.TryParse(cboChartOfAccountIdAcrualIGD.SelectedValue, out chartOfAccountIdAcrualIGD);
            int.TryParse(cboSubledgerIdAcrualIGD.SelectedValue, out subLegderIdAcrualIGD);
            entity.ChartOfAccountIdAcrualIGD = chartOfAccountIdAcrualIGD;
            entity.SubledgerIdAcrualIGD = subLegderIdAcrualIGD;

            int chartOfAccountIdDiscountIGD = 0;
            int subLegderIdDiscountIGD = 0;
            int.TryParse(cboChartOfAccountIdDiscountIGD.SelectedValue, out chartOfAccountIdDiscountIGD);
            int.TryParse(cboSubledgerIdDiscountIGD.SelectedValue, out subLegderIdDiscountIGD);
            entity.ChartOfAccountIdDiscountIGD = chartOfAccountIdDiscountIGD;
            entity.SubledgerIdDiscountIGD = subLegderIdDiscountIGD;

            int chartOfAccountIdInventoryIGD = 0;
            int subLegderIdInventoryIGD = 0;
            int.TryParse(cboChartOfAccountIdInventoryIGD.SelectedValue, out chartOfAccountIdInventoryIGD);
            int.TryParse(cboSubledgerIdInventoryIGD.SelectedValue, out subLegderIdInventoryIGD);
            entity.ChartOfAccountIdInventoryIGD = chartOfAccountIdInventoryIGD;
            entity.SubledgerIdInventoryIGD = subLegderIdInventoryIGD;

            int chartOfAccountIdCOGSIGD = 0;
            int subLegderIdCOGSIGD = 0;
            int.TryParse(cboChartOfAccountIdCOGSIGD.SelectedValue, out chartOfAccountIdCOGSIGD);
            int.TryParse(cboSubledgerIdCOGSIGD.SelectedValue, out subLegderIdCOGSIGD);
            entity.ChartOfAccountIdCOGSIGD = chartOfAccountIdCOGSIGD;
            entity.SubledgerIdCOGSIGD = subLegderIdCOGSIGD;

            int chartOfAccountIdSalesReturnIGD = 0;
            int subLegderIdSalesReturnIGD = 0;
            int.TryParse(cboChartOfAccountIdSalesReturnIGD.SelectedValue, out chartOfAccountIdSalesReturnIGD);
            int.TryParse(cboSubledgerIdSalesReturnIGD.SelectedValue, out subLegderIdSalesReturnIGD);
            entity.ChartOfAccountIdSalesReturnIGD = chartOfAccountIdSalesReturnIGD;
            entity.SubledgerIdSalesReturnIGD = subLegderIdSalesReturnIGD;

            int chartOfAccountIdPurchaseReturnIGD = 0;
            int subLegderIdPurchaseReturnIGD = 0;
            int.TryParse(cboChartOfAccountIdPurchaseReturnIGD.SelectedValue, out chartOfAccountIdPurchaseReturnIGD);
            int.TryParse(cboSubledgerIdPurchaseReturnIGD.SelectedValue, out subLegderIdPurchaseReturnIGD);
            entity.ChartOfAccountIdPurchaseReturnIGD = chartOfAccountIdPurchaseReturnIGD;
            entity.SubledgerIdPurchaseReturnIGD = subLegderIdPurchaseReturnIGD;

            int chartOfAccountIdCostIGD = 0;
            int subLegderIdCostIGD = 0;
            int.TryParse(cboChartOfAccountIdCostIGD.SelectedValue, out chartOfAccountIdCostIGD);
            int.TryParse(cboSubledgerIdCostIGD.SelectedValue, out subLegderIdCostIGD);
            entity.ChartOfAccountIdCostIGD = chartOfAccountIdCostIGD;
            entity.SubledgerIdCostIGD = subLegderIdCostIGD;

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
            {
                int chartOfAccountIdCOGSIGDTemp = 0;
                int subLegderIdCOGSIGDTemp = 0;
                int.TryParse(cboChartOfAccountIdCOGSIGDTemp.SelectedValue, out chartOfAccountIdCOGSIGDTemp);
                int.TryParse(cboSubledgerIdCOGSIGDTemp.SelectedValue, out subLegderIdCOGSIGDTemp);
                entity.ChartOfAccountIdCOGSIGDTemp = chartOfAccountIdCOGSIGDTemp;
                entity.SubledgerIdCOGSIGDTemp = subLegderIdCOGSIGDTemp;
            }

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(ProductAccount entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            ProductAccountQuery que = new ProductAccountQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ProductAccountID > txtProductAccountID.Text);
                que.OrderBy(que.ProductAccountID.Ascending);
            }
            else
            {
                que.Where(que.ProductAccountID < txtProductAccountID.Text);
                que.OrderBy(que.ProductAccountID.Descending);
            }
            ProductAccount entity = new ProductAccount();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged OutPatient

        protected void cboChartOfAccountIdIncome_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdIncome.Items.Clear();
            cboSubledgerIdIncome.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdIncome.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdIncome.Items.Clear();
                cboChartOfAccountIdIncome.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdAcrual_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdAcrual.Items.Clear();
            cboSubledgerIdAcrual.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdAcrual.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdAcrual.Items.Clear();
                cboChartOfAccountIdAcrual.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdDiscount_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdDiscount.Items.Clear();
            cboSubledgerIdDiscount.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdDiscount.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdDiscount.Items.Clear();
                cboChartOfAccountIdDiscount.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdInventory_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdInventory.Items.Clear();
            cboSubledgerIdInventory.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdInventory.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdInventory.Items.Clear();
                cboChartOfAccountIdInventory.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdCOGS_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdCOGS.Items.Clear();
            cboSubledgerIdCOGS.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdCOGS.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdCOGS.Items.Clear();
                cboChartOfAccountIdCOGS.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdCOGSOPTemp_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdCOGSOPTemp.Items.Clear();
            cboSubledgerIdCOGSOPTemp.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboSubledgerIdCOGSOPTemp.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboSubledgerIdCOGSOPTemp.Items.Clear();
                cboSubledgerIdCOGSOPTemp.Text = string.Empty;
                return;
            }
        }


        protected void cboChartOfAccountIdSalesReturn_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdSalesReturn.Items.Clear();
            cboSubledgerIdSalesReturn.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdSalesReturn.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdSalesReturn.Items.Clear();
                cboChartOfAccountIdSalesReturn.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdPurchaseReturn_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdPurchaseReturn.Items.Clear();
            cboSubledgerIdPurchaseReturn.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdPurchaseReturn.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdPurchaseReturn.Items.Clear();
                cboChartOfAccountIdPurchaseReturn.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdCost_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdCost.Items.Clear();
            cboSubledgerIdCost.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdCost.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdCost.Items.Clear();
                cboChartOfAccountIdCost.Text = string.Empty;
                return;
            }
        }


        #endregion

        #region Method & Event TextChanged Inpatient

        protected void cboChartOfAccountIdIncomeIP_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdIncomeIP.Items.Clear();
            cboSubledgerIdIncomeIP.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdIncomeIP.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdIncomeIP.Items.Clear();
                cboChartOfAccountIdIncomeIP.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdAcrualIP_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdAcrualIP.Items.Clear();
            cboSubledgerIdAcrualIP.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdAcrualIP.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdAcrualIP.Items.Clear();
                cboChartOfAccountIdAcrualIP.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdDiscountIP_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdDiscountIP.Items.Clear();
            cboSubledgerIdDiscountIP.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdDiscountIP.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdDiscountIP.Items.Clear();
                cboChartOfAccountIdDiscountIP.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdInventoryIP_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdInventoryIP.Items.Clear();
            cboSubledgerIdInventoryIP.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdInventoryIP.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdInventoryIP.Items.Clear();
                cboChartOfAccountIdInventoryIP.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdCOGSIP_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdCOGSIP.Items.Clear();
            cboSubledgerIdCOGSIP.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdCOGSIP.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdCOGSIP.Items.Clear();
                cboChartOfAccountIdCOGSIP.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdCOGSIPTemp_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdCOGSIPTemp.Items.Clear();
            cboSubledgerIdCOGSIPTemp.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdCOGSIPTemp.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdCOGSIPTemp.Items.Clear();
                cboChartOfAccountIdCOGSIPTemp.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdSalesReturnIP_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdSalesReturnIP.Items.Clear();
            cboSubledgerIdSalesReturnIP.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdSalesReturnIP.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdSalesReturnIP.Items.Clear();
                cboChartOfAccountIdSalesReturnIP.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdPurchaseReturnIP_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdPurchaseReturnIP.Items.Clear();
            cboSubledgerIdPurchaseReturnIP.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdPurchaseReturnIP.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdPurchaseReturnIP.Items.Clear();
                cboChartOfAccountIdPurchaseReturnIP.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdCostIP_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdCostIP.Items.Clear();
            cboSubledgerIdCostIP.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdCostIP.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdCostIP.Items.Clear();
                cboChartOfAccountIdCostIP.Text = string.Empty;
                return;
            }
        }

        #endregion

        #region Method & Event TextChanged Emergency

        protected void cboChartOfAccountIdIncomeIGD_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdIncomeIGD.Items.Clear();
            cboSubledgerIdIncomeIGD.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdIncomeIGD.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdIncomeIGD.Items.Clear();
                cboChartOfAccountIdIncomeIGD.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdAcrualIGD_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdAcrualIGD.Items.Clear();
            cboSubledgerIdAcrualIGD.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdAcrualIGD.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdAcrualIGD.Items.Clear();
                cboChartOfAccountIdAcrualIGD.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdDiscountIGD_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdDiscountIGD.Items.Clear();
            cboSubledgerIdDiscountIGD.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdDiscountIGD.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdDiscountIGD.Items.Clear();
                cboChartOfAccountIdDiscountIGD.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdInventoryIGD_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdInventoryIGD.Items.Clear();
            cboSubledgerIdInventoryIGD.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdInventoryIGD.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdInventoryIGD.Items.Clear();
                cboChartOfAccountIdInventoryIGD.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdCOGSIGD_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdCOGSIGD.Items.Clear();
            cboSubledgerIdCOGSIGD.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdCOGSIGD.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdCOGSIGD.Items.Clear();
                cboChartOfAccountIdCOGSIGD.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdCOGSIGDTemp_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdCOGSIGDTemp.Items.Clear();
            cboSubledgerIdCOGSIGDTemp.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdCOGSIGDTemp.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdCOGSIGDTemp.Items.Clear();
                cboChartOfAccountIdCOGSIGDTemp.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdSalesReturnIGD_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdSalesReturnIGD.Items.Clear();
            cboSubledgerIdSalesReturnIGD.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdSalesReturnIGD.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdSalesReturnIGD.Items.Clear();
                cboChartOfAccountIdSalesReturnIGD.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdPurchaseReturnIGD_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdPurchaseReturnIGD.Items.Clear();
            cboSubledgerIdPurchaseReturnIGD.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdPurchaseReturnIGD.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdPurchaseReturnIGD.Items.Clear();
                cboChartOfAccountIdPurchaseReturnIGD.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdCostIGD_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerIdCostIGD.Items.Clear();
            cboSubledgerIdCostIGD.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountIdCostIGD.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountIdCostIGD.Items.Clear();
                cboChartOfAccountIdCostIGD.Text = string.Empty;
                return;
            }
        }

        #endregion

        #region ComboBox OutPatient
        #region ComboBox ChartOfAccountIdIncome
        protected void cboChartOfAccountIdIncome_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdIncome.DataSource = dtb;
            cboChartOfAccountIdIncome.DataBind();
        }

        protected void cboChartOfAccountIdIncome_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdIncome
        protected void cboSubledgerIdIncome_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdIncome.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdIncome.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdIncome.DataSource = dtb;
            cboSubledgerIdIncome.DataBind();
        }

        protected void cboSubledgerIdIncome_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdAcrual
        protected void cboChartOfAccountIdAcrual_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdAcrual.DataSource = dtb;
            cboChartOfAccountIdAcrual.DataBind();
        }

        protected void cboChartOfAccountIdAcrual_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdAcrual
        protected void cboSubledgerIdAcrual_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdAcrual.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdAcrual.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdAcrual.DataSource = dtb;
            cboSubledgerIdAcrual.DataBind();
        }

        protected void cboSubledgerIdAcrual_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdDiscount
        protected void cboChartOfAccountIdDiscount_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdDiscount.DataSource = dtb;
            cboChartOfAccountIdDiscount.DataBind();
        }

        protected void cboChartOfAccountIdDiscount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdDiscount
        protected void cboSubledgerIdDiscount_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdDiscount.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdDiscount.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdDiscount.DataSource = dtb;
            cboSubledgerIdDiscount.DataBind();
        }

        protected void cboSubledgerIdDiscount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdInventory
        protected void cboChartOfAccountIdInventory_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdInventory.DataSource = dtb;
            cboChartOfAccountIdInventory.DataBind();
        }

        protected void cboChartOfAccountIdInventory_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdInventory
        protected void cboSubledgerIdInventory_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdInventory.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdInventory.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdInventory.DataSource = dtb;
            cboSubledgerIdInventory.DataBind();
        }

        protected void cboSubledgerIdInventory_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdCOGS
        protected void cboChartOfAccountIdCOGS_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            (sender as RadComboBox).DataSource = dtb;
            (sender as RadComboBox).DataBind();
        }

        protected void cboChartOfAccountIdCOGS_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdCOGS
        protected void cboSubledgerIdCOGS_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdCOGS.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdCOGS.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            (sender as RadComboBox).DataSource = dtb;
            (sender as RadComboBox).DataBind();
        }

        protected void cboSubledgerIdCOGS_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdSalesReturn
        protected void cboChartOfAccountIdSalesReturn_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdSalesReturn.DataSource = dtb;
            cboChartOfAccountIdSalesReturn.DataBind();
        }

        protected void cboChartOfAccountIdSalesReturn_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdSalesReturn
        protected void cboSubledgerIdSalesReturn_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdSalesReturn.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdSalesReturn.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdSalesReturn.DataSource = dtb;
            cboSubledgerIdSalesReturn.DataBind();
        }

        protected void cboSubledgerIdSalesReturn_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdPurchaseReturn
        protected void cboChartOfAccountIdPurchaseReturn_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdPurchaseReturn.DataSource = dtb;
            cboChartOfAccountIdPurchaseReturn.DataBind();
        }

        protected void cboChartOfAccountIdPurchaseReturn_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdPurchaseReturn
        protected void cboSubledgerIdPurchaseReturn_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdPurchaseReturn.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdPurchaseReturn.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdPurchaseReturn.DataSource = dtb;
            cboSubledgerIdPurchaseReturn.DataBind();
        }

        protected void cboSubledgerIdPurchaseReturn_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdCost
        protected void cboChartOfAccountIdCost_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdCost.DataSource = dtb;
            cboChartOfAccountIdCost.DataBind();
        }

        protected void cboChartOfAccountIdCost_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdCost
        protected void cboSubledgerIdCost_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdCost.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdCost.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdCost.DataSource = dtb;
            cboSubledgerIdCost.DataBind();
        }

        protected void cboSubledgerIdCost_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #endregion

        #region ComboBox InPatient

        #region ComboBox ChartOfAccountIdIncome
        protected void cboChartOfAccountIdIncomeIP_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdIncomeIP.DataSource = dtb;
            cboChartOfAccountIdIncomeIP.DataBind();
        }

        protected void cboChartOfAccountIdIncomeIP_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdIncome
        protected void cboSubledgerIdIncomeIP_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdIncomeIP.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdIncomeIP.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdIncomeIP.DataSource = dtb;
            cboSubledgerIdIncomeIP.DataBind();
        }

        protected void cboSubledgerIdIncomeIP_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdAcrual
        protected void cboChartOfAccountIdAcrualIP_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdAcrualIP.DataSource = dtb;
            cboChartOfAccountIdAcrualIP.DataBind();
        }

        protected void cboChartOfAccountIdAcrualIP_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdAcrual
        protected void cboSubledgerIdAcrualIP_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdAcrualIP.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdAcrualIP.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdAcrualIP.DataSource = dtb;
            cboSubledgerIdAcrualIP.DataBind();
        }

        protected void cboSubledgerIdAcrualIP_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdDiscount
        protected void cboChartOfAccountIdDiscountIP_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdDiscountIP.DataSource = dtb;
            cboChartOfAccountIdDiscountIP.DataBind();
        }

        protected void cboChartOfAccountIdDiscountIP_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdDiscount
        protected void cboSubledgerIdDiscountIP_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdDiscountIP.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdDiscountIP.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdDiscountIP.DataSource = dtb;
            cboSubledgerIdDiscountIP.DataBind();
        }

        protected void cboSubledgerIdDiscountIP_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdInventory
        protected void cboChartOfAccountIdInventoryIP_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdInventoryIP.DataSource = dtb;
            cboChartOfAccountIdInventoryIP.DataBind();
        }

        protected void cboChartOfAccountIdInventoryIP_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdInventory
        protected void cboSubledgerIdInventoryIP_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdInventoryIP.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdInventoryIP.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdInventoryIP.DataSource = dtb;
            cboSubledgerIdInventoryIP.DataBind();
        }

        protected void cboSubledgerIdInventoryIP_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdCOGS
        protected void cboChartOfAccountIdCOGSIP_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            (sender as RadComboBox).DataSource = dtb;
            (sender as RadComboBox).DataBind();
        }

        protected void cboChartOfAccountIdCOGSIP_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdCOGS
        protected void cboSubledgerIdCOGSIP_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdCOGSIP.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdCOGSIP.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            (sender as RadComboBox).DataSource = dtb;
            (sender as RadComboBox).DataBind();
        }

        protected void cboSubledgerIdCOGSIP_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdSalesReturn
        protected void cboChartOfAccountIdSalesReturnIP_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdSalesReturnIP.DataSource = dtb;
            cboChartOfAccountIdSalesReturnIP.DataBind();
        }

        protected void cboChartOfAccountIdSalesReturnIP_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdSalesReturn
        protected void cboSubledgerIdSalesReturnIP_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdSalesReturnIP.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdSalesReturnIP.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdSalesReturnIP.DataSource = dtb;
            cboSubledgerIdSalesReturnIP.DataBind();
        }

        protected void cboSubledgerIdSalesReturnIP_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdPurchaseReturn
        protected void cboChartOfAccountIdPurchaseReturnIP_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdPurchaseReturnIP.DataSource = dtb;
            cboChartOfAccountIdPurchaseReturnIP.DataBind();
        }

        protected void cboChartOfAccountIdPurchaseReturnIP_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdPurchaseReturn
        protected void cboSubledgerIdPurchaseReturnIP_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdPurchaseReturnIP.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdPurchaseReturnIP.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdPurchaseReturnIP.DataSource = dtb;
            cboSubledgerIdPurchaseReturnIP.DataBind();
        }

        protected void cboSubledgerIdPurchaseReturnIP_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdCost
        protected void cboChartOfAccountIdCostIP_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdCostIP.DataSource = dtb;
            cboChartOfAccountIdCostIP.DataBind();
        }

        protected void cboChartOfAccountIdCostIP_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdCost
        protected void cboSubledgerIdCostIP_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdCostIP.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdCostIP.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdCostIP.DataSource = dtb;
            cboSubledgerIdCostIP.DataBind();
        }

        protected void cboSubledgerIdCostIP_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #endregion

        #region ComboBox Emergency

        #region ComboBox ChartOfAccountIdIncome
        protected void cboChartOfAccountIdIncomeIGD_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdIncomeIGD.DataSource = dtb;
            cboChartOfAccountIdIncomeIGD.DataBind();
        }

        protected void cboChartOfAccountIdIncomeIGD_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdIncome
        protected void cboSubledgerIdIncomeIGD_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdIncomeIGD.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdIncomeIGD.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdIncomeIGD.DataSource = dtb;
            cboSubledgerIdIncomeIGD.DataBind();
        }

        protected void cboSubledgerIdIncomeIGD_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdAcrual
        protected void cboChartOfAccountIdAcrualIGD_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdAcrualIGD.DataSource = dtb;
            cboChartOfAccountIdAcrualIGD.DataBind();
        }

        protected void cboChartOfAccountIdAcrualIGD_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdAcrual
        protected void cboSubledgerIdAcrualIGD_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdAcrualIGD.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdAcrualIGD.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdAcrualIGD.DataSource = dtb;
            cboSubledgerIdAcrualIGD.DataBind();
        }

        protected void cboSubledgerIdAcrualIGD_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdDiscount
        protected void cboChartOfAccountIdDiscountIGD_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdDiscountIGD.DataSource = dtb;
            cboChartOfAccountIdDiscountIGD.DataBind();
        }

        protected void cboChartOfAccountIdDiscountIGD_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdDiscount
        protected void cboSubledgerIdDiscountIGD_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdDiscountIGD.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdDiscountIGD.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdDiscountIGD.DataSource = dtb;
            cboSubledgerIdDiscountIGD.DataBind();
        }

        protected void cboSubledgerIdDiscountIGD_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdInventory
        protected void cboChartOfAccountIdInventoryIGD_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdInventoryIGD.DataSource = dtb;
            cboChartOfAccountIdInventoryIGD.DataBind();
        }

        protected void cboChartOfAccountIdInventoryIGD_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdInventory
        protected void cboSubledgerIdInventoryIGD_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdInventoryIGD.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdInventoryIGD.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdInventoryIGD.DataSource = dtb;
            cboSubledgerIdInventoryIGD.DataBind();
        }

        protected void cboSubledgerIdInventoryIGD_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdCOGS
        protected void cboChartOfAccountIdCOGSIGD_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            (sender as RadComboBox).DataSource = dtb;
            (sender as RadComboBox).DataBind();
        }

        protected void cboChartOfAccountIdCOGSIGD_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdCOGS
        protected void cboSubledgerIdCOGSIGD_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdCOGSIGD.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdCOGSIGD.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            (sender as RadComboBox).DataSource = dtb;
            (sender as RadComboBox).DataBind();
        }

        protected void cboSubledgerIdCOGSIGD_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdSalesReturn
        protected void cboChartOfAccountIdSalesReturnIGD_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdSalesReturnIGD.DataSource = dtb;
            cboChartOfAccountIdSalesReturnIGD.DataBind();
        }

        protected void cboChartOfAccountIdSalesReturnIGD_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdSalesReturn
        protected void cboSubledgerIdSalesReturnIGD_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdSalesReturnIGD.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdSalesReturnIGD.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdSalesReturnIGD.DataSource = dtb;
            cboSubledgerIdSalesReturnIGD.DataBind();
        }

        protected void cboSubledgerIdSalesReturnIGD_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdPurchaseReturn
        protected void cboChartOfAccountIdPurchaseReturnIGD_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdPurchaseReturnIGD.DataSource = dtb;
            cboChartOfAccountIdPurchaseReturnIGD.DataBind();
        }

        protected void cboChartOfAccountIdPurchaseReturnIGD_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdPurchaseReturn
        protected void cboSubledgerIdPurchaseReturnIGD_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdPurchaseReturnIGD.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdPurchaseReturnIGD.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdPurchaseReturnIGD.DataSource = dtb;
            cboSubledgerIdPurchaseReturnIGD.DataBind();
        }

        protected void cboSubledgerIdPurchaseReturnIGD_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #region ComboBox ChartOfAccountIdCost
        protected void cboChartOfAccountIdCostIGD_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountIdCostIGD.DataSource = dtb;
            cboChartOfAccountIdCostIGD.DataBind();
        }

        protected void cboChartOfAccountIdCostIGD_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerIdCost
        protected void cboSubledgerIdCostIGD_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountIdCostIGD.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdCostIGD.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerIdCostIGD.DataSource = dtb;
            cboSubledgerIdCostIGD.DataBind();
        }

        protected void cboSubledgerIdCostIGD_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        #endregion
    }
}
