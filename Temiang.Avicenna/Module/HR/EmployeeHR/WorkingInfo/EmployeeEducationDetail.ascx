<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeEducationDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.EmployeeEducationDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeeEducation" runat="server" ValidationGroup="EmployeeEducation" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeeEducation"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblEmployeeEducationID" runat="server" Text="ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtEmployeeEducationID" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSREducationStatus" runat="server" Text="Education Status"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSREducationStatus" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSREducationStatus" runat="server" ErrorMessage="Education Status required."
                            ControlToValidate="cboSREducationStatus" SetFocusOnError="True" ValidationGroup="EmployeeEducation"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSREducationFinancingSources" runat="server" Text="Financing Sources"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSREducationFinancingSources" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSREducationFinancingSources" runat="server" ErrorMessage="Financing Sources required."
                            ControlToValidate="cboSREducationFinancingSources" SetFocusOnError="True" ValidationGroup="EmployeeEducation"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label" />
                    <td class="entry">
                        <asp:CheckBox ID="chkIsTuitionAssistance" runat="server" Text="Tuition Assistance" />
                    </td>
                    <td width="20px" />
                    <td />
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblAssistanceAmount" runat="server" Text="Assistance Amount"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtAssistanceAmount" runat="server" Width="100px" Value="0" NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvAssistanceAmount" runat="server" ErrorMessage="Assistance Amount required."
                            ControlToValidate="txtAssistanceAmount" SetFocusOnError="True" ValidationGroup="EmployeeEducation"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblInstitutionName" runat="server" Text="Institution Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtInstitutionName" runat="server" Width="300px" MaxLength="200" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvInstitutionName" runat="server" ErrorMessage="Institution Name required."
                            ControlToValidate="txtInstitutionName" SetFocusOnError="True" ValidationGroup="EmployeeEducation"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblStudyProgram" runat="server" Text="Study Program"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtStudyProgram" runat="server" Width="300px" MaxLength="200" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfStudyProgram" runat="server" ErrorMessage="Study Program required."
                            ControlToValidate="txtStudyProgram" SetFocusOnError="True" ValidationGroup="EmployeeEducation"
                            Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblYearPeriod" runat="server" Text="Year Period"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtStartYearPeriod" runat="server" Width="100px" MaxLength="4" />
                                </td>
                                <td>&nbsp;-&nbsp;
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtEndYearPeriod" runat="server" Width="100px" MaxLength="4" />
                                </td>
                            </tr>
                        </table>

                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvStartYearPeriod" runat="server" ErrorMessage="Start Year Period required."
                            ControlToValidate="txtStartYearPeriod" SetFocusOnError="True" ValidationGroup="EmployeeEducation"
                            Width="100%">
                            <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvtEndYearPeriod" runat="server" ErrorMessage="Start Year Period required."
                            ControlToValidate="txtEndYearPeriod" SetFocusOnError="True" ValidationGroup="EmployeeEducation"
                            Width="100%">
                            <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRStudyPeriodStatus" runat="server" Text="Study Period Status"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRStudyPeriodStatus" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRStudyPeriodStatus" runat="server" ErrorMessage="Study Period Status required."
                            ControlToValidate="cboSRStudyPeriodStatus" SetFocusOnError="True" ValidationGroup="EmployeeEducation"
                            Width="100%">
                            <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeeEducation"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="EmployeeEducation" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label" />
                    <td class="entry">
                        <asp:CheckBox ID="chkIsCommitmentToWork" runat="server" Text="Employment / Service Bond" />
                    </td>
                    <td width="20px" />
                    <td />
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDurationOfService" runat="server" Text="Duration Of Service"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtDurationOfService" runat="server" Width="300px" MaxLength="100" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblServiceDate" runat="server" Text="Service Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="txtStartServiceDate" runat="server" Width="100px" />
                                </td>
                                <td>&nbsp;-&nbsp;
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtEndServiceDate" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
            </table>
        </td>
    </tr>
</table>
