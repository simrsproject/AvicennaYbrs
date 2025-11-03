using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeeWorkingInfoSearch : BasePageDialog
    {
        private string getPageID
        {
            get
            {
                return Request.QueryString["status"];
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = getPageID == "recruit" ? AppConstant.Program.ApplicantWorkingInfo :
                (getPageID == "trn" ? AppConstant.Program.EmployeeOrientation : AppConstant.Program.EmployeeWorkingInfo); 
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSREmployeeStatus, AppEnum.StandardReference.EmployeeStatus);
                StandardReference.InitializeIncludeSpace(cboSREmployeeType, AppEnum.StandardReference.EmployeeType);
                StandardReference.InitializeIncludeSpace(cboSRRemunerationType, AppEnum.StandardReference.RemunerationType);

                var unit = new OrganizationUnitCollection();
                unit.Query.Where(unit.Query.IsActive == true);
                if (unit.Query.Load())
                {
                    cboOrganizationUnit.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                    foreach (OrganizationUnit u in unit)
                    {
                        cboOrganizationUnit.Items.Add(new Telerik.Web.UI.RadComboBoxItem(u.OrganizationUnitName, u.OrganizationUnitID.ToString()));
                    }
                }

                if (getPageID == "recruit")
                {
                    lblEmployeeNumber.Text = "Applicant No";
                    lblEmployeeName.Text = "Applicant Name";
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            OnButtonOkClick();

            return true;
        }

        private void OnButtonOkClick()
        {
            var query = new VwEmployeeTableQuery("a");
            var emplGrade = new PositionGradeQuery("i");
            var position = new PositionQuery("h");
            var unit = new OrganizationUnitQuery("g");
            var info = new EmployeeWorkingInfoQuery("f");
            var remuneration = new AppStandardReferenceItemQuery("e");
            var type = new AppStandardReferenceItemQuery("d");
            var status = new AppStandardReferenceItemQuery("c");
            var supervisor = new VwEmployeeTableQuery("b");
            var org = new OrganizationUnitQuery("j");
            var div = new OrganizationUnitQuery("k");
            var sdiv = new OrganizationUnitQuery("l");

            query.LeftJoin(supervisor).On(query.SupervisorId == supervisor.PersonID);
            query.LeftJoin(status).On
                    (
                        query.SREmployeeStatus == status.ItemID &
                        status.StandardReferenceID == AppEnum.StandardReference.EmployeeStatus
                    );
            query.LeftJoin(type).On
                    (
                        query.SREmployeeType == type.ItemID &
                        type.StandardReferenceID == AppEnum.StandardReference.EmployeeType
                    );
            query.LeftJoin(remuneration).On
                    (
                        query.SRRemunerationType == remuneration.ItemID &
                        remuneration.StandardReferenceID == AppEnum.StandardReference.RemunerationType
                    );
            query.LeftJoin(info).On(query.PersonID == info.PersonID);
            query.LeftJoin(unit).On(query.ServiceUnitID == unit.OrganizationUnitID);
            query.LeftJoin(position).On(query.PositionID == position.PositionID);
            query.LeftJoin(emplGrade).On(query.PositionGradeID == emplGrade.PositionGradeID);
            query.LeftJoin(org).On(org.OrganizationUnitID == query.OrganizationUnitID);
            query.LeftJoin(div).On(div.OrganizationUnitID == query.SubOrganizationUnitID);
            query.LeftJoin(sdiv).On(sdiv.OrganizationUnitID == query.SubDivisonID);

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                           query.PersonID,
                           query.EmployeeNumber.As("EmployeeNo"),
                           query.EmployeeName,
                           supervisor.EmployeeName.As("SupervisorName"),
                           status.ItemName.As("EmployeeStatusName"),
                           type.ItemName.As("EmployeeTypeName"),
                           remuneration.ItemName.As("RemunerationTypeName"),
                           info.AbsenceCardNo,
                           query.JoinDate,
                           position.PositionName,
                           emplGrade.PositionGradeName,
                           unit.OrganizationUnitName.As("ServiceUnitName"),
                            org.OrganizationUnitName,
                           info.LastUpdateDateTime,
                           info.LastUpdateByUserID,
                           @"<CASE WHEN k.OrganizationUnitName IS NULL THEN l.OrganizationUnitName ELSE (CASE WHEN l.OrganizationUnitName IS NULL THEN k.OrganizationUnitName ELSE k.OrganizationUnitName + ' - ' + l.OrganizationUnitName END) END AS 'Division'>"
                        );

            if (getPageID == "recruit")
            {
                var eepq = new EmployeeEmploymentPeriodQuery("eep");
                query.LeftJoin(eepq).On(eepq.PersonID == query.PersonID && eepq.SREmploymentType == "0");
                query.Select(@"<ISNULL(eep.EmployeeNumber, a.EmployeeNumber) AS EmployeeNumber>");
                query.Where(query.SREmploymentType == "0");

                if (!string.IsNullOrEmpty(txtEmployeeNo.Text))
                {
                    if (cboFilterEmployeeNumber.SelectedIndex == 1)
                        query.Where(query.Or(query.EmployeeNumber == txtEmployeeNo.Text, eepq.EmployeeNumber == txtEmployeeNo.Text));
                    else
                    {
                        string searchTextContain = string.Format("%{0}%", txtEmployeeNo.Text);
                        query.Where(query.Or(query.EmployeeNumber.Like(searchTextContain),
                            eepq.EmployeeNumber.Like(searchTextContain)
                            ));
                    }
                }
            }
            else
            {
                query.Select(query.EmployeeNumber);
                query.Where(query.SREmploymentType != "0");

                if (!string.IsNullOrEmpty(txtEmployeeNo.Text))
                {
                    if (cboFilterEmployeeNumber.SelectedIndex == 1)
                        query.Where(query.EmployeeNumber == txtEmployeeNo.Text);
                    else
                    {
                        string searchTextContain = string.Format("%{0}%", txtEmployeeNo.Text);
                        query.Where(query.EmployeeNumber.Like(searchTextContain));
                    }
                }
            }
                
            if (!string.IsNullOrEmpty(txtEmployeeName.Text))
            {
                if (cboFilterEmployeeName.SelectedIndex == 1)
                    query.Where(query.EmployeeName == txtEmployeeName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeName.Text);
                    query.Where(query.EmployeeName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSREmployeeStatus.SelectedValue))
                query.Where(query.SREmployeeStatus == cboSREmployeeStatus.SelectedValue);
            if (!string.IsNullOrEmpty(cboSREmployeeType.SelectedValue))
                query.Where(query.SREmployeeType == cboSREmployeeType.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRRemunerationType.SelectedValue))
                query.Where(query.SRRemunerationType == cboSRRemunerationType.SelectedValue);
            if (!string.IsNullOrEmpty(cboOrganizationUnit.SelectedValue))
                query.Where(query.Or(query.OrganizationUnitID == cboOrganizationUnit.SelectedValue.ToInt(), 
                    query.SubOrganizationUnitID == cboOrganizationUnit.SelectedValue.ToInt(), 
                    query.SubDivisonID == cboOrganizationUnit.SelectedValue.ToInt(), 
                    query.ServiceUnitID == cboOrganizationUnit.SelectedValue));

            query.OrderBy(query.PersonID.Ascending);
            
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset
        }
    }
}
