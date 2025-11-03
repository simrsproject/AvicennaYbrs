<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkScheduleDetailItem.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.WorkScheduleDetailItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumWorkSchedule" runat="server" ValidationGroup="WorkSchedule" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="WorkSchedule"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
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
        <td class="label">
            <asp:Label ID="lblScheduleInDate" runat="server" Text="Schedule In"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadDatePicker ID="txtScheduleInDate" runat="server" Width="100px" DateInput-ReadOnly="false"
                DatePopupButton-Enabled="true">
            </telerik:RadDatePicker>
            <telerik:RadTimePicker ID="txtScheduleInTime" runat="server" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvScheduleInDate" runat="server" ErrorMessage="Schedule In Date required."
                ControlToValidate="txtScheduleInDate" SetFocusOnError="True" ValidationGroup="WorkSchedule"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvScheduleInTime" runat="server" ErrorMessage="Schedule In Time required."
                ControlToValidate="txtScheduleInTime" SetFocusOnError="True" ValidationGroup="WorkSchedule"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblScheduleOutDate" runat="server" Text="Schedule Out"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadDatePicker ID="txtScheduleOutDate" runat="server" Width="100px" DateInput-ReadOnly="false"
                DatePopupButton-Enabled="true">
            </telerik:RadDatePicker>
            <telerik:RadTimePicker ID="txtScheduleOutTime" runat="server" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvScheduleOutDate" runat="server" ErrorMessage="Schedule Out Date required."
                ControlToValidate="txtScheduleOutDate" SetFocusOnError="True" ValidationGroup="WorkSchedule"
                Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvScheduleOutTime" runat="server" ErrorMessage="Schedule Out Time required."
                ControlToValidate="txtScheduleOutTime" SetFocusOnError="True" ValidationGroup="WorkSchedule"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="WorkSchedule"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="WorkSchedule" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button></td>
    </tr>
</table>
