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
    public partial class WageStructureAndScaleJobPositionDialogItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadTextBox TxtSubGroupID
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtSubGroupID"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            txtItemID.ReadOnly = true;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (AppStandardReferenceItemCollection)Session["collEmployeeJobPosition"];
                if (coll.Count == 0)
                {
                    txtItemID.Text = TxtSubGroupID.Text.Trim() + ".01";
                }
                else
                {
                    int prefix = (TxtSubGroupID.Text.Trim() + ".").Length;

                    var itemIdMax = (coll.OrderByDescending(c => c.ItemID).Select(c => c.ItemID.Substring(prefix, 2))).Take(1);
                    int itemId = int.Parse(itemIdMax.Single()) + 1;
                    txtItemID.Text = TxtSubGroupID.Text.Trim() + "." + string.Format("{0:00}", itemId);
                }

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtItemID.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemID);
            txtItemName.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemName);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (AppStandardReferenceItemCollection)Session["collEmployeeJobPosition"];

                string id = txtItemID.Text;
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.ItemName.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID : {0} already exist", id);
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

        #endregion
    }
}