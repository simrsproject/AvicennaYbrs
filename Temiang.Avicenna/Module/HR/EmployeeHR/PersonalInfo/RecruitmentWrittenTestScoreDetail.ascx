<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecruitmentWrittenTestScoreDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.RecruitmentWrittenTestScoreDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumRecruitmentWrittenTestScore" runat="server" ValidationGroup="RecruitmentWrittenTestScore" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="RecruitmentWrittenTestScore"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEvaluatorID" runat="server" Text="Evaluator"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboEvaluatorID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboEvaluatorID_ItemDataBound"
                            OnItemsRequested="cboEvaluatorID_ItemsRequested" OnSelectedIndexChanged="cboEvaluatorID_SelectedIndexChanged">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "PersonID") %>
                                </b>&nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 10 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvCboEvaluatorID" runat="server" ErrorMessage="Evaluator Name required."
                            ValidationGroup="entry" ControlToValidate="cboEvaluatorID" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
<%--                <tr>
                    <td class="label">
                        <asp:Label ID="lblPositionID" runat="server" Text="Position"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtPositionID" runat="server" Width="300px" MaxLength="20"
                            ReadOnly="true" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr height="30">
                    <td class="label">
                        <asp:Label ID="lblScore" runat="server" Text="Score"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtScore" runat="server" Width="100px" MaxLength="10"
                            MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvScore" runat="server" ErrorMessage="Score required."
                            ControlToValidate="txtScore" SetFocusOnError="True" ValidationGroup="RecruitmentWrittenTestScore"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>

                    </td>
                    <td></td>
                </tr>--%>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="RecruitmentWrittenTestScore"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="RecruitmentWrittenTestScore" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
