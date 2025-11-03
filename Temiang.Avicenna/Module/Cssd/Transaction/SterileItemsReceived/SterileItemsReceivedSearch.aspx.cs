using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class SterileItemsReceivedSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CssdSterileItemsReceived;

            if (!IsPostBack)
            {
                var unitColl = new ServiceUnitCollection();
                unitColl.Query.Where(unitColl.Query.IsActive == true);
                unitColl.LoadAll();

                cboFromServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var unit in unitColl)
                {
                    cboFromServiceUnitID.Items.Add(new RadComboBoxItem(unit.ServiceUnitName, unit.ServiceUnitID));
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new CssdSterileItemsReceivedQuery("a");
            var fromunit = new ServiceUnitQuery("b");
            var usr = new AppUserQuery("c");
            var fromroom = new ServiceRoomQuery("d");

            query.Select
                (
                    query.ReceivedNo,
                    query.ReceivedDate,
                    query.ReceivedTime,
                    fromunit.ServiceUnitName.As("FromServiceUnitName"),
                    fromroom.RoomName.As("FromRoomName"),
                    query.SenderBy,
                    usr.UserName.As("ReceivedByUserName"),
                    query.IsApproved,
                    query.IsVoid,
                    "<'SterileItemsReceivedDetail.aspx?md=view&id='+a.ReceivedNo AS RUrl>"
                );

            query.InnerJoin(fromunit).On(fromunit.ServiceUnitID == query.FromServiceUnitID);
            query.InnerJoin(usr).On(usr.UserID == query.ReceivedByUserID);
            query.LeftJoin(fromroom).On(fromroom.RoomID == query.FromRoomID);

            if (!string.IsNullOrEmpty(txtReceivedNo.Text))
            {
                if (cboFilterReceivedNo.SelectedIndex == 1)
                    query.Where(query.ReceivedNo == txtReceivedNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtReceivedNo.Text);
                    query.Where(query.ReceivedNo.Like(searchTextContain));
                }
            }
            if (!txtReceivedDate.IsEmpty)
                query.Where(query.ReceivedDate == txtReceivedDate.SelectedDate);
            if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                query.Where(query.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);

            query.OrderBy(query.ReceivedDate.Descending, query.ReceivedNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
