using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.AssetManagement.Master
{
    public partial class AssetItemNonFixedSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.ASSET_ITEM_NONFIXEDASSETS;

            if (!IsPostBack)
            {
                var query = new AssetGroupQuery();
                query.Select(query.AssetGroupId, query.GroupName);
                query.Where(query.IsActive == true);
                query.OrderBy(query.AssetGroupId.Ascending);

                DataTable dtb = query.LoadDataTable();

                cboAssetGroupID.Items.Clear();
                cboAssetGroupID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (DataRow item in dtb.Rows)
                {
                    cboAssetGroupID.Items.Add(new RadComboBoxItem(item["GroupName"].ToString(), item["AssetGroupId"].ToString()));
                }

                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrder, false);
                ComboBox.PopulateWithServiceUnitForTransaction(cboMaintenanceServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrderRealization, false);
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new AssetQuery("a");
            var unit = new ServiceUnitQuery("b");
            var room = new ServiceRoomQuery("c");
            var agroup = new AssetGroupQuery("d");
            var astatus = new AppStandardReferenceItemQuery("e");

            query.Select
                (
                    query.AssetID,
                    query.AssetName,
                    query.BrandName,
                    query.SerialNumber,
                    agroup.GroupName.As("AssetGroupName"),
                    unit.ServiceUnitName,
                    room.RoomName.As("LocationName"),
                    query.Notes,
                    astatus.ItemName.As("AssetStatus")
                );

            query.InnerJoin(unit).On(unit.ServiceUnitID == query.ServiceUnitID);
            query.LeftJoin(room).On(room.RoomID == query.AssetLocationID);
            query.InnerJoin(agroup).On(agroup.AssetGroupId == query.AssetGroupID);
            query.InnerJoin(astatus).On
                (
                    astatus.ItemID == query.SRAssetsStatus &&
                    astatus.StandardReferenceID == "AssetsStatus"
                );
            query.Where(query.UsageTimeEstimation == 0);
            query.OrderBy(query.AssetID.Ascending);

            if (!string.IsNullOrEmpty(txtAssetID.Text))
            {
                string searchText = string.Format("%{0}%", txtAssetID.Text);
                query.Where(query.AssetID.Like(searchText));
            }
            if (!string.IsNullOrEmpty(txtAssetName.Text))
            {
                string searchText = string.Format("%{0}%", txtAssetName.Text);
                query.Where(query.AssetName.Like(searchText));
            }
            if (!string.IsNullOrEmpty(cboAssetGroupID.SelectedValue))
                query.Where(query.AssetGroupID == cboAssetGroupID.SelectedValue);
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboMaintenanceServiceUnitID.SelectedValue))
                query.Where(query.MaintenanceServiceUnitID == cboMaintenanceServiceUnitID.SelectedValue);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
