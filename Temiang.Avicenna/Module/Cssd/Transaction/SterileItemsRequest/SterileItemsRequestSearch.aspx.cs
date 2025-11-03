using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class SterileItemsRequestSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CssdSterileItemsRequest;

            if (!IsPostBack)
            {
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new CssdSterileItemsRequestQuery("a");
            var fromunit = new ServiceUnitQuery("b");
            var fromroom = new ServiceRoomQuery("c");

            query.Select
                (
                    query.RequestNo,
                    query.RequestDate,
                    fromunit.ServiceUnitName.As("FromServiceUnitName"),
                    fromroom.RoomName.As("FromRoomName"),
                    query.SenderBy,
                    query.IsApproved,
                    query.IsVoid,
                    "<'SterileItemsRequestDetail.aspx?md=view&id='+a.RequestNo AS RUrl>"
                );

            query.InnerJoin(fromunit).On(fromunit.ServiceUnitID == query.FromServiceUnitID);
            query.LeftJoin(fromroom).On(fromroom.RoomID == query.FromRoomID);

            if (!string.IsNullOrEmpty(txtRequestNo.Text))
            {
                if (cboFilterRequestNo.SelectedIndex == 1)
                    query.Where(query.RequestNo == txtRequestNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtRequestNo.Text);
                    query.Where(query.RequestNo.Like(searchTextContain));
                }
            }
            if (!txtRequestDate.IsEmpty)
                query.Where(query.RequestDate == txtRequestDate.SelectedDate);
            
            query.OrderBy(query.RequestDate.Descending, query.RequestNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}