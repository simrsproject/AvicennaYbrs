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
    public partial class PackagingItemDetailItem : BaseUserControl
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

            trOldIsDtt.Visible = AppSession.Parameter.IsCssdUsingDttTerm;
            trNewIsDtt.Visible = !AppSession.Parameter.IsCssdUsingDttTerm;
            rfvTemperature.Visible = trNewIsDtt.Visible;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            var receivedNo = (String)DataBinder.Eval(DataItem, CssdPackagingItemMetadata.ColumnNames.ReceivedNo);
            txtReceivedSeqNo.Text = (String)DataBinder.Eval(DataItem, CssdPackagingItemMetadata.ColumnNames.ReceivedSeqNo);

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

                cboSRCssdItemUnit.SelectedValue = received.SRCssdItemUnit;
                txtNotes.Text = received.Notes;
            }

            object expiredDate = DataBinder.Eval(DataItem, ItemTransactionItemMetadata.ColumnNames.ExpiredDate);
            if (expiredDate != null)
                txtExpiredDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, CssdPackagingItemMetadata.ColumnNames.ExpiredDate);
            else
                txtExpiredDate.Clear();
            txtReuseTo.Value = Convert.ToDouble(DataBinder.Eval(DataItem, CssdPackagingItemMetadata.ColumnNames.ReuseTo));
            chkIsNeedUltrasound.Checked = (bool)DataBinder.Eval(DataItem, CssdPackagingItemMetadata.ColumnNames.IsNeedUltrasound);

            var isDtt = (bool)DataBinder.Eval(DataItem, CssdPackagingItemMetadata.ColumnNames.IsDtt);
            chkIsDtt.Checked = isDtt;
            rblIsDtt.SelectedValue = isDtt ? "1" : "0";
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
        }

        #region Properties for return entry value
        
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
        }
        #endregion
    }
}