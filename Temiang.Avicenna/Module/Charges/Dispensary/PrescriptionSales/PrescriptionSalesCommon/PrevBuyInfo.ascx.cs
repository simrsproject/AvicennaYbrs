using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Charges.Dispensary.PrescriptionSales.PrescriptionSalesCommon
{
    public partial class PrevBuyInfo : System.Web.UI.UserControl
    {
        public object Data;
 
        protected void Page_Load(object sender, EventArgs e)
        {
            repeater.DataSource = Data;
            repeater.DataBind();
        }
    }
}