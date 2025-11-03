<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="RiskManagementDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.RiskManagementDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%" border="0">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientIncident" runat="server" Text="Incident No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientIncidentNo" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvPatientIncident" runat="server" ErrorMessage="Incident No required."
                                ValidationGroup="entry" ControlToValidate="txtPatientIncidentNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReportedBy" runat="server" Text="Reported By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReportedBy" runat="server" Width="300px" MaxLength="100"
                                ReadOnly="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label6" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitIDInCharge" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitInCharge" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="entry" ControlToValidate="cboServiceUnitIDInCharge" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%" border="0">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label7" runat="server" Text="Incident Date Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtIncidentDate" runat="server" Width="100px" OnSelectedDateChanged="txtIncidentDate_SelectedDateChanged"
                                            AutoPostBack="True">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="txtIncidentTime" runat="server" TimeView-Interval="00:30"
                                            TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                            TimeView-Columns="4" TimeView-StartTime="07:00" TimeView-EndTime="22:00" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvIncidentDate" runat="server" ErrorMessage="Incident date required."
                                ValidationGroup="entry" ControlToValidate="txtIncidentDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label8" runat="server" Text="Reporting Date Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtReportingDate" runat="server" Width="100px">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="txtReportingTime" runat="server" TimeView-Interval="00:30"
                                            TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                            TimeView-Columns="4" TimeView-StartTime="07:00" TimeView-EndTime="22:00" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvReportingDate" runat="server" ErrorMessage="Reporting date required."
                                ValidationGroup="entry" ControlToValidate="txtReportingDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Incident Detail Information" PageViewID="pgvDetail"
                Selected="true" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvDetail" runat="server">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%" border="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblIncidentName" runat="server" Text="Incident"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtIncidentName" runat="server" Width="100%" TextMode="MultiLine" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvIncidentName" runat="server" ErrorMessage="Insident required."
                                        ValidationGroup="entry" ControlToValidate="txtIncidentName" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblIncidentGroup" runat="server" Text="Incident Group"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRIncidentGroup" runat="server" Width="300px" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvSrIncidentGroup" runat="server" ErrorMessage="Incident group required."
                                        ValidationGroup="entry" ControlToValidate="cboSRIncidentGroup" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblClinicalImpact" runat="server" Text="Clinical Impact"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRClinicalImpact" runat="server" Width="300px" OnSelectedIndexChanged="cboSRClinicalImpact_SelectedIndexChanged"
                                        AutoPostBack="True" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvSrClinicalImpact" runat="server" ErrorMessage="Clinical impact required."
                                        ValidationGroup="entry" ControlToValidate="cboSRClinicalImpact" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblProbablyFrequency" runat="server" Text="Probability Frequency"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRProbabilityFrequency" runat="server" Width="300px"
                                        OnSelectedIndexChanged="cboSRProbabilityFrequency_SelectedIndexChanged" AutoPostBack="True" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSrFollowUp" runat="server" Text="Follow Up"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadComboBox ID="cboSRIncidentFollowUp" runat="server" Width="300px" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRiskGradingName" runat="server" Font-Italic="True" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="label">
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsApproved" Text="Approved" runat="server" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblHandling" runat="server" Text="Handling"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtHandling" runat="server" Width="100%" TextMode="MultiLine" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label9" runat="server" Text="Handled By"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRHandledBy" runat="server" Width="300px" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
