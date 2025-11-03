using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Bridging.PCare.BusinessObject;
using Temiang.Avicenna.Bridging.PCare.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.PCareCommon
{
    public partial class PCareMember : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ButtonOk != null)
                ButtonOk.Enabled = false;
        }

        #region Attribute Peserta

        public string NoKartu
        {
            get { return txtNoKartu.Text; }
            set { txtNoKartu.Text = value; }
        }

        public string Nama
        {
            get { return txtNama.Text; }
            set
            {
                if (ButtonOk != null)
                    ButtonOk.Enabled = !string.IsNullOrWhiteSpace(value);

                txtNama.Text = value;
            }
        }

        public string HubunganKeluarga
        {
            get { return txtHubunganKeluarga.Text; }
            set { txtHubunganKeluarga.Text = value; }
        }

        public string Sex
        {
            get { return optSexPerempuan.Checked ? "P" : "L"; }
            set
            {
                if (value == "P")
                    optSexPerempuan.Checked = true;
                else
                    optSexLakilaki.Checked = true;
            }
        }

        public string TglLahir
        {
            get { return txtTglLahir.Text; }
            set { txtTglLahir.Text = value; }
        }

        public string TglMulaiAktif
        {
            get { return txtTglMulaiAktif.Text; }
            set { txtTglMulaiAktif.Text = value; }
        }

        public string TglAkhirBerlaku
        {
            get { return txtTglAkhirBerlaku.Text; }
            set { txtTglAkhirBerlaku.Text = value; }
        }

        public string KdProviderPst_kdProvider
        {
            get { return txtKdProviderPst_kdProvider.Text; }
            set { txtKdProviderPst_kdProvider.Text = value; }
        }
        public string KdProviderPst_nmProvider
        {
            get { return txtKdProviderPst_nmProvider.Text; }
            set { txtKdProviderPst_nmProvider.Text = value; }
        }

        public string KdProviderGigi_kdProvider
        {
            get { return txtKdProviderGigi_kdProvider.Text; }
            set { txtKdProviderGigi_kdProvider.Text = value; }
        }

        public string KdProviderGigi_nmProvider
        {
            get { return txtKdProviderGigi_nmProvider.Text; }
            set { txtKdProviderGigi_nmProvider.Text = value; }
        }

        public string JnsKelas_kode
        {
            get { return txtJnsKelas_kode.Text; }
            set { txtJnsKelas_kode.Text = value; }
        }

        public string JnsKelas_nama
        {
            get { return txtJnsKelas_nama.Text; }
            set { txtJnsKelas_nama.Text = value; }
        }

        public string JnsPeserta_kode
        {
            get { return txtJnsPeserta_kode.Text; }
            set { txtJnsPeserta_kode.Text = value; }
        }

        public string JnsPeserta_nama
        {
            get { return txtJnsPeserta_nama.Text; }
            set { txtJnsPeserta_nama.Text = value; }
        }

        public string GolDarah
        {
            get { return txtGolDarah.Text; }
            set { txtGolDarah.Text = value; }
        }

        public string NoHP
        {
            get { return txtNoHP.Text; }
            set { txtNoHP.Text = value; }
        }

        public string NoKTP
        {
            get { return txtNoKTP.Text; }
            set { txtNoKTP.Text = value; }
        }

        public bool Aktif
        {
            get { return chkAktif.Checked; }
            set { chkAktif.Checked = Convert.ToBoolean(value); }
        }

        public string KetAktif
        {
            get { return txtKetAktif.Text; }
            set { txtKetAktif.Text = value; }
        }

        //asuransi_kdAsuransi
        public string Asuransi_kdAsuransi
        {
            get { return txtAsuransi_kdAsuransi.Text; }
            set { txtAsuransi_kdAsuransi.Text = value; }
        }

        //    asuransi_nmAsuransi
        public string Asuransi_nmAsuransi
        {
            get { return txtAsuransi_nmAsuransi.Text; }
            set { txtAsuransi_nmAsuransi.Text = value; }
        }

        //    asuransi_noAsuransi
        public string Asuransi_noAsuransi
        {
            get { return txtAsuransi_noAsuransi.Text; }
            set { txtAsuransi_noAsuransi.Text = value; }
        }

        public string RegistrationNo
        {
            get { return Convert.ToString(ViewState["regno"]); }
            set { ViewState["regno"] = value; }
        }

        public string PatientID
        {
            get { return Convert.ToString(ViewState["patid"]); }
            set { ViewState["patid"] = value; }
        }
        #endregion

        private Button ButtonOk
        {
            get
            {
                var obj = Helper.FindControlRecursive(this.Page.Master, "btnOk");
                if (obj != null)
                    return (Button)obj;
                return null;
            }
        }

        public void Populate(string noKartu, string regNo, string patientID, string nik)
        {
            txtNoKartuSearch.Text = noKartu;
            txtNikSearch.Text = nik;

            NoKartu = noKartu;
            RegistrationNo = regNo;
            PatientID = patientID;

            if (!string.IsNullOrEmpty(PatientID))
            {
                var patient = new Patient();
                patient.LoadByPrimaryKey(PatientID);
                txtMedicalNo.Text = patient.MedicalNo;
                txtPatientName.Text = patient.PatientName;
            }

            if (!string.IsNullOrEmpty(RegistrationNo))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(RegistrationNo);
                if (string.IsNullOrEmpty(PatientID))
                {
                    var patient = new Patient();
                    patient.LoadByPrimaryKey(reg.PatientID);
                    txtMedicalNo.Text = patient.MedicalNo;
                    txtPatientName.Text = patient.PatientName;
                }
            }

            var peserta = new BpjsPeserta();
            peserta.LoadByPrimaryKey(noKartu);
            Populate(peserta);

        }

        private void Populate(BpjsPeserta peserta)
        {
            NoKartu = peserta.NoKartu;
            Nama = peserta.Nama;
            HubunganKeluarga = peserta.HubunganKeluarga;
            Sex = peserta.Sex;
            TglLahir = Convert.ToDateTime(peserta.TglLahir).ToString(AppConstant.DisplayFormat.Date);

            TglMulaiAktif = string.Empty;
            if (peserta.TglMulaiAktif != null)
                TglMulaiAktif = Convert.ToDateTime(peserta.TglMulaiAktif).ToString(AppConstant.DisplayFormat.Date);

            TglAkhirBerlaku = string.Empty;
            if (peserta.TglAkhirBerlaku != null)
                TglAkhirBerlaku = Convert.ToDateTime(peserta.TglAkhirBerlaku).ToString(AppConstant.DisplayFormat.Date);

            KdProviderPst_kdProvider = peserta.KdProviderPst_kdProvider;
            KdProviderPst_nmProvider = peserta.KdProviderPst_nmProvider;
            KdProviderGigi_kdProvider = peserta.KdProviderGigi_kdProvider;
            KdProviderGigi_nmProvider = peserta.KdProviderGigi_nmProvider;
            JnsKelas_kode = peserta.JnsKelas_kode;
            JnsKelas_nama = peserta.JnsKelas_nama;
            JnsPeserta_kode = peserta.JnsPeserta_kode;
            JnsPeserta_nama = peserta.JnsPeserta_nama;
            GolDarah = peserta.GolDarah;
            NoHP = peserta.NoHP;
            NoKTP = peserta.NoKTP;
            Aktif = peserta.Aktif ?? false;
            KetAktif = peserta.KetAktif;
            Asuransi_kdAsuransi = peserta.Asuransi_kdAsuransi;
            Asuransi_nmAsuransi = peserta.Asuransi_nmAsuransi;
            Asuransi_noAsuransi = peserta.Asuransi_noAsuransi;
        }


        //protected void btnUpdateBpjsInfo_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var peserta = new Bridging.PCare.BusinessObject.Peserta();
        //        var result = peserta.SaveToLocalDataBase(txtNoKartu.Text);
        //        if (result.IsOk) //OK
        //        {
        //            // Save GuarantorNo
        //            if (!string.IsNullOrEmpty(PatientID))
        //            {
        //                var patient = new Patient();
        //                if (patient.LoadByPrimaryKey(PatientID))
        //                {
        //                    patient.GuarantorCardNo = txtNoKartu.Text;
        //                    patient.Save();
        //                }
        //            }

        //            if (!string.IsNullOrEmpty(RegistrationNo))
        //            {
        //                var reg = new Registration();
        //                if (reg.LoadByPrimaryKey(RegistrationNo))
        //                {
        //                    if (string.IsNullOrEmpty(PatientID))
        //                    {
        //                        var patient = new Patient();
        //                        patient.LoadByPrimaryKey(reg.PatientID);
        //                        patient.GuarantorCardNo = txtNoKartu.Text;
        //                        patient.Save();
        //                    }
        //                }
        //            }

        //            //Populate display
        //            var ent = new BpjsPeserta();
        //            ent.LoadByPrimaryKey(txtNoKartu.Text);
        //            Populate(ent);
        //            this.ShowMessageAfterPostback(btnUpdateBpjsInfo.Text + " has success.");
        //        }
        //        else
        //        {
        //            var msg = string.Format("{0} has failed.\\n{1}", btnUpdateBpjsInfo.Text,
        //                result.MetaData.MessageDescription);
        //            this.ShowMessageAfterPostback(msg);

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = string.Format("{0} has failed.\\nBridging problem : {1}", btnUpdateBpjsInfo.Text, ex.Message);
        //        this.ShowMessageAfterPostback(msg);
        //    }

        //}

        protected void btnNikSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                var peserta = new Bridging.PCare.BusinessObject.Peserta();
                var result = peserta.SaveToLocalDataBase(txtNikSearch.Text, "nik");
                if (result.IsOk) //OK
                {
                    //Populate display
                    var ent = new BpjsPeserta();
                    ent.LoadByPrimaryKey(result.Response.NoKartu);
                    Populate(ent);
                    this.ShowMessageAfterPostback("Finish");
                }
                else
                {
                    var msg = string.Format("Validate NIK has failed.\\n{0}",
                        result.MetaData.MessageDescription);
                    this.ShowMessageAfterPostback(msg);
                }

            }
            catch (Exception ex)
            {
                var msg = string.Format("Validate NIK has failed.\\nBridging problem : {0}", ex.Message);
                this.ShowMessageAfterPostback(msg);
            }

        }

        protected void btnNoKartuSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                var peserta = new Bridging.PCare.BusinessObject.Peserta();
                var result = peserta.SaveToLocalDataBase(txtNoKartuSearch.Text, "bpjsno");
                if (result.IsOk) //OK
                {
                    //Populate display
                    var ent = new BpjsPeserta();
                    ent.LoadByPrimaryKey(result.Response.NoKartu);
                    Populate(ent);
                    this.ShowMessageAfterPostback("Finish");
                }
                else
                {
                    var msg = string.Format("Validate BPJS No has failed.\\n{0}",
                        result.MetaData.MessageDescription);
                    this.ShowMessageAfterPostback(msg);
                }

            }
            catch (Exception ex)
            {
                var msg = string.Format("Validate  BPJS No has failed.\\nBridging problem : {0}", ex.Message);
                this.ShowMessageAfterPostback(msg);
            }

        }
    }
}