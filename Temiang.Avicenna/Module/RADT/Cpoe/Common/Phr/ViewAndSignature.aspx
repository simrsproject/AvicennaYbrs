<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAndSignature.aspx.cs" Inherits="Temiang.Avicenna.Module.Emr.Phr.ViewAndSignature" %>

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
            window.pdfjsLib.GlobalWorkerOptions.workerSrc = '../../../../../JavaScript/pdf.worker.js';
            // Tambah tombol Signature
            function pageLoad(app, args) {
                var pdfViewer = $find('<%= pdfViewer.ClientID %>');

                pdfViewer.addToolBarItem({
                    type: "button",
                    text: "Signature",
                    click: function (e) {
                        editSignature();
                    }
                });
                pdfViewer.addToolBarItem({
                    type: "button",
                    text: "Download Word Doc",
                    click: function (e) {
                        downloadWordFile();
                    }
                });
                pdfViewer.addToolBarItem({
                    type: "button",
                    text: "Save to Guarantor Document",
                    click: function (e) {
                        saveToGuarantorDoc();
                    }
                });
            }

            function saveToGuarantorDoc() {
                __doPostBack("<%= pdfViewer.UniqueID %>", 'saveToGuarantorDoc');
            }

            function editSignature() {
                var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/Signature/SignatureEdit.html';
                var oWnd = $find("<%= winImage.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
            }

            function downloadWordFile() {
                __doPostBack("<%= pdfViewer.UniqueID %>", 'downloadWordFile');
            }

            function winImage_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    document.getElementById('<%=hdnImage.ClientID %>').value = arg.image;

                    // Postback
                    __doPostBack("<%= pdfViewer.UniqueID %>", 'signature');
                }
            }
        </script>
        <asp:HiddenField runat="server" ID="hdnImage" />
        <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="620px" Behavior="Move, Close,Maximize,Resize"
            ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="winImage_ClientClose"
            ID="winImage" />
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
