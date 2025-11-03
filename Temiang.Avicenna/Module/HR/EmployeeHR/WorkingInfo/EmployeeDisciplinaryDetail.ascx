<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeDisciplinaryDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.EmployeeDisciplinaryDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeeDisciplinary" runat="server" ValidationGroup="EmployeeDisciplinary" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeeDisciplinary"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblEmployeeDisciplinaryID" runat="server" Text="Employee Disciplinary ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtEmployeeDisciplinaryID" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRWarningLevel" runat="server" Text="Warning Level"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRWarningLevel" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRWarningLevel" runat="server" ErrorMessage="Warning Level required."
                            ControlToValidate="cboSRWarningLevel" SetFocusOnError="True" ValidationGroup="EmployeeDisciplinary"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblIncidentDate" runat="server" Text="Incident Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtIncidentDate" runat="server" Width="100px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvIncidentDate" runat="server" ErrorMessage="Incident Date required."
                            ControlToValidate="txtIncidentDate" SetFocusOnError="True" ValidationGroup="EmployeeDisciplinary"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDateIssue" runat="server" Text="Date Issue"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtDateIssue" runat="server" Width="100px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvDateIssue" runat="server" ErrorMessage="Date Issue required."
                            ControlToValidate="txtDateIssue" SetFocusOnError="True" ValidationGroup="EmployeeDisciplinary"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblViolation" runat="server" Text="Violation"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtViolation" runat="server" Width="300px" MaxLength="1000"
                            TextMode="MultiLine" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEffectViolation" runat="server" Text="Effect Violation"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtEffectViolation" runat="server" Width="300px" MaxLength="1000"
                            TextMode="MultiLine" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRViolationDegree" runat="server" Text="Violation Degree"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRViolationDegree" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRViolationType" runat="server" Text="Violation Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRViolationType" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblAdviceGiven" runat="server" Text="Advice Given"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtAdviceGiven" runat="server" Width="300px" MaxLength="1000"
                            TextMode="MultiLine" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSanctionGiven" runat="server" Text="Sanction Given"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtSanctionGiven" runat="server" Width="300px" MaxLength="1000"
                            TextMode="MultiLine" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNote" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNote" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEffectiveDate" runat="server" Text="Effective Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtEffectiveDate" runat="server" Width="100px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvEffectiveDate" runat="server" ErrorMessage="Effective Date required."
                            ValidationGroup="EmployeeDisciplinary" ControlToValidate="txtEffectiveDate" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblValidUntil" runat="server" Text="Valid Until"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtValidUntil" runat="server" Width="100px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvValidUntil" runat="server" ErrorMessage="Valid Until required."
                            ValidationGroup="EmployeeDisciplinary" ControlToValidate="txtValidUntil" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeeDisciplinary"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="EmployeeDisciplinary" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
