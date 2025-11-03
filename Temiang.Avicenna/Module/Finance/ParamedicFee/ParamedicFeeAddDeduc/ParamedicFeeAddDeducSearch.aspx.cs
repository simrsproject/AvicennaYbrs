using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee
{
    public partial class ParamedicFeeAddDeducSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ParamedicFeeAddDeduc;
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ParamedicFeeAddDeducQuery("a");
            var parQ = new ParamedicQuery("b");
            var stdQ = new AppStandardReferenceItemQuery("c");

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
            if (!txtTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.TransactionDate == txtTransactionDate.SelectedDate);
            }
            if (!string.IsNullOrEmpty(txtParamedicName.Text))
            {
                if (cboFilterParamedicName.SelectedIndex == 1)
                    query.Where(parQ.ParamedicName == txtParamedicName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtParamedicName.Text);
                    query.Where(parQ.ParamedicName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboFilterStatus.SelectedValue))
            {
                if (cboFilterStatus.SelectedValue == "2")
                    query.Where(query.VerificationNo != "" );
                if (cboFilterStatus.SelectedValue == "3")
                    query.Where(query.VerificationNo.IsNull());
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtParamedicName.Text);
                    query.Where(parQ.ParamedicName.Like(searchTextContain));
                }
            }

            query.InnerJoin(parQ).On(query.ParamedicID == parQ.ParamedicID);
            query.InnerJoin(stdQ).On(query.SRParamedicFeeAdjustType == stdQ.ItemID &&
                                             stdQ.StandardReferenceID == "ParamedicFeeAdjustType");
            query.Select(
                query.TransactionNo,
                query.TransactionDate,
                parQ.ParamedicName,
                stdQ.ItemName,
                query.Amount,
                query.Notes,
                query.IsIncludeInTaxCalc,
                query.IsApproved
                );

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
