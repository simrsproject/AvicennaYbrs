using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class SnackOrderSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.SnackOrder;

            if (!IsPostBack)
                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, false);
        }

        public override bool OnButtonOkClicked()
        {
            var query = new SnackOrderQuery("a");
            var unit = new ServiceUnitQuery("b");
            var qusr = new AppUserServiceUnitQuery("u");
            query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
            query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
            query.Select
                (
                    query.SnackOrderNo,
                    query.SnackOrderDate,
                    query.SnackOrderForDate,
                    unit.ServiceUnitName,
                    query.IsApproved,
                    query.IsVoid
                );
            query.Where(qusr.UserID == AppSession.UserLogin.UserID);
            
            if (!string.IsNullOrEmpty(txtSnackOrderNo.Text))
            {
                if (cboFilterSnackOrderNo.SelectedIndex == 1)
                    query.Where(query.SnackOrderNo == txtSnackOrderNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtSnackOrderNo.Text);
                    query.Where(query.SnackOrderNo.Like(searchTextContain));
                }
            }
            if (!txtSnackOrderDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.SnackOrderDate == txtSnackOrderDate.SelectedDate);
            }
            if (!txtSnackOrderForDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.SnackOrderForDate == txtSnackOrderForDate.SelectedDate);
            }
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
            }

            query.OrderBy(query.SnackOrderNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
