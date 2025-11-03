<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormulariumNonFormulariumCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.FormulariumNonFormulariumCtl" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="" />
        </td>
        <td>
            <asp:RadioButtonList ID="rbtFormulariumNonFormularium" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                <asp:ListItem Value="1" Text="Formularium" />
                <asp:ListItem Value="0" Text="Non Formularium" />
            </asp:RadioButtonList>
        </td>
    </tr>
</table>