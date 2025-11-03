<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PatientInformationDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PatientInformationDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="vertical-align: top">
                <fieldset id="FieldSet1" style="width: 150px; min-height: 150px;">
                    <legend>Photo</legend>
                    <asp:Image runat="server" ID="imgPatientPhoto" Width="150px" Height="150px" />
                </fieldset>
            </td>
            <td style="width: 50%;vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientID" runat="server" Text="Patient ID / Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientID" runat="server" Width="130px" ReadOnly="true">
                            </telerik:RadTextBox>
                            &nbsp;/&nbsp;
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRSalutation" runat="server" Text="Salutation"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSalutationName" runat="server" Width="300px" ReadOnly="true">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label>
                        </td>
                        <td class="entry" style="vertical-align: middle;">
                            <telerik:RadTextBox ID="txtFirstName" runat="server" Width="300px" ReadOnly="true">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMiddleName" runat="server" Width="300px" ReadOnly="true">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLastName" runat="server" Text="Last Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtLastName" runat="server" Width="300px" ReadOnly="true">
                            </telerik:RadTextBox>
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
                            <asp:Label ID="lblCityOfBirth" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtCityOfBirth" runat="server" Width="180px" ReadOnly="true" />
                                    </td>
                                    <td>
                                        &nbsp;/&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false">
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
<%--                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSex" runat="server" Text="Gender"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rbtSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="M" Enabled="false" Text="Male" />
                                <asp:ListItem Value="F" Enabled="false" Text="Female" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSex" runat="server" Text="Gender"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSex" runat="server" Width="300px" ReadOnly="true">
                            </telerik:RadTextBox>
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
                            <telerik:RadTextBox ID="txtAgeYear" runat="server" Width="50px" ReadOnly="true">
                            </telerik:RadTextBox>
                            &nbsp;Y&nbsp;
                            <telerik:RadTextBox ID="txtAgeMonth" runat="server" Width="50px" ReadOnly="true">
                            </telerik:RadTextBox>
                            &nbsp;M&nbsp;
                            <telerik:RadTextBox ID="txtAgeDay" runat="server" Width="50px" ReadOnly="true">
                            </telerik:RadTextBox>
                            &nbsp;D
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSSN" runat="server" Text="SSN"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSSN" runat="server" Width="300px" ReadOnly="true">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtGuarantorName" runat="server" Width="300px" ReadOnly="true">
                            </telerik:RadTextBox>
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
    <telerik:RadTabStrip runat="server" ID="tabDetail" MultiPageID="mpgDetail" ShowBaseLine="true">
        <Tabs>
            <telerik:RadTab Text="Address" runat="server" PageViewID="pgAddress" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab Text="Temporary Address" runat="server" PageViewID="pgTempAddress">
            </telerik:RadTab>
            <telerik:RadTab Text="Detail" runat="server" PageViewID="pgDetail">
            </telerik:RadTab>
            <telerik:RadTab Text="Office & Occupation" runat="server" PageViewID="pgOffice">
            </telerik:RadTab>
            <telerik:RadTab Text="Other" runat="server" PageViewID="pgOther">
            </telerik:RadTab>
            <telerik:RadTab Text="Allergy" runat="server" PageViewID="pgAllergy">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage runat="server" ID="mpgDetail" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView runat="server" ID="pgAddress">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2" style="text-align: center">
                        <table width="100%">
                            <tr>
                                <td class="labelcaption">
                                    <asp:Label ID="Label2" runat="server" Text="Address :"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="50%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblStreetName" runat="server" Text="Street Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtStreetName" runat="server" Width="300px" MaxLength="250" TextMode="MultiLine"
                                        ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblZipCode" runat="server" Text="Zip Code"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtZipCode" runat="server" Width="100px" MaxLength="15" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDistrict" runat="server" Text="District"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtDistrict" runat="server" Width="300px" MaxLength="20"
                                        ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCounty" runat="server" Text="County"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtCounty" runat="server" Width="300px" MaxLength="50" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtCity" runat="server" Width="300px" MaxLength="50" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20px">
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="50%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblState" runat="server" Text="State"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtState" runat="server" Width="300px" MaxLength="50" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtPhoneNo" runat="server" Width="300px" MaxLength="50" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFaxNo" runat="server" Text="Fax No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtFaxNo" runat="server" Width="300px" MaxLength="50" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblMobilePhoneNo" runat="server" Text="Mobile Phone No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtMobilePhoneNo" runat="server" Width="300px" MaxLength="20"
                                        ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtEmail" runat="server" Width="300px" MaxLength="50" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20px">
                                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="Email is not valid" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="entry">
                                        <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                    </asp:RegularExpressionValidator>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="pgTempAddress">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2" style="text-align: center">
                        <table width="100%">
                            <tr>
                                <td class="labelcaption">
                                    <asp:Label ID="Label1" runat="server" Text="Address :"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="50%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblTempStreetName" runat="server" Text="Street Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtTempAddressStreetName" runat="server" Width="300px" ReadOnly="true" TextMode="MultiLine">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblTempAddressZipCode" runat="server" Text="Zip Code"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0" width="100">
                                        <tr>
                                            <td>
                                                <telerik:RadTextBox ID="txtTempAddressZipCode" runat="server" Width="100px" ReadOnly="true">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblTempAddressDistrict" runat="server" Text="District"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtTempAddressDistrict" runat="server" Width="300px" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblTempAddressCounty" runat="server" Text="County"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtTempAddressCounty" runat="server" Width="300px" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblTempAddressCity" runat="server" Text="City"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtTempAddressCity" runat="server" Width="300px" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblTempAddressState" runat="server" Text="State"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtTempAddressState" runat="server" Width="300px" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblTempAddressPhoneNo" runat="server" Text="Phone No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtTempAddressPhoneNo" runat="server" Width="300px" ReadOnly="true">
                                    </telerik:RadTextBox>
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
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="pgDetail">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRBloodType" runat="server" Text="Blood"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtBloodType" runat="server" Width="300px" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblBloodRhesus" runat="server" Text="Rhesus"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtBloodRhesus" runat="server" Width="300px" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSREthnic" runat="server" Text="Ethnic"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtEthnic" runat="server" Width="300px" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSREducation" runat="server" Text="Education"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtEducation" runat="server" Width="300px" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRMaritalStatus" runat="server" Text="Marital Status"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtMaritalStatus" runat="server" Width="300px" ReadOnly="true">
                                    </telerik:RadTextBox>
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
                                    <asp:Label ID="lblSRNationality" runat="server" Text="Nationality"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtNationality" runat="server" Width="300px" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRPatientCategory" runat="server" Text="Patient Category"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtPatientCategory" runat="server" Width="300px" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRReligion" runat="server" Text="Religion"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtReligion" runat="server" Width="300px" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRMedicalFileBin" runat="server" Text="Medical File Bin"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtMedicalFileBin" runat="server" Width="300px" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRMedicalFileStatus" runat="server" Text="Medical File Status"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtMedicalFileStatus" runat="server" Width="300px" ReadOnly="true">
                                    </telerik:RadTextBox>
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
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="pgOffice">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSROccupation" runat="server" Text="Occupation"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtOccupation" runat="server" Width="300px" ReadOnly="true">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCompany" runat="server" Text="Company"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtCompany" runat="server" Width="300px" ReadOnly="true">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="pgOther">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblLastVisitDate" runat="server" Text="Last Visit Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtLastVisitDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                        DatePopupButton-Enabled="false">
                                    </telerik:RadDatePicker>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblNumberOfVisit" runat="server" Text="Number Of Visit"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtNumberOfVisit" runat="server" Width="100px" ReadOnly="true">
                                        <NumberFormat DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOldMedicalNo" runat="server" Text="Old Medical No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOldMedicalNo" runat="server" Width="100px" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAccountNo" runat="server" Text="Account No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtAccountNo" runat="server" Width="300px" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                </td>
                                <td class="entry">
                                    <asp:CheckBox ID="chkIsBlackList" Text="Blacklist" runat="server" Enabled="false" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                </td>
                                <td class="entry">
                                    <asp:CheckBox ID="chkIsAlive" Text="Deceased" runat="server" Enabled="false" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDeathCertificateNo" runat="server" Text="Death Certificate No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtDeathCertificateNo" runat="server" Width="300px" ReadOnly="true">
                                    </telerik:RadTextBox>
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
                                </td>
                                <td class="entry">
                                    <asp:CheckBox ID="chkIsDonor" Text="Donor" runat="server" Enabled="false" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblNumberOfDonor" runat="server" Text="Number Of Donor"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtNumberOfDonor" runat="server" Width="100px" ReadOnly="true">
                                        <NumberFormat DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblLastDonorDate" runat="server" Text="Last Donor Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtLastDonorDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                        DatePopupButton-Enabled="false">
                                    </telerik:RadDatePicker>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDiagnosticNo" runat="server" Text="Radiology No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtDiagnosticNo" runat="server" Width="100%" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtNotes" runat="server" Width="100%" TextMode="MultiLine"
                                        ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                </td>
                                <td class="entry">
                                    <asp:CheckBox ID="chkIsActive" Text="Active" runat="server" Enabled="false" />
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
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="pgAllergy">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadGrid ID="grdPatientAllergy" runat="server" AutoGenerateColumns="False"
                            GridLines="None" OnNeedDataSource="grdPatientAllergy_NeedDataSource">
                            <MasterTableView DataKeyNames="ItemID" GroupLoadMode="Client">
                                <GroupByExpressions>
                                    <telerik:GridGroupByExpression>
                                        <SelectFields>
                                            <telerik:GridGroupByField FieldName="StandardReferenceID" HeaderText="Group "></telerik:GridGroupByField>
                                        </SelectFields>
                                        <GroupByFields>
                                            <telerik:GridGroupByField FieldName="StandardReferenceID" SortOrder="Ascending">
                                            </telerik:GridGroupByField>
                                        </GroupByFields>
                                    </telerik:GridGroupByExpression>
                                </GroupByExpressions>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Allergen Name" UniqueName="ItemName"
                                        SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="200px" />
                                    <telerik:GridBoundColumn DataField="DescAndReaction" HeaderText="Description & Reaction"
                                        UniqueName="DescAndReaction" SortExpression="DescAndReaction" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
