using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class SalaryComponentRuleMatrixDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSROperandType, AppEnum.StandardReference.OperandType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtSalaryComponentRuleMatrixID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtSalaryComponentRuleMatrixID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SalaryComponentRuleMatrixMetadata.ColumnNames.SalaryComponentRuleMatrixID));
            PopulatecboSalaryComponetID(cboSalaryComponetID, (String)DataBinder.Eval(DataItem, "SalaryComponentName"));
            cboSROperandType.SelectedValue = (String)DataBinder.Eval(DataItem, SalaryComponentRuleMatrixMetadata.ColumnNames.SROperandType);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                SalaryComponentRuleMatrixCollection coll =
                    (SalaryComponentRuleMatrixCollection)Session["collSalaryComponentRuleMatrix"];

                //TODO: Betulkan cara pengecekannya
                string id = txtSalaryComponentRuleMatrixID.Text;
                bool isExist = false;
                foreach (SalaryComponentRuleMatrix item in coll)
                {
                    if (item.SalaryRuleComponentID.Equals(id))
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

        #region Properties for return entry value
        public Int32 SalaryComponentRuleMatrixID
        {
            get { return Convert.ToInt32(txtSalaryComponentRuleMatrixID.Text); }
        }
        public Int32 SalaryRuleComponentID
        {
            get { return Convert.ToInt32(cboSalaryComponetID.SelectedValue); }
        }
        public string SalaryComponentName
        {
            get { return cboSalaryComponetID.Text; }
        }
        
        public String SROperandType
        {
            get { return cboSROperandType.SelectedValue; }
        }
        public String OperandTypeName
        {
            get { return cboSROperandType.Text; }
        }

        #endregion

        #region Method & Event TextChanged
        #endregion

        #region ComboBox

        protected void cboSalaryComponetID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboSalaryComponetID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboSalaryComponetID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            SalaryComponentQuery query = new SalaryComponentQuery();

            query.Where
                 (
                     query.Or
                         (
                             query.SalaryComponentCode.Like(searchTextContain),
                             query.SalaryComponentName.Like(searchTextContain)
                         )
                 );

            query.Select(query.SalaryComponentID, query.SalaryComponentCode, query.SalaryComponentName);
            query.OrderBy(query.SalaryComponentCode.Ascending);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Items.Add(new RadComboBoxItem("0", ""));
                comboBox.SelectedValue = dtb.Rows[0]["SalaryComponentID"].ToString();
            }
        }
        protected void cboSalaryComponetID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SalaryComponentCode"].ToString().Trim() + "-" + ((DataRowView)e.Item.DataItem)["SalaryComponentName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SalaryComponentID"].ToString();
        }

        #endregion
    }
}
