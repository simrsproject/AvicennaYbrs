<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GuarantorTypeGroupCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.GuarantorTypeGroupCtl" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Guarantor Type" />
        </td>
        <td>
            <asp:RadioButtonList ID="rbtGuarantorTypeGroup" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                <asp:ListItem Value="1" Text="Personal" />
                <asp:ListItem Value="2" Text="Coorporate & Insurance" />
                <asp:ListItem Value="3" Text="BPJS" />
            </asp:RadioButtonList>
        </td>
    </tr>
</table>