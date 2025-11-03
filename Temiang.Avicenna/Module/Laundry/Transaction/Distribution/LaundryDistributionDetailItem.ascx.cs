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

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaundryDistributionDetailItem : BaseUserControl
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

                var coll = (LaundryDistributionItemCollection)Session["collLaundryDistributionItem" + Request.UserHostName];
                if (coll.Count == 0)
                    txtSeqNo.Text = "001";
                else
                {
                    var sequenceNo = (coll.OrderByDescending(c => c.SeqNo).Select(c => c.SeqNo)).Take(1);
                    int seqNo = int.Parse(sequenceNo.Single()) + 1;
                    txtSeqNo.Text = string.Format("{0:000}", seqNo);
                }

                return;
            }
            ViewState["IsNewRecord"] = false;
            txtSeqNo.Text = (String)DataBinder.Eval(DataItem, LaundryDistributionItemMetadata.ColumnNames.SeqNo);
            PopulateCboItemID(cboItemID, (String)DataBinder.Eval(DataItem, "ItemName"));
            cboItemID.Text = (String)DataBinder.Eval(DataItem, "ItemName");
            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, LaundryDistributionItemMetadata.ColumnNames.ItemID);
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, LaundryDistributionItemMetadata.ColumnNames.Qty));

            var i = new Item();
            if (i.LoadByPrimaryKey(cboItemID.SelectedValue))
                ComboBox.PopulateWithItemUnit(cboSRItemUnit, cboItemID.SelectedValue, i.SRItemType);
            else
                ComboBox.PopulateWithAllItemUnit(cboSRItemUnit);

            cboSRItemUnit.SelectedValue = (String)DataBinder.Eval(DataItem, LaundryDistributionItemMetadata.ColumnNames.SRItemUnit);
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
                var coll = (LaundryDistributionItemCollection)Session["collLaundryDistributionItem" + Request.UserHostName];

                string itemId = cboItemID.SelectedValue;

                if (coll.Any(row => row.ItemID.Equals(itemId)))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item {0} has exist", cboItemID.Text);
                    return;
                }
            }

            //Check Entry ItemID
            var qrItem = new ItemQuery("a");
            var qrProduct = new ItemProductNonMedicQuery("b");
            qrItem.InnerJoin(qrProduct).On(qrItem.ItemID == qrProduct.ItemID);
            qrItem.es.Top = 1;
            qrItem.Where(qrItem.ItemID == cboItemID.SelectedValue, qrProduct.IsNeedToBeLaundered == true);
            var item = new Item();
            if (!item.Load(qrItem))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Selected item not valid, please select exist item";
                return;
            }
        }

        #region Properties for return entry value
        public String SeqNo
        {
            get { return txtSeqNo.Text; }
        }
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
        public String ItemUnitName
        {
            get { return cboSRItemUnit.Text; }
        }
        #endregion

        #region Method & Event TextChanged
        #endregion

        #region ComboBox ItemID
        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboItemID((RadComboBox)sender, e.Text);
        }
        private void PopulateCboItemID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new ItemQuery("a");
            var nmedic = new ItemProductNonMedicQuery("b");
            query.InnerJoin(nmedic).On(query.ItemID == nmedic.ItemID);
            query.Select(query.ItemID, query.ItemName);
            query.Where(
                query.ItemName.Like(searchTextContain),
                query.IsActive == true,
                nmedic.IsNeedToBeLaundered == true);

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
            {
                ComboBox.PopulateWithItemBaseUnit(cboSRItemUnit, e.Value, i.SRItemType);
                var nm = new ItemProductNonMedic();
                if (nm.LoadByPrimaryKey(e.Value))
                    cboSRItemUnit.SelectedValue = nm.SRItemUnit;
            }

            else
                ComboBox.PopulateWithAllItemUnit(cboSRItemUnit);
        }
        #endregion
    }
}