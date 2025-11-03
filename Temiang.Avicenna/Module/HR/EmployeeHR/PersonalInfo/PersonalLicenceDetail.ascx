<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersonalLicenceDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.PersonalLicenceDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPersonalLicence" runat="server" ValidationGroup="PersonalLicence" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PersonalLicence"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblPersonalLicenceID" runat="server" Text="Personal Licence ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtPersonalLicenceID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPersonalLicenceID" runat="server" ErrorMessage="Personal Licence ID required."
                            ControlToValidate="txtPersonalLicenceID" SetFocusOnError="True" ValidationGroup="PersonalLicence" Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>

                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRLicenceType" runat="server" Text="License Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRLicenceType" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRLicenceType" runat="server" ErrorMessage="License Type required."
                            ControlToValidate="cboSRLicenceType" SetFocusOnError="True" ValidationGroup="PersonalLicence" Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNote" runat="server" Text="License No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNote" runat="server" Width="300px" MaxLength="100" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvNote" runat="server" ErrorMessage="License No required."
                            ControlToValidate="txtNote" SetFocusOnError="True" ValidationGroup="PersonalLicence" Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblValidFrom" runat="server" Text="Valid From"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" MinDate="01/01/1900" MaxDate="12/31/2999" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvValidFrom" runat="server" ErrorMessage="Valid From required."
                            ControlToValidate="txtValidFrom" SetFocusOnError="True" ValidationGroup="PersonalLicence" Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblValidTo" runat="server" Text="Valid To"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtValidTo" runat="server" Width="100px" MinDate="01/01/1900" MaxDate="12/31/2999" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvValidTo" runat="server" ErrorMessage="Valid To required."
                            ControlToValidate="txtValidTo" SetFocusOnError="True" ValidationGroup="PersonalLicence" Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PersonalLicence"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="PersonalLicence"
                            Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblVerificationLetterNo" runat="server" Text="Verification Letter No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtVerificationLetterNo" runat="server" Width="300px" MaxLength="100" />
                    </td>
                    <td width="20px">
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblVerificationDate" runat="server" Text="Verification Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtVerificationDate" runat="server" Width="100px" />
                    </td>
                    <td width="20px">
                    </td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>

</table>
