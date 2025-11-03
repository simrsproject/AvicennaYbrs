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
using Telerik.ReportViewer.Html5.WebForms;
using Temiang.Avicenna.BusinessObject.Common;
using PictureBox = Telerik.Reporting.PictureBox;
using Report = Telerik.Reporting.Report;
using ReportSource = Telerik.Reporting.ReportSource;
using SubReport = Telerik.Reporting.SubReport;

namespace Temiang.Avicenna.Module.Reports
{
    public partial class ReportViewerHtml5 : BasePage
    {
        /// <summary>
        /// Load assemblyClassName in specific assembly file
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="assemblyClassName"></param>
        /// <returns></returns>
        private Type LoadReport(string applicationID, string assemblyName, string assemblyClassName)
        {
            if (string.IsNullOrEmpty(applicationID) ||
                ApplicationSettings.DefaultApplication.Name.Equals(applicationID))
            {
                //var executingAssembly = Assembly.GetExecutingAssembly();
                //applicationDirectory = Path.GetDirectoryName(executingAssembly.Location);
                return Type.GetType(string.Concat(assemblyClassName, ",", assemblyName));
            }

            //Load the assembly from the specified path. 
            var assemblyFileName = assemblyName.Trim() + ".dll";
            var assemblyPath = ApplicationSettings.ApplicationInfo.Applications[applicationID].BinFolderLocation + "\\" + assemblyFileName;

            if (File.Exists(assemblyPath))
            {
                //Load the assembly from the specified path.                    
                var loadingAssembly = Assembly.LoadFrom(assemblyPath);

                //Return the loaded class.
                return loadingAssembly.GetType(assemblyClassName.Trim());
            }
            return null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            btnSaveToSepDoc.Enabled = false;
            foreach (PrintJobParameter jobParameter in AppSession.PrintJobParameters)
            {
                if (jobParameter.Name.ToLower().Contains("registrationno"))
                {
                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(jobParameter.ValueString) && !string.IsNullOrEmpty(reg.BpjsSepNo))
                    {
                        btnSaveToSepDoc.Enabled = true;
                        btnSaveToSepDoc.Text = string.Format("Save PDF To SEP Folder: {0}", reg.BpjsSepNo);
                    }
                    break;
                }
            }

            var reportName = string.Empty;
            var rpt = InitializedReportDocument(ref reportName);
            Page.Title = "Print Preview " + reportName;

            if (rpt != null)
            {
                var rptSource = new Telerik.ReportViewer.Html5.WebForms.ReportSource();
                rptSource.IdentifierType = IdentifierType.TypeReportSource;
                rptSource.Identifier = rpt.DocumentName;

                var pars = new Telerik.ReportViewer.Html5.WebForms.ParameterCollection();
                reportViewer.ReportSource = rptSource;
            }
        }

        private IReportDocument InitializedReportDocument(ref string reportName)
        {
            string assemblyClassName;
            string assemblyName;
            string programID = AppSession.PrintJobReportID;
            string storeProcedureName;
            bool isDirectPrintEnable;
            bool isUsingReportHeader;
            string programType;
            string navigateUrl;

            var appProgram = new AppProgram();
            appProgram.LoadByPrimaryKey(AppSession.PrintJobReportID);

            reportName = appProgram.ProgramName;

            var appProgramHC = new AppProgramHealthcare();
            if (appProgramHC.LoadByPrimaryKey(programID, AppSession.Parameter.HealthcareID))
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
            if (AppSession.Parameter.IsLogProgramAccess)
                CreateUserProgramLog(assemblyClassName);

            // 2015-10-14 by Handono
            // btnDirectPrint tidak perlu selalu dimunculkan krn akan membingungkan user
            // dan hanya berfungsi jika report akan dicetak ke printer dg tipe dot matrix 
            // yg jika diprint dari preview toolbox hasilnya akan kurang bagus 
            // sehingga perlu di print dari aplikasi Printer Client yg berbasis desktop
            btnDirectPrint.Enabled = isDirectPrintEnable;
            // End Modif


            if (programType == "RPT" || programType == "RSLIP" || programType == "BOOK")
            {
                if (!string.IsNullOrEmpty(assemblyClassName) && !string.IsNullOrEmpty(assemblyName))
                {
                    //var reportType = Type.GetType(string.Concat(assemblyClassName, ",", assemblyName));
                    var reportType = LoadReport(appProgram.ApplicationID, assemblyName, assemblyClassName);

                    IReportDocument rpt;
                    try
                    {
                        rpt = (IReportDocument)Activator.CreateInstance(reportType, programID, AppSession.PrintJobParameters,
                            AppSession.UserLogin.UserName);
                    }
                    catch
                    {
                        rpt = (IReportDocument)Activator.CreateInstance(reportType, programID, AppSession.PrintJobParameters);
                    }

                    return rpt;
                }
            }
            else if (programType == "XML")
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
                                        AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitial));

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
                            var pars = (AppSession.PrintJobParameters.Where(p =>
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
                                var pars = (AppSession.PrintJobParameters.Where(p => p.Name.Trim().ToLower() == wsPar.Name.Trim().ToLower()));
                                if (pars.Count() == 0)
                                    continue;
                                var par = pars.Single();
                                wsPar.Value = par.ValueString;
                            }
                        }
                        else
                        {
                            var jArray = Helper.JsonStrToArray(storeProcedureName);

                            var serviceUrl = string.Format("{0}/{1}",
                            ConfigurationManager.AppSettings.Get("WebServiceDataSourceUrlRoot"), jArray["serviceurl"].ToString());

                            ds.ServiceUrl = serviceUrl;
                            foreach (WebServiceParameter wsPar in ds.Parameters)
                            {
                                if (wsPar.Name.Trim().ToLower() == "accesskey")
                                {
                                    wsPar.Value = "sciadmin88";
                                }
                                else if (wsPar.Name.Trim().ToLower() == "jsonqueryandparam")
                                {
                                    foreach (var p in AppSession.PrintJobParameters)
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
                    return report;
                }
            }

            return null;
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

        protected void btnSaveToSepDoc_Click(object sender, EventArgs e)
        {
            var reportName = string.Empty;
            var report = InitializedReportDocument(ref reportName);
            if (report != null)
            {
                // SEP ID
                var bpjsSepNo = string.Empty;
                var regDate = DateTime.Now;
                foreach (PrintJobParameter jobParameter in AppSession.PrintJobParameters)
                {
                    if (jobParameter.Name.ToLower().Contains("registration"))
                    {
                        var reg = new Registration();
                        if (reg.LoadByPrimaryKey(jobParameter.ValueString))
                        {
                            bpjsSepNo = reg.BpjsSepNo;
                            regDate = reg.RegistrationDate.Value;
                        }
                        break;
                    }
                }

                if (string.IsNullOrEmpty(bpjsSepNo)) return;

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

                var sepFolder = AppParameter.GetParameterValue(AppParameter.ParameterItem.SepFolder);

                if (string.IsNullOrWhiteSpace(sepFolder))
                    sepFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "BpjsSepDocument"); //Server.MapPath("~/App_Document/BpjsSepDocument");

                var fileName = string.Format("{0}{1}{2} {3} {4}.{5}", regDate.Year, regDate.Month, regDate.Day, bpjsSepNo.Trim(), reportName.Trim(), result.Extension);

                string path = string.Format("{0}//{1:0000}//{2:00}//{3:00}//{4}", sepFolder, regDate.Year, regDate.Month, regDate.Day, bpjsSepNo.Trim());
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string filePath = Path.Combine(path, fileName);

                using (FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                {
                    fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                }

                string script = string.Empty;
                if (File.Exists(filePath))
                {
                    script = string.Format("<script type='text/javascript'>alert('Report has export to {0}');</script>", filePath);
                }
                else
                    script = "<script type='text/javascript'>alert('Save failed');</script>";

                if (!Page.ClientScript.IsStartupScriptRegistered("msgSave"))
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgSave", script);

            }
        }
    }
}