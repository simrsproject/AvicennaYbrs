using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;


namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class AbRestrictionSuggestionDetail : BaseUserControl
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
            edtSuggestionNote.Content = DataBinder.Eval(DataItem, AbRestrictionSuggestionMetadata.ColumnNames.SuggestionNote).ToStringDefaultEmpty();
            ComboBox.SelectedValue(cboAbLevel, DataBinder.Eval(DataItem, AbRestrictionItemMetadata.ColumnNames.AbLevel).ToStringDefaultEmpty());
            cboAbLevel.Enabled = false;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll =
                    (AbRestrictionSuggestionCollection)Session["collAbRestrictionSuggestion"];

                var abLevel = cboAbLevel.SelectedValue.ToInt();
                bool isExist = false;
                foreach (AbRestrictionSuggestion item in coll)
                {
                    if (item.AbLevel.Equals(abLevel))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Ab Suggestion for Stratification: {0} has exist", "I;II;III".Split(';')[abLevel - 1]);
                }
            }
        }

        #region Properties for return entry value

        public int AbLevel
        {
            get { return cboAbLevel.SelectedValue.ToInt(); }
        }
        public String SuggestionNote
        {
            get { return edtSuggestionNote.Content; }
        }
        #endregion
    }
}
