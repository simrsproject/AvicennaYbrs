using System;
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
using PictureBox = Telerik.Reporting.PictureBox;
using Report = Telerik.Reporting.Report;
using SubReport = Telerik.Reporting.SubReport;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using TextBox = Telerik.Reporting.TextBox;
using RestSharp;
using RestSharp.Authenticators;
using System.Web.UI;
using Newtonsoft.Json;
using Telerik.Web.UI;
using System.Drawing;
using System.Text.Encodings.Web;
using System.Web;
using Microsoft.Owin;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.Reports
{
    public partial class ReportViewer : BasePage
    {
        private bool? _isParameterRequiredComplete = null;

        private static bool? _isGuarantorDocumentPathUseFormula = null;
        private static bool IsGuarantorDocumentPathUseFormula
        {
            get
            {
                if (_isGuarantorDocumentPathUseFormula == null)
                    _isGuarantorDocumentPathUseFormula = AppParameter.IsYes(AppParameter.ParameterItem.IsGuarantorDocumentPathUseFormula);

                return _isGuarantorDocumentPathUseFormula ?? false;
            }
        }
        protected bool IsParameterRequiredComplete()
        {
            if (_isParameterRequiredComplete != null)
                return _isParameterRequiredComplete ?? false;

            // Check parameter
            var rptPars = new AppReportParameterCollection();
            rptPars.Query.Where(rptPars.Query.ProgramID == AppSession.PrintJobReportID);
            rptPars.LoadAll();
            foreach (var par in rptPars)
            {
                if (par.ParameterName.Contains(";"))
                {
                    var pars = par.ParameterName.Split(';');
                    foreach (var parName in pars)
                    {
                        if (AppSession.PrintJobParameters.FindByParameterName(parName) == null)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (AppSession.PrintJobParameters.FindByParameterName(par.ParameterName) == null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["param"]))
            {
                // Extract param
                var jobReport = JObject.Parse(HttpUtility.UrlDecode(Request.QueryString["param"]));

                //var jobReport = new { PrintJobReportID = AppSession.PrintJobReportID, PrintParameters = printPars };
                AppSession.PrintJobReportID = Convert.ToString(jobReport["pjrid"]);
                var printPars = JsonConvert.DeserializeObject<List<PrintParameter>>(Convert.ToString(jobReport["prs"]));

                var printJobParameters = new PrintJobParameterCollection();
                foreach (var item in printPars)
                {
                    printJobParameters.AddNew(item.Name, item.ValueString, item.ValueNumeric, item.ValueDateTime);
                }
                AppSession.PrintJobParameters = printJobParameters;
            }

            var urlRedirect = string.Empty;
            var reportType = "word";
            var fileName = string.Empty;
            var appProgramHC = new AppProgramHealthcare();
            if (appProgramHC.LoadByPrimaryKey(AppSession.PrintJobReportID, AppSession.Parameter.HealthcareInitial))
            {
                reportType = appProgramHC.ProgramType.ToLower();
            }
            else
            {
                var prg = new AppProgram();
                if (!prg.LoadByPrimaryKey(AppSession.PrintJobReportID))
                {
                    throw new Exception(string.Format("ProgramID -> [{0}] in AppProgram not found.", AppSession.PrintJobReportID));
                }
                reportType = prg.ProgramType.ToLower();
            }

            if (string.IsNullOrEmpty(Request.QueryString["param"])) // Jangan diproses lagi jika sudah ada param nya
            {
                var rptAppUrl = ConfigurationManager.AppSettings.Get("ReportAppLocation");
                if (!string.IsNullOrEmpty(rptAppUrl))
                {
                    var printPars = new List<PrintParameter>();
                    foreach (PrintJobParameter item in AppSession.PrintJobParameters)
                    {
                        printPars.Add(new PrintParameter(item.Name, item.ValueString, item.ValueNumeric, item.ValueDateTime));
                    }

                    var jobReport = new { pjrid = AppSession.PrintJobReportID, prs = printPars };
                    var param = HttpUtility.UrlEncode(JsonConvert.SerializeObject(jobReport));
                    switch (reportType)
                    {
                        case "word":
                            urlRedirect = string.Format("{0}/Module/RADT/Cpoe/Common/Phr/ViewAndSignature.aspx?param={1}", rptAppUrl, param);
                            break;
                        case "ssrs":
                            urlRedirect = string.Format("{0}/Module/Reports/SsRsViewer.aspx?param={1}", rptAppUrl, param);
                            break;
                        default:
                            urlRedirect = string.Format("{0}/Module/Reports/ReportViewer.aspx?param={1}", rptAppUrl, param);
                            break;
                    }
                }
                else
                {
                    switch (reportType)
                    {
                        case "word":
                            urlRedirect = "~/Module/RADT/Cpoe/Common/Phr/ViewAndSignature.aspx";
                            break;
                        case "ssrs":
                            urlRedirect = "SsRsViewer.aspx";
                            break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(urlRedirect))
                Response.Redirect(urlRedirect);

            if (Request.Browser.Type.ToLower().Contains("firefox"))
            {
                return;
            }

            base.OnInit(e);
        }

        /// <summary>
        /// Load assemblyClassName in specific assembly file
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="assemblyClassName"></param>
        /// <returns></returns>
        private static IReportDocument LoadReport(string applicationID, string assemblyName, string assemblyClassName)
        {
            Type reportType = null;

            try
            {
                applicationID = ApplicationSettings.ApplicationInfo.Applications[applicationID].Name;
            }
            catch
            {
                applicationID = ApplicationSettings.DefaultApplication.Name;
            }

            // Ovveride Load the assembly from the specified path. 
            var assemblyFileName = assemblyName.Trim() + ".dll";
            var assemblyPath = ApplicationSettings.ApplicationInfo.Applications[applicationID].BinFolderLocation + "\\" + assemblyFileName;
            if (File.Exists(assemblyPath))
            {
                //Load the assembly from the specified path.                    
                var loadingAssembly = Assembly.LoadFrom(assemblyPath);

                //Return the loaded class.
                reportType = loadingAssembly.GetType(assemblyClassName.Trim());
            }
            else
            {
                // Default use report internal
                reportType = Type.GetType(string.Concat(assemblyClassName, ",", assemblyName));
            }


            IReportDocument rpt = null;
            if (reportType != null)
            {
                try
                {
                    rpt = (IReportDocument)Activator.CreateInstance(reportType, AppSession.PrintJobReportID,
                        AppSession.PrintJobParameters,
                        AppSession.UserLogin.UserName);
                }
                catch
                {
                    rpt = (IReportDocument)Activator.CreateInstance(reportType, AppSession.PrintJobReportID,
                        AppSession.PrintJobParameters);
                }

                if (rpt != null)
                {
                    if (ConfigurationManager.AppSettings.Get("VendorIdentifier") == "1")
                    {
                        var pageFt = (from r in ((Report)rpt).Items
                                      where r.GetType().FullName == "Telerik.Reporting.PageFooterSection"
                                      select r).SingleOrDefault();
                        if (pageFt != null)
                        {
                            foreach (var item in pageFt.Items)
                            {
                                if (item is TextBox box)
                                {
                                    box.Format = box.Format.Replace("Avicenna ", "AVIAT ");
                                    box.Value = box.Value.Replace("Avicenna ", "AVIAT ");
                                }
                            }
                        }
                        var rptFt = (from r in ((Report)rpt).Items
                                     where r.GetType().FullName == "Telerik.Reporting.ReportFooterSection"
                                     select r).SingleOrDefault();
                        if (rptFt != null)
                        {
                            foreach (var item in rptFt.Items)
                            {
                                if (item is TextBox box)
                                {
                                    box.Format = box.Format.Replace("Avicenna ", "AVIAT ");
                                    box.Value = box.Value.Replace("Avicenna ", "AVIAT ");
                                }
                            }
                        }
                    }
                }
            }

            return rpt;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            SaveToSepDocButtonSetting();
            SendToEmailButtonSetting();

            var esign = new AppProgramEsign();
            if (!esign.LoadByPrimaryKey(AppSession.PrintJobReportID))
                btnEsign.Enabled = false;

            if (!IsParameterRequiredComplete())
            {
                // Code show popupnya dilakukan di aspx
                return;
            }
            ShowReport();
        }

        private void ShowReport()
        {
            var reportName = string.Empty;
            var programCategory = string.Empty;
            bool isDirectPrintEnable = false;

            rptViewer.ViewMode = AppSession.Parameter.IsRptInPreviewMode ? Telerik.ReportViewer.WebForms.ViewMode.PrintPreview : Telerik.ReportViewer.WebForms.ViewMode.Interactive;

            var prg = new AppProgram();
            prg.LoadByPrimaryKey(AppSession.PrintJobReportID);
            Page.Title = "Print Preview " + prg.ProgramName;
            cboViewerType.SelectedValue = string.IsNullOrEmpty(prg.AccessKey) ? "1" : prg.AccessKey;

            if (cboViewerType.SelectedValue == "1")
            {
                ltEmbed.Visible = false;
                var rpt = InitializedReportDocument(ref reportName, ref isDirectPrintEnable, ref programCategory);
                if (rpt != null) rptViewer.Report = rpt;
            }
            else
            {
                rptViewer.Visible = false;

                var appProgramHC = new AppProgramHealthcare();
                if (appProgramHC.LoadByPrimaryKey(prg.ProgramID, AppSession.Parameter.HealthcareInitial))
                {
                    isDirectPrintEnable = appProgramHC.IsDirectPrintEnable ?? false;
                }
                else
                {
                    isDirectPrintEnable = prg.IsDirectPrintEnable ?? false;
                }

                string embed = "<object id=\"cssLiteral\" data=\"{0}{1}\" type=\"application/pdf\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}{1}&download=1\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = string.Format(embed, ResolveUrl("ReportViewerHandler.ashx?id="), string.Empty);
            }

            btnDirectPrint.Enabled = isDirectPrintEnable;
        }

        private void SaveToGuarantorButtonSeting(string regNo)
        {
            //db:20240108 - penggabungan folder ke noreg utama (cek merge billing)
            var mb = new MergeBilling();
            if (mb.LoadByPrimaryKey(regNo) && mb.FromRegistrationNo != string.Empty)
                regNo = mb.FromRegistrationNo;

            var reg = new Registration();
            if (reg.LoadByPrimaryKey(regNo))
            {
                var gr = new Guarantor();
                gr.LoadByPrimaryKey(reg.GuarantorID);
                var isBpjsPatient = gr.SRGuarantorType.Equals(AppParameter.GetParameterValue(AppParameter.ParameterItem.GuarantorTypeBpjs));
                string folderName;
                if (isBpjsPatient && !string.IsNullOrEmpty(reg.BpjsSepNo))
                {
                    btnSaveToGuarantorDoc.Enabled = true;
                    folderName = reg.BpjsSepNo;
                }
                else
                {
                    btnSaveToGuarantorDoc.Enabled = !gr.SRGuarantorType.Equals(AppParameter.GetParameterValue(AppParameter.ParameterItem.GuarantorTypeSelf)); ;
                    folderName = reg.RegistrationNo.Replace("/", string.Empty);
                }

                if (btnSaveToGuarantorDoc.Enabled)
                {
                    btnSaveToGuarantorDoc.Text = string.Format("Save PDF To Guarantor Document: {0}", folderName);
                }
            }
        }
        private void SaveToSepDocButtonSetting()
        {
            btnSaveToGuarantorDoc.Enabled = false;
            foreach (PrintJobParameter jobParameter in AppSession.PrintJobParameters)
            {
                var lowerName = jobParameter.Name.ToLower();
                if (lowerName.Contains("list")) continue;

                if (lowerName.Contains("nosep") || lowerName.Contains("sepno"))
                {
                    var folderName = jobParameter.ValueString;
                    btnSaveToGuarantorDoc.Enabled = true;
                    btnSaveToGuarantorDoc.Text = string.Format("Save PDF To Guarantor Document: {0}", folderName);
                    break;
                }
                else if (lowerName.Contains("registrationno") || lowerName.Contains("regno"))
                {
                    SaveToGuarantorButtonSeting(jobParameter.ValueString);
                    break;
                }
                else if (lowerName.Contains("prescriptionno"))
                {
                    var presc = new TransPrescription();
                    if (presc.LoadByPrimaryKey(jobParameter.ValueString))
                    {
                        SaveToGuarantorButtonSeting(presc.RegistrationNo);
                        break;
                    }
                }
                else if (lowerName.Contains("transactionno"))
                {
                    var tc = new TransCharges();
                    if (tc.LoadByPrimaryKey(jobParameter.ValueString))
                    {
                        SaveToGuarantorButtonSeting(tc.RegistrationNo);
                        break;
                    }
                }
                else if (lowerName.Contains("paymentno"))
                {
                    var tp = new TransPayment();
                    if (tp.LoadByPrimaryKey(jobParameter.ValueString))
                    {
                        SaveToGuarantorButtonSeting(tp.RegistrationNo);
                        break;
                    }
                }
                else if (lowerName.Contains("resultno"))
                {
                    var pa = new PathologyAnatomy();
                    if (pa.LoadByPrimaryKey(jobParameter.ValueString))
                    {
                        SaveToGuarantorButtonSeting(pa.RegistrationNo);
                        break;
                    }
                }
                else if (lowerName.Contains("bookingno"))
                {
                    var boNo = new ServiceUnitBooking();
                    if (boNo.LoadByPrimaryKey(jobParameter.ValueString) && !string.IsNullOrWhiteSpace(boNo.RegistrationNo))
                    {
                        SaveToGuarantorButtonSeting(boNo.RegistrationNo);
                        break;
                    }
                }
                else if (lowerName.Contains("registrationinfomedicid"))
                {
                    var rim = new RegistrationInfoMedic();
                    if (rim.LoadByPrimaryKey(jobParameter.ValueString))
                    {
                        SaveToGuarantorButtonSeting(rim.RegistrationNo);
                        break;
                    }
                }
            }

            if (btnSaveToGuarantorDoc.Enabled == false)
            {
                // Alternative setting from StandardReference
                var parValue = string.Empty;
                var regNo = GetRegNoUsingStandardReferenceQuery(ref parValue);
                if (!string.IsNullOrEmpty(regNo))
                    SaveToGuarantorButtonSeting(regNo);
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

        private string EmployeeEmail(string personId)
        {
            var emailAddress = string.Empty;

            var contact = new PersonalContactCollection();
            contact.Query.Where(contact.Query.PersonID == personId.ToInt(), contact.Query.SRContactType == AppSession.Parameter.PersonalContactTypeEmail);
            if (contact.Query.Load())
            {
                var email = contact.SingleOrDefault();
                if (email != null)
                    emailAddress = email.ContactValue;
            }

            return emailAddress;
        }

        private string EmailAddress()
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
                else if (jobParameter.Name.ToLower().Contains("medicalno"))
                {
                    var pat = new Patient();
                    pat.Query.Where(pat.Query.MedicalNo == jobParameter.ValueString);
                    pat.Query.es.Top = 1;

                    if (pat.Query.Load())
                    {
                        emailAddress = pat.Email;
                    }
                    break;
                }
                else if (jobParameter.Name.ToLower().Contains("personid") && !string.IsNullOrWhiteSpace(jobParameter.ValueString))
                {
                    emailAddress = EmployeeEmail(jobParameter.ValueString);
                    break;
                }
                else if (jobParameter.Name.ToLower().Contains("registrationno"))
                {
                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(jobParameter.ValueString))
                    {
                        var pat = new Patient();
                        if (pat.LoadByPrimaryKey(reg.PatientID))
                        {
                            emailAddress = pat.Email;
                        }
                    }
                    break;
                }
            }

            return emailAddress;
        }

        private void SendToEmailButtonSetting()
        {
            btnSendToEmail.Enabled = false;
            var emailAddress = EmailAddress();

            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                btnSendToEmail.Enabled = true;
                btnSendToEmail.Text = string.Format("Email To {0}", emailAddress);
            }
        }

        internal static IReportDocument InitializedReportDocument(ref string reportName, ref bool isDirectPrintEnable, ref string programCategory)
        {
            return InitializedReportDocument(ref reportName, ref isDirectPrintEnable, ref programCategory, AppSession.Parameter.HealthcareInitial, AppSession.PrintJobReportID, AppSession.PrintJobParameters);
        }

        internal static IReportDocument InitializedReportDocument(ref string reportName, ref bool isDirectPrintEnable, ref string programCategory, string healthcareInitial, string programID, PrintJobParameterCollection printJobParameters)
        {
            string assemblyClassName;
            string assemblyName;
            string storeProcedureName;
            bool isUsingReportHeader;
            string programType;
            string navigateUrl;

            var appProgram = new AppProgram();
            appProgram.LoadByPrimaryKey(programID);

            reportName = appProgram.ProgramName.Replace("/", string.Empty);
            reportName = reportName.Replace("\\", string.Empty);
            reportName = reportName.Replace(" ", string.Empty);
            programCategory = appProgram.SRProgramCategory;

            var appProgramHC = new AppProgramHealthcare();
            if (appProgramHC.LoadByPrimaryKey(programID, healthcareInitial))
            {
                assemblyClassName = appProgramHC.AssemblyClassName;
                assemblyName = appProgramHC.AssemblyName;
                storeProcedureName = appProgramHC.StoreProcedureName;
                programType = appProgramHC.ProgramType;
                isDirectPrintEnable = appProgramHC.IsDirectPrintEnable ?? false;
                isUsingReportHeader = appProgramHC.IsUsingReportHeader ?? false;
                navigateUrl = appProgramHC.NavigateUrl;
            }
            else
            {
                assemblyClassName = appProgram.AssemblyClassName;
                assemblyName = appProgram.AssemblyName;
                storeProcedureName = appProgram.StoreProcedureName;
                programType = appProgram.ProgramType;
                isDirectPrintEnable = appProgram.IsDirectPrintEnable ?? false;
                isUsingReportHeader = appProgram.IsUsingReportHeader ?? false;
                navigateUrl = appProgram.NavigateUrl;
            }

            // Log access program
            //if (AppSession.Parameter.IsLogProgramAccess)
            //    CreateUserProgramLog(assemblyClassName);


            if (programType == "RPT" || programType == "RSLIP" || programType == "BOOK")
            {
                if (!string.IsNullOrEmpty(assemblyClassName) && !string.IsNullOrEmpty(assemblyName))
                {
                    //var reportType = Type.GetType(string.Concat(assemblyClassName, ",", assemblyName));
                    var rpt = LoadReport(!string.IsNullOrEmpty(appProgram.ApplicationID) ?
                        appProgram.ApplicationID : ApplicationSettings.DefaultApplication.Name, assemblyName, assemblyClassName);

                    //return LoadReport(!string.IsNullOrEmpty(appProgram.ApplicationID) ?
                    //    appProgram.ApplicationID : ApplicationSettings.DefaultApplication.Name, assemblyName, assemblyClassName);

                    // Error: After upgrading to the 15.0.21.224 or higher Reporting version, the reports that don't receive data may not be rendered.
                    // In the older versions, the same reports would have the sections that don't display data as header and footer rendered.
                    // The solution is setting SkipBlankPages of your Reports to False, which will revert the previous behavior. (Handono 230310)
                    ((Telerik.Reporting.Report)rpt).SkipBlankPages = false;

                    rpt.DocumentName = reportName; //Untuk keperluan penamaan file export dari toolbar save (Handono 230406)
                    return rpt;
                }
            }
            else if (programType == "XML")
            {
                var rpt = LoadReportXML(AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitial), printJobParameters, navigateUrl, isUsingReportHeader, storeProcedureName);
                rpt.DocumentName = reportName; //Untuk keperluan penamaan file export dari toolbar save (Handono 230406)
                return rpt;
            }

            return null;
        }

        private static IReportDocument LoadReportXML(string healthcareInitial, PrintJobParameterCollection printJobParameters, string navigateUrl, bool isUsingReportHeader, string storeProcedureName)
        {
            using (var xmlRptReader =
                XmlReader.Create(ConfigurationManager.AppSettings.Get("ReportUrlLocation") + navigateUrl,
                    new XmlReaderSettings() { IgnoreWhitespace = true }))
            {
                var report =
                    (Report)new Telerik.Reporting.XmlSerialization.ReportXmlSerializer().Deserialize(xmlRptReader);

                if (isUsingReportHeader)
                {
                    using (var xmlSubReader =
                        XmlReader.Create(ConfigurationManager.AppSettings.Get("ReportHeaderUrlLocation"),
                            new XmlReaderSettings() { IgnoreWhitespace = true }))
                    {
                        var subSrc =
                            (Report)new Telerik.Reporting.XmlSerialization.ReportXmlSerializer().Deserialize(xmlSubReader);
                        (subSrc.DataSource as SqlDataSource).ConnectionString = esConfigSettings.ConnectionInfo
                            .Connections[esConfigSettings.ConnectionInfo.Default].ConnectionString;

                        var pict = (from x in subSrc.Items["reportHeader"].Items
                                    where x.Name == "pictureBox1"
                                    select x).SingleOrDefault();
                        if (pict != null)
                            (pict as PictureBox).Value =
                                ReportLibrary.Helper.ResourceLogo(
                                    healthcareInitial);

                        var sub = new SubReport
                        {
                            Location = new PointU(new Unit(0, UnitType.Inch), new Unit(0, UnitType.Inch)),
                            Size = new SizeU(new Unit(5, UnitType.Inch), new Unit(0.8, UnitType.Inch)),
                            ReportSource = subSrc
                        };

                        var pageHd = (from r in report.Items
                                      where r.GetType().FullName == "Telerik.Reporting.ReportHeaderSection"
                                      select r).SingleOrDefault();
                        if (pageHd != null) pageHd.Items.Add(sub);
                    }
                }

                if (ConfigurationManager.AppSettings.Get("VendorIdentifier") == "1")
                {
                    var pageFt = (from r in report.Items
                                  where r.GetType().FullName == "Telerik.Reporting.PageFooterSection"
                                  select r).SingleOrDefault();
                    if (pageFt != null)
                    {
                        foreach (var item in pageFt.Items)
                        {
                            if (item is TextBox box)
                            {
                                box.Format = box.Format.Replace("Avicenna ", "AVIAT ");
                                box.Value = box.Value.Replace("Avicenna ", "AVIAT ");
                            }
                        }
                    }
                    var rptFt = (from r in report.Items
                                 where r.GetType().FullName == "Telerik.Reporting.ReportFooterSection"
                                 select r).SingleOrDefault();
                    if (rptFt != null)
                    {
                        foreach (var item in rptFt.Items)
                        {
                            if (item is TextBox box)
                            {
                                box.Format = box.Format.Replace("Avicenna ", "AVIAT ");
                                box.Value = box.Value.Replace("Avicenna ", "AVIAT ");
                            }
                        }
                    }
                }

                if (report.DataSource is SqlDataSource)
                {
                    var conn = (SqlDataSource)report.DataSource;
                    conn.ConnectionString = esConfigSettings.ConnectionInfo
                        .Connections[esConfigSettings.ConnectionInfo.Default]
                        .ConnectionString;
                    conn.CommandTimeout = int.MaxValue;
                    if (!string.IsNullOrWhiteSpace(storeProcedureName))
                    {
                        conn.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
                        conn.SelectCommand = storeProcedureName;
                    }

                    foreach (var param in conn.Parameters)
                    {
                        var pars = (printJobParameters.Where(p =>
                            p.Name.Trim() == param.Name.Trim().Replace("@", string.Empty)));
                        if (pars.Count() == 0)
                            continue; // parameter tidak ketemu. ada report yang parameternya flexible jadi skip kl tidak ketemu
                        var par = pars.Single();
                        switch (param.DbType)
                        {
                            case DbType.DateTime:
                            case DbType.DateTime2:
                                param.Value = par.ValueDateTime;
                                break;
                            case DbType.AnsiString:
                            case DbType.AnsiStringFixedLength:
                            case DbType.String:
                            case DbType.StringFixedLength:
                                param.Value = par.ValueString;
                                break;
                            case DbType.Decimal:
                            case DbType.Double:
                            case DbType.Int16:
                            case DbType.Int32:
                            case DbType.Int64:
                            case DbType.Single:
                                param.Value = par.ValueNumeric;
                                break;
                        }
                    }
                }
                else if (report.DataSource is WebServiceDataSource)
                {
                    var ds = (WebServiceDataSource)report.DataSource;

                    if (!Helper.IsJson(storeProcedureName))
                    {
                        var serviceUrl = string.Format("{0}/{1}",
                            ConfigurationManager.AppSettings.Get("WebServiceDataSourceUrlRoot"), storeProcedureName);

                        ds.ServiceUrl = serviceUrl;
                        foreach (WebServiceParameter wsPar in ds.Parameters)
                        {
                            //var pars = (AppSession.PrintJobParameters.Where(p =>
                            //    p.Name.Trim().ToLower() == wsPar.Name.Trim().ToLower()));
                            var pars = (printJobParameters.Where(p =>
                                p.Name.Trim().ToLower() == wsPar.Name.Trim().ToLower()));
                            
                            if (pars.Count() == 0)
                                continue;
                            var par = pars.Single();
                            if (!string.IsNullOrEmpty(par.ValueString))
                                wsPar.Value = par.ValueString;
                            else if (par.ValueNumeric != null)
                                wsPar.Value = par.ValueNumeric;
                            else if (par.ValueDateTime != null)
                                wsPar.Value = par.ValueDateTime;
                        }
                    }
                    else
                    {
                        var jArray = Helper.JsonStrToArray(storeProcedureName);

                        var serviceUrl = string.Format("{0}/{1}",
                            ConfigurationManager.AppSettings.Get("WebServiceDataSourceUrlRoot"),
                            jArray["serviceurl"].ToString());

                        ds.ServiceUrl = serviceUrl;
                        foreach (WebServiceParameter wsPar in ds.Parameters)
                        {
                            if (wsPar.Name.Trim().ToLower() == "accesskey")
                            {
                                wsPar.Value = "sciadmin88";
                            }
                            else if (wsPar.Name.Trim().ToLower() == "jsonqueryandparam")
                            {
                                foreach (var p in printJobParameters)
                                {
                                    storeProcedureName = storeProcedureName.Replace("@" + p.Name, p.ValueString);
                                }

                                wsPar.Value = storeProcedureName;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }

                UpdateCheckBox(report.Items);

                // Error: After upgrading to the 15.0.21.224 or higher Reporting version, the reports that don't receive data may not be rendered.
                // In the older versions, the same reports would have the sections that don't display data as header and footer rendered.
                // The solution is setting SkipBlankPages of your Reports to False, which will revert the previous behavior. (Handono 230310)
                ((Telerik.Reporting.Report)report).SkipBlankPages = false;

                return report;
            }
        }

        private static void UpdateCheckBox(Telerik.Reporting.ReportItemBase.ItemCollection items)
        {
            foreach (var item in items)
            {
                if (item.Items.Count > 0)
                    UpdateCheckBox(item.Items);
                else
                    if (item is Telerik.Reporting.CheckBox)
                {
                    var chk = (Telerik.Reporting.CheckBox)item;
                    chk.IndeterminateImage = ReportLibrary.Properties.Resources.UnChecked16;
                    chk.UncheckedImage = ReportLibrary.Properties.Resources.UnChecked16;
                    chk.CheckedImage = ReportLibrary.Properties.Resources.Checked16;
                }
            }
        }

        private static void CreateUserProgramLog(string assemblyClassName)
        {
            if (!string.IsNullOrEmpty(AppSession.PrintJobReportID) && AppSession.UserLogin.UserLogID != null)
            {
                var log = new UserProgramLog();
                log.UserLogID = AppSession.UserLogin.UserLogID;
                log.ProgramID = AppSession.PrintJobReportID;
                log.AccessDateTime = (new DateTime()).NowAtSqlServer();

                var parValue = string.Concat(assemblyClassName, Environment.NewLine, "Parameter: ");
                if (AppSession.PrintJobParameters != null)
                {
                    foreach (PrintJobParameter par in AppSession.PrintJobParameters)
                    {
                        parValue = string.Concat(parValue, Environment.NewLine,
                            string.Format("{0} val:s:{1}|n:{2}|d:{3}", par.Name, par.ValueString, par.ValueNumeric,
                                par.ValueDateTime
                                ));
                    }
                }
                log.Parameter = parValue;
                log.Save();
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


        protected void btnSaveToGuarantorDoc_Click(object sender, EventArgs e)
        {
            var filePath = SaveFileToGuarantorDocument(AppSession.Parameter.HealthcareInitial, AppSession.PrintJobReportID, AppSession.PrintJobParameters);

            string script = string.Empty;
            if (!string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath))
            {
                script = string.Format("<script type=\"text/javascript\">alert(\"Report has export to {0}\");</script>", filePath.Replace(@"\", @"\\"));
            }
            else
                script = "<script type='text/javascript'>alert('Save failed');</script>";

            if (!Page.ClientScript.IsStartupScriptRegistered("msgSave"))
                Page.ClientScript.RegisterStartupScript(this.GetType(), "msgSave", script);
        }

        internal static string GuarantorDocumentFilePath(string registrationNo, string programID, string reportName, string extension, ref string regType, ref string guarantorID, string bpjsSepNo)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);
            regType = reg.SRRegistrationType;
            guarantorID = reg.GuarantorID;

            if (IsGuarantorDocumentPathUseFormula)
            {
                var pathFormula = AppParameter.GetParameterValue(AppParameter.ParameterItem.GuarantorDocumentPathFormula);
                if (!string.IsNullOrWhiteSpace(pathFormula))
                {
                    //var gr = new Guarantor();
                    //gr.LoadByPrimaryKey(guarantorID);
                    //var isBpjs = gr.SRGuarantorType.Equals(AppParameter.GetParameterValue(AppParameter.ParameterItem.GuarantorTypeBpjs));

                    var docDate = DateTime.Today;
                    if (!string.IsNullOrEmpty(bpjsSepNo))
                    {
                        var bpjsSEP = new BpjsSEP();
                        bpjsSEP.Query.Where(bpjsSEP.Query.NoSEP == bpjsSepNo);
                        bpjsSEP.Query.es.Top = 1;
                        if (!bpjsSEP.Query.Load())
                        {
                            var patient = new Patient();
                            patient.LoadByPrimaryKey(reg.PatientID);

                            docDate = reg.RegistrationDate.Value;
                        }
                        else
                        {
                            docDate = bpjsSEP.TanggalSEP.Value;
                        }
                    }

                    var fileNameFormula = AppParameter.GetParameterValue(AppParameter.ParameterItem.GuarantorDocumentFileNameFormula);
                    var fileName = FileNameUseFormula(fileNameFormula, programID, reg, docDate, extension, reportName);

                    var path = GuarantorDocumentFilePathUseFormula(reg, bpjsSepNo, pathFormula, programID, docDate);

                    var filePath = Path.Combine(path, fileName);
                    return filePath;
                }
            }

            return GuarantorDocumentFilePathDefault(reg, programID, reportName, extension, ref regType, ref guarantorID, bpjsSepNo);
        }

        private static string GuarantorDocumentFilePathUseFormula(Registration reg, string bpjsSepNo, string pathFormula, string programID, DateTime docDate)
        {
            //Dari klien ada minta untuk save to guarantor itu, urutan folder nya :
            //1. Bulan+tahun (0723)
            //2. RegistrationType (Rawat Jalan atau Inap)
            //3. Tanggal
            //4. Nama File : 
            //	- 6digit belakang SEP No + A1 = Dokumen Print INACBG E-KLAIM
            //	- 6digit Belakang SEP No + A2 = Print SEP
            //	- 6digit Belakang SEP No + A3 = Cetakan Resume Kunjungan Pasien Rawat Jalan
            //	- 6digit Belakang SEP No + A4 = Surat Kontrol Rawat Jalan
            //	- 6digit Belakang SEP No + F  = Kunjungan Rehab Medis/Fisioterapi
            //	- 6digit belakang SEP No + Z = Billing Statement Pasien



            // MM+YY>RT>DD
            var path = string.Empty;
            var pathFormulas = pathFormula.Split('>');
            foreach (var formula in pathFormulas)
            {
                if (formula.Contains("+"))
                {
                    var subs = formula.Split('+');
                    foreach (var sub in subs)
                        path = string.Concat(path, PathPartValue(sub, reg, docDate, programID));
                }
                else
                    path = string.Concat(path, "\\", PathPartValue(formula, reg, docDate, programID));
            }

            var sepFolder = AppParameter.GetParameterValue(AppParameter.ParameterItem.SepFolder);
            if (string.IsNullOrWhiteSpace(sepFolder))
                sepFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "BpjsSepDocument");

            return string.Concat(sepFolder, "\\", path.Trim());
        }

        private static string PathPartValue(string pathPart, Registration reg, DateTime docDate, string programID, string txNo = "")
        {
            //4. Nama File :  BPJSSEPNO(R6)+DG+_DI+_TN
            //	- 6digit belakang SEP No + A1 = Dokumen Print INACBG E-KLAIM
            //	- 6digit Belakang SEP No + A2 = Print SEP
            //	- 6digit Belakang SEP No + A3 = Cetakan Resume Kunjungan Pasien Rawat Jalan
            //	- 6digit Belakang SEP No + A4 = Surat Kontrol Rawat Jalan
            //	- 6digit Belakang SEP No + F  = Kunjungan Rehab Medis/Fisioterapi
            //	- 6digit belakang SEP No + Z = Billing Statement Pasien

            if (string.IsNullOrEmpty(pathPart))
                return String.Empty;

            var oriPathPart = pathPart;

            if (pathPart.Length == 1)
                return pathPart;

            if (pathPart.Contains("_"))
                pathPart = pathPart.Replace("_", string.Empty);

            switch (pathPart)
            {
                case "MM":
                    return oriPathPart.Replace(pathPart, string.Format("{0:00}", docDate.Month));
                case "DD":
                    return oriPathPart.Replace(pathPart, string.Format("{0:00}", docDate.Day));
                case "YY":
                    return oriPathPart.Replace(pathPart, string.Format("{0:0000}", docDate.Year).Substring(2));
                case "YYYY":
                    return oriPathPart.Replace(pathPart, string.Format("{0:0000}", docDate.Year));
                case "RT":
                    return oriPathPart.Replace(pathPart, reg.SRRegistrationType);
                case "DG": // Document Group
                    return oriPathPart.Replace(pathPart, DocumentGroup(reg, programID));
                case "DI": //Document Initial
                    {
                        var docInit = DocumentInitial(reg, programID);
                        if (string.IsNullOrWhiteSpace(docInit)) return string.Empty;
                        return oriPathPart.Replace(pathPart, docInit);
                    }
                case "TN": //TransactonNo
                    {
                        if (string.IsNullOrWhiteSpace(txNo)) return string.Empty;
                        return oriPathPart.Replace(pathPart, txNo);
                    }
                default:
                    {
                        //4. Nama File :  BPJSSEPNO(R6)+DG+_DI+_TN
                        var val = string.Empty;
                        if (pathPart.Contains("("))
                        {
                            var paths = pathPart.Split('(');
                            val = reg.GetColumn(paths[0]).ToString();
                            paths[1] = paths[1].Remove(0, 1); //Remove first char
                            paths[1] = paths[1].Remove(paths[1].Length - 1, 1); //Remove last char
                            val = val.Substring(val.Length - paths[1].ToInt());
                        }
                        else
                            val = reg.GetColumn(pathPart).ToString();

                        return oriPathPart.Replace(pathPart, val);
                    }
            }
        }

        private static string DocumentGroup(Registration reg, string programID)
        {
            if (string.IsNullOrWhiteSpace(programID)) return String.Empty;

            var gdc = new GuarantorDocumentChecklistQuery("gdc");
            var dcd = new DocumentChecklistDefinitionQuery("dcd");
            gdc.InnerJoin(dcd).On(gdc.SRDocumentChecklist == dcd.SRDocumentChecklist);

            var df = new DocumentFilesQuery("df");
            gdc.InnerJoin(df).On(dcd.DocumentFilesID == df.DocumentFilesID);

            gdc.Where(gdc.GuarantorID == reg.GuarantorID, gdc.SRRegistrationType == reg.SRRegistrationType, df.ProgramID == programID);
            gdc.es.Top = 1;
            gdc.Select(gdc.SRDocumentChecklist);
            var dtb = gdc.LoadDataTable();


            if (dtb != null && dtb.Rows != null && dtb.Rows.Count > 0)
            {
                return dtb.Rows[0][0].ToString();
            }
            return String.Empty;
        }
        private static string DocumentInitial(Registration reg, string programID)
        {
            if (string.IsNullOrWhiteSpace(programID)) return String.Empty;

            var gdc = new GuarantorDocumentChecklistQuery("gdc");
            var dcd = new DocumentChecklistDefinitionQuery("dcd");
            gdc.InnerJoin(dcd).On(gdc.SRDocumentChecklist == dcd.SRDocumentChecklist);

            var df = new DocumentFilesQuery("df");
            gdc.InnerJoin(df).On(dcd.DocumentFilesID == df.DocumentFilesID);

            gdc.Where(gdc.GuarantorID == reg.GuarantorID, gdc.SRRegistrationType == reg.SRRegistrationType, df.ProgramID == programID);
            gdc.es.Top = 1;
            gdc.Select(df.DocumentInitial);
            var dtb = gdc.LoadDataTable();

            if (dtb != null && dtb.Rows != null && dtb.Rows.Count > 0)
            {
                var dinit = dtb.Rows[0][0];
                return dinit == DBNull.Value ? string.Empty : dinit.ToString();
            }
            return String.Empty;
        }
        private static string FileNameUseFormula(string fileNameFormula, string programID, Registration reg, DateTime docDate, string extension, string txNo)
        {
            //4. Nama File :  BPJSSEPNO(R6)+DG
            //	- 6digit belakang SEP No + A1 = Dokumen Print INACBG E-KLAIM
            //	- 6digit Belakang SEP No + A2 = Print SEP
            //	- 6digit Belakang SEP No + A3 = Cetakan Resume Kunjungan Pasien Rawat Jalan
            //	- 6digit Belakang SEP No + A4 = Surat Kontrol Rawat Jalan
            //	- 6digit Belakang SEP No + F  = Kunjungan Rehab Medis/Fisioterapi
            //	- 6digit belakang SEP No + Z = Billing Statement Pasien
            var fileName = string.Empty;
            if (fileNameFormula.Contains("+"))
            {
                var parts = fileNameFormula.Split('+');
                foreach (var part in parts)
                {
                    fileName = string.Concat(fileName, PathPartValue(part, reg, docDate, programID, txNo));
                }
            }

            return String.Format("{0}.{1}", fileName, extension);
        }

        private static string GuarantorDocumentFilePathDefault(Registration reg, string programID, string reportName, string extension, ref string regType, ref string guarantorID, string bpjsSepNo)
        {
            var isBpjs = false;
            var medicalNo = string.Empty;
            var docDate = DateTime.Today;
            var guarantorHeaderID = string.Empty;

            if (!string.IsNullOrEmpty(bpjsSepNo))
            {
                isBpjs = true;
                regType = string.Empty;
                guarantorID = string.Empty;

                var bpjsSEP = new BpjsSEP();
                bpjsSEP.Query.Where(bpjsSEP.Query.NoSEP == bpjsSepNo);
                bpjsSEP.Query.es.Top = 1;
                if (!bpjsSEP.Query.Load())
                {
                    var patient = new Patient();
                    patient.LoadByPrimaryKey(reg.PatientID);

                    medicalNo = patient.MedicalNo;
                    docDate = reg.RegistrationDate.Value;
                }
                else
                {
                    medicalNo = bpjsSEP.NoMR;
                    docDate = bpjsSEP.TanggalSEP.Value;
                }

                // Check BpjsSepNo
                reg = new Registration();
                reg.Query.Where(reg.Query.BpjsSepNo == bpjsSepNo);
                reg.Query.es.Top = 1;
                if (!reg.Query.Load())
                {
                    // Get GuarantorHeaderID from Guarantor table
                    var grType = AppParameter.GetParameterValue(AppParameter.ParameterItem.GuarantorTypeBpjs);
                    var gr = new Guarantor();
                    gr.Query.Where(gr.Query.SRGuarantorType == grType, gr.Query.GuarantorHeaderID > string.Empty);
                    gr.Query.es.Top = 1;
                    if (gr.Query.Load())
                    {
                        guarantorHeaderID = gr.GuarantorHeaderID;
                    }
                }
            }

            if (reg.RegistrationNo != null)
            {
                regType = reg.SRRegistrationType;
                guarantorID = reg.GuarantorID;
                docDate = reg.RegistrationDate.Value;

                var gr = new Guarantor();
                gr.LoadByPrimaryKey(guarantorID);
                isBpjs = gr.SRGuarantorType.Equals(AppParameter.GetParameterValue(AppParameter.ParameterItem.GuarantorTypeBpjs));
                bpjsSepNo = reg.BpjsSepNo;
                guarantorHeaderID = gr.GuarantorHeaderID ?? gr.GuarantorID.Trim();

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                medicalNo = pat.MedicalNo;
            }

            var regNoOrSepNo = isBpjs ? (bpjsSepNo ?? reg.RegistrationNo) : reg.RegistrationNo;
            regNoOrSepNo = regNoOrSepNo.Trim().Replace("/", "-");


            var sepFolder = AppParameter.GetParameterValue(AppParameter.ParameterItem.SepFolder);

            if (string.IsNullOrWhiteSpace(sepFolder))
                sepFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "BpjsSepDocument");

            string fileName, path;
            // BPJS : Tahun -> Bulan -> GuarantorHeaderID/GuarantorID -> Tgl -> SEP No 
            // Non BPJS : Tahun -> Bulan -> GuarantorHeaderID/GuarantorID -> Tgl ->  Reg No 
            path = string.Format("{0}\\{1:0000}\\{2:00}\\{3:00}\\{5:00}\\{4}", sepFolder, docDate.Year, docDate.Month, guarantorHeaderID, regNoOrSepNo, docDate.Day);

            fileName = FileNameDefault(reportName, extension, docDate, regNoOrSepNo);

            //// Ovveride for Tarakan
            //// \\192.168.6.2\ElektronikBPJS\Tahun\Bulan\norm+Noreg+SEP
            //if (AppSession.Parameter.HealthcareInitial == "RSTJ")
            //{
            //    var programCategory = "005"; // Document Penunjang
            //    if (!string.IsNullOrWhiteSpace(programID))
            //    {
            //        var prg = new AppProgram();
            //        if (prg.LoadByPrimaryKey(programID) && !string.IsNullOrWhiteSpace(prg.SRProgramCategory))
            //            programCategory = prg.SRProgramCategory;
            //    }

            //    bpjsSepNo = string.IsNullOrWhiteSpace(bpjsSepNo) ? string.Empty : string.Format("_{0}", bpjsSepNo.Trim().Replace("/", "-"));
            //    path = string.Format("{0}\\{1:0000}\\{2:00}\\{3}_{4}{5}", sepFolder, regDate.Year, regDate.Month, reg.RegistrationNo.Trim().Replace("/", "-"), medicalNo.Trim().Replace("/", "-"), bpjsSepNo);

            //    programCategory = string.IsNullOrWhiteSpace(programCategory) ? "001" : programCategory;
            //    fileName = string.Format("{0}.{1}{2}", programCategory,
            //          reportName.Trim().Replace(" ", "_"), string.IsNullOrWhiteSpace(extension) ? string.Empty : string.Format(".{0}", extension));
            //}
            var filePath = Path.Combine(path, fileName);
            return filePath;

        }

        private static string FileNameDefault(string reportName, string extension, DateTime date, string regNoOrSepNo)
        {
            return string.Format("{0:0000}{1:00}{2:00}_{3}_{4}{5}", date.Year, date.Month, date.Day, regNoOrSepNo, reportName.Trim().Replace(" ", "_"), string.IsNullOrWhiteSpace(extension) ? string.Empty : string.Format(".{0}", extension));
        }

        public static string SaveFileToGuarantorDocument(string healthcareInitial, string programID, PrintJobParameterCollection printJobParameters)
        {
            var reportName = string.Empty;
            var programCategory = string.Empty;
            bool isDirectPrintEnable = false;
            //var isBpjs = false;


            var report = InitializedReportDocument(ref reportName, ref isDirectPrintEnable, ref programCategory, healthcareInitial, programID, printJobParameters);
            if (report == null) return string.Empty;

            if (IsGuarantorDocumentPathUseFormula)
                reportName = String.Empty; // Untuk GuarantorDocumentPathUseFormula hanya diisi No Tx

            var regNo = string.Empty;
            var bpjsSepNo = string.Empty;
            foreach (PrintJobParameter jobParameter in printJobParameters)
            {
                var lowerName = jobParameter.Name.ToLower();
                if (lowerName.Contains("list")) continue;

                if (lowerName.Contains("nosep") || lowerName.Contains("sepno"))
                {
                    bpjsSepNo = jobParameter.ValueString;
                    break;
                }

                if (lowerName.Contains("registrationno") || lowerName.Contains("regno"))
                {
                    regNo = jobParameter.ValueString;
                    break;
                }

                if (lowerName.Contains("prescriptionno"))
                {
                    var presc = new TransPrescription();
                    if (presc.LoadByPrimaryKey(jobParameter.ValueString))
                    {
                        regNo = presc.RegistrationNo;
                        reportName = String.Format("{0}_{1}", reportName.Trim(), jobParameter.ValueString.Replace("/", string.Empty)); // Tambah no tx
                        break;
                    }
                }
                else if (lowerName.Contains("transactionno"))
                {
                    var tc = new TransCharges();
                    if (tc.LoadByPrimaryKey(jobParameter.ValueString))
                    {
                        regNo = tc.RegistrationNo;
                        reportName = String.Format("{0}_{1}", reportName.Trim(), jobParameter.ValueString.Replace("/", string.Empty)); // Tambah no tx

                        // Tambah SequenceNo jika ada, untuk cetakan hasil yg per Item (Handono 230410)
                        var pSeqNo = printJobParameters.FindByParameterName("p_SequenceNo");
                        if (pSeqNo != null)
                            reportName = string.Concat(reportName, "-", pSeqNo.ValueString.Replace("/", string.Empty));
                        break;
                    }
                }
                else if (lowerName.Contains("paymentno"))
                {
                    var tp = new TransPayment();
                    if (tp.LoadByPrimaryKey(jobParameter.ValueString))
                    {
                        regNo = tp.RegistrationNo;
                        reportName = String.Format("{0}_{1}", reportName.Trim(), jobParameter.ValueString.Replace("/", string.Empty)); // Tambah no tx
                        break;
                    }
                }
                else if (lowerName.Contains("resultno"))
                {
                    var pa = new PathologyAnatomy();
                    if (pa.LoadByPrimaryKey(jobParameter.ValueString))
                    {
                        regNo = pa.RegistrationNo;
                        reportName = String.Format("{0}_{1}", reportName.Trim(), jobParameter.ValueString.Replace("/", string.Empty)); // Tambah no tx
                        break;
                    }
                }
                else if (lowerName.Contains("bookingno"))
                {
                    var boNo = new ServiceUnitBooking();
                    if (boNo.LoadByPrimaryKey(jobParameter.ValueString) && !string.IsNullOrWhiteSpace(boNo.RegistrationNo))
                    {
                        regNo = boNo.RegistrationNo;
                        reportName = String.Format("{0}_{1}", reportName.Trim(), jobParameter.ValueString.Replace("/", string.Empty)); // Tambah no tx
                        break;
                    }
                }
                else if (lowerName.Contains("registrationinfomedicid"))
                {
                    var rim = new RegistrationInfoMedic();
                    if (rim.LoadByPrimaryKey(jobParameter.ValueString))
                    {
                        regNo = rim.RegistrationNo;
                        reportName = String.Format("{0}_{1}", reportName.Trim(), jobParameter.ValueString.Replace("/", string.Empty)); // Tambah no tx
                        break;
                    }
                }
            }

            // Cari berdasarkan ParameterName untuk RegistrationNo yg diseting di StandardReference
            if (string.IsNullOrEmpty(regNo) && string.IsNullOrEmpty(bpjsSepNo))
            {
                var parValue = string.Empty;
                regNo = GetRegNoUsingStandardReferenceQuery(ref parValue);
                reportName = String.Format("{0}_{1}", reportName.Trim(), parValue.Replace("/", string.Empty)); // Tambah no tx
            }

            if (string.IsNullOrEmpty(regNo) && string.IsNullOrEmpty(bpjsSepNo)) return string.Empty;

            var reportProcessor = new ReportProcessor();
            var deviceInfo = new System.Collections.Hashtable();

            // Set Password
            //deviceInfo["OwnerPassword"] = "test"; 
            //deviceInfo["UserPassword"] = "test1"; 

            RenderingResult result = null;
            if (report is ReportBook)
            {
                var irs = new Telerik.Reporting.InstanceReportSource();
                irs.ReportDocument = (ReportBook)report;

                result = reportProcessor.RenderReport("PDF", irs, deviceInfo);
            }
            else
                result = reportProcessor.RenderReport("PDF", (Report)report, deviceInfo);

            string regType = string.Empty;
            string guarantorID = string.Empty;

            //db:20240108 - penggabungan folder ke noreg utama (cek merge billing)
            var mb = new MergeBilling();
            if (mb.LoadByPrimaryKey(regNo) && mb.FromRegistrationNo != string.Empty)
                regNo = mb.FromRegistrationNo;

            var filePath = GuarantorDocumentFilePath(regNo, programID, reportName.Trim(), result.Extension, ref regType, ref guarantorID, bpjsSepNo);

            var path = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            //var filePath = Path.Combine(path, fileName);

            if (File.Exists(filePath)) File.Delete(filePath);

            using (FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
            }

            SaveRegistrationDocumentCheckList(regNo, guarantorID, regType, filePath);
            return filePath;
        }

        private static string GetRegNoUsingStandardReferenceQuery(ref string parValue)
        {
            string regNo = string.Empty;
            var appstdColl = new AppStandardReferenceItemCollection();
            if (appstdColl.LoadByStandardReferenceID("RptParamToRegNo", 0))
            {
                foreach (PrintJobParameter jobParameter in AppSession.PrintJobParameters)
                {
                    var std = appstdColl.Where(a => a.ItemID.ToLower() == jobParameter.Name.ToLower());
                    if (std.Any())
                    {
                        regNo = appstdColl.GetRegistrationNo(std.First().Note, jobParameter.ValueString);
                        parValue = jobParameter.ValueString;
                        if (!string.IsNullOrEmpty(regNo)) break;
                    }
                    else
                    {
                        std = appstdColl.Where(a => a.ItemName.ToLower() == jobParameter.Name.ToLower());
                        if (std.Any())
                        {
                            regNo = appstdColl.GetRegistrationNo(std.First().Note, jobParameter.ValueString);
                            if (!string.IsNullOrEmpty(regNo)) break;
                        }
                    }
                }
            }

            return regNo;
        }

        internal static void SaveRegistrationDocumentCheckList(string regNo, string guarantorID, string registrationType, string filePath)
        {
            // Save RegistrationDocumentCheckList jika id pendukungnya ada
            var grr = new GuarantorDocumentChecklist();
            if (grr.LoadByPrimaryKey(guarantorID, registrationType))
            {
                var dcdc = new DocumentChecklistDefinitionCollection();
                dcdc.Query.Where(dcdc.Query.SRDocumentChecklist == grr.SRDocumentChecklist);
                if (dcdc.Query.Load())
                {
                    var documentFilesID = 0;
                    foreach (var dcd in dcdc)
                    {
                        var df = new DocumentFiles();
                        if (!df.LoadByPrimaryKey(dcd.DocumentFilesID ?? 0)) continue;

                        if (string.IsNullOrWhiteSpace(df.ProgramID)) continue;
                        documentFilesID = df.DocumentFilesID ?? 0;
                    }

                    var rdcl = new RegistrationDocumentCheckList();
                    if (!rdcl.LoadByPrimaryKey(regNo, documentFilesID)) rdcl = new RegistrationDocumentCheckList();

                    rdcl.RegistrationNo = regNo;
                    rdcl.DocumentFilesID = documentFilesID;
                    rdcl.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    rdcl.LastUpdateDateTime = DateTime.Now;
                    rdcl.FileName = filePath;
                    rdcl.Save();
                }
            }
        }
        protected void btnSendToEmail_Click(object sender, EventArgs e)
        {
            string toEmail = EmailAddress();

            var errMsg = string.Empty;
            var filePath = CreateAttachmentFile(AppSession.UserLogin.UserID, ref toEmail);

            var appProgram = new AppProgram();
            appProgram.LoadByPrimaryKey(AppSession.PrintJobReportID);

            var sb = new StringBuilder();
            sb.AppendLine("Please see attached file.");
            sb.AppendLine(string.Empty);
            sb.AppendLine("Report Parameter:");

            foreach (PrintJobParameter rptpar in AppSession.PrintJobParameters)
            {
                if (!string.IsNullOrEmpty(rptpar.ValueString))
                {
                    sb.AppendFormat("- {0}: {1}", rptpar.Name, rptpar.ValueString);
                    sb.AppendLine(string.Empty);
                }
                else if (rptpar.ValueDateTime != null)
                {
                    sb.AppendFormat("- {0}: {1}", rptpar.Name, rptpar.ValueDateTime.Value.ToString(AppConstant.DisplayFormat.DateShortMonth));
                    sb.AppendLine(string.Empty);
                }
                else if (rptpar.ValueNumeric != null)
                {
                    sb.AppendFormat("- {0}: {1}", rptpar.Name, rptpar.ValueNumeric);
                    sb.AppendLine(string.Empty);
                }
            }
            sb.AppendLine(string.Empty);
            sb.AppendLine(string.Empty);
            sb.AppendLine(AppSession.Parameter.EmailSender);
            //sb.AppendLine("IT Department");
            sb.AppendLine(Healthcare.AddressComplete());

            errMsg = SendEmailWithAttachment(toEmail, appProgram.ProgramName, sb.ToString(), filePath);

            string script = string.Empty;
            if (!string.IsNullOrWhiteSpace(errMsg))
                script = string.Format("<script type='text/javascript'>alert('Send email failed : {0}');</script>", errMsg);

            else
                script = string.Format("<script type='text/javascript'>alert('Report has send to {0}');</script>", toEmail);


            if (!Page.ClientScript.IsStartupScriptRegistered("msgSave"))
                Page.ClientScript.RegisterStartupScript(this.GetType(), "msgSave", script);
        }

        internal static string CreateAttachmentFile(string userID, ref string toEmail)
        {
            var reportName = string.Empty;
            var programCategory = string.Empty;
            bool isDirectPrintEnable = false;

            var report = InitializedReportDocument(ref reportName, ref isDirectPrintEnable, ref programCategory);
            if (report == null) return string.Empty;


            var paramedic = new Paramedic();
            foreach (PrintJobParameter jobParameter in AppSession.PrintJobParameters)
            {
                if (jobParameter.Name.ToLower().Contains("paramedicid"))
                {
                    if (paramedic.LoadByPrimaryKey(jobParameter.ValueString))
                    {
                        toEmail = paramedic.Email;
                    }
                    break;
                }
            }

            var reportProcessor = new ReportProcessor();
            var deviceInfo = new System.Collections.Hashtable();

            RenderingResult result = null;
            if (report is ReportBook)
            {
                var irs = new Telerik.Reporting.InstanceReportSource();
                irs.ReportDocument = (ReportBook)report;
                result = reportProcessor.RenderReport("PDF", irs, deviceInfo);
            }
            else
                result = reportProcessor.RenderReport("PDF", (Report)report, deviceInfo);

            var tmpFolder = AppParameter.GetParameterValue(AppParameter.ParameterItem.TmpFolder);

            if (string.IsNullOrWhiteSpace(tmpFolder))
                tmpFolder = "C:\\TMP";

            var fileName = string.Format("{0}.{1}", reportName.Trim(), result.Extension);

            string path = string.Format("{0}//{1}", tmpFolder, userID.Trim().Replace('.', '_'));


            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var filePath = Path.Combine(path, fileName);

            if (!File.Exists(filePath))
                File.Delete(filePath);

            using (FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
            }

            return filePath;
        }

        public static string SendEmailWithAttachment(string toAddress, string subject, string body, string file)
        {
            // https://myaccount.google.com/lesssecureapps?pli=1 <- harus dienablekan
            //string fromAddress = "AvicennaHis.SCI@gmail.com";
            //string fromPassword = "sciadmin88";
            //toAddress ->  avicennahis.sci.logerror@gmail.com -> sciadmin88

            //Seting baru supaya bisa menggunakan gmail
            //https://support.google.com/mail/answer/185833?hl=en
            //Create & use app passwords
            //Important: To create an app password, you need 2-Step Verification on your Google Account.
            //If you use 2-Step-Verification and get a "password incorrect" error when you sign in, you can try to use an app password.
            //1. Go to your Google Account. (https://myaccount.google.com/)
            //2. Select Security.
            //3. Under "Signing in to Google," select 2-Step Verification.
            //  3.1 At the bottom of the page, select App passwords.
            //  3.2 Enter a name that helps you remember where you’ll use the app password.
            //  3.3 Pilih Email dan Komputer Windows
            //  3.4 Select Generate.
            //4. Save ke parameter EmailPassword hasil generate passwordnya

            var fromAddress = AppParameter.GetParameterValue(AppParameter.ParameterItem.EmailAddress);
            if (string.IsNullOrWhiteSpace(fromAddress)) return "Email address empty";

            // Create a message and set up the recipients.
            var message = new MailMessage(fromAddress, toAddress, subject, body);

            // Create  the file attachment for this email message.
            var data = new Attachment(file, MediaTypeNames.Application.Octet);
            // Add time stamp information for the file.
            var disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(file);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
            // Add the file attachment to this email message.
            message.Attachments.Add(data);

            // smtp settings
            var fromPassword = AppParameter.GetParameterValue(AppParameter.ParameterItem.EmailPassword);
            var host = AppParameter.GetParameterValue(AppParameter.ParameterItem.EmailHost);
            var port = AppParameter.GetParameterValue(AppParameter.ParameterItem.EmailPort).ToInt();
            var client = new SmtpClient();
            {
                client.Host = string.IsNullOrEmpty(host) ? "smtp.gmail.com" : host;
                client.Port = port == 0 ? 587 : port;
                client.EnableSsl = true;
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(fromAddress, fromPassword);
                client.Timeout = 20000;
            }

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                return string.Format("Exception caught in CreateMessageWithAttachment(): {0}", ex.Message);
            }
            data.Dispose();
            return string.Empty;
        }

        protected void cboViewerType_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var prg = new AppProgram();
            prg.LoadByPrimaryKey(AppSession.PrintJobReportID);
            prg.AccessKey = cboViewerType.SelectedValue;
            prg.Save();

            Response.Redirect("ReportViewer.aspx");
        }

        private static byte[] GenerateQRCode(string urlRootHist, string parValue)
        {
            var imgHelper = new ImageHelper();

            if (string.IsNullOrWhiteSpace(urlRootHist))
            {
                //return blank white image
                var whiteImage = new Bitmap(1, 1);
                whiteImage.SetPixel(0, 0, Color.White);
                return imgHelper.ConvertImageToByteArray(whiteImage, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            //return QRCode
            var barcode = new Telerik.Web.UI.RadBarcode();
            barcode.Type = Telerik.Web.UI.BarcodeType.QRCode;
            barcode.Text = string.Format("{0}/{1}", urlRootHist, parValue.Replace("/", "_"));
            barcode.ShowText = true;
            barcode.OutputType = Telerik.Web.UI.BarcodeOutputType.SVG_VML;

            return imgHelper.ConvertImageToByteArray(barcode.GetImage(), System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        internal static string SignPdf(string healthcareInitial, string programID, PrintJobParameterCollection printJobParameters, string nik, string passphrase)
        {
            // Check parameter 
            var pars = (printJobParameters.Where(p =>
                                p.Name.Trim().ToLower() == "p_isforesign"));
            if (pars.Count() == 0)
                printJobParameters.AddNew("p_IsForEsign", "Yes");


            string pdfFile = SaveFileToGuarantorDocument(healthcareInitial, programID, printJobParameters);

            var url = string.Format("{0}/api/sign/pdf", AppParameter.GetParameterValue(AppParameter.ParameterItem.ESignUrlRoot));
            var userId = AppParameter.GetParameterValue(AppParameter.ParameterItem.ESignUserId);
            var pwd = AppParameter.GetParameterValue(AppParameter.ParameterItem.ESignPassword);

            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(userId, pwd);

            var request = new RestSharp.RestRequest();
            request.Method = Method.Post;

            var timeOutPar = AppParameter.GetParameterValue(AppParameter.ParameterItem.PCareTimeOutInSecond);
            var timeOut = Convert.ToInt16(timeOutPar) * 1000;
            request.Timeout = timeOut;

            //request.AddHeader("Content-Type", "multipart/form-data");

            byte[] pdfInBytes = File.ReadAllBytes(pdfFile);
            request.AddFile("file", pdfInBytes, Path.GetFileName(pdfFile), "application/pdf");

            //request.AddParameter("nik", "0803202100007062");
            //request.AddParameter("passphrase", "Bsr3mantap.,!");
            request.AddParameter("nik", nik);
            request.AddParameter("passphrase", passphrase);

            var esign = new AppProgramEsign();
            esign.LoadByPrimaryKey(programID);

            request.AddParameter("tampilan", (esign.IsVisible ?? false) ? "visible" : "invisible");
            request.AddParameter("image", "true");

            // Barcode
            var parRegNo = printJobParameters.FindByParameterName("p_RegistrationNo");
            var parValue = parRegNo == null ? string.Empty : parRegNo.ValueString;
            var qrCode = GenerateQRCode(esign.UrlRootHist, parValue);
            request.AddFile("imageTTD", qrCode, "imageTTD.jpg", "image/jpeg");

            if (!string.IsNullOrWhiteSpace(esign.TagCoordinate))
                request.AddParameter("tag_koordinat", esign.TagCoordinate);
            else
            {
                if (!string.IsNullOrWhiteSpace(esign.Page))
                    request.AddParameter("halaman", esign.Page);
                else
                    request.AddParameter("halaman", "TERAKHIR");
            }


            if (esign.XAxis > 0)
                request.AddParameter("xAxis", esign.XAxis ?? 10);

            if (esign.YAxis > 0)
                request.AddParameter("yAxis", esign.YAxis ?? 50);

            if (esign.Width > 0)
                request.AddParameter("width", esign.Width ?? 250);

            if (esign.Height > 0)
                request.AddParameter("height", esign.Height ?? 70);

            //request.AddParameter("text", "Dokumen ini telah ditandatangani secara elektronik menggunakan sertifikat elektronik yang diterbitkan oleh BSrE BSSN"); 

            var response = client.Execute(request);

            // Log Last Result
            var esignLog = new EsignLog();
            var qr = new EsignLogQuery();
            qr.Where(qr.ProgramID == programID, qr.RegistrationNo == parValue);

            if (!esignLog.Load(qr))
            {
                esignLog = new EsignLog();
                esignLog.ApiUrl = url;
                esignLog.ProgramID = programID;
                esignLog.RegistrationNo = parValue;
            }
            esignLog.StatusCode = response.StatusCode.ToInt();
            esignLog.Nik = nik;
            esignLog.ErrorMessage = String.Empty;

            var signedPdf = string.Empty;
            string script = string.Empty;
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                signedPdf = string.Format("{0}\\{1}_Signed.pdf", Path.GetDirectoryName(pdfFile), Path.GetFileNameWithoutExtension(pdfFile));
                using (var stream = File.Create(signedPdf))
                {
                    var data = response.RawBytes;
                    stream.Write(data, 0, data.Length);
                }

                if (!string.IsNullOrWhiteSpace(signedPdf) && File.Exists(signedPdf))
                {
                    script = string.Format("<script type=\"text/javascript\">alert(\"Esign pdf file has save to {0}\");</script>", signedPdf.Replace(@"\", @"\\"));
                }

                // Save log path file without rootFolder
                var rootFolder = AppParameter.GetParameterValue(AppParameter.ParameterItem.SepFolder);
                if (string.IsNullOrWhiteSpace(rootFolder))
                    rootFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "BpjsSepDocument");

                esignLog.SignedFilePath = signedPdf.Substring(rootFolder.Length + 1);
            }
            else
            {
                esignLog.ErrorMessage = response.Content;
                esignLog.SignedFilePath = string.Empty;
                // Check status
                if (!string.IsNullOrEmpty(response.Content))
                {
                    var respStat = JsonConvert.DeserializeObject<EsignResponseError>(response.Content);
                    if (respStat != null)
                        script = String.Format("<script type='text/javascript'>alert('{0}');</script>", respStat.MessageError);
                    else
                        script = "<script type='text/javascript'>alert('Esign failed');</script>";
                }
                else
                    script = "<script type='text/javascript'>alert('Esign failed');</script>";
            }


            esignLog.Save();

            // Simpan log History
            var logHist = new EsignLogHist();
            logHist.ApiUrl = esignLog.ApiUrl;
            logHist.Nik = esignLog.Nik;
            logHist.ProgramID = esignLog.ProgramID;
            logHist.RegistrationNo = esignLog.RegistrationNo;
            logHist.TransactionNo = esignLog.TransactionNo;
            logHist.SignedFilePath = esignLog.SignedFilePath;
            logHist.ErrorMessage = esignLog.ErrorMessage;
            logHist.StatusCode = esignLog.StatusCode;
            logHist.Save();

            return script;
        }


        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            if (eventArgument != null)
            {
                if (eventArgument.Contains("esign"))
                {
                    var evenArgs = eventArgument.Split('_');
                    var msgResult = SignPdf(AppSession.Parameter.HealthcareInitial, AppSession.PrintJobReportID, AppSession.PrintJobParameters, evenArgs[1], evenArgs[2]);

                    if (!string.IsNullOrEmpty(msgResult))
                    {
                        if (!Page.ClientScript.IsStartupScriptRegistered("msgSave"))
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "msgSave", msgResult);
                    }
                }
                if (eventArgument.Contains("refresh"))
                {
                    ShowReport();
                }
            }
            else
                base.RaisePostBackEvent(sourceControl, eventArgument);
        }


    }

    public class EsignResponseError
    {
        [JsonProperty("status_code")]
        public int StatusCode;

        [JsonProperty("error")]
        public string MessageError;

    }

    public class PrintParameter
    {
        public PrintParameter(string name, string valueString, Decimal? valueNumeric, DateTime? valueDateTime)
        {
            Name = name;
            ValueString = valueString;
            ValueNumeric = valueNumeric;
            ValueDateTime = valueDateTime;
        }

        [JsonProperty("nm")]
        public string Name { get; set; }

        [JsonProperty("vs", NullValueHandling = NullValueHandling.Ignore)]
        public string ValueString { get; set; }

        [JsonProperty("vn", NullValueHandling = NullValueHandling.Ignore)]
        public System.Decimal? ValueNumeric { get; set; }

        [JsonProperty("vd", NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTime? ValueDateTime { get; set; }
    }
}