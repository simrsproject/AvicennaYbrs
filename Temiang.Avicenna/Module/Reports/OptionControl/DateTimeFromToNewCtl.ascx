<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateTimeFromToNewCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.DateTimeFromToNewCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Date From" />
        </td>
        <td>
            <table cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <telerik:RadDatePicker ID="txtDateFrom" runat="server" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <telerik:RadTimePicker ID="txtTimeFrom" runat="server" TimeView-Interval="01:00"
                            TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm:ss"
                            TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblToDate" runat="server" Text="To" />
        </td>
        <td>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadDatePicker ID="txtDateTo" runat="server" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <telerik:RadTimePicker ID="txtTimeTo" runat="server" TimeView-Interval="01:00" TimeView-TimeFormat="HH:mm"
                            DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm:ss" TimeView-Columns="4"
                            TimeView-StartTime="00:00" TimeView-EndTime="23:59" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
