using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ReferralSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.Referral;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ReferralQuery("a");
            var asriq = new AppStandardReferenceItemQuery("b");
            query.InnerJoin(asriq).On(query.SRReferralGroup == asriq.ItemID);
            query.Where(asriq.StandardReferenceID == "ReferralGroup");

            query.Select
                (
                    query.ReferralID,
                    query.ReferralName,
                    query.DepartmentName,
                    asriq.ItemName.As("SRReferralGroup"),
                    query.StreetName,
                    query.City,
                    query.IsRefferalFrom,
                    query.IsRefferalTo,
                    query.IsActive
                );

            if (!string.IsNullOrEmpty(txtReferralID.Text))
            {
                if (cboFilterReferralID.SelectedIndex == 1)
                    query.Where(query.ReferralID == txtReferralID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtReferralID.Text);
                    query.Where(query.ReferralID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtReferralName.Text))
            {
                if (cboFilterReferralName.SelectedIndex == 1)
                    query.Where(query.ReferralName == txtReferralName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtReferralName.Text);
                    query.Where(query.ReferralName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSRReferralGroup.SelectedValue))
                query.Where(query.SRReferralGroup == cboSRReferralGroup.SelectedValue);

            query.OrderBy(query.ReferralID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }

        protected void cboSRReferralGroup_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }

        protected void cboSRReferralGroup_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested((RadComboBox)o, "ReferralGroup", e.Text);
        }
    }
}
