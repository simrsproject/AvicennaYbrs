using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs.VClaim
{
    public partial class BpjsVClaimSuplesiDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.BpjsSep;

            if (!IsPostBack)
            {
                txtNoPesertaPeserta.Text = Request.QueryString["id"];
                txtTglPelayanan.SelectedDate = DateTime.Now.Date;
            }
        }

        protected void btnCariData_Click(object sender, EventArgs e)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetSuplesiJasaRaharja(txtNoPesertaPeserta.Text, txtTglPelayanan.SelectedDate.Value.Date);
            if (response.MetaData.IsValid)
            {
                grdList.DataSource = response.Response.Jaminan;
                grdList.DataBind();
            }
            else ScriptManager.RegisterStartupScript(this, GetType(), "suplesi", string.Format("alert('Code : {0}, Message : {1}');", response.MetaData.Code, response.MetaData.Message), true);
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.mode = '" + "value!suplesi!" + grdList.SelectedValue + "'";
        }
    }
}
