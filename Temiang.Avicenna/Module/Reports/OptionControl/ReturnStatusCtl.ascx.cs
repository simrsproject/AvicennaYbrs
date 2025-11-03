using System;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class ReturnStatusCtl : BaseOptionCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                rbtReturnStatus.SelectedValue = "1";
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_IsReturn", rbtReturnStatus.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}