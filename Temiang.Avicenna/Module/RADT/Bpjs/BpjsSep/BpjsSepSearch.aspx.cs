using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Configuration;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class BpjsSepSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.BpjsSep;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }

        public override bool OnButtonOkClicked()
        {
            //validasi
            //if (!string.IsNullOrEmpty(txtNoSep.Text))
            //{
            //    var bs = new BpjsSEP();
            //    if (!bs.LoadByPrimaryKey(txtNoSep.Text))
            //    {
            //        var svc = new Common.BPJS.VClaim.v11.Service();
            //        var response = svc.GetSep(txtNoSep.Text);
            //        if (response.MetaData.IsValid)
            //        {
            //            var sep = response.Response;
            //            bs = new BpjsSEP();
            //            //bs.SepID,
            //            bs.NoSEP = sep.NoSep;
            //            bs.NomorKartu = sep.Peserta.NoKartu;
            //            bs.TanggalSEP = Convert.ToDateTime( sep.TglSep);

            //            svc = new Common.BPJS.VClaim.v11.Service();
            //            var rjk = svc.GetRujukan(true, sep.NoRujukan, Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);
            //            if (!rjk.MetaData.IsValid) 
            //            {
            //                svc = new Common.BPJS.VClaim.v11.Service();
            //                rjk = svc.GetRujukan(true, sep.NoRujukan, Common.BPJS.VClaim.Enum.JenisFaskes.RS);
            //                bs.JenisRujukan = "2";
            //            }
            //            else bs.JenisRujukan = "1";
            //            bs.TanggalRujukan = Convert.ToDateTime(rjk.Response.Rujukan.TglKunjungan);
            //            bs.PPKRujukan = rjk.Response.Rujukan.ProvPerujuk.Kode;
            //            bs.NamaPPKRujukan = rjk.Response.Rujukan.ProvPerujuk.Nama;
            //            bs.NoRujukan = sep.NoRujukan;
            //            bs.PPKPelayanan = ConfigurationManager.AppSettings["BPJSHospitalID"];
            //            bs.JenisPelayanan = bs.JenisPelayanan == "Rawat Jalan" ? "2" : "1";
            //            bs.Catatan = sep.Catatan;

            //            //svc = new Common.BPJS.VClaim.v11.Service();
            //            //var diag = svc.GetDiagnosa(sep.Diagnosa);
            //            //if (diag.MetaData.IsValid) bs.DiagnosaAwal = diag.Response.Diagnosa[0].Kode;
            //            //else 
            //            bs.DiagnosaAwal = rjk.Response.Rujukan.Diagnosa.Kode;

            //            svc = new Common.BPJS.VClaim.v11.Service();
            //            var poli = svc.GetPoli(sep.Poli);
            //            if (poli.MetaData.IsValid) bs.PoliTujuan = poli.Response.Poli[0].Kode;

            //            //bs.KelasRawat,
            //            //bs.LakaLantas,
            //            bs.User = string.Empty;

            //            svc = new Common.BPJS.VClaim.v11.Service();
            //            var pst = svc.GetPeserta(Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta, bs.NomorKartu, bs.TanggalSEP.Value.Date);
            //            if (pst.MetaData.IsValid)
            //            {
            //            bs.NoMR = pst.Response.Peserta.Mr.NoMR;
            //            //bs.TanggalPulang,
            //            bs.NoTransaksi = string.Empty;
            //            bs.NamaPasien = pst.Response.Peserta.Nama;
            //            bs.Nik = pst.Response.Peserta.Nik;
            //            bs.JenisKelamin = pst.Response.Peserta.Sex;
            //            bs.TanggalLahir = Convert.ToDateTime( pst.Response.Peserta.TglLahir);
            //            bs.JenisPeserta = pst.Response.Peserta.JenisPeserta.Kode + " - " + pst.Response.Peserta.JenisPeserta.Keterangan;
            //            }
            //            bs.DetailKeanggotaan = string.Empty;
            //            bs.PatientID,
            //            //bs.KodeCBG = string.Empty;
            //            //bs.TariffCBG = string.Empty;
            //            //bs.DeskripsiCBG,
            //            //bs.LokasiLaka,
            //            bs.NamaKelasRawat =  pst.Response.Peserta.HakKelas.Kode + " - " + pst.Response.Peserta.HakKelas.Keterangan;
            //            bs.IsEksekutif = sep.PoliEksekutif == "0" ? false : true;
            //            bs.IsCob = string.IsNullOrEmpty(sep.Penjamin) ? false : true;
            //            //bs.PenjaminLaka,
            //            //bs.NamaCob,
            //            bs.StatusPeserta = pst.Response.Peserta.StatusPeserta.Kode + " - " + pst.Response.Peserta.StatusPeserta.Keterangan;
            //            bs.Umur = pst.Response.Peserta.Umur.UmurSaatPelayanan;
            //            //bs.IsKatarak,
            //            //bs.TglKejadian,
            //            //bs.IsSuplesi,
            //            //bs.NoSepSuplesi,
            //            //bs.KodePropinsi,
            //            //bs.KodeKabupaten,
            //            //bs.KodeKecamatan,
            //            bs.NoSkdp = string.Empty;
            //            bs.KodeDpjp = string.Empty;rjk.Response.
            //            //bs.FromRegistrationNo

            //            bs.Save();
            //        }
            //    }
            //}

            HideInformationHeader();

            if (!string.IsNullOrEmpty(txtNoKartu.Text) && txtNoKartu.Text.Length != 13)
            {
                ShowInformationHeader("No Peserta harus 13 digit");
                return false;
            }
            if (!string.IsNullOrEmpty(txtNoNik.Text) && txtNoNik.Text.Length != 13)
            {
                ShowInformationHeader("No NIK harus 16 digit");
                return false;
            }

            BpjsSEPQuery query = new BpjsSEPQuery("a");

            var std = new AppStandardReferenceItemQuery("b");
            var diag = new DiagnoseQuery("c");
            var reg = new RegistrationQuery("e");
            var brg = new ServiceUnitBridgingQuery("f");
            var appt = new AppointmentQuery("g");
            var unit = new ServiceUnitQuery("h");
            //var brg2 = new ServiceUnitBridgingQuery("i");

            query.es.Distinct = true;
            query.LeftJoin(std).On(std.StandardReferenceID == AppEnum.StandardReference.BpjsTypeOfService.ToString() && std.ItemID == query.JenisPelayanan);
            query.LeftJoin(diag).On(query.DiagnosaAwal == diag.DiagnoseID);
            query.LeftJoin(brg).On(query.PoliTujuan == brg.BridgingID && brg.SRBridgingType == AppEnum.BridgingType.BPJS.ToString());
            query.LeftJoin(reg).On(query.NoSEP == reg.BpjsSepNo && query.NomorKartu == reg.GuarantorCardNo && reg.IsVoid == false && reg.IsFromDispensary == false && query.TanggalSEP.Date() == reg.RegistrationDate.Date());
            query.LeftJoin(appt).On(query.NoTransaksi == appt.AppointmentNo);
            query.LeftJoin(unit).On(appt.ServiceUnitID == unit.ServiceUnitID);
            //query.LeftJoin(brg2).On(appt.ServiceUnitID == brg2.ServiceUnitID && brg2.SRBridgingType == AppEnum.BridgingType.BPJS.ToString());
            query.Select(
                    query.SepID,
                    query.NoSEP,
                    query.TanggalSEP,
                    query.NoRujukan,
                    query.TanggalRujukan,
                    query.NomorKartu,
                    //query.NamaPasien,
                    query.TanggalLahir,
                    query.LastUpdateByUserID,
                    std.ItemName.As("TypeOfService"),
                    diag.DiagnoseName,
                    "<CAST(CASE WHEN a.LakaLantas = '1' THEN 1 ELSE 0 END AS BIT) AS IsLakaLantas>",
                    //query.PoliTujuan.As("BridgingID"),
                    //brg.BridgingID.Coalesce("''"),
                    "<ISNULL(h.ServiceUnitName, f.BridgingName) AS BridgingName>",
                    //brg.BridgingName.Coalesce("''"),
                    //"<CAST(ISNULL((SELECT CASE WHEN (SELECT COUNT(r.RegistrationNo) FROM Registration AS r WHERE r.BpjsSepNo = a.NoSEP AND r.IsVoid = 0 AND r.IsFromDispensary = 0) > 0 THEN 1 ELSE 0 END), 0) AS BIT) AS IsRegistration>",
                    "<CAST(CASE WHEN e.RegistrationNo IS NULL THEN 0 ELSE 1 END AS BIT) AS IsRegistration>",
                    "<a.NamaPasien + ' (' + a.JenisKelamin + ')' AS NamaPasienJK>",
                    reg.RegistrationNo.Coalesce("''")
                );

            if (!string.IsNullOrEmpty(txtNoSep.Text)) query.Where(query.NoSEP == txtNoSep.Text);
            if (!string.IsNullOrEmpty(txtNoKartu.Text)) query.Where(query.NomorKartu == txtNoKartu.Text);
            if (!txtTanggalSep.IsEmpty) query.Where(query.TanggalSEP == txtTanggalSep.SelectedDate.Value.Date);
            if (!string.IsNullOrEmpty(txtNamaPasien.Text))
            {
                if (cboFilterNamaPasien.SelectedValue == "Contains") query.Where(query.NamaPasien.Like("%" + txtNamaPasien.Text + "%"));
                else query.Where(query.NamaPasien == txtNamaPasien.Text);
            }
            query.OrderBy(query.NoSEP.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
