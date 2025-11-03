using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Configuration;
using System.IO;
using System.Globalization;

namespace Temiang.Avicenna.Module.Finance.Integration.JasaRaharja
{
    public partial class DataQueryDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.JasaRaharjaDataQuery;

            if (IsPostBack) return;

            var jr = new WebService.JasaRaharja();
            var entity = jr.GET_KEJADIAN_RS(Request.QueryString["id"])[0];

            txtNamaKorban.Text = entity.NAMA_KORBAN;
            txtNIK.Text = entity.NIK;
            txtTipeID.Text = entity.TIPE_ID;
            txtAlamat.Text = entity.ALAMAT;
            txtNoTelp.Text = entity.NO_TELP;
            rblJenisKelamin.SelectedIndex = entity.JENIS_KELAMIN == "L" ? 0 : 1;
            txtUmur.Text = entity.UMUR;
            txtTglKejadian.SelectedDate = DateTime.ParseExact(entity.TGL_KEJADIAN, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            txtTglMasukRS.SelectedDate = DateTime.ParseExact(entity.TGL_MASUK_RS, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            txtNamaRuangan.Text = entity.RUANGAN;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.isSuccess = ''";
        }
    }
}
