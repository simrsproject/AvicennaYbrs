<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DemographyOptionCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.DemographyOptionCtl" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Option" />
        </td>
        <td>
            <asp:RadioButtonList ID="rbtDemographyOption" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                <asp:ListItem Value="0" Text="Jenis Kelamin, Umur, Pendidikan, Cara Pembayaran" />
                <asp:ListItem Value="1" Text="Domisili" />
            </asp:RadioButtonList>
        </td>
    </tr>
</table>