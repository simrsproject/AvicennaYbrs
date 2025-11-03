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

namespace Temiang.Avicenna.Module.RADT.MedicalRecord.RlReport
{
    public partial class RlReport5_2 : BasePageDetail
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
            foreach (RlTxReport52V2025 item in RlTxReport52V2025s)
                item.RlTxReportNo = entity.RlTxReportNo;

            using (esTransactionScope trans = new esTransactionScope())
            {
                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                entity.Save();
                RlTxReport52V2025s.Save();

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
            grdRlReport5_2V2025.Columns[0].Visible = isVisible;
            grdRlReport5_2V2025.Columns[grdRlReport5_2V2025.Columns.Count - 1].Visible = isVisible;

            grdRlReport5_2V2025.MasterTableView.CommandItemDisplay = isVisible
                ? GridCommandItemDisplay.Top
                : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                RlTxReport52V2025s = null;

            //Perbaharui tampilan dan data
            grdRlReport5_2V2025.Rebind();
        }

        private RlTxReport52V2025Collection RlTxReport52V2025s
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collRlTxReport52V2025"];
                    if (obj != null)
                        return ((RlTxReport52V2025Collection)(obj));
                }

                var coll = new RlTxReport52V2025Collection();
                var query = new RlTxReport52V2025Query("a");
                var diagQ = new DiagnoseQuery("b");
                query.InnerJoin(diagQ).On(query.DiagnosaID == diagQ.DiagnoseID);
                query.Select(query, diagQ.DiagnoseName.As("refToDiagnose_DiagnoseName"));
                query.Where(query.RlTxReportNo == txtRlTxReportNo.Text);
                query.OrderBy(query.JumlahKunjungan.Descending);
                coll.Load(query);

                Session["collRlTxReport52V2025"] = coll;
                return coll;
            }
            set
            {
                Session["collRlTxReport52V2025"] = value;
            }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            RlTxReport52V2025s = null; //Reset Record Detail
            grdRlReport5_2V2025.DataSource = RlTxReport52V2025s;
            grdRlReport5_2V2025.DataBind();
        }

        protected void grdRlReport5_2V2025_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRlReport5_2V2025.DataSource = RlTxReport52V2025s;
        }

        protected void grdRlReport5_2V2025_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String diagId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][RlTxReport52V2025Metadata.ColumnNames.DiagnosaID]);
            RlTxReport52V2025 entity = FindRlTxReport52V2025(diagId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdRlReport5_2V2025_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String diagId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][RlTxReport52V2025Metadata.ColumnNames.DiagnosaID]);
            RlTxReport52V2025 entity = FindRlTxReport52V2025(diagId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdRlReport5_2V2025_InsertCommand(object source, GridCommandEventArgs e)
        {
            RlTxReport52V2025 entity = RlTxReport52V2025s.AddNew();
            SetEntityValue(entity, e);
        }

        private RlTxReport52V2025 FindRlTxReport52V2025(String diagNo)
        {
            RlTxReport52V2025Collection coll = RlTxReport52V2025s;
            RlTxReport52V2025 retEntity = null;
            foreach (RlTxReport52V2025 rec in coll)
            {
                if (rec.DiagnosaID.Equals(diagNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(RlTxReport52V2025 entity, GridCommandEventArgs e)
        {
            var userControl = (RlReport5_2Detail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RlTxReportNo = txtRlTxReportNo.Text;
                entity.DiagnosaID = userControl.DiagnoseID;
                entity.DiagnoseName = userControl.DiagnoseName;
                entity.KasusBaruL = userControl.KasusBaruL.ToInt();
                entity.KasusBaruP = userControl.KasusBaruP.ToInt();
                entity.JumlahKasusBaru = userControl.JumlahKasusBaru.ToInt();
                entity.KunjunganL = userControl.KunjunganL.ToInt();
                entity.KunjunganP = userControl.KunjunganP.ToInt();
                entity.JumlahKunjungan = userControl.JumlahKunjungan.ToInt();

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

                    grdRlReport5_2V2025.Rebind();

                    break;
                case "print":
                    Print(AppConstant.Report.RL5_2V2025);
                    break;
            }
        }

        private void Process()
        {
            if (string.IsNullOrEmpty(cboPeriodMonthStart.SelectedValue) || string.IsNullOrEmpty(cboPeriodMonthEnd.SelectedValue))
                return;

            if (int.Parse(cboPeriodMonthStart.SelectedValue) > int.Parse(cboPeriodMonthEnd.SelectedValue))
                return;

            RlTxReport52V2025s.MarkAllAsDeleted();

            var reg = new RegistrationQuery("r");
            var epd = new EpisodeDiagnoseQuery("e");
            var dia = new DiagnoseQuery("d");
            var pat = new PatientQuery("p");

            reg.Select(
                dia.DiagnoseID,
                dia.DiagnoseName,
                @"<CASE WHEN p.Sex = 'M' AND e.IsOldCase = 0 
                        THEN COUNT(r.RegistrationNo) 
                        ELSE 0 
                   END AS KasusBaruL>",
                @"<CASE WHEN p.Sex = 'F' AND e.IsOldCase = 0  
                        THEN COUNT(r.RegistrationNo) 
                        ELSE 0 
                   END AS KasusBaruP>",
                @"<CASE WHEN p.Sex = 'M'
                        THEN COUNT(r.RegistrationNo) 
                        ELSE 0 
                   END AS KunjunganL>",
                @"<CASE WHEN p.Sex = 'F'
                        THEN COUNT(r.RegistrationNo) 
                        ELSE 0 
                   END AS KunjunganP>"
                //(@"<(SELECT COUNT(r2.RegistrationNo) 
                //                 FROM Registration r2
                //              INNER JOIN EpisodeDiagnose AS e2 
                //               ON e2.RegistrationNo = r2.RegistrationNo
                //              INNER JOIN Patient AS p2 ON r2.PatientID = p2.PatientID
                //              WHERE e2.DiagnoseID = d.DiagnoseID
                //               AND r2.SRRegistrationType != '{0}'
                //         AND r2.IsVoid = 0
                //         AND (MONTH(r2.RegistrationDate) BETWEEN {1} AND {2})
                //         AND (YEAR(r2.RegistrationDate) = {3})
                //               AND p2.Sex = p.Sex) AS JumlahKunjungan>", 
                //              AppConstant.RegistrationType.InPatient, 
                //              cboPeriodMonthStart.SelectedValue, 
                //              cboPeriodMonthEnd.SelectedValue, 
                //              txtPeriodYear.Text)
                );
            reg.InnerJoin(epd).On(reg.RegistrationNo == epd.RegistrationNo);
            reg.InnerJoin(dia).On(epd.DiagnoseID == dia.DiagnoseID);
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.Where(
                reg.SRRegistrationType != AppConstant.RegistrationType.InPatient,
                reg.IsVoid == false
                //epd.SRDiagnoseType == "DiagnoseType-001"
                );

            if (AppSession.Parameter.HealthcareInitial == "RSGPI")
                reg.Where(epd.SRDiagnoseType == "DiagnoseType-004");
            else
                reg.Where(epd.SRDiagnoseType == "DiagnoseType-001");


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

            reg.Where(string.Format("<MONTH(r.RegistrationDate) BETWEEN {0} AND {1}>", cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue));
            reg.Where(string.Format("<YEAR(r.RegistrationDate) = {0}>", txtPeriodYear.Text));
            reg.GroupBy(
                dia.DiagnoseID,
                dia.DiagnoseName,
                pat.Sex,
                epd.IsOldCase
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
                           KasusBaruL = g.Sum(s => s.Field<int>("KasusBaruL")),
                           KasusBaruP = g.Sum(s => s.Field<int>("KasusBaruP")),
                           KunjunganL = g.Sum(s => s.Field<int>("KunjunganL")),
                           KunjunganP = g.Sum(s => s.Field<int>("KunjunganP")),
                           JumlahKunjungan = g.Sum(s => s.Field<int>("KunjunganL")) + g.Sum(s => s.Field<int>("KunjunganP"))
                       })
           .OrderByDescending(x => x.JumlahKunjungan)
           .Take(10);

            foreach (var row in dtb)
            {
                var entity = RlTxReport52V2025s.AddNew();

                entity.RlTxReportNo = txtRlTxReportNo.Text;
                entity.DiagnosaID = row.DiagnoseID;
                entity.DiagnoseName = row.DiagnoseName;
                entity.KasusBaruL = row.KasusBaruL;
                entity.KasusBaruP = row.KasusBaruP;
                entity.JumlahKasusBaru = entity.KasusBaruL + entity.KasusBaruP;
                entity.KunjunganL = row.KunjunganL;
                entity.KunjunganP = row.KunjunganP;
                entity.JumlahKunjungan = entity.KunjunganL + entity.KunjunganP;
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
