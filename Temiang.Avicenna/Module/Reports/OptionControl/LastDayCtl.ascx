<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LastDayCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.LastDayCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Last Day" />
        </td>
        <td>
            <telerik:RadNumericTextBox runat="server" ID="numLastDay" Type="Number" MinValue="0" MaxValue="31" Value="6" NumberFormat-DecimalDigits="0">
            </telerik:RadNumericTextBox>
        </td>
    </tr>
</table>
