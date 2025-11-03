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
    public partial class RlReport5_3 : BasePageDetail
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

            //Display Data Detail
            PopulateGridDetail();
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
            foreach (RlTxReport53 item in RlTxReport53s)
                item.RlTxReportNo = entity.RlTxReportNo;

            using (esTransactionScope trans = new esTransactionScope())
            {
                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                entity.Save();
                RlTxReport53s.Save();

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

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdRlReport5_3.Columns[0].Visible = isVisible;
            grdRlReport5_3.Columns[grdRlReport5_3.Columns.Count - 1].Visible = isVisible;

            grdRlReport5_3.MasterTableView.CommandItemDisplay = isVisible
                ? GridCommandItemDisplay.Top
                : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                RlTxReport53s = null;

            //Perbaharui tampilan dan data
            grdRlReport5_3.Rebind();
        }

        private RlTxReport53Collection RlTxReport53s
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collRlTxReport53"];
                    if (obj != null)
                        return ((RlTxReport53Collection)(obj));
                }

                var coll = new RlTxReport53Collection();
                var query = new RlTxReport53Query("a");
                var diagQ = new DiagnoseQuery("b");
                query.InnerJoin(diagQ).On(query.DiagnosaID == diagQ.DiagnoseID);
                query.Select(query, diagQ.DiagnoseName.As("refToDiagnose_DiagnoseName"));
                query.Where(query.RlTxReportNo == txtRlTxReportNo.Text);
                query.OrderBy(query.Total.Descending);
                coll.Load(query);

                Session["collRlTxReport53"] = coll;
                return coll;
            }
            set
            {
                Session["collRlTxReport53"] = value;
            }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            RlTxReport53s = null; //Reset Record Detail
            grdRlReport5_3.DataSource = RlTxReport53s;
            grdRlReport5_3.DataBind();
        }

        protected void grdRlReport5_3_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRlReport5_3.DataSource = RlTxReport53s;
        }

        protected void grdRlReport5_3_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String diagId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][RlTxReport53Metadata.ColumnNames.DiagnosaID]);
            RlTxReport53 entity = FindRlTxReport53(diagId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdRlReport5_3_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String diagId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][RlTxReport53Metadata.ColumnNames.DiagnosaID]);
            RlTxReport53 entity = FindRlTxReport53(diagId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdRlReport5_3_InsertCommand(object source, GridCommandEventArgs e)
        {
            RlTxReport53 entity = RlTxReport53s.AddNew();
            SetEntityValue(entity, e);
        }

        private RlTxReport53 FindRlTxReport53(String diagNo)
        {
            RlTxReport53Collection coll = RlTxReport53s;
            RlTxReport53 retEntity = null;
            foreach (RlTxReport53 rec in coll)
            {
                if (rec.DiagnosaID.Equals(diagNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(RlTxReport53 entity, GridCommandEventArgs e)
        {
            var userControl = (RlReport5_3Detail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RlTxReportNo = txtRlTxReportNo.Text;
                entity.DiagnosaID = userControl.DiagnoseID;
                entity.DiagnoseName = userControl.DiagnoseName;
                entity.KeluarHidupL = userControl.KeluarHidupL.ToInt();
                entity.KeluarHidupP = userControl.KeluarHidupP.ToInt();
                entity.KeluarMatiL = userControl.KeluarMatiL.ToInt();
                entity.KeluarMatiP = userControl.KeluarMatiP.ToInt();
                entity.Total = entity.KeluarHidupL + entity.KeluarHidupP + entity.KeluarMatiL + entity.KeluarMatiP;

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

                    grdRlReport5_3.Rebind();

                    break;
                case "print":
                    Print(AppConstant.Report.RL5_3);
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

            RlTxReport53s.MarkAllAsDeleted();

            //RlTxReport53.Process(fromMonth, toMonth, year, txtRlTxReportNo.Text, AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48,
            //        AppSession.UserLogin.UserID, RlTxReport53s, out RlTxReport53s);

            var reg = new RegistrationQuery("r");
            var epd = new EpisodeDiagnoseQuery("e");
            var dia = new DiagnoseQuery("d");
            var pat = new PatientQuery("p");

            reg.Select(
                dia.DiagnoseID,
                dia.DiagnoseName,
                string.Format(@"<CASE WHEN p.Sex = 'M' AND r.SRDischargeCondition IN ('{0}', '{1}') 
                                      THEN COUNT(r.RegistrationNo) 
                                      ELSE 0 
                                 END AS KeluarMatiL>", AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48),
                string.Format(@"<CASE WHEN p.Sex = 'M' AND r.SRDischargeCondition NOT IN ('{0}', '{1}') 
                                      THEN COUNT(r.RegistrationNo) 
                                      ELSE 0 
                                 END AS KeluarHidupL>", AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48),
                string.Format(@"<CASE WHEN p.Sex = 'F' AND r.SRDischargeCondition IN ('{0}', '{1}') 
                                      THEN COUNT(r.RegistrationNo) 
                                      ELSE 0 
                                 END AS KeluarMatiP>", AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48),
                string.Format(@"<CASE WHEN p.Sex = 'F' AND r.SRDischargeCondition NOT IN ('{0}', '{1}') 
                                      THEN COUNT(r.RegistrationNo) 
                                      ELSE 0 
                                 END AS KeluarHidupP>", AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48)
                );
            reg.InnerJoin(epd).On(reg.RegistrationNo == epd.RegistrationNo);
            reg.InnerJoin(dia).On(epd.DiagnoseID == dia.DiagnoseID);
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.Where(
                reg.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                reg.IsVoid == false,
                epd.SRDiagnoseType == "DiagnoseType-001"
                );
            if (AppSession.Parameter.IsUsingAllICD10 == "No" && AppSession.Parameter.IsRL5354IncludeICD10O == "No")
                reg.Where(dia.DiagnoseID.Substring(1, 1) != "Z", dia.DiagnoseID.Substring(1, 1) != "O",
                          dia.DiagnoseID.Substring(1, 1) != "V", dia.DiagnoseID.Substring(1, 1) != "W",
                          dia.DiagnoseID.Substring(1, 1) != "X", dia.DiagnoseID.Substring(1, 1) != "Y");
            if (AppSession.Parameter.HealthcareInitial == "RSSA")
                reg.Where(dia.DiagnoseID.Substring(1, 1) != "P", dia.DiagnoseID.Substring(1, 1) != "S");

            if (AppSession.Parameter.IsUsingAllICD10 == "No" && AppSession.Parameter.IsRL5354IncludeICD10O == "Yes")
                reg.Where(dia.DiagnoseID.Substring(1, 1) != "Z",
                          dia.DiagnoseID.Substring(1, 1) != "V", dia.DiagnoseID.Substring(1, 1) != "W",
                          dia.DiagnoseID.Substring(1, 1) != "X", dia.DiagnoseID.Substring(1, 1) != "Y");

            reg.Where(string.Format("<MONTH(r.DischargeDate) BETWEEN {0} AND {1}>", cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue));
            reg.Where(string.Format("<YEAR(r.DischargeDate) = {0}>", txtPeriodYear.Text));

            reg.GroupBy(
                dia.DiagnoseID,
                dia.DiagnoseName,
                pat.Sex,
                reg.SRDischargeCondition
                );

            var dtb = from i in reg.LoadDataTable().AsEnumerable()
                      group i by new
                      {
                          DiagnoseID = i.Field<string>("DiagnoseID"),
                          DiagnoseName = i.Field<string>("DiagnoseName")
                      } into g
                      select new
                      {
                          DiagnoseID = g.Key.DiagnoseID,
                          DiagnoseName = g.Key.DiagnoseName,
                          KeluarHidupL = g.Sum(s => s.Field<int>("KeluarHidupL")),
                          KeluarHidupP = g.Sum(s => s.Field<int>("KeluarHidupP")),
                          KeluarMatiL = g.Sum(s => s.Field<int>("KeluarMatiL")),
                          KeluarMatiP = g.Sum(s => s.Field<int>("KeluarMatiP"))
                      };

            foreach (var row in dtb)
            {
                var entity = RlTxReport53s.AddNew();

                entity.RlTxReportNo = txtRlTxReportNo.Text;
                entity.DiagnosaID = row.DiagnoseID;
                entity.DiagnoseName = row.DiagnoseName;
                entity.KeluarHidupL = row.KeluarHidupL;
                entity.KeluarHidupP = row.KeluarHidupP;
                entity.KeluarMatiL = row.KeluarMatiL;
                entity.KeluarMatiP = row.KeluarMatiP;
                entity.Total = entity.KeluarHidupL + entity.KeluarHidupP + entity.KeluarMatiL + entity.KeluarMatiP;
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
