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

namespace Temiang.Avicenna.Module.RADT.MedicalRecord.RlReport.v2025
{
    public partial class RlReport4_3 : BasePageDetail
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

            //Display Data Detail
            PopulateGridDetail();
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
            foreach (RlTxReport43V2025 item in RlTxReport43V2025s)
                item.RlTxReportNo = entity.RlTxReportNo;

            using (esTransactionScope trans = new esTransactionScope())
            {
                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                entity.Save();
                RlTxReport43V2025s.Save();

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

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdRlReport4_3V2025.Columns[0].Visible = isVisible;
            grdRlReport4_3V2025.Columns[grdRlReport4_3V2025.Columns.Count - 1].Visible = isVisible;

            grdRlReport4_3V2025.MasterTableView.CommandItemDisplay = isVisible
                ? GridCommandItemDisplay.Top
                : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                RlTxReport43V2025s = null;

            //Perbaharui tampilan dan data
            grdRlReport4_3V2025.Rebind();
        }

        private RlTxReport43V2025Collection RlTxReport43V2025s
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collRlTxReport43V2025"];
                    if (obj != null)
                        return ((RlTxReport43V2025Collection)(obj));
                }

                var coll = new RlTxReport43V2025Collection();
                var query = new RlTxReport43V2025Query("a");
                var diagQ = new DiagnoseQuery("b");
                query.InnerJoin(diagQ).On(query.DiagnosaID == diagQ.DiagnoseID);
                query.Select(query, diagQ.DiagnoseName.As("refToDiagnose_DiagnoseName"));
                query.Where(query.RlTxReportNo == txtRlTxReportNo.Text);
                query.OrderBy(query.TotalKeluarMati.Descending);
                coll.Load(query);

                Session["collRlTxReport43V2025"] = coll;
                return coll;
            }
            set
            {
                Session["collRlTxReport43V2025"] = value;
            }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            RlTxReport43V2025s = null; //Reset Record Detail
            grdRlReport4_3V2025.DataSource = RlTxReport43V2025s;
            grdRlReport4_3V2025.DataBind();
        }

        protected void grdRlReport4_3V2025_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRlReport4_3V2025.DataSource = RlTxReport43V2025s;
        }

        protected void grdRlReport4_3V2025_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String diagId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][RlTxReport43V2025Metadata.ColumnNames.DiagnosaID]);
            RlTxReport43V2025 entity = FindRlTxReport43V2025(diagId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdRlReport4_3V2025_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String diagId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][RlTxReport43V2025Metadata.ColumnNames.DiagnosaID]);
            RlTxReport43V2025 entity = FindRlTxReport43V2025(diagId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdRlReport4_3V2025_InsertCommand(object source, GridCommandEventArgs e)
        {
            RlTxReport43V2025 entity = RlTxReport43V2025s.AddNew();
            SetEntityValue(entity, e);
        }

        private RlTxReport43V2025 FindRlTxReport43V2025(String diagNo)
        {
            RlTxReport43V2025Collection coll = RlTxReport43V2025s;
            RlTxReport43V2025 retEntity = null;
            foreach (RlTxReport43V2025 rec in coll)
            {
                if (rec.DiagnosaID.Equals(diagNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(RlTxReport43V2025 entity, GridCommandEventArgs e)
        {
            var userControl = (RlReport4_3Detail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RlTxReportNo = txtRlTxReportNo.Text;
                entity.DiagnosaID = userControl.DiagnoseID;
                entity.DiagnoseName = userControl.DiagnoseName;
                entity.HidupMatiL = userControl.HidupMatiL.ToInt();
                entity.HidupMatiP = userControl.HidupMatiP.ToInt();
                entity.TotalHidupMati = entity.HidupMatiL + entity.HidupMatiP;
                entity.KeluarMatiL = userControl.KeluarMatiL.ToInt();
                entity.KeluarMatiP = userControl.KeluarMatiP.ToInt();
                entity.TotalKeluarMati = entity.KeluarMatiL + entity.KeluarMatiP;

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = DateTime.Now;
                }
            }
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

                    grdRlReport4_3V2025.Rebind();

                    break;
                case "print":
                    Print(AppConstant.Report.RL4_3V2025);
                    break;
            }
        }

        private void Process()
        {
            if (string.IsNullOrEmpty(cboPeriodMonthStart.SelectedValue) || string.IsNullOrEmpty(cboPeriodMonthEnd.SelectedValue))
                return;

            if (int.Parse(cboPeriodMonthStart.SelectedValue) > int.Parse(cboPeriodMonthEnd.SelectedValue))
                return;

            int fromMonth = cboPeriodMonthStart.SelectedValue.ToInt();
            int toMonth = cboPeriodMonthEnd.SelectedValue.ToInt();
            int year = txtPeriodYear.Text.ToInt();

            RlTxReport43V2025s.MarkAllAsDeleted();

            //RlTxReport43V2025.Process(fromMonth, toMonth, year, txtRlTxReportNo.Text, AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48,
            //        AppSession.UserLogin.UserID, RlTxReport43V2025s, out RlTxReport43V2025s);

            var reg = new RegistrationQuery("r");
            var epd = new EpisodeDiagnoseQuery("e");
            var dia = new DiagnoseQuery("d");
            var pat = new PatientQuery("p");

            reg.Select(
                dia.DiagnoseID,
                dia.DiagnoseName,
                string.Format(@"<CASE WHEN p.Sex = 'M' AND r.SRDischargeCondition NOT IN ('{0}', '{1}') 
                                      THEN COUNT(r.RegistrationNo) 
                                      ELSE 0 
                                 END AS HidupMatiL>", AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48),
                string.Format(@"<CASE WHEN p.Sex = 'F' AND r.SRDischargeCondition NOT IN ('{0}', '{1}') 
                                      THEN COUNT(r.RegistrationNo) 
                                      ELSE 0 
                                 END AS HidupMatiP>", AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48),
                string.Format(@"<CASE WHEN p.Sex = 'M' AND r.SRDischargeCondition IN ('{0}', '{1}') 
                                      THEN COUNT(r.RegistrationNo) 
                                      ELSE 0 
                                 END AS KeluarMatiL>", AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48),
                string.Format(@"<CASE WHEN p.Sex = 'F' AND r.SRDischargeCondition IN ('{0}', '{1}') 
                                      THEN COUNT(r.RegistrationNo) 
                                      ELSE 0 
                                 END AS KeluarMatiP>", AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48)
                );
            reg.InnerJoin(epd).On(reg.RegistrationNo == epd.RegistrationNo);
            reg.InnerJoin(dia).On(epd.DiagnoseID == dia.DiagnoseID);
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.Where(
                reg.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                reg.IsVoid == false,
                epd.SRDiagnoseType == "DiagnoseType-001",
                epd.DiagnoseID != string.Empty,
                dia.DiagnoseID.NotLike("Z%"),
                dia.DiagnoseID.NotLike("V%"),
                dia.DiagnoseID.NotLike("X%"),
                dia.DiagnoseID.NotLike("W%"),
                dia.DiagnoseID.NotLike("Y%"),
                dia.DiagnoseID.NotLike("R%"),
                dia.DiagnoseID.NotLike("080%"),
                dia.DiagnoseID.NotLike("082%")
                );
            //reg.Where(dia.DiagnoseID.Substring(0, 1) != "Z", dia.DiagnoseID.Substring(0, 3) != "O80", dia.DiagnoseID.Substring(0, 3) != "O82",
            //          dia.DiagnoseID.Substring(0, 1) != "V", dia.DiagnoseID.Substring(0, 1) != "W", dia.DiagnoseID.Substring(0, 1) != "R",
            //          dia.DiagnoseID.Substring(0, 1) != "X", dia.DiagnoseID.Substring(0, 1) != "Y");

            reg.Where(string.Format("<MONTH(r.DischargeDate) BETWEEN {0} AND {1}>", cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue));
            reg.Where(string.Format("<YEAR(r.DischargeDate) = {0}>", txtPeriodYear.Text));

            reg.GroupBy(
                dia.DiagnoseID,
                dia.DiagnoseName,
                pat.Sex,
                reg.SRDischargeCondition
                );

            var dtb = (from i in reg.LoadDataTable().AsEnumerable()
                      group i by new
                      {
                          DiagnoseID = i.Field<string>("DiagnoseID"),
                          DiagnoseName = i.Field<string>("DiagnoseName")
                      } into g
                       select new
                       {
                           DiagnoseID = g.Key.DiagnoseID,
                           DiagnoseName = g.Key.DiagnoseName,
                           HidupMatiL = g.Sum(s => s.Field<int>("HidupMatiL")) + g.Sum(s => s.Field<int>("KeluarMatiL")),
                           HidupMatiP = g.Sum(s => s.Field<int>("HidupMatiP")) + g.Sum(s => s.Field<int>("KeluarMatiP")),
                           KeluarMatiL = g.Sum(s => s.Field<int>("KeluarMatiL")),
                           KeluarMatiP = g.Sum(s => s.Field<int>("KeluarMatiP")),
                           TotalMati = g.Sum(s => s.Field<int>("KeluarMatiL")) + g.Sum(s => s.Field<int>("KeluarMatiP"))
                       })
                        .OrderByDescending(x => x.TotalMati)
                        .Take(10);

            foreach (var row in dtb)
            {
                var entity = RlTxReport43V2025s.AddNew();

                entity.RlTxReportNo = txtRlTxReportNo.Text;
                entity.DiagnosaID = row.DiagnoseID;
                entity.DiagnoseName = row.DiagnoseName;
                entity.HidupMatiL = row.HidupMatiL;
                entity.HidupMatiP = row.HidupMatiP;
                entity.TotalHidupMati = entity.HidupMatiL + entity.HidupMatiP;
                entity.KeluarMatiL = row.KeluarMatiL;
                entity.KeluarMatiP = row.KeluarMatiP;
                entity.TotalKeluarMati = entity.KeluarMatiL + entity.KeluarMatiP;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
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
    }
}
