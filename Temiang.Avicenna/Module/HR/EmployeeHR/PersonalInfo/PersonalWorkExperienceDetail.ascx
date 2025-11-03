<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersonalWorkExperienceDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.PersonalWorkExperienceDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPersonalWorkExperience" runat="server" ValidationGroup="PersonalWorkExperience" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PersonalWorkExperience"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblPersonalWorkExperienceID" runat="server" Text="Personal Work Experience ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtPersonalWorkExperienceID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPersonalWorkExperienceID" runat="server" ErrorMessage="Personal Work Experience ID required."
                            ControlToValidate="txtPersonalWorkExperienceID" SetFocusOnError="True" ValidationGroup="PersonalWorkExperience" Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRLineBisnis" runat="server" Text="Line Business"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRLineBisnis" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRLineBisnis" runat="server" ErrorMessage="Line Business required."
                            ControlToValidate="cboSRLineBisnis" SetFocusOnError="True" ValidationGroup="PersonalWorkExperience" Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr runat="server" id="trDatePeriod">
                    <td class="label">
                        <asp:Label ID="lblDatePeriod" runat="server" Text="Date Period"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="txtStartDate" runat="server" Width="100px" MinDate="01/01/1900" MaxDate="12/31/2999" />
                                </td>
                                <td>&nbsp;to&nbsp;
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtEndDate" runat="server" Width="100px" MinDate="01/01/1900" MaxDate="12/31/2999" />
                                </td>
                            </tr>
                        </table>

                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr runat="server" id="trYearPeriod">
                    <td class="label">
                        <asp:Label ID="lblYearPeriod" runat="server" Text="Year Period"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtStartYear" runat="server" Width="80px" MaxLength="4" />
                                </td>
                                <td>&nbsp;to&nbsp;
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtEndYear" runat="server" Width="80px" MaxLength="4" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCompany" runat="server" Text="Company"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtCompany" runat="server" Width="300px" MaxLength="50" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDivision" runat="server" Text="Division"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtDivision" runat="server" Width="300px" MaxLength="50" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtLocation" runat="server" Width="300px" MaxLength="100" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>

                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PersonalWorkExperience"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="PersonalWorkExperience"
                            Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblJobDesc" runat="server" Text="Job Desc"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtJobDesc" runat="server" Width="300px" Height="80px" MaxLength="500" TextMode="MultiLine" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSupervisorName" runat="server" Text="Supervisor Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtSupervisorName" runat="server" Width="300px" MaxLength="50" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblLastSalary" runat="server" Text="Last Salary"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtLastSalary" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblReasonOfLeaving" runat="server" Text="Reason Of Leaving"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtReasonOfLeaving" runat="server" Width="300px" Height="80px" MaxLength="5000" TextMode="MultiLine" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>

</table>
