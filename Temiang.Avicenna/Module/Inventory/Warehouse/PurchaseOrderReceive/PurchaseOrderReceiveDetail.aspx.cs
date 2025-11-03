using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class PurchaseOrderReceiveDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private bool IsGrantsReceiving
        {
            get
            {
                return (Request.QueryString["grants"] == "1");
            }
        }

        private bool IsDirectPurchase
        {
            get
            {
                return (Request.QueryString["grants"] == "2");
            }
        }
        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;
            if (cboToServiceUnitID.SelectedValue == string.Empty) return;

            var serv = new ServiceUnit();
            if (serv.LoadByPrimaryKey(cboToServiceUnitID.SelectedValue))
            {
                _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, IsGrantsReceiving ? TransactionCode.GrantsReceive : (IsDirectPurchase ? TransactionCode.DirectPurchase : TransactionCode.PurchaseOrderReceive), serv.DepartmentID);
                txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
            }
        }

        #region Page Event & Initialize
        private void InitGrantsReceiving() {
            trRefNo.Visible = !IsGrantsReceiving && !IsDirectPurchase;
            trCurrType.Visible = !IsGrantsReceiving && !IsDirectPurchase;
            trPOType.Visible = !IsGrantsReceiving;
            cboSRPurchaseOrderType.Enabled = IsDirectPurchase;
            trTaxType.Visible = !IsGrantsReceiving;
            trDeliveryNo.Visible = !IsGrantsReceiving && !IsDirectPurchase;
            pnlPphNonFixedValue.Visible = !IsGrantsReceiving && !IsDirectPurchase;
            trCons.Visible = !IsGrantsReceiving && !IsDirectPurchase;
            trConsLoc.Visible = !IsGrantsReceiving && !IsDirectPurchase;
            //pnlFooter.Visible = !IsGrantsReceiving&& !IsDirectPurchase;
            chkIsNonMasterOrder.Visible = !IsGrantsReceiving && !IsDirectPurchase;
            trProductAccount.Visible = !IsGrantsReceiving && !IsDirectPurchase;
            chkIsAssets.Visible = !IsDirectPurchase;
            //chkIsAssets.Enabled = !AppSession.Application.IsModuleAssetActive;
            cboBusinessPartnerID.Enabled = IsGrantsReceiving || IsDirectPurchase;
            chkIsInventoryItem.Enabled = IsGrantsReceiving;
            cboSRItemType.Enabled = !IsDirectPurchase;
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List

            UrlPageSearch = "#";
            UrlPageList = "PurchaseOrderReceiveList.aspx?cons=" + Request.QueryString["cons"] + "&grants=" + Request.QueryString["grants"];


            ProgramID = Request.QueryString["cons"] == "0"
                            ? AppConstant.Program.ReceivingOrder
                            : AppConstant.Program.ReceivingOrderConsignment;
            ProgramID = IsGrantsReceiving ? AppConstant.Program.GrantsReceive : (IsDirectPurchase ? AppConstant.Program.DirectPurchase : ProgramID);

            InitGrantsReceiving();

            //StandardReference Initialize
            if (!IsPostBack)
            {
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
                ComboBox.SelectedValue(cboSRItemType, BusinessObject.Reference.ItemType.Medical);

                StandardReference.Initialize(cboSRPurchaseOrderType, AppEnum.StandardReference.PurchaseOrderType);

                var productAcc = new ProductAccountCollection();
                productAcc.Query.Where(productAcc.Query.IsActive == true);
                productAcc.LoadAll();

                cboSRProductAccountID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var c in productAcc)
                {
                    cboSRProductAccountID.Items.Add(new RadComboBoxItem(c.ProductAccountName, c.ProductAccountID));
                }

                var curr = new CurrencyRateCollection();
                curr.Query.Where(curr.Query.IsActive == true);
                curr.LoadAll();
                cboCurrencyType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var entity in curr)
                {
                    cboCurrencyType.Items.Add(new RadComboBoxItem(entity.CurrencyName, entity.CurrencyID));
                }

                var funit = new ServiceUnitCollection();
                funit.Query.Where(funit.Query.IsActive == true);
                funit.LoadAll();
                cboFromServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var u in funit)
                {
                    cboFromServiceUnitID.Items.Add(new RadComboBoxItem(u.ServiceUnitName, u.ServiceUnitID));
                }

                trFromServiceUnitID.Visible = (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH");

                if (AppSession.Parameter.IsTxUsingEdDetail)
                {
                    grdItemTransactionItem.Columns.FindByUniqueName("BatchNumber").Visible = false; // batch no txt
                    grdItemTransactionItem.Columns.FindByUniqueName("ExpiredDate").Visible = false; // ed txt
                }
                else
                    grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = false; // ed txt

                if (!AppSession.Parameter.IsPphUsesAfixedValue)
                {
                    pnlPphNonFixedValue.Visible = false;
                }
                else
                {
                    StandardReference.InitializeIncludeSpace(cboSRPph, AppEnum.StandardReference.Pph);
                }

                txtTaxAmount.ReadOnly = !(AppSession.Parameter.IsPOCanEditTax);
                rblTypesOfTaxes.Enabled = AppSession.Parameter.IsPORTaxTypeEnabled || IsDirectPurchase;
                grdItemTransactionItem.Columns.FindByUniqueName("FabricName").Visible = AppSession.Parameter.IsUsingFactoryInTheItemProcurementProcess;
            }

            //Add Event for Request Order Selection
            this.AjaxManager.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(AjaxManager_AjaxRequest);
        }

        protected void txtTaxAmount_TextChanged(object sender, EventArgs e)
        {
            txtTaxPercentage.Value = (txtTaxAmount.Value / txtTransactionAmount.Value) * 100;
            CalculateDetailTransaction();
        }

        void AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            PopulateReferenceFromPurchasetOrder();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (Request.QueryString["type"] != "2") return;
            ToolBarMenuSearch.Enabled = false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItemTransactionItem, grdItemTransactionItem);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboSRItemType);
            ajax.AddAjaxSetting(grdItemTransactionItem, txtTransactionAmount);
            ajax.AddAjaxSetting(grdItemTransactionItem, txtDiscountAmount);
            ajax.AddAjaxSetting(grdItemTransactionItem, txtReceiveAmount);
            ajax.AddAjaxSetting(grdItemTransactionItem, txtAmountTaxed);
            ajax.AddAjaxSetting(grdItemTransactionItem, txtTaxAmount);
            //ajax.AddAjaxSetting(grdItemTransactionItem, txtPph22);
            //ajax.AddAjaxSetting(grdItemTransactionItem, txtPph23);
            ajax.AddAjaxSetting(grdItemTransactionItem, txtTotal);

            ajax.AddAjaxSetting(cboBusinessPartnerID, txtTaxPercentage);
            ajax.AddAjaxSetting(cboBusinessPartnerID, txtTaxAmount);
            ajax.AddAjaxSetting(cboBusinessPartnerID, txtTotal);

            ajax.AddAjaxSetting(rblTypesOfTaxes, txtTaxPercentage);
            ajax.AddAjaxSetting(rblTypesOfTaxes, txtTaxAmount);
            ajax.AddAjaxSetting(rblTypesOfTaxes, txtTotal);
            //ajax.AddAjaxSetting(rblTypesOfTaxes, txtPph22);
            //ajax.AddAjaxSetting(rblTypesOfTaxes, txtPph23);

            ajax.AddAjaxSetting(AjaxManager, txtTransactionAmount);
            ajax.AddAjaxSetting(AjaxManager, txtDiscountAmount);
            ajax.AddAjaxSetting(AjaxManager, txtReceiveAmount);
            ajax.AddAjaxSetting(AjaxManager, txtAmountTaxed);
            ajax.AddAjaxSetting(AjaxManager, txtTaxPercentage);
            ajax.AddAjaxSetting(AjaxManager, txtTaxAmount);
            ajax.AddAjaxSetting(AjaxManager, txtTotal);
            //ajax.AddAjaxSetting(AjaxManager, txtPph22);
            //ajax.AddAjaxSetting(AjaxManager, txtPph23);

            ajax.AddAjaxSetting(cboToServiceUnitID, cboToServiceUnitID);
            ajax.AddAjaxSetting(cboToServiceUnitID, cboToLocationID);
            ajax.AddAjaxSetting(cboToServiceUnitID, txtTransactionNo);

            ajax.AddAjaxSetting(txtDiscountAmount, txtReceiveAmount);
            ajax.AddAjaxSetting(txtDiscountAmount, txtAmountTaxed);
            ajax.AddAjaxSetting(txtDiscountAmount, txtTaxAmount);
            ajax.AddAjaxSetting(txtDiscountAmount, txtTotal);
            //ajax.AddAjaxSetting(txtDiscountAmount, txtPph22);
            //ajax.AddAjaxSetting(txtDiscountAmount, txtPph23);

            ajax.AddAjaxSetting(txtShippingCharges, txtShippingCharges);
            ajax.AddAjaxSetting(txtShippingCharges, txtTotal);
            //ajax.AddAjaxSetting(txtShippingCharges, txtPph22);
            //ajax.AddAjaxSetting(txtShippingCharges, txtPph23);

            ajax.AddAjaxSetting(txtStampAmount, txtStampAmount);
            ajax.AddAjaxSetting(txtStampAmount, txtTotal);
            //ajax.AddAjaxSetting(txtStampAmount, txtPph22);
            //ajax.AddAjaxSetting(txtStampAmount, txtPph23);

            ajax.AddAjaxSetting(txtAdvanceAmount, txtAdvanceAmount);
            ajax.AddAjaxSetting(txtAdvanceAmount, txtTotal);
            //ajax.AddAjaxSetting(txtAdvanceAmount, txtPph22);
            //ajax.AddAjaxSetting(txtAdvanceAmount, txtPph23);

            //Purchase Order Selection
            ajax.AddAjaxSetting(AjaxManager, txtReferenceNo);
            ajax.AddAjaxSetting(AjaxManager, cboSRItemType);
            ajax.AddAjaxSetting(AjaxManager, grdItemTransactionItem);
            ajax.AddAjaxSetting(AjaxManager, cboBusinessPartnerID);
            ajax.AddAjaxSetting(AjaxManager, cboSRPurchaseOrderType);
            ajax.AddAjaxSetting(AjaxManager, cboSRProductAccountID);
            ajax.AddAjaxSetting(AjaxManager, txtDiscountAmount);
            ajax.AddAjaxSetting(AjaxManager, txtShippingCharges);
            ajax.AddAjaxSetting(AjaxManager, txtAdvanceAmount);
            ajax.AddAjaxSetting(AjaxManager, chkIsInventoryItem);
            ajax.AddAjaxSetting(AjaxManager, chkIsNonMasterOrder);
            ajax.AddAjaxSetting(AjaxManager, chkIsAssets);
            ajax.AddAjaxSetting(AjaxManager, chkIsConsignment);
            ajax.AddAjaxSetting(AjaxManager, chkIsConsignmentAlreadyReceived);
            ajax.AddAjaxSetting(AjaxManager, cboCurrencyType);
            ajax.AddAjaxSetting(AjaxManager, txtCurrencyRate);
            ajax.AddAjaxSetting(AjaxManager, cboToServiceUnitID);
            ajax.AddAjaxSetting(AjaxManager, cboToLocationID);
            ajax.AddAjaxSetting(AjaxManager, rblTypesOfTaxes);
            ajax.AddAjaxSetting(AjaxManager, txtTaxPercentage);
            ajax.AddAjaxSetting(AjaxManager, cboFromLocationID);

            if (AppSession.Parameter.IsPphUsesAfixedValue)
            {
                ajax.AddAjaxSetting(grdItemTransactionItem, txtPphAmount);
                ajax.AddAjaxSetting(rblTypesOfTaxes, txtPphAmount);

                ajax.AddAjaxSetting(AjaxManager, cboSRPph);
                ajax.AddAjaxSetting(AjaxManager, txtPphPercentage);
                ajax.AddAjaxSetting(AjaxManager, txtPphAmount);

                ajax.AddAjaxSetting(txtDiscountAmount, txtPphAmount);
                ajax.AddAjaxSetting(txtShippingCharges, txtPphAmount);
                ajax.AddAjaxSetting(txtStampAmount, txtPphAmount);
                ajax.AddAjaxSetting(txtAdvanceAmount, txtPphAmount);

                ajax.AddAjaxSetting(cboSRPph, txtPphPercentage);
                ajax.AddAjaxSetting(cboSRPph, txtPphAmount);
            }
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuEditClick()
        {
            string serviceUnitID = cboToServiceUnitID.SelectedValue;
            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, IsGrantsReceiving ? TransactionCode.GrantsReceive : (IsDirectPurchase ? TransactionCode.DirectPurchase : TransactionCode.PurchaseOrderReceive), true);
            cboToServiceUnitID.SelectedValue = serviceUnitID;
            txtTransactionDate.Enabled = AppSession.Parameter.IsAllowEditPorDate;
            if (grdItemTransactionItem.Columns.FindByUniqueName("ExpiredDate").Visible == false)
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = false; // ed ico
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new ItemTransaction();
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
            var c = ItemTransactionItems;
            if (c.Count == 0)
            {
                args.MessageText = "Data can't be approved because detail is empty. Please check back your data.";
                args.IsCancel = true;
                return;
            }

            //var isValidate = c.All(item => item.IsAccEd != false && item.IsAccPrice != false && item.IsAccQty != false);
            //if (isValidate == false)
            //{
            //    args.MessageText = "Data can't be approved because there are items that need confirmation. Please contact your supervisor.";
            //    args.IsCancel = true;
            //    return;
            //}

            if (chkIsInventoryItem.Checked && AppSession.Parameter.IsTxUsingEdDetail)
            {
                var msg = string.Empty;
                foreach (var item in c)
                {
                    if (item.IsControlExpired)
                    {
                        decimal qty = (item.Quantity ?? 0) * (item.ConversionFactor ?? 0);
                        var ed = new ItemTransactionItemEdCollection();
                        ed.Query.Where(ed.Query.TransactionNo == item.TransactionNo,
                                       ed.Query.SequenceNo == item.SequenceNo);
                        ed.LoadAll();
                        decimal qtyDt = ed.Sum(i => (i.Quantity ?? 0) * (i.ConversionFactor ?? 0));

                        if (qty != qtyDt)
                        {
                            if (msg == string.Empty)
                                msg = item.ItemID;
                            else
                                msg += ", " + item.ItemID;
                        }
                    }
                    else
                    {
                        DateTime defDate = DateTime.Parse("1/1/2999");
                        var ed = new ItemTransactionItemEd();
                        if (!ed.LoadByPrimaryKey(txtTransactionNo.Text, item.SequenceNo, defDate, "-N/A-"))
                            ed.AddNew();
                        ed.TransactionNo = txtTransactionNo.Text;
                        ed.SequenceNo = item.SequenceNo;
                        ed.ExpiredDate = defDate;
                        ed.BatchNumber = "-N/A-";
                        ed.ItemID = item.ItemID;
                        ed.Quantity = item.Quantity;
                        ed.SRItemUnit = item.SRItemUnit;
                        ed.ConversionFactor = item.ConversionFactor;
                        ed.QuantityFinishInBaseUnit = 0;
                        ed.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        ed.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        ed.IsClosed = false;
                        ed.ClosedDateTime = null;
                        ed.ClosedByUserID = null;
                        ed.ReferenceNo = string.Empty;
                        ed.ReferenceSequenceNo = string.Empty;
                        ed.Save();
                    }
                }
                if (msg != string.Empty)
                {
                    args.MessageText = "Data can't be approved. Quantity detail Expiry Date for item: " + msg + " does not match the total quantity received.";
                    args.IsCancel = true;
                    return;
                }
            }

            string itemZeroCostPrice;
            var entity = new ItemTransaction();
            entity.LoadByPrimaryKey(txtTransactionNo.Text);

            ItemTransaction.UpdateCostPriceForPOR(entity, ItemTransactionItems, out itemZeroCostPrice);

            //-db 20230612 : dipindah ke function save
            //if (entity.TransactionCode != BusinessObject.Reference.TransactionCode.GrantsReceive) {
            //    var msg2 = ItemTransaction.IsExceedOrderQuantityForPurchaseOrderReceived(ItemTransactionItems);
            //    if (msg2 != string.Empty)
            //    {
            //        args.MessageText = msg2;
            //        args.IsCancel = true;
            //        return;
            //    }
            //}
            
            if (chkIsNonMasterOrder.Checked)
            {
                var retval = (new ItemTransaction()).ApproveNonMaster(txtTransactionNo.Text, ItemTransactionItems, AppSession.UserLogin.UserID);
                if (retval != string.Empty)
                {
                    args.MessageText = retval;
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                if (entity.IsConsignment == true && entity.IsConsignmentAlreadyReceived == false)
                {
                    var msg =
                        ItemTransaction.IsItemMinusProcessForPurchaseOrderReceivedConsignment(txtTransactionNo.Text,
                                                                                              ItemTransactionItems,
                                                                                              entity.FromLocationID);
                    if (msg != string.Empty)
                    {
                        args.MessageText = msg;
                        args.IsCancel = true;
                        return;
                    }
                }

                var str = (new ItemTransaction()).Approve(txtTransactionNo.Text, ItemTransactionItems, AppSession.UserLogin.UserID, AppSession.Parameter.RoundingTransaction);
                if (!string.IsNullOrEmpty(str))
                {
                    args.MessageText = str;
                    args.IsCancel = true;

                    this.LogError(new Exception(str));
                }
            }

            if (grdItemTransactionItem.Columns.FindByUniqueName("ExpiredDate").Visible == false)
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = false; // ed ico
        }
        public override bool OnGetStatusMenuUnApprovalEnabled()
        {
            return !PostingStatus.IsUnApproveDisabledIfPerClosed(txtTransactionDate.SelectedDate.Value);
        }
        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
            {
                var entity = new ItemTransaction();
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
                if (entity.IsInventoryItem == true)
                {
                    args.MessageText = "Un-Approved process is invalid. Purchase Order Receive for Inventory Item can't be canceled.";
                    args.IsCancel = true;
                    return;
                }

                (new ItemTransaction()).UnApproved(entity, ItemTransactionItems, AppSession.UserLogin.UserID);

                if (grdItemTransactionItem.Columns.FindByUniqueName("ExpiredDate").Visible == false)
                    grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = true; // ed ico

            }
            else
            {
                //var entity = new ItemTransaction();
                //entity.LoadByPrimaryKey(txtTransactionNo.Text);
                //if (entity.ApprovedDate.Value.Date == DateTime.Now.Date)
                //{
                //    (new ItemTransaction()).Void(entity, ItemTransactionItems, AppSession.UserLogin.UserID);
                //}
                //else
                //{
                args.MessageText = "Un-Approved is invalid.";
                args.IsCancel = true;
                //}
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            (new ItemTransaction()).Void(txtTransactionNo.Text, AppSession.UserLogin.UserID);
            if (grdItemTransactionItem.Columns.FindByUniqueName("ExpiredDate").Visible == false)
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = false; // ed ico
        }
        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            (new ItemTransaction()).UnVoid(txtTransactionNo.Text, AppSession.UserLogin.UserID);
            if (grdItemTransactionItem.Columns.FindByUniqueName("ExpiredDate").Visible == false)
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = true; // ed ico
        }

        private bool IsApprovedOrVoid(ItemTransaction entity, ValidateArgs args)
        {
            if (entity.IsApproved.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }
            return true;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ItemTransaction());
            PopulateNewTransactionNo();
            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, IsGrantsReceiving ? TransactionCode.GrantsReceive : (IsDirectPurchase ? TransactionCode.DirectPurchase : TransactionCode.PurchaseOrderReceive), true);
            cboToServiceUnitID.SelectedIndex = 0;
            cboToLocationID.Items.Clear();
            cboToLocationID.Text = string.Empty;
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer(); //DateTime.Now;
            txtTransactionDate.Enabled = AppSession.Parameter.IsAllowEditPorDate;
            txtInvoiceSupplierDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            btnGetItem.Enabled = true;
            btnResetItem.Enabled = true;
            chkIsInventoryItem.Checked = IsGrantsReceiving || IsDirectPurchase;
            if (IsDirectPurchase)
            {
                txtInvoiceNo.Text = "-";
                rblTypesOfTaxes.SelectedIndex = 2;
                cboSRPurchaseOrderType.SelectedValue = "CS";
                cboSRItemType.SelectedValue = ItemType.Medical;
            }
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
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
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

            var a = new ItemTransaction();
            a.LoadByPrimaryKey(txtReferenceNo.Text);

            if (chkIsNonMasterOrder.Checked && cboSRProductAccountID.SelectedValue == string.Empty)
            {
                args.MessageText = "Product Account ID required.";
                args.IsCancel = true;
                return;
            }

            if (trFromServiceUnitID.Visible && string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue) && !chkIsInventoryItem.Checked)
            {
                if (!chkIsInventoryItem.Checked)
                {
                    args.MessageText = "Cost For Unit required.";
                    args.IsCancel = true;
                    return;
                }
            }

            if (chkIsConsignment.Checked && string.IsNullOrEmpty(cboFromLocationID.SelectedValue))
            {
                args.MessageText = "Location For Consignment required.";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
            {
                args.MessageText = "Service Unit required.";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboSRItemType.SelectedValue))
            {
                args.MessageText = "Item Type required.";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboBusinessPartnerID.SelectedValue))
            {
                args.MessageText = "Supplier required.";
                args.IsCancel = true;
                return;
            }

            if (trRefNo.Visible && txtReferenceNo.Text == string.Empty)
            {
                args.MessageText = "Purchase Order No required.";
                args.IsCancel = true;
                return;
            }

            entity = new ItemTransaction();
            entity.AddNew();
            SetEntityValue(entity);
            if (ItemTransactionItems.Where(x => x.TransactionNo == txtTransactionNo.Text).Count() == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            if (entity.TransactionCode != BusinessObject.Reference.TransactionCode.GrantsReceive)
            {
                if (entity.TransactionCode != BusinessObject.Reference.TransactionCode.DirectPurchase)
                {
                    var msg2 = ItemTransaction.IsExceedOrderQuantityForPurchaseOrderReceived(ItemTransactionItems);
                    if (msg2 != string.Empty)
                    {
                        args.MessageText = msg2;
                        args.IsCancel = true;
                        return;
                    }
                }
            }

            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (chkIsConsignment.Checked && string.IsNullOrEmpty(cboFromLocationID.SelectedValue))
            {
                args.MessageText = "Location For Consignment required.";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
            {
                args.MessageText = "Service Unit required.";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboSRItemType.SelectedValue))
            {
                args.MessageText = "Item Type required.";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboBusinessPartnerID.SelectedValue))
            {
                args.MessageText = "Supplier required.";
                args.IsCancel = true;
                return;
            }

            if (trRefNo.Visible && txtReferenceNo.Text == string.Empty)
            {
                args.MessageText = "Purchase Order No required.";
                args.IsCancel = true;
                return;
            }

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

                if (entity.TransactionCode != BusinessObject.Reference.TransactionCode.GrantsReceive)
                {
                    var msg2 = ItemTransaction.IsExceedOrderQuantityForPurchaseOrderReceived(ItemTransactionItems);
                    if (msg2 != string.Empty)
                    {
                        args.MessageText = msg2;
                        args.IsCancel = true;
                        return;
                    }
                }

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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "ItemTransaction";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            switch (programID)
            {
                case AppConstant.Report.PurchaseReceiveByInvoiceSupplierNoSlip:
                    printJobParameters.AddNew("p_InvoiceNo", txtInvoiceNo.Text);
                    break;
                default:
                    printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
                    break;
            }
        }

        #endregion

        #region ToolBar Menu Support
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
            //return true;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
            btnGetItem.Enabled = newVal != AppEnum.DataMode.Read;
            btnResetItem.Enabled = newVal != AppEnum.DataMode.Read;
            chkIsTaxable.Enabled = newVal == AppEnum.DataMode.New;

            txtTaxAmount.ReadOnly = !(AppSession.Parameter.IsPOCanEditTax);
            rblTypesOfTaxes.Enabled = AppSession.Parameter.IsPORTaxTypeEnabled || IsDirectPurchase;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ItemTransaction();
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
            var itemTransaction = (ItemTransaction)entity;
            txtTransactionNo.Text = itemTransaction.TransactionNo;
            txtTransactionDate.SelectedDate = itemTransaction.TransactionDate;
            //txtBusinessPartnerID.Text = itemTransaction.BusinessPartnerID;

            var eArgs = new RadComboBoxItemsRequestedEventArgs();
            eArgs.Text = itemTransaction.BusinessPartnerID ?? "";
            cboSupplier_ItemsRequested(cboBusinessPartnerID, eArgs);
            var cboi = cboBusinessPartnerID.Items.FindItemByValue(itemTransaction.BusinessPartnerID ?? "");
            if (cboi != null)
            {
                cboBusinessPartnerID.SelectedValue = cboi.Value;
            }
            else {
                //cboBusinessPartnerID.SelectedIndex = 0;
            }

            txtInvoiceNo.Text = itemTransaction.str.InvoiceNo;
            if (itemTransaction.InvoiceSupplierDate != null)
                txtInvoiceSupplierDate.SelectedDate = itemTransaction.InvoiceSupplierDate;
            txtDeliveryOrdersNo.Text = itemTransaction.str.DeliveryOrdersNo;
            txtReferenceNo.Text = itemTransaction.ReferenceNo;
            ComboBox.PopulateWithOneServiceUnit(cboToServiceUnitID, itemTransaction.ToServiceUnitID ?? string.Empty);
            if (!string.IsNullOrEmpty(itemTransaction.ToServiceUnitID))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboToLocationID, itemTransaction.ToServiceUnitID);
                if (!string.IsNullOrEmpty(itemTransaction.ToLocationID))
                    cboToLocationID.SelectedValue = itemTransaction.ToLocationID;
                else cboToLocationID.SelectedIndex = 1;
            }
            cboSRItemType.SelectedValue = itemTransaction.SRItemType;
            cboFromServiceUnitID.SelectedValue = itemTransaction.FromServiceUnitID;

            ComboBox.SelectedValue(cboSRPurchaseOrderType, itemTransaction.SRPurchaseOrderType);
            cboSRProductAccountID.SelectedValue = itemTransaction.ProductAccountID;

            ComboBox.SelectedValue(cboCurrencyType, AppParameter.GetParameterValue(AppParameter.ParameterItem.CurrencyRupiahID));
            var curr = new CurrencyRate();
            curr.LoadByPrimaryKey(cboCurrencyType.SelectedValue);
            txtCurrencyRate.Value = Convert.ToDouble(curr.CurrencyRate);

            txtNotes.Text = itemTransaction.Notes;
            chkIsVoid.Checked = itemTransaction.IsVoid ?? false;
            chkIsApproved.Checked = itemTransaction.IsApproved ?? false;
            chkIsTaxable.Checked = itemTransaction.IsTaxable == 1;
            //ViewState["TaxPercentage" + Request.UserHostName] = itemTransaction.TaxPercentage;
            txtTransactionAmount.Value = Convert.ToDouble(itemTransaction.DiscountAmount + itemTransaction.ChargesAmount);
            txtDiscountAmount.Value = Convert.ToDouble(itemTransaction.DiscountAmount);
            txtReceiveAmount.Value = Convert.ToDouble(itemTransaction.ChargesAmount);
            txtAmountTaxed.Value = Convert.ToDouble(itemTransaction.AmountTaxed);
            txtTaxAmount.Value = Convert.ToDouble(itemTransaction.TaxAmount);
            txtTaxPercentage.Value = Convert.ToDouble(itemTransaction.TaxPercentage);

            cboSRPph.SelectedValue = itemTransaction.SRPph;
            txtPphPercentage.Value = Convert.ToDouble(itemTransaction.PphPercentage);
            txtPphAmount.Value = Convert.ToDouble(itemTransaction.PphAmount);

            txtShippingCharges.Value = Convert.ToDouble(itemTransaction.DownPaymentAmount);
            txtStampAmount.Value = Convert.ToDouble(itemTransaction.StampAmount);
            txtAdvanceAmount.Value = Convert.ToDouble(itemTransaction.AdvanceAmount);

            chkIsInventoryItem.Checked = itemTransaction.IsInventoryItem ?? false;
            chkIsNonMasterOrder.Checked = itemTransaction.IsNonMasterOrder ?? false;
            chkIsAssets.Checked = itemTransaction.IsAssets ?? false;
            chkIsConsignment.Checked = itemTransaction.IsConsignment ?? false;
            chkIsConsignmentAlreadyReceived.Checked = itemTransaction.IsConsignmentAlreadyReceived ?? false;
            if (chkIsConsignment.Checked)
            {
                ComboBox.PopulateWithSupplierForLocation(cboFromLocationID, cboBusinessPartnerID.SelectedValue);
                cboFromLocationID.SelectedValue = itemTransaction.FromLocationID;
            }
            else
            {
                cboFromLocationID.Items.Clear();
                cboFromLocationID.Text = string.Empty;
            }

            //Display Data Detail
            PopulateGridDetail();
            btnGetItem.Enabled = false;
            btnResetItem.Enabled = false;

            //CalculateDetailTransaction();

            txtReceiveAmount.Value = Convert.ToDouble(itemTransaction.ChargesAmount ?? 0);
            txtDiscountAmount.Value = Convert.ToDouble(itemTransaction.DiscountAmount ?? 0);
            txtAmountTaxed.Value = Convert.ToDouble(itemTransaction.AmountTaxed ?? 0);

            txtTaxAmount.Value = Convert.ToDouble(itemTransaction.TaxAmount ?? 0);
            txtTaxPercentage.Value = Convert.ToDouble(itemTransaction.TaxPercentage ?? 0);

            if (itemTransaction.IsTaxable != null)
                rblTypesOfTaxes.SelectedIndex = itemTransaction.IsTaxable == 2 ? 2 : (itemTransaction.IsTaxable == 1 ? 0 : 1);

            CalculateTotal();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(ItemTransaction entity)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionCode = IsGrantsReceiving ? TransactionCode.GrantsReceive : (IsDirectPurchase ? TransactionCode.DirectPurchase : TransactionCode.PurchaseOrderReceive);
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.BusinessPartnerID = cboBusinessPartnerID.SelectedValue;
            entity.InvoiceNo = txtInvoiceNo.Text;
            if (!txtInvoiceSupplierDate.IsEmpty)
                entity.InvoiceSupplierDate = txtInvoiceSupplierDate.SelectedDate;
            else
                entity.str.InvoiceSupplierDate = string.Empty;
            entity.DeliveryOrdersNo = txtDeliveryOrdersNo.Text;
            entity.ReferenceNo = txtReferenceNo.Text;
            entity.ToServiceUnitID = cboToServiceUnitID.SelectedItem.Value;
            entity.ToLocationID = cboToLocationID.SelectedValue;
            
            entity.SRItemType = cboSRItemType.SelectedValue;
            //-db (6/6/2023): ditambah default value u/ kasus di rsi dimana por consignment jd terisi "020" (belum nemu sumber masalahnya)
            entity.SRPurchaseOrderType = (cboSRPurchaseOrderType.SelectedValue == "CS" || cboSRPurchaseOrderType.SelectedValue == "CR") ? cboSRPurchaseOrderType.SelectedValue : (IsDirectPurchase ? "CS" : "CR");
            entity.Notes = txtNotes.Text;
            entity.IsTaxable = Convert.ToInt16(rblTypesOfTaxes.SelectedIndex == 0 ? 1 : (rblTypesOfTaxes.SelectedIndex == 1 ? 0 : 2));
            entity.ChargesAmount = Convert.ToDecimal(txtReceiveAmount.Value);
            entity.AmountTaxed = Convert.ToDecimal(txtAmountTaxed.Value);
            entity.TaxAmount = entity.IsTaxable == 1 ? Convert.ToDecimal(txtTaxAmount.Value) : (entity.IsTaxable == 0 ? Convert.ToDecimal(txtTaxAmount.Value/* + 0.01D <-- angka ini buat apa ya???*/) : 0M);
            entity.TaxPercentage = Convert.ToDecimal(txtTaxPercentage.Value);
            entity.ProductAccountID = cboSRProductAccountID.SelectedValue;
            entity.CurrencyID = cboCurrencyType.SelectedValue;
            entity.CurrencyRate = Convert.ToDecimal(txtCurrencyRate.Value);
            entity.DiscountAmount = Convert.ToDecimal(txtDiscountAmount.Value);

            entity.SRPph = cboSRPph.SelectedValue;
            entity.PphPercentage = Convert.ToDecimal(txtPphPercentage.Value);
            entity.PphAmount = Convert.ToDecimal(txtPphAmount.Value);

            entity.DownPaymentAmount = Convert.ToDecimal(txtShippingCharges.Value);
            entity.StampAmount = Convert.ToDecimal(txtStampAmount.Value);
            entity.AdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Value);

            entity.IsNonMasterOrder = chkIsNonMasterOrder.Checked;
            entity.IsInventoryItem = chkIsInventoryItem.Checked;
            entity.IsAssets = chkIsAssets.Checked;
            entity.IsConsignment = chkIsConsignment.Checked;
            entity.IsConsignmentAlreadyReceived = chkIsConsignmentAlreadyReceived.Checked;
            entity.FromLocationID = cboFromLocationID.SelectedValue;

            var refs = new ItemTransaction();
            refs.LoadByPrimaryKey(txtReferenceNo.Text);
            entity.TermOfPayment = refs.TermOfPayment;
            entity.FromServiceUnitID = refs.ToServiceUnitID;
            entity.IsConsignmentAlreadyReceived = refs.IsConsignmentAlreadyReceived ?? false;
            entity.SRItemCategory = refs.SRItemCategory;
            entity.SRPurchaseCategorization = refs.SRPurchaseCategorization;
            entity.SRProcurementType = refs.SRProcurementType;
            if (trFromServiceUnitID.Visible)
                entity.ServiceUnitCostID = cboFromServiceUnitID.SelectedValue;
            else
                entity.ServiceUnitCostID = refs.ServiceUnitCostID;
                
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer(); //DateTime.Now;
            }

            //Update Detil
            foreach (ItemTransactionItem item in ItemTransactionItems)
            {
                if (item.es.IsAdded)
                {
                    item.TransactionNo = txtTransactionNo.Text;
                }
                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer(); //DateTime.Now;
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
            var que = new ItemTransactionQuery("a");
            var qusr = new AppUserServiceUnitQuery("u");
            que.InnerJoin(qusr).On(que.ToServiceUnitID == qusr.ServiceUnitID &&
                                         qusr.UserID == AppSession.UserLogin.UserID);

            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TransactionNo > txtTransactionNo.Text && que.TransactionCode == (IsGrantsReceiving ? TransactionCode.GrantsReceive : (IsDirectPurchase ? TransactionCode.DirectPurchase : TransactionCode.PurchaseOrderReceive)));
                if (Request.QueryString["cons"] == "0")
                    que.Where(que.IsConsignment == false);
                else que.Where(que.IsConsignment == true);
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < txtTransactionNo.Text && que.TransactionCode == (IsGrantsReceiving ? TransactionCode.GrantsReceive : (IsDirectPurchase ? TransactionCode.DirectPurchase : TransactionCode.PurchaseOrderReceive)));
                if (Request.QueryString["cons"] == "0")
                    que.Where(que.IsConsignment == false);
                else que.Where(que.IsConsignment == true);
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new ItemTransaction();
            entity.Load(que);
            OnPopulateEntryControl(entity);

            if (grdItemTransactionItem.Columns.FindByUniqueName("ExpiredDate").Visible == false)
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = !chkIsApproved.Checked && !chkIsVoid.Checked; // ed ico

        }

        #endregion

        #region Method & Event TextChanged

        protected void cboToServiceUnitID_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateNewTransactionNo();
            ComboBox.PopulateWithServiceUnitForLocation(cboToLocationID, cboToServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                cboToLocationID.SelectedIndex = 1;
            else
                cboToLocationID.SelectedIndex = 0;
        }

        protected void cboBusinessPartnerID_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateCalculateTax();
        }

        private void PopulateCalculateTax()
        {
            var supp = new Supplier();
            supp.LoadByPrimaryKey(cboBusinessPartnerID.SelectedValue);

            if (rblTypesOfTaxes.SelectedIndex == 0)
                txtTaxPercentage.Value = Convert.ToDouble(supp.TaxPercentage ?? 0);
            else
                txtTaxPercentage.Value = 0;

            CalculateTax();
        }

        protected void txtReferenceNo_TextChanged(object sender, EventArgs e)
        {
            PopulateReferenceFromPurchasetOrder();
        }

        private void PopulateReferenceFromPurchasetOrder()
        {
            object obj = Session["POR_W3TOT:ItemSelected" + Request.UserHostName];
            if (obj == null) return;

            //delete previouse item
            if (ItemTransactionItems.Count > 0)
                ItemTransactionItems.MarkAllAsDeleted();

            DataTable dtbSelectedItem = (DataTable)obj;
            if (dtbSelectedItem.Rows.Count > 0)
                txtReferenceNo.Text = dtbSelectedItem.Rows[0]["TransactionNo"].ToString();

            //for header
            var header = new ItemTransaction();
            header.LoadByPrimaryKey(txtReferenceNo.Text);
            //txtBusinessPartnerID.Text = header.BusinessPartnerID;

            var eArgs = new RadComboBoxItemsRequestedEventArgs();
            eArgs.Text = header.BusinessPartnerID ?? "";
            cboSupplier_ItemsRequested(cboBusinessPartnerID, eArgs);
            var cboi = cboBusinessPartnerID.Items.FindItemByValue(header.BusinessPartnerID ?? "");
            if (cboi != null)
            {
                cboBusinessPartnerID.SelectedValue = cboi.Value;
            }
            else
            {
                cboBusinessPartnerID.SelectedIndex = 0;
            }

            txtCurrencyRate.Value = Convert.ToDouble(header.CurrencyRate);
            cboCurrencyType.SelectedValue = header.CurrencyID;
            chkIsInventoryItem.Checked = header.IsInventoryItem ?? false;
            chkIsNonMasterOrder.Checked = header.IsNonMasterOrder ?? false;
            chkIsAssets.Checked = header.IsAssets ?? false;
            chkIsConsignment.Checked = header.IsConsignment ?? false;
            chkIsConsignmentAlreadyReceived.Checked = header.IsConsignmentAlreadyReceived ?? false;
            chkIsAssets.Enabled = !(chkIsConsignment.Checked); //&& !AppSession.Application.IsModuleAssetActive;
            rblTypesOfTaxes.SelectedIndex = header.IsTaxable == 2 ? 2 : (header.IsTaxable == 1 ? 0 : 1);
                
            if (chkIsConsignment.Checked)
            {
                ComboBox.PopulateWithSupplierForLocation(cboFromLocationID, cboBusinessPartnerID.SelectedValue);
                cboFromLocationID.SelectedIndex = cboFromLocationID.Items.Count == 2 ? 1 : 0;
            }
            else
            {
                cboFromLocationID.Items.Clear();
                cboFromLocationID.Text = string.Empty;
            }

            if (chkIsNonMasterOrder.Checked)
            {
                cboSRProductAccountID.Enabled = true;
                cboSRProductAccountID.SelectedValue = header.ProductAccountID;
            }
            else
            {
                cboSRProductAccountID.Enabled = false;
                cboSRProductAccountID.SelectedValue = string.Empty;
            }

            txtDiscountAmount.Value = Convert.ToDouble(header.DiscountAmount);
            txtShippingCharges.Value = Convert.ToDouble(header.DownPaymentAmount);
            txtAdvanceAmount.Value = Convert.ToDouble(header.AdvanceAmount);

            cboSRItemType.SelectedValue = header.SRItemType;
            ComboBox.SelectedValue(cboSRPurchaseOrderType, header.SRPurchaseOrderType);
            //txtTaxPercentage.Value = Convert.ToDouble(header.TaxPercentage);

            if (AppSession.Parameter.IsPorTaxBasedOnPo)
                txtTaxPercentage.Value = (header.IsTaxable == 2) ? 0 : ((header.IsTaxable == 0) ? Convert.ToDouble(AppSession.Parameter.TaxPercentage) : Convert.ToDouble(header.TaxPercentage));
            else
            {
                if (header.IsTaxable == 2)
                    txtTaxPercentage.Value = 0;
                else
                {
                    var s = new Supplier();
                    if (s.LoadByPrimaryKey(cboBusinessPartnerID.SelectedValue))
                        txtTaxPercentage.Value = Convert.ToDouble(s.TaxPercentage ?? 0);
                    else
                        txtTaxPercentage.Value = Convert.ToDouble(AppSession.Parameter.TaxPercentage);
                }
            }

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(header.FromServiceUnitID);
            if (!string.IsNullOrEmpty(unit.ServiceUnitPorID))
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, IsGrantsReceiving ? TransactionCode.GrantsReceive : (IsDirectPurchase ? TransactionCode.DirectPurchase : TransactionCode.PurchaseOrderReceive), true);
                cboToServiceUnitID.SelectedValue = unit.ServiceUnitPorID;

                cboToServiceUnitID_OnSelectedIndexChanged(cboToServiceUnitID,
                new RadComboBoxSelectedIndexChangedEventArgs(cboToServiceUnitID.Text, string.Empty,
                    cboToServiceUnitID.SelectedValue, string.Empty));

                cboToLocationID.SelectedValue = !string.IsNullOrEmpty(unit.LocationPorID)
                    ? (chkIsConsignment.Checked && cboToLocationID.SelectedValue != string.Empty ? cboToLocationID.SelectedValue : unit.LocationPorID)
                                                    : unit.GetMainLocationId(cboToServiceUnitID.SelectedValue);
            }

            string seqNo;
            int i = 0;

            //decimal tax = 0;
            decimal total = 0;

            foreach (DataRow row in dtbSelectedItem.Rows)
            {
                if (Convert.ToDecimal(row["QtyRecv"]) == 0 || Convert.ToBoolean(row["IsSelect"]) == false) continue;
                i++;
                seqNo = string.Format("{0:000}", i);
                ItemTransactionItem entity = ItemTransactionItems.AddNew();

                //ViewState["TaxPercentage" + Request.UserHostName] = Convert.ToDecimal(row["TaxPercentage"]);
                entity.ItemID = row["ItemID"].ToString();
                entity.SequenceNo = seqNo;
                entity.ReferenceNo = row["TransactionNo"].ToString();
                entity.ReferenceSequenceNo = row["SequenceNo"].ToString();
                entity.Description = row["Description"].ToString();
                entity.FabricID = row["FabricID"].ToString();
                entity.FabricName = row["FabricName"].ToString();
                entity.Quantity = Convert.ToDecimal(row["QtyRecv"]);
                entity.ConversionFactor = Convert.ToDecimal(row["ConversionFactor"]);
                entity.SRItemUnit = row["SRItemUnit"].ToString();

                if (row["ExpiredDate"] != null && Convert.ToDateTime(row["ExpiredDate"]).Year > 1900)
                    entity.ExpiredDate = Convert.ToDateTime(row["ExpiredDate"]);
                else
                    entity.str.ExpiredDate = string.Empty;

                entity.BatchNumber = row["BatchNumber"].ToString();
                entity.IsDiscountInPercent = (bool)row["IsDiscountInPercent"];
                entity.IsBonusItem = (bool)row["IsBonusItem"];
                entity.IsAccEd = (bool)row["IsAccEd"];
                entity.IsAccPrice = (bool)row["IsAccPrice"];
                entity.IsAccQty = (bool)row["IsAccQty"];
                entity.IsControlExpired = (bool)row["IsControlExpired"];
                entity.IsTaxable = (bool)row["IsTaxable"];
                entity.IsNotCompleteED = (bool)row["IsNotCompleteED"];

                if (entity.IsBonusItem == true)
                {
                    entity.Discount1Percentage = 0;
                    entity.Discount2Percentage = 0;
                    entity.Discount = 0;
                    entity.Price = 0;
                    entity.PriceInCurrency = 0;
                    entity.DiscountInCurrency = 0;
                }
                else
                {
                    entity.Discount1Percentage = (decimal)row["Discount1Percentage"];
                    entity.Discount2Percentage = (decimal)row["Discount2Percentage"];
                    entity.Discount = (decimal)row["Discount"];

                    if (header.IsTaxable == 0)
                    {
                        //var prices = Helper.GetReversePriceValueV2((decimal)row["Price"], entity.Discount1Percentage ?? 0, entity.Discount ?? 0);
                        var prices = Helper.GetReversePriceValueV2((decimal)row["Price"], entity.Discount1Percentage ?? 0, entity.Discount2Percentage ?? 0, entity.Discount ?? 0, Convert.ToDecimal(txtTaxPercentage.Value) / 100);

                        entity.Price = prices[0];
                        entity.Discount = prices[1];
                        //tax += prices[3];
                    }
                    else
                    {
                        entity.Price = (decimal)row["Price"];
                        entity.Discount = (decimal)row["Discount"];
                    }

                    entity.PriceInCurrency = entity.Price * (Convert.ToDecimal(header.CurrencyRate) == 0 ? 1 : Convert.ToDecimal(header.CurrencyRate));
                    entity.DiscountInCurrency = entity.Discount * (Convert.ToDecimal(header.CurrencyRate) == 0 ? 1 : Convert.ToDecimal(header.CurrencyRate));
                }
                entity.CostPrice = (decimal)row["CostPrice"];
                entity.IsClosed = false;

                total += ((entity.Price ?? 0) - (entity.Discount ?? 0)) * (entity.Quantity ?? 0);
            }
            grdItemTransactionItem.DataSource = ItemTransactionItems;
            grdItemTransactionItem.DataBind();

            if ((header.DiscountAmount ?? 0) > 0)
            {
                decimal dsc = total / ((header.ChargesAmount ?? 0) + (header.DiscountAmount ?? 0)) * (header.DiscountAmount ?? 0);
                txtDiscountAmount.Value = Convert.ToDouble(dsc);
            }
            CalculateDetailTransaction();

            cboSRItemType.Enabled = !(ItemTransactionItems.Count > 0);
            cboToServiceUnitID.Enabled = !(ItemTransactionItems.Count > 0) && AppSession.Parameter.IsPoAndPorInTheSameUnit;
            cboToLocationID.Enabled = !(ItemTransactionItems.Count > 0);

            //Remove session
            Session.Remove("POR_W3TOT:ItemSelected" + Request.UserHostName);

            // pph
            cboSRPph.SelectedValue = header.SRPph;
            txtPphPercentage.Value = System.Convert.ToDouble(header.PphPercentage ?? 0);
            CalculatePph();
        }

        protected void btnResetItem_Click(object sender, EventArgs e)
        {
            //Reset Item
            if (txtReferenceNo.Text != string.Empty)
            {
                txtReferenceNo.Text = string.Empty;
                if (ItemTransactionItems.Count > 0)
                    ItemTransactionItems.MarkAllAsDeleted();
                cboSRProductAccountID.SelectedValue = string.Empty;
                cboSRProductAccountID.Enabled = false;
                cboCurrencyType.SelectedValue = string.Empty;
                txtCurrencyRate.Value = 1D;
                grdItemTransactionItem.DataSource = ItemTransactionItems;
                grdItemTransactionItem.DataBind();
            }
        }

        private void CalculateDetailTransaction()
        {
            if (ItemTransactionItems.Count > 0)
            {
                decimal? totaldiscitem = ItemTransactionItems.Where(item => !Convert.ToBoolean(item.IsBonusItem)).Aggregate<ItemTransactionItem, decimal?>(0, (current, item) => current + (item.Discount * item.Quantity));
                decimal? totaltransaction = ItemTransactionItems.Where(item => !Convert.ToBoolean(item.IsBonusItem)).Aggregate<ItemTransactionItem, decimal?>(0, (current, item) => current + (item.Price * item.Quantity));
                decimal? totaltax = ItemTransactionItems.Where(item => !Convert.ToBoolean(item.IsBonusItem) && Convert.ToBoolean(item.IsTaxable)).Aggregate<ItemTransactionItem, decimal?>(0, (current, item) => current + ((item.Price - item.Discount) * item.Quantity));

                var amount = (totaltransaction ?? 0) - (totaldiscitem ?? 0);
                amount = Math.Round(amount, 2, MidpointRounding.ToEven);

                var tax = Math.Round(totaltax ?? 0, 2, MidpointRounding.ToEven);

                txtTransactionAmount.Value = Convert.ToDouble(amount);
                txtReceiveAmount.Value = Convert.ToDouble(amount) - txtDiscountAmount.Value;
                txtAmountTaxed.Value = (Convert.ToDouble(tax) - txtDiscountAmount.Value) < 0 ? 0 : Convert.ToDouble(tax) - txtDiscountAmount.Value;

                if (AppSession.Parameter.IsPORoundingDownZeroDigit)
                {
                    txtTransactionAmount.Value = System.Convert.ToInt64(txtTransactionAmount.Value);
                    txtReceiveAmount.Value = System.Convert.ToInt64(txtReceiveAmount.Value);
                    txtAmountTaxed.Value = System.Convert.ToInt64(txtAmountTaxed.Value);
                }

                CalculateTax();
                CalculateTotal();
                CalculatePph();
            }
        }

        private void CalculateTax()
        {
            if (txtTaxPercentage.Value == 0)
                txtTaxAmount.Value = 0.00;
            else
            {
                //decimal? amount = ItemTransactionItems.Where(item =>
                //    !Convert.ToBoolean(item.IsBonusItem))
                //    .Aggregate<ItemTransactionItem, decimal?>(0, (current, item) =>
                //        current + (((item.Price * item.Quantity) - (item.Discount * item.Quantity)) * (Convert.ToDecimal(txtTaxPercentage.Value) / 100)));

                //amount = Math.Round(amount.Value, 2, MidpointRounding.ToEven);
                //txtTaxAmount.Value = txtAmountTaxed.Value * (txtTaxPercentage.Value / 100);
                txtTaxAmount.Value = Math.Round(txtAmountTaxed.Value.Value * (txtTaxPercentage.Value.Value / 100), 2);

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
            txtTotal.Value = txtReceiveAmount.Value + txtTaxAmount.Value + txtShippingCharges.Value + txtStampAmount.Value - txtAdvanceAmount.Value;
        }

        protected void txtDiscountAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateDetailTransaction();
        }

        #endregion

        #region Record Detail Method Function

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemTransactionItem.Columns[0].Visible = isVisible;
            grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 1].Visible = isVisible;
            if (grdItemTransactionItem.Columns.FindByUniqueName("ExpiredDate").Visible == false)
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = !isVisible && !chkIsApproved.Checked; // ed ico

            grdItemTransactionItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                ItemTransactionItems = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdItemTransactionItem.Rebind();
        }

        private ItemTransactionItemCollection ItemTransactionItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["POR_W3TOT:collItemTransactionItem" + Request.UserHostName];
                    if (obj != null)
                        return ((ItemTransactionItemCollection)(obj));
                }

                var coll = new ItemTransactionItemCollection();
                var query = new ItemTransactionItemQuery("a");

                var iq = new ItemQuery("b");

                query.LeftJoin(iq).On(query.ItemID == iq.ItemID);

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy
                    (
                        query.ItemID.Ascending, query.IsBonusItem.Ascending, query.Price.Ascending
                    );

                query.Select(query);

                if (cboSRItemType.SelectedValue == ItemType.Medical)
                {
                    var ipq = new ItemProductMedicQuery("c");
                    query.LeftJoin(ipq).On(query.ItemID == ipq.ItemID);
                    query.Select(@"<ISNULL(c.IsControlExpired, 0) AS 'refToItemProduct_IsControlExpired'>");
                }
                else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                {
                    var ipq = new ItemProductNonMedicQuery("c");
                    query.LeftJoin(ipq).On(query.ItemID == ipq.ItemID);
                    query.Select(@"<ISNULL(c.IsControlExpired, 0) AS 'refToItemProduct_IsControlExpired'>");
                }
                else
                {
                    var ipq = new ItemKitchenQuery("c");
                    query.LeftJoin(ipq).On(query.ItemID == ipq.ItemID);
                    query.Select(@"<ISNULL(c.IsControlExpired, 0) AS 'refToItemProduct_IsControlExpired'>");
                }

                var fq = new FabricQuery("f");
                query.LeftJoin(fq).On(fq.FabricID == query.FabricID);

                query.Select(iq.Barcode.As("refToItem_Barcode"),
                    @"<CASE WHEN a.Quantity * a.ConversionFactor > ISNULL((SELECT SUM(itie.Quantity * itie.ConversionFactor)
                        FROM ItemTransactionItemEd AS itie 
                        WHERE itie.TransactionNo = a.TransactionNo AND itie.SequenceNo = a.SequenceNo), 0) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS 'refToItemProduct_IsNotCompleteED'>", 
                    fq.FabricName.As("refToFabric_FabricName"));
                coll.Load(query);

                Session["POR_W3TOT:collItemTransactionItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["POR_W3TOT:collItemTransactionItem" + Request.UserHostName] = value; }
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
            if (!chkIsConsignmentAlreadyReceived.Checked)
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                if (editedItem == null) return;

                String sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemTransactionItemMetadata.ColumnNames.SequenceNo]);
                ItemTransactionItem entity = FindItemTransactionItem(sequenceNo);
                if (entity != null)
                    SetEntityValue(entity, e);

                CalculateDetailTransaction();
            }
        }

        private ItemTransactionItem FindItemTransactionItem(String sequenceNo)
        {
            return ItemTransactionItems.Where(x => x.SequenceNo == sequenceNo &&
                (x.TransactionNo ?? string.Empty) == (x.es.IsAdded ? string.Empty : txtTransactionNo.Text)).First();

            //ItemTransactionItemCollection coll = ItemTransactionItems;
            //ItemTransactionItem retEntity = null;
            //foreach (ItemTransactionItem rec in coll)
            //{
            //    if (rec.SequenceNo.Equals(sequenceNo))
            //    {
            //        retEntity = rec;
            //        break;
            //    }
            //}
            //return retEntity;
        }

        protected void grdItemTransactionItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            if (!chkIsConsignmentAlreadyReceived.Checked)
            {
                GridDataItem item = e.Item as GridDataItem;
                if (item == null) return;

                String sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemTransactionItemMetadata.ColumnNames.SequenceNo]);
                ItemTransactionItem entity = FindItemTransactionItem(sequenceNo);
                if (entity != null)
                {
                    entity.MarkAsDeleted();
                }
                CalculateDetailTransaction();
            }
        }

        protected void grdItemTransactionItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            if (!chkIsConsignmentAlreadyReceived.Checked)
            {
                ItemTransactionItem entity = ItemTransactionItems.AddNew();
                SetEntityValue(entity, e);
                CalculateDetailTransaction();
            }
        }

        private void SetEntityValue(ItemTransactionItem entity, GridCommandEventArgs e)
        {
            var userControl = (PurchaseOrderReceiveItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
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
                    decimal disc1 = Convert.ToDecimal(entity.Price * entity.Discount1Percentage / 100);
                    decimal disc2 = Convert.ToDecimal((entity.Price - disc1) * entity.Discount2Percentage / 100);
                    entity.Discount = disc1 + disc2;
                }
                else
                {
                    entity.Discount1Percentage = 0;
                    entity.Discount2Percentage = 0;
                    entity.Discount = userControl.Discount;
                }
                entity.DiscountInCurrency = entity.Discount * Convert.ToDecimal(txtCurrencyRate.Value);
                entity.BatchNumber = userControl.BatchNumber;
                if (userControl.ExpiredDate == null)
                    entity.str.ExpiredDate = string.Empty;
                else
                    entity.ExpiredDate = userControl.ExpiredDate;
                entity.IsBonusItem = userControl.IsBonusItem;
                if (cboSRItemType.SelectedValue == ItemType.Medical)
                {
                    var med = new ItemProductMedic();
                    med.LoadByPrimaryKey(entity.ItemID);
                    entity.IsControlExpired = med.IsControlExpired ?? false;
                }
                else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                {
                    var nonMed = new ItemProductNonMedic();
                    nonMed.LoadByPrimaryKey(entity.ItemID);
                    entity.IsControlExpired = nonMed.IsControlExpired ?? false;
                }
                else
                {
                    var kitchen = new ItemKitchen();
                    kitchen.LoadByPrimaryKey(entity.ItemID);
                    entity.IsControlExpired = kitchen.IsControlExpired ?? false;
                }
                entity.IsTaxable = userControl.IsTaxable;
                entity.IsNotCompleteED = true;

                if (IsGrantsReceiving || IsDirectPurchase)
                {
                    entity.ReferenceNo = string.Empty;
                    entity.ReferenceSequenceNo = string.Empty;
                }

                entity.FabricID = userControl.FabricID;
                entity.FabricName = userControl.FabricName;

                if (!string.IsNullOrEmpty(userControl.Barcode))
                {
                    var item = new Item();
                    item.LoadByPrimaryKey(entity.ItemID);
                    item.Barcode = userControl.Barcode;
                    item.Save();
                }
            }
        }

        #endregion

        protected void rblTypesOfTaxes_OnTextChanged(object sender, EventArgs e)
        {
            PopulateCalculateTax();
            //var supp = new Supplier();
            //supp.LoadByPrimaryKey(cboBusinessPartnerID.SelectedValue);

            //if (rblTypesOfTaxes.SelectedIndex == 0)
            //    txtTaxPercentage.Value = Convert.ToDouble(supp.TaxPercentage ?? 0);
            //else
            //    txtTaxPercentage.Value = 0;

            //CalculateTax();
        }

        protected void txtShippingCharges_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
            CalculatePph();
        }

        protected void txtStampAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
            CalculatePph();
        }

        protected void AdvanceAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
            CalculatePph();
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

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);
            if (sourceControl is RadGrid)
            {
                if (eventArgument.Contains("updbarcode"))
                {
                    var pars = eventArgument.Split('|');
                    var itemID = pars[1];
                    var barcode = pars[2];
                    foreach (ItemTransactionItem item in ItemTransactionItems)
                    {
                        if (item.ItemID == itemID)
                            item.Barcode = barcode;
                    }
                    grdItemTransactionItem.Rebind();
                }
                else if (eventArgument.Contains("rebindEd"))
                {
                    var pars = eventArgument.Split('|');
                    var seqNo = pars[1];
                    foreach (ItemTransactionItem item in ItemTransactionItems)
                    {
                        if (item.SequenceNo == seqNo)
                        {
                            var itie = new ItemTransactionItemEdCollection();
                            itie.Query.Where(itie.Query.TransactionNo == txtTransactionNo.Text, itie.Query.SequenceNo == seqNo);
                            itie.LoadAll();

                            decimal qtyEd = 0;
                            foreach (var x in itie)
                            {
                                qtyEd += Convert.ToDecimal(x.Quantity * x.ConversionFactor);
                            }
                            item.IsNotCompleteED = Convert.ToDecimal(item.Quantity * item.ConversionFactor) > qtyEd;
                        }
                    }

                    grdItemTransactionItem.Rebind();
                }
            }
        }

        protected void cboSupplier_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new SupplierQuery("a");
            if (string.IsNullOrEmpty(e.Text))
            {
                query.Where(query.IsActive == true);
            }
            else
            {
                //query.Where(query.Or(query.SupplierID == e.Text));
                string searchTextContain = string.Format("%{0}%", e.Text);
                query.Where
                    (
                    query.Or(
                           query.SupplierID == e.Text,
                           query.SupplierName.Like(searchTextContain)
                           )
                       );
            }

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
