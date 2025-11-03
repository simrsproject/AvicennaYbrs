<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuestionnaireItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.Credential.Questionnaire.QuestionnaireItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumCredentialQuestionnaireItem" runat="server" ValidationGroup="CredentialQuestionnaireItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="CredentialQuestionnaireItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td width="50%" valign="Top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblQuestionCode" runat="server" Text="Question Code"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtQuestionCode" runat="server" Width="300px" MaxLength="50" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvQuestionCode" runat="server" ErrorMessage="Question Code required."
                            ControlToValidate="txtQuestionCode" SetFocusOnError="True" ValidationGroup="CredentialQuestionnaireItem" Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblQuestionNo" runat="server" Text="Question No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtQuestionNo" runat="server" Width="300px" MaxLength="50" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblQuestionName" runat="server" Text="Question Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtQuestionName" runat="server" Width="300px" TextMode="MultiLine" Height="80px" MaxLength="255" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvQuestionName" runat="server" ErrorMessage="Question Name required."
                            ValidationGroup="CredentialQuestionnaireItem" ControlToValidate="txtQuestionName" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRCredentialActionType" runat="server" Text="Action Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboSRCredentialActionType" Width="300px" AllowCustomText="true"
                            Filter="Contains">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRCredentialQuestionLevel" runat="server" Text="Question Level"></asp:Label>
                    </td>
                    <td class="entry">
                        <asp:RadioButtonList ID="rbtSRCredentialQuestionLevel" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="00" Text="0" />
                            <asp:ListItem Value="01" Text="1" />
                            <asp:ListItem Value="02" Text="2" />
                            <asp:ListItem Value="03" Text="3" />
                            <asp:ListItem Value="04" Text="4" />
                            <asp:ListItem Value="05" Text="5" />
                        </asp:RadioButtonList>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRCredentialQuestionLevel" runat="server" ErrorMessage="Question Level required."
                            ValidationGroup="CredentialQuestionnaireItem" ControlToValidate="rbtSRCredentialQuestionLevel" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                    </td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsDetail" runat="server" Text="Detail" />
                    </td>
                    <td width="20px">
                    </td>
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="CredentialQuestionnaireItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>' />
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="CredentialQuestionnaireItem" Visible='<%# DataItem is GridInsertionObject %>' />
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" valign="Top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>
