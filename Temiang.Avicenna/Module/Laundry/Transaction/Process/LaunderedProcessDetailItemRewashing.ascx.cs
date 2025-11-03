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
    public partial class LaunderedProcessDetailItemRewashing : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadTextBox TxtProcessNo
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtProcessNo"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRItemUnit, AppEnum.StandardReference.ItemUnit);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (LaunderedProcessItemRewashingCollection)Session["collLaunderedProcessItemRewashing" + Request.UserHostName];
                if (coll.Count == 0)
                    txtProcessSeqNo.Text = "001";
                else
                {
                    var sequenceNo = (coll.OrderByDescending(c => c.ProcessSeqNo).Select(c => c.ProcessSeqNo)).Take(1);
                    int seqNo = int.Parse(sequenceNo.Single()) + 1;
                    txtProcessSeqNo.Text = string.Format("{0:000}", seqNo);
                }

                return;
            }
            ViewState["IsNewRecord"] = false;
            txtProcessSeqNo.Text = (String)DataBinder.Eval(DataItem, LaunderedProcessItemRewashingMetadata.ColumnNames.ProcessSeqNo);
            PopulateCboItemID(cboItemID, (String)DataBinder.Eval(DataItem, "ItemName"));
            cboItemID.Text = (String)DataBinder.Eval(DataItem, "ItemName");
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, LaunderedProcessItemRewashingMetadata.ColumnNames.Qty));

            var i = new Item();
            if (i.LoadByPrimaryKey(cboItemID.SelectedValue))
                ComboBox.PopulateWithItemUnit(cboSRItemUnit, cboItemID.SelectedValue, i.SRItemType);
            else
                ComboBox.PopulateWithAllItemUnit(cboSRItemUnit);

            cboSRItemUnit.SelectedValue = (String)DataBinder.Eval(DataItem, LaunderedProcessItemRewashingMetadata.ColumnNames.SRItemUnit);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, LaunderedProcessItemRewashingMetadata.ColumnNames.Notes);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQty.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity Must Bigger than 0");
                return;
            }
        }

        #region Properties for return entry value
        public String ProcessSeqNo
        {
            get { return txtProcessSeqNo.Text; }
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
        public String Notes
        {
            get { return txtNotes.Text; }
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
            var nm = new ItemProductNonMedicQuery("b");
            query.InnerJoin(nm).On(query.ItemID == nm.ItemID);

            query.Where(
                query.ItemName.Like(searchTextContain),
                query.IsActive == true, nm.IsNeedToBeLaundered == true);

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