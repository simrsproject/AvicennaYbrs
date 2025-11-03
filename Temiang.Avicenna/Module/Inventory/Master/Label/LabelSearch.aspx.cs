using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class LabelSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.Label;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new LabellQuery();
            if (!string.IsNullOrEmpty(txtLabelName.Text))
            {
                if (cboFilterLabelName.SelectedIndex == 1)
                    query.Where(query.LabelName == txtLabelName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtLabelName.Text);
                    query.Where(query.LabelName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.LabelID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
