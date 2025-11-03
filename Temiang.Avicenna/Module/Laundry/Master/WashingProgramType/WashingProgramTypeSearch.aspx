<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="WashingProgramTypeSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Laundry.Master.WashingProgramTypeSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblSRLaundryProcessType" runat="server" Text="Category"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRLaundryProcessType" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRLaundryProgram" runat="server" Text="Program"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRLaundryProgram" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRLaundryType" runat="server" Text="Laundry Type"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRLaundryType" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" />
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
