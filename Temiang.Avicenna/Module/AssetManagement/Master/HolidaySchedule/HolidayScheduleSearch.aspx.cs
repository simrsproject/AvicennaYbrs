using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.AssetManagement.Master
{
    public partial class HolidayScheduleSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.AssetHolidaySchedule;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new HolidayScheduleQuery("a");
            
            query.Select
                (
                    query.PeriodYear
                );

            query.OrderBy(query.PeriodYear.Ascending);

            if (!string.IsNullOrEmpty(txtPeriodYear.Text))
                query.Where(query.PeriodYear == txtPeriodYear.Text);

            query.es.Distinct = true;

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
