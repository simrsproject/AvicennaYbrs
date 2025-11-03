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
    public partial class EventMealOrderDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New)
                return;

            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer(), AppEnum.AutoNumber.EventMealOrderNo);
            txtOrderNo.Text = _autoNumber.LastCompleteNumber;
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "EventMealOrderSearch.aspx";
            UrlPageList = "EventMealOrderList.aspx";

            ProgramID = AppConstant.Program.EventMealOrder;
            this.WindowSearch.Height = 400;

            if (!IsPostBack)
                EventMealOrderItems = null;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdEventMealOrderItem, grdEventMealOrderItem);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
            
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new EventMealOrder();
            if (!entity.LoadByPrimaryKey(txtOrderNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new EventMealOrder();
            if (!entity.LoadByPrimaryKey(txtOrderNo.Text))
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
            var entity = new EventMealOrder();
            if (!entity.LoadByPrimaryKey(txtOrderNo.Text))
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

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new EventMealOrder());
            txtOrderDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            PopulateNewTransactionNo();
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new EventMealOrder();
            if (entity.LoadByPrimaryKey(txtOrderNo.Text))
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

            var entity = new EventMealOrder();
            SetEntityValue(entity);
            if (EventMealOrderItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new EventMealOrder();
            if (entity.LoadByPrimaryKey(txtOrderNo.Text))
            {
                SetEntityValue(entity);
                if (EventMealOrderItems.Count == 0)
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
            auditLogFilter.PrimaryKeyData = string.Format("OrderNo='{0}'", txtOrderNo.Text.Trim());
            auditLogFilter.TableName = "EventMealOrder";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_OrderNo", txtOrderNo.Text);
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtOrderNo.Text != string.Empty;
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
            var entity = new EventMealOrder();
            if (parameters.Length > 0)
            {
                var transactionNo = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transactionNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtOrderNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var emo = (EventMealOrder)entity;
            txtOrderNo.Text = emo.OrderNo;
            txtOrderDate.SelectedDate = emo.OrderDate;
            txtEventName.Text = emo.EventName;
            txtPic.Text = emo.Pic;
            txtEventDate.SelectedDate = emo.EventDate;
            txtEventTime.Text = emo.EventTime;
            txtNoOfParticipant.Value = emo.NoOfParticipant;
            txtNotes.Text = emo.Notes;
            chkIsVoid.Checked = emo.IsVoid ?? false;
            chkIsApproved.Checked = emo.IsApproved ?? false;

            //Display Data Detail
            PopulateGridDetail();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(EventMealOrder entity)
        {
            entity.OrderNo = txtOrderNo.Text;
            entity.OrderDate = txtOrderDate.SelectedDate;
            entity.EventName = txtEventName.Text;
            entity.Pic = txtPic.Text;
            entity.EventDate = txtEventDate.SelectedDate;
            entity.EventTime = txtEventTime.TextWithLiterals;
            entity.NoOfParticipant = Convert.ToInt16(txtNoOfParticipant.Value);
            entity.Notes = txtNotes.Text;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            //Update Detil
            foreach (var item in EventMealOrderItems)
            {
                item.OrderNo = txtOrderNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(EventMealOrder entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                EventMealOrderItems.Save();

                //autonumber has been saved on SetEntity
                //if (DataModeCurrent == DataMode.New)
                //    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new EventMealOrderQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.OrderNo > txtOrderNo.Text);
                que.OrderBy(que.OrderNo.Ascending);
            }
            else
            {
                que.Where(que.OrderNo < txtOrderNo.Text);
                que.OrderBy(que.OrderNo.Descending);
            }
            var entity = new EventMealOrder();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        private void SetApproved(EventMealOrder entity, bool isApproved)
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

        private void SetVoid(EventMealOrder entity, bool isVoid)
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

        #endregion

        #region Record Detail Method Function

        private EventMealOrderItemCollection EventMealOrderItems
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collEventMealOrderItem" + Request.UserHostName];
                    if (obj != null)
                        return ((EventMealOrderItemCollection)(obj));
                }

                var coll = new EventMealOrderItemCollection();

                var query = new EventMealOrderItemQuery("a");
                var food = new FoodQuery("b");

                query.InnerJoin(food).On(query.FoodID == food.FoodID);
                query.OrderBy(query.FoodID.Ascending);

                query.Select(
                    query,
                    food.FoodName.As("refToFood_FoodName")
                    );
                query.Where(query.OrderNo == txtOrderNo.Text);
                coll.Load(query);
                Session["collEventMealOrderItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collEventMealOrderItem" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            var isVisible = (newVal != AppEnum.DataMode.Read);
            grdEventMealOrderItem.Columns[0].Visible = isVisible;
            grdEventMealOrderItem.Columns[grdEventMealOrderItem.Columns.Count - 1].Visible = isVisible;

            grdEventMealOrderItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                EventMealOrderItems = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdEventMealOrderItem.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            EventMealOrderItems = null; //Reset Record Detail
            grdEventMealOrderItem.DataSource = EventMealOrderItems;
            grdEventMealOrderItem.MasterTableView.IsItemInserted = false;
            grdEventMealOrderItem.MasterTableView.ClearEditItems();
            grdEventMealOrderItem.DataBind();
        }

        protected void grdEventMealOrderItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEventMealOrderItem.DataSource = EventMealOrderItems;
        }

        protected void grdEventMealOrderItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            var foodId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EventMealOrderItemMetadata.ColumnNames.FoodID]);
            var entity = FindEventMealOrderItem(foodId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        private EventMealOrderItem FindEventMealOrderItem(String foodId)
        {
            var coll = EventMealOrderItems;
            return coll.FirstOrDefault(rec => rec.FoodID.Equals(foodId));
        }

        protected void grdEventMealOrderItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var foodId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][EventMealOrderItemMetadata.ColumnNames.FoodID]);
            var entity = FindEventMealOrderItem(foodId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdEventMealOrderItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = EventMealOrderItems.AddNew();
            SetEntityValue(entity, e);

            //grid not close first
            e.Canceled = true;
            grdEventMealOrderItem.Rebind();
        }

        private void SetEntityValue(EventMealOrderItem entity, GridCommandEventArgs e)
        {
            var userControl = (EventMealOrderItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.FoodID = userControl.FoodID;
                entity.FoodName = userControl.FoodName;
                entity.Qty = Convert.ToInt16(userControl.Qty);
            }
        }

        #endregion
    }
}
