using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class PurchaseOrderReturnDetail : BasePageDetail
    {
        private string getPageID
        {
            get
            {
                return Request.QueryString["cons"];
            }
        }

        private AppAutoNumberLast _autoNumber;
        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;
            if (cboFromServiceUnitID.SelectedValue == string.Empty) return;
            ServiceUnit serv = new ServiceUnit();
            if (serv.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue))
            {
                _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, TransactionCode.PurchaseOrderReturn, serv.DepartmentID);
                txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
            }
        }

        private void PopulateFromSelectedPurchaseOrder()
        {
            object obj = Session["POReturn:ItemSelected" + Request.UserHostName + hdnPageId.Value];
            if (obj == null) return;

            //delete previouse item
            if (ItemTransactionItems.Count > 0)
                ItemTransactionItems.MarkAllAsDeleted();

            DataTable dtbSelectedItem = (DataTable)obj;
            if (dtbSelectedItem.Rows.Count > 0)
            {
                txtReferenceNo.Text = dtbSelectedItem.Rows[0]["TransactionNo"].ToString();
            }

            var hd = new ItemTransaction();
            hd.LoadByPrimaryKey(txtReferenceNo.Text);
            var supp = new SupplierQuery();
            supp.Where(supp.SupplierID == hd.BusinessPartnerID);
            cboBusinessPartnerID.DataSource = supp.LoadDataTable();
            cboBusinessPartnerID.DataBind();
            cboBusinessPartnerID.SelectedValue = hd.BusinessPartnerID;

            if (getPageID == "1")
            {
                ComboBox.PopulateWithSupplierForLocation(cboToLocationID, cboBusinessPartnerID.SelectedValue);
                cboToLocationID.SelectedValue = hd.FromLocationID;
            }

            string seqNo;
            string parCostType = AppParameter.GetParameterValue(AppParameter.ParameterItem.ItemProductCostPriceType);

            int i = 0;
            foreach (DataRow row in dtbSelectedItem.Rows)
            {
                if (Convert.ToDecimal(row["QtyInput"]) <= 0)
                    continue;

                i++;
                seqNo = string.Format("{0:000}", i);
                ItemTransactionItem entity = ItemTransactionItems.AddNew();

                entity.ItemID = row["ItemID"].ToString();
                entity.SequenceNo = seqNo;
                entity.ReferenceNo = row["TransactionNo"].ToString();
                entity.ReferenceSequenceNo = row["SequenceNo"].ToString();
                entity.Description = row["Description"].ToString();
                entity.Quantity = Convert.ToDecimal(row["QtyInput"]);
                entity.SRItemUnit = row["SRItemUnit"].ToString();
                entity.ConversionFactor = Convert.ToDecimal(row["ConversionFactor"]);
                entity.Price = Convert.ToDecimal(row["Price"]);
                entity.Discount1Percentage = Convert.ToDecimal(row["Discount1Percentage"]);
                entity.Discount2Percentage = Convert.ToDecimal(row["Discount2Percentage"]);
                entity.Discount = Convert.ToDecimal(row["Discount"]);
                entity.IsDiscountInPercent = Convert.ToBoolean(row["IsDiscountInPercent"]);
                entity.PriceInCurrency = Convert.ToDecimal(row["PriceInCurrency"]);
                entity.DiscountInCurrency = Convert.ToDecimal(row["DiscountInCurrency"]);
                entity.IsControlExpired = Convert.ToBoolean(row["IsControlExpired"]);

                if (row["ExpiredDate"] != null && !(row["ExpiredDate"] is DBNull) &&
                    Convert.ToDateTime(row["ExpiredDate"]).Year > 1900 && 
                    Convert.ToDateTime(row["ExpiredDate"]).Year != 2999)  //SmallDateTime, 1900-01-01 through 2079-06-06
                    entity.ExpiredDate = Convert.ToDateTime(row["ExpiredDate"]);
                else
                    entity.str.ExpiredDate = string.Empty;

                entity.BatchNumber = row["BatchNumber"].ToString();

                var ent = new ItemTransaction();
                ent.LoadByPrimaryKey(row["TransactionNo"].ToString());

                if (cboSRItemType.SelectedValue == ItemType.Medical)
                {
                    var med = new ItemProductMedic();
                    if (med.LoadByPrimaryKey(entity.ItemID))
                    {
                        if (parCostType == "AVG")
                            entity.CostPrice = med.CostPrice;
                        else
                        {
                            entity.CostPrice = (((decimal)row["Price"] - (decimal)row["Discount"]) * (1 + (ent.TaxPercentage / 100)));
                        }
                    }
                    else
                        entity.CostPrice = entity.Price;
                }
                else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                {
                    var nonMed = new ItemProductNonMedic();
                    if (nonMed.LoadByPrimaryKey(entity.ItemID))
                    {
                        if (parCostType == "AVG")
                            entity.CostPrice = nonMed.CostPrice;
                        else
                        {
                            entity.CostPrice = (((decimal)row["Price"] - (decimal)row["Discount"]) * (1 + (ent.TaxPercentage / 100)));
                        }
                    }
                    else
                        entity.CostPrice = entity.Price;
                }
                else if (cboSRItemType.SelectedValue == ItemType.Kitchen)
                {
                    var kitchen = new ItemKitchen();
                    if (kitchen.LoadByPrimaryKey(entity.ItemID))
                    {
                        if (parCostType == "AVG")
                            entity.CostPrice = kitchen.CostPrice;
                        else
                        {
                            entity.CostPrice = (((decimal)row["Price"] - (decimal)row["Discount"]) * (1 + (ent.TaxPercentage / 100)));
                        }
                    }
                    else
                        entity.CostPrice = entity.Price;
                }

            }
            grdItemTransactionItem.DataSource = ItemTransactionItems;
            grdItemTransactionItem.DataBind();

            CalculateDetailTransaction();

            cboSRItemType.Enabled = !(ItemTransactionItems.Count > 0);

            //Remove session
            Session.Remove("POReturn:ItemSelected" + Request.UserHostName);
        }

        protected void btnResetItem_Click(object sender, EventArgs e)
        {
            if (txtReferenceNo.Text != string.Empty)
            {
                txtReferenceNo.Text = string.Empty;
                if (ItemTransactionItems.Count > 0)
                    ItemTransactionItems.MarkAllAsDeleted();
                grdItemTransactionItem.DataSource = ItemTransactionItems;
                grdItemTransactionItem.DataBind();
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PurchaseOrderReturnSearch.aspx?cons=" + getPageID;
            UrlPageList = "PurchaseOrderReturnList.aspx?cons=" + getPageID;

            ProgramID = getPageID == "0"
                            ? AppConstant.Program.PurchaseOrderReturn
                            : AppConstant.Program.PurchaseOrderReturnConsignment;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                hdnPageId.Value = PageID;

                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
                ComboBox.SelectedValue(cboSRItemType, ItemType.Medical);
                StandardReference.InitializeIncludeSpace(cboSRPurchaseReturnType, AppEnum.StandardReference.PurchaseReturnType);

                if (AppSession.Parameter.IsPurcReturnWithPrice)
                {
                    grdItemTransactionItem.Columns[6].Visible = true;
                    grdItemTransactionItem.Columns[7].Visible = true;
                    grdItemTransactionItem.Columns[8].Visible = true;
                    grdItemTransactionItem.Columns[9].Visible = true;
                    grdItemTransactionItem.Columns[10].Visible = true;
                    tblPrice.Visible = true;
                }

                trLocationConsignment.Visible = getPageID == "1";
            }

            //Add Event for Request Order Selection
            AjaxManager.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(AjaxManager_AjaxRequest);
        }

        private void AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            PopulateFromSelectedPurchaseOrder();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItemTransactionItem, grdItemTransactionItem);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboSRItemType);

            if (AppSession.Parameter.IsPurcReturnWithPrice)
            {
                ajax.AddAjaxSetting(grdItemTransactionItem, txtTransactionAmount);
                ajax.AddAjaxSetting(grdItemTransactionItem, txtTaxPercentage);
                ajax.AddAjaxSetting(grdItemTransactionItem, txtTotal);
                ajax.AddAjaxSetting(grdItemTransactionItem, txtTaxAmount);

                ajax.AddAjaxSetting(AjaxManager, txtTransactionAmount);
                ajax.AddAjaxSetting(AjaxManager, txtTaxPercentage);
                ajax.AddAjaxSetting(AjaxManager, txtTaxAmount);
                ajax.AddAjaxSetting(AjaxManager, txtTotal);
            }

            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromServiceUnitID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, txtTransactionNo);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromLocationID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboSRItemType);

            //PurchaseReturn Request Selection
            ajax.AddAjaxSetting(AjaxManager, txtReferenceNo);
            ajax.AddAjaxSetting(AjaxManager, cboBusinessPartnerID);
            ajax.AddAjaxSetting(AjaxManager, grdItemTransactionItem);
            ajax.AddAjaxSetting(AjaxManager, cboSRItemType);
            if (getPageID == "1")
                ajax.AddAjaxSetting(AjaxManager, cboToLocationID);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
            //string serviceUnitID = cboFromServiceUnitID.SelectedValue;
            //ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.PurchaseOrderReturn, true);
            //cboFromServiceUnitID.SelectedValue = serviceUnitID;

            cboSRItemType.Enabled = ItemTransactionItems.Count == 0;
            cboFromServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboFromLocationID.Enabled = cboSRItemType.Enabled;
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
            if (string.IsNullOrEmpty(cboSRPurchaseReturnType.SelectedValue))
            {
                args.MessageText = "Return Type required.";
                args.IsCancel = true;
                return;
            }

            var refs = new ItemTransaction();
            refs.LoadByPrimaryKey(txtReferenceNo.Text);
            if (refs.IsNonMasterOrder ?? false)
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
                if (refs.IsInventoryItem ?? false)
                {
                    string result = ItemTransaction.IsItemMinusProcess(txtTransactionNo.Text, ItemTransactionItems);
                    if (result != string.Empty)
                    {
                        args.MessageText = result;
                        args.IsCancel = true;
                        return;
                    }
                }
                
                var retval = (new ItemTransaction()).Approve(txtTransactionNo.Text, ItemTransactionItems, AppSession.UserLogin.UserID);
                if (retval != string.Empty)
                {
                    args.MessageText = retval;
                    args.IsCancel = true;
                    return;
                }
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            (new ItemTransaction()).Void(txtTransactionNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            (new ItemTransaction()).UnVoid(txtTransactionNo.Text, AppSession.UserLogin.UserID);
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

            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();// DateTime.Now;
            cboBusinessPartnerID.Items.Clear();
            cboBusinessPartnerID.Text = string.Empty;
            ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.PurchaseOrderReturn, true);
            cboFromServiceUnitID.Text = string.Empty;
            cboFromLocationID.Items.Clear();
            cboFromLocationID.Text = string.Empty;

            PopulateNewTransactionNo();
            txtTransactionAmount.Value = 0;
            txtTaxPercentage.Value = 0;
            txtTaxAmount.Value = 0;
            txtTotal.Value = 0;
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
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            //Check Supplier entry
            if (!IsSupplierExist(args)) return;

            if (getPageID == "1" && string.IsNullOrEmpty(cboToLocationID.SelectedValue))
            {
                args.MessageText = "Location For Consignment required.";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboSRPurchaseReturnType.SelectedValue))
            {
                args.MessageText = "Return Type required.";
                args.IsCancel = true;
                return;
            }

            PopulateNewTransactionNo();
            // save autonumber immediately to decrease time gap between create and save
            _autoNumber.Save();

            ItemTransaction entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
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
            SaveEntity(entity);
        }

        private bool IsSupplierExist(ValidateArgs args)
        {
            SupplierQuery query = new SupplierQuery();
            query.es.Top = 1;
            query.Where(query.SupplierName == cboBusinessPartnerID.Text);
            Supplier item = new Supplier();
            if (!item.Load(query))
            {
                args.IsCancel = true;
                args.MessageText = "Selected supplier not valid, please select exist supplier";
                return false;
            }
            return true;
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            //Check Supplier entry
            if (!IsSupplierExist(args)) return;

            if (getPageID == "1" && string.IsNullOrEmpty(cboToLocationID.SelectedValue))
            {
                args.MessageText = "Location For Consignment required.";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboSRPurchaseReturnType.SelectedValue))
            {
                args.MessageText = "Return Type required.";
                args.IsCancel = true;
                return;
            }

            var entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                if (ItemTransactionItems.Where(x => x.TransactionNo == txtTransactionNo.Text).Count() == 0)
                {
                    args.MessageText = AppConstant.Message.RecordDetailEmpty;
                    args.IsCancel = true;
                    return;
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
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
            btnGetPickList.Enabled = newVal != AppEnum.DataMode.Read;
            btnResetItem.Enabled = newVal != AppEnum.DataMode.Read;
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
            var itemTransaction = (ItemTransaction)entity;
            txtTransactionNo.Text = itemTransaction.TransactionNo;
            txtTransactionDate.SelectedDate = itemTransaction.TransactionDate;
            txtReferenceNo.Text = itemTransaction.ReferenceNo;
            if (!string.IsNullOrEmpty(itemTransaction.FromServiceUnitID))
            {
                ComboBox.PopulateWithOneServiceUnit(cboFromServiceUnitID, itemTransaction.FromServiceUnitID);
                ComboBox.PopulateWithServiceUnitForLocation(cboFromLocationID, itemTransaction.FromServiceUnitID);
                if (!string.IsNullOrEmpty(itemTransaction.FromLocationID))
                    cboFromLocationID.SelectedValue = itemTransaction.FromLocationID;
                else cboFromLocationID.SelectedIndex = 1;
            }
            else
            {
                cboFromServiceUnitID.Items.Clear();
                cboFromServiceUnitID.Text = string.Empty;
                cboFromLocationID.Items.Clear();
                cboFromLocationID.Text = string.Empty;
            }

            cboSRItemType.SelectedValue = itemTransaction.SRItemType;
            chkIsVoid.Checked = itemTransaction.IsVoid ?? false;
            chkIsApproved.Checked = itemTransaction.IsApproved ?? false;
            cboSRPurchaseReturnType.SelectedValue = itemTransaction.SRPurchaseReturnType;
            txtNotes.Text = itemTransaction.Notes;
            if (!string.IsNullOrEmpty(itemTransaction.BusinessPartnerID))
            {
                var supp = new SupplierQuery();
                supp.Where(supp.SupplierID == itemTransaction.BusinessPartnerID);
                cboBusinessPartnerID.DataSource = supp.LoadDataTable();
                cboBusinessPartnerID.DataBind();
                cboBusinessPartnerID.SelectedValue = itemTransaction.BusinessPartnerID;
            }
            else
            {
                cboBusinessPartnerID.Items.Clear();
                cboBusinessPartnerID.Text = string.Empty;
            }

            if (getPageID == "1")
            {
                ComboBox.PopulateWithSupplierForLocation(cboToLocationID, cboBusinessPartnerID.SelectedValue);
                cboToLocationID.SelectedValue = itemTransaction.ToLocationID;
            }
            else
            {
                cboToLocationID.Items.Clear();
                cboToLocationID.Text = string.Empty;
            }

            //Display Data Detail
            PopulateGridDetail();
            CalculateDetailTransaction();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ItemTransaction entity)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionCode = BusinessObject.Reference.TransactionCode.PurchaseOrderReturn;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.ReferenceNo = txtReferenceNo.Text;
            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            entity.FromLocationID = cboFromLocationID.SelectedValue;
            entity.BusinessPartnerID = cboBusinessPartnerID.SelectedValue;

            entity.SRItemType = cboSRItemType.SelectedValue;
            entity.SRPurchaseReturnType = cboSRPurchaseReturnType.SelectedValue;
            entity.Notes = txtNotes.Text;

            var refs = new ItemTransaction();
            refs.LoadByPrimaryKey(txtReferenceNo.Text);

            var refsItemColl = new ItemTransactionItemCollection();
            refsItemColl.Query.Where(refsItemColl.Query.TransactionNo == refs.TransactionNo);
            refsItemColl.LoadAll();

            entity.IsNonMasterOrder = refs.IsNonMasterOrder;
            entity.IsInventoryItem = refs.IsInventoryItem;
            entity.SRPurchaseOrderType = refs.SRPurchaseOrderType;
            entity.IsTaxable = refs.IsTaxable;
            entity.TaxPercentage = refs.TaxPercentage;
            entity.CurrencyID = refs.CurrencyID;
            entity.CurrencyRate = refs.CurrencyRate;
            entity.InvoiceNo = refs.InvoiceNo;
            entity.ProductAccountID = refs.ProductAccountID;
            entity.IsConsignment = refs.IsConsignment;
            entity.ToLocationID = cboToLocationID.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
            }

            decimal? totalcharge = 0;
            decimal? totaltax = 0;

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
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                }
                var trans = ((item.Price * item.Quantity) - (item.Discount * item.Quantity));
                totalcharge += trans;
                var itemRef = refsItemColl.Where(x => x.SequenceNo == item.ReferenceSequenceNo
                    && x.TransactionNo == item.ReferenceNo).FirstOrDefault();
                if (itemRef != null)
                {
                    item.IsTaxable = itemRef.IsTaxable;

                    totaltax += (refs.TaxPercentage == 0 || refs.IsTaxable == 2) ? 0 :
                        ((itemRef.IsTaxable ?? false) ? ((trans * refs.TaxPercentage) / Convert.ToDecimal(100)) : 0);
                }
            }

            //totaltax = refs.TaxPercentage == 0 ? 0 : ((totalcharge.Value * refs.TaxPercentage) / Convert.ToDecimal(100));

            entity.SRPph = refs.SRPph;
            entity.PphPercentage = refs.PphPercentage;

            entity.ChargesAmount = (-1) * totalcharge;
            //entity.TaxAmount = (-1) * totaltax;
            //var discDevider = (refs.ChargesAmount + refs.DiscountAmount) * refs.DiscountAmount;
            //entity.DiscountAmount = entity.ChargesAmount == 0 ? 0 : (discDevider / entity.ChargesAmount);
            decimal? discDevider = 0;
            if (refs.ChargesAmount + refs.DiscountAmount != 0)
                discDevider = refs.DiscountAmount / (refs.ChargesAmount + refs.DiscountAmount);
            entity.DiscountAmount = entity.ChargesAmount == 0 ? 0 : (discDevider * entity.ChargesAmount);
            entity.ChargesAmount = entity.ChargesAmount - entity.DiscountAmount;

            // return pph
            decimal? totaltaxed = ItemTransactionItems.Where(item => !Convert.ToBoolean(item.IsBonusItem) && Convert.ToBoolean(item.IsTaxable)).Aggregate<ItemTransactionItem, decimal?>(0, (current, item) => current + ((item.Price - item.Discount) * item.Quantity));
            var tax = Math.Round(totaltaxed ?? 0, 2, MidpointRounding.ToEven);
            entity.AmountTaxed = -tax - entity.DiscountAmount;
            entity.TaxAmount = Math.Round((decimal)(entity.AmountTaxed * entity.TaxPercentage / 100), 2);
            var pphDevider = (refs.AmountTaxed + entity.DiscountAmount) * refs.PphAmount;
            entity.PphAmount = entity.AmountTaxed == 0 ? 0 : (pphDevider / entity.AmountTaxed);
        }

        private void SaveEntity(ItemTransaction entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ItemTransactionItems.Save();

                if ((entity.IsInventoryItem ?? false) && AppSession.Parameter.IsEnabledStockWithEdControl)
                {
                    var itieColl = new ItemTransactionItemEdCollection();
                    itieColl.Query.Where(itieColl.Query.TransactionNo == entity.TransactionNo);
                    itieColl.LoadAll();
                    itieColl.MarkAllAsDeleted();
                    itieColl.Save();

                    foreach (var item in ItemTransactionItems)
                    {
                        var ed = itieColl.AddNew();
                        ed.TransactionNo = entity.TransactionNo;
                        ed.SequenceNo = item.SequenceNo;
                        if (item.IsControlExpired)
                        {
                            ed.ExpiredDate = item.ExpiredDate;
                            ed.BatchNumber = item.BatchNumber;
                        }
                        else
                        {
                            DateTime defDate = DateTime.Parse("1/1/2999");
                            ed.ExpiredDate = defDate;
                            ed.BatchNumber = "-N/A-";
                        }
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
                    }

                    itieColl.Save();
                }

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
                que.Where(que.TransactionNo > txtTransactionNo.Text &&
                          que.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseOrderReturn);
                if (getPageID == "0")
                    que.Where(que.IsConsignment == false);
                else que.Where(que.IsConsignment == true);
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < txtTransactionNo.Text &&
                          que.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseOrderReturn);
                if (getPageID == "0")
                    que.Where(que.IsConsignment == false);
                else que.Where(que.IsConsignment == true);
                que.OrderBy(que.TransactionNo.Descending);
            }
            var entity = new ItemTransaction();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Record Detail Method Function

        private ItemTransactionItemCollection ItemTransactionItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["POReturn:collItemTransactionItem" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                        return ((ItemTransactionItemCollection)(obj));
                }

                var coll = new ItemTransactionItemCollection();
                var query = new ItemTransactionItemQuery("a");

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

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.ItemID.Ascending);

                coll.Load(query);
                Session["POReturn:collItemTransactionItem" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["POReturn:collItemTransactionItem" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemTransactionItem.Columns[0].Visible = isVisible;
            grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 1].Visible = isVisible;

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
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String sequenceNo =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ItemTransactionItemMetadata.ColumnNames.SequenceNo]);
            ItemTransactionItem entity = FindItemTransactionItem(sequenceNo);
            if (entity != null)
                SetEntityValue(entity, e);

            CalculateDetailTransaction();
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
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String sequenceNo =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemTransactionItemMetadata.ColumnNames.SequenceNo
                        ]);
            ItemTransactionItem entity = FindItemTransactionItem(sequenceNo);
            if (entity != null)
                entity.MarkAsDeleted();

            cboSRItemType.Enabled = !(ItemTransactionItems.Count > 0);

            CalculateDetailTransaction();
        }

        protected void grdItemTransactionItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemTransactionItem entity = ItemTransactionItems.AddNew();
            SetEntityValue(entity, e);

            //grid not close first
            e.Canceled = true;
            grdItemTransactionItem.Rebind();

            CalculateDetailTransaction();
        }

        private void SetEntityValue(ItemTransactionItem entity, GridCommandEventArgs e)
        {
            var userControl = (PurchaseOrderReturnItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.Quantity = userControl.Quantity;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ConversionFactor = userControl.ConversionFactor;
                entity.Price = userControl.Price;
                entity.PriceInCurrency = userControl.Price;
                entity.IsDiscountInPercent = userControl.IsDiscountInPercent;
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
                entity.DiscountInCurrency = entity.Discount;
            }
        }

        #endregion

        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateNewTransactionNo();
            ComboBox.PopulateWithServiceUnitForLocation(cboFromLocationID, e.Value);
            cboFromLocationID.SelectedIndex = 1;
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, e.Value, TransactionCode.PurchaseOrderReturn);
            cboSRItemType.SelectedValue = string.Empty;
            cboSRItemType.Text = string.Empty;
        }

        protected void cboBusinessPartnerID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SupplierQuery("a");
            query.Where(query.IsActive == true,
                query.Or(query.SupplierID == e.Text, query.SupplierName.Like(searchTextContain))
                );
            query.Select(query.SupplierID, query.SupplierName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboBusinessPartnerID.DataSource = dtb;
            cboBusinessPartnerID.DataBind();
        }

        protected void cboBusinessPartnerID_OnItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.SupplierItemDataBound(e);
        }

        private void CalculateDetailTransaction()
        {
            if (ItemTransactionItems.Count > 0)
            {
                decimal? totaldiscitem = ItemTransactionItems.Where(item => !Convert.ToBoolean(item.IsBonusItem)).Aggregate<ItemTransactionItem, decimal?>(0, (current, item) => current + (item.Discount * item.Quantity));
                decimal? totaltransaction = ItemTransactionItems.Where(item => !Convert.ToBoolean(item.IsBonusItem)).Aggregate<ItemTransactionItem, decimal?>(0, (current, item) => current + (item.Price * item.Quantity));

                txtTransactionAmount.Value = Convert.ToDouble(totaltransaction - totaldiscitem);

                CalculateTax();
            }
        }

        private void CalculateTax()
        {
            var it = new ItemTransaction();
            it.LoadByPrimaryKey(txtReferenceNo.Text);
            if (it.IsTaxable == 1)
            {
                txtTaxPercentage.Value = Convert.ToDouble(it.TaxPercentage);
                txtTaxAmount.Value = ((txtTransactionAmount.Value * txtTaxPercentage.Value) / Convert.ToDouble(100));
            }
            else
            {
                txtTaxPercentage.Value = 0.00;
                txtTaxAmount.Value = 0.00;
            }

            CalculateTotal();
        }

        private void CalculateTotal()
        {
            txtTotal.Value = txtTransactionAmount.Value + txtTaxAmount.Value;
        }

    }
}