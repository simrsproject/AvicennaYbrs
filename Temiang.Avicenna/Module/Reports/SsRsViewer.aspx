<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SsRsViewer.aspx.cs" Inherits="Temiang.Avicenna.Module.Reports.SsRsViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
                    <asp:Button ID="btnDirectPrint" runat="server" Width="100%" Text="Print in Service Unit Printer" OnClick="btnDirectPrint_Click" />
                </td>
                <td>
                    <asp:Button ID="btnSaveToSepDoc" runat="server" Width="100%" Text="Save To SEP Folder" OnClick="btnSaveToSepDoc_Click" /></td>
                <td>
                    <asp:Button ID="btnSendToEmail" runat="server" Width="100%" Text="Send to Email" OnClick="btnSendToEmail_Click" /></td>
            </tr>
        </table>


        <div style="height: 95vh;">
            <asp:ScriptManager runat="server"></asp:ScriptManager>
            <rsweb:reportviewer id="reportViewer" runat="server" processingmode="Remote" height="100%" width="100%" showparameterprompts="False">
                <ServerReport ReportServerUrl="" ReportPath=""  />
            </rsweb:reportviewer>
        </div>
    </form>

    <%--    <script language="javascript" type="text/javascript">
        ResizeReport();

        function ResizeReport() {
            var viewer = document.getElementById("<%= reportViewer.ClientID %>");
            var htmlheight = document.documentElement.clientHeight;
            viewer.style.height = (htmlheight - 30) + "px";
        }

        window.onresize = function resize() { ResizeReport(); }
    </script>--%>
</body>
</html>

