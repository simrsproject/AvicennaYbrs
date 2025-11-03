using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class SalaryScaleDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "SalaryScaleSearch.aspx";
            UrlPageList = "SalaryScaleList.aspx";

            ProgramID = AppConstant.Program.SalaryScale;

            this.WindowSearch.Height = 400;

            txtSalaryScaleID.Text = "1";

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSREmploymentType, AppEnum.StandardReference.EmploymentType);
                StandardReference.InitializeIncludeSpace(cboSRProfessionGroup, AppEnum.StandardReference.ProfessionGroup);
                StandardReference.InitializeIncludeSpace(cboSREducationGroup, AppEnum.StandardReference.EducationGroup);
            }

            //PopUp Search
            if (!IsCallback)
            {

            }
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
            OnPopulateEntryControl(new SalaryScale());
            cboSREducationGroup.SelectedValue = string.Empty;
            cboSREducationGroup.Text = string.Empty;
            cboSREmploymentType.SelectedValue = string.Empty;
            cboSREmploymentType.Text = string.Empty;
            cboSRProfessionGroup.SelectedValue = string.Empty;
            cboSRProfessionGroup.Text = string.Empty;
            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new SalaryScale();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtSalaryScaleID.Value)))
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
            var entity = new SalaryScale();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new SalaryScale();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtSalaryScaleID.Value)))
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
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("SalaryScaleCode='{0}'", txtSalaryScaleID.Value.ToString());
            auditLogFilter.TableName = "SalaryScale";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(Common.AppEnum.DataMode oldVal, Common.AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtSalaryScaleCode.Enabled = (newVal == Common.AppEnum.DataMode.New);
            RefreshCommandItemSalaryScaleFactor(newVal);
            cboPositionGradeID.Enabled = (newVal == Common.AppEnum.DataMode.New);
            cboSREmploymentType.Enabled = (newVal == Common.AppEnum.DataMode.New);
            cboSRProfessionGroup.Enabled = (newVal == Common.AppEnum.DataMode.New);
            cboSREducationGroup.Enabled = (newVal == Common.AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new SalaryScale();
            if (parameters.Length > 0)
            {
                string id = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(id));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtSalaryScaleID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            SalaryScale scale = (SalaryScale)entity;
            txtSalaryScaleID.Value = Convert.ToDouble(scale.SalaryScaleID);
            txtSalaryScaleCode.Text = scale.SalaryScaleCode;
            txtSalaryScaleName.Text = scale.SalaryScaleName;

            if (scale.PositionGradeID.HasValue && scale.PositionGradeID > 0)
            {
                var pgQuery = new PositionGradeQuery();
                pgQuery.Where(pgQuery.PositionGradeID == Convert.ToInt32(scale.PositionGradeID));
                cboPositionGradeID.DataSource = pgQuery.LoadDataTable();
                cboPositionGradeID.DataBind();
                cboPositionGradeID.SelectedValue = scale.PositionGradeID.ToString();

                var pg = new PositionGrade();
                pg.LoadByPrimaryKey(Convert.ToInt32(scale.PositionGradeID));
                cboPositionGradeID.Text = pg.PositionGradeName;
            }
            else
            {
                cboPositionGradeID.Items.Clear();
                cboPositionGradeID.SelectedValue = string.Empty;
                cboPositionGradeID.Text = string.Empty;
            }

            cboSREmploymentType.SelectedValue = scale.SREmploymentType;
            cboSRProfessionGroup.SelectedValue = scale.SRProfessionGroup;
            cboSREducationGroup.SelectedValue = scale.SREducationGroup;
            txtNotes.Text = scale.Notes;
            chkIsActive.Checked = scale.IsActive ?? false;

            //Display Data Detail
            PopulateSalaryScaleFactorGrid();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(SalaryScale entity)
        {
            entity.SalaryScaleID = Convert.ToInt32(txtSalaryScaleID.Value);
            entity.SalaryScaleCode = txtSalaryScaleCode.Text;
            entity.PositionGradeID = Convert.ToInt32(cboPositionGradeID.SelectedValue);
            entity.SREmploymentType = cboSREmploymentType.SelectedValue;
            entity.SRProfessionGroup = cboSRProfessionGroup.SelectedValue;
            entity.SREducationGroup = cboSREducationGroup.SelectedValue;
            entity.Notes = txtNotes.Text;
            entity.IsActive = chkIsActive.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //--> Standard Salary Faktor
            foreach (SalaryScaleFactor factor in SalaryScaleFactors)
            {
                factor.SalaryScaleID = Convert.ToInt32(txtSalaryScaleID.Text);
                //Last Update Status
                if (factor.es.IsAdded || factor.es.IsModified)
                {
                    factor.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    factor.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(SalaryScale entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                SalaryScaleFactors.Save();

                //Commit if success, Rollback if failed
                trans.Complete();

            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new SalaryScaleQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.SalaryScaleID > txtSalaryScaleID.Text.ToInt());
                que.OrderBy(que.SalaryScaleID.Ascending);
            }
            else
            {
                que.Where(que.SalaryScaleID < txtSalaryScaleID.Text.ToInt());
                que.OrderBy(que.SalaryScaleID.Descending);
            }
            var entity = new SalaryScale();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged
        protected void cboSREmploymentType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboPositionGradeID.Items.Clear();
            cboPositionGradeID.Text = string.Empty;
            cboPositionGradeID.SelectedValue = string.Empty;
        }
        #endregion

        #region ComboBox Function

        protected void cboPositionGradeID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            PositionGradeQuery query = new PositionGradeQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.PositionGradeID,
                    query.PositionGradeCode,
                    query.PositionGradeName
                );
            query.Where
                (
                    query.Or
                        (
                            query.PositionGradeID.Like(searchTextContain),
                            query.PositionGradeName.Like(searchTextContain)
                        )
                );
            query.Where(query.SREmploymentType == cboSREmploymentType.SelectedValue);

            cboPositionGradeID.DataSource = query.LoadDataTable();
            cboPositionGradeID.DataBind();
        }

        protected void cboPositionGradeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionGradeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionGradeID"].ToString();
        }

        #endregion ComboBox Function

        #region Record Detail Method Function StandardSalaryFaktor
        private void RefreshCommandItemSalaryScaleFactor(Common.AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != Common.AppEnum.DataMode.Read);
            grdSalaryScaleFactor.Columns[0].Visible = isVisible;
            grdSalaryScaleFactor.Columns[grdSalaryScaleFactor.Columns.Count - 1].Visible = isVisible;

            grdSalaryScaleFactor.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdSalaryScaleFactor.Rebind();
        }

        private SalaryScaleFactorCollection SalaryScaleFactors
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collSalaryScaleFactor"];
                    if (obj != null)
                    {
                        return ((SalaryScaleFactorCollection)(obj));
                    }
                }

                SalaryScaleFactorCollection coll = new SalaryScaleFactorCollection();
                SalaryScaleFactorQuery query = new SalaryScaleFactorQuery();

                query.Select
                    (
                    query.SalaryScaleFactorID,
                    query.SalaryScaleID,
                    query.ValidFrom,
                    query.Amount,
                    query.LastUpdateByUserID,
                    query.LastUpdateDateTime
                    );

                query.Where(query.SalaryScaleID == Convert.ToInt32(txtSalaryScaleID.Text)); 
                query.OrderBy(query.ValidFrom.Descending); 
                coll.Load(query);

                Session["collSalaryScaleFactor"] = coll;
                return coll;
            }
            set { Session["collSalaryScaleFactor"] = value; }
        }

        private void PopulateSalaryScaleFactorGrid()
        {
            //Display Data Detail
            SalaryScaleFactors = null; //Reset Record Detail
            grdSalaryScaleFactor.DataSource = SalaryScaleFactors; //Requery
            grdSalaryScaleFactor.MasterTableView.IsItemInserted = false;
            grdSalaryScaleFactor.MasterTableView.ClearEditItems();
            grdSalaryScaleFactor.DataBind();
        }

        protected void grdSalaryScaleFactor_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSalaryScaleFactor.DataSource = SalaryScaleFactors;
        }

        protected void grdSalaryScaleFactor_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][SalaryScaleFactorMetadata.ColumnNames.SalaryScaleFactorID]);
            SalaryScaleFactor entity = FindSalaryScaleFactor(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdSalaryScaleFactor_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][SalaryScaleFactorMetadata.ColumnNames.SalaryScaleFactorID]);
            SalaryScaleFactor entity = FindSalaryScaleFactor(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdSalaryScaleFactor_InsertCommand(object source, GridCommandEventArgs e)
        {
            SalaryScaleFactor entity = SalaryScaleFactors.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdSalaryScaleFactor.Rebind();
        }
        private SalaryScaleFactor FindSalaryScaleFactor(Int32 id)
        {
            SalaryScaleFactorCollection coll = SalaryScaleFactors;
            SalaryScaleFactor retEntity = null;
            foreach (SalaryScaleFactor rec in coll)
            {
                if (rec.SalaryScaleFactorID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(SalaryScaleFactor entity, GridCommandEventArgs e)
        {
            var userControl = (SalaryScaleFactorDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.SalaryScaleFactorID = userControl.SalaryScaleFactorID;
                entity.ValidFrom = userControl.ValidFrom;
                entity.Amount = userControl.Amount;
            }
        }

        #endregion
    }
}