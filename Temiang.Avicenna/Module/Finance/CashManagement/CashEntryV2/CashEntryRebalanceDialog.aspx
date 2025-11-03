<%@ Page Title="Rejournal" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="CashEntryRebalanceDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.CashManagement.CashEntryV2.CashEntryRebalanceDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="lblJournalType" runat="server" Text="Bank" Width="110px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboBank" runat="server" Width="100%">
                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="lblReferenceNo" runat="server" Text="Period" Width="110px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtDateStart" runat="server" Width="105px">
                                <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                                <Calendar ID="Calendar1" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                    ViewSelectorText="x">
                                </Calendar>
                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtDateEnd" runat="server" Width="105px">
                                <DateInput ID="DateInput2" runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                                <Calendar ID="Calendar2" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                    ViewSelectorText="x">
                                </Calendar>
                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
