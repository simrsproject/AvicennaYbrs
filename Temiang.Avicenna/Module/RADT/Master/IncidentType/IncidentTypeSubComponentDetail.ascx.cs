using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class IncidentTypeSubComponentDetail : BaseUserControl
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

            txtSubComponentID.Text = (String)DataBinder.Eval(DataItem, IncidentTypeItemMetadata.ColumnNames.SubComponentID);
            txtSubComponentName.Text = (String)DataBinder.Eval(DataItem, IncidentTypeItemMetadata.ColumnNames.SubComponentName);
            chkIsAllowEdit.Checked = (bool)DataBinder.Eval(DataItem, IncidentTypeItemMetadata.ColumnNames.IsAllowEdit);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (IncidentTypeItemCollection)Session["collIncidentTypeItem"];

                string subCompId = txtSubComponentID.Text;
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.SubComponentID.Equals(subCompId))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Sub Component ID : {0} already exist", subCompId);
                }
            }
        }

        #region Properties for return entry value

        public String SubComponentID
        {
            get { return txtSubComponentID.Text; }
        }

        public String SubComponentName
        {
            get { return txtSubComponentName.Text; }
        }

        public Boolean IsAllowEdit
        {
            get { return chkIsAllowEdit.Checked; }
        }

        #endregion
    }
}