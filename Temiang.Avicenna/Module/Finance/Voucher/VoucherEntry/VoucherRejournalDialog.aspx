<%@ Page Title="Rejournal" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="VoucherRejournalDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.VoucherRejournalDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="lblJournalType" runat="server" Text="Journal Type" Width="110px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboJournalType" runat="server" AutoPostBack="true" Width="100%" 
                OnSelectedIndexChanged="cboJournalType_OnSelectedIndexChanged">
                    <Items>
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
                        <telerik:RadComboBoxItem runat="server" Text="Account Receiveable Invoicing" Value="14" />
                        <telerik:RadComboBoxItem runat="server" Text="Account Receiveable Payment" Value="11" />
                        <telerik:RadComboBoxItem runat="server" Text="Account Payable Invoicing" Value="25" />
                        <telerik:RadComboBoxItem runat="server" Text="Account Payable Payment" Value="26" />
                        <telerik:RadComboBoxItem runat="server" Text="Purchase Order Received" Value="15" />
                        <telerik:RadComboBoxItem runat="server" Text="Purchase Order Returned" Value="16" />
                        <telerik:RadComboBoxItem runat="server" Text="Grant Received" Value="24" />
                        <telerik:RadComboBoxItem runat="server" Text="Distribution" Value="20" />
                        <telerik:RadComboBoxItem runat="server" Text="Inventory Issue" Value="22" />
                        <telerik:RadComboBoxItem runat="server" Text="Physician Fee Verification" Value="28" />
                        <telerik:RadComboBoxItem runat="server" Text="Physician Fee Payment" Value="27" />
                        <telerik:RadComboBoxItem runat="server" Text="Physician Fee Payable" Value="48" />
                        <telerik:RadComboBoxItem runat="server" Text="Physician Fee Payable Reversal" Value="48a" />
                        <telerik:RadComboBoxItem runat="server" Text="Fixed Asset" Value="30" />
                        <telerik:RadComboBoxItem runat="server" Text="Inventory Stock Adjustment" Value="32" />
                        <telerik:RadComboBoxItem runat="server" Text="Inventory Stock Opname" Value="33" />
                        <telerik:RadComboBoxItem runat="server" Text="Inventory Production" Value="34" />
                        <telerik:RadComboBoxItem runat="server" Text="Patient Receivable" Value="35" />
                        <telerik:RadComboBoxItem runat="server" Text="Patient Receivable Reversal" Value="35a" />
                        <telerik:RadComboBoxItem runat="server" Text="Sales" Value="40" />
                        <telerik:RadComboBoxItem runat="server" Text="SalesReturn" Value="41" />
                        <telerik:RadComboBoxItem runat="server" Text="ARCustomer" Value="42" />
                        <telerik:RadComboBoxItem runat="server" Text="ARCustomerPayment" Value="43" />
                        <%--<telerik:RadComboBoxItem runat="server" Text="Account Payable" Value="25" />
                        <telerik:RadComboBoxItem runat="server" Text="Distribution" Value="20" >
                        <telerik:RadComboBoxItem runat="server" Text="Distribution Confirmed" Value="21">--%>
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="lblReferenceNo" runat="server" Text="Reference No" Width="110px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td><telerik:RadTextBox ID="txtReferenceNo" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td><telerik:RadDatePicker ID="txtReferenceDate" runat="server" Width="105px" Visible="false">
                            <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy">
                            </DateInput>
                            <Calendar ID="Calendar1" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                ViewSelectorText="x">
                            </Calendar>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        </td>
                        <td><telerik:RadDatePicker ID="txtJournalDate" runat="server" Width="105px" Visible="false">
                            <DateInput ID="DateInput2" runat="server" DateFormat="dd/MM/yyyy">
                            </DateInput>
                            <Calendar ID="Calendar2" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                ViewSelectorText="x">
                            </Calendar>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker></td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
