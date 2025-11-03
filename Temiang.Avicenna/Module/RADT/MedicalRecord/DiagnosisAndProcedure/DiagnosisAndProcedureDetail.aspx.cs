using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;


namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class DiagnosisAndProcedureDetail : BasePageDetail
    {
        private string RegNo
        {
            get
            {
                return Request.QueryString["pid"];
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "##";
            UrlPageList = "DiagnosisAndProcedureList.aspx";

            ProgramID = AppConstant.Program.PatientDiagnosisAndProcedureEntry;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRDischargeMethod, AppEnum.StandardReference.DischargeMethod);
                StandardReference.InitializeIncludeSpace(cboSRDischargeCondition, AppEnum.StandardReference.DischargeCondition);

                PopulateRegistrationInformation(RegNo);
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuSearch.Visible = false;
            ToolBarMenuAdd.Visible = false;
            if (!this.IsUserEditAble)
                ToolBarMenuEdit.Visible = false;
        }

        private void PopulateRegistrationInformation(string registrationNo)
        {
            var registration = new Registration();
            if (registration.LoadByPrimaryKey(registrationNo))
            {
                txtRegistrationNo.Text = registrationNo;
                var patient = new Patient();
                if (patient.LoadByPrimaryKey(registration.PatientID))
                {
                    txtMedicalNo.Text = patient.MedicalNo;
                    var std = new AppStandardReferenceItem();
                    txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
                    txtPatientName.Text = patient.PatientName;
                    txtGender.Text = patient.Sex;
                    txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));
                    txtAgeDay.Value = Helper.GetAgeInDay(patient.DateOfBirth.Value);
                    txtAgeMonth.Value = Helper.GetAgeInMonth(patient.DateOfBirth.Value);
                    txtAgeYear.Value = Helper.GetAgeInYear(patient.DateOfBirth.Value);
                    txtAddress.Text = patient.Address;

                    PopulatePatientImage(registration.PatientID);
                }
                else
                {
                    txtMedicalNo.Text = string.Empty;
                    txtSalutation.Text = string.Empty;
                    txtPatientName.Text = string.Empty;
                    txtGender.Text = string.Empty;
                    txtPlaceDOB.Text = string.Empty;
                    txtAgeDay.Value = 0;
                    txtAgeMonth.Value = 0;
                    txtAgeYear.Value = 0;
                    txtAddress.Text = string.Empty;

                    PopulatePatientImage(string.Empty);
                }

                DateTime regDateTime = registration.RegistrationDate ?? DateTime.Now;
                txtRegistrationDateTime.SelectedDate = DateTime.Parse(regDateTime.ToShortDateString() + " " + registration.RegistrationTime);
                if (registration.DischargeDate != null)
                    txtDischargeDateTimeInfo.SelectedDate = registration.DischargeDate;
                else txtDischargeDateTimeInfo.Clear();

                txtParamedicID.Text = registration.ParamedicID;
                var par = new Paramedic();
                par.LoadByPrimaryKey(txtParamedicID.Text);
                lblParamedicName.Text = par.ParamedicName;

                txtServiceUnitID.Text = registration.ServiceUnitID;
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(txtServiceUnitID.Text);
                lblServiceUnitName.Text = unit.ServiceUnitName;
                txtRoomID.Text = registration.RoomID;

                var room = new ServiceRoom();
                room.LoadByPrimaryKey(txtRoomID.Text);
                lblRoomName.Text = room.RoomName;

                txtBedID.Text = registration.BedID;

                txtGuarantorID.Text = registration.GuarantorID;
                var guar = new Guarantor();
                guar.LoadByPrimaryKey(txtGuarantorID.Text);
                lblGuarantorName.Text = guar.GuarantorName;

                if (registration.DischargeDate != null)
                    txtDischargeDateTime.SelectedDate = registration.DischargeDate;
                else txtDischargeDateTime.Clear();
                
                cboSRDischargeMethod.SelectedValue = registration.SRDischargeMethod;
                cboSRDischargeCondition.SelectedValue = registration.SRDischargeCondition;
                txtDischargeMedicalNotes.Text = registration.DischargeMedicalNotes;
                txtDischargeNotes.Text = registration.DischargeNotes;
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtSalutation.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtGender.Text = string.Empty;
                txtPlaceDOB.Text = string.Empty;

                txtAgeDay.Value = 0;
                txtAgeMonth.Value = 0;
                txtAgeYear.Value = 0;

                txtRegistrationDateTime.Clear();
                txtDischargeDateTimeInfo.Clear();
                txtParamedicID.Text = string.Empty;
                lblParamedicName.Text = string.Empty;

                txtServiceUnitID.Text = string.Empty;
                lblServiceUnitName.Text = string.Empty;
                txtRoomID.Text = string.Empty;
                lblRoomName.Text = string.Empty;
                txtBedID.Text = registration.BedID;
                txtGuarantorID.Text = registration.GuarantorID;
                lblGuarantorName.Text = string.Empty;

                txtDischargeDateTime.Clear();
                cboSRDischargeMethod.SelectedValue = string.Empty;
                cboSRDischargeCondition.SelectedValue = string.Empty;
                txtDischargeMedicalNotes.Text = string.Empty;
                txtDischargeNotes.Text = string.Empty;

                PopulatePatientImage(string.Empty);
            }

            PopulateEpisodeDiagnoseGrid();
            PopulateEpisodeProcedureGrid();
            PopulateBookingNotesGrid();
            PopulateEpisodeProcedureDiagnosticGrid();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
        }

        protected override void OnMenuEditClick()
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
            if (AppSession.Parameter.IsMedRecCanChangePatientDischarge && !txtDischargeDateTime.IsEmpty)
            {
                if (string.IsNullOrEmpty(cboSRDischargeMethod.SelectedValue))
                {
                    args.MessageText = string.Format("Discharge Method required.");
                    args.IsCancel = true;
                    return;
                }

                if (string.IsNullOrEmpty(cboSRDischargeCondition.SelectedValue))
                {
                    args.MessageText = string.Format("Discharge Condition required.");
                    args.IsCancel = true;
                    return;
                }
            }

            var entity = new Registration();
            if (entity.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
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
            auditLogFilter.PrimaryKeyData = string.Format("RegistrationNo='{0}'", txtRegistrationNo.Text.Trim());
            auditLogFilter.TableName = "Registration";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdEpisodeDiagnose.Columns[0].Visible = isVisible;
            grdEpisodeDiagnose.Columns[grdEpisodeDiagnose.Columns.Count - 1].Visible = isVisible;
            grdEpisodeDiagnose.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            grdEpisodeProcedure.Columns[0].Visible = isVisible;
            grdEpisodeProcedure.Columns[grdEpisodeProcedure.Columns.Count - 1].Visible = isVisible;
            grdEpisodeProcedure.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            grdEpisodeProcedureDiagnostic.Columns[0].Visible = isVisible;
            grdEpisodeProcedureDiagnostic.Columns[grdEpisodeProcedureDiagnostic.Columns.Count - 1].Visible = isVisible;
            grdEpisodeProcedureDiagnostic.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
            {
                EpisodeDiagnoses = null;
                EpisodeProcedures = null;
                EpisodeProcedureDiagnostics = null;
            }

            //Perbaharui tampilan dan data
            grdEpisodeDiagnose.Rebind();
            grdEpisodeProcedure.Rebind();
            grdEpisodeProcedureDiagnostic.Rebind();

            if (AppSession.Parameter.IsMedRecCanChangePatientDischarge)
            {
                txtDischargeDateTime.Enabled = isVisible;
                cboSRDischargeMethod.Enabled = isVisible;
                cboSRDischargeCondition.Enabled = isVisible;
                txtDischargeMedicalNotes.ReadOnly = !isVisible;
                txtDischargeNotes.ReadOnly = !isVisible;
            }
            else
            {
                txtDischargeDateTime.Enabled = false;
                cboSRDischargeMethod.Enabled = false;
                cboSRDischargeCondition.Enabled = false;
                txtDischargeMedicalNotes.ReadOnly = true;
                txtDischargeNotes.ReadOnly = true;
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Registration();
            if (parameters.Length > 0)
            {
                var regNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(regNo);
            }
            else
                entity.LoadByPrimaryKey(txtRegistrationNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var reg = (Registration)entity;

            txtRegistrationNo.Text = reg.RegistrationNo;
            PopulateRegistrationInformation(txtRegistrationNo.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
        }
        #endregion

        #region Private Method Standard

        private void SetEntityValue(Registration entity)
        {
            //update registration
            if (!txtDischargeDateTime.IsEmpty)
            {
                entity.DischargeDate = txtDischargeDateTime.SelectedDate.Value.Date;
                entity.DischargeTime = txtDischargeDateTime.SelectedDate.Value.ToString("HH:mm");
            }
            entity.DischargeMedicalNotes = txtDischargeMedicalNotes.Text;
            entity.DischargeNotes = txtDischargeNotes.Text;
            entity.SRDischargeCondition = cboSRDischargeCondition.SelectedValue;
            entity.SRDischargeMethod = cboSRDischargeMethod.SelectedValue;
            entity.DischargeOperatorID = AppSession.UserLogin.UserID;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = new DateTime().NowAtSqlServer();
        }

        private void SaveEntity(Registration entity)
        {
            using (var trans = new esTransactionScope())
            {
                if (AppSession.Parameter.IsMedRecCanChangePatientDischarge && !txtDischargeDateTime.IsEmpty)
                {
                    entity.Save();

                    var dischargeHistory = new PatientDischargeHistory();
                    dischargeHistory.AddNew();
                    dischargeHistory.RegistrationNo = entity.RegistrationNo;
                    dischargeHistory.BedID = entity.BedID;
                    dischargeHistory.DischargeDate = entity.DischargeDate;
                    dischargeHistory.DischargeTime = entity.DischargeTime;
                    dischargeHistory.SRDischargeMethod = entity.SRDischargeMethod;
                    dischargeHistory.SRDischargeCondition = entity.SRDischargeCondition;
                    dischargeHistory.DischargeOperatorID = entity.DischargeOperatorID;
                    dischargeHistory.IsCancel = false;
                    dischargeHistory.LastUpdateDateTime = new DateTime().NowAtSqlServer();
                }
                EpisodeDiagnoses.Save();
                EpisodeProcedures.Save();
                EpisodeProcedureDiagnostics.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new RegistrationQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.RegistrationNo > txtRegistrationNo.Text
                    );
                que.OrderBy(que.RegistrationNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.RegistrationNo < txtRegistrationNo.Text
                    );
                que.OrderBy(que.RegistrationNo.Descending);
            }

            var entity = new Registration();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Detail Method Function SOAP
        private DataTable EpisodeSOAPEs
        {
            get
            {
                var query = new RegistrationInfoMedicQuery("a");
                var param = new ParamedicQuery("b");

                query.Select
                (
                    query,
                    param.ParamedicName
                );
                query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);
                query.Where(
                    query.RegistrationNo == Request.QueryString["pid"],
                    query.SRMedicalNotesInputType == "SOAP",
                    query.Or(query.IsDeleted.IsNull(), query.IsDeleted == false)
                );
                query.OrderBy(query.DateTimeInfo.Ascending);

                return query.LoadDataTable();
            }
        }

        protected void grdEpisodeSOAPE_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEpisodeSOAPE.DataSource = EpisodeSOAPEs;
        }
        
#endregion

        #region Record Detail Method Function Episode Diagnose

        private EpisodeDiagnoseCollection EpisodeDiagnoses
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEpisodeDiagnose" + Request.UserHostName];
                    if (obj != null)
                        return ((EpisodeDiagnoseCollection)(obj));
                }

                var coll = new EpisodeDiagnoseCollection();
                var query = new EpisodeDiagnoseQuery("a");
                var diag = new DiagnoseQuery("b");
                var item = new AppStandardReferenceItemQuery("e");
                var morph = new MorphologyQuery("c");
                var param = new ParamedicQuery("d");
                var usrc = new AppUserQuery("f");
                var usru = new AppUserQuery("g");

                query.LeftJoin(diag).On(query.DiagnoseID == diag.DiagnoseID);
                query.InnerJoin(item).On(query.SRDiagnoseType == item.ItemID);
                query.LeftJoin(morph).On(query.MorphologyID == morph.MorphologyID);
                query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);
                query.LeftJoin(usrc).On(usrc.UserID == query.CreateByUserID);
                query.LeftJoin(usru).On(usru.UserID == query.LastUpdateByUserID);

                query.Select
                    (
                        query.SelectAll(),
                        diag.DiagnoseName.As("refToDiagnose_DiagnoseName"),
                        item.ItemName.As("refToAppStandardReferenceItem_SRDiagnoseType"),
                        morph.MorphologyName.As("refToMorphology_MorphologyName"),
                        param.ParamedicName.As("refToParamedic_ParamedicName"),
                        usrc.UserName.As("refToAppUser_CreateByUserID"),
                        usru.UserName.As("refToAppUser_LastUpdateByUserID")
                    );

                query.Where(query.RegistrationNo == Request.QueryString["pid"]);
                query.OrderBy(query.SRDiagnoseType.Ascending, query.DiagnoseID.Ascending);

                coll.Load(query);

                Session["collEpisodeDiagnose" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collEpisodeDiagnose" + Request.UserHostName] = value; }
        }

        private void PopulateEpisodeDiagnoseGrid()
        {
            EpisodeDiagnoses = null; //Reset Record Detail
            grdEpisodeDiagnose.DataSource = EpisodeDiagnoses;
            grdEpisodeDiagnose.MasterTableView.IsItemInserted = false;
            grdEpisodeDiagnose.MasterTableView.ClearEditItems();
            grdEpisodeDiagnose.DataBind();
        }

        protected void grdEpisodeDiagnose_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEpisodeDiagnose.DataSource = EpisodeDiagnoses;
        }

        protected void grdEpisodeDiagnose_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EpisodeDiagnoseMetadata.ColumnNames.SequenceNo]);
            EpisodeDiagnose entity = FindEpisodeDiagnose(sequenceNo);
            if (entity != null)
                SetEntityValueDiagnose(entity, e);
        }

        protected void grdEpisodeDiagnose_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][EpisodeDiagnoseMetadata.ColumnNames.SequenceNo]);
            EpisodeDiagnose entity = FindEpisodeDiagnose(sequenceNo);
            if (entity != null)
                entity.IsVoid = true;
        }

        protected void grdEpisodeDiagnose_InsertCommand(object source, GridCommandEventArgs e)
        {
            EpisodeDiagnose entity = EpisodeDiagnoses.AddNew();
            SetEntityValueDiagnose(entity, e);

            e.Canceled = true;
            grdEpisodeDiagnose.Rebind();
        }

        private EpisodeDiagnose FindEpisodeDiagnose(String sequenceNo)
        {
            EpisodeDiagnoseCollection coll = EpisodeDiagnoses;
            return coll.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        private void SetEntityValueDiagnose(EpisodeDiagnose entity, GridCommandEventArgs e)
        {
            var userControl = (EpisodeDiagDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RegistrationNo = RegNo;
                entity.SequenceNo = userControl.SequenceNo;
                entity.DiagnoseID = userControl.DiagnoseID;
                entity.DiagnoseName = userControl.DiagnoseName;
                entity.SRDiagnoseType = userControl.SRDiagnoseType;
                entity.DiagnoseType = userControl.DiagnoseType;
                entity.DiagnosisText = userControl.DiagnosisText;
                entity.MorphologyID = userControl.MorphologyID;
                entity.MorphologyName = userControl.MorphologyName;
                entity.ParamedicID = userControl.ParamedicID;
                entity.ParamedicName = userControl.ParamedicName;
                entity.IsAcuteDisease = userControl.IsAcuteDisease;
                entity.IsChronicDisease = userControl.IsChronicDisease;
                entity.IsOldCase = userControl.IsOldCase;
                entity.IsConfirmed = userControl.IsConfirmed;
                entity.IsVoid = userControl.IsVoid;
                entity.Notes = userControl.Notes;
                entity.ExternalCauseID = userControl.ExternalCauseID;

                if (entity.es.IsAdded)
                {
                    entity.CreateByUserID = AppSession.UserLogin.UserID;
                    entity.CreateDateTime = (new DateTime()).NowAtSqlServer();
                }
                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
        }

        protected void grdEpisodeDiagnose_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                EpisodeDiagnose item = EpisodeDiagnoses[e.Item.DataSetIndex];
                if (item != null)
                {
                    if (item.IsVoid ?? false)
                    {
                        for (int i = 0; i < e.Item.Cells.Count; i++)
                        {
                            if (i > 0 && i < e.Item.Cells.Count)
                                e.Item.Cells[i].Font.Strikeout = true;
                        }
                    }
                }
            }
        }

        #endregion

        #region Record Detail Method Function Episode Procedure

        private EpisodeProcedureCollection EpisodeProcedures
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEpisodeProcedure" + Request.UserHostName];
                    if (obj != null)
                        return ((EpisodeProcedureCollection)(obj));
                }

                var coll = new EpisodeProcedureCollection();
                var query = new EpisodeProcedureQuery("a");
                var param = new ParamedicQuery("b");
                var proc = new ProcedureQuery("c");
                var usrc = new AppUserQuery("d");
                var usru = new AppUserQuery("e");

                query.LeftJoin(param).On(query.ParamedicID == param.ParamedicID);
                query.LeftJoin(proc).On(query.ProcedureID == proc.ProcedureID);
                query.LeftJoin(usrc).On(usrc.UserID == query.CreateByUserID);
                query.LeftJoin(usru).On(usru.UserID == query.LastUpdateByUserID);

                query.Select
                     (
                         query.SelectAll(),
                         param.ParamedicName.As("refToParamedic_ParamedicName"),
                         usrc.UserName.As("refToAppUser_CreateByUserID"),
                         usru.UserName.As("refToAppUser_LastUpdateByUserID")
                     );

                query.Where(query.RegistrationNo == Request.QueryString["pid"], query.IsFromOperatingRoom == true);
                query.OrderBy(query.SequenceNo.Ascending);

                coll.Load(query);

                Session["collEpisodeProcedure" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collEpisodeProcedure" + Request.UserHostName] = value; }
        }

        private void PopulateEpisodeProcedureGrid()
        {
            ////Display Data Detail
            //Session.Remove("collEpisodeProcedure"); //Reset Record Detail

            //grdEpisodeProcedure.MasterTableView.IsItemInserted = false;
            //grdEpisodeProcedure.MasterTableView.ClearEditItems();

            //grdEpisodeProcedure.Rebind(); //Ambil ulang record detail


            EpisodeProcedures = null; //Reset Record Detail
            grdEpisodeProcedure.DataSource = EpisodeProcedures;
            grdEpisodeProcedure.MasterTableView.IsItemInserted = false;
            grdEpisodeProcedure.MasterTableView.ClearEditItems();
            grdEpisodeProcedure.DataBind();
        }

        protected void grdEpisodeProcedure_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEpisodeProcedure.DataSource = EpisodeProcedures;
        }

        protected void grdEpisodeProcedure_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EpisodeProcedureMetadata.ColumnNames.SequenceNo]);
            EpisodeProcedure entity = FindEpisodeProcedure(sequenceNo);
            if (entity != null && entity.IsVoid == false)
                SetEntityValueEpisodeProcedure(entity, e);
        }

        protected void grdEpisodeProcedure_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            String sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][EpisodeProcedureMetadata.ColumnNames.SequenceNo]);
            EpisodeProcedure entity = FindEpisodeProcedure(sequenceNo);
            if (entity != null)
                entity.IsVoid = true;
        }

        protected void grdEpisodeProcedure_InsertCommand(object source, GridCommandEventArgs e)
        {
            EpisodeProcedure entity = EpisodeProcedures.AddNew();
            SetEntityValueEpisodeProcedure(entity, e);

            e.Canceled = true;
            grdEpisodeProcedure.Rebind();
        }

        private EpisodeProcedure FindEpisodeProcedure(String sequenceNo)
        {
            EpisodeProcedureCollection coll = EpisodeProcedures;
            return coll.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        private void SetEntityValueEpisodeProcedure(EpisodeProcedure entity, GridCommandEventArgs e)
        {
            var userControl = (EpisodeProcDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RegistrationNo = RegNo;
                entity.SequenceNo = userControl.SequenceNo;
                entity.ProcedureDate = userControl.ProcedureDate;
                entity.ProcedureTime = userControl.ProcedureTime;
                entity.ProcedureDate2 = userControl.ProcedureDate2;
                entity.ProcedureTime2 = userControl.ProcedureTime2;
                entity.IncisionDateTime = userControl.IncisionDateTime;
                entity.ParamedicID = userControl.ParamedicID;
                entity.ParamedicName = userControl.ParamedicName;
                entity.ParamedicID2 = userControl.ParamedicID2;
                entity.ProcedureID = userControl.ProcedureID;
                entity.ProcedureName = userControl.ProcedureName;
                entity.SRProcedureCategory = userControl.SRProcedureCategory;
                entity.SRAnestesi = userControl.SRAnestesi;
                entity.RoomID = userControl.RoomID;
                entity.IsCito = userControl.IsCito;
                entity.IsVoid = userControl.IsVoid;
                entity.AssistantID1 = userControl.AssistantID1;
                entity.AssistantID2 = userControl.AssistantID2;
                
                entity.ParamedicID2a = userControl.ParamedicID2a;
                entity.ParamedicID3a = userControl.ParamedicID3a;
                entity.ParamedicID4a = userControl.ParamedicID4a;
                entity.AssistantIDAnestesi = userControl.AssistantIDAnestesi;
                entity.AssistantIDAnestesi2 = userControl.AssistantIDAnestesi2;
                entity.InstrumentatorID1 = userControl.InstrumentatorID1;
                entity.InstrumentatorID2 = userControl.InstrumentatorID2;
                
                if (entity.es.IsAdded)
                {
                    entity.BookingNo = userControl.BookingNo;
                    entity.OpNotesSeqNo = userControl.OpNotesSeqNo;
                    entity.IsFromOperatingRoom = true;
                    entity.CreateByUserID = AppSession.UserLogin.UserID;
                    entity.CreateByUserName = AppSession.UserLogin.UserName;
                    entity.CreateDateTime = (new DateTime()).NowAtSqlServer();
                }
                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateByUserName = AppSession.UserLogin.UserName;
                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
        }

        protected void grdEpisodeProcedure_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                EpisodeProcedure item = EpisodeProcedures[e.Item.DataSetIndex];
                if (item != null)
                {
                    if (item.IsVoid ?? false)
                    {
                        for (int i = 0; i < e.Item.Cells.Count; i++)
                        {
                            if (i > 0 && i < e.Item.Cells.Count)
                                e.Item.Cells[i].Font.Strikeout = true;
                        }
                    }
                }
            }
        }

        #endregion

        #region Record Detail Booking
        private void PopulateBookingNotesGrid()
        {
            grdBookingNotes.DataSource = BookingNotes;
            grdBookingNotes.DataBind();
        }

        protected void grdBookingNotes_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdBookingNotes.DataSource = BookingNotes;
        }

        private DataTable BookingNotes
        {
            get
            {
                //// 1. Query ServiceUnitBooking untuk keperluan pengisian form2 Pra Operasi 
                var query = new ServiceUnitBookingQuery("a");
                var param = new ParamedicQuery("b");
                var reg = new RegistrationQuery("r");
                var paramAnes = new ParamedicQuery("c");

                query.InnerJoin(reg).On(reg.RegistrationNo == query.RegistrationNo);
                query.InnerJoin(param).On(param.ParamedicID == query.ParamedicID);
                query.LeftJoin(paramAnes).On(paramAnes.ParamedicID == query.ParamedicIDAnestesi);
                query.Select
                (
                    query.BookingNo,
                    query.RegistrationNo, query.RealizationDateTimeFrom.As("ProcedureDate"),
                    "<LEFT(CONVERT(VARCHAR, a.RealizationDateTimeFrom, 8), 5) as ProcedureTime>",
                    param.ParamedicName, "<'' as ProcedureName>", query.AnestesyNotes, query.Diagnose,
                    query.PostDiagnosis, query.Notes, paramAnes.ParamedicName.As("ParamedicAnestesiName")
                );

                query.Where(query.RegistrationNo == RegNo, query.IsApproved == true);
                query.OrderBy(query.BookingNo.Descending);

                return query.LoadDataTable();
            }
        }

        protected void grdBookingNotes_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "refresh")
            {
                grdEpisodeProcedure.DataSource = null;
                grdEpisodeProcedure.Rebind();
            }
        }

        private string _bookingNo = string.Empty;
        protected void grdBookingNotes_OnItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
                _bookingNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["BookingNo"]);

            if (e.Item is GridNestedViewItem)
            {
                // Populate
                var grd1 = (RadGrid)e.Item.FindControl("grdEpisodeProcedureDetail");
                grd1.DataSource = EpisodeProcedureDetails(_bookingNo);
                grd1.Rebind();

                var grd2 = (RadGrid)e.Item.FindControl("grdEpisodeProcedureDetailAns");
                grd2.DataSource = EpisodeProcedureAnsDetails(_bookingNo);
                grd2.Rebind();

                _bookingNo = string.Empty;
            }
        }

        private DataTable EpisodeProcedureDetails(string bookingNo)
        {
            var query = new ServiceUnitBookingOperatingNotesQuery("a");
            var sub = new ServiceUnitBookingQuery("b");
            var par = new ParamedicQuery("c");
            query.InnerJoin(sub).On(sub.BookingNo == query.BookingNo);
            query.InnerJoin(par).On(par.ParamedicID == query.ParamedicID);
            query.Select(sub.RegistrationNo, query.BookingNo, query.OpNotesSeqNo.As("SequenceNo"), query.ParamedicID,
                par.ParamedicName, query.Regio, "<'' AS ProcedureName>", query.OperatingNotes);
            query.Where(query.BookingNo == bookingNo, query.IsVoid == false);
            query.OrderBy(par.ParamedicName.Ascending, query.OpNotesSeqNo.Ascending);

            DataTable dtb = query.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
                var epcoll = new EpisodeProcedureCollection();
                epcoll.Query.Where(epcoll.Query.BookingNo == _bookingNo,
                    epcoll.Query.OpNotesSeqNo == row["SequenceNo"].ToString(), epcoll.Query.IsVoid == false);
                epcoll.LoadAll();
                var procedureName = string.Empty;
                foreach (var ep in epcoll)
                {
                    if (procedureName == string.Empty)
                        procedureName = "[" + ep.ProcedureID + "] " + ep.ProcedureName;
                    else
                        procedureName = procedureName + "; [" + ep.ProcedureID + "] " + ep.ProcedureName;
                }
                row["ProcedureName"] = procedureName;
            }
            dtb.AcceptChanges();

            return dtb;
        }

        private DataTable EpisodeProcedureAnsDetails(string bookingNo)
        {
            var query = new EpisodeProcedureQuery("a");
            var booking = new ServiceUnitBookingQuery("b");
            var usr = new AppUserQuery("c");
            query.InnerJoin(booking).On(booking.BookingNo == query.BookingNo);
            query.InnerJoin(usr).On(usr.UserID == query.CreateByUserID);
            query.Where(query.BookingNo == bookingNo, query.OpNotesSeqNo == string.Empty, usr.ParamedicID == booking.ParamedicIDAnestesi, query.IsVoid == false);
            query.Select(query);
            return query.LoadDataTable();
        }

        #endregion

        #region Record Detail Method Function Episode Procedure Diagnostoc

        private EpisodeProcedureCollection EpisodeProcedureDiagnostics
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEpisodeProcedureDiagnostic" + Request.UserHostName];
                    if (obj != null)
                        return ((EpisodeProcedureCollection)(obj));
                }

                var coll = new EpisodeProcedureCollection();
                var query = new EpisodeProcedureQuery("a");
                var param = new ParamedicQuery("b");
                var proc = new ProcedureQuery("c");
                var usrc = new AppUserQuery("d");
                var usru = new AppUserQuery("e");

                query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);
                query.InnerJoin(proc).On(query.ProcedureID == proc.ProcedureID);
                query.LeftJoin(usrc).On(usrc.UserID == query.CreateByUserID);
                query.LeftJoin(usru).On(usru.UserID == query.LastUpdateByUserID);

                query.Select
                     (
                         query.SelectAll(),
                         param.ParamedicName.As("refToParamedic_ParamedicName"),
                         usrc.UserName.As("refToAppUser_CreateByUserID"),
                         usru.UserName.As("refToAppUser_LastUpdateByUserID")
                     );

                query.Where(query.RegistrationNo == Request.QueryString["pid"], query.IsFromOperatingRoom == false);
                query.OrderBy(query.SequenceNo.Ascending);

                coll.Load(query);

                Session["collEpisodeProcedureDiagnostic" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collEpisodeProcedureDiagnostic" + Request.UserHostName] = value; }
        }

        private void PopulateEpisodeProcedureDiagnosticGrid()
        {
            EpisodeProcedureDiagnostics = null; //Reset Record Detail
            grdEpisodeProcedureDiagnostic.DataSource = EpisodeProcedureDiagnostics;
            grdEpisodeProcedureDiagnostic.MasterTableView.IsItemInserted = false;
            grdEpisodeProcedureDiagnostic.MasterTableView.ClearEditItems();
            grdEpisodeProcedureDiagnostic.DataBind();
        }

        protected void grdEpisodeProcedureDiagnostic_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEpisodeProcedureDiagnostic.DataSource = EpisodeProcedureDiagnostics;
        }

        protected void grdEpisodeProcedureDiagnostic_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EpisodeProcedureMetadata.ColumnNames.SequenceNo]);
            EpisodeProcedure entity = FindEpisodeProcedureDiagnostic(sequenceNo);
            if (entity != null)
                SetEntityValueEpisodeProcedureDiagnostic(entity, e);
        }

        protected void grdEpisodeProcedureDiagnostic_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            String sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][EpisodeProcedureMetadata.ColumnNames.SequenceNo]);
            EpisodeProcedure entity = FindEpisodeProcedureDiagnostic(sequenceNo);
            if (entity != null)
                entity.IsVoid = true;
        }

        protected void grdEpisodeProcedureDiagnostic_InsertCommand(object source, GridCommandEventArgs e)
        {
            EpisodeProcedure entity = EpisodeProcedureDiagnostics.AddNew();
            SetEntityValueEpisodeProcedureDiagnostic(entity, e);

            e.Canceled = true;
            grdEpisodeProcedureDiagnostic.Rebind();
        }

        private EpisodeProcedure FindEpisodeProcedureDiagnostic(String sequenceNo)
        {
            EpisodeProcedureCollection coll = EpisodeProcedureDiagnostics;
            return coll.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        private void SetEntityValueEpisodeProcedureDiagnostic(EpisodeProcedure entity, GridCommandEventArgs e)
        {
            var userControl = (EpisodeProcDiagnosticDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RegistrationNo = RegNo;
                entity.SequenceNo = userControl.SequenceNo;
                entity.ProcedureDate = userControl.ProcedureDate;
                entity.ProcedureTime = userControl.ProcedureTime;
                entity.ProcedureDate2 = userControl.ProcedureDate;
                entity.ProcedureTime2 = userControl.ProcedureTime;
                entity.IncisionDateTime = DateTime.Parse(userControl.ProcedureDate.ToShortDateString() + " " + userControl.ProcedureTime);
                entity.ParamedicID = userControl.ParamedicID;
                entity.ParamedicName = userControl.ParamedicName;
                entity.ParamedicID2 = string.Empty;
                entity.ProcedureID = userControl.ProcedureID;
                entity.ProcedureName = userControl.ProcedureName;
                entity.SRProcedureCategory = string.Empty;
                entity.SRAnestesi = string.Empty;
                entity.RoomID = string.Empty;
                entity.IsCito = false;
                entity.IsVoid = false;
                entity.AssistantID1 = string.Empty;
                entity.AssistantID2 = string.Empty;
                entity.BookingNo = string.Empty;
                entity.ParamedicID2a = string.Empty;
                entity.ParamedicID3a = string.Empty;
                entity.ParamedicID4a = string.Empty;
                entity.IsFromOperatingRoom = false;
                entity.OpNotesSeqNo = string.Empty;

                if (entity.es.IsAdded)
                {
                    entity.CreateByUserID = AppSession.UserLogin.UserID;
                    entity.CreateByUserName = AppSession.UserLogin.UserName;
                    entity.CreateDateTime = (new DateTime()).NowAtSqlServer();
                }
                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateByUserName = AppSession.UserLogin.UserName;
                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
        }

        protected void grdEpisodeProcedureDiagnostic_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                EpisodeProcedure item = EpisodeProcedureDiagnostics[e.Item.DataSetIndex];
                if (item != null)
                {
                    if (item.IsVoid ?? false)
                    {
                        for (int i = 0; i < e.Item.Cells.Count; i++)
                        {
                            if (i > 0 && i < e.Item.Cells.Count)
                                e.Item.Cells[i].Font.Strikeout = true;
                        }
                    }
                }
            }
        }

        #endregion

        #region PatientImage
        private void PopulatePatientImage(string patientID)
        {
            // Load from database
            var patientImg = new PatientImage();
            if (patientImg.LoadByPrimaryKey(patientID))
            {
                // Show Image
                if (patientImg.Photo != null)
                {
                    imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(patientImg.Photo));
                }
                else
                {
                    imgPatientPhoto.ImageUrl = txtGender.Text == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                }
            }
            else
                imgPatientPhoto.ImageUrl = txtGender.Text == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";

        }
        #endregion
    }
}