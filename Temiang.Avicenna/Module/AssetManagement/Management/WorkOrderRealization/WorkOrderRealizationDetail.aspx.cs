using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class WorkOrderRealizationDetail : BasePageDetail
    {
        private string getPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "#";
            UrlPageList = "WorkOrderRealizationList.aspx?type=" + getPageID;

            ProgramID = getPageID == "" ? AppConstant.Program.AssetWorkOrderRealization : AppConstant.Program.SanitationActivityWorkOrderRealization;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, TransactionCode.AssetWorkOrder, false);
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.AssetWorkOrderRealization, true);

                StandardReference.InitializeIncludeSpace(cboSRWorkPriority, AppEnum.StandardReference.WorkPriority);
                StandardReference.InitializeIncludeSpace(cboSRAssetsStatus, AppEnum.StandardReference.AssetsStatus);
                StandardReference.InitializeIncludeSpace(cboSRAssetsWarrantyContract, AppEnum.StandardReference.AssetsWarrantyContract);
                StandardReference.InitializeIncludeSpace(cboSRFailureCode, AppEnum.StandardReference.FailureCode);

                rfvSRWorkTrade.Visible = AppSession.Parameter.IsWorkTradeMandatory;

                if (getPageID != "")
                {
                    StandardReference.InitializeIncludeSpace(cboSRWorkTrade, AppEnum.StandardReference.WorkTrade);

                    pnlAssetInfo.Visible = false;
                    //tabStrip.Tabs[2].Visible = false;
                    //tabStrip.Tabs[3].Visible = false;
                    //trCostEstimation.Visible = false;
                }
                else
                {
                    string[] exc = AppSession.Parameter.WorkTradeSanitation.Split(',');
                    StandardReference.InitializeIncludeSpace(cboSRWorkTrade, AppEnum.StandardReference.WorkTrade, exc, false);
                }

                RadToolBar2.Items[0].Visible = AppSession.Parameter.WorkOrderRealizationAutoGenerateTx == "PR";
                RadToolBar2.Items[1].Visible = AppSession.Parameter.WorkOrderRealizationAutoGenerateTx == "DR";
                RadToolBar2.Items[2].Visible = AppSession.Parameter.WorkOrderRealizationAutoGenerateTx == "IR";

                if (AppSession.Parameter.IsUsingCentralizedPurchaseRequest)
                {
                    tabStrip.Tabs[3].Text = "Distribution Request / Inventory Issue Request";
                    rbGenerateList.SelectedValue = AppSession.Parameter.WorkOrderRealizationAutoGenerateTx == "DR" ? "0" : "1";
                    switch (rbGenerateList.SelectedValue)
                    {
                        case "0":
                            ComboBox.PopulateWithServiceUnitForTransaction(cboGenerateToServiceUnitID, TransactionCode.Distribution, false, string.Empty, ItemType.NonMedical);
                            break;
                        case "1":
                            ComboBox.PopulateWithServiceUnitForTransaction(cboGenerateToServiceUnitID, TransactionCode.InventoryIssueOutForOtherUnit, false, string.Empty, ItemType.NonMedical);
                            break;
                    }
                }
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            if (getPageID == "")
            {
                ajax.AddAjaxSetting(grdItem, grdItem);
                ajax.AddAjaxSetting(grdItem, txtCostEstimation);
                ajax.AddAjaxSetting(grdItem, pnlInfo);
                ajax.AddAjaxSetting(grdItem, lblInfo);
                ajax.AddAjaxSetting(grdItem, grdRequestList);
                ajax.AddAjaxSetting(grdItem, pnlInfo2);
                ajax.AddAjaxSetting(grdItem, lblInfo2);
                ajax.AddAjaxSetting(grdRequestList, grdRequestList);
            }
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new AssetWorkOrder();
            if (entity.LoadByPrimaryKey(txtOrderNo.Text))
            {
                if (entity.SRWorkStatus == AppSession.Parameter.WorkStatusClosed && entity.IsProceed == true)
                {
                    args.MessageText = "Work Order has been closed. The data can not be changed.";
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuEditClick()
        {
            if (getPageID != "")
            {
                cboSRWorkTrade.Enabled = false;
                cboSRWorkTradeItem.Enabled = !chkIsPreventiveMaintenance.Checked;
            }
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            if (cboSRWorkStatus.SelectedValue == AppSession.Parameter.WorkStatusOpen || cboSRWorkStatus.SelectedValue == AppSession.Parameter.WorkStatusClosed)
            {
                args.MessageText = "Work Status should be chosen with the other options.";
                args.IsCancel = true;
                return;
            }

            if (AssetWorkOrderImplementers.Count == 0)
            {
                args.MessageText = "Implementer required.";
                args.IsCancel = true;
                return;
            }

            var entity = new AssetWorkOrder();
            entity.LoadByPrimaryKey(txtOrderNo.Text);

            if (cboSRWorkStatus.SelectedValue == AppSession.Parameter.WorkStatusThirdParties && string.IsNullOrEmpty(entity.ReceivedFromThirdPartiesByUserID))
            {
                args.MessageText = "Assets have not been received from a third party.";
                args.IsCancel = true;
                return;
            }

            var woReff = new AssetWorkOrderCollection();
            woReff.Query.Where(woReff.Query.ReferenceNo == txtOrderNo.Text, woReff.Query.IsProceed == false);
            woReff.LoadAll();
            if (woReff.Count > 0)
            {
                args.MessageText = "There is a refer work order that has not been completed.";
                args.IsCancel = true;
                return;
            }

            var unit = new ServiceUnit();
            var locationId = unit.GetMainLocationId(cboToServiceUnitID.SelectedValue);

            var location = new Location();
            bool isLocattionAllowedToStockGoods = true;
            if (location.LoadByPrimaryKey(locationId))
                isLocattionAllowedToStockGoods = location.IsAllowedToStockGoods ?? true;

            if (getPageID == "" && AppSession.Parameter.IsMaterialUsedAwoNeedRequest)
            {
                if (isLocattionAllowedToStockGoods == false)
                {
                    foreach (var item in AssetWorkOrderItems)
                    {
                        if (item.Quantity > 0)
                        {
                            if (item.IsGeneratePrDr == false && item.IsGenerateIr == false)
                            {
                                if (item.IsInventoryItem)
                                {
                                    if (AppSession.Parameter.WorkOrderRealizationAutoGenerateTx == "DR")
                                        args.MessageText = "There is no Distribution Request transaction for item : " + item.ItemName + ". Recheck your request list.";
                                    else
                                        args.MessageText = "There is no Inventroy Issue Request transaction for item : " + item.ItemName + ". Recheck your request list.";
                                }
                                else
                                    args.MessageText = "There is no Purchase Request transaction for item : " + item.ItemName + ". Recheck your request list.";

                                args.IsCancel = true;
                                return;
                            }
                            
                            if (item.IsInventoryItem)
                            {
                                if (item.IsGeneratePrDr ?? false)
                                {
                                    //01. cek distribusi request
                                    var drColl = new ItemTransactionItemCollection();
                                    var drDt = new ItemTransactionItemQuery("a");
                                    var drHd = new ItemTransactionQuery("b");
                                    drDt.InnerJoin(drHd).On(drDt.TransactionNo == drHd.TransactionNo &&
                                                            drHd.TransactionCode == TransactionCode.DistributionRequest &&
                                                            drHd.IsApproved == true);
                                    drDt.Where(drDt.ReferenceNo == item.OrderNo, drDt.ReferenceSequenceNo == item.SeqNo);
                                    drColl.Load(drDt);
                                    if (drColl.Count == 0)
                                    {
                                        args.MessageText = "There is no Distribution Request transaction for item : " + item.ItemName + ". Recheck your request list.";
                                        args.IsCancel = true;
                                        return;
                                    }
                                    foreach (var dr in drColl)
                                    {
                                        //02. cek distribusi
                                        var dColl = new ItemTransactionItemCollection();
                                        var dDt = new ItemTransactionItemQuery("a");
                                        var dHd = new ItemTransactionQuery("b");
                                        dDt.InnerJoin(dHd).On(dDt.TransactionNo == dHd.TransactionNo &&
                                                                dHd.TransactionCode == TransactionCode.Distribution &&
                                                                dHd.IsApproved == true);
                                        dDt.Where(dDt.ReferenceNo == dr.TransactionNo, dDt.ReferenceSequenceNo == dr.SequenceNo);
                                        dColl.Load(dDt);
                                        if (dColl.Count == 0)
                                        {
                                            args.MessageText = "There is no Distribution transaction for item : " + item.ItemName + ". Check back to the central warehouse.";
                                            args.IsCancel = true;
                                            return;
                                        }
                                    }
                                }
                                else if (item.IsGenerateIr ?? false)
                                {
                                    //01. cek inventory issue request
                                    var irColl = new ItemTransactionItemCollection();
                                    var irDt = new ItemTransactionItemQuery("a");
                                    var irHd = new ItemTransactionQuery("b");
                                    irDt.InnerJoin(irHd).On(irDt.TransactionNo == irHd.TransactionNo &&
                                                            irHd.TransactionCode == TransactionCode.InventoryIssueRequestOut &&
                                                            irHd.IsApproved == true);
                                    irDt.Where(irDt.ReferenceNo == item.OrderNo, irDt.ReferenceSequenceNo == item.SeqNo);
                                    irColl.Load(irDt);
                                    if (irColl.Count == 0)
                                    {
                                        args.MessageText = "There is no Inventory Issue Request transaction for item : " + item.ItemName + ". Recheck your request list.";
                                        args.IsCancel = true;
                                        return;
                                    }

                                    foreach (var ir in irColl)
                                    {
                                        //02. cek inventory issue
                                        var dColl = new ItemTransactionItemCollection();
                                        var dDt = new ItemTransactionItemQuery("a");
                                        var dHd = new ItemTransactionQuery("b");
                                        dDt.InnerJoin(dHd).On(dDt.TransactionNo == dHd.TransactionNo &&
                                                                dHd.TransactionCode == TransactionCode.InventoryIssueOut &&
                                                                dHd.IsApproved == true);
                                        dDt.Where(dDt.ReferenceNo == ir.TransactionNo, dDt.ReferenceSequenceNo == ir.SequenceNo);
                                        dColl.Load(dDt);
                                        if (dColl.Count == 0)
                                        {
                                            args.MessageText = "There is no Inventory Issue transaction for item : " + item.ItemName + ". Check back to the central warehouse.";
                                            args.IsCancel = true;
                                            return;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //01. cek purchase request
                                var prColl = new ItemTransactionItemCollection();
                                var prDt = new ItemTransactionItemQuery("a");
                                var prHd = new ItemTransactionQuery("b");
                                prDt.InnerJoin(prHd).On(prDt.TransactionNo == prHd.TransactionNo &&
                                                        prHd.TransactionCode == TransactionCode.PurchaseRequest &&
                                                        prHd.IsApproved == true);
                                prDt.Where(prDt.ReferenceNo == item.OrderNo, prDt.ReferenceSequenceNo == item.SeqNo);
                                prColl.Load(prDt);
                                if (prColl.Count == 0)
                                {
                                    args.MessageText = "There is no Purchase Request transaction for item : " + item.ItemName + ". Recheck your request list.";
                                    args.IsCancel = true;
                                    return;
                                }

                                foreach (var pr in prColl)
                                {
                                    //02. cek purchase order
                                    var poColl = new ItemTransactionItemCollection();
                                    var poDt = new ItemTransactionItemQuery("a");
                                    var poHd = new ItemTransactionQuery("b");
                                    poDt.InnerJoin(poHd).On(poDt.TransactionNo == poHd.TransactionNo &&
                                                            poHd.TransactionCode == TransactionCode.PurchaseOrder &&
                                                            poHd.IsApproved == true);
                                    poDt.Where(poDt.ReferenceNo == pr.TransactionNo, poDt.ReferenceSequenceNo == pr.SequenceNo);
                                    poColl.Load(poDt);
                                    if (poColl.Count == 0)
                                    {
                                        args.MessageText = "There is no Purchase Order transaction for item : " + item.ItemName + ". Check back to the central warehouse.";
                                        args.IsCancel = true;
                                        return;
                                    }

                                    foreach (var po in poColl)
                                    {
                                        //03. cek purchase order receive
                                        var porColl = new ItemTransactionItemCollection();
                                        var porDt = new ItemTransactionItemQuery("a");
                                        var porHd = new ItemTransactionQuery("b");
                                        porDt.InnerJoin(porHd).On(porDt.TransactionNo == porHd.TransactionNo &&
                                                                porHd.TransactionCode == TransactionCode.PurchaseOrderReceive &&
                                                                porHd.IsApproved == true);
                                        porDt.Where(porDt.ReferenceNo == po.TransactionNo, porDt.ReferenceSequenceNo == po.SequenceNo);
                                        porColl.Load(porDt);
                                        if (porColl.Count == 0)
                                        {
                                            args.MessageText = "There is no PO Received transaction for item : " + item.ItemName + ". Check back to the central warehouse.";
                                            args.IsCancel = true;
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            using (var trans = new esTransactionScope())
            {
                btnRefer.Visible = false;

                entity.IsProceed = true;
                if (entity.SRWorkStatus == AppSession.Parameter.WorkStatusThirdParties || entity.SRWorkStatus == AppSession.Parameter.WorkStatusWaitingForParts)
                    entity.SRWorkStatus = AppSession.Parameter.WorkStatusDone;
                entity.LastRealizationDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastRealizationByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.Save();

                // stock calculation
                var chargesBalances = new ItemBalanceCollection();
                var chargesDetailBalances = new ItemBalanceDetailCollection();
                var itemBalanceDetailEds = new ItemBalanceDetailEdCollection();
                var chargesMovements = new ItemMovementCollection();

                string itemNoStock;
                var assetWorkOrderItems = AssetWorkOrderItems.Where(x => x.IsInventoryItem == true && x.IsGenerateIr == false);

                ItemBalance.PrepareItemBalancesForWorkOrderRealization(entity, assetWorkOrderItems, cboToServiceUnitID.SelectedValue, locationId, AppSession.UserLogin.UserID,
                   ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, ref itemBalanceDetailEds, AppSession.Parameter.IsEnabledStockWithEdControl, out itemNoStock);

                if (!string.IsNullOrEmpty(itemNoStock))
                {
                    if (itemNoStock == "x")
                    {
                        var y = false;
                        var itemNoBalance = string.Empty;
                        foreach (var i in assetWorkOrderItems)
                        {
                            if (i.IsInventoryItem)
                            {
                                y = true;
                                itemNoBalance = i.ItemName;
                                break;
                            }

                        }
                        if (y)
                        {
                            args.MessageText = "There is no balance setting in unit : " + cboToServiceUnitID.Text + " for " + itemNoBalance;
                            args.IsCancel = true;
                            return;
                        }
                    }
                    else
                    {
                        args.MessageText = "Insufficient balance of item : " + itemNoStock;
                        args.IsCancel = true;
                        return;
                    }
                }

                if (chargesBalances != null)
                {
                    var loc = new Location();
                    if (loc.LoadByPrimaryKey(locationId) && loc.IsHoldForTransaction == true)
                    {
                        args.MessageText = "Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.";
                        args.IsCancel = true;
                        return;
                    }
                }

                if (chargesBalances != null)
                    chargesBalances.Save();
                if (chargesDetailBalances != null)
                    chargesDetailBalances.Save();
                if (itemBalanceDetailEds != null)
                    itemBalanceDetailEds.Save();
                if (chargesMovements != null)
                    chargesMovements.Save();

                if (AppSession.Parameter.IsWorkOrderRealizationAutoReturn && isLocattionAllowedToStockGoods == false)
                {
                    var su = new ServiceUnit();
                    su.LoadByPrimaryKey(entity.ToServiceUnitID);

                    var distDtColl = new ItemTransactionItemCollection();

                    var items = grdItem.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => new
                    {
                        IsInventoryItem = ((CheckBox)dataItem.FindControl("chkIsInventoryItem")).Checked,
                        IsGeneratePrDr = ((CheckBox)dataItem.FindControl("chkIsGeneratePrDr")).Checked,
                        IsGenerateIr = ((CheckBox)dataItem.FindControl("chkIsGenerateIr")).Checked,
                        TransactionNo = dataItem["TransactionNoPrDr"].Text,
                        ItemID = dataItem["ItemID"].Text,
                        RemainingQty = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQty")).Value ?? 0) - Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyRealization")).Value ?? 0)
                    }).Where(dataItem => dataItem.IsInventoryItem && dataItem.TransactionNo != string.Empty && dataItem.RemainingQty > 0 && dataItem.IsGenerateIr == false);

                    var itemGroups = items.GroupBy(c => new
                    {
                        c.IsInventoryItem,
                        c.ItemID
                    }).Select(q => new
                    {
                        q.Key.IsInventoryItem,
                        q.Key.ItemID,
                        RemainingQty = q.Sum(p => (p.RemainingQty))
                    });

                    foreach (var group in (from g in itemGroups
                                           group g by new
                                           {
                                               g.IsInventoryItem
                                           }
                                               into grp
                                           orderby grp.Key.IsInventoryItem
                                           select new
                                           {
                                               IsInventoryItem = grp.Key.IsInventoryItem
                                           }))
                    {
                        _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, TransactionCode.Distribution, su.DepartmentID);

                        foreach (var i in itemGroups.Where(i => i.IsInventoryItem == group.IsInventoryItem))
                        {
                            var c = distDtColl.AddNew();

                            #region detail

                            c.TransactionNo = _autoNumber.LastCompleteNumber;
                            c.ItemID = i.ItemID;
                            c.SequenceNo = string.Format("{0:000}", distDtColl.Count + 1);
                            c.Quantity = i.RemainingQty;

                            var item = new Item();
                            item.LoadByPrimaryKey(c.ItemID);
                            c.Description = item.ItemName;

                            var ipm = new ItemProductNonMedic();
                            ipm.LoadByPrimaryKey(c.ItemID);
                            c.SRItemUnit = ipm.SRItemUnit;
                            c.ConversionFactor = 1;
                            c.QuantityFinishInBaseUnit = 0;
                            c.PageNo = 0;

                            c.Price = ipm.PriceInBaseUnit;
                            c.PriceInCurrency = ipm.PriceInBaseUnit;

                            if (i.IsInventoryItem)
                            {
                                c.CostPrice = ipm.CostPrice;
                                c.IsDiscountInPercent = false;
                                c.Discount1Percentage = 0;
                                c.Discount2Percentage = 0;
                            }
                            else
                            {
                                c.CostPrice = 0;
                                c.IsDiscountInPercent = true;
                                c.Discount1Percentage = c.Discount1Percentage ?? 0;
                                c.Discount2Percentage = c.Discount2Percentage ?? 0;
                            }

                            decimal disc1 = Convert.ToDecimal(c.Price * c.Discount1Percentage / 100);
                            decimal disc2 = Convert.ToDecimal((c.Price - disc1) * c.Discount2Percentage / 100);
                            c.Discount = disc1 + disc2;
                            c.DiscountInCurrency = c.Discount;
                            c.IsBonusItem = false;
                            c.IsClosed = false;
                            c.Total = (c.Price - c.Discount) * c.Quantity;
                            c.BatchNumber = string.Empty;
                            c.str.ExpiredDate = string.Empty;
                            c.IsPackage = false;
                            c.Specification = string.Empty;

                            c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            c.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                            #endregion
                        }

                        var distHd = new ItemTransaction();

                        #region header

                        distHd.TransactionNo = _autoNumber.LastCompleteNumber;
                        distHd.TransactionCode = TransactionCode.Distribution;
                        distHd.TransactionDate = (new DateTime()).NowAtSqlServer().Date;
                        distHd.SRItemType = ItemType.NonMedical;

                        distHd.FromServiceUnitID = cboToServiceUnitID.SelectedValue;
                        distHd.FromLocationID = unit.GetMainLocationId(distHd.str.FromServiceUnitID);

                        distHd.ToServiceUnitID = AppSession.Parameter.MainPurchasingUnitIDForNonMedical;
                        distHd.ToLocationID = unit.GetMainLocationId(distHd.str.ToServiceUnitID);

                        distHd.ServiceUnitCostID = cboFromServiceUnitID.SelectedValue;
                        distHd.Notes = string.Empty;
                        distHd.IsBySystem = true;
                        distHd.IsInventoryItem = group.IsInventoryItem;
                        distHd.ReferenceNo = txtOrderNo.Text;

                        distHd.IsApproved = true;
                        distHd.ApprovedDate = (new DateTime()).NowAtSqlServer().Date;
                        distHd.ApprovedByUserID = AppSession.UserLogin.UserID;
                        distHd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        distHd.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        #endregion

                        _autoNumber.Save();
                        distHd.Save();
                        distDtColl.Save();

                        var reference = new ItemTransaction();
                        var referenceItems = new ItemTransactionItemCollection();
                        chargesBalances = new ItemBalanceCollection();
                        var chargesBalancesTo = new ItemBalanceCollection();
                        chargesDetailBalances = new ItemBalanceDetailCollection();
                        chargesMovements = new ItemMovementCollection();
                        var itemHistory = new ItemTransactionItemHistoryCollection();
                        ItemBalanceDetailEdCollection itemBalanceDetailEd = null;

                        string itemNoStock2;

                        ItemBalance.PrepareItemBalancesForDistribution(distDtColl, distHd.FromServiceUnitID,
                                                                       distHd.FromLocationID, distHd.ToLocationID,
                                                                       AppSession.UserLogin.UserID, string.Empty,
                                                                       ref reference, ref referenceItems,
                                                                       ref chargesBalances, ref chargesBalancesTo,
                                                                       ref chargesDetailBalances, ref chargesMovements,
                                                                       ref itemHistory, ref itemBalanceDetailEd,
                                                                       out itemNoStock2, AppSession.Parameter.IsEnabledStockWithEdControl);

                        if (!string.IsNullOrEmpty(itemNoStock2))
                        {
                            args.MessageText = "Insufficient balance of item : " + itemNoStock2;
                            args.IsCancel = true;
                            return;
                        }

                        if (chargesBalances != null)
                            chargesBalances.Save();
                        if (chargesBalancesTo != null)
                            chargesBalancesTo.Save();
                        if (chargesDetailBalances != null)
                            chargesDetailBalances.Save();
                        if (itemBalanceDetailEd != null)
                            itemBalanceDetailEd.Save();
                        if (chargesMovements != null)
                            chargesMovements.Save();
                        if (itemHistory != null)
                            itemHistory.Save();

                        if (AppSession.Parameter.IsDistributionAutoConfirm)
                        {
                            chargesBalances = new ItemBalanceCollection();

                            var chargesMovementsTo = new ItemMovementCollection();
                            var chargesDetailBalancesTo = new ItemBalanceDetailCollection();
                            var itemBalanceDetailEdTo = new ItemBalanceDetailEdCollection();

                            ItemBalance.PrepareItemBalancesForAutoDistribution(distDtColl, distHd, AppSession.UserLogin.UserID,
                                  ref chargesBalances, ref chargesDetailBalancesTo, ref chargesMovementsTo, ref chargesMovements, ref itemBalanceDetailEdTo, AppSession.Parameter.IsEnabledStockWithEdControl);

                            if (chargesBalances != null)
                                chargesBalances.Save();
                            if (chargesDetailBalancesTo != null)
                                chargesDetailBalancesTo.Save();
                            if (itemBalanceDetailEdTo != null)
                                itemBalanceDetailEdTo.Save();
                            if (chargesMovementsTo != null)
                                chargesMovementsTo.Save();

                        }
                    }
                }

                if (entity.IsPreventiveMaintenance ?? false)
                {
                    var asset = new Asset();
                    if (asset.LoadByPrimaryKey(entity.AssetID))
                    {
                        asset.LastMaintenanceDate = entity.LastRealizationDateTime.Value.Date;
                        asset.NextMaintenanceDate = entity.LastRealizationDateTime.Value.Date.AddDays(Convert.ToInt32(asset.MaintenanceInterval));
                        asset.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        asset.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        asset.Save();
                    }
                }

                //Commit if success, Rollback if failed
                var app = new AppParameter();
                app.LoadByPrimaryKey("acc_IsAutoJournalInvIssue");
                if (app.ParameterValue == "Yes")
                {
                    var awois = (AssetWorkOrderItems.Where(b => b.QuantityRealization > 0 && b.IsInventoryItem == true && b.IsGenerateIr == false));
                    if (awois.Count() > 0)
                    {
                        /* Automatic Journal Testing Start */

                        var closingperiod = DateTime.Now;
                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod);
                        if (isClosingPeriod)
                        {
                            args.MessageText = "Financial statements for period: " +
                                               string.Format("{0:MMMM-yyyy}", closingperiod) +
                                               " have been closed. Please contact the authorities.";
                            args.IsCancel = true;
                            return;
                        }

                        int? journalId = JournalTransactions.AddNewInventoryIssueFromWorkOrderJournal(entity, AppSession.UserLogin.UserID, 0);

                        /* Automatic Journal Testing End */
                    }
                }

                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            if (AssetWorkOrderItems.Count > 0)
            {
                args.MessageText = "There is materials used for this work order.";
                args.IsCancel = true;
                return;
            }

            if (getPageID != "")
            {
                var result = new SanitationActivityResult();
                if (result.LoadByPrimaryKey(txtOrderNo.Text))
                {
                    args.MessageText = "Work order already have result.";
                    args.IsCancel = true;
                    return;
                }
            }

            var entity = new AssetWorkOrder();
            entity.LoadByPrimaryKey(txtOrderNo.Text);

            if (entity.SRWorkStatus == AppSession.Parameter.WorkStatusThirdParties && !string.IsNullOrEmpty(entity.SentToThirdPartiesByUserID))
            {
                args.MessageText = "Work order has been processed for services to third parties.";
                args.IsCancel = true;
                return;
            }

            if (!string.IsNullOrEmpty(entity.AcceptedByUserID))
            {
                args.MessageText = "Work order already closed.";
                args.IsCancel = true;
                return;
            }

            entity.IsProceed = false;
            entity.LastRealizationDateTime = (new DateTime()).NowAtSqlServer();
            entity.LastRealizationByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.Save();
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AssetWorkOrder());

            txtOrderDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            cboFromServiceUnitID.Text = string.Empty;
            cboToServiceUnitID.Text = string.Empty;

            ViewState["IsApproved"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboSRWorkTradeItem.SelectedValue))
            {
                var wti = new AppStandardReferenceItemCollection();
                wti.Query.Where(wti.Query.StandardReferenceID == "WorkTradeItem", wti.Query.IsActive == true, wti.Query.ReferenceID == cboSRWorkTrade.SelectedValue);
                wti.LoadAll();
                if (wti.Count > 0)
                {
                    args.MessageText = "Work Order Trade Item required.";
                    args.IsCancel = true;
                    return;
                }
            }

            var entity = new AssetWorkOrder();
            if (entity.LoadByPrimaryKey(txtOrderNo.Text))
            {
                SetEntityValue(entity);
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

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_OrderNo", txtOrderNo.Text);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("OrderNo='{0}'", txtOrderNo.Text.Trim());
            auditLogFilter.TableName = "AssetWorkOrder";
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtOrderNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            btnRefer.Visible = getPageID == "" && (newVal == AppEnum.DataMode.Read) && (bool)ViewState["IsApproved"] == false;

            RefreshCommandItemGrid(oldVal, newVal);
            RefreshCommandItemGridImplementer(oldVal, newVal);

            rbGenerateList.Enabled = (newVal == AppEnum.DataMode.Read);
            cboGenerateToServiceUnitID.Enabled = (newVal == AppEnum.DataMode.Read);
            lbtnGenerate.Visible = (newVal == AppEnum.DataMode.Read);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AssetWorkOrder();
            if (parameters.Length > 0)
            {
                String orderNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(orderNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtOrderNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var wo = (AssetWorkOrder)entity;
            txtOrderNo.Text = wo.OrderNo;
            txtOrderDate.SelectedDate = wo.OrderDate;
            cboFromServiceUnitID.SelectedValue = wo.FromServiceUnitID;
            cboToServiceUnitID.SelectedValue = wo.ToServiceUnitID;
            txtRequiredDate.SelectedDate = wo.RequiredDate;
            if (!string.IsNullOrEmpty(wo.SRWorkStatus))
            {
                var ws = new AppStandardReferenceItemQuery();
                ws.Where(ws.StandardReferenceID == AppEnum.StandardReference.WorkStatus.ToString(),
                         ws.ItemID == wo.SRWorkStatus);
                cboSRWorkStatus.DataSource = ws.LoadDataTable();
                cboSRWorkStatus.DataBind();

                cboSRWorkStatus.SelectedValue = wo.SRWorkStatus;
            }
            else
            {
                var ws = new AppStandardReferenceItemQuery();
                ws.Where(ws.StandardReferenceID == AppEnum.StandardReference.WorkStatus.ToString(),
                         ws.ItemID == AppSession.Parameter.WorkStatusOpen);
                cboSRWorkStatus.DataSource = ws.LoadDataTable();
                cboSRWorkStatus.DataBind();

                cboSRWorkStatus.SelectedValue = AppSession.Parameter.WorkStatusOpen;
            }
            if (!string.IsNullOrEmpty(wo.SRWorkType))
            {
                var ws = new AppStandardReferenceItemQuery();
                ws.Where(ws.StandardReferenceID == AppEnum.StandardReference.WorkType.ToString(),
                         ws.ItemID == wo.SRWorkType);
                cboSRWorkType.DataSource = ws.LoadDataTable();
                cboSRWorkType.DataBind();

                cboSRWorkType.SelectedValue = wo.SRWorkType;
            }
            else
            {
                cboSRWorkType.Items.Clear();
                cboSRWorkType.Text = string.Empty;
            }
            cboSRWorkPriority.SelectedValue = wo.SRWorkPriority;
            cboSRWorkTrade.SelectedValue = wo.SRWorkTrade;
            ComboBox.PopulateWorkTradeItemList(cboSRWorkTradeItem, cboSRWorkTrade.SelectedValue, false);
            cboSRWorkTradeItem.SelectedValue = wo.SRWorkTradeItem;
            txtProblemDescription.Text = wo.ProblemDescription;
            chkIsPreventiveMaintenance.Checked = wo.IsPreventiveMaintenance ?? false;
            txtPMNo.Text = wo.PMNo;

            var usr = new AppUser();
            usr.LoadByPrimaryKey(wo.RequestByUserID);
            txtRequestByUserID.Text = usr.UserName;
            txtAssetID.Text = wo.AssetID;
            PopulateAssetInfo(txtAssetID.Text);

            if (!string.IsNullOrEmpty(wo.ItemID))
            {
                var iq = new ItemQuery();
                iq.Where(iq.ItemID == wo.ItemID);
                cboItemID.DataSource = iq.LoadDataTable();
                cboItemID.DataBind();
                cboItemID.SelectedValue = wo.ItemID;
            }
            else
            {
                cboItemID.Items.Clear();
                cboItemID.Text = string.Empty;
            }
            txtQty.Value = Convert.ToDouble(wo.Qty);

            if (!string.IsNullOrEmpty(wo.ReceivedByUserID))
            {
                txtReceivedDate.SelectedDate = wo.ReceivedDateTime.Value.Date;
                txtReceivedTime.Text = wo.ReceivedDateTime.Value.ToString("HH:mm");
                txtReceivedByUserID.Text = wo.ReceivedByUserID;
                usr = new AppUser();
                usr.LoadByPrimaryKey(wo.ReceivedByUserID);
                txtReceivedBy.Text = usr.UserName;
            }
            else
            {
                //txtReceivedDate.SelectedDate = null;
                //txtReceivedTime.Text = "00:00";
                txtReceivedDate.SelectedDate = (new DateTime()).NowAtSqlServer().Date;
                txtReceivedTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
                txtReceivedByUserID.Text = string.Empty;
                txtReceivedBy.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(wo.FirstResponseByUserID))
            {
                txtFirstResponseDate.SelectedDate = wo.FirstResponseDateTime.Value.Date;
                txtFirstResponseTime.Text = wo.FirstResponseDateTime.Value.ToString("HH:mm");
                txtFirstResponseByUserID.Text = wo.FirstResponseByUserID;
                usr = new AppUser();
                usr.LoadByPrimaryKey(wo.FirstResponseByUserID);
                txtFirstResponseByUserName.Text = usr.UserName;
            }
            else
            {
                txtFirstResponseDate.SelectedDate = null;
                txtFirstResponseTime.Text = "00:00";
                txtFirstResponseByUserID.Text = string.Empty;
                txtFirstResponseByUserName.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(wo.LastRealizationByUserID))
            {
                txtLastRealizationDate.SelectedDate = wo.LastRealizationDateTime.Value.Date;
                txtLastRealizationTime.Text = wo.LastRealizationDateTime.Value.ToString("HH:mm");
                txtLastRealizationByUserID.Text = wo.LastRealizationByUserID;
                usr = new AppUser();
                usr.LoadByPrimaryKey(wo.LastRealizationByUserID);
                txtLastRealizationBy.Text = usr.UserName;
            }
            else
            {
                txtLastRealizationDate.SelectedDate = null;
                txtLastRealizationTime.Text = "00:00";
                txtLastRealizationByUserID.Text = string.Empty;
                txtLastRealizationBy.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(wo.AcceptedByUserID))
            {
                txtAcceptedDate.SelectedDate = wo.AcceptedDateTime.Value.Date;
                txtAcceptedTime.Text = wo.AcceptedDateTime.Value.ToString("HH:mm");
                usr = new AppUser();
                usr.LoadByPrimaryKey(wo.AcceptedByUserID);
                txtAcceptedByUserID.Text = usr.UserName;
            }
            else
            {
                txtAcceptedDate.SelectedDate = null;
                txtAcceptedTime.Text = "00:00";
                txtAcceptedByUserID.Text = string.Empty;
            }

            cboSRFailureCode.SelectedValue = wo.SRFailureCode;
            txtFailureCauseDescription.Text = wo.FailureCauseDescription;
            txtActionTaken.Text = wo.ActionTaken;
            txtPreventionTaken.Text = wo.PreventionTaken;
            txtCostEstimation.Value = Convert.ToDouble(wo.CostEstimation);

            txtAppovedDate.SelectedDate = wo.ApprovedDateTime.Value.Date;
            txtApprovedTime.Text = wo.ApprovedDateTime.Value.ToString("HH:mm");

            txtAcceptedBy.Text = wo.AcceptedBy;
            txtReferenceNo.Text = wo.ReferenceNo;

            //Display Data Detail
            PopulateGridDetail();
            PopulateGridImplementerDetail();

            //if (!string.IsNullOrEmpty(wo.ReferenceNo))
            //    tabStrip.Tabs[2].Visible = false;//tab material used

            ViewState["IsApproved"] = wo.IsProceed ?? false;
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(AssetWorkOrder entity)
        {
            entity.OrderNo = txtOrderNo.Text;
            entity.SRWorkStatus = cboSRWorkStatus.SelectedValue;
            if (cboSRWorkStatus.SelectedValue == AppSession.Parameter.WorkStatusThirdParties && string.IsNullOrEmpty(entity.ReceivedFromLogisticsByUserID))
            {
                entity.ReceivedFromLogisticsDateTime = (new DateTime()).NowAtSqlServer();
                entity.ReceivedFromLogisticsByUserID = AppSession.UserLogin.UserID;
            }
            entity.SRWorkTrade = cboSRWorkTrade.SelectedValue;
            entity.SRWorkTradeItem = cboSRWorkTradeItem.SelectedValue;
            if (string.IsNullOrEmpty(entity.ReceivedByUserID))
            {
                entity.ReceivedDateTime = (new DateTime()).NowAtSqlServer();
                entity.ReceivedByUserID = AppSession.UserLogin.UserID;
            }
            else if (string.IsNullOrEmpty(entity.FirstResponseByUserID))
            {
                entity.FirstResponseDateTime = (new DateTime()).NowAtSqlServer();
                entity.FirstResponseByUserID = AppSession.UserLogin.UserID;
            }

            entity.SRFailureCode = cboSRFailureCode.SelectedValue;
            entity.FailureCauseDescription = txtFailureCauseDescription.Text;
            entity.ActionTaken = txtActionTaken.Text;
            entity.PreventionTaken = txtPreventionTaken.Text;
            entity.CostEstimation = Convert.ToDecimal(txtCostEstimation.Value);
            entity.LastRealizationByUserID = AppSession.UserLogin.UserID;
            entity.LastRealizationDateTime = (new DateTime()).NowAtSqlServer();
            entity.IsProceed = false;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            //Update Detil
            foreach (AssetWorkOrderItem item in AssetWorkOrderItems)
            {
                item.OrderNo = txtOrderNo.Text;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            foreach (AssetWorkOrderImplementer item in AssetWorkOrderImplementers)
            {
                item.OrderNo = txtOrderNo.Text;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
        }

        private void SaveEntity(AssetWorkOrder entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                AssetWorkOrderItems.Save();
                AssetWorkOrderImplementers.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AssetWorkOrderQuery("a");
            var user = new AppUserServiceUnitQuery("b");
            que.InnerJoin(user).On(que.ToServiceUnitID == user.ServiceUnitID && user.UserID == AppSession.UserLogin.UserID);

            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.OrderNo > txtOrderNo.Text);
                que.OrderBy(que.OrderNo.Ascending);
            }
            else
            {
                que.Where(que.OrderNo < txtOrderNo.Text);
                que.OrderBy(que.OrderNo.Descending);
            }

            var entity = new AssetWorkOrder();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[1].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            RadToolBar2.Visible = !isVisible && !AppSession.Parameter.IsUsingCentralizedPurchaseRequest;
            pnlGenerateRequest.Visible = !isVisible && AppSession.Parameter.IsUsingCentralizedPurchaseRequest;
            grdItem.Columns[2].Visible = AppSession.Parameter.WorkOrderRealizationAutoGenerateTx == "PR" && !AppSession.Parameter.IsUsingCentralizedPurchaseRequest;
            grdItem.Columns[7].Visible = grdItem.Columns[2].Visible;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                AssetWorkOrderItems = null;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private AssetWorkOrderItemCollection AssetWorkOrderItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collAssetWorkOrderItem" + Request.UserHostName];
                    if (obj != null)
                        return ((AssetWorkOrderItemCollection)(obj));
                }

                var coll = new AssetWorkOrderItemCollection();

                var query = new AssetWorkOrderItemQuery("a");
                var nm = new ItemProductNonMedicQuery("b");
                var tx = new VwItemTransactionItemPrDrFromWoQuery("c");
                var iq = new ItemQuery("d");

                query.LeftJoin(nm).On(query.ItemID == nm.ItemID);
                query.LeftJoin(tx).On(query.OrderNo == tx.ReferenceNo && query.SeqNo == tx.ReferenceSequenceNo);
                query.LeftJoin(iq).On(iq.ItemID == query.ItemID);
                query.Select(
                    query,
                    @"<ISNULL(b.IsInventoryItem, 0) AS 'refToItemProductNonMedic_IsInventoryItem'>",
                    @"<CASE WHEN a.IsGeneratePrDr = 1 OR a.IsGenerateIr = 1 THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'refTo_IsEnabledGeneratePrDr'>",
                    @"<CASE WHEN a.IsGeneratePrDr = 1 OR a.IsGenerateIr = 1 THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'refTo_IsChecklistGeneratePrDr'>",
                    @"<ISNULL(c.TransactionNo, '') AS 'refToItemTransactionItem_TransactionNo'>"
                    );

                query.Where(query.OrderNo == txtOrderNo.Text);

                query.OrderBy(query.SeqNo.Ascending);

                coll.Load(query);

                Session["collAssetWorkOrderItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collAssetWorkOrderItem" + Request.UserHostName] = value; }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            AssetWorkOrderItems = null; //Reset Record Detail
            grdItem.DataSource = AssetWorkOrderItems;
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = AssetWorkOrderItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][AssetWorkOrderItemMetadata.ColumnNames.SeqNo]);
            AssetWorkOrderItem entity = FindItem(sequenceNo);
            if (entity != null)
                SetEntityValue(entity, e);

            CalculateDetailMaterialUsed();
        }

        private AssetWorkOrderItem FindItem(String sequenceNo)
        {
            AssetWorkOrderItemCollection coll = AssetWorkOrderItems;
            return coll.FirstOrDefault(rec => rec.SeqNo.Equals(sequenceNo));
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][AssetWorkOrderItemMetadata.ColumnNames.SeqNo]);
            AssetWorkOrderItem entity = FindItem(sequenceNo);

            if (entity != null)
            {
                if (entity.IsGeneratePrDr == false && entity.IsGenerateIr == false)
                {
                    entity.MarkAsDeleted();
                }
                else
                {
                    var dt = new ItemTransactionItemQuery("a");
                    var hd = new ItemTransactionQuery("b");
                    dt.InnerJoin(hd).On(dt.TransactionNo == hd.TransactionNo && hd.IsVoid == false);
                    dt.Where(dt.ReferenceNo == txtOrderNo.Text, dt.ReferenceSequenceNo == sequenceNo);
                    DataTable dtb = dt.LoadDataTable();
                    if (dtb.Rows.Count == 0)
                        entity.MarkAsDeleted();
                }
            }

            //if (entity != null && entity.IsGeneratePrDr == false)
            //    entity.MarkAsDeleted();

            CalculateDetailMaterialUsed();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            AssetWorkOrderItem entity = AssetWorkOrderItems.AddNew();
            SetEntityValue(entity, e);

            CalculateDetailMaterialUsed();

            //grid not close first
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(AssetWorkOrderItem entity, GridCommandEventArgs e)
        {
            var userControl = (WorkOrderRealizationItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SeqNo = userControl.SequenceNo;
                entity.IsMasterItem = userControl.IsMasterItem;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Quantity = userControl.Quantity;
                entity.QuantityRealization = userControl.QuantityRealization;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ConversionFactor = userControl.ConversionFactor;
                entity.CostPrice = userControl.CostPrice;
                entity.Price = userControl.Price;
                entity.IsInventoryItem = userControl.IsInventoryItem;
                entity.IsGeneratePrDr = userControl.IsGeneratePrDr;
                entity.IsGenerateIr = userControl.IsGenerateIr;
                if (userControl.IsGeneratePrDr || userControl.IsGenerateIr)
                {
                    entity.IsEnabledGeneratePrDr = false;
                    entity.IsChecklistGeneratePrDr = false;
                }
                else
                {
                    entity.IsEnabledGeneratePrDr = true;
                    entity.IsChecklistGeneratePrDr = true;
                }
                entity.Specification = userControl.Specification;
            }
        }

        #endregion

        #region Purchase/Distribution Request
        protected void grdRequestList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdRequestList.DataSource = ItemTransactions;
        }

        protected void grdRequestList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            //Load record
            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");

            query.Select
                (
                    query.TransactionNo,
                    query.ItemID,
                    query.SRItemUnit,
                    query.Quantity,
                    query.QuantityFinishInBaseUnit,
                    query.SequenceNo,
                    query.IsClosed,
                    query.Description,
                    iq.ItemName.As("ItemName")
                );
            query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
            query.Where(query.TransactionNo == e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString());
            query.OrderBy(query.ItemID.Ascending);

            //Apply
            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        private DataTable ItemTransactions
        {
            get
            {
                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("b");
                var qryserviceunitto = new ServiceUnitQuery("c");
                var itemtype = new AppStandardReferenceItemQuery("d");
                var user = new AppUserServiceUnitQuery("e");

                query.Select
                    (
                        query.TransactionNo,
                        query.TransactionDate,
                        qryserviceunit.ServiceUnitName.As("FServiceUnitID"),
                        qryserviceunitto.ServiceUnitName.As("TServiceUnitID"),
                        itemtype.ItemName,
                        query.IsInventoryItem,
                        query.IsApproved,
                        query.IsClosed,
                        query.Notes,
                        query.IsVoid
                    );

                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                query.InnerJoin(qryserviceunitto).On(qryserviceunitto.ServiceUnitID == query.ToServiceUnitID);
                query.LeftJoin(itemtype).On
                    (
                        itemtype.ItemID == query.SRItemType &&
                        itemtype.StandardReferenceID == "ItemType"
                    );
                query.InnerJoin(user).On(user.ServiceUnitID == query.FromServiceUnitID & user.UserID == AppSession.UserLogin.UserID);
                query.Where
                    (
                    query.Or
                        (
                        query.TransactionCode == TransactionCode.PurchaseRequest,
                        query.TransactionCode == TransactionCode.DistributionRequest,
                        query.TransactionCode == TransactionCode.InventoryIssueRequestOut
                        ),
                    query.ReferenceNo == txtOrderNo.Text
                    );
                query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }
        #endregion

        #region Work Order Implementer
        private void RefreshCommandItemGridImplementer(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdImplementer.Columns[0].Visible = isVisible;
            grdImplementer.Columns[grdImplementer.Columns.Count - 1].Visible = isVisible;

            grdImplementer.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                AssetWorkOrderImplementers = null;

            //Perbaharui tampilan dan data

            grdImplementer.Rebind();
        }

        private AssetWorkOrderImplementerCollection AssetWorkOrderImplementers
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collAssetWorkOrderImplementer" + Request.UserHostName];
                    if (obj != null)
                        return ((AssetWorkOrderImplementerCollection)(obj));
                }

                var coll = new AssetWorkOrderImplementerCollection();

                var query = new AssetWorkOrderImplementerQuery("a");
                var usr = new AppUserQuery("b");
                query.InnerJoin(usr).On(query.UserID == usr.UserID);

                query.Select(
                    query,
                    usr.UserName.As("refToAppUser_UserName")
                    );

                query.Where(query.OrderNo == txtOrderNo.Text);

                query.OrderBy(query.UserID.Ascending);

                coll.Load(query);

                Session["collAssetWorkOrderImplementer" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collAssetWorkOrderImplementer" + Request.UserHostName] = value; }
        }

        private void PopulateGridImplementerDetail()
        {
            //Display Data Detail
            AssetWorkOrderImplementers = null; //Reset Record Detail
            grdImplementer.DataSource = AssetWorkOrderImplementers;
            grdImplementer.MasterTableView.IsItemInserted = false;
            grdImplementer.MasterTableView.ClearEditItems();
            grdImplementer.DataBind();
        }

        protected void grdImplementer_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdImplementer.DataSource = AssetWorkOrderImplementers;
        }

        protected void grdImplementer_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String userId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][AssetWorkOrderImplementerMetadata.ColumnNames.UserID]);
            AssetWorkOrderImplementer entity = FindImplementer(userId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        private AssetWorkOrderImplementer FindImplementer(String userId)
        {
            AssetWorkOrderImplementerCollection coll = AssetWorkOrderImplementers;
            return coll.FirstOrDefault(rec => rec.UserID.Equals(userId));
        }

        protected void grdImplementer_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String userId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][AssetWorkOrderImplementerMetadata.ColumnNames.UserID]);
            AssetWorkOrderImplementer entity = FindImplementer(userId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdImplementer_InsertCommand(object source, GridCommandEventArgs e)
        {
            AssetWorkOrderImplementer entity = AssetWorkOrderImplementers.AddNew();
            SetEntityValue(entity, e);

            //grid not close first
            e.Canceled = true;
            grdImplementer.Rebind();
        }

        private void SetEntityValue(AssetWorkOrderImplementer entity, GridCommandEventArgs e)
        {
            var userControl = (WorkOrderRealizationImplementerDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.UserID = userControl.UserID;
                entity.UserName = userControl.UserName;
                entity.Notes = userControl.Notes;
            }
        }
        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuAdd.Enabled = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"];

            if (RadToolBar2.Visible)
                RadToolBar2.Visible = !(bool)ViewState["IsApproved"];

            if (pnlGenerateRequest.Visible)
                pnlGenerateRequest.Visible = !(bool)ViewState["IsApproved"];
        }

        private void PopulateAssetInfo(string assetId)
        {
            var asset = new BusinessObject.Asset();
            if (asset.LoadByPrimaryKey(assetId))
            {
                lblAssetName.Text = asset.AssetName;
                txtBrandName.Text = asset.BrandName;
                txtSerialNo.Text = asset.SerialNumber;

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(asset.ServiceUnitID);
                var room = new ServiceRoom();
                if (room.LoadByPrimaryKey(asset.AssetLocationID))
                {
                    if (!string.IsNullOrEmpty(room.RoomName))
                        txtLocationName.Text = unit.ServiceUnitName + " - " + room.RoomName;
                    else
                        txtLocationName.Text = unit.ServiceUnitName;
                }
                else
                    txtLocationName.Text = unit.ServiceUnitName;

                cboSRAssetsStatus.SelectedValue = asset.SRAssetsStatus;
                txtAssetNotes.Text = asset.Notes;
                txtNotesToTechnician.Text = asset.NotesToTechnician;
                cboSRAssetsWarrantyContract.SelectedValue = asset.SRAssetsWarrantyContract;
                txtGuaranteeExpiredDate.SelectedDate = asset.GuaranteeExpiredDate;
            }
            else
            {
                lblAssetName.Text = string.Empty;
                txtBrandName.Text = string.Empty;
                txtSerialNo.Text = string.Empty;
                txtLocationName.Text = string.Empty;
                cboSRAssetsStatus.SelectedValue = string.Empty;
                txtAssetNotes.Text = string.Empty;
                txtNotesToTechnician.Text = string.Empty;
                cboSRAssetsWarrantyContract.SelectedValue = string.Empty;
                txtGuaranteeExpiredDate.SelectedDate = null;
            }
        }

        private void CalculateDetailMaterialUsed()
        {
            if (AssetWorkOrderItems.Count > 0)
            {
                decimal? total = AssetWorkOrderItems.Aggregate<AssetWorkOrderItem, decimal?>(0, (current, item) => current + (item.Price * item.QuantityRealization));

                txtCostEstimation.Value = Convert.ToDouble(total);
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument.Contains("generate")) //(eventArgument == "generate")
            {
                var _txCode = eventArgument == "generate" ? "PR" : (eventArgument == "generateDR" ? "DR" : "IR");

                pnlInfo.Visible = false;

                bool _IsDistReqOrPurcReqUsingBudgetPlan = AppSession.Parameter.IsDistReqOrPurcReqUsingBudgetPlan;

                var su = new ServiceUnit();
                if (su.LoadByPrimaryKey(cboToServiceUnitID.SelectedValue))
                {
                    var transNos = string.Empty;

                    using (var trans = new esTransactionScope())
                    {
                        var coll = new ItemTransactionItemCollection();

                        var items = grdItem.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("generateChkbox")).Checked)
                                                                                      .Select(dataItem => new
                                                                                      {
                                                                                          SeqNo = dataItem["SeqNo"].Text,
                                                                                          ItemID = dataItem["ItemID"].Text,
                                                                                          ItemName = dataItem["ItemName"].Text,
                                                                                          Specification = dataItem["Specification"].Text,
                                                                                          SRItemUnit = dataItem["SRItemUnit"].Text,
                                                                                          IsInventoryItem = ((CheckBox)dataItem.FindControl("chkIsInventoryItem")).Checked,
                                                                                          IsMasterItem = ((CheckBox)dataItem.FindControl("chkIsMasterItem")).Checked,
                                                                                          Quantity = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQty")).Value ?? 0)
                                                                                      });

                        foreach (var group in (from g in items
                                               group g by new
                                               {
                                                   g.IsInventoryItem,
                                                   g.IsMasterItem
                                               }
                                                   into grp
                                               orderby grp.Key.IsInventoryItem
                                               select new
                                               {
                                                   IsInventoryItem = grp.Key.IsInventoryItem,
                                                   IsMasterItem = grp.Key.IsMasterItem
                                               }))
                        {
                            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date,
                                                                     group.IsInventoryItem
                                                                         ? (_txCode == "IR" ? TransactionCode.InventoryIssueRequestOut : TransactionCode.DistributionRequest)
                                                                         : TransactionCode.PurchaseRequest,
                                                                     su.DepartmentID);

                            foreach (var i in items.Where(i => i.IsInventoryItem == group.IsInventoryItem && i.IsMasterItem == group.IsMasterItem))
                            {
                                var c = coll.AddNew();

                                #region detail

                                c.TransactionNo = _autoNumber.LastCompleteNumber;
                                c.ItemID = i.IsMasterItem ? i.ItemID : string.Empty;
                                c.SequenceNo = string.Format("{0:000}", coll.Count + 1);
                                c.ReferenceNo = txtOrderNo.Text;
                                c.ReferenceSequenceNo = i.SeqNo;
                                if (i.IsInventoryItem)
                                {
                                    if (_IsDistReqOrPurcReqUsingBudgetPlan)
                                    {
                                        var toUnit = AppSession.Parameter.MainPurchasingUnitIDForNonMedical;
                                        var iti = new ItemTransactionItem();
                                        decimal qtyBp = iti.GetCountBudgetPlan(cboToServiceUnitID.SelectedValue, toUnit, i.ItemID,
                                                                               (new DateTime()).NowAtSqlServer().Year, "");
                                        decimal qtyOffered = iti.GetCountBudgetPlanRealization(toUnit, cboToServiceUnitID.SelectedValue, i.ItemID,
                                                                       (new DateTime()).NowAtSqlServer().Year, "", false);
                                        decimal qtyBalance = qtyBp - qtyOffered;

                                        c.Quantity = i.Quantity > qtyBalance ? qtyBalance : i.Quantity;
                                    }
                                    else
                                        c.Quantity = i.Quantity;
                                }
                                else
                                    c.Quantity = i.Quantity;

                                if (i.IsMasterItem)
                                {
                                    var item = new Item();
                                    item.LoadByPrimaryKey(c.ItemID);
                                    c.Description = item.ItemName;

                                    var ipm = new ItemProductNonMedic();
                                    ipm.LoadByPrimaryKey(c.ItemID);
                                    c.SRItemUnit = ipm.SRItemUnit;

                                    c.Price = ipm.PriceInBaseUnit;
                                    c.PriceInCurrency = ipm.PriceInBaseUnit;
                                    c.CostPrice = ipm.CostPrice;
                                }
                                else
                                {
                                    c.Description = i.ItemName;
                                    c.SRItemUnit = i.SRItemUnit;
                                    c.Price = 0;
                                    c.PriceInCurrency = 0;
                                    c.CostPrice = 0;
                                }

                                c.ConversionFactor = 1;
                                c.QuantityFinishInBaseUnit = 0;
                                c.PageNo = 0;

                                if (i.IsInventoryItem)
                                {
                                    c.IsDiscountInPercent = false;
                                    c.Discount1Percentage = 0;
                                    c.Discount2Percentage = 0;
                                }
                                else
                                {
                                    c.CostPrice = 0;
                                    c.IsDiscountInPercent = true;
                                    c.Discount1Percentage = c.Discount1Percentage ?? 0;
                                    c.Discount2Percentage = c.Discount2Percentage ?? 0;
                                }

                                decimal disc1 = Convert.ToDecimal(c.Price * c.Discount1Percentage / 100);
                                decimal disc2 = Convert.ToDecimal((c.Price - disc1) * c.Discount2Percentage / 100);
                                c.Discount = disc1 + disc2;
                                c.DiscountInCurrency = c.Discount;
                                c.IsBonusItem = false;
                                c.IsClosed = false;
                                c.Total = (c.Price - c.Discount) * c.Quantity;
                                c.BatchNumber = string.Empty;
                                c.str.ExpiredDate = string.Empty;
                                c.IsPackage = false;
                                c.Specification = i.Specification;

                                c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                c.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                                #endregion

                                var awoi = new AssetWorkOrderItem();
                                if (awoi.LoadByPrimaryKey(txtOrderNo.Text, i.SeqNo))
                                {
                                    awoi.IsGeneratePrDr = _txCode != "IR";
                                    awoi.IsGenerateIr = _txCode == "IR";
                                    awoi.Save();
                                }
                            }

                            #region header

                            var entity = new ItemTransaction();
                            entity.TransactionNo = _autoNumber.LastCompleteNumber;
                            entity.TransactionCode = group.IsInventoryItem
                                                         ? (_txCode == "IR" ? TransactionCode.InventoryIssueRequestOut : TransactionCode.DistributionRequest)
                                                         : TransactionCode.PurchaseRequest;
                            entity.TransactionDate = (new DateTime()).NowAtSqlServer().Date;
                            entity.SRItemType = ItemType.NonMedical;

                            entity.FromServiceUnitID = cboToServiceUnitID.SelectedValue;
                            var unit = new ServiceUnit();
                            entity.FromLocationID = unit.GetMainLocationId(entity.FromServiceUnitID);

                            entity.ToServiceUnitID = group.IsInventoryItem
                                                         ? AppSession.Parameter.MainDistributionServiceUnitIDForNonMedical
                                                         : AppSession.Parameter.MainPurchasingUnitIDForNonMedical;
                            entity.ToLocationID = unit.GetMainLocationId(entity.ToServiceUnitID);

                            entity.ServiceUnitCostID = cboFromServiceUnitID.SelectedValue;
                            entity.Notes = string.Empty;
                            entity.IsBySystem = true;
                            entity.IsInventoryItem = group.IsInventoryItem;
                            entity.IsNonMasterOrder = !group.IsMasterItem;
                            entity.ReferenceNo = txtOrderNo.Text;

                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                            #endregion

                            _autoNumber.Save();
                            entity.Save();
                            coll.Save();

                            transNos = string.IsNullOrEmpty(transNos) ? entity.TransactionNo : transNos + ", " + entity.TransactionNo;
                        }

                        trans.Complete();
                    }

                    pnlInfo.Visible = true;

                    if (!string.IsNullOrEmpty(transNos))
                    {
                        if (_txCode == "PR")
                            lblInfo.Text = "Generate Purchase / Distribution Request Succeed with No. : " + transNos;
                        else if (_txCode == "DR")
                            lblInfo.Text = "Generate Distribution Request Succeed with No. : " + transNos;
                        else
                            lblInfo.Text = "Generate Inventory Issue Request Succeed with No. : " + transNos;
                    }
                    else
                        lblInfo.Text = "There is no selected item to be processed.";
                }

                PopulateGridDetail();
                grdRequestList.Rebind();
            }
            else if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');

                if (param[0] == "approved")
                {
                    pnlInfo2.Visible = false;

                    var it = new ItemTransaction();
                    if (it.LoadByPrimaryKey(param[1]))
                    {
                        var itiColl = new ItemTransactionItemCollection();
                        itiColl.Query.Where(itiColl.Query.TransactionNo == it.TransactionNo);
                        itiColl.LoadAll();

                        string result = (new ItemTransaction()).Approve(it.TransactionNo, itiColl, AppSession.UserLogin.UserID);
                        if (result != string.Empty)
                        {
                            pnlInfo2.Visible = true;
                            lblInfo2.Text = result;
                        }

                        grdRequestList.Rebind();
                    }
                }
                else if (param[0] == "print")
                {
                    var it = new ItemTransaction();
                    if (it.LoadByPrimaryKey(param[1]))
                    {
                        var jobParameters = new PrintJobParameterCollection();

                        var parameter = jobParameters.AddNew();
                        parameter.Name = "p_TransactionNo";
                        parameter.ValueString = it.TransactionNo;

                        AppSession.PrintJobParameters = jobParameters;
                        AppSession.PrintJobReportID = it.TransactionCode == TransactionCode.PurchaseRequest
                                                          ? AppConstant.Report.PurchaseRequestSlp
                                                          : (it.TransactionCode == TransactionCode.DistributionRequest ? AppConstant.Report.DistributionRequestSlp : AppConstant.Report.InventoryIssueRequestSlp);

                        string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                                        "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                                        "oWnd.Show();" +
                                        "oWnd.Maximize();";
                        RadAjaxPanel1.ResponseScripts.Add(script);
                    }
                }
            }
        }

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery("a");
            var ipnm = new ItemProductNonMedicQuery("b");
            query.InnerJoin(ipnm).On(query.ItemID == ipnm.ItemID);
            query.es.Top = 20;
            query.Where
                (query.Or(query.ItemName.Like(searchTextContain),
                          query.ItemID.Like(searchTextContain)),
                 query.IsActive == true);
            query.OrderBy(query.ItemName.Ascending);
            DataTable dtb = query.LoadDataTable();

            cboItemID.DataSource = dtb;
            cboItemID.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"] + " [" +
                          ((DataRowView)e.Item.DataItem)["ItemID"] + "]";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRWorkStatus_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Where
                (query.StandardReferenceID == AppEnum.StandardReference.WorkStatus.ToString(),
                 query.Or(query.ItemName.Like(searchTextContain),
                          query.ItemID.Like(searchTextContain)),
                 query.IsActive == true, query.IsUsedBySystem == true);
            if (getPageID != "")
                query.Where(query.ItemID.NotIn(AppSession.Parameter.WorkStatusThirdParties));

            query.OrderBy(query.ItemID.Ascending);
            DataTable dtb = query.LoadDataTable();

            cboSRWorkStatus.DataSource = dtb;
            cboSRWorkStatus.DataBind();
        }

        protected void cboSRWorkStatus_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRWorkType_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Where
                (query.StandardReferenceID == AppEnum.StandardReference.WorkType.ToString(),
                 query.Or(query.ItemName.Like(searchTextContain),
                          query.ItemID.Like(searchTextContain)),
                 query.IsActive == true, query.IsUsedBySystem == true);
            query.OrderBy(query.ItemID.Ascending);
            DataTable dtb = query.LoadDataTable();

            cboSRWorkType.DataSource = dtb;
            cboSRWorkType.DataBind();
        }

        protected void cboSRWorkType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRWorkTrade_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComboBox.PopulateWorkTradeItemList(cboSRWorkTradeItem, e.Value, true);
        }

        protected void rbGenerateList_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rbGenerateList.SelectedValue)
            {
                case "0":
                    ComboBox.PopulateWithServiceUnitForTransaction(cboGenerateToServiceUnitID, TransactionCode.Distribution, false, string.Empty, ItemType.NonMedical);
                    break;
                case "1":
                    ComboBox.PopulateWithServiceUnitForTransaction(cboGenerateToServiceUnitID, TransactionCode.InventoryIssueOutForOtherUnit, false, string.Empty, ItemType.NonMedical);
                    break;
            }

            cboGenerateToServiceUnitID.SelectedValue = string.Empty;
            cboGenerateToServiceUnitID.Text = string.Empty;
        }

        protected void lbtnGenerate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboGenerateToServiceUnitID.SelectedValue))
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "Service Unit to generate DR / IR is required.";
            }
            else
            {
                var _txCode = rbGenerateList.SelectedValue == "0" ? "DR" : "IR";

                pnlInfo.Visible = false;

                bool _IsDistReqOrPurcReqUsingBudgetPlan = AppSession.Parameter.IsDistReqOrPurcReqUsingBudgetPlan;

                var su = new ServiceUnit();
                if (su.LoadByPrimaryKey(cboToServiceUnitID.SelectedValue))
                {
                    var transNos = string.Empty;

                    using (var trans = new esTransactionScope())
                    {
                        var coll = new ItemTransactionItemCollection();

                        var items = grdItem.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("generateChkbox")).Checked)
                                                                                      .Select(dataItem => new
                                                                                      {
                                                                                          SeqNo = dataItem["SeqNo"].Text,
                                                                                          ItemID = dataItem["ItemID"].Text,
                                                                                          ItemName = dataItem["ItemName"].Text,
                                                                                          Specification = dataItem["Specification"].Text,
                                                                                          SRItemUnit = dataItem["SRItemUnit"].Text,
                                                                                          IsInventoryItem = ((CheckBox)dataItem.FindControl("chkIsInventoryItem")).Checked,
                                                                                          IsMasterItem = ((CheckBox)dataItem.FindControl("chkIsMasterItem")).Checked,
                                                                                          Quantity = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQty")).Value ?? 0)
                                                                                      });

                        foreach (var group in (from g in items
                                               group g by new
                                               {
                                                   g.IsInventoryItem,
                                                   g.IsMasterItem
                                               }
                                                   into grp
                                               orderby grp.Key.IsInventoryItem
                                               select new
                                               {
                                                   IsInventoryItem = grp.Key.IsInventoryItem,
                                                   IsMasterItem = grp.Key.IsMasterItem
                                               }))
                        {
                            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date,
                                                                     group.IsInventoryItem
                                                                         ? (_txCode == "IR" ? TransactionCode.InventoryIssueRequestOut : TransactionCode.DistributionRequest)
                                                                         : TransactionCode.PurchaseRequest,
                                                                     su.DepartmentID);

                            foreach (var i in items.Where(i => i.IsInventoryItem == group.IsInventoryItem && i.IsMasterItem == group.IsMasterItem))
                            {
                                var c = coll.AddNew();

                                #region detail

                                c.TransactionNo = _autoNumber.LastCompleteNumber;
                                c.ItemID = i.IsMasterItem ? i.ItemID : string.Empty;
                                c.SequenceNo = string.Format("{0:000}", coll.Count + 1);
                                c.ReferenceNo = txtOrderNo.Text;
                                c.ReferenceSequenceNo = i.SeqNo;
                                if (i.IsInventoryItem)
                                {
                                    if (_IsDistReqOrPurcReqUsingBudgetPlan)
                                    {
                                        var toUnit = cboGenerateToServiceUnitID.SelectedValue;
                                        var iti = new ItemTransactionItem();
                                        decimal qtyBp = iti.GetCountBudgetPlan(cboToServiceUnitID.SelectedValue, toUnit, i.ItemID,
                                                                               (new DateTime()).NowAtSqlServer().Year, "");
                                        decimal qtyOffered = iti.GetCountBudgetPlanRealization(toUnit, cboToServiceUnitID.SelectedValue, i.ItemID,
                                                                       (new DateTime()).NowAtSqlServer().Year, "", false);
                                        decimal qtyBalance = qtyBp - qtyOffered;

                                        c.Quantity = i.Quantity > qtyBalance ? qtyBalance : i.Quantity;
                                    }
                                    else
                                        c.Quantity = i.Quantity;
                                }
                                else
                                    c.Quantity = i.Quantity;

                                if (i.IsMasterItem)
                                {
                                    var item = new Item();
                                    item.LoadByPrimaryKey(c.ItemID);
                                    c.Description = item.ItemName;

                                    var ipm = new ItemProductNonMedic();
                                    ipm.LoadByPrimaryKey(c.ItemID);
                                    c.SRItemUnit = ipm.SRItemUnit;

                                    c.Price = ipm.PriceInBaseUnit;
                                    c.PriceInCurrency = ipm.PriceInBaseUnit;
                                    c.CostPrice = ipm.CostPrice;
                                }
                                else
                                {
                                    c.Description = i.ItemName;
                                    c.SRItemUnit = i.SRItemUnit;
                                    c.Price = 0;
                                    c.PriceInCurrency = 0;
                                    c.CostPrice = 0;
                                }

                                c.ConversionFactor = 1;
                                c.QuantityFinishInBaseUnit = 0;
                                c.PageNo = 0;

                                if (i.IsInventoryItem)
                                {
                                    c.IsDiscountInPercent = false;
                                    c.Discount1Percentage = 0;
                                    c.Discount2Percentage = 0;
                                }
                                else
                                {
                                    c.CostPrice = 0;
                                    c.IsDiscountInPercent = true;
                                    c.Discount1Percentage = c.Discount1Percentage ?? 0;
                                    c.Discount2Percentage = c.Discount2Percentage ?? 0;
                                }

                                decimal disc1 = Convert.ToDecimal(c.Price * c.Discount1Percentage / 100);
                                decimal disc2 = Convert.ToDecimal((c.Price - disc1) * c.Discount2Percentage / 100);
                                c.Discount = disc1 + disc2;
                                c.DiscountInCurrency = c.Discount;
                                c.IsBonusItem = false;
                                c.IsClosed = false;
                                c.Total = (c.Price - c.Discount) * c.Quantity;
                                c.BatchNumber = string.Empty;
                                c.str.ExpiredDate = string.Empty;
                                c.IsPackage = false;
                                c.Specification = i.Specification;

                                c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                c.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                                #endregion

                                var awoi = new AssetWorkOrderItem();
                                if (awoi.LoadByPrimaryKey(txtOrderNo.Text, i.SeqNo))
                                {
                                    awoi.IsGeneratePrDr = _txCode == "DR";
                                    awoi.IsGenerateIr = _txCode == "IR";
                                    awoi.Save();
                                }
                            }

                            #region header

                            var entity = new ItemTransaction();
                            entity.TransactionNo = _autoNumber.LastCompleteNumber;
                            entity.TransactionCode = group.IsInventoryItem
                                                         ? (_txCode == "IR" ? TransactionCode.InventoryIssueRequestOut : TransactionCode.DistributionRequest)
                                                         : TransactionCode.PurchaseRequest;
                            entity.TransactionDate = (new DateTime()).NowAtSqlServer().Date;
                            entity.SRItemType = ItemType.NonMedical;

                            entity.FromServiceUnitID = cboToServiceUnitID.SelectedValue;
                            var unit = new ServiceUnit();
                            entity.FromLocationID = unit.GetMainLocationId(entity.FromServiceUnitID);

                            entity.ToServiceUnitID = cboGenerateToServiceUnitID.SelectedValue;
                            entity.ToLocationID = unit.GetMainLocationId(entity.ToServiceUnitID);

                            entity.ServiceUnitCostID = cboFromServiceUnitID.SelectedValue;
                            entity.Notes = string.Empty;
                            entity.IsBySystem = true;
                            entity.IsInventoryItem = group.IsInventoryItem;
                            entity.IsNonMasterOrder = !group.IsMasterItem;
                            entity.ReferenceNo = txtOrderNo.Text;

                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                            #endregion

                            _autoNumber.Save();
                            entity.Save();
                            coll.Save();

                            transNos = string.IsNullOrEmpty(transNos) ? entity.TransactionNo : transNos + ", " + entity.TransactionNo;
                        }

                        trans.Complete();
                    }

                    pnlInfo.Visible = true;

                    if (!string.IsNullOrEmpty(transNos))
                    {
                        if (_txCode == "DR")
                            lblInfo.Text = "Generate Distribution Request Succeed with No. : " + transNos;
                        else
                            lblInfo.Text = "Generate Inventory Issue Request Succeed with No. : " + transNos;
                    }
                    else
                        lblInfo.Text = "There is no selected item to be processed.";
                }
                PopulateGridDetail();
                grdRequestList.Rebind();
            }
        }
    }
}
