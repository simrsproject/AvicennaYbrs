<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Codebehind="EmbalaceSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.EmbalaceSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblEmbalaceID" runat="server" Text="Embalace ID" Width="100px"></asp:Label></td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterEmbalaceID" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtEmbalaceID" runat="server" Width="100%">
                </telerik:RadTextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblEmbalaceName" runat="server" Text="Embalace Name" Width="100px"></asp:Label></td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterEmbalaceName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtEmbalaceName" runat="server" Width="100%">
                </telerik:RadTextBox>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
