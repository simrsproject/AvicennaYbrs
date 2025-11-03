using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeeOrientationDetail : BaseUserControl
    {
        private string logBookType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        private string personId
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["personId"]) ? string.Empty : Request.QueryString["personId"];
            }
        }

        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private string PageId
        {
            get { return ((HiddenField)Helper.FindControlRecursive(Page, "hdnPageId")).Value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            rblOrientationType.Enabled = logBookType == "";

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtEmployeeOrientationID.Text = "1";

                if (logBookType != "")
                    rblOrientationType.SelectedIndex = 1;

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtEmployeeOrientationID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeOrientationMetadata.ColumnNames.EmployeeOrientationID));
            rblOrientationType.SelectedIndex = (Boolean)DataBinder.Eval(DataItem, EmployeeOrientationMetadata.ColumnNames.IsGeneral) == true ? 0 : 1;
            txtStartDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeOrientationMetadata.ColumnNames.StartDate);
            txtEndDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeOrientationMetadata.ColumnNames.EndDate);
            txtDurationHour.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeOrientationMetadata.ColumnNames.DurationHour));
            txtDurationMinutes.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeOrientationMetadata.ColumnNames.DurationMinutes));
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
        }

        #region Properties for return entry value
        public Int32 EmployeeOrientationID
        {
            get { return Convert.ToInt32(txtEmployeeOrientationID.Text); }
        }
        public Boolean IsGeneral
        {
            get { return rblOrientationType.SelectedIndex == 0; }
        }
        public DateTime StartDate
        {
            get { return Convert.ToDateTime(txtStartDate.SelectedDate); }
        }
        public DateTime EndDate
        {
            get { return Convert.ToDateTime(txtEndDate.SelectedDate); }
        }
        public Decimal DurationHour
        {
            get { return Convert.ToDecimal(txtDurationHour.Value); }
        }
        public Decimal DurationMinutes
        {
            get { return Convert.ToDecimal(txtDurationMinutes.Value); }
        }
        #endregion
    }
}