using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.CashManagement.CashEntryV2
{
    public partial class CashEntryItemDetail : BaseUserControl
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
                var desc = "-";
                try
                {
                    desc = Session["CashTransaction::Description"] as string;
                }
                catch { }

                if (!string.IsNullOrEmpty(desc)) txtDescription.Text = desc;

                this.lblChartOfAccountCode.Visible = false;
                this.lblChartOfAccountName.Visible = false;

                this.txtChartOfAccountCode.Visible = true;
                this.txtChartOfAccountName.Visible = true;

                return;
            }

            this.DetailId = (int)DataBinder.Eval(DataItem, "DetailId");
            this.ChartOfAccountCode = (string)DataBinder.Eval(DataItem, "ChartOfAccountCode");
            
            //this.LoadSubLedger(this.ChartOfAccountCode);
            var subLs = new SubLedgersCollection();
            var subL = SubLedgers.Get((int)DataBinder.Eval(DataItem, "SubLedgerId"));
            ddlSubLedger.Items.Clear();
            if (subL != null)
            {
                subLs.Add(subL);
            }
            ddlSubLedger.DataSource = subLs;
            ddlSubLedger.DataBind();
            if (subL != null) {
                this.SubLedgerId = (int)DataBinder.Eval(DataItem, "SubLedgerId");
            }


            this.ChartOfAccountName = (string)DataBinder.Eval(DataItem, "ChartOfAccountName");
            
            this.Description = (string)DataBinder.Eval(DataItem, "Description");
            this.Amount = (decimal)DataBinder.Eval(DataItem, "Amount");
            this.IsParentRefference = (bool)DataBinder.Eval(DataItem, "IsParentRefference");

            if (!string.IsNullOrEmpty((string)DataBinder.Eval(DataItem, "ListId")))
            {
                txtCashList.Items.Clear();
                CashTransactionListCollection coll = CashTransactionList.GetLike((string)DataBinder.Eval(DataItem, "ListId"), null);

                txtCashList.DataSource = coll;
                txtCashList.DataBind();
                this.CashListId = (string)DataBinder.Eval(DataItem, "ListId");

                CashTransactionList entity = CashTransactionList.Get(this.CashListId);
                this.lblCashList.Text = entity.Description;
                this.txtCashList.Visible = false;
                this.lblCashList.Visible = true;
            }
            else
            {
                this.txtCashList.Visible = false;
                this.lblCashList.Visible = false; 
            }

            this.lblChartOfAccountCode.Text = this.ChartOfAccountCode;
            this.txtChartOfAccountCode.Visible = false;
            this.lblChartOfAccountCode.Visible = true;

            this.lblChartOfAccountName.Text = this.ChartOfAccountName;
            this.txtChartOfAccountName.Visible = false;
            this.lblChartOfAccountName.Visible = true;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if ((txtAmount.Value ?? 0) == 0) {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Value can not be 0.";
                return;
            }
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

        #region Method & Event TextChanged
        protected void Page_Init(object sender, EventArgs e)
        {
            trCashList.Visible = (AppSession.Parameter.HealthcareInitialAppsVersion == "RSUSKY");

            Helper.SetupComboBox(txtChartOfAccountCode);
            Helper.SetupComboBox(txtCashList);
            Helper.SetupComboBox(ddlSubLedger);

            txtChartOfAccountCode.TextChanged += txtChartOfAccountCode_TextChanged;
            txtChartOfAccountCode.ItemDataBound += txtChartOfAccountCode_ItemDataBound;
            txtChartOfAccountCode.ItemsRequested += txtChartOfAccountCode_ItemsRequested;

            txtCashList.ItemsRequested += txtCashList_ItemsRequested;
            txtCashList.ItemDataBound += txtCashList_ItemDataBound;
            txtCashList.TextChanged += txtCashList_TextChanged;

            ddlSubLedger.TextChanged += ddlSubLedger_TextChanged;
            ddlSubLedger.ItemDataBound += ddlSubLedger_ItemDataBound;
            ddlSubLedger.ItemsRequested += ddlSubLedger_ItemsRequested;
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        void txtCashList_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox box = o as RadComboBox;
            string val = e.Text;
            if (val.Length != 0)
            {
                box.Items.Clear();
                CashTransactionListCollection coll = CashTransactionList.GetLike(val, true);

                box.DataSource = coll;
                box.DataBind();
            }
        }

        void txtCashList_TextChanged(object sender, EventArgs e)
        {
            string code = txtCashList.SelectedValue;
            CashTransactionList entity = CashTransactionList.Get(code);
            if (entity != null)
            {
                txtCashList.SelectedValue = entity.ListId;
                txtDescription.Text = txtCashList.Text;

                if (entity.ChartOfAccountId != null || entity.ChartOfAccountId != 0)
                {
                    var coa = ChartOfAccounts.Get(Convert.ToInt16(entity.ChartOfAccountId));
                    txtChartOfAccountCode.SelectedValue = entity.ChartOfAccountId.ToString();
                    txtChartOfAccountCode.Text = coa.ChartOfAccountCode;
                    txtChartOfAccountName.Text = coa.ChartOfAccountName;

                    if (entity.SubledgerId != 0)
                    {
                        var subLs = new SubLedgersCollection();
                        var subL = SubLedgers.Get(Convert.ToInt16(entity.SubledgerId));
                        ddlSubLedger.Items.Clear();//.ClearSelection();
                        if (subL != null)
                        {
                            subLs.Add(subL);
                        }
                        ddlSubLedger.DataSource = subLs;
                        ddlSubLedger.DataBind();
                    }
                    else
                    {
                        ddlSubLedger.Items.Clear();
                        //LoadSubLedger(coa);
                        
                    }
                }
                else
                {
                    txtChartOfAccountCode.SelectedIndex = 0;
                    txtChartOfAccountName.Text = string.Empty;
                }
            }
            else
            {
                txtCashList.SelectedValue = string.Empty;
            }
        }

        void txtCashList_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var entity = ((CashTransactionList)e.Item.DataItem);
            e.Item.Attributes["Description"] = entity.Description;
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

                    // 
                    var bankColl = new BankCollection();
                    bankColl.Query.Where(bankColl.Query.ChartOfAccountId.Equal(entity.ChartOfAccountId));
                    if (bankColl.LoadAll()) {
                        chkIsParentRefference.Enabled = true;
                        if (bankColl.First().IsCrossRefference ?? false) {
                            chkIsParentRefference.Checked = true;
                        }
                    }
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

        //private void LoadSubLedger(ChartOfAccounts entity)
        //{
        //    ddlSubLedger.Items.Clear();
        //    ddlSubLedger.Items.Add(new RadComboBoxItem("", "0"));

        //    if (entity.SubLedgerId.HasValue && entity.SubLedgerId.Value != 0)
        //        foreach (var slEntity in SubLedgers.GetByGroupId(entity.SubLedgerId.Value))
        //            ddlSubLedger.Items.Add(new RadComboBoxItem(string.Format("{0} - {1}", slEntity.SubLedgerName, slEntity.Description), slEntity.SubLedgerId.Value.ToString()));
        //}

        //private void LoadSubLedger(string chartOfAccountCode)
        //{
        //    var entity = ChartOfAccounts.Get(chartOfAccountCode);
        //    LoadSubLedger(entity);
        //}

        
        #endregion

        #region Properties for return entry value
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

        public string CashListId
        {
            get { return this.txtCashList.SelectedValue; }
            set { this.txtCashList.SelectedValue = value; }
        }

        public int ChartOfAccountId
        {
            get 
            {
                int retVal = 0;
                int.TryParse(txtChartOfAccountCode.SelectedValue, out retVal);
                return retVal;
            }
            set {
                var coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(value);
                var ea = new RadComboBoxItemsRequestedEventArgs();
                ea.Text = coa.ChartOfAccountCode;
                txtChartOfAccountCode_ItemsRequested(txtChartOfAccountCode, ea);
                txtChartOfAccountCode.SelectedValue = value.ToString();
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

        public string Description
        {
            get { return this.txtDescription.Text; }
            set { this.txtDescription.Text = value; }
        }
        public decimal Amount
        {
            get { return decimal.Parse(this.txtAmount.Text); }
            set { this.txtAmount.Text = value.ToString(); }
        }

        public bool IsParentRefference
        {
            get { return chkIsParentRefference.Checked; }
            set { chkIsParentRefference.Checked = value; }
        }
        #endregion
    }
}