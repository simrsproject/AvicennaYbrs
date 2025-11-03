<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CredentialingCompetencyAssessmentEvaluatorItem.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.Credential.Process.CredentialingCompetencyAssessmentEvaluatorItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumCredentialCompetencyAssessmentEvaluator" runat="server" ValidationGroup="CredentialCompetencyAssessmentEvaluator" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="CredentialCompetencyAssessmentEvaluator"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEvaluatorID" runat="server" Text="Evaluator"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboEvaluatorID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboEvaluatorID_ItemDataBound"
                            OnItemsRequested="cboEvaluatorID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvEvaluatorID" runat="server" ErrorMessage="Evaluator required."
                            ControlToValidate="cboEvaluatorID" SetFocusOnError="True" ValidationGroup="CredentialCompetencyAssessmentEvaluator" Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRCompetencyAssessmentEvalRole" runat="server" Text="Role"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRCompetencyAssessmentEvalRole" runat="server" Width="300px">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRCompetencyAssessmentEvalRole" runat="server" ErrorMessage="Role required."
                            ControlToValidate="cboSRCompetencyAssessmentEvalRole" SetFocusOnError="True" ValidationGroup="CredentialCompetencyAssessmentEvaluator" Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="CredentialCompetencyAssessmentEvaluator"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="CredentialCompetencyAssessmentEvaluator"
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