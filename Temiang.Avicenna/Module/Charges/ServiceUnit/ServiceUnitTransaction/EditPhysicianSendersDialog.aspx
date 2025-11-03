<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="EditPhysicianSendersDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.EditPhysicianSendersDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <fieldset>
                    <legend>PATIENT INFORMATION</legend>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblTransactionNo" Text="Transaction No / Date" />
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadTextBox runat="server" ID="txtTransactionNo" Width="195px" ReadOnly="True" />
                                                    </td>
                                                    <td style="width: 5px">
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                                            DatePopupButton-Enabled="false">
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblServiceUnit" Text="Service Unit" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtServiceUnitName" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblServiceUnitOrder" Text="Service Unit Order" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtServiceUnitOrderName" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblRegistrationNo" Text="Reg No / MRN" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="189px" MaxLength="20"
                                                ReadOnly="true" />
                                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblPatientName" Text="Patient Name" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="275px" MaxLength="20"
                                                ReadOnly="true" />
                                            <telerik:RadTextBox ID="txtGender" runat="server" Width="14px" ReadOnly="true" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblRoomBed" Text="Room / Bed" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtRoom" Width="189px" ReadOnly="True" />
                                            <telerik:RadTextBox runat="server" ID="txtBed" Width="100px" ReadOnly="True" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset>
                    <legend>PHYSICIAN SENDERS</legend>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblPhysicianSenders" Text="Physician Senders" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtPhysicianSenders" Width="300px" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
