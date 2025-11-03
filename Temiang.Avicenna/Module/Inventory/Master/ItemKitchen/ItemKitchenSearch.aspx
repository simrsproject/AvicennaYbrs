<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="ItemKitchenSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.ItemKitchenSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblItemGroup" runat="server" Text="Item Group" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterItemGroup" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboItemGroupID" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblItemID" runat="server" Text="Item ID" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterItemID" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtItemID" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblItemName" runat="server" Text="Item Name" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterItemName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td></td>
        </tr>
        <asp:Panel runat="server" ID="pnlNewUpload">
            <tr>
                <td class="label"></td>
                <td class="filter"></td>
                <td class="entry">
                    <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Checked="true" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label"></td>
                <td class="filter"></td>
                <td class="entry">
                    <asp:CheckBox ID="chkIsNewUpload" runat="server" Text="New Upload" />
                </td>
                <td></td>
            </tr>
        </asp:Panel>
    </table>
</asp:Content>
