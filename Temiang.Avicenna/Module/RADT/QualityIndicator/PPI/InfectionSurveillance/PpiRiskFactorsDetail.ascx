<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PpiRiskFactorsDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PpiRiskFactorsDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemBalance" runat="server" ValidationGroup="PpiRiskFactors" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PpiRiskFactors"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td style="width: 50%;vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRRiskFactorsType" runat="server" Text="Risk Factors Type" />
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRRiskFactorsType" runat="server" Width="304px" AllowCustomText="true"
                            Filter="Contains" OnSelectedIndexChanged="cboSRRiskFactorsType_SelectedIndexChanged"
                            AutoPostBack="True" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRRiskFactorsType" runat="server" ErrorMessage="Risk Factors Type required."
                            ControlToValidate="cboSRRiskFactorsType" SetFocusOnError="True" ValidationGroup="PpiRiskFactors"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRiskFactorsID" runat="server" Text="Risk Factors" />
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboRiskFactorsID" runat="server" Width="304px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRiskFactorsID" runat="server" ErrorMessage="Risk Factors required."
                            ControlToValidate="cboRiskFactorsID" SetFocusOnError="True" ValidationGroup="PpiRiskFactors"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRRiskFactorsLocation" runat="server" Text="Location" />
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRRiskFactorsLocation" runat="server" Width="304px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRRiskFactorsLocation" runat="server" ErrorMessage="Location required."
                            ControlToValidate="cboSRRiskFactorsLocation" SetFocusOnError="True" ValidationGroup="PpiRiskFactors"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PpiRiskFactors"
                            Visible='<%# !(DataItem is GridInsertionObject) %>' />
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="PpiRiskFactors" Visible='<%# DataItem is GridInsertionObject %>' />
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel" />
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%;vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDateOfInitialInstallation" runat="server" Text="Date Started" />
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtDateOfInitialInstallation" runat="server" Width="100px"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvDateOfInitialInstallation" runat="server" ErrorMessage="Date Started required."
                            ControlToValidate="txtDateOfInitialInstallation" SetFocusOnError="True" ValidationGroup="PpiRiskFactors"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDateOfFinalInstallation" runat="server" Text="Date Finished" />
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtDateOfFinalInstallation" runat="server" Width="100px"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvDateOfFinalInstallation" runat="server" ErrorMessage="Date Finished required."
                            ControlToValidate="txtDateOfFinalInstallation" SetFocusOnError="True" ValidationGroup="PpiRiskFactors"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
            </table>
        </td>
    </tr>
</table>