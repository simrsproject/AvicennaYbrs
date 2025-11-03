using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Dal.Core;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class SisruteDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "SisruteSearch.aspx";
            UrlPageList = "SisruteList.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.Sisrute;

            if (!IsPostBack)
            {
                txtTglRujukan.SelectedDate = DateTime.Now.Date;
                txtJamRujukan.SelectedDate = DateTime.Now;
            }
        }

        private DataTable Registrations(string filter)
        {
            var qr = new RegistrationQuery("r");

            var qp = new PatientQuery("p");
            qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);

            var suq = new ServiceUnitQuery("x");
            qr.InnerJoin(suq).On(qr.ServiceUnitID == suq.ServiceUnitID);

            qr.Where
                (
                    qr.IsFromDispensary == false,
                    qr.IsVoid == false,
                    qr.IsClosed == false,
                    qr.IsNonPatient == false
                );


            qr.Where
                (
                    string.Format(@"<p.MedicalNo LIKE '%{0}%'
                                            OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%' 
                                            OR p.OldMedicalNo LIKE '%{0}%' 
                                            OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%' 
                                            OR LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '%{0}%'>", filter)
                );

            qr.es.Top = 25;
            qr.es.Distinct = true;
            qr.Select
                (
                    qp.MedicalNo,
                    qr.RegistrationNo,
                    qr.RegistrationDate,
                    suq.ServiceUnitName,
                    qp.PatientName
                );

            var dtb = qr.LoadDataTable();

            return dtb;
        }

        protected void cboNoRM_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                rblNIK.SelectedValue = "1";
                txtNIK.Text = string.Empty;
                txtJKN.Text = string.Empty;
                txtNamaPasien.Text = string.Empty;
                rblJenisKelamin.SelectedValue = "1";
                txtTglLahir.Clear();
                txtAlamat.Text = string.Empty;
                txtNoKontak.Text = string.Empty;
                txtDpjp.Text = string.Empty;
            }
            var patient = new Patient();
            patient.LoadByMedicalNo(e.Value.Split(';')[0]);

            if (string.IsNullOrEmpty(patient.Ssn)) rblNIK.SelectedValue = "1";
            else rblNIK.SelectedValue = "0";
            txtNIK.Text = patient.Ssn;
            txtJKN.Text = patient.GuarantorCardNo;
            txtNamaPasien.Text = patient.PatientName;
            rblJenisKelamin.SelectedValue = patient.Sex == "M" ? "1" : "2";
            txtTempatLahir.Text = patient.CityOfBirth;
            txtTglLahir.SelectedDate = patient.DateOfBirth.Value.Date;
            txtAlamat.Text = patient.Address;
            txtNoKontak.Text = patient.MobilePhoneNo;

            var reg = new Registration();
            reg.LoadByPrimaryKey(e.Value.Split(';')[1]);

            var medic = new Paramedic();
            medic.LoadByPrimaryKey(reg.ParamedicID);

            txtDpjp.Text = string.Format("{0};{1})", medic.ParamedicID, medic.ParamedicName);
        }

        protected void cboNoRM_ItemsRequested(object o, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            cboNoRM.DataSource = Registrations(e.Text);
            cboNoRM.DataBind();
        }

        protected void cboNoRM_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            e.Item.Text = string.Format("{0} ; {1} ; {2}", ((DataRowView)e.Item.DataItem)["PatientName"].ToString(), ((DataRowView)e.Item.DataItem)["MedicalNo"].ToString(),
                ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString());
            e.Item.Value = string.Format("{0};{1}", ((DataRowView)e.Item.DataItem)["MedicalNo"].ToString(), ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString());
        }

        protected void cboKodeIcdX_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((RadComboBoxItemData)e.Item.DataItem).Text;
            e.Item.Value = ((RadComboBoxItemData)e.Item.DataItem).Value;
        }

        protected void cboAlasanRujukan_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((Common.Sisrute.Common.Data)e.Item.DataItem).NAMA;
            e.Item.Value = ((Common.Sisrute.Common.Data)e.Item.DataItem).KODE;
        }

        protected void cboAlasanRujukan_ItemsRequested(object o, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            var svc = new Common.Sisrute.Service();
            var response = JsonConvert.DeserializeObject<Common.Sisrute.Referensi.Alasan>(svc.GetReferensi(e.Text, 1));
            cboAlasanRujukan.DataSource = response.Data;
            cboAlasanRujukan.DataBind();
        }

        protected void cboKodeIcd9Cm_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ProcedureName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ProcedureID"].ToString();

        }

        protected void cboFaskesTujuan_ItemsRequested(object o, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            var svc = new Common.Sisrute.Service();
            var response = JsonConvert.DeserializeObject<Common.Sisrute.Referensi.Faskes>(svc.GetReferensi(e.Text, 2));
            cboFaskesTujuan.DataSource = response.Data;
            cboFaskesTujuan.DataBind();
        }

        private Common.Sisrute.Rujukan.SetRujukan.RootObject SetEntity()
        {
            var entity = new Common.Sisrute.Rujukan.SetRujukan.RootObject();

            var PASIEN = new Common.Sisrute.Rujukan.SetRujukan.PASIEN();
            var norm = cboNoRM.SelectedValue.Split(';');
            PASIEN.NORM = norm[0];
            PASIEN.NIK = txtNIK.Text;
            PASIEN.NO_KARTU_JKN = txtJKN.Text;
            PASIEN.NAMA = txtNamaPasien.Text;
            PASIEN.JENIS_KELAMIN = rblJenisKelamin.SelectedValue;
            PASIEN.TANGGAL_LAHIR = txtTglLahir.SelectedDate.Value.ToString("yyyy-MM-dd");
            PASIEN.TEMPAT_LAHIR = txtTempatLahir.Text;
            PASIEN.ALAMAT = txtAlamat.Text;
            PASIEN.KONTAK = txtNoKontak.Text;
            entity.PASIEN = PASIEN;

            var RUJUKAN = new Common.Sisrute.Rujukan.SetRujukan.RUJUKAN();
            RUJUKAN.JENIS_RUJUKAN = rblJenisRujukan.SelectedValue;
            RUJUKAN.TANGGAL = string.Format("{0} {1}", txtTglRujukan.SelectedDate.Value.ToString("yyyy-MM-dd"), txtJamRujukan.SelectedDate.Value.ToString("HH:mm:ss"));
            RUJUKAN.FASKES_TUJUAN = cboFaskesTujuan.SelectedValue;
            RUJUKAN.ALASAN = cboAlasanRujukan.SelectedValue;
            RUJUKAN.ALASAN_LAINNYA = txtDiagnosis.Text;
            RUJUKAN.DIAGNOSA = cboKodeIcdX.SelectedValue;

            var DOKTER = new Common.Sisrute.Rujukan.SetRujukan.DOKTER();
            var dpjp = txtDpjp.Text.Split(';');
            DOKTER.NIK = dpjp[0];
            DOKTER.NAMA = dpjp[1];
            RUJUKAN.DOKTER = DOKTER;

            var PETUGAS = new Common.Sisrute.Rujukan.SetRujukan.PETUGAS();
            PETUGAS.NIK = AppSession.UserLogin.UserID;
            PETUGAS.NAMA = AppSession.UserLogin.UserName;
            RUJUKAN.PETUGAS = PETUGAS;

            entity.RUJUKAN = RUJUKAN;

            var KONDISI_UMUM = new Common.Sisrute.Rujukan.SetRujukan.KONDISIUMUM();
            KONDISI_UMUM.ANAMNESIS_DAN_PEMERIKSAAN_FISIK = txtAnamnesis.Text;
            KONDISI_UMUM.KESADARAN = rblKesadaran.SelectedValue;
            KONDISI_UMUM.TEKANAN_DARAH = txtTekananDarah.Text;
            KONDISI_UMUM.FREKUENSI_NADI = txtFrekNadi.Text;
            KONDISI_UMUM.SUHU = txtSuhu.Text;
            KONDISI_UMUM.PERNAPASAN = txtFrekNafas.Text;
            KONDISI_UMUM.KEADAAN_UMUM = txtKeteranganLain.Text;
            KONDISI_UMUM.NYERI = rblNyeri.SelectedValue;
            KONDISI_UMUM.ALERGI = txtRiwayatAlergi.Text;
            entity.KONDISI_UMUM = KONDISI_UMUM;

            var PENUNJANG = new Common.Sisrute.Rujukan.SetRujukan.PENUNJANG();
            PENUNJANG.LABORATORIUM = txtHasilLab.Text;
            PENUNJANG.RADIOLOGI = txtHasilRadiologi.Text;
            PENUNJANG.TERAPI_ATAU_TINDAKAN = txtTerapi.Text;
            entity.PENUNJANG = PENUNJANG;

            return entity;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var svc = new Common.Sisrute.Service();
            var response = svc.SetRujukan(SetEntity(), Common.BPJS.Helper.WebRequestMethod.POST);
            if (response == null)
            {
                args.IsCancel = true;
                args.MessageText = "No response is available.";
            }
            else
            {
                if (!response.Success)
                {
                    args.IsCancel = true;
                    args.MessageText = response.Detail;
                }
                else ViewState["rujukan"] = response.Datum;
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var svc = new Common.Sisrute.Service();
            var response = svc.SetRujukan(SetEntity(), Common.BPJS.Helper.WebRequestMethod.PUT);
            if (response == null)
            {
                args.IsCancel = true;
                args.MessageText = "No response is available.";
            }
            else
            {
                if (!response.Success)
                {
                    args.IsCancel = true;
                    args.MessageText = response.Detail;
                }
                else ViewState["rujukan"] = response.Datum;
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var svc = new Common.Sisrute.Service();
            var response = svc.BatalRujukan(txtNoRujukan.Text, new Common.Sisrute.Rujukan.SetRujukan.PETUGAS() { NIK = AppSession.UserLogin.UserID, NAMA = AppSession.UserLogin.UserName });
            if (!response.Success)
            {
                args.IsCancel = true;
                args.MessageText = response.Detail;
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            if (ViewState["rujukan"] == null)
            {
                var svc = new Common.Sisrute.Service();
                ViewState["rujukan"] = svc.GetRujukanByNo(parameters.Any() ? parameters[0] : txtNoRujukan.Text);
            }
            esEntity entity = null;
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            if (ViewState["rujukan"] == null) return;
            var rsp = ViewState["rujukan"] as Common.Sisrute.Rujukan.Post.Data;
            txtNoRujukan.Text = rsp.RUJUKAN.NOMOR;

            var patient = new PatientQuery();
            patient.Select(patient.MedicalNo, patient.PatientName, "<'' AS RegistrationNo>");
            patient.Where(patient.MedicalNo == rsp.PASIEN.NORM);
            cboNoRM.DataSource = patient.LoadDataTable();
            cboNoRM.DataBind();
            cboNoRM.SelectedValue = rsp.PASIEN.NORM;

            txtNIK.Text = rsp.PASIEN.NIK;
            txtJKN.Text = rsp.PASIEN.NOKARTUJKN;
            txtNamaPasien.Text = rsp.PASIEN.NAMA;
            rblJenisKelamin.SelectedValue = rsp.PASIEN.JENISKELAMIN.KODE;
            txtTglLahir.SelectedDate = Convert.ToDateTime(rsp.PASIEN.TANGGALLAHIR);
            txtTempatLahir.Text = rsp.PASIEN.TEMPATLAHIR;
            txtAlamat.Text = rsp.PASIEN.ALAMAT;
            txtNoKontak.Text = rsp.PASIEN.KONTAK;

            rblJenisRujukan.SelectedValue = rsp.RUJUKAN.JENISRUJUKAN.KODE;
            var tglRujukan = Convert.ToDateTime(rsp.RUJUKAN.TANGGAL);
            txtTglRujukan.SelectedDate = tglRujukan;
            txtJamRujukan.SelectedDate = tglRujukan;

            cboFaskesTujuan_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = string.Empty });
            cboFaskesTujuan.SelectedValue = rsp.RUJUKAN.FASKESTUJUAN.KODE;

            cboAlasanRujukan_ItemsRequested(null, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = string.Empty });
            cboAlasanRujukan.SelectedValue = rsp.RUJUKAN.ALASAN.KODE;

            txtDiagnosis.Text = rsp.RUJUKAN.ALASANLAINNYA;

            cboKodeIcdX.DataSource = WebService.Sisrute.GetDiagnosaIcdX(rsp.RUJUKAN.DIAGNOSA.KODE);
            cboKodeIcdX.DataBind();
            cboKodeIcdX.SelectedValue = rsp.RUJUKAN.DIAGNOSA.KODE;

            txtDpjp.Text = string.Format("{0};{1}", rsp.RUJUKAN.DOKTER.NIK, rsp.RUJUKAN.DOKTER.NAMA);

            txtAnamnesis.Text = rsp.KONDISIUMUM.ANAMNESISDANPEMERIKSAANFISIK;
            rblKesadaran.SelectedValue = rsp.KONDISIUMUM.KESADARAN.KODE;
            txtTekananDarah.Text = rsp.KONDISIUMUM.TEKANANDARAH;
            txtFrekNadi.Text = rsp.KONDISIUMUM.FREKUENSINADI;
            txtSuhu.Text = rsp.KONDISIUMUM.SUHU;
            txtFrekNafas.Text = rsp.KONDISIUMUM.PERNAPASAN;
            txtKeteranganLain.Text = rsp.KONDISIUMUM.KEADAANUMUM;
            rblNyeri.SelectedValue = rsp.KONDISIUMUM.NYERI.KODE;
            txtRiwayatAlergi.Text = rsp.KONDISIUMUM.ALERGI;

            txtHasilLab.Text = rsp.PENUNJANG.LABORATORIUM;
            txtHasilRadiologi.Text = rsp.PENUNJANG.RADIOLOGI;
            txtTerapi.Text = rsp.PENUNJANG.TERAPIATAUTINDAKAN;
        }

        protected override void OnMenuNewClick()
        {
            ViewState["rujukan"] = null;
        }
    }
}
