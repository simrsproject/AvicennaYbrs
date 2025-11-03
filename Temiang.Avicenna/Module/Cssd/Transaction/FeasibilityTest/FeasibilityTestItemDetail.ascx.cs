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
    public partial class FeasibilityTestItemDetail : BaseUserControl
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

            cboItemID.Enabled = false;
            txtQty.ReadOnly = true;
            cboSRCssdItemUnit.Enabled = false;
            txtNotes.ReadOnly = true;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;
            
            var receivedNo = (String)DataBinder.Eval(DataItem, CssdFeasibilityTestItemMetadata.ColumnNames.ReceivedNo);
            txtReceivedSeqNo.Text = (String)DataBinder.Eval(DataItem, CssdFeasibilityTestItemMetadata.ColumnNames.ReceivedSeqNo);

            var received = new CssdSterileItemsReceivedItem();
            if (received.LoadByPrimaryKey(receivedNo, txtReceivedSeqNo.Text))
            {
                PopulateCboItemID(cboItemID, received.ItemID, false);
                cboItemID.SelectedValue = received.ItemID;
                txtQty.Value = Convert.ToDouble(received.Qty);

                var i = new Item();
                if (i.LoadByPrimaryKey(cboItemID.SelectedValue))
                {
                    ComboBox.PopulateWithItemUnit(cboSRCssdItemUnit, cboItemID.SelectedValue, i.SRItemType);
                    chkIsItemProduction.Checked = i.IsItemProduction ?? false;
                }
                else
                {
                    ComboBox.PopulateWithAllItemUnit(cboSRCssdItemUnit);
                    chkIsItemProduction.Checked = false;
                }
                if (chkIsItemProduction.Checked)
                {
                    chkIsBrokenInstrument.Enabled = false;
                    txtQtyReplacements.ReadOnly = true;
                }

                cboSRCssdItemUnit.SelectedValue = received.SRCssdItemUnit;
                txtNotes.Text = received.Notes;
            }

            chkIsFeasibilityTestPassed.Checked = (bool)DataBinder.Eval(DataItem, CssdFeasibilityTestItemMetadata.ColumnNames.IsFeasibilityTestPassed);
            chkIsBrokenInstrument.Checked = (bool)DataBinder.Eval(DataItem, CssdFeasibilityTestItemMetadata.ColumnNames.IsBrokenInstrument);
            txtQtyReplacements.Value = Convert.ToDouble(DataBinder.Eval(DataItem, CssdFeasibilityTestItemMetadata.ColumnNames.QtyReplacements));
            txtQtyReplacements.MaxValue = Convert.ToDouble(DataBinder.Eval(DataItem, CssdSterileItemsReceivedItemMetadata.ColumnNames.Qty));
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQtyReplacements.Value > 0 && !chkIsBrokenInstrument.Checked)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Broken Instrument must be checked.");
                return;
            }
        }

        #region Properties for return entry value
        public Boolean IsFeasibilityTestPassed
        {
            get { return chkIsFeasibilityTestPassed.Checked; }
        }
        public Boolean IsBrokenInstrument
        {
            get { return chkIsBrokenInstrument.Checked; }
        }
        public Decimal QtyReplacements
        {
            get { return Convert.ToDecimal(txtQtyReplacements.Value); }
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
            query.Select(query.ItemID, query.ItemName);
            query.Where(query.Or(query.ItemID == textSearch,
                query.ItemName.Like(searchTextContain)));
            if (isNew)
                query.Where(query.IsActive == true, query.IsNeedToBeSterilized == true);

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
                ComboBox.PopulateWithItemUnit(cboSRCssdItemUnit, e.Value, i.SRItemType);
                chkIsItemProduction.Checked = i.IsItemProduction ?? false;
            }
            else
            {
                ComboBox.PopulateWithAllItemUnit(cboSRCssdItemUnit);
                chkIsItemProduction.Checked = false;
            }
            cboSRCssdItemUnit.SelectedIndex = 1;
            if (chkIsItemProduction.Checked)
            {
                txtQty.ReadOnly = true;
                txtQty.Value = 1;
            }
        }
        #endregion
    }
}