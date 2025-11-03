using System;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class AgeDMYFromToCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Value initialized
            if (!Page.IsPostBack)
            {
                txtYFromAge.Value = 0;
                txtMFromAge.Value = 0;
                txtDFromAge.Value = 0;
                txtYToAge.Value = 0;
                txtMToAge.Value = 0;
                txtDToAge.Value = 0;
                
            }
        }
        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_YFromAge", txtYFromAge.Value.ToString());
            parameters.AddNew("p_MFromAge", txtMFromAge.Value.ToString());
            parameters.AddNew("p_DFromAge", txtDFromAge.Value.ToString());
            parameters.AddNew("p_YToAge", txtYToAge.Value.ToString());
            parameters.AddNew("p_MToAge", txtMToAge.Value.ToString());
            parameters.AddNew("p_DToAge", txtDToAge.Value.ToString());

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
                return string.Format("Period : {0} y {1} m {2} d to {3} y {4} m {5} d", txtYFromAge.Text, txtMFromAge.Text, txtDFromAge.Text, txtYToAge.Text, txtMToAge.Text,txtDToAge.Text);
            }
        }

    }
}