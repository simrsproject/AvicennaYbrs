using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Reporting;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Xml;
using System.Configuration;
using Temiang.Dal.Interfaces;
using System.Data;
using System.IO;
using System.Reflection;
using Telerik.Reporting.Drawing;
using Telerik.Reporting.Processing;
using Temiang.Avicenna.BusinessObject.Common;
using PictureBox = Telerik.Reporting.PictureBox;
using Report = Telerik.Reporting.Report;
using SubReport = Telerik.Reporting.SubReport;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;

namespace Temiang.Avicenna.Module.Reports
{
    public partial class SsRsViewer : BasePage
    {
        private string _reportPath = string.Empty;
        protected override void OnInit(EventArgs e)
        {
            var fileName = string.Empty;
            var appProgramHC = new AppProgramHealthcare();
            if (appProgramHC.LoadByPrimaryKey(AppSession.PrintJobReportID, AppSession.Parameter.HealthcareInitial))
            {
                _reportPath = string.Format("/{0}", appProgramHC.NavigateUrl);
            }
            else
            {
                var prg = new AppProgram();
                prg.LoadByPrimaryKey(AppSession.PrintJobReportID);
                _reportPath = string.Format("/{0}", prg.NavigateUrl);
            }
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            SaveToSepDocButtonSetting();
            SendToEmailButtonSetting();

            var reportName = string.Empty;
            bool isDirectPrintEnable = false;

            Page.Title = "Print Preview " + reportName;

            reportViewer.ServerReport.ReportServerCredentials = new MyReportServerCredentials();

            var ssrsServerUrl = ConfigurationManager.AppSettings.Get("SsrsServerUrl");
            reportViewer.ServerReport.ReportServerUrl = new System.Uri(ssrsServerUrl);
            reportViewer.ServerReport.ReportPath = _reportPath;

            var rptPars = new List<Microsoft.Reporting.WebForms.ReportParameter>();
            foreach (PrintJobParameter jobPar in AppSession.PrintJobParameters)
            {
                if (!string.IsNullOrEmpty(jobPar.ValueString))
                {
                    rptPars.Add(new Microsoft.Reporting.WebForms.ReportParameter(jobPar.Name, jobPar.ValueString));
                }
                else if (jobPar.ValueDateTime != null)
                {
                    rptPars.Add(new Microsoft.Reporting.WebForms.ReportParameter(jobPar.Name, jobPar.ValueDateTime.Value.ToString(AppConstant.DisplayFormat.DateSql)));
                }
                else if (jobPar.ValueNumeric != null)
                {
                    rptPars.Add(new Microsoft.Reporting.WebForms.ReportParameter(jobPar.Name, jobPar.ValueNumeric.ToString()));
                }
            }

            reportViewer.ServerReport.SetParameters(rptPars);
            reportViewer.ServerReport.Refresh();
            btnDirectPrint.Enabled = isDirectPrintEnable;
        }


        private void SaveToSepDocButtonSetting()
        {
            btnSaveToSepDoc.Enabled = false;
            var sepNo = string.Empty;
            foreach (PrintJobParameter jobParameter in AppSession.PrintJobParameters)
            {
                if (jobParameter.Name.ToLower().Contains("registrationno"))
                {
                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(jobParameter.ValueString) && !string.IsNullOrEmpty(reg.BpjsSepNo))
                    {
                        btnSaveToSepDoc.Enabled = true;
                        sepNo = reg.BpjsSepNo;
                    }

                    break;
                }
                else if (jobParameter.Name.ToLower().Contains("prescriptionno"))
                {
                    var presc = new TransPrescription();
                    if (presc.LoadByPrimaryKey(jobParameter.ValueString))
                    {
                        var reg = new Registration();
                        if (reg.LoadByPrimaryKey(presc.RegistrationNo) && !string.IsNullOrEmpty(reg.BpjsSepNo))
                        {
                            btnSaveToSepDoc.Enabled = true;
                            sepNo = reg.BpjsSepNo;
                        }

                        break;
                    }
                }
                else if (jobParameter.Name.ToLower().Contains("transactionno"))
                {
                    var tc = new TransCharges();
                    if (tc.LoadByPrimaryKey(jobParameter.ValueString))
                    {
                        var reg = new Registration();
                        if (reg.LoadByPrimaryKey(tc.RegistrationNo) && !string.IsNullOrEmpty(reg.BpjsSepNo))
                        {
                            btnSaveToSepDoc.Enabled = true;
                            sepNo = reg.BpjsSepNo;
                        }

                        break;
                    }
                }
                else if (jobParameter.Name.ToLower().Contains("paymentno"))
                {
                    var tc = new TransPayment();
                    if (tc.LoadByPrimaryKey(jobParameter.ValueString))
                    {
                        var reg = new Registration();
                        if (reg.LoadByPrimaryKey(tc.RegistrationNo) && !string.IsNullOrEmpty(reg.BpjsSepNo))
                        {
                            btnSaveToSepDoc.Enabled = true;
                            sepNo = reg.BpjsSepNo;
                        }

                        break;
                    }
                }
            }

            if (btnSaveToSepDoc.Enabled)
            {
                btnSaveToSepDoc.Text = string.Format("Save PDF To SEP Folder: {0}", sepNo);
            }
        }

        private string ParamedicEmail(string paramedicID)
        {
            var emailAddress = string.Empty;
            var par = new Paramedic();
            if (par.LoadByPrimaryKey(paramedicID) && !string.IsNullOrEmpty(par.Email))
            {
                emailAddress = par.Email;
            }

            return emailAddress;
        }

        private string ParamedicEmail()
        {
            var emailAddress = string.Empty;
            foreach (PrintJobParameter jobParameter in AppSession.PrintJobParameters)
            {
                if (jobParameter.Name.ToLower().Contains("paramedicid") && !string.IsNullOrWhiteSpace(jobParameter.ValueString))
                {
                    emailAddress = ParamedicEmail(jobParameter.ValueString);
                    break;
                }
                else if (jobParameter.Name.ToLower().Contains("verificationno"))
                {
                    var pfv = new ParamedicFeeVerification();
                    if (pfv.LoadByPrimaryKey(jobParameter.ValueString))
                    {
                        emailAddress = ParamedicEmail(pfv.ParamedicID);
                    }
                    break;
                }
            }

            return emailAddress;
        }

        private void SendToEmailButtonSetting()
        {
            btnSendToEmail.Enabled = false;
            var emailAddress = ParamedicEmail();

            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                btnSendToEmail.Enabled = true;
                btnSendToEmail.Text = string.Format("Email To {0}", emailAddress);
            }
        }



        protected void btnDirectPrint_Click(object sender, EventArgs e)
        {
            string printerName = PrintManager.CreatePrintJob(AppSession.PrintJobReportID, AppSession.PrintJobParameters);
            string script = printerName != string.Empty ? string.Format("<script type='text/javascript'>alert('Report Print has order to printer {0}');</script>", printerName) : "<script type='text/javascript'>alert('Please contact IT support for defined printer address for print direct');</script>";
            if (!Page.ClientScript.IsStartupScriptRegistered("msgPrint"))
                Page.ClientScript.RegisterStartupScript(this.GetType(), "msgPrint", script);

            //Reset Session
            AppSession.PrintJobReportID = null;
            AppSession.PrintJobParameters = null;

            script = "<script type='text/javascript'>close();</script>";
            //Create Startup Javascript for close window
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeMe", script);
        }

        protected void btnSaveToSepDoc_Click(object sender, EventArgs e)
        {

        }

        protected void btnSendToEmail_Click(object sender, EventArgs e)
        {

        }
    }
    //public class CustomReportCredentials : IReportServerCredentials
    //{
    //    private string _UserName;
    //    private string _PassWord;
    //    private string _DomainName;

    //    public CustomReportCredentials(string UserName, string PassWord, string DomainName)
    //    {
    //        _UserName = UserName;
    //        _PassWord = PassWord;
    //        _DomainName = DomainName;
    //    }

    //    public System.Security.Principal.WindowsIdentity ImpersonationUser
    //    {
    //        get { return null; }
    //    }

    //    public ICredentials NetworkCredentials
    //    {
    //        get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
    //    }

    //    public bool GetFormsCredentials(out Cookie authCookie, out string user,
    //     out string password, out string authority)
    //    {
    //        authCookie = null;
    //        user = password = authority = null;
    //        return false;
    //    }
    //}


    [Serializable]
    public sealed class MyReportServerCredentials : IReportServerCredentials
    {
        public WindowsIdentity ImpersonationUser
        {
            get
            {
                // Use the default Windows user.  Credentials will be
                // provided by the NetworkCredentials property.
                return null;
            }
        }

        public ICredentials NetworkCredentials
        {
            get
            {

                // Read the user information from the Web.config file.  
                // By reading the information on demand instead of 
                // storing it, the credentials will not be stored in 
                // session, reducing the vulnerable surface area to the
                // Web.config file, which can be secured with an ACL.

                // User name
                string userName =
                    ConfigurationManager.AppSettings
                        ["SsrsUserName"];

                if (string.IsNullOrEmpty(userName))
                    throw new Exception(
                        "Missing SSRS user name from web.config file");

                // Password
                string password =
                    ConfigurationManager.AppSettings
                        ["SsrsPassWord"];

                if (string.IsNullOrEmpty(password))
                    throw new Exception(
                        "Missing SSRS password from web.config file");

                // Domain
                string domain =
                    ConfigurationManager.AppSettings
                        ["SsrsDomainName"];

                if (string.IsNullOrEmpty(domain))
                    throw new Exception(
                        "Missing SSRS domain from web.config file");

                return new NetworkCredential(userName, password, domain);
            }
        }


        public bool GetFormsCredentials(out Cookie authCookie,
                out string userName, out string password,
                out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;

            // Not using form credentials
            return false;
        }
    }
}