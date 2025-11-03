<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatientDetailCtl.ascx.cs"
    Inherits="Temiang.Avicenna.CustomControl.PatientDetailCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="cboGuarantorID">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtBusinessMethod" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20"
                            ReadOnly="True" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" MaxLength="15"
                            ReadOnly="True" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblParamedicID" runat="server" Text="Physician ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtParamedicID" runat="server" Width="100px" MaxLength="10"
                            ReadOnly="True" />
                        &nbsp;
                        <asp:Label ID="lblParamedicName" runat="server" CssClass="labeldescription"></asp:Label>
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="50"
                            ReadOnly="True" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSex" runat="server" Text="Gender"></asp:Label>
                    </td>
                    <td class="entry">
                        <asp:RadioButton ID="optSexMale" runat="server" Text="Male" GroupName="Sex" />
                        <asp:RadioButton ID="optSexFemale" runat="server" Text="Female" GroupName="Sex" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtAgeYear" runat="server" Width="50px" ReadOnly="True">
                            <NumberFormat DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                        &nbsp;Y&nbsp;
                        <telerik:RadNumericTextBox ID="txtAgeMonth" runat="server" Width="50px" ReadOnly="True">
                            <NumberFormat DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                        &nbsp;M&nbsp;
                        <telerik:RadNumericTextBox ID="txtAgeDay" runat="server" Width="50px" ReadOnly="True">
                            <NumberFormat DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                        &nbsp;D
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDepartmentID" runat="server" Text="Department"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtDepartmentID" runat="server" Width="100px" MaxLength="10"
                            ReadOnly="True" />
                        &nbsp;
                        <asp:Label ID="lblDepartmentName" runat="server" CssClass="labeldescription"></asp:Label>
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="10"
                            ReadOnly="True" />
                        &nbsp;
                        <asp:Label ID="lblServiceUnitName" runat="server" CssClass="labeldescription"></asp:Label>
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRoomID" runat="server" Text="Division ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtRoomID" runat="server" Width="100px" MaxLength="10" ReadOnly="True" />
                        &nbsp;
                        <asp:Label ID="lblRoomName" runat="server" CssClass="labeldescription"></asp:Label>
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBedID" runat="server" Text="Bed No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtBedID" runat="server" Width="100px" MaxLength="10" ReadOnly="True" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor ID*"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboGuarantorID" runat="server" Width="304px">
                            <FooterTemplate>
                                Note : Show max 10 result
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvGuarantorID" runat="server" ErrorMessage="Guarantor ID required."
                            ValidationGroup="entry" ControlToValidate="cboGuarantorID" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBusinessMethod" runat="server" Text="Business Method"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtBusinessMethod" runat="server" Width="300px" MaxLength="50"
                            ReadOnly="True" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
