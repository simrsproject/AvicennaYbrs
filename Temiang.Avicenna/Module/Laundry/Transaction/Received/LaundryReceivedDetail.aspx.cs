using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaundryReceivedDetail : BasePageDetail
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
            UrlPageSearch = "LaundryReceivedSearch.aspx?type=" + getPageID;
            UrlPageList = "LaundryReceivedList.aspx?type=" + getPageID;

            ProgramID = getPageID == "i" ? AppConstant.Program.LaundryReceivedInfectious : AppConstant.Program.LaundryReceived;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                if (getPageID == "")
                {
                    tabStrip.Tabs[0].Visible = true;
                    tabStrip.Tabs[1].Visible = false;
                    multiPage.SelectedIndex = 0;
                }
                else
                {
                    tabStrip.Tabs[0].Visible = false;
                    tabStrip.Tabs[1].Visible = true;
                    multiPage.SelectedIndex = 1;
                    if (getPageID == "n")
                    {
                        tabStrip.Tabs[1].Text = "Non Infectious";
                    }
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
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromServiceUnitID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromRoomID);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new LaundryReceived());

            txtReceivedNo.Text = GetNewReceivedNo();
            txtReceivedDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtReceivedTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //var entity = new LaundryReceived();
            //if (entity.LoadByPrimaryKey(txtReceivedNo.Text))
            //{
            //    entity.MarkAsDeleted();

            //    SaveEntity(entity);
            //}
            //else
            //    args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
            {
                args.MessageText = "From Unit required.";
                args.IsCancel = true;
                return;
            }

            if (getPageID == "")
            {
                if (LaundryReceivedItems.Count == 0)
                {
                    args.MessageText = AppConstant.Message.RecordDetailEmpty;
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                if (LaundryReceivedItemInfectiouss.Count == 0)
                {
                    args.MessageText = AppConstant.Message.RecordDetailEmpty;
                    args.IsCancel = true;
                    return;
                }
            }

            var entity = new LaundryReceived();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
            {
                args.MessageText = "From Unit required.";
                args.IsCancel = true;
                return;
            }

            if (getPageID == "")
            {
                if (LaundryReceivedItems.Count == 0)
                {
                    args.MessageText = AppConstant.Message.RecordDetailEmpty;
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                if (LaundryReceivedItemInfectiouss.Count == 0)
                {
                    args.MessageText = AppConstant.Message.RecordDetailEmpty;
                    args.IsCancel = true;
                    return;
                }
            }

            var entity = new LaundryReceived();
            if (entity.LoadByPrimaryKey(txtReceivedNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
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
            auditLogFilter.PrimaryKeyData = string.Format("ReceivedNo='{0}'", txtReceivedNo.Text.Trim());
            auditLogFilter.TableName = "LaundryReceived";
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new LaundryReceived();
            if (entity.LoadByPrimaryKey(txtReceivedNo.Text))
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

        private bool IsApprovedOrVoid(LaundryReceived entity, ValidateArgs args)
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
            printJobParameters.AddNew("p_ReceivedNo", txtReceivedNo.Text);
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
            RefreshCommandItemInfectious(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new LaundryReceived();
            if (parameters.Length > 0)
            {
                var receivedNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(receivedNo);
            }
            else
                entity.LoadByPrimaryKey(txtReceivedNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var received = (LaundryReceived)entity;

            txtReceivedNo.Text = received.ReceivedNo;
            txtReceivedDate.SelectedDate = received.ReceivedDate;
            txtReceivedTime.Text = received.ReceivedTime;
            if (!string.IsNullOrEmpty(received.FromServiceUnitID))
            {
                var q = new ServiceUnitQuery();
                q.Where(q.ServiceUnitID == received.FromServiceUnitID);
                cboFromServiceUnitID.DataSource = q.LoadDataTable();
                cboFromServiceUnitID.DataBind();
                cboFromServiceUnitID.SelectedValue = received.FromServiceUnitID;

                PopulateRoomList(received.FromServiceUnitID, false);
                cboFromRoomID.SelectedValue = received.FromRoomID;
            }
            else
            {
                cboFromServiceUnitID.Items.Clear();
                cboFromServiceUnitID.Text = string.Empty;
                cboFromRoomID.Items.Clear();
                cboFromRoomID.Text = string.Empty;
            }

            txtSenderBy.Text = received.SenderBy;

            if (!string.IsNullOrEmpty(received.ReceivedByUserID))
            {
                var usr = new AppUserQuery();
                usr.Where(usr.UserID == received.ReceivedByUserID);
                cboReceivedByUserID.DataSource = usr.LoadDataTable();
                cboReceivedByUserID.DataBind();
                cboReceivedByUserID.SelectedValue = received.ReceivedByUserID;
            }
            else
            {
                var usr = new AppUserQuery();
                usr.Where(usr.UserID == AppSession.UserLogin.UserID);
                cboReceivedByUserID.DataSource = usr.LoadDataTable();
                cboReceivedByUserID.DataBind();
                cboReceivedByUserID.SelectedValue = AppSession.UserLogin.UserID;
            }

            PopulateItemGrid();
            PopulateItemInfectiousGrid();

            ViewState["IsApproved"] = received.IsApproved ?? false;
            ViewState["IsVoid"] = received.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new LaundryReceived();
            if (!entity.LoadByPrimaryKey(txtReceivedNo.Text))
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
            var entity = new LaundryReceived();
            if (!entity.LoadByPrimaryKey(txtReceivedNo.Text))
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

            if (!AppSession.Parameter.IsCentralizedLaundrie || getPageID == "i")
            {
                DataTable dtb = (new LaunderedProcessItemCollection()).GetItemProceed(txtReceivedNo.Text);
                if (dtb.Rows.Count > 0)
                {
                    args.MessageText = "Data has been processed.";
                    args.IsCancel = true;
                    return;
                }
            }
            
            SetApproval(entity, false, args);
        }

        private void SetApproval(LaundryReceived entity, bool isApproval, ValidateArgs args)
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

                if (getPageID == "" && AppSession.Parameter.IsCentralizedLaundrie)
                {
                    var balancesFrom = new LaundryItemBalanceCollection();
                    var balancesTo = new LaundryItemBalanceCollection();
                    var movements = new LaundryItemMovementCollection();

                    if (isApproval)
                        LaundryItemBalance.PrepareBalancesForReceived(LaundryReceivedItems, entity.FromServiceUnitID, AppSession.Parameter.ServiceUnitLaundryID, AppSession.UserLogin.UserID,
                            ref balancesFrom, ref balancesTo, ref movements);
                    else
                        LaundryItemBalance.PrepareBalancesForReceivedReverse(LaundryReceivedItems, entity.FromServiceUnitID, AppSession.Parameter.ServiceUnitLaundryID, AppSession.UserLogin.UserID,
                            ref balancesFrom, ref balancesTo, ref movements);

                    if (balancesFrom != null)
                        balancesFrom.Save();
                    if (balancesTo != null)
                        balancesTo.Save();
                    if (movements != null)
                        movements.Save();
                }

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new LaundryReceived();
            if (!entity.LoadByPrimaryKey(txtReceivedNo.Text))
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
            var entity = new LaundryReceived();
            if (!entity.LoadByPrimaryKey(txtReceivedNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(LaundryReceived entity, bool isVoid)
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
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(LaundryReceived entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtReceivedNo.Text = GetNewReceivedNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.ReceivedNo = txtReceivedNo.Text;
            entity.ReceivedDate = txtReceivedDate.SelectedDate;
            entity.ReceivedTime = txtReceivedTime.TextWithLiterals;
            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            entity.FromRoomID = cboFromRoomID.SelectedValue;
            entity.SenderBy = txtSenderBy.Text;
            entity.ReceivedByUserID = cboReceivedByUserID.SelectedValue;
            entity.IsInfectious = getPageID == "i";
            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in LaundryReceivedItems)
            {
                item.ReceivedNo = txtReceivedNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in LaundryReceivedItemInfectiouss)
            {
                item.ReceivedNo = txtReceivedNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(LaundryReceived entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                LaundryReceivedItems.Save();
                LaundryReceivedItemInfectiouss.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new LaundryReceivedQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.ReceivedNo > txtReceivedNo.Text
                    );
                que.OrderBy(que.ReceivedNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.ReceivedNo < txtReceivedNo.Text
                    );
                que.OrderBy(que.ReceivedNo.Descending);
            }
            if (getPageID == "i")
                que.Where(que.IsInfectious == true);
            else
                que.Where(que.IsInfectious == false);

            var entity = new LaundryReceived();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of LaundryReceivedItem

        private LaundryReceivedItemCollection LaundryReceivedItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLaundryReceivedItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((LaundryReceivedItemCollection)(obj));
                    }
                }

                var coll = new LaundryReceivedItemCollection();
                var query = new LaundryReceivedItemQuery("a");
                var iq = new ItemQuery("b");
                var unitq = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName"),
                        unitq.ItemName.As("refToAppStandardReferenceItem_ItemUnit")

                    );
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                query.InnerJoin(unitq).On(query.SRItemUnit == unitq.ItemID &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.ReceivedNo == txtReceivedNo.Text);
                query.OrderBy(query.ReceivedSeqNo.Ascending);
                coll.Load(query);

                Session["collLaundryReceivedItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collLaundryReceivedItem" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible
                                                             ? GridCommandItemDisplay.Top
                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            LaundryReceivedItems = null; //Reset Record Detail
            grdItem.DataSource = LaundryReceivedItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private LaundryReceivedItem FindItem(String seqNo)
        {
            LaundryReceivedItemCollection coll = LaundryReceivedItems;
            LaundryReceivedItem retEntity = null;
            foreach (LaundryReceivedItem rec in coll)
            {
                if (rec.ReceivedSeqNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = LaundryReceivedItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String seqNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][LaundryReceivedItemMetadata.ColumnNames.ReceivedSeqNo]);
            LaundryReceivedItem entity = FindItem(seqNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LaundryReceivedItemMetadata.ColumnNames.ReceivedSeqNo]);
            LaundryReceivedItem entity = FindItem(seqNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            LaundryReceivedItem entity = LaundryReceivedItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(LaundryReceivedItem entity, GridCommandEventArgs e)
        {
            var userControl = (LaundryReceivedDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ReceivedSeqNo = userControl.ReceivedSeqNo;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Qty = userControl.Qty;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ItemUnit = userControl.ItemUnitName;
                entity.Notes = userControl.Notes;
            }
        }

        #endregion

        #region Record Detail Method Function of LaundryReceivedItemInfectious

        private LaundryReceivedItemInfectiousCollection LaundryReceivedItemInfectiouss
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLaundryReceivedItemInfectious" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((LaundryReceivedItemInfectiousCollection)(obj));
                    }
                }

                var coll = new LaundryReceivedItemInfectiousCollection();
                var query = new LaundryReceivedItemInfectiousQuery("a");
                var iq = new ItemLinenQuery("b");
                var unitq = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItemLinen_ItemName"),
                        unitq.ItemName.As("refToAppStandardReferenceItem_ItemUnit")

                    );
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                query.InnerJoin(unitq).On(query.SRItemUnit == unitq.ItemID &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.ReceivedNo == txtReceivedNo.Text);
                query.OrderBy(query.ReceivedSeqNo.Ascending);
                coll.Load(query);

                Session["collLaundryReceivedItemInfectious" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collLaundryReceivedItemInfectious" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItemInfectious(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItemInfectious.Columns[0].Visible = isVisible;
            grdItemInfectious.Columns[grdItemInfectious.Columns.Count - 1].Visible = isVisible;

            grdItemInfectious.MasterTableView.CommandItemDisplay = isVisible
                                                             ? GridCommandItemDisplay.Top
                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItemInfectious.Rebind();
        }

        private void PopulateItemInfectiousGrid()
        {
            //Display Data Detail
            LaundryReceivedItemInfectiouss = null; //Reset Record Detail
            grdItemInfectious.DataSource = LaundryReceivedItemInfectiouss; //Requery
            grdItemInfectious.MasterTableView.IsItemInserted = false;
            grdItemInfectious.MasterTableView.ClearEditItems();
            grdItemInfectious.DataBind();
        }

        private LaundryReceivedItemInfectious FindItemInfectious(String seqNo)
        {
            LaundryReceivedItemInfectiousCollection coll = LaundryReceivedItemInfectiouss;
            LaundryReceivedItemInfectious retEntity = null;
            foreach (LaundryReceivedItemInfectious rec in coll)
            {
                if (rec.ReceivedSeqNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItemInfectious_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemInfectious.DataSource = LaundryReceivedItemInfectiouss;
        }

        protected void grdItemInfectious_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String seqNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][LaundryReceivedItemInfectiousMetadata.ColumnNames.ReceivedSeqNo]);
            LaundryReceivedItemInfectious entity = FindItemInfectious(seqNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItemInfectious_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LaundryReceivedItemInfectiousMetadata.ColumnNames.ReceivedSeqNo]);
            LaundryReceivedItemInfectious entity = FindItemInfectious(seqNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItemInfectious_InsertCommand(object source, GridCommandEventArgs e)
        {
            LaundryReceivedItemInfectious entity = LaundryReceivedItemInfectiouss.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemInfectious.Rebind();
        }

        private void SetEntityValue(LaundryReceivedItemInfectious entity, GridCommandEventArgs e)
        {
            var userControl = (LaundryReceivedDetailItemInfectious)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ReceivedSeqNo = userControl.ReceivedSeqNo;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Qty = userControl.Qty;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ItemUnit = userControl.ItemUnitName;
                entity.Notes = userControl.Notes;
            }
        }

        #endregion

        #region Combobox
        protected void cboFromServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ServiceUnitItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboFromServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
                PopulateRoomList(e.Value, true);
            else
            {
                cboFromRoomID.Items.Clear();
                cboFromRoomID.Text = string.Empty;
            }
        }

        private void PopulateRoomList(string unitId, bool isNew)
        {
            cboFromRoomID.Items.Clear();

            var sr = new ServiceRoomCollection();
            var srQ = new ServiceRoomQuery("a");

            srQ.Select(srQ.RoomID, srQ.RoomName);
            srQ.Where(srQ.ServiceUnitID == unitId);
            if (isNew)
                srQ.Where(srQ.IsActive == true);
            srQ.OrderBy(srQ.RoomID.Ascending);
            srQ.es.Distinct = true;

            sr.Load(srQ);

            cboFromRoomID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ServiceRoom entity in sr)
            {
                cboFromRoomID.Items.Add(new RadComboBoxItem(entity.RoomName, entity.RoomID));
            }
        }

        protected void cboReceivedByUserID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.UserByUnitItemRequested((RadComboBox)sender, AppSession.Parameter.ServiceUnitLaundryID, e.Text);
        }

        protected void cboReceivedByUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.UserItemDataBound(e);
        }
        #endregion

        private string GetNewReceivedNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, getPageID == "i" ? AppEnum.AutoNumber.LaundryReceivedInfNo : AppEnum.AutoNumber.LaundryReceivedNo);

            return _autoNumber.LastCompleteNumber;
        }
    }
}
