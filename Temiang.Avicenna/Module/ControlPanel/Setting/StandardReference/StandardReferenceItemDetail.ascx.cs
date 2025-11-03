using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Linq;
using System.Data.Linq;
using System.Data;
using System.Drawing;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ControlPanel.Setting
{
    public partial class StandardReferenceItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadTextBox txtStandardReferenceID
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtStandardReferenceID"); }
        }

        private CheckBox chkIsHasCOA
        {
            get
            { return (CheckBox)Helper.FindControlRecursive(Page, "chkIsHasCOA"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            InitInput();
            chkIsUsedBySystem.Enabled = false;
            //Item Length
            int itemLength = Convert.ToInt32(((RadNumericTextBox)Helper.FindControlRecursive(Page, "txtItemLength")).Value);
            if (itemLength > 0 && itemLength < 21) txtItemID.MaxLength = itemLength;
            else txtItemID.MaxLength = 20;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                chkIsUsedBySystem.Checked = false;
                chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtItemID.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemID);
            txtItemName.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemName);
            txtNote.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.Note);
            txtReferenceID.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ReferenceID);
            if (trBackColor.Visible)
                txtBackColor.SelectedColor = ColorTranslator.FromHtml(txtReferenceID.Text);
            chkIsUsedBySystem.Checked = (Boolean)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.IsUsedBySystem);
            chkIsActive.Checked = (Boolean)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.IsActive);
            int coaId = (int?)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.coaID) ?? 0;
            if (coaId != 0) PopulateCboChartOfAccount(cboCOA, coaId);
            int subId = (int?)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.subledgerID) ?? 0;
            if (subId != 0) PopulateCboSubledger(cboSubledger, subId);

            txtNumericValue.Value = System.Convert.ToDouble(DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.NumericValue));
            txtLineNumber.Value = System.Convert.ToDouble(DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.LineNumber));
            txtCustomField.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.CustomField);
            txtCustomField2.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.CustomField2);
        }

        private void InitInput()
        {
            // numeric value
            var txtID = ((RadTextBox)Helper.FindControlRecursive(Page, "txtStandardReferenceID"));
            if (txtID != null) {
                if (!string.IsNullOrEmpty(txtID.Text)) {
                    var std = new AppStandardReference();
                    if(std.LoadByPrimaryKey(txtID.Text)){
                        if (std.IsNumericValue ?? false) {
                            txtNumericValue.ReadOnly = false;
                        }
                    }
                }
            }
            rfvNumericValue.Enabled = !txtNumericValue.ReadOnly;

            pnlCOA.Visible = chkIsHasCOA.Checked;
            trReferenceID.Visible = txtStandardReferenceID.Text != "PrescriptionCategory";
            trBackColor.Visible = txtStandardReferenceID.Text == "PrescriptionCategory";
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                AppStandardReferenceItemCollection coll = (AppStandardReferenceItemCollection)Session["collAppStandardReferenceItem"];

                string itemID = txtItemID.Text;
                bool isExist = false;
                foreach (AppStandardReferenceItem item in coll)
                {
                    if (item.ItemID.Equals(itemID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", itemID);
                }
            }
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
            get { return HtmlTagHelper.Devalidate(txtItemName.Text); }
        }

        public String Note
        {
            get { return txtNote.Text; }
        }

        public String ReferenceID
        {
            get 
            {
                if (trReferenceID.Visible)
                    return txtReferenceID.Text;

                return ColorTranslator.ToHtml(txtBackColor.SelectedColor);
            }
        }

        public Boolean IsUsedBySystem
        {
            get { return chkIsUsedBySystem.Checked; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
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

        public Int32 LineNumber
        {
            get { return System.Convert.ToInt32(txtLineNumber.Value ?? 0); }
        }
        public String CustomField
        {
            get { return txtCustomField.Text; }
        }
        public String CustomField2
        {
            get { return txtCustomField2.Text; }
        }

        #endregion
    }
}