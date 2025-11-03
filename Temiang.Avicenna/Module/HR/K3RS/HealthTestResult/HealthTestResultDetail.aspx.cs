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
    public partial class HealthTestResultDetail : BasePageDetail
    {
        private string FormType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? "" : Request.QueryString["type"];
            }
        }

        #region Page Event & Initialize
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "##";
            UrlPageList = "HealthTestResultList.aspx";
            ProgramID = AppConstant.Program.K3RS_EmployeeHealthTestResult;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRHealthDegreeConclusion, AppEnum.StandardReference.HealthDegreeConclusion);
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new EmployeeHealthTestResult());

            txtResultDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtRegistrationNo.Text = Request.QueryString["rno"];
            GetRegistrationInfo(txtRegistrationNo.Text);
            cboSRHealthDegreeConclusion.SelectedValue = string.Empty;
            cboSRHealthDegreeConclusion.Text = string.Empty;
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new EmployeeHealthTestResult();
            if (entity.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
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
            var entity = new EmployeeHealthTestResult();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new EmployeeHealthTestResult();
            if (entity.LoadByPrimaryKey(txtRegistrationNo.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("RegistrationNo='{0}'", txtRegistrationNo.Text);
            auditLogFilter.TableName = "EmployeeHealthTestResult";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_RegistrationNo", txtRegistrationNo.Text);
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new EmployeeHealthTestResult();
            if (!entity.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new EmployeeHealthTestResult();
            if (parameters.Length > 0)
            {
                string regNo = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(regNo);

                txtRegistrationNo.Text = entity.RegistrationNo;
            }
            else
            {
                entity.LoadByPrimaryKey(txtRegistrationNo.Text);
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var test = (EmployeeHealthTestResult)entity;

            txtRegistrationNo.Text = test.RegistrationNo;
            GetRegistrationInfo(txtRegistrationNo.Text);

            txtResultDate.SelectedDate = test.ResultDate;
            lblHealtTest.Text = FormType == "mcu" ? "MEDICAL CHECK UP" : "RECTAL SWAB";
            if (!string.IsNullOrEmpty(test.RegistrationNo))
            {
                txtEmployeeServiceUnitID.Text = test.EmployeeServiceUnitID;
                var ou = new OrganizationUnit();
                if (ou.LoadByPrimaryKey(txtEmployeeServiceUnitID.Text.ToInt()))
                    txtEmployeeServiceUnitName.Text = ou.OrganizationUnitName;
            }
            txtExamination.Text = test.Examination;
            txtResult.Text = test.Result;
            cboSRHealthDegreeConclusion.SelectedValue = test.SRHealthDegreeConclusion;
            txtK3rsRecomendation.Text = test.K3rsRecomendation;
            txtPhysicianRecomendation.Text = test.PhysicianRecomendation;
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(EmployeeHealthTestResult entity)
        {
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.ResultDate = txtResultDate.SelectedDate;
            entity.PersonID = Convert.ToInt32(txtPersonID.Value);
            entity.EmployeeServiceUnitID = txtEmployeeServiceUnitID.Text;
            entity.Examination = txtExamination.Text;
            entity.Result = txtResult.Text;
            entity.SRHealthDegreeConclusion = cboSRHealthDegreeConclusion.SelectedValue;
            entity.K3rsRecomendation = txtK3rsRecomendation.Text;
            entity.PhysicianRecomendation = txtPhysicianRecomendation.Text;
            entity.IsMcu = FormType == "mcu";
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        }

        private void SaveEntity(EmployeeHealthTestResult entity)
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
            var que = new EmployeeHealthTestResultQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.RegistrationNo > txtRegistrationNo.Text);
                que.OrderBy(que.RegistrationNo.Ascending);
            }
            else
            {
                que.Where(que.RegistrationNo < txtRegistrationNo.Text);
                que.OrderBy(que.RegistrationNo.Descending);
            }
            if (FormType == "mcu")
                que.Where(que.IsMcu == true);
            else
                que.Where(que.IsMcu == false);

            var entity = new EmployeeHealthTestResult();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }
        #endregion

        #region Override Method & Function
        #endregion

        #region Combobox
        #endregion

        private void GetRegistrationInfo(string registrationNo)
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(registrationNo))
            {
                txtRegistrationDate.SelectedDate = reg.RegistrationDate;
                var par = new Paramedic();
                if (par.LoadByPrimaryKey(reg.ParamedicID))
                    txtParamedicName.Text = par.ParamedicName;
                else
                    txtParamedicName.Text = string.Empty;

                var guar = new Guarantor();
                if (guar.LoadByPrimaryKey(reg.GuarantorID))
                    txtGuarantorName.Text = guar.GuarantorName;
                else
                    txtGuarantorName.Text = string.Empty;

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;

                var emp = new VwEmployeeTable();
                emp.Query.Where(emp.Query.PersonID == pat.PersonID);
                emp.Query.Load();

                txtEmployeeName.Text = emp.EmployeeName;
                txtEmployeeNumber.Text = emp.EmployeeNumber;
                txtPersonID.Value = Convert.ToDouble(emp.PersonID);
                txtDateOfBirth.SelectedDate = emp.BirthDate;
                txtSex.Text = emp.SRGenderType;

                txtAgeInYear.Value = Convert.ToDouble(reg.AgeInYear);
                txtAgeInMonth.Value = Convert.ToDouble(reg.AgeInMonth);
                txtAgeInDay.Value = Convert.ToDouble(reg.AgeInDay);

                var etype = new AppStandardReferenceItem();
                if (etype.LoadByPrimaryKey(AppEnum.StandardReference.EmployeeType.ToString(), emp.SREmployeeType))
                    txtEmployeeType.Text = etype.ItemName;
                else 
                    txtEmployeeType.Text = string.Empty;

                var pos = new Position();
                if (pos.LoadByPrimaryKey(Convert.ToInt32(emp.PositionID)))
                    txtPositionName.Text = pos.PositionName;
                else
                    txtPositionName.Text = string.Empty;

                txtEmployeeServiceUnitID.Text = emp.ServiceUnitID;
                var ou = new OrganizationUnit();
                if (ou.LoadByPrimaryKey(txtEmployeeServiceUnitID.Text.ToInt()))
                    txtEmployeeServiceUnitName.Text = ou.OrganizationUnitName;
                else
                    txtEmployeeServiceUnitName.Text = string.Empty;
                txtEmployeeServiceYear.Value = Convert.ToDouble(emp.ServiceYear);
                txtServiceYearText.Text = emp.ServiceYearText;
            }
            else
            {
                txtRegistrationDate.SelectedDate = null;
                txtParamedicName.Text = string.Empty;
                txtGuarantorName.Text = string.Empty;
                txtMedicalNo.Text = string.Empty;
                txtEmployeeName.Text = string.Empty;
                txtEmployeeNumber.Text = string.Empty;
                txtPersonID.Value = -1;
                txtDateOfBirth.SelectedDate = null;
                txtSex.Text = string.Empty;

                txtAgeInYear.Value = 0;
                txtAgeInMonth.Value = 0;
                txtAgeInDay.Value = 0;

                txtEmployeeType.Text = string.Empty;
                txtPositionName.Text = string.Empty;
                txtEmployeeServiceUnitID.Text = string.Empty;
                txtEmployeeServiceUnitName.Text = string.Empty;
                txtEmployeeServiceYear.Value = 0;
                txtServiceYearText.Text = string.Empty;
            }
        }
    }
}