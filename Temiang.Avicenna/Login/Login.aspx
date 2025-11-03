<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterBasePage.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="Temiang.Avicenna.Login" %>

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
    <telerik:RadFormDecorator ID="fw_RadFormDecorator" runat="server" DecoratedControls="All" />

    <div class="center-div" style="border: double; border-color: darkgray;">
        <table>
            <tr style="display: none">
                <td colspan="2" align="center" style="border-bottom: double; border-color: darkgray;">
                    <img src="../Images/LogoSCI.jpg" />&nbsp;&nbsp;
                        <img src="../Images/BSre_bssn.jpg" />
                </td>
            </tr>
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
                        <tr runat="server" id="rowUserHostName">
                            <td class="labellogin">
                                <asp:Label ID="Label1" runat="server" Text="Computer ID"></asp:Label>
                            </td>
                            <td width="150px">
                                <telerik:RadComboBox runat="server" ID="cboUserHostName" Width="150px"
                                    EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                                    OnItemsRequested="cboUserHostName_ItemsRequested" OnItemDataBound="cboUserHostName_ItemDataBound">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="entry"
                                    ErrorMessage="Computer ID required." ControlToValidate="cboUserHostName" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="labellogin">
                                <asp:Label ID="lblUserID" runat="server" Text="User ID"></asp:Label>
                            </td>
                            <td width="150px">
                                <telerik:RadTextBox ID="txtUserID" runat="server" Width="150px" AutoCompleteType="Disabled" />
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvUserID" runat="server" ValidationGroup="entry"
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
                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ValidationGroup="entry"
                                    ErrorMessage="Password required." ControlToValidate="txtPassword" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center;">
                                <telerik:RadButton ID="btnLogin" runat="server" Width="70px" Text="Login" OnClick="btnLogin_Click"
                                    ValidationGroup="entry" />
                                <telerik:RadButton ID="btnSci" runat="server" Width="70px" Text="sci" OnClick="btnSci_Click"
                                    ValidationGroup="bypass" />
                                <telerik:RadButton ID="btnSciD" runat="server" Width="80px" Text="dokter (scid)" OnClick="btnSciD_Click"
                                    ValidationGroup="bypass" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td colspan="2" align="center">
                    <asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="entry"
                        ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" style="background-color: ButtonFace; font-size: xx-small; border-top: double; border-color: darkgray;">Powered by <%=_brand.VendorName %><br />
                    Built Date: <%= ApplicationLastBuildTime() %></td>
            </tr>
        </table>
    </div>
</asp:Content>
