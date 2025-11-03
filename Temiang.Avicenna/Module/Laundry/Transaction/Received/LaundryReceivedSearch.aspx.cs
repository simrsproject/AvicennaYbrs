using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaundryReceivedSearch : BasePageDialog
    {
        private string getPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = getPageID == "i" ? AppConstant.Program.LaundryReceivedInfectious : AppConstant.Program.LaundryReceived;

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
            var query = new LaundryReceivedQuery("a");
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
                    query.IsVoid
                );

            query.InnerJoin(fromunit).On(fromunit.ServiceUnitID == query.FromServiceUnitID);
            query.InnerJoin(usr).On(usr.UserID == query.ReceivedByUserID);
            query.LeftJoin(fromroom).On(fromroom.RoomID == query.FromRoomID);

            if (getPageID == "")
            {
                query.Select(@"<'LaundryReceivedDetail.aspx?md=view&id='+a.ReceivedNo+'&type=' AS RUrl>");
                query.Where(query.IsInfectious == false);
            }
            else if (getPageID == "n")
            {
                query.Select(@"<'LaundryReceivedDetail.aspx?md=view&id='+a.ReceivedNo+'&type=n' AS RUrl>");
                query.Where(query.IsInfectious == false);
            }
            else
            {
                query.Select(@"<'LaundryReceivedDetail.aspx?md=view&id='+a.ReceivedNo+'&type=i' AS RUrl>");
                query.Where(query.IsInfectious == true);
            }

            query.OrderBy(query.ReceivedDate.Descending, query.ReceivedNo.Descending);

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

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
