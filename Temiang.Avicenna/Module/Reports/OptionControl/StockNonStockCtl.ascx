<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StockNonStockCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.StockNonStockCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Stock / Non Stock" />
        </td>
        <td>
            <asp:RadioButtonList ID="rbtStockNonStock" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                <asp:ListItem Value="0" Text="All" Selected="True" />
                <asp:ListItem Value="1" Text="Stock" />
                <asp:ListItem Value="2" Text="Non Stock" />
            </asp:RadioButtonList>
        </td>
    </tr>
</table>
