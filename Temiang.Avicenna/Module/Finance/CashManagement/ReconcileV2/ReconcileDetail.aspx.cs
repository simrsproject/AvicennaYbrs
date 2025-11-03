using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Finance.CashManagement.ReconcileV2
{
    public partial class ReconcileDetail : BasePageDetail
    {
        public class SelectedData { 
            public int ID { get; set; }
            public decimal Debit { get; set; }
            public decimal Credit { get; set; }
        }
        public string BankID
        {
            get
            {
                return Request.QueryString["bankid"];
            }
        }

        public string Description
        {
            get
            {
                return Request.QueryString["Description"];
            }
        }
        private double BankBalanceTemp
        {
            set
            {
                Session["BankBalanceTemp"] = value;
            }
            get
            {
                return Session["BankBalanceTemp"] == null ? 0 : (double)Session["BankBalanceTemp"];
            }
        }

        private DateTime BalanceDateTemp
        {
            set
            {
                Session["BalanceDateTemp"] = value;
            }
            get
            {
                return Session["BalanceDateTemp"] == null ? DateTime.Now : (DateTime)Session["BalanceDateTemp"];
            }
        }

        private bool ChkAllTransTmp
        {
            set
            {
                Session["ChkAllTransTmp"] = value;
            }
            get
            {
                return Session["ChkAllTransTmp"] == null ? false : (bool)Session["ChkAllTransTmp"];
            }
        }

        private bool ChkCashTrans
        {
            set
            {
                Session["ChkCashTrans"] = value;
            }
            get
            {
                return Session["ChkCashTrans"] == null ? false : (bool)Session["ChkCashTrans"];
            }
        }

        private bool ChkBankInq
        {
            set
            {
                Session["ChkBankInq"] = value;
            }
            get
            {
                return Session["ChkBankInq"] == null ? false : (bool)Session["ChkBankInq"];
            }
        }

        private List<SelectedData> SelectedCT
        {
            set
            {
                Session["SelectedCtId"] = value;
            }
            get
            {
                if (Session["SelectedCtId"] == null) Session["SelectedCtId"] = new List<SelectedData>();
                return (List<SelectedData>)Session["SelectedCtId"];
            }
        }
        private List<SelectedData> SelectedIq
        {
            set
            {
                Session["SelectedIqId"] = value;
            }
            get
            {
                if (Session["SelectedIqId"] == null) Session["SelectedIqId"] = new List<SelectedData>();
                return (List<SelectedData>)Session["SelectedIqId"];
            }
        }

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            ToolBarMenuSearch.Enabled = false;
            //UrlPageSearch = "VoucherEntrySearch.aspx";

            ProgramID = AppConstant.Program.RECONCILE;
            UrlPageList = "ReconcileList.aspx";
            UrlPageSearch = "ReconcileSearch.aspx";
    
            if (!IsPostBack)
            {
                this.ToolBarMenuMoveNext.Enabled = false;
                this.ToolBarMenuMovePrev.Enabled = false;

                txtBankBalance.Value = BankBalanceTemp;
                txtDate.SelectedDate = BalanceDateTemp;
                chkAllTransaction.Checked = ChkAllTransTmp;
                chkCTransaction.Checked = ChkCashTrans;
                chkBInquiry.Checked = ChkBankInq;
                txtDescription.Text = Description;


                StandardReference.InitializeIncludeSpace(cboTransCode, AppEnum.StandardReference.CashTransactionCode);

                SelectedCT = null;
                SelectedIq = null;
            }
        }

        protected void grdTransaction_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            int iRowCount; decimal Balance, ReconciledBalance;
            DateTime date = txtDate.SelectedDate.HasValue ? 
                txtDate.SelectedDate.Value : DateTime.Now;
            var description = txtDescription.Text;            
            var cbTrans = (new CashTransactionBalanceCollection()).GetCashTransactionByBalanceByPage(
                BankID, description, date, !chkAllTransaction.Checked | !chkCTransaction.Checked,
                ((grdTransaction.CurrentPageIndex * grdTransaction.PageSize) + 1),
                ((grdTransaction.CurrentPageIndex + 1) * grdTransaction.PageSize), 
                out iRowCount, out Balance, out ReconciledBalance);

            txtBalance.Value = System.Convert.ToDouble(ReconciledBalance);
            txtCurrentBalance.Value = System.Convert.ToDouble(Balance);
            CalculateDifferent();

            grdTransaction.VirtualItemCount = iRowCount;
            grdTransaction.DataSource = cbTrans;
        }

        protected void grdTransaction_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                var chk = (dataItem.FindControl("detailChkbox") as CheckBox);
                if (!(((e.Item as GridItem).DataItem as DataRowView)["ReconcileID"] is DBNull))
                {
                    chk.Enabled = false;
                    chk.Visible = false;
                }

                var ct = SelectedCT.Where(x => x.ID == System.Convert.ToInt32(dataItem.GetDataKeyValue("TxnBalanceId"))).FirstOrDefault();
                if (ct != null)
                {
                    chk.Checked = true;
                }
            }
        }

        protected void txtBankBalance_TextChanged(object o, EventArgs e) {
            BankBalanceTemp = txtBankBalance.Value ?? 0;
            CalculateDifferent();
        }

        private void CalculateDifferent() {
            txtDifferent.Value = (txtBankBalance.Value ?? 0) - (txtBalance.Value ?? 0);
        }

        private void UpdateBankBalance() {
            if (chkBankBalanceFromInquiry.Checked) {
                var bid = new BankInquiryDetail();
                txtBankBalance.Value = System.Convert.ToDouble(bid.GetBalanceBankIdByDate(BankID, txtDate.SelectedDate.Value));
                txtBankBalance_TextChanged(txtBankBalance, new EventArgs());
            }
        }

        private void CalculateSelectedReconcile() {
            txtSelectedCashTrans.Value = System.Convert.ToDouble(SelectedCT.Sum(x => x.Debit - x.Credit));
            txtSelectedInquiry.Value = System.Convert.ToDouble(SelectedIq.Sum(x => x.Credit - x.Debit));
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var cbh = ((CheckBox)sender);
            foreach (CheckBox chkBox in ((sender as CheckBox).NamingContainer as GridHeaderItem).OwnerTableView.Items.Cast<GridDataItem>()
                .Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                if (chkBox.Checked != cbh.Checked) {
                    chkBox.Checked = ((CheckBox)sender).Checked;
                    detailChkbox_CheckedChanged(chkBox, new EventArgs());
                }
            }
        }

        protected void detailChkbox_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (sender as CheckBox);
            var gdi = (chk.NamingContainer as GridDataItem);
            var grd = gdi.OwnerTableView.OwnerGrid;
            switch (grd.ID) {
                case "grdTransaction":
                    {
                        var id = System.Convert.ToInt32(gdi.GetDataKeyValue("TxnBalanceId"));
                        if (chk.Checked) {
                            var sd = new SelectedData()
                            {
                                ID = id,
                                Debit = System.Convert.ToDecimal(gdi["DebitAmount"].Text),
                                Credit = System.Convert.ToDecimal(gdi["CreditAmount"].Text)
                            };
                            SelectedCT.Add(sd);
                        }
                        else {
                            var ct = SelectedCT.Where(x => x.ID == id).FirstOrDefault();
                            if (ct != null) {
                                SelectedCT.Remove(ct);
                            }
                        }
                        break;
                    }
                case "grdInquiryTransaction": 
                    {
                        var id = System.Convert.ToInt32(gdi.GetDataKeyValue("TransactionID"));
                        if (chk.Checked)
                        {
                            var sd = new SelectedData()
                            {
                                ID = id,
                                Debit = System.Convert.ToDecimal(gdi["Debit"].Text),
                                Credit = System.Convert.ToDecimal(gdi["Credit"].Text)
                            };
                            SelectedIq.Add(sd);
                        }
                        else
                        {
                            var ct = SelectedIq.Where(x => x.ID == id).FirstOrDefault();
                            if (ct != null)
                            {
                                SelectedIq.Remove(ct);
                            }
                        }
                        break;
                    }
            }

            CalculateSelectedReconcile();
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            //SaveValueToCookie();
            BalanceDateTemp = txtDate.SelectedDate ?? DateTime.Now;
            //ChkAllTransTmp = chkAllTransaction.Checked;
            //ChkCashTrans = chkCTransaction.Checked;
            //ChkBankInq = chkBInquiry.Checked;
            if (chkAllTransaction.Checked == true)
            {
                grdTransaction.Rebind();
                grdInquiryTransaction.Rebind();
            }
            else if (chkCTransaction.Checked == true)
            {
                grdTransaction.Rebind();
            }
            else if (chkBInquiry.Checked == true)
            {
                grdInquiryTransaction.Rebind();
            }
            //grdTransaction.Rebind();
            //grdInquiryTransaction.Rebind();

            UpdateBankBalance();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bank bank = new Bank();
                if (bank.LoadByPrimaryKey(BankID)) {
                    lblBankName.Text = bank.BankName;
                }
            }
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);

            EnableControl();
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            EnableControl();
        }
        #endregion

        private void EnableControl() {
            txtDate.Enabled = true;
            txtBalance.Enabled = true;
            chkBankBalanceFromInquiry.Enabled = true;
            txtDescription.Enabled = true;
            txtDescription.ReadOnly = false;
            txtBankBalance.ReadOnly = chkBankBalanceFromInquiry.Checked;
            chkAllTransaction.Enabled = true;
            chkBInquiry.Enabled = true;
            chkBInquiry.Checked = true;
            chkCTransaction.Enabled = true;
            chkCTransaction.Checked = true;
            cboTransCode.Enabled = true;
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (sourceControl is RadGrid && eventArgument == "rebind")
            {
                var rg = sourceControl as RadGrid;
                if (rg.ID == "grdInquiry") {
                    rg.Rebind();
                }
            }
        }

        protected void grdInquiry_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid rg = sender as RadGrid;
            var biColl = new BankInquiryCollection();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (GridSortExpression ep in rg.MasterTableView.SortExpressions)
            {
                sb.AppendFormat("{0}^{1}", ep.FieldName, ep.SortOrder);
                sb.Append(",");
            }

            rg.VirtualItemCount = biColl.GetTotalCount(BankID);

            biColl.LoadByPaging(BankID, rg.CurrentPageIndex, rg.PageSize, sb.ToString());
            rg.DataSource = biColl;
        }

        protected void grdInquiry_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "RowClick")
            {
                //GridDataItem dataItem = e.Item as GridDataItem;
                //int tid = System.Convert.ToInt32(dataItem.GetDataKeyValue("InquiryID"));
                grdInquiryDetail.Rebind();
            }
        }

        protected void grdInquiryDetail_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid rg = sender as RadGrid;
            if (grdInquiry.SelectedItems.Count > 0)
            {
                int InquiryID = System.Convert.ToInt32((grdInquiry.SelectedItems[0] as GridDataItem).GetDataKeyValue("InquiryID"));

                var bidColl = new BankInquiryDetailCollection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (GridSortExpression ep in rg.MasterTableView.SortExpressions)
                {
                    sb.AppendFormat("{0}^{1}", ep.FieldName, ep.SortOrder);
                    sb.Append(",");
                }

                rg.VirtualItemCount = bidColl.GetCountByInquiryByPaging(InquiryID);

                bidColl.LoadByInquiryByPaging(InquiryID, rg.CurrentPageIndex, rg.PageSize, sb.ToString());
                rg.DataSource = bidColl;
            }
            else {
                rg.DataSource = null;
            }
        }

        protected void chkBankBalanceFromInquiry_CheckedChanged(object sender, EventArgs e)
        {
            EnableControl();
        }

        protected void grdInquiryTransaction_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid rg = sender as RadGrid;
            var bidColl = new BankInquiryDetailCollection();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (GridSortExpression ep in rg.MasterTableView.SortExpressions)
            {
                sb.AppendFormat("{0}^{1}", ep.FieldName, ep.SortOrder);
                sb.Append(",");
            }

            rg.VirtualItemCount = bidColl.GetCountByBankIdByPaging(BankID, txtDescription.Text, txtDate.SelectedDate.Value, !chkAllTransaction.Checked | !chkBInquiry.Checked);

            bidColl.LoadByBankIdByPaging(BankID, txtDescription.Text, txtDate.SelectedDate.Value, !chkAllTransaction.Checked| !chkBInquiry.Checked, rg.CurrentPageIndex, rg.PageSize, sb.ToString());
            rg.DataSource = bidColl;
        }

        protected void grdInquiryTransaction_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                var chk = (dataItem.FindControl("detailChkbox") as CheckBox);
                if (((e.Item as GridItem).DataItem as BankInquiryDetail).ReconcileID.HasValue)
                {
                    chk.Enabled = false;
                    chk.Visible = false;
                }

                var ct = SelectedIq.Where(x => x.ID == System.Convert.ToInt32(dataItem.GetDataKeyValue("TransactionID"))).FirstOrDefault();
                if (ct != null)
                {    
                    chk.Checked = true;
                }
            }
        }

        protected void btnClearSelected_Click(object sender, ImageClickEventArgs e)
        {
            SelectedCT = null;
            SelectedIq = null;
            grdTransaction.Rebind();
            grdInquiryTransaction.Rebind();
            CalculateSelectedReconcile();
        }

        protected void btnSaveReconcile_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Reconcile();
                btnClearSelected_Click(btnClearSelected, e);
            }
            catch (Exception ex) {
                ShowInformationHeader(ex.Message);
            }
        }

        private void Reconcile()
        {
            var selectedBalanceID = SelectedCT.Select(x => x.ID);
            var selectedInquiryTransID = SelectedIq.Select(x => x.ID);
       

            if (!selectedBalanceID.Any())
            {
                throw new Exception("No data to be reconciled!");
            }

            var balColl = new CashTransactionBalanceCollection();
            var cashtransColl = new CashTransactionCollection();
            if (selectedBalanceID.Any())
            {
                var bq = balColl.Query;
                bq.Where(bq.TxnBalanceId.In(selectedBalanceID));
                bq.Load();

                var ct = cashtransColl.Query;
                ct.Where(ct.TransactionId.In(balColl.Select(x => x.TransactionId).Distinct()));
                ct.Load();

                if (balColl.Where(x => !cashtransColl.Select(c => c.TransactionId ?? 0).Contains(x.TransactionId ?? 0)).Any())
                {
                    throw new Exception("Cast Transaction not found");
                }
            }

            var inqColl = new BankInquiryDetailCollection();
            if (selectedInquiryTransID.Any())
            {
                var iq = inqColl.Query;
                iq.Where(iq.TransactionID.In(selectedInquiryTransID));
                iq.Load();
            }

            if (balColl.Where(x => x.ReconcileID.HasValue).Any())
            {
                throw new Exception("Some row in selected balance have been reconciled, reconciliation aborted!");
            }
            if (inqColl.Where(x => x.ReconcileID.HasValue).Any())
            {
                throw new Exception("Some row in selected inquiry have been reconciled, reconciliation aborted!");
            }

            var sumCT = balColl.Sum(x => (x.DebitAmount ?? 0) - (x.CreditAmount ?? 0));
            var sumIq = inqColl.Sum(x => (x.Credit ?? 0) - (x.Debit ?? 0));

            if (sumCT != sumIq) {
                if (sumIq == 0)
                {
                    // tidak perlu validasi supaya bisa rekon manual seperti rekon menu v1
                }
                else {
                    throw new Exception(string.Format("Transaction does not equal between {0} and {1}", sumCT.ToString("N2"), sumIq.ToString("N2")));
                }
            }

            var rc = new BankReconcile();
            rc.AddNew();
            rc.BankID = BankID;
            rc.DebitCashTransaction = balColl.Sum(x => x.DebitAmount ?? 0);
            rc.CreditCashTransaction = balColl.Sum(x => x.CreditAmount ?? 0);
            rc.DebitInquiry = inqColl.Sum(x => x.Debit ?? 0);
            rc.CreditInquiry = inqColl.Sum(x => x.Credit ?? 0);
            rc.CreatedDateTime = DateTime.Now;
            rc.CreatedByUserID = AppSession.UserLogin.UserID;
            rc.LastUpdateDateTime = DateTime.Now;
            rc.LastUpdateByUserID = AppSession.UserLogin.UserID;

            using (var trans = new esTransactionScope())
            {
                rc.Save();

                foreach (var bal in balColl)
                {
                    bal.ReconcileID = rc.ReconcileID;
                }
                balColl.Save();

                foreach (var ct in cashtransColl)
                {
                    ct.IsCleared = true;
                    ct.ClearedBy = AppSession.UserLogin.UserID;
                    ct.ClearedDateTime = DateTime.Now;
                }
                cashtransColl.Save();

                foreach (var inq in inqColl)
                {
                    inq.ReconcileID = rc.ReconcileID;
                }
                inqColl.Save();

                trans.Complete();
            }

            grdTransaction.Rebind();
            grdInquiryTransaction.Rebind();

        }

        protected void btnSaveTransCode_Click(object sender, ImageClickEventArgs e)
        {
            List<int> selectedInquiryDetailID = new List<int>();
            foreach (var gdi in grdInquiryDetail.MasterTableView.Items.Cast<GridDataItem>()
                .Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked))
            {
                selectedInquiryDetailID.Add(System.Convert.ToInt32(gdi.GetDataKeyValue("TransactionID")));
            }
            //foreach (GridDataItem row in grdInquiryDetail.MasterTableView.Items.wher)
            //{ 

            //}

            if (selectedInquiryDetailID.Count == 0) {
                ShowInformationHeader("No data selected");
                return;
            }else {
                var bidColl = new BankInquiryDetailCollection();
                var q = bidColl.Query;
                q.Where(q.TransactionID.In(selectedInquiryDetailID));
                q.Load();

                foreach (var bid in bidColl) {
                    bid.SRCashTransactionCode = cboTransCode.SelectedValue;

                    bid.LastUpdateDateTime = DateTime.Now;
                    bid.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }

                bidColl.Save();

                grdInquiryDetail.Rebind();

                HideInformationHeader();
            }
        }

        protected void grdInquiry_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            BankInquiry bi = new BankInquiry();
            if (bi.LoadByPrimaryKey(System.Convert.ToInt32(item.GetDataKeyValue("InquiryID")))) {
                // validasi
                var bidColl = new BankInquiryDetailCollection();
                if (bidColl.LoadByInquiryID(bi.InquiryID ?? 0)) {
                    var iReconCount = bidColl.ReconciledCount;
                    if (iReconCount > 0) {
                        // can not delete
                        ShowInformationHeader(string.Format("{0} row(s) have been reconciled, inquiry can not be deleted", iReconCount.ToString()));
                        return;
                    }

                    var iRelatedCount = bidColl.RelatedCount;
                    if (iRelatedCount > 0)
                    {
                        // can not delete
                        ShowInformationHeader(string.Format("{0} row(s) have been related to another transaction, inquiry can not be deleted", iRelatedCount.ToString()));
                        return;
                    }

                    bidColl.MarkAllAsDeleted();
                    bi.MarkAsDeleted();
                    using (var trans = new esTransactionScope())
                    {
                        bidColl.Save();
                        bi.Save();

                        trans.Complete();
                    }

                    ((RadGrid)sender).Rebind();
                    grdInquiryDetail.Rebind();
                }
            }
        }
    }
}