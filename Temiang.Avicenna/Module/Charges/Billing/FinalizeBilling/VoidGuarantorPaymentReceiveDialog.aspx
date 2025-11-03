<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="VoidGuarantorPaymentReceiveDialog.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Charges.VoidGuarantorPaymentReceiveDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
    <asp:CustomValidator ID="customValidator" ValidationGroup="entry" runat="server"></asp:CustomValidator>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="lblNotes" Text="Void Notes" />
            </td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtNotes" Width="300px" TextMode="MultiLine" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvNotes" runat="server" ErrorMessage="Void Notes required."
                    ValidationGroup="entry" ControlToValidate="txtNotes" SetFocusOnError="True">
                    <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>&nbsp;
            </td>
            <td />
        </tr>
    </table>
</asp:Content>
