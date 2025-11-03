<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="TransfusionMonitoringDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.BloodBank.TransfusionMonitoringDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No / Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="140px" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtTransctionDate" runat="server" Width="100px" Enabled="False">
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTransactionNo" runat="server" ErrorMessage="Transaction No required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransctionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / MRN"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="190px" MaxLength="20"
                                            ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="103px" MaxLength="15"
                                            ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="28px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="243px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="23px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAgeYear" runat="server" ReadOnly="true" Width="40px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;Y&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeMonth" runat="server" ReadOnly="true" Width="40px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;M&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeDay" runat="server" ReadOnly="true" Width="40px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;D
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParamedicID" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblParamedicName" runat="server"></asp:Label>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="20"
                                ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblServiceUnitName" runat="server"></asp:Label>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRoomID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblRoomName" runat="server"></asp:Label>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBedID" runat="server" Text="Bed No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBedID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtGuarantorID" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblGuarantorName" runat="server"></asp:Label>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDiagnose" runat="server" Text="Diagnosis"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtDiagnose" runat="server" Width="300px" MaxLength="500"
                                ReadOnly="True" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReason" runat="server" Text="Reason"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReason" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine"
                                ReadOnly="True" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Blood Information & Transfusion" PageViewID="pvInfo"
                Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Re-Check" PageViewID="pgRecheck">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Vital Signs" PageViewID="pvVitalSigns">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Reactions" PageViewID="pvReactions">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Action" PageViewID="pgAction">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pvInfo" runat="server" Selected="True">
            <table style="width: 100%" cellpadding="1" cellspacing="1">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblBagNo" runat="server" Text="Bag No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtBagNo" runat="server" Width="300px" ReadOnly="True" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblBloodType" runat="server" Text="Blood Type / Rhesus"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadComboBox runat="server" ID="cboSRBloodType" Width="105px" AllowCustomText="true"
                                                    Filter="Contains" Enabled="False">
                                                </telerik:RadComboBox>
                                            </td>
                                            <td style="width: 5px"></td>
                                            <td>
                                                <asp:RadioButtonList ID="rblBloodRhesus" runat="server" RepeatDirection="Horizontal"
                                                    RepeatLayout="Flow" Enabled="False">
                                                    <asp:ListItem Value="0" Text="+" />
                                                    <asp:ListItem Value="1" Text="-" />
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRBloodGroupReceived" runat="server" Text="Blood Group"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSRBloodGroupReceived" Width="300px" AllowCustomText="true"
                                        Filter="Contains" Enabled="False">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRBloodSource" runat="server" Text="Blood Source"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSRBloodSource" Width="300px" AllowCustomText="true"
                                        Filter="Contains" Enabled="False">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRBloodSourceFrom" runat="server" Text="Blood Source From"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSRBloodSourceFrom" Width="300px" AllowCustomText="true"
                                        Filter="Contains" Enabled="False">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblVolumeBag" runat="server" Text="Volume"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtVolumeBag" runat="server" Width="100px" NumberFormat-DecimalDigits="0" ReadOnly="true" />
                                            </td>
                                            <td style="width: 5px"></td>
                                            <td>ML/CC
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblExpiredDateTime" runat="server" Text="Expired Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDateTimePicker ID="txtExpiredDateDateTime" runat="server" AutoPostBackControl="None"
                                        Enabled="False">
                                        <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm" DateFormat="dd/MM/yyyy HH:mm">
                                        </DateInput>
                                        <TimeView ID="TimeView2" runat="server" TimeFormat="HH:mm">
                                        </TimeView>
                                    </telerik:RadDateTimePicker>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblBloodBagTemperature" runat="server" Text="Blood Bag Temperature"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtBloodBagTemperature" runat="server" Width="100px"
                                                    NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                            </td>
                                            <td style="width: 5px"></td>
                                            <td>°C
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblIsCrossMatchingSuitability" runat="server" Text="Cross Matching Suitability"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="width: 35%">
                                                <asp:RadioButtonList ID="rblIsCrossMatchingSuitability" runat="server" RepeatDirection="Vertical"
                                                    RepeatLayout="Flow" Enabled="False">
                                                    <asp:ListItem Value="1" Text="Compatible" />
                                                    <asp:ListItem Value="0" Text="In-Compatible" />
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td class="label" style="width: 40%">
                                                            <asp:Label ID="Label27" runat="server" Text="Major"></asp:Label>
                                                        </td>
                                                        <td class="entry">
                                                            <asp:RadioButtonList ID="rblCrossmatchCompatibleMajor" runat="server" RepeatDirection="Horizontal"
                                                                RepeatLayout="Flow" Enabled="False">
                                                                <asp:ListItem Value="+" Text="+" />
                                                                <asp:ListItem Value="-" Text="-" />
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label" style="width: 40%">
                                                            <asp:Label ID="Label32" runat="server" Text="Minor"></asp:Label>
                                                        </td>
                                                        <td class="entry">
                                                            <asp:RadioButtonList ID="rblCrossmatchCompatibleMinor" runat="server" RepeatDirection="Horizontal"
                                                                RepeatLayout="Flow" Enabled="False">
                                                                <asp:ListItem Value="+" Text="+" />
                                                                <asp:ListItem Value="-" Text="-" />
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td>
                                                            <telerik:RadNumericTextBox ID="txtCrossmatchCompatibleMinorLevel" runat="server"
                                                                Width="50px" NumberFormat-DecimalDigits="0" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label" style="width: 40%">
                                                            <asp:Label ID="Label33" runat="server" Text="Auto Control"></asp:Label>
                                                        </td>
                                                        <td class="entry">
                                                            <asp:RadioButtonList ID="rblCrossmatchCompatibleAutoControl" runat="server" RepeatDirection="Horizontal"
                                                                RepeatLayout="Flow" Enabled="False">
                                                                <asp:ListItem Value="+" Text="+" />
                                                                <asp:ListItem Value="-" Text="-" />
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td>
                                                            <telerik:RadNumericTextBox ID="txtCrossmatchCompatibleAutoControlLevel" runat="server"
                                                                Width="50px" NumberFormat-DecimalDigits="0" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 250px">
                                    <asp:Label ID="lblIsScreeningLabelPassedPmi" runat="server" Text="Screening Label Passed (PMI)"></asp:Label>
                                </td>
                                <td class="entry">
                                    <asp:RadioButtonList ID="rblIsScreeningLabelPassedPmi" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" Enabled="False">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblIsBloodTypeCompatibility" runat="server" Text="Blood Type Compatibility"></asp:Label>
                                </td>
                                <td class="entry">
                                    <asp:RadioButtonList ID="rblIsBloodTypeCompatibility" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" Enabled="False">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblIsLabelDonorsMatchesWithPatientsForm" runat="server" Text="Label Donors Matches With Patients Form"></asp:Label>
                                </td>
                                <td class="entry">
                                    <asp:RadioButtonList ID="rblIsLabelDonorsMatchesWithPatientsForm" runat="server"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="False">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblIsScreeningLabelPassedBd" runat="server" Text="Screening Label Passed (Blood Bank)"></asp:Label>
                                </td>
                                <td class="entry">
                                    <asp:RadioButtonList ID="rblIsScreeningLabelPassedBd" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" Enabled="False">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgRecheck" runat="server">
            <table style="width: 100%; display: none" cellpadding="1" cellspacing="1">
                <tr>
                    <td>
                        <fieldset>
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 50%; vertical-align: top">
                                        <table width="100%">
                                            <tr>
                                                <td class="label" style="width: 25%">
                                                    <asp:Label ID="lblDidNotPassTheScreeningTest" runat="server" Text="Did not pass the screening test :"
                                                        ForeColor="Red"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkIsHiv" runat="server" Text="HIV" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkIsVbrl" runat="server" Text="VDRL" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkIsHbsAg" runat="server" Text="HBS Ag" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkIsHcv" runat="server" Text="HCV" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 50%; vertical-align: top">
                                        <table width="100%">
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label"></td>
                                <td class="entry">
                                    <asp:CheckBox ID="chkIsReCheck" runat="server" Text="Re-Check" AutoPostBack="True"
                                        OnCheckedChanged="chkIsReCheck_CheckedChanged" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblReCheckDateTime" runat="server" Text="Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDateTimePicker ID="txtReCheckDateTime" runat="server" AutoPostBackControl="None">
                                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm" DateFormat="dd/MM/yyyy HH:mm">
                                        </DateInput>
                                        <TimeView ID="TimeView1" runat="server" TimeFormat="HH:mm">
                                        </TimeView>
                                    </telerik:RadDateTimePicker>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblIsReCheckExpiredDate" runat="server" Text="Expired Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <asp:RadioButtonList ID="rblIsReCheckExpiredDate" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvIsReCheckExpiredDate" runat="server" ErrorMessage="Expired Date required."
                                        ControlToValidate="rblIsReCheckExpiredDate" SetFocusOnError="True" ValidationGroup="entry"
                                        Width="100%">
                                        <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblIsReCheckLeak" runat="server" Text="Leak"></asp:Label>
                                </td>
                                <td class="entry">
                                    <asp:RadioButtonList ID="rblIsReCheckLeak" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvIsReCheckLeak" runat="server" ErrorMessage="Leak required."
                                        ControlToValidate="rblIsReCheckLeak" SetFocusOnError="True" ValidationGroup="entry"
                                        Width="100%">
                                        <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblIsHemolysis" runat="server" Text="Hemolysis"></asp:Label>
                                </td>
                                <td class="entry">
                                    <asp:RadioButtonList ID="rblIsReCheckHemolysis" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvIsReCheckHemolysis" runat="server" ErrorMessage="Hemolysis required."
                                        ControlToValidate="rblIsReCheckHemolysis" SetFocusOnError="True" ValidationGroup="entry"
                                        Width="100%">
                                        <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblIsReCheckClotting" runat="server" Text="Clotting"></asp:Label>
                                </td>
                                <td class="entry">
                                    <asp:RadioButtonList ID="rblIsReCheckClotting" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvIsReCheckClotting" runat="server" ErrorMessage="Clotting required."
                                        ControlToValidate="rblIsReCheckClotting" SetFocusOnError="True" ValidationGroup="entry"
                                        Width="100%">
                                        <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblIsReCheckBloodTypeCompatibility" runat="server" Text="Blood Type Compatibility"></asp:Label>
                                </td>
                                <td class="entry">
                                    <asp:RadioButtonList ID="rblIsReCheckBloodTypeCompatibility" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvIsReCheckBloodTypeCompatibility" runat="server"
                                        ErrorMessage="Blood Type Compatibility required." ControlToValidate="rblIsReCheckBloodTypeCompatibility"
                                        SetFocusOnError="True" ValidationGroup="entry" Width="100%">
                                        <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblReCheckOfficer" runat="server" Text="Checked by #1"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtReCheckOfficer" runat="server" Width="100%" MaxLength="150" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvReCheckOfficer" runat="server"
                                        ErrorMessage="Checked by #1 required." ControlToValidate="txtReCheckOfficer"
                                        SetFocusOnError="True" ValidationGroup="entry" Width="100%">
                                        <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblReCheckOfficer2" runat="server" Text="Checked by #2"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtReCheckOfficer2" runat="server" Width="100%" MaxLength="150" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtNotes" runat="server" Width="100%" MaxLength="500" TextMode="MultiLine" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pvVitalSigns" runat="server">
            <table style="width: 100%" cellpadding="1" cellspacing="1">
                <tr>
                    <td style="width: 20%">
                        <table width="100%">
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionVitalSigns0" runat="server" Text="Pre Transfusion"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionVitalSigns1" runat="server" Text="15'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionVitalSigns2" runat="server" Text="30'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionVitalSigns10" runat="server" Text="30'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionVitalSigns11" runat="server" Text="30'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionVitalSigns12" runat="server" Text="30'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionVitalSigns13" runat="server" Text="30'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionVitalSigns3" runat="server" Text="60'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionVitalSigns4" runat="server" Text="120'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionVitalSigns5" runat="server" Text="180'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionVitalSigns6" runat="server" Text="240'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionVitalSigns7" runat="server" Text="Post Transfusion (1st 8 hours)"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionVitalSigns8" runat="server" Text="Post Transfusion (2nd 8 hours)"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionVitalSigns9" runat="server" Text="Post Transfusion (3rd 8 hours)"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionVitalSigns14" runat="server" Text="Post Transfusion"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblBp" runat="server" Text="Blood Presure"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtBpPre" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtBp10" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtBp30" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtBp60" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtBp120" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtBp180" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtBp240" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtBpPost" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtBpPost2" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtBpPost3" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtBp31" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtBp32" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtBp33" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtBp34" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtBpPost4" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label2" runat="server" Text="Pulse"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPulsePre" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPulse10" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPulse30" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPulse60" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPulse120" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPulse180" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPulse240" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPulsePost" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPulsePost2" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPulsePost3" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPulse31" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPulse32" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPulse33" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPulse34" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPulsePost4" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label3" runat="server" Text="Temperature"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTemperaturePre" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTemperature10" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTemperature30" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTemperature60" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTemperature120" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTemperature180" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTemperature240" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTemperaturePost" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTemperaturePost2" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTemperaturePost3" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTemperature31" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTemperature32" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTemperature33" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTemperature34" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTemperaturePost4" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label11" runat="server" Text="Respiratory"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtRespiratoryPre" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtRespiratory10" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtRespiratory30" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtRespiratory60" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtRespiratory120" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtRespiratory180" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtRespiratory240" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtRespiratoryPost" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtRespiratoryPost2" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtRespiratoryPost3" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtRespiratory31" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtRespiratory32" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtRespiratory33" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtRespiratory34" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtRespiratoryPost4" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pvReactions" runat="server">
            <table style="width: 100%" cellpadding="1" cellspacing="1">
                <tr>
                    <td style="width: 20%">
                        <table width="100%">
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionReaction0" runat="server" Text="Pre Transfusion"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionReaction1" runat="server" Text="15'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionReaction2" runat="server" Text="30'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionReaction3" runat="server" Text="60'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionReaction4" runat="server" Text="120'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionReaction5" runat="server" Text="180'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionReaction6" runat="server" Text="240'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionReaction7" runat="server" Text="Post Transfusion (1st 8 hours)"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionReaction8" runat="server" Text="Post Transfusion (2nd 8 hours)"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionReaction9" runat="server" Text="Post Transfusion (3rd 8 hours)"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionReaction10" runat="server" Text="30'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionReaction11" runat="server" Text="30'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionReaction12" runat="server" Text="30'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionReaction13" runat="server" Text="30'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionReaction14" runat="server" Text="Post Transfusion"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFever" runat="server" Text="Fever"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsFeverPre" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsFever10" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsFever30" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsFever60" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsFever120" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsFever180" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsFever240" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsFeverPost" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsFeverPost2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsFeverPost3" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsFeverPost4" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsFeverPost5" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsFeverPost6" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsFeverPost7" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsFeverPost8" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblShivering" runat="server" Text="Shivering"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShiveringPre" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShivering10" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShivering30" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShivering60" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShivering120" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShivering180" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShivering240" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShiveringPost" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShiveringPost2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShiveringPost3" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShiveringPost4" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShiveringPost5" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShiveringPost6" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShiveringPost7" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShiveringPost8" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSob" runat="server" Text="Short Of Breath"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsSobPre" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsSob10" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsSob30" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsSob60" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsSob120" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsSob180" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsSob240" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsSobPost" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsSobPost2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsSobPost3" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsSobPost4" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsSobPost5" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsSobPost6" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsSobPost7" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsSobPost8" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPainful" runat="server" Text="Painful"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPainfulPre" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPainful10" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPainful30" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPainful60" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPainful120" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPainful180" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPainful240" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPainfulPost" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPainfulPost2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPainfulPost3" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPainfulPost4" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPainfulPost5" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPainfulPost6" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPainfulPost7" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPainfulPost8" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblNausea" runat="server" Text="Nausea"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsNauseaPre" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsNausea10" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsNausea30" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsNausea60" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsNausea120" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsNausea180" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsNausea240" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsNauseaPost" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsNauseaPost2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsNauseaPost3" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsNauseaPost4" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsNauseaPost5" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsNauseaPost6" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsNauseaPost7" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsNauseaPost8" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblBleeding" runat="server" Text="Bleeding"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsBleedingPre" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsBleeding10" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsBleeding30" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsBleeding60" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsBleeding120" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsBleeding180" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsBleeding240" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsBleedingPost" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsBleedingPost2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsBleedingPost3" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsBleedingPost4" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsBleedingPost5" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsBleedingPost6" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsBleedingPost7" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsBleedingPost8" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblHypotension" runat="server" Text="Hypotension"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHypotensionPre" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHypotension10" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHypotension30" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHypotension60" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHypotension120" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHypotension180" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHypotension240" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHypotensionPost" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHypotensionPost2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHypotensionPost3" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHypotensionPost4" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHypotensionPost5" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHypotensionPost6" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHypotensionPost7" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHypotensionPost8" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblShock" runat="server" Text="Shock"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShockPre" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShock10" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShock30" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShock60" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShock120" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShock180" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShock240" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShockPost" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShockPost2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShockPost3" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShockPost4" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShockPost5" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShockPost6" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShockPost7" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsShockPost8" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblUrticaria" runat="server" Text="Urticaria"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsUrticariaPre" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsUrticaria10" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsUrticaria30" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsUrticaria60" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsUrticaria120" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsUrticaria180" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsUrticaria240" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsUrticariaPost" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsUrticariaPost2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsUrticariaPost3" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsUrticariaPost4" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsUrticariaPost5" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsUrticariaPost6" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsUrticariaPost7" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsUrticariaPost8" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblRash" runat="server" Text="Rash"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRashPre" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRash10" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRash30" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRash60" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRash120" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRash180" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRash240" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRashPost" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRashPost2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRashPost3" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRashPost4" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRashPost5" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRashPost6" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRashPost7" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRashPost8" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPruritus" runat="server" Text="Pruritus"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPruritusPre" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPruritus10" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPruritus30" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPruritus60" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPruritus120" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPruritus180" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPruritus240" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPruritusPost" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPruritusPost2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPruritusPost3" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPruritusPost4" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPruritusPost5" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPruritusPost6" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPruritusPost7" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPruritusPost8" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAnxious" runat="server" Text="Anxious"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsAnxiousPre" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsAnxious10" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsAnxious30" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsAnxious60" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsAnxious120" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsAnxious180" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsAnxious240" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsAnxiousPost" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsAnxiousPost2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsAnxiousPost3" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsAnxiousPost4" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsAnxiousPost5" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsAnxiousPost6" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsAnxiousPost7" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsAnxiousPost8" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblWeak" runat="server" Text="Weak"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsWeakPre" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsWeak10" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsWeak30" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsWeak60" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsWeak120" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsWeak180" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsWeak240" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsWeakPost" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsWeakPost2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsWeakPost3" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsWeakPost4" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsWeakPost5" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsWeakPost6" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsWeakPost7" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsWeakPost8" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPalpitations" runat="server" Text="Palpitations"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPalpitationsPre" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPalpitations10" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPalpitations30" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPalpitations60" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPalpitations120" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPalpitations180" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPalpitations240" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPalpitationsPost" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPalpitationsPost2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPalpitationsPost3" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPalpitationsPost4" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPalpitationsPost5" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPalpitationsPost6" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPalpitationsPost7" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsPalpitationsPost8" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblMildDyspnea" runat="server" Text="Mild Dyspnea"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMildDyspneaPre" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMildDyspnea10" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMildDyspnea30" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMildDyspnea60" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMildDyspnea120" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMildDyspnea180" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMildDyspnea240" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMildDyspneaPost" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMildDyspneaPost2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMildDyspneaPost3" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMildDyspneaPost4" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMildDyspneaPost5" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMildDyspneaPost6" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMildDyspneaPost7" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMildDyspneaPost8" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblHeadache" runat="server" Text="Headache"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHeadachePre" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHeadache10" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHeadache30" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHeadache60" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHeadache120" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHeadache180" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHeadache240" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHeadachePost" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHeadachePost2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHeadachePost3" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHeadachePost4" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHeadachePost5" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHeadachePost6" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHeadachePost7" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsHeadachePost8" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblRednessOnTheSkin" runat="server" Text="Redness On The Skin"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRednessOnTheSkinPre" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRednessOnTheSkin10" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRednessOnTheSkin30" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRednessOnTheSkin60" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRednessOnTheSkin120" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRednessOnTheSkin180" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRednessOnTheSkin240" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRednessOnTheSkinPost" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRednessOnTheSkinPost2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRednessOnTheSkinPost3" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRednessOnTheSkinPost4" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRednessOnTheSkinPost5" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRednessOnTheSkinPost6" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRednessOnTheSkinPost7" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsRednessOnTheSkinPost8" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblTachycardia" runat="server" Text="Tachycardia"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsTachycardiaPre" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsTachycardia10" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsTachycardia30" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsTachycardia60" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsTachycardia120" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsTachycardia180" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsTachycardia240" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsTachycardiaPost" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsTachycardiaPost2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsTachycardiaPost3" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsTachycardiaPost4" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsTachycardiaPost5" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsTachycardiaPost6" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsTachycardiaPost7" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsTachycardiaPost8" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblMuscleStiffness" runat="server" Text="Muscle Stiffness"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMuscleStiffnessPre" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMuscleStiffness10" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMuscleStiffness30" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMuscleStiffness60" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMuscleStiffness120" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMuscleStiffness180" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMuscleStiffness240" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMuscleStiffnessPost" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMuscleStiffnessPost2" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMuscleStiffnessPost3" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMuscleStiffnessPost4" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMuscleStiffnessPost5" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMuscleStiffnessPost6" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMuscleStiffnessPost7" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblIsMuscleStiffnessPost8" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="label">
                        <asp:Label ID="lblOtherReaction" runat="server" Text="Other Reaction"></asp:Label>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOtherReactionPre" runat="server" Width="100px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOtherReaction10" runat="server" Width="100px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOtherReaction30" runat="server" Width="100px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOtherReaction60" runat="server" Width="100px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOtherReaction120" runat="server" Width="100px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOtherReaction180" runat="server" Width="100px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOtherReaction240" runat="server" Width="100px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOtherReactionPost" runat="server" Width="100px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOtherReactionPost2" runat="server" Width="100px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOtherReactionPost3" runat="server" Width="100px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOtherReactionPost4" runat="server" Width="100px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOtherReactionPost5" runat="server" Width="100px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOtherReactionPost6" runat="server" Width="100px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOtherReactionPost7" runat="server" Width="100px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOtherReactionPost8" runat="server" Width="100px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgAction" runat="server">
            <table style="width: 100%" cellpadding="1" cellspacing="1">
                <tr>
                    <td style="width: 20%">
                        <table width="100%">
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionAction0" runat="server" Text="Pre Transfusion"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionAction1" runat="server" Text="15'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionAction2" runat="server" Text="30'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionAction3" runat="server" Text="60'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionAction4" runat="server" Text="120'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionAction5" runat="server" Text="180'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionAction6" runat="server" Text="240'"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionAction7" runat="server" Text="Post Transfusion (1st 8 hours)"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionAction8" runat="server" Text="Post Transfusion (2nd 8 hours)"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaptionAction9" runat="server" Text="Post Transfusion (3rd 8 hours)"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblHemoglobin" runat="server" Text="Hemoglobin"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHemoglobinPre" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHemoglobin10" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHemoglobin30" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHemoglobin60" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHemoglobin120" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHemoglobin180" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHemoglobin240" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHemoglobinPost" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHemoglobinPost2" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHemoglobinPost3" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblHematocrit" runat="server" Text="Hematocrit"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHematocritPre" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHematocrit10" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHematocrit30" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHematocrit60" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHematocrit120" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHematocrit180" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHematocrit240" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHematocritPost" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHematocritPost2" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtHematocritPost3" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPlatelet" runat="server" Text="Platelet"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPlateletPre" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPlatelet10" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPlatelet30" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPlatelet60" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPlatelet120" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPlatelet180" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPlatelet240" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPlateletPost" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPlateletPost2" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPlateletPost3" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="label">
                        <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                    </td>
                    <td style="width: 8%" class="entry">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtActionPre" runat="server" Width="103px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%" class="entry">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtAction10" runat="server" Width="103px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%" class="entry">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtAction30" runat="server" Width="103px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%" class="entry">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtAction60" runat="server" Width="103px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%" class="entry">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtAction120" runat="server" Width="103px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%" class="entry">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtAction180" runat="server" Width="103px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%" class="entry">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtAction240" runat="server" Width="103px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%" class="entry">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtActionPost" runat="server" Width="103px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%" class="entry">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtActionPost2" runat="server" Width="103px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 8%" class="entry">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtActionPost3" runat="server" Width="103px" TextMode="MultiLine"
                                        MaxLength="500" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <table width="100%">
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td colspan="10">
                        <table width="100%">
                            <tr>
                                <td style="width: 25%" class="label">
                                    <asp:Label ID="Label1" runat="server" Text="Start"></asp:Label>
                                </td>
                                <td style="width: 25%" class="label">
                                    <asp:Label ID="Label34" runat="server" Text="End"></asp:Label>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="label">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:Label ID="lblTransfusionDateTime" runat="server" Text="Transfusion Date / Time"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td colspan="10">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 25%">
                                    <telerik:RadDateTimePicker ID="txtTransfusionStartDateTime" runat="server" AutoPostBackControl="None">
                                        <DateInput ID="DateInput3" runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm" DateFormat="dd/MM/yyyy HH:mm">
                                        </DateInput>
                                        <TimeView ID="TimeView3" runat="server" TimeFormat="HH:mm">
                                        </TimeView>
                                    </telerik:RadDateTimePicker>
                                </td>
                                <td style="width: 25%">
                                    <telerik:RadDateTimePicker ID="txtTransfusionEndDateTime" runat="server" AutoPostBackControl="None">
                                        <DateInput ID="DateInput4" runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm" DateFormat="dd/MM/yyyy HH:mm">
                                        </DateInput>
                                        <TimeView ID="TimeView4" runat="server" TimeFormat="HH:mm">
                                        </TimeView>
                                    </telerik:RadDateTimePicker>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="label">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:Label ID="lblTransfusedOfficer" runat="server" Text="Transfused By"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td colspan="10">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 25%">
                                    <telerik:RadTextBox ID="txtTransfusedOfficerStartBy" runat="server" Width="98%" MaxLength="150" />
                                </td>
                                <td style="width: 25%">
                                    <telerik:RadTextBox ID="txtTransfusedOfficerEndBy" runat="server" Width="98%" MaxLength="150" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="display: none">
                    <td style="width: 20%" class="label">
                        <asp:Label ID="lblQtyTransfusion" runat="server" Text="Qty Transfusion"></asp:Label>
                    </td>
                    <td colspan="10" class="entry">
                        <telerik:RadNumericTextBox ID="txtQtyTransfusion" runat="server" Width="100px" Value="1"
                            ReadOnly="True" />
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
