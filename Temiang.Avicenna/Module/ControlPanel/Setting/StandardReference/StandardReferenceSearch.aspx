<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Codebehind="StandardReferenceSearch.aspx.cs" Inherits="Temiang.Avicenna.ControlPanel.Setting.StandardReferenceSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblStandardReferenceID" runat="server" Text="Reference ID"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterStandardReferenceID" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtStandardReferenceID" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblStandardReferenceName" runat="server" Text="Reference Name"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterStandardReferenceName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtStandardReferenceName" runat="server" Width="300px" MaxLength="500" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblStandardReferenceGroup" runat="server" Text="Standard Reference Group"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboStandardReferenceGroup" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtStandardReferenceGroup" runat="server" Width="300px" MaxLength="500" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblItemID" runat="server" Text="Reference Item ID"></asp:Label>
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
                <telerik:RadTextBox ID="txtItemID" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="labellong">
                <asp:Label ID="lblItemName" runat="server" Text="Reference Item Name"></asp:Label>
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
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
