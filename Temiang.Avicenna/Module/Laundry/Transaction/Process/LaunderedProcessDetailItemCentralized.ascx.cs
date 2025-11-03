using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaunderedProcessDetailItemCentralized : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRItemUnit, AppEnum.StandardReference.ItemUnit);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;
            PopulateCboItemID(cboItemID, (String)DataBinder.Eval(DataItem, "ItemName"), false);
            cboItemID.Text = (String)DataBinder.Eval(DataItem, "ItemName");
            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, LaunderedProcessItemCentralizationMetadata.ColumnNames.ItemID);
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, LaunderedProcessItemCentralizationMetadata.ColumnNames.Qty));
            var item = new ItemProductNonMedic();
            if (item.LoadByPrimaryKey(cboItemID.SelectedValue))
                cboSRItemUnit.SelectedValue = item.SRItemUnit;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (LaunderedProcessItemCentralizationCollection)Session["collLaunderedProcessItemCentralization" + Request.UserHostName];

                bool isExist = coll.Any(cons => (cons.ItemID.Equals(cboItemID.SelectedValue)));

                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item: {0} has exist", cboItemID.Text);
                    return;
                }
            }

            if (txtQty.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity Must Bigger than 0");
                return;
            }
        }

        #region Properties for return entry value
        public String ItemID
        {
            get { return cboItemID.SelectedValue; }
        }
        public String ItemName
        {
            get { return cboItemID.Text; }
        }
        public Decimal Qty
        {
            get { return Convert.ToDecimal(txtQty.Value); }
        }
        public String SRItemUnit
        {
            get { return cboSRItemUnit.SelectedValue; }
        }
        public String ItemUnit
        {
            get { return cboSRItemUnit.Text; }
        }

        #endregion

        #region Method & Event TextChanged
        #endregion

        #region ComboBox ItemID
        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboItemID((RadComboBox)sender, e.Text, true);
        }
        private void PopulateCboItemID(RadComboBox comboBox, string textSearch, bool isNew)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new ItemQuery("a");
            var nm = new ItemProductNonMedicQuery("b");
            query.InnerJoin(nm).On(query.ItemID == nm.ItemID);

            query.Where(
                query.ItemName.Like(searchTextContain));

            if (isNew)
                query.Where(query.IsActive == true, nm.IsNeedToBeLaundered == true); 

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var nm = new ItemProductNonMedic();
            if (nm.LoadByPrimaryKey(e.Value))
                cboSRItemUnit.SelectedValue = nm.SRItemUnit;
            else
            {
                cboSRItemUnit.SelectedValue = string.Empty;
                cboSRItemUnit.Text = string.Empty;
            }
                
        }
        #endregion
    }
}