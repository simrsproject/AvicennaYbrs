<%@ Control Language="C#" AutoEventWireup="true" Codebehind="InvoicingItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Receivable.InvoicingItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumInvoiceSupplierItem" runat="server" ValidationGroup="InvoicesItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="InvoicesItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="txtPaymentNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtPaymentNo" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblPaymentNo" runat="server" Text="Payment No"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtPaymentNo" runat="server" Width="300px" MaxLength="3"
                Enabled="false" Text="d" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvPaymentNo" runat="server" ErrorMessage="Payment No required."
                ControlToValidate="txtPaymentNo" SetFocusOnError="True" ValidationGroup="InvoicesItem"
                Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblPaymentDate" runat="server" Text="Payment Date"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadDatePicker ID="txtPaymentDate" runat="server" Width="100px" Enabled="False" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" 
                Enabled="false" />
        </td>
        <td width="20px">
            
        </td>
        <td>
        </td>
    </tr>
    <tr style="display: none">
        <td class="label">
            <asp:Label ID="lblPatientID" runat="server" Text="Patient ID"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtPatientID" runat="server" Width="300px" 
                Enabled="false" />
        </td>
        <td width="20px">
            
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" 
                Enabled="false" />
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
                MinValue="0" NumberFormat-DecimalDigits="2" />
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
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="InvoicesItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="InvoicesItem" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                OnClick="btnCancel_ButtonClick" CommandName="Cancel"></asp:Button></td>
    </tr>
</table>
