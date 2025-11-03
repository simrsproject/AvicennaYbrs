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
    public partial class SterilizationProcessItemDetail : BaseUserControl
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
            StandardReference.InitializeIncludeSpace(cboSRCssdItemUnit, AppEnum.StandardReference.ItemUnit);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (CssdSterilizationProcessItemCollection)Session["collCssdSterilizationProcessItem"];
                if (coll.Count == 0)
                    txtProcessSeqNo.Text = "001";
                else
                {
                    var sequenceNo = (coll.OrderByDescending(c => c.ReceivedSeqNo).Select(c => c.ReceivedSeqNo)).Take(1);
                    int seqNo = int.Parse(sequenceNo.Single()) + 1;
                    txtProcessSeqNo.Text = string.Format("{0:000}", seqNo);
                }

                return;
            }
            ViewState["IsNewRecord"] = false;
            cboItemID.Enabled = false;
            txtProcessSeqNo.Text = (String)DataBinder.Eval(DataItem, CssdSterilizationProcessItemMetadata.ColumnNames.ProcessSeqNo);
            txtReceivedNo.Text =
                (String)DataBinder.Eval(DataItem, CssdSterilizationProcessItemMetadata.ColumnNames.ReceivedNo);
            txtReceivedSeqNo.Text =
                (String)DataBinder.Eval(DataItem, CssdSterilizationProcessItemMetadata.ColumnNames.ReceivedSeqNo);
            PopulateCboItemID(cboItemID, (String)DataBinder.Eval(DataItem, "ItemName"), false);
            cboItemID.Text = (String)DataBinder.Eval(DataItem, "ItemName");

            var ri = new CssdSterileItemsReceivedItem();
            if (ri.LoadByPrimaryKey(txtReceivedNo.Text, txtReceivedSeqNo.Text))
            {
                cboItemID.SelectedValue = ri.ItemID;
                cboSRCssdItemUnit.SelectedValue = ri.SRCssdItemUnit;
                txtItemNo.Text = ri.CssdItemNo.ToInt().ToString();
            }
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, CssdSterilizationProcessItemMetadata.ColumnNames.Qty));
            txtWeight.Value = Convert.ToDouble(DataBinder.Eval(DataItem, CssdSterilizationProcessItemMetadata.ColumnNames.Weight));

            PopulateQtyProcessed(TxtProcessNo.Text, txtProcessSeqNo.Text, txtReceivedNo.Text, txtReceivedSeqNo.Text);

        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQty.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity Must Bigger than 0");
                return;
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
        public String ProcessSeqNo
        {
            get { return txtProcessSeqNo.Text; }
        }
        public String ReceivedNo
        {
            get { return txtReceivedNo.Text; }
        }
        public String ReceivedSeqNo
        {
            get { return txtReceivedSeqNo.Text; }
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
        public String SRCssdItemUnit
        {
            get { return cboSRCssdItemUnit.SelectedValue; }
        }
        public String CssdItemUnitName
        {
            get { return cboSRCssdItemUnit.Text; }
        }
        public Decimal Weight
        {
            get { return Convert.ToDecimal(txtWeight.Value); }
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

            query.Where(query.ItemName.Like(searchTextContain));
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

        private void PopulateQtyProcessed(string processNo, string processSeqNo, string receivedNo, string receivedSeqNo)
        {
            var received = new CssdSterileItemsReceivedItem();
            double qtyReceived = received.LoadByPrimaryKey(receivedNo, receivedSeqNo) ? Convert.ToDouble(received.Qty ?? 0) : 0;

            DataTable dtb = (new CssdSterilizationProcessItemCollection()).GetQtyProcessed(processNo, processSeqNo, receivedNo, receivedSeqNo);
            double qtyProcessed = dtb.Rows.Count > 0 ? Convert.ToDouble(dtb.Rows[0]["Qty"]) : 0;

            txtQty.MaxValue = qtyReceived - qtyProcessed;
        }
        #endregion
    }
}