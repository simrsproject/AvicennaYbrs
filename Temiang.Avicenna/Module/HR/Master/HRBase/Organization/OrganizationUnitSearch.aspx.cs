using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Master
{
    public partial class OrganizationUnitSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.OrganizationUnit; //TODO: Isi ProgramID

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpaceSortByLineNumber(cboSROrganizationLevel, AppEnum.StandardReference.OrganizationLevel);
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new OrganizationUnitQuery("a");
            var level = new AppStandardReferenceItemQuery("b");
            var parent = new OrganizationUnitQuery("c");
            var vet = new VwEmployeeTableQuery("d");
            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                            query.OrganizationUnitID,
                            query.OrganizationUnitCode,
                            query.OrganizationUnitName,
                            query.ParentOrganizationUnitID,
                            parent.OrganizationUnitName.As("ParentOrganizationUnitName"),
                            query.SROrganizationLevel,
                            level.ItemName.As("OrganizationLevelName"),
                            vet.EmployeeName,
                            query.IsActive,
                            query.LastUpdateDateTime,
                            query.LastUpdateByUserID
                        );
            query.LeftJoin(level).On
                    (
                        query.SROrganizationLevel == level.ItemID &
                        level.StandardReferenceID == "OrganizationLevel"
                    );
            query.LeftJoin(parent).On(query.ParentOrganizationUnitID == parent.OrganizationUnitID);
            query.LeftJoin(vet).On(vet.PersonID == query.PersonID);
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            if (!string.IsNullOrEmpty(txtOrganizationUnitCode.Text))
            {
                if (cboFilterOrganizationUnitCode.SelectedIndex == 1)
                    query.Where(query.OrganizationUnitCode == txtOrganizationUnitCode.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtOrganizationUnitCode.Text);
                    query.Where(query.OrganizationUnitCode.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtOrganizationUnitName.Text))
            {
                if (cboFilterOrganizationUnitName.SelectedIndex == 1)
                    query.Where(query.OrganizationUnitName == txtOrganizationUnitName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtOrganizationUnitName.Text);
                    query.Where(query.OrganizationUnitName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtParentOrganizationUnitName.Text))
            {
                if (cboFilterParentOrganizationUnitName.SelectedIndex == 1)
                    query.Where(parent.OrganizationUnitName == txtParentOrganizationUnitName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtParentOrganizationUnitName.Text);
                    query.Where(parent.OrganizationUnitName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSROrganizationLevel.SelectedValue))
            {
                query.Where(query.SROrganizationLevel == cboSROrganizationLevel.SelectedValue);
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
