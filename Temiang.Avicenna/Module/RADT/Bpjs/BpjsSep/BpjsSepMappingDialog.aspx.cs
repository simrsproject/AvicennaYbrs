using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Text;
using Temiang.Avicenna.BusinessObject;
using System.Configuration;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class BpjsSepMappingDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.BpjsSep;

            if (!IsPostBack)
            {
                var service = new Temiang.Avicenna.Common.BPJS.VClaim.v11.Service();
                var response = service.GetSep(Request.QueryString["sepNo"]);
                if (response.MetaData.IsValid)
                {
                    var data = response.Response;

                    var sb = new StringBuilder();
                    sb.AppendLine(string.Format("No SEP : {0}", data.NoSep));
                    sb.AppendLine(string.Format("Nama : {0}", data.Peserta.Nama));
                    //sb.AppendLine(string.Format("NIK : {0}", data.Peserta.Nik));
                    sb.AppendLine(string.Format("No Kartu : {0}", data.Peserta.NoKartu));
                    sb.AppendLine(string.Format("No MR : {0}", data.Peserta.NoMr));
                    sb.AppendLine(string.Format("Jenis Kelamin : {0}", data.Peserta.Kelamin));
                    sb.AppendLine(string.Format("Tanggal Lahir : {0}", Convert.ToDateTime(data.Peserta.TglLahir).ToString("dd-MM-yyyy")));
                    sb.AppendLine(string.Format("Jenis Peserta : {0}", data.Peserta.JnsPeserta));
                    sb.AppendLine(string.Format("Kelas Pelayanan : {0}", data.JnsPelayanan));
                    sb.AppendLine(string.Format("Kelas Rawat : {0}", data.KelasRawat));
                    sb.AppendLine(string.Format("Poli Tujuan : {0}", data.Poli));
                    sb.AppendLine(string.Format("Diagnosa Awal : {0}", data.Diagnosa));
                    sb.AppendLine(string.Format("Catatan : {0}", data.Catatan));
                    //sb.AppendLine(string.Format("Keterangan : {0}", data.LakaLantas.Keterangan));

                    txtDetailSEP.Text = sb.ToString();
                }
                else txtDetailSEP.Text = response.MetaData.Message;

                if (!string.IsNullOrEmpty(Request.QueryString["regNo"]))
                {
                    txtNoRegistrasi.Text = Request.QueryString["regNo"];
                    if (!string.IsNullOrEmpty(txtNoRegistrasi.Text)) btnFilterRegistrasi_Click(null, null);
                }
            }
        }

        protected void btnFilterRegistrasi_Click(object sender, ImageClickEventArgs e)
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(txtNoRegistrasi.Text))
            {
                var sb = new StringBuilder();
                sb.AppendLine(string.Format("No Registrasi : {0}", reg.RegistrationNo));
                sb.AppendLine(string.Format("Tanggal Registrasi : {0}", reg.RegistrationDate.Value.ToString("dd-MM-yyyy")));

                var patient = new Patient();
                patient.LoadByPrimaryKey(reg.PatientID);
                sb.AppendLine(string.Format("No MR : {0}", patient.MedicalNo));
                sb.AppendLine(string.Format("Nama : {0}", patient.PatientName));

                var grr = new Guarantor();
                grr.LoadByPrimaryKey(reg.GuarantorID);

                sb.AppendLine(string.Format("Penjamin : {0}", grr.GuarantorName));

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(reg.ServiceUnitID);
                sb.AppendLine(string.Format("Poli Tujuan : {0}", unit.ServiceUnitName));

                var medic = new Paramedic();
                medic.LoadByPrimaryKey(reg.ParamedicID);
                sb.AppendLine(string.Format("Dokter : {0}", medic.ParamedicName));

                if (!string.IsNullOrEmpty(reg.BpjsSepNo))
                {
                    sb.AppendLine("--------------------------");
                    sb.AppendLine(string.Format("No SEP : {0}", reg.BpjsSepNo));
                }

                txtDetailRegistrasi.Text = sb.ToString();
            }
            else txtDetailRegistrasi.Text = "Data registrasi tidak ditemukan.";
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.mode = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(txtNoRegistrasi.Text))
            {
                //var service = new Temiang.Avicenna.Common.BPJS.v21.Service();
                //var response = service.GetDetailSep(Request.QueryString["sepNo"]);
                //if (response.Metadata.IsValid)
                //{
                //    var data = response.Response;

                //    //var map = new Common.BPJS.v21.Service();
                //    //var resp = map.Mapping(new Common.BPJS.v21.Sep.Mapping.TMapSep()
                //    //{
                //    //    noSep = Request.QueryString["sepNo"],
                //    //    noTrans = reg.RegistrationNo,
                //    //    ppkPelayanan = ConfigurationManager.AppSettings["BPJSHospitalID"]
                //    //});
                //    //if (!resp.Metadata.IsValid) return false;

                //    //if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                //    //{
                //    //    var co = new Common.BPJS.v21.Service();
                //    //    var respon = co.Update(new Common.BPJS.v21.Sep.UpdateTanggalPulang.TSep
                //    //    {
                //    //        noSep = Request.QueryString["sepNo"],
                //    //        ppkPelayanan = ConfigurationManager.AppSettings["BPJSHospitalID"],
                //    //        tglPlg = reg.RegistrationDate.Value.Date.ToString("yyyy-MM-dd HH:mm:ss")
                //    //    });
                //    //    if (!respon.Metadata.IsValid) return false;
                //    //}

                //    reg.BpjsSepNo = Request.QueryString["sepNo"];
                //    reg.GuarantorCardNo = data.Peserta.NoKartu;

                //    reg.Save();
                //}
                return true;
            }
            else return false;
        }
    }
}
