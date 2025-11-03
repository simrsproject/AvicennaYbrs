using System;
using System.Linq;
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
    public partial class RlReport3_1 : BasePageDetail
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

        protected override void OnMenuEditClick()
        {
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
            var coll = new RlTxReport31Collection();
            coll.Query.Where(coll.Query.RlTxReportNo == entity.RlTxReportNo);
            coll.LoadAll();
            coll.MarkAllAsDeleted();
            coll.Save();

            var newcoll = new RlTxReport31Collection();

            foreach (GridDataItem dataItem in grdRlReport3_1.MasterTableView.Items)
            {
                var reportItemId = dataItem.GetDataKeyValue("RlMasterReportItemID").ToInt();
                var txtPasienAwal = (dataItem.FindControl("txtPasienAwal") as RadNumericTextBox);
                var txtPasienMasuk = (dataItem.FindControl("txtPasienMasuk") as RadNumericTextBox);
                var txtPasienKeluarHidup = (dataItem.FindControl("txtPasienKeluarHidup") as RadNumericTextBox);
                var txtPasienKeluarMatiK48 = (dataItem.FindControl("txtPasienKeluarMatiK48") as RadNumericTextBox);
                var txtPasienKeluarMatiL48 = (dataItem.FindControl("txtPasienKeluarMatiL48") as RadNumericTextBox);
                var txtPasienPindahan = (dataItem.FindControl("txtPasienPindahan") as RadNumericTextBox);
                var txtPasienDipindahkan = (dataItem.FindControl("txtPasienDipindahkan") as RadNumericTextBox);
                var txtLamaRawat = (dataItem.FindControl("txtLamaRawat") as RadNumericTextBox);
                var txtPasienAkhir = (dataItem.FindControl("txtPasienAkhir") as RadNumericTextBox);
                var txtHariRawat = (dataItem.FindControl("txtHariRawat") as RadNumericTextBox);
                var txtVvip = (dataItem.FindControl("txtVvip") as RadNumericTextBox);
                var txtVip = (dataItem.FindControl("txtVip") as RadNumericTextBox);
                var txtI = (dataItem.FindControl("txtI") as RadNumericTextBox);
                var txtIi = (dataItem.FindControl("txtIi") as RadNumericTextBox);
                var txtIii = (dataItem.FindControl("txtIii") as RadNumericTextBox);
                var txtKelasKhusus = (dataItem.FindControl("txtKelasKhusus") as RadNumericTextBox);

                var rpt = newcoll.AddNew();
                rpt.RlTxReportNo = entity.RlTxReportNo;
                rpt.RlMasterReportItemID = reportItemId;
                rpt.PasienAwal = txtPasienAwal.Value.ToInt();
                rpt.PasienMasuk = txtPasienMasuk.Value.ToInt();
                rpt.PasienKeluarHidup = txtPasienKeluarHidup.Value.ToInt();
                rpt.PasienKeluarMatiK48 = txtPasienKeluarMatiK48.Value.ToInt();
                rpt.PasienKeluarMatiL48 = txtPasienKeluarMatiL48.Value.ToInt();
                rpt.PasienPindahan = txtPasienPindahan.Value.ToInt();
                rpt.PasienDipindahkan = txtPasienDipindahkan.Value.ToInt();
                rpt.LamaRawat = txtLamaRawat.Value.ToInt();
                rpt.PasienAkhir = txtPasienAkhir.Value.ToInt();
                rpt.HariRawat = txtHariRawat.Value.ToInt();
                rpt.Vvip = txtVvip.Value.ToInt();
                rpt.Vip = txtVip.Value.ToInt();
                rpt.I = txtI.Value.ToInt();
                rpt.Ii = txtIi.Value.ToInt();
                rpt.Iii = txtIii.Value.ToInt();
                rpt.KelasKhusus = txtKelasKhusus.Value.ToInt();
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

        protected void grdRlReport3_1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRlReport3_1.DataSource = RlTxReport31S;
        }

        private RlTxReport31Collection RlTxReport31S
        {
            get
            {
                var obj = ViewState["collRlTxReport31"];
                if (obj != null)
                    return ((RlTxReport31Collection)(obj));

                var collection = new RlTxReport31Collection();

                var query = new RlTxReport31Query("a");
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
                        coll.PasienAwal = 0;
                        coll.PasienMasuk = 0;
                        coll.PasienKeluarHidup = 0;
                        coll.PasienKeluarMatiK48 = 0;
                        coll.PasienKeluarMatiL48 = 0;
                        coll.PasienPindahan = 0;
                        coll.PasienDipindahkan = 0;
                        coll.LamaRawat = 0;
                        coll.PasienAkhir = 0;
                        coll.HariRawat = 0;
                        coll.Vvip = 0;
                        coll.Vip = 0;
                        coll.I = 0;
                        coll.Ii = 0;
                        coll.Iii = 0;
                        coll.KelasKhusus = 0;
                        coll.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        coll.LastUpdateDateTime = DateTime.Now;
                    }
                }

                ViewState["collRlTxReport31"] = collection;

                return collection;
            }
            set { ViewState["collRlTxReport31"] = value; }
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

                    grdRlReport3_1.Rebind();

                    break;
                case "print":
                    Print(AppConstant.Report.RL3_1);
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

            foreach (var item in RlTxReport31S)
            {
                int pAwal = 0, pMasuk = 0, pHidup = 0, pMatiK48 = 0, pMatiL48 = 0, pAkhir = 0, lamaRawat = 0, hariRawat = 0;
                int pPindahan = 0, pDipindahkan = 0;
                int vvip = 0, vip = 0, i = 0, ii = 0, iii = 0, khusus = 0;

                var mri = new RlMasterReportItem();
                mri.LoadByPrimaryKey(item.RlMasterReportItemID ?? 0);

                string paramedicRl1 = mri.SRParamedicRL1;
                if (!string.IsNullOrEmpty(paramedicRl1))
                {
                    RlTxReport31.Process(startDate, endDate, paramedicRl1, AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48,
                            out pAwal, out pMasuk, out pHidup, out pMatiK48, out pMatiL48, out pAkhir, out lamaRawat, out hariRawat, out pPindahan, out pDipindahkan,
                            out vvip, out vip, out i, out ii, out iii, out khusus);
                }

                item.PasienAwal = pAwal;
                item.PasienMasuk = pMasuk;
                item.PasienKeluarHidup = pHidup;
                item.PasienKeluarMatiK48 = pMatiK48;
                item.PasienKeluarMatiL48 = pMatiL48;
                item.PasienPindahan = pPindahan;
                item.PasienDipindahkan = pDipindahkan;
                item.LamaRawat = lamaRawat;
                item.PasienAkhir = pAkhir;
                item.HariRawat = hariRawat;
                item.Vvip = vvip;
                item.Vip = vip;
                item.I = i;
                item.Ii = ii;
                item.Iii = iii;
                item.KelasKhusus = khusus;
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

            grdRlReport3_1.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                RlTxReport31S = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdRlReport3_1.Rebind();
        }
    }
}
