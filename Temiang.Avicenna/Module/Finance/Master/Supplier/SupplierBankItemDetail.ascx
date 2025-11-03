<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupplierBankItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Master.SupplierBankItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumSupplierBank" runat="server" BackColor="PapayaWhip"
    Font-Size="Small" BorderColor="#FF8000" BorderStyle="Solid" ValidationGroup="SupplierBank" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="SupplierBank"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBankAccountNo" runat="server" Text="Bank Account No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtBankAccountNo" runat="server" Width="300px" MaxLength="50">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvBankAccountNo" runat="server" ErrorMessage="Bank Account No required."
                            ValidationGroup="SupplierBank" ControlToValidate="txtBankAccountNo" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBankName" runat="server" Text="Bank Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtBankName" runat="server" Width="300px" MaxLength="150">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvBankName" runat="server" ErrorMessage="Bank Name required."
                            ValidationGroup="SupplierBank" ControlToValidate="txtBankName" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBankAccountName" runat="server" Text="Bank Account Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtBankAccountName" runat="server" Width="300px" MaxLength="150">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvBankAccountName" runat="server" ErrorMessage="Bank Account Name required."
                            ValidationGroup="SupplierBank" ControlToValidate="txtBankAccountName" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                    </td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="SupplierBank"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="SupplierBank" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>
