using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee
{
    public partial class ParamedicFeeAddDeducItem : BaseUserControl
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
                var coll = (ParamedicFeeAddDeducCoaItemCollection)Session["collParamedicFeeAddDeducCoaItem"];
                this.ListItemId = coll.Count + 1;
                return;
            }

            this.ListItemId = (int)DataBinder.Eval(DataItem, "ListItemId");

            txtChartOfAccountCode.Items.Clear();
            var coa = new ChartOfAccountsCollection();
            coa.Query.Where(coa.Query.ChartOfAccountId == (int)DataBinder.Eval(DataItem, "ChartOfAccountId"));
            coa.Query.Load();
            txtChartOfAccountCode.DataSource = coa;
            txtChartOfAccountCode.DataBind();
            this.ChartOfAccountId = (int)DataBinder.Eval(DataItem, "ChartOfAccountId");

            ddlSubLedger.Items.Clear();
            var subs = new SubLedgersCollection();
            subs.Query.Where(subs.Query.SubLedgerId == (int)DataBinder.Eval(DataItem, "SubLedgerId"));
            subs.Query.Load();
            ddlSubLedger.DataSource = subs;
            ddlSubLedger.DataBind();
            if (subs.Count > 0) this.SubLedgerId = (int)DataBinder.Eval(DataItem, "SubLedgerId");
            this.Amount = (decimal)DataBinder.Eval(DataItem, "Amount");
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {

        }

        #region Method & Event TextChanged

        protected void Page_Init(object sender, EventArgs e)
        {
            txtChartOfAccountCode.ItemDataBound += txtChartOfAccountCode_ItemDataBound;
            txtChartOfAccountCode.ItemsRequested += txtChartOfAccountCode_ItemsRequested;

            ddlSubLedger.ItemDataBound += ddlSubLedger_ItemDataBound;
            ddlSubLedger.ItemsRequested += ddlSubLedger_ItemsRequested;
        }

        protected void txtChartOfAccountCode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var entity = ((ChartOfAccounts)e.Item.DataItem);
            e.Item.Text = entity.ChartOfAccountCode + " - " + entity.ChartOfAccountName;
            e.Item.Value = entity.ChartOfAccountId.ToString();

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
            e.Item.Text = entity.SubLedgerName + " - " + entity.Description;
            e.Item.Value = entity.SubLedgerId.ToString();
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

        #endregion

        #region Properties for return entry value

        public int ListItemId
        {
            get
            {
                int retVal = 0;
                int.TryParse(ViewState["ListItemId"].ToString(), out retVal);
                return retVal;
            }
            set
            {
                ViewState["ListItemId"] = value.ToString();
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
            set { this.txtChartOfAccountCode.SelectedValue = value.ToString(); }
        }

        public string ChartOfAccountName
        {
            get { return txtChartOfAccountCode.Text; }
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
            get { return ddlSubLedger.Text; }
        }

        public decimal Amount
        {
            get { return Convert.ToDecimal(this.txtAmount.Value); }
            set { this.txtAmount.Value = Convert.ToDouble(value); }
        }

        #endregion
    }
}