using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class SnackOrderDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New)
                return;

            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer(), AppEnum.AutoNumber.SnackOrderNo);
            txtSnackOrderNo.Text = _autoNumber.LastCompleteNumber;
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "SnackOrderSearch.aspx";
            UrlPageList = "SnackOrderList.aspx";

            ProgramID = AppConstant.Program.SnackOrder;
            this.WindowSearch.Height = 400;

            if (!IsPostBack)
                SnackOrderItems = null;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdSnackOrderItem, grdSnackOrderItem);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
            cboServiceUnitID.Enabled = (SnackOrderItems.Count == 0);
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            if (!IsValidUser())
            {
                args.MessageText = "You don't have authorization to Edit this transaction. This data belong to unit: " +
                                   cboServiceUnitID.Text;
                args.IsCancel = true;
                return;
            }

            var entity = new SnackOrder();
            if (entity.LoadByPrimaryKey(txtSnackOrderNo.Text))
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
            if (!IsValidUser())
            {
                args.MessageText = "You don't have authorization to Approved this transaction. This data belong to unit: " +
                                   cboServiceUnitID.Text;
                args.IsCancel = true;
                return;
            }

            var entity = new SnackOrder();
            if (!entity.LoadByPrimaryKey(txtSnackOrderNo.Text))
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

            SetApproved(entity, true);  
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            if (!IsValidUser())
            {
                args.MessageText = "You don't have authorization to Void this transaction. This data belong to unit: " +
                                   cboServiceUnitID.Text;
                args.IsCancel = true;
                return;
            }

            var entity = new SnackOrder();
            if (!entity.LoadByPrimaryKey(txtSnackOrderNo.Text))
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

        private bool IsApprovedOrVoid(SnackOrder entity, ValidateArgs args)
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

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new SnackOrder());
            txtSnackOrderDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtSnackOrderForDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            cboServiceUnitID.Text = string.Empty;
            PopulateNewTransactionNo();
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new SnackOrder();
            if (entity.LoadByPrimaryKey(txtSnackOrderNo.Text))
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

            var entity = new SnackOrder();
            SetEntityValue(entity);
            if (SnackOrderItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new SnackOrder();
            if (entity.LoadByPrimaryKey(txtSnackOrderNo.Text))
            {
                SetEntityValue(entity);
                if (SnackOrderItems.Count == 0)
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
            auditLogFilter.PrimaryKeyData = string.Format("SnackOrderNo='{0}'", txtSnackOrderNo.Text.Trim());
            auditLogFilter.TableName = "SnackOrder";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_SnackOrderNo", txtSnackOrderNo.Text);
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtSnackOrderNo.Text != string.Empty;
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
            var entity = new SnackOrder();
            if (parameters.Length > 0)
            {
                var transactionNo = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transactionNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtSnackOrderNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var so = (SnackOrder)entity;
            txtSnackOrderNo.Text = so.SnackOrderNo;
            txtSnackOrderDate.SelectedDate = so.SnackOrderDate;
            txtSnackOrderForDate.SelectedDate = so.SnackOrderForDate;
            ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, TransactionCode.SnackOrder, true);
            cboServiceUnitID.SelectedValue = so.ServiceUnitID;
            txtNotes.Text = so.Notes;
            chkIsVoid.Checked = so.IsVoid ?? false;
            chkIsApproved.Checked = so.IsApproved ?? false;

            //Display Data Detail
            PopulateGridDetail();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(SnackOrder entity)
        {
            entity.SnackOrderNo = txtSnackOrderNo.Text;
            entity.SnackOrderDate = txtSnackOrderDate.SelectedDate;
            entity.SnackOrderForDate = txtSnackOrderForDate.SelectedDate;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.Notes = txtNotes.Text;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.IsApproved = chkIsApproved.Checked;
            entity.IsVoid = chkIsVoid.Checked;

            //Update Detil
            foreach (var item in SnackOrderItems)
            {
                item.SnackOrderNo = txtSnackOrderNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(SnackOrder entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                SnackOrderItems.Save();

                //autonumber has been saved on SetEntity
                //if (DataModeCurrent == DataMode.New)
                //    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new SnackOrderQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.SnackOrderNo > txtSnackOrderNo.Text);
                que.OrderBy(que.SnackOrderNo.Ascending);
            }
            else
            {
                que.Where(que.SnackOrderNo < txtSnackOrderNo.Text);
                que.OrderBy(que.SnackOrderNo.Descending);
            }
            var entity = new SnackOrder();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        private void SetApproved(SnackOrder entity, bool isApproved)
        {
            //header
            entity.IsApproved = isApproved;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void SetVoid(SnackOrder entity, bool isVoid)
        {
            //header
            entity.IsVoid = isVoid;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private bool IsValidUser()
        {
            var usr = new AppUserServiceUnitCollection();
            usr.Query.Where(usr.Query.UserID == AppSession.UserLogin.UserID &&
                            usr.Query.ServiceUnitID == cboServiceUnitID.SelectedValue);
            usr.LoadAll();
            
            return usr.Count > 0;
        }

        #endregion

        #region Record Detail Method Function

        private SnackOrderItemCollection SnackOrderItems
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collSnackOrderItem" + Request.UserHostName];
                    if (obj != null)
                        return ((SnackOrderItemCollection)(obj));
                }

                var coll = new SnackOrderItemCollection();

                var query = new SnackOrderItemQuery("a");
                var snack = new SnackQuery("b");

                query.InnerJoin(snack).On(query.SnackID == snack.SnackID);
                query.OrderBy(query.SnackID.Ascending);

                query.Select(
                    query,
                    snack.SnackName.As("refToSnack_SnackName")
                    );
                query.Where(query.SnackOrderNo == txtSnackOrderNo.Text);
                coll.Load(query);
                Session["collSnackOrderItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collSnackOrderItem" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            var isVisible = (newVal != AppEnum.DataMode.Read);
            grdSnackOrderItem.Columns[0].Visible = isVisible;
            grdSnackOrderItem.Columns[grdSnackOrderItem.Columns.Count - 1].Visible = isVisible;

            grdSnackOrderItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                SnackOrderItems = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdSnackOrderItem.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            SnackOrderItems = null; //Reset Record Detail
            grdSnackOrderItem.DataSource = SnackOrderItems;
            grdSnackOrderItem.MasterTableView.IsItemInserted = false;
            grdSnackOrderItem.MasterTableView.ClearEditItems();
            grdSnackOrderItem.DataBind();
        }

        protected void grdSnackOrderItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSnackOrderItem.DataSource = SnackOrderItems;
        }

        protected void grdSnackOrderItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            var snackId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][SnackOrderItemMetadata.ColumnNames.SnackID]);
            var entity = FindSnackOrderItem(snackId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        private SnackOrderItem FindSnackOrderItem(String snackId)
        {
            var coll = SnackOrderItems;
            return coll.FirstOrDefault(rec => rec.SnackID.Equals(snackId));
        }

        protected void grdSnackOrderItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var snackId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][SnackOrderItemMetadata.ColumnNames.SnackID]);
            var entity = FindSnackOrderItem(snackId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdSnackOrderItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = SnackOrderItems.AddNew();
            SetEntityValue(entity, e);

            //grid not close first
            e.Canceled = true;
            grdSnackOrderItem.Rebind();
        }

        private void SetEntityValue(SnackOrderItem entity, GridCommandEventArgs e)
        {
            var userControl = (SnackOrderItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SnackID = userControl.SnackID;
                entity.SnackName = userControl.SnackName;
                entity.QtyShift1 = userControl.QtyShift1;
                entity.QtyShift2 = userControl.QtyShift2;
                entity.QtyShift3 = userControl.QtyShift3;
                entity.Notes = userControl.Notes;
            }
        }

        #endregion
    }
}
