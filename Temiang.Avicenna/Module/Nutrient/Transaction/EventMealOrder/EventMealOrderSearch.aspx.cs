using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class EventMealOrderSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.EventMealOrder;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new EventMealOrderQuery("a");
            query.Select
                (
                    query.OrderNo,
                    query.OrderDate,
                    query.EventDate,
                    query.EventTime,
                    query.EventName,
                    query.Pic,
                    query.NoOfParticipant,
                    query.IsApproved,
                    query.IsVoid
                );
            query.OrderBy(query.OrderNo.Descending);

            if (!string.IsNullOrEmpty(txtOrderNo.Text))
            {
                if (cboFilterOrderNo.SelectedIndex == 1)
                    query.Where(query.OrderNo == txtOrderNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtOrderNo.Text);
                    query.Where(query.OrderNo.Like(searchTextContain));
                }
            }
            if (!txtOrderDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.OrderDate == txtOrderDate.SelectedDate);
            }
            if (!string.IsNullOrEmpty(txtEventName.Text))
            {
                if (cboFilterEventName.SelectedIndex == 1)
                    query.Where(query.EventName == txtEventName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEventName.Text);
                    query.Where(query.EventName.Like(searchTextContain));
                }
            }
            if (!txtEventDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.EventDate == txtEventDate.SelectedDate);
            }
            
            query.OrderBy(query.OrderNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
