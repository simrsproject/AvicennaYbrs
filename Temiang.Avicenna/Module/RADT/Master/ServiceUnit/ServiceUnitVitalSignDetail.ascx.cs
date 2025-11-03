using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceUnitVitalSignDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            var vs = new VitalSignCollection();
            vs.LoadAll();

            cboVitalSign.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var v in vs)
            {
                cboVitalSign.Items.Add(new RadComboBoxItem(v.VitalSignName, v.VitalSignID));
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;
            cboVitalSign.SelectedValue = (String)DataBinder.Eval(DataItem, ServiceUnitVitalSignMetadata.ColumnNames.VitalSignID);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                BasePage basePage = (BasePage)this.Page;
                ServiceUnitVitalSignCollection coll = (ServiceUnitVitalSignCollection)Session["collServiceUnitAutoVitalSign"];

                string id = cboVitalSign.SelectedValue;
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.VitalSignID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", cboVitalSign.Text);
                }
            }
        }

        #region Properties for return entry value
        public String VitalSignID
        {
            get { return cboVitalSign.SelectedValue; }
        }
        public String VitalSignName
        {
            get { return cboVitalSign.Text; }
        }
        #endregion

    }
}