<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReturnStatusCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ReturnStatusCtl" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Status" />
        </td>
        <td>
            <asp:RadioButtonList ID="rbtReturnStatus" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                <asp:ListItem Value="1" Text="Sudah Kembali" />
                <asp:ListItem Value="0" Text="Belum Kembali" />
            </asp:RadioButtonList>
        </td>
    </tr>
</table>