<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecruitmentTestDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.RecruitmentTestDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblTestDate" runat="server" Text="Test Date" />
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtTestDate" runat="server" Width="100px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblTestName" runat="server" Text="Test Name" />
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboTestName" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblTestResult" runat="server" Text="Test Result" />
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtTestResult" runat="server" Width="300px" MaxLength="255" />
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
                        <asp:Label ID="lblNotes" runat="server" Text="Notes" />
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" MaxLength="4000" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRRecruitmentTestConclusion" runat="server" Text="Conclusion" />
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRRecruitmentTestConclusion" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="Button1" Text="Update" runat="server" CommandName="Update" Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="Button2" Text="Insert" runat="server" CommandName="PerformInsert"
                            Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="Button3" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
