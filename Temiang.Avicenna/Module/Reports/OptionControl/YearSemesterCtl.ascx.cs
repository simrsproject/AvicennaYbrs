using System;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class YearSemesterCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cboPeriodSemester.Items.Add(new RadComboBoxItem("Semester 1", "1"));
                cboPeriodSemester.Items.Add(new RadComboBoxItem("Semester 2", "2"));


                for (int i = DateTime.Now.Year; i > DateTime.Now.Year - 11; i--)
                {
                    cboPeriodYear.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
                }

                cboPeriodYear.SelectedValue = DateTime.Now.Year.ToString();
                cboPeriodSemester.SelectedValue = "1";
            }
        }
        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_PeriodSemester", cboPeriodSemester.SelectedValue);
            parameters.AddNew("p_PeriodYear", cboPeriodYear.SelectedValue);
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
                return string.Format("Period : {0} {1}", cboPeriodSemester.Text, cboPeriodYear.Text);
            }
        }

    }
}