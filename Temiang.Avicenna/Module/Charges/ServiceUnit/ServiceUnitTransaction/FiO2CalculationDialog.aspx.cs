using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class FiO2CalculationDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStartDateTime.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtEndDateTime.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtUsagePerMinute.Value = 1;
                txtUsagePersen.Value = 0;
                txtTotalUsage.Value = 1;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                (Helper.FindControlRecursive(this, "btnCancel") as Button).Visible = false;
            }
        }

        protected void btnCalculate_Click(object sender, ImageClickEventArgs e)
        {
            if (txtStartDateTime.IsEmpty || txtEndDateTime.IsEmpty || txtStartDateTime.SelectedDate > txtEndDateTime.SelectedDate)
            {
                txtTotalUsage.Value = 1;
                return;
            }

            DataTable dtb =
                    (new TransChargesItemCollection()).GetTotalMinutes(txtStartDateTime.SelectedDate ?? (new DateTime()).NowAtSqlServer(), txtEndDateTime.SelectedDate ?? (new DateTime()).NowAtSqlServer());

            if (dtb.Rows.Count > 0)
            {
                int totMinutes = dtb.Rows[0]["TotMinutes"].ToInt();
                txtTotalUsage.Value = Convert.ToDouble(totMinutes) * txtUsagePerMinute.Value* txtUsagePersen.Value / 100;
            }
            else 
                txtTotalUsage.Value = 1;
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Format("oArg.TotalUsage = {0}", txtTotalUsage.Value);
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }
    }
}