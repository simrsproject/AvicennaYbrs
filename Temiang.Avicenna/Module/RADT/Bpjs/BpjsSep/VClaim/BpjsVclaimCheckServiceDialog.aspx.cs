using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Configuration;
using System.Net;

namespace Temiang.Avicenna.Module.RADT.Bpjs.VClaim
{
    public partial class BpjsVclaimCheckServiceDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.BpjsSep;

            if (!IsPostBack)
            {
                Timer1_Tick(null, null);
                ButtonOk.Visible = false;
                ButtonCancel.Visible = false;
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            string url = ConfigurationManager.AppSettings["BPJSServiceUrlLocation"];
            try
            {
                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)myRequest.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    lblMessage.Text = string.Format("Service is online");
                    imgOk.Visible = true;
                    imgFailed.Visible = false;
                }
                else
                {
                    lblMessage.Text = string.Format("Service is offline : {0}", response.StatusDescription);
                    imgOk.Visible = false;
                    imgFailed.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = string.Format("Service is offline : {0}", ex.Message);
                imgOk.Visible = false;
                imgFailed.Visible = true;
            }
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }
    }
}
