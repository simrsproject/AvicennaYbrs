using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance
{
    public partial class CoaEditBkuDialog : BasePageDialog
    {
        private int ChartOfAccountID {
            get {
                return System.Convert.ToInt32(Request.QueryString["coaid"]);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var coa = new ChartOfAccounts();
                if (coa.LoadByPrimaryKey(ChartOfAccountID)) {
                    lblCoa.Text = string.Format("{0} - {1}", coa.ChartOfAccountCode, coa.ChartOfAccountName);
                    if ((coa.BkuAccountID ?? 0) != 0) {
                        var coaBku = new ChartOfAccounts();
                        if (coaBku.LoadByPrimaryKey(coa.BkuAccountID ?? 0)) {
                            cboBkuAccount_ItemsRequested(cboBkuAccount, new RadComboBoxItemsRequestedEventArgs() { Text = coaBku.ChartOfAccountCode });
                            cboBkuAccount.SelectedValue = coaBku.ChartOfAccountId.Value.ToString();
                        }
                    }
                }
            }
        }

        protected void cboBkuAccount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboBkuAccount_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var coa = new ChartOfAccountsQuery();
            coa.es.Top = 20;
            coa.Where(coa.Or(coa.ChartOfAccountCode.Like(searchText), coa.ChartOfAccountName.Like(searchText)), coa.IsDetail == true, coa.IsActive == true);
            coa.OrderBy(coa.ChartOfAccountId.Ascending);
            cboBkuAccount.DataSource = coa.LoadDataTable();
            cboBkuAccount.DataBind();
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
           
            var coa = new ChartOfAccounts();
            if (coa.LoadByPrimaryKey(ChartOfAccountID))
            {
                if (!string.IsNullOrEmpty(cboBkuAccount.SelectedValue))
                {
                    coa.BkuAccountID = System.Convert.ToInt32(cboBkuAccount.SelectedValue);
                }
                else {
                    coa.BkuAccountID = null;
                }
                coa.Save();
            }
            return true;
        }
    }
}
