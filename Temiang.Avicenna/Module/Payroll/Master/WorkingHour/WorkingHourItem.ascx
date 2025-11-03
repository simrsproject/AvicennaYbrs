<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkingHourItem.ascx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.WorkingHourItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ServiceUnitAutoBillItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitAutoBillItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">Organization Unit
        </td>
        <td>
            <telerik:RadComboBox ID="cboOrganizationUnit" runat="server" Width="304px" AllowCustomText="true" Filter="Contains" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvOrganizationUnit" runat="server" ErrorMessage="Organization Unit required."
                ValidationGroup="ServiceUnitAutoBillItem" ControlToValidate="cboOrganizationUnit" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ServiceUnitAutoBillItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>' />
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ServiceUnitAutoBillItem" Visible='<%# DataItem is GridInsertionObject %>' />
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel" />
        </td>
    </tr>
</table>
