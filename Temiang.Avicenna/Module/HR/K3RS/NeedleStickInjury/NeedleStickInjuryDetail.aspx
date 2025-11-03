<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="NeedleStickInjuryDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.K3RS.NeedleStickInjuryDetail" %>

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
                        <td></td>
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
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReferenceNo" runat="server" Text="Reference No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReferenceNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvReferenceNo" runat="server" ErrorMessage="Reference No required."
                                ValidationGroup="entry" ControlToValidate="txtReferenceNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td>
                            <asp:CheckBox ID="chkIsApproved" Text="Approved" runat="server" />
                            <asp:CheckBox ID="chkIsVoid" Text="Void" runat="server" />
                        </td>
                        <td></td>
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
                            <fieldset>
                                <legend><b>EXPOSED EMPLOYEES</b></legend>
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtEmployeeName" runat="server" Width="300px" MaxLength="150" ReadOnly="true" />
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvEmployeeName" runat="server" ErrorMessage="Employee Name required."
                                                ValidationGroup="entry" ControlToValidate="txtEmployeeName" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image25" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblEmployeeNumber" runat="server" Text="Employee Number"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtEmployeeNumber" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblDateOfBirth" runat="server" Text="Date Of Birth / Sex"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="100px" DateInput-ReadOnly="true"
                                                            DatePopupButton-Enabled="false">
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                    <td>&nbsp;/&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtSex" runat="server" Width="42px" ReadOnly="True" />
                                                    </td>
                                                </tr>
                                            </table>

                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtAgeInYear" runat="server" Width="40px" ReadOnly="true">
                                                            <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>&nbsp;Y&nbsp;&nbsp;</td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtAgeInMonth" runat="server" Width="40px" ReadOnly="true">
                                                            <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>&nbsp;M&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtAgeInDay" runat="server" Width="40px" ReadOnly="true">
                                                            <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>&nbsp;D&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblEmployeeType" runat="server" Text="Employee Type"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtEmployeeType" runat="server" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPositionID" runat="server" Text="Position"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtPositionName" runat="server" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            <fieldset>
                                <legend><b>PATIENT (SOURCE OF EXPOSURE)</b></legend>
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPatientID" runat="server" Text="Patient Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboPatientID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboPatientID_ItemDataBound"
                                                OnItemsRequested="cboPatientID_ItemsRequested" OnSelectedIndexChanged="cboPatientID_SelectedIndexChanged">
                                                <ItemTemplate>
                                                    <b>
                                                        <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                                    </b>&nbsp;-&nbsp;
                                                    <%# System.Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth")).ToString(Temiang.Avicenna.Common.AppConstant.DisplayFormat.Date)%>
                                                    <br />
                                                    <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %>
                                                    &nbsp;|&nbsp;
                                                    <%# DataBinder.Eval(Container.DataItem, "PatientID") %>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Note : Show max 10 items
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvPatientID" runat="server" ErrorMessage="Patient Name required."
                                                ValidationGroup="entry" ControlToValidate="cboPatientID" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboRegistrationNo" runat="server" Width="300px" EnableLoadOnDemand="true"
                                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboRegistrationNo_ItemsRequested"
                                                OnItemDataBound="cboRegistrationNo_ItemDataBound">
                                                <ItemTemplate>
                                                    <b>
                                                        <%# DataBinder.Eval(Container.DataItem, "RegistrationNo")%>
                                                    </b>
                                                    <br />
                                                    <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                                                    <br />
                                                    <%# DataBinder.Eval(Container.DataItem, "RoomName")%>
                                                    &nbsp;-&nbsp;
                                                    <%# DataBinder.Eval(Container.DataItem, "BedID")%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Note : Show max 5 items
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblDiagnose" runat="server" Text="Diagnosis"></asp:Label>
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
                                        <td></td>
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
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPatientImunizationHistory" runat="server" Text="Imunization History"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtPatientImunizationHistory" runat="server" Width="300px"
                                                MaxLength="250" TextMode="MultiLine" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
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
                                        <td></td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            <fieldset>
                                <legend><b>FOLLOW UP BY PPI TEAM</b></legend>
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
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFollowUp" runat="server" Text="Follow Up Notes"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtFollowUpNotes" runat="server" Width="300px" MaxLength="500"
                                                TextMode="MultiLine" Height="68px" />
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvFollowUp" runat="server" ErrorMessage="Follow Up Notes required."
                                                ValidationGroup="entry" ControlToValidate="txtFollowUpNotes" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
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
                                        <td></td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td>
                            <fieldset>
                                <legend><b>CASE IDENTIFICATION</b></legend>
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblIncidentDate" runat="server" Text="Incident Date / Time"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtIncidentDate" runat="server" Width="100px" Enabled="false">
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTimePicker ID="txtIncidentTime" runat="server" Width="80px" Enabled="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="Label8" runat="server" Text="Reporting Date / Time"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtReportingDate" runat="server" Width="100px" Enabled="false">
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTimePicker ID="txtReportingTime" runat="server" Width="80px" Enabled="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSREmployeeIncidentType" runat="server" Text="Incident Type"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSREmployeeIncidentType" runat="server" Width="300px" Enabled="false" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSRNeedleType" runat="server" Text="Needle Type"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSRNeedleType" runat="server" Width="300px" Enabled="false" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSREmployeeInjuryCategory" runat="server" Text="Injury Category"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSREmployeeInjuryCategory" runat="server" Width="300px" Enabled="false" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblLossTime" runat="server" Text="Loss Time"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtLossTime" runat="server" Width="50px" ReadOnly="true">
                                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                            </telerik:RadNumericTextBox>
                                            Day(s)
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblInjuredLocation" runat="server" Text="Injury Location"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtInjuredLocation" runat="server" Width="300px" MaxLength="100" ReadOnly="true" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                </table>
                                <hr />
                                <table>
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
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblExposedArea" runat="server" Text="Exposed Areas"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtExposedArea" runat="server" Width="300px" MaxLength="150" />
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvExposedArea" runat="server" ErrorMessage="Exposed Areas required."
                                                ValidationGroup="entry" ControlToValidate="txtExposedArea" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblReason" runat="server" Text="Reason"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtReason" runat="server" Width="300px" MaxLength="250" />
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvReason" runat="server" ErrorMessage="Reason required."
                                                ValidationGroup="entry" ControlToValidate="txtReason" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
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
                                        <td width="20px"></td>
                                        <td></td>
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
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblOfficerImunizationHistory" runat="server" Text="Imunization History"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtOfficerImunizationHistory" runat="server" Width="300px"
                                                MaxLength="250" TextMode="MultiLine" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>

                                    <tr>
                                        <td class="label"></td>
                                        <td class="entry">
                                            <asp:CheckBox ID="chkIsSpo" Text="Already working according to standard operational (SPO)" runat="server" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label"></td>
                                        <td class="entry">
                                            <asp:CheckBox ID="chkIsUsingApd" Text="Wear Personal Protective Equipment (APD)" runat="server" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
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
                                            <asp:RequiredFieldValidator ID="rfvRecomendation" runat="server" ErrorMessage="Recomendation required."
                                                ValidationGroup="entry" ControlToValidate="txtRecomendation" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>
</asp:Content>
