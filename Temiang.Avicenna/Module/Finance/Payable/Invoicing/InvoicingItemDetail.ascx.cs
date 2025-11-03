using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.Payable
{
    public partial class InvoicingItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            // Currency
            var curr = new CurrencyRateCollection();
            curr.Query.Where(curr.Query.IsActive == true);
            curr.LoadAll();
            cboCurrencyID.Items.Clear();
            cboCurrencyID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var entity in curr)
            {
                cboCurrencyID.Items.Add(new RadComboBoxItem(entity.CurrencyName, entity.CurrencyID));
            }

            if (AppSession.Parameter.IsPphUsesAfixedValue)
                pnlPphFixedValue.Visible = true;
            else
                pnlPphNonFixedValue.Visible = true;

            StandardReference.InitializeIncludeSpace(cboSRPph, AppEnum.StandardReference.Pph);

            if (AppSession.Parameter.IsCoaAPNonMedicSeparated) {
                trSRItemType.Visible = true;
                cboSRItemType.Visible = true;
                rfvSRItemType.Visible = true;
            }

            txtDownPaymentAmount.ReadOnly = true;
            chkIsPpnExcluded.Enabled = !AppSession.Parameter.IsApInvoiceIncPPN;

            if (DataItem is GridInsertionObject)
            {
                txtTransactionNo.Text = "-";
                txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();

                txtAmount.Value = 0;
                txtPPnAmount.Value = 0;
                chkIsPpnExcluded.Checked = false;
                txtPPh22Amount.Value = 0;
                txtPPh23Amount.Value = 0;
                txtPphPercentage.Value = 0;
                txtPphAmount.Value = 0;
                txtStampAmount.Value = 0;
                txtDownPaymentAmount.Value = 0;
                txtOtherDeduction.Value = 0;
                txtBasicPphCalculation.Value = 0;

                if (!AppSession.Parameter.IsCoaAPNonMedicSeparated)
                {
                    var sup = new Supplier();
                    var supID = ((RadComboBox)Helper.FindControlRecursive(this.Page, "cboSupplierID")).SelectedValue;
                    if (sup.LoadByPrimaryKey(supID))
                    {
                        var coa = new ChartOfAccounts();
                        if (coa.LoadByPrimaryKey(sup.ChartOfAccountIdAPTemporary ?? 0))
                        {
                            ChartOfAccountCodeText = string.Format("{0} - {1}", coa.ChartOfAccountCode, coa.ChartOfAccountName);
                        }
                        SubLedgerId = sup.SubledgerIdAPTemporary ?? 0;
                    }
                }

                cboCurrencyID.SelectedValue = AppSession.Parameter.CurrencyRupiahID;
                txtCurrencyRate.Value = 1;

                return;
            }

            txtTransactionNo.Text = (String)DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.TransactionNo);
            txtTransactionDate.SelectedDate =
                    (DateTime)DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.TransactionDate);
     
            txtAmount.Value =
                Convert.ToDouble(DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.Amount));
            txtPPnAmount.Value =
                            Convert.ToDouble(DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.PPnAmount));

            chkIsPpnExcluded.Checked = (DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.IsPpnExcluded) is DBNull) ? false :
                Convert.ToBoolean(DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.IsPpnExcluded));

            txtPPh22Amount.Value =
                Convert.ToDouble(DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.PPh22Amount));
            txtPPh23Amount.Value =
                Convert.ToDouble(DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.PPh23Amount));

            cboSRPph.SelectedValue = (String)DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.SRPph);
            txtPphPercentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.PphPercentage));
            txtPphAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.PphAmount));

            txtStampAmount.Value =
                Convert.ToDouble(DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.StampAmount));
            txtOtherDeduction.Value =
                            Convert.ToDouble(DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.OtherDeduction));
            txtDownPaymentAmount.Value =
                            Convert.ToDouble(DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.DownPaymentAmount));
            txtNotes.Text = (String)DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.Notes);
            txtInvoiceSN.Text = (String)DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.InvoiceSN);
            object taxInvoiceDate = DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.TaxInvoiceDate);
            if (taxInvoiceDate != null)
                txtTaxInvoiceDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.TaxInvoiceDate);
            else
                txtTaxInvoiceDate.Clear();

            SRItemType = (String)DataBinder.Eval(DataItem, InvoiceSupplierItemMetadata.ColumnNames.SRItemType);
            ChartOfAccountCodeText = string.Format("{0} - {1}", DataBinder.Eval(DataItem, "ChartOfAccountCode"), DataBinder.Eval(DataItem, "ChartOfAccountName"));
            CurrencyRate = Convert.ToDecimal( DataBinder.Eval(DataItem, "CurrencyRate"));
            CurrencyID = (String)DataBinder.Eval(DataItem, "CurrencyID");
            SubLedgerId = Convert.ToInt16(DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.SubLedgerId));

            var por = new ItemTransaction();
            txtBasicPphCalculation.Value = por.LoadByPrimaryKey(txtTransactionNo.Text) ? Convert.ToDouble(por.PphAmount) : 0;

            // Enable
            cboCOA.Enabled = true.Equals(DataBinder.Eval(DataItem, "IsAdditionalInvoice")) ;
            cboCurrencyID.Enabled = cboCOA.Enabled;
            txtCurrencyRate.Enabled = cboCOA.Enabled;
            cboSl.Enabled = cboCOA.Enabled;

            if (AppSession.Parameter.IsCoaAPNonMedicSeparated)
            {
                cboSRItemType.Enabled = cboCOA.Enabled;
                rfvSRItemType.Enabled = cboCOA.Enabled;
            }

            if (!AppSession.Parameter.IsAllowEditAmountApInvoice)
            {
                txtAmount.ReadOnly = !cboCOA.Enabled;
                txtPPnAmount.ReadOnly = !cboCOA.Enabled;
            }
            
        }

        protected void cboCurrencyID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                var c = new CurrencyRate();
                c.LoadByPrimaryKey(e.Value);
                txtCurrencyRate.Value = Convert.ToDouble(c.CurrencyRate);
            }
            else
                txtCurrencyRate.Value = 1;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtAmount.Value == 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Invalid amount");
                return;
            }

            if (txtPPh22Amount.Value > 0 && txtPPh23Amount.Value > 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("PPh 22 & PPh 23 Amount can not both have value more than 0");
                return;
            }

            if (true.Equals(DataBinder.Eval(DataItem, "IsAdditionalInvoice")) && txtCurrencyRate.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Currency Rate Must Bigger than 0");
                return;
            }

            if ((cboCOA.Enabled || true.Equals(DataBinder.Eval(DataItem, "IsAdditionalInvoice"))) && ChartOfAccountId==0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Chart Of AccountCode not valid");
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
        }

        protected void cboSRItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (e.Value) {
                case "": {
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
                            if (coa.LoadByPrimaryKey(sup.ChartOfAccountIdAPTemporary ?? 0))
                            {
                                //ChartOfAccountCodeText = string.Format("{0} - {1}", coa.ChartOfAccountCode, coa.ChartOfAccountName);
                                var args = new RadComboBoxItemsRequestedEventArgs();
                                args.Text = coa.ChartOfAccountCode;
                                cboCOA_ItemsRequested(cboCOA, args);
                                if (cboCOA.Items.Count == 1) {
                                    cboCOA.SelectedIndex = 0;
                                }
                            }
                            SubLedgerId = sup.SubledgerIdAPTemporary ?? 0;
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
                            if (coa.LoadByPrimaryKey(sup.ChartOfAccountIdAPTemporaryNonMedic ?? 0))
                            {
                                ChartOfAccountCodeText = string.Format("{0} - {1}", coa.ChartOfAccountCode, coa.ChartOfAccountName);
                            }
                            SubLedgerId = sup.SubledgerIdAPTemporaryNonMedic ?? 0;
                        }

                        break;
                    }
            }
        }
        
        #region Properties for return entry value
        public String TransactionNo
        {
            get { return txtTransactionNo.Text; }
        }
        public DateTime? TransactionDate
        {
            get { return txtTransactionDate.SelectedDate; }
        }
    
        public Decimal Amount
        {
            get { return Convert.ToDecimal(txtAmount.Value); }
        }
        public Decimal PPnAmount
        {
            get { return Convert.ToDecimal(txtPPnAmount.Value); }
        }
        public bool IsPpnExcluded {
            get {
                return chkIsPpnExcluded.Checked;
            }
        }
        public Decimal PPh22Amount
        {
            get { return Convert.ToDecimal(txtPPh22Amount.Value); }
        }
        public Decimal PPh23Amount
        {
            get { return Convert.ToDecimal(txtPPh23Amount.Value); }
        }
        public String SRPph
        {
            get { return cboSRPph.SelectedValue; }
        }
        public String PphTypeName
        {
            get { return cboSRPph.Text; }
        }
        public Decimal PphPercentage
        {
            get { return Convert.ToDecimal(txtPphPercentage.Value); }
        }
        public Decimal PphAmount
        {
            get { return Convert.ToDecimal(txtPphAmount.Value); }
        }
        public Decimal StampAmount
        {
            get { return Convert.ToDecimal(txtStampAmount.Value); }
        }
        public Decimal OtherDeduction
        {
            get { return Convert.ToDecimal(txtOtherDeduction.Value); }
        }
        public Decimal DownPaymentAmount
        {
            get { return Convert.ToDecimal(txtDownPaymentAmount.Value); }
        }
        public String Notes
        {
            get { return txtNotes.Text; }
        }
        public String InvoiceSN
        {
            get { return txtInvoiceSN.Text; }
        }
        public DateTime? TaxInvoiceDate
        {
            get { return txtTaxInvoiceDate.SelectedDate; }
        }
        public int ChartOfAccountId
        {
            get
            {
                try
                {
                    int retVal = 0;
                    var coa = new ChartOfAccountsQuery();
                    coa.Where(coa.ChartOfAccountCode == cboCOA.Text.Split('-')[0]);
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
            get { return this.cboCOA.Text; }
            set { this.cboCOA.Text = value; }
        }

        public string CurrencyID
        {
            get { return this.cboCurrencyID.SelectedValue; }
            set { ComboBox.SelectedValue(cboCurrencyID, value); }
        }
        public Decimal CurrencyRate
        {
            get { return Convert.ToDecimal(txtCurrencyRate.Value); }
            set { txtCurrencyRate.Value = Convert.ToDouble(value); }
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
                    sub.Where(sub.SubLedgerName == cboSl.Text.Split(new string[]{" - "}, StringSplitOptions.None)[0], sub.GroupId == groupID);
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
                cboSl.DataSource = dtb;
                cboSl.DataBind();
                ComboBox.SelectedValue(cboSl, value.ToString());
            }
        }
        public string SRItemType
        {
            get { return this.cboSRItemType.SelectedValue; }
            set { ComboBox.SelectedValue(cboSRItemType, value); }
        }
        #endregion


        #region Method & Event TextChanged COA & SL
        protected void cboCOA_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var cboCoa = (RadComboBox)o;
            cboSl.Items.Clear();
            cboSl.Text = string.Empty;

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

        protected void cboCOA_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboCOA_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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

        protected void cboSl_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        protected void cboSl_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var cboSl = (RadComboBox)sender;

            int groupID;
            if (cboCOA.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboCOA.SelectedValue));
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
            cboSl.DataSource = dtb;
            cboSl.DataBind();
        }
        #endregion

        protected void cboSRPph_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var pph = new AppStandardReferenceItem();
            if (pph.LoadByPrimaryKey("Pph", cboSRPph.SelectedValue))
            {
                if (pph.ReferenceID == "Progresif")
                {
                    txtPphPercentage.Value = 0;

                    //decimal pphAmt = InvoiceSupplier.PphProgresif(Convert.ToDecimal(txtAmount.Value));
                    decimal pphAmt = InvoiceSupplier.PphProgresif(Convert.ToDecimal(txtBasicPphCalculation.Value));
                    txtPphAmount.Value = Convert.ToDouble(pphAmt);
                }
                else
                {
                    txtPphPercentage.Value = Convert.ToDouble(pph.ReferenceID);
                    txtPphAmount.Value = txtBasicPphCalculation.Value * (txtPphPercentage.Value / 100);
                    //txtPphAmount.Value = txtAmount.Value * (txtPphPercentage.Value / 100);
                }
            }
            else
            {
                txtPphPercentage.Value = 0;
                txtPphAmount.Value = 0;
            }
        }

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
            var collitem = (InvoiceSupplierItemCollection)Session["collInvoiceSupplierItem" + Request.UserHostName];
        }
    }
}