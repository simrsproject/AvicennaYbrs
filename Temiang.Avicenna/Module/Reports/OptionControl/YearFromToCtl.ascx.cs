using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class YearFromToCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                for (int i = DateTime.Now.Year + 1; i > DateTime.Now.Year - 11; i--)
                {
                    cboYearFrom.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
                    cboYearTo.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
                }

                cboYearFrom.SelectedValue = DateTime.Now.Year.ToString();
                cboYearTo.SelectedValue = DateTime.Now.Year.ToString();
            }
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_YearFrom", cboYearFrom.SelectedValue);
            parameters.AddNew("p_YearTo", cboYearTo.SelectedValue);
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
                return string.Format("Period : {0} to {1}", cboYearFrom.SelectedValue, cboYearTo.SelectedValue);
            }
        }

    }
}