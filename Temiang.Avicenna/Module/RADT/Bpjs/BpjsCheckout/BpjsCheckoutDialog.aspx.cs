using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Configuration;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class BpjsCheckoutDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.BpjsSep;

            if (!IsPostBack)
            {
                //var service = new Temiang.Avicenna.Common.BPJS.VClaim.Service();
                //var riwayat = service.UpdateTglPulang( Request.QueryString["noSep"]);
                //if (riwayat.Metadata.IsValid)
                //{
                //    if (riwayat.Response.Count.ToInt() == 0) return;
                //    var list = riwayat.Response.List[0];
                //    if (!string.IsNullOrEmpty(list.TglPulang))
                //    {
                //        txtTglPulang.SelectedDate = Convert.ToDateTime(list.TglPulang);

                //        this.ShowInformationHeader("Pasien telah checkout.");
                //        ButtonOk.Enabled = false;
                //    }
                //}
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.IsNullOrEmpty(Request.QueryString["src"]) ? "oWnd.argument = 'rebind'" : "oWnd.argument.mode = 'value!checkout!'";
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid) return false;
            if (txtTglPulang.IsEmpty)
            {
                this.ShowInformationHeader(string.Format("Tgl Pulang required."));
                return false;
            }

            //var co = new Common.BPJS.v21.Service();
            //var response = co.Update(new Common.BPJS.v21.Sep.UpdateTanggalPulang.TSep 
            //{ 
            //    noSep = txtNoSEP.Text,
            //    ppkPelayanan = ConfigurationManager.AppSettings["BPJSHospitalID"], 
            //    tglPlg = txtTglPulang.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss") 
            //});
            //if (!response.Metadata.IsValid) return false;

            return true;
        }
    }
}
