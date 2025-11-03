<%@ Page Title="Checkout Manual" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="BpjsCheckoutDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.BpjsCheckoutDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
    <asp:CustomValidator ID="customValidator" ValidationGroup="entry" runat="server"></asp:CustomValidator>
    <table width="100%">
        <tr>
            <td class="label">
                No. SEP
            </td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtNoSEP" Width="300px" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvNoSEP" runat="server" ErrorMessage="No SEP required."
                    ValidationGroup="entry" ControlToValidate="txtNoSEP" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                Tgl Pulang
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtTglPulang" runat="server" Width="100px" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="Tgl Pulang required."
                    ValidationGroup="entry" ControlToValidate="txtTglPulang" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td />
        </tr>
    </table>
</asp:Content>
