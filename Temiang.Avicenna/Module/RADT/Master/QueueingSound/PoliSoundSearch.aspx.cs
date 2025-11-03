using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class PoliSoundSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.QueueingSound;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ServiceUnitQuery();
            if (!string.IsNullOrEmpty(txtServiceUnitID.Text))
            {
                if (cboFilterServiceUnitID.SelectedIndex == 1)
                    query.Where(query.ServiceUnitID == txtServiceUnitID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtServiceUnitID.Text);
                    query.Where(query.ServiceUnitID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtServiceUnitName.Text))
            {
                if (cboFilterServiceUnitName.SelectedIndex == 1)
                    query.Where(query.ServiceUnitName == txtServiceUnitName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtServiceUnitName.Text);
                    query.Where(query.ServiceUnitName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.ServiceUnitID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
