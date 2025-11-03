using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class AdditionalMealOrderDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "AdditionalMealOrderSearch.aspx";
            UrlPageList = "AdditionalMealOrderList.aspx";

            ProgramID = AppConstant.Program.ExtraMealOrder;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                var qunitx = new ServiceUnitClassMenuExtraSettingQuery("a");
                var qunit = new ServiceUnitQuery("b");
                var qusr = new AppUserServiceUnitQuery("c");
                qunitx.InnerJoin(qunit).On(qunitx.ServiceUnitID == qunit.ServiceUnitID);
                qunitx.InnerJoin(qusr).On(qunitx.ServiceUnitID == qusr.ServiceUnitID && qusr.UserID == AppSession.UserLogin.UserID);
                qunitx.Select(qunitx.ServiceUnitID, qunit.ServiceUnitName);
                DataTable dtbx = qunitx.LoadDataTable();

                cboServiceUnitID.Items.Add(new RadComboBoxItem("", ""));
                foreach (DataRow row in dtbx.Rows)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(row["ServiceUnitName"].ToString(), row["ServiceUnitID"].ToString()));
                }

                var qmenu = new MenuQuery("a");
                qmenu.Select(qmenu.MenuID, qmenu.MenuName);
                qmenu.Where(qmenu.IsActive == true,
                            qmenu.Or(qmenu.IsExtra == true, qmenu.MenuID == AppSession.Parameter.DefaultMenuStandard));
                qmenu.OrderBy(qmenu.MenuID.Ascending);
                DataTable dtb = qmenu.LoadDataTable();

                cboMenuID.Items.Add(new RadComboBoxItem("", ""));
                foreach (DataRow row in dtb.Rows)
                {
                    cboMenuID.Items.Add(new RadComboBoxItem(row["MenuName"].ToString(), row["MenuID"].ToString()));
                }

                ComboBox.PopulateWithClassInpatient(cboClassID);
                StandardReference.InitializeIncludeSpace(cboSRMealSet, AppEnum.StandardReference.MealSet);

                AddMealOrderItemDetails = null;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItem, grdItem);
            ajax.AddAjaxSetting(cboServiceUnitID, cboServiceUnitID);
            ajax.AddAjaxSetting(cboServiceUnitID, cboClassID);
            ajax.AddAjaxSetting(cboServiceUnitID, cboMenuID);
        }

        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            //printJobParameters.AddNew("OrderNo", txtOrderNo.Text);
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AddMealOrder());

            txtOrderDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtEffectiveDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            txtOrderNo.Text = GetNewTransactionNo();

            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new AddMealOrder();
            if (entity.LoadByPrimaryKey(txtOrderNo.Text))
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

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (txtQty.Value <= 1)
            {
                args.MessageText = "Qty order must greater than 0.";
                args.IsCancel = true;
                return;
            }

            var menu = new Menu();
            menu.LoadByPrimaryKey(cboMenuID.SelectedValue);
            if (menu.IsExtra == false && AddMealOrderItemDetails.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            int x = AddMealOrderItemDetails.Aggregate(0, (current, item) => current + (item.Qty ?? 0));

            if (x > 0 && x != Convert.ToInt32(txtQty.Value))
            {
                args.MessageText = "Qty detail must equal with qty order.";
                args.IsCancel = true;
                return;
            }

            var entity = new AddMealOrder();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (txtQty.Value <= 1)
            {
                args.MessageText = "Qty order must greater than 0.";
                args.IsCancel = true;
                return;
            }

            var menu = new Menu();
            menu.LoadByPrimaryKey(cboMenuID.SelectedValue);
            if (menu.IsExtra == false && AddMealOrderItemDetails.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            int x = AddMealOrderItemDetails.Aggregate(0, (current, item) => current + (item.Qty ?? 0));
            if (x > 0 && x != Convert.ToInt32(txtQty.Value))
            {
                args.MessageText = "Qty detail must equal with qty order.";
                args.IsCancel = true;
                return;
            }

            var entity = new AddMealOrder();
            if (entity.LoadByPrimaryKey(txtOrderNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
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
            auditLogFilter.TableName = "AddMealOrder";
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new AddMealOrder();
            if (!entity.LoadByPrimaryKey(txtOrderNo.Text))
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

            SetApproved(entity, true);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new AddMealOrder();
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

            SetApproved(entity, false);
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new AddMealOrder();
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

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
        }

        private bool IsApprovedOrVoid(AddMealOrder entity, ValidateArgs args)
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

        private void SetApproved(AddMealOrder entity, bool isApproved)
        {
            //header
            entity.IsApproved = isApproved;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (var trans = new esTransactionScope())
            {
                if (isApproved)
                {
                    var menu = new Menu();
                    menu.LoadByPrimaryKey(entity.MenuID);

                    bool isExtra = menu.IsExtra ?? false;

                    var menuVersionId = string.Empty;
                    var menuSeqNo = string.Empty;

                    var menuSettingQ = new MenuSettingQuery();
                    menuSettingQ.es.Top = 1;
                    menuSettingQ.Where(menuSettingQ.StartingDate <= txtEffectiveDate.SelectedDate.Value.Date,
                                       menuSettingQ.IsExtra == isExtra);
                    menuSettingQ.OrderBy(menuSettingQ.StartingDate.Descending);

                    var menuSetting = new MenuSetting();
                    if (menuSetting.Load(menuSettingQ))
                    {
                        var menuVersion = new MenuVersion();
                        menuVersion.LoadByPrimaryKey(menuSetting.VersionID);
                        var cycle = menuVersion.Cycle ?? 1;

                        int diff = (txtEffectiveDate.SelectedDate.Value.Date.Subtract(menuSetting.StartingDate.Value.Date)).Days;
                        int i = (diff % cycle) + Convert.ToInt32(menuSetting.SeqNo);

                        if (i > cycle)
                            i = i - cycle;

                        var seqNo = i < 10 ? "0" + string.Format("{0}", i) : string.Format("{0}", i);

                        menuVersionId = menuSetting.VersionID;
                        menuSeqNo = seqNo;

                        entity.VersionID = menuVersionId;
                        entity.SeqNo = menuSeqNo;
                    }

                    var menuItemQ = new MenuItemQuery();
                    menuItemQ.es.Top = 1;
                    menuItemQ.Where(menuItemQ.MenuID == entity.MenuID,
                                    menuItemQ.VersionID == menuVersionId, menuItemQ.SeqNo == menuSeqNo,
                                    menuItemQ.ClassID == entity.ClassID, menuItemQ.IsActive == true);
                    menuItemQ.OrderBy(menuItemQ.LastUpdateDateTime.Descending);

                    var menuItem = new MenuItem();
                    var menuItemId = menuItem.Load(menuItemQ) ? menuItem.MenuItemID : string.Empty;

                    if (!string.IsNullOrEmpty(menuItemId))
                    {
                        var coll = new AddMealOrderItemCollection();

                        var menuItemFoodColl = new MenuItemFoodCollection();
                        menuItemFoodColl.Query.Where(menuItemFoodColl.Query.MenuItemID == menuItemId,
                                                     menuItemFoodColl.Query.IsStandard == true,
                                                     menuItemFoodColl.Query.SRMealSet == entity.SRMealSet);
                        menuItemFoodColl.LoadAll();

                        foreach (var menuItemFood in menuItemFoodColl)
                        {
                            var c = coll.AddNew();

                            c.OrderNo = entity.OrderNo;
                            c.FoodID = menuItemFood.FoodID;
                            c.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        }

                        coll.Save();

                        entity.MenuItemID = menuItemId;
                    }
                    else
                    {
                        var coll = new AddMealOrderItemCollection();

                        var menuItemExtraFoodColl = new MenuItemExtraFoodCollection();
                        var menuItemExtraFoodQ = new MenuItemExtraFoodQuery("a");
                        var menuItemExtraQ = new MenuItemExtraQuery("b");
                        var dayQ = new AppStandardReferenceItemQuery("c");
                        menuItemExtraFoodQ.InnerJoin(menuItemExtraQ).On(menuItemExtraFoodQ.SeqNo == menuItemExtraQ.SeqNo);
                        menuItemExtraFoodQ.InnerJoin(dayQ).On(menuItemExtraFoodQ.SRDayName == dayQ.ItemID &&
                                                              dayQ.StandardReferenceID == AppEnum.StandardReference.DayName);
                        menuItemExtraFoodQ.Where(menuItemExtraQ.StartingDate <= entity.EffectiveDate,
                                                 menuItemExtraQ.MenuID == entity.MenuID,
                                                 menuItemExtraQ.SRMealSet == entity.SRMealSet,
                                                 dayQ.ItemName == entity.EffectiveDate.Value.Date.DayOfWeek.ToString());
                        menuItemExtraFoodColl.Load(menuItemExtraFoodQ);

                        foreach (var menuItemFood in menuItemExtraFoodColl)
                        {
                            var c = coll.AddNew();

                            c.OrderNo = entity.OrderNo;
                            c.FoodID = menuItemFood.FoodID;
                            c.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        }

                        coll.Save();
                    }

                    #region mealOrderPlan

                    menuSettingQ = new MenuSettingQuery();
                    menuSettingQ.es.Top = 1;
                    menuSettingQ.Where(menuSettingQ.StartingDate <= txtEffectiveDate.SelectedDate.Value.Date.AddDays(1),
                                       menuSettingQ.IsExtra == isExtra);
                    menuSettingQ.OrderBy(menuSettingQ.StartingDate.Descending);

                    menuSetting = new MenuSetting();
                    if (menuSetting.Load(menuSettingQ))
                    {
                        var menuVersion = new MenuVersion();
                        menuVersion.LoadByPrimaryKey(menuSetting.VersionID);
                        var cycle = menuVersion.Cycle ?? 1;

                        int diff = (txtEffectiveDate.SelectedDate.Value.Date.AddDays(1).Subtract(menuSetting.StartingDate.Value.Date)).Days;
                        int i = (diff % cycle) + Convert.ToInt32(menuSetting.SeqNo);

                        if (i > cycle)
                            i = i - cycle;

                        var seqNo = i < 10 ? "0" + string.Format("{0}", i) : string.Format("{0}", i);

                        menuVersionId = menuSetting.VersionID;
                        menuSeqNo = seqNo;
                    }

                    menuItemQ = new MenuItemQuery();
                    menuItemQ.es.Top = 1;
                    menuItemQ.Where(menuItemQ.MenuID == entity.MenuID,
                                    menuItemQ.VersionID == menuVersionId, menuItemQ.SeqNo == menuSeqNo,
                                    menuItemQ.ClassID == entity.ClassID, menuItemQ.IsActive == true);
                    menuItemQ.OrderBy(menuItemQ.LastUpdateDateTime.Descending);

                    menuItem = new MenuItem();
                    menuItemId = menuItem.Load(menuItemQ) ? menuItem.MenuItemID : string.Empty;

                    if (!string.IsNullOrEmpty(menuItemId))
                    {
                        var coll = new MealOrderItemPlanCollection();

                        var menuItemFoodColl = new MenuItemFoodCollection();
                        menuItemFoodColl.Query.Where(menuItemFoodColl.Query.MenuItemID == menuItemId,
                                                     menuItemFoodColl.Query.IsStandard == true,
                                                     menuItemFoodColl.Query.SRMealSet == entity.SRMealSet);
                        menuItemFoodColl.LoadAll();

                        foreach (var menuItemFood in menuItemFoodColl)
                        {
                            var c = coll.AddNew();

                            c.OrderNo = entity.OrderNo;
                            c.OrderToDate = entity.EffectiveDate.Value.Date.AddDays(1);
                            c.SRMealSet = entity.SRMealSet;
                            c.FoodID = menuItemFood.FoodID;
                            c.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        }

                        coll.Save();
                    }
                    else
                    {
                        var coll = new MealOrderItemPlanCollection();

                        var menuItemExtraFoodColl = new MenuItemExtraFoodCollection();
                        var menuItemExtraFoodQ = new MenuItemExtraFoodQuery("a");
                        var menuItemExtraQ = new MenuItemExtraQuery("b");
                        var dayQ = new AppStandardReferenceItemQuery("c");
                        menuItemExtraFoodQ.InnerJoin(menuItemExtraQ).On(menuItemExtraFoodQ.SeqNo == menuItemExtraQ.SeqNo);
                        menuItemExtraFoodQ.InnerJoin(dayQ).On(menuItemExtraFoodQ.SRDayName == dayQ.ItemID &&
                                                              dayQ.StandardReferenceID == AppEnum.StandardReference.DayName);
                        menuItemExtraFoodQ.Where(menuItemExtraQ.StartingDate <= entity.EffectiveDate.Value.Date.AddDays(1),
                                                 menuItemExtraQ.MenuID == entity.MenuID,
                                                 menuItemExtraQ.SRMealSet == entity.SRMealSet,
                                                 dayQ.ItemName == entity.EffectiveDate.Value.Date.AddDays(1).DayOfWeek.ToString());
                        menuItemExtraFoodColl.Load(menuItemExtraFoodQ);

                        foreach (var menuItemFood in menuItemExtraFoodColl)
                        {
                            var c = coll.AddNew();

                            c.OrderNo = entity.OrderNo;
                            c.OrderToDate = entity.EffectiveDate.Value.Date.AddDays(1);
                            c.SRMealSet = entity.SRMealSet;
                            c.FoodID = menuItemFood.FoodID;
                            c.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        }

                        coll.Save();
                    }

                    #endregion

                }
                else
                {
                    entity.VersionID = null;
                    entity.SeqNo = null;
                    entity.MenuItemID = null;

                    var coll = new AddMealOrderItemCollection();
                    coll.Query.Where(coll.Query.OrderNo == entity.OrderNo);
                    coll.LoadAll();
                    coll.MarkAllAsDeleted();
                    coll.Save();

                    var collPlan = new MealOrderItemPlanCollection();
                    collPlan.Query.Where(collPlan.Query.OrderNo == entity.OrderNo);
                    collPlan.LoadAll();
                    collPlan.MarkAllAsDeleted();
                    collPlan.Save();
                }

                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void SetVoid(AddMealOrder entity, bool isVoid)
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

        #region ToolBar Menu Support
        public override bool OnGetStatusMenuEdit()
        {
            return txtOrderNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AddMealOrder();
            if (parameters.Length > 0)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                    entity.LoadByPrimaryKey(Request.QueryString["id"]);
            }
            else
                entity.LoadByPrimaryKey(txtOrderNo.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var x = (AddMealOrder)entity;
            txtOrderNo.Text = x.OrderNo;
            txtOrderDate.SelectedDate = x.OrderDate;
            if (x.EffectiveDate.HasValue)
                txtEffectiveDate.SelectedDate = x.EffectiveDate.Value.Date;
            cboServiceUnitID.SelectedValue = x.ServiceUnitID;
            cboClassID.SelectedValue = x.ClassID;
            cboMenuID.SelectedValue = x.MenuID;
            cboSRMealSet.SelectedValue = x.SRMealSet;
            txtQty.Value = Convert.ToDouble(x.Qty);
            txtNotes.Text = x.Notes;

            ViewState["IsApproved"] = x.IsApproved ?? false;
            ViewState["IsVoid"] = x.IsVoid ?? false;

            //Display Data Detail
            PopulateAddMealOrderItemDetailGrid();
        }
        #endregion

        #region Private Method Standard
        private void SetEntityValue(AddMealOrder entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtOrderNo.Text = GetNewTransactionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            entity.OrderNo = txtOrderNo.Text;
            entity.OrderDate = txtOrderDate.SelectedDate;
            entity.EffectiveDate = txtEffectiveDate.SelectedDate;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.ClassID = cboClassID.SelectedValue;
            entity.MenuID = cboMenuID.SelectedValue;
            entity.SRMealSet = cboSRMealSet.SelectedValue;
            entity.Qty = Convert.ToInt16(txtQty.Value);
            entity.Notes = txtNotes.Text;
            entity.IsApproved = false;
            entity.IsVoid = false;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (AddMealOrderItemDetail item in AddMealOrderItemDetails)
            {
                item.OrderNo = txtOrderNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(AddMealOrder entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                AddMealOrderItemDetails.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AddMealOrderQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.OrderNo > txtOrderNo.Text
                    );
                que.OrderBy(que.OrderNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.OrderNo < txtOrderNo.Text
                    );
                que.OrderBy(que.OrderNo.Descending);
            }

            var entity = new AddMealOrder();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer(), AppEnum.AutoNumber.AddMealOrderNo);
            return _autoNumber.LastCompleteNumber;
        }

        #endregion

        #region Record Detail Method Function AddMealOrderItem
        private AddMealOrderItemDetailCollection AddMealOrderItemDetails
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collAddMealOrderItemDetail" + Request.UserHostName];
                    if (obj != null)
                        return ((AddMealOrderItemDetailCollection)(obj));
                }

                var coll = new AddMealOrderItemDetailCollection();
                var query = new AddMealOrderItemDetailQuery("a");
                var foodQ = new FoodQuery("b");

                query.Select
                    (
                        query,
                        foodQ.FoodName.As("refToFood_FoodName")
                    );

                query.InnerJoin(foodQ).On(query.FoodID == foodQ.FoodID);
                query.Where(query.OrderNo == txtOrderNo.Text);

                query.OrderBy(query.FoodID.Ascending);
                coll.Load(query);

                Session["collAddMealOrderItemDetail" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collAddMealOrderItemDetail" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                AddMealOrderItemDetails = null;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateAddMealOrderItemDetailGrid()
        {
            //Display Data Detail
            AddMealOrderItemDetails = null; //Reset Record Detail
            grdItem.DataSource = AddMealOrderItemDetails; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = AddMealOrderItemDetails;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            String foodId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][AddMealOrderItemDetailMetadata.ColumnNames.FoodID]);
            AddMealOrderItemDetail entity = FindItemGrid(foodId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String foodId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][AddMealOrderItemDetailMetadata.ColumnNames.FoodID]);
            AddMealOrderItemDetail entity = FindItemGrid(foodId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            AddMealOrderItemDetail entity = AddMealOrderItemDetails.AddNew();
            SetEntityValue(entity, e);

            //grid not close first
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(AddMealOrderItemDetail entity, GridCommandEventArgs e)
        {
            var userControl = (AdditionalMealOrderItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.FoodID = userControl.FoodID;
                entity.FoodName = userControl.FoodName;
                entity.Qty = userControl.Qty;
            }
        }

        private AddMealOrderItemDetail FindItemGrid(string foodId)
        {
            AddMealOrderItemDetailCollection coll = AddMealOrderItemDetails;
            AddMealOrderItemDetail retval = null;
            foreach (AddMealOrderItemDetail rec in coll)
            {
                if (rec.FoodID.Equals(foodId))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }
        #endregion

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var m = new ServiceUnitClassMenuExtraSetting();
            if (m.LoadByPrimaryKey(e.Value))
            {
                cboMenuID.SelectedValue = m.MenuID;
                cboClassID.SelectedValue = m.ClassID;
            }
            else
            {
                cboMenuID.SelectedValue = string.Empty;
                cboMenuID.Text = string.Empty;
                cboClassID.SelectedValue = string.Empty;
                cboClassID.Text = string.Empty;
            }
        }
    }
}
