using System;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class InfectionDiseaseBtnCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_DiseaseType", rbtDiseaseType.SelectedValue);

            //Retun List
            return parameters;
        }

        public override string ParameterCaption
        {
            get { return lblCaption.Text; }
            set { lblCaption.Text = value; }
        }
    }
}