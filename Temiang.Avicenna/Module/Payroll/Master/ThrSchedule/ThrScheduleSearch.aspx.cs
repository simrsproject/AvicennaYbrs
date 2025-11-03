using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using System.Data;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class ThrScheduleSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ThrSchedule;

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSRReligion, AppEnum.StandardReference.Religion);

        }

        protected void cboPayrollPeriodID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.PayrollPeriodItemsRequested((RadComboBox)o, e.Text);
        }

        protected void cboPayrollPeriodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.PayrollPeriodItemDataBound(e);
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ThrScheduleQuery("a");
            var pp = new PayrollPeriodQuery("b");
            var tsi = new ThrScheduleItemQuery("c");
            query.InnerJoin(pp).On(query.PayrollPeriodID == pp.PayrollPeriodID);
            query.InnerJoin(tsi).On(tsi.CounterID == query.CounterID);
            query.Select(query.CounterID, pp.PayrollPeriodCode, query.PayrollPeriodName, query.PayDate, query.SPTYear, query.LastUpdateByUserID, query.LastUpdateByUserID);
            
            query.es.Distinct = true;

            if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                query.Where(query.PayrollPeriodID == Convert.ToInt32(cboPayrollPeriodID.SelectedValue));
            if (!string.IsNullOrEmpty(cboSRReligion.SelectedValue))
                query.Where(tsi.SRReligion == cboSRReligion.SelectedValue);

            query.OrderBy(pp.PayrollPeriodCode.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
