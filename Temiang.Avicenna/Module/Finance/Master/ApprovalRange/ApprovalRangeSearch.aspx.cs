using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ApprovalRangeSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.ApprovalRange;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ApprovalRangeQuery("a");
            var asriq = new AppStandardReferenceItemQuery("b");
            query.InnerJoin(asriq).On(asriq.StandardReferenceID == "ItemType" && query.SRItemType == asriq.ItemID);

            query.Select
                (
                    query.ApprovalRangeID,
                    query.AmountFrom,
                    query.ApprovalLevelFinal,
                    asriq.ItemName.As("ItemTypeName")
                );
            query.OrderBy(query.SRItemType.Ascending, query.AmountFrom.Ascending);


            if (!string.IsNullOrEmpty(txtApprovalRangeID.Text))
            {
                if (cboFilterApprovalRangeID.SelectedIndex == 1)
                    query.Where(query.ApprovalRangeID == txtApprovalRangeID.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtApprovalRangeID.Text);
                    query.Where(query.ApprovalRangeID.Like(searchText));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
