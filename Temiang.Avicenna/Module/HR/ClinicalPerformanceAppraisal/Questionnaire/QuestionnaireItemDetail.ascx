<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuestionnaireItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.CPA.QuestionnaireItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumClinicalPerformanceAppraisalQuestionnaireItem" runat="server" ValidationGroup="ClinicalPerformanceAppraisalQuestionnaireItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ClinicalPerformanceAppraisalQuestionnaireItem"
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
                            ControlToValidate="txtQuestionCode" SetFocusOnError="True" ValidationGroup="ClinicalPerformanceAppraisalQuestionnaireItem" Width="100%">
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
                        <asp:Label ID="lblQuestionGroupName" runat="server" Text="Question Group"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboQuestionGroupName" runat="server" Width="300px" AllowCustomText="true" />
                    </td>
                    <td width="20px"></td>
                    <td />
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
                            ValidationGroup="ClinicalPerformanceAppraisalQuestionnaireItem" ControlToValidate="txtQuestionName" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsDetail" runat="server" Text="Detail" />
                    </td>
                    <td width="20px"></td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblLoadScore" runat="server" Text="Load Score"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtLoadScore" runat="server" Width="100px" NumberFormat-DecimalDigits="0"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvLoadScore" runat="server" ErrorMessage="Load Score required."
                            ControlToValidate="txtLoadScore" SetFocusOnError="True" ValidationGroup="ClinicalPerformanceAppraisalQuestionnaireItem" Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ClinicalPerformanceAppraisalQuestionnaireItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>' />
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ClinicalPerformanceAppraisalQuestionnaireItem" Visible='<%# DataItem is GridInsertionObject %>' />
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
