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
    public partial class RlReport3_12 : BasePageDetail
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
                    cboPeriodMonthEnd.Items.Add(new RadComboBoxItem(m.Month, m.Value.ToString()));
                }

                txtRlMasterReportID.Text = Request.QueryString["rptId"];
                var rpt = new RlMasterReport();
                rpt.LoadByPrimaryKey(txtRlMasterReportID.Text.ToInt());
                txtRlMasterReportNo.Text = rpt.RlMasterReportNo;
                txtRlMasterReportName.Text = rpt.RlMasterReportName;
            }
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new RlTxReport());
            txtRlTxReportNo.Text = GetNewTransactionNo();
            txtPeriodYear.Text = DateTime.Now.Year.ToString();
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboPeriodMonthStart.SelectedValue) || string.IsNullOrEmpty(cboPeriodMonthEnd.SelectedValue))
            {
                args.MessageText = "Period Month required.";
                args.IsCancel = true;
                return;
            }

            if (Convert.ToInt16(cboPeriodMonthStart.SelectedValue) > Convert.ToInt16(cboPeriodMonthEnd.SelectedValue))
            {
                args.MessageText = "Start Month can't be greater than End Month.";
                args.IsCancel = true;
                return;
            }

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
                             coll.Query.PeriodMonthEnd == cboPeriodMonthEnd.SelectedValue, coll.Query.PeriodYear == txtPeriodYear.Text);
            coll.LoadAll();
            if (coll.Count > 0)
            {
                args.MessageText = "Record with this period has registered.";
                args.IsCancel = true;
                return;
            }

            entity = new RlTxReport();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new RlTxReport();
            if (entity.LoadByPrimaryKey(txtRlTxReportNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
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
            cboPeriodMonthEnd.Enabled = (newVal == AppEnum.DataMode.New);
            txtPeriodYear.ReadOnly = (newVal != AppEnum.DataMode.New);

            pnlPrint.Visible = newVal == AppEnum.DataMode.Read;
            RefreshCommandItemGrid(oldVal, newVal);
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
            cboPeriodMonthEnd.SelectedValue = rpt.PeriodMonthEnd;
            txtPeriodYear.Text = rpt.PeriodYear;
        }

        private void SetEntityValue(RlTxReport entity)
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
        }

        private void SaveEntity(RlTxReport entity)
        {
            var coll = new RlTxReport312Collection();
            coll.Query.Where(coll.Query.RlTxReportNo == entity.RlTxReportNo);
            coll.LoadAll();
            coll.MarkAllAsDeleted();
            coll.Save();

            var newcoll = new RlTxReport312Collection();

            foreach (GridDataItem dataItem in grdRlReport3_12.MasterTableView.Items)
            {
                var reportItemId = dataItem.GetDataKeyValue("RlMasterReportItemID").ToInt();
                var txtKonselingAnc = (dataItem.FindControl("txtKonselingAnc") as RadNumericTextBox);
                var txtKonselingPascaPersalinan = (dataItem.FindControl("txtKonselingPascaPersalinan") as RadNumericTextBox);
                var txtKbBaruCmBukanRujukan = (dataItem.FindControl("txtKbBaruCmBukanRujukan") as RadNumericTextBox);
                var txtKbBaruCmRujukanRi = (dataItem.FindControl("txtKbBaruCmRujukanRi") as RadNumericTextBox);
                var txtKbBaruCmRujukanRj = (dataItem.FindControl("txtKbBaruCmRujukanRj") as RadNumericTextBox);
                var txtKbBaruCmTotal = (dataItem.FindControl("txtKbBaruCmTotal") as RadNumericTextBox);
                var txtKbBaruDkNifas = (dataItem.FindControl("txtKbBaruDkNifas") as RadNumericTextBox);
                var txtKbBaruDkAbortus = (dataItem.FindControl("txtKbBaruDkAbortus") as RadNumericTextBox);
                var txtKbBaruDkLain = (dataItem.FindControl("txtKbBaruDkLain") as RadNumericTextBox);
                var txtKunjunganUlang = (dataItem.FindControl("txtKunjunganUlang") as RadNumericTextBox);
                var txtKeluhanEfekSamping = (dataItem.FindControl("txtKeluhanEfekSamping") as RadNumericTextBox);
                var txtKeluhanEfekSampingDiRujuk = (dataItem.FindControl("txtKeluhanEfekSampingDiRujuk") as RadNumericTextBox);

                var rpt = newcoll.AddNew();
                rpt.RlTxReportNo = entity.RlTxReportNo;
                rpt.RlMasterReportItemID = reportItemId;
                rpt.KonselingAnc = txtKonselingAnc.Value.ToInt();
                rpt.KonselingPascaPersalinan = txtKonselingPascaPersalinan.Value.ToInt();
                rpt.KbBaruCmBukanRujukan = txtKbBaruCmBukanRujukan.Value.ToInt();
                rpt.KbBaruCmRujukanRi = txtKbBaruCmRujukanRi.Value.ToInt();
                rpt.KbBaruCmRujukanRj = txtKbBaruCmRujukanRj.Value.ToInt();
                rpt.KbBaruCmTotal = txtKbBaruCmTotal.Value.ToInt();
                rpt.KbBaruDkNifas = txtKbBaruDkNifas.Value.ToInt();
                rpt.KbBaruDkAbortus = txtKbBaruDkAbortus.Value.ToInt();
                rpt.KbBaruDkLain = txtKbBaruDkLain.Value.ToInt();
                rpt.KunjunganUlang = txtKunjunganUlang.Value.ToInt();
                rpt.KeluhanEfekSamping = txtKeluhanEfekSamping.Value.ToInt();
                rpt.KeluhanEfekSampingDiRujuk = txtKeluhanEfekSampingDiRujuk.Value.ToInt();
                rpt.LastUpdateDateTime = DateTime.Now;
                rpt.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                entity.Save();
                newcoll.Save();
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

        protected void grdRlReport3_12_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRlReport3_12.DataSource = RlTxReport312S;
        }

        private RlTxReport312Collection RlTxReport312S
        {
            get
            {
                var obj = ViewState["collRlTxReport312"];
                if (obj != null)
                    return ((RlTxReport312Collection)(obj));

                var collection = new RlTxReport312Collection();

                var query = new RlTxReport312Query("a");
                var mriq = new RlMasterReportItemQuery("b");
                query.InnerJoin(mriq).On(query.RlMasterReportItemID == mriq.RlMasterReportItemID);
                query.Select(
                        query,
                        mriq.RlMasterReportItemCode.As("refToRlMasterReportItem_RlMasterReportItemCode"),
                        mriq.RlMasterReportItemName.As("refToRlMasterReportItem_RlMasterReportItemName")
                    );

                query.Where(query.RlTxReportNo == txtRlTxReportNo.Text);

                query.OrderBy(mriq.RlMasterReportItemNo.Ascending);

                collection.Load(query);

                if (collection.Count == 0)
                {
                    var mric = new RlMasterReportItemCollection();
                    mric.Query.Where(mric.Query.RlMasterReportID == Request.QueryString["rptId"]);
                    mric.Query.OrderBy(mric.Query.RlMasterReportItemNo.Ascending);
                    mric.LoadAll();
                    foreach (var item in mric)
                    {
                        var coll = collection.AddNew();
                        coll.RlTxReportNo = txtRlTxReportNo.Text;
                        coll.RlMasterReportItemID = item.RlMasterReportItemID;
                        coll.RlMasterReportItemCode = item.RlMasterReportItemCode;
                        coll.RlMasterReportItemName = item.RlMasterReportItemName;
                        coll.KonselingAnc = 0;
                        coll.KonselingPascaPersalinan = 0;
                        coll.KbBaruCmBukanRujukan = 0;
                        coll.KbBaruCmRujukanRi = 0;
                        coll.KbBaruCmRujukanRj = 0;
                        coll.KbBaruCmTotal = 0;
                        coll.KbBaruDkNifas = 0;
                        coll.KbBaruDkAbortus = 0;
                        coll.KbBaruDkLain = 0;
                        coll.KunjunganUlang = 0;
                        coll.KeluhanEfekSamping = 0;
                        coll.KeluhanEfekSampingDiRujuk = 0;
                        coll.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        coll.LastUpdateDateTime = DateTime.Now;
                    }
                }

                ViewState["collRlTxReport312"] = collection;

                return collection;
            }
            set { ViewState["collRlTxReport312"] = value; }
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

                    Process();

                    grdRlReport3_12.Rebind();

                    break;
                case "print":
                    Print(AppConstant.Report.RL3_12);
                    break;
            }
        }

        private void Process()
        {
            if (string.IsNullOrEmpty(cboPeriodMonthStart.SelectedValue) || string.IsNullOrEmpty(cboPeriodMonthEnd.SelectedValue))
                return;

            if (int.Parse(cboPeriodMonthStart.SelectedValue) > int.Parse(cboPeriodMonthEnd.SelectedValue))
                return;

            var startDate = new DateTime(int.Parse(txtPeriodYear.Text), int.Parse(cboPeriodMonthStart.SelectedValue), 1);
            var endDate = new DateTime(int.Parse(txtPeriodYear.Text), int.Parse(cboPeriodMonthEnd.SelectedValue),
                                       DateTime.DaysInMonth(int.Parse(txtPeriodYear.Text), int.Parse(cboPeriodMonthEnd.SelectedValue)));

            foreach (var item in RlTxReport312S)
            {
                RlTxReport312.Process(startDate, endDate, item.RlMasterReportItemID ?? 0, out int konselingAnc, out int konselingPascaPersalinan, out int kbBaruCmBukanRujukan,
                    out int kbBaruCmRujukanRi, out int kbBaruCmRujukanRj, out int kbBaruCmTotal, out int kbBaruDkNifas, out int kbBaruDkAbortus, out int kbBaruDkLain, out int kunjunganUlang,
                    out int keluhanEfekSamping, out int keluhanEfekSampingDiRujuk);

                item.KonselingAnc = konselingAnc;
                item.KonselingPascaPersalinan = konselingPascaPersalinan;
                item.KbBaruCmBukanRujukan = kbBaruCmBukanRujukan;
                item.KbBaruCmRujukanRi = kbBaruCmRujukanRi;
                item.KbBaruCmRujukanRj = kbBaruCmRujukanRj;
                item.KbBaruCmTotal = kbBaruCmTotal;
                item.KbBaruDkNifas = kbBaruDkNifas;
                item.KbBaruDkAbortus = kbBaruDkAbortus;
                item.KbBaruDkLain = kbBaruDkLain;
                item.KunjunganUlang = kunjunganUlang;
                item.KeluhanEfekSamping = keluhanEfekSamping;
                item.KeluhanEfekSampingDiRujuk = keluhanEfekSampingDiRujuk;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void Print(string reportName)
        {
            var jobParameters = new PrintJobParameterCollection();

            var jobParameter = jobParameters.AddNew();
            jobParameter.Name = "RlTxReportNo";
            jobParameter.ValueString = txtRlTxReportNo.Text;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = reportName;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                            "oWnd.Show();" +
                            "oWnd.Maximize();";
            ajxPanel.ResponseScripts.Add(script);
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);

            grdRlReport3_12.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                RlTxReport312S = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdRlReport3_12.Rebind();
        }
    }
}
