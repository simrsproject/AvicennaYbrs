<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="AssessmentAspectDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.TrainingHR.Master.AssessmentAspect.AssessmentAspectDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAssessmentAspectID" runat="server" Text="ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAssessmentAspectID" runat="server" Width="100px" MaxLength="10" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Assessment Aspect ID is required."
                                ValidationGroup="entry" ControlToValidate="txtAssessmentAspectID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAssessmentAspectName" runat="server" Text="Assessment Aspect"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAssessmentAspectName" runat="server" Width="300px" MaxLength="255" TextMode="MultiLine"/>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvAssessmentAspectName" runat="server" ErrorMessage="Assessment Aspect Name is required."
                                ValidationGroup="entry" ControlToValidate="txtAssessmentAspectName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr style="display:none">
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
                    <tr style="display:none">
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
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20"></td>
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
