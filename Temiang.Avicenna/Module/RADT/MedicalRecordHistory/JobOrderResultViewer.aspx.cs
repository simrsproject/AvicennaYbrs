using System;
using System.Drawing.Imaging;
using Telerik.Web.UI;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class JobOrderResultViewer : BasePageDialog
    {
        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    ProgramID = AppConstant.Program.MedicalRecordHistory;
        //    Title = "Job Order Result Viewer - " + Request.QueryString["orderNo"];

        //    if (!IsPostBack)
        //    {
        //        var path = AppSession.Parameter.OrderResultFolderPath + Request.QueryString["orderNo"].Trim() + ".pdf";
        //        var pdfViewer = new Aspose.Pdf.Kit.PdfViewer();
        //        pdfViewer.OpenPdfFile(path);

        //        Session["pdfViewer"] = null;
        //        Session["pdfViewer"] = pdfViewer;

        //        cboPageNumber.Items.Clear();
        //        for (var i = 1; i <= pdfViewer.PageCount; i++)
        //        {
        //            cboPageNumber.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
        //        }

        //        cboPageNumber.Enabled = true;

        //        var selectedPage = pdfViewer.DecodePage(1);
        //        selectedPage.Save(Server.MapPath("page.jpeg"), ImageFormat.Jpeg);
        //        imgViewer.ImageUrl = "page.jpeg";
        //        var ratio = imgViewer.Width.Value / selectedPage.Width;
        //        imgViewer.Height = (int)(selectedPage.Height * ratio);
        //        selectedPage.Dispose();
        //    }
        //}

        //private void ShowSelectedPage()
        //{
        //    int page = int.Parse(cboPageNumber.SelectedValue);
        //    var pdfViewer = (Aspose.Pdf.Kit.PdfViewer)Session["pdfViewer"];
        //    if (null == pdfViewer)
        //        imgViewer.ImageUrl = string.Empty;
        //    else
        //    {
        //        var selectedPage = pdfViewer.DecodePage(page);
        //        selectedPage.Save(Server.MapPath("page.jpeg"), ImageFormat.Jpeg);
        //        selectedPage.Dispose();

        //        imgViewer.ImageUrl = "page.jpeg";
        //    }
        //}

        protected void cboPageNumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //ShowSelectedPage();
        }
    }
}
