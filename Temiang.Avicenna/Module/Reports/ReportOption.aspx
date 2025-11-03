<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportOption.aspx.cs" Inherits="Temiang.Avicenna.Module.Reports.ReportOption" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Option</title>
</head>
<body>
    <style type="text/css">
        .center-div {
            position: fixed;
            top: 50%;
            left: 50%; /* bring your own prefixes */
            transform: translate(-50%, -50%);
        }

        td.labellogin {
            width: 100px;
            padding-left: 10px;
            background-color: ButtonFace;
            color: ButtonText;
            text-align: left;
            height: 24px;
        }

        .reveal-eye {
            position: relative;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            position: absolute;
            right: 140px;
            top: 135px;
            bottom: 1px;
            z-index: 2;
            width: 26px;
            height: 24px;
            background: #fff url(https://dtzbdy9anri2p.cloudfront.net/cache/b55f544d09a0872a74b4427ce1fe18dd78418396/telerik/img/dist/reveal-password.png) 50% 50% no-repeat;
            cursor: pointer;
            visibility: hidden;
            opacity: 0;
            transition: opacity .2s ease 0s,visibility 0s linear .2s;
        }

            .reveal-eye.is-visible {
                display: block;
                visibility: visible;
                opacity: 1;
                transition: opacity .2s ease 0s,visibility 0s linear 0s;
            }
    </style>
    <script type="text/javascript">
        function checkShowPasswordVisibility() {
            var $revealEye = $telerik.$(this).parent().find(".reveal-eye");
            if (this.value) {
                $revealEye.addClass("is-visible");
            } else {
                $revealEye.removeClass("is-visible");
            }
        }
        function txtPassword_OnLoad(sender, args) {
            var $revealEye = $telerik.$('<span class="reveal-eye"></span>')

            $telerik.$(sender.get_element()).parent().append($revealEye);
            $telerik.$(sender.get_element()).on("keyup", checkShowPasswordVisibility)

            $revealEye.on({
                mousedown: function () { sender.get_element().setAttribute("type", "text") },
                mouseup: function () { sender.get_element().setAttribute("type", "password") },
                mouseout: function () { sender.get_element().setAttribute("type", "password") }
            });
        }
    </script>

    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }
        function Close() {
            GetRadWindow().close();
        }
        function OnRequestStart(sender, args) {
            if ("<%= IsCalledFromReportViewer.ToString().ToLower() %>" == "false") {
                var btnPreview = document.getElementById("<%= btnPreview.ClientID %>");
                btnPreview.disabled = true;
            }
        }
        function OnResponseEnd(sender, args) {
            
            var val = document.getElementById("hdnUrl").value;
            if (val == null || val === "") return;

            if (val === "refresh") {
                // Refresh main page
                var oWnd = GetRadWindow();
                oWnd.argument = new Object();
                oWnd.argument.refresh = true;

                //Close the RadWindow            
                oWnd.close();
                return;
            }

            var btnPreview = document.getElementById("<%= btnPreview.ClientID %>");
            btnPreview.disabled = false;

            //if (val == '' || val == null) val = 'ReportViewer.aspx';

            if ("<%=Request.QueryString["tp"].ToLower() %>" == "rpt" || "<%=Request.QueryString["tp"].ToLower() %>" == "xml") {
                openWindowMax(val, "");
            }
            else {
                openWindowMax("PivotViewer.aspx", "");
            }
        }

        function openWindow(url) {
            var oWnd = radopen(url, 'winDialog');
        }
    </script>

    <form id="form1" runat="server">
        <asp:HiddenField ID="hdnUrl" runat="server" />
        <telerik:RadScriptManager ID="scriptMgr" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="ajxManager" runat="server">
            <ClientEvents OnResponseEnd="OnResponseEnd" OnRequestStart="OnRequestStart"></ClientEvents>
        </telerik:RadAjaxManager>
        <telerik:RadSkinManager ID="RadSkinManager1" runat="server" Skin="WebBlue">
        </telerik:RadSkinManager>
        <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
                                  Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
                                  ReloadOnShow="True" ShowContentDuringLoad="false" >
            <Windows>
                <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server"
                                   ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" Modal="True">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <table align="center">
            <tr>
                <td>
                    <h2>
                        <asp:Label runat="server" ID="lblReportName"></asp:Label></h2>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlPassCode" Style="width: 98%;" runat="server">
            <fieldset>
                <legend>
                    <asp:Literal runat="server" id="litPassCodeCaption"></asp:Literal>
                </legend>
                <table>
                    <tr>
                        <td>&nbsp;&nbsp;
                    <img src="../../Images/keys.gif" />
                        </td>
                        <td>
                            <table width="240px">
                                <tr>
                                    <td class="labellogin">Passcode
                                    </td>
                                    <td width="150px">
                                        <telerik:RadTextBox ID="txtPassword" runat="server" Width="120px" TextMode="Password" AutoCompleteType="Disabled">
                                            <ClientEvents OnLoad="txtPassword_OnLoad" />
                                        </telerik:RadTextBox>
                                    </td>
                                    <td width="20px"></td>
                                </tr>

                            </table>
                        </td>
                    </tr>

                </table>
                
            </fieldset>
        </asp:Panel>
        <fieldset>
            <legend>
                <h3>Report Criteria</h3>
            </legend>
            <asp:Panel ID="pnlAreaOption" Style="width: 98%;" runat="server">
            </asp:Panel>
        </fieldset>
        <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
        <asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="entry"
            ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
        <table align="center">
            <tr>
                <td>
                    <asp:Button ID="btnViewPdf" Text="View as PDF" runat="server" Width="100px" OnClick="btnViewPdf_Click" Visible="false" ValidationGroup="entry"/>
                </td>
                <td>
                    <asp:Button ID="btnExport" Text="Export to Excel" runat="server" Width="100px" OnClick="btnExport_Click" ValidationGroup="entry"/>
                </td>
                <td>
                    <asp:Button ID="btnPreview" Text="Preview" runat="server" Width="100px" OnClick="btnPreview_Click" ValidationGroup="entry"/>
                    <asp:Button ID="btnOk" Text="Ok" runat="server" Width="100px" OnClick="btnOk_Click" ValidationGroup="entry"/>
                </td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" Width="100px" Text="Close" OnClientClick="Close();return false;" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
