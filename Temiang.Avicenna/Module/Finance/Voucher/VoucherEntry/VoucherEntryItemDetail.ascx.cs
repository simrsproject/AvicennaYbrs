using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry
{
    public partial class VoucherEntryItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            //PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.Accounts, txtDocumentNumber);

            if (DataItem is GridInsertionObject)
            {
                string desc = Session["JournalTransaction::Description"] as string;
                string reff = Session["JournalTransaction::RefferenceNo"] as string;
                if (!string.IsNullOrEmpty(desc))
                    txtDescription.Text = desc;
                if (!string.IsNullOrEmpty(reff))
                    txtDocumentNumber.Text = reff;
                    

                //this.lblChartOfAccountCode.Visible = false;
                //this.lblChartOfAccountName.Visible = false;

                this.txtChartOfAccountCode.Visible = true;
                this.txtChartOfAccountName.Visible = true;

                //Session.Remove("JournalTransaction::Description");
                return;
            }

            this.DetailId = (int)DataBinder.Eval(DataItem, "DetailId");
            this.ChartOfAccountCode = (string)DataBinder.Eval(DataItem, "ChartOfAccountCode");
            this.LoadSubLedger(this.ChartOfAccountCode);
            this.ChartOfAccountName = (string)DataBinder.Eval(DataItem, "ChartOfAccountName");
            if (ddlSubLedger.FindItemByValue(DataBinder.Eval(DataItem, "SubLedgerId").ToString()) != null)
            {
                this.SubLedgerId = (int)DataBinder.Eval(DataItem, "SubLedgerId");
            }
            this.DocumentNumber = (string)DataBinder.Eval(DataItem, "DocumentNumber");
            this.Description = (string)DataBinder.Eval(DataItem, "Description");
            //edit
            this.RegistrationNo = (string)DataBinder.Eval(DataItem, "RegistrationNo");
            this.Debit = (decimal)DataBinder.Eval(DataItem, "Debit");
            this.Credit = (decimal)DataBinder.Eval(DataItem, "Credit");

            this.txtChartOfAccountCode.Text = this.ChartOfAccountCode;
            //this.txtChartOfAccountCode.Visible = false;
            //this.lblChartOfAccountCode.Visible = true;

            this.txtChartOfAccountName.Text = this.ChartOfAccountName;
            //this.txtChartOfAccountName.Visible = false;
            //this.lblChartOfAccountName.Visible = true;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(this.ChartOfAccountCode))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid chart of account id.";
                return;
            }


            ChartOfAccounts entity = ChartOfAccounts.Get(this.ChartOfAccountCode);
            if (entity != null && entity.SubLedgerId.HasValue && entity.SubLedgerId > 0)
                {
                    if (this.SubLedgerId == 0)
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = "Subledger can not be empty.";
                    }
                }
            if (entity != null && entity.IsControlDocNumber == true)
            {
                if (string.IsNullOrEmpty(this.DocumentNumber))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Reference can not be empty";
                }
            }
            
        }

        #region Method & Event TextChanged
        protected void Page_Init(object sender, EventArgs e)
        {
            Helper.SetupComboBox(txtChartOfAccountCode);

            this.txtChartOfAccountCode.TextChanged += new EventHandler(txtChartOfAccountCode_TextChanged);
            this.txtChartOfAccountCode.ItemDataBound += new RadComboBoxItemEventHandler(txtChartOfAccountCode_ItemDataBound);
            this.txtChartOfAccountCode.ItemsRequested += new RadComboBoxItemsRequestedEventHandler(txtChartOfAccountCode_ItemsRequested);
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void txtChartOfAccountCode_TextChanged(object sender, EventArgs e)
        {
            txtChartOfAccountName.Text = string.Empty;

            string coa = txtChartOfAccountCode.Text;
            ChartOfAccounts entity = ChartOfAccounts.Get(coa);
            if (entity != null)
            {
                if ((bool)entity.IsDetail /*&& entity.AccountLevel == 4*/)
                {
                    txtChartOfAccountName.Text = entity.ChartOfAccountName;

                    LoadSubLedger(entity);
                }
                else
                {
                    txtChartOfAccountName.Text = string.Empty;
                }
            }
        }

        protected void txtChartOfAccountCode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ChartOfAccounts entity = ((ChartOfAccounts)e.Item.DataItem);
            e.Item.Attributes["ChartOfAccountName"] = entity.ChartOfAccountName;
        }

        protected void txtChartOfAccountCode_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox box = o as RadComboBox;
            string coa = e.Text;
            if (coa.Length != 0)
            {
                box.Items.Clear();
                ChartOfAccountsCollection coll = ChartOfAccounts.GetLike(coa, true, true);

                box.DataSource = coll;
                box.DataBind();
            }
        }

        private void LoadSubLedger(ChartOfAccounts entity)
        {
            ddlSubLedger.Text = string.Empty;
            ddlSubLedger.SelectedValue = string.Empty;
            ddlSubLedger.Items.Clear();
            ddlSubLedger.Items.Add(new RadComboBoxItem("", "0"));

            if (entity.SubLedgerId.HasValue && entity.SubLedgerId.Value != 0)
                foreach (SubLedgers slEntity in SubLedgers.GetByGroupId(entity.SubLedgerId.Value))
                    ddlSubLedger.Items.Add(new RadComboBoxItem(string.Format("{0} - {1}", slEntity.SubLedgerName, slEntity.Description), slEntity.SubLedgerId.Value.ToString()));
        }

        private void LoadSubLedger(string chartOfAccountCode)
        {
            ChartOfAccounts entity = ChartOfAccounts.Get(chartOfAccountCode);
            LoadSubLedger(entity);
        }
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

        public int ChartOfAccountId
        {
            get 
            {
                int retVal = 0;

                string coa = txtChartOfAccountCode.Text;
                if (string.IsNullOrEmpty(txtChartOfAccountCode.SelectedValue.Trim()))
                {
                    if (coa.Length != 0)
                    {
                        this.txtChartOfAccountCode.Items.Clear();
                        ChartOfAccountsCollection coll = ChartOfAccounts.GetLike(coa, true, true);

                        this.txtChartOfAccountCode.DataSource = coll;
                        this.txtChartOfAccountCode.DataBind();

                        if (txtChartOfAccountCode.Items.Count == 1)
                            this.txtChartOfAccountCode.Items[0].Selected = true;
                    }
                }
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
        public string DocumentNumber
        {
            get { return this.txtDocumentNumber.Text; }
            set { this.txtDocumentNumber.Text = value; }
        }
        public string Description
        {
            get { return this.txtDescription.Text; }
            set { this.txtDescription.Text = value; }
        }
        public string RegistrationNo
        {
            get { return this.txtRegistrationNo.Text; }
            set { this.txtRegistrationNo.Text = value; }
        }
        public decimal Debit
        {
            get { return decimal.Parse(this.txtDebit.Text); }
            set { this.txtDebit.Text = value.ToString(); }
        }
        public decimal Credit
        {
            get { return decimal.Parse(this.txtCredit.Text); }
            set { this.txtCredit.Text = value.ToString(); }
        }
        #endregion
    }
}