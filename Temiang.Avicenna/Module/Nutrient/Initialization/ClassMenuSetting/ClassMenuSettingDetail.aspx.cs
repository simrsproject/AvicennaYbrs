using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class ClassMenuSettingDetail : BasePageDetail
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "ClassMenuSettingSearch.aspx";
            UrlPageList = "ClassMenuSettingList.aspx";

            ProgramID = AppConstant.Program.ClassMenuSetting;
        }

        private void SetEntityValue(ClassMenuSetting entity)
        {
            entity.ClassID = txtClassID.Text;
            entity.IsOptional = chkIsOptional.Checked;
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            foreach (ClassMealSetMenuSetting item in ClassMealSetMenuSettings)
            {
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ClassQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ClassID > txtClassID.Text, que.IsActive == true, que.IsInPatientClass == true);
                que.OrderBy(que.ClassID.Ascending);
            }
            else
            {
                que.Where(que.ClassID < txtClassID.Text, que.IsActive == true, que.IsInPatientClass == true);
                que.OrderBy(que.ClassID.Descending);
            }
            var entity = new Class();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Class();
            if (parameters.Length > 0)
            {
                String id = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id);
            }
            else
                entity.LoadByPrimaryKey(txtClassID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var cls = (Class)entity;
            txtClassID.Text = cls.ClassID;
            txtClassName.Text = cls.ClassName;

            var detail = new ClassMenuSetting();
            if (cls.ClassID != null)
            {
                detail.LoadByPrimaryKey(cls.ClassID);
                chkIsOptional.Checked = detail.IsOptional ?? false;
            }

            //Display Data Detail
            PopulateGridDetail();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Class());
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
            auditLogFilter.PrimaryKeyData = "ClassID='" + txtClassID.Text.Trim() + "'";
            auditLogFilter.TableName = "Class";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new ClassMenuSetting();
            if (!entity.LoadByPrimaryKey(txtClassID.Text))
                entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(ClassMenuSetting entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                ClassMealSetMenuSettings.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ClassMenuSetting();
            if (!entity.LoadByPrimaryKey(txtClassID.Text))
                entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        #endregion

        #region Record Detail Method Function

        private ClassMealSetMenuSettingCollection ClassMealSetMenuSettings
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collClassMealSetMenuSetting"];
                    if (obj != null)
                        return ((ClassMealSetMenuSettingCollection)(obj));
                }
                var coll = new ClassMealSetMenuSettingCollection();

                var query = new ClassMealSetMenuSettingQuery("a");
                var c = new AppStandardReferenceItemQuery("b");
                query.Select
                    (
                        query,
                        c.ItemName.As("refToAppStandardReferenceItem_ItemName")
                    );
                query.InnerJoin(c).On(query.SRMealSet == c.ItemID & c.StandardReferenceID == AppEnum.StandardReference.MealSet);
                query.Where(query.ClassID == txtClassID.Text);

                coll.Load(query);

                Session["collClassMealSetMenuSetting"] = coll;
                return coll;
            }
            set
            {
                Session["collClassMealSetMenuSetting"] = value;
            }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                ClassMealSetMenuSettings = null;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            ClassMealSetMenuSettings = null; //Reset Record Detail
            grdItem.DataSource = ClassMealSetMenuSettings;
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = ClassMealSetMenuSettings;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            String id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ClassMealSetMenuSettingMetadata.ColumnNames.SRMealSet]);
            ClassMealSetMenuSetting entity = FindItemGrid(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ClassMealSetMenuSettingMetadata.ColumnNames.SRMealSet]);
            ClassMealSetMenuSetting entity = FindItemGrid(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ClassMealSetMenuSetting entity = ClassMealSetMenuSettings.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(ClassMealSetMenuSetting entity, GridCommandEventArgs e)
        {
            var userControl = (ClassMenuSettingItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ClassID = txtClassID.Text;
                entity.SRMealSet = userControl.SRMealSet;
                entity.MealSetName = userControl.MealSetName;
                entity.IsOptional = userControl.IsOptional;
            }
        }

        private ClassMealSetMenuSetting FindItemGrid(string id)
        {
            ClassMealSetMenuSettingCollection coll = ClassMealSetMenuSettings;
            ClassMealSetMenuSetting retval = null;
            foreach (ClassMealSetMenuSetting rec in coll)
            {
                if (rec.SRMealSet.Equals(id))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        #endregion
    }
}
