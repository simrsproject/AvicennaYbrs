using System;
using Temiang.Avicenna.BusinessObject;
namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class AmountCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Value initialized
            if (!Page.IsPostBack)
                txtAmountValue.Value = 0;
        }
        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_Amount", txtAmountValue.Value.ToString());

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
                return string.Format("Amount : {0}", "");
            }
        }
    }
}