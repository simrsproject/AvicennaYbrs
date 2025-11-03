using System;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class DemographyOptionCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                rbtDemographyOption.SelectedValue = "0";
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_Option", rbtDemographyOption.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}