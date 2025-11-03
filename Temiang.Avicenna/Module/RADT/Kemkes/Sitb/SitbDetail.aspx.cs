using DocumentFormat.OpenXml.Drawing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Kemkes
{
    public partial class SitbDetail : BasePageDetail
    {
        private string _hospitalSitb = ConfigurationManager.AppSettings["SitbHospitalID"];
        private string _kabupatenSitb = ConfigurationManager.AppSettings["SitbHospitalKabupatenID"];
        private string _provinsiSitb = ConfigurationManager.AppSettings["SitbHospitalProvinsiID"];

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "SitbSearch.aspx";
            UrlPageList = "SitbList.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.KemenkesSitb;

            if (!IsPostBack)
            {
                ToolBarMenuAdd.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboKabKota, AppEnum.StandardReference.KabKotaKemenkes);
                StandardReference.InitializeIncludeSpace(cboProvinsi, AppEnum.StandardReference.ProvinsiKemenkes);

                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regno"]);

                var patient = new Patient();
                patient.LoadByPrimaryKey(reg.PatientID);

                txtNoRegistrasi.Text = reg.RegistrationNo;
                txtNama.Text = patient.PatientName;
                txtNik.Text = patient.Ssn;
                rblJenisKelamin.SelectedValue = patient.Sex == "M" ? "L" : "P";
                txtTglLahir.SelectedDate = patient.DateOfBirth;
                txtAlamatKtp.Text = patient.Address;
            }
        }

        protected override void OnMenuNewClick()
        {
            var diag = new Diagnose();
            diag.LoadByPrimaryKey(Request.QueryString["diag"]);
            var diagnosa = new List<Common.BPJS.VClaim.v11.Diagnosa.Diagnosa2>();
            diagnosa.Add(new Temiang.Avicenna.Common.BPJS.VClaim.v11.Diagnosa.Diagnosa2()
            {
                Kode = diag.DiagnoseID,
                Nama = diag.DiagnoseName
            });

            cboDiagnosa.DataSource = diagnosa;
            cboDiagnosa.DataBind();
            cboDiagnosa.SelectedValue = Request.QueryString["diag"];

            cboKabKota.SelectedValue = _kabupatenSitb;
            cboProvinsi.SelectedValue = _provinsiSitb;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrWhiteSpace(rblSebelumPengobatanHasilTesCepat.SelectedValue))
            {
                args.IsCancel = true;
                args.MessageText = "Sebelum pengobatan hasil tes cepat required";
                return;
            }

            var svc = new Common.SirsKemkes.Service();
            var request = new Common.SirsKemkes.Tuberkulosis.Json.Request()
            {
                IdTb03 = string.Empty,
                KdPasien = txtNama.Text,
                Nik = txtNik.Text,
                JenisKelamin = rblJenisKelamin.SelectedValue,
                AlamatLengkap = txtAlamatKtp.Text,
                IdPropinsiFaskes = _provinsiSitb,
                KdKabupatenFaskes = _kabupatenSitb,
                IdPropinsiPasien = cboProvinsi.SelectedValue,
                KdKabupatenPasien = cboKabKota.SelectedValue,
                KdFasyankes = _hospitalSitb,
                KodeIcdX = cboDiagnosa.SelectedValue,
                TipeDiagnosis = rblTipeDiagnosis.SelectedValue,
                KlasifikasiLokasiAnatomi = rblLokasiAnatomi.SelectedValue,
                KlasifikasiRiwayatPengobatan = rblRiwayatPengobatan.SelectedValue,
                TanggalMulaiPengobatan = txtTglMulaiPengobatan.SelectedDate.Value.Date.ToString("yyyyMMdd"),
                PaduanOat = txtPanduanOat.Text,
                SebelumPengobatanHasilMikroskopis = rblSebelumPengobatanHasilMikroskopis.SelectedValue,
                SebelumPengobatanHasilTesCepat = rblSebelumPengobatanHasilTesCepat.SelectedValue,
                SebelumPengobatanHasilBiakan = rblSebelumPengobatanHasilBiakan.SelectedValue,
                HasilMikroskopisBulan2 = rblHasilMikroskopisBulan2.SelectedValue,
                HasilMikroskopisBulan3 = rblHasilMikroskopisBulan3.SelectedValue,
                HasilMikroskopisBulan5 = rblHasilMikroskopisBulan5.SelectedValue,
                AkhirPengobatanHasilMikroskopis = rblAkhirPengobatanHasilMikroskopis.SelectedValue,
                TanggalHasilAkhirPengobatan = txtTglAkhirPengobatan.SelectedDate == null ? string.Empty : txtTglAkhirPengobatan.SelectedDate.Value.Date.ToString("yyyyMMdd"),
                HasilAkhirPengobatan = rblHasilAkhirPengobatan.SelectedValue,
                TglLahir = txtTglLahir.SelectedDate.Value.Date.ToString("yyyyMMdd"),
                FotoToraks = rblFotoToraks.SelectedValue
            };

            var log = new WebServiceAPILog()
            {
                DateRequest = DateTime.Now,
                IPAddress = string.Empty,
                UrlAddress = string.Empty,
                Params = JsonConvert.SerializeObject(request),
                Response = string.Empty,
                Totalms = 0
            };
            log.Save();

            var response = svc.PostInsertSitb(request);

            if (response == null)
            {
                args.IsCancel = true;
                args.MessageText = "No connection to server";
                return;
            }
            if (response.Status.ToLower() != "berhasil")
            {
                args.IsCancel = true;
                args.MessageText = $"{response.Status}, {response.Keterangan}";
                return;
            }

            var sitb = new KemenkesSitb
            {
                RegistrationNo = Request.QueryString["regno"],
                SitbNo = response.IdTb03,
                RequestSitb = JsonConvert.SerializeObject(request),
                ResponseSitb = JsonConvert.SerializeObject(response),
                LastUpdateByUserID = AppSession.UserLogin.UserID,
                LastUpdateDateTime = DateTime.Now
            };
            sitb.Save();

            txtNoSitb.Text = sitb.SitbNo;
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrWhiteSpace(rblSebelumPengobatanHasilTesCepat.SelectedValue))
            {
                args.IsCancel = true;
                args.MessageText = "Sebelum pengobatan hasil tes cepat required";
                return;
            }

            var sitb = new KemenkesSitb();
            sitb.LoadByPrimaryKey(Request.QueryString["regno"]);

            var svc = new Common.SirsKemkes.Service();
            var request = new Common.SirsKemkes.Tuberkulosis.Json.Request()
            {
                IdTb03 = sitb.SitbNo,
                KdPasien = txtNama.Text,
                Nik = txtNik.Text,
                JenisKelamin = rblJenisKelamin.SelectedValue,
                AlamatLengkap = txtAlamatKtp.Text,
                IdPropinsiFaskes = _provinsiSitb,
                KdKabupatenFaskes = _kabupatenSitb,
                IdPropinsiPasien = cboProvinsi.SelectedValue,
                KdKabupatenPasien = cboKabKota.SelectedValue,
                KdFasyankes = _hospitalSitb,
                KodeIcdX = cboDiagnosa.SelectedValue,
                TipeDiagnosis = rblTipeDiagnosis.SelectedValue,
                KlasifikasiLokasiAnatomi = rblLokasiAnatomi.SelectedValue,
                KlasifikasiRiwayatPengobatan = rblRiwayatPengobatan.SelectedValue,
                TanggalMulaiPengobatan = txtTglMulaiPengobatan.SelectedDate.Value.Date.ToString("yyyyMMdd"),
                PaduanOat = txtPanduanOat.Text,
                SebelumPengobatanHasilMikroskopis = rblSebelumPengobatanHasilMikroskopis.SelectedValue,
                SebelumPengobatanHasilTesCepat = rblSebelumPengobatanHasilTesCepat.SelectedValue,
                SebelumPengobatanHasilBiakan = rblSebelumPengobatanHasilBiakan.SelectedValue,
                HasilMikroskopisBulan2 = rblHasilMikroskopisBulan2.SelectedValue,
                HasilMikroskopisBulan3 = rblHasilMikroskopisBulan3.SelectedValue,
                HasilMikroskopisBulan5 = rblHasilMikroskopisBulan5.SelectedValue,
                AkhirPengobatanHasilMikroskopis = rblAkhirPengobatanHasilMikroskopis.SelectedValue,
                TanggalHasilAkhirPengobatan = txtTglAkhirPengobatan.SelectedDate == null ? string.Empty : txtTglAkhirPengobatan.SelectedDate.Value.Date.ToString("yyyyMMdd"),
                HasilAkhirPengobatan = rblHasilAkhirPengobatan.SelectedValue,
                TglLahir = txtTglLahir.SelectedDate.Value.Date.ToString("yyyyMMdd"),
                FotoToraks = rblFotoToraks.SelectedValue
            };
            var response = svc.PostUpdateSitb(request);

            if (response == null)
            {
                args.IsCancel = true;
                args.MessageText = "No connection to server";
                return;
            }
            if (response.Status.ToLower() != "update berhasil")
            {
                args.IsCancel = true;
                args.MessageText = $"{response.Status}, {response.Keterangan}";
                return;
            }

            sitb.RequestSitb = JsonConvert.SerializeObject(request);
            sitb.ResponseSitb = JsonConvert.SerializeObject(response);
            sitb.LastUpdateByUserID = AppSession.UserLogin.UserID;
            sitb.LastUpdateDateTime = DateTime.Now;
            sitb.Save();
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            if (parameters.Length > 0)
            {
                var diag = new Diagnose();
                diag.LoadByPrimaryKey(Request.QueryString["diag"]);
                var diagnosa = new List<Common.BPJS.VClaim.v11.Diagnosa.Diagnosa2>();
                diagnosa.Add(new Temiang.Avicenna.Common.BPJS.VClaim.v11.Diagnosa.Diagnosa2()
                {
                    Kode = diag.DiagnoseID,
                    Nama = diag.DiagnoseName
                });

                cboDiagnosa.DataSource = diagnosa;
                cboDiagnosa.DataBind();
                cboDiagnosa.SelectedValue = Request.QueryString["diag"];

                var sitb = new KemenkesSitb();
                if (!sitb.LoadByPrimaryKey(Request.QueryString["regno"])) return;

                if (string.IsNullOrWhiteSpace(sitb.SitbNo)) return;
                txtNoSitb.Text = sitb.SitbNo;

                if (string.IsNullOrWhiteSpace(sitb.RequestSitb)) return;

                var data = JsonConvert.DeserializeObject<Common.SirsKemkes.Tuberkulosis.Json.Request>(sitb.RequestSitb);

                txtNama.Text = data.KdPasien;
                txtNik.Text = data.Nik;
                rblJenisKelamin.SelectedValue = data.JenisKelamin;
                txtAlamatKtp.Text = data.AlamatLengkap;
                cboKabKota.SelectedValue = data.KdKabupatenPasien;
                cboProvinsi.SelectedValue = data.IdPropinsiPasien;

                diag = new Diagnose();
                diag.LoadByPrimaryKey(data.KodeIcdX);

                diagnosa = new List<Common.BPJS.VClaim.v11.Diagnosa.Diagnosa2>();
                diagnosa.Add(new Temiang.Avicenna.Common.BPJS.VClaim.v11.Diagnosa.Diagnosa2()
                {
                    Kode = data.KodeIcdX,
                    Nama = diag.DiagnoseName
                });

                cboDiagnosa.DataSource = diagnosa;
                cboDiagnosa.DataBind();
                cboDiagnosa.SelectedValue = data.KodeIcdX;

                rblTipeDiagnosis.SelectedValue = data.TipeDiagnosis;
                rblLokasiAnatomi.SelectedValue = data.KlasifikasiLokasiAnatomi;
                rblRiwayatPengobatan.SelectedValue = data.KlasifikasiRiwayatPengobatan;

                var format = "yyyyMMdd";
                DateTime.TryParseExact(data.TanggalMulaiPengobatan, format, null, System.Globalization.DateTimeStyles.None, out var tglMulai);
                txtTglMulaiPengobatan.SelectedDate = tglMulai;
                txtPanduanOat.Text = data.PaduanOat;
                rblSebelumPengobatanHasilMikroskopis.SelectedValue = data.SebelumPengobatanHasilMikroskopis;
                rblSebelumPengobatanHasilTesCepat.SelectedValue = data.SebelumPengobatanHasilTesCepat;
                rblSebelumPengobatanHasilBiakan.SelectedValue = data.SebelumPengobatanHasilBiakan;
                rblHasilMikroskopisBulan2.SelectedValue = data.HasilMikroskopisBulan2;
                rblHasilMikroskopisBulan3.SelectedValue = data.HasilMikroskopisBulan3;
                rblHasilMikroskopisBulan5.SelectedValue = data.HasilMikroskopisBulan5;
                rblHasilAkhirPengobatan.SelectedValue = data.AkhirPengobatanHasilMikroskopis;

                if (!string.IsNullOrWhiteSpace(data.TanggalHasilAkhirPengobatan))
                {
                    DateTime.TryParseExact(data.TanggalHasilAkhirPengobatan, format, null, System.Globalization.DateTimeStyles.None, out var tglAkhir);
                    txtTglAkhirPengobatan.SelectedDate = tglAkhir;
                }

                DateTime.TryParseExact(data.TglLahir, format, null, System.Globalization.DateTimeStyles.None, out var tglLahir);
                txtTglLahir.SelectedDate = tglLahir;
                rblFotoToraks.SelectedValue = data.FotoToraks;
            }
        }

        protected void cboDiagnosa_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((Common.BPJS.VClaim.v11.Diagnosa.Diagnosa2)e.Item.DataItem).Nama;
            e.Item.Value = ((Common.BPJS.VClaim.v11.Diagnosa.Diagnosa2)e.Item.DataItem).Kode;
        }

        protected void cboKabKota_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Value))
            {
                cboProvinsi.SelectedValue = string.Empty;
                cboProvinsi.Text = string.Empty;
                return;
            }

            var std = new AppStandardReferenceItem();
            std.LoadByPrimaryKey(AppEnum.StandardReference.KabKotaKemenkes.ToString(), e.Value);
            cboProvinsi.SelectedValue = std.Note;
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            OnPopulateEntryControl(new string[0]);
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            OnPopulateEntryControl(new string[0]);
        }
    }
}