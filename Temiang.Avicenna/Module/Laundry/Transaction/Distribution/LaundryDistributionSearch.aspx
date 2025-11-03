<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="LaundryDistributionSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Laundry.Transaction.LaundryDistributionSearch" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblDistributionNo" runat="server" Text="Distribution No" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterDistributionNo" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDistributionNo" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDistributionDate" runat="server" Text="Distribution Date" Width="100px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtDistributionDate" runat="server" Width="100px" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblToServiceUnitID" runat="server" Text="To Unit" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterToServiceUnitID" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboToServiceUnitID" runat="server" Width="300px" DataTextField="ServiceUnitName"
                    DataValueField="ServiceUnitID" AllowCustomText="true" MarkFirstMatch="true" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>