<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OutstandingCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.OutstandingCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Outstanding / Verified" />
        </td>
        <td>
            <asp:RadioButtonList ID="rbtOutStanding" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                <asp:ListItem Value="0" Text="All" Selected="True" />
                <asp:ListItem Value="1" Text="Verified" />
                <asp:ListItem Value="2" Text="Outstanding" />
            </asp:RadioButtonList>
        </td>
    </tr>
</table>
