using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class DietDetail : BasePageDetail
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "DietSearch.aspx";
            UrlPageList = "DietList.aspx";

            ProgramID = AppConstant.Program.Diet;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRDietType, AppEnum.StandardReference.DietType);
            }
        }

        private void SetEntityValue(Diet entity)
        {
            entity.DietID = txtDietID.Text;
            entity.DietName = txtDietName.Text;
            entity.SRDietType = cboSRDietType.SelectedValue;
            entity.Calorie = Convert.ToDecimal(txtCalorie.Value);
            entity.CalorieMin = Convert.ToDecimal(txtCalorieMin.Value);
            entity.CalorieMax = Convert.ToDecimal(txtCalorieMax.Value);
            entity.CalorieInterval = Convert.ToDecimal(txtCalorieInterval.Value);
            entity.Protein = Convert.ToDecimal(txtProtein.Value);
            entity.ProteinMin = Convert.ToDecimal(txtProteinMin.Value);
            entity.ProteinMax = Convert.ToDecimal(txtProteinMax.Value);
            entity.ProteinInterval = Convert.ToDecimal(txtProteinInterval.Value);
            entity.Fat = Convert.ToDecimal(txtFat.Value);
            entity.FatMin = Convert.ToDecimal(txtFatMin.Value);
            entity.FatMax = Convert.ToDecimal(txtFatMax.Value);
            entity.FatInterval = Convert.ToDecimal(txtFatInterval.Value);
            entity.Carbohydrate = Convert.ToDecimal(txtCarbohydrate.Value);
            entity.CarbohydrateMin = Convert.ToDecimal(txtCarbohydrateMin.Value);
            entity.CarbohydrateMax = Convert.ToDecimal(txtCarbohydrateMax.Value);
            entity.CarbohydrateInterval = Convert.ToDecimal(txtCarbohydrateInterval.Value);
            entity.Salt = Convert.ToDecimal(txtSalt.Value);
            entity.SaltMin = Convert.ToDecimal(txtSaltMin.Value);
            entity.SaltMax = Convert.ToDecimal(txtSaltMax.Value);
            entity.SaltInterval = Convert.ToDecimal(txtSaltInterval.Value);
            entity.Fiber = Convert.ToDecimal(txtFiber.Value);
            entity.FiberMin = Convert.ToDecimal(txtFiberMin.Value);
            entity.FiberMax = Convert.ToDecimal(txtFiberMax.Value);
            entity.FiberInterval = Convert.ToDecimal(txtFiberInterval.Value);
            entity.PriorityNo = Convert.ToInt16(txtPriorityNo.Value);
            entity.IsGetSnack = chkIsGetSnack.Checked;
            entity.IsActive = chkIsActive.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Update Key
            DietComplicationCollection coll = DietComplications;
            foreach (DietComplication item in coll)
            {
                item.DietID = txtDietID.Text;
            }

            DietMenuCollection collMenu = DietMenus;
            foreach (DietMenu item in collMenu)
            {
                item.DietID = txtDietID.Text;
                item.LastUpdateDateTime = DateTime.Now;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new DietQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.DietID > txtDietID.Text);
                que.OrderBy(que.DietID.Ascending);
            }
            else
            {
                que.Where(que.DietID < txtDietID.Text);
                que.OrderBy(que.DietID.Descending);
            }
            var entity = new Diet();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Diet();
            if (parameters.Length > 0)
            {
                String dietId = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(dietId);
            }
            else
                entity.LoadByPrimaryKey(txtDietID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var diet = (Diet)entity;
            txtDietID.Text = diet.DietID;
            txtDietName.Text = diet.DietName;
            cboSRDietType.SelectedValue = diet.SRDietType;
            txtCalorie.Value = Convert.ToDouble(diet.Calorie);
            txtCalorieMin.Value = Convert.ToDouble(diet.CalorieMin);
            txtCalorieMax.Value = Convert.ToDouble(diet.CalorieMax);
            txtCalorieInterval.Value = Convert.ToDouble(diet.CalorieInterval);
            txtProtein.Value = Convert.ToDouble(diet.Protein);
            txtProteinMin.Value = Convert.ToDouble(diet.ProteinMin);
            txtProteinMax.Value = Convert.ToDouble(diet.ProteinMax);
            txtProteinInterval.Value = Convert.ToDouble(diet.ProteinInterval);
            txtFat.Value = Convert.ToDouble(diet.Fat);
            txtFatMin.Value = Convert.ToDouble(diet.FatMin);
            txtFatMax.Value = Convert.ToDouble(diet.FatMax);
            txtFatInterval.Value = Convert.ToDouble(diet.FatInterval);
            txtCarbohydrate.Value = Convert.ToDouble(diet.Carbohydrate);
            txtCarbohydrateMin.Value = Convert.ToDouble(diet.CarbohydrateMin);
            txtCarbohydrateMax.Value = Convert.ToDouble(diet.CarbohydrateMax);
            txtCarbohydrateInterval.Value = Convert.ToDouble(diet.CarbohydrateInterval);
            txtSalt.Value = Convert.ToDouble(diet.Salt);
            txtSaltMin.Value = Convert.ToDouble(diet.SaltMin);
            txtSaltMax.Value = Convert.ToDouble(diet.SaltMax);
            txtSaltInterval.Value = Convert.ToDouble(diet.SaltInterval);
            txtFiber.Value = Convert.ToDouble(diet.Fiber);
            txtFiberMin.Value = Convert.ToDouble(diet.FiberMin);
            txtFiberMax.Value = Convert.ToDouble(diet.FiberMax);
            txtFiberInterval.Value = Convert.ToDouble(diet.FiberInterval);
            txtPriorityNo.Value = diet.PriorityNo;
            chkIsGetSnack.Checked = diet.IsGetSnack ?? false;
            chkIsActive.Checked = diet.IsActive ?? false;
            
            //Display Data Detail
            PopulateGridDetail();
            PopulateGridMenuDetail();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Diet());
            chkIsActive.Checked = true;
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
            auditLogFilter.PrimaryKeyData = "DietID='" + txtDietID.Text.Trim() + "'";
            auditLogFilter.TableName = "Diet";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtDietID.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItemGrid(oldVal, newVal);
            RefreshCommandItemMenuGrid(oldVal, newVal);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new Diet();
            if (entity.LoadByPrimaryKey(txtDietID.Text))
            {
                var dpiColl = new DietPatientItemCollection();
                dpiColl.Query.Where(dpiColl.Query.DietID == txtDietID.Text);
                dpiColl.LoadAll();
                if (dpiColl.Count > 0)
                {
                    args.MessageText = AppConstant.Message.RecordHasUsed;
                    args.IsCancel = true;
                    return;
                }

                entity.MarkAsDeleted();

                var coll = new DietComplicationCollection();
                coll.Query.Where(coll.Query.DietID == txtDietID.Text);
                coll.MarkAllAsDeleted();

                var collMenu = new DietMenuCollection();
                collMenu.Query.Where(coll.Query.DietID == txtDietID.Text);
                collMenu.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    coll.Save();
                    collMenu.Save();
                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var e = new Diet();
            if (e.LoadByPrimaryKey(txtDietID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            var entity = new Diet();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(Diet entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                DietComplications.Save();
                DietMenus.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new Diet();
            if (entity.LoadByPrimaryKey(txtDietID.Text))
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

        #endregion

        #region Record Detail Method Function

        #region Diet Complication
        private DietComplicationCollection DietComplications
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collDietComplication"];
                    if (obj != null)
                        return ((DietComplicationCollection)(obj));
                }

                var coll = new DietComplicationCollection();
                var query = new DietComplicationQuery("a");
                var dietQ = new DietQuery("b");

                string dietId = txtDietID.Text;
                query.Select
                    (
                        query,
                        dietQ.DietName.As("refToDiet_DietName")
                    );
                query.InnerJoin(dietQ).On(query.DietComplicationID == dietQ.DietID);
                query.Where(query.DietID == dietId);
                query.OrderBy(query.DietComplicationID.Ascending);
                coll.Load(query);

                Session["collDietComplication"] = coll;
                return coll;
            }
            set { Session["collDietComplication"] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdDietComplication.Columns[0].Visible = isVisible;

            grdDietComplication.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                DietComplications = null;

            //Perbaharui tampilan dan data
            grdDietComplication.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            DietComplications = null; //Reset Record Detail
            grdDietComplication.DataSource = DietComplications;
            grdDietComplication.DataBind();
        }

        protected void grdDietComplication_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDietComplication.DataSource = DietComplications;
        }

        protected void grdDietComplication_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            String dietId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][DietComplicationMetadata.ColumnNames.DietComplicationID]);
            DietComplication entity = FindItemGrid(dietId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdDietComplication_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String dietId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][DietComplicationMetadata.ColumnNames.DietComplicationID]);
            DietComplication entity = FindItemGrid(dietId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdDietComplication_InsertCommand(object source, GridCommandEventArgs e)
        {
            DietComplication entity = DietComplications.AddNew();
            SetEntityValue(entity, e);
        }

        private void SetEntityValue(DietComplication entity, GridCommandEventArgs e)
        {
            var userControl = (DietComplicationDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.DietID = txtDietID.Text;
                entity.DietComplicationID = userControl.DietID;
                entity.DietComplicationName = userControl.DietName;
                entity.IsActive = userControl.IsActive;
            }
        }

        private DietComplication FindItemGrid(string dietId)
        {
            DietComplicationCollection coll = DietComplications;
            DietComplication retval = null;
            foreach (DietComplication rec in coll)
            {
                if (rec.DietComplicationID.Equals(dietId))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }
        #endregion

        #region Diet Menu
        private DietMenuCollection DietMenus
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collDietMenu"];
                    if (obj != null)
                        return ((DietMenuCollection)(obj));
                }

                var coll = new DietMenuCollection();
                var query = new DietMenuQuery("a");
                var fofq = new AppStandardReferenceItemQuery("b");
                var mq = new MenuQuery("c");

                string dietId = txtDietID.Text;
                query.Select
                    (
                        query,
                        fofq.ItemName.As("refToStdReff_FormOfFoodName"),
                        mq.MenuName.As("refToMenu_MenuName")
                    );
                query.InnerJoin(fofq).On(query.FormOfFood == fofq.ItemID &&
                                         fofq.StandardReferenceID == AppEnum.StandardReference.FormOfFood);
                query.InnerJoin(mq).On(query.MenuID == mq.MenuID);
                query.Where(query.DietID == dietId);
                query.OrderBy(query.FormOfFood.Ascending);
                coll.Load(query);

                Session["collDietMenu"] = coll;
                return coll;
            }
            set { Session["collDietMenu"] = value; }
        }

        private void RefreshCommandItemMenuGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdDietMenu.Columns[0].Visible = isVisible;

            grdDietMenu.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                DietMenus = null;

            //Perbaharui tampilan dan data
            grdDietMenu.Rebind();
        }

        private void PopulateGridMenuDetail()
        {
            //Display Data Detail
            DietMenus = null; //Reset Record Detail
            grdDietMenu.DataSource = DietMenus;
            grdDietMenu.DataBind();
        }

        protected void grdDietMenu_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDietMenu.DataSource = DietMenus;
        }

        protected void grdDietMenu_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            String id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][DietMenuMetadata.ColumnNames.FormOfFood]);
            DietMenu entity = FindItemMenuGrid(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdDietMenu_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][DietMenuMetadata.ColumnNames.FormOfFood]);
            DietMenu entity = FindItemMenuGrid(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdDietMenu_InsertCommand(object source, GridCommandEventArgs e)
        {
            DietMenu entity = DietMenus.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdDietMenu.Rebind();
        }

        private void SetEntityValue(DietMenu entity, GridCommandEventArgs e)
        {
            var userControl = (DietMenuDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.DietID = txtDietID.Text;
                entity.FormOfFood = userControl.FormOfFood;
                entity.FormOfFoodName = userControl.FormOfFoodName;
                entity.MenuID = userControl.MenuID;
                entity.MenuName = userControl.MenuName;
                entity.IsActive = userControl.IsActive;
            }
        }

        private DietMenu FindItemMenuGrid(string id)
        {
            DietMenuCollection coll = DietMenus;
            DietMenu retval = null;
            foreach (DietMenu rec in coll)
            {
                if (rec.FormOfFood.Equals(id))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }
        #endregion

        #endregion
    }
}
