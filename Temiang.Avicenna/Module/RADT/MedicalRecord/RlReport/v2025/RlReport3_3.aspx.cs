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

namespace Temiang.Avicenna.Module.RADT.MedicalRecord.v2025
{
    public partial class RlReport3_3 : BasePageDetail
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
            var coll = new RlTxReport33V2025Collection();
            coll.Query.Where(coll.Query.RlTxReportNo == entity.RlTxReportNo);
            coll.LoadAll();
            coll.MarkAllAsDeleted();
            coll.Save();

            var newcoll = new RlTxReport33V2025Collection();

            foreach (GridDataItem dataItem in grdRlReport3_3.MasterTableView.Items)
            {
                var reportItemId = dataItem.GetDataKeyValue("RlMasterReportItemID").ToInt();
                var txtPasienRujukan = (dataItem.FindControl("txtPasienRujukan") as RadNumericTextBox);
                var txtPasienNonRujukan = (dataItem.FindControl("txtPasienNonRujukan") as RadNumericTextBox);
                var txtDiRawat = (dataItem.FindControl("txtDiRawat") as RadNumericTextBox);
                var txtDiRujuk = (dataItem.FindControl("txtDiRujuk") as RadNumericTextBox);
                var txtPulang = (dataItem.FindControl("txtPulang") as RadNumericTextBox);
                var txtMatiDiUgdLaki = (dataItem.FindControl("txtMatiDiUgdLaki") as RadNumericTextBox);
                var txtDoaLaki = (dataItem.FindControl("txtDoaLaki") as RadNumericTextBox);
                var txtMatiDiUgdPerempuan = (dataItem.FindControl("txtMatiDiUgdPerempuan") as RadNumericTextBox);
                var txtDoaPerempuan = (dataItem.FindControl("txtDoaPerempuan") as RadNumericTextBox);
                var txtLukaLaki = (dataItem.FindControl("txtLukaLaki") as RadNumericTextBox);
                var txtLukaPerempuan = (dataItem.FindControl("txtLukaPerempuan") as RadNumericTextBox);
                var txtFalseEmergency = (dataItem.FindControl("txtFalseEmergency") as RadNumericTextBox);

                var rpt = newcoll.AddNew();
                rpt.RlTxReportNo = entity.RlTxReportNo;
                rpt.RlMasterReportItemID = reportItemId;
                rpt.PasienRujukan = txtPasienRujukan.Value.ToInt();
                rpt.PasienNonRujukan = txtPasienNonRujukan.Value.ToInt();
                rpt.DiRawat = txtDiRawat.Value.ToInt();
                rpt.DiRujuk = txtDiRujuk.Value.ToInt();
                rpt.Pulang = txtPulang.Value.ToInt();
                rpt.MatiDiUgdLaki = txtMatiDiUgdLaki.Value.ToInt();
                rpt.DoaLaki = txtDoaLaki.Value.ToInt();
                rpt.LastUpdateDateTime = DateTime.Now;
                rpt.LastUpdateByUserID = AppSession.UserLogin.UserID;
                rpt.MatiDiUgdPerempuan = txtMatiDiUgdPerempuan.Value.ToInt();
                rpt.DoaPerempuan = txtDoaPerempuan.Value.ToInt();
                rpt.LukaLaki = txtLukaLaki.Value.ToInt();
                rpt.LukaPerempuan = txtLukaPerempuan.Value.ToInt();
                rpt.FalseEmergency = txtFalseEmergency.Value.ToInt();
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

        protected void grdRlReport3_3_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRlReport3_3.DataSource = RlTxReport33V2025S;
        }

        private RlTxReport33V2025Collection RlTxReport33V2025S
        {
            get
            {
                var obj = ViewState["collRlTxReport33V2025"];
                if (obj != null)
                    return ((RlTxReport33V2025Collection)(obj));

                var collection = new RlTxReport33V2025Collection();

                var query = new RlTxReport33V2025Query("a");
                var mriq = new RlMasterReportItemV2025Query("b");
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
                        coll.PasienRujukan = 0;
                        coll.PasienNonRujukan = 0;
                        coll.DiRawat = 0;
                        coll.DiRujuk = 0;
                        coll.Pulang = 0;
                        coll.MatiDiUgdLaki = 0;
                        coll.DoaLaki = 0;
                        coll.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        coll.LastUpdateDateTime = DateTime.Now;
                        coll.MatiDiUgdPerempuan = 0;
                        coll.DoaPerempuan = 0;
                        coll.LukaLaki = 0;
                        coll.LukaPerempuan = 0;
                        coll.FalseEmergency = 0;
                    }
                }

                ViewState["collRlTxReport33V2025"] = collection;

                return collection;
            }
            set { ViewState["collRlTxReport33V2025"] = value; }
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

                    grdRlReport3_3.Rebind();

                    break;
                case "print":
                    Print(AppConstant.Report.RL3_3V);
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

            foreach (var item in RlTxReport33V2025S)
            {
                int pRujukan = 0, pNonRujukan = 0, pDiRawat = 0, pDiRujuk = 0, pPulang = 0, pMatiDiUgdLaki = 0, pMatiDiUgdPerempuan = 0, pDoaLaki = 0, pDoaPerempuan = 0, pLukaLaki = 0, pLukaPerempuan = 0, pFalseEmergency = 0;

                var mri = new RlMasterReportItemV2025();
                mri.LoadByPrimaryKey(item.RlMasterReportItemID ?? 0);

                string parValue = mri.ParameterValue;
                string parvalue2 = mri.SRParamedicRL1;
                string parValueCode = mri.RlMasterReportItemCode;
                if (!string.IsNullOrEmpty(parValue))
                {
                    RlTxReport33V2025.Process(fromMonth, toMonth, year, parValue, parvalue2, parValueCode, AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48, AppSession.Parameter.DischargeConditionDie,
                           out pRujukan, out pNonRujukan, out pDiRawat, out pDiRujuk, out pPulang, out pMatiDiUgdLaki, out pMatiDiUgdPerempuan, out pDoaLaki, out pDoaPerempuan, out pLukaLaki, out pLukaPerempuan, out pFalseEmergency);
                }

                item.PasienRujukan = pRujukan;
                item.PasienNonRujukan = pNonRujukan;
                item.DiRawat = pDiRawat;
                item.DiRujuk = pDiRujuk;
                item.Pulang = pPulang;
                item.MatiDiUgdLaki = pMatiDiUgdLaki;
                item.DoaLaki = pDoaLaki;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
                item.MatiDiUgdPerempuan = pMatiDiUgdPerempuan;
                item.DoaPerempuan = pDoaPerempuan;
                item.LukaLaki = pLukaLaki;
                item.LukaPerempuan = pLukaPerempuan;
                item.FalseEmergency = pFalseEmergency;
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

            grdRlReport3_3.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                RlTxReport33V2025S = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdRlReport3_3.Rebind();
        }
    }
}
