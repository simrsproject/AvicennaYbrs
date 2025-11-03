<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AmountCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.AmountCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Amount" />
        </td>
        <td>
            <telerik:RadNumericTextBox ID="txtAmountValue" runat="server" Width="127px" >
            </telerik:RadNumericTextBox>
        </td>
    </tr>
</table>