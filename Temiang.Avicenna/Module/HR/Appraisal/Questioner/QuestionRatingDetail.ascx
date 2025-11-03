<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuestionRatingDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.Appraisal.QuestionRatingDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:ValidationSummary ID="vsumAppraisalQuestionRating" runat="server" ValidationGroup="AppraisalQuestionRating" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="AppraisalQuestionRating"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRatingCode" runat="server" Text="Code"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtRatingCode" runat="server" Width="300px" MaxLength="10" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRatingCode" runat="server" ErrorMessage="Rating Code required."
                            ControlToValidate="txtRatingCode" SetFocusOnError="True" ValidationGroup="AppraisalQuestionRating" Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRatingName" runat="server" Text="Rating Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtRatingName" runat="server" Width="300px" MaxLength="200" TextMode="MultiLine" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRatingName" runat="server" ErrorMessage="Rating Name required."
                            ControlToValidate="txtRatingName" SetFocusOnError="True" ValidationGroup="AppraisalQuestionRating" Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr runat="server" id="trRatingValue">
                    <td class="label">
                        <asp:Label ID="lblRatingValue" runat="server" Text="Rating Value"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtRatingValue" runat="server" Width="100px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRatingValue" runat="server" ErrorMessage="Rating Value required."
                            ControlToValidate="txtRatingValue" SetFocusOnError="True" ValidationGroup="AppraisalQuestionRating" Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="AppraisalQuestionRating"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="AppraisalQuestionRating"
                            Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                CommandName="Cancel"></asp:Button></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr runat="server" id="trRatingValueMinMax">
                    <td class="label">
                        <asp:Label ID="lblRatingValueMin" runat="server" Text="Min Value"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtRatingValueMin" runat="server" Width="100px" NumberFormat-DecimalDigits="2"/>
                                </td>
                                <td style="width: 15px"></td>
                                <td class="label">
                                    <asp:Label ID="lblRatingValueMax" runat="server" Text="Max Value"></asp:Label>
                                </td>
                                <td style="width: 5px"></td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtRatingValueMax" runat="server" Width="100px" NumberFormat-DecimalDigits="2"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRatingValueMin" runat="server" ErrorMessage="Min Value required."
                            ControlToValidate="txtRatingValueMin" SetFocusOnError="True" ValidationGroup="AppraisalQuestionRating" Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvRatingValueMax" runat="server" ErrorMessage="Max Value required."
                            ControlToValidate="txtRatingValueMax" SetFocusOnError="True" ValidationGroup="AppraisalQuestionRating" Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr runat="server" id="trRatingValueMinMaxPercent">
                    <td class="label">
                        <asp:Label ID="Label1" runat="server" Text="Min Value"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtRatingValueMinPercent" runat="server" Width="100px" NumberFormat-DecimalDigits="2" Type="Percent" />
                                </td>
                                <td style="width: 15px"></td>
                                <td class="label">
                                    <asp:Label ID="Label2" runat="server" Text="Max Value"></asp:Label>
                                </td>
                                <td style="width: 5px"></td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtRatingValueMaxPercent" runat="server" Width="100px" NumberFormat-DecimalDigits="2" Type="Percent"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRatingValueMinPercent" runat="server" ErrorMessage="Min Value required."
                            ControlToValidate="txtRatingValueMinPercent" SetFocusOnError="True" ValidationGroup="AppraisalQuestionRating" Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvRatingValueMaxPercent" runat="server" ErrorMessage="Max Value required."
                            ControlToValidate="txtRatingValueMaxPercent" SetFocusOnError="True" ValidationGroup="AppraisalQuestionRating" Width="100%">
                            <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
