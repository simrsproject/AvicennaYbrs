using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Ambulance.Master
{
    public partial class DriverSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.Driver;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            VehicleDriversQuery query = new VehicleDriversQuery("a");
            var stRef = new AppStandardReferenceItemQuery("st");
            query.LeftJoin(stRef).On(stRef.StandardReferenceID == "DriverStatus" && stRef.ItemID == query.SRDriverStatus);
            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(query.DriverID, query.DriverName, query.SRDriverStatus,
                stRef.ItemName.As("SRDriverStatusName"),
                query.IsActive
                );
            query.OrderBy(query.DriverName.Ascending);

            if (!string.IsNullOrEmpty(txtDriverName.Text))
            {
                if (cboFilterDriverName.SelectedIndex == 1)
                    query.Where(query.DriverName == txtDriverName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDriverName.Text);
                    query.Where(query.DriverName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}