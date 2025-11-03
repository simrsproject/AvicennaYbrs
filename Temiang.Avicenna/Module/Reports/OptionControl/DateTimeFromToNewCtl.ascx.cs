using System;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class DateTimeFromToNewCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Value initialized
            if (!Page.IsPostBack)
            {
                txtDateFrom.SelectedDate = DateTime.Now;
                txtDateTo.SelectedDate = DateTime.Now;

                var date = DateTime.Now;
                txtTimeFrom.SelectedDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                txtTimeTo.SelectedDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            }
        }
        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();

            
            if (txtTimeFrom.SelectedDate == null)
            {
                var date = Convert.ToDateTime(txtDateFrom.SelectedDate);
                txtTimeFrom.SelectedDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            }

            if (txtTimeTo.SelectedDate == null)
            {
                var date = Convert.ToDateTime(txtTimeTo.SelectedDate);
                txtTimeTo.SelectedDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            }

            var d1 = DateTime.Parse(txtDateFrom.SelectedDate.Value.ToShortDateString() + " " + txtTimeFrom.SelectedDate.Value.ToShortTimeString());
            var d2 = DateTime.Parse(txtDateTo.SelectedDate.Value.ToShortDateString() + " " + txtTimeTo.SelectedDate.Value.ToShortTimeString());

            parameters.AddNew("p_FromDateTime", d1);
            parameters.AddNew("p_ToDateTime", d2);

            //Retun List
            return parameters;
        }

        public override string ParameterCaption
        {
            get { return lblCaption.Text; }
            set { lblCaption.Text = value; }
        }
        public override string ReportSubTitle
        {
            get
            {
                var d1 = DateTime.Parse(txtDateFrom.SelectedDate.Value.ToShortDateString() + " " + txtTimeFrom.SelectedDate.Value.ToShortTimeString());
                var d2 = DateTime.Parse(txtDateTo.SelectedDate.Value.ToShortDateString() + " " + txtTimeTo.SelectedDate.Value.ToShortTimeString());

                return string.Format("Period : {0} to {1}", d1, d2);
            }
        }
    }
}