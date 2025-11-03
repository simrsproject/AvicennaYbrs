using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.Paramedic;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool  OnButtonOkClicked()
        {
            var query = new ParamedicQuery();
            if (!string.IsNullOrEmpty(txtParamedicID.Text))
            {
                if (cboFilterParamedicID.SelectedIndex == 1)
                    query.Where(query.ParamedicID == txtParamedicID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtParamedicID.Text);
                    query.Where(query.ParamedicID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtParamedicName.Text))
            {
                if (cboFilterParamedicName.SelectedIndex == 1)
                    query.Where(query.ParamedicName == txtParamedicName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtParamedicName.Text);
                    query.Where(query.ParamedicName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
