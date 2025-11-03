using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class DistributionRequestDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;
            if (cboFromServiceUnitID.SelectedValue == string.Empty) return;

            var serv = new ServiceUnit();
            if (serv.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue))
            {
                _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, BusinessObject.Reference.TransactionCode.DistributionRequest, serv.DepartmentID);
                txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
            }
        }

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "DistributionRequestSearch.aspx";
            UrlPageList = "DistributionRequestList.aspx";

            ProgramID = AppConstant.Program.DistributionRequest;

            this.WindowSearch.Height = 400;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                var costUnit = new ServiceUnitCollection();
                costUnit.Query.Where(costUnit.Query.IsActive == true);
                costUnit.LoadAll();
                cboServiceUnitCostID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var u in costUnit)
                {
                    cboServiceUnitCostID.Items.Add(new RadComboBoxItem(u.ServiceUnitName, u.ServiceUnitID));
                }

                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                {
                    trServiceUnitCostID.Visible = true;
                    rfvServiceUnitCostID.Visible = true;
                }

                grdItemTransactionItem.Columns.FindByUniqueName("BalanceFrom").Visible = (AppSession.Parameter.IsShowBalanceInfoInDistributionRequest);
                grdItemTransactionItem.Columns.FindByUniqueName("BalanceTo").Visible = (AppSession.Parameter.IsShowBalanceInfoInDistributionRequest);
            }

            //Add Event for Request Order Selection
            AjaxManager.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(AjaxManager_AjaxRequest);
        }

        private void AjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            PopulateFromPickList();
        }

        private void PopulateFromPickList()
        {
            Session.Remove("DistRequest:Selection");

            object obj = Session["DistRequest:ItemSelected"];
            if (obj == null) return;

            //delete previouse item
            if (ItemTransactionItems.Count > 0)
                ItemTransactionItems.MarkAllAsDeleted();

            DataTable dtbSelectedItem = (DataTable)obj;
            if (dtbSelectedItem.Rows.Count == 0) return;
            int i = 0;
            foreach (DataRow row in dtbSelectedItem.Rows)
            {
                if (Convert.ToDecimal(row["QtyInput"]) < 1) continue;
                i++;

                ItemTransactionItem entity = ItemTransactionItems.AddNew();
                entity.ItemID = row["ItemID"].ToString();
                entity.ItemName = row["ItemName"].ToString();
                entity.SequenceNo = string.Format("{0:000}", i);
                entity.Quantity = Convert.ToDecimal(row["QtyInput"]);
                entity.SRItemUnit = row["SRItemUnit"].ToString();
                entity.ConversionFactor = 1;

                var it = new Item();
                it.LoadByPrimaryKey(entity.ItemID);
                if (it.SRItemType == BusinessObject.Reference.ItemType.Medical)
                {
                    var ipm = new ItemProductMedic();
                    if (ipm.LoadByPrimaryKey(entity.ItemID))
                    {
                        entity.CostPrice = ipm.CostPrice;
                        entity.Price = ipm.PriceInBaseUnit;
                        entity.PriceInCurrency = ipm.PriceInBaseUnit;
                    }
                    else
                    {
                        entity.CostPrice = 0;
                        entity.Price = 0;
                        entity.PriceInCurrency = 0;
                    }
                }
                else if (it.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
                {
                    var ipm = new ItemProductNonMedic();
                    if (ipm.LoadByPrimaryKey(entity.ItemID))
                    {
                        entity.CostPrice = ipm.CostPrice;
                        entity.Price = ipm.PriceInBaseUnit;
                        entity.PriceInCurrency = ipm.PriceInBaseUnit;
                    }
                    else
                    {
                        entity.CostPrice = 0;
                        entity.Price = 0;
                        entity.PriceInCurrency = 0;
                    }
                }
                else
                {
                    var ipm = new ItemKitchen();
                    if (ipm.LoadByPrimaryKey(entity.ItemID))
                    {
                        entity.CostPrice = ipm.CostPrice;
                        entity.Price = ipm.PriceInBaseUnit;
                        entity.PriceInCurrency = ipm.PriceInBaseUnit;
                    }
                    else
                    {
                        entity.CostPrice = 0;
                        entity.Price = 0;
                        entity.PriceInCurrency = 0;
                    }
                }
                var bal = new ItemBalance();
                if (bal.LoadByPrimaryKey(cboFromLocationID.SelectedValue, entity.ItemID))
                    entity.Balance = bal.Balance;
                else entity.Balance = 0;

                bal = new ItemBalance();
                if (bal.LoadByPrimaryKey(cboToLocationID.SelectedValue, entity.ItemID))
                    entity.Balance2 = bal.Balance;
                else entity.Balance2 = 0;
            }
            grdItemTransactionItem.DataSource = ItemTransactionItems;
            grdItemTransactionItem.DataBind();

            //Remove session
            Session.Remove("DistRequest:ItemSelected");
            cboSRItemType.Enabled = ItemTransactionItems.Count == 0;
            cboItemGroupID.Enabled = cboSRItemType.Enabled;

            bool isListItemBaseOnLocationTo = false;
            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue) && AppSession.Parameter.DistributionRequestBasedOnLocationToRestriction.Contains(cboToServiceUnitID.SelectedValue))
            {
                isListItemBaseOnLocationTo = true;
            }

            cboFromServiceUnitID.Enabled = isListItemBaseOnLocationTo || cboSRItemType.Enabled;
            cboFromLocationID.Enabled = isListItemBaseOnLocationTo || cboSRItemType.Enabled;
            cboToServiceUnitID.Enabled = !isListItemBaseOnLocationTo || cboSRItemType.Enabled;
            cboToLocationID.Enabled = !isListItemBaseOnLocationTo || cboSRItemType.Enabled;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //cboSRItemType.Enabled = !(DataModeCurrent != AppEnum.DataMode.Read && ItemTransactionItems.Count == 0);
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItemTransactionItem, grdItemTransactionItem);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboSRItemType);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboFromServiceUnitID);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboFromLocationID);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboItemGroupID);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboToServiceUnitID);
            ajax.AddAjaxSetting(grdItemTransactionItem, cboToLocationID);

            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromServiceUnitID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromLocationID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, txtTransactionNo);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboSRItemType);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboItemGroupID);

            ajax.AddAjaxSetting(cboSRItemType, cboItemGroupID);
            ajax.AddAjaxSetting(cboSRItemType, cboToServiceUnitID);
            ajax.AddAjaxSetting(cboSRItemType, cboToLocationID);

            ajax.AddAjaxSetting(cboToServiceUnitID, cboToServiceUnitID);
            ajax.AddAjaxSetting(cboToServiceUnitID, cboToLocationID);

            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                ajax.AddAjaxSetting(cboFromServiceUnitID, cboServiceUnitCostID);

            //Pick List Update
            ajax.AddAjaxSetting(AjaxManager, grdItemTransactionItem);
            ajax.AddAjaxSetting(AjaxManager, cboFromServiceUnitID);
            ajax.AddAjaxSetting(AjaxManager, cboFromLocationID);
            ajax.AddAjaxSetting(AjaxManager, cboSRItemType);
            ajax.AddAjaxSetting(AjaxManager, cboItemGroupID);
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuEditClick()
        {
            string fromServiceUnitId = cboFromServiceUnitID.SelectedValue;
            ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.DistributionRequest, true);
            cboFromServiceUnitID.SelectedValue = fromServiceUnitId;

            string toServiceUnitId = cboToServiceUnitID.SelectedValue;
            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, BusinessObject.Reference.TransactionCode.Distribution, false, string.Empty, cboSRItemType.SelectedValue);
            cboToServiceUnitID.SelectedValue = toServiceUnitId;

            cboSRItemType.Enabled = ItemTransactionItems.Count == 0;
            cboItemGroupID.Enabled = cboSRItemType.Enabled;

            bool isListItemBaseOnLocationTo = false;
            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue) && AppSession.Parameter.DistributionRequestBasedOnLocationToRestriction.Contains(cboToServiceUnitID.SelectedValue))
            {
                isListItemBaseOnLocationTo = true;
            }

            cboFromServiceUnitID.Enabled = isListItemBaseOnLocationTo || cboSRItemType.Enabled;
            cboFromLocationID.Enabled = isListItemBaseOnLocationTo || cboSRItemType.Enabled;
            cboToServiceUnitID.Enabled = !isListItemBaseOnLocationTo || cboSRItemType.Enabled;
            cboToLocationID.Enabled = !isListItemBaseOnLocationTo || cboSRItemType.Enabled;
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
        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
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

        private bool IsProceed(ValidateArgs args)
        {
            var drQ = new ItemTransactionQuery();
            drQ.Where(drQ.ReferenceNo == txtTransactionNo.Text, drQ.IsVoid == false);
            DataTable dtb = drQ.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                args.IsCancel = true;
                args.MessageText = "This transaction can't be canceled, this data has been proceed to another process";
                return false;
            }
            return true;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            if (!AppSession.Parameter.IsDistributionAutoConfirm && AppSession.Parameter.IsUsingValidationPendingBalance)
            {
                if (ItemBalance.IsHasPendingBalance(cboFromLocationID.SelectedValue))
                {
                    args.IsCancel = true;
                    args.MessageText = "This location has pending balance. Distribution request cannot be continued.";
                    return;
                }
            }

            string result = (new ItemTransaction()).Approve(txtTransactionNo.Text, ItemTransactionItems, AppSession.UserLogin.UserID);
            if (result != string.Empty)
            {
                args.MessageText = result;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            if (!IsProceed(args)) return;
            (new ItemTransaction()).UnApprove(txtTransactionNo.Text, ItemTransactionItems, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            (new ItemTransaction()).Void(txtTransactionNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            (new ItemTransaction()).UnVoid(txtTransactionNo.Text, AppSession.UserLogin.UserID);
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ItemTransaction());
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();// DateTime.Now;
            ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.DistributionRequest, true);
            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, BusinessObject.Reference.TransactionCode.Distribution, false);
            cboFromServiceUnitID.SelectedValue = string.Empty;
            cboFromServiceUnitID.Text = string.Empty;
            cboFromLocationID.Items.Clear();
            cboFromLocationID.Text = string.Empty;
            cboToServiceUnitID.SelectedValue = string.Empty;
            cboToServiceUnitID.Text = string.Empty;
            cboToLocationID.Items.Clear();
            cboToLocationID.Text = string.Empty;
            cboServiceUnitCostID.Text = string.Empty;
            cboSRItemType.SelectedValue = string.Empty;
            cboSRItemType.Text = string.Empty;
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
            if (cboFromServiceUnitID.SelectedValue.Equals(cboToServiceUnitID.SelectedValue) && cboFromLocationID.SelectedValue.Equals(cboToLocationID.SelectedValue))
            {
                args.MessageText = "Service Unit and Request To Unit & Location has same value.";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboFromLocationID.SelectedValue) || string.IsNullOrEmpty(cboToLocationID.SelectedValue))
            {
                args.MessageText = "Location required.";
                args.IsCancel = true;
                return;
            }

            var exception = new LocationException();
            if (exception.LoadByPrimaryKey(cboToLocationID.SelectedValue, cboFromLocationID.SelectedValue))
            {
                args.MessageText = "The selected location is an exception for distribution.";
                args.IsCancel = true;
                return;
            }

            PopulateNewTransactionNo();
            // save autonumber immediately to decrease time gap between create and save
            _autoNumber.Save();

            var entity = new ItemTransaction();
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
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (cboFromServiceUnitID.SelectedValue.Equals(cboToServiceUnitID.SelectedValue) && cboFromLocationID.SelectedValue.Equals(cboToLocationID.SelectedValue))
            {
                args.MessageText = "Service Unit and Request To Unit & Location has same value.";
                args.IsCancel = true;
                return;
            }

            if (string.IsNullOrEmpty(cboFromLocationID.SelectedValue) || string.IsNullOrEmpty(cboToLocationID.SelectedValue))
            {
                args.MessageText = "Location required.";
                args.IsCancel = true;
                return;
            }

            var exception = new LocationException();
            if (exception.LoadByPrimaryKey(cboToLocationID.SelectedValue, cboFromLocationID.SelectedValue))
            {
                args.MessageText = "The selected location is an exception for distribution.";
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
            ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.DistributionRequest, false);
            cboFromServiceUnitID.SelectedValue = itemTransaction.FromServiceUnitID;
            if (!string.IsNullOrEmpty(itemTransaction.FromServiceUnitID))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboFromLocationID, itemTransaction.FromServiceUnitID);
                if (!string.IsNullOrEmpty(itemTransaction.FromLocationID))
                    cboFromLocationID.SelectedValue = itemTransaction.FromLocationID;
                else cboFromLocationID.SelectedIndex = 1;
            }
            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, BusinessObject.Reference.TransactionCode.Distribution, false, string.Empty, 
                string.IsNullOrEmpty(itemTransaction.SRItemType) ? string.Empty : itemTransaction.SRItemType);
            cboToServiceUnitID.SelectedValue = itemTransaction.ToServiceUnitID;
            if (!string.IsNullOrEmpty(itemTransaction.ToServiceUnitID))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboToLocationID, itemTransaction.ToServiceUnitID);
                if (!string.IsNullOrEmpty(itemTransaction.ToLocationID))
                    cboToLocationID.SelectedValue = itemTransaction.ToLocationID;
                else cboToLocationID.SelectedIndex = 1;
            }

            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, BusinessObject.Reference.TransactionCode.DistributionRequest);
            cboServiceUnitCostID.SelectedValue = itemTransaction.ServiceUnitCostID;
            cboSRItemType.SelectedValue = itemTransaction.SRItemType;
            chkIsVoid.Checked = itemTransaction.IsVoid ?? false;
            chkIsApproved.Checked = itemTransaction.IsApproved ?? false;
            chkIsClosed.Checked = itemTransaction.IsClosed ?? false;
            txtNotes.Text = itemTransaction.Notes;
            ComboBox.PopulateWithOneItemGroup(cboItemGroupID, itemTransaction.ItemGroupID);
            //Display Data Detail
            PopulateGridDetail();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(ItemTransaction entity)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionCode = BusinessObject.Reference.TransactionCode.DistributionRequest;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.SRItemType = cboSRItemType.SelectedValue;

            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            entity.FromLocationID = cboFromLocationID.SelectedValue;

            entity.ToServiceUnitID = cboToServiceUnitID.SelectedValue;
            entity.ToLocationID = cboToLocationID.SelectedValue;

            entity.ServiceUnitCostID = cboServiceUnitCostID.SelectedValue;
            entity.Notes = txtNotes.Text;
            entity.ItemGroupID = cboItemGroupID.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
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
                    item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
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
                que.Where(que.TransactionNo > txtTransactionNo.Text && que.TransactionCode == BusinessObject.Reference.TransactionCode.DistributionRequest);
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < txtTransactionNo.Text && que.TransactionCode == BusinessObject.Reference.TransactionCode.DistributionRequest);
                que.OrderBy(que.TransactionNo.Descending);
            }
            var entity = new ItemTransaction();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateNewTransactionNo();
            ComboBox.PopulateWithServiceUnitForLocation(cboFromLocationID, e.Value);
            cboFromLocationID.SelectedIndex = 1;
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, BusinessObject.Reference.TransactionCode.DistributionRequest);
            cboServiceUnitCostID.SelectedValue = e.Value;
        }

        protected void cboToServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComboBox.PopulateWithServiceUnitForLocation(cboToLocationID, e.Value);
            cboToLocationID.SelectedIndex = 1;
        }
        protected void cboItemGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            ItemGroupQuery query = new ItemGroupQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.ItemGroupID,
                    query.ItemGroupName
                );
            query.Where
                (
                    query.Or
                        (
                            query.ItemGroupID.Like(searchTextContain),
                            query.ItemGroupName.Like(searchTextContain)
                        ),
                        query.IsActive == true,
                        query.SRItemType == cboSRItemType.SelectedValue
                );

            cboItemGroupID.DataSource = query.LoadDataTable();
            cboItemGroupID.DataBind();
        }

        protected void cboItemGroupID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemGroupName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemGroupID"].ToString();
        }

        #region Record Detail Method Function

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
            grdItemTransactionItem.Rebind();
        }

        private ItemTransactionItemCollection ItemTransactionItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["DistRequest:collItemTransactionItem" + Request.UserHostName];
                    if (obj != null)
                        return ((ItemTransactionItemCollection)(obj));
                }

                var coll = new ItemTransactionItemCollection();
                var query = new ItemTransactionItemQuery("a");
                var itq = new ItemTransactionQuery("b");
                var iq = new ItemQuery("c");
                var bal = new ItemBalanceQuery("d");
                var bal2 = new ItemBalanceQuery("e");
                var asri = new AppStandardReferenceItemQuery("f");

                query.InnerJoin(itq).On(itq.TransactionNo == query.TransactionNo);
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                query.LeftJoin(bal).On(bal.LocationID == itq.FromLocationID && bal.ItemID == query.ItemID);
                query.LeftJoin(bal2).On(bal2.LocationID == itq.ToLocationID && bal2.ItemID == query.ItemID);
                query.LeftJoin(asri).On(bal.SRItemBin == asri.ItemID && asri.StandardReferenceID == "ItemBin");

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy
                    (
                        query.ItemID.Ascending
                    );

                query.Select
                    (
                        query.TransactionNo,
                        query.ItemID,
                        query.SRItemUnit,
                        query.Quantity,
                        query.SequenceNo,
                        query.ReferenceNo,
                        query.ReferenceSequenceNo,
                        query.QuantityFinishInBaseUnit,
                        query.PageNo,
                        query.ConversionFactor,
                        query.CostPrice,
                        query.Price,
                        query.PriceInCurrency,
                        query.Discount1Percentage,
                        query.Discount2Percentage,
                        query.BatchNumber,
                        query.ExpiredDate,
                        query.IsPackage,
                        query.IsBonusItem,
                        query.IsClosed,
                        query.LastUpdateByUserID,
                        query.LastUpdateDateTime,
                        iq.ItemName.As("refToItem_ItemName"),
                        bal.Balance.As("refToItemBalance_Balance"),
                        bal2.Balance.As("refToItemBalance_Balance2"),
                        bal.Minimum.As("refToItemBalance_Minimum"),
                        bal.Maximum.As("refToItemBalance_Maximum"),
                        asri.ItemName.As("refToSRI_ItemBin")
                    );

                coll.Load(query);

                Session["DistRequest:collItemTransactionItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["DistRequest:collItemTransactionItem" + Request.UserHostName] = value; }
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

            String sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemTransactionItemMetadata.ColumnNames.SequenceNo]);
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

            String sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemTransactionItemMetadata.ColumnNames.SequenceNo]);
            ItemTransactionItem entity = FindItemTransactionItem(sequenceNo);
            if (entity != null)
                entity.MarkAsDeleted();

            cboSRItemType.Enabled = ItemTransactionItems.Count == 0;
            cboItemGroupID.Enabled = cboSRItemType.Enabled;

            bool isListItemBaseOnLocationTo = false;
            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue) && AppSession.Parameter.DistributionRequestBasedOnLocationToRestriction.Contains(cboToServiceUnitID.SelectedValue))
            {
                isListItemBaseOnLocationTo = true;
            }

            cboFromServiceUnitID.Enabled = isListItemBaseOnLocationTo || cboSRItemType.Enabled;
            cboFromLocationID.Enabled = isListItemBaseOnLocationTo || cboSRItemType.Enabled;
            cboToServiceUnitID.Enabled = !isListItemBaseOnLocationTo || cboSRItemType.Enabled;
            cboToLocationID.Enabled = !isListItemBaseOnLocationTo || cboSRItemType.Enabled;
        }

        protected void grdItemTransactionItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ItemTransactionItem entity = ItemTransactionItems.AddNew();
            SetEntityValue(entity, e);
            //grid not close first
            e.Canceled = true;
            grdItemTransactionItem.Rebind();
            cboSRItemType.Enabled = ItemTransactionItems.Count == 0;
            cboItemGroupID.Enabled = cboSRItemType.Enabled;

            bool isListItemBaseOnLocationTo = false;
            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue) && AppSession.Parameter.DistributionRequestBasedOnLocationToRestriction.Contains(cboToServiceUnitID.SelectedValue))
            {
                isListItemBaseOnLocationTo = true;
            }

            cboFromServiceUnitID.Enabled = isListItemBaseOnLocationTo || cboSRItemType.Enabled;
            cboFromLocationID.Enabled = isListItemBaseOnLocationTo || cboSRItemType.Enabled;
            cboToServiceUnitID.Enabled = !isListItemBaseOnLocationTo || cboSRItemType.Enabled;
            cboToLocationID.Enabled = !isListItemBaseOnLocationTo || cboSRItemType.Enabled;
        }

        private void SetEntityValue(ItemTransactionItem entity, GridCommandEventArgs e)
        {
            var userControl = (DistributionRequestItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                SetEntityValue(entity, userControl);
            }
        }

        private void SetEntityValue(ItemTransactionItem entity, DistributionRequestItemDetail userControl)
        {

            entity.ItemID = userControl.ItemID;
            entity.ItemName = userControl.ItemName;
            entity.SequenceNo = userControl.SequenceNo;
            entity.Quantity = userControl.Quantity;
            entity.SRItemUnit = userControl.SRItemUnit;
            entity.ConversionFactor = userControl.ConversionFactor;

            var it = new Item();
            it.LoadByPrimaryKey(entity.ItemID);
            if (it.SRItemType == BusinessObject.Reference.ItemType.Medical)
            {
                var ipm = new ItemProductMedic();
                if (ipm.LoadByPrimaryKey(entity.ItemID))
                {
                    entity.CostPrice = ipm.CostPrice;
                    entity.Price = ipm.PriceInBaseUnit * entity.ConversionFactor;
                    entity.PriceInCurrency = ipm.PriceInBaseUnit * entity.ConversionFactor;
                }
                else
                {
                    entity.CostPrice = 0;
                    entity.Price = 0;
                    entity.PriceInCurrency = 0;
                }
            }
            else if (it.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
            {
                var ipm = new ItemProductNonMedic();
                if (ipm.LoadByPrimaryKey(entity.ItemID))
                {
                    entity.CostPrice = ipm.CostPrice;
                    entity.Price = ipm.PriceInBaseUnit * entity.ConversionFactor;
                    entity.PriceInCurrency = ipm.PriceInBaseUnit * entity.ConversionFactor;
                }
                else
                {
                    entity.CostPrice = 0;
                    entity.Price = 0;
                    entity.PriceInCurrency = 0;
                }
            }
            else
            {
                var ipm = new ItemKitchen();
                if (ipm.LoadByPrimaryKey(entity.ItemID))
                {
                    entity.CostPrice = ipm.CostPrice;
                    entity.Price = ipm.PriceInBaseUnit * entity.ConversionFactor;
                    entity.PriceInCurrency = ipm.PriceInBaseUnit * entity.ConversionFactor;
                }
                else
                {
                    entity.CostPrice = 0;
                    entity.Price = 0;
                    entity.PriceInCurrency = 0;
                }
            }
            var bal = new ItemBalance();
            if (bal.LoadByPrimaryKey(cboFromLocationID.SelectedValue, entity.ItemID))
            {
                //var ib = new ItemBalanceQuery("a");
                //var std = new AppStandardReferenceItemQuery("b");
                //ib.InnerJoin(ib).On(ib.ItemID==bal.ItemID);
                //ib.LeftJoin(std).On(ib.SRItemBin == std.ItemID && std.StandardReferenceID == AppEnum.StandardReference.ItemBin);
                // kalo mau ambil value di 1 table, gak perlu pake query, tp langsung ke table itu aja

                var binName = string.Empty;
                if (!string.IsNullOrEmpty(bal.SRItemBin))
                {
                    var std = new AppStandardReferenceItem();
                    if (std.LoadByPrimaryKey(AppEnum.StandardReference.ItemBin.ToString(), bal.SRItemBin))
                        binName = std.ItemName;

                }
                
                entity.Balance = bal.Balance;
                entity.Minimum = bal.Minimum;
                entity.Maximum = bal.Maximum;
                entity.SRItemBinName = binName;
                ;


            }
            else entity.Balance = 0;

            bal = new ItemBalance();
            if (bal.LoadByPrimaryKey(cboToLocationID.SelectedValue, entity.ItemID))
            {
                entity.Balance2 = bal.Balance;
            }
            else entity.Balance2 = 0;

        }

        #endregion

        protected void cboSRItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItemGroupID.Text = string.Empty;
            cboItemGroupID.SelectedValue = null;

            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, BusinessObject.Reference.TransactionCode.Distribution, false, string.Empty, e.Value);
            cboToLocationID.SelectedValue = string.Empty;
            cboToLocationID.Text = string.Empty;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            if (string.IsNullOrEmpty(eventArgument))
                eventArgument = string.Empty;

            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (eventArgument.Contains("addwithbarcode"))
            {
                var barcode = eventArgument.Split('|')[1];
                if (AddItemDetailWithBarcode(barcode))
                {
                    grdItemTransactionItem.Rebind();
                }
                var txtBarcode = (RadTextBox)sourceControl;
                txtBarcode.Text = string.Empty;
                txtBarcode.Focus();
            }

        }

        #region Barcode entry

        private bool AddItemDetailWithBarcode(string barcode)
        {
            //Check hanya untuk type item 11 Medical & 21 Non Medical
            var item = new Item();
            if (!item.LoadByBarcode(barcode))
            {
                // Barcode bisa sbg ItemID
                if (!item.LoadByPrimaryKey(barcode))
                    return false;
            }

            var itemID = item.ItemID;

            if (item.SRItemType != cboSRItemType.SelectedValue)
                return false;

            //Check bila sudah ada maka tambah di qty nya saja
            //foreach (var transactionItem in ItemTransactionItems)
            //{
            //    if (transactionItem.ItemID == itemID)
            //    {
            //        entity = transactionItem;
            //        break;
            //    }
            //}
            ItemTransactionItem entity = ItemTransactionItems.FirstOrDefault(transactionItem => transactionItem.ItemID == itemID);
            if (entity != null)
            {
                entity.Quantity += 1;
            }
            else
            {
                var sequenceNo = ItemTransactionItems.Count > 0
                   ? string.Format("{0:000}", int.Parse(ItemTransactionItems[ItemTransactionItems.Count - 1].SequenceNo) + 1)
                   : "001";
                entity = ItemTransactionItems.AddNew();
                var itemEntry = (DistributionRequestItemDetail)LoadControl("DistributionRequestItemDetail.ascx");
                itemEntry.PopulateWithItemID(item.ItemID, item.SRItemType);
                SetEntityValue(entity, itemEntry);

                entity.SequenceNo = sequenceNo;
                entity.ItemName = item.ItemName;
                entity.ItemID = itemID;
                entity.Quantity = 1;
            }

            return true;
        }

        #endregion
    }
}