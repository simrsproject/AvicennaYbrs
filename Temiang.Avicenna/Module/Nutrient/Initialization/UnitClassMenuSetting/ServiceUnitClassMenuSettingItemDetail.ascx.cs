using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class ServiceUnitClassMenuSettingItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            ComboBox.PopulateWithClassInpatient(cboClassID);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }

            ViewState["IsNewRecord"] = false;
            cboClassID.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, ServiceUnitClassMenuSettingMetadata.ColumnNames.ClassID));
            chkIsOptinal.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, ServiceUnitClassMenuSettingMetadata.ColumnNames.IsOptional));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll =
                    (ServiceUnitClassMenuSettingCollection)Session["collServiceUnitClassMenuSetting"];

                bool isExist = coll.Any(row => row.ClassID.Equals(cboClassID.SelectedValue));

                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Class {0} has exist", cboClassID.Text);
                }
            }
        }

        public String ClassID
        {
            get { return cboClassID.SelectedValue; }
        }

        public String ClassName
        {
            get { return cboClassID.Text; }
        }

        public Boolean IsOptional
        {
            get { return chkIsOptinal.Checked; }
        }
    }
}