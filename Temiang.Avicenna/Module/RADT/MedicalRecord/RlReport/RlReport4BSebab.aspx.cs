using System;
using System.Web.Util;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Linq;
using System.Globalization;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class RlReport4BSebab : BasePageDetail
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

                RlTxReport4BSebabs = null;
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
            {
                txtRlTxReportNo.Text = GetNewTransactionNo();
                _autoNumber.Save();
            }

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
            int fromMonth = cboPeriodMonthStart.SelectedValue.ToInt();
            int toMonth = cboPeriodMonthEnd.SelectedValue.ToInt();
            int year = txtPeriodYear.Text.ToInt();

            var coll = new RlTxReport4BSebabCollection();
            coll.Query.Where(coll.Query.RlTxReportNo == entity.RlTxReportNo);
            coll.LoadAll();
            coll.MarkAllAsDeleted();
            coll.Save();

            //var newcoll = RlTxReport4BSebabs;

            //RlTxReport4BSebab.Process(fromMonth, toMonth, year, AppSession.UserLogin.UserID, newcoll, out newcoll);

            using (esTransactionScope trans = new esTransactionScope())
            {
                //if (DataModeCurrent == AppEnum.DataMode.New)
                //    _autoNumber.Save();

                entity.Save();
                //newcoll.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }

            int result = RlTxReport.ProcessRlTxReport4BSebab(entity.RlTxReportNo, fromMonth, toMonth, year, AppSession.UserLogin.UserID);
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

        protected void grdRlReport4BSebab_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //grdRlReport4BSebab.DataSource = RlTxReport4BSebabs;
        }

        private RlTxReport4BSebabCollection RlTxReport4BSebabs
        {
            get
            {
                var obj = ViewState["collRlTxReport4BSebab"];
                if (obj != null)
                    return ((RlTxReport4BSebabCollection)(obj));

                var collection = new RlTxReport4BSebabCollection();

                var query = new RlTxReport4BSebabQuery("a");
                var mriq = new RlMasterReportItemQuery("b");
                var dtdq = new DtdQuery("c");
                query.InnerJoin(mriq).On(query.RlMasterReportItemID == mriq.RlMasterReportItemID);
                query.InnerJoin(dtdq).On(mriq.RlMasterReportItemCode == dtdq.DtdNo);
                query.Select(
                        query,
                        mriq.RlMasterReportItemCode.As("refToRlMasterReportItem_RlMasterReportItemCode"),
                        mriq.RlMasterReportItemName.As("refToRlMasterReportItem_RlMasterReportItemName"),
                        dtdq.DtdLabel.As("refToDtd_DtdLabel")
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
                        var dtd = new Dtd();
                        dtd.LoadByPrimaryKey(item.RlMasterReportItemCode);
                        coll.DtdLabel = dtd.DtdLabel;
                        coll.L0006h = 0;
                        coll.P0006h = 0;
                        coll.L0628h = 0;
                        coll.P0628h = 0;
                        coll.L28h01t = 0;
                        coll.P28h01t = 0;
                        coll.L0104t = 0;
                        coll.P0104t = 0;
                        coll.L0414t = 0;
                        coll.P0414t = 0;
                        coll.L1424t = 0;
                        coll.P1424t = 0;
                        coll.L2444t = 0;
                        coll.P2444t = 0;
                        coll.L4464t = 0;
                        coll.P4464t = 0;
                        coll.L64t = 0;
                        coll.P64t = 0;
                        coll.KasusBaruL = 0;
                        coll.KasusBaruP = 0;
                        coll.TotalKasusBaru = 0;
                        coll.TotalKunjungan = 0;
                        coll.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        coll.LastUpdateDateTime = DateTime.Now;
                    }
                }

                ViewState["collRlTxReport4BSebab"] = collection;

                return collection;
            }
            set { ViewState["collRlTxReport4BSebab"] = value; }
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

                    grdRlReport4BSebab.Rebind();

                    break;
                case "print":
                    Print(AppConstant.Report.RL4BSebab);
                    break;
            }
        }

        private void Process()
        {
            if (string.IsNullOrEmpty(cboPeriodMonthStart.SelectedValue) || string.IsNullOrEmpty(cboPeriodMonthEnd.SelectedValue))
                return;

            if (int.Parse(cboPeriodMonthStart.SelectedValue) > int.Parse(cboPeriodMonthEnd.SelectedValue))
                return;

            var epidiagq = new EpisodeDiagnoseQuery("a");
            var diagq = new DiagnoseQuery("b");
            var regq = new RegistrationQuery("c");
            var patq = new PatientQuery("d");

            epidiagq.Select(
                regq.AgeInDay,
                regq.AgeInMonth,
                regq.AgeInYear,
                patq.Sex,
                epidiagq.IsOldCase,
                diagq.DtdNo
                );
            epidiagq.InnerJoin(diagq).On(
                epidiagq.ExternalCauseID == diagq.DiagnoseID &&
                diagq.DtdNo.In(RlTxReport4BSebabs.Select(r => r.RlMasterReportItemCode).ToArray())
                );
            epidiagq.InnerJoin(regq).On(epidiagq.RegistrationNo == regq.RegistrationNo);
            epidiagq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);
            epidiagq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType != AppConstant.RegistrationType.InPatient,
                epidiagq.IsVoid == false
                );
            epidiagq.Where(string.Format("<MONTH(c.RegistrationDate) BETWEEN {0} AND {1}>", cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue));
            epidiagq.Where(string.Format("<YEAR(c.RegistrationDate) = {0}>", txtPeriodYear.Text));

            DataTable dtb = epidiagq.LoadDataTable();

            if (dtb.Rows.Count == 0)
                return;

            foreach (DataRow row in dtb.Rows)
            {
                var item = RlTxReport4BSebabs.Where(r => r.RlMasterReportItemCode == row["DtdNo"].ToString()).Single();

                if (Convert.ToInt16(row["AgeInDay"]) <= 6 &&
                    Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                    Convert.ToInt16(row["AgeInYear"]) == 0)
                {
                    if (row["Sex"].ToString() == "M") item.L0006h++;
                    else item.P0006h++;
                }
                else
                {
                    if (Convert.ToInt16(row["AgeInDay"]) > 6 &&
                        Convert.ToInt16(row["AgeInDay"]) <= 28 &&
                        Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                        Convert.ToInt16(row["AgeInYear"]) == 0)
                    {
                        if (row["Sex"].ToString() == "M") item.L0628h++;
                        else item.P0628h++;
                    }
                    else
                    {
                        if ((Convert.ToInt16(row["AgeInDay"]) > 28 &&
                             Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                             Convert.ToInt16(row["AgeInYear"]) == 0) ||
                            (Convert.ToInt16(row["AgeInMonth"]) >= 1 &&
                             Convert.ToInt16(row["AgeInYear"]) == 0) ||
                            (Convert.ToInt16(row["AgeInYear"]) == 1))
                        {
                            if (row["Sex"].ToString() == "M") item.L28h01t++;
                            else item.P28h01t++;
                        }
                        else
                        {
                            if (Convert.ToInt16(row["AgeInYear"]) > 1 &&
                                Convert.ToInt16(row["AgeInYear"]) <= 4)
                            {
                                if (row["Sex"].ToString() == "M") item.L0104t++;
                                else item.P0104t++;
                            }
                            else
                            {
                                if (Convert.ToInt16(row["AgeInYear"]) > 4 &&
                                    Convert.ToInt16(row["AgeInYear"]) <= 14)
                                {
                                    if (row["Sex"].ToString() == "M") item.L0414t++;
                                    else item.P0414t++;
                                }
                                else
                                {
                                    if (Convert.ToInt16(row["AgeInYear"]) > 14 &&
                                        Convert.ToInt16(row["AgeInYear"]) <= 24)
                                    {
                                        if (row["Sex"].ToString() == "M") item.L1424t++;
                                        else item.P1424t++;
                                    }
                                    else
                                    {
                                        if (Convert.ToInt16(row["AgeInYear"]) > 24 &&
                                            Convert.ToInt16(row["AgeInYear"]) <= 44)
                                        {
                                            if (row["Sex"].ToString() == "M") item.L2444t++;
                                            else item.P2444t++;
                                        }
                                        else
                                        {
                                            if (Convert.ToInt16(row["AgeInYear"]) > 44 &&
                                                Convert.ToInt16(row["AgeInYear"]) <= 64)
                                            {
                                                if (row["Sex"].ToString() == "M") item.L4464t++;
                                                else item.P4464t++;
                                            }
                                            else
                                            {
                                                if (row["Sex"].ToString() == "M") item.L64t++;
                                                else item.P64t++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (Convert.ToBoolean(row["IsOldCase"]) == false)
                {
                    if (row["Sex"].ToString() == "M") item.KasusBaruL++;
                    else item.KasusBaruP++;
                }

                item.TotalKasusBaru = item.KasusBaruL + item.KasusBaruP;
                item.TotalKunjungan++;
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

            grdRlReport4BSebab.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                RlTxReport4BSebabs = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdRlReport4BSebab.Rebind();
        }
    }
}
