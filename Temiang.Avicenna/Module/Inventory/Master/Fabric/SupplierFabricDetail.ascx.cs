using System;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class SupplierFabricDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.Supplier, txtSupplierID);

            if (DataItem is GridInsertionObject)
            {
                chkIsActive.Checked = true;

                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtSupplierID.Text = (String)DataBinder.Eval(DataItem, SupplierFabricMetadata.ColumnNames.SupplierID);
            Supplier s = new Supplier();
            if (s.LoadByPrimaryKey(txtSupplierID.Text))
                lblSupplierName.Text = s.SupplierName;

            chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, SupplierFabricMetadata.ColumnNames.IsActive);
        }

        #region Properties for return entry value

        public String SupplierID
        {
            get { return txtSupplierID.Text; }
        }

        public String SupplierName
        {
            get { return lblSupplierName.Text; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        #endregion

        protected void txtSupplierID_TextChanged(object sender, EventArgs e)
        {
            Supplier s = new Supplier();
            if (s.LoadByPrimaryKey(txtSupplierID.Text))
                lblSupplierName.Text = s.SupplierName;
            else
            {
                txtSupplierID.Text = string.Empty;
                lblSupplierName.Text = string.Empty;
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                SupplierFabricCollection coll = (SupplierFabricCollection)Session["collSupplierFabric"];

                string itemID = txtSupplierID.Text;
                bool isExist = false;
                foreach (SupplierFabric item in coll)
                {
                    if (item.SupplierID.Equals(itemID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", itemID);
                }
            }
        }
    }
}
