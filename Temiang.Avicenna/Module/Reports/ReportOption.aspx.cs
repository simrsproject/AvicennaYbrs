using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.UI;
using Telerik.Reporting;
using Telerik.Reporting.Processing;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.Module.Reports.OptionControl;
using System.Configuration;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.Module.Reports
{
    public partial class ReportOption : BasePage
    {
        private string ReportID => Request.QueryString["id"];
        private readonly AppReportParameterCollection _appReportParameters = new AppReportParameterCollection();

        protected bool IsCalledFromReportViewer
        {
            get
            {
                if (Request.QueryString["mode"] != null && Request.QueryString["mode"].Equals("cfvw"))
                    return true;
                else
                    return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo cultureInfo = AppSession.UserLogin.SRLanguage == null ? Thread.CurrentThread.CurrentCulture : new CultureInfo(AppSession.UserLogin.SRLanguage);
            InitializeCulture(this, cultureInfo);

            if (IsCalledFromReportViewer)
            {
                ajxManager.AjaxSettings.AddAjaxSetting(btnOk, btnOk);
                ajxManager.AjaxSettings.AddAjaxSetting(btnOk, hdnUrl);
                ajxManager.AjaxSettings.AddAjaxSetting(btnOk, validationSummary);

                btnExport.Visible = false;
                btnPreview.Visible = false;
                btnOk.Visible = true;
            }
            else
            {
                ajxManager.AjaxSettings.AddAjaxSetting(btnPreview, btnPreview);
                ajxManager.AjaxSettings.AddAjaxSetting(btnPreview, hdnUrl);
                ajxManager.AjaxSettings.AddAjaxSetting(btnPreview, validationSummary);

                ajxManager.AjaxSettings.AddAjaxSetting(btnViewPdf, btnViewPdf);
                ajxManager.AjaxSettings.AddAjaxSetting(btnViewPdf, hdnUrl);
                ajxManager.AjaxSettings.AddAjaxSetting(btnViewPdf, validationSummary);

                btnOk.Visible = false;
            }

            if (!IsPostBack && pnlPassCode.Visible)
                litPassCodeCaption.Text = string.Format("<h3>This report need passcode&nbsp;&nbsp;{0}</h3>", LinkEditPassCode());

        }

        private static void InitializeCulture(Control root, CultureInfo cultureInfo)
        {
            foreach (Control ctl in root.Controls)
            {
                switch (ctl.GetType().Name)
                {
                    case "RadDatePicker":
                        var dtCtl = (RadDatePicker)ctl;
                        dtCtl.Culture = cultureInfo;
                        dtCtl.MinDate = new DateTime(1900, 1, 1);
                        break;
                    case "RadNumericTextBox":
                        var numCtl = (RadNumericTextBox)ctl;
                        numCtl.Culture = cultureInfo;
                        break;

                    default:
                        if (ctl.HasControls())
                            InitializeCulture(ctl, cultureInfo);
                        break;
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!IsCalledFromReportViewer && !Page.ClientScript.IsClientScriptIncludeRegistered("wPopMax"))
                Page.ClientScript.RegisterClientScriptInclude("wPopMax", "../../JavaScript/OpenWindowMax.js");

            pnlPassCode.Visible = IsProgramSignatureNeed();

            InitializedReportOptionControl();
        }

        private void InitializedReportOptionControl()
        {
            try
            {
                if (ReportID != null && ReportID.Trim() != "")
                {
                    string programID = ReportID;
                    _appReportParameters.Query.Where(_appReportParameters.Query.ProgramID == programID);
                    _appReportParameters.Query.OrderBy(_appReportParameters.Query.IndexNo.Descending);
                    _appReportParameters.LoadAll();

                    Control ctlParent = Helper.FindControlRecursive(Form, "pnlAreaOption");
                    //Load control report
                    BaseOptionCtl ctl;
                    foreach (AppReportParameter par in _appReportParameters)
                    {
                        ctl = (BaseOptionCtl)LoadControl("~/Module/Reports/OptionControl/" + par.ReportControlName.Trim() + ".ascx");

                        ctlParent.Controls.AddAt(0, ctl);
                        ctl.ID = par.ReportControlName + par.IndexNo.ToString().Trim();
                        ctl.EnableViewState = true;
                        ctl.ParameterCaption = par.ParameterCaption;
                        ctl.ReferenceID = par.ReferenceID;
                    }

                    var prg = new AppProgram();
                    if (prg.LoadByPrimaryKey(programID))
                        lblReportName.Text = prg.ProgramName;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
                //if (IsCallback)
                //    Response.RedirectLocation = ResolveUrl("~/GenericError.aspx");
                //else
                //    Response.Redirect("~/GenericError.aspx");
            }
        }

        private void PrepareReportParameter()
        {
            try
            {
                PrintJobParameterCollection parameters = new PrintJobParameterCollection();
                int iMax = _appReportParameters.Count - 1;
                for (int ctlNo = iMax; ctlNo > -1; ctlNo--)
                {
                    AppReportParameter par = _appReportParameters[ctlNo];
                    BaseOptionCtl ctlReport = (BaseOptionCtl)Helper.FindControlRecursive(this, par.ReportControlName + par.IndexNo.ToString().Trim());

                    //Ganti dgn nama dari database
                    string[] arrPar = par.ParameterName.Split(';');
                    PrintJobParameterCollection entryParameters = ctlReport.PrintJobParameters();
                    if (arrPar.GetUpperBound(0) > 0)
                    {
                        int i = 0;
                        foreach (var parName in arrPar)
                        {
                            PrintJobParameter item = entryParameters[i];
                            parameters.AddNew(parName, item.ValueString, item.ValueNumeric, item.ValueDateTime);
                            i++;
                        }
                    }
                    else
                    {
                        PrintJobParameter item = entryParameters[0];
                        parameters.AddNew(par.ParameterName, item.ValueString, item.ValueNumeric, item.ValueDateTime);
                    }
                }

                // Reset jika  ReportID nya sudah berbeda // Handono (230320)
                if (AppSession.PrintJobReportID != null && AppSession.PrintJobReportID != ReportID)
                    AppSession.PrintJobParameters = null;

                // Jangan direset tapi timpa untuk parameter name yg sama untuk kasus bukan dipanggil dari report menu
                // tetapi misal dari program entry yg sudah ada parameter tetapi perlu tambahan parameter
                // Handono (230320)
                if (AppSession.PrintJobParameters != null && AppSession.PrintJobParameters.Count > 0)
                {
                    var prevPars = AppSession.PrintJobParameters;
                    foreach (var newPar in parameters)
                    {
                        var prevPar = prevPars.FindByParameterName(newPar.Name);
                        if (prevPar != null)
                        {
                            prevPar.ValueString = newPar.ValueString;
                            prevPar.ValueDateTime = newPar.ValueDateTime;
                            prevPar.ValueNumeric = newPar.ValueNumeric;
                        }
                        else
                            prevPars.AddNew(newPar.Name, newPar.ValueString, newPar.ValueNumeric, newPar.ValueDateTime);
                    }
                }
                else
                    AppSession.PrintJobParameters = parameters;

                AppSession.PrintJobReportID = ReportID;

                AppSession.PrintShowToolBarPrint = true;

                if (Request.QueryString["pvtId"] != null)
                {
                    AppSession.PrintCustomPivotID = Request.QueryString["pvtId"];
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            PrepareReportParameter();

            // Selanjutnya akan di handle oleh OnResponseEnd di client side

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("ReportAppLocation")))
            {
                var str = string.Empty;
                foreach (var param in AppSession.PrintJobParameters)
                {
                    var p = string.Empty;
                    if (param.ValueNumeric != null) p += string.Format("n|{0}", param.ValueNumeric.ToString());
                    else if (param.ValueDateTime != null) p += string.Format("d|{0}", param.ValueDateTime.Value.ToString("MM/dd/yyyy HH:mm:ss"));
                    else p += string.Format("s|{0}", param.ValueString);
                    str += string.Format("{0}^{1}*", param.Name, p);
                }

                var app = new AppProgram();
                app.LoadByPrimaryKey(ReportID);

                hdnUrl.Value = string.Format("{0}{1}?id={2}&user={3}&param={4}", ConfigurationManager.AppSettings.Get("ReportAppLocation"),
                    app.ProgramType == "PVT" ? "PivotViewer.aspx" : "ReportViewer.aspx", ReportID,
                    AppSession.UserLogin.UserID, str);
            }
            else
                hdnUrl.Value = "ReportViewer.aspx";
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            PrepareReportParameter();

            // Selanjutnya akan di handle oleh OnResponseEnd di client side
            hdnUrl.Value = "refresh";
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            //PrepareReportParameter();

            //AppProgram appProgram = new AppProgram();
            //appProgram.LoadByPrimaryKey(AppSession.PrintJobReportID);

            //if (appProgram.AssemblyClassName != null)
            //{
            //    if (!string.IsNullOrEmpty(appProgram.AssemblyClassName) &&
            //        !string.IsNullOrEmpty(appProgram.AssemblyName))
            //    {
            //        string typeName = string.Concat(appProgram.AssemblyClassName, ",", appProgram.AssemblyName);

            //        Type reportType = Type.GetType(typeName);
            //        IReportDocument rpt;
            //        try
            //        {
            //            rpt = (IReportDocument)Activator.CreateInstance(reportType, appProgram.ProgramID, AppSession.PrintJobParameters, AppSession.UserLogin.UserName);
            //        }
            //        catch
            //        {
            //            try
            //            {
            //                rpt = (IReportDocument)Activator.CreateInstance(reportType, appProgram.ProgramID, AppSession.PrintJobParameters);
            //            }
            //            catch (Exception ex2)
            //            {
            //                throw new Exception(ex2.Message, ex2);
            //            }
            //        }

            //        ExportToExcel(rpt);
            //    }
            //}

            PrepareReportParameter();

            var appProgram = new AppProgram();
            appProgram.LoadByPrimaryKey(AppSession.PrintJobReportID);
            var rptType = appProgram.ProgramType.ToLower();

            var appHc = new AppProgramHealthcare();
            if (appHc.LoadByPrimaryKey(appProgram.ProgramID, AppSession.Parameter.HealthcareInitial))
            {
                rptType = appHc.ProgramType.ToLower();
            }

            switch (rptType)
            {
                case "pvt":
                    {
                        ExportPivotToExcel();
                        break;
                    }
                case "ssrs":
                    {
                        ExportSsrsToExcel(appProgram, appHc);
                        break;
                    }
                default:
                    {
                        var reportName = string.Empty;
                        bool isDirectPrintEnable = false;
                        var programCategory = string.Empty;
                        var report = ReportViewer.InitializedReportDocument(ref reportName, ref isDirectPrintEnable,
                            ref programCategory);
                        if (report == null) return;

                        ExportToExcel(report);
                        break;
                    }
            }
        }

        private void ExportSsrsToExcel(AppProgram appProgram, AppProgramHealthcare appHc)
        {
            var reportPath = string.Empty;
            var reportName = appProgram.ProgramName.Replace("/", string.Empty);
            reportName = reportName.Replace("\\", string.Empty);
            reportName = reportName.Replace(" ", string.Empty);

            if (!string.IsNullOrWhiteSpace(appHc.ProgramType) && appHc.ProgramType.ToLower() == "ssrs")
                reportPath = string.Format("/{0}",appHc.NavigateUrl);
            else if (!string.IsNullOrWhiteSpace(appProgram.ProgramType) && appProgram.ProgramType.ToLower() == "ssrs")
                reportPath = string.Format("/{0}",appProgram.NavigateUrl);
            else
                return;

            var rview = new Microsoft.Reporting.WebForms.ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials();

            rview.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings.Get("SsrsServerUrl"));
            var rptPars = new System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter>();


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

            rview.ServerReport.ReportPath = reportPath; //  "/ReportFolder/ReportName";
            rview.ServerReport.SetParameters(rptPars);
            string mimeType, encoding, extension, deviceInfo;
            string[] streamids;
            Microsoft.Reporting.WebForms.Warning[] warnings;
            deviceInfo =
                "<DeviceInfo>" +
                "<SimplePageHeaders>True</SimplePageHeaders>" +
                "</DeviceInfo>";

            string format = "Excel"; //Desired format goes here (PDF, Excel, or Image)
            byte[] bytes = rview.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
            Response.Clear();

            //if (format == "PDF")
            //{
            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("Content-disposition", "filename=output.pdf");
            //}
            //else if (format == "Excel")
            //{
            //    Response.ContentType = "application/excel";
            //    Response.AddHeader("Content-disposition", "filename=output.xls");
            //}

            //Response.OutputStream.Write(bytes, 0, bytes.Length);
            //Response.OutputStream.Flush();
            //Response.OutputStream.Close();
            //Response.Flush();
            //Response.Close();

            Response.ContentType = mimeType;
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.Expires = -1;
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", string.Format("{0};FileName=\"{1}.{2}\"", "attachment", reportName,extension));
            Response.BinaryWrite(bytes);
            Response.End();
        }

        private void ExportToExcel(IReportDocument reportToExport)
        {
            var reportProcessor = new ReportProcessor();
            var instanceReportSource = new InstanceReportSource { ReportDocument = reportToExport };
            //var result = reportProcessor.RenderReport("XLSX", instanceReportSource, null);
            var result = reportProcessor.RenderReport(AppSession.Parameter.ExcelFileExtension.Replace(".", ""), instanceReportSource, null);

            var fileName = result.DocumentName + "." + result.Extension;

            Response.Clear();
            Response.ContentType = result.MimeType;
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.Expires = -1;
            Response.Buffer = true;

            Response.AddHeader("Content-Disposition", string.Format("{0};FileName=\"{1}\"", "attachment", fileName));

            Response.BinaryWrite(result.DocumentBytes);
            Response.End();
        }

        private void ExportPivotToExcel()
        {
            try
            {
                var table = (new ReportDataSource()).GetDataTable(AppSession.PrintJobReportID,
                                                                  AppSession.PrintJobParameters);
                if (table.Rows.Count > 0)
                {
                    AppProgram appProgram = new AppProgram();
                    appProgram.LoadByPrimaryKey(AppSession.PrintJobReportID);
                    string fileName = appProgram.ProgramName.Trim().Replace(" ", "");

                    Common.CreateExcelFile.CreateExcelDocument(table, fileName.Replace('/', '_').Replace(".", "_").Replace(" ", "_") + AppSession.Parameter.ExcelFileExtension, this.Response);
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                throw new Exception(error);
            }
        }

        protected void btnViewPdf_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            PrepareReportParameter();
            hdnUrl.Value = "PdfViewer.aspx";
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (pnlPassCode.Visible && !IsProgramSignatureValid(txtPassword.Text))
            {
                args.IsValid = false;
                customValidator.ErrorMessage = "Passcode is not accepted";
                txtPassword.Focus();
            }
        }

        private bool IsProgramSignatureNeed()
        {
            if (ReportID.Contains("RPT.PYR"))
            {
                var prgSign = new AppProgramSignature();
                if (prgSign.LoadByPrimaryKey("ALL", "ALL")) //Check passcode for ALL
                {
                    return true; // perlu passcode
                }
            }

            return false; // Passcode bypass / app tanpa passcode
        }

        protected string LinkEditPassCode()
        {
            // Hanya power user yg bisa merubah passcode
            var userAccess = new UserAccess(AppSession.UserLogin.UserID, AppSession.PrintJobReportID);

            if (userAccess.IsPowerUserAble)
                return string.Format(
                    "<a href='#' onclick='javascript:openWindow(\"{0}/Login/ChangePassCode.aspx\"); event.cancelBubble = true;if(event.stopPropagation) event.stopPropagation();'><img src='{0}/Images/Toolbar/edit16.png' alt='edit' /></a>",
                    Helper.UrlRoot());

            return string.Empty;
        }
    }
}