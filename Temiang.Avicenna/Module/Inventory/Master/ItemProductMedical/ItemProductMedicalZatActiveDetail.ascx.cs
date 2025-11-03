using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ItemProductMedicalZatActiveDetail : BaseUserControl
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
                return;
            }
            ViewState["IsNewRecord"] = false;
            ComboBox.PopulateWithOneZatActive(cboZatActiveID, (String)DataBinder.Eval(DataItem, ItemProductMedicZatActiveMetadata.ColumnNames.ZatActiveID));
            chkIsPrinted.Checked = (bool) DataBinder.Eval(DataItem, ItemProductMedicZatActiveMetadata.ColumnNames.IsPrinted);
        }

        protected void cboZatActiveID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ZatActiveName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ZatActiveID"].ToString();
        }

        protected void cboZatActiveID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataTable tbl = LoadItem(e.Text);
            cboZatActiveID.DataSource = tbl;
            cboZatActiveID.DataBind();
        }

        private DataTable LoadItem(string searchText)
        {
            string searchTextContain = string.Format("%{0}%", searchText);
            var query = new ZatActiveQuery("a");
            query.es.Top = 30;
            query.Select
                (
                    query.ZatActiveID,
                    query.ZatActiveName
                );
            query.Where(
                query.IsActive == true,
                query.Or(
                    query.ZatActiveName.Like(searchTextContain),
                    query.ZatActiveID.Like(searchTextContain)
                    )
                );
            query.OrderBy(query.ZatActiveName.Ascending);

            return query.LoadDataTable();
        }

        #region Properties for return entry value

        public String ZatActiveID
        {
            get { return cboZatActiveID.SelectedValue; }
        }

        public String ZatActiveName
        {
            get { return cboZatActiveID.Text; }
        }

        public Boolean IsPrinted
        {
            get { return chkIsPrinted.Checked; }
        }

        #endregion

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                ItemProductMedicZatActiveCollection coll = (ItemProductMedicZatActiveCollection)Session["collItemProductMedicZatActiveCollection"];

                string ZatActiveID = cboZatActiveID.SelectedValue;
                bool isExist = false;
                foreach (ItemProductMedicZatActive item in coll)
                {
                    if (item.ZatActiveID.Equals(ZatActiveID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", ZatActiveID);
                }
            }
        }
    }
}
