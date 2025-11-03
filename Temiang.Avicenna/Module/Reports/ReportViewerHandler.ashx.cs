using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Reporting;
using Temiang.Dal.Interfaces;
using System.Data;
using PictureBox = Telerik.Reporting.PictureBox;
using Report = Telerik.Reporting.Report;
using SubReport = Telerik.Reporting.SubReport;
using System.Xml;
using System.Configuration;
using Telerik.Reporting.Drawing;
using Telerik.Reporting.Processing;
using System.IO;
using System.Reflection;

namespace Temiang.Avicenna.Module.Reports
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ReportViewerHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            var reportName = string.Empty;
            bool isDirectPrintEnable = false;

            var rpt = InitializedReportDocument(ref reportName, ref isDirectPrintEnable);

            ReportProcessor reportProcessor = new ReportProcessor();
            Telerik.Reporting.InstanceReportSource instanceReportSource = new Telerik.Reporting.InstanceReportSource();
            instanceReportSource.ReportDocument = rpt;
            if (rpt != null)
            {
                RenderingResult result = reportProcessor.RenderReport("PDF", instanceReportSource, null);

                context.Response.Clear();
                context.Response.Buffer = true;
                //context.Response.Charset = string.Empty;
                context.Response.Expires = -1;
                context.Response.Cache.SetCacheability(HttpCacheability.Private);//HttpCacheability.NoCache
                context.Response.ContentType = "application/pdf";
                context.Response.AddHeader("content-length", result.DocumentBytes.Length.ToString());
                context.Response.BinaryWrite(result.DocumentBytes);
                context.Response.Flush();
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        internal static IReportDocument InitializedReportDocument(ref string reportName, ref bool isDirectPrintEnable)
        {
            string assemblyClassName;
            string assemblyName;
            string programID = AppSession.PrintJobReportID;
            string storeProcedureName;
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

            // Log access program
            if (AppSession.Parameter.IsLogProgramAccess)
                CreateUserProgramLog(assemblyClassName);


            if (programType == "RPT" || programType == "RSLIP" || programType == "BOOK")
            {
                if (!string.IsNullOrEmpty(assemblyClassName) && !string.IsNullOrEmpty(assemblyName))
                {
                    //var reportType = Type.GetType(string.Concat(assemblyClassName, ",", assemblyName));
                    return LoadReport(!string.IsNullOrEmpty(appProgram.ApplicationID) ?
                        appProgram.ApplicationID : ApplicationSettings.DefaultApplication.Name, assemblyName, assemblyClassName);
                }
            }
            else if (programType == "XML")
            {
                return LoadReportXML(navigateUrl, isUsingReportHeader, storeProcedureName);
            }


            return null;
        }

        private static IReportDocument LoadReportXML(string navigateUrl, bool isUsingReportHeader, string storeProcedureName)
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
                            var pars = (AppSession.PrintJobParameters.Where(p =>
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

                UpdateCheckBox(report.Items);
                return report;
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

        private static IReportDocument LoadReport(string applicationID, string assemblyName, string assemblyClassName)
        {
            Type reportType = null;

            if (string.IsNullOrEmpty(applicationID) ||
                ApplicationSettings.DefaultApplication.Name.Equals(applicationID))
            {
                //var executingAssembly = Assembly.GetExecutingAssembly();
                //applicationDirectory = Path.GetDirectoryName(executingAssembly.Location);
                reportType = Type.GetType(string.Concat(assemblyClassName, ",", assemblyName));
            }

            //Load the assembly from the specified path. 
            var assemblyFileName = assemblyName.Trim() + ".dll";
            var assemblyPath = ApplicationSettings.ApplicationInfo.Applications[applicationID].BinFolderLocation + "\\" + assemblyFileName;

            if (File.Exists(assemblyPath))
            {
                //Load the assembly from the specified path.                    
                var loadingAssembly = Assembly.LoadFrom(assemblyPath);

                //Return the loaded class.
                reportType = loadingAssembly.GetType(assemblyClassName.Trim());
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
            }

            return rpt;
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
    }
}
