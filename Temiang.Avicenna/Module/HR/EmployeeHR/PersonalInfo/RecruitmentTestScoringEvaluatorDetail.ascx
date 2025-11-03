<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecruitmentTestScoringEvaluatorDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.RecruitmentTestScoringEvaluatorDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPersonalRecruitmentTestEvaluator" runat="server" ValidationGroup="PersonalRecruitmentTestEvaluator" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PersonalRecruitmentTestEvaluator"
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
                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboEvaluatorID_ItemDataBound"
                            OnItemsRequested="cboEvaluatorID_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboEvaluatorID_SelectedIndexChanged">
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
                            ControlToValidate="cboEvaluatorID" SetFocusOnError="True" ValidationGroup="PersonalRecruitmentTestEvaluator" Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPositionID" runat="server" Text="Position"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboPositionID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPositionID_ItemDataBound"
                            OnItemsRequested="cboPositionID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "PositionName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvPositionID" runat="server" ErrorMessage="Position required."
                            ControlToValidate="cboPositionID" SetFocusOnError="True" ValidationGroup="PersonalRecruitmentTestEvaluator" Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr runat="server" id="trScore">
                    <td class="label">
                        <asp:Label ID="lblScore" runat="server" Text="Score"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtScore" runat="server" Width="100px" MaxLength="10"
                            MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvScore" runat="server" ErrorMessage="Score required."
                            ControlToValidate="txtScore" SetFocusOnError="True" ValidationGroup="PersonalRecruitmentTestEvaluator"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PersonalRecruitmentTestEvaluator"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="PersonalRecruitmentTestEvaluator" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top"></td>
    </tr>
</table>