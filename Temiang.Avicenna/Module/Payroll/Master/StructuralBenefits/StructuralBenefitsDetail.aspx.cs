using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class StructuralBenefitsDetail : BasePageDetail
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "StructuralBenefitsSearch.aspx";
            UrlPageList = "StructuralBenefitsList.aspx";

            ProgramID = AppConstant.Program.StructuralBenefits;
        }

        private void SetEntityValue()
        {
            StructuralBenefitsCollection coll = StructuralBenefitss;
            foreach (StructuralBenefits item in coll)
            {
                item.OrganizationUnitID = txtOrganizationUnitID.Text.ToInt();
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new OrganizationUnitQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.OrganizationUnitID > txtOrganizationUnitID.Text.ToInt(), que.IsActive == true, que.SROrganizationLevel == "1");
                que.OrderBy(que.OrganizationUnitID.Ascending);
            }
            else
            {
                que.Where(que.OrganizationUnitID < txtOrganizationUnitID.Text.ToInt(), que.IsActive == true, que.SROrganizationLevel == "1");
                que.OrderBy(que.OrganizationUnitID.Descending);
            }
            var entity = new OrganizationUnit();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new OrganizationUnit();
            if (parameters.Length > 0)
            {
                String id = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id.ToInt());
            }
            else
                entity.LoadByPrimaryKey(txtOrganizationUnitID.Text.ToInt());
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var unit = (OrganizationUnit)entity;
            txtOrganizationUnitID.Text = unit.OrganizationUnitID.ToString();
            txtOrganizationUnitCode.Text = unit.OrganizationUnitCode;
            txtOrganizationUnitName.Text = unit.OrganizationUnitName;

            //Display Data Detail
            PopulateGridDetail();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new OrganizationUnit());
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
            //auditLogFilter.PrimaryKeyData = "OrganizationUnitID='" + txtOrganizationUnitID.Text.Trim() + "'";
            //auditLogFilter.TableName = "OrganizationUnit";
        }

        protected override void OnDataModeChanged(Common.AppEnum.DataMode oldVal, Common.AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var coll = new StructuralBenefitsCollection();
            coll.Query.Where(coll.Query.OrganizationUnitID == txtOrganizationUnitID.Text.ToInt());
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
            SetEntityValue();
            SaveEntity();
        }

        private void SaveEntity()
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                StructuralBenefitss.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new OrganizationUnit();
            if (entity.LoadByPrimaryKey(txtOrganizationUnitID.Text.ToInt()))
            {
                SetEntityValue();
                SaveEntity();
            }
        }

        #endregion

        #region Record Detail Method Function

        private StructuralBenefitsCollection StructuralBenefitss
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collStructuralBenefits"];
                    if (obj != null)
                        return ((StructuralBenefitsCollection)(obj));
                }

                var coll = new StructuralBenefitsCollection();
                var query = new StructuralBenefitsQuery("a");
                var positionQ = new PositionQuery("b");

                query.Select
                    (
                        query,
                        positionQ.PositionName.As("refToPosition_PositionName")
                    );
                query.InnerJoin(positionQ).On(query.PositionID == positionQ.PositionID);
                query.Where(query.OrganizationUnitID == txtOrganizationUnitID.Text);
                query.OrderBy(query.PositionID.Ascending, query.ValidFrom.Ascending);
                coll.Load(query);

                Session["collStructuralBenefits"] = coll;
                return coll;
            }
            set { Session["collStructuralBenefits"] = value; }
        }

        private void RefreshCommandItemGrid(Common.AppEnum.DataMode oldVal, Common.AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != Common.AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != Common.AppEnum.DataMode.Read)
                StructuralBenefitss = null;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            StructuralBenefitss = null; //Reset Record Detail
            grdItem.DataSource = StructuralBenefitss;
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = StructuralBenefitss;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            String positionId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][StructuralBenefitsMetadata.ColumnNames.PositionID]);
            DateTime validFrom = Convert.ToDateTime(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][StructuralBenefitsMetadata.ColumnNames.ValidFrom]);
            StructuralBenefits entity = FindItemGrid(positionId, validFrom);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String positionId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][StructuralBenefitsMetadata.ColumnNames.PositionID]);
            DateTime validFrom = Convert.ToDateTime(item.OwnerTableView.DataKeyValues[item.ItemIndex][StructuralBenefitsMetadata.ColumnNames.ValidFrom]);
            StructuralBenefits entity = FindItemGrid(positionId, validFrom);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            StructuralBenefits entity = StructuralBenefitss.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(StructuralBenefits entity, GridCommandEventArgs e)
        {
            var userControl = (StructuralBenefitsItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.OrganizationUnitID = txtOrganizationUnitID.Text.ToInt();
                entity.PositionID = userControl.PositionID;
                entity.PositionName = userControl.PositionName;
                entity.ValidFrom = userControl.ValidFrom;
                entity.Amount = userControl.Amount;
            }
        }

        private StructuralBenefits FindItemGrid(string positionId, DateTime validFrom)
        {
            StructuralBenefitsCollection coll = StructuralBenefitss;
            StructuralBenefits retval = null;
            foreach (StructuralBenefits rec in coll)
            {
                if (rec.PositionID.Equals(positionId.ToInt()) && rec.ValidFrom.Value.Date.Equals(validFrom.Date))
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
