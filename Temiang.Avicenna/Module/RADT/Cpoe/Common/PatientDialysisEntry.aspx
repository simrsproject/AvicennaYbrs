<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="PatientDialysisEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.PatientDialysisEntry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <table width="100%">
                                <tr>
                                    <td class="labelcaption">
                                        <asp:Label ID="lblGeneralInformation" runat="server" Text="General Information"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInitialDiagnosis" runat="server" Text="Initial Diagnosis" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInitialDiagnosis" runat="server" Width="300px" MaxLength="255" />
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRefferingHospital" runat="server" Text="Reffering Hospital" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRefferingHospital" runat="server" Width="300px" MaxLength="255" />
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRefferingPhysician" runat="server" Text="Reffering Physician" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRefferingPhysician" runat="server" Width="300px" MaxLength="255" />
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <table width="100%">
                                <tr>
                                    <td class="labelcaption">
                                        <asp:Label ID="lblExamResult" runat="server" Text="Examination Result"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblHb" runat="server" Text="Hb" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtHb" runat="server" Width="200px" MaxLength="255" />
                            <telerik:RadDatePicker ID="txtHbDate" runat="server" Width="100px" />
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblUrea" runat="server" Text="Urea" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtUrea" runat="server" Width="200px" MaxLength="255" />
                            <telerik:RadDatePicker ID="txtUreaDate" runat="server" Width="100px" />
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCreatinine" runat="server" Text="Creatinine" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtCreatinine" runat="server" Width="200px" MaxLength="255" />
                            <telerik:RadDatePicker ID="txtCreatinineDate" runat="server" Width="100px" />
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblHBsAg" runat="server" Text="HBsAg" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtHBsAg" runat="server" Width="200px" MaxLength="255" />
                            <telerik:RadDatePicker ID="txtHBsAgDate" runat="server" Width="100px" />
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAntiHCV" runat="server" Text="Anti HCV" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAntiHCV" runat="server" Width="200px" MaxLength="255" />
                            <telerik:RadDatePicker ID="txtAntiHCVDate" runat="server" Width="100px" />
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAntiHIV" runat="server" Text="Anti HIV" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAntiHIV" runat="server" Width="200px" MaxLength="255" />
                            <telerik:RadDatePicker ID="txtAntiHIVDate" runat="server" Width="100px" />
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblKidneyUltrasound" runat="server" Text="Kidney Ultrasound" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtKidneyUltrasound" runat="server" Width="200px" MaxLength="255" />
                            <telerik:RadDatePicker ID="txtKidneyUltrasoundDate" runat="server" Width="100px" />
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblECHO" runat="server" Text="ECHO" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtECHO" runat="server" Width="200px" MaxLength="255" />
                            <telerik:RadDatePicker ID="txtECHODate" runat="server" Width="100px" />
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <table width="100%">
                                <tr>
                                    <td class="labelcaption">
                                        <asp:Label ID="lblHemodialisa" runat="server" Text="Hemodialisa"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFirstHemodialysisDate" runat="server" Text="First Hemodialysis Date" />
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtFirstHemodialysisDate" runat="server" Width="100px" />
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransferToHD" runat="server" Text="Date of transfer to HD" />
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransferHDDate" runat="server" Width="100px" />
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <table width="100%">
                                <tr>
                                    <td class="labelcaption">
                                        <asp:Label ID="lblPeritonealDialysis" runat="server" Text="Peritoneal Dialysis"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFirstPeritonealDialysisDate" runat="server" Text="First Peritoneal Dialysis Date" />
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtFirstPeritonealDialysisDate" runat="server" Width="100px" />
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransferToPD" runat="server" Text="Date of transfer to PD" />
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransferPDDate" runat="server" Width="100px" />
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <table width="100%">
                                <tr>
                                    <td class="labelcaption">
                                        <asp:Label ID="lblKidneyTransplant" runat="server" Text="Kidney Transplant"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblKidneyTransplantDate" runat="server" Text="Kidney Transplant Date" />
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtKidneytransplantDate" runat="server" Width="100px" />
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
