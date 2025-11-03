using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master.AcctSubGroup
{
    public partial class SubLedgerDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            cboHoSubLedgerId.WebServiceSettings.Path = string.Format("{0}/WebService/ComboBoxDataService.asmx",
                ConfigurationManager.AppSettings["ConsolidationWebServiceUrlRoot"]);
        }

        protected override void OnDataBinding(EventArgs e)
        {
            string id = string.IsNullOrEmpty(Request.QueryString["id"]) ? "-1" : Request.QueryString["id"];
            bool isProfitLossGroup = id == AppSession.Parameter.SubLedgerGroupIdServiceUnit;

            trDirectCost.Visible = isProfitLossGroup && AppSession.Parameter.acc_IsJournalPayrollWithDirectIndirectCost;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                chkIsDirectCost.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtSubLedgerName.Text = (String)DataBinder.Eval(DataItem, SubLedgersMetadata.ColumnNames.SubLedgerName);
            txtDescription.Text = (String)DataBinder.Eval(DataItem, SubLedgersMetadata.ColumnNames.Description);
            cboHoSubLedgerId.Items.Clear();
            var hoSubLedgerId =  DataBinder.Eval(DataItem, SubLedgersMetadata.ColumnNames.HoSubLedgerId).ToInt();
            if (hoSubLedgerId > 0)
            {
                cboHoSubLedgerId.Items.Add(new RadComboBoxItem(
                    (String) DataBinder.Eval(DataItem, SubLedgersMetadata.ColumnNames.HoDescription),
                    hoSubLedgerId.ToString()));
                cboHoSubLedgerId.SelectedIndex = 0;
            }
            try {
                chkIsDirectCost.Checked = (bool)DataBinder.Eval(DataItem, SubLedgersMetadata.ColumnNames.IsDirectCost);
            }
            catch
            {
                chkIsDirectCost.Checked = true;
            }
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ////Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                SubLedgersCollection coll =
                    (SubLedgersCollection)Session["collSubLedgers"];

                string name = txtSubLedgerName.Text;
                bool isExist = false;
                foreach (SubLedgers item in coll)
                {
                    if (item.SubLedgerName.Equals(name))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Sub Ledger Name: {0} has exist", name);
                }
            }
        }

        #region Properties for return entry value
        public String SubLedgerName
        {
            get { return txtSubLedgerName.Text; }
        }
        public String Description
        {
            get { return txtDescription.Text; }
        }
        public int HoSubLedgerId
        {
            get { return cboHoSubLedgerId.SelectedValue.ToInt(); }
        }
        public string HoDescription
        {
            get { return cboHoSubLedgerId.Text; }
        }
        public bool IsDirectCost
        {
            get { return chkIsDirectCost.Checked; }
        }
        #endregion
    }
}