<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TermCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.TermCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Term" />
        </td>
        <td>
            <telerik:RadNumericTextBox ID="txtTermValue" runat="server" Width="127px" >
            </telerik:RadNumericTextBox>
        </td>
    </tr>
</table>