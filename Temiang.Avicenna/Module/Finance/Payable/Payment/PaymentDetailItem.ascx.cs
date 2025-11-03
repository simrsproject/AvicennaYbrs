using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Finance.Payable
{
    public partial class PaymentDetailItem : BaseUserControl
    {
        private object _dataItem;

        private RadComboBox cboSupplierID
        {
            get { return Helper.FindControlRecursive(this.Page, "cboSupplierID") as RadComboBox; }
        }

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (AppSession.Parameter.IsCoaAPNonMedicSeparated)
            {
                trSRItemType.Visible = true;
                cboSRItemType.Visible = true;
                rfvSRItemType.Visible = true;
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var cur = new CurrencyRateCollection();
                cur.Query.Where(cur.Query.IsActive == true);
                cur.Query.Load();

                foreach (var c in cur)
                {
                    cboCurrency.Items.Add(new RadComboBoxItem(c.CurrencyName, c.CurrencyID));
                }

                var coll = (InvoiceSupplierItemCollection)Session["InvoiceSupplierItemsPayment" + Request.UserHostName];
                if (coll.Count == 0) ViewState["SequenceNo"] = "ADD-001";
                else
                {
                    try
                    {
                        var sequenceNo = (coll.Where(c => c.TransactionNo.Substring(0, 4) == "ADD-").OrderByDescending(c => c.TransactionNo).Select(c => c.TransactionNo)).Take(1);
                        int seqNo = int.Parse(sequenceNo.Single().Substring(4, 3)) + 1;
                        ViewState["SequenceNo"] = "ADD-" + string.Format("{0:000}", seqNo);
                    }
                    catch { ViewState["SequenceNo"] = "ADD-001"; }
                }

                txtPaymentAmount.Value = 0;

                if (!AppSession.Parameter.IsCoaAPNonMedicSeparated)
                {
                    var sup = new Supplier();
                    var supID = cboSupplierID.SelectedValue;
                    if (sup.LoadByPrimaryKey(supID))
                    {
                        var coa = new ChartOfAccounts();
                        if (coa.LoadByPrimaryKey(sup.ChartOfAccountIdAP ?? 0))
                        {
                            ChartOfAccountCodeText = string.Format("{0} - {1}", coa.ChartOfAccountCode, coa.ChartOfAccountName);
                        }
                        SubLedgerId = sup.SubledgerIdAP ?? 0;
                    }
                }

                // COA
                //if (!string.IsNullOrEmpty(cboSupplierID.SelectedValue))
                //{
                //    var sup = new Supplier();
                //    if (sup.LoadByPrimaryKey(cboSupplierID.SelectedValue))
                //    {
                //        List<int> coaid = new List<int>();
                //        if ((sup.ChartOfAccountIdAP ?? 0) != 0) {
                //            coaid.Add(sup.ChartOfAccountIdAP.Value);
                //        }
                //        if ((sup.ChartOfAccountIdAPNonMedic ?? 0) != 0)
                //        {
                //            coaid.Add(sup.ChartOfAccountIdAPNonMedic.Value);
                //        }

                //        var query = new ChartOfAccountsQuery();
                //        query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
                //        query.Where(query.IsDetail == 1);
                //        if(coaid.Any()) query.Where(query.ChartOfAccountId.In(coaid));
                //        query.es.Top = 20;
                //        DataTable dtb = query.LoadDataTable();
                //        cboChartOfAccountId.DataSource = dtb;
                //        cboChartOfAccountId.DataBind();

                //        cboChartOfAccountId.SelectedIndex = 0;
                //        RadComboBoxItemsRequestedEventArgs args = new RadComboBoxItemsRequestedEventArgs();
                //        args.Text = sup.SupplierID;
                //        cboSubledgerId_ItemsRequested(cboSubledgerId, args);

                //        var slText = sup.SupplierID + " - " + sup.SupplierName;
                //        var cboItem = cboSubledgerId.Items.FindItemByText(slText);
                //        if (cboItem != null) cboItem.Selected = true;
                //    }
                //}
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtPaymentAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.PaymentAmount));
            cboCurrency.SelectedValue = (String)DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.CurrencyID);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.Notes);
            txtInvoiceReferenceNo.Text = (String)DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.InvoiceReferenceNo);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(cboSupplierID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Supplier required");
                return;
            }

            if (AppSession.Parameter.IsCoaAPNonMedicSeparated)
            {
                if (string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item Type required");
                    return;
                }
            }

            if (ChartOfAccountId == 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Chart Of Account Code not valid");
                return;
            }
        }

        protected void cboSRItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (e.Value)
            {
                case "":
                    {
                        ChartOfAccountCodeText = string.Format("{0} - {1}", "0", "");
                        SubLedgerId = 0;
                        break;
                    }
                case "11":
                    {
                        var sup = new Supplier();
                        var supID = ((RadComboBox)Helper.FindControlRecursive(this.Page, "cboSupplierID")).SelectedValue;
                        if (sup.LoadByPrimaryKey(supID))
                        {
                            var coa = new ChartOfAccounts();
                            if (coa.LoadByPrimaryKey(sup.ChartOfAccountIdAP ?? 0))
                            {
                                ChartOfAccountCodeText = string.Format("{0} - {1}", coa.ChartOfAccountCode, coa.ChartOfAccountName);
                            }
                            SubLedgerId = sup.SubledgerIdAP ?? 0;
                        }

                        break;
                    }
                default:
                    {
                        var sup = new Supplier();
                        var supID = ((RadComboBox)Helper.FindControlRecursive(this.Page, "cboSupplierID")).SelectedValue;
                        if (sup.LoadByPrimaryKey(supID))
                        {
                            var coa = new ChartOfAccounts();
                            if (coa.LoadByPrimaryKey(sup.ChartOfAccountIdAPNonMedic ?? 0))
                            {
                                ChartOfAccountCodeText = string.Format("{0} - {1}", coa.ChartOfAccountCode, coa.ChartOfAccountName);
                            }
                            SubLedgerId = sup.SubledgerIdAPNonMedic ?? 0;
                        }

                        break;
                    }
            }
        }

        protected void cboChartOfAccountCode_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var cboCoa = (RadComboBox)o;
            cboSubledgerId.Items.Clear();
            cboSubledgerId.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboCoa.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboCoa.Items.Clear();
                cboCoa.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountCode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboChartOfAccountCode_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var cboCOA = sender as RadComboBox;
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboCOA.DataSource = dtb;
            cboCOA.DataBind();
        }

        protected void cboSubledgerId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        protected void cboSubledgerId_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            int coaID = ChartOfAccountId;
            if (coaID == 0)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(coaID);
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerId.DataSource = dtb;
            cboSubledgerId.DataBind();


            //int groupID;
            //if (cboChartOfAccountId.SelectedValue == string.Empty)
            //{
            //    groupID = 0;
            //}
            //else
            //{
            //    ChartOfAccounts coa = new ChartOfAccounts();
            //    coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountId.SelectedValue));
            //    groupID = coa.SubLedgerId ?? 0;
            //}
            //var query = new SubLedgersQuery();
            //query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            //query.Where(query.GroupId == groupID);
            //query.Where
            //            (
            //                query.Or
            //                (
            //                    query.SubLedgerName.Like(string.Format("%.{0}%", e.Text)),
            //                    query.Description.Like(string.Format("%.{0}%", e.Text))
            //                )
            //            );
            //query.es.Top = 20;
            //DataTable dtb = query.LoadDataTable();
            //cboSubledgerId.DataSource = dtb;
            //cboSubledgerId.DataBind();
        }

        public String SequenceNo
        {
            get { return ViewState["SequenceNo"].ToString(); }
        }

        public Decimal PaymentAmount
        {
            get { return Convert.ToDecimal(txtPaymentAmount.Value); }
        }

        public String CurrencyID
        {
            get { return cboCurrency.SelectedValue; }
        }

        public String InvoiceReferenceNo
        {
            get { return txtInvoiceReferenceNo.Text; }
        }       
        public String Notes
        {
            get { return txtNotes.Text; }
        }

        //public string ChartOfAccountID
        //{
        //    get { return cboChartOfAccountId.SelectedValue; }
        //}

        public int ChartOfAccountId
        {
            get
            {
                try
                {
                    int retVal = 0;
                    var coa = new ChartOfAccountsQuery();
                    coa.Where(coa.ChartOfAccountCode == cboChartOfAccountCode.Text.Split('-')[0]);
                    coa.Select(coa.ChartOfAccountId);
                    var dtb = coa.LoadDataTable();
                    if (dtb != null && dtb.Rows.Count > 0)
                        int.TryParse(dtb.Rows[0][0].ToString(), out retVal);
                    return retVal;
                }
                catch (Exception)
                {
                    //nothing;
                }
                return 0;
            }
        }

        public string ChartOfAccountCodeText
        {
            get { return this.cboChartOfAccountCode.Text; }
            set { this.cboChartOfAccountCode.Text = value; }
        }
        
        public int SubLedgerId
        {
            get
            {
                try
                {
                    int retVal = 0;
                    int groupID;
                    int coaID = ChartOfAccountId;
                    if (coaID == 0)
                    {
                        groupID = 0;
                    }
                    else
                    {
                        ChartOfAccounts coa = new ChartOfAccounts();
                        coa.LoadByPrimaryKey(coaID);
                        groupID = coa.SubLedgerId ?? 0;
                    }

                    var sub = new SubLedgersQuery();
                    sub.Where(sub.SubLedgerName == cboSubledgerId.Text.Split(new string[] { " - " }, StringSplitOptions.None)[0], sub.GroupId == groupID);
                    sub.Select(sub.SubLedgerId);
                    var dtb = sub.LoadDataTable();
                    if (dtb != null && dtb.Rows.Count > 0)
                        int.TryParse(dtb.Rows[0][0].ToString(), out retVal);
                    return retVal;
                }
                catch (Exception)
                {
                    //nothing;
                }
                return 0;
            }
            set
            {
                var query = new SubLedgersQuery();
                query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
                query.Where(query.SubLedgerId == value);
                var dtb = query.LoadDataTable();
                cboSubledgerId.DataSource = dtb;
                cboSubledgerId.DataBind();
                ComboBox.SelectedValue(cboSubledgerId, value.ToString());
            }
        }

        //public string SubledgerID
        //{
        //    get { return cboSubledgerId.SelectedValue; }
        //}

        public string SRItemType
        {
            get { return this.cboSRItemType.SelectedValue; }
            set { ComboBox.SelectedValue(cboSRItemType, value); }
        }
    }
}