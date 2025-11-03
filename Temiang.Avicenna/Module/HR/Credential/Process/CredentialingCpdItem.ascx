<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CredentialingCpdItem.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.Credential.Process.CredentialingCpdItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumCredentialProcessCpd" runat="server" ValidationGroup="CredentialProcessCpd" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="CredentialProcessCpd"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="display:none">
                    <td class="label">
                        <asp:Label ID="lblCpdNo" runat="server" Text="Seq No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtCpdNo" runat="server" Width="300px" MaxLength="3" ReadOnly="true" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvCpdNo" runat="server" ErrorMessage="Seq No required."
                            ControlToValidate="txtCpdNo" SetFocusOnError="True" ValidationGroup="CredentialProcessCpd" Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCpdName" runat="server" Text="CPD Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtCpdName" runat="server" Width="300px" MaxLength="255" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvCpdName" runat="server" ErrorMessage="CPD Name required."
                            ControlToValidate="txtCpdName" SetFocusOnError="True" ValidationGroup="CredentialProcessCpd" Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblInstitutionName" runat="server" Text="Institution"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtInstitutionName" runat="server" Width="300px" MaxLength="255" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvInstitutionName" runat="server" ErrorMessage="Institution required."
                            ControlToValidate="txtInstitutionName" SetFocusOnError="True" ValidationGroup="CredentialProcessCpd" Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblTimeAndHours" runat="server" Text="Time & Number of Hours"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtTimeAndHours" runat="server" Width="300px" MaxLength="100" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvTimeAndHours" runat="server" ErrorMessage="Time And Number of Hours required."
                            ControlToValidate="txtTimeAndHours" SetFocusOnError="True" ValidationGroup="CredentialProcessCpd" Width="100%">
                            <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSkp" runat="server" Text="SKP"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtSkp" runat="server" Width="100px" NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSkp" runat="server" ErrorMessage="SKP required."
                            ControlToValidate="txtSkp" SetFocusOnError="True" ValidationGroup="CredentialProcessCpd" Width="100%">
                            <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblAchievedCompetence" runat="server" Text="Achieved Competence"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtAchievedCompetence" runat="server" Width="300px" MaxLength="255" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvAchievedCompetence" runat="server" ErrorMessage="Achieved Competence required."
                            ControlToValidate="txtAchievedCompetence" SetFocusOnError="True" ValidationGroup="CredentialProcessCpd" Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPhysicalEvidence" runat="server" Text="Physical Evidence"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtPhysicalEvidence" runat="server" Width="300px" MaxLength="100" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPhysicalEvidence" runat="server" ErrorMessage="Physical Evidence required."
                            ControlToValidate="txtPhysicalEvidence" SetFocusOnError="True" ValidationGroup="CredentialProcessCpd" Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="CredentialProcessCpd"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="CredentialProcessCpd"
                            Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top"></td>
    </tr>

</table>
