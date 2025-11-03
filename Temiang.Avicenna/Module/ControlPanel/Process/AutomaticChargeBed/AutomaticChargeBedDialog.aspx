<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="AutomaticChargeBedDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.ControlPanel.Process.AutomaticChargeBedDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                    <table width="50%">
                        <tr>
                            <td colspan="2" align="center" style="background-color: Black; font-weight: bold;
                                color: White;">
                                <asp:Label runat="server" ID="lblText" Text="AUTOMATIC CHARGE BED" Font-Size="12"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="middle" align="right" style="width: 45%">
                                <asp:Label runat="server" ID="lblDate" Text="For Date" Font-Bold="True"></asp:Label>
                            </td>
                            <td valign="middle" align="left" style="width: 55%">
                                <telerik:RadDatePicker ID="txtDate" runat="server" Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" valign="middle" align="center">
                                <asp:Button ID="btnOk" runat="server" ValidationGroup="entry" Text="Ok" Width="70px"
                                    OnClick="btnOk_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>
</asp:Content>
