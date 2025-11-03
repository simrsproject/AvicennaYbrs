using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class BodyDiagramSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.BodyDiagram;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new BodyDiagramQuery();
            if (!string.IsNullOrEmpty(txtBodyID.Text))
            {
                if (cboFilterBodyID.SelectedIndex == 1)
                    query.Where(query.BodyID == txtBodyID.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtBodyID.Text);
                    query.Where(query.BodyID.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(txtBodyName.Text))
            {
                if (cboFilterBodyName.SelectedIndex == 1)
                    query.Where(query.BodyName == txtBodyName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtBodyName.Text);
                    query.Where(query.BodyName.Like(searchText));
                }
            }
            query.OrderBy(query.BodyID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
