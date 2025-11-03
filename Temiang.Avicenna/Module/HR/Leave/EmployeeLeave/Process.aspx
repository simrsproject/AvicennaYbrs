<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="Process.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Leave.Process" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtYear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtYear" />
                    <telerik:AjaxUpdatedControl ControlID="txtStartDate" />
                    <telerik:AjaxUpdatedControl ControlID="txtEndDate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
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
                            <td colspan="4" align="center" style="background-color: Black; font-weight: bold; color: White;">
                                <asp:Label runat="server" ID="lblText" Text="EMPLOYEE ANNUAL LEAVE PROCESS"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblYearPeriod" runat="server" Text="Year Period"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtYear" runat="server" Width="300px" MaxLength="4" AutoPostBack="true" OnTextChanged="txtYear_TextChanged" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="lblPeriod" Text="Valid from"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtStartDate" runat="server" Width="100px" MinDate="01/01/1900"
                                                MaxDate="12/31/2999" />
                                        </td>
                                        <td style="width: 15px">to
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtEndDate" runat="server" Width="100px" MinDate="01/01/1900"
                                                MaxDate="12/31/2999" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="4" valign="middle" align="center">
                                <asp:Button ID="btnOk" runat="server" ValidationGroup="entry" Text="Ok" Width="80px"
                                    OnClick="btnOk_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center" style="background-color: Yellow; font-weight: bold; color: Black;">Policy Rule
                            </td>
                        </tr>
                        <tr>
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
