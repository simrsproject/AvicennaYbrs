using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Master.HRBase.RL4
{
    public partial class RL4ProfessionTypeItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadTextBox TxtRL4TypeID
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtRL4TypeID"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            txtRL4ProfessionTypeID.ReadOnly = true;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (AppStandardReferenceItemCollection)Session["collRL4ProfessionType"];
                if (coll.Count == 0)
                {
                    txtRL4ProfessionTypeID.Text = TxtRL4TypeID.Text.Trim() + ".01";
                }
                else
                {
                    int prefix = (TxtRL4TypeID.Text.Trim() + ".").Length;

                    var itemIdMax = (coll.OrderByDescending(c => c.ItemID).Select(c => c.ItemID.Substring(prefix, 2))).Take(1);
                    int itemId = int.Parse(itemIdMax.Single()) + 1;
                    txtRL4ProfessionTypeID.Text = TxtRL4TypeID.Text.Trim() + "." + string.Format("{0:00}", itemId);
                }

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtRL4ProfessionTypeID.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemID);
            txtRL4ProfessionTypeName.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemName);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                AppStandardReferenceItemCollection coll = (AppStandardReferenceItemCollection)Session["collRL4ProfessionType"];

                bool isExist = false;
                foreach (var i in coll)
                {
                    if (i.ItemID.Equals(txtRL4ProfessionTypeID.Text))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID {0} has exist.", txtRL4ProfessionTypeID.Text);
                }
            }
        }

        #region Properties for return entry value
        public String ItemID
        {
            get { return txtRL4ProfessionTypeID.Text; }
        }
        public String ItemName
        {
            get { return txtRL4ProfessionTypeName.Text; }
        }

        #endregion
    }
}