<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="MedicalRecordFileStatusDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.MedicalRecordFileStatusDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ErrorMessage="Registration No required."
                                ValidationGroup="entry" ControlToValidate="txtRegistrationNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
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
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPhysician" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPhysicianName" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" Enabled="False" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtGender" runat="server" Width="80px" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAge" runat="server" Width="80px" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAddress" runat="server" Width="300px" ReadOnly="True"
                                TextMode="MultiLine" />
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
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td class="labelcaption">
                            <asp:Label ID="Label2" runat="server" Text="Borrowing Information"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFileOutDate" runat="server" Text="Date Of File Out"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 103px">
                                        <telerik:RadDatePicker ID="txtFileOutDate" runat="server" Width="100px" />
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtFileOutTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px">
                                        </telerik:RadMaskedTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvFileOutDate" runat="server" ErrorMessage="Date Of File Out required."
                                ValidationGroup="entry" ControlToValidate="txtFileOutDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRequestByUserID" runat="server" Text="Request By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRequestName" runat="server" Width="300px" ReadOnly="True" />
                            <telerik:RadTextBox ID="txtRequestByUserID" runat="server" Width="300px" Visible="False" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRequestByUserID" runat="server" ErrorMessage="Request By required."
                                ValidationGroup="entry" ControlToValidate="txtRequestByUserID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRMedicalFileStatus" runat="server" Text="Category"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRMedicalFileStatus" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRMedicalFileStatus" runat="server" ErrorMessage="Status In / Out required."
                                ValidationGroup="entry" ControlToValidate="cboSRMedicalFileStatus" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRMedicalFileCategory" runat="server" Text="Status In / Out"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRMedicalFileCategory" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" Enabled="False" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRMedicalFileCategory" runat="server" ErrorMessage="For The Purposes Of required."
                                ValidationGroup="entry" ControlToValidate="cboSRMedicalFileCategory" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFileInDate" runat="server" Text="Date Of File In"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 103px">
                                        <telerik:RadDatePicker ID="txtFileInDate" runat="server" Width="100px" Enabled="False" />
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtFileInTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px" ReadOnly="True">
                                        </telerik:RadMaskedTextBox>
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
                            <asp:Label ID="lblNameOfRecipientID" runat="server" Text="Name Of Recipient"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNameOfRecipient" runat="server" Width="300px" ReadOnly="True" />
                            <telerik:RadTextBox ID="txtReceiptByUserID" runat="server" Visible="False" />
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
</asp:Content>
