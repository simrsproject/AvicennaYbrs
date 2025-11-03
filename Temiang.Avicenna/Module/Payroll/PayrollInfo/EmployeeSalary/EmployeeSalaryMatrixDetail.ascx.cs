using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.PayrollInfo
{
    public partial class EmployeeSalaryMatrixDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private string PageId
        {
            get { return ((HiddenField)Helper.FindControlRecursive(Page, "hdnPageId")).Value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRCurrencyCode, AppEnum.StandardReference.Currency);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtEmployeeSalaryMatrixID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;
            cboSalaryComponentID.Enabled = false;

            txtEmployeeSalaryMatrixID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeSalaryMatrixMetadata.ColumnNames.EmployeeSalaryMatrixID));

            var scId = Convert.ToString(DataBinder.Eval(DataItem, EmployeeSalaryMatrixMetadata.ColumnNames.SalaryComponentID));
            PopulatecboSalaryComponentID(cboSalaryComponentID, scId, false);
            cboSalaryComponentID.SelectedValue = scId;
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeSalaryMatrixMetadata.ColumnNames.Qty));
            txtNominalAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeSalaryMatrixMetadata.ColumnNames.NominalAmount));
            cboSRCurrencyCode.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeSalaryMatrixMetadata.ColumnNames.SRCurrencyCode);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                EmployeeSalaryMatrixCollection coll =
                    (EmployeeSalaryMatrixCollection)Session["collEmployeeSalaryMatrix" + Request.UserHostName + PageId];

                //TODO: Betulkan cara pengecekannya
                int id = cboSalaryComponentID.SelectedValue.ToInt();

                bool isExist = false;
                foreach (EmployeeSalaryMatrix item in coll)
                {
                    if (item.SalaryComponentID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Salary Component Name: {0} has exist", cboSalaryComponentID.Text);
                }
            }
        }

        #region Properties for return entry value
        public Int64 EmployeeSalaryMatrixID
        {
            get { return Convert.ToInt64(txtEmployeeSalaryMatrixID.Text); }
        }
        public Int32 SalaryComponentID
        {
            get { return Convert.ToInt32(cboSalaryComponentID.SelectedValue); }
        }
        public string SalaryComponentName
        {
            get { return cboSalaryComponentID.Text; }
        }
        public Int32 Qty
        {
            get { return Convert.ToInt32(txtQty.Text); }
        }
        public Decimal NominalAmount
        {
            get { return Convert.ToDecimal(txtNominalAmount.Value); }
        }
        public String SRCurrencyCode
        {
            get { return cboSRCurrencyCode.SelectedValue; }
        }
        #endregion

        #region Method & Event TextChanged
        
        #endregion

        #region ComboBox ItemID
        protected void cboSalaryComponentID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboSalaryComponentID((RadComboBox)sender, e.Text, true);
        }
        private void PopulatecboSalaryComponentID(RadComboBox comboBox, string textSearch, bool isNew)
        {
            SalaryComponentQuery query = new SalaryComponentQuery("a");
            EmployeeSalaryMatrixQuery employee = new EmployeeSalaryMatrixQuery("b");
            query.LeftJoin(employee).On(query.SalaryComponentID == employee.SalaryComponentID);

            if (isNew)
            {
                string searchTextContain = string.Format("%{0}%", textSearch);
                query.Where(query.SalaryComponentName.Like(searchTextContain));
            }
            else
                query.Where(query.SalaryComponentID == textSearch.ToInt());

            query.Select(query.SalaryComponentID, query.SalaryComponentCode.RTrim(), query.SalaryComponentName);

            query.es.Top = 20;
            query.es.Distinct = true;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            //if (dtb.Rows.Count > 0)
            //{
            //    comboBox.SelectedValue = dtb.Rows[0]["SalaryComponentID"].ToString();
            //}
        }
        protected void cboSalaryComponentID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SalaryComponentName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SalaryComponentID"].ToString();
        }
        #endregion
    }
}
