using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Cssd.Master
{
    public partial class ItemDetailItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private CheckBox chkIsItemProduction
        {
            get
            { return (CheckBox)Helper.FindControlRecursive(Page, "chkIsItemProduction"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            txtQty.Enabled = chkIsItemProduction.Checked;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtQty.Value = 1;

                return;
            }
            ViewState["IsNewRecord"] = false;

            ComboBox.PopulateWithOneItem(cboItemID, (String)DataBinder.Eval(DataItem, CssdItemDetailMetadata.ColumnNames.ItemDetailID));
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, CssdItemDetailMetadata.ColumnNames.Qty));

            var i = new Item();
            if (i.LoadByPrimaryKey(cboItemID.SelectedValue))
                ComboBox.PopulateWithItemBaseUnit(cboSRCssdItemUnit, cboItemID.SelectedValue, i.SRItemType);
            else
                ComboBox.PopulateWithAllItemUnit(cboSRCssdItemUnit);
            cboSRCssdItemUnit.SelectedIndex = 1;

        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQty.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity Must Bigger than 0");
                return;
            }

            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll =
                    (CssdItemDetailCollection)Session["collCssdItemDetail"];

                string itemId = cboItemID.SelectedValue;

                if (coll.Any(row => row.ItemDetailID.Equals(itemId)))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item {0} has exist", cboItemID.Text);
                }
            }
        }

        #region Properties for return entry value
        public String ItemDetailID
        {
            get { return cboItemID.SelectedValue; }
        }
        public String ItemDetailName
        {
            get { return cboItemID.Text; }
        }
        public decimal Qty
        {
            get { return Convert.ToDecimal(txtQty.Value); }
        }
        public String SrItemUnit
        {
            get { return cboSRCssdItemUnit.SelectedValue; }
        }
        #endregion

        #region Method & Event TextChanged

        #endregion

        #region ComboBox ItemID
        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }
        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var i = new Item();
            if (i.LoadByPrimaryKey(e.Value))
                ComboBox.PopulateWithItemBaseUnit(cboSRCssdItemUnit, e.Value, i.SRItemType);
            else
                ComboBox.PopulateWithAllItemUnit(cboSRCssdItemUnit);
            cboSRCssdItemUnit.SelectedIndex = 1;
        }
        #endregion

        #region ComboBox ServiceUnitID
        protected void cboServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboServiceUnitID((RadComboBox)sender, e.Text);
        }

        private void PopulateCboServiceUnitID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new ServiceUnitQuery("a");
            query.Where(
                query.SRRegistrationType.In(AppConstant.RegistrationType.InPatient,
                                            AppConstant.RegistrationType.OutPatient,
                                            AppConstant.RegistrationType.EmergencyPatient,
                                            AppConstant.RegistrationType.ClusterPatient,
                                            AppConstant.RegistrationType.MedicalCheckUp),
                query.IsActive == true,
                query.ServiceUnitName.Like(searchTextContain));
            query.Select(query.ServiceUnitID, query.ServiceUnitName);

            query.es.Top = 10;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["ServiceUnitID"].ToString();
            }
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        #endregion
    }
}