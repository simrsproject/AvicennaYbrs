<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="ServiceUnitBookingList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.ServiceUnitBookingList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../../../../JavaScript/jquery.js"></script>

    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script language="javascript" type="text/javascript">
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
                    case "Edit":
                        if ('<%= IsUserEditAble %>' == 'True') {
                            var oWnd = $find("<%= winProcess.ClientID %>");
                            oWnd.setUrl('ServiceUnitBookingDialog.aspx?id=' + selectedAppointment.get_id() + "&t=" + '<%= Request.QueryString["t"] %>');
                            oWnd.set_width(document.body.offsetWidth);
                            oWnd.show();
                        }
                        else
                            alert("Invalid user access.");
                        break;
                    case "Void":
                        if ('<%= IsUserVoidAble %>' == 'True') {
                            

                            if (selectedAppointment.get_subject().split('-')[1] == ' A')
                                alert('Booking schedule is approved, void is invalid.');
                            else {
                                if (confirm('Are you sure to void this selected booking schedule?'))
                                    $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>").ajaxRequest('void|' + selectedAppointment.get_id());
                            }
                        }
                        else
                            alert("Invalid user access.");
                        break;
                }
            }

            function timeSlotContextMenuItemClicked(sender, eventArgs) {

                //console.log(JSON.stringify(eventArgs));
                console.log(eventArgs.get_slot().get_startTime());

                var clickedItem = eventArgs.get_item();
                switch (clickedItem.get_value()) {
                    case "CommandNewAppointment":
                        var startTime = eventArgs.get_slot().get_startTime();

                        /*startTime.setHours(startTime.getHours()-1);*/
                        startTime.setHours(startTime.getHours());

                        console.log(startTime.toString());
                        //var endTime = new Date(startTime);

                        //var datestring = (startTime.getUTCMonth()+1) + "/" + startTime.getUTCDate()  + "/" + startTime.getUTCFullYear() + "|" +
                        //    startTime.getUTCHours() + ":" + startTime.getUTCMinutes();

                        var datestring = (startTime.getMonth() + 1) + "/" + startTime.getDate() + "/" + startTime.getFullYear() + "|" +
                            startTime.getHours() + ":" + startTime.getMinutes();

                        //console.log(datestring);
                        //return;
                        var url = "ServiceUnitBookingDetail.aspx?md=new&regno=&start=" + datestring + "&t=" + '<%= Request.QueryString["t"] %>';
                        window.location.href = url;
                        break;
                }
            }


            function gotoEditUrl(id, regno) {
                var url = 'ServiceUnitBookingDetail.aspx?md=edit&id=' + id + '&regno=' + regno + "&t=" + '<%= Request.QueryString["t"] %>';
                window.location.href = url;
            }

            function gotoViewUrl(id, regno) {
                var url = 'ServiceUnitBookingDetail.aspx?md=view&id=' + id + '&regno=' + regno + "&t=" + '<%= Request.QueryString["t"] %>';
                window.location.href = url;
            }

            function gotoAddUrl(regno) {
                var url = 'ServiceUnitBookingDetail.aspx?md=new&regno=' + regno + "&t=" + '<%= Request.QueryString["t"] %>';
                window.location.href = url;
            }

            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                switch (val) {
                    case "direct":
                        var url = 'ServiceUnitBookingDetail.aspx?md=new&regno=' + "&t=" + '<%= Request.QueryString["t"] %>';
                        window.location.href = url;
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
            <telerik:AjaxSetting AjaxControlID="fw_RadAjaxManager">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="schList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="cboParamedicID2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Timer ID="Timer1" runat="server" Interval="60000" OnTick="Timer1_Tick" />
    <telerik:RadWindow runat="server" Animation="None" Width="900px" Height="680px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winProcess"
        OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="New" Value="direct" ImageUrl="~/Images/Toolbar/new16.png"
                HoveredImageUrl="~/Images/Toolbar/new16_d.png" />
        </Items>
    </telerik:RadToolBar>
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
                                    OnClick="btnFilterParamedic2_Click" ToolTip="Filter By Physician" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%">
                    <table width="100%">
                        <tr style="display: none">
                            <td class="label">
                                Registration Type
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
    <telerik:RadScheduler DataDescriptionField="Description" DataEndField="BookingDateTimeTo"
        DataKeyField="BookingNo" DataStartField="BookingDateTimeFrom" DataSubjectField="PatientName"
        EnableDescriptionField="true" GroupBy="Room" ID="schList" ReadOnly="true" runat="server"
        SelectedView="DayView" ShowAllDayRow="false" ShowFooter="false" ShowFullTime="false" 
        WeekView-HeaderDateFormat="MMMM dd, yyyy" DayStartTime="00:00:00" DayEndTime="23:59:00"
        StartEditingInAdvancedForm="false" OnNavigationComplete="schList_NavigationComplete"
        OnClientAppointmentContextMenu="appointmentContextMenu"
        OnClientAppointmentContextMenuItemClicked="appointmentContextMenuItemClicked"
        OnClientTimeSlotContextMenuItemClicked="timeSlotContextMenuItemClicked"
        OnAppointmentDataBound="schList_AppointmentDataBound">
        <AppointmentContextMenus>
            <telerik:RadSchedulerContextMenu runat="server" ID="SchedulerAppointmentContextMenu">
                <Items>
                    <telerik:RadMenuItem Text="Edit" Value="Edit" ImageUrl="~/Images/Toolbar/edit16.png" />
                    <telerik:RadMenuItem Text="Void" Value="Void" ImageUrl="~/Images/Toolbar/cancel16.png" />
                </Items>
            </telerik:RadSchedulerContextMenu>
            <telerik:RadSchedulerContextMenu runat="server" ID="SchedulerAppointmentContextMenu2">
                <Items>
                    <telerik:RadMenuItem Text="Edit" Value="Edit" ImageUrl="~/Images/Toolbar/edit16.png" />
                    <telerik:RadMenuItem Text="Void" Value="Void" Enabled="false" ImageUrl="~/Images/Toolbar/cancel16.png" />
                </Items>
            </telerik:RadSchedulerContextMenu>
        </AppointmentContextMenus>
        <TimeSlotContextMenus>
            <telerik:RadSchedulerContextMenu runat="server" ID="SchedulerTimeSlotContextMenu">
                <Items>
                    <telerik:RadMenuItem Text="New Schedule" Value="CommandNewAppointment" />
                    <telerik:RadMenuItem IsSeparator="true" />
                    <telerik:RadMenuItem Text="Go to today" Value="CommandGoToToday" />
                </Items>
            </telerik:RadSchedulerContextMenu>
        </TimeSlotContextMenus>
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
