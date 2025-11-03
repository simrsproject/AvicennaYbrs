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
    public partial class LaporanKematianDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "LaporanKematianSearch.aspx";
            UrlPageList = "LaporanKematianList.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.KemenkesLaporanKematian;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            var svc = new Common.SirsKemkes.Service();
            var login = svc.PostLogin();

            svc = new Common.SirsKemkes.Service();
            var lokasi = svc.GetLokasiKematian(login.Data.AccessToken, 1, 1000);
            if (lokasi.Data.Any()) cboLokasiKematian.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
            foreach (var data in lokasi.Data)
            {
                cboLokasiKematian.Items.Add(new Telerik.Web.UI.RadComboBoxItem(data.Nama, data.Id.ToString()));
            }
            svc = new Common.SirsKemkes.Service();
            var penyebab = svc.GetPenyebabKematianLangsung(login.Data.AccessToken, 1, 1000);
            if (penyebab.Data.Any()) cboPenyebabKematian.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
            foreach (var data in penyebab.Data)
            {
                cboPenyebabKematian.Items.Add(new Telerik.Web.UI.RadComboBoxItem(data.Description, data.Id.ToString()));
            }
            svc = new Common.SirsKemkes.Service();
            var kasus = svc.GetKasusKematian(login.Data.AccessToken, 1, 1000);
            if (kasus.Data.Any()) cboKasusKematian.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
            foreach (var data in kasus.Data)
            {
                cboKasusKematian.Items.Add(new Telerik.Web.UI.RadComboBoxItem(data.Nama, data.Id.ToString()));
            }
            svc = new Common.SirsKemkes.Service();
            var komorbid = svc.GetKomorbid(login.Data.AccessToken, 1, 1000);
            if (komorbid.Data.Any())
            {
                cboKomorbid1.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                cboKomorbid2.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                cboKomorbid3.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                cboKomorbid4.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
            }
            foreach (var data in komorbid.Data)
            {
                cboKomorbid1.Items.Add(new Telerik.Web.UI.RadComboBoxItem($"{data.Id}-{data.Description}", data.Id.ToString()));
                cboKomorbid2.Items.Add(new Telerik.Web.UI.RadComboBoxItem($"{data.Id}-{data.Description}", data.Id.ToString()));
                cboKomorbid3.Items.Add(new Telerik.Web.UI.RadComboBoxItem($"{data.Id}-{data.Description}", data.Id.ToString()));
                cboKomorbid4.Items.Add(new Telerik.Web.UI.RadComboBoxItem($"{data.Id}-{data.Description}", data.Id.ToString()));
            }
        }

        protected override void OnMenuNewClick()
        {
            //txtTglMasuk.SelectedDate = DateTime.Now.Date;
            txtSaturasiOksigen.Value = 0;
            rbtStatusKomorbid.SelectedValue = "0";
            rbtStatusKomorbid_SelectedIndexChanged(null, null);
            rbtStatusKehamilan.SelectedValue = "0";
        }

        protected void cboKelurahanKtp_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Value) || string.IsNullOrWhiteSpace(e.Value))
            {
                txtKecamatanKtp.Text = string.Empty;
                lblKecamatanKtp.Text = string.Empty;
                txtKabKotaKtp.Text = string.Empty;
                lblKabKotaKtp.Text = string.Empty;
                txtProvinsiKtp.Text = string.Empty;
                lblProvinsiKtp.Text = string.Empty;
            }
            else
            {
                var std = new AppStandardReferenceItem();
                std.LoadByPrimaryKey(AppEnum.StandardReference.KecamatanKemenkes.ToString(), e.Value.Split('|')[1]);
                txtKecamatanKtp.Text = std.ItemID;
                lblKecamatanKtp.Text = std.ItemName;

                std = new AppStandardReferenceItem();
                std.LoadByPrimaryKey(AppEnum.StandardReference.KabKotaKemenkes.ToString(), e.Value.Split('|')[2]);
                txtKabKotaKtp.Text = std.ItemID;
                lblKabKotaKtp.Text = std.ItemName;

                std = new AppStandardReferenceItem();
                std.LoadByPrimaryKey(AppEnum.StandardReference.ProvinsiKemenkes.ToString(), e.Value.Split('|')[3]);
                txtProvinsiKtp.Text = std.ItemID;
                lblProvinsiKtp.Text = std.ItemName;
            }
        }

        protected void cboKelurahanKtp_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text.Trim().Length < 3) return;

            var kel = new AppStandardReferenceItemQuery("a");
            var kec = new AppStandardReferenceItemQuery("b");
            var kota = new AppStandardReferenceItemQuery("c");
            var prov = new AppStandardReferenceItemQuery("d");

            kel.Select(
                kel.ItemID.As("KelId"),
                kel.ItemName.As("Kelurahan"),
                kec.ItemID.As("KecId"),
                kec.ItemName.As("Kecamatan"),
                kota.ItemID.As("KabId"),
                kota.ItemName.As("KabKota"),
                prov.ItemID.As("ProvId"),
                prov.ItemName.As("Provinsi")
                );
            kel.InnerJoin(kec).On(kec.StandardReferenceID == AppEnum.StandardReference.KecamatanKemenkes && kel.Note == kec.ItemID);
            kel.InnerJoin(kota).On(kota.StandardReferenceID == AppEnum.StandardReference.KabKotaKemenkes && kec.Note == kota.ItemID);
            kel.InnerJoin(prov).On(prov.StandardReferenceID == AppEnum.StandardReference.ProvinsiKemenkes && kota.Note == prov.ItemID);
            kel.Where(kel.StandardReferenceID == AppEnum.StandardReference.KelurahanKemenkes,
                kel.Or(
                    kel.ItemID == e.Text,
                    kel.ItemName.Like($"%{e.Text}%"),
                    kec.ItemID == e.Text,
                    kec.ItemName.Like($"%{e.Text}%")
                ));
            cboKelurahanKtp.DataSource = kel.LoadDataTable();
            cboKelurahanKtp.DataBind();
        }

        protected void cboKelurahanKtp_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["Kelurahan"].ToString();
            e.Item.Value = $"{((DataRowView)e.Item.DataItem)["KelId"]}|{((DataRowView)e.Item.DataItem)["KecId"]}|{((DataRowView)e.Item.DataItem)["KabId"]}|{((DataRowView)e.Item.DataItem)["ProvId"]}";
        }

        protected void cboNama_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text.Trim().Length < 3) return;

            var reg = new RegistrationQuery("a");
            var pat = new PatientQuery("b");
            var unit = new ServiceUnitQuery("c");

            reg.es.Top = 10;
            reg.Select(pat.MedicalNo, reg.RegistrationNo, pat.PatientName, unit.ServiceUnitName);
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            if (!txtTglMasuk.IsEmpty) reg.Where(reg.RegistrationDate.Date() == txtTglMasuk.SelectedDate.Value.Date);
            reg.Where(
                reg.Or(
                    pat.MedicalNo == e.Text,
                    pat.Ssn == e.Text,
                    reg.RegistrationNo == e.Text,
                    $"< OR RTRIM(b.FirstName+' '+b.MiddleName)+' '+b.LastName LIKE '%{e.Text}%'>"
                    ),
                reg.SRDischargeCondition.In(new string[] { "E05", "E06", "E07", "I04", "I05", "O05" }), reg.IsVoid == false
                , reg.SRCovidStatus.IsNotNull(), reg.SRCovidComorbidStatus.IsNotNull()
                );
            cboNama.DataSource = reg.LoadDataTable();
            cboNama.DataBind();
        }

        protected void cboNama_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PatientName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString();
        }

        protected void cboNama_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Value) || string.IsNullOrWhiteSpace(e.Value))
            {
                txtNik.Text = string.Empty;
                rbtJenisKelamin.SelectedValue = string.Empty;
                txtTglLahir.Clear();
                txtAlamatDomisili.Text = string.Empty;
                txtTglMasuk.Clear();
                txtTglKematian.Clear();
                cboKasusKematian.SelectedValue = string.Empty;
                cboKasusKematian.Text = string.Empty;
                rbtStatusKomorbid.SelectedValue = "0";
                rbtStatusKomorbid_SelectedIndexChanged(null, null);
            }
            else
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(e.Value);

                txtTglMasuk.SelectedDate = reg.RegistrationDate;
                txtTglKematian.SelectedDate = reg.DischargeDate;

                var patient = new Patient();
                patient.LoadByPrimaryKey(reg.PatientID);

                txtNik.Text = patient.Ssn;
                if (patient.Sex == "M") rbtJenisKelamin.SelectedValue = "L";
                else rbtJenisKelamin.SelectedValue = "P";
                txtTglLahir.SelectedDate = patient.DateOfBirth;
                txtAlamatDomisili.Text = patient.Address;

                if (reg.SRCovidStatus != null)
                {
                    if (reg.SRCovidStatus == 1) cboKasusKematian.SelectedValue = "Z09";
                    else if (reg.SRCovidStatus == 2) cboKasusKematian.SelectedValue = "B34.2";
                }
                if (!string.IsNullOrWhiteSpace(reg.SRCovidComorbidStatus) && reg.SRCovidComorbidStatus == "1")
                {
                    rbtStatusKomorbid.SelectedValue = "1";
                    rbtStatusKomorbid_SelectedIndexChanged(null, null);
                }
            }
        }

        protected void rbtStatusKomorbid_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboKomorbid1.Enabled = rbtStatusKomorbid.SelectedValue == "1";
            cboKomorbid2.Enabled = rbtStatusKomorbid.SelectedValue == "1";
            cboKomorbid3.Enabled = rbtStatusKomorbid.SelectedValue == "1";
            cboKomorbid4.Enabled = rbtStatusKomorbid.SelectedValue == "1";

            if (rbtStatusKomorbid.SelectedValue == "0")
            {
                cboKomorbid1.SelectedValue = string.Empty;
                cboKomorbid1.Text = string.Empty;
                cboKomorbid2.SelectedValue = string.Empty;
                cboKomorbid2.Text = string.Empty;
                cboKomorbid3.SelectedValue = string.Empty;
                cboKomorbid3.Text = string.Empty;
                cboKomorbid4.SelectedValue = string.Empty;
                cboKomorbid4.Text = string.Empty;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var svc = new Common.SirsKemkes.Service();
            var login = svc.PostLogin();
            svc = new Common.SirsKemkes.Service();
            var request = new Common.SirsKemkes.LaporanKematian.Json.Insert.Request.Root()
            {
                Nik = txtNik.Text,
                Nama = cboNama.Text,
                JenisKelamin = rbtJenisKelamin.SelectedValue,
                TanggalLahir = txtTglLahir.SelectedDate.Value.ToString("yyyy-MM-dd"),
                KtpAlamat = txtAlamatKtp.Text,
                KtpKelurahanId = cboKelurahanKtp.SelectedValue.Split('|')[0],
                KtpKecamatanId = txtKecamatanKtp.Text,
                KtpKabKotaId = txtKabKotaKtp.Text,
                KtpProvinsiId = txtProvinsiKtp.Text,
                DomisiliAlamat = txtAlamatDomisili.Text,
                TanggalMasuk = txtTglMasuk.SelectedDate.Value.ToString("yyyy-MM-dd"),
                SaturasiOksigen = txtSaturasiOksigen.Value.ToInt(),
                TanggalKematian = txtTglKematian.SelectedDate.Value.ToString("yyyy-MM-dd"),
                LokasiKematianId = cboLokasiKematian.SelectedValue.ToInt(),
                PenyebabKematianLangsungId = cboPenyebabKematian.SelectedValue,
                KasusKematianId = cboKasusKematian.SelectedValue,
                StatusKomorbid = rbtStatusKomorbid.SelectedValue,
                Komorbid1Id = string.IsNullOrWhiteSpace(cboKomorbid1.SelectedValue) ? null : cboKomorbid1.SelectedValue,
                Komorbid2Id = string.IsNullOrWhiteSpace(cboKomorbid2.SelectedValue) ? null : cboKomorbid2.SelectedValue,
                Komorbid3Id = string.IsNullOrWhiteSpace(cboKomorbid3.SelectedValue) ? null : cboKomorbid3.SelectedValue,
                Komorbid4Id = string.IsNullOrWhiteSpace(cboKomorbid4.SelectedValue) ? null : cboKomorbid4.SelectedValue,
                StatusKehamilan = rbtStatusKehamilan.SelectedValue
            };
            var response = svc.PostInsert(login.Data.AccessToken, request);

            var wsal = new WebServiceAPILog()
            {
                DateRequest = DateTime.Now,
                IPAddress = "C",
                UrlAddress = ConfigurationManager.AppSettings["SirsKemkesLaporanKematianServiceUrlLocation"],
                Params = JsonConvert.SerializeObject(request),
                Response = JsonConvert.SerializeObject(response),
                Totalms = response.Status ? response.Data.Id : 0
            };
            wsal.Save();
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var svc = new Common.SirsKemkes.Service();
            var login = svc.PostLogin();
            svc = new Common.SirsKemkes.Service();
            var request = new Common.SirsKemkes.LaporanKematian.Json.Update.Request.Root()
            {
                TanggalMasuk = txtTglMasuk.SelectedDate.Value.ToString("yyyy-MM-dd"),
                SaturasiOksigen = txtSaturasiOksigen.Value.ToInt(),
                TanggalKematian = txtTglKematian.SelectedDate.Value.ToString("yyyy-MM-dd"),
                LokasiKematianId = cboLokasiKematian.SelectedValue.ToInt(),
                PenyebabKematianLangsungId = cboPenyebabKematian.SelectedValue,
                KasusKematianId = cboKasusKematian.SelectedValue,
                StatusKomorbid = rbtStatusKomorbid.SelectedValue,
                Komorbid1Id = string.IsNullOrWhiteSpace(cboKomorbid1.SelectedValue) ? null : cboKomorbid1.SelectedValue,
                Komorbid2Id = string.IsNullOrWhiteSpace(cboKomorbid2.SelectedValue) ? null : cboKomorbid2.SelectedValue,
                Komorbid3Id = string.IsNullOrWhiteSpace(cboKomorbid3.SelectedValue) ? null : cboKomorbid3.SelectedValue,
                Komorbid4Id = string.IsNullOrWhiteSpace(cboKomorbid4.SelectedValue) ? null : cboKomorbid4.SelectedValue,
            };
            var response = svc.PostUpdate(login.Data.AccessToken, Request.QueryString["id"].ToInt(), request);

            var wsal = new WebServiceAPILog()
            {
                DateRequest = DateTime.Now,
                IPAddress = "U",
                UrlAddress = ConfigurationManager.AppSettings["SirsKemkesLaporanKematianServiceUrlLocation"],
                Params = JsonConvert.SerializeObject(request),
                Response = JsonConvert.SerializeObject(response),
                Totalms = response.Status ? response.Data.Id : 0
            };
            wsal.Save();
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            if (parameters.Length > 0)
            {
                var id = (string)parameters[0];
                if (!string.IsNullOrWhiteSpace(id))
                {
                    WebServiceAPILog wsial = null;

                    var create = new WebServiceAPILog();
                    create.Query.Where(create.Query.IPAddress == "C" && create.Query.UrlAddress == ConfigurationManager.AppSettings["SirsKemkesLaporanKematianServiceUrlLocation"] && create.Query.Totalms == id.ToInt());
                    if (!create.Query.Load()) return;
                    wsial = create;

                    var request = JsonConvert.DeserializeObject<Common.SirsKemkes.LaporanKematian.Json.Insert.Request.Root>(wsial.Params);
                    if (request == null) return;
                    cboNama_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = request.Nik });
                    cboNama.SelectedIndex = 0;
                    cboNama_SelectedIndexChanged(null, new Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs(null, null, cboNama.SelectedValue, null));
                    txtAlamatKtp.Text = request.KtpAlamat;
                    cboKelurahanKtp_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = request.KtpKelurahanId });
                    cboKelurahanKtp.SelectedIndex = 0;
                    cboKelurahanKtp_SelectedIndexChanged(null, new Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs(null, null, cboKelurahanKtp.SelectedValue, null));
                    rbtStatusKehamilan.SelectedValue = request.StatusKehamilan;

                    var update = new WebServiceAPILog();
                    update.Query.es.Top = 1;
                    update.Query.Where(update.Query.IPAddress == "U" && update.Query.UrlAddress == ConfigurationManager.AppSettings["SirsKemkesLaporanKematianServiceUrlLocation"] && update.Query.Totalms == id.ToInt());
                    update.Query.OrderBy(update.Query.DateRequest.Descending);
                    if (update.Query.Load()) wsial = update;

                    request = JsonConvert.DeserializeObject<Common.SirsKemkes.LaporanKematian.Json.Insert.Request.Root>(wsial.Params);

                    txtSaturasiOksigen.Value = request.SaturasiOksigen;

                    string format = "yyyy-MM-dd";
                    DateTime parsed;
                    DateTime.TryParseExact(request.TanggalKematian, format, null, System.Globalization.DateTimeStyles.None, out parsed);

                    txtTglKematian.SelectedDate = parsed.Date;
                    cboLokasiKematian.SelectedValue = request.LokasiKematianId.ToString();
                    cboPenyebabKematian.SelectedValue = request.PenyebabKematianLangsungId;
                    cboKasusKematian.SelectedValue = request.KasusKematianId;
                    rbtStatusKomorbid.SelectedValue = request.StatusKomorbid;
                    rbtStatusKomorbid_SelectedIndexChanged(null, null);
                    if (!string.IsNullOrWhiteSpace(request.Komorbid1Id)) cboKomorbid1.SelectedValue = request.Komorbid1Id;
                    if (!string.IsNullOrWhiteSpace(request.Komorbid2Id)) cboKomorbid2.SelectedValue = request.Komorbid2Id;
                    if (!string.IsNullOrWhiteSpace(request.Komorbid3Id)) cboKomorbid3.SelectedValue = request.Komorbid3Id;
                    if (!string.IsNullOrWhiteSpace(request.Komorbid4Id)) cboKomorbid4.SelectedValue = request.Komorbid4Id;
                }
            }
        }
    }
}