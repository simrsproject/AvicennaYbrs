<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterBasePage.Master" AutoEventWireup="true" CodeBehind="PassCodeWithoutMenu.aspx.cs" Inherits="Temiang.Avicenna.PassCodeWithoutMenu" Title="Passcode" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            right: 20px;
            top: 84px;
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

        function openWindow(url) {
            var oWnd = radopen(url, 'winDialog');
        }
    </script>
    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" ShowContentDuringLoad="false" >
        <Windows>
            <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server"
                ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" Modal="True">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel runat="server" ID="ajxPanel">
        <div class="center-div">
            <fieldset>
                <legend>
                    <asp:Literal runat="server" id="litPassCodeCaption"></asp:Literal>
                </legend>
                <table>
                    <tr>
                        <td>&nbsp;&nbsp;
                    <img src="../Module/../Images/keys.gif" />
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
                <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
            </fieldset>

            <asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="entry"
                ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
            <table align="center">
                <tr>
                    <td>
                        <asp:Button ID="btnOk" Text="Ok" runat="server" Width="100px" OnClick="btnOk_Click" ValidationGroup="entry" />
                    </td>
                </tr>
            </table>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
