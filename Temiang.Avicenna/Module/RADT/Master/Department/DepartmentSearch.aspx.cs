using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class DepartmentSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.Department;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new DepartmentQuery();
            if (!string.IsNullOrEmpty(txtDepartmentID.Text))
            {
                if (cboFilterDepartmentID.SelectedIndex == 1)
                    query.Where(query.DepartmentID == txtDepartmentID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDepartmentID.Text);
                    query.Where(query.DepartmentID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtDepartmentName.Text))
            {
                if (cboFilterDepartmentName.SelectedIndex == 1)
                    query.Where(query.DepartmentName == txtDepartmentName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDepartmentName.Text);
                    query.Where(query.DepartmentName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.DepartmentID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
