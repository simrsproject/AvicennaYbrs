using System;
using System.Threading;
using System.Web.UI;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.MasterPage
{
    public partial class MasterDialog : System.Web.UI.MasterPage
    {
        ///<summary>
        ///
        ///</summary>
        protected BasePageDialog BasePageDialogCurrent
        {
            get { return (BasePageDialog)this.ContentPlaceHolder1.Page; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            string script;
            if (((BasePageDialog)this.ContentPlaceHolder1.Page).OnButtonOkClicked())
            {
                script = "<script type='text/javascript'>CloseAndApply();</script>";
                //Create Startup Javascript for close window              
                Page.ClientScript.RegisterStartupScript(this.GetType(), "closeMe", script);
            }
            else
            {
                if (hdfMessage.Value != string.Empty)
                {
                    script = string.Format("<script type='text/javascript'>alert('{0}');</script>", hdfMessage.Value);
                    //Create Startup Javascript for message
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msg", script);
                }
            }

        }

    }
}
