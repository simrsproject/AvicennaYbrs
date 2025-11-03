<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KiaCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.KiaCtl" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="KIA" />
        </td>
        <td>
            <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Vertical">
                <asp:ListItem Selected="true">Children and Babies</asp:ListItem>
                <asp:ListItem>Maternal</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
</table>