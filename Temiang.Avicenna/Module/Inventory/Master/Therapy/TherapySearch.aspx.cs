using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class TherapySearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.Therapy;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new TherapyQuery();
            if (!string.IsNullOrEmpty(txtTherapyName.Text))
            {
                if (cboFilterTherapyName.SelectedIndex == 1)
                    query.Where(query.TherapyName == txtTherapyName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTherapyName.Text);
                    query.Where(query.TherapyName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.TherapyID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
