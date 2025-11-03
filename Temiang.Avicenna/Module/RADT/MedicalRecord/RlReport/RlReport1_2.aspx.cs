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

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class RlReport1_2 : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "#";
            UrlPageList = "RlReportList.aspx";

            ProgramID = AppConstant.Program.RlReport;

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
                }

                txtRlMasterReportID.Text = Request.QueryString["rptId"];
                var rpt = new RlMasterReport();
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
            OnPopulateEntryControl(new RlTxReport());
            txtRlTxReportNo.Text = GetNewTransactionNo();
            txtPeriodYear.Text = DateTime.Now.Year.ToString();
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new RlTxReport();
            if (entity.LoadByPrimaryKey(txtRlTxReportNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            var coll = new RlTxReportCollection();
            coll.Query.Where(coll.Query.RlMasterReportID == txtRlMasterReportID.Text.ToInt(),
                             coll.Query.PeriodMonthStart == cboPeriodMonthStart.SelectedValue,
                             coll.Query.PeriodYear == txtPeriodYear.Text);
            coll.LoadAll();
            if (coll.Count > 0)
            {
                args.MessageText = "Record with this period has registered.";
                args.IsCancel = true;
                return;
            }

            var detil = new RlTxReport12();
            if (detil.LoadByPrimaryKey(txtRlTxReportNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new RlTxReport();
            entity.AddNew();

            detil = new RlTxReport12();
            detil.AddNew();

            SetEntityValue(entity, detil);
            SaveEntity(entity, detil);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new RlTxReport();
            var detail = new RlTxReport12();

            if (entity.LoadByPrimaryKey(txtRlTxReportNo.Text))
            {
                if (!detail.LoadByPrimaryKey(txtRlTxReportNo.Text))
                {
                    detail = new RlTxReport12();
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
            auditLogFilter.TableName = "RlTxReport";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            cboPeriodMonthStart.Enabled = (newVal == AppEnum.DataMode.New);
            txtPeriodYear.ReadOnly = (newVal != AppEnum.DataMode.New);

            pnlPrint.Visible = newVal == AppEnum.DataMode.Read;
            btnProcess.Enabled = newVal != AppEnum.DataMode.Read;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new RlTxReport();
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
            var rpt = (RlTxReport)entity;
            txtRlTxReportNo.Text = rpt.RlTxReportNo;
            cboPeriodMonthStart.SelectedValue = rpt.PeriodMonthStart;
            txtPeriodYear.Text = rpt.PeriodYear;

            var rptDetail = new RlTxReport12();
            if (rpt.RlTxReportNo != null)
                rptDetail.LoadByPrimaryKey(rpt.RlTxReportNo);
            txtBor.Value = Convert.ToDouble(rptDetail.Bor);
            txtLos.Value = Convert.ToDouble(rptDetail.Los);
            txtBto.Value = Convert.ToDouble(rptDetail.Bto);
            txtToi.Value = Convert.ToDouble(rptDetail.Toi);
            txtNdr.Value = Convert.ToDouble(rptDetail.Ndr);
            txtGdr.Value = Convert.ToDouble(rptDetail.Gdr);
            txtRataKunj.Value = Convert.ToDouble(rptDetail.RataKunjungan);
            txtRata2.Value = Convert.ToDouble(rptDetail.RataRata);
        }

        private void SetEntityValue(RlTxReport entity, RlTxReport12 detail)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
                txtRlTxReportNo.Text = GetNewTransactionNo();

            entity.RlTxReportNo = txtRlTxReportNo.Text;
            entity.RlMasterReportID = txtRlMasterReportID.Text.ToInt();
            entity.PeriodMonthStart = cboPeriodMonthStart.SelectedValue;
            entity.PeriodMonthEnd = cboPeriodMonthStart.SelectedValue;
            entity.PeriodYear = txtPeriodYear.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            detail.RlTxReportNo = txtRlTxReportNo.Text;
            detail.Bor = Convert.ToDecimal(txtBor.Value);
            detail.Los = Convert.ToDecimal(txtLos.Value);
            detail.Bto = Convert.ToDecimal(txtBto.Value);
            detail.Toi = Convert.ToDecimal(txtToi.Value);
            detail.Ndr = Convert.ToDecimal(txtNdr.Value);
            detail.Gdr = Convert.ToDecimal(txtGdr.Value);
            detail.RataKunjungan = Convert.ToDecimal(txtRataKunj.Value);
            detail.RataRata = Convert.ToDecimal(txtRata2.Value);

            //Last Update Status
            if (detail.es.IsAdded || detail.es.IsModified)
            {
                detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                detail.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(RlTxReport entity, RlTxReport12 detail)
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
            var que = new RlTxReportQuery();
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
            var entity = new RlTxReport();
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
            int result = RlTxReport12Item.ProcessInsert(cboPeriodMonthStart.SelectedValue, txtPeriodYear.Text, AppSession.UserLogin.UserID);

            double jmHariPerawatan = 0;
            double jmTt = 0;
            double jmHariDlmSatuPeriode = 0;
            double jmJtt = 0;
            double jmLamaDiRawat = 0;
            double jmPasienKeluar = 0;
            double jmPasienMati = 0;
            double jmPasienMati48 = 0;
            double jmKunjunganPoli = 0;

            var rl = new RlTxReport12Item();
            if (rl.LoadByPrimaryKey(cboPeriodMonthStart.SelectedValue, txtPeriodYear.Text))
            {
                jmHariPerawatan = Convert.ToDouble(rl.HariPerawatan);
                jmTt = Convert.ToDouble(rl.Tt);
                jmHariDlmSatuPeriode = Convert.ToDouble(rl.HariDlmSatuPeriode);
                jmJtt = Convert.ToDouble(rl.JTt);
                jmLamaDiRawat = Convert.ToDouble(rl.LamaDirawat);
                jmPasienKeluar = Convert.ToDouble(rl.Keluar);
                jmPasienMati = Convert.ToDouble(rl.KeluarMati);
                jmPasienMati48 = Convert.ToDouble(rl.KeluarMati48);
                jmKunjunganPoli = Convert.ToDouble(rl.Kunjungan);
            }

            txtBor.Value = (jmHariPerawatan / jmJtt) * 100;//(jmHariPerawatan/(jmTt*jmHariDlmSatuPeriode))*100;
            txtLos.Value = jmLamaDiRawat / jmPasienKeluar;
            txtBto.Value = jmPasienKeluar / jmTt;
            txtToi.Value = (jmJtt - jmHariPerawatan) / jmPasienKeluar;//((jmTt*jmHariDlmSatuPeriode) - jmHariPerawatan)/jmPasienKeluar;
            txtNdr.Value = (jmPasienMati48 / jmPasienKeluar) * 1000;
            txtGdr.Value = (jmPasienMati / jmPasienKeluar) * 1000;
            txtRataKunj.Value = jmKunjunganPoli / jmHariDlmSatuPeriode;
            txtRata2.Value = 0;
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            var jobParameters = new PrintJobParameterCollection();

            var jobParameter = jobParameters.AddNew();
            jobParameter.Name = "RlTxReportNo";
            jobParameter.ValueString = txtRlTxReportNo.Text;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = AppConstant.Report.RL1_2;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                            "oWnd.Show();" +
                            "oWnd.Maximize();";
            ajxPanel.ResponseScripts.Add(script);
        }
    }
}
