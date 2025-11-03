<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CredentialingWorkExperienceItem.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.Credential.Process.CredentialingWorkExperienceItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumCredentialProcessWorkExperience" runat="server" ValidationGroup="CredentialProcessWorkExperience" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="CredentialProcessWorkExperience"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="display:none">
                    <td class="label">
                        <asp:Label ID="lblWorkExperienceNo" runat="server" Text="Seq No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtWorkExperienceNo" runat="server" Width="300px" MaxLength="3" ReadOnly="true"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvWorkExperienceNo" runat="server" ErrorMessage="Seq No required."
                            ControlToValidate="txtWorkExperienceNo" SetFocusOnError="True" ValidationGroup="CredentialProcessWorkExperience" Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblInstitutionName" runat="server" Text="Institution"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtInstitutionName" runat="server" Width="300px" MaxLength="255"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvInstitutionName" runat="server" ErrorMessage="Institution required."
                            ControlToValidate="txtInstitutionName" SetFocusOnError="True" ValidationGroup="CredentialProcessWorkExperience" Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblStartPeriod" runat="server" Text="Start Period"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtStartPeriod" runat="server" Width="300px" MaxLength="100"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvStartPeriod" runat="server" ErrorMessage="Start Period required."
                            ControlToValidate="txtStartPeriod" SetFocusOnError="True" ValidationGroup="CredentialProcessWorkExperience" Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEndPeriod" runat="server" Text="End Period"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtEndPeriod" runat="server" Width="300px" MaxLength="100"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvEndPeriod" runat="server" ErrorMessage="End Period required."
                            ControlToValidate="txtEndPeriod" SetFocusOnError="True" ValidationGroup="CredentialProcessWorkExperience" Width="100%">
                            <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                 <tr>
                    <td class="label">
                        <asp:Label ID="lblPositionName" runat="server" Text="Position"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtPositionName" runat="server" Width="300px" MaxLength="100"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPositionName" runat="server" ErrorMessage="Position required."
                            ControlToValidate="txtPositionName" SetFocusOnError="True" ValidationGroup="CredentialProcessWorkExperience" Width="100%">
                            <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="CredentialProcessWorkExperience"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="CredentialProcessWorkExperience"
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