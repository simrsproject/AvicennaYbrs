using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.AssetManagement.Management
{
    public partial class SanitationMaintenanceActivityScheduleSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.SanitationMaintenanceActivitySchedule;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrder, false);
                ComboBox.PopulateWorkTradeItemList(cboSRWorkTradeItem, AppSession.Parameter.WorkTradeSanitation, true);
            }

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new SanitationMaintenanceActivitySchedulePeriodQuery("a");
            var wti = new AppStandardReferenceItemQuery("b");
            var unit = new ServiceUnitQuery("c");

            query.Select
                (
                    query.SRWorkTradeItem,
                    query.ServiceUnitID,
                    query.PeriodYear,
                    wti.ItemName.As("WorkTradeItemName"),
                    unit.ServiceUnitName
                );
            query.InnerJoin(wti).On(wti.StandardReferenceID == "WorkTradeItem" && wti.ItemID == query.SRWorkTradeItem);
            query.InnerJoin(unit).On(unit.ServiceUnitID == query.ServiceUnitID);

            query.OrderBy(query.PeriodYear.Descending, query.ServiceUnitID.Ascending);

            if (!string.IsNullOrEmpty(txtPeriodYear.Text))
                query.Where(query.PeriodYear == txtPeriodYear.Text);
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRWorkTradeItem.SelectedValue))
                query.Where(query.SRWorkTradeItem == cboSRWorkTradeItem.SelectedValue);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}