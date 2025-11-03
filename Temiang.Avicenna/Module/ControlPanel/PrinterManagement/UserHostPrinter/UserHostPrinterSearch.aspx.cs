using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.ControlPanel.PrinterManagement
{
    public partial class UserHostPrinterSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.UserHostPrinter;//TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var query = new UserHostPrinterQuery("a");
            PrinterQuery qb = new PrinterQuery("b");
            query.LeftJoin(qb).On(query.PrinterID == qb.PrinterID);
            query.Select
                (
                query.UserHostName,
                query.Description,
                query.PrinterID,
                qb.PrinterName
                );

            if (!string.IsNullOrEmpty(txtUserHostName.Text))
            {
                if (cboFilterUserHostName.SelectedIndex == 1)
                    query.Where(query.UserHostName == txtUserHostName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtUserHostName.Text);
                    query.Where(query.UserHostName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtDescription.Text))
            {
                if (cboFilterDescription.SelectedIndex == 1)
                    query.Where(query.Description == txtDescription.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDescription.Text);
                    query.Where(query.Description.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
