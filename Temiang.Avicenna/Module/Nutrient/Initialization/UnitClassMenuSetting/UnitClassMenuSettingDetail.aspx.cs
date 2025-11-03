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
    public partial class UnitClassMenuSettingDetail : BasePageDetail
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "UnitClassMenuSettingSearch.aspx";
            UrlPageList = "UnitClassMenuSettingList.aspx";

            ProgramID = AppConstant.Program.UnitClassMenuSetting;
        }

        private void SetEntityValue()
        {
            ServiceUnitClassMenuSettingCollection coll = ServiceUnitClassMenuSettings;
            foreach (ServiceUnitClassMenuSetting item in coll)
            {
                item.ServiceUnitID = txtServiceUnitID.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ServiceUnitQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ServiceUnitID > txtServiceUnitID.Text, que.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                que.OrderBy(que.ServiceUnitID.Ascending);
            }
            else
            {
                que.Where(que.ServiceUnitID < txtServiceUnitID.Text, que.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                que.OrderBy(que.ServiceUnitID.Descending);
            }
            var entity = new ServiceUnit();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ServiceUnit();
            if (parameters.Length > 0)
            {
                String unitId = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(unitId);
            }
            else
                entity.LoadByPrimaryKey(txtServiceUnitID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var unit = (ServiceUnit)entity;
            txtServiceUnitID.Text = unit.ServiceUnitID;
            txtServiceUnitName.Text = unit.ServiceUnitName;

            //Display Data Detail
            PopulateGridDetail();

            var x = ServiceUnitClassMealSetMenuSettings;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ServiceUnit());
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
            auditLogFilter.PrimaryKeyData = "ServiceUnitID='" + txtServiceUnitID.Text.Trim() + "'";
            auditLogFilter.TableName = "ServiceUnit";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var coll = new ServiceUnitClassMenuSettingCollection();
            coll.Query.Where(coll.Query.ServiceUnitID == txtServiceUnitID.Text);
            coll.MarkAllAsDeleted();

            using (esTransactionScope trans = new esTransactionScope())
            {
                coll.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new ServiceUnit();
            
            SetEntityValue();
            SaveEntity();
        }

        private void SaveEntity()
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                ServiceUnitClassMenuSettings.Save();
                ServiceUnitClassMealSetMenuSettings.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ServiceUnit();
            if (entity.LoadByPrimaryKey(txtServiceUnitID.Text))
            {
                SetEntityValue();
                SaveEntity();
            }
        }

        #endregion

        #region Record Detail Method Function

        private ServiceUnitClassMenuSettingCollection ServiceUnitClassMenuSettings
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collServiceUnitClassMenuSetting"];
                    if (obj != null)
                        return ((ServiceUnitClassMenuSettingCollection)(obj));
                }

                var coll = new ServiceUnitClassMenuSettingCollection();
                var query = new ServiceUnitClassMenuSettingQuery("a");
                var classQ = new ClassQuery("b");

                query.Select
                    (
                        query,
                        classQ.ClassName.As("refToClass_ClassName")
                    );
                query.InnerJoin(classQ).On(query.ClassID == classQ.ClassID && classQ.IsActive == true);
                query.Where(query.ServiceUnitID == txtServiceUnitID.Text);
                query.OrderBy(query.ClassID.Ascending);
                coll.Load(query);

                Session["collServiceUnitClassMenuSetting"] = coll;
                return coll;
            }
            set { Session["collServiceUnitClassMenuSetting"] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 2].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                ServiceUnitClassMenuSettings = null;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            ServiceUnitClassMenuSettings = null; //Reset Record Detail
            grdItem.DataSource = ServiceUnitClassMenuSettings;
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = ServiceUnitClassMenuSettings;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            String classId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ServiceUnitClassMenuSettingMetadata.ColumnNames.ClassID]);
            ServiceUnitClassMenuSetting entity = FindItemGrid(classId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String classId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ServiceUnitClassMenuSettingMetadata.ColumnNames.ClassID]);
            ServiceUnitClassMenuSetting entity = FindItemGrid(classId);
            if (entity != null)
                entity.MarkAsDeleted();

            ServiceUnitClassMealSetMenuSettingCollection mealSetColl = ServiceUnitClassMealSetMenuSettings;
            foreach (ServiceUnitClassMealSetMenuSetting mealSet in mealSetColl.Where(mealSet => mealSet.ClassID.Equals(classId)))
            {
                mealSet.MarkAsDeleted();
            }
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ServiceUnitClassMenuSetting entity = ServiceUnitClassMenuSettings.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(ServiceUnitClassMenuSetting entity, GridCommandEventArgs e)
        {
            var userControl = (ServiceUnitClassMenuSettingItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ServiceUnitID = txtServiceUnitID.Text;
                entity.ClassID = userControl.ClassID;
                entity.ClassName = userControl.ClassName;
                entity.IsOptional = userControl.IsOptional;
            }
        }

        private ServiceUnitClassMenuSetting FindItemGrid(string classId)
        {
            ServiceUnitClassMenuSettingCollection coll = ServiceUnitClassMenuSettings;
            ServiceUnitClassMenuSetting retval = null;
            foreach (ServiceUnitClassMenuSetting rec in coll)
            {
                if (rec.ClassID.Equals(classId))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        #endregion

        private ServiceUnitClassMealSetMenuSettingCollection ServiceUnitClassMealSetMenuSettings
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collServiceUnitClassMealSetMenuSetting"];
                    if (obj != null)
                        return ((ServiceUnitClassMealSetMenuSettingCollection)(obj));
                }
                var coll = new ServiceUnitClassMealSetMenuSettingCollection();

                var query = new ServiceUnitClassMealSetMenuSettingQuery("a");
                var c = new AppStandardReferenceItemQuery("b");
                query.Select
                    (
                        query,
                        c.ItemName.As("refToAppStandardReferenceItem_ItemName")
                    );
                query.InnerJoin(c).On(query.SRMealSet == c.ItemID & c.StandardReferenceID == AppEnum.StandardReference.MealSet);
                query.Where(query.ServiceUnitID == txtServiceUnitID.Text);

                coll.Load(query);

                Session["collServiceUnitClassMealSetMenuSetting"] = coll;
                return coll;
            }
            set
            {
                Session["collServiceUnitClassMealSetMenuSetting"] = value;
            }
        }
    }
}
