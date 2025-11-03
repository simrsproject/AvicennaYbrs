using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class ReturnDistributionSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.LaundryReturnDistribution;

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
            var query = new LaundryReturnDistributionQuery("a");
            var fromUnit = new ServiceUnitQuery("b");
            var usr = new AppUserQuery("c");

            query.Select
                (
                    query.ReturnNo,
                    query.ReturnDate,
                    query.ReturnTime,
                    fromUnit.ServiceUnitName.As("FromServiceUnitName"),
                    usr.UserName.As("HandedByUserName"),
                    query.ReceivedBy,
                    query.IsApproved,
                    query.IsVoid,
                    @"<'ReturnDistributionDetail.aspx?md=view&id='+a.ReturnNo AS RUrl>"
                );

            query.InnerJoin(fromUnit).On(fromUnit.ServiceUnitID == query.FromServiceUnitID);
            query.InnerJoin(usr).On(usr.UserID == query.HandedByUserID);

            query.OrderBy(query.ReturnDate.Descending, query.ReturnNo.Descending);

            if (!string.IsNullOrEmpty(txtReturnNo.Text))
            {
                if (cboFilterReturnNo.SelectedIndex == 1)
                    query.Where(query.ReturnNo == txtReturnNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtReturnNo.Text);
                    query.Where(query.ReturnNo.Like(searchTextContain));
                }
            }
            if (!txtReturnDate.IsEmpty)
                query.Where(query.ReturnDate == txtReturnDate.SelectedDate);
            if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                query.Where(query.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}