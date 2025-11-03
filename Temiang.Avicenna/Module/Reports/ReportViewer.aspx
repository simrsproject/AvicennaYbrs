<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="Temiang.Avicenna.Module.Reports.ReportViewer" %>




<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=17.0.23.118, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
</head>
<body>
    <style>
        #cssLiteral {
            height: calc(100vh - 28px);
            width: 100%;
        }
    </style>
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

        function openInfo() {
            var oWnd = $find("<%= winDialog.ClientID %>");
            oWnd.setUrl("ReportInfo.aspx");
            oWnd.setSize(600, 500);
            oWnd.center();
            oWnd.show();
        }

        function ESignLogin() {
            var oWnd = $find("<%= winAction.ClientID %>");
            oWnd.setUrl("ESign/ESignLogin.aspx");
            oWnd.show();

            return false;
        }

        function winAction_OnClientClose(oWnd, args) {
            if (oWnd.argument) {
                if (oWnd.argument.esignkey != null) {
                    __doPostBack("<%= btnEsign.ClientID %>", "esign_" + oWnd.argument.esignkey);
                }
                else if (oWnd.argument.refresh != null) {
                    __doPostBack("<%= btnEsign.ClientID %>", "refresh");
                }
            }
        }
    </script>

    <form id="form1" runat="server" style="width: 100%; height: 100%;">
        <telerik:RadScriptManager ID="fw_RadScriptManager" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server" VisibleStatusbar="false"
            ShowContentDuringLoad="false" Behaviors="Maximize, Close,Move" Modal="True" ShowOnTopWhenMaximized="true">
        </telerik:RadWindow>
        <telerik:RadWindow ID="winAction" Animation="None" Width="400px" Height="350px"
            runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false" ReloadOnShow="True"
            Modal="true" OnClientClose="winAction_OnClientClose" Title="">
        </telerik:RadWindow>
        <table width="100%">
            <tr>
                <td style="width: 15%">
                    <asp:Button ID="btnDirectPrint" runat="server" Width="100%" Text="Print in Service Unit Printer" OnClick="btnDirectPrint_Click" />
                </td>
                <td style="width: 15%">
                    <asp:Button ID="btnSaveToGuarantorDoc" runat="server" Width="100%" Text="Save To Guarantor Document" OnClick="btnSaveToGuarantorDoc_Click" />
                </td>
                <td style="width: 15%">
                    <asp:Button ID="btnEsign" runat="server" Width="100%" Text="Esign Document" OnClientClick="return ESignLogin()" CausesValidation="false" />
                </td>
                <td style="width: 15%">
                    <asp:Button ID="btnSendToEmail" runat="server" Width="100%" Text="Send to Email" OnClick="btnSendToEmail_Click" />
                </td>
                <td style="width: 15%">
                    <asp:Button ID="btnFilter" runat="server" Width="100%" Text="Report Filter" OnClientClick="return OpenReportOption()" CausesValidation="false" />
                </td>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="right">Viewer Type&nbsp;
                            </td>
                            <td align="right">
                                <telerik:RadComboBox runat="server" ID="cboViewerType" Width="100%" AutoPostBack="true"
                                    OnSelectedIndexChanged="cboViewerType_SelectedIndexChanged">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Default" Value="1" />
                                        <telerik:RadComboBoxItem Text="PDF" Value="2" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 20px">
                    <a href="#" onclick="javascript:openInfo(); return false;">
                        <img src="../../Images/infogreen16.png" border="0" alt="Info" title="Report Information" /></a>
                </td>
            </tr>
        </table>
        <telerik:ReportViewer ID="rptViewer" runat="server" Width="100%" Height="100%" asyncrendering="False"
            sizetoreportcontent="True">
        </telerik:ReportViewer>
        <asp:Literal ID="ltEmbed" runat="server" />
    </form>

    <script language="javascript" type="text/javascript">
        ResizeReport();

        function ResizeReport() {
            var viewer = document.getElementById("<%= rptViewer.ClientID %>");
            var htmlheight = document.documentElement.clientHeight;
            viewer.style.height = (htmlheight - 50) + "px";
        }

        window.onresize = function resize() { ResizeReport(); }

        function OpenReportOption() {
            var oWnd = $find("<%= winAction.ClientID %>");
            var url = "ReportOption.aspx?id=<%= Temiang.Avicenna.Common.AppSession.PrintJobReportID %>&tp=&mode=cfvw";
            oWnd.setUrl(url);

            oWnd.setSize(500, 425);
            oWnd.center();
            oWnd.show();

            return false;
        }

        if ("<%= IsParameterRequiredComplete().ToString().ToLower() %>" == "false") {
            window.onload = function openReportOption() { OpenReportOption(); }
        }

    </script>

</body>
</html>
