using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Transaction
{
    public partial class UpdateSalaryItemDetail : BaseUserControl
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

                //TODO: Inisialisasi control untuk new row
                
                txtWageTransactionItemID.Value = 1;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtWageTransactionItemID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, WageTransactionItemMetadata.ColumnNames.WageTransactionItemID));
            PopulatecboSalaryComponentID(cboSalaryComponentID, (String)DataBinder.Eval(DataItem, "SalaryComponentName"));
            txtNominalAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, WageTransactionItemMetadata.ColumnNames.NominalAmount));
            txtSRCurrencyCode.Text = Convert.ToString(DataBinder.Eval(DataItem, WageTransactionItemMetadata.ColumnNames.SRCurrencyCode));
            txtCurrencyRate.Value = Convert.ToDouble(DataBinder.Eval(DataItem, WageTransactionItemMetadata.ColumnNames.CurrencyRate));
            txtCurrencyAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, WageTransactionItemMetadata.ColumnNames.CurrencyAmount));
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
        }

        #region Properties for return entry value
        public Int64 WageTransactionItemID
        {
            get { return Convert.ToInt64(txtWageTransactionItemID.Text); }
        }
        public Int32 SalaryComponentID
        {
            get { return Convert.ToInt32(cboSalaryComponentID.SelectedValue); }
        }
        public string SalaryComponentName
        {
            get { return cboSalaryComponentID.Text; }
        }
        public Decimal NominalAmount
        {
            get { return Convert.ToDecimal(txtNominalAmount.Value); }
        }
        public Decimal CurrencyAmount
        {
            get { return Convert.ToDecimal(txtCurrencyAmount.Value); }
        }
        
        #endregion

        #region Method & Event TextChanged

        #endregion

        #region ComboBox ItemID
        protected void cboSalaryComponentID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboSalaryComponentID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboSalaryComponentID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new SalaryComponentQuery("a");
            var employee = new WageTransactionItemQuery("b");
            query.LeftJoin(employee).On(query.SalaryComponentID == employee.SalaryComponentID);

            query.Where(query.SalaryComponentName.Like(searchTextContain));

            query.Select(query.SalaryComponentID, query.SalaryComponentCode.RTrim(), query.SalaryComponentName);

            query.es.Top = 20;
            query.es.Distinct = true;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["SalaryComponentID"].ToString();
            }
        }
        protected void cboSalaryComponentID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SalaryComponentName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SalaryComponentID"].ToString();
        }
        #endregion

        protected void txtNominalAmount_TextChanged(object sender, EventArgs e)
        {
            txtCurrencyAmount.Value = txtNominalAmount.Value*1;
        }

    }
}