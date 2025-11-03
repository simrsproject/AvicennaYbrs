using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class ParamedicFeeCategoryCtl : BaseOptionCtl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRParamedicFeeCategory, AppEnum.StandardReference.PhysicianFeeCategory);
            }
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_SRParamedicFeeCategory", cboSRParamedicFeeCategory.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}