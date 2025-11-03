<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="SanitationWasteSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Management.SanitationWasteSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterTransactionNo" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" />
            </td>
            <td></td>
        </tr>
        <tr runat="server" id="trFromServiceUnitID">
            <td class="label">
                <asp:Label ID="lblFromServiceUnitID" runat="server" Text="From Service Unit" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterFromServiceUnitID" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboFromServiceUnitID" runat="server" Width="300px" DataTextField="ServiceUnitName"
                    DataValueField="ServiceUnitID" AllowCustomText="true" MarkFirstMatch="true" />
            </td>
            <td></td>
        </tr>
        <tr runat="server" id="trSupplierID">
            <td class="label">
                <asp:Label ID="lblSupplierID" runat="server" Text="Third Party Name" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterSupplierID" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboSupplierID" Width="300px" EnableLoadOnDemand="true"
                    HighlightTemplatedItems="true" OnItemDataBound="cboSupplierID_ItemDataBound" 
                    OnItemsRequested="cboSupplier_ItemsRequested" EmptyMessage="Select...">
                    <FooterTemplate>
                        Note : Show max 20 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
