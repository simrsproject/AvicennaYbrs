using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class VisitPackageItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            cboItemID.Enabled = false;
            var itemq = new ItemQuery();
            itemq.Where(itemq.ItemID == (String)DataBinder.Eval(DataItem, VisitPackageItemMetadata.ColumnNames.ItemID));
            cboItemID.DataSource = itemq.LoadDataTable();
            cboItemID.DataBind();
            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, VisitPackageItemMetadata.ColumnNames.ItemID);
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, VisitPackageItemMetadata.ColumnNames.Qty));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (VisitPackageItemCollection)Session["collVisitPackageItem"];

                string id = cboItemID.SelectedValue;
                bool isExist = false;
                foreach (VisitPackageItem item in coll)
                {
                    if (item.ItemID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", id);
                }
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

        #endregion

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery();

            query.Where
                (
                query.SRItemType.In(ItemType.Service, ItemType.Laboratory, ItemType.Radiology),
                query.Or
                    (
                        query.ItemName.Like(searchTextContain),
                        query.ItemID.Like(searchTextContain)
                    ),
                query.IsActive == true
                );

            query.OrderBy(query.ItemName.Ascending);
            query.es.Top = 20;

            cboItemID.DataSource = query.LoadDataTable();
            cboItemID.DataBind();
        }
        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

    }
}