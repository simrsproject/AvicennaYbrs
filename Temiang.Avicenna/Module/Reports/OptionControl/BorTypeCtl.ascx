<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BorTypeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.BorTypeCtl" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="" />
        </td>
        <td>
            <asp:RadioButtonList ID="rbtBorType" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                <asp:ListItem Value="RI" Text="Rawat Inap" />
                <asp:ListItem Value="IPI" Text="IPI" />
                <asp:ListItem Value="RS" Text="Rumah Sakit" />
            </asp:RadioButtonList>
        </td>
    </tr>
</table>