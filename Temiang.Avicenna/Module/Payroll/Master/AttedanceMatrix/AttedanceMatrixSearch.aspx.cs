using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class AttedanceMatrixSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AttedanceMatrix; //TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var query = new AttedanceMatrixQuery();
            if (!string.IsNullOrEmpty(txtAttedanceMatrixName.Text))
            {
                if (cboFilterAttedanceMatrixName.SelectedIndex == 1)
                    query.Where(query.AttedanceMatrixName == txtAttedanceMatrixName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtAttedanceMatrixName.Text);
                    query.Where(query.AttedanceMatrixName.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(txtAttedanceMatrixFieldt.Text))
            {
                if (cboFilterAttedanceMatrixFieldt.SelectedIndex == 1)
                    query.Where(query.AttedanceMatrixFieldt == txtAttedanceMatrixFieldt.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtAttedanceMatrixFieldt.Text);
                    query.Where(query.AttedanceMatrixFieldt.Like(searchText));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
