<%@  Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="ServiceUnitBookingRealizationList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.ServiceUnitBookingRealizationList" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            var selectedAppointment = null;

            function appointmentContextMenu(sender, eventArgs) {
                var menu = $find("<%= SchedulerAppointmentContextMenu.ClientID %>");
                selectedAppointment = eventArgs.get_appointment();
                menu.show(eventArgs.get_domEvent());
            }

            function appointmentContextMenuItemClicked(sender, eventArgs) {
                if (!selectedAppointment)
                    return;

                var clickedItem = eventArgs.get_item();
                switch (clickedItem.get_value()) {
                    case "Update":
                        if ('<%= IsUserApproveAble %>' == 'True') {
                            var oWnd = $find("<%= winUpdate.ClientID %>");
                            oWnd.SetUrl("ServiceUnitBookingRealizationDetail.aspx?id=" + selectedAppointment.get_id() + "&t=" + '<%= Request.QueryString["t"] %>');
                            oWnd.set_width(document.body.offsetWidth);
                            oWnd.show();
                        }
                        else
                            alert("Invalid user access.");
                        break;
                    case "Void":
                        if ('<%= IsUserVoidAble %>' == 'True') {
                            if (confirm('Are you sure to void this selected booking schedule?'))
                                $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>").ajaxRequest('void|' + selectedAppointment.get_id());
                        }
                        else
                            alert("Invalid user access.");
                        break;
                }
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument) {
                    if (oWnd.argument == 'rebind') {
                        $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>").ajaxRequest("");
                        oWnd.argument = 'undefined';
                    }
                }
            }

            function onPrintButtonClick(id) {
                $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>").ajaxRequest("print|" + id);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="fw_RadAjaxManager">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="schList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="cboParamedicID2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Timer1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fw_RadAjaxManager" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="schList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParamedic2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fw_RadAjaxManager" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="schList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="cboParamedicID2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSRRegistrationType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fw_RadAjaxManager" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="schList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="cboParamedicID2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="schList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fw_RadAjaxManager" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="schList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Timer ID="Timer1" runat="server" Interval="60000" OnTick="Timer1_Tick" />
    <telerik:RadWindow ID="winUpdate" Animation="None" Width="1000px" Height="700px"
        runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <cc:CollapsePanel ID="CollapsePanel2" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Physician"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboParamedicID2" Width="304px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterParamedic2" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilterParamedic2_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%">
                    <table width="100%">
                        <tr style="display: none">
                            <td class="label">Registration Type
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboSRRegistrationType" Width="304px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnSRRegistrationType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilterParamedic2_Click" ToolTip="Filter By Registration Type" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadScheduler runat="server" ID="schList" ShowFooter="false" EnableDescriptionField="true"
        WeekView-HeaderDateFormat="MMMM dd, yyyy" AllowDelete="false" AllowEdit="false"
        SelectedView="DayView" AllowInsert="false" ShowFullTime="false" ShowAllDayRow="false"
        Height="100%" DayStartTime="00:00:00" DayEndTime="23:59:00" GroupBy="Room" DataDescriptionField="Description"
        DataKeyField="BookingNo" DataStartField="BookingDateTimeFrom" DataEndField="BookingDateTimeTo"
        DataSubjectField="PatientName" OnNavigationComplete="schList_NavigationComplete" OnClientAppointmentContextMenu="appointmentContextMenu"
        OnClientAppointmentContextMenuItemClicked="appointmentContextMenuItemClicked"
        OnAppointmentDataBound="schList_AppointmentDataBound">
        <AppointmentContextMenus>
            <telerik:RadSchedulerContextMenu runat="server" ID="SchedulerAppointmentContextMenu">
                <Items>
                    <telerik:RadMenuItem Text="Realization" Value="Update" ImageUrl="~/Images/Toolbar/edit16.png" />
                    <telerik:RadMenuItem Text="Void" Value="Void" ImageUrl="~/Images/Toolbar/cancel16.png" />
                </Items>
            </telerik:RadSchedulerContextMenu>
            <telerik:RadSchedulerContextMenu runat="server" ID="SchedulerAppointmentContextMenu1">
                <Items>
                    <telerik:RadMenuItem Text="Realization (Update)" Value="Update" ImageUrl="~/Images/Toolbar/edit16.png" />
                    <telerik:RadMenuItem Text="Void" Value="Void" ImageUrl="~/Images/Toolbar/cancel16.png" />
                </Items>
            </telerik:RadSchedulerContextMenu>
            <telerik:RadSchedulerContextMenu runat="server" ID="SchedulerAppointmentContextMenu2">
                <Items>
                    <telerik:RadMenuItem Text="Realization (Update)" Value="Update" ImageUrl="~/Images/Toolbar/edit16.png" />
                    <telerik:RadMenuItem Text="Void" Value="Void" Enabled="false" ImageUrl="~/Images/Toolbar/cancel16.png" />
                </Items>
            </telerik:RadSchedulerContextMenu>
        </AppointmentContextMenus>
        <ResourceTypes>
            <telerik:ResourceType KeyField="RoomID" Name="Room" TextField="RoomName" ForeignKeyField="RoomID" />
        </ResourceTypes>
        <ResourceStyles>
            <telerik:ResourceStyleMapping Type="IsApproved" Text="0" ApplyCssClass="rsCategoryOrange" />
            <telerik:ResourceStyleMapping Type="IsApproved" Text="1" ApplyCssClass="rsCategoryBlue" />
        </ResourceStyles>
        <AppointmentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <div style="text-decoration: underline; font-weight: bold;">
                            <%# Eval("Subject")%>
                        </div>
                        <div>
                            <%# Eval("Description")%>
                        </div>
                    </td>
                    <td width="20px">
                        <%# string.Format("<a href=\"#\" onclick=\"onPrintButtonClick('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/print16.png\" border=\"0\" alt=\"Print\" /></a>", Eval("ID"))%>
                    </td>
                </tr>
            </table>
        </AppointmentTemplate>
    </telerik:RadScheduler>
    <telerik:RadAjaxPanel ID="radAjaxPanel" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
</asp:Content>
