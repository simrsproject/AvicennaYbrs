<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="AttendanceOutstandingList.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Transaction.AttendanceOutstandingList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">
            function openScheduleDialog(period, person, day) {
                var oWnd = window.$find("<%= winDialog.ClientID %>");
                oWnd.setUrl('ScheduleDetailDialog.aspx?period=' + period + '&person=' + person + '&day=' + day);
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" ShowContentDuringLoad="false">
        <Windows>
            <telerik:RadWindow ID="winDialog" Width="640px" Height="480px" runat="server" ShowContentDuringLoad="false"
                Behaviors="Close,Move" Modal="True">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterPayrollPeriodID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterOrganizationUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterEmployeeName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterScheduleDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterFilterType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterWorkingHour">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                    <table>
                        <tr>
                            <td class="label">Payroll Period
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboPayrollPeriodID" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterPayrollPeriodID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">Organization Unit
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboOrganizationUnitID" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterOrganizationUnitID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">Employee Name
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboEmployeeName" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterEmployeeName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table>
                        <tr>
                            <td class="label">Schedule Date
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker runat="server" ID="txtScheduleDate" Width="100px" DateInput-ReadOnly="true" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterScheduleDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">Filter Type
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboFilterType" runat="server" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="All" />
                                        <telerik:RadComboBoxItem Value="1" Text="Not Attending" />
                                        <telerik:RadComboBoxItem Value="2" Text="Check In & Not Check Out" Selected="true" />
                                        <telerik:RadComboBoxItem Value="3" Text="Complete" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterFilterType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">Working Hour
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboWorkingHour" runat="server" Width="300px" CheckBoxes="true" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterWorkingHour" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                    </table>
                </td>
                <td width="auto" valign="middle">
                    <table>
                        <tr>
                            <td valign="top">
                                <asp:ImageButton ID="btnExportToExcel" runat="server" ImageUrl="~/Images/Toolbar/imp_exp_excel16.png"
                                    OnClick="btnExportToExcel_Click" ToolTip="Export" />&nbsp;&nbsp;Attendance Report
                                <br />
                                <asp:ImageButton ID="btnExportOutstandingSchdedule" runat="server" ImageUrl="~/Images/Toolbar/imp_exp_excel16.png"
                                    OnClick="btnExportOutstandingSchdedule_Click" ToolTip="Export" />&nbsp;&nbsp;Outsanding Schedule Report                   
                                <br />
                                <asp:ImageButton ID="btnExportAttendanceCard" runat="server" ImageUrl="~/Images/Toolbar/imp_exp_excel16.png"
                                    OnClick="btnExportAttendanceCard_Click" ToolTip="Export" />&nbsp;&nbsp;Employee Attendance Report
                                <br />
                                <asp:ImageButton ID="btnExportLateness" runat="server" ImageUrl="~/Images/Toolbar/imp_exp_excel16.png"
                                    OnClick="btnExportLateness_Click" ToolTip="Export" />&nbsp;&nbsp;Employee Lateness Report
                            </td>
                            <td valign="top" style="display: none;">
                                <asp:ImageButton ID="btnExportNightShift" runat="server" ImageUrl="~/Images/Toolbar/imp_exp_excel16.png"
                                    OnClick="btnExportNightShift_Click" ToolTip="Export" />&nbsp;&nbsp;Night Shift Report
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="50" OnDetailTableDataBind="grdList_DetailTableDataBind"
        OnItemCommand="grdList_ItemCommand">
        <ExportSettings IgnorePaging="true" ExportOnlyData="true" OpenInNewWindow="true">
            <Excel Format="Xlsx" />
        </ExportSettings>
        <MasterTableView DataKeyNames="PayrollPeriodID, PersonID, EmployeeNumber" ClientDataKeyNames="PayrollPeriodID, PersonID, EmployeeNumber">
            <Columns>
                <telerik:GridButtonColumn UniqueName="ResetColumn" Text="Reset" CommandName="Reset" ConfirmText="Are you sure to reset this attendance?"
                    ButtonType="ImageButton" ImageUrl="../../../../Images/Toolbar/refresh16.png">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
                <telerik:GridTemplateColumn UniqueName="Detail" SortExpression="Detail" AllowSorting="false">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openScheduleDialog('{0}','{1}','{2}'); return false;\"><img src=\"../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"Schedule Detail\" /></a>",
                            DataBinder.Eval(Container.DataItem, "PayrollPeriodID"), DataBinder.Eval(Container.DataItem, "PersonID"), DataBinder.Eval(Container.DataItem, "Day")) %>
                    </ItemTemplate>
                    <HeaderStyle Width="30px" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="PayrollPeriodName" HeaderText="Payroll Period"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="MedicalNo" SortExpression="PayrollPeriodName"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="OrganizationUnitName" HeaderText="Organization Unit" UniqueName="OrganizationUnitName"
                    SortExpression="OrganizationUnitName" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="EmployeeNumber" HeaderText="Employee No" UniqueName="EmployeeNumber"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" SortExpression="EmployeeNumber" />
                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                    SortExpression="EmployeeName" />
                <telerik:GridBoundColumn DataField="WorkingHourName" HeaderText="Working Hour" UniqueName="WorkingHourName"
                    SortExpression="WorkingHourName" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="TapCount" HeaderText="Tap" UniqueName="TapCount"
                    SortExpression="TapCount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="ScheduleInDate" HeaderText="Schedule In Date"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="ScheduleInDate" SortExpression="ScheduleInDate"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ScheduleInTime" HeaderText="Schedule In Time" UniqueName="ScheduleInTime"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" SortExpression="ScheduleInTime" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="CheckInDate" HeaderText="Check In Date"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="CheckInDate" SortExpression="CheckInDate"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CheckInTime" HeaderText="Check In Time" UniqueName="CheckInTime"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" SortExpression="CheckInTime" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="ScheduleOutDate" HeaderText="Schedule Out Date"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="ScheduleOutDate" SortExpression="ScheduleOutDate"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ScheduleOutTime" HeaderText="Schedule Out Time" UniqueName="ScheduleOutTime"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" SortExpression="ScheduleOutTime" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="CheckOutDate" HeaderText="Check Out Date"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="CheckOutDate" SortExpression="CheckOutDate"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CheckOutTime" HeaderText="Check Out Time" UniqueName="CheckOutTime"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" SortExpression="CheckOutTime" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="detail" DataKeyNames="employeeNo,dateTimeKey" ClientDataKeyNames="employeeNo,dateTimeKey" AutoGenerateColumns="false"
                    GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridButtonColumn UniqueName="ReloadColumn" Text="Reload" CommandName="Reload" ConfirmText="Are you sure to reload this attendance?"
                            ButtonType="ImageButton" ImageUrl="../../../../Images/Toolbar/refresh16.png">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="dateTime" HeaderText="Tap History"
                            HeaderStyle-HorizontalAlign="Center" UniqueName="dateTime" SortExpression="dateTime"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn DataField="Response" HeaderText="Detail"
                            HeaderStyle-HorizontalAlign="Left" UniqueName="Response" SortExpression="Response"
                            ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
