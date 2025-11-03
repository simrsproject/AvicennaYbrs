using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Employee
{
    public partial class EmployeeDisciplinaryDetail : BasePageDetail
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "EmployeeDisciplinarySearch.aspx?type=" + FormType;
            UrlPageList = "EmployeeDisciplinaryList.aspx?type=" + FormType;

            ProgramID = FormType == string.Empty ? AppConstant.Program.EmployeeDisciplinaryHistory : AppConstant.Program.EmployeeDisciplinary;
            txtEmployeeDisciplinaryID.Text = "1";
			//StandardReference Initialize
			if (!IsPostBack)
            {
				StandardReference.InitializeIncludeSpace(cboSRWarningLevel, AppEnum.StandardReference.WarningLevel);	
				StandardReference.InitializeIncludeSpace(cboSRViolationDegree, AppEnum.StandardReference.ViolationDegree);	
				StandardReference.InitializeIncludeSpace(cboSRViolationType, AppEnum.StandardReference.ViolationType);	
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
            ajax.AddAjaxSetting(cboSRWarningLevel, cboSRWarningLevel);
            ajax.AddAjaxSetting(cboSRWarningLevel, txtValidUntil);
            ajax.AddAjaxSetting(txtEffectiveDate, txtEffectiveDate);
            ajax.AddAjaxSetting(txtEffectiveDate, txtValidUntil);
        }
        #endregion

        #region Toolbar Menu Event

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            if (FormType == "sv")
            {
                var entity = new EmployeeDisciplinary();
                if (entity.LoadByPrimaryKey(Convert.ToInt32(txtEmployeeDisciplinaryID.Text)))
                {
                    if ((!string.IsNullOrEmpty(entity.CreatedByUserID) && entity.CreatedByUserID != AppSession.UserLogin.UserID) || entity.SRWarningLevel != "00")
                    {
                        args.MessageText = "You don't have authorization to edit this data.";
                        args.IsCancel = true;
                        return;
                    }
                }
                else
                {
                    args.MessageText = AppConstant.Message.RecordNotExist;
                    args.IsCancel = true;
                }
            }
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new EmployeeDisciplinary());
            cboPersonalID.SelectedValue = string.Empty;
            cboPersonalID.Text = string.Empty;
            if (FormType == "sv")
                cboSRWarningLevel.SelectedValue = "00";
            else 
                cboSRWarningLevel.Text = string.Empty;
            cboSRViolationDegree.Text = string.Empty;
            cboSRViolationType.Text = string.Empty;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            EmployeeDisciplinary entity = new EmployeeDisciplinary();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtEmployeeDisciplinaryID.Text)))
            {
                if (FormType == "sv")
                {
                    if ((!string.IsNullOrEmpty(entity.CreatedByUserID) && entity.CreatedByUserID != AppSession.UserLogin.UserID) || entity.SRWarningLevel != "00")
                    {
                        args.MessageText = "You don't have authorization to delete this data.";
                        args.IsCancel = true;
                        return;
                    }
                }

                entity.MarkAsDeleted();
                entity.Save();
                //SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            EmployeeDisciplinary entity = new EmployeeDisciplinary();
            entity = new EmployeeDisciplinary();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            EmployeeDisciplinary entity = new EmployeeDisciplinary();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtEmployeeDisciplinaryID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("EmployeeDisciplinaryID='{0}'", txtEmployeeDisciplinaryID.Text.Trim());
            auditLogFilter.TableName = "EmployeeDisciplinary";
        }
        #endregion

        #region ToolBar Menu Support

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (FormType == "sv")
            {
                cboSRWarningLevel.Enabled = false;
            }
        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtEmployeeDisciplinaryID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            EmployeeDisciplinary entity = new EmployeeDisciplinary();
            if (parameters.Length > 0)
            {
                string employeeDisciplinaryID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(employeeDisciplinaryID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtEmployeeDisciplinaryID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            EmployeeDisciplinary employeeDisciplinary = (EmployeeDisciplinary)entity;
            txtEmployeeDisciplinaryID.Value = Convert.ToDouble(employeeDisciplinary.EmployeeDisciplinaryID);

            VwEmployeeTableQuery plQuery = new VwEmployeeTableQuery();
            plQuery.Where(plQuery.PersonID == Convert.ToInt32(employeeDisciplinary.PersonID));
            cboPersonalID.DataSource = plQuery.LoadDataTable();
            cboPersonalID.DataBind();
            cboPersonalID.SelectedValue = employeeDisciplinary.PersonID.ToString();

            cboSRWarningLevel.SelectedValue = employeeDisciplinary.SRWarningLevel;
            txtIncidentDate.SelectedDate = employeeDisciplinary.IncidentDate;
            txtDateIssue.SelectedDate = employeeDisciplinary.DateIssue;
            txtViolation.Text = employeeDisciplinary.Violation;
            txtEffectViolation.Text = employeeDisciplinary.EffectViolation;
            txtAdviceGiven.Text = employeeDisciplinary.AdviceGiven;
            txtSanctionGiven.Text = employeeDisciplinary.SanctionGiven;
            cboSRViolationDegree.SelectedValue = employeeDisciplinary.SRViolationDegree;
            cboSRViolationType.SelectedValue = employeeDisciplinary.SRViolationType;
            txtNote.Text = employeeDisciplinary.Note;
            txtEffectiveDate.SelectedDate = employeeDisciplinary.EffectiveDate;
            txtValidUntil.SelectedDate = employeeDisciplinary.ValidUntil;

            //Display Data Detail
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(EmployeeDisciplinary entity)
        {
            entity.EmployeeDisciplinaryID = Convert.ToInt32(txtEmployeeDisciplinaryID.Value);
            entity.PersonID = Convert.ToInt32(cboPersonalID.SelectedValue);
            entity.SRWarningLevel = cboSRWarningLevel.SelectedValue;
            entity.IncidentDate = txtIncidentDate.SelectedDate;
            entity.DateIssue = txtDateIssue.SelectedDate;
            entity.Violation = txtViolation.Text;
            entity.EffectViolation = txtEffectViolation.Text;
            entity.AdviceGiven = txtAdviceGiven.Text;
            entity.SanctionGiven = txtSanctionGiven.Text;
            entity.SRViolationDegree = cboSRViolationDegree.SelectedValue;
            entity.SRViolationType = cboSRViolationType.SelectedValue;
            entity.Note = txtNote.Text;
            entity.EffectiveDate = txtEffectiveDate.SelectedDate;
            entity.ValidUntil = txtValidUntil.SelectedDate;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                if (entity.es.IsAdded)
                {
                    entity.CreatedDateTime = DateTime.Now;
                    entity.CreatedByUserID = AppSession.UserLogin.UserID;
                }

                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(EmployeeDisciplinary entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                //Commit if success, Rollback if failed
                trans.Complete();

                txtEmployeeDisciplinaryID.Value = entity.EmployeeDisciplinaryID;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            EmployeeDisciplinaryQuery que = new EmployeeDisciplinaryQuery("a");
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.EmployeeDisciplinaryID > txtEmployeeDisciplinaryID.Text);
                que.OrderBy(que.EmployeeDisciplinaryID.Ascending);
            }
            else
            {
                que.Where(que.EmployeeDisciplinaryID < txtEmployeeDisciplinaryID.Text);
                que.OrderBy(que.EmployeeDisciplinaryID.Descending);
            }
            if (FormType == "sv")
            {
                var emps = new EmployeeWorkingInfoQuery("emps");
                que.InnerJoin(emps).On(emps.PersonID == que.PersonID);
                que.Where(emps.SupervisorId == AppSession.UserLogin.PersonID);
            }

            EmployeeDisciplinary entity = new EmployeeDisciplinary();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged
        protected void cboSRWarningLevel_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            GetValidUntilDate();
        }
        protected void txtEffectiveDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            GetValidUntilDate();
        }
        private void GetValidUntilDate()
        {
            var std = new AppStandardReferenceItem();
            if (std.LoadByPrimaryKey(AppEnum.StandardReference.WarningLevel.ToString(), cboSRWarningLevel.SelectedValue)) 
            {
                if (std.NumericValue.HasValue && std.NumericValue > 0 && !txtEffectiveDate.IsEmpty)
                    txtValidUntil.SelectedDate = txtEffectiveDate.SelectedDate.Value.AddMonths(std.NumericValue.ToInt()); // --> .AddDays(-1) disini, tambahin adddays itu kalo mau dikurangin 1 hari
                else
                    txtValidUntil.Clear();
            }
            else
                txtValidUntil.Clear();
        }
        #endregion

        #region ComboBox Function

        protected void cboPersonalID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery("a");
            query.es.Top = 20;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );
            query.Where
                (
                    query.SREmployeeStatus == "1",
                    query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );
            if (FormType == "sv")
            {
                query.Where(query.SupervisorId == AppSession.UserLogin.PersonID);
            }

            cboPersonalID.DataSource = query.LoadDataTable();
            cboPersonalID.DataBind();
        }

        protected void cboPersonalID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        #endregion ComboBox Function
    }
}
