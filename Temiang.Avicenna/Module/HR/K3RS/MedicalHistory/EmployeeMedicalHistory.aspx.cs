using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Configuration;
using Temiang.Avicenna.Module.RADT;
using DevExpress.XtraRichEdit.Layout.Export;

namespace Temiang.Avicenna.Module.HR.K3RS
{
    public partial class EmployeeMedicalHistory : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            ProgramID = AppConstant.Program.K3RS_EmployeeMedicalHistory;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRRegistrationType, AppEnum.StandardReference.RegistrationType);
                PopulateEntryControl();
            }
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
        }

        private void PopulateEntryControl()
        {
            var personId = Page.Request.QueryString["id"];
            if (string.IsNullOrEmpty(personId))
                return;

            var pi = new PersonalInfo();
            pi.LoadByPrimaryKey(Convert.ToInt32(personId));

            var emp = new VwEmployeeTable();
            emp.Query.Where(emp.Query.PersonID == Convert.ToInt32(personId));
            emp.Query.Load();

            PopulateEmployeeImage(Convert.ToInt32(personId), pi.SRGenderType);

            txtEmployeeNumber.Text = pi.EmployeeNumber;
            txtEmployeeName.Text = emp.EmployeeName;
            txtPlaceBirth.Text = pi.PlaceBirth;
            txtBirthDate.SelectedDate = pi.BirthDate;
            rbtSex.SelectedValue = pi.SRGenderType;

            txtPatientID.Text = pi.PatientID;
            var patient = new Patient();
            if (patient.LoadByPrimaryKey(pi.PatientID)) 
                txtMedicalNo.Text = patient.MedicalNo;

            var asri = new AppStandardReferenceItem();
            if (asri.LoadByPrimaryKey(AppEnum.StandardReference.EmployeeStatus.ToString(), emp.SREmployeeStatus))
                txtSREmployeeStatus.Text = asri.ItemName;
            else txtSREmployeeStatus.Text = string.Empty;
            
            txtJoinDate.SelectedDate = emp.JoinDate;
            string[] empStatusResign = AppSession.Parameter.EmployeeStatueResignReference.Split(',');

            if (empStatusResign.Any(e => e.Contains(emp.SREmployeeStatus)))
            {
                var employeeWorkingInfo = new EmployeeWorkingInfo();
                if (employeeWorkingInfo.LoadByPrimaryKey(Convert.ToInt32(personId)))
                    txtResignDate.SelectedDate = employeeWorkingInfo.ResignDate;
                else txtResignDate.SelectedDate = null;
            }
            else
                txtResignDate.SelectedDate = emp.ResignDate;

            DataTable Employee = (new EmployeeWorkingInfoCollection()).EmployeeWorkingInfoView(Convert.ToInt32(personId));
            string organizationID = "-1";
            try { organizationID = Employee.Rows[0]["OrganizationUnitID"].ToString(); }
            catch { }
            string serviceUnitId = "-1";
            try { serviceUnitId = Employee.Rows[0]["ServiceUnitID"].ToString(); }
            catch { }

            var organizationUnit = new OrganizationUnit();
            if (organizationUnit.LoadByPrimaryKey(Convert.ToInt32(organizationID)))
            {
                var ouname = organizationUnit.OrganizationUnitName;
                organizationUnit = new OrganizationUnit();
                if (!string.IsNullOrEmpty(serviceUnitId) && organizationUnit.LoadByPrimaryKey(Convert.ToInt32(serviceUnitId)))
                {
                    txtOrganizationName.Text = ouname + " - " + organizationUnit.OrganizationUnitName;
                }
            }

            txtServiceYear.Value = Convert.ToDouble(emp.ServiceYear);
            txtServiceYearText.Text = emp.ServiceYearText;
            asri = new AppStandardReferenceItem();
            if (asri.LoadByPrimaryKey(AppEnum.StandardReference.EmploymentType.ToString(), emp.SREmploymentType))
                txtSREmploymentType.Text = asri.ItemName;
            else txtSREmploymentType.Text = string.Empty;
        }

        private void PopulateEmployeeImage(int personId, string gender)
        {
            // Load from database
            var personalImg = new PersonalImage();
            if (personalImg.LoadByPrimaryKey(personId))
            {
                // Show Image
                if (personalImg.Photo != null)
                {
                    imgPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(personalImg.Photo));
                }
                else
                {
                    imgPhoto.ImageUrl = gender == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                }
            }
            else
                imgPhoto.ImageUrl = gender == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var reg = new RegistrationQuery("a");
            var unit = new ServiceUnitQuery("c");
            
            reg.Select(
                reg.RegistrationNo,
                reg.RegistrationDateTime,
                unit.ServiceUnitName
            );

            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            reg.Where(
                reg.PatientID == txtPatientID.Text, 
                reg.PatientID != string.Empty,
                reg.IsFromDispensary == false,
                reg.IsVoid == false,
                reg.IsDirectPrescriptionReturn == false,
                reg.IsNonPatient == false
            );

            if (!txtRegistrationDateFrom.IsEmpty && !txtRegistrationDateTo.IsEmpty)
                reg.Where(reg.RegistrationDate >= txtRegistrationDateFrom.SelectedDate, reg.RegistrationDate < txtRegistrationDateTo.SelectedDate.Value.AddDays(1));
            if (!string.IsNullOrEmpty(cboSRRegistrationType.SelectedValue))
                reg.Where(reg.SRRegistrationType == cboSRRegistrationType.SelectedValue);

            reg.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationTime.Descending);

            DataTable dtb = reg.LoadDataTable();

            grdList.DataSource = dtb;

            if (dtb.Rows.Count > 0)
                txtRegistrationNo.Text = dtb.Rows[0]["RegistrationNo"].ToString();
            else
                txtRegistrationNo.Text = string.Empty;

            InitializeDataDetail(txtRegistrationNo.Text);
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if (!(source is RadGrid)) return;
            RadGrid grd = (RadGrid)source;
            switch (grd.ID.ToLower())
            {
                case "grddiagnose": // Populate Detail
                    string[] pars = eventArgument.Split('|');
                    string regNo = pars[0].Split(':')[1];
                    InitializeDataDetail(regNo);
                    break;
            }
        }

        private void InitializeDataDetail(string registrationNo)
        {
            PopulateRegistrationInfo(registrationNo);
            PopulateGrid(registrationNo);

            btnResultLab.Visible = rblTestResult.SelectedIndex == 0;
            btnResultRad.Visible = rblTestResult.SelectedIndex == 1;
        }

        private void PopulateRegistrationInfo(string registrationNo)
        {
            var registration = new Registration();
            if (registration.LoadByPrimaryKey(registrationNo))
            {
                txtRegistrationNo.Text = registrationNo;
                DateTime regDateTime = registration.RegistrationDate ?? DateTime.Now;
                txtRegistrationDateTime.SelectedDate = DateTime.Parse(regDateTime.ToShortDateString() + " " + registration.RegistrationTime);
                if (registration.DischargeDate != null)
                    txtDischargeDateTimeInfo.SelectedDate = registration.DischargeDate;
                else txtDischargeDateTimeInfo.Clear();

                var par = new Paramedic();
                par.LoadByPrimaryKey(registration.ParamedicID);
                txtParamedicName.Text = par.ParamedicName;

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(registration.ServiceUnitID);
                txtServiceUnitName.Text = unit.ServiceUnitName;
                
                var room = new ServiceRoom();
                room.LoadByPrimaryKey(registration.RoomID);
                txtRoomName.Text = room.RoomName;

                txtBedID.Text = registration.BedID;

                var guar = new Guarantor();
                guar.LoadByPrimaryKey(registration.GuarantorID);
                txtGuarantorName.Text = guar.GuarantorName;

                if (registration.DischargeDate != null)
                    txtDischargeDateTimeInfo.SelectedDate = registration.DischargeDate;
                else txtDischargeDateTimeInfo.Clear();
            }
            else
            {
                txtRegistrationNo.Text = string.Empty;
                txtRegistrationDateTime.Clear();
                txtDischargeDateTimeInfo.Clear();
                txtParamedicName.Text = string.Empty;
                txtServiceUnitName.Text = string.Empty;
                txtRoomName.Text = string.Empty;
                txtBedID.Text = registration.BedID;
                txtGuarantorName.Text = string.Empty;
            }
        }

        private void PopulateGrid(string registrationNo)
        {
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
                    query.RegistrationNo,
                    query.SequenceNo,
                    query.DiagnoseID,
                    diag.DiagnoseName,
                    item.ItemName.As("DiagnoseType"),
                    morph.MorphologyName,
                    param.ParamedicName.As("ParamedicName"),
                    query.Notes,
                    query.IsAcuteDisease,
                    query.IsChronicDisease,
                    query.IsOldCase,
                    query.IsConfirmed
                );

            query.Where(query.RegistrationNo == registrationNo, query.IsVoid == false);
            query.OrderBy(query.SRDiagnoseType.Ascending, query.DiagnoseID.Ascending);
            DataTable dtb = query.LoadDataTable();

            grdDiagnose.DataSource = dtb;
            grdDiagnose.DataBind();
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void rblTestResult_OnTextChanged(object sender, EventArgs e)
        {
            btnResultLab.Visible = rblTestResult.SelectedIndex == 0;
            btnResultRad.Visible = rblTestResult.SelectedIndex == 1;
        }
    }
}