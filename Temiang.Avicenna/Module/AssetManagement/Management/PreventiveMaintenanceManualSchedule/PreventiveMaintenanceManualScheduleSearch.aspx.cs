using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class PreventiveMaintenanceManualScheduleSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.AssetPreventiveMaintenanceManualSchedule;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrder, false);
                ComboBox.PopulateWithServiceUnitForTransaction(cboMaintenanceServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrderRealization, true);
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
            var query = new AssetPreventiveMaintenanceSchedulePeriodQuery("a");
            var asset = new AssetQuery("b");
            var unit = new ServiceUnitQuery("c");
            var usr = new AppUserServiceUnitQuery("d");

            query.Select
                (
                    query.AssetID,
                    query.PeriodYear,
                    asset.AssetName,
                    asset.SerialNumber,
                    unit.ServiceUnitName,
                    @"<b.AssetName + ' (SN : ' + b.SerialNumber + ' - Unit : ' + c.ServiceUnitName + ')' AS 'Group'>"
                );
            query.InnerJoin(asset).On(query.AssetID == asset.AssetID);
            query.InnerJoin(unit).On(asset.ServiceUnitID == unit.ServiceUnitID);
            query.InnerJoin(usr).On(asset.MaintenanceServiceUnitID == usr.ServiceUnitID &&
                                    usr.UserID == AppSession.UserLogin.UserID);

            query.OrderBy(asset.AssetName.Ascending);

            if (!string.IsNullOrEmpty(txtAsset.Text))
            {
                if (cboFilterAsset.SelectedIndex == 1)
                    query.Where(asset.AssetName == txtAsset.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtAsset.Text);
                    query.Where(asset.AssetName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtPeriodYear.Text))
                query.Where(query.PeriodYear == txtPeriodYear.Text);
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                query.Where(asset.ServiceUnitID == cboServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboMaintenanceServiceUnitID.SelectedValue))
                query.Where(asset.MaintenanceServiceUnitID == cboMaintenanceServiceUnitID.SelectedValue);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
