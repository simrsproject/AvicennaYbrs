<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="FeasibilityTestSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Cssd.Transaction.FeasibilityTestSearch" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblFeasibilityTestNo" runat="server" Text="Transaction No"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterFeasibilityTestNo" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtFeasibilityTestNo" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblFeasibilityTestDate" runat="server" Text="Date"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtFeasibilityTestDate" runat="server" Width="100px" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>