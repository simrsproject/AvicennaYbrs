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
    public partial class RlReport5_4 : BasePageDetail
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
            foreach (RlTxReport54 item in RlTxReport54s)
                item.RlTxReportNo = entity.RlTxReportNo;

            using (esTransactionScope trans = new esTransactionScope())
            {
                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                entity.Save();
                RlTxReport54s.Save();

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
            grdRlReport5_4.Columns[0].Visible = isVisible;
            grdRlReport5_4.Columns[grdRlReport5_4.Columns.Count - 1].Visible = isVisible;

            grdRlReport5_4.MasterTableView.CommandItemDisplay = isVisible
                ? GridCommandItemDisplay.Top
                : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                RlTxReport54s = null;

            //Perbaharui tampilan dan data
            grdRlReport5_4.Rebind();
        }

        private RlTxReport54Collection RlTxReport54s
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collRlTxReport54"];
                    if (obj != null)
                        return ((RlTxReport54Collection)(obj));
                }

                var coll = new RlTxReport54Collection();
                var query = new RlTxReport54Query("a");
                var diagQ = new DiagnoseQuery("b");
                query.InnerJoin(diagQ).On(query.DiagnosaID == diagQ.DiagnoseID);
                query.Select(query, diagQ.DiagnoseName.As("refToDiagnose_DiagnoseName"));
                query.Where(query.RlTxReportNo == txtRlTxReportNo.Text);
                query.OrderBy(query.JumlahKunjungan.Descending);
                coll.Load(query);

                Session["collRlTxReport54"] = coll;
                return coll;
            }
            set
            {
                Session["collRlTxReport54"] = value;
            }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            RlTxReport54s = null; //Reset Record Detail
            grdRlReport5_4.DataSource = RlTxReport54s;
            grdRlReport5_4.DataBind();
        }

        protected void grdRlReport5_4_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRlReport5_4.DataSource = RlTxReport54s;
        }

        protected void grdRlReport5_4_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String diagId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][RlTxReport54Metadata.ColumnNames.DiagnosaID]);
            RlTxReport54 entity = FindRlTxReport54(diagId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdRlReport5_4_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String diagId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][RlTxReport54Metadata.ColumnNames.DiagnosaID]);
            RlTxReport54 entity = FindRlTxReport54(diagId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdRlReport5_4_InsertCommand(object source, GridCommandEventArgs e)
        {
            RlTxReport54 entity = RlTxReport54s.AddNew();
            SetEntityValue(entity, e);
        }

        private RlTxReport54 FindRlTxReport54(String diagNo)
        {
            RlTxReport54Collection coll = RlTxReport54s;
            RlTxReport54 retEntity = null;
            foreach (RlTxReport54 rec in coll)
            {
                if (rec.DiagnosaID.Equals(diagNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(RlTxReport54 entity, GridCommandEventArgs e)
        {
            var userControl = (RlReport5_4Detail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RlTxReportNo = txtRlTxReportNo.Text;
                entity.DiagnosaID = userControl.DiagnoseID;
                entity.DiagnoseName = userControl.DiagnoseName;
                entity.KasusBaruL = userControl.KasusBaruL.ToInt();
                entity.KasusBaruP = userControl.KasusBaruP.ToInt();
                entity.JumlahKasusBaru = userControl.JumlahKasusBaru.ToInt();
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

                    grdRlReport5_4.Rebind();

                    break;
                case "print":
                    Print(AppConstant.Report.RL5_4);
                    break;
            }
        }

        private void Process()
        {
            if (string.IsNullOrEmpty(cboPeriodMonthStart.SelectedValue) || string.IsNullOrEmpty(cboPeriodMonthEnd.SelectedValue))
                return;

            if (int.Parse(cboPeriodMonthStart.SelectedValue) > int.Parse(cboPeriodMonthEnd.SelectedValue))
                return;

            RlTxReport54s.MarkAllAsDeleted();

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
                string.Format(@"<(SELECT COUNT(r2.RegistrationNo) 
                                 FROM Registration r2
       		                     INNER JOIN EpisodeDiagnose AS e2 
       			                     ON e2.RegistrationNo = r2.RegistrationNo
       		                     INNER JOIN Patient AS p2 ON r2.PatientID = p2.PatientID
       		                     WHERE e2.DiagnoseID = d.DiagnoseID
       			                     AND r2.SRRegistrationType != '{0}'
				                     AND r2.IsVoid = 0
				                     AND (MONTH(r2.RegistrationDate) BETWEEN {1} AND {2})
				                     AND (YEAR(r2.RegistrationDate) = {3})
       			                     AND p2.Sex = p.Sex) AS JumlahKunjungan>", 
                              AppConstant.RegistrationType.InPatient, 
                              cboPeriodMonthStart.SelectedValue, 
                              cboPeriodMonthEnd.SelectedValue, 
                              txtPeriodYear.Text)
                );
            reg.InnerJoin(epd).On(reg.RegistrationNo == epd.RegistrationNo);
            reg.InnerJoin(dia).On(epd.DiagnoseID == dia.DiagnoseID);
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.Where(
                reg.SRRegistrationType != AppConstant.RegistrationType.InPatient,
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

            reg.Where(string.Format("<MONTH(r.RegistrationDate) BETWEEN {0} AND {1}>", cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue));
            reg.Where(string.Format("<YEAR(r.RegistrationDate) = {0}>", txtPeriodYear.Text));
            reg.GroupBy(
                dia.DiagnoseID,
                dia.DiagnoseName,
                pat.Sex,
                epd.IsOldCase
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
                          KasusBaruL = g.Sum(s => s.Field<int>("KasusBaruL")),
                          KasusBaruP = g.Sum(s => s.Field<int>("KasusBaruP")),
                          JumlahKunjungan = g.Sum(s => s.Field<int>("JumlahKunjungan"))
                      };

            foreach (var row in dtb)
            {
                var entity = RlTxReport54s.AddNew();

                entity.RlTxReportNo = txtRlTxReportNo.Text;
                entity.DiagnosaID = row.DiagnoseID;
                entity.DiagnoseName = row.DiagnoseName;
                entity.KasusBaruL = row.KasusBaruL;
                entity.KasusBaruP = row.KasusBaruP;
                entity.JumlahKasusBaru = entity.KasusBaruL + entity.KasusBaruP;
                entity.JumlahKunjungan = row.JumlahKunjungan;
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
