<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeePerformanceAppraisalDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.EmployeePerformanceAppraisalDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeePerformanceAppraisal" runat="server" ValidationGroup="EmployeePerformanceAppraisal" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeePerformanceAppraisal"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblPerformanceAppraisalID" runat="server" Text="Performance Appraisal ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtPerformanceAppraisalID" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblParticipantItemID" runat="server" Text="ParticipantItemID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtParticipantItemID" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblYearPeriod" runat="server" Text="Year"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtYearPeriod" runat="server" Width="300px" MaxLength="4" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvYearPeriod" runat="server" ErrorMessage="Year required."
                            ControlToValidate="txtYearPeriod" SetFocusOnError="True" ValidationGroup="EmployeePerformanceAppraisal"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRQuarterPeriod" runat="server" Text="Quarter"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRQuarterPeriod" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRQuarterPeriod" runat="server" ErrorMessage="Quarter required."
                            ControlToValidate="cboSRQuarterPeriod" SetFocusOnError="True" ValidationGroup="EmployeePerformanceAppraisal"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Score
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtScore" runat="server" Width="100px" Value="0" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblScoreText" runat="server" Text="Score Text"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtScoreText" runat="server" Width="300px" MaxLength="50"/>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeePerformanceAppraisal"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="EmployeePerformanceAppraisal" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
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
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine"/>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
