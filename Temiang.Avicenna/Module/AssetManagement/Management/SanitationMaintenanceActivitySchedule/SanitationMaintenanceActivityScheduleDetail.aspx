<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="SanitationMaintenanceActivityScheduleDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Management.SanitationMaintenanceActivityScheduleDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function openRadWindow() {
            var cboYear = $find("<%= cboPeriodYear.ClientID %>");
            var year = cboYear.get_text();
            var oWnd = $find("<%= winSchedule.ClientID %>");
            oWnd.setUrl("ScheduleDateSelect.aspx?year=" + year);
            oWnd.show();
        }

        function onWinScheduleClientClose(oWnd) {
            //Jika apply di click
            var arg = oWnd.argument;
            if (arg) {
                var ajxPanel = $find("<%= AjaxPanel.ClientID %>");
                ajxPanel.ajaxRequest('cldSchedule');
            }
            oWnd = null;
        }
    </script>

    <telerik:RadWindow runat="server" Animation="None" Width="500px" Height="330px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Title="Change Schedule"
        OnClientClose="onWinScheduleClientClose" ID="winSchedule" ReloadOnShow="true">
    </telerik:RadWindow>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPeriodYear" runat="server" Text="Year"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPeriodYear" ValidationGroup="entry" runat="server" Width="80px"
                                AutoPostBack="true" OnSelectedIndexChanged="cboPeriodYear_SelectedIndexChanged" />
                        </td>
                        <td width="50">
                            <asp:RequiredFieldValidator ID="rfvPeriodYear" runat="server" ErrorMessage="Year"
                                ValidationGroup="entry" ControlToValidate="cboPeriodYear" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRWorkTradeItem" runat="server" Text="Work Trade Item"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRWorkTradeItem" runat="server" Width="300px"
                                AllowCustomText="true" Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboSRWorkTradeItem_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="50">
                            <asp:RequiredFieldValidator ID="rfvSRWorkTradeItem" runat="server" ErrorMessage="Work Trade Item required."
                                ValidationGroup="entry" ControlToValidate="cboSRWorkTradeItem" SetFocusOnError="True"
                                Width="20px">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvSRWorkTradeItem" runat="server" ErrorMessage="Sanitation maintenance activity schedule in this year has registered"
                                ValidationGroup="entry" ControlToValidate="cboSRWorkTradeItem" SetFocusOnError="True"
                                Width="20px" OnServerValidate="cvSRWorkTradeItem_ServerValidate">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:CustomValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:Button ID="btnChangeSchedule" runat="server" Enabled="false" Text="Change Schedule"
                                OnClientClick="openRadWindow();return false;" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <telerik:RadCalendar ID="cldSchedule" runat="server" OnDayRender="CustomizeDay" Height="100px"
                    Width="150px" Enabled="false" EnableMultiSelect="False" EnableViewState="False"
                    UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" MultiViewColumns="6"
                    MultiViewRows="2" EnableMonthYearFastNavigation="False" EnableNavigation="False"
                    ShowOtherMonthsDays="False" ShowRowHeaders="False" ShowDayCellToolTips="false">
                </telerik:RadCalendar>
            </td>
        </tr>
    </table>
</asp:Content>
