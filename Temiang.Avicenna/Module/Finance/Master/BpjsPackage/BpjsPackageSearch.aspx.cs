using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class BpjsPackageSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.BpjsPackage;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new BpjsPackageQuery("a");
            query.Select(
                            query.PackageID,
                            query.PackageName,
                            query.IsActive
                        );

            if (!string.IsNullOrEmpty(txtPackageID.Text))
            {
                if (cboFilterPackageID.SelectedIndex == 1)
                    query.Where(query.PackageID == txtPackageID.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtPackageID.Text);
                    query.Where(query.PackageID.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(txtPackageName.Text))
            {
                if (cboFilterPackageName.SelectedIndex == 1)
                    query.Where(query.PackageName == txtPackageName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtPackageName.Text);
                    query.Where(query.PackageName.Like(searchText));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}