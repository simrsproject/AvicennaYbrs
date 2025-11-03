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
    public partial class LaundrySortingProcessDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "LaundrySortingProcessSearch.aspx";
            UrlPageList = "LaundrySortingProcessList.aspx";

            ProgramID = AppConstant.Program.LaundrySortingProcess;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
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
            ajax.AddAjaxSetting(txtReferenceNo, txtReferenceNo);
            ajax.AddAjaxSetting(txtReferenceNo, txtProcessNo);
            ajax.AddAjaxSetting(txtReferenceNo, txtProcessSeqNo);
            ajax.AddAjaxSetting(txtReferenceNo, grdItemReference);
            ajax.AddAjaxSetting(grdItemReference, grdItemReference);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new LaundrySortingProcess());

            txtTransactionNo.Text = GetNewProcessNo();
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            btnGetPickList.Enabled = true;
            btnResetItem.Enabled = true;

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (LaundrySortingProcessItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new LaundrySortingProcess();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (LaundrySortingProcessItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new LaundrySortingProcess();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "LaundrySortingProcess";
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new LaundrySortingProcess();
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

        private bool IsApprovedOrVoid(LaundrySortingProcess entity, ValidateArgs args)
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
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
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

            btnGetPickList.Enabled = newVal != AppEnum.DataMode.Read;
            btnResetItem.Enabled = newVal != AppEnum.DataMode.Read;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new LaundrySortingProcess();
            if (parameters.Length > 0)
            {
                var transNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transNo);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var process = (LaundrySortingProcess)entity;

            txtTransactionNo.Text = process.TransactionNo;
            txtTransactionDate.SelectedDate = process.TransactionDate;
            txtProcessNo.Text = process.ProcessNo;
            txtProcessSeqNo.Text = process.ProcessSeqNo;
            txtNotes.Text = process.Notes;
            
            btnGetPickList.Enabled = false;
            btnResetItem.Enabled = false;

            PopulateItemGrid();
            PopulateItemReferenceGrid();

            ViewState["IsApproved"] = process.IsApproved ?? false;
            ViewState["IsVoid"] = process.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new LaundrySortingProcess();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
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
            var entity = new LaundrySortingProcess();
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

            if (entity.IsApproved == false)
            {
                args.MessageText = AppConstant.Message.RecordHasNotApproved;
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, false, args);
        }

        private void SetApproval(LaundrySortingProcess entity, bool isApproval, ValidateArgs args)
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

                if (AppSession.Parameter.IsCentralizedLaundrie)
                {
                    var fromUnit = string.Empty;
                    var proses = new LaunderedProcessItemInfectious();
                    if (proses.LoadByPrimaryKey(entity.ProcessNo, entity.ProcessSeqNo))
                    {
                        var received = new LaundryReceived();
                        if (received.LoadByPrimaryKey(proses.ReceivedNo))
                            fromUnit = received.FromServiceUnitID;
                    }

                    if (!string.IsNullOrEmpty(fromUnit))
                    {
                        var balancesFrom = new LaundryItemBalanceCollection();
                        var balancesTo = new LaundryItemBalanceCollection();
                        var movements = new LaundryItemMovementCollection();

                        if (isApproval)
                            LaundryItemBalance.PrepareBalancesForSorting(LaundrySortingProcessItems, fromUnit, AppSession.Parameter.ServiceUnitLaundryID, AppSession.UserLogin.UserID,
                                ref balancesFrom, ref balancesTo, ref movements);
                        else
                            LaundryItemBalance.PrepareBalancesForSortingReverse(LaundrySortingProcessItems, fromUnit, AppSession.Parameter.ServiceUnitLaundryID, AppSession.UserLogin.UserID,
                                ref balancesFrom, ref balancesTo, ref movements);

                        if (balancesFrom != null)
                            balancesFrom.Save();
                        if (balancesTo != null)
                            balancesTo.Save();
                        if (movements != null)
                            movements.Save();
                    }
                }

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new LaundrySortingProcess();
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

            SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            var entity = new LaundrySortingProcess();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(LaundrySortingProcess entity, bool isVoid)
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

        private void SetEntityValue(LaundrySortingProcess entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewProcessNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.ProcessNo = txtProcessNo.Text;
            entity.ProcessSeqNo = txtProcessSeqNo.Text;
            entity.Notes = txtNotes.Text;
            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in LaundrySortingProcessItems)
            {
                item.TransactionNo = txtTransactionNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(LaundrySortingProcess entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                LaundrySortingProcessItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new LaundrySortingProcessQuery();
            que.es.Top = 1; // SELECT TOP 1 ..

            if (isNextRecord)
            {
                que.Where
                    (
                        que.TransactionNo > txtTransactionNo.Text
                    );
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.TransactionNo < txtTransactionNo.Text
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new LaundrySortingProcess();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged
        #endregion

        #region Record Detail Method Function of LaundrySortingProcessItemReferences

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
                var receivedHd = new LaundryReceivedQuery("bh");
                var iq = new ItemLinenQuery("c");
                var unitq = new AppStandardReferenceItemQuery("d");
                var suq = new ServiceUnitQuery("su");

                query.Select
                    (
                        query,
                        received.ItemID.As("refToLaundryReceivedItemInfectious_ItemID"),
                        iq.ItemName.As("refToItemLinen_ItemName"),
                        unitq.ItemName.As("refToAppStandardReferenceItem_ItemUnit"),
                        received.Notes.As("refToLaundryReceivedItemInfectious_Notes"),
                        receivedHd.FromServiceUnitID.As("refToLaundryReceivedItemInfectious_FromServiceUnitID"),
                        suq.ServiceUnitName.As("refToLaundryReceivedItemInfectious_FromServiceUnitName")
                    );
                query.InnerJoin(received).On(received.ReceivedNo == query.ReceivedNo &&
                                         received.ReceivedSeqNo == query.ReceivedSeqNo);
                query.InnerJoin(receivedHd).On(receivedHd.ReceivedNo == received.ReceivedNo);
                query.InnerJoin(suq).On(suq.ServiceUnitID == receivedHd.FromServiceUnitID);
                query.InnerJoin(iq).On(iq.ItemID == received.ItemID);
                query.InnerJoin(unitq).On(unitq.ItemID == received.SRItemUnit &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.ProcessNo == txtProcessNo.Text, query.ProcessSeqNo == txtProcessSeqNo.Text);
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

        private void PopulateItemReferenceGrid()
        {
            //Display Data Detail
            LaunderedProcessItemInfectiouss = null; //Reset Record Detail
            grdItemReference.DataSource = LaunderedProcessItemInfectiouss; //Requery
            grdItemReference.MasterTableView.IsItemInserted = false;
            grdItemReference.MasterTableView.ClearEditItems();
            grdItemReference.DataBind();
        }

        protected void grdItemReference_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemReference.DataSource = LaunderedProcessItemInfectiouss;
        }
        #endregion

        #region Record Detail Method Function of LaundrySortingProcessItem

        private LaundrySortingProcessItemCollection LaundrySortingProcessItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLaundrySortingProcessItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((LaundrySortingProcessItemCollection)(obj));
                    }
                }

                var coll = new LaundrySortingProcessItemCollection();
                var query = new LaundrySortingProcessItemQuery("a");
                var iq = new ItemQuery("b");
                var unitq = new AppStandardReferenceItemQuery("c");
                var inmq = new ItemProductNonMedicQuery("d");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName"),
                        unitq.ItemName.As("refToAppStandardReferenceItem_ItemUnit")
                    );
                query.InnerJoin(iq).On(iq.ItemID == query.ItemID);
                query.InnerJoin(unitq).On(unitq.ItemID == query.SRItemUnit &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.InnerJoin(inmq).On(inmq.ItemID == query.ItemID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(iq.ItemName.Ascending);
                coll.Load(query);

                Session["collLaundrySortingProcessItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collLaundrySortingProcessItem" + Request.UserHostName] = value;
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
            LaundrySortingProcessItems = null; //Reset Record Detail
            grdItem.DataSource = LaundrySortingProcessItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private LaundrySortingProcessItem FindItem(String itemId)
        {
            LaundrySortingProcessItemCollection coll = LaundrySortingProcessItems;
            LaundrySortingProcessItem retEntity = null;
            foreach (LaundrySortingProcessItem rec in coll)
            {
                if (rec.ItemID.Equals(itemId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = LaundrySortingProcessItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][LaundrySortingProcessItemMetadata.ColumnNames.ItemID]);
            LaundrySortingProcessItem entity = FindItem(itemId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String itemId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LaundrySortingProcessItemMetadata.ColumnNames.ItemID]);
            LaundrySortingProcessItem entity = FindItem(itemId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            LaundrySortingProcessItem entity = LaundrySortingProcessItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(LaundrySortingProcessItem entity, GridCommandEventArgs e)
        {
            var userControl = (LaundrySortingProcessItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Qty = userControl.Qty;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ItemUnit = userControl.ItemUnitName;
            }
        }

        #endregion

        private string GetNewProcessNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.LaundrySortingNo);

            return _autoNumber.LastCompleteNumber;
        }

        //protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        //{
        //    base.RaisePostBackEvent(sourceControl, eventArgument);

        //    if (string.IsNullOrEmpty(eventArgument))
        //        return;

        //    if (sourceControl is RadGrid)
        //    {
        //        if (eventArgument == "rebind")
        //            grdItem.Rebind();
        //    }
        //}

        protected void btnResetItem_Click(object sender, EventArgs e)
        {
            if (txtProcessNo.Text != string.Empty)
            {
                txtProcessNo.Text = string.Empty;
                if (LaunderedProcessItemInfectiouss.Count > 0)
                    LaunderedProcessItemInfectiouss.MarkAllAsDeleted();
                grdItemReference.DataSource = LaunderedProcessItemInfectiouss;
                grdItemReference.DataBind();
            }
        }

        protected void txtReferenceNo_TextChanged(object sender, EventArgs e)
        {
            if (txtReferenceNo.Text == string.Empty || !txtReferenceNo.Text.Contains("|"))
            {
                txtProcessNo.Text = string.Empty;
                txtProcessSeqNo.Text = string.Empty;

                return;
            }
            var val = txtReferenceNo.Text.Split('|');

            txtProcessNo.Text = val[0];
            txtProcessSeqNo.Text= val[1];

            PopulateItemReferenceGrid();
        }

    }
}