using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ZatActiveSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.ZatActive;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ZatActiveQuery("a");
            var stdi = new AppStandardReferenceItemQuery("stdi");
            query.LeftJoin(stdi).On(query.SRZatActiveGroup == stdi.ItemID & stdi.StandardReferenceID == "ZatActiveGroup");
            query.Select(query, stdi.ItemName.As("ZatActiveGroupName"));

            if (!string.IsNullOrEmpty(txtZatActiveName.Text))
            {
                if (cboFilterZatActiveName.SelectedIndex == 1)
                    query.Where(query.ZatActiveName == txtZatActiveName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtZatActiveName.Text);
                    query.Where(query.ZatActiveName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.ZatActiveID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
