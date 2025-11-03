using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.Common.BPJS.VClaim.v11;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.Module.RADT.Bpjs.VClaim
{
    public partial class BpjsVClaimDetail : BasePageDetail
    {
        //private System.Timers.Timer timer;
        //private Thread thread = null;

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "../BpjsSepSearch.aspx";
            UrlPageList = "../BpjsSepList.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.BpjsSep;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
                {
                    if (AppSession.Parameter.HealthcareInitial == "RSI")
                    {
                        //cboAssesmentPelayanan.Items.Remove(4);
                    }
                }
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            //if (!IsPostBack) CheckBpjsWebApi();

            if (!IsPostBack)
            {
                if (AppSession.Parameter.HealthcareInitial == "RSI")
                {
                    foreach (RadComboBoxItem item in cboAssesmentPelayanan.Items.Cast<RadComboBoxItem>())
                    {
                        if (new string[] { "3", "4" }.Contains(item.Value)) item.Visible = false;
                    }
                }
            }
        }

        //private bool CheckBpjsWebApi()
        //{
        //    HideInformationHeader();
        //    string url = ConfigurationManager.AppSettings["BPJSServiceUrlLocation"];
        //    try
        //    {
        //        Common.BPJS.Helper.IgnoreBadCertificates();

        //        var myRequest = (HttpWebRequest)WebRequest.Create(url);
        //        var response = (HttpWebResponse)myRequest.GetResponse();

        //        if (response.StatusCode != HttpStatusCode.OK)
        //        {
        //            ShowInformationHeader(string.Format("BPJS Service is offline (url : {0}), error : {1}", url, response.StatusDescription));
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowInformationHeader(string.Format("BPJS Service is offline (url : {0}), error : {1}", url, ex.Message));
        //        return true;
        //    }
        //    return false;
        //}

        //private static bool CheckBpjsWebApi(ValidateArgs args)
        //{
        //    //HideInformationHeader();
        //    string url = ConfigurationManager.AppSettings["BPJSServiceUrlLocation"];
        //    try
        //    {
        //        Common.BPJS.Helper.IgnoreBadCertificates();

        //        var myRequest = (HttpWebRequest)WebRequest.Create(url);
        //        var response = (HttpWebResponse)myRequest.GetResponse();

        //        if (response.StatusCode != HttpStatusCode.OK)
        //        {
        //            args.MessageText = string.Format("BPJS Service is offline (url : {0}), error : {1}", url, response.StatusDescription);
        //            args.IsCancel = true;
        //            return args.IsCancel;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //ShowInformationHeader(string.Format("BPJS Service is offline (url : {0}), error : {1}", url, ex.Message));
        //        //return true;

        //        args.MessageText = string.Format("BPJS Service is offline (url : {0}), error : {1}", url, ex.Message);
        //        args.IsCancel = true;
        //        return args.IsCancel;

        //    }
        //    return false;
        //}

        protected void btnCariData_Click(object sender, EventArgs e)
        {
            HideInformationHeader();

            //if (CheckBpjsWebApi()) return;

            if (string.IsNullOrEmpty(txtNomor.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "cari", string.Format("alert('Code : {0}, Message : {1}');", "000", rblJenis.SelectedValue == "1" ? "No Rujukan belum diisi" : "No Peserta belum diisi"), true);
                return;
            }

            ViewState["sepID"] = 0;
            string tglCetakKartu = string.Empty;

            //var item = cboTujuanKunjungan.Items.Single(i => i.Value == "0");
            //item.Enabled = true;

            var svc = new Common.BPJS.VClaim.v11.Service();
            if (rblJenis.SelectedValue == "1")
            {
                var response = svc.GetRujukan(true, txtNomor.Text, cboAsalRujukan.SelectedValue == "1" ? Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1 : Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                if (response.MetaData.IsValid)
                {
                    var rujukan = response.Response.Rujukan;

                    tglCetakKartu = rujukan.Peserta.TglCetakKartu;
                    cboPelayanan.SelectedValue = rujukan.Pelayanan.Kode;

                    var peserta = rujukan.Peserta;

                    ViewState["ProlanisPRB"] = peserta.Informasi.ProlanisPRB;

                    txtNamaPeserta.Text = peserta.Nama;
                    txtNikPeserta.Text = peserta.Nik;
                    txtNoPesertaPeserta.Text = peserta.NoKartu;

                    string format = "yyyy-MM-dd";
                    DateTime parsed;
                    DateTime.TryParseExact(peserta.TglLahir, format, null, System.Globalization.DateTimeStyles.None, out parsed);

                    txtTglLahirPeserta.SelectedDate = parsed;

                    txtUmurPeserta.Text = peserta.Umur.UmurSaatPelayanan;
                    rblJenisKelamin.SelectedIndex = peserta.Sex == "L" ? 0 : 1;

                    txtNoMrPeserta.Text = peserta.Mr.NoMR;
                    var patient = new Patient();
                    if (!string.IsNullOrEmpty(txtNoMrPeserta.Text) && patient.LoadByMedicalNo(txtNoMrPeserta.Text))
                    {
                        var list = new List<Patient>();
                        list.Add(new Patient()
                        {
                            PatientID = patient.PatientID,
                            MedicalNo = patient.MedicalNo,
                            FirstName = patient.FirstName,
                            MiddleName = patient.MiddleName,
                            LastName = patient.LastName
                        });

                        cboNoMRSep.DataSource = list;
                        cboNoMRSep.DataBind();
                        cboNoMRSep.SelectedValue = patient.MedicalNo + "|" + patient.PatientID;
                        cboNoMRSep.Text = patient.MedicalNo + " - " + patient.PatientName;
                    }
                    else
                    {
                        //cboNoMRSep.DataSource = null;
                        //cboNoMRSep.DataBind();
                        cboNoMRSep.Items.Clear();
                        cboNoMRSep.SelectedValue = string.Empty;
                        cboNoMRSep.Text = string.Empty;
                    }

                    txtNoTelpPeserta.Text = peserta.Mr.NoTelepon;
                    if (!string.IsNullOrEmpty(peserta.HakKelas.Kode)) txtHakKelasPeserta.Text = peserta.HakKelas.Kode + " - " + peserta.HakKelas.Keterangan;
                    if (!string.IsNullOrEmpty(peserta.ProvUmum.KdProvider)) txtFaskesTingkat1.Text = peserta.ProvUmum.KdProvider + " - " + peserta.ProvUmum.NmProvider;
                    if (!string.IsNullOrEmpty(peserta.JenisPeserta.Kode)) txtJenisPeserta.Text = peserta.JenisPeserta.Kode + " - " + peserta.JenisPeserta.Keterangan;
                    if (!string.IsNullOrEmpty(peserta.StatusPeserta.Kode)) txtStatusPeserta.Text = peserta.StatusPeserta.Kode + " - " + peserta.StatusPeserta.Keterangan;
                    if (!string.IsNullOrEmpty(peserta.Cob.NoAsuransi)) txtCOBPeserta.Text = peserta.Cob.NoAsuransi + " - " + peserta.Cob.NmAsuransi;
                    ViewState["cobPeserta"] = peserta.Cob.NoAsuransi;

                    cboPelayanan_SelectedIndexChanged(null, new Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, cboPelayanan.SelectedValue, string.Empty));

                    if (pnlRajal.Visible)
                    {
                        chkEksekutifSep.Checked = false;
                        //cboPoliSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = rujukan.PoliRujukan.Kode });
                        //cboPoliSep.SelectedValue = rujukan.PoliRujukan.Kode;
                    }

                    //cboJenisRujukanSep.SelectedValue = cboAsalRujukan.SelectedValue;
                    //if (!string.IsNullOrEmpty(rujukan.ProvPerujuk.Kode))
                    //{
                    //    cboAsalRujukanSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = rujukan.ProvPerujuk.Kode });
                    //    cboAsalRujukanSep.SelectedValue = rujukan.ProvPerujuk.Kode;
                    //    cboAsalRujukanSep.Text = rujukan.ProvPerujuk.Kode + " - " + rujukan.ProvPerujuk.Nama;
                    //}

                    //LoadDetailRujukan(rujukan.NoKunjungan);

                    //DateTime.TryParseExact(rujukan.TglKunjungan, format, null, System.Globalization.DateTimeStyles.None, out parsed);
                    //txtTglRujukanSep.SelectedDate = parsed;

                    //txtNoRujukanSep.Text = rujukan.NoKunjungan;

                    //cboDiagnosaSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = rujukan.Diagnosa.Kode });
                    //cboDiagnosaSep.SelectedValue = rujukan.Diagnosa.Kode;
                    //cboDiagnosaSep.Text = rujukan.Diagnosa.Kode + " - " + rujukan.Diagnosa.Nama;

                    if (pnlRanap.Visible) cboKelasRawatSep.SelectedValue = peserta.HakKelas.Kode;
                    txtNoTeleponSep.Text = peserta.Mr.NoTelepon;
                    txtInformasiPeserta.Text = peserta.Informasi.ProlanisPRB;

                    txtCatatanSep.Text = rujukan.Keluhan;

                    var rjk = new List<Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2>();
                    rjk.Add(new Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2()
                    {
                        NoKunjungan = rujukan.NoKunjungan,
                        TglKunjungan = Convert.ToDateTime(rujukan.TglKunjungan).ToString("yyyy-MM-dd"),
                        ProvPerujuk = new Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.ProvPerujuk() { Nama = rujukan.ProvPerujuk.Nama },
                        PoliRujukan = new Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.PoliRujukan() { Nama = rujukan.PoliRujukan.Nama }
                    });
                    cboNoRujukan.DataSource = rjk.Distinct();
                    cboNoRujukan.DataBind();
                    cboNoRujukan.SelectedValue = rujukan.NoKunjungan;
                    cboNoRujukan_SelectedIndexChanged(null, new Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, cboNoRujukan.SelectedValue, string.Empty));

                    svc = new Common.BPJS.VClaim.v11.Service();
                    var responseJmlRujukan = svc.GetDataJumlahSEPRujukan(cboAsalRujukan.SelectedValue == "1" ? Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1 : Common.BPJS.VClaim.Enum.JenisFaskes.RS, txtNomor.Text);
                    if (responseJmlRujukan.MetaData.IsValid)
                    {
                        if (responseJmlRujukan.Response.JumlahSEP.ToInt() == 0)
                        {
                            TujuanKunjunganNormal(false);
                        }
                        else if (responseJmlRujukan.Response.JumlahSEP.ToInt() > 0)
                        {
                            TujuanKunjunganNonNormal();
                        }
                    }

                    LoadDetailSkdp();
                }
                else ScriptManager.RegisterStartupScript(this, GetType(), "cari", string.Format("alert('Code : {0}, Message : {1}');", response.MetaData.Code, response.MetaData.Message), true);
            }
            else if (rblJenis.SelectedValue == "2")
            {
                svc = new Common.BPJS.VClaim.v11.Service();

                var kartu = rblJenisKartu.SelectedValue == "1" ? Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta : Common.BPJS.VClaim.Enum.SearchPeserta.NIK;
                if (kartu == Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta)
                {
                    if (txtNomor.Text.Length != 13) ScriptManager.RegisterStartupScript(this, GetType(), "cari", string.Format("alert('Code : {0}, Message : {1}');", "000", "No Peserta harus 13 digit"), true);
                }
                else if (kartu == Common.BPJS.VClaim.Enum.SearchPeserta.NIK)
                {
                    if (txtNomor.Text.Length != 16) ScriptManager.RegisterStartupScript(this, GetType(), "cari", string.Format("alert('Code : {0}, Message : {1}');", "000", "No NIK harus 16 digit"), true);
                }

                var response = svc.GetPeserta(kartu, txtNomor.Text, txtTglSep.SelectedDate.Value.Date);
                if (response.MetaData.IsValid)
                {
                    var peserta = response.Response.Peserta;

                    ViewState["ProlanisPRB"] = peserta.Informasi.ProlanisPRB;

                    tglCetakKartu = peserta.TglCetakKartu;
                    txtNamaPeserta.Text = peserta.Nama;
                    txtNikPeserta.Text = peserta.Nik;
                    txtNoPesertaPeserta.Text = peserta.NoKartu;

                    string format = "yyyy-MM-dd";
                    DateTime parsed;
                    DateTime.TryParseExact(peserta.TglLahir, format, null, System.Globalization.DateTimeStyles.None, out parsed);

                    txtTglLahirPeserta.SelectedDate = parsed;

                    txtUmurPeserta.Text = peserta.Umur.UmurSaatPelayanan;
                    rblJenisKelamin.SelectedIndex = peserta.Sex == "L" ? 0 : 1;

                    txtNoMrPeserta.Text = peserta.Mr.NoMR;
                    var patient = new Patient();
                    if (!string.IsNullOrEmpty(txtNoMrPeserta.Text) && patient.LoadByMedicalNo(txtNoMrPeserta.Text))
                    {
                        var list = new List<Patient>();
                        list.Add(new Patient()
                        {
                            PatientID = patient.PatientID,
                            MedicalNo = patient.MedicalNo,
                            FirstName = patient.FirstName,
                            MiddleName = patient.MiddleName,
                            LastName = patient.LastName
                        });

                        cboNoMRSep.DataSource = list;
                        cboNoMRSep.DataBind();
                        cboNoMRSep.SelectedValue = patient.MedicalNo + "|" + patient.PatientID;
                        cboNoMRSep.Text = patient.MedicalNo + " - " + patient.PatientName;
                    }
                    else
                    {
                        //cboNoMRSep.DataSource = null;
                        //cboNoMRSep.DataBind();
                        cboNoMRSep.Items.Clear();
                        cboNoMRSep.SelectedValue = string.Empty;
                        cboNoMRSep.Text = string.Empty;
                    }

                    txtNoTelpPeserta.Text = peserta.Mr.NoTelepon;
                    if (!string.IsNullOrEmpty(peserta.HakKelas.Kode)) txtHakKelasPeserta.Text = peserta.HakKelas.Kode + " - " + peserta.HakKelas.Keterangan;
                    if (!string.IsNullOrEmpty(peserta.ProvUmum.KdProvider)) txtFaskesTingkat1.Text = peserta.ProvUmum.KdProvider + " - " + peserta.ProvUmum.NmProvider;
                    if (!string.IsNullOrEmpty(peserta.JenisPeserta.Kode)) txtJenisPeserta.Text = peserta.JenisPeserta.Kode + " - " + peserta.JenisPeserta.Keterangan;
                    if (!string.IsNullOrEmpty(peserta.StatusPeserta.Kode)) txtStatusPeserta.Text = peserta.StatusPeserta.Kode + " - " + peserta.StatusPeserta.Keterangan;
                    if (!string.IsNullOrEmpty(peserta.Cob.NoAsuransi)) txtCOBPeserta.Text = peserta.Cob.NoAsuransi + " - " + peserta.Cob.NmAsuransi;
                    ViewState["cobPeserta"] = peserta.Cob.NoAsuransi;

                    txtInformasiPeserta.Text = peserta.Informasi.ProlanisPRB;

                    cboPelayanan_SelectedIndexChanged(null, new Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, cboPelayanan.SelectedValue, string.Empty));

                    cboJenisRujukanSep.SelectedIndex = 0;
                    if (!string.IsNullOrEmpty(peserta.ProvUmum.KdProvider))
                    {
                        cboAsalRujukanSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = peserta.ProvUmum.KdProvider });
                        cboAsalRujukanSep.SelectedValue = peserta.ProvUmum.KdProvider;
                        cboAsalRujukanSep.Text = peserta.ProvUmum.KdProvider + " - " + peserta.ProvUmum.NmProvider;
                    }

                    txtTglRujukanSep.SelectedDate = DateTime.Now.Date;

                    //cboNoRujukan.DataSource = null;
                    //cboNoRujukan.DataBind();
                    cboNoRujukan.Items.Clear();
                    cboNoRujukan.SelectedValue = string.Empty;
                    cboNoRujukan.Text = string.Empty;

                    //cboDiagnosaSep.DataSource = null;
                    //cboDiagnosaSep.DataBind();
                    cboDiagnosaSep.Items.Clear();
                    cboDiagnosaSep.SelectedValue = string.Empty;
                    cboDiagnosaSep.Text = string.Empty;

                    if (pnlRanap.Visible) cboKelasRawatSep.SelectedValue = peserta.HakKelas.Kode;
                    txtNoTeleponSep.Text = peserta.Mr.NoTelepon;
                    txtCatatanSep.Text = string.Empty;

                    //rujukan
                    var listRujukan = new List<Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2>();
                    var listRujukanHd = new List<Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2>();

                    var responseMessage = string.Empty;
                    svc = new Common.BPJS.VClaim.v11.Service();
                    var rujukan = svc.GetRujukan(txtNomor.Text, Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);
                    if (rujukan != null)
                    {
                        if (rujukan.MetaData.IsValid && rujukan.Response != null && rujukan.Response.Rujukan != null)
                        {
                            listRujukan.AddRange(rujukan.Response.Rujukan);
                            listRujukanHd.AddRange(rujukan.Response.Rujukan.Where(r => r.PoliRujukan.Kode == "HDL"));
                        }
                        else responseMessage = string.Format("Fakses 1 - Code : {0}, Message : {1}", rujukan.MetaData.Code, rujukan.MetaData.Message);
                    }
                    svc = new Common.BPJS.VClaim.v11.Service();
                    rujukan = svc.GetRujukan(txtNomor.Text, Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                    if (rujukan != null)
                    {
                        if (rujukan.MetaData.IsValid && rujukan.Response != null && rujukan.Response.Rujukan != null)
                        {
                            listRujukan.AddRange(rujukan.Response.Rujukan);
                            listRujukanHd.AddRange(rujukan.Response.Rujukan.Where(r => r.PoliRujukan.Kode == "HDL"));
                        }
                        else responseMessage += string.Format("\nFakses 2 (RS) - Code : {0}, Message : {1}", rujukan.MetaData.Code, rujukan.MetaData.Message);
                    }
                    if (listRujukan.Any(l => txtTglSep.SelectedDate?.Subtract(DateTime.ParseExact(l.TglKunjungan, format, null, System.Globalization.DateTimeStyles.None)).TotalDays <= 90))
                    {
                        if (listRujukanHd.Any())
                        {
                            svc = new Common.BPJS.VClaim.v11.Service();
                            var khusus = svc.GetRujukanKhusus(txtTglSep.SelectedDate.Value.Month, txtTglSep.SelectedDate.Value.Year);
                            var rujukanKhusus = new List<Common.BPJS.VClaim.v20.RujukanKhusus.Select.Rujukan>();
                            if (khusus.MetaData.IsValid && khusus.Response.Rujukan != null)
                                rujukanKhusus = khusus.Response.Rujukan.Where(k => listRujukanHd.Select(h => h.NoKunjungan).Contains(k.Norujukan) && DateTime.ParseExact(k.TglrujukanBerakhir, format, null, System.Globalization.DateTimeStyles.None).Date >= txtTglSep.SelectedDate?.Date).ToList();
                            if (!rujukanKhusus.Any()) listRujukanHd.Clear();
                            else listRujukanHd = listRujukanHd.Where(l => rujukanKhusus.Select(h => h.Norujukan).Contains(l.NoKunjungan)).ToList();
                        }

                        var list = listRujukan.Where(l => txtTglSep.SelectedDate?.Subtract(DateTime.ParseExact(l.TglKunjungan, format, null, System.Globalization.DateTimeStyles.None)).TotalDays <= 90).OrderByDescending(l => DateTime.ParseExact(l.TglKunjungan, format, null, System.Globalization.DateTimeStyles.None)).Distinct().ToList();
                        if (listRujukan.Any()) list.AddRange(listRujukanHd);

                        cboNoRujukan.DataSource = list;
                        cboNoRujukan.DataBind();
                        //if (rujukan.Response != null)
                        {
                            cboNoRujukan.SelectedIndex = 0;
                            cboNoRujukan_SelectedIndexChanged(null, new Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, cboNoRujukan.SelectedValue, string.Empty));
                        }
                    }
                    else
                    {
                        TujuanKunjunganNormal(false);
                    }

                    LoadDetailSkdp();
                    //else ScriptManager.RegisterStartupScript(this, GetType(), "cari", string.Format("alert('{0}');", responseMessage), true);
                }
                else ScriptManager.RegisterStartupScript(this, GetType(), "cari", string.Format("alert('Code : {0}, Message : {1}');", response.MetaData.Code, response.MetaData.Message), true);
            }
            else
            {
                var response = svc.GetSep(txtNomor.Text);
                if (response.MetaData.IsValid)
                {
                    var result = response.Response;
                    if (result != null)
                    {
                        //var bs = new BpjsSEP();
                        //bs.Query.Where(bs.Query.NoSEP == result.NoSep);
                        //if (!bs.Query.Load()) bs = new BpjsSEP();
                        ////bs.SepID,
                        //bs.NoSEP = result.NoSep;
                        //bs.NomorKartu = result.Peserta.NoKartu;
                        //bs.TanggalSEP = result.TglSep;
                        //bs.TanggalRujukan = result.TglSep;
                        //bs.NoRujukan = string.Empty;
                        //bs.PPKRujukan,
                        //bs.NamaPPKRujukan,
                        //bs.PPKPelayanan,
                        //bs.JenisPelayanan,
                        //bs.Catatan,
                        //bs.DiagnosaAwal,
                        //bs.PoliTujuan,
                        //bs.KelasRawat,
                        //bs.LakaLantas,
                        //bs.[User],
                        //bs.NoMR,
                        //bs.TanggalPulang,
                        //bs.NoTransaksi,
                        //bs.NamaPasien,
                        //bs.NIK,
                        //bs.JenisKelamin,
                        //bs.TanggalLahir,
                        //bs.JenisPeserta,
                        //bs.DetailKeanggotaan,
                        //bs.PatientID,
                        //bs.KodeCBG,
                        //bs.TariffCBG,
                        //bs.DeskripsiCBG,
                        //bs.LokasiLaka,
                        //bs.NamaKelasRawat,
                        //bs.IsEksekutif,
                        //bs.IsCob,
                        //bs.PenjaminLaka,
                        //bs.NamaCob,
                        //bs.StatusPeserta,
                        //bs.Umur,
                        //bs.JenisRujukan,
                        //bs.IsKatarak,
                        //bs.TglKejadian,
                        //bs.IsSuplesi,
                        //bs.NoSepSuplesi,
                        //bs.KodePropinsi,
                        //bs.KodeKabupaten,
                        //bs.KodeKecamatan,
                        //bs.NoSkdp,
                        //bs.KodeDpjp
                        //bs.Save();

                        //OnPopulateEntryControl(bs);
                    }
                }
                else ScriptManager.RegisterStartupScript(this, GetType(), "cari", string.Format("alert('Code : {0}, Message : {1}');", response.MetaData.Code, response.MetaData.Message), true);
            }

            //cboDpjpSep.DataSource = null;
            //cboDpjpSep.DataBind();
            cboDpjpSep.Items.Clear();
            cboDpjpSep.SelectedValue = string.Empty;
            cboDpjpSep.Text = string.Empty;

            chkCobSep.Checked = false;
            chkPenjaminKLLSep.Checked = false;
            chkPenjaminKLLSep_CheckedChanged(null, null);

            txtAppointmentSep.Text = string.Empty;

            chkCobSep.Enabled = !string.IsNullOrEmpty(txtCOBPeserta.Text);

            try
            {
                //if (!string.IsNullOrEmpty(tglCetakKartu))
                //{
                svc = new Common.BPJS.VClaim.v11.Service();
                var riwayat = svc.GetDataHistoriPelayananPeserta(txtNoPesertaPeserta.Text, txtTglSep.SelectedDate.Value.Date.AddDays(-89).ToString("yyyy-MM-dd"), txtTglSep.SelectedDate.Value.Date);
                if (riwayat.MetaData.IsValid)
                {
                    if (riwayat.Response.Histori != null)
                    {
                        var list = riwayat.Response.Histori.ToDataTable<Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori>();
                        grdList.DataSource = list;
                        //if (cboPelayanan.SelectedValue == "2")
                        //{
                        //    var riwayatRanapList = list.AsEnumerable().Where(t => t.Field<string>("PpkPelayanan") == ConfigurationManager.AppSettings["BPJSHospitalID"] && t.Field<string>("JnsPelayanan") == "2").Take(1).ToList();
                        //    foreach (var riwayatRanap in riwayatRanapList)
                        //    {

                        //    }
                        //}
                        var data = riwayat.Response.Histori.SingleOrDefault(r => r.TglSep == txtTglSep.SelectedDate?.ToString("yyyy-MM-dd"));
                        if (data != null)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "cari", string.Format("alert('Pasien telah melakukan kunjungan di {0}, pada tanggal {1}{2}');", data.PpkPelayanan, data.TglSep, ", " + data.Poli), true);
                        }
                    }
                    else
                    {
                        var list = new List<Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori>
                            {
                                new Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori()
                                {
                                    Poli = "Tidak ada data."
                                }
                            };
                        grdList.DataSource = list.ToDataTable<Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori>();
                    }
                    grdList.DataBind();
                }
                else
                {
                    var list = new List<Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori>
                        {
                            new Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori()
                            {
                                Poli = $"{riwayat.MetaData.Code} - {riwayat.MetaData.Message}"
                            }
                        };
                    grdList.DataSource = list.ToDataTable<Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori>();
                    grdList.DataBind();
                }
                //}
                //else
                //{
                //    var list = new List<Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori>
                //    {
                //        new Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori()
                //        {
                //            Poli = "Tidak ada data."
                //        }
                //    };
                //    grdList.DataSource = list.ToDataTable<Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori>();
                //    grdList.DataBind();
                //}
            }
            catch (Exception ex)
            {
                var list = new List<Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori>
                {
                    new Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori()
                    {
                        Poli = ex.Message
                    }
                };
                grdList.DataSource = list.ToDataTable<Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori>();
                grdList.DataBind();
            }

            if (cboPelayanan.SelectedValue == "2")
            {
                var bs = new BpjsSEP();
                bs.Query.es.Top = 1;
                bs.Query.Where(bs.Query.TanggalSEP.Date() == txtTglSep.SelectedDate?.Date, bs.Query.NomorKartu == txtNoPesertaPeserta.Text);
                bs.Query.OrderBy(bs.Query.NoSEP.Descending);
                if (bs.Query.Load())
                {
                    if (!string.IsNullOrWhiteSpace(bs.NoTransaksi))
                    {
                        var appt = new BusinessObject.Appointment();
                        appt.LoadByPrimaryKey(bs.NoTransaksi);

                        var unit = new ServiceUnit();
                        if (unit.LoadByPrimaryKey(appt.ServiceUnitID))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "cari",
                                string.Format("alert('Code : {0}, Message : {1}');", "000",
                                    $"Pasien sudah mendapatkan pelayanan di {unit.ServiceUnitName} pada tanggal {txtTglSep.SelectedDate?.ToString("yyyy-MM-dd")}"),
                                true);
                        }
                    }
                    else
                    {
                        var bridgings = new ServiceUnitBridgingCollection();
                        bridgings.Query.Where(bridgings.Query.BridgingID == bs.PoliTujuan,
                            bridgings.Query.SRBridgingType == AppEnum.BridgingType.BPJS.ToString());
                        if (bridgings.Query.Load())
                        {
                            foreach (var bridging in bridgings)
                            {
                                var unit = new ServiceUnit();
                                if (unit.LoadByPrimaryKey(bridging.ServiceUnitID))
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "cari",
                                        string.Format("alert('Code : {0}, Message : {1}');", "000",
                                            $"Pasien sudah mendapatkan pelayanan di {unit.ServiceUnitName} pada tanggal {txtTglSep.SelectedDate?.ToString("yyyy-MM-dd")}"),
                                        true);
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void btnClearData_Click(object sender, EventArgs e)
        {
            OnMenuNewClick();

            //var item = cboTujuanKunjungan.Items.Single(i => i.Value == "0");
            //item.Enabled = true;
        }

        protected void chkCobSep_CheckedChanged(object sender, EventArgs e)
        {
            if (ViewState["cobPeserta"] == null || string.IsNullOrEmpty(ViewState["cobPeserta"].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "cob", string.Format("alert('Code : {0}, Message : {1}');", "000", "Peserta bukan jaminan cob"), true);
                chkCobSep.Checked = false;
            }
        }

        protected void rblJenis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblJenis.SelectedValue == "1")
            {
                pnlFilter.Visible = true;
                pnlRujukan.Visible = true;
                pnlKartu.Visible = false;
                lblNomor.Text = "No Rujukan";
                btnCariRujukan.Visible = false;
            }
            else if (rblJenis.SelectedValue == "2")
            {
                pnlFilter.Visible = true;
                pnlRujukan.Visible = false;
                pnlKartu.Visible = true;
                lblNomor.Text = "No Kartu";
                btnCariRujukan.Visible = false;
            }
            else
            {
                pnlFilter.Visible = false;
                pnlRujukan.Visible = false;
                pnlKartu.Visible = false;
                lblNomor.Text = "No SEP";
                btnCariRujukan.Visible = false;
            }
            txtNomor.Text = string.Empty;
        }

        protected override void OnMenuNewClick()
        {
            ViewState["sepID"] = 0;
            ViewState["cobPeserta"] = string.Empty;
            ViewState["ProlanisPRB"] = string.Empty;

            rblJenis.SelectedIndex = 1;
            rblJenis_SelectedIndexChanged(null, null);

            cboPelayanan.SelectedIndex = 1;
            cboPelayanan_SelectedIndexChanged(null, new Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, "2", string.Empty));
            rblJenisKartu.SelectedIndex = 0;

            txtTglSep.SelectedDate = DateTime.Now.Date;

            txtTglRujukanSep.SelectedDate = DateTime.Now.Date;
            //txtTglSepSep.SelectedDate = DateTime.Now.Date;

            txtNomor.Text = string.Empty;

            txtNamaPeserta.Text = string.Empty;
            txtNikPeserta.Text = string.Empty;
            txtNoPesertaPeserta.Text = string.Empty;
            txtTglLahirPeserta.SelectedDate = null;
            txtUmurPeserta.Text = string.Empty;
            rblJenisKelamin.SelectedIndex = -1;
            txtNoMrPeserta.Text = string.Empty;
            txtNoTelpPeserta.Text = string.Empty;
            txtHakKelasPeserta.Text = string.Empty;
            txtFaskesTingkat1.Text = string.Empty;
            txtJenisPeserta.Text = string.Empty;
            txtStatusPeserta.Text = string.Empty;
            txtCOBPeserta.Text = string.Empty;

            txtNoSep.Text = string.Empty;
            chkEksekutifSep.Checked = false;

            //cboPoliSep.DataSource = null;
            //cboPoliSep.DataBind();
            cboPoliSep.Items.Clear();
            cboPoliSep.SelectedValue = string.Empty;
            cboPoliSep.Text = string.Empty;

            cboJenisRujukanSep.SelectedIndex = 0;

            //cboAsalRujukanSep.DataSource = null;
            //cboAsalRujukanSep.DataBind();
            cboAsalRujukanSep.Items.Clear();
            cboAsalRujukanSep.SelectedValue = string.Empty;
            cboAsalRujukanSep.Text = string.Empty;

            //cboNoRujukan.DataSource = null;
            //cboNoRujukan.DataBind();
            cboNoRujukan.Items.Clear();
            cboNoRujukan.SelectedValue = string.Empty;
            cboNoRujukan.Text = string.Empty;

            //cboNoMRSep.DataSource = null;
            //cboNoMRSep.DataBind();
            cboNoMRSep.Items.Clear();
            cboNoMRSep.SelectedValue = string.Empty;
            cboNoMRSep.Text = string.Empty;

            //cboDiagnosaSep.DataSource = null;
            //cboDiagnosaSep.DataBind();
            cboDiagnosaSep.Items.Clear();
            cboDiagnosaSep.SelectedValue = string.Empty;
            cboDiagnosaSep.Text = string.Empty;

            txtNoTeleponSep.Text = string.Empty;
            txtCatatanSep.Text = string.Empty;
            chkKatarakSep.Checked = false;

            chkCobSep.Checked = false;
            chkCobSep.Enabled = true;

            chkPenjaminKLLSep.Checked = false;
            chkPenjaminKLLSep_CheckedChanged(null, null);

            //cboDpjpSep.DataSource = null;
            //cboDpjpSep.DataBind();
            cboDpjpSep.Items.Clear();
            cboDpjpSep.SelectedValue = string.Empty;
            cboDpjpSep.Text = string.Empty;

            cboDpjpPelayanan.Items.Clear();
            cboDpjpPelayanan.SelectedValue = string.Empty;
            cboDpjpPelayanan.Text = string.Empty;

            txtAppointmentSep.Text = string.Empty;

            chkKatarakSep.Checked = false;

            //cboPoliSep.Enabled = true;
            txtNomor.ReadOnly = false;
            txtTglSep.DateInput.ReadOnly = false;
            txtTglSep.DatePopupButton.Enabled = true;
            //txtTglSepSep.DateInput.ReadOnly = false;
            //txtTglSepSep.DatePopupButton.Enabled = true;

            //cboPelayanan.Enabled = true;
            btnClearData.Enabled = true;
            rblJenisKartu.Enabled = true;
            btnClearData.Enabled = true;

            //cboRegistrasiIGD.DataSource = null;
            //cboRegistrasiIGD.DataBind();
            cboRegistrasiIGD.Items.Clear();
            cboRegistrasiIGD.SelectedValue = string.Empty;
            cboRegistrasiIGD.Text = string.Empty;

            //grdList.DataSource = null;
            //grdList.DataBind();

            ResetComboTujuanKunjungan();

            //cboTujuanKunjungan.SelectedValue = string.Empty;
            //cboFlagProcedure.SelectedValue = string.Empty;
            //cboFlagProcedure_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, string.Empty, string.Empty));

            //cboKodePenunjang.SelectedValue = string.Empty;
            //cboAssesmentPelayanan.SelectedValue = string.Empty;

            cboDpjpKontrol.Items.Clear();
            cboDpjpKontrol.SelectedValue = string.Empty;
            cboDpjpKontrol.Text = string.Empty;

            cboNoSkdp.Items.Clear();
            cboNoSkdp.SelectedValue = string.Empty;
            cboNoSkdp.Text = string.Empty;
        }

        protected override void OnMenuEditClick()
        {
            //cboPoliSep.Enabled = false;
            txtNomor.ReadOnly = true;
            txtTglSep.DateInput.ReadOnly = true;
            txtTglSep.DatePopupButton.Enabled = false;
            //txtTglSepSep.DateInput.ReadOnly = true;
            //txtTglSepSep.DatePopupButton.Enabled = false;

            rblJenis.Enabled = false;
            cboPelayanan.Enabled = false;
            rblJenisKartu.Enabled = false;
            btnClearData.Enabled = false;
        }

        protected void cboAsalRujukanSep_ItemsRequested(object o, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            //if (CheckBpjsWebApi()) return;

            //cboAsalRujukanSep.DataSource = null;
            //cboAsalRujukanSep.DataBind();
            cboAsalRujukanSep.Items.Clear();
            cboAsalRujukanSep.SelectedValue = string.Empty;
            cboAsalRujukan.Text = string.Empty;

            //if (e.Text.Trim().Length < 3) return;

            //var svc = new Common.BPJS.VClaim.Service();
            //var response = svc.GetFaskes(e.Text.Trim(), Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);
            //if (response.MetaData.IsValid)
            //{
            //    var result = response.Response.Faskes;
            //    cboAsalRujukanSep.DataSource = result;
            //    cboAsalRujukanSep.DataBind();
            //}
            //else
            //{
            //    svc = new Common.BPJS.VClaim.Service();
            //    response = svc.GetFaskes(e.Text.Trim(), Common.BPJS.VClaim.Enum.JenisFaskes.RS);
            //    if (response.MetaData.IsValid)
            //    {
            //        var result = response.Response.Faskes;
            //        cboAsalRujukanSep.DataSource = result;
            //        cboAsalRujukanSep.DataBind();
            //    }
            //    else ScriptManager.RegisterStartupScript(this, GetType(), "poli", string.Format("alert('Code : {0}, Message : {1}');", response.MetaData.Code, response.MetaData.Message), true);
            //};

            var count = 0;
            var svc = new Common.BPJS.VClaim.v11.Service();
            var faskes1 = svc.GetFaskes(e.Text.Trim(), Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);
            //if (!faskes1.MetaData.IsValid)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "poli", string.Format("alert('Code : {0}, Message : {1}');", faskes1.MetaData.Code, faskes1.MetaData.Message), true);
            //    return;
            //}
            count += (faskes1 != null && faskes1.Response != null) ? faskes1.Response.Faskes.Count() : 0;

            svc = new Common.BPJS.VClaim.v11.Service();
            var faskes2 = svc.GetFaskes(e.Text.Trim(), Common.BPJS.VClaim.Enum.JenisFaskes.RS);
            //if (!faskes2.MetaData.IsValid)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "poli", string.Format("alert('Code : {0}, Message : {1}');", faskes2.MetaData.Code, faskes2.MetaData.Message), true);
            //    return;
            //}
            count += (faskes2 != null && faskes2.Response != null) ? faskes2.Response.Faskes.Count() : 0;

            var result = new List<Common.BPJS.VClaim.v11.Faskes.Faske>();
            if (faskes1 != null && faskes1.Response != null) result.AddRange(faskes1.Response.Faskes);
            if (faskes2 != null && faskes2.Response != null) result.AddRange(faskes2.Response.Faskes);

            cboAsalRujukanSep.DataSource = result;
            cboAsalRujukanSep.DataBind();
        }

        protected void cboAsalRujukanSep_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((Common.BPJS.VClaim.v11.Faskes.Faske)e.Item.DataItem).Nama;
            e.Item.Value = ((Common.BPJS.VClaim.v11.Faskes.Faske)e.Item.DataItem).Kode;
        }

        protected void cboPoliSep_ItemsRequested(object o, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            //if (CheckBpjsWebApi()) return;

            //cboPoliSep.DataSource = null;
            //cboPoliSep.DataBind();
            //cboPoliSep.Items.Clear();
            //cboPoliSep.SelectedValue = string.Empty;

            //if (e.Text.Trim().Length < 3) return;

            //var svc = new Common.BPJS.VClaim.Service();
            //var response = svc.GetPoli(e.Text.Trim());
            //if (response.MetaData.IsValid)
            //{
            //    var result = response.Response.Poli;
            //    cboPoliSep.DataSource = result;
            //    cboPoliSep.DataBind();
            //}
            //else ScriptManager.RegisterStartupScript(this, GetType(), "poli", string.Format("alert('Code : {0}, Message : {1}');", response.MetaData.Code, response.MetaData.Message), true);

            var sub = new ServiceUnitBridgingQuery("a");
            var su = new ServiceUnitQuery("b");

            sub.Select(sub.BridgingID, sub.ServiceUnitID, "<CASE WHEN a.BridgingName = '' THEN b.ServiceUnitName ELSE a.BridgingName END AS ServiceUnitName>");
            sub.InnerJoin(su).On(sub.ServiceUnitID == su.ServiceUnitID && sub.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(), AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString(), AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString()));
            //sub.Where(string.Format("<CASE WHEN a.BridgingName = '' THEN b.ServiceUnitName ELSE a.BridgingName END LIKE '%{0}%'>", e.Text));

            if (!string.IsNullOrWhiteSpace(e.Message)) sub.Where(sub.ServiceUnitID == e.Message);
            else if (!string.IsNullOrWhiteSpace(e.Text)) sub.Where(sub.BridgingID == e.Text);

            var dtb = sub.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
                if (new string[] { "hdl", "irm", "igd" }.Contains(row["BridgingID"].ToString().ToLower())) continue;

                var bridging = new ServiceUnitBridging();
                bridging.Query.Where(bridging.Query.BridgingID == row["BridgingID"].ToString(), bridging.Query.BridgingName == row["ServiceUnitName"].ToString());
                if (!bridging.Query.Load()) continue;
                var psds = new ParamedicScheduleDateCollection();
                psds.Query.Where(psds.Query.ServiceUnitID == bridging.ServiceUnitID, psds.Query.ScheduleDate.Date() == txtTglSep.SelectedDate?.Date);
                if (!psds.Query.Load())
                {
                    row.Delete();
                    continue;
                }

                //var total = 0;
                //foreach (var psd in psds)
                //{
                //    var ot = new OperationalTime();
                //    ot.LoadByPrimaryKey(psd.OperationalTimeID);

                //    var jam = TimeSpan.ParseExact(DateTime.Now.ToString("HH:mm"), "hh\\:mm", null);
                //    var ot1 = new TimeSpan();
                //    var ot2 = new TimeSpan();

                //    if (!string.IsNullOrWhiteSpace(ot.StartTime1) && !string.IsNullOrWhiteSpace(ot.EndTime1))
                //    {
                //        ot1 = TimeSpan.ParseExact(ot.StartTime1, "hh\\:mm", null);
                //        ot2 = TimeSpan.ParseExact(ot.EndTime1, "hh\\:mm", null);
                //    }

                //    if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                //    {
                //        ot1 = TimeSpan.ParseExact(ot.StartTime2, "hh\\:mm", null);
                //        ot2 = TimeSpan.ParseExact(ot.EndTime2, "hh\\:mm", null);
                //    }

                //    if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                //    {
                //        ot1 = TimeSpan.ParseExact(ot.StartTime3, "hh\\:mm", null);
                //        ot2 = TimeSpan.ParseExact(ot.EndTime3, "hh\\:mm", null);
                //    }

                //    if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                //    {
                //        ot1 = TimeSpan.ParseExact(ot.StartTime4, "hh\\:mm", null);
                //        ot2 = TimeSpan.ParseExact(ot.EndTime4, "hh\\:mm", null);
                //    }

                //    if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                //    {
                //        ot1 = TimeSpan.ParseExact(ot.StartTime5, "hh\\:mm", null);
                //        ot2 = TimeSpan.ParseExact(ot.EndTime5, "hh\\:mm", null);
                //    }

                //    if (jam >= ot1 && jam <= ot2)
                //    {
                //        total++;
                //        continue;
                //    }
                //}
                //if (total == 0) row.Delete();
            }
            dtb.AcceptChanges();
            if (dtb.Rows.Count > 0)
            {
                cboPoliSep.DataSource = dtb;
                cboPoliSep.DataBind();
            }
        }

        protected void cboPoliSep_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            //e.Item.Text = ((Common.BPJS.VClaim.Referensi.Poli.Data)e.Item.DataItem).Nama;
            //e.Item.Value = ((Common.BPJS.VClaim.Referensi.Poli.Data)e.Item.DataItem).Kode;

            e.Item.Text = ((DataRowView)e.Item.DataItem)["BridgingID"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["BridgingID"].ToString() + "#" + ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboDiagnosaSep_ItemsRequested(object o, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            //if (CheckBpjsWebApi()) return;
            if (string.IsNullOrWhiteSpace(e.Text)) return;


            //cboDiagnosaSep.DataSource = null;
            //cboDiagnosaSep.DataBind();
            cboDiagnosaSep.Items.Clear();
            cboDiagnosaSep.SelectedValue = string.Empty;
            cboDiagnosaSep.Text = string.Empty;

            //if (e.Text.Trim().Length < 3) return;

            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetDiagnosa(e.Text.Trim());
            if (response.MetaData.IsValid)
            {
                var result = response.Response.Diagnosa;
                cboDiagnosaSep.DataSource = result;
                cboDiagnosaSep.DataBind();
            }
            else ScriptManager.RegisterStartupScript(this, GetType(), "diag", string.Format("alert('Code : {0}, Message : {1}');", response.MetaData.Code, response.MetaData.Message), true);
        }

        protected void cboDiagnosaSep_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((Common.BPJS.VClaim.v11.Diagnosa.Diagnosa2)e.Item.DataItem).Nama;
            e.Item.Value = ((Common.BPJS.VClaim.v11.Diagnosa.Diagnosa2)e.Item.DataItem).Kode;
        }

        protected void cboNoRujukan_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2)e.Item.DataItem).NoKunjungan;
            e.Item.Value = ((Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2)e.Item.DataItem).NoKunjungan;
        }

        protected void cboNoRujukan_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value)) return;
            LoadDetailRujukan(e.Value);
        }

        private void LoadDetailSkdp()
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            var kontrolResponse = svc.GetRencanaKontrolByNoPeserta(txtTglSep.SelectedDate?.ToString("MM"), txtTglSep.SelectedDate?.Year.ToString(), txtNomor.Text, Common.BPJS.VClaim.Enum.FilterRencanaKontrol.TanggalRencanaKontrol);
            if (kontrolResponse.MetaData.IsValid)
            {
                //var listKontrol = new List<Common.BPJS.VClaim.v11.RencanaKontrol.Select.ResponseSuratKontrolList.List>();
                //foreach(var item in kontrolResponse.Response.List)
                //{
                //    listKontrol.Add(item);   
                //}
                if (kontrolResponse.Response.List.Any(l => l.JnsKontrol == cboPelayanan.SelectedValue && l.TerbitSEP.ToLower().Trim() == "belum"))
                {
                    cboNoSkdp.DataSource = kontrolResponse.Response.List.Where(l => l.JnsKontrol == cboPelayanan.SelectedValue && l.TerbitSEP.ToLower().Trim() == "belum").Select(l => new SkdpSource()
                    {
                        NoSuratKontrol = l.NoSuratKontrol,
                        TglRencanaKontrol = l.TglRencanaKontrol,
                        NamaPoliTujuan = l.NamaPoliTujuan,
                        NamaDokter = l.NamaDokter,
                        JenisSuratKontrol = l.JnsKontrol == "1" ? "Rawat Inap" : "Rawat Jalan"
                    }).ToList();
                    cboNoSkdp.DataBind();

                    Temiang.Avicenna.Common.BPJS.VClaim.v11.RencanaKontrol.Select.ResponseSuratKontrolList.List kontrol;
                    //if (cboPelayanan.SelectedValue == "2") kontrol =
                    kontrol = kontrolResponse.Response.List.OrderByDescending(l => l.TglRencanaKontrol).FirstOrDefault(l => l.JnsKontrol == cboPelayanan.SelectedValue && l.TerbitSEP.ToLower().Trim() == "belum" && l.TglRencanaKontrol == txtTglSep.SelectedDate?.ToString("yyyy-MM-dd") && l.PoliTujuan == cboPoliSep.SelectedValue.Split('#')[0]);
                    //else
                    //kontrol = kontrolResponse.Response.List.OrderByDescending(l => l.TglRencanaKontrol).FirstOrDefault(l => l.JnsKontrol == cboPelayanan.SelectedValue && l.TerbitSEP.ToLower().Trim() == "belum" && l.TglRencanaKontrol == txtTglSep.SelectedDate?.ToString("yyyy-MM-dd"));
                    if (kontrol != null)
                    {
                        cboNoSkdp.SelectedValue = kontrol.NoSuratKontrol;
                        cboNoSkdp_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, kontrol.NoSuratKontrol, string.Empty));
                    }
                }
                else
                {
                    cboNoSkdp.Items.Clear();
                    cboNoSkdp.SelectedValue = string.Empty;
                    cboNoSkdp.Text = string.Empty;

                    cboDpjpKontrol.Items.Clear();
                    cboDpjpKontrol.SelectedValue = string.Empty;
                    cboDpjpKontrol.Text = string.Empty;
                }
            }
            else
            {
                cboNoSkdp.Items.Clear();
                cboNoSkdp.SelectedValue = string.Empty;
                cboNoSkdp.Text = string.Empty;

                cboDpjpKontrol.Items.Clear();
                cboDpjpKontrol.SelectedValue = string.Empty;
                cboDpjpKontrol.Text = string.Empty;

                if (cboPoliSep.SelectedValue.Split('#')[0].ToLower() != "igd") ScriptManager.RegisterStartupScript(this, GetType(), "cari", "alert('Surat kontrol tidak ditemukan');", true);
            }
        }

        private void LoadDetailRujukan(string noRujukan)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = new Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan();
            bool isValid = false;

            response = svc.GetRujukan(true, noRujukan, Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);
            isValid = response.MetaData.IsValid;
            //if (isValid) cboJenisRujukanSep.SelectedValue = "1";

            string format = "yyyy-MM-dd";

            if (!isValid)
            {
                svc = new Common.BPJS.VClaim.v11.Service();
                response = svc.GetRujukan(true, noRujukan, Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                isValid = response.MetaData.IsValid;
                if (isValid)
                {
                    var reffer = response.Response.Rujukan;

                    if (reffer.PoliRujukan != null)
                    {
                        cboPoliSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = reffer.PoliRujukan.Kode });
                        foreach (RadComboBoxItem item in cboPoliSep.Items)
                        {
                            if (item.Value.Split('#')[0] == reffer.PoliRujukan.Kode)
                            {
                                if (cboPoliSep.SelectedValue != item.Value)
                                {
                                    cboPoliSep.SelectedValue = item.Value;
                                    cboPoliSep.Text = cboPoliSep.SelectedItem.Text;
                                }
                                break;
                            }
                        }
                        //cboPoliSep.SelectedValue = reffer.PoliRujukan.Kode;
                        //if (cboPoliSep.Items.Any()) cboPoliSep.SelectedIndex = 0;
                        cboJenisRujukanSep.SelectedValue = "2";
                    }

                    if (!string.IsNullOrEmpty(reffer.ProvPerujuk.Kode))
                    {
                        cboAsalRujukanSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = reffer.ProvPerujuk.Kode });
                        cboAsalRujukanSep.SelectedValue = reffer.ProvPerujuk.Kode;
                        cboAsalRujukanSep.Text = reffer.ProvPerujuk.Kode + " - " + reffer.ProvPerujuk.Nama;
                    }

                    DateTime parsed;
                    DateTime.TryParseExact(reffer.TglKunjungan, format, null, System.Globalization.DateTimeStyles.None, out parsed);
                    txtTglRujukanSep.SelectedDate = parsed;

                    cboDiagnosaSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = reffer.Diagnosa.Kode });
                    cboDiagnosaSep.SelectedValue = reffer.Diagnosa.Kode;
                    //cboDiagnosaSep.Text = reffer.Diagnosa.Kode + " - " + reffer.Diagnosa.Nama;

                    //var selisih = txtTglSep.SelectedDate.Value.Date.Subtract(DateTime.ParseExact(reffer.TglKunjungan, format, null, System.Globalization.DateTimeStyles.None).Date).TotalDays;
                    //if (selisih > 90) ScriptManager.RegisterStartupScript(this, GetType(), "cari", "alert('No Rujukan sudah tidak berlaku, lebih dari 90 hari');", true);
                }
                //else
                //{
                //    TujuanKunjunganNormal();
                //}
            }
            else
            {
                var reffer = response.Response.Rujukan;

                if (reffer.PoliRujukan != null)
                {
                    cboPoliSep_ItemsRequested(null,
                        new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = reffer.PoliRujukan.Kode });
                    cboPoliSep.SelectedValue = reffer.PoliRujukan.Kode;
                    if (cboPoliSep.Items.Any())
                    {
                        cboPoliSep.SelectedIndex = 0;
                        foreach (RadComboBoxItem item in cboPoliSep.Items)
                        {
                            if (item.Value.Split('#')[0] == reffer.PoliRujukan.Kode)
                            {
                                if (cboPoliSep.SelectedValue != item.Value)
                                {
                                    cboPoliSep.SelectedValue = item.Value;
                                    cboPoliSep.Text = cboPoliSep.SelectedItem.Text;
                                }
                                break;
                            }
                        }
                        //cboPoliSep.SelectedValue = reffer.PoliRujukan.Kode;
                        //cboPoliSep.Text = cboPoliSep.SelectedItem.Text;
                    }

                    cboJenisRujukanSep.SelectedValue = "1";
                }

                if (!string.IsNullOrEmpty(reffer.ProvPerujuk.Kode))
                {
                    cboAsalRujukanSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = reffer.ProvPerujuk.Kode });
                    cboAsalRujukanSep.SelectedValue = reffer.ProvPerujuk.Kode;
                    cboAsalRujukanSep.Text = reffer.ProvPerujuk.Kode + " - " + reffer.ProvPerujuk.Nama;
                }

                DateTime parsed;
                DateTime.TryParseExact(reffer.TglKunjungan, format, null, System.Globalization.DateTimeStyles.None, out parsed);
                txtTglRujukanSep.SelectedDate = parsed;

                cboDiagnosaSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = reffer.Diagnosa.Kode });
                cboDiagnosaSep.SelectedValue = reffer.Diagnosa.Kode;
                cboDiagnosaSep.Text = reffer.Diagnosa.Kode + " - " + reffer.Diagnosa.Nama;

                var selisih = txtTglSep.SelectedDate.Value.Date.Subtract(DateTime.ParseExact(reffer.TglKunjungan, format, null, System.Globalization.DateTimeStyles.None).Date).TotalDays;
                if (selisih > 90)
                {
                    if (reffer.PoliRujukan.Kode == "HDL")
                    {
                        svc = new Service();
                        var khusus = svc.GetRujukanKhusus(txtTglSep.SelectedDate?.Month ?? 0, txtTglSep.SelectedDate?.Year ?? 0);
                        if (khusus.MetaData.IsValid && khusus.Response.Rujukan != null)
                        {
                            if (!khusus.Response.Rujukan.Any(k =>
                                    k.Norujukan == noRujukan &&
                                    DateTime.ParseExact(k.TglrujukanBerakhir, format, null, DateTimeStyles.None).Date >=
                                    txtTglSep.SelectedDate?.Date))
                            {
                                var rk = khusus.Response.Rujukan.SingleOrDefault(k => k.Norujukan == noRujukan && DateTime.ParseExact(k.TglrujukanBerakhir, format, null, DateTimeStyles.None).Date >= txtTglSep.SelectedDate?.Date);
                                ScriptManager.RegisterStartupScript(this, GetType(), "cari",
                                    rk != null
                                        ? $"alert('No Rujukan Khusus sudah tidak berlaku, lebih dari tanggal {khusus.Response.Rujukan.Single(k => k.Norujukan == noRujukan).TglrujukanBerakhir}');"
                                        : $"alert('No Rujukan Khusus tidak ditemukan atas No Rujukan {noRujukan}');",
                                    true);
                            }
                            else
                                txtTglRujukanSep.SelectedDate = DateTime.ParseExact(
                                    khusus.Response.Rujukan.Single(k => k.Norujukan == noRujukan && DateTime.ParseExact(k.TglrujukanBerakhir, format, null, DateTimeStyles.None).Date >= txtTglSep.SelectedDate?.Date).TglrujukanAwal, format, null,
                                    DateTimeStyles.None).Date;
                        }
                    }
                    else ScriptManager.RegisterStartupScript(this, GetType(), "cari", "alert('No Rujukan sudah tidak berlaku, lebih dari 90 hari');", true);
                }
            }

            if (cboPelayanan.SelectedValue == "2")
            {
                svc = new Common.BPJS.VClaim.v11.Service();
                var responseJmlRujukan = svc.GetDataJumlahSEPRujukan(cboJenisRujukanSep.SelectedValue == "1" ? Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1 : Common.BPJS.VClaim.Enum.JenisFaskes.RS, noRujukan);
                if (responseJmlRujukan.MetaData.IsValid)
                {
                    if (responseJmlRujukan.Response.JumlahSEP.ToInt() < 1)
                    {
                        //cboTujuanKunjungan.SelectedValue = "0";
                        //cboFlagProcedure.SelectedValue = string.Empty;
                        //cboFlagProcedure_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, string.Empty, string.Empty));
                        //cboKodePenunjang.SelectedValue = string.Empty;
                        //cboAssesmentPelayanan.SelectedValue = string.Empty;

                        TujuanKunjunganNormal(false);

                        cboNoSkdp.Items.Clear();
                        cboNoSkdp.SelectedValue = string.Empty;
                        cboNoSkdp.Text = string.Empty;

                        cboDpjpKontrol.Items.Clear();
                        cboDpjpKontrol.SelectedValue = string.Empty;
                        cboDpjpKontrol.Text = string.Empty;

                        cboDpjpPelayanan.Items.Clear();
                        cboDpjpPelayanan.SelectedValue = string.Empty;
                        cboDpjpPelayanan.Text = string.Empty;
                    }
                    else if (responseJmlRujukan.Response.JumlahSEP.ToInt() >= 1)
                    {
                        TujuanKunjunganNonNormal();

                        cboNoSkdp.Items.Clear();
                        cboNoSkdp.SelectedValue = string.Empty;
                        cboNoSkdp.Text = string.Empty;

                        cboDpjpKontrol.Items.Clear();
                        cboDpjpKontrol.SelectedValue = string.Empty;
                        cboDpjpKontrol.Text = string.Empty;

                        cboDpjpPelayanan.Items.Clear();
                        cboDpjpPelayanan.SelectedValue = string.Empty;
                        cboDpjpPelayanan.Text = string.Empty;

                        svc = new Common.BPJS.VClaim.v11.Service();
                        var kontrolResponse = svc.GetRencanaKontrolByNoPeserta(txtTglSep.SelectedDate?.ToString("MM"), txtTglSep.SelectedDate?.Year.ToString(), txtNomor.Text, Common.BPJS.VClaim.Enum.FilterRencanaKontrol.TanggalRencanaKontrol);
                        if (kontrolResponse.MetaData.IsValid)
                        {
                            //var listKontrol = new List<Common.BPJS.VClaim.v11.RencanaKontrol.Select.ResponseSuratKontrolList.List>();
                            //foreach(var item in kontrolResponse.Response.List)
                            //{
                            //    listKontrol.Add(item);   
                            //}
                            cboNoSkdp.DataSource = kontrolResponse.Response.List.Where(l => l.JnsKontrol == cboPelayanan.SelectedValue && l.TerbitSEP.ToLower().Trim() == "belum").Select(l => new SkdpSource()
                            {
                                NoSuratKontrol = l.NoSuratKontrol,
                                TglRencanaKontrol = l.TglRencanaKontrol,
                                NamaPoliTujuan = l.NamaPoliTujuan,
                                NamaDokter = l.NamaDokter,
                                JenisSuratKontrol = l.JnsKontrol == "1" ? "Rawat Inap" : "Rawat Jalan"
                            }).ToList();
                            cboNoSkdp.DataBind();

                            Temiang.Avicenna.Common.BPJS.VClaim.v11.RencanaKontrol.Select.ResponseSuratKontrolList.List kontrol;
                            kontrol = cboPelayanan.SelectedValue == "2" ? kontrolResponse.Response.List.OrderByDescending(l => l.TglRencanaKontrol).FirstOrDefault(l => l.TglRencanaKontrol == txtTglSep.SelectedDate?.ToString("yyyy-MM-dd") && l.PoliTujuan == cboPoliSep.SelectedValue.Split('#')[0] && l.TerbitSEP.ToLower().Trim() == "belum") :
                                kontrolResponse.Response.List.OrderByDescending(l => l.TglRencanaKontrol).FirstOrDefault(l => l.TglRencanaKontrol == txtTglSep.SelectedDate?.ToString("yyyy-MM-dd") && l.JnsKontrol == cboPelayanan.SelectedValue && l.TerbitSEP.ToLower().Trim() == "belum");
                            if (kontrol != null)
                            {
                                cboNoSkdp.SelectedValue = kontrol.NoSuratKontrol;
                                cboNoSkdp_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, kontrol.NoSuratKontrol, string.Empty));
                            }
                        }
                        else
                        {
                            if (cboPoliSep.SelectedValue.Split('#')[0].ToLower() != "igd") ScriptManager.RegisterStartupScript(this, GetType(), "cari", "alert('Surat kontrol tidak ditemukan');", true);
                        }

                        //cboTujuanKunjungan.SelectedValue = "2";
                        //cboFlagProcedure.SelectedValue = string.Empty;
                        //cboFlagProcedure_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, string.Empty, string.Empty));
                        //cboKodePenunjang.SelectedValue = string.Empty;
                        //cboAssesmentPelayanan.SelectedValue = string.Empty;
                    }
                }
                else
                {
                    TujuanKunjunganNormal(false);

                    cboNoSkdp.Items.Clear();
                    cboNoSkdp.SelectedValue = string.Empty;
                    cboNoSkdp.Text = string.Empty;

                    cboDpjpKontrol.Items.Clear();
                    cboDpjpKontrol.SelectedValue = string.Empty;
                    cboDpjpKontrol.Text = string.Empty;

                    //svc = new Common.BPJS.VClaim.v11.Service();
                    //var kontrolResponse = svc.GetRencanaKontrolByNoPeserta(txtTglSep.SelectedDate?.ToString("MM"), txtTglSep.SelectedDate?.Year.ToString(), txtNomor.Text, Common.BPJS.VClaim.Enum.FilterRencanaKontrol.TanggalRencanaKontrol);
                    //if (kontrolResponse.MetaData.IsValid)
                    //{
                    //    //var listKontrol = new List<Common.BPJS.VClaim.v11.RencanaKontrol.Select.ResponseSuratKontrolList.List>();
                    //    //foreach(var item in kontrolResponse.Response.List)
                    //    //{
                    //    //    listKontrol.Add(item);   
                    //    //}
                    //    cboNoSkdp.DataSource = kontrolResponse.Response.List.Select(l => new SkdpSource()
                    //    {
                    //        NoSuratKontrol = l.NoSuratKontrol,
                    //        TglRencanaKontrol = l.TglRencanaKontrol,
                    //        NamaPoliTujuan = l.NamaPoliTujuan,
                    //        NamaDokter = l.NamaDokter,
                    //        JenisSuratKontrol = l.JnsKontrol == "1" ? "Rawat Inap" : "Rawat Jalan"
                    //    }).ToList();
                    //    cboNoSkdp.DataBind();

                    //    Temiang.Avicenna.Common.BPJS.VClaim.v11.RencanaKontrol.Select.ResponseSuratKontrolList.List kontrol;
                    //    if (cboPelayanan.SelectedValue == "2") kontrol = kontrolResponse.Response.List.OrderByDescending(l => l.TglRencanaKontrol).FirstOrDefault(l => l.TglRencanaKontrol == txtTglSep.SelectedDate?.ToString("yyyy-MM-dd") && l.PoliTujuan == cboPoliSep.SelectedValue);
                    //    else kontrol = kontrolResponse.Response.List.OrderByDescending(l => l.TglRencanaKontrol).FirstOrDefault(l => l.TglRencanaKontrol == txtTglSep.SelectedDate?.ToString("yyyy-MM-dd") && l.JnsKontrol == cboPelayanan.SelectedValue);
                    //    if (kontrol != null)
                    //    {
                    //        cboNoSkdp.SelectedValue = kontrol.NoSuratKontrol;
                    //        cboNoSkdp_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, kontrol.NoSuratKontrol, string.Empty));
                    //    }
                    //}
                }
            }
            else
            {
                //cboTujuanKunjungan.SelectedValue = "0";
                //cboFlagProcedure.SelectedValue = string.Empty;
                //cboFlagProcedure_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, string.Empty, string.Empty));
                //cboKodePenunjang.SelectedValue = string.Empty;
                //cboAssesmentPelayanan.SelectedValue = string.Empty;

                TujuanKunjunganNormal(false);

                cboNoSkdp.Items.Clear();
                cboNoSkdp.SelectedValue = string.Empty;
                cboNoSkdp.Text = string.Empty;

                cboDpjpKontrol.Items.Clear();
                cboDpjpKontrol.SelectedValue = string.Empty;
                cboDpjpKontrol.Text = string.Empty;
            }
        }

        protected void cboPelayanan_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value == "1")
            {
                //if (CheckBpjsWebApi()) return;

                //cboPoliSep.DataSource = null;
                //cboPoliSep.DataBind();
                cboPoliSep.Items.Clear();
                cboPoliSep.SelectedValue = string.Empty;
                cboPoliSep.Text = string.Empty;

                pnlRajal.Visible = false;
                pnlRanap.Visible = true;

                var kls = new List<Common.BPJS.VClaim.v11.KelasRawat.List>();
                kls.Add(new Temiang.Avicenna.Common.BPJS.VClaim.v11.KelasRawat.List()
                {
                    Kode = "1",
                    Nama = "Kelas 1"
                });
                kls.Add(new Temiang.Avicenna.Common.BPJS.VClaim.v11.KelasRawat.List()
                {
                    Kode = "2",
                    Nama = "Kelas 2"
                });
                kls.Add(new Temiang.Avicenna.Common.BPJS.VClaim.v11.KelasRawat.List()
                {
                    Kode = "3",
                    Nama = "Kelas 3"
                });
                cboKelasRawatSep.DataSource = kls;
                cboKelasRawatSep.DataBind();
                cboKelasRawatSep.SelectedIndex = -1;

                pnlRegIGD.Visible = true;
            }
            else
            {
                //cboPoliSep.DataSource = null;
                //cboPoliSep.DataBind();
                cboPoliSep.Items.Clear();
                cboPoliSep.SelectedValue = string.Empty;
                cboPoliSep.Text = string.Empty;

                pnlRajal.Visible = true;

                //cboKelasRawatSep.DataSource = null;
                //cboKelasRawatSep.DataBind();
                cboKelasRawatSep.Items.Clear();
                cboKelasRawatSep.SelectedValue = string.Empty;
                cboKelasRawatSep.Text = string.Empty;

                pnlRanap.Visible = false;
                pnlRegIGD.Visible = true;
            }
        }

        private static string GetFullName(string firstName, string middleName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName)) return string.Empty;

            var name = firstName.Trim();
            if (string.IsNullOrEmpty(middleName.Trim())) name = name + " " + middleName.Trim();
            if (string.IsNullOrEmpty(lastName.Trim())) name = name + " " + lastName.Trim();

            return name;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            HideInformationHeader();

            var pat = new PatientQuery();
            pat.Where(pat.GuarantorCardNo == txtNoPesertaPeserta.Text);
            if (pat.IsBlackList == true)
            {

                args.MessageText = "Pasien di BlackList";
                args.IsCancel = true;
                return;
            }

            //if (CheckBpjsWebApi(args)) return;

            //if (string.IsNullOrEmpty(cboDpjpSep.SelectedValue) && !string.IsNullOrEmpty(cboDpjpSep.Text))
            //{
            //    args.MessageText = "Nama dokter dpjp tidak terdaftar";
            //    args.IsCancel = true;
            //    return;
            //}

            //var sep = new BpjsSEP();
            //sep.Query.es.Top = 1;
            //sep.Query.Where(
            //    sep.Query.TanggalSEP == txtTglSepSep.SelectedDate.Value.Date,
            //    sep.Query.JenisPelayanan == cboPelayanan.SelectedValue,
            //    sep.Query.NomorKartu == txtNoPesertaPeserta.Text,
            //    sep.Query.PoliTujuan.NotIn(AppSession.Parameter.BpjsIgdUgdBridgingID)
            //    );
            //if (sep.Query.Load())
            //{
            //    if (cboPelayanan.SelectedValue == "2")
            //    {
            //        args.MessageText = "Pasien telah melakukan registrasi SEP Rawat Jalan";
            //        args.IsCancel = true;
            //        return;
            //    }
            //}

            if (cboPelayanan.SelectedValue == "1")
            {
                var kelas = txtHakKelasPeserta.Text.Split('-')[0].Trim();
                if (cboKelasRawatSep.SelectedValue.ToInt() < kelas.ToInt())
                {
                    args.MessageText = "Kelas rawat tidak boleh lebih besar dari hak kelas peserta";
                    args.IsCancel = true;
                    return;
                }

                if (string.IsNullOrWhiteSpace(cboNoSkdp.SelectedValue))
                {
                    args.MessageText = "No skdp rawat inap harus diisi dengan no surat kontrol/spri";
                    args.IsCancel = true;
                    return;
                }

                //if (string.IsNullOrWhiteSpace(cboRegistrasiIGD.SelectedValue))
                //{
                //    args.MessageText = "No registrasi IGD / RJ harus diisi";
                //    args.IsCancel = true;
                //    return;
                //}
            }

            //if (txtTglSep.SelectedDate.Value.Date > DateTime.Now.Date)
            //{
            //    args.MessageText = "Tanggal SEP lebih dari tanggal hari ini";
            //    args.IsCancel = true;
            //    return;
            //}

            if (txtTglRujukanSep.SelectedDate.Value.Date > txtTglSep.SelectedDate.Value.Date)
            {
                args.MessageText = "Tanggal rujukan lebih dari tanggal SEP";
                args.IsCancel = true;
                return;
            }

            //if (txtTglSep.SelectedDate.Value.Date < DateTime.Now.Date.AddDays(-3))
            //{
            //    args.MessageText = "Tanggal SEP lebih dari 3 x 24 jam";
            //    args.IsCancel = true;
            //    return;
            //}

            if (chkPenjaminKLLSep.Checked)
            {
                if (txtTglKejadianSep.SelectedDate.Value.Date > txtTglSep.SelectedDate.Value.Date)
                {
                    args.MessageText = "Tanggal kejadian KLL lebih dari tanggal SEP";
                    args.IsCancel = true;
                    return;
                }

                if (chkPenjaminKLLSep.Checked && !chkSuplesiSep.Checked)
                {
                    if (string.IsNullOrEmpty(cboPropinsiSep.SelectedValue))
                    {
                        args.MessageText = "Kode provinsi tidak boleh kosong";
                        args.IsCancel = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(cboKabupatenSep.SelectedValue))
                    {
                        args.MessageText = "Kode kabupaten tidak boleh kosong";
                        args.IsCancel = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(cboKecamatanSep.SelectedValue))
                    {
                        args.MessageText = "Kode kecamatan tidak boleh kosong";
                        args.IsCancel = true;
                        return;
                    }
                }
            }

            var entity = new BpjsSEP();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity, args);
        }

        protected string IsNewPatient
        {
            get
            {
                if (string.IsNullOrWhiteSpace(txtNoPesertaPeserta.Text)) return "0|";
                var patient = new PatientCollection();
                patient.Query.Where(patient.Query.GuarantorCardNo == txtNoPesertaPeserta.Text);
                if (!patient.Query.Load()) return $"0|Data pasien dengan no peserta {txtNoPesertaPeserta.Text} tidak ditemukan";
                if (patient.Count > 1) return $"0|Duplikasi pasien dengan no peserta {txtNoPesertaPeserta.Text}";
                return "1|" + txtNoPesertaPeserta.Text;
            }
        }

        public class SkdpSource
        {
            public string NoSuratKontrol { get; set; }
            public string TglRencanaKontrol { get; set; }
            public string NamaPoliTujuan { get; set; }
            public string NamaDokter { get; set; }
            public string JenisSuratKontrol { get; set; }
        }

        public class SepSource
        {
            public DateTime TglSep { get; set; }
            public string Pelayanan { get; set; }
            public string NoPeserta { get; set; }
            public string SkdpSep { get; set; }

            public string HakKelasPeserta { get; set; }
            public string KelasRawatSep { get; set; }
            public DateTime TglRujukanSep { get; set; }
            public bool IsPenjaminKLLSep { get; set; }
            public DateTime TglKejadianSep { get; set; }
            public bool IsSuplesiSep { get; set; }
            public string PropinsiSep { get; set; }
            public string KabupatenSep { get; set; }
            public string KecamatanSep { get; set; }
            public string NoSep { get; set; }

        }

        //private static void SetBpjsSEPValue(BpjsSEP bs, SepSource txt)
        //{
        //    //bs.SepID,
        //    bs.NoSEP = txt.NoSep;
        //    bs.NomorKartu = txt.NoPeserta;
        //    bs.TanggalSEP = txt.TglSep.Date;
        //    bs.TanggalRujukan = txt.TglRujukanSep.Date;
        //    bs.NoRujukan = txt.NoRujukan;
        //    bs.PPKRujukan = txt.AsalRujukanSep;
        //    bs.NamaPPKRujukan = txt.AsalRujukanSep;
        //    bs.PPKPelayanan = ConfigurationManager.AppSettings["BPJSHospitalID"];
        //    bs.JenisPelayanan = txt.Pelayanan;
        //    bs.Catatan = txt.CatatanSep;
        //    bs.DiagnosaAwal = txt.DiagnosaSep;
        //    bs.PoliTujuan = txt.PoliSep;
        //    bs.KelasRawat = txt.KelasRawatSep;
        //    bs.LakaLantas = chkPenjaminKLLSep.Checked ? "1" : "0";
        //    bs.User = string.IsNullOrEmpty(AppSession.UserLogin.LicenseNo) ? AppSession.UserLogin.UserID : AppSession.UserLogin.LicenseNo;
        //    bs.NoMR = txt.NoMRSep.SelectedValue.Split('|')[0];
        //    if (pnlRajal.Visible) bs.TanggalPulang = bs.TanggalSEP;
        //    else bs.str.TanggalPulang = string.Empty;
        //    bs.NoTransaksi = txt.AppointmentSep;
        //    bs.NamaPasien = txt.NamaPeserta;
        //    bs.Nik = txt.NikPeserta;
        //    bs.JenisKelamin = rblJenisKelamin;
        //    bs.TanggalLahir = txtTglLahirPeserta.SelectedDate.Value.Date;
        //    bs.JenisPeserta = txtJenisPeserta;
        //    bs.DetailKeanggotaan = txtNoTeleponSep;
        //    bs.PatientID = cboNoMRSep.SelectedValue.Split('|')[1];
        //    //bs.KodeCBG,
        //    //bs.TariffCBG,
        //    //bs.DeskripsiCBG,
        //    bs.LokasiLaka = bs.PenjaminLaka = pnlLakaLantas.Visible ? txtLokasiKLLSep.Text : string.Empty;
        //    bs.NamaKelasRawat = txtHakKelasPeserta;
        //    bs.IsEksekutif = chkEksekutifSep.Checked;
        //    bs.IsCob = chkCobSep.Checked;

        //    if (pnlLakaLantas.Visible) bs.PenjaminLaka = GetPenjaminLaka();
        //    else bs.PenjaminLaka = string.Empty;

        //    bs.NamaCob = txtCOBPeserta;
        //    bs.StatusPeserta = txtStatusPeserta;
        //    bs.Umur = txtUmurPeserta;
        //    bs.JenisRujukan = cboJenisRujukanSep;
        //    bs.IsKatarak = chkKatarakSep.Checked;
        //    if (pnlLakaLantas.Visible) bs.TglKejadian = txtTglKejadianSep.SelectedDate.Value.Date;
        //    else bs.str.TglKejadian = string.Empty;
        //    bs.IsSuplesi = chkSuplesiSep.Checked;
        //    bs.NoSepSuplesi = bs.IsSuplesi ?? false ? txtSuplesiSep.Text : string.Empty;
        //    bs.KodePropinsi = bs.IsSuplesi ?? false ? cboPropinsiSep.SelectedValue : string.Empty;
        //    bs.KodeKabupaten = bs.IsSuplesi ?? false ? cboKabupatenSep.SelectedValue : string.Empty;
        //    bs.KodeKecamatan = bs.IsSuplesi ?? false ? cboKecamatanSep.SelectedValue : string.Empty;
        //    bs.NoSkdp = txt.SkdpSep;
        //    bs.KodeDpjp = txt.DpjpSep;
        //    bs.FromRegistrationNo = txt.RegistrasiIGD;
        //    bs.ProlanisPRB = ViewState["ProlanisPRB"] == null ? string.Empty : ViewState["ProlanisPRB"].ToString();
        //    bs.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //    bs.LastUpdateDateTime = DateTime.Now;
        //    bs.KodeDpjpPelayanan = txt.DpjpPelayanan;
        //}


        //public static void SaveNew(ValidateArgs args, SepSource txt)
        //{
        //    if (CheckBpjsWebApi(args)) return;

        //    var sep = new BpjsSEP();
        //    sep.Query.es.Top = 1;
        //    sep.Query.Where(
        //        sep.Query.TanggalSEP == txt.TglSep.Date,
        //        sep.Query.JenisPelayanan == txt.Pelayanan,
        //        sep.Query.NomorKartu == txt.NoPeserta,
        //        sep.Query.PoliTujuan.NotIn(AppSession.Parameter.BpjsIgdUgdBridgingID)
        //        );
        //    if (sep.Query.Load())
        //    {
        //        if (txt.Pelayanan == "2")
        //        {
        //            args.MessageText = "Pasien telah melakukan registrasi SEP Rawat Jalan";
        //            args.IsCancel = true;
        //            return;
        //        }
        //    }

        //    if (txt.SkdpSep.Length < 6 && txt.SkdpSep.Length > 0)
        //    {
        //        args.MessageText = "No SKDP terdiri dari 6 digit";
        //        args.IsCancel = true;
        //        return;
        //    }

        //    if (txt.Pelayanan == "1")
        //    {
        //        var kelas = txt.HakKelasPeserta.Split('-')[0].Trim();
        //        if (txt.KelasRawatSep.ToInt() < kelas.ToInt())
        //        {
        //            args.MessageText = "Kelas rawat tidak boleh lebih besar dari hak kelas peserta";
        //            args.IsCancel = true;
        //            return;
        //        }
        //    }

        //    if (txt.TglSep.Date > DateTime.Now.Date)
        //    {
        //        args.MessageText = "Tanggal SEP lebih dari tanggal hari ini";
        //        args.IsCancel = true;
        //        return;
        //    }

        //    if (txt.TglRujukanSep.Date > txt.TglSep.Date)
        //    {
        //        args.MessageText = "Tanggal rujukan lebih dari tanggal SEP";
        //        args.IsCancel = true;
        //        return;
        //    }

        //    if (txt.TglSep.Date < DateTime.Now.Date.AddDays(-3))
        //    {
        //        args.MessageText = "Tanggal SEP lebih dari 3 x 24 jam";
        //        args.IsCancel = true;
        //        return;
        //    }

        //    if (txt.IsPenjaminKLLSep)
        //    {
        //        var penjamin = new List<bool>();
        //        foreach (ListItem chk in cblPenjaminKLLSep.Items)
        //        {
        //            penjamin.Add(chk.Selected);
        //        }
        //        if (penjamin.Count(p => p == false) == 4)
        //        {
        //            args.MessageText = "Penjamin KLL belum dipilih";
        //            args.IsCancel = true;
        //            return;
        //        }

        //        if (txt.TglKejadianSep.Date > txt.TglSep.Date)
        //        {
        //            args.MessageText = "Tanggal kejadian KLL lebih dari tanggal SEP";
        //            args.IsCancel = true;
        //            return;
        //        }

        //        if (txt.IsPenjaminKLLSep && !txt.IsSuplesiSep)
        //        {

        //            if (string.IsNullOrEmpty(txt.PropinsiSep))
        //            {
        //                args.MessageText = "Kode provinsi tidak boleh kosong";
        //                args.IsCancel = true;
        //                return;
        //            }
        //            if (string.IsNullOrEmpty(txt.KabupatenSep))
        //            {
        //                args.MessageText = "Kode kabupaten tidak boleh kosong";
        //                args.IsCancel = true;
        //                return;
        //            }
        //            if (string.IsNullOrEmpty(txt.KecamatanSep))
        //            {
        //                args.MessageText = "Kode kecamatan tidak boleh kosong";
        //                args.IsCancel = true;
        //                return;
        //            }
        //        }
        //    }

        //    var entity = new BpjsSEP();
        //    SetEntityValue(entity);
        //    SaveEntity(entity, args);

        //    //if (AppSession.Parameter.IsAutoPrintSEP)
        //    //{
        //    //    var parSep = new PrintJobParameterCollection();
        //    //    parSep.AddNew("p_NoSep", txt.NoSep, null, null);
        //    //    PrintManager.CreatePrintJob(AppConstant.Report.BpjsSEP, parSep);
        //    //}
        //}

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            HideInformationHeader();
            //if (CheckBpjsWebApi(args)) return;

            var sep = new BpjsSEP();
            sep.LoadByPrimaryKey(ViewState["sepID"].ToInt());

            var pat = new Patient();
            pat.LoadByPrimaryKey(sep.PatientID);
            if (pat.IsBlackList == true)
            {

                args.MessageText = "Pasien di BlackList";
                args.IsCancel = true;
                return;
            }

            if (cboPelayanan.SelectedValue == "1")
            {
                var kelas = txtHakKelasPeserta.Text.Split('-')[0].Trim();
                if (cboKelasRawatSep.SelectedValue.ToInt() < kelas.ToInt())
                {
                    args.MessageText = "Kelas rawat tidak boleh lebih besar dari hak kelas peserta";
                    args.IsCancel = true;
                    return;
                }

                if (string.IsNullOrWhiteSpace(cboNoSkdp.SelectedValue))
                {
                    args.MessageText = "No skdp rawat inap harus diisi dengan no surat kontrol/spri";
                    args.IsCancel = true;
                    return;
                }
            }

            if (txtTglSep.SelectedDate.Value.Date > DateTime.Now.Date)
            {
                args.MessageText = "Tanggal SEP lebih dari tanggal hari ini";
                args.IsCancel = true;
                return;
            }

            if (txtTglRujukanSep.SelectedDate.Value.Date > txtTglSep.SelectedDate.Value.Date)
            {
                args.MessageText = "Tanggal rujukan lebih dari tanggal SEP";
                args.IsCancel = true;
                return;
            }

            //if (txtTglSep.SelectedDate.Value.Date < DateTime.Now.Date.AddDays(-3))
            //{
            //    args.MessageText = "Tanggal SEP lebih dari 3 x 24 jam";
            //    args.IsCancel = true;
            //    return;
            //}

            if (chkPenjaminKLLSep.Checked)
            {
                if (txtTglKejadianSep.SelectedDate.Value.Date > txtTglSep.SelectedDate.Value.Date)
                {
                    args.MessageText = "Tanggal kejadian KLL lebih dari tanggal SEP";
                    args.IsCancel = true;
                    return;
                }

                if (chkSuplesiSep.Checked)
                {
                    if (string.IsNullOrEmpty(cboPropinsiSep.SelectedValue))
                    {
                        args.MessageText = "Kode provinsi tidak boleh kosong";
                        args.IsCancel = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(cboKabupatenSep.SelectedValue))
                    {
                        args.MessageText = "Kode kabupaten tidak boleh kosong";
                        args.IsCancel = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(cboKecamatanSep.SelectedValue))
                    {
                        args.MessageText = "Kode kecamatan tidak boleh kosong";
                        args.IsCancel = true;
                        return;
                    }
                }
            }

            var entity = new BpjsSEP();
            if (entity.LoadByPrimaryKey(ViewState["sepID"].ToInt()))
            {
                SetEntityValue(entity);
                SaveEntity(entity, args);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //if (CheckBpjsWebApi(args)) return;

            var reg = new Registration();
            reg.Query.es.Top = 1;
            reg.Query.Where(reg.Query.BpjsSepNo == txtNoSep.Text, reg.Query.GuarantorCardNo == txtNoPesertaPeserta.Text, reg.Query.IsVoid == false);
            if (reg.Query.Load())
            {
                args.MessageText = "No SEP telah di mapping ke registrasi kunjungan pasien, data tidak bisa dihapus, ref : " + reg.RegistrationNo;
                args.IsCancel = true;
                return;
            }

            var entity = new BpjsSEP();
            if (entity.LoadByPrimaryKey(ViewState["sepID"].ToInt()))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity, args);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
        }

        private void SetEntityValue(BpjsSEP bs)
        {
            //bs.SepID,
            bs.NoSEP = txtNoSep.Text;
            bs.NomorKartu = txtNoPesertaPeserta.Text;
            bs.TanggalSEP = txtTglSep.SelectedDate.Value.Date;
            bs.TanggalRujukan = txtTglRujukanSep.SelectedDate.Value.Date;
            bs.NoRujukan = cboNoRujukan.Text;
            bs.PPKRujukan = cboAsalRujukanSep.SelectedValue;
            bs.NamaPPKRujukan = cboAsalRujukanSep.Text;
            bs.PPKPelayanan = ConfigurationManager.AppSettings["BPJSHospitalID"];
            bs.JenisPelayanan = cboPelayanan.SelectedValue;
            bs.Catatan = txtCatatanSep.Text;
            bs.DiagnosaAwal = cboDiagnosaSep.SelectedValue;
            bs.PoliTujuan = cboPelayanan.SelectedValue == "1" ? string.Empty : cboPoliSep.SelectedValue.Split('#')[0];
            bs.KelasRawat = pnlRajal.Visible ? string.Empty : cboKelasRawatSep.SelectedValue; //hak kelas
            bs.LakaLantas = chkPenjaminKLLSep.Checked ? cboJenisKejadianLaka.SelectedValue : "0";
            bs.NoLP = chkPenjaminKLLSep.Checked ? txtNoLp.Text : string.Empty;
            bs.User = string.IsNullOrEmpty(AppSession.UserLogin.LicenseNo) ? AppSession.UserLogin.UserID : AppSession.UserLogin.LicenseNo;
            bs.NoMR = cboNoMRSep.SelectedValue.Split('|')[0];
            if (pnlRajal.Visible) bs.TanggalPulang = bs.TanggalSEP;
            else bs.str.TanggalPulang = string.Empty;
            bs.NoTransaksi = txtAppointmentSep.Text;
            bs.NamaPasien = txtNamaPeserta.Text;
            bs.Nik = txtNikPeserta.Text;
            bs.JenisKelamin = rblJenisKelamin.SelectedValue;
            bs.TanggalLahir = txtTglLahirPeserta.SelectedDate.Value.Date;
            bs.JenisPeserta = txtJenisPeserta.Text;
            bs.DetailKeanggotaan = txtNoTeleponSep.Text;
            bs.PatientID = cboNoMRSep.SelectedValue.Split('|')[1];
            //bs.KodeCBG,
            //bs.TariffCBG,
            //bs.DeskripsiCBG,
            bs.LokasiLaka = bs.PenjaminLaka = pnlLakaLantas.Visible ? txtLokasiKLLSep.Text : string.Empty;
            bs.NamaKelasRawat = pnlRajal.Visible ? string.Empty : $"{cboKelasRawatSep.SelectedValue} - {cboKelasRawatSep.Text}"; //kelas rawat
            bs.IsEksekutif = chkEksekutifSep.Checked;
            bs.IsCob = chkCobSep.Checked;

            bs.PenjaminLaka = string.Empty;

            bs.NamaCob = txtCOBPeserta.Text;
            bs.StatusPeserta = txtStatusPeserta.Text;
            bs.Umur = txtUmurPeserta.Text;
            bs.JenisRujukan = cboJenisRujukanSep.SelectedValue;
            bs.IsKatarak = chkKatarakSep.Checked;
            if (pnlLakaLantas.Visible) bs.TglKejadian = txtTglKejadianSep.SelectedDate.Value.Date;
            else bs.str.TglKejadian = string.Empty;
            bs.IsSuplesi = chkSuplesiSep.Checked;
            bs.NoSepSuplesi = bs.IsSuplesi ?? false ? txtSuplesiSep.Text : string.Empty;
            bs.KodePropinsi = cboPropinsiSep.SelectedValue;
            bs.KodeKabupaten = cboKabupatenSep.SelectedValue;
            bs.KodeKecamatan = cboKecamatanSep.SelectedValue;
            bs.KodeDpjp = cboDpjpSep.SelectedValue;
            bs.FromRegistrationNo = cboRegistrasiIGD.SelectedValue;
            bs.ProlanisPRB = ViewState["ProlanisPRB"] == null ? string.Empty : ViewState["ProlanisPRB"].ToString();
            bs.LastUpdateByUserID = AppSession.UserLogin.UserID;
            bs.LastUpdateDateTime = DateTime.Now;
            bs.KodeDpjpPelayanan = cboDpjpPelayanan.SelectedValue;
            bs.KlsRawatNaik = cboKelasRawatNaik.SelectedValue;
            bs.Pembiayaan = cboPembiayaan.SelectedValue;
            bs.TujuanKunj = cboTujuanKunjungan.SelectedValue;
            bs.FlagProcedure = cboFlagProcedure.SelectedValue;
            bs.KdPenunjang = cboKodePenunjang.SelectedValue;
            bs.AssesmentPel = cboAssesmentPelayanan.SelectedValue;
            bs.KodeDpjpKontrol = cboDpjpKontrol.SelectedValue;
            bs.NoSkdp = cboNoSkdp.Text;
            bs.KlsHak = txtHakKelasPeserta.Text;
        }

        private void SaveEntity(BpjsSEP entity, ValidateArgs args)
        {
            var isAdded = false;
            var isModified = false;
            var isDeleted = false;

            var svc = new Common.BPJS.VClaim.v11.Service();

            //using (var trans = new esTransactionScope())
            //{
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                isAdded = entity.es.IsAdded;
                isModified = entity.es.IsModified;

                //entity.Save();

                var patient = new Patient();
                patient.LoadByPrimaryKey(entity.PatientID);
                if (AppSession.Parameter.GuarantorAskesID.Count() > 0) patient.GuarantorID = AppSession.Parameter.GuarantorAskesID[0];
                patient.GuarantorCardNo = entity.NomorKartu;
                patient.Save();

                var diag = new Diagnose();
                if (!diag.LoadByPrimaryKey(cboDiagnosaSep.SelectedValue))
                {
                    diag = new Diagnose()
                    {
                        DiagnoseID = cboDiagnosaSep.SelectedValue,
                        DtdNo = "0",
                        DiagnoseName = cboDiagnosaSep.Text.Replace("'", "`"),
                        IsChronicDisease = false,
                        IsDisease = false,
                        IsActive = true,
                        LastUpdateDateTime = DateTime.Now,
                        LastUpdateByUserID = AppSession.UserLogin.UserID
                    };
                    diag.Save();
                }
                else
                {
                    try
                    {
                        if (diag.DiagnoseName != cboDiagnosaSep.Text)
                        {
                            diag.DiagnoseName = cboDiagnosaSep.Text.Replace("'", "`");
                            diag.Save();
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            else if (entity.es.IsDeleted)
            {
                isDeleted = entity.es.IsDeleted;

                if (!string.IsNullOrEmpty(txtNoSep.Text))
                {
                    var delete = new Temiang.Avicenna.Common.BPJS.VClaim.v11.Sep.DeleteRequest.TSep()
                    {
                        NoSep = txtNoSep.Text,
                        User = string.IsNullOrEmpty(AppSession.UserLogin.LicenseNo) ? AppSession.UserLogin.UserID : AppSession.UserLogin.LicenseNo
                    };

                    svc = new Common.BPJS.VClaim.v11.Service();
                    var response = svc.Delete(delete);
                    {
                        var log = new WebServiceAPILog()
                        {
                            DateRequest = DateTime.Now,
                            IPAddress = string.Empty,
                            UrlAddress = "DeleteSep",
                            Params = JsonConvert.SerializeObject(delete),
                            Response = JsonConvert.SerializeObject(response),
                            Totalms = 0
                        };
                        log.Save();
                    }
                    if (!response.MetaData.IsValid)
                    {
                        args.MessageText = string.Format("Code : {0}, Message : {1}", response.MetaData.Code, response.MetaData.Message);
                        args.IsCancel = true;
                        return;
                    }
                    else
                    {
                        entity.Save();

                        ScriptManager.RegisterStartupScript(this, GetType(), "delete", string.Format("alert('Code : {0}, Message : {1}');", "000", "Hapus SEP berhasil, silahkan kembali ke layar list"), true);
                    }
                }
            }

            //if (!isDeleted) ViewState["sepID"] = entity.SepID;

            if (isAdded)
            {
                var insert = new Common.BPJS.VClaim.v20.Sep.InsertRequest.TSep()
                {
                    Rujukan = new Common.BPJS.VClaim.v20.Sep.InsertRequest.Rujukan()
                    {
                        AsalRujukan = cboJenisRujukanSep.SelectedValue,
                        TglRujukan = txtTglRujukanSep.SelectedDate.Value.Date.ToString("yyyy-MM-dd"),
                        NoRujukan = cboNoRujukan.Text,
                        PpkRujukan = cboAsalRujukanSep.SelectedValue
                    },
                    Poli = new Common.BPJS.VClaim.v20.Sep.InsertRequest.Poli()
                    {
                        Tujuan = cboPelayanan.SelectedValue == "1" ? string.Empty : cboPoliSep.SelectedValue.Split('#')[0],
                        Eksekutif = chkEksekutifSep.Checked ? "1" : "0"
                    },
                    Cob = new Common.BPJS.VClaim.v20.Sep.InsertRequest.TCob()
                    {
                        Cob = chkCobSep.Checked ? "1" : "0"
                    },
                    Katarak = new Common.BPJS.VClaim.v20.Sep.InsertRequest.TKatarak()
                    {
                        Katarak = chkKatarakSep.Checked ? "1" : "0"
                    },
                    Jaminan = new Common.BPJS.VClaim.v20.Sep.InsertRequest.Jaminan()
                    {
                        LakaLantas = chkPenjaminKLLSep.Checked ? cboJenisKejadianLaka.SelectedValue : "0",
                        NoLp = txtNoLp.Text,
                        Penjamin = new Common.BPJS.VClaim.v20.Sep.InsertRequest.Penjamin()
                        {
                            TglKejadian = pnlLakaLantas.Visible ? txtTglKejadianSep.SelectedDate.Value.Date.ToString("yyyy-MM-dd") : string.Empty,
                            Keterangan = pnlLakaLantas.Visible ? txtLokasiKLLSep.Text : string.Empty,
                            Suplesi = new Common.BPJS.VClaim.v20.Sep.InsertRequest.TSuplesi()
                            {
                                Suplesi = chkSuplesiSep.Checked ? "1" : "0",
                                NoSepSuplesi = pnlSuplesi.Visible ? txtSuplesiSep.Text : string.Empty,
                                LokasiLaka = new Common.BPJS.VClaim.v20.Sep.InsertRequest.LokasiLaka()
                                {
                                    KdPropinsi = cboPropinsiSep.SelectedValue,
                                    KdKabupaten = cboKabupatenSep.SelectedValue,
                                    KdKecamatan = cboKecamatanSep.SelectedValue
                                }
                            }
                        }
                    },
                    TujuanKunj = cboTujuanKunjungan.SelectedValue,
                    FlagProcedure = cboFlagProcedure.SelectedValue,
                    KdPenunjang = cboKodePenunjang.SelectedValue,
                    AssesmentPel = cboAssesmentPelayanan.SelectedValue,
                    Skdp = new Common.BPJS.VClaim.v20.Sep.InsertRequest.Skdp()
                    {
                        NoSurat = cboNoSkdp.Text,
                        KodeDPJP = cboDpjpKontrol.SelectedValue
                    },
                    NoKartu = txtNoPesertaPeserta.Text,
                    TglSep = txtTglSep.SelectedDate.Value.Date.ToString("yyyy-MM-dd"),
                    PpkPelayanan = ConfigurationManager.AppSettings["BPJSHospitalID"],
                    JnsPelayanan = cboPelayanan.SelectedValue,
                    KlsRawat = new Common.BPJS.VClaim.v20.Sep.InsertRequest.KlsRawat()
                    {
                        KlsRawatHak = pnlRajal.Visible ? "3" : cboKelasRawatSep.SelectedValue,
                        KlsRawatNaik = cboKelasRawatNaik.SelectedValue,
                        Pembiayaan = cboPembiayaan.SelectedValue,
                        PenanggungJawab = cboPembiayaan.Text
                    },
                    DpjpLayan = cboPelayanan.SelectedValue == "1" ? string.Empty : cboDpjpPelayanan.SelectedValue,
                    NoMR = cboNoMRSep.SelectedValue.Split('|')[0],
                    Catatan = txtCatatanSep.Text,
                    DiagAwal = cboDiagnosaSep.SelectedValue,
                    NoTelp = txtNoTeleponSep.Text,
                    User = string.IsNullOrEmpty(AppSession.UserLogin.LicenseNo) ? AppSession.UserLogin.UserID : AppSession.UserLogin.LicenseNo
                };

                //{
                var log = new WebServiceAPILog()
                {
                    DateRequest = DateTime.Now,
                    IPAddress = string.Empty,
                    UrlAddress = string.Empty,
                    Params = JsonConvert.SerializeObject(insert),
                    //Response = string.Empty,
                    Totalms = 0
                };
                //log.Save();
                //}

                svc = new Common.BPJS.VClaim.v11.Service();
                var response = svc.Insert(insert);
                if (response.MetaData.IsValid)
                {
                    log.Response = JsonConvert.SerializeObject(response);
                    log.Save();

                    var sep = response.Response.Sep;
                    txtNoSep.Text = sep.NoSep;

                    //var entity2 = new BpjsSEP();
                    //entity2.LoadByPrimaryKey(ViewState["sepID"].ToInt());
                    //entity2.NoSEP = txtNoSep.Text;
                    //entity2.Save();

                    entity.NoSEP = txtNoSep.Text;
                    entity.Save();

                    ViewState["sepID"] = entity.SepID;

                    if (!string.IsNullOrWhiteSpace(cboRegistrasi.SelectedValue))
                    {
                        var reg = new Registration();
                        reg.LoadByPrimaryKey(cboRegistrasi.SelectedValue);
                        reg.GuarantorCardNo = entity.NomorKartu;
                        reg.BpjsSepNo = entity.NoSEP;
                        reg.Save();
                    }

                    if (Helper.IsBpjsAntrolIntegration)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(entity.NoTransaksi) && entity.JenisPelayanan == "2")
                            {
                                log = new WebServiceAPILog();
                                log.DateRequest = DateTime.Now;
                                log.IPAddress = string.Empty;
                                log.UrlAddress = "BpjsVClaimDetail";
                                log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                {
                                    Kodebooking = entity.NoTransaksi,
                                    Taskid = 3,
                                    Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                                });

                                var svcAntrol = new Common.BPJS.Antrian.Service();
                                var responseAntrol = svcAntrol.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                {
                                    Kodebooking = entity.NoTransaksi,
                                    Taskid = 3,
                                    Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                                });

                                log.Response = JsonConvert.SerializeObject(response);
                                log.Save();
                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "insert", string.Format("alert('Code : {0}, Message : {1}');", "000", "Pembuatan SEP berhasil, No SEP : " + entity.NoSEP), true);

                    if (AppSession.Parameter.HealthcareInitial != "RSYS")
                    {
                        if (AppSession.Parameter.IsAutoPrintSEP)
                        {
                            var parSep = new PrintJobParameterCollection();
                            parSep.AddNew("p_NoSep", txtNoSep.Text, null, null);
                            PrintManager.CreatePrintJob(AppConstant.Report.BpjsSEP, parSep);
                        }
                    }

                    var prg = new AppProgram();
                    if (prg.LoadByPrimaryKey(AppConstant.Report.BpjsESEP))
                    {
                        var printJobParameters = new PrintJobParameterCollection();
                        printJobParameters.AddNew("p_NoSep", txtNoSep.Text);
                        var path = Module.Reports.ReportViewer.SaveFileToGuarantorDocument(AppSession.Parameter.HealthcareInitial, AppConstant.Report.BpjsESEP, printJobParameters);

                        log = new WebServiceAPILog();
                        log.DateRequest = DateTime.Now;
                        log.IPAddress = string.Empty;
                        log.UrlAddress = "BpjsVClaimESep";
                        log.Params = txtNoSep.Text;
                        log.Response = path;
                        log.Save();
                    }
                }
                else
                {
                    log.Response = JsonConvert.SerializeObject(response);
                    log.Save();

                    args.MessageText = string.Format("Code : {0}, Message : {1}", response.MetaData.Code, response.MetaData.Message);
                    args.IsCancel = true;
                    return;
                }
            }
            else if (isModified)
            {
                if (!string.IsNullOrEmpty(txtNoSep.Text))
                {
                    var update = new Temiang.Avicenna.Common.BPJS.VClaim.v20.Sep.UpdateRequest.Update.TSep()
                    {
                        NoSep = txtNoSep.Text,
                        KlsRawat = new Common.BPJS.VClaim.v20.Sep.UpdateRequest.Update.KlsRawat()
                        {
                            KlsRawatHak = pnlRajal.Visible ? "3" : cboKelasRawatSep.SelectedValue,
                            KlsRawatNaik = cboKelasRawatNaik.SelectedValue,
                            Pembiayaan = cboPembiayaan.SelectedValue,
                            PenanggungJawab = cboPembiayaan.Text
                        },
                        Poli = new Common.BPJS.VClaim.v20.Sep.UpdateRequest.Update.Poli()
                        {
                            Tujuan = cboPelayanan.SelectedValue == "1" ? string.Empty : cboPoliSep.SelectedValue.Split('#')[0],
                            Eksekutif = chkEksekutifSep.Checked ? "1" : "0"
                        },
                        Cob = new Common.BPJS.VClaim.v20.Sep.UpdateRequest.Update.TCob()
                        {
                            Cob = chkCobSep.Checked ? "1" : "0"
                        },
                        Katarak = new Common.BPJS.VClaim.v20.Sep.UpdateRequest.Update.TKatarak()
                        {
                            Katarak = chkKatarakSep.Checked ? "1" : "0"
                        },
                        Jaminan = new Common.BPJS.VClaim.v20.Sep.UpdateRequest.Update.Jaminan()
                        {
                            LakaLantas = chkPenjaminKLLSep.Checked ? cboJenisKejadianLaka.SelectedValue : "0",
                            //nooLp = txtNoLp.Text,
                            Penjamin = new Common.BPJS.VClaim.v20.Sep.UpdateRequest.Update.Penjamin()
                            {
                                TglKejadian = pnlLakaLantas.Visible ? txtTglKejadianSep.SelectedDate.Value.Date.ToString("yyyy-MM-dd") : string.Empty,
                                Keterangan = pnlLakaLantas.Visible ? txtLokasiKLLSep.Text : string.Empty,
                                Suplesi = new Common.BPJS.VClaim.v20.Sep.UpdateRequest.Update.TSuplesi()
                                {
                                    Suplesi = chkSuplesiSep.Checked ? "1" : "0",
                                    NoSepSuplesi = pnlSuplesi.Visible ? txtSuplesiSep.Text : string.Empty,
                                    LokasiLaka = new Common.BPJS.VClaim.v20.Sep.UpdateRequest.Update.LokasiLaka()
                                    {
                                        KdPropinsi = cboPropinsiSep.SelectedValue,
                                        KdKabupaten = cboKabupatenSep.SelectedValue,
                                        KdKecamatan = cboKecamatanSep.SelectedValue
                                    }
                                }
                            }
                        },
                        DpjpLayan = cboPelayanan.SelectedValue == "1" ? string.Empty : cboDpjpPelayanan.SelectedValue,
                        NoMR = cboNoMRSep.SelectedValue.Split('|')[0],
                        Catatan = txtCatatanSep.Text,
                        DiagAwal = cboDiagnosaSep.SelectedValue,
                        NoTelp = txtNoTeleponSep.Text,
                        User = "Yulianti"
                    };

                    //{
                    var log = new WebServiceAPILog()
                    {
                        DateRequest = DateTime.Now,
                        IPAddress = string.Empty,
                        UrlAddress = string.Empty,
                        Params = JsonConvert.SerializeObject(update),
                        //Response = string.Empty,
                        Totalms = 0
                    };
                    //log.Save();
                    //}

                    svc = new Common.BPJS.VClaim.v11.Service();
                    var response = svc.Update(update);
                    if (response.MetaData.IsValid)
                    {
                        log.Response = JsonConvert.SerializeObject(response);
                        log.Save();

                        //txtNoSep.Text = response.Response;
                        entity.Save();

                        ViewState["sepID"] = entity.SepID;

                        if (!string.IsNullOrWhiteSpace(cboRegistrasi.SelectedValue))
                        {
                            var reg = new Registration();
                            reg.LoadByPrimaryKey(cboRegistrasi.SelectedValue);
                            reg.GuarantorCardNo = entity.NomorKartu;
                            reg.BpjsSepNo = entity.NoSEP;
                            reg.Save();
                        }

                        ScriptManager.RegisterStartupScript(this, GetType(), "edit", string.Format("alert('Code : {0}, Message : {1}');", "000", "Edit SEP berhasil, No SEP : " + txtNoSep.Text), true);
                    }
                    else
                    {
                        log.Response = JsonConvert.SerializeObject(response);
                        log.Save();

                        args.MessageText = string.Format("Code : {0}, Message : {1}", response.MetaData.Code, response.MetaData.Message);
                        args.IsCancel = true;
                        return;
                    }
                }
                else
                {

                }
            }

            //Commit if success, Rollback if failed
            //trans.Complete();
            //}
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("SepID='{0}'", ViewState["sepID"].ToInt().ToString());
            auditLogFilter.TableName = "BpjsSEP";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            if (pnlRujukan.Visible) btnCariRujukan.Enabled = (newVal == AppEnum.DataMode.New);
            btnCariData.Enabled = (newVal == AppEnum.DataMode.New);
            btnClearData.Enabled = (newVal == AppEnum.DataMode.New);
            btnCreatePatient.Enabled = (newVal == AppEnum.DataMode.New);
            btnCariPasien.Enabled = (newVal == AppEnum.DataMode.New);
            btnCariAppointment.Enabled = (newVal == AppEnum.DataMode.New);
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new BpjsSEPQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.SepID > ViewState["sepID"].ToInt());
                que.OrderBy(que.NoSEP.Ascending);
            }
            else
            {
                que.Where(que.SepID < ViewState["sepID"].ToInt());
                que.OrderBy(que.NoSEP.Descending);
            }
            var entity = new BpjsSEP();
            if (entity.Load(que)) OnPopulateEntryControl(entity);
            //else OnMenuNewClick();
            //else OnPopulateEntryControl(null);
            //else MoveRecord(false);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            //if (CheckBpjsWebApi()) return;

            var entity = new BpjsSEP();
            if (parameters.Length > 0)
            {
                int sepId = parameters[0].ToInt();
                if (!parameters[0].Equals(string.Empty)) entity.LoadByPrimaryKey(sepId);
            }
            else entity.LoadByPrimaryKey(ViewState["sepID"].ToInt());

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var bs = (BpjsSEP)entity;

            if (bs != null && bs.SepID != null)
            {
                ViewState["sepID"] = bs.SepID;
                ViewState["ProlanisPRB"] = bs.ProlanisPRB;

                txtNoSep.Text = bs.NoSEP;

                rblJenis.SelectedIndex = 1;
                rblJenis_SelectedIndexChanged(null, null);

                cboPelayanan.SelectedValue = bs.JenisPelayanan;
                cboPelayanan_SelectedIndexChanged(null, new Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, bs.JenisPelayanan, string.Empty));
                txtTglSep.SelectedDate = bs.TanggalSEP;
                rblJenisKartu.SelectedIndex = 0;

                txtNomor.Text = bs.NomorKartu;

                if (pnlRajal.Visible)
                {
                    chkEksekutifSep.Checked = bs.IsEksekutif ?? false;

                    if (!string.IsNullOrWhiteSpace(bs.NoTransaksi))
                    {
                        var appt = new BusinessObject.Appointment();
                        appt.LoadByPrimaryKey(bs.NoTransaksi);

                        cboPoliSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = bs.PoliTujuan, Message = appt.ServiceUnitID });
                    }
                    else cboPoliSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = bs.PoliTujuan });

                    foreach (RadComboBoxItem item in cboPoliSep.Items)
                    {
                        if (item.Value.Split('#')[0] == bs.PoliTujuan)
                        {
                            if (cboPoliSep.SelectedValue != item.Value)
                            {
                                cboPoliSep.SelectedValue = item.Value;
                                cboPoliSep.Text = cboPoliSep.SelectedItem.Text;
                            }
                            break;
                        }
                    }
                    //cboPoliSep.SelectedValue = bs.PoliTujuan;
                    //if (cboPoliSep.Items.Any()) cboPoliSep.SelectedIndex = 0;
                }

                cboJenisRujukanSep.SelectedValue = bs.JenisRujukan;
                var faskes = new List<Common.BPJS.VClaim.v11.Faskes.Faske>();
                faskes.Add(new Temiang.Avicenna.Common.BPJS.VClaim.v11.Faskes.Faske()
                {
                    Kode = bs.PPKRujukan,
                    Nama = bs.NamaPPKRujukan
                });
                cboAsalRujukanSep.DataSource = faskes;
                cboAsalRujukanSep.DataBind();
                cboAsalRujukanSep.SelectedValue = bs.PPKRujukan;

                txtTglRujukanSep.SelectedDate = bs.TanggalRujukan;

                if (!string.IsNullOrWhiteSpace(bs.PoliTujuan))
                {
                    var sub = new ServiceUnitBridging();
                    sub.Query.Where(sub.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(), AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString(), AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString()), sub.Query.BridgingID == bs.PoliTujuan);
                    if (!string.IsNullOrWhiteSpace(bs.NoTransaksi))
                    {
                        var appt = new BusinessObject.Appointment();
                        appt.LoadByPrimaryKey(bs.NoTransaksi);

                        sub.Query.Where(sub.Query.ServiceUnitID == appt.ServiceUnitID);
                    }
                    else sub.Query.es.Top = 1;
                    sub.Query.Load();

                    var rujukan = new List<Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2>();
                    rujukan.Add(new Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2()
                    {
                        NoKunjungan = bs.NoRujukan,
                        TglKunjungan = bs.TanggalRujukan.Value.ToString("yyyy-MM-dd"),
                        ProvPerujuk = new Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.ProvPerujuk() { Nama = bs.NamaPPKRujukan },
                        PoliRujukan = new Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.PoliRujukan() { Nama = sub.BridgingName }
                    });
                    cboNoRujukan.DataSource = rujukan;
                    cboNoRujukan.DataBind();
                    cboNoRujukan.SelectedValue = bs.NoRujukan;
                }

                txtTglSep.SelectedDate = bs.TanggalSEP;

                var patient = new Patient();
                if (patient.LoadByPrimaryKey(bs.PatientID))
                {
                    var list = new List<Patient>();
                    list.Add(new Patient()
                    {
                        PatientID = patient.PatientID,
                        MedicalNo = patient.MedicalNo,
                        FirstName = patient.FirstName,
                        MiddleName = patient.MiddleName,
                        LastName = patient.LastName
                    });

                    cboNoMRSep.DataSource = list;
                    cboNoMRSep.DataBind();
                    cboNoMRSep.SelectedValue = patient.MedicalNo + "|" + patient.PatientID;
                }

                if (pnlRanap.Visible)
                {
                    cboKelasRawatSep.SelectedValue = bs.KelasRawat;
                    cboKelasRawatNaik.SelectedValue = bs.KlsRawatNaik;
                    cboPembiayaan.SelectedValue = bs.Pembiayaan;
                }

                var diag = new Diagnose();
                diag.LoadByPrimaryKey(bs.DiagnosaAwal);

                var diagnosa = new List<Common.BPJS.VClaim.v11.Diagnosa.Diagnosa2>();
                diagnosa.Add(new Temiang.Avicenna.Common.BPJS.VClaim.v11.Diagnosa.Diagnosa2()
                {
                    Kode = bs.DiagnosaAwal,
                    Nama = diag.DiagnoseName
                });

                cboDiagnosaSep.DataSource = diagnosa;
                cboDiagnosaSep.DataBind();
                cboDiagnosaSep.SelectedValue = bs.DiagnosaAwal;

                txtNoTeleponSep.Text = bs.DetailKeanggotaan;
                txtCatatanSep.Text = bs.Catatan;
                chkCobSep.Checked = bs.IsCob ?? false;

                chkPenjaminKLLSep.Checked = (bs.LakaLantas == "1");

                pnlLakaLantas.Visible = chkPenjaminKLLSep.Checked;
                if (pnlLakaLantas.Visible)
                {
                    txtLokasiKLLSep.Text = bs.LokasiLaka;
                }
                txtAppointmentSep.Text = bs.NoTransaksi;

                txtNamaPeserta.Text = bs.NamaPasien;
                txtNikPeserta.Text = bs.Nik;
                txtNoPesertaPeserta.Text = bs.NomorKartu;
                txtTglLahirPeserta.SelectedDate = bs.TanggalLahir;
                txtUmurPeserta.Text = bs.Umur;
                rblJenisKelamin.SelectedIndex = bs.JenisKelamin == "L" ? 0 : 1;
                txtNoMrPeserta.Text = bs.NoMR;
                txtNoTelpPeserta.Text = bs.DetailKeanggotaan;
                txtHakKelasPeserta.Text = bs.NamaKelasRawat;
                txtFaskesTingkat1.Text = bs.NamaPPKRujukan;
                txtJenisPeserta.Text = bs.JenisPeserta;
                txtStatusPeserta.Text = bs.StatusPeserta;
                txtCOBPeserta.Text = bs.NamaCob;
                chkCobSep.Enabled = !string.IsNullOrEmpty(txtCOBPeserta.Text);

                chkKatarakSep.Checked = bs.IsKatarak ?? false;
                if (pnlLakaLantas.Visible) cboJenisKejadianLaka.SelectedValue = bs.LakaLantas;
                if (pnlLakaLantas.Visible) txtTglKejadianSep.SelectedDate = bs.TglKejadian;
                if (pnlLakaLantas.Visible)
                {
                    chkSuplesiSep.Checked = bs.IsSuplesi ?? false;

                    if (!string.IsNullOrEmpty(bs.KodePropinsi))
                    {
                        var svc = new Common.BPJS.VClaim.v11.Service();
                        var response = svc.GetPropinsi();
                        if (response.MetaData.IsValid)
                        {
                            cboPropinsiSep.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                            foreach (var item in response.Response.List)
                            {
                                cboPropinsiSep.Items.Add(new RadComboBoxItem(item.Nama, item.Kode));
                            }
                        }
                        cboPropinsiSep.SelectedValue = bs.KodePropinsi;
                    }
                    if (!string.IsNullOrEmpty(bs.KodeKabupaten))
                    {
                        var svc = new Common.BPJS.VClaim.v11.Service();
                        var response = svc.GetKabupaten(bs.KodePropinsi);
                        if (response.MetaData.IsValid)
                        {
                            cboKabupatenSep.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                            foreach (var item in response.Response.List)
                            {
                                cboKabupatenSep.Items.Add(new RadComboBoxItem(item.Nama, item.Kode));
                            }
                        }
                        cboKabupatenSep.SelectedValue = bs.KodeKabupaten;
                    }
                    if (!string.IsNullOrEmpty(bs.KodeKecamatan))
                    {
                        var svc = new Common.BPJS.VClaim.v11.Service();
                        var response = svc.GetKecamatan(bs.KodeKabupaten);
                        if (response.MetaData.IsValid)
                        {
                            cboKecamatanSep.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                            foreach (var item in response.Response.List)
                            {
                                cboKecamatanSep.Items.Add(new RadComboBoxItem(item.Nama, item.Kode));
                            }
                        }
                        cboKecamatanSep.SelectedValue = bs.KodeKecamatan;
                    }
                }
                if (pnlLakaLantas.Visible && chkSuplesiSep.Checked) txtSuplesiSep.Text = bs.NoSepSuplesi;

                cboDpjpSep_ItemsRequested(cboDpjpSep, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = bs.KodeDpjp });
                cboDpjpSep.SelectedValue = bs.KodeDpjp;

                cboRegistrasiIGD_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = bs.FromRegistrationNo });
                cboRegistrasiIGD.SelectedValue = bs.FromRegistrationNo;

                if (!string.IsNullOrEmpty(bs.KodeDpjpPelayanan))
                {
                    cboDpjpSep_ItemsRequested(cboDpjpPelayanan, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = bs.KodeDpjpPelayanan });
                    cboDpjpPelayanan.SelectedValue = bs.KodeDpjpPelayanan;
                }

                cboTujuanKunjungan.SelectedValue = bs.TujuanKunj;
                cboFlagProcedure.SelectedValue = bs.FlagProcedure;
                cboFlagProcedure_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, bs.FlagProcedure, string.Empty));

                cboKodePenunjang.SelectedValue = bs.KdPenunjang;
                cboAssesmentPelayanan.SelectedValue = bs.AssesmentPel;

                if (!string.IsNullOrEmpty(bs.KodeDpjpKontrol))
                {
                    cboDpjpSep_ItemsRequested(cboDpjpKontrol, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = bs.KodeDpjpKontrol });
                    cboDpjpKontrol.SelectedValue = bs.KodeDpjpKontrol;
                }

                if (!string.IsNullOrWhiteSpace(bs.NoSkdp))
                {
                    var list = new List<SkdpSource>();
                    list.Add(new SkdpSource()
                    {
                        NoSuratKontrol = bs.NoSkdp,
                        TglRencanaKontrol = string.Empty,
                        NamaPoliTujuan = string.Empty,
                        NamaDokter = string.Empty
                    });

                    cboNoSkdp.DataSource = list;
                    cboNoSkdp.DataBind();
                    cboNoSkdp.SelectedIndex = 0;
                    //cboNoSkdp_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, bs.NoSkdp, string.Empty));
                }
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (sourceControl is RadTextBox || sourceControl is RadComboBox)
            {
                if (!string.IsNullOrEmpty(eventArgument) && eventArgument.Contains("!"))
                {
                    var str = eventArgument.Split('!');
                    if (str.Length > 0)
                    {
                        if (str[1] == "pasien")
                        {
                            cboNoMRSep.DataSource = null;
                            cboNoMRSep.DataBind();
                            cboNoMRSep.Items.Clear();
                            cboNoMRSep.SelectedValue = string.Empty;
                            cboNoMRSep.Text = string.Empty;

                            var patient = new Patient();
                            if (patient.LoadByPrimaryKey(str[2]))
                            {
                                var list = new List<Patient>();
                                list.Add(new Patient()
                                {
                                    PatientID = patient.PatientID,
                                    MedicalNo = patient.MedicalNo,
                                    FirstName = patient.FirstName,
                                    MiddleName = patient.MiddleName,
                                    LastName = patient.LastName
                                });

                                cboNoMRSep.DataSource = list;
                                cboNoMRSep.DataBind();
                                cboNoMRSep.SelectedValue = patient.MedicalNo + "|" + patient.PatientID;
                                cboNoMRSep.Text = patient.MedicalNo + " - " + patient.PatientName;
                            }
                        }
                        else if (str[1] == "appointment")
                        {
                            //var appt = new BusinessObject.Appointment();
                            //appt.LoadByPrimaryKey(str[2]);

                            //if (string.IsNullOrWhiteSpace(cboPoliSep.SelectedValue))
                            //{
                            //    var sub = new ServiceUnitBridging();
                            //    sub.Query.Where(sub.Query.ServiceUnitID == appt.ServiceUnitID, sub.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString()));
                            //    if (sub.Query.Load())
                            //    {
                            //        cboPoliSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = sub.BridgingID });
                            //        cboPoliSep.SelectedValue = sub.BridgingID;
                            //    }
                            //}

                            //if (string.IsNullOrWhiteSpace(cboDpjpSep.SelectedValue))
                            //{
                            //    var pb = new ParamedicBridging();
                            //    pb.Query.Where(pb.Query.ParamedicID == appt.ParamedicID, pb.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString()));
                            //    if (pb.Query.Load())
                            //    {
                            //        cboDpjpSep_ItemsRequested(cboDpjpSep, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = pb.BridgingID });
                            //        cboDpjpSep.SelectedValue = pb.BridgingID;

                            //        cboDpjpSep_ItemsRequested(cboDpjpPelayanan, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = pb.BridgingID });
                            //        cboDpjpPelayanan.SelectedValue = pb.BridgingID;

                            //        cboDpjpSep_ItemsRequested(cboDpjpKontrol, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = pb.BridgingID });
                            //        cboDpjpKontrol.SelectedValue = pb.BridgingID;
                            //    }
                            //}

                            txtAppointmentSep.Text = str[2];

                            //if (string.IsNullOrWhiteSpace(cboNoRujukan.SelectedValue) && !string.IsNullOrWhiteSpace(appt.ReferenceNumber))
                            //{
                            //    cboNoRujukan.SelectedValue = appt.ReferenceNumber;
                            //    cboNoRujukan_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, appt.ReferenceNumber, string.Empty));
                            //}
                        }
                        else if (str[1] == "rujukan")
                        {
                            var svc = new Common.BPJS.VClaim.Service();
                            var response = svc.GetRujukan(str[2], Common.BPJS.VClaim.Enum.SearchRujukan.NoRujukan);
                            if (response.MetaData.IsValid)
                            {
                                var rujukan = response.Response.Rujukan;
                                cboPelayanan.SelectedValue = rujukan.Pelayanan.Kode;

                                var peserta = rujukan.Peserta;

                                txtNamaPeserta.Text = peserta.Nama;
                                txtNoPesertaPeserta.Text = peserta.NoKartu;
                                txtNikPeserta.Text = peserta.Nik;

                                string format = "yyyy-MM-dd";
                                DateTime parsed;
                                DateTime.TryParseExact(peserta.TglLahir, format, null, System.Globalization.DateTimeStyles.None, out parsed);

                                txtTglLahirPeserta.SelectedDate = parsed;
                                txtUmurPeserta.Text = peserta.Umur.UmurSekarang;
                                rblJenisKelamin.SelectedIndex = peserta.Sex == "L" ? 0 : 1;

                                txtNoMrPeserta.Text = peserta.Mr.NoMR;
                                var patient = new Patient();
                                if (!string.IsNullOrEmpty(txtNoMrPeserta.Text) && patient.LoadByMedicalNo(txtNoMrPeserta.Text))
                                {
                                    var list = new List<Patient>();
                                    list.Add(new Patient()
                                    {
                                        PatientID = patient.PatientID,
                                        MedicalNo = patient.MedicalNo,
                                        FirstName = patient.FirstName,
                                        MiddleName = patient.MiddleName,
                                        LastName = patient.LastName
                                    });

                                    cboNoMRSep.DataSource = list;
                                    cboNoMRSep.DataBind();
                                    cboNoMRSep.SelectedValue = patient.MedicalNo + "|" + patient.PatientID;
                                    cboNoMRSep.Text = patient.MedicalNo + " - " + patient.PatientName;
                                }
                                else
                                {
                                    cboNoMRSep.DataSource = null;
                                    cboNoMRSep.DataBind();
                                    cboNoMRSep.Items.Clear();
                                    cboNoMRSep.SelectedValue = string.Empty;
                                    cboNoMRSep.Text = string.Empty;
                                }

                                txtNoTelpPeserta.Text = peserta.Mr.NoTelepon;
                                if (!string.IsNullOrEmpty(peserta.HakKelas.Kode)) txtHakKelasPeserta.Text = peserta.HakKelas.Kode + " - " + peserta.HakKelas.Keterangan;
                                if (!string.IsNullOrEmpty(peserta.ProvUmum.KdProvider)) txtFaskesTingkat1.Text = peserta.ProvUmum.KdProvider + " - " + peserta.ProvUmum.NmProvider;
                                if (!string.IsNullOrEmpty(peserta.JenisPeserta.Kode)) txtJenisPeserta.Text = peserta.JenisPeserta.Kode + " - " + peserta.JenisPeserta.Keterangan;
                                if (!string.IsNullOrEmpty(peserta.StatusPeserta.Kode)) txtStatusPeserta.Text = peserta.StatusPeserta.Kode + " - " + peserta.StatusPeserta.Keterangan;
                                if (!string.IsNullOrEmpty(peserta.Cob.NoAsuransi)) txtCOBPeserta.Text = peserta.Cob.NoAsuransi + " - " + peserta.Cob.NmAsuransi;
                                ViewState["cobPeserta"] = peserta.Cob.NoAsuransi;

                                cboPelayanan_SelectedIndexChanged(null, new Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, cboPelayanan.SelectedValue, string.Empty));

                                if (pnlRajal.Visible)
                                {
                                    chkEksekutifSep.Checked = false;
                                    cboPoliSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = rujukan.PoliRujukan.Kode });
                                    foreach (RadComboBoxItem item in cboPoliSep.Items)
                                    {
                                        if (item.Value.Split('#')[0] == rujukan.PoliRujukan.Kode)
                                        {
                                            if (cboPoliSep.SelectedValue != item.Value)
                                            {
                                                cboPoliSep.SelectedValue = item.Value;
                                                cboPoliSep.Text = cboPoliSep.SelectedItem.Text;
                                            }
                                            break;
                                        }
                                    }
                                    //cboPoliSep.SelectedValue = rujukan.PoliRujukan.Kode;
                                    //if (cboPoliSep.Items.Any()) cboPoliSep.SelectedIndex = 0;
                                }

                                cboJenisRujukanSep.SelectedIndex = 0;
                                if (!string.IsNullOrEmpty(rujukan.ProvPerujuk.Kode))
                                {
                                    cboAsalRujukanSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = rujukan.ProvPerujuk.Kode });
                                    cboAsalRujukanSep.SelectedValue = rujukan.ProvPerujuk.Kode;
                                    cboAsalRujukanSep.Text = peserta.ProvUmum.KdProvider + " - " + peserta.ProvUmum.NmProvider;
                                }

                                LoadDetailRujukan(rujukan.NoKunjungan);

                                //DateTime.TryParseExact(rujukan.TglKunjungan, format, null, System.Globalization.DateTimeStyles.None, out parsed);
                                //txtTglRujukanSep.SelectedDate = parsed;

                                //txtNoRujukanSep.Text = rujukan.NoKunjungan;

                                //cboDiagnosaSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = rujukan.Diagnosa.Kode });
                                //cboDiagnosaSep.SelectedValue = rujukan.Diagnosa.Kode;
                                //cboDiagnosaSep.Text = rujukan.Diagnosa.Kode + " - " + rujukan.Diagnosa.Nama;

                                if (pnlRanap.Visible) cboKelasRawatSep.SelectedValue = peserta.HakKelas.Kode;
                                txtNoTeleponSep.Text = peserta.Mr.NoTelepon;
                                txtCatatanSep.Text = rujukan.Keluhan;

                                chkCobSep.Checked = false;
                                chkPenjaminKLLSep.Checked = false;
                                chkPenjaminKLLSep_CheckedChanged(null, null);

                                txtAppointmentSep.Text = string.Empty;
                            }
                        }
                        //else if (str[1] == "skdp")
                        //{
                        //    var svc = new Common.BPJS.VClaim.v11.Service();
                        //    var response = svc.GetRencanaKontrolByNoSuratKontrol(str[2]);
                        //    if (response.MetaData.IsValid)
                        //    {
                        //        txtNoSkdp.Text = str[2];

                        //        if (response.Response.JnsKontrol == "2")
                        //        {
                        //            var item = cboTujuanKunjungan.Items.Single(i => i.Value == "0");
                        //            item.Enabled = false;
                        //        }
                        //        else cboTujuanKunjungan.SelectedValue = "0";

                        //        cboDpjpSep_ItemsRequested(cboDpjpSep, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = response.Response.KodeDokterPembuat });
                        //        cboDpjpSep.SelectedValue = response.Response.KodeDokterPembuat;

                        //        cboDpjpSep_ItemsRequested(cboDpjpKontrol, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = response.Response.KodeDokter });
                        //        cboDpjpKontrol.SelectedValue = response.Response.KodeDokter;

                        //        cboDpjpSep_ItemsRequested(cboDpjpPelayanan, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = cboDpjpKontrol.SelectedValue });
                        //        cboDpjpPelayanan.SelectedValue = cboDpjpKontrol.SelectedValue;
                        //    }
                        //}
                    }
                }
            }
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_NoSep", txtNoSep.Text);
        }

        protected void chkPenjaminKLLSep_CheckedChanged(object sender, EventArgs e)
        {
            pnlLakaLantas.Visible = chkPenjaminKLLSep.Checked;
            if (pnlLakaLantas.Visible)
            {
                cboJenisKejadianLaka.SelectedValue = string.Empty;
                txtNoLp.Text = string.Empty;
                txtTglKejadianSep.SelectedDate = DateTime.Now.Date;
                txtLokasiKLLSep.Text = string.Empty;
                chkSuplesiSep.Checked = false;
                chkSuplesiSep_CheckedChanged(null, null);

                cboPropinsiSep.Items.Clear();
                cboPropinsiSep.SelectedValue = string.Empty;
                cboPropinsiSep.Text = string.Empty;

                var svc = new Common.BPJS.VClaim.v11.Service();
                var response = svc.GetPropinsi();
                if (response.MetaData.IsValid)
                {
                    cboPropinsiSep.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (var item in response.Response.List)
                    {
                        cboPropinsiSep.Items.Add(new RadComboBoxItem(item.Nama, item.Kode));
                    }
                }

                cboKabupatenSep.Items.Clear();
                cboKabupatenSep.SelectedValue = string.Empty;
                cboKabupatenSep.Text = string.Empty;

                cboKecamatanSep.Items.Clear();
                cboKecamatanSep.SelectedValue = string.Empty;
                cboKecamatanSep.Text = string.Empty;
            }
        }

        protected void cboNoMRSep_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            cboNoMRSep.DataSource = null;
            cboNoMRSep.DataBind();
            cboNoMRSep.Items.Clear();
            cboNoMRSep.SelectedValue = string.Empty;

            //if (e.Text.Trim().Length < 3) return;

            var patient = new PatientCollection();
            patient.Query.es.Top = 20;
            patient.Query.Where(
                patient.Query.Or(
                    string.Format("<RTRIM(FirstName+' '+MiddleName)+' '+LastName LIKE '%{0}%' OR >", e.Text),
                    //patient.Query.MedicalNo.Like("%" + e.Text + "%"),
                    string.Format("<REPLACE(MedicalNo, '-', '') = '{0}'>", e.Text.Replace("-", string.Empty))
                    )
                );
            patient.Query.Load();

            cboNoMRSep.DataSource = patient;
            cboNoMRSep.DataBind();
        }

        protected void cboNoMRSep_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((Patient)e.Item.DataItem).MedicalNo + " - " + ((Patient)e.Item.DataItem).PatientName;
            e.Item.Value = ((Patient)e.Item.DataItem).MedicalNo + "|" + ((Patient)e.Item.DataItem).PatientID;
        }

        protected string IsLinkedToRegistration
        {
            get
            {
                if (string.IsNullOrEmpty(txtNoSep.Text)) return string.Empty;
                var reg = new Registration();
                reg.Query.es.Top = 1;
                reg.Query.Where(reg.Query.BpjsSepNo == txtNoSep.Text, reg.Query.GuarantorCardNo == txtNoPesertaPeserta.Text, reg.Query.IsVoid == false);
                if (reg.Query.Load()) return reg.RegistrationNo;
                return string.Empty;
            }
        }

        protected void chkSuplesiSep_CheckedChanged(object sender, EventArgs e)
        {
            pnlSuplesi.Visible = chkSuplesiSep.Checked;
            if (pnlSuplesi.Visible)
            {
                txtSuplesiSep.Text = string.Empty;

                //cboPropinsiSep.Items.Clear();
                //cboPropinsiSep.SelectedValue = string.Empty;
                //cboPropinsiSep.Text = string.Empty;

                //var svc = new Common.BPJS.VClaim.v11.Service();
                //var response = svc.GetPropinsi();
                //if (response.MetaData.IsValid)
                //{
                //    cboPropinsiSep.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                //    foreach (var item in response.Response.List)
                //    {
                //        cboPropinsiSep.Items.Add(new RadComboBoxItem(item.Nama, item.Kode));
                //    }
                //}

                //cboKabupatenSep.Items.Clear();
                //cboKabupatenSep.SelectedValue = string.Empty;
                //cboKabupatenSep.Text = string.Empty;

                //cboKecamatanSep.Items.Clear();
                //cboKecamatanSep.SelectedValue = string.Empty;
                //cboKecamatanSep.Text = string.Empty;
            }
        }

        protected void cboDpjpSep_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = (string.IsNullOrEmpty(((DataRowView)e.Item.DataItem)["BridgingID"].ToString()) ? string.Empty : ((DataRowView)e.Item.DataItem)["BridgingID"].ToString() + " - ") + ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["BridgingID"].ToString();
        }

        protected void cboDpjpSep_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            //cboDpjpSep.DataSource = null;
            //cboDpjpSep.DataBind();

            var cbo = (o as RadComboBox);

            cbo.Items.Clear();
            cbo.SelectedValue = string.Empty;

            var sub = new ParamedicBridgingQuery("a");
            var su = new ParamedicQuery("b");

            sub.Select(sub.BridgingID, "<CASE WHEN a.BridgingName = '' THEN b.ParamedicName ELSE a.BridgingName END AS ParamedicName>");
            sub.InnerJoin(su).On(sub.ParamedicID == su.ParamedicID && sub.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString()));
            //sub.Where(string.Format("<CASE WHEN a.BridgingName = '' THEN b.ParamedicName ELSE a.BridgingName END LIKE '%{0}%'>", e.Text));
            sub.Where(sub.BridgingID == e.Text);

            cbo.DataSource = sub.LoadDataTable();
            cbo.DataBind();
        }

        protected void cboPropinsiSep_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboKabupatenSep.Items.Clear();
            cboKabupatenSep.SelectedValue = string.Empty;
            cboKabupatenSep.Text = string.Empty;

            if (string.IsNullOrEmpty(e.Value)) return;

            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetKabupaten(e.Value);
            if (response.MetaData.IsValid)
            {
                cboKabupatenSep.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var item in response.Response.List)
                {
                    cboKabupatenSep.Items.Add(new RadComboBoxItem(item.Nama, item.Kode));
                }
            }
        }

        protected void cboKabupatenSep_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboKecamatanSep.Items.Clear();
            cboKecamatanSep.SelectedValue = string.Empty;
            cboKecamatanSep.Text = string.Empty;

            if (string.IsNullOrEmpty(e.Value)) return;

            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetKecamatan(e.Value);
            if (response.MetaData.IsValid)
            {
                cboKecamatanSep.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var item in response.Response.List)
                {
                    cboKecamatanSep.Items.Add(new RadComboBoxItem(item.Nama, item.Kode));
                }
            }
        }

        protected void cboRegistrasiIGD_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString() + " - " + Convert.ToDateTime(((DataRowView)e.Item.DataItem)["RegistrationDate"]).ToString("dd/MM/yyyy") + " - " + ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString();
        }

        protected void cboRegistrasiIGD_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            //cboRegistrasiIGD.DataSource = null;
            //cboRegistrasiIGD.DataBind();
            cboRegistrasiIGD.Items.Clear();
            cboRegistrasiIGD.SelectedValue = string.Empty;

            var reg = new RegistrationQuery("a");
            var patient = new PatientQuery("b");
            var unit = new ServiceUnitQuery("c");

            reg.es.Top = 10;
            reg.Select(
                reg.RegistrationNo,
                reg.RegistrationDate,
                unit.ServiceUnitName
                );
            reg.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            reg.Where(patient.MedicalNo == cboNoMRSep.SelectedValue.Split('|')[0]);
            reg.Where(
                reg.RegistrationNo == e.Text,
                reg.SRRegistrationType.In(AppConstant.RegistrationType.EmergencyPatient, AppConstant.RegistrationType.OutPatient, AppConstant.RegistrationType.InPatient),
                reg.IsClosed == false,
                reg.IsVoid == false
                );
            reg.OrderBy(reg.RegistrationDate.Descending);

            cboRegistrasiIGD.DataSource = reg.LoadDataTable();
            cboRegistrasiIGD.DataBind();
        }

        protected void btnCariHistory_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtNomor.Text) && !txtPeriode1.IsEmpty && !txtPeriode2.IsEmpty)
                {
                    var svc = new Common.BPJS.VClaim.v11.Service();
                    var riwayat = svc.GetDataHistoriPelayananPeserta(txtNomor.Text, txtPeriode1.SelectedDate.Value.Date, txtPeriode2.SelectedDate.Value.Date);
                    if (riwayat.MetaData.IsValid)
                    {
                        if (riwayat.Response.Histori != null)
                        {
                            var list = riwayat.Response.Histori.ToDataTable<Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori>();
                            grdList.DataSource = list;
                            if (cboPelayanan.SelectedValue == "2")
                            {
                                var riwayatRanapList = list.AsEnumerable().Where(t => t.Field<string>("PpkPelayanan") == ConfigurationManager.AppSettings["BPJSHospitalID"] && t.Field<string>("JnsPelayanan") == "2").Take(1).ToList();
                                foreach (var riwayatRanap in riwayatRanapList)
                                {

                                }
                            }
                        }
                        else
                        {
                            var list = new List<Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori>();
                            list.Add(new Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori()
                            {
                                Poli = "Tidak ada data."
                            });
                            grdList.DataSource = list.ToDataTable<Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori>();
                        }
                        grdList.DataBind();

                    }
                    else
                    {
                        var list = new List<Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori>();
                        list.Add(new Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori()
                        {
                            Poli = $"{riwayat.MetaData.Code} - {riwayat.MetaData.Message}"
                        });
                        grdList.DataSource = list.ToDataTable<Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori>();
                        grdList.DataBind();
                    }
                }
                else
                {
                    var list = new List<Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori>();
                    list.Add(new Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori()
                    {
                        Poli = "Tidak ada data."
                    });
                    grdList.DataSource = list.ToDataTable<Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori>();
                    grdList.DataBind();
                }
            }
            catch (Exception ex)
            {
                var list = new List<Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori>();
                list.Add(new Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori()
                {
                    Poli = ex.Message
                });
                grdList.DataSource = list.ToDataTable<Common.BPJS.VClaim.v11.Monitoring.HistoriPelayananPeserta.Histori>();
                grdList.DataBind();
            }
        }

        protected void cboFlagProcedure_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboKodePenunjang.Items.Clear();
            cboKodePenunjang.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            if (e.Value == "0")
            {
                cboKodePenunjang.Items.Add(new RadComboBoxItem("Laboratorium", "7"));
                cboKodePenunjang.Items.Add(new RadComboBoxItem("USG", "8"));
                cboKodePenunjang.Items.Add(new RadComboBoxItem("Farmasi", "9"));
                cboKodePenunjang.Items.Add(new RadComboBoxItem("Lain-Lain", "10"));
                cboKodePenunjang.Items.Add(new RadComboBoxItem("MRI", "11"));
            }
            else if (e.Value == "1")
            {
                cboKodePenunjang.Items.Add(new RadComboBoxItem("Radioterapi", "1"));
                cboKodePenunjang.Items.Add(new RadComboBoxItem("Kemoterapi", "2"));
                cboKodePenunjang.Items.Add(new RadComboBoxItem("Rehabilitasi Medik", "3"));
                cboKodePenunjang.Items.Add(new RadComboBoxItem("Rehabilitasi Psikososial", "4"));
                cboKodePenunjang.Items.Add(new RadComboBoxItem("Transfusi Darah", "5"));
                cboKodePenunjang.Items.Add(new RadComboBoxItem("Pelayanan Gigi", "6"));
                cboKodePenunjang.Items.Add(new RadComboBoxItem("HEMODIALISA", "12"));
            }
        }

        protected void cboTujuanKunjungan_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //if (string.IsNullOrEmpty(e.Value))
            //{
            //    cboFlagProcedure.SelectedValue = string.Empty;
            //    cboFlagProcedure_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, string.Empty, string.Empty));

            //    //if (e.Value == "1")
            //    //{
            //    //    cboAssesmentPelayanan.SelectedValue = string.Empty;
            //    //}
            //}
            //else
            //{
            if (e.Value == "0")
            {
                TujuanKunjunganNormal(false);
            }
            else if (e.Value == "1")
            {
                TujuanKunjunganProsedur();
            }
            else if (e.Value == "2")
            {
                TujuanKunjunganKonsulDokter(true);
            }
            //}
        }

        protected void cboPoliSep_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value.ToLower() == "igd")
            {
                cboJenisRujukanSep.SelectedValue = "2";
                cboAsalRujukanSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = ConfigurationManager.AppSettings["BPJSHospitalID"] });
                cboAsalRujukanSep.SelectedValue = ConfigurationManager.AppSettings["BPJSHospitalID"];

                var svc = new Common.BPJS.VClaim.v11.Service();
                var faskes = svc.GetFaskes(ConfigurationManager.AppSettings["BPJSHospitalID"], Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                if (faskes.MetaData.IsValid)
                {
                    cboAsalRujukanSep.Text = faskes.Response.Faskes[0].Kode + " - " + faskes.Response.Faskes[0].Nama;
                }

                txtTglRujukanSep.SelectedDate = txtTglSep.SelectedDate;

                cboNoRujukan.Items.Clear();
                cboNoRujukan.SelectedValue = string.Empty;
                cboNoRujukan.Text = string.Empty;

                cboDpjpSep.Items.Clear();
                cboDpjpSep.SelectedValue = string.Empty;
                cboDpjpSep.Text = string.Empty;

                //cboTujuanKunjungan.SelectedValue = "0";
                //cboFlagProcedure.SelectedValue = string.Empty;
                //cboFlagProcedure_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, string.Empty, string.Empty));
                //cboKodePenunjang.SelectedValue = string.Empty;
                //cboAssesmentPelayanan.SelectedValue = string.Empty;

                //var bs = new BpjsSEPCollection();
                //bs.Query.Where(bs.Query.TanggalSEP.Date() == txtTglSep.SelectedDate?.Date, bs.Query.NomorKartu == txtNoPesertaPeserta.Text);
                //if (bs.Query.Load()) TujuanKunjunganKonsulDokter(false);
                //else 
                TujuanKunjunganNormal(true);

                cboDpjpKontrol.Items.Clear();
                cboDpjpKontrol.SelectedValue = string.Empty;
                cboDpjpKontrol.Text = string.Empty;

                cboNoSkdp.Items.Clear();
                cboNoSkdp.SelectedValue = string.Empty;
                cboNoSkdp.Text = string.Empty;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(cboPoliSep.SelectedValue) && !string.IsNullOrWhiteSpace(cboNoRujukan.Text))
                {
                    var svc = new Common.BPJS.VClaim.v11.Service();
                    var response = new Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan();
                    bool isValid = false;

                    response = svc.GetRujukan(true, cboNoRujukan.SelectedValue, Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);
                    isValid = response.MetaData.IsValid;

                    if (!isValid)
                    {
                        svc = new Common.BPJS.VClaim.v11.Service();
                        response = svc.GetRujukan(true, cboNoRujukan.SelectedValue, Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                        if (response.MetaData.IsValid)
                        {
                            var reffer = response.Response.Rujukan;

                            if (reffer.PoliRujukan != null)
                            {
                                if (cboPoliSep.SelectedValue.Split('#')[0] != response.Response.Rujukan.PoliRujukan.Kode)
                                {
                                    //cboTujuanKunjungan.SelectedValue = "0";

                                    TujuanKunjunganNormalKonsulInternal();
                                }
                                else LoadDetailRujukan(cboNoRujukan.Text);
                            }
                        }
                    }
                    else
                    {
                        var reffer = response.Response.Rujukan;

                        if (reffer.PoliRujukan != null)
                        {
                            if (cboPoliSep.SelectedValue.Split('#')[0] != response.Response.Rujukan.PoliRujukan.Kode)
                            {
                                //cboTujuanKunjungan.SelectedValue = "0";

                                TujuanKunjunganNormalKonsulInternal();
                            }
                            else LoadDetailRujukan(cboNoRujukan.Text);
                        }
                    }
                }
            }
        }

        protected void cboNoSkdp_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Value))
            {
                //cboDpjpKontrol.DataSource = null;
                //cboDpjpKontrol.DataBind();
                cboDpjpKontrol.Items.Clear();
                cboDpjpKontrol.SelectedValue = string.Empty;
                cboDpjpKontrol.Text = string.Empty;
                return;
            }
            var svc = new Common.BPJS.VClaim.v11.Service();
            var kontrolResponse = svc.GetRencanaKontrolByNoSuratKontrol(e.Value);
            if (kontrolResponse.MetaData.IsValid)
            {
                //string format = "yyyy-MM-dd";
                //DateTime.TryParseExact(filter[3], format, null, System.Globalization.DateTimeStyles.None, out var date);

                var sub = new ParamedicBridgingQuery("a");
                var su = new ParamedicQuery("b");
                var sup = new ServiceUnitParamedicQuery("c");

                sub.es.Distinct = true;
                sub.Select(sub.BridgingID, "<CASE WHEN a.BridgingName = '' THEN b.ParamedicName ELSE a.BridgingName END AS ParamedicName>", sub.ParamedicID);
                sub.InnerJoin(su).On(sub.ParamedicID == su.ParamedicID && sub.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString()));
                //if (filter[2] == "2")
                //{
                var units = new List<string>();
                if (!string.IsNullOrWhiteSpace(kontrolResponse.Response.KodeDokter))
                {
                    var bridging = new ServiceUnitBridgingCollection();
                    bridging.Query.Where(bridging.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString()), bridging.Query.BridgingID == kontrolResponse.Response.PoliTujuan);
                    if (bridging.Query.Load())
                    {
                        units = bridging.Select(b => b.ServiceUnitID).ToList();
                        sub.InnerJoin(sup).On(sub.ParamedicID == sup.ParamedicID && sup.ServiceUnitID.In(bridging.Select(b => b.ServiceUnitID)));
                    }
                }
                //}
                //sub.Where(string.Format("<CASE WHEN a.BridgingName = '' THEN b.ParamedicName ELSE a.BridgingName END LIKE '%{0}%'>", filter[0]));
                sub.Where(su.IsActive == true);
                var dtb = sub.LoadDataTable();

                //var result = new List<RadComboBoxItemData>(dtb.Rows.Count);
                //foreach (DataRow data in dtb.Rows)
                //{
                //if (units.Any())
                //{
                //    foreach (var unit in units)
                //    {
                //        var sUnit = new ServiceUnit();
                //        sUnit.LoadByPrimaryKey(unit);
                //        if (sUnit.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                //        {
                //            var sch = new ParamedicScheduleDate();
                //            if (!sch.LoadByPrimaryKey(unit, data["ParamedicID"].ToString(), txtTglSep.SelectedDate?.Year.ToString(), txtTglSep.SelectedDate.Value.Date)) data.Delete();
                //        }
                //    }
                //}

                //var item = new RadComboBoxItemData();
                //item.Text = (string.IsNullOrEmpty(data["BridgingID"].ToString()) ? string.Empty : data["BridgingID"].ToString() + " - ") + data["ParamedicName"].ToString();
                //item.Value = data["BridgingID"].ToString();
                //result.Add(item);
                //}
                //dtb.AcceptChanges();

                cboDpjpKontrol.DataSource = dtb;
                cboDpjpKontrol.DataBind();
                if (dtb.Rows.Count == 1) cboDpjpKontrol.SelectedIndex = 0;
                else
                {
                    cboDpjpKontrol.SelectedValue = kontrolResponse.Response.KodeDokter;

                    if (!string.IsNullOrWhiteSpace(cboDpjpKontrol.SelectedValue) && cboPelayanan.SelectedValue == "2")
                    {
                        cboDpjpSep_ItemsRequested(cboDpjpSep, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = string.IsNullOrWhiteSpace(kontrolResponse.Response.KodeDokterPembuat) ? string.Empty : kontrolResponse.Response.KodeDokterPembuat });
                        cboDpjpSep.SelectedValue = kontrolResponse.Response.KodeDokterPembuat;

                        cboDpjpSep_ItemsRequested(cboDpjpPelayanan, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = string.IsNullOrWhiteSpace(kontrolResponse.Response.KodeDokter) ? string.Empty : kontrolResponse.Response.KodeDokter });
                        cboDpjpPelayanan.SelectedValue = kontrolResponse.Response.KodeDokter;
                    }
                }

                if (cboPelayanan.SelectedValue == "2")
                {
                    //if (!string.IsNullOrWhiteSpace(kontrolResponse.Response.PoliTujuan))
                    //{
                    //    cboPoliSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = kontrolResponse.Response.PoliTujuan });
                    //    if (cboPoliSep.Items.Any())
                    //    {
                    //        //cboPoliSep.SelectedValue = kontrolResponse.Response.PoliTujuan;
                    //        //cboPoliSep.Text = kontrolResponse.Response.PoliTujuan;

                    //        var index = -1;
                    //        for (int i = 0; i < cboPoliSep.Items.Count; i++)
                    //        {
                    //            if (cboPoliSep.Items[i].Value == kontrolResponse.Response.PoliTujuan)
                    //            {
                    //                index = i;
                    //                break;
                    //            }
                    //        }
                    //        if (index > -1)
                    //        {
                    //            cboPoliSep.SelectedValue = kontrolResponse.Response.PoliTujuan;
                    //            cboPoliSep.SelectedIndex = index;
                    //        }
                    //    }
                    //}

                    //if (string.IsNullOrWhiteSpace(cboNoRujukan.Text))
                    //{
                    //    cboNoRujukan.Items.Clear();
                    //    cboNoRujukan.SelectedValue = string.Empty;
                    //    cboNoRujukan.Text = string.Empty;

                    //    var rjk = new List<Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2>();

                    //    if (kontrolResponse.Response.Sep != null && !string.IsNullOrWhiteSpace(kontrolResponse.Response.Sep.JnsPelayanan) && kontrolResponse.Response.Sep.JnsPelayanan.Trim().ToLower() == "rawat inap")
                    //    {
                    //        rjk.Add(new Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2()
                    //        {
                    //            NoKunjungan = kontrolResponse.Response.Sep.NoSep,
                    //            TglKunjungan = Convert.ToDateTime(kontrolResponse.Response.Sep.TglSep).ToString("yyyy-MM-dd"),
                    //            ProvPerujuk = new Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.ProvPerujuk() { Nama = kontrolResponse.Response.ProvPerujuk == null || string.IsNullOrWhiteSpace(kontrolResponse.Response.ProvPerujuk.NmProviderPerujuk) ? string.Empty : kontrolResponse.Response.ProvPerujuk.NmProviderPerujuk },
                    //            PoliRujukan = new Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.PoliRujukan() { Nama = string.IsNullOrWhiteSpace(kontrolResponse.Response.Sep.Poli) ? string.Empty : kontrolResponse.Response.Sep.Poli }
                    //        });
                    //    }
                    //    else if (kontrolResponse.Response.Sep != null && !string.IsNullOrWhiteSpace(kontrolResponse.Response.Sep.JnsPelayanan) && kontrolResponse.Response.Sep.JnsPelayanan.Trim().ToLower() == "rawat jalan")
                    //    {
                    //        rjk.Add(new Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2()
                    //        {
                    //            NoKunjungan = kontrolResponse.Response.Sep.ProvPerujuk.NoRujukan,
                    //            TglKunjungan = Convert.ToDateTime(kontrolResponse.Response.Sep.ProvPerujuk.TglRujukan).ToString("yyyy-MM-dd"),
                    //            ProvPerujuk = new Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.ProvPerujuk() { Nama = kontrolResponse.Response.Sep.ProvPerujuk == null || string.IsNullOrWhiteSpace(kontrolResponse.Response.Sep.ProvPerujuk.NmProviderPerujuk) ? string.Empty : kontrolResponse.Response.Sep.ProvPerujuk.NmProviderPerujuk },
                    //            PoliRujukan = new Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.PoliRujukan() { Nama = string.IsNullOrWhiteSpace(kontrolResponse.Response.Sep.Poli) ? string.Empty : kontrolResponse.Response.Sep.Poli }
                    //        });
                    //    }
                    //    cboNoRujukan.DataSource = rjk.Distinct();
                    //    cboNoRujukan.DataBind();
                    //    if (rjk.Any()) cboNoRujukan.SelectedIndex = 0;
                    //}

                    if (kontrolResponse.Response.Sep != null)
                    {
                        if (!string.IsNullOrWhiteSpace(kontrolResponse.Response.Sep.JnsPelayanan) && kontrolResponse.Response.Sep.JnsPelayanan.Trim().ToLower() == "rawat inap")
                        {
                            txtTglRujukanSep.SelectedDate = Convert.ToDateTime(kontrolResponse.Response.Sep.TglSep);
                            cboNoRujukan.Text = kontrolResponse.Response.Sep.NoSep;

                            TujuanKunjunganNormal(false);
                        }
                        else if (!string.IsNullOrWhiteSpace(kontrolResponse.Response.Sep.JnsPelayanan) && kontrolResponse.Response.Sep.JnsPelayanan.Trim().ToLower() == "rawat jalan")
                        {
                            txtTglRujukanSep.SelectedDate = Convert.ToDateTime(kontrolResponse.Response.Sep.ProvPerujuk.TglRujukan);
                        }
                    }

                    svc = new Common.BPJS.VClaim.v11.Service();
                    var data = svc.GetRencanaKontrolByNoSep(kontrolResponse.Response.Sep.NoSep);
                    if (data.MetaData.IsValid)
                    {
                        cboJenisRujukanSep.SelectedValue = "1";
                        cboAsalRujukanSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = data.Response.ProvUmum.KdProvider });
                        cboAsalRujukanSep.SelectedValue = data.Response.ProvUmum.KdProvider;
                        cboAsalRujukanSep.Text = data.Response.ProvUmum.KdProvider + " - " + data.Response.ProvUmum.NmProvider;
                    }

                    if (string.IsNullOrWhiteSpace(cboDiagnosaSep.SelectedValue) && kontrolResponse.Response.Sep.Diagnosa != null)
                    {
                        cboDiagnosaSep.Items.Clear();
                        cboDiagnosaSep.SelectedValue = string.Empty;
                        cboDiagnosaSep.Text = string.Empty;

                        cboDiagnosaSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = kontrolResponse.Response.Sep.Diagnosa.Split('-')[0].Trim() });
                        if (cboDiagnosaSep.Items.Count > 0) cboDiagnosaSep.SelectedIndex = 0;
                    }
                }
                else
                {
                    svc = new Common.BPJS.VClaim.v11.Service();
                    var riwayat = svc.GetDataHistoriPelayananPeserta(txtNoPesertaPeserta.Text, txtTglSep.SelectedDate.Value.Date.AddDays(-1).ToString("yyyy-MM-dd"), txtTglSep.SelectedDate.Value.Date);
                    if (riwayat.MetaData.IsValid)
                    {
                        if (riwayat.Response.Histori != null && riwayat.Response.Histori.Any())
                        {
                            var sep = riwayat.Response.Histori.OrderByDescending(r => r.TglSep).FirstOrDefault(r => r.JnsPelayanan == "2");
                            if (sep != null)
                            {
                                cboNoRujukan.Items.Clear();
                                cboNoRujukan.SelectedValue = string.Empty;
                                cboNoRujukan.Text = string.Empty;

                                var rjk = new List<Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2>
                                    {
                                        new Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2()
                                        {
                                            NoKunjungan = sep.NoSep,
                                            TglKunjungan = Convert.ToDateTime(sep.TglSep).ToString("yyyy-MM-dd"),
                                            ProvPerujuk = new Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.ProvPerujuk() { Nama = sep.PpkPelayanan },
                                            PoliRujukan = new Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.PoliRujukan() { Nama = sep.Poli }
                                        }
                                    };
                                cboNoRujukan.DataSource = rjk;
                                cboNoRujukan.DataBind();
                                cboNoRujukan.SelectedValue = sep.NoSep;

                                svc = new Common.BPJS.VClaim.v11.Service();
                                var data = svc.GetRencanaKontrolByNoSuratKontrol(e.Value);
                                if (data.MetaData.IsValid)
                                {
                                    if (data.Response.ProvPerujuk != null)
                                    {
                                        cboJenisRujukanSep.SelectedValue = data.Response.ProvPerujuk.AsalRujukan;
                                        cboAsalRujukanSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = data.Response.ProvPerujuk.KdProviderPerujuk });
                                        cboAsalRujukanSep.SelectedValue = data.Response.ProvPerujuk.KdProviderPerujuk;
                                        cboAsalRujukanSep.Text = data.Response.ProvPerujuk.KdProviderPerujuk + " - " + data.Response.ProvPerujuk.NmProviderPerujuk;
                                    }
                                    else
                                    {
                                        cboJenisRujukanSep.SelectedValue = "2";
                                        cboAsalRujukanSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = ConfigurationManager.AppSettings["BPJSHospitalID"] });
                                        cboAsalRujukanSep.SelectedValue = ConfigurationManager.AppSettings["BPJSHospitalID"];

                                        svc = new Common.BPJS.VClaim.v11.Service();
                                        var faskes = svc.GetFaskes(ConfigurationManager.AppSettings["BPJSHospitalID"], Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                                        if (faskes.MetaData.IsValid)
                                        {
                                            cboAsalRujukanSep.Text = faskes.Response.Faskes[0].Kode + " - " + faskes.Response.Faskes[0].Nama;
                                        }
                                    }
                                }

                                if (string.IsNullOrWhiteSpace(cboDiagnosaSep.SelectedValue) && kontrolResponse.Response.Sep.Diagnosa != null)
                                {
                                    cboDiagnosaSep.Items.Clear();
                                    cboDiagnosaSep.SelectedValue = string.Empty;
                                    cboDiagnosaSep.Text = string.Empty;

                                    if (kontrolResponse.Response.Sep != null && !string.IsNullOrWhiteSpace(kontrolResponse.Response.Sep.Diagnosa))
                                    {
                                        cboDiagnosaSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = kontrolResponse.Response.Sep.Diagnosa.Split('-')[0].Trim() });
                                        if (cboDiagnosaSep.Items.Count > 0) cboDiagnosaSep.SelectedIndex = 0;
                                    }
                                }
                            }
                            else
                            {
                                cboJenisRujukanSep.SelectedValue = "2";
                                cboAsalRujukanSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = ConfigurationManager.AppSettings["BPJSHospitalID"] });
                                cboAsalRujukanSep.SelectedValue = ConfigurationManager.AppSettings["BPJSHospitalID"];

                                svc = new Common.BPJS.VClaim.v11.Service();
                                var faskes = svc.GetFaskes(ConfigurationManager.AppSettings["BPJSHospitalID"], Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                                if (faskes.MetaData.IsValid)
                                {
                                    cboAsalRujukanSep.Text = faskes.Response.Faskes[0].Kode + " - " + faskes.Response.Faskes[0].Nama;
                                }

                                cboNoRujukan.Items.Clear();
                                cboNoRujukan.SelectedValue = string.Empty;
                                cboNoRujukan.Text = string.Empty;
                            }
                        }
                    }
                    else
                    {
                        var bpjs = new BpjsSEP();
                        bpjs.Query.es.Top = 1;
                        if (rblJenis.SelectedValue == "1")
                            bpjs.Query.Where(bpjs.Query.NoRujukan == txtNomor.Text);
                        else if (rblJenis.SelectedValue == "2")
                            bpjs.Query.Where(bpjs.Query.NomorKartu == txtNomor.Text);
                        bpjs.Query.Where(bpjs.Query.JenisPelayanan == "2");
                        bpjs.Query.OrderBy(bpjs.Query.TanggalSEP.Descending);
                        if (bpjs.Query.Load())
                        {
                            cboNoRujukan.Items.Clear();
                            cboNoRujukan.SelectedValue = string.Empty;
                            cboNoRujukan.Text = string.Empty;

                            var rjk = new List<Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2>
                            {
                                new Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2()
                                {
                                    NoKunjungan = bpjs.NoSEP,
                                    TglKunjungan = Convert.ToDateTime(bpjs.TanggalSEP).ToString("yyyy-MM-dd"),
                                    ProvPerujuk = new Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.ProvPerujuk() { Nama = bpjs.PPKPelayanan },
                                    PoliRujukan = new Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.PoliRujukan() { Nama = bpjs.PoliTujuan }
                                }
                            };
                            cboNoRujukan.DataSource = rjk;
                            cboNoRujukan.DataBind();
                            cboNoRujukan.SelectedValue = bpjs.NoSEP;

                            svc = new Common.BPJS.VClaim.v11.Service();
                            var data = svc.GetRencanaKontrolByNoSuratKontrol(e.Value);
                            if (data.MetaData.IsValid)
                            {
                                if (data.Response.ProvPerujuk != null)
                                {
                                    cboJenisRujukanSep.SelectedValue = data.Response.ProvPerujuk.AsalRujukan;
                                    cboAsalRujukanSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = data.Response.ProvPerujuk.KdProviderPerujuk });
                                    cboAsalRujukanSep.SelectedValue = data.Response.ProvPerujuk.KdProviderPerujuk;
                                    cboAsalRujukanSep.Text = data.Response.ProvPerujuk.KdProviderPerujuk + " - " + data.Response.ProvPerujuk.NmProviderPerujuk;
                                }
                                else
                                {
                                    cboJenisRujukanSep.SelectedValue = "2";
                                    cboAsalRujukanSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = ConfigurationManager.AppSettings["BPJSHospitalID"] });
                                    cboAsalRujukanSep.SelectedValue = ConfigurationManager.AppSettings["BPJSHospitalID"];

                                    svc = new Common.BPJS.VClaim.v11.Service();
                                    var faskes = svc.GetFaskes(ConfigurationManager.AppSettings["BPJSHospitalID"], Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                                    if (faskes.MetaData.IsValid)
                                    {
                                        cboAsalRujukanSep.Text = faskes.Response.Faskes[0].Kode + " - " + faskes.Response.Faskes[0].Nama;
                                    }
                                }
                            }

                            if (string.IsNullOrWhiteSpace(cboDiagnosaSep.SelectedValue))
                            {
                                cboDiagnosaSep.Items.Clear();
                                cboDiagnosaSep.SelectedValue = string.Empty;
                                cboDiagnosaSep.Text = string.Empty;

                                if (kontrolResponse.Response.Sep != null && !string.IsNullOrWhiteSpace(bpjs.DiagnosaAwal))
                                {
                                    cboDiagnosaSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = bpjs.DiagnosaAwal.Trim() });
                                    if (cboDiagnosaSep.Items.Count > 0) cboDiagnosaSep.SelectedIndex = 0;
                                }
                            }
                        }
                        else
                        {
                            cboJenisRujukanSep.SelectedValue = "2";
                            cboAsalRujukanSep_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = ConfigurationManager.AppSettings["BPJSHospitalID"] });
                            cboAsalRujukanSep.SelectedValue = ConfigurationManager.AppSettings["BPJSHospitalID"];

                            svc = new Common.BPJS.VClaim.v11.Service();
                            var faskes = svc.GetFaskes(ConfigurationManager.AppSettings["BPJSHospitalID"], Common.BPJS.VClaim.Enum.JenisFaskes.RS);
                            if (faskes.MetaData.IsValid)
                            {
                                cboAsalRujukanSep.Text = faskes.Response.Faskes[0].Kode + " - " + faskes.Response.Faskes[0].Nama;
                            }

                            cboNoRujukan.Items.Clear();
                            cboNoRujukan.SelectedValue = string.Empty;
                            cboNoRujukan.Text = string.Empty;
                        }
                    }
                }
            }
        }

        protected void cboNoSkdp_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((SkdpSource)e.Item.DataItem).NoSuratKontrol;
            e.Item.Value = ((SkdpSource)e.Item.DataItem).NoSuratKontrol;
        }

        protected void txtTglSep_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            txtTglRujukanSep.SelectedDate = txtTglSep.SelectedDate;
        }

        public void TujuanKunjunganNormal(bool igd)
        {
            cboTujuanKunjungan.Items.Clear();
            cboTujuanKunjungan.Items.Add(new RadComboBoxItem("Normal", "0"));
            cboTujuanKunjungan.SelectedValue = "0";

            cboFlagProcedure.Items.Clear();
            cboFlagProcedure.SelectedValue = string.Empty;
            cboFlagProcedure.Text = string.Empty;

            cboKodePenunjang.Items.Clear();
            cboKodePenunjang.SelectedValue = string.Empty;
            cboKodePenunjang.Text = string.Empty;

            cboAssesmentPelayanan.Items.Clear();
            //if (igd)
            //{
            //    cboAssesmentPelayanan.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            //    cboAssesmentPelayanan.Items.Add(new RadComboBoxItem("Poli spesialis tidak tersedia pada hari sebelumnya", "1"));
            //    cboAssesmentPelayanan.Items.Add(new RadComboBoxItem("Jam Poli telah berakhir pada hari sebelumnya", "2"));
            //    cboAssesmentPelayanan.Items.Add(new RadComboBoxItem("Dokter Spesialis yang dimaksud tidak praktek pada hari sebelumnya", "3"));
            //    cboAssesmentPelayanan.Items.Add(new RadComboBoxItem("Atas Instruksi RS", "4"));
            //    cboAssesmentPelayanan.Items.Add(new RadComboBoxItem("Tujuan Kontrol", "5"));
            //    //cboAssesmentPelayanan.SelectedValue = "1";
            //}
            //else
            //{
            cboAssesmentPelayanan.SelectedValue = string.Empty;
            cboAssesmentPelayanan.Text = string.Empty;
            //}
        }

        public void TujuanKunjunganNormalKonsulInternal()
        {
            cboTujuanKunjungan.Items.Clear();
            cboTujuanKunjungan.Items.Add(new RadComboBoxItem("Normal", "0"));
            cboTujuanKunjungan.SelectedValue = "0";

            cboFlagProcedure.Items.Clear();
            cboFlagProcedure.SelectedValue = string.Empty;
            cboFlagProcedure.Text = string.Empty;

            cboKodePenunjang.Items.Clear();
            cboKodePenunjang.SelectedValue = string.Empty;
            cboKodePenunjang.Text = string.Empty;

            cboAssesmentPelayanan.Items.Clear();
            cboAssesmentPelayanan.Items.Add(new RadComboBoxItem("Poli spesialis tidak tersedia pada hari sebelumnya", "1"));
            cboAssesmentPelayanan.Items.Add(new RadComboBoxItem("Jam Poli telah berakhir pada hari sebelumnya", "2"));
            cboAssesmentPelayanan.Items.Add(new RadComboBoxItem("Dokter Spesialis yang dimaksud tidak praktek pada hari sebelumnya", "3"));
            cboAssesmentPelayanan.Items.Add(new RadComboBoxItem("Atas Instruksi RS", "4"));
        }

        public void TujuanKunjunganNonNormal()
        {
            cboTujuanKunjungan.Items.Clear();
            cboTujuanKunjungan.Items.Add(new RadComboBoxItem("Prosedur", "1"));
            cboTujuanKunjungan.Items.Add(new RadComboBoxItem("Konsul Dokter", "2"));

            cboFlagProcedure.Items.Clear();
            cboFlagProcedure.SelectedValue = string.Empty;
            cboFlagProcedure.Text = string.Empty;

            cboKodePenunjang.Items.Clear();
            cboKodePenunjang.SelectedValue = string.Empty;
            cboKodePenunjang.Text = string.Empty;

            cboAssesmentPelayanan.Items.Clear();
            cboAssesmentPelayanan.SelectedValue = string.Empty;
            cboAssesmentPelayanan.Text = string.Empty;
        }

        public void TujuanKunjunganProsedur()
        {
            cboFlagProcedure.Items.Clear();
            cboFlagProcedure.Items.Add(new RadComboBoxItem("Prosedur Tidak Berkelanjutan", "0"));
            cboFlagProcedure.Items.Add(new RadComboBoxItem("Prosedur dan Terapi Berkelanjutan", "1"));

            cboKodePenunjang.Items.Clear();
            cboKodePenunjang.SelectedValue = string.Empty;
            cboKodePenunjang.Text = string.Empty;

            cboAssesmentPelayanan.Items.Clear();
            cboAssesmentPelayanan.SelectedValue = string.Empty;
            cboAssesmentPelayanan.Text = string.Empty;
        }

        public void TujuanKunjunganKonsulDokter(bool poliSama)
        {
            cboFlagProcedure.Items.Clear();
            cboFlagProcedure.SelectedValue = string.Empty;
            cboFlagProcedure.Text = string.Empty;

            cboKodePenunjang.Items.Clear();
            cboKodePenunjang.SelectedValue = string.Empty;
            cboKodePenunjang.Text = string.Empty;

            cboAssesmentPelayanan.Items.Clear();
            cboAssesmentPelayanan.Items.Add(new RadComboBoxItem("Poli spesialis tidak tersedia pada hari sebelumnya", "1"));
            cboAssesmentPelayanan.Items.Add(new RadComboBoxItem("Jam Poli telah berakhir pada hari sebelumnya", "2"));
            cboAssesmentPelayanan.Items.Add(new RadComboBoxItem("Dokter Spesialis yang dimaksud tidak praktek pada hari sebelumnya", "3"));
            cboAssesmentPelayanan.Items.Add(new RadComboBoxItem("Atas Instruksi RS", "4"));
            if (poliSama) cboAssesmentPelayanan.Items.Add(new RadComboBoxItem("Tujuan Kontrol", "5"));
        }

        public void ResetComboTujuanKunjungan()
        {
            cboTujuanKunjungan.Items.Clear();
            cboTujuanKunjungan.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            cboTujuanKunjungan.Items.Add(new RadComboBoxItem("Normal", "0"));
            cboTujuanKunjungan.Items.Add(new RadComboBoxItem("Prosedur", "1"));
            cboTujuanKunjungan.Items.Add(new RadComboBoxItem("Konsul Dokter", "2"));
            cboTujuanKunjungan.SelectedValue = string.Empty;

            cboFlagProcedure.Items.Clear();
            cboFlagProcedure.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            cboFlagProcedure.Items.Add(new RadComboBoxItem("Prosedur Tidak Berkelanjutan", "0"));
            cboFlagProcedure.Items.Add(new RadComboBoxItem("Prosedur dan Terapi Berkelanjutan", "1"));
            cboFlagProcedure.SelectedValue = string.Empty;

            cboKodePenunjang.Items.Clear();
            cboKodePenunjang.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            cboKodePenunjang.Items.Add(new RadComboBoxItem("Radioterapi", "1"));
            cboKodePenunjang.Items.Add(new RadComboBoxItem("Kemoterapi", "2"));
            cboKodePenunjang.Items.Add(new RadComboBoxItem("Rehabilitasi Medik", "3"));
            cboKodePenunjang.Items.Add(new RadComboBoxItem("Rehabilitasi Psikososial", "4"));
            cboKodePenunjang.Items.Add(new RadComboBoxItem("Transfusi Darah", "5"));
            cboKodePenunjang.Items.Add(new RadComboBoxItem("Pelayanan Gigi", "6"));
            cboKodePenunjang.Items.Add(new RadComboBoxItem("Laboratorium", "7"));
            cboKodePenunjang.Items.Add(new RadComboBoxItem("USG", "8"));
            cboKodePenunjang.Items.Add(new RadComboBoxItem("Farmasi", "9"));
            cboKodePenunjang.Items.Add(new RadComboBoxItem("Lain-Lain", "10"));
            cboKodePenunjang.Items.Add(new RadComboBoxItem("MRI", "11"));
            cboKodePenunjang.Items.Add(new RadComboBoxItem("HEMODIALISA", "12"));
            cboKodePenunjang.SelectedValue = string.Empty;

            cboAssesmentPelayanan.Items.Clear();
            cboAssesmentPelayanan.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            cboAssesmentPelayanan.Items.Add(new RadComboBoxItem("Poli spesialis tidak tersedia pada hari sebelumnya", "1"));
            cboAssesmentPelayanan.Items.Add(new RadComboBoxItem("Jam Poli telah berakhir pada hari sebelumnya", "2"));
            cboAssesmentPelayanan.Items.Add(new RadComboBoxItem("Dokter Spesialis yang dimaksud tidak praktek pada hari sebelumnya", "3"));
            cboAssesmentPelayanan.Items.Add(new RadComboBoxItem("Atas Instruksi RS", "4"));
            cboAssesmentPelayanan.Items.Add(new RadComboBoxItem("Tujuan Kontrol", "5"));
            cboAssesmentPelayanan.SelectedValue = string.Empty;
        }
    }
}