<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Codebehind="LocationSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.LocationSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblLocationName" runat="server" Text="Location Name" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterLocationName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtLocationName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td>
            </td>
        </tr>
        <tr style="display: none">
            <td class="label">
                <asp:Label ID="lblParentID" runat="server" Text="Parent Name" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterParentID" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtParentID" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td>
            </td>
        </tr>
        <tr style="display: none">
            <td class="label">
                <asp:Label ID="lblItemGroupID" runat="server" Text="Item Group Name" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterItemGroupID" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtItemGroupID" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td>
            </td>
        </tr>
        <tr style="display: none">
            <td class="label">
                <asp:Label ID="lblItemSubGroupID" runat="server" Text="Item Sub Group Name" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterItemSubGroupID" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtItemSubGroupID" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
