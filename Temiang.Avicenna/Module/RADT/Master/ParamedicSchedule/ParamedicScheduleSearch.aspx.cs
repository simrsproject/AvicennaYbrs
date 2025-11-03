using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicScheduleSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.ParamedicSchedule;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ParamedicScheduleQuery("a");
            var paramedicQuery = new ParamedicQuery("p");
            var unit = new ServiceUnitQuery("c");
            var tcode = new ServiceUnitTransactionCodeQuery("d");

            query.Select
                (
                    query.ServiceUnitID,
                    query.ParamedicID,
                    query.Notes,
                    query.PeriodYear,
                    unit.ServiceUnitName,
                    paramedicQuery.ParamedicName,
                    paramedicQuery.Address
                );
            query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
            query.InnerJoin(paramedicQuery).On(query.ParamedicID == paramedicQuery.ParamedicID);
            query.LeftJoin(tcode).On(tcode.ServiceUnitID == query.ServiceUnitID && tcode.SRTransactionCode == BusinessObject.Reference.TransactionCode.JobOrder.ToString());
            query.Where(tcode.ServiceUnitID.IsNull());

            if (!string.IsNullOrEmpty(txtParamedicName.Text))
            {
                if (cboFilterParamedicName.SelectedIndex == 1)
                    query.Where(paramedicQuery.ParamedicName == txtParamedicName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtParamedicName.Text);
                    query.Where(paramedicQuery.ParamedicName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtPeriodYear.Text))
                query.Where(query.PeriodYear == txtPeriodYear.Text);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
