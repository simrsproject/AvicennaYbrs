<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemGroupGenericCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ItemGroupGenericCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Item Group" />
        </td>
        <td>
            <asp:RadioButtonList ID="rbtGeneric" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                <asp:ListItem Value="0" Text="All" />
                <asp:ListItem Value="1" Text="Generic" />
            </asp:RadioButtonList>
        </td>
    </tr>
</table>
