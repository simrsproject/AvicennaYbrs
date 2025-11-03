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
    public partial class ClassMenuSettingItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRMealSet, AppEnum.StandardReference.MealSet, true);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }

            ViewState["IsNewRecord"] = false;
            cboSRMealSet.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, ClassMealSetMenuSettingMetadata.ColumnNames.SRMealSet));
            chkIsOptinal.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, ClassMealSetMenuSettingMetadata.ColumnNames.IsOptional));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ClassMealSetMenuSettingCollection)Session["collClassMealSetMenuSetting"];

                bool isExist = coll.Any(row => row.SRMealSet.Equals(cboSRMealSet.SelectedValue));

                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Meal Set {0} has exist", cboSRMealSet.Text);
                }
            }
        }

        public String SRMealSet
        {
            get { return cboSRMealSet.SelectedValue; }
        }

        public String MealSetName
        {
            get { return cboSRMealSet.Text; }
        }

        public Boolean IsOptional
        {
            get { return chkIsOptinal.Checked; }
        }
    }
}