using System;
using System.Linq;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class BpjsSepDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "BpjsSepSearch.aspx";
            UrlPageList = "BpjsSepList.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.BpjsSep;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboJenisPelayanan, AppEnum.StandardReference.BpjsTypeOfService);

                var service = new Common.BPJS.VClaim.v11.Service();
                var type = service.GetKelasRawat();
                if (type.MetaData.IsValid)
                {
                    cboKelasRawat.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (var str in type.Response.List)
                    {
                        cboKelasRawat.Items.Add(new RadComboBoxItem(str.Nama, str.Kode));
                    }
                }

                cboPoliTujuan.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                var alias = new ServiceUnitBridgingCollection();
                alias.Query.Where(alias.Query.SRBridgingType == AppEnum.BridgingType.BPJS.ToString(), alias.Query.IsActive == true);
                alias.Query.Load();
                foreach (var entity in alias)
                {
                    cboPoliTujuan.Items.Add(new RadComboBoxItem(entity.BridgingName, entity.BridgingID));
                }

                //var svc = new Common.BPJS.v21.Service();
                //var poli = svc.GetPoli();
                //if (poli.Metadata.IsValid)
                //{
                //    var resp = poli.Response;
                //    foreach (var list in resp.List)
                //    {
                //        cboPoliTujuan.Items.Add(new RadComboBoxItem(string.Format("{0} - {1}", list.KdPoli, list.NmPoli), list.KdPoli));
                //    }
                //}
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = GetRiwayatTable;
        }

        protected void cboMR_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var patient = new Patient();
            patient.Query.es.Top = 1;
            patient.Query.Where(patient.Query.MedicalNo == e.Value);
            if (patient.Query.Load()) txtNoPatientID.Text = patient.PatientID;
        }

        protected void cboJenisPelayanan_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //rawat jalan disabled dan set kelas 3
            if (e.Value == "2")
            {
                cboKelasRawat.SelectedValue = "3";
                cboKelasRawat.Enabled = false;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNomorKartu.Text)) return;

            var table = GetRiwayatTable;
            table.Rows.Clear();

            //var service = new Common.BPJS.v21.Service();

            //detail
            //var resp = service.GetPeserta(txtNomorKartu.Text, true);
            //if (!resp.Metadata.IsValid)
            //{
            //    service = new Common.BPJS.v21.Service();
            //    resp = service.GetPeserta(txtNomorKartu.Text, false);
            //    if (!resp.Metadata.IsValid)
            //    {
            //        txtPatientDetail.Text = string.Format("{0} - {1}", resp.Metadata.Code, resp.Metadata.Message);
            //        ViewState["peserta"] = null;
            //        return;
            //    }
            //}

            //var peserta = resp.Response.Peserta;

            //txtNomorKartu.Text = peserta.NoKartu;

            //var sb = new StringBuilder();
            //sb.AppendLine(string.Format("Nama : {0}", peserta.Nama));
            //sb.AppendLine(string.Format("NIK : {0}", peserta.Nik));
            //sb.AppendLine(string.Format("No Kartu : {0}", peserta.NoKartu));
            //sb.AppendLine(string.Format("No MR : {0}", peserta.NoMr));
            //sb.AppendLine(string.Format("Jenis Kelamin : {0}", peserta.Sex));
            //sb.AppendLine(string.Format("Tanggal Lahir : {0}", Convert.ToDateTime(peserta.TglLahir).ToString("dd-MM-yyyy")));
            //sb.AppendLine(string.Format("Usia : {0}", peserta.Umur.UmurSekarang));
            //sb.AppendLine(string.Format("Jenis Peserta : {0}", peserta.JenisPeserta.NmJenisPeserta));
            //sb.AppendLine(string.Format("Kelas Tanggungan : {0}", peserta.KelasTanggungan.NmKelas));
            //sb.AppendLine(string.Format("Status : {0}", peserta.StatusPeserta.Keterangan));

            //ViewState["peserta"] = string.Format("{0}#{1}#{2}#{3}#{4}", peserta.Nama, peserta.Nik, peserta.Sex, peserta.TglLahir, peserta.JenisPeserta.NmJenisPeserta);

            //txtPPKRujukan.Text = peserta.ProvUmum.KdProvider;
            //lblNamaPPKRujukan.Text = peserta.ProvUmum.NmProvider;

            ////riwayat
            //service = new Common.BPJS.v21.Service();
            //var riwayat = service.GetRiwayat(txtNomorKartu.Text);
            //if (riwayat.Metadata.IsValid)
            //{
            //    if (riwayat.Response.Count.ToInt() == 0) return;

            //    foreach (var entity in riwayat.Response.List)
            //    {
            //        var row = table.NewRow();

            //        row[0] = entity.NoSEP;
            //        row[1] = Convert.ToDateTime(entity.TglSEP);
            //        row[2] = entity.JnsPelayanan;
            //        row[3] = entity.PoliTujuan.KdPoli + " - " + entity.PoliTujuan.NmPoli;
            //        row[4] = entity.Diagnosa.KodeDiagnosa + " - " + entity.Diagnosa.NamaDiagnosa;
            //        row[5] = Convert.ToDecimal(entity.BiayaTagihan);
            //        try
            //        {
            //            DateTime? date = null;
            //            row[6] = string.IsNullOrEmpty(entity.TglPulang) ? date : Convert.ToDateTime(entity.TglPulang);
            //        }
            //        catch { }
            //        table.Rows.Add(row);
            //    }

            //    //var last = table.AsEnumerable().OrderByDescending(t => t.Field<DateTime>("tglSEP")).Take(1).SingleOrDefault();
            //    //if (last != null)
            //    //{
            //    //    sb.AppendLine("------------------------------");
            //    //    sb.AppendLine(string.Format("Tanggal Kunjungan Terakhir : {0}", Convert.ToDateTime(last["tglSEP"]).ToString("dd-MM-yyyy")));
            //    //    sb.AppendLine(string.Format("Diagnosa Terakhir : {0}", last["diagnosa"].ToString()));

            //    //    if (last["tglPulang"] == null)
            //    //        ShowInformationHeader(string.Format("Pasien belum checkout dari kunjungan (SEP : {0}).", last["noSEP"].ToString()));
            //    //    else HideInformationHeader();
            //    //}

            //    grdList.DataSource = table;
            //    grdList.DataBind();
            //}

            //txtPatientDetail.Text = sb.ToString();

            //cboKelasRawat.SelectedValue = peserta.KelasTanggungan.KdKelas;

            //service = new Temiang.Avicenna.Common.BPJS.v20.Service();
            //var rujukan = service.GetRefferalByMemberNo(txtNomorKartu.Text);
            //if (rujukan.Metadata.IsValid)
            //{
            //    var item = rujukan.Response.Item;

            //    txtNoRujukan.Text = item.NoKunjungan;
            //    txtTanggalRujukan.SelectedDate = DateTime.Parse(item.TglKunjungan);
            //    txtPPKRujukan.Text = item.ProvKunjungan.KdProvider;
            //    lblNamaPPKRujukan.Text = item.ProvKunjungan.NmProvider;
            //    txtCatatan.Text = item.Catatan;
            //    if (!string.IsNullOrEmpty(item.Diagnosa.KdDiag))
            //    {
            //        var diag = new DiagnoseQuery();
            //        diag.Select(diag.DiagnoseID.As("KodeDiagnosa"), diag.DiagnoseName.As("NamaDiagnosa"));
            //        diag.Where(diag.DiagnoseID == item.Diagnosa.KdDiag);

            //        cboDiagnosaAwal.DataSource = diag.LoadDataTable();
            //        cboDiagnosaAwal.DataBind();
            //        try
            //        {
            //            cboDiagnosaAwal.SelectedValue = item.Diagnosa.KdDiag;
            //        }
            //        catch { }
            //    }
            //    cboPoliTujuan.SelectedValue = item.PoliRujukan.KdPoli;
            //}

            //if (!string.IsNullOrEmpty(peserta.NoMr))
            //{
            //    var dtbPatient = (new PatientCollection()).PatientRegisterAble(peserta.NoMr, string.Empty, string.Empty, string.Empty, 1);
            //    if (dtbPatient.Rows.Count > 0)
            //    {
            //        cboMR.DataSource = dtbPatient;
            //        cboMR.DataBind();
            //        try
            //        {
            //            cboMR.SelectedValue = dtbPatient.Rows[0]["MedicalNo"].ToString();
            //            txtNoPatientID.Text = dtbPatient.Rows[0]["PatientID"].ToString();
            //        }
            //        catch
            //        { }
            //    }
            //}
            //else
            //{
            //    cboMR.DataSource = null;
            //    cboMR.DataBind();
            //    cboMR.Items.Clear();
            //    cboMR.SelectedValue = string.Empty;
            //    cboMR.Text = string.Empty;
            //}
        }

        private DataTable GetRiwayatTable
        {
            get
            {
                if (ViewState["riwayatTable"] != null) return ViewState["riwayatTable"] as DataTable;

                var table = new DataTable();
                table.Columns.Add("noSEP", typeof(string));
                table.Columns.Add("tglSEP", typeof(DateTime));
                table.Columns.Add("jnsPelayanan", typeof(string));
                table.Columns.Add("poliTujuan", typeof(string));
                table.Columns.Add("diagnosa", typeof(string));
                table.Columns.Add("biayaTagihan", typeof(decimal));
                table.Columns.Add("tglPulang", typeof(DateTime));

                ViewState["riwayatTable"] = table;

                return GetRiwayatTable;
            }
            set
            { ViewState["riwayatTable"] = value; }
        }

        protected void cboDiagnosaAwal_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((Common.BPJS.VClaim.Referensi.Diagnosa.Data)e.Item.DataItem).Nama;
            e.Item.Value = ((Common.BPJS.VClaim.Referensi.Diagnosa.Data)e.Item.DataItem).Kode;
        }

        protected void cboDiagnosaAwal_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text.Length < 3)
            {
                cboDiagnosaAwal.DataSource = null;
                cboDiagnosaAwal.DataBind();
                cboDiagnosaAwal.Items.Clear();
                cboDiagnosaAwal.SelectedValue = string.Empty;
                return;
            }
            cboDiagnosaAwal.DataSource = null;
            cboDiagnosaAwal.DataBind();
            cboDiagnosaAwal.Items.Clear();
            cboDiagnosaAwal.SelectedValue = string.Empty;

            var svc = new Common.BPJS.VClaim.v11.Service();
            var diagnosa = svc.GetDiagnosa(e.Text);
            if (diagnosa.MetaData.IsValid)
            {
                var resp = diagnosa.Response.Diagnosa;

                //if (Diagnose.Rows.Count > 1) Diagnose.Rows.Clear();
                //foreach (var list in resp.List)
                //{
                //    var row = Diagnose.NewRow();
                //    row["KodeDiagnosa"] = list.KodeDiagnosa;
                //    row["NamaDiagnosa"] = list.NamaDiagnosa;
                //    Diagnose.Rows.Add(row);
                //}

                cboDiagnosaAwal.DataSource = resp;
                cboDiagnosaAwal.DataBind();
            }

            //var diag = new DiagnoseQuery();
            //diag.es.Top = 50;
            //diag.Select(diag.DiagnoseID.As("KodeDiagnosa"), diag.DiagnoseName.As("NamaDiagnosa"));
            //diag.Where(diag.Or(diag.DiagnoseID.Like("%" + e.Text + "%"), diag.DiagnoseName.Like("%" + e.Text + "%")));
            //cboDiagnosaAwal.DataSource = diag.LoadDataTable();
            //cboDiagnosaAwal.DataBind();
        }

        protected void cboMR_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["MedicalNo"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["PatientName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["MedicalNo"].ToString();
        }

        protected void cboMR_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Text)) return;

            var dtbPatient = (new PatientCollection()).PatientRegisterAble(e.Text, string.Empty, string.Empty, string.Empty, 5);
            cboMR.DataSource = dtbPatient;
            cboMR.DataBind();
        }

        private DataTable Diagnose
        {
            get
            {
                if (ViewState["tbl_diagnose"] != null) return (DataTable)ViewState["tbl_diagnose"];

                var table = new DataTable();
                table.Columns.Add("KodeDiagnosa", typeof(string));
                table.Columns.Add("NamaDiagnosa", typeof(string));

                ViewState["tbl_diagnose"] = table;
                return table;
            }
            set
            {
                ViewState["tbl_diagnose"] = value;
            }
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new BpjsSEP());

            txtNoSEP.Text = string.Empty;
            txtNomorKartu.Text = string.Empty;
            txtPatientDetail.Text = string.Empty;
            txtTanggalSEP.SelectedDate = DateTime.Now.Date;
            txtTanggalRujukan.SelectedDate = DateTime.Now.Date;
            txtNoRujukan.Text = string.Empty;
            txtPPKRujukan.Text = string.Empty;
            cboJenisPelayanan.SelectedValue = string.Empty;
            txtCatatan.Text = string.Empty;

            cboDiagnosaAwal.DataSource = null;
            cboDiagnosaAwal.DataBind();
            cboDiagnosaAwal.Items.Clear();
            cboDiagnosaAwal.SelectedValue = string.Empty;
            cboDiagnosaAwal.Text = string.Empty;

            cboPoliTujuan.SelectedValue = string.Empty;
            cboKelasRawat.SelectedValue = string.Empty;
            chkIsLakaLantas.Checked = false;

            cboMR.DataSource = null;
            cboMR.DataBind();
            cboMR.Items.Clear();
            cboMR.SelectedValue = string.Empty;
            cboMR.Text = string.Empty;

            grdList.DataSource = null;
            grdList.DataBind();

            txtNoPatientID.Text = string.Empty;
            txtRegistrationNo.Text = string.Empty;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var reg = new Registration();
            reg.Query.es.Top = 1;
            reg.Query.Where(reg.Query.BpjsSepNo == txtNoSEP.Text, reg.Query.IsVoid == false);
            if (reg.Query.Load())
            {
                args.MessageText = "No SEP telah di mapping ke registrasi kunjungan pasien.";
                args.IsCancel = true;

                return;
            }

            var entity = new BpjsSEP();
            if (entity.LoadByPrimaryKey(txtNoSEP.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity, args);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }

            //if (!string.IsNullOrEmpty(args.MessageText)) return;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new BpjsSEP();
            if (entity.LoadByPrimaryKey(txtNoSEP.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new BpjsSEP();

            SetEntityValue(entity);
            SaveEntity(entity, args);

            //var bpjs = new Common.BPJS.v21.Service();
            ////if (entity.es.IsAdded)
            ////{
            ////Temiang.Avicenna.Common.BPJS.v21.Sep.Response.Sep sepResponse;
            //var response = bpjs.Insert(new Common.BPJS.v21.Sep.Insert.TSep
            //{
            //    noKartu = entity.NomorKartu,
            //    tglSep = entity.TanggalSEP.Value.ToString("yyyy-MM-dd HH:mm:ss"),
            //    tglRujukan = entity.TanggalRujukan.Value.ToString("yyyy-MM-dd HH:mm:ss"),
            //    noRujukan = entity.NoRujukan,
            //    ppkRujukan = entity.PPKRujukan,
            //    ppkPelayanan = entity.PPKPelayanan,
            //    jnsPelayanan = entity.JenisPelayanan,
            //    catatan = entity.Catatan,
            //    diagAwal = entity.DiagnosaAwal,
            //    poliTujuan = entity.PoliTujuan,
            //    klsRawat = entity.KelasRawat,
            //    lakaLantas = entity.LakaLantas,
            //    lokasiLaka = entity.LokasiLaka,
            //    user = entity.User,
            //    noMr = entity.NoMR
            //});
            //if (response.Metadata.IsValid)
            //{
            //    entity.NoSEP = response.Response;
            //    txtNoSEP.Text = entity.NoSEP;

            //    using (var trans = new esTransactionScope())
            //    {
            //        //if (entity.es.IsAdded || entity.es.IsModified)
            //        //{
            //        var patient = new Patient();
            //        if (patient.LoadByPrimaryKey(entity.PatientID))
            //        {
            //            patient.GuarantorCardNo = entity.NomorKartu;
            //            patient.Save();
            //        }
            //        //}

            //        entity.Save();

            //        //Commit if success, Rollback if failed
            //        trans.Complete();
            //    }
            //}
            //else
            //{
            //    args.MessageText = String.Format("Server error (HTTP {0}: {1}).", "800", "test");
            //    args.IsCancel = true;
            //    //return;
            //}
            ////}


            //if (!string.IsNullOrEmpty(args.MessageText)) return;
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new BpjsSEP();
            if (entity.LoadByPrimaryKey(txtNoSEP.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity, args);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }

            //if (!string.IsNullOrEmpty(args.MessageText)) return;
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("BpjsSEP='{0}'", txtNoSEP.Text);
            auditLogFilter.TableName = "BpjsSEP";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new BpjsSEP();
            if (parameters.Length > 0)
            {
                String sepId = (String)parameters[0];
                if (!parameters[0].Equals(string.Empty)) entity.LoadByPrimaryKey(sepId);
            }
            else
            {
                entity.LoadByPrimaryKey(txtNoSEP.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var bs = (BpjsSEP)entity;

            txtNoSEP.Text = bs.NoSEP;

            if (string.IsNullOrEmpty(txtNoSEP.Text)) return;

            txtNomorKartu.Text = bs.NomorKartu;

            if (!string.IsNullOrEmpty(bs.DetailKeanggotaan)) txtPatientDetail.Text = bs.DetailKeanggotaan;
            else
            {
                var sb = new StringBuilder();
                sb.AppendLine(string.Format("Nama : {0}", bs.NamaPasien));
                sb.AppendLine(string.Format("NIK : {0}", bs.Nik));
                sb.AppendLine(string.Format("No Kartu : {0}", bs.NomorKartu));
                sb.AppendLine(string.Format("No MR : {0}", string.IsNullOrEmpty(bs.NoMR) ? string.Empty : bs.NoMR));
                sb.AppendLine(string.Format("Jenis Kelamin : {0}", bs.JenisKelamin));
                sb.AppendLine(string.Format("Tanggal Lahir : {0}", Convert.ToDateTime(bs.TanggalLahir).ToString("dd-MM-yyyy")));
                sb.AppendLine(string.Format("Usia : {0}",
                    string.Format("{0} tahun ,{1} bulan ,{2} hari", Helper.GetAgeInYear(bs.TanggalLahir.Value.Date, bs.TanggalSEP.Value.Date).ToString(),
                    Helper.GetAgeInMonth(bs.TanggalLahir.Value.Date, bs.TanggalSEP.Value.Date).ToString(),
                    Helper.GetAgeInDay(bs.TanggalLahir.Value.Date, bs.TanggalSEP.Value.Date).ToString())
                    ));
                sb.AppendLine(string.Format("Jenis Peserta : {0}", bs.JenisPeserta));
                sb.AppendLine(string.Format("Kelas Tanggungan : {0}", bs.KelasRawat));
                sb.AppendLine(string.Format("Status : {0}", "AKTIF"));
                txtPatientDetail.Text = sb.ToString();
            }

            txtTanggalSEP.SelectedDate = bs.TanggalSEP;
            txtTanggalRujukan.SelectedDate = bs.TanggalRujukan;
            txtNoRujukan.Text = bs.NoRujukan;
            txtPPKRujukan.Text = bs.PPKRujukan == ConfigurationManager.AppSettings["BPJSHospitalID"] ? string.Empty : bs.PPKRujukan;
            lblNamaPPKRujukan.Text = bs.NamaPPKRujukan;
            cboJenisPelayanan.SelectedValue = bs.JenisPelayanan;
            txtCatatan.Text = bs.Catatan;

            //var bpjs = new Common.BPJS.v21.Service();
            //Common.BPJS.v21.Diagnosa.Diagnosa response = bpjs.GetDiagnosa(bs.DiagnosaAwal);
            //if (response.Metadata.IsValid)
            //{
            //    Common.BPJS.v21.Diagnosa.List[] list = response.Response.List;

            //    if (Diagnose.Rows.Count > 1) Diagnose.Rows.Clear();
            //    foreach (var l in list)
            //    {
            //        var row = Diagnose.NewRow();
            //        row["KodeDiagnosa"] = l.KodeDiagnosa;
            //        row["NamaDiagnosa"] = l.NamaDiagnosa;
            //        Diagnose.Rows.Add(row);
            //    }

            //    //var diag = new DiagnoseQuery();
            //    //diag.Select(diag.DiagnoseID.As("KodeDiagnosa"), diag.DiagnoseName.As("NamaDiagnosa"));
            //    //diag.Where(diag.DiagnoseID == bs.DiagnosaAwal);

            //    cboDiagnosaAwal.DataSource = Diagnose;
            //    cboDiagnosaAwal.DataBind();
            //}
            cboDiagnosaAwal.SelectedValue = bs.DiagnosaAwal;

            cboPoliTujuan.SelectedValue = bs.PoliTujuan;
            cboKelasRawat.SelectedValue = bs.KelasRawat;
            chkIsLakaLantas.Checked = bs.LakaLantas == "1";
            txtLokasiLaka.Text = bs.LokasiLaka;

            if (!string.IsNullOrEmpty(bs.NoMR))
            {
                var dtbPatient = (new PatientCollection()).PatientRegisterAble(bs.NoMR, string.Empty, string.Empty, string.Empty, 1);
                cboMR.DataSource = dtbPatient;
                cboMR.DataBind();
                try
                {
                    cboMR.SelectedValue = dtbPatient.Rows[0]["MedicalNo"].ToString();
                }
                catch
                { }
            }
            txtNoPatientID.Text = bs.PatientID;

            var reg = new Registration();
            reg.Query.Where(reg.Query.BpjsSepNo == txtNoSEP.Text, reg.Query.IsVoid == false);
            if (reg.Query.Load()) txtRegistrationNo.Text = reg.RegistrationNo;
            else txtRegistrationNo.Text = string.Empty;
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_NoSep", txtNoSEP.Text);
        }

        private void SetEntityValue(BpjsSEP bs)
        {
            bs.NoSEP = txtNoSEP.Text;
            bs.NomorKartu = txtNomorKartu.Text;
            bs.TanggalSEP = txtTanggalSEP.SelectedDate;
            bs.TanggalRujukan = txtTanggalRujukan.IsEmpty ? txtTanggalSEP.SelectedDate : txtTanggalRujukan.SelectedDate;
            bs.NoRujukan = txtNoRujukan.Text;
            bs.PPKRujukan = txtPPKRujukan.Text;
            bs.NamaPPKRujukan = lblNamaPPKRujukan.Text;
            bs.PPKPelayanan = ConfigurationManager.AppSettings["BPJSHospitalID"];
            bs.JenisPelayanan = cboJenisPelayanan.SelectedValue;
            bs.Catatan = txtCatatan.Text;
            bs.DiagnosaAwal = cboDiagnosaAwal.SelectedValue;
            bs.PoliTujuan = cboPoliTujuan.SelectedValue;
            bs.KelasRawat = cboKelasRawat.SelectedValue;
            bs.LakaLantas = chkIsLakaLantas.Checked ? "1" : "2";
            bs.User = AppSession.UserLogin.UserID;
            bs.NoMR = string.IsNullOrEmpty(cboMR.SelectedValue) ? "-" : cboMR.SelectedValue.Replace("-", "");

            string[] peserta = ViewState["peserta"].ToString().Split('#');
            bs.NamaPasien = peserta[0];
            bs.Nik = peserta[1];
            bs.JenisKelamin = peserta[2];
            bs.TanggalLahir = DateTime.Parse(peserta[3]);
            bs.JenisPeserta = peserta[4];

            bs.DetailKeanggotaan = txtPatientDetail.Text;
            bs.PatientID = txtNoPatientID.Text;
            bs.KodeCBG = string.Empty;
            bs.TariffCBG = 0;
            bs.LokasiLaka = txtLokasiLaka.Text;
            bs.NamaKelasRawat = cboKelasRawat.Text;
        }

        private void SaveEntity(BpjsSEP entity, ValidateArgs args)
        {
            //var bpjs = new Common.BPJS.v21.Service();
            //if (entity.es.IsAdded)
            //{
            //    var response = bpjs.Insert(new Common.BPJS.v21.Sep.Insert.TSep
            //    {
            //        noKartu = entity.NomorKartu,
            //        tglSep = entity.TanggalSEP.Value.ToString("yyyy-MM-dd HH:mm:ss"),
            //        tglRujukan = entity.TanggalRujukan.Value.ToString("yyyy-MM-dd HH:mm:ss"),
            //        noRujukan = entity.NoRujukan,
            //        ppkRujukan = entity.PPKRujukan,
            //        ppkPelayanan = entity.PPKPelayanan,
            //        jnsPelayanan = entity.JenisPelayanan,
            //        catatan = entity.Catatan,
            //        diagAwal = entity.DiagnosaAwal,
            //        poliTujuan = entity.PoliTujuan,
            //        klsRawat = entity.KelasRawat,
            //        lakaLantas = entity.LakaLantas,
            //        lokasiLaka = entity.LokasiLaka,
            //        user = entity.User,
            //        noMr = entity.NoMR
            //    });
            //    if (response.Metadata.IsValid)
            //    {
            //        entity.NoSEP = response.Response;
            //        txtNoSEP.Text = entity.NoSEP;
            //    }
            //    else
            //    {
            //        args.MessageText = String.Format("Server error (HTTP {0}: {1}).", response.Metadata.Code, response.Metadata.Message);
            //        args.IsCancel = true;
            //        return;
            //    }
            //}
            //else if (entity.es.IsModified)
            //{
            //    var response = bpjs.Update(new Common.BPJS.v21.Sep.Update.TSep
            //    {
            //        noSep = entity.NoSEP,
            //        noKartu = entity.NomorKartu,
            //        tglSep = entity.TanggalSEP.Value.ToString("yyyy-MM-dd HH:mm:ss"),
            //        tglRujukan = entity.TanggalRujukan.Value.ToString("yyyy-MM-dd HH:mm:ss"),
            //        noRujukan = entity.NoRujukan,
            //        ppkRujukan = entity.PPKRujukan,
            //        ppkPelayanan = entity.PPKPelayanan,
            //        jnsPelayanan = entity.JenisPelayanan,
            //        catatan = entity.Catatan,
            //        diagAwal = entity.DiagnosaAwal,
            //        poliTujuan = entity.PoliTujuan,
            //        klsRawat = entity.KelasRawat,
            //        lakaLantas = entity.LakaLantas,
            //        lokasiLaka = entity.LokasiLaka,
            //        user = entity.User,
            //        noMr = entity.NoMR
            //    });
            //    if (!response.Metadata.IsValid)
            //    {
            //        args.MessageText = String.Format("Server error (HTTP {0}: {1}).", response.Metadata.Code, response.Metadata.Message);
            //        args.IsCancel = true;
            //        return;
            //    }
            //}
            //else if (entity.es.IsDeleted)
            //{
            //    var response = bpjs.Delete(new Common.BPJS.v21.Sep.Delete.TSep { noSep = entity.NoSEP, ppkPelayanan = entity.PPKPelayanan });
            //    if (!response.Metadata.IsValid)
            //    {
            //        args.MessageText = String.Format("Server error (HTTP {0}: {1}).", response.Metadata.Code, response.Metadata.Message);
            //        args.IsCancel = true;
            //        return;
            //    }
            //}

            //using (var trans = new esTransactionScope())
            //{
            //    if (entity.es.IsAdded || entity.es.IsModified)
            //    {
            //        var patient = new Patient();
            //        patient.LoadByPrimaryKey(entity.PatientID);
            //        patient.GuarantorCardNo = entity.NomorKartu;
            //        patient.Save();
            //    }

            //    entity.Save();

            //    //Commit if success, Rollback if failed
            //    trans.Complete();
            //}
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new BpjsSEPQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.NoSEP > txtNoSEP.Text);
                que.OrderBy(que.NoSEP.Ascending);
            }
            else
            {
                que.Where(que.NoSEP < txtNoSEP.Text);
                que.OrderBy(que.NoSEP.Descending);
            }
            var entity = new BpjsSEP();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (sourceControl is RadGrid)
            {
                var str = eventArgument.Split('!');
                if (str.Length > 0)
                {
                    if (str[1] == "rujukan")
                    {
                        var split = str[2].Split('|');
                        txtNoRujukan.Text = split[0];
                        txtTanggalRujukan.SelectedDate = DateTime.Parse(split[1]);
                        txtPPKRujukan.Text = split[2];
                        lblNamaPPKRujukan.Text = split[3];
                        txtCatatan.Text = split[4];
                        if (!string.IsNullOrEmpty(split[5]))
                        {
                            //var bpjs = new Common.BPJS.v21.Service();
                            //var response = bpjs.GetDiagnosa(split[5]);
                            //if (response.Metadata.IsValid)
                            //{
                            //    if (Diagnose.Rows.Count > 1) Diagnose.Rows.Clear();
                            //    foreach (var list in response.Response.List)
                            //    {
                            //        var row = Diagnose.NewRow();
                            //        row["KodeDiagnosa"] = list.KodeDiagnosa;
                            //        row["NamaDiagnosa"] = list.NamaDiagnosa;
                            //        Diagnose.Rows.Add(row);
                            //    }

                            //    //var diag = new DiagnoseQuery();
                            //    //diag.Select(diag.DiagnoseID.As("KodeDiagnosa"), diag.DiagnoseName.As("NamaDiagnosa"));
                            //    //diag.Where(diag.DiagnoseID == split[5]);

                            //    //cboDiagnosaAwal.DataSource = diag.LoadDataTable();
                            //    //cboDiagnosaAwal.DataBind();
                            //    //try
                            //    //{
                            //    //    cboDiagnosaAwal.SelectedValue = split[5];
                            //    //}
                            //    //catch { }
                            //    //}
                            //}
                            //cboPoliTujuan.SelectedValue = split[6];
                        }
                    }
                    else if (str[1] == "pasien")
                    {
                        cboMR.DataSource = null;
                        cboMR.DataBind();
                        cboMR.Items.Clear();
                        cboMR.SelectedValue = string.Empty;
                        cboMR.Text = string.Empty;

                        var patient = new PatientQuery();
                        patient.Select(
                            patient.PatientID,
                            patient.MedicalNo,
                            patient.PatientName
                            );
                        patient.Where(patient.PatientID == str[2]);
                        var tbl = patient.LoadDataTable();
                        if (tbl.Rows.Count > 0)
                        {
                            cboMR.DataSource = tbl;
                            cboMR.DataBind();
                            try
                            {
                                cboMR.SelectedValue = tbl.Rows[0]["MedicalNo"].ToString();
                                txtNoPatientID.Text = tbl.Rows[0]["PatientID"].ToString();
                            }
                            catch
                            { }
                        }
                    }
                    else if (str[1] == "checkout")
                    {
                        //var service = new Common.BPJS.v21.Service();
                        //var riwayat = service.GetRiwayat(txtNomorKartu.Text);
                        //if (riwayat.Metadata.IsValid)
                        //{
                        //    var table = GetRiwayatTable;
                        //    table.Rows.Clear();

                        //    if (riwayat.Response.Count.ToInt() == 0) return;

                        //    foreach (var entity in riwayat.Response.List)
                        //    {
                        //        var row = table.NewRow();

                        //        row[0] = entity.NoSEP;
                        //        row[1] = Convert.ToDateTime(entity.TglSEP);
                        //        row[2] = entity.JnsPelayanan;
                        //        row[3] = entity.PoliTujuan.KdPoli + " - " + entity.PoliTujuan.NmPoli;
                        //        row[4] = entity.Diagnosa.KodeDiagnosa + " - " + entity.Diagnosa.NamaDiagnosa;
                        //        row[5] = Convert.ToDecimal(entity.BiayaTagihan);
                        //        DateTime? date = null;
                        //        row[6] = string.IsNullOrEmpty(entity.TglPulang) ? date : Convert.ToDateTime(entity.TglPulang);

                        //        table.Rows.Add(row);
                        //    }

                        //    var last = table.AsEnumerable().OrderByDescending(t => t.Field<DateTime>("tglSEP")).Take(1).SingleOrDefault();
                        //    if (last != null)
                        //    {
                        //        if (last["tglPulang"] == null) ShowInformationHeader(string.Format("Pasien belum checkout dari kunjungan (SEP : {0}).", last["noSEP"].ToString()));
                        //        else HideInformationHeader();
                        //    }

                        //    grdList.DataSource = table;
                        //    grdList.DataBind();
                        //}
                    }
                }
            }
        }
    }
}