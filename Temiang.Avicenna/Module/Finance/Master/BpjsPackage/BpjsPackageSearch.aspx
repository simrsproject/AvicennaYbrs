<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="BpjsPackageSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.BpjsPackageSearch" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblPackageID" runat="server" Text="ID" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterPackageID" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtPackageID" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPackageName" runat="server" Text="Grouper Inacbg" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterPackageName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtPackageName" runat="server" Width="300px" MaxLength="250" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>