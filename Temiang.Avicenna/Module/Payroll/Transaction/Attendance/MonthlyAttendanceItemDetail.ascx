<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MonthlyAttendanceItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Transaction.Attendance.MonthlyAttendanceItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumWageTransactionItem" runat="server" ValidationGroup="MonthlyAttendanceDetail" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="MonthlyAttendanceDetail"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblMonthlyAttendanceDetailID" runat="server" Text="MonthlyAttendanceDetailID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtMonthlyAttendanceDetailID" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Schedule Date
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtSchedule" runat="server" Width="100px" DateInput-ReadOnly="false"
                            DatePopupButton-Enabled="true" AutoPostBack="true" OnSelectedDateChanged="txtSchedule_SelectedDateChanged">
                        </telerik:RadDatePicker>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSchedule" runat="server" ErrorMessage="Schedule Date required."
                            ControlToValidate="txtSchedule" SetFocusOnError="True" ValidationGroup="MonthlyAttendanceDetail"
                            Width="100%">
                            <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Working Hour
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboWorkingHour" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="cboWorkingHour_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvWorkingHour" runat="server" ErrorMessage="Working Hour required."
                            ControlToValidate="cboWorkingHour" SetFocusOnError="True" ValidationGroup="MonthlyAttendanceDetail"
                            Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblScheduleInDate" runat="server" Text="Schedule In"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="txtScheduleInDate" runat="server" Width="100px" DateInput-ReadOnly="false"
                                        DatePopupButton-Enabled="true">
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <telerik:RadTimePicker ID="txtScheduleInTime" runat="server" Width="100px" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvScheduleInDate" runat="server" ErrorMessage="Schedule In Date required."
                            ControlToValidate="txtScheduleInDate" SetFocusOnError="True" ValidationGroup="MonthlyAttendanceDetail"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvScheduleInTime" runat="server" ErrorMessage="Schedule In Time required."
                            ControlToValidate="txtScheduleInTime" SetFocusOnError="True" ValidationGroup="MonthlyAttendanceDetail"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblScheduleOutDate" runat="server" Text="Schedule Out"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="txtScheduleOutDate" runat="server" Width="100px" DateInput-ReadOnly="false"
                                        DatePopupButton-Enabled="true">
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <telerik:RadTimePicker ID="txtScheduleOutTime" runat="server" Width="100px" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvScheduleOutDate" runat="server" ErrorMessage="Schedule Out Date required."
                            ControlToValidate="txtScheduleOutDate" SetFocusOnError="True" ValidationGroup="MonthlyAttendanceDetail"
                            Width="100%">
                            <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvScheduleOutTime" runat="server" ErrorMessage="Schedule Out Time required."
                            ControlToValidate="txtScheduleOutTime" SetFocusOnError="True" ValidationGroup="MonthlyAttendanceDetail"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCheckInDate" runat="server" Text="Check In"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="txtCheckInDate" runat="server" Width="100px" DateInput-ReadOnly="false"
                                        DatePopupButton-Enabled="true">
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <telerik:RadTimePicker ID="txtCheckInTime" runat="server" Width="100px" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvCheckInDate" runat="server" ErrorMessage="Check In Date required."
                            ControlToValidate="txtCheckInDate" SetFocusOnError="True" ValidationGroup="MonthlyAttendanceDetail"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvCheckInTime" runat="server" ErrorMessage="Check In Time required."
                            ControlToValidate="txtCheckInTime" SetFocusOnError="True" ValidationGroup="MonthlyAttendanceDetail"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCheckOutDate" runat="server" Text="Check Out"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="txtCheckOutDate" runat="server" Width="100px" DateInput-ReadOnly="false"
                                        DatePopupButton-Enabled="true">
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <telerik:RadTimePicker ID="txtCheckOutTime" runat="server" Width="100px" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="MonthlyAttendanceDetail"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="MonthlyAttendanceDetail" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox runat="server" ID="chkIsOvertime" Text="Overtime" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblOvertimeHours" runat="server" Text="Overtime (Hours)"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtOvertimeHours" runat="server"
                            Width="100px" NumberFormat-DecimalDigits="2" MinValue="0" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblMinutes" runat="server" Text="In Minutes"></asp:Label>
                                </td>
                                <td width="25px"></td>
                                <td class="label">
                                    <asp:Label WidthID="lblCutPercentage" runat="server" Text="Cut (%)"></asp:Label>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblLateMinutes" runat="server" Text="Late"></asp:Label>
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtLateMinutes" runat="server"
                                        Width="100px" NumberFormat-DecimalDigits="0" MinValue="0" />
                                </td>
                                <td width="10px"></td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtLateCutPercentage" runat="server"
                                        Width="100px" NumberFormat-DecimalDigits="2" MinValue="0" Type="Percent" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEarlyLeaveMinutes" runat="server" Text="Early Leave"></asp:Label>
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtEarlyLeaveMinutes" runat="server"
                                        Width="100px" NumberFormat-DecimalDigits="0" MinValue="0" />
                                </td>
                                <td width="10px"></td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtEarlyLeaveCutPercentage" runat="server"
                                        Width="100px" NumberFormat-DecimalDigits="2" MinValue="0" Type="Percent" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox runat="server" ID="chkIsHasPermission" Text="Has Permission" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox runat="server" ID="chkIsInvalid" Text="Invalid" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
