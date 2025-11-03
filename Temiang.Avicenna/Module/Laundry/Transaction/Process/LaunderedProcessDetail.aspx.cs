using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaunderedProcessDetail : BasePageDetail
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
            UrlPageSearch = "LaunderedProcessSearch.aspx?type=" + getPageID;
            UrlPageList = "LaunderedProcessList.aspx?type=" + getPageID;

            ProgramID = (getPageID == "" || getPageID == "n") ? AppConstant.Program.LaundererProcess : (getPageID == "i" ? AppConstant.Program.LaundererProcessInfectious : AppConstant.Program.LaundererProcessRewashing);

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                if (getPageID == "")
                {
                    if (!AppSession.Parameter.IsCentralizedLaundrie)
                    {
                        tabStrip.Tabs[0].Visible = true;
                        tabStrip.Tabs[1].Visible = false;
                        tabStrip.Tabs[2].Visible = false;
                        tabStrip.Tabs[3].Visible = false;
                        multiPage.SelectedIndex = 0;
                    }
                    else
                    {
                        tabStrip.Tabs[0].Visible = false;
                        tabStrip.Tabs[1].Visible = true;
                        tabStrip.Tabs[2].Visible = false;
                        tabStrip.Tabs[3].Visible = false;
                        multiPage.SelectedIndex = 1;
                    }
                }
                else if (getPageID == "n")
                {
                    tabStrip.Tabs[0].Visible = false;
                    tabStrip.Tabs[1].Visible = false;
                    tabStrip.Tabs[2].Visible = true;
                    tabStrip.Tabs[3].Visible = false;
                    multiPage.SelectedIndex = 2;
                    tabStrip.Tabs[1].Text = "Non Infectious";
                }
                else if (getPageID == "i")
                {
                    tabStrip.Tabs[0].Visible = false;
                    tabStrip.Tabs[1].Visible = false;
                    tabStrip.Tabs[2].Visible = true;
                    tabStrip.Tabs[3].Visible = false;
                    multiPage.SelectedIndex = 2;
                }
                else 
                {
                    tabStrip.Tabs[0].Visible = false;
                    tabStrip.Tabs[1].Visible = false;
                    tabStrip.Tabs[2].Visible = false;
                    tabStrip.Tabs[3].Visible = true;
                    multiPage.SelectedIndex = 3;
                }
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            //ToolBarMenuSearch.Visible = false;
            //ToolBarMenuAdd.Visible = false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboMachineID, cboMachineID);
            ajax.AddAjaxSetting(cboMachineID, cboSRLaundryProgram);
            ajax.AddAjaxSetting(cboMachineID, cboSRLaundryType);
            ajax.AddAjaxSetting(cboMachineID, txtMachineVolume);
            ajax.AddAjaxSetting(cboMachineID, txtMachineNotes);
            ajax.AddAjaxSetting(cboMachineID, grdItemConsumption);

            ajax.AddAjaxSetting(cboSRLaundryProgram, cboSRLaundryType);
            ajax.AddAjaxSetting(cboSRLaundryProgram, grdItemConsumption);

            ajax.AddAjaxSetting(cboSRLaundryType, grdItemConsumption);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new LaunderedProcess());

            txtProcessNo.Text = GetNewProcessNo();
            txtProcessDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtProcessTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

            btnGetPickList.Enabled = true;
            btnGetPickListCentralization.Enabled = true;
            btnGetInfectiousPickList.Enabled = true;
            btnGetRewashingPickList.Enabled = true;
            btnResetItem.Enabled = true;

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //var entity = new LaunderedProcess();
            //if (entity.LoadByPrimaryKey(txtProcessNo.Text))
            //{
            //    entity.MarkAsDeleted();

            //    SaveEntity(entity);
            //}
            //else
            //    args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (getPageID == "")
            {
                if (!AppSession.Parameter.IsCentralizedLaundrie)
                {
                    if (LaunderedProcessItems.Count == 0)
                    {
                        args.MessageText = AppConstant.Message.RecordDetailEmpty;
                        args.IsCancel = true;
                        return;
                    }
                }
                else
                {
                    if (LaunderedProcessItemCentralizations.Count == 0)
                    {
                        args.MessageText = AppConstant.Message.RecordDetailEmpty;
                        args.IsCancel = true;
                        return;
                    }
                }
            }
            else if (getPageID == "i" || getPageID == "n")
            {
                if (LaunderedProcessItemInfectiouss.Count == 0)
                {
                    args.MessageText = AppConstant.Message.RecordDetailEmpty;
                    args.IsCancel = true;
                    return;
                }
            }
            else 
            {
                if (LaunderedProcessItemRewashings.Count == 0)
                {
                    args.MessageText = AppConstant.Message.RecordDetailEmpty;
                    args.IsCancel = true;
                    return;
                }
            }

            if (string.IsNullOrEmpty(cboSRLaundryType.SelectedValue))
            {
                args.MessageText = "Laundry Type required.";
                args.IsCancel = true;
                return;
            }

            var entity = new LaunderedProcess();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboSRLaundryType.SelectedValue))
            {
                args.MessageText = "Laundry Type required.";
                args.IsCancel = true;
                return;
            }

            var entity = new LaunderedProcess();
            if (entity.LoadByPrimaryKey(txtProcessNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
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
            auditLogFilter.PrimaryKeyData = string.Format("ProcessNo='{0}'", txtProcessNo.Text.Trim());
            auditLogFilter.TableName = "LaunderedProcess";
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new LaunderedProcess();
            if (entity.LoadByPrimaryKey(txtProcessNo.Text))
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

        private bool IsApprovedOrVoid(LaunderedProcess entity, ValidateArgs args)
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

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_ProcessNo", txtProcessNo.Text);
            printJobParameters.AddNew("p_UserID", AppSession.UserLogin.UserID);
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return ViewState["IsApproved"] == null ? false : !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItem(newVal);
            RefreshCommandItemCentralized(newVal);
            RefreshCommandItemInfectious(newVal);
            RefreshCommandItemRewashing(newVal);
            RefreshCommandItemConsumption(newVal);

            btnGetPickList.Visible = (getPageID == "" && !AppSession.Parameter.IsCentralizedLaundrie);
            btnGetPickList.Enabled = (newVal != AppEnum.DataMode.Read && getPageID == "" && !AppSession.Parameter.IsCentralizedLaundrie);

            btnGetPickListCentralization.Visible = (getPageID == "" && AppSession.Parameter.IsCentralizedLaundrie);
            btnGetPickListCentralization.Enabled = (newVal != AppEnum.DataMode.Read && getPageID == "" && AppSession.Parameter.IsCentralizedLaundrie);

            btnGetInfectiousPickList.Visible = (getPageID == "i" || getPageID == "n");
            btnGetInfectiousPickList.Enabled = newVal != AppEnum.DataMode.Read && (getPageID == "i" || getPageID == "n");

            btnGetRewashingPickList.Visible = getPageID == "r";
            btnGetRewashingPickList.Enabled = newVal != AppEnum.DataMode.Read && getPageID == "r";
            
            btnResetItem.Enabled = newVal != AppEnum.DataMode.Read;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new LaunderedProcess();
            if (parameters.Length > 0)
            {
                var receivedNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(receivedNo);
            }
            else
                entity.LoadByPrimaryKey(txtProcessNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var process = (LaunderedProcess)entity;

            txtProcessNo.Text = process.ProcessNo;
            txtProcessDate.SelectedDate = process.ProcessDate;
            txtProcessTime.Text = process.ProcessTime;
            if (!string.IsNullOrEmpty(process.MachineID))
            {
                var machine = new LaundryWashingMachineQuery();
                machine.Where(machine.MachineID == process.MachineID);
                cboMachineID.DataSource = machine.LoadDataTable();
                cboMachineID.DataBind();

                cboMachineID.SelectedValue = process.MachineID;
            }
            else
            {
                cboMachineID.Items.Clear();
                cboMachineID.Text = string.Empty;
                cboMachineID.SelectedValue = string.Empty;
            }
            GetMachineInfo(cboMachineID.SelectedValue, false);

            if (!string.IsNullOrEmpty(process.SRLaundryProgram))
            {
                var p = new AppStandardReferenceItemQuery();
                p.Where(p.StandardReferenceID == "LaundryProgram", p.ItemID == process.SRLaundryProgram);
                cboSRLaundryProgram.DataSource = p.LoadDataTable();
                cboSRLaundryProgram.DataBind();

                cboSRLaundryProgram.SelectedValue = process.SRLaundryProgram;
            }
            else
            {
                cboSRLaundryProgram.Items.Clear();
                cboSRLaundryProgram.Text = string.Empty;
                cboSRLaundryProgram.SelectedValue = string.Empty;
            }

            if (!string.IsNullOrEmpty(process.SRLaundryType))
            {
                var p = new AppStandardReferenceItemQuery();
                p.Where(p.StandardReferenceID == "LaundryType", p.ItemID == process.SRLaundryType);
                cboSRLaundryType.DataSource = p.LoadDataTable();
                cboSRLaundryType.DataBind();

                cboSRLaundryType.SelectedValue = process.SRLaundryType;
            }
            else
            {
                cboSRLaundryType.Items.Clear();
                cboSRLaundryType.Text = string.Empty;
                cboSRLaundryType.SelectedValue = string.Empty;
            }

            btnGetPickList.Enabled = false;
            btnGetPickListCentralization.Enabled = false;
            btnGetInfectiousPickList.Enabled = false;
            btnGetRewashingPickList.Enabled = false;
            btnResetItem.Enabled = false;

            PopulateItemGrid();
            PopulateItemCentralizedGrid();
            PopulateItemInfectiousGrid();
            PopulateItemRewashingGrid();
            PopulateItemConsumptionGrid();

            ViewState["IsApproved"] = process.IsApproved ?? false;
            ViewState["IsVoid"] = process.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new LaunderedProcess();
            if (!entity.LoadByPrimaryKey(txtProcessNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, true, args);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new LaunderedProcess();
            if (!entity.LoadByPrimaryKey(txtProcessNo.Text))
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

            if (entity.IsApproved == false)
            {
                args.MessageText = AppConstant.Message.RecordHasNotApproved;
                args.IsCancel = true;
                return;
            }

            if (getPageID == "")
            {
                DataTable dtb = (new LaundryReturnedItemCollection()).GetItemReturned(txtProcessNo.Text);
                if (dtb.Rows.Count > 0)
                {
                    args.MessageText = "Data has been processed returns.";
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                //DataTable dtb = (new LaundryReturnedItemInfectiousCollection()).GetItemReturned(txtProcessNo.Text);
                //if (dtb.Rows.Count > 0)
                //{
                //    args.MessageText = "Data has been processed.";
                //    args.IsCancel = true;
                //    return;
                //}
            }

            if (LaunderedProcessItemConsumptions.Count == 0)
            {
                SetApproval(entity, false, args);
            }
            else
            {
                args.MessageText = "Data has been processed stock for consumed items.";
                args.IsCancel = true;
                return;
            }
        }

        private void SetApproval(LaunderedProcess entity, bool isApproval, ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                entity.IsApproved = isApproval;
                if (isApproval)
                {
                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                    entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                }
                else
                {
                    entity.ApprovedByUserID = null;
                    entity.ApprovedDateTime = null;
                }

                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.Save();


                if (isApproval)
                {
                    #region stock calculation
                    var chargesBalances = new ItemBalanceCollection();
                    var chargesDetailBalances = new ItemBalanceDetailCollection();
                    var chargesDetailBalanceEds = new ItemBalanceDetailEdCollection();
                    var chargesMovements = new ItemMovementCollection();

                    string itemNoStock;
                    var itemConsumptions = LaunderedProcessItemConsumptions;

                    var serviceUnitId = AppSession.Parameter.ServiceUnitLaundryID;
                    var locationId = ServiceUnit.MainLocationID(serviceUnitId);

                    ItemBalance.PrepareItemBalancesForLaundryWashingProcess(entity, itemConsumptions, serviceUnitId, locationId, AppSession.UserLogin.UserID,
                       ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, ref chargesDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl, out itemNoStock);

                    if (!string.IsNullOrEmpty(itemNoStock))
                    {
                        if (itemNoStock == "x")
                        {
                            var y = false;
                            var itemNoBalance = string.Empty;
                            foreach (var i in LaunderedProcessItemConsumptions)
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
                                args.MessageText = "There is no balance setting in Laundry unit for " + itemNoBalance;
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
                    if (chargesDetailBalanceEds != null)
                        chargesDetailBalanceEds.Save();
                    if (chargesMovements != null)
                        chargesMovements.Save();

                    if (getPageID == "" && AppSession.Parameter.IsCentralizedLaundrie)
                    {
                        var balancesFrom = new LaundryItemBalanceCollection();
                        var balancesTo = new LaundryItemBalanceCollection();
                        var movements = new LaundryItemMovementCollection();

                        if (isApproval)
                            LaundryItemBalance.PrepareBalancesForProcess(LaunderedProcessItemCentralizations, AppSession.Parameter.ServiceUnitLaundryID, AppSession.Parameter.ServiceUnitLaundryID, AppSession.UserLogin.UserID,
                                ref balancesFrom, ref balancesTo, ref movements);
                        else
                            LaundryItemBalance.PrepareBalancesForProcessReverse(LaunderedProcessItemCentralizations, AppSession.Parameter.ServiceUnitLaundryID, AppSession.Parameter.ServiceUnitLaundryID, AppSession.UserLogin.UserID,
                                ref balancesFrom, ref balancesTo, ref movements);

                        if (balancesFrom != null)
                            balancesFrom.Save();
                        if (balancesTo != null)
                            balancesTo.Save();
                        if (movements != null)
                            movements.Save();
                    }

                    #endregion
                }

                trans.Complete();
            }

            if (isApproval)
            {
                #region journal
                var app = new AppParameter();
                app.LoadByPrimaryKey("acc_IsAutoJournalInvIssue");
                if (app.ParameterValue == "Yes")
                {
                    var cons = (LaunderedProcessItemConsumptions.Where(b => b.Qty > 0 && b.IsInventoryItem == true));
                    if (cons.Count() > 0)
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

                        int? journalId = JournalTransactions.AddNewInventoryIssueFromLaundryWashingProcessJournal(entity, AppSession.Parameter.ServiceUnitLaundryID, AppSession.UserLogin.UserID, 0);

                        /* Automatic Journal Testing End */
                    }
                }
                #endregion
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new LaunderedProcess();
            if (!entity.LoadByPrimaryKey(txtProcessNo.Text))
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

            SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            var entity = new LaunderedProcess();
            if (!entity.LoadByPrimaryKey(txtProcessNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(LaunderedProcess entity, bool isVoid)
        {
            using (var trans = new esTransactionScope())
            {
                //header
                entity.IsVoid = isVoid;
                if (isVoid)
                {
                    entity.VoidByUserID = AppSession.UserLogin.UserID;
                    entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
                }
                else
                {
                    entity.VoidByUserID = null;
                    entity.VoidDateTime = null;
                }
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                entity.Save();

                trans.Complete();
            }
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(LaunderedProcess entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtProcessNo.Text = GetNewProcessNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.ProcessNo = txtProcessNo.Text;
            entity.ProcessDate = txtProcessDate.SelectedDate;
            entity.ProcessTime = txtProcessTime.TextWithLiterals;
            entity.SRLaundryProcessType = getPageID == "" ? "01" : (getPageID == "n" ? "01" : (getPageID == "i" ? "02" : "03"));
            entity.SRLaundryProgram = cboSRLaundryProgram.SelectedValue;
            entity.SRLaundryType = cboSRLaundryType.SelectedValue;
            entity.MachineID = cboMachineID.SelectedValue;
            entity.Notes = string.Empty;
            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in LaunderedProcessItems)
            {
                item.ProcessNo = txtProcessNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in LaunderedProcessItemCentralizations)
            {
                item.ProcessNo = txtProcessNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in LaunderedProcessItemInfectiouss)
            {
                item.ProcessNo = txtProcessNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in LaunderedProcessItemRewashings)
            {
                item.ProcessNo = txtProcessNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in LaunderedProcessItemConsumptions)
            {
                item.ProcessNo = txtProcessNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(LaunderedProcess entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                LaunderedProcessItems.Save();
                LaunderedProcessItemCentralizations.Save();
                LaunderedProcessItemInfectiouss.Save();
                LaunderedProcessItemRewashings.Save();
                LaunderedProcessItemConsumptions.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new LaunderedProcessQuery();
            que.es.Top = 1; // SELECT TOP 1 ..

            if (isNextRecord)
            {
                que.Where
                    (
                        que.ProcessNo > txtProcessNo.Text
                    );
                que.OrderBy(que.ProcessNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.ProcessNo < txtProcessNo.Text
                    );
                que.OrderBy(que.ProcessNo.Descending);
            }
            if (getPageID == "")
                que.Where(que.SRLaundryProcessType == "01");
            else if (getPageID == "n")
                que.Where(que.SRLaundryProcessType == "01");
            else if (getPageID == "i")
                que.Where(que.SRLaundryProcessType == "02");
            else 
                que.Where(que.SRLaundryProcessType == "03");

            var entity = new LaunderedProcess();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged
        #endregion

        #region Record Detail Method Function of LaunderedProcessItems

        private LaunderedProcessItemCollection LaunderedProcessItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLaunderedProcessItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((LaunderedProcessItemCollection)(obj));
                    }
                }

                var coll = new LaunderedProcessItemCollection();
                var query = new LaunderedProcessItemQuery("a");
                var received = new LaundryReceivedItemQuery("b");
                var iq = new ItemQuery("c");
                var unitq = new AppStandardReferenceItemQuery("d");

                query.Select
                    (
                        query,

                        received.ItemID.As("refToLaundryReceivedItem_ItemID"),
                        iq.ItemName.As("refToItem_ItemName"),

                        unitq.ItemName.As("refToAppStandardReferenceItem_ItemUnit"),
                        received.Notes.As("refToLaundryReceivedItem_Notes")

                    );
                query.InnerJoin(received).On(received.ReceivedNo == query.ReceivedNo &&
                                         received.ReceivedSeqNo == query.ReceivedSeqNo);
                query.InnerJoin(iq).On(iq.ItemID == received.ItemID);
                query.InnerJoin(unitq).On(unitq.ItemID == received.SRItemUnit &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.ProcessNo == txtProcessNo.Text);
                query.OrderBy(query.ProcessSeqNo.Ascending);
                coll.Load(query);

                Session["collLaunderedProcessItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collLaunderedProcessItem" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            LaunderedProcessItems = null; //Reset Record Detail
            grdItem.DataSource = LaunderedProcessItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private LaunderedProcessItem FindItem(String seqNo)
        {
            LaunderedProcessItemCollection coll = LaunderedProcessItems;
            LaunderedProcessItem retEntity = null;
            foreach (LaunderedProcessItem rec in coll)
            {
                if (rec.ProcessSeqNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = LaunderedProcessItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String seqNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][LaunderedProcessItemMetadata.ColumnNames.ProcessSeqNo]);
            LaunderedProcessItem entity = FindItem(seqNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LaunderedProcessItemMetadata.ColumnNames.ProcessSeqNo]);
            LaunderedProcessItem entity = FindItem(seqNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        private void SetEntityValue(LaunderedProcessItem entity, GridCommandEventArgs e)
        {
            var userControl = (LaunderedProcessDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.Qty = userControl.Qty;
            }
        }

        #endregion

        #region Record Detail Method Function of LaunderedProcessItemCentralization

        private LaunderedProcessItemCentralizationCollection LaunderedProcessItemCentralizations
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLaunderedProcessItemCentralization" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((LaunderedProcessItemCentralizationCollection)(obj));
                    }
                }

                var coll = new LaunderedProcessItemCentralizationCollection();
                var query = new LaunderedProcessItemCentralizationQuery("a");
                var iq = new ItemQuery("b");
                var nmq = new ItemProductNonMedicQuery("c");
                var unitq = new AppStandardReferenceItemQuery("d");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName"),
                        unitq.ItemName.As("refToAppStandardReferenceItem_ItemUnit")

                    );
                query.InnerJoin(iq).On(iq.ItemID == query.ItemID);
                query.InnerJoin(nmq).On(nmq.ItemID == query.ItemID);
                query.InnerJoin(unitq).On(unitq.ItemID == nmq.SRItemUnit &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.ProcessNo == txtProcessNo.Text);
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collLaunderedProcessItemCentralization" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collLaunderedProcessItemCentralization" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItemCentralized(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemCentralized.Columns[0].Visible = isVisible;
            grdItemCentralized.Columns[grdItemCentralized.Columns.Count - 1].Visible = isVisible;

            grdItemCentralized.MasterTableView.CommandItemDisplay = isVisible
                                                             ? GridCommandItemDisplay.Top
                                                             : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdItemCentralized.Rebind();
        }

        private void PopulateItemCentralizedGrid()
        {
            //Display Data Detail
            LaunderedProcessItemCentralizations = null; //Reset Record Detail
            grdItemCentralized.DataSource = LaunderedProcessItemCentralizations; //Requery
            grdItemCentralized.MasterTableView.IsItemInserted = false;
            grdItemCentralized.MasterTableView.ClearEditItems();
            grdItemCentralized.DataBind();
        }

        private LaunderedProcessItemCentralization FindItemCentralized(String itemId)
        {
            LaunderedProcessItemCentralizationCollection coll = LaunderedProcessItemCentralizations;
            LaunderedProcessItemCentralization retEntity = null;
            foreach (LaunderedProcessItemCentralization rec in coll)
            {
                if (rec.ItemID.Equals(itemId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItemCentralized_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemCentralized.DataSource = LaunderedProcessItemCentralizations;
        }

        protected void grdItemCentralized_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][LaunderedProcessItemCentralizationMetadata.ColumnNames.ItemID]);
            LaunderedProcessItemCentralization entity = FindItemCentralized(itemId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItemCentralized_InsertCommand(object source, GridCommandEventArgs e)
        {
            LaunderedProcessItemCentralization entity = LaunderedProcessItemCentralizations.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemCentralized.Rebind();
        }

        protected void grdItemCentralized_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String itemId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LaunderedProcessItemCentralizationMetadata.ColumnNames.ItemID]);
            LaunderedProcessItemCentralization entity = FindItemCentralized(itemId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        private void SetEntityValue(LaunderedProcessItemCentralization entity, GridCommandEventArgs e)
        {
            var userControl = (LaunderedProcessDetailItemCentralized)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Qty = userControl.Qty;
                entity.ItemUnit = userControl.ItemUnit;
            }
        }

        #endregion

        #region Record Detail Method Function of LaunderedProcessItemInfectiouss

        private LaunderedProcessItemInfectiousCollection LaunderedProcessItemInfectiouss
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLaunderedProcessItemInfectious" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((LaunderedProcessItemInfectiousCollection)(obj));
                    }
                }

                var coll = new LaunderedProcessItemInfectiousCollection();
                var query = new LaunderedProcessItemInfectiousQuery("a");
                var received = new LaundryReceivedItemInfectiousQuery("b");
                var iq = new ItemLinenQuery("c");
                var unitq = new AppStandardReferenceItemQuery("d");

                query.Select
                    (
                        query,

                        received.ItemID.As("refToLaundryReceivedItemInfectious_ItemID"),
                        iq.ItemName.As("refToItemLinen_ItemName"),

                        unitq.ItemName.As("refToAppStandardReferenceItem_ItemUnit"),
                        received.Notes.As("refToLaundryReceivedItemInfectious_Notes")

                    );
                query.InnerJoin(received).On(received.ReceivedNo == query.ReceivedNo &&
                                         received.ReceivedSeqNo == query.ReceivedSeqNo);
                query.InnerJoin(iq).On(iq.ItemID == received.ItemID);
                query.InnerJoin(unitq).On(unitq.ItemID == received.SRItemUnit &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.ProcessNo == txtProcessNo.Text);
                query.OrderBy(query.ProcessSeqNo.Ascending);
                coll.Load(query);

                Session["collLaunderedProcessItemInfectious" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collLaunderedProcessItemInfectious" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItemInfectious(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemInfectious.Columns[0].Visible = isVisible;
            grdItemInfectious.Columns[grdItemInfectious.Columns.Count - 1].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdItemInfectious.Rebind();
        }

        private void PopulateItemInfectiousGrid()
        {
            //Display Data Detail
            LaunderedProcessItemInfectiouss = null; //Reset Record Detail
            grdItemInfectious.DataSource = LaunderedProcessItemInfectiouss; //Requery
            grdItemInfectious.MasterTableView.IsItemInserted = false;
            grdItemInfectious.MasterTableView.ClearEditItems();
            grdItemInfectious.DataBind();
        }

        private LaunderedProcessItemInfectious FindItemInfectious(String seqNo)
        {
            LaunderedProcessItemInfectiousCollection coll = LaunderedProcessItemInfectiouss;
            LaunderedProcessItemInfectious retEntity = null;
            foreach (LaunderedProcessItemInfectious rec in coll)
            {
                if (rec.ProcessSeqNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItemInfectious_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemInfectious.DataSource = LaunderedProcessItemInfectiouss;
        }

        protected void grdItemInfectious_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String seqNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][LaunderedProcessItemInfectiousMetadata.ColumnNames.ProcessSeqNo]);
            LaunderedProcessItemInfectious entity = FindItemInfectious(seqNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItemInfectious_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LaunderedProcessItemInfectiousMetadata.ColumnNames.ProcessSeqNo]);
            LaunderedProcessItemInfectious entity = FindItemInfectious(seqNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        private void SetEntityValue(LaunderedProcessItemInfectious entity, GridCommandEventArgs e)
        {
            var userControl = (LaunderedProcessDetailItemInfectious)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.Qty = userControl.Qty;
            }
        }

        #endregion

        #region Record Detail Method Function of LaunderedProcessItemRewashings

        private LaunderedProcessItemRewashingCollection LaunderedProcessItemRewashings
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLaunderedProcessItemRewashing" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((LaunderedProcessItemRewashingCollection)(obj));
                    }
                }

                var coll = new LaunderedProcessItemRewashingCollection();
                var query = new LaunderedProcessItemRewashingQuery("a");
                var iq = new ItemQuery("c");
                var unitq = new AppStandardReferenceItemQuery("d");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName"),
                        unitq.ItemName.As("refToAppStandardReferenceItem_ItemUnit")
                    );
                query.InnerJoin(iq).On(iq.ItemID == query.ItemID);
                query.InnerJoin(unitq).On(unitq.ItemID == query.SRItemUnit &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.ProcessNo == txtProcessNo.Text);
                query.OrderBy(query.ProcessSeqNo.Ascending);
                coll.Load(query);

                Session["collLaunderedProcessItemRewashing" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collLaunderedProcessItemRewashing" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItemRewashing(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemRewashing.Columns[0].Visible = isVisible;
            grdItemRewashing.Columns[grdItemRewashing.Columns.Count - 1].Visible = isVisible;

            grdItemRewashing.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdItemRewashing.Rebind();
        }

        private void PopulateItemRewashingGrid()
        {
            //Display Data Detail
            LaunderedProcessItemRewashings = null; //Reset Record Detail
            grdItemRewashing.DataSource = LaunderedProcessItemRewashings; //Requery
            grdItemRewashing.MasterTableView.IsItemInserted = false;
            grdItemRewashing.MasterTableView.ClearEditItems();
            grdItemRewashing.DataBind();
        }

        private LaunderedProcessItemRewashing FindItemRewashing(String seqNo)
        {
            LaunderedProcessItemRewashingCollection coll = LaunderedProcessItemRewashings;
            LaunderedProcessItemRewashing retEntity = null;
            foreach (LaunderedProcessItemRewashing rec in coll)
            {
                if (rec.ProcessSeqNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItemRewashing_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemRewashing.DataSource = LaunderedProcessItemRewashings;
        }

        protected void grdItemRewashing_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String seqNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][LaunderedProcessItemRewashingMetadata.ColumnNames.ProcessSeqNo]);
            LaunderedProcessItemRewashing entity = FindItemRewashing(seqNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItemRewashing_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LaunderedProcessItemRewashingMetadata.ColumnNames.ProcessSeqNo]);
            LaunderedProcessItemRewashing entity = FindItemRewashing(seqNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }
        protected void grdItemRewashing_InsertCommand(object source, GridCommandEventArgs e)
        {
            LaunderedProcessItemRewashing entity = LaunderedProcessItemRewashings.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemRewashing.Rebind();
        }
        private void SetEntityValue(LaunderedProcessItemRewashing entity, GridCommandEventArgs e)
        {
            var userControl = (LaunderedProcessDetailItemRewashing)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ProcessNo = txtProcessNo.Text;
                entity.ProcessSeqNo = userControl.ProcessSeqNo;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Qty = userControl.Qty;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ItemUnit = userControl.ItemUnitName;
                entity.Notes = userControl.Notes;
            }
        }

        #endregion

        #region Record Detail Method Function of LaunderedProcessItemConsumptions

        private LaunderedProcessItemConsumptionCollection LaunderedProcessItemConsumptions
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLaunderedProcessItemConsumption" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((LaunderedProcessItemConsumptionCollection)(obj));
                    }
                }

                var coll = new LaunderedProcessItemConsumptionCollection();
                var query = new LaunderedProcessItemConsumptionQuery("a");
                var iq = new ItemQuery("b");
                var unitq = new AppStandardReferenceItemQuery("c");
                var inmq = new ItemProductNonMedicQuery("d");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName"),
                        unitq.ItemName.As("refToAppStandardReferenceItem_ItemUnit"),
                        inmq.IsInventoryItem.As("refToItemProductNonMedic_IsInventoryItem")
                    );
                query.InnerJoin(iq).On(iq.ItemID == query.ItemID);
                query.InnerJoin(unitq).On(unitq.ItemID == query.SRItemUnit &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.InnerJoin(inmq).On(inmq.ItemID == query.ItemID);
                query.Where(query.ProcessNo == txtProcessNo.Text);
                query.OrderBy(query.ItemID.Ascending);
                coll.Load(query);

                Session["collLaunderedProcessItemConsumption" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collLaunderedProcessItemConsumption" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItemConsumption(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemConsumption.Columns[0].Visible = isVisible;
            grdItemConsumption.Columns[grdItemConsumption.Columns.Count - 1].Visible = isVisible;

            grdItemConsumption.MasterTableView.CommandItemDisplay = isVisible
                                                             ? GridCommandItemDisplay.Top
                                                             : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdItemConsumption.Rebind();
        }

        private void PopulateItemConsumptionGrid()
        {
            //Display Data Detail
            LaunderedProcessItemConsumptions = null; //Reset Record Detail
            grdItemConsumption.DataSource = LaunderedProcessItemConsumptions; //Requery
            grdItemConsumption.MasterTableView.IsItemInserted = false;
            grdItemConsumption.MasterTableView.ClearEditItems();
            grdItemConsumption.DataBind();
        }

        private LaunderedProcessItemConsumption FindItemConsumption(String itemId)
        {
            LaunderedProcessItemConsumptionCollection coll = LaunderedProcessItemConsumptions;
            LaunderedProcessItemConsumption retEntity = null;
            foreach (LaunderedProcessItemConsumption rec in coll)
            {
                if (rec.ItemID.Equals(itemId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItemConsumption_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemConsumption.DataSource = LaunderedProcessItemConsumptions;
        }

        protected void grdItemConsumption_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][LaunderedProcessItemConsumptionMetadata.ColumnNames.ItemID]);
            LaunderedProcessItemConsumption entity = FindItemConsumption(itemId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItemConsumption_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String itemId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LaunderedProcessItemConsumptionMetadata.ColumnNames.ItemID]);
            LaunderedProcessItemConsumption entity = FindItemConsumption(itemId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItemConsumption_InsertCommand(object source, GridCommandEventArgs e)
        {
            LaunderedProcessItemConsumption entity = LaunderedProcessItemConsumptions.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemConsumption.Rebind();
        }

        private void SetEntityValue(LaunderedProcessItemConsumption entity, GridCommandEventArgs e)
        {
            var userControl = (LaunderedProcessDetailItemConsumption)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Qty = userControl.Qty;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ItemUnit = userControl.ItemUnitName;
                entity.CostPrice = userControl.CostPrice;
                entity.Price = userControl.Price;
                entity.IsInventoryItem = userControl.IsInventoryItem;
            }
        }

        #endregion

        #region Combobox
        protected void cboMachineID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new LaundryWashingMachineQuery("a");
            query.Where(
                query.Or(query.MachineID == e.Text,
                query.MachineName.Like(searchTextContain)),
                query.IsActive == true
                );
            if (!string.IsNullOrEmpty(cboSRLaundryProgram.SelectedValue))
            {
                var p = new LaundryWashingMachineProgramQuery("b");
                query.InnerJoin(p).On(p.MachineID == query.MachineID);
            }

            query.Select(query.MachineID, query.MachineName);

            query.es.Top = 20;
            query.es.Distinct = true;
            DataTable dtb = query.LoadDataTable();
            cboMachineID.DataSource = dtb;
            cboMachineID.DataBind();
        }

        protected void cboMachineID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["MachineName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["MachineID"].ToString();
        }

        protected void cboMachineID_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMachineInfo(cboMachineID.SelectedValue, true);
        }

        private void GetMachineInfo(string machineId, bool isNew)
        {
            var p = new LaundryWashingMachine();
            if (p.LoadByPrimaryKey(machineId))
            {
                txtMachineVolume.Value = Convert.ToDouble(p.Volume);
                txtMachineNotes.Text = p.Notes;
            }
            else
            {
                txtMachineVolume.Value = 0;
                txtMachineNotes.Text = string.Empty;
            }

            if (isNew)
            {
                cboSRLaundryProgram.SelectedValue = string.Empty;
                cboSRLaundryProgram.Text = string.Empty;
                cboSRLaundryType.SelectedValue = string.Empty;
                cboSRLaundryType.Text = string.Empty;

                PopulateItemConsumption();
            }
        }

        private void PopulateItemConsumption()
        {
            LaunderedProcessItemConsumptions.MarkAllAsDeleted();

            var micColl = new LaundryWashingProgramTypeItemConsumptionCollection();
            if (getPageID == "")
                micColl.Query.Where(micColl.Query.SRLaundryProcessType == "01");
            else if (getPageID == "n")
                micColl.Query.Where(micColl.Query.SRLaundryProcessType == "01");
            else if (getPageID == "i")
                micColl.Query.Where(micColl.Query.SRLaundryProcessType == "02");
            else
                micColl.Query.Where(micColl.Query.SRLaundryProcessType == "03");
            micColl.Query.Where(micColl.Query.SRLaundryProgram == cboSRLaundryProgram.SelectedValue, micColl.Query.SRLaundryType == cboSRLaundryType.SelectedValue);
            micColl.LoadAll();
            foreach (var mic in micColl)
            {
                var x = LaunderedProcessItemConsumptions.AddNew();
                x.ItemID = mic.ItemID;
                x.Qty = mic.Qty;
                x.SRItemUnit = mic.SRItemUnit;

                var i = new Item();
                if (i.LoadByPrimaryKey(x.ItemID))
                    x.ItemName = i.ItemName;
                else x.ItemName = string.Empty;

                var ipm = new ItemProductNonMedic();
                if (ipm.LoadByPrimaryKey(x.ItemID))
                {
                    x.CostPrice = ipm.CostPrice;
                    x.Price = ipm.PriceInBaseUnit;
                }
                else
                {
                    x.CostPrice = 0;
                    x.Price = 0;
                }

                var unit = new AppStandardReferenceItem();
                if (unit.LoadByPrimaryKey("ItemUnit", x.SRItemUnit))
                    x.ItemUnit = unit.ItemName;
                else
                    x.ItemName = string.Empty;
            }

            grdItemConsumption.Rebind();
        }

        protected void cboSRLaundryProgram_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new LaundryWashingMachineProgramQuery("a");
            var p = new AppStandardReferenceItemQuery("b");
            query.InnerJoin(p).On(p.StandardReferenceID == "LaundryProgram" && p.ItemID == query.SRLaundryProgram);

            query.Where(query.MachineID == cboMachineID.SelectedValue,
                query.Or(query.SRLaundryProgram == e.Text,
                p.ItemName.Like(searchTextContain))
                );
            query.Select(p.ItemID, p.ItemName);

            query.es.Top = 20;
            query.es.Distinct = true;
            DataTable dtb = query.LoadDataTable();
            cboSRLaundryProgram.DataSource = dtb;
            cboSRLaundryProgram.DataBind();
        }

        protected void cboSRLaundryProgram_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRLaundryProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSRLaundryType.SelectedValue = string.Empty;
            cboSRLaundryType.Text = string.Empty;
            PopulateItemConsumption();
        }

        protected void cboSRLaundryType_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new LaundryWashingProgramTypeQuery("a");
            var p = new AppStandardReferenceItemQuery("b");
            query.InnerJoin(p).On(p.StandardReferenceID == "LaundryType" && p.ItemID == query.SRLaundryType);
            if (getPageID == "")
                query.Where(query.SRLaundryProcessType == "01");
            else if (getPageID == "n")
                query.Where(query.SRLaundryProcessType == "01");
            else if (getPageID == "i")
                query.Where(query.SRLaundryProcessType == "02");
            else 
                query.Where(query.SRLaundryProcessType == "03");
            query.Where(query.SRLaundryProgram == cboSRLaundryProgram.SelectedValue,
                query.Or(query.SRLaundryType == e.Text,
                p.ItemName.Like(searchTextContain))
                );
            query.Select(p.ItemID, p.ItemName);

            query.es.Top = 20;
            query.es.Distinct = true;
            DataTable dtb = query.LoadDataTable();
            cboSRLaundryType.DataSource = dtb;
            cboSRLaundryType.DataBind();
        }

        protected void cboSRLaundryType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRLaundryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateItemConsumption();
        }
        #endregion

        private string GetNewProcessNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date,
                                                  (getPageID == "" || getPageID == "n") ? AppEnum.AutoNumber.LaundryProcessNo : (getPageID == "i" ? AppEnum.AutoNumber.LaundryProcessInfNo : AppEnum.AutoNumber.LaundryReProcessNo));

            return _autoNumber.LastCompleteNumber;
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (sourceControl is RadGrid)
            {
                if (eventArgument == "rebind")
                    grdItem.Rebind();
                else if (eventArgument == "rebind1")
                    grdItemCentralized.Rebind();
                else if (eventArgument == "rebind2")
                    grdItemInfectious.Rebind();
                else
                    grdItemRewashing.Rebind();
            }
        }

        protected void btnResetItem_Click(object sender, EventArgs e)
        {
            if (LaunderedProcessItems.Count > 0)
                LaunderedProcessItems.MarkAllAsDeleted();
            grdItem.DataSource = LaunderedProcessItems;
            grdItem.DataBind();

            if (LaunderedProcessItemCentralizations.Count > 0)
                LaunderedProcessItemCentralizations.MarkAllAsDeleted();
            grdItemCentralized.DataSource = LaunderedProcessItemCentralizations;
            grdItemCentralized.DataBind();

            if (LaunderedProcessItemInfectiouss.Count > 0)
                LaunderedProcessItemInfectiouss.MarkAllAsDeleted();
            grdItemInfectious.DataSource = LaunderedProcessItemInfectiouss;
            grdItemInfectious.DataBind();

            if (LaunderedProcessItemRewashings.Count > 0)
                LaunderedProcessItemRewashings.MarkAllAsDeleted();
            grdItemRewashing.DataSource = LaunderedProcessItemRewashings;
            grdItemRewashing.DataBind();
        }
    }
}