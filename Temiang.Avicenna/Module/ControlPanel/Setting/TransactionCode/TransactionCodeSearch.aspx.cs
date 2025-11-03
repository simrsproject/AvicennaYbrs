using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ControlPanel.Setting
{
    public partial class TransactionCodeSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.TransactionCodeNumbering;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new AppAutoNumberTransactionCodeQuery("a");
            var sr = new AppStandardReferenceItemQuery("b");
            query.InnerJoin(sr).On(query.SRTransactionCode == sr.ItemID & sr.StandardReferenceID == "TransactionCode");

            if (!string.IsNullOrEmpty(txtTransactionCode.Text))
            {
                if (cboFilterTransactionCode.SelectedIndex == 1)
                    query.Where(query.SRTransactionCode == txtTransactionCode.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTransactionCode.Text);
                    query.Where(query.SRTransactionCode.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtTransactionName.Text))
            {
                if (cboFilterTransactionCode.SelectedIndex == 1)
                    query.Where(sr.ItemName == txtTransactionName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTransactionName.Text);
                    query.Where(sr.ItemName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset
            return true;
        }
    }
}