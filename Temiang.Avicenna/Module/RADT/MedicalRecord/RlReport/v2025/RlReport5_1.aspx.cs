using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord.v2025
{
    public partial class RlReport5_1 : BasePageDetail
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

                RlTxReport5_1s = null;
            }
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new RlTxReportV2025());
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
                             coll.Query.PeriodMonthEnd == cboPeriodMonthEnd.SelectedValue, coll.Query.PeriodYear == txtPeriodYear.Text);
            coll.LoadAll();
            if (coll.Count > 0)
            {
                args.MessageText = "Record with this period has registered.";
                args.IsCancel = true;
                return;
            }

            entity = new RlTxReportV2025();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new RlTxReportV2025();
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
            auditLogFilter.TableName = "RlTxReportV2025";
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
        }

        private void SetEntityValue(RlTxReportV2025 entity)
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

        private void SaveEntity(RlTxReportV2025 entity)
        {
            int fromMonth = cboPeriodMonthStart.SelectedValue.ToInt();
            int toMonth = cboPeriodMonthEnd.SelectedValue.ToInt();
            int year = txtPeriodYear.Text.ToInt();

            var coll = new RlTxReport51V2025Collection();
            coll.Query.Where(coll.Query.RlTxReportNo == entity.RlTxReportNo);
            coll.LoadAll();
            coll.MarkAllAsDeleted();
            coll.Save();

            //var newcoll = RlTxReport5_1s;

            //RlTxReport5_1.Process(fromMonth, toMonth, year, AppSession.UserLogin.UserID, newcoll, out newcoll);

            using (esTransactionScope trans = new esTransactionScope())
            {
                //if (DataModeCurrent == AppEnum.DataMode.New)
                //    _autoNumber.Save();

                entity.Save();
                //newcoll.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            int result = RlTxReport51V2025.ProcessRlTxReport51V2025(entity.RlTxReportNo, fromMonth, toMonth, year, AppSession.UserLogin.UserID);
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

        protected void grdRlReport5_1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //grdRlReport5_1.DataSource = RlTxReport5_1s;
        }

        private RlTxReport51V2025Collection RlTxReport5_1s
        {
            get
            {
                var obj = ViewState["collRlTxReport51V2025"];
                if (obj != null)
                    return ((RlTxReport51V2025Collection)(obj));

                var collection = new RlTxReport51V2025Collection();

                var query = new RlTxReport51V2025Query("a");
                var mriq = new RlMasterReportItemV2025Query("b");
                var dtdq = new DtdQuery("c");

                query.InnerJoin(mriq).On(query.RlMasterReportItemID == mriq.RlMasterReportItemID);
                query.InnerJoin(dtdq).On(mriq.RlMasterReportItemCode == dtdq.DtdNo);
                query.Select(
                        query,
                        mriq.RlMasterReportItemCode.As("refToRlMasterReportItemV2025_RlMasterReportItemCode"),
                        mriq.RlMasterReportItemName.As("refToRlMasterReportItemV2025_RlMasterReportItemCode"),
                        dtdq.DtdLabel.As("refToDtd_DtdLabel")
                    );
                query.Where(query.RlTxReportNo == txtRlTxReportNo.Text);
                query.OrderBy(mriq.RlMasterReportItemNo.Ascending);

                collection.Load(query);

                if (collection.Count == 0)
                {
                    var mric = new RlMasterReportItemV2025Collection();
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
                        coll.L0007h = 0;
                        coll.P0007h = 0;
                        coll.L0828h = 0;
                        coll.P0828h = 0;
                        coll.L29h03b = 0;
                        coll.P29h03b = 0;
                        coll.L3b6b = 0;
                        coll.P3b6b = 0;
                        coll.L6b11b = 0;
                        coll.P6b11b = 0;
                        coll.L0104t = 0;
                        coll.P0104t = 0;
                        coll.L0509t = 0;
                        coll.P0509t = 0;
                        coll.L1014t = 0;
                        coll.P1014t = 0;
                        coll.L1519t = 0;
                        coll.P1519t = 0;
                        coll.L2024t = 0;
                        coll.P2024t = 0;
                        coll.L2529t = 0;
                        coll.P2529t = 0;
                        coll.L3034t = 0;
                        coll.P3034t = 0;
                        coll.L3539t = 0;
                        coll.P3539t = 0;
                        coll.L4044t = 0;
                        coll.P4044t = 0;
                        coll.L4549t = 0;
                        coll.P4549t = 0;
                        coll.L5054t = 0;
                        coll.P5054t = 0;
                        coll.L5559t = 0;
                        coll.P5559t = 0;
                        coll.L6064t = 0;
                        coll.P6064t = 0;
                        coll.L6569t = 0;
                        coll.P6569t = 0;
                        coll.L7074t = 0;
                        coll.P7074t = 0;
                        coll.L7579t = 0;
                        coll.P7579t = 0;
                        coll.L8084t = 0;
                        coll.P8084t = 0;
                        coll.L85t = 0;
                        coll.P85t = 0;
                        coll.KasusBaruL = 0;
                        coll.KasusBaruP = 0;
                        coll.TotalKasusBaru = 0;
                        coll.TotalKunjungan = 0;
                        coll.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        coll.LastUpdateDateTime = DateTime.Now;
                    }
                }

                ViewState["collRlTxReport51V2025"] = collection;

                return collection;
            }
            set { ViewState["collRlTxReport51V2025"] = value; }
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

                    grdRlReport5_1.Rebind();
                    break;
                case "print":
                    Print(AppConstant.Report.RL51V2025);
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
                epidiagq.DiagnoseID == diagq.DiagnoseID &&
                diagq.DtdNo.In(RlTxReport5_1s.Select(r => r.RlMasterReportItemCode).ToArray())
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

            var dtb = epidiagq.LoadDataTable();

            if (dtb.Rows.Count == 0)
                return;

            foreach (DataRow row in dtb.Rows)
            {
                var item = RlTxReport5_1s.Where(r => r.RlMasterReportItemCode == row["DtdNo"].ToString()).Single();
                if (Convert.ToInt16(row["AgeInDay"]) <= 0 &&
                    Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                    Convert.ToInt16(row["AgeInYear"]) == 0)
                {
                    if (row["Sex"].ToString() == "M") item.L0001j++;
                    else item.P0001j++;
                }
                else
                {
                    if (Convert.ToInt16(row["AgeInDay"]) <= 1 &&
                    Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                    Convert.ToInt16(row["AgeInYear"]) == 0)
                    {
                        if (row["Sex"].ToString() == "M") item.L0001h++;
                        else item.P0001h++;
                    }
                    else
                    {
                        if (Convert.ToInt16(row["AgeInDay"]) <= 7 &&
                        Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                        Convert.ToInt16(row["AgeInYear"]) == 0)
                        {
                            if (row["Sex"].ToString() == "M") item.L0007h++;
                            else item.P0007h++;
                        }
                        else
                        {
                            if (Convert.ToInt16(row["AgeInDay"]) > 7 &&
                                Convert.ToInt16(row["AgeInDay"]) <= 28 &&
                                Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                                Convert.ToInt16(row["AgeInYear"]) == 0)
                            {
                                if (row["Sex"].ToString() == "M") item.L0828h++;
                                else item.P0828h++;
                            }
                            else
                            {
                                if ((Convert.ToInt16(row["AgeInDay"]) > 28 &&
                                     Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                                     Convert.ToInt16(row["AgeInYear"]) == 0) ||
                                    (Convert.ToInt16(row["AgeInMonth"]) > 1 &&
                                     Convert.ToInt16(row["AgeInMonth"]) < 3 &&
                                     Convert.ToInt16(row["AgeInYear"]) == 0) ||
                                     (Convert.ToInt16(row["AgeInMonth"]) > 1 &&
                                     Convert.ToInt16(row["AgeInMonth"]) <= 3 &&
                                     Convert.ToInt16(row["AgeInDay"]) == 0 &&
                                     Convert.ToInt16(row["AgeInYear"]) == 0))
                                {
                                    if (row["Sex"].ToString() == "M") item.L29h03b++;
                                    else item.P29h03b++;
                                }
                                else
                                {
                                    if ((Convert.ToInt16(row["AgeInMonth"]) >= 3 &&
                                         Convert.ToInt16(row["AgeInYear"]) == 0) ||
                                        (Convert.ToInt16(row["AgeInMonth"]) < 6 &&
                                         Convert.ToInt16(row["AgeInYear"]) == 0))
                                    {
                                        if (row["Sex"].ToString() == "M") item.L3b6b++;
                                        else item.P3b6b++;
                                    }
                                    else
                                    {
                                        if ((Convert.ToInt16(row["AgeInMonth"]) >= 6 &&
                                             Convert.ToInt16(row["AgeInYear"]) == 0) ||
                                            (Convert.ToInt16(row["AgeInMonth"]) <= 11 &&
                                             Convert.ToInt16(row["AgeInYear"]) == 0))
                                        {
                                            if (row["Sex"].ToString() == "M") item.L6b11b++;
                                            else item.P6b11b++;
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
                                                    Convert.ToInt16(row["AgeInYear"]) <= 9)
                                                {
                                                    if (row["Sex"].ToString() == "M") item.L0509t++;
                                                    else item.P0509t++;
                                                }
                                                else
                                                {
                                                    if (Convert.ToInt16(row["AgeInYear"]) > 9 &&
                                                        Convert.ToInt16(row["AgeInYear"]) <= 14)
                                                    {
                                                        if (row["Sex"].ToString() == "M") item.L1014t++;
                                                        else item.P1014t++;
                                                    }
                                                    else
                                                    {
                                                        if (Convert.ToInt16(row["AgeInYear"]) > 14 &&
                                                            Convert.ToInt16(row["AgeInYear"]) <= 19)
                                                        {
                                                            if (row["Sex"].ToString() == "M") item.L1519t++;
                                                            else item.P1519t++;
                                                        }
                                                        else
                                                        {
                                                            if (Convert.ToInt16(row["AgeInYear"]) > 19 &&
                                                                Convert.ToInt16(row["AgeInYear"]) <= 24)
                                                            {
                                                                if (row["Sex"].ToString() == "M") item.L2024t++;
                                                                else item.P2024t++;
                                                            }
                                                            else
                                                            {
                                                                if (Convert.ToInt16(row["AgeInYear"]) > 24 &&
                                                                    Convert.ToInt16(row["AgeInYear"]) <= 29)
                                                                {
                                                                    if (row["Sex"].ToString() == "M") item.L2529t++;
                                                                    else item.P2529t++;
                                                                }
                                                                else
                                                                {
                                                                    if (Convert.ToInt16(row["AgeInYear"]) > 29 &&
                                                                        Convert.ToInt16(row["AgeInYear"]) <= 34)
                                                                    {
                                                                        if (row["Sex"].ToString() == "M") item.L3034t++;
                                                                        else item.P3034t++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (Convert.ToInt16(row["AgeInYear"]) > 34 &&
                                                                            Convert.ToInt16(row["AgeInYear"]) <= 39)
                                                                        {
                                                                            if (row["Sex"].ToString() == "M") item.L3539t++;
                                                                            else item.P3539t++;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (Convert.ToInt16(row["AgeInYear"]) > 39 &&
                                                                                Convert.ToInt16(row["AgeInYear"]) <= 44)
                                                                            {
                                                                                if (row["Sex"].ToString() == "M") item.L4044t++;
                                                                                else item.P4044t++;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (Convert.ToInt16(row["AgeInYear"]) > 44 &&
                                                                                    Convert.ToInt16(row["AgeInYear"]) <= 49)
                                                                                {
                                                                                    if (row["Sex"].ToString() == "M") item.L4549t++;
                                                                                    else item.P4549t++;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (Convert.ToInt16(row["AgeInYear"]) > 49 &&
                                                                                        Convert.ToInt16(row["AgeInYear"]) <= 54)
                                                                                    {
                                                                                        if (row["Sex"].ToString() == "M") item.L5054t++;
                                                                                        else item.P5054t++;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (Convert.ToInt16(row["AgeInYear"]) > 54 &&
                                                                                            Convert.ToInt16(row["AgeInYear"]) <= 59)
                                                                                        {
                                                                                            if (row["Sex"].ToString() == "M") item.L5559t++;
                                                                                            else item.P5559t++;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (Convert.ToInt16(row["AgeInYear"]) > 59 &&
                                                                                                Convert.ToInt16(row["AgeInYear"]) <= 64)
                                                                                            {
                                                                                                if (row["Sex"].ToString() == "M") item.L6064t++;
                                                                                                else item.P6064t++;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (Convert.ToInt16(row["AgeInYear"]) > 64 &&
                                                                                                    Convert.ToInt16(row["AgeInYear"]) <= 69)
                                                                                                {
                                                                                                    if (row["Sex"].ToString() == "M") item.L6569t++;
                                                                                                    else item.P6569t++;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (Convert.ToInt16(row["AgeInYear"]) > 69 &&
                                                                                                        Convert.ToInt16(row["AgeInYear"]) <= 74)
                                                                                                    {
                                                                                                        if (row["Sex"].ToString() == "M") item.L7074t++;
                                                                                                        else item.P7074t++;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (Convert.ToInt16(row["AgeInYear"]) > 74 &&
                                                                                                            Convert.ToInt16(row["AgeInYear"]) <= 79)
                                                                                                        {
                                                                                                            if (row["Sex"].ToString() == "M") item.L7579t++;
                                                                                                            else item.P7579t++;
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (Convert.ToInt16(row["AgeInYear"]) > 79 &&
                                                                                                                Convert.ToInt16(row["AgeInYear"]) <= 84)
                                                                                                            {
                                                                                                                if (row["Sex"].ToString() == "M") item.L8084t++;
                                                                                                                else item.P8084t++;
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (row["Sex"].ToString() == "M") item.L85t++;
                                                                                                                else item.P85t++;
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                                
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
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

            grdRlReport5_1.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                RlTxReport5_1s = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdRlReport5_1.Rebind();
        }
    }
}
