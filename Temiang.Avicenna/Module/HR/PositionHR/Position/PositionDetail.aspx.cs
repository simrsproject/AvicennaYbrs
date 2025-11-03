using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PositionSearch.aspx";
            UrlPageList = "PositionList.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.Position; //TODO: Isi ProgramID
            txtPositionID.Text = "1";
            //StandardReference Initialize
            if (!IsPostBack)
            {
                trPositionGradeID.Visible = false;
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
            txtPositionID.Value = 0;

            OnPopulateEntryControl(new Position());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Position entity = new Position();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPositionID.Text)))
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
            int positionLevelId = -1;
            int.TryParse(cboPositionLevelID.SelectedValue, out positionLevelId);

            if (positionLevelId == -1)
            {
                args.MessageText = "Please choose Position Level for this Position";
                args.IsCancel = true;
                return;
            }

            var entity = new Position();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            int positionLevelId = -1;
            int.TryParse(cboPositionLevelID.SelectedValue, out positionLevelId);

            if (positionLevelId == -1)
            {
                args.MessageText = "Please choose Position Level for this Position";
                args.IsCancel = true;
                return;
            }

            var entity = new Position();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPositionID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("PositionID='{0}'", txtPositionID.Text.Trim());
            auditLogFilter.TableName = "Position";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtPositionID.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItemPositionDuty(newVal);
            RefreshCommandItemPositionGoal(newVal);
            RefreshCommandItemPositionRanking(newVal);
            RefreshCommandItemPositionResponsibility(newVal);
            RefreshCommandItemPositionAuthority(newVal);
            RefreshCommandItemPositionWorkResult(newVal);
            RefreshCommandItemPositionFunctionalArea(newVal);
            RefreshCommandItemPositionBenchmark(newVal);

            RefreshCommandItemPositionPhysical(newVal);
            RefreshCommandItemPositionPsychological(newVal);
            RefreshCommandItemPositionEducation(newVal);
            RefreshCommandItemPositionLicense(newVal);
            RefreshCommandItemPositionWorkExperience(newVal);
            RefreshCommandItemPositionEmploymentCompany(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Position entity = new Position();
            if (parameters.Length > 0)
            {
                string positionID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(positionID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtPositionID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            Position position = (Position)entity;
            txtPositionID.Value = Convert.ToInt32(position.PositionID);

            if (position.PositionGradeID.HasValue && position.PositionGradeID > -1)
            {
                var pgQuery = new PositionGradeQuery();
                pgQuery.Select(pgQuery.PositionGradeID, pgQuery.PositionGradeCode, pgQuery.PositionGradeName);
                pgQuery.Where(pgQuery.PositionGradeID == Convert.ToInt32(position.PositionGradeID));
                DataTable dtb = pgQuery.LoadDataTable();
                cboPositionGradeID.DataSource = dtb;
                cboPositionGradeID.DataBind();
                cboPositionGradeID.SelectedValue = pgQuery.PositionGradeID;
                if (dtb.Rows.Count > 0)
                    cboPositionLevelID.Text = dtb.Rows[0]["PositionGradeCode"].ToString() + " - " + dtb.Rows[0]["PositionGradeName"].ToString();
            }
            else
            {
                cboPositionGradeID.Items.Clear();
                cboPositionGradeID.SelectedValue = string.Empty;
                cboPositionGradeID.Text = string.Empty;
            }

            if (position.PositionLevelID.HasValue && position.PositionLevelID > -1)
            {
                var plQuery = new PositionLevelQuery();
                plQuery.Select(plQuery.PositionLevelID, plQuery.PositionLevelCode, plQuery.PositionLevelName);
                plQuery.Where(plQuery.PositionLevelID == Convert.ToInt32(position.PositionLevelID));
                DataTable dtb = plQuery.LoadDataTable();
                cboPositionLevelID.DataSource = dtb;
                cboPositionLevelID.DataBind();
                cboPositionLevelID.SelectedValue = plQuery.PositionLevelID.ToString();
                if (dtb.Rows.Count > 0)
                    cboPositionLevelID.Text = dtb.Rows[0]["PositionLevelCode"].ToString() + " - " + dtb.Rows[0]["PositionLevelName"].ToString();
            }
            else
            {
                cboPositionLevelID.Items.Clear();
                cboPositionLevelID.SelectedValue = string.Empty;
                cboPositionLevelID.Text = string.Empty;
            }

            txtPositionCode.Text = position.PositionCode;
            txtPositionName.Text = position.PositionName;
            txtSummary.Text = position.Summary;
            txtValidFrom.SelectedDate = position.ValidFrom;
            txtValidTo.SelectedDate = position.ValidTo;
            txtGeneralQualification.Text = position.GeneralQualification;

            //Display Data Detail
            PopulatePositionDutyGrid();
            PopulatePositionGoalGrid();
            PopulatePositionRankingGrid();
            PopulatePositionResponsibilityGrid();
            PopulatePositionAuthorityGrid();
            PopulatePositionWorkResultGrid();
            PopulatePositionFunctionalAreaGrid();
            PopulatePositionBenchmarkGrid();

            PopulatePositionPhysicalGrid();
            PopulatePositionPsychologicalGrid();
            PopulatePositionEducationGrid();
            PopulatePositionLicenseGrid();
            PopulatePositionWorkExperienceGrid();
            PopulatePositionEmploymentCompanyGrid();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(Position entity)
        {
            entity.PositionID = Convert.ToInt32(txtPositionID.Value);
            entity.PositionCode = txtPositionCode.Text;
            entity.PositionName = txtPositionName.Text;
            entity.Summary = txtSummary.Text;
            //entity.PositionGradeID = int.TryParse(cboPositionGradeID.SelectedValue, null);
            
            int positionLevelId = -1;
            int.TryParse(cboPositionLevelID.SelectedValue, out positionLevelId);
            entity.PositionLevelID = positionLevelId;

            entity.ValidFrom = txtValidFrom.SelectedDate;
            entity.ValidTo = txtValidTo.SelectedDate;
            entity.GeneralQualification = txtGeneralQualification.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(Position entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                txtPositionID.Value = entity.PositionID;

                //Update Detil
                //--> PositionDuty
                foreach (PositionDuty duty in PositionDutys)
                {
                    duty.PositionID = Convert.ToInt32(txtPositionID.Text);
                    //Last Update Status
                    if (duty.es.IsAdded || duty.es.IsModified)
                    {
                        duty.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        duty.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> PositionGoal
                foreach (PositionGoal goal in PositionGoals)
                {
                    goal.PositionID = Convert.ToInt32(txtPositionID.Text);
                    //Last Update Status
                    if (goal.es.IsAdded || goal.es.IsModified)
                    {
                        goal.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        goal.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> PositionRanking
                foreach (PositionRanking ranking in PositionRankings)
                {
                    ranking.PositionID = Convert.ToInt32(txtPositionID.Text);
                    //Last Update Status
                    if (ranking.es.IsAdded || ranking.es.IsModified)
                    {
                        ranking.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        ranking.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> PositionResponsibility
                foreach (PositionResponsibility responsibility in PositionResponsibilitys)
                {
                    responsibility.PositionID = Convert.ToInt32(txtPositionID.Text);
                    //Last Update Status
                    if (responsibility.es.IsAdded || responsibility.es.IsModified)
                    {
                        responsibility.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        responsibility.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> PositionAuthority
                foreach (PositionAuthority authority in PositionAuthoritys)
                {
                    authority.PositionID = Convert.ToInt32(txtPositionID.Text);
                    //Last Update Status
                    if (authority.es.IsAdded || authority.es.IsModified)
                    {
                        authority.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        authority.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> PositionWorkResult
                foreach (PositionWorkResult workResult in PositionWorkResults)
                {
                    workResult.PositionID = Convert.ToInt32(txtPositionID.Text);
                    //Last Update Status
                    if (workResult.es.IsAdded || workResult.es.IsModified)
                    {
                        workResult.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        workResult.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> PositionFunctionalArea
                foreach (PositionFunctionalArea area in PositionFunctionalAreas)
                {
                    area.PositionID = Convert.ToInt32(txtPositionID.Text);
                    //Last Update Status
                    if (area.es.IsAdded || area.es.IsModified)
                    {
                        area.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        area.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> PositionBenchmark
                foreach (PositionBenchmark benchmark in PositionBenchmarks)
                {
                    benchmark.PositionID = Convert.ToInt32(txtPositionID.Text);
                    //Last Update Status
                    if (benchmark.es.IsAdded || benchmark.es.IsModified)
                    {
                        benchmark.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        benchmark.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> PositionPhysical
                foreach (PositionPhysical physical in PositionPhysicals)
                {
                    physical.PositionID = Convert.ToInt32(txtPositionID.Text);
                    //Last Update Status
                    if (physical.es.IsAdded || physical.es.IsModified)
                    {
                        physical.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        physical.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> positionPsychological
                foreach (PositionPsychological psychological in PositionPsychologicals)
                {
                    psychological.PositionID = Convert.ToInt32(txtPositionID.Text);
                    //Last Update Status
                    if (psychological.es.IsAdded || psychological.es.IsModified)
                    {
                        psychological.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        psychological.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> positionEducation
                foreach (PositionEducation education in PositionEducations)
                {
                    education.PositionID = Convert.ToInt32(txtPositionID.Text);
                    //Last Update Status
                    if (education.es.IsAdded || education.es.IsModified)
                    {
                        education.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        education.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> positionLicense
                foreach (PositionLicense license in PositionLicenses)
                {
                    license.PositionID = Convert.ToInt32(txtPositionID.Text);
                    //Last Update Status
                    if (license.es.IsAdded || license.es.IsModified)
                    {
                        license.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        license.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> PositionWorkExperience
                foreach (PositionWorkExperience work in PositionWorkExperiences)
                {
                    work.PositionID = Convert.ToInt32(txtPositionID.Text);
                    //Last Update Status
                    if (work.es.IsAdded || work.es.IsModified)
                    {
                        work.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        work.LastUpdateDateTime = DateTime.Now;
                    }
                }

                //--> PositionEmploymentCompany
                foreach (PositionEmploymentCompany employment in PositionEmploymentCompanys)
                {
                    employment.PositionID = Convert.ToInt32(txtPositionID.Text);
                    //Last Update Status
                    if (employment.es.IsAdded || employment.es.IsModified)
                    {
                        employment.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        employment.LastUpdateDateTime = DateTime.Now;
                    }
                }

                PositionDutys.Save();
                PositionGoals.Save();
                PositionRankings.Save();
                PositionResponsibilitys.Save();
                PositionAuthoritys.Save();
                PositionWorkResults.Save();
                PositionFunctionalAreas.Save();
                PositionBenchmarks.Save();

                PositionPhysicals.Save();
                PositionPsychologicals.Save();
                PositionEducations.Save();
                PositionLicenses.Save();
                PositionWorkExperiences.Save();
                PositionEmploymentCompanys.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            PositionQuery que = new PositionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PositionID > txtPositionID.Text);
                que.OrderBy(que.PositionID.Ascending);
            }
            else
            {
                que.Where(que.PositionID < txtPositionID.Text);
                que.OrderBy(que.PositionID.Descending);
            }
            Position entity = new Position();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        #endregion

        #region ComboBox Function

        protected void cboPositionGradeID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            PositionGradeQuery query = new PositionGradeQuery();
            query.es.Top = 20;
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
                            query.PositionGradeCode.Like(searchTextContain),
                            query.PositionGradeName.Like(searchTextContain)
                        )
                );

            cboPositionGradeID.DataSource = query.LoadDataTable();
            cboPositionGradeID.DataBind();
        }

        protected void cboPositionGradeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionGradeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionGradeID"].ToString();
        }

        protected void cboPositionLevelID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            PositionLevelQuery query = new PositionLevelQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.PositionLevelID,
                    query.PositionLevelCode,
                    query.PositionLevelName
                );
            query.Where
                (
                    query.Or
                        (
                            query.PositionLevelCode.Like(searchTextContain),
                            query.PositionLevelName.Like(searchTextContain)
                        )
                );

            cboPositionLevelID.DataSource = query.LoadDataTable();
            cboPositionLevelID.DataBind();
        }

        protected void cboPositionLevelID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionLevelCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["PositionLevelName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionLevelID"].ToString();
        }

        #endregion ComboBox Function

        #region Record Detail Method Function PositionDuty
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PositionDutys.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemPositionDuty(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPositionDuty.Columns[0].Visible = isVisible;
            grdPositionDuty.Columns[grdPositionDuty.Columns.Count - 1].Visible = isVisible;

            grdPositionDuty.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPositionDuty.Rebind();
        }

        private PositionDutyCollection PositionDutys
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPositionDuty"];
                    if (obj != null)
                    {
                        return ((PositionDutyCollection)(obj));
                    }
                }

                PositionDutyCollection coll = new PositionDutyCollection();
                PositionDutyQuery query = new PositionDutyQuery("a");

                PositionQuery posquery = new PositionQuery("b");

                query.InnerJoin(posquery).On(query.PositionID == posquery.PositionID);

                query.Where(query.PositionID == txtPositionID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PositionID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                query.Select
                    (
                       query.PositionDutyID,
                       query.PositionID,
                       query.DutyName,
                       query.Description
                    );
                Session["collPositionDuty"] = coll;
                return coll;
            }
            set { Session["collPositionDuty"] = value; }
        }

        private void PopulatePositionDutyGrid()
        {
            //Display Data Detail
            PositionDutys = null; //Reset Record Detail
            grdPositionDuty.DataSource = PositionDutys; //Requery
            grdPositionDuty.MasterTableView.IsItemInserted = false;
            grdPositionDuty.MasterTableView.ClearEditItems();
            grdPositionDuty.DataBind();
        }

        protected void grdPositionDuty_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPositionDuty.DataSource = PositionDutys;
        }

        protected void grdPositionDuty_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 positionDutyID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PositionDutyMetadata.ColumnNames.PositionDutyID]);
            PositionDuty entity = FindPositionDuty(positionDutyID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPositionDuty_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 positionDutyID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PositionDutyMetadata.ColumnNames.PositionDutyID]);
            PositionDuty entity = FindPositionDuty(positionDutyID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPositionDuty_InsertCommand(object source, GridCommandEventArgs e)
        {
            PositionDuty entity = PositionDutys.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPositionDuty.Rebind();
        }
        private PositionDuty FindPositionDuty(Int32 positionDutyID)
        {
            PositionDutyCollection coll = PositionDutys;
            PositionDuty retEntity = null;
            foreach (PositionDuty rec in coll)
            {
                if (rec.PositionDutyID.Equals(positionDutyID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PositionDuty entity, GridCommandEventArgs e)
        {
            PositionDutyDetail userControl = (PositionDutyDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                //entity.PositionID = userControl.PositionID;
                entity.DutyName = userControl.DutyName;
                entity.Description = userControl.Description;

            }
        }

        #endregion

        #region Record Detail Method Function PositionGoal
        private void RefreshCommandItemPositionGoal(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPositionGoal.Columns[0].Visible = isVisible;
            grdPositionGoal.Columns[grdPositionGoal.Columns.Count - 1].Visible = isVisible;

            grdPositionGoal.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPositionGoal.Rebind();
        }

        private PositionGoalCollection PositionGoals
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPositionGoal"];
                    if (obj != null)
                    {
                        return ((PositionGoalCollection)(obj));
                    }
                }

                var coll = new PositionGoalCollection();
                var query = new PositionGoalQuery("a");

                var posquery = new PositionQuery("b");

                query.InnerJoin(posquery).On(query.PositionID == posquery.PositionID);

                query.Where(query.PositionID == txtPositionID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PositionID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                query.Select
                    (
                       query.PositionGoalID,
                       query.PositionID,
                       query.GoalName,
                       query.Description
                    );
                Session["collPositionGoal"] = coll;
                return coll;
            }
            set { Session["collPositionGoal"] = value; }
        }

        private void PopulatePositionGoalGrid()
        {
            //Display Data Detail
            PositionGoals = null; //Reset Record Detail
            grdPositionGoal.DataSource = PositionGoals; //Requery
            grdPositionGoal.MasterTableView.IsItemInserted = false;
            grdPositionGoal.MasterTableView.ClearEditItems();
            grdPositionGoal.DataBind();
        }

        protected void grdPositionGoal_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPositionGoal.DataSource = PositionGoals;
        }

        protected void grdPositionGoal_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PositionGoalMetadata.ColumnNames.PositionGoalID]);
            PositionGoal entity = FindPositionGoal(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPositionGoal_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PositionGoalMetadata.ColumnNames.PositionGoalID]);
            PositionGoal entity = FindPositionGoal(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPositionGoal_InsertCommand(object source, GridCommandEventArgs e)
        {
            PositionGoal entity = PositionGoals.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPositionGoal.Rebind();
        }
        private PositionGoal FindPositionGoal(Int32 id)
        {
            PositionGoalCollection coll = PositionGoals;
            PositionGoal retEntity = null;
            foreach (PositionGoal rec in coll)
            {
                if (rec.PositionGoalID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PositionGoal entity, GridCommandEventArgs e)
        {
            var userControl = (PositionGoalDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.GoalName = userControl.GoalName;
                entity.Description = userControl.Description;
            }
        }

        #endregion

        #region Record Detail Method Function PositionRanking
        private void RefreshCommandItemPositionRanking(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPositionRanking.Columns[0].Visible = isVisible;
            grdPositionRanking.Columns[grdPositionRanking.Columns.Count - 1].Visible = isVisible;

            grdPositionRanking.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPositionRanking.Rebind();
        }

        private PositionRankingCollection PositionRankings
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPositionRanking"];
                    if (obj != null)
                    {
                        return ((PositionRankingCollection)(obj));
                    }
                }

                var coll = new PositionRankingCollection();
                var query = new PositionRankingQuery("a");

                var posquery = new PositionQuery("b");

                query.InnerJoin(posquery).On(query.PositionID == posquery.PositionID);

                query.Where(query.PositionID == txtPositionID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PositionID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                query.Select
                    (
                       query.PositionRankingID,
                       query.PositionID,
                       query.RankingName,
                       query.Description
                    );
                Session["collPositionRanking"] = coll;
                return coll;
            }
            set { Session["collPositionRanking"] = value; }
        }

        private void PopulatePositionRankingGrid()
        {
            //Display Data Detail
            PositionRankings = null; //Reset Record Detail
            grdPositionRanking.DataSource = PositionRankings; //Requery
            grdPositionRanking.MasterTableView.IsItemInserted = false;
            grdPositionRanking.MasterTableView.ClearEditItems();
            grdPositionRanking.DataBind();
        }

        protected void grdPositionRanking_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPositionRanking.DataSource = PositionRankings;
        }

        protected void grdPositionRanking_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PositionRankingMetadata.ColumnNames.PositionRankingID]);
            PositionRanking entity = FindPositionRanking(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPositionRanking_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PositionRankingMetadata.ColumnNames.PositionRankingID]);
            PositionRanking entity = FindPositionRanking(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPositionRanking_InsertCommand(object source, GridCommandEventArgs e)
        {
            PositionRanking entity = PositionRankings.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPositionRanking.Rebind();
        }
        private PositionRanking FindPositionRanking(Int32 id)
        {
            PositionRankingCollection coll = PositionRankings;
            PositionRanking retEntity = null;
            foreach (PositionRanking rec in coll)
            {
                if (rec.PositionRankingID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PositionRanking entity, GridCommandEventArgs e)
        {
            var userControl = (PositionRankingDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RankingName = userControl.RankingName;
                entity.Description = userControl.Description;
            }
        }

        #endregion

        #region Record Detail Method Function PositionResponsibility
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PositionResponsibilitys.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemPositionResponsibility(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPositionResponsibility.Columns[0].Visible = isVisible;
            grdPositionResponsibility.Columns[grdPositionResponsibility.Columns.Count - 1].Visible = isVisible;

            grdPositionResponsibility.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPositionResponsibility.Rebind();
        }

        private PositionResponsibilityCollection PositionResponsibilitys
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPositionResponsibility"];
                    if (obj != null)
                    {
                        return ((PositionResponsibilityCollection)(obj));
                    }
                }

                PositionResponsibilityCollection coll = new PositionResponsibilityCollection();
                PositionResponsibilityQuery query = new PositionResponsibilityQuery("a");

                PositionQuery posquery = new PositionQuery("b");

                query.InnerJoin(posquery).On(query.PositionID == posquery.PositionID);

                query.Where(query.PositionID == txtPositionID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PositionID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                query.Select
                    (
                       query.PositionResponsibilityID,
                       query.PositionID,
                       query.ResponsibilityName,
                       query.Description,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );
                Session["collPositionResponsibility"] = coll;
                return coll;
            }
            set { Session["collPositionResponsibility"] = value; }
        }

        private void PopulatePositionResponsibilityGrid()
        {
            //Display Data Detail
            PositionResponsibilitys = null; //Reset Record Detail
            grdPositionResponsibility.DataSource = PositionResponsibilitys; //Requery
            grdPositionResponsibility.MasterTableView.IsItemInserted = false;
            grdPositionResponsibility.MasterTableView.ClearEditItems();
            grdPositionResponsibility.DataBind();
        }

        protected void grdPositionResponsibility_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPositionResponsibility.DataSource = PositionResponsibilitys;
        }

        protected void grdPositionResponsibility_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 positionResponsibilityID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PositionResponsibilityMetadata.ColumnNames.PositionResponsibilityID]);
            PositionResponsibility entity = FindPositionResponsibility(positionResponsibilityID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPositionResponsibility_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 positionResponsibilityID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PositionResponsibilityMetadata.ColumnNames.PositionResponsibilityID]);
            PositionResponsibility entity = FindPositionResponsibility(positionResponsibilityID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPositionResponsibility_InsertCommand(object source, GridCommandEventArgs e)
        {
            PositionResponsibility entity = PositionResponsibilitys.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPositionResponsibility.Rebind();
        }

        private PositionResponsibility FindPositionResponsibility(Int32 positionResponsibilityID)
        {
            PositionResponsibilityCollection coll = PositionResponsibilitys;
            PositionResponsibility retEntity = null;
            foreach (PositionResponsibility rec in coll)
            {
                if (rec.PositionResponsibilityID.Equals(positionResponsibilityID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(PositionResponsibility entity, GridCommandEventArgs e)
        {
            PositionResponsibilityDetail userControl = (PositionResponsibilityDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.ResponsibilityName = userControl.ResponsibilityName;
                entity.Description = userControl.Description;

            }
        }

        #endregion

        #region Record Detail Method Function PositionAuthority
        private void RefreshCommandItemPositionAuthority(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPositionAuthority.Columns[0].Visible = isVisible;
            grdPositionAuthority.Columns[grdPositionAuthority.Columns.Count - 1].Visible = isVisible;

            grdPositionAuthority.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPositionAuthority.Rebind();
        }

        private PositionAuthorityCollection PositionAuthoritys
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPositionAuthority"];
                    if (obj != null)
                    {
                        return ((PositionAuthorityCollection)(obj));
                    }
                }

                var coll = new PositionAuthorityCollection();
                var query = new PositionAuthorityQuery("a");

                var posquery = new PositionQuery("b");

                query.InnerJoin(posquery).On(query.PositionID == posquery.PositionID);

                query.Where(query.PositionID == txtPositionID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PositionID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                query.Select
                    (
                       query.PositionAuthorityID,
                       query.PositionID,
                       query.AuthorityName,
                       query.Description,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );
                Session["collPositionAuthority"] = coll;
                return coll;
            }
            set { Session["collPositionAuthority"] = value; }
        }

        private void PopulatePositionAuthorityGrid()
        {
            //Display Data Detail
            PositionAuthoritys = null; //Reset Record Detail
            grdPositionAuthority.DataSource = PositionAuthoritys; //Requery
            grdPositionAuthority.MasterTableView.IsItemInserted = false;
            grdPositionAuthority.MasterTableView.ClearEditItems();
            grdPositionAuthority.DataBind();
        }

        protected void grdPositionAuthority_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPositionAuthority.DataSource = PositionAuthoritys;
        }

        protected void grdPositionAuthority_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PositionAuthorityMetadata.ColumnNames.PositionAuthorityID]);
            PositionAuthority entity = FindPositionAuthority(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPositionAuthority_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PositionAuthorityMetadata.ColumnNames.PositionAuthorityID]);
            PositionAuthority entity = FindPositionAuthority(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPositionAuthority_InsertCommand(object source, GridCommandEventArgs e)
        {
            PositionAuthority entity = PositionAuthoritys.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPositionAuthority.Rebind();
        }

        private PositionAuthority FindPositionAuthority(Int32 id)
        {
            PositionAuthorityCollection coll = PositionAuthoritys;
            PositionAuthority retEntity = null;
            foreach (PositionAuthority rec in coll)
            {
                if (rec.PositionAuthorityID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(PositionAuthority entity, GridCommandEventArgs e)
        {
            PositionAuthorityDetail userControl = (PositionAuthorityDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.AuthorityName = userControl.AuthorityName;
                entity.Description = userControl.Description;

            }
        }

        #endregion

        #region Record Detail Method Function PositionWorkResult
        private void RefreshCommandItemPositionWorkResult(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPositionWorkResult.Columns[0].Visible = isVisible;
            grdPositionWorkResult.Columns[grdPositionWorkResult.Columns.Count - 1].Visible = isVisible;

            grdPositionWorkResult.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPositionWorkResult.Rebind();
        }

        private PositionWorkResultCollection PositionWorkResults
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPositionWorkResult"];
                    if (obj != null)
                    {
                        return ((PositionWorkResultCollection)(obj));
                    }
                }

                var coll = new PositionWorkResultCollection();
                var query = new PositionWorkResultQuery("a");

                var posquery = new PositionQuery("b");

                query.InnerJoin(posquery).On(query.PositionID == posquery.PositionID);

                query.Where(query.PositionID == txtPositionID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PositionID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                query.Select
                    (
                       query.PositionWorkResultID,
                       query.PositionID,
                       query.WorkResultName,
                       query.Description,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );
                Session["collPositionWorkResult"] = coll;
                return coll;
            }
            set { Session["collPositionWorkResult"] = value; }
        }

        private void PopulatePositionWorkResultGrid()
        {
            //Display Data Detail
            PositionWorkResults = null; //Reset Record Detail
            grdPositionWorkResult.DataSource = PositionWorkResults; //Requery
            grdPositionWorkResult.MasterTableView.IsItemInserted = false;
            grdPositionWorkResult.MasterTableView.ClearEditItems();
            grdPositionWorkResult.DataBind();
        }

        protected void grdPositionWorkResult_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPositionWorkResult.DataSource = PositionWorkResults;
        }

        protected void grdPositionWorkResult_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PositionWorkResultMetadata.ColumnNames.PositionWorkResultID]);
            PositionWorkResult entity = FindPositionWorkResult(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPositionWorkResult_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PositionWorkResultMetadata.ColumnNames.PositionWorkResultID]);
            PositionWorkResult entity = FindPositionWorkResult(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPositionWorkResult_InsertCommand(object source, GridCommandEventArgs e)
        {
            PositionWorkResult entity = PositionWorkResults.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPositionWorkResult.Rebind();
        }

        private PositionWorkResult FindPositionWorkResult(Int32 id)
        {
            PositionWorkResultCollection coll = PositionWorkResults;
            PositionWorkResult retEntity = null;
            foreach (PositionWorkResult rec in coll)
            {
                if (rec.PositionWorkResultID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(PositionWorkResult entity, GridCommandEventArgs e)
        {
            PositionWorkResultDetail userControl = (PositionWorkResultDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.WorkResultName = userControl.WorkResultName;
                entity.Description = userControl.Description;

            }
        }

        #endregion

        #region Record Detail Method Function PositionFunctionalArea
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PositionFunctionalAreas.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemPositionFunctionalArea(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPositionFunctionalArea.Columns[0].Visible = isVisible;
            grdPositionFunctionalArea.Columns[grdPositionFunctionalArea.Columns.Count - 1].Visible = isVisible;

            grdPositionFunctionalArea.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPositionFunctionalArea.Rebind();
        }

        private PositionFunctionalAreaCollection PositionFunctionalAreas
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPositionFunctionalArea"];
                    if (obj != null)
                    {
                        return ((PositionFunctionalAreaCollection)(obj));
                    }
                }

                PositionFunctionalAreaCollection coll = new PositionFunctionalAreaCollection();
                AppStandardReferenceItemQuery area = new AppStandardReferenceItemQuery("c");
                PositionQuery position = new PositionQuery("b");
                PositionFunctionalAreaQuery query = new PositionFunctionalAreaQuery("a");


                query.Select
                    (
                       query.PositionFunctionalAreaID,
                       query.PositionID,
                       query.SRPositionFunctionalArea,
                       area.ItemName.As("refToHR_PositionFunctionalAreaName"),
                       query.Description,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(position).On(query.PositionID == position.PositionID);
                query.InnerJoin(area).On
                        (
                            query.SRPositionFunctionalArea == area.ItemID &
                            area.StandardReferenceID == "PositionFunctionalArea"
                        );

                query.Where(query.PositionID == txtPositionID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PositionID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collPositionFunctionalArea"] = coll;
                return coll;
            }
            set { Session["collPositionFunctionalArea"] = value; }
        }

        private void PopulatePositionFunctionalAreaGrid()
        {
            //Display Data Detail
            PositionFunctionalAreas = null; //Reset Record Detail
            grdPositionFunctionalArea.DataSource = PositionFunctionalAreas; //Requery
            grdPositionFunctionalArea.MasterTableView.IsItemInserted = false;
            grdPositionFunctionalArea.MasterTableView.ClearEditItems();
            grdPositionFunctionalArea.DataBind();
        }

        protected void grdPositionFunctionalArea_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPositionFunctionalArea.DataSource = PositionFunctionalAreas;
        }

        protected void grdPositionFunctionalArea_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 positionFunctionalAreaID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PositionFunctionalAreaMetadata.ColumnNames.PositionFunctionalAreaID]);
            PositionFunctionalArea entity = FindPositionFunctionalArea(positionFunctionalAreaID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPositionFunctionalArea_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 positionFunctionalAreaID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PositionFunctionalAreaMetadata.ColumnNames.PositionFunctionalAreaID]);
            PositionFunctionalArea entity = FindPositionFunctionalArea(positionFunctionalAreaID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPositionFunctionalArea_InsertCommand(object source, GridCommandEventArgs e)
        {
            PositionFunctionalArea entity = PositionFunctionalAreas.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPositionFunctionalArea.Rebind();
        }
        private PositionFunctionalArea FindPositionFunctionalArea(Int32 positionFunctionalAreaID)
        {
            PositionFunctionalAreaCollection coll = PositionFunctionalAreas;
            PositionFunctionalArea retEntity = null;
            foreach (PositionFunctionalArea rec in coll)
            {
                if (rec.PositionFunctionalAreaID.Equals(positionFunctionalAreaID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PositionFunctionalArea entity, GridCommandEventArgs e)
        {
            PositionFunctionalAreaDetail userControl = (PositionFunctionalAreaDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.SRPositionFunctionalArea = userControl.SRPositionFunctionalArea;
                entity.PositionFunctionalAreaName = userControl.PositionFunctionalAreaName;
                entity.Description = userControl.Description;
            }
        }

        #endregion

        #region Record Detail Method Function PositionBenchmark
        private void RefreshCommandItemPositionBenchmark(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPositionBenchmark.Columns[0].Visible = isVisible;
            grdPositionBenchmark.Columns[grdPositionBenchmark.Columns.Count - 1].Visible = isVisible;

            grdPositionBenchmark.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPositionBenchmark.Rebind();
        }

        private PositionBenchmarkCollection PositionBenchmarks
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPositionBenchmark"];
                    if (obj != null)
                    {
                        return ((PositionBenchmarkCollection)(obj));
                    }
                }

                var coll = new PositionBenchmarkCollection();
                var query = new PositionBenchmarkQuery("a");

                var posquery = new PositionQuery("b");

                query.InnerJoin(posquery).On(query.PositionID == posquery.PositionID);

                query.Where(query.PositionID == txtPositionID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PositionID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                query.Select
                    (
                       query.PositionBenchmarkID,
                       query.PositionID,
                       query.BenchmarkName,
                       query.Description
                    );
                Session["collPositionBenchmark"] = coll;
                return coll;
            }
            set { Session["collPositionBenchmark"] = value; }
        }

        private void PopulatePositionBenchmarkGrid()
        {
            //Display Data Detail
            PositionBenchmarks = null; //Reset Record Detail
            grdPositionBenchmark.DataSource = PositionBenchmarks; //Requery
            grdPositionBenchmark.MasterTableView.IsItemInserted = false;
            grdPositionBenchmark.MasterTableView.ClearEditItems();
            grdPositionBenchmark.DataBind();
        }

        protected void grdPositionBenchmark_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPositionBenchmark.DataSource = PositionBenchmarks;
        }

        protected void grdPositionBenchmark_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PositionBenchmarkMetadata.ColumnNames.PositionBenchmarkID]);
            PositionBenchmark entity = FindPositionBenchmark(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPositionBenchmark_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PositionBenchmarkMetadata.ColumnNames.PositionBenchmarkID]);
            PositionBenchmark entity = FindPositionBenchmark(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPositionBenchmark_InsertCommand(object source, GridCommandEventArgs e)
        {
            PositionBenchmark entity = PositionBenchmarks.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPositionBenchmark.Rebind();
        }
        private PositionBenchmark FindPositionBenchmark(Int32 id)
        {
            PositionBenchmarkCollection coll = PositionBenchmarks;
            PositionBenchmark retEntity = null;
            foreach (PositionBenchmark rec in coll)
            {
                if (rec.PositionBenchmarkID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PositionBenchmark entity, GridCommandEventArgs e)
        {
            var userControl = (PositionBenchmarkDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.BenchmarkName = userControl.BenchmarkName;
                entity.Description = userControl.Description;
            }
        }

        #endregion

        #region Record Detail Method Function PositionPhysical
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PositionPhysicals.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemPositionPhysical(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPositionPhysical.Columns[0].Visible = isVisible;
            grdPositionPhysical.Columns[grdPositionPhysical.Columns.Count - 1].Visible = isVisible;

            grdPositionPhysical.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPositionPhysical.Rebind();
        }

        private PositionPhysicalCollection PositionPhysicals
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPositionPhysical"];
                    if (obj != null)
                    {
                        return ((PositionPhysicalCollection)(obj));
                    }
                }

                PositionPhysicalCollection coll = new PositionPhysicalCollection();
                AppStandardReferenceItemQuery measurementCode = new AppStandardReferenceItemQuery("e");
                AppStandardReferenceItemQuery operandType = new AppStandardReferenceItemQuery("d");
                AppStandardReferenceItemQuery physical = new AppStandardReferenceItemQuery("c");
                PositionPhysicalQuery query = new PositionPhysicalQuery("b");
                PositionQuery posquery = new PositionQuery("a");

                query.Select
                    (
                       query.PositionPhysicalID,
                       query.PositionID,
                       query.SRPhysicalCharacteristic,
                       physical.ItemName.As("refToCharacteristicName_PositionPhysical"),
                       query.SROperandType,
                       operandType.ItemName.As("refToOperandTypeName_PositionPhysical"),
                       query.PhysicalValue,
                       query.SRMeasurementCode,
                       measurementCode.ItemName.As("refToMeasurementName_PositionPhysical"),
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(posquery).On(query.PositionID == posquery.PositionID);
                query.InnerJoin(physical).On
                        (
                            query.SRPhysicalCharacteristic == physical.ItemID &
                            physical.StandardReferenceID == "PhysicalCharacteristic"
                        );
                query.InnerJoin(operandType).On
                       (
                           query.SROperandType == operandType.ItemID &
                           operandType.StandardReferenceID == "OperandType"
                       );
                query.InnerJoin(measurementCode).On
                       (
                           query.SROperandType == measurementCode.ItemID &
                           measurementCode.StandardReferenceID == "MeasurementCode"
                       );
                query.Where(query.PositionID == txtPositionID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PositionID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collPositionPhysical"] = coll;
                return coll;
            }
            set { Session["collPositionPhysical"] = value; }
        }

        private void PopulatePositionPhysicalGrid()
        {
            //Display Data Detail
            PositionPhysicals = null; //Reset Record Detail
            grdPositionPhysical.DataSource = PositionPhysicals; //Requery
            grdPositionPhysical.MasterTableView.IsItemInserted = false;
            grdPositionPhysical.MasterTableView.ClearEditItems();
            grdPositionPhysical.DataBind();
        }

        protected void grdPositionPhysical_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPositionPhysical.DataSource = PositionPhysicals;
        }

        protected void grdPositionPhysical_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 positionPhysicalID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PositionPhysicalMetadata.ColumnNames.PositionPhysicalID]);
            PositionPhysical entity = FindPositionPhysical(positionPhysicalID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPositionPhysical_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 positionPhysicalID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PositionPhysicalMetadata.ColumnNames.PositionPhysicalID]);
            PositionPhysical entity = FindPositionPhysical(positionPhysicalID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPositionPhysical_InsertCommand(object source, GridCommandEventArgs e)
        {
            PositionPhysical entity = PositionPhysicals.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPositionPhysical.Rebind();
        }
        private PositionPhysical FindPositionPhysical(Int32 positionPhysicalID)
        {
            PositionPhysicalCollection coll = PositionPhysicals;
            PositionPhysical retEntity = null;
            foreach (PositionPhysical rec in coll)
            {
                if (rec.PositionPhysicalID.Equals(positionPhysicalID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PositionPhysical entity, GridCommandEventArgs e)
        {
            PositionPhysicalDetail userControl = (PositionPhysicalDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.PositionPhysicalID = userControl.PositionPhysicalID;
                entity.SRPhysicalCharacteristic = userControl.SRPhysicalCharacteristic;
                entity.PhysicalCharacteristicName = userControl.PhysicalCharacteristicName;
                entity.SROperandType = userControl.SROperandType;
                entity.OperandTypeName = userControl.OperandTypeName;
                entity.PhysicalValue = userControl.PhysicalValue;
                entity.SRMeasurementCode = userControl.SRMeasurementCode;
                entity.MeasurementName = userControl.MeasurementName;

            }
        }

        #endregion

        #region Record Detail Method Function PositionPsychological
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PositionPsychologicals.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemPositionPsychological(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPositionPsychological.Columns[0].Visible = isVisible;
            grdPositionPsychological.Columns[grdPositionPsychological.Columns.Count - 1].Visible = isVisible;

            grdPositionPsychological.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPositionPsychological.Rebind();
        }

        private PositionPsychologicalCollection PositionPsychologicals
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPositionPsychological"];
                    if (obj != null)
                    {
                        return ((PositionPsychologicalCollection)(obj));
                    }
                }

                PositionPsychologicalCollection coll = new PositionPsychologicalCollection();
                AppStandardReferenceItemQuery operandType = new AppStandardReferenceItemQuery("d");
                AppStandardReferenceItemQuery psychological = new AppStandardReferenceItemQuery("c");
                PositionPsychologicalQuery query = new PositionPsychologicalQuery("b");
                PositionQuery posquery = new PositionQuery("a");

                query.Select
                    (
                       query.PositionPsychologicalID,
                       query.PositionID,
                       query.SRPsychological,
                       psychological.ItemName.As("refToSRPsychological_PositionPsychological"),
                       query.SROperandType,
                       operandType.ItemName.As("refToOperandTypeName_PositionPhysical"),
                       query.PsychologicalValue,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(posquery).On(query.PositionID == posquery.PositionID);
                query.InnerJoin(psychological).On
                        (
                            query.SRPsychological == psychological.ItemID &
                            psychological.StandardReferenceID == "psychological"
                        );
                query.LeftJoin(operandType).On
                       (
                           query.SROperandType == operandType.ItemID &
                           operandType.StandardReferenceID == "OperandType"
                       );

                query.Where(query.PositionID == txtPositionID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PositionID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collPositionPsychological"] = coll;
                return coll;
            }
            set { Session["collPositionPsychological"] = value; }
        }

        private void PopulatePositionPsychologicalGrid()
        {
            //Display Data Detail
            PositionPsychologicals = null; //Reset Record Detail
            grdPositionPsychological.DataSource = PositionPsychologicals; //Requery
            grdPositionPsychological.MasterTableView.IsItemInserted = false;
            grdPositionPsychological.MasterTableView.ClearEditItems();
            grdPositionPsychological.DataBind();
        }

        protected void grdPositionPsychological_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPositionPsychological.DataSource = PositionPsychologicals;
        }

        protected void grdPositionPsychological_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 positionPsychologicalID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PositionPsychologicalMetadata.ColumnNames.PositionPsychologicalID]);
            PositionPsychological entity = FindPositionPsychological(positionPsychologicalID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPositionPsychological_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 positionPsychologicalID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PositionPsychologicalMetadata.ColumnNames.PositionPsychologicalID]);
            PositionPsychological entity = FindPositionPsychological(positionPsychologicalID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPositionPsychological_InsertCommand(object source, GridCommandEventArgs e)
        {
            PositionPsychological entity = PositionPsychologicals.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPositionPsychological.Rebind();
        }
        private PositionPsychological FindPositionPsychological(Int32 positionPsychologicalID)
        {
            PositionPsychologicalCollection coll = PositionPsychologicals;
            PositionPsychological retEntity = null;
            foreach (PositionPsychological rec in coll)
            {
                if (rec.PositionPsychologicalID.Equals(positionPsychologicalID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PositionPsychological entity, GridCommandEventArgs e)
        {
            PositionPsychologicalDetail userControl = (PositionPsychologicalDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.PositionPsychologicalID = userControl.PositionPsychologicalID;
                entity.SRPsychological = userControl.SRPsychological;
                entity.PsychologicalName = userControl.PsychologicalName;
                entity.PsychologicalValue = userControl.PsychologicalValue;
                entity.SROperandType = userControl.SROperandType;
                entity.OperandTypeName = userControl.OperandTypeName;


            }
        }

        #endregion

        #region Record Detail Method Function PositionEducation
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PositionEducations.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemPositionEducation(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPositionEducation.Columns[0].Visible = isVisible;
            grdPositionEducation.Columns[grdPositionEducation.Columns.Count - 1].Visible = isVisible;

            grdPositionEducation.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPositionEducation.Rebind();
        }

        private PositionEducationCollection PositionEducations
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPositionEducation"];
                    if (obj != null)
                    {
                        return ((PositionEducationCollection)(obj));
                    }
                }

                PositionEducationCollection coll = new PositionEducationCollection();
                AppStandardReferenceItemQuery requirement = new AppStandardReferenceItemQuery("e");
                AppStandardReferenceItemQuery field = new AppStandardReferenceItemQuery("d");
                AppStandardReferenceItemQuery level = new AppStandardReferenceItemQuery("c");
                PositionEducationQuery query = new PositionEducationQuery("b");
                PositionQuery posquery = new PositionQuery("a");

                query.Select
                    (
                       query.PositionEducationID,
                       query.PositionID,
                       query.SRRequirement,
                       requirement.ItemName.As("refToHRRequirementName_PositionEducation"),
                       query.SREducationLevel,
                       level.ItemName.As("refToEducationLevelName_PositionEducation"),
                       query.SREducationField,
                       field.ItemName.As("refToEducationFieldName_PositionEducation"),
                       query.EducationNotes,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(posquery).On(query.PositionID == posquery.PositionID);
                query.InnerJoin(level).On
                        (
                            query.SREducationLevel == level.ItemID &
                            level.StandardReferenceID == "EducationLevel"
                        );
                query.LeftJoin(field).On
                       (
                           query.SREducationField == field.ItemID &
                           field.StandardReferenceID == "EducationField"
                       );
                query.InnerJoin(requirement).On
                        (
                            query.SRRequirement == requirement.ItemID &
                            requirement.StandardReferenceID == "HRLevelRequirement"
                        );

                query.Where(query.PositionID == txtPositionID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PositionID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collPositionEducation"] = coll;
                return coll;
            }
            set { Session["collPositionEducation"] = value; }
        }

        private void PopulatePositionEducationGrid()
        {
            //Display Data Detail
            PositionEducations = null; //Reset Record Detail
            grdPositionEducation.DataSource = PositionEducations; //Requery
            grdPositionEducation.MasterTableView.IsItemInserted = false;
            grdPositionEducation.MasterTableView.ClearEditItems();
            grdPositionEducation.DataBind();
        }

        protected void grdPositionEducation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPositionEducation.DataSource = PositionEducations;
        }

        protected void grdPositionEducation_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 positionEducationID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PositionEducationMetadata.ColumnNames.PositionEducationID]);
            PositionEducation entity = FindPositionEducation(positionEducationID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPositionEducation_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 positionEducationID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PositionEducationMetadata.ColumnNames.PositionEducationID]);
            PositionEducation entity = FindPositionEducation(positionEducationID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPositionEducation_InsertCommand(object source, GridCommandEventArgs e)
        {
            PositionEducation entity = PositionEducations.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPositionEducation.Rebind();
        }
        private PositionEducation FindPositionEducation(Int32 positionEducationID)
        {
            PositionEducationCollection coll = PositionEducations;
            PositionEducation retEntity = null;
            foreach (PositionEducation rec in coll)
            {
                if (rec.PositionEducationID.Equals(positionEducationID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PositionEducation entity, GridCommandEventArgs e)
        {
            PositionEducationDetail userControl = (PositionEducationDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.PositionEducationID = userControl.PositionEducationID;
                entity.SRRequirement = userControl.SRRequirement;
                entity.HRRequirementName = userControl.HRRequirementName;
                entity.SREducationLevel = userControl.SREducationLevel;
                entity.EducationLevelName = userControl.EducationLevelName;
                entity.SREducationField = userControl.SREducationField;
                entity.EducationFieldName = userControl.EducationFieldName;
                entity.EducationNotes = userControl.EducationNotes;

            }
        }

        #endregion

        #region Record Detail Method Function PositionLicense
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PositionLicenses.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemPositionLicense(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPositionLicense.Columns[0].Visible = isVisible;
            grdPositionLicense.Columns[grdPositionLicense.Columns.Count - 1].Visible = isVisible;

            grdPositionLicense.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPositionLicense.Rebind();
        }

        private PositionLicenseCollection PositionLicenses
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPositionLicense"];
                    if (obj != null)
                    {
                        return ((PositionLicenseCollection)(obj));
                    }
                }

                PositionLicenseCollection coll = new PositionLicenseCollection();
                AppStandardReferenceItemQuery requirement = new AppStandardReferenceItemQuery("d");
                AppStandardReferenceItemQuery license = new AppStandardReferenceItemQuery("c");
                PositionLicenseQuery query = new PositionLicenseQuery("b");
                PositionQuery posquery = new PositionQuery("a");

                query.Select
                    (
                       query.PositionLicenseID,
                       query.PositionID,
                       query.SRRequirement,
                       requirement.ItemName.As("refToHRRequirementName_PositionLicense"),
                       query.SRLicenseType,
                       license.ItemName.As("refToLicenseTypeName_PositionLicense"),
                       query.LicenseNotes,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(posquery).On(query.PositionID == posquery.PositionID);
                query.InnerJoin(license).On
                        (
                            query.SRLicenseType == license.ItemID &
                            license.StandardReferenceID == "LicenseType"
                        );
                query.InnerJoin(requirement).On
                        (
                            query.SRRequirement == requirement.ItemID &
                            requirement.StandardReferenceID == "HRLevelRequirement"
                        );
                query.Where(query.PositionID == txtPositionID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PositionID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collPositionLicense"] = coll;
                return coll;
            }
            set { Session["collPositionLicense"] = value; }
        }

        private void PopulatePositionLicenseGrid()
        {
            //Display Data Detail
            PositionLicenses = null; //Reset Record Detail
            grdPositionLicense.DataSource = PositionLicenses; //Requery
            grdPositionLicense.MasterTableView.IsItemInserted = false;
            grdPositionLicense.MasterTableView.ClearEditItems();
            grdPositionLicense.DataBind();
        }

        protected void grdPositionLicense_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPositionLicense.DataSource = PositionLicenses;
        }

        protected void grdPositionLicense_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 positionLicenseID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PositionLicenseMetadata.ColumnNames.PositionLicenseID]);
            PositionLicense entity = FindPositionLicense(positionLicenseID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPositionLicense_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 positionLicenseID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PositionLicenseMetadata.ColumnNames.PositionLicenseID]);
            PositionLicense entity = FindPositionLicense(positionLicenseID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPositionLicense_InsertCommand(object source, GridCommandEventArgs e)
        {
            PositionLicense entity = PositionLicenses.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPositionLicense.Rebind();
        }
        private PositionLicense FindPositionLicense(Int32 positionLicenseID)
        {
            PositionLicenseCollection coll = PositionLicenses;
            PositionLicense retEntity = null;
            foreach (PositionLicense rec in coll)
            {
                if (rec.PositionLicenseID.Equals(positionLicenseID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PositionLicense entity, GridCommandEventArgs e)
        {
            PositionLicenseDetail userControl = (PositionLicenseDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.SRRequirement = userControl.SRRequirement;
                entity.HRRequirementName = userControl.HRRequirementName;
                entity.SRLicenseType = userControl.SRLicenseType;
                entity.LicenseTypeName = userControl.LicenseTypeName;
                entity.LicenseNotes = userControl.LicenseNotes;

            }
        }

        #endregion

        #region Record Detail Method Function PositionWorkExperience
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PositionWorkExperiences.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemPositionWorkExperience(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPositionWorkExperience.Columns[0].Visible = isVisible;
            grdPositionWorkExperience.Columns[grdPositionWorkExperience.Columns.Count - 1].Visible = isVisible;

            grdPositionWorkExperience.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPositionWorkExperience.Rebind();
        }

        private PositionWorkExperienceCollection PositionWorkExperiences
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPositionWorkExperience"];
                    if (obj != null)
                    {
                        return ((PositionWorkExperienceCollection)(obj));
                    }
                }

                PositionWorkExperienceCollection coll = new PositionWorkExperienceCollection();
                AppStandardReferenceItemQuery requirement = new AppStandardReferenceItemQuery("d");
                AppStandardReferenceItemQuery work = new AppStandardReferenceItemQuery("c");
                PositionWorkExperienceQuery query = new PositionWorkExperienceQuery("b");
                PositionQuery posquery = new PositionQuery("a");

                query.Select
                    (
                       query.PositionWorkExperienceID,
                       query.PositionID,
                       query.SRRequirement,
                       requirement.ItemName.As("refToHRRequirementName_PositionWorkExperience"),
                       query.SRLineBusiness,
                       work.ItemName.As("refToLineBusinessName_PositionWorkExperience"),
                       query.YearExperience,
                       query.WorkExperienceNotes,
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(posquery).On(query.PositionID == posquery.PositionID);
                query.InnerJoin(work).On
                        (
                            query.SRLineBusiness == work.ItemID &
                            work.StandardReferenceID == "LineBusiness"
                        );
                query.InnerJoin(requirement).On
                        (
                            query.SRRequirement == requirement.ItemID &
                            requirement.StandardReferenceID == "HRLevelRequirement"
                        );

                query.Where(query.PositionID == txtPositionID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PositionID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collPositionWorkExperience"] = coll;
                return coll;
            }
            set { Session["collPositionWorkExperience"] = value; }
        }

        private void PopulatePositionWorkExperienceGrid()
        {
            //Display Data Detail
            PositionWorkExperiences = null; //Reset Record Detail
            grdPositionWorkExperience.DataSource = PositionWorkExperiences; //Requery
            grdPositionWorkExperience.MasterTableView.IsItemInserted = false;
            grdPositionWorkExperience.MasterTableView.ClearEditItems();
            grdPositionWorkExperience.DataBind();
        }

        protected void grdPositionWorkExperience_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPositionWorkExperience.DataSource = PositionWorkExperiences;
        }

        protected void grdPositionWorkExperience_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 positionWorkExperienceID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PositionWorkExperienceMetadata.ColumnNames.PositionWorkExperienceID]);
            PositionWorkExperience entity = FindPositionWorkExperience(positionWorkExperienceID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPositionWorkExperience_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 positionWorkExperienceID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PositionWorkExperienceMetadata.ColumnNames.PositionWorkExperienceID]);
            PositionWorkExperience entity = FindPositionWorkExperience(positionWorkExperienceID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPositionWorkExperience_InsertCommand(object source, GridCommandEventArgs e)
        {
            PositionWorkExperience entity = PositionWorkExperiences.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPositionWorkExperience.Rebind();
        }
        private PositionWorkExperience FindPositionWorkExperience(Int32 positionWorkExperienceID)
        {
            PositionWorkExperienceCollection coll = PositionWorkExperiences;
            PositionWorkExperience retEntity = null;
            foreach (PositionWorkExperience rec in coll)
            {
                if (rec.PositionWorkExperienceID.Equals(positionWorkExperienceID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PositionWorkExperience entity, GridCommandEventArgs e)
        {
            PositionWorkExperienceDetail userControl = (PositionWorkExperienceDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.SRRequirement = userControl.SRRequirement;
                entity.HRRequirementName = userControl.HRRequirementName;
                entity.SRLineBusiness = userControl.SRLineBusiness;
                entity.LineBusinessName = userControl.LineBusinessName;
                entity.YearExperience = userControl.YearExperience;
                entity.WorkExperienceNotes = userControl.WorkExperienceNotes;

            }
        }

        #endregion

        #region Record Detail Method Function PositionEmploymentCompany
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah PositionEmploymentCompanys.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemPositionEmploymentCompany(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPositionEmploymentCompany.Columns[0].Visible = isVisible;
            grdPositionEmploymentCompany.Columns[grdPositionEmploymentCompany.Columns.Count - 1].Visible = isVisible;

            grdPositionEmploymentCompany.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdPositionEmploymentCompany.Rebind();
        }

        private PositionEmploymentCompanyCollection PositionEmploymentCompanys
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPositionEmploymentCompany"];
                    if (obj != null)
                    {
                        return ((PositionEmploymentCompanyCollection)(obj));
                    }
                }

                PositionEmploymentCompanyCollection coll = new PositionEmploymentCompanyCollection();
                AppStandardReferenceItemQuery requirement = new AppStandardReferenceItemQuery("d");
                PositionGradeQuery grade = new PositionGradeQuery("c");
                PositionEmploymentCompanyQuery query = new PositionEmploymentCompanyQuery("b");
                PositionQuery posquery = new PositionQuery("a");

                query.Select
                    (
                       query.PositionEmploymentCompanyID,
                       query.PositionID,
                       query.SRRequirement,
                       requirement.ItemName.As("refToHRRequirementName_PositionWorkExperience"),
                       query.YearOfService,
                       query.PositionGradeID,
                       grade.PositionGradeName.As("refToPositionGradeName_PositionEmploymentCompany"),
                       query.LastUpdateByUserID,
                       query.LastUpdateDateTime
                    );

                query.InnerJoin(posquery).On(query.PositionID == posquery.PositionID);
                query.LeftJoin(grade).On
                        (
                            query.PositionGradeID == grade.PositionGradeID
                        );
                query.InnerJoin(requirement).On
                        (
                            query.SRRequirement == requirement.ItemID &
                            requirement.StandardReferenceID == "HRLevelRequirement"
                        );

                query.Where(query.PositionID == txtPositionID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.PositionID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collPositionEmploymentCompany"] = coll;
                return coll;
            }
            set { Session["collPositionEmploymentCompany"] = value; }
        }

        private void PopulatePositionEmploymentCompanyGrid()
        {
            //Display Data Detail
            PositionEmploymentCompanys = null; //Reset Record Detail
            grdPositionEmploymentCompany.DataSource = PositionEmploymentCompanys; //Requery
            grdPositionEmploymentCompany.MasterTableView.IsItemInserted = false;
            grdPositionEmploymentCompany.MasterTableView.ClearEditItems();
            grdPositionEmploymentCompany.DataBind();
        }

        protected void grdPositionEmploymentCompany_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPositionEmploymentCompany.DataSource = PositionEmploymentCompanys;
        }

        protected void grdPositionEmploymentCompany_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 positionEmploymentCompanyID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PositionEmploymentCompanyMetadata.ColumnNames.PositionEmploymentCompanyID]);
            PositionEmploymentCompany entity = FindPositionEmploymentCompany(positionEmploymentCompanyID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPositionEmploymentCompany_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 positionEmploymentCompanyID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PositionEmploymentCompanyMetadata.ColumnNames.PositionEmploymentCompanyID]);
            PositionEmploymentCompany entity = FindPositionEmploymentCompany(positionEmploymentCompanyID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdPositionEmploymentCompany_InsertCommand(object source, GridCommandEventArgs e)
        {
            PositionEmploymentCompany entity = PositionEmploymentCompanys.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPositionEmploymentCompany.Rebind();
        }
        private PositionEmploymentCompany FindPositionEmploymentCompany(Int32 positionEmploymentCompanyID)
        {
            PositionEmploymentCompanyCollection coll = PositionEmploymentCompanys;
            PositionEmploymentCompany retEntity = null;
            foreach (PositionEmploymentCompany rec in coll)
            {
                if (rec.PositionEmploymentCompanyID.Equals(positionEmploymentCompanyID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PositionEmploymentCompany entity, GridCommandEventArgs e)
        {
            PositionEmploymentCompanyDetail userControl = (PositionEmploymentCompanyDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.PositionEmploymentCompanyID = userControl.PositionEmploymentCompanyID;
                entity.SRRequirement = userControl.SRRequirement;
                entity.HRRequirementName = userControl.HRRequirementName;
                entity.YearOfService = userControl.YearOfService;
                entity.PositionGradeID = userControl.PositionGradeID;
                entity.PositionGradeName = userControl.PositionGradeName;

            }
        }

        #endregion
    }
}
