<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatientIncidentSafetyGoalsItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PatientIncidentSafetyGoalsItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumIncidentSafetyGoals" runat="server" ValidationGroup="IncidentSafetyGoals" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="IncidentSafetyGoals"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRSafetyGoals" runat="server" Text="Safety Goal"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRSafetyGoals" runat="server" Width="300px">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRSafetyGoals" runat="server" ErrorMessage="Safety Goals required."
                            ControlToValidate="cboSRSafetyGoals" SetFocusOnError="True" ValidationGroup="IncidentSafetyGoals"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRecommendation" runat="server" Text="Recommendation"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtRecommendation" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="IncidentSafetyGoals"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="IncidentSafetyGoals" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>
