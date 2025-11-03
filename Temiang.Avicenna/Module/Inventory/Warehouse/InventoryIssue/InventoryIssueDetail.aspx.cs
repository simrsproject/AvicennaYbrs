using System;
using System.Linq;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Text;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class InventoryIssueDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private string getPageID
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["list"]))
                    return "n";

                return Request.QueryString["list"];
            }
        }

        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;
            if (cboFromServiceUnitID.SelectedValue == string.Empty) return;

            var serv = new ServiceUnit();
            if (serv.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue))
            {
                _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date, TransactionCode.InventoryIssueOut, serv.DepartmentID);
                txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "InventoryIssueSearch.aspx";
            //UrlPageList = string.IsNullOrEmpty(Request.QueryString["rod"]) ? AppParameter.GetParameterValue(AppParameter.ParameterItem.IsInventoryIssueUsingRequest).ToLower() == "yes" ? "InventoryIssueOrderList.aspx" : "InventoryIssueList.aspx" : "../ItemRequestMaintenance/ItemRequestMaintenanceList.aspx?su=" + Request.QueryString["su"] + "&it=" + Request.QueryString["it"];
            UrlPageList = string.IsNullOrEmpty(Request.QueryString["rod"]) ? getPageID == "y" ? "InventoryIssueOrderList.aspx" : "InventoryIssueList.aspx" : "../ItemRequestMaintenance/ItemRequestMaintenanceList.aspx?su=" + Request.QueryString["su"] + "&it=" + Request.QueryString["it"];

            ProgramID = AppConstant.Program.InventoryIssue;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                //ViewState["ReferenceNo" + Request.UserHostName] = string.Empty;

                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);

                ComboBox.SelectedValue(cboSRItemType, ItemType.Medical);

                if (!AppSession.Parameter.IsTxUsingEdDetail)
                    grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = false; //ico ed

                //trRefNo.Visible = AppSession.Parameter.IsInventoryIssueUsingRequest == "Yes";
                trRefNo.Visible = getPageID == "y";
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            //ToolBarMenuSearch.Enabled = string.IsNullOrEmpty(getPageID);
            ToolBarMenuSearch.Enabled = getPageID == "n";

            if (!string.IsNullOrEmpty(Request.QueryString["req"]))
            {
                ToolBarMenuAdd.Enabled = false;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            //ajax.AddAjaxSetting(cboFromServiceUnitID, txtTransactionNo);
            //ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromServiceUnitID);
            //ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromLocationID);
            //ajax.AddAjaxSetting(cboFromServiceUnitID, cboToServiceUnitID);
            //ajax.AddAjaxSetting(cboFromServiceUnitID, cboSRItemType);

            //ajax.AddAjaxSetting(grdItemTransactionItem, grdItemTransactionItem);
            //ajax.AddAjaxSetting(grdItemTransactionItem, cboFromServiceUnitID);
            //ajax.AddAjaxSetting(grdItemTransactionItem, cboFromLocationID);
            //ajax.AddAjaxSetting(grdItemTransactionItem, cboSRItemType);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
            string fromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, TransactionCode.InventoryIssueOut, true);
            cboFromServiceUnitID.SelectedValue = fromServiceUnitID;

            string toServiceUnitID = cboToServiceUnitID.SelectedValue;
            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.InventoryIssueRequestOut, false);
            cboToServiceUnitID.SelectedValue = toServiceUnitID;

            cboSRItemType.Enabled = ItemTransactionItems.Count == 0;
            cboFromServiceUnitID.Enabled = cboSRItemType.Enabled;
            cboFromLocationID.Enabled = cboSRItemType.Enabled;
            if (!string.IsNullOrEmpty(txtReferenceNo.Text))
                cboToServiceUnitID.Enabled = false;
            else
                cboToServiceUnitID.Enabled = cboFromServiceUnitID.SelectedValue == AppSession.Parameter.ServiceUnitLogisticCentralWarehouseId;

            if (AppSession.Parameter.IsTxUsingEdDetail)
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = false; // ed ico
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
            if (ItemTransactionItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            if (!AppSession.Parameter.IsCanProcessExceededRequestOnInventoryIssueOut)
            {
                string process = ItemTransaction.IsItemAlreadyProcess(txtTransactionNo.Text, ItemTransactionItems);
                if (process != string.Empty)
                {
                    args.MessageText = process;
                    args.IsCancel = true;
                    return;
                }
            }

            string result = ItemTransaction.IsItemMinusProcess(txtTransactionNo.Text, ItemTransactionItems);
            if (result != string.Empty)
            {
                args.MessageText = result;
                args.IsCancel = true;
                return;
            }

            var loc = new Location();
            if (loc.LoadByPrimaryKey(cboFromLocationID.SelectedValue) && loc.IsHoldForTransaction == true)
            {
                args.MessageText = "Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.";
                args.IsCancel = true;
                return;
            }

            using (var trans = new esTransactionScope())
            {
                var entity = new ItemTransaction();
                entity.LoadByPrimaryKey(txtTransactionNo.Text);

                if (entity.IsApproved ?? false) {
                    args.MessageText = "Transaction has been approved";
                    args.IsCancel = true;
                    return;
                }

                entity.IsApproved = true;
                entity.ApprovedDate = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                entity.Save();

                // stock calculation
                var chargesBalances = new ItemBalanceCollection();
                var chargesDetailBalances = new ItemBalanceDetailCollection();
                var chargesMovements = new ItemMovementCollection();
                var itemBalanceDetailEds = new ItemBalanceDetailEdCollection();

                string itemNoStock;
                var itemTransactionItems = ItemTransactionItems;

                ItemTransaction entityRef;
                ItemTransactionItemCollection collRef;
                ItemTransactionItemEdCollection collEdRef;
                ItemTransaction.PrepareUpdateReferenceItem(entity.TransactionCode, entity.ReferenceNo, entity.TransactionNo, true, AppSession.UserLogin.UserID, out entityRef,
                out collRef, out collEdRef);
                
                ItemBalance.PrepareItemBalances(entity, itemTransactionItems, entity.FromServiceUnitID, entity.FromLocationID, AppSession.UserLogin.UserID,
                   ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, ref itemBalanceDetailEds, out itemNoStock, AppSession.Parameter.IsEnabledStockWithEdControl);

                // close
                if (!string.IsNullOrEmpty(entity.ReferenceNo))
                {
                    if (AppSession.Parameter.IsCloseOutstandingIssueRequest)
                    {
                        entityRef.IsClosed = true;
                        foreach (var r in collRef)
                        {
                            r.IsClosed = true;
                        }
                    }
                }
                //

                if (!string.IsNullOrEmpty(itemNoStock))
                {
                    if (itemNoStock.Length >= 5 && itemNoStock.Substring(0, 5) == "Zero|")
                        args.MessageText = "Zero cost price of item : " + itemNoStock.Replace("Zero|", "");
                    else
                        args.MessageText = "Insufficient balance of item : " + itemNoStock;

                    args.IsCancel = true;
                    return;
                }


                if (string.IsNullOrEmpty(entity.ReferenceNo) || !AppSession.Parameter.IsInventoryIssueNeedConfirm)
                {
                    foreach (var item in itemTransactionItems)
                    {
                        item.RequestQty = item.Quantity;
                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        item.ApprovedByUserID = AppSession.UserLogin.UserID;
                        item.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                        item.IsClosed = true;
                    }

                    itemTransactionItems.Save();

                    if (entityRef != null)
                        entityRef.Save();
                    if (collRef != null)
                        collRef.Save();
                    if (collEdRef != null)
                        collEdRef.Save();

                    if (chargesBalances != null)
                        chargesBalances.Save();
                    if (chargesDetailBalances != null)
                        chargesDetailBalances.Save();
                    if (itemBalanceDetailEds != null)
                        itemBalanceDetailEds.Save();
                    if (chargesMovements != null)
                        chargesMovements.Save();
                    
                    var app = new AppParameter();
                    app.LoadByPrimaryKey("acc_IsAutoJournalInvIssue");
                    if (app.ParameterValue == "Yes")
                    {
                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.TransactionDate.Value);
                        if (isClosingPeriod)
                        {
                            args.MessageText = "Financial statements for period: " +
                                               string.Format("{0:MMMM-yyyy}", entity.TransactionDate) +
                                               " have been closed. Please contact the authorities.";
                            args.IsCancel = true;
                            return;
                        }

                        /* Automatic Journal Testing Start */

                        int? journalId = JournalTransactions.AddNewInventoryIssueJournal(entity, AppSession.UserLogin.UserID, 0);

                        /* Automatic Journal Testing End */
                    }
                }
                else
                {
                    itemTransactionItems.Save();

                    if (entityRef != null)
                        entityRef.Save();
                    if (collRef != null)
                        collRef.Save();
                    if (collEdRef != null)
                        collEdRef.Save();
                }

                trans.Complete();
            }

            if (AppSession.Parameter.IsTxUsingEdDetail)
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = false; // ed ico

        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new ItemTransaction();
            entity.LoadByPrimaryKey(txtTransactionNo.Text);

            if ((entity.ApprovedDate ?? (new DateTime()).NowAtSqlServer()).Date != (new DateTime()).NowAtSqlServer().Date)
            {
                args.MessageText = "Transaction is expired.";
                args.IsCancel = true;
                return;
            }

            using (var trans = new esTransactionScope())
            {
                entity.IsApproved = false;
                entity.ApprovedDate = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                entity.IsVoid = true;
                entity.VoidByUserID = AppSession.UserLogin.UserID;
                entity.VoidDate = (new DateTime()).NowAtSqlServer();
                entity.Save();

                // stock calculation
                var chargesBalances = new ItemBalanceCollection();
                var chargesDetailBalances = new ItemBalanceDetailCollection();
                var chargesMovements = new ItemMovementCollection();
                var itemBalanceDetailEds = new ItemBalanceDetailEdCollection();

                var itemTransactionItems = ItemTransactionItems;

                ItemBalance.PrepareItemBalancesForInventoryIssueVoid(entity, itemTransactionItems, entity.FromServiceUnitID, entity.FromLocationID, AppSession.UserLogin.UserID,
                   ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, ref itemBalanceDetailEds, AppSession.Parameter.IsEnabledStockWithEdControl);

                itemTransactionItems.Save();

                if (string.IsNullOrEmpty(entity.ReferenceNo) || !AppSession.Parameter.IsInventoryIssueNeedConfirm)
                {
                    if (chargesBalances != null)
                        chargesBalances.Save();
                    if (chargesDetailBalances != null)
                        chargesDetailBalances.Save();
                    if (itemBalanceDetailEds != null)
                        itemBalanceDetailEds.Save();
                    if (chargesMovements != null)
                        chargesMovements.Save();

                    //Commit if success, Rollback if failed

                    var app = new AppParameter();
                    app.LoadByPrimaryKey("acc_IsAutoJournalInvIssue");
                    if (app.ParameterValue == "Yes")
                    {
                        /* Automatic Journal Testing Start */

                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(entity.TransactionDate.Value);
                        if (isClosingPeriod)
                        {
                            args.MessageText = "Financial statements for period: " +
                                               string.Format("{0:MMMM-yyyy}", entity.TransactionDate) +
                                               " have been closed. Please contact the authorities.";
                            args.IsCancel = true;
                            return;
                        }

                        int? journalId = JournalTransactions.AddNewInventoryIssueVoidJournal(entity, AppSession.UserLogin.UserID, 0);

                        /* Automatic Journal Testing End */
                    }
                }

                trans.Complete();
            }

            if (AppSession.Parameter.IsTxUsingEdDetail)
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = false; // ed ico

        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            (new ItemTransaction()).Void(txtTransactionNo.Text, AppSession.UserLogin.UserID, getPageID);
            if (AppSession.Parameter.IsTxUsingEdDetail) 
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = false; // ed ico
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            (new ItemTransaction()).UnVoid(txtTransactionNo.Text, AppSession.UserLogin.UserID);
            if (AppSession.Parameter.IsTxUsingEdDetail) 
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = !AppSession.Parameter.IsEnabledStockWithEdControl; // ed ico
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

            ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, TransactionCode.InventoryIssueOut, true);
            ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.InventoryIssueRequestOut, false);
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();// DateTime.Now;
            PopulateNewTransactionNo();
            cboFromServiceUnitID.Text = string.Empty;
            cboToServiceUnitID.Text = string.Empty;
            cboFromLocationID.Items.Clear();
            cboFromLocationID.Text = string.Empty;

            if (!string.IsNullOrEmpty(Request.QueryString["req"]))
            {
                PopulateFromRequest();

                var cstext1 = new StringBuilder();
                cstext1.Append("<script type=text/javascript> __doPostBack('ctl00$ContentPlaceHolder1$grdItemTransactionItem$ctl00$ctl02$ctl00$RebindGridButton','') </script>");
                Page.ClientScript.RegisterStartupScript(GetType(), "OpenAddNewRecordGrid", cstext1.ToString());
            }
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

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            ItemTransaction entity = new ItemTransaction();
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

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
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
            txtReferenceNo.Text = itemTransaction.ReferenceNo;
            txtTransactionDate.SelectedDate = itemTransaction.TransactionDate;
            ComboBox.PopulateWithOneServiceUnit(cboFromServiceUnitID, itemTransaction.FromServiceUnitID ?? string.Empty);
            if (!string.IsNullOrEmpty(itemTransaction.FromServiceUnitID))
            {
                ComboBox.PopulateWithServiceUnitForLocation(cboFromLocationID, itemTransaction.FromServiceUnitID);
                if (!string.IsNullOrEmpty(itemTransaction.FromLocationID)) cboFromLocationID.SelectedValue = itemTransaction.FromLocationID;
                else cboFromLocationID.SelectedIndex = 1;
            }

            ComboBox.PopulateWithOneServiceUnit(cboToServiceUnitID, itemTransaction.ToServiceUnitID ?? string.Empty);
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, TransactionCode.InventoryIssueOut);
            cboSRItemType.SelectedValue = itemTransaction.SRItemType;
            chkIsVoid.Checked = itemTransaction.IsVoid ?? false;
            chkIsApproved.Checked = itemTransaction.IsApproved ?? false;
            txtNotes.Text = itemTransaction.Notes;

            //Display Data Detail
            PopulateGridDetail();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ItemTransaction entity)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionCode = TransactionCode.InventoryIssueOut;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.ReferenceNo = txtReferenceNo.Text;//ViewState["ReferenceNo" + Request.UserHostName] == null ? string.Empty : ViewState["ReferenceNo" + Request.UserHostName].ToString();
            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            entity.FromLocationID = cboFromLocationID.SelectedValue;
            entity.ToServiceUnitID = cboToServiceUnitID.SelectedValue;
            entity.SRItemType = cboSRItemType.SelectedValue;
            entity.Notes = txtNotes.Text;

            if (!string.IsNullOrEmpty(entity.ReferenceNo))
            {
                var txRef = new ItemTransaction();
                if (txRef.LoadByPrimaryKey(entity.ReferenceNo))
                {
                    entity.ServiceUnitCostID = string.IsNullOrEmpty(txRef.ServiceUnitCostID) ? txRef.FromServiceUnitID : txRef.ServiceUnitCostID;
                }
            }

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

                //if (!string.IsNullOrEmpty(Request.QueryString["req"]))
                //{
                //    var e2 = new ItemTransaction();
                //    e2.LoadByPrimaryKey(Request.QueryString["req"]);
                //    e2.ReferenceDate = entity.TransactionDate;
                //    e2.ReferenceNo = entity.TransactionNo;
                //    e2.Save();
                //}

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
                que.Where
                    (
                        que.TransactionNo > txtTransactionNo.Text &&
                        que.TransactionCode == TransactionCode.InventoryIssueOut
                    );
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.TransactionNo < txtTransactionNo.Text &&
                        que.TransactionCode == TransactionCode.InventoryIssueOut
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }

            ItemTransaction entity = new ItemTransaction();
            entity.Load(que);
            OnPopulateEntryControl(entity);

            if (AppSession.Parameter.IsTxUsingEdDetail)
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = !chkIsApproved.Checked && !chkIsVoid.Checked && !AppSession.Parameter.IsEnabledStockWithEdControl; // ed ico
        }

        #endregion

        #region Record Detail Method Function

        private ItemTransactionItemCollection ItemTransactionItems
        {
            get
            {
                //if (IsPostBack)
                //{
                    object obj = Session["InventoryIssue:collItemTransactionItem" + Request.UserHostName];
                    if (obj != null)
                        return ((ItemTransactionItemCollection)(obj));
                //}

                ItemTransactionItemCollection coll = new ItemTransactionItemCollection();
                ItemTransactionItemQuery query = new ItemTransactionItemQuery("a");
                ItemTransactionQuery it = new ItemTransactionQuery("it");

                ItemQuery iq = new ItemQuery("b");
                var ibq = new ItemBalanceQuery("e");

                query.InnerJoin(iq).On(query.ItemID == iq.ItemID)
                    .InnerJoin(it).On(query.TransactionNo == it.TransactionNo)
                    .LeftJoin(ibq).On(query.ItemID == ibq.ItemID && ibq.LocationID == it.FromLocationID);

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.ItemID.Ascending);

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName"),
                        @"<ISNULL(e.Balance, 0)/a.ConversionFactor AS 'refToItemBalance_Balance'>"
                    );

                if (cboSRItemType.SelectedValue == ItemType.Medical)
                {
                    var ipq = new ItemProductMedicQuery("f");
                    query.LeftJoin(ipq).On(query.ItemID == ipq.ItemID);
                    query.Select(@"<ISNULL(f.IsControlExpired, 0) AS 'refToItemProduct_IsControlExpired'>");
                }
                else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                {
                    var ipq = new ItemProductNonMedicQuery("f");
                    query.LeftJoin(ipq).On(query.ItemID == ipq.ItemID);
                    query.Select(@"<ISNULL(f.IsControlExpired, 0) AS 'refToItemProduct_IsControlExpired'>");
                }
                else
                {
                    var ipq = new ItemKitchenQuery("f");
                    query.LeftJoin(ipq).On(query.ItemID == ipq.ItemID);
                    query.Select(@"<ISNULL(f.IsControlExpired, 0) AS 'refToItemProduct_IsControlExpired'>");
                }


                coll.Load(query);
                Session["InventoryIssue:collItemTransactionItem" + Request.UserHostName] = coll;

                return coll;
            }
            set { Session["InventoryIssue:collItemTransactionItem" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemTransactionItem.Columns[0].Visible = isVisible;
            grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 1].Visible = isVisible;

            if (AppSession.Parameter.IsTxUsingEdDetail)
                grdItemTransactionItem.Columns.FindByUniqueName("editED").Visible = !isVisible && !chkIsApproved.Checked && !AppSession.Parameter.IsEnabledStockWithEdControl; // ed ico

            grdItemTransactionItem.MasterTableView.CommandItemDisplay = isVisible && string.IsNullOrEmpty(txtReferenceNo.Text) ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

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
            if (editedItem == null)
                return;

            String sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                [ItemTransactionItemMetadata.ColumnNames.SequenceNo]);

            ItemTransactionItem entity = FindItemTransactionItem(sequenceNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        private ItemTransactionItem FindItemTransactionItem(String sequenceNo)
        {
            return ItemTransactionItems.Where(x => x.SequenceNo == sequenceNo &&
                (x.TransactionNo ?? string.Empty) == (x.es.IsAdded ? string.Empty : txtTransactionNo.Text)).First();
            
        }

        protected void grdItemTransactionItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemTransactionItemMetadata.ColumnNames.SequenceNo]);

            ItemTransactionItem entity = FindItemTransactionItem(sequenceNo);
            if (entity != null) entity.MarkAsDeleted();

            cboFromServiceUnitID.Enabled = !(ItemTransactionItems.Count > 0);
            cboSRItemType.Enabled = !(ItemTransactionItems.Count > 0);
            cboFromLocationID.Enabled = !(ItemTransactionItems.Count > 0);
        }

        protected void grdItemTransactionItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            #region Generate SequenceNo
            // jika insert maka bikin sequenceno disini saja
            // kasus RSUI sering error duplikat sequence pada saat insert tapi timeout ajax client
            // makanya generate sequence dipindah kemari
            InventoryIssueItemDetail userControl = (InventoryIssueItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (ItemTransactionItems.Count == 0)
                userControl.SequenceNo = "001";
            else
            {
                int seqNo = 0;
                foreach (ItemTransactionItem item in ItemTransactionItems)
                {
                    if (int.Parse(item.SequenceNo) > seqNo)
                        seqNo = int.Parse(item.SequenceNo);
                }
                userControl.SequenceNo = string.Format("{0:000}", seqNo + 1);
            }
            // end of bikin sequenceno
            #endregion

            ItemTransactionItem entity = ItemTransactionItems.AddNew();
            SetEntityValue(entity, e);

            //grid not close first
            e.Canceled = true;
            grdItemTransactionItem.Rebind();
        }

        private void SetEntityValue(ItemTransactionItem entity, GridCommandEventArgs e)
        {
            InventoryIssueItemDetail userControl = (InventoryIssueItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.SequenceNo = userControl.SequenceNo;
                entity.Quantity = userControl.Quantity;
                entity.ItemName = userControl.ItemName;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ConversionFactor = userControl.ConversionFactor; 
                if (cboSRItemType.SelectedValue == ItemType.Medical)
                {
                    ItemProductMedic med = new ItemProductMedic();
                    med.LoadByPrimaryKey(entity.ItemID);
                    entity.CostPrice = med.CostPrice;
                    entity.Price = med.PriceInBasedUnitWVat;
                    entity.IsControlExpired = med.IsControlExpired ?? false;
                }
                else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                {
                    ItemProductNonMedic nonMed = new ItemProductNonMedic();
                    nonMed.LoadByPrimaryKey(entity.ItemID);
                    entity.CostPrice = nonMed.CostPrice;
                    entity.Price = nonMed.PriceInBasedUnitWVat;
                    entity.IsControlExpired = nonMed.IsControlExpired ?? false;
                }
                else
                {
                    var kitchen = new ItemKitchen();
                    kitchen.LoadByPrimaryKey(entity.ItemID);
                    entity.CostPrice = kitchen.CostPrice;
                    entity.Price = kitchen.PriceInBasedUnitWVat;
                    entity.IsControlExpired = kitchen.IsControlExpired ?? false;
                }
                var balance = new ItemBalance();
                if (balance.LoadByPrimaryKey(cboFromLocationID.SelectedValue, entity.ItemID))
                    entity.Balance = balance.Balance / entity.ConversionFactor;
                else entity.Balance = 0;
            }
        }

        #endregion

        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateNewTransactionNo();
            ComboBox.PopulateWithServiceUnitForLocation(cboFromLocationID, e.Value);
            cboFromLocationID.SelectedIndex = 1;
            cboToServiceUnitID.SelectedValue = e.Value;

            if (string.IsNullOrEmpty(txtReferenceNo.Text))
                cboToServiceUnitID.Enabled = (e.Value == AppSession.Parameter.ServiceUnitLogisticCentralWarehouseId || AppSession.Parameter.IsAllowInventoryIssueWithoutRequest);
            else 
                cboToServiceUnitID.Enabled = false;
            
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, TransactionCode.InventoryIssueOut);
        }

        private void PopulateFromRequest()
        {
            var it = new ItemTransaction();
            it.LoadByPrimaryKey(Request.QueryString["req"]);

            cboFromServiceUnitID.SelectedValue = it.ToServiceUnitID;
            cboFromServiceUnitID_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, cboFromServiceUnitID.SelectedValue, string.Empty));
            cboFromLocationID.SelectedValue = it.ToLocationID;
            cboToServiceUnitID.SelectedValue = it.FromServiceUnitID;
            txtTransactionDate.SelectedDate = DateTime.Now;//it.TransactionDate;
            cboSRItemType.SelectedValue = it.SRItemType;
            txtNotes.Text = it.Notes;
            txtReferenceNo.Text = it.TransactionNo;
            cboFromServiceUnitID.Enabled = false;
            cboToServiceUnitID.Enabled = false;
            cboSRItemType.Enabled = false;

            var coll = new ItemTransactionItemCollection();
            var qi = new ItemTransactionItemQuery("i");
            var qit = new ItemTransactionQuery("it");
            var qb = new ItemBalanceQuery("b");

            qi.InnerJoin(qit).On(qi.TransactionNo == qit.TransactionNo)
                .LeftJoin(qb).On(qi.ItemID == qb.ItemID && qit.ToLocationID == qb.LocationID)
                .Where(qi.TransactionNo == it.TransactionNo)
                .Select(qi, qb.Balance.As("refToItemBalance_Balance"));
            coll.Load(qi);

            ItemTransactionItems = null; //Reset Record Detail

            foreach (var c in coll)
            {
                decimal qtyFinished = 0;
                var itiref = new ItemTransactionItemQuery("a");
                var itref = new ItemTransactionQuery("b");
                itiref.InnerJoin(itref).On(itref.TransactionNo == itiref.TransactionNo);
                itiref.Where(itref.TransactionCode == TransactionCode.InventoryIssueOut, itref.IsVoid == false,
                             itiref.ReferenceNo == c.TransactionNo, itiref.ReferenceSequenceNo == c.SequenceNo);
                itiref.Select(@"<ISNULL(SUM(a.Quantity), 0) AS QtyFinished>");
                DataTable dtbref = itiref.LoadDataTable();
                if (dtbref.Rows.Count > 0)
                    qtyFinished = Convert.ToDecimal(dtbref.Rows[0]["QtyFinished"]);

                if (c.Quantity - qtyFinished > 0)
                {
                    var entity = ItemTransactionItems.AddNew();
                    entity.ItemID = c.ItemID;
                    entity.SequenceNo = c.SequenceNo;
                    entity.ReferenceNo = c.TransactionNo;
                    entity.ReferenceSequenceNo = c.SequenceNo;
                    entity.Quantity = c.Quantity - qtyFinished;
                    entity.Balance = c.Balance;

                    var item = new Item();
                    item.LoadByPrimaryKey(entity.ItemID);
                    entity.ItemName = item.ItemName;

                    entity.SRItemUnit = c.SRItemUnit;
                    entity.ConversionFactor = c.ConversionFactor; //Transaksi inventory out selalu dalam Base Unit
                    if (cboSRItemType.SelectedValue == ItemType.Medical)
                    {
                        var med = new ItemProductMedic();
                        med.LoadByPrimaryKey(entity.ItemID);
                        entity.CostPrice = med.CostPrice;
                        entity.Price = med.PriceInBasedUnitWVat;
                        entity.IsControlExpired = med.IsControlExpired ?? false;
                    }
                    else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                    {
                        var nonMed = new ItemProductNonMedic();
                        nonMed.LoadByPrimaryKey(entity.ItemID);
                        entity.CostPrice = nonMed.CostPrice;
                        entity.Price = nonMed.PriceInBasedUnitWVat;
                        entity.IsControlExpired = nonMed.IsControlExpired ?? false;
                    }
                    else
                    {
                        var kitchen = new ItemKitchen();
                        kitchen.LoadByPrimaryKey(entity.ItemID);
                        entity.CostPrice = kitchen.CostPrice;
                        entity.Price = kitchen.PriceInBasedUnitWVat;
                        entity.IsControlExpired = kitchen.IsControlExpired ?? false;
                    }
                }
            }

            //grdItemTransactionItem.Rebind();
            grdItemTransactionItem.DataSource = ItemTransactionItems;
            grdItemTransactionItem.MasterTableView.IsItemInserted = false;
            grdItemTransactionItem.MasterTableView.ClearEditItems();
            grdItemTransactionItem.DataBind();
        }
    }
}