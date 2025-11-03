<%@ Control Language="C#" AutoEventWireup="true" Codebehind="InvoicingItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Receivable.Customer.InvoicingItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumInvoiceCustomerItem" runat="server" ValidationGroup="InvoiceCustomerItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="InvoicesItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="txtTransactionNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtTransactionNo" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="3"
                Enabled="false" Text="d" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvTransactionNo" runat="server" ErrorMessage="Transaction No required."
                ControlToValidate="txtTransactionNo" SetFocusOnError="True" ValidationGroup="InvoiceCustomerItem"
                Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" Enabled="False" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtAmount" runat="server" Width="100px" MaxLength="10"
                MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="2" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    
    <tr>
        <td class="label">
            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNotes" TextMode="MultiLine" runat="server" Width="300px" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="InvoiceCustomerItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="InvoiceCustomerItem" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                OnClick="btnCancel_ButtonClick" CommandName="Cancel"></asp:Button></td>
    </tr>
</table>
