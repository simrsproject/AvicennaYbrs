using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionQualificationDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PositionQualificationSearch.aspx";
            UrlPageList = "PositionQualificationList.aspx";

            ProgramID = AppConstant.Program.PositionQualification; //TODO: Isi ProgramID
            txtPositionID.Text = "1";
            //StandardReference Initialize
            if (!IsPostBack)
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
            Position entity = new Position();
            entity = new Position();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Position entity = new Position();
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

            PositionGradeQuery pgQuery = new PositionGradeQuery();
            pgQuery.Where(pgQuery.PositionGradeID == Convert.ToInt32(position.PositionGradeID));
            var pg = new PositionGrade();
            txtPositionGradeName.Text = pg.Load(pgQuery) ? pg.PositionGradeName : string.Empty;
            
            PositionLevelQuery plQuery = new PositionLevelQuery();
            plQuery.Where(plQuery.PositionLevelID == Convert.ToInt32(position.PositionLevelID));
            var pl = new PositionLevel();
            txtPositionLevelName.Text = pl.Load(plQuery) ? pl.PositionLevelName : string.Empty;

            txtPositionCode.Text = position.PositionCode;
            txtPositionName.Text = position.PositionName;
            txtSummary.Text = position.Summary;
            txtValidFrom.SelectedDate = position.ValidFrom;
            txtValidTo.SelectedDate = position.ValidTo;

            //Display Data Detail
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

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }


            //Update Detil

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
        }

        private void SaveEntity(Position entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
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
