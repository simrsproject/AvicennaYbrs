<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="Temiang.Avicenna.ChangePassword" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ajxLoadingPanel" runat="server" Transparency="30">
        <img alt="Loading..." src='<%= RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Default.Ajax.loading.gif") %>'
            style="border: 0px; margin-top: 75px;" />
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="ajxPanel" runat="server" LoadingPanelID="ajxLoadingPanel">
        <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry"
            BackColor="#FFFFC0" Font-Size="Small" BorderColor="#FFC080" BorderStyle="Solid"
            EnableClientScript="true" />
        <asp:Panel ID="pnlInformation" runat="server" Visible="false" BorderColor="#FFC080"
            BackColor="#FFFFC0" BorderStyle="Solid">
            <table width="90%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="32px">
                        <asp:Image ID="imgAttention" runat="server" ImageUrl="~/Images/AttentionLarge.png" />
                    </td>
                    <td align="left" valign="middle">
                        &nbsp;&nbsp;
                        <asp:Label ID="lblInformation" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table width="100%" height="400px" style="vertical-align: middle;">
            <tr>
                <td valign="middle" align="center">
                    <table>
                        <tr>
                            <td colspan="4" align="center" style="background-color: Black; font-weight: bold;
                                color: White;">
                                User
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblUserID" runat="server" Text="User ID"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtUserID" runat="server" Width="300px" ReadOnly="true" />
                            </td>
                            <td width="20">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblUserName" runat="server" Text="User Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtUserName" runat="server" Width="300px" ReadOnly="true" />
                            </td>
                            <td width="20">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center" style="background-color: Black; font-weight: bold;
                                color: White;">
                                Current Password
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPassword" runat="server" Text="Password*"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPassword" runat="server" Width="300px" TextMode="Password" />
                            </td>
                            <td width="20">
                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Current Password required."
                                    ValidationGroup="entry" ControlToValidate="txtPassword" SetFocusOnError="True"
                                    Width="100%" EnableClientScript="true">
                                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center" style="background-color: Black; font-weight: bold;
                                color: White;">
                                New Password
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="New Password"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtNewPassword" runat="server" Width="300px" TextMode="Password" />
                            </td>
                            <td width="20">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server" Text="Validation Password"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtValidationNewPassword" runat="server" Width="300px" TextMode="Password" />
                            </td>
                            <td width="20">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button ID="btnOk" runat="server" ValidationGroup="entry" Text="Ok" Width="80px"
                                    OnClick="btnOk_Click" />
                            </td>
                        </tr>
                        <tr id="trPasswordPolicy1" runat="server" visible="false">
                            <td colspan="4" align="center" style="background-color: Yellow; font-weight: bold;
                                color: Black;">
                                Password Policy Rule
                            </td>
                        </tr>
                        <tr id="trPasswordPolicy2" runat="server" visible="false">
                            <td colspan="4" align="center" style="background-color: Yellow; color: Black;">
                                <asp:Label ID="lblPolicyInfo" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>
</asp:Content>
