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

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class DistributionDetailItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRCssdItemUnit, AppEnum.StandardReference.ItemUnit);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;
            cboItemID.Enabled = false;

            PopulateCboItemID(cboItemID, (String)DataBinder.Eval(DataItem, "ItemName"), false);
            cboItemID.Text = (String)DataBinder.Eval(DataItem, "ItemName");
            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, CssdDistributionItemMetadata.ColumnNames.ItemID);
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, CssdDistributionItemMetadata.ColumnNames.Qty));

            var i = new Item();
            if (i.LoadByPrimaryKey(cboItemID.SelectedValue))
            {
                ComboBox.PopulateWithItemUnit(cboSRCssdItemUnit, cboItemID.SelectedValue, i.SRItemType);
                var baseUnit = string.Empty;
                if (i.SRItemType == ItemType.Medical)
                {
                    var item = new ItemProductMedic();
                    item.LoadByPrimaryKey(i.ItemID);
                    baseUnit = item.SRItemUnit;
                }
                else if (i.SRItemType == ItemType.NonMedical)
                {
                    var item = new ItemProductNonMedic();
                    item.LoadByPrimaryKey(i.ItemID);
                    baseUnit = item.SRItemUnit;
                }
                else
                {
                    var item = new ItemKitchen();
                    item.LoadByPrimaryKey(i.ItemID);
                    baseUnit = item.SRItemUnit;
                }
                cboSRCssdItemUnit.SelectedValue = baseUnit;
            }
            else
                ComboBox.PopulateWithAllItemUnit(cboSRCssdItemUnit);
            
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
                var coll = (CssdDistributionItemCollection)Session["collCssdDistributionItem" + Request.UserHostName];

                string itemId = cboItemID.SelectedValue;

                if (coll.Any(row => row.ItemID.Equals(itemId)))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item {0} has exist", cboItemID.Text);
                    return;
                }
            }

            //Check Entry ItemID
            var qrItem = new ItemQuery();
            qrItem.es.Top = 1;
            qrItem.Where(qrItem.ItemID == cboItemID.SelectedValue, qrItem.IsNeedToBeSterilized == true);
            var item = new Item();
            if (!item.Load(qrItem))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Selected item not valid, please select exist item";
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
        public String CssdItemUnitName
        {
            get { return cboSRCssdItemUnit.Text; }
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
            var balanceq = new CssdItemBalanceQuery("b");
            query.InnerJoin(balanceq).On(balanceq.ServiceUnitID == AppSession.Parameter.ServiceUnitCssdID && balanceq.ItemID == query.ItemID);
            query.Select(query.ItemID, query.ItemName, balanceq.Balance);
            query.Where(
                query.ItemName.Like(searchTextContain));
            if (isNew)
                query.Where(query.IsActive == true, query.IsNeedToBeSterilized == true, balanceq.Balance > 0);

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
            var i = new Item();
            if (i.LoadByPrimaryKey(e.Value))
                ComboBox.PopulateWithItemUnit(cboSRCssdItemUnit, e.Value, i.SRItemType);
            else
                ComboBox.PopulateWithAllItemUnit(cboSRCssdItemUnit);
            cboSRCssdItemUnit.SelectedIndex = 1;
        }
        #endregion
    }
}