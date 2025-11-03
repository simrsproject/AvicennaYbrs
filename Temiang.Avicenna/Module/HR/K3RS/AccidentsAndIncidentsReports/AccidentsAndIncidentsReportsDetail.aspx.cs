using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.K3RS
{
    public partial class AccidentsAndIncidentsReportsDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private string _type
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        #region Page Event & Initialize
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 400;

            // Url Search & List
            if (_type == "ver")
                UrlPageSearch = "AccidentsAndIncidentsReportsSearch.aspx";
            else UrlPageSearch = "##";

            UrlPageList = _type == "" ? "AccidentsAndIncidentsReportsList.aspx" : "AccidentsAndIncidentsReportsVerificationList.aspx";

            ProgramID = _type == "" ? AppConstant.Program.K3RS_EmployeeIncident : AppConstant.Program.K3RS_EmployeeIncidentVerification;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSREmployeeIncidentStatus, AppEnum.StandardReference.EmployeeIncidentStatus);
                StandardReference.InitializeIncludeSpace(cboSREmployeeIncidentType, AppEnum.StandardReference.EmployeeIncidentType);
                StandardReference.InitializeIncludeSpace(cboSREmployeeInjuryCategory, AppEnum.StandardReference.EmployeeInjuryCategory);
                StandardReference.InitializeIncludeSpace(cboSRNeedleType, AppEnum.StandardReference.NeedleType);
                StandardReference.InitializeIncludeSpace(cboSREmployeeAccidentReportStatus, AppEnum.StandardReference.EmployeeAccidentReportStatus);

                trEmployeeAccidentReportStatus.Visible = (_type != "");
                rfvSREmployeeAccidentReportStatus.Visible = (_type != "");
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboPersonID, cboPersonID);
            ajax.AddAjaxSetting(cboPersonID, txtEmployeeNumber);
            ajax.AddAjaxSetting(cboPersonID, txtDateOfBirth);
            ajax.AddAjaxSetting(cboPersonID, txtAgeInYear);
            ajax.AddAjaxSetting(cboPersonID, txtAgeInMonth);
            ajax.AddAjaxSetting(cboPersonID, txtAgeInDay);
            ajax.AddAjaxSetting(cboPersonID, txtSex);
            ajax.AddAjaxSetting(cboPersonID, cboPositionID);

            ajax.AddAjaxSetting(txtIncidentDate, txtTransactionNo);
            ajax.AddAjaxSetting(txtIncidentDate, txtAgeInYear);
            ajax.AddAjaxSetting(txtIncidentDate, txtAgeInMonth);
            ajax.AddAjaxSetting(txtIncidentDate, txtAgeInDay);
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new EmployeeAccidentReports());

            PopulateNewNo();
            cboSREmployeeIncidentStatus.SelectedValue = string.Empty;
            cboSREmployeeIncidentStatus.Text = string.Empty;
            cboSREmployeeIncidentType.SelectedValue = string.Empty;
            cboSREmployeeIncidentType.Text = string.Empty;
            cboSRNeedleType.SelectedValue = string.Empty;
            cboSRNeedleType.Text = string.Empty;
            cboSREmployeeInjuryCategory.SelectedValue = string.Empty;
            cboSREmployeeInjuryCategory.Text = string.Empty;
            cboSREmployeeAccidentReportStatus.SelectedValue = string.Empty;
            cboSREmployeeAccidentReportStatus.Text = string.Empty;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new EmployeeAccidentReports();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (!IsApproved(entity, args))
                    return;

                entity.MarkAsDeleted();

                SaveEntity(entity);
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (cboSREmployeeIncidentType.SelectedValue == AppSession.Parameter.EmployeeIncidentTypeNSI && string.IsNullOrEmpty(cboSRNeedleType.SelectedValue))
            {
                args.MessageText = "Needle Type is required.";
                args.IsCancel = true;
                return;
            }

            var entity = new EmployeeAccidentReports();
            entity.AddNew();
            PopulateNewNo();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (cboSREmployeeIncidentType.SelectedValue == AppSession.Parameter.EmployeeIncidentTypeNSI && string.IsNullOrEmpty(cboSRNeedleType.SelectedValue))
            {
                args.MessageText = "Needle Type is required.";
                args.IsCancel = true;
                return;
            }

            var entity = new EmployeeAccidentReports();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "EmployeeAccidentReports";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                var entity = new EmployeeAccidentReports();
                entity.LoadByPrimaryKey(txtTransactionNo.Text);

                if (_type == "")
                {
                    entity.IsApproved = true;
                    entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                }
                else
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
            var entity = new EmployeeAccidentReports();
            entity.LoadByPrimaryKey(txtTransactionNo.Text);

            if (_type == "" && (entity.IsVerified ?? false))
            {
                args.MessageText = AppConstant.Message.RecordHasVerified;
                args.IsCancel = true;
                return;
            }

            var nsi = new EmployeeNeedleStickInjuryCollection();
            nsi.Query.Where(nsi.Query.ReferenceNo == entity.TransactionNo, nsi.Query.IsVoid == false);
            nsi.LoadAll();
            if (nsi.Count > 0)
            {
                args.MessageText = AppConstant.Message.RecordHasUsed;
                args.IsCancel = true;
                return;
            }

            using (var trans = new esTransactionScope())
            {
                if (_type == "")
                {
                    entity.IsApproved = false;
                    entity.ApprovedDateTime = null;
                    entity.ApprovedByUserID = null;
                }
                else
                {
                    entity.IsVerified = false;
                    entity.VerifiedDateTime = null;
                    entity.VerifiedByUserID = null;
                }

                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new EmployeeAccidentReports();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (!IsVoid(entity, args))
                    return;

                entity.IsVoid = true;
                entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
                entity.VoidByUserID = AppSession.UserLogin.UserID;

                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                SaveEntity(entity);
            }
        }


        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new EmployeeAccidentReports();
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

        private bool IsApprovedOrVoid(EmployeeAccidentReports entity, ValidateArgs args)
        {
            if (_type == "")
            {
                if (entity.IsApproved ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }
            else
            {
                if (entity.IsVerified ?? false)
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

        private bool IsApproved(EmployeeAccidentReports entity, ValidateArgs args)
        {
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        private bool IsVoid(EmployeeAccidentReports entity, ValidateArgs args)
        {
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            
            if (_type == "ver")
            {
                ToolBarMenuAdd.Enabled = false;
                ToolBarMenuSearch.Enabled = false;
            }
        }

        public override bool OnGetStatusMenuEdit()
        {
            return txtTransactionNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !chkIsVoid.Checked;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new EmployeeAccidentReports();
            if (parameters.Length > 0)
            {
                String transNo = (String)parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transNo);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var pi = (EmployeeAccidentReports)entity;
            txtTransactionNo.Text = pi.TransactionNo;

            txtIncidentDate.SelectedDate = pi.IncidentDateTime.HasValue ? pi.IncidentDateTime.Value : (new DateTime()).NowAtSqlServer();
            txtIncidentTime.SelectedDate = pi.IncidentDateTime.HasValue ? pi.IncidentDateTime.Value : (new DateTime()).NowAtSqlServer();
            txtIncidentLocation.Text = pi.IncidentLocation;
            txtReportingDate.SelectedDate = pi.ReportingDateTime.HasValue ? pi.ReportingDateTime.Value : (new DateTime()).NowAtSqlServer();
            txtReportingTime.SelectedDate = pi.ReportingDateTime.HasValue ? pi.ReportingDateTime.Value : (new DateTime()).NowAtSqlServer();
            txtChronology.Text = pi.ChronologicalEvents;
            txtAspectsOfTheCause.Text = pi.AspectsOfTheCause;

            if (!string.IsNullOrEmpty(pi.TransactionNo))
            {
                var empq = new VwEmployeeTableQuery();
                empq.Where(empq.PersonID == Convert.ToInt32(pi.PersonID));
                var dtb = empq.LoadDataTable();
                if (dtb.Rows.Count > 0)
                {
                    cboPersonID.DataSource = dtb;
                    cboPersonID.DataBind();
                    cboPersonID.SelectedValue = pi.PersonID.ToString();
                    txtEmployeeNumber.Text = dtb.Rows[0]["EmployeeNumber"].ToString();
                    txtDateOfBirth.SelectedDate = Convert.ToDateTime(dtb.Rows[0]["BirthDate"]);
                    txtSex.Text = dtb.Rows[0]["SRGenderType"].ToString() == "F" ? "Female" : "Male";
                }
                else
                {
                    cboPersonID.Items.Clear();
                    cboPersonID.SelectedValue = string.Empty;
                    cboPersonID.Text = string.Empty;
                    txtEmployeeNumber.Text = string.Empty;
                    txtDateOfBirth.SelectedDate = null;
                    txtSex.Text = string.Empty;
                }

                var posq = new PositionQuery();
                posq.Where(posq.PositionID == Convert.ToInt32(pi.PositionID));
                var dtb2 = posq.LoadDataTable();
                if (dtb2.Rows.Count > 0)
                {
                    cboPositionID.DataSource = dtb2;
                    cboPositionID.DataBind();
                    cboPositionID.SelectedValue = pi.PositionID.ToString();
                }
                else
                {
                    cboPositionID.Items.Clear();
                    cboPositionID.SelectedValue = string.Empty;
                    cboPositionID.Text = string.Empty;
                }
            }
            else
            {
                cboPersonID.Items.Clear();
                cboPersonID.SelectedValue = string.Empty;
                cboPersonID.Text = string.Empty;

                txtEmployeeNumber.Text = string.Empty;
                txtDateOfBirth.SelectedDate = null;
                txtSex.Text = string.Empty;

                cboPositionID.Items.Clear();
                cboPositionID.SelectedValue = string.Empty;
                cboPositionID.Text = string.Empty;

            }

            txtAgeInYear.Value = Convert.ToDouble(pi.AgeInYear);
            txtAgeInMonth.Value = Convert.ToDouble(pi.AgeInMonth);
            txtAgeInDay.Value = Convert.ToDouble(pi.AgeInDay);

            cboSREmployeeIncidentType.SelectedValue = pi.SREmployeeIncidentType;
            cboSRNeedleType.SelectedValue = pi.SRNeedleType;
            cboSREmployeeIncidentStatus.SelectedValue = pi.SREmployeeIncidentStatus;
            cboSREmployeeInjuryCategory.SelectedValue = pi.SREmployeeInjuryCategory;
            txtLossTime.Value = Convert.ToDouble(pi.LossTime);
            txtInjuredLocation.Text = pi.InjuredLocation;
            txtPlaceOfTreatment.Text = pi.PlaceOfTreatment;

            txtUnsafeCondition.Text = pi.UnsafeCondition;
            txtUnsafeAct.Text = pi.UnsafeAct;
            txtPersonalIndirectCause.Text = pi.PersonalIndirectCause;
            txtWorkingIndirectCause.Text = pi.WorkingIndirectCause;
            cboSREmployeeAccidentReportStatus.SelectedValue = pi.SREmployeeAccidentReportStatus;

            txtActionPlan.Text = pi.ActionPlan;
            txtTarget.Text = pi.Target;
            txtAuthority.Text = pi.Authority;

            chkIsApproved.Checked = _type == "" ? (pi.IsApproved ?? false) : (pi.IsVerified ?? false);
            chkIsVoid.Checked = pi.IsVoid ?? false;
        }

        private void HitungUmur(DateTime? DateOfBirth, DateTime? dNow, RadNumericTextBox txtYear, RadNumericTextBox txtMonth, RadNumericTextBox txtDay)
        {
            if (!DateOfBirth.HasValue) return;
            if (!dNow.HasValue) return;
            var y = Helper.GetAgeInYear(DateOfBirth.Value.Date, dNow.Value.Date).ToString();
            var m = Helper.GetAgeInMonth(DateOfBirth.Value.Date, dNow.Value.Date).ToString();
            var d = Helper.GetAgeInDay(DateOfBirth.Value.Date, dNow.Value.Date).ToString();

            txtYear.Value = y.ToDouble();
            txtMonth.Value = m.ToDouble();
            txtDay.Value = d.ToDouble();
        }
        #endregion

        #region Private Method Standard
        private void SetEntityValue(EmployeeAccidentReports entity)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.IncidentDateTime = DateTime.Parse(txtIncidentDate.SelectedDate.Value.ToShortDateString() + " " +
                               txtIncidentTime.SelectedDate.Value.ToShortTimeString());
            entity.ReportingDateTime = DateTime.Parse(txtReportingDate.SelectedDate.Value.ToShortDateString() + " " +
                               txtReportingTime.SelectedDate.Value.ToShortTimeString());
            entity.IncidentLocation = txtIncidentLocation.Text;
            entity.ChronologicalEvents = txtChronology.Text;
            entity.AspectsOfTheCause = txtAspectsOfTheCause.Text;
            entity.PersonID = Convert.ToInt32(cboPersonID.SelectedValue);

            var ewi = new EmployeeWorkingInfo();
            if (ewi.LoadByPrimaryKey(entity.PersonID ?? -1))
                entity.SupervisorID = ewi.SupervisorId;
            else
                entity.SupervisorID = -1;

            if (!string.IsNullOrEmpty(cboPositionID.SelectedValue))
                entity.PositionID = Convert.ToInt32(cboPositionID.SelectedValue);
            else
                entity.PositionID = -1;
            
            entity.AgeInYear = Convert.ToByte(txtAgeInYear.Value);
            entity.AgeInMonth = Convert.ToByte(txtAgeInMonth.Value);
            entity.AgeInDay = Convert.ToByte(txtAgeInDay.Value);
            entity.SREmployeeIncidentStatus = cboSREmployeeIncidentStatus.SelectedValue;
            entity.SREmployeeIncidentType = cboSREmployeeIncidentType.SelectedValue;
            entity.SRNeedleType = cboSRNeedleType.SelectedValue;
            entity.SREmployeeInjuryCategory = cboSREmployeeInjuryCategory.SelectedValue;
            entity.LossTime = Convert.ToByte(txtLossTime.Value);
            entity.InjuredLocation = txtInjuredLocation.Text;
            entity.PlaceOfTreatment = txtPlaceOfTreatment.Text;
            entity.UnsafeCondition = txtUnsafeCondition.Text;
            entity.UnsafeAct = txtUnsafeAct.Text;
            entity.PersonalIndirectCause = txtPersonalIndirectCause.Text;
            entity.WorkingIndirectCause = txtWorkingIndirectCause.Text;
            entity.SREmployeeAccidentReportStatus = cboSREmployeeAccidentReportStatus.SelectedValue;
            entity.ActionPlan = txtActionPlan.Text;
            entity.Target = txtTarget.Text;
            entity.Authority = txtAuthority.Text;

            //Last Update Status
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

        private void SaveEntity(EmployeeAccidentReports entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new EmployeeAccidentReportsQuery();
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
            var entity = new EmployeeAccidentReports();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Override Method & Function

        private void PopulateNewNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New)
                return;

            _autoNumber = Helper.GetNewAutoNumber(txtIncidentDate.SelectedDate.Value, AppEnum.AutoNumber.EmployeeIncidentNo);
            txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
        }

        #endregion

        #region ComboBox
        protected void PersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );
            query.Where
                (
                    query.Or
                        (
                            query.EmployeeNumber.Like(searchText),
                            query.EmployeeName.Like(searchText)
                        )
                );

            cboPersonID.DataSource = query.LoadDataTable();
            cboPersonID.DataBind();
        }

        protected void PersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboPositionID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new PositionQuery();
            query.Where(
                query.PositionName.Like(searchText));

            query.Select(query.PositionID, query.PositionCode, query.PositionName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboPositionID.DataSource = dtb;
            cboPositionID.DataBind();
        }

        protected void cboPositionID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionCode"].ToString() + " " + ((DataRowView)e.Item.DataItem)["PositionName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionID"].ToString();
        }
        #endregion

        #region Selected Changed
        protected void cboPersonID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
                return;

            var emp = new VwEmployeeTable();
            emp.Query.Where(emp.Query.PersonID == e.Value.ToInt());
            emp.Query.Load();

            txtEmployeeNumber.Text = emp.EmployeeNumber;
            txtDateOfBirth.SelectedDate = emp.BirthDate;
            HitungUmur(emp.BirthDate, txtIncidentDate.SelectedDate, txtAgeInYear, txtAgeInMonth, txtAgeInDay);
            txtSex.Text = emp.SRGenderType == "F" ? "Female" : "Male";

            var posq = new PositionQuery();
            posq.Where(posq.PositionID == emp.PositionID);
            DataTable posdt = posq.LoadDataTable();
            cboPositionID.DataSource = posdt;
            cboPositionID.DataBind();
            cboPositionID.SelectedValue = emp.PositionID.ToString();
        }

        protected void txtIncidentDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            PopulateNewNo();
            HitungUmur(txtDateOfBirth.SelectedDate, txtIncidentDate.SelectedDate, txtAgeInYear, txtAgeInMonth, txtAgeInDay);
        }
        #endregion
    }
}