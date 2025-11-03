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
    public partial class SterileItemsReceivedItemDetail : BaseUserControl
    {
        private object _dataItem;
        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadTextBox TxtProductionNo
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtProductionNo"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            pnlAddInfo.Visible = !AppSession.Application.IsMenuCssdPackagingActive;
            trExpiredDate.Visible = AppSession.Parameter.IsCssdExpiredValidateInReceiveDetail;
            trOldIsDtt.Visible = AppSession.Parameter.IsCssdUsingDttTerm;
            trNewIsDtt.Visible = !AppSession.Parameter.IsCssdUsingDttTerm;
            rfvTemperature.Visible = trNewIsDtt.Visible;

            StandardReference.InitializeIncludeSpace(cboSRCssdItemUnit, AppEnum.StandardReference.ItemUnit);

            if (!string.IsNullOrEmpty(TxtProductionNo.Text))
            {
                cboItemID.Enabled = false;
                txtQty.ReadOnly = true;
                cboSRCssdItemUnit.Enabled = false;
            }
            
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (CssdSterileItemsReceivedItemCollection)Session["collCssdSterileItemsReceivedItem" + Request.UserHostName];
                if (coll.Count == 0)
                    txtReceivedSeqNo.Text = "001";
                else
                {
                    var sequenceNo = (coll.OrderByDescending(c => c.ReceivedSeqNo).Select(c => c.ReceivedSeqNo)).Take(1);
                    int seqNo = int.Parse(sequenceNo.Single()) + 1;
                    txtReceivedSeqNo.Text = string.Format("{0:000}", seqNo);
                }
                txtQty.Value = 1;

                return;
            }
            ViewState["IsNewRecord"] = false;
            cboItemID.Enabled = false;
            txtReceivedSeqNo.Text = (String)DataBinder.Eval(DataItem, CssdSterileItemsReceivedItemMetadata.ColumnNames.ReceivedSeqNo);
            PopulateCboItemID(cboItemID, (String)DataBinder.Eval(DataItem, "ItemName"), false);
            cboItemID.Text = (String)DataBinder.Eval(DataItem, "ItemName");
            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, CssdSterileItemsReceivedItemMetadata.ColumnNames.ItemID);
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, CssdSterileItemsReceivedItemMetadata.ColumnNames.Qty));

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
                txtQty.ReadOnly = true;
                cboSRCssdItemUnit.Enabled = false;
            }
                
            cboSRCssdItemUnit.SelectedValue = (String)DataBinder.Eval(DataItem, CssdSterileItemsReceivedItemMetadata.ColumnNames.SRCssdItemUnit);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, CssdSterileItemsReceivedItemMetadata.ColumnNames.Notes);
            object expiredDate = DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ExpiredDate);
            if (expiredDate != null)
                txtExpiredDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, CssdSterileItemsReceivedItemMetadata.ColumnNames.ExpiredDate);
            else
                txtExpiredDate.Clear();
            txtReuseTo.Value = Convert.ToDouble(DataBinder.Eval(DataItem, CssdSterileItemsReceivedItemMetadata.ColumnNames.ReuseTo));
            chkIsNeedUltrasound.Checked = (bool)DataBinder.Eval(DataItem, CssdSterileItemsReceivedItemMetadata.ColumnNames.IsNeedUltrasound);

            var isDtt = (bool)DataBinder.Eval(DataItem, CssdSterileItemsReceivedItemMetadata.ColumnNames.IsDtt);
            chkIsDtt.Checked = isDtt;
            rblIsDtt.SelectedValue = isDtt ? "1" : "0";
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
        public String Notes
        {
            get { return txtNotes.Text; }
        }
        public DateTime? ExpiredDate
        {
            get { return txtExpiredDate.SelectedDate; }
        }
        public Int16 ReuseTo
        {
            get { return Convert.ToInt16(txtReuseTo.Value); }
        }
        public Boolean IsNeedUltrasound
        {
            get { return chkIsNeedUltrasound.Checked; }
        }
        public Boolean IsDtt
        {
            get 
            {
                if (AppSession.Parameter.IsCssdUsingDttTerm)
                    return chkIsDtt.Checked;
                return (rblIsDtt.SelectedValue == "0" ? false : true);
            }
        }
        public Boolean IsItemProduction
        {
            get { return chkIsItemProduction.Checked; }
        }
        public Boolean IsNewRecord
        {
            get { return (bool)ViewState["IsNewRecord"]; }
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
            query.Where(
                query.ItemName.Like(searchTextContain));
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