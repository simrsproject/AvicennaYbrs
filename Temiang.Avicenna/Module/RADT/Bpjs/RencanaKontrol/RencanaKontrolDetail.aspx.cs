using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Newtonsoft.Json;
using System.Net;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class RencanaKontrolDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "RencanaKontrolSearch.aspx";
            UrlPageList = "RencanaKontrolList.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.BpjsRencanaKontrol;
        }

        protected void cboPoliDirujuk_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((Common.BPJS.VClaim.v11.RencanaKontrol.Select.ResponseSpesialistikList.List)e.Item.DataItem).NamaPoli;
            e.Item.Value = ((Common.BPJS.VClaim.v11.RencanaKontrol.Select.ResponseSpesialistikList.List)e.Item.DataItem).KodePoli;
        }

        protected override void OnMenuNewClick()
        {
            txtNoRujukan.Text = string.Empty;

            rblJenisKontrol.SelectedValue = "2";
            rblJenisKontrol_SelectedIndexChanged(null, null);

            txtTglRujukan.SelectedDate = DateTime.Now.Date;
        }

        protected void btnCariPasienSep_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNoSep.Text)) return;
            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetRencanaKontrolByNoSep(txtNoSep.Text);
            if (response.MetaData.IsValid)
            {
                string format = "yyyy-MM-dd";
                DateTime.TryParseExact(response.Response.TglSep, format, null, System.Globalization.DateTimeStyles.None, out var parsedTglSep);
                txtTglSep.SelectedDate = parsedTglSep.Date;
                txtJenisPelayanan.Text = response.Response.JnsPelayanan;
                txtPoliSep.Text = response.Response.Poli;
                txtNoPeserta.Text = response.Response.Peserta.NoKartu;
                txtNamaPeserta.Text = response.Response.Peserta.Nama;

                if (string.IsNullOrWhiteSpace(response.Response.Peserta.TglLahir))
                {
                    svc = new Common.BPJS.VClaim.v11.Service();
                    var responsePeserta = svc.GetPeserta(Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta, txtNoPeserta.Text, DateTime.Now.Date);
                    if (responsePeserta.MetaData.IsValid)
                    {
                        DateTime parsedTglLahir;
                        DateTime.TryParseExact(responsePeserta.Response.Peserta.TglLahir, format, null, System.Globalization.DateTimeStyles.None, out parsedTglLahir);
                        txtTglLahir.SelectedDate = parsedTglLahir;
                    }
                }
                else
                {
                    DateTime.TryParseExact(response.Response.Peserta.TglLahir, format, null, System.Globalization.DateTimeStyles.None, out var parsedTglLahir);
                    txtTglLahir.SelectedDate = parsedTglLahir.Date;
                }

                cboPoliDirujuk.Items.Clear();
                //if (rblJenisKontrol.SelectedValue == "2")
                if (!txtTglRujukan.IsEmpty)
                {
                    cboPoliDirujuk.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                    svc = new Common.BPJS.VClaim.v11.Service();
                    var responsePoli = svc.GetDataRencanaKontrolSpesialistik(rblJenisKontrol.SelectedValue == "1" ? Common.BPJS.VClaim.Enum.JenisKontrol.SPRI : Common.BPJS.VClaim.Enum.JenisKontrol.Kontrol,
                        rblJenisKontrol.SelectedValue == "1" ? txtNoPeserta.Text : txtNoSep.Text, txtTglRujukan.SelectedDate.Value.Date);
                    if (responsePoli.MetaData.IsValid)
                    {
                        foreach (var item in responsePoli.Response.List)
                        {
                            cboPoliDirujuk.Items.Add(new RadComboBoxItem($"{item.NamaPoli} - Kapasitas : {item.Kapasitas} - Jml Rencana Kontrol dan Rujukan : {item.JmlRencanaKontroldanRujukan}", item.KodePoli));
                        }
                    }
                    else
                    {
                        cboPoliDirujuk.Items.Add(new RadComboBoxItem(responsePoli.MetaData.Message, string.Empty));
                    }
                }
            }
            else
                ShowInformationHeader(string.Format("Code {0}), Message : {1}", response.MetaData.Code, response.MetaData.Message));
        }

        protected void rblJenisKontrol_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (rblJenisKontrol.SelectedValue == "1")
            //{
            //    txtNoSep.ReadOnly = true;
            //    btnCariPasienSep.Enabled = false;
            //    txtNoPeserta.ReadOnly = false;
            //    btnCariPasienPeserta.Enabled = true;
            //}
            //else
            //{
            //    txtNoSep.ReadOnly = false;
            //    btnCariPasienSep.Enabled = true;
            //    txtNoPeserta.ReadOnly = true;
            //    btnCariPasienPeserta.Enabled = false;
            //}
            txtNoSep.Text = string.Empty;
            txtTglSep.Clear();
            txtJenisPelayanan.Text = string.Empty;
            txtPoliSep.Text = string.Empty;
            txtNoPeserta.Text = string.Empty;
            txtTglLahir.Clear();
            txtTglRujukan.Clear();
            cboPoliDirujuk.Items.Clear();
            cboPoliDirujuk.SelectedValue = string.Empty;
            cboPoliDirujuk.Text = string.Empty;
            cboDpjpKontrol.Items.Clear();
            cboDpjpKontrol.SelectedValue = string.Empty;
            cboDpjpKontrol.Text = string.Empty;
        }

        protected void btnCariPasienPeserta_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNoPeserta.Text)) return;
            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetPeserta(Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta, txtNoPeserta.Text, DateTime.Now.Date);
            if (response.MetaData.IsValid)
            {
                txtTglSep.Clear();
                txtJenisPelayanan.Text = string.Empty;
                txtPoliSep.Text = string.Empty;
                txtNoPeserta.Text = response.Response.Peserta.NoKartu;
                txtNamaPeserta.Text = response.Response.Peserta.Nama;

                svc = new Common.BPJS.VClaim.v11.Service();
                var responsePeserta = svc.GetPeserta(Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta, txtNoPeserta.Text, DateTime.Now.Date);
                if (responsePeserta.MetaData.IsValid)
                {
                    string format = "yyyy-MM-dd";
                    DateTime parsedTglLahir;
                    DateTime.TryParseExact(responsePeserta.Response.Peserta.TglLahir, format, null, System.Globalization.DateTimeStyles.None, out parsedTglLahir);
                    txtTglLahir.SelectedDate = parsedTglLahir;
                }

                cboPoliDirujuk.Items.Clear();
                cboPoliDirujuk.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                svc = new Common.BPJS.VClaim.v11.Service();
                var responsePoli = svc.GetDataRencanaKontrolSpesialistik(rblJenisKontrol.SelectedValue == "1" ? Common.BPJS.VClaim.Enum.JenisKontrol.SPRI : Common.BPJS.VClaim.Enum.JenisKontrol.Kontrol,
                    rblJenisKontrol.SelectedValue == "1" ? txtNoPeserta.Text : txtNoSep.Text, txtTglRujukan.IsEmpty ? txtTglRujukan.FocusedDate.Date : txtTglRujukan.SelectedDate.Value.Date);
                if (responsePoli.MetaData.IsValid)
                {
                    foreach (var item in responsePoli.Response.List)
                    {
                        cboPoliDirujuk.Items.Add(new RadComboBoxItem($"{item.NamaPoli} - Kapasitas : {item.Kapasitas} - Jml Rencana Kontrol dan Rujukan : {item.JmlRencanaKontroldanRujukan}", item.KodePoli));
                    }
                }
                else
                {
                    cboPoliDirujuk.Items.Add(new RadComboBoxItem(responsePoli.MetaData.Message, string.Empty));
                }
            }
            else
                ShowInformationHeader(string.Format("Code {0}), Message : {1}", response.MetaData.Code, response.MetaData.Message));
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            if (rblJenisKontrol.SelectedValue == "1")
            {
                var response = svc.Insert(new Common.BPJS.VClaim.v11.RencanaKontrol.InsertSpri.Request.Root()
                {
                    Request = new Common.BPJS.VClaim.v11.RencanaKontrol.InsertSpri.Request.TRequest()
                    {
                        NoKartu = txtNoPeserta.Text,
                        KodeDokter = cboDpjpKontrol.SelectedValue,
                        PoliKontrol = cboPoliDirujuk.SelectedValue,
                        TglRencanaKontrol = txtTglRujukan.IsEmpty ? txtTglRujukan.FocusedDate.Date.ToString("yyyy-MM-dd") : txtTglRujukan.SelectedDate.Value.ToString("yyyy-MM-dd"),
                        User = AppSession.UserLogin.UserID
                    }
                });
                if (response.MetaData.IsValid) txtNoRujukan.Text = response.Response.NoSPRI;
                else
                {
                    ShowInformationHeader(string.Format("Code {0}), Message : {1}", response.MetaData.Code, response.MetaData.Message));
                    args.MessageText = string.Format("Code {0}), Message : {1}", response.MetaData.Code, response.MetaData.Message);
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                var response = svc.Insert(new Common.BPJS.VClaim.v11.RencanaKontrol.Insert.Request.Root()
                {
                    Request = new Common.BPJS.VClaim.v11.RencanaKontrol.Insert.Request.TRequest()
                    {
                        NoSEP = txtNoSep.Text,
                        KodeDokter = cboDpjpKontrol.SelectedValue,
                        PoliKontrol = cboPoliDirujuk.SelectedValue,
                        TglRencanaKontrol = txtTglRujukan.IsEmpty ? txtTglRujukan.FocusedDate.Date.ToString("yyyy-MM-dd") : txtTglRujukan.SelectedDate.Value.ToString("yyyy-MM-dd"),
                        User = AppSession.UserLogin.UserID
                    }
                });
                if (response.MetaData.IsValid)
                {
                    txtNoRujukan.Text = response.Response.NoSuratKontrol;

                    if (string.IsNullOrWhiteSpace(cboPoliDirujuk.SelectedValue)) return;
                    //var load = false;

                    //var poli = new ServiceUnitBridging();
                    //poli.Query.Where(poli.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                    //poli.Query.Where($"< SUBSTRING(BridgingID, CHARINDEX(';', BridgingID) + 1, 3) = '{cboPoliDirujuk.SelectedValue}'>");
                    //load = poli.Query.Load();
                    //if (!load)
                    //{
                    //    poli = new ServiceUnitBridging();
                    //    poli.Query.Where(poli.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                    //    poli.Query.Where($"< SUBSTRING(BridgingID, 0, CHARINDEX(';', BridgingID)) = '{cboPoliDirujuk.SelectedValue}'>");
                    //    load = poli.Query.Load();
                    //}

                    //if (load)
                    //{
                    var pb = new ParamedicBridging();
                    pb.Query.Where(pb.Query.BridgingID == cboDpjpKontrol.SelectedValue, pb.Query.SRBridgingType == AppEnum.BridgingType.BPJS.ToString());
                    if (!pb.Query.Load()) return;

                    var psds = new ParamedicScheduleDateCollection();
                    psds.Query.Where(psds.Query.ScheduleDate.Date() == txtTglRujukan.SelectedDate?.Date,
                        psds.Query.ParamedicID == pb.ParamedicID);
                    if (!psds.Query.Load()) return;

                    foreach (var psd in psds)
                    {
                        var appt = new AppointmentQuery("a");
                        var pat = new PatientQuery("b");

                        appt.es.Top = 1;
                        appt.InnerJoin(pat).On(appt.PatientID == pat.PatientID);
                        appt.Where(pat.GuarantorID == txtNoPeserta.Text, appt.AppointmentDate.Date() == txtTglRujukan.SelectedDate?.Date, appt.ParamedicID == pb.ParamedicID, appt.ServiceUnitID == psd.ServiceUnitID);

                        var app = new BusinessObject.Appointment();
                        if (!app.Load(appt)) continue;
                        app.ReferenceNumber = txtNoRujukan.Text;
                        app.Save();
                    }
                    //}
                }
                else
                {
                    ShowInformationHeader(string.Format("Code {0}), Message : {1}", response.MetaData.Code, response.MetaData.Message));
                    args.MessageText = string.Format("Code {0}), Message : {1}", response.MetaData.Code, response.MetaData.Message);
                    args.IsCancel = true;
                    return;
                }
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            if (rblJenisKontrol.SelectedValue == "1")
            {
                var response = svc.Update(new Common.BPJS.VClaim.v11.RencanaKontrol.UpdateSpri.Request.Root()
                {
                    Request = new Common.BPJS.VClaim.v11.RencanaKontrol.UpdateSpri.Request.TRequest()
                    {
                        NoSPRI = txtNoRujukan.Text,
                        KodeDokter = cboDpjpKontrol.SelectedValue,
                        PoliKontrol = cboPoliDirujuk.SelectedValue,
                        TglRencanaKontrol = txtTglRujukan.IsEmpty ? txtTglRujukan.FocusedDate.Date.ToString("yyyy-MM-dd") : txtTglRujukan.SelectedDate.Value.ToString("yyyy-MM-dd"),
                        User = AppSession.UserLogin.UserID
                    }
                });
                if (response.MetaData.IsValid) txtNoRujukan.Text = response.Response.NoSPRI;
                else ShowInformationHeader(string.Format("Code {0}), Message : {1}", response.MetaData.Code, response.MetaData.Message));
            }
            else
            {
                var response = svc.Update(new Common.BPJS.VClaim.v11.RencanaKontrol.Update.Request.Root()
                {
                    Request = new Common.BPJS.VClaim.v11.RencanaKontrol.Update.Request.TRequest()
                    {
                        NoSuratKontrol = txtNoRujukan.Text,
                        NoSEP = txtNoSep.Text,
                        KodeDokter = cboDpjpKontrol.SelectedValue,
                        PoliKontrol = cboPoliDirujuk.SelectedValue,
                        TglRencanaKontrol = txtTglRujukan.IsEmpty ? txtTglRujukan.FocusedDate.Date.ToString("yyyy-MM-dd") : txtTglRujukan.SelectedDate.Value.ToString("yyyy-MM-dd"),
                        User = AppSession.UserLogin.UserID
                    }
                });
                if (response.MetaData.IsValid)
                {
                    txtNoRujukan.Text = response.Response.NoSuratKontrol;

                    if (string.IsNullOrWhiteSpace(cboPoliDirujuk.SelectedValue)) return;
                    //var load = false;

                    //var poli = new ServiceUnitBridging();
                    //poli.Query.Where(poli.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                    //poli.Query.Where($"< SUBSTRING(BridgingID, CHARINDEX(';', BridgingID) + 1, 3) = '{cboPoliDirujuk.SelectedValue}'>");
                    //load = poli.Query.Load();
                    //if (!load)
                    //{
                    //    poli = new ServiceUnitBridging();
                    //    poli.Query.Where(poli.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                    //    poli.Query.Where($"< SUBSTRING(BridgingID, 0, CHARINDEX(';', BridgingID)) = '{cboPoliDirujuk.SelectedValue}'>");
                    //    load = poli.Query.Load();
                    //}

                    //if (load)
                    //{
                    var pb = new ParamedicBridging();
                    pb.Query.Where(pb.Query.BridgingID == cboDpjpKontrol.SelectedValue, pb.Query.SRBridgingType == AppEnum.BridgingType.BPJS.ToString());
                    if (!pb.Query.Load()) return;

                    var psds = new ParamedicScheduleDateCollection();
                    psds.Query.Where(psds.Query.ScheduleDate.Date() == txtTglRujukan.SelectedDate?.Date,
                        psds.Query.ParamedicID == pb.ParamedicID);
                    if (!psds.Query.Load()) return;

                    foreach (var psd in psds)
                    {
                        var appt = new AppointmentQuery("a");
                        var pat = new PatientQuery("b");

                        appt.es.Top = 1;
                        appt.InnerJoin(pat).On(appt.PatientID == pat.PatientID);
                        appt.Where(pat.GuarantorID == txtNoPeserta.Text, appt.AppointmentDate.Date() == txtTglRujukan.SelectedDate?.Date, appt.ParamedicID == pb.ParamedicID, appt.ServiceUnitID == psd.ServiceUnitID);

                        var app = new BusinessObject.Appointment();
                        if (!app.Load(appt)) continue;
                        app.ReferenceNumber = txtNoRujukan.Text;
                        app.Save();
                    }
                    //}
                }
                else ShowInformationHeader(string.Format("Code {0}), Message : {1}", response.MetaData.Code, response.MetaData.Message));
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.Delete(new Common.BPJS.VClaim.v11.RencanaKontrol.Delete.Request.Root()
            {
                Request = new Common.BPJS.VClaim.v11.RencanaKontrol.Delete.Request.TRequest()
                {
                    TSuratkontrol = new Common.BPJS.VClaim.v11.RencanaKontrol.Delete.Request.TSuratkontrol()
                    {
                        NoSuratKontrol = txtNoRujukan.Text,
                        User = AppSession.UserLogin.UserID
                    }
                }
            });
            if (!response.MetaData.IsValid)
                ShowInformationHeader(string.Format("Code {0}), Message : {1}", response.MetaData.Code, response.MetaData.Message));

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            if (parameters.Length > 0)
            {
                string noKontrol = parameters[0];
                var svc = new Common.BPJS.VClaim.v11.Service();
                var response = svc.GetRencanaKontrolByNoSuratKontrol(noKontrol);
                if (response.MetaData.IsValid)
                {
                    txtNoRujukan.Text = response.Response.NoSuratKontrol;
                    rblJenisKontrol.SelectedValue = response.Response.JnsKontrol;
                    txtNoSep.Text = response.Response.Sep.NoSep;

                    string format = "yyyy-MM-dd";

                    if (!string.IsNullOrWhiteSpace(response.Response.Sep.NoSep))
                    {
                        DateTime parsed;
                        DateTime.TryParseExact(response.Response.Sep.TglSep, format, null, System.Globalization.DateTimeStyles.None, out parsed);
                        txtTglSep.SelectedDate = parsed.Date;
                        txtJenisPelayanan.Text = response.Response.Sep.JnsPelayanan;
                        txtPoliSep.Text = response.Response.Sep.Poli;
                        txtNoPeserta.Text = response.Response.Sep.Peserta.NoKartu;
                        txtNamaPeserta.Text = response.Response.Sep.Peserta.Nama;
                    }
                    else
                    {
                        txtNoPeserta.Text = Request.QueryString["nopeserta"];
                        txtNamaPeserta.Text = Request.QueryString["namapeserta"];
                    }

                    if (!string.IsNullOrWhiteSpace(txtNoPeserta.Text))
                    {
                        svc = new Common.BPJS.VClaim.v11.Service();
                        var responsePeserta = svc.GetPeserta(Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta, txtNoPeserta.Text, DateTime.Now.Date);
                        if (responsePeserta.MetaData.IsValid)
                        {
                            DateTime parsedTglLahir;
                            DateTime.TryParseExact(responsePeserta.Response.Peserta.TglLahir, format, null, System.Globalization.DateTimeStyles.None, out parsedTglLahir);
                            txtTglLahir.SelectedDate = parsedTglLahir;
                        }
                    }

                    DateTime parsedKontrol;
                    DateTime.TryParseExact(response.Response.TglRencanaKontrol, format, null, System.Globalization.DateTimeStyles.None, out parsedKontrol);
                    txtTglRujukan.SelectedDate = parsedKontrol.Date;

                    cboPoliDirujuk.Items.Clear();
                    cboPoliDirujuk.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                    svc = new Common.BPJS.VClaim.v11.Service();
                    var responsePoli = svc.GetDataRencanaKontrolSpesialistik(rblJenisKontrol.SelectedValue == "1" ? Common.BPJS.VClaim.Enum.JenisKontrol.SPRI : Common.BPJS.VClaim.Enum.JenisKontrol.Kontrol,
                        rblJenisKontrol.SelectedValue == "1" ? txtNoPeserta.Text : txtNoSep.Text, txtTglRujukan.IsEmpty ? txtTglRujukan.FocusedDate.Date : txtTglRujukan.SelectedDate.Value.Date);
                    if (responsePoli.MetaData.IsValid)
                    {
                        foreach (var item in responsePoli.Response.List)
                        {
                            cboPoliDirujuk.Items.Add(new RadComboBoxItem($"{item.NamaPoli} - Kapasitas : {item.Kapasitas} - Jml Rencana Kontrol dan Rujukan : {item.JmlRencanaKontroldanRujukan}", item.KodePoli));
                        }
                    }
                    else
                    {
                        var sub = new ServiceUnitBridging();
                        sub.Query.es.Top = 1;
                        sub.Query.Where(sub.Query.SRBridgingType == AppEnum.BridgingType.BPJS.ToString(),
                            sub.Query.BridgingID == response.Response.PoliTujuan);
                        sub.Query.OrderBy(sub.Query.ServiceUnitID.Ascending);
                        if (sub.Query.Load())
                        {
                            cboPoliDirujuk.Items.Add(new RadComboBoxItem(sub.BridgingName, response.Response.PoliTujuan));
                        }
                    }
                    cboPoliDirujuk.SelectedValue = response.Response.PoliTujuan;

                    cboDpjpKontrol.Items.Clear();
                    cboDpjpKontrol.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                    svc = new Common.BPJS.VClaim.v11.Service();
                    var responseDpjp = svc.GetDataRencanaKontrolJadwalDokter(rblJenisKontrol.SelectedValue == "1" ? Common.BPJS.VClaim.Enum.JenisKontrol.SPRI : Common.BPJS.VClaim.Enum.JenisKontrol.Kontrol,
                        cboPoliDirujuk.SelectedValue, txtTglRujukan.IsEmpty ? txtTglRujukan.FocusedDate.Date : txtTglRujukan.SelectedDate.Value.Date);
                    if (responseDpjp.MetaData.IsValid)
                    {
                        foreach (var item in responseDpjp.Response.List)
                        {
                            cboDpjpKontrol.Items.Add(new RadComboBoxItem(item.NamaDokter, item.KodeDokter));
                        }

                        cboDpjpKontrol.SelectedValue = response.Response.KodeDokter;
                    }
                }
                else
                {
                    OnMenuNewClick();

                    string noKartu = parameters[1];

                    svc = new Common.BPJS.VClaim.v11.Service();
                    var kontrol = svc.GetRencanaKontrolByNoPeserta(DateTime.Now.ToString("MM"), DateTime.Now.Year.ToString(), noKartu, Common.BPJS.VClaim.Enum.FilterRencanaKontrol.TanggalRencanaKontrol);
                    if (kontrol.MetaData.IsValid)
                    {
                        var item = kontrol.Response.List.SingleOrDefault(k => k.NoSuratKontrol == noKontrol);
                        if (item != null)
                        {
                            txtNoRujukan.Text = item.NoSuratKontrol;
                            rblJenisKontrol.SelectedValue = item.JnsKontrol;
                            txtNoSep.Text = item.NoSepAsalKontrol;

                            string format = "yyyy-MM-dd";

                            if (!string.IsNullOrWhiteSpace(txtNoSep.Text))
                            {
                                DateTime parsed;
                                DateTime.TryParseExact(item.TglSEP, format, null, System.Globalization.DateTimeStyles.None, out parsed);
                                txtTglSep.SelectedDate = parsed.Date;
                                txtJenisPelayanan.Text = item.JnsPelayanan;
                                txtPoliSep.Text = item.PoliAsal + " - " + item.NamaPoliAsal;
                                txtNoPeserta.Text = item.NoKartu;
                                txtNamaPeserta.Text = item.Nama;
                            }
                            else
                            {
                                txtNoPeserta.Text = Request.QueryString["nopeserta"];
                                txtNamaPeserta.Text = Request.QueryString["namapeserta"];
                            }

                            if (!string.IsNullOrWhiteSpace(txtNoPeserta.Text))
                            {
                                svc = new Common.BPJS.VClaim.v11.Service();
                                var responsePeserta = svc.GetPeserta(Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta, txtNoPeserta.Text, DateTime.Now.Date);
                                if (responsePeserta.MetaData.IsValid)
                                {
                                    DateTime parsedTglLahir;
                                    DateTime.TryParseExact(responsePeserta.Response.Peserta.TglLahir, format, null, System.Globalization.DateTimeStyles.None, out parsedTglLahir);
                                    txtTglLahir.SelectedDate = parsedTglLahir;
                                }
                            }

                            DateTime parsedKontrol;
                            DateTime.TryParseExact(item.TglRencanaKontrol, format, null, System.Globalization.DateTimeStyles.None, out parsedKontrol);
                            txtTglRujukan.SelectedDate = parsedKontrol.Date;

                            cboPoliDirujuk.Items.Clear();
                            cboPoliDirujuk.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                            svc = new Common.BPJS.VClaim.v11.Service();
                            var responsePoli = svc.GetDataRencanaKontrolSpesialistik(rblJenisKontrol.SelectedValue == "1" ? Common.BPJS.VClaim.Enum.JenisKontrol.SPRI : Common.BPJS.VClaim.Enum.JenisKontrol.Kontrol,
                                rblJenisKontrol.SelectedValue == "1" ? txtNoPeserta.Text : txtNoSep.Text, txtTglRujukan.IsEmpty ? txtTglRujukan.FocusedDate.Date : txtTglRujukan.SelectedDate.Value.Date);
                            if (responsePoli.MetaData.IsValid)
                            {
                                foreach (var data in responsePoli.Response.List)
                                {
                                    cboPoliDirujuk.Items.Add(new RadComboBoxItem($"{data.NamaPoli} - Kapasitas : {data.Kapasitas} - Jml Rencana Kontrol dan Rujukan : {data.JmlRencanaKontroldanRujukan}", data.KodePoli));
                                }

                                if (rblJenisKontrol.SelectedValue == "1")
                                {
                                    var index = 0;
                                    var exist = false;
                                    for (index = 0; index < cboPoliDirujuk.Items.Count; index++)
                                    {
                                        if (cboPoliDirujuk.Items[index].Text.Trim().ToLower() == item.NamaPoliTujuan.Trim().ToLower())
                                        {
                                            exist = true;
                                            break;
                                        }
                                    }
                                    if (exist) cboPoliDirujuk.SelectedIndex = index;
                                }
                                else cboPoliDirujuk.SelectedValue = item.PoliTujuan;
                            }

                            cboDpjpKontrol.Items.Clear();
                            cboDpjpKontrol.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                            if (!string.IsNullOrWhiteSpace(cboPoliDirujuk.SelectedValue))
                            {
                                svc = new Common.BPJS.VClaim.v11.Service();
                                var responseDpjp = svc.GetDataRencanaKontrolJadwalDokter(rblJenisKontrol.SelectedValue == "1" ? Common.BPJS.VClaim.Enum.JenisKontrol.SPRI : Common.BPJS.VClaim.Enum.JenisKontrol.Kontrol,
                                    cboPoliDirujuk.SelectedValue, txtTglRujukan.IsEmpty ? txtTglRujukan.FocusedDate.Date : txtTglRujukan.SelectedDate.Value.Date);
                                if (responseDpjp.MetaData.IsValid)
                                {
                                    foreach (var data in responseDpjp.Response.List)
                                    {
                                        cboDpjpKontrol.Items.Add(new RadComboBoxItem(data.NamaDokter, data.KodeDokter));
                                    }

                                    cboDpjpKontrol.SelectedValue = item.KodeDokter;
                                }
                            }
                        }
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, GetType(), "cari", string.Format("alert('Code : {0}, Message : {1}');", response.MetaData.Code, response.MetaData.Message), true);
                }
            }
        }

        protected void cboPoliDirujuk_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Value))
            {
                cboDpjpKontrol.Items.Clear();
                cboDpjpKontrol.SelectedValue = string.Empty;
                cboDpjpKontrol.Text = string.Empty;
                return;
            }

            cboDpjpKontrol.Items.Clear();
            cboDpjpKontrol.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetDataRencanaKontrolJadwalDokter(rblJenisKontrol.SelectedValue == "1" ? Common.BPJS.VClaim.Enum.JenisKontrol.SPRI : Common.BPJS.VClaim.Enum.JenisKontrol.Kontrol,
                e.Value, txtTglRujukan.SelectedDate.Value.Date);
            if (response.MetaData.IsValid)
            {
                foreach (var item in response.Response.List)
                {
                    cboDpjpKontrol.Items.Add(new RadComboBoxItem(item.NamaDokter, item.KodeDokter));
                }
            }
            else
            {
                cboDpjpKontrol.Items.Add(new RadComboBoxItem(response.MetaData.Message, string.Empty));
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            OnPopulateEntryControl(new string[0]);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            OnPopulateEntryControl(new string[0]);
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetRencanaKontrolByNoSuratKontrol(txtNoRujukan.Text);
            if (response.MetaData.IsValid)
            {
                if (string.IsNullOrWhiteSpace(response.Response.Sep.Peserta.NoKartu)) response.Response.Sep.Peserta.NoKartu = txtNoPeserta.Text;
                if (string.IsNullOrWhiteSpace(response.Response.Sep.Peserta.Nama)) response.Response.Sep.Peserta.Nama = txtNamaPeserta.Text;
                if (string.IsNullOrWhiteSpace(response.Response.Sep.Peserta.TglLahir)) response.Response.Sep.Peserta.TglLahir = txtTglLahir.SelectedDate.Value.ToString("yyyy-MM-dd");

                var json = new JsonBridgingValueTemp();
                if (!json.LoadByPrimaryKey(txtNoRujukan.Text)) json = new JsonBridgingValueTemp();
                json.Id = txtNoRujukan.Text;
                json.JsonValue = JsonConvert.SerializeObject(response.Response);
                json.Save();

                printJobParameters.AddNew("p_NoKontrol", txtNoRujukan.Text);
            }
            else
            {
                svc = new Common.BPJS.VClaim.v11.Service();
                var list = svc.GetRencanaKontrolByNoPeserta((txtTglRujukan.SelectedDate.Value.Month.ToString().Length == 1 ? "0" : string.Empty) + txtTglRujukan.SelectedDate.Value.Month.ToString(), txtTglRujukan.SelectedDate.Value.Year.ToString(), txtNoPeserta.Text, Common.BPJS.VClaim.Enum.FilterRencanaKontrol.TanggalRencanaKontrol);
                var data = list.Response.List.SingleOrDefault(l => l.TglRencanaKontrol == txtTglRujukan.SelectedDate.Value.ToString("yyyy-MM-dd"));
                if (data != null)
                {
                    var json = new JsonBridgingValueTemp();
                    if (!json.LoadByPrimaryKey(txtNoRujukan.Text)) json = new JsonBridgingValueTemp();
                    json.Id = txtNoRujukan.Text;
                    json.JsonValue = JsonConvert.SerializeObject(new Common.BPJS.VClaim.v11.RencanaKontrol.Select.ResponseSuratKontrol.Response()
                    {
                        NoSuratKontrol = data.NoSuratKontrol,
                        TglRencanaKontrol = data.TglRencanaKontrol,
                        TglTerbit = data.TglTerbitKontrol,
                        JnsKontrol = data.JnsKontrol,
                        NamaJnsKontrol = data.NamaJnsKontrol,
                        PoliTujuan = data.PoliTujuan,
                        NamaPoliTujuan = data.NamaPoliTujuan,
                        KodeDokter = data.KodeDokter,
                        NamaDokter = data.NamaDokter,
                        Sep = new Common.BPJS.VClaim.v11.RencanaKontrol.Select.ResponseSuratKontrol.Sep()
                        {
                            Peserta = new Common.BPJS.VClaim.v11.RencanaKontrol.Select.ResponseSuratKontrol.Peserta()
                            {
                                NoKartu = txtNoPeserta.Text,
                                Nama = txtNamaPeserta.Text,
                                TglLahir = txtTglLahir.SelectedDate.Value.ToString("yyyy-MM-dd")
                            }
                        }
                    });
                    json.Save();

                    printJobParameters.AddNew("p_NoKontrol", txtNoRujukan.Text);
                }
            }
        }

        protected void txtTglRujukan_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            cboPoliDirujuk.Items.Clear();
            cboPoliDirujuk.SelectedValue = string.Empty;
            cboPoliDirujuk.Text = string.Empty;

            cboDpjpKontrol.Items.Clear();
            cboDpjpKontrol.SelectedValue = string.Empty;
            cboDpjpKontrol.Text = string.Empty;

            if (!string.IsNullOrWhiteSpace(txtNoPeserta.Text)) btnCariPasienPeserta_Click(null, null);
            if (!string.IsNullOrWhiteSpace(txtNoSep.Text)) btnCariPasienSep_Click(null, null);
        }
    }
}