<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="UpdateBedStatusDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.InPatient.UpdateBedStatusDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="1" cellspacing="5">
        <tr>
            <td style="width: 100%">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label3" runat="server" Text="BED INFORMATION" Font-Bold="True" Font-Size="9"></asp:Label>
                    </legend>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtServiceUnit" runat="server" Width="240px" ReadOnly="true"
                                                Enabled="false" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtRoom" runat="server" Width="240px" ReadOnly="true" Enabled="false" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblClassID" runat="server" Text="Class"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtClass" runat="server" Width="240px" ReadOnly="true" Enabled="false" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBedID" runat="server" Text="Bed No"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtBedID" runat="server" Width="100px" MaxLength="10" ReadOnly="true"
                                                Enabled="false" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBedStatus" runat="server" Text="Bed Status"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox runat="server" ID="cboBedStatus" Width="243px" AllowCustomText="true"
                                                MarkFirstMatch="true">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trIsRoomIn">
                                        <td class="label">
                                        </td>
                                        <td class="entry">
                                            <asp:CheckBox ID="chkIsRoomIn" runat="server" Text="Room In" />
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td colspan="2" class="label" style="text-align: center">
                                <asp:Label ID="lblInfoPatient" runat="server" Text="Patient Information" Font-Bold="true"
                                    Font-Size="12px" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="240px" MaxLength="20"
                                                ReadOnly="true" Enabled="false" />
                                        </td>
                                        <td style="width: 60px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPatientID" runat="server" Text="Patient ID"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtPatientID" runat="server" Width="240px" ReadOnly="true"
                                                Enabled="false" />
                                        </td>
                                        <td style="width: 60px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="240px" MaxLength="15"
                                                ReadOnly="true" ShowButton="false" Enabled="false" />
                                        </td>
                                        <td style="width: 60px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="240px" ReadOnly="true"
                                                Enabled="false" />
                                        </td>
                                        <td style="width: 60px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtGuarantor" runat="server" Width="240px" ReadOnly="true"
                                                Enabled="false" />
                                        </td>
                                        <td style="width: 60px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblServiceUnitIDPatient" runat="server" Text="Service Unit"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtServiceUnitIDPatient" runat="server" Width="240px" ReadOnly="true"
                                                Enabled="false" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblClassIDPatient" runat="server" Text="Class"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtClassIDPatient" runat="server" Width="240px" ReadOnly="true"
                                                Enabled="false" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtParamedic" runat="server" Width="240px" ReadOnly="true"
                                                Enabled="false" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
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
