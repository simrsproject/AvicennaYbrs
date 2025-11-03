using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class MealOrderOprDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private string RegNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List 
            UrlPageSearch = "#";
            UrlPageList = "MealOrderOprList.aspx";

            ProgramID = AppConstant.Program.MealOrderOutpatient;

            if (!IsPostBack)
            {
                MealOrderItems = null;
                MealOrderItem02s = null;
                MealOrderItem03s = null;
            }

        }

        private void PopulateRegistrationInformation(string registrationNo)
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(registrationNo))
            {
                txtRegistrationNo.Text = reg.RegistrationNo;
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.str.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;
                txtGender.Text = pat.Sex;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));
                txtAgeInYear.Text = Convert.ToString(reg.AgeInYear);
                txtAgeInMonth.Text = Convert.ToString(reg.AgeInMonth);
                txtAgeInDay.Text = Convert.ToString(reg.AgeInDay);

                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);
                txtPhysicianName.Text = par.ParamedicName;
                txtServiceUnitID.Text = reg.ServiceUnitID;
                var unit = new ServiceUnit();
                if (unit.LoadByPrimaryKey(reg.ServiceUnitID))
                    txtServiceUnitName.Text = unit.ServiceUnitName;
                else txtServiceUnitName.Text = string.Empty;
                txtClassID.Text = reg.ChargeClassID;
                var guar = new Guarantor();
                if (guar.LoadByPrimaryKey(reg.GuarantorID))
                    txtGuarantorName.Text = guar.GuarantorName;
                else txtGuarantorName.Text = string.Empty;
            }
            else
            {
                txtRegistrationNo.Text = string.Empty;
                txtMedicalNo.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtGender.Text = string.Empty;
                txtSalutation.Text = string.Empty;
                txtPlaceDOB.Text = string.Empty;
                txtAgeInYear.Text = string.Empty;
                txtAgeInMonth.Text = string.Empty;
                txtAgeInDay.Text = string.Empty;

                txtPhysicianName.Text = string.Empty;
                txtServiceUnitID.Text = string.Empty;
                txtServiceUnitName.Text = string.Empty;
                txtClassID.Text = reg.ChargeClassID;
                txtGuarantorName.Text = string.Empty;
            }
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            //printJobParameters.AddNew("TransactionNo", txtTransactionNo.Text);
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new MealOrder());

            PopulateRegistrationInformation(string.IsNullOrEmpty(txtRegistrationNo.Text)
                                                    ? RegNo
                                                    : txtRegistrationNo.Text);

            txtOrderNo.Text = GetNewTransactionNo();
            txtOrderDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new MealOrder();
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
            var entity = new MealOrder();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new MealOrder();
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
            auditLogFilter.TableName = "MealOrder";
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new MealOrder();
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
            var entity = new MealOrder();
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
            if (AppSession.Parameter.IsUsingMealOrderVerification)
            {
                if (entity.IsVerified ?? false)
                {
                    args.MessageText = "This data has been verified.";
                    args.IsCancel = true;
                    return;
                }
            }

            SetApproved(entity, false);
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new MealOrder();
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

            SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
        }

        private bool IsApprovedOrVoid(MealOrder entity, ValidateArgs args)
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

        private void SetApproved(MealOrder entity, bool isApproved)
        {
            using (var trans = new esTransactionScope())
            {
                //header
                entity.IsApproved = isApproved;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void SetVoid(MealOrder entity, bool isVoid)
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
            RefreshCommandItemMealOrder(oldVal, newVal);
            RefreshCommandItemMealOrder02(oldVal, newVal);
            RefreshCommandItemMealOrder03(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new MealOrder();
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
            var mealOrder = (MealOrder)entity;
            txtOrderNo.Text = mealOrder.OrderNo;
            txtOrderDate.SelectedDate = mealOrder.OrderDate;
            txtRegistrationNo.Text = mealOrder.RegistrationNo;

            PopulateRegistrationInformation(txtRegistrationNo.Text);

            ViewState["IsVoid"] = mealOrder.IsVoid ?? false;
            ViewState["IsApproved"] = mealOrder.IsApproved ?? false;

            //Display Data Detail
            PopulateMealOrderItemGrid();
            PopulateMealOrderItem02Grid();
            PopulateMealOrderItem03Grid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(MealOrder entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtOrderNo.Text = GetNewTransactionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            entity.OrderNo = txtOrderNo.Text;
            entity.OrderDate = txtOrderDate.SelectedDate;
            entity.EffectiveDate = txtOrderDate.SelectedDate;
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.ServiceUnitID = txtServiceUnitID.Text;
            entity.ClassID = txtClassID.Text;
            entity.BedID = string.Empty;
            entity.DietPatientNo = string.Empty;
            entity.DietID = string.Empty;
            entity.MenuID = string.Empty;
            entity.MenuItemID = string.Empty;
            entity.FastingTime = string.Empty;
            entity.IsApproved = false;
            entity.IsVoid = false;
            entity.IsOpr = true;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            //Last Update Status Detail
            foreach (var item in MealOrderItems)
            {
                item.OrderNo = entity.OrderNo;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in MealOrderItem02s)
            {
                item.OrderNo = entity.OrderNo;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in MealOrderItem03s)
            {
                item.OrderNo = entity.OrderNo;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(MealOrder entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                MealOrderItems.Save();
                MealOrderItem02s.Save();
                MealOrderItem03s.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new MealOrderQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.OrderNo > txtOrderNo.Text, que.IsOpr == true);
                que.OrderBy(que.OrderNo.Ascending);
            }
            else
            {
                que.Where(que.OrderNo < txtOrderNo.Text, que.IsOpr == true);
                que.OrderBy(que.OrderNo.Descending);
            }

            var entity = new MealOrder();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer(), AppEnum.AutoNumber.MealOrderOprNo);
            return _autoNumber.LastCompleteNumber;
        }

        #endregion

        #region Record Detail Method Function MealOrderItem

        private MealOrderItemCollection MealOrderItems
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collMealOrderOprItem" + Request.UserHostName];
                    if (obj != null)
                        return ((MealOrderItemCollection)(obj));
                }

                var coll = new MealOrderItemCollection();
                var query = new MealOrderItemQuery("a");
                var foodQ = new FoodQuery("b");
                var stdQ = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query,
                        foodQ.FoodName.As("refToFood_FoodName"),
                        foodQ.SRFoodGroup1.As("refToFood_FoodGroupID"),
                        stdQ.ItemName.As("refToFood_FoodGroupName")
                        );

                query.InnerJoin(foodQ).On(query.FoodID == foodQ.FoodID);
                query.InnerJoin(stdQ).On(foodQ.SRFoodGroup1 == stdQ.ItemID &
                                         stdQ.StandardReferenceID == AppEnum.StandardReference.FoodGroup1);
                query.Where(query.OrderNo == txtOrderNo.Text, query.SRMealSet == "01");

                query.OrderBy(foodQ.SRFoodGroup1.Ascending, query.FoodID.Ascending);
                coll.Load(query);

                Session["collMealOrderOprItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collMealOrderOprItem" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemMealOrder(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);

            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            if (oldVal != AppEnum.DataMode.Read)
                MealOrderItems = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdItem.Rebind();
        }

        private void PopulateMealOrderItemGrid()
        {
            //Display Data Detail
            MealOrderItems = null; //Reset Record Detail
            grdItem.DataSource = MealOrderItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = MealOrderItems;
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            var foodId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][MealOrderItemMetadata.ColumnNames.FoodID]);
            var entity = FindMealOrderItem(foodId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            MealOrderItem entity = MealOrderItems.AddNew();
            SetEntityValue(entity, e);

            //grid not close first
            e.Canceled = true;
            grdItem.Rebind();
        }

        private MealOrderItem FindMealOrderItem(String foodId)
        {
            return MealOrderItems.FirstOrDefault(rec => rec.FoodID.Equals(foodId));
        }

        private void SetEntityValue(MealOrderItem entity, GridCommandEventArgs e)
        {
            var userControl = (MealOrderOprItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRMealSet = "01";
                entity.FoodID = userControl.FoodID;
                entity.FoodName = userControl.FoodName;

                var f = new Food();
                f.LoadByPrimaryKey(entity.FoodID);

                var fg = new AppStandardReferenceItem();
                if (fg.LoadByPrimaryKey("FoodGroup1", f.SRFoodGroup1))
                    entity.FoodGroupName = fg.ItemName;

                entity.IsCustom = true;
                entity.IsOptional = true;

                entity.DietPatientNo = string.Empty;
                entity.DietID = string.Empty;
                entity.MenuID = string.Empty;
                entity.MenuItemID = string.Empty;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        #endregion

        #region Record Detail Method Function MealOrderItem - Lunch

        private MealOrderItemCollection MealOrderItem02s
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collMealOrderOprItem02" + Request.UserHostName];
                    if (obj != null)
                        return ((MealOrderItemCollection)(obj));
                }

                var coll = new MealOrderItemCollection();
                var query = new MealOrderItemQuery("a");
                var foodQ = new FoodQuery("b");
                var stdQ = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query,
                        foodQ.FoodName.As("refToFood_FoodName"),
                        foodQ.SRFoodGroup1.As("refToFood_FoodGroupID"),
                        stdQ.ItemName.As("refToFood_FoodGroupName")
                        );

                query.InnerJoin(foodQ).On(query.FoodID == foodQ.FoodID);
                query.InnerJoin(stdQ).On(foodQ.SRFoodGroup1 == stdQ.ItemID &
                                         stdQ.StandardReferenceID == AppEnum.StandardReference.FoodGroup1);
                query.Where(query.OrderNo == txtOrderNo.Text, query.SRMealSet == "02");

                query.OrderBy(foodQ.SRFoodGroup1.Ascending, query.FoodID.Ascending);
                coll.Load(query);

                Session["collMealOrderOprItem02" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collMealOrderOprItem02" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemMealOrder02(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);

            grdItem02.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            grdItem02.Columns[grdItem02.Columns.Count - 1].Visible = isVisible;

            if (oldVal != AppEnum.DataMode.Read)
                MealOrderItem02s = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdItem02.Rebind();
        }

        private void PopulateMealOrderItem02Grid()
        {
            //Display Data Detail
            MealOrderItem02s = null; //Reset Record Detail
            grdItem02.DataSource = MealOrderItem02s; //Requery
            grdItem02.MasterTableView.IsItemInserted = false;
            grdItem02.MasterTableView.ClearEditItems();
            grdItem02.DataBind();
        }

        protected void grdItem02_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem02.DataSource = MealOrderItem02s;
        }

        protected void grdItem02_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            var foodId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][MealOrderItemMetadata.ColumnNames.FoodID]);
            var entity = FindMealOrderItem02(foodId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem02_InsertCommand(object source, GridCommandEventArgs e)
        {
            MealOrderItem entity = MealOrderItem02s.AddNew();
            SetEntityValue02(entity, e);

            //grid not close first
            e.Canceled = true;
            grdItem02.Rebind();
        }

        private MealOrderItem FindMealOrderItem02(String foodId)
        {
            return MealOrderItem02s.FirstOrDefault(rec => rec.FoodID.Equals(foodId));
        }

        private void SetEntityValue02(MealOrderItem entity, GridCommandEventArgs e)
        {
            var userControl = (MealOrderOprItemDetail02)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRMealSet = "02";
                entity.FoodID = userControl.FoodID;
                entity.FoodName = userControl.FoodName;

                var f = new Food();
                f.LoadByPrimaryKey(entity.FoodID);

                var fg = new AppStandardReferenceItem();
                if (fg.LoadByPrimaryKey("FoodGroup1", f.SRFoodGroup1))
                    entity.FoodGroupName = fg.ItemName;

                entity.IsCustom = true;
                entity.IsOptional = true;

                entity.DietPatientNo = string.Empty;
                entity.DietID = string.Empty;
                entity.MenuID = string.Empty;
                entity.MenuItemID = string.Empty;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        #endregion

        #region Record Detail Method Function MealOrderItem - Dinner

        private MealOrderItemCollection MealOrderItem03s
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collMealOrderOprItem03" + Request.UserHostName];
                    if (obj != null)
                        return ((MealOrderItemCollection)(obj));
                }

                var coll = new MealOrderItemCollection();
                var query = new MealOrderItemQuery("a");
                var foodQ = new FoodQuery("b");
                var stdQ = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query,
                        foodQ.FoodName.As("refToFood_FoodName"),
                        foodQ.SRFoodGroup1.As("refToFood_FoodGroupID"),
                        stdQ.ItemName.As("refToFood_FoodGroupName")
                        );

                query.InnerJoin(foodQ).On(query.FoodID == foodQ.FoodID);
                query.InnerJoin(stdQ).On(foodQ.SRFoodGroup1 == stdQ.ItemID &
                                         stdQ.StandardReferenceID == AppEnum.StandardReference.FoodGroup1);
                query.Where(query.OrderNo == txtOrderNo.Text, query.SRMealSet == "03");

                query.OrderBy(foodQ.SRFoodGroup1.Ascending, query.FoodID.Ascending);
                coll.Load(query);

                Session["collMealOrderOprItem03" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collMealOrderOprItem03" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemMealOrder03(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);

            grdItem03.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            grdItem03.Columns[grdItem03.Columns.Count - 1].Visible = isVisible;

            if (oldVal != AppEnum.DataMode.Read)
                MealOrderItem03s = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdItem03.Rebind();
        }

        private void PopulateMealOrderItem03Grid()
        {
            //Display Data Detail
            MealOrderItem03s = null; //Reset Record Detail
            grdItem03.DataSource = MealOrderItem03s; //Requery
            grdItem03.MasterTableView.IsItemInserted = false;
            grdItem03.MasterTableView.ClearEditItems();
            grdItem03.DataBind();
        }

        protected void grdItem03_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem03.DataSource = MealOrderItem03s;
        }

        protected void grdItem03_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            var foodId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][MealOrderItemMetadata.ColumnNames.FoodID]);
            var entity = FindMealOrderItem03(foodId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem03_InsertCommand(object source, GridCommandEventArgs e)
        {
            MealOrderItem entity = MealOrderItem03s.AddNew();
            SetEntityValue03(entity, e);

            //grid not close first
            e.Canceled = true;
            grdItem03.Rebind();
        }

        private MealOrderItem FindMealOrderItem03(String foodId)
        {
            return MealOrderItem03s.FirstOrDefault(rec => rec.FoodID.Equals(foodId));
        }

        private void SetEntityValue03(MealOrderItem entity, GridCommandEventArgs e)
        {
            var userControl = (MealOrderOprItemDetail03)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRMealSet = "03";
                entity.FoodID = userControl.FoodID;
                entity.FoodName = userControl.FoodName;

                var f = new Food();
                f.LoadByPrimaryKey(entity.FoodID);

                var fg = new AppStandardReferenceItem();
                if (fg.LoadByPrimaryKey("FoodGroup1", f.SRFoodGroup1))
                    entity.FoodGroupName = fg.ItemName;

                entity.IsCustom = true;
                entity.IsOptional = true;

                entity.DietPatientNo = string.Empty;
                entity.DietID = string.Empty;
                entity.MenuID = string.Empty;
                entity.MenuItemID = string.Empty;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        #endregion
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
        }
    }
}