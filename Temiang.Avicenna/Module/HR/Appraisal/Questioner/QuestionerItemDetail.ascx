<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuestionerItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.Appraisal.QuestionerItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumAppraisalQuestionItem" runat="server" ValidationGroup="AppraisalQuestionItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="AppraisalQuestionItem"
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
                            ControlToValidate="txtQuestionCode" SetFocusOnError="True" ValidationGroup="AppraisalQuestionItem" Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
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
                        <telerik:RadTextBox ID="txtQuestionName" runat="server" Width="300px" TextMode="MultiLine" MaxLength="200" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvQuestionName" runat="server" ErrorMessage="Question Name required."
                            ValidationGroup="AppraisalQuestionItem" ControlToValidate="txtQuestionName" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="AppraisalQuestionItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>' />
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="AppraisalQuestionItem" Visible='<%# DataItem is GridInsertionObject %>' />
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" valign="Top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Description"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
                    </td>
                    <td width="20px"></td>
                    <td />
                </tr>
                <asp:Panel ID="pnlTarget" runat="server">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTarget" runat="server" Text="Target"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTarget" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAchievements" runat="server" Text="Achievements"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAchievements" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlRating" runat="server">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRating" runat="server" Text="Rating"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtRating" runat="server" Width="100px" NumberFormat-DecimalDigits="2" Type="Percent" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRating" runat="server" ErrorMessage="Rating required."
                                ControlToValidate="txtRating" SetFocusOnError="True" ValidationGroup="AppraisalCategoryQuestion" Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBenchmark" runat="server" Text="Benchmark"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtBenchmark" runat="server" Width="100px" NumberFormat-DecimalDigits="2" Type="Percent" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvBenchmark" runat="server" ErrorMessage="Benchmark required."
                                ControlToValidate="txtBenchmark" SetFocusOnError="True" ValidationGroup="AppraisalCategoryQuestion" Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </asp:Panel>
                <tr runat="server" id="trMinMaxValue">
                    <td class="label">
                        <asp:Label ID="lblMinValue" runat="server" Text="Min Value"></asp:Label>
                    </td>
                    <td class="entry">
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtMinValue" runat="server" Width="100px" NumberFormat-DecimalDigits="2" />
                                </td>
                                <td width="5px"></td>
                                <td class="label">
                                    <asp:Label ID="lblMaxValue" runat="server" Text="Max Value"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtMaxValue" runat="server" Width="100px" NumberFormat-DecimalDigits="2" />
                                </td>
                            </tr>
                        </table>

                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvMinValue" runat="server" ErrorMessage="Benchmark required."
                            ControlToValidate="txtBenchmark" SetFocusOnError="True" ValidationGroup="AppraisalCategoryQuestion" Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
