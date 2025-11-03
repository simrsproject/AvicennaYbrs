using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class GuarantorItemRestrictionsDetail : BaseUserControl
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

            var iq = new ItemQuery();
            iq.Where(iq.ItemID ==
                     (String)DataBinder.Eval(DataItem, GuarantorItemRestrictionsMetadata.ColumnNames.ItemID));
            cboItemID.DataSource = iq.LoadDataTable();
            cboItemID.DataBind();
            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, GuarantorItemRestrictionsMetadata.ColumnNames.ItemID);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (GuarantorItemRestrictionsCollection)Session["collGuarantorItemRestrictions"];

                string itemID = cboItemID.SelectedValue;
                bool isExist = false;
                foreach (GuarantorItemRestrictions item in coll)
                {
                    if (item.ItemID.Equals(itemID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", itemID);
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

        #endregion

        #region Method & Event TextChanged

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery();
            query.Select(query.ItemID, query.ItemName);
            query.Where
                (query.SRItemType.In(ItemType.Medical, ItemType.NonMedical), query.IsActive == true,
                 query.Or
                     (
                         query.ItemID.Like(searchTextContain),
                         query.ItemName.Like(searchTextContain)
                     )
                );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboItemID.DataSource = dtb;
            cboItemID.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        #endregion
    }
}