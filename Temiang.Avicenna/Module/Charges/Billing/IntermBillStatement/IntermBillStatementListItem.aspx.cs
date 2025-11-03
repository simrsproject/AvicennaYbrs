using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Charges.Billing
{
    public partial class IntermBillStatementListItem : BasePage
    {
        private string _healthcareInitial, _selfGuarantor;
        private bool _isUsingIntermBill;

        protected void Page_Init(object sender, EventArgs e)
        {
            switch (Request.QueryString["type"])
            {
                case "usr":
                    ProgramID = AppConstant.Program.PrintOutIntermBill;
                    break;
                case "all":
                    ProgramID = AppConstant.Program.PrintOutIntermBillAll;
                    break;
            }

            _healthcareInitial = AppSession.Parameter.HealthcareInitialAppsVersion;
            _isUsingIntermBill = AppSession.Parameter.IsUsingIntermBill;
            _selfGuarantor = AppSession.Parameter.SelfGuarantor;

            if (!IsPostBack)
            {
                PopulateEntryControl();
            }
        }

        private void PopulateEntryControl()
        {
            var regNo = Page.Request.QueryString["regno"];
            if (string.IsNullOrEmpty(regNo))
                return;

            var registration = new Registration();
            registration.LoadByPrimaryKey(regNo);

            txtRegistrationNo.Text = registration.RegistrationNo;

            var patient = new Patient();
            patient.LoadByPrimaryKey(registration.str.PatientID);

            txtMedicalNo.Text = patient.MedicalNo;
            txtPatientName.Text = patient.PatientName;
            var std = new AppStandardReferenceItem();
            txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
            txtGender.Text = patient.Sex;
            txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));
            txtAgeInYear.Text = Convert.ToString(registration.AgeInYear);
            txtAgeInMonth.Text = Convert.ToString(registration.AgeInMonth);
            txtAgeInDay.Text = Convert.ToString(registration.AgeInDay);
            var su = new ServiceUnit();
            su.LoadByPrimaryKey(registration.str.ServiceUnitID);
            txtServiceUnitName.Text = su.ServiceUnitName;

            var sr = new ServiceRoom();
            sr.LoadByPrimaryKey(registration.str.RoomID);
            txtRoomName.Text = sr.RoomName;
            txtBedID.Text = registration.BedID;

            var cls = new Class();
            cls.LoadByPrimaryKey(registration.str.ChargeClassID);
            txtClassName.Text = cls.ClassName;

            cls = new Class();
            cls.LoadByPrimaryKey(registration.str.CoverageClassID);
            txtCoverageClassName.Text = cls.ClassName;

            var par = new Paramedic();
            par.LoadByPrimaryKey(registration.str.ParamedicID);
            txtParamedicName.Text = par.ParamedicName;

            var g = new Guarantor();
            g.LoadByPrimaryKey(registration.str.GuarantorID);
            txtGuarantorName.Text = g.GuarantorName;

            rblToGuarantor.SelectedIndex = registration.GuarantorID == AppSession.Parameter.SelfGuarantor ? 1 : 0;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = IntermBills;
        }

        private DataTable IntermBills
        {
            get
            {
                if (ViewState["IntermBills" + Request.UserHostName] != null)
                    return (DataTable)ViewState["IntermBills" + Request.UserHostName];

                var query = new IntermBillQuery("a");
                if (rblToGuarantor.SelectedIndex == 1)
                {
                    var py = new TransPaymentItemIntermBillQuery("b");
                    query.LeftJoin(py).On(
                        query.IntermBillNo == py.IntermBillNo &&
                        py.IsPaymentProceed == true &&
                        py.IsPaymentReturned == false
                    );
                }
                else
                {
                    var py = new TransPaymentItemIntermBillGuarantorQuery("b");
                    query.LeftJoin(py).On(
                        query.IntermBillNo == py.IntermBillNo &&
                        py.IsPaymentProceed == true &&
                        py.IsPaymentReturned == false
                    );
                }
                query.Select
                    (
                        query.IntermBillNo,
                        query.IntermBillDate,
                        query.StartDate,
                        query.EndDate,
                        query.PatientAmount,
                        query.GuarantorAmount,
                        "<CASE WHEN b.PaymentNo IS NOT NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsPaid>",
                        "<CASE WHEN b.PaymentNo IS NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsUnPaid>"
                    );

                query.Where(
                        query.RegistrationNo.In(MergeRegistrationList()),
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
                                   cc.Query.RegistrationNo.In(MergeRegistrationList()));
                    cc.LoadAll();
                    if (cc.Count == 0)
                        row.Delete();
                }
                tbl.AcceptChanges();

                ViewState["IntermBills" + Request.UserHostName] = tbl;

                return tbl;
            }
            set { ViewState["IntermBills" + Request.UserHostName] = value; }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(source is RadGrid))
                return;

            switch (eventArgument)
            {
                case "rebind":
                    IntermBills = null;
                    grdItem.Rebind();
                    break;
                case "print":
                    switch (_healthcareInitial)
                    {
                        case "RSSMCB":
                            Print(AppConstant.Report.BillingStatementDetail2, false, false);
                            break;
                        default:
                            Print(AppConstant.Report.BillingStatementRekap);
                            break;
                    } 
                    //Print(AppConstant.Report.BillingIntermBillStatement);
                    break;
                case "printr":
                    PrintR(AppConstant.Report.RssaBillingPrescription);
                    break;
                case "printd":
                    PrintBillingInformation(AppConstant.Report.BillingInformation);
                    //Print(AppConstant.Report.DepositIntermBillStatement);
                    break;
            }
        }

        private void Print(string reportName)
        {
            Print(reportName, false, true);
        }

        private void Print(string reportName, bool forceUseNoIntermbill, bool ShowPatientPaid)
        {
            var jobParameters = new PrintJobParameterCollection();

            var jobParameter = jobParameters.AddNew();
            jobParameter.Name = "IntermBillNoList";
            jobParameter.ValueString = string.Empty;

            if (_healthcareInitial == "RSMM")
            {
                string[] intermBillNoList = IntermBillList();
                foreach (var str in intermBillNoList)
                {
                    jobParameter.ValueString += str + ",";
                }

                if (jobParameter.ValueString != string.Empty)
                    jobParameter.ValueString = jobParameter.ValueString.Substring(0, jobParameter.ValueString.Length - 1);
            }
            else
            {
                if (_isUsingIntermBill && !forceUseNoIntermbill)
                {
                    string[] intermBillNoList = IntermBillList();
                    foreach (var str in intermBillNoList)
                    {
                        jobParameter.ValueString += str + ",";
                    }

                    if (jobParameter.ValueString == string.Empty)
                        return;

                    jobParameter.ValueString = jobParameter.ValueString.Substring(0, jobParameter.ValueString.Length - 1);
                }
            }

            string[] registrationNoList = MergeRegistrationList();
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
            parRegNo.ValueString = txtRegistrationNo.Text;

            var parUserID = jobParameters.AddNew();
            parUserID.Name = "UserID";
            parUserID.ValueString = AppSession.UserLogin.UserID;

            var parUser = jobParameters.AddNew();
            parUser.Name = "UserName";
            parUser.ValueString = AppSession.UserLogin.UserName;

            var parplafond = jobParameters.AddNew();
            parplafond.Name = "plafond";
            var oreg = new Registration();
            oreg.LoadByPrimaryKey(txtRegistrationNo.Text);
            parplafond.ValueString = Convert.ToString(oreg.PlavonAmount);

            var parDate1 = jobParameters.AddNew();
            parDate1.Name = "StartDate";
            parDate1.ValueDateTime = Convert.ToDateTime("1900-01-01 00:00:00");

            var parDate2 = jobParameters.AddNew();
            parDate2.Name = "EndDate";
            parDate2.ValueDateTime = (new DateTime()).NowAtSqlServer().AddDays(10);

            var parSelfGuarantor = jobParameters.AddNew();
            parSelfGuarantor.Name = "SelfGuarantor";
            parSelfGuarantor.ValueString = _selfGuarantor;

            var parAksesGuarantor = jobParameters.AddNew();
            parAksesGuarantor.Name = "AskesGuarantor";
            parAksesGuarantor.ValueString = string.Empty;

            if (_healthcareInitial == "RSSMCB")
            {
                var parShowPatientPaid = jobParameters.AddNew();
                parShowPatientPaid.Name = "ShowPatientPaid";
                parShowPatientPaid.ValueNumeric = ShowPatientPaid ? 1 : 0;
            }

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = reportName;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                            "oWnd.Show();" +
                            "oWnd.Maximize();";
            RadAjaxPanel1.ResponseScripts.Add(script);
        }

        private void PrintR(string reportName)
        {
            string[] intermBillNoList = IntermBillList();
            var jobParameters = new PrintJobParameterCollection();

            var jobParameter = jobParameters.AddNew();
            jobParameter.Name = "IntermBillNoList";
            jobParameter.ValueString = string.Empty;

            foreach (var str in intermBillNoList)
            {
                jobParameter.ValueString += str + ",";
            }

            jobParameter.ValueString = jobParameter.ValueString.Substring(0, jobParameter.ValueString.Length - 1);

            string[] registrationNoList = MergeRegistrationList();
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
            parRegNo.ValueString = txtRegistrationNo.Text;

            var parUserID = jobParameters.AddNew();
            parUserID.Name = "UserID";
            parUserID.ValueString = AppSession.UserLogin.UserID;

            var parUser = jobParameters.AddNew();
            parUser.Name = "UserName";
            parUser.ValueString = AppSession.UserLogin.UserName;

            var parplafond = jobParameters.AddNew();
            var oreg = new Registration();
            parplafond.Name = "plafond";
            oreg.LoadByPrimaryKey(txtRegistrationNo.Text);
            parplafond.ValueString = Convert.ToString(oreg.PlavonAmount);

            var parDate1 = jobParameters.AddNew();
            parDate1.Name = "StartDate";
            parDate1.ValueDateTime = Convert.ToDateTime("1900-01-01 00:00:00");

            var parDate2 = jobParameters.AddNew();
            parDate2.Name = "EndDate";
            parDate2.ValueDateTime = DateTime.Now.AddDays(10);

            var parSelfGuarantor = jobParameters.AddNew();
            parSelfGuarantor.Name = "SelfGuarantor";
            parSelfGuarantor.ValueString = AppSession.Parameter.SelfGuarantor;

            var parAksesGuarantor = jobParameters.AddNew();
            parAksesGuarantor.Name = "AskesGuarantor";
            parAksesGuarantor.ValueString = string.Empty;// AppSession.Parameter.GuarantorAskesID;
            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = reportName;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                            "oWnd.Show();" +
                            "oWnd.Maximize();";
            RadAjaxPanel1.ResponseScripts.Add(script);
        }

        private void PrintBillingInformation(string reportName)
        {
            string[] registrationNoList = MergeRegistrationList();

            var jobParameters = new PrintJobParameterCollection();

            var parRegNo = jobParameters.AddNew();
            parRegNo.Name = "RegNo";
            parRegNo.ValueString = txtRegistrationNo.Text;

            var jobParameter = jobParameters.AddNew();
            jobParameter.Name = "RegistrationNoList";
            jobParameter.ValueString = "";
            foreach (var str in registrationNoList)
            {
                jobParameter.ValueString += str + ",";
            }
            jobParameter.ValueString = jobParameter.ValueString.Substring(0, jobParameter.ValueString.Length - 1);

            jobParameter = jobParameters.AddNew();
            jobParameter.Name = "DownPayment";
            jobParameter.ValueNumeric = Helper.Payment.GetTotalDownPayment(registrationNoList);

            var paymentType = new string[3];
            paymentType.SetValue(AppSession.Parameter.PaymentTypePayment, 0);
            paymentType.SetValue(AppSession.Parameter.PaymentTypeCorporateAR, 1);
            paymentType.SetValue(AppSession.Parameter.PaymentTypePersonalAR, 2);
            jobParameter = jobParameters.AddNew();
            jobParameter.Name = "PaymentAmount";
            jobParameter.ValueNumeric = Helper.Payment.GetTotalPayment(registrationNoList, paymentType);

            var jobParameterUser = jobParameters.AddNew();
            jobParameterUser.Name = "UserName";
            jobParameterUser.ValueString = AppSession.UserLogin.UserName;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = reportName;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
            "oWnd.Show();" +
            "oWnd.Maximize();";
            RadAjaxPanel1.ResponseScripts.Add(script);
        }

        public bool GetStatusCheck(object isOrder, object IsOrderRealization, object IsApprove)
        {
            if (IsApprove.Equals(true))
            {
                if (isOrder.Equals(true))
                    return (bool)IsOrderRealization;
                return true;
            }
            return false;
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            foreach (GridDataItem dataItem in grdItem.MasterTableView.Items)
            {
                var status = (sender as CheckBox).Checked;
                var chk = (dataItem.FindControl("detailChkbox") as CheckBox);
                if (chk.Enabled)
                {
                    if (chk.Checked != status)
                        chk.Checked = status;
                }
            }
        }

        private string[] MergeRegistrationList()
        {
            if (ViewState["MergeRegistration" + Request.UserHostName] == null)
                ViewState["MergeRegistration" + Request.UserHostName] = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);

            return (string[])ViewState["MergeRegistration" + Request.UserHostName];
        }

        private string[] IntermBillList()
        {
            int i = 0;
            foreach (GridDataItem dataItem in grdItem.MasterTableView.Items)
            {
                if (((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                    i++;
                
            }
            var arr = new string[i];

            var idx = 0;
            foreach (GridDataItem dataItem in grdItem.MasterTableView.Items)
            {
                if (((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                {
                    arr.SetValue(dataItem["IntermBillNo"].Text, idx);
                    idx++;
                }
            }
            
            return arr;
        }

        protected void rblToGuarantor_OnTextChanged(object sender, EventArgs e)
        {
            IntermBills = null;
            grdItem.Rebind();
        }
    }
}
