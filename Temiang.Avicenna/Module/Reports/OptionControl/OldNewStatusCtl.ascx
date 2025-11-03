<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OldNewStatusCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.OldNewStatusCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Status Patient" />
        </td>
        <td>
            <asp:RadioButtonList ID="rbtOldNewStatus" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                <asp:ListItem Value="0" Text="All" />
                <asp:ListItem Value="1" Text="New" />
                <asp:ListItem Value="2" Text="Old" />
            </asp:RadioButtonList>
        </td>
    </tr>
</table>
