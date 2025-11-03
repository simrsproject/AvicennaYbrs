using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.HR.K3RS
{
    public partial class NeedleStickInjuryDetail : BasePageDetail
    {
        private string FormType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? "" : Request.QueryString["type"];
            }
        }

        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "##";
            UrlPageList = FormType == "" ? "NeedleStickInjuryList.aspx" + FormType : "NeedleStickInjuryFollowUpList.aspx";
            ProgramID = FormType == "" ? AppConstant.Program.K3RS_EmployeeNeedleStickInjury : AppConstant.Program.K3RS_EmployeeNeedleStickInjuryFollowUp;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSREmployeeIncidentType, AppEnum.StandardReference.EmployeeIncidentType);
                StandardReference.InitializeIncludeSpace(cboSRNeedleType, AppEnum.StandardReference.NeedleType);
                StandardReference.InitializeIncludeSpace(cboSREmployeeInjuryCategory, AppEnum.StandardReference.EmployeeInjuryCategory);

                if (FormType == "" && AppSession.Parameter.IsUsingEmployeeNeedleStickInjuryFollowUp)
                {
                    rfvFollowUpDate.Visible = false;
                    rfvFollowUp.Visible = false;
                    rfvFollowUpBy.Visible = false;
                }
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboPatientID, cboPatientID);
            ajax.AddAjaxSetting(cboPatientID, txtMedicalNo);
            ajax.AddAjaxSetting(cboPatientID, cboRegistrationNo);
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new EmployeeNeedleStickInjury());

            txtTransactionNo.Text = GetNewTransactionNo();
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            txtReferenceNo.Text = Request.QueryString["rno"];
            GetIncidentInfo(txtReferenceNo.Text);

            if (FormType == "" && AppSession.Parameter.IsUsingEmployeeNeedleStickInjuryFollowUp)
            {
                txtFollowUpNotes.ReadOnly = true;
                txtFollowUpDate.Enabled = false;
                txtFollowUpBy.ReadOnly = true;
            }
        }

        protected override void OnMenuEditClick()
        {
            if (FormType == "ver")
            {
                cboPatientID.Enabled = false;
                cboRegistrationNo.Enabled = false;
                txtExposedArea.ReadOnly = true;
                txtReason.ReadOnly = true;
                chkIsBlood.Enabled = false;
                chkIsFluidSperm.Enabled = false;
                chkIsVaginalSecretions.Enabled = false;
                chkIsCerebrospinal.Enabled = false;
                chkIsUrine.Enabled = false;
                chkIsFaeces.Enabled = false;
                chkIsOfficerHiv.Enabled = false;
                chkIsOfficerHbv.Enabled = false;
                chkIsOfficerHcv.Enabled = false;
                txtOfficerImunizationHistory.ReadOnly = true;
                txtChronology.ReadOnly = true;
                txtRecomendation.ReadOnly = true;
                txtDiagnose.ReadOnly = true;
                chkIsPatientHiv.Enabled = false;
                chkIsPatientHbv.Enabled = false;
                chkIsPatientHcv.Enabled = false;
                txtPatientImunizationHistory.ReadOnly = true;
                txtKnownBy.ReadOnly = true;
                chkIsUsingApd.Enabled = false;
                chkIsSpo.Enabled = false;
            }
            else if (FormType == "" && AppSession.Parameter.IsUsingEmployeeNeedleStickInjuryFollowUp)
            {
                txtFollowUpNotes.ReadOnly = true;
                txtFollowUpDate.Enabled = false;
                txtFollowUpBy.ReadOnly = true;
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new EmployeeNeedleStickInjury();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (!IsApproved(entity, args))
                    return;

                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (!chkIsBlood.Checked && !chkIsFluidSperm.Checked && !chkIsVaginalSecretions.Checked && !chkIsCerebrospinal.Checked && !chkIsUrine.Checked && !chkIsFaeces.Checked)
            {
                args.MessageText = "Source Of Exposure required.";
                args.IsCancel = true;
                return;
            }

            var entity = new EmployeeNeedleStickInjury();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new EmployeeNeedleStickInjury();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text);
            auditLogFilter.TableName = "EmployeeNeedleStickInjury";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            if (FormType == "ver")
            {
                if (txtFollowUpDate.IsEmpty)
                {
                    args.MessageText = "Follow Up Date required.";
                    args.IsCancel = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtFollowUpNotes.Text))
                {
                    args.MessageText = "Follow Up required.";
                    args.IsCancel = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtFollowUpBy.Text))
                {
                    args.MessageText = "Follow Up By required.";
                    args.IsCancel = true;
                    return;
                }
            }

            using (var trans = new esTransactionScope())
            {
                var entity = new EmployeeNeedleStickInjury();
                entity.LoadByPrimaryKey(txtTransactionNo.Text);

                entity.IsApproved = true;
                entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;

                if (FormType == "ver")
                {
                    entity.IsVerified = true;
                    entity.VerifiedDateTime = (new DateTime()).NowAtSqlServer();
                    entity.VerifiedByUserID = AppSession.UserLogin.UserID;
                }

                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new EmployeeNeedleStickInjury();
            entity.LoadByPrimaryKey(txtTransactionNo.Text);

            if (FormType == "")
            {
                if (entity.IsVerified == true)
                {
                    args.MessageText = "Data already verified.";
                    args.IsCancel = true;
                    return;
                }
            }

            using (var trans = new esTransactionScope())
            {
                if (FormType == "ver")
                {
                    entity.IsVerified = false;
                    entity.VerifiedDateTime = null;
                    entity.VerifiedByUserID = null;
                }
                else
                {
                    entity.IsApproved = false;
                    entity.ApprovedDateTime = null;
                    entity.ApprovedByUserID = null;
                }

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
            var entity = new EmployeeNeedleStickInjury();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
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

        private bool IsApprovedOrVoid(EmployeeNeedleStickInjury entity, ValidateArgs args)
        {
            if (FormType == "ver")
            {
                if (entity.IsVerified ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }
            else
            {
                if (entity.IsApproved ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }

            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        private bool IsApproved(EmployeeNeedleStickInjury entity, ValidateArgs args)
        {
            if (entity.IsApproved ?? false)
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

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new EmployeeNeedleStickInjury();
            if (parameters.Length > 0)
            {
                string transNo = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transNo);

                txtTransactionNo.Text = entity.TransactionNo;
            }
            else
            {
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var ppi = (EmployeeNeedleStickInjury)entity;

            txtTransactionNo.Text = ppi.TransactionNo;
            txtTransactionDate.SelectedDate = ppi.TransactionDate;
            txtReferenceNo.Text = ppi.ReferenceNo;
            
            GetIncidentInfo(txtReferenceNo.Text);

            txtExposedArea.Text = ppi.ExposedArea;
            txtReason.Text = ppi.Reason;
            chkIsBlood.Checked = ppi.IsBlood ?? false;
            chkIsFluidSperm.Checked = ppi.IsFluidSperm ?? false;
            chkIsVaginalSecretions.Checked = ppi.IsVaginalSecretions ?? false;
            chkIsCerebrospinal.Checked = ppi.IsCerebrospinal ?? false;
            chkIsUrine.Checked = ppi.IsUrine ?? false;
            chkIsFaeces.Checked = ppi.IsFaeces ?? false;
            chkIsOfficerHiv.Checked = ppi.IsOfficerHiv ?? false;
            chkIsOfficerHbv.Checked = ppi.IsOfficerHbv ?? false;
            chkIsOfficerHcv.Checked = ppi.IsOfficerHcv ?? false;
            txtOfficerImunizationHistory.Text = ppi.OfficerImunizationHistory;
            if (!string.IsNullOrEmpty(ppi.Chronology))
                txtChronology.Text =  ppi.Chronology;

            if (!string.IsNullOrEmpty(ppi.PatientID))
            {
                var dtbPatient = (new PatientCollection()).PatientRegisterAble(ppi.PatientID, string.Empty, string.Empty, string.Empty, 10);
                cboPatientID.DataSource = dtbPatient;
                cboPatientID.DataBind();
                cboPatientID.SelectedValue = ppi.PatientID;

                var patient = new Patient();
                if (patient.LoadByPrimaryKey(ppi.PatientID))
                    txtMedicalNo.Text = patient.MedicalNo;
                else
                    txtMedicalNo.Text = string.Empty;
            }
            else
            {
                cboPatientID.Items.Clear();
                cboPatientID.SelectedValue = string.Empty;
                cboPatientID.Text = string.Empty;

                txtMedicalNo.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(ppi.RegistrationNo))
            {
                var qreg = new RegistrationQuery("a");
                var qpat = new PatientQuery("b");
                var qunit = new ServiceUnitQuery("c");
                var qroom = new ServiceRoomQuery("d");
                var qbed = new BedQuery("e");

                qreg.Select(
                    qreg.RegistrationNo,
                    qreg.BedID,
                    qpat.PatientID,
                    qpat.MedicalNo,
                    qpat.PatientName,
                    qunit.ServiceUnitName,
                    qroom.RoomName
                    );
                qreg.InnerJoin(qpat).On(qreg.PatientID == qpat.PatientID);
                qreg.InnerJoin(qunit).On(qreg.ServiceUnitID == qunit.ServiceUnitID);
                qreg.LeftJoin(qroom).On(qreg.RoomID == qroom.RoomID);
                qreg.LeftJoin(qbed).On(qreg.RegistrationNo == qbed.RegistrationNo);
                qreg.Where(qreg.RegistrationNo == ppi.RegistrationNo);

                cboRegistrationNo.DataSource = qreg.LoadDataTable();
                cboRegistrationNo.DataBind();
                cboRegistrationNo.SelectedValue = ppi.RegistrationNo;
            }
            else
            {
                cboRegistrationNo.Items.Clear();
                cboRegistrationNo.SelectedValue = string.Empty;
                cboRegistrationNo.Text = string.Empty;
            }
            
            txtDiagnose.Text = ppi.Diagnose;
            chkIsPatientHiv.Checked = ppi.IsPatientHiv ?? false;
            chkIsPatientHbv.Checked = ppi.IsPatientHbv ?? false;
            chkIsPatientHcv.Checked = ppi.IsPatientHcv ?? false;
            txtPatientImunizationHistory.Text = ppi.PatientImunizationHistory;
            txtKnownBy.Text = ppi.KnownBy;
            chkIsSpo.Checked = ppi.IsSpo ?? false;
            chkIsUsingApd.Checked = ppi.IsUsingApd ?? false;
            txtRecomendation.Text = ppi.Recomendation;

            txtFollowUpNotes.Text = ppi.FollowUpNotes;
            txtFollowUpDate.SelectedDate = ppi.FollowUpDate;
            txtFollowUpBy.Text = ppi.FollowUpBy;

            if (FormType == "ver")
                chkIsApproved.Checked = ppi.IsVerified ?? false;
            else
                chkIsApproved.Checked = ppi.IsApproved ?? false;
            chkIsVoid.Checked = ppi.IsVoid ?? false;
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(EmployeeNeedleStickInjury entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewTransactionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.ReferenceNo = txtReferenceNo.Text;
            entity.ExposedArea = txtExposedArea.Text;
            entity.Reason = txtReason.Text;
            entity.IsBlood = chkIsBlood.Checked;
            entity.IsFluidSperm = chkIsFluidSperm.Checked;
            entity.IsVaginalSecretions = chkIsVaginalSecretions.Checked;
            entity.IsCerebrospinal = chkIsCerebrospinal.Checked;
            entity.IsUrine = chkIsUrine.Checked;
            entity.IsFaeces = chkIsFaeces.Checked;
            entity.IsOfficerHiv = chkIsOfficerHiv.Checked;
            entity.IsOfficerHbv = chkIsOfficerHbv.Checked;
            entity.IsOfficerHcv = chkIsOfficerHcv.Checked;
            entity.OfficerImunizationHistory = txtOfficerImunizationHistory.Text;
            entity.Chronology = txtChronology.Text;
            entity.Recomendation = txtRecomendation.Text;
            entity.PatientID = cboPatientID.SelectedValue;
            entity.RegistrationNo = cboRegistrationNo.SelectedValue;
            entity.Diagnose = txtDiagnose.Text;
            entity.IsPatientHiv = chkIsPatientHiv.Checked;
            entity.IsPatientHbv = chkIsPatientHbv.Checked;
            entity.IsPatientHcv = chkIsPatientHcv.Checked;
            entity.PatientImunizationHistory = txtPatientImunizationHistory.Text;
            entity.IsSpo = chkIsSpo.Checked;
            entity.IsUsingApd = chkIsUsingApd.Checked;
            entity.KnownBy = txtKnownBy.Text;
            entity.IsVoid = false;
           
            if (!txtFollowUpDate.IsEmpty)
                entity.FollowUpDate = txtFollowUpDate.SelectedDate;
            else entity.str.FollowUpDate = string.Empty;

            entity.FollowUpNotes = txtFollowUpNotes.Text;
            entity.FollowUpBy = txtFollowUpBy.Text;

            if (entity.es.IsAdded)
            {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
            else if (entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(EmployeeNeedleStickInjury entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new EmployeeNeedleStickInjuryQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TransactionNo > txtTransactionNo.Text);
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < txtTransactionNo.Text);
                que.OrderBy(que.TransactionNo.Descending);
            }
            if (FormType == "")
                que.Where(que.CreatedByUserID == AppSession.UserLogin.UserID);
            else
                que.Where(que.IsApproved == true);

            var entity = new EmployeeNeedleStickInjury();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }
        #endregion

        #region Override Method & Function
        #endregion

        #region Combobox
        protected void cboPatientID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                var patient = new Patient();
                if (patient.LoadByPrimaryKey(e.Value))
                {
                    txtMedicalNo.Text = patient.MedicalNo;
                }
                else
                    txtMedicalNo.Text = string.Empty;
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
            }
            cboRegistrationNo.Items.Clear();
            cboRegistrationNo.Text = string.Empty;
        }

        protected void cboPatientID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var dtbPatient = (new PatientCollection()).PatientRegisterAble(e.Text, string.Empty, string.Empty, string.Empty, 10);
            cboPatientID.DataSource = dtbPatient;
            cboPatientID.DataBind();
        }

        protected void cboPatientID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PatientName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
        }

        protected void cboRegistrationNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var reg = new RegistrationQuery("a");
            var pat = new PatientQuery("b");
            var unit = new ServiceUnitQuery("c");
            var room = new ServiceRoomQuery("d");
            var bed = new BedQuery("e");

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
            reg.LeftJoin(bed).On(reg.RegistrationNo == bed.RegistrationNo);
            reg.Where(
                reg.IsVoid == false,
                reg.IsConsul == false,
                reg.PatientID == cboPatientID.SelectedValue,
                reg.IsFromDispensary == false,
                reg.IsDirectPrescriptionReturn == false,
                reg.IsNonPatient == false
                );

            if (e.Text.Trim().Contains(" "))
            {
                var searchs = e.Text.Trim().Split(' ');
                foreach (var search in searchs)
                {
                    var searchLike = "%" + search + "%";
                    reg.Where(reg.RegistrationNo.Like(searchLike));
                }
            }
            else
            {
                string searchTextContain = string.Format("%{0}%", e.Text);
                reg.Where(reg.RegistrationNo.Like(searchTextContain));
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

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.EmployeeNsiNo);

            return _autoNumber.LastCompleteNumber;
        }

        private void GetIncidentInfo(string transactionNo)
        {
            var incident = new EmployeeAccidentReports();
            if (incident.LoadByPrimaryKey(transactionNo))
            {
                var emp = new VwEmployeeTable();
                emp.Query.Where(emp.Query.PersonID == incident.PersonID);
                emp.Query.Load();

                txtEmployeeName.Text = emp.EmployeeName;
                txtEmployeeNumber.Text = emp.EmployeeNumber;
                txtDateOfBirth.SelectedDate = emp.BirthDate;
                txtSex.Text = emp.SRGenderType;

                txtAgeInYear.Value = Convert.ToDouble(incident.AgeInYear);
                txtAgeInMonth.Value = Convert.ToDouble(incident.AgeInMonth);
                txtAgeInDay.Value = Convert.ToDouble(incident.AgeInDay);

                var emptype = new AppStandardReferenceItem();
                if (emptype.LoadByPrimaryKey(AppEnum.StandardReference.EmployeeType.ToString(), emp.SREmployeeType))
                    txtEmployeeType.Text = emptype.ItemName;
                else
                    txtEmployeeType.Text = string.Empty;

                var position = new Position();
                if (position.LoadByPrimaryKey(incident.PositionID.ToInt()))
                    txtPositionName.Text = position.PositionName;
                else
                    txtPositionName.Text = string.Empty;

                txtIncidentDate.SelectedDate = incident.IncidentDateTime.Value;
                txtIncidentTime.SelectedDate = incident.IncidentDateTime.Value;
                txtReportingDate.SelectedDate = incident.ReportingDateTime.Value;
                txtReportingTime.SelectedDate = incident.ReportingDateTime;

                cboSREmployeeIncidentType.SelectedValue = incident.SREmployeeIncidentType;
                cboSRNeedleType.SelectedValue = incident.SRNeedleType;
                cboSREmployeeInjuryCategory.SelectedValue = incident.SREmployeeInjuryCategory;
                txtLossTime.Value = Convert.ToDouble(incident.LossTime);
                txtInjuredLocation.Text = incident.InjuredLocation;
                txtChronology.Text = incident.ChronologicalEvents;
            }
            else
            {
                txtEmployeeName.Text = string.Empty;
                txtEmployeeNumber.Text = string.Empty;
                txtDateOfBirth.SelectedDate = null;
                txtSex.Text = string.Empty;

                txtAgeInYear.Value = 0;
                txtAgeInMonth.Value = 0;
                txtAgeInDay.Value = 0;

                txtEmployeeType.Text = string.Empty;
                txtPositionName.Text = string.Empty;

                txtIncidentDate.SelectedDate = null;
                txtIncidentTime.SelectedDate = null;
                txtReportingDate.SelectedDate = null;
                txtReportingTime.SelectedDate = null;

                cboSREmployeeIncidentType.SelectedValue = string.Empty;
                cboSREmployeeIncidentType.Text = string.Empty;
                cboSRNeedleType.SelectedValue = string.Empty;
                cboSRNeedleType.Text = string.Empty;
                cboSREmployeeInjuryCategory.SelectedValue = string.Empty;
                cboSREmployeeInjuryCategory.Text = string.Empty;
                txtLossTime.Value = 0;
                txtInjuredLocation.Text = string.Empty;
                txtChronology.Text = string.Empty;
            }
        }
    }
}