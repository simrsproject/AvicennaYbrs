using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance
{
    public partial class BkuDetailItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            rblJenis.SelectedValue = "D";
            txtNominal.Value = 0;

            TransaksiBkuDetailId = (Session["collTransaksiBkuDetail"] as TransaksiBkuDetailCollection).Count + 1;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            TransaksiBkuDetailId = Convert.ToInt32(DataBinder.Eval(DataItem, TransaksiBkuDetailMetadata.ColumnNames.Id));

            var coaId = Convert.ToInt32(DataBinder.Eval(DataItem, TransaksiBkuDetailMetadata.ColumnNames.KodeRekening));
            var coa = new ChartOfAccounts();
            coa.LoadByPrimaryKey(coaId);

            cboKodeRekening_ItemsRequested(null, new RadComboBoxItemsRequestedEventArgs() { Text = coa.ChartOfAccountCode });
            cboKodeRekening.SelectedValue = coaId.ToString();

            rblJenis.SelectedValue = DataBinder.Eval(DataItem, TransaksiBkuDetailMetadata.ColumnNames.Posisi).ToString();
            txtNominal.Value = Convert.ToInt32(DataBinder.Eval(DataItem, TransaksiBkuDetailMetadata.ColumnNames.Nominal));

            var itemId = DataBinder.Eval(DataItem, TransaksiBkuDetailMetadata.ColumnNames.KodeItem).ToString();
            if (!string.IsNullOrWhiteSpace(itemId))
            {
                cboItem_ItemsRequested(null, new RadComboBoxItemsRequestedEventArgs() { Text = itemId });
                cboItem.SelectedValue = itemId.ToString();
            }

            txtMemo.Text = (String)DataBinder.Eval(DataItem, TransaksiBkuDetailMetadata.ColumnNames.Memo);
        }

        protected void cboKodeRekening_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboKodeRekening_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var coa = new ChartOfAccountsQuery();
            coa.es.Top = 20;
            coa.Where(coa.Or(coa.ChartOfAccountCode.Like(searchTextContain), coa.ChartOfAccountName.Like(searchTextContain)), coa.IsDetail == true, coa.IsActive == true, coa.IsBku == true);
            coa.OrderBy(coa.ChartOfAccountId.Ascending);
            cboKodeRekening.DataSource = coa.LoadDataTable();
            cboKodeRekening.DataBind();
        }

        protected void cboItem_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItem_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var coa = new ItemQuery();
            coa.es.Top = 20;
            coa.Where(coa.Or(coa.ItemID == e.Text, coa.ItemName.Like(searchTextContain)), coa.IsActive == true);
            coa.OrderBy(coa.ItemName.Ascending);
            cboItem.DataSource = coa.LoadDataTable();
            cboItem.DataBind();
        }

        public int TransaksiBkuDetailId
        {
            get; set;
        }

        public int KodeRekening
        {
            get
            {
                return cboKodeRekening.SelectedValue.ToInt();
            }
        }

        public string ChartOfAccountCode
        {
            get
            {
                return cboKodeRekening.Text;
            }
        }

        public string KodeItem
        {
            get
            {
                return cboItem.SelectedValue;
            }
        }

        public string ItemName
        {
            get
            {
                return cboItem.Text;
            }
        }

        public string Posisi
        {
            get
            {
                return rblJenis.SelectedValue;
            }
        }

        public decimal Nominal
        {
            get
            {
                return Convert.ToDecimal(txtNominal.Value);
            }
        }

        public string Memo
        {
            get
            {
                return txtMemo.Text;
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {

        }
    }
}