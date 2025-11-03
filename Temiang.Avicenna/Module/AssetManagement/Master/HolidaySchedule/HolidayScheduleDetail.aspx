<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="HolidayScheduleDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Master.HolidayScheduleDetail" Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function openRadWindow() {
            var cboYear = $find("<%= cboPeriodYear.ClientID %>");
            var year = cboYear.get_text();
            var oWnd = $find("<%= winSchedule.ClientID %>");
            oWnd.setUrl("HolidayScheduleDateSelect.aspx?year=" + year);
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
                            <asp:Label ID="lblPeriodYear" runat="server" Text="Period"></asp:Label>
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
                    <tr runat="server" id="trNew">
                        <td class="label">Copy From Period</td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboCopyYear" ValidationGroup="entry" runat="server" Width="80px" />
                            <telerik:RadButton runat="server" ID="btnCopyFrom" Text="Load" OnClick="btnCopyFrom_Click" />
                        </td>
                        <td width="50"></td>
                        <td></td>

                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%" style="display: none">
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:Button ID="btnChangeSchedule" runat="server" Enabled="False" Text="Change Schedule"
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
                <telerik:RadCalendar ID="cldSchedule" runat="server" Height="100px"
                    Width="150px" Enabled="true" EnableMultiSelect="true" EnableViewState="False"
                    UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" MultiViewColumns="6"
                    MultiViewRows="2" EnableMonthYearFastNavigation="False" EnableNavigation="False"
                    ShowOtherMonthsDays="False" ShowRowHeaders="False" ShowDayCellToolTips="false">
                </telerik:RadCalendar>
            </td>
        </tr>
    </table>
</asp:Content>
