using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.ControlPanel.Setting
{
    public partial class AppAutoNumberLastDetail : BaseUserControl
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

            txtDepartmentInitial.Text = (String)DataBinder.Eval(DataItem, AppAutoNumberLastMetadata.ColumnNames.DepartmentInitial);		
            txtYearNo.Value = Convert.ToInt32(DataBinder.Eval(DataItem, AppAutoNumberLastMetadata.ColumnNames.YearNo));
            txtMonthNo.Value = Convert.ToInt32(DataBinder.Eval(DataItem, AppAutoNumberLastMetadata.ColumnNames.MonthNo));
            txtDayNo.Value = Convert.ToInt32(DataBinder.Eval(DataItem, AppAutoNumberLastMetadata.ColumnNames.DayNo));
            txtLastNumber.Value = Convert.ToInt64(DataBinder.Eval(DataItem, AppAutoNumberLastMetadata.ColumnNames.LastNumber));
            txtLastCompleteNumber.Text = (String)DataBinder.Eval(DataItem, AppAutoNumberLastMetadata.ColumnNames.LastCompleteNumber);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //nothing todo
        }

        #region Properties for return entry value
        public String DepartmentInitial
        {
            get { return txtDepartmentInitial.Text; }
        } 
        public String YearNo
        {
            get { return txtYearNo.Text; }
        }

        public String MonthNo
        {
            get { return txtMonthNo.Text; }
        }

        public String DayNo
        {
            get { return txtDayNo.Text; }
        }

        public String LastNumber
        {
            get { return txtLastNumber.Text; }
        }

        public String LastCompleteNumber
        {
            get { return txtLastCompleteNumber.Text; }
        }

        #endregion
    }
}
