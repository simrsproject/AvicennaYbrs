using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalInfoSearch : BasePageDialog
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

            ProgramID = getPageID == "recruit" ? AppConstant.Program.ApplicantPersonalInfo : AppConstant.Program.PersonalInfo;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                if (getPageID == "recruit")
                {
                    lblEmployeeNumber.Text = "Applicant No";
                    lblEmployeeName.Text = "Applicant Name";
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new PersonalInfoQuery("a");
            var gender = new AppStandardReferenceItemQuery("b");
            var religion = new AppStandardReferenceItemQuery("c");
            //var blood = new AppStandardReferenceItemQuery("d");
            var marital = new AppStandardReferenceItemQuery("e");
            var wi = new EmployeeWorkingInfoQuery("f");
            var cls = new ClassQuery("g");
            var status = new AppStandardReferenceItemQuery("h");
            var unit = new OrganizationUnitQuery("x");
            var info = new VwEmployeeTableQuery("y");

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                            query.PersonID,
                            query.EmployeeNumber.As("EmployeeNo"),
                            query.EmployeeName,
                            query.PlaceBirth,
                            query.BirthDate,
                            query.SRGenderType,
                            gender.ItemName.As("GenderName"),
                            query.SRReligion,
                            religion.ItemName.As("ReligionName"),
                            //query.SRBloodType,
                            //blood.ItemName.As("BloodType"),
                            query.SRMaritalStatus,
                            marital.ItemName.As("MaritalStatus"),
                            query.LastUpdateDateTime,
                            query.LastUpdateByUserID,
                            cls.ClassName, info.Nik
                        );
            query.LeftJoin(gender).On
                    (
                        query.SRGenderType == gender.ItemID &
                        gender.StandardReferenceID == "GenderType"
                    );
            query.LeftJoin(religion).On
                    (
                        query.SRReligion == religion.ItemID &
                        religion.StandardReferenceID == "Religion"
                    );
            //query.LeftJoin(blood).On
            //        (
            //            query.SRBloodType == blood.ItemID &
            //            blood.StandardReferenceID == "BloodType"
            //        );
            query.LeftJoin(marital).On
                    (
                        query.SRMaritalStatus == marital.ItemID &
                        marital.StandardReferenceID == "TaxStatus"
                    );

            query.LeftJoin(cls).On(query.CoverageClass == cls.ClassID);
            query.LeftJoin(wi).On(query.PersonID == wi.PersonID);
            query.LeftJoin(status).On
                    (
                        wi.SREmployeeStatus == status.ItemID &
                        status.StandardReferenceID == AppEnum.StandardReference.EmployeeStatus
                    );
            query.InnerJoin(info).On(query.PersonID == info.PersonID);
            query.LeftJoin(unit).On(info.ServiceUnitID == unit.OrganizationUnitID);

            if (getPageID == "recruit")
            {
                var eepq = new EmployeeEmploymentPeriodQuery("eep");
                query.LeftJoin(eepq).On(eepq.PersonID == query.PersonID && eepq.SREmploymentType == "0");
                query.Select(@"<ISNULL(eep.EmployeeNumber, a.EmployeeNumber) AS EmployeeNumber>");
                query.Where(info.SREmploymentType == "0"); // applicant

                if (!string.IsNullOrEmpty(txtEmployeeNumber.Text))
                {
                    if (cboFilterEmployeeNumber.SelectedIndex == 1)
                        query.Where(query.Or(query.EmployeeNumber == txtEmployeeNumber.Text, eepq.EmployeeNumber == txtEmployeeNumber.Text));
                    else
                    {
                        string searchTextContain = string.Format("%{0}%", txtEmployeeNumber.Text);
                        query.Where(query.Or(query.EmployeeNumber.Like(searchTextContain),
                            eepq.EmployeeNumber.Like(searchTextContain)
                            ));
                    }
                }
            }
            else
            {
                query.Select(query.EmployeeNumber);
                query.Where(info.SREmploymentType != "0");

                if (!string.IsNullOrEmpty(txtEmployeeNumber.Text))
                {
                    if (cboFilterEmployeeNumber.SelectedIndex == 1)
                        query.Where(query.EmployeeNumber == txtEmployeeNumber.Text);
                    else
                    {
                        string searchTextContain = string.Format("%{0}%", txtEmployeeNumber.Text);
                        query.Where(query.EmployeeNumber.Like(searchTextContain));
                    }
                }
            }
                
            query.OrderBy(query.PersonID.Ascending);

            if (!string.IsNullOrEmpty(txtEmployeeName.Text))
            {
                if (cboFilterEmployeeName.SelectedIndex == 1)
                    query.Where(info.EmployeeName == txtEmployeeName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeName.Text);
                    query.Where(info.EmployeeName.Like(searchTextContain));
                }
            }

            if (!string.IsNullOrEmpty(txtNik.Text))
            {
                if (cboFilterNik.SelectedIndex == 1)
                    query.Where(info.Nik == txtNik.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtNik.Text);
                    query.Where(info.Nik.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();
            return true;
        }
    }
}
