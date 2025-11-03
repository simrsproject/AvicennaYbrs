<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeAppraisalQuestionDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.EmployeeAppraisalQuestionDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeeAppraisalQuestioner" runat="server" ValidationGroup="EmployeeAppraisalQuestioner" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeeAppraisalQuestioner"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblEmployeeAppraisalQuestionerID" runat="server" Text="ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtEmployeeAppraisalQuestionerID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvEmployeeAppraisalQuestionerID" runat="server" ErrorMessage="Employee Achievement ID required."
                            ControlToValidate="txtEmployeeAppraisalQuestionerID" SetFocusOnError="True" ValidationGroup="EmployeeAppraisalQuestioner"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblQuestionerID" runat="server" Text="Questionnaire"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboQuestionerID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboQuestionerID_ItemDataBound"
                            OnItemsRequested="cboQuestionerID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "QuestionerNo")%>
                                &nbsp;-&nbsp;
                                <%# DataBinder.Eval(Container.DataItem, "QuestionerName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 10 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvQuestionerID" runat="server" ErrorMessage="Questionnaire required."
                            ControlToValidate="cboQuestionerID" SetFocusOnError="True" ValidationGroup="EmployeeAppraisalQuestioner"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>

                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeeAppraisalQuestioner"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="EmployeeAppraisalQuestioner" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
            </table>
        </td>
    </tr>
</table>
