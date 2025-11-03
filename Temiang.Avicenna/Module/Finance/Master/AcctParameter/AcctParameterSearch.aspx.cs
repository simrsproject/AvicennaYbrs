using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master.AcctParameter
{
    public partial class AcctParameterSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.AccountingParameter;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new AppParameterQuery();
            if (!string.IsNullOrEmpty(txtParameterID.Text))
            {
                string searchText = string.Format("%{0}%", txtParameterID.Text);
                if (cboFilterParameterID.SelectedIndex == 1)
                    query.Where(query.ParameterID == txtParameterID.Text);

                else
                    query.Where(query.ParameterID.Like(searchText));
            }
            if (!string.IsNullOrEmpty(txtParameterName.Text))
            {
                if (cboFilterParameterID.SelectedIndex == 1)
                    query.Where(query.ParameterName == txtParameterName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtParameterName.Text);
                    query.Where(query.ParameterName.Like(searchText));
                }
            }
            query.Where(
                            query.IsUsedBySystem == false,
                            query.ParameterID.Like("coa%")

                        );

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset
            return true;
        }
    }
}
