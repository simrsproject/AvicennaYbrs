<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Codebehind="ChartOfAccountSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ChartOfAccountSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label"> 
                <asp:Label ID="lblAccountID" runat="server" Text="Account Code" Width="100px"></asp:Label>
			</td>
            <td class="filter">
            </td>
            <td class="entry">
				<telerik:RadTextBox ID="txtAccountID" runat="server" Width="300px" MaxLength="15"/>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label"> 
                <asp:Label ID="lblAccountName" runat="server" Text="Account Name" Width="100px"></asp:Label>
			</td>
            <td class="filter">
            </td>
            <td class="entry">
				<telerik:RadTextBox ID="txtAccountName" runat="server" Width="300px" MaxLength="100"/>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label"> 
                <asp:Label ID="lblAccountLevel" runat="server" Text="Account Level" Width="100px"></asp:Label>
			</td>
            <td class="filter">
            </td>
            <td class="entry">
				<telerik:RadComboBox ID="cboAccountLevel" runat="server" Width="300px" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label"> 
                <asp:Label ID="lblGeneralAccount" runat="server" Text="General Account" Width="100px"></asp:Label>
			</td>
            <td class="filter">
            </td>
            <td class="entry">
				<telerik:RadTextBox ID="txtGeneralAccount" runat="server" Width="300px" MaxLength="100"/>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>

