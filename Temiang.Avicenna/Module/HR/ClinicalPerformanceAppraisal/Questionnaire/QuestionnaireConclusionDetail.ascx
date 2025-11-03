<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuestionnaireConclusionDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.CPA.QuestionnaireConclusionDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumClinicalPerformanceAppraisalQuestionnaireConclusion" runat="server" ValidationGroup="ClinicalPerformanceAppraisalQuestionnaireConclusion" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ClinicalPerformanceAppraisalQuestionnaireConclusion"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td width="50%" valign="Top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblConclusionGrade" runat="server" Text="Grade"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtConclusionGrade" runat="server" Width="100px" MaxLength="2" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvConclusionGrade" runat="server" ErrorMessage="Grade required."
                            ControlToValidate="txtConclusionGrade" SetFocusOnError="True" ValidationGroup="ClinicalPerformanceAppraisalQuestionnaireConclusion" Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblConclusionGradeName" runat="server" Text="Grade Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtConclusionGradeName" runat="server" Width="300px" MaxLength="255" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvConclusionGradeName" runat="server" ErrorMessage="Grade Name required."
                            ValidationGroup="ClinicalPerformanceAppraisalQuestionnaireConclusion" ControlToValidate="txtConclusionGradeName" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblMinValue" runat="server" Text="Minimum Value"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtMinValue" runat="server" Width="100px" NumberFormat-DecimalDigits="2"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvMinValue" runat="server" ErrorMessage="Minimum Value required."
                            ControlToValidate="txtMinValue" SetFocusOnError="True" ValidationGroup="ClinicalPerformanceAppraisalQuestionnaireItem" Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblMaxValue" runat="server" Text="Maximum Value"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtMaxValue" runat="server" Width="100px" NumberFormat-DecimalDigits="2"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvMaxValue" runat="server" ErrorMessage="Maximum Value required."
                            ControlToValidate="txtMaxValue" SetFocusOnError="True" ValidationGroup="ClinicalPerformanceAppraisalQuestionnaireItem" Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ClinicalPerformanceAppraisalQuestionnaireConclusion"
                            Visible='<%# !(DataItem is GridInsertionObject) %>' />
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ClinicalPerformanceAppraisalQuestionnaireConclusion" Visible='<%# DataItem is GridInsertionObject %>' />
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
