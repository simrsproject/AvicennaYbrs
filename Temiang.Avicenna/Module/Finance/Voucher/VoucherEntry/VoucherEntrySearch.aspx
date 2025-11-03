<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="VoucherEntrySearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.VoucherEntrySearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="lblIsApproved" runat="server" Text="Status" Width="110px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboPostingStatus" runat="server" Width="300px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="-" Value="-1" />
                        <telerik:RadComboBoxItem runat="server" Text="Non-Approved" Value="0" />
                        <telerik:RadComboBoxItem runat="server" Text="Approved" Value="1" />
                        <telerik:RadComboBoxItem runat="server" Text="Void" Value="2" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="lblVoucherCode" runat="server" Text="Voucher Code" Width="110px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadTextBox ID="txtVoucherCode" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="lblVoucherNumber" runat="server" Text="Voucher Number" Width="110px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadTextBox ID="txtVoucherNumber" runat="server" Width="300px" MaxLength="5" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="lblVoucherDate" runat="server" Text="Voucher Date" Width="110px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <table cellpadding="0" cellspacing="">
                    <tr>
                        <td colspan="3">
                            <asp:RadioButtonList runat="server" ID="rbRangeFilter">
                                <asp:ListItem Value="0" Text="All Range"></asp:ListItem>
                                <asp:ListItem Value="1" Text="This Month"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Last One Month"></asp:ListItem>
                                <asp:ListItem Value="3" Text="This Year"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Last One Year"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtVoucherDate" runat="server" Width="100px" />
                        </td>
                        <td style="width: 10px">-</td>
                        <td>
                            <telerik:RadDatePicker ID="txtVoucherDateTo" runat="server" Width="100px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="lblJournalType" runat="server" Text="Journal Type" Width="110px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboJournalType" runat="server" Width="300px">
                    <%--<Items>
                    <telerik:RadComboBoxItem runat="server" Text="-" Value="" />
                    <telerik:RadComboBoxItem runat="server" Text="General" Value="01" />
                    <telerik:RadComboBoxItem runat="server" Text="Income" Value="02" />
                    <telerik:RadComboBoxItem runat="server" Text="Cash Transaction" Value="12" />
                    <telerik:RadComboBoxItem runat="server" Text="Prescription" Value="03" />
                    <telerik:RadComboBoxItem runat="server" Text="Prescription Return" Value="04" />
                    <telerik:RadComboBoxItem runat="server" Text="Down Payment" Value="09" />
                    <telerik:RadComboBoxItem runat="server" Text="Down Payment Return" Value="08" />
                    <telerik:RadComboBoxItem runat="server" Text="Payment Received" Value="10" />
                    <telerik:RadComboBoxItem runat="server" Text="Payment Received (Guarantor Uninvoice)" Value="13" />
                    <telerik:RadComboBoxItem runat="server" Text="Account Receivable Invoicing" Value="14" />
                    <telerik:RadComboBoxItem runat="server" Text="Account Receivable Payment" Value="11" />
                    <telerik:RadComboBoxItem runat="server" Text="Account Payable Invoicing" Value="25" />
                    <telerik:RadComboBoxItem runat="server" Text="Account Payable Payment" Value="26" />
                    <telerik:RadComboBoxItem runat="server" Text="Purchase Order Received" Value="15" />
                    <telerik:RadComboBoxItem runat="server" Text="Purchase Order Returned" Value="16" />
                    <telerik:RadComboBoxItem runat="server" Text="Grant Received" Value="24" />
                    <telerik:RadComboBoxItem runat="server" Text="Distribution" Value="20" />
                    <telerik:RadComboBoxItem runat="server" Text="Inventory Issue" Value="22" />
                    <telerik:RadComboBoxItem runat="server" Text="Phycisian Fee Verification" Value="28" />
                    <telerik:RadComboBoxItem runat="server" Text="Phycisian Fee Payment" Value="27" />
                    <telerik:RadComboBoxItem runat="server" Text="Fixed Asset" Value="30" />
                    <telerik:RadComboBoxItem runat="server" Text="Inventory Stock Adjustment" Value="32" />
                    <telerik:RadComboBoxItem runat="server" Text="Inventory Stock Opname" Value="33" />
                    <telerik:RadComboBoxItem runat="server" Text="Inventory Production" Value="34" />
                    <telerik:RadComboBoxItem runat="server" Text="Patient Receivable" Value="35" />
                    <telerik:RadComboBoxItem runat="server" Text="Payroll" Value="38" />
                    <telerik:RadComboBoxItem runat="server" Text="THR" Value="39" />
                    <telerik:RadComboBoxItem runat="server" Text="Account Payable" Value="25" />
                    <telerik:RadComboBoxItem runat="server" Text="Distribution Confirmed" Value="21"/>
                </Items>--%>
                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="Label1" runat="server" Text="Description" Width="110px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No" Width="110px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="lblReferenceNo" runat="server" Text="Reference No" Width="110px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadTextBox ID="txtReferenceNo" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
