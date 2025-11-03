<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutoNumberLastDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.ControlPanel.Setting.AppAutoNumberLastDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumAppAutoNumberLast" runat="server" ValidationGroup="AppAutoNumberLast" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="AppAutoNumberLast"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" valign="top">
            <table width="100%">
                <td class="label">
                    <asp:Label ID="lblDepartmentInitial" runat="server" Text="Department Initial"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtDepartmentInitial" runat="server" Width="100px" MaxLength="3" Enabled="False" />
                </td>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblYearNo" runat="server" Text="Year No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtYearNo" NumberFormat-DecimalDigits="0" runat="server"
                            Width="100px" Enabled="False">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblMonthNo" runat="server" Text="Month No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtMonthNo" NumberFormat-DecimalDigits="0" runat="server"
                            Width="100px" Enabled="False">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDayNo" runat="server" Text="Day No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtDayNo" NumberFormat-DecimalDigits="0" runat="server"
                            Width="100px" Enabled="False">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td width="20px">
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
                        <asp:Label ID="lblLastCompleteNumber" runat="server" Text="Last Complete Number"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtLastCompleteNumber" runat="server" Width="300px" MaxLength="20"
                            Enabled="False" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblLastNumber" runat="server" Text="Last Number"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtLastNumber" NumberFormat-DecimalDigits="0" runat="server"
                            Width="100px">
                            <NumberFormat DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="AppAutoNumberLast"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="AppAutoNumberLast" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
