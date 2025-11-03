using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.BloodBank.Stock
{
    public partial class StockReceivedSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.BloodStockReceived;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRBloodSource, AppEnum.StandardReference.BloodSource);
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new BloodReceivedQuery("a");
            var bs = new AppStandardReferenceItemQuery("b");

            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    bs.ItemName.As("BloodSource"),
                    query.Notes,
                    query.IsApproved,
                    query.IsVoid,
                    "<'StockReceivedDetail.aspx?md=view&id='+a.TransactionNo AS RUrl>"
                );

            query.InnerJoin(bs).On(bs.ItemID == query.SRBloodSource && bs.StandardReferenceID == AppEnum.StandardReference.BloodSource);

            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            {
                if (cboFilterTransactionNo.SelectedIndex == 1)
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTransactionNo.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }
            }
            if (!txtTransactionDate.IsEmpty)
                query.Where(query.TransactionDate == txtTransactionDate.SelectedDate);
            if (!string.IsNullOrEmpty(cboSRBloodSource.SelectedValue))
                query.Where(query.SRBloodSource == cboSRBloodSource.SelectedValue);

            query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
