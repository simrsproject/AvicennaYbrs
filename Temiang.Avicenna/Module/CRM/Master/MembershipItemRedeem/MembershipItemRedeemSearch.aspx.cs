using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.CRM.Master
{
    public partial class MembershipItemRedeemSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.MembershipItemRedeem;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRItemReedemGroup, AppEnum.StandardReference.ItemReedemGroup);
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new MembershipItemRedeemQuery("a");
            var item = new AppStandardReferenceItemQuery("b");
            query.InnerJoin(item).On(item.StandardReferenceID == "ItemReedemGroup" && item.ItemID == query.SRItemReedemGroup);
            query.Select
                (
                    query.ItemReedemID,
                    query.ItemReedemName,
                    item.ItemName.As("ItemReedemGroup"),
                    query.PointsUsed,
                    query.IsActive
                );
            
            if (!string.IsNullOrEmpty(txtItemReedemID.Text))
            {
                if (cboFilterItemReedemID.SelectedIndex == 1)
                    query.Where(query.ItemReedemID == txtItemReedemID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtItemReedemID.Text);
                    query.Where(query.ItemReedemID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtItemReedemName.Text))
            {
                if (cboFilterItemReedemName.SelectedIndex == 1)
                    query.Where(query.ItemReedemName == txtItemReedemName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtItemReedemName.Text);
                    query.Where(query.ItemReedemName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSRItemReedemGroup.SelectedValue))
                query.Where(query.SRItemReedemGroup == cboSRItemReedemGroup.SelectedValue);

            query.OrderBy(query.ItemReedemID.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}