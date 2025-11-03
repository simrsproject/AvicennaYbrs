<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InfectionDiseaseBtnCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.InfectionDiseaseBtnCtl" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Disease Type" />
        </td>
        <td>
            <asp:RadioButtonList ID="rbtDiseaseType" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                <asp:ListItem Value="0" Text="Infection" Selected="True" />
                <asp:ListItem Value="1" Text="No Infection" />
            </asp:RadioButtonList>
        </td>
    </tr>
</table>