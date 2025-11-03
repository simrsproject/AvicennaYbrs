<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CredentialingLicenseItem.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.Credential.Process.CredentialingLicenseItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumCredentialProcessLicense" runat="server" ValidationGroup="CredentialProcessLicense" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="CredentialProcessLicense"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRLicenseType" runat="server" Text="License Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRLicenseType" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRLicenseType" runat="server" ErrorMessage="License Type required."
                            ControlToValidate="cboSRLicenseType" SetFocusOnError="True" ValidationGroup="CredentialProcessLicense" Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblLicenseNo" runat="server" Text="License No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtLicenseNo" runat="server" Width="300px" MaxLength="100" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvLicenseNo" runat="server" ErrorMessage="License No required."
                            ControlToValidate="txtLicenseNo" SetFocusOnError="True" ValidationGroup="CredentialProcessLicense" Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDateOfIssue" runat="server" Text="Date Of Issue"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtDateOfIssue" runat="server" Width="100px" MinDate="01/01/1900" MaxDate="12/31/2999" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvDateOfIssue" runat="server" ErrorMessage="Date Of Issue required."
                            ControlToValidate="txtDateOfIssue" SetFocusOnError="True" ValidationGroup="CredentialProcessLicense" Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblValidUntil" runat="server" Text="Valid Until"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtValidUntil" runat="server" Width="100px" MinDate="01/01/1900" MaxDate="12/31/2999" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvValidUntil" runat="server" ErrorMessage="Valid Until required."
                            ControlToValidate="txtValidUntil" SetFocusOnError="True" ValidationGroup="CredentialProcessLicense" Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="CredentialProcessLicense"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="CredentialProcessLicense"
                            Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            
        </td>
    </tr>

</table>