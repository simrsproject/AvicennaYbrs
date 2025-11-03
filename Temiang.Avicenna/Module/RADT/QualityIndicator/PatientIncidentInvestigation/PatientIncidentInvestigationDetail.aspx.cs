using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PatientIncidentInvestigationDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 300;
            // Url Search & List
            UrlPageSearch = "##";
            UrlPageList = "PatientIncidentInvestigationList.aspx";
            ProgramID = AppConstant.Program.PatientIncidentInvestigation;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnit(cboServiceUnitID, false);
                ComboBox.PopulateWithServiceUnit(cboServiceUnitRelatedUnitID, false);
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new PatientIncidentRelatedUnit();
            if (entity.LoadByPrimaryKey(txtPatientIncidentNo.Text, cboServiceUnitRelatedUnitID.SelectedValue))
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
            //auditLogFilter.PrimaryKeyData = string.Format("PatientIncidentNo='{0}'", txtPatientIncidentNo.Text.Trim());
            //auditLogFilter.TableName = "PatientIncidentRelatedUnit";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("transNo", txtPatientIncidentNo.Text);
            printJobParameters.AddNew("unitId", cboServiceUnitRelatedUnitID.SelectedValue);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(txtIncidentDirectCause.Text))
            {
                args.MessageText = "Direct Cause required.";
                args.IsCancel = true;
                return;
            }
            if (PatientIncidentUnderlyingCausesItemComponents.Count == 0)
            {
                args.MessageText = "Data can't be approved because underlying causes is empty.";
                args.IsCancel = true;
                return;
            }

            if (PatientIncidentInvestigations.Count == 0)
            {
                args.MessageText = "Data can't be approved because recomendation is empty.";
                args.IsCancel = true;
                return;
            }

            using (var trans = new esTransactionScope())
            {
                var entity = new PatientIncidentRelatedUnit();
                entity.LoadByPrimaryKey(txtPatientIncidentNo.Text, cboServiceUnitRelatedUnitID.SelectedValue);

                entity.IsInvestigationApproved = true;
                entity.InvestigationApprovedDateTime = (new DateTime()).NowAtSqlServer();
                entity.InvestigationApprovedByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var coll = new PatientIncidentInvestigationCollection();
            coll.Query.Where(coll.Query.PatientIncidentNo == txtPatientIncidentNo.Text,
                             coll.Query.Implementation.IsNotNull());
            coll.LoadAll();
            if (coll.Count>0)
            {
                args.MessageText = "Data can't be cancel because implementation for recomendation already exist.";
                args.IsCancel = true;
                return;
            }

            using (var trans = new esTransactionScope())
            {
                var entity = new PatientIncidentRelatedUnit();
                entity.LoadByPrimaryKey(txtPatientIncidentNo.Text, cboServiceUnitRelatedUnitID.SelectedValue);

                entity.IsInvestigationApproved = false;
                entity.InvestigationApprovedByUserID = null;
                entity.InvestigationApprovedDateTime = null;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new PatientIncidentRelatedUnit();
            if (entity.LoadByPrimaryKey(txtPatientIncidentNo.Text, cboServiceUnitRelatedUnitID.SelectedValue))
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

        private bool IsApprovedOrVoid(PatientIncidentRelatedUnit entity, ValidateArgs args)
        {
            if (entity.IsInvestigationApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
        }

        public override bool OnGetStatusMenuEdit()
        {
            return txtPatientIncidentNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemUndelyingCauses(newVal);
            RefreshCommandItemInvestigation(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new PatientIncidentRelatedUnit();
            entity.LoadByPrimaryKey(Request.QueryString["id"], (Request.QueryString["uid"]));
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var relUnit = (PatientIncidentRelatedUnit)entity;
            txtPatientIncidentNo.Text = relUnit.PatientIncidentNo;
            cboServiceUnitRelatedUnitID.SelectedValue = relUnit.ServiceUnitID;

            var pi = new PatientIncident();
            pi.LoadByPrimaryKey(txtPatientIncidentNo.Text);

            if (pi.NonPatient ?? false)
            {
                txtAge.Text = string.Empty;
                txtMedicalNo.Text = string.Empty;
                txtPatientName.Text = (pi.FirstName + " " + pi.MiddleName + " " + pi.LastName).Trim();
                txtSex.Text = pi.Sex;
                
                HitungUmur(pi.DateOfBirth, pi.IncidentDateTime, txtAge);
            }
            else
            {
                if (!string.IsNullOrEmpty(pi.RegistrationNo))
                {
                    var reg = new RegistrationQuery("a");
                    var pat = new PatientQuery("b");
                    var unit = new ServiceUnitQuery("c");
                    reg.es.Top = 5;
                    reg.Select(
                            reg.RegistrationNo,
                            reg.BedID,
                            pat.PatientID,
                            pat.MedicalNo,
                            pat.PatientName,
                            unit.ServiceUnitName
                            );
                    reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
                    reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
                    reg.Where(reg.RegistrationNo == (pi.RegistrationNo ?? ""));
                    cboRegistrationNo.DataSource = reg.LoadDataTable();
                    cboRegistrationNo.DataBind();

                    cboRegistrationNo.SelectedValue = pi.RegistrationNo;
                    GetRegistration(pi.RegistrationNo);
                }
                else
                {
                    var p = new Patient();
                    if (p.LoadByPrimaryKey(pi.PatientID))
                    {
                        txtMedicalNo.Text = p.MedicalNo;
                        txtPatientName.Text = p.PatientName;
                        txtSex.Text = p.Sex;

                        HitungUmur(p.DateOfBirth, pi.IncidentDateTime, txtAge);
                    }
                }
            }

            txtInitialName.Text = pi.InitialName;

            //
            ComboBox.PopulateWithServiceUnit(cboServiceUnitID, false);
            cboServiceUnitID.SelectedValue = pi.ServiceUnitID;
            ComboBox.PopulateWithRoom(cboRoomID, pi.ServiceUnitID ?? "");
            cboRoomID.SelectedValue = pi.RoomID;
            ComboBox.PopulateWithBed(cboBedID, pi.RoomID ?? "");
            cboBedID.SelectedValue = pi.BedID;
            //

            txtIncidentLocation.Text = pi.IncidentLocation;
            
            txtIncidentDate.SelectedDate = pi.IncidentDateTime.HasValue ? pi.IncidentDateTime.Value : (new DateTime()).NowAtSqlServer();
            txtIncidentTime.SelectedDate = pi.IncidentDateTime.HasValue ? pi.IncidentDateTime.Value : (new DateTime()).NowAtSqlServer();
            txtReportingDate.SelectedDate = pi.ReportingDateTime.HasValue ? pi.ReportingDateTime.Value : (new DateTime()).NowAtSqlServer();
            txtReportingTime.SelectedDate = pi.ReportingDateTime.HasValue ? pi.ReportingDateTime.Value : (new DateTime()).NowAtSqlServer();
            txtIncidentName.Text = pi.IncidentName;
            txtChronology.Text = pi.Chronology;

            chkIsApproved.Checked = relUnit.IsInvestigationApproved ?? false;

            txtIncidentChronologyCauses.Text = relUnit.IncidentChronologyCauses;
            txtIncidentDirectCause.Text = relUnit.IncidentDirectCause;
            txtIncidentUnderlyingCauses.Text = relUnit.IncidentUnderlyingCauses;
            txtInvestigationBy.Text = relUnit.InvestigationByUserID ?? AppSession.UserLogin.UserID;
            txtInvestigationDate.SelectedDate = relUnit.InvestigationDateTime.HasValue ? relUnit.InvestigationDateTime.Value : (new DateTime()).NowAtSqlServer();
            txtInvestigationTime.SelectedDate = relUnit.InvestigationDateTime.HasValue ? relUnit.InvestigationDateTime.Value : (new DateTime()).NowAtSqlServer();

            PopulateUnderlyingCausesGrid();
            PopulateInvestigationGrid();
            PopulateRelatedUnitGrid();
        }
        #endregion

        #region Private Method Standard
        private void SetEntityValue(PatientIncidentRelatedUnit entity)
        {
            entity.PatientIncidentNo = txtPatientIncidentNo.Text;
            entity.ServiceUnitID = cboServiceUnitRelatedUnitID.SelectedValue;
            entity.IncidentChronologyCauses = txtIncidentChronologyCauses.Text;
            entity.IncidentDirectCause = txtIncidentDirectCause.Text;
            entity.IncidentUnderlyingCauses = txtIncidentUnderlyingCauses.Text;
            entity.InvestigationByUserID = txtInvestigationBy.Text;
            entity.InvestigationDateTime = DateTime.Parse(txtInvestigationDate.SelectedDate.Value.ToShortDateString() + " " +
                               txtInvestigationTime.SelectedDate.Value.ToShortTimeString());
            
            //Last Update Status
            if (entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in PatientIncidentUnderlyingCausesItemComponents)
            {
                item.PatientIncidentNo = txtPatientIncidentNo.Text;
                item.ServiceUnitID = cboServiceUnitRelatedUnitID.SelectedValue;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in PatientIncidentInvestigations)
            {
                item.PatientIncidentNo = txtPatientIncidentNo.Text;
                item.ServiceUnitID = cboServiceUnitRelatedUnitID.SelectedValue;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(PatientIncidentRelatedUnit entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                PatientIncidentUnderlyingCausesItemComponents.Save();
                PatientIncidentInvestigations.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new PatientIncidentRelatedUnitQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PatientIncidentNo == txtPatientIncidentNo.Text,
                          que.ServiceUnitID == cboServiceUnitRelatedUnitID.SelectedValue);
                que.OrderBy(que.PatientIncidentNo.Ascending);
            }
            else
            {
                que.Where(que.PatientIncidentNo == txtPatientIncidentNo.Text,
                          que.ServiceUnitID == cboServiceUnitRelatedUnitID.SelectedValue);
                que.OrderBy(que.PatientIncidentNo.Descending);
            }
            var entity = new PatientIncidentRelatedUnit();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Override Method & Function

        private void GetRegistration(string RegistrationNo)
        {
            var r = new Registration();

            if (string.IsNullOrEmpty(RegistrationNo))
            {
                //
                txtAge.Text = string.Empty;
                txtMedicalNo.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtSex.Text = string.Empty;
            }
            else
            {
                if (r.LoadByPrimaryKey(RegistrationNo))
                {
                    txtAge.Text = r.AgeInYear.ToString() + " yr " + r.AgeInMonth + " mth " + r.AgeInDay + "dy";

                    var p = new Patient();
                    if (p.LoadByPrimaryKey(r.PatientID))
                    {
                        txtMedicalNo.Text = p.MedicalNo;
                        txtPatientName.Text = p.PatientName;
                        txtSex.Text = p.Sex;
                    }

                    if (DataModeCurrent == AppEnum.DataMode.New)
                    {
                        ComboBox.PopulateWithServiceUnit(cboServiceUnitID, false);
                        cboServiceUnitID.SelectedValue = r.ServiceUnitID;
                        ComboBox.PopulateWithRoom(cboRoomID, r.ServiceUnitID ?? "");
                        cboRoomID.SelectedValue = r.RoomID;
                        ComboBox.PopulateWithBed(cboBedID, r.RoomID ?? "");
                        cboBedID.SelectedValue = r.BedID;
                    }
                }
            }
        }

        private void HitungUmur(DateTime? DateOfBirth, DateTime? dNow, RadTextBox txt)
        {
            if (!DateOfBirth.HasValue) return;
            if (!dNow.HasValue) return;
            var y = Helper.GetAgeInYear(DateOfBirth.Value.Date, dNow.Value.Date).ToString();
            var m = Helper.GetAgeInMonth(DateOfBirth.Value.Date, dNow.Value.Date).ToString();
            var d = Helper.GetAgeInDay(DateOfBirth.Value.Date, dNow.Value.Date).ToString();

            txt.Text = y + " yr " + m + " mth " + d + " dy ";
        }
        #endregion

        #region Selected Changed
        protected void cboRegistrationNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var reg = new RegistrationQuery("a");
            var pat = new PatientQuery("b");
            var unit = new ServiceUnitQuery("c");
            var room = new ServiceRoomQuery("d");

            reg.es.Top = 5;
            reg.Select(
                reg.RegistrationNo,
                reg.BedID,
                pat.PatientID,
                pat.MedicalNo,
                pat.PatientName,
                unit.ServiceUnitName,
                room.RoomName
                );
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            reg.LeftJoin(room).On(reg.RoomID == room.RoomID);
            reg.Where(
                reg.IsClosed == false,
                reg.IsVoid == false,
                reg.IsConsul == false
                );

            if (e.Text.Trim().Contains(" "))
            {
                var searchs = e.Text.Trim().Split(' ');
                foreach (var search in searchs)
                {
                    var searchLike = "%" + search + "%";
                    reg.Where(
                        reg.Or(
                            reg.RegistrationNo.Like(searchLike),
                        //pat.PatientID.Like(searchLike),
                            pat.FirstName.Like(searchLike),
                            pat.LastName.Like(searchLike),
                            pat.MiddleName.Like(searchLike),
                            pat.MedicalNo.Like(searchLike)
                        )
                    );
                }
            }
            else
            {
                string searchTextContain = string.Format("%{0}%", e.Text);
                reg.Where(
                    reg.Or(
                        reg.RegistrationNo.Like(searchTextContain),
                        //pat.PatientID.Like(searchTextContain),
                        pat.MedicalNo.Like(searchTextContain),
                        pat.FirstName.Like(searchTextContain),
                        pat.MiddleName.Like(searchTextContain),
                        pat.LastName.Like(searchTextContain)
                    )
                );
            }
            reg.OrderBy(reg.RegistrationDate.Descending);

            cboRegistrationNo.DataSource = reg.LoadDataTable();
            cboRegistrationNo.DataBind();
        }

        protected void cboRegistrationNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString();
        }
        #endregion

        #region Record Detail Method Function of Patient Incident Underlying Causes

        private PatientIncidentUnderlyingCausesItemComponentCollection PatientIncidentUnderlyingCausesItemComponents
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPatientIncidentUnderlyingCausesItemComponent"];
                    if (obj != null)
                    {
                        return ((PatientIncidentUnderlyingCausesItemComponentCollection)(obj));
                    }
                }

                var coll = new PatientIncidentUnderlyingCausesItemComponentCollection();
                var query = new PatientIncidentUnderlyingCausesItemComponentQuery("a");
                var factor = new ContributoryFactorsClassificationFrameworkQuery("b");
                var factorItem = new ContributoryFactorsClassificationFrameworkItemQuery("c");
                var factorItemComp = new ContributoryFactorsClassificationFrameworkItemComponentQuery("d");

                query.Select
                    (
                        query, 
                        factor.FactorName.As("refToContributoryFactorsClassificationFramework_FactorName"), 
                        factorItem.FactorItemName.As("refToContributoryFactorsClassificationFrameworkItem_FactorItemName"),
                        factorItemComp.ComponentName.As("refToContributoryFactorsClassificationFrameworkItemComp_ComponentName")
                    );
                query.InnerJoin(factor).On(query.FactorID == factor.FactorID);
                query.InnerJoin(factorItem).On(query.FactorID == factorItem.FactorID &&
                                               query.FactorItemID == factorItem.FactorItemID);
                query.InnerJoin(factorItemComp).On(query.FactorID == factorItemComp.FactorID &&
                                                   query.FactorItemID == factorItemComp.FactorItemID &&
                                                   query.ComponentID == factorItemComp.ComponentID);
                query.Where(query.PatientIncidentNo == txtPatientIncidentNo.Text,
                            query.ServiceUnitID == cboServiceUnitRelatedUnitID.SelectedValue);
                coll.Load(query);

                Session["collPatientIncidentUnderlyingCausesItemComponent"] = coll;
                return coll;
            }
            set
            {
                Session["collPatientIncidentUnderlyingCausesItemComponent"] = value;
            }
        }

        private void RefreshCommandItemUndelyingCauses(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdUnderlyingCauses.Columns[0].Visible = isVisible;
            grdUnderlyingCauses.Columns[grdUnderlyingCauses.Columns.Count - 1].Visible = isVisible;

            grdUnderlyingCauses.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdUnderlyingCauses.Rebind();
        }

        private void PopulateUnderlyingCausesGrid()
        {
            //Display Data Detail
            PatientIncidentUnderlyingCausesItemComponents = null; //Reset Record Detail
            grdUnderlyingCauses.DataSource = PatientIncidentUnderlyingCausesItemComponents; //Requery
            grdUnderlyingCauses.MasterTableView.IsItemInserted = false;
            grdUnderlyingCauses.MasterTableView.ClearEditItems();
            grdUnderlyingCauses.DataBind();
        }

        private PatientIncidentUnderlyingCausesItemComponent FindUnderlyingCauses(String factorId, String factorItemId, String compId)
        {
            PatientIncidentUnderlyingCausesItemComponentCollection coll = PatientIncidentUnderlyingCausesItemComponents;
            PatientIncidentUnderlyingCausesItemComponent retEntity = null;
            foreach (PatientIncidentUnderlyingCausesItemComponent rec in coll)
            {
                if (rec.FactorID.Equals(factorId) && rec.FactorItemID.Equals(factorItemId) && rec.ComponentID.Equals(compId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdUnderlyingCauses_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdUnderlyingCauses.DataSource = PatientIncidentUnderlyingCausesItemComponents;
        }

        protected void grdUnderlyingCauses_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String factorId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.FactorID]);
            String factorItemId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.FactorItemID]);
            String compId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.ComponentID]);
            PatientIncidentUnderlyingCausesItemComponent entity = FindUnderlyingCauses(factorId, factorItemId, compId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdUnderlyingCauses_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String factorId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.FactorID]);
            String factorItemId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.FactorItemID]);
            String compId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.ComponentID]);
            PatientIncidentUnderlyingCausesItemComponent entity = FindUnderlyingCauses(factorId, factorItemId, compId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdUnderlyingCauses_InsertCommand(object source, GridCommandEventArgs e)
        {
            PatientIncidentUnderlyingCausesItemComponent entity = PatientIncidentUnderlyingCausesItemComponents.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdUnderlyingCauses.Rebind();
        }

        private void SetEntityValue(PatientIncidentUnderlyingCausesItemComponent entity, GridCommandEventArgs e)
        {
            var userControl = (PatientIncidentUnderlyingCausesItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.FactorID = userControl.FactorID;
                entity.FactorName = userControl.FactorName;
                entity.FactorItemID = userControl.FactorItemID;
                entity.FactorItemName = userControl.FactorItemName;
                entity.ComponentID = userControl.ComponentID;
                entity.Component = userControl.Component;
                entity.ComponentName = userControl.ComponentName;
            }
        }

        #endregion

        #region Record Detail Method Function of Patient Incident Investigation

        private PatientIncidentInvestigationCollection PatientIncidentInvestigations
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPatientIncidentInvestigation"];
                    if (obj != null)
                    {
                        return ((PatientIncidentInvestigationCollection)(obj));
                    }
                }

                var coll = new PatientIncidentInvestigationCollection();
                var query = new PatientIncidentInvestigationQuery("a");

                query.Select
                    (
                        query

                    );

                query.Where(query.PatientIncidentNo == txtPatientIncidentNo.Text,
                            query.ServiceUnitID == cboServiceUnitRelatedUnitID.SelectedValue);
                coll.Load(query);

                Session["collPatientIncidentInvestigation"] = coll;
                return coll;
            }
            set
            {
                Session["collPatientIncidentInvestigation"] = value;
            }
        }

        private void RefreshCommandItemInvestigation(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdInvestigation.Columns[0].Visible = isVisible;
            grdInvestigation.Columns[grdInvestigation.Columns.Count - 1].Visible = isVisible;

            grdInvestigation.MasterTableView.CommandItemDisplay = isVisible
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdInvestigation.Rebind();
        }

        private void PopulateInvestigationGrid()
        {
            //Display Data Detail
            PatientIncidentInvestigations = null; //Reset Record Detail
            grdInvestigation.DataSource = PatientIncidentInvestigations; //Requery
            grdInvestigation.MasterTableView.IsItemInserted = false;
            grdInvestigation.MasterTableView.ClearEditItems();
            grdInvestigation.DataBind();
        }

        private PatientIncidentInvestigation FindInvestigation(String seqNo)
        {
            PatientIncidentInvestigationCollection coll = PatientIncidentInvestigations;
            PatientIncidentInvestigation retEntity = null;
            foreach (PatientIncidentInvestigation rec in coll)
            {
                if (rec.SeqNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdInvestigation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdInvestigation.DataSource = PatientIncidentInvestigations;
        }

        protected void grdInvestigation_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String seqNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PatientIncidentInvestigationMetadata.ColumnNames.SeqNo]);
            PatientIncidentInvestigation entity = FindInvestigation(seqNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdInvestigation_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][PatientIncidentInvestigationMetadata.ColumnNames.SeqNo]);
            PatientIncidentInvestigation entity = FindInvestigation(seqNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdInvestigation_InsertCommand(object source, GridCommandEventArgs e)
        {
            PatientIncidentInvestigation entity = PatientIncidentInvestigations.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdInvestigation.Rebind();
        }

        private void SetEntityValue(PatientIncidentInvestigation entity, GridCommandEventArgs e)
        {
            var userControl = (PatientIncidentInvestigationItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SeqNo = userControl.SeqNo;
                entity.Recomendation = userControl.Recomendation;
                entity.RecomendationDateTime = userControl.RecomendationDateTime;
                entity.PersonInCharge = userControl.PersonInCharge;
                entity.Implementation = userControl.Implementation;
                entity.ImplementationDateTime = userControl.ImplementationDateTime;
            }
        }

        #endregion

        #region Record Detail Method Function of Patient Incident Related Unit

        private PatientIncidentRelatedUnitCollection PatientIncidentRelatedUnits
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPatientIncidentRelatedUnit"];
                    if (obj != null)
                    {
                        return ((PatientIncidentRelatedUnitCollection)(obj));
                    }
                }

                var coll = new PatientIncidentRelatedUnitCollection();
                var query = new PatientIncidentRelatedUnitQuery("a");
                var su = new ServiceUnitQuery("b");

                query.Select
                    (
                        query,
                        su.ServiceUnitName.As("refToServiceUnit_ServiceUnitName")

                    );
                query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);
                query.Where(query.PatientIncidentNo == txtPatientIncidentNo.Text);
                coll.Load(query);

                Session["collPatientIncidentRelatedUnit"] = coll;
                return coll;
            }
            set
            {
                Session["collPatientIncidentRelatedUnit"] = value;
            }
        }

        private void PopulateRelatedUnitGrid()
        {
            //Display Data Detail
            PatientIncidentRelatedUnits = null; //Reset Record Detail
            grdRelatedUnit.DataSource = PatientIncidentRelatedUnits; //Requery
            grdRelatedUnit.MasterTableView.IsItemInserted = false;
            grdRelatedUnit.MasterTableView.ClearEditItems();
            grdRelatedUnit.DataBind();
        }

        protected void grdRelatedUnit_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRelatedUnit.DataSource = PatientIncidentRelatedUnits;
        }

        #endregion
    }
}
