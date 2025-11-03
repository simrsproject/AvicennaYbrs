<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SafetyCultureIncidentReportsRecommendationItem.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.KEHRS.SafetyCultureIncidentReportsRecommendationItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumSafetyCultureIncidentReportsRecommendation" runat="server" ValidationGroup="SafetyCultureIncidentReportsRecommendation" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="SafetyCultureIncidentReportsRecommendation"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<asp:HiddenField runat="server" ID="hdnSequenceNo" />
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td width="50%" valign="Top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRecommendation" runat="server" Text="Recommendation" />
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtRecommendation" runat="server" Width="100%" TextMode="MultiLine" Height="250px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRecommendation" runat="server" ErrorMessage="Recommendation required."
                            ValidationGroup="SafetyCultureIncidentReportsRecommendation" ControlToValidate="txtRecommendation" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="SafetyCultureIncidentReportsRecommendation"
                            Visible='<%# !(DataItem is GridInsertionObject) %>' />
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="SafetyCultureIncidentReportsRecommendation" Visible='<%# DataItem is GridInsertionObject %>' />
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" valign="Top">
            
        </td>
    </tr>
</table>