using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Telerik.Web.UI;
using Telerik.Web.UI.ImageEditor;

namespace Temiang.Avicenna.Module.Finance.Budgeting
{
    public partial class ExportToExcelDialog : BasePageDialog
    {   
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonOk.Visible = false;
                ButtonCancel.Text = "Close";

            }
            
            if (Session["ExportBudget"] != null) {
                var dtb = Session["ExportBudget"] as DataTable;
                Common.CreateExcelFile.CreateExcelDocument(dtb, "Budget.xls", this.Response);
                Session["ExportBudget"] = null;
            }
            
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }
    }
}
