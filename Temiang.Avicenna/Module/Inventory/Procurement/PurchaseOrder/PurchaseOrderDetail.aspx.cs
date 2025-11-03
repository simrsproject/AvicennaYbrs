using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Procurement
{
    public partial class PurchaseOrderDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New)
                return;

            if (cboFromServiceUnitID.SelectedValue == string.Empty)
                return;

            var serv = new ServiceUnit();
            if (serv.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue))
            {
                _autoNumber = Helper.GetNewAutoNumber(
                    txtTransactionDate.SelectedDate.Value.Date,
                    BusinessObject.Reference.TransactionCode.PurchaseOrder,
                    serv.DepartmentID
                    );
                txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
            }
        }

        private void PopulateGridDetailFromReferenceNo()
        {
            var itemTransaction = new ItemTransaction();
            itemTransaction.LoadByPrimaryKey(txtReferenceNo.Text);
            string srItemType = itemTransaction.SRItemType;

            var query = new ItemTransactionItemQuery("a");

            var iq = new ItemQuery("b");
            query.LeftJoin(iq).On(query.ItemID == iq.ItemID);

            var itemDetil = new ItemProductMedicQuery("c");
            var itemDetil2 = new ItemProductNonMedicQuery("d");
            var kitchen = new ItemKitchenQuery("e");

            if (srItemType == ItemType.Medical)
            {
                query.LeftJoin(itemDetil).On(query.ItemID == itemDetil.ItemID);

                query.Select(
                        itemDetil.PriceInPurchaseUnit.Coalesce(query.Price).As("Price"),
                        itemDetil.PriceInPurchaseUnit.Coalesce(query.Price).As("PriceInCurrency"),
                        itemDetil.PurchaseDiscount1.Coalesce("'0'"),
                        itemDetil.PurchaseDiscount2.Coalesce("'0'")
                    );
            }
            else if (srItemType == ItemType.NonMedical)
            {
                query.LeftJoin(itemDetil2).On(query.ItemID == itemDetil2.ItemID);

                query.Select(
                        itemDetil2.PriceInPurchaseUnit.Coalesce(query.Price).As("Price"),
                        itemDetil2.PriceInPurchaseUnit.Coalesce(query.Price).As("PriceInCurrency"),
                        itemDetil2.PurchaseDiscount1.Coalesce("'0'"),
                        itemDetil2.PurchaseDiscount2.Coalesce("'0'")
                    );
            }
            else if (srItemType == ItemType.Kitchen)
            {
                query.LeftJoin(kitchen).On(query.ItemID == kitchen.ItemID);

                query.Select(
                        kitchen.PriceInPurchaseUnit.Coalesce(query.Price).As("Price"),
                        kitchen.PriceInPurchaseUnit.Coalesce(query.Price).As("PriceInCurrency"),
                        kitchen.PurchaseDiscount1.Coalesce("'0'"),
                        kitchen.PurchaseDiscount2.Coalesce("'0'")
                    );
            }
            else
            {
                query.Select(
    "<'0' as Price>",
    "<'0' as PriceInCurrency>",
    "<'0' as PurchaseDiscount1>",
    "<'0' as PurchaseDiscount2>");
            }


            query.Where(
                query.TransactionNo == txtReferenceNo.Text,
                query.IsClosed == false
                );

            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsUsingApprovalPurchaseRequest))
                query.Where(query.RequestQty.IsNotNull(), query.Quantity > 0);

            query.OrderBy(query.ItemID.Ascending);

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    iq.ItemName,
                    query.SRItemUnit,
                    query.Quantity,
                    @"<CASE WHEN a.ConversionFactor = 0 THEN a.QuantityFinishInBaseUnit ELSE a.QuantityFinishInBaseUnit / a.ConversionFactor END AS QtyFinish>",
                    @"<CASE WHEN a.ConversionFactor = 0 THEN a.Quantity - a.QuantityFinishInBaseUnit ELSE (a.Quantity - (a.QuantityFinishInBaseUnit / a.ConversionFactor)) END AS QtyInput>",
                    @"<CASE WHEN a.ItemID = '' THEN a.Description ELSE b.ItemName END AS Description>",
                    string.Format("<'{0}' as SRItemType>", srItemType),
                    query.ConversionFactor,
                    @"<(a.Quantity*a.ConversionFactor) AS QtyRequest>",
                    query.Specification,
                    @"<CASE WHEN b.SRItemType = '11' THEN 'Drug Dist. License No : ' WHEN b.SRItemType = '21' THEN 'Specification : '+ ISNULL(a.Specification, '') ELSE '' END AS 'AdditionalInfo'>"
                );
            var dtb = query.LoadDataTable();

            if (AppSession.Parameter.IsPrOutstandingListBasedOnCalcQtyOrder)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    var iti = new ItemTransactionItemQuery("a");
                    var it = new ItemTransactionQuery("b");
                    iti.InnerJoin(it).On(it.TransactionNo == iti.TransactionNo &&
                                         it.TransactionCode == TransactionCode.PurchaseOrder && it.IsVoid == false);
                    iti.Select(iti.ReferenceNo, iti.ReferenceSequenceNo, (iti.Quantity * iti.ConversionFactor).Sum().As("QtyFinished"));
                    iti.Where(iti.ReferenceNo == row["TransactionNo"].ToString(), iti.ReferenceSequenceNo == row["SequenceNo"].ToString());
                    iti.GroupBy(iti.ReferenceNo, iti.ReferenceSequenceNo);
                    DataTable dtbd = iti.LoadDataTable();
                    if (dtbd.Rows.Count > 0)
                    {
                        if (Convert.ToDouble(row["QtyRequest"]) <= Convert.ToDouble(dtbd.Rows[0]["QtyFinished"]))
                            row.Delete();
                        else
                        {
                            row["QtyInput"] = (Convert.ToDouble(row["QtyRequest"]) -
                                               Convert.ToDouble(dtbd.Rows[0]["QtyFinished"])) /
                                              Convert.ToDouble(row["ConversionFactor"]);
                        }
                    }
                }
                dtb.AcceptChanges();
            }

            Session["ROItemSelected" + Request.UserHostName] = dtb;

            PopulateFromSelectedRequestOrder();
        }

        private void PopulateFromSelectedRequestOrder()
        {
            object obj = Session["ROItemSelected" + Request.UserHostName];
            if (obj == null)
                return;

            //delete previouse item
            if (ItemTransactionItems.Count > 0)
                ItemTransactionItems.MarkAllAsDeleted();

            DataTable dtbSelectedItem = (DataTable)obj;
            if (dtbSelectedItem.Rows.Count > 0)
            {
                txtReferenceNo.Text = dtbSelectedItem.Rows[0]["TransactionNo"].ToString();
                var tr = new ItemTransaction();
                tr.LoadByPrimaryKey(txtReferenceNo.Text);
                cboSRItemType.SelectedValue = tr.SRItemType;
                cboSRItemCategory.SelectedValue = tr.SRItemCategory;
                chkIsNonMasterOrder.Checked = tr.IsNonMasterOrder ?? false;
                if (tr.ProductAccountID.Trim().Equals(string.Empty))
                    chkIsNonMasterOrder_CheckedChanged(chkIsNonMasterOrder, (new EventArgs()));
                else
                    cboSRProductAccountID.SelectedValue = tr.ProductAccountID;
                if (!string.IsNullOrEmpty(tr.BusinessPartnerID))
                {
                    cboBusinessPartnerID.Items.Clear();
                    var sq = new SupplierQuery();
                    sq.Where(sq.SupplierID == tr.BusinessPartnerID);
                    cboBusinessPartnerID.DataSource = sq.LoadDataTable();
                    cboBusinessPartnerID.DataBind();
                    cboBusinessPartnerID.SelectedValue = tr.BusinessPartnerID;

                    //GetContractNo();
                    GetTax(tr.BusinessPartnerID);
                    CalculateTax();
                }
                chkIsInventoryItem.Checked = tr.IsInventoryItem ?? false;
                chkIsInventoryItem.Enabled = false;
                chkIsNonMasterOrder.Enabled = false;
                chkIsAssets.Checked = tr.IsAssets ?? false;
                chkIsConsignment.Checked = tr.IsConsignment ?? false;
                chkIsConsignment.Enabled = false;
                chkIsConsignmentAlreadyReceived.Checked = tr.IsConsignmentAlreadyReceived ?? false;
                if (chkIsConsignmentAlreadyReceived.Checked)
                    cboBusinessPartnerID.Enabled = false;
                cboCategorization.SelectedValue = tr.SRPurchaseCategorization;
                cboSRProcurementType.SelectedValue = tr.SRProcurementType;
                //if (AppSession.Application.IsModuleAssetActive)
                //    chkIsAssets.Enabled = false;

                if (Request.QueryString["cons"] == "1")
                {
                    chkIsAssets.Enabled = false;
                }
            }

            int i = 0;
            foreach (DataRow row in dtbSelectedItem.Rows)
            {
                if (Convert.ToDecimal(row["QtyInput"]) <= 0)
                    continue;

                i++;
                string seqNo = string.Format("{0:000}", i);

                var entity = ItemTransactionItems.AddNew();
                entity.ItemID = row["ItemID"].ToString();
                entity.SequenceNo = seqNo;
                entity.ReferenceNo = row["TransactionNo"].ToString();
                entity.ReferenceSequenceNo = row["SequenceNo"].ToString();
                entity.Description = row["Description"].ToString();
                entity.Quantity = Convert.ToDecimal(row["QtyInput"]);
                entity.ConversionFactor = Convert.ToDecimal(row["ConversionFactor"]);
                entity.Discount1Percentage = row["PurchaseDiscount1"] != null ? Convert.ToDecimal(row["PurchaseDiscount1"]) : 0;
                entity.Discount2Percentage = row["PurchaseDiscount2"] != null ? Convert.ToDecimal(row["PurchaseDiscount2"]) : 0;
                entity.IsBonusItem = false;
                entity.IsClosed = false;
                entity.SRItemUnit = row["SRItemUnit"].ToString();
                entity.Price = row["Price"] != null ? Convert.ToDecimal(row["Price"]) : 0;
                entity.Specification = row["Specification"].ToString();

                // update harga per supplier
                if (entity.IsBonusItem == false)
                {
                    var scItem = new SupplierContractItem();
                    if (scItem.LoadByPrimaryKey(txtContractNo.Text, entity.ItemID))
                    {
                        entity.Discount1Percentage = scItem.PurchaseDiscount1 ?? 0;
                        entity.Discount2Percentage = scItem.PurchaseDiscount2 ?? 0;
                        entity.Price = scItem.PriceInPurchaseUnit ?? 0;
                    }
                    else
                    {
                        entity.SetQtyPricePO(entity.ReferenceNo, entity.ItemID,
                            entity.ConversionFactor.ToDecimal(), cboBusinessPartnerID.SelectedValue, false);
                    }
                }
                // end of update harga per supplier

                entity.PriceInCurrency = (entity.Price) * Convert.ToDecimal(txtCurrencyRate.Value);
                entity.Discount = (entity.Price * entity.Discount1Percentage / 100) +
                                  ((entity.Price - (entity.Price * entity.Discount1Percentage / 100)) *
                                   entity.Discount2Percentage / 100);
                entity.DiscountInCurrency = entity.Discount * Convert.ToDecimal(txtCurrencyRate.Value);
                entity.IsDiscountInPercent = true;

                if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorByStockGroup) && !string.IsNullOrEmpty(txtReferenceNo.Text))
                {
                    var itref = new ItemTransaction();
                    if (itref.LoadByPrimaryKey(txtReferenceNo.Text))
                    {
                        var stockGroup = "abcd";
                        var loc = new Location();
                        loc.LoadByPrimaryKey(itref.FromLocationID);
                        if (!string.IsNullOrEmpty(loc.SRStockGroup))
                            stockGroup = loc.SRStockGroup;

                        ProcurementUtils.PopulateBalanceInfoByStockGroup(entity, stockGroup, itref.FromLocationID);

                        switch (cboSRItemType.SelectedValue)
                        {
                            case ItemType.Medical:
                                var ipm = new ItemProductMedic();
                                ipm.LoadByPrimaryKey(entity.ItemID);
                                entity.SRMasterBaseUnit = ipm.SRItemUnit;
                                break;
                            case ItemType.NonMedical:
                                var ipnm = new ItemProductNonMedic();
                                ipnm.LoadByPrimaryKey(entity.ItemID);
                                entity.SRMasterBaseUnit = ipnm.SRItemUnit;
                                break;
                            case ItemType.Kitchen:
                                var ik = new ItemKitchen();
                                ik.LoadByPrimaryKey(entity.ItemID);
                                entity.SRMasterBaseUnit = ik.SRItemUnit;
                                break;
                        }
                    }
                    else
                    {
                        entity.Balance = 0;
                        entity.BalanceSG = 0;
                        entity.Minimum = 0;
                        entity.Maximum = 0;
                        entity.SRMasterBaseUnit = string.Empty;
                        entity.BalanceTotal = 0;
                    }
                }
                else
                {
                    if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPOWithStockInfo) &&
                        !string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                    {
                        ProcurementUtils.PopulateBalanceInfoByItemType(entity, cboSRItemType.SelectedValue);
                    }
                    else
                    {
                        entity.Balance = 0;
                        entity.BalanceSG = 0;
                        entity.Minimum = 0;
                        entity.Maximum = 0;
                        entity.SRMasterBaseUnit = string.Empty;
                        entity.BalanceTotal = 0;
                    }
                }

                entity.IsTaxable = true;

                if (cboSRItemType.SelectedValue == ItemType.Medical)
                {
                    var suppItem = new SupplierItem();
                    entity.DrugDistributionLicenseNo = suppItem.LoadByPrimaryKey(cboBusinessPartnerID.SelectedValue, entity.ItemID)
                                                           ? suppItem.DrugDistributionLicenseNo
                                                           : string.Empty;
                    entity.AdditionalInfo = "Drug Dist. License No : " + entity.DrugDistributionLicenseNo;
                }
                else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                {
                    entity.DrugDistributionLicenseNo = string.Empty;
                    entity.AdditionalInfo = "Specification : " + entity.Specification;
                }
                else {
                    entity.DrugDistributionLicenseNo = string.Empty;
                    entity.AdditionalInfo = string.Empty;
                }

                if (entity.ItemID == string.Empty)
                {
                    entity.IsAsset = false;
                    entity.EconomicLifeInYear = 0;
                }
                else
                {
                    var itm = new Item();
                    if (itm.LoadByPrimaryKey(entity.ItemID))
                    {
                        entity.IsAsset = itm.IsAsset ?? false;
                        entity.EconomicLifeInYear = itm.EconomicLifeInYear;
                    }
                    else
                    {
                        entity.IsAsset = false;
                        entity.EconomicLifeInYear = 0;
                    }
                }
            }

            //#region Update Harga Per Supplier
            //foreach (var item in ItemTransactionItems)
            //{
            //    if (item.IsBonusItem == false)
            //    {
            //        var scItem = new SupplierContractItem();
            //        if (scItem.LoadByPrimaryKey(txtContractNo.Text, item.ItemID))
            //        {
            //            item.Discount1Percentage = scItem.PurchaseDiscount1 ?? 0;
            //            item.Discount2Percentage = scItem.PurchaseDiscount2 ?? 0;
            //            item.Price = scItem.PriceInPurchaseUnit ?? 0;
            //        }
            //        else
            //        {
            //            item.SetQtyPricePO(item.ReferenceNo, item.ItemID, 
            //                item.ConversionFactor.ToDecimal(), cboBusinessPartnerID.SelectedValue);
            //        }

            //        item.Discount = (item.Price * item.Discount1Percentage / 100) +
            //                            ((item.Price - (item.Price * item.Discount1Percentage / 100)) *
            //                             item.Discount2Percentage / 100);
            //    }
            //}
            //#endregion

            grdItemTransactionItem.DataSource = ItemTransactionItems;
            grdItemTransactionItem.DataBind();

            CalculateDetailTransaction(true);

            cboSRItemType.Enabled = !(ItemTransactionItems.Count > 0);
            cboFromServiceUnitID.Enabled = !(ItemTransactionItems.Count > 0);
            chkIsInventoryItem.Enabled = !(ItemTransactionItems.Count > 0);
            chkIsNonMasterOrder.Enabled = !(ItemTransactionItems.Count > 0) && !chkIsInventoryItem.Checked;
            chkIsConsignment.Enabled = !(ItemTransactionItems.Count > 0);
            cboSRItemCategory.Enabled = !(ItemTransactionItems.Count > 0);
            cboSRProductAccountID.Enabled = chkIsNonMasterOrder.Checked;
            //if (AppSession.Application.IsModuleAssetActive)
            //    chkIsAssets.Enabled= !(ItemTransactionItems.Count > 0);

            //Remove session
            Session.Remove("ROItemSelected" + Request.UserHostName);
        }

        protected void btnResetPR_Click(object sender, EventArgs e)
        {
            //Reset PR
            if (txtReferenceNo.Text != string.Empty)
            {
                txtReferenceNo.Text = string.Empty;
                cboSRItemType.Enabled = true;
                cboSRItemCategory.Enabled = true;
                cboFromServiceUnitID.Enabled = true;
                if (ItemTransactionItems.Count > 0)
                    ItemTransactionItems.MarkAllAsDeleted();
                grdItemTransactionItem.DataSource = ItemTransactionItems;
                grdItemTransactionItem.DataBind();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "##";

            if (string.IsNullOrEmpty(Request.QueryString["rop"]))
            {
                UrlPageList = "PurchaseOrderList.aspx?cons=" + Request.QueryString["cons"];
                if (!string.IsNullOrEmpty(Request.QueryString["suptype"]))
                    UrlPageList = UrlPageList + "&suptype=" + Request.QueryString["suptype"];
            }
            else if (Request.QueryString["rop"] == "0")
            {
                UrlPageList = "PurchaseOrderList.aspx?cons=" + Request.QueryString["cons"];
                if (!string.IsNullOrEmpty(Request.QueryString["suptype"]))
                    UrlPageList = UrlPageList + "&suptype=" + Request.QueryString["suptype"];
            }
            else if (Request.QueryString["rop"] == "1")
                UrlPageList = "../ReOrderPurchaseOrder/ReOrderPurchaseOrderList.aspx?su=" + Request.QueryString["su"] +
                              "&it=" + Request.QueryString["it"] + "&cons=0";
            else
                UrlPageList = "../ReOrderPoBasedOnPr/ReOrderPoBasedOnPrList.aspx?su=" + Request.QueryString["su"] +
                              "&it=" + Request.QueryString["it"] + "&cons=0";


            ProgramID = Request.QueryString["cons"] == "0"
                            ? AppConstant.Program.PurchaseOrder
                            : AppConstant.Program.PurchaseOrderConsignment;
            if (!string.IsNullOrEmpty(Request.QueryString["suptype"]))
            {
                ProgramID = AppConstant.Program.PurchaseOrderFilteredBySupplierType;
            }

            boxApprovalProgress.Visible = AppSession.Parameter.IsUseApprovalLevel && Request.QueryString["cons"] == "0";
            trPaymentType.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSMM";
            rfvSRPaymentType.Visible = trPaymentType.Visible;
            trContractNo.Visible = false;
            trSRProcurementType.Visible = AppSession.Parameter.IsUsingProcurementTypeInPO;
            rfvCategorization.Visible = cboCategorization.Visible;
            rfvSRProcurementType.Visible = trSRProcurementType.Visible;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                var supp = new SupplierCollection();
                supp.Query.Where(supp.Query.IsActive == true);
                if (!string.IsNullOrEmpty(Request.QueryString["suptype"]))
                    supp.Query.Where(supp.Query.SRSupplierType == Request.QueryString["suptype"]);

                supp.LoadAll();
                cboBusinessPartnerID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var entity in supp)
                {
                    cboBusinessPartnerID.Items.Add(new RadComboBoxItem(entity.SupplierName, entity.SupplierID));
                }

                var curr = new CurrencyRateCollection();
                curr.Query.Where(curr.Query.IsActive == true);
                curr.LoadAll();
                cboCurrencyType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var entity in curr)
                {
                    cboCurrencyType.Items.Add(new RadComboBoxItem(entity.CurrencyName, entity.CurrencyID));
                }

                StandardReference.InitializeIncludeSpace(cboSRDownPaymentType, AppEnum.StandardReference.DownPaymentType);
                StandardReference.InitializeIncludeSpace(cboSRPaymentType, AppEnum.StandardReference.PaymentTypePO);
                StandardReference.InitializeIncludeSpace(cboTermID, AppEnum.StandardReference.Term);
                StandardReference.InitializeIncludeSpace(cboSRPurchaseOrderType, AppEnum.StandardReference.PurchaseOrderType);
                StandardReference.InitializeIncludeSpace(cboCategorization, AppEnum.StandardReference.PurchaseCategorization);
                StandardReference.InitializeIncludeSpace(cboSRProcurementType, AppEnum.StandardReference.ProcurementType);

                var productAcc = new ProductAccountCollection();
                productAcc.Query.Where(productAcc.Query.IsActive == true);
                productAcc.LoadAll();

                cboSRProductAccountID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var c in productAcc)
                {
                    cboSRProductAccountID.Items.Add(new RadComboBoxItem(c.ProductAccountName, c.ProductAccountID));
                }

                //  Reset Session
                ItemTransactionItems = null;

                if (AppSession.Parameter.IsAllowEditPoDate)
                {
                    txtTransactionDate.DateInput.ReadOnly = false;
                    txtTransactionDate.DatePopupButton.Enabled = true;
                }

                // Hide column balance info
                ProcurementUtils.HideColumnStockInfo(grdItemTransactionItem.MasterTableView);

                if (!AppSession.Parameter.IsPphUsesAfixedValue)
                {
                    pnlPphNonFixedValue.Visible = false;
                }
                else
                {
                    StandardReference.InitializeIncludeSpace(cboSRPph, AppEnum.StandardReference.Pph);
                }
                trSRItemCategory.Visible = AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory;
                grdItemTransactionItem.Columns.FindByUniqueName("FabricName").Visible = AppSession.Parameter.IsUsingFactoryInTheItemProcurementProcess;
            }

            //Add Event for Request Order Selection
            AjaxManager.AjaxRequest += AjaxManager_AjaxRequest;
        }

        private void AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            PopulateFromSelectedRequestOrder();
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;

            if (!string.IsNullOrEmpty(Request.QueryString["pr"]))
            {
                ToolBarMenuAdd.Enabled = false;
            }

            // ToolBarMenuApproval
            if (AppSession.Parameter.IsUseApprovalLevel && Request.QueryString["cons"] == "0")
            {
                ViewState["apprLevel"] = 0;
                ViewState["apprCount"] = 0;
                var dtbApproval = (DataTable)Session["ds_grdApproval"];
                ViewState["apprCount"] = dtbApproval.Rows.Count;
                if (ViewState["apprCount"].ToInt() > 0)
                {
                    var disableToolbarApproval = true;
                    foreach (DataRow row in dtbApproval.Rows)
                    {
                        if (true.Equals(row["IsApproveAble"]))
                        {
                            ViewState["apprLevel"] = row["ApprovalLevel"];
                            disableToolbarApproval = false;
                            break;
                        }
                    }
                    if (disableToolbarApproval)
                    {
                        ToolBarMenuApproval.Enabled = false;
                        ToolBarMenuUnApproval.Enabled = false;
                    }
                }
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItemTransactionItem, grdItemTransactionItem);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboSRItemType);
            if (AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory)
                ajax.AddAjaxSetting(grdItemTransactionItem, cboSRItemCategory);
            ajax.AddAjaxSetting(grdItemTransactionItem, txtTransactionAmount);
            ajax.AddAjaxSetting(grdItemTransactionItem, txtChargesAmount);
            ajax.AddAjaxSetting(grdItemTransactionItem, txtAmountTaxed);
            ajax.AddAjaxSetting(grdItemTransactionItem, txtTaxAmount);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboCurrencyType);
            ajax.AddAjaxSetting(grdItemTransactionItem, txtCurrencyRate);
            ajax.AddAjaxSetting(grdItemTransactionItem, txtTotal);
            ajax.AddAjaxSetting(grdItemTransactionItem, chkIsInventoryItem);
            ajax.AddAjaxSetting(grdItemTransactionItem, chkIsNonMasterOrder);
            ajax.AddAjaxSetting(grdItemTransactionItem, chkIsConsignment);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboSRProductAccountID);
            //if (AppSession.Application.IsModuleAssetActive)
            //    ajax.AddAjaxSetting(grdItemTransactionItem, chkIsAssets);
            //ajax.AddAjaxSetting(grdItemTransactionItem, txtPph22);
            //ajax.AddAjaxSetting(grdItemTransactionItem, txtPph23);

            ajax.AddAjaxSetting(cboCurrencyType, txtCurrencyRate);
            ajax.AddAjaxSetting(cboCurrencyType, grdItemTransactionItem);
            ajax.AddAjaxSetting(txtCurrencyRate, grdItemTransactionItem);

            ajax.AddAjaxSetting(chkIsInventoryItem, chkIsNonMasterOrder);
            ajax.AddAjaxSetting(chkIsInventoryItem, chkIsAssets);
            ajax.AddAjaxSetting(chkIsInventoryItem, chkIsConsignment);
            ajax.AddAjaxSetting(chkIsInventoryItem, chkIsConsignmentAlreadyReceived);

            ajax.AddAjaxSetting(chkIsNonMasterOrder, cboSRProductAccountID);
            ajax.AddAjaxSetting(chkIsNonMasterOrder, chkIsAssets);
            ajax.AddAjaxSetting(chkIsNonMasterOrder, chkIsConsignment);
            ajax.AddAjaxSetting(chkIsNonMasterOrder, chkIsConsignmentAlreadyReceived);

            ajax.AddAjaxSetting(chkIsAssets, chkIsConsignment);
            ajax.AddAjaxSetting(chkIsConsignment, chkIsAssets);
            ajax.AddAjaxSetting(chkIsAssets, chkIsConsignmentAlreadyReceived);

            ajax.AddAjaxSetting(chkIsTaxable, txtTaxPercentage);
            ajax.AddAjaxSetting(chkIsTaxable, txtTaxAmount);
            ajax.AddAjaxSetting(chkIsTaxable, txtTotal);
            //ajax.AddAjaxSetting(chkIsTaxable, txtPph22);
            //ajax.AddAjaxSetting(chkIsTaxable, txtPph23);

            ajax.AddAjaxSetting(rblTypesOfTaxes, txtTaxPercentage);
            ajax.AddAjaxSetting(rblTypesOfTaxes, txtTaxAmount);
            ajax.AddAjaxSetting(rblTypesOfTaxes, txtTotal);
            //ajax.AddAjaxSetting(rblTypesOfTaxes, txtPph22);
            //ajax.AddAjaxSetting(rblTypesOfTaxes, txtPph23);

            ajax.AddAjaxSetting(cboBusinessPartnerID, cboBusinessPartnerID);
            ajax.AddAjaxSetting(cboBusinessPartnerID, chkIsTaxable);
            ajax.AddAjaxSetting(cboBusinessPartnerID, rblTypesOfTaxes);
            ajax.AddAjaxSetting(cboBusinessPartnerID, txtTaxPercentage);
            ajax.AddAjaxSetting(cboBusinessPartnerID, txtTermOfPayment);
            ajax.AddAjaxSetting(cboBusinessPartnerID, txtTaxAmount);
            ajax.AddAjaxSetting(cboBusinessPartnerID, txtTotal);
            ajax.AddAjaxSetting(cboBusinessPartnerID, grdItemTransactionItem);
            //ajax.AddAjaxSetting(cboBusinessPartnerID, txtPph22);
            //ajax.AddAjaxSetting(cboBusinessPartnerID, txtPph23);
            ajax.AddAjaxSetting(cboBusinessPartnerID, txtPBFLicenseNo);
            ajax.AddAjaxSetting(cboBusinessPartnerID, txtTransactionAmount);
            ajax.AddAjaxSetting(cboBusinessPartnerID, txtChargesAmount);
            ajax.AddAjaxSetting(cboBusinessPartnerID, txtAmountTaxed);

            ajax.AddAjaxSetting(txtDownPaymentAmount, txtDownPaymentAmount);
            ajax.AddAjaxSetting(txtDownPaymentAmount, txtTotal);
            //ajax.AddAjaxSetting(txtDownPaymentAmount, txtPph22);
            //ajax.AddAjaxSetting(txtDownPaymentAmount, txtPph23);

            ajax.AddAjaxSetting(txtAdvanceAmount, txtAdvanceAmount);
            ajax.AddAjaxSetting(txtAdvanceAmount, txtTotal);
            //ajax.AddAjaxSetting(txtAdvanceAmount, txtPph22);
            //ajax.AddAjaxSetting(txtAdvanceAmount, txtPph23);

            //Request Order Selection
            ajax.AddAjaxSetting(AjaxManager, chkIsNonMasterOrder);
            ajax.AddAjaxSetting(AjaxManager, chkIsInventoryItem);
            ajax.AddAjaxSetting(AjaxManager, chkIsAssets);
            ajax.AddAjaxSetting(AjaxManager, chkIsConsignment);
            ajax.AddAjaxSetting(AjaxManager, chkIsConsignmentAlreadyReceived);
            ajax.AddAjaxSetting(AjaxManager, txtReferenceNo);
            ajax.AddAjaxSetting(AjaxManager, cboSRItemType);
            if (AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory)
                ajax.AddAjaxSetting(AjaxManager, cboSRItemCategory);
            ajax.AddAjaxSetting(AjaxManager, grdItemTransactionItem);
            ajax.AddAjaxSetting(AjaxManager, cboSRPurchaseOrderType);
            ajax.AddAjaxSetting(AjaxManager, cboTermID);
            ajax.AddAjaxSetting(AjaxManager, cboSRProductAccountID);
            ajax.AddAjaxSetting(AjaxManager, txtTotal);
            ajax.AddAjaxSetting(AjaxManager, txtTransactionAmount);
            ajax.AddAjaxSetting(AjaxManager, txtChargesAmount);
            ajax.AddAjaxSetting(AjaxManager, txtAmountTaxed);
            ajax.AddAjaxSetting(AjaxManager, txtTaxAmount);
            ajax.AddAjaxSetting(AjaxManager, txtTaxPercentage);
            ajax.AddAjaxSetting(AjaxManager, cboSRProductAccountID);
            //ajax.AddAjaxSetting(AjaxManager, txtPph22);
            //ajax.AddAjaxSetting(AjaxManager, txtPph23);

            ajax.AddAjaxSetting(AjaxManager, cboBusinessPartnerID);
            ajax.AddAjaxSetting(AjaxManager, txtPBFLicenseNo);
            //ajax.AddAjaxSetting(AjaxManager, txtContractNo);
            ajax.AddAjaxSetting(AjaxManager, chkIsTaxable);
            ajax.AddAjaxSetting(AjaxManager, rblTypesOfTaxes);
            ajax.AddAjaxSetting(AjaxManager, txtTermOfPayment);

            //if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH" || AppSession.Parameter.HealthcareInitialAppsVersion == "RSBHP")
            ajax.AddAjaxSetting(AjaxManager, cboCategorization);

            if (AppSession.Parameter.IsPphUsesAfixedValue)
            {
                ajax.AddAjaxSetting(grdItemTransactionItem, txtPphAmount);
                ajax.AddAjaxSetting(chkIsTaxable, txtPphAmount);
                ajax.AddAjaxSetting(rblTypesOfTaxes, txtPphAmount);
                ajax.AddAjaxSetting(cboBusinessPartnerID, txtPphAmount);
                ajax.AddAjaxSetting(txtDownPaymentAmount, txtPphAmount);
                ajax.AddAjaxSetting(txtAdvanceAmount, txtPphAmount);
                ajax.AddAjaxSetting(AjaxManager, txtPphAmount);
            }
        }

        protected override void OnMenuEditClick()
        {
            pnlInfo.Visible = false;
            cboSRItemType.Enabled = ItemTransactionItems.Count == 0;
            cboSRItemCategory.Enabled = ItemTransactionItems.Count == 0;
            if (!string.IsNullOrEmpty(txtReferenceNo.Text))
                cboFromServiceUnitID.Enabled = ItemTransactionItems.Count == 0;
            cboCurrencyType.Enabled = ItemTransactionItems.Count == 0;
            txtCurrencyRate.ReadOnly = ItemTransactionItems.Count != 0;
            if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSCH" && !chkIsInventoryItem.Checked)
                chkIsNonMasterOrder.Enabled = ItemTransactionItems.Count == 0;
            chkIsInventoryItem.Enabled = ItemTransactionItems.Count == 0;
            chkIsNonMasterOrder.Enabled = ItemTransactionItems.Count == 0 && !chkIsInventoryItem.Checked;
            chkIsConsignment.Enabled = ItemTransactionItems.Count == 0;
            cboSRProductAccountID.Enabled = chkIsNonMasterOrder.Checked;
            txtTransactionDate.Enabled = false;
            //if (AppSession.Application.IsModuleAssetActive)
            //    chkIsAssets.Enabled = ItemTransactionItems.Count == 0;

            if (Request.QueryString["cons"] == "1")
            {
                chkIsInventoryItem.Enabled = false;
                chkIsNonMasterOrder.Enabled = false;
                chkIsAssets.Enabled = false;
                chkIsConsignment.Enabled = false;
            }
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            ItemTransaction entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            var hd = new ItemTransaction();
            if (hd.LoadByPrimaryKey(txtTransactionNo.Text) && hd.IsApproved == true)
            {
                if (hd.PrintNumber == null)
                    hd.PrintNumber = 1;
                else
                    hd.PrintNumber++;
                hd.LastPrintedDateTime = (new DateTime()).NowAtSqlServer();
                hd.LastPrintedByUserID = AppSession.UserLogin.UserID;
                hd.Save();
            }

            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            if (ItemTransactionItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            if (txtTotal.Value < 0)
            {
                args.MessageText = "The total amount is less than zero. Check back your transaction.";
                args.IsCancel = true;
                return;
            }

            //if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSCH")
            //{
            //    if (!IsContractValid(args)) return;
            //}

            //if (string.IsNullOrEmpty(cboSRDownPaymentType.SelectedValue))
            //{
            //    args.MessageText = "Shipping Charges Type required.";
            //    args.IsCancel = true;
            //    return;
            //}

            if (trPaymentType.Visible & string.IsNullOrEmpty(cboSRPaymentType.SelectedValue))
            {
                args.MessageText = "Payment Type required.";
                args.IsCancel = true;
                return;
            }

            if (AppSession.Parameter.ServiceUnitPurchasingId.Contains(cboFromServiceUnitID.SelectedValue))
            {
                if (chkIsInventoryItem.Checked && !chkIsAssets.Checked)
                {
                    var assetInventoryAmountLimit = Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_AssetInventoryAmountLimit));
                    var economicLifeInYearLimit = Convert.ToInt32(AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_EconomicLifeInYearLimit));

                    var e = new ItemTransaction();
                    e.LoadByPrimaryKey(txtTransactionNo.Text);

                    var msg = string.Empty;

                    foreach (var i in ItemTransactionItems.Where(x => x.IsAsset.Equals(false) && x.EconomicLifeInYear >= economicLifeInYearLimit && ((x.PriceInCurrency - x.DiscountInCurrency) * (1 + (e.TaxPercentage.Value/100)) > assetInventoryAmountLimit)))
                    {
                        if (msg == string.Empty)
                            msg = i.Description;
                        else
                            msg += ", " + i.Description;
                    }
                    if (msg != string.Empty)
                    {
                        args.MessageText = string.Format("The price of items: {0}, exceed the asset price limit ({1}).  Please check back your item master.", msg, (String.Format("{0:N0}", assetInventoryAmountLimit)));
                        args.IsCancel = true;
                        return;
                    }
                }
            }

            //if (chkIsNonMasterOrder.Checked)
            //    (new ItemTransaction()).ApproveNonMaster(txtTransactionNo.Text, ItemTransactionItems, AppSession.UserLogin.UserID);
            //else
            //    (new ItemTransaction()).Approve(txtTransactionNo.Text, ItemTransactionItems, AppSession.UserLogin.UserID);

            if (AppSession.Parameter.IsUseApprovalLevel && ViewState["apprCount"].ToInt() > 0)
            {
                using (var trans = new esTransactionScope())
                {
                    var apprLevel = ViewState["apprLevel"].ToInt(); // Viewstate diisi saat loadcomplete
                    Util.ApprovalLevelUtil.Approve(args, TransactionCode.PurchaseOrder, txtTransactionNo.Text, apprLevel, AppSession.UserLogin.UserID);
                    if (!args.IsCancel)
                        trans.Complete();
                }
            }
            else
            {
                Procurement.PurchaseOrderDetail.ApprovePurchaseOrder(args, chkIsNonMasterOrder.Checked, txtTransactionNo.Text, AppSession.UserLogin.UserID);
            }

        }

        public static void ApprovePurchaseOrder(ValidateArgs args, bool isNonMasterOrder, string transactionNo, string userID)
        {
            // Approve
            var itemTransactionItems = new ItemTransactionItemCollection();
            var itiq = new ItemTransactionItemQuery();
            itiq.Where(itiq.TransactionNo == transactionNo);
            itemTransactionItems.Load(itiq);

            if (isNonMasterOrder)
                (new ItemTransaction()).ApproveNonMaster(transactionNo, itemTransactionItems,
                    userID);
            else
                (new ItemTransaction()).Approve(transactionNo, itemTransactionItems,
                    userID);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            pnlInfo.Visible = false;
            if (!IsProceed(args)) return;

            if (chkIsNonMasterOrder.Checked)
            {
                //var entity = new ItemTransaction();
                //entity.LoadByPrimaryKey(txtTransactionNo.Text);
                //entity.IsApproved = false;
                //entity.ApprovedDate = (new DateTime()).NowAtSqlServer();
                //entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                //entity.Save();
                (new ItemTransaction()).UnApproveNonMaster(txtTransactionNo.Text, ItemTransactionItems, AppSession.UserLogin.UserID);
            }
            else
                (new ItemTransaction()).UnApprove(txtTransactionNo.Text, ItemTransactionItems, AppSession.UserLogin.UserID);
        }
        public static void UnApprovePurchaseOrder(ValidateArgs args, bool isNonMasterOrder, string transactionNo, string userID)
        {
            if (!IsProceed(args, transactionNo)) return;

            // Approve
            var itemTransactionItems = new ItemTransactionItemCollection();
            var itiq = new ItemTransactionItemQuery();
            itiq.Where(itiq.TransactionNo == transactionNo);
            itemTransactionItems.Load(itiq);

            if (isNonMasterOrder)
                (new ItemTransaction()).UnApproveNonMaster(transactionNo, itemTransactionItems,
                    userID);
            else
                (new ItemTransaction()).UnApprove(transactionNo, itemTransactionItems, userID);
        }
        private static bool IsProceed(ValidateArgs args, string transactionNo)
        {
            var porQ = new ItemTransactionQuery();
            porQ.Where(porQ.ReferenceNo == transactionNo, porQ.IsVoid == false);
            DataTable dtb = porQ.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                args.IsCancel = true;
                args.MessageText = "This transaction can't be canceled, this data has been proceed to another process";
                return false;
            }
            return true;
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            pnlInfo.Visible = false;
            var entity = new ItemTransaction();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }
            if (AppSession.Parameter.IsUseApprovalLevel.ToString().ToLower() == "yes" && Request.QueryString["cons"] == "0")
                if (Util.ApprovalLevelUtil.IsApprovalLevelInProgress(args, txtTransactionNo.Text)) return;

            (new ItemTransaction()).Void(txtTransactionNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            (new ItemTransaction()).UnVoid(txtTransactionNo.Text, AppSession.UserLogin.UserID);
        }

        private bool IsApprovedOrVoid(ItemTransaction entity, ValidateArgs args)
        {
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }
            return true;
        }

        private bool IsProceed(ValidateArgs args)
        {
            var porQ = new ItemTransactionQuery();
            porQ.Where(porQ.ReferenceNo == txtTransactionNo.Text, porQ.IsVoid == false);
            DataTable dtb = porQ.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                args.IsCancel = true;
                args.MessageText = "This transaction can't be canceled, this data has been proceed to another process";
                return false;
            }
            return true;
        }

        private bool IsProceedApprovalLevel(ValidateArgs args)
        {
            var apps = new ApprovalTransactionCollection();
            apps.Query.Where(apps.Query.TransactionNo == txtTransactionNo.Text, apps.Query.IsApproved == true);
            apps.LoadAll();
            if (apps.Count > 0)
            {
                args.IsCancel = true;
                args.MessageText = "This transaction can't be canceled, this data has been proceed to another process";
                return false;
            }
            return true;
        }

        private bool IsContractValid(ValidateArgs args)
        {
            var sc = new SupplierContract();
            if (sc.LoadByPrimaryKey(txtContractNo.Text))
            {
                var sciq = new SupplierContractItemQuery();
                sciq.Where(sciq.TransactionNo == txtContractNo.Text, sciq.IsActive == true);
                if (sciq.LoadDataTable().Rows.Count > 0)
                {
                    var itiq = new ItemTransactionItemQuery("a");
                    sciq = new SupplierContractItemQuery("b");
                    var iq = new ItemQuery("c");
                    itiq.Select(itiq.ItemID, iq.ItemName);
                    itiq.InnerJoin(iq).On(itiq.ItemID == iq.ItemID);
                    itiq.LeftJoin(sciq).On(itiq.ItemID == sciq.ItemID && sciq.TransactionNo == txtContractNo.Text &&
                                           sciq.IsActive == true);
                    itiq.Where(sciq.ItemID.IsNull(), itiq.TransactionNo == txtTransactionNo.Text);
                    DataTable dtb = itiq.LoadDataTable();
                    if (dtb.Rows.Count > 0)
                    {
                        args.IsCancel = true;
                        args.MessageText = "This transaction can't be approved, item: " + dtb.Rows[0]["ItemName"] + " not included in the contract";
                        return false;
                    }
                }

                decimal contract, purchase, charges;
                contract = sc.ContractAmount ?? 0;
                purchase = sc.PurchaseAmount ?? 0;

                if (contract > 0)
                {
                    var it = new ItemTransaction();
                    it.LoadByPrimaryKey(txtTransactionNo.Text);
                    charges = it.ChargesAmount ?? 0 + it.TaxAmount ?? 0;

                    if (charges + purchase > contract)
                    {
                        args.IsCancel = true;
                        args.MessageText = "This transaction can't be approved, amount of purchases already exceeded the amount of the contract";
                        return false;
                    }
                }
            }

            return true;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ItemTransaction());

            ComboBox.SelectedValue(cboCurrencyType, AppParameter.GetParameterValue(AppParameter.ParameterItem.CurrencyRupiahID));

            var curr = new CurrencyRate();
            curr.LoadByPrimaryKey(cboCurrencyType.SelectedValue);
            txtCurrencyRate.Value = Convert.ToDouble(curr.CurrencyRate);
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            cboSRPurchaseOrderType.SelectedValue = AppSession.Parameter.DefaultPurchaseOrderType;
            cboSRDownPaymentType.SelectedValue = AppSession.Parameter.DefaultDownPaymentType;

            if (string.IsNullOrEmpty(Request.QueryString["pr"]))
            {
                cboBusinessPartnerID.Text = string.Empty;
                txtPBFLicenseNo.Text = string.Empty;
                cboFromServiceUnitID.Text = string.Empty;

                cboSRProductAccountID.Text = string.Empty;

                chkIsInventoryItem.Checked = true;
                chkIsNonMasterOrder.Checked = false;
                chkIsNonMasterOrder.Enabled = false;
                chkIsAssets.Checked = false;
                chkIsConsignment.Checked = false;
                chkIsConsignmentAlreadyReceived.Checked = false;
                cboSRProductAccountID.Enabled = false;
                //if (AppSession.Application.IsModuleAssetActive)
                //    chkIsAssets.Enabled = false;
            }

            if (Request.QueryString["cons"] == "1")
            {
                chkIsInventoryItem.Checked = true;
                chkIsInventoryItem.Enabled = false;
                chkIsNonMasterOrder.Checked = false;
                chkIsNonMasterOrder.Enabled = false;
                chkIsAssets.Checked = false;
                chkIsAssets.Enabled = false;
                chkIsConsignment.Checked = true;
                chkIsConsignment.Enabled = false;
            }

            PopulateNewTransactionNo();
            GetTax(cboBusinessPartnerID.SelectedValue);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (!IsSupplierExist(args))
                return;

            if (txtTransactionDate.SelectedDate.Value.Date > (new DateTime()).NowAtSqlServer().Date)
            {
                args.MessageText = "Purchase Order Date can not be greater than today's date";
                args.IsCancel = true;
                return;
            }

            if (txtTotal.Value < 0)
            {
                args.MessageText = "The total amount is less than zero. Check back your transaction.";
                args.IsCancel = true;
                return;
            }

            if (ItemTransactionItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            if (ItemTransactionItems.Any(item => item.IsBonusItem == false && item.Price == 0))
            {
                args.MessageText = "There are items not bonuses at zero price. Check back your transaction.";
                args.IsCancel = true;
                return;
            }

            if (chkIsNonMasterOrder.Checked & string.IsNullOrEmpty(cboSRProductAccountID.SelectedValue))
            {
                args.MessageText = "Product Account required.";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboCategorization.SelectedValue))
            {
                var IsValid = true;
                if (cboSRItemType.SelectedValue == ItemType.Medical && AppSession.Parameter.IsProcurementForItemMedicBasedOnInvCategory)
                    IsValid = false;
                else if (cboSRItemType.SelectedValue == ItemType.NonMedical && AppSession.Parameter.IsProcurementForItemNonMedicBasedOnInvCategory)
                    IsValid = false;
                else if (cboSRItemType.SelectedValue == ItemType.Kitchen && AppSession.Parameter.IsProcurementForItemKitchenBasedOnInvCategory)
                    IsValid = false;

                if (!IsValid)
                {
                    args.MessageText = "Inventory Category is required.";
                    args.IsCancel = true;
                    return;
                }
            }

            PopulateNewTransactionNo();
            // save autonumber immediately to decrease time gap between create and save
            _autoNumber.Save();

            var entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new ItemTransaction();
            entity.AddNew();
            SetEntityValue(entity);
            
            SaveEntity(entity);

            // Email to user
            if (boxApprovalProgress.Visible)
            {
                Util.ApprovalLevelUtil.EmailToApprover(entity);
            }
        }

        private bool IsSupplierExist(ValidateArgs args)
        {
            var query = new SupplierQuery();
            query.es.Top = 1;
            //query.Where(query.SupplierName == cboBusinessPartnerID.Text);
            query.Where(query.SupplierID == cboBusinessPartnerID.SelectedValue);

            var item = new Supplier();
            if (!item.Load(query))
            {
                args.IsCancel = true;
                args.MessageText = "Selected supplier not valid, please select exist supplier";
                return false;
            }
            return true;
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            //Check Supplier entry
            if (!IsSupplierExist(args))
                return;

            if (txtTotal.Value < 0)
            {
                args.MessageText = "The total amount is less than zero. Check back your transaction.";
                args.IsCancel = true;
                return;
            }

            if (ItemTransactionItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            if (ItemTransactionItems.Any(item => item.IsBonusItem == false && item.Price == 0))
            {
                args.MessageText = "There are items not bonuses at zero price. Check back your transaction.";
                args.IsCancel = true;
                return;
            }

            var entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                
                if (entity.IsNonMasterOrder == true & string.IsNullOrEmpty(entity.ProductAccountID))
                {
                    args.MessageText = "Product Account required.";
                    args.IsCancel = true;
                    return;
                }

                if (string.IsNullOrEmpty(entity.SRPurchaseCategorization))
                {
                    var IsValid = true;
                    if (entity.SRItemType == ItemType.Medical && AppSession.Parameter.IsProcurementForItemMedicBasedOnInvCategory)
                        IsValid = false;
                    else if (entity.SRItemType == ItemType.NonMedical && AppSession.Parameter.IsProcurementForItemNonMedicBasedOnInvCategory)
                        IsValid = false;
                    else if (entity.SRItemType == ItemType.Kitchen && AppSession.Parameter.IsProcurementForItemKitchenBasedOnInvCategory)
                        IsValid = false;

                    if (!IsValid)
                    {
                        args.MessageText = "Inventory Category is required.";
                        args.IsCancel = true;
                        return;
                    }
                }

                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "ItemTransaction";
        }

        public override bool OnGetStatusMenuEdit()
        {
            return txtTransactionNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !chkIsVoid.Checked;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
            btnGetPr.Enabled = newVal != AppEnum.DataMode.Read;
            btnResetPR.Enabled = newVal != AppEnum.DataMode.Read;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ItemTransaction();
            if (parameters.Length > 0)
            {
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(parameters[0]);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var itemTransaction = (ItemTransaction)entity;
            txtTransactionNo.Text = itemTransaction.TransactionNo;
            txtTransactionDate.SelectedDate = itemTransaction.TransactionDate;
            txtDeliveryOrdersDate.SelectedDate = itemTransaction.DeliveryOrdersDate;

            object deliveryOrdersDate = itemTransaction.DeliveryOrdersDate;
            if (deliveryOrdersDate != null)
                txtDeliveryOrdersDate.SelectedDate = itemTransaction.DeliveryOrdersDate;
            else
                txtDeliveryOrdersDate.Clear();

            if (!string.IsNullOrEmpty(itemTransaction.BusinessPartnerID))
            {
                var suppq = new SupplierQuery();
                suppq.Where(suppq.SupplierID == itemTransaction.BusinessPartnerID);
                DataTable suppdtb = suppq.LoadDataTable();
                cboBusinessPartnerID.DataSource = suppdtb;
                cboBusinessPartnerID.DataBind();
                cboBusinessPartnerID.SelectedValue = suppdtb.Rows[0]["SupplierID"].ToString();
                cboBusinessPartnerID.Text = suppdtb.Rows[0]["SupplierName"].ToString();

                txtPBFLicenseNo.Text = suppdtb.Rows[0]["PBFLicenseNo"].ToString();

                //cboBusinessPartnerID.SelectedValue = itemTransaction.BusinessPartnerID;
                //var supp = new Supplier();
                //txtPBFLicenseNo.Text = supp.LoadByPrimaryKey(itemTransaction.BusinessPartnerID) ? supp.PBFLicenseNo : string.Empty;
            }
            else
            {
                cboBusinessPartnerID.Items.Clear();
                cboBusinessPartnerID.Text = string.Empty;
                txtPBFLicenseNo.Text = string.Empty;
            }

            ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.PurchaseOrder, true);

            cboSRDownPaymentType.SelectedValue = itemTransaction.SRDownPaymentType;
            cboSRPaymentType.SelectedValue = itemTransaction.SRPaymentType;
            cboTermID.SelectedValue = itemTransaction.TermID;
            cboSRPurchaseOrderType.SelectedValue = itemTransaction.SRPurchaseOrderType;
            cboCurrencyType.SelectedValue = itemTransaction.CurrencyID;
            txtCurrencyRate.Value = Convert.ToDouble(itemTransaction.CurrencyRate ?? 1);
            txtTransactionAmount.Value = Convert.ToDouble(itemTransaction.DiscountAmount + itemTransaction.ChargesAmount);
            txtDiscountAmount.Value = Convert.ToDouble(itemTransaction.DiscountAmount);
            txtChargesAmount.Value = Convert.ToDouble(itemTransaction.ChargesAmount);
            txtAmountTaxed.Value = Convert.ToDouble(itemTransaction.AmountTaxed);
            txtDownPaymentAmount.Value = Convert.ToDouble(itemTransaction.DownPaymentAmount);
            txtAdvanceAmount.Value = Convert.ToDouble(itemTransaction.AdvanceAmount);

            chkIsTaxable.Checked = itemTransaction.IsTaxable == 1;
            rblTypesOfTaxes.SelectedIndex = itemTransaction.IsTaxable == 1 ? 0 : (itemTransaction.IsTaxable == 0 ? 1 : 2);

            txtTaxPercentage.Value = Convert.ToDouble(itemTransaction.TaxPercentage);
            txtTaxAmount.Value = Convert.ToDouble(itemTransaction.TaxAmount);
            chkIsVoid.Checked = itemTransaction.IsVoid ?? false;
            chkIsApproved.Checked = itemTransaction.IsApproved ?? false;
            chkIsClosed.Checked = itemTransaction.IsClosed ?? false;
            txtNotes.Text = itemTransaction.Notes;
            chkIsNonMasterOrder.Checked = itemTransaction.IsNonMasterOrder ?? false;
            chkIsInventoryItem.Checked = itemTransaction.IsInventoryItem ?? false;
            chkIsAssets.Checked = itemTransaction.IsAssets ?? false;
            chkIsConsignment.Checked = itemTransaction.IsConsignment ?? false;
            chkIsConsignmentAlreadyReceived.Checked = itemTransaction.IsConsignmentAlreadyReceived ?? false;
            txtLeadTime.Text = itemTransaction.LeadTime;
            txtContractNo.Text = itemTransaction.ContractNo;
            txtTotal.Value = txtChargesAmount.Value + txtTaxAmount.Value + txtDownPaymentAmount.Value - txtAdvanceAmount.Value;
            txtTermOfPayment.Value = Convert.ToDouble(itemTransaction.TermOfPayment);

            cboSRPph.SelectedValue = itemTransaction.SRPph;
            txtPphPercentage.Value = Convert.ToDouble(itemTransaction.PphPercentage);
            txtPphAmount.Value = Convert.ToDouble(itemTransaction.PphAmount);

            if ((!string.IsNullOrEmpty(Request.QueryString["pr"])) && DataModeCurrent == AppEnum.DataMode.New)
            {
                txtReferenceNo.Text = Request.QueryString["pr"];
                var tx = new ItemTransaction();
                tx.LoadByPrimaryKey(txtReferenceNo.Text);
                cboFromServiceUnitID.SelectedValue = tx.ToServiceUnitID;
                ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, TransactionCode.PurchaseOrder);
                cboSRItemType.SelectedValue = tx.SRItemType;
                cboSRItemCategory.SelectedValue = tx.SRItemCategory;
                PopulateGridDetailFromReferenceNo();

                //if (!string.IsNullOrEmpty(cboBusinessPartnerID.SelectedValue)) {
                //    var args = new RadComboBoxSelectedIndexChangedEventArgs(cboBusinessPartnerID.Text,string.Empty, cboBusinessPartnerID.SelectedValue, string.Empty);
                //    cboBusinessPartnerID_SelectedIndexChanged(cboBusinessPartnerID, args);
                //}
            }
            else
            {
                txtReferenceNo.Text = itemTransaction.ReferenceNo;
                if (!string.IsNullOrEmpty(itemTransaction.FromServiceUnitID))
                    cboFromServiceUnitID.SelectedValue = itemTransaction.FromServiceUnitID;
                else
                {
                    cboFromServiceUnitID.Items.Clear();
                    cboFromServiceUnitID.Text = string.Empty;
                    ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, TransactionCode.PurchaseOrder, true);
                }
                ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, TransactionCode.PurchaseOrder);
                cboSRItemType.SelectedValue = itemTransaction.SRItemType;
                cboSRItemCategory.SelectedValue = itemTransaction.SRItemCategory;
                cboSRProductAccountID.SelectedValue = itemTransaction.ProductAccountID;
                if (!string.IsNullOrEmpty(itemTransaction.SRPurchaseCategorization))
                    cboCategorization.SelectedValue = itemTransaction.SRPurchaseCategorization;
                else
                {
                    cboCategorization.SelectedValue = string.Empty;
                    cboCategorization.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(itemTransaction.SRProcurementType))
                    cboSRProcurementType.SelectedValue = itemTransaction.SRProcurementType;
                else
                {
                    cboSRProcurementType.SelectedValue = string.Empty;
                    cboSRProcurementType.Text = string.Empty;
                }

                PopulateGridDetail();
            }

            chkIsInstallmentOrder.Checked = itemTransaction.IsInstallmentType ?? false;

            CalculateDetailTransaction(false);
            grdApproval.Rebind();
        }

        private void SetEntityValue(ItemTransaction entity)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionCode = BusinessObject.Reference.TransactionCode.PurchaseOrder;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            if (txtDeliveryOrdersDate.IsEmpty)
                entity.str.DeliveryOrdersDate = string.Empty;
            else
                entity.DeliveryOrdersDate = txtDeliveryOrdersDate.SelectedDate;
            entity.BusinessPartnerID = cboBusinessPartnerID.SelectedValue;
            entity.CurrencyID = cboCurrencyType.SelectedValue;
            entity.CurrencyRate = Convert.ToDecimal(txtCurrencyRate.Value);
            entity.ReferenceNo = txtReferenceNo.Text;
            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            entity.TermID = cboTermID.SelectedValue;
            entity.SRPurchaseOrderType = cboSRPurchaseOrderType.SelectedValue;
            entity.SRItemType = cboSRItemType.SelectedValue;
            entity.SRItemCategory = cboSRItemCategory.SelectedValue;
            entity.DiscountAmount = Convert.ToDecimal(txtDiscountAmount.Value);
            entity.ChargesAmount = Convert.ToDecimal(txtChargesAmount.Value);
            entity.AmountTaxed = Convert.ToDecimal(txtAmountTaxed.Value);
            entity.DownPaymentAmount = Convert.ToDecimal(txtDownPaymentAmount.Value);
            entity.AdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Value);
            entity.SRDownPaymentType = cboSRDownPaymentType.SelectedItem.Value;
            entity.SRPaymentType = cboSRPaymentType.SelectedValue;
            entity.TaxPercentage = Convert.ToDecimal(txtTaxPercentage.Value);
            entity.TaxAmount = Convert.ToDecimal(txtTaxAmount.Value);
            entity.IsTaxable = Convert.ToInt16(rblTypesOfTaxes.SelectedIndex == 0 ? 1 : (rblTypesOfTaxes.SelectedIndex == 1 ? 0 : 2));

            entity.Notes = txtNotes.Text;
            entity.IsNonMasterOrder = chkIsNonMasterOrder.Checked;
            entity.IsInventoryItem = chkIsInventoryItem.Checked;
            entity.IsAssets = chkIsAssets.Checked;
            entity.IsConsignment = chkIsConsignment.Checked;
            entity.IsConsignmentAlreadyReceived = chkIsConsignmentAlreadyReceived.Checked;
            entity.LeadTime = txtLeadTime.Text;
            entity.ContractNo = txtContractNo.Text;
            entity.ProductAccountID = cboSRProductAccountID.SelectedValue;
            entity.TermOfPayment = Convert.ToDecimal(txtTermOfPayment.Value);

            entity.SRPph = cboSRPph.SelectedValue;
            entity.PphPercentage = Convert.ToDecimal(txtPphPercentage.Value);
            entity.PphAmount = Convert.ToDecimal(txtPphAmount.Value);

            if (!string.IsNullOrEmpty(entity.ReferenceNo))
            {
                var refs = new ItemTransaction();
                if (refs.LoadByPrimaryKey(entity.ReferenceNo))
                {
                    entity.ServiceUnitCostID = refs.ServiceUnitCostID;
                    entity.IsConsignmentAlreadyReceived = refs.IsConsignmentAlreadyReceived ?? false;
                }
                else
                {
                    entity.ServiceUnitCostID = string.Empty;
                    entity.IsConsignmentAlreadyReceived = false;
                }
            }
            else
            {
                entity.ServiceUnitCostID = string.Empty;
                entity.IsConsignmentAlreadyReceived = false;
            }
            
            entity.SRPurchaseCategorization = cboCategorization.SelectedValue;
            entity.SRProcurementType = cboSRProcurementType.SelectedValue;
            entity.IsInstallmentType = chkIsInstallmentOrder.Checked;

            if (entity.es.IsAdded)
            {
                entity.CreateByUserID = AppSession.UserLogin.UserID;
                entity.CreateDateTime = (new DateTime()).NowAtSqlServer();
            }

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            //Update Detil
            foreach (ItemTransactionItem item in ItemTransactionItems)
            {
                item.TransactionNo = txtTransactionNo.Text;
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
        }

        private void SaveEntity(ItemTransaction entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ItemTransactionItems.Save();

                //autonumber has been saved on SetEntity
                //if (DataModeCurrent == AppEnum.DataMode.New)
                //    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            if (AppSession.Parameter.HealthcareInitial == "RSYS")

            {
                var que = new ItemTransactionQuery("a");
                var qusr = new AppUserServiceUnitQuery("u");
                que.InnerJoin(qusr).On(que.FromServiceUnitID == qusr.ServiceUnitID &&
                                             qusr.UserID == AppSession.UserLogin.UserID);

                que.es.Top = 1; // SELECT TOP 1 ..
                if (isNextRecord)
                {
                    que.Where(
                        que.TransactionNo > txtTransactionNo.Text,
                        que.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseOrder
                        );


                    que.OrderBy(que.TransactionNo.Ascending);
                }
                else
                {
                    que.Where(
                        que.TransactionNo < txtTransactionNo.Text,
                        que.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseOrder
                        );


                    que.OrderBy(que.TransactionNo.Descending);
                }
                var entity = new ItemTransaction();
                entity.Load(que);
                ItemTransactionItems = null;
                OnPopulateEntryControl(entity);
            }
            else
            {
                var que = new ItemTransactionQuery("a");
                var qusr = new AppUserServiceUnitQuery("u");
                que.InnerJoin(qusr).On(que.FromServiceUnitID == qusr.ServiceUnitID &&
                                             qusr.UserID == AppSession.UserLogin.UserID);

                que.es.Top = 1; // SELECT TOP 1 ..
                if (isNextRecord)
                {
                    que.Where(
                        que.TransactionNo > txtTransactionNo.Text,
                        que.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseOrder
                        );
                    if (Request.QueryString["cons"] == "0")
                        que.Where(que.IsConsignment == false);
                    else que.Where(que.IsConsignment == true);

                    que.OrderBy(que.TransactionNo.Ascending);
                }
                else
                {
                    que.Where(
                        que.TransactionNo < txtTransactionNo.Text,
                        que.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseOrder
                        );
                    if (Request.QueryString["cons"] == "0")
                        que.Where(que.IsConsignment == false);
                    else que.Where(que.IsConsignment == true);

                    que.OrderBy(que.TransactionNo.Descending);
                }
                var entity = new ItemTransaction();
                entity.Load(que);
                ItemTransactionItems = null;
                OnPopulateEntryControl(entity);
            }
            
        }

        private void CalculateDetailTransaction(bool isNew)
        {
            if (ItemTransactionItems.Count > 0)
            {
                decimal? totaltransaction = 0;
                decimal? totaldiscitem = 0;
                decimal? totaltax = 0;

                foreach (ItemTransactionItem item in ItemTransactionItems)
                {
                    if (!Convert.ToBoolean(item.IsBonusItem))
                    {
                        totaltransaction += (item.Price * item.Quantity);
                        totaldiscitem += (item.Discount * item.Quantity);

                        if (Convert.ToBoolean(item.IsTaxable))
                            totaltax += ((item.Price - item.Discount) * item.Quantity);
                    }
                }

                txtTransactionAmount.Value = Convert.ToDouble(totaltransaction - totaldiscitem);
                txtChargesAmount.Value = Convert.ToDouble(totaltransaction - totaldiscitem) - txtDiscountAmount.Value;
                if (Convert.ToDouble(totaltax) - txtDiscountAmount.Value < 0)
                    txtAmountTaxed.Value = 0;
                else
                    txtAmountTaxed.Value = Convert.ToDouble(totaltax) - txtDiscountAmount.Value;

                if (AppSession.Parameter.IsPORoundingDownZeroDigit)
                {
                    txtTransactionAmount.Value = System.Convert.ToInt64(txtTransactionAmount.Value);
                    txtChargesAmount.Value = System.Convert.ToInt64(txtChargesAmount.Value);
                    txtAmountTaxed.Value = System.Convert.ToInt64(txtAmountTaxed.Value);
                }

                if (isNew) CalculateTax();
                CalculatePph();
            }
        }

        private void CalculateTax()
        {
            if (txtTaxPercentage.Value == 0)
                txtTaxAmount.Value = 0.00;
            else
            {
                txtTaxAmount.Value = ((txtAmountTaxed.Value * txtTaxPercentage.Value) / Convert.ToDouble(100));
                if (AppSession.Parameter.IsPORoundingDownZeroDigit)
                {
                    txtTaxAmount.Value = System.Convert.ToInt64(txtTaxAmount.Value);
                }
            }
            CalculateTotal();
            CalculatePph();
        }

        private void CalculateTotal()
        {
            var total = txtChargesAmount.Value + txtTaxAmount.Value + txtDownPaymentAmount.Value - txtAdvanceAmount.Value;
            txtTotal.Value = total;
        }

        private void GetTax(string suppId)
        {
            var supp = new Supplier();
            if (supp.LoadByPrimaryKey(suppId))
            {
                chkIsTaxable.Checked = supp.IsPKP ?? false;
                rblTypesOfTaxes.SelectedIndex = supp.IsPKP ?? false ? 0 : AppSession.Parameter.SupplierNonPkpTaxStatusDefault.ToInt();
                txtTaxPercentage.Value = supp.IsPKP ?? false ? Convert.ToDouble(supp.TaxPercentage ?? 0) : 0;
                txtTermOfPayment.Value = Convert.ToDouble(supp.TermOfPayment);
                txtPBFLicenseNo.Text = supp.PBFLicenseNo;
            }
            else
            {
                chkIsTaxable.Checked = false;
                rblTypesOfTaxes.SelectedIndex = 0;
                txtTaxPercentage.Value = 0;
                txtTermOfPayment.Value = 0;
                txtPBFLicenseNo.Text = string.Empty;
            }
        }

        private void GetContractNo()
        {
            if (cboBusinessPartnerID.SelectedValue != null && txtTransactionDate.SelectedDate != null)
            {
                var sc = new SupplierContractQuery();
                sc.Select(sc.TransactionNo);
                sc.Where(
                    sc.SupplierID == cboBusinessPartnerID.SelectedValue, txtTransactionDate.SelectedDate >= sc.ContractStart,
                    txtTransactionDate.SelectedDate <= sc.ContractEnd, sc.IsActive == true);
                sc.OrderBy(sc.TransactionNo.Ascending);
                sc.es.Top = 1;
                DataTable dtsc = sc.LoadDataTable();
                if (dtsc.Rows.Count > 0)
                    txtContractNo.Text = dtsc.Rows[0]["TransactionNo"].ToString();
                else
                    txtContractNo.Text = string.Empty;
            }
            else
                txtContractNo.Text = string.Empty;
        }

        private void UpdateItemPrice()
        {
            if (ItemTransactionItems.Count > 0)
            {
                decimal? totaltransaction = 0;
                decimal? totaldiscitem = 0;
                decimal? totaltax = 0;

                foreach (var entity in ItemTransactionItems)
                {
                    if (entity.IsBonusItem == false)
                    {
                        /////
                        // update harga per supplier
                        var scItem = new SupplierContractItem();
                        if (scItem.LoadByPrimaryKey(txtContractNo.Text, entity.ItemID))
                        {
                            entity.Discount1Percentage = scItem.PurchaseDiscount1 ?? 0;
                            entity.Discount2Percentage = scItem.PurchaseDiscount2 ?? 0;
                            entity.Price = scItem.PriceInPurchaseUnit ?? 0;
                        }
                        else
                        {
                            entity.SetQtyPricePO(entity.ReferenceNo, entity.ItemID,
                                entity.ConversionFactor.ToDecimal(), cboBusinessPartnerID.SelectedValue, false);
                        }
                        // end of update harga per supplier

                        entity.PriceInCurrency = (entity.Price) * Convert.ToDecimal(txtCurrencyRate.Value);
                        entity.Discount = (entity.Price * entity.Discount1Percentage / 100) +
                                          ((entity.Price - (entity.Price * entity.Discount1Percentage / 100)) *
                                           entity.Discount2Percentage / 100);
                        entity.DiscountInCurrency = entity.Discount * Convert.ToDecimal(txtCurrencyRate.Value);
                        entity.IsDiscountInPercent = true;
                        /////

                        entity.Discount = (entity.Price * entity.Discount1Percentage / 100) +
                                            ((entity.Price - (entity.Price * entity.Discount1Percentage / 100)) *
                                             entity.Discount2Percentage / 100);

                        totaltransaction += (entity.Price * entity.Quantity);
                        totaldiscitem += (entity.Discount * entity.Quantity);

                        if (entity.IsTaxable ?? false)
                            totaltax += ((entity.Price - entity.Discount) * entity.Quantity);
                    }
                }

                grdItemTransactionItem.Rebind();

                txtTransactionAmount.Value = Convert.ToDouble(totaltransaction - totaldiscitem);
                txtChargesAmount.Value = Convert.ToDouble(totaltransaction - totaldiscitem) -
                                         txtDiscountAmount.Value;
                txtAmountTaxed.Value = Convert.ToDouble(totaltax) - txtDiscountAmount.Value;

                if (AppSession.Parameter.IsPORoundingDownZeroDigit)
                {
                    txtTransactionAmount.Value = System.Convert.ToInt64(txtTransactionAmount.Value);
                    txtChargesAmount.Value = System.Convert.ToInt64(txtChargesAmount.Value);
                    txtAmountTaxed.Value = System.Convert.ToInt64(txtAmountTaxed.Value);
                }
            }
        }

        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateNewTransactionNo();
            var itemTypeBefore = cboSRItemType.SelectedValue;
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, BusinessObject.Reference.TransactionCode.PurchaseOrder);
            if (!string.IsNullOrEmpty(itemTypeBefore))
                cboSRItemType.SelectedValue = itemTypeBefore;
        }

        protected void cboBusinessPartnerID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.SupplierItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboSupplier_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SupplierQuery("a");
            query.Where(
                query.Or(query.SupplierID == e.Text,
                query.SupplierName.Like(searchTextContain)),
                query.IsActive == true
                );
            if (!string.IsNullOrEmpty(Request.QueryString["suptype"]))
                query.Where(query.SRSupplierType == Request.QueryString["suptype"]);

            query.Select(query.SupplierID, query.SupplierName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboBusinessPartnerID.DataSource = dtb;
            cboBusinessPartnerID.DataBind();
        }

        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.SupplierItemDataBound(e);
        }

        protected void cboBusinessPartnerID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //GetContractNo();
            UpdateItemPrice();
            GetTax(e.Value);
            CalculateTax();
        }

        protected void chkIsTaxable_CheckedChanged(object sender, EventArgs e)
        {
            var supp = new Supplier();
            supp.LoadByPrimaryKey(cboBusinessPartnerID.SelectedValue);

            if (chkIsTaxable.Checked)
                txtTaxPercentage.Value = Convert.ToDouble(supp.TaxPercentage ?? 0);
            else
                txtTaxPercentage.Value = 0;

            CalculateTax();
        }

        protected void txtTaxPercentage_TextChanged(object sender, EventArgs e)
        {
            CalculateTax();
        }

        protected void txtDiscountAmount_TextChanged(object sender, EventArgs e)
        {
            txtChargesAmount.Value = txtTransactionAmount.Value - txtDiscountAmount.Value;
            decimal? totaltax = ItemTransactionItems.Where(item => !Convert.ToBoolean(item.IsBonusItem) && Convert.ToBoolean(item.IsTaxable)).Aggregate<ItemTransactionItem, decimal?>(0, (current, item) => current + ((item.Price - item.Discount) * item.Quantity));
            txtAmountTaxed.Value = Convert.ToDouble(totaltax) - txtDiscountAmount.Value;

            if (AppSession.Parameter.IsPORoundingDownZeroDigit)
            {
                txtChargesAmount.Value = System.Convert.ToInt64(txtChargesAmount.Value);
                txtAmountTaxed.Value = System.Convert.ToInt64(txtAmountTaxed.Value);
            }

            CalculateTax();
        }

        protected void txtDownPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
            CalculatePph();
        }

        protected void txtAdvanceAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
            CalculatePph();
        }

        private ItemTransactionItemCollection ItemTransactionItems
        {
            get
            {
                object obj = Session["PurchaseOrderItems" + Request.UserHostName];
                if (obj != null)
                    return ((ItemTransactionItemCollection)(obj));

                var coll = new ItemTransactionItemCollection();
                var query = new ItemTransactionItemQuery("a");
                var iq = new ItemQuery("b");
                var suppItemq = new SupplierItemQuery("e");
                var fq = new FabricQuery("f");

                query.Select(
                    query, 
                    @"<ISNULL(e.DrugDistributionLicenseNo, '') AS refToItemProductMedic_DrugDistributionLicenseNo>",
                    @"<CASE WHEN b.SRItemType = '11' THEN 'Drug Dist. License No : ' + ISNULL(e.DrugDistributionLicenseNo, '') WHEN b.SRItemType = '21' THEN 'Specification : '+ ISNULL(a.Specification, '') ELSE '' END AS 'refToAdditionalInfo'>",
                    fq.FabricName.As("refToFabric_FabricName"),
                    @"<ISNULL(b.IsAsset, 0) AS 'refToItem_IsAsset'>",
                    @"<ISNULL(b.EconomicLifeInYear, 0) AS 'refToItem_EconomicLifeInYear'>");
                
                query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
                query.LeftJoin(suppItemq).On(query.ItemID == suppItemq.ItemID &&
                                             suppItemq.SupplierID == cboBusinessPartnerID.SelectedValue);
                query.LeftJoin(fq).On(fq.FabricID == query.FabricID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);

                if (chkIsInventoryItem.Checked && AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPOWithStockInfo))
                {
                    InitializeQueryWithStockInfo(query);
                }
                else
                {
                    query.Select(@"<CONVERT(decimal(10,2), 0) AS refToItemBalance_Balance>",
                        @"<CONVERT(decimal(10,2), 0) AS refToItemBalance_BalanceSG>",
                        @"<CONVERT(decimal(10,2), 0) AS refToItemBalance_Minimum>",
                        @"<CONVERT(decimal(10,2), 0) AS refToItemBalance_Maximum>",
                        @"<'' AS refToItemProduct_SRItemUnit>",
                        @"<CONVERT(decimal(10,2), 0) AS refToItemBalance_BalanceTotal>"
                    );
                }
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);
                Session["PurchaseOrderItems" + Request.UserHostName] = coll;

                return coll;
            }
            set { Session["PurchaseOrderItems" + Request.UserHostName] = value; }
        }

        private void InitializeQueryWithStockInfo(ItemTransactionItemQuery query)
        {
            // Base Unit
            var ipnmq = new ItemProductNonMedicQuery("i2");
            var ikq = new ItemKitchenQuery("i2");
            var ipmq = new ItemProductMedicQuery("i2");

            // Balance Min Max
            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorByStockGroup) && !string.IsNullOrEmpty(txtReferenceNo.Text))
            {
                var itref = new ItemTransaction();
                if (itref.LoadByPrimaryKey(txtReferenceNo.Text))
                {
                    switch (itref.SRItemType)
                    {
                        case ItemType.NonMedical:
                            query.LeftJoin(ipnmq).On(query.ItemID == ipnmq.ItemID);
                            break;
                        case ItemType.Kitchen:
                            query.LeftJoin(ikq).On(query.ItemID == ikq.ItemID);
                            break;
                        default:
                            query.LeftJoin(ipmq).On(query.ItemID == ipmq.ItemID);
                            break;
                    }

                    var ibq = new ItemBalanceByStockGroupQuery("c");
                    var stockGroup = "abcd";
                    var loc = new Location();
                    loc.LoadByPrimaryKey(itref.FromLocationID);
                    if (!string.IsNullOrEmpty(loc.SRStockGroup))
                        stockGroup = loc.SRStockGroup;

                    query.LeftJoin(ibq).On(query.ItemID == ibq.ItemID && ibq.SRStockGroup == stockGroup);

                    var locationID = ProcurementUtils.LocationIdByItemType(cboSRItemType.SelectedValue);
                    var ibq2 = new ItemBalanceQuery("d");
                    if (string.IsNullOrEmpty(locationID))
                        locationID = "ABCD_EFG";
                    query.LeftJoin(ibq2).On(query.ItemID == ibq2.ItemID && ibq2.LocationID == locationID);

                    query.Select(@"<CONVERT(decimal(10,2),COALESCE(d.Balance,0)) AS refToItemBalance_Balance>",
                        @"<CONVERT(decimal(10,2),COALESCE(c.Balance,0)) AS refToItemBalance_BalanceSG>",
                        @"<CONVERT(decimal(10,2),COALESCE(c.Minimum,0)) AS refToItemBalance_Minimum>",
                        @"<CONVERT(decimal(10,2),COALESCE(c.Maximum,0)) AS refToItemBalance_Maximum>",
                        @"<i2.SRItemUnit AS refToItemProduct_SRItemUnit>"
                        );
                }
                else
                {
                    query.Select(@"<CONVERT(decimal(10,2), 0) AS refToItemBalance_Balance>",
                        @"<CONVERT(decimal(10,2), 0) AS refToItemBalance_BalanceSG>",
                        @"<CONVERT(decimal(10,2), 0) AS refToItemBalance_Minimum>",
                        @"<CONVERT(decimal(10,2), 0) AS refToItemBalance_Maximum>",
                        @"<'' AS refToItemProduct_SRItemUnit>"
                        );
                }
            }
            else
            {
                switch (cboSRItemType.SelectedValue)
                {
                    case ItemType.NonMedical:
                        query.LeftJoin(ipnmq).On(query.ItemID == ipnmq.ItemID);
                        break;
                    case ItemType.Kitchen:
                        query.LeftJoin(ikq).On(query.ItemID == ikq.ItemID);
                        break;
                    default:
                        query.LeftJoin(ipmq).On(query.ItemID == ipmq.ItemID);
                        break;
                }

                var locationID = ProcurementUtils.LocationIdByItemType(cboSRItemType.SelectedValue);
                var ibq = new ItemBalanceQuery("c");
                if (string.IsNullOrEmpty(locationID))
                    locationID = "ABCD_EFG";
                query.LeftJoin(ibq).On(query.ItemID == ibq.ItemID && ibq.LocationID == locationID);

                query.Select(@"<CONVERT(decimal(10,2),COALESCE(c.Balance,0)) AS refToItemBalance_Balance>",
                    @"<CONVERT(decimal(10,2), 0) AS refToItemBalance_BalanceSG>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Minimum,0)) AS refToItemBalance_Minimum>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Maximum,0)) AS refToItemBalance_Maximum>",
                    @"<i2.SRItemUnit AS refToItemProduct_SRItemUnit>"
                    );
            }

            // Sub Query BalanceTotal
            var itemBalTot = new ItemBalanceQuery("ibt");
            itemBalTot.Select((itemBalTot.Balance.Sum().As("BalanceTotal")));
            itemBalTot.Where(itemBalTot.ItemID == query.ItemID);
            query.Select(itemBalTot.Select().As("refToItemBalance_BalanceTotal"));
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemTransactionItem.Columns[0].Visible = isVisible;
            grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 1].Visible = isVisible;

            grdItemTransactionItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                ItemTransactionItems = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdItemTransactionItem.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            ItemTransactionItems = null; //Reset Record Detail
            grdItemTransactionItem.DataSource = ItemTransactionItems;
            grdItemTransactionItem.MasterTableView.IsItemInserted = false;
            grdItemTransactionItem.MasterTableView.ClearEditItems();
            grdItemTransactionItem.DataBind();
        }

        protected void grdItemTransactionItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemTransactionItem.DataSource = ItemTransactionItems;
        }

        protected void grdItemTransactionItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            ItemTransactionItem entity = FindItemTransactionItem(Convert.ToString(
                editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemTransactionItemMetadata.ColumnNames.SequenceNo])
                );

            if (entity != null)
                SetEntityValue(entity, e);

            CalculateDetailTransaction(true);
        }

        private ItemTransactionItem FindItemTransactionItem(String sequenceNo)
        {
            var coll = ItemTransactionItems;
            ItemTransactionItem retEntity = null;

            foreach (ItemTransactionItem rec in coll)
            {
                if (rec.SequenceNo.Equals(sequenceNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItemTransactionItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            if (!chkIsConsignmentAlreadyReceived.Checked)
            {
                GridDataItem item = e.Item as GridDataItem;
                if (item == null)
                    return;

                ItemTransactionItem entity = FindItemTransactionItem(
                    Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemTransactionItemMetadata.ColumnNames.SequenceNo]));
                if (entity != null)
                {
                    if (txtReferenceNo.Text.Length > 0)
                        if (Convert.ToBoolean(entity.IsBonusItem))
                            entity.MarkAsDeleted();
                        else
                            entity.MarkAsDeleted();
                    else
                        entity.MarkAsDeleted();
                }

                cboSRItemType.Enabled = ItemTransactionItems.Count == 0;
                cboSRItemCategory.Enabled = ItemTransactionItems.Count == 0;
                cboFromServiceUnitID.Enabled = ItemTransactionItems.Count == 0;
                cboCurrencyType.Enabled = ItemTransactionItems.Count == 0;
                txtCurrencyRate.ReadOnly = ItemTransactionItems.Count != 0;
                if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSCH" && !chkIsInventoryItem.Checked)
                    chkIsNonMasterOrder.Enabled = ItemTransactionItems.Count == 0;
                chkIsInventoryItem.Enabled = ItemTransactionItems.Count == 0;
                chkIsNonMasterOrder.Enabled = ItemTransactionItems.Count == 0 && !chkIsInventoryItem.Checked;
                chkIsConsignment.Enabled = ItemTransactionItems.Count == 0;
                cboSRProductAccountID.Enabled = chkIsNonMasterOrder.Checked;
                //if (AppSession.Application.IsModuleAssetActive)
                //    chkIsAssets.Enabled = ItemTransactionItems.Count == 0;

                CalculateDetailTransaction(true);
            }
        }

        protected void grdItemTransactionItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            if (!chkIsConsignmentAlreadyReceived.Checked)
            {
                ItemTransactionItem entity = ItemTransactionItems.AddNew();
                SetEntityValue(entity, e);

                CalculateDetailTransaction(true);
                //grid not close first
                e.Canceled = true;
                grdItemTransactionItem.Rebind();

                cboCurrencyType.Enabled = ItemTransactionItems.Count == 0;
                txtCurrencyRate.ReadOnly = ItemTransactionItems.Count != 0;
            }
        }

        private void SetEntityValue(ItemTransactionItem entity, GridCommandEventArgs e)
        {
            var userControl = (PurchaseOrderItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                if (!chkIsNonMasterOrder.Checked)
                {
                    entity.ItemID = userControl.ItemID;
                    var itm = new Item();
                    if (itm.LoadByPrimaryKey(entity.ItemID))
                    {
                        entity.IsAsset = itm.IsAsset ?? false;
                        entity.EconomicLifeInYear = itm.EconomicLifeInYear;
                    }
                    else
                    {
                        entity.IsAsset = false;
                        entity.EconomicLifeInYear = 0;
                    }
                }
                else
                {
                    entity.ItemID = string.Empty;
                    entity.IsAsset = false;
                    entity.EconomicLifeInYear = 0;
                }
                   
                entity.SequenceNo = userControl.SequenceNo;
                entity.Quantity = userControl.Quantity;
                entity.Description = userControl.ItemName;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ConversionFactor = userControl.ConversionFactor;
                entity.Price = userControl.Price;
                entity.PriceInCurrency = entity.Price * Convert.ToDecimal(txtCurrencyRate.Value);
                entity.IsDiscountInPercent = userControl.IsDiscountInPercent;
                if (entity.IsDiscountInPercent == true)
                {
                    entity.Discount1Percentage = userControl.Discount1Percentage;
                    entity.Discount2Percentage = userControl.Discount2Percentage;
                    decimal disc1 = Math.Round(Convert.ToDecimal(entity.Price * entity.Discount1Percentage / 100), 2);
                    decimal disc2 = Math.Round(Convert.ToDecimal((entity.Price - disc1) * entity.Discount2Percentage / 100), 2);
                    entity.Discount = disc1 + disc2;
                }
                else
                {
                    entity.Discount1Percentage = 0;
                    entity.Discount2Percentage = 0;
                    entity.Discount = userControl.DiscountAmount;
                }
                entity.DiscountInCurrency = entity.Discount * Convert.ToDecimal(txtCurrencyRate.Text);
                entity.IsBonusItem = userControl.IsBonusItem;
                entity.IsClosed = userControl.IsClosed;
                entity.Specification = userControl.Specs;
                entity.IsTaxable = userControl.IsTaxable;

                ProcurementUtils.PopulateBalanceInfoByBlankValue(entity);

                if (chkIsInventoryItem.Checked && AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPOWithStockInfo))
                {
                    // Override stock Info
                    if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorByStockGroup) && !string.IsNullOrEmpty(txtReferenceNo.Text))
                    {
                        var itref = new ItemTransaction();
                        if (itref.LoadByPrimaryKey(txtReferenceNo.Text))
                        {
                            var stockGroup = "abcd";
                            var loc = new Location();
                            loc.LoadByPrimaryKey(itref.FromLocationID);
                            if (!string.IsNullOrEmpty(loc.SRStockGroup))
                                stockGroup = loc.SRStockGroup;

                            ProcurementUtils.PopulateBalanceInfoByStockGroup(entity, stockGroup, itref.FromLocationID);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                        {
                            ProcurementUtils.PopulateBalanceInfoByItemType(entity, cboSRItemType.SelectedValue);
                        }
                    }
                }

                if (cboSRItemType.SelectedValue == ItemType.Medical)
                {
                    var suppItem = new SupplierItem();
                    entity.DrugDistributionLicenseNo = suppItem.LoadByPrimaryKey(cboBusinessPartnerID.SelectedValue,
                                                                                 entity.ItemID)
                                                           ? suppItem.DrugDistributionLicenseNo
                                                           : string.Empty;
                    entity.AdditionalInfo = "Drug Dist. License No : " + entity.DrugDistributionLicenseNo;
                }
                else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                {
                    entity.DrugDistributionLicenseNo = string.Empty;
                    entity.AdditionalInfo = "Specification : " + entity.Specification;
                }
                else {
                    entity.DrugDistributionLicenseNo = string.Empty;
                    entity.AdditionalInfo = string.Empty;
                }

                entity.FabricID = userControl.FabricID;
                entity.FabricName = userControl.FabricName;
                
            }
        }

        protected void cboCurrencyType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                var c = new CurrencyRate();
                c.LoadByPrimaryKey(e.Value);
                txtCurrencyRate.Value = Convert.ToDouble(c.CurrencyRate);

                if (ItemTransactionItems.Count > 0)
                {
                    foreach (ItemTransactionItem item in ItemTransactionItems)
                    {
                        item.PriceInCurrency = item.Price * Convert.ToDecimal(txtCurrencyRate.Value);
                        item.DiscountInCurrency = item.Discount * Convert.ToDecimal(txtCurrencyRate.Value);
                    }
                    grdItemTransactionItem.Rebind();
                }
            }
            else
                txtCurrencyRate.Value = 1;
        }

        protected void txtCurrencyRate_TextChanged(object sender, EventArgs e)
        {
            if (ItemTransactionItems.Count > 0)
            {
                foreach (ItemTransactionItem item in ItemTransactionItems)
                {
                    item.PriceInCurrency = item.Price * Convert.ToDecimal(txtCurrencyRate.Value);
                    item.DiscountInCurrency = item.Discount * Convert.ToDecimal(txtCurrencyRate.Value);
                }
                grdItemTransactionItem.Rebind();
            }
        }

        protected void chkIsInventoryItem_CheckedChanged(object sender, EventArgs e)
        {
            chkIsNonMasterOrder.Enabled = AppSession.Parameter.HealthcareInitialAppsVersion != "RSCH" && !chkIsInventoryItem.Checked;
            chkIsNonMasterOrder.Checked = false;
            chkIsAssets.Enabled = true;
            chkIsAssets.Checked = false;
            chkIsConsignment.Enabled = chkIsInventoryItem.Checked;
            chkIsConsignment.Checked = false;
            chkIsConsignmentAlreadyReceived.Checked = false;
        }

        protected void chkIsNonMasterOrder_CheckedChanged(object sender, EventArgs e)
        {
            cboSRProductAccountID.Enabled = chkIsNonMasterOrder.Checked;

            cboSRProductAccountID.Text = string.Empty;
            cboSRProductAccountID.SelectedValue = string.Empty;

            // default selection lihat dari appparameter
            if (chkIsNonMasterOrder.Checked && !string.IsNullOrEmpty(AppSession.Parameter.PoNonMasterDefPAccount))
            {
                cboSRProductAccountID.SelectedValue = AppSession.Parameter.PoNonMasterDefPAccount;
            }
            chkIsAssets.Enabled = !chkIsNonMasterOrder.Checked;
            chkIsAssets.Checked = false;
            chkIsConsignment.Enabled = chkIsInventoryItem.Checked;
            chkIsConsignment.Checked = false;
            chkIsConsignmentAlreadyReceived.Checked = false;
        }

        protected void chkIsAssets_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReferenceNo.Text))
            {
                chkIsConsignment.Enabled = !chkIsAssets.Checked && !chkIsNonMasterOrder.Checked;
                chkIsConsignment.Checked = false;
                chkIsConsignmentAlreadyReceived.Checked = false;
            }
        }

        protected void chkIsConsignment_CheckedChanged(object sender, EventArgs e)
        {
            chkIsAssets.Enabled = !chkIsConsignment.Checked;
            chkIsAssets.Checked = false;
        }

        protected void rblTypesOfTaxes_OnTextChanged(object sender, EventArgs e)
        {
            var supp = new Supplier();
            supp.LoadByPrimaryKey(cboBusinessPartnerID.SelectedValue);

            if (rblTypesOfTaxes.SelectedIndex == 0)
                txtTaxPercentage.Value = Convert.ToDouble(supp.TaxPercentage ?? 0);
            else
                txtTaxPercentage.Value = 0;

            CalculateTax();
        }

        protected void grdApproval_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!boxApprovalProgress.Visible) return;

            var dtbApprov = Util.ApprovalLevelUtil.ApprovalLevelQue(txtTransactionNo.Text);
            Session["ds_grdApproval"] = dtbApprov;
            grdApproval.DataSource = dtbApprov;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (sourceControl is RadGrid)
            {
                if (eventArgument.Contains("_approv|"))
                {
                    pnlInfo.Visible = false;
                    var msg = string.Empty;

                    bool isValid = true;
                    if (ItemTransactionItems.Count == 0)
                    {
                        msg = AppConstant.Message.RecordDetailEmpty;
                        isValid = false;
                    }
                    else if (txtTotal.Value < 0)
                    {
                        msg = "The total amount is less than zero. Check back your transaction.";
                        isValid = false;
                    }
                    //else if (string.IsNullOrEmpty(cboSRDownPaymentType.SelectedValue))
                    //{
                    //    msg = "Shipping Charges Type required.";
                    //    isValid = false;
                    //}
                    else if (trPaymentType.Visible & string.IsNullOrEmpty(cboSRPaymentType.SelectedValue))
                    {
                        msg = "Payment Type required.";
                        isValid = false;
                    } 
                    else if (chkIsNonMasterOrder.Checked & string.IsNullOrEmpty(cboSRProductAccountID.SelectedValue))
                    {
                        msg = "Product Account required.";
                        isValid = false;
                    }

                    if (isValid)
                        AproveUnApproveViaApproveLevel(eventArgument, true);
                    else
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = msg;
                    }
                }
                else if (eventArgument.Contains("_unapprov|"))
                {
                    AproveUnApproveViaApproveLevel(eventArgument, false);
                }
            }

        }

        private void AproveUnApproveViaApproveLevel(string eventArgument, bool isApprove)
        {
            var param = eventArgument.Split('|');
            var args = new ValidateArgs();

            using (var trans = new esTransactionScope())
            {
                if (isApprove)
                    Util.ApprovalLevelUtil.Approve(args, TransactionCode.PurchaseOrder, txtTransactionNo.Text, param[1].ToInt(), AppSession.UserLogin.UserID);
                else
                    Util.ApprovalLevelUtil.UnApprove(args, TransactionCode.PurchaseOrder, txtTransactionNo.Text, param[1].ToInt());

                if (!args.IsCancel)
                {
                    trans.Complete();
                    Helper.ShowMessageAfterPostback(Page, "Process success.");
                }
                else
                    Helper.ShowMessageAfterPostback(Page, string.Format("{0}. Process failed.", args.MessageText));
            }
            grdApproval.Rebind();

            //Approval Status Information
            var it = new ItemTransaction();
            it.LoadByPrimaryKey(txtTransactionNo.Text);

            var info = (Panel)Helper.FindControlRecursive(Master, "fw_PanelStatus");
            info.Visible = it.IsApproved ?? false;
            //((RadBinaryImage)Helper.FindControlRecursive(Master, "fw_StampStatus")).Visible = (it.IsApproved ?? false);
        }

        protected void cboSRPph_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CalculatePph();
        }

        private void CalculatePph()
        {
            var pph = new AppStandardReferenceItem();
            if (pph.LoadByPrimaryKey("Pph", cboSRPph.SelectedValue))
            {
                var amountTaxed = (txtAmountTaxed.Value + txtAdvanceAmount.Value);

                if (pph.ReferenceID == "Progresif")
                {
                    txtPphPercentage.Value = 0;

                    //decimal pphAmt = InvoiceSupplier.PphProgresif(Convert.ToDecimal(txtAmount.Value));
                    decimal pphAmt = InvoiceSupplier.PphProgresif(Convert.ToDecimal(amountTaxed));
                    txtPphAmount.Value = Convert.ToDouble(pphAmt);
                }
                else
                {
                    txtPphPercentage.Value = Convert.ToDouble(pph.ReferenceID);
                    txtPphAmount.Value = amountTaxed * (txtPphPercentage.Value / 100);
                    //txtPphAmount.Value = txtAmount.Value * (txtPphPercentage.Value / 100);
                }
            }
            else
            {
                txtPphPercentage.Value = 0;
                txtPphAmount.Value = 0;
            }
        }
    }
}
