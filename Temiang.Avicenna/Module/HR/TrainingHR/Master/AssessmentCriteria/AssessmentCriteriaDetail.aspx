<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="AssessmentCriteriaDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.TrainingHR.Master.AssessmentCriteria.AssessmentCriteriaDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td width="50%">
                <table width="100%">
                    <tr style="display:none">
                        <td class="label">
                            <asp:Label ID="lblAssessmentCriteriaID" runat="server" Text="ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAssessmentCriteriaID" runat="server" />
                        </td>
                        <td width="20px">
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAssessmentCriteriaName" runat="server" Text="Assessment Criteria"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAssessmentCriteriaName" runat="server" Width="300px" MaxLength="200" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvAssessmentCriteriaName" runat="server" ErrorMessage="Assessment Criteria is required."
                                ValidationGroup="entry" ControlToValidate="txtAssessmentCriteriaName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMinValue" runat="server" Text="Min Value"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtMinValue" runat="server" Width="100px" Value="0" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvMinValue" runat="server" ErrorMessage="Min Value is required."
                                ValidationGroup="entry" ControlToValidate="txtMinValue" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMaxValue" runat="server" Text="Max Value"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtMaxValue" runat="server" Width="100px" Value="0" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvMaxValue" runat="server" ErrorMessage="Max Value is required."
                                ValidationGroup="entry" ControlToValidate="txtMaxValue" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRecommendation" runat="server" Text="Conclution / Recommendation"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRecommendation" runat="server" Width="300px" MaxLength="255" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRecommendation" runat="server" ErrorMessage="Conclution / Recommendation is required."
                                ValidationGroup="entry" ControlToValidate="txtRecommendation" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                </table>
            </td>
        </tr>
    </table>
</asp:Content>