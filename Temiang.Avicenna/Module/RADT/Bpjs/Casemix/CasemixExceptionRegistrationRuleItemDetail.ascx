<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CasemixExceptionRegistrationRuleItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.CasemixExceptionRegistrationRuleItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ServiceUnitAutoBillItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitAutoBillItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table>
    <tr>
        <td class="label">Item Name
        </td>
        <td class="entry">
            <telerik:RadTextBox runat="server" ID="txtItemID" Width="100px" AutoPostBack="true"
                OnTextChanged="txtItemID_TextChanged" />
            <asp:Label runat="server" ID="lblItemName" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvBridgingType" runat="server" ErrorMessage="Item Name required."
                ControlToValidate="txtItemID" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label">Qty
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox runat="server" ID="txtQty" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvQty" runat="server" ErrorMessage="Qty required."
                ControlToValidate="txtQty" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
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