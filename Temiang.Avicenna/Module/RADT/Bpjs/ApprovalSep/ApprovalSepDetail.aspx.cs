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
    public partial class ApprovalSepDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ApprovalSepSearch.aspx";
            UrlPageList = "ApprovalSepList.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.BpjsApproval;
        }

        protected override void OnMenuNewClick()
        {
            txtNoPeserta.Text = string.Empty;
            txtTglSep.SelectedDate = DateTime.Now.Date;
            txtNamaPeserta.Text = string.Empty;
            cboPelayanan.SelectedValue = string.Empty;
            cboJenisPengajuan.SelectedValue = string.Empty;
            txtKeterangan.Text = string.Empty;
        }

        protected override void OnMenuEditClick()
        {
            txtNoPeserta.ReadOnly = true;
            btnCariPasien.Enabled = false;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var ruj = new BpjsApproval();
            if (ruj.LoadByPrimaryKey(txtNoPeserta.Text, txtTglSep.SelectedDate.Value.Date, cboPelayanan.SelectedValue))
            {
                args.MessageText = "Pengajuan atas no peserta sudah dibuat";
                args.IsCancel = true;
                return;
            }

            var entity = new BpjsApproval();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity, args);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new BpjsApproval();
            if (entity.LoadByPrimaryKey(txtNoPeserta.Text, txtTglSep.SelectedDate.Value.Date, cboPelayanan.SelectedValue))
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
            var entity = new BpjsApproval();
            if (entity.LoadByPrimaryKey(txtNoPeserta.Text, txtTglSep.SelectedDate.Value.Date, cboPelayanan.SelectedValue))
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

        private void SetEntityValue(BpjsApproval br)
        {
            br.NoKartu = txtNoPeserta.Text;
            br.TglSep = txtTglSep.SelectedDate.Value.Date;
            br.JnsPelayanan = cboPelayanan.SelectedValue;
            br.JnsPengajuan = cboJenisPengajuan.SelectedValue;
            br.NamaPasien = txtNamaPeserta.Text;
            br.JenisKelamin = rbtSex.SelectedValue;
            br.Keterangan = txtKeterangan.Text;
            br.User = AppSession.UserLogin.UserID;
            br.LastUpdateDateTime = DateTime.Now;
            br.LastUpdateByUserID = AppSession.UserLogin.UserID;
        }

        private void SaveEntity(BpjsApproval entity, ValidateArgs args)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();

            using (var trans = new esTransactionScope())
            {
                if (entity.es.IsAdded)
                {
                    var response = svc.Insert(new Common.BPJS.VClaim.v11.Sep.Pengajuan.Request.Root()
                    {
                        Request = new Common.BPJS.VClaim.v11.Sep.Pengajuan.Request.TRequest()
                        {
                            TSep = new Common.BPJS.VClaim.v11.Sep.Pengajuan.Request.TSep()
                            {
                                NoKartu = entity.NoKartu,
                                TglSep = entity.TglSep.Value.ToString("yyyy-MM-dd"),
                                JnsPelayanan = entity.JnsPelayanan,
                                JnsPengajuan = entity.JnsPengajuan,
                                Keterangan = entity.Keterangan,
                                User = entity.User
                            }
                        }
                    });
                    if (!response.Metadata.IsValid)
                    {
                        args.MessageText = string.Format("Code : {0}, Message : {1}", response.Metadata.Code, response.Metadata.Message);
                        args.IsCancel = true;
                        return;
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
            auditLogFilter.PrimaryKeyData = string.Format("NoPeserta='{0}'", txtNoPeserta.Text);
            auditLogFilter.TableName = "BpjsApproval";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            btnCariPasien.Enabled = (newVal == AppEnum.DataMode.New);
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new BpjsApprovalQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.NoKartu > txtNoPeserta.Text);
                que.OrderBy(que.NoKartu.Ascending);
            }
            else
            {
                que.Where(que.NoKartu < txtNoPeserta.Text);
                que.OrderBy(que.NoKartu.Ascending);
            }
            var entity = new BpjsApproval();
            if (entity.Load(que)) OnPopulateEntryControl(entity);
            else OnMenuNewClick();
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new BpjsApproval();
            if (parameters.Length > 0)
            {
                string id1 = parameters[0];

                string format = "yyyy-MM-dd";
                DateTime parsed;
                DateTime.TryParseExact(parameters[1], format, null, System.Globalization.DateTimeStyles.None, out parsed);

                string id3 = parameters[2];

                if (!parameters[0].Equals(string.Empty)) entity.LoadByPrimaryKey(id1, parsed, id3);
            }
            else
                entity.LoadByPrimaryKey(txtNoPeserta.Text, txtTglSep.SelectedDate.Value.Date, cboPelayanan.SelectedValue);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var br = (BpjsApproval)entity;

            if (br != null)
            {
                txtNoPeserta.Text = br.NoKartu;
                txtTglSep.SelectedDate = br.TglSep.Value.Date;
                cboPelayanan.SelectedValue = br.JnsPelayanan;
                cboJenisPengajuan.SelectedValue = br.JnsPengajuan;
                txtNamaPeserta.Text = br.NamaPasien;
                rbtSex.SelectedValue = br.JenisKelamin;
                txtKeterangan.Text = br.Keterangan;
                ViewState["IsApproved"] = br.IsApproved ?? false;
            }
        }

        protected void btnCariPasien_Click(object sender, ImageClickEventArgs e)
        {
            HideInformationHeader();

            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetPeserta(Common.BPJS.VClaim.Enum.SearchPeserta.NoPeserta, txtNoPeserta.Text, txtTglSep.SelectedDate.Value.Date);
            if (response.MetaData.IsValid)
            {
                txtNamaPeserta.Text = response.Response.Peserta.Nama;
                rbtSex.SelectedValue = response.Response.Peserta.Sex;
            }
            else ScriptManager.RegisterStartupScript(this, GetType(), "cari", string.Format("alert('Code : {0}, Message : {1}');", response.MetaData.Code, response.MetaData.Message), true);
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new BpjsApproval();
            if (!entity.LoadByPrimaryKey(txtNoPeserta.Text, txtTglSep.SelectedDate.Value.Date, cboPelayanan.SelectedValue))
            {
                args.MessageText = "Pengajuan SEP telah di approve";
                args.IsCancel = true;
                return;
            }

            var svc = new Common.BPJS.VClaim.v11.Service();

            using (var trans = new esTransactionScope())
            {
                var response = svc.Update(new Common.BPJS.VClaim.v11.Sep.Approval.Request.Root()
                {
                    Request = new Common.BPJS.VClaim.v11.Sep.Approval.Request.TRequest()
                    {
                        TSep = new Common.BPJS.VClaim.v11.Sep.Approval.Request.TSep()
                        {
                            NoKartu = entity.NoKartu,
                            TglSep = entity.TglSep.Value.ToString("yyyy-MM-dd"),
                            JnsPelayanan = entity.JnsPelayanan,
                            JnsPengajuan = entity.JnsPengajuan,
                            Keterangan = entity.Keterangan,
                            User = entity.User
                        }
                    }
                });
                if (!response.Metadata.IsValid)
                {
                    args.MessageText = string.Format("Code : {0}, Message : {1}", response.Metadata.Code, response.Metadata.Message);
                    args.IsCancel = true;
                    return;
                }

                entity.IsApproved = true;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.Save();

                trans.Complete();
            }
        }
    }
}