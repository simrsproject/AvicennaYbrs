<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="ClosingPayment.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.ClosingPayment" %>

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
                            <td colspan="4" align="center" style="background-color: Black; font-weight: bold;
                                color: White;">
                                <asp:Label runat="server" ID="lblText" Text="CLOSING PAYMENT" Font-Size="12"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="middle" align="right" style="width: 20%">
                                <asp:Label runat="server" ID="lblPeriod" Text="Period :" Font-Bold="True"></asp:Label>
                            </td>
                            <td style="width: 5%">
                            </td>
                            <td valign="middle" align="left" style="width: 55%">
                                <table cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker runat="server" ID="txtDateFrom" Width="100px" />
                                        </td>
                                        <td>
                                            <telerik:RadTimePicker ID="txtTimeFrom" runat="server" TimeView-Interval="01:00"
                                                TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm:ss"
                                                TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59" Width="80px" />
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;<b>to</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker runat="server" ID="txtDateTo" Width="100px" />
                                        </td>
                                        <td>
                                            <telerik:RadTimePicker ID="txtTimeTo" runat="server" TimeView-Interval="01:00" TimeView-TimeFormat="HH:mm"
                                                DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm:ss" TimeView-Columns="4"
                                                TimeView-StartTime="00:00" TimeView-EndTime="23:59" Width="80px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td valign="middle" align="right" style="width: 20%">
                                <asp:Label runat="server" ID="lblCashier" Text="Cashier :" Font-Bold="True"></asp:Label>
                            </td>
                            <td style="width: 5%">
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cboCashier" Width="100%" runat="server" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" OnItemDataBound="cboCashier_ItemDataBound" OnItemsRequested="cboCashier_ItemsRequested">
                                    <ItemTemplate>
                                        <b>
                                            <%# DataBinder.Eval(Container.DataItem, "UserName")%>
                                        </b>
                                        <br />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" valign="middle" align="center">
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
