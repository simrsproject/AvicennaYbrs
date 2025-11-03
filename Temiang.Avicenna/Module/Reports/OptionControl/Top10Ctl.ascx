<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Top10Ctl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.Top10Ctl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Top10" />
        </td>
        <td>
            <telerik:RadNumericTextBox runat="server" ID="txtTop10" Width="50px" NumberFormat-DecimalDigits="0">
            </telerik:RadNumericTextBox>
        </td>
    </tr>
</table>
