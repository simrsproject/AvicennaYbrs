<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="MonthlyAttendanceItem.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Transaction.Attendance.MonthlyAttendanceItem" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">

            function Recalculate(id) {
                __doPostBack("<%= grdSummary.UniqueID %>", "recalculate|" + id);

            }
        </script>

    </telerik:RadCodeBlock>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="3">
                <table width="100%">
                    <tr>
                        <td class="labelcaption">
                            <asp:Label ID="lblEmployeeInfo" runat="server" Text="Employee Information"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; width: 33%">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPayrollPeriodID" runat="server" Text="Payroll Period"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPayrollPeriodID" runat="server" Width="304px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPayrollPeriodID_ItemDataBound"
                                OnItemsRequested="cboPayrollPeriodID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "PayrollPeriodCode")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "PayrollPeriodName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 12 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPayrollPeriodID" runat="server" ErrorMessage="Payroll Period required."
                                ValidationGroup="entry" ControlToValidate="cboPayrollPeriodID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width: 33%">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPersonID" runat="server" Text="Employee Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPersonID" runat="server" Width="304px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPersonID_ItemDataBound"
                                OnItemsRequested="cboPersonID_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboPersonID_SelectedIndexChanged">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPersonID" runat="server" ErrorMessage="Employee Name required."
                                ValidationGroup="entry" ControlToValidate="cboPersonID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image25" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width: 34%">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Organization Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="3">
                <table width="100%">
                    <tr>
                        <td class="labelcaption">
                            <asp:Label ID="lblDayCalculations" runat="server" Text="Day Calculations"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; width: 33%">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPayDays" runat="server" Text="Pay Days"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPayDays" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPayDays" runat="server" ErrorMessage="Pay Days required."
                                ValidationGroup="entry" ControlToValidate="txtPayDays" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblUnPayDays" runat="server" Text="UnPay Days"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtUnPayDays" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvUnPayDays" runat="server" ErrorMessage="UnPay Days required."
                                ValidationGroup="entry" ControlToValidate="txtUnPayDays" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAbsenceCount" runat="server" Text="Absence Count"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAbsenceCount" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvAbsenceCount" runat="server" ErrorMessage="Absence Count required."
                                ValidationGroup="entry" ControlToValidate="txtAbsenceCount" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSRAttedanceInsentif" runat="server" Text="Attedance Insentif"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRAttedanceInsentif" runat="server" Width="304px" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width: 33%">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblWorkingDays" runat="server" Text="Working Days"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtWorkingDays" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvWorkingDays" runat="server" ErrorMessage="Working Days required."
                                ValidationGroup="entry" ControlToValidate="txtWorkingDays" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblHolidays" runat="server" Text="Holidays"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtHolidays" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvHolidays" runat="server" ErrorMessage="Holidays required."
                                ValidationGroup="entry" ControlToValidate="txtHolidays" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblOvertimeDays" runat="server" Text="Overtime Days"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtOvertimeDays" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvOvertimeDays" runat="server" ErrorMessage="Overtime Days required."
                                ValidationGroup="entry" ControlToValidate="txtOvertimeDays" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>

                </table>
            </td>
            <td style="vertical-align: top; width: 34%">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblHolidayWorking" runat="server" Text="Holiday Working"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtHolidayWorking" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvHolidayWorking" runat="server" ErrorMessage="Holiday Working required."
                                ValidationGroup="entry" ControlToValidate="txtHolidayWorking" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLateDays" runat="server" Text="Late Days"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtLateDays" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvLateDays" runat="server" ErrorMessage="Late Days required."
                                ValidationGroup="entry" ControlToValidate="txtLateDays" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEarlyLeaveDays" runat="server" Text="Early Leave Days"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtEarlyLeaveDays" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvEarlyLeaveDays" runat="server" ErrorMessage="Early Leave Days required."
                                ValidationGroup="entry" ControlToValidate="txtEarlyLeaveDays" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%" runat="server" id="tblWorkingTime">
        <tr>
            <td colspan="3">
                <table width="100%">
                    <tr>
                        <td class="labelcaption">
                            <asp:Label ID="lblWorkingTime" runat="server" Text="Working Time (Hours)"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; width: 33%">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblOvertimeHours" runat="server" Text="Overtime Hours"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtOvertimeHours" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvOvertimeHours" runat="server" ErrorMessage="Overtime Hours required."
                                ValidationGroup="entry" ControlToValidate="txtOvertimeHours" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblConvertOvertimeHours" runat="server" Text="Convert Overtime Hours"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtConvertOvertimeHours" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvConvertOvertimeHours" runat="server" ErrorMessage="Convert Overtime Hours required."
                                ValidationGroup="entry" ControlToValidate="txtConvertOvertimeHours" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width: 33%">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBasicWorkingTime" runat="server" Text="Basic Working Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtBasicWorkingTime" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvBasicWorkingTime" runat="server" ErrorMessage="Basic Working Time required."
                                ValidationGroup="entry" ControlToValidate="txtBasicWorkingTime" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image16" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTotalWorkingTime" runat="server" Text="Total Working Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTotalWorkingTime" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTotalWorkingTime" runat="server" ErrorMessage="Total Working Time required."
                                ValidationGroup="entry" ControlToValidate="txtTotalWorkingTime" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image17" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width: 34%"></td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%" style="display: none">
        <tr>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td class="labelcaption">
                            <asp:Label ID="lblFoodExspenses" runat="server" Text="Food Expenses"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBasicFoodExspenses" runat="server" Text="Basic Food Expenses"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtBasicFoodExspenses" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvBasicFoodExspenses" runat="server" ErrorMessage="Basic Food Expenses required."
                                ValidationGroup="entry" ControlToValidate="txtBasicFoodExspenses" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image18" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblOvertimeFoodExspenses" runat="server" Text="Overtime Food Expenses"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtOvertimeFoodExspenses" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvOvertimeFoodExspenses" runat="server" ErrorMessage="Overtime Food Expenses required."
                                ValidationGroup="entry" ControlToValidate="txtOvertimeFoodExspenses" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image19" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRamadhanFoodExspenses" runat="server" Text="Ramadhan Food Expenses"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtRamadhanFoodExspenses" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRamadhanFoodExspenses" runat="server" ErrorMessage="Ramadhan Food Expenses required."
                                ValidationGroup="entry" ControlToValidate="txtRamadhanFoodExspenses" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image20" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%" style="display: none">
        <tr>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td class="labelcaption">
                            <asp:Label ID="lblShiftCompensation" runat="server" Text="Shift Compensation"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblShift1Compensation" runat="server" Text="Shift #1"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtShift1Compensation" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvShift1Compensation" runat="server" ErrorMessage="Shift 1 Compensation required."
                                ValidationGroup="entry" ControlToValidate="txtShift1Compensation" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image21" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblShift2Compensation" runat="server" Text="Shift #2"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtShift2Compensation" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvShift2Compensation" runat="server" ErrorMessage="Shift 2 Compensation required."
                                ValidationGroup="entry" ControlToValidate="txtShift2Compensation" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image22" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblShift3Compensation" runat="server" Text="Shift #3"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtShift3Compensation" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvShift3Compensation" runat="server" ErrorMessage="Shift 3 Compensation required."
                                ValidationGroup="entry" ControlToValidate="txtShift3Compensation" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image23" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblShift4Compensation" runat="server" Text="Shift #4"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtShift4Compensation" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvShift4Compensation" runat="server" ErrorMessage="Shift 4 Compensation required."
                                ValidationGroup="entry" ControlToValidate="txtShift4Compensation" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image24" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
        ShowBaseLine="true" Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Attendance List" PageViewID="pgList"
                Selected="true" />
            <telerik:RadTab runat="server" Text="Attendance Summary" PageViewID="pgSummary" />
            <telerik:RadTab runat="server" Text="Leave with Pay Cut Information" PageViewID="pgLeave" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgList" runat="server" Selected="true">
            <telerik:RadGrid ID="grdDetail" runat="server" ShowFooter="false" OnNeedDataSource="grdDetail_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdDetail_UpdateCommand" OnInsertCommand="grdDetail_InsertCommand" OnDeleteCommand="grdDetail_DeleteCommand">
                <PagerStyle Mode="NextPrevAndNumeric" />
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="MonthlyAttendanceDetailID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridDateTimeColumn DataField="ScheduleInDate" HeaderText="Schedule In Date"
                            UniqueName="ScheduleInDate" SortExpression="ScheduleInDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ScheduleInTime" HeaderText="Schedule In Time" UniqueName="ScheduleInTime"
                            SortExpression="ScheduleInTime">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn DataField="CheckInDate" HeaderText="Check In Date"
                            UniqueName="CheckInDate" SortExpression="CheckInDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="CheckInTime" HeaderText="Check In Time" UniqueName="CheckInTime"
                            SortExpression="CheckInTime">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn DataField="ScheduleOutDate" HeaderText="Schedule Out Date"
                            UniqueName="ScheduleOutDate" SortExpression="ScheduleOutDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ScheduleOutTime" HeaderText="Schedule Out Time" UniqueName="ScheduleOutTime"
                            SortExpression="ScheduleOutTime">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn DataField="CheckOutDate" HeaderText="Check Out Date"
                            UniqueName="CheckOutDate" SortExpression="CheckOutDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="CheckOutTime" HeaderText="Check Out Time" UniqueName="CheckOutTime"
                            SortExpression="CheckOutTime">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsOvertime" HeaderText="Overtime"
                            UniqueName="IsOvertime" SortExpression="IsOvertime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn DataField="OvertimeHours" HeaderText="Overtime (Hours)"
                            UniqueName="OvertimeHours" SortExpression="OvertimeHours" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn DataField="LateMinutes" HeaderText="Late (Minutes)"
                            UniqueName="LateMinutes" SortExpression="LateMinutes" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn DataField="LateCutPercentage"
                            HeaderText="Late Cut (%)" UniqueName="LateCutPercentage" SortExpression="LateCutPercentage"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn DataField="EarlyLeaveMinutes" HeaderText="Early Leave (Minutes)"
                            UniqueName="EarlyLeaveMinutes" SortExpression="EarlyLeaveMinutes" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn DataField="EarlyLeaveCutPercentage"
                            HeaderText="Early Leave Cut (%)" UniqueName="EarlyLeaveCutPercentage" SortExpression="EarlyLeaveCutPercentage"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridCheckBoxColumn DataField="IsHasPermission" HeaderText="Has Permission"
                            UniqueName="IsHasPermission" SortExpression="IsHasPermission" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn DataField="IsInvalid" HeaderText="Invalid"
                            UniqueName="IsInvalid" SortExpression="IsInvalid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn DataField="IsOff" HeaderText="Schedule Off"
                            UniqueName="IsOff" SortExpression="IsOff" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn DataField="IsPayCut" HeaderText="Pay Cut"
                            UniqueName="IsPayCut" SortExpression="IsInvalid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="MonthlyAttendanceItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="MonthlyAttendanceItemDetailEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>

        <telerik:RadPageView ID="pgSummary" runat="server">
            <telerik:RadGrid ID="grdSummary" runat="server" ShowFooter="false" OnNeedDataSource="grdSummary_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None">
                <PagerStyle Mode="NextPrevAndNumeric" />
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="MonthlyAttendanceDetailID">
                    <ColumnGroups>
                        <telerik:GridColumnGroup HeaderText="Overtime (Hours)" Name="Overtime" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                    </ColumnGroups>
                    <ColumnGroups>
                        <telerik:GridColumnGroup HeaderText="Night Shift" Name="NightShift" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                    </ColumnGroups>
                    <ColumnGroups>
                        <telerik:GridColumnGroup HeaderText="Pay Cut" Name="PayCut" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                    </ColumnGroups>
                    <Columns>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            HeaderText="" ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"Recalculate('-1'); return false;\">{0}</a>",
                                            "<img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" title=\"Racalculate All\" />")%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" onclick=\"Recalculate('{0}'); return false;\"><b>{1}</b></a>",
                              DataBinder.Eval(Container.DataItem, "MonthlyAttendanceDetailID"),
                              "<img src=\"../../../../Images/Toolbar/refresh16.png\" border=\"0\" title=\"Racalculate\" />"))%>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="WorkingHourName" HeaderText="Working Hour" UniqueName="WorkingHourName"
                            SortExpression="WorkingHourName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn DataField="ScheduleInDate" HeaderText="Schedule In Date"
                            UniqueName="ScheduleInDate" SortExpression="ScheduleInDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ScheduleInTime" HeaderText="Schedule In Time" UniqueName="ScheduleInTime"
                            SortExpression="ScheduleInTime">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn DataField="ScheduleOutDate" HeaderText="Schedule Out Date"
                            UniqueName="ScheduleOutDate" SortExpression="ScheduleOutDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ScheduleOutTime" HeaderText="Schedule Out Time" UniqueName="ScheduleOutTime"
                            SortExpression="ScheduleOutTime">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>

                        <telerik:GridNumericColumn DataField="WeekdayOvertime5WD" HeaderText="Working Day (5WD)"
                            UniqueName="WeekdayOvertime5WD" SortExpression="WeekdayOvertime5WD" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="Overtime" />
                        <telerik:GridNumericColumn DataField="HolidayOvertime5WD" HeaderText="Off Day (5WD)"
                            UniqueName="HolidayOvertime5WD" SortExpression="HolidayOvertime5WD" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="Overtime" />

                        <telerik:GridNumericColumn DataField="WeekdayOvertime6WD" HeaderText="Working Day (6WD)"
                            UniqueName="WeekdayOvertime6WD" SortExpression="WeekdayOvertime6WD" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="Overtime" />
                        <telerik:GridNumericColumn DataField="HolidayOvertime6WD" HeaderText="Off Day (6WD)"
                            UniqueName="HolidayOvertime6WD" SortExpression="HolidayOvertime6WD" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="Overtime" />

                        <telerik:GridNumericColumn DataField="LeaderShift" HeaderText="Leader Shift"
                            UniqueName="LeaderShift" SortExpression="LeaderShift" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />

                        <telerik:GridNumericColumn DataField="NightShift" HeaderText="Night Shift"
                            UniqueName="NightShift" SortExpression="NightShift" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" ColumnGroupName="NightShift" Visible="false" />
                        <telerik:GridNumericColumn DataField="WeekdayNightShift" HeaderText="Weekday"
                            UniqueName="WeekdayNightShift" SortExpression="WeekdayNightShift" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" ColumnGroupName="NightShift" />
                        <telerik:GridNumericColumn DataField="HolidayNightShift" HeaderText="Holiday"
                            UniqueName="HolidayNightShift" SortExpression="HolidayNightShift" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" ColumnGroupName="NightShift" />

                        <telerik:GridNumericColumn DataField="MealAllowance" HeaderText="Meal Allowance"
                            UniqueName="MealAllowance" SortExpression="MealAllowance" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" Visible="false" />

                        <telerik:GridNumericColumn DataField="PayCut5WD" HeaderText="5WD"
                            UniqueName="PayCut5WD" SortExpression="PayCut5WD" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" ColumnGroupName="PayCut" Visible="false" />
                        <telerik:GridNumericColumn DataField="PayCut6WD" HeaderText="6WD"
                            UniqueName="PayCut6WD" SortExpression="PayCut6WD" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" ColumnGroupName="PayCut" Visible="false" />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgLeave" runat="server">
            <telerik:RadGrid ID="grdLeave" runat="server" ShowFooter="false" OnNeedDataSource="grdLeave_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None">
                <PagerStyle Mode="NextPrevAndNumeric" />
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="LeaveRequestID">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="EmployeeLeaveTypeName" HeaderText="Leave Type"
                            UniqueName="EmployeeLeaveTypeName" SortExpression="EmployeeLeaveTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="RequestLeaveDateFrom"
                            HeaderText="From Date" UniqueName="RequestLeaveDateFrom" SortExpression="RequestLeaveDateFrom"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="RequestLeaveDateTo"
                            HeaderText="To Date" UniqueName="RequestLeaveDateTo" SortExpression="RequestLeaveDateTo"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PayCutDays" HeaderText="Pay Cut Days"
                            UniqueName="PayCutDays" SortExpression="PayCutDays" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />
                        <telerik:GridBoundColumn DataField="WorkingDayName" HeaderText="Working Day"
                            UniqueName="WorkingDayName" SortExpression="WorkingDayName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
