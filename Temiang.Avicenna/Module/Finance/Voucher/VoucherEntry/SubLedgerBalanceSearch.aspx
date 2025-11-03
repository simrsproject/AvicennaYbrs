<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="SubLedgerBalanceSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.SubLedgerBalanceSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="Label3" runat="server" Text="Chart Of Account Code" Width="110px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtCoaCode" runat="server" Width="300px" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="Label2" runat="server" Text="Chart Of Account Name" Width="110px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtCoaName" runat="server" Width="300px" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="Label1" runat="server" Text="Sub Ledger Name" Width="110px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtSlName" runat="server" Width="300px" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
