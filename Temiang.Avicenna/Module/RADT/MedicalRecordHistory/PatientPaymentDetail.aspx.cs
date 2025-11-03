using System;
using System.Data;
using System.Web.UI;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientPaymentDetail : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.MedicalRecordHistory;

            ((Button)Helper.FindControlRecursive(Page, "btnOk")).Visible = false;
            ((Button)Helper.FindControlRecursive(Page, "btnCancel")).Visible = false;
        }
    
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                grdList.DataSource = TransPayments;

                grdList.Columns[13].Visible = AppSession.Parameter.HealthcareInitialAppsVersion != "RSSMCB";
                grdList.Columns[14].Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB";
                grdList.Columns[15].Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB";
                grdList.Columns[16].Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB";
                grdList.Columns[17].Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB";
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;

            string paymentNo = dataItem.GetDataKeyValue("PaymentNo").ToString();

            switch (e.DetailTableView.Name)
            { 

                case "grdDetail":
                    {
                        //Load record
                        var query = new TransPaymentItemQuery("a");
                        var qpaytype = new PaymentTypeQuery("b");
                        var qpaymethod = new PaymentMethodQuery("c");
                        var qcardprovider = new AppStandardReferenceItemQuery("d");
                        var qcardtype = new AppStandardReferenceItemQuery("e");
                        var qdiscreason = new AppStandardReferenceItemQuery("f");
                        var qedcmachine = new EDCMachineQuery("g");

                        query.InnerJoin(qpaytype).On(query.SRPaymentType == qpaytype.SRPaymentTypeID);
                        query.LeftJoin(qpaymethod).On
                            (
                                qpaymethod.SRPaymentMethodID == query.SRPaymentMethod
                                & qpaymethod.SRPaymentTypeID == query.SRPaymentType
                            );
                        query.LeftJoin(qcardprovider).On
                            (
                                qcardprovider.ItemID == query.SRCardProvider &
                                qcardprovider.StandardReferenceID == "CardProvider"
                            );
                        query.LeftJoin(qcardtype).On
                            (
                                qcardtype.ItemID == query.SRCardType &
                                qcardtype.StandardReferenceID == "CardType"
                            );
                        query.LeftJoin(qdiscreason).On
                            (
                                qdiscreason.ItemID == query.SRDiscountReason &
                                qdiscreason.StandardReferenceID == "DiscountReason"
                            );
                        query.LeftJoin(qedcmachine).On(query.EDCMachineID == qedcmachine.EDCMachineID &
                                                       query.SRCardProvider == qedcmachine.SRCardProvider);

                        query.Where(query.PaymentNo == paymentNo);
                        query.OrderBy(query.SequenceNo.Ascending);

                        query.Select
                            (
                                query.PaymentNo,
                                query.SequenceNo,
                                qpaytype.PaymentTypeName,
                                query.Amount,
                                query.Balance,
                                qpaymethod.PaymentMethodName,
                                qcardprovider.ItemName.As("CardProviderName"),
                                qcardtype.ItemName.As("CardTypeName"),
                                qdiscreason.ItemName.As("DiscountReasonName"),
                                qedcmachine.EDCMachineName
                            );

                        e.DetailTableView.DataSource = query.LoadDataTable();

                        break;
                    }

                case "grdDetail2":
                    {
                        var query = new InvoicesItemQuery("a");
                        var hd = new InvoicesQuery("b");

                        query.InnerJoin(hd).On(query.InvoiceNo == hd.InvoiceNo);
                        query.Where(
                            hd.IsInvoicePayment == true,
                            hd.IsVoid == false,
                            hd.IsApproved == true,
                            query.PaymentNo == paymentNo
                        );
                        query.Select(query.InvoiceNo, query.PaymentNo, query.PaymentDate, 
                            query.PaymentAmount, query.OtherAmount);
                        var dt = query.LoadDataTable();
                        e.DetailTableView.DataSource = dt;

                        e.DetailTableView.Visible = (dt.Rows.Count != 0);

                        break;
                    }
            }
            
        }

        private DataTable TransPayments
        {
            get
            {
                string patientID = Page.Request.QueryString["patientID"].ToString();

                DataTable dtdTransPayment = (new TransPaymentCollection()).TransPaymentHistory(patientID, txtRegistrationNo.Text, txtPaymentNo.Text);

                return dtdTransPayment;
            }
        }

        protected void grdList2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList2.DataSource = PatientTransPayments;
        }

        private DataTable PatientTransPayments
        {
            get
            {
                string patientID = Page.Request.QueryString["patientID"].ToString();

                DataTable dtdTransPayment = (new TransPaymentPatientCollection()).TransPaymentPatientHistory(patientID, txtPatientPaymentNo.Text);

                return dtdTransPayment;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void btnFilter2_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList2.Rebind();
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            var p = new TransPayment();
            if (p.LoadByPrimaryKey(e.CommandArgument.ToString()))
            {
                if (e.CommandName == "Print")
                {
                    var jobParameters = new PrintJobParameterCollection();

                    AppSession.PrintJobParameters = jobParameters;

                    string[] registrationNoList = MergeRegistrationList(p.RegistrationNo);
                    var jobParameter2 = jobParameters.AddNew();
                    jobParameter2.Name = "RegistrationNoList";
                    jobParameter2.ValueString = string.Empty;

                    foreach (var str in registrationNoList)
                    {
                        jobParameter2.ValueString += str + ",";
                    }

                    jobParameter2.ValueString = jobParameter2.ValueString.Substring(0, jobParameter2.ValueString.Length - 1);

                    var parPaymentNo = jobParameters.AddNew();
                    parPaymentNo.Name = "PaymentNo";
                    parPaymentNo.ValueString = e.CommandArgument.ToString();

                    var parRegNo = jobParameters.AddNew();
                    parRegNo.Name = "RegNo";
                    parRegNo.ValueString = p.RegistrationNo;

                    var parUserID = jobParameters.AddNew();
                    parUserID.Name = "UserID";
                    parUserID.ValueString = AppSession.UserLogin.UserID;

                    var parUser = jobParameters.AddNew();
                    parUser.Name = "UserName";
                    parUser.ValueString = AppSession.UserLogin.UserName;

                    var parInitialRpt = jobParameters.AddNew();
                    parInitialRpt.Name = "InitialRpt";
                    parInitialRpt.ValueString = "1";

                    var parClassID = jobParameters.AddNew();
                    parClassID.Name = "ClassID";
                    parClassID.ValueString = string.Empty;

                    AppSession.PrintJobParameters = jobParameters;
                    var pay = new TransPayment();
                    pay.LoadByPrimaryKey(e.CommandArgument.ToString());

                    if (pay.IsToGuarantor ?? false)
                        AppSession.PrintJobReportID = AppConstant.Report.BillingGuarantorStatementDetail;
                    else
                        AppSession.PrintJobReportID = AppConstant.Report.BillingPatientStatementDetail2;

                    //ini pake parameter intermbill, bukan paymentno
                    //if (pay.IsToGuarantor ?? false)
                    //    AppSession.PrintJobReportID = AppConstant.Report.BillingGuarantorStatementDetail2;
                    //else
                    //    AppSession.PrintJobReportID = AppConstant.Report.BillingPatientStatementDetail2;

                    string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                    "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                    "oWnd.Show();" +
                    "oWnd.Maximize();";
                    RadAjaxPanel1.ResponseScripts.Add(script);
                }
                else if (e.CommandName == "PrintGuarantorReceipt")
                {
                    var tp = new TransPayment();
                    if (tp.LoadByPrimaryKey(e.CommandArgument.ToString()))
                    {
                        tp.PrintNumber++;
                        if (!tp.IsPrinted ?? false)
                            tp.IsPrinted = true;
                        tp.LastPrintedDateTime = (new DateTime()).NowAtSqlServer();
                        tp.LastPrintedByUserID = AppSession.UserLogin.UserID;
                        tp.Save();
                    }

                    var jobParameters = new PrintJobParameterCollection();

                    var pay = new TransPayment();
                    pay.LoadByPrimaryKey(e.CommandArgument.ToString());
                    if (pay.IsToGuarantor ?? false)
                    {
                        AppSession.PrintJobReportID = AppConstant.Report.RSSA_PaymentRRtInPatientG;

                        var parPaymentNo = jobParameters.AddNew();
                        parPaymentNo.Name = "PaymentNo";
                        parPaymentNo.ValueString = e.CommandArgument.ToString();

                        var parUserName = jobParameters.AddNew();
                        parUserName.Name = "UserName";
                        parUserName.ValueString = AppSession.UserLogin.UserName;

                        var parRegistrationNo = jobParameters.AddNew();
                        parRegistrationNo.Name = "RegistrationNo";
                        parRegistrationNo.ValueString = txtRegistrationNo.Text;

                        var parGuarantorAskesID = jobParameters.AddNew();
                        parGuarantorAskesID.Name = "GuarantorAskesID";
                        parGuarantorAskesID.ValueString = string.Empty;// _guarantorAskesID;
                    }
                    else
                    {
                        AppSession.PrintJobReportID = AppConstant.Report.PaymentReceiptAllDirect;

                        var parPaymentNo = jobParameters.AddNew();
                        parPaymentNo.Name = "PaymentNo";
                        parPaymentNo.ValueString = e.CommandArgument.ToString();

                        var parUserName = jobParameters.AddNew();
                        parUserName.Name = "UserName";
                        parUserName.ValueString = AppSession.UserLogin.UserName;
                    }

                    AppSession.PrintJobParameters = jobParameters;

                    string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                    "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                    "oWnd.Show();" +
                    "oWnd.Maximize();";
                    RadAjaxPanel1.ResponseScripts.Add(script);

                }
                else if (e.CommandName == "PrintGuarantorBillingStatement")
                {
                    var jobParameters = new PrintJobParameterCollection();

                    AppSession.PrintJobParameters = jobParameters;

                    string[] registrationNoList = MergeRegistrationList(p.RegistrationNo);
                    var jobParameter2 = jobParameters.AddNew();
                    jobParameter2.Name = "RegistrationNoList";
                    jobParameter2.ValueString = string.Empty;

                    foreach (var str in registrationNoList)
                    {
                        jobParameter2.ValueString += str + ",";
                    }

                    jobParameter2.ValueString = jobParameter2.ValueString.Substring(0, jobParameter2.ValueString.Length - 1);

                    var parPaymentNo = jobParameters.AddNew();
                    parPaymentNo.Name = "PaymentNo";
                    parPaymentNo.ValueString = e.CommandArgument.ToString();

                    var parRegNo = jobParameters.AddNew();
                    parRegNo.Name = "RegNo";
                    parRegNo.ValueString = p.RegistrationNo;

                    var parUserID = jobParameters.AddNew();
                    parUserID.Name = "UserID";
                    parUserID.ValueString = AppSession.UserLogin.UserID;

                    var parUser = jobParameters.AddNew();
                    parUser.Name = "UserName";
                    parUser.ValueString = AppSession.UserLogin.UserName;

                    var parInitialRpt = jobParameters.AddNew();
                    parInitialRpt.Name = "InitialRpt";
                    parInitialRpt.ValueString = "1";

                    var parClassID = jobParameters.AddNew();
                    parClassID.Name = "ClassID";
                    parClassID.ValueString = string.Empty;

                    AppSession.PrintJobParameters = jobParameters;

                    var pay = new TransPayment();
                    pay.LoadByPrimaryKey(e.CommandArgument.ToString());
                    if (pay.IsToGuarantor ?? false)
                    {
                        AppSession.PrintJobReportID = AppConstant.Report.BillingGuarantorStatementDetail;
                    }
                    else {
                        AppSession.PrintJobReportID = AppConstant.Report.BillingPatientStatementDetail2;
                    }

                    string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                    "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                    "oWnd.Show();" +
                    "oWnd.Maximize();";
                    RadAjaxPanel1.ResponseScripts.Add(script);
                }
                else if (e.CommandName == "PrintGuarantorOnlyBillingStatementNoDiscNoDP")
                {
                    var tp = new TransPayment();
                    if (tp.LoadByPrimaryKey(e.CommandArgument.ToString()))
                    {
                        tp.PrintNumber++;
                        if (!tp.IsPrinted ?? false)
                            tp.IsPrinted = true;
                        tp.LastPrintedDateTime = (new DateTime()).NowAtSqlServer();
                        tp.LastPrintedByUserID = AppSession.UserLogin.UserID;
                        tp.Save();
                    }

                    var jobParameters = new PrintJobParameterCollection();

                    var parPaymentNo = jobParameters.AddNew();
                    parPaymentNo.Name = "PaymentNo";
                    parPaymentNo.ValueString = e.CommandArgument.ToString();

                    var parUserName = jobParameters.AddNew();
                    parUserName.Name = "UserName";
                    parUserName.ValueString = AppSession.UserLogin.UserName;

                    var parShowPatientPaid = jobParameters.AddNew();
                    parShowPatientPaid.Name = "ShowPatientPaid";
                    parShowPatientPaid.ValueNumeric = 4;

                    AppSession.PrintJobParameters = jobParameters;
                    AppSession.PrintJobReportID = AppConstant.Report.BillingStatementPaymentReceiptGuarantorOnly;

                    string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                    "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                    "oWnd.Show();" +
                    "oWnd.Maximize();";
                    RadAjaxPanel1.ResponseScripts.Add(script);
                }
                else if (e.CommandName == "PrintGuarantorOnlyBillingStatement")
                {
                    var tp = new TransPayment();
                    if (tp.LoadByPrimaryKey(e.CommandArgument.ToString()))
                    {
                        tp.PrintNumber++;
                        if (!tp.IsPrinted ?? false)
                            tp.IsPrinted = true;
                        tp.LastPrintedDateTime = (new DateTime()).NowAtSqlServer();
                        tp.LastPrintedByUserID = AppSession.UserLogin.UserID;
                        tp.Save();
                    }

                    var jobParameters = new PrintJobParameterCollection();

                    var parPaymentNo = jobParameters.AddNew();
                    parPaymentNo.Name = "PaymentNo";
                    parPaymentNo.ValueString = e.CommandArgument.ToString();

                    var parUserName = jobParameters.AddNew();
                    parUserName.Name = "UserName";
                    parUserName.ValueString = AppSession.UserLogin.UserName;

                    var parShowPatientPaid = jobParameters.AddNew();
                    parShowPatientPaid.Name = "ShowPatientPaid";
                    parShowPatientPaid.ValueNumeric = 2;

                    AppSession.PrintJobParameters = jobParameters;
                    AppSession.PrintJobReportID = AppConstant.Report.BillingStatementPaymentReceiptGuarantorOnly;

                    string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                    "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                    "oWnd.Show();" +
                    "oWnd.Maximize();";
                    RadAjaxPanel1.ResponseScripts.Add(script);
                }
            }
        }

        private string[] MergeRegistrationList(string regNo)
        {
            if (ViewState["MergeRegistration"] == null)
                ViewState["MergeRegistration"] = Helper.MergeBilling.GetMergeRegistration(regNo);

            return (string[])ViewState["MergeRegistration"];
        }
    }
}