<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="HealthTestResultDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.K3RS.HealthTestResultDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="179px" ReadOnly="true" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="120px" ReadOnly="true" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ErrorMessage="Registration No required."
                                                ValidationGroup="entry" ControlToValidate="txtRegistrationNo" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadDatePicker ID="txtRegistrationDate" runat="server" Width="100px" Enabled="False" />
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvRegistrationDate" runat="server" ErrorMessage="Registration Date required."
                                                ValidationGroup="entry" ControlToValidate="txtRegistrationDate" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top">
                                <table>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblParamedic" runat="server" Text="Physician"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtParamedicName" runat="server" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblGuarantor" runat="server" Text="Guarantor"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtGuarantorName" runat="server" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <fieldset>
                    <legend><b>EMPLOYEE IDENTITY</b></legend>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtEmployeeName" runat="server" Width="300px" MaxLength="150" ReadOnly="true" />
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvEmployeeName" runat="server" ErrorMessage="Employee Name required."
                                                ValidationGroup="entry" ControlToValidate="txtEmployeeName" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image25" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblEmployeeNumber" runat="server" Text="Employee Number"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtEmployeeNumber" runat="server" Width="300px" ReadOnly="true" />
                                            <telerik:RadNumericTextBox ID="txtPersonID" runat="server" Width="100px" Visible="false" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblDateOfBirth" runat="server" Text="Date Of Birth / Sex"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="100px" DateInput-ReadOnly="true"
                                                            DatePopupButton-Enabled="false">
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                    <td>&nbsp;/&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtSex" runat="server" Width="42px" ReadOnly="True" />
                                                    </td>
                                                </tr>
                                            </table>

                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtAgeInYear" runat="server" Width="40px" ReadOnly="true">
                                                            <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>&nbsp;Y&nbsp;&nbsp;</td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtAgeInMonth" runat="server" Width="40px" ReadOnly="true">
                                                            <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>&nbsp;M&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtAgeInDay" runat="server" Width="40px" ReadOnly="true">
                                                            <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>&nbsp;D&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblEmployeeType" runat="server" Text="Employee Type"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtEmployeeType" runat="server" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPositionID" runat="server" Text="Position"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtPositionName" runat="server" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="label">
                                            <asp:Label ID="lblEmployeeServiceUnitID" runat="server" Text="Service Unit ID"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtEmployeeServiceUnitID" runat="server" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblEmployeeServiceUnitName" runat="server" Text="Service Unit"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtEmployeeServiceUnitName" runat="server" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblEmployeeServiceYear" runat="server" Text="Service Year"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtEmployeeServiceYear" runat="server" Width="100px" ReadOnly="true" />
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtServiceYearText" runat="server" Width="196px" ReadOnly="true" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td>
                            <fieldset>
                                <legend><b>HEALTH TEST</b>&nbsp;:&nbsp;<asp:Label ID="lblHealtTest" runat="server" Text="Health Test" Font-Bold="true"></asp:Label></legend>
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblResultDate" runat="server" Text="Result Date"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadDatePicker ID="txtResultDate" runat="server" Width="100px" Enabled="False" />
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvResultDate" runat="server" ErrorMessage="Result Date required."
                                                ValidationGroup="entry" ControlToValidate="txtResultDate" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lbExamination" runat="server" Text="Examination"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtExamination" runat="server" Width="100%" TextMode="MultiLine" MaxLength="500" Height="100px" />
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvExamination" runat="server" ErrorMessage="Examination required."
                                                ValidationGroup="entry" ControlToValidate="txtExamination" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblResult" runat="server" Text="Result"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtResult" runat="server" Width="100%" TextMode="MultiLine" MaxLength="500" Height="100px"/>
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvResult" runat="server" ErrorMessage="Result required."
                                                ValidationGroup="entry" ControlToValidate="txtResult" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSRHealthDegreeConclusion" runat="server" Text="Health Degree Conclusion"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSRHealthDegreeConclusion" runat="server" Width="300px" />
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvSRHealthDegreeConclusion" runat="server" ErrorMessage="Health Degree Conclusion required."
                                                ValidationGroup="entry" ControlToValidate="cboSRHealthDegreeConclusion" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td>
                            <fieldset>
                                <legend><b>RECOMENDATION</b></legend>
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblK3rsRecomendation" runat="server" Text="K3RS"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtK3rsRecomendation" runat="server" Width="100%" TextMode="MultiLine" MaxLength="500" Height="130px"/>
                                        </td>
                                        <td width="20px"></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPhysicianRecomendation" runat="server" Text="Physician"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtPhysicianRecomendation" runat="server" Width="100%" TextMode="MultiLine" MaxLength="500" Height="127px"/>
                                        </td>
                                        <td width="20px"></td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
