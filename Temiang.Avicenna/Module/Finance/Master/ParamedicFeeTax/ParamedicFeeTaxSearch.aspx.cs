using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master.ParamedicFeeTax
{
    public partial class ParamedicFeeTaxSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ParamedicFeeTax;
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }
    }
}
