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

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class StockOpnameDetail : BasePageDetail
    {
        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"openWinStockOpnameAdd(); args.set_cancel(true);");
            return script;
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "StockOpnameSearch.aspx";
            UrlPageList = "StockOpnameList.aspx";

            ProgramID = AppConstant.Program.CssdStockOpname;

            WindowSearch.Height = 400;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                DataGridDataTable = null;
                grdItem.Columns.FindByUniqueName("PrevBalance").Visible = AppSession.Parameter.IsShowSystemQtyInCssdStockTacking;
                grdItem.Columns.FindByUniqueName("PrevBalanceReceived").Visible = AppSession.Parameter.IsShowSystemQtyInCssdStockTacking;
                grdItem.Columns.FindByUniqueName("PrevBalanceDeconImmersion").Visible = AppSession.Parameter.IsShowSystemQtyInCssdStockTacking;
                grdItem.Columns.FindByUniqueName("PrevBalanceDeconAbstersion").Visible = AppSession.Parameter.IsShowSystemQtyInCssdStockTacking;
                grdItem.Columns.FindByUniqueName("PrevBalanceDeconDrying").Visible = AppSession.Parameter.IsShowSystemQtyInCssdStockTacking;
                grdItem.Columns.FindByUniqueName("PrevBalanceFeasibilityTest").Visible = AppSession.Parameter.IsShowSystemQtyInCssdStockTacking;
                grdItem.Columns.FindByUniqueName("PrevBalancePackaging").Visible = AppSession.Parameter.IsShowSystemQtyInCssdStockTacking;
                grdItem.Columns.FindByUniqueName("PrevBalanceUltrasound").Visible = AppSession.Parameter.IsShowSystemQtyInCssdStockTacking;
                grdItem.Columns.FindByUniqueName("PrevBalanceSterilization").Visible = AppSession.Parameter.IsShowSystemQtyInCssdStockTacking;
                grdItem.Columns.FindByUniqueName("PrevBalanceDistribution").Visible = AppSession.Parameter.IsShowSystemQtyInCssdStockTacking && AppSession.Parameter.IsCentralizedCssd;
                grdItem.Columns.FindByUniqueName("PrevBalanceReturned").Visible = AppSession.Parameter.IsShowSystemQtyInCssdStockTacking && !AppSession.Parameter.IsCentralizedCssd;

                grdItem.Columns.FindByUniqueName("BalanceDistribution").Visible = AppSession.Parameter.IsCentralizedCssd;
                grdItem.Columns.FindByUniqueName("BalanceReturned").Visible = !AppSession.Parameter.IsCentralizedCssd;
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

            var entity = new CssdStockOpname();
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

            var pageNo = cboPageNo.SelectedValue.ToInt();
            if (pageNo > 0)
            {
                Approval(args, entity, cboPageNo.SelectedValue.ToInt());
            }
            else
            {
                try
                {
                    var apprNewColl = new CssdStockOpnameApprovalCollection();

                    using (var trans = new esTransactionScope())
                    {
                        // Pindah ke bbrp page
                        var itiq = new CssdStockOpnameItemQuery();
                        var itiColl = new CssdStockOpnameItemCollection();
                        itiq.Where(itiq.TransactionNo == txtTransactionNo.Text, itiq.PageNo == pageNo);
                        itiq.OrderBy(itiq.SequenceNo.Ascending);
                        itiColl.Load(itiq);

                        // Last Page
                        var stkApprQr = new CssdStockOpnameApprovalQuery();
                        stkApprQr.Where(stkApprQr.TransactionNo == txtTransactionNo.Text);
                        stkApprQr.OrderBy(stkApprQr.PageNo.Descending);
                        stkApprQr.es.Top = 1;

                        var stkAppr = new CssdStockOpnameApproval();
                        if (stkAppr.Load(stkApprQr))
                        {
                            pageNo = stkAppr.PageNo ?? 0;
                        }

                        var itiCollNew = new CssdStockOpnameItemCollection();
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
                            var appr = new CssdStockOpnameApproval();
                            appr.LoadByPrimaryKey(line.TransactionNo, line.PageNo ?? 0);

                            pageNo = appr.PageNo ?? 0;
                            var cssdStockOpnameItems = GetDataGridCollection(pageNo, txtTransactionNo.Text);
                            SetApproval(entity, cssdStockOpnameItems, appr, pageNo);
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

        private bool Approval(ValidateArgs args, CssdStockOpname entity, int pageNo)
        {
            CssdStockOpnameApproval appr;
            if (!ApprovedStatusByPass(args, pageNo, out appr))
                return false;

            var cssdStockOpnameItems = GetDataGridCollection(pageNo, txtTransactionNo.Text);


            return SetApproval(entity, cssdStockOpnameItems, appr, pageNo);
        }

        private bool ApprovedStatusByPass(ValidateArgs args, int pageNo, out CssdStockOpnameApproval appr)
        {
            appr = new CssdStockOpnameApproval();
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
            var so = new CssdStockOpnameApprovalCollection();
            so.Query.Where(so.Query.TransactionNo == txtTransactionNo.Text, so.Query.IsApproved == true);
            so.LoadAll();

            if (so.Count > 0)
            {
                args.MessageText = "This transaction have page that already proceed. Data cant't be delete.";
            }
            else
            {
                var entity = new CssdStockOpname();
                if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
                {
                    using (esTransactionScope trans = new esTransactionScope())
                    {
                        var itemDetail = new CssdStockOpnameItemCollection();
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
            var so = new CssdStockOpnameApprovalCollection();
            so.Query.Where(so.Query.TransactionNo == txtTransactionNo.Text, so.Query.IsApproved == true);
            so.LoadAll();

            if (so.Count > 0)
            {
                args.MessageText = "This transaction have page that already proceed. Data cant't be void.";
            }
            else
            {
                var entity = new CssdStockOpname();
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
                        entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
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
            var entity = new CssdStockOpname();
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
            auditLogFilter.TableName = "CssdStockOpname";
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtTransactionNo.Text != string.Empty && cboPageNo.SelectedValue.ToInt() > 0;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            if (cboPageNo.SelectedValue.ToInt() == 0) return true;

            var query = new CssdStockOpnameApprovalQuery();
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
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItem(newVal);
            cboPageNo.Enabled = newVal == AppEnum.DataMode.Read;
            txtTransactionDate.Enabled = newVal == AppEnum.DataMode.New;
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
            var entity = new CssdStockOpname();
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
            var so = (CssdStockOpname)entity;

            PopulatePageNo(so.TransactionNo);

            txtTransactionNo.Text = so.TransactionNo;
            txtTransactionDate.SelectedDate = so.TransactionDate;
            var serviceUnit = new ServiceUnit();
            if (serviceUnit.LoadByPrimaryKey(so.ServiceUnitID))
            {
                txtServiceUnitID.Text = so.ServiceUnitID;
                txtServiceUnitName.Text = serviceUnit.ServiceUnitName;
            }
            else
            {
                txtServiceUnitID.Text = string.Empty;
                txtServiceUnitName.Text = string.Empty;
            }
            txtNotes.Text = so.Notes;
            ViewState["IsVoid"] = so.IsVoid ?? false;

            //Display Data Detail
            PopulateItemGrid();
        }

        private void PopulatePageNo(string transactionNo)
        {
            cboPageNo.Items.Clear();
            if (string.IsNullOrEmpty(transactionNo)) return;

            var query = new CssdStockOpnameApprovalQuery("a");
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
            if (ViewState["CssdPageNo" + Request.UserHostName] != null && ViewState["CssdTransactionNo" + Request.UserHostName] != null && ViewState["CssdTransactionNo" + Request.UserHostName].Equals(txtTransactionNo.Text))
                ComboBox.SelectedValue(cboPageNo, ViewState["CssdPageNo" + Request.UserHostName].ToString());
            else
            {
                ComboBox.SelectedValue(cboPageNo, cboPageNo.Items[0].Value);
            }
        }

        #endregion

        #region Private Method Standard

        private void SaveEntity(CssdStockOpname entity)
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
                foreach (GridDataItem dataItem in grdItem.MasterTableView.Items)
                {
                    //Update Item Detail
                    CssdStockOpnameItem detail = itemDetails[i];

                    if (txtServiceUnitID.Text == AppSession.Parameter.ServiceUnitCssdID)
                    {
                        detail.Balance = (decimal)((RadNumericTextBox)dataItem.FindControl("txtBalanceSterilization")).Value;
                        detail.BalanceReceived = (decimal)((RadNumericTextBox)dataItem.FindControl("txtBalanceReceived")).Value;
                        detail.BalanceDeconImmersion = (decimal)((RadNumericTextBox)dataItem.FindControl("txtBalanceDeconImmersion")).Value;
                        detail.BalanceDeconAbstersion = (decimal)((RadNumericTextBox)dataItem.FindControl("txtBalanceDeconAbstersion")).Value;
                        detail.BalanceDeconDrying = (decimal)((RadNumericTextBox)dataItem.FindControl("txtBalanceDeconDrying")).Value;
                        detail.BalanceFeasibilityTest = (decimal)((RadNumericTextBox)dataItem.FindControl("txtBalanceFeasibilityTest")).Value;
                        detail.BalancePackaging = (decimal)((RadNumericTextBox)dataItem.FindControl("txtBalancePackaging")).Value;
                        detail.BalanceUltrasound = (decimal)((RadNumericTextBox)dataItem.FindControl("txtBalanceUltrasound")).Value;
                        detail.BalanceSterilization = (decimal)((RadNumericTextBox)dataItem.FindControl("txtBalanceSterilization")).Value;
                        detail.BalanceDistribution = (decimal)((RadNumericTextBox)dataItem.FindControl("txtBalanceDistribution")).Value;
                        detail.BalanceReturned = (decimal)((RadNumericTextBox)dataItem.FindControl("txtBalanceReturned")).Value;
                    }
                    else
                    {
                        detail.Balance = (decimal)((RadNumericTextBox)dataItem.FindControl("txtBalance")).Value;
                    }

                    detail.Notes = ((RadTextBox)dataItem.FindControl("txtNotes")).Text ?? string.Empty;

                    //Update Display data
                    dtbOnGrid.Rows[i]["Balance"] = detail.Balance;
                    dtbOnGrid.Rows[i]["BalanceReceived"] = detail.BalanceReceived;
                    dtbOnGrid.Rows[i]["BalanceDeconImmersion"] = detail.BalanceDeconImmersion;
                    dtbOnGrid.Rows[i]["BalanceDeconAbstersion"] = detail.BalanceDeconAbstersion;
                    dtbOnGrid.Rows[i]["BalanceDeconDrying"] = detail.BalanceDeconDrying;
                    dtbOnGrid.Rows[i]["BalanceFeasibilityTest"] = detail.BalanceFeasibilityTest;
                    dtbOnGrid.Rows[i]["BalancePackaging"] = detail.BalancePackaging;
                    dtbOnGrid.Rows[i]["BalanceUltrasound"] = detail.BalanceUltrasound;
                    dtbOnGrid.Rows[i]["BalanceSterilization"] = detail.BalanceSterilization;
                    dtbOnGrid.Rows[i]["BalanceDistribution"] = detail.BalanceDistribution;
                    dtbOnGrid.Rows[i]["BalanceReturned"] = detail.BalanceReturned;
                    dtbOnGrid.Rows[i]["Notes"] = detail.Notes;

                    detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    detail.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    // Ambil ulang info Balance for live count 
                    var ib = new CssdItemBalance();
                    if (ib.LoadByPrimaryKey(entity.ServiceUnitID, detail.ItemID))
                    {
                        detail.PrevBalance = ib.Balance;
                        detail.PrevBalanceReceived = ib.BalanceReceived;
                        detail.PrevBalanceDeconImmersion = ib.BalanceDeconImmersion;
                        detail.PrevBalanceDeconAbstersion = ib.BalanceDeconAbstersion;
                        detail.PrevBalanceDeconDrying = ib.BalanceDeconDrying;
                        detail.PrevBalanceFeasibilityTest = ib.BalanceFeasibilityTest;
                        detail.PrevBalancePackaging = ib.BalancePackaging;
                        detail.PrevBalanceUltrasound = ib.BalanceUltrasound;
                        detail.PrevBalanceSterilization = ib.BalanceSterilization;
                        detail.PrevBalanceDistribution = ib.BalanceDistribution;
                        detail.PrevBalanceReturned = ib.BalanceReturned;
                    }

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
            var que = new CssdStockOpnameQuery("a");
            var qusr = new AppUserServiceUnitQuery("u");
            que.InnerJoin(qusr).On(qusr.ServiceUnitID == que.ServiceUnitID && qusr.UserID == AppSession.UserLogin.UserID);

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

            var entity = new CssdStockOpname();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function ItemTransactionItem

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            grdItem.Rebind();
        }

        private DataTable GetDataGridDataTable(int pageNo, string transactionNo)
        {
            esDynamicQuery query = GetQueryDetail(pageNo, transactionNo);
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        private CssdStockOpnameItemCollection GetDataGridCollection(int pageNo, string transactionNo)
        {
            var coll = new CssdStockOpnameItemCollection();
            var query = (CssdStockOpnameItemQuery)GetQueryDetail(pageNo, transactionNo);
            query.Select(query);
            coll.Load(query);

            return coll;
        }

        private esDynamicQuery GetQueryDetail(int pageNo, string transactionNo)
        {
            var unitId = string.Empty;
            var trans = new CssdStockOpname();
            if (trans.LoadByPrimaryKey(transactionNo))
                unitId = trans.ServiceUnitID;

            var query = new CssdStockOpnameItemQuery("a");
            var hd = new CssdStockOpnameQuery("b");
            var item = new ItemQuery("c");
            var vwItem = new VwItemProductMedicNonMedicQuery("d");

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,

                    query.Balance,
                    query.BalanceReceived,
                    query.BalanceDeconImmersion,
                    query.BalanceDeconAbstersion,
                    query.BalanceDeconDrying,
                    query.BalanceFeasibilityTest,
                    query.BalancePackaging,
                    query.BalanceUltrasound,
                    query.BalanceSterilization,
                    query.BalanceDistribution,
                    query.BalanceReturned,

                    query.PrevBalance,
                    query.PrevBalanceReceived,
                    query.PrevBalanceDeconImmersion,
                    query.PrevBalanceDeconAbstersion,
                    query.PrevBalanceDeconDrying,
                    query.PrevBalanceFeasibilityTest,
                    query.PrevBalancePackaging,
                    query.PrevBalanceUltrasound,
                    query.PrevBalanceSterilization,
                    query.PrevBalanceDistribution,
                    query.PrevBalanceReturned,

                    query.Notes,

                    item.ItemName,
                    vwItem.SRItemUnit
                );
            if (unitId == AppSession.Parameter.ServiceUnitCssdID)
                query.Select(@"<CAST(1 AS BIT) AS 'IsCssdUnit'>");
            else
                query.Select(@"<CAST(0 AS BIT) AS 'IsCssdUnit'>");

            query.InnerJoin(hd).On(hd.TransactionNo == query.TransactionNo);
            query.InnerJoin(item).On(item.ItemID == query.ItemID);
            query.InnerJoin(vwItem).On(vwItem.ItemID == query.ItemID);
           
            query.Where(query.TransactionNo == transactionNo, query.PageNo == pageNo);

            query.OrderBy(item.ItemName.Ascending, query.SequenceNo.Ascending);

            return query;
        }

        private DataTable DataGridDataTable
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["CssdStockOpnameItems" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((DataTable)(obj));
                    }
                }

                DataTable dtb = GetDataGridDataTable(cboPageNo.SelectedValue.ToString().ToInt(), txtTransactionNo.Text);

                Session["CssdStockOpnameItems" + Request.UserHostName] = dtb;
                return dtb;

            }
            set { Session["CssdStockOpnameItems" + Request.UserHostName] = value; }
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            DataGridDataTable = null; //Reset Record Detail
            grdItem.Rebind();
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = DataGridDataTable;
            RefreshMenuStatus();
        }

        #endregion

        private static bool SetApproval(CssdStockOpname entity, CssdStockOpnameItemCollection cssdStockOpnameItems, CssdStockOpnameApproval stockOpnameApproval, int pageNo)
        {
            //stock calculation
            using (var trans = new esTransactionScope())
            {
                // stock calculation
                var balances = new CssdItemBalanceCollection();

                CssdItemBalance.PrepareItemBalancesForStockOpname(cssdStockOpnameItems, entity.ServiceUnitID, AppSession.UserLogin.UserID, ref balances);

                balances.Save();

                if (String.IsNullOrEmpty(stockOpnameApproval.TransactionNo))
                {
                    stockOpnameApproval.AddNew();
                    stockOpnameApproval.TransactionNo = entity.TransactionNo;
                    stockOpnameApproval.PageNo = pageNo;
                }

                stockOpnameApproval.IsApproved = true;
                stockOpnameApproval.ApprovedDateTime = DateTime.Today;
                stockOpnameApproval.ApprovedByUserID = AppSession.UserLogin.UserID;
                stockOpnameApproval.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return true;
        }

        protected void cboPageNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            DataGridDataTable = null;
            grdItem.Rebind();
            this.RefreshMenuStatus();
            ViewState["CssdPageNo" + Request.UserHostName] = cboPageNo.SelectedValue;
            ViewState["CssdTransactionNo" + Request.UserHostName] = txtTransactionNo.Text;
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

                    grdItem.Rebind();
                }
            }
        }


        public static bool? IsEnabledAddNewItemCSSD()
        {
            return AppSession.Parameter.IsEnabledAddNewItemCSSD;
        }
    }
}