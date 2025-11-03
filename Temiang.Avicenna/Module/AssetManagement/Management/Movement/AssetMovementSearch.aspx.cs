using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;


namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class AssetMovementSearch : BasePageDialog
    {
        private string FormType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = FormType == "req" ? AppConstant.Program.ASSET_MOVEMENT_REQUEST : AppConstant.Program.ASSET_MOVEMENT;

            if (!IsPostBack)
            {
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new AssetMovementQuery("a");
            var asset = new AssetQuery("b");
            var fsu = new ServiceUnitQuery("c");
            var floc = new LocationQuery("d");
            var tsu = new ServiceUnitQuery("e");
            var tloc = new LocationQuery("f");

            query.InnerJoin(asset).On(asset.AssetID == query.AssetID);
            query.InnerJoin(fsu).On(fsu.ServiceUnitID == query.FromServiceUnitID);
            query.LeftJoin(floc).On(floc.LocationID == query.FromAssetLocationID);
            query.InnerJoin(tsu).On(tsu.ServiceUnitID == query.ToServiceUnitID);
            query.LeftJoin(tloc).On(tloc.LocationID == query.ToAssetLocationID);

            if (FormType == "req")
            {
                var usr = new AppUserServiceUnitQuery("usr");
                query.InnerJoin(usr).On(usr.ServiceUnitID == query.FromServiceUnitID && usr.UserID == AppSession.UserLogin.UserID);
            }

            query.OrderBy
                (
                    query.AssetMovementNo.Descending
                );

            query.Select(
                query.AssetMovementNo,
                query.MovementDate,
                query.AssetID,
                asset.AssetName,
                query.FromServiceUnitID,
                fsu.ServiceUnitName.As("FromServiceUnitName"),
                query.FromAssetLocationID,
                floc.LocationName.As("FromLocationName"),
                query.ToServiceUnitID,
                tsu.ServiceUnitName.As("ToServiceUnitName"),
                query.ToAssetLocationID,
                tloc.LocationName.As("ToLocationName"),
                query.Notes,
                query.IsPosted,
                query.IsApproved,
                query.LastUpdateDateTime,
                query.LastUpdateByUserID
                );

            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            {
                query.Where(query.AssetMovementNo == txtTransactionNo.Text);
            }
            if (!string.IsNullOrEmpty(txtAssetId.Text))
            {
                query.Where(query.AssetID == txtAssetId.Text);
            }
            if (!string.IsNullOrEmpty(txtAssetName.Text))
            {
                if (cboFilterAssetName.SelectedIndex == 1)
                    query.Where(asset.AssetName == txtAssetName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtAssetName.Text);
                    query.Where(asset.AssetName.Like(searchText));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}