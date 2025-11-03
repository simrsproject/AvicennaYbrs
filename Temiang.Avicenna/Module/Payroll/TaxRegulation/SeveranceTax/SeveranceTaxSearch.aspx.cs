using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.TaxRegulation
{
    public partial class SeveranceTaxSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.SeveranceTax; //TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var query = new SeveranceTaxQuery();
            if (txtValidFrom.SelectedDate != null)
            {
                query.Where(query.ValidFrom == txtValidFrom.SelectedDate.Value.Date);
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
