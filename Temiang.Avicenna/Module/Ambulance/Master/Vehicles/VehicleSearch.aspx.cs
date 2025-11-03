using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Ambulance.Master
{
    public partial class VehicleSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.Vehicle;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            VehiclesQuery query = new VehiclesQuery("a");
            var stVT = new AppStandardReferenceItemQuery("stVT");
            var stVS = new AppStandardReferenceItemQuery("stVS");
            var ast = new AssetQuery("ast");
            query.LeftJoin(stVT).On(stVT.StandardReferenceID == "VehicleType" && stVT.ItemID == query.SRVehicleType)
               .LeftJoin(stVS).On(stVS.StandardReferenceID == "VehicleStatus" && stVS.ItemID == query.SRVehicleStatus)
               .LeftJoin(ast).On(query.AssetID == ast.AssetID);
            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(query.VehicleID, query.PlateNo, query.SRVehicleType, query.SRVehicleStatus,
                stVT.ItemName.As("SRVehicleTypeName"),
                stVS.ItemName.As("SRVehicleStatusName"),
                query.IsActive
                );
            query.OrderBy(query.PlateNo.Ascending);

            if (!string.IsNullOrEmpty(txtPlatNo.Text))
            {
                if (cboFilterPlatNo.SelectedIndex == 1)
                    query.Where(query.PlateNo == txtPlatNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPlatNo.Text);
                    query.Where(query.PlateNo.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}