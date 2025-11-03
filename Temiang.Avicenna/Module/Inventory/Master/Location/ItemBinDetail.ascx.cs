using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ItemBinDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private string PageId
        {
            get { return ((HiddenField)Helper.FindControlRecursive(Page, "hdnPageId")).Value; }
        }

        private RadTextBox TxtLocationId
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtLocationID"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                if (AppSession.Parameter.IsItemBinIdAutoCreate)
                {
                    txtItemID.ReadOnly = true;
                    var coll = (AppStandardReferenceItemCollection)Session["collAppStandardReferenceItem_ItemBin" + PageId];
                    if (coll.Count == 0)
                    {
                        txtItemID.Text = TxtLocationId.Text.Trim() + ".001";
                    }
                    else
                    {
                        int prefix = (TxtLocationId.Text.Trim() + ".").Length;
                        //txtItemID.Text = TxtLocationId.Text.Trim() + "." + string.Format("{0:000}", int.Parse(coll[coll.Count - 1].ItemID.Substring(prefix, 3)) + 1);

                        var itemIdMax = (coll.OrderByDescending(c => c.ItemID).Select(c => c.ItemID.Substring(prefix, 3))).Take(1);
                        int itemId = int.Parse(itemIdMax.Single()) + 1;
                        txtItemID.Text = TxtLocationId.Text.Trim() + "." + string.Format("{0:000}", itemId);
                    }
                }

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtItemID.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemID);
            txtItemName.Text = (String)(DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemName));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (AppStandardReferenceItemCollection)Session["collAppStandardReferenceItem_ItemBin" + PageId];

                string stdId = AppEnum.StandardReference.ItemBin.ToString();
                string id = txtItemID.Text;
                bool isExist = false;
                foreach (AppStandardReferenceItem row in coll)
                {
                    if (row.ItemID.Equals(id) & row.StandardReferenceID.Equals(stdId))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value
        public String ItemID
        {
            get { return txtItemID.Text; }
        }
        public String ItemName
        {
            get { return txtItemName.Text; }
        }
        public String ReferenceID
        {
            get { return TxtLocationId.Text; }
        }
        #endregion
    }
}