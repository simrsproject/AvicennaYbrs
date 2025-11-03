using DevExpress.DataProcessing.InMemoryDataProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.Common.BPJS.VClaim.Klaim;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class RujukanDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "RujukanSearch.aspx";
            UrlPageList = "RujukanList.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.BpjsRujukan;
        }

        protected void cboDiagnosaSep_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((Common.BPJS.VClaim.v11.Diagnosa.Diagnosa2)e.Item.DataItem).Nama;
            e.Item.Value = ((Common.BPJS.VClaim.v11.Diagnosa.Diagnosa2)e.Item.DataItem).Kode;
        }

        protected void cboAsalRujukanSep_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((Common.BPJS.VClaim.v11.Faskes.Faske)e.Item.DataItem).Nama;
            e.Item.Value = ((Common.BPJS.VClaim.v11.Faskes.Faske)e.Item.DataItem).Kode;
        }

        protected void cboPoliDirujuk_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((Common.BPJS.VClaim.v11.Poli.Poli2)e.Item.DataItem).Nama;
            e.Item.Value = ((Common.BPJS.VClaim.v11.Poli.Poli2)e.Item.DataItem).Kode;

        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (sourceControl is RadTextBox)
            {
                if (((RadTextBox)sourceControl).ID == txtNoSep.ID)
                {
                    var sep = new BpjsSEP();
                    sep.Query.Where(sep.Query.NoSEP == txtNoSep.Text);
                    if (sep.Query.Load())
                    {
                        txtTglSep.SelectedDate = sep.TanggalSEP;
                        txtNoPeserta.Text = sep.NomorKartu;
                        txtNamaPeserta.Text = sep.NamaPasien;
                        txtTglRujukan.SelectedDate = sep.TanggalSEP;
                        cboPelayanan.SelectedValue = sep.JenisPelayanan;

                        var faskes = new List<Common.BPJS.VClaim.v11.Faskes.Faske>();
                        faskes.Add(new Temiang.Avicenna.Common.BPJS.VClaim.v11.Faskes.Faske()
                        {
                            Kode = sep.PPKRujukan,
                            Nama = sep.NamaPPKRujukan
                        });

                        cboPpkDirujuk.DataSource = faskes;
                        cboPpkDirujuk.DataBind();
                        cboPpkDirujuk.SelectedValue = sep.PPKRujukan;

                        var svc = new Common.BPJS.VClaim.v11.Service();
                        var vklaim = svc.GetSep(sep.NoSEP);
                        if (vklaim.MetaData.IsValid)
                        {
                            svc = new Common.BPJS.VClaim.v11.Service();
                            var rujukan = svc.GetRujukan(vklaim.Response.NoRujukan);
                            if (rujukan.MetaData.IsValid)
                            {
                                var selisih = DateTime.Now.Date.Subtract(DateTime.ParseExact(rujukan.Response.Rujukan.TglKunjungan, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None).Date).TotalDays;
                                if (selisih > 90) ScriptManager.RegisterStartupScript(this, GetType(), "cari", "alert('No Rujukan sudah tidak berlaku, lebih dari 90 hari');", true);
                            }
                        }
                    }
                }
            }
        }

        protected override void OnMenuNewClick()
        {
            txtNoRujukan.Text = string.Empty;
            txtTglRujukan.SelectedDate = DateTime.Now.Date;
            txtNoSep.Text = string.Empty;
            txtTglSep.Clear();
            txtNoPeserta.Text = string.Empty;
            txtNamaPeserta.Text = string.Empty;
            txtTglRujukan.Clear();
            txtTglRencanaKunjungan.Clear();
            cboPelayanan.SelectedValue = string.Empty;
            cboTipeRujukan.SelectedValue = string.Empty;

            cboPpkDirujuk.DataSource = null;
            cboPpkDirujuk.DataBind();
            cboPpkDirujuk.Items.Clear();
            cboPpkDirujuk.SelectedValue = string.Empty;
            cboPpkDirujuk.Text = string.Empty;

            cboPoliDirujuk.DataSource = null;
            cboPoliDirujuk.DataBind();
            cboPoliDirujuk.Items.Clear();
            cboPoliDirujuk.SelectedValue = string.Empty;
            cboPoliDirujuk.Text = string.Empty;

            cboDiagnosa.DataSource = null;
            cboDiagnosa.DataBind();
            cboDiagnosa.Items.Clear();
            cboDiagnosa.SelectedValue = string.Empty;
            cboDiagnosa.Text = string.Empty;

            txtCatatan.Text = string.Empty;
        }

        protected override void OnMenuEditClick()
        {
            txtNoSep.ReadOnly = true;
            btnCariPasien.Enabled = false;
            txtTglRujukan.DateInput.ReadOnly = true;
            txtTglRujukan.DatePopupButton.Enabled = false;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var ruj = new BpjsRujukan();
            ruj.Query.Where(ruj.Query.NoSep == txtNoSep.Text);
            if (ruj.Query.Load())
            {
                args.MessageText = "Rujukan atas SEP sudah dibuat";
                args.IsCancel = true;
                return;
            }

            var entity = new BpjsRujukan();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity, args);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new BpjsRujukan();
            if (entity.LoadByPrimaryKey(txtNoSep.Text, txtNoRujukan.Text))
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
            var entity = new BpjsRujukan();
            if (entity.LoadByPrimaryKey(txtNoSep.Text, txtNoRujukan.Text))
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

        private void SetEntityValue(BpjsRujukan br)
        {
            br.NoSep = txtNoSep.Text;
            br.NoRujukan = txtNoRujukan.Text;
            br.TglRujukan = txtTglRujukan.SelectedDate.Value.Date;
            br.TglRencana = txtTglRencanaKunjungan.SelectedDate.Value.Date;
            br.PpkDirujuk = cboPpkDirujuk.SelectedValue;
            br.NamaPpkDirujuk = cboPpkDirujuk.Text;
            br.JnsPelayanan = cboPelayanan.SelectedValue;
            br.Catatan = txtCatatan.Text;
            br.DiagRujukan = cboDiagnosa.SelectedValue;
            br.TipeRujukan = cboTipeRujukan.SelectedValue;
            br.PoliRujukan = cboPoliDirujuk.SelectedValue;
            br.NamaPoliRujukan = cboPoliDirujuk.Text;
            br.User = AppSession.UserLogin.UserID;
            br.LastUpdateDateTime = DateTime.Now;
            br.LastUpdateByUserID = AppSession.UserLogin.UserID;
        }

        private void SaveEntity(BpjsRujukan entity, ValidateArgs args)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();

            using (var trans = new esTransactionScope())
            {
                if (entity.es.IsAdded)
                {
                    svc = new Common.BPJS.VClaim.v11.Service();
                    var vklaim = svc.GetSep(entity.NoSep);
                    if (vklaim.MetaData.IsValid)
                    {
                        svc = new Common.BPJS.VClaim.v11.Service();
                        var rujukan = svc.GetRujukan(vklaim.Response.NoRujukan);
                        if (rujukan.MetaData.IsValid)
                        {
                            var selisih = DateTime.Now.Date.Subtract(DateTime.ParseExact(rujukan.Response.Rujukan.TglKunjungan, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None).Date).TotalDays;
                            if (selisih > 90) ScriptManager.RegisterStartupScript(this, GetType(), "cari", "alert('No Rujukan sudah tidak berlaku, lebih dari 90 hari');", true);
                        }
                    }

                    svc = new Common.BPJS.VClaim.v11.Service();
                    var response = svc.Insert(new Common.BPJS.VClaim.v20.Rujukan.Insert.Request.Root()
                    {
                        Request = new Common.BPJS.VClaim.v20.Rujukan.Insert.Request.TRequest()
                        {
                            TRujukan = new Common.BPJS.VClaim.v20.Rujukan.Insert.Request.TRujukan()
                            {
                                NoSep = entity.NoSep,
                                TglRujukan = entity.TglRujukan.Value.ToString("yyyy-MM-dd"),
                                TglRencanaKunjungan = entity.TglRencana.Value.ToString("yyyy-MM-dd"),
                                PpkDirujuk = entity.PpkDirujuk,
                                JnsPelayanan = entity.JnsPelayanan,
                                Catatan = entity.Catatan,
                                DiagRujukan = entity.DiagRujukan,
                                TipeRujukan = entity.TipeRujukan,
                                PoliRujukan = entity.PoliRujukan,
                                User = entity.User
                            }
                        }
                    });
                    if (response.MetaData.IsValid)
                    {
                        txtNoRujukan.Text = response.Response.Rujukan.NoRujukan;
                        entity.NoRujukan = txtNoRujukan.Text;
                    }
                    else
                    {
                        args.MessageText = string.Format("Code : {0}, Message : {1}", response.MetaData.Code, response.MetaData.Message);
                        args.IsCancel = true;
                        return;
                    }
                }
                else if (entity.es.IsModified)
                {
                    var response = svc.Update(new Common.BPJS.VClaim.v20.Rujukan.Update.Request.Root()
                    {
                        Request = new Common.BPJS.VClaim.v20.Rujukan.Update.Request.TRequest()
                        {
                            TRujukan = new Common.BPJS.VClaim.v20.Rujukan.Update.Request.TRujukan()
                            {
                                NoRujukan = entity.NoRujukan,
                                TglRujukan = entity.TglRujukan.Value.ToString("yyyy-MM-dd"),
                                TglRencanaKunjungan = entity.TglRencana.Value.ToString("yyyy-MM-dd"),
                                PpkDirujuk = entity.PpkDirujuk,
                                JnsPelayanan = entity.JnsPelayanan,
                                Catatan = entity.Catatan,
                                DiagRujukan = entity.DiagRujukan,
                                TipeRujukan = entity.TipeRujukan,
                                PoliRujukan = entity.PoliRujukan,
                                User = entity.User
                            }
                        }
                    });
                    if (!response.MetaData.IsValid)
                    {
                        args.MessageText = string.Format("Code : {0}, Message : {1}", response.MetaData.Code, response.MetaData.Message);
                        args.IsCancel = true;
                        return;
                    }
                }
                else if (entity.es.IsDeleted)
                {
                    var response = svc.Delete(new Common.BPJS.VClaim.v11.Rujukan.Delete.Request.Root()
                    {
                        Request = new Common.BPJS.VClaim.v11.Rujukan.Delete.Request.TRequest()
                        {
                            TRujukan = new Common.BPJS.VClaim.v11.Rujukan.Delete.Request.TRujukan()
                            {
                                NoRujukan = txtNoRujukan.Text,
                                User = AppSession.UserLogin.UserID
                            }
                        }
                    });
                    if (!response.MetaData.IsValid)
                    {
                        args.MessageText = string.Format("Code : {0}, Message : {1}", response.MetaData.Code, response.MetaData.Message);
                        args.IsCancel = true;
                        return;
                    }
                }

                if (!entity.es.IsDeleted)
                {
                    var diag = new Diagnose();
                    if (!diag.LoadByPrimaryKey(entity.DiagRujukan))
                    {
                        diag = new Diagnose()
                        {
                            DiagnoseID = entity.DiagRujukan,
                            DtdNo = "0",
                            DiagnoseName = cboDiagnosa.Text.Replace("'", "`"),
                            IsChronicDisease = false,
                            IsDisease = false,
                            IsActive = true,
                            LastUpdateDateTime = DateTime.Now,
                            LastUpdateByUserID = AppSession.UserLogin.UserID
                        };
                        diag.Save();
                    }
                }

                entity.Save();

                trans.Complete();
            }
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
            auditLogFilter.PrimaryKeyData = string.Format("NoSep='{0}'", txtNoSep.Text);
            auditLogFilter.TableName = "BpjsRujukan";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            btnCariPasien.Enabled = (newVal == AppEnum.DataMode.New);
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new BpjsRujukanQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.NoSep > txtNoSep.Text);
                que.OrderBy(que.NoSep.Ascending);
            }
            else
            {
                que.Where(que.NoSep < txtNoSep.Text);
                que.OrderBy(que.NoSep.Descending);
            }
            var entity = new BpjsRujukan();
            if (entity.Load(que)) OnPopulateEntryControl(entity);
            else OnMenuNewClick();
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new BpjsRujukan();
            if (parameters.Length > 0)
            {
                string sepId = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                {
                    entity.Query.Where(entity.Query.NoSep == sepId);
                    entity.Query.Load();
                }
            }
            else
            {
                entity.Query.Where(entity.Query.NoSep == txtNoSep.Text);
                entity.Query.Load();
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var br = (BpjsRujukan)entity;

            if (br != null)
            {
                txtNoSep.Text = br.NoSep;
                var sep = new BpjsSEP();
                sep.Query.es.Top = 1;
                sep.Query.Where(sep.Query.NoSEP == txtNoSep.Text);
                if (sep.Query.Load())
                {
                    txtTglSep.SelectedDate = sep.TanggalSEP;
                    txtNoPeserta.Text = sep.NomorKartu;
                    txtNamaPeserta.Text = sep.NamaPasien;
                }

                txtNoRujukan.Text = br.NoRujukan;
                txtTglRujukan.SelectedDate = br.TglRujukan;
                txtTglRencanaKunjungan.SelectedDate = br.TglRencana;

                var faskes = new List<Common.BPJS.VClaim.v11.Faskes.Faske>();
                faskes.Add(new Temiang.Avicenna.Common.BPJS.VClaim.v11.Faskes.Faske()
                {
                    Kode = br.PpkDirujuk,
                    Nama = br.NamaPpkDirujuk
                });

                cboPpkDirujuk.DataSource = faskes;
                cboPpkDirujuk.DataBind();
                cboPpkDirujuk.SelectedValue = br.PpkDirujuk;

                cboPelayanan.SelectedValue = br.JnsPelayanan;
                txtCatatan.Text = br.Catatan;

                var diag = new Diagnose();
                diag.LoadByPrimaryKey(br.DiagRujukan);

                var diagnosa = new List<Common.BPJS.VClaim.v11.Diagnosa.Diagnosa2>();
                diagnosa.Add(new Temiang.Avicenna.Common.BPJS.VClaim.v11.Diagnosa.Diagnosa2()
                {
                    Kode = br.DiagRujukan,
                    Nama = diag.DiagnoseName
                });

                cboDiagnosa.DataSource = diagnosa;
                cboDiagnosa.DataBind();
                cboDiagnosa.SelectedValue = br.DiagRujukan;

                cboTipeRujukan.SelectedValue = br.TipeRujukan;

                var poli = new List<Common.BPJS.VClaim.v11.Poli.Poli2>();
                poli.Add(new Temiang.Avicenna.Common.BPJS.VClaim.v11.Poli.Poli2()
                {
                    Kode = br.PoliRujukan,
                    Nama = br.NamaPoliRujukan
                });

                cboPoliDirujuk.DataSource = poli;
                cboPoliDirujuk.DataBind();
                cboPoliDirujuk.SelectedValue = br.PoliRujukan;
            }
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_NoRujukan", txtNoRujukan.Text);
        }
    }
}