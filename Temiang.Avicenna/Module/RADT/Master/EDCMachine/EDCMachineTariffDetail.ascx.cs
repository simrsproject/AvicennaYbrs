using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class EDCMachineTariffDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRCardType, AppEnum.StandardReference.CardType);	

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                chkIsChargedToPatient.Checked = false;
                chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            cboSRCardType.SelectedValue = (String)DataBinder.Eval(DataItem, EDCMachineTariffMetadata.ColumnNames.SRCardType);
            txtEDCMachineTariff.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EDCMachineTariffMetadata.ColumnNames.EDCMachineTariff));
            txtAddFeeAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EDCMachineTariffMetadata.ColumnNames.AddFeeAmount));
            chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, EDCMachineTariffMetadata.ColumnNames.IsActive);
            chkIsChargedToPatient.Checked = ((bool?)DataBinder.Eval(DataItem, EDCMachineTariffMetadata.ColumnNames.IsChargedToPatient)) ?? false;

            int? chartOfAccountId = (int?)DataBinder.Eval(DataItem, EDCMachineTariffMetadata.ColumnNames.ChartOfAccountId);
            int? subLedgerId = (int?)DataBinder.Eval(DataItem, EDCMachineTariffMetadata.ColumnNames.SubledgerID);

            if (chartOfAccountId.HasValue && chartOfAccountId != 0)
            {
                ChartOfAccountsQuery coaQ = new ChartOfAccountsQuery();
                coaQ.Select(coaQ.ChartOfAccountId, coaQ.ChartOfAccountCode, coaQ.ChartOfAccountName);
                coaQ.Where(coaQ.ChartOfAccountId == chartOfAccountId);
                DataTable dtbCoa = coaQ.LoadDataTable();
                cboChartOfAccountId.DataSource = dtbCoa;
                cboChartOfAccountId.DataBind();
                cboChartOfAccountId.SelectedValue = chartOfAccountId.ToString();
                if (subLedgerId.HasValue && subLedgerId != 0)
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

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                EDCMachineTariffCollection coll = (EDCMachineTariffCollection)Session["collEDCMachineTariff"];

                string id = cboSRCardType.SelectedValue;
                bool isExist = false;
                foreach (EDCMachineTariff item in coll)
                {
                    if (item.SRCardType.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }

                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
                }
            }
        }

        protected void cboChartOfAccountId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboChartOfAccountId_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            RadComboBox o = (RadComboBox)sender;
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            ), query.IsDetail == true
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            o.DataSource = dtb;
            o.DataBind();
        }

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

        #region Properties for return entry value

        public String SRCardType
        {
            get { return cboSRCardType.SelectedValue; }
        }

        public String CardTypeName
        {
            get { return cboSRCardType.Text; }
        }

        public Decimal EDCMachineTariff
        {
            get { return Convert.ToDecimal(txtEDCMachineTariff.Value); }
        }

        public Decimal AddFeeAmount
        {
            get { return Convert.ToDecimal(txtAddFeeAmount.Value); }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        public Boolean IsChargedToPatient
        {
            get { return chkIsChargedToPatient.Checked; }
        }

        public int CoaId
        {
            get { return Convert.ToInt32(string.IsNullOrEmpty(cboChartOfAccountId.SelectedValue) ? "0":cboChartOfAccountId.SelectedValue); }
        }

        public int SlId
        {
            get { return Convert.ToInt32(string.IsNullOrEmpty(cboSubledgerId.SelectedValue) ? "0" : cboSubledgerId.SelectedValue); }
        }

        #endregion
    }
}
