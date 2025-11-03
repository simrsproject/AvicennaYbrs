using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class Top10Ctl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Value initialized
            if (!Page.IsPostBack)
                txtTop10.Value = 10;
           
        }
        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_Top10", txtTop10.Value.ToString());

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
                return string.Format("Top 10 : {0}", "");
            }
        }
    }
}