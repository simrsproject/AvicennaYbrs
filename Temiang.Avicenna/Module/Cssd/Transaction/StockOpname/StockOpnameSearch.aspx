<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="StockOpnameSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Cssd.Transaction.StockOpnameSearch" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No" Width="100px"></asp:Label>
            </td>
            <td class="filter">

                <telerik:RadComboBox ID="cboFilterTransactionNo" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>

                </telerik:RadComboBox>

            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtTransactionDateFrom" runat="server" Width="100px" />
                        </td>
                        <td>&nbsp;-&nbsp;</td>
                        <td>
                            <telerik:RadDatePicker ID="txtTransactionDateTo" runat="server" Width="100px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboServiceUnitID" Width="300px" runat="server" AllowCustomText="true"
                    Filter="Contains" >
                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>