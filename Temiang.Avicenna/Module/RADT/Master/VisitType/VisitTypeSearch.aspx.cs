using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class VisitTypeSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.VisitType;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new VisitTypeQuery();
            if (!string.IsNullOrEmpty(txtVisitTypeID.Text))
            {
                if (cboFilterVisitTypeID.SelectedIndex == 1)
                    query.Where(query.VisitTypeID == txtVisitTypeID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtVisitTypeID.Text);
                    query.Where(query.VisitTypeID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtVisitTypeName.Text))
            {
                if (cboFilterVisitTypeName.SelectedIndex == 1)
                    query.Where(query.VisitTypeName == txtVisitTypeName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtVisitTypeName.Text);
                    query.Where(query.VisitTypeName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.VisitTypeID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
