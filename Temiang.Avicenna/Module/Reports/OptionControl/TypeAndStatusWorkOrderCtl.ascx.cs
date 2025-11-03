using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class TypeAndStatusWorkOrderCtl : BaseOptionCtl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRWorkType, AppEnum.StandardReference.WorkType);
                StandardReference.InitializeIncludeSpace(cboSRWorkStatus, AppEnum.StandardReference.WorkStatus);
            }
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_SRWorkType", cboSRWorkType.SelectedValue);
            parameters.AddNew("p_SRWorkStatus", cboSRWorkStatus.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}