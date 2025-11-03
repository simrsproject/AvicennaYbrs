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
    public partial class RL4EducationMajorItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadTextBox TxtRL4EducationLevelID
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtRL4EducationLevelID"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            txtRL4EducationMajorID.ReadOnly = true;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (AppStandardReferenceItemCollection)Session["collRL4EducationMajor"];
                if (coll.Count == 0)
                {
                    txtRL4EducationMajorID.Text = TxtRL4EducationLevelID.Text.Trim() + ".01";
                }
                else
                {
                    int prefix = (TxtRL4EducationLevelID.Text.Trim() + ".").Length;

                    var itemIdMax = (coll.OrderByDescending(c => c.ItemID).Select(c => c.ItemID.Substring(prefix, 2))).Take(1);
                    int itemId = int.Parse(itemIdMax.Single()) + 1;
                    txtRL4EducationMajorID.Text = TxtRL4EducationLevelID.Text.Trim() + "." + string.Format("{0:00}", itemId);
                }

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtRL4EducationMajorID.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemID);
            txtRL4EducationMajorName.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemName);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                AppStandardReferenceItemCollection coll = (AppStandardReferenceItemCollection)Session["collRL4EducationMajor"];

                bool isExist = false;
                foreach (var i in coll)
                {
                    if (i.ItemID.Equals(txtRL4EducationMajorID.Text))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID {0} has exist.", txtRL4EducationMajorID.Text);
                }
            }
        }

        #region Properties for return entry value
        public String ItemID
        {
            get { return txtRL4EducationMajorID.Text; }
        }
        public String ItemName
        {
            get { return txtRL4EducationMajorName.Text; }
        }

        #endregion
    }
}