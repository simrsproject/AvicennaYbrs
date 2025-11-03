using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class LocationSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.Location;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new LocationQuery("a");
            var sg = new AppStandardReferenceItemQuery("b");
            query.LeftJoin(sg).On(sg.StandardReferenceID == "StockGroup" && sg.ItemID == query.SRStockGroup);

            query.Select(
                query.LocationID,
                query.LocationName,
                query.ShortName,
                query.IsHoldForTransaction,
                query.IsActive,
                query.IsConsignment,
                query.SRStockGroup,
                sg.ItemName.As("StockGroupName")
                );

            if (!string.IsNullOrEmpty(txtLocationName.Text))
            {
                if (cboFilterLocationName.SelectedIndex == 1)
                    query.Where(query.LocationName == txtLocationName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtLocationName.Text);
                    query.Where(query.LocationName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtParentID.Text))
            {
                if (cboFilterParentID.SelectedIndex == 1)
                    query.Where(query.ParentID == txtParentID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtParentID.Text);
                    query.Where(query.ParentID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtItemGroupID.Text))
            {
                if (cboFilterItemGroupID.SelectedIndex == 1)
                    query.Where(query.ItemGroupID == txtItemGroupID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtItemGroupID.Text);
                    query.Where(query.ItemGroupID.Like(searchTextContain));
                }
            }
            query.OrderBy(query.LocationID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
