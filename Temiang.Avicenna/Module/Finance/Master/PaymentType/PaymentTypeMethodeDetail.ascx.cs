using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class PaymentTypeMethodeDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtSRPaymentMethodID.Text = (String)DataBinder.Eval(DataItem, PaymentMethodMetadata.ColumnNames.SRPaymentMethodID);
            txtPaymentMethodName.Text = (String)DataBinder.Eval(DataItem, PaymentMethodMetadata.ColumnNames.PaymentMethodName);
            if (txtSRPaymentMethodID.Text != string.Empty)
            {
                string srPaymentTypeID = ((RadTextBox)Helper.FindControlRecursive(Page, "txtSRPaymentTypeID")).Text;
                PaymentMethod x = new PaymentMethod();
                x.LoadByPrimaryKey(srPaymentTypeID, txtSRPaymentMethodID.Text);
                int chartOfAccountId = (x.ChartOfAccountID.HasValue ? x.ChartOfAccountID.Value : 0);
                int subLedgerId = (x.SubledgerID.HasValue ? x.SubledgerID.Value : 0);

                if (chartOfAccountId != 0)
                {
                    ChartOfAccountsQuery coaQ = new ChartOfAccountsQuery();
                    coaQ.Select(coaQ.ChartOfAccountId, coaQ.ChartOfAccountCode, coaQ.ChartOfAccountName);
                    coaQ.Where(coaQ.ChartOfAccountId == chartOfAccountId);
                    DataTable dtbCoa = coaQ.LoadDataTable();
                    cboChartOfAccountId.DataSource = dtbCoa;
                    cboChartOfAccountId.DataBind();
                    if (dtbCoa.Rows.Count > 0)
                    {
                        cboChartOfAccountId.SelectedValue = chartOfAccountId.ToString();
                    }
                    if (subLedgerId != 0)
                    {
                        SubLedgersQuery slQ = new SubLedgersQuery();
                        slQ.Select(slQ.SubLedgerId, slQ.SubLedgerName, slQ.Description);
                        slQ.Where(slQ.SubLedgerId == subLedgerId);
                        DataTable dtbSl = slQ.LoadDataTable();
                        cboSubledgerId.DataSource = dtbSl;
                        cboSubledgerId.DataBind();
                        cboSubledgerId.SelectedValue = subLedgerId.ToString();
                    }
                    else
                    {
                        cboSubledgerId.Items.Clear();
                        cboSubledgerId.Text = string.Empty;
                    }
                }
                else
                {
                    cboChartOfAccountId.Items.Clear();
                    cboSubledgerId.Items.Clear();
                    cboChartOfAccountId.Text = string.Empty;
                    cboSubledgerId.Text = string.Empty;
                }
            }
            else
            {
                cboChartOfAccountId.Items.Clear();
                cboSubledgerId.Items.Clear();
                cboChartOfAccountId.Text = string.Empty;
                cboSubledgerId.Text = string.Empty;
            }
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PaymentMethodCollection coll =
                    (PaymentMethodCollection)Session["collPaymentMethod"];

                string srPaymentMethodID = txtSRPaymentMethodID.Text;
                bool isExist = false;
                foreach (PaymentMethod item in coll)
                {
                    if (item.SRPaymentTypeID.Equals(srPaymentMethodID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Payment Method ID: {0} has exist", srPaymentMethodID);
                }
            }
        }

        #region Properties for return entry value
        public String SRPaymentMethodID
        {
            get { return txtSRPaymentMethodID.Text; }
        }
        public String PaymentMethodName
        {
            get { return txtPaymentMethodName.Text; }
        }
        public String ChartOfAccountID
        {
            get { return cboChartOfAccountId.SelectedValue; }
        }
        public String ChartOfAccountName
        {
            get { return cboChartOfAccountId.Text; }
        }
        public String SubLedgerID
        {
            get { return cboSubledgerId.SelectedValue; }
        }
        public String SubLedgerName
        {
            get { return cboSubledgerId.Text; }
        }
        #endregion

        #region Method & Event TextChanged

        protected void cboChartOfAccountId_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerId.Items.Clear();
            cboSubledgerId.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountId.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountId.Items.Clear();
                cboChartOfAccountId.Text = string.Empty;
                return;
            }
        }

        #endregion

        #region ComboBox ChartOfAccountId
        protected void cboChartOfAccountId_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
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
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountId.DataSource = dtb;
            cboChartOfAccountId.DataBind();
        }

        protected void cboChartOfAccountId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        #endregion

        #region ComboBox SubledgerId
        protected void cboSubledgerId_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountId.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountId.SelectedValue));
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
        }

        protected void cboSubledgerId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion
    }
}
