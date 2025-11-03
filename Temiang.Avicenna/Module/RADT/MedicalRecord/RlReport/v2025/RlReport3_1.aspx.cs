using System;
using System.Web.Util;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Globalization;
using System.Linq;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord.v2025
{
    public partial class RlReport3_1 : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "#";
            UrlPageList = "RlReportList.aspx";

            ProgramID = AppConstant.Program.RlReportV2025;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                var months = DateTimeFormatInfo.InvariantInfo.MonthNames.Where(m => !string.IsNullOrEmpty(m))
                                                                        .Select((m, i) => new
                                                                        {
                                                                            Month = m,
                                                                            Value = (i + 2) - 1
                                                                        });

                foreach (var m in months)
                {
                    cboPeriodMonthStart.Items.Add(new RadComboBoxItem(m.Month, m.Value.ToString()));
                    cboPeriodMonthEnd.Items.Add(new RadComboBoxItem(m.Month, m.Value.ToString()));
                }

                txtRlMasterReportID.Text = Request.QueryString["rptId"];
                var rpt = new RlMasterReportV2025();
                rpt.LoadByPrimaryKey(txtRlMasterReportID.Text.ToInt());
                txtRlMasterReportNo.Text = rpt.RlMasterReportNo;
                txtRlMasterReportName.Text = rpt.RlMasterReportName;
            }
        }

        //protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        //{
        //    ajax.AddAjaxSetting(btnProcess, txtBor);
        //    ajax.AddAjaxSetting(btnProcess, txtLos);
        //    ajax.AddAjaxSetting(btnProcess, txtBto);
        //    ajax.AddAjaxSetting(btnProcess, txtToi);
        //    ajax.AddAjaxSetting(btnProcess, txtNdr);
        //    ajax.AddAjaxSetting(btnProcess, txtGdr);
        //    ajax.AddAjaxSetting(btnProcess, txtRataKunj);
        //    ajax.AddAjaxSetting(btnProcess, txtRata2);

        //    ajax.AddAjaxSetting(AjaxManager, btnProcess);
        //}

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new RlTxReportV2025());
            txtRlTxReportNo.Text = GetNewTransactionNo();
            txtPeriodYear.Text = DateTime.Now.Year.ToString();
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new RlTxReportV2025();
            if (entity.LoadByPrimaryKey(txtRlTxReportNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            var coll = new RlTxReportV2025Collection();
            coll.Query.Where(coll.Query.RlMasterReportID == txtRlMasterReportID.Text.ToInt(),
                             coll.Query.PeriodMonthStart == cboPeriodMonthStart.SelectedValue,
                             coll.Query.PeriodMonthEnd == cboPeriodMonthEnd.SelectedValue,
                             coll.Query.PeriodYear == txtPeriodYear.Text);
            coll.LoadAll();
            if (coll.Count > 0)
            {
                args.MessageText = "Record with this period has registered.";
                args.IsCancel = true;
                return;
            }

            var detil = new RlTxReport31V2025();
            if (detil.LoadByPrimaryKey(txtRlTxReportNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new RlTxReportV2025();
            entity.AddNew();

            detil = new RlTxReport31V2025();
            detil.AddNew();

            SetEntityValue(entity, detil);
            SaveEntity(entity, detil);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new RlTxReportV2025();
            var detail = new RlTxReport31V2025();

            if (entity.LoadByPrimaryKey(txtRlTxReportNo.Text))
            {
                if (!detail.LoadByPrimaryKey(txtRlTxReportNo.Text))
                {
                    detail = new RlTxReport31V2025();
                    detail.AddNew();
                }
                SetEntityValue(entity, detail);
                SaveEntity(entity, detail);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
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
            auditLogFilter.PrimaryKeyData = string.Format("RlTxReportNo='{0}'", txtRlTxReportNo.Text.Trim());
            auditLogFilter.TableName = "RlTxReportV2025";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            cboPeriodMonthStart.Enabled = (newVal == AppEnum.DataMode.New);
            cboPeriodMonthEnd.Enabled = (newVal == AppEnum.DataMode.New);
            txtPeriodYear.ReadOnly = (newVal != AppEnum.DataMode.New);

            pnlPrint.Visible = newVal == AppEnum.DataMode.Read;
            btnProcess.Enabled = newVal != AppEnum.DataMode.Read;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new RlTxReportV2025();
            if (parameters.Length > 0)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["rptNo"]))
                    entity.LoadByPrimaryKey(Request.QueryString["rptNo"]);
            }
            else
                entity.LoadByPrimaryKey(txtRlTxReportNo.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var rpt = (RlTxReportV2025)entity;
            txtRlTxReportNo.Text = rpt.RlTxReportNo;
            cboPeriodMonthStart.SelectedValue = rpt.PeriodMonthStart;
            cboPeriodMonthEnd.SelectedValue = rpt.PeriodMonthEnd;
            txtPeriodYear.Text = rpt.PeriodYear;

            var rptDetail = new RlTxReport31V2025();
            if (rpt.RlTxReportNo != null)
                rptDetail.LoadByPrimaryKey(rpt.RlTxReportNo);
            txtBorNonIntensif.Value = Convert.ToDouble(rptDetail.BorNonIntensif);
            txtBorICU.Value = Convert.ToDouble(rptDetail.BorICU);
            txtBorNICU.Value = Convert.ToDouble(rptDetail.BorNICU);
            txtBorPICU.Value = Convert.ToDouble(rptDetail.BorPICU);
            txtBorIntensifLainnya.Value = Convert.ToDouble(rptDetail.BorIntensifLainnya);
            txtLosNonIntensif.Value = Convert.ToDouble(rptDetail.LosNonIntensif);
            txtLosICU.Value = Convert.ToDouble(rptDetail.LosICU);
            txtLosNICU.Value = Convert.ToDouble(rptDetail.LosNICU);
            txtLosPICU.Value = Convert.ToDouble(rptDetail.LosPICU);
            txtLosIntensifLainnya.Value = Convert.ToDouble(rptDetail.LosIntensifLainnya);
            txtBtoNonIntensif.Value = Convert.ToDouble(rptDetail.BtoNonIntensif);
            txtBtoICU.Value = Convert.ToDouble(rptDetail.BtoICU);
            txtBtoNICU.Value = Convert.ToDouble(rptDetail.BtoNICU);
            txtBtoPICU.Value = Convert.ToDouble(rptDetail.BtoPICU);
            txtBtoIntensifLainnya.Value = Convert.ToDouble(rptDetail.BtoIntensifLainnya);
            txtToiNonIntensif.Value = Convert.ToDouble(rptDetail.ToiNonIntensif);
            txtToiICU.Value = Convert.ToDouble(rptDetail.ToiICU);
            txtToiNICU.Value = Convert.ToDouble(rptDetail.ToiNICU);
            txtToiPICU.Value = Convert.ToDouble(rptDetail.ToiPICU);
            txtToiIntensifLainnya.Value = Convert.ToDouble(rptDetail.ToiIntensifLainnya);
            txtNdrNonIntensif.Value = Convert.ToDouble(rptDetail.NdrNonIntensif);
            txtNdrICU.Value = Convert.ToDouble(rptDetail.NdrICU);
            txtNdrNICU.Value = Convert.ToDouble(rptDetail.NdrNICU);
            txtNdrPICU.Value = Convert.ToDouble(rptDetail.NdrPICU);
            txtNdrIntensifLainnya.Value = Convert.ToDouble(rptDetail.NdrIntensifLainnya);
            txtGdrNonIntensif.Value = Convert.ToDouble(rptDetail.GdrNonIntensif);
            txtGdrICU.Value = Convert.ToDouble(rptDetail.GdrICU);
            txtGdrNICU.Value = Convert.ToDouble(rptDetail.GdrNICU);
            txtGdrPICU.Value = Convert.ToDouble(rptDetail.GdrPICU);
            txtGdrIntensifLainnya.Value = Convert.ToDouble(rptDetail.GdrIntensifLainnya);
            //txtRataKunj.Value = Convert.ToDouble(rptDetail.RataKunjungan);
            //txtRata2.Value = Convert.ToDouble(rptDetail.RataRata);
        }

        private void SetEntityValue(RlTxReportV2025 entity, RlTxReport31V2025 detail)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
                txtRlTxReportNo.Text = GetNewTransactionNo();

            entity.RlTxReportNo = txtRlTxReportNo.Text;
            entity.RlMasterReportID = txtRlMasterReportID.Text.ToInt();
            entity.PeriodMonthStart = cboPeriodMonthStart.SelectedValue;
            entity.PeriodMonthEnd = cboPeriodMonthEnd.SelectedValue;
            entity.PeriodYear = txtPeriodYear.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            detail.RlTxReportNo = txtRlTxReportNo.Text;
            detail.BorNonIntensif = Convert.ToDecimal(txtBorNonIntensif.Value);
            detail.BorICU = Convert.ToDecimal(txtBorICU.Value);
            detail.BorNICU = Convert.ToDecimal(txtBorNICU.Value);
            detail.BorPICU = Convert.ToDecimal(txtBorPICU.Value);
            detail.BorIntensifLainnya = Convert.ToDecimal(txtBorIntensifLainnya.Value);
            detail.LosNonIntensif = Convert.ToDecimal(txtLosNonIntensif.Value);
            detail.LosICU = Convert.ToDecimal(txtLosICU.Value);
            detail.LosNICU = Convert.ToDecimal(txtLosNICU.Value);
            detail.LosPICU = Convert.ToDecimal(txtLosPICU.Value);
            detail.LosIntensifLainnya = Convert.ToDecimal(txtLosIntensifLainnya.Value);
            detail.BtoNonIntensif = Convert.ToDecimal(txtBtoNonIntensif.Value);
            detail.BtoICU = Convert.ToDecimal(txtBtoICU.Value);
            detail.BtoNICU = Convert.ToDecimal(txtBtoNICU.Value);
            detail.BtoPICU = Convert.ToDecimal(txtBtoPICU.Value);
            detail.BtoIntensifLainnya = Convert.ToDecimal(txtBtoIntensifLainnya.Value);
            detail.ToiNonIntensif = Convert.ToDecimal(txtToiNonIntensif.Value);
            detail.ToiICU = Convert.ToDecimal(txtToiICU.Value);
            detail.ToiNICU = Convert.ToDecimal(txtToiNICU.Value);
            detail.ToiPICU = Convert.ToDecimal(txtToiPICU.Value);
            detail.ToiIntensifLainnya = Convert.ToDecimal(txtToiIntensifLainnya.Value);
            detail.NdrNonIntensif = Convert.ToDecimal(txtNdrNonIntensif.Value);
            detail.NdrICU = Convert.ToDecimal(txtNdrICU.Value);
            detail.NdrNICU = Convert.ToDecimal(txtNdrNICU.Value);
            detail.NdrPICU = Convert.ToDecimal(txtNdrPICU.Value);
            detail.NdrIntensifLainnya = Convert.ToDecimal(txtNdrIntensifLainnya.Value);
            detail.GdrNonIntensif = Convert.ToDecimal(txtGdrNonIntensif.Value);
            detail.GdrICU = Convert.ToDecimal(txtGdrICU.Value);
            detail.GdrNICU = Convert.ToDecimal(txtGdrNICU.Value);
            detail.GdrPICU = Convert.ToDecimal(txtGdrPICU.Value);
            detail.GdrIntensifLainnya = Convert.ToDecimal(txtGdrIntensifLainnya.Value);
            //detail.RataKunjungan = Convert.ToDecimal(txtRataKunj.Value);
            //detail.RataRata = Convert.ToDecimal(txtRata2.Value);

            //Last Update Status
            if (detail.es.IsAdded || detail.es.IsModified)
            {
                detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                detail.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(RlTxReportV2025 entity, RlTxReport31V2025 detail)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                entity.Save();
                detail.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new RlTxReportV2025Query();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.RlTxReportNo > txtRlTxReportNo.Text, que.RlMasterReportID == txtRlMasterReportID.Text.ToInt());
                que.OrderBy(que.RlTxReportNo.Ascending);
            }
            else
            {
                que.Where(que.RlTxReportNo < txtRlTxReportNo.Text, que.RlMasterReportID == txtRlMasterReportID.Text.ToInt());
                que.OrderBy(que.RlTxReportNo.Descending);
            }
            var entity = new RlTxReportV2025();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);
            return _autoNumber.LastCompleteNumber;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(source is RadGrid))
                return;

            switch (eventArgument)
            {
                case "process":
                    Validate();
                    if (!IsValid)
                        return;

                    break;
            }
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            int result = RlTxReport31ItemV2025.ProcessInsert(cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue, txtPeriodYear.Text, AppSession.UserLogin.UserID);

            double jmHariPerawatanNonIntensif = 0;
            double jmHariPerawatanICU = 0;
            double jmHariPerawatanNICU = 0;
            double jmHariPerawatanPICU = 0;
            double jmHariPerawatanIntensifLainnya = 0;
            double jmTtNonIntensif = 0;
            double jmTtICU = 0;
            double jmTtNICU = 0;
            double jmTtPICU = 0;
            double jmTtIntensifLainnya = 0;
            //double jmHariDlmSatuPeriode = 0;
            double jmJttNonIntensif = 0;
            double jmJttICU = 0;
            double jmJttNICU = 0;
            double jmJttPICU = 0;
            double jmJttIntensifLainnya = 0;
            double jmLamaDiRawatNonIntensif = 0;
            double jmLamaDiRawatICU = 0;
            double jmLamaDiRawatNICU = 0;
            double jmLamaDiRawatPICU = 0;
            double jmLamaDiRawatIntensifLainnya = 0;
            double jmPasienKeluarNonIntensif = 0;
            double jmPasienKeluarICU = 0;
            double jmPasienKeluarNICU = 0;
            double jmPasienKeluarPICU = 0;
            double jmPasienKeluarIntensifLainnya = 0;
            double jmPasienMatiNonIntensif = 0;
            double jmPasienMatiICU = 0;
            double jmPasienMatiNICU = 0;
            double jmPasienMatiPICU = 0;
            double jmPasienMatiIntensifLainnya = 0;
            double jmPasienMati48NonIntensif = 0;
            double jmPasienMati48ICU = 0;
            double jmPasienMati48NICU = 0;
            double jmPasienMati48PICU = 0;
            double jmPasienMati48IntensifLainnya = 0;
            //double jmKunjunganPoli = 0;

            var rl = new RlTxReport31ItemV2025();
            if (rl.LoadByPrimaryKey(cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue, txtPeriodYear.Text))
            {
                jmHariPerawatanNonIntensif = Convert.ToDouble(rl.HariPerawatanNonIntensif);
                jmHariPerawatanICU = Convert.ToDouble(rl.HariPerawatanICU);
                jmHariPerawatanNICU = Convert.ToDouble(rl.HariPerawatanNICU);
                jmHariPerawatanPICU = Convert.ToDouble(rl.HariPerawatanPICU);
                jmHariPerawatanIntensifLainnya = Convert.ToDouble(rl.HariPerawatanIntensifLainnya);
                jmTtNonIntensif = Convert.ToDouble(rl.TtNonIntensif);
                jmTtICU = Convert.ToDouble(rl.TtICU);
                jmTtNICU = Convert.ToDouble(rl.TtNICU);
                jmTtPICU = Convert.ToDouble(rl.TtPICU);
                jmTtIntensifLainnya = Convert.ToDouble(rl.TtIntensifLainnya);
                //jmHariDlmSatuPeriode = Convert.ToDouble(rl.HariDlmSatuPeriode);
                jmJttNonIntensif = Convert.ToDouble(rl.JTtNonIntensif);
                jmJttICU = Convert.ToDouble(rl.JTtICU);
                jmJttNICU = Convert.ToDouble(rl.JTtNICU);
                jmJttPICU = Convert.ToDouble(rl.JTtPICU);
                jmJttIntensifLainnya = Convert.ToDouble(rl.JTtIntensifLainnya);
                jmLamaDiRawatNonIntensif = Convert.ToDouble(rl.LamaDirawatNonIntensif);
                jmLamaDiRawatICU = Convert.ToDouble(rl.LamaDirawatICU);
                jmLamaDiRawatNICU = Convert.ToDouble(rl.LamaDirawatNICU);
                jmLamaDiRawatPICU = Convert.ToDouble(rl.LamaDirawatPICU);
                jmLamaDiRawatIntensifLainnya = Convert.ToDouble(rl.LamaDirawatIntensifLainnya);
                jmPasienKeluarNonIntensif = Convert.ToDouble(rl.KeluarNonIntensif);
                jmPasienKeluarICU = Convert.ToDouble(rl.KeluarICU);
                jmPasienKeluarNICU = Convert.ToDouble(rl.KeluarNICU);
                jmPasienKeluarPICU = Convert.ToDouble(rl.KeluarPICU);
                jmPasienKeluarIntensifLainnya = Convert.ToDouble(rl.KeluarIntensifLainnya);
                jmPasienMatiNonIntensif = Convert.ToDouble(rl.KeluarMatiNonIntensif);
                jmPasienMatiICU = Convert.ToDouble(rl.KeluarMatiICU);
                jmPasienMatiNICU = Convert.ToDouble(rl.KeluarMatiNICU);
                jmPasienMatiPICU = Convert.ToDouble(rl.KeluarMatiPICU);
                jmPasienMatiIntensifLainnya = Convert.ToDouble(rl.KeluarMatiIntensifLainnya);
                jmPasienMati48NonIntensif = Convert.ToDouble(rl.KeluarMati48NonIntensif);
                jmPasienMati48ICU = Convert.ToDouble(rl.KeluarMati48ICU);
                jmPasienMati48NICU = Convert.ToDouble(rl.KeluarMati48NICU);
                jmPasienMati48PICU = Convert.ToDouble(rl.KeluarMati48PICU);
                jmPasienMati48IntensifLainnya = Convert.ToDouble(rl.KeluarMati48IntensifLainnya);
                //jmKunjunganPoli = Convert.ToDouble(rl.Kunjungan);
            }

            //txtBorNonIntensif.Value = (jmHariPerawatanNonIntensif / jmJttNonIntensif) * 100;
            //txtBorICU.Value = (jmHariPerawatanICU / jmJttICU) * 100;
            //txtBorNICU.Value = (jmHariPerawatanNICU / jmJttNICU) * 100;
            //txtBorPICU.Value = (jmHariPerawatanPICU / jmJttPICU) * 100;
            //txtBorIntensifLainnya.Value = (jmHariPerawatanIntensifLainnya / jmJttIntensifLainnya) * 100;
            //txtLosNonIntensif.Value = jmLamaDiRawatNonIntensif / jmPasienKeluarNonIntensif;
            //txtLosICU.Value = jmLamaDiRawatICU / jmPasienKeluarICU;
            //txtLosNICU.Value = jmLamaDiRawatNICU / jmPasienKeluarNICU;
            //txtLosPICU.Value = jmLamaDiRawatPICU / jmPasienKeluarPICU;
            //txtLosIntensifLainnya.Value = jmLamaDiRawatIntensifLainnya / jmPasienKeluarIntensifLainnya;
            //txtBtoNonIntensif.Value = jmPasienKeluarNonIntensif / jmTtNonIntensif;
            //txtBtoICU.Value = jmPasienKeluarICU / jmTtICU;
            //txtBtoNICU.Value = jmPasienKeluarNICU / jmTtNICU;
            //txtBtoPICU.Value = jmPasienKeluarPICU / jmTtPICU;
            //txtBtoIntensifLainnya.Value = jmPasienKeluarIntensifLainnya / jmTtIntensifLainnya;
            //txtToiNonIntensif.Value = (jmJttNonIntensif - jmHariPerawatanNonIntensif) / jmPasienKeluarNonIntensif;
            //txtToiICU.Value = (jmJttICU - jmHariPerawatanICU) / jmPasienKeluarICU;
            //txtToiNICU.Value = (jmJttNICU - jmHariPerawatanNICU) / jmPasienKeluarNICU;
            //txtToiPICU.Value = (jmJttPICU - jmHariPerawatanPICU) / jmPasienKeluarPICU;
            //txtToiIntensifLainnya.Value = (jmJttIntensifLainnya - jmHariPerawatanIntensifLainnya) / jmPasienKeluarIntensifLainnya;
            //txtNdrNonIntensif.Value = (jmPasienMati48NonIntensif / jmPasienKeluarNonIntensif) * 1000;
            //txtNdrICU.Value = (jmPasienMati48ICU / jmPasienKeluarICU) * 1000;
            //txtNdrNICU.Value = (jmPasienMati48NICU / jmPasienKeluarNICU) * 1000;
            //txtNdrPICU.Value = (jmPasienMati48PICU / jmPasienKeluarPICU) * 1000;
            //txtNdrIntensifLainnya.Value = (jmPasienMati48IntensifLainnya / jmPasienKeluarIntensifLainnya) * 1000;
            //txtGdrNonIntensif.Value = (jmPasienMatiNonIntensif / jmPasienKeluarNonIntensif) * 1000;
            //txtGdrICU.Value = (jmPasienMatiICU / jmPasienKeluarICU) * 1000;
            //txtGdrNICU.Value = (jmPasienMatiNICU / jmPasienKeluarNICU) * 1000;
            //txtGdrPICU.Value = (jmPasienMatiPICU / jmPasienKeluarPICU) * 1000;
            //txtGdrIntensifLainnya.Value = (jmPasienMatiIntensifLainnya / jmPasienKeluarIntensifLainnya) * 1000;
            ////txtRataKunj.Value = jmKunjunganPoli / jmHariDlmSatuPeriode;
            //txtRata2.Value = 0;

            txtBorNonIntensif.Value = jmJttNonIntensif != 0 ? (jmHariPerawatanNonIntensif / jmJttNonIntensif) * 100 : 0;
            txtBorICU.Value = jmJttICU != 0 ? (jmHariPerawatanICU / jmJttICU) * 100 : 0;
            txtBorNICU.Value = jmJttNICU != 0 ? (jmHariPerawatanNICU / jmJttNICU) * 100 : 0;
            txtBorPICU.Value = jmJttPICU != 0 ? (jmHariPerawatanPICU / jmJttPICU) * 100 : 0;
            txtBorIntensifLainnya.Value = jmJttIntensifLainnya != 0 ? (jmHariPerawatanIntensifLainnya / jmJttIntensifLainnya) * 100 : 0;
            txtLosNonIntensif.Value = jmPasienKeluarNonIntensif != 0 ? jmLamaDiRawatNonIntensif / jmPasienKeluarNonIntensif : 0;
            txtLosICU.Value = jmPasienKeluarICU != 0 ? jmLamaDiRawatICU / jmPasienKeluarICU : 0;
            txtLosNICU.Value = jmPasienKeluarNICU != 0 ? jmLamaDiRawatNICU / jmPasienKeluarNICU : 0;
            txtLosPICU.Value = jmPasienKeluarPICU != 0 ? jmLamaDiRawatPICU / jmPasienKeluarPICU : 0;
            txtLosIntensifLainnya.Value = jmPasienKeluarIntensifLainnya != 0 ? jmLamaDiRawatIntensifLainnya / jmPasienKeluarIntensifLainnya : 0;
            txtBtoNonIntensif.Value = jmTtNonIntensif != 0 ? jmPasienKeluarNonIntensif / jmTtNonIntensif : 0;
            txtBtoICU.Value = jmTtICU != 0 ? jmPasienKeluarICU / jmTtICU : 0;
            txtBtoNICU.Value = jmTtNICU != 0 ? jmPasienKeluarNICU / jmTtNICU : 0;
            txtBtoPICU.Value = jmTtPICU != 0 ? jmPasienKeluarPICU / jmTtPICU : 0;
            txtBtoIntensifLainnya.Value = jmTtIntensifLainnya != 0 ? jmPasienKeluarIntensifLainnya / jmTtIntensifLainnya : 0;
            txtToiNonIntensif.Value = jmPasienKeluarNonIntensif != 0 ? (jmJttNonIntensif - jmHariPerawatanNonIntensif) / jmPasienKeluarNonIntensif : 0;
            txtToiICU.Value = jmPasienKeluarICU != 0 ? (jmJttICU - jmHariPerawatanICU) / jmPasienKeluarICU : 0;
            txtToiNICU.Value = jmPasienKeluarNICU != 0 ? (jmJttNICU - jmHariPerawatanNICU) / jmPasienKeluarNICU : 0;
            txtToiPICU.Value = jmPasienKeluarPICU != 0 ? (jmJttPICU - jmHariPerawatanPICU) / jmPasienKeluarPICU : 0;
            txtToiIntensifLainnya.Value = jmPasienKeluarIntensifLainnya != 0 ? (jmJttIntensifLainnya - jmHariPerawatanIntensifLainnya) / jmPasienKeluarIntensifLainnya : 0;
            txtNdrNonIntensif.Value = jmPasienKeluarNonIntensif != 0 ? (jmPasienMati48NonIntensif / jmPasienKeluarNonIntensif) * 1000 : 0;
            txtNdrICU.Value = jmPasienKeluarICU != 0 ? (jmPasienMati48ICU / jmPasienKeluarICU) * 1000 : 0;
            txtNdrNICU.Value = jmPasienKeluarNICU != 0 ? (jmPasienMati48NICU / jmPasienKeluarNICU) * 1000 : 0;
            txtNdrPICU.Value = jmPasienKeluarPICU != 0 ? (jmPasienMati48PICU / jmPasienKeluarPICU) * 1000 : 0;
            txtNdrIntensifLainnya.Value = jmPasienKeluarIntensifLainnya != 0 ? (jmPasienMati48IntensifLainnya / jmPasienKeluarIntensifLainnya) * 1000 : 0;
            txtGdrNonIntensif.Value = jmPasienKeluarNonIntensif != 0 ? (jmPasienMatiNonIntensif / jmPasienKeluarNonIntensif) * 1000 : 0;
            txtGdrICU.Value = jmPasienKeluarICU != 0 ? (jmPasienMatiICU / jmPasienKeluarICU) * 1000 : 0;
            txtGdrNICU.Value = jmPasienKeluarNICU != 0 ? (jmPasienMatiNICU / jmPasienKeluarNICU) * 1000 : 0;
            txtGdrPICU.Value = jmPasienKeluarPICU != 0 ? (jmPasienMatiPICU / jmPasienKeluarPICU) * 1000 : 0;
            txtGdrIntensifLainnya.Value = jmPasienKeluarIntensifLainnya != 0 ? (jmPasienMatiIntensifLainnya / jmPasienKeluarIntensifLainnya) * 1000 : 0;
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            var jobParameters = new PrintJobParameterCollection();

            var jobParameter = jobParameters.AddNew();
            jobParameter.Name = "RlTxReportNo";
            jobParameter.ValueString = txtRlTxReportNo.Text;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = AppConstant.Report.RL3_1V2025;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                            "oWnd.Show();" +
                            "oWnd.Maximize();";
            ajxPanel.ResponseScripts.Add(script);
        }
    }
}
