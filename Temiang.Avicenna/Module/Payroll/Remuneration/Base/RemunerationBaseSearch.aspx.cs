using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Remuneration.Base
{
    public partial class RemunerationBaseSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.EmployeeRemunerationBase;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new WageBaseQuery("a");
            query.Select(query);
            query.OrderBy(query.ValidFrom.Descending);

            if (!txtFromDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                query.Where(query.ValidFrom >= txtFromDate.SelectedDate);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}