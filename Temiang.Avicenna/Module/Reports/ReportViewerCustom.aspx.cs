using System;
using System.Collections;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.IO;
using System.Web;
using Telerik.Reporting.Processing;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Reports
{
    public partial class ReportViewerCustom : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            var btnSaveToSepDoc = FindByValue(tbarPrintPreview, "SaveToSep");
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
            bool isDirectPrintEnable=false;
            var programCategory = string.Empty;
            var rpt = ReportViewer.InitializedReportDocument(ref reportName, ref isDirectPrintEnable,ref programCategory);
            Page.Title = "Print Preview " + reportName;

            if (rpt != null)
                ReportViewer1.Report = rpt;

            FindByValue(tbarPrintPreview, "PrintDirect").Enabled = isDirectPrintEnable;
        }

        private RadToolBarButton FindByValue(RadToolBar tbar, string value)
        {
            foreach (RadToolBarItem item in tbar.Items)
            {
                if (item is RadToolBarButton && item.Value == value)
                    return (RadToolBarButton)item;
            }

            return null;
        }

        private void PrintDirect()
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

        protected void tbarPrintPreview_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            switch (e.Item.Value)
            {
                case "PrintDirect" :
                    PrintDirect();
                    break;
                case "ShowDialogPrint" :
                    ShowDialogPrint();
                    break;
                case "SaveToSep":
                    SaveToSepFolder();
                    break;
            }

        }

        private void SaveToSepFolder()
        {
            var filePath = ReportViewer.SaveFileToGuarantorDocument(AppSession.Parameter.HealthcareInitial, AppSession.PrintJobReportID, AppSession.PrintJobParameters);

            string script = string.Empty;
            if (!string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath))
            {
                script = string.Format("<script type='text/javascript'>alert('Report has export to {0}');</script>", filePath);
            }
            else
                script = "<script type='text/javascript'>alert('Save failed');</script>";

            if (!Page.ClientScript.IsStartupScriptRegistered("msgSave"))
                Page.ClientScript.RegisterStartupScript(this.GetType(), "msgSave", script);

        }
        protected void ShowDialogPrint()
        {
            var deviceInfo = new Hashtable();

            //print with dialog
            deviceInfo["JavaScript"] = "this.print({bUI: true, bSilent: false, bShrinkToFit: true});";

            var reportProcessor = new ReportProcessor();
            var reportSource = new Telerik.Reporting.InstanceReportSource();

            //var reportName = string.Empty;
            //var rpt = InitializedReportDocument(ref reportName);

            reportSource.ReportDocument = ReportViewer1.Report;

            var renderingResult = reportProcessor.RenderReport("PDF", reportSource, deviceInfo);

            Response.Clear();
            Response.ContentType = renderingResult.MimeType;
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.Expires = -1;
            Response.Buffer = true;
            Response.BinaryWrite(renderingResult.DocumentBytes);
            Response.End();
        }
    }
}