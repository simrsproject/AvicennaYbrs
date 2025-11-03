using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class IndicationSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.Indication;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new IndicationQuery();
            if (!string.IsNullOrEmpty(txtIndicationName.Text))
            {
                if (cboFilterIndicationName.SelectedIndex == 1)
                    query.Where(query.IndicationName == txtIndicationName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtIndicationName.Text);
                    query.Where(query.IndicationName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.IndicationID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
