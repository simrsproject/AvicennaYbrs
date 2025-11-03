<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistrationTypeBtnCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.RegistrationTypeBtnCtl" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Registration Type" />
        </td>
        <td>
            <asp:RadioButtonList ID="rbtRegistrationType" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                <asp:ListItem Value="IPR" Text="Inpatient" Selected="True" />
                <asp:ListItem Value="OPR" Text="Outpatient" />
            </asp:RadioButtonList>
        </td>
    </tr>
</table>