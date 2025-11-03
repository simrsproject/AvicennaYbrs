using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaundryDistributionSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.LaundryDistribution;

            if (!IsPostBack)
            {
                var unitColl = new ServiceUnitCollection();
                unitColl.Query.Where(unitColl.Query.IsActive == true);
                unitColl.LoadAll();

                cboToServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var unit in unitColl)
                {
                    cboToServiceUnitID.Items.Add(new RadComboBoxItem(unit.ServiceUnitName, unit.ServiceUnitID));
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new LaundryDistributionQuery("a");
            var tounit = new ServiceUnitQuery("b");
            var usr = new AppUserQuery("c");

            query.Select
                (
                    query.DistributionNo,
                    query.DistributionDate,
                    query.DistributionTime,
                    tounit.ServiceUnitName.As("ToServiceUnitName"),
                    usr.UserName.As("HandedByUserName"),
                    query.ReceivedBy,
                    query.IsApproved,
                    query.IsVoid,
                    @"<'LaundryDistributionDetail.aspx?md=view&id='+a.DistributionNo AS RUrl>"
                );

            query.InnerJoin(tounit).On(tounit.ServiceUnitID == query.ToServiceUnitID);
            query.InnerJoin(usr).On(usr.UserID == query.HandedByUserID);

            query.OrderBy(query.DistributionDate.Descending, query.DistributionNo.Descending);

            if (!string.IsNullOrEmpty(txtDistributionNo.Text))
            {
                if (cboFilterDistributionNo.SelectedIndex == 1)
                    query.Where(query.DistributionNo == txtDistributionNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDistributionNo.Text);
                    query.Where(query.DistributionNo.Like(searchTextContain));
                }
            }
            if (!txtDistributionDate.IsEmpty)
                query.Where(query.DistributionDate == txtDistributionDate.SelectedDate);
            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                query.Where(query.ToServiceUnitID == cboToServiceUnitID.SelectedValue);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}