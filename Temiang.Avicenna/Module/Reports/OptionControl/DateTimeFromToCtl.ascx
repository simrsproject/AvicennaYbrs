<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateTimeFromToCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.DateTimeFromToCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Date From" />
        </td>
        <td>
            <telerik:RadDateTimePicker runat="server" ID="txtFromDateTime" Width="150px">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                </Calendar>
                <TimeView CellSpacing="-1" Culture="Indonesian (Indonesia)">
                </TimeView>
                <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                </DateInput>
            </telerik:RadDateTimePicker>
        </td>
    </tr>
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblToDate" runat="server" Text="To" />
        </td>
        <td>
            <telerik:RadDateTimePicker runat="server" ID="txtToDateTime" Width="150px">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                </Calendar>
                <TimeView CellSpacing="-1" Culture="Indonesian (Indonesia)">
                </TimeView>
                <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                </DateInput>
            </telerik:RadDateTimePicker>
        </td>
    </tr>
</table>
