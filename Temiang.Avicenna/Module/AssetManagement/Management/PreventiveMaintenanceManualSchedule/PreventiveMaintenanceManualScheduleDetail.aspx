<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="PreventiveMaintenanceManualScheduleDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.PreventiveMaintenanceManualScheduleDetail"
    Title="Untitled Page" %>

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
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitMaintenance" runat="server" Text="Maintenance Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboToServiceUnitID" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnit" runat="server" Text="Asset Location"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboFromServiceUnitID" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAssetID" runat="server" Text="Asset"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboAssetID" runat="server" Width="300px" HighlightTemplatedItems="True"
                                AutoPostBack="True" MarkFirstMatch="false" EnableLoadOnDemand="true" OnItemDataBound="cboAssetID_ItemDataBound"
                                OnItemsRequested="cboAssetID_ItemsRequested" OnSelectedIndexChanged="cboAssetID_SelectedIndexChanged">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "AssetName")%>
                                    </b>
                                    <br />
                                    Serial No :
                                    <%# DataBinder.Eval(Container.DataItem, "SerialNumber")%>
                                    <br />
                                    Location :&nbsp;<%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                                    <br />
                                    Unit Maintenance :&nbsp;<%# DataBinder.Eval(Container.DataItem, "MaintenanceServiceUnitName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 result
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="50">
                            <asp:RequiredFieldValidator ID="rfvAssetID" runat="server" ErrorMessage="Asset required."
                                ValidationGroup="entry" ControlToValidate="cboAssetID" SetFocusOnError="True"
                                Width="20px">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvAssetID" runat="server" ErrorMessage="Asset preventive maintenance schedule in this year has registered"
                                ValidationGroup="entry" ControlToValidate="cboAssetID" SetFocusOnError="True"
                                Width="20px" OnServerValidate="cvAssetID_ServerValidate">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:CustomValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBrandName" runat="server" Text="Model No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBrandName" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="50">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSerialNo" runat="server" Text="Serial No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSerialNo" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="50">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:Button ID="btnChangeSchedule" runat="server" Enabled="false" Text="Change Schedule"
                                OnClientClick="openRadWindow();return false;" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
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
