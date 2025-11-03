using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceUnitItemServiceCompDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadTextBox TxtItemId
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtItemID"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboRegType, AppEnum.StandardReference.RegistrationType);

            StandardReference.InitializeIncludeSpace(cboAccountGroupID, AppEnum.StandardReference.GuarantorIncomeGroup);
            if (cboAccountGroupID.Items.Count == 2) cboAccountGroupID.SelectedIndex = 1;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                PopulateTariffComponent(true);

                return;
            }

            ViewState["IsNewRecord"] = false;
            
            PopulateTariffComponent(false);
            cboTariffComponentID.SelectedValue = (string)DataBinder.Eval(DataItem, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.TariffComponentID);
            cboTariffComponentID.Enabled = false;
            cboRegType.SelectedValue = (string)DataBinder.Eval(DataItem, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SRRegistrationType);
            cboRegType.Enabled = false;

            PopulateCombo(true, cboChartOfAccountIdIncome, (int?)DataBinder.Eval(DataItem, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ChartOfAccountIdIncome));
            PopulateCombo(false, cboSubledgerIdIncome, (int?)DataBinder.Eval(DataItem, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SubledgerIdIncome));

            PopulateCombo(true, cboChartOfAccountIdDiscount, (int?)DataBinder.Eval(DataItem, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ChartOfAccountIdDiscount));
            PopulateCombo(false, cboSubledgerIdDiscount, (int?)DataBinder.Eval(DataItem, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SubledgerIdDiscount));

            PopulateCombo(true, cboChartOfAccountIdCost, (int?)DataBinder.Eval(DataItem, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.ChartOfAccountIdCost));
            PopulateCombo(false, cboSubledgerIdCost, (int?)DataBinder.Eval(DataItem, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SubledgerIdCost));

            cboAccountGroupID.SelectedValue = (string)DataBinder.Eval(DataItem, ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SRGuarantorIncomeGroup);
            cboAccountGroupID.Enabled = false;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ServiceUnitItemServiceCompMappingCollection)Session["ServiceUnitItemServiceCompMappingCollection"];

                string componentId = cboTariffComponentID.SelectedValue;
                string regType = cboRegType.SelectedValue;
                bool isExist = false;
                foreach (ServiceUnitItemServiceCompMapping item in coll)
                {
                    if (item.TariffComponentID.Equals(componentId) && item.SRRegistrationType.Equals(regType) &&
                        item.SRGuarantorIncomeGroup.Equals(GuarantorIncomeGroupID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Data Tariff Component: {0} with Registration Type: {1} and guarantor income group: {2} already exist", cboTariffComponentID.Text, cboRegType.Text, GuarantorIncomeGroupName);
                }
            }
            //if (string.IsNullOrEmpty(cboTariffComponentID.SelectedValue))
            //{
            //    args.IsValid = false;
            //    ((CustomValidator)source).ErrorMessage = string.Format("Tariff Component required.");
            //}
            //if (string.IsNullOrEmpty(cboRegType.SelectedValue))
            //{
            //    args.IsValid = false;
            //    ((CustomValidator)source).ErrorMessage = string.Format("Registration Type required.");
            //}
        }

        private void PopulateTariffComponent(bool isAddNewRecord)
        {
            if (isAddNewRecord)
            {
                var itemTariffCompQ = new ItemTariffComponentQuery("a");
                var tariffCompQ = new TariffComponentQuery("b");
                itemTariffCompQ.InnerJoin(tariffCompQ).On(itemTariffCompQ.TariffComponentID == tariffCompQ.TariffComponentID);
                itemTariffCompQ.Select(itemTariffCompQ.TariffComponentID, tariffCompQ.TariffComponentName);
                itemTariffCompQ.Where(itemTariffCompQ.ItemID == TxtItemId.Text);
                itemTariffCompQ.OrderBy(itemTariffCompQ.TariffComponentID.Ascending);
                itemTariffCompQ.es.Distinct = true;
                DataTable dtb = itemTariffCompQ.LoadDataTable();
                foreach (DataRow row in dtb.Rows)
                {
                    cboTariffComponentID.Items.Add(new RadComboBoxItem(row["TariffComponentName"].ToString(), row["TariffComponentID"].ToString()));
                }

                var tci = new TransChargesItemQuery("tci");
                var tcic = new TransChargesItemCompQuery("tcic");
                tci.InnerJoin(tcic).On(tci.TransactionNo == tcic.TransactionNo)
                    .Where(tci.ItemID == TxtItemId.Text)
                    .Select(tcic.TariffComponentID);
                tci.es.Distinct = true;
                var tbl = tci.LoadDataTable();
                var tcColl = new TariffComponentCollection();
                tcColl.LoadAll();

                foreach (System.Data.DataRow row in tbl.AsEnumerable()
                    .Where(x => !dtb.AsEnumerable()
                                .Select(y => y["TariffComponentID"].ToString())
                                .Contains(x[0].ToString()))) {
                    cboTariffComponentID.Items.Add(new RadComboBoxItem(
                        tcColl.Where(x => x.TariffComponentID == row[0].ToString()).Select(x => x.TariffComponentName).FirstOrDefault(),
                        row[0].ToString()));
                }
            }
            else
            {
                var tariffCompQ = new TariffComponentQuery("a");
                tariffCompQ.Select(tariffCompQ.TariffComponentID, tariffCompQ.TariffComponentName);
                tariffCompQ.OrderBy(tariffCompQ.TariffComponentID.Ascending);
                DataTable dtb = tariffCompQ.LoadDataTable();
                foreach (DataRow row in dtb.Rows)
                {
                    cboTariffComponentID.Items.Add(new RadComboBoxItem(row["TariffComponentName"].ToString(), row["TariffComponentID"].ToString()));
                }
            }
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
            get { return cboTariffComponentID.SelectedValue; }
        }

        public String SRRegistrationType
        {
            get { return cboRegType.SelectedValue; }
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

        public string GuarantorIncomeGroupID
        {
            get
            {
                return cboAccountGroupID.SelectedValue;
            }
        }

        public String GuarantorIncomeGroupName
        {
            get
            {
                return cboAccountGroupID.Text;
            }
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
