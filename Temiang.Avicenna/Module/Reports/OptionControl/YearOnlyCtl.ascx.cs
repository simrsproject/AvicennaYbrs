using System;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class YearOnlyCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                for (int i = DateTime.Now.Year + 1; i > DateTime.Now.Year - 11; i--)
                {
                    cboPeriodYear.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
                }

                cboPeriodYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }
        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_PeriodYear", cboPeriodYear.SelectedValue);
            //Retun List
            return parameters;
        }

        public override string ParameterCaption
        {
            get { return lblYear.Text; }
            set { lblYear.Text = value; }
        }
        public override string ReportSubTitle
        {
            get
            {
                return string.Format("Tahun : {0}", cboPeriodYear.Text);
            }
        }

    }
}