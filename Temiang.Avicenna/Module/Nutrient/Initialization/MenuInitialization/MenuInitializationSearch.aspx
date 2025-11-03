<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="MenuInitializationSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Initialization.MenuInitializationSearch" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblStartingDate" runat="server" Text="Starting Date" Width="100px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtStartingDate" runat="server" Width="100px" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>