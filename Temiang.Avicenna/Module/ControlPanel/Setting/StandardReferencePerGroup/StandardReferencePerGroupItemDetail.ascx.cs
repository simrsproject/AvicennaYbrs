using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Linq;
using System.Data.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.Module.Charges;

namespace Temiang.Avicenna.Module.ControlPanel.Setting
{
    public partial class StandardReferencePerGroupItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadTextBox TxtStandardReferenceId
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtStandardReferenceID"); }
        }

        private CheckBox chkIsHasCOA
        {
            get
            { return (CheckBox)Helper.FindControlRecursive(Page, "chkIsHasCOA"); }
        }

        private CheckBox chkIsNumericValue
        {
            get
            { return (CheckBox)Helper.FindControlRecursive(Page, "chkIsNumericValue"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            InitInput();
            
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                chkIsActive.Checked = true;
                return;
            }

            ViewState["IsNewRecord"] = false;
            txtItemID.ReadOnly = true;

            txtItemID.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemID);
            txtItemName.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemName);
            txtNote.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.Note);
            txtReferenceID.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ReferenceID);
            if (trBackColor.Visible)
                txtBackColor.SelectedColor = ColorTranslator.FromHtml(txtReferenceID.Text);

            var i = new Item();
            if (i.LoadByPrimaryKey(txtReferenceID.Text))
            {
                var itemq = new ItemQuery();
                itemq.Where(itemq.ItemID == txtReferenceID.Text);
                cboItemID.DataSource = itemq.LoadDataTable();
                cboItemID.DataBind();
                cboItemID.SelectedValue = txtReferenceID.Text;
            }
            else
            {
                cboItemID.Items.Clear();
                cboItemID.Text = string.Empty;
                cboItemID.SelectedValue = string.Empty;
            }

            if (pnlBloodGroup.Visible)
            {
                var customField = (string)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.CustomField);
                chkIsNeedCrossMatchingProcess.Checked = string.IsNullOrEmpty(customField) ? false : customField == "1";

                var customField2 = (string)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.CustomField2);
                chkIsReturnable.Checked = string.IsNullOrEmpty(customField2) ? false : customField2 == "1";
            }
            else
            {
                chkIsNeedCrossMatchingProcess.Checked = false;
                chkIsReturnable.Checked = false;
            }

            chkIsActive.Checked = (Boolean)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.IsActive);

            if (chkIsHasCOA.Checked)
            {
                int coaId = (int?)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.coaID) ?? 0;
                if (coaId != 0) PopulateCboChartOfAccount(cboCOA, coaId);
                int subId = (int?)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.subledgerID) ?? 0;
                if (subId != 0) PopulateCboSubledger(cboSubledger, subId);
            }
            
            txtNumericValue.Value = System.Convert.ToDouble(DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.NumericValue));
        }

        private void InitInput()
        {
            pnlCOA.Visible = chkIsHasCOA.Checked;
            trNumericValue.Visible = chkIsNumericValue.Checked;

            trReferenceId.Visible = TxtStandardReferenceId.Text != "RiskManagementBands" && TxtStandardReferenceId.Text == "BloodGroup";
            trBackColor.Visible = TxtStandardReferenceId.Text == "RiskManagementBands";
            pnlBloodGroup.Visible= TxtStandardReferenceId.Text == "BloodGroup";
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (AppStandardReferenceItemCollection)Session["collAppStandardReferenceItem"];

                string itemId = txtItemID.Text;
                bool isExist = false;
                foreach (AppStandardReferenceItem item in coll)
                {
                    if (item.ItemID.Equals(itemId))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", itemId);
                }
            }
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery();
            query.Select(query.ItemID, query.ItemName);
            query.Where
            (query.SRItemType.In(ItemType.Service, ItemType.Laboratory), query.IsActive == true,
                query.Or
                (
                    query.ItemID.Like(searchTextContain),
                    query.ItemName.Like(searchTextContain)
                )
            );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboItemID.DataSource = dtb;
            cboItemID.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        #region COA
        protected void cboCOA_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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
            query.Where(query.IsDetail == 1);
            query.Where(query.IsActive == 1);
            query.es.Top = 20;
            System.Data.DataTable dtb = query.LoadDataTable();
            cboCOA.DataSource = dtb;
            cboCOA.DataBind();
        }
        protected void cboCOA_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((System.Data.DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((System.Data.DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((System.Data.DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        protected void cboCOA_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledger.Items.Clear();
            cboSubledger.Text = string.Empty;
        }
        private void PopulateCboChartOfAccount(RadComboBox comboBox, int coaId)
        {
            ChartOfAccountsQuery coaQ = new ChartOfAccountsQuery();
            coaQ.Select(coaQ.ChartOfAccountId, coaQ.ChartOfAccountCode, coaQ.ChartOfAccountName);
            coaQ.Where(coaQ.ChartOfAccountId == coaId);
            DataTable dtbCoa = coaQ.LoadDataTable();
            comboBox.DataSource = dtbCoa;
            comboBox.DataBind();
            if (dtbCoa.AsEnumerable().Where(x => x.Field<int>("ChartOfAccountId") == coaId).Count() > 0)
                comboBox.SelectedValue = coaId.ToString();
        }
        protected void cboSubledger_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox box = sender as RadComboBox;
            box.Items.Clear();

            string val = e.Text;

            int coa = System.Convert.ToInt32(cboCOA.SelectedValue);
            var entity = ChartOfAccounts.GetById(coa);
            if (entity != null)
            {
                if ((bool)entity.IsDetail /*&& entity.AccountLevel == 4*/)
                {
                    if (entity.SubLedgerId.HasValue && entity.SubLedgerId.Value != 0)
                    {
                        box.Items.Clear();
                        var sl = SubLedgers.GetByGroupId(entity.SubLedgerId.Value, val);
                        //var slo = from s in sl where s.Description.Contains(val) select s;
                        box.DataSource = sl;
                        box.DataBind();
                    }
                }
            }
        }
        protected void cboSubledger_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((SubLedgers)e.Item.DataItem).SubLedgerName + " - " + ((SubLedgers)e.Item.DataItem).Description.ToString();
            e.Item.Value = ((SubLedgers)e.Item.DataItem).SubLedgerId.ToString();
        }
        private void PopulateCboSubledger(RadComboBox comboBox, int subId)
        {
            if (subId != 0)
            {
                comboBox.Items.Clear();
                var sl = SubLedgers.Get(subId);
                var slColl = new SubLedgersCollection();
                slColl.AttachEntity(sl);
                comboBox.DataSource = slColl;
                comboBox.DataBind();
            }
        }
        #endregion

        #region Properties for return entry value

        public String ItemID
        {
            get { return txtItemID.Text; }
        }

        public String ItemName
        {
            get { return txtItemName.Text; }
        }

        public String Note
        {
            get { return txtNote.Text; }
        }

        public String ReferenceID
        {
            get
            {
                if (pnlBloodGroup.Visible)
                    return cboItemID.SelectedValue;
                if (trBackColor.Visible)
                    return ColorTranslator.ToHtml(txtBackColor.SelectedColor);

                return txtReferenceID.Text;
            }
        }

        public Boolean IsUsedBySystem
        {
            get { return true; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        public String CustomField
        {
            get 
            {
                if (pnlBloodGroup.Visible)
                    return chkIsNeedCrossMatchingProcess.Checked ? "1" : "0";
                return string.Empty;
            }
        }

        public String CustomField2
        {
            get 
            {
                if (pnlBloodGroup.Visible)
                    return chkIsReturnable.Checked ? "1" : "0";
                return string.Empty;
            }
        }

        public string CoaId
        {
            get { return cboCOA.SelectedValue; }
        }

        public string SubLedgerId
        {
            get { return cboSubledger.SelectedValue; }
        }

        public decimal? NumericValue
        {
            get { return System.Convert.ToDecimal(txtNumericValue.Value ?? 0); }
        }


        #endregion
    }
}