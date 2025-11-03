<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PersonalEducationHistorySearch.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Query.PersonalEducationHistorySearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblEmployeeNumber" runat="server" Text="Employee No" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterEmployeeNumber" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtEmployeeNo" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblFirstName" runat="server" Text="First Name" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFirstName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtFirstName" runat="server" Width="300px" MaxLength="500" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSREducationLevel" runat="server" Text="Education Level" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSREducationLevel" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblInstitutionName" runat="server" Text="Institution Name" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterInstitutionName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtInstitutionName" runat="server" Width="300px" MaxLength="50" />
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
