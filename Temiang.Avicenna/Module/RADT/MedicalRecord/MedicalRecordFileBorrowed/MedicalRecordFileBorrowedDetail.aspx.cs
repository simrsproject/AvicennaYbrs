using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class MedicalRecordFileBorrowedDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New)
                return;

            _autoNumber = Helper.GetNewAutoNumber(txtDateOfBorrowing.SelectedDate.Value.Date, AppEnum.AutoNumber.MrFileBorrowed);
            txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "MedicalRecordFileBorrowedSearch.aspx";
            UrlPageList = "MedicalRecordFileBorrowedList.aspx";
            ProgramID = AppConstant.Program.MedicalRecordFileBorrowed;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnit(cboServiceUnitID, false);
                StandardReference.InitializeIncludeSpace(cboSRForThePurposesOf, AppEnum.StandardReference.MrbForThePurposesOf);

                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH" || AppSession.Parameter.HealthcareInitialAppsVersion == "RSSA")
                {
                    txtMedicalNo.Visible = true;
                    cboPatientID.Visible = false;
                    rfvMedicalNo2.Visible = true;
                    rfvMedicalNo.Visible = false;
                    txtRegistrationNo.Visible = true;
                    cboRegistrationNo.Visible = false;
                }

                txtPrevDuration.Visible = false;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
            {
                ajax.AddAjaxSetting(txtMedicalNo, txtMedicalNo);
                ajax.AddAjaxSetting(txtMedicalNo, txtPatientName);
                ajax.AddAjaxSetting(txtMedicalNo, txtAddress);
                ajax.AddAjaxSetting(txtMedicalNo, txtAge);
                ajax.AddAjaxSetting(txtMedicalNo, txtGender);

                ajax.AddAjaxSetting(txtRegistrationNo, txtRegistrationNo);
                ajax.AddAjaxSetting(txtRegistrationNo, txtPhysicianName);
            }
            else
            {
                ajax.AddAjaxSetting(cboPatientID, cboPatientID);
                ajax.AddAjaxSetting(cboPatientID, txtPatientName);
                ajax.AddAjaxSetting(cboPatientID, txtAddress);
                ajax.AddAjaxSetting(cboPatientID, txtAge);
                ajax.AddAjaxSetting(cboPatientID, txtGender);
                ajax.AddAjaxSetting(cboPatientID, txtPatientID);

                ajax.AddAjaxSetting(cboRegistrationNo, cboRegistrationNo);
                ajax.AddAjaxSetting(cboRegistrationNo, txtPhysicianName);
                ajax.AddAjaxSetting(cboRegistrationNo, txtRegistrationNo);
            }
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
            cboPatientID.Enabled = false;
            cboRegistrationNo.Enabled = false;
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new MedicalRecordFileBorrowed();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.DateOfReturn != null)
            {
                args.MessageText = "Medical record file has been returned.";
                args.IsCancel = true;
            }
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {

        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {

        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {

        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new MedicalRecordFileBorrowed());
            txtDateOfBorrowing.SelectedDate = DateTime.Now;

            cboRegistrationNo.Items.Clear();
            cboRegistrationNo.Text = string.Empty;

            cboServiceUnitID.SelectedValue = string.Empty;
            cboServiceUnitID.Text = string.Empty;
            cboSRForThePurposesOf.SelectedValue = string.Empty;
            cboSRForThePurposesOf.Text = string.Empty;
            txtDuration.Value = 3;
            
            PopulatePatientInformation(string.Empty, false);
            PopulateRegistrationInformation(string.Empty);

            PopulateNewTransactionNo();
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new MedicalRecordFileBorrowed();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            PopulateNewTransactionNo();
            var entity = new MedicalRecordFileBorrowed();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new MedicalRecordFileBorrowed();
            entity.AddNew();
            SetEntityValue(entity);

            if (string.IsNullOrEmpty(entity.ServiceUnitID))
            {
                args.MessageText = "Service Unit required.";
                args.IsCancel = true;
                return;
            }

            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new MedicalRecordFileBorrowed();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                if (string.IsNullOrEmpty(entity.ServiceUnitID))
                {
                    args.MessageText = "Service Unit required.";
                    args.IsCancel = true;
                    return;
                }
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text);
            auditLogFilter.TableName = "MedicalRecordFileBorrowed";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtTransactionNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return true;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new MedicalRecordFileBorrowed();
            if (parameters.Length > 0)
            {
                String transNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var fb = (MedicalRecordFileBorrowed)entity;
            txtTransactionNo.Text = fb.TransactionNo;
            
            if (!string.IsNullOrEmpty(fb.PatientID))
            {
                var patq = new PatientQuery("b");

                patq.Select(
                        patq.PatientID,
                        patq.MedicalNo,
                        patq.PatientName
                        );
                patq.Where(patq.PatientID == fb.PatientID);
                cboPatientID.DataSource = patq.LoadDataTable();
                cboPatientID.DataBind();

                cboPatientID.SelectedValue = fb.PatientID;
                txtPatientID.Text = fb.PatientID;

                PopulatePatientInformation(fb.PatientID, false);
            }
            else
            {
                txtPatientID.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtAge.Text = string.Empty;
                txtGender.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(fb.RegistrationNo))
            {
                var regq = new RegistrationQuery("a");
                var patq = new PatientQuery("b");

                regq.Select(
                        regq.RegistrationNo,
                        patq.MedicalNo,
                        patq.PatientName
                        );
                regq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);
                regq.Where(regq.RegistrationNo == fb.RegistrationNo);
                cboRegistrationNo.DataSource = regq.LoadDataTable();
                cboRegistrationNo.DataBind();

                cboRegistrationNo.SelectedValue = fb.RegistrationNo;
                txtRegistrationNo.Text = fb.RegistrationNo;

                PopulateRegistrationInformation(fb.RegistrationNo);
            }
            else
            {
                txtRegistrationNo.Text = string.Empty;
                txtPhysicianName.Text = string.Empty;
            }

            txtDateOfBorrowing.SelectedDate = fb.DateOfBorrowing;
            cboServiceUnitID.SelectedValue = fb.ServiceUnitID;
            txtNameOfTheBorrower.Text = fb.NameOfTheBorrower;
            cboSRForThePurposesOf.SelectedValue = fb.SRForThePurposesOf;
            txtDuration.Value = Convert.ToDouble(fb.Duration);
            txtPrevDuration.Value = Convert.ToDouble(fb.Duration);
            txtNotes.Text = fb.Notes;
            txtNameOfGivenID.Text = string.IsNullOrEmpty(fb.NameOfGivenID) ? AppSession.UserLogin.UserID : fb.NameOfGivenID;

            var usr = new AppUser();
            txtNameOfGiven.Text = usr.LoadByPrimaryKey(txtNameOfGivenID.Text) ? usr.UserName : string.Empty;

            txtDateOfReturn.SelectedDate = fb.DateOfReturn;
            txtReturnByName.Text = fb.ReturnByName;
            txtNameOfRecipientID.Text = fb.NameOfRecipientID;

            if (!string.IsNullOrEmpty(fb.NameOfRecipientID))
            {
                usr = new AppUser();
                txtNameOfRecipient.Text = usr.LoadByPrimaryKey(fb.NameOfRecipientID) ? usr.UserName : string.Empty;
            }

            grdHistory.Rebind();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(MedicalRecordFileBorrowed entity)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.PatientID = txtPatientID.Text;
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.DateOfBorrowing = txtDateOfBorrowing.SelectedDate;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.NameOfTheBorrower = txtNameOfTheBorrower.Text;
            entity.SRForThePurposesOf = cboSRForThePurposesOf.SelectedValue;
            entity.Duration = Convert.ToInt16(txtDuration.Value);
            entity.Notes = txtNotes.Text;
            entity.NameOfGivenID = txtNameOfGivenID.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(MedicalRecordFileBorrowed entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                if (Convert.ToInt16(txtPrevDuration.Value) != entity.Duration)
                {
                    var hist = new MedicalRecordFileBorrowedHistory();
                    hist.AddNew();
                    hist.TransactionNo = entity.TransactionNo;
                    hist.LastUpdateDateTime = DateTime.Now;
                    hist.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    hist.Duration = entity.Duration;
                    hist.Save();
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new MedicalRecordFileBorrowedQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.RegistrationNo > cboRegistrationNo.SelectedValue
                    );
                que.OrderBy(que.RegistrationNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.RegistrationNo < cboRegistrationNo.SelectedValue
                    );
                que.OrderBy(que.RegistrationNo.Descending);
            }

            var entity = new MedicalRecordFileBorrowed();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        protected void cboPatientID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var pat = new PatientQuery("a");

            pat.es.Top = 5;
            pat.Select(
                pat.PatientID,
                pat.MedicalNo,
                pat.PatientName
                );
            pat.Where(pat.MedicalNo.Length() > 0, pat.IsActive == true);

            if (e.Text.Trim().Contains(" "))
            {
                var searchs = e.Text.Trim().Split(' ');
                foreach (var search in searchs)
                {
                    var searchLike = "%" + search + "%";
                    pat.Where(
                        pat.Or(
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
                pat.Where(
                    pat.Or(
                        pat.MedicalNo.Like(searchTextContain),
                        pat.FirstName.Like(searchTextContain),
                        pat.MiddleName.Like(searchTextContain),
                        pat.LastName.Like(searchTextContain)
                        )
                );
            }
            pat.OrderBy(pat.MedicalNo.Ascending);

            cboPatientID.DataSource = pat.LoadDataTable();
            cboPatientID.DataBind();
        }

        protected void cboPatientID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["MedicalNo"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
        }

        protected void cboPatientID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulatePatientInformation(e.Value, false);
        }

        protected void cboRegistrationNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var reg = new RegistrationQuery("a");
            var pat = new PatientQuery("b");

            reg.es.Top = 5;
            reg.Select(
                reg.RegistrationNo,
                pat.MedicalNo,
                pat.PatientName
                );
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.Where(reg.IsVoid == false, reg.IsConsul == false, reg.PatientID == cboPatientID.SelectedValue);

            if (e.Text.Trim().Contains(" "))
            {
                var searchs = e.Text.Trim().Split(' ');
                foreach (var search in searchs)
                {
                    var searchLike = "%" + search + "%";
                    reg.Where(
                        reg.Or(
                            reg.RegistrationNo.Like(searchLike),
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

        protected void cboRegistrationNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateRegistrationInformation(e.Value);
        }

        protected void txtMedicalNo_TextChanged(object sender, EventArgs e)
        {
            PopulatePatientInformation(txtMedicalNo.Text, true);
        }

        protected void txtRegistrationNo_TextChanged(object sender, EventArgs e)
        {
            PopulateRegistrationInformation(txtRegistrationNo.Text);
        }

        private void PopulatePatientInformation(string id, bool isMedrec)
        {
            var pat = new Patient();
            if (isMedrec)
            {
                if (pat.LoadByMedicalNo(id))
                {
                    txtPatientID.Text = pat.PatientID;
                    txtMedicalNo.Text = pat.MedicalNo;
                    txtPatientName.Text = pat.PatientName;
                    txtAddress.Text = pat.StreetName + " " + pat.City.Trim() + " " + pat.County.Trim();

                    string ageYear = Helper.GetAgeInYear(pat.DateOfBirth.Value).ToString();
                    string ageMonth = Helper.GetAgeInMonth(pat.DateOfBirth.Value).ToString();
                    string ageDay = Helper.GetAgeInDay(pat.DateOfBirth.Value).ToString();

                    if (ageYear == "0")
                    {
                        if (ageMonth == "0")
                            txtAge.Text = ageDay + " d";
                        else
                            txtAge.Text = ageMonth + " m";
                    }
                    else
                        txtAge.Text = ageYear + " y";

                    txtGender.Text = pat.Sex == "M" ? "Male" : "Female";
                }
                else
                {
                    txtMedicalNo.Text = string.Empty;
                    txtPatientName.Text = string.Empty;
                    txtAddress.Text = string.Empty;
                    txtAge.Text = string.Empty;
                    txtGender.Text = string.Empty;
                }
            }
            else
            {
                if (pat.LoadByPrimaryKey(id))
                {
                    txtPatientID.Text = pat.PatientID;
                    txtMedicalNo.Text = pat.MedicalNo;
                    txtPatientName.Text = pat.PatientName;
                    txtAddress.Text = pat.StreetName + " " + pat.City.Trim() + " " + pat.County.Trim();

                    string ageYear = Helper.GetAgeInYear(pat.DateOfBirth.Value).ToString();
                    string ageMonth = Helper.GetAgeInMonth(pat.DateOfBirth.Value).ToString();
                    string ageDay = Helper.GetAgeInDay(pat.DateOfBirth.Value).ToString();

                    if (ageYear == "0")
                    {
                        if (ageMonth == "0")
                            txtAge.Text = ageDay + " d";
                        else
                            txtAge.Text = ageMonth + " m";
                    }
                    else
                        txtAge.Text = ageYear + " y";

                    txtGender.Text = pat.Sex == "M" ? "Male" : "Female";
                }
                else
                {
                    txtMedicalNo.Text = string.Empty;
                    txtPatientName.Text = string.Empty;
                    txtAddress.Text = string.Empty;
                    txtAge.Text = string.Empty;
                    txtGender.Text = string.Empty;
                }
            }
        }

        private void PopulateRegistrationInformation(string regNo)
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(regNo))
            {
                if (reg.PatientID == txtPatientID.Text)
                {
                    txtRegistrationNo.Text = reg.RegistrationNo;
                    var par = new Paramedic();
                    par.LoadByPrimaryKey(reg.ParamedicID);

                    txtPhysicianName.Text = par.ParamedicName;
                }
                else
                {
                    txtRegistrationNo.Text = string.Empty;
                    txtPhysicianName.Text = string.Empty;
                }
            }
            else
            {
                txtRegistrationNo.Text = string.Empty;
                txtPhysicianName.Text = string.Empty;
            }
        }

        protected void grdHistory_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdHistory.DataSource = MedicalRecordFileBorrowedHistorys;
        }

        private DataTable MedicalRecordFileBorrowedHistorys
        {
            get
            {
                var query = new MedicalRecordFileBorrowedHistoryQuery("a");
                var usr = new AppUserQuery("b");

                query.InnerJoin(usr).On(query.LastUpdateByUserID == usr.UserID);
                query.Select
                    (
                        query.TransactionNo,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID,
                        usr.UserName.As("UpdateBy"),
                        query.Duration
                    );
                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.LastUpdateDateTime.Ascending);
                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }
    }
}
