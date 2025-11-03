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
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;
using DevExpress.Web;

namespace Temiang.Avicenna.Module.CRM
{
    public partial class MembershipDetail : BasePageDetail
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["t"];
            }
        }

        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "MembershipSearch.aspx?t=" + FormType;
            UrlPageList = "MembershipList.aspx?t=" + FormType;

            this.WindowSearch.Height = 300;

            ProgramID = FormType == "g" ? AppConstant.Program.Membership : AppConstant.Program.MembershipEmployee;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRMembershipType, AppEnum.StandardReference.MembershipType);
                trSRMembershipType.Visible = false;
                StandardReference.InitializeIncludeSpace(cboSRSalutation, AppEnum.StandardReference.Salutation);

                trPatientID.Visible = FormType == "g";
                trMedicalNo.Visible = FormType == "g";
                trPersonID.Visible = FormType == "e";

                if (FormType == "g")
                {
                    tblDateRange.Visible = false;
                    grdMembership.Columns[3].Visible = false;
                    grdMembership.Columns[4].Visible = false;
                    grdMembership.Columns[5].Visible = false;
                    grdMembership.Columns[6].Visible = false;
                    grdMembership.Columns[7].Visible = false;
                    grdMembership.Columns[8].Visible = false;
                    grdMembership.Columns[13].Visible = false;
                }
                else
                {
                    tabStrip.Tabs[0].Visible = false;
                    tabStrip.SelectedIndex = 1;
                    multiPage.SelectedIndex = 1;
                    grdMembership.Columns[1].Visible = false;
                    grdMembership.Columns[2].Visible = false;
                    grdMembership.Columns[grdMembership.Columns.Count - 3].Visible = false;
                    grdMembership.Columns[grdMembership.Columns.Count - 4].Visible = false;
                    
                    grdMembership.Columns[grdMembership.Columns.Count - 7].Visible = false;
                    grdMembership.Columns[grdMembership.Columns.Count - 8].Visible = false;
                    grdMembership.Columns[grdMembership.Columns.Count - 9].Visible = false;
                }
                txtStartDate.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                txtEndDate.SelectedDate= DateTime.Today;
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            //if (IsPostBack) return;
            if (DataModeCurrent != AppEnum.DataMode.Edit) return;

            cboSRMembershipType.Enabled = false;
            txtJoinDate.Enabled = false;
        }

        private void PopulatePatientInformation(string patientId)
        {
            var patient = new Patient();
            if (patient.LoadByPrimaryKey(patientId))
            {
                txtMedicalNo.Text = patient.MedicalNo;
                cboSRSalutation.SelectedValue = patient.SRSalutation;
                txtPatientName.Text = patient.PatientName;
                rbtSex.SelectedValue = patient.Sex;
                txtCityOfBirth.Text = patient.CityOfBirth;
                txtDateOfBirth.SelectedDate = patient.DateOfBirth;
                txtAgeDay.Value = Helper.GetAgeInDay(patient.DateOfBirth.Value);
                txtAgeMonth.Value = Helper.GetAgeInMonth(patient.DateOfBirth.Value);
                txtAgeYear.Value = Helper.GetAgeInYear(patient.DateOfBirth.Value);
                txtAddress.Text = patient.Address;
                txtPhoneNo.Text = patient.PhoneNo;
                txtMobilePhone.Text = patient.MobilePhoneNo;
                txtEmail.Text = patient.Email;
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                cboSRSalutation.SelectedValue = string.Empty;
                txtPatientName.Text = string.Empty;
                txtCityOfBirth.Text = string.Empty;
                txtDateOfBirth.SelectedDate = null;
                txtAgeDay.Value = 0;
                txtAgeMonth.Value = 0;
                txtAgeYear.Value = 0;
                txtAddress.Text = string.Empty;
                txtPhoneNo.Text = string.Empty;
                txtMobilePhone.Text = string.Empty;
                txtEmail.Text = string.Empty;
            }

            PopulatePatientImage(patientId);
        }

        private void PopulateEmployeeInformation(int personId)
        {
            var p = new PersonalInfo();
            if (p.LoadByPrimaryKey(personId))
            {
                txtMedicalNo.Text = string.Empty;
                cboSRSalutation.SelectedValue = p.SRSalutation;
                txtPatientName.Text = p.EmployeeName;
                rbtSex.SelectedValue = p.SRGenderType;
                txtCityOfBirth.Text = p.PlaceBirth;
                txtDateOfBirth.SelectedDate = p.BirthDate;
                txtAgeDay.Value = Helper.GetAgeInDay(p.BirthDate.Value);
                txtAgeMonth.Value = Helper.GetAgeInMonth(p.BirthDate.Value);
                txtAgeYear.Value = Helper.GetAgeInYear(p.BirthDate.Value);

                var pAddr = new PersonalAddressCollection();
                pAddr.Query.Where(pAddr.Query.PersonID == personId, pAddr.Query.SRAddressType == "01");
                if (pAddr.Query.Load())
                {
                    var addr = pAddr.SingleOrDefault();
                    if (addr != null) txtAddress.Text = addr.StateName;
                    else txtAddress.Text = string.Empty;
                }
                else
                {
                    txtAddress.Text = string.Empty;
                }

                var pCont = new PersonalContactCollection();
                pCont.Query.Where(pCont.Query.PersonID == personId, pCont.Query.SRContactType.In("01", "02", "04"));
                if (pCont.Query.Load())
                {
                    var cont = pCont.Where(i => i.SRContactType == "01").SingleOrDefault();
                    if (cont != null) txtPhoneNo.Text = cont.ContactValue;
                    else txtPhoneNo.Text = string.Empty;
                    cont = pCont.Where(i => i.SRContactType == "02").SingleOrDefault();
                    if (cont != null) txtMobilePhone.Text = cont.ContactValue;
                    else txtMobilePhone.Text = string.Empty;
                    cont = pCont.Where(i => i.SRContactType == "04").SingleOrDefault();
                    if (cont != null) txtEmail.Text = cont.ContactValue;
                    else txtEmail.Text = string.Empty;
                }
                else
                {
                    txtPhoneNo.Text = string.Empty;
                    txtMobilePhone.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                }
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                cboSRSalutation.SelectedValue = string.Empty;
                txtPatientName.Text = string.Empty;
                txtCityOfBirth.Text = string.Empty;
                txtDateOfBirth.SelectedDate = null;
                txtAgeDay.Value = 0;
                txtAgeMonth.Value = 0;
                txtAgeYear.Value = 0;
                txtAddress.Text = string.Empty;
                txtPhoneNo.Text = string.Empty;
                txtMobilePhone.Text = string.Empty;
                txtEmail.Text = string.Empty;
            }

            PopulatePersonImage(personId);
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdMembership, grdMembership);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Membership());
            PopulatePatientInformation(string.Empty);
            txtMembershipNo.Text = GetNewTransactionNo();
            txtJoinDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            chkIsActive.Checked = true;
        }

        protected override void OnMenuEditClick()
        {
            if (FormType == "g")
            {
                cboPatientID.Enabled = false;
                cboSRSalutation.Enabled = false;
                txtPatientName.ReadOnly = true;
                rbtSex.Enabled = false;
                txtCityOfBirth.ReadOnly = true;
                txtDateOfBirth.Enabled = false;
                txtAddress.ReadOnly = true;
                txtPhoneNo.ReadOnly = true;
                txtMobilePhone.ReadOnly = true;
                txtEmail.ReadOnly = true;
            }
            else
            {
                cboPersonID.Enabled = false;
                if (!string.IsNullOrEmpty(cboPersonID.SelectedValue) && cboPersonID.SelectedValue != "-1")
                {
                    cboSRSalutation.Enabled = false;
                    txtPatientName.ReadOnly = true;
                    rbtSex.Enabled = false;
                    txtCityOfBirth.ReadOnly = true;
                    txtDateOfBirth.Enabled = false;
                    txtAddress.ReadOnly = true;
                    txtPhoneNo.ReadOnly = true;
                    txtMobilePhone.ReadOnly = true;
                    txtEmail.ReadOnly = true;
                }
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new Membership();
            if (entity.LoadByPrimaryKey(txtMembershipNo.Text))
            {
                var detail = new MembershipDetailCollection();
                detail.Query.Where(detail.Query.MembershipNo == entity.MembershipNo, 
                    detail.Query.Or(
                    detail.Query.TotalAmount != 0, detail.Query.BalanceAmount != 0, detail.Query.RewardPoint != 0, detail.Query.RewardPointRefferal != 0));
                detail.LoadAll();
                if (detail.Count > 0)
                {
                    detail.MarkAllAsDeleted();
                    entity.MarkAsDeleted();

                    detail.Save();
                    entity.Save();
                }
                else
                {
                    args.MessageText = "Membership is already used.";
                }
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new Membership();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new Membership();
            if (entity.LoadByPrimaryKey(txtMembershipNo.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("MembershipNo='{0}'", txtMembershipNo.Text.Trim());
            auditLogFilter.TableName = "Membership";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_MembershipNo", txtMembershipNo.Text);
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);

            txtStartDate.Enabled = true;
            txtEndDate.Enabled = true;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Membership();
            if (parameters.Length > 0)
            {
                var memNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(memNo);
            }
            else
                entity.LoadByPrimaryKey(txtMembershipNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var pm = (Membership)entity;

            txtMembershipNo.Text = pm.MembershipNo;
            cboSRMembershipType.SelectedValue = pm.SRMembershipType;
            txtJoinDate.SelectedDate = pm.JoinDate ?? (new DateTime()).NowAtSqlServer();

            var setFromEntity = false;

            if (FormType == "g")
            {
                if (!string.IsNullOrEmpty(pm.str.PatientID))
                {
                    var dtbPatient = (new PatientCollection()).PatientRegisterAble(pm.PatientID, string.Empty, string.Empty, string.Empty, 10);
                    cboPatientID.DataSource = dtbPatient;
                    cboPatientID.DataBind();
                    cboPatientID.SelectedValue = pm.PatientID;
                }
                else
                {
                    cboPatientID.Items.Clear();
                    cboPatientID.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(cboPatientID.SelectedValue))
                    PopulatePatientInformation(cboPatientID.SelectedValue);
                else
                    setFromEntity = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(pm.str.PersonID))
                {
                    if (pm.str.PersonID != "-1")
                    {
                        var emps = new VwEmployeeTableQuery();
                        emps.Where(emps.PersonID == pm.PersonID);
                        cboPersonID.DataSource = emps.LoadDataTable();
                        cboPersonID.DataBind();
                        cboPersonID.SelectedValue = pm.PersonID.ToString();

                        PopulateEmployeeInformation(cboPersonID.SelectedValue.ToInt());
                    }
                    else
                    {
                        cboPersonID.Items.Clear();
                        cboPersonID.Text = string.Empty;

                        setFromEntity = true;
                    }
                }
                else
                {
                    cboPersonID.Items.Clear();
                    cboPersonID.Text = string.Empty;

                    setFromEntity = true;
                }


            }
            
            if (setFromEntity)
            {
                txtMedicalNo.Text = string.Empty;
                cboSRSalutation.SelectedValue = pm.SRSalutation;
                txtPatientName.Text = pm.MemberName;
                rbtSex.SelectedValue = pm.Sex;
                txtCityOfBirth.Text = pm.CityOfBirth;
                txtDateOfBirth.SelectedDate = pm.DateOfBirth;
                txtAgeYear.Text = Helper.GetAgeInYear(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
                txtAgeMonth.Text = Helper.GetAgeInMonth(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
                txtAgeDay.Text = Helper.GetAgeInDay(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
                txtAddress.Text = pm.Address;
                txtPhoneNo.Text = pm.PhoneNo;
                txtMobilePhone.Text = pm.MobilePhoneNo;
                txtEmail.Text = pm.Email;

                PopulateBlankImage(pm.Sex);
            }

            chkIsActive.Checked = pm.IsActive ?? false;

            PopulateGridDetail();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(Membership entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtMembershipNo.Text = GetNewTransactionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.MembershipNo = txtMembershipNo.Text;
            entity.SRMembershipType = FormType == "g" ? "01" : "02"; //cboSRMembershipType.SelectedValue;
            entity.JoinDate = txtJoinDate.SelectedDate;
            entity.PatientID = cboPatientID.SelectedValue;
            entity.PersonID = FormType == "g" ? -1 : (string.IsNullOrEmpty(cboPersonID.SelectedValue) ? -1 : cboPersonID.SelectedValue.ToInt());
            entity.MemberName = txtPatientName.Text;
            entity.SRSalutation = cboSRSalutation.SelectedValue;
            entity.Sex = rbtSex.SelectedValue;
            entity.CityOfBirth = txtCityOfBirth.Text;
            entity.DateOfBirth = txtDateOfBirth.SelectedDate;
            entity.Address = txtAddress.Text;
            entity.PhoneNo = txtPhoneNo.Text;
            entity.MobilePhoneNo = txtMobilePhone.Text;
            entity.Email = txtEmail.Text;
            entity.IsActive = chkIsActive.Checked;

            if (entity.es.IsAdded)
            {
                entity.CreateByUserID = AppSession.UserLogin.UserID;
                entity.CreateDateTime = DateTime.Now;
            }

            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(Membership entity)
        {
            foreach (BusinessObject.MembershipDetail item in MembershipDetails)
                item.MembershipNo = entity.MembershipNo;

            foreach (MembershipMember item in MembershipMembers)
                item.MembershipNo = entity.MembershipNo;

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                MembershipDetails.Save();
                MembershipMembers.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new MembershipQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.MembershipNo > txtMembershipNo.Text
                    );
                que.OrderBy(que.MembershipNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.MembershipNo < txtMembershipNo.Text
                    );
                que.OrderBy(que.MembershipNo.Descending);
            }
            if (FormType == "g")
                que.Where(que.SRMembershipType == "01");
            else
                que.Where(que.SRMembershipType == "02");

            var entity = new Membership();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdMembership.Columns[0].Visible = isVisible && FormType == "g";
            grdMembership.Columns[grdMembership.Columns.Count - 1].Visible = isVisible && FormType == "g";
            grdMembership.Columns[grdMembership.Columns.Count - 3].Visible = !isVisible && FormType == "g";
            grdMembership.Columns[grdMembership.Columns.Count - 4].Visible = !isVisible && FormType == "g";
            grdMembership.Columns[grdMembership.Columns.Count - 5].Visible = !isVisible;
            grdMembership.Columns[grdMembership.Columns.Count - 6].Visible = !isVisible && FormType == "g";

            grdMembership.MasterTableView.CommandItemDisplay = isVisible && FormType == "g"
                ? GridCommandItemDisplay.Top
                : GridCommandItemDisplay.None;

            grdMembershipMember.Columns[0].Visible = isVisible;
            grdMembershipMember.Columns[grdMembershipMember.Columns.Count - 1].Visible = isVisible;

            grdMembershipMember.MasterTableView.CommandItemDisplay = isVisible
                ? GridCommandItemDisplay.Top
                : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
            {
                MembershipDetails = null;
                MembershipMembers = null;
            }

            //Perbaharui tampilan dan data
            grdMembership.Rebind();
            grdMembershipMember.Rebind();
        }

        private MembershipDetailCollection MembershipDetails
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collMembershipDetail" + Request.UserHostName];
                    if (obj != null)
                        return ((MembershipDetailCollection)(obj));
                }

                var coll = new MembershipDetailCollection();
                var query = new MembershipDetailQuery("a");
                var reg = new RegistrationQuery("b");
                var pat = new PatientQuery("c");
                var su = new ServiceUnitQuery("d");
                var guar = new GuarantorQuery("e");

                query.LeftJoin(reg).On(reg.RegistrationNo == query.RegistrationNo);
                query.LeftJoin(pat).On(pat.PatientID == reg.PatientID);
                query.LeftJoin(su).On(su.ServiceUnitID == reg.ServiceUnitID);
                query.LeftJoin(guar).On(guar.GuarantorID == reg.GuarantorID);

                query.Select(
                    query, 
                    @"<a.RewardPoint - a.ClaimedPoint AS 'refToBalance'>",
                    pat.MedicalNo.As("refToMedicalNo"),
                    pat.PatientName.As("refToPatientName"),
                    su.ServiceUnitName.As("refToServiceUnitName"),
                    guar.GuarantorName.As("refToGuarantorName")
                    );
                query.Where(query.MembershipNo == txtMembershipNo.Text);

                query.OrderBy(query.StartDate.Ascending);
                coll.Load(query);

                Session["collMembershipDetail" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collMembershipDetail" + Request.UserHostName] = value;
            }
        }

        private MembershipMemberCollection MembershipMembers
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collMembershipMember" + Request.UserHostName];
                    if (obj != null)
                        return ((MembershipMemberCollection)(obj));
                }

                var coll = new MembershipMemberCollection();
                var query = new MembershipMemberQuery("a");
                var pat = new PatientQuery("b");
                query.InnerJoin(pat).On(pat.PatientID == query.PatientID);

                query.Select(query,
                    pat.MedicalNo.As("refToPatient_MedicalNo"),
                    pat.PatientName.As("refToPatient_PatientName"),
                    pat.Sex.As("refToPatient_Sex"),
                    pat.CityOfBirth.As("refToPatient_CityOfBirth"),
                    pat.DateOfBirth.As("refToPatient_DateOfBirth"),
                    pat.Address.As("refToPatient_Address"),
                    pat.PhoneNo.As("refToPatient_PhoneNo"),
                    pat.MobilePhoneNo.As("refToPatient_MobilePhoneNo"),
                    pat.Email.As("refToPatient_Email")
                    );

                query.Where(query.MembershipNo == txtMembershipNo.Text);
                query.OrderBy(query.CreateDateTime.Ascending);
                coll.Load(query);

                Session["collMembershipMember" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collMembershipMember" + Request.UserHostName] = value;
            }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            MembershipDetails = null; //Reset Record Detail
            grdMembership.DataSource = MembershipDetails;
            grdMembership.DataBind();

            MembershipMembers = null;
            grdMembershipMember.DataSource = MembershipMembers;
            grdMembershipMember.DataBind();
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdMembership.CurrentPageIndex = 0;
            grdMembership.Rebind();
        }

        protected void grdMembership_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (FormType == "e")
            {
                var ds = from d in MembershipDetails
                         where d.StartDate >= txtStartDate.SelectedDate && d.StartDate <= txtEndDate.SelectedDate
                         select d;
                grdMembership.DataSource = ds;
            }
            else
            {
                grdMembership.DataSource = MembershipDetails;
            }
        }

        protected void grdMembership_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][MembershipDetailMetadata.ColumnNames.MembershipDetailID]);
            BusinessObject.MembershipDetail entity = FindMembershipDetail(id);
            if (entity != null && entity.TotalAmount == 0 && entity.RewardPoint == 0 && entity.RewardPointRefferal == 0)
                SetEntityValue(entity, e);
        }

        protected void grdMembership_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][MembershipDetailMetadata.ColumnNames.MembershipDetailID]);
            BusinessObject.MembershipDetail entity = FindMembershipDetail(id);
            if (entity != null && entity.TotalAmount == 0 && entity.RewardPoint == 0 && entity.RewardPointRefferal == 0)
                entity.MarkAsDeleted();
        }

        protected void grdMembership_InsertCommand(object source, GridCommandEventArgs e)
        {
            BusinessObject.MembershipDetail entity = MembershipDetails.AddNew();
            SetEntityValue(entity, e);
        }

        private BusinessObject.MembershipDetail FindMembershipDetail(String id)
        {
            MembershipDetailCollection coll = MembershipDetails;
            BusinessObject.MembershipDetail retEntity = null;
            foreach (BusinessObject.MembershipDetail rec in coll)
            {
                if (rec.MembershipDetailID.ToString().Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(BusinessObject.MembershipDetail entity, GridCommandEventArgs e)
        {
            var userControl = (MembershipDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.MembershipDetailID = userControl.MembershipDetailID;
                entity.StartDate = userControl.StartDate;
                entity.EndDate = userControl.EndDate;
                entity.RegistrationNo = string.Empty;
                entity.TotalAmount = userControl.TotalAmount;
                entity.ReedeemAmount = userControl.ReedeemAmount;
                entity.BalanceAmount = userControl.BalanceAmount;
                entity.RewardPoint = userControl.RewardPoint;
                entity.RewardPointRefferal = userControl.RewardPointRefferal;
                entity.ClaimedPoint = userControl.ClaimedPoint;
                entity.Balance = (entity.RewardPoint ?? 0) - (entity.ClaimedPoint ?? 0);
                entity.IsClosed = false;

                //Last Update Status
                if (entity.es.IsAdded)
                {
                    entity.CreateByUserID = AppSession.UserLogin.UserID;
                    entity.CreateDateTime = DateTime.Now;
                }

                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        protected void grdMembershipMember_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdMembershipMember.DataSource = MembershipMembers;
        }

        protected void grdMembershipMember_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][MembershipMemberMetadata.ColumnNames.PatientID]);
            MembershipMember entity = FindMembershipMember(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdMembershipMember_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][MembershipMemberMetadata.ColumnNames.PatientID]);
            MembershipMember entity = FindMembershipMember(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdMembershipMember_InsertCommand(object source, GridCommandEventArgs e)
        {
            MembershipMember entity = MembershipMembers.AddNew();
            SetEntityValue(entity, e);
        }

        private MembershipMember FindMembershipMember(String id)
        {
            MembershipMemberCollection coll = MembershipMembers;
            MembershipMember retEntity = null;
            foreach (MembershipMember rec in coll)
            {
                if (rec.PatientID.ToString().Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(MembershipMember entity, GridCommandEventArgs e)
        {
            var userControl = (MembershipMemberItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.PatientID = userControl.PatientID;

                var pat = new Patient();
                if (pat.LoadByPrimaryKey(entity.PatientID))
                {
                    entity.MedicalNo = pat.MedicalNo;
                    entity.PatientName = pat.PatientName;
                    entity.Sex = pat.Sex;
                    entity.CityOfBirth = pat.CityOfBirth;
                    entity.DateOfBirth = pat.DateOfBirth ?? DateTime.Now.Date;
                    entity.Address = pat.Address;
                    entity.PhoneNo = pat.PhoneNo;
                    entity.MobilePhoneNo = pat.MobilePhoneNo;
                    entity.Email = pat.Email;
                }

                entity.IsActive = userControl.IsActive;

                //Last Update Status
                if (entity.es.IsAdded)
                {
                    entity.CreateByUserID = AppSession.UserLogin.UserID;
                    entity.CreateDateTime = DateTime.Now;
                }

                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = DateTime.Now;
                }
            }
        }
        #endregion

        protected void cboPatientID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                PopulatePatientInformation(e.Value);
            }
            else
            {
                PopulatePatientInformation(string.Empty);
            }

            PopulatePatientImage(e.Value);
        }

        protected void cboPatientID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var dtbPatient = (new PatientCollection()).PatientRegisterAble(e.Text, string.Empty, string.Empty, string.Empty, 10);
            cboPatientID.DataSource = dtbPatient;
            cboPatientID.DataBind();
        }

        protected void cboPatientID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
        }

        protected void cboPersonID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                PopulateEmployeeInformation(e.Value.ToInt());
            }
            else
            {
                PopulateEmployeeInformation(-1);
            }

            PopulatePersonImage(e.Value.ToInt());
        }

        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
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
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );

            cboPersonID.DataSource = query.LoadDataTable();
            cboPersonID.DataBind();
        }

        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void txtDateOfBirth_SelectedDateChanged(object sender, EventArgs e)
        {
            txtAgeYear.Text = Helper.GetAgeInYear(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
            txtAgeMonth.Text = Helper.GetAgeInMonth(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
            txtAgeDay.Text = Helper.GetAgeInDay(txtDateOfBirth.SelectedDate ?? (new DateTime()).NowAtSqlServer().Date).ToString();
        }

        protected void cboSRSalutation_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var appStandardReferenceItem = new AppStandardReferenceItem();
            appStandardReferenceItem.LoadByPrimaryKey(AppEnum.StandardReference.Salutation.ToString(), cboSRSalutation.SelectedValue);
            rbtSex.SelectedValue = appStandardReferenceItem.ReferenceID;
        }

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, FormType == "g" ? AppEnum.AutoNumber.MembershipNo : AppEnum.AutoNumber.MembershipEmployeeNo);

            return _autoNumber.LastCompleteNumber;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');

                if (param[0].ToString() == "calculation")
                {
                    var id = Convert.ToInt64(param[1]);

                    var div = FormType == "g" ? AppSession.Parameter.MultipleForRewardPoints : AppSession.Parameter.MultipleForRewardPointsForEmployee;
                    var x = BusinessObject.MembershipDetail.UpdateRewardPointsBalanceAmount(id, div, AppSession.UserLogin.UserID);
                }
                else
                {
                    var id = Convert.ToInt64(param[1]);
                    var md = new BusinessObject.MembershipDetail();
                    if (md.LoadByPrimaryKey(id))
                    {
                        md.IsClosed = param[0].ToString() == "close" ? true : false;
                        md.Save();
                    }
                }
                MembershipDetails = null;
                grdMembership.Rebind();
            }
        }

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
                    imgPatientPhoto.ImageUrl = rbtSex.SelectedValue == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                }
            }
            else
                imgPatientPhoto.ImageUrl = rbtSex.SelectedValue == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";

        }

        private void PopulatePersonImage(int personID)
        {
            // Load from database
            var personImg = new PersonalImage();
            if (personImg.LoadByPrimaryKey(personID))
            {
                // Show Image
                if (personImg.Photo != null)
                {
                    imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(personImg.Photo));
                }
                else
                {
                    imgPatientPhoto.ImageUrl = rbtSex.SelectedValue == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                }
            }
            else
                imgPatientPhoto.ImageUrl = rbtSex.SelectedValue == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";

        }

        private void PopulateBlankImage(string sex)
        {
           imgPatientPhoto.ImageUrl = sex == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
        }
        #endregion
    }
}