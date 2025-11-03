using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Master
{
    public partial class AbRestrictionSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.AbRestriction;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new AbRestrictionQuery("a");
            var parent = new AbRestrictionQuery("p");
            query.LeftJoin(parent).On(query.ParentID == parent.AbRestrictionID);


            if (!string.IsNullOrEmpty(txtRestrictionName.Text))
            {
                if (cboFilterRestrictionName.SelectedIndex == 1)
                    query.Where(query.AbRestrictionName == txtRestrictionName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtRestrictionName.Text);
                    query.Where(query.AbRestrictionName.Like(searchText));
                }
            }

            query.Select(query.AbRestrictionID, query.ParentID, query.AbRestrictionName, parent.AbRestrictionName.As("ParentName"));
            query.OrderBy(query.AbRestrictionID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
