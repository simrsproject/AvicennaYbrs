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
    public partial class UnitClassMenuExtraSettingDetail : BasePageDetail
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "UnitClassMenuExtraSettingSearch.aspx";
            UrlPageList = "UnitClassMenuExtraSettingList.aspx";

            ProgramID = AppConstant.Program.UnitClassMenuExtraSetting;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithClassInpatient(cboClassID);

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
            }
        }

        private void SetEntityValue(ServiceUnitClassMenuExtraSetting entity)
        {
            entity.ServiceUnitID = txtServiceUnitID.Text;
            entity.ClassID = cboClassID.SelectedValue;
            entity.MenuID = cboMenuID.SelectedValue;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ServiceUnitQuery("a");
            var std = new AppStandardReferenceItemQuery("b");
            que.InnerJoin(std).On(que.ServiceUnitID == std.ItemID &&
                                  std.StandardReferenceID == AppEnum.StandardReference.UnitForExtraMealOrder);
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ServiceUnitID > txtServiceUnitID.Text);
                que.OrderBy(que.ServiceUnitID.Ascending);
            }
            else
            {
                que.Where(que.ServiceUnitID < txtServiceUnitID.Text);
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

            var x = new ServiceUnitClassMenuExtraSetting();
            if (x.LoadByPrimaryKey(txtServiceUnitID.Text))
            {
                cboClassID.SelectedValue = x.ClassID;
                cboMenuID.SelectedValue = x.MenuID;
            }
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

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ServiceUnitClassMenuExtraSetting();
            entity.LoadByPrimaryKey(txtServiceUnitID.Text);
            entity.MarkAsDeleted();

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new ServiceUnitClassMenuExtraSetting();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(ServiceUnitClassMenuExtraSetting entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ServiceUnitClassMenuExtraSetting();
            if (!entity.LoadByPrimaryKey(txtServiceUnitID.Text))
                entity.AddNew();
            
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        #endregion
    }
}
