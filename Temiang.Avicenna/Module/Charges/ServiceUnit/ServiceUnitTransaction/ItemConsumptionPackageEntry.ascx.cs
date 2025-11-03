using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ItemConsumptionPackageEntry : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private string PageId
        {
            get { return Request.QueryString["pageId"].ToString(); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }
            
            ViewState["IsNewRecord"] = false;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = ((TransChargesItemConsumptionCollection)Session["collTransChargesItemConsumption" + Request.UserHostName + PageId]).Where(i => i.TransactionNo == Request.QueryString["trans"] &&
                                                                                                                 i.SequenceNo.Substring(0, 3) == Request.QueryString["seq"]);

                string detailItemID = cboDetailItemID.SelectedValue;
                bool isExist = false;
                foreach (TransChargesItemConsumption item in coll)
                {
                    if (item.DetailItemID.Equals(detailItemID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Detail Item ID: {0} has exist", detailItemID);
                }
            }
        }

        #region Properties for return entry value
        public String DetailItemID
        {
            get { return cboDetailItemID.SelectedValue; }
        }
        public String DetailItemName
        {
            get { return cboDetailItemID.Text; }
        }
        public Decimal? Qty
        {
            get { return Convert.ToDecimal(txtQty.Value); }
        }
        public String SRItemUnit
        {
            get { return txtSRItemUnit.Text; }
        }
        #endregion

        #region cboItemID

        protected void cboDetailItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery("a");
            var prodmedQ = new VwItemProductMedicNonMedicQuery("b");

            query.es.Top = 20;
            query.InnerJoin(prodmedQ).On(query.ItemID == prodmedQ.ItemID);
            query.Where(query.Or(query.ItemID.Like(searchTextContain),
                                 query.ItemName.Like(searchTextContain)));
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            cboDetailItemID.DataSource = query.LoadDataTable();
            cboDetailItemID.DataBind();
        }

        protected void cboDetailItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboDetailItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var item = new Item();
            if (!item.LoadByPrimaryKey(e.Value))
            {
                cboDetailItemID.Text = string.Empty;
                return;
            }

            PopulateItemUnit(item.ItemID, item.SRItemType);
        }

        private void PopulateItemUnit(string itemID, string itemType)
        {
            if (BusinessObject.Reference.ItemType.Medical.Equals(itemType))
            {
                var item = new ItemProductMedic();
                if (item.LoadByPrimaryKey(itemID))
                    txtSRItemUnit.Text = item.SRItemUnit;
            }
            else
            {
                var item = new ItemProductNonMedic();
                if (item.LoadByPrimaryKey(itemID))
                    txtSRItemUnit.Text = item.SRItemUnit;
            }
        }

        #endregion
    }
}