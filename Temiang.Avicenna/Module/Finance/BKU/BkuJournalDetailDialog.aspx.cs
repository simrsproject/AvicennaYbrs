using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance
{
    public partial class BkuJournalDetailDialog : BasePageDialog
    {
        private int BkuJournalID {
            get {
                return System.Convert.ToInt32(Request.QueryString["bkuid"]);
            }
        }

        private void LoadHeader() {
            var bku = new BkuJournalTransactions();
            if(bku.LoadByPrimaryKey(BkuJournalID)){
                var jt = new JournalTransactions();
                if (jt.LoadByPrimaryKey(bku.JournalIdToCopy ?? 0)) {
                    txtTransactionNumber.Text = string.Format("{0}-{1}", jt.JournalCode, jt.TransactionNumber);
                    txtTransactionDate.SelectedDate = jt.TransactionDate;
                    txtRefferenceNumber.Text = jt.RefferenceNumber;
                    txtDescription.Text = jt.Description;

                    double totalDebit, totalCredit;
                    BkuJournalTransactionDetails.GetTotal(bku.BkuJournalId ?? 0, out totalDebit, out totalCredit);
                    txtTotalAmountCredit.Value = totalCredit;
                    txtTotalAmountDebit.Value = totalDebit;
                    txtSelisih.Value = Math.Abs(totalCredit - totalDebit);


                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonOk.Visible = false;
                ButtonCancel.Text = "Close";

                LoadHeader();
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }

        protected void grdVoucherEntryItem_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var bkud = new BkuJournalTransactionDetailsQuery("bkud");
            var coa = new ChartOfAccountsQuery("coa");
            var sl = new SubLedgersQuery("sl");
            bkud.InnerJoin(coa).On(bkud.ChartOfAccountId == coa.ChartOfAccountId)
                .LeftJoin(sl).On(coa.SubLedgerId == sl.GroupId)
                .Where(bkud.BkuJournalId == BkuJournalID)
                .Select(
                    bkud.BkuDetailId, bkud.BkuJournalId, bkud.ChartOfAccountId, coa.ChartOfAccountCode, coa.ChartOfAccountName,
                    bkud.SubLedgerId, sl.Description.As("SubLedgerName"), bkud.Debit, bkud.Credit, bkud.Description
                );
            grdVoucherEntryItem.DataSource = bkud.LoadDataTable();
        }

        protected void gridCoaSetting_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var jmsg = new BkuJournalMessagesQuery("jmsg");
            var jtd = new JournalTransactionDetailsQuery("jtd");
            var coa = new ChartOfAccountsQuery("coa");
            var coa2 = new ChartOfAccountsQuery("coa2");

            jmsg.InnerJoin(jtd).On(jmsg.DetailJournalId == jtd.DetailId)
                .LeftJoin(coa).On(jtd.ChartOfAccountId == coa.ChartOfAccountId)
                .LeftJoin(coa2).On(coa.BkuAccountID == coa2.ChartOfAccountId)
                .Where(jmsg.BkuJournalId == BkuJournalID)
                .Select(
                    jtd.ChartOfAccountId, coa.ChartOfAccountCode, coa.ChartOfAccountName,
                    coa2.ChartOfAccountCode.As("ChartOfAccountCodeBKU"), coa2.ChartOfAccountName.As("ChartOfAccountNameBKU"), jmsg.Message
                );

            var dtb = jmsg.LoadDataTable();

            //jtd = new JournalTransactionDetailsQuery("jtd");
            //var bjt = new BkuJournalTransactionsQuery("bjt");
            //coa = new ChartOfAccountsQuery("coa");
            //coa2 = new ChartOfAccountsQuery("coa2");

            //jtd.InnerJoin(bjt).On(jtd.JournalId == bjt.JournalIdToCopy)
            //    .LeftJoin(coa).On(jtd.ChartOfAccountId == coa.ChartOfAccountId)
            //    .LeftJoin(coa2).On(coa.BkuAccountID == coa2.ChartOfAccountId)
            //    .Where(bjt.BkuJournalId == BkuJournalID)
            //    .Select(
            //        jtd.ChartOfAccountId, coa.ChartOfAccountCode, coa.ChartOfAccountName,
            //        coa2.ChartOfAccountCode.As("ChartOfAccountCodeBKU"), coa2.ChartOfAccountName.As("ChartOfAccountNameBKU"), "<'' Message>"
            //    );
            //var dtb2 = jtd.LoadDataTable();

            //foreach (DataRow dr in dtb2.Rows) {
            //    var rColl = dtb.AsEnumerable().Where(r => r["ChartOfAccountId"].ToString() == dr["ChartOfAccountId"].ToString());
            //    foreach (var r in rColl) {
            //        r["ChartOfAccountCodeBKU"] = dr["ChartOfAccountCodeBKU"];
            //        r["ChartOfAccountNameBKU"] = dr["ChartOfAccountNameBKU"];
            //    }
            //}

            //var mColl = dtb2.AsEnumerable().Where(r => !(dtb.AsEnumerable().Select(x => x["ChartOfAccountId"].ToString())).Contains(r["ChartOfAccountId"].ToString()));
            //foreach (DataRow r in mColl) {
            //    var nr = dtb.NewRow();
            //    nr["ChartOfAccountId"] = r["ChartOfAccountId"];
            //    nr["ChartOfAccountCode"] = r["ChartOfAccountCode"];
            //    nr["ChartOfAccountName"] = r["ChartOfAccountName"];
            //    nr["ChartOfAccountCodeBKU"] = r["ChartOfAccountCodeBKU"];
            //    nr["ChartOfAccountNameBKU"] = r["ChartOfAccountNameBKU"];
            //    dtb.Rows.Add(nr);
            //}
            //dtb.AcceptChanges();

            gridCoaSetting.DataSource = dtb;

        }

        protected void btnRecalculate_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            BkuJournalTransactions.Rejournal(BkuJournalID, AppSession.UserLogin.UserID);
            LoadHeader();
            grdVoucherEntryItem.Rebind();
            gridCoaSetting.Rebind();
        }
    }
}
