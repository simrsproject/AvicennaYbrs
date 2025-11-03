using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.ControlPanel.PrinterManagement
{
    public partial class PrinterSearch : BasePageDialog
    {
	    protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PrinterLocation;
        }
		
        public override bool OnButtonOkClicked()
        {
            var query = new PrinterQuery();
			if (! string.IsNullOrEmpty(txtPrinterID.Text))
            {
                if (cboFilterPrinterID.SelectedIndex == 1)
                    query.Where(query.PrinterID == txtPrinterID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPrinterID.Text);
                    query.Where(query.PrinterID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtPrinterName.Text))
            {
                if (cboFilterPrinterName.SelectedIndex == 1)
                    query.Where(query.PrinterName == txtPrinterName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPrinterName.Text);
                    query.Where(query.PrinterName.Like(searchTextContain));
                }
            }		


            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
