using System;
using System.IO;
using Telerik.Reporting;
using Telerik.Reporting.Processing;
using Telerik.Windows.Documents.Common.FormatProviders;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.Model;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Reports
{
    public partial class PdfViewer : BasePage
    {

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                LoadToPdf();
            else
            {
                if (Request.Params.Get("__EVENTARGUMENT") == "downloadExcelFile")
                {

                    byte[] renderedBytes = null;
                    IFormatProvider<RadFlowDocument> formatProvider = new DocxFormatProvider();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        renderedBytes = ms.ToArray();
                    }

                    Response.ClearHeaders();
                    Response.ClearContent();
                    Response.AppendHeader("content-disposition", "attachment; filename=DownloadFile.docx");
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    Response.BinaryWrite(renderedBytes);
                    Response.End();
                }
            }
        }

        private void LoadToPdf()
        {
            var reportName = string.Empty;
            bool isDirectPrintEnable = false;
            var programCategory = string.Empty;
            var report = ReportViewer.InitializedReportDocument(ref reportName, ref isDirectPrintEnable, ref programCategory);
            if (report == null) return;

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
                result = reportProcessor.RenderReport("PDF", (Telerik.Reporting.Report)report, deviceInfo);

            pdfViewer.PdfjsProcessingSettings.FileSettings.Data = Convert.ToBase64String(result.DocumentBytes);
        }
    }
}