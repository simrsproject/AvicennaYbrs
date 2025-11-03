<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="VehicleScheduleList.aspx.cs" Inherits="Temiang.Avicenna.Module.Ambulance.Transaction.VehicleScheduleList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                    case "View":
                        var url = "VehicleScheduleDetail.aspx?md=view&order=<%=Request.QueryString["order"]%>&id=" + selectedAppointment.get_id();
                        window.location.href = url;
                        break;
                    case "Edit":
                        var url = "VehicleScheduleDetail.aspx?md=edit&order=<%=Request.QueryString["order"]%>&id=" + selectedAppointment.get_id();
                        window.location.href = url;
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

                        startTime.setHours(startTime.getHours());

                        console.log(startTime.toString());
                        //var endTime = new Date(startTime);

                        var datestring = (startTime.getMonth() + 1) + "/" + startTime.getDate() + "/" + startTime.getFullYear() + "|" +
                            startTime.getHours() + ":" + startTime.getMinutes();

                        console.log(datestring);
                        //return;
                        var url = "VehicleScheduleDetail.aspx?md=new&order=<%=Request.QueryString["order"]%>&start=" + datestring;
                        //alert(url);
                        window.location.href = url;
                        break;
                }
            }
        </script>
        <style type="text/css">
            .Vehicle {
                height:45px;
                font-size:large;
                text-align:right;
            }
            .Vehicle00 {
                text-align:center;
            }
            .Vehicle01 {
                
                background-image:url('../../../Images/Vehicles/ambulance128.png');
                background-repeat:no-repeat;
                background-position-y:center;
            }
            .Vehicle02 {
                background-image:url('../../../Images/Vehicles/mobil_jenazah128.png');
                background-repeat:no-repeat;
                background-position-y:center;
            }
            .Vehicle03 {
                background-image:url('../../../Images/Vehicles/mobil_dinas128.png');
                background-repeat:no-repeat;
                background-position-y:center;
            }

            .Realized {
                background-color:orangered;
            }
            .Confirmed {
                background-color:lawngreen;
            }
        </style>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="Timer1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fw_RadAjaxManager" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="schList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="schList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="schList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="fw_RadAjaxManager">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="schList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Timer ID="Timer1" runat="server" Interval="60000" OnTick="Timer1_Tick" />
    <telerik:RadScheduler DataDescriptionField="Description" DataStartField="BookingDateTimeStart" DataEndField="BookingDateTimeEnd"
        DataKeyField="TransactionNo" DataSubjectField="Destination"
        EnableDescriptionField="true" GroupBy="Vehicle" ID="schList" ReadOnly="true" runat="server"
        SelectedView="DayView" ShowAllDayRow="false" ShowFooter="false" ShowFullTime="false"
        WeekView-HeaderDateFormat="MMMM dd, yyyy" DayStartTime="00:00:00" DayEndTime="23:59:00"
        StartEditingInAdvancedForm="false" OnNavigationComplete="schList_NavigationComplete"
        OnClientAppointmentContextMenu="appointmentContextMenu" Height="100%" OverflowBehavior="Auto"
        OnClientAppointmentContextMenuItemClicked="appointmentContextMenuItemClicked"
        OnClientTimeSlotContextMenuItemClicked="timeSlotContextMenuItemClicked"
        OnAppointmentDataBound="schList_AppointmentDataBound"
        OnResourceHeaderCreated="schList_ResourceHeaderCreated"
        OnAppointmentCreated="schList_AppointmentCreated" >
        <AppointmentContextMenus>
            <telerik:RadSchedulerContextMenu runat="server" ID="SchedulerAppointmentContextMenu">
                <Items>
                    <telerik:RadMenuItem Text="View" Value="View" ImageUrl="~/Images/Toolbar/views16.png" />
                    <telerik:RadMenuItem Text="Edit" Value="Edit" ImageUrl="~/Images/Toolbar/edit16.png" />
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
        <ResourceHeaderTemplate>
            <asp:Panel ID="ResourceImageWrapper" runat="server" CssClass="ResCustomClass">
                <asp:Label ID="lblPlateNo" runat="server"></asp:Label>
            </asp:Panel>
        </ResourceHeaderTemplate>
        <ResourceTypes>
            <telerik:ResourceType KeyField="VehicleID" Name="Vehicle" TextField="PlateNo" ForeignKeyField="VehicleID" />
        </ResourceTypes>
        <ResourceStyles>
            <telerik:ResourceStyleMapping Type="IsApprove" Text="0" ApplyCssClass="rsCategoryOrange" />
            <telerik:ResourceStyleMapping Type="IsApprove" Text="1" ApplyCssClass="rsCategoryBlue" />
        </ResourceStyles>
        <AppointmentTemplate>
            <%# Eval("Description") %>
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
