using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Remuneration
{
    public partial class IncentiveProcessSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.EmployeeIncentiveProcess;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new EmployeeIncentiveProcessQuery("a");
            var pp = new PayrollPeriodQuery("b");
            query.InnerJoin(pp).On(pp.PayrollPeriodID == query.PayrollPeriodID);
            query.Select(query, pp.PayrollPeriodName);
            query.OrderBy(pp.PayrollPeriodCode.Descending);

            if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                query.Where(query.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt());

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }

        protected void cboPayrollPeriodID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.PayrollPeriodItemsRequested((RadComboBox)o, e.Text);
        }

        protected void cboPayrollPeriodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.PayrollPeriodItemDataBound(e);
        }
    }
}