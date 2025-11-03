<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="OrganizationUnitSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Master.OrganizationUnitSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblOrganizationUnitCode" runat="server" Text="Organization Unit Code"
                    Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterOrganizationUnitCode" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtOrganizationUnitCode" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblOrganizationUnitName" runat="server" Text="Organization Unit Name"
                    Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterOrganizationUnitName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtOrganizationUnitName" runat="server" Width="300px" MaxLength="200" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Parent Organization Unit Name" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterParentOrganizationUnitName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtParentOrganizationUnitName" runat="server" Width="300px"
                    MaxLength="200" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSROrganizationLevel" runat="server" Text="Organization Level" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSROrganizationLevel" runat="server" Width="300px"></telerik:RadComboBox>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
