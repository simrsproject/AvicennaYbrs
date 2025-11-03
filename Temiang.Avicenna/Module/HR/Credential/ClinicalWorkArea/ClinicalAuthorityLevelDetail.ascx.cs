using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Credential.ClinicalWorkArea
{
    public partial class ClinicalAuthorityLevelDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadTextBox TxtClinicalWorkAreaCode
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtClinicalWorkAreaCode"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            txtItemID.ReadOnly = true;
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (AppStandardReferenceItemCollection)Session["collClinicalAuthorityLevel"];
                if (coll.Count == 0)
                {
                    txtItemID.Text = TxtClinicalWorkAreaCode.Text.Trim() + ".01";
                }
                else
                {
                    int prefix = (TxtClinicalWorkAreaCode.Text.Trim() + ".").Length;

                    var itemIdMax = (coll.OrderByDescending(c => c.ItemID).Select(c => c.ItemID.Substring(prefix, 2))).Take(1);
                    int itemId = int.Parse(itemIdMax.Single()) + 1;
                    txtItemID.Text = TxtClinicalWorkAreaCode.Text.Trim() + "." + string.Format("{0:00}", itemId);
                }
                chkIsActive.Checked = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtItemID.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemID);
            txtItemName.Text = (String)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.ItemName);
            chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, AppStandardReferenceItemMetadata.ColumnNames.IsActive);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                AppStandardReferenceItemCollection coll = (AppStandardReferenceItemCollection)Session["collClinicalAuthorityLevel"];

                bool isExist = false;
                foreach (var i in coll)
                {
                    if (i.ItemID.Equals(txtItemID.Text))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID {0} has exist.", txtItemID.Text);
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
        public bool IsActive
        {
            get { return chkIsActive.Checked; }
        }

        #endregion
    }
}