<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OvertimeFormulaItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.OvertimeFormulaItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumOvertimeDetailFormula" runat="server" ValidationGroup="OvertimeDetailFormula" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="OvertimeDetailFormula"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblOvertimeDetailID" runat="server" Text="Overtime Detail ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtOvertimeDetailID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblHourFrom" runat="server" Text="Hour From"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtHourFrom" runat="server" Width="100px" NumberFormat-DecimalDigits="2" />
                                </td>
                                <td style="width: 20px">
                                     &nbsp;To
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtHourTo" runat="server" Width="100px" NumberFormat-DecimalDigits="2" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvHourFrom" runat="server" ErrorMessage="Hour From required."
                            ControlToValidate="txtHourFrom" SetFocusOnError="True" ValidationGroup="OvertimeDetailFormula"
                            Width="100%">
                            <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvHourTo" runat="server" ErrorMessage="Hour To required."
                            ControlToValidate="txtHourTo" SetFocusOnError="True" ValidationGroup="OvertimeDetailFormula"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblValue" runat="server" Text="Value"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtValue" runat="server" Width="100px" NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvValue" runat="server" ErrorMessage="Value required."
                            ControlToValidate="txtValue" SetFocusOnError="True" ValidationGroup="OvertimeDetailFormula"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblFormula" runat="server" Text="Formula"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtFormula" runat="server" Width="100px" NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvFormula" runat="server" ErrorMessage="Formula required."
                            ControlToValidate="txtFormula" SetFocusOnError="True" ValidationGroup="OvertimeDetailFormula"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="OvertimeDetailFormula"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="OvertimeDetailFormula" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                
            </table>
        </td>
    </tr>
</table>