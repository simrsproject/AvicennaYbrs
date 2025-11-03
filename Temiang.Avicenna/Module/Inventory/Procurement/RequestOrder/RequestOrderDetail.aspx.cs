using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Procurement
{
    public partial class RequestOrderDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private bool IsItemConsignmentAlreadyReceived
        {
            get
            {
                return !string.IsNullOrEmpty(Request.QueryString["cons"]) && Request.QueryString["cons"] != "0";
            }
        }

        private string getPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["itype"]) ? string.Empty : Request.QueryString["itype"];
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "RequestOrderSearch.aspx";

            if (!IsItemConsignmentAlreadyReceived)
            {
                UrlPageList = string.IsNullOrEmpty(Request.QueryString["wo"])
                              ? ((getPageID == "a" || getPageID == "") ? "RequestOrderList.aspx?itype=" + getPageID : "RequestOrderAssetApprovalList.aspx") 
                              : "../../../../Module/AssetManagement/Management/WorkOrderThirdParties/WorkOrderReceivedFromThirdPartiesList.aspx";
            }
            else
            {
                UrlPageList = "RequestOrderConsignmentList.aspx";
            }

            ProgramID = getPageID == "" ? AppConstant.Program.RequestOrder : (getPageID == "a" ? AppConstant.Program.RequestOrderAsset : AppConstant.Program.RequestOrderAssetApproval);

            Session["PurchaseRequestForWorkOrder" + Request.UserHostName] =
                string.IsNullOrEmpty(Request.QueryString["wo"]) ? string.Empty : Request.QueryString["wo"];

            //StandardReference Initialize
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.PurchaseRequest, true);
                ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, BusinessObject.Reference.TransactionCode.PurchaseRequest);
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, BusinessObject.Reference.TransactionCode.PurchaseOrder, false, string.Empty, cboSRItemType.SelectedValue);
                StandardReference.InitializeIncludeSpace(cboCategorization, AppEnum.StandardReference.PurchaseCategorization);
                StandardReference.InitializeIncludeSpace(cboSRItemCategory, AppEnum.StandardReference.ItemCategory);

                var productAcc = new ProductAccountCollection();
                productAcc.Query.Where(productAcc.Query.IsActive == true);
                productAcc.LoadAll();

                cboSRProductAccountID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var c in productAcc)
                {
                    cboSRProductAccountID.Items.Add(new RadComboBoxItem(c.ProductAccountName, c.ProductAccountID));
                }

                var costUnit = new ServiceUnitCollection();
                costUnit.Query.Where(costUnit.Query.IsActive == true);
                costUnit.LoadAll();
                cboServiceUnitCostID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var u in costUnit)
                {
                    cboServiceUnitCostID.Items.Add(new RadComboBoxItem(u.ServiceUnitName, u.ServiceUnitID));
                }

                if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorBySupplierItem) || IsItemConsignmentAlreadyReceived)
                {
                    trSupplier.Visible = true;
                    chkIsUsedFilterSupplier.Checked = true;
                }
                else
                {
                    trSupplier.Visible = false;
                    chkIsUsedFilterSupplier.Checked = false;
                }
                    
                //  Reset Session
                ItemTransactionItems = null;

                ProcurementUtils.HideColumnStockAndPriceInfo(grdItemTransactionItem.MasterTableView);
                if (AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory)
                {
                    trSRItemCategory.Visible = true;
                    chkIsUsedFilterItemCategory.Checked = true;
                }
                else
                {
                    trSRItemCategory.Visible = false;
                    chkIsUsedFilterItemCategory.Checked = false;
                }
                
                trReferenceNo.Visible = IsItemConsignmentAlreadyReceived;
            }

            AjaxManager.AjaxRequest += AjaxManager_AjaxRequest;
        }

        private void AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            PopulateFromSelectedRequestOrder();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItemTransactionItem, grdItemTransactionItem);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboSRItemType);
            if (AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory)
                ajax.AddAjaxSetting(grdItemTransactionItem, cboSRItemCategory);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboFromServiceUnitID);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboLocationID);
            ajax.AddAjaxSetting(grdItemTransactionItem, chkIsNonMasterOrder);
            ajax.AddAjaxSetting(grdItemTransactionItem, chkIsInventoryItem);
            ajax.AddAjaxSetting(grdItemTransactionItem, chkIsConsignment);
            //if (AppSession.Application.IsModuleAssetActive)
            //    ajax.AddAjaxSetting(grdItemTransactionItem, chkIsAssets);


            //ajax.AddAjaxSetting(cboFromServiceUnitID, txtLocationID);
            //ajax.AddAjaxSetting(cboFromServiceUnitID, lblLocationName);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboLocationID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, txtTransactionNo);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboSRItemType);

            //if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboServiceUnitCostID);

            ajax.AddAjaxSetting(chkIsInventoryItem, chkIsInventoryItem);
            ajax.AddAjaxSetting(chkIsInventoryItem, chkIsNonMasterOrder);
            ajax.AddAjaxSetting(chkIsInventoryItem, chkIsAssets);
            ajax.AddAjaxSetting(chkIsInventoryItem, chkIsConsignment);

            ajax.AddAjaxSetting(chkIsNonMasterOrder, chkIsNonMasterOrder);
            ajax.AddAjaxSetting(chkIsNonMasterOrder, chkIsAssets);
            ajax.AddAjaxSetting(chkIsNonMasterOrder, chkIsConsignment);

            ajax.AddAjaxSetting(chkIsAssets, chkIsAssets);
            ajax.AddAjaxSetting(chkIsAssets, chkIsConsignment);

            ajax.AddAjaxSetting(chkIsConsignment, chkIsConsignment);
            ajax.AddAjaxSetting(chkIsConsignment, chkIsAssets);

            ajax.AddAjaxSetting(cboSRItemType, cboSRItemType);
            ajax.AddAjaxSetting(cboSRItemType, cboToServiceUnitID);

            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorBySupplierItem))
            {
                ajax.AddAjaxSetting(cboSRItemType, cboSRItemType);
                ajax.AddAjaxSetting(cboSRItemType, cboBusinessPartnerID);
                ajax.AddAjaxSetting(cboBusinessPartnerID, cboBusinessPartnerID);
                ajax.AddAjaxSetting(cboBusinessPartnerID, grdItemTransactionItem);
            }

            //Request Order Selection
            ajax.AddAjaxSetting(AjaxManager, chkIsNonMasterOrder);
            ajax.AddAjaxSetting(AjaxManager, chkIsInventoryItem);
            ajax.AddAjaxSetting(AjaxManager, chkIsAssets);
            ajax.AddAjaxSetting(AjaxManager, chkIsConsignment);
            ajax.AddAjaxSetting(AjaxManager, txtReferenceNo);
            ajax.AddAjaxSetting(AjaxManager, txtTransactionNo);
            ajax.AddAjaxSetting(AjaxManager, cboFromServiceUnitID);
            ajax.AddAjaxSetting(AjaxManager, cboToServiceUnitID);
            ajax.AddAjaxSetting(AjaxManager, cboServiceUnitCostID);
            ajax.AddAjaxSetting(AjaxManager, cboSRItemType);
            ajax.AddAjaxSetting(AjaxManager, cboSRProductAccountID);
            ajax.AddAjaxSetting(AjaxManager, cboBusinessPartnerID);
            ajax.AddAjaxSetting(AjaxManager, grdItemTransactionItem);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
            if (IsItemConsignmentAlreadyReceived)
            {
                cboFromServiceUnitID.Enabled = false;
                cboLocationID.Enabled = false;
                cboToServiceUnitID.Enabled = false;
                cboSRItemType.Enabled = false;
                cboSRItemCategory.Enabled = false;
                cboServiceUnitCostID.Enabled = false;
                chkIsNonMasterOrder.Enabled = false;
                cboBusinessPartnerID.Enabled = false;
                chkIsInventoryItem.Enabled = false;
                chkIsNonMasterOrder.Enabled = false;
                chkIsAssets.Enabled = false;
                chkIsConsignment.Enabled = false;
            }
            else
            {
                cboSRItemType.Enabled = ItemTransactionItems.Count == 0;
                cboSRItemCategory.Enabled = ItemTransactionItems.Count == 0;
                cboFromServiceUnitID.Enabled = ItemTransactionItems.Count == 0;
                cboLocationID.Enabled = ItemTransactionItems.Count == 0;
                chkIsInventoryItem.Enabled = ItemTransactionItems.Count == 0;
                if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSCH" && !chkIsInventoryItem.Checked)
                    chkIsNonMasterOrder.Enabled = ItemTransactionItems.Count == 0;
                chkIsConsignment.Enabled = ItemTransactionItems.Count == 0;
                chkIsAssets.Enabled = ItemTransactionItems.Count == 0 && !chkIsInventoryItem.Checked && getPageID != "a" && getPageID != "aa";
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

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            string result = (new ItemTransaction()).Approve(txtTransactionNo.Text, ItemTransactionItems, AppSession.UserLogin.UserID);
            if (result != string.Empty)
            {
                args.MessageText = result;
                args.IsCancel = true;
                return;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["wo"]))
            {
                var awo = new AssetWorkOrder();
                if (awo.LoadByPrimaryKey(Request.QueryString["wo"]))
                {
                    decimal costAmt = ItemTransactionItems.Sum(item => ((item.Quantity ?? 0) * (item.Price ?? 0)));

                    awo.CostEstimation += costAmt;
                    awo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    awo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    awo.Save();
                }
            }
            //db:20231030 - knp ada validasi ini di sini y? hmmm....
            //if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            //{
            //    var it = new ItemTransaction();
            //    if (it.LoadByPrimaryKey(txtTransactionNo.Text))
            //    {

            //        if (string.IsNullOrEmpty(it.SRPurchaseCategorization))
            //        {
            //            args.MessageText = "Categorization is required.";
            //            args.IsCancel = true;
            //            return;
            //        }
            //    }
            //}
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            if (!IsProceed(args)) return;

            (new ItemTransaction()).UnApprove(txtTransactionNo.Text, ItemTransactionItems, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            OnVoid(true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            OnVoid(false);
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

        private void OnVoid(bool isVoid)
        {
            ItemTransaction header = new ItemTransaction();
            header.LoadByPrimaryKey(txtTransactionNo.Text);

            header.IsVoid = isVoid;
            header.VoidByUserID = isVoid ? AppSession.UserLogin.UserID : string.Empty;
            if (isVoid)
                header.VoidDate = (new DateTime()).NowAtSqlServer();
            else
                header.str.VoidDate = string.Empty;

            header.LastUpdateByUserID = AppSession.UserLogin.UserID;
            header.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (ItemTransactionItem detail in ItemTransactionItems)
            {
                detail.IsClosed = isVoid;
                detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                detail.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                header.Save();
                ItemTransactionItems.Save();

                trans.Complete();
            }
        }

        private bool IsProceed(ValidateArgs args)
        {
            var porDtQ = new ItemTransactionItemQuery("a");
            var porQ = new ItemTransactionQuery("b");
            porDtQ.InnerJoin(porQ).On(porQ.TransactionNo == porDtQ.TransactionNo);
            porDtQ.Where(porDtQ.ReferenceNo == txtTransactionNo.Text, porQ.IsVoid == false);
            DataTable dtb = porDtQ.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                args.IsCancel = true;
                args.MessageText = "This transaction can't be canceled, this data has been proceed to PO.";
                return false;
            }

            if (AppSession.Parameter.IsUsingApprovalPurchaseRequest)
            {
                var proceed = false;
                foreach (var item in ItemTransactionItems)
                {
                    if (item.RequestQty != null)
                        if (item.RequestQty != 0)
                            proceed = true;
                }
                if (proceed)
                {
                    args.IsCancel = true;
                    args.MessageText = "This transaction can't be canceled, this data has been proceed to Request Approval.";
                    return false;
                }
            }

            return true;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ItemTransaction());

            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtPlanningDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            if (!IsItemConsignmentAlreadyReceived)
            {
                if (string.IsNullOrEmpty(Request.QueryString["wo"]))
                {
                    cboFromServiceUnitID.Text = string.Empty;
                    cboLocationID.Items.Clear();
                    cboLocationID.Text = string.Empty;
                    cboServiceUnitCostID.Text = string.Empty;
                    cboToServiceUnitID.Text = string.Empty;
                }
                else
                {
                    var wo = new AssetWorkOrder();
                    wo.LoadByPrimaryKey(Request.QueryString["wo"]);
                    cboFromServiceUnitID.SelectedValue = wo.ToServiceUnitID;

                    if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                    {
                        ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, cboFromServiceUnitID.SelectedValue);
                        cboLocationID.SelectedIndex = 1;
                    }

                    cboServiceUnitCostID.SelectedValue = wo.FromServiceUnitID;
                    cboToServiceUnitID.SelectedValue = AppSession.Parameter.MainPurchasingUnitIDForNonMedical;
                    ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, TransactionCode.PurchaseRequest);
                    cboSRItemType.SelectedValue = ItemType.NonMedical;

                    PopulateNewTransactionNo();
                }

                cboCategorization.Text = string.Empty;
                cboSRItemCategory.Text = string.Empty;
                cboSRProductAccountID.Text = string.Empty;
                chkIsInventoryItem.Checked = getPageID != "a";
                chkIsNonMasterOrder.Enabled = false;
                chkIsNonMasterOrder.Checked = false;
                chkIsAssets.Checked = getPageID == "a";
                chkIsConsignment.Checked = false;

            }
            else
                PopulateNewTransactionNo();

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            ItemTransaction entity = new ItemTransaction();
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
            if (ItemTransactionItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }
            if (string.IsNullOrEmpty(cboSRItemType.SelectedValue))
            {
                args.MessageText = "Item Type is required.";
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
            if (trSRItemCategory.Visible && string.IsNullOrEmpty(cboSRItemCategory.SelectedValue))
            {
                args.MessageText = "Item Category is required.";
                args.IsCancel = true;
                return;
            }
            if (pnlProductAccountID.Visible && chkIsNonMasterOrder.Checked && string.IsNullOrEmpty(cboSRProductAccountID.SelectedValue))
            {
                args.MessageText = "Product Account required.";
                args.IsCancel = true;
                return;
            }

            PopulateNewTransactionNo();
            // save autonumber immediately to decrease time gap between create and save
            _autoNumber.Save();

            var entity = new ItemTransaction();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);

                if (ItemTransactionItems.Count == 0)
                {
                    args.MessageText = AppConstant.Message.RecordDetailEmpty;
                    args.IsCancel = true;
                    return;
                }
                if (string.IsNullOrEmpty(entity.SRItemType))
                {
                    args.MessageText = "Item Type is required.";
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
                if (pnlProductAccountID.Visible && entity.IsNonMasterOrder == true && string.IsNullOrEmpty(entity.ProductAccountID))
                {
                    args.MessageText = "Product Account required.";
                    args.IsCancel = true;
                    return;
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

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "ItemTransaction";
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtTransactionNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
            if (getPageID == "a" || getPageID == "aa")
            {
                chkIsNonMasterOrder.Enabled = false;
                chkIsInventoryItem.Enabled = false;
                chkIsAssets.Enabled = false;
                chkIsConsignment.Enabled = false;
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ItemTransaction entity = new ItemTransaction();
            if (parameters.Length > 0)
            {
                String transactionNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transactionNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            ItemTransaction itemTransaction = (ItemTransaction)entity;
            txtTransactionNo.Text = itemTransaction.TransactionNo;
            txtTransactionDate.SelectedDate = itemTransaction.TransactionDate;
            txtPlanningDate.SelectedDate = itemTransaction.PlanningDate;

            cboFromServiceUnitID.SelectedValue = itemTransaction.FromServiceUnitID;
            cboServiceUnitCostID.SelectedValue = itemTransaction.ServiceUnitCostID;
            //PopulateLocationName();
            if (!string.IsNullOrEmpty(itemTransaction.FromServiceUnitID))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, itemTransaction.FromServiceUnitID);
                if (!string.IsNullOrEmpty(itemTransaction.FromLocationID))
                    cboLocationID.SelectedValue = itemTransaction.FromLocationID;
                else cboLocationID.SelectedIndex = 1;
            }

            cboToServiceUnitID.SelectedValue = itemTransaction.ToServiceUnitID;
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, BusinessObject.Reference.TransactionCode.PurchaseRequest);
            cboSRItemType.SelectedValue = itemTransaction.SRItemType;

            ViewState["IsApproved"] = itemTransaction.IsApproved ?? false;
            ViewState["IsVoid"] = itemTransaction.IsVoid ?? false;

            chkIsClosed.Checked = itemTransaction.IsClosed ?? false;
            chkIsNonMasterOrder.Checked = itemTransaction.IsNonMasterOrder ?? false;
            chkIsInventoryItem.Checked = itemTransaction.IsInventoryItem ?? false;
            chkIsAssets.Checked = itemTransaction.IsAssets ?? false;
            chkIsConsignment.Checked = itemTransaction.IsConsignment ?? false;
            txtNotes.Text = itemTransaction.Notes;
            cboSRProductAccountID.SelectedValue = itemTransaction.ProductAccountID;
            cboCategorization.SelectedValue = itemTransaction.SRPurchaseCategorization;
            cboSRItemCategory.SelectedValue = itemTransaction.SRItemCategory;

            if (!string.IsNullOrEmpty(itemTransaction.BusinessPartnerID))
            {
                var sq = new SupplierQuery();
                sq.Where(sq.SupplierID == itemTransaction.BusinessPartnerID);
                cboBusinessPartnerID.DataSource = sq.LoadDataTable();
                cboBusinessPartnerID.DataBind();
                cboBusinessPartnerID.SelectedValue = itemTransaction.BusinessPartnerID;
            }
            else
            {
                cboBusinessPartnerID.Items.Clear();
                cboBusinessPartnerID.Text = string.Empty;
            }

            if ((!string.IsNullOrEmpty(Request.QueryString["pr"])) && DataModeCurrent == AppEnum.DataMode.New)
            {
                txtReferenceNo.Text = Request.QueryString["pr"];
                var tx = new ItemTransaction();
                tx.LoadByPrimaryKey(txtReferenceNo.Text);

                ComboBox.PopulateWithOneServiceUnit(cboFromServiceUnitID,
                                                    tx.SRItemType == ItemType.Medical
                                                        ? AppSession.Parameter.ServiceUnitPharCentralWarehouseId1
                                                        : AppSession.Parameter.ServiceUnitLogisticCentralWarehouseId);
                ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, TransactionCode.PurchaseRequest);
                cboSRItemType.SelectedValue = tx.SRItemType;
                PopulateGridDetailFromReferenceNo();
            }
            else
            {
                //Display Data Detail
                PopulateGridDetail();
            }
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ItemTransaction entity)
        {
            //if (DataModeCurrent == AppEnum.DataMode.New)
            //{
            //    PopulateNewTransactionNo();
            //    // save autonumber immediately to decrease time gap between create and save
            //    _autoNumber.Save();
            //}

            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionCode = TransactionCode.PurchaseRequest;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.PlanningDate = txtPlanningDate.SelectedDate;
            entity.BusinessPartnerID = cboBusinessPartnerID.SelectedValue;
            entity.ReferenceNo = !string.IsNullOrEmpty(Request.QueryString["wo"]) ? Request.QueryString["wo"] : txtReferenceNo.Text;
            entity.ReferenceDate = DateTime.Parse("01/01/1900");
            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            entity.FromLocationID = cboLocationID.SelectedValue; //txtLocationID.Text;
            entity.ServiceUnitCostID = cboServiceUnitCostID.SelectedValue;
            entity.ToServiceUnitID = cboToServiceUnitID.SelectedValue;
            entity.ToLocationID = string.Empty;
            entity.TermID = string.Empty;
            entity.SRItemType = cboSRItemType.SelectedValue;
            entity.DiscountAmount = 0;
            entity.ChargesAmount = 0;
            entity.StampAmount = 0;
            entity.DownPaymentAmount = 0;
            entity.DownPaymentReferenceNo = string.Empty;
            entity.SRDownPaymentType = string.Empty;
            entity.SRAdjustmentType = string.Empty;
            entity.SRDistributionType = string.Empty;
            entity.SRPurchaseReturnType = string.Empty;
            entity.TaxPercentage = 0;
            entity.TaxAmount = 0;
            entity.IsTaxable = 0;
            entity.IsVoid = false;
            entity.VoidDate = DateTime.Parse("01/01/1900");
            entity.VoidByUserID = string.Empty;
            entity.IsApproved = false;
            entity.ApprovedDate = DateTime.Parse("01/01/1900");
            entity.ApprovedByUserID = string.Empty;
            entity.IsClosed = false;
            entity.IsBySystem = false;
            entity.Notes = txtNotes.Text;
            entity.IsNonMasterOrder = chkIsNonMasterOrder.Checked;
            entity.SRPurchaseCategorization = cboCategorization.SelectedValue;
            entity.SRItemCategory = cboSRItemCategory.SelectedValue;
            entity.ProductAccountID = cboSRProductAccountID.SelectedValue;
            entity.IsInventoryItem = chkIsInventoryItem.Checked;
            entity.IsAssets = chkIsAssets.Checked;
            entity.IsConsignment = chkIsConsignment.Checked;
            entity.IsConsignmentAlreadyReceived = IsItemConsignmentAlreadyReceived;
            
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

                //AutoNumberLast
                //autonumber has been saved on SetEntity
                //if (DataModeCurrent == AppEnum.DataMode.New)
                //    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ItemTransactionQuery("a");
            var qusr = new AppUserServiceUnitQuery("u");
            que.InnerJoin(qusr).On(que.FromServiceUnitID == qusr.ServiceUnitID &&
                                         qusr.UserID == AppSession.UserLogin.UserID);

            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TransactionNo > txtTransactionNo.Text && que.TransactionCode == TransactionCode.PurchaseRequest);
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < txtTransactionNo.Text && que.TransactionCode == TransactionCode.PurchaseRequest);
                que.OrderBy(que.TransactionNo.Descending);
            }

            if (IsItemConsignmentAlreadyReceived)
                que.Where(que.IsConsignmentAlreadyReceived.IsNotNull(), que.IsConsignmentAlreadyReceived == true);
            else
                que.Where(que.Or(que.IsConsignmentAlreadyReceived.IsNull(), que.IsConsignmentAlreadyReceived == false));

            ItemTransaction entity = new ItemTransaction();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        protected void cboSRItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                cboToServiceUnitID.Items.Clear();
                cboToServiceUnitID.SelectedValue = string.Empty;
                cboToServiceUnitID.Text = string.Empty;

                return;
            }
            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, BusinessObject.Reference.TransactionCode.PurchaseOrder, false, string.Empty, e.Value);
            cboBusinessPartnerID.Enabled = cboSRItemType.SelectedValue == ItemType.Medical;
        }

        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //PopulateLocationName();
            PopulateNewTransactionNo();
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, BusinessObject.Reference.TransactionCode.PurchaseRequest);
            cboServiceUnitCostID.SelectedValue = e.Value;
            ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, e.Value);
            cboLocationID.SelectedIndex = 1;
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
            UpdateItemPrice();
        }

        private void UpdateItemPrice()
        {
            if (ItemTransactionItems.Count > 0)
            {
                var supplierID = cboBusinessPartnerID.SelectedValue;
                foreach (var item in ItemTransactionItems)
                {
                    ProcurementUtils.PopulateWithHistPrice(item, cboSRItemType.SelectedValue, supplierID);
                }

                grdItemTransactionItem.Rebind();
            }
        }

        //private void PopulateLocationName()
        //{
        //    lblLocationName.Text = string.Empty;
        //    txtLocationID.Text = string.Empty;

        //    if (cboFromServiceUnitID.SelectedValue == string.Empty)
        //        return;

        //    ServiceUnit unit = new ServiceUnit();
        //    unit.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue);

        //    Location loc = new Location();
        //    if (loc.LoadByPrimaryKey(unit.str.LocationID))
        //    {
        //        txtLocationID.Text = unit.LocationID;
        //        lblLocationName.Text = loc.LocationName;
        //    }
        //}

        private void PopulateNewTransactionNo()
        {
            txtTransactionNo.Text = string.Empty;

            if (DataModeCurrent != AppEnum.DataMode.New)
                return;

            if (cboFromServiceUnitID.SelectedValue == string.Empty)
                return;

            ServiceUnit serv = new ServiceUnit();
            if (serv.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue))
            {
                _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, BusinessObject.Reference.TransactionCode.PurchaseRequest, serv.DepartmentID);
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
                query.TransactionNo == txtReferenceNo.Text
                );

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
                    @"<(a.Quantity*a.ConversionFactor) AS QtyTransfer>"
                );
            var dtb = query.LoadDataTable();

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
                    if (Convert.ToDouble(row["QtyTransfer"]) <= Convert.ToDouble(dtbd.Rows[0]["QtyFinished"]))
                        row.Delete();
                    else
                    {
                        row["QtyInput"] = (Convert.ToDouble(row["QtyTransfer"]) -
                                           Convert.ToDouble(dtbd.Rows[0]["QtyFinished"])) /
                                          Convert.ToDouble(row["ConversionFactor"]);
                    }
                }
            }
            dtb.AcceptChanges();

            Session["CTItemSelected" + Request.UserHostName] = dtb;

            PopulateFromSelectedRequestOrder();
        }

        private void PopulateFromSelectedRequestOrder()
        {
            object obj = Session["CTItemSelected" + Request.UserHostName];
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

                if (tr.SRItemType == ItemType.Medical)
                {
                    ComboBox.PopulateWithOneServiceUnit(cboFromServiceUnitID, AppSession.Parameter.ServiceUnitPharCentralWarehouseId1);
                    ComboBox.PopulateWithOneServiceUnit(cboToServiceUnitID, AppSession.Parameter.MainPurchasingUnitIDForMedical);
                }
                else
                {
                    ComboBox.PopulateWithOneServiceUnit(cboFromServiceUnitID, AppSession.Parameter.ServiceUnitLogisticCentralWarehouseId);
                    ComboBox.PopulateWithOneServiceUnit(cboToServiceUnitID, AppSession.Parameter.MainPurchasingUnitIDForNonMedical);
                }
                ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, cboFromServiceUnitID.SelectedValue);
                cboLocationID.SelectedIndex = 1;

                cboFromServiceUnitID.Enabled = false;
                cboLocationID.Enabled = false;
                cboToServiceUnitID.Enabled = false;

                ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, TransactionCode.PurchaseRequest);
                cboSRItemType.SelectedValue = tr.SRItemType;
                cboSRItemType.Enabled = false;

                cboServiceUnitCostID.SelectedValue = tr.ToServiceUnitID;
                cboServiceUnitCostID.Enabled = false;

                chkIsNonMasterOrder.Checked = tr.IsNonMasterOrder ?? false;
                if (!string.IsNullOrEmpty(tr.ProductAccountID))
                    cboSRProductAccountID.SelectedValue = tr.ProductAccountID;

                if (!string.IsNullOrEmpty(tr.BusinessPartnerID))
                {
                    cboBusinessPartnerID.Items.Clear();
                    var sq = new SupplierQuery();
                    sq.Where(sq.SupplierID == tr.BusinessPartnerID);
                    cboBusinessPartnerID.DataSource = sq.LoadDataTable();
                    cboBusinessPartnerID.DataBind();
                    cboBusinessPartnerID.SelectedValue = tr.BusinessPartnerID;
                    cboBusinessPartnerID.Enabled = false;
                }

                chkIsInventoryItem.Checked = tr.IsInventoryItem ?? false;
                chkIsInventoryItem.Enabled = false;
                chkIsNonMasterOrder.Checked = tr.IsNonMasterOrder ?? false;
                chkIsNonMasterOrder.Enabled = false;
                chkIsAssets.Checked = tr.IsAssets ?? false;
                chkIsAssets.Enabled = false;
                chkIsConsignment.Checked = tr.IsConsignment ?? false;
                chkIsConsignment.Enabled = false;
                cboCategorization.SelectedValue = tr.SRPurchaseCategorization;
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
                entity.PriceInCurrency = entity.Price;
                entity.Discount = (entity.Price * entity.Discount1Percentage / 100) +
                                  ((entity.Price - (entity.Price * entity.Discount1Percentage / 100)) *
                                   entity.Discount2Percentage / 100);
                entity.DiscountInCurrency = entity.Discount;
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
                    entity.DrugDistributionLicenseNo = suppItem.LoadByPrimaryKey(cboBusinessPartnerID.SelectedValue,
                                                                                 entity.ItemID)
                                                           ? suppItem.DrugDistributionLicenseNo
                                                           : string.Empty;
                }
                else
                    entity.DrugDistributionLicenseNo = string.Empty;
            }

            grdItemTransactionItem.DataSource = ItemTransactionItems;
            grdItemTransactionItem.DataBind();

            cboSRProductAccountID.Enabled = chkIsNonMasterOrder.Checked;

            //Remove session
            Session.Remove("CTItemSelected" + Request.UserHostName);
        }

        #region Record Detail Method Function

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemTransactionItem.Columns[0].Visible = isVisible;
            grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 1].Visible = isVisible;

            if (IsItemConsignmentAlreadyReceived)
                grdItemTransactionItem.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            else
                grdItemTransactionItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                ItemTransactionItems = null;

            ////Perbaharui tampilan dan data
            //grdItemTransactionItem.DataSource = null;
            //grdItemTransactionItem.Rebind();

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdItemTransactionItem.Rebind();
        }

        private ItemTransactionItemCollection ItemTransactionItems
        {
            get
            {
                //if (IsPostBack)
                //{
                object obj = Session["RequestOrderItems" + Request.UserHostName];
                if (obj != null)
                    return ((ItemTransactionItemCollection)(obj));
                //}

                var coll = new ItemTransactionItemCollection();

                var query = new ItemTransactionItemQuery("a");
                var iq = new ItemQuery("b");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName"),
                        @"<CASE WHEN b.SRItemType <> '21' THEN '' ELSE 'Specification : '+ ISNULL(a.Specification, '') END AS 'refToAdditionalInfo'>"
                    );

                query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);

                if (AppSession.Parameter.HealthcareInitial == "RSTJ")
                    query.OrderBy(iq.ItemName.Ascending);
                else
                    query.OrderBy(query.SequenceNo.Ascending);

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

                coll.Load(query);

                Session["RequestOrderItems" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["RequestOrderItems" + Request.UserHostName] = value; }
        }

        private void InitializeQueryWithStockInfo(ItemTransactionItemQuery query)
        {
            // Base Unit
            var ipnmq = new ItemProductNonMedicQuery("i2");
            var ikq = new ItemKitchenQuery("i2");
            var ipmq = new ItemProductMedicQuery("i2");

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

            // Balance Min Max
            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorByStockGroup))
            {
                var stockGroup = "ABCD_EFG";
                var ibbsgq = new ItemBalanceByStockGroupQuery("c");
                var loc = new Location();
                loc.LoadByPrimaryKey(cboLocationID.SelectedValue);
                if (!string.IsNullOrEmpty(loc.SRStockGroup))
                    stockGroup = loc.SRStockGroup;
                query.LeftJoin(ibbsgq).On(query.ItemID == ibbsgq.ItemID && ibbsgq.SRStockGroup == stockGroup);

                var ibq = new ItemBalanceQuery("bl");
                var locationID = cboLocationID.SelectedValue ?? string.Empty;
                query.LeftJoin(ibq).On(query.ItemID == ibq.ItemID && ibq.LocationID == locationID);

                query.Select(@"<CONVERT(decimal(10,2),COALESCE(c.Balance,0)) AS refToItemBalance_BalanceSG>",
                    @"<CONVERT(decimal(10,2),COALESCE(bl.Balance,0)) AS refToItemBalance_Balance>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Minimum,0)) AS refToItemBalance_Minimum>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Maximum,0)) AS refToItemBalance_Maximum>",
                    @"<i2.SRItemUnit AS refToItemProduct_SRItemUnit>"
                    );
            }
            else
            {
                var locationID = cboLocationID.SelectedValue; //ProcurementUtils.LocationIdByItemType(cboSRItemType.SelectedValue);
                var ibq = new ItemBalanceQuery("c");
                if (string.IsNullOrEmpty(locationID))
                    locationID = "ABCD_EFG";
                query.LeftJoin(ibq).On(query.ItemID == ibq.ItemID && ibq.LocationID == locationID);

                query.Select(@"<CONVERT(decimal(10,2),0) AS refToItemBalance_BalanceSG>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Balance,0)) AS refToItemBalance_Balance>",
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
            if (IsItemConsignmentAlreadyReceived)
                return;

            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemTransactionItemMetadata.ColumnNames.SequenceNo]);
            ItemTransactionItem entity = FindItemTransactionItem(sequenceNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItemTransactionItem_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        private ItemTransactionItem FindItemTransactionItem(String sequenceNo)
        {
            ItemTransactionItemCollection coll = ItemTransactionItems;
            return coll.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        protected void grdItemTransactionItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            if (IsItemConsignmentAlreadyReceived)
                return;

            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemTransactionItemMetadata.ColumnNames.SequenceNo]);
            ItemTransactionItem entity = FindItemTransactionItem(sequenceNo);
            if (entity != null)
                entity.MarkAsDeleted();

            if (ItemTransactionItems.Count == 0)
            {
                cboSRItemType.Enabled = true;
                cboSRItemCategory.Enabled = true;
                cboFromServiceUnitID.Enabled = true;
                cboLocationID.Enabled = true;
                chkIsInventoryItem.Enabled = getPageID != "a" && getPageID != "aa"; //true;
                if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSCH" && !chkIsInventoryItem.Checked)
                    chkIsNonMasterOrder.Enabled = getPageID != "a" && getPageID != "aa"; //true;
                chkIsConsignment.Enabled = getPageID != "a" && getPageID != "aa"; //true;
                chkIsAssets.Enabled = !chkIsInventoryItem.Checked && getPageID != "a" && getPageID != "aa"; //true;
            }
        }

        protected void grdItemTransactionItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemTransactionItem entity = ItemTransactionItems.AddNew();
            SetEntityValue(entity, e);

            //grid not close first
            e.Canceled = true;
            grdItemTransactionItem.Rebind();

            cboSRItemType.Enabled = (ItemTransactionItems.Count == 0);
            cboSRItemCategory.Enabled = (ItemTransactionItems.Count == 0);
            cboFromServiceUnitID.Enabled = (ItemTransactionItems.Count == 0);
            cboLocationID.Enabled = (ItemTransactionItems.Count == 0);
            chkIsInventoryItem.Enabled = (ItemTransactionItems.Count == 0) && getPageID != "a" && getPageID != "aa";
            if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSCH" && !chkIsInventoryItem.Checked)
                chkIsNonMasterOrder.Enabled = (ItemTransactionItems.Count == 0) && getPageID != "a" && getPageID != "aa";
            chkIsConsignment.Enabled = (ItemTransactionItems.Count == 0) && getPageID != "a" && getPageID != "aa";
            chkIsAssets.Enabled = (ItemTransactionItems.Count == 0) && !chkIsInventoryItem.Checked && getPageID != "a" && getPageID != "aa";
        }

        private void SetEntityValue(ItemTransactionItem entity, GridCommandEventArgs e)
        {
            RequestOrderItemDetail userControl = (RequestOrderItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SequenceNo = userControl.SequenceNo;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.ReferenceNo = string.Empty;
                entity.ReferenceSequenceNo = string.Empty;
                entity.Quantity = userControl.Quantity;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ConversionFactor = userControl.ConversionFactor;
                entity.QuantityFinishInBaseUnit = 0;
                entity.PageNo = 0;
                entity.CostPrice = 0;


                //entity.Price = 0;
                //entity.Discount1Percentage = 0;
                //entity.Discount2Percentage = 0;
                entity.IsDiscountInPercent = true;


                entity.Price = userControl.Price;
                entity.Discount1Percentage = userControl.Discount1Percentage;
                entity.Discount2Percentage = userControl.Discount2Percentage;
                entity.Discount = (entity.Price * entity.Discount1Percentage / 100) +
                ((entity.Price - (entity.Price * entity.Discount1Percentage / 100)) *
                 entity.Discount2Percentage / 100);

                entity.PriceInCurrency = entity.Price;
                entity.BatchNumber = string.Empty;
                entity.str.ExpiredDate = string.Empty;
                entity.IsPackage = false;
                entity.IsBonusItem = false;
                entity.IsClosed = false;
                entity.Description = userControl.Description;
                entity.Specification = userControl.Specification;

                ProcurementUtils.PopulateBalanceInfoByBlankValue(entity);

                if (chkIsInventoryItem.Checked && AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPOWithStockInfo))
                {
                    if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorByStockGroup))
                    {
                        var stockGroup = "abcd";
                        var loc = new Location();
                        loc.LoadByPrimaryKey(cboLocationID.SelectedValue ?? string.Empty);
                        if (!string.IsNullOrEmpty(loc.SRStockGroup))
                            stockGroup = loc.SRStockGroup;

                        ProcurementUtils.PopulateBalanceInfoByStockGroup(entity, stockGroup, cboLocationID.SelectedValue ?? string.Empty);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                        {
                            ProcurementUtils.PopulateBalanceInfoByItemType(entity, cboSRItemType.SelectedValue);
                        }
                    }
                }
                if (cboSRItemType.SelectedValue != ItemType.NonMedical)
                    entity.AdditionalInfo = string.Empty;
                else entity.AdditionalInfo = "Specification : " + entity.Specification;
            }
        }

        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"];

            if (IsItemConsignmentAlreadyReceived)
            {
                ToolBarMenuAdd.Visible = false;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["itype"]))
            {
                ToolBarMenuSearch.Visible = false;
                ToolBarMenuMoveNext.Enabled = false;
                ToolBarMenuMovePrev.Enabled = false;
            }
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid))
                return;

            RadGrid grd = (RadGrid)sourceControl;
            switch (grd.ID)
            {
                case "grdItemTransactionItem":
                    grdItemTransactionItem.Rebind();
                    break;
            }
        }

        protected void chkIsInventoryItem_CheckedChanged(object sender, EventArgs e)
        {
            chkIsNonMasterOrder.Enabled = AppSession.Parameter.HealthcareInitialAppsVersion != "RSCH" && !chkIsInventoryItem.Checked;
            chkIsNonMasterOrder.Checked = false;
            chkIsAssets.Enabled = !chkIsInventoryItem.Checked;//true;
            chkIsAssets.Checked = false;
            chkIsConsignment.Enabled = chkIsInventoryItem.Checked;
            chkIsConsignment.Checked = false;
        }

        protected void chkIsNonMasterOrder_CheckedChanged(object sender, EventArgs e)
        {
            chkIsAssets.Enabled = true;
            chkIsAssets.Checked = false;
            chkIsConsignment.Enabled = chkIsInventoryItem.Checked;
            chkIsConsignment.Checked = false;
        }

        protected void chkIsAssets_CheckedChanged(object sender, EventArgs e)
        {
            chkIsConsignment.Enabled = !chkIsAssets.Checked && !chkIsNonMasterOrder.Checked;
            chkIsConsignment.Checked = false;
        }

        protected void chkIsConsignment_CheckedChanged(object sender, EventArgs e)
        {
            chkIsAssets.Enabled = !chkIsConsignment.Checked;
            chkIsAssets.Checked = false;
        }
    }
}