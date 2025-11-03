<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceUnitVitalSignDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ServiceUnitVitalSignDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ServiceUnitVisitType" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitVisitType"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblVisitTypeID" runat="server" Text="Vital Sign"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboVitalSign" Width="300px" AllowCustomText="true"
                Filter="Contains" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvVisitTypeID" runat="server" ErrorMessage="Vital Sign required."
                ControlToValidate="cboVitalSign" SetFocusOnError="True" ValidationGroup="ServiceUnitVisitType"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ServiceUnitVisitType"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ServiceUnitVisitType" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
