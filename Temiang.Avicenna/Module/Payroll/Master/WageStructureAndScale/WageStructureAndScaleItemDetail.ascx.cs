using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class WageStructureAndScaleItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadTextBox TxtItemID
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtItemID"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            txtItemID.ReadOnly = true;
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (AppStandardReferenceItemCollection)Session["collWageStructureAndScaleTypeItem"];
                if (coll.Count == 0)
                {
                    txtItemID.Text = TxtItemID.Text.Trim() + ".01";
                }
                else
                {
                    int prefix = (TxtItemID.Text.Trim() + ".").Length;
                    
                    var itemIdMax = (coll.OrderByDescending(c => c.ItemID).Select(c => c.ItemID.Substring(prefix, 2))).Take(1);
                    int itemId = int.Parse(itemIdMax.Single()) + 1;
                    txtItemID.Text = TxtItemID.Text.Trim() + "." + string.Format("{0:00}", itemId);
                }

                return;
            }

            ViewState["IsNewRecord"] = false;

            txtItemID.Text = Convert.ToString(DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemID));
            txtItemName.Text = Convert.ToString(DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemName));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (AppStandardReferenceItemCollection)Session["collWageStructureAndScaleTypeItem"];

                string id = txtItemID.Text;
                bool isExist = false;
                foreach (AppStandardReferenceItem item in coll)
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
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value
        public string ItemID
        {
            get { return txtItemID.Text; }
        }
        public string ItemName
        {
            get { return txtItemName.Text; }
        }
        #endregion
    }
}