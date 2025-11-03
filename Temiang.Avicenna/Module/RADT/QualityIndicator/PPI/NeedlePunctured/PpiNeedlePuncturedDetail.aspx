<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="PpiNeedlePuncturedDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PpiNeedlePuncturedDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTransactionNo" runat="server" ErrorMessage="Transaction No required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" Enabled="False" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label">
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsApproved" Text="Approved" runat="server" />
                            <asp:CheckBox ID="chkIsVoid" Text="Void" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td>
                            &nbsp;<b>I. Officer Are Exposed</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblOfficerName" runat="server" Text="Officer Name"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtOfficerName" runat="server" Width="300px" MaxLength="150" />
                                    </td>
                                    <td width="20px">
                                        <asp:RequiredFieldValidator ID="rfvOfficerName" runat="server" ErrorMessage="Officer Name required."
                                            ValidationGroup="entry" ControlToValidate="txtOfficerName" SetFocusOnError="True"
                                            Width="100%">
                                            <asp:Image ID="Image25" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblDatePunctured" runat="server" Text="Incident Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDatePicker ID="txtDatePunctured" runat="server" Width="100px" />
                                    </td>
                                    <td width="20">
                                        <asp:RequiredFieldValidator ID="rfvDatePunctured" runat="server" ErrorMessage="Incident Date required."
                                            ValidationGroup="entry" ControlToValidate="txtDatePunctured" SetFocusOnError="True"
                                            Width="100%">
                                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPuncturedAreas" runat="server" Text="Exposed Areas"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtPuncturedAreas" runat="server" Width="300px" MaxLength="150" />
                                    </td>
                                    <td width="20px">
                                        <asp:RequiredFieldValidator ID="rfvPuncturedAreas" runat="server" ErrorMessage="Exposed Areas required."
                                            ValidationGroup="entry" ControlToValidate="txtPuncturedAreas" SetFocusOnError="True"
                                            Width="100%">
                                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblCausePunctured" runat="server" Text="Reason"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtCausePunctured" runat="server" Width="300px" MaxLength="250" />
                                    </td>
                                    <td width="20px">
                                        <asp:RequiredFieldValidator ID="rfvCausePunctured" runat="server" ErrorMessage="Reason required."
                                            ValidationGroup="entry" ControlToValidate="txtCausePunctured" SetFocusOnError="True"
                                            Width="100%">
                                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSourceOfExposure" runat="server" Text="Source Of Exposure"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkIsBlood" Text="Blood" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkIsFluidSperm" Text="Fluid Sperm" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkIsVaginalSecretions" Text="Vaginal Secretions" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkIsCerebrospinal" Text="Cerebrospinal" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkIsUrine" Text="Urine" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkIsFaeces" Text="Faeces" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblOfficerHistoryOfDisease" runat="server" Text="History Of Disease"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkIsOfficerHiv" Text="HIV" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkIsOfficerHbv" Text="HBV" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkIsOfficerHcv" Text="HCV" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblOfficerImunizationHistory" runat="server" Text="Imunization History"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtOfficerImunizationHistory" runat="server" Width="300px"
                                            MaxLength="250" />
                                    </td>
                                    <td width="20px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblChronology" runat="server" Text="Chronology"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtChronology" runat="server" Width="300px" MaxLength="500"
                                            TextMode="MultiLine" />
                                    </td>
                                    <td width="20px">
                                        <asp:RequiredFieldValidator ID="rfvChronology" runat="server" ErrorMessage="Chronology required."
                                            ValidationGroup="entry" ControlToValidate="txtChronology" SetFocusOnError="True"
                                            Width="100%">
                                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td>
                            &nbsp;<b>II. Patient (Source Of Exposure)</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="250" />
                                    </td>
                                    <td width="20px">
                                        <asp:RequiredFieldValidator ID="rfvPatientName" runat="server" ErrorMessage="Patient Name required."
                                            ValidationGroup="entry" ControlToValidate="txtPatientName" SetFocusOnError="True"
                                            Width="100%">
                                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" MaxLength="250" />
                                    </td>
                                    <td width="20px">
                                        <asp:RequiredFieldValidator ID="rfvMedicalNo" runat="server" ErrorMessage="Medical No required."
                                            ValidationGroup="entry" ControlToValidate="txtMedicalNo" SetFocusOnError="True"
                                            Width="100%">
                                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblDiagnose" runat="server" Text="Diagnose"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtDiagnose" runat="server" Width="300px" MaxLength="250" />
                                    </td>
                                    <td width="20px">
                                        <asp:RequiredFieldValidator ID="rfvDiagnose" runat="server" ErrorMessage="Diagnose required."
                                            ValidationGroup="entry" ControlToValidate="txtDiagnose" SetFocusOnError="True"
                                            Width="100%">
                                            <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPatientHistoryOfDisease" runat="server" Text="History Of Disease"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkIsPatientHiv" Text="HIV" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkIsPatientHbv" Text="HBV" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkIsPatientHcv" Text="HCV" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPatientImunizationHistory" runat="server" Text="Imunization History"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtPatientImunizationHistory" runat="server" Width="300px"
                                            MaxLength="250" />
                                    </td>
                                    <td width="20px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblKnownBy" runat="server" Text="Known By"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtKnownBy" runat="server" Width="300px" MaxLength="150" />
                                    </td>
                                    <td width="20px">
                                        <asp:RequiredFieldValidator ID="rfvKnownBy" runat="server" ErrorMessage="Known By required."
                                            ValidationGroup="entry" ControlToValidate="txtKnownBy" SetFocusOnError="True"
                                            Width="100%">
                                            <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            &nbsp;<b>III. Follow Up By PPI Team</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblFollowUpDate" runat="server" Text="Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDatePicker ID="txtFollowUpDate" runat="server" Width="100px" />
                                    </td>
                                    <td width="20">
                                        <asp:RequiredFieldValidator ID="rfvFollowUpDate" runat="server" ErrorMessage="Follow Up Date required."
                                            ValidationGroup="entry" ControlToValidate="txtFollowUpDate" SetFocusOnError="True"
                                            Width="100%">
                                            <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblFollowUp" runat="server" Text="Follow Up"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtFollowUp" runat="server" Width="300px" MaxLength="500"
                                            TextMode="MultiLine" />
                                    </td>
                                    <td width="20px">
                                        <asp:RequiredFieldValidator ID="rfvFollowUp" runat="server" ErrorMessage="Follow Up required."
                                            ValidationGroup="entry" ControlToValidate="txtFollowUp" SetFocusOnError="True"
                                            Width="100%">
                                            <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblFollowUpBy" runat="server" Text="Follow Up By"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtFollowUpBy" runat="server" Width="300px" MaxLength="150" />
                                    </td>
                                    <td width="20px">
                                        <asp:RequiredFieldValidator ID="rfvFollowUpBy" runat="server" ErrorMessage="Follow Up By required."
                                            ValidationGroup="entry" ControlToValidate="txtFollowUpBy" SetFocusOnError="True"
                                            Width="100%">
                                            <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
