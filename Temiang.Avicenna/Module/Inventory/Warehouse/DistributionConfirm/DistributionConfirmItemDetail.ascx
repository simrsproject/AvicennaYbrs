<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DistributionConfirmItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.DistributionConfirmItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemTransactionItem" runat="server" ValidationGroup="ItemTransactionItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemTransactionItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblSequenceNo" runat="server" Text="Sequence No"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtSequenceNo" runat="server" Width="100px" MaxLength="3"
                Enabled="false" Text="" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSequenceNo" runat="server" ErrorMessage="Sequence No required."
                ControlToValidate="txtSequenceNo" SetFocusOnError="True" ValidationGroup="ItemTransactionItem"
                Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
        </td>
        <td class="entry" colspan="3">
            <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" Enabled="false" />&nbsp;<asp:Label
                ID="lblItemName" runat="server"></asp:Label>
        </td>
    </tr>
    <tr height="30">
        <td class="label">
            <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
        </td>
        <td class="entry">
            <table cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Width="100px" MaxLength="10"
                            MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="0" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtSRItemUnit" runat="server" Width="100px" ReadOnly="true" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="Quantity required."
                ControlToValidate="txtQuantity" SetFocusOnError="True" ValidationGroup="ItemTransactionItem"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemTransactionItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                OnClick="btnCancel_ButtonClick" CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
