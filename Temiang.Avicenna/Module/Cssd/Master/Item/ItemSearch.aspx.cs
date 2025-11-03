using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Cssd.Master
{
    public partial class ItemSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.CssdItem;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ItemQuery("a");
            var qs = new VwItemProductMedicNonMedicQuery("b");
            var qgroup = new ItemGroupQuery("c");
            var qstd = new AppStandardReferenceItemQuery("d");

            query.InnerJoin(qs).On(query.ItemID == qs.ItemID);
            query.LeftJoin(qgroup).On(query.ItemGroupID == qgroup.ItemGroupID);
            query.LeftJoin(qstd).On(qstd.StandardReferenceID == AppEnum.StandardReference.CssdItemGroup &&
                                    query.SRCssdItemGroup == qstd.ItemID);
            query.Where(query.IsNeedToBeSterilized == true);

            query.Select
                (
                    query.ItemID,
                    qgroup.ItemGroupName,
                    query.ItemName,
                    qs.SRItemUnit,
                    qstd.ItemName.As("CssdItemGroup"),
                    query.CssdPackagingCostAmount.Coalesce("0"),
                    query.IsItemProduction,
                    query.IsActive
                );
            
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
            if (chkIsItemProduction.Checked)
                query.Where(query.IsItemProduction == true);
            else
                query.Where(query.IsItemProduction == false);

            query.OrderBy(query.ItemID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
