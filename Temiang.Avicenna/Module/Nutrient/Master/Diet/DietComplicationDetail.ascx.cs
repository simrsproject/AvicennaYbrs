using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class DietComplicationDetail : BaseUserControl
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
                chkIsActive.Checked = true;
                return;
            }

            ViewState["IsNewRecord"] = false;
            ComboBox.DietsRequested(cboDietID, (String)DataBinder.Eval(DataItem, "DietID"), string.Empty, false);
            cboDietID.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, DietComplicationMetadata.ColumnNames.DietID));
            chkIsActive.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, DietComplicationMetadata.ColumnNames.IsActive));
        }

        protected void cboDietID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.DietsRequested((RadComboBox)sender, e.Text, string.Empty, false);
        }

        protected void cboDietID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["DietName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["DietID"].ToString();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
        }

        public String DietID
        {
            get { return cboDietID.SelectedValue; }
        }

        public String DietName
        {
            get { return cboDietID.Text; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }
    }
}