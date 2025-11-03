<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Codebehind="QuestionFormInServiceUnitSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.QuestionFormInServiceUnitSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblServiceUnitID" runat="server" Text="ID" Width="100px"></asp:Label></td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterServiceUnitID" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100%">
                </telerik:RadTextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblServiceUnitName" runat="server" Text="Service Unit" Width="100px"></asp:Label></td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterServiceUnitName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="100%">
                </telerik:RadTextBox>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
