<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RiskManagementItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.v2.RiskManagementItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumRiskManagementItem" runat="server" ValidationGroup="RiskManagementItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="RiskManagementItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<asp:HiddenField runat="server" ID="hdnSequenceNo" />
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRRiskManagementCategory" runat="server" Text="Category"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRRiskManagementCategory" runat="server" Width="300px">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRRiskManagementCategory" runat="server" ErrorMessage="Category required."
                            ControlToValidate="cboSRRiskManagementCategory" SetFocusOnError="True" ValidationGroup="RiskManagementItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRiskManagementDescription" runat="server" Text="Description"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtRiskManagementDescription" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" Height="70px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRiskManagementDescription" runat="server" ErrorMessage="Description required."
                            ControlToValidate="txtRiskManagementDescription" SetFocusOnError="True" ValidationGroup="RiskManagementItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>

            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRRiskManagementImpact" runat="server" Text="Impact"></asp:Label>
                    </td>
                    <td class="entry">
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="cboSRRiskManagementImpact" runat="server" Width="300px" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboSRRiskManagementImpact_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtImpactScore" runat="server" Width="50px" ReadOnly="true">
                                        <NumberFormat DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRRiskManagementImpact" runat="server" ErrorMessage="Impact required."
                            ControlToValidate="cboSRRiskManagementImpact" SetFocusOnError="True" ValidationGroup="RiskManagementItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRRiskManagementProbability" runat="server" Text="Probability"></asp:Label>
                    </td>
                    <td class="entry">
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="cboSRRiskManagementProbability" runat="server" Width="300px" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboSRRiskManagementProbability_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtProbabilityScore" runat="server" Width="50px" ReadOnly="true">
                                        <NumberFormat DecimalDigits="0" />
                                    </telerik:RadNumericTextBox></td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRRiskManagementProbability" runat="server" ErrorMessage="Probability required."
                            ControlToValidate="cboSRRiskManagementProbability" SetFocusOnError="True" ValidationGroup="RiskManagementItem"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRRiskManagementBands" runat="server" Text="Bands"></asp:Label>
                    </td>
                    <td class="entry">
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="cboSRRiskManagementBands" runat="server" Width="300px" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboSRRiskManagementBands_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtBandsColor" runat="server" Width="50px" ReadOnly="true" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRRiskManagementBands" runat="server" ErrorMessage="Bands required."
                            ControlToValidate="cboSRRiskManagementBands" SetFocusOnError="True" ValidationGroup="RiskManagementItem"
                            Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRRiskManagementControlling" runat="server" Text="Controlling"></asp:Label>
                    </td>
                    <td class="entry">
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="cboSRRiskManagementControlling" runat="server" Width="300px" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboSRRiskManagementControlling_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtControllingScore" runat="server" Width="50px" ReadOnly="true">
                                        <NumberFormat DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRRiskManagementControlling" runat="server" ErrorMessage="Controlling required."
                            ControlToValidate="cboSRRiskManagementControlling" SetFocusOnError="True" ValidationGroup="RiskManagementItem"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRiskRating" runat="server" Text="Risk Rating"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtRiskRating" runat="server" Width="100px">
                            <NumberFormat DecimalDigits="0" />
                        </telerik:RadNumericTextBox>

                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRiskRating" runat="server" ErrorMessage="Risk Rating required."
                            ControlToValidate="txtRiskRating" SetFocusOnError="True" ValidationGroup="RiskManagementItem"
                            Width="100%">
                            <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRiskScore" runat="server" Text="Risk Score"></asp:Label>
                    </td>
                    <td class="entry">
                        <asp:Button ID="btnRiskScore" runat="server" CommandName="Pick" CssClass="minimal" Width="80px" />
                        <telerik:RadNumericTextBox ID="txtRiskScore" runat="server" Width="50px" ReadOnly="true" Visible="false">
                            <NumberFormat DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblTotalScore" runat="server" Text="Total Score"></asp:Label>
                    </td>
                    <td class="entry">
                        <asp:Button ID="btnTotalScore" runat="server" CommandName="Pick" CssClass="minimal" Width="80px" />
                        <telerik:RadNumericTextBox ID="txtTotalScore" runat="server" Width="50px" ReadOnly="true" Visible="false">
                            <NumberFormat DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td width="33%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblReason" runat="server" Text="Reason"></asp:Label>
                    </td>
                    <td class="entry" colspan="3">
                        <telerik:RadTextBox ID="txtReason" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" Height="70px" />
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="RiskManagementItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="RiskManagementItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="33%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                    </td>
                    <td class="entry" colspan="3">
                        <telerik:RadTextBox ID="txtAction" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" Height="70px" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="34%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPic" runat="server" Text="Person In Charge"></asp:Label>
                    </td>
                    <td class="entry" colspan="3">
                        <telerik:RadTextBox ID="txtPic" runat="server" Width="300px" MaxLength="200" TextMode="MultiLine" Height="70px" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
