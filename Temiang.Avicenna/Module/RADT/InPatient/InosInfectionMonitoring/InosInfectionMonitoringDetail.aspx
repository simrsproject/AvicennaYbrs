<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="InosInfectionMonitoringDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.InPatient.InosInfectionMonitoringDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td width="50%">
                <table width="100%">
                    <tr style="display: none">
                        <td class="label">Monitoring ID
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtMonitoringID" runat="server" />
                        </td>
                        <td width="20px" />
                        <td />
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
                                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="103px" ReadOnly="true" />

                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtRegistrationDate" runat="server" Width="110px" Enabled="false">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td class="label" style="width: 80px">
                                        <asp:Label ID="lblLengthOfStay" runat="server" Text="Length Of Stay"></asp:Label>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtLengthOfStay" runat="server" Width="50px" ReadOnly="True">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                        &nbsp;Day(s)
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
                </table>
            </td>
            <td width="50%">
                <table width="100%">
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
                        <td class="entry" colspan="3">
                            <telerik:RadTextBox ID="txtGuarantorID" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblGuarantorName" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%" cellpadding="0" cellspacing="1">
        <tr>
            <td>
                <fieldset>
                    <legend>HAIs Monitoring</legend>
                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblMonitoringDate" runat="server" Text="Monitoring Date"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadDatePicker ID="txtMonitoringDate" runat="server" Width="100px">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvMonitoringDate" runat="server" ErrorMessage="Monitoring Date required."
                                                ValidationGroup="entry" ControlToValidate="txtMonitoringDate" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblTreatment" runat="server" Text="Treatment"></asp:Label>
                                        </td>
                                        <td class="entry" colspan="3">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 150px">
                                                        <asp:CheckBox ID="chkIsMechanicalVentilator" runat="server" Text="Mechanical Ventilator" />
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td style="width: 150px">
                                                        <asp:CheckBox ID="chkIsUrineCatheter" runat="server" Text="Urine Catheter" />
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td style="width: 150px">
                                                        <asp:CheckBox ID="chkIsCentralVeinLine" runat="server" Text="Central Vein Line" />

                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsTotalCare" runat="server" Text="Total Care" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsInpatient" runat="server" Text="Inpatient" />
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsSurgery" runat="server" Text="Surgery" />
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsIntraVeinLine" runat="server" Text="Intra Vein Line" />
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsAntibioticDrugs" runat="server" Text="Antibiotic Drugs" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table width="100%">
                                                <hr />
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblIncidenceOfHAIs" runat="server" Text="Incidence of HAIs"></asp:Label>
                                        </td>
                                        <td class="entry" colspan="3">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 150px">
                                                        <asp:CheckBox ID="chkIsVAP" runat="server" Text="VAP" />
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td style="width: 150px">
                                                        <asp:CheckBox ID="chkIsISK" runat="server" Text="ISK" />
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsIADP" runat="server" Text="IADP" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsHAP" runat="server" Text="HAP" />
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsILO" runat="server" Text="ILO" />
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td></td>
                                                </tr>

                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table width="100%">
                                                <hr />
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblIncidenceOfOtherInfections" runat="server" Text="Incidence of Other Infections"></asp:Label>
                                        </td>
                                        <td class="entry" colspan="3">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 150px">
                                                        <asp:CheckBox ID="chkIsPhlebitis" runat="server" Text="Phlebitis" />
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsDecubitus" runat="server" Text="Decubitus" /></td>
                                                </tr>
                                            </table>
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
</asp:Content>
