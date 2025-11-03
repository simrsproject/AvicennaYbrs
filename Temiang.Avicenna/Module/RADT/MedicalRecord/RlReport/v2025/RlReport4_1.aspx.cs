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

namespace Temiang.Avicenna.Module.RADT.MedicalRecord.v2025
{
    public partial class RlReport4_1 : BasePageDetail
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

                RlTxReport41V2025s = null;
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

        private void SaveEntity(RlTxReportV2025 entity)
        {
            int fromMonth = cboPeriodMonthStart.SelectedValue.ToInt();
            int toMonth = cboPeriodMonthEnd.SelectedValue.ToInt();
            int year = txtPeriodYear.Text.ToInt();

            var coll = new RlTxReport41V2025Collection();
            coll.Query.Where(coll.Query.RlTxReportNo == entity.RlTxReportNo);
            coll.LoadAll();
            coll.MarkAllAsDeleted();
            coll.Save();

            var newcoll = RlTxReport41V2025s;

            RlTxReport41V2025.Process(fromMonth, toMonth, year, AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48, AppSession.UserLogin.UserID, newcoll, out newcoll);

            //var epidiagq = new EpisodeDiagnoseQuery("a");
            //var diagq = new DiagnoseQuery("b");
            //var regq = new RegistrationQuery("c");
            //var patq = new PatientQuery("d");

            //epidiagq.Select(
            //    regq.AgeInDay,
            //    regq.AgeInMonth,
            //    regq.AgeInYear,
            //    patq.Sex,
            //    regq.SRDischargeCondition,
            //    diagq.DtdNo
            //    );
            //epidiagq.InnerJoin(diagq).On(
            //    epidiagq.DiagnoseID == diagq.DiagnoseID &&
            //    diagq.DtdNo.In(newcoll.Select(r => r.RlMasterReportItemCode).ToArray())
            //    );
            //epidiagq.InnerJoin(regq).On(epidiagq.RegistrationNo == regq.RegistrationNo);
            //epidiagq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);
            //epidiagq.Where(
            //    regq.IsVoid == false,
            //    regq.SRRegistrationType == AppConstant.RegistrationType.InPatient,
            //    epidiagq.IsVoid == false
            //    );
            //epidiagq.Where(string.Format("<MONTH(c.DischargeDate) BETWEEN {0} AND {1}>", cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue));
            //epidiagq.Where(string.Format("<YEAR(c.DischargeDate) = {0}>", txtPeriodYear.Text));

            //DataTable dtb = epidiagq.LoadDataTable();

            //if (dtb.Rows.Count > 0)
            //{
            //    foreach (DataRow row in dtb.Rows)
            //    {
            //        var item = newcoll.Where(r => r.RlMasterReportItemCode == row["DtdNo"].ToString()).Single();

            //        if (Convert.ToInt16(row["AgeInDay"]) <= 6 &&
            //            Convert.ToInt16(row["AgeInMonth"]) == 0 &&
            //            Convert.ToInt16(row["AgeInYear"]) == 0)
            //        {
            //            if (row["Sex"].ToString() == "M") item.L0006h++;
            //            else item.P0006h++;
            //        }
            //        else
            //        {
            //            if (Convert.ToInt16(row["AgeInDay"]) > 6 &&
            //                Convert.ToInt16(row["AgeInDay"]) <= 28 &&
            //                Convert.ToInt16(row["AgeInMonth"]) == 0 &&
            //                Convert.ToInt16(row["AgeInYear"]) == 0)
            //            {
            //                if (row["Sex"].ToString() == "M") item.L0628h++;
            //                else item.P0628h++;
            //            }
            //            else
            //            {
            //                if ((Convert.ToInt16(row["AgeInDay"]) > 28 &&
            //                     Convert.ToInt16(row["AgeInMonth"]) == 0 &&
            //                     Convert.ToInt16(row["AgeInYear"]) == 0) ||
            //                    (Convert.ToInt16(row["AgeInMonth"]) >= 1 &&
            //                     Convert.ToInt16(row["AgeInYear"]) == 0) ||
            //                    (Convert.ToInt16(row["AgeInYear"]) == 1))
            //                {
            //                    if (row["Sex"].ToString() == "M") item.L28h01t++;
            //                    else item.P28h01t++;
            //                }
            //                else
            //                {
            //                    if (Convert.ToInt16(row["AgeInYear"]) > 1 &&
            //                        Convert.ToInt16(row["AgeInYear"]) <= 4)
            //                    {
            //                        if (row["Sex"].ToString() == "M") item.L0104t++;
            //                        else item.P0104t++;
            //                    }
            //                    else
            //                    {
            //                        if (Convert.ToInt16(row["AgeInYear"]) > 4 &&
            //                            Convert.ToInt16(row["AgeInYear"]) <= 14)
            //                        {
            //                            if (row["Sex"].ToString() == "M") item.L0414t++;
            //                            else item.P0414t++;
            //                        }
            //                        else
            //                        {
            //                            if (Convert.ToInt16(row["AgeInYear"]) > 14 &&
            //                                Convert.ToInt16(row["AgeInYear"]) <= 24)
            //                            {
            //                                if (row["Sex"].ToString() == "M") item.L1424t++;
            //                                else item.P1424t++;
            //                            }
            //                            else
            //                            {
            //                                if (Convert.ToInt16(row["AgeInYear"]) > 24 &&
            //                                    Convert.ToInt16(row["AgeInYear"]) <= 44)
            //                                {
            //                                    if (row["Sex"].ToString() == "M") item.L2444t++;
            //                                    else item.P2444t++;
            //                                }
            //                                else
            //                                {
            //                                    if (Convert.ToInt16(row["AgeInYear"]) > 44 &&
            //                                        Convert.ToInt16(row["AgeInYear"]) <= 64)
            //                                    {
            //                                        if (row["Sex"].ToString() == "M") item.L4464t++;
            //                                        else item.P4464t++;
            //                                    }
            //                                    else
            //                                    {
            //                                        if (row["Sex"].ToString() == "M") item.L64t++;
            //                                        else item.P64t++;
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }

            //        if (row["SRDischargeCondition"].ToString() == AppSession.Parameter.DischargeConditionDieLessThen48 ||
            //            row["SRDischargeCondition"].ToString() == AppSession.Parameter.DischargeConditionDieMoreThen48)
            //            item.TotalMati++;
            //        item.TotalL = item.L0006h + item.L0628h + item.L28h01t + item.L0104t + item.L0414t + item.L1424t + item.L2444t + item.L4464t + item.L64t;
            //        item.TotalP = item.P0006h + item.P0628h + item.P28h01t + item.P0104t + item.P0414t + item.P1424t + item.P2444t + item.P4464t + item.P64t;
            //        item.Total = item.TotalL + item.TotalP;
            //        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
            //        item.LastUpdateDateTime = DateTime.Now;
            //    }
            //}

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

        protected void grdRlReport4_1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //grdRlReport41V2025.DataSource = RlTxReport41V2025s;
        }

        private RlTxReport41V2025Collection RlTxReport41V2025s
        {
            get
            {
                var obj = ViewState["collRlTxReport41V2025"];
                if (obj != null)
                    return ((RlTxReport41V2025Collection)(obj));

                var collection = new RlTxReport41V2025Collection();

                var query = new RlTxReport41V2025Query("a");
                var mriq = new RlMasterReportItemV2025Query("b");
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
                        coll.L1j = 0;
                        coll.P1j = 0;
                        coll.L123j = 0;
                        coll.P123j = 0;
                        coll.L0107h = 0;
                        coll.P0107h = 0;
                        coll.L0828h = 0;
                        coll.P0828h = 0;
                        coll.L29h03b = 0;
                        coll.P29h03b = 0;
                        coll.L0306b = 0;
                        coll.P0306b = 0;
                        coll.L0611b = 0;
                        coll.P0611b = 0;
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
                        coll.TotalPasienHidupL = 0;
                        coll.TotalPasienHidupP = 0;
                        coll.TotalPasienHidup = 0;
                        coll.TotalPasienMatiL = 0;
                        coll.TotalPasienMatiP = 0;
                        coll.TotalPasienMati = 0;
                        coll.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        coll.LastUpdateDateTime = DateTime.Now;

                    }
                }

                ViewState["collRlTxReport41V2025"] = collection;

                return collection;
            }
            set { ViewState["collRlTxReport41V2025"] = value; }
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

                    grdRlReport4_1.Rebind();

                    break;
                case "print":
                    Print(AppConstant.Report.RL4_1V2025);
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
            var brq = new BirthRecordQuery("e");

            epidiagq.Select(
                regq.AgeInDay,
                regq.AgeInMonth,
                regq.AgeInYear,
                patq.Sex,
                regq.SRDischargeCondition,
                diagq.DtdNo,
                @"<DATEDIFF(Minute,e.TimeOfBirth, GETDATE()) AS Selisih>"
                );
            epidiagq.InnerJoin(diagq).On(
                epidiagq.DiagnoseID == diagq.DiagnoseID &&
                diagq.DtdNo.In(RlTxReport41V2025s.Select(r => r.RlMasterReportItemCode).ToArray())
                );
            epidiagq.InnerJoin(regq).On(epidiagq.RegistrationNo == regq.RegistrationNo);
            epidiagq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);
            epidiagq.LeftJoin(brq).On(regq.RegistrationNo == brq.RegistrationNo);
            epidiagq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                epidiagq.IsVoid == false
                );
            epidiagq.Where(string.Format("<MONTH(c.DischargeDate) BETWEEN {0} AND {1}>", cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue));
            epidiagq.Where(string.Format("<YEAR(c.DischargeDate) = {0}>", txtPeriodYear.Text));

            DataTable dtb = epidiagq.LoadDataTable();

            if (dtb.Rows.Count == 0)
                return;

            foreach (DataRow row in dtb.Rows)
            {
                var item = RlTxReport41V2025s.Where(r => r.RlMasterReportItemCode == row["DtdNo"].ToString()).Single();

                if (!Convert.IsDBNull(row["Selisih"]) && Convert.ToInt32(row["Selisih"]) < 60)
                {
                    if (row["Sex"].ToString() == "M") item.L1j++;
                    else item.P1j++;
                }
                else
                {
                    if ((!Convert.IsDBNull(row["Selisih"]) && Convert.ToInt32(row["Selisih"]) >= 60 &&
                         !Convert.IsDBNull(row["Selisih"]) && Convert.ToInt32(row["Selisih"]) <= 1380))
                    {
                        if (row["Sex"].ToString() == "M") item.L123j++;
                        else item.P123j++;
                    }
                    else
                    {
                        if ((Convert.ToInt16(row["AgeInDay"]) >= 1 &&
                             Convert.ToInt16(row["AgeInDay"]) <= 7 &&
                             Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                             Convert.ToInt16(row["AgeInYear"]) == 0))
                        {
                            if (row["Sex"].ToString() == "M") item.L0107h++;
                            else item.P0107h++;
                        }
                        else
                        {
                            if (Convert.ToInt16(row["AgeInDay"]) >= 8 &&
                                Convert.ToInt16(row["AgeInDay"]) <= 28 &&
                                Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                                Convert.ToInt16(row["AgeInYear"]) == 0)
                            {
                                if (row["Sex"].ToString() == "M") item.L0828h++;
                                else item.P0828h++;
                            }
                            else
                            {
                                if ((Convert.ToInt16(row["AgeInDay"]) >= 29 &&
                                     Convert.ToInt16(row["AgeInMonth"]) < 3 &&
                                     Convert.ToInt16(row["AgeInYear"]) == 0))
                                {
                                    if (row["Sex"].ToString() == "M") item.L29h03b++;
                                    else item.P29h03b++;
                                }
                                else
                                {
                                    if (Convert.ToInt16(row["AgeInMonth"]) >= 3 &&
                                        Convert.ToInt16(row["AgeInMonth"]) < 6 &&
                                        Convert.ToInt16(row["AgeInYear"]) == 0)
                                    {
                                        if (row["Sex"].ToString() == "M") item.L0306b++;
                                        else item.L0306b++;
                                    }
                                    else
                                    {
                                        if (Convert.ToInt16(row["AgeInMonth"]) >= 6 &&
                                            Convert.ToInt16(row["AgeInMonth"]) <= 11 &&
                                            Convert.ToInt16(row["AgeInYear"]) == 0)
                                        {
                                            if (row["Sex"].ToString() == "M") item.L0611b++;
                                            else item.P0611b++;
                                        }
                                        else
                                        {
                                            if (Convert.ToInt16(row["AgeInYear"]) >= 1 &&
                                                Convert.ToInt16(row["AgeInYear"]) <= 4)
                                            {
                                                if (row["Sex"].ToString() == "M") item.L0104t++;
                                                else item.P0104t++;
                                            }
                                            else
                                            {
                                                if (Convert.ToInt16(row["AgeInYear"]) >= 5 &&
                                                    Convert.ToInt16(row["AgeInYear"]) <= 9)
                                                {
                                                    if (row["Sex"].ToString() == "M") item.L0509t++;
                                                    else item.P0509t++;
                                                }
                                                else
                                                {
                                                    if (Convert.ToInt16(row["AgeInYear"]) >= 10 &&
                                                        Convert.ToInt16(row["AgeInYear"]) <= 14)
                                                    {
                                                        if (row["Sex"].ToString() == "M") item.L1014t++;
                                                        else item.P1014t++;
                                                    }
                                                    else
                                                    {
                                                        if (Convert.ToInt16(row["AgeInYear"]) >= 15 &&
                                                            Convert.ToInt16(row["AgeInYear"]) <= 19)
                                                        {
                                                            if (row["Sex"].ToString() == "M") item.L1519t++;
                                                            else item.P1519t++;
                                                        }
                                                        else
                                                        {
                                                            if (Convert.ToInt16(row["AgeInYear"]) >= 20 &&
                                                                Convert.ToInt16(row["AgeInYear"]) <= 24)
                                                            {
                                                                if (row["Sex"].ToString() == "M") item.L2024t++;
                                                                else item.P2024t++;
                                                            }
                                                            else
                                                            {
                                                                if (Convert.ToInt16(row["AgeInYear"]) >= 25 &&
                                                                    Convert.ToInt16(row["AgeInYear"]) <= 29)
                                                                {
                                                                    if (row["Sex"].ToString() == "M") item.L2529t++;
                                                                    else item.P2529t++;
                                                                }
                                                                else
                                                                {
                                                                    if (Convert.ToInt16(row["AgeInYear"]) >= 30 &&
                                                                        Convert.ToInt16(row["AgeInYear"]) <= 34)
                                                                    {
                                                                        if (row["Sex"].ToString() == "M") item.L3034t++;
                                                                        else item.P3034t++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (Convert.ToInt16(row["AgeInYear"]) >= 35 &&
                                                                            Convert.ToInt16(row["AgeInYear"]) <= 39)
                                                                        {
                                                                            if (row["Sex"].ToString() == "M") item.L3539t++;
                                                                            else item.P3539t++;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (Convert.ToInt16(row["AgeInYear"]) >= 40 &&
                                                                                Convert.ToInt16(row["AgeInYear"]) <= 44)
                                                                            {
                                                                                if (row["Sex"].ToString() == "M") item.L4044t++;
                                                                                else item.P4044t++;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (Convert.ToInt16(row["AgeInYear"]) >= 45 &&
                                                                                    Convert.ToInt16(row["AgeInYear"]) <= 49)
                                                                                {
                                                                                    if (row["Sex"].ToString() == "M") item.L4549t++;
                                                                                    else item.P4549t++;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (Convert.ToInt16(row["AgeInYear"]) >= 50 &&
                                                                                        Convert.ToInt16(row["AgeInYear"]) <= 54)
                                                                                    {
                                                                                        if (row["Sex"].ToString() == "M") item.L5054t++;
                                                                                        else item.P5054t++;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (Convert.ToInt16(row["AgeInYear"]) >= 55 &&
                                                                                            Convert.ToInt16(row["AgeInYear"]) <= 59)
                                                                                        {
                                                                                            if (row["Sex"].ToString() == "M") item.L5559t++;
                                                                                            else item.P5559t++;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (Convert.ToInt16(row["AgeInYear"]) >= 60 &&
                                                                                                Convert.ToInt16(row["AgeInYear"]) <= 64)
                                                                                            {
                                                                                                if (row["Sex"].ToString() == "M") item.L6064t++;
                                                                                                else item.P6064t++;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (Convert.ToInt16(row["AgeInYear"]) >= 65 &&
                                                                                                    Convert.ToInt16(row["AgeInYear"]) <= 69)
                                                                                                {
                                                                                                    if (row["Sex"].ToString() == "M") item.L6569t++;
                                                                                                    else item.P6569t++;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (Convert.ToInt16(row["AgeInYear"]) >= 70 &&
                                                                                                        Convert.ToInt16(row["AgeInYear"]) <= 74)
                                                                                                    {
                                                                                                        if (row["Sex"].ToString() == "M") item.L7074t++;
                                                                                                        else item.P7074t++;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (Convert.ToInt16(row["AgeInYear"]) >= 75 &&
                                                                                                            Convert.ToInt16(row["AgeInYear"]) <= 79)
                                                                                                        {
                                                                                                            if (row["Sex"].ToString() == "M") item.L7579t++;
                                                                                                            else item.P7579t++;
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (Convert.ToInt16(row["AgeInYear"]) >= 80 &&
                                                                                                                Convert.ToInt16(row["AgeInYear"]) <= 84)
                                                                                                            {
                                                                                                                if (row["Sex"].ToString() == "M") item.L8084t++;
                                                                                                                else item.P8084t++;
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (Convert.ToInt16(row["AgeInYear"]) >= 85)
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
                }

                //if (row["SRDischargeCondition"].ToString() == AppSession.Parameter.DischargeConditionDieLessThen48 ||
                //    row["SRDischargeCondition"].ToString() == AppSession.Parameter.DischargeConditionDieMoreThen48)
                //    item.TotalPasienMatiL++;

                if ((row["SRDischargeCondition"].ToString() == AppSession.Parameter.DischargeConditionDieLessThen48 ||
                     row["SRDischargeCondition"].ToString() == AppSession.Parameter.DischargeConditionDieMoreThen48) &&
                     row["Sex"].ToString() == "M")
                {
                    item.TotalPasienMatiL++; // Tambah total laki-laki yang meninggal
                }
                else if ((row["SRDischargeCondition"].ToString() == AppSession.Parameter.DischargeConditionDieLessThen48 ||
                          row["SRDischargeCondition"].ToString() == AppSession.Parameter.DischargeConditionDieMoreThen48) &&
                          row["Sex"].ToString() == "F")
                {
                    item.TotalPasienMatiP++; // Tambah total perempuan yang meninggal
                }
                item.TotalPasienHidupL = item.L0107h + item.L0828h + item.L29h03b + item.L0306b + item.L0611b + item.L0104t + item.L0509t + item.L1014t + item.L1519t + item.L2024t + item.L2529t + item.L3034t + item.L3539t + item.L4044t + item.L4549t + item.L5054t + item.L5559t + item.L6064t + item.L6569t + item.L7074t + item.L7579t + item.L8084t + item.L85t + item.L1j + item.L123j;
                item.TotalPasienHidupP = item.P0107h + item.P0828h + item.P29h03b + item.P0306b + item.P0611b + item.P0104t + item.P0509t + item.P1014t + item.P1519t + item.P2024t + item.P2529t + item.P3034t + item.P3539t + item.P4044t + item.P4549t + item.P5054t + item.P5559t + item.P6064t + item.P6569t + item.P7074t + item.P7579t + item.P8084t + item.P85t + item.P1j + item.P123j;
                item.TotalPasienHidup = item.TotalPasienHidupL + item.TotalPasienHidupP;
                item.TotalPasienMati = item.TotalPasienMatiL + item.TotalPasienMatiP;
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

            grdRlReport4_1.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                RlTxReport41V2025s = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdRlReport4_1.Rebind();
        }
    }
}
