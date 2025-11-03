<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfirmAttendanceCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ConfirmAttendanceCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Confirm Attendance" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboConfirmAttendance" Width="100%" runat="server" HighlightTemplatedItems="true">
                <Items>
                    <telerik:RadComboBoxItem runat="server" Text="All" Value="" />
                    <telerik:RadComboBoxItem runat="server" Text="Confirmed" Value="1" />
                    <telerik:RadComboBoxItem runat="server" Text="Not Confirmed" Value="0" />
                </Items>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
