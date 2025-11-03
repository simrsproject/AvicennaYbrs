using System;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class FastSlowCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                rbtMoving.SelectedValue = "1";
            
        }

        public override string ParameterCaption
        {
            get { return lblCaption.Text; }
            set { lblCaption.Text = value; }
        }

        public override string ReportSubTitle
        {
            get { return string.Format("Moving : {0} [{1}]", rbtMoving.Text, rbtMoving.SelectedValue); }
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_IsFast", rbtMoving.SelectedValue);

            //Retun List
            return parameters;
        }

    }
}