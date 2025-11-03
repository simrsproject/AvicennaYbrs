using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Integration.Eklaim
{
    public partial class SitbDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.InacbgProcess;

            if (!IsPostBack) ButtonCancel.Visible = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var svc = new Common.Inacbg.v57.Service();
                var response = svc.SetValidateSitb(Request.QueryString["nosep"], Request.QueryString["nositb"]);
                if (response.Metadata.IsValid)
                {
                    txtStatus.Text = response.Response.Status;
                    txtKeterangan.Text = response.Response.Detail;
                    if (response.Response.Validation != null)
                    {
                        txtNama.Text = response.Response.Validation.Data.First().Nama;
                        txtNik.Text = response.Response.Validation.Data.First().Nik;
                        txtJenisKelamin.Text = response.Response.Validation.Data.First().JenisKelaminId == "1" ? "Laki-laki" : "Perempuan";
                    }
                }
                else
                {
                    txtStatus.Text = response.Response != null ? response.Response.Status : response.Metadata.Code;
                    txtKeterangan.Text = response.Response != null ? response.Response.Detail : response.Metadata.Message;
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }
    }
}