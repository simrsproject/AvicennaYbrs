using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceRoomSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.ServiceRoom;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                RestoreValueFromCookie();
                ComboBox.PopulateWithServiceUnit(cboServiceUnitID, false);
            }
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ServiceRoomQuery("a");
            var suq = new ServiceUnitQuery("b");
            AppStandardReferenceItemQuery floor = new AppStandardReferenceItemQuery("c");
            query.InnerJoin(suq).On(query.ServiceUnitID == suq.ServiceUnitID);
            query.LeftJoin(floor).On(query.SRFloor == floor.ItemID & floor.StandardReferenceID == AppEnum.StandardReference.Floor);
            query.Select(query.SelectAllExcept(), suq.ServiceUnitName, floor.ItemName.As("SRFloorName"));

            if (!string.IsNullOrEmpty(txtRoomID.Text))
            {
                if (cboFilterRoomID.SelectedIndex == 1)
                    query.Where(query.RoomID == txtRoomID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtRoomID.Text);
                    query.Where(query.RoomID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtRoomName.Text))
            {
                if (cboFilterRoomName.SelectedIndex == 1)
                    query.Where(query.RoomName == txtRoomName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtRoomName.Text);
                    query.Where(query.RoomName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);

            query.OrderBy(query.RoomID.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
