using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Xml;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;
using Telerik.Reporting.Processing;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using PictureBox = Telerik.Reporting.Processing.PictureBox;
using Report = Telerik.Reporting.Processing.Report;
using SqlDataSource = System.Web.UI.WebControls.SqlDataSource;
using SqlDataSourceCommandType = System.Web.UI.WebControls.SqlDataSourceCommandType;
using SubReport = Telerik.Reporting.Processing.SubReport;
using Unit = System.Web.UI.WebControls.Unit;
using UnitType = System.Web.UI.WebControls.UnitType;

namespace Temiang.Avicenna.Module.Reports
{
    public partial class GeneratePdf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs eventArgs)
        {
            var deviceInfo = new Hashtable();

            //print with dialog
            deviceInfo["JavaScript"] = "this.print({bUI: true, bSilent: false, bShrinkToFit: true});";

            var reportProcessor = new ReportProcessor();
            var reportSource = new Telerik.Reporting.InstanceReportSource();

            var reportName = string.Empty;
            var rpt = InitializedReportDocument(ref reportName);

            reportSource.ReportDocument = rpt;


            var renderingResult = reportProcessor.RenderReport("PDF", reportSource, deviceInfo);

            Response.Clear();
            Response.ContentType = renderingResult.MimeType;
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.Expires = -1;
            Response.Buffer = true;
            Response.BinaryWrite(renderingResult.DocumentBytes);
            Response.End();
        }

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
            if (appProgramHC.LoadByPrimaryKey(programID, AppSession.Parameter.HealthcareInitial))
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
                        (Telerik.Reporting.Report)new Telerik.Reporting.XmlSerialization.ReportXmlSerializer().Deserialize(xmlRptReader);

                    if (isUsingReportHeader)
                    {
                        using (var xmlSubReader =
                            XmlReader.Create(ConfigurationManager.AppSettings.Get("ReportHeaderUrlLocation"),
                                new XmlReaderSettings() { IgnoreWhitespace = true }))
                        {
                            var subSrc =
                                (Telerik.Reporting.Report)new Telerik.Reporting.XmlSerialization.ReportXmlSerializer().Deserialize(xmlSubReader);
                            (subSrc.DataSource as Telerik.Reporting.SqlDataSource).ConnectionString = esConfigSettings.ConnectionInfo
                                .Connections[esConfigSettings.ConnectionInfo.Default].ConnectionString;

                            var pict = (from x in subSrc.Items["reportHeader"].Items
                                        where x.Name == "pictureBox1"
                                        select x).SingleOrDefault();
                            if (pict != null)
                                (pict as Telerik.Reporting.PictureBox).Value =
                                    ReportLibrary.Helper.ResourceLogo(
                                        AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitial));

                            var sub = new Telerik.Reporting.SubReport
                            {
                                Location = new PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Inch)),
                                Size = new SizeU(new Telerik.Reporting.Drawing.Unit(5, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.8, Telerik.Reporting.Drawing.UnitType.Inch)),
                                ReportSource = subSrc
                            };

                            var pageHd = (from r in report.Items
                                          where r.GetType().FullName == "Telerik.Reporting.ReportHeaderSection"
                                          select r).SingleOrDefault();
                            if (pageHd != null) pageHd.Items.Add(sub);
                        }
                    }

                    if (report.DataSource is Telerik.Reporting.SqlDataSource)
                    {
                        var conn = (Telerik.Reporting.SqlDataSource)report.DataSource;
                        conn.ConnectionString = esConfigSettings.ConnectionInfo
                            .Connections[esConfigSettings.ConnectionInfo.Default]
                            .ConnectionString;
                        conn.CommandTimeout = int.MaxValue;
                        if (!string.IsNullOrWhiteSpace(storeProcedureName))
                        {
                            conn.SelectCommandType = Telerik.Reporting.SqlDataSourceCommandType.StoredProcedure;
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
                    return report;
                }
            }

            return null;
        }

    }
}