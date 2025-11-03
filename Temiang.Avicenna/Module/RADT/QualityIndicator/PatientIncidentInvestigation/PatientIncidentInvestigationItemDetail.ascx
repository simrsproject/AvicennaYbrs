<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatientIncidentInvestigationItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PatientIncidentInvestigationItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumIncidentInvestigation" runat="server" ValidationGroup="IncidentInvestigation" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="IncidentInvestigation"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblSeqNo" runat="server" Text="Seq No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtSeqNo" runat="server" Width="100px" MaxLength="3" Enabled="false"
                            Text="" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSeqNo" runat="server" ErrorMessage="Seq No required."
                            ControlToValidate="txtSeqNo" SetFocusOnError="True" ValidationGroup="IncidentInvestigation"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRecomendation" runat="server" Text="Recomendation"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtRecomendation" runat="server" Width="300px" MaxLength="500"
                            TextMode="MultiLine" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRIncidentInvestigation" runat="server" ErrorMessage="Recomendation required."
                            ControlToValidate="txtRecomendation" SetFocusOnError="True" ValidationGroup="IncidentInvestigation"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRecomendationDateTime" runat="server" Text="Recomendation Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtRecomendationDateTime" runat="server" Width="100px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRecomendationDateTime" runat="server" ErrorMessage="Recomendation Date required."
                            ControlToValidate="txtRecomendationDateTime" SetFocusOnError="True" ValidationGroup="IncidentInvestigation"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPersonInCharge" runat="server" Text="Person In Charge"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtPersonInCharge" runat="server" Width="300px" MaxLength="50" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPersonInCharge" runat="server" ErrorMessage="Person In Charge required."
                            ControlToValidate="txtPersonInCharge" SetFocusOnError="True" ValidationGroup="IncidentInvestigation"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblImplementation" runat="server" Text="Implementation"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtImplementation" runat="server" Width="300px" MaxLength="500"
                            TextMode="MultiLine" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvImplementation" runat="server" ErrorMessage="Implementation required."
                            ControlToValidate="txtImplementation" SetFocusOnError="True" ValidationGroup="IncidentInvestigation"
                            Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblImplementationDateTime" runat="server" Text="Implementation Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtImplementationDateTime" runat="server" Width="100px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="frvImplementationDateTime" runat="server" ErrorMessage="Implementation Date required."
                            ControlToValidate="txtImplementationDateTime" SetFocusOnError="True" ValidationGroup="IncidentInvestigation"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="IncidentInvestigation"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="IncidentInvestigation" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
