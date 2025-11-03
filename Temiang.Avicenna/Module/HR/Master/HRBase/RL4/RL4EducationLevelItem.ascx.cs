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
    public partial class RL4EducationLevelItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadTextBox TxtRL4ProfessionTypeID
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtRL4ProfessionTypeID"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            txtRL4EducationLevelID.ReadOnly = true;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (AppStandardReferenceItemCollection)Session["collRL4EducationLevel"];
                if (coll.Count == 0)
                {
                    txtRL4EducationLevelID.Text = TxtRL4ProfessionTypeID.Text.Trim() + ".01";
                }
                else
                {
                    int prefix = (TxtRL4ProfessionTypeID.Text.Trim() + ".").Length;

                    var itemIdMax = (coll.OrderByDescending(c => c.ItemID).Select(c => c.ItemID.Substring(prefix, 2))).Take(1);
                    int itemId = int.Parse(itemIdMax.Single()) + 1;
                    txtRL4EducationLevelID.Text = TxtRL4ProfessionTypeID.Text.Trim() + "." + string.Format("{0:00}", itemId);
                }

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtRL4EducationLevelID.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemID);
            txtRL4EducationLevelName.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemName);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                AppStandardReferenceItemCollection coll = (AppStandardReferenceItemCollection)Session["collRL4EducationLevel"];

                bool isExist = false;
                foreach (var i in coll)
                {
                    if (i.ItemID.Equals(txtRL4EducationLevelID.Text))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID {0} has exist.", txtRL4EducationLevelID.Text);
                }
            }
        }

        #region Properties for return entry value
        public String ItemID
        {
            get { return txtRL4EducationLevelID.Text; }
        }
        public String ItemName
        {
            get { return txtRL4EducationLevelName.Text; }
        }

        #endregion
    }
}