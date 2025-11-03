using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class QueueingSoundSearch : BasePageDialog
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
            var query = new QueueingSoundQuery("query");
            query.Select
                (
                    query
                );

            
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                if (cboFilterName.SelectedIndex == 1)
                    query.Where(query.Name == txtName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtName.Text);
                    query.Where(query.Name.Like(searchTextContain));
                }
            }
            query.OrderBy(query.SoundID.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
