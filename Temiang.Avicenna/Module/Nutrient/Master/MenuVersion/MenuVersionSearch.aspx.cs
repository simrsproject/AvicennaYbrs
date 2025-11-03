using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class MenuVersionSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = Request.QueryString["ext"] == "1" ? AppConstant.Program.MenuExtraVersion : AppConstant.Program.MenuVersion;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }

        public override bool OnButtonOkClicked()
        {
            var query = new MenuVersionQuery("a");
            query.Select
                (
                    query.VersionID,
                    query.VersionName,
                    query.Cycle,
                    query.IsPlusOneRule,
                    query.IsActive
                );
            if (!string.IsNullOrEmpty(txtVersionID.Text))
            {
                if (cboFilterVersionID.SelectedIndex == 1)
                    query.Where(query.VersionID == txtVersionID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtVersionID.Text);
                    query.Where(query.VersionID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtVersionName.Text))
            {
                if (cboFilterVersionName.SelectedIndex == 1)
                    query.Where(query.VersionName == txtVersionName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtVersionName.Text);
                    query.Where(query.VersionName.Like(searchTextContain));
                }
            }
            if (Request.QueryString["ext"] == "1")
                query.Where(query.IsExtra == true);
            else
                query.Where(query.IsExtra == false);

            query.OrderBy(query.VersionID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
