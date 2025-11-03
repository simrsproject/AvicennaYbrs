using System;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class pJasmedPeriodCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Value initialized
            if (!Page.IsPostBack)
            {
                txtToDate.SelectedDate = DateTime.Today;
                txtFromDate.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                txtToDate2.SelectedDate = DateTime.Today;
                txtFromDate2.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            }
        }
        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("dStartDischarge", txtFromDate.SelectedDate);
            parameters.AddNew("dEndDischarge", txtToDate.SelectedDate);
            parameters.AddNew("dStartPayment", txtFromDate2.SelectedDate);
            parameters.AddNew("dEndPayment", txtToDate2.SelectedDate);

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
                return string.Format("Period : {0} to {1}", txtFromDate.SelectedDate, txtToDate.SelectedDate);
            }
        }
    }
}