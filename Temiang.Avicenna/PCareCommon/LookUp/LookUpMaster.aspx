<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="LookUpMaster.aspx.cs" Inherits="Temiang.Avicenna.PCareCommon.LookUpMaster" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="label">
                <asp:Label ID="lblAssetName" runat="server" Text="Search"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtSearch" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20px">
                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                    OnClick="btnSearch_Click" />
            </td>
            <td></td>
        </tr>
        <tr runat="server" id="trMessage">
            <td colspan="4">
                <asp:Label ID="lblMessage" runat="server"></asp:Label></td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="10"
        AllowSorting="True" GridLines="None">
        <MasterTableView DataKeyNames="Code">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Code" HeaderText="Code"
                    UniqueName="Code" SortExpression="Code" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="Name" HeaderText="Name" UniqueName="Name"
                    SortExpression="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
