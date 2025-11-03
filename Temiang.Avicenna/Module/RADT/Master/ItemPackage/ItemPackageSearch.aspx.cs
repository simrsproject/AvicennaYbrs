using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemPackageSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.PackageItem;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool  OnButtonOkClicked()
        {
            var query = new ItemQuery("a");
            if (!string.IsNullOrEmpty(txtItemID.Text))
            {
                if (cboFilterItemID.SelectedIndex == 1)
                    query.Where(query.ItemID == txtItemID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtItemID.Text);
                    query.Where(query.ItemID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtItemName.Text))
            {
                if (cboFilterItemName.SelectedIndex == 1)
                    query.Where(query.ItemName == txtItemName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtItemName.Text);
                    query.Where(query.ItemName.Like(searchTextContain));
                }
            }
            query.Where(query.SRItemType == BusinessObject.Reference.ItemType.Package);

            var grp = new ItemGroupQuery("c");
            query.LeftJoin(grp).On(query.ItemGroupID == grp.ItemGroupID);

            query.Select(query.ItemID,
                            grp.ItemGroupName,
                            query.ItemName,
                            query.IsActive,
                            query.Notes);
            query.OrderBy(query.ItemID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
