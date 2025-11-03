using System;
using System.Web.UI;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Module.Inventory.StockOpname;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.Inventory.Stock
{
    public partial class StockOpnameDetail : BasePageDetail
    {
        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"openWinStockOpnameAdd('{0}'); args.set_cancel(true);", AppSession.Parameter.IsStockOpnamePerGroupItem);
            return script;
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "StockOpnameSearch.aspx";
            UrlPageList = "StockOpnameList.aspx";

            ProgramID = AppConstant.Program.StockOpname;

            WindowSearch.Height = 400;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                DataGridDataTable = null;
                grdItemTransactionItem.Columns.FindByUniqueName("PrevQty").Visible = AppSession.Parameter.IsShowSystemQtyInStockTacking;
                grdItemTransactionItem.Columns.FindByUniqueName("BatchNumber").Visible = AppSession.Parameter.IsEnabledStockWithEdControl;
                grdItemTransactionItem.Columns.FindByUniqueName("ExpiredDate").Visible = AppSession.Parameter.IsEnabledStockWithEdControl;

                grdItemTransactionItem.Columns.FindByUniqueName("Quantity").Visible = !AppSession.Parameter.IsEnabledStockWithEdControl;
                grdItemTransactionItem.Columns.FindByUniqueName("QuantityEd").Visible = AppSession.Parameter.IsEnabledStockWithEdControl;

                grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 1].Visible = false;
            }
        }


        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            if (cboPageNo.SelectedValue.ToInt() == 0)
            {
                args.MessageText = "Current Page cannot Approved";
                args.IsCancel = true;
                return;
            }

            ItemTransaction entity = new ItemTransaction();
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

            AppParameter app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalStockOpname");
            if (app.ParameterValue == "Yes")
            {
                var closingperiod = entity.TransactionDate.Value.Date;
                var isClosingPeriod = PostingStatus.IsPeriodeClosed(closingperiod);
                if (isClosingPeriod)
                {
                    args.MessageText = "Financial statements for period: " +
                                       string.Format("{0:MMMM-yyyy}", closingperiod) +
                                       " have been closed. Please contact the authorities.";
                    args.IsCancel = true;
                    return;
                }
            }

            var pageNo = cboPageNo.SelectedValue.ToInt();
            if (pageNo > 0)
            {
                //using (var trans = new esTransactionScope())
                //{
                //    Approval(args, entity, cboPageNo.SelectedValue.ToInt());

                //    // Commit
                //    trans.Complete();
                //}
                Approval(args, entity, cboPageNo.SelectedValue.ToInt());
            }
            else
            {
                try
                {
                    var apprNewColl = new ItemStockOpnameApprovalCollection();

                    using (var trans = new esTransactionScope())
                    {
                        var itemTransactionItems = GetDataGridCollection(pageNo, txtTransactionNo.Text);
                        if (!ZeroCostStatusByPass(args, pageNo, itemTransactionItems))
                            return;

                        // Pindah ke bbrp page
                        var itiq = new ItemTransactionItemQuery();
                        var itiColl = new ItemTransactionItemCollection();
                        itiq.Where(itiq.TransactionNo == txtTransactionNo.Text, itiq.PageNo == pageNo);
                        itiq.OrderBy(itiq.SequenceNo.Ascending);
                        itiColl.Load(itiq);



                        // Last Page
                        var stkApprQr = new ItemStockOpnameApprovalQuery();
                        stkApprQr.Where(stkApprQr.TransactionNo == txtTransactionNo.Text);
                        stkApprQr.OrderBy(stkApprQr.PageNo.Descending);
                        stkApprQr.es.Top = 1;

                        var stkAppr = new ItemStockOpnameApproval();
                        if (stkAppr.Load(stkApprQr))
                        {
                            pageNo = stkAppr.PageNo ?? 0;
                        }

                        var itiCollNew = new ItemTransactionItemCollection();
                        const int maxPageSize = 30;
                        int i = maxPageSize - 1; // Supaya langsung membuat page baru
                        foreach (var item in itiColl)
                        {
                            i++;
                            if (pageNo == 0 || i == maxPageSize)
                            {
                                i = 0;

                                // Add Page
                                pageNo++;

                                // Create new page
                                var approval = apprNewColl.AddNew();
                                approval.TransactionNo = txtTransactionNo.Text;
                                approval.PageNo = pageNo;
                                approval.IsApproved = false;
                            }

                            item.PageNo = pageNo;
                        }

                        // Save new page
                        apprNewColl.Save();

                        itiColl.Save();

                        // Commit
                        trans.Complete();
                    }


                    // Approve
                    var pageFailed = string.Empty;
                    var lastErr = string.Empty;
                    foreach (var line in apprNewColl)
                    {
                        try
                        {
                            var appr = new ItemStockOpnameApproval();
                            appr.LoadByPrimaryKey(line.TransactionNo, line.PageNo ?? 0);

                            pageNo = appr.PageNo ?? 0;
                            var itemTransactionItems = GetDataGridCollection(pageNo, txtTransactionNo.Text);
                            SetApproval(entity, itemTransactionItems, appr, pageNo);
                        }
                        catch (Exception ex)
                        {
                            lastErr = ex.Message;
                            pageFailed = pageFailed + ", " + pageNo.ToString();
                        }
                    }

                    ComboBox.SelectedValue(cboPageNo, pageNo.ToString());

                    if (!string.IsNullOrEmpty(pageFailed))
                    {
                        args.IsCancel = true;
                        args.MessageText = string.Format("Approval proses failed in page : {0} {1}", pageFailed, lastErr);
                    }
                }
                catch (Exception ex)
                {
                    args.IsCancel = true;
                    args.MessageText = ex.Message;
                }
            }
        }

        private bool Approval(ValidateArgs args, ItemTransaction entity, int pageNo)
        {
            ItemStockOpnameApproval appr;
            if (!ApprovedStatusByPass(args, pageNo, out appr))
                return false;

            var itemTransactionItems = GetDataGridCollection(pageNo, txtTransactionNo.Text);
            if (!ZeroCostStatusByPass(args, pageNo, itemTransactionItems))
                return false;

            if (!StockMutatedStatusByPass(args, pageNo, itemTransactionItems))
                return false;
         
            return SetApproval(entity, itemTransactionItems, appr, pageNo);
        }

        private bool ZeroCostStatusByPass(ValidateArgs args, int pageNo, ItemTransactionItemCollection itemTransactionItems)
        {
            string itemZeroCostPrice;
            ItemTransaction.UpdateCostPrice(itemTransactionItems, out itemZeroCostPrice);
            if (!string.IsNullOrEmpty(itemZeroCostPrice))
            {
                args.MessageText = "Zero cost price of item : " + itemZeroCostPrice;
                args.IsCancel = true;
                return false;
            }
            return true;
        }

        private bool StockMutatedStatusByPass(ValidateArgs args, int pageNo, ItemTransactionItemCollection itemTransactionItems)
        {
            //string itemZeroCostPrice;
            //if (!string.IsNullOrEmpty(itemZeroCostPrice))
            //{
            //    args.MessageText = "Zero cost price of item : " + itemZeroCostPrice;
            //    args.IsCancel = true;
            //    return false;
            //}
            return true;
        }

        private bool ApprovedStatusByPass(ValidateArgs args, int pageNo, out ItemStockOpnameApproval appr)
        {
            appr = new ItemStockOpnameApproval();
            if (appr.LoadByPrimaryKey(txtTransactionNo.Text, pageNo))
            {
                if (appr.IsApproved ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }
            return true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var so = new ItemStockOpnameApprovalCollection();
            so.Query.Where(so.Query.TransactionNo == txtTransactionNo.Text, so.Query.IsApproved == true);
            so.LoadAll();

            if (so.Count > 0)
            {
                args.MessageText = "This transaction have page that already proceed. Data cant't be delete.";
            }
            else
            {
                var entity = new ItemTransaction();
                if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
                {
                    using (esTransactionScope trans = new esTransactionScope())
                    {
                        var itemDetail = new ItemTransactionItemCollection();
                        itemDetail.Query.Where(itemDetail.Query.TransactionNo == txtTransactionNo.Text);
                        itemDetail.LoadAll();
                        itemDetail.MarkAllAsDeleted();

                        entity.Save();
                        itemDetail.Save();

                        //Commit if success, Rollback if failed
                        trans.Complete();
                    }
                    //entity.MarkAsDeleted();
                    //SaveEntity(entity);
                }
                else
                {
                    args.MessageText = AppConstant.Message.RecordNotExist;
                }
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var so = new ItemStockOpnameApprovalCollection();
            so.Query.Where(so.Query.TransactionNo == txtTransactionNo.Text, so.Query.IsApproved == true);
            so.LoadAll();

            if (so.Count > 0)
            {
                args.MessageText = "This transaction have page that already proceed. Data cant't be void.";
            }
            else
            {
                var entity = new ItemTransaction();
                if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
                {
                    using (esTransactionScope trans = new esTransactionScope())
                    {
                        var itemDetail = new ItemTransactionItemCollection();
                        itemDetail.Query.Where(itemDetail.Query.TransactionNo == txtTransactionNo.Text);
                        itemDetail.LoadAll();
                        itemDetail.MarkAllAsDeleted();

                        entity.IsVoid = true;
                        entity.VoidByUserID = AppSession.UserLogin.UserID;
                        entity.VoidDate = (new DateTime()).NowAtSqlServer();
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        entity.Save();
                        itemDetail.Save();
                        so.MarkAllAsDeleted();
                        so.Save();

                        //Commit if success, Rollback if failed
                        trans.Complete();
                    }
                }
                else
                {
                    args.MessageText = AppConstant.Message.RecordNotExist;
                }
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            ItemTransaction entity = new ItemTransaction();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "ItemTransaction";
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtTransactionNo.Text != string.Empty && cboPageNo.SelectedValue.ToInt() > 0;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            if (cboPageNo.SelectedValue.ToInt() == 0) return true; // Untuk SO w/ barcode hal yg belum di scan

            var query = new ItemStockOpnameApprovalQuery();
            string pageNo = cboPageNo.SelectedValue.ToString(); // Pakai Selected index krn waktu load pertama x text belum terisi
            query.Where(query.TransactionNo == txtTransactionNo.Text, query.PageNo == pageNo);
            query.Select(query.IsApproved);
            DataTable dtb = query.LoadDataTable();
            bool isAproved = false;
            if (dtb.Rows != null && dtb.Rows.Count > 0)
            {
                if (dtb.Rows[0][0] != DBNull.Value)
                    isAproved = (bool)dtb.Rows[0][0];
            }
            return !isAproved;
        }

        public override bool OnGetStatusMenuVoid()
        {
            var retval = (bool)ViewState["IsVoid"];

            return !retval;
            //return ViewState["IsVoid"] == null ? false : !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemItemTransactionItem(newVal);
            cboPageNo.Enabled = newVal == AppEnum.DataMode.Read;
            txtTransactionDate.Enabled = newVal == AppEnum.DataMode.New;

            grdItemTransactionItem.Columns[grdItemTransactionItem.Columns.Count - 1].Visible = (newVal == AppEnum.DataMode.Read);
            grdItemTransactionItem.Columns.FindByUniqueName("EditQty").Visible = !AppSession.Parameter.IsEnabledStockWithEdControl && (newVal == AppEnum.DataMode.Read) && (OnGetStatusMenuApproval() ?? false);
            grdItemTransactionItem.Columns.FindByUniqueName("EditQtyEd").Visible = AppSession.Parameter.IsEnabledStockWithEdControl && (newVal == AppEnum.DataMode.Read) && (OnGetStatusMenuApproval() ?? false);
        }
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            if (!(OnGetStatusMenuApproval() ?? true))
            {
                args.IsCancel = true;
                args.MessageText = AppConstant.Message.RecordHasApproved;
                return;
            }
            if (cboPageNo.SelectedValue.ToInt() == 0)
            {
                args.IsCancel = true;
                args.MessageText = "This page cannot edit using batch edit mode, please edit using barcode scan mode";
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
            var itemTransaction = (ItemTransaction)entity;

            PopulatePageNo(itemTransaction.TransactionNo);

            txtTransactionNo.Text = itemTransaction.TransactionNo;
            txtTransactionDate.SelectedDate = itemTransaction.TransactionDate;
            var serviceUnit = new ServiceUnit();
            serviceUnit.LoadByPrimaryKey(itemTransaction.FromServiceUnitID);
            txtServiceUnitName.Text = serviceUnit.ServiceUnitName;
            var location = new Location();
            if (!string.IsNullOrEmpty(itemTransaction.FromLocationID))
            {
                location.LoadByPrimaryKey(itemTransaction.FromLocationID);
                txtLocationID.Text = location.LocationID;
                txtLocationName.Text = location.LocationName;
            }
            else
            {
                if (location.LoadByPrimaryKey(serviceUnit.GetMainLocationId(itemTransaction.FromServiceUnitID)))
                {
                    txtLocationID.Text = location.LocationID;
                    txtLocationName.Text = location.LocationName;
                }
            }

            var std = new AppStandardReferenceItem();
            std.LoadByPrimaryKey(AppEnum.StandardReference.ItemType.ToString(), itemTransaction.SRItemType);
            txtSRItemType.Text = std.ItemID;
            txtSRItemTypeName.Text = std.ItemName;
            txtNotes.Text = itemTransaction.Notes;
            ViewState["IsVoid"] = itemTransaction.IsVoid ?? false;

            //Display Data Detail
            PopulateItemTransactionItemGrid();
        }

        //protected override void OnLoadComplete(EventArgs e)
        //{
        //    base.OnLoadComplete(e);

        //    ToolBarMenuDelete.Enabled = OnGetStatusMenuApproval();
        //}

        //private void PopulatePageNo(string transactionNo)
        //{
        //    cboPageNo.Items.Clear();
        //    if (string.IsNullOrEmpty(transactionNo)) return;

        //    var item = new ItemTransactionItem();
        //    var qrItem = new ItemTransactionItemQuery();
        //    qrItem.es.Top = 1;
        //    qrItem.Where(qrItem.TransactionNo == transactionNo, qrItem.PageNo == 0, qrItem.SequenceNo.Like("A%"));

        //    if (item.Load(qrItem) && !string.IsNullOrEmpty(item.TransactionNo))
        //        cboPageNo.Items.Add(new RadComboBoxItem("Not Scanned", "0"));

        //    //Page Count
        //    var query = new ItemStockOpnameApprovalQuery("a");

        //    query.Select
        //        (
        //            query.PageNo.Max().As("PageCount")
        //        );
        //    query.Where(query.TransactionNo == transactionNo);
        //    DataTable dtb = query.LoadDataTable();
        //    if (dtb.Rows != null && dtb.Rows.Count > 0)
        //    {
        //        if (dtb.Rows[0][0] != DBNull.Value)
        //        {
        //            int pageCount = Convert.ToInt32(dtb.Rows[0][0]);
        //            for (int i = 0; i < pageCount; i++)
        //            {
        //                cboPageNo.Items.Add(new RadComboBoxItem((i + 1).ToString(), (i + 1).ToString()));
        //            }
        //        }
        //    }
        //    else
        //    {
        //        cboPageNo.Items.Add(new RadComboBoxItem("1", "1"));
        //    }

        //    //Set to prev Page No
        //    if (ViewState["PageNo" + Request.UserHostName] != null && ViewState["TransactionNo" + Request.UserHostName] != null && ViewState["TransactionNo" + Request.UserHostName].Equals(txtTransactionNo.Text))
        //        ComboBox.SelectedValue(cboPageNo, ViewState["PageNo" + Request.UserHostName].ToString());
        //    else
        //    {
        //        ComboBox.SelectedValue(cboPageNo, cboPageNo.Items[0].Value);
        //    }
        //}

        private void PopulatePageNo(string transactionNo)
        {
            cboPageNo.Items.Clear();
            if (string.IsNullOrEmpty(transactionNo)) return;

            var query = new ItemStockOpnameApprovalQuery("a");
            query.Select(query.PageNo);
            query.Where(query.TransactionNo == transactionNo);
            query.OrderBy(query.PageNo.Ascending);
            var dtb = query.LoadDataTable();
            if (dtb.Rows != null && dtb.Rows.Count > 0)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    var pageNo = row["PageNo"].ToString();
                    if (pageNo.Equals("0"))
                        cboPageNo.Items.Add(new RadComboBoxItem("Not Scanned", "0"));
                    else
                        cboPageNo.Items.Add(new RadComboBoxItem(pageNo, pageNo));
                }
            }
            else
            {
                cboPageNo.Items.Add(new RadComboBoxItem("1", "1"));
            }

            //Set to prev Page No
            if (ViewState["PageNo" + Request.UserHostName] != null && ViewState["TransactionNo" + Request.UserHostName] != null && ViewState["TransactionNo" + Request.UserHostName].Equals(txtTransactionNo.Text))
                ComboBox.SelectedValue(cboPageNo, ViewState["PageNo" + Request.UserHostName].ToString());
            else
            {
                ComboBox.SelectedValue(cboPageNo, cboPageNo.Items[0].Value);
            }
        }

        #endregion

        #region Private Method Standard

        private void SaveEntity(ItemTransaction entity)
        {
            entity.Notes = txtNotes.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            // Data display on grid
            DataTable dtbOnGrid = DataGridDataTable;

            // Get Detail collection
            var itemDetails = GetDataGridCollection(Convert.ToInt32(cboPageNo.Text), txtTransactionNo.Text);
            using (var trans = new esTransactionScope())
            {
                Int32 i = 0;
                foreach (GridDataItem dataItem in grdItemTransactionItem.MasterTableView.Items)
                {
                    //Update Item Detail
                    ItemTransactionItem detail = itemDetails[i];

                    try
                    {
                        if (AppSession.Parameter.IsEnabledStockWithEdControl)
                            detail.Quantity = (decimal)((RadNumericTextBox)dataItem.FindControl("txtQuantityEd")).Value;
                        else
                            detail.Quantity = (decimal)((RadNumericTextBox)dataItem.FindControl("txtQuantity")).Value;
                    }
                    catch (Exception ex)
                    {
                        detail.Quantity = 0;
                    }

                    //Update Display data
                    dtbOnGrid.Rows[i]["Quantity"] = detail.Quantity;
                    dtbOnGrid.Rows[i]["Note"] = detail.Note;

                    var item = new Item();
                    item.LoadByPrimaryKey(detail.ItemID);
                    if (item.SRItemType == BusinessObject.Reference.ItemType.Medical)
                    {
                        var ipm = new ItemProductMedic();
                        detail.CostPrice = ipm.LoadByPrimaryKey(detail.ItemID) ? ipm.CostPrice : 0;
                    }
                    else if (item.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
                    {
                        var ipm = new ItemProductNonMedic();
                        detail.CostPrice = ipm.LoadByPrimaryKey(detail.ItemID) ? ipm.CostPrice : 0;
                    }
                    else
                    {
                        var ipm = new ItemKitchen();
                        detail.CostPrice = ipm.LoadByPrimaryKey(detail.ItemID) ? ipm.CostPrice : 0;
                    }

                    detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    detail.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    // Update ItemStockOpnamePrevBalance
                    var prevBalance = new ItemStockOpnamePrevBalance();
                    if (!prevBalance.LoadByPrimaryKey(txtTransactionNo.Text, detail.ItemID))
                    {
                        prevBalance.AddNew();
                        prevBalance.TransactionNo = txtTransactionNo.Text;
                        prevBalance.ItemID = detail.ItemID;
                    }

                    // Cost Price
                    decimal costPrice = 0;
                    if (entity.SRItemType == BusinessObject.Reference.ItemType.Medical)
                    {
                        var itemMedic = new ItemProductMedic();
                        if (itemMedic.LoadByPrimaryKey(detail.ItemID))
                        {
                            costPrice = itemMedic.CostPrice ?? 0;
                        }
                    }
                    else if (entity.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
                    {
                        var itemNonMedic = new ItemProductNonMedic();
                        if (itemNonMedic.LoadByPrimaryKey(detail.ItemID))
                        {
                            costPrice = itemNonMedic.CostPrice ?? 0;
                        }
                    }
                    else
                    {
                        var itemKitchen = new ItemKitchen();
                        if (itemKitchen.LoadByPrimaryKey(detail.ItemID))
                        {
                            costPrice = itemKitchen.CostPrice ?? 0;
                        }
                    }

                    prevBalance.CostPrice = costPrice;

                    // Ambil ulang info Balance for live count (Remark by Handono 202002)
                    //prevBalance.Quantity = Convert.ToDecimal(detail.GetColumn("PrevQty"));
                    var ib = new ItemBalance();
                    if (ib.LoadByPrimaryKey(entity.FromLocationID, detail.ItemID))
                    {
                        prevBalance.Quantity = ib.Balance;
                    }

                    prevBalance.SRItemUnit = detail.SRItemUnit;
                    prevBalance.Save();


                    DateTime expiredDate;
                    string batchNo;

                    if (detail.str.ExpiredDate == string.Empty)
                    {
                        expiredDate = Convert.ToDateTime("1/1/2999");
                        batchNo = "-N/A-";
                    }
                    else
                    {
                        expiredDate = detail.ExpiredDate ?? Convert.ToDateTime("1/1/2999");
                        batchNo = detail.BatchNumber;
                    }

                    // Update ItemStockOpnamePrevBalance
                    var prevBalanceEd = new ItemStockOpnamePrevBalanceEd();
                    if (!prevBalanceEd.LoadByPrimaryKey(txtTransactionNo.Text, detail.SequenceNo))
                    {
                        prevBalanceEd.AddNew();
                        prevBalanceEd.TransactionNo = txtTransactionNo.Text;
                        prevBalanceEd.SequenceNo = detail.SequenceNo;
                        prevBalanceEd.ItemID = detail.ItemID;
                        prevBalanceEd.ExpiredDate = expiredDate;
                        prevBalanceEd.BatchNumber = batchNo;
                    }

                    var ibed = new ItemBalanceDetailEd();
                    if (ibed.LoadByPrimaryKey(entity.FromLocationID, detail.ItemID, expiredDate, batchNo))
                        prevBalanceEd.Quantity = ibed.Balance;
                    else
                        prevBalanceEd.Quantity = 0;

                    prevBalanceEd.SRItemUnit = detail.SRItemUnit;
                    prevBalanceEd.CostPrice = costPrice;
                    prevBalanceEd.Save();

                    //Counter
                    i++;
                }

                entity.Save();
                itemDetails.Save();

                //Commit if success, Rollback if failed
                trans.Complete();

                DataGridDataTable = dtbOnGrid;
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
                que.Where(que.TransactionNo > txtTransactionNo.Text);
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < txtTransactionNo.Text);
                que.OrderBy(que.TransactionNo.Descending);
            }

            ItemTransaction entity = new ItemTransaction();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function ItemTransactionItem

        private void RefreshCommandItemItemTransactionItem(AppEnum.DataMode newVal)
        {
            grdItemTransactionItem.Rebind();
        }

        private DataTable GetDataGridDataTable(int pageNo, string transactionNo)
        {
            esDynamicQuery query = GetQueryDetail(pageNo, transactionNo);
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        private ItemTransactionItemCollection GetDataGridCollection(int pageNo, string transactionNo)
        {
            var coll = new ItemTransactionItemCollection();
            var query = (ItemTransactionItemQuery)GetQueryDetail(pageNo, transactionNo);
            query.Select(query);
            coll.Load(query);

            return coll;
        }

        private esDynamicQuery GetQueryDetail(int pageNo, string transactionNo)
        {
            var query = new ItemTransactionItemQuery("a");
            var item = new ItemQuery("b");
            var prevBal = new ItemStockOpnamePrevBalanceQuery("c");
            var medic = new ItemProductMedicQuery("d");
            var nonmedic = new ItemProductNonMedicQuery("e");
            var std = new AppStandardReferenceItemQuery("f");
            var ib = new ItemBalanceQuery("g");
            var kitchen = new ItemKitchenQuery("e");

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    query.SRItemUnit,
                    query.Quantity,
                    //item.ItemName.As("refToItem_ItemName"),
                    item.ItemName.As("ItemName"),
                    //prevBal.Quantity.As("PrevQty"),
                    @"<CASE WHEN ISNULL(a.IsAccEd, 0) = 0 THEN ISNULL(c.Quantity, 0) ELSE 0 END AS 'PrevQty'>",
                    std.ItemName.As("ItemBinName"),
                    query.Note,
                    query.BatchNumber,
                    query.ExpiredDate
                );
            query.InnerJoin(item).On(query.ItemID == item.ItemID);

            var trans = new ItemTransaction();
            trans.LoadByPrimaryKey(transactionNo);
            query.InnerJoin(ib).On(ib.LocationID == trans.FromLocationID && ib.ItemID == item.ItemID);
            query.LeftJoin(std).On(ib.SRItemBin == std.ItemID && std.StandardReferenceID == AppEnum.StandardReference.ItemBin);

            if (trans.SRItemType == BusinessObject.Reference.ItemType.Medical)
            {
                query.Select(medic.IsInventoryItem, medic.IsControlExpired);
                query.InnerJoin(medic).On(item.ItemID == medic.ItemID);
            }
            else if (trans.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
            {
                query.Select(nonmedic.IsInventoryItem, nonmedic.IsControlExpired);
                query.InnerJoin(nonmedic).On(item.ItemID == nonmedic.ItemID);
            }
            else if (trans.SRItemType == BusinessObject.Reference.ItemType.Kitchen)
            {
                query.Select(kitchen.IsInventoryItem, kitchen.IsControlExpired);
                query.InnerJoin(kitchen).On(item.ItemID == kitchen.ItemID);
            }

            query.LeftJoin(prevBal).On(query.TransactionNo == prevBal.TransactionNo & query.ItemID == prevBal.ItemID);
            query.Where(query.TransactionNo == transactionNo, query.PageNo == pageNo);

            query.OrderBy(ib.SRItemBin.Ascending, item.ItemName.Ascending, query.SequenceNo.Ascending);

            return query;
        }

        private DataTable DataGridDataTable
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["StockOpnameItems" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((DataTable)(obj));
                    }
                }

                DataTable dtb = GetDataGridDataTable(cboPageNo.SelectedValue.ToString().ToInt(), txtTransactionNo.Text);

                Session["StockOpnameItems" + Request.UserHostName] = dtb;
                return dtb;

            }
            set { Session["StockOpnameItems" + Request.UserHostName] = value; }
        }

        private void PopulateItemTransactionItemGrid()
        {
            //Display Data Detail
            DataGridDataTable = null; //Reset Record Detail
            grdItemTransactionItem.Rebind();
            grdItemTransactionItem.MasterTableView.IsItemInserted = false;
            grdItemTransactionItem.MasterTableView.ClearEditItems();
        }

        protected void grdItemTransactionItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemTransactionItem.DataSource = DataGridDataTable;
            RefreshMenuStatus();

            grdItemTransactionItem.Columns.FindByUniqueName("EditQty").Visible = !AppSession.Parameter.IsEnabledStockWithEdControl && (DataModeCurrent == AppEnum.DataMode.Read) && (OnGetStatusMenuApproval() ?? false);
            grdItemTransactionItem.Columns.FindByUniqueName("EditQtyEd").Visible = AppSession.Parameter.IsEnabledStockWithEdControl && (DataModeCurrent == AppEnum.DataMode.Read) && (OnGetStatusMenuApproval() ?? false);
        }

        #endregion

        private static bool SetApproval(ItemTransaction entity, ItemTransactionItemCollection itemTransactionItems, ItemStockOpnameApproval stockOpnameApproval, int pageNo)
        {
            //stock calculation
            using (var trans = new esTransactionScope())
            {
                // stock calculation
                var chargesBalances = new ItemBalanceCollection();
                var chargesitiBalances = new ItemBalanceDetailCollection();
                var chargesMovements = new ItemMovementCollection();
                var chargesBalanceDetailEds = new ItemBalanceDetailEdCollection();
                var itemStockOpnamePrevBalances = new ItemStockOpnamePrevBalanceCollection();
                var itemStockOpnamePrevBalanceEds = new ItemStockOpnamePrevBalanceEdCollection();

                if (!AppSession.Parameter.IsEnabledStockWithEdControl)
                    ItemBalance.PrepareItemBalancesForStockOpname(itemTransactionItems, entity.FromServiceUnitID, entity.FromLocationID,
                        AppSession.UserLogin.UserID, ref chargesBalances, ref chargesitiBalances, ref chargesMovements, ref chargesBalanceDetailEds, ref itemStockOpnamePrevBalances,
                        ref itemStockOpnamePrevBalanceEds);
                else
                    ItemBalance.PrepareItemBalancesForStockOpnameWithEdControl(itemTransactionItems, entity.FromServiceUnitID, entity.FromLocationID,
                        AppSession.UserLogin.UserID, ref chargesBalances, ref chargesitiBalances, ref chargesMovements, ref chargesBalanceDetailEds, ref itemStockOpnamePrevBalances,
                        ref itemStockOpnamePrevBalanceEds);

                itemTransactionItems.Save();
                itemStockOpnamePrevBalances.Save();
                if (itemStockOpnamePrevBalanceEds != null)
                    itemStockOpnamePrevBalanceEds.Save();

                if (chargesBalances != null)
                    chargesBalances.Save();
                if (chargesitiBalances != null)
                    chargesitiBalances.Save();
                if (chargesBalanceDetailEds != null)
                {
                    foreach (var ed in chargesBalanceDetailEds)
                    {
                        if (ed.ST == "U")
                            ed.ST = "";
                    }

                    chargesBalanceDetailEds.Save();
                }
                    
                if (chargesMovements != null)
                    chargesMovements.Save();

                if (String.IsNullOrEmpty(stockOpnameApproval.TransactionNo))
                {
                    stockOpnameApproval.AddNew();
                    stockOpnameApproval.TransactionNo = entity.TransactionNo;
                    stockOpnameApproval.PageNo = pageNo;
                }

                stockOpnameApproval.IsApproved = true;
                stockOpnameApproval.ApprovedByUserID = AppSession.UserLogin.UserID;
                stockOpnameApproval.ApprovedDate = DateTime.Today;
                stockOpnameApproval.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            AppParameter app = new AppParameter();
            app.LoadByPrimaryKey("acc_IsAutoJournalStockOpname");
            if (app.ParameterValue == "Yes")
            {
                /* Automatic Journal Testing Start */
                // find journalID from prev page if any
                var jID = 0;
                var jo = new JournalTransactionsCollection();
                jo.Query.Where(jo.Query.RefferenceNumber == entity.TransactionNo);
                if (jo.LoadAll())
                {
                    if (jo.Count > 0) jID = jo[0].JournalId.Value;
                }
                int? journalId = JournalTransactions.AddNewInventoryStockAdjustmentJournal(entity, AppSession.UserLogin.UserID, jID, "ST", pageNo, false);

                /* Automatic Journal Testing End */
            }
            return true;
        }

        protected void cboPageNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            DataGridDataTable = null;
            grdItemTransactionItem.Rebind();
            this.RefreshMenuStatus();
            ViewState["PageNo" + Request.UserHostName] = cboPageNo.SelectedValue;
            ViewState["TransactionNo" + Request.UserHostName] = txtTransactionNo.Text;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            if (string.IsNullOrEmpty(eventArgument))
                eventArgument = string.Empty;

            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (sourceControl is RadGrid)
            {
                if (eventArgument.Contains("rebind"))
                {
                    PopulatePageNo(txtTransactionNo.Text);
                    DataGridDataTable = null;
                    //cboPageNo.SelectedIndex = cboPageNo.Items.Count - 1;
                    if (eventArgument.Contains("_"))
                        ComboBox.SelectedValue(cboPageNo, eventArgument.Split('_')[1]);

                    grdItemTransactionItem.Rebind();
                }
            }
        }

        public static bool? IsEnabledStockWithEdControlStatus()
        {
            return AppSession.Parameter.IsEnabledStockWithEdControl;
        }
    }
}
