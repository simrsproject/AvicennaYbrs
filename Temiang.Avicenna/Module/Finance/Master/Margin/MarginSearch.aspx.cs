using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class MarginSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.MARGIN;
        }

        public override bool  OnButtonOkClicked()
        {
            var query = new ItemProductMarginQuery("a");
            query.Select(
                            query.MarginID,
                            query.MarginName,
                            query.IsActive
                        );

            query.OrderBy(query.MarginID.Ascending);

            if (!string.IsNullOrEmpty(txtMarginID.Text))
            {
                if (cboFilterMarginID.SelectedIndex == 1)
                    query.Where(query.MarginID == txtMarginID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtMarginID.Text);
                    query.Where(query.MarginID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtMarginName.Text))
            {
                if (cboFilterMarginName.SelectedIndex == 1)
                    query.Where(query.MarginName == txtMarginName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtMarginName.Text);
                    query.Where(query.MarginName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
