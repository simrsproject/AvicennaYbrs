using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ContributoryFactorsClassificationFrameworkItemComponentDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtComponentID.Text = (String)DataBinder.Eval(DataItem, ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.ComponentID);
            txtComponentName.Text = (String)DataBinder.Eval(DataItem, ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.ComponentName);
            chkIsAllowEdit.Checked = (bool)DataBinder.Eval(DataItem, ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.IsAllowEdit);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ContributoryFactorsClassificationFrameworkItemComponentCollection)Session["collContributoryFactorsClassificationFrameworkItemComponent"];

                string compId = txtComponentID.Text;
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.ComponentID.Equals(compId))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Component ID : {0} already exist", compId);
                }
            }
        }

        #region Properties for return entry value

        public String ComponentID
        {
            get { return txtComponentID.Text; }
        }

        public String ComponentName
        {
            get { return txtComponentName.Text; }
        }

        public Boolean IsAllowEdit
        {
            get { return chkIsAllowEdit.Checked; }
        }

        #endregion
    }
}