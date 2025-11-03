using System;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class FormulariumNonFormulariumCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                rbtFormulariumNonFormularium.SelectedValue = "1";
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_IsFormularium", rbtFormulariumNonFormularium.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}