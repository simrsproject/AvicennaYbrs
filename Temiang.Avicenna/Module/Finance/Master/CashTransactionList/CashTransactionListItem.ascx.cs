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
    public partial class CashTransactionListItem : BaseUserControl
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

                this.lblChartOfAccountCode.Visible = false;
                this.lblChartOfAccountName.Visible = false;

                this.txtChartOfAccountCode.Visible = true;
                this.txtChartOfAccountName.Visible = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            this.DetailId = (int)DataBinder.Eval(DataItem, "ListItemID");

            var coa = ChartOfAccounts.Get((int)DataBinder.Eval(DataItem, "ChartOfAccountId"));
            this.ChartOfAccountCode = coa.ChartOfAccountCode;
            this.ChartOfAccountName = coa.ChartOfAccountName;

            var subLs = new SubLedgersCollection();
            var subL = SubLedgers.Get((int)DataBinder.Eval(DataItem, "SubLedgerId"));
            if (subL != null) subLs.Add(subL);
            ddlSubLedger.DataSource = subLs;
            ddlSubLedger.DataBind();
            if (subL != null) this.SubLedgerId = (int)DataBinder.Eval(DataItem, "SubLedgerId");

            this.Amount = (decimal)DataBinder.Eval(DataItem, "Amount");

            this.lblChartOfAccountCode.Text = this.ChartOfAccountCode;
            this.txtChartOfAccountCode.Visible = false;
            this.lblChartOfAccountCode.Visible = true;

            this.lblChartOfAccountName.Text = this.ChartOfAccountName;
            this.txtChartOfAccountName.Visible = false;
            this.lblChartOfAccountName.Visible = true;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            Helper.SetupComboBox(txtChartOfAccountCode);
            Helper.SetupComboBox(ddlSubLedger);

            txtChartOfAccountCode.TextChanged += txtChartOfAccountCode_TextChanged;
            txtChartOfAccountCode.ItemDataBound += txtChartOfAccountCode_ItemDataBound;
            txtChartOfAccountCode.ItemsRequested += txtChartOfAccountCode_ItemsRequested;

            ddlSubLedger.TextChanged += ddlSubLedger_TextChanged;
            ddlSubLedger.ItemDataBound += ddlSubLedger_ItemDataBound;
            ddlSubLedger.ItemsRequested += ddlSubLedger_ItemsRequested;
        }

        protected void txtChartOfAccountCode_TextChanged(object sender, EventArgs e)
        {
            txtChartOfAccountName.Text = string.Empty;

            string coa = txtChartOfAccountCode.Text;
            var entity = ChartOfAccounts.Get(coa);
            if (entity != null)
            {
                if ((bool)entity.IsDetail /*&& entity.AccountLevel == 4*/)
                {
                    txtChartOfAccountName.Text = entity.ChartOfAccountName;

                    //LoadSubLedger(entity);
                }
                else
                {
                    txtChartOfAccountName.Text = string.Empty;
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

        protected void ddlSubLedger_TextChanged(object sender, EventArgs e)
        {

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

        public int DetailId
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

        public decimal Amount
        {
            get { return decimal.Parse(this.txtAmount.Text); }
            set { this.txtAmount.Text = value.ToString(); }
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