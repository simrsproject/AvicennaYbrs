using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class IncidentTypeComponentDetail : BaseUserControl
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

            txtComponentID.Text = (String)DataBinder.Eval(DataItem, IncidentTypeMetadata.ColumnNames.ComponentID);
            txtComponentName.Text = (String)DataBinder.Eval(DataItem, IncidentTypeMetadata.ColumnNames.ComponentName);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (IncidentTypeCollection)Session["collIncidentType"];

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

        #endregion
    }
}