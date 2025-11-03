using System;
using System.Data;

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;


namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class OvertimeFormulaDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "OvertimeFormulaSearch.aspx";
            UrlPageList = "OvertimeFormulaList.aspx";

            ProgramID = AppConstant.Program.OvertimeFormula;
            txtOvertimeID.Text = "0";

        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Overtime());

            txtOvertimeID.Text = "0";
            txtValidFrom.SelectedDate = DateTime.Now;
            txtValidTo.SelectedDate = DateTime.Now;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new Overtime();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtOvertimeID.Text)))
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
            var entity = new Overtime();
            entity = new Overtime();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new Overtime();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtOvertimeID.Text)))
            {
                SetEntityValue(entity);
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
            auditLogFilter.PrimaryKeyData = string.Format("OvertimeID='{0}'", txtOvertimeID.Text.Trim());
            auditLogFilter.TableName = "Overtime";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtOvertimeID.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItemOverTimeDetail(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Overtime();
            if (parameters.Length > 0)
            {
                string overtimeId = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(overtimeId));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtOvertimeID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var overtime = (Overtime)entity;
            txtOvertimeID.Value = Convert.ToDouble(overtime.OvertimeID);
            txtOvertimeName.Text = overtime.OvertimeName;
            if (overtime.SalaryComponentID != null)
            {
                var salaryCompId = overtime.SalaryComponentID;
                var sc = new SalaryComponentQuery();
                sc.Where(sc.SalaryComponentID == salaryCompId);
                cboSalaryComponetID.DataSource = sc.LoadDataTable();
                cboSalaryComponetID.DataBind();
                cboSalaryComponetID.SelectedValue = salaryCompId.ToString();
            }
            txtValidFrom.SelectedDate = overtime.ValidFrom;
            txtValidTo.SelectedDate = overtime.ValidTo;

            PopulateOverTimeDetailGrid();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(Overtime entity)
        {
            entity.OvertimeName = txtOvertimeName.Text;
            entity.SalaryComponentID = cboSalaryComponetID.SelectedValue.ToInt();
            entity.ValidFrom = txtValidFrom.SelectedDate;
            entity.ValidTo = txtValidTo.SelectedDate;

            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(Overtime entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                foreach (OvertimeDetail detil in OvertimeDetails)
                {
                    detil.OvertimeID = entity.OvertimeID;

                    if (detil.es.IsAdded || detil.es.IsModified)
                    {
                        detil.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        detil.LastUpdateDateTime = DateTime.Now;
                    }
                }

                OvertimeDetails.Save();

                trans.Complete();

                txtOvertimeID.Text = entity.OvertimeID.ToString();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new OvertimeQuery();
            que.es.Top = 1;
            if (isNextRecord)
            {
                que.Where(que.OvertimeID > txtOvertimeID.Text.ToInt());
                que.OrderBy(que.OvertimeID.Ascending);
            }
            else
            {
                que.Where(que.OvertimeID < txtOvertimeID.Text.ToInt());
                que.OrderBy(que.OvertimeID.Descending);
            }
            var entity = new Overtime();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged


        #endregion

        #region ComboBox Function

        protected void cboSalaryComponetID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SalaryComponentQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.SalaryComponentID,
                    query.SalaryComponentCode,
                    query.SalaryComponentName
                );
            query.Where
                (
                    query.SRSalaryType == AppSession.Parameter.SalaryTypeOvertime,
                    query.Or
                        (
                            query.SalaryComponentCode.Like(searchTextContain),
                            query.SalaryComponentName.Like(searchTextContain)
                        )
                );
            query.OrderBy(query.SalaryComponentCode.Ascending);

            cboSalaryComponetID.DataSource = query.LoadDataTable();
            cboSalaryComponetID.DataBind();
        }

        protected void cboSalaryComponetID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SalaryComponentName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SalaryComponentID"].ToString();
        }

        #endregion ComboBox Function

        #region Record Detail Method Function SalaryComponentRuleDefinition

        private void RefreshCommandItemOverTimeDetail(AppEnum.DataMode newVal)
        {
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdOvertimeDetail.Columns[0].Visible = isVisible;
            grdOvertimeDetail.Columns[grdOvertimeDetail.Columns.Count - 1].Visible = isVisible;

            grdOvertimeDetail.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            grdOvertimeDetail.Rebind();
        }

        private OvertimeDetailCollection OvertimeDetails
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collOvertimeDetail"];
                    if (obj != null)
                    {
                        return ((OvertimeDetailCollection)(obj));
                    }
                }

                var coll = new OvertimeDetailCollection();
                var query = new OvertimeDetailQuery("a");

                query.Select
                    (
                    query.OvertimeID,
                    query.OvertimeDetailID,
                    query.HourFrom,
                    query.HourTo,
                    query.Value,
                    query.Formula,
                    query.Notes,
                    query.LastUpdateByUserID,
                    query.LastUpdateDateTime
                    );

                query.Where(query.OvertimeID == txtOvertimeID.Text);
                query.OrderBy(query.HourFrom.Ascending);

                coll.Load(query);
                Session["collOvertimeDetail"] = coll;
                return coll;
            }
            set { Session["collOvertimeDetail"] = value; }
        }

        private void PopulateOverTimeDetailGrid()
        {
            OvertimeDetails = null;
            grdOvertimeDetail.DataSource = OvertimeDetails;
            grdOvertimeDetail.MasterTableView.IsItemInserted = false;
            grdOvertimeDetail.MasterTableView.ClearEditItems();
            grdOvertimeDetail.DataBind();
        }

        protected void grdOvertimeDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdOvertimeDetail.DataSource = OvertimeDetails;
        }

        protected void grdOvertimeDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int64 id = Convert.ToInt64(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][OvertimeDetailMetadata.ColumnNames.OvertimeDetailID]);
            OvertimeDetail entity = FindOvertimeDetail(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdOvertimeDetail_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int64 id = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex][OvertimeDetailMetadata.ColumnNames.OvertimeDetailID]);
            OvertimeDetail entity = FindOvertimeDetail(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdOvertimeDetail_InsertCommand(object source, GridCommandEventArgs e)
        {
            OvertimeDetail entity = OvertimeDetails.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdOvertimeDetail.Rebind();
        }

        private OvertimeDetail FindOvertimeDetail(Int64 id)
        {
            var coll = OvertimeDetails;
            OvertimeDetail retEntity = null;
            foreach (OvertimeDetail rec in coll)
            {
                if (rec.OvertimeDetailID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(OvertimeDetail entity, GridCommandEventArgs e)
        {
            var userControl = (OvertimeFormulaItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.HourFrom = userControl.HourFrom;
                entity.HourTo = userControl.HourTo;
                entity.Value = userControl.Value;
                entity.Formula = userControl.Formula;
                entity.Notes = string.Empty;
            }
        }

        #endregion
    }
}