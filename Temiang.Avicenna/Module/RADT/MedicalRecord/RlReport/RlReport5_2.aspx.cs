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
    public partial class RlReport5_2 : BasePageDetail
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
            var coll = new RlTxReport52Collection();
            coll.Query.Where(coll.Query.RlTxReportNo == entity.RlTxReportNo);
            coll.LoadAll();
            coll.MarkAllAsDeleted();
            coll.Save();

            var newcoll = new RlTxReport52Collection();

            foreach (GridDataItem dataItem in grdRlReport5_2.MasterTableView.Items)
            {
                var reportItemId = dataItem.GetDataKeyValue("RlMasterReportItemID").ToInt();
                var txtJumlah = (dataItem.FindControl("txtJumlah") as RadNumericTextBox);

                var rpt = newcoll.AddNew();
                rpt.RlTxReportNo = entity.RlTxReportNo;
                rpt.RlMasterReportItemID = reportItemId;
                rpt.Jumlah = txtJumlah.Value.ToInt();
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

        protected void grdRlReport5_2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRlReport5_2.DataSource = RlTxReport52S;
        }

        private RlTxReport52Collection RlTxReport52S
        {
            get
            {
                var obj = ViewState["collRlTxReport52"];
                if (obj != null)
                    return ((RlTxReport52Collection)(obj));

                var collection = new RlTxReport52Collection();

                var query = new RlTxReport52Query("a");
                var mriq = new RlMasterReportItemQuery("b");

                query.InnerJoin(mriq).On(query.RlMasterReportItemID == mriq.RlMasterReportItemID);
                query.Select(
                        query,
                        mriq.RlMasterReportItemCode.As("refToRlMasterReportItem_RlMasterReportItemCode"),
                        mriq.RlMasterReportItemName.As("refToRlMasterReportItem_RlMasterReportItemName")
                    );
                query.Where(query.RlTxReportNo == txtRlTxReportNo.Text);
                query.OrderBy(query.RlMasterReportItemID.Ascending);

                collection.Load(query);

                if (collection.Count == 0)
                {
                    var mric = new RlMasterReportItemCollection();
                    mric.Query.Where(mric.Query.RlMasterReportID == Request.QueryString["rptId"]);
                    mric.Query.OrderBy(mric.Query.RlMasterReportItemID.Ascending);
                    mric.LoadAll();
                    foreach (var item in mric)
                    {
                        var coll = collection.AddNew();
                        coll.RlTxReportNo = txtRlTxReportNo.Text;
                        coll.RlMasterReportItemID = item.RlMasterReportItemID;
                        coll.RlMasterReportItemCode = item.RlMasterReportItemCode;
                        coll.RlMasterReportItemName = item.RlMasterReportItemName;
                        coll.Jumlah = 0;
                        coll.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        coll.LastUpdateDateTime = DateTime.Now;
                    }
                }

                ViewState["collRlTxReport52"] = collection;

                return collection;
            }
            set { ViewState["collRlTxReport52"] = value; }
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

                    grdRlReport5_2.Rebind();

                    break;
                case "print":
                    Print(AppConstant.Report.RL5_2);
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

            int total = 0;
            foreach (var item in RlTxReport52S)
            {
                int jml = 0;

                var mri = new RlMasterReportItem();
                mri.LoadByPrimaryKey(item.RlMasterReportItemID ?? 0);

                string parValue = mri.ParameterValue;

                if (item.RlMasterReportItemID == 1756) //TOTAL
                    jml = total;
                else
                {
                    RlTxReport52.Process(fromMonth, toMonth, year, mri.RlMasterReportItemID ?? 0, parValue, AppSession.Parameter.AssasmentObgynPoliKebidanan, AppSession.Parameter.AssasmentObgynPenyKandungan,
                        out jml);
                    total += jml;
                }

                //if (!string.IsNullOrEmpty(parValue))
                //{
                //    //var parValueList = new string[1];
                //    //if (parValue.Contains(","))
                //    var parValueList = parValue.Split(',');

                //    switch (item.RlMasterReportItemID)
                //    {
                //        case 1727: //Penyakit Dalam
                //        case 1728: //Bedah -- KLINIK BEDAH, KLINIK BEDAH PLASTIK, KLINIK BEDAH MINOR, KLINIK BEDAH TUMOR, KLINIK BEDAH ANAK, KLINIK DIGESTIVE

                //        case 1734: //Bedah Saraf
                //        case 1735: //Saraf
                //        case 1736: //Jiwa

                //        case 1738: //Psikologi
                //        case 1739: //THT
                //        case 1740: //Mata
                //        case 1741: //Kulit dan Kelamin
                //        case 1742: //Gigi & Mulut -- Poli Gigi & Bedah Mulut & Ortodenti
                //        case 1743: //Geriatri
                //        case 1744: //Kardiologi
                //        case 1745: //Radiologi
                //        case 1746: //Bedah Orthopedi
                //        case 1747: //Paru - Paru
                //        case 1748: //Kusta
                //        case 1749: //Umum -- Poli Umum, dan Klinik 24 Jam

                //        case 1751: //Rehabilitasi Medik -- Klinik Rehabilitasi Medik
                //        case 1752: //Akupungtur Medik -- 
                //        case 1753: //Konsultasi Gizi
                //        case 1754: //Day Care -- heamodialisa + kemo, VK kamar bersalin, ODC di kamar bedah
                //        case 1755: //Lain - Lain
                //            var regq = new RegistrationQuery("a");
                //            regq.Where(
                //                regq.IsVoid == false,
                //                regq.ServiceUnitID.In(parValueList)
                //                );
                //            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue));
                //            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", txtPeriodYear.Text));

                //            jml = regq.LoadDataTable().Rows.Count;
                //            total += jml;
                //            break;

                //        case 1729: //Kesehatan Anak (Neonatal) -- klinik anak & imunisasi umur 0hari - 28 hari

                //            var regq2 = new RegistrationQuery("a");
                //            regq2.Where(
                //                regq2.IsVoid == false,
                //                regq2.ServiceUnitID.In(parValueList), 
                //                regq2.AgeInYear == 0, 
                //                regq2.AgeInMonth == 0, 
                //                regq2.AgeInDay <= 28
                //                );
                //            regq2.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue));
                //            regq2.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", txtPeriodYear.Text));

                //            jml = regq2.LoadDataTable().Rows.Count;
                //            total += jml;
                //            break;

                //        case 1730: //Kesehatan Anak Lainnya) -- klinik anak & imunisasi umur > 29 hari
                //            var regq2b = new RegistrationQuery("a");
                //            regq2b.Where(
                //                regq2b.IsVoid == false,
                //                regq2b.ServiceUnitID.In(parValueList),
                //                regq2b.AgeInYear == 0,
                //                regq2b.AgeInMonth == 0,
                //                regq2b.AgeInDay > 28
                //                );
                //            regq2b.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue));
                //            regq2b.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", txtPeriodYear.Text));

                //            jml = regq2b.LoadDataTable().Rows.Count;

                //            regq2b = new RegistrationQuery("a");
                //            regq2b.Where(
                //                regq2b.IsVoid == false,
                //                regq2b.ServiceUnitID.In(parValueList),
                //                regq2b.Or(regq2b.AgeInMonth > 0, regq2b.AgeInYear > 0),
                //                regq2b.AgeInYear <= 13
                //                );
                //            regq2b.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue));
                //            regq2b.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", txtPeriodYear.Text));

                //            jml += regq2b.LoadDataTable().Rows.Count;

                //            total += jml;

                //            break;

                //        case 1731: //Obstetri & Ginekologi (Ibu Hamil) -- poli kebidanan dan kia (pemeriksaan kehamilan) ambil dari data SU di Obstetric Type yang Obstetri & Ginekologi (Ibu Hamil)
                //            switch (AppSession.Parameter.HealthcareInitial)
                //            {
                //                case "RSCH":
                //                    var regq3 = new RegistrationQuery("a");
                //                    regq3.Where(
                //                        regq3.IsVoid == false,
                //                        regq3.ServiceUnitID.In(parValueList),
                //                        regq3.SRObstetricType == "01"
                //                        );
                //                    regq3.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue));
                //                    regq3.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", txtPeriodYear.Text));

                //                    jml = regq3.LoadDataTable().Rows.Count;
                //                    break;
                //                default:
                //                    var regO1 = new RegistrationQuery("a");
                //                    var ass1 = new PatientAssessmentQuery("b");
                //                    regO1.InnerJoin(ass1).On(ass1.RegistrationNo == regO1.RegistrationNo);
                //                    regO1.Where(
                //                        regO1.IsVoid == false,
                //                        regO1.ServiceUnitID.In(parValueList),
                //                        ass1.SRAssessmentType == AppSession.Parameter.AssasmentObgynPoliKebidanan
                //                        );
                //                    regO1.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue));
                //                    regO1.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", txtPeriodYear.Text));
                //                    jml = regO1.LoadDataTable().Rows.Count;
                //                    break;
                //            }

                //            total += jml;
                //            break;

                //        case 1732: //Obstetri & Ginekologi Lainnya) -- poli kebidanan dan kia (pemeriksaan kehamilan) ambil dari data SU di Obstetric Type yang Obstetri & Ginekologi (Lainnya)
                //            switch (AppSession.Parameter.HealthcareInitial)
                //            {
                //                case "RSCH":
                //                    var regq4 = new RegistrationQuery("a");
                //                    regq4.Where(
                //                        regq4.IsVoid == false,
                //                        regq4.ServiceUnitID.In(parValueList),
                //                        regq4.SRObstetricType == "02"
                //                        );
                //                    regq4.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue));
                //                    regq4.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", txtPeriodYear.Text));

                //                    jml = regq4.LoadDataTable().Rows.Count;
                //                    break;
                //                default:
                //                    var regO2 = new RegistrationQuery("a");
                //                    var ass2 = new PatientAssessmentQuery("b");
                //                    regO2.InnerJoin(ass2).On(ass2.RegistrationNo == regO2.RegistrationNo);
                //                    regO2.Where(
                //                        regO2.IsVoid == false,
                //                        regO2.ServiceUnitID.In(parValueList),
                //                        ass2.SRAssessmentType == AppSession.Parameter.AssasmentObgynPenyKandungan
                //                        );
                //                    regO2.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue));
                //                    regO2.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", txtPeriodYear.Text));
                //                    jml = regO2.LoadDataTable().Rows.Count;
                //                    break;
                //            }

                //            total += jml;
                //            break;

                //        case 1733: //Keluarga Berencana
                //            switch (AppSession.Parameter.HealthcareInitial)
                //            {
                //                case "RSCH":
                //                    var regq4b = new RegistrationQuery("a");
                //                    regq4b.Where(
                //                        regq4b.IsVoid == false,
                //                        regq4b.ServiceUnitID.In(parValueList),
                //                        regq4b.SRObstetricType.NotIn("01", "02")
                //                        );
                //                    regq4b.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue));
                //                    regq4b.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", txtPeriodYear.Text));

                //                    jml = regq4b.LoadDataTable().Rows.Count;
                //                    break;
                //                default:
                //                    break;
                //            }

                //            total += jml;
                //            break;

                //        case 1737: //Napza -- dari IGD ( laporan narkotika) dari Visit Reason EMR
                //            var regq5 = new RegistrationQuery("a");
                //            regq5.Where(
                //                regq5.IsVoid == false,
                //                regq5.ServiceUnitID.In(parValueList),
                //                regq5.SRVisitReason == "009"
                //                );
                //            regq5.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue));
                //            regq5.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", txtPeriodYear.Text));

                //            jml = regq5.LoadDataTable().Rows.Count;
                //            total += jml;
                //            break;

                //        case 1750: //Rawat Darurat -- IGD
                //            var regq6 = new RegistrationQuery("a");
                //            regq6.Where(
                //                regq6.IsVoid == false,
                //                regq6.ServiceUnitID.In(parValueList),
                //                regq6.Or(regq6.SRVisitReason != "009", regq6.SRVisitReason.IsNull()) 
                //                );
                //            regq6.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", cboPeriodMonthStart.SelectedValue, cboPeriodMonthEnd.SelectedValue));
                //            regq6.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", txtPeriodYear.Text));

                //            jml = regq6.LoadDataTable().Rows.Count;
                //            total += jml;
                //            break;

                //        case 1756: //TOTAL
                //            jml = total;
                //            break;
                //    }
                //}

                item.Jumlah = jml;
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

            grdRlReport5_2.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                RlTxReport52S = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdRlReport5_2.Rebind();
        }
    }
}
