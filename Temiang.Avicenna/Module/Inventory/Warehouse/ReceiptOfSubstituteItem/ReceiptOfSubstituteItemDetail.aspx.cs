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
    public partial class ReceiptOfSubstituteItemDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;
        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;
            if (cboToServiceUnitID.SelectedValue == string.Empty) return;
            var serv = new ServiceUnit();
            if (serv.LoadByPrimaryKey(cboToServiceUnitID.SelectedValue))
            {
                _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, TransactionCode.ReceiptOfSubstitute, serv.DepartmentID);
                txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
            }
        }

        private void PopulateFromSelectedPurchaseOrder()
        {
            object obj = Session["POSubstitute:ItemSelected" + Request.UserHostName];
            if (obj == null) return;

            //delete previouse item
            if (ItemTransactionItems.Count > 0)
                ItemTransactionItems.MarkAllAsDeleted();

            DataTable dtbSelectedItem = (DataTable)obj;
            if (dtbSelectedItem.Rows.Count > 0)
            {
                txtReferenceNo.Text = dtbSelectedItem.Rows[0]["TransactionNo"].ToString();
            }

            string seqNo;
            string parCostType = AppParameter.GetParameterValue(AppParameter.ParameterItem.ItemProductCostPriceType);

            int i = 0;
            foreach (DataRow row in dtbSelectedItem.Rows)
            {
                if (Convert.ToDecimal(row["QtyInput"]) < 1)
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

                if (row["ExpiredDate"] != null && Convert.ToDateTime(row["ExpiredDate"]).Year > 1900)
                    entity.ExpiredDate = Convert.ToDateTime(row["ExpiredDate"]);
                else
                    entity.str.ExpiredDate = string.Empty;

                entity.BatchNumber = row["BatchNumber"].ToString();

                var ent = new ItemTransaction();
                ent.LoadByPrimaryKey(row["TransactionNo"].ToString());

                if (cboSRItemType.SelectedValue == BusinessObject.Reference.ItemType.Medical)
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
                else if (cboSRItemType.SelectedValue == BusinessObject.Reference.ItemType.NonMedical)
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
                else if (cboSRItemType.SelectedValue == BusinessObject.Reference.ItemType.Kitchen)
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

            cboSRItemType.Enabled = !(ItemTransactionItems.Count > 0);

            //Remove session
            Session.Remove("POSubstitute:ItemSelected" + Request.UserHostName);
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
            UrlPageSearch = "ReceiptOfSubstituteItemSearch.aspx";
            UrlPageList = "ReceiptOfSubstituteItemList.aspx";

            ProgramID = AppConstant.Program.ReceiptOfSubstituteItem;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
                ComboBox.SelectedValue(cboSRItemType, BusinessObject.Reference.ItemType.Medical);

                if (AppSession.Parameter.IsTxUsingEdDetail)
                {
                    grdItemTransactionItem.Columns[6].Visible = false; // batch no txt
                    grdItemTransactionItem.Columns[7].Visible = false; // ed txt
                }
                else
                    grdItemTransactionItem.Columns[8].Visible = false; // ed txt
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

            ajax.AddAjaxSetting(cboToServiceUnitID, cboToServiceUnitID);
            ajax.AddAjaxSetting(cboToServiceUnitID, txtTransactionNo);
            ajax.AddAjaxSetting(cboToServiceUnitID, cboToLocationID);

            //PurchaseReturn Request Selection
            ajax.AddAjaxSetting(AjaxManager, txtReferenceNo);
            ajax.AddAjaxSetting(AjaxManager, grdItemTransactionItem);
            ajax.AddAjaxSetting(AjaxManager, cboSRItemType);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
            cboSRItemType.Enabled = ItemTransactionItems.Count == 0;
            cboToServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboToLocationID.Enabled = cboSRItemType.Enabled;

            if (grdItemTransactionItem.Columns[6].Visible == false)
                grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 2].Visible = false; // ed ico

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

            if (AppSession.Parameter.IsTxUsingEdDetail)
            {
                var msg = string.Empty;
                foreach (var item in c)
                {
                    if (item.IsControlExpired)
                    {
                        decimal qty = (item.Quantity ?? 0)*(item.ConversionFactor ?? 0);
                        var ed = new ItemTransactionItemEdCollection();
                        ed.Query.Where(ed.Query.TransactionNo == item.TransactionNo,
                                       ed.Query.SequenceNo == item.SequenceNo);
                        ed.LoadAll();
                        decimal qtyDt = ed.Sum(i => (i.Quantity ?? 0)*(i.ConversionFactor ?? 0));

                        if (qty != qtyDt)
                        {
                            if (msg == string.Empty)
                                msg = item.ItemID;
                            else
                                msg += ", " + item.ItemID;
                        }
                    }
                }
                if (msg != string.Empty)
                {
                    args.MessageText = "Data can't be approved. Quantity detail Expiry Date for item: " + msg +
                                       " does not match the total quantity received.";
                    args.IsCancel = true;
                    return;
                }
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
                var retval = (new ItemTransaction()).Approve(txtTransactionNo.Text, ItemTransactionItems, AppSession.UserLogin.UserID);
                if (retval != string.Empty)
                {
                    args.MessageText = retval;
                    args.IsCancel = true;
                    return;
                }
            }
            if (grdItemTransactionItem.Columns[6].Visible == false)
                grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 2].Visible = false; // ed ico

        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            (new ItemTransaction()).Void(txtTransactionNo.Text, AppSession.UserLogin.UserID);
            if (grdItemTransactionItem.Columns[6].Visible == false)
                grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 2].Visible = false; // ed ico

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
            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.ReceiptOfSubstitute, true);
            cboToServiceUnitID.Text = string.Empty;
            cboToLocationID.Items.Clear();
            cboToLocationID.Text = string.Empty;

            PopulateNewTransactionNo();
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
            if (!string.IsNullOrEmpty(itemTransaction.ToServiceUnitID))
            {
                ComboBox.PopulateWithOneServiceUnit(cboToServiceUnitID, itemTransaction.ToServiceUnitID);
                ComboBox.PopulateWithServiceUnitForLocation(cboToLocationID, itemTransaction.ToServiceUnitID);
                if (!string.IsNullOrEmpty(itemTransaction.ToLocationID))
                    cboToLocationID.SelectedValue = itemTransaction.ToLocationID;
                else cboToLocationID.SelectedIndex = 1;
            }
            else
            {
                cboToServiceUnitID.Items.Clear();
                cboToServiceUnitID.Text = string.Empty;
                cboToLocationID.Items.Clear();
                cboToLocationID.Text = string.Empty;
            }

            cboSRItemType.SelectedValue = itemTransaction.SRItemType;
            chkIsVoid.Checked = itemTransaction.IsVoid ?? false;
            chkIsApproved.Checked = itemTransaction.IsApproved ?? false;
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

            //Display Data Detail
            PopulateGridDetail();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ItemTransaction entity)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionCode = TransactionCode.ReceiptOfSubstitute;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.ReferenceNo = txtReferenceNo.Text;
            entity.ToServiceUnitID = cboToServiceUnitID.SelectedValue;
            entity.ToLocationID = cboToLocationID.SelectedValue;
            entity.BusinessPartnerID = cboBusinessPartnerID.SelectedValue;

            entity.SRItemType = cboSRItemType.SelectedValue;
            entity.Notes = txtNotes.Text;

            var refs = new ItemTransaction();
            refs.LoadByPrimaryKey(txtReferenceNo.Text);
            entity.IsNonMasterOrder = refs.IsNonMasterOrder;
            entity.SRPurchaseOrderType = refs.SRPurchaseOrderType;
            entity.IsTaxable = refs.IsTaxable;
            entity.TaxPercentage = refs.TaxPercentage;
            entity.CurrencyID = refs.CurrencyID;
            entity.CurrencyRate = refs.CurrencyRate;
            entity.InvoiceNo = refs.InvoiceNo;
            entity.ProductAccountID = refs.ProductAccountID;
            entity.IsConsignment = refs.IsConsignment;

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
                totalcharge += ((item.Price * item.Quantity) - (item.Discount * item.Quantity));
            }

            totaltax = refs.TaxPercentage == 0 ? 0 : ((totalcharge.Value * refs.TaxPercentage) / Convert.ToDecimal(100));

            entity.ChargesAmount = (-1) * totalcharge;
            entity.TaxAmount = (-1) * totaltax;
        }

        private void SaveEntity(ItemTransaction entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ItemTransactionItems.Save();

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
                que.Where(que.TransactionNo > txtTransactionNo.Text &&
                          que.TransactionCode == TransactionCode.ReceiptOfSubstitute);
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < txtTransactionNo.Text &&
                          que.TransactionCode == TransactionCode.ReceiptOfSubstitute);
                que.OrderBy(que.TransactionNo.Descending);
            }
            var entity = new ItemTransaction();
            entity.Load(que);
            OnPopulateEntryControl(entity);
            if (grdItemTransactionItem.Columns[6].Visible == false)
                grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 2].Visible = !chkIsApproved.Checked && !chkIsVoid.Checked; // ed ico

        }
        #endregion

        #region Record Detail Method Function

        private ItemTransactionItemCollection ItemTransactionItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["POSubstitute:collItemTransactionItem" + Request.UserHostName];
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
                Session["POSubstitute:collItemTransactionItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["POSubstitute:collItemTransactionItem" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemTransactionItem.Columns[0].Visible = isVisible;
            grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 1].Visible = isVisible;

            if (grdItemTransactionItem.Columns[6].Visible == false)
                grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 2].Visible = !isVisible && !chkIsApproved.Checked; // ed ico


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
        }

        protected void grdItemTransactionItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemTransactionItem entity = ItemTransactionItems.AddNew();
            SetEntityValue(entity, e);

            //grid not close first
            e.Canceled = true;
            grdItemTransactionItem.Rebind();
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

        protected void cboToServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateNewTransactionNo();
            ComboBox.PopulateWithServiceUnitForLocation(cboToLocationID, e.Value);
            cboToLocationID.SelectedIndex = 1;
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
    }
}
