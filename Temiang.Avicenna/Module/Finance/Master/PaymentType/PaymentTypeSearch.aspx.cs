using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class PaymentTypeSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.PAYMENTTYPE;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new PaymentTypeQuery("a");
            var coaQ = new ChartOfAccountsQuery("b");
            var slQ = new SubLedgersQuery("c");
            query.LeftJoin(coaQ).On(query.ChartOfAccountID == coaQ.ChartOfAccountId);
            query.LeftJoin(slQ).On(query.SubledgerID == slQ.SubLedgerId);
            query.Select
                (
                query.SRPaymentTypeID,
                query.PaymentTypeName,
                @"<
                            RTRIM(b.ChartOfAccountCode) + ' - ' + RTRIM(b.ChartOfAccountName) AS ChartOfAccountName
                        >",
                @"<
                            RTRIM(c.SubLedgerName) + ' - ' + RTRIM(c.Description) AS SubLedgerName
                        >",
                query.IsCashierFrontOffice,
                query.IsArPayment,
                query.IsApPayment,
                query.IsFeePayment,
                query.IsAssetAuctionPayment
                );

            if (!string.IsNullOrEmpty(txtSRPaymentTypeID.Text))
            {
                if (cboFilterSRPaymentTypeID.SelectedIndex == 1)
                    query.Where(query.SRPaymentTypeID == txtSRPaymentTypeID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtSRPaymentTypeID.Text);
                    query.Where(query.SRPaymentTypeID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtPaymentTypeName.Text))
            {
                if (cboFilterPaymentTypeName.SelectedIndex == 1)
                    query.Where(query.PaymentTypeName == txtPaymentTypeName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPaymentTypeName.Text);
                    query.Where(query.PaymentTypeName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.SRPaymentTypeID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
