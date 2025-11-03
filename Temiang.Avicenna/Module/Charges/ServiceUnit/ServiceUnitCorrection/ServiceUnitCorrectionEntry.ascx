<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceUnitCorrectionEntry.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Charges.ServiceUnitCorrectionEntry" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumTransChargesItem" runat="server" ValidationGroup="TransChargesItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="TransChargesItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox runat="server" ID="txtItemID" Width="100px" ReadOnly="true">
            </telerik:RadTextBox>
            &nbsp;
            <asp:Label runat="server" ID="lblItemName"></asp:Label>
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblChargeQty" runat="server" Text="Charge Quantity"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtChargeQuantity" runat="server" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvChargeQuantity" runat="server" ErrorMessage="Charge Quantity required."
                ControlToValidate="txtChargeQuantity" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                Width="100%">
                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="TransChargesItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="TransChargesItem" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
