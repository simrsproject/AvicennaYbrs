using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class CashTransactionTemplateItem : BaseUserControl
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

            this.TemplateDetailId = (int)DataBinder.Eval(DataItem, "TemplateDetailId");

            var coa = ChartOfAccounts.Get((int)DataBinder.Eval(DataItem, "ChartOfAccountId"));
            if (coa != null){
                this.ChartOfAccountCode = coa.ChartOfAccountCode;
                txtChartOfAccountCode_ItemsRequested(txtChartOfAccountCode, 
                    new RadComboBoxItemsRequestedEventArgs() { Text = coa.ChartOfAccountCode.Trim() });
                if (txtChartOfAccountCode.Items.FindItemByValue(coa.ChartOfAccountId.ToString()) != null)
                {
                    txtChartOfAccountCode.SelectedValue = coa.ChartOfAccountId.ToString();
                    txtChartOfAccountCode_TextChanged(txtChartOfAccountCode, new EventArgs());
                }
            }

            var subL = SubLedgers.Get((int)DataBinder.Eval(DataItem, "SubLedgerId"));
            if (subL != null) {
                ddlSubLedger_ItemsRequested(ddlSubLedger, 
                    new RadComboBoxItemsRequestedEventArgs() { Text = subL.Description.Trim() });
                if (ddlSubLedger.Items.FindItemByValue(subL.SubLedgerId.ToString()) != null) {
                    ddlSubLedger.SelectedValue = subL.SubLedgerId.ToString();
                }   
            }

            this.AmountVariablePctg = (decimal)DataBinder.Eval(DataItem, "AmountVariablePercentage");
            this.AmountFixed = (decimal)DataBinder.Eval(DataItem, "AmountFixed");
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            Helper.SetupComboBox(txtChartOfAccountCode);
            Helper.SetupComboBox(ddlSubLedger);

            txtChartOfAccountCode.TextChanged += txtChartOfAccountCode_TextChanged;
            txtChartOfAccountCode.ItemDataBound += txtChartOfAccountCode_ItemDataBound;
            txtChartOfAccountCode.ItemsRequested += txtChartOfAccountCode_ItemsRequested;

            //ddlSubLedger.TextChanged += ddlSubLedger_TextChanged;
            ddlSubLedger.ItemDataBound += ddlSubLedger_ItemDataBound;
            ddlSubLedger.ItemsRequested += ddlSubLedger_ItemsRequested;
        }

        protected void txtChartOfAccountCode_TextChanged(object sender, EventArgs e)
        {
            txtChartOfAccountName.Text = string.Empty;

            if (!string.IsNullOrEmpty(txtChartOfAccountCode.SelectedValue)) {
                var coa = new ChartOfAccounts();
                if (coa.LoadByPrimaryKey(System.Convert.ToInt32(txtChartOfAccountCode.SelectedValue))) {
                    txtChartOfAccountName.Text = coa.ChartOfAccountName;
                }
            }
        }

        protected void txtChartOfAccountCode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var entity = ((ChartOfAccounts)e.Item.DataItem);
            e.Item.Attributes["ChartOfAccountName"] = entity.ChartOfAccountName;
        }

        protected void txtChartOfAccountCode_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var box = o as RadComboBox;
            var coa = e.Text;
            if (coa.Length != 0)
            {
                box.Items.Clear();
                var coll = ChartOfAccounts.GetLike(coa, true, true);

                box.DataSource = coll;
                box.DataBind();
            }
        }

        protected void ddlSubLedger_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var entity = ((SubLedgers)e.Item.DataItem);
            e.Item.Attributes["SubLedgerName"] = entity.SubLedgerName;
        }

        protected void ddlSubLedger_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox box = o as RadComboBox;
            box.Items.Clear();

            string val = e.Text;

            string coa = txtChartOfAccountCode.Text;
            var entity = ChartOfAccounts.Get(coa);
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

        public int TemplateDetailId
        {
            get
            {
                int retVal = 0;
                int.TryParse(lblDetailId.Text, out retVal);
                return retVal;
            }
            set
            {
                this.lblDetailId.Text = value.ToString();
            }
        }

        public int ChartOfAccountId
        {
            get
            {
                int retVal = 0;
                int.TryParse(txtChartOfAccountCode.SelectedValue, out retVal);
                return retVal;
            }
        }

        public string ChartOfAccountCode
        {
            get { return this.txtChartOfAccountCode.Text; }
            set { this.txtChartOfAccountCode.Text = value; }
        }

        public string ChartOfAccountName
        {
            get { return this.txtChartOfAccountName.Text; }
            set { this.txtChartOfAccountName.Text = value; }
        }

        public int SubLedgerId
        {
            get
            {
                int retVal = 0;
                int.TryParse(this.ddlSubLedger.SelectedValue, out retVal);
                return retVal;
            }
            set { this.ddlSubLedger.SelectedValue = value.ToString(); }
        }

        public string SubLedgerName
        {
            get { return this.ddlSubLedger.Text; }
            set { this.ddlSubLedger.Text = value; }
        }

        public decimal AmountVariablePctg
        {
            get { return System.Convert.ToDecimal(txtAmountVariablePctg.Value ?? 0); }
            set { this.txtAmountVariablePctg.Value = System.Convert.ToDouble(value); }
        }

        public decimal AmountFixed
        {
            get { return System.Convert.ToDecimal(txtAmountFixed.Value ?? 0); }
            set { this.txtAmountFixed.Value = System.Convert.ToDouble(value); }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(false)) return;

            if (string.IsNullOrEmpty(this.ChartOfAccountCode))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid chart of account id.";
                return;
            }

            var entity = ChartOfAccounts.Get(this.ChartOfAccountCode);
            if (entity != null && entity.SubLedgerId.HasValue && entity.SubLedgerId > 0)
            {
                if (this.SubLedgerId == 0)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Subledger can not be empty.";
                }
            }
        }
    }
}