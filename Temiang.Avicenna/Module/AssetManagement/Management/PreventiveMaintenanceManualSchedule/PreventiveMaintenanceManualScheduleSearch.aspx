<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PreventiveMaintenanceManualScheduleSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.PreventiveMaintenanceManualScheduleSearch"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblAsset" runat="server" Text="Asset" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterAsset" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtAsset" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPeriodYear" runat="server" Text="Period Year" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterPeriodYear" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtPeriodYear" runat="server" Width="300px" MaxLength="4" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblMaintenanceServiceUnitID" runat="server" Text="Maintenance Unit"
                    Width="100px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboMaintenanceServiceUnitID" runat="server" Width="300px"
                    AllowCustomText="true" Filter="Contains">
                </telerik:RadComboBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblServiceUnitID" runat="server" Text="Asset Location" Width="100px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains">
                </telerik:RadComboBox>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
