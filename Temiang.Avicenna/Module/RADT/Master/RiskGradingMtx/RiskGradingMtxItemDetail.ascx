<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RiskGradingMtxItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.RiskGradingMtxItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumRiskGradingMtx" runat="server" ValidationGroup="RiskGradingMtx" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="RiskGradingMtx"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRIncidentProbabilityFrequency" runat="server" Text="Incident Probability Frequency"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRIncidentProbabilityFrequency" runat="server" Width="300px">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRIncidentProbabilityFrequency" runat="server"
                            ErrorMessage="Incident Probability Frequency required." ControlToValidate="cboSRIncidentProbabilityFrequency"
                            SetFocusOnError="True" ValidationGroup="RiskGradingMtx" Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRIncidentFollowUp" runat="server" Text="Incident Follow Up"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRIncidentFollowUp" runat="server" Width="300px">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRIncidentFollowUp" runat="server" ErrorMessage="Incident Follow Up required."
                            ControlToValidate="cboSRIncidentFollowUp" SetFocusOnError="True" ValidationGroup="RiskGradingMtx"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRiskGradingID" runat="server" Text="Risk Grading"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboRiskGradingID" runat="server" Width="300px">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvcboRiskGradingID" runat="server" ErrorMessage="Risk Grading required."
                            ControlToValidate="cboRiskGradingID" SetFocusOnError="True" ValidationGroup="RiskGradingMtx"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="RiskGradingMtx"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="RiskGradingMtx" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
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
