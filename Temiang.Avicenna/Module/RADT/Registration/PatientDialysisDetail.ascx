<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatientDialysisDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.PatientDialysisDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPatientDialysis" runat="server" ValidationGroup="PatientDialysis" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PatientDialysis"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
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
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRCurrency" runat="server" ErrorMessage="xxxxx required."
                    	    ControlToValidate="txtInitialDiagnosis" SetFocusOnError="True" ValidationGroup="PatientDialysis" Width="100%">
						    <asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
					    </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPhysicianSenders" runat="server" Text="Physician Senders" />
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtPhysicianSenders" runat="server" Width="300px" MaxLength="255" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRSSender" runat="server" Text="RS Sender" />
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtRSSender" runat="server" Width="300px" MaxLength="255" />
                    </td>
                    <td width="20px"></td>
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
                        <telerik:RadTextBox ID="txtHb" runat="server" Width="300px" MaxLength="255" />
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtHbDate" runat="server" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblUrea" runat="server" Text="Urea" />
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtUrea" runat="server" Width="300px" MaxLength="255" />
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtUreaDate" runat="server" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCreatinine" runat="server" Text="Creatinine" />
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtCreatinine" runat="server" Width="300px" MaxLength="255" />
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtCreatinineDate" runat="server" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblHBsAg" runat="server" Text="HBsAg" />
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtHBsAg" runat="server" Width="300px" MaxLength="255" />
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtHBsAgDate" runat="server" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblAntiHCV" runat="server" Text="Anti HCV" />
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtAntiHCV" runat="server" Width="300px" MaxLength="255" />
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtAntiHCVDate" runat="server" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblAntiHIV" runat="server" Text="Anti HIV" />
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtAntiHIV" runat="server" Width="300px" MaxLength="255" />
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtAntiHIVDate" runat="server" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblKidneyUltrasound" runat="server" Text="Kidney Ultrasound" />
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtKidneyUltrasound" runat="server" Width="300px" MaxLength="255" />
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtKidneyUltrasoundDate" runat="server" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblECHO" runat="server" Text="ECHO" />
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtECHO" runat="server" Width="300px" MaxLength="255" />
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtECHODate" runat="server" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>

            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <table width="100%">
                            <tr>
                                <td class="labelcaption">
                                    <asp:Label ID="Label5" runat="server" Text="Hemodiailsa"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblHDpremiere" runat="server" Text="HD Premiere Date" />
                    </td>
                    <%--                    <td class="entry">
                        <telerik:RadTextBox ID="txtHDDate" runat="server" Width="300px" MaxLength="255" />
                    </td>--%>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtHDDate" runat="server" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblTransferToHD" runat="server" Text="Date of transfer to HD" />
                    </td>
                    <%--                   <td class="entry">
                        <telerik:RadTextBox ID="txtTransferHDDate" runat="server" Width="300px" MaxLength="255" />
                    </td>--%>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtTransferHDDate" runat="server" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                    <td width="20px"></td>
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
                        <asp:Label ID="lblPDPremiere" runat="server" Text="PD Premiere Date" />
                    </td>
                    <%--                    <td class="entry">
                        <telerik:RadTextBox ID="txtPDDate" runat="server" Width="300px" MaxLength="255" />
                    </td>--%>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtPDDate" runat="server" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblTransferToPD" runat="server" Text="Date of transfer to PD" />
                    </td>
                    <%--                    <td class="entry">
                        <telerik:RadTextBox ID="txtTransferPDDate" runat="server" Width="300px" MaxLength="255" />
                    </td>--%>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtTransferPDDate" runat="server" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                    <td width="20px"></td>
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
                    <%--                    <td class="entry">
                        <telerik:RadTextBox ID="txtKidneytransplantDate" runat="server" Width="300px" MaxLength="255" />
                    </td>--%>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtKidneytransplantDate" runat="server" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PatientDialysis"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="PatientDialysis" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button></td>
    </tr>
</table>
