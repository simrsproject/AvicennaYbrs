using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Tariff
{
    public partial class ItemTariffRequestProcessSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ItemTariffRequestProcess;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ItemTariffRequestProcessQuery("a");
            var tariffType = new AppStandardReferenceItemQuery("b");
            var tariffType2 = new AppStandardReferenceItemQuery("c");
            var itemType = new AppStandardReferenceItemQuery("d");
            var igroup = new ItemGroupQuery("e");
            query.InnerJoin(tariffType).On(query.FromSRTariffType == tariffType.ItemID &
                                           tariffType.StandardReferenceID ==
                                           AppEnum.StandardReference.TariffType.ToString());
            query.InnerJoin(tariffType2).On(query.ToSRTariffType == tariffType2.ItemID &
                                            tariffType2.StandardReferenceID ==
                                            AppEnum.StandardReference.TariffType.ToString());
            query.InnerJoin(itemType).On(query.SRItemType == itemType.ItemID &
                                         itemType.StandardReferenceID ==
                                         AppEnum.StandardReference.ItemType.ToString());
            query.LeftJoin(igroup).On(igroup.ItemGroupID == query.ItemGroupID);

            query.Select(query.SelectAllExcept(), itemType.ItemName.As("ItemTypeName"),
                tariffType.ItemName.As("FromTariffTypeName"), tariffType2.ItemName.As("ToTariffTypeName"), igroup.ItemGroupName);
            query.Where(query.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical));
            
            query.Where(query.IsApproved == chkIsApproved.Checked);

            if (!string.IsNullOrEmpty(txtTariffRequestNo.Text))
            {
                if (cboFilterTariffRequestNo.SelectedIndex == 1)
                    query.Where(query.TariffRequestNo == txtTariffRequestNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTariffRequestNo.Text);
                    query.Where(query.TariffRequestNo.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSRTariffType.SelectedValue))
                query.Where(query.ToSRTariffType == cboSRTariffType.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                query.Where(query.SRItemType == cboSRItemType.SelectedValue);
            if (!txtStartingDate.IsEmpty)
                query.Where(query.StartingDate == txtStartingDate.SelectedDate);

            query.OrderBy(query.TariffRequestNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}