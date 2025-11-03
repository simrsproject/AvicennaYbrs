using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceUnitItemServiceClassDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            ViewState["IsNewRecord"] = false;
            ViewState["ClassID"] = (string)DataBinder.Eval(DataItem, ServiceUnitItemServiceClassMetadata.ColumnNames.ClassID);
            ViewState["TariffComponentID"] = (string)DataBinder.Eval(DataItem, ServiceUnitItemServiceClassMetadata.ColumnNames.TariffComponentID);

            PopulateCombo(true, cboChartOfAccountIdIncome, (int?)DataBinder.Eval(DataItem, ServiceUnitItemServiceClassMetadata.ColumnNames.ChartOfAccountIdIncome));
            PopulateCombo(false, cboSubledgerIdIncome, (int?)DataBinder.Eval(DataItem, ServiceUnitItemServiceClassMetadata.ColumnNames.SubledgerIdIncome));

            PopulateCombo(true, cboChartOfAccountIdDiscount, (int?)DataBinder.Eval(DataItem, ServiceUnitItemServiceClassMetadata.ColumnNames.ChartOfAccountIdDiscount));
            PopulateCombo(false, cboSubledgerIdDiscount, (int?)DataBinder.Eval(DataItem, ServiceUnitItemServiceClassMetadata.ColumnNames.SubledgerIdDiscount));

            PopulateCombo(true, cboChartOfAccountIdCost, (int?)DataBinder.Eval(DataItem, ServiceUnitItemServiceClassMetadata.ColumnNames.ChartOfAccountIdCost));
            PopulateCombo(false, cboSubledgerIdCost, (int?)DataBinder.Eval(DataItem, ServiceUnitItemServiceClassMetadata.ColumnNames.SubledgerIdCost));
        }

        private void PopulateCombo(bool isCOA, RadComboBox comboBox, int? value)
        {
            if (isCOA)
            {
                var coa = new ChartOfAccountsQuery();
                coa.Where(coa.ChartOfAccountId == (value ?? 0));

                comboBox.DataSource = coa.LoadDataTable();
            }
            else
            {
                var sub = new SubLedgersQuery();
                sub.Where(sub.SubLedgerId == (value ?? 0));

                comboBox.DataSource = sub.LoadDataTable();
            }
            comboBox.DataBind();

            if (comboBox.Items.Count > 0)
                comboBox.SelectedValue = (value ?? 0).ToString();
        }

        public String TariffComponentID
        {
            get { return (string)ViewState["TariffComponentID"]; }
        }

        public int? COARevenueID
        {
            get
            {
                int value;
                int.TryParse(cboChartOfAccountIdIncome.SelectedValue, out value);
                return value;
            }
        }

        public String COARevenueName
        {
            get { return cboChartOfAccountIdIncome.Text; }
        }

        public int? SubledgerRevenueID
        {
            get
            {
                int value;
                int.TryParse(cboSubledgerIdIncome.SelectedValue, out value);
                return value;
            }
        }

        public String SubledgerRevenueName
        {
            get { return cboSubledgerIdIncome.Text; }
        }

        public int? COADiscountID
        {
            get
            {
                int value;
                int.TryParse(cboChartOfAccountIdDiscount.SelectedValue, out value);
                return value;
            }
        }

        public String COADiscountName
        {
            get { return cboChartOfAccountIdDiscount.Text; }
        }

        public int? SubledgerDiscountlID
        {
            get
            {
                int value;
                int.TryParse(cboSubledgerIdDiscount.SelectedValue, out value);
                return value;
            }
        }

        public String SubledgerDiscountName
        {
            get { return cboSubledgerIdDiscount.Text; }
        }

        public int? COACostID
        {
            get
            {
                int value;
                int.TryParse(cboChartOfAccountIdCost.SelectedValue, out value);
                return value;
            }
        }

        public String COACostName
        {
            get { return cboChartOfAccountIdCost.Text; }
        }

        public int? SubledgerCostlID
        {
            get
            {
                int value;
                int.TryParse(cboSubledgerIdCost.SelectedValue, out value);
                return value;
            }
        }

        public String SubledgerCostName
        {
            get { return cboSubledgerIdCost.Text; }
        }

        protected void cboChartOfAccountIdIncome_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                cboSubledgerIdIncome.Items.Clear();
                cboSubledgerIdIncome.Text = string.Empty;
            }
        }

        protected void cboChartOfAccountIdIncome_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.es.Top = 20;
            query.Select(
                query.ChartOfAccountId,
                query.ChartOfAccountCode,
                query.ChartOfAccountName
                );
            query.Where(
                query.Or(
                    query.ChartOfAccountCode.Like(searchTextContain),
                    query.ChartOfAccountName.Like(searchTextContain)
                    )
                );
            query.Where(
                query.IsDetail == 1,
                query.IsActive == 1
                );
            query.OrderBy(query.ChartOfAccountId.Ascending);

            cboChartOfAccountIdIncome.DataSource = query.LoadDataTable();
            cboChartOfAccountIdIncome.DataBind();
        }

        protected void cboChartOfAccountIdIncome_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"] + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboSubledgerIdIncome_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (string.IsNullOrEmpty(cboChartOfAccountIdIncome.SelectedValue))
                groupID = 0;
            else
            {
                var coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdIncome.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.es.Top = 20;
            query.Select(
                query.SubLedgerId,
                query.SubLedgerName,
                query.Description
                );
            query.Where(
                query.GroupId == groupID,
                query.Or(
                    query.SubLedgerName.Like(searchTextContain),
                    query.Description.Like(searchTextContain)
                    )
                );

            cboSubledgerIdIncome.DataSource = query.LoadDataTable();
            cboSubledgerIdIncome.DataBind();
        }

        protected void cboSubledgerIdIncome_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"] + " - " + ((DataRowView)e.Item.DataItem)["Description"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        protected void cboChartOfAccountIdDiscount_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                cboSubledgerIdDiscount.Items.Clear();
                cboSubledgerIdDiscount.Text = string.Empty;
            }
        }

        protected void cboChartOfAccountIdDiscount_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.es.Top = 20;
            query.Select(
                query.ChartOfAccountId,
                query.ChartOfAccountCode,
                query.ChartOfAccountName
                );
            query.Where(
                    query.Or(
                    query.ChartOfAccountCode.Like(searchTextContain),
                    query.ChartOfAccountName.Like(searchTextContain)
                    ),
                    query.IsDetail == 1,
                    query.IsActive == 1
                );
            query.OrderBy(query.ChartOfAccountId.Ascending);

            cboChartOfAccountIdDiscount.DataSource = query.LoadDataTable();
            cboChartOfAccountIdDiscount.DataBind();
        }

        protected void cboChartOfAccountIdDiscount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"] + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboSubledgerIdDiscount_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (string.IsNullOrEmpty(cboChartOfAccountIdDiscount.SelectedValue))
                groupID = 0;
            else
            {
                var coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdDiscount.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.es.Top = 20;
            query.Select(
                query.SubLedgerId,
                query.SubLedgerName,
                query.Description
                );
            query.Where(
                    query.GroupId == groupID,
                    query.Or(
                    query.SubLedgerName.Like(searchTextContain),
                    query.Description.Like(searchTextContain)
                    )
                );

            cboSubledgerIdDiscount.DataSource = query.LoadDataTable();
            cboSubledgerIdDiscount.DataBind();
        }

        protected void cboSubledgerIdDiscount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"] + " - " + ((DataRowView)e.Item.DataItem)["Description"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        protected void cboChartOfAccountIdCost_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                cboSubledgerIdCost.Items.Clear();
                cboSubledgerIdCost.Text = string.Empty;
                return;
            }
        }

        protected void cboChartOfAccountIdCost_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.es.Top = 20;
            query.Select(
                query.ChartOfAccountId,
                query.ChartOfAccountCode,
                query.ChartOfAccountName
                );
            query.Where(
                    query.Or(
                    query.ChartOfAccountCode.Like(searchTextContain),
                    query.ChartOfAccountName.Like(searchTextContain)
                    ),
                    query.IsDetail == 1,
                    query.IsActive == 1
                );
            query.OrderBy(query.ChartOfAccountId.Ascending);

            cboChartOfAccountIdCost.DataSource = query.LoadDataTable();
            cboChartOfAccountIdCost.DataBind();
        }

        protected void cboChartOfAccountIdCost_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"] + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboSubledgerIdCost_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (string.IsNullOrEmpty(cboChartOfAccountIdCost.SelectedValue))
                groupID = 0;
            else
            {
                var coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdCost.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.es.Top = 20;
            query.Select(
                query.SubLedgerId,
                query.SubLedgerName,
                query.Description
                );
            query.Where(
                    query.GroupId == groupID,
                    query.Or(
                    query.SubLedgerName.Like(searchTextContain),
                    query.Description.Like(searchTextContain)
                    )
                );

            cboSubledgerIdCost.DataSource = query.LoadDataTable();
            cboSubledgerIdCost.DataBind();
        }

        protected void cboSubledgerIdCost_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"] + " - " + ((DataRowView)e.Item.DataItem)["Description"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
    }
}