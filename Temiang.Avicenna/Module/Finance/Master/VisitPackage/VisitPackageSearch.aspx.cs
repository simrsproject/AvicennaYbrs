using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class VisitPackageSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.VisitPackage;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new VisitPackageQuery();
            query.OrderBy(query.VisitPackageID.Ascending);

            if (!string.IsNullOrEmpty(txtVisitPackageID.Text))
            {
                if (cboFilterVisitPackageID.SelectedIndex == 1)
                    query.Where(query.VisitPackageID == txtVisitPackageID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtVisitPackageID.Text);
                    query.Where(query.VisitPackageID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtVisitPackageName.Text))
            {
                if (cboFilterVisitPackageName.SelectedIndex == 1)
                    query.Where(query.VisitPackageName == txtVisitPackageName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtVisitPackageName.Text);
                    query.Where(query.VisitPackageName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}