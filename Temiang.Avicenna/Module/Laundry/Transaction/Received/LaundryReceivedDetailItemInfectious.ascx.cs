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
    public partial class LaundryReceivedDetailItemInfectious : BaseUserControl
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
            cboSRItemUnit.Enabled = false;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (LaundryReceivedItemInfectiousCollection)Session["collLaundryReceivedItemInfectious" + Request.UserHostName];
                if (coll.Count == 0)
                    txtReceivedSeqNo.Text = "001";
                else
                {
                    var sequenceNo = (coll.OrderByDescending(c => c.ReceivedSeqNo).Select(c => c.ReceivedSeqNo)).Take(1);
                    int seqNo = int.Parse(sequenceNo.Single()) + 1;
                    txtReceivedSeqNo.Text = string.Format("{0:000}", seqNo);
                }

                cboSRItemUnit.SelectedValue = AppSession.Parameter.ItemUnitKg;

                return;
            }
            ViewState["IsNewRecord"] = false;
            txtReceivedSeqNo.Text = (String)DataBinder.Eval(DataItem, LaundryReceivedItemInfectiousMetadata.ColumnNames.ReceivedSeqNo);
            PopulateCboItemID(cboItemID, (String)DataBinder.Eval(DataItem, "ItemName"));
            cboItemID.Text = (String)DataBinder.Eval(DataItem, "ItemName");
            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, LaundryReceivedItemInfectiousMetadata.ColumnNames.ItemID);
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, LaundryReceivedItemInfectiousMetadata.ColumnNames.Qty));
            cboSRItemUnit.SelectedValue = (String)DataBinder.Eval(DataItem, LaundryReceivedItemInfectiousMetadata.ColumnNames.SRItemUnit);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, LaundryReceivedItemInfectiousMetadata.ColumnNames.Notes);
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
            var qrItem = new ItemLinenQuery("a");
            qrItem.es.Top = 1;
            qrItem.Where(qrItem.ItemID == cboItemID.SelectedValue);
            var item = new ItemLinen();
            if (!item.Load(qrItem))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Selected item not valid, please select exist item";
                return;
            }
        }

        #region Properties for return entry value
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
            var query = new ItemLinenQuery("a");
            query.Select(query.ItemID, query.ItemName);
            query.Where(
                query.ItemName.Like(searchTextContain),
                query.IsActive == true);

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
        #endregion
    }
}