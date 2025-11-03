<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="TestResultTemplateSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.TestResultTemplateSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblTestResultName" runat="server" Text="Test Result Name" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterTestResultName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtTestResultName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblParamedicID" runat="server" Text="Physician" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterParamedicID" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" Selected="true" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" HighlightTemplatedItems="True"
                    MarkFirstMatch="true" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboParamedicID_ItemDataBound"
                    OnItemsRequested="cboParamedicID_ItemsRequested">
                    <FooterTemplate>
                        Note : Show max 15 result
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
