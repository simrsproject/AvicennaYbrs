using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Windows.Documents.Flow.Model;

namespace Temiang.Avicenna.Module.Charges.Billing
{
    public partial class BillingStatementList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.PrintOutBillingStatement;
            if (!string.IsNullOrEmpty(Request.QueryString["df"]))
            {
                if (Request.QueryString["df"].ToString() == "mkt")
                    ProgramID = AppConstant.Program.PrintOutBillingStatementForMarketing;
            }


            if (!IsPostBack)
            {
                var coll = new ServiceUnitCollection();
                var unit = new ServiceUnitQuery("a");
                unit.Where
                    (
                        //unit.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        unit.SRRegistrationType.In(
                            AppConstant.RegistrationType.EmergencyPatient,
                            AppConstant.RegistrationType.InPatient,
                            AppConstant.RegistrationType.OutPatient,
                            AppConstant.RegistrationType.MedicalCheckUp
                        ),
                        unit.IsActive == true
                    );
                unit.OrderBy(unit.DepartmentID.Ascending);

                coll.Load(unit);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in coll)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                grdRegisteredList.Columns[0].Visible = string.IsNullOrEmpty(Request.QueryString["df"]);
                grdRegisteredList.Columns[1].Visible = string.IsNullOrEmpty(Request.QueryString["df"]) && AppSession.Parameter.HealthcareInitialAppsVersion == "RSGPI";
                grdRegisteredList.Columns[2].Visible = string.IsNullOrEmpty(Request.QueryString["df"]) && AppSession.Parameter.HealthcareInitialAppsVersion == "RSUI"; // print rincian biaya diskon uang R
                grdRegisteredList.Columns[3].Visible = string.IsNullOrEmpty(Request.QueryString["df"]);
                grdRegisteredList.Columns[4].Visible = !string.IsNullOrEmpty(Request.QueryString["df"]);
                grdRegisteredList.Columns[5].Visible = string.IsNullOrEmpty(Request.QueryString["df"]) && AppSession.Parameter.HealthcareInitialAppsVersion == "RSRM";
                if (!string.IsNullOrEmpty(Request.QueryString["df"]))
                {
                    grdRegisteredList.Columns[4].Visible = Request.QueryString["df"].ToString() == "mkt";
                }
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdRegisteredList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = Registrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }

        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) &&
                    rdpRegistrationDate.IsEmpty && rdpDischargeDate.IsEmpty;
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var mrg = new MergeBillingQuery("b");
                var grr = new GuarantorQuery("x");
                var sal = new AppStandardReferenceItemQuery("sal");

                qr.es.Top = AppSession.Parameter.MaxResultRecord;

                qr.Select
                    (
                        qr.RegistrationNo,
                        qr.RegistrationDate,
                        qr.RegistrationTime,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qm.ParamedicName,
                        unit.ServiceUnitName,
                        room.RoomName,
                        qr.BedID,
                        qr.IsTransferedToInpatient,
                        qr.SRRegistrationType,
                        qr.IsHoldTransactionEntry,
                        grr.GuarantorName,
                        sal.ItemName.As("SalutationName")
                    );

                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                qr.InnerJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                qr.LeftJoin(room).On(qr.RoomID == room.RoomID);
                qr.InnerJoin(mrg).On(qr.RegistrationNo == mrg.RegistrationNo);
                qr.InnerJoin(grr).On(qr.GuarantorID == grr.GuarantorID);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);

                var isFilter = false;
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //qr.Where(
                    //    qr.Or(
                    //        qr.RegistrationNo == searchReg,
                    //        qp.MedicalNo == searchReg,
                    //        qp.OldMedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //        string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    Helper.AddFilterMedNoOrRegNoOrPatName(qr, qp, searchReg, "registration");

                    isFilter = true;
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    qr.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                        );
                    isFilter = true;
                }

                qr.Where(qr.IsVoid == false);

                if (rdpRegistrationDate.SelectedDate.HasValue || rdpDischargeDate.SelectedDate.HasValue)
                {
                    qr.es.Top = null;
                    if (rdpRegistrationDate.SelectedDate.HasValue)
                        qr.Where(qr.RegistrationDate.Equal(rdpRegistrationDate.SelectedDate.Value.Date));
                    if (rdpDischargeDate.SelectedDate.HasValue)
                        qr.Where("<CASE r.SRRegistrationType WHEN 'IPR' THEN r.DischargeDate ELSE r.RegistrationDate END = '" + rdpDischargeDate.SelectedDate.Value.ToString("yyyy-MM-dd") + "'>");
                    isFilter = true;
                }

                if (!isFilter || AppSession.Parameter.HealthcareInitialAppsVersion == "RSSA")
                    qr.Where(qr.IsClosed == false);

                qr.OrderBy(qr.RegistrationDate.Descending, qr.ServiceUnitID.Ascending, qr.RegistrationNo.Descending);

                return qr.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdRegisteredList.Rebind();
        }

        private string[] IntermBills(string[] registrationNoList)
        {
            var query = new IntermBillQuery("a");
            //if (rblToGuarantor.SelectedIndex == 1)
            //{
            //    var py = new TransPaymentItemIntermBillQuery("b");
            //    query.LeftJoin(py).On(
            //        query.IntermBillNo == py.IntermBillNo &&
            //        py.IsPaymentProceed == true &&
            //        py.IsPaymentReturned == false
            //    );
            //    var py2 = new TransPaymentItemIntermBillGuarantorQuery("c");
            //    query.LeftJoin(py2).On(
            //        query.IntermBillNo == py2.IntermBillNo &&
            //        py2.IsPaymentProceed == true &&
            //        py2.IsPaymentReturned == false
            //    );
            //}
            //else
            //{
            //    var py = new TransPaymentItemIntermBillGuarantorQuery("b");
            //    query.LeftJoin(py).On(
            //        query.IntermBillNo == py.IntermBillNo &&
            //        py.IsPaymentProceed == true &&
            //        py.IsPaymentReturned == false
            //    );
            //    var py2 = new TransPaymentItemIntermBillQuery("c");
            //    query.LeftJoin(py2).On(
            //        query.IntermBillNo == py2.IntermBillNo &&
            //        py2.IsPaymentProceed == true &&
            //        py2.IsPaymentReturned == false);
            //}

            //var py3 = new TransPaymentItemIntermBillQuery("x");
            //query.LeftJoin(py3).On(
            //    query.IntermBillNo == py3.IntermBillNo &&
            //    py3.IsPaymentProceed == true &&
            //    py3.IsPaymentReturned == false
            //);
            query.Select
                (
                    query.IntermBillNo,
                    query.IntermBillDate,
                    query.StartDate,
                    query.EndDate,
                    query.PatientAmount,
                    query.GuarantorAmount,
                    query.AdministrationAmount,
                    query.GuarantorAdministrationAmount,
                    "<ISNULL(a.DiscAdmPatient, 0) AS DiscAdmPatient>",
                    "<ISNULL(a.DiscAdmGuarantor, 0) AS DiscAdmGuarantor>",
                    //"<CASE WHEN x.PaymentNo IS NOT NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsPatientPaid>",
                    "<ISNULL(a.AskesCoveredSeqNo, '') AS AskesCoveredSeqNo>",
                    query.LastUpdateByUserID,
                    "<CAST(0 AS BIT) AS IsNotAllowEdit>"
                );

            query.Where(
                    query.RegistrationNo.In(registrationNoList),
                    query.IsVoid == false
                );
            query.OrderBy(
                    query.IntermBillNo.Ascending
                );

            DataTable tbl = query.LoadDataTable();

            foreach (DataRow row in tbl.Rows)
            {
                var cc = new CostCalculationCollection();
                cc.Query.Where(cc.Query.IntermBillNo == row["IntermBillNo"].ToString(),
                               cc.Query.RegistrationNo.In(registrationNoList));
                cc.LoadAll();
                if (cc.Count == 0)
                    row.Delete();
            }
            tbl.AcceptChanges();

            return tbl.Rows.Count > 0 ? tbl.AsEnumerable().Select(t => t.Field<string>("IntermBillNo")).ToArray() : new string[1] { string.Empty };
        }

        protected void grdRegisteredList_ItemCommand(object source, GridCommandEventArgs e)
        {
            var isPrint = false;
            var programId = string.Empty;
            var parameterName = string.Empty;
            var parameterValue = string.Empty;

            if (e.CommandName == "Print")
            {
                isPrint = true;

                var jobParameters = new PrintJobParameterCollection();
                string[] registrationNoList = Helper.MergeBilling.GetMergeRegistration(e.CommandArgument.ToString());

                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSUI" || AppSession.Parameter.HealthcareInitialAppsVersion == "RSPM")
                {
                    var jobParameter = jobParameters.AddNew();
                    jobParameter.Name = "IntermBillNoList";
                    jobParameter.ValueString = string.Empty;

                    if (AppSession.Parameter.IsUsingIntermBill)
                    {
                        string[] intermBillNoList = IntermBills(registrationNoList);

                        foreach (var str in intermBillNoList)
                        {
                            jobParameter.ValueString += str + ",";
                        }

                        if (jobParameter.ValueString == string.Empty)
                            return;

                        jobParameter.ValueString = jobParameter.ValueString.Substring(0, jobParameter.ValueString.Length - 1);
                    }


                    var jobParameter2 = jobParameters.AddNew();
                    jobParameter2.Name = "RegistrationNoList";
                    jobParameter2.ValueString = string.Empty;

                    foreach (var str in registrationNoList)
                    {
                        jobParameter2.ValueString += str + ",";
                    }

                    jobParameter2.ValueString = jobParameter2.ValueString.Substring(0, jobParameter2.ValueString.Length - 1);

                    var parRegNo = jobParameters.AddNew();
                    parRegNo.Name = "RegNo";
                    parRegNo.ValueString = e.CommandArgument.ToString();

                    var parUserID = jobParameters.AddNew();
                    parUserID.Name = "UserID";
                    parUserID.ValueString = AppSession.UserLogin.UserID;

                    var parUser = jobParameters.AddNew();
                    parUser.Name = "UserName";
                    parUser.ValueString = AppSession.UserLogin.UserName;

                    var parplafond = jobParameters.AddNew();
                    parplafond.Name = "plafond";

                    var regs = new RegistrationCollection();
                    regs.Query.Where(regs.Query.RegistrationNo.In(registrationNoList));
                    parplafond.ValueString = regs.Query.Load() ? regs.Sum(r => r.PlavonAmount ?? 0).ToString() : string.Empty;

                    var parDate1 = jobParameters.AddNew();
                    parDate1.Name = "StartDate";
                    parDate1.ValueDateTime = txtTransDate1.SelectedDate ?? Convert.ToDateTime("1900-01-01 00:00:00");

                    var parDate2 = jobParameters.AddNew();
                    parDate2.Name = "EndDate";
                    parDate2.ValueDateTime = txtTransDate2.SelectedDate ?? DateTime.Now.AddDays(10);

                    var parSelfGuarantor = jobParameters.AddNew();
                    parSelfGuarantor.Name = "SelfGuarantor";
                    parSelfGuarantor.ValueString = AppSession.Parameter.SelfGuarantor;

                    var parAksesGuarantor = jobParameters.AddNew();
                    parAksesGuarantor.Name = "AskesGuarantor";
                    parAksesGuarantor.ValueString = string.Empty;// AppSession.Parameter.GuarantorAskesID;

                    AppSession.PrintJobReportID = AppConstant.Report.BillingStatementDetail2;

                    parameterName = "IntermBillNoList";
                    parameterValue = jobParameter.ValueString;
                }
                else
                {

                    var jobParameter2 = jobParameters.AddNew();
                    jobParameter2.Name = "RegistrationNoList";
                    jobParameter2.ValueString = string.Empty;
                    foreach (var str in registrationNoList)
                    {
                        jobParameter2.ValueString += str + ",";
                    }
                    jobParameter2.ValueString = jobParameter2.ValueString.Substring(0, jobParameter2.ValueString.Length - 1);

                    switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                    {
                        case "RSSA":
                            {
                                var dpParameter = jobParameters.AddNew();
                                dpParameter.Name = "DownPayment";
                                dpParameter.ValueNumeric = Helper.Payment.GetTotalDownPayment(registrationNoList) - Helper.Payment.GetTotalDownPaymentReturn(registrationNoList);

                                var payParameter = jobParameters.AddNew();
                                payParameter.Name = "PaymentAmount";
                                payParameter.ValueNumeric = Helper.Payment.GetTotalPayment(registrationNoList);

                                var par = jobParameters.AddNew();
                                par.Name = "ParamedicTariffComponentID";
                                par.ValueString = AppSession.Parameter.ParamedicTariffComponentID;

                                var parServiceUnitID = jobParameters.AddNew();
                                parServiceUnitID.Name = "ServiceUnitID";
                                parServiceUnitID.ValueString = string.Empty;

                                var parIncludePrescription = jobParameters.AddNew();
                                parIncludePrescription.Name = "parIncludePrescription";
                                parIncludePrescription.ValueString = "false";

                                AppSession.PrintJobReportID = AppConstant.Report.RssaBillingTemporaryStatement;

                                parameterName = "RegistrationNoList";
                                parameterValue = jobParameter2.ValueString;

                                break;
                            }
                        case "RSRM":
                            {
                                var payParameter = jobParameters.AddNew();
                                payParameter.Name = "PaymentNo";
                                payParameter.ValueString = string.Empty;

                                var ibParameter = jobParameters.AddNew();
                                ibParameter.Name = "IntermBillNoList";
                                ibParameter.ValueString = string.Empty;

                                var parUser = jobParameters.AddNew();
                                parUser.Name = "UserName";
                                parUser.ValueString = AppSession.UserLogin.UserName;

                                var parplafond = jobParameters.AddNew();
                                parplafond.Name = "plafond";

                                var reg = new Registration();
                                reg.LoadByPrimaryKey(e.CommandArgument.ToString());
                                parplafond.ValueNumeric = reg.PlavonAmount ?? 0;

                                var parSelfGuarantor = jobParameters.AddNew();
                                parSelfGuarantor.Name = "SelfGuarantor";
                                parSelfGuarantor.ValueString = AppSession.Parameter.SelfGuarantor;

                                AppSession.PrintJobReportID = AppConstant.Report.BillingIntermStatementPatientDetail;

                                parameterName = "IntermBillNoList";
                                parameterValue = ibParameter.ValueString;

                                break;

                            }

                        //RSGPI, ARSANI
                        default:
                            {
                                var ibParameter = jobParameters.AddNew();
                                ibParameter.Name = "IntermBillNoList";
                                ibParameter.ValueString = string.Empty;

                                var parUser = jobParameters.AddNew();
                                parUser.Name = "UserName";
                                parUser.ValueString = AppSession.UserLogin.UserName;

                                var parplafond = jobParameters.AddNew();
                                parplafond.Name = "plafond";

                                var reg = new Registration();
                                reg.LoadByPrimaryKey(e.CommandArgument.ToString());
                                parplafond.ValueNumeric = reg.PlavonAmount ?? 0;

                                var parSelfGuarantor = jobParameters.AddNew();
                                parSelfGuarantor.Name = "SelfGuarantor";
                                parSelfGuarantor.ValueString = AppSession.Parameter.SelfGuarantor;

                                AppSession.PrintJobReportID = AppConstant.Report.BillingIntermStatementPatientDetail;

                                parameterName = "IntermBillNoList";
                                parameterValue = ibParameter.ValueString;

                                break;
                            }
                    }

                    var parDate1 = jobParameters.AddNew();
                    parDate1.Name = "StartDate";
                    parDate1.ValueDateTime = txtTransDate1.SelectedDate ?? Convert.ToDateTime("1900-01-01 00:00:00");

                    var parDate2 = jobParameters.AddNew();
                    parDate2.Name = "EndDate";
                    parDate2.ValueDateTime = txtTransDate2.SelectedDate ?? DateTime.Now.AddDays(10);

                    var parRegNo = jobParameters.AddNew();
                    parRegNo.Name = "RegNo";
                    parRegNo.ValueString = e.CommandArgument.ToString();

                    var parUserID = jobParameters.AddNew();
                    parUserID.Name = "UserID";
                    parUserID.ValueString = AppSession.UserLogin.UserID;

                    var parAksesGuarantor = jobParameters.AddNew();
                    parAksesGuarantor.Name = "AskesGuarantor";
                    parAksesGuarantor.ValueString = string.Empty;//AppSession.Parameter.GuarantorAskesID;
                }

                AppSession.PrintJobParameters = jobParameters;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                                "oWnd.Show();" +
                                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "PrintForMarketing")
            {

                isPrint = true;

                var jobParameters = new PrintJobParameterCollection();
                string[] registrationNoList = Helper.MergeBilling.GetMergeRegistration(e.CommandArgument.ToString());

                var ibParameter = jobParameters.AddNew();
                ibParameter.Name = "IntermBillNoList";
                ibParameter.ValueString = string.Empty;

                var jobParameter2 = jobParameters.AddNew();
                jobParameter2.Name = "RegistrationNoList";
                jobParameter2.ValueString = string.Empty;
                foreach (var str in registrationNoList)
                {
                    jobParameter2.ValueString += str + ",";
                }
                jobParameter2.ValueString = jobParameter2.ValueString.Substring(0, jobParameter2.ValueString.Length - 1);

                var parRegNo = jobParameters.AddNew();
                parRegNo.Name = "RegNo";
                parRegNo.ValueString = e.CommandArgument.ToString();

                var parUserID = jobParameters.AddNew();
                parUserID.Name = "UserID";
                parUserID.ValueString = AppSession.UserLogin.UserID;

                var parUser = jobParameters.AddNew();
                parUser.Name = "UserName";
                parUser.ValueString = AppSession.UserLogin.UserName;

                var parplafond = jobParameters.AddNew();
                parplafond.Name = "plafond";

                var reg = new Registration();
                reg.LoadByPrimaryKey(e.CommandArgument.ToString());
                parplafond.ValueNumeric = reg.PlavonAmount ?? 0;


                var parDate1 = jobParameters.AddNew();
                parDate1.Name = "StartDate";
                parDate1.ValueDateTime = txtTransDate1.SelectedDate ?? Convert.ToDateTime("1900-01-01 00:00:00");

                var parDate2 = jobParameters.AddNew();
                parDate2.Name = "EndDate";
                parDate2.ValueDateTime = txtTransDate2.SelectedDate ?? DateTime.Now.AddDays(10);

                var parAksesGuarantor = jobParameters.AddNew();
                parAksesGuarantor.Name = "AskesGuarantor";
                parAksesGuarantor.ValueString = string.Empty;

                var parSelfGuarantor = jobParameters.AddNew();
                parSelfGuarantor.Name = "SelfGuarantor";
                parSelfGuarantor.ValueString = AppSession.Parameter.SelfGuarantor;

                AppSession.PrintJobReportID = AppConstant.Report.BillingIntermStatementForMarketing;

                AppSession.PrintJobParameters = jobParameters;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                                "oWnd.Show();" +
                                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);

                parameterName = "IntermBillNoList";
                parameterValue = ibParameter.ValueString;
            }
            else if (e.CommandName == "PrintR")
            {
                isPrint = true;

                var jobParameters = new PrintJobParameterCollection();
                string[] registrationNoList = Helper.MergeBilling.GetMergeRegistration(e.CommandArgument.ToString());

                //if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSUI" || AppSession.Parameter.HealthcareInitialAppsVersion == "RSPM")
                {
                    var jobParameter = jobParameters.AddNew();
                    jobParameter.Name = "IntermBillNoList";
                    jobParameter.ValueString = string.Empty;

                    if (AppSession.Parameter.IsUsingIntermBill)
                    {
                        string[] intermBillNoList = IntermBills(registrationNoList);

                        foreach (var str in intermBillNoList)
                        {
                            jobParameter.ValueString += str + ",";
                        }

                        if (jobParameter.ValueString == string.Empty)
                            return;

                        jobParameter.ValueString = jobParameter.ValueString.Substring(0, jobParameter.ValueString.Length - 1);
                    }

                    var jobParameter2 = jobParameters.AddNew();
                    jobParameter2.Name = "RegistrationNoList";
                    jobParameter2.ValueString = string.Empty;

                    foreach (var str in registrationNoList)
                    {
                        jobParameter2.ValueString += str + ",";
                    }

                    jobParameter2.ValueString = jobParameter2.ValueString.Substring(0, jobParameter2.ValueString.Length - 1);

                    var parRegNo = jobParameters.AddNew();
                    parRegNo.Name = "RegNo";
                    parRegNo.ValueString = e.CommandArgument.ToString();

                    var parUserID = jobParameters.AddNew();
                    parUserID.Name = "UserID";
                    parUserID.ValueString = AppSession.UserLogin.UserID;

                    var parUser = jobParameters.AddNew();
                    parUser.Name = "UserName";
                    parUser.ValueString = AppSession.UserLogin.UserName;

                    var parplafond = jobParameters.AddNew();
                    parplafond.Name = "plafond";

                    var regs = new RegistrationCollection();
                    regs.Query.Where(regs.Query.RegistrationNo.In(registrationNoList));
                    parplafond.ValueString = regs.Query.Load() ? regs.Sum(r => r.PlavonAmount ?? 0).ToString() : string.Empty;

                    var parDate1 = jobParameters.AddNew();
                    parDate1.Name = "StartDate";
                    parDate1.ValueDateTime = txtTransDate1.SelectedDate ?? Convert.ToDateTime("1900-01-01 00:00:00");

                    var parDate2 = jobParameters.AddNew();
                    parDate2.Name = "EndDate";
                    parDate2.ValueDateTime = txtTransDate2.SelectedDate ?? DateTime.Now.AddDays(10);

                    var parSelfGuarantor = jobParameters.AddNew();
                    parSelfGuarantor.Name = "SelfGuarantor";
                    parSelfGuarantor.ValueString = AppSession.Parameter.SelfGuarantor;

                    var parAksesGuarantor = jobParameters.AddNew();
                    parAksesGuarantor.Name = "AskesGuarantor";
                    parAksesGuarantor.ValueString = string.Empty;//AppSession.Parameter.GuarantorAskesID;

                    AppSession.PrintJobReportID = AppConstant.Report.BillingStatementDetail2Pribadi;

                    parameterName = "IntermBillNoList";
                    parameterValue = jobParameter.ValueString;
                }

                AppSession.PrintJobParameters = jobParameters;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                                "oWnd.Show();" +
                                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "PrintRekap")
            {
                isPrint = true;

                var jobParameters = new PrintJobParameterCollection();
                string[] registrationNoList = Helper.MergeBilling.GetMergeRegistration(e.CommandArgument.ToString());

                {
                    var jobParameter = jobParameters.AddNew();
                    jobParameter.Name = "IntermBillNoList";
                    jobParameter.ValueString = string.Empty;

                    if (AppSession.Parameter.IsUsingIntermBill)
                    {
                        string[] intermBillNoList = IntermBills(registrationNoList);

                        foreach (var str in intermBillNoList)
                        {
                            jobParameter.ValueString += str + ",";
                        }

                        if (jobParameter.ValueString == string.Empty)
                            return;

                        jobParameter.ValueString = jobParameter.ValueString.Substring(0, jobParameter.ValueString.Length - 1);
                    }

                    var jobParameter2 = jobParameters.AddNew();
                    jobParameter2.Name = "RegistrationNoList";
                    jobParameter2.ValueString = string.Empty;

                    foreach (var str in registrationNoList)
                    {
                        jobParameter2.ValueString += str + ",";
                    }

                    jobParameter2.ValueString = jobParameter2.ValueString.Substring(0, jobParameter2.ValueString.Length - 1);

                    var parRegNo = jobParameters.AddNew();
                    parRegNo.Name = "RegNo";
                    parRegNo.ValueString = e.CommandArgument.ToString();

                    var parUserID = jobParameters.AddNew();
                    parUserID.Name = "UserID";
                    parUserID.ValueString = AppSession.UserLogin.UserID;

                    var parUser = jobParameters.AddNew();
                    parUser.Name = "UserName";
                    parUser.ValueString = AppSession.UserLogin.UserName;

                    var parplafond = jobParameters.AddNew();
                    parplafond.Name = "plafond";

                    var regs = new RegistrationCollection();
                    regs.Query.Where(regs.Query.RegistrationNo.In(registrationNoList));
                    parplafond.ValueString = regs.Query.Load() ? regs.Sum(r => r.PlavonAmount ?? 0).ToString() : string.Empty;

                    var parDate1 = jobParameters.AddNew();
                    parDate1.Name = "StartDate";
                    parDate1.ValueDateTime = txtTransDate1.SelectedDate ?? Convert.ToDateTime("1900-01-01 00:00:00");

                    var parDate2 = jobParameters.AddNew();
                    parDate2.Name = "EndDate";
                    parDate2.ValueDateTime = txtTransDate2.SelectedDate ?? DateTime.Now.AddDays(10);

                    var parSelfGuarantor = jobParameters.AddNew();
                    parSelfGuarantor.Name = "SelfGuarantor";
                    parSelfGuarantor.ValueString = AppSession.Parameter.SelfGuarantor;

                    var parAksesGuarantor = jobParameters.AddNew();
                    parAksesGuarantor.Name = "AskesGuarantor";
                    parAksesGuarantor.ValueString = string.Empty;//AppSession.Parameter.GuarantorAskesID;

                    AppSession.PrintJobReportID = AppConstant.Report.BillingStatementRekap2;

                    parameterName = "IntermBillNoList";
                    parameterValue = jobParameter.ValueString;
                }

                AppSession.PrintJobParameters = jobParameters;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                                "oWnd.Show();" +
                                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "PrintSum")
            {
                isPrint = true;

                var jobParameters = new PrintJobParameterCollection();
                string[] registrationNoList = Helper.MergeBilling.GetMergeRegistration(e.CommandArgument.ToString());

                switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                {
                    case "RSSA":
                        var parServiceUnitID = jobParameters.AddNew();
                        parServiceUnitID.Name = "ServiceUnitID";
                        parServiceUnitID.ValueString = string.Empty;

                        var parIncludePrescription = jobParameters.AddNew();
                        parIncludePrescription.Name = "parIncludePrescription";
                        parIncludePrescription.ValueString = "false";

                        var parRegNo = jobParameters.AddNew();
                        parRegNo.Name = "RegNo";
                        parRegNo.ValueString = e.CommandArgument.ToString();

                        var parAksesGuarantor = jobParameters.AddNew();
                        parAksesGuarantor.Name = "AskesGuarantor";
                        parAksesGuarantor.ValueString = string.Empty;//AppSession.Parameter.GuarantorAskesID;

                        AppSession.PrintJobReportID = AppConstant.Report.RssaBillingTemporaryStatement;
                        break;
                    //RSGPI, ARSANI
                    default:
                        var parRegistrationNo = jobParameters.AddNew();
                        parRegistrationNo.Name = "RegistrationNo";
                        parRegistrationNo.ValueString = e.CommandArgument.ToString();

                        var payDiscount = jobParameters.AddNew();
                        payDiscount.Name = "PaymentDiscount";
                        payDiscount.ValueNumeric = Helper.Payment.GetTotalPaymentDiscount(registrationNoList);

                        var parUserName = jobParameters.AddNew();
                        parUserName.Name = "UserName";
                        parUserName.ValueString = AppSession.UserLogin.UserID;

                        AppSession.PrintJobReportID = AppConstant.Report.BillingSummary;
                        break;
                }

                var dpParameter = jobParameters.AddNew();
                dpParameter.Name = "DownPayment";
                dpParameter.ValueNumeric = Helper.Payment.GetTotalDownPayment(registrationNoList) - Helper.Payment.GetTotalDownPaymentReturn(registrationNoList);

                var payParameter = jobParameters.AddNew();
                payParameter.Name = "PaymentAmount";
                payParameter.ValueNumeric = Helper.Payment.GetTotalPayment(registrationNoList);

                var jobParameter2 = jobParameters.AddNew();
                jobParameter2.Name = "RegistrationNoList";
                jobParameter2.ValueString = string.Empty;
                foreach (var str in registrationNoList)
                {
                    jobParameter2.ValueString += str + ",";
                }
                jobParameter2.ValueString = jobParameter2.ValueString.Substring(0, jobParameter2.ValueString.Length - 1);

                var par = jobParameters.AddNew();
                par.Name = "ParamedicTariffComponentID";
                par.ValueString = AppSession.Parameter.ParamedicTariffComponentID;

                var parDate1 = jobParameters.AddNew();
                parDate1.Name = "StartDate";
                parDate1.ValueDateTime = txtTransDate1.SelectedDate ?? Convert.ToDateTime("1900-01-01 00:00:00");

                var parDate2 = jobParameters.AddNew();
                parDate2.Name = "EndDate";
                parDate2.ValueDateTime = txtTransDate2.SelectedDate ?? DateTime.Now.AddDays(10);

                var parUserID = jobParameters.AddNew();
                parUserID.Name = "UserID";
                parUserID.ValueString = AppSession.UserLogin.UserID;

                AppSession.PrintJobParameters = jobParameters;

                parameterName = "RegistrationNoList";
                parameterValue = jobParameter2.ValueString;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                                "oWnd.Show();" +
                                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "PrintPresc")
            {
                isPrint = true;

                string[] registrationNoList = Helper.MergeBilling.GetMergeRegistration(e.CommandArgument.ToString());

                PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();
                PrintJobParameter jobParameter = jobParameters.AddNew();
                jobParameter.Name = "RegistrationNoList";
                jobParameter.ValueString = "";
                foreach (var str in registrationNoList)
                {
                    jobParameter.ValueString += str + ",";
                }
                jobParameter.ValueString = jobParameter.ValueString.Substring(0, jobParameter.ValueString.Length - 1);

                jobParameter = jobParameters.AddNew();
                jobParameter.Name = "DownPayment";
                jobParameter.ValueNumeric = Helper.Payment.GetTotalDownPayment(registrationNoList) - Helper.Payment.GetTotalDownPaymentReturn(registrationNoList);

                jobParameter = jobParameters.AddNew();
                jobParameter.Name = "PaymentAmount";
                jobParameter.ValueNumeric = Helper.Payment.GetTotalPayment(registrationNoList);

                var par = jobParameters.AddNew();
                par.Name = "ParamedicTariffComponentID";
                par.ValueString = AppSession.Parameter.ParamedicTariffComponentID;

                var parDate1 = jobParameters.AddNew();
                parDate1.Name = "StartDate";
                parDate1.ValueDateTime = txtTransDate1.SelectedDate ?? Convert.ToDateTime("1900-01-01 00:00:00");

                var parDate2 = jobParameters.AddNew();
                parDate2.Name = "EndDate";
                parDate2.ValueDateTime = txtTransDate2.SelectedDate ?? DateTime.Now.AddYears(1);

                var parRegNo = jobParameters.AddNew();
                parRegNo.Name = "RegNo";
                parRegNo.ValueString = e.CommandArgument.ToString();

                var parUserID = jobParameters.AddNew();
                parUserID.Name = "UserID";
                parUserID.ValueString = AppSession.UserLogin.UserID;

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.BillingPrescription;

                parameterName = "RegistrationNoList";
                parameterValue = jobParameter.ValueString;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }

            if (isPrint && AppSession.Parameter.IsUsedPrintSlipLogForBillingStatement)
                PrintSlipLog.InsertUpdate(AppSession.PrintJobReportID, parameterName, parameterValue, AppSession.UserLogin.UserID);
        }
    }
}
