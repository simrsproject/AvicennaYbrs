<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YearOnlyCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.YearOnlyCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblYear" runat="server" Text="Year" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboPeriodYear" Width="70px" runat="server">
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
