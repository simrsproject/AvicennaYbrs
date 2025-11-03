<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="WorkingHourSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.WorkingHourSearch" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblWorkingHourName" runat="server" Text="Working Hour Name"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterWorkingHourName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtWorkingHourName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblShiftCategory" runat="server" Text="Shift Category"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterShiftCategory" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRShift" runat="server" Width="300px" AllowCustomText="true" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>