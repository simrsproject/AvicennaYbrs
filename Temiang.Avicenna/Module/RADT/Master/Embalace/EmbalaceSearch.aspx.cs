using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class EmbalaceSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.Embalace;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new EmbalaceQuery();
            if (!string.IsNullOrEmpty(txtEmbalaceID.Text))
            {
                if (cboFilterEmbalaceID.SelectedIndex == 1)
                    query.Where(query.EmbalaceID == txtEmbalaceID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEmbalaceID.Text);
                    query.Where(query.EmbalaceID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtEmbalaceName.Text))
            {
                if (cboFilterEmbalaceName.SelectedIndex == 1)
                    query.Where(query.EmbalaceName == txtEmbalaceName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEmbalaceName.Text);
                    query.Where(query.EmbalaceName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.EmbalaceID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
