<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="LoginPopup.aspx.cs" Inherits="Temiang.Avicenna.LoginPopup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
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
            right: 1px;
            /*top: 176px;*/
            top: 118px;
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
    <table>
        <tr>
            <td colspan="2" align="center" style="background-color: ButtonFace; border-bottom: double; border-color: darkgray;">
                <h1>Login</h1>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;
                        <img src="../Images/keys.gif" />
            </td>
            <td>
                <table>
                    <tr>
                        <td class="labellogin">
                            <asp:Label ID="lblUserID" runat="server" Text="User ID"></asp:Label>
                        </td>
                        <td width="150px">
                            <telerik:RadTextBox ID="txtUserID" runat="server" Width="150px" AutoCompleteType="Disabled" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvUserID" runat="server" ValidationGroup="login"
                                ErrorMessage="User ID required." ControlToValidate="txtUserID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="labellogin">
                            <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                        </td>
                        <td width="150px">
                            <telerik:RadTextBox ID="txtPassword" runat="server" Width="150px" TextMode="Password" AutoCompleteType="Disabled">
                                <ClientEvents OnLoad="txtPassword_OnLoad" />
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ValidationGroup="login"
                                ErrorMessage="Password required." ControlToValidate="txtPassword" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="login" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td colspan="2" align="center">
                <asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="login"
                    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
            </td>
        </tr>
    </table>
</asp:Content>
