using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Finance.Integration.Eklaim
{
    public partial class eKlaimPdf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var svc = new Common.Inacbg.v51.Service();
            var print = svc.Print(new Temiang.Avicenna.Common.Inacbg.v51.Claim.Create.Data() { nomor_sep = Request.QueryString["sepno"] });
            if (print.Metadata.IsValid)
            {
                byte[] bytes = Convert.FromBase64String(print.Data);
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", bytes.Length.ToString());
                Response.BinaryWrite(bytes);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "print", string.Format("alert('{0} - {1}');", print.Metadata.Code, print.Metadata.Message), true);
            }
        }
    }
}
