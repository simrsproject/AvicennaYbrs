<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DistributionTypeCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.DistributionTypeCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Distribution Type" />
        </td>
        <td>
            <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Vertical">
                <asp:ListItem Selected="true">All</asp:ListItem>
                <asp:ListItem>Out</asp:ListItem>
                <asp:ListItem>In</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
</table>
