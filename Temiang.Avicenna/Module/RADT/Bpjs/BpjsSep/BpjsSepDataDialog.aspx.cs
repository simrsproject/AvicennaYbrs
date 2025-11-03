using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Text;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class BpjsSepDataDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.BpjsSep;

            if (!IsPostBack) ButtonOk.Text = "Create SEP";
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            //var service = new Common.BPJS.v21.Service();
            //var resp = service.GetPeserta(txtNomorKartu.Text, true);
            //if (!resp.Metadata.IsValid)
            //{
            //    service = new Common.BPJS.v21.Service();
            //    resp = service.GetPeserta(txtNomorKartu.Text, false);
            //}
            //if (!resp.Metadata.IsValid) txtNomorKartu.Text = string.Format("{0} - {1}", resp.Metadata.Code, resp.Metadata.Message);
            //else
            //{
            //    var peserta = resp.Response.Peserta;

            //    var sb = new StringBuilder();
            //    sb.AppendLine(string.Format("Nama : {0}", peserta.Nama));
            //    sb.AppendLine(string.Format("NIK : {0}", peserta.Nik));
            //    sb.AppendLine(string.Format("No Kartu : {0}", peserta.NoKartu));
            //    sb.AppendLine(string.Format("No MR : {0}", peserta.NoMr));
            //    sb.AppendLine(string.Format("Jenis Kelamin : {0}", peserta.Sex));
            //    sb.AppendLine(string.Format("Tanggal Lahir : {0}", Convert.ToDateTime(peserta.TglLahir).ToString("dd-MM-yyyy")));
            //    sb.AppendLine(string.Format("Usia : {0}", peserta.Umur.UmurSekarang));
            //    sb.AppendLine(string.Format("Jenis Peserta : {0}", peserta.JenisPeserta.NmJenisPeserta));
            //    sb.AppendLine(string.Format("Kelas Tanggungan : {0}", peserta.KelasTanggungan.NmKelas));
            //    sb.AppendLine(string.Format("Status : {0}", peserta.StatusPeserta.Keterangan));

            //    txtPatientDetail.Text = sb.ToString();
            //}
        }
    }
}
