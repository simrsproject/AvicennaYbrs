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
    public partial class WageStructureAndScaleWorGroupItemDetail : BaseUserControl
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
            txtWorkSubGroupID.ReadOnly = true;
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (AppStandardReferenceItemCollection)Session["collEmployeeWorkSubGroup"];
                if (coll.Count == 0)
                {
                    txtWorkSubGroupID.Text = TxtItemID.Text.Trim() + ".01";
                }
                else
                {
                    int prefix = (TxtItemID.Text.Trim() + ".").Length;

                    var itemIdMax = (coll.OrderByDescending(c => c.ItemID).Select(c => c.ItemID.Substring(prefix, 2))).Take(1);
                    int itemId = int.Parse(itemIdMax.Single()) + 1;
                    txtWorkSubGroupID.Text = TxtItemID.Text.Trim() + "." + string.Format("{0:00}", itemId);
                }

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtWorkSubGroupID.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemID);
            txtWorkSubGroupName.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemName);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                AppStandardReferenceItemCollection coll = (AppStandardReferenceItemCollection)Session["collEmployeeWorkSubGroup"];

                bool isExist = false;
                foreach (var i in coll)
                {
                    if (i.ItemID.Equals(txtWorkSubGroupID.Text))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID {0} has exist.", txtWorkSubGroupID.Text);
                }
            }
        }

        #region Properties for return entry value
        public String ItemID
        {
            get { return txtWorkSubGroupID.Text; }
        }
        public String ItemName
        {
            get { return txtWorkSubGroupName.Text; }
        }

        #endregion
    }
}