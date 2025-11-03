<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="VoidRegistrationDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.VoidRegistrationDialog" %>

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
                                            <asp:Label runat="server" ID="lblRegistrationNo" Text="Registration No" />
                                        </td>
                                        <td class="entry">
                                            <table cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20"
                                                            ReadOnly="true" />
                                                    </td>
                                                    <td>
                                                        <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                                            <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                                                Text=""></asp:Label>&nbsp; </a>
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
                                            <asp:Label runat="server" ID="lblMedicalNo" Text="Medical No" />
                                        </td>
                                        <td class="entry">
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
                                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSex" runat="server" Text="Gender"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <asp:RadioButton ID="optSexFemale" runat="server" Text="Female" GroupName="Sex" Enabled="false" />
                                            <asp:RadioButton ID="optSexMale" runat="server" Text="Male" GroupName="Sex" Enabled="false" />
                                        </td>
                                        <td style="width: 20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table width="100%">
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
                                            <asp:Label runat="server" ID="lblRoomBed" Text="Room" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtRoom" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblBedNo" Text="Bed" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtBed" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblPhysicianName" Text="Physician" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtPhysicianName" Width="300px" ReadOnly="True" />
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
                    <legend>VOID REGISTRATION</legend>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblVoidReason" Text="Void Reason" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox runat="server" ID="cboVoidReason" Width="300px" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblVoidNotes" Text="Void Notes" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtVoidNotes" Width="300px" TextMode="MultiLine" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table width="100%"></table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
