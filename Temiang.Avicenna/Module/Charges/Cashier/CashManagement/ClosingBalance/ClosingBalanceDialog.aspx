<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="ClosingBalanceDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Cashier.ClosingBalanceDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <fieldset>
                    <legend>INFORMATION</legend>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblTransactionNo" Text="Transaction No" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblOpeningDate" runat="server" Text="Opening Date / Time"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtOpeningDate" runat="server" Width="100px" Enabled="False">
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                    <td>
                                                        <telerik:RadMaskedTextBox ID="txtOpeningTime" runat="server" Mask="<00..23>:<00..59>"
                                                            PromptChar="_" RoundNumericRanges="false" Width="50px" ReadOnly="True">
                                                        </telerik:RadMaskedTextBox>
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
                                            <asp:Label ID="lblSRShift" runat="server" Text="Shift"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSRShift" runat="server" Width="304px" Enabled="False">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSRCashierCounter" runat="server" Text="Counter"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSRCashierCounter" runat="server" Width="304px" Enabled="False">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblOpeningBalance" runat="server" Text="Opening Balance"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtOpeningBalance" runat="server" Width="100px" ReadOnly="True" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table width="100%">
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset>
                    <legend>CLOSING</legend>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblCashAmount" runat="server" Text="Cash Amount"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtCashAmount" runat="server" Width="100px" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table width="100%">
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
