using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Tariff
{
    public partial class ItemProductTariffRequestSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ITEM_PRODUCT_TARIFF_REQUEST;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ItemTariffRequestQuery("a");
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

            var tariffType = new AppStandardReferenceItemQuery("b");
            var itemType = new AppStandardReferenceItemQuery("d");
            var classQuery = new ClassQuery("c");
            query.InnerJoin(tariffType).On(
                query.SRTariffType == tariffType.ItemID &
                tariffType.StandardReferenceID == AppEnum.StandardReference.TariffType.ToString());
            query.InnerJoin(itemType).On(
                query.SRItemType == itemType.ItemID &
                itemType.StandardReferenceID == AppEnum.StandardReference.ItemType.ToString());

            query.InnerJoin(classQuery).On(query.ClassID == classQuery.ClassID);
            query.Select(query.SelectAllExcept(), itemType.ItemName.As("ItemTypeName"),
                         tariffType.ItemName.As("TariffTypeName"), classQuery.ClassName);
            query.Where(query.IsApproved == chkIsApproved.Checked);
            query.Where
                (
                    query.Or
                    (
                        query.SRItemType == BusinessObject.Reference.ItemType.Medical,
                        query.SRItemType == BusinessObject.Reference.ItemType.NonMedical
                    )
                );
            query.OrderBy(query.TariffRequestDate.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
