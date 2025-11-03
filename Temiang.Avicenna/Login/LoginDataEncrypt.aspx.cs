using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna
{
    public partial class LoginDataEncrypt : BasePageDialog
    {
	    protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PrinterLocation;
        }
		
        public override bool OnButtonOkClicked()
        {
            PrinterQuery query = new PrinterQuery();
            //if (!txtPrinterID.Text.Trim().Equals(string.Empty))
            //{
            //    if (cboFilterPrinterID.SelectedIndex==1)
            //        query.Where(query.PrinterID == txtPrinterID.Text);
            //    else
            //        query.Where(query.PrinterID.Like(string.Format("%.{0}%", txtPrinterID.Text)));
            //}		
            //if (!txtPrinterName.Text.Trim().Equals(string.Empty))
            //{
            //    if (cboFilterPrinterName.SelectedIndex==1)
            //        query.Where(query.PrinterName == txtPrinterName.Text);
            //    else
            //        query.Where(query.PrinterName.Like(string.Format("%.{0}%", txtPrinterName.Text)));
            //}		


            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
