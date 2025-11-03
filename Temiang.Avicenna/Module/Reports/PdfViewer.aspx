<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PdfViewer.aspx.cs" Inherits="Temiang.Avicenna.Module.Reports.PdfViewer" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PDF Viewer</title>
    <script type="text/javascript" src="../../../../../JavaScript/pdf.js"></script>
</head>
<body>

    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement && window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }
        function close() {
            var oWindow = GetRadWindow();
            if (oWindow) {
                oWindow.close();
            }
            else {
                window.close();
            }
        }

    </script>

    <form id="form1" runat="server" style="width: 100%; height: 100%;">
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <script type="text/javascript">
            window.pdfjsLib.GlobalWorkerOptions.workerSrc = '../../../../JavaScript/pdf.worker.js';
            // Tambah tombol Signature
            function pageLoad(app, args) {
                var pdfViewer = $find('<%= pdfViewer.ClientID %>');

                pdfViewer.addToolBarItem({
                    type: "button",
                    text: "Download Word Doc",
                    click: function (e) {
                        downloadExcelFile();
                    }
                });
            }

            function downloadExcelFile() {
                __doPostBack("<%= pdfViewer.UniqueID %>", 'downloadExcelFile');
            }

        </script>
        <telerik:RadPdfViewer runat="server" ID="pdfViewer" Height="550px" Width="100%" Scale="0.9">
            <ToolBarSettings Items="pager,zoom,toggleSelection,search,download,print,spacer" />
        </telerik:RadPdfViewer>

        <script language="javascript" type="text/javascript">
            ResizeReport();

            function ResizeReport() {
                var viewer = document.getElementById("<%= pdfViewer.ClientID %>");
            var htmlheight = document.documentElement.clientHeight;
            viewer.style.height = (htmlheight - 30) + "px";
        }

        window.onresize = function resize() { ResizeReport(); }
        </script>
        

    </form>

</body>
</html>
