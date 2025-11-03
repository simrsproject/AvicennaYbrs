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
    public partial class ParamedicGlobalScheduleDetail : BaseUserControl
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

            var otcoll = new OperationalTimeCollection();
            otcoll.LoadAll();
            cboOperationalTimeID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var ot in otcoll)
            {
                cboOperationalTimeID.Items.Add(new RadComboBoxItem(ot.OperationalTimeName, ot.OperationalTimeID));
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;
            cboDayOfWeek.Enabled = false;

            cboDayOfWeek.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, ParamedicGlobalScheduleMetadata.ColumnNames.DayOfWeek));
            cboOperationalTimeID.SelectedValue = (String)DataBinder.Eval(DataItem, ParamedicGlobalScheduleMetadata.ColumnNames.OperationalTimeID);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ParamedicGlobalScheduleCollection)Session["collParamedicGlobalSchedule"];

                int id = cboDayOfWeek.SelectedValue.ToInt();
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.DayOfWeek.Equals(id))
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

        public int DayOfWeek
        {
            get { return cboDayOfWeek.SelectedValue.ToInt(); }
        }

        public string DayOfWeekName
        {
            get { return cboDayOfWeek.Text; }
        }

        public String OperationalTimeID
        {
            get { return cboOperationalTimeID.SelectedValue; }
        }

        public String OperationalTimeName
        {
            get { return cboOperationalTimeID.Text; }
        }
    }
}