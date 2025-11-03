<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="ResetPassword.aspx.cs" Inherits="Temiang.Avicenna.ResetPassword" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ajxLoadingPanel" runat="server" Transparency="30">
        <img alt="Loading..." src='<%= RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Default.Ajax.loading.gif") %>'
            style="border: 0px; margin-top: 75px;" />
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="ajxPanel" runat="server" Width="100%" LoadingPanelID="ajxLoadingPanel">
        <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry"
            BackColor="#FFFFC0" Font-Size="Small" BorderColor="#FFC080" BorderStyle="Solid"
            EnableClientScript="true" />
        <asp:Panel ID="pnlInformation" Width="99%" runat="server" Visible="false" BorderColor="#FFC080"
            BackColor="#FFFFC0" BorderStyle="Solid">
            <table width="90%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50px">
                        <asp:Image ID="imgAttention" runat="server" ImageUrl="~/Images/AttentionLarge.png" />
                    </td>
                    <td align="left" valign="middle">
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
                                <telerik:RadComboBox runat="server" ID="cboUserID" Width="304px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" AutoPostBack="true" MarkFirstMatch="true" OnItemDataBound="cboUserID_ItemDataBound"
                                    OnItemsRequested="cboUserID_ItemsRequested" OnSelectedIndexChanged="cboUserID_SelectedIndexChanged">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "UserID") %>
                                        &nbsp;-&nbsp;(<%# DataBinder.Eval(Container.DataItem, "UserName")%>)
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
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
                                <telerik:RadTextBox ID="txtUserName" runat="server" Width="300px" MaxLength="50"
                                    Enabled="false" />
                            </td>
                            <td width="20">
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
                                <telerik:RadTextBox ID="txtNewPassword" runat="server" Width="300px" MaxLength="15"
                                    TextMode="Password" />
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
                                <telerik:RadTextBox ID="txtValidationNewPassword" runat="server" Width="300px" MaxLength="15"
                                    TextMode="Password" />
                            </td>
                            <td width="20">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                            </td>
                            <td class="entry">
                                <asp:CheckBox ID="chkIsLocked" runat="server" Text="Locked" />
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
                    </table>
                </td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>
</asp:Content>
