using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class AssetWorkPriorityCtl : BaseOptionCtl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRWorkPriority, AppEnum.StandardReference.WorkPriority);
            }
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_SRWorkPriority", cboSRWorkPriority.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}