using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Globalization;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceUnitScheduleDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            cboDayOfWeek.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            var culture = new CultureInfo(AppSession.UserLogin.SRLanguage);
            var days = culture.DateTimeFormat.DayNames;
            int i = 1;
            foreach (var day in days)
            {
                cboDayOfWeek.Items.Add(new RadComboBoxItem(day, i.ToString()));
                i++;
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                ViewState["ServiceUnitID"] = string.Empty;

                return;
            }
            ViewState["IsNewRecord"] = false;
            ViewState["ServiceUnitID"] = (String)DataBinder.Eval(DataItem, ServiceUnitScheduleMetadata.ColumnNames.ServiceUnitID);
            
            cboDayOfWeek.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, ServiceUnitScheduleMetadata.ColumnNames.DayOfWeek));
            txtStartTime.Text = (String)DataBinder.Eval(DataItem, ServiceUnitScheduleMetadata.ColumnNames.StartTime);
            txtEndTime.Text = (String)DataBinder.Eval(DataItem, ServiceUnitScheduleMetadata.ColumnNames.EndTime);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ServiceUnitScheduleCollection)Session["collServiceUnitSchedule"];

                string itemID = cboDayOfWeek.SelectedValue;
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.DayOfWeek.Equals(itemID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Day of Week : {0} already exist", cboDayOfWeek.Text);
                }
            }
        }

        public String ServiceUnitID
        {
            get { return ViewState["ServiceUnitID"].ToString(); }
        }

        public int DayOfWeek
        {
            get { return cboDayOfWeek.SelectedValue.ToInt(); }
        }

        public string DayOfWeekName
        {
            get { return cboDayOfWeek.Text; }
        }

        public String StartTime
        {
            get { return txtStartTime.TextWithLiterals; }
        }

        public String EndTime
        {
            get { return txtEndTime.TextWithLiterals; }
        }
    }
}