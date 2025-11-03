<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewerHtml5.aspx.cs" Inherits="Temiang.Avicenna.Module.Reports.ReportViewerHtml5" %>



<%@ Register Assembly="Telerik.ReportViewer.Html5.WebForms, Version=17.0.23.118, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.Html5.WebForms" TagPrefix="telerik" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        <table width="100%">
            <tr>
                <td style="width: 50%">
                    <asp:Button ID="btnDirectPrint" runat="server" Width="100%" Text="Print Direct" OnClick="btnDirectPrint_Click" />
                </td>
                <td>
                    <asp:Button ID="btnSaveToSepDoc" runat="server" Width="100%" Text="Save To SEP Folder" OnClick="btnSaveToSepDoc_Click" /></td>
            </tr>
        </table>

        <telerik:ReportViewer ID="reportViewer" runat="server"></telerik:ReportViewer>
        <telerik:DeferredScripts ID="deferredScripts" runat="server" />
    </form>

    <script language="javascript" type="text/javascript">
        //ResizeReport();

        function ResizeReport() {
            var viewer = document.getElementById("<%= reportViewer.ClientID %>");
            var htmlheight = document.documentElement.clientHeight;
            viewer.style.height = (htmlheight - 30) + "px";
        }

        //window.onresize = function resize() { ResizeReport(); }
    </script>

</body>
</html>
