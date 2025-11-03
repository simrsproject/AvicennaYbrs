using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ProcedureSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.Procedure;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ProcedureQuery();
            if (!string.IsNullOrEmpty(txtProcedureID.Text))
            {
                if (cboFilterProcedureID.SelectedIndex == 1)
                    query.Where(query.ProcedureID == txtProcedureID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtProcedureID.Text);
                    query.Where(query.ProcedureID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtProcedureName.Text))
            {
                if (cboFilterProcedureName.SelectedIndex == 1)
                    query.Where(query.ProcedureName == txtProcedureName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtProcedureName.Text);
                    query.Where(query.ProcedureName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.ProcedureID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
