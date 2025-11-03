using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class LocationDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "LocationSearch.aspx";
            UrlPageList = "LocationList.aspx";

            ProgramID = AppConstant.Program.Location;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                hdnPageId.Value = PageID;

                StandardReference.InitializeIncludeSpace(cboSRTypeOfInventory, AppEnum.StandardReference.TypeOfInventory);

                trTypeOfInventory.Visible = AppSession.Parameter.IsSeparationBetweenOutpatientAndInpatientSupplies;

                var std = new AppStandardReferenceItem();
                if (std.LoadByPrimaryKey(AppEnum.StandardReference.ItemType.ToString(),
                                     BusinessObject.Reference.ItemType.Kitchen))
                    tabDetail.Tabs[2].Visible = std.IsActive ?? false;
                tabDetail.Tabs[5].Visible = !AppSession.Parameter.IsDistributionAutoConfirm;

                trIsAllowedToStockGoods.Visible = AppSession.Parameter.IsMaterialUsedAwoNeedRequest;
                grdItemMedic.Columns.FindByUniqueName("ItemSubBin").Visible = AppSession.Parameter.IsUsingItemSubBin;
                grdItemNonMedic.Columns.FindByUniqueName("ItemSubBin").Visible = AppSession.Parameter.IsUsingItemSubBin;
                grdItemKitchen.Columns.FindByUniqueName("ItemSubBin").Visible = AppSession.Parameter.IsUsingItemSubBin;
            }

            //PopUp Search
            if (!IsCallback)
            {
                //PopUpSearch.Initialize(AppEnum.PopUpSearch.Parent, Page, txtParentID);
                PopUpSearch.Initialize(AppEnum.PopUpSearch.ItemGroup, Page, txtItemGroupID);
                PopUpSearch.Initialize(AppEnum.PopUpSearch.Permit, Page, txtPermitID);
                StandardReference.InitializeIncludeSpace(cboSRStockGroup, AppEnum.StandardReference.StockGroup);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItemMedic, grdItemMedic);
            ajax.AddAjaxSetting(grdItemNonMedic, grdItemNonMedic);
            ajax.AddAjaxSetting(grdItemKitchen, grdItemKitchen);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Location());
            cboSRTypeOfInventory.SelectedValue = string.Empty;
            cboSRTypeOfInventory.Text = string.Empty;
            cboSRStockGroup.SelectedValue = string.Empty;
            cboSRStockGroup.Text = string.Empty;
            chkIsActive.Checked = true;
            chkIsAllowedToStockGoods.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var ib = new ItemBalanceCollection();
            ib.Query.Where(ib.Query.LocationID == txtLocationID.Text);
            ib.LoadAll();

            if (ib.Count > 0)
            {
                args.MessageText = AppConstant.Message.RecordHasUsed;
                args.IsCancel = true;
                return;
            }

            var su = new ServiceUnitLocationCollection();
            su.Query.Where(su.Query.LocationID == txtLocationID.Text);
            su.LoadAll();

            if (su.Count > 0)
            {
                args.MessageText = AppConstant.Message.RecordHasUsed;
                args.IsCancel = true;
                return;
            }

            var entity = new Location();
            entity.LoadByPrimaryKey(txtLocationID.Text);
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new Location();
            if (entity.LoadByPrimaryKey(txtLocationID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new Location();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new Location();
            if (entity.LoadByPrimaryKey(txtLocationID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
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
            auditLogFilter.PrimaryKeyData = string.Format("LocationID='{0}'", txtLocationID.Text.Trim());
            auditLogFilter.TableName = "Location";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_LocationID", txtLocationID.Text);

            switch (programID)
            {
                case AppConstant.Report.Location_ItemMedic:
                    {
                        printJobParameters.AddNew("p_ItemType", BusinessObject.Reference.ItemType.Medical);
                        break;
                    }
                case AppConstant.Report.Location_ItemNonMedic:
                    {
                        printJobParameters.AddNew("p_ItemType", BusinessObject.Reference.ItemType.NonMedical);
                        break;
                    }
                case AppConstant.Report.Location_ItemKitchen:
                    {
                        printJobParameters.AddNew("p_ItemType", BusinessObject.Reference.ItemType.Kitchen);
                        break;
                    }
            }
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtLocationID.ReadOnly = (newVal != AppEnum.DataMode.New);
            RefreshCommandItemMedicBalance(newVal);
            RefreshCommandItemNonMedicBalance(newVal);
            RefreshCommandItemKitchenBalance(newVal);
            RefreshCommandItemBin(newVal);
            RefreshCommandLocationException(newVal); 
            RefreshCommandLocationExceptionDistributionConfirm(newVal);

            txtFilterItemMedic.ReadOnly = false;
            txtFilterItemNonMedic.ReadOnly = false;
            txtFilterItemKitchen.ReadOnly = false;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Location();
            if (parameters.Length > 0)
            {
                String locationID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(locationID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtLocationID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var location = (Location)entity;
            txtLocationID.Text = location.LocationID;
            txtLocationName.Text = location.LocationName;
            txtShortName.Text = location.ShortName;
            txtPermitID.Text = location.PermitID;
            PopulatePermitName(false);
            chkIsHeader.Checked = location.IsHeader ?? false;
            chkIsHoldForTransaction.Checked = location.IsHoldForTransaction ?? false;
            chkIsActive.Checked = location.IsActive ?? false;
            chkIsAllowedToStockGoods.Checked = location.IsAllowedToStockGoods ?? false;
            cboSRTypeOfInventory.SelectedValue = location.SRTypeOfInventory;
            chkIsConsignment.Checked = location.IsConsignment ?? false;
            chkIsValidateMaxValueOnDistReqForIpm.Checked = location.IsValidateMaxValueOnDistReqForIpm ?? false;
            chkIsValidateMaxValueOnDistReqForIpnm.Checked = location.IsValidateMaxValueOnDistReqForIpnm ?? false;
            chkIsValidateMaxValueOnDistReqForIk.Checked = location.IsValidateMaxValueOnDistReqForIk ?? false;
            chkIsValidateMaxValueOnPurcReqForIpm.Checked = location.IsValidateMaxValueOnPurcReqForIpm ?? false;
            chkIsValidateMaxValueOnPurcReqForIpnm.Checked = location.IsValidateMaxValueOnPurcReqForIpnm ?? false;
            chkIsValidateMaxValueOnPurcReqForIk.Checked = location.IsValidateMaxValueOnPurcReqForIk ?? false;
            cboSRStockGroup.SelectedValue = location.SRStockGroup;
            //ComboBox.SelectedValue(cboSRStockGroup, location.SRStockGroup);
            chkIsAutoUpdateStockMinMax.Checked = location.IsAutoUpdateStockMinMax ?? false;
            if (txtLocationID.Text != string.Empty)
            {
                // --Income--
                int coaIncome = (location.ChartOfAccountIdIncome.HasValue ? location.ChartOfAccountIdIncome.Value : 0);
                int slIncome = (location.SubledgerIdIncome.HasValue ? location.SubledgerIdIncome.Value : 0);
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
                int coaAcrual = (location.ChartOfAccountIdAcrual.HasValue ? location.ChartOfAccountIdAcrual.Value : 0);
                int slAcrual = (location.SubledgerIdAcrual.HasValue ? location.SubledgerIdAcrual.Value : 0);
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
                int coaDiscount = (location.ChartOfAccountIdDiscount.HasValue ? location.ChartOfAccountIdDiscount.Value : 0);
                int slDiscount = (location.SubledgerIdDiscount.HasValue ? location.SubledgerIdDiscount.Value : 0);
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
                int coaInventory = (location.ChartOfAccountIdInventory.HasValue ? location.ChartOfAccountIdInventory.Value : 0);
                int slInventory = (location.SubledgerIdInventory.HasValue ? location.SubledgerIdInventory.Value : 0);
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
                int coaCOGS = (location.ChartOfAccountIdCOGS.HasValue ? location.ChartOfAccountIdCOGS.Value : 0);
                int slCOGS = (location.SubledgerIdCOGS.HasValue ? location.SubledgerIdCOGS.Value : 0);
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

                // --SalesReturn--
                int coaSalesReturn = (location.ChartOfAccountIdSalesReturn.HasValue ? location.ChartOfAccountIdSalesReturn.Value : 0);
                int slSalesReturn = (location.SubledgerIdSalesReturn.HasValue ? location.SubledgerIdSalesReturn.Value : 0);
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
                int coaPurchaseReturn = (location.ChartOfAccountIdPurchaseReturn.HasValue ? location.ChartOfAccountIdPurchaseReturn.Value : 0);
                int slPurchaseReturn = (location.SubledgerIdPurchaseReturn.HasValue ? location.SubledgerIdPurchaseReturn.Value : 0);
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
                int coaCost = (location.ChartOfAccountIdCost.HasValue ? location.ChartOfAccountIdCost.Value : 0);
                int slCost = (location.SubledgerIdCost.HasValue ? location.SubledgerIdCost.Value : 0);
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

            }
            else
            {
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
            }

            PopulateItemMedicBalanceGrid();
            PopulateItemNonMedicBalanceGrid();
            PopulateItemKitchenBalanceGrid();
            PopulateItemBinGrid();
            PopulateLocationExceptionGrid();
            PopulateLocationExceptionDistributionConfirmGrid();

            var coll = ItemMedicBalanceDetails;
            var coll2 = ItemNonMedicBalanceDetails;
            var coll3 = ItemKitchenBalanceDetails;
        }

        private void PopulateCboChartOfAccount(RadComboBox comboBox, int coaId)
        {
            var coaQ = new ChartOfAccountsQuery();
            coaQ.Select(coaQ.ChartOfAccountId, coaQ.ChartOfAccountCode, coaQ.ChartOfAccountName);
            coaQ.Where(coaQ.ChartOfAccountId == coaId);
            DataTable dtbCoa = coaQ.LoadDataTable();
            comboBox.DataSource = dtbCoa;
            comboBox.DataBind();
            comboBox.SelectedValue = coaId.ToString();
        }

        private void PopulateCboSubLedger(RadComboBox comboBox, int subLedgerID)
        {
            var slQ = new SubLedgersQuery();
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

        private void SetEntityValue(Location entity)
        {
            entity.LocationID = txtLocationID.Text;
            entity.LocationName = txtLocationName.Text;
            entity.ShortName = txtShortName.Text;
            entity.ParentID = string.Empty;
            entity.ItemGroupID = string.Empty;
            entity.PermitID = txtPermitID.Text;
            entity.IsHeader = false;

            entity.IsActive = chkIsActive.Checked;
            entity.IsAllowedToStockGoods = chkIsAllowedToStockGoods.Checked;
            entity.SRTypeOfInventory = cboSRTypeOfInventory.SelectedValue;
            entity.IsConsignment = chkIsConsignment.Checked;
            entity.IsValidateMaxValueOnDistReqForIpm = chkIsValidateMaxValueOnDistReqForIpm.Checked;
            entity.IsValidateMaxValueOnDistReqForIpnm = chkIsValidateMaxValueOnDistReqForIpnm.Checked;
            entity.IsValidateMaxValueOnDistReqForIk = chkIsValidateMaxValueOnDistReqForIk.Checked;
            entity.IsValidateMaxValueOnPurcReqForIpm = chkIsValidateMaxValueOnPurcReqForIpm.Checked;
            entity.IsValidateMaxValueOnPurcReqForIpnm = chkIsValidateMaxValueOnPurcReqForIpnm.Checked;
            entity.IsValidateMaxValueOnPurcReqForIk = chkIsValidateMaxValueOnPurcReqForIk.Checked;
            entity.SRStockGroup = cboSRStockGroup.SelectedValue;
            entity.IsAutoUpdateStockMinMax = chkIsAutoUpdateStockMinMax.Checked;

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

            bool isHold = entity.IsHoldForTransaction ?? false;
            entity.IsHoldForTransaction = chkIsHoldForTransaction.Checked;
            if (chkIsHoldForTransaction.Checked != isHold)
            {
                entity.LastHoldForTransactionDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastHoldForTransactionByUserID = AppSession.UserLogin.UserID;
            }

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            //Update Detil
            foreach (ItemBalance item in ItemMedicBalances)
            {
                item.LocationID = txtLocationID.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            foreach (ItemBalanceDetail item in ItemMedicBalanceDetails)
            {
                item.LocationID = txtLocationID.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            foreach (ItemBalance item in ItemNonMedicBalances)
            {
                item.LocationID = txtLocationID.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            foreach (ItemBalanceDetail item in ItemNonMedicBalanceDetails)
            {
                item.LocationID = txtLocationID.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            foreach (ItemBalance item in ItemKitchenBalances)
            {
                item.LocationID = txtLocationID.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            foreach (ItemBalanceDetail item in ItemKitchenBalanceDetails)
            {
                item.LocationID = txtLocationID.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            foreach (AppStandardReferenceItem item in ItemBins)
            {
                //item.ReferenceID = txtLocationID.Text;
                item.IsActive = true;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            foreach (LocationException item in LocationExceptions)
            {
                item.LocationID = txtLocationID.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            foreach (LocationExceptionDistributionConfirm item in LocationExceptionDistributionConfirms)
            {
                item.LocationID = txtLocationID.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
        }

        private void SaveEntity(Location entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ItemMedicBalances.Save();
                ItemNonMedicBalances.Save();
                ItemKitchenBalances.Save();

                ItemMedicBalanceDetails.Save();
                ItemNonMedicBalanceDetails.Save();
                ItemKitchenBalanceDetails.Save();

                ItemBins.Save();
                LocationExceptions.Save();
                LocationExceptionDistributionConfirms.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new LocationQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.LocationID > txtLocationID.Text);
                que.OrderBy(que.LocationID.Ascending);
            }
            else
            {
                que.Where(que.LocationID < txtLocationID.Text);
                que.OrderBy(que.LocationID.Descending);
            }
            var entity = new Location();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged

        protected void txtParentID_TextChanged(object sender, EventArgs e)
        {
            PopulateParentName(true);
        }

        private void PopulateParentName(bool isResetIdIfNotExist)
        {
            ////TODO: Fix generated Code
            //if (txtParentID.Text == string.Empty)
            //{
            //    lblParentName.Text = string.Empty;
            //    return;
            //}
            //Parent entity = new Parent();
            //if (entity.LoadByPrimaryKey(txtParentID.Text))
            //    lblParentName.Text = entity.ParentName;
            //else
            //{
            //    lblParentName.Text = string.Empty;
            //    if (isResetIdIfNotExist)
            //        txtParentID.Text = string.Empty;
            //}
        }

        protected void txtItemGroupID_TextChanged(object sender, EventArgs e)
        {
            PopulateItemGroupName(true);
        }

        private void PopulateItemGroupName(bool isResetIdIfNotExist)
        {
            ////TODO: Fix generated Code
            //if (txtItemGroupID.Text == string.Empty)
            //{
            //    lblItemGroupName.Text = string.Empty;
            //    return;
            //}
            //ItemGroup entity = new ItemGroup();
            //if (entity.LoadByPrimaryKey(txtItemGroupID.Text))
            //    lblItemGroupName.Text = entity.ItemGroupName;
            //else
            //{
            //    lblItemGroupName.Text = string.Empty;
            //    if (isResetIdIfNotExist)
            //        txtItemGroupID.Text = string.Empty;
            //}
        }

        protected void txtPermitID_TextChanged(object sender, EventArgs e)
        {
            PopulatePermitName(true);
        }

        private void PopulatePermitName(bool isResetIdIfNotExist)
        {
            if (txtPermitID.Text == string.Empty)
            {
                lblPermitName.Text = string.Empty;
                return;
            }
            LocationPermit entity = new LocationPermit();
            if (entity.LoadByPrimaryKey(txtPermitID.Text))
                lblPermitName.Text = entity.PermitName;
            else
            {
                lblPermitName.Text = string.Empty;
                if (isResetIdIfNotExist)
                    txtPermitID.Text = string.Empty;
            }
        }

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

        #region Record Detail Method Function Item Medic
        private void RefreshCommandItemMedicBalance(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemMedic.Columns[0].Visible = isVisible;
            grdItemMedic.Columns[grdItemMedic.Columns.Count - 1].Visible = isVisible;

            grdItemMedic.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItemMedic.Rebind();
        }

        private ItemBalanceCollection ItemMedicBalances
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemMedic" + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((ItemBalanceCollection)(obj));
                    }
                }

                var coll = new ItemBalanceCollection();

                var query = new ItemBalanceQuery("a");
                var qrItemMed = new ItemProductMedicQuery("b");
                var qrRef = new AppStandardReferenceItemQuery("c");
                var qrItem = new ItemQuery("d");
                var qrItemBin = new AppStandardReferenceItemQuery("e");

                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID);
                query.LeftJoin(qrRef).On(qrItemMed.SRItemUnit == qrRef.ItemID & qrRef.StandardReferenceID == "ItemUnit");
                query.InnerJoin(qrItem).On(query.ItemID == qrItem.ItemID);
                query.LeftJoin(qrItemBin).On(query.SRItemBin == qrItemBin.ItemID &
                                             qrItemBin.StandardReferenceID == "ItemBin");

                query.Where(query.LocationID == txtLocationID.Text);

                query.Select(query, qrItem.ItemName.As("refToItem_ItemName"), qrRef.ItemName.As("refToSRI_ItemUnit"),
                             qrItemBin.ItemName.As("refToSRI_ItemBin"));
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collItemMedic" + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collItemMedic" + hdnPageId.Value] = value; }
        }

        private ItemBalanceDetailCollection ItemMedicBalanceDetails
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemMedicDetail" + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((ItemBalanceDetailCollection)(obj));
                    }
                }

                var coll = new ItemBalanceDetailCollection();
                var query = new ItemBalanceDetailQuery("a");
                query.Where(query.LocationID == txtLocationID.Text);

                query.Select(query);
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collItemMedicDetail" + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collItemMedicDetail" + hdnPageId.Value] = value; }
        }

        private void PopulateItemMedicBalanceGrid()
        {
            //Display Data Detail
            ItemMedicBalances = null; //Reset Record Detail
            grdItemMedic.DataSource = ItemMedicBalances; //Requery
            grdItemMedic.MasterTableView.IsItemInserted = false;
            grdItemMedic.MasterTableView.ClearEditItems();
            grdItemMedic.DataBind();
        }

        protected void grdItemMedic_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (txtFilterItemMedic.Text.Trim() != string.Empty)
            {
                var ds = from d in ItemMedicBalances
                         where d.ItemName.ToLower().Contains(txtFilterItemMedic.Text.ToLower()) || d.ItemID.ToLower().Contains(txtFilterItemMedic.Text.ToLower())
                         select d;
                grdItemMedic.DataSource = ds;
            }
            else
            {
                //var ds = (from d in ItemMedicBalances
                //         orderby d.ItemName
                //         select d).Take(20);
                //grdItemMedic.DataSource = ds;

                grdItemMedic.DataSource = ItemMedicBalances;
            }
        }

        protected void grdItemMedic_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemBalanceMetadata.ColumnNames.ItemID]);
            ItemBalance entity = FindItemMedicBalance(itemID);
            if (entity != null)
                SetEntityValueItemMedic(entity, e);

            //ItemBalanceDetail enDetail = FindItemMedicBalanceDetail(itemID);
            //if (enDetail != null)
            //    SetEntityValueItemMedicDetail(enDetail, e);
        }

        protected void grdItemMedic_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemBalanceMetadata.ColumnNames.ItemID]);
            ItemBalance entity = FindItemMedicBalance(itemID);
            if (entity != null)
            {
                // delete is forbidden if balance > 0!!
                if (entity.Balance != 0)
                {
                    Helper.ShowMessageAfterPostback(this, "Item can not be deleted, balance is not empty");
                    e.Canceled = true;
                    return;
                }
                entity.MarkAsDeleted();
            }

            ItemBalanceDetail enDetail = FindItemMedicBalanceDetail(itemID);
            if (enDetail != null)
                enDetail.MarkAsDeleted();
        }

        protected void grdItemMedic_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemBalance entity = ItemMedicBalances.AddNew();
            SetEntityValueItemMedic(entity, e);

            ItemBalanceDetail enDetail = ItemMedicBalanceDetails.AddNew();
            SetEntityValueItemMedicDetail(enDetail, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemMedic.Rebind();
        }
        private ItemBalance FindItemMedicBalance(String itemID)
        {
            ItemBalanceCollection coll = ItemMedicBalances;
            ItemBalance retEntity = null;
            foreach (ItemBalance rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private ItemBalanceDetail FindItemMedicBalanceDetail(String itemID)
        {
            ItemBalanceDetailCollection coll = ItemMedicBalanceDetails;
            ItemBalanceDetail retEntity = null;
            foreach (ItemBalanceDetail rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValueItemMedic(ItemBalance entity, GridCommandEventArgs e)
        {
            ItemMedicBalanceDetail userControl = (ItemMedicBalanceDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.SRItemUnitName = userControl.SRItemUnitName;
                entity.Minimum = userControl.Minimum;
                entity.Maximum = userControl.Maximum;
                entity.SRItemBin = userControl.SRItemBin;
                entity.SRItemBinName = userControl.SRItemBinName;
                entity.ItemSubBin = userControl.ItemSubBin;
            }
        }

        private void SetEntityValueItemMedicDetail(ItemBalanceDetail entity, GridCommandEventArgs e)
        {
            var userControl = (ItemMedicBalanceDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ReferenceNo = string.Empty;
                entity.TransactionCode = string.Empty;
                entity.BalanceDate = DateTime.Now;
                entity.Balance = 0;
                entity.Booking = 0;
                entity.Price = 0;
            }
        }

        #endregion

        #region Record Detail Method Function Item Non Medic
        private void RefreshCommandItemNonMedicBalance(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemNonMedic.Columns[0].Visible = isVisible;
            grdItemNonMedic.Columns[grdItemNonMedic.Columns.Count - 1].Visible = isVisible;

            grdItemNonMedic.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItemNonMedic.Rebind();
        }

        private ItemBalanceCollection ItemNonMedicBalances
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemNonMedic" + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((ItemBalanceCollection)(obj));
                    }
                }

                var coll = new ItemBalanceCollection();

                var query = new ItemBalanceQuery("a");
                var qrItemMed = new ItemProductNonMedicQuery("b");
                var qrRef = new AppStandardReferenceItemQuery("c");
                var qrItem = new ItemQuery("d");
                var qrItemBin = new AppStandardReferenceItemQuery("e");

                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID);
                query.LeftJoin(qrRef).On(qrItemMed.SRItemUnit == qrRef.ItemID & qrRef.StandardReferenceID == "ItemUnit");
                query.InnerJoin(qrItem).On(query.ItemID == qrItem.ItemID);
                query.LeftJoin(qrItemBin).On(query.SRItemBin == qrItemBin.ItemID &
                                             qrItemBin.StandardReferenceID == "ItemBin");

                query.Where(query.LocationID == txtLocationID.Text);

                query.Select(query, qrItem.ItemName.As("refToItem_ItemName"), qrRef.ItemName.As("refToSRI_ItemUnit"),
                             qrItemBin.ItemName.As("refToSRI_ItemBin"));
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collItemNonMedic" + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collItemNonMedic" + hdnPageId.Value] = value; }
        }

        private ItemBalanceDetailCollection ItemNonMedicBalanceDetails
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemNonMedicDetail" + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((ItemBalanceDetailCollection)(obj));
                    }
                }

                var coll = new ItemBalanceDetailCollection();
                var query = new ItemBalanceDetailQuery("a");
                query.Where(query.LocationID == txtLocationID.Text);
                query.Select(query);
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collItemNonMedicDetail" + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collItemNonMedicDetail" + hdnPageId.Value] = value; }
        }

        private void PopulateItemNonMedicBalanceGrid()
        {
            //Display Data Detail
            ItemNonMedicBalances = null; //Reset Record Detail
            grdItemNonMedic.DataSource = ItemNonMedicBalances; //Requery
            grdItemNonMedic.MasterTableView.IsItemInserted = false;
            grdItemNonMedic.MasterTableView.ClearEditItems();
            grdItemNonMedic.DataBind();
        }

        protected void grdItemNonMedic_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (txtFilterItemNonMedic.Text.Trim() != string.Empty)
            {
                var ds = from d in ItemNonMedicBalances
                         where d.ItemName.ToLower().Contains(txtFilterItemNonMedic.Text.ToLower()) || d.ItemID.ToLower().Contains(txtFilterItemNonMedic.Text.ToLower())
                         select d;
                grdItemNonMedic.DataSource = ds;
            }
            else
            {
                grdItemNonMedic.DataSource = ItemNonMedicBalances;
            }
        }

        protected void grdItemNonMedic_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemBalanceMetadata.ColumnNames.ItemID]);
            ItemBalance entity = FindItemNonMedicBalance(itemID);

            if (entity != null)
                SetEntityValueItemNonMedic(entity, e);

            //ItemBalanceDetail enDetail = FindItemNonMedicBalanceDetail(itemID);
            //if (enDetail != null)
            //    SetEntityValueItemNonMedicDetail(enDetail, e);
        }

        protected void grdItemNonMedic_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemBalanceMetadata.ColumnNames.ItemID]);
            ItemBalance entity = FindItemNonMedicBalance(itemID);
            if (entity != null)
            {
                // delete is forbidden if balance > 0!!
                if (entity.Balance != 0)
                {
                    Helper.ShowMessageAfterPostback(this, "Item can not be deleted, balance is not empty");
                    e.Canceled = true;
                    return;
                }
                entity.MarkAsDeleted();
            }

            ItemBalanceDetail enDetail = FindItemNonMedicBalanceDetail(itemID);
            if (enDetail != null)
                enDetail.MarkAsDeleted();
        }

        protected void grdItemNonMedic_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemBalance entity = ItemNonMedicBalances.AddNew();
            ItemBalanceDetail enDetail = ItemNonMedicBalanceDetails.AddNew();
            SetEntityValueItemNonMedic(entity, e);
            SetEntityValueItemNonMedicDetail(enDetail, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemNonMedic.Rebind();
        }

        private ItemBalance FindItemNonMedicBalance(String itemID)
        {
            ItemBalanceCollection coll = ItemNonMedicBalances;
            ItemBalance retEntity = null;
            foreach (ItemBalance rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private ItemBalanceDetail FindItemNonMedicBalanceDetail(String itemID)
        {
            ItemBalanceDetailCollection coll = ItemNonMedicBalanceDetails;
            ItemBalanceDetail retEntity = null;
            foreach (ItemBalanceDetail rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValueItemNonMedic(ItemBalance entity, GridCommandEventArgs e)
        {
            var userControl = (ItemNonMedicBalanceDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.SRItemUnitName = userControl.SRItemUnitName;
                entity.Minimum = userControl.Minimum;
                entity.Maximum = userControl.Maximum;
                entity.SRItemBin = userControl.SRItemBin;
                entity.SRItemBinName = userControl.SRItemBinName;
                entity.ItemSubBin = userControl.ItemSubBin;
            }
        }

        private void SetEntityValueItemNonMedicDetail(ItemBalanceDetail entity, GridCommandEventArgs e)
        {
            var userControl = (ItemNonMedicBalanceDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ReferenceNo = string.Empty;
                entity.TransactionCode = string.Empty;
                entity.BalanceDate = DateTime.Now;
                entity.Balance = 0;
                entity.Booking = 0;
                entity.Price = 0;
            }
        }

        #endregion

        #region Record Detail Method Function Item Kitchen
        private void RefreshCommandItemKitchenBalance(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemKitchen.Columns[0].Visible = isVisible;
            grdItemKitchen.Columns[grdItemKitchen.Columns.Count - 1].Visible = isVisible;

            grdItemKitchen.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItemKitchen.Rebind();
        }

        private ItemBalanceCollection ItemKitchenBalances
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemKitchen" + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((ItemBalanceCollection)(obj));
                    }
                }

                var coll = new ItemBalanceCollection();

                var query = new ItemBalanceQuery("a");
                var qrItemKitchen = new ItemKitchenQuery("b");
                var qrRef = new AppStandardReferenceItemQuery("c");
                var qrItem = new ItemQuery("d");
                var qrItemBin = new AppStandardReferenceItemQuery("e");

                query.InnerJoin(qrItemKitchen).On(query.ItemID == qrItemKitchen.ItemID);
                query.LeftJoin(qrRef).On(qrItemKitchen.SRItemUnit == qrRef.ItemID & qrRef.StandardReferenceID == "ItemUnit");
                query.InnerJoin(qrItem).On(query.ItemID == qrItem.ItemID);
                query.LeftJoin(qrItemBin).On(query.SRItemBin == qrItemBin.ItemID &
                                             qrItemBin.StandardReferenceID == "ItemBin");

                query.Where(query.LocationID == txtLocationID.Text);

                query.Select(query, qrItem.ItemName.As("refToItem_ItemName"), qrRef.ItemName.As("refToSRI_ItemUnit"),
                             qrItemBin.ItemName.As("refToSRI_ItemBin"));
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collItemKitchen" + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collItemKitchen" + hdnPageId.Value] = value; }
        }

        private ItemBalanceDetailCollection ItemKitchenBalanceDetails
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemKitchenDetail" + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((ItemBalanceDetailCollection)(obj));
                    }
                }

                var coll = new ItemBalanceDetailCollection();
                var query = new ItemBalanceDetailQuery("a");
                query.Where(query.LocationID == txtLocationID.Text);
                query.Select(query);
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collItemKitchenDetail" + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collItemKitchenDetail" + hdnPageId.Value] = value; }
        }

        private void PopulateItemKitchenBalanceGrid()
        {
            //Display Data Detail
            ItemKitchenBalances = null; //Reset Record Detail
            grdItemKitchen.DataSource = ItemKitchenBalances; //Requery
            grdItemKitchen.MasterTableView.IsItemInserted = false;
            grdItemKitchen.MasterTableView.ClearEditItems();
            grdItemKitchen.DataBind();
        }

        protected void grdItemKitchen_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (txtFilterItemKitchen.Text.Trim() != string.Empty)
            {
                var ds = from d in ItemKitchenBalances
                    where d.ItemName.ToLower().Contains(txtFilterItemKitchen.Text.ToLower()) || d.ItemID.ToLower().Contains(txtFilterItemKitchen.Text.ToLower())
                    select d;
                grdItemKitchen.DataSource = ds;
            }
            else
            {
                grdItemKitchen.DataSource = ItemKitchenBalances;
            }
        }

        protected void grdItemKitchen_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemBalanceMetadata.ColumnNames.ItemID]);
            ItemBalance entity = FindItemKitchenBalance(itemID);

            if (entity != null)
                SetEntityValueItemKitchen(entity, e);

            //ItemBalanceDetail enDetail = FindItemKitchenBalanceDetail(itemID);
            //if (enDetail != null)
            //    SetEntityValueItemKitchenDetail(enDetail, e);
        }

        protected void grdItemKitchen_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemBalanceMetadata.ColumnNames.ItemID]);
            ItemBalance entity = FindItemKitchenBalance(itemID);
            if (entity != null)
            {
                // delete is forbidden if balance > 0!!
                if (entity.Balance != 0)
                {
                    Helper.ShowMessageAfterPostback(this, "Item can not be deleted, balance is not empty");
                    e.Canceled = true;
                    return;
                }
                entity.MarkAsDeleted();
            }

            ItemBalanceDetail enDetail = FindItemKitchenBalanceDetail(itemID);
            if (enDetail != null)
                enDetail.MarkAsDeleted();
        }

        protected void grdItemKitchen_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemBalance entity = ItemKitchenBalances.AddNew();
            ItemBalanceDetail enDetail = ItemKitchenBalanceDetails.AddNew();
            SetEntityValueItemKitchen(entity, e);
            SetEntityValueItemKitchenDetail(enDetail, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemKitchen.Rebind();
        }

        private ItemBalance FindItemKitchenBalance(String itemID)
        {
            ItemBalanceCollection coll = ItemKitchenBalances;
            ItemBalance retEntity = null;
            foreach (ItemBalance rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private ItemBalanceDetail FindItemKitchenBalanceDetail(String itemID)
        {
            ItemBalanceDetailCollection coll = ItemKitchenBalanceDetails;
            ItemBalanceDetail retEntity = null;
            foreach (ItemBalanceDetail rec in coll)
            {
                if (rec.ItemID.Equals(itemID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValueItemKitchen(ItemBalance entity, GridCommandEventArgs e)
        {
            var userControl = (ItemKitchenBalanceDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.SRItemUnitName = userControl.SRItemUnitName;
                entity.Minimum = userControl.Minimum;
                entity.Maximum = userControl.Maximum;
                entity.SRItemBin = userControl.SRItemBin;
                entity.SRItemBinName = userControl.SRItemBinName;
                entity.ItemSubBin = userControl.ItemSubBin;
            }
        }

        private void SetEntityValueItemKitchenDetail(ItemBalanceDetail entity, GridCommandEventArgs e)
        {
            var userControl = (ItemKitchenBalanceDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ReferenceNo = string.Empty;
                entity.TransactionCode = string.Empty;
                entity.BalanceDate = DateTime.Now;
                entity.Balance = 0;
                entity.Booking = 0;
                entity.Price = 0;
            }
        }

        #endregion

        #region Record Detail Method Function Item Bin
        private void RefreshCommandItemBin(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemBin.Columns[0].Visible = isVisible;
            grdItemBin.Columns[grdItemBin.Columns.Count - 1].Visible = isVisible;

            grdItemBin.MasterTableView.CommandItemDisplay = isVisible
                                                                ? GridCommandItemDisplay.Top
                                                                : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItemBin.Rebind();
        }

        private AppStandardReferenceItemCollection ItemBins
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collAppStandardReferenceItem_ItemBin" + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((AppStandardReferenceItemCollection)(obj));
                    }
                }

                var coll = new AppStandardReferenceItemCollection();

                var query = new AppStandardReferenceItemQuery("a");
                query.Where(query.StandardReferenceID == AppEnum.StandardReference.ItemBin, query.ReferenceID == txtLocationID.Text);
                query.Select(query);
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collAppStandardReferenceItem_ItemBin" + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collAppStandardReferenceItem_ItemBin" + hdnPageId.Value] = value; }
        }

        private void PopulateItemBinGrid()
        {
            //Display Data Detail
            ItemBins = null; //Reset Record Detail
            grdItemBin.DataSource = ItemBins; //Requery
            grdItemBin.MasterTableView.IsItemInserted = false;
            grdItemBin.MasterTableView.ClearEditItems();
            grdItemBin.DataBind();
        }

        protected void grdItemBin_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemBin.DataSource = ItemBins;
        }

        protected void grdItemBin_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][AppStandardReferenceItemMetadata.ColumnNames.ItemID]);
            AppStandardReferenceItem entity = FindItemBin(itemId);

            if (entity != null)
                SetEntityValueItemBin(entity, e);
        }

        protected void grdItemBin_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][AppStandardReferenceItemMetadata.ColumnNames.ItemID]);
            AppStandardReferenceItem entity = FindItemBin(itemId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItemBin_InsertCommand(object source, GridCommandEventArgs e)
        {
            AppStandardReferenceItem entity = ItemBins.AddNew();
            SetEntityValueItemBin(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemBin.Rebind();
        }

        private AppStandardReferenceItem FindItemBin(String itemId)
        {
            AppStandardReferenceItemCollection coll = ItemBins;
            AppStandardReferenceItem retEntity = null;
            foreach (AppStandardReferenceItem rec in coll)
            {
                if (rec.ItemID.Equals(itemId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValueItemBin(AppStandardReferenceItem entity, GridCommandEventArgs e)
        {
            var userControl = (ItemBinDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.StandardReferenceID = AppEnum.StandardReference.ItemBin.ToString();
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.ReferenceID = userControl.ReferenceID;
            }
        }

        #endregion

        #region Record Detail Method Function Location Exception
        private void RefreshCommandLocationException(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdLocationException.Columns[grdLocationException.Columns.Count - 1].Visible = isVisible;

            grdLocationException.MasterTableView.CommandItemDisplay = isVisible
                                                                ? GridCommandItemDisplay.Top
                                                                : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdLocationException.Rebind();
        }

        private LocationExceptionCollection LocationExceptions
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLocationException" + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((LocationExceptionCollection)(obj));
                    }
                }

                var coll = new LocationExceptionCollection();

                var query = new LocationExceptionQuery("a");
                var loc = new LocationQuery("b");
                query.InnerJoin(loc).On(loc.LocationID == query.LocationExceptionID);
                query.Where(query.LocationID == txtLocationID.Text);
                query.Select(query, loc.LocationName.As("refToLocation_LocationExceptionName"));
                query.OrderBy(query.LocationExceptionID.Ascending);
                coll.Load(query);

                Session["collLocationException" + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collLocationException" + hdnPageId.Value] = value; }
        }

        private void PopulateLocationExceptionGrid()
        {
            //Display Data Detail
            LocationExceptions = null; //Reset Record Detail
            grdLocationException.DataSource = LocationExceptions; //Requery
            grdLocationException.MasterTableView.IsItemInserted = false;
            grdLocationException.MasterTableView.ClearEditItems();
            grdLocationException.DataBind();
        }

        protected void grdLocationException_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdLocationException.DataSource = LocationExceptions;
        }

        protected void grdLocationException_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String locId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LocationExceptionMetadata.ColumnNames.LocationExceptionID]);
            LocationException entity = FindLocationException(locId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdLocationException_InsertCommand(object source, GridCommandEventArgs e)
        {
            LocationException entity = LocationExceptions.AddNew();
            SetEntityValueLocationException(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdLocationException.Rebind();
        }

        private LocationException FindLocationException(String locId)
        {
            LocationExceptionCollection coll = LocationExceptions;
            LocationException retEntity = null;
            foreach (LocationException rec in coll)
            {
                if (rec.LocationExceptionID.Equals(locId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValueLocationException(LocationException entity, GridCommandEventArgs e)
        {
            var userControl = (LocationExceptionDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.LocationExceptionID = userControl.LocationExceptionID;
                entity.LocationExceptionName = userControl.LocationExceptionName;
            }
        }

        #endregion

        #region Record Detail Method Function Location Exception Dist. Confirmed
        private void RefreshCommandLocationExceptionDistributionConfirm(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdLocationExceptionDistConfirm.Columns[grdLocationExceptionDistConfirm.Columns.Count - 1].Visible = isVisible;

            grdLocationExceptionDistConfirm.MasterTableView.CommandItemDisplay = isVisible
                                                                ? GridCommandItemDisplay.Top
                                                                : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdLocationExceptionDistConfirm.Rebind();
        }

        private LocationExceptionDistributionConfirmCollection LocationExceptionDistributionConfirms
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLocationExceptionDistributionConfirm" + hdnPageId.Value];
                    if (obj != null)
                    {
                        return ((LocationExceptionDistributionConfirmCollection)(obj));
                    }
                }

                var coll = new LocationExceptionDistributionConfirmCollection();

                var query = new LocationExceptionDistributionConfirmQuery("a");
                var loc = new LocationQuery("b");
                query.InnerJoin(loc).On(loc.LocationID == query.LocationExceptionID);
                query.Where(query.LocationID == txtLocationID.Text);
                query.Select(query, loc.LocationName.As("refToLocation_LocationExceptionName"));
                query.OrderBy(query.LocationExceptionID.Ascending);
                coll.Load(query);

                Session["collLocationExceptionDistributionConfirm" + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collLocationExceptionDistributionConfirm" + hdnPageId.Value] = value; }
        }

        private void PopulateLocationExceptionDistributionConfirmGrid()
        {
            //Display Data Detail
            LocationExceptionDistributionConfirms = null; //Reset Record Detail
            grdLocationException.DataSource = LocationExceptionDistributionConfirms; //Requery
            grdLocationException.MasterTableView.IsItemInserted = false;
            grdLocationException.MasterTableView.ClearEditItems();
            grdLocationException.DataBind();
        }

        protected void grdLocationExceptionDistConfirm_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdLocationExceptionDistConfirm.DataSource = LocationExceptionDistributionConfirms;
        }

        protected void grdLocationExceptionDistConfirm_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String locId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LocationExceptionDistributionConfirmMetadata.ColumnNames.LocationExceptionID]);
            LocationExceptionDistributionConfirm entity = FindLocationExceptionDistributionConfirm(locId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdLocationExceptionDistConfirm_InsertCommand(object source, GridCommandEventArgs e)
        {
            LocationExceptionDistributionConfirm entity = LocationExceptionDistributionConfirms.AddNew();
            SetEntityValueLocationExceptionDistributionConfirm(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdLocationExceptionDistConfirm.Rebind();
        }

        private LocationExceptionDistributionConfirm FindLocationExceptionDistributionConfirm(String locId)
        {
            LocationExceptionDistributionConfirmCollection coll = LocationExceptionDistributionConfirms;
            LocationExceptionDistributionConfirm retEntity = null;
            foreach (LocationExceptionDistributionConfirm rec in coll)
            {
                if (rec.LocationExceptionID.Equals(locId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValueLocationExceptionDistributionConfirm(LocationExceptionDistributionConfirm entity, GridCommandEventArgs e)
        {
            var userControl = (LocationExceptionDistConfirmDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.LocationExceptionID = userControl.LocationExceptionID;
                entity.LocationExceptionName = userControl.LocationExceptionName;
            }
        }

        #endregion

        #region ComboBox
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
            cboChartOfAccountIdCOGS.DataSource = dtb;
            cboChartOfAccountIdCOGS.DataBind();
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
            cboSubledgerIdCOGS.DataSource = dtb;
            cboSubledgerIdCOGS.DataBind();
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

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            switch (mpgDetail.SelectedIndex)
            {
                case 0:
                    {
                        grdItemMedic.CurrentPageIndex = 0;
                        grdItemMedic.Rebind();
                        break;
                    }
                case 1:
                    {
                        grdItemNonMedic.CurrentPageIndex = 0;
                        grdItemNonMedic.Rebind();
                        break;
                    }
                case 2:
                    {
                        grdItemKitchen.CurrentPageIndex = 0;
                        grdItemKitchen.Rebind();
                        break;
                    }
            }
        }
    }
}
