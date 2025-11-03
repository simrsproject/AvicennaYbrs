<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="AppointmentScheduleList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.OutPatient.AppointmentScheduleList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script src="../../../../JavaScript/DateFormat.js"></script>
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
                    case "Import":
                        if ('<%= IsUserAddAble %>' == 'True') {
                            var subject = selectedAppointment.get_subject();
                            if (subject.substring(0, 1) == '#') {
                                var unit = $find("<%= cboClusterID.ClientID %>");
                                var medic = $find("<%= cboParamedicID.ClientID %>");
                                var date = selectedAppointment.get_start();
                                var oWnd = $find("<%= winImportExcel.ClientID %>");
                                var que = subject.split('-');
                                oWnd.setUrl("AppointmentImportDialog.aspx?unit=" + unit.get_value() + "&medic=" + medic.get_value() + "&datetime=" + date.format("isoDateTime") + "&que=" + que[0].substring(1));
                                oWnd.show();
                                //oWnd.maximize();
                                oWnd.add_pageLoad(onClientPageLoad);
                            }
                            else if (subject.substring(0, 1) == '*') {
                                alert("Physician is on leave or unavailable to modify.");
                            }
                            else if (subject.substring(0, 1) == '@') {
                                alert("Clinic is already closed.");
                            }
                            else
                                alert("Slot time is already registered.");
                        }
                        else
                            alert("Invalid user access.");
                        break;
                    case "New":
                        if ('<%= IsUserAddAble %>' == 'True') {
                            var subject = selectedAppointment.get_subject();
                            if (subject.substring(0, 1) == '#') {
                                var unit = $find("<%= cboClusterID.ClientID %>");
                                var medic = $find("<%= cboParamedicID.ClientID %>");
                                var date = selectedAppointment.get_start();
                                location.replace('AppointmentDetail.aspx?md=new&id=' + selectedAppointment.get_id() + '&unit=' + unit.get_value() + '&medic=' + medic.get_value() + '&datetime=' + date.format("isoDateTime") + '&at=' + '<%= Request.QueryString["at"] %>');
                            }
                            else if (subject.substring(0, 1) == '*') {
                                alert("Physician is on leave or unavailable to modify.");
                            }
                            else if (subject.substring(0, 1) == '@') {
                                alert("Clinic is already closed.");
                            }
                            else
                                alert("Slot time is already registered.");
                        }
                        else
                            alert("Invalid user access.");
                        break;
                    case "Edit":
                        if ('<%= IsUserEditAble %>' == 'True') {
                            var subject = selectedAppointment.get_subject();
                            if ((subject.substring(0, 1) == '#') || (subject.substring(0, 1) == '*') || (subject.substring(0, 1) == '@') || (subject.substring(0, 1) == '^'))
                                alert("Slot time is unavailable to edit.");
                            else {
                                var unit = $find("<%= cboClusterID.ClientID %>");
                                var medic = $find("<%= cboParamedicID.ClientID %>");
                                location.replace('AppointmentDetail.aspx?md=edit&id=' + selectedAppointment.get_id() + '&unit=' + unit.get_value() + '&medic=' + medic.get_value() + '&at=' + '<%= Request.QueryString["at"] %>');
                            }
                        }
                        else
                            alert("Invalid user access.");
                        break;
                    case "View":
                        var subject = selectedAppointment.get_subject();
                        if ((subject.substring(0, 1) == '#') || (subject.substring(0, 1) == '*') || (subject.substring(0, 1) == '@'))
                            alert("Slot time is unavailable to view.");
                        else if ((subject.substring(0, 1) == '^')) {
                            var unit = $find("<%= cboClusterID.ClientID %>");
                            var medic = $find("<%= cboParamedicID.ClientID %>");
                            location.replace('AppointmentDetail.aspx?md=view&id=' + selectedAppointment.get_id() + '&unit=' + unit.get_value() + '&medic=' + medic.get_value() + '&at=' + '<%= Request.QueryString["at"] %>' + '&closed=1');
                        }
                        else {
                            var unit = $find("<%= cboClusterID.ClientID %>");
                            var medic = $find("<%= cboParamedicID.ClientID %>");
                            location.replace('AppointmentDetail.aspx?md=view&id=' + selectedAppointment.get_id() + '&unit=' + unit.get_value() + '&medic=' + medic.get_value() + '&at=' + '<%= Request.QueryString["at"] %>');
                        }
                        break;
                }
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.id != null)
                    $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>").ajaxRequest("");
            }

            function onClientClick() {
                var oWne = $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>").ajaxRequest("");
                alert('Populating appointment data for ' + medic.get_text());
            }

            function onNewButtonClick(id, subject, start) {
                if (subject.substring(0, 1) == '#') {
                    if (confirm('Create new appointment on selected time?')) {
                        var dt = new Date(start);
                        var unit = $find("<%= cboClusterID.ClientID %>");
                        var medic = $find("<%= cboParamedicID.ClientID %>");
                        location.replace('AppointmentDetail.aspx?md=new&id=' + id + '&unit=' + unit.get_value() + '&medic=' + medic.get_value() + '&datetime=' + dt.format("isoDateTime") + '&at=' + '<%= Request.QueryString["at"] %>');
                    }
                }
                else if (subject.substring(0, 1) == '*') {
                    alert("Physician is on leave or unavailable to modify.");
                }
                else if (subject.substring(0, 1) == '@') {
                    alert("Clinic is already closed.");
                }
                else
                    alert("Slot time is already registered.");
            }

            function onViewButtonClick(id, subject) {
                if ((subject.substring(0, 1) == '#') || (subject.substring(0, 1) == '*') || (subject.substring(0, 1) == '@'))
                    alert("Slot time is unavailable to view.");
                else if ((subject.substring(0, 1) == '^')) {
                    var unit = $find("<%= cboClusterID.ClientID %>");
                    var medic = $find("<%= cboParamedicID.ClientID %>");
                    location.replace('AppointmentDetail.aspx?md=view&id=' + id + '&unit=' + unit.get_value() + '&medic=' + medic.get_value() + '&at=' + '<%= Request.QueryString["at"] %>' + '&closed=1');
                }
                else {
                    var unit = $find("<%= cboClusterID.ClientID %>");
                    var medic = $find("<%= cboParamedicID.ClientID %>");
                    location.replace('AppointmentDetail.aspx?md=view&id=' + id + '&unit=' + unit.get_value() + '&medic=' + medic.get_value() + '&at=' + '<%= Request.QueryString["at"] %>');
                }
            }

            function onEditButtonClick(id, subject) {
                if ((subject.substring(0, 1) == '#') || (subject.substring(0, 1) == '*') || (subject.substring(0, 1) == '@') || (subject.substring(0, 1) == '^'))
                    alert("Slot time is unavailable to edit.");
                else {
                    var unit = $find("<%= cboClusterID.ClientID %>");
                    var medic = $find("<%= cboParamedicID.ClientID %>");
                    location.replace('AppointmentDetail.aspx?md=edit&id=' + id + '&unit=' + unit.get_value() + '&medic=' + medic.get_value() + '&at=' + '<%= Request.QueryString["at"] %>');
                }
            }

            function openWinImportExcel() {
                var unit = $find("<%= cboClusterID.ClientID %>");
                if (unit.get_value() == '') {
                    alert("Service unit is not selected.");
                    return;
                }
                var medic = $find("<%= cboParamedicID.ClientID %>");
                if (medic.get_value() == '') {
                    alert("Physician is not selected.");
                    return;
                }

                var oWnd = $find("<%= winImportExcel.ClientID %>");
                oWnd.setUrl("AppointmentImportExcelDialog.aspx?at=" + '<%= Request.QueryString["at"] %>' + "&unit=" + unit.get_value() + "&medic=" + medic.get_value());
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="900px" Height="350px" ShowContentDuringLoad="False"
        VisibleStatusbar="false" Modal="true" Behavior="Close" DestroyOnClose="false"
        OnClientClose="onClientClose" ID="winImportExcel">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="fw_RadAjaxManager">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="schList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboClusterID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboParamedicID" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboParamedicID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnPhysicianNotes" />
                    <telerik:AjaxUpdatedControl ControlID="lblPhysicianLeaveNotes" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Timer1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fw_RadAjaxManager" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="schList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtNoOfAppt" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchMonth">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fw_RadAjaxManager" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="schList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtNoOfAppt" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchYear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fw_RadAjaxManager" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="schList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtNoOfAppt" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchNotes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fw_RadAjaxManager" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="schList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtNoOfAppt" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchCluster">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fw_RadAjaxManager" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="schList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtNoOfAppt" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fw_RadAjaxManager" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="schList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtNoOfAppt" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtNoOfAppt" />
                    <telerik:AjaxUpdatedControl ControlID="lblPhysicianLeaveNotes" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="schList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="schList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Timer ID="Timer1" runat="server" Interval="60000" OnTick="Timer1_Tick" />
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblMonth" runat="server" Text="Month"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboMonth" Width="300px">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnSearchMonth" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboYear" Width="300px">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnSearchYear" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lblPhysicianLeaveNotes" runat="server" Text="" ForeColor="Red" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Notes"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnSearchNotes" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblClusterID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboClusterID" Width="300px" AllowCustomText="true"
                                    Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboClusterID_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnSearchCluster" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px" EnableLoadOnDemand="true"
                                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboParamedicID_ItemDataBound"
                                                OnItemsRequested="cboParamedicID_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboParamedicID_SelectedIndexChanged">
                                                <FooterTemplate>
                                                    Note : Show max 10 items
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:ImageButton ID="btnPhysicianNotes" runat="server" ImageUrl="~/Images/stickynote16.png" ToolTip="" />
                                        </td>
                                    </tr>
                                </table>

                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnSearchParamedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="lblDate" Text="Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtDate" runat="server" Width="100px" AutoPostBack="True"
                                                OnSelectedDateChanged="txtDate_SelectedDateChanged" />
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtNoOfAppt" runat="server" Width="50px" NumberFormat-DecimalDigits="0">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20">
                                <a id="btnImportExcel" runat="server" href="#" onclick="openWinImportExcel(); return false;">
                                    <img src="../../../../Images/Toolbar/imp_exp_excel16.png" border="0" /></a>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadScheduler runat="server" ID="schList" EnableDescriptionField="False"
        WeekView-HeaderDateFormat="MMMM dd, yyyy" ReadOnly="true" ShowFullTime="true"
        RowHeight="45px" ShowNavigationPane="true" Height="100%" SelectedView="TimelineView"
        ShowAllDayRow="false" DataDescriptionField="Description" DataKeyField="SlotNo"
        DataStartField="Start" OverflowBehavior="Scroll" DayStartTime="07:00" ShowViewTabs="false"
        DayEndTime="21:00" DataEndField="End" DataSubjectField="Subject" OnClientAppointmentContextMenu="appointmentContextMenu"
        OnNavigationComplete="schList_NavigationComplete"
        OnClientAppointmentContextMenuItemClicked="appointmentContextMenuItemClicked">
        <AppointmentContextMenus>
            <telerik:RadSchedulerContextMenu runat="server" ID="SchedulerAppointmentContextMenu">
                <Items>
                    <telerik:RadMenuItem Text="Reschedule" Value="Import" ImageUrl="~/Images/Toolbar/refresh16.png" />
                    <telerik:RadMenuItem IsSeparator="true" />
                    <telerik:RadMenuItem Text="New" Value="New" ImageUrl="~/Images/Toolbar/new16.png" />
                    <telerik:RadMenuItem Text="Edit" Value="Edit" ImageUrl="~/Images/Toolbar/edit16.png" />
                    <telerik:RadMenuItem IsSeparator="true" />
                    <telerik:RadMenuItem Text="View" Value="View" ImageUrl="~/Images/Toolbar/views16.png" />
                </Items>
            </telerik:RadSchedulerContextMenu>
        </AppointmentContextMenus>
        <ResourceStyles>
            <telerik:ResourceStyleMapping Type="IsApproved" Text="0" ApplyCssClass="rsCategoryOrange" />
            <telerik:ResourceStyleMapping Type="IsApproved" Text="1" ApplyCssClass="rsCategoryBlue" />
        </ResourceStyles>
        <AppointmentTemplate>
            <div id="table" style="width: 100%; border-style: none; border-width: inherit; border-color: inherit;">
                <div class="row">
                    <span class="cell" style="text-decoration: underline; font-weight: bold;">
                        <%# Eval("Subject")%>
                    </span><span class="cell" style="float: right; width: 70px;">
                        <%# string.Format("<a href=\"#\" onclick=\"onViewButtonClick('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"View\" /></a>",
                                Eval("ID"), Eval("Subject"))%>
                        &nbsp;
                        <%# string.Format("<a href=\"#\" onclick=\"onNewButtonClick('{0}','{1}','{2}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New\" /></a>",
                                Eval("ID"), Eval("Subject"), Eval("Start"))%>
                        &nbsp;
                        <%# string.Format("<a href=\"#\" onclick=\"onEditButtonClick('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"Edit\" /></a>",
                                Eval("ID"), Eval("Subject")) %>
                    </span>
                </div>
            </div>
            <%# Eval("Description")%>
        </AppointmentTemplate>
    </telerik:RadScheduler>
</asp:Content>
