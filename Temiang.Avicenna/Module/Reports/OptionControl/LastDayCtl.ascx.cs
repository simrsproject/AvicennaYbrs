using System;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class LastDayCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            
        }

        public override string ParameterCaption
        {
            get { return lblCaption.Text; }
            set { lblCaption.Text = value; }
        }

        public override string ReportSubTitle
        {
            get { return string.Format("Last Day : {0}", numLastDay.Value); }
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_LastDay", numLastDay.Value.ToString());

            //Retun List
            return parameters;
        }

    }
}