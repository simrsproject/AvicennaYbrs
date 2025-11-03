<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="SmfSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.SmfSearch" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblSmfID" runat="server" Text="SMF ID" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterSmfID" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtSmfID" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSmfName" runat="server" Text="SMF Name" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterSmfName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtSmfName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>