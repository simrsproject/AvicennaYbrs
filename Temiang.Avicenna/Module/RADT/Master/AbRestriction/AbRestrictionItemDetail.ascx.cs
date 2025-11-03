using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;


namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class AbRestrictionItemDetail : BaseUserControl
    {
        public object DataItem { get; set; }
        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;
            ComboBox.PopulateWithOneZatActive(cboZatActiveID, (String)DataBinder.Eval(DataItem, AbRestrictionItemMetadata.ColumnNames.ZatActiveID));
            ComboBox.SelectedValue(cboAbLevel,DataBinder.Eval(DataItem, AbRestrictionItemMetadata.ColumnNames.AbLevel).ToString());
            txtNotes.Text = DataBinder.Eval(DataItem, AbRestrictionItemMetadata.ColumnNames.Notes).ToString();
            cboZatActiveID.Enabled = false;
            cboAbLevel.Enabled = false;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll =
                    (AbRestrictionItemCollection)Session["collAbRestrictionItem"];

                var itemID = cboZatActiveID.SelectedValue;
                var level = cboAbLevel.SelectedValue.ToInt();
                bool isExist = false;
                foreach (AbRestrictionItem item in coll)
                {
                    if (item.ZatActiveID.Equals(itemID) && item.AbLevel.Equals(level))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Ab Restriction Item ID: {0} has exist in Stratification {1}", itemID, cboAbLevel.Text);
                }
            }
        }

        #region Properties for return entry value

        public String ZatActiveID
        {
            get { return cboZatActiveID.SelectedValue; }
        }

        public String ZatActiveName
        {
            get { return cboZatActiveID.Text; }
        }

        public int AbLevel
        {
            get { return cboAbLevel.SelectedValue.ToInt(); }
        }
        public String Notes
        {
            get { return txtNotes.Text; }
        }
        #endregion
    }
}
