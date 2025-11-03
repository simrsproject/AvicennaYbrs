<%@ Control Language="C#" AutoEventWireup="true" Codebehind="StockGroupCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.StockGroupCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Stock Group" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboStockGroup" Width="100%" runat="server" >
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
