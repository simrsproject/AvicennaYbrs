<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FastSlowCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.FastSlowCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Moving" />
        </td>
        <td>
            <asp:RadioButtonList ID="rbtMoving" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Value="1" Text="Fast" />
                <asp:ListItem Value="0" Text="Slow" />
            </asp:RadioButtonList>
        </td>
    </tr>
</table>
