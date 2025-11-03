using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.BloodBank.Stock
{
    public partial class BloodExterminationSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.BloodStockExtermination;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new BloodExterminationQuery("a");
            var bs = new AppStandardReferenceItemQuery("b");
            var usr = new AppUserQuery("c");

            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    bs.ItemName.As("ReasonsForExtermination"),
                    query.Weight,
                    usr.UserName.As("BloodBankOfficer"),
                    query.IncineratorOperator,
                    query.KnownBy,
                    query.Notes,
                    query.IsApproved,
                    query.IsVoid,
                    "<'BloodExterminationDetail.aspx?md=view&id='+a.TransactionNo AS RUrl>"
                );

            query.InnerJoin(bs).On(bs.StandardReferenceID == AppEnum.StandardReference.ReasonsForExtermination && bs.ItemID == query.SRReasonsForExtermination);
            query.InnerJoin(usr).On(usr.UserID == query.BloodBankOfficerByUserID);
            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            {
                if (cboFilterTransactionNo.SelectedIndex == 1)
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtTransactionNo.Text);
                    query.Where(query.TransactionNo.Like(searchText));
                }
            }
            if (!txtTransactionDate.IsEmpty)
                query.Where(query.TransactionDate == txtTransactionDate.SelectedDate);

            query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
